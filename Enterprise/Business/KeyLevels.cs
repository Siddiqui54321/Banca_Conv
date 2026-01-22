using System;
using System.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Shared.Security; 
using SHMA.Enterprise.Data;

namespace SHMA.Enterprise.Business {
	public class KeyLevels : SHMA.Enterprise.TableModule {
		public KeyLevels(DataHolder dh):base(dh, "KeyLevels"){
		}
		int GetMyParent(string tableName, string colName){
			int[] keyLevels = (int[]) KeyLevelsDB.keyLevels(tableName, colName).ToArray(Type.GetType("System.Double"));
			int keyCount = keyLevels.Length;
			int myKey = keyLevels[keyCount-1];
			int myParent=0;
			for (int i=0; i<keyCount-2; i++){	
				myParent += keyLevels[i];
			}
			return myParent;
		}
		public static void CheckKeyLevels(string tableID, NameValueCollection columnNameValue){
			string keyColumn = null;
			System.Collections.ArrayList keyList = KeyLevelsDB.keyLevels(tableID);
			int[] keyLevels;

			if (keyList.Count>0){
				keyColumn = keyList[0].ToString();
				keyList.RemoveAt(0);
				keyLevels = (int[])keyList.ToArray(Type.GetType("System.Int32"));				
				string keyValue = columnNameValue[keyColumn]==null?"":columnNameValue[keyColumn].ToString();
				int len=0;
				for (int i = 0;i<keyLevels.Length; i++){
					len += keyLevels[i];
					if (keyValue.Length == len){
						return;
					}
				}
				throw new ApplicationException("Violation of defined key level(s)");
			}
			
		}	

		public static void CheckKeyLevelsByEntityID(string entityID, NameValueCollection columnNameValue) {
			string keyColumn = null;
			System.Collections.ArrayList keyList = new System.Collections.ArrayList() ;

			if (columnNameValue["POR_ORGACODE"]==null)
			{
				 keyList = KeyLevelsDB.keyLevelsByEntityID(entityID);
			}
			else
			{
			     keyList = KeyLevelsDB.keyLevelsByEntityID(entityID,columnNameValue["POR_ORGACODE"].ToString());
			}

			int[] keyLevels;

			if (keyList.Count>0) {
				keyColumn = keyList[0].ToString();
				keyList.RemoveAt(0);
				keyLevels = (int[])keyList.ToArray(Type.GetType("System.Int32"));				
				string keyValue = columnNameValue[keyColumn]==null?"":columnNameValue[keyColumn].ToString();
				int len=0;
				bool validKey = false;
				int lvlNo = 0;
				for (int i = 0;i<keyLevels.Length; i++) {
					len += keyLevels[i];
					if (keyValue.Length == len) {
						validKey = true;
						lvlNo = i+1;
					}
				}
				if(validKey) {
					IDataReader entryColumns = KeyLevelsDB.getUpdateAbleColumnsRO(entityID);
					if (entryColumns != null){
						if (entryColumns.Read()){
							if(entryColumns["PSE_GRPDTLFIELD"]!=null && entryColumns["PSE_GRPDTLFIELD"].ToString().Length>0)
							{
									if (lvlNo==keyLevels.Length) 
									columnNameValue[entryColumns["PSE_GRPDTLFIELD"]] = "D";
								else 
									columnNameValue[entryColumns["PSE_GRPDTLFIELD"]] = "G";

								if(lvlNo>1)
									columnNameValue[entryColumns["PSE_PARENTKEYFIELD"]] = keyValue.Substring(0,keyValue.Length - keyLevels[lvlNo-1]);
								else
									columnNameValue[entryColumns["PSE_PARENTKEYFIELD"]] = DBNull.Value;
								columnNameValue[entryColumns["PSE_LVLNOFIELD"]] = lvlNo;
							}
						entryColumns.Close();
						}
					}
				}
				else
					throw new ApplicationException("Violation of defined key level(s)");
			}
		}

		public static string GetMyParent(string tableID, string columnId, NameValueCollection columnNameValue){
			System.Collections.ArrayList keyList = KeyLevelsDB.keyLevels(tableID, columnId);
			int[] keyLevels;
			if (keyList.Count>0){
				keyLevels = (int[])keyList.ToArray(Type.GetType("System.Int32"));
				int len=0;
				for (int i=0; i<keyLevels.Length-1; i++){
					len += keyLevels[i];
				}	
				return columnNameValue[columnId].ToString().Substring(0, len);
			}
			else
				return "";			
		}	
	}
}