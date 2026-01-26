using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
//using System.Data.OracleClient;
using System.Data.OleDb;

namespace SHAB.Data
{
	public class LCQD_QUESTIONSUBDETAIL : SHMA.CodeVision.Data.DataClassBase
	{
		public LCQD_QUESTIONSUBDETAIL(DataHolder dh):base(dh){}		
		
		public override String TableName
		{		
			get {return "LCQD_QUESTIONSUBDETAIL"; }		
		}
		public static IDataReader getMedicalSubQuestions(string cqnCode,string prodCode)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT B.cqn_code,B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION FROM LCCN_COLUMN A ");
			sb.Append(" INNER JOIN LCQD_QUESTIONDETAIL B ");
			sb.Append(" ON B.CCN_COLUMNID=A.CCN_COLUMNID ");
			sb.Append(" INNER JOIN lcqn_questionnaire C ON ");
			sb.Append(" C.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN lpqn_questionnaire D ");
			sb.Append(" ON D.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN LPPR_PRODUCT E ");
			sb.Append(" ON E.PPR_PRODCD=D.PPR_PRODCD ");
			sb.Append(" WHERE B.CQN_CODE = ? ");
			sb.Append(" AND D.PPR_PRODCD = ? ");
			IDbCommand myCommand = DB.CreateCommand(sb.ToString(), DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 10, cqnCode));
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));			
			return myCommand.ExecuteReader();
		}
		public DataHolder getAssessmentQuestionnaireData(string cch_code, string ccd_code, string ccs_code,string default_)
		{
            StringBuilder query = new StringBuilder();
            query.Append("Select lq.CQN_CODE,lq.CQN_DESC,lq.CQN_SHORT,lq.CQN_TYPE,lq.CQN_CONDITION,lq.CQN_CRTLTYPE, lq.CQN_QTYPE ");
            query.Append(" from lcqn_questionnaire lq,LACH_ASSESSMENT la ");
            query.Append("where CQN_QTYPE in ('QLC', 'QNC') ");
            query.Append(" and lq.cqn_code=la.cqn_code ");
            query.Append(" and lq.cqn_type = la.cqn_type ");
            query.Append("and la.cch_code=? ");
            query.Append("and la.ccd_code=? ");
            query.Append("and la.ccs_code=? ");
            query.Append("and lq.cqn_type = ? ");
			query.Append("order by lq.cqn_qtype , SUBSTR(lq.CQN_CODE, 3)");

            IDbCommand myCommand = DB.CreateCommand(query.ToString(), DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@cch_code", DbType.String, 2, cch_code));
            myCommand.Parameters.Add(DB.CreateParameter("@ccd_code", DbType.String, 2, ccd_code));
            myCommand.Parameters.Add(DB.CreateParameter("@ccs_code", DbType.String, 4, ccs_code));
            myCommand.Parameters.Add(DB.CreateParameter("@cqn_type", DbType.String, 1, default_));
            if (this.Holder==null)
            {
                this.Holder = new DataHolder();
            }
            this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE_DATA");return this.Holder;			
        }
        public DataHolder getAssessmentQuestionnaireOptionsData(string cqn_code)
        {
            StringBuilder query = new StringBuilder();
            query.Append("select CQN_CODE,CQN_TYPE,CQN_SUBCODE,CQN_SUBDESC,CQN_SUBCODE||'-'||CQN_SCORE as CQN_SCORE,CQN_VALUETYPE, ");
            query.Append("CQN_RANGEFROM,CQN_RANGETO,CQN_VALUEDESC,CQN_STATUS,CQN_EFFECTIVE ");
            query.Append("From lcqd_questionsubdetail where cqn_code=? and CQN_STATUS='Y'");
            IDbCommand myCommand = DB.CreateCommand(query.ToString(), DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@cqn_code", DbType.String, 10, cqn_code));
            if (this.Holder == null)
            {
                this.Holder = new DataHolder();
            }
            else
            {
                if (this.Holder.Data.Tables.Count>1)
                {
                    this.Holder.Data.Tables.RemoveAt(1);
                }
                
            }
            this.Holder.FillData(myCommand, "lcqd_questionsubdetail_DATA"); return this.Holder;
        }
        public static string getQuestionChannelMapping(string CCH_CODE, string CCD_CODE, string CCS_CODE)
		{
			StringBuilder query_ = new StringBuilder();
			query_.Append("SELECT COUNT(*) fROM LACH_ASSESSMENT   ");
			query_.Append(" WHERE CQN_TYPE='1'");			
			query_.Append("			AND CCH_CODE=? ");
			query_.Append("			AND CCD_CODE=? ");
			query_.Append("			AND CCS_CODE=? ");
			IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE", DbType.String, 2, CCH_CODE));			
			myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE", DbType.String, 2, CCD_CODE));			
			myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE", DbType.String, 6, CCS_CODE));						
			IDataReader rdr = myCommand.ExecuteReader();
			object answer = "";
			if(rdr.Read())
			{   
				answer = rdr.GetValue(0);
			}
			rdr.Close();
			
			return answer!=null ? answer.ToString():"0";
		}
        public static string getQueryResult(string query, string paramter)
        {
            StringBuilder query_ = new StringBuilder();
            query_.Append(query);
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal", DbType.String, 16, paramter));
            IDataReader rdr = myCommand.ExecuteReader();
            object answer = "";
            if (rdr.Read())
            {
                answer = rdr.GetValue(0);
            }
            rdr.Close();

            return answer != null ? answer.ToString() : "0";
        }

        public DataHolder LoadMedicalQuestionnaireData(string proposal,string prodCode)
		{		
			String strQuery = "select * from lnqn_questionnaire where np1_proposal = ? and np2_setno = 1 and ppr_prodcd = ?";			
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
			this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE");return this.Holder;
		}

		public static Boolean Exists(string proposal,string prodCode,string cqnCode)
		{			
			String strQuery = "select count(*) from lnqn_questionnaire where np1_proposal = ? and np2_setno = ? and ppr_prodcd = ?";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 10, proposal));
			myCommand.Parameters.Add(DB.CreateParameter("@np2_setno",DbType.UInt32, 2, 1));
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 16, cqnCode));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);			
		}
		
		public DataHolder FindByPK(string proposal,string prodCode,string cqnCode)
		{			
			String strQuery = "select * from lnqn_questionnaire where np1_proposal = ? and np2_setno = ? and ppr_prodcd = ? and cqn_code = ?";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 10, proposal));
			myCommand.Parameters.Add(DB.CreateParameter("@np2_setno",DbType.UInt32, 10, 1));
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 10, prodCode));
			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 10, cqnCode));
			this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE");return this.Holder;			
		}


		public static bool isConditionTrue(string prodCode,string proposal,string cqnCode,string condID)
		{			
			ace.ProcedureAdapter call =  new ace.ProcedureAdapter("CHECK_LCQNCONDITION_CALL",(OleDbConnection)DB.Connection);
			string cond = "";
			try
			{
				call.Set("P_PROPOSAL", OleDbType.VarChar, proposal);
				call.Set("P_PRODCD",   OleDbType.VarChar, prodCode);
				call.Set("P_SETNO",    OleDbType.Numeric, 1);
				call.Set("P_QUESTION", OleDbType.VarChar, cqnCode);
				call.Set("P_CONDITION",OleDbType.VarChar, condID);
				call.RegisetrOutParameter("MRTRNSTRING",OleDbType.VarChar,1000);
				//call.Execute();

				/*ace.OracleClientAdapter call =  new ace.OracleClientAdapter("CHECK_LCQNCONDITION", DB.Connection);
				call.RegisetrInParameter("P_PROPOSAL", OracleType.VarChar, 50);
				call.Set("P_PROPOSAL", OracleType.VarChar, proposal);
				call.Set("P_PRODCD", OracleType.VarChar, prodCode);
				call.Set("P_SETNO", OracleType.Number,1);
				call.Set("P_QUESTION", OracleType.VarChar, cqnCode);
				call.Set("P_CONDITION", OracleType.VarChar,100,condID);
				call.RegisetrReturnParameter("mRtrnString",OracleType.VarChar,1000);*/
			
				call.Execute();			
				cond = call.Get("mRtrnString").ToString();						
				//call.Close();
			}
			catch(Exception ex){
			
			}
			return cond.ToString().Equals("Y") ? true : false;
		}
	}
}
