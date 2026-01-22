using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LPQN_QUESTIONNAIREDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LPQN_QUESTIONNAIREDB      (DataHolder dh):base(dh)
		
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LPQN_QUESTIONNAIRE";}
			//			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LPQN_QUESTIONNAIRE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LPQN_QUESTIONNAIRE WHERE PPR_PRODCD=? AND CQN_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 16, pkNameValue["CQN_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LPQN_QUESTIONNAIRE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
									//</method-body>
	}
	//</method>

	//<method><method-name>GetPRODUCTQUESTION_lister_RO</method-name><method-signature>
	public static IDataReader GetPRODUCTQUESTION_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(274);//to do we have to Optimize it too.
sb_query.Append("SELECT LPQN.CQN_CODE,CQN_DESC,PPR_PRODCD FROM LPQN_QUESTIONNAIRE LPQN,  LCQN_QUESTIONNAIRE LCQN WHERE LPQN.CQN_CODE = LCQN.CQN_CODE  AND PPR_PRODCD=SV(\"PPR_PRODCD1\")  ORDER BY LPQN.CQN_CODE");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>GetPRODUCTQUESTION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetPRODUCTQUESTION_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(274);//to do we have to Optimize it too.
sb_query.Append("SELECT LPQN.CQN_CODE,CQN_DESC,PPR_PRODCD FROM LPQN_QUESTIONNAIRE LPQN,  LCQN_QUESTIONNAIRE LCQN WHERE  ({0} like '{1}')  AND LPQN.CQN_CODE = LCQN.CQN_CODE  AND PPR_PRODCD=SV(\"PPR_PRODCD1\")  ORDER BY LPQN.CQN_CODE ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CQN_CODE,string PPR_PRODCD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LPQN_QUESTIONNAIRE WHERE PPR_PRODCD=? AND CQN_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, PPR_PRODCD));
myCommand.Parameters.Add(DB.CreateParameter("@CQN_CODE",DbType.String, 16, CQN_CODE));

this.Holder.FillData(myCommand, "LPQN_QUESTIONNAIRE");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
