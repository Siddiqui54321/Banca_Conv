using System;
using SHMA.Enterprise.Data;
using System.Data;
namespace SHMA.Enterprise.Shared.Data
{
	/// <summary>
	/// Summary description for ApplicationObjectDB.
	/// </summary>
	public class ApplicationObjectDB
	{
		public ApplicationObjectDB()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataTable GetGlobalValues(bool disconnect) 
		{
			//</method-signature><method-body>
			//entity base table's query
			IDataReader dataReader=null;
			DataTable dataTable = new DataTable();
			try
			{
				String strQuery = "SELECT 'g_'" + PortableSQL.getConcateOperator() + "PYH_SPHDCODE" + PortableSQL.getConcateOperator() + "'_'" + PortableSQL.getConcateOperator() + "PYD_SPDTCODE as PYD_SPDTCODE , PYD_SPDTVALUE FROM PR_GN_YD_SYSTEMPARADTL";
				IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
				dataReader = myCommand.ExecuteReader();
				SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
			}
			finally
			{
				if(dataReader != null) 
				{
					dataReader.Close();
				}
				if(disconnect)
				{
					DB.Connection.Close();
				}
			}
			return dataTable;
			//	//</method-body>
		}

	}
}
