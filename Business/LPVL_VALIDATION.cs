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
	
	public class LPVL_VALIDATION : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LPVL_VALIDATION(DataHolder dh):base(dh, "LPVL_VALIDATION"){
		}

		//static string[] pKeys = {"PVL_VALIDATIONFOR","PPR_PRODCD","PVL_LEVEL"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"PVL_VALIDATIONFOR","PPR_PRODCD","PVL_LEVEL"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPVL_VALIDATIONDB(dataHolder);
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

