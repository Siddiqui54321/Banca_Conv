using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;

namespace SHAB.Data
{
	public class CCS_CHANLSUBDETLDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public CCS_CHANLSUBDETLDB (DataHolder dh) : base(dh)
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "CCS_CHANLSUBDETL";}
			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM CCS_CHANLSUBDETL";
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
			sb_query.Append("SELECT CCS_CODE,CCS_DESCR,CCH_CODE,CCD_CODE FROM CCS_CHANLSUBDETL WHERE  ({0} like '{1}')  AND ((CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\"))  )  order by CCS_DESCR ");
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


		//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO(string CCH_CODE,string CCD_CODE)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(173);//to do we have to Optimize it too.
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
		//</method>

		//<method><method-name>GetILUS_ST_CHANNELSUBDTL_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ST_CHANNELSUBDTL_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(236);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCS_CODE,CCS_DESCR,CCH_CODE,CCD_CODE FROM CCS_CHANLSUBDETL WHERE ((CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\"))  ) order by CCS_DESCR   ");
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
			String strQuery = "SELECT count(*) FROM CCS_CHANLSUBDETL WHERE CCH_CODE=? AND CCD_CODE=? AND CCS_CODE=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, pkNameValue["CCH_CODE"]));
			myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, pkNameValue["CCD_CODE"]));
			myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE",DbType.String, 5, pkNameValue["CCS_CODE"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>getAll_RO</method-name><method-signature>
		public static IDataReader getAll_RO()
		{
			//</method-signature><method-body>
			const String strQuery = "SELECT * FROM CCS_CHANLSUBDETL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(string CCD_CODE,string CCH_CODE,string CCS_CODE)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM CCS_CHANLSUBDETL WHERE CCH_CODE=? AND CCD_CODE=? AND CCS_CODE=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 5, CCH_CODE));
			myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 3, CCD_CODE));
			myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE",DbType.String, 5, CCS_CODE));

			this.Holder.FillData(myCommand, "CCS_CHANLSUBDETL");
			return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCS_CODE_RO(string CCH_CODE, string CCD_CODE)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(173);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCS_CODE, CCS_CODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("CCS_DESCR DESC_F FROM CCS_CHANLSUBDETL WHERE CCH_CODE='" + CCH_CODE + "' AND CCD_CODE='" + CCD_CODE + "'" );
			sb_query.Append("CCS_DESCR DESC_F FROM CCS_CHANLSUBDETL WHERE CCH_CODE='" + CCH_CODE + "' " );
            if (CCD_CODE!="" || CCD_CODE!=null)
            {
				sb_query.Append("and CCd_CODE='" + CCD_CODE + "' ");
			}
			string query = sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>


		//<method><method-name>GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO</method-name><method-signature>
		public static String GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCS_DESCR DESC_F  FROM CCS_CHANLSUBDETL WHERE CCH_CODE=SV(\"CCH_CODE\") AND CCD_CODE=SV(\"CCD_CODE\") AND CCS_CODE=SV(\"CCS_CODE\") ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return (String)myCommand.ExecuteScalar();
			//</method-body>
		}
		//</method>


		//<method><method-name>GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO</method-name><method-signature>
		public static String GetLogOnBranch()
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(209);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCS_DESCR DESC_F  FROM CCS_CHANLSUBDETL WHERE CCH_CODE=SV(\"s_CCH_CODE\") AND CCD_CODE=SV(\"s_CCD_CODE\") AND CCS_CODE=SV(\"s_CCS_CODE\") ");
			string query = sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return (String)myCommand.ExecuteScalar();
			//</method-body>
		}
		//</method>
		public IDataReader GetBranchNames()
		{
			//SELECT CCS_DESCR,CCS_FIELD1 FROM CCS_CHANLSUBDETL WHERE CCS_FIELD1 IS NOT NULL AND CCH_CODE = '2' AND CCD_CODE = '1'
			string CCH_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCH_CODE").ToString();
			string CCD_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCD_CODE").ToString();
			string query = String.Format("SELECT CCS_FIELD1,CCS_FIELD1||'-'||CCS_DESCR as CCS_DESCR FROM CCS_CHANLSUBDETL WHERE CCS_FIELD1 IS NOT NULL AND CCH_CODE = '{0}' AND CCD_CODE = '{1}'", CCH_CODE,CCD_CODE);
			IDbCommand myCommand = DB.CreateCommand(query);																											
			return myCommand.ExecuteReader();

			//		IDbCommand myCommand = DB.CreateCommand(query, DB.Connection);	
			//		dataHolder.FillData(myCommand, "BranchNames");
			//return dataHolder;
		}

		public static String GetDefaultBranchName()
		{
			string CCH_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCH_CODE").ToString();
			string CCD_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCD_CODE").ToString();
			string CCS_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCS_CODE").ToString();
			string query = String.Format("SELECT CCS_FIELD1,CCS_DESCR FROM CCS_CHANLSUBDETL WHERE CCS_FIELD1 IS NOT NULL AND CCH_CODE = '{0}' AND CCD_CODE = '{1}' AND CCS_CODE='{2}' ",CCH_CODE,CCD_CODE,CCS_CODE);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return Convert.ToString(myCommand.ExecuteScalar());

		}

		public static String GetBranchCode(string proposalNo,string accountNo )
		{
			string query = string.Format("select pbb_branchcode from lnu1_underwriti where np1_proposal ='{0}' and nu1_accountno = '{1}' and  nu1_life='F'",
				proposalNo,accountNo);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return Convert.ToString(myCommand.ExecuteScalar());
		}

		public static string GetBranchDetails(string proposalNo)
		{
			string BranchDet="";
			/*
			string query = string.Format("SELECT A.NP1_PROPOSAL, A.NP1_CHANNEL, A.NP1_CHANNELDETAIL, A.NP1_CHANNELSDETAIL FROM LNP1_POLICYMASTR A WHERE A.NP1_PROPOSAL = '{0}'", proposalNo);	
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			IDataReader reader = myCommand.ExecuteReader();
			while (reader.Read())
			{
				BranchDet = reader["NP1_CHANNEL"].ToString()+","+reader["NP1_CHANNELDETAIL"].ToString();
			}*/
			String strQuery = "SELECT A.NP1_PROPOSAL, A.NP1_CHANNEL, A.NP1_CHANNELDETAIL, A.NP1_CHANNELSDETAIL FROM LNP1_POLICYMASTR A WHERE A.NP1_PROPOSAL = '"+proposalNo+"' ";
			DataTable dt = new DataTable();
			OleDbDataAdapter myda = new OleDbDataAdapter(strQuery, DB.Connection.ConnectionString.ToString());
			myda.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				BranchDet = dt.Rows[0]["NP1_CHANNEL"].ToString()+","+dt.Rows[0]["NP1_CHANNELDETAIL"].ToString();
			}
			else {BranchDet="";}
			return BranchDet;
		}
	}
}
