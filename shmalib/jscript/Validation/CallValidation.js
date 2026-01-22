/************************* Validation Related Functions ***************************/
function validatePlan()
{
	return true;
	/*if (parent.parent.newValidation == 'Y')
	{
		//Set Validation field collection
		var parameters = "NP1_PROPOSAL~"    + getField("NP1_PROPOSAL").value;
		   parameters += "&PPR_PRODCDB~"    + getField("PPR_PRODCD").value;
		   parameters += "&PPR_PRODCD~"     + getField("PPR_PRODCD").value;
		   parameters += "&NPR_BENEFITTERM~"+ getFieldValue("NPR_BENEFITTERM");
		   parameters += "&NPR_PREMIUMTER~" + getFieldValue("NPR_PREMIUMTER");
		   parameters += "&NPR_SUMASSURED~" + getFieldValue("NPR_SUMASSURED");
		   parameters += "&NPR_TOTPREM~"    + getFieldValue("NPR_TOTPREM");
		   parameters += "&NPR_PREMIUM~"    + getFieldValue("NPR_PREMIUM");
		   parameters += "&CCB_CODE~"       + getField("CCB_CODE").value;
		   parameters += "&CMO_MODE~"       + getField("CMO_MODE").value;
		   parameters += "&PCU_CURRCODE~"   + getField("PCU_CURRCODE").value;
		   parameters += "&NPR_INDEXATION~" + getField("NPR_INDEXATION").value;
		   parameters += "&NPR_INDEXRATE~"  + getFieldValue("NPR_INDEXRATE");
		   
		//Validate Now
		var error = CallValidation("validatePlan", parameters);
		if (error.length > 0)
		{
			alert(error);
			return false;
		}
		else
		{
			return true;
		}
	}
	else
	{
		return true;
	}
	*/
}

function validateBenefits()
{
	return true;

	if(totalRecords>1) 
	{
		if (newValidation == 'Y')
		{
			var parameters = strProposal + ",";
			for (var navi=1;navi<totalRecords;navi++)
			{
				var rider = getTabularFieldValue(navi,"PPR_PRODCD");
				parameters += "PPR_PRODCD~"   + rider;
				parameters += "&SA"+rider+"~" + getTabularFieldValue(navi,"NPR_SUMASSURED");
				parameters += "&BT"+rider+"~" + getTabularFieldValue(navi,"NPR_BENEFITTERM");
				parameters += "&TP"+rider+"~" + getTabularFieldValue(navi,"NPR_PREMIUM");
				parameters += ">";
			}
			//Remove last character '>'
			parameters = parameters.substring(0, parameters.length-1);

			//Validate Now
			var error = CallValidation("validateBenefits", parameters);
			if (error.length > 0)
			{
				//alert(error);
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			return true;
		}
	}
	else
	{
		return true;
	}
	
}

function CallValidation(methodName, parameters)
{
	var className  = "ace.CallValidationFromClient";
	return executeClass(className + "," + methodName + "," + parameters); 
}