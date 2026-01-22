using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace ace
{
	
	public class POLICY_ACCEPTANCE:shgn.SHGNCommand
	{
		private const string APPROVED="001";
		private const string DECLINED="002";
		EnvHelper env = new EnvHelper();

		#region Framework Methods
		public override void fsoperationBeforeSave()
		{
			ValidatePolicy();
			ValidateBeneficiary();
			ValidateAccountNo();
			//This check is being stop by Zulfi Bhai, this has been convered in POLICY_ACCEPTANCE.calcTotalSumAtRisk
			//ValidateTSR();
			ValidatePremium();
			//ValidateInIllas();
		}

		public override void fsoperationAfterSave()
		{	
		}

		public override void fsoperationAfterUpdate()
		{
			deleteRider();
			transferPolicy();
		}
		public override void fsoperationBeforeUpdate()
		{
			ValidatePolicy();
			ValidateAccountNo();
			ValidateBeneficiary();
			//This check is being stop by Zulfi Bhai, this has been convered in POLICY_ACCEPTANCE.calcTotalSumAtRisk
			//ValidateTSR();
			ValidatePremium();
		}

		#endregion

		#region Validation
		private void ValidatePolicy()
		{
			string qryAddress = "SELECT CCN_CTRYCD, CCT_CITYCD, CPR_PROVCD, NAD_ADDRESS1 FROM LNAD_ADDRESS WHERE NAD_ADDRESSTYP='C' AND NP1_PROPOSAL = ? ";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",SessionObject.Get(("NP1_PROPOSAL")));
			rowset rs=DB.executeQuery(qryAddress,pc);
			if(rs.size()<=0)
			{
				throw new ProcessException("Correspondence Address not entered or saved");
			}
			while(rs.next())
			{
				if(rs.getObject("CCN_CTRYCD")==null || rs.getObject("NAD_ADDRESS1")==null)
				{
					throw new ProcessException("Correspondence Address not entered or saved");
				}
			}

		}

		private void ValidateAccountNo()
		{
			if(env.getAttribute("s_CCH_CODE").ToString().Equals("2"))
			{
				string qryAddress = "SELECT NU1_ACCOUNTNO ACCOUNTNO,NU1_IBAN IBANNO FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL =? AND NU1_LIFE='F' ";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@NP1_PROPOSAL",SessionObject.Get(("NP1_PROPOSAL")));
				//pc.puts("@NPH_CODE",env.getAttribute("s_NPH_CODE"));
				rowset rs=DB.executeQuery(qryAddress,pc);
				if(rs.size()<=0)
				{
					throw new ProcessException("Personal Information Missing");
				}
				while(rs.next())
				{
                    // if(rs.getObject("NU1_ACCOUNTNO")==null || rs.getObject("NU1_ACCOUNTNO").ToString().Equals(""))

                    // if ((rs.getObject("NU1_ACCOUNTNO") == null || rs.getObject("NU1_ACCOUNTNO").ToString().Trim().Equals(""))
                    //  && (rs.getObject("NU1_IBAN") == null || rs.getObject("NU1_IBAN").ToString().Trim().Equals("")))
                    if (rs.getObject("ACCOUNTNO") == null && rs.getObject("IBANNO") == null)

                    {
						throw new ProcessException("Account Number not Entered..");
					}
				}
			}

		}

		public void ValidateBeneficiary()
		{
			const string BY_PERCENT="02";
			bool blnPercentExist=false;
			double totPercent=0;
			
				string query = "select NBF_BASIS, NBF_PERCNTAGE from LNBF_BENEFICIARY where NP1_PROPOSAL=? ";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@NP1_PROPOSAL", SessionObject.Get(("NP1_PROPOSAL")));
				rowset rs=DB.executeQuery(query,pc);

				if(rs.size()<1)
				{			
					throw new ProcessException("Beneficiary is not defined.");				
				
				}
				
			    while(rs.next())
				{
					if(rs.getString("NBF_BASIS") == BY_PERCENT)
					{
						blnPercentExist = true;	
						totPercent += rs.getDouble("NBF_PERCNTAGE");
					}
				}

				if(blnPercentExist == true)
				{
					if(totPercent < 100 || totPercent>100)
					{
						throw new ProcessException("Total Beneficiary Percentage is " + totPercent + ". It should be 100%");
						
					}
					
				}
			
			
		}

		private void ValidateTSR()
		{
			string cnic="";
			if(env.getAttribute("s_NPH_IDNO")==null)
			{
				string qryIDNo = "SELECT NPH_IDNO FROM LNPH_PHOLDER PH INNER JOIN LNU1_UNDERWRITI U1 " +
					"ON PH.NPH_CODE = U1.NPH_CODE AND PH.NPH_LIFE = U1.NPH_LIFE AND PH.NPH_LIFE = 'D' " +
					"AND U1.NP1_PROPOSAL ='"+env.getAttribute("NP1_PROPOSAL").ToString()+"'";
				rowset rs1=DB.executeQuery(qryIDNo);
				if(rs1.next())
				{
					cnic = rs1.getObject("NPH_IDNO").ToString();
				}
			}
			else
				cnic = env.getAttribute("s_NPH_IDNO").ToString();

			ProcedureAdapter cs = new ProcedureAdapter("TSRFORIDNO_CALL");
			cs.Set("pIDNo", OleDbType.VarChar, cnic);
			cs.RegisetrOutParameter("code", OleDbType.Numeric);
			cs.Execute();
			Decimal prevSA = Convert.ToDecimal(cs.Get("code"));
			Decimal TSR=0;
			if(System.Configuration.ConfigurationSettings.AppSettings["TSR"]!=null)
			{
				TSR = Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings["TSR"]);
			}
			else
				throw new ProcessException("Total Sum at Risk not Defined");

			string saQry = "SELECT NPR_SUMASSURED FROM LNPR_PRODUCT WHERE NP1_PROPOSAL = '"+env.getAttribute("NP1_PROPOSAL").ToString()+"' " + 
				"AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPPR_PRODUCT L WHERE L.PPR_BASRIDR='B')";

			rowset rs = DB.executeQuery(saQry);
			Decimal currentSA=0;
			if(rs.next() && rs.getObject("NPR_SUMASSURED")!=null)
			{
				currentSA=Convert.ToDecimal(rs.getObject("NPR_SUMASSURED"));
			}
			Decimal tsrValue =Convert.ToDecimal(cs.Get("code"));
			if((currentSA+prevSA)>TSR )
			{
				throw new ProcessException("Total Sum Assured "+ (currentSA+prevSA)  +" is more then Sum at Risk "+TSR);
			}
			
		}

		public bool CheckIfPolicyApproved(string NP1_PROPOSAL)
		{
			//Sample Code.
			return(SHAB.Data.LNP1_POLICYMASTRDB.PolicyApproved(NP1_PROPOSAL));

			//			rowset CHECK_PREMIUM = DB.executeQuery("select NP1_PROPOSAL FROM lnp1_policymastr WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"' AND CDC_CODE='001'");
			//			if (CHECK_PREMIUM.next())
			//			{
			//				return true;
			//			}
			//			else
			//			{
			//			    return false;
			//			}
		}
		public bool CheckIfPolicyValidate(string NP1_PROPOSAL)
		{
			//Sample Code.
			/*if(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"])=="true")
			{
				return(SHAB.Data.LNP1_POLICYMASTRDB.PolicyValidated(NP1_PROPOSAL));
			}
			else
			{
				return false;
			}*/

			if(BA.BAUtility.Validate_Check() == false)
			{
				return false;
			}
			else
			{
				return(SHAB.Data.LNP1_POLICYMASTRDB.PolicyValidated(NP1_PROPOSAL));
			}
		
		}

		public void ValidatePremium()
		{
			string proposal = Convert.ToString(SessionObject.Get(("NP1_PROPOSAL")));
			string query = "SELECT NPR_PREMIUM FROM LNPR_PRODUCT WHERE NPR_BASICFLAG='Y' AND NP2_SETNO=1 AND NP1_PROPOSAL=? ";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs=DB.executeQuery(query,pc);
			if(rs.next())
			{
				if(rs.getObject("NPR_PREMIUM") != null)
				{
					if(rs.getDouble("NPR_PREMIUM") <= 0.0)
					{
						throw new Exception("Validated Premium - Premium must be greater than zero.");
					}
				}
				else
				{
					throw new Exception("Validated Premium - Premium not found.");
				}
			}
			else
			{
				throw new Exception("Validated Premium - Propsoal not found");
			}
		}

		public void ValidateBaseProduct()
		{
			string proposal = Convert.ToString(SessionObject.Get(("NP1_PROPOSAL")));
			string query = "SELECT NPR_PREMIUM,NPR_SUMASSURED FROM LNPR_PRODUCT WHERE NPR_BASICFLAG='Y' AND NP2_SETNO=1 AND NP1_PROPOSAL=? ";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs=DB.executeQuery(query,pc);
			if(rs.next())
			{
				if(clsIlasUtility.isNoorID())
				{
					if(rs.getObject("NPR_SUMASSURED") != null)
					{
						if(rs.getDouble("NPR_SUMASSURED") <= 0.0)
						{
							throw new Exception("Sum Assured must be greater than zero.");
						}
					}
					else
					{
						throw new Exception("Sum Assured not defined");
					}
				}

				if(rs.getObject("NPR_PREMIUM") != null)
				{
					if(rs.getDouble("NPR_PREMIUM") <= 0.0)
					{
						throw new Exception("Premium must be greater than zero.");
					}
				}
				else
				{
					throw new Exception("Premium not defined");
				}
			}
			else
			{
				throw new Exception("Base Product/Plan not defined");
			}
		}

		
		#endregion

		#region Transfer To Ilas Related Functions + Policy Number Generation
		private void deleteRider()
		{
			DB.executeDML("DELETE FROM LNPR_PRODUCT WHERE NP1_PROPOSAL = '" + SessionObject.Get(("NP1_PROPOSAL")).ToString() + "' AND NPR_BASICFLAG='N' AND NVL(NPR_PREMIUM,0)=0 AND NVL(NPR_SUMASSURED,0)=0");
		}

		private void transferPolicy()
		{
			/*
			ProcedureAdapter cs = new ProcedureAdapter("TRANSFER_BAPROPOSAL_CALL");
			cs.Set("PFROMPROPOSAL", OleDbType.VarChar, Convert.ToString(SessionObject.Get("NP1_PROPOSAL")));
			cs.Set("PTOPROPOSAL",   OleDbType.VarChar, Convert.ToString(SessionObject.Get("NP1_PROPOSAL")));
			cs.Execute();
			*/
			
			string proposal = Convert.ToString(SessionObject.Get("NP1_PROPOSAL"));
	
			string decision = ((string)System.Configuration.ConfigurationSettings.AppSettings["Transfer"]);
			string subsStandard ="";
				try
				{
					subsStandard = ((string)System.Configuration.ConfigurationSettings.AppSettings["TransferSub"]);
				}
				catch(Exception es)
				{
					
				}
			if(decision.Equals("Proposal") )
			{
				/*OleDbCommand cmd=(OleDbCommand) DB.CreateCommand("TRANSFER_BAPROPOSAL",DB.Connection);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.Add(new OleDbParameter("PFROMPROPOSAL", OleDbType.VarChar)).Value = proposal;
				cmd.Parameters.Add(new OleDbParameter("PTOPROPOSAL", OleDbType.VarChar)).Value   = proposal;
				cmd.ExecuteNonQuery();*/

				try
				{
					ProcedureAdapter cs = new ProcedureAdapter("TRANSFER_BAPROPOSAL");
					cs.Set("PFROMPROPOSAL", OleDbType.VarChar, proposal);
					cs.Set("PTOPROPOSAL", OleDbType.VarChar, proposal);
					cs.RegisetrOutParameter("ERRORMSG", OleDbType.VarChar, 2000);
					cs.Execute();
					string strError = Convert.ToString(cs.Get("ERRORMSG"));
					if(strError.Length > 0)
					{
						throw new ProcessException(strError);
					}
				}
				catch(Exception ex)
				{
					string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
					throw new ProcessException(ex.Message);
				}
			}
			else if(decision.Equals("Online")  || subsStandard.ToUpper().Equals("TRUE"))
			{
				OleDbCommand cmd=(OleDbCommand) DB.CreateCommand("TRANSFERTOILAS",DB.Connection);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				OleDbParameter param=new OleDbParameter("ERRMSG",OleDbType.VarChar, 1000);
				param.Direction =System.Data.ParameterDirection.Output;
				cmd.Parameters.Add(new OleDbParameter("PFROMPROPOSAL", OleDbType.VarChar)).Value = proposal;
				cmd.Parameters.Add(new OleDbParameter("PTOPROPOSAL", OleDbType.VarChar)).Value   = proposal;
				cmd.Parameters.Add(param);
				cmd.ExecuteNonQuery();
				string errorMessage = cmd.Parameters["ERRMSG"].Value==null?"":cmd.Parameters["ERRMSG"].Value.ToString();
				if(errorMessage!="")
				{
					throw new Exception(errorMessage);
				}
			}

			

			//Generate Policy After Transfer To Ilas
			string policy_Number = "";
			if(ace.ValidationUtility.genPolicyAfterTransferToIlas())
			{
				bool generatePolicyNo = true;
				rowset rsSubStandard = DB.executeQuery("SELECT 'A' FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + proposal + "' AND NP2_SUBSTANDAR IN('Y','R') ");
				if(rsSubStandard.next())
				{
					generatePolicyNo = ace.ValidationUtility.isPolicyNumberNeedForSubStandard();
				}
				
				if(generatePolicyNo)
				{
					try
					{
						policy_Number = generatePolicyNumber(proposal);
						if(policy_Number == null)
						{
							policy_Number = "";
						}
					}
					catch(Exception ex)
					{
						string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
						throw new ProcessException(ex.Message);
					}
				}
			}

			//Update Policy Master
			if(!ace.ValidationUtility.isPostingFound())
			{
				string reference = Convert.ToString(env.getAttribute("s_CCH_CODE"))+Convert.ToString(env.getAttribute("s_CCD_CODE"))+Convert.ToString(env.getAttribute("s_CCS_CODE"));
				string query = "UPDATE lnp1_policymastr SET NP1_SELECTED='Y', PBR_REFERENCE=? WHERE NP1_PROPOSAL=? AND PBR_REFERENCE IS NULL ";
				SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@PBR_REFERENCE",reference);
				if(policy_Number.Trim().Length > 0)
				{
					query = "UPDATE LNP1_POLICYMASTR SET NP1_SELECTED='Y', PBR_REFERENCE=?, NP1_POLICYNO=?, NP1_ISSUEDATE=? WHERE NP1_PROPOSAL=? AND PBR_REFERENCE IS NULL ";
					pc.puts("@NP1_POLICYNO", policy_Number );
					pc.puts("@NP1_ISSUEDATE", Convert.ToDateTime(Convert.ToString(env.getAttribute("s_CURR_SYSDATE"))));
				}
				pc.puts("@NP1_PROPOSAL", proposal);
				DB.executeDML(query,pc);
			}
		}

		
		public static string generatePolicyNumber(string proposal)
		{
			try
			{
				ProcedureAdapter cs = new ProcedureAdapter("GENENRATE_POLICYNO_CALL");
				cs.Set("p_proposal", OleDbType.VarChar, proposal);
				cs.RegisetrOutParameter("policyNumber", OleDbType.VarChar, 1000 );
				cs.Execute();
				string policy_Number = Convert.ToString(cs.Get("policyNumber"));
				return policy_Number;
			}
			catch(Exception ex)
			{
				string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
				throw new ProcessException(ex.Message);
			}
		}

		public static double calcTotalSumAtRisk(string proposal)
		{
			try
			{
				ProcedureAdapter proc = new ProcedureAdapter("SumAtRisk_Banca");
				proc.Set("p_client", OleDbType.VarChar, ace.clsIlasUtility.getClientCode(proposal));
				proc.Set("p_life", OleDbType.VarChar, ace.clsIlasUtility.getClientLife(proposal));
				proc.Set("p_proposal", OleDbType.VarChar, proposal);
				proc.RegisetrOutParameter("m_sar", OleDbType.Decimal, 9);
				proc.Execute();
				double totalSAR = Convert.ToDouble(proc.Get("m_sar"));
				return totalSAR;
			}
			catch(Exception ex)
			{
				string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
				throw new Exception(ex.Message);
			}
		}
		#endregion
		
	}
}