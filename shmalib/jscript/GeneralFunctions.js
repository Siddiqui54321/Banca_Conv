function setCurrencyTabular(objid)
{
	for (i=0;i<totalRecords-1;i++) 
	{
		var id="000000"+i;
		id=id.substring(id.length-6);
		var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;
		//condition handling section and event fire enable disable
		applyNumberFormat(document.getElementById(item), 2);
	}
}

//Return Validation for Product...
function getvalidationforproduct(vfor)
{
	var methodName = "check_validation";
	var parameters =vfor+','+document.getElementById('ddlPPR_PRODCD').value;
	//alert(parameters);
	return validateproduct(methodName,parameters); 
}

//Finally call the Remote Class 
function validateproduct(methodName,parameters)
{
	var className  = "SHAB.Data.LPVL_VALIDATIONS";
	return executeClass(className + "," + methodName + "," + parameters); 
}

function setPercentTabular(objid)
{
	for (i=0;i<totalRecords-1;i++) 
	{
		var id="000000"+i;
		id=id.substring(id.length-6);
		var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;
		//condition handling section and event fire enable disable
		applyNumberFormatPercent(document.getElementById(item), 2);
	}
}


function onexecute_EachTabularEntry(objname, funcname)
{

	for (i=0;i<totalRecords-1;i++) 
	{
		var id="000000"+i;
		id=id.substring(id.length-6);
		var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objname;

		funcname = funcname.replace('?obj', 'document.getElementById(item)');
		
		eval(funcname);
	}
}

function onevent_EachTabularEntry(objname, eventname)
{

	for (i=0;i<totalRecords-1;i++) 
	{
		var id="000000"+i;
		id=id.substring(id.length-6);
		var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objname;

		eventname = 'document.getElementById(item).'+eventname;
		
		eval(eventname);
	}
}



function setCurrencyLister(objid)
{
	for (i=0;i<20;i++) 
	{
		var id="000000"+i;
		id=id.substring(id.length-6);
		var item = "lister__ctl"+eval(eval(id))+"_"+objid;
		//condition handling section and event fire enable disable
		//alert(document.getElementById(item));
		
		if (document.getElementById(item)==null)
			break;

		applyNumberFormat(document.getElementById(item),2);
	}
}



function setCurrencySingle(objid)
{
	//document.getElementById(objid).onblur();
}

function setFormatSingle(objid,precision)
{
	applyNumberFormat(document.getElementById(objid),precision);
}

