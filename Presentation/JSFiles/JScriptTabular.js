/*
// function to disable context menu
document.oncontextmenu = function () {
    //return false;
    return true;
}

// funtion to stop Ctrl N "New window opening"
document.onkeydown = function () {

    if (((event.keyCode == 78) || (event.keyCode == 110)) && (event.ctrlKey)) {
        event.cancelBubble = true;
        event.returnValue = false;
        event.keyCode = false;
        return false;
    }
}*/


//// funtion to stop Ctrl N "New window opening"
//document.onkeydown = function(){
//	
//	if (((event.keyCode == 78)|| (event.keyCode == 67) || (event.keyCode == 86)) && (event.ctrlKey))
//	{
//		event.cancelBubble = true;
//		event.returnValue = false;
//		event.keyCode = false; 
//		return false;
//	}
//}

inSend = false;
function executeProcess(proccessName){
	if (proccessName == "shsm.SHSM_VerifyTransaction" || proccessName == "shsm.SHSM_RejectTransaction"){
		if (IsProcessAllowed(proccessName)){		
			callEvent('Process','ProcessName', proccessName);				
		}			
	}		
	else{
		callEvent('Process','ProcessName', proccessName);
	}
}
	

// For checking dupliate rows in tabular, compatible with .NET***

function getReleventFieldName(refobj,fieldName)
{
if (refobj.name.indexOf("New")!=-1) {
  fieldName = fieldName.substring(0,3) + 'New' + fieldName.substring(3);
 }
return fieldName ; 
}

function checkDuplicateLevel1(srcFieldName1, requiredFieldName, message) {
    
	duplicateFound=false;
	for (j = 1; j < totalRecords; j++) {
	    
    srcFieldValue1 = getTabularFieldByIndex(j, srcFieldName1).value;
    //alert(srcFieldValue1);
		for(wi=j+1;wi<=totalRecords;wi++)
		{
			requiredFieldValue=getTabularFieldByIndex(wi,requiredFieldName).value;
			if(requiredFieldValue.length=0)continue;
			srcFieldValue11 = getTabularFieldByIndex(wi,srcFieldName1).value;
			if (srcFieldValue1=== srcFieldValue11)
			{
					alert(message);
					getTabularFieldByIndex(wi,srcFieldName1).focus();
					duplicateFound=true;
					break;
			}
		}
	}
	return duplicateFound;
}
function checkDuplicateLevel2(srcFieldName1,srcFieldName2,requiredFieldName,message)
{
	duplicateFound=false;
	
	for(j=1;j<=totalRecords;j++)
	{
 		srcFieldValue1 = getTabularFieldByIndex(j,srcFieldName1).value;
		srcFieldValue2 = getTabularFieldByIndex(j,srcFieldName2).value;
		
		for(wi=j+1;wi<=totalRecords;wi++)
		{
			requiredFieldValue=getTabularFieldByIndex(wi,requiredFieldName).value;	
			if(requiredFieldValue.length==0)continue;

 			srcFieldValue11 = getTabularFieldByIndex(wi,srcFieldName1).value;
			srcFieldValue22= getTabularFieldByIndex(wi,srcFieldName2).value;
			
			if (srcFieldValue1=== srcFieldValue11 && srcFieldValue2===srcFieldValue22)
			{
					alert(message);
					getTabularFieldByIndex(wi,srcFieldName2).focus();
					duplicateFound=true;
					break;
			}
		}
	}
	return duplicateFound;
}

function checkDuplicateLevel3(srcFieldName1,srcFieldName2,srcFieldName3,requiredFieldName,message)
{
	duplicateFound=false;
	
	for(j=1;j<totalRecords;j++)
	{
 		srcFieldValue1 = getTabularFieldByIndex(j,srcFieldName1).value;
		srcFieldValue2 = getTabularFieldByIndex(j,srcFieldName2).value;
		srcFieldValue3 = getTabularFieldByIndex(j,srcFieldName3).value;
		
		for(wi=j+1;wi<=totalRecords;wi++)
		{
			requiredFieldValue=getTabularFieldByIndex(wi,requiredFieldName).value;	
			if(requiredFieldValue.length==0)continue;

 			srcFieldValue11 = getTabularFieldByIndex(wi,srcFieldName1).value;
			srcFieldValue22= getTabularFieldByIndex(wi,srcFieldName2).value;
			srcFieldValue33= getTabularFieldByIndex(wi,srcFieldName3).value;
			
			if (srcFieldValue1=== srcFieldValue11 && srcFieldValue2===srcFieldValue22 				&& srcFieldValue3===srcFieldValue33)
			{
					getTabularFieldByIndex(wi,srcFieldName3).focus();
					duplicateFound=true;
					break;
			}
		}
	}
	return duplicateFound;
}		

/*function setColSum(sourceField,totalFieldName) 
{
	if  (debug) alert ('In setColSum, totalFieldName= ' + totalFieldName + ', sourceField= ' + sourceField);
	if  (debug) alert ('In setColSum, totalField: ' + document.getElementById(totalFieldName));
	//-- Support for compatible code in Java and Dot Net------------------------ 
	if (totalFieldName == "Total") {totalFieldName = "total" + sourceField ;}
	//-------------------------------------------------------------------------
	//document.getElementById(totalFieldName).innerHTML=getColSum(sourceField);
	alert(1);
	if (totalRecords > 0) 	
	{
		TotalValue = getColSum(sourceField)
		objField = getTabularFieldByIndex(1,sourceField);
		if(objField!=null)
		{
		document.getElementById(totalFieldName).innerHTML=TotalValue;
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
			getTotalFieldByName(totalFieldName).innerHTML = getCurrencyFormatedValue(TotalValue, objField.getAttribute("Precision"));
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
			getTotalFieldByName(totalFieldName).innerHTML = getNumberFormatedValue(TotalValue, objField.getAttribute("Precision"));
		}
	}
}*/

function setColSum(sourceField,totalFieldName) 
{
    
	if  (debug) alert ('In setColSum, totalFieldName= ' + totalFieldName + ', sourceField= ' + sourceField);
	if  (debug) alert ('In setColSum, totalField: ' + document.getElementById(totalFieldName));
	//alert(document.getElementById(totalFieldName));
	//alert(getTabularFieldByIndex(totalRecords+1, totalFieldName));
	
	//-- Support for compatible code in Java and Dot Net------------------------ 
	if (totalFieldName == "Total") {totalFieldName = "total" + sourceField ;}
	//-------------------------------------------------------------------------

	//document.getElementById(totalFieldName).innerHTML=getColSum(sourceField);
	//getTotalFieldByName(totalFieldName).innerHTML=getColSum(sourceField);
	
	if (totalRecords > 1) 	
	{
	    TotalValue = getColSum(sourceField)
		objField = getTabularFieldByIndex(1,sourceField);
		getTotalFieldByName(totalFieldName).innerHTML=TotalValue;
		//alert(getTotalFieldByName(totalFieldName));
		
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
			getTotalFieldByName(totalFieldName).innerHTML = getCurrencyFormatedValue(TotalValue, objField.getAttribute("Precision"));
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
			getTotalFieldByName(totalFieldName).innerHTML = getNumberFormatedValue(TotalValue, objField.getAttribute("Precision"));

	}
	
}

/*
function getTotalFieldByName(totalFieldName)
{
    //alert("EntryGrid__ctl"+parseInt(totalRecords+1)+"_"+totalFieldName);
    alert("EntryGrid_" + totalFieldName);
    return document.getElementById("EntryGrid_" + totalFieldName);

    //  Comment By Faseh 

	//if(parseInt(totalRecords+1) <= 9)
      //  return 	document.getElementById("EntryGrid_ctl0"+parseInt(totalRecords+1)+"_"+totalFieldName);
    //else
      //  return 	document.getElementById("EntryGrid_ctl"+parseInt(totalRecords+1)+"_"+totalFieldName);
}	
*/

function getTotalFieldByName(totalFieldName) {
    //alert("EntryGrid__ctl"+parseInt(totalRecords+1)+"_"+totalFieldName);
    if (parseInt(totalRecords + 1) <= 9)
        return document.getElementById("EntryGrid_ctl0" + parseInt(totalRecords + 1) + "_" + totalFieldName);
    else
        return document.getElementById("EntryGrid_ctl" + parseInt(totalRecords + 1) + "_" + totalFieldName);
}

function getColSum(fieldName) 
{
	sa =0;
	for (i=1;i<=totalRecords;i++) 
	{
		obj = getTabularFieldByIndex(i,fieldName);
		if (obj==null) 
			break;
		
		if (obj.value.length>0) 
		{
			sa=parseFloat(getFieldValueByRef(obj))+parseFloat(sa);
		}
	}
	return sa;
}

//function selectAllCheckBoxes(status){
//	for (var chkID = 0; chkID < totalRecords; chkID++){
//	    var rcount = chkID  ;
//	    var Ctl;


//	    if (rcount <= 9) {
//	        
//            Ctl = document.getElementById("EntryGrid_chkDelete_" + rcount);
//	        //Ctl = document.getElementById("EntryGrid_ctl0" + rcount  + "_chkDelete");
//	        //EntryGrid_chkDelete_0
//	    }
//	    else
//	        Ctl = document.getElementById("EntryGrid_chkDelete_" + rcount);
//		    //Ctl = document.getElementById("EntryGrid_ctl" + rcount  + "_chkDelete");

//		Ctl.checked=status;
//	}
//}

function selectAllCheckBoxes(status) {
    for (var chkID = 0; chkID < totalRecords-1; chkID++) {
        var rcount = chkID+2;
        var Ctl;
        if (rcount <= 9) {
            Ctl = document.getElementById("EntryGrid_ctl0" + rcount + "_chkDelete");
        }
        else {
            Ctl = document.getElementById("EntryGrid_ctl" + rcount + "_chkDelete");
        }
        //Ctl = document.getElementById("EntryGrid_chkDelete_" + rcount);
        Ctl.checked = status;
    }
}

function fccheckBoxClicked(chkBox){	
}

function eventClick1(row)
{
    try {

    
    for (var chkID = 1; chkID < totalRecords; chkID++)
    {
        var rcount = chkID + 1;
	    var rcount1 = chkID-1;
        var Ctl;

        if (rcount <= 9) {
            Ctl = document.getElementById("EntryGrid_ctl0" + rcount + "_chkDelete");
        }
        else {
            Ctl = document.getElementById("EntryGrid_ctl" + rcount + "_chkDelete");
        }
        if (Ctl.checked) {
            document.getElementById("row" + (rcount1 + 2)).style.backgroundColor = "Black";
        }
        else {
            document.getElementById("row" + (rcount1 + 2)).style.background = "FFFFFF";
        }
    }
} catch (e) {

}
}

function callSend(){ 
    if (window.event.keyCode == 9){
        send();
    }
}

function setCombo(obj,currentValue){
	for(j=0;j<obj.options.length;j++){
		if(obj.options[j].value==currentValue){
			obj.selectedIndex=j;
			return;
		}
	}
}