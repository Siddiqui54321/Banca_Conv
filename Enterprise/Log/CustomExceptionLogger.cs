using System;
using SHMA.Enterprise.Configuration;

namespace SHMA.Enterprise.Shared.Log
{
	/// <summary>
	/// Summary description for CustomExceptionLogger.
	/// </summary>
	public class CustomExceptionLogger
	{
		private static bool LogEanabled;
		private static string LogFilePath;
		private static string[] ExceptionTokens;
        private static bool isHostedOnIIS;
		
		static CustomExceptionLogger()
		{
			LogEanabled = false;
			LogFilePath = null;
			ExceptionTokens = null;
            isHostedOnIIS = AppSettings.GetSetting("HostedOnIIS") == null ? true : AppSettings.GetSetting("HostedOnIIS")=="Y" ? true : false;  // "Y" or "N"

			string Setting = null;
			Setting = AppSettings.GetSetting("ExceptionLogEnabled");
			if ((Setting != null ) && (Setting.ToLower().Equals("true")))
			{		
				LogEanabled = true;
				
				Setting = AppSettings.GetSetting("ExceptionLogPath");
				if ((Setting!=null) && (Setting.Trim().Length>0))
				{
                    if (isHostedOnIIS)
                    {
                        LogFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + Setting;
                    }
                    else
                    {
                        LogFilePath = AppDomain.CurrentDomain.BaseDirectory + Setting;
                    }
				}

				Setting = AppSettings.GetSetting("ExceptionLogTokens");
				if ((Setting!=null) && (Setting.Trim().Length>0))
				{
					char[] splitter  = {','};
					string[]List = Setting.Split(splitter);

					for(int i=0; i< List.Length; i++)
					{
						List[i] = List[i].Trim();
					}

					ExceptionTokens = List;
				}

			}

		}
			public static void Log(Exception e)
			{
					if (LogEanabled)
				{
					string message = e.Message;
					foreach(string tok in ExceptionTokens)
						if (message.IndexOf(tok)>=0)
						{
							DNH.Common.Errors.ErrorLog logger = new DNH.Common.Errors.ErrorLog(LogFilePath);	
							logger.SaveExeptionToLog(e);
						}
				}
			}

		
	}
}
