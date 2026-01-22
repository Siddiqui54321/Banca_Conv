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
	
	public class PEX_EXGRATE : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public PEX_EXGRATE(DataHolder dh):base(dh, "PEX_EXGRATE"){
		}

		//static string[] pKeys = {"PCU_BASECURR","PCU_CURRCODE","PET_EXRATETYPE","PEX_VALUEDAT","PEX_SERIAL"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"PCU_BASECURR","PCU_CURRCODE","PET_EXRATETYPE","PEX_VALUEDAT","PEX_SERIAL"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new PEX_EXGRATEDB(dataHolder);
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

