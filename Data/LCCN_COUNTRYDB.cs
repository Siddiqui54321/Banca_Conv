using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
	public class LCCN_COUNTRYDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LCCN_COUNTRYDB                        (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LCCN_COUNTRY";}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LCCN_COUNTRY";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetILUS_ET_NM_Country_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Country_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(155);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD FROM LCCN_COUNTRY  WHERE  ({0} like '{1}') ORDER BY CCN_DESCR ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PER_PROPOSALDET_CCN_CTRYCD_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PER_PROPOSALDET_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(190);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			//sb_query.Append("'-'");
			//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCN_DESCR DESC_F  FROM LCCN_COUNTRY  ORDER BY CCN_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_UC_USERCOUNTRY_CCN_CTRYCD_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_UC_USERCOUNTRY_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(128);//to do we have to Optimize it too.
			sb_query.Append(" DESC_F  FROM LCCN_COUNTRY  ");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_Country_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_Country_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(155);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD FROM LCCN_COUNTRY ORDER BY CCN_DESCR ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_HOME_CCN_CTRYCD_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_HOME_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(190);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,CCN_CTRYCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCN_DESCR DESC_F  FROM LCCN_COUNTRY  ORDER BY CCN_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		public  DataHolder FindByPK(string CCN_CTRYCD)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LCCN_COUNTRY WHERE CCN_CTRYCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, CCN_CTRYCD));

			this.Holder.FillData(myCommand, "LCCN_COUNTRY");return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>getAll_RO</method-name><method-signature>
		public static IDataReader getAll_RO()
		{
			//</method-signature><method-body>
			const String strQuery = "SELECT * FROM LCCN_COUNTRY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		public static IDataReader getAll_Companies()
		{
			//</method-signature><method-body>
			const String strQuery = "Select ccd_code,NP1_ASSIGNMENTCD||'-'||NP1_ASSIGNMENTDESC as cdesc  from LCCH_CHANNEL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		public static IDataReader getAll_Assignee()
		{
			//</method-signature><method-body>
			const String strQuery = "Select com_id,com_name from LNCO_COMPANY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>Exists</method-name><method-signature>
		public static Boolean Exists(NameValueCollection pkNameValue)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT count(*) FROM LCCN_COUNTRY WHERE CCN_CTRYCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 3, pkNameValue["CCN_CTRYCD"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_WELCOME_CCN_CTRYCD_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_WELCOME_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(288);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,''");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("''");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCN_DESCR DESC_F  FROM LCCN_COUNTRY  WHERE CCN_CTRYCD IN (SELECT CCN_CTRYCD FROM LUCN_USERCOUNTRY WHERE USE_USERID=SV(\"s_USE_USERID\")) ORDER BY CCN_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_UC_USERCOUNTRY2_CCN_CTRYCD_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_UC_USERCOUNTRY2_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(172);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,CCN_CTRYCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCN_DESCR DESC_F  FROM LCCN_COUNTRY  ");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>


		public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_CCN_CTRYCD_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(288);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,''");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("''");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("CCN_DESCR DESC_F  FROM LCCN_COUNTRY  WHERE CCN_CTRYCD IN (SELECT CCN_CTRYCD FROM LUCN_USERCOUNTRY WHERE USE_USERID=SV(\"s_USE_USERID\")) ORDER BY CCN_DESCR");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader(); 
			//</method-body>
		}
		//</method>

		//

		public static IDataReader getcity(string COUNTRYID, string PROVINCE)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT CCT_CITYCD,CCT_DESCR FROM LCCT_CITY where CCN_CTRYCD='"+COUNTRYID+"' and CCT_PROVCD='" + PROVINCE + "' ORDER BY CCT_DESCR";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}

		//Get Province BY Country Id

	
		public static IDataReader GetProvince(string COUNTRYID)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT CPR_PROVCD,CPR_DESCR FROM LCPR_PROVINCE where CCN_CTRYCD='"+COUNTRYID+"' ORDER BY CPR_DESCR ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			DB.closeConnection();
			return myCommand.ExecuteReader();
		
			//</method-body>
		}

		//<method><method-name>GetCOUNTRY_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetCOUNTRY_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(147);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,CCN_DESCR FROM LCCN_COUNTRY   WHERE  ({0} like '{1}')  ORDER BY CCN_DESCR ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetCOUNTRY_lister_RO</method-name><method-signature>
		public static IDataReader GetCOUNTRY_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(147);//to do we have to Optimize it too.
			sb_query.Append("SELECT CCN_CTRYCD,CCN_DESCR FROM LCCN_COUNTRY ORDER BY CCN_DESCR ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>
	}
}
