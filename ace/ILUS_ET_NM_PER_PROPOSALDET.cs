using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OleDb;


namespace ace
{
	
	public class ILUS_ET_NM_PER_PROPOSALDET:shgn.SHGNCommand
	{
		EnvHelper env = new EnvHelper();

		#region framework methods

		public override void fsoperationBeforeSave()
		{
		}
        public override string fsoperationBeforeSave(string nicuser)
        {
            string errorMessage = string.Empty;
            string nic = nicuser;
            try
            {

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
                    errorMessage = cmd.Parameters["user_Validation"].Value.ToString();
                }

            }
            catch (Exception e)
            {
                errorMessage = "Error " + e.Message;
            }
            return errorMessage;
        }
		public override void fsoperationAfterSave()
		{
			updatePolicyMaster();
			DB.executeDML("insert into lnpu_purpose select '"+env.getAttribute("key_for_aftersave_NP1_PROPOSAL")+"', cpu_code,'N' from lcpu_purpose");

			//************* Activity Log *************//
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_GENERATED);

		}

		public override void fsoperationAfterUpdate()
		{
			updatePolicyMaster();
			fsUpdateProduct();			
		}

		private void updatePolicyMaster()
		{
			string aagCode       = System.Convert.ToString(env.getAttribute("s_AAG_AGCODE"));
			string compCode      = System.Convert.ToString(env.getAttribute("s_PCM_COMPCODE"));
			string channel       = System.Convert.ToString(env.getAttribute("s_CCH_CODE"));
			string channelDtl    = System.Convert.ToString(env.getAttribute("s_CCD_CODE"));
			string channelSubDtl = System.Convert.ToString(env.getAttribute("s_CCS_CODE"));
			string proposal      = System.Convert.ToString(env.getAttribute("key_for_aftersave_NP1_PROPOSAL"));

			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.clear();
			pm.puts("@AAG_AGCODE",         aagCode,       Types.VARCHAR);
			pm.puts("@PCM_COMPCODE",       compCode,      Types.VARCHAR);
			pm.puts("@PCL_LOCATCODE",      channel+channelDtl+channelSubDtl, Types.VARCHAR);
			pm.puts("@NP1_CHANNELSDETAIL", channelSubDtl, Types.VARCHAR);
			pm.puts("@NP1_PROPOSAL",       proposal,      Types.VARCHAR);

			string query = "UPDATE LNP1_POLICYMASTR SET AAG_AGCODE=?, PCM_COMPCODE=?, PCL_LOCATCODE=?, NP1_CHANNELSDETAIL=? WHERE NP1_PROPOSAL=?";
			DB.executeDML(query,pm);		
		}

		private void fsUpdateProduct()
		{
			try
			{
				string proposal = System.Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
				string query = "UPDATE LNPR_PRODUCT SET NPR_PREMIUM=0, NPR_PREMIUM_FC=0, NPR_PREMIUM_AV=0, NPR_AGEPREM=0, NPR_BENEFITTERM=0, NPR_PREMIUMTER=0 WHERE NP1_PROPOSAL=?";
				SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
				pm.puts("@NP1_PROPOSAL", proposal, Types.VARCHAR);
				DB.executeDML(query,pm);
			}
			catch(Exception e)
			{
							
			}

		}


		public override void fsoperationAfterDelete()
		{

		}
 
		
		public override void fsoperationBeforeDelete()
		{
			//Delete child entries in lnu1_underwriti and lnph_pholder
			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
		
			rowset rsLNU1_UNDERWRITI = DB.executeQuery("select NPH_CODE, NPH_LIFE from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' ");

			try
			{
				DB.executeDML("delete from lnpu_purpose where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"'");
				DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"'");

				while (rsLNU1_UNDERWRITI.next())
				{
					pm.clear();
					pm.puts("@NPH_CODE", rsLNU1_UNDERWRITI.getString(1));
					pm.puts("@NPH_LIFE", rsLNU1_UNDERWRITI.getString(2));
			
					//DB.executeDML("delete from lnu1_underwriti where NPH_CODE=? and NPH_LIFE=?", pm);
					DB.executeDML("delete from lnph_pholder where NPH_CODE=? and NPH_LIFE=?", pm);
				
				}
			
				//Remove Questionaire
				string proposal = getString("NP1_PROPOSAL");
				string product = env.getAttribute("PPR_PRODCD").ToString();
				removeQuestionaire(proposal,product);

				SessionObject.Remove("NPH_TITLE");
				SessionObject.Remove("NPH_SEX");
				SessionObject.Remove("NPH_FULLNAME");
				SessionObject.Remove("NPH_FULLNAMEARABIC");
				SessionObject.Remove("NPH_BIRTHDATE");
				SessionObject.Remove("COP_OCCUPATICD");
				SessionObject.Remove("CCL_CATEGORYCD");
				SessionObject.Remove("NPH_INSUREDTYPE");
				SessionObject.Remove("NPH_CODE");
				SessionObject.Remove("NPH_LIFE");
				SessionObject.Remove("NPH_CODE_s");
				SessionObject.Remove("NPH_LIFE_s");

			}
			catch(Exception e)
			{
				string ex = e.StackTrace;

			}
		}
		#endregion

		#region Business Methods
		private void removeQuestionaire(string proposal, string product)
		{
			string query = "DELETE FROM LNQN_QUESTIONNAIRE WHERE NP1_PROPOSAL=? AND PPR_PRODCD=? ";
			SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
			pc.clear();
			pc.puts("@NP1_PROPOSAL", proposal, Types.VARCHAR);
			pc.puts("@PPR_PRODCD", product,  Types.VARCHAR);
			DB.executeDML(query, pc);		
		}
		#endregion
	}
}