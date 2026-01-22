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
	public class LNAN_QUESTIONNAIREDB:SHMA.CodeVision.Data.DataClassBase
	{
		public LNAN_QUESTIONNAIREDB(DataHolder dh):base(dh){}		
		
		public override String TableName
		{		
			get {return "LNAN_QUESTIONNAIRE";}		
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

		public DataHolder getMedicalQuestionnaireData(string proposal,string prodCode,string default_)
		{
            string qcode = string.Empty;
            if (default_ == "1")
            {
                qcode = "0970";
            }
            else
            {
                qcode = "0971";
            }
            StringBuilder query = new StringBuilder();
            query.Append("Select * from ( ");
            query.Append("select lc.cqn_code, lc.cqn_desc, lc.cqn_condition, lc.cqn_short ");
            query.Append("  from lcqn_questionnaire lc ");
            query.Append("inner join lpqn_questionnaire lp ");
            query.Append("  on lp.cqn_code = lc.cqn_code ");
            query.Append(" where lp.ppr_prodcd = ? ");
            query.Append(" and lc.cqn_code<>'" + qcode + "'  and lc.cqn_type=? ");
            query.Append(" union all ");
            query.Append("select lc.cqn_code, lc.cqn_desc, lc.cqn_condition, lc.cqn_short ");
            query.Append("  from lcqn_questionnaire lc ");
            query.Append("inner join lpqn_questionnaire lp ");
            query.Append("  on lp.cqn_code = lc.cqn_code ");
            query.Append(" where lp.ppr_prodcd = ? ");
            query.Append(" and lc.cqn_code='"+ qcode + "' and lc.cqn_type=?  )");

            StringBuilder query_ = new StringBuilder();

			query_.Append("select lc.cqn_code,lc.cqn_desc,lc.cqn_condition,lc.cqn_short from lcqn_questionnaire lc ");
			query_.Append("inner join lpqn_questionnaire lp on ");
			query_.Append("lp.cqn_code = lc.cqn_code ");//and lp.pqn_default = ? 
            query_.Append("where lp.ppr_prodcd = ? order by cqn_code ");			
			IDbCommand myCommand = DB.CreateCommand(query.ToString(), DB.Connection);
			//myCommand.Parameters.Add(DB.CreateParameter("@pqn_default",DbType.String, 1, default_));
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
            myCommand.Parameters.Add(DB.CreateParameter("@cqn_type", DbType.String, 1, default_));
            myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd", DbType.String, 3, prodCode));
            myCommand.Parameters.Add(DB.CreateParameter("@cqn_type", DbType.String, 1, default_));
            if (this.Holder==null)
            {
                this.Holder = new DataHolder();
            }
            this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE_DATA");return this.Holder;			
		}
		public static string getAnswerOfQuestion(string proposal,string prodcode,string cqncode)
		{
			StringBuilder query_ = new StringBuilder();
			query_.Append("select CQN_SUBCODE||'-'||nqn_answer from lnan_questionnaire ");
			query_.Append(" where	np1_proposal = ? and ");			
			query_.Append("			np2_setno = 1 and ");
			query_.Append("			ppr_prodcd = ? and ");
			query_.Append("			cqn_code = ? ");
			IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodcode));			
			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 16, cqncode));						
			IDataReader rdr = myCommand.ExecuteReader();
			object answer = "";
			if(rdr.Read())
			{   
				answer = rdr.GetValue(0);
			}
			rdr.Close();
			
			return answer!=null ? answer.ToString():"";
		}
        public static string getQuestionTotalMarks(string cqncode)
        {
            StringBuilder query_ = new StringBuilder();
            query_.Append("Select cqn_score from lcqd_questionsubdetail where cqn_code=? ");
            query_.Append("and cqn_subcode='0'");
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@cqn_code", DbType.String, 16, cqncode));
            IDataReader rdr = myCommand.ExecuteReader();
            object answer = "";
            if (rdr.Read())
            {
                answer = rdr.GetValue(0);
            }
            rdr.Close();

            return answer != null ? answer.ToString() : "";
        }

        public DataHolder LoadAssessmentQuestionnaireData(string proposal,string prodCode)
		{		
			String strQuery = "select * from lnan_questionnaire where np1_proposal = ? and np2_setno = 1 and ppr_prodcd = ?";			
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
			this.Holder.FillData(myCommand, "LNAN_QUESTIONNAIRE");return this.Holder;
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
