using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LAAG_AGENTDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LAAG_AGENTDB            (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LAAG_AGENT";}
			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LAAG_AGENT";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}

        public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_AAG_AGCODE_RO()
		{
			StringBuilder sb_query=new StringBuilder(169);
            sb_query.Append("select aag_agcode||'-'||aag_fullname||'-'||aag_imedsupr||'-'||aag_imedsupr_name||' - '||chl_level  AAG_AGCODE from vw_laag_bsc order by aag_agcode");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
		}

		public static IDataReader GetDDL_ILUS_ET_UM_USERMANAGMENT_AAG_AGCODE_RO(string usertype,string bankcode)
		{
			StringBuilder sb_query=new StringBuilder(169);
			sb_query.Append("SELECT AAG_AGCODE,AAG_AGCODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("AAG_NAME DESC_F  FROM LAAG_AGENT a where a.ext_nactive='Y' ");

			if (usertype=="B" && bankcode == "5")
			{
				sb_query.Append(" and a.aag_imedsupr ='9510000' or a.aag_agcode='9510000' ");
			}
			sb_query.Append(" ORDER BY AAG_AGCODE ");
		
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
		}

		//<method><method-name>getAll_RO</method-name><method-signature>
		public static IDataReader getAll_RO()
		{
			//</method-signature><method-body>
			const String strQuery = "SELECT * FROM LAAG_AGENT";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>


		/// <summary>
		//========================== Agency Setup IBAD CODE 18-FEB-2019
		/// </summary>
		/// <param name="usertype"></param>
		/// <returns></returns>
		public static DataTable USE_USERTYPE()
		{
			StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
			sb_query.Append("SELECT * FROM LCSD_SYSTEMDTL WHERE CSH_ID ='AGTYP'");	
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			

			OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
			con.Open();
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = con;
			cmd.CommandText =  query;
			cmd.CommandType = CommandType.Text;
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds, "LCSD_SYSTEMDTL");
			con.Close();
			DataTable dtTable = ds.Tables["LCSD_SYSTEMDTL"];
			return dtTable;
		}
		
		
		public static DataTable MAXID()
		{
			StringBuilder sb_query = new StringBuilder(400);//to do we have to Optimize it too.
			sb_query.Append(" SELECT CASE WHEN LAAG_FLG = 'N' THEN (SELECT MAX(AAG_AGCODE) + 1 MAXID FROM LAAG_AGENT WHERE AAG_AGCODE LIKE '660%') ");	
			sb_query.Append(" ELSE NULL END MAXID");	
			sb_query.Append(" FROM (SELECT NVL(MAX('X'),'N') LAAG_FLG");	
			sb_query.Append(" FROM LAAG_AGENT WHERE AAG_AGCODE = ");	
			sb_query.Append("(SELECT MAX(AAG_AGCODE) + 1 MAXID FROM LAAG_AGENT WHERE AAG_AGCODE LIKE '660%'))");	
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			

			OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
			con.Open();
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = con;
			cmd.CommandText =  query;
			cmd.CommandType = CommandType.Text;
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds, "MAXID");
			con.Close();
			DataTable dtTable = ds.Tables["MAXID"];
			return dtTable;
		}


		public static DataTable SearchAgent(string AgentCode)
		{
			StringBuilder sb_query = new StringBuilder(400);//to do we have to Optimize it too.
			sb_query.Append(" SELECT AAG_AGCODE AGNTCODE, AAG_AGCODE ");	
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("' - '");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append(" AAG_NAME "); 
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("' - '");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append(" DECODE(EXT_NACTIVE,'Y','ACTIVE','N','IN ACTIVE','' ) AGNT_DESC,");
			sb_query.Append(" 	AAG_NAME, AAG_BROKER FROM LAAG_AGENT");	
			sb_query.Append(" WHERE AAG_AGCODE like '"+AgentCode+"'");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			

			OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
			con.Open();
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = con;
			cmd.CommandText =  query;
			cmd.CommandType = CommandType.Text;
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds, "AGENTDET");
			con.Close();
			DataTable dtTable = ds.Tables["AGENTDET"];
			return dtTable;
		}



        //<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_COP_OCCUPATICD_RO</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_COP_OCCUPATICD_RO()
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(201);//to do we have to Optimize it too.
            sb_query.Append("SELECT COP_OCCUPATICD,");
            //sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //sb_query.Append("'-'");
            //sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            sb_query.Append("COP_DESCR DESC_F  FROM LCOP_OCCUPATION  ORDER BY COP_DESCR");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }

		//========================== Agency Setup IBAD CODE 18-FEB-2019

	}
}
