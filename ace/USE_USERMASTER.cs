using System;

namespace ace
{
	/// <summary>
	/// Summary description for USE_USERMASTER.
	/// </summary>
	public class USE_USERMASTER
	{
		public USE_USERMASTER()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public string testMethod(string abc)
		{
			return abc;
		}
		public bool validatePassword(string userId,string pwdEntered)
		{
            string pwd = SHAB.Data.USE_USERMASTERDB.getPassword(userId);
			return Security.UserOperation.validatePassword(pwdEntered,pwd);
		}
		public int getRemainingExpiryDays(string userId,int pwdExpiryDays)
		{	
			string pwdUpdatedDate = SHAB.Data.USE_USERMASTERDB.getLastUpdatedPasswordDate(userId);			
			if(pwdUpdatedDate.Equals(string.Empty))
			{
				updateUseMaster(userId,"use_jobdescrip",DateTime.Now.ToString());
				pwdUpdatedDate = DateTime.Now.ToString();							
			}
			return getRemainingDays(Convert.ToDateTime(pwdUpdatedDate),pwdExpiryDays);
		}
		private int getRemainingDays(DateTime lastPwdChangeDate,int pwdExpiryDays)
		{	
			//string pwdExpiryDays = ace.ConfigSettings.getDecryptedEntry("PasswordExpiryDays");
			DateTime currDate = DateTime.Now;		
			int totalDays = currDate.Subtract(lastPwdChangeDate).Days;
			return pwdExpiryDays - totalDays;
		}
		private bool isChangedPasswordExistInPassWordHistory(string userID,string changedPassword,string[] passwordHistoryList)
		{
			bool exists = false;	
		
			foreach(string password in passwordHistoryList)
			{
				if(changedPassword.Equals(password))
				{
					exists = true;
					break;
				}
			}		
			return exists;
		}
		public bool validatingPasswordHistory(string userID,string password,int passwordHistorySaved)
		{
			//int passwordHistorySaved = int.Parse(ace.ConfigSettings.getDecryptedEntry("PasswordHistorySaved"));
			//string passwordHistoryList = ace.ConfigSettings.getDecryptedData(SHAB.Data.USE_USERMASTERDB.getPasswordListHistory(userID));
			string passwordHistoryList = Security.EncryptorDecryptor.EncryptDecrypt(SHAB.Data.USE_USERMASTERDB.getPasswordListHistory(userID));
			bool pwdExistsInHistory = isChangedPasswordExistInPassWordHistory(userID,password,passwordHistoryList.Split(new char[]{','}));
			bool isValid = false;
			if(!pwdExistsInHistory)
			{
				int index = passwordHistoryList.IndexOf(",");
				if(passwordHistorySaved <= passwordHistoryList.Split(new char[]{','}).Length)
				{
					if(index!=-1)
					{
						passwordHistoryList = passwordHistoryList.Equals(string.Empty) ? string.Empty : passwordHistoryList.Substring(++index);
					}
				}
				passwordHistoryList += passwordHistoryList.Equals(string.Empty) ? password : ","+ password;
				//updateUseMaster(userID,"use_expression='"+ace.ConfigSettings.getEncryptedData(passwordHistoryList)+"',use_jobdescrip='"+DateTime.Now.ToString()+"',use_password='"+ Security.UserOperation.getEncryptedPassword(password) +"'");		
				updateUseMaster(userID,"use_expression='"+Security.EncryptorDecryptor.EncryptDecrypt(passwordHistoryList)+"',use_jobdescrip='"+DateTime.Now.ToString()+"',use_password='"+ Security.UserOperation.getEncryptedPassword(password) +"'");		
				
				isValid = true;				
			}			
			return isValid;
		}
		public void updateUseMaster(string userID,string colName,string colValue)
		{
			SHMA.Enterprise.Data.DB.executeDML("update use_usermaster set "+colName+" = '"+colValue+"' where upper(use_userid) = '"+userID.ToUpper()+"'");			
		}
		public void updateUseMaster(string userID,string strColNameValue)
		{
			SHMA.Enterprise.Data.DB.executeDML("update use_usermaster set "+strColNameValue+" where upper(use_userid) = '"+userID.ToUpper()+"'");
		}
	}
}
