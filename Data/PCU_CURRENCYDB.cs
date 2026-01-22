using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class PCU_CURRENCYDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public PCU_CURRENCYDB         (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "PCU_CURRENCY";}
			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM PCU_CURRENCY";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_NM_PCU_CURRENCY_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_PCU_CURRENCY_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(139);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE FROM PCU_CURRENCY   WHERE  ({0} like '{1}') ");
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
	public DataHolder FindByPK(string PCU_CURRCODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM PCU_CURRENCY WHERE PCU_CURRCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PCU_CURRCODE",DbType.String, 3, PCU_CURRCODE));

this.Holder.FillData(myCommand, "PCU_CURRENCY");return this.Holder;
									//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_PCU_CURRENCY_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_PCU_CURRENCY_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(139);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE FROM PCU_CURRENCY  ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
											//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PCU_CURRDESC DESC_F  FROM PCU_CURRENCY  ORDER BY PCU_CURRCODE");
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
const String strQuery = "SELECT * FROM PCU_CURRENCY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
												//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_TARGETVALUES_PCU_AVCURRCODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_TARGETVALUES_PCU_AVCURRCODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PCU_CURRDESC DESC_F  FROM PCU_CURRENCY  ORDER BY PCU_CURRDESC");
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
String strQuery = "SELECT count(*) FROM PCU_CURRENCY WHERE PCU_CURRCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PCU_CURRCODE",DbType.String, 3, pkNameValue["PCU_CURRCODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
									//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_TE_ER_EXCHANGERATE_PCU_CURRCODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_TE_ER_EXCHANGERATE_PCU_CURRCODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(179);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE,PCU_CURRCODE");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("'-'");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PCU_CURRDESC DESC_F  FROM PCU_CURRENCY  ");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
	//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PCU_CURRDESC DESC_F  FROM PCU_CURRENCY  ORDER BY PCU_CURRDESC");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_AVCURRCODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_AVCURRCODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_CURRCODE,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PCU_CURRDESC DESC_F  FROM PCU_CURRENCY  ORDER BY PCU_CURRDESC");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

}
}
