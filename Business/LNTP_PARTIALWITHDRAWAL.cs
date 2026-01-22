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
	
	public class LNTP_PARTIALWITHDRAWAL : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LNTP_PARTIALWITHDRAWAL(DataHolder dh):base(dh, "LNTP_PARTIALWITHDRAWAL"){
		}

		//static string[] pKeys = {"NP1_PROPOSAL","NTP_COLCODE"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL","NTP_COLCODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNTP_PARTIALWITHDRAWALDB(dataHolder);
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

