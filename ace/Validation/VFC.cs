using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	public class VFC
	{
		#region "Construtor"
		private System.Collections.SortedList collection;
		public VFC()
		{
			collection = new System.Collections.SortedList();
		}

		#endregion
   
		#region "Set/Get Product and other fields in collection"
		public void setValue(string key, object _value)
		{
			if (collection.ContainsKey(key)) 
			{
				collection[key] = _value;
			}
			else 
			{
				collection.Add(key, _value);
			}
		}
   
		public object getObject(string key)
		{
			return collection[key];
		}
   
		public string getString(string key)
		{
			return Convert.ToString(collection[key]);
		}
   
		public double getDouble(string key)
		{
			return Convert.ToDouble(collection[key]);
		}
   
		public int getInt(string key)
		{
			return Convert.ToInt16(collection[key]);
		}
		#endregion
   
		#region "Set/Get Rider information"

		public void setRiderSumAssured(string riderCode, double _value)
		{
			string key = "SA" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				collection[key] = _value;
			}
		}
		public double getRiderSumAssured(string riderCode)
		{
			string key = "SA" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				return getDouble(key);
			}
			else 
			{
				return 0;
			}
		}
   

		public void setRiderTotalPremium(string riderCode, double _value)
		{
			string key = "TP" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				collection[key] = _value;
			}
		}
		public double getRiderTotalPremium(string riderCode)
		{
			string key = "TP" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				return getDouble(key);
			}
			else 
			{
				return 0;
			}
		}

		
		public void setRiderBenefitTerm(string riderCode, int _value)
		{
			string key = "BT" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				collection[key] = _value;
			}
		}
   
		public int getRiderBenefitTerm(string riderCode)
		{
			string key = "BT" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				return getInt(key);
			}
			else 
			{
				return 0;
			}
		}


		public void setRiderFrequency(string riderCode, double value)
		{
			string key = "FR" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				collection[key] = value;
			}
		}
   
		public double getRiderFrequency(string riderCode)
		{
			string key = "FR" + riderCode;
			if (collection.ContainsKey(key)) 
			{
				return getDouble(key);
			}
			else 
			{
				return 0;
			}
		}
		#endregion
	}

}