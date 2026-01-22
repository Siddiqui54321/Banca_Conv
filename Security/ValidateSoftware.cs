using System;

namespace Security
{
	/// <summary>
	/// Summary description for ValidateSoftware.
	/// </summary>
	public class ValidateSoftware
	{
		public ValidateSoftware()
		{
		}

		public static bool validateKey()
		{
			SecurityUtility secUtility = new Security.SecurityUtility();
			String[] secResponse = secUtility.ReadSecurityFile("Security.enc");
			return true;
		}

		private static string GetSystemInfo(string SoftwareName)
		{
			//if (UseProcessorID == true)

				SoftwareName += RunQuery("Processor", "ProcessorId");
			/*
			if (UseBaseBoardProduct == true)

				SoftwareName += RunQuery("BaseBoard", "Product");

			if (UseBaseBoardManufacturer == true)

				SoftwareName += RunQuery("BaseBoard", "Manufacturer");
			// See more in source code

			SoftwareName = RemoveUseLess(SoftwareName);

			if (SoftwareName.Length < 25)

				return GetSystemInfo(SoftwareName);
				*/
			return SoftwareName.Substring(0, 25).ToUpper();
		} 

		private static string RunQuery(string TableName, string MethodName)
		{
			/*ManagementObjectSearcher MOS =
				new ManagementObjectSearcher("Select * from Win32_" + TableName);
			foreach (ManagementObject MO in MOS.Get())
			{
				try
				{
					return MO[MethodName].ToString();
				}
				catch (Exception e)
				{
					//System.Windows.Forms.MessageBox.Show(e.Message);
				}
			}*/
			return "";
		} 
	}
}
