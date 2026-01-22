using System;
namespace SHMA.Enterprise{
	public class NameValueCollection : System.Collections.SortedList{
		public NameValueCollection(){	
		}		
		public NameValueCollection(object defaultValue, params object[] keys)
		{
			foreach (object obj in keys)
			{
				Add(obj, defaultValue);
			}
		}
		public void add(String key, Object value) 
		{
			this.Add(key,value);
			
		}
		public Object get(String key) {
			return(this[key]);
		}

		public int size() {
			return(this.Count);
		}

		public void clear() {
			this.Clear();
		}

		public void remove(String name) {
			this.Remove(name);
		}

		public System.Collections.IEnumerator iterator() {
			return(this.GetEnumerator());
		}

		public System.Collections.IEnumerator  keysIterator() {
			return(this.Keys.GetEnumerator());
		}
		public Object getObject(String key){
			return(this[key]);
		}
		public NameValueCollection Copy(){
			NameValueCollection myClone = new NameValueCollection();
			foreach (string key in this.Keys){
				myClone.Add( key , this[key]);
			}
			return myClone;
		}
		public void Set(string key, object value) {
			if (this.ContainsKey(key))
				this[key] = value;
			else
				this.Add(key,value);			

		}
		public void set(string key, object value) {
			this.Set(key, value);
		}
	}	
}
