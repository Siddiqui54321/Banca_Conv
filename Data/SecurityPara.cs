using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;

namespace SHAB.Data
{
	/// <summary>
	/// Summary description for SecurityPara.
	/// </summary>
	public class SecurityPara : SHMA.CodeVision.Data.DataClassBase
	{
		public SecurityPara(DataHolder dh):base(dh)
		{}
		public override String TableName
		{		
			get {return "SECURITY_PARA";}		
		}
		public static rowset getSecurityParameters()
		{
			const string query_ = "select sec_activitylog, sec_loginlog, sec_passwordexpirydays, sec_msgbeforeexpirydays, sec_passwordhistorysaved, sec_passwordattemptsallowed from security_para where sec_activescheme = 'Y'";
			return DB.executeQuery(query_);
		}
	}
}
