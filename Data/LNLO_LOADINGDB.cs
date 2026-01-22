using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LNLO_LOADINGDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNLO_LOADINGDB        (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNLO_LOADING";}
			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LNLO_LOADING";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
		//</method-signature><method-body>
		//String strQuery = "SELECT count(*) FROM LNLO_LOADING WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? AND NLO_TYPE=? AND NLO_SUBTYPE=? ";
		String strQuery = "SELECT count(*) FROM LNLO_LOADING WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? AND NLO_TYPE=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
		myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO",DbType.Decimal, 3, pkNameValue["NP2_SETNO"]));
		myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
		myCommand.Parameters.Add(DB.CreateParameter("@NLO_TYPE",DbType.String, 3, pkNameValue["NLO_TYPE"]));
		//myCommand.Parameters.Add(DB.CreateParameter("@NLO_SUBTYPE",DbType.String, 1, pkNameValue["NLO_SUBTYPE"]));
		int noOfRecords=(int)myCommand.ExecuteScalar();
		return(noOfRecords>=1);
		//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LNLO_LOADING";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
											//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string NP1_PROPOSAL,double NP2_SETNO,string PPR_PRODCD, string NLO_TYPE)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT * FROM LNLO_LOADING WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? AND NLO_TYPE=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
		myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO",   DbType.Decimal, 3, NP2_SETNO));
		myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",  DbType.String,  3, PPR_PRODCD));
		myCommand.Parameters.Add(DB.CreateParameter("@NLO_TYPE",    DbType.String,  3, NLO_TYPE));

		this.Holder.FillData(myCommand, "LNLO_LOADING");return this.Holder;
		//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_EQ_TARGETVALUES_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_EQ_TARGETVALUES_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(833);//to do we have to Optimize it too.
		//sb_query.Append("SELECT LN.NP1_PROPOSAL,LN.NP2_SETNO,LN.PPR_PRODCD,LN.NLO_TYPE,LN.NLO_SUBTYPE,LC.CAD_DESCRIPTION,LN.NLO_AMOUNT,LN2.NLO_AMOUNT NLO_AMOUNT_LN2,LN3.NLO_AMOUNT NLO_AMOUNT_LN3 FROM LNLO_LOADING LN,LNLO_LOADING LN2,LNLO_LOADING LN3,LCAD_ADJUSTMENTS LC,LCAD_ADJUSTMENTS LC2,LCAD_ADJUSTMENTS LC3  WHERE LN.NLO_TYPE=LC.CAD_TYPE AND LN.NLO_SUBTYPE='O' AND SUBSTR(LN.NLO_TYPE,1,1)='X' AND LN.NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND LN2.NLO_TYPE=LC2.CAD_TYPE AND LN2.NLO_SUBTYPE='O' AND SUBSTR(LN2.NLO_TYPE,1,1)='Y' AND LN2.NP1_PROPOSAL=LN.NP1_PROPOSAL AND LN3.NLO_TYPE=LC3.CAD_TYPE AND LN3.NLO_SUBTYPE='O' AND SUBSTR(LN3.NLO_TYPE,1,1)='Z' AND LN3.NP1_PROPOSAL=LN.NP1_PROPOSAL AND SUBSTR(LN2.NLO_TYPE,2,2)=SUBSTR(LN.NLO_TYPE,2,2) AND SUBSTR(LN3.NLO_TYPE,2,2)=SUBSTR(LN.NLO_TYPE,2,2)  ");
		sb_query.Append("SELECT LN.NP1_PROPOSAL,LN.NP2_SETNO,LN.PPR_PRODCD,LN.NLO_TYPE,LN.NLO_SUBTYPE,nvl(LCV.CVC_VALUE, LC.CAD_DESCRIPTION) CAD_DESCRIPTION,") ;
		sb_query.Append("ROUND(LN.NLO_AMOUNT) NLO_AMOUNT,ROUND(LN2.NLO_AMOUNT) NLO_AMOUNT_LN2,ROUND(LN3.NLO_AMOUNT) NLO_AMOUNT_LN3 ");
		sb_query.Append("FROM LNLO_LOADING LN, LNLO_LOADING LN2,LNLO_LOADING LN3,LCAD_ADJUSTMENTS LC,LCAD_ADJUSTMENTS LC2,LCAD_ADJUSTMENTS LC3, ");
		//
		sb_query.Append("(select LTRIM(RTRIM(TO_CHAR(ROWNUM,'00'))) ID, '@ '||TO_CHAR(CVC_VALUE*100,'999.09') ||' Percentage' CVC_VALUE ");
		sb_query.Append("from lcvc_valuecomb where ppr_prodcd=(SELECT PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND NPR_BASICFLAG='Y') ");
		sb_query.Append("and cmp_processid='007' and csp_processid='900' and ccn_columnid in ('FE198','FE199','FE200') ORDER BY CVC_VALUE) LCV ");
		//
		sb_query.Append("WHERE LN.NLO_TYPE=LC.CAD_TYPE AND LN.NLO_SUBTYPE='O' AND SUBSTR(LN.NLO_TYPE,1,1)='X' ");
		sb_query.Append("AND LN.NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ");
		sb_query.Append("AND LN2.NLO_TYPE=LC2.CAD_TYPE AND LN2.NLO_SUBTYPE='O' AND SUBSTR(LN2.NLO_TYPE,1,1)='Y' AND LN2.NP1_PROPOSAL=LN.NP1_PROPOSAL ");
		sb_query.Append("AND LN3.NLO_TYPE=LC3.CAD_TYPE AND LN3.NLO_SUBTYPE='O' AND SUBSTR(LN3.NLO_TYPE,1,1)='Z' AND LN3.NP1_PROPOSAL=LN.NP1_PROPOSAL ");
		sb_query.Append("AND SUBSTR(LN2.NLO_TYPE,2,2)=SUBSTR(LN.NLO_TYPE,2,2) AND SUBSTR(LN3.NLO_TYPE,2,2)=SUBSTR(LN.NLO_TYPE,2,2)  ");
		sb_query.Append("AND LCV.ID(+) =  SUBSTR(LN.NLO_TYPE,2,2) ");
		
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
				
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
			//</method-body>
	}
	//</method>

	//<method><method-name>GetMANUALADJUSTMENT_lister_RO</method-name><method-signature>
	public static IDataReader GetMANUALADJUSTMENT_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
		sb_query.Append("SELECT NP1_PROPOSAL, NP2_SETNO, PPR_PRODCD, NLO_TYPE, CAD_DESCRIPTION, NLO_SUBTYPE, NLO_VALUE, NLO_SAPRNTPR ");
		sb_query.Append(" FROM LNLO_LOADING LNLO, LCAD_ADJUSTMENTS LCAD WHERE LNLO.NLO_TYPE = LCAD.CAD_TYPE AND NP1_PROPOSAL=SV(\"PROPOSAL_ID\") AND PPR_PRODCD=SV(\"PLAN_ID\") AND NP2_SETNO=1 ORDER BY NLO_TYPE ");
		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>



	//<method><method-name>GetMANUALADJUSTMENT_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetMANUALADJUSTMENT_lister_filter_RO(string columnName,string columnValue)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(134);//to do we have to Optimize it too.
		sb_query.Append("SELECT NP1_PROPOSAL, NP2_SETNO, PPR_PRODCD, NLO_TYPE, CAD_DESCRIPTION, NLO_SUBTYPE, NLO_VALUE, NLO_SAPRNTPR ");
		sb_query.Append(" FROM LNLO_LOADING LNLO, LCAD_ADJUSTMENTS LCAD WHERE ({0} like '{1}') AND  LNLO.NLO_TYPE = LCAD.CAD_TYPE AND NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND PPR_PRODCD=SV(\"PPR_PRODCD\") AND NP2_SETNO=1 ORDER BY NLO_TYPE ");

		sb_query.Append("SELECT * FROM LNLO_LOADING WHERE  ({0} like '{1}') AND NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND PPR_PRODCD=SV(\"PPR_PRODCD\") AND NP2_SETNO=1 ORDER BY NLO_TYPE ");
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
