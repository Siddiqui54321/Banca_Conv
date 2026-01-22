using System;
using System.Data;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	public class ValidationUtility
	{
		public static bool isNewValidation()
		{
			//APPBH - NEWVALIDATION
			//rowset rs = DB.executeQuery("SELECT 'A' FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='NEWVALIDATION' AND CSD_VALUE IN ('Y','y')");
			rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='NEWVALIDATION'");
			if (rs.next())
			{
				if(rs.getString("CSD_VALUE").ToUpper() == "N")
					return false;
				else
					return true;
			}
			else
			{
				return true;
			}
		}

		public static bool isPolicyNumberNeedForSubStandard()
		{
			try
			{
				//APPBH - SUBSTANDARD_POL (Issue Policy For Sub Standar Policy)
				//rowset rs = DB.executeQuery("SELECT 'A' FROM LCSD_SYSTEMDTL WHERE CSH_ID='POLNO' AND CSD_TYPE='SUB_STANDARD' AND CSD_VALUE IN ('Y','y')");
				rowset rs = DB.executeQuery("SELECT 'A' FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='SUBSTANDARD_POL' AND CSD_VALUE IN ('Y','y')");
				if (rs.next())
					return true;
				else
					return false;		
			}
			catch(Exception e)
			{
				return false;
			}
		}
		public static bool isPostingFound()
		{
			try
			{				
				rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='CPU' AND UPPER(CSD_STATUS) = 'Y'");
				if (rs.next())
					return true;
				else
					return false;		
			}
			catch(Exception e)
			{
				return false;
			}
		}

		public static bool genPolicyBeforeTransferToIlas()
		{
			//APPBH - POLICYNO
			rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='POLICYNO' ");
			if (rs.next())
			{
				if(rs.getString("CSD_VALUE").ToUpper() == "AT")
				{
					return false;
				}
				else if(rs.getString("CSD_VALUE").ToUpper() == "BT")
				{
					return true;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}

		public static bool genPolicyAfterTransferToIlas()
		{
			//APPBH - POLICYNO
			rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='POLICYNO' ");
			if (rs.next())
			{
				if(rs.getString("CSD_VALUE").ToUpper() == "AT")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}


		public static string getApprovePhrase()
		{
			string ApprovePhrase = getFieldValue("APPROVED");
			if(ApprovePhrase == "")
			{
				ApprovePhrase = "Standard";
			}
			return ApprovePhrase;
		}

		public static string getPostponedPhrase()
		{
			string PostponedPhrase = getFieldValue("POSTPONED");
			if(PostponedPhrase == "")
			{
				PostponedPhrase = "Refer to Company";
			}
			return PostponedPhrase;
		}

		public static string getDeclinedPhrase()
		{
			string DeclinedPhrase = getFieldValue("DECLINED");
			if(DeclinedPhrase == "")
			{
				DeclinedPhrase = "Sub Standard";
			}
			return DeclinedPhrase;
		}

		private static string getFieldValue(string decision)
		{
			rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='" + decision + "' ");
			if (rs.next())
			{
				if(rs.getObject("CSD_VALUE") != null)
				{
					return rs.getString("CSD_VALUE").ToUpper();
				}
				else
				{
					return "";
				}
			}
			else
			{
				return "";
			}
		}

		public static bool isManualProposal()
		{
			rowset rs = DB.executeQuery("select 'A' from lcui_clientui where CUI_SCREENID='PROPOSAL' AND CUI_COLUMNID='txtNP1_PROPOSAL' AND CUI_DISABLE IN('N','n') and CUI_VISIBILE IN ('','Y','y')");
			if (rs.next())
				return true;
			else
				return false;		
		}

		public static bool isPlan(string product)
		{
			rowset rsRider = DB.executeQuery("SELECT 'A' FROM LPPR_PRODUCT WHERE PPR_PRODCD='" + product + "' AND PPR_BASRIDR='B' ");
			if (rsRider.next())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool isRider(string product)
		{
			rowset rsRider = DB.executeQuery("SELECT 'A' FROM LPPR_PRODUCT WHERE PPR_PRODCD='" + product + "' AND PPR_BASRIDR='R' ");
			if (rsRider.next())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
