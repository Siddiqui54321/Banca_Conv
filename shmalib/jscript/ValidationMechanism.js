//Author: Khalid Masood wasti
//A handy generic mechenism to apply business logic conveniently on tabular views
//lazy loads, function pointers, and hook on the fly

/*
function validateTabular(objName) 
{
		for (i=0;i<totalRecords-1;i++) 
		{
			
			var id="000000"+i;
			id=id.substring(id.length-6);
			var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objName;
			//condition handling section and event fire enable disable
			setValidatorState(document.getElementById(item));
		}
}



function setValidatorState1(obj)
{
	var arrOfValidators = document.myForm.validationColumns.value.split(',');
	for (x=0; x<arrOfValidators.length; x++) 
	{
		if (obj.value == "N")
			document.getElementById('EntryGrid__'+(obj.id.split('_')[2])+"_rfv"+arrOfValidators[x]).enabled = false;
		else
			document.getElementById('EntryGrid__'+(obj.id.split('_')[2])+"_rfv"+arrOfValidators[x]).enabled = true;
	}
	this.Page_ClientValidate();
}


function setValidatorState(obj)
{
	var arrOfValidators = document.myForm.conditionCols.value.split(',');
	var item = 'EntryGrid__'+(obj.id.split('_')[2])+'_';
	
	for (x=0; x<arrOfValidators.length; x++) 
	{

		//alert(getEvaluate(item));
		if (getEvaluate(item)==true)
			document.getElementById(item+"rfv"+arrOfValidators[x]).enabled = true;
		else
			document.getElementById(item+"rfv"+arrOfValidators[x]).enabled = false;

	}
	this.Page_ClientValidate();
}


function getEvaluate(index)
{
	var arrOfConditionScript = document.myForm.conditionVars.value.split(',');
	var conditionScript = document.myForm.conditionVals.value;
	
	for (j=0; j<arrOfConditionScript.length; j++)
	{
		var replacement = "document.getElementById('"+index+arrOfConditionScript[j]+"').value";
		conditionScript = conditionScript.replace(arrOfConditionScript[j],replacement); 
		//alert(eval(replacement));
	}

	
	return eval(conditionScript);
}




function validateTabular(objName, condition) 
{
		for (i=0;i<totalRecords-1;i++) 
		{
			
			var id="000000"+i;
			id=id.substring(id.length-6);
			var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objName;
			//condition handling section and event fire enable disable
			setValidatorState(document.getElementById(item), objName, condition);
		}
}


//setValidatorState_OnCondition(condion value column, condion apply columns with relevant condition)
//e.g., ddlNPR_SELECTED(condion value column obj, condion value column generic name, condion apply columns with relevant condition)
//e.g., setValidatorState_OnCondition(ddlNPR_SELECTED.ref, ddlNPR_SELECTED.genericName, NPR_SUMASSURED:=ddlNPR_SELECTED=='Y', NPR_BENEFITTERM:=ddlNPR_SELECTED=='N');
//e.g., setValidatorState(this, "ddlNBF_BASIS", "NBF_AMOUNT:=ddlNBF_BASIS=='01',NBF_PERCNTAGE:=ddlNBF_BASIS=='02'");
//DEPRECATED
function setValidatorState(obj, variable, condition)
{
	var arrOfValidators = condition.split(',');
	var item = 'EntryGrid__'+(obj.id.split('_')[2])+'_';
	
	for (x=0; x<arrOfValidators.length; x++) 
	{

		var element = arrOfValidators[x].split(':=')[0];
		var cond = arrOfValidators[x].split(':=')[1];
		
		if (getEvaluate(variable, item, cond)==true)
			document.getElementById(item+"rfv"+element).enabled = true;
		else
			document.getElementById(item+"rfv"+element).enabled = false;

	}
	this.Page_ClientValidate();
}


function getEvaluate(variable, index, condition)
{
	
	var replacement = "document.getElementById('"+index+variable+"').value";
	condition = condition.replace(variable,replacement); 

	
	return eval(condition);
}

// END DEPRECATED

*/

function applyValidationTabular(function_ref, valid_identities)
{
		var identities = valid_identities.split(',');
	

		for (i=0;i<totalRecords-1;i++) 
		{
			
			var id="000000"+i;
			id=id.substring(id.length-6);
			var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_";
			
			
			var func = function_ref;
			

			for (x=0; x<identities.length; x++)
			{
				func = replace(func.toString(), identities[x],item+identities[x]);
			}
			eval(func + 'validate();');
		}
}

function applyValidationSingleTabular(obj, function_ref, valid_identities)
{

		var identities = valid_identities.split(',');


		var index = obj.id.split('_')[2].replace('ctl','');
		var item = "EntryGrid__ctl"+eval(eval(index))+"_";

		
		var func = function_ref;
		for (x=0; x<identities.length; x++)
		{
			if (eval(index<=totalRecords))
				func = replace(func.toString(), identities[x],item+identities[x]);
			else
			{
				//alert(func);
				var identity = replaceNewForNewEntry(identities[x]);
				//alert(identity);
				func = replace(func.toString(), identities[x],item+identity);
				//alert(func);
			}
		}
		//alert(func);

		eval(func + 'validate();');
}


//replace mechanism wrote by Khalid
//BUGGY Replace
//TODO: if x is not found the replace malfunction, and returns undefined or null, need correction 
function replace(argvalue, x, y) {

  /*if ((x == y) || (parseInt(y.indexOf(x)) > -1)) {
    errmessage = "replace function error: \n";
    errmessage += "Second argument and third argument could be the same ";
    errmessage += "or third argument contains second argument.\n";
    errmessage += "This will create an infinite loop as it's replaced globally.";
    alert(errmessage);
    return false;
  }*/

	//alert(argvalue +', ' + x + ', ' + y);

  var part = '';

  while (argvalue.indexOf(x) != -1) {
    var leading = argvalue.substring(0, argvalue.indexOf(x));
    var trailing = argvalue.substring(argvalue.indexOf(x) + x.length,
	argvalue.length);
    part += leading + y;
    argvalue = trailing;
  }
  part += trailing;

  return part;
}


function replaceNewForNewEntry(identity)
{
	identity = identity.toString().replace('rfv','prfvNew').replace('txt','txtNew').replace('ddl','ddlNew');
	return identity;
}