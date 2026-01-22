using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using FieldValidationException = SHMA.Enterprise.Exceptions.FieldValidationException;
using System.Data.OleDb;
using Bancassurance.Data;
using System.Globalization;

namespace ace
{
	


	public class ILUS_ET_NM_PLANDETAILS:shgn.SHGNCommand
	{
		public string strRidersUpdateInformation = "";
		EnvHelper env = new EnvHelper();

		public override void fsoperationAfterSave()
		{
			
			this.UpdatePolicyMaster();
			this.updatePlan();
			this.generateRiders();
			this.updateRiders();
			//generateQuestionaire(env.getAttribute("NP1_PROPOSAL").ToString(),getString("PPR_PRODCD"));//generate Questionaire
			this.validateInput();	

			//************* Activity Log *************//			
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PLAN_UPDATED,Convert.ToString(SessionObject.Get("NP1_PROPOSAL")) + "~" + Convert.ToString(get("PPR_PRODCD")));
		}

		public override void fsoperationAfterUpdate()
		{
			this.UpdatePolicyMaster();
			this.updatePlan();
			this.deleteRiders();
			this.generateRiders();
			this.updateRiders();
			this.validateInput();
			
			//************* Activity Log *************//			
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PLAN_UPDATED,Convert.ToString(SessionObject.Get("NP1_PROPOSAL")) + "~" + Convert.ToString(get("PPR_PRODCD")));			
		}
        
        public override string fsoperationBeforeSave(string proposalno)
        {
            string errorMessage = string.Empty;
            string nic = string.Empty;
            try
            {
                string sqlString = "Select nph_IDNO From lnu1_underwriti uw\n" +
                   "inner join lnph_pholder lph\n" +
                   "on uw.nph_code=lph.nph_code\n" +
                   " where np1_proposal='" + proposalno + "'";
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
                OleDbCommand cmd = null;
                cmd = (OleDbCommand)DB.CreateCommand("Policy_LapsedSurrenderPeriod", DB.Connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                OleDbParameter param = new OleDbParameter("user_Validation", OleDbType.VarChar, 200);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(new OleDbParameter("user_NPHCODE", OleDbType.VarChar)).Value = nic;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (!cmd.Parameters["user_Validation"].Value.ToString().Equals("Valid"))
                {
                    errorMessage=cmd.Parameters["user_Validation"].Value.ToString();
                }
                
            }
            catch (Exception e)
            {
                errorMessage="Error " +e.Message;
            }
            return errorMessage;
        }

        public void SaveBeneficiary(BeneficiaryData Data)
        {
            try
            {
                string sqlString = "insert into lnbf_beneficiary\n" +
                "  (NP1_PROPOSAL,  NBF_BENEFCD,\n" +
                "    CRL_RELEATIOCD,  NBF_BENNAME,\n" +
                "   NBF_AGE,   NBF_PERCNTAGE,  NBF_AMOUNT,\n" +
                "   NBF_DOB,   NBF_NOMINEE, NBF_BASIS)\n" +
                "values\n" +
                "  ('"+ SessionObject.Get("NP1_PROPOSAL").ToString() + "',\n" +
                "   1,\n" +
                "   ?,\n" +
                "   ?,\n" +
                "   ?,\n" +
                "   100.00,\n" +
                "   0.00,\n" +
                "   to_date(?, 'dd/mm/yyyy'),\n" +
                "   'N',\n" +
                "   '02')";
                if (int.Parse(Data.beneficiaryAge)>15)
                {
                    throw new ProcessException("Nominee Age Should be less than or equal to 15");
                }
                else if ((int.Parse(Data.beneficiaryAge) + int.Parse(Data.benefitTerm))>25)
                {
                    throw new ProcessException("Nominee Age Should be less than'"+ (int.Parse(Data.benefitTerm)-26) + "'");
                }
                SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                pc.puts("@CRL_RELEATIOCD", Data.beneficiaryRelation);
                pc.puts("@NBF_BENNAME", Data.beneficiaryName);
                pc.puts("@NBF_AGE", Data.beneficiaryAge);
                pc.puts("@NBF_DOB", Data.beneficiaryDOB);
                DB.executeDML(sqlString, pc);
            }
            catch (Exception e)
            {
                throw new ProcessException(e.Message);
            }
        }
        public void UpdateBeneficiary(BeneficiaryData Data)
        {
            try
            {
                rowset rsBeneficiary= DB.executeQuery("Select count(*) as Beneficiaries From lnbf_beneficiary where np1_proposal='" + SessionObject.Get("NP1_PROPOSAL").ToString() + "'");
                if (rsBeneficiary.next())
                {
                    if (int.Parse(rsBeneficiary.getString(1).ToString()) > 0)
                    {
                        string sqlString = "update lnbf_beneficiary\n" +
                                            "   set CRL_RELEATIOCD=?,\n" +
                                            "       NBF_BENNAME=?,\n" +
                                            "       NBF_AGE=?,\n" +
                                            "       NBF_DOB=to_date(?, 'dd/mm/yyyy')\n" +
                                            "   where np1_proposal='" + SessionObject.Get("NP1_PROPOSAL").ToString() + "'";


                        SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                        pc.puts("@CRL_RELEATIOCD", Data.beneficiaryRelation);
                        pc.puts("@NBF_BENNAME", Data.beneficiaryName);
                        pc.puts("@NBF_AGE", Data.beneficiaryAge);
                        pc.puts("@NBF_DOB", Data.beneficiaryDOB);
                        DB.executeDML(sqlString, pc);
                    }
                    else
                    {
                        SaveBeneficiary(Data);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ProcessException(e.Message);
            }
        }
        public void DeleteBeneficiary()
        { 
            try
            {
                string sqlString = "delete from lnbf_beneficiary\n" +
                "   where np1_proposal='" + SessionObject.Get("NP1_PROPOSAL").ToString() + "'";
                DB.executeDML(sqlString);
            }
            catch (Exception e)
            {
                throw new ProcessException(e.Message);
            }
        }
        private void updatePlan()
		{
			object objProposal = SessionObject.Get("NP1_PROPOSAL");
			try
			{
				if (objProposal == null || objProposal.ToString().Equals(""))
				{
					throw new ProcessException("Please Select A Proposal First");
				}
				else
				{
					string planProposal = objProposal.ToString();
					string planProdCode = Convert.ToString(get("PPR_PRODCD"));
					double planBenTerm  = Convert.ToDouble(get("NPR_BENEFITTERM"));
					int intPlanBenTerm  = Convert.ToInt16(planBenTerm);
				
					DateTime CommDate = clsIlasUtility.getCommencementDate(planProposal);
					DateTime MatDate  = new DateTime(CommDate.Year+intPlanBenTerm, CommDate.Month, CommDate.Day);		//chg-04032024 Feb-29 issue comments the line and add below	
					//DateTime MatDate = DateTime.ParseExact("29/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

					SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
					pc.puts("@NPR_MATURITYDATE", MatDate);					//chg-04032024 Feb-29 issue comments the line
					pc.puts("@NP1_PROPOSAL",     planProposal);
					pc.puts("@PPR_PRODCD",       planProdCode);
					DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_MATURITYDATE=? WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ", pc);	//chg-04032024 Feb-29 issue comments the line and add below
					//DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_MATURITYDATE=to_date('29/02/2024','DD/MM/YYYY') WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ", pc);
				}
			}
			catch(Exception e)
			{
				throw new ProcessException (e.Message);
			}
		}

		#region Business Methods
		private void UpdatePolicyMaster()
		{
			try
			{
				SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
				pm.puts("@PCU_CURRCODE", this.get("PCU_CURRCODE_PRM").ToString() );
				pm.puts("@CMO_MODE", this.get("CMO_MODE").ToString());
				//pm.puts("@PCU_AVCURRCODE", this.get("PCU_AVCURRCODE"));
				pm.puts("@NP1_RETIREMENTAGE", System.Convert.ToDouble(env.getAttribute("NP1_RETIREMENTAGE")) );
				pm.puts("@NP1_TOTALANNUALPREM", System.Convert.ToDouble(env.getAttribute("NP1_TOTALANNUALPREM")));

				pm.puts("@CFR_FORFEITUCD", this.get("CFR_FORFEITUCD").ToString());

				pm.puts("@NP1_PROPOSAL", env.getAttribute("NP1_PROPOSAL").ToString());

							
				//select NP1_PROPOSAL, PCU_CURRCODE from lnp1_policymastr where np1_proposal ='R/01/0010124'
				//DB.executeDML("update lnp1_policymastr set PCU_CURRCODE=?, CMO_MODE=?, PCU_AVCURRCODE="+(this.get("PCU_AVCURRCODE")==null?"null":this.get("PCU_AVCURRCODE"))+" where np1_proposal =?", pm);
				DB.executeDML("update lnp1_policymastr set PCU_CURRCODE=?, CMO_MODE=?, PCU_AVCURRCODE="+(this.get("PCU_AVCURRCODE")==null?"null":this.get("PCU_AVCURRCODE"))+", NP1_RETIREMENTAGE=?, NP1_TOTALANNUALPREM=?, CFR_FORFEITUCD=?  where np1_proposal =?", pm);
			}
			catch( Exception e)
			{
				throw new ProcessException(e.Message);
			}
		}

		private void deleteRiders()
		{
			object objProposal = SessionObject.Get("NP1_PROPOSAL");
			try
			{
				if (objProposal == null || objProposal.ToString().Equals(""))
				{
					throw new ProcessException("Please Select A Proposal First");
				}
				else
				{
					string strProposal = objProposal.ToString();
					String strBasicPlan = " Select ppr_prodcd,nvl(NPR_BENEFITTERM,0) NPR_BENEFITTERM, nvl(NPR_PREMIUMTER,0) NPR_PREMIUMTER, nvl(NPR_SUMASSURED,0) NPR_SUMASSURED from lnpr_product " +
						" Where np1_proposal='"+ strProposal +"' and NPR_BASICFLAG='Y'";
					rowset rstBasicPlan = DB.executeQuery(strBasicPlan);

					//If Plan (Base product) found then proceed
					if (rstBasicPlan.next())
					{
						string strProduct = rstBasicPlan.getString("PPR_PRODCD");
						
						//**************************************************************// 
						//************** Delete not selected Riders ********************//
						//**************************************************************//
						string riderQuery = "SELECT PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + strProposal + "' AND NPR_BASICFLAG='N' ";
						rowset rstRiders = DB.executeQuery(riderQuery);
						if(rstRiders.next() == true)
						{
							//Delete only those riders which are not selected
							DB.executeDML("DELETE FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + strProposal + "' AND NPR_BASICFLAG='N' AND NPR_SELECTED <> 'Y'");
						}
					}
				}
			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
			
		}
	
		private void generateRiders()
		{
			object objProposal = SessionObject.Get("NP1_PROPOSAL");
			try
			{
				if (objProposal == null || objProposal.ToString().Equals(""))
				{
					throw new ProcessException("Please Select A Proposal First");
				}
				else
				{
					string strProposal = objProposal.ToString();

					double dblFaceValue = this.getDouble("NPR_SUMASSURED");
					double dblBenifitTerm = this.getDouble("NPR_BENEFITTERM");
					double dblPremiumTerm = this.get("NPR_PREMIUMTER")==null?0:this.getDouble("NPR_PREMIUMTER");
					if(dblPremiumTerm==0)
						dblPremiumTerm = dblBenifitTerm;

					//20 jan 2009 - start
					//string strProduct = this.getDouble("PPR_PRODCD");
					//string sql = "select CHECK_VALIDATION('Y','"+strProposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+sa+","+(dbopt==""?"null":dbopt)+") from dual"; 
					//20 jan 2009 - end
					

					DB.executeDML("Update lnpr_product set ppr_prodcd='" + getString("PPR_PRODCD") + "', npr_life='F', NPR_PREMIUM=0 where np1_proposal='" + strProposal + "' and np2_setno = 1 and npr_basicflag='Y' ") ;

					///******** If riders exist then return ********/
					//rowset rsRider = DB.executeQuery("SELECT count(*) cnt FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + strProposal + "' and NVL(npr_basicflag,'N')='N'");
					//if(rsRider.next() && rsRider.getDouble("cnt")>0)
					//{
					//	return;
					//}					
					//DB.executeDML("Delete from lnpr_product where np1_proposal='" + strProposal + "' and np2_setno=1 and NVL(npr_basicflag,'N')='N'");

					String strBasicPlan = " Select ppr_prodcd,nvl(NPR_BENEFITTERM,0) NPR_BENEFITTERM, nvl(NPR_PREMIUMTER,0) NPR_PREMIUMTER, nvl(NPR_SUMASSURED,0) NPR_SUMASSURED from lnpr_product " +
						" Where np1_proposal='"+ strProposal +"' and NPR_BASICFLAG='Y'";

					rowset rstBasicPlan = DB.executeQuery(strBasicPlan);
					if (rstBasicPlan.next())
					{
						string strProduct  = rstBasicPlan.getString("PPR_PRODCD");
						double SumAssured  = rstBasicPlan.getDouble("NPR_SUMASSURED");
						int    BenefitTerm = rstBasicPlan.getInt("NPR_BENEFITTERM");

						rowset rsSelectedRiders = DB.executeQuery("SELECT PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+ strProposal +"' AND NP2_SETNO=1 AND NPR_BASICFLAG='N' AND NPR_SELECTED='Y'");
						string selectedRidersList = "";
						while (rsSelectedRiders.next())
						{
							selectedRidersList += rsSelectedRiders.getString("PPR_PRODCD") + ",";
						}

						string strRider = " Select pri.ppr_rider, NVL(pri.pri_builtin,'N') PRI_BUILTIN, decode(ppr.PPR_COMMLOADINGENABLED,'Y', GET_SYSPARA.GET_VALUE('DEFLT','DEF_DBOPTION'), '0') DBOPTION " +
							              " from lpri_rider pri, lppr_product ppr where ppr.ppr_prodcd=pri.ppr_rider and pri.ppr_prodcd='" + strProduct + "'";
						rowset rstRiders = DB.executeQuery(strRider);

						

						while (rstRiders.next())
						{
							//DB.executeDML("delete from lnpr_product where np1_proposal = '" + strProposal + "' and np2_setno = 1 and ppr_prodcd = '" + rstRiders.getString("PPR_RIDER") +"'" );
							
							//*************************************************************************************************//
							//**** Insert Rider if it is not present in Selected Rider List(means selected in LNPR_PRODUCT) ***//
							//*************************************************************************************************//
							if(selectedRidersList.IndexOf(rstRiders.getString("PPR_RIDER")) == -1)
							{
								String strInsert ="";
								int    riderBenTerm  = 0;
								double riderSumAss   = 0;
								string riderSelected = "N";
								string riderBuiltin  = "";

								if(rstRiders.getString("PRI_BUILTIN").ToUpper() == "Y")
								{
									riderBenTerm  = BenefitTerm;
									riderSumAss   = SumAssured;
									riderSelected = "Y";
									riderBuiltin  = "Y";
								}

								strInsert = " Insert into lnpr_product (np1_proposal, np2_setno, ppr_prodcd, npr_basicflag,npr_life,ccb_code, NPR_PREMIUMTER, NPR_BENEFITTERM, NPR_SUMASSURED,NPR_BUILTIN,NPR_SELECTED, NPR_COMMLOADING) " +
									" values ('" + strProposal + "',1,'" + rstRiders.getString("PPR_RIDER") + "','N',GET_SYSPARA.GET_VALUE('GLOBL','DEF_LIFE'),GET_SYSPARA.GET_VALUE('GLOBL','DEF_CALC_BASIS'), " +
									" 0, "+riderBenTerm+", "+riderSumAss+", '" + riderBuiltin + "','" + riderSelected + "', decode('" + rstRiders.getString("DBOPTION") + "','0',null,'" + rstRiders.getString("DBOPTION") + "') )";
								DB.executeDML(strInsert);
							}
						}
					}
					else
					{
						throw new ProcessException("Please Enter The Basic Plan, Before Generating Riders");
					}
				}
			}
			catch(Exception e)
			{
				throw new ProcessException (e.Message);
			}
		}

		private void updateRiders()
		{
			bool anyRiderRemoved = false;
			object objProposal = SessionObject.Get("NP1_PROPOSAL");
			try
			{
				if (objProposal == null || objProposal.ToString().Equals(""))
				{
					throw new ProcessException("Please Select A Proposal First");
				}
				else
				{
					string planProposal = objProposal.ToString();
					string planProdCode = Convert.ToString(get("PPR_PRODCD"));
					double planBenTerm  = Convert.ToDouble(get("NPR_BENEFITTERM"));

					DateTime CommDate = clsIlasUtility.getCommencementDate(planProposal);
					

					//***************************************************************************//
					//**************** Get only Selected Riders *********************************//
					//***************************************************************************//
					string strRider = " SELECT LPRI.PPR_RIDER,                             " +
						              "        NVL(LPRI.PRI_BUILTIN,'N') PRI_BUILTIN,      " + 
						              "        NVL(LNPR.NPR_BENEFITTERM,0) NPR_BENEFITTERM " +
						              " FROM   LNPR_PRODUCT LNPR, LPRI_RIDER LPRI          " + 
						              " WHERE  LNPR.PPR_PRODCD   = LPRI.PPR_RIDER          " +
						              "   AND  LNPR.NP1_PROPOSAL ='" + planProposal + "'   " +
						              "   AND  LPRI.PPR_PRODCD   ='" + planProdCode + "'   " +
						              "   AND  LNPR.NPR_SELECTED ='Y'                      " +
						              "   AND  LNPR.NPR_BASICFLAG='N'  ORDER BY 1          " ;
					
					rowset rsRiders = DB.executeQuery(strRider);

					while (rsRiders.next())
					{
						string riderCode = rsRiders.getString("PPR_RIDER");
						double riderBenTerm = rsRiders.getDouble("NPR_BENEFITTERM");


						if(rsRiders.getString("PRI_BUILTIN").ToUpper() == "Y")
						{
							//******** Update Builtin Rider  ***********//
							riderBenTerm = planBenTerm;
							int intRiderBenTerm = Convert.ToInt16(riderBenTerm);
							DateTime riderMatDate  = new DateTime(CommDate.Year+intRiderBenTerm, CommDate.Month, CommDate.Day);

							SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
							pc.puts("@NPR_BENEFITTERM",  riderBenTerm);
							pc.puts("@NPR_MATURITYDATE", riderMatDate);
							pc.puts("@NP1_PROPOSAL",     planProposal);
							pc.puts("@PPR_PRODCD",       riderCode);
							DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_MATURITYDATE=? WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ", pc);
						}
						else
						{
							//if(riderBenTerm > planBenTerm)
							{
								//******** Un select the Normal Rider  ***********//
								DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_SELECTED='N', NPR_BENEFITTERM=0, NPR_PREMIUMTER=0, NPR_SUMASSURED=0, NPR_TOTPREM=0, NPR_PREMIUM=0, NPR_PREMIUM_FC=0, NPR_PREMIUM_AV=0, NPR_MATURITYDATE=null WHERE NP1_PROPOSAL='"+ planProposal +"' AND NP2_SETNO=1 AND PPR_PRODCD='" + riderCode + "'");
								anyRiderRemoved = true;
							}
						}
					}
				}
			}
			catch(Exception e)
			{
				throw new ProcessException (e.Message);
			}

			if(anyRiderRemoved == true)
			{
				this.strRidersUpdateInformation = "One or more riders did not meet the current criterion of Base Product(Plan). Please change riders accordingly from Rider Screen";
			}
			else
			{
				this.strRidersUpdateInformation = "";
			}
		}
		
		private void generateQuestionaire(string proposal, string product)
		{
			//string query ="SELECT LCQN.CQN_CODE CQN_CODE FROM LCQN_QUESTIONNAIRE LCQN, LPQN_QUESTIONNAIRE LPQN WHERE PPR_PRODCD=? AND LCQN.CQN_CODE=LPQN.CQN_CODE";
			string query = " SELECT CQN_CODE FROM LPQN_QUESTIONNAIRE WHERE PPR_PRODCD=? ";
			SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
			pc.clear();
			pc.puts("@PPR_PRODCD", product,  Types.VARCHAR);
			rowset rsMasterQuestionaire = DB.executeQuery(query, pc);
			
			if(rsMasterQuestionaire.size()<1)
			{
				throw new ProcessException("Question(s) not found in master table [LCQN_QUESTIONNAIRE]");
			}
			else
			{
				query = "INSERT INTO LNQN_QUESTIONNAIRE(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CQN_CODE) VALUES(?,?,?,?)";
				

				while(rsMasterQuestionaire.next())
				{
					pc.clear();
					pc.puts("@NP1_PROPOSAL", proposal, Types.VARCHAR);
					pc.puts("@NP2_SETNO", 1,  Types.INTEGER);
					pc.puts("@PPR_PRODCD", product,  Types.VARCHAR);
					pc.puts("@CQN_CODE", rsMasterQuestionaire.getString("CQN_CODE"),  Types.VARCHAR);
					DB.executeDML(query, pc);
				}
			}
		}
		
		#endregion
	
		#region Validation from setup
		private void validateInput()
		{
			try
			{
				ValidationCall objValidation = new ValidationCall();
				objValidation.validatePlan(Convert.ToString(SessionObject.Get("NP1_PROPOSAL")),1,Convert.ToString(get("PPR_PRODCD")));
				objValidation.validateBenefit(Convert.ToString(SessionObject.Get("NP1_PROPOSAL")));
			}
			catch(Exception e)
			{
				throw new FieldValidationException(e.Message);
			}
		}
  

        #endregion

        #region old method
        /*private void ValidateRangeValues()
			{
				rowset rsValidate = null;

				if(getString("CCB_CODE") == "T")
				{   //Total Premium Validation
					rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','TOTPREM',"+get("NPR_TOTPREM")+","+get("NPR_PAIDUPTOAGE")+","+get("NPR_BENEFITTERM")+") from dual");
					if(rsValidate.next())
						if (rsValidate.getObject(1)!=null)
							throw new ProcessException("Total Premium Amount: "+rsValidate.getString(1));
				}
				else
				{   //Sum Assured Validation
					rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','SUMASSURED',"+get("NPR_SUMASSURED")+","+get("NPR_PAIDUPTOAGE")+","+get("NPR_BENEFITTERM")+") from dual");
					if(rsValidate.next())
						if (rsValidate.getObject(1)!=null)
							throw new ProcessException("Sum Assured Amount: "+rsValidate.getString(1));
				}

				rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','BTERM',"+get("NPR_BENEFITTERM")+","+get("NPR_PAIDUPTOAGE")+","+get("NPR_BENEFITTERM")+") from dual");
				if(rsValidate.next())
					if (rsValidate.getObject(1)!=null)
						throw new ProcessException("Benefit Term: "+rsValidate.getString(1));
			
				rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','MATURITYAGE',"+get("NPR_PAIDUPTOAGE")+","+get("NPR_PAIDUPTOAGE")+","+get("NPR_BENEFITTERM")+") from dual");
				if(rsValidate.next())
					if (rsValidate.getObject(1)!=null)
						throw new ProcessException("Premium Paid upto Age: "+rsValidate.getString(1));
			}*/
        #endregion
    }
}