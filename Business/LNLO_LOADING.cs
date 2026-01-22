using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LNLO_LOADING : SHMA.CodeVision.Business.BusinessClassBase{	
		public LNLO_LOADING(DataHolder dh):base(dh, "LNLO_LOADING"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","NP2_SETNO","PPR_PRODCD","NLO_TYPE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNLO_LOADINGDB(dataHolder);
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

