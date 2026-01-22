using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCPR_PROVINCEDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCPR_PROVINCEDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCPR_PROVINCE";}
			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCPR_PROVINCE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetPROVINCE_lister_RO</method-name><method-signature>
	public static IDataReader GetPROVINCE_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(202);//to do we have to Optimize it too.
sb_query.Append("SELECT CPR_PROVCD,CPR_DESCR,CCN_CTRYCD FROM LCPR_PROVINCE WHERE ((CCN_CTRYCD=SV(\"CCN_CTRYCD\"))  )  ORDER BY CPR_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LCPR_PROVINCE WHERE CCN_CTRYCD=? AND CPR_PROVCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, pkNameValue["CCN_CTRYCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@CPR_PROVCD",DbType.String, 3, pkNameValue["CPR_PROVCD"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCPR_PROVINCE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCN_CTRYCD,string CPR_PROVCD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCPR_PROVINCE WHERE CCN_CTRYCD=? AND CPR_PROVCD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, CCN_CTRYCD));
myCommand.Parameters.Add(DB.CreateParameter("@CPR_PROVCD",DbType.String, 3, CPR_PROVCD));

this.Holder.FillData(myCommand, "LCPR_PROVINCE");return this.Holder;
	//</method-body>
	}
	//</method>

	//<method><method-name>GetPROVINCE_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetPROVINCE_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(202);//to do we have to Optimize it too.
sb_query.Append("SELECT CPR_PROVCD,CPR_DESCR,CCN_CTRYCD FROM LCPR_PROVINCE WHERE  ({0} like '{1}')  AND ((CCN_CTRYCD=SV(\"CCN_CTRYCD\"))  )  ORDER BY CPR_DESCR   ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

}
}
