using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCMO_MODEDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCMO_MODEDB    (DataHolder dh):base(dh)
		
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCMO_MODE";}
			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCMO_MODE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_NM_LCMO_MODE_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_LCMO_MODE_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(132);//to do we have to Optimize it too.
sb_query.Append("SELECT CMO_MODE FROM LCMO_MODE  ");
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
String strQuery = "SELECT count(*) FROM LCMO_MODE WHERE CMO_MODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CMO_MODE",DbType.String, 1, pkNameValue["CMO_MODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
				//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(195);//to do we have to Optimize it too.
sb_query.Append("SELECT CMO_MODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("CMO_DESCRIPTION DESC_F  FROM LCMO_MODE  ORDER BY CMO_DESCRIPTION");
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
const String strQuery = "SELECT * FROM LCMO_MODE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
							//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_LCMO_MODE_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_LCMO_MODE_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(132);//to do we have to Optimize it too.
sb_query.Append("SELECT CMO_MODE FROM LCMO_MODE   WHERE  ({0} like '{1}') ");
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
	public DataHolder FindByPK(string CMO_MODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCMO_MODE WHERE CMO_MODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CMO_MODE",DbType.String, 1, CMO_MODE));

this.Holder.FillData(myCommand, "LCMO_MODE");return this.Holder;
				//</method-body>
	}
	//</method>

}
}
