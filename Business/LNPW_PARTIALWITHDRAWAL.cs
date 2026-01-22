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
	
	public class LNPW_PARTIALWITHDRAWAL : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LNPW_PARTIALWITHDRAWAL(DataHolder dh):base(dh, "LNPW_PARTIALWITHDRAWAL"){
		}

		//static string[] pKeys = {"NPW_YEAR","NP1_PROPOSAL"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"NPW_YEAR","NP1_PROPOSAL"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNPW_PARTIALWITHDRAWALDB(dataHolder);
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

