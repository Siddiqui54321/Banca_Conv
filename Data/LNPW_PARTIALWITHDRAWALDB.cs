using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LNPW_PARTIALWITHDRAWALDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNPW_PARTIALWITHDRAWALDB    (DataHolder dh):base(dh)
		
	
	
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNPW_PARTIALWITHDRAWAL";}
			//			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LNPW_PARTIALWITHDRAWAL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LNPW_PARTIALWITHDRAWAL WHERE NPW_YEAR=? AND NP1_PROPOSAL=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NPW_YEAR",DbType.Decimal, 2, pkNameValue["NPW_YEAR"]));
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>


	//<method><method-name>GetILUS_ET_TB_VERTICALWITHDRAWAL_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_TB_VERTICALWITHDRAWAL_ALL_Data()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(347);//to do we have to Optimize it too.
		sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_PURPOSE,NPW_REQUIREDFOR,NPW_REQIREDFORCD,NPW_ALLOWAMOUNT,NPW_CUMWITHDRAWAL,NPW_ADHOCEXCESPRM,NPW_DEATHBENEFITOPTION,NP1_PROPOSAL, NPW_ADHOCEPPW FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NPW_YEAR ");  
		String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNPW_PARTIALWITHDRAWAL");
		return this.Holder;
		//</method-body>
	}
	//</method>
	
	
	//<method><method-name>GetILUS_ET_TB_VERTICALWITHDRAWAL_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_TB_VERTICALWITHDRAWAL_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(347);//to do we have to Optimize it too.
sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_PURPOSE,NPW_REQUIREDFOR,NPW_REQIREDFORCD,NPW_ALLOWAMOUNT,NPW_CUMWITHDRAWAL,NPW_ADHOCEXCESPRM,NPW_DEATHBENEFITOPTION,NP1_PROPOSAL, NPW_ADHOCEPPW FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and (NPW_PW is not null or NPW_ALLOWAMOUNT is not null or NPW_CUMWITHDRAWAL is not null) ORDER BY NPW_YEAR ");  
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNPW_PARTIALWITHDRAWAL");
		return this.Holder;
			//</method-body>
	}
	//</method>

	public DataHolder GetILUS_ET_TB_VERTICALWITHDRAWAL_ADHOC_Data()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(347);//to do we have to Optimize it too.
		sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_PURPOSE,NPW_REQUIREDFOR,NPW_REQIREDFORCD,NPW_ALLOWAMOUNT,NPW_CUMWITHDRAWAL,NPW_ADHOCEXCESPRM,NPW_DEATHBENEFITOPTION,NP1_PROPOSAL, NPW_ADHOCEPPW FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and (NPW_ADHOCEXCESPRM is not null or NPW_ADHOCEPPW is not null) ORDER BY NPW_YEAR ");
		String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNPW_PARTIALWITHDRAWAL_ADHOC");
		return this.Holder;
		//</method-body>
	}
	//</method>


	public DataHolder GetILUS_ET_TB_VERTICALWITHDRAWAL_SWITCH_Data()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(347);//to do we have to Optimize it too.
		sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_PURPOSE,NPW_REQUIREDFOR,NPW_REQIREDFORCD,NPW_ALLOWAMOUNT,NPW_CUMWITHDRAWAL,NPW_ADHOCEXCESPRM,NPW_DEATHBENEFITOPTION,NP1_PROPOSAL, NPW_ADHOCEPPW FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and (NPW_DEATHBENEFITOPTION is not null) ORDER BY NPW_YEAR ");
		String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNPW_PARTIALWITHDRAWAL_SWITCH");
		return this.Holder;
		//</method-body>
	}
	//</method>


	//<method><method-name>GetILUS_ET_NM_WITHDRAWAL_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_WITHDRAWAL_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
string subView = "select NBF_BENEFCD, NBF_BENNAME, 'B' npw_requiredfor, np1_proposal from lnbf_beneficiary "
				+ " UNION "
				+ " SELECT ph.nph_code, ph.nph_fullname, 'I' code,  u1.np1_proposal "
				+ " FROM LNPH_PHOLDER ph, lnu1_underwriti u1 "
				+ " WHERE "
				+ " ph.nph_code = u1.nph_code "
				+ " and ph.nph_life = u1.nph_life ";

			StringBuilder sb_query=new StringBuilder(303);//to do we have to Optimize it too.
			//sb_query.Append("SELECT NPW_YEAR,NPW_PW,BF.NBF_BENNAME,NP1_PROPOSAL FROM LNPW_PARTIALWITHDRAWAL PW,  LNBF_BENEFICIARY BF  WHERE  ({0} like '{1}')  AND PW.NPW_REQIREDFORCD=BF.NBF_BENEFCD AND PW.NP1_PROPOSAL='R/07/0010042'  ORDER BY PW.NPW_YEAR ");
			sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_CUMWITHDRAWAL,BF.NBF_BENNAME,NP1_PROPOSAL FROM LNPW_PARTIALWITHDRAWAL PW,  (" + subView + ") BF    WHERE  ({0} like '{1}')  AND PW.NPW_REQIREDFORCD=BF.NBF_BENEFCD AND PW.NPW_REQUIREDFOR=BF.npw_requiredfor AND PW.NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY PW.NPW_YEAR ");
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
const String strQuery = "SELECT * FROM LNPW_PARTIALWITHDRAWAL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_WITHDRAWAL_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_WITHDRAWAL_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
string subView = "select NBF_BENEFCD, NBF_BENNAME, 'B' npw_requiredfor, np1_proposal from lnbf_beneficiary "
				+ " where NP1_PROPOSAL=SV(\"NP1_PROPOSAL\")" 
				+ " UNION "
				+ " SELECT ph.nph_code, ph.nph_fullname, 'I' code,  u1.np1_proposal "
				+ " FROM LNPH_PHOLDER ph, lnu1_underwriti u1 "
				+ " WHERE "
				+ " ph.nph_code = u1.nph_code "
				+ " and ph.nph_life = u1.nph_life "
				+ " and NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ";


			StringBuilder sb_query=new StringBuilder(303);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPW_YEAR,NPW_PW,NPW_CUMWITHDRAWAL,BF.NBF_BENNAME,PW.NP1_PROPOSAL FROM LNPW_PARTIALWITHDRAWAL PW,  (" + subView + ") BF  WHERE PW.NPW_REQIREDFORCD=BF.NBF_BENEFCD AND PW.NPW_REQUIREDFOR=BF.npw_requiredfor AND PW.NP1_PROPOSAL=SV(\"NP1_PROPOSAL\")  ORDER BY PW.NPW_YEAR ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string NP1_PROPOSAL,double NPW_YEAR)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LNPW_PARTIALWITHDRAWAL WHERE NPW_YEAR=? AND NP1_PROPOSAL=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NPW_YEAR",DbType.Decimal, 2, NPW_YEAR));
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));

this.Holder.FillData(myCommand, "LNPW_PARTIALWITHDRAWAL");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
