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
	
	public class LUCH_USERCHANNEL : SHMA.CodeVision.Business.BusinessClassBase 
	{
		
		public LUCH_USERCHANNEL(DataHolder dh):base(dh, "LUCH_USERCHANNEL"){
		}

		//static string[] pKeys = {"CCH_CODE","USE_USERID"};
		
		public static string[] PrimaryKeys{
			get{	return new string[]{"USE_USERID","CCH_CODE","CCD_CODE","CCS_CODE"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LUCH_USERCHANNELDB(dataHolder);
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

