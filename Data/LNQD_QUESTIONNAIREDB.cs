using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data
{
	public class LNQD_QUESTIONDETAILDB:SHMA.CodeVision.Data.DataClassBase
	{
		public LNQD_QUESTIONDETAILDB(DataHolder dh):base(dh){}		
		
		public override String TableName
		{		
			get {return "LNQD_QUESTIONDETAIL";}		
		}

		public static IDataReader getMedicalSubQuestions(string cqnCode,string prodCode, string proposal)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT B.cqn_code,B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION, F.NQD_ANSWER FROM LCCN_COLUMN A ");
			sb.Append(" INNER JOIN LCQD_QUESTIONDETAIL B ");
			sb.Append(" ON B.CCN_COLUMNID=A.CCN_COLUMNID ");
			sb.Append(" INNER JOIN lcqn_questionnaire C ON ");
			sb.Append(" C.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN lpqn_questionnaire D ");
			sb.Append(" ON D.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN LPPR_PRODUCT E ");
			sb.Append(" ON E.PPR_PRODCD=D.PPR_PRODCD ");
			sb.Append(" LEFT OUTER JOIN LNQD_QUESTIONDETAIL F ON F.NP1_PROPOSAL = ? AND  ");
            sb.Append("		F.NP2_SETNO = 1 AND F.PPR_PRODCD = D.PPR_PRODCD AND ");
            sb.Append("     F.CQN_CODE = B.CQN_CODE AND F.CCN_COLUMNID = B.CCN_COLUMNID ");
			sb.Append(" WHERE B.CQN_CODE = ? ");
			sb.Append(" AND D.PPR_PRODCD = ? ORDER BY B.CQN_CODE,A.CCN_COLUMNID");
			IDbCommand myCommand = DB.CreateCommand(sb.ToString(), DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 10, cqnCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, prodCode));
			return myCommand.ExecuteReader();
		}
		public DataTable getMedicalSubQuestionsDT(string cqnCode,string prodCode, string proposal)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT B.cqn_code,B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION, F.NQD_ANSWER FROM LCCN_COLUMN A ");
			sb.Append(" INNER JOIN LCQD_QUESTIONDETAIL B ");
			sb.Append(" ON B.CCN_COLUMNID=A.CCN_COLUMNID ");
			sb.Append(" INNER JOIN lcqn_questionnaire C ON ");
			sb.Append(" C.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN lpqn_questionnaire D ");
			sb.Append(" ON D.CQN_CODE=B.CQN_CODE ");
			sb.Append(" INNER JOIN LPPR_PRODUCT E ");
			sb.Append(" ON E.PPR_PRODCD=D.PPR_PRODCD ");
			sb.Append(" LEFT OUTER JOIN LNQD_QUESTIONDETAIL F ON F.NP1_PROPOSAL = ? AND  ");
			sb.Append("		F.NP2_SETNO = 1 AND F.PPR_PRODCD = D.PPR_PRODCD AND ");
			sb.Append("     F.CQN_CODE = B.CQN_CODE AND F.CCN_COLUMNID = B.CCN_COLUMNID ");
			sb.Append(" WHERE B.CQN_CODE = ? ");
			sb.Append(" AND D.PPR_PRODCD = ? ORDER BY B.CQN_CODE,A.CCN_COLUMNID");
			IDbCommand myCommand = DB.CreateCommand(sb.ToString(), DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 10, cqnCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, prodCode));
			Holder.FillData(myCommand, "LCQD_QUESTIONDETAIL_DATA");
			DataTable dt = Holder["LCQD_QUESTIONDETAIL_DATA"].Copy();
			Holder.Data.Tables.Remove("LCQD_QUESTIONDETAIL_DATA");
			return dt;			
		}

//		public DataHolder getMedicalSubQuestionnaireData(string proposal,string prodCode)
//		{		
//			StringBuilder query_ = new StringBuilder();
//			query_.Append("select lc.cqn_code,lc.cqn_desc,lc.cqn_condition from lcqn_questionnaire lc ");
//			query_.Append("inner join lpqn_questionnaire lp on ");
//			query_.Append("lp.cqn_code = lc.cqn_code and lp.pqn_default = 'N' ");			
//			query_.Append("where lp.ppr_prodcd = ? order by cqn_code ");			
//			IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
//			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));			
//			this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE_DATA");return this.Holder;			
//		}
//		public static string getAnswerOfQuestion(string proposal,string prodcode,string cqncode)
//		{
//			StringBuilder query_ = new StringBuilder();
//			query_.Append("select nqn_answer from lnqn_questionnaire ");
//			query_.Append(" where	np1_proposal = ? and ");			
//			query_.Append("			np2_setno = 1 and ");
//			query_.Append("			ppr_prodcd = ? and ");
//			query_.Append("			cqn_code = ? ");
//			IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
//			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
//			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodcode));			
//			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 16, cqncode));						
//			IDataReader rdr = myCommand.ExecuteReader();
//			string answer = "";
//			if(rdr.Read())
//			{
//                answer = rdr.GetString(0);
//			}
//			rdr.Close();
//			
//			return answer;
//		}

		
		public DataHolder LoadMedicalSubQuestionnaireData(string proposal,string prodCode, string cqnCode)
		{		
			String strQuery = "select * from lnqd_questiondetail where np1_proposal = ? and np2_setno = 1 and ppr_prodcd = ? and cqn_code = ?";			
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 16, cqnCode));
			this.Holder.FillData(myCommand, "LNQD_QUESTIONDETAIL");return this.Holder;
		}
		public DataHolder LoadMedicalSubQuestionnaireData(string proposal,string prodCode)
		{		
			String strQuery = "select * from lnqd_questiondetail where np1_proposal = ? and np2_setno = 1 and ppr_prodcd = ?";			
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 12, proposal));			
			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));			
			this.Holder.FillData(myCommand, "LNQD_QUESTIONDETAIL");return this.Holder;
		}

//		public static Boolean Exists(string proposal,string prodCode,string cqnCode)
//		{			
//			String strQuery = "select count(*) from lnqn_questionnaire where np1_proposal = ? and np2_setno = ? and ppr_prodcd = ?";
//			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
//			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 10, proposal));
//			myCommand.Parameters.Add(DB.CreateParameter("@np2_setno",DbType.UInt32, 2, 1));
//			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 3, prodCode));
//			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 16, cqnCode));
//			int noOfRecords=(int)myCommand.ExecuteScalar();
//			return(noOfRecords>=1);			
//		}
//		
//		public DataHolder FindByPK(string proposal,string prodCode,string cqnCode)
//		{			
//			String strQuery = "select * from lnqn_questionnaire where np1_proposal = ? and np2_setno = ? and ppr_prodcd = ? and cqn_code = ?";
//			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
//			myCommand.Parameters.Add(DB.CreateParameter("@np1_proposal",DbType.String, 10, proposal));
//			myCommand.Parameters.Add(DB.CreateParameter("@np2_setno",DbType.UInt32, 10, 1));
//			myCommand.Parameters.Add(DB.CreateParameter("@ppr_prodcd",DbType.String, 10, prodCode));
//			myCommand.Parameters.Add(DB.CreateParameter("@cqn_code",DbType.String, 10, cqnCode));
//			this.Holder.FillData(myCommand, "LNQN_QUESTIONNAIRE");return this.Holder;			
//		}
	}
}
