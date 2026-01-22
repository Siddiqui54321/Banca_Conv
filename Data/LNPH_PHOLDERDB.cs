using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LNPH_PHOLDERDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LNPH_PHOLDERDB              (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
	
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LNPH_PHOLDER";}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LNPH_PHOLDER";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetILUS_ET_NM_PER_PERSONALDETINS_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PER_PERSONALDETINS_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(297);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPH_CODE,NPH_LIFE FROM LNPH_PHOLDER  WHERE (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_PER_PERSONALDETINS_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PER_PERSONALDETINS_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(297);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPH_CODE,NPH_LIFE FROM LNPH_PHOLDER  WHERE  ({0} like '{1}')  AND (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
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
			String strQuery = "SELECT count(*) FROM LNPH_PHOLDER WHERE NPH_CODE=? AND NPH_LIFE=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_CODE",DbType.String, 12, pkNameValue["NPH_CODE"]));
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_LIFE",DbType.String, 1, pkNameValue["NPH_LIFE"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetDDL_ILUS_ET_NM_PROPOSAL_NPH_FULLNAME_RO</method-name><method-signature>
		public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_NPH_FULLNAME_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(321);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPH_CODE,");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//			sb_query.Append("'-'");
//			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("NPH_FULLNAME DESC_F  FROM LNPH_PHOLDER  WHERE NPH_CODE= (SELECT NPH_CODE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') ORDER BY NPH_FULLNAME");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_PER_PERSONALDET_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PER_PERSONALDET_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(297);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPH_CODE,NPH_LIFE FROM LNPH_PHOLDER  WHERE (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')  ");
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
			const String strQuery = "SELECT * FROM LNPH_PHOLDER";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ET_NM_PER_PERSONALDET_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ET_NM_PER_PERSONALDET_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(297);//to do we have to Optimize it too.
			sb_query.Append("SELECT NPH_CODE,NPH_LIFE FROM LNPH_PHOLDER  WHERE  ({0} like '{1}')  AND (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')  ");
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
		public DataHolder FindByPK(string NPH_CODE,string NPH_LIFE)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LNPH_PHOLDER WHERE NPH_CODE=? AND NPH_LIFE=? ";

			//String strQuery = "SELECT LNPH_PHOLDER.*,cop.COP_DESCR FROM LNPH_PHOLDER,lcop_occupation cop WHERE ";
			//strQuery+=" (cop.cop_occupaticd(+) = LNPH_PHOLDER.cop_occupaticd) AND ";
			//strQuery+=" LNPH_PHOLDER.NPH_CODE=? AND LNPH_PHOLDER.NPH_LIFE=? ";

			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_CODE",DbType.String, 12, NPH_CODE));
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_LIFE",DbType.String, 1, NPH_LIFE));

			this.Holder.FillData(myCommand, "LNPH_PHOLDER");return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindIllusByPK(string NPH_IDNO2,string NPH_CODE,string NPH_LIFE)
		{
			//</method-signature><method-body>
			//String strQuery = "SELECT * FROM LNPH_PHOLDER WHERE NPH_IDNO2 =? AND NPH_CODE=? AND NPH_LIFE=? ";
			String strQuery = "SELECT * FROM LNPH_PHOLDER WHERE NPH_IDNO2 =? ";

			//String strQuery = "SELECT LNPH_PHOLDER.*,cop.COP_DESCR FROM LNPH_PHOLDER,lcop_occupation cop WHERE ";
			//strQuery+=" (cop.cop_occupaticd(+) = LNPH_PHOLDER.cop_occupaticd) AND ";
			//strQuery+=" LNPH_PHOLDER.NPH_CODE=? AND LNPH_PHOLDER.NPH_LIFE=? ";

			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_IDNO2",DbType.String, 13, NPH_IDNO2));
//			myCommand.Parameters.Add(DB.CreateParameter("@NPH_CODE",DbType.String, 12, NPH_CODE));
//			myCommand.Parameters.Add(DB.CreateParameter("@NPH_LIFE",DbType.String, 1, NPH_LIFE));

			this.Holder.FillData(myCommand, "LNPH_PHOLDER");return this.Holder;
			//</method-body>
		}
		//</method>

	}
}
