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
	
	public class LNBF_BENEFICIARY : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LNBF_BENEFICIARY(DataHolder dh):base(dh, "LNBF_BENEFICIARY"){
		}

		//static string[] pKeys = {"NP1_PROPOSAL","NBF_BENEFCD"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","NBF_BENEFCD"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNBF_BENEFICIARYDB(dataHolder);
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

