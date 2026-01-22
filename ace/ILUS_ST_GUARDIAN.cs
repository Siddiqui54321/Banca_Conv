using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;


namespace ace
{
	


	public class ILUS_ST_GUARDIAN:shgn.SHGNCommand
	{
		EnvHelper env = new EnvHelper();

		#region Framework method

		public override void fsoperationBeforeSave()
		{
			set("NGU_GUARDCD",getGuardianCode());
		}

		public override void fsoperationAfterSave()
		{
			validateAge();
			updateBeneficiary();
		}

		public override void fsoperationAfterUpdate()
		{
			validateAge();
			updateBeneficiary();
		}

		public override void fsoperationAfterDelete()
		{
			RemoveGuardianFromBeneficiary();
		}
		#endregion

		
		#region Business Methods

		private void RemoveGuardianFromBeneficiary()
		{
			string proposal    = Convert.ToString(env.getAttribute("NP1_PROPOSAL")); 
			string beneficiary = Convert.ToString(env.getAttribute("BENEFICIARY_CODE"));
			DB.executeDML("UPDATE LNBF_BENEFICIARY SET NGU_GUARDCD=null WHERE NP1_PROPOSAL='" + proposal +"' AND NBF_BENEFCD=" + beneficiary );

			/*try
			{
				//Remove Proposal Number 
				string guardian = Convert.ToString(get("NGU_GUARDCD"));
				string query = "select 'A' from LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + proposal +"' AND NBF_BENEFCD" + beneficiary + " AND NGU_GUARDCD=" + guardian ;
				rowset rs = DB.executeQuery(query);
				if(rs.next() == false)
				{
					RemoveProposalFromGuardian();
				}
			}*/
		}



		public static void RemoveUnusedGuardianRef(string proposal)
		{
			try
			{
				string query = "select NGU_GUARDCD from LNGU_GUARDIAN WHERE NP1_PROPOSAL='" + proposal +"'" ;
				rowset rsGuardian = DB.executeQuery(query);
				
				while(rsGuardian.next())
				{
					string guardian = rsGuardian.getString("NGU_GUARDCD");
					query = "select 'A' from LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + proposal +"' AND NGU_GUARDCD=" + guardian ;
					rowset rsBenficiary = DB.executeQuery(query);
					if(rsBenficiary.next() == false)
					{
						//Remove Proposal From Guardian;
						DB.executeDML("UPDATE LNGU_GUARDIAN SET NP1_PROPOSAL=null WHERE NGU_GUARDCD=" + guardian);
					}
				}
			}
			catch(Exception e)
			{
				throw new Exception("Error in Removing proposal from Guardian ");
			}
		}

		private void RemoveProposalFromGuardian()
		{
			int guardCode = Convert.ToInt32(get("NGU_GUARDCD"));
			string user = Convert.ToString(env.getAttribute("s_USE_USERID")); 
			DateTime sysDate = Convert.ToDateTime(env.getAttribute("s_CURR_SYSDATE"));

			string query = "UPDATE LNGU_GUARDIAN SET NP1_PROPOSAL=null, USE_USERID=?, USE_DATETIME=? WHERE NGU_GUARDCD=?";
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@USE_USERID", user);
			pc.puts("@USE_DATETIME", sysDate);
			pc.puts("@NGU_GUARDCD", guardCode);
			DB.executeDML(query,pc);
		}


		private int getGuardianCode()
		{
			try
			{
				ProcedureAdapter call = new  ProcedureAdapter("GENERATE_GUARDIANNO_CALL");
				call.RegisetrOutParameter("GUARDIANCODE", System.Data.OleDb.OleDbType.Numeric);
				call.Execute();
				return Convert.ToInt32(call.Get("GUARDIANCODE"));
			}
			catch(Exception e)
			{
				throw new ProcessException("Error in generating Guardian Code. Error: "+ e.Message);
			}
		}

		private void updateBeneficiary()
		{
			string proposal    = Convert.ToString(env.getAttribute("NP1_PROPOSAL")); 
			string beneficiary = Convert.ToString(env.getAttribute("BENEFICIARY_CODE"));
			string guardian    = Convert.ToString(get("NGU_GUARDCD"));
			DB.executeDML("UPDATE LNBF_BENEFICIARY SET NGU_GUARDCD=" + guardian + " WHERE NP1_PROPOSAL='" + proposal +"' AND NBF_BENEFCD=" + beneficiary );
		}

		private void validateAge()
		{
			int Age = Convert.ToInt16(get("NGU_AGE")); 
			if(Age < ace.clsIlasConstant.AGE_LIMIT)
			{
				throw new ProcessException("Guardian Age must be greater than " + Convert.ToString(ace.clsIlasConstant.AGE_LIMIT) + " years.");
			}
		}


		//Called from Ajax based method
		public static string[] getGuardianInfo(string proposal, string guardian)
		{
			string [] result = new string[]{"","","",""};
			string query = "select NGU_GUARDCD, NGU_NAME, gu.CRL_RELEATIOCD, CRL_DESCR from LNGU_GUARDIAN gu, LCRL_RELATION rl "
				         + "where gu.CRL_RELEATIOCD=rl.CRL_RELEATIOCD and NP1_PROPOSAL='" + proposal + "' and ngu_guardcd=" + guardian;
 
			try
			{
				rowset rs = DB.executeQuery(query);
				if(rs.next())
				{
					result[0] = Convert.ToString(rs.getObject("NGU_GUARDCD")); 
					result[1] = rs.getString("NGU_NAME"); 
					result[2] = rs.getString("CRL_RELEATIOCD");
					result[3] = rs.getString("CRL_DESCR");
				}			
			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
			return result;
		}


		#endregion
	}

}