using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business 
{
	/// <summary>
	/// Summary description for Tax.
	/// </summary>
	
	public class LUCN_USERCOUNTRY : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LUCN_USERCOUNTRY(DataHolder dh):base(dh, "LUCN_USERCOUNTRY"){
		}

		//static string[] pKeys = {"CCN_CTRYCD","USE_USERID"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"CCN_CTRYCD","USE_USERID"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LUCN_USERCOUNTRYDB(dataHolder);
			}
		}

		public override void Add(NameValueCollection columnNameValue, NameValueCollection allFields, string EntityID) 
		{
		   	
		   	
		   	base.Add(columnNameValue,allFields,EntityID);
		   	
		}
		
		public override void Add(NameValueCollection columnNameValue,string fieldCombination,string valueCombination)
		{
		        			  
		        
		        base.Add(columnNameValue,fieldCombination,valueCombination);
		}

	}
}

