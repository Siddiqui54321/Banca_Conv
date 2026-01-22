using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LNPU_PURPOSEDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNPU_PURPOSEDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNPU_PURPOSE";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LNPU_PURPOSE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LNPU_PURPOSE WHERE NP1_PROPOSAL=? AND CPU_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
myCommand.Parameters.Add(DB.CreateParameter("@CPU_CODE",DbType.String, 3, pkNameValue["CPU_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_PURPOSE_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_PURPOSE_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(148);//to do we have to Optimize it too.
sb_query.Append("SELECT NP1_PROPOSAL,CPU_CODE FROM LNPU_PURPOSE  ");
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
const String strQuery = "SELECT * FROM LNPU_PURPOSE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CPU_CODE,string NP1_PROPOSAL)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LNPU_PURPOSE WHERE NP1_PROPOSAL=? AND CPU_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
myCommand.Parameters.Add(DB.CreateParameter("@CPU_CODE",DbType.String, 3, CPU_CODE));

this.Holder.FillData(myCommand, "LNPU_PURPOSE");return this.Holder;
	//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_PURPOSE_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_PURPOSE_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(148);//to do we have to Optimize it too.
sb_query.Append("SELECT NP1_PROPOSAL,CPU_CODE FROM LNPU_PURPOSE   WHERE  ({0} like '{1}') ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_PURPOSE_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_NM_PURPOSE_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(161);//to do we have to Optimize it too.
sb_query.Append("SELECT NP1_PROPOSAL,CPU_CODE,NPU_SELECTED FROM LNPU_PURPOSE  ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNPU_PURPOSE");
		return this.Holder;
			//</method-body>
	}
	//</method>

}
}
