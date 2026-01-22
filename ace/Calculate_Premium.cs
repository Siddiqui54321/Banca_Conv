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

	public class Calculate_Premium : shgn.ProcessCommand
	{
		double pdbl_Premium,pdbl_PremiumRate,pdbl_SumAssured, pdbl_EffectAge, pdbl_BT, pdbl_PT;
		String strClientCode;
		int faceCurrency ;
		int premCurrency ;
		int avCurrency;

		public override String processing()
		{
			string strValidationErrors = "";
			try
			{
				NameValueCollection[] nmRows = this.getDataRows();
				for(int a=0; a<nmRows.Length; a++)
				{
					NameValueCollection nvc = nmRows[a];
					if (nvc.getObject("NP1_PROPOSAL") == null || nvc.getObject("NP1_PROPOSAL").ToString().Equals(""))
					{
						throw new ProcessException ( "Please Select A Proposal First" );
					}
					else
					{
						string proposal = nvc.getObject("NP1_PROPOSAL").ToString();
						String strCurrency = "Select a.pcu_currcode premcurrency, b.pcu_currcode facecurrency, NVL(a.pcu_avcurrcode,a.pcu_currcode) avcurrency " +
							" from lnp1_policymastr a, lnpr_product b where a.np1_proposal = '"+ proposal 
							+ "' and b.np1_proposal=a.np1_proposal and b.np2_setno=1 and b.npr_basicflag='Y'";

						rowset rstCurrency = DB.executeQuery( strCurrency );
						if ( rstCurrency.next() )
						{
							faceCurrency = rstCurrency.getInt("FACECURRENCY");
							premCurrency = rstCurrency.getInt("PREMCURRENCY");
							avCurrency = rstCurrency.getInt("AVCURRENCY");

						}
			
						////////////////////////
						/* Validate Proposal */
						////////////////////////
						try
						{
							// Naseer 16/07/2008
							//Current_BT = double.Parse(nvc.getObject("NPR_BENEFITTERM" ).ToString());
							double Current_PT = double.Parse(nvc.getObject("NPR_PREMIUMTER" ).ToString());
							double Current_Age = double.Parse(getAge(proposal));
							double Current_SA = double.Parse(( nvc.getObject("NPR_SUMASSURED").ToString() ));
					
							ProcedureAdapter cs = new ProcedureAdapter("VALIDATE_WEBPROPOSAL_CALL");
							cs.Set("P_PROPOSAL", OleDbType.VarChar, proposal);
							cs.Set("P_AGE", OleDbType.Numeric, Current_Age);
							cs.Set("P_TERM", OleDbType.Numeric, Current_PT); //cs.Set("P_TERM", OracleType.Number, Current_BT);
							cs.Set("P_SA", OleDbType.Numeric, Current_SA);
							cs.RegisetrOutParameter("RETUR01", OleDbType.VarChar, 500);

							//Execute and retrieve the returned values
							cs.Execute();
							strValidationErrors = Convert.ToString(cs.Get("RETUR01"));
							if(strValidationErrors.Trim().Length>0)
								throw new ProcessException (strValidationErrors);
						}
						catch(Exception e)
						{
							strValidationErrors = e.Message;
						}
						
						if (strValidationErrors!=null && !strValidationErrors.Equals(""))
						{
							throw new ProcessException (strValidationErrors);
						}

                        /* Calculate Premium Before Validation*/
                        Calculate_Basic_Plan(proposal);
						Calculate_Riders    (proposal);
						//**** Not found in DB -- Calculate_Bonus     (proposal);
						//**** Not found in DB -- Generate_Charges    (proposal);
						GenerateIllustration(proposal);
						//Calculate_Basic_Plan  (proposal);
						
						//********** Mark UnSelect those Riders whose Benefit Term, Sum Assured and Premium are 0 ********//
						DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_SELECTED='N' WHERE NP1_PROPOSAL='" + proposal + "' AND NPR_SELECTED='Y' AND NPR_BASICFLAG='N' AND NVL(NPR_BENEFITTERM,0)=0 AND NVL(NPR_SUMASSURED,0)=0 AND NVL(NPR_PREMIUM,0)=0");

						//************* Activity Log *************//			
						Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PREMIUM_CALCULATED, proposal + "~" + ace.Ace_General.getPPR_PRODCD(proposal));
					}
				}
				return "";
			}
			catch (Exception ex)
			{
				//ex.printStackTrace();
				String msg = ex.Message;
				////System.out.println("Original  Message: " + msg);
				msg = msg.Replace("\"", "");
				msg = msg.Replace("'", "");
				msg = msg.Replace("\r", " ");
				////System.out.println("Processed Message: " + msg);
				return msg;
			}
			finally
			{
				//SessionObject.Set("FLAG_RESET_PREMIUM","");
			}
		}
	

		private void Calculate_Basic_Plan(String proposal) 
		{
			//System.out.println("------------------- Calculating Basic Plan ----------------");
			String strBasicPlan = "Select * from lnpr_product where np1_proposal = '"+ proposal 
				+ "' and np2_setno=1 and npr_basicflag='Y'";

			rowset rstBasicPlan = DB.executeQuery( strBasicPlan );
			if ( rstBasicPlan.next() )
			{
				String strProduct = rstBasicPlan.getString("PPR_PRODCD");
				if ( strProduct == null && strProduct.Equals("") )//EqualsIgnoreCase
				{
					throw new ProcessException ("Unable To Calculate Premium. No Basic Plan Found");
				}
				else
				{
					strClientCode = getClientCode(proposal);
					string life = (rstBasicPlan.getObject("NPR_LIFE")==null ? "S" : rstBasicPlan.getString("NPR_LIFE"));

					ProcedureAdapter call =  new ProcedureAdapter("BASIC_PLAN_PREMIUM_RENEWAL");
					call.Set("MNPR_LIFE",     OleDbType.VarChar, 10, life);
					call.Set("MNP1_PROPOSAL", OleDbType.VarChar, 50, rstBasicPlan.getString("NP1_PROPOSAL") );
					call.Set("MNP2_SETNO",    OleDbType.Numeric, 1);
					call.Set("MPPR_PRODCD",   OleDbType.VarChar, 50, rstBasicPlan.getString("PPR_PRODCD"));
					call.Set("MNPH_CODE",     OleDbType.VarChar, 50, strClientCode);
					call.RegisetrOutParameter("MNPR_PREMIUM",     OleDbType.Numeric);
					call.RegisetrOutParameter("MNPR_PREMRATE",    OleDbType.Numeric);
					call.RegisetrOutParameter("MNPR_SUMASSURED",  OleDbType.Numeric);
					call.RegisetrOutParameter("MNP2_EFFECTIVEAGE",OleDbType.Numeric);
					call.RegisetrOutParameter("MNPR_BENEFITTERM", OleDbType.Numeric);
					call.RegisetrOutParameter("MNPR_PREMIUMTER",  OleDbType.Numeric);

					// Seting last (10th) parameter of basic plan premium renewal to null
					call.Set("M_ESCFLAG",     OleDbType.VarChar, 10, "N");
					call.RegisetrOutParameter("M_ERROR",          OleDbType.VarChar, 500);
					call.Execute();

					string pError = call.Get("M_ERROR") == null ? "" : Convert.ToString(call.Get("M_ERROR")) ;
					if (pError.Trim().Length > 0)
					{
						throw new ProcessException(pError);
					}

					pdbl_Premium     =  Double.Parse(call.Get("MNPR_PREMIUM")     == null ? "0.0" : (call.Get("MNPR_PREMIUM").ToString().Trim()     == "" ? "0.0" : call.Get("MNPR_PREMIUM").ToString()) );
					pdbl_PremiumRate =  Double.Parse(call.Get("MNPR_PREMRATE")    == null ? "0.0" : (call.Get("MNPR_PREMRATE").ToString().Trim()    == "" ? "0.0" : call.Get("MNPR_PREMRATE").ToString()) );
					pdbl_SumAssured  =  Double.Parse(call.Get("MNPR_SUMASSURED")  == null ? "0.0" : (call.Get("MNPR_SUMASSURED").ToString().Trim()  == "" ? "0.0" : call.Get("MNPR_SUMASSURED").ToString()));
					pdbl_EffectAge   =  Double.Parse(call.Get("MNP2_EFFECTIVEAGE")== null ? "0.0" : (call.Get("MNP2_EFFECTIVEAGE").ToString().Trim()== "" ? "0.0" : call.Get("MNP2_EFFECTIVEAGE").ToString()));
					pdbl_BT          =  Double.Parse(call.Get("MNPR_BENEFITTERM") == null ? "0.0" : (call.Get("MNPR_BENEFITTERM").ToString().Trim() == "" ? "0.0" : call.Get("MNPR_BENEFITTERM").ToString()));
					pdbl_PT          =  Double.Parse(call.Get("MNPR_PREMIUMTER")  == null ? "0.0" : (call.Get("MNPR_PREMIUMTER").ToString().Trim()  == "" ? "0.0" : call.Get("MNPR_PREMIUMTER").ToString()));

					
					//call.Close();
					call =  new ProcedureAdapter("ADJUSTMENTS_PREMIUM_RENEWAL");
					call.Set("MNPR_LIFE",    OleDbType.VarChar, 10,(rstBasicPlan.getObject("NPR_LIFE")==null?"F":rstBasicPlan.getString("NPR_LIFE")));
					call.Set("MNP1_PROPOSAL",OleDbType.VarChar, 50, rstBasicPlan.getString("NP1_PROPOSAL") );
					call.Set("MNP2_SETNO",   OleDbType.Numeric,  1);
					call.Set("MPPR_PRODCD",  OleDbType.VarChar, 50, rstBasicPlan.getString("PPR_PRODCD"));
					call.Set("MNPH_CODE",    OleDbType.VarChar, 50, strClientCode);
					call.Set("M_ESCFLAG",    OleDbType.VarChar, 10, "");// Seting last (6th) parameter of ADJUSTMENTS PREMIUM RENEWAL to null
					call.RegisetrOutParameter("M_ERROR",OleDbType.VarChar,500 );
					call.Execute();
					
					pError = call.Get("M_ERROR") == null ? "" : Convert.ToString(call.Get("M_ERROR"));
				
					if (pError.Trim().Length > 0)
					{
						throw new ProcessException (parseError(pError));
					}

					//Charges/Fees
					Double charges=0.00;
					rowset LNP2_CHARGES = DB.executeQuery("SELECT NP2_TOTLOAD from LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+rstBasicPlan.getString("NP1_PROPOSAL")+"' and np2_setno=1 ");
					if (LNP2_CHARGES.next()) 
					{
						charges=LNP2_CHARGES.getDouble("NP2_TOTLOAD");			
					}

					//// UPDATING CALCULATED VALUES IN LNPR_PRODUCT TABLE ////
					string premiumTermQuery = "";
					if (clsIlasUtility.isPremiumTermManual() == false)
					{
						premiumTermQuery = " ,NPR_PREMIUMTER ="+ pdbl_PT;
					}

					string strUpdatePlan = "Update LNPR_PRODUCT   "  
						+ " set NPR_PREMIUM     = GET_CONVERTED_VALUE(" + pdbl_Premium +","+faceCurrency+","+premCurrency+")" 
						+ "     ,NPR_PREMIUM_FC  = "+ pdbl_Premium       
						+ "     ,NPR_TOTPREM     = "+ (pdbl_Premium+charges)
						+ "     ,NPR_PREMIUM_AV  = GET_CONVERTED_VALUE(" + pdbl_Premium  +","+faceCurrency+","+avCurrency+")" 
						+ "     ,NPR_PREMRATE    = "+ pdbl_PremiumRate   
						+ "     ,NPR_SUMASSURED  = "+ pdbl_SumAssured    
						+ "     ,NPR_AGEPREM     = "+ pdbl_EffectAge     
						+ "     ,NPR_BENEFITTERM = "+ pdbl_BT            
						//+ "     ,NPR_PREMIUMTER  = "+ pdbl_PT          
						+       premiumTermQuery                                
						+ " Where NP1_PROPOSAL = '"+ rstBasicPlan.getString("NP1_PROPOSAL") +"' and "
						+ "     NP2_SETNO    = 1 and "
						+ "     PPR_PRODCD   = '"+ rstBasicPlan.getString("PPR_PRODCD") +"'";
					DB.executeDML(strUpdatePlan);

					//Initializing rider's values
					string riderQry = "Update LNPR_PRODUCT      "+
						              " SET NPR_PREMIUM = 0, NPR_PREMIUM_FC  = 0, NPR_PREMIUM_AV  = 0, NPR_PREMRATE = NULL, " +
						              " NPR_SUMASSURED = 0, NPR_BENEFITTERM = 0, NPR_PREMIUMTER = 0 "+
						              " Where NP1_PROPOSAL = '"+ rstBasicPlan.getString("NP1_PROPOSAL") +"' and "+
						              "       NP2_SETNO    = 1 AND NPR_BASICFLAG='N' AND NVL(NPR_SELECTED,'Y')='N' AND NVL(NPR_BUILTIN,'N')='N' " ;
					DB.executeDML(riderQry);


					//************ Update Builtin Riders Informaion (SumAssured) ****************//
					riderQry = " Update LNPR_PRODUCT SET NPR_SUMASSURED= " + pdbl_SumAssured
						     + " Where NP1_PROPOSAL ='"+ rstBasicPlan.getString("NP1_PROPOSAL") +"' AND NP2_SETNO=1 "
						     + "   AND NPR_BASICFLAG='N' AND NVL(NPR_SELECTED,'Y')='Y' AND NVL(NPR_BUILTIN,'N')='Y' " ;
					DB.executeDML(riderQry);

					
				}
			}
			else
			{
				throw new ProcessException ("Unable To Calculate Premium. No Basic Plan Found");
			}
		}

		private void Calculate_Riders(String proposal) 
		{
			//--------------------- Calculating Riders ------------------
			ProcedureAdapter call =  new ProcedureAdapter("RIDER_PREMIUM_RENEWAL");
			call.Set("MNPR_LIFE",    OleDbType.VarChar, 10, "F");
			call.Set("MNP1_PROPOSAL",OleDbType.VarChar, 50, proposal );
			call.Set("MNP2_SETNO",   OleDbType.Numeric, 1);
			call.Set("MNPH_CODE",    OleDbType.VarChar, 50, strClientCode);
			call.Set("M_ESCFLAG",    OleDbType.VarChar, 10, "N");
			call.RegisetrOutParameter("M_ERROR",OleDbType.VarChar,500 );
			call.Execute();

			string pError =  call.Get("M_ERROR") == null ? "" : Convert.ToString(call.Get("M_ERROR"));
				
			if (pError.Trim().Length > 0)
				throw new ProcessException(parseError(pError));
	
		}

		private void GenerateIllustration(string proposal)
		{
			ProcedureAdapter call = new ProcedureAdapter("CALL_ILUSTRATION");
			call.Set("P_PROPOSAL", OleDbType.VarChar, 50, proposal );
			call.RegisetrOutParameter("M_ERROR",OleDbType.VarChar,500 );
			call.Execute();
			string pError =  call.Get("M_ERROR") == null ? "" : Convert.ToString(call.Get("M_ERROR"));
			if (pError.Trim().Length > 0)
				throw new ProcessException(parseError(pError));
		}

		//		private void Calculate_Bonus(String proposal) 
		//		{
		//			//--------------------- Calculating Riders ------------------);
		//			String strBasicPlan = "Select a.ppr_prodcd, a.npr_benefitterm from lnpr_product a, lcsd_systemdtl b where np1_proposal = '"+ proposal 
		//				+ "' and a.np2_setno=1 and a.npr_basicflag='Y' and b.csh_id='GLOBL' AND b.csd_type='BONUS_PRODUCT' and instr(b.csd_value,a.ppr_prodcd)>0 ";
		//
		//			rowset rstBasicPlan = DB.executeQuery( strBasicPlan );
		//			if ( rstBasicPlan.next() )
		//			{
		//				call =  new OracleClientAdapter("GENERATE_BONUS", DB.Connection);
		//				call.Set("P_PROPOSAL", OracleType.VarChar, 50, ""+proposal );
		//				call.Set("P_UPTOAGE", OracleType.Number, rstBasicPlan.getString("NPR_BENEFITTERM"));
		//				call.Execute();
		//
		//			}
		//		}

		//		private void Generate_Charges(String proposal) 
		//		{
		//			String strBasicPlan = "Select a.ppr_prodcd, a.npr_benefitterm from lnpr_product a, lcsd_systemdtl b where np1_proposal = '"+ proposal 
		//				+ "' and a.np2_setno=1 and a.npr_basicflag='Y' and b.csh_id='GLOBL' AND b.csd_type='CHARGES_PRODUCT' and instr(b.csd_value,a.ppr_prodcd)>0 ";
		//
		//			rowset rstBasicPlan = DB.executeQuery( strBasicPlan );
		//			if ( rstBasicPlan.next() )
		//			{
		//				call =  new OracleClientAdapter("GENERATE_CHARGES", DB.Connection);
		//				call.Set("P_PROPOSAL", OracleType.VarChar, 50, ""+proposal );
		//				call.Execute();
		//			}
		//		}

		
		private String getClientCode(String proposal) 
		{
			String strClient = "Select nph_code from lnu1_underwriti where np1_proposal = '"+ proposal +
				"' and nu1_life='F'";

			rowset rstClient = DB.executeQuery( strClient );
			if ( rstClient.next() )
			{
				String client =  rstClient.getString("NPH_CODE");
				if ( client == null && client.Equals("") )//EqualsIgnoreCase
				{
					throw new ProcessException ("Unable To Calculate Premium. First Life Not Present.");
				}
				else
				{
					return client;
				}
			}
			else
			{
				throw new ProcessException ("Unable To Calculate Premium. First Life Not Present.");
			}
		}
        private string NomineeAge(string proposal)
        {
            string query= "Select NBF_AGE from lnbf_beneficiary\n"+
                           " where np1_proposal = '"+ proposal + "'\n" +
                           " and nbf_nominee = 'N' ";
            rowset rstAge = DB.executeQuery(query);
            if (rstAge.next())
            {
                string age = rstAge.getString("NBF_AGE");
                return age;
            }
            else
            {
                return "0";
            }
        }
		private String getAge(String proposal) 
		{


			//String strAge = "Select np2_ageprem from lnp2_policymastr where np1_proposal = '"+ proposal +"' ";
			String strAge = "SELECT DECODE(NVL(NPH_INSUREDTYPE,'N'), 'Y', np2.np2_ageprem, np2.np2_ageprem2nd) NP2_AGEPREM " +
				" FROM LNP2_POLICYMASTR NP2, LNPH_PHOLDER NPH " +
				" WHERE NPH.NPH_CODE = (SELECT NPH_CODE FROM LNU1_UNDERWRITI "+
				" WHERE NP1_PROPOSAL=NP2.NP1_PROPOSAL AND NPH_LIFE='D' AND NU1_LIFE='F') "+
				" AND NP2.NP1_PROPOSAL = '" + proposal + "'  AND NP2.NP2_SETNO=1  " ;

			//System.out.println(strClient);
			rowset rstAge = DB.executeQuery( strAge );
			if ( rstAge.next() )
			{
				String age =  rstAge.getString("NP2_AGEPREM");
				if ( age == null && age.Equals("") )//EqualsIgnoreCase
				{
					throw new ProcessException ("Unable To Get Age of the Client...");
				}
				else
				{
					return age;
				}
			}
			else
			{
				throw new ProcessException ("Unable To Get Age of the Client...");
			}
		}

		private String parseError(String pError) 
		{
			String[] tokens = pError.Split('#');
			String line1 ;

			if (tokens.Length > 1 )
			{
				//line1 = tokens[0]+"\n"+"    "+tokens[1]==null?"":tokens[1];
				line1 = tokens[0]+"    "+(tokens[1]==null?"":tokens[1]);
			}
			else
			{
				line1 = pError;
			}
			//String UserId = tokens[1].ToString().Split('=')[1].ToString().Trim();
			//String Password = tokens[2].ToString().Split('=')[1].ToString().Trim();
			//String DataSource = tokens[3].ToString().Split('=')[1].ToString().Trim();

			return line1;
		}

		
	}
}
