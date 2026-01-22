using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LCDC_DECISIONDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LCDC_DECISIONDB   (DataHolder dh):base(dh)
		{}
		//</constructor>

		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LCDC_DECISION";}
				//			//			//			//</property-body>
		}
		//</property>

		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LCDC_DECISION";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
		
		//<method><method-name>GetDDL_PolicyAcceptance_CDC_CODE_RO</method-name><method-signature>
		public static IDataReader GetDDL_PolicyAcceptance_CDC_CODE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(211);
			sb_query.Append("SELECT CDC_CODE,");
			sb_query.Append("CDC_DESCRIPTION DESC_F  FROM LCDC_DECISION  ORDER BY CDC_SHORT");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>
		//<method><method-name>GetDDL_LPVL_VALIDATION_DECISION_PVL_VALIDRANGE_RO</method-name><method-signature>
		public static IDataReader GetDDL_LPVL_VALIDATION_DECISION_PVL_VALIDRANGE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(211);
			sb_query.Append("SELECT CDC_CODE,");
			sb_query.Append("CDC_DESCRIPTION DESC_F  FROM LCDC_DECISION  ORDER BY CDC_SHORT");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		
	}
}