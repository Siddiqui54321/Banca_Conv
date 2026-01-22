using System;
using System.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;

namespace SHMA.Enterprise.Presentation.ErrorMessageFilter {
	/// <summary>
	/// Summary description for ErrorMessageFilterDB.
	/// </summary>
	public class ErrorMessageFilterDB:SHMA.Enterprise.DataGateway {
		public ErrorMessageFilterDB(DataHolder dh):base(dh)
		{ 	}

		public override String TableName {
			//</property-signature><property-body>
			get {return "PR_GN_EM_ERRORMESSAGE";}
			//			//			//			//</property-body>
		}
		public static int RecordCount {
			//</property-signature><property-body>
			get {
				const string strQuery="SELECT COUNT(*) FROM PR_GN_EM_ERRORMESSAGE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}

		public static IDataReader getDB2errorMessage(string errorCode){
			string strQuery = "SELECT  * FROM PR_GN_EM_ERRORMESSAGE WHERE PEM_ERMSDB2CODE = '" + errorCode + "'";
			IDbCommand myCommand = DB.CreateCommand(strQuery);
			return myCommand.ExecuteReader();
		}
		public static IDataReader getSQLerrorMessage(string errorCode){
			string strQuery = "SELECT * FROM PR_GN_EM_ERRORMESSAGE WHERE PEM_ERMSSQLCODE = '" + errorCode + "'";
			IDbCommand myCommand = DB.CreateCommand(strQuery);
			return myCommand.ExecuteReader();
		}
		public static IDataReader getORAerrorMessage(string errorCode){
			string strQuery = "SELECT * FROM PR_GN_EM_ERRORMESSAGE WHERE PEM_ERMSORACODE = '" + errorCode + "'";
			IDbCommand myCommand = DB.CreateCommand(strQuery);
			return myCommand.ExecuteReader();
		}

	}
}
