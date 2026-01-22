using System;
using System.Data;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared.Log;

namespace SHMA.Enterprise.Presentation.ErrorMessageFilter {
	/// <summary>
	/// Summary description for ErrorMessageFilter.
	/// </summary>
	public class ErrorMessageFilter {
		private ErrorMessageFilter() {
		}
		public static string Parse(System.Exception ex){
			CustomExceptionLogger.Log(ex);
			return DBErrorMessageFilter.Parse(ex);
		}
	}

	public class DBErrorMessageFilter{
		private DBErrorMessageFilter(){}
		public static string Parse(System.Exception ex){
			System.Data.OleDb.OleDbException oleDbEx=null;
			System.Data.SqlClient.SqlException sqlEx=null;
			if (ex is System.Data.OleDb.OleDbException)
				oleDbEx = (System.Data.OleDb.OleDbException) ex;
			else if(ex is System.Data.SqlClient.SqlException)
				sqlEx = (System.Data.SqlClient.SqlException)ex;
			else
				return ex.Message;
				
			IDataReader errMsgs=null;
			string ErrorCode=null;
			switch (DB.dataAccessType){
				case(DB.DataAccessType.DB2):
					ErrorCode = Convert.ToString(oleDbEx.Errors[oleDbEx.Errors.Count-1].NativeError);
					break;
				case(DB.DataAccessType.Sql):
					ErrorCode = Convert.ToString(sqlEx.Errors[sqlEx.Errors.Count-1]);
					break;
				case(DB.DataAccessType.OleDB): 
					ErrorCode = Convert.ToString(oleDbEx.Errors[oleDbEx.Errors.Count-1].NativeError);
					break;
			}

			switch(DB.dataBaseType){
				case(DB.DataBaseType.SQLServer):
					errMsgs = ErrorMessageFilterDB.getSQLerrorMessage(ErrorCode);
					break;
				case(DB.DataBaseType.Oracle):
					errMsgs = ErrorMessageFilterDB.getORAerrorMessage(ErrorCode);
					break;
				case(DB.DataBaseType.DB2):
					errMsgs = ErrorMessageFilterDB.getDB2errorMessage(ErrorCode);
					break;
			}

			if(errMsgs!=null && errMsgs.Read()){
				string dbMsg;
				dbMsg = Convert.ToString(errMsgs["PEM_ERMSDESC"]);
				errMsgs.Close();
				return "<ErrorMessage>" + dbMsg + "<ErrorMessageDetail>" + ex.Message + ":Method:" + ex.TargetSite;
			}
			else
				return ErrorCode + ":" + ex.Message;

		}
	}
}
