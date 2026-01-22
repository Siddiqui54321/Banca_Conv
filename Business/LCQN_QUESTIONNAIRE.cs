using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LCQN_QUESTIONNAIRE : SHMA.CodeVision.Business.BusinessClassBase{	
		public LCQN_QUESTIONNAIRE(DataHolder dh):base(dh, "LCQN_QUESTIONNAIRE"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"CQN_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LCQN_QUESTIONNAIREDB(dataHolder);
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

