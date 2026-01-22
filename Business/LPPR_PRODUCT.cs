using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LPPR_PRODUCT : SHMA.CodeVision.Business.BusinessClassBase{	
		public LPPR_PRODUCT(DataHolder dh):base(dh, "LPPR_PRODUCT"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"PPR_PRODCD"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPPR_PRODUCTDB(dataHolder);
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

