using System;
using System.Text;

namespace Security
{
	/// <summary>
	/// Summary description for EncryptorDecryptor.
	/// </summary>
	public class EncryptorDecryptor
	{
		private static int key = 129;
		public static string EncryptDecrypt(string textToEncrypt)
		{ 	
			StringBuilder inSb = new StringBuilder(textToEncrypt);
			StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
			char c;
			for (int i = 0; i < textToEncrypt.Length; i++)
			{
				c = inSb[i];
				c = (char)(c ^ key);
				outSb.Append(c);
			}			
			return outSb.ToString();
		}
	}
}
