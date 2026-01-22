using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	/// <summary>
	/// Summary description for LNCM_COMMENTSDB.
	/// </summary>
	public class LNCM_COMMENTSDB : SHMA.CodeVision.Data.DataClassBase
	{
		public LNCM_COMMENTSDB(DataHolder dh):base(dh){	}
		public override String TableName
		{		
			get {return "LNCM_COMMENTS";}		
		}		
		public DataHolder findByPK(string proposal)
		{
			const string query_ = "SELECT * FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ? AND CM_SERIAL_NO=(SELECT MAX(CM_SERIAL_NO) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ?)";
			IDbCommand myCommand = DB.CreateCommand(query_, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, proposal));
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, proposal));
			this.Holder.FillData(myCommand, "LNCM_COMMENTS");return this.Holder;
		}
		public IDataReader getCommentsOfPorposal(string proposal)
		{	
			const string query_ = "SELECT NP1_PROPOSAL, CM_SERIAL_NO, CM_COMMENTDATE, CM_COMMENTBY, DECODE(CM_ACTION,'F','FILE GENERATED','R','COMPLIANCE CHECKED',CM_ACTION) CM_ACTION, CM_COMMENTS FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ? ORDER BY CM_SERIAL_NO DESC";
			IDbCommand myCommand = DB.CreateCommand(query_, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, proposal));
			return myCommand.ExecuteReader();
		}

		public DataHolder getCommentsOfPorposal_New(string proposal)
		{	
			proposal = ""+proposal;
			proposal = proposal.ToString().Replace("'","");

			//string query1 = "SELECT NP1_PROPOSAL, CM_SERIAL_NO, CM_COMMENTDATE, CM_COMMENTBY, DECODE(CM_ACTION,'F','FILE GENERATED','R','COMPLIANCE CHECKED',CM_ACTION) CM_ACTION, CM_COMMENTS FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = '"+proposal+"' ORDER BY CM_SERIAL_NO DESC";
			string query1 = " SELECT LNCM.NP1_PROPOSAL, PH.NPH_FULLNAME, LNCM.CM_SERIAL_NO, LNCM.CM_COMMENTDATE, "+
		"LNCM.CM_COMMENTBY,DECODE(LNCM.CM_ACTION,'F','FILEGENERATED','R','COMPLIANCECHECKED',LNCM.CM_ACTION)CM_ACTION, LNCM.CM_COMMENTS FROM LNCM_COMMENTS LNCM, LNU1_UNDERWRITI LNUI, LNPH_PHOLDER PH WHERE 1 = 1 "+
		"AND LNCM.NP1_PROPOSAL = LNUI.NP1_PROPOSAL AND LNUI.NPH_CODE = PH.NPH_CODE AND LNCM.NP1_PROPOSAL = '"+proposal+"' ORDER BY LNCM.CM_SERIAL_NO DESC";


			//string query1 = "SELECT NP1_PROPOSAL, CM_SERIAL_NO, CM_COMMENTDATE, CM_COMMENTBY, DECODE(CM_ACTION,'F','FILE GENERATED','R','COMPLIANCE CHECKED',CM_ACTION) CM_ACTION, CM_COMMENTS FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ? ORDER BY CM_SERIAL_NO DESC";
			IDbCommand myCommand = DB.CreateCommand(query1, DB.Connection);
			//myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, proposal));
			//return myCommand.ExecuteReader();
			//IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
			this.Holder.FillData(myCommand, "LNCM_COMMENTS_DATA"); 
			return this.Holder;
		}

		public string getActionforProposal(string proposal)
		{	
			const string query_ = "SELECT NP1_PROPOSAL, CM_SERIAL_NO, CM_COMMENTDATE, CM_COMMENTBY, DECODE(CM_ACTION,'F','FILE GENERATED','R','COMPLIANCE CHECKED',CM_ACTION) CM_ACTION, CM_COMMENTS FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ? ORDER BY CM_SERIAL_NO DESC";
		//	IDbCommand myCommand = DB.CreateCommand(query_, DB.Connection);
		//	myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, proposal));
			//return myCommand.ExecuteReader();

			SHMA.Enterprise.Data.ParameterCollection pm=new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL",proposal.ToUpper(),DbType.String);
			rowset rst = DB.executeQuery(query_,pm);
string action ="";
			if (rst.next())
			{
				action = rst.getString("CM_ACTION");
			}
				return action;
		}


		public static int getNextSerial(string proposal)
		{
			const string query_ = "SELECT NVL(MAX(CM_SERIAL_NO),0)+1 FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = ?";
			IDbCommand myCommand = DB.CreateCommand(query_, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, proposal));
			object objSerial = myCommand.ExecuteScalar();
			return objSerial != null ? int.Parse(objSerial.ToString()) : 1;			
		}
		public static void addComments(SHMA.Enterprise.Data.ParameterCollection pc)
		{
			const string query_ = "INSERT INTO LNCM_COMMENTS (NP1_PROPOSAL, CM_SERIAL_NO, CM_COMMENTDATE, CM_COMMENTBY, CM_ACTION, CM_COMMENTS)  values (?,?,?,?,?,?) ";
			DB.executeDML(query_,pc);
		}
		public static void AddUserComments(string proposal,string comments,string status)
		{
			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL",proposal);
			pm.puts("@CM_SERIAL_NO", getNextSerial(proposal).ToString());
            pm.puts("@CM_COMMENTDATE", DateTime.Now);
            //pm.puts("@CM_COMMENTDATE", SHMA.Enterprise.Presentation.SessionObject.GetString("ClossingDate"));   //chg_closing	comments above
			pm.puts("CM_COMMENTBY",SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_USERID"));
			pm.puts("CM_ACTION",status);
			pm.puts("CM_COMMENTS",comments);

			DB.executeDML("INSERT INTO LNCM_COMMENTS (NP1_PROPOSAL,CM_SERIAL_NO,CM_COMMENTDATE,CM_COMMENTBY,CM_ACTION,CM_COMMENTS) values (?,?,?,?,?,?)", pm);

		}

	}
}
