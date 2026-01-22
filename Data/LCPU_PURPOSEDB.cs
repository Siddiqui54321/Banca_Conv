using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class LCPU_PURPOSEDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCPU_PURPOSEDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCPU_PURPOSE";}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetDDL_ILUS_ET_TB_WITHDRAWAL_NTP_COL2_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_TB_WITHDRAWAL_NTP_COL2_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(168);//to do we have to Optimize it too.
sb_query.Append("SELECT CPU_CODE,CPU_CODE");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("'-'");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("CPU_DESCR DESC_F  FROM LCPU_PURPOSE  ");
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
String strQuery = "SELECT count(*) FROM LCPU_PURPOSE WHERE CPU_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CPU_CODE",DbType.String, 3, pkNameValue["CPU_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
			//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCPU_PURPOSE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_PURPOSE_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_NM_PURPOSE_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(145);//to do we have to Optimize it too.
sb_query.Append("SELECT CPU_CODE,CPU_DESCR FROM LCPU_PURPOSE  ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LCPU_PURPOSE");
		return this.Holder;
					//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CPU_CODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCPU_PURPOSE WHERE CPU_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CPU_CODE",DbType.String, 3, CPU_CODE));

this.Holder.FillData(myCommand, "LCPU_PURPOSE");return this.Holder;
			//</method-body>
	}
	//</method>

}
}
