using System;
using System.Data;
using SHMA.Enterprise;

namespace SHMA.Enterprise.Shared.Security{
	public class ApplicationLog{
		public static void WriteLog(string appCode, string action, string tableName, string entity, string keyComb, string valueComb, string levelCode, string userCode, string flag, DateTime date){
			string qry =  "INSERT INTO SH_SM_AD_APPACTAUDLOG(SAA_APPCODE, SAV_ACTCODE, SAD_TABLEID, PSE_ENTITYID, SAU_KEYCOMB, SAD_VALUE, SUL_LEVELCODE, SUS_USERCODE, SAD_FLAG, SAD_DATE) VALUES(";
			qry = string.Format("{0}'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', ?)", qry ,appCode , action,  tableName,  entity,  keyComb,  valueComb,  levelCode,  userCode, flag);			
//			IDbCommand cmd = DB.CreateCommand(qry, DB.Connection);
//			cmd.Parameters.Add(DB.CreateParameter("@SAD_DATE", DbType.DateTime, date));
//			if ((int)cmd.ExecuteNonQuery()<1)
//				throw new ApplicationException("Cannot update log . . .");
		}
	}
}