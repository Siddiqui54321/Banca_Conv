using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LPCH_CHANNEL : SHMA.CodeVision.Business.BusinessClassBase{	
		public LPCH_CHANNEL(DataHolder dh):base(dh, "LPCH_CHANNEL"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"CCH_CODE","CCD_CODE","CCS_CODE","PPR_PRODCD"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPCH_CHANNELDB(dataHolder);
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

