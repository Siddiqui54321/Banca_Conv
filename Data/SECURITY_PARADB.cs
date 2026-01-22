using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
	public class SECURITY_PARADB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public SECURITY_PARADB (DataHolder dh):base(dh)
		
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "SECURITY_PARA";}
			//			//</property-body>
		}
		//</property>
		//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
			get
			{
				const string strQuery="SELECT COUNT(*) FROM SECURITY_PARA";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//</property-body>
		}
		//</property>
		//<method><method-name>GetSECURITY_PARA_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetSECURITY_PARA_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(238);//to do we have to Optimize it too.
			sb_query.Append("SELECT PCM_COMPCODE ,CCN_CTRYCD  FROM SECURITY_PARA WHERE  ({0} like '{1}')  AND ((PCM_COMPCODE =SV(\"PCM_COMPCODE \") AND CCN_CTRYCD =SV(\"CCN_CTRYCD \"))  )   ");
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
			String strQuery = "SELECT count(*) FROM SECURITY_PARA WHERE PCM_COMPCODE =? AND CCN_CTRYCD =? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@PCM_COMPCODE ",DbType.String, 2, pkNameValue["PCM_COMPCODE "]));
			myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD ",DbType.String, 3, pkNameValue["CCN_CTRYCD "]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetSECURITY_PARA_lister_RO</method-name><method-signature>
		public static IDataReader GetSECURITY_PARA_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(238);//to do we have to Optimize it too.
			//sb_query.Append("SELECT PCM_COMPCODE ,CCN_CTRYCD  FROM SECURITY_PARA WHERE ((PCM_COMPCODE =SV(\"PCM_COMPCODE \") AND CCN_CTRYCD =SV(\"CCN_CTRYCD \"))  )   ");
			sb_query.Append("SELECT PCM_COMPCODE ,CCN_CTRYCD  FROM SECURITY_PARA WHERE ((PCM_COMPCODE =SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD =SV(\"s_CCN_CTRYCD\"))  )   ");
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
			const String strQuery = "SELECT * FROM SECURITY_PARA";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(string CCN_CTRYCD ,string PCM_COMPCODE )
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM SECURITY_PARA WHERE PCM_COMPCODE =? AND CCN_CTRYCD =? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@PCM_COMPCODE ",DbType.String, 2, PCM_COMPCODE ));
			myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD ",DbType.String, 3, CCN_CTRYCD ));

			this.Holder.FillData(myCommand, "SECURITY_PARA");return this.Holder;
			//</method-body>
		}
		//</method>

	}
}
