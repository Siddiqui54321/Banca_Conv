using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCCN_COLUMNDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCCN_COLUMNDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCCN_COLUMN";}
			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCCN_COLUMN";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetLCCN_COLUMN_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLCCN_COLUMN_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(172);//to do we have to Optimize it too.
sb_query.Append("SELECT CCN_COLUMNID,CCN_DESCRIPTION FROM LCCN_COLUMN WHERE  ({0} like '{1}')  AND CCN_COLUMNID LIKE 'FK%' order by CCN_COLUMNID ");
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
String strQuery = "SELECT count(*) FROM LCCN_COLUMN WHERE CCN_COLUMNID=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_COLUMNID",DbType.String, 5, pkNameValue["CCN_COLUMNID"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLCCN_COLUMN_lister_RO</method-name><method-signature>
	public static IDataReader GetLCCN_COLUMN_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(172);//to do we have to Optimize it too.
sb_query.Append("SELECT CCN_COLUMNID,CCN_DESCRIPTION FROM LCCN_COLUMN WHERE CCN_COLUMNID LIKE 'FK%'  order by CCN_COLUMNID ");
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
const String strQuery = "SELECT * FROM LCCN_COLUMN";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCN_COLUMNID)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCCN_COLUMN WHERE CCN_COLUMNID=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_COLUMNID",DbType.String, 5, CCN_COLUMNID));

this.Holder.FillData(myCommand, "LCCN_COLUMN");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
