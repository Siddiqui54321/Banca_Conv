using System;
using SHMA.Enterprise.Configuration;
using System.Text;
using System.Data;
using System.Data.OleDb;
using SHMA.Enterprise.Data;


namespace SHMA.Enterprise.Shared.Log
{
    public class DBErrorLogger
    {

		public static void logExecutionError(string Query ,string ParameterList, string strStackTrace , string strErrorDesc )
		{
			string strErrorCode = getErrorCode(strErrorDesc);
			string strQuery = Query;
			string strParameter = ParameterList;
			string strInsertQry = "Insert Into PR_ED_DL_DBERRORLOG (PDL_LOGDATETIME,PDL_ERRORCODE,PDL_STACKTRACE,PDL_ERRORDESC,PDL_QUERYTEXT,PDL_PARAMLIST) VALUES(?,?,?,?,?,?)";
			
			ParameterCollection PCollection = new ParameterCollection();
			PCollection.clear();
			PCollection.puts("@PDL_LOGDATETIME",    DateTime.Now,		Types.DATE);
			PCollection.puts("@PDL_ERRORCODE",		strErrorCode,       Types.VARCHAR);
			PCollection.puts("@PDL_STACKTRACE",		strStackTrace,		Types.VARCHAR);
			PCollection.puts("@PDL_ERRORDESC",		strErrorDesc,       Types.VARCHAR);
			PCollection.puts("@PDL_QUERYTEXT",		strQuery,			Types.VARCHAR);
			PCollection.puts("@PDL_PARAMLIST",		strParameter,		Types.VARCHAR);

			DB.executeDML_Adhoc(strInsertQry,PCollection,30);	
		}

		public static void logExecutionError(string strQuery ,string strStackTrace , string strErrorDesc )
		{
			logExecutionError(strQuery ,"", strStackTrace ,strErrorDesc );
		}
		
		public static void logExecutionError(IDbCommand cmd ,string strStackTrace , string strErrorDesc )
		{
			string strQuery = cmd.CommandText;
			string strParameter = getParamList(cmd);

			logExecutionError(strQuery ,strParameter, strStackTrace ,strErrorDesc );
		}

		public static string getParamList(IDbCommand cmd)
		{
		  string strParamList = "";
		  string str_ParamName = "";
          string str_ParamValue = "";
		
			if (cmd.Parameters.Count > 0)
			{
				Object[] pArray = new Object[cmd.Parameters.Count];            
				cmd.Parameters.CopyTo(pArray, 0);


				for(int i=0; i< cmd.Parameters.Count ;i++)
				{
					str_ParamName = Convert.ToString(((System.Data.OleDb.OleDbParameter)pArray.GetValue(i)).ParameterName);
					str_ParamValue = Convert.ToString(((System.Data.OleDb.OleDbParameter)pArray.GetValue(i)).Value);
					strParamList = strParamList + str_ParamName + " - " + str_ParamValue + " ; ";
				}
			}

			return strParamList;
		
		}


		private static string getErrorCode(string ErrorDesc)
		{
			string ErrorCode = "G"; //General Error;
			if (ErrorDesc.ToLower().IndexOf("deadlock") < 0)
			{
				ErrorCode = "D"; // DeadLock Error;
			}
			else if (ErrorDesc.ToLower().IndexOf("timeout") < 0)
			{
				ErrorCode = "T"; // TimeOut Error;
			}
			else if (ErrorDesc.ToLower().IndexOf("violation of primary key") < 0)
			{
				ErrorCode = "P"; // Primary Key Error;
			}
			else if (ErrorDesc.ToLower().IndexOf("violation of foreign key") < 0)
			{
				ErrorCode = "F"; // Foreign Key Error;
			}

			return ErrorCode ;
		}
	}
}
