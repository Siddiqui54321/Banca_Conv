using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;

namespace ace
{
	/// <summary>
	/// Summary description for Change_Password.
	/// </summary>
	public class Change_Password
	{
		public Change_Password()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//check User Password

		public bool chkuserpwd(string use_userid,string use_password)
		{
		
			//String struserpwd = "select use_password from USE_USERMASTER where use_userid='"+use_userid+"' and use_password='"+use_password+"'";
			String struserpwd = "select use_password from USE_USERMASTER where use_userid='"+use_userid+"' and use_password='"+Security.UserOperation.getEncryptedPassword(use_password)+"'";
            bool flag=false;
			//System.out.println(strBasicPlan);
			rowset rstBasicPlan = DB.executeQuery( struserpwd );
			if ( rstBasicPlan.next())
			{
			flag=true;
			return flag;
			}
			else
			{
			
				//throw new ProcessException ("password does not match");
				flag=false;
				return flag;  
			}
		}

//Update User Password
		public void updateuserpwd(string use_userid,string use_userpwd)
		{
			try
			{
				//DB.executeDML("update USE_USERMASTER set USE_PASSWORD='"+use_userpwd+"' where USE_USERID='"+use_userid+"'");
				string encPassword = Security.UserOperation.getEncryptedPassword(use_userpwd);
				DB.executeDML("update USE_USERMASTER set USE_PASSWORD='" + encPassword + "' where USE_USERID='"+use_userid+"'");
			}
			catch(Exception ex)
			{
			
			}
		
		}
		public void updateActiveuserpwd(string use_userid,string use_userpwd)
		{
			try
			{
				//DB.executeDML("update USE_USERMASTER set USE_PASSWORD='"+use_userpwd+"' where USE_USERID='"+use_userid+"'");
				string encPassword = Security.UserOperation.getEncryptedPassword(use_userpwd);
				DB.executeDML("update USE_USERMASTER set USE_PASSWORD='" + encPassword + "',USE_ACTIVE='Y' where USE_USERID='"+use_userid+"'");
			}
			catch(Exception ex)
			{
			
			}
		
		}
		public void updateuserpwd(string use_userid,string use_userpwd,Security.ACTIVITY activity)
		{
			//DB.executeDML("update USE_USERMASTER set USE_PASSWORD='"+use_userpwd+"' where USE_USERID='"+use_userid+"'");
			//string encPassword = Security.UserOperation.getEncryptedPassword(use_userpwd);
			//DB.executeDML("update USE_USERMASTER set use_jobdescrip='"+DateTime.Now.ToString()+"', USE_PASSWORD='" + encPassword + "' where USE_USERID='"+use_userid+"'");
			int passwordHistorySaved = new Security.SecurityParams().getPasswordHistorySaved();
			if(!new ace.USE_USERMASTER().validatingPasswordHistory(use_userid,use_userpwd,passwordHistorySaved))
			{
				//int passwordHistorySaved = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["PasswordHistorySaved"]);
				
				throw new Exception("Last "+passwordHistorySaved+" Passwords are not Allowed, try other passwords.");
			}
			else
			{
				Security.LogingUtility.GenerateActivityLog(activity);
			}
		}
		public void updateUserPWD(string userID,Security.ACTIVITY activity)
		{
			int pwdExpiryDays = new Security.SecurityParams().getPasswordExpiryDays();
            //DateTime Expiredate = DateTime.Now.AddDays(-(pwdExpiryDays+1));--Disabled by RizwanKarim on 17/09/2021 to update expiry date same as of changed password date..Expiry days increased to 99999
            DateTime Expiredate = DateTime.Now;
			//new ace.USE_USERMASTER().updateUseMaster(userID,"use_password='"+ Security.UserOperation.getEncryptedPassword(userID)+"',USE_JOBDESCRIP='"+Expiredate.ToString()+"'");
            new ace.USE_USERMASTER().updateUseMaster(userID, "use_password='" + Security.UserOperation.getEncryptedPassword(userID) + "',USE_JOBDESCRIP='" + Expiredate.ToString() + "'"+",USE_ACTIVE='Y"+"'");	
            Security.LogingUtility.GenerateActivityLog(activity);
		}

	}
}
