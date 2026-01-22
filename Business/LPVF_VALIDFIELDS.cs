using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LPVF_VALIDFIELDS : SHMA.CodeVision.Business.BusinessClassBase{	
		public LPVF_VALIDFIELDS(DataHolder dh):base(dh, "LPVF_VALIDFIELDS"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"PPR_PRODCD","PVF_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPVF_VALIDFIELDSDB(dataHolder);
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

