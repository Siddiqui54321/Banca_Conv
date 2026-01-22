using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class SmRlRoleDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public SmRlRoleDB            (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "SM_RL_ROLE";}
			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM SM_RL_ROLE";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetDDL_ILUS_ET_NM_PROPOSAL_AAG_AGCODE_RO</method-name><method-signature>
		public static IDataReader GetUserType(string usertype)
		{
			//</method-signature><method-body>
			/*StringBuilder sb_query=new StringBuilder(169);//to do we have to Optimize it too.
			sb_query.Append("SELECT AAG_AGCODE,");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("AAG_NAME DESC_F  FROM LAAG_AGENT  ");*/
			string query = "";

			if(usertype=="B")
			{
				query = "select srl_type code ,srl_desc desc_f from sm_rl_role where srl_type in ('S','M','C') order by slv_levelid desc";
			}
			else
			{
				query = "select srl_type code ,srl_desc desc_f from sm_rl_role order by slv_levelid desc";
			}

			//string query=sb_query.ToString();
			//query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		public static IDataReader GetDDL_ILUS_ET_UM_USERMANAGMENT_AAG_AGCODE_RO()
		{
			StringBuilder sb_query=new StringBuilder(169);
			sb_query.Append("SELECT AAG_AGCODE,AAG_AGCODE");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("AAG_NAME DESC_F  FROM LAAG_AGENT  ");
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

	

	}
}
