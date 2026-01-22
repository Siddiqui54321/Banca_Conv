using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LVFS_FIELDSETUPDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LVFS_FIELDSETUPDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LVFS_FIELDSETUP";}
			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LVFS_FIELDSETUP";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LVFS_FIELDSETUP";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_LPVF_VALIDFIELDS_VALIDATION_PVF_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_LPVF_VALIDFIELDS_VALIDATION_PVF_CODE_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(199);//to do we have to Optimize it too.
sb_query.Append("SELECT VFS_CODE,VFS_CODE");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("'-'");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("VFS_DESC DESC_F  FROM LVFS_FIELDSETUP  WHERE VFS_SOURCE IN ('P','Q')");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
	//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_LPVF_VALIDFIELDS_DECISION_PVF_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_LPVF_VALIDFIELDS_DECISION_PVF_CODE_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(189);//to do we have to Optimize it too.
		sb_query.Append("SELECT VFS_CODE,VFS_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("VFS_DESC DESC_F  FROM LVFS_FIELDSETUP  WHERE VFS_SOURCE IN ('P','Q')");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_RIDER_BEHAVIOUR_VFS_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_RIDER_BEHAVIOUR_VFS_CODE_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(203);//to do we have to Optimize it too.
		sb_query.Append("SELECT VFS_CODE,VFS_DESC DESC_F  FROM LVFS_FIELDSETUP  WHERE VFS_CODE IN ('MANDRIDER','FORBIDDENRIDER')");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

}
}
