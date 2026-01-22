using System;	
using System.IO;
using System.Web.UI;

namespace SHMA.Enterprise.Shared{
	public sealed class FileStore{
		
		private static System.Collections.Specialized.NameValueCollection  appSettings = System.Configuration.ConfigurationSettings.AppSettings;		
		
		public static string StoreFile(string strKey,  System.Web.HttpPostedFile file, string strFolder){						 			
			if (file!=null && file.FileName.Length>0 && file.FileName.LastIndexOf(@"\")>0){				
				string strPath="";
				string fileName = FileStore.GetFileName(file.FileName);
				if (appSettings["FileStorePhysical"] == null)					
					strPath = string.Format(@"FileStore\{0}\{1}", strFolder ,strKey);
				else
					strPath = string.Format(@"{0}\{1}\{2}",  appSettings["FileStorePhysical"] , strFolder , strKey);

				if (Directory.Exists(strPath))			Directory.Delete(strPath,true);
				System.IO.Directory.CreateDirectory(strPath);
				file.SaveAs(string.Format(@"{0}\{1}",strPath ,fileName));
				return fileName ;
			}	
			else{
				throw new Exception("Invalid file.");
			}
		}


		public static string GetFileName(string FilePath){        
			if (FilePath.Length>0){
				int pos = FilePath.LastIndexOf(@"\") + 1; 
				return FilePath.Substring(pos);															
			}
			else
				return null;
		}

		
		public static string GetFilePath(string strKey, string strFolder){        
			string strPath="";
			if (appSettings["FileStorePhysical"] == null)					
				strPath = string.Format(@"FileStore/{0}/{1}", strFolder , strKey);
			else
				strPath = String.Format(@"{0}/{1}/{2}", appSettings["FileStorePhysical"], strFolder , strKey);
			
			if (Directory.Exists(strPath)){
				string[] files = Directory.GetFiles(strPath);
				if (files.Length>0)
					return files[0];
				else
					return null;
			}
			else
				return null;	
		}


		public static string GetDownloadPath(string strKey, string strFolder, string FileName){        
			string strPath="";			
			if (appSettings["FileStoreVirtual"] == null)
				strPath = string.Format(@"FileStore/{0}/{1}/{2}", strFolder , strKey, FileName);
			else
				strPath = string.Format(@"{0}/{1}/{2}/{3}",appSettings["FileStoreVirtual"] , strFolder , strKey, FileName);
		
			return strPath;	
		}

		public static void RemoveFile(string FilePath){			 
				System.IO.File.Delete(FilePath);
		}
	}
}

