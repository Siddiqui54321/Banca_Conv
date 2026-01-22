using System;
using System.Configuration;
using SHMA.Enterprise.Exceptions;

namespace SHMA.Enterprise.Configuration{
	public sealed class AppSettings{
		public enum DataAccessType{
			None ,
			SqlDB ,
			OleDB,
			DB2,
			Oracle
		}
		public static DataAccessType DBAccessType{
				get{	
				switch(System.Configuration.ConfigurationSettings.AppSettings["DBType"]){
					case "SqlDB":
						return DataAccessType.SqlDB;
					case "OleDB":
						return DataAccessType.OleDB;
					case "DB2":
						return DataAccessType.DB2;
					case "Oracle":					
						return DataAccessType.Oracle;					
					default: 
						return DataAccessType.None;
				}	
			}
		}
		public static string GetSetting(string key){

			return System.Configuration.ConfigurationSettings.AppSettings[key];
		}

//		public static int GetInt(string key, string group){
//			NameValueCollection config = (NameValueCollection)ConfigurationSettings.GetConfig(group);
//			int numOfRows = 0;
//			if (config["key"] != null)
//				numOfRows = (int)config["NoOfListerRows"];
//			return numOfRows;		
//		}

		public static int GetInt(string key, string group){			
			return int.Parse(GetSetting("NoOfListerRows"));		
		}		
		
		public static int GetInt(string key){
			string val = System.Configuration.ConfigurationSettings.AppSettings[key];
			if (val!=null)
				return int.Parse(System.Configuration.ConfigurationSettings.AppSettings[key]);
			else
				throw new AppSettingNotFoundException(key);
		}		
	}
}