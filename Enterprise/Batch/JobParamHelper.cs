using System;
using NameValueCollection=SHMA.Enterprise.NameValueCollection;
using EnvHelper=SHMA.Enterprise.Shared.EnvHelper;

namespace SHMA.Enterprise.Batch
{
	public sealed class JobParamHelper
	{
		private JobParamHelper(){}

		public static NameValueCollection GetItemsData(object [][] columnNameValueArray)
		{
			try
			{
				if (columnNameValueArray[0].Length == 0) return null;
				object [] keysNVC = columnNameValueArray[0];//get Keys array for NVC
				NameValueCollection nvc = new NameValueCollection();
				for(int i=0; i<keysNVC.Length; i++)
					nvc.Add(keysNVC[i],columnNameValueArray[1][i]);
				return nvc;
			}
			catch(Exception e)
			{
				throw new Exception("Problem in constructing NameValue Collection." + e.Message);
			}
		}

		public static NameValueCollection[] GetAllItemsData(object [][] NVCArray)
		{
			try
			{
				if (NVCArray[0].Length == 0) return null;
				NameValueCollection columnNameValue=new NameValueCollection();
				NameValueCollection[] dataRows = new NameValueCollection[NVCArray.Length-1];//Because 0 index is for keys only
				object [] keysNVC = NVCArray[0];//get Keys array for NVC
				for(int row=1; row<NVCArray.Length; row++)
				{
					columnNameValue = new NameValueCollection();
					for (int nvcElement=0; nvcElement<keysNVC.Length; nvcElement++)
						columnNameValue.Add(keysNVC[nvcElement],NVCArray[row][nvcElement]);
					dataRows[row-1] = columnNameValue;
				}
				return dataRows;
			}
			catch(Exception e)
			{
				throw new Exception("Problem in constructing NameValue Collection Array." + e.Message);
			}
		}

		public static EnvHelper GetAllSessionValues(string[] sessionKeys, Object[] sessionValues)
		{
			EnvHelper env = new EnvHelper();
			try
			{
				if (sessionKeys.Length == 0) return null;
				for(int i=0; i<sessionKeys.Length; i++)
					env.setAttribute(sessionKeys[i].ToString(),sessionValues[i]);
				return env;
			}
			catch(Exception e)
			{
				throw new Exception("Problem in coverting session values in Environment Helper." + e.Message);
			}
		}
	}
}
