using System;
//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;


namespace ace
{
	/// <summary>
	/// Summary description for ValidationClass.
	/// </summary>
	public class ValidationClass
	{
		public ValidationClass()
		{
	
		}
		//Check Premium Exsists
		public void CheckPremiumExsist(string NP1_PROPOSAL)
		{
			//Sample Code.
		
			rowset CHECK_PREMIUM = DB.executeQuery("select * FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"'");
			if (CHECK_PREMIUM.next())
			{

			}
		}

		public static  bool UpdateInAnyFormAllowed()
		{
			bool rtrnValue=true;
			rowset rs= DB.executeQuery("select NP1_SELECTED FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");
			if(rs.next())
			{
				object np1Select = rs.getObject("NP1_SELECTED");
				if(np1Select!=null)
				{
					rtrnValue=false;
				}
			}
			return rtrnValue;
		}




		#region "Date Validation"
		public static string Validate_Date()
		{
			try 
			{
				const string strDateQry = "SELECT TO_CHAR(SYSDATE,'dd/mm/yyyy') DB_DATE FROM DUAL"; 			
				rowset rstUser = DB.executeQuery(strDateQry);
				if (rstUser.next())
				{
					if (rstUser.getObject("DB_DATE").ToString() == System.DateTime.Now.ToString("dd/MM/yyyy"))
					{ 
						return "1," + rstUser.getObject("DB_DATE").ToString(); 
					}
					else { return "0,"	+ rstUser.getObject("DB_DATE").ToString(); }
				}
				else { return "0"; }	
			}
			catch(Exception ex){ return ex.Message; }
		}
		#endregion

	}

}
