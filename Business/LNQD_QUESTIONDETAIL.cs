using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LNQD_QUESTIONDETAIL : SHMA.CodeVision.Business.BusinessClassBase
	{	
		public LNQD_QUESTIONDETAIL(DataHolder dh):base(dh, "LNQD_QUESTIONDETAIL"){}

		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","NP2_SETNO","PPR_PRODCD","CQN_CODE","CCN_COLUMNID"};}
		}

		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}

		protected sealed override DataClassBase dataObject{
			get{
				return new LNQD_QUESTIONDETAILDB(dataHolder);
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

