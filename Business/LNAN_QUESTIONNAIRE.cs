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
	
	public class LNAN_QUESTIONNAIRE : SHMA.CodeVision.Business.BusinessClassBase 
	{	
		public LNAN_QUESTIONNAIRE(DataHolder dh):base(dh, "LNAN_QUESTIONNAIRE")
		{
		}

		public static string[] PrimaryKeys
		{
			get{	return new string[]{"NP1_PROPOSAL","NP2_SETNO","PPR_PRODCD","CQN_CODE"};}
		}

		public sealed override string[] GetPrimaryKeys()
		{
			return PrimaryKeys;
		}

		protected sealed override DataClassBase dataObject
		{
			get
			{
				return new LNAN_QUESTIONNAIREDB(dataHolder);
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

