using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LNGU_GUARDIANDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LNGU_GUARDIANDB (DataHolder dh):base(dh)
		
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LNGU_GUARDIAN";}
			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LNGU_GUARDIAN";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
		//<method><method-name>Exists</method-name><method-signature>
		public static Boolean Exists(NameValueCollection pkNameValue)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT count(*) FROM LNGU_GUARDIAN WHERE NGU_GUARDCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NGU_GUARDCD",DbType.Decimal, 6, pkNameValue["NGU_GUARDCD"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ST_GUARDIAN_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetILUS_ST_GUARDIAN_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(163);//to do we have to Optimize it too.
			//sb_query.Append("SELECT NGU_GUARDCD,NGU_NAME,CRL_RELEATIOCD FROM LNGU_GUARDIAN   WHERE  ({0} like '{1}') AND NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NGU_GUARDCD");
			sb_query.Append("SELECT NGU_GUARDCD,NGU_NAME,GU.CRL_RELEATIOCD CRL_RELEATIOCD, CRL_DESCR FROM LNGU_GUARDIAN GU, LCRL_RELATION RL WHERE ({0} like '{1}') AND GU.CRL_RELEATIOCD = RL.CRL_RELEATIOCD AND NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NGU_GUARDCD ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_ST_GUARDIAN_lister_RO</method-name><method-signature>
		public static IDataReader GetILUS_ST_GUARDIAN_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(163);//to do we have to Optimize it too.
			//sb_query.Append("SELECT NGU_GUARDCD,NGU_NAME,CRL_RELEATIOCD FROM LNGU_GUARDIAN WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NGU_GUARDCD ");
			sb_query.Append("SELECT NGU_GUARDCD,NGU_NAME,GU.CRL_RELEATIOCD CRL_RELEATIOCD, CRL_DESCR FROM LNGU_GUARDIAN GU, LCRL_RELATION RL WHERE GU.CRL_RELEATIOCD = RL.CRL_RELEATIOCD AND NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ORDER BY NGU_GUARDCD ");
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
			const String strQuery = "SELECT * FROM LNGU_GUARDIAN";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(double NGU_GUARDCD)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LNGU_GUARDIAN WHERE NGU_GUARDCD=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NGU_GUARDCD",DbType.Decimal, 6, NGU_GUARDCD));

			this.Holder.FillData(myCommand, "LNGU_GUARDIAN");return this.Holder;
			//</method-body>
		}
		//</method>

	}
}
