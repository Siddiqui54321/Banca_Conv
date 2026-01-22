using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCRL_RELATIONDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCRL_RELATIONDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCRL_RELATION";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCRL_RELATION";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>


	//<method><method-name>GetDDL_ILUS_BENEFECIARY_CRL_RELEATIOCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_BENEFECIARY_CRL_RELEATIOCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(160);//to do we have to Optimize it too.
		sb_query.Append("SELECT CRL_RELEATIOCD,CRL_DESCR DESC_F  FROM LCRL_RELATION WHERE UPPER(SUBSTR(CRL_RELEATIOCD,1,1))<>'Z' ORDER BY 2");
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
String strQuery = "SELECT count(*) FROM LCRL_RELATION WHERE CRL_RELEATIOCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CRL_RELEATIOCD",DbType.String, 3, pkNameValue["CRL_RELEATIOCD"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_LCRL_RELATION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_LCRL_RELATION_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(160);//to do we have to Optimize it too.
sb_query.Append("SELECT CRL_RELEATIOCD FROM LCRL_RELATION  WHERE  ({0} like '{1}') ORDER BY CRL_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_LCRL_RELATION_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_LCRL_RELATION_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(160);//to do we have to Optimize it too.
sb_query.Append("SELECT CRL_RELEATIOCD FROM LCRL_RELATION ORDER BY CRL_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_TB_BENEFECIARY_CRL_RELEATIOCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_TB_BENEFECIARY_CRL_RELEATIOCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(199);//to do we have to Optimize it too.
sb_query.Append("SELECT CRL_RELEATIOCD,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("CRL_DESCR DESC_F  FROM LCRL_RELATION  ORDER BY CRL_DESCR");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCRL_RELATION";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CRL_RELEATIOCD)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT * FROM LCRL_RELATION WHERE CRL_RELEATIOCD=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CRL_RELEATIOCD",DbType.String, 3, CRL_RELEATIOCD));

		this.Holder.FillData(myCommand, "LCRL_RELATION");return this.Holder;
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ST_GUARDIAN_CRL_RELEATIOCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ST_GUARDIAN_CRL_RELEATIOCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(181);//to do we have to Optimize it too.
		//sb_query.Append("SELECT CRL_RELEATIOCD,CRL_RELEATIOCD");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("'-'");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("SELECT CRL_RELEATIOCD,CRL_DESCR DESC_F  FROM LCRL_RELATION  WHERE UPPER(SUBSTR(CRL_RELEATIOCD,1,1))='Z' ORDER BY 2");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_BENEFECIARY_CRL_RELATIOCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_BENEFECIARY_CRL_RELATIOCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(160);//to do we have to Optimize it too.
		sb_query.Append("SELECT CRL_RELEATIOCD,CRL_DESCR DESC_F  FROM LCRL_RELATION  ");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

}
}
