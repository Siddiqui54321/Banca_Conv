using System;
using System.Data;
using SHMA.Enterprise.Data;




namespace SHMA.Enterprise.Batch
{
	public sealed class BatchJobDB
	{

		#region Job
		public static DataTable FindAnyOpenJob()
		{
			string query = "select PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,bp.PBP_STATUS  PBP_STATUS,PBS_STATUSDESC "
						 + "from pr_ed_bp_batchprocess bp, PR_ED_BS_BATCHPROCSTATUS bs where bp.PBP_STATUS = bs.PBS_STATUS and PBS_OPEN ='Y' ";
			return BatchDML.executeQuery(query);
		}

		public static DataTable FindOpenJobForUser(string user)
		{
			string query = "select PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,bp.PBP_STATUS  PBP_STATUS,PBS_STATUSDESC "
						 + "from pr_ed_bp_batchprocess bp, PR_ED_BS_BATCHPROCSTATUS bs where bp.PBP_STATUS = bs.PBS_STATUS and PBS_OPEN ='Y' "
						 + "and PBP_USER = '" + user + "' ";
			return BatchDML.executeQuery(query);		
		}

		public static DataTable FindOpenJobForProcess(string ProcessName)
		{
			string query = "select PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,bp.PBP_STATUS  PBP_STATUS,PBS_STATUSDESC "
						 + "from pr_ed_bp_batchprocess bp, PR_ED_BS_BATCHPROCSTATUS bs where bp.PBP_STATUS = bs.PBS_STATUS and PBS_OPEN ='Y' "
						 + "and PBP_CLASSNAME = '" + ProcessName + "' ";
			return BatchDML.executeQuery(query);		
		}

		public static DataTable FindOpenJobForUserProcess(string user,string ProcessName)
		{
			string query = "select PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,bp.PBP_STATUS  PBP_STATUS,PBS_STATUSDESC "
						 + "from pr_ed_bp_batchprocess bp, PR_ED_BS_BATCHPROCSTATUS bs where bp.PBP_STATUS = bs.PBS_STATUS and PBS_OPEN ='Y' "
						 + "and PBP_CLASSNAME = '" + ProcessName + "' and PBP_USER = '" + user + "' ";
			return BatchDML.executeQuery(query);
		}

		public static DataTable getStatusCollection()
		{
			string query ="SELECT * FROM PR_ED_BS_BATCHPROCSTATUS";
			return SHMA.Enterprise.Batch.BatchDML.executeQuery(query);
		}
		#endregion

		#region Log
		public static DataTable FindBatchLog(string sessionId, string processName, string referenceNo, string user)
		{
			string query = "select PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER from  pr_ed_bp_batchprocess "
				         + "where PBP_SESSIONID='" + sessionId + "' AND PBP_CLASSNAME='" + processName + "' AND PBP_REFERENCENO='" + referenceNo + "' AND PBP_USER='"+ user + "'";
			return BatchDML.executeQuery(query);		
		}

		public static void InsertBatchLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user,DateTime datetime, string processDesc)
		{
			try
			{
				ParameterCollection ParaColl = new ParameterCollection();
				System.Text.StringBuilder sb_query=new System.Text.StringBuilder(181);
				sb_query.Append(" INSERT INTO PR_ED_BP_BATCHPROCESS  ");
				sb_query.Append(" (PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,PBP_DATETIME,PBP_PROCESSDESC,PBP_STATUS,PBP_STATUSDETAIL)  ");
				sb_query.Append(" Values (?,?,?,?,?,?,?,?) ");

				ParaColl.clear();
				ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
				ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
				ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
				ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
				ParaColl.puts("@PBP_DATETIME", datetime, Types.DATE);
                ParaColl.puts("@PBP_PROCESSDESC", Truncate(processDesc,500), Types.VARCHAR);
				ParaColl.puts("@PBP_STATUS", status, Types.VARCHAR);
                ParaColl.puts("@PBP_STATUSDETAIL", Truncate(statusDesc,5000), Types.VARCHAR);
				BatchDML.executeDML(sb_query.ToString(), ParaColl);
			}
			catch(Exception e)
			{
                throw new Exception("InsertBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
			}
		}

        // Overloads to log ThreadId 
        public static void InsertBatchLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user, DateTime datetime, string processDesc, int currThreadId)
        {
            try
            {
                ParameterCollection ParaColl = new ParameterCollection();
                System.Text.StringBuilder sb_query = new System.Text.StringBuilder(181);
                sb_query.Append(" INSERT INTO PR_ED_BP_BATCHPROCESS  ");
                sb_query.Append(" (PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,PBP_DATETIME,PBP_PROCESSDESC,PBP_STATUS,PBP_STATUSDETAIL,PBP_THREADID)  ");
                sb_query.Append(" Values (?,?,?,?,?,?,?,?,?) ");

                ParaColl.clear();
                ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
                ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
                ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
                ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
                ParaColl.puts("@PBP_DATETIME", datetime, Types.DATE);
                ParaColl.puts("@PBP_PROCESSDESC", Truncate(processDesc,500), Types.VARCHAR);
                ParaColl.puts("@PBP_STATUS", status, Types.VARCHAR);
                ParaColl.puts("@PBP_STATUSDETAIL", Truncate(statusDesc,5000), Types.VARCHAR);
                ParaColl.puts("@PBP_THREADID", currThreadId, Types.INTEGER);
                BatchDML.executeDML(sb_query.ToString(), ParaColl);
            }
            catch (Exception e)
            {
                throw new Exception("InsertBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
            }
        }

        public static void UpdateBatchLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user)
        {
            try
            {
                ParameterCollection ParaColl = new ParameterCollection();
                System.Text.StringBuilder sb_query = new System.Text.StringBuilder(181);
                sb_query.Append(" Update PR_ED_BP_BATCHPROCESS  ");
                sb_query.Append(" Set PBP_STATUS=?, PBP_STATUSDETAIL=? ");
                sb_query.Append(" where PBP_SESSIONID=? ");
                sb_query.Append(" and   PBP_CLASSNAME=? ");
                sb_query.Append(" and   PBP_REFERENCENO=? ");
                sb_query.Append(" and   PBP_USER=? ");
                ParaColl.clear();
                ParaColl.puts("@PBP_STATUS", status, Types.VARCHAR);
                ParaColl.puts("@PBP_STATUSDETAIL", Truncate(statusDesc,5000), Types.VARCHAR);
                ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
                ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
                ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
                ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
                BatchDML.executeDML(sb_query.ToString(), ParaColl);
            }
            catch (Exception e)
            {
                throw new Exception("UpdateBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
            }
        }

        // Overloads to log  process completion datetime
        public static void UpdateBatchLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user, DateTime endDate)
		{
			try
			{
				ParameterCollection ParaColl = new ParameterCollection();
				System.Text.StringBuilder sb_query=new System.Text.StringBuilder(181);
				sb_query.Append(" Update PR_ED_BP_BATCHPROCESS  ");
				sb_query.Append(" Set PBP_STATUS=?, PBP_STATUSDETAIL=?, PBP_ENDDATE=? ");
				sb_query.Append(" where PBP_SESSIONID=? ");
				sb_query.Append(" and   PBP_CLASSNAME=? ");
				sb_query.Append(" and   PBP_REFERENCENO=? ");
				sb_query.Append(" and   PBP_USER=? ");
				ParaColl.clear();
				ParaColl.puts("@PBP_STATUS", status, Types.VARCHAR);
				ParaColl.puts("@PBP_STATUSDETAIL", Truncate(statusDesc,5000), Types.VARCHAR);
                ParaColl.puts("@PBP_ENDDATE", endDate, Types.DATE);
				ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
				ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
				ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
				ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
				BatchDML.executeDML(sb_query.ToString(), ParaColl);
			}
			catch(Exception e)
			{
                throw new Exception("UpdateBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
			}
		}

		#endregion

        public static string Truncate(string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("Length must be >= 0");
            }

            if (str == null)
            {
                return null;
            }
            int maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);
        }


	}
}
