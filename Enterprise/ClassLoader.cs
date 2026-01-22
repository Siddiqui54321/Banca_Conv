using System;
using System.Web;
using System.Reflection;
using SHMA.Enterprise.Shared.Log;
namespace SHMA.Enterprise {
	/// <summary>
	/// Summary description for ClassLoader.
	/// </summary>
	public class ClassLoader {
		private string _cfgFile="ClassLoader.config";

		public   string cfgFile {
			get    { return _cfgFile; }
			set    { _cfgFile=value; }
		}

		public ClassLoader() {
			//
			// TODO: Add constructor logic here
			//
		}
		public object loadType(string type){
            object returnObject = null;
            try
            {
                returnObject = loadTypeFromLoadedAssemblies(type);
            }
            catch (Exception ex)
            {
                DBErrorLogger.logExecutionError("No-Query:loadTypeFromLoadedAssemblies", "Class Name: " + type, ex.StackTrace, "Class could not be loaded:" + ex.Message);
                returnObject = null;
            }

            if (returnObject == null)
            {
                try
                {
                    returnObject = loadTypeFromUnloadedAssembies(type);
                }
                catch (Exception ee)
                {
                    DBErrorLogger.logExecutionError("No-Query:loadTypeFromUnloadedAssembies", "Class Name: " + type, ee.StackTrace, "Class could not be loaded:" + ee.Message);
                }
            }
            return returnObject;
		}


		private object loadTypeFromLoadedAssemblies(string typeName){
			System.Reflection.Assembly[] assemblies = getLoadedAssemblies();
			object returnObject=null;
			foreach(Assembly assembly in assemblies) {
				if (assembly.GetType(typeName)!=null){
					returnObject = AppDomain.CurrentDomain.CreateInstance(assembly.FullName,typeName).Unwrap();
					break;
				}
			}
			return returnObject;
		}
		

		private object loadTypeFromUnloadedAssembies(string typeName){
			SHMA.Enterprise.Configuration.ConfigReader creader = new SHMA.Enterprise.Configuration.ConfigReader();
			Object returnObject=null;
			creader.cfgFile = getInfoFile();
			string asmblyName = creader.GetValue(typeName);
			if(asmblyName!=null && asmblyName!="") {
				string[] asemblies = asmblyName.Split(',');
				for(int i=0;i<asemblies.Length;i++){
					if(!isAssemblyLoaded(asemblies[i])){
						Assembly assembly = AppDomain.CurrentDomain.Load(asemblies[i]);
						if(assembly!=null){
							if(assembly.GetType(typeName)!=null){
								returnObject = AppDomain.CurrentDomain.CreateInstance(asemblies[i],typeName).Unwrap();
								return returnObject;
							}
						}
					}
				}
			}
			return returnObject;
		}
		
		private string getInfoFile(){
			string path = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath);
			return path + "\\" + _cfgFile;
		}

		private bool isAssemblyLoaded(string assemblyName){
			Assembly[] loadedAssemblies = getLoadedAssemblies();
			foreach(Assembly assembly in loadedAssemblies){
				string[] asmName = assembly.FullName.Split(',');
				asmName = asmName[0].Split('.');
				if(assemblyName.Equals(asmName[asmName.Length-1]))
					return true;
			}
			return false;
		}
		
		private Assembly[] getLoadedAssemblies(){
			return AppDomain.CurrentDomain.GetAssemblies();
		}
	}
}