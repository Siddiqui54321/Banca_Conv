using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCCL_CATEGORYDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCCL_CATEGORYDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCCL_CATEGORY";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCCL_CATEGORY";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_NM_OccupationalClass_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_OccupationalClass_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(166);//to do we have to Optimize it too.
sb_query.Append("SELECT CCL_CATEGORYCD FROM LCCL_CATEGORY ORDER BY CCL_DESCRIPTION ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCL_CATEGORYCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCL_CATEGORYCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(211);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCL_CATEGORYCD,");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("'-'");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("CCL_DESCRIPTION DESC_F  FROM LCCL_CATEGORY  ORDER BY CCL_DESCRIPTION");

		//sb_query.Append("CCL_CATEGORYCD||'-'||CCL_DESCRIPTION DESC_F  FROM LCCL_CATEGORY  ORDER BY CCL_CATEGORYCD");
		sb_query.Append("CCL_DESCRIPTION DESC_F  FROM LCCL_CATEGORY  ORDER BY CCL_CATEGORYCD");
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
String strQuery = "SELECT count(*) FROM LCCL_CATEGORY WHERE CCL_CATEGORYCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCL_CATEGORYCD",DbType.String, 3, pkNameValue["CCL_CATEGORYCD"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_OccupationalClass_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_OccupationalClass_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(166);//to do we have to Optimize it too.
sb_query.Append("SELECT CCL_CATEGORYCD FROM LCCL_CATEGORY  WHERE  ({0} like '{1}') ORDER BY CCL_DESCRIPTION ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_CCL_CATEGORYCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_CCL_CATEGORYCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(211);//to do we have to Optimize it too.
sb_query.Append("SELECT CCL_CATEGORYCD,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("CCL_DESCRIPTION DESC_F  FROM LCCL_CATEGORY  ORDER BY CCL_DESCRIPTION");
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
const String strQuery = "SELECT * FROM LCCL_CATEGORY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCL_CATEGORYCD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCCL_CATEGORY WHERE CCL_CATEGORYCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCL_CATEGORYCD",DbType.String, 3, CCL_CATEGORYCD));

this.Holder.FillData(myCommand, "LCCL_CATEGORY");return this.Holder;
			//</method-body>
	}
	//</method>

}
}
