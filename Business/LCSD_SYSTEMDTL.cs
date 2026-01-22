using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LCSD_SYSTEMDTL : SHMA.CodeVision.Business.BusinessClassBase{	
		
		public LCSD_SYSTEMDTL(DataHolder dh):base(dh, "LCSD_SYSTEMDTL")
		{
		}

		public static string[] PrimaryKeys
		{
			get
			{
				return new string[]{"CSH_ID","CSD_TYPE"};
			}
		}

		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LCSD_SYSTEMDTLDB(dataHolder);
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

