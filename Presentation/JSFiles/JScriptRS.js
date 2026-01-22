
function filterLister(_column) {
    alert('filterLister');
	var _filterValue = prompt("Enter a value for " + _column +" (% for all)", "%");
	if (_filterValue!=null)
		callEvent('Filter', _column ,_filterValue);			
}			
function callEvent(eventType, arg, argValue){			
	if (eventType == 'Delete'){				
		if (confirm("Are you sure to delete?")==false)
			return;			
	}		
	if (eventType == 'New')
		RefreshFields();		
	else{										
		myForm._CustomArgName.value = arg;
		myForm._CustomArgVal.value = argValue;
		myForm._CustomEventVal.value = eventType;
		if (eventType == 'Filter')
			__doPostBack('_CustomEvent','');	
		else
			myForm._CustomEvent.onclick();
	}
}

/*=========================================================================================
    Start         functions coppied as it is from SHGN_GeneralFuncs.js  *************/


// function for col management 
function fcmanageCol(cboVal,arr_Com,frameNo){
	var orgVal = trim(cboVal);
	if(orgVal.length==0) return;
	var bln_cboValueExist=fccheckCboValueExist(cboVal,arr_Com,frameNo);
	if(bln_cboValueExist){
		for(i=0;i<arr_Com.length;i++)	{
			var str_data=arr_Com[i].split("~");
			var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			if(row==null) continue;
			if(str_data[0]==cboVal){
				if(str_data[2]=="N"){
					if (row==null)
						continue;
					row.style.display="none";
				}
				else{
					if (row==null)
						continue;
					row.style.display="inline";
				}
			}
		}
	}
	else
	{
	
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			if (row==null)
				continue;
			row.style.display="inline";
		}	
	}	
}

// function for col management coppied from SHGN_GeneralFuncs.js
function fccheckCboValueExist(cboVal,arr_Com,frameNo){
	var bln_cboValueExist=false;
	for(i=0;i<arr_Com.length;i++)
	{
		var str_data=arr_Com[i].split("~");
		if(str_data[0]==cboVal)
		{
			bln_cboValueExist=true;
			break;
		}
	}
	return bln_cboValueExist;
}

function trim(str_Value) {
	str_Value=rtrim(str_Value);
	str_Value=ltrim(str_Value);
	return str_Value;
}
function rtrim(str_Value) {
	if (str_Value.length<1)
		return str_Value;
	while (str_Value.charAt(0)==" ") {
		str_Value=str_Value.substring(1);
	}
	return str_Value;
}
function ltrim(str_Value) {
	if (str_Value.length<1)
		return str_Value;
	while (str_Value.charAt(str_Value.length-1)==" ") {
		str_Value=str_Value.substring(0,str_Value.length-1);
	}
	return str_Value;
}
/*==================================================================================*/
	
	
	
	
	
/*                    to change the style of selected row				
==================================================================================*/
var prevRow;
var prevRowStyle;

function setRow(row){
	if (prevRow!=null)
		prevRow.className=prevRowStyle;				
	prevRowStyle=row.className;
	prevRow=row;
	row.className="ListerSelItem";
}
/*==============================================================================*/
	
	
	
function clearOptions(select){
	for (var i=select.options.length-1; i>=0; i--) {
		select.options[i] = null;
	}
}



/*                    Dependent combo				
==================================================================================*/
function fcfilterChildCombo(obj_Ref,str_Qry,str_ChildName) {
//	alert(str_Qry);
	var str_ComboName=obj_Ref.name;
	str_ComboName=(str_ComboName.indexOf("_m_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_pk_")!=-1?str_ComboName.substring(0,str_ComboName.length-4):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_n_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_d_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_Qry=str_Qry.replace("~"+str_ComboName+"~",obj_Ref.value);
	
	var childValue = str_ChildName.value;
	
		// To assign values to variables 
		//document.frm_ComboForm.ARRAY_QUERY.value=str_Qry;
		//document.frm_ComboForm.CHILD_NAME.value=str_ChildName;
		// To set action to service servlet
		//document.frm_ComboForm.action="../servlet/shgn.SHGNComboFilterScript";
		// To get the available IFrame.
		//document.frm_ComboForm.target=("RemoteComboIFrame"+int_CurrentIFrame);
		// To increment the avaialbel IFrame value.
		//int_CurrentIFrame++;
		//if (int_CurrentIFrame>8)
		//	int_CurrentIFrame=1;
		// To submit form.
		//document.frm_ComboForm.submit();
	var result = RSExecute("GetComboData.aspx", "filterChildCombo", str_Qry);
	var comboArray = result.return_value;
	fcfillCombo(str_ChildName, comboArray);
	fcsetComboForValue(str_ChildName, childValue);	
}


function fcfillCombo(str_ComboName, comboArray) {
	fcsetArrayValuesInCombo(str_ComboName, comboArray);
	//fccallOtherFuncsAfterFillingCombo(str_ComboName,str_ArrayName);
}	
function fcsetArrayValuesInCombo(str_ComboName, comboArray) {
	for (i=str_ComboName.length-1;i>=0;i--)
		str_ComboName.options[i]=null;
	var opt = new Option("","");
	str_ComboName.options[0] = opt;
	for (i=1;i<comboArray.length+1;i++) {
		var str_Value=comboArray[i-1].split("~");
		var str_AllValues="";
		for (j=1;j<str_Value.length;j++)
			str_AllValues+=str_Value[j]+"~";
		str_AllValues=(str_AllValues.length>1 && str_AllValues.indexOf("~")!=-1?str_AllValues.substring(0,str_AllValues.length-1):str_AllValues);
		var opt = new Option(str_Value[0],str_AllValues);
		str_ComboName.options[i] = opt;
	}
}

function fcsetComboForValue(str_ChildName,vl)  { 
  	z=str_ChildName
 	for (a=0;a<z.length;a++) 
 	{ 	
 		if (z.options[a].value===vl) 
 		{ 	
 			z.options[a].selected=true; 
 			return; 
 		} 
 	}
	z.selectedIndex=-1;
}