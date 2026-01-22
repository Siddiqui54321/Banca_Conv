using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LPVD_VALIDATIONDETAIL : SHMA.CodeVision.Business.BusinessClassBase{	
		public LPVD_VALIDATIONDETAIL(DataHolder dh):base(dh, "LPVD_VALIDATIONDETAIL"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"PPR_PRODCD","PVL_VALIDATIONFOR","PVL_LEVEL","PVD_LEVEL","PVD_VALIDATIONFOR"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPVD_VALIDATIONDETAILDB(dataHolder);
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

