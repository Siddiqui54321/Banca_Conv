using System;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
//using System.Data.OracleClient;

namespace Security
{
	/// <summary>
	/// Summary description for LoginService.
	/// </summary>
	public class LogingUtility
	{
		public LogingUtility(){}
	
		#region "Login Log Related"

		/// <summary>
		/// Create Log for Wrong attempt/Not successeded for Login
		/// </summary>
		/// <param name="userCode">User Code</param>
		/// <param name="remarks">Reason: Why user not able to LogIn</param>
		public static void GenerateLoginLog(string userCode, string remarks)
		{
			GenerateLoginLog(userCode, false, DateTime.Now, remarks);
		}

		/// <summary>
		/// For Successful Login
		/// </summary>
		/// <param name="userCode">User Code</param>
		/// <param name="loginTime">Login Date Time</param>
		public static void GenerateLoginLog(string userCode, DateTime loginTime)
		{
			GenerateLoginLog(userCode, true, loginTime, "");
		}

		private static void GenerateLoginLog(string userCode, bool successful, DateTime dtTime, string remarks)
		{
			if(LOGINLOG())
			{
				string query = "INSERT INTO SLL_LOGINLOG (SLL_ID,SLL_DATETIME,USE_USERID,SLL_SUCCESSFUL,SLL_REMARKS) VALUES(?,?,?,?,?) ";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.clear();
				pc.puts("@SLL_ID",getAutoNumber("SLL_LOGINLOG", "SLL_ID"));
				pc.puts("@SLL_DATETIME", dtTime);
				pc.puts("@USE_USERID",userCode);
				pc.puts("@SLL_SUCCESSFUL",successful==true ? "Y" : "N");
				pc.puts("@SLL_REMARKS",remarks);
				DB.executeDML(query,pc);
			}
		}
		
		public static void Logout()
		{
			try
			{
				//************* Activity Log - Logout *************//
				GenerateActivityLog(ACTIVITY.LOGOUT);

				EnvHelper env = new EnvHelper();
				string userCode =  Convert.ToString(env.getAttribute("s_USE_USERID")); 
				DateTime loginTime = Convert.ToDateTime(env.getAttribute("s_LOGINTIME"));

				string query = "UPDATE SLL_LOGINLOG SET SLL_LOGOUT=? WHERE USE_USERID=? AND SLL_DATETIME=? ";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.clear();
				pc.puts("@SLL_LOGOUT", DateTime.Now);
				pc.puts("@USE_USERID",userCode);
				pc.puts("@SLL_DATETIME", loginTime);
				DB.executeDML(query,pc);
			}
			catch(Exception e)
			{
				//
				return;
			}

		}

		private static bool LOGINLOG()
		{
			try
			{
				string loginLog = System.Configuration.ConfigurationSettings.AppSettings["LoginLog"];
				if(Convert.ToString(loginLog).ToUpper() == "N")
					return false;
				else
					return true;
			}
			catch(Exception ex)
			{
				return true;
			}
		}
		#endregion



		#region "Activity Log Related functions"
		/// <summary>
		/// Generate Activity Log with Proposal as Document Reference 
		/// </summary>
		/// <param name="activityCode">Security.ACTIVITY enum</param>
		public static void GenerateActivityLog(Security.ACTIVITY activity)
		{
			GenerateActivityLog(activity,null);
		}

		/// <summary>
		/// Generate Activity Log
		/// </summary>
		/// <param name="activityCode">Security.ACTIVITY enum</param>
		/// <param name="docReference">Document Refrence</param>
		public static void GenerateActivityLog(Security.ACTIVITY activity, string docReference)
		{
			if(activity != ACTIVITY.NONE)
			{
				if(ACTIVITYLOG())
				{
					EnvHelper env = new EnvHelper();

					string userCode = Convert.ToString(env.getAttribute("s_USE_USERID")); 
					if(activity == ACTIVITY.LOGIN || activity == ACTIVITY.LOGOUT)
					{
						docReference = "";
					}
					else if(docReference == null) 
					{
						docReference = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
					}
					int activityCode = Convert.ToInt16(activity);

					string query = "INSERT INTO SAL_ACTIVITYLOG (SAL_ID,SAL_DATETIME,SAL_DOCREF,USE_USERID,SAV_ID) VALUES(?,?,?,?,?) ";
					SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
					pc.clear();
					pc.puts("@SAL_ID",getAutoNumber("SAL_ACTIVITYLOG", "SAL_ID"));
					//pc.puts("@SAL_ID",getNextNumber());
					pc.puts("@SAL_DATETIME",DateTime.Now);
					pc.puts("@SAL_DOCREF",docReference);
					pc.puts("@USE_USERID",userCode);
					pc.puts("@SAV_ID", activityCode);
					DB.executeDML(query,pc);
				}
			}
		}


		private static int getAutoNumber(string table, string column)
		{
			try
			{
				int id = 1;
				rowset rs = DB.executeQuery("SELECT NVL(MAX(" + column + "),0)+1 AUTONUMBER FROM " + table + " ");
				if(rs.next())
				{
					id= rs.getInt("AUTONUMBER");
				}
				return id;
			}
			catch(Exception ex)
			{
				return 1;
			}
		}
		
		private static string getNextNumber()
		{	
			ace.ProcedureAdapter cs = new  ace.ProcedureAdapter("GENERATE_SEQ_CALL");			
			cs.Set("P_SEQTYPE", System.Data.OleDb.OleDbType.VarChar,"BAAL");
			cs.Set("P_COMB", System.Data.OleDb.OleDbType.VarChar, "01");
			cs.RegisetrOutParameter("LOGSEQ", System.Data.OleDb.OleDbType.Numeric,1000);
			string logSeq = string.Empty;
			try
			{
				cs.Execute();
				logSeq = cs.Get("LOGSEQ").ToString();				
			}
			 catch(Exception ex){				
				throw new Exception("An Error Occured during the Processing of the request,Please contact HO.");
			}
					
			return logSeq;
		}

		private static bool ACTIVITYLOG()
		{
			try
			{
				string activityLog = System.Configuration.ConfigurationSettings.AppSettings["ActivityLog"];
				if(activityLog.ToUpper() == "N")
					return false;
				else
					return true;
			}
			catch(Exception ex)
			{
				return true;
			}
		}
		#endregion


	}

	public enum ACTIVITY
	{	
		NONE = -1,

		// *** Login *** //
		LOGIN = 0, 
		LOGOUT = 99, 
		
		// *** Proposal *** //
		PROPOSAL_SELECTED = 1, 
		PROPOSAL_GENERATED = 2, 
		PROPOSAL_VALIDATED_STANDARD = 3, 
		PROPOSAL_VALIDATED_SUBSTANDARD = 4, 
		PROPOSAL_POSTED = 5, 
		PROFILE_UPDATED = 20,
		ADDRESS_UPDATED = 21,
		BENEFICIARY_UPDATED = 22,
		MEDICAL_QUESTIONS_UPDATED = 23,
		SECOND_LIFE_UPDATED = 24,

		// *** Product *** //
		PLAN_UPDATED = 10,
		PREMIUM_CALCULATED = 11,
		RIDERS_UPDATED = 12,

		// *** Printing *** //
		ILLUSTRATION_PRINTED = 30,
		ADVICE_PRINTED = 31,
		PERSONAL_PROFILE_PRINTED = 32,
		POLICY_PRINTED = 33,
		PROPOSAL_INQUIRY_PRINTED = 34,
		PAYMENT_HISTORY = 35,   //chg-his
		POLICY_STATUS = 36,   //chg-his
        RPT_DDAFORM = 37, // for JSB Report from Proposal Status 16 page
        RPT_DDAJSB =38, // for JSB Report single page new tab
        RPT_PROFILEJS= 39 // for JSB Report Personal profile

    }
}
