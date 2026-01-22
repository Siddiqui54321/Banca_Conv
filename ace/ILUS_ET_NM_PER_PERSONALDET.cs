using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace ace
{
	
	public class ILUS_ET_NM_PER_PERSONALDET:shgn.SHGNCommand
	{
		
		EnvHelper env = new EnvHelper();

		//public override void fsoperationAfterSave()
		public override void fsoperationBeforeSave()
		{

		}
     
		public override void fsoperationAfterSave()
		{
			if(Convert.ToString(this.get("NPH_INSUREDTYPE")) == "Y")
			{
				deleteSecondLife();
			}

			fscreateModelLinkPersonalDet();
			//updatePolicyHolder(Convert.ToString(env.getAttribute("_pk_NPH_CODE")));
			generateClientAddress();

			//************* Activity Log *************//
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROFILE_UPDATED);
		}

		public override void fsoperationAfterUpdate()
		{
			//Apply Recalculated Retirement Age in table first
			/*string proposal  = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
			rowset rs = DB.executeQuery("select NP2_AGEPREM from LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + proposal +"'");
			if(rs.next())
			{
				int maturityAge = rs.getInt("NP2_AGEPREM") + 21;
				DB.executeDML("Update lnp1_policymastr SET Np1_Retirementage="+maturityAge+" WHERE NP1_PROPOSAL='"+proposal+"'");
			}
			*/

			fscreateModelLinkPersonalDet();
			
			/* January 2, 2011 
			bool isPolicyInsured = ((""+this.get("NPH_INSUREDTYPE")).Equals("N")?false:true);
			//if policy owner is insured then delete the second record for NU1_LIFE="S"
			if (isPolicyInsured)
				fsdeleteModelPersonalDetInsSecondLife();
			*/
			if(Convert.ToString(this.get("NPH_INSUREDTYPE")) == "Y")
			{
				deleteSecondLife();
			}
			

			//Reset Plan fields and Delete Riders 
			String proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
			DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=0, NPR_PREMIUMTER=0, NPR_SUMASSURED=0, NPR_TOTPREM=0, NPR_PREMIUM=0, NPR_PREMIUM_FC=0, NPR_PREMIUM_AV=0 WHERE NP1_PROPOSAL='" + proposal + "' AND NP2_SETNO = 1 AND NPR_BASICFLAG='Y' ") ;
			DB.executeDML("DELETE FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + proposal + "' AND NP2_SETNO=1 AND NVL(NPR_BASICFLAG,'N')='N'");
			string companyCode = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["CompanyCodes"]);
			if (companyCode.Contains(SessionObject.Get("s_ccd_code").ToString()))
			{
				DB.executeDML("delete from lnas_assignee where np1_proposal='" + proposal + "' and np1_assignmentcd =(select nvl(np1_assignmentcd,0) from lcch_channel where ccd_code='" + SessionObject.Get("s_ccd_code").ToString() + "')");
			}

			generateClientAddress();

			//Recalculate Premium
			//string proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
			//RecalculatePemium(proposal);

			//************* Activity Log *************//
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROFILE_UPDATED);
		}


		//private void fsdeleteModelPersonalDetInsSecondLife()
		private void deleteSecondLife()
		{
			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			rowset rsLNU1_UNDERWRITI = DB.executeQuery("select NPH_CODE, NU1_LIFE from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='S'");
			try
			{
				DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='S'");
				while (rsLNU1_UNDERWRITI.next())
				{
					pm.clear();
					pm.puts("@NPH_CODE", rsLNU1_UNDERWRITI.getString(1));
					pm.puts("@NPH_LIFE", rsLNU1_UNDERWRITI.getString(2));
			
					//DB.executeDML("delete from lnu1_underwriti where NPH_CODE=? and NPH_LIFE=?", pm);
					DB.executeDML("delete from lnph_pholder where NPH_CODE=? and NPH_LIFE=?", pm);
				}

				SessionObject.Remove("NPH_CODE_s");
				SessionObject.Remove("NPH_LIFE_s");
			}
			catch(Exception e)
			{
				string ex = e.StackTrace;
			}
		}

		/*private void generateClientAddress()
		{
			string BA_Proposal   = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
			string BA_ClientCode = Convert.ToString(env.getAttribute("_pk_NPH_CODE"));
			string BA_ClientID   = ace.clsIlasUtility.getClientID1FromBanca(BA_ClientCode);
			string user          = Convert.ToString(env.getAttribute("s_USE_USERID"));

			if(ClientExistInIlas(BA_ClientID) != 0)
			{
				string qry = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO)='"+BA_ClientID+"'";
				if(clsIlasUtility.isNoorID())
				{
					qry = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO2)='"+BA_ClientID+"'";
				}
				rowset rs = DB.executeQuery(qry);
				if(rs.next())
				{
					//Delete old entries
					DB.executeDML("DELETE FROM LNAD_ADDRESS WHERE UPPER(NPH_CODE)=" + BA_ClientCode );

					//Get these from ILAS and insert into Banca
					string ILAS_ClientCode = rs.getString("NPH_CODE");
					System.Text.StringBuilder query = new System.Text.StringBuilder();
					query.Append(" INSERT INTO LNAD_ADDRESS ");
					query.Append(" (NPH_CODE,NP1_PROPOSAL,NPH_LIFE,NAD_ADDRESSTYP,PFS_ACNTYEAR,OAC_ACTIVICODE,ARQ_REQUESTNO,USE_USERID,USE_TIMEDATE, ");
					query.Append(" NAD_POBOX,NAD_ADDRESS1,CCN_CTRYCD,NAD_ADDRESS2,CPR_PROVCD,CCT_CITYCD,NAD_ADDRESS3,NAD_TELNO1,NAD_TELNO2,NAD_FAXNO, ");
					query.Append(" NAD_MOBILE,NAD_PAGER,NAD_EMAIL1,NAD_EMAIL2,ARQ_REQUESTYPE,CONVERT,NPH_LASTCHANGEDBY,NPH_LASTCHNAGEDATE) ");
					query.Append("         ");
					query.Append(" SELECT  ");
					query.Append("'" + BA_ClientCode + "','" + BA_Proposal + "',NPH_LIFE,NAD_ADDRESSTYP,PFS_ACNTYEAR,OAC_ACTIVICODE,ARQ_REQUESTNO,'" + user +"',SYSDATE, ");
					query.Append(" NAD_POBOX,NAD_ADDRESS1,CCN_CTRYCD,NAD_ADDRESS2,CPR_PROVCD,CCT_CITYCD,NAD_ADDRESS3,NAD_TELNO1,NAD_TELNO2,NAD_FAXNO, ");
					query.Append(" NAD_MOBILE,NAD_PAGER,NAD_EMAIL1,NAD_EMAIL2,ARQ_REQUESTYPE,CONVERT,NPH_LASTCHANGEDBY,NPH_LASTCHNAGEDATE ");
					query.Append(" FROM ILAS_LNAD_ADDRESS WHERE NPH_CODE=" + ILAS_ClientCode );
					DB.executeDML(query.ToString());
				}
			}
		}*/
		private void generateClientAddress()
		{
			string BA_Proposal   = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
			string BA_ClientCode = Convert.ToString(env.getAttribute("_pk_NPH_CODE"));
			string user          = Convert.ToString(env.getAttribute("s_USE_USERID"));

			if(searchClientByCode_ILAS(BA_ClientCode) == true)
			{
				//Delete old entries
				DB.executeDML("DELETE FROM LNAD_ADDRESS WHERE UPPER(NPH_CODE)=" + BA_ClientCode );

				//Insert from ILAS into BANCA
				System.Text.StringBuilder query = new System.Text.StringBuilder();
				query.Append("INSERT INTO LNAD_ADDRESS ");
				query.Append("     (NPH_CODE,NP1_PROPOSAL,NPH_LIFE,NAD_ADDRESSTYP,PFS_ACNTYEAR,OAC_ACTIVICODE,ARQ_REQUESTNO,USE_USERID,USE_TIMEDATE, ");
				query.Append("      NAD_POBOX,NAD_ADDRESS1,CCN_CTRYCD,NAD_ADDRESS2,CPR_PROVCD,CCT_CITYCD,NAD_ADDRESS3,NAD_TELNO1,NAD_TELNO2,NAD_FAXNO, ");
				query.Append("      NAD_MOBILE,NAD_PAGER,NAD_EMAIL1,NAD_EMAIL2,ARQ_REQUESTYPE,CONVERT,NPH_LASTCHANGEDBY,NPH_LASTCHNAGEDATE) ");
				query.Append("SELECT  ");
				query.Append("      NPH_CODE,'" + BA_Proposal + "',NPH_LIFE,NAD_ADDRESSTYP,PFS_ACNTYEAR,OAC_ACTIVICODE,ARQ_REQUESTNO,'" + user +"',SYSDATE, ");
				query.Append("      NAD_POBOX,NAD_ADDRESS1,CCN_CTRYCD,NAD_ADDRESS2,CPR_PROVCD,CCT_CITYCD,NAD_ADDRESS3,NAD_TELNO1,NAD_TELNO2,NAD_FAXNO, ");
				query.Append("      NAD_MOBILE,NAD_PAGER,NAD_EMAIL1,NAD_EMAIL2,ARQ_REQUESTYPE,CONVERT,NPH_LASTCHANGEDBY,NPH_LASTCHNAGEDATE ");
				query.Append("      FROM ILAS_LNAD_ADDRESS WHERE NPH_CODE="+BA_ClientCode );
				DB.executeDML(query.ToString());
			}
		}


		//Create linking entry in lnu1_underwriti
		private void fscreateModelLinkPersonalDet()
		{
			String item1  = ""+env.getAttribute("NP1_PROPOSAL");
			String item2  = ""+env.getAttribute("_pk_NPH_CODE");
			String smoker = ""+env.getAttribute("NU1_SMOKER");
			String bankCode = ""+env.getAttribute("NP1_CHANNEL");
			String branchCode = ""+env.getAttribute("NP1_CHANNELDETAIL");
			String accountNo = ""+env.getAttribute("NU1_ACCOUNTNO");
            String ibanNo = "" + env.getAttribute("NU1_IBAN");


            String NPH_WEIGHTUOM=""+env.getAttribute("NPH_WEIGHTUOM");
			String NPH_WEIGHTACTUAL=""+env.getAttribute("NPH_WEIGHTACTUAL");
			String NPH_WEIGHT =""+env.getAttribute("NPH_WEIGHT");
				
			String NPH_HEIGHTUOM=""+env.getAttribute("NPH_HEIGHTUOM");
			String NPH_HEIGHTACTUAL=""+env.getAttribute("NPH_HEIGHTACTUAL");
			String NPH_HEIGHT=""+env.getAttribute("NPH_HEIGHT");
			DateTime dtOB = (System.DateTime)this.get("NPH_BIRTHDATE");
			String NU1_OVERUNDERWT=""+env.getAttribute("NU1_OVERUNDERWT");		


			bool isPolicyInsured =((""+this.get("NPH_INSUREDTYPE")).Equals("N")?false:true);
			
			


			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL", env.getAttribute("NP1_PROPOSAL"));
			pm.puts("@NPH_CODE", env.getAttribute("_pk_NPH_CODE"));
			pm.puts("@NPH_LIFE","D");
			pm.puts("@NU1_LIFE","F");
			

			pm.puts("@NU1_HEIGHT",Convert.ToDouble(NPH_HEIGHT));
			pm.puts("@NU1_WEIGHT",Convert.ToDouble(NPH_WEIGHT));
			
			//PAYER
			//01/01/2011 - stop by zulfi bhai
			if(isPolicyInsured)
			{
				pm.puts("@NU1_PAYER","B");
			}
			else
			{
			    pm.puts("NU1_PAYER","O");
			}
			//pm.puts("@NU1_PAYER","B");

			pm.puts("@NU1_OVERUNDERWT",Convert.ToDouble(NU1_OVERUNDERWT));
			pm.puts("@NU1_SMOKER",smoker);
			
			pm.puts("@NU1_HEIGHTUOM",NPH_HEIGHTUOM);
			pm.puts("@NU1_HEIGHTACTUAL",Convert.ToDouble(NPH_HEIGHTACTUAL));
			
			pm.puts("@NU1_WEIGHTUOM", NPH_WEIGHTUOM);
			pm.puts("@NU1_WEIGHTACTUAL",Convert.ToDouble(NPH_WEIGHTACTUAL));
			
			pm.puts("@NU1_ACCOUNTNO",accountNo);
            pm.puts("@NU1_IBAN", ibanNo); // Imran
            pm.puts("@PBK_BANKCODE",bankCode);
			pm.puts("@PBB_BRANCHCODE",branchCode);
					
			////Hegiht,Weight and BMI
			
			if(isPolicyInsured)
			{
				//pm.puts("@NU1_PAYER","B");
				//prepare lnu1_underwriti for new entry, so delete old entry if exists in case of changing persons on proposal
				DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='F'");
				//DB.executeDML("insert into lnu1_underwriti(NP1_PROPOSAL, NPH_CODE, NPH_LIFE,NU1_LIFE,NU1_SMOKER, PBK_BANKCODE, PBB_BRANCHCODE,NU1_ACCOUNTNO,NU1_PAYER) values (?,?,?,?,?,?,?,?,?)", pm); 
				DB.executeDML("insert into lnu1_underwriti (np1_proposal,nph_code,nph_life,nu1_life,nu1_height,nu1_weight,nu1_payer,nu1_overunderwt,nu1_smoker,nu1_heightuom,nu1_heightactual,nu1_weightuom,nu1_weightactual,nu1_accountno,NU1_IBAN,pbk_bankcode,pbb_branchcode) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", pm);
				int agePrem =	ace.Ace_General.calculate_Age_InYear(dtOB);
				DB.executeDML("update lnp2_policymastr set np2_ageprem=" + agePrem + " Where np1_proposal = '"+env.getAttribute("NP1_PROPOSAL")+"' ");
			}
			else
			{
				//prepare lnu1_underwriti for new entry, so delete old entry if exists in case of changing persons on proposal
				DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='F'");
				//pm.puts("NU1_PAYER","O");
				//DB.executeDML("insert into lnu1_underwriti (NP1_PROPOSAL, NPH_CODE, NPH_LIFE, NU1_LIFE,NU1_SMOKER, PBK_BANKCODE, PBB_BRANCHCODE,NU1_HEIGHTUOM,NU1_HEIGHTACTUAL,NU1_WEIGHTUOM,NU1_WEIGHTACTUAL,NU1_ACCOUNTNO,NU1_OVERUNDERWT) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", pm);
			    DB.executeDML("insert into lnu1_underwriti (np1_proposal,nph_code,nph_life,nu1_life,nu1_height,nu1_weight,nu1_payer,nu1_overunderwt,nu1_smoker,nu1_heightuom,nu1_heightactual,nu1_weightuom,nu1_weightactual,nu1_accountno,NU1_IBAN,pbk_bankcode,pbb_branchcode) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", pm);
				//DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='F'");
				//pm.puts("NU1_PAYER","I");
				//DB.executeDML("insert into lnu1_underwriti (NP1_PROPOSAL, NPH_CODE, NPH_LIFE, NU1_LIFE,NU1_SMOKER, PBK_BANKCODE, PBB_BRANCHCODE, NU1_ACCOUNTNO,NU1_PAYER) values (?,?,?,?,?,?,?,?,?)", pm);
			}

			//Update ID Number 
			//string proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
		}

		#region static methods (Call by presentation code behind)
		public static string getClientNumber()
		{
			ProcedureAdapter call = new ProcedureAdapter("GENERATE_CLIENTNO_CALL");
			call.RegisetrOutParameter("CLIENTNO", OleDbType.VarChar, 1000 );
			call.Execute();
			return Convert.ToString(call.Get("CLIENTNO"));
		}

		public static string[] checkAndGetClientInfo(string strNIC, string eventType)
		{
            string[] result = new string[] { "N", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "N", "", "", "", "N", "Y" };
			

			//IMPORTANT NOTE: ID Searching criteria - That would be change on client basis
			string queryForID = " AND UPPER(NPH_IDNO)='"+strNIC.Replace("-","")+"'";

			bool blnNoorId = ace.clsIlasUtility.isNoorID();
			if(blnNoorId == true)
			{	//NOTE: In ILAS Noor ID is Saved in NPH_IDNO2 but in Banca it is being saved in NPH_IDNO
				queryForID = " AND UPPER(NPH_IDNO2) LIKE '" + strNIC + "%'";
			}

			//*****************************************************************************************
			//******************************** Search in ILAS *****************************************
			//*****************************************************************************************

            string query = " SELECT PH.NPH_FATHERNAME,PH.NPH_MAIDENNAME,nvl(PH.NPH_DOCISSUEDAT,null) NPH_DOCISSUEDAT,nvl(PH.NPH_DOCEXPIRDAT,null)NPH_DOCEXPIRDAT,PH.NPH_CODE, NPH_IDNO ,NPH_IDNO2, CSD_TYPE NPH_TITLE,NPH_FULLNAME,NPH_BIRTHDATE,NPH_SEX,COP_OCCUPATICD,CCL_CATEGORYCD, "
                         + "        NPH_HEIGHTUOM,NPH_HEIGHTACTUAL,NPH_HEIGHT,NPH_WEIGHTUOM,NPH_WEIGHTACTUAL,NPH_WEIGHT,CNT_NATCD,NU1_SMOKER "
                         + " FROM   ILAS_LNPH_PHOLDER PH LEFT OUTER JOIN LNU1_UNDERWRITI U1 ON PH.NPH_CODE =U1.NPH_CODE "
                         + "       , LCSD_SYSTEMDTL SD  "
                         + " WHERE  PH.NPH_TITLE=SD.CSD_VALUE AND CSH_ID='TITLE' " + queryForID + " ORDER BY NPH_CODE DESC ";
            try
			{
				//******* This flag will be use in Screen to disable some columns **********
				string HaveAnyValidatedProposal = "N";
				bool PersonFound = false;
				rowset rsPHolder = DB.executeQuery(query);
				if(rsPHolder.next())
				{
					PersonFound = true;
				}
				else
				{	
					if(blnNoorId == true)
					{	//NOTE: In Banca Noor ID saved in NPH_IDNO while in ILAS is in NPH_IDNO2 
						queryForID = " AND NPH_IDNO LIKE '" + strNIC + "%'";
					}

					//*****************************************************************************************
					//******************************** Search in BANCA ****************************************
					//*****************************************************************************************

                    query = "SELECT PH.NPH_FATHERNAME,PH.NPH_MAIDENNAME,nvl(PH.NPH_DOCISSUEDAT,null) NPH_DOCISSUEDAT,nvl(PH.NPH_DOCEXPIRDAT,null)NPH_DOCEXPIRDAT,PH.NPH_CODE, NPH_IDNO ,NPH_IDNO2, CSD_TYPE NPH_TITLE,NPH_FULLNAME,NPH_BIRTHDATE,NPH_SEX,COP_OCCUPATICD,CCL_CATEGORYCD, "
                          + "       NPH_HEIGHTUOM,NPH_HEIGHTACTUAL,NPH_HEIGHT,NPH_WEIGHTUOM,NPH_WEIGHTACTUAL,NPH_WEIGHT,CNT_NATCD,NU1_SMOKER "
                          + "FROM   LNPH_PHOLDER PH LEFT OUTER JOIN LNU1_UNDERWRITI U1 ON PH.NPH_CODE =U1.NPH_CODE "
                          + "     , LCSD_SYSTEMDTL SD "
                          + "WHERE  PH.NPH_TITLE=SD.CSD_TYPE AND CSH_ID='TITLE' " + queryForID + " ORDER BY NPH_CODE DESC ";

                    rsPHolder = DB.executeQuery(query);
					if(rsPHolder.next())
					{
						if(eventType.ToUpper() == "NEW")
						{
							PersonFound = true;
						}
						else
						{
							rowset rs = DB.executeQuery("select COUNT(*) AS CODE_FOUND FROM LNPH_PHOLDER WHERE UPPER(NPH_IDNO) LIKE '" + strNIC + "%'");
							if(rs.next())
							{
								int intCount = rs.getInt("CODE_FOUND");
								if(intCount > 1)
								{	//Do not inclue current Policy holder itself
									PersonFound = true;
								}
							}
						}

					}
				}

				if(PersonFound == true)
				{
					/******* Check either this client has any validated proposal or not *********/
					string customerID = rsPHolder.getString("NPH_CODE");
					query = "SELECT 'A' FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL IN(SELECT NP1_PROPOSAL from LNU1_UNDERWRITI A WHERE NPH_CODE='" + customerID + "') AND NP2_SUBSTANDAR IS NOT NULL ";
					rowset rs = DB.executeQuery(query);
					if(rs.next())
					{
						HaveAnyValidatedProposal = "Y";
					}
                                        
                    DateTime dtDOB = rsPHolder.getDate("NPH_BIRTHDATE");
                    DateTime dtCNICISSUEDATE = default(DateTime);
                    DateTime dtCNICEXPIRYDATE = default(DateTime);


                    if (rsPHolder.getString("NPH_MAIDENNAME") != null || rsPHolder.getString("NPH_FATHERNAME") != null)
                    {
                       dtCNICISSUEDATE = rsPHolder.getDate("NPH_DOCISSUEDAT");
                       dtCNICEXPIRYDATE = rsPHolder.getDate("NPH_DOCEXPIRDAT");
                    }

					result[0] = "Y"; //Person found
					result[1] = rsPHolder.getString("NPH_TITLE"); //Title
					result[2] = rsPHolder.getString("NPH_FULLNAME"); //Name
                    result[3] = dtCNICISSUEDATE.Day.ToString().PadLeft(2, '0') + "/" + dtCNICISSUEDATE.Month.ToString().PadLeft(2, '0') + "/" + dtCNICISSUEDATE.Year.ToString().PadLeft(4, '0');//CNICISSUEDATE
                    result[4] = dtCNICEXPIRYDATE.Day.ToString().PadLeft(2, '0') + "/" + dtCNICEXPIRYDATE.Month.ToString().PadLeft(2, '0') + "/" + dtCNICEXPIRYDATE.Year.ToString().PadLeft(4, '0');//CNICEXPIRYDATE
                    result[5] = rsPHolder.getString("NPH_FATHERNAME") == null ? "" : rsPHolder.getString("NPH_FATHERNAME");  //FATHERNAME
                    result[6] = rsPHolder.getString("NPH_MAIDENNAME") == null ? "" : rsPHolder.getString("NPH_MAIDENNAME");  //MOTHERNAME

					result[7] = dtDOB.Day.ToString().PadLeft(2,'0') + "/" + dtDOB.Month.ToString().PadLeft(2,'0') + "/" + dtDOB.Year.ToString().PadLeft(4,'0');//DOB
					result[8] = rsPHolder.getString("NPH_SEX"); //Gender
					result[9] = rsPHolder.getString("COP_OCCUPATICD"); //occupation
					result[10] = rsPHolder.getString("CCL_CATEGORYCD"); //Occupation class
					
					result[11] = rsPHolder.getString("NPH_HEIGHTUOM"); //Height Type 
					//result[8] = rsPHolder.getString("NPH_HEIGHTACTUAL"); //Height Entered(Actual)
					//result[9] = rsPHolder.getString("NPH_HEIGHT"); //Height
					result[12] = System.Convert.ToString(Math.Round(rsPHolder.getDouble("NPH_HEIGHTACTUAL"),3)); //Height Entered(Actual)
					result[13] = System.Convert.ToString(Math.Round(rsPHolder.getDouble("NPH_HEIGHT"),2)); //Height


					result[14] = rsPHolder.getString("NPH_WEIGHTUOM"); //Weight Type 
					//result[11] = rsPHolder.getString("NPH_WEIGHTACTUAL"); //Weight Entered(Actual)
					//result[12] = rsPHolder.getString("NPH_WEIGHT"); //Weight
					result[15] = System.Convert.ToString(Math.Round(rsPHolder.getDouble("NPH_WEIGHTACTUAL"),3)); //Weight Entered(Actual)
					result[16] = System.Convert.ToString(Math.Round(rsPHolder.getDouble("NPH_WEIGHT"),2)); //Weight
					
					
					result[17] = rsPHolder.getObject("NPH_CODE")== null ? "" : rsPHolder.getString("NPH_CODE"); //Client Code
					result[18] = HaveAnyValidatedProposal;
					result[19] = rsPHolder.getObject("NPH_IDNO")== null ? "" : rsPHolder.getString("NPH_IDNO");//ID1
					result[20] = rsPHolder.getObject("NPH_IDNO2")== null ? "" : rsPHolder.getString("NPH_IDNO2");//ID2
					result[21] = rsPHolder.getObject("CNT_NATCD")== null ? "" : rsPHolder.getString("CNT_NATCD");//Nationality
					result[22] = rsPHolder.getObject("NU1_SMOKER")== null ? "" : rsPHolder.getString("NU1_SMOKER");//Smoker
					//Update the client is allowed or not
					result[23] = isClientUpdateAllowed(strNIC);
				}			
			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
			return result;
		}



		public static int ClientExistInIlas(string ID)
		{
			string query = "";
			int returnValue=0;
			if(ace.Ace_General.getApplicationName().Equals("BANCASSURANCE"))
			{
				if(ace.clsIlasUtility.isNoorID() == false)
				{
					query = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO)='" + ID + "'";
				}
				else
				{
					query = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO2)='" + ID + "'";
				}

				rowset rs = DB.executeQuery(query);
				if(rs.next())
				{
					returnValue= rs.getInt("NPH_CODE");
				}
				else
				{
					returnValue=0;
				}
			}
			return returnValue;
		}



		public static int ClientExist(string NIC)
		{
			string query = "SELECT NPH_CODE FROM LNPH_PHOLDER WHERE NPH_IDNO='" + NIC + "'";
			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				return rs.getInt("NPH_CODE");
			}
			else
			{
				return 0;
			}
		}

		public static bool searchClientByCode_BANCA(string nphCode)
		{
			string query = "SELECT 'A' FROM LNPH_PHOLDER WHERE NPH_CODE="+nphCode ;
			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool searchClientByCode_ILAS(string nphCode)
		{
			bool returnValue=false;
			if(ace.Ace_General.getApplicationName().Equals("BANCASSURANCE") )
			{
				string query = "SELECT 'A' FROM ILAS_LNPH_PHOLDER WHERE NPH_CODE="+nphCode;
				rowset rs = DB.executeQuery(query);
				if(rs.next())
				{
					returnValue= true;
				}
				else
				{
					returnValue= false;
				}
			}
			return returnValue;

		}



		public static string isClientUpdateAllowed(string ID)
		{
			string query = "";
			if(ace.clsIlasUtility.isNoorID() == false)
			{
				query = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO) like '" + ID + "%'";
			}
			else
			{
				query = "SELECT NPH_CODE FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO2) like '" + ID + "%'";
			}

			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				return "N";
			}
			else
			{
				return "Y";
			}
		}

		#endregion

	}
	//END OF CLASS
}