using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LPVF_VALIDFIELDSDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LPVF_VALIDFIELDSDB    (DataHolder dh):base(dh)
		
	
		
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LPVF_VALIDFIELDS";}
			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LPVF_VALIDFIELDS";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetLPVF_VALIDFIELDS_VALIDATION_lister_RO</method-name><method-signature>
	public static IDataReader GetLPVF_VALIDFIELDS_VALIDATION_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(212);//to do we have to Optimize it too.
sb_query.Append("SELECT PPR_PRODCD,B.VFS_DESC,A.PVF_CODE FROM LPVF_VALIDFIELDS A,LVFS_FIELDSETUP B  WHERE A.PVF_CODE=B.VFS_CODE AND B.VFS_SOURCE NOT IN ('N') AND A.PPR_PRODCD=SV(\"PPR_PRODCD_S\") ORDER BY A.PVF_CODE  ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVF_VALIDFIELDS_DECISION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLPVF_VALIDFIELDS_DECISION_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(177);//to do we have to Optimize it too.
		//	sb_query.Append("SELECT PPR_PRODCD,PVF_CODE FROM LPVF_VALIDFIELDS  WHERE  ({0} like '{1}')  AND PVF_CODE='DECISION'  ");
        sb_query.Append("SELECT v.PPR_PRODCD,p.ppr_descr,v.PVF_CODE FROM LPVF_VALIDFIELDS v\n"+
                            "inner join LPPR_PRODUCT p on v.ppr_prodcd=p.ppr_prodcd\n"+
                           " and p.PPR_BASRIDR = 'B'  \n"+
                           " and p.PPR_PRODCD <> '999' WHERE  (v.{0} like '{1}')  AND v.PVF_CODE='DECISION'  ");
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
String strQuery = "SELECT count(*) FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD=? AND PVF_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@PVF_CODE",DbType.String, 50, pkNameValue["PVF_CODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVF_VALIDFIELDS_VALIDATION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLPVF_VALIDFIELDS_VALIDATION_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(212);//to do we have to Optimize it too.
sb_query.Append("SELECT PPR_PRODCD,B.VFS_DESC,A.PVF_CODE FROM LPVF_VALIDFIELDS A,LVFS_FIELDSETUP B  WHERE  ({0} like '{1}')  AND A.PVF_CODE=B.VFS_CODE AND B.VFS_SOURCE NOT IN ('N') AND A.PPR_PRODCD=SV(\"PPR_PRODCD_S\") ORDER BY A.PVF_CODE   ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
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
const String strQuery = "SELECT * FROM LPVF_VALIDFIELDS";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVF_VALIDFIELDS_DECISION_lister_RO</method-name><method-signature>
	public static IDataReader GetLPVF_VALIDFIELDS_DECISION_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(177);//to do we have to Optimize it too.
			//sb_query.Append("SELECT PPR_PRODCD,PVF_CODE FROM LPVF_VALIDFIELDS  WHERE PVF_CODE='DECISION'  ");
			//sb_query.Append("SELECT VF.PPR_PRODCD,P.PPR_DESCR,S.VFS_DESC,VF.PVF_FIELDCOMB,VF.PVF_CODE FROM LPVF_VALIDFIELDS VF ");
			sb_query.Append("SELECT VF.PPR_PRODCD,P.PPR_DESCR,VF.PVF_FIELDCOMB,VF.PVF_CODE FROM LPVF_VALIDFIELDS VF ");
			sb_query.Append("	INNER JOIN LPPR_PRODUCT P ON ");
			sb_query.Append("		VF.PPR_PRODCD = P.PPR_PRODCD ");
			//sb_query.Append("	INNER JOIN LVFS_FIELDSETUP S ON ");
			//sb_query.Append("		VF.PVF_CODE = S.VFS_CODE ");
			sb_query.Append(" WHERE PVF_CODE='DECISION'  order by VF.PPR_PRODCD");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string PPR_PRODCD,string PVF_CODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD=? AND PVF_CODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, PPR_PRODCD));
myCommand.Parameters.Add(DB.CreateParameter("@PVF_CODE",DbType.String, 50, PVF_CODE));

this.Holder.FillData(myCommand, "LPVF_VALIDFIELDS");return this.Holder;
	//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVF_PPRID_DESCRIPTION_lister_RO</method-name><method-signature>
	public static IDataReader GetLPVF_PPRID_DESCRIPTION_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(268);//to do we have to Optimize it too.
		sb_query.Append("SELECT LPVF.PPR_PRODCD,LPPR.PPR_DESCR,PVF_CODE FROM LPVF_VALIDFIELDS LPVF, LPPR_PRODUCT LPPR  WHERE PVF_CODE = SV(\"VFS_CODE\")  AND LPVF.PPR_PRODCD = LPPR.PPR_PRODCD  ");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetLPVF_PPRID_DESCRIPTION_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetLPVF_PPRID_DESCRIPTION_lister_filter_RO(string columnName,string columnValue)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(268);//to do we have to Optimize it too.
		sb_query.Append("SELECT LPVF.PPR_PRODCD,LPPR.PPR_DESCR,PVF_CODE FROM LPVF_VALIDFIELDS LPVF, LPPR_PRODUCT LPPR  WHERE  ({0} like '{1}')  AND PVF_CODE = SV(\"VFS_CODE\")  AND LPVF.PPR_PRODCD = LPPR.PPR_PRODCD  ");
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
