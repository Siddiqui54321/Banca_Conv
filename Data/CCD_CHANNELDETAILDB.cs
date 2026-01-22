using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class CCD_CHANNELDETAILDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public CCD_CHANNELDETAILDB        (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "CCD_CHANNELDETAIL";}
			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM CCD_CHANNELDETAIL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	
	
	//<method><method-name>GetILUS_ST_CHANNELDETAIL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_CHANNELDETAIL_lister_filter_RO(string columnName,string columnValue)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(198);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE,CCD_DESCR,CCH_CODE FROM CCD_CHANNELDETAIL WHERE  ({0} like '{1}')  AND ((CCH_CODE=SV(\"CCH_CODE\"))  )  ORDER BY CCD_DESCR  ");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		query = string.Format(query, columnName, columnValue);
		query = string.Format(query, columnName, columnValue);
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_CCD_CHANNELDETAIL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_CCD_CHANNELDETAIL_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(140);//to do we have to Optimize it too.
sb_query.Append("SELECT CCD_CODE FROM CCD_CHANNELDETAIL  ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
										//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ST_CHANNELDETAIL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_CHANNELDETAIL_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(198);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE,CCD_DESCR,CCH_CODE FROM CCD_CHANNELDETAIL WHERE ((CCH_CODE=SV(\"CCH_CODE\"))  )  ORDER BY CCD_DESCR ");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>


	//<method><method-name>GetILUS_ET_NM_CCD_CHANNELDETAIL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_CCD_CHANNELDETAIL_lister_filter_RO(string columnName,string columnValue)
	{
//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(140);//to do we have to Optimize it too.
sb_query.Append("SELECT CCD_CODE FROM CCD_CHANNELDETAIL   WHERE  ({0} like '{1}') ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
										//</method-body>
	}
	//</method>

	//<method><method-name>GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO</method-name><method-signature>
	public static String GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
sb_query.Append("SELECT CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL WHERE CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\")");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return (String)myCommand.ExecuteScalar();
	//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCD_CODE,string CCH_CODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM CCD_CHANNELDETAIL WHERE CCH_CODE=? AND CCD_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, CCH_CODE));
myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, CCD_CODE));

this.Holder.FillData(myCommand, "CCD_CHANNELDETAIL");return this.Holder;
		//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM CCD_CHANNELDETAIL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
											//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO(string CCH_CODE)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE, CCD_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL WHERE CCH_CODE='" + CCH_CODE + "'");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNELDETAIL_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNELDETAIL_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(211);//to do we have to Optimize it too.
sb_query.Append("SELECT CCD_CODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("''");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='01' ORDER BY CCD_DESCR");
string query=sb_query.ToString();
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
		String strQuery = "SELECT count(*) FROM CCD_CHANNELDETAIL WHERE CCH_CODE=? AND CCD_CODE=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, pkNameValue["CCH_CODE"]));
		myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, pkNameValue["CCD_CODE"]));
		int noOfRecords=(int)myCommand.ExecuteScalar();
		return(noOfRecords>=1);
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO(string CCH_CODE)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE, CCD_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		
		sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL WHERE CCH_CODE='" + CCH_CODE + "' ORDER BY 1 ASC");		
		
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}


//=========== 11-Feb-2019
	public static DataTable GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO_DT(string CCH_CODE)
	{
		DataTable Dt_New = new DataTable();
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE, CCD_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		
		sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL WHERE CCH_CODE='" + CCH_CODE + "' ORDER BY 1 ASC");		
		
		string query=sb_query.ToString();	
		query = EnvHelper.Parse(query);
		
		OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
		con.Open();
		OleDbCommand cmd = new OleDbCommand();
		cmd.Connection = con;
		cmd.CommandText =  query;
		cmd.CommandType = CommandType.Text;
		OleDbDataAdapter da = new OleDbDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds, "USERCHANNEL");
		con.Close();
		DataTable dtTable = ds.Tables["USERCHANNEL"];
		return dtTable;
		//</method-body>
	}
//=========== 11-Feb-2019
	//</method>
	
	/// <summary>
	////=============== IBAD EDITING CODE - MULTIPLE BANK BRANCHES TO SAME USER ====================
	////=============== IBAD EDITING CODE - MULTIPLE BANK BRANCHES TO SAME USER ====================
	/// </summary>
	/// <param name="CCH_CODE"></param>
	/// <returns></returns>




	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_CHANNELDETAIL(string CCH_CODE)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCD_CODE, CCD_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL WHERE CCH_CODE='" + CCH_CODE + "'");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>
}
}
