using System;
using System.Web;
using System.Web.SessionState ;
//using System.Threading;

namespace SHMA.Enterprise.Presentation{
	public sealed class SessionObject {
//		private static void Initialize(){						
//			Page page = new Page();
//			LocalDataStoreSlot SessionSlot;
//			if (Thread.GetNamedDataSlot("Session") == null)
//				SessionSlot = Thread.AllocateNamedDataSlot("Session");
//			else
//				SessionSlot= Thread.GetNamedDataSlot("Session");				
//			Thread.SetData(SessionSlot,page.Session);
//			page = null;
//		}
	static HttpSessionState Session{		
		get{ return HttpContext.Current.Session;}
	}
	
#region public Methods
		public static void Remove(string name){
			Session.Remove(name);
		}
		public static string GetString(string name){								
			Object obj = null;
			string str = ""; //It should have been "" but requires TIGHT TEST
			if (HttpContext.Current.Session[name]!=null){				
				obj = Session[name];
			}
			if (obj != null){
				str = obj.ToString();
			}
			return str;
		}
		public static object Get(string name){			
			if (HttpContext.Current.Session[name]!=null){				
				return Session[name];
			}
			else	return null;
		}
		public static void Set(string name, object Value){
			HttpContext.Current.Session[name] = Value;
			//if(Value == null || Value == "" && name == "POR_ORGACODE"){
			//	throw new ProcessException("Empty POR_ORGACODE is set by this entity in session");
		   //}
		}
		
		public static void Add(string strKey, object objValue){			
			if	(Session[strKey]!=null)
				HttpContext.Current.Session.Add(strKey, objValue);
			else
				HttpContext.Current.Session[strKey] = objValue;
		}
		public static void RemoveAll(){
			HttpContext.Current.Session.RemoveAll();
		}
		public static void RemoveAt(int Index){
			HttpContext.Current.Session.RemoveAt(Index);
		}
		public static void RemoveAt(string strKey){
			int counter=0;
			System.Collections.IEnumerator Keys  = Session.Keys.GetEnumerator();
			while(Keys.MoveNext()) {
				if(Keys.Current.ToString() == strKey){
					HttpContext.Current.Session.RemoveAt(counter);
					break;
				}
				counter++;
			}
		}
		public static int Count{
			get{	
				return HttpContext.Current.Session.Count;}
		}

		public static bool ContainsKey(string strKey){
			bool keyFound = false;
			System.Collections.IEnumerator Keys  = Session.Keys.GetEnumerator();
			 while(Keys.MoveNext()) {
				 if(Keys.Current.ToString() == strKey){
					 keyFound = true;
					 break;
				 }
			}
			return keyFound;
		}

		public static void Refresh(string[] strKeys, string[] strPattrens){
		
            /*
            string Key;
			bool DeleteKey = true;
			System.Collections.ArrayList indexToRemove= new System.Collections.ArrayList();
			int KeyCounter=0;
			System.Collections.IEnumerator Keys  = HttpContext.Current.Session.Keys.GetEnumerator();
			while(Keys.MoveNext()) {
				Key = Keys.Current.ToString();
				
				for(int i=0;i<strPattrens.Length;i++){
					if(Key.Substring(0,strPattrens[i].Length)== strPattrens[i]){
						DeleteKey = false;
						break;
					}
				}
				if(DeleteKey){				
					for(int i=0;i<strKeys.Length;i++)
						if(Key == strKeys[i]){
							DeleteKey = false;
							break;
						}
				}
				if(DeleteKey){
					//HttpContext.Current.Session.RemoveAt(KeyCounter);
					indexToRemove.Add(KeyCounter);
				}
				DeleteKey = true;
				KeyCounter++;
			}
			System.Collections.IEnumerator removeIndex  = indexToRemove.GetEnumerator();
			KeyCounter=0;
			while(removeIndex.MoveNext()){
				HttpContext.Current.Session.RemoveAt(Convert.ToInt32(removeIndex.Current)-KeyCounter);
				KeyCounter++;
			}*/
		}
		HttpSessionState session{
			get{
				return HttpContext.Current.Session;
			}
		}

#endregion 
	
	}
}