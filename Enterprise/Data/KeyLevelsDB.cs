using System;
using System.Data;
using System.Collections;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data; 

namespace SHMA.Enterprise.Data{
	public class KeyLevelsDB : SHMA.Enterprise.DataGateway{
		public KeyLevelsDB(DataHolder dh):base(dh){}

		public override String TableName{
			get {return "PR_ED_KL_KEYLEVELS";}
		}

		public static int CountByID (int typeCode){
			String strQuery="SELECT COUNT(*) FROM KeyLevels where Code= ?";
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@Code", DbType.Int32 , 8, typeCode)) ;
			return (int) myCommand.ExecuteScalar(); 
		}

		public static ArrayList keyLevelsByEntityID(string entityID) {
			//const String strQuery = "SELECT PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH FROM PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID INNER JOIN PR_ED_EC_ENTITYCOLUMN ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_EC_ENTITYCOLUMN.PSE_ENTITYID AND PR_ED_EC_ENTITYCOLUMN.PEC_GRPDTLFIELD = 'D' WHERE (PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID = ?) AND (PR_ED_KL_KEYLEVELS.POR_ORGACODE = ?) ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			String strQuery = "SELECT PSE_LVLFIELD, PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH FROM PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID WHERE (PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID = ?) AND (PR_ED_KL_KEYLEVELS.POR_ORGACODE = SV(\"POR_ORGACODE\")) ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			strQuery = EnvHelper.Parse(strQuery);
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_TABLEID", DbType.String, entityID));
			//	myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, tableID));
			//	myCommand.Parameters.Add(DB.CreateParameter("@PSE_FIELDID", DbType.String, columnId));
			// myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, "SV(\"POR_ORGACODE\")"));
			IDataReader keyReader = myCommand.ExecuteReader();
			ArrayList kLevels = new ArrayList();
			bool colAdd = true;
			while (keyReader.Read()) {
				if (colAdd)
				{
					kLevels.Add(keyReader.GetValue(0).ToString());
					colAdd=false;
				}
				kLevels.Add(int.Parse(keyReader.GetValue(1).ToString()));
			}
			keyReader.Close();			
			return kLevels;
		}

		public static ArrayList keyLevelsByEntityID(string entityID, string orgaCode) 
		{
			//const String strQuery = "SELECT PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH FROM PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID INNER JOIN PR_ED_EC_ENTITYCOLUMN ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_EC_ENTITYCOLUMN.PSE_ENTITYID AND PR_ED_EC_ENTITYCOLUMN.PEC_GRPDTLFIELD = 'D' WHERE (PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID = ?) AND (PR_ED_KL_KEYLEVELS.POR_ORGACODE = ?) ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			String strQuery = "SELECT PSE_LVLFIELD, PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH FROM PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID WHERE (PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID = ?) AND (PR_ED_KL_KEYLEVELS.POR_ORGACODE = ?) ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_TABLEID", DbType.String, entityID));
			//	myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, tableID));
			//	myCommand.Parameters.Add(DB.CreateParameter("@PSE_FIELDID", DbType.String, columnId));
			 myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, orgaCode));
			IDataReader keyReader = myCommand.ExecuteReader();
			ArrayList kLevels = new ArrayList();
			bool colAdd = true;
			while (keyReader.Read()) 
			{
				if (colAdd)
				{
					kLevels.Add(keyReader.GetValue(0).ToString());
					colAdd=false;
				}
				kLevels.Add(int.Parse(keyReader.GetValue(1).ToString()));
			}
			keyReader.Close();			
			return kLevels;
		}


		public static ArrayList keyLevels(string tableID, string columnId){			
			const String strQuery = "SELECT PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH FROM PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON PR_ED_KL_KEYLEVELS.PSE_ENTITYID = PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID WHERE (PR_ED_SE_SYSTEMENTITY.PSE_TABLEID = ?) AND (PR_ED_KL_KEYLEVELS.PKL_FIELDID = ?) AND (PR_ED_KL_KEYLEVELS.POR_ORGACODE = ?) ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_TABLEID", DbType.String, tableID));
			//myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, tableID));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_FIELDID", DbType.String, columnId));
			//myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, "001"));
			IDataReader keyReader = myCommand.ExecuteReader();
			ArrayList kLevels = new ArrayList();
			while (keyReader.Read()){
				kLevels.Add(int.Parse(keyReader.GetValue(0).ToString()));
			}
			keyReader.Close();
			return kLevels;
		}
		public static ArrayList keyLevels(string tableID){			
			//const String strQuery = "SELECT DISTINCT PR_ED_KL_KEYLEVELS.PKL_LVLNO, PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH AS Expr1, PR_ED_SE_SYSTEMENTITY.PSE_LVLFIELD AS Expr2 FROM         PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY PR_ED_EC_ENTITYCOLUMN.PSE_ENTITYID = PR_ED_KL_KEYLEVELS.PSE_ENTITYID WHERE     (PR_ED_SE_SYSTEMENTITY.PSE_TABLEID = ?)  AND (PR_ED_EC_ENTITYCOLUMN.PEC_COLLEVELBREAKUP = 'Y') ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			const String strQuery = "SELECT DISTINCT PR_ED_KL_KEYLEVELS.PKL_LVLNO, PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH AS Expr1, PR_ED_SE_SYSTEMENTITY.PSE_LVLFIELD AS Expr2 FROM  PR_ED_KL_KEYLEVELS INNER JOIN PR_ED_SE_SYSTEMENTITY ON  PR_ED_SE_SYSTEMENTITY.PSE_ENTITYID = PR_ED_KL_KEYLEVELS.PSE_ENTITYID WHERE  (PR_ED_SE_SYSTEMENTITY.PSE_TABLEID = ?)  ORDER BY PR_ED_KL_KEYLEVELS.PKL_LVLNO";
			//string column="";
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_TABLEID", DbType.String, tableID));
			//myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, tableID));
			//myCommand.Parameters.Add(DB.CreateParameter("@PSE_FIELDID", DbType.String, columnId));
			//myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, "001"));
			IDataReader keyReader = myCommand.ExecuteReader();
			ArrayList kLevels = new ArrayList();			
			if (keyReader.Read()){
				kLevels.Add(keyReader.GetValue(2));
				kLevels.Add(int.Parse(keyReader.GetValue(1).ToString()));
			}
			while (keyReader.Read()){
				kLevels.Add(int.Parse(keyReader.GetValue(1).ToString()));
			}
			keyReader.Close();			
			return kLevels;			
		}

		public static int GetKeyLength(string entityID){			
			System.Text.StringBuilder  strQuery = new System.Text.StringBuilder("SELECT SUM(PR_ED_KL_KEYLEVELS.PKL_LVLWIDTH) AS PKL_LVLWIDTH ");
			//strQuery.Append("FROM PR_ED_SE_ENTITYCOLUMN INNER JOIN PR_ED_KL_KEYLEVELS ");
			strQuery.Append("FROM PR_ED_KL_KEYLEVELS ");
			//strQuery.Append("ON PR_ED_EC_ENTITYCOLUMN.PSE_ENTITYID = PR_ED_KL_KEYLEVELS.PSE_ENTITYID ");
			//strQuery.Append("WHERE (PR_ED_EC_ENTITYCOLUMN.PEC_COLLEVELBREAKUP = 'Y' AND PR_ED_KL_KEYLEVELS.PSE_ENTITYID = ?) ");
			strQuery.Append("WHERE  (PR_ED_KL_KEYLEVELS.PSE_ENTITYID = ?) ");
			IDbCommand myCommand = DB.CreateCommand(strQuery.ToString());		
			//myCommand.Parameters.Add(DB.CreateParameter("@PKL_LVLTABLEID", DbType.String, tableID));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityID));
			//myCommand.Parameters.Add(DB.CreateParameter("@PKL_FIELDID", DbType.String, columnId));
			object obj = myCommand.ExecuteScalar();
			if (obj==null)
				return 0;	//throw new ApplicationException("key level not found for " + tableID);
			else if (obj.Equals(DBNull.Value)){
				return 0;
			}
			else
				return int.Parse(obj.ToString());
		}

		public static IDataReader getUpdateAbleColumnsRO(string entityId){
			String strQuery = "SELECT PSE_GRPDTLFIELD, PSE_PARENTKEYFIELD, PSE_LVLNOFIELD FROM PR_ED_SE_SYSTEMENTITY WHERE PSE_ENTITYID = '"+ entityId +"'";
			IDbCommand myCommand = DB.CreateCommand(strQuery);
			IDataReader keyReader = myCommand.ExecuteReader();
			return keyReader;		
		}

		public static int GetCodeLength(string tableID, string columnId){			
			const String strQuery = "SELECT SUM(PKL_LVLWIDTH) FROM PR_ED_KN_KEYNUMGEN WHERE (PKL_LVLTABLEID = ?) AND (PSE_ENTITYID = ?) and (PKL_FIELDID=?)";
			IDbCommand myCommand = DB.CreateCommand(strQuery);		
			myCommand.Parameters.Add(DB.CreateParameter("@PKL_LVLTABLEID", DbType.String, tableID));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, tableID));
			myCommand.Parameters.Add(DB.CreateParameter("@PKL_FIELDID", DbType.String, columnId));
			object obj = myCommand.ExecuteScalar();
			if (obj==null)
				throw new ApplicationException("key level not found for " + columnId);
			else
				return int.Parse(obj.ToString());
		}


	}
}
