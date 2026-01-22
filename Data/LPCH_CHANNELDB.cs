using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LPCH_CHANNELDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LPCH_CHANNELDB       (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LPCH_CHANNEL";}
			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LPCH_CHANNEL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>

	//<method><method-name>GetILUS_ST_CHANNELSUBDTL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_CHANNELSUBDTL_lister_filter_RO(string columnName,string columnValue)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(236);//to do we have to Optimize it too.
		//sb_query.Append("SELECT PPR_PRODCD,CCH_CODE,CCD_CODE,CCS_CODE FROM LPCH_CHANNEL WHERE  ({0} like '{1}')  AND ((CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")  )  )   ");
		//sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,PPR_PRODCD FROM LPCH_CHANNEL WHERE  ({0} like '{1}')  AND (CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")  )   ");
		//sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,ch.PPR_PRODCD PPR_PRODCD, PPR_DESCR FROM LPCH_CHANNEL ch, LPPR_PRODUCT pr WHERE ({0} like '{1}')  AND (( ch.PPR_PRODCD=pr.PPR_PRODCD and CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\") ))   ");
		//sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,ch.PPR_PRODCD PPR_PRODCD, PPR_DESCR FROM LPCH_CHANNEL ch, LPPR_PRODUCT pr WHERE ({0} like '{1}')  AND (( ch.PPR_PRODCD=pr.PPR_PRODCD and CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\") ))   ");
		sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,PPR_PRODCD, (SELECT PPR_DESCR FROM LPPR_PRODUCT PR WHERE CH.PPR_PRODCD=PR.PPR_PRODCD) PPR_DESCR FROM LPCH_CHANNEL CH WHERE  ({0} like '{1}')  AND (CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")  )   ");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		query = string.Format(query, columnName, columnValue);
		query = string.Format(query, columnName, columnValue);
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>


	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO(string CCH_CODE,string CCD_CODE)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCS_CODE, CCS_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CCS_DESCR DESC_F  FROM LPCH_CHANNEL WHERE CCH_CODE='" + CCH_CODE + "' AND CCD_CODE='" + CCD_CODE + "'" );
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ST_CHANNELSUBDTL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ST_BRANCHPRODUCT_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(236);//to do we have to Optimize it too.
		//sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,PPR_PRODCD FROM LPCH_CHANNEL WHERE CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")    ");
		//sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,ch.PPR_PRODCD PPR_PRODCD, PPR_DESCR FROM LPCH_CHANNEL ch, LPPR_PRODUCT pr WHERE (( ch.PPR_PRODCD=pr.PPR_PRODCD and CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")   ))");
		sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,PPR_PRODCD, (SELECT PPR_DESCR FROM LPPR_PRODUCT PR WHERE CH.PPR_PRODCD=PR.PPR_PRODCD) PPR_DESCR FROM LPCH_CHANNEL CH WHERE  (CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\")  )");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>


	//OK
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT count(*) FROM LPCH_CHANNEL WHERE PPR_PRODCD=? AND CCH_CODE=? AND CCD_CODE=? AND CCS_CODE=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
		myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 3, pkNameValue["CCH_CODE"]));
		myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, pkNameValue["CCD_CODE"]));
		myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE",DbType.String, 5, pkNameValue["CCS_CODE"]));
		int noOfRecords=(int)myCommand.ExecuteScalar();
		return(noOfRecords>=1);
		//</method-body>
	}
	//</method>


	//OK
	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
		//</method-signature><method-body>
		const String strQuery = "SELECT * FROM LPCH_CHANNEL";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//OK
	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string PPR_PRODCD, string CCH_CODE, string CCD_CODE,string CCS_CODE)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT * FROM LPCH_CHANNEL WHERE PPR_PRODCD=? AND CCH_CODE=? AND CCD_CODE=? AND CCS_CODE=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, PPR_PRODCD));
		myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 3, CCH_CODE));
		myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, CCD_CODE));
		myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE",DbType.String, 5, CCS_CODE));

		this.Holder.FillData(myCommand, "LPCH_CHANNEL");return this.Holder;
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
	/*public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCS_CODE_RO(string CCH_CODE, string CCD_CODE)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(173);//to do we have to Optimize it too.
		sb_query.Append("SELECT CCS_CODE, CCS_CODE");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CCS_DESCR DESC_F  FROM CCS_CHANLSUBDETL WHERE CCH_CODE='" + CCH_CODE + "' AND CCD_CODE='" + CCD_CODE + "'" );
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	*/
	//</method>




	//<method><method-name>GetILUS_ST_BRANCHPRODUCT_Data</method-name><method-signature>
	public DataHolder GetILUS_ST_BRANCHPRODUCT_Data()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(263);//to do we have to Optimize it too.
		sb_query.Append("SELECT PPR_PRODCD,CCH_CODE,CCD_CODE,CCS_CODE FROM LPCH_CHANNEL WHERE ((CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\"))  )   ");
		String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LPCH_CHANNEL");
		return this.Holder;
		//</method-body>
	}
	//</method>

}
}
