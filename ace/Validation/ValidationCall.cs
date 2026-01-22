using System;
using System.Data;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OleDb;
namespace ace
{
	/// <summary>
	/// ValidationCall class is responsible to load all necessary fields to loade in collection,
	/// And call the real validation process.
	/// </summary>
	public class ValidationCall
	{
		private VFC vfCollection;
		private FieldRegister vfRegister;
		public ValidationCall()	{this.vfCollection = new VFC();}

		#region "Plan / Base Product"
		public void validatePlan(string proposal, int setNo, string product)
		{
			try
			{	
				if (ValidationUtility.isNewValidation())
				{
					setValidationFieldsForPlan(proposal, setNo, product);
					setMoreFieldsInCollection();
					string error = callValidatePlanNow(product);
					if (error.Length > 0)
					{
						throw new Exception(error);
					}
				}
			}
			catch(Exception e)
			{
				//throw new ProcessException("<h1 style='color:red'><u>Plan Validation</u></h1>" + e.Message);
				//throw new ProcessException("<CENTER><H3 style=\"COLOR: #336699\"><B><U>Plan Validation Error</U></B></H3></CENTER>" + e.Message);
				throw new ProcessException(e.Message);
			}
		}

		private string callValidatePlanNow(string product)
		{
			string validationError = "";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@PPR_PRODCD", product);
			rowset rs = DB.executeQuery("SELECT PVL_VALIDRANGE FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR='" + clsIlasConstant.CALLVALIDATION + "' AND PPR_PRODCD=? AND PVL_VALIDRANGE NOT IN ('MANDRIDER')", pc);
			
			while(rs.next())
			{
				string validateField = rs.getString("PVL_VALIDRANGE");
				if (validateField != "")
				{
					string columnName = vfRegister.getFieldName(validateField);
					object inputValue = vfCollection.getObject(columnName);
					try
					{
						Validation objValidation = new Validation(product, validateField, inputValue, vfCollection, vfRegister);
						objValidation.validateProduct();
					}
					catch(Exception e)
					{
						//validationError += "<p><pre>   * " + e.Message + "</pre></p>";
						//validationError += "\\n   *   " + e.Message;
						validationError += e.Message;
					}
				}
			}

			if(validationError.Trim().Length > 0)
			{
				rowset rsProduct = DB.executeQuery("SELECT PPR_DESCR FROM LPPR_PRODUCT LPPR WHERE PPR_PRODCD='"+product+"'");
				rsProduct.next();
				string productInfo = rsProduct.getString("PPR_DESCR") + " - " + product;
				//validationError = "<BR><FONT style=\"BACKGROUND-COLOR: #3BB9FF\" size=2 ><B><U>" + productInfo + "</FONT></U></B>" + validationError;
				validationError = "<BR><FONT style=\"COLOR: #336699\" size=2 ><B><U>" + productInfo + "</FONT></U></B><br>" + validationError;
				
			}
			return validationError;
		}

		private void setValidationFieldsForPlan(string proposal, int setNo, string product)
		{
			string query = "";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();

			if(product=="")
			{   //Pick base Product information if product not known 
				query = "SELECT * FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=? AND NPR_BASICFLAG='Y' ";
				pc.puts("@NP1_PROPOSAL",proposal);
			}
			else
			{   //Pick base Product 
				query = "SELECT * FROM LNPR_PRODUCT WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? ";
				pc.puts("@NP1_PROPOSAL",proposal);
				pc.puts("@NP2_SETNO",setNo);
				pc.puts("@PPR_PRODCD",product);
			}
			rowset rs = DB.executeQuery(query,pc);
			if (rs.next())
			{
				/******* Base Product Columns ********/
				vfCollection.setValue("NP1_PROPOSAL", proposal);
				//Base Product
				vfCollection.setValue("PPR_PRODCDB", rs.getString("PPR_PRODCD"));
				//Current Product (might be Rider or Base Product itself)
				vfCollection.setValue("PPR_PRODCD", rs.getString("PPR_PRODCD"));
				vfCollection.setValue("NPR_BENEFITTERM", rs.getDouble("NPR_BENEFITTERM"));
				//vfCollection.setValue("NPR_PREMIUMTER", (rs.getDouble("NPR_PREMIUMTER")==0 ? rs.getDouble("NPR_BENEFITTERM") : rs.getDouble("NPR_PREMIUMTER")));
				vfCollection.setValue("NPR_PREMIUMTER", rs.getDouble("NPR_PREMIUMTER"));
				vfCollection.setValue("NPR_SUMASSURED", rs.getDouble("NPR_SUMASSURED"));
				vfCollection.setValue("NPR_PREMIUM", rs.getDouble("NPR_PREMIUM"));
				vfCollection.setValue("NPR_LOADING", rs.getObject("NPR_LOADING") == null ? 0.0 : rs.getDouble("NPR_LOADING"));
				vfCollection.setValue("NPR_TOTPREM", rs.getObject("NPR_TOTPREM") == null ? 0.0 : rs.getDouble("NPR_TOTPREM"));
				vfCollection.setValue("NPR_PREMRATE", rs.getObject("NPR_PREMRATE") == null ? 0.0 : rs.getDouble("NPR_PREMRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE", rs.getObject("NPR_EXTMORTRATE") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE"));
				vfCollection.setValue("NPR_ENDORSMT", rs.getString("NPR_ENDORSMT"));
				vfCollection.setValue("NPR_LIFE", rs.getString("NPR_LIFE"));
				vfCollection.setValue("NPR_SAR", rs.getObject("NPR_SAR") == null ? 0.0 : rs.getDouble("NPR_SAR"));
				vfCollection.setValue("NPR_ETASA", rs.getObject("NPR_ETASA") == null ? 0.0 : rs.getDouble("NPR_ETASA"));
				vfCollection.setValue("NPR_PAIDUPSA", rs.getObject("NPR_PAIDUPSA") == null ? 0.0 : rs.getDouble("NPR_PAIDUPSA"));
				vfCollection.setValue("NPR_ETATERM", rs.getObject("NPR_ETATERM") == null ? 0.0 : rs.getDouble("NPR_ETATERM"));
				vfCollection.setValue("NPR_PURENDOWMENT", rs.getObject("NPR_PURENDOWMENT") == null ? 0.0 : rs.getDouble("NPR_PURENDOWMENT"));
				vfCollection.setValue("NPR_INDEXATION", rs.getString("NPR_INDEXATION"));
				vfCollection.setValue("NPR_INDEXRATE", rs.getDouble("NPR_INDEXRATE"));
				vfCollection.setValue("NPR_ETAPREMIUM", rs.getObject("NPR_ETAPREMIUM") == null ? 0.0 : rs.getDouble("NPR_ETAPREMIUM"));
				vfCollection.setValue("NPR_CASHVALUE", rs.getObject("NPR_CASHVALUE") == null ? 0.0 : rs.getDouble("NPR_CASHVALUE"));
				vfCollection.setValue("NPR_FACTOR", rs.getObject("NPR_FACTOR") == null ? 0.0 : rs.getDouble("NPR_FACTOR"));
				vfCollection.setValue("NPR_ETADAYS", rs.getObject("NPR_ETADAYS") == null ? 0.0 : rs.getDouble("NPR_ETADAYS"));
				vfCollection.setValue("NPR_EDUNITS", rs.getDouble("NPR_EDUNITS"));
				vfCollection.setValue("NPR_AGEPREM", rs.getObject("NPR_AGEPREM") == null ? 0.0 : rs.getInt("NPR_AGEPREM"));
				vfCollection.setValue("NPR_AGEPREM2ND", rs.getObject("NPR_AGEPREM2ND") == null ? 0.0 : rs.getInt("NPR_AGEPREM2ND"));
				//CONVERT
				//NPR_COMMLOADING
				//NPR_INCLUDELOADINNIV
				//NPR_MATURITYDATE
				vfCollection.setValue("NPR_AGEDIFFERENCE", rs.getObject("NPR_AGEDIFFERENCE") == null ? 0.0 : rs.getDouble("NPR_AGEDIFFERENCE"));
				vfCollection.setValue("CCB_CODE", rs.getString("CCB_CODE"));
				//NPR_PAID
				vfCollection.setValue("NPR_INTERESTRATE", rs.getObject("NPR_INTERESTRATE") == null ? 0.0 : rs.getDouble("NPR_INTERESTRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE2", rs.getObject("NPR_EXTMORTRATE2") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE2"));
				vfCollection.setValue("NP2_AGEPREM", rs.getObject("NP2_AGEPREM") == null ? 0.0 : rs.getInt("NP2_AGEPREM"));
				vfCollection.setValue("NPR_PAIDUPTOAGE", rs.getObject("NPR_PAIDUPTOAGE") == null ? 0.0 : rs.getInt("NPR_PAIDUPTOAGE"));
				vfCollection.setValue("CMO_MODE", rs.getString("CMO_MODE"));
				vfCollection.setValue("NPR_BASICPRMANNUAL", rs.getObject("NPR_BASICPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_BASICPRMANNUAL"));
				vfCollection.setValue("NPR_EXCESPRMANNUAL", rs.getObject("NPR_EXCESPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESPRMANNUAL"));
				//NPR_SELECTED
				vfCollection.setValue("NPR_AGEPREM2", rs.getObject("NPR_AGEPREM2") == null ? 0.0 : rs.getInt("NPR_AGEPREM2"));
				vfCollection.setValue("PCU_CURRCODE", rs.getString("PCU_CURRCODE"));
				vfCollection.setValue("NPR_EXCESSPREMIUM", rs.getObject("NPR_EXCESSPREMIUM") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM"));
				vfCollection.setValue("NPR_EXCESSPREMIUM_ACTUAL", rs.getObject("NPR_EXCESSPREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUM_FC", rs.getObject("NPR_PREMIUM_FC") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_FC"));
				vfCollection.setValue("NPR_PREMIUM_AV", rs.getObject("NPR_PREMIUM_AV") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_AV"));
				vfCollection.setValue("NPR_PREMIUM_ACTUAL", rs.getObject("NPR_PREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUMDISCOUNT", rs.getObject("NPR_PREMIUMDISCOUNT") == null ? 0.0 : rs.getDouble("NPR_PREMIUMDISCOUNT"));
				
				

				/****** More fields need to be fill in collection *****/
				//vfCollection.setValue("PEX_RATE", clsIlasUtility.getExchangeRate(rs.getString("PCU_CURRCODE")));
				//vfCollection.setValue("NPH_SEX", clsIlasUtility.getSex(proposal));
				//vfCollection.setValue("NPH_ANNUINCOME", clsIlasUtility.getAnnualIncome(proposal));
				//vfCollection.setValue("CCL_CATEGORYCD", clsIlasUtility.getOccupationalClass(proposal));
				//vfCollection.setValue("NP2_AGEPREM", clsIlasUtility.getEntryage(proposal));
				//try
				//{
				//	vfCollection.setValue("NP2_AGEPREM2ND", clsIlasUtility.getEntryAge2nd(proposal));
				//}
				//catch(Exception e)
				//{
				//	vfCollection.setValue("NP2_AGEPREM2ND", 0);
				//}
			}
			else
			{
				throw new Exception("Record not found for Product");
			}
		}

		#endregion

		#region "Rider / Benefit"
		public void validateBenefit(string proposal)
		{
			try
			{
				if (ValidationUtility.isNewValidation())
				{
					setValidationFieldsForPlan(proposal, 1, "");
					setValidationFieldsForRider(proposal);
					setMoreFieldsInCollection();

					//***** Validate Mandatory Riders *******//
					string error = validateMandatoryRider(proposal);
					if (error.Length > 0)
					{
						throw new Exception(error);
					}

					//***** Validate Forbidden(Prohibited) Rider *******//
					error = validateForbiddenRider(proposal);
					if (error.Length > 0)
					{
						throw new Exception(error);
					}

					//***** Validate Riders Input values *******//
					error = validateBenefitNow(proposal);;
					if (error.Length > 0)
					{
						throw new Exception(error);
					}
                  
				}
			}
			catch(Exception e)
			{
				//throw new ProcessException(e.Message);
				//throw new ProcessException("<h1><u>Benefit(s) Validation</u></h1>" + e.Message);
				//throw new ProcessException("Benefit(s) Validation \\n============" + e.Message);
				//throw new ProcessException("<CENTER><H3 style=\"COLOR: #336699\"><B><U>Benefit(s) Validation Error</U></B></H3></CENTER>" + e.Message);
				throw new ProcessException(e.Message);
			}
		}


        private string validateSumAssuredPerLife(string proposal,string procedureName)
        {
            string errorMessage = string.Empty;
            string nic = string.Empty;
            try
            {
                rowset rsSelectedRiders = DB.executeQuery("SELECT A.PPR_PRODCD, B.PPR_DESCR,a.npr_sumassured FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND NPR_BASICFLAG='N' AND NPR_SELECTED='Y'");
                while (rsSelectedRiders.next())
                {
                    string rider = rsSelectedRiders.getString("PPR_PRODCD");
                    if (rider!="ADB" && rider!="AIB")
                    {
                        continue;
                    }

                    string sqlString = "Select nph_IDNO From lnu1_underwriti uw\n" +
                    "inner join lnph_pholder lph\n" +
                    "on uw.nph_code=lph.nph_code\n" +
                    " where np1_proposal='" + proposal + "'";
                    rowset rsSelectedIDNO = null;
                    try
                    {
                         rsSelectedIDNO = DB.executeQuery(sqlString);
                    }
                    catch (Exception)
                    {
                    }
                    if (rsSelectedIDNO.next())
                    {
                        nic = rsSelectedIDNO.getString("nph_IDNO");
                    }
                    
                    if (nic=="")
                    { 
                        errorMessage = "<BR><FONT style=\"COLOR: #336699\" size=2 ><B><U>User CNIC Not Found!</FONT></U></B><br>";
                        return errorMessage;
                    }
                    OleDbCommand cmd = null;
                    cmd = (OleDbCommand)DB.CreateCommand(procedureName, DB.Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    OleDbParameter param = new OleDbParameter("user_totAmount", OleDbType.Numeric, 18);
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new OleDbParameter("user_PPRODCODE", OleDbType.VarChar)).Value = rider;
                    cmd.Parameters.Add(new OleDbParameter("user_NPHCODE", OleDbType.VarChar)).Value = nic;
                    cmd.Parameters.Add(new OleDbParameter("user_PROPOSALNO", OleDbType.VarChar)).Value = proposal;
                   
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    if ((double.Parse(cmd.Parameters["user_totAmount"].Value.ToString()) - (double.Parse(rsSelectedRiders.getString("npr_sumassured")))) < 0)
                    {
                        errorMessage = "<BR><FONT style=\"COLOR: #336699\" size=2 ><B><U> "+rider+ " Limit is Exceeding!</FONT></U></B><br><FONT style=\"COLOR: #336699\" size=2 ><B><U>Available Limit is "+ cmd.Parameters["user_totAmount"].Value.ToString()+ "</FONT></U></B>";
                        break;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new ProcessException(e.Message);
            }
            return errorMessage;
        }
		private string validateBenefitNow(string proposal)
		{
			string productInfo = "";
			string validationError = "";

			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rsRider = DB.executeQuery("SELECT NP1_PROPOSAL, NP2_SETNO, LNPR.PPR_PRODCD, PPR_DESCR FROM LNPR_PRODUCT LNPR, LPPR_PRODUCT LPPR WHERE LNPR.PPR_PRODCD=LPPR.PPR_PRODCD AND NP1_PROPOSAL=? AND NVL(NPR_BASICFLAG,'Y')='N' AND NPR_SELECTED='Y' order by PPR_DESCR ", pc);
			while(rsRider.next())
			{
				/**** Get rider code ****/
				string product =  rsRider.getString("PPR_PRODCD");
				string baseProduct = this.vfCollection.getString("PPR_PRODCDB");
				rowset rsBuiltinRider = DB.executeQuery("select 'A' from LPRI_RIDER WHERE PPR_PRODCD='" + baseProduct + "' AND PPR_RIDER='"+product+"' AND NVL(PRI_BUILTIN,'N') ='N'");

				//Validate only Non-Builtin (Non-Mandatory Rider)
				if(rsBuiltinRider.next())
				{
					productInfo    =  rsRider.getString("PPR_DESCR") + " - " + product;

					SHMA.Enterprise.Data.ParameterCollection pc2=new SHMA.Enterprise.Data.ParameterCollection();
					pc2.clear();
					pc2.puts("@PPR_PRODCD", product);
					rowset rs = DB.executeQuery("SELECT PVL_VALIDRANGE FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR='" + clsIlasConstant.CALLVALIDATION + "' AND PPR_PRODCD=? AND PVL_VALIDRANGE NOT IN ('MANDRIDER')", pc2);
			
					while(rs.next())
					{
						string validateField = rs.getString("PVL_VALIDRANGE");
						if (validateField != "")
						{
							/**** Embed Rider Code if, Field related to Rider(Benefit) ****/
							string riderCode = "";
							if(vfRegister.getSourceType(validateField) == "P")
							{
								riderCode = product;
							}

							/**** Get input(Saved value by user) from collection ****/
							string columnName = vfRegister.getFieldName(validateField) + riderCode;
							object inputValue = vfCollection.getObject(columnName);

							try
							{
                                if (validateField == "SUMASSUREDPERLIFE")
                                {
                                    columnName = columnName.Substring(0, columnName.Length - 3);
                                    validationError = validateSumAssuredPerLife(proposal, columnName);
                                }
                                else
                                {
                                    Validation objValidation = new Validation(product, validateField, inputValue, vfCollection, vfRegister);
                                    objValidation.validateProduct();
                                }
								/**** Benefit Validation ****/
								
							}
							catch(Exception e)
							{
								if (validationError.IndexOf(productInfo) < 0)
								{
									validationError += "<BR><FONT style=\"COLOR: #336699\" size=2 ><B><U>" + productInfo + "</FONT></U></B><br>";
								}
								validationError += e.Message;
                                break;
							}
						}
					}
				}//End of Builtin Rider Check
			}
			return validationError;
		}
		
		private void setValidationFieldsForRider(string proposal)
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("SELECT * FROM LNPR_PRODUCT LNPR WHERE NP1_PROPOSAL=? AND NVL(NPR_BASICFLAG,'Y')='N'", pc);
			while(rs.next())
			{
				string rider = rs.getString("PPR_PRODCD"); 
				//vfCollection.setRiderSumAssured(rs.getString("PPR_PRODCD"),  rs.getDouble("NPR_SUMASSURED"));
				//vfCollection.setRiderTotalPremium(rs.getString("PPR_PRODCD"), rs.getDouble("NPR_TOTPREM"));
				//vfCollection.setRiderBenefitTerm(rs.getString("PPR_PRODCD"),  rs.getInt("NPR_BENEFITTERM"));
				//vfCollection.setRiderFrequency(rs.getString("PPR_PRODCD"),    rs.getDouble("NPR_ETASA"));
				
				vfCollection.setValue("PPR_PRODCD"+rider,     rs.getString("PPR_PRODCD"));
				vfCollection.setValue("NPR_BENEFITTERM"+rider,rs.getDouble("NPR_BENEFITTERM"));
				vfCollection.setValue("NPR_PREMIUMTER"+rider, rs.getDouble("NPR_PREMIUMTER"));
				vfCollection.setValue("NPR_SUMASSURED"+rider, rs.getDouble("NPR_SUMASSURED"));
				vfCollection.setValue("NPR_PREMIUM"+rider,    rs.getDouble("NPR_PREMIUM"));
				vfCollection.setValue("NPR_LOADING"+rider,    rs.getObject("NPR_LOADING")     == null ? 0.0 : rs.getDouble("NPR_LOADING"));
				vfCollection.setValue("NPR_TOTPREM"+rider,    rs.getObject("NPR_TOTPREM")     == null ? 0.0 : rs.getDouble("NPR_TOTPREM"));
				vfCollection.setValue("NPR_PREMRATE"+rider,   rs.getObject("NPR_PREMRATE")    == null ? 0.0 : rs.getDouble("NPR_PREMRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE"+rider,rs.getObject("NPR_EXTMORTRATE") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE"));
				vfCollection.setValue("NPR_ENDORSMT"+rider,   rs.getString("NPR_ENDORSMT"));
				vfCollection.setValue("NPR_LIFE"+rider,       rs.getString("NPR_LIFE"));
				vfCollection.setValue("NPR_SAR"+rider,        rs.getObject("NPR_SAR")           == null ? 0.0 : rs.getDouble("NPR_SAR"));
				vfCollection.setValue("NPR_ETASA"+rider,      rs.getObject("NPR_ETASA")         == null ? 0.0 : rs.getDouble("NPR_ETASA"));
				vfCollection.setValue("NPR_PAIDUPSA"+rider,   rs.getObject("NPR_PAIDUPSA")      == null ? 0.0 : rs.getDouble("NPR_PAIDUPSA"));
				vfCollection.setValue("NPR_ETATERM"+rider,    rs.getObject("NPR_ETATERM")       == null ? 0.0 : rs.getDouble("NPR_ETATERM"));
				vfCollection.setValue("NPR_PURENDOWMENT"+rider,rs.getObject("NPR_PURENDOWMENT") == null ? 0.0 : rs.getDouble("NPR_PURENDOWMENT"));
				vfCollection.setValue("NPR_INDEXATION"+rider, rs.getString("NPR_INDEXATION"));
				vfCollection.setValue("NPR_INDEXRATE"+rider,  rs.getDouble("NPR_INDEXRATE"));
				vfCollection.setValue("NPR_ETAPREMIUM"+rider, rs.getObject("NPR_ETAPREMIUM") == null ? 0.0 : rs.getDouble("NPR_ETAPREMIUM"));
				vfCollection.setValue("NPR_CASHVALUE"+rider,  rs.getObject("NPR_CASHVALUE")  == null ? 0.0 : rs.getDouble("NPR_CASHVALUE"));
				vfCollection.setValue("NPR_FACTOR"+rider,     rs.getObject("NPR_FACTOR")     == null ? 0.0 : rs.getDouble("NPR_FACTOR"));
				vfCollection.setValue("NPR_ETADAYS"+rider,    rs.getObject("NPR_ETADAYS")    == null ? 0.0 : rs.getDouble("NPR_ETADAYS"));
				vfCollection.setValue("NPR_EDUNITS"+rider,    rs.getDouble("NPR_EDUNITS"));
				vfCollection.setValue("NPR_AGEPREM"+rider,    rs.getObject("NPR_AGEPREM")    == null ? 0.0 : rs.getInt("NPR_AGEPREM"));
				vfCollection.setValue("NPR_AGEPREM2ND"+rider, rs.getObject("NPR_AGEPREM2ND") == null ? 0.0 : rs.getInt("NPR_AGEPREM2ND"));
				//CONVERT
				//NPR_COMMLOADING
				//NPR_INCLUDELOADINNIV
				//NPR_MATURITYDATE
				vfCollection.setValue("NPR_AGEDIFFERENCE"+rider, rs.getObject("NPR_AGEDIFFERENCE") == null ? 0.0 : rs.getDouble("NPR_AGEDIFFERENCE"));
				vfCollection.setValue("CCB_CODE"+rider,          rs.getString("CCB_CODE"));
				//NPR_PAID
				vfCollection.setValue("NPR_INTERESTRATE"+rider,  rs.getObject("NPR_INTERESTRATE") == null ? 0.0 : rs.getDouble("NPR_INTERESTRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE2"+rider,  rs.getObject("NPR_EXTMORTRATE2") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE2"));
				vfCollection.setValue("NP2_AGEPREM"+rider,       rs.getObject("NP2_AGEPREM")      == null ? 0.0 : rs.getInt("NP2_AGEPREM"));
				vfCollection.setValue("NPR_PAIDUPTOAGE"+rider,   rs.getObject("NPR_PAIDUPTOAGE")  == null ? 0.0 : rs.getInt("NPR_PAIDUPTOAGE"));
				vfCollection.setValue("CMO_MODE"+rider,          rs.getString("CMO_MODE"));
				vfCollection.setValue("NPR_BASICPRMANNUAL"+rider,rs.getObject("NPR_BASICPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_BASICPRMANNUAL"));
				vfCollection.setValue("NPR_EXCESPRMANNUAL"+rider,rs.getObject("NPR_EXCESPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESPRMANNUAL"));
				//NPR_SELECTED
				vfCollection.setValue("NPR_AGEPREM2"+rider,      rs.getObject("NPR_AGEPREM2") == null ? 0.0 : rs.getInt("NPR_AGEPREM2"));
				vfCollection.setValue("PCU_CURRCODE"+rider,      rs.getString("PCU_CURRCODE"));
				vfCollection.setValue("NPR_EXCESSPREMIUM"+rider, rs.getObject("NPR_EXCESSPREMIUM") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM"));
				vfCollection.setValue("NPR_EXCESSPREMIUM_ACTUAL"+rider, rs.getObject("NPR_EXCESSPREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUM_FC"+rider,    rs.getObject("NPR_PREMIUM_FC")       == null ? 0.0 : rs.getDouble("NPR_PREMIUM_FC"));
				vfCollection.setValue("NPR_PREMIUM_AV"+rider,    rs.getObject("NPR_PREMIUM_AV")       == null ? 0.0 : rs.getDouble("NPR_PREMIUM_AV"));
				vfCollection.setValue("NPR_PREMIUM_ACTUAL"+rider,rs.getObject("NPR_PREMIUM_ACTUAL")   == null ? 0.0 : rs.getDouble("NPR_PREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUMDISCOUNT"+rider,rs.getObject("NPR_PREMIUMDISCOUNT") == null ? 0.0 : rs.getDouble("NPR_PREMIUMDISCOUNT"));
			}
		}
		
		
		private string validateMandatoryRider(string proposal)
		{
			
			try
			{
				if (ValidationUtility.isNewValidation())
				{
					string validationError = "";

					//********** Pick only those Riders which are selected ***********
					rowset rsSelectedRiders = DB.executeQuery("SELECT A.PPR_PRODCD, B.PPR_DESCR FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND NPR_BASICFLAG='N' AND NPR_SELECTED='Y'");
					while(rsSelectedRiders.next())
					{
						string rider     = rsSelectedRiders.getString("PPR_PRODCD");
						string RiderInfo = rsSelectedRiders.getString("PPR_DESCR") + " - " + rider ;
						string mandatoryRiderList = "";

						//*********** Get Mandatory Riders List ***********
						try
						{
							mandatoryRiderList = this.getMandatoryRider(proposal, rider);
						}
						catch(Exception e)
						{
							throw new ProcessException("Rider (" + RiderInfo + ") - " +  e.Message);
						}
						string missingRidersList  = "";
						
						if(mandatoryRiderList.Trim().Length > 0)
						{
							//*********** Check Mandatory Riders are selected or not ***********
							int condRiderCount = 0;
							string[] MandRidersArray = mandatoryRiderList.Split(new char[] { ',' });
							for (int j = 0; j <= MandRidersArray.Length - 1; j++) 
							{
								bool mandRiderIsMissing = false;
								string mandRiderQuery = "";
								bool condRiderValidation = false;
								if(MandRidersArray[j].IndexOf("~")>=0)
								{
									condRiderCount= MandRidersArray[j].Length - MandRidersArray[j].Replace("~","").Length+1;
									string mandRiders = MandRidersArray[j].Replace("~","','");
									mandRiderQuery = "SELECT A.PPR_PRODCD, B.PPR_DESCR, NPR_SELECTED FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND A.PPR_PRODCD IN ('" + mandRiders + "') AND NPR_BASICFLAG='N' AND NPR_SELECTED='Y'";
									condRiderValidation=true;
								}
								else 
								{
									mandRiderQuery ="SELECT A.PPR_PRODCD, B.PPR_DESCR, NPR_SELECTED FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND A.PPR_PRODCD='" + MandRidersArray[j] + "' AND NPR_BASICFLAG='N' ";
								}
								rowset rsMandRiderSelected = DB.executeQuery(mandRiderQuery);
								if(rsMandRiderSelected.next())
								{   
									//******* If not selected then Mandatory Rider is missing *******
									//if(rsMandRiderSelected.getString("NPR_SELECTED") != "Y" && (rsMandRiderSelected.size()<=condRiderCount || condRiderCount==0))
									if(rsMandRiderSelected.getString("NPR_SELECTED") != "Y" && (rsMandRiderSelected.size()<=0 || condRiderCount==0))
									{
										mandRiderIsMissing = true;
									}
								}
								else
								{
									//******* If record not found then Mandatory Rider is missing *******
										mandRiderIsMissing = true;
								}

								if(mandRiderIsMissing)
								{
									//missingRidersList = missingRidersList + "&nbsp;&nbsp;&nbsp;>&nbsp;" + rsMandRiderSelected.getString("PPR_DESCR") + " - " + MandRidersArray[j] + "&nbsp;<B><I>(Missing...)</I></B><BR>";
									if (mandRiderQuery.IndexOf("AND NPR_SELECTED='Y'")>0)
									{
										mandRiderQuery = mandRiderQuery.Replace("AND NPR_SELECTED='Y'","");
										rsMandRiderSelected = DB.executeQuery(mandRiderQuery);
										rsMandRiderSelected.next();
									}
									int noOfIteration=0;
									do
									{
										missingRidersList = missingRidersList + "&nbsp;&nbsp;&nbsp;>&nbsp;" + rsMandRiderSelected.getString("PPR_DESCR") + " - " + rsMandRiderSelected.getString("PPR_PRODCD") + "&nbsp;<I>Missing...</I>";
										noOfIteration++;
										if(rsMandRiderSelected.size()!=noOfIteration)
											missingRidersList = missingRidersList +"&nbsp;<B><I>(  OR ...)</I></B><BR>";
										else
											missingRidersList = missingRidersList + "<BR>";

									}while(rsMandRiderSelected.next());
									
								}
							}
						}
						if(missingRidersList.Length > 0)
						{
							string RiderHeading = "<BR><FONT style=\"COLOR: #336699\" size=1 ><B><U>" + RiderInfo + "</FONT></U></B><br>" ;
							validationError = validationError + RiderHeading + missingRidersList;
						}
					}//End of while
					
					if(validationError.Length > 0)
					{
						throw new ProcessException(validationError);
					}
				}
				return "";
			}
			catch(ProcessException e)
			{
				string MandatoryRiderHeading = "<BR><CENTER><FONT style=\"COLOR: #336699\" size=2><B><U> Mandatory Riders </FONT></U></B></CENTER><br>" ;
				return MandatoryRiderHeading + e.Message;
			}
		}


		private string validateForbiddenRider(string proposal)
		{
			try
			{
				if (ValidationUtility.isNewValidation())
				{
					string validationError = "";

					//********** Pick only those Riders which are selected ***********
					rowset rsSelectedRiders = DB.executeQuery("SELECT A.PPR_PRODCD, B.PPR_DESCR FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND NPR_BASICFLAG='N' AND NPR_SELECTED='Y'");
					while(rsSelectedRiders.next())
					{
						string rider     = rsSelectedRiders.getString("PPR_PRODCD");
						string RiderInfo = rsSelectedRiders.getString("PPR_DESCR") + " - " + rider ;
						string riderList = "";

						//*********** Get Riders List ***********
						try
						{
							riderList = this.getForbiddenRider(proposal, rider);
						}
						catch(Exception e)
						{
							throw new ProcessException("Rider (" + RiderInfo + ") - " +  e.Message);
						}
						string forbiddenRidersList  = "";

						//*********** Check Forbidden Riders either selected or not ***********//
						if(riderList.Trim().Length > 0)
						{
							string[] ForbRidersArray = riderList.Split(new char[] { ',' });
							for (int j = 0; j <= ForbRidersArray.Length - 1; j++) 
							{
								bool forbRiderIsSelected = false;

								rowset rsForbRiderSelected = DB.executeQuery("SELECT A.PPR_PRODCD, B.PPR_DESCR, NPR_SELECTED FROM LNPR_PRODUCT A, LPPR_PRODUCT B WHERE A.PPR_PRODCD=B.PPR_PRODCD AND NP1_PROPOSAL='" + proposal + "' AND A.PPR_PRODCD='" + ForbRidersArray[j] + "' AND NPR_BASICFLAG='N' ");
								if(rsForbRiderSelected.next())
								{   
									//******* Raise error, if selected *******//
									if(rsForbRiderSelected.getString("NPR_SELECTED") == "Y")
									{
										forbRiderIsSelected = true;
									}
								}

								if(forbRiderIsSelected)
								{
									forbiddenRidersList = forbiddenRidersList + "&nbsp;&nbsp;&nbsp;>&nbsp;" + rsForbRiderSelected.getString("PPR_DESCR") + " - " + ForbRidersArray[j] + "&nbsp;<B><I>(Not allowed...)</I></B><BR>";
								}
							}
						}
						if(forbiddenRidersList.Length > 0)
						{
							string RiderHeading = "<BR><FONT style=\"COLOR: #336699\" size=1 ><B><U>" + RiderInfo + "</FONT></U></B><br>" ;
							validationError = validationError + RiderHeading + forbiddenRidersList;
						}
					}//End of while
					
					if(validationError.Length > 0)
					{
						throw new ProcessException(validationError);
					}
				}
				return "";
			}
			catch(ProcessException e)
			{
				string MandatoryRiderHeading = "<BR><CENTER><FONT style=\"COLOR: #336699\" size=2><B><U> Forbidden Riders </FONT></U></B></CENTER><br>" ;
				return MandatoryRiderHeading + e.Message;
			}
		}


		private string getMandatoryRider(string proposal, string rider)
		{
			return getConditonalRiders(proposal, rider, clsIlasConstant.MANDATORY_RIDER);
		}

		private string getForbiddenRider(string proposal, string rider)
		{
			return getConditonalRiders(proposal, rider, clsIlasConstant.FORBIDDEN_RIDER);
		}

		private string getConditonalRiders(string proposal, string rider, string fieldName)
		{
			string ridersList = "";

			//************ Get Field Combination *****************
			string fieldCombination = "";
			string valueCombination = "";
			string QEI = "";
			rowset rsFieldComb = DB.executeQuery("SELECT PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD='" + rider + "' AND PVF_CODE='" + fieldName + "'");
			if(rsFieldComb.next())
			{
				if(rsFieldComb.getObject("PVF_FIELDCOMB") != null)
				{
					fieldCombination = rsFieldComb.getString("PVF_FIELDCOMB").Trim();
					if(fieldCombination.Length > 0)
					{
						string[] fieldArray = fieldCombination.Split(new char[] { ',' });
						for (int i = 0; i <= fieldArray.Length - 1; i++) 
						{
							//************ Set Value Combination ******************
							valueCombination = valueCombination + this.vfCollection.getString(fieldArray[i]) + ",";
						}
						//************ Set Query Extra Info for filtering which is based on FC/VC  ******************
						QEI = " AND PVL_VALUECOMB in ('" + valueCombination.Remove(valueCombination.LastIndexOf(","), 1) + "')";
					}
				}

				//************ get Mandatory Riders Now  ******************
				string mandRiderQuery = "SELECT PVL_VALIDRANGE FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR='" + fieldName + "' AND PPR_PRODCD='" + rider + "' ";
				rowset rsMandatoryRider = DB.executeQuery(mandRiderQuery + QEI);
				if(rsMandatoryRider.next())
				{
					if(rsMandatoryRider.getObject("PVL_VALIDRANGE") != null)
					{
						if(rsMandatoryRider.getString("PVL_VALIDRANGE").Trim().Length > 0)
						{
							ridersList = rsMandatoryRider.getString("PVL_VALIDRANGE");
						}
					}
				}
			}			
			return ridersList;
		}
		#endregion

		#region "Load more Fields (Query/Session)"
		private void setMoreFieldsInCollection()
		{
			try
			{
				EnvHelper env = new EnvHelper();
				this.vfRegister = new FieldRegister();
				rowset rsFields = DB.executeQuery("SELECT * FROM LVFS_FIELDSETUP");
				while(rsFields.next())
				{
					string vfID    = rsFields.getString("VFS_CODE");
					string vfDesc  = rsFields.getString("VFS_DESC");
					string dbID    = rsFields.getString("VFS_ID");
					string vfType  = rsFields.getString("VFS_DATATYPE");
					string Source  = rsFields.getString("VFS_SOURCE");
					string Query   = rsFields.getString("VFS_QUERY");
					
					//Set Important Information in Field Register
					vfRegister.setDescription(vfID,vfDesc);
					vfRegister.setFieldName(vfID,dbID);
					vfRegister.setDataType(vfID,vfType);
					vfRegister.setSourceType(vfID,Source);
					
					//Set only Query(Q)/Session(S) based columns in vfCollection
					if(Source == "Q")
					{
						try
						{
							//Get Expression and then set Session values in Session place holder (i.e SV("col"))
							rowset rsRes = DB.executeQuery(EnvHelper.Parse(Validation.getExecuteableExpression(Query, vfCollection)));
							if(rsRes.next())
							{
								if(vfType == "N")
								{
									vfCollection.setValue(dbID, rsRes.getDouble(1));
								}
								else if(vfType == "D")
								{
									vfCollection.setValue(dbID, rsRes.getDate(1));
								}
								else
								{
									vfCollection.setValue(dbID, rsRes.getString(1));
								}
							}
						}
						catch(Exception e)
						{	
						}
					}
					else if(Source == "S")
					{
						if(vfType == "N")
						{
							vfCollection.setValue(dbID, Convert.ToDouble(env.getAttribute(dbID)));
						}
						else if(vfType == "D")
						{
							//vfCollection.setValue(vfID, Convert.ToDateTime(env.getAttribute(vfID)));
							vfCollection.setValue(dbID, Convert.ToString(env.getAttribute(dbID)));
						}
						else
						{
							vfCollection.setValue(dbID, Convert.ToString(env.getAttribute(dbID)));
						}
					}
				}
			}
			catch(Exception e)
			{
			}
		}
		#endregion
	}
}
