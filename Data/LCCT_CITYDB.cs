using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCCT_CITYDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCCT_CITYDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCCT_CITY";}
			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCCT_CITY";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LCCT_CITY WHERE CCN_CTRYCD=? AND CCT_PROVCD=? AND CCT_CITYCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, pkNameValue["CCN_CTRYCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@CCT_PROVCD",DbType.String, 3, pkNameValue["CCT_PROVCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@CCT_CITYCD",DbType.String, 3, pkNameValue["CCT_CITYCD"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetCITY_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetCITY_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(243);//to do we have to Optimize it too.
sb_query.Append("SELECT CCT_CITYCD,CCT_DESCR,CCN_CTRYCD,CCT_PROVCD FROM LCCT_CITY WHERE  ({0} like '{1}')  AND ((CCN_CTRYCD=SV(\"CCN_CTRYCD\") AND CCT_PROVCD=SV(\"CPR_PROVCD\"))  )  ORDER BY CCT_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>GetCITY_lister_RO</method-name><method-signature>
	public static IDataReader GetCITY_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(243);//to do we have to Optimize it too.
sb_query.Append("SELECT CCT_CITYCD,CCT_DESCR,CCN_CTRYCD,CCT_PROVCD FROM LCCT_CITY WHERE ((CCN_CTRYCD=SV(\"CCN_CTRYCD\") AND CCT_PROVCD=SV(\"CPR_PROVCD\"))  ) ORDER BY CCT_DESCR ");
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
const String strQuery = "SELECT * FROM LCCT_CITY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCN_CTRYCD,string CCT_CITYCD,string CCT_PROVCD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCCT_CITY WHERE CCN_CTRYCD=? AND CCT_PROVCD=? AND CCT_CITYCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, CCN_CTRYCD));
myCommand.Parameters.Add(DB.CreateParameter("@CCT_PROVCD",DbType.String, 3, CCT_PROVCD));
myCommand.Parameters.Add(DB.CreateParameter("@CCT_CITYCD",DbType.String, 3, CCT_CITYCD));

this.Holder.FillData(myCommand, "LCCT_CITY");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
