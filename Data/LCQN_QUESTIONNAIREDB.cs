using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCQN_QUESTIONNAIREDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCQN_QUESTIONNAIREDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCQN_QUESTIONNAIRE";}
			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCQN_QUESTIONNAIRE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LCQN_QUESTIONNAIRE WHERE CQN_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 16, pkNameValue["CQN_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLCQN_QUESTIONAIRE_lister_RO</method-name><method-signature>
	public static IDataReader GetLCQN_QUESTIONAIRE_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(150);//to do we have to Optimize it too.
sb_query.Append("SELECT CQN_CODE,CQN_DESC FROM LCQN_QUESTIONNAIRE  ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCQN_QUESTIONNAIRE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CQN_CODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCQN_QUESTIONNAIRE WHERE CQN_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 16, CQN_CODE));

this.Holder.FillData(myCommand, "LCQN_QUESTIONNAIRE");return this.Holder;
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLCQN_QUESTIONAIRE_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLCQN_QUESTIONAIRE_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(150);//to do we have to Optimize it too.
sb_query.Append("SELECT CQN_CODE,CQN_DESC FROM LCQN_QUESTIONNAIRE   WHERE  ({0} like '{1}') ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>
	//<method><method-name>GetDDL_PRODUCTQUESTION_CQN_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_PRODUCTQUESTION_CQN_CODE_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(271);//to do we have to Optimize it too.
		sb_query.Append("SELECT CQN_CODE,CQN_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("CQN_DESC DESC_F  FROM LCQN_QUESTIONNAIRE  WHERE CQN_CODE NOT IN(SELECT CQN_CODE FROM LPQN_QUESTIONNAIRE WHERE PPR_PRODCD=SV(\"PPR_PRODCD1\")) order by cqn_code");
		sb_query.Append("CQN_DESC DESC_F  FROM LCQN_QUESTIONNAIRE order by cqn_code ");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

}
}
