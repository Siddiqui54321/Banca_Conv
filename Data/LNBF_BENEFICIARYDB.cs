using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class LNBF_BENEFICIARYDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNBF_BENEFICIARYDB      (DataHolder dh):base(dh)
		
	
	
	
		
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNBF_BENEFICIARY";}
			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_TB_BENEFECIARY_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_TB_BENEFECIARY_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(308);//to do we have to Optimize it too.
			sb_query.Append("SELECT NP1_PROPOSAL,NBF_BENEFCD,NGU_GUARDCD,NBF_BENNAME,NBF_BENNAMEARABIC,NBF_DOB,NBF_AGE,CRL_RELEATIOCD,NBF_BASIS,NBF_AMOUNT,NBF_PERCNTAGE FROM LNBF_BENEFICIARY  WHERE NP1_PROPOSAL =SV(\"NP1_PROPOSAL\")  ORDER BY NBF_BENNAME ");
			//sb_query.Append("SELECT bf.NP1_PROPOSAL,NBF_BENEFCD,bf.NGU_GUARDCD NGU_GUARDCD,NGU_NAME,NBF_BENNAME,NBF_BENNAMEARABIC,NBF_DOB,NBF_AGE,bf.CRL_RELEATIOCD,NBF_BASIS,NBF_AMOUNT,NBF_PERCNTAGE FROM LNBF_BENEFICIARY bf LEFT OUTER JOIN LNGU_GUARDIAN gu ON bf.NGU_GUARDCD=gu.NGU_GUARDCD WHERE bf.NP1_PROPOSAL =SV(\"NP1_PROPOSAL\")  ORDER BY NBF_BENNAME ");
			String query = EnvHelper.Parse(sb_query.ToString());
			IDbCommand myCommand = DB.CreateCommand(query);
			this.Holder.FillData(myCommand, "LNBF_BENEFICIARY");
			return this.Holder;
					//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL=? AND NBF_BENEFCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
			myCommand.Parameters.Add(DB.CreateParameter("@NBF_BENEFCD",DbType.String, 6, pkNameValue["NBF_BENEFCD"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
					//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_BENEFECIARY_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_BENEFECIARY_lister_filter_RO(string columnName,string columnValue)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(301);//to do we have to Optimize it too.
		//sb_query.Append("SELECT NBF_BENNAME,NBF_DOB,NBF_AGE,A.CRL_RELEATIOCD,B.CRL_DESCR,NBF_BENEFCD,NP1_PROPOSAL FROM LNBF_BENEFICIARY A,LCRL_RELATION B WHERE  ({0} like '{1}')  AND ((NP1_PROPOSAL=SVN(\"NP1_PROPOSAL\")) )  AND  A.CRL_RELEATIOCD = B.CRL_RELEATIOCD  ");
		sb_query.Append("SELECT NBF_BENNAME,NBF_DOB,NBF_AGE,A.CRL_RELEATIOCD,B.CRL_DESCR,NBF_BENEFCD,NP1_PROPOSAL,NGU_GUARDCD ");
		//sb_query.Append(",CASE WHEN NBF_BASIS = '01' THEN CAST(NBF_AMOUNT AS VARCHAR2(10)) else CAST(NBF_PERCNTAGE AS VARCHAR(3)) || ' %'  end BASIS ");
		sb_query.Append(",CASE WHEN NBF_BASIS = '01' THEN RTRIM(LTRIM(TO_CHAR(CAST(NBF_AMOUNT AS VARCHAR2(10)),'9,999,999.99'))) else CAST(NBF_PERCNTAGE AS VARCHAR(3)) || ' %'  end BASIS, RTRIM(LTRIM(TO_CHAR(CAST(NBF_AGE AS VARCHAR2(3)),'999'))) AGE ");
		sb_query.Append("FROM LNBF_BENEFICIARY A,LCRL_RELATION B WHERE  ({0} like '{1}')  AND ((NP1_PROPOSAL=SVN(\"NP1_PROPOSAL\")) )  AND  A.CRL_RELEATIOCD = B.CRL_RELEATIOCD   ");
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
const String strQuery = "SELECT * FROM LNBF_BENEFICIARY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQIREDFORCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQIREDFORCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(234);//to do we have to Optimize it too.
			sb_query.Append("SELECT NBF_BENEFCD,");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//			sb_query.Append("'-'");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("NBF_BENNAME DESC_F  FROM LNBF_BENEFICIARY  WHERE NP1_PROPOSAL='R/07/0010042' ORDER BY NBF_BENNAME");
			sb_query.Append("NBF_BENNAME DESC_F  FROM VW_REQUIREDFOR WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NBF_BENNAME");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string NBF_BENEFCD,string NP1_PROPOSAL)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL=? AND NBF_BENEFCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			myCommand.Parameters.Add(DB.CreateParameter("@NBF_BENEFCD",DbType.String, 6, NBF_BENEFCD));

			this.Holder.FillData(myCommand, "LNBF_BENEFICIARY");return this.Holder;
					//</method-body>
	}
	//</method>

	/*
	public static void ddd(string NBF_BENEFCD,string NP1_PROPOSAL)
	{
		String strQuery = "SELECT * FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL=? AND NBF_BENEFCD=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 0, NP1_PROPOSAL));
		myCommand.Parameters.Add(DB.CreateParameter("@NBF_BENEFCD",DbType.String, 0, NBF_BENEFCD));

		this.Holder.FillData(myCommand, "LNBF_BENEFICIARY");return this.Holder;
	}
	*/

	//<method><method-name>GetILUS_BENEFECIARY_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_BENEFECIARY_lister_RO(int offset,int numRows)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(313);//to do we have to Optimize it too.
		//sb_query.Append("SELECT A.NBF_BENNAME,A.NBF_DOB,NBF_AGE,A.CRL_RELEATIOCD,B.CRL_DESCR,A.NBF_BENEFCD,A.NP1_PROPOSAL FROM LNBF_BENEFICIARY A,LCRL_RELATION B WHERE A.CRL_RELEATIOCD = B.CRL_RELEATIOCD AND ((A.NP1_PROPOSAL=SVN(\"NP1_PROPOSAL\"))  )   ");
		sb_query.Append("SELECT NBF_BENNAME,NBF_DOB,NBF_AGE,A.CRL_RELEATIOCD,B.CRL_DESCR,NBF_BENEFCD,NP1_PROPOSAL,NGU_GUARDCD,NBF_IDNO ");
		//sb_query.Append(",CASE WHEN NBF_BASIS = '01' THEN CAST(NBF_AMOUNT AS VARCHAR2(10)) else CAST(NBF_PERCNTAGE AS VARCHAR(3)) || ' %'  end BASIS ");
		sb_query.Append(",CASE WHEN NBF_BASIS = '01' THEN RTRIM(LTRIM(TO_CHAR(CAST(NBF_AMOUNT AS VARCHAR2(10)),'9,999,999.99'))) else CAST(NBF_PERCNTAGE AS VARCHAR(3)) || ' %'  end BASIS, RTRIM(LTRIM(TO_CHAR(CAST(NBF_AGE AS VARCHAR2(3)),'999'))) AGE ");
		sb_query.Append("FROM LNBF_BENEFICIARY A,LCRL_RELATION B WHERE  NP1_PROPOSAL=SVN(\"NP1_PROPOSAL\")  AND  A.CRL_RELEATIOCD = B.CRL_RELEATIOCD   ");

		string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>


}
}
