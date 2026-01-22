using System;

namespace Security
{
	public class SecurityUtility
	{
		public string[] ReadSecurityFile(string filename)
		{
			try
			{
				System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);

				byte[] bytes = new byte[fs.Length];
				fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
				fs.Close();

				string s="";
				RC4 objRC4 = new RC4();
			
				for(int i=0; i<bytes.Length; i++)
					s = s + System.Convert.ToString(System.Convert.ToChar(objRC4.EnDeCryptSingle(bytes[i])));

				s = s.Replace("\r\n","~");
				string [] lines = s.Split('~');

				return lines;
			}
			catch(System.IO.FileNotFoundException fileNotFound)
			{
				throw new SHMA.Enterprise.Exceptions.ProcessException("Security file not found. ");
			}
			catch(System.IO.IOException IOError)
			{
				throw new SHMA.Enterprise.Exceptions.ProcessException("Error in reading Security File " + IOError.Message);			
			}
			catch(Exception e)
			{
				throw new SHMA.Enterprise.Exceptions.ProcessException("Error in reading Security File " + e.Message);
			}

		}
	}
}

