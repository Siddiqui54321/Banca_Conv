using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class CCH_CHANNELDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public CCH_CHANNELDB     (DataHolder dh):base(dh)
		
	
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "CCH_CHANNEL";}
			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM CCH_CHANNEL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetDESC_ILUS_DH_CHANNELSUBDTL_CN_RO</method-name><method-signature>
	public static String GetDESC_ILUS_DH_CHANNELSUBDTL_CN_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
sb_query.Append("SELECT CCH_DESCR DESC_F  FROM CCH_CHANNEL WHERE CCH_CODE=SV(\"CCH_CODE\")");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return (String)myCommand.ExecuteScalar();
	//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_CCH_CHANNEL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_CCH_CHANNEL_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCH_CODE FROM CCH_CHANNEL  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_CCH_CHANNEL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_CCH_CHANNEL_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCH_CODE FROM CCH_CHANNEL   WHERE  ({0} like '{1}') ");
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
	public DataHolder FindByPK(string CCH_CODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM CCH_CHANNEL WHERE CCH_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, CCH_CODE));

this.Holder.FillData(myCommand, "CCH_CHANNEL");return this.Holder;
			//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM CCH_CHANNEL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNEL_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNEL_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(271);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCH_CODE,");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//			sb_query.Append("''");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());

			sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '784') ORDER BY CCH_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCH_CODE,CCH_CODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL  ");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>


	public static IDataReader GetDDL_CHANNELS()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCH_CODE,CCH_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CCH_DESCR DESC_F FROM CCH_CHANNEL ORDER BY CCH_CODE ");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}

	//<method><method-name>GetILUS_ST_CHANNEL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_CHANNEL_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
sb_query.Append("SELECT CCH_CODE,CCH_DESCR FROM CCH_CHANNEL   WHERE  ({0} like '{1}') ORDER BY CCH_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM CCH_CHANNEL WHERE CCH_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, pkNameValue["CCH_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ST_CHANNEL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_CHANNEL_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
sb_query.Append("SELECT CCH_CODE,CCH_DESCR FROM CCH_CHANNEL ORDER BY CCH_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDESC_ILUS_DH_CHANNELDETAIL_CN_RO</method-name><method-signature>
	public static String GetDESC_ILUS_DH_CHANNELDETAIL_CN_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(203);//to do we have to Optimize it too.
sb_query.Append("SELECT CCH_DESCR DESC_F  FROM CCH_CHANNEL WHERE CCH_CODE=SV(\"CCH_CODE\") AND CCH_CODE=SV(\"CCH_CODE\")");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return (String)myCommand.ExecuteScalar();
		//</method-body>
	}
	//</method>

}
}
