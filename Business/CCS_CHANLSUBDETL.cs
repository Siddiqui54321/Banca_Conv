using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class CCS_CHANLSUBDETL : SHMA.CodeVision.Business.BusinessClassBase{	
		public CCS_CHANLSUBDETL(DataHolder dh):base(dh, "CCS_CHANLSUBDETL"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"CCH_CODE","CCD_CODE","CCS_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new CCS_CHANLSUBDETLDB(dataHolder);
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

