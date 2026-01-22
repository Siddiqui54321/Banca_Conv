using System;
using System.Data;
using System.Text;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using System.Web;

namespace SHMA.Enterprise.Shared
{
	public class EnvHelper
	{
		//Added by Asif
		private static String strEnvSource = System.Configuration.ConfigurationSettings.AppSettings["EnvSource"]==null? "session":System.Configuration.ConfigurationSettings.AppSettings["EnvSource"];
        private static String strGlobalVariable = System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"] == null ? "Application" : System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"];

        public static HttpApplicationState Appvariable;

        public EnvHelper()
        {
            if (strGlobalVariable=="Application" && Appvariable == null)
            {
                Appvariable = HttpContext.Current.Application; // to support multi threading processes
            }
        }
        
        public static string Parse(string stringToParse){
			return new RunTimeLibrary().replaceRequestSessionApplicationValues(stringToParse);			
		}
		public static void setRequestInSession(){
			string[] param;
			foreach (string key in System.Web.HttpContext.Current.Request.Params.AllKeys){
				if (key.StartsWith("r_")){
					param = System.Web.HttpContext.Current.Request[key].Split(',');
					SessionObject.Set(key.Replace("r_",""), param[param.Length-1]);
				}
			}
		}
		
        public void setAttribute(String name, Object value) 
		{
			//Added by Asif
			if(strEnvSource=="collection")
			{
				EnvVarCollection.Set(name,value);
			}
			else
			{
				SessionObject.Set(name,value);
			}
		}

        public static System.Collections.Hashtable getCollection()
        {
            // function used to get current thread collection to forward to new thread in multithreaded process
            return EnvVarCollection.getCollection();
        }

        public static void setCollection(System.Collections.Hashtable envCollection)
        {
            // function used to set current thread collection to use in multithreaded process
            EnvVarCollection.setCollection(envCollection);
        }

		public Object getAttribute(String name) 
		{
			//Added by Rehan
		    if (name.IndexOf("g_") == 0)
		    {
		        if (strGlobalVariable == "Application")
		        {
                    return Appvariable.Contents.Get(name).ToString();
                    //return (System.Web.HttpContext.Current.Application[name].ToString());
                }
                else if (strEnvSource == "collection")
                {
                    return (EnvVarCollection.Get(name));
                }
                else
                {
                    return (SessionObject.Get(name));
                }

		    }
		    else 
            {
                if (strEnvSource == "collection")
                {
                    return (EnvVarCollection.Get(name));
                }
                else
                {
                    return (SessionObject.Get(name));
                }
            }

            //Added by Asif
            //if(strEnvSource=="collection")
            //{
            //    return (EnvVarCollection.Get(name));
            //}
            //else
            //{
            //    if(name.IndexOf("g_")==0)
            //    {
            //        return(System.Web.HttpContext.Current.Application[name].ToString());
            //    }
            //    else
            //    {
            //        return(SessionObject.Get(name));
            //    }
            //}



//			if(name.IndexOf("g_")==0)
//				return(System.Web.HttpContext.Current.Application[name].ToString());
//			else
//				return(SessionObject.Get(name));
		}
		//public static string SetRequestValuesInSession(){
		//	return new RunTimeLibrary().replaceRequestSessionApplicationValues(stringToParse);			
		//}
		public static void RefreshStateVariables(){
			string[] sessionPattren=new string[2];
			string[] sessionKey = new string[3];
			sessionPattren[0] = "s_";
			sessionPattren[1] = "g_";
			sessionKey[0] = "POR_ORGACODE";
			sessionKey[1] = "PLC_LOCACODE";
			sessionKey[2] = "PFS_ACNTYEAR";
			SessionObject.Refresh(sessionKey,sessionPattren);
		}
	}
}
