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
	public class clsIlasUtility
	{

		#region "String Related"
		public static string ReplaceIgnoreCase(string sBuffer, string sFind, string sReplace)
		{
			string regexString = "";
			foreach (char c in sFind) 
			{
				regexString += ("[" + c.ToString().ToLower()) + c.ToString().ToUpper() + "]";
			}
			return System.Text.RegularExpressions.Regex.Replace(sBuffer, regexString, sReplace);
		}
		#endregion

		#region "Forex Related Functions"
		public static double getExchangeAmount(double rate, double amount)
		{
			return rate * amount;
		}

		public static double getExchangeAmount_By_Currency(string currency, double amount)
		{
			return getExchangeAmount(getExchangeRate(currency), amount);
		}

		public static double getExchangeAmount_By_Proposal(string proposal, double amount)
		{
			return getExchangeAmount_By_Currency(getCurrCode(proposal), amount);
		}

		public static double getExchangeRate(string currCode)
		{
			try 
			{
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@PCU_CURRCODE1",currCode);
				pc.puts("@PCU_CURRCODE2",currCode);
				rowset rs = DB.executeQuery("SELECT PEX_RATE FROM PEX_EXGRATE WHERE PCU_CURRCODE=? AND PET_EXRATETYPE='B' AND PCU_BASECURR=1 AND PEX_VALUEDAT=(SELECT MAX(PEX_VALUEDAT) PEX_VALUEDAT FROM PEX_EXGRATE WHERE PCU_CURRCODE=? AND PET_EXRATETYPE='B' AND PCU_BASECURR=1) ORDER BY PEX_SERIAL DESC",pc);
				if (rs.next()) 
				{
					if (rs.getObject("PEX_RATE") == null) 
					{
						throw new Exception("Rate is null.");
					}
					else 
					{
						return rs.getDouble("PEX_RATE");
					}
				}
				else 
				{
					throw new Exception("Record not found.");
				}
			}
			catch (Exception ex) 
			{
				throw new Exception("Exchange rate: " + ex.Message);
			}
		}

		public static double getExchangeRate_By_Proposal(string proposal)
		{
			return getExchangeRate(getCurrCode(proposal));
		}

		public static string getCurrCode(string proposal)
		{
			try 
			{

				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@NP1_PROPOSAL",proposal);
				rowset rs = DB.executeQuery("SELECT PCU_CURRCODE FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'",pc);
				if (rs.next()) 
				{
					if (rs.getObject("PCU_CURRCODE") == null) 
					{
						throw new Exception("Currency is null.");
					}
					else 
					{
						return rs.getString("PCU_CURRCODE");
					}
				}
				else 
				{
					throw new Exception("Record not found.");
				}
			}
			catch (Exception ex) 
			{
				throw new Exception("Currency for exchange rate: " + ex.Message);
			}
		}
		
		public static double getIndexationCharges(string product)
		{
			try 
			{
				double indxcharges = -1;
				rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='IDXCH' AND CSD_TYPE='" + product + "'");
				if (rs.next()) 
				{   //******** Search for requested product *********
					if (rs.getObject("CSD_VALUE") == null) 
					{
						throw new Exception("Indexation Charges (IDXCH) is null in System Detail setup");
					}
					else
					{
						indxcharges = rs.getDouble("CSD_VALUE");
					}
				}
				else 
				{   //******** Search for 999 product *********
					rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='IDXCH' AND CSD_TYPE='999'");
					if (rs.next()) 
					{
						if (rs.getObject("CSD_VALUE") == null) 
						{
							throw new Exception("Indexation Charges (IDXCH) is null in System Detail setup");
						}
						else
						{
							indxcharges = rs.getDouble("CSD_VALUE");
						}
					}
					else
					{
						throw new Exception("Indexation Charges (IDXCH) is not defined in System Detail setup");
					}
				}

				if (indxcharges >= 0) 
				{
					return indxcharges;
				} 
				else 
				{
					throw new Exception("Indexation Charges (IDXCH) not found in System Detail table.");
				}
			} 
			catch (Exception ex) 
			{
				throw new ProcessException("Error in getting Indexation charges : " + ex.Message);
			} 
		}		

		#endregion

		#region "Ilas Specific"
		public static string getClientCode(string proposal)
		{
			rowset rs = DB.executeQuery("SELECT NPH_CODE FROM LNU1_UNDERWRITI WHERE NU1_LIFE='F' AND NP1_PROPOSAL='" + proposal + "' ");
			if (rs.next()) 
			{
				if (rs.getObject("NPH_CODE") == null) 
				{
					throw new Exception("Client code is null in LNU1_UNDERWRITI for proposal: " + proposal);
				}
				else 
				{
					return rs.getString("NPH_CODE");
				}
			}
			else 
			{
				throw new Exception("Client not found in LNU1_UNDERWRITI for proposal: " + proposal);
			}
		}


		public static string getClientID1FromBanca(string clientCode)
		{
			rowset rs = DB.executeQuery("SELECT NPH_IDNO,NPH_IDNO2 FROM LNPH_PHOLDER WHERE NPH_CODE='" + clientCode + "'");
			if (rs.next()) 
			{
				/*if(isNoorID())
				{
					if (rs.getObject("NPH_IDNO2") == null) 
					{
						throw new Exception("ID Number is null in LNPH_PHOLDER for Client Code: " + clientCode);
					}
					else 
					{
						return rs.getString("NPH_IDNO2");
					}
				}
				else
				{*/
					if (rs.getObject("NPH_IDNO") == null) 
					{
						throw new Exception("ID Number is null in LNPH_PHOLDER for Client Code: " + clientCode);
					}
					else 
					{
						return rs.getString("NPH_IDNO");
					}
				/*}*/
			}
			else 
			{
				throw new Exception("Client " + clientCode + " not found in LNPH_PHOLDER");
			}
		}

		public static string getClientLife(string proposal)
		{
			rowset rs = DB.executeQuery("SELECT NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL='" + proposal + "' ");
			if (rs.next()) 
			{
				if (rs.getObject("NPH_LIFE") == null) 
				{
					throw new Exception("Life code is null in LNU1_UNDERWRITI for proposal: " + proposal);
				}
				else 
				{
					return rs.getString("NPH_LIFE");
				}
			}
			else 
			{
				throw new Exception("Client not found in LNU1_UNDERWRITI for proposal: " + proposal);
			}
		}

		public static string getSex(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("SELECT NPH_SEX FROM LNU1_UNDERWRITI UR, LNPH_PHOLDER PH WHERE UR.NPH_CODE=PH.NPH_CODE AND PH.NPH_LIFE=UR.NPH_LIFE AND UR.NU1_LIFE='F' AND NP1_PROPOSAL=? ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("NPH_SEX") == null) 
				{
					throw new Exception("Sex is null in LNPH_PHOLDER for proposal: " + proposal);
				}
				else 
				{
					return rs.getString("NPH_SEX");
				}
			}
			else 
			{
				throw new Exception("Record not found in LNPH_PHOLDER for proposal: " + proposal);
			}
		}

		public static double getAnnualIncome(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("SELECT NVL(NPH_ANNUINCOME,0) NPH_ANNUINCOME FROM LNU1_UNDERWRITI UR, LNPH_PHOLDER PH WHERE UR.NPH_CODE=PH.NPH_CODE AND PH.NPH_LIFE=UR.NPH_LIFE AND UR.NU1_LIFE='F' AND NP1_PROPOSAL=? ",pc);
			if (rs.next()) 
			{
				return rs.getDouble("NPH_ANNUINCOME");
			}
			else 
			{
				return 0;
			}
		}

		public static string getOccupationalClass(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("SELECT CCL_CATEGORYCD FROM LNU1_UNDERWRITI UR, LNPH_PHOLDER PH WHERE UR.NPH_CODE=PH.NPH_CODE AND PH.NPH_LIFE=UR.NPH_LIFE AND UR.NU1_LIFE='F' AND NP1_PROPOSAL=? ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("CCL_CATEGORYCD") == null) 
				{
					throw new Exception("Occupational class is null in LNPH_PHOLDER for proposal: " + proposal);
				}
				else 
				{
					return rs.getString("CCL_CATEGORYCD");
				}
			}
			else 
			{
				throw new Exception("Record not found in LNPH_PHOLDER for proposal: " + proposal);
			}
		}
		
		public static int getEntryage(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("select NP2_AGEPREM from LNP2_POLICYMASTR WHERE NP1_PROPOSAL=? ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("NP2_AGEPREM") == null) 
				{
					throw new Exception("Age is null in LNP2_POLICYMASTR for proposal: " + proposal);
				}
				else 
				{
					return rs.getInt("NP2_AGEPREM");
				}
			}
			else 
			{
				throw new Exception("Record not found in LNPH_PHOLDER for proposal: " + proposal);
			}
		}

		public static int getEntryAge2nd(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("select NP2_AGEPREM2ND from LNP2_POLICYMASTR WHERE NP1_PROPOSAL=? ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("NP2_AGEPREM2ND") == null) 
				{
					throw new Exception("Second Life Age is null in LNP2_POLICYMASTR for proposal: " + proposal);
				}
				else 
				{
					return rs.getInt("NP2_AGEPREM2ND");
				}
			}
			else 
			{
				throw new Exception("Record not found in LNPH_PHOLDER for proposal: " + proposal);
			}
		}

		public static string getValFieldDesc(string validateField)
		{
			if (validateField == clsIlasConstant.VALIDATE_INDEXRATE) 
			{
				return "Index Rate";
			}
			else if (validateField == clsIlasConstant.VALIDATE_MATURITYAGE) 
			{
				return "Maturity Age";
			}
			else if (validateField == clsIlasConstant.VALIDATE_ENTRYAGE) 
			{
				return "Entry Age";
			}
			else if (validateField == clsIlasConstant.VALIDATE_SUMASSURED) 
			{
				return "Sum Assured";
			}
			else if (validateField == clsIlasConstant.VALIDATE_BTERM) 
			{
				return "Benefit Term";
			}
			else if (validateField == clsIlasConstant.VALIDATE_PTERM) 
			{
				return "Premium Term";
			}
			else if (validateField == clsIlasConstant.VALIDATE_FAFACTOR) 
			{
				return "Face Amount Factor";
			}
			else if (validateField == clsIlasConstant.VALIDATE_TOTPREM) 
			{
				return "Total Premium";
			}
			else if (validateField == clsIlasConstant.VALIDATE_PREMIUM) 
			{
				return "Contribution";
			}
			else if (validateField == clsIlasConstant.VALIDATE_BENRELATION)
			{
				return "Beneficiary Relation";
			}
			else 
			{
				return validateField;
			}
		}

		public static double getBMI(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("select NU1_OVERUNDERWT from LNU1_UNDERWRITI WHERE NP1_PROPOSAL=? and NPH_LIFE='D' AND NU1_LIFE='F' ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("NU1_OVERUNDERWT") == null) 
				{
					throw new Exception("BMI(NU1_OVERUNDERWT) is null in LNU1_UNDERWRITI for proposal: " + proposal);
				}
				else 
				{
					return rs.getDouble("NU1_OVERUNDERWT");
				}
			}
			else 
			{
				//throw new Exception("Record not found in LNU1_UNDERWRITI for proposal: " + proposal);
				return 0;
			}
		}
		public static double getBMI2ndLife(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("select NU1_OVERUNDERWT from LNU1_UNDERWRITI WHERE NP1_PROPOSAL=? and NPH_LIFE='D' AND NU1_LIFE='S' ",pc);
			if (rs.next()) 
			{
				if (rs.getObject("NU1_OVERUNDERWT") == null) 
				{
					throw new Exception("BMI(NU1_OVERUNDERWT) is null in LNU1_UNDERWRITI for proposal: " + proposal);
				}
				else 
				{
					return rs.getDouble("NU1_OVERUNDERWT");
				}
			}
			else 
			{
				//throw new Exception("Record not found in LNU1_UNDERWRITI for proposal: " + proposal);
				return 0;
			}
		}
		
		/*public static string getHiddenColums()
		{
			//HDCOL - Hide Column
			string HideColumn = "";
			rowset rs = DB.executeQuery("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='HDCOL' AND CSD_VALUE IN ('Y','y')");
			while(rs.next())
			{
				HideColumn += "~" + rs.getString("CSD_TYPE");
			}
			if (HideColumn.Length > 0) HideColumn = HideColumn.Substring(1);
			return HideColumn;
		}

		public static string getHiddenColumsList()
		{

			string HideColumn = "";
			rowset rs = DB.executeQuery("SELECT CUI_COLUMNID FROM LCUI_CLIENTUI WHERE CUI_SCREENID='' AND CUI_VISIBILITY IN ('H','h')");
			while(rs.next())
			{
				HideColumn += "~" + rs.getString("CUI_COLUMNID");
			}
			if (HideColumn.Length > 0) HideColumn = HideColumn.Substring(1);
			return HideColumn;
		}*/


		public static bool isPremiumTermManual()
		{
			rowset rs = DB.executeQuery("SELECT 'A' FROM LCSD_SYSTEMDTL WHERE CSH_ID='DSCOL' AND CSD_TYPE='NPR_PREMIUMTER' AND (CSD_VALUE IN ('N','n','') OR CSD_VALUE IS NULL)");
			if (rs.next())
				return true;
			else
				return false;
		}

		#endregion

		#region "APPBH - LCSD_SYSTEMDTL"
		
		#region "Commencement Date"
		public static int getMaximumCommencementDate()
		{
			int commencementDateLimit = 27;

			try
			{
				rowset rs = DB.executeQuery("select CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='MAXCOMMDATE'");
				if (rs.next())
				{
					if(rs.getObject("CSD_VALUE") != null)
					{
						if(rs.getString("CSD_VALUE").Trim() != "")
						{
							string strCommDate = rs.getString("CSD_VALUE");
							commencementDateLimit = Convert.ToInt16(strCommDate);
							if( !(commencementDateLimit > 0 && commencementDateLimit < 32))
							{
                                //commencementDateLimit = Convert.ToInt32( strCommDate);
                                commencementDateLimit = 27;
							}
						}
					}
				}
			}
			catch(Exception e)
			{
				
			}
			return commencementDateLimit;
		}
		public static string GetPCTControlParam()
		{
			string query = "Select PCT_ANYCOMMENDATE from SLILASPRD.pct_control";
           // string query = "Select PCT_ANYCOMMENDATE from SLILASJS.pct_control";

            rowset rs = DB.executeQuery(query);
			if (rs.next())
			{
				return Convert.ToString(rs.getObject("PCT_ANYCOMMENDATE"));
			}
			else
			{
				return "";
			}
		}
		public static int getAlternateCommencementDate()
		{
			int intCommencementDate = 27;
			try
			{
				rowset rs = DB.executeQuery("select CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='ALTCOMMDATE'");
				if (rs.next())
				{
					if(rs.getObject("CSD_VALUE") != null)
					{
						if(rs.getString("CSD_VALUE").Trim() != "")
						{
							string strCommDate = rs.getString("CSD_VALUE");
							intCommencementDate = Convert.ToInt16(strCommDate);
							int intMaxCommsDate = getMaximumCommencementDate();
							if( !(intCommencementDate > 0 && intCommencementDate <= intMaxCommsDate))
							{
								intCommencementDate = 27;
							}
						}
					}
				}
			}
			catch(Exception e)
			{
				
			}
			return intCommencementDate;
		}

		public static DateTime getCommencementDate(string proposal)
		{
			bool blnIndividual = true;
			String chkcomm = ""; 
			try
			{
				rowset rs = DB.executeQuery("SELECT NP2_COMMENDATE FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + proposal + "'");
				if (rs.next())
				{
					if(rs.getObject("NP2_COMMENDATE") == null)
					{
						throw new ProcessException("Commencement date is null");
					}
					else
					{
						chkcomm = rs.getObject("NP2_COMMENDATE").ToString();
						return rs.getDate("NP2_COMMENDATE");
					}
				}
				else
				{
					throw new ProcessException("Commencement date not found in LNP2_POLICYMASTR");
				}

			}
			catch(Exception e)
			{	
				throw new ProcessException(e.Message);
			}
		}

		#endregion

		
		public static string getAgeRoundingCriteria()
		{
			rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='AGE_ROUNDING'");
			if (rs.next())
			{
				if(rs.getObject("CSD_VALUE") == null)
					return "ACTUAL";
				else if(rs.getString("CSD_VALUE").ToUpper() == "FLOOR")
					return "FLOOR";
				else if(rs.getString("CSD_VALUE").ToUpper() == "CEIL")
					return "CEIL";
				else if(rs.getString("CSD_VALUE").ToUpper() == "ACTUAL")
					return "ACTUAL";
				else
					return "ACTUAL";
			}
			else
			{
				return "ACTUAL";
			}
		}
		public static bool isNoorIllustrion()
		{
			rowset rs = DB.executeQuery("SELECT 'A' FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='ILLUSTRATE_NOOR' AND CSD_VALUE IN ('Y','y')");
			if (rs.next()) 
			{
				return true;
			}
			else 
			{
				return false;
			}
		}
		

		public static string isIndividualRelation(string relationCode)
		{
			if(isIndividual_Relation(relationCode))
				return "true";
			else
				return "false";
		}
		public static bool isIndividual_Relation(string relationCode)
		{
			bool blnIndividual = true;
			try
			{
				rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='REL_NON_IND' ");
				if (rs.next())
				{
					if(rs.getObject("CSD_VALUE") != null)
					{
						if(rs.getString("CSD_VALUE").Trim() != "")
						{
							string[] relCodeList = rs.getString("CSD_VALUE").Split(',');
							for(int i=0; i<relCodeList.Length; i++)
							{
								if(relCodeList[i] == relationCode)
								{
									blnIndividual = false;
									break;
								}
							}
						}
					}
				}
			}
			catch(Exception e)
			{	
			}
			return blnIndividual;
		}
		public static bool askGuardianInfo()
		{
			bool blnAskGuardian = true;
			try
			{
				rowset rs = DB.executeQuery("SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='APPBH' AND CSD_TYPE='ASK_GUARDIAN' AND CSD_VALUE IN ('N','n') ");
				if (rs.next()) blnAskGuardian = false;
			}
			catch(Exception e){}
			return blnAskGuardian;
		}
		public static bool askFundInfo(string product)
		{
			bool blnAskFund= false;
			try
			{	//if button is hidden then don't ask for Funds Setting
				string query = SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PLAN' AND CUI_COLUMNID='PLAN_BUTTON' AND CUI_CAPTION='FundSetting' AND UPPER(CUI_VISIBILE)='Y' and  CUI_CRITERIA LIKE '%" + product+ "%' ");
				rowset rs = DB.executeQuery(query);
				if (rs.next()) 
				{
					blnAskFund = true;
				}
				else
				{	//In case of N and product not in list thats mean fund should be ask
					query = SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PLAN' AND CUI_COLUMNID='PLAN_BUTTON' AND CUI_CAPTION='FundSetting'  AND UPPER(CUI_VISIBILE)='N' ");//--and  CUI_CRITERIA LIKE '%" + product+ "%' 
					rs = DB.executeQuery(query);
					if (rs.next()) 
					{
						blnAskFund = false;
					}
					else
					{
						blnAskFund = true;
					}
				}
			}
			catch(Exception e){}
			return blnAskFund;
		}
		#endregion
	
		#region "Client UI setting"
		public static string getColumsStyle()
		{
			/********************* Proposal *********************/
			string ProposalColumns = "";
			rowset rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PROPOSAL'"));
			while(rs.next())
			{
				ProposalColumns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID")) 
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
				

			}
			if (ProposalColumns.Length > 0) 
			{
				ProposalColumns = "var ProposalColumns='" + ProposalColumns.Substring(1) + "';";
			}
			else
			{
				ProposalColumns = "var ProposalColumns='';";
			}

			/********************* Personel Detail *********************/
			string PersonelColumns = "";
			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PERSONNEL'"));
			while(rs.next())
			{
				PersonelColumns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID")) 
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));

			}
			if (PersonelColumns.Length > 0) 
			{
				PersonelColumns = "var personelColumns='" + PersonelColumns.Substring(1) + "';";
			}
			else
			{
				PersonelColumns = "var personelColumns='';";
			}

			/********************* Personel (2nd) Detail *********************/
			string Personel2Columns = "";
			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PERSONNEL2'"));
			while(rs.next())
			{
				Personel2Columns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID")) 
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
				

			}
			if (Personel2Columns.Length > 0) 
			{
				Personel2Columns = "var personel2Columns='" + Personel2Columns.Substring(1) + "';";
			}
			else
			{
				Personel2Columns = "var personel2Columns='';";
			}

			/********************* Plan Detail *********************/
			string PlanColumns = "";
			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PLAN'"));
			while(rs.next())
			{
				PlanColumns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID"))
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
			}
			if (PlanColumns.Length > 0) 
			{
				PlanColumns = "var planColumns='" + PlanColumns.Substring(1) + "';";
			}
			else
			{
				PlanColumns = "var planColumns='';";
			}

			/********************* Plan Buttons *********************/
			string PlanButtons = "";
			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PLAN_BUTTON'"));
			while(rs.next())
			{
				PlanButtons += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID"))
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
			}
			if (PlanButtons.Length > 0) 
			{
				PlanButtons = "var planButtons='" + PlanButtons.Substring(1) + "';";
			}
			else
			{
				PlanButtons = "var planButtons='';";
			}


//			/********************* Riders Deatil *********************/
			string RiderColumns = "";
//			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='RIDER'"));
//			while(rs.next())
//			{
//				RiderColumns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID"))
//					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
//					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
//					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
//					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
//					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
//					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
//					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
//			}
//			if (RiderColumns.Length > 0) 
//			{
//				RiderColumns = "var riderColumns='" + RiderColumns.Substring(1) + "';";
//			}
//			else
//			{
//				RiderColumns = "var riderColumns='';";
//			}

			/********************* Policy Acceptance *********************/
			string AcceptanceColumns = "";
			rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='ACCEPTANCE'"));
			while(rs.next())
			{
				AcceptanceColumns += "~" + (rs.getObject("CUI_COLUMNID") == null ? "" : rs.getString("CUI_COLUMNID")) 
					+  "," + (rs.getObject("CUI_VISIBILE") == null ? "" : rs.getString("CUI_VISIBILE"))
					+  "," + (rs.getObject("CUI_DISABLE")  == null ? "" : rs.getString("CUI_DISABLE"))
					+  "," + (rs.getObject("CUI_CAPTION")  == null ? "" : rs.getString("CUI_CAPTION"))
					+  "," + (rs.getObject("CUI_DEFVALUE") == null ? "" : rs.getString("CUI_DEFVALUE"))
					+  "," + (rs.getObject("CUI_MANDATORY")== null ? "" : rs.getString("CUI_MANDATORY"))
					+  "," + (rs.getObject("CUI_FORMAT")   == null ? "" : rs.getString("CUI_FORMAT"))
					+  "," + (rs.getObject("CUI_CRITERIA") == null ? "" : rs.getString("CUI_CRITERIA").Replace(",", " "));
			}
			if (AcceptanceColumns.Length > 0) 
			{
				AcceptanceColumns = "var acceptanceColumns='" + AcceptanceColumns.Substring(1) + "';";
			}
			else
			{
				AcceptanceColumns = "var acceptanceColumns='';";
			}
			return ProposalColumns + PersonelColumns + Personel2Columns + PlanColumns + PlanButtons + RiderColumns + AcceptanceColumns;
		}

		public static string getDisableColums()
		{
			//HDCOL - Hide Column
			string column = "";
			rowset rs = DB.executeQuery("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='DSCOL' AND CSD_VALUE IN ('Y','y')");
			while(rs.next())
			{
				column += "~" + rs.getString("CSD_TYPE");
			}

			if (column.Length > 0) column = column.Substring(1);

			return column;
		}

		public static string getEnableColums()
		{
			string column = "";
			rowset rs = DB.executeQuery("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='DSCOL' AND (CSD_VALUE IN ('N','n','') OR CSD_VALUE IS NULL)");
			while(rs.next())
			{
				column += "~" + rs.getString("CSD_TYPE");
			}

			if (column.Length > 0) column = column.Substring(1);

			return column;
		}

		public static string getHiddenTabs()
		{
			//HDTAB - Hide Tab
			string HideTabs = "";
			rowset rs = DB.executeQuery("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='HDTAB' AND CSD_VALUE IN ('Y','y')");
			while(rs.next())
			{
				HideTabs += "~" + rs.getString("CSD_TYPE");
			}

			if(ace.Ace_General.IsIllustration())
			{
				HideTabs += "~MedicalTab" ; 
				HideTabs += "~acceptanceTab"; 
 				HideTabs += "~rptAdvice"; 
 				HideTabs += "~rptProfile"; 
 				HideTabs += "~rptPolicy";
                HideTabs += "~rptPayHis";   //chg-his
                HideTabs += "~rptPolSta";    //chg-his
                HideTabs += "~rptPersonalInfo"; 
			}
			else if(ace.Ace_General.IsBancaasurance())
			{
				HideTabs += "~TargetValues" ; 
			}


			if (HideTabs.Length > 0) HideTabs = HideTabs.Substring(1);

			return HideTabs;
		}

		public static string getHiddenButtons()
		{
			//HDBTN - Hide Button
			string HiddenButtons = "";
			rowset rs = DB.executeQuery("SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='HDBTN' AND CSD_VALUE IN ('Y','y')");
			while(rs.next())
			{
				HiddenButtons += "~" + rs.getString("CSD_TYPE");
			}

			if (HiddenButtons.Length > 0) HiddenButtons = HiddenButtons.Substring(1);

			return HiddenButtons;
		}

		public static bool isNoorID()
		{
			rowset rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT 'A' FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='PERSONNEL' AND CUI_COLUMNID='txtCNIC_VALUE' AND CUI_FORMAT='NOORID' "));
			if(rs.next())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	
		public static bool isManualAdjustmentAllowed()
		{
			bool found = false;
			rowset rs = DB.executeQuery("select CSD_VALUE from LCSD_SYSTEMDTL where csh_id = 'APPBH' AND CSD_TYPE ='MANADJPLANS' ");
			if(rs.next())
				if(rs.getObject("CSD_VALUE") != null)
					if(rs.getString("CSD_VALUE").Trim().Length > 0 )
						found = true;

			return found;
		}
		
		/*public static string getManualAjustmentPlans()
		{
			string queryList ="";
			rowset rs = DB.executeQuery("select CSD_VALUE from LCSD_SYSTEMDTL where csh_id = 'APPBH' AND CSD_TYPE ='MANADJPLANS' ");
			if(rs.next())
				if(rs.getObject("CSD_VALUE") != null)
					if(rs.getString("CSD_VALUE").Trim().Length > 0 )
					{
						string[] val = rs.getString("CSD_VALUE").Trim().Split(',');
						for (int i = 0; i < val.Length; i++)
						{
							val[i] = "'" + val[i] + "'";
						}

						queryList = string.Join(",", val);
					}
			return queryList;
		}*/

		public static string getListFromSysDetail(string sysId, string sysType, bool numeric)
		{
			string singleQoute = (numeric == false) ? "'" : "";
			string queryList ="";

			rowset rs = DB.executeQuery("select CSD_VALUE from LCSD_SYSTEMDTL where csh_id = '" + sysId + "' AND CSD_TYPE ='" + sysType + "' ");
			if(rs.next())
				if(rs.getObject("CSD_VALUE") != null)
					if(rs.getString("CSD_VALUE").Trim().Length > 0 )
					{
						string[] val = rs.getString("CSD_VALUE").Trim().Split(',');
						for (int i = 0; i < val.Length; i++)
						{
							val[i] = singleQoute + val[i] + singleQoute;
						}

						queryList = string.Join(",", val);
					}
			return "(" + queryList + ")";
		}

		
		
		#endregion
	}
}
