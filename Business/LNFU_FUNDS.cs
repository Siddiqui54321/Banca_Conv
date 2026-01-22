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
	
	public class LNFU_FUNDS : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LNFU_FUNDS(DataHolder dh):base(dh, "LNFU_FUNDS"){
		}

		//static string[] pKeys = {"NP1_PROPOSAL","NP2_SETNO","PPR_PRODCD","CFU_FUNDCODE"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","NP2_SETNO","PPR_PRODCD","CFU_FUNDCODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNFU_FUNDSDB(dataHolder);
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

