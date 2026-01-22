using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OracleClient;

namespace ace
{
	
	public class ILUS_ET_NM_PER_PERSONALDETINS:shgn.SHGNCommand
	{
		
		EnvHelper env = new EnvHelper();

		//public override void fsoperationAfterSave()
		public override void fsoperationBeforeSave()
		{
		}
		
		public override void fsoperationAfterSave()
		{
			fscreateModelLinkPersonalDet();
			//************* Activity Log *************//
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.SECOND_LIFE_UPDATED);
		}

		public override void fsoperationAfterUpdate()
		{
			fscreateModelLinkPersonalDet();
			//************* Activity Log *************//
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.SECOND_LIFE_UPDATED);
		}


		//Create linking entry in lnu1_underwriti
		private void fscreateModelLinkPersonalDet()
		{
			String item1 = ""+env.getAttribute("NP1_PROPOSAL");
			String item2 = ""+env.getAttribute("_pk_NPH_CODE");
			String smoker = ""+env.getAttribute("NU1_SMOKER");
			String bankCode = ""+env.getAttribute("NP1_CHANNEL");
			String branchCode = ""+env.getAttribute("NP1_CHANNELDETAIL");
			String accountNo = ""+env.getAttribute("NU1_ACCOUNTNO");
 
			String NPH_WEIGHTUOM=""+env.getAttribute("NPH_WEIGHTUOM");
			String NPH_WEIGHTACTUAL=""+env.getAttribute("NPH_WEIGHTACTUAL");
			String NPH_WEIGHT =""+env.getAttribute("NPH_WEIGHT");
				
			String NPH_HEIGHTUOM=""+env.getAttribute("NPH_HEIGHTUOM");
			String NPH_HEIGHTACTUAL=""+env.getAttribute("NPH_HEIGHTACTUAL");
			String NPH_HEIGHT=""+env.getAttribute("NPH_HEIGHT");
			
			String NU1_OVERUNDERWT=""+env.getAttribute("NU1_OVERUNDERWT");
			
			bool isPolicyInsured1 = ((""+this.get("NPH_INSUREDTYPE")).Equals("N")?false:true);
			

			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL", env.getAttribute("NP1_PROPOSAL"));
			pm.puts("@NPH_CODE", env.getAttribute("NPH_CODE_s"));
			pm.puts("@NPH_LIFE","D");
			pm.puts("@NU1_LIFE","S");
			

			if(NPH_HEIGHT.Trim().Length > 0)
                pm.puts("@NU1_HEIGHT",Convert.ToDouble(NPH_HEIGHT));
			else
				pm.puts("@NU1_HEIGHT",0);

			if(NPH_WEIGHT.Trim().Length > 0)
				pm.puts("@NU1_WEIGHT",Convert.ToDouble(NPH_WEIGHT));
			else
				pm.puts("@NU1_WEIGHT",0);
			
			//PAYER
				
			if(isPolicyInsured1)
			{
				pm.puts("NU1_PAYER","I");

			}
			
			if(NU1_OVERUNDERWT.Trim().Length > 0)
				pm.puts("@NU1_OVERUNDERWT",Convert.ToDouble(NU1_OVERUNDERWT));
			else
				pm.puts("@NU1_OVERUNDERWT",0);

			pm.puts("@NU1_SMOKER",smoker);
			
			if(NPH_HEIGHTUOM.Trim().Length > 0)
				pm.puts("@NU1_HEIGHTUOM",NPH_HEIGHTUOM);
			else
				pm.puts("@NU1_HEIGHTUOM",0);

			if(NPH_HEIGHTACTUAL.Trim().Length > 0)
				pm.puts("@NU1_HEIGHTACTUAL",Convert.ToDouble(NPH_HEIGHTACTUAL));
			else
				pm.puts("@NU1_HEIGHTACTUAL",0);
			
			
			
			if(NPH_WEIGHTUOM.Trim().Length > 0)
                pm.puts("@NU1_WEIGHTUOM", NPH_WEIGHTUOM);
			else
				pm.puts("@NU1_WEIGHTUOM", 0);


			if(NPH_WEIGHTACTUAL.Trim().Length > 0)
				pm.puts("@NU1_WEIGHTACTUAL",Convert.ToDouble(NPH_WEIGHTACTUAL));
			else
				pm.puts("@NU1_WEIGHTACTUAL",0);
			
			pm.puts("@NU1_ACCOUNTNO",accountNo);
			pm.puts("@PBK_BANKCODE",bankCode);
			pm.puts("@PBB_BRANCHCODE",branchCode);
					
			////Hegiht,Weight and BMI

			
			//prepare lnu1_underwriti for new entry, so delete old entry if exists in case of changing persons on proposal
			DB.executeDML("delete from lnu1_underwriti where np1_proposal='"+env.getAttribute("NP1_PROPOSAL")+"' and NU1_LIFE='S'");
			
			try
			{
//New Code
				bool isPolicyInsured = ((""+this.get("NPH_INSUREDTYPE")).Equals("N")?false:true);
				if(isPolicyInsured)
				{
					//pm.puts("NU1_PAYER","I");
					//DB.executeDML("insert into lnu1_underwriti (NP1_PROPOSAL, NPH_CODE, NPH_LIFE, NU1_LIFE,NU1_SMOKER, PBK_BANKCODE, PBB_BRANCHCODE, NU1_ACCOUNTNO,NU1_PAYER) values (?,?,?,?,?,?,?,?,?)", pm);
					DB.executeDML("insert into lnu1_underwriti (np1_proposal,nph_code,nph_life,nu1_life,nu1_height,nu1_weight,nu1_payer,nu1_overunderwt,nu1_smoker,nu1_heightuom,nu1_heightactual,nu1_weightuom,nu1_weightactual,nu1_accountno,pbk_bankcode,pbb_branchcode) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", pm);
			
				}				

				//DB.executeDML("insert into lnu1_underwriti (NP1_PROPOSAL, NPH_CODE, NPH_LIFE, NU1_LIFE, NU1_SMOKER, PBK_BANKCODE, PBB_BRANCHCODE, NU1_ACCOUNTNO) values (?,?,?,?,?,?,?,?)", pm);
			}
			catch(Exception e)
			{
				throw new ProcessException("Person cannot be same on second life...");
			}

		}

		private string getNPH_CODE(String strNP1_PROPOSAL)
		{
			rowset rsMax = DB.executeQuery("select max(nph_code) from lnu1_underwriti where np1_proposal='"+strNP1_PROPOSAL+"'");
			if (rsMax.next())
			return rsMax.getString(1);

			return "1";
		
		
		}
	}
	//END OF CLASS
}