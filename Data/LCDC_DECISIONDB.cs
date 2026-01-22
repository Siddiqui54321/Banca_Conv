using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class LCRE_REASONDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LCRE_REASONDB   (DataHolder dh):base(dh)
		{}
		//</constructor>

		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LCRE_REASON";}
				//			//			//			//</property-body>
		}
		//</property>

		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM LCRE_REASON";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
		
		//<method><method-name>GetDDL_PolicyAcceptance_CDC_CODE_RO</method-name><method-signature>
		public static IDataReader GetDDL_PolicyAcceptance_CRE_REASCODE_RO()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(211);
			sb_query.Append("SELECT CRE_REASCODE,");
			sb_query.Append("CRE_DESCRIPT DESC_F  FROM LCRE_REASON  ORDER BY CRE_REASCODE");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>
	}
}