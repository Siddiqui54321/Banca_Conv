using System;
using SHMA.Enterprise.Data;
using System.Collections.Specialized;
namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for ParameterCollection.
	/// </summary>
	public class ParameterCollection:NameObjectCollectionBase
		
	{
		private System.Collections.ArrayList dataType = new System.Collections.ArrayList();
		public ParameterCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public virtual System.Data.DbType getType(int index) 
		{
			return	(System.Data.DbType)dataType[index];
		}

		public void puts(String key, Object Value)
		{
			if (Value is System.DateTime) 
			{
				dataType.Add(Types.DATE);
			}
			else if (Value is System.Double) 
			{
				dataType.Add(Types.DOUBLE);
			}
			else if (Value is System.String) 
			{
				dataType.Add(Types.VARCHAR);
			}
			else if (Value is System.Int64) 
			{
				dataType.Add(Types.BIGINT);
			}
			else if (Value is System.Int32) 
			{
				dataType.Add(Types.INTEGER);
			}
			else if (Value is System.Int16) 
			{
				dataType.Add(Types.SMALLINT);
			}
			else if (Value is System.Single) 
			{
				dataType.Add(Types.FLOAT);
			}
			else if (Value is System.Single) 
			{
				dataType.Add(Types.FLOAT);
			}
			else if (Value is System.Decimal)
			{
				dataType.Add(Types.DECIMAL);
			}
			else 
			{
				dataType.Add(Types.OTHER);
			}

            if (Value is System.String && Value != null)
            {
                Value = (Object)Value.ToString().Trim();
            }

			this.BaseAdd(key,Value);
		}

		public void puts(String key, Object Value, System.Data.DbType sqlType) {

            if (Value is System.String && Value != null)
            {
                Value = (Object)Value.ToString().Trim();
            }

            dataType.Add(sqlType); 
			this.BaseAdd(key,Value);
		}

		public Object gets(string key)
		{
			return(this.BaseGet(key));
		}

		public int size()
		{
			return(this.Count);
		}

		public void clear()  
		{
			this.BaseClear();
			dataType.Clear();
		}

		public void remove(string key)
		{
			this.BaseRemove(key);
			dataType.Remove(key);
		}

		public System.Collections.IEnumerator values()
		{
			return(this.GetEnumerator());
		}

	}
}
