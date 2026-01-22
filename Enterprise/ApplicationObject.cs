using System;
using System.Data;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared.Data;
namespace SHMA.Enterprise.Shared
{
	/// <summary>
	/// Summary description for ApplicationObject.
	/// </summary>
	public class ApplicationObject
	{
		public ApplicationObject()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void LoadGlobalApplicationVariables(bool Disconnect)
		{
			DataTable dataTable = ApplicationObjectDB.GetGlobalValues(Disconnect);
			foreach (DataRow row in dataTable.Rows) 
			{
				System.Web.HttpContext.Current.Application[row["PYD_SPDTCODE"].ToString()]= row["PYD_SPDTVALUE"].ToString();	
			}
		}
	}
}
