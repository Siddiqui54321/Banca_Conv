using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LCSD_SYSTEMDTLDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LCSD_SYSTEMDTLDB                                       (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
		
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LCSD_SYSTEMDTL";}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LCSD_SYSTEMDTL";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetILUS_ET_NM_Channel_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Channel_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL  WHERE CSH_ID='CHNEL'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		public static IDataReader GetExtendedFlag_Lister_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);//to do we have to Optimize it too.
			sb_query.Append("SELECT csd_type,csd_value desc_f FROM LCSD_SYSTEMDTL  WHERE CSH_ID='PRFL1'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}

		public static IDataReader GetCRF_FORFEITUCD()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);
			sb_query.Append("SELECT csd_type,csd_value desc_f FROM LCSD_SYSTEMDTL  WHERE CSH_ID='FRFET'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}

		//<method><method-name>GetDDL_ILUS_ET_NM_HOME_NP1_CHANNEL_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_HOME_NP1_CHANNEL_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			/*
			sb_query.Append("SELECT CSD_TYPE,CSD_TYPE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='CHNEL' ORDER BY CSD_VALUE");
			*/
			sb_query.Append("SELECT CCH_CODE csd_type,CCH_CODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '784') ORDER BY CCH_DESCR");

			//		SessionObject.Get("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue);
			//		SessionObject.Set("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue);
			//		SessionObject.Set("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue);

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
			String strQuery = "SELECT count(*) FROM LCSD_SYSTEMDTL WHERE csd_type=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@csd_type",DbType.String, 15, pkNameValue["csd_type"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_HOME_NP1_CHANNELDETAIL_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_HOME_NP1_CHANNELDETAIL_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			/*
			sb_query.Append("SELECT CSD_TYPE,CSD_TYPE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='BANKC' ORDER BY CSD_VALUE");
			*/
			sb_query.Append("SELECT CCD_CODE CSD_TYPE,CCD_CODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='01' ORDER BY CCD_DESCR");

			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_PURPOSE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_PURPOSE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='PURPS' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_TARGETVALUES_NPR_INCLUDELOADINNIV_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_TARGETVALUES_NPR_INCLUDELOADINNIV_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='EPRMT' ORDER BY CSD_VALUE");
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
			const String strQuery = "SELECT * FROM LCSD_SYSTEMDTL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_COMMLOADING_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_COMMLOADING_RO()
			//SELECT SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-')+1,1) CSD_TYPE,CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-')+1,1) CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3) = SV(\"PPR_PRODCD\") ORDER BY CSD_TYPE, CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
			//</method>

			//<method><method-name>GetDDL_ILUS_ET_TB_BENEFECIARY_NBF_BASIS_RO</method-name><method-signature>
			public static IDataReader GetDDL_ILUS_ET_TB_BENEFECIARY_NBF_BASIS_RO()
			{
				//</method-signature><method-body>
				StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
				sb_query.Append("SELECT CSD_TYPE,");
				//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
				//			sb_query.Append("'-'");
				//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
				sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='BNFBS' ORDER BY CSD_VALUE");
				string query=sb_query.ToString();
				query = EnvHelper.Parse(query);
				IDbCommand myCommand = DB.CreateCommand(query);
				return myCommand.ExecuteReader();
				//</method-body>
			}
		//</method>

		//<method><method-name>GetDDL_ILUS_BENEFECIARY_NBF_BASIS_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_BENEFECIARY_NBF_BASIS_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(194);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='BNFBS' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_TITLE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_TITLE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='TITLE' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_INCLUDELOADINNIV_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_INCLUDELOADINNIV_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='EPRMT' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_PURPOSE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_PURPOSE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='PURPS' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNELDETAIL_RO</method-name><method-signature>
		//***
		public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNELDETAIL_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			//sb_query.Append("SELECT CSD_TYPE,CSD_TYPE");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='BANKC' ORDER BY CSD_VALUE");
			
			sb_query.Append("SELECT CCD_CODE csd_type,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE=SV(\"NP1_CHANNEL\") ORDER BY CCD_DESCR");
			sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE=SV(\"s_CCH_CODE\") ORDER BY CCD_DESCR");

			
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQUIREDFOR_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQUIREDFOR_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='REQFR' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_TB_PLANDETAILS_NPR_COMMLOADING_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_TB_PLANDETAILS_NPR_COMMLOADING_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNR' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_NPH_TITLE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_NPH_TITLE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='TITLE' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_NPH_TITLE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_IDTYPE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE NPH_IDTYPE,");
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='BAIDT' ORDER BY CSD_STATUS");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNELDETAIL_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNELDETAIL_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCD_CODE csd_type,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE=SV(\"NP1_CHANNEL\") ORDER BY CCD_DESCR");
			sb_query.Append("CCD_DESCR DESC_F  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE=SV(\"s_CCH_CODE\") ORDER BY CCD_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_Title_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Title_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL  WHERE  ({0} like '{1}')  AND CSH_ID='TITLE'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_Title_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Title_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL  WHERE CSH_ID='TITLE'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNEL_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNEL_RO()
		{
			
			//filter mechanism 
			//SELECT CCH_CODE || '-' || CCH_DESCR,CCH_CODE  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '')
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCH_CODE csd_type,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = SV(\"s_CCN_CTRYCD\")) ORDER BY CCH_DESCR");
			sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL  WHERE CCH_CODE = SV(\"s_CCH_CODE\") ORDER BY CCH_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(string csd_type)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LCSD_SYSTEMDTL WHERE csd_type=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@csd_type",DbType.String, 15, csd_type));

			this.Holder.FillData(myCommand, "LCSD_SYSTEMDTL");return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(string CSH_ID, string CSD_TYPE)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LCSD_SYSTEMDTL WHERE CSH_ID=? AND CSD_TYPE=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CSH_ID",DbType.String, 5, CSH_ID));
			myCommand.Parameters.Add(DB.CreateParameter("@CSD_TYPE",DbType.String, 15, CSD_TYPE));

			this.Holder.FillData(myCommand, "LCSD_SYSTEMDTL");return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_DEATHBENEFITOPTION_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_DEATHBENEFITOPTION_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNEL_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNEL_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			//sb_query.Append("SELECT CSD_TYPE,CSD_TYPE");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='CHNEL' ORDER BY CSD_VALUE");
			
			sb_query.Append("SELECT CCH_CODE csd_type,");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//			sb_query.Append("'-'");
			//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCH_DESCR DESC_F  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = SV(\"s_CCN_CTRYCD\")) ORDER BY CCH_DESCR");
			
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_DEATHBENEFITOPTION_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_DEATHBENEFITOPTION_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			//sb_query.Append("SELECT SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-')+1,1) CSD_TYPE,CSD_TYPE");
			sb_query.Append("SELECT CSD_TYPE,");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DBOSW' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_Channel_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Channel_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(178);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL  WHERE  ({0} like '{1}')  AND CSH_ID='CHNEL'  ORDER BY CSD_VALUE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_REQUIREDFOR_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_REQUIREDFOR_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CSD_TYPE,CSD_TYPE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CSD_VALUE DESC_F  FROM LCSD_SYSTEMDTL  WHERE CSH_ID='REQFR' ORDER BY CSD_VALUE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_CCB_CODE_RO()
		{

			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
			rowset rs = DB.executeQuery("select 'A' from  LCSD_SYSTEMDTL where CSH_ID='CALBS' ");
			if(rs.next())
			{
				sb_query.Append("select csd_type CCB_CODE, csd_value DESC_F from  LCSD_SYSTEMDTL where CSH_ID='CALBS' ORDER BY CSD_STATUS ");
			}
			else
			{
				sb_query.Append(" select 'T' CCB_CODE, 'Premium'     DESC_F FROM dual ");
				sb_query.Append(" union ");
				sb_query.Append(" select 'S' CCB_CODE, 'Sum Assured' DESC_F FROM dual ");
			}
			//</method-signature><method-body>
			
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO</method-name><method-signature>
		public static IDataReader GetDDL_PolicyAcceptance_NP1_PAYMENTMET_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(200);//to do we have to Optimize it too.
			rowset rs = DB.executeQuery("select 'A' from LCSD_SYSTEMDTL where CSH_ID='BAPTP'");
			if(rs.next())
			{
				sb_query.Append("select csd_type NP1_PAYMENTMET, csd_value DESC_F from  LCSD_SYSTEMDTL where CSH_ID='BAPTP' ");
			}
			else
			{
				sb_query.Append("select 'B' NP1_PAYMENTMET, 'Bank Debit Order' DESC_F FROM DUAL ");
			}

			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO</method-name><method-signature>
		public static IDataReader GetDDL_BANCAPARAMS_CSH_ID()
		{
			//</method-signature><method-body>
			string query = "SELECT CSH_ID, CSH_DESCR DESC_F FROM LCSH_SYSTEMHDR WHERE CSH_ID IN (SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='BAPRM')";
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetLCSD_SYSTEMDTL_BANCAPARAMS_lister_RO</method-name><method-signature>
		public static IDataReader GetLCSD_SYSTEMDTL_BANCAPARAMS_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			
			//Filter Query for CSD_TYPE
			string filterQueryForType = "";
			rowset rs = DB.executeQuery(EnvHelper.Parse("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='BAPRM' AND CSD_TYPE=SV(\"CSH_ID\") "));
			if(rs.next())
			{
				if(rs.getObject("CSD_VALUE") != null)
				{
					if(rs.getString("CSD_VALUE").Trim().Length > 0)
					{
						string strTypeList = "";
						string []typeList = rs.getString("CSD_VALUE").Trim().Split(',');
						for (int i=0; i<typeList.Length; i++)
						{
							if(i==0)
							{
								strTypeList = "'" + typeList[i].Trim() + "'";
							}
							else
							{
								strTypeList += ",'" + typeList[i].Trim() + "'";
							}
						}
						filterQueryForType = " AND CSD_TYPE IN (" + strTypeList + ")";
					}
				}
			}


			StringBuilder sb_query=new StringBuilder(212);
			//sb_query.Append("SELECT CSH_ID,CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID=SV(\"CSH_ID\") ORDER BY CSD_TYPE ");
			
			sb_query.Append("SELECT CSH_ID,CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID=SV(\"CSH_ID\") " + filterQueryForType + " ORDER BY CSD_TYPE ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetLCSD_SYSTEMDTL_BANCAPARAMS_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetLCSD_SYSTEMDTL_BANCAPARAMS_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(212);//to do we have to Optimize it too.
		    sb_query.Append("SELECT CSH_ID,CSD_TYPE FROM LCSD_SYSTEMDTL WHERE ({0} like '{1}') AND CSH_ID=SV(\"CSH_ID\") ORDER BY CSD_TYPE ");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		
		public static IDataReader getUIMenuExist(string userType)
		{
			string query_ = "select csd_type,csd_value from lcsd_systemdtl where csh_id = 'MENUE' ";
			IDbCommand myCommand = DB.CreateCommand(query_);
			//myCommand.Parameters.Add(DB.CreateParameter("@CSD_TYPE",DbType.String, 15, userType));
			return myCommand.ExecuteReader();
		}

		public static IDataReader getUIMenu(string userType)
		{
			string query_ = "select csd_type,csd_value from lcsd_systemdtl where csh_id = 'MENUE' and csd_type like '%"+userType+"%' order by to_number(substr(csd_type,0,instr(csd_type,',')-1))";
			IDbCommand myCommand = DB.CreateCommand(query_);
			//myCommand.Parameters.Add(DB.CreateParameter("@CSD_TYPE",DbType.String, 15, userType));
			return myCommand.ExecuteReader();
		}

		public static bool InsertAllowed()
		{
			bool blnInsertAllowed = true;
			rowset rs = DB.executeQuery(EnvHelper.Parse("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='BAPRM' AND CSD_TYPE=SV(\"CSH_ID\") "));
			if(rs.next())
			{
				if(rs.getObject("CSD_VALUE") != null)
				{
					if(rs.getString("CSD_VALUE").Trim().Length > 0)
					{
						blnInsertAllowed = false;
					}
				}
			}

			return blnInsertAllowed;
		}

	}
}
