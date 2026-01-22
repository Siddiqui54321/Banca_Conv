using System;

namespace Security
{
	/// <summary>
	/// Summary description for UserOperation.
	/// </summary>
	public class UserOperation
	{
		public UserOperation()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//		public bool login(string user, string password)
//		{
//			if (hasInvalidCharacters(user))
//				throw new Exception("User Code contains illegal characters");
//		
//			if (hasInvalidCharacters(password))
//				throw new Exception("Password contains illegal characters");
//
//			loadUserInfoFromDB(user);//Check user and its status
//	
//			if(EncryptionManager.testString(str_userPassword, str_value)==false)
//				throw new Exception("Invalid password");
//		}

		public static bool validatePassword(string passwordEntered, string encryptedPasswordDB)
		{
			return EncryptionManager.testString(passwordEntered, encryptedPasswordDB);
		}

		public static string getEncryptedPassword(string password)
		{
			return EncryptionManager.getHash(password);
		}
	}
}
