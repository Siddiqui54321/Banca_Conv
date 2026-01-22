using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LPQN_QUESTIONNAIRE : SHMA.CodeVision.Business.BusinessClassBase{	
		public LPQN_QUESTIONNAIRE(DataHolder dh):base(dh, "LPQN_QUESTIONNAIRE"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"PPR_PRODCD","CQN_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LPQN_QUESTIONNAIREDB(dataHolder);
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

