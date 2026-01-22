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
	
	public class LNPU_PURPOSE : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LNPU_PURPOSE(DataHolder dh):base(dh, "LNPU_PURPOSE"){
		}

		//static string[] pKeys = {"NP1_PROPOSAL","CPU_CODE"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","CPU_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNPU_PURPOSEDB(dataHolder);
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

