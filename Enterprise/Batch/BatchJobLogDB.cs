using System;
using System.Data;
using SHMA.Enterprise.Data;




namespace SHMA.Enterprise.Batch
{

    enum LogTypes { 
        MasterRecord = 0,
        DetailRecord = 1,
        Successfull = 2,
        UnSuccesfull = 3
    }

    public sealed class BatchJobLogDB
	{

		#region Job
		public static DataTable getJobErrors(string sessionId, string processName, string referenceNo, string user)
		{
		    string query = "select PBL_SEQNO,PBL_DATETIME,PBL_LOGTYPE,PBL_STATUSDETAIL from PR_ED_BP_BATCHPROCESSLOG" +
                " where PBP_SESSIONID=?  and   PBP_CLASSNAME=? and PBP_REFERENCENO=? and PBP_USER=? " +
                " and PBL_LOGTYPE = 'E'";

            ParameterCollection ParaColl = new ParameterCollection();

            ParaColl.clear();
            ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
            ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
            ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
            ParaColl.puts("@PBP_USER", user, Types.VARCHAR);

			return BatchDML.executeQuery(query,ParaColl);
		}

        public static int getJobErrorCount(string sessionId, string processName, string referenceNo, string user)
        {
            string query = "select PBL_SEQNO,PBL_DATETIME,PBL_LOGTYPE,PBL_STATUSDETAIL from PR_ED_BP_BATCHPROCESSLOG" +
                " where PBP_SESSIONID=?  and   PBP_CLASSNAME=? and PBP_REFERENCENO=? and PBP_USER=? " +
                " and PBL_LOGTYPE = 'E'";

            ParameterCollection ParaColl = new ParameterCollection();

            ParaColl.clear();
            ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
            ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
            ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
            ParaColl.puts("@PBP_USER", user, Types.VARCHAR);

            DataTable dt = BatchDML.executeQuery(query, ParaColl);
            return dt.Rows.Count;
        }

        public static int getJobErrorCount(string sessionId, string processName, string referenceNo, string user, string threadId)
        {
            string query = "select PBL_SEQNO,PBL_DATETIME,PBL_LOGTYPE,PBL_STATUSDETAIL from PR_ED_BP_BATCHPROCESSLOG " +
                " where PBP_SESSIONID=?  and   PBP_CLASSNAME=? and PBP_REFERENCENO=? and PBP_USER=? and PBL_THREADID = ? " +
                " and PBL_LOGTYPE = 'E'";

            ParameterCollection ParaColl = new ParameterCollection();

            ParaColl.clear();
            ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
            ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
            ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
            ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
            ParaColl.puts("@PBL_THREADID", threadId, Types.VARCHAR);

            DataTable dt = BatchDML.executeQuery(query, ParaColl);
            return dt.Rows.Count;
        }


        public static void InsertBatchJobLog(string sessionId, string processName, string referenceNo, string user, string SeqNo, DateTime datetime, string LogType, string statusDesc)
		{
			try
			{
				ParameterCollection ParaColl = new ParameterCollection();
				System.Text.StringBuilder sb_query=new System.Text.StringBuilder(181);
				sb_query.Append(" INSERT INTO PR_ED_BP_BATCHPROCESSLOG  ");
				sb_query.Append(" (PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,PBL_SEQNO,PBL_DATETIME,PBL_LOGTYPE,PBL_STATUSDETAIL)  ");
				sb_query.Append(" Values (?,?,?,?,?,?,?,?) ");

				ParaColl.clear();
				ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
				ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
				ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
				ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
                ParaColl.puts("@PBL_SEQNO", SeqNo, Types.VARCHAR);
				ParaColl.puts("@PBL_DATETIME", datetime, Types.DATE);
				ParaColl.puts("@PBL_LOGTYPE", LogType, Types.VARCHAR);
				ParaColl.puts("@PBL_STATUSDETAIL", statusDesc, Types.VARCHAR);
				BatchDML.executeDML(sb_query.ToString(), ParaColl);
			}
			catch(Exception e)
			{
                throw new Exception("InsertBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
			}
		}

        // Overloads to log ThreadId 
        public static void InsertBatchJobLog(string sessionId, string processName, string referenceNo, string user, string SeqNo, DateTime datetime, string LogType, string statusDesc, string ThreadId)
        {
            try
            {
                ParameterCollection ParaColl = new ParameterCollection();
                System.Text.StringBuilder sb_query = new System.Text.StringBuilder(181);
                sb_query.Append(" INSERT INTO PR_ED_BP_BATCHPROCESSLOG  ");
                sb_query.Append(" (PBP_SESSIONID,PBP_CLASSNAME,PBP_REFERENCENO,PBP_USER,PBL_SEQNO,PBL_DATETIME,PBL_LOGTYPE,PBL_STATUSDETAIL,PBL_THREADID) ");
                sb_query.Append(" Values (?,?,?,?,?,?,?,?,?) ");

                ParaColl.clear();
                ParaColl.puts("@PBP_SESSIONID", sessionId, Types.VARCHAR);
                ParaColl.puts("@PBP_CLASSNAME", processName, Types.VARCHAR);
                ParaColl.puts("@PBP_REFERENCENO", referenceNo, Types.VARCHAR);
                ParaColl.puts("@PBP_USER", user, Types.VARCHAR);
                ParaColl.puts("@PBL_SEQNO", SeqNo, Types.VARCHAR);
                ParaColl.puts("@PBL_DATETIME", datetime, Types.DATE);
                ParaColl.puts("@PBL_LOGTYPE", LogType, Types.VARCHAR);
                ParaColl.puts("@PBL_STATUSDETAIL", statusDesc, Types.VARCHAR);
                ParaColl.puts("@PBL_THREADID", ThreadId, Types.VARCHAR);
                BatchDML.executeDML(sb_query.ToString(), ParaColl);
            }
            catch (Exception e)
            {
                throw new Exception("InsertBatchLog- " + e.Message.ToString());
                //System.Console.WriteLine(e.Message);
            }
        }

        public static void UpdateBatchJobLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user)
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
                ParaColl.puts("@PBP_STATUSDETAIL", statusDesc, Types.VARCHAR);
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
        public static void UpdateBatchJobLog(string status, string statusDesc, string sessionId, string processName, string referenceNo, string user, DateTime endDate)
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
				ParaColl.puts("@PBP_STATUSDETAIL", statusDesc, Types.VARCHAR);
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


	}
}
