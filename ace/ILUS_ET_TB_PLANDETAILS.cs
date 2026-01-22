using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using FieldValidationException = SHMA.Enterprise.Exceptions.FieldValidationException;

namespace ace
{
	
	public class ILUS_ET_TB_PLANDETAILS:shgn.SHGNCommand
	{
		
		EnvHelper env = new EnvHelper();

		public override void fsoperationBeforeSave()
		{
		}
		public override void fsoperationAfterSave()
		{
			validateInput();
			//updateBenefitTerm();

			//************* Activity Log *************//			
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.RIDERS_UPDATED, Convert.ToString(env.getAttribute("NP1_PROPOSAL")) + "~" + ace.Ace_General.getPPR_PRODCD(Convert.ToString(env.getAttribute("NP1_PROPOSAL"))));

		}

		public override void fsoperationBeforeUpdate()
		{

			/*rowset rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','SUMASSURED',"+get("NPR_SUMASSURED")+","+SessionObject.Get("NP2_AGEPREM")+","+get("NPR_BENEFITTERM")+") from dual");
			if(rsValidate.next())
				if (rsValidate.getObject(1)!=null)
					throw new ProcessException("Face Amount: "+rsValidate.getString(1));
			rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','BTERM',"+get("NPR_BENEFITTERM")+","+SessionObject.Get("NP2_AGEPREM")+","+get("NPR_BENEFITTERM")+") from dual");
			if(rsValidate.next())
				if (rsValidate.getObject(1)!=null)
					throw new ProcessException("Benefit Term: "+rsValidate.getString(1));
			rsValidate = DB.executeQuery("select CHECK_VALIDATION('"+get("PPR_PRODCD")+"','MATURITYAGE',"+SessionObject.Get("NP2_AGEPREM")+","+SessionObject.Get("NP2_AGEPREM")+","+get("NPR_BENEFITTERM")+") from dual");
			if(rsValidate.next())
				if (rsValidate.getObject(1)!=null)
					throw new ProcessException("Premium Paid upto Age: "+rsValidate.getString(1));
					*/
		}

		public override void fsoperationAfterUpdate()
		{
			validateInput();
			//updateBenefitTerm();

			//************* Activity Log *************//			
			Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.RIDERS_UPDATED, Convert.ToString(env.getAttribute("NP1_PROPOSAL")) + "~" + ace.Ace_General.getPPR_PRODCD(Convert.ToString(env.getAttribute("NP1_PROPOSAL"))));

		}

		public override void fsoperationBeforeDelete()
		{
		}
		public override void fsoperationAfterDelete()
		{
		}

		private void updateBenefitTerm()
		{
			//"SELECT NPR_BENEFITTERM,PPR_PRODCD,NP1_PROPOSAL,NP2_SETNO FROM LNPR_PRODUCT  WHERE  NP1_PROPOSAL ="+  env.getAttribute("NP1_PROPOSAL")+" AND NVL(NPR_BASICFLAG,'N') = 'Y'"
			rowset rsPlan = DB.executeQuery("SELECT NPR_BENEFITTERM FROM LNPR_PRODUCT  WHERE  NP1_PROPOSAL ='"+  env.getAttribute("NP1_PROPOSAL")+"' AND NVL(NPR_BASICFLAG,'N') = 'Y'");

			if (rsPlan.next())
				DB.executeDML("UPDATE LNPR_PRODUCT  SET NPR_BENEFITTERM=" + rsPlan.getObject(1) + ",NPR_PREMIUMTER=" + rsPlan.getObject(1) + "  WHERE NP1_PROPOSAL='"+env.getAttribute("NP1_PROPOSAL")+"'");

		}

		private void validateInput()
		{
			//ValidationCall objValidation = new ValidationCall();
			//objValidation.validateBenefit(Convert.ToString(env.getAttribute("NP1_PROPOSAL")));
			try
			{
				ValidationCall objValidation = new ValidationCall();
				objValidation.validateBenefit(Convert.ToString(env.getAttribute("NP1_PROPOSAL")));
			}
			catch(Exception e)
			{
				throw new FieldValidationException(e.Message);
			}
		}
	}
}