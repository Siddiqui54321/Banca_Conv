using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LPVD_VALIDATIONDETAILDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LPVD_VALIDATIONDETAILDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LPVD_VALIDATIONDETAIL";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LPVD_VALIDATIONDETAIL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetLPVD_VALIDATIONDETAIL_DECISION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLPVD_VALIDATIONDETAIL_DECISION_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(402);//to do we have to Optimize it too.
sb_query.Append("SELECT PVD_SEQUENCE,PVD_VALIDATIONFOR,PVD_RELOPERATOR,PVD_VALIDFROM,PVD_VALIDTO,PPR_PRODCD,PVL_VALIDATIONFOR,PVL_LEVEL,PVD_LEVEL FROM LPVD_VALIDATIONDETAIL WHERE  ({0} like '{1}')  AND ((PPR_PRODCD=SV(\"PPR_PRODCD\") AND PVL_VALIDATIONFOR=SV(\"PVL_VALIDATIONFOR\") AND PVL_LEVEL=SVN(\"PVL_LEVEL\"))  )  ORDER BY PVD_SEQUENCE ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVD_VALIDATIONDETAIL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLPVD_VALIDATIONDETAIL_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(381);//to do we have to Optimize it too.
sb_query.Append("SELECT PVD_SEQUENCE,PVD_VALIDATIONFOR,PVD_RELOPERATOR,PVD_VALIDFROM,PVD_VALIDTO,PPR_PRODCD,PVL_VALIDATIONFOR,PVL_LEVEL,PVD_LEVEL FROM LPVD_VALIDATIONDETAIL WHERE  ({0} like '{1}')  AND ((PPR_PRODCD=SV(\"PPR_PRODCD_S\") AND PVL_VALIDATIONFOR=SV(\"PVL_VALIDATIONFOR\") AND PVL_LEVEL=SVN(\"PVL_LEVEL\"))  )   ");
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
String strQuery = "SELECT count(*) FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD=? AND PVL_VALIDATIONFOR=? AND PVL_LEVEL=? AND PVD_LEVEL=? AND PVD_VALIDATIONFOR=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@PVL_VALIDATIONFOR",DbType.String, 50, pkNameValue["PVL_VALIDATIONFOR"]));
myCommand.Parameters.Add(DB.CreateParameter("@PVL_LEVEL",DbType.Decimal, 5, pkNameValue["PVL_LEVEL"]));
myCommand.Parameters.Add(DB.CreateParameter("@PVD_LEVEL",DbType.Decimal, 5, pkNameValue["PVD_LEVEL"]));
myCommand.Parameters.Add(DB.CreateParameter("@PVD_VALIDATIONFOR",DbType.String, 50, pkNameValue["PVD_VALIDATIONFOR"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVD_VALIDATIONDETAIL_lister_RO</method-name><method-signature>
	public static IDataReader GetLPVD_VALIDATIONDETAIL_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(381);//to do we have to Optimize it too.
sb_query.Append("SELECT PVD_SEQUENCE,PVD_VALIDATIONFOR,PVD_RELOPERATOR,PVD_VALIDFROM,PVD_VALIDTO,PPR_PRODCD,PVL_VALIDATIONFOR,PVL_LEVEL,PVD_LEVEL FROM LPVD_VALIDATIONDETAIL WHERE ((PPR_PRODCD=SV(\"PPR_PRODCD_S\") AND PVL_VALIDATIONFOR=SV(\"PVL_VALIDATIONFOR\") AND PVL_LEVEL=SVN(\"PVL_LEVEL\"))  )   ");
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
const String strQuery = "SELECT * FROM LPVD_VALIDATIONDETAIL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string PPR_PRODCD,double PVD_LEVEL,string PVD_VALIDATIONFOR,double PVL_LEVEL,string PVL_VALIDATIONFOR)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD=? AND PVL_VALIDATIONFOR=? AND PVL_LEVEL=? AND PVD_LEVEL=? AND PVD_VALIDATIONFOR=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, PPR_PRODCD));
myCommand.Parameters.Add(DB.CreateParameter("@PVL_VALIDATIONFOR",DbType.String, 50, PVL_VALIDATIONFOR));
myCommand.Parameters.Add(DB.CreateParameter("@PVL_LEVEL",DbType.Decimal, 5, PVL_LEVEL));
myCommand.Parameters.Add(DB.CreateParameter("@PVD_LEVEL",DbType.Decimal, 5, PVD_LEVEL));
myCommand.Parameters.Add(DB.CreateParameter("@PVD_VALIDATIONFOR",DbType.String, 50, PVD_VALIDATIONFOR));

this.Holder.FillData(myCommand, "LPVD_VALIDATIONDETAIL");return this.Holder;
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVD_VALIDATIONDETAIL_DECISION_lister_RO</method-name><method-signature>
	public static IDataReader GetLPVD_VALIDATIONDETAIL_DECISION_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(402);//to do we have to Optimize it too.
sb_query.Append("SELECT PVD_SEQUENCE,PVD_VALIDATIONFOR,PVD_RELOPERATOR,PVD_VALIDFROM,PVD_VALIDTO,PPR_PRODCD,PVL_VALIDATIONFOR,PVL_LEVEL,PVD_LEVEL FROM LPVD_VALIDATIONDETAIL WHERE ((PPR_PRODCD=SV(\"PPR_PRODCD\") AND PVL_VALIDATIONFOR=SV(\"PVL_VALIDATIONFOR\") AND PVL_LEVEL=SVN(\"PVL_LEVEL\"))  )  ORDER BY PVD_SEQUENCE ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

}
}
