using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
//using System.Data.OracleClient;

using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;


namespace ace
{
	public class IllustrateNoor
	{
		//******************** Noor Takaful Products *********************
		private const string NOOR_SMART_SAVE     = "430";
		private const string NOOR_SECURE_INCOME  = "450";
		private const string NOOR_SECURE_INVEST  = "460";
		private const string NOOR_EDUCARE_PLAN   = "470";
		private const string NOOR_EXECUTIVE_CARE = "480";

		public void callNoorIllustration(string proposal) 
		{
			if(clsIlasUtility.isNoorIllustrion())
			{
				//************* Load all neccessary fields first *******************
				string product = Ace_General.getPPR_PRODCD(proposal);
				string ExclIncl= "";
				if(product == NOOR_EXECUTIVE_CARE || product == NOOR_SECURE_INVEST)
				{
					ExclIncl = "Y";
				}
				else
				{
					ExclIncl = "N";
				}

				//Load Basic Plan information in Rowset	
				rowset rsPlan = DB.executeQuery("SELECT * FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + proposal + "' AND PPR_PRODCD='" + product + "' AND NVL(NPR_BASICFLAG,'N') = 'Y'");
				rsPlan.next();

				//Load Yield values from System Detail
				int yield1=0,yield2=0,yield3=0;
				rowset rsYield = DB.executeQuery("SELECT * FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND UPPER(CSD_TYPE) IN ('YIELD1','YIELD2','YIELD3')");
				if(rsYield.size() != 3)
				{
					throw new ProcessException("Yield1, Yield2, Yield3 must be defined in System Detail Setup(APPBH).");
				}

				while(rsYield.next())
				{
					try
					{
						if(rsYield.getObject("CSD_VALUE")==null)
						{
							throw new ProcessException(rsYield.getString("CSD_TYPE") + " is null in System Detail Setup(APPBH).");
						}
						
						if (rsYield.getString("CSD_TYPE").ToUpper() == "YIELD1")
							yield1 = Convert.ToInt16(rsYield.getString("CSD_VALUE"));
						else if (rsYield.getString("CSD_TYPE").ToUpper() == "YIELD2")
							yield2 = Convert.ToInt16(rsYield.getString("CSD_VALUE"));
						else if (rsYield.getString("CSD_TYPE").ToUpper() == "YIELD3")
							yield3 = Convert.ToInt16(rsYield.getString("CSD_VALUE"));
					}
					catch(Exception e)
					{
						throw new ProcessException("Error in getting Yield : " + e.Message);
					}
				}

				//*************** Create object and its properties ***************
				try
				{
					NoorIllustrate.ILLUSTRAT obj = new NoorIllustrate.ILLUSTRAT();
					obj.Proposal         = proposal;
					obj.ProductCode      = product;
					obj.Age              = clsIlasUtility.getEntryage(proposal);
					obj.SecondLifeAge    = (product == NOOR_EDUCARE_PLAN) ? clsIlasUtility.getEntryAge2nd(proposal) : 0;//if produtct educare 470
					obj.Gender           = clsIlasUtility.getSex(proposal);
					obj.ExchangeRate     = Convert.ToSingle(clsIlasUtility.getExchangeRate_By_Proposal(proposal));//pex_exrat currency
					obj.Excl_Incl        = ExclIncl;
					obj.IndexSA          = 0; 
					obj.IndexCharges     = Convert.ToInt32(clsIlasUtility.getIndexationCharges(product));//.35 -- must in system para
					obj.IndexPremium     = Convert.ToInt32((rsPlan.getObject("NPR_INDEXRATE") == null) ? 0 : rsPlan.getDouble("NPR_INDEXRATE"));
					obj.Mode             = rsPlan.getString("CMO_MODE");
					obj.Premium          = Convert.ToSingle((rsPlan.getObject("NPR_TOTPREM") == null) ? 0 : rsPlan.getDouble("NPR_TOTPREM"));
					obj.Terms            = Convert.ToInt16((rsPlan.getObject("NPR_BENEFITTERM") == null) ? 0 : rsPlan.getDouble("NPR_BENEFITTERM"));
					obj.KeyManSumAssured = Convert.ToSingle(rsPlan.getDouble("NPR_SUMASSURED"));
					obj.OccupationalClass= clsIlasUtility.getOccupationalClass(proposal);
				
					//Generate Illustration now
					obj.GENERATE(yield1, yield2, yield3,false);
					//obj.GENERATE(yield1, yield2, yield3);
				}
				catch(Exception e)
				{
					throw new ProcessException("Error in Illustration. " + e.Message);
				}

				
				//*************** Finally Generate Allocation Charges - Used in Report ***************
				try
				{
					string allocCharges = NoorIllustrate.clsSevice.getAllocationChargesString(proposal);
					DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_COMMENTS='" + allocCharges + "' WHERE NP1_PROPOSAL='" + proposal + "'");
				}
				catch(Exception e)
				{
					throw new ProcessException("Error in generating Allocation Charges. " + e.Message);
				}

			}
		}
	}
}
