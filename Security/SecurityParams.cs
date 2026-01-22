using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;

namespace Security
{
	/// <summary>
	/// Summary description for SecurityParams.
	/// </summary>
	public class SecurityParams
	{
		public SecurityParams()
		{
			try
			{
				rowset rowSet = SHAB.Data.SecurityPara.getSecurityParameters();
				if(rowSet.next())
				{
					this.passwordExpiryDays = rowSet.getInt("SEC_PASSWORDEXPIRYDAYS");
					this.msgBeforepasswordDays = rowSet.getInt("SEC_MSGBEFOREEXPIRYDAYS");
					this.passwordHistorySaved = rowSet.getInt("SEC_PASSWORDHISTORYSAVED");
					this.passwordAttemptAllowed = rowSet.getInt("SEC_PASSWORDATTEMPTSALLOWED");
				}
			}
			catch{
				this.passwordExpiryDays = defaultValue;
				this.msgBeforepasswordDays = defaultValue;
				this.passwordHistorySaved = defaultValue;
				this.passwordAttemptAllowed = defaultValue;
			}

		}
		public int getPasswordExpiryDays()
		{
			return passwordExpiryDays;
		}
		public int getMsgBeforepasswordDays()
		{
			return msgBeforepasswordDays;
		}
		public int getPasswordHistorySaved()
		{
			return passwordHistorySaved;
		}
		public int getPasswordAttemptAllowed()
		{
			return passwordAttemptAllowed;
		}

		private int passwordExpiryDays = 0;
		private int msgBeforepasswordDays = 0;
		private int passwordHistorySaved = 0;
		private int passwordAttemptAllowed = 0;
		private int defaultValue = 999;
		//private char activityLog = 'N';
	}
}
