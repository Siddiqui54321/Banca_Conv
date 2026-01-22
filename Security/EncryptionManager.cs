using System;
using StringBuilder = System.Text.StringBuilder;
using ASCIIEncoding = System.Text.ASCIIEncoding;
using MD5 = System.Security.Cryptography.MD5;
using MD5CryptoServiceProvider = System.Security.Cryptography.MD5CryptoServiceProvider;


namespace Security
{
	public class EncryptionManager 
	{

		private EncryptionManager() 
		{
		}

		public static bool testString(String str_stringToTest,String str_hash)
		{
			return getHash(str_stringToTest).Equals(str_hash);
		}

		public static String getHash(String str_stringToHash)
		{

			
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] buffer = encoding.GetBytes(str_stringToHash);

			// This is one implementation of the abstract class MD5.
			MD5 md5 = new MD5CryptoServiceProvider();

			byte[] digest = md5.ComputeHash(buffer);

			//And return the hex hash value as a String
			return asHex(digest);
		}

		private static String asHex (byte[] hash)
		{
			StringBuilder buf = new StringBuilder(hash.Length * 2);
			for (int i = 0; i < hash.Length; i++) 
			{
				int int_val = (int)hash[i] & 0xff;
				if (int_val < 0x10)
					buf.Append("0");
				buf.Append(Convert.ToString(int_val, 16));
			}
			return buf.ToString();
		}

	}
}