using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LNPR_PRODUCTDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LNPR_PRODUCTDB       (DataHolder dh):base(dh)
		
	
	
	
	
	
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LNPR_PRODUCT";}
			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS.aspx";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetILUS_ET_NM_PLANDETAILS_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PLANDETAILS_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(230);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,NP1_PROPOSAL,NP2_SETNO FROM LNPR_PRODUCT  WHERE  ({0} like '{1}')  AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(NPR_BASICFLAG,'N') = 'Y'  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_TARGETVALUES_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_TARGETVALUES_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(230);//to do we have to Optimize it too.
			sb_query.Append("SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(NPR_BASICFLAG,'N') = 'Y'  ");
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
			String strQuery = "SELECT count(*) FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
			myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO",DbType.Decimal, 3, pkNameValue["NP2_SETNO"]));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_PLANDETAILS_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PLANDETAILS_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(230);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,NP1_PROPOSAL,NP2_SETNO FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(NPR_BASICFLAG,'N') = 'Y'  ");
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
			const String strQuery = "SELECT * FROM LNPR_PRODUCT";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_TB_PLANDETAILS_Data</method-name><method-signature>
		public DataHolder GetILUS_ET_TB_PLANDETAILS_Data()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(316);//to do we have to Optimize it too.
			//sb_query.Append("SELECT NP1_PROPOSAL, NP2_SETNO, LNPR.PPR_PRODCD PPR_PRODCD, NPR_SELECTED, CCB_CODE, NPR_TOTPREM, NPR_SUMASSURED, NVL(LNPR.NPR_BENEFITTERM,0) NPR_BENEFITTERM, NPR_PREMIUM, NPR_COMMLOADING, NPR_BASICFLAG FROM LNPR_PRODUCT LNPR, LPPR_PRODUCT LPPR WHERE LNPR.PPR_PRODCD=LPPR.PPR_PRODCD AND LNPR.NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(LNPR.NPR_BASICFLAG,'Y') = 'N' ");
			sb_query.Append("SELECT NP1_PROPOSAL, NP2_SETNO, PPR_PRODCD, NPR_SELECTED, CCB_CODE, NPR_TOTPREM, NPR_SUMASSURED, NVL(NPR_BENEFITTERM,0) NPR_BENEFITTERM, NPR_PREMIUM, NVL(NPR_EDUNITS,0) NPR_EDUNITS, NPR_COMMLOADING, NPR_BASICFLAG FROM LNPR_PRODUCT WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(NPR_BASICFLAG,'Y') = 'N' ORDER BY PPR_PRODCD ");
			/*sb_query.Append("SELECT LNPR.NP1_PROPOSAL,LNPR.NP2_SETNO,LNPR.PPR_PRODCD,LNPR.NPR_SELECTED,LNPR.CCB_CODE,LNPR.NPR_TOTPREM, LNPR.NPR_SUMASSURED, NVL(LNPR.NPR_BENEFITTERM,0) NPR_BENEFITTERM, LNPR.NPR_PREMIUM,LNPR.NPR_COMMLOADING,LNPR.NPR_BASICFLAG,   ");
			sb_query.Append("(SELECT PRI_BUILTIN FROM LPRI_RIDER LPRI WHERE LPRI.PPR_RIDER=LNPR.PPR_PRODCD AND LPRI.PPR_PRODCD=(SELECT DISTINCT PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND NPR_BASICFLAG='Y')) PRI_BUILTIN   ");
			sb_query.Append("FROM LNPR_PRODUCT LNPR, LPPR_PRODUCT LPPR WHERE LNPR.PPR_PRODCD=LPPR.PPR_PRODCD AND LNPR.NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(LNPR.NPR_BASICFLAG,'Y') = 'N' ORDER BY LPPR.PPG_GROUPID ");*/
			String query = EnvHelper.Parse(sb_query.ToString());
			IDbCommand myCommand = DB.CreateCommand(query);
			this.Holder.FillData(myCommand, "LNPR_PRODUCT");
			return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_TARGETVALUES_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_TARGETVALUES_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(230);//to do we have to Optimize it too.
			sb_query.Append("SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD FROM LNPR_PRODUCT  WHERE  ({0} like '{1}')  AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NVL(NPR_BASICFLAG,'N') = 'Y'  ");
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
		//public DataHolder FindByPK(string NP1_PROPOSAL,double NP2_SETNO,string PPR_PRODCD)
		public DataHolder FindByPK(string NP1_PROPOSAL, double NP2_SETNO, string PPR_PRODCD)
		{

			//</method-signature><method-body>
			String strQuery = "SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,USE_USERID,USE_DATETIME,NPR_BENEFITTERM,NPR_PREMIUMTER,NPR_SUMASSURED,NPR_PREMIUM,NPR_LOADING,NPR_BASICFLAG,NPR_TOTPREM,NPR_BUILTIN,NPR_PREMRATE,NPR_EXTMORTRATE,NPR_ENDORSMT,NPR_LIFE,NPR_SAR,NPR_ETASA,NPR_PAIDUPSA,NPR_ETATERM,NPR_PURENDOWMENT,NPR_INDEXATION,NPR_INDEXRATE,NPR_ETAPREMIUM,NPR_CASHVALUE,NPR_FACTOR,NPR_ETADAYS,NPR_EDUNITS,NP2_APPROVED,NPR_AGEPREM,NPR_AGEPREM2ND,CONVERT,NPR_EFFECTDATE,NPR_COMMLOADING,NPR_INCLUDELOADINNIV,NPR_MATURITYDATE,NPR_AGEDIFFERENCE,CCB_CODE,NPR_PAID,NPR_INTERESTRATE,NPR_EXTMORTRATE2,NP2_AGEPREM,NPR_PAIDUPTOAGE,CMO_MODE,NPR_BASICPRMANNUAL,NPR_EXCESPRMANNUAL,NPR_SELECTED,NPR_AGEPREM2,PCU_CURRCODE,NPR_EXCESSPREMIUM,NPR_EXCESSPREMIUM_ACTUAL,NPR_PREMIUM_FC,NPR_PREMIUM_AV,NPR_PREMIUM_ACTUAL,NPR_PREMIUMDISCOUNT FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, NP1_PROPOSAL));
			myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO", DbType.Decimal, 3, NP2_SETNO));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD", DbType.String, 3, PPR_PRODCD));



			this.Holder.FillData(myCommand, "LNPR_PRODUCT"); 
			return this.Holder; // original
																				 //</method-body>
		}

	


	public static Boolean UnderWritten(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT COUNT(NQN_ANSWER) ANSWER FROM LNQN_QUESTIONNAIRE WHERE NP1_PROPOSAL=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			object objPremium = myCommand.ExecuteScalar();

			if(objPremium==null)
			{
				return false;
			}
			else
			{
				if(System.Convert.ToString(objPremium) == ""  )
				{
					return false;
				}
				else if(Convert.ToDouble(objPremium) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static Boolean PremiumExist(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT SUM(NPR_PREMIUM) FROM LNPR_PRODUCT WHERE NPR_BASICFLAG='Y' AND NP2_SETNO=1 AND NP1_PROPOSAL=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			object objPremium = myCommand.ExecuteScalar();

			if(objPremium==null)
			{
				return false;
			}
			else
			{
				if(System.Convert.ToString(objPremium) == ""  )
				{
					return false;
				}
				else if(Convert.ToDouble(objPremium) == 0)
				{
					return false;
				}
				else if(Convert.ToDouble(objPremium) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		

	}
}
