using System;
//using System.Web;
//using System.Web.SessionState ;
//using System.Threading;

namespace SHMA.Enterprise.Presentation
{
	public sealed class EnvVarCollection 
	{
		[ThreadStatic]
		private static System.Collections.Hashtable envVariables;
		private EnvVarCollection(){}

		#region public Methods

		public static System.Collections.Hashtable EnvCollection
		{
			get
			{
				if (envVariables == null)
				{
					envVariables = new System.Collections.Hashtable();
				}
				return envVariables;
			}
			//set
			//{
			//	envVariables = value;
			//}
		}

		public static void Remove(string name)
		{
			 EnvVarCollection.envVariables.Remove(name);
		}
        
		public static string GetString(string name)
		{
			Object obj = null;
			string str = ""; //It should have been "" but requires TIGHT TEST
			if (EnvVarCollection.envVariables[name]!=null)
			{
				obj = EnvVarCollection.envVariables[name];
			}
			if (obj != null)
			{
				str = obj.ToString();
			}
			return str;
		}

		public static object Get(string name)
		{
			if (EnvVarCollection.envVariables[name]!=null)
				return EnvVarCollection.envVariables[name];
			else	
				return null;
		}

		public static void Set(string name, object Value)
		{
			//EnvVarCollection.envVariables[name]= Value;
			EnvVarCollection.EnvCollection[name]= Value;
		}
		
		public static void Add(string strKey, object objValue)
		{
			if	(EnvVarCollection.envVariables[strKey]!=null)
				EnvVarCollection.EnvCollection.Add(strKey, objValue);
			else
				EnvVarCollection.EnvCollection[strKey]= objValue;
		}

		public static void RemoveAll()
		{
			EnvVarCollection.EnvCollection.Clear();
		}

		public static void RemoveAt(int Index)
		{
			//HttpContext.Current.Session.RemoveAt(Index);
			int counter=0;
			//System.Collections.IEnumerator Keys = EnvVarCollection.envVariables.GetEnumerator();
			System.Collections.IDictionaryEnumerator Keys = EnvVarCollection.EnvCollection.GetEnumerator();
			while(Keys.MoveNext())
			{
				if(counter == Index)
				{
					EnvVarCollection.EnvCollection.Remove(Keys.Key);
					break;
				}
				counter++;
			}
		}

		public static void RemoveAt(string strKey)
		{
			EnvVarCollection.EnvCollection.Remove(strKey);
		}

		public static int Count
		{
			get
			{
				return EnvVarCollection.EnvCollection.Count;
			}
		}

		public static bool ContainsKey(string strKey)
		{
			bool keyFound = false;
			System.Collections.IDictionaryEnumerator Keys = EnvVarCollection.EnvCollection.GetEnumerator();
			 while(Keys.MoveNext()) 
			 {
				 if(Keys.Key.ToString() == strKey)
				 {
					 keyFound = true;
					 break;
				 }
			}
			return keyFound;
		}

        public static System.Collections.Hashtable getCollection()
        {
            // function used to get current thread collection to forward to new thread in multithreaded process
            return envVariables;
        }

        public static void setCollection(System.Collections.Hashtable envCollection)
        {
            // function used to set current thread collection to use in multithreaded process
            envVariables = envCollection;
        }

		public static void Refresh(string[] strKeys, string[] strPattrens)
		{
			/*string Key;
			bool DeleteKey = true;
			System.Collections.ArrayList indexToRemove= new System.Collections.ArrayList();
			int KeyCounter=0;
			//System.Collections.IEnumerator Keys  = HttpContext.Current.Session.Keys.GetEnumerator();
			System.Collections.IDictionaryEnumerator Keys = EnvVarCollection.EnvCollection.GetEnumerator();
			
			while(Keys.MoveNext()) 
			{
				Key = Keys.Key.ToString();
				
				for(int i=0;i<strPattrens.Length;i++)
				{
					if(Key.Substring(0,strPattrens[i].Length)== strPattrens[i])
					{
						DeleteKey = false;
						break;
					}
				}

				if(DeleteKey)
				{
					for(int i=0;i<strKeys.Length;i++)
						if(Key == strKeys[i])
						{
							DeleteKey = false;
							break;
						}
				}

				if(DeleteKey)
				{
					//HttpContext.Current.Session.RemoveAt(KeyCounter);
					indexToRemove.Add(KeyCounter);
				}
				DeleteKey = true;
				KeyCounter++;
			}
			System.Collections.IEnumerator removeIndex  = indexToRemove.GetEnumerator();
			KeyCounter=0;
			while(removeIndex.MoveNext())
			{
				HttpContext.Current.Session.RemoveAt(Convert.ToInt32(removeIndex.Current)-KeyCounter);
				KeyCounter++;
			}*/
		}

		#endregion 
	
	}
}