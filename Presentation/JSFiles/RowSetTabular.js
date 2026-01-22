var _lastEvent;
var debug = false;
var selectedRowIndex = -1;
var prvRowIndex = -1
function SetStatus(rowStatus){
	
	if (myForm.txtModifiedRows.value.indexOf(rowStatus) == -1)
		myForm.txtModifiedRows.value += rowStatus + ',' ;
}			
function HideLister()
{		
	if (document.getElementById("ListerDiv").style.visibility=="hidden"){
		(document.getElementById("ListerDiv").style.visibility="visible")
		document.getElementById("GridDiv").className="GridDivWithLister";			
		myForm.btnHideLister.value="Hide";
		myForm.btnHideLister.className="btnHideLister";
	}
	else
	{
		document.getElementById("ListerDiv").style.visibility="hidden";
		document.getElementById("GridDiv").className="GridDiv";					
		myForm.btnHideLister.value="UnHide";				
		myForm.btnHideLister.className="btnHideListerPressed";
	}
}

	function getTotalRecords()
	{
		return totalRecords;
	}
	
	function getFilterURL(obj,pKeys)
	{
		str = obj.id;
		ind = str.indexOf('_ctl')+4;
		rowInd = str.substring(ind,str.indexOf('_',ind));
		selectedRowIndex = rowInd-1;
		str_URL='';
		for(i=0;i<pKeys.length;i++)
		{
			fieldID = 'EntryGrid__ctl'+rowInd+'_'+pKeys[i];
			str_URL+=(('r_'+pKeys[i].substring(3))+'='+document.getElementById(fieldID).value+'&');
		}
		if (str_URL.length>0)
			str_URL = str_URL.substring(0,str_URL.length-1);
		
		return str_URL;
	}

	function getSelectedRowIndex()
	{
		return selectedRowIndex;
	}
	
	
	
	function getTabularFieldByPeer(obj,fieldName)
	{

		if (debug) alert("1 getTabularFieldByPeer " + obj.id + " " +  fieldName);
		var index ;
		str = obj.id;

		if (str.indexOf('row') < 0 ) {
			ind = str.indexOf('_ctl')+4;
			index = str.substring(ind,str.indexOf('_',ind));
			if (debug) alert("2 getTabularFieldByPeer " + obj + " " +  fieldName + " "  + index);
		}
		else{
			ind = str.indexOf('row');
			if (ind>=0){
				index = str.substring(3);
				if (debug) alert("3 getTabularFieldByPeer " + obj + " " +  fieldName + " "  + index);
			}
		}
		if (index<0){
			alert ('An error in getTabularFieldbyPeer');
		}

		if (debug) alert("4 return getTabularFieldByPeer " + obj + " " +  fieldName + " "  + index);
		return getTabularFieldByIndex(index-1,fieldName);
	}


	function getTabularFieldByIndex(index,fieldName)
	{
		//alert ('in getTabularFieldByPeer, index: ' + index + ', fieldname: ' + fieldName);
		index++;
		if(totalRecords==(index-1))
		{
			//alert("in New");
			obj = checkAndGetTextBoxObject(index,'new',fieldName);
			if(obj==null)
				obj = checkAndGetComboBoxObject(index,'new',fieldName);

			if(obj==null)
				obj = checkAndGetLabelObject(index,'new',fieldName);
				
		}
		else
		{
			//alert('old getTabularFieldbyIndex, index:' + index + ' fieldName: ' + fieldName);
			obj = checkAndGetTextBoxObject(index,'',fieldName);
			if(obj==null)
				obj = checkAndGetComboBoxObject(index,'',fieldName);
				
			if(obj==null)
				obj = checkAndGetLabelObject(index,'',fieldName);
				
		}
		return obj;
	}
	
/*	function internally used	*/
function checkAndGetTextBoxObject(index,rowType,fieldName)
{

	if(rowType=='new')
		preFix = 'txtNew';
	else
		preFix = 'txt';

	return document.getElementById('EntryGrid__ctl'+index+'_'+preFix+fieldName);
}

/*	function internally used	*/
function checkAndGetComboBoxObject(index,rowType,fieldName)
{

	if(rowType=='new')
		preFix = 'ddlNew';
	else
		preFix = 'ddl';

	return document.getElementById('EntryGrid__ctl'+index+'_'+preFix+fieldName);
}

/*	function internally used	*/
function checkAndGetLabelObject(index,rowType,fieldName)
{

	if(rowType=='new')
		preFix = 'lblNew';
	else
		preFix = 'lbl';

	return document.getElementById('EntryGrid__ctl'+index+'_'+preFix+fieldName);
}

/*================================================================================== */
/*Rehan
function setColSum(sourceField,totalFieldName) 
{
	if  (debug) alert ('In setColSum, totalFieldName= ' + totalFieldName + ', sourceField= ' + sourceField);
	if (debug) alert ('In setColSum, totalField: ' + document.getElementById(totalFieldName));
	document.getElementById(totalFieldName).innerHTML=getColSum(sourceField);
}
*/
function setColSum(sourceField,totalFieldName) 
{
	if  (debug) alert ('In setColSum, totalFieldName= ' + totalFieldName + ', sourceField= ' + sourceField);
	if  (debug) alert ('In setColSum, totalField: ' + document.getElementById(totalFieldName));
	//-- Support for compatible code in Java and Dot Net------------------------ 
	if (totalFieldName == "Total") {totalFieldName = "total" + sourceField ;}
	//-------------------------------------------------------------------------
	//document.getElementById(totalFieldName).innerHTML=getColSum(sourceField);
	
	if (totalRecords > 0) 	
	{
		TotalValue = getColSum(sourceField)
		objField = getTabularFieldByIndex(1,sourceField);
		getTotalFieldByName(totalFieldName).innerHTML=TotalValue;
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
			getTotalFieldByName(totalFieldName).innerHTML = getCurrencyFormatedValue(TotalValue, objField.getAttribute("Precision"));
		if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
			getTotalFieldByName(totalFieldName).innerHTML = getNumberFormatedValue(TotalValue, objField.getAttribute("Precision"));
	}
}

function getTotalFieldByName(totalFieldName)
{
	return 	document.getElementById("EntryGrid__ctl"+parseInt(totalRecords+1)+"_"+totalFieldName);
	//return 	document.getElementById(totalFieldName);
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


function setTextAreaRows(tArea_Ref){
	if (tArea_Ref.rows == 1)
		tArea_Ref.rows=4; else tArea_Ref.rows = 1;
}
function filterLister(_column, _colHeading){
	var _filterValue = prompt("Enter a value for " + _colHeading +" (% for all)", "%");
	if (_filterValue!=null)
		callEvent('Filter', _column ,_filterValue);			
}			
//======================================
//Methods to support Farrukh Button Bar
function addClicked(){
	if (debug) alert (' In tabular addClicked, last event: ' + _lastEvent);
	_lastEvent = "Add";
	callEvent('Add','', '');
}
function saveClicked(){
	if (debug)		alert (' In tabular, saveClicked, validating last event: ' + _lastEvent);
	if (beforeSend() && beforeSave()){
	//if (beforeSave()){				// Comment by Naseer as discussed with Shahid Bhai
		setSelectedSaved(true);			// By Zulfiqar to support final save anamoly
		if (debug) alert (' In tabular going to save, last event: ' + _lastEvent);
		callEvent('Save','', '');					
	}
	else
		setSelectedSaved(false);
		//parent.[0].setAllSaved(false);	
}
	
function setSelectedSaved(flag)
{
	var strLocation = new String()
	strLocation = location.href;
	strLocation = strLocation.substring(strLocation.lastIndexOf("/")+1, strLocation.lastIndexOf("."));
	markFlag =true;
	for(i=0;i<parent.arr.length;i++)
	{
		for(j=0;j<parent.arr[i].length;j++)
		{
			obj = parent.arr[i][j];
			if(markFlag)
				obj.saved=flag;
			if(parent.arr[i][j]==strLocation){
				markFlag=false;
				break;
			}
		}
	}
}

function updateClicked(){
	if (debug) alert (' In tabular, updateClicked, validating last event: ' + _lastEvent);		
	if (_lastEvent != 'Add'){
		if (beforeSend() && beforeUpdate())
		// if (beforeUpdate())			// Comment by Naseer as discussed with Shahid Bhai
			callEvent('Update','', '');
	}
}

function viewClicked(){
	if (debug) alert (' In tabular, updateClicked, validating last event: ' + _lastEvent);		
	if (_lastEvent != 'Add'){
		//if (beforeSend() && beforeUpdate())
		if (beforeUpdate())
			callEvent('Update','', '');
	}
}


function deleteClicked(){
	if (_lastEvent != 'Add' && _lastEvent != 'Delete')	
		callEvent('Delete','', '');
}
function deleteDetail(){
	callEvent('Delete','', '');
}
function editClicked(){
	callEvent('Edit','', '');
}
function sendMenu(){
	callEvent('','', '');
}

function beforeSend(){
	return true;
}
function send(){
	if (debug) alert ('In Send');
	if (beforeSend('0')) 
	{
/*	if ((_lastEvent == 'Add')(_lastEvent == 'Save') )
		if (beforeSave()){
			if (debug) alert (' In tabular going to save, last event: ' + _lastEvent);
			callEvent('Save','', '');					
		}
	}
	else if ((_lastEvent == 'Update')(_lastEvent == 'Edit') )
		if (beforeUpdate()){
			if (debug) alert (' In tabular going to update, last event: ' + _lastEvent);
			callEvent('Update','', '');					
		}
*/
		callEvent('Save','', '');					
	}

}

/*function fcvalidate(a,t,n) 
{
	return true;
}
*/
//======================================

function callEvent(eventType, arg, argValue)
{			

	if (debug) alert('in call event of tabular, lastEvent: '+_lastEvent + ', eventtype: ' + eventType);
	if (eventType == 'Delete'){				
		if (confirm("Are you sure to delete?")==false)
			return;			
	}		
	
	myForm._CustomArgName.value = arg;
	myForm._CustomArgVal.value = argValue;
	myForm._CustomEventVal.value = eventType;
	alert();
	parent.parent.showDiv(); 
	if (eventType == 'Filter')
		__doPostBack('_CustomEvent','');	
	else
		myForm._CustomEvent.onclick();
		 if (debug) alert('end call event of tabular, lastEvent: '+_lastEvent + ', eventtype: ' + eventType);		
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
	else{	
		for(i=0;i<arr_Com.length;i++){
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
/*                    Enquiry				
==================================================================================*/
//var prevRow;
//var prevRowStyle;
//function setRow(row){
//	if (prevRow!=null)
//		prevRow.className=prevRowStyle;				
//	prevRowStyle=row.className;
//	prevRow=row;
//	row.className="ListerSelItem";
//}

//for selection of a row
var prevRow;
var prevRowStyle;
function setEnquiryRow(row, fieldComb, valueComb){	
	if (prevRow!=null)
		prevRow.className=prevRowStyle;				
	prevRowStyle=row.className;
	prevRow=row;
	row.className="ListerSelItem";
	myForm._CustomArgName.value = fieldComb;
	myForm._CustomArgVal.value = valueComb;			
	afterDataListerRowClicked('');
}
function afterDataListerRowClicked(obj_Ref){	
}

function setDataListerValuesInSession() {
	var fieldAndValueComb = myForm._CustomArgName.value + '~' + myForm._CustomArgVal.value;							
	var result = RSExecute("RemoteService.aspx", "SetSessionValues", fieldAndValueComb);
	if (result.return_value.length>0){		if (debug) alert(result.return_value);		}
}

/*					clear combo items
==============================================================================*/
function clearOptions(select){
	for (var i=select.options.length-1; i>=0; i--) {
		select.options[i] = null;
	}
}

/*                    Dependent combo				
==================================================================================*/
function fcfilterChildCombo(obj_Ref,str_Qry, str_ChildName){	
	//if (debug) alert("Before"+str_Qry);	
	str_ChildName=(str_ChildName.indexOf("_m_")!=-1?str_ChildName.substring(0,str_ChildName.length-3):str_ChildName);
	str_ChildName=(str_ChildName.indexOf("_pk_")!=-1?str_ChildName.substring(0,str_ChildName.length-4):str_ChildName);
	str_ChildName=(str_ChildName.indexOf("_n_")!=-1?str_ChildName.substring(0,str_ChildName.length-3):str_ChildName);
	str_ChildName=(str_ChildName.indexOf("_d_")!=-1?str_ChildName.substring(0,str_ChildName.length-3):str_ChildName);
	
	var childCombo = document.getElementsByName(str_ChildName)(0);	
	var childValue = childCombo.value;	
	var str_ComboName=obj_Ref.name;	
	
	str_ComboName=(str_ComboName.indexOf("_m_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_pk_")!=-1?str_ComboName.substring(0,str_ComboName.length-4):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_n_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_d_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	
	str_Qry=str_Qry.replace("~ddl"+str_ComboName+"~",obj_Ref.value);
	str_Qry=str_Qry.replace("~"+str_ComboName+"~",obj_Ref.value);	
	//if (debug) alert("After:"+str_Qry);
	var result = RSExecute("RemoteService.aspx", "filterChildCombo", str_Qry);
	var comboArray = result.return_value;
	//if (debug) alert(comboArray);
	//if (debug) alert(comboArray.length);
	fcfillCombo(childCombo, comboArray);
	fcsetComboForValue(childCombo, childValue);
	if (childCombo.onchange!=null)
	{
			childCombo.onchange();
	}
}
/*
function fcfilterChildComboStartUp(obj_Ref,str_Qry,str_ChildName, childValue) {	
	var str_ComboName=obj_Ref.name;
	str_ComboName=(str_ComboName.indexOf("_m_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_pk_")!=-1?str_ComboName.substring(0,str_ComboName.length-4):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_n_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_ComboName=(str_ComboName.indexOf("_d_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
	str_Qry=str_Qry.replace("~"+str_ComboName+"~",obj_Ref.value);
	var result = RSExecute("GetComboData.aspx", "filterChildCombo", str_Qry);
	var comboArray = result.return_value;
	fcfillCombo(str_ChildName, comboArray);
	fcsetComboForValue(str_ChildName, childValue);	
}
*/

function fcfillCombo(childCombo, comboArray) {	
	fcsetArrayValuesInCombo(childCombo, comboArray);		
	//fccallOtherFuncsAfterFillingCombo(childCombo,comboArray);
}	
function fcsetArrayValuesInCombo(childCombo, comboArray) 
{
	for (i=childCombo.length-1;i>=0;i--)
		childCombo.options[i]=null;

	var opt = new Option("","");
	childCombo.options[0] = opt;
	for (i=1;i<comboArray.length+1;i++) 
	{
		var str_Value=comboArray[i-1].split("~");
		var str_AllValues="";
		for (j=1;j<str_Value.length;j++)
			str_AllValues+=str_Value[j]+"~";
		str_AllValues=(str_AllValues.length>1 && str_AllValues.indexOf("~")!=-1?str_AllValues.substring(0,str_AllValues.length-1):str_AllValues);
		//if (debug) alert(str_Value[0]+"   "+str_AllValues);
		var opt = new Option(str_Value[0],str_AllValues);
		//var opt = new Option(str_AllValues,str_Value[0]);
		childCombo.options[i] = opt;
	}
}

function fccallOtherFuncsAfterFillingCombo(childCombo,str_ArrayName){	
	//if (debug) alert(str_ComboName);
}
function fcsetComboForValue(combo,vl)  { 
  	z=combo
 	for (a=0;a<z.length;a++){
 		if (z.options[a].value==vl){ 	
 			z.options[a].selected=true; 
 			return; 
 		} 
 	}
	z.selectedIndex=-1;
}
function fcfilterChildComboWithQEI(obj_Ref,str_Qry, childCombo, str_QEI) {		
	fcfilterChildCombo(obj_Ref,str_Qry + ' ' + str_QEI, childCombo);
//	var childValue = childCombo.value;
//	var str_ComboName=obj_Ref.name;
//	str_ComboName=(str_ComboName.indexOf("_m_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
//	str_ComboName=(str_ComboName.indexOf("_pk_")!=-1?str_ComboName.substring(0,str_ComboName.length-4):str_ComboName);
//	str_ComboName=(str_ComboName.indexOf("_n_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
//	str_ComboName=(str_ComboName.indexOf("_d_")!=-1?str_ComboName.substring(0,str_ComboName.length-3):str_ComboName);
//	str_Qry = str_Qry.replace("~"+str_ComboName+"~",obj_Ref.value);

//	var result = RSExecute("GetComboData.aspx", "filterChildCombo", str_Qry + " " + str_QEI);	
//	var comboArray = result.return_value;		
//	fcfillCombo(childCombo, comboArray);\
	

//	fcsetComboForValue(childCombo, childValue);	

}

function fcfilterChildComboWithQEIAndPN(obj_Ref,str_ParentFieldName,str_Qry,str_ChildName,str_QEI) {
	str_Qry=str_Qry.replace("~"+str_ParentFieldName+"~",obj_Ref.value);
	fcfilterChildCombo(obj_Ref,str_Qry + ' ' + str_QEI, str_ChildName);
	//document.frm_ComboForm.ARRAY_QUERY.value=str_Qry+str_QEI;
	//document.frm_ComboForm.CHILD_NAME.value=str_ChildName;
	//document.frm_ComboForm.action="../servlet/shgn.SHGNComboFilterScript";
	//document.frm_ComboForm.target="RemoteComboIFrame"+(int_CurrentIFrame);
	//int_CurrentIFrame++;
	//if (int_CurrentIFrame>8)
	//	int_CurrentIFrame=1;
	//document.frm_ComboForm.submit();
}

function fcfilterChildComboWithParentName(obj_Ref, str_ParentFieldName, str_Qry, str_ChildName) {
	str_Qry=str_Qry.replace("~"+str_ParentFieldName+"~",obj_Ref.value);
	fcfilterChildCombo(obj_Ref,str_Qry , str_ChildName);
}

function checkKeyLevel(source, arguments) {
    if (numLevels<=0){
      arguments.IsValid=true;
    }
    else{
      arguments.IsValid=false;          
      var str = arguments.Value.toString();
      var len = 0;

      for (i=0;i<numLevels;i++){
          len = len + combinations[i];
          if (str.length == len ){
              arguments.IsValid=true;
              break;
          }
      }
    }
}

//		Checks	
//==============================================================================

function fccheckDecimal(v,nd,d,n){
	//if (ok==false && eg!=v) {
	//ok=true;
	//return true;
	//}
	//if (n==false && v.value=="")
	//return true;
	//var vv=v.value;
	//vv=fcremoveChar(vv,",");
	//var znd="",zd="";
	//for (z=0;z<nd;z++)
	//znd+="9";
	//for (z=0;z<d;z++)
	//zd+="9";
	//if (fcisNumberM(vv)==false) {
	//alert ("Not a valid No...");
	//ok=false;
	//eg=v;
	//v.focus();
	//return false;
	//}
	//var a=vv.indexOf(".");
	//if (a==-1 && vv.length>nd) {
	//alert ("Not a valid format ("+znd+"."+zd+")");
	//ok=false;
	//eg=v;
	//v.focus();
	//return false;
	//}
	//var b=vv.substring(0,a);
	//if (b.length>nd) {
	//alert ("Not a valid format ("+znd+"."+zd+")");
	//ok=false;
	//eg=v;
	//v.focus();
	//return false;
	//}
	//if (a==-1) {
	//ok=true;
	//return true;
	//}
	//b=vv.substring(a+1,vv.length);
	//if (b.length>d) {
	//alert ("Not a valid format ("+znd+"."+zd+")");
	//ok=false;
	//eg=v;
	//v.focus();
	//return false;
	//}
	//ok=true;
	//v.value=fcsetNo(vv);

	//numberFormat(v.id);
	return true;
}
function fccheckDefault(obj_Ref,obj_ArrayRef) {
}

					
/*					Fetch Data
=============================================================*/
function fetchData(){
	str_query = document.myForm.frm_fetchData_query;
	doubleDArray = RSExecute("RemoteService.aspx", "FetchData", str_query);
	setValuesInForm(doubleDArray);
}

function setValuesInForm(doubleDArray)
{
	for(i=0;i<doubleDArray.length;i++)	
	{
		obj = document.getElementById("txt"+doubleDArray[i,0]);
		if(obj==null)
			obj = document.getElementById("ddl"+(doubleDArray[i,0]));	
		if(obj==null)
			continue;
		obj.value=doubleDArray[i,1];
	}
}


/*			Check Default
==================================================*/
function fccheckDefault(obj_Ref,obj_ArrayRef) {
	if (obj_ArrayRef.length>0 && obj_Ref.value=="Y") {
		alert("Only One Record can be saved as default");
		obj_Ref.value="N";
	}
}

function getTotal(fieldName)
{
	return document.getElementById('total'+fieldName).innerHTML;
}

function beforeSave(){
	
	return beforeSend('1');
}
function beforeUpdate(){
	return beforeSend('1');
}
function setDecimal(obj)
{
	var amt = obj.value;
	if((amt.length>0) && (amt.indexOf(".")==-1))
	{
		//alert("parseFloat(amt):"+parseFloat(amt));
		obj.value=parseFloat(amt)+".0";
	}
}


// Check and return true if parameter value is number
// other wise return false.
function fcisNumberM(a)
{
	if (fcisEmptyM(a))
		return false;
	if (isNaN(a)==true || a=='') 
	{ 
		return false;
	} 
	return true;
}
// Check and return true if parameter value is empty after removing 
// trailing spaces other wise true.
function fcisEmptyM(z)
{
	org=z;
	if (org==null)
		return true;
	counter=0,lastIndex=0,startIndex=0,tmp="";

	while (counter!=-1)
	{
		counter=org.indexOf(" ",lastIndex);
		if (counter!=-1)
		{
			tmp+=org.substring(startIndex,counter);
			lastIndex=counter+1;
			startIndex=lastIndex;
		}
		else
		{
			tmp+=org.substring(startIndex,org.length);
		}
	}
	if (tmp=="")
		return true;
	else
		return false;
}
/******************************* functions new added ****************************************/
var setValueFromSource2Flag=false;
var setValueFromSource2obj=null;
function setValueFromSource2(objRef,srcFieldName1,srcFieldName2,targetFieldName,dataArray,message)
{	
		//alert(objRef.id);  
		var srcFieldValue1=getTabularFieldByPeer(objRef,srcFieldName1).value ;
		var srcFieldValue2=getTabularFieldByPeer(objRef,srcFieldName2).value ;
		var targetField_obj=getTabularFieldByPeer(objRef,targetFieldName);
		//alert(targetField_obj);
		var d=srcFieldValue1+"~"+srcFieldValue2;
		var found=false;
		var dataTokens=null;
		var i=0;
		for (;i<dataArray.length;i++) 
		{
			dataTokens=dataArray[i].split("~");
			if (d===dataTokens[0]+'~'+dataTokens[1]) 
			{
				//alert(d+"  "+dataTokens[0]+'~'+dataTokens[1]);
				//alert("found"+dataTokens[2]+"   "+dataTokens[3]);
				targetField_obj.value=dataTokens[2];
				//alert(targetField_obj.value);
				found=true;
				break;
			}
		}
		if(setValueFromSource2Flag)
		{
			setValueFromSource2obj = null;
			setValueFromSource2Flag=false;
			return;
		}
		
		if (!found)
		{
			targetField_obj.value="";
			if(message!=null && message.length>0)
			{
				if ( srcFieldValue2.length != 0) {
					alert(message);
					setValueFromSource2obj=objRef;
					setValueFromSource2Flag=true;	
				}
				else {
					setValueFromSource2obj=null;
					setValueFromSource2Flag=false;	
				}
			}
		}
}

function setValueFromSource3(objRef,srcFieldName1,srcFieldName2,sourceFieldName3,targetFieldName,dataArray,message)
{
	var srcFieldValue1=getTabularFieldByPeer(objRef,srcFieldName1).value ;
	var srcFieldValue2=getTabularFieldByPeer(objRef,srcFieldName2).value ;
	var srcFieldValue3=getTabularFieldByPeer(objRef,srcFieldName3).value ;
	var targetField_obj=getTabularFieldByPeer(objRef,targetFieldName);
	//alert(targetField_obj);
	var d=srcFieldValue1+"~"+srcFieldValue2+'~'+srcFieldName3;
	var found=false;
	var dataTokens=null;
	var i=0;
	for (;i<dataArray.length;i++) 
	{
		dataTokens=dataArray[i].split("~");
		if (d===dataTokens[0]+'~'+dataTokens[1]+'~'+dataTokens[2]) 
		{
			//alert(d+"  "+dataTokens[0]+'~'+dataTokens[1]+'~'+dataTokens[2]);
			//alert("found"+dataTokens[2]+"   "+dataTokens[3]);
			targetField_obj.value=dataTokens[3];
			//alert(targetField_obj.value);
			found=true;
			break;
		}
	}
	if (!found)
	{
		targetField_obj.value="";
		if(message!=null && message.length>0)
		{
			alert(message);
		}
	}
}

function set2ValuesFromSource2(objRef,srcFieldName1,srcFieldName2,targetFieldName1,targetFieldName2,dataArray,message)
{
	var srcFieldValue1=getTabularFieldByPeer(objRef,srcFieldName1).value ;
	var srcFieldValue2=getTabularFieldByPeer(objRef,srcFieldName2).value ;
	var targetField_obj1=getTabularFieldByPeer(objRef,targetFieldName1);
	var targetField_obj2=getTabularFieldByPeer(objRef,targetFieldName2);
	
	//alert(targetField_obj);
	var d=srcFieldValue1+"~"+srcFieldValue2;
	var found=false;
	var dataTokens=null;
	var i=0;
	for (;i<dataArray.length;i++) 
	{
		dataTokens=dataArray[i].split("~");
		if (d===dataTokens[0]+'~'+dataTokens[1]) 
		{
			//alert(d+"  "+dataTokens[0]+'~'+dataTokens[1]);
			//alert("found"+dataTokens[2]+"   "+dataTokens[3]);
			targetField_obj1.value=dataTokens[2];
			targetField_obj2.value=dataTokens[3];
			//alert(targetField_obj.value);
			found=true;
			break;
		}
	}
	if (!found)
	{
		targetField_obj1.value="";
		targetField_obj2.value="";
		if(message!=null && message.length>0)
		{
			alert(message);
		}
	}
}

function calculateAmountOnRate(p_objRef,rateFieldName,amountFieldName,targetFieldName,defaultValue)
{
	if(debug)alert("1");	
	rateFieldValue=getTabularFieldByPeer(p_objRef,rateFieldName).value;
	rateFieldValue=eval(rateFieldValue);
	if(debug)alert("2"+rateFieldValue);		
	amountFieldValue=getTabularFieldByPeer(p_objRef,amountFieldName).value;
	if(rateFieldValue==null || rateFieldValue.length==0)
	{
		//getTabularFieldByPeer(p_objRef,amountFieldName).value=defaultValue;
		getTabularFieldByPeer(p_objRef,targetFieldName).value=defaultValue;
	}
	if(amountFieldValue.length==0)
	{
		//alert("Enter Amount In Amount Field..");
		return;
	}
	else
	{
		if(debug)alert("3amount=>"+amountFieldValue);
		targetField_obj=getTabularFieldByPeer(p_objRef,targetFieldName);
		result=rateFieldValue*amountFieldValue;
		if(debug)alert("4result before=>"+result);
		result=doRound(result,2);
		if(debug)alert("5after"+result);	
		targetField_obj.value=isNaN(result)?defaultValue:result;
	}
}

function doRound(value,precision)
{
	result=0;
	if(value==null || value.length==0)
	{
		alert("Value:"+value+" Recieved In doRound() function is Incorrect..");
		result=0;
	}
	else
	{
		if(precision>0)
		{
			factor=Math.pow(10,precision);
			value=eval(value);
			if(debug)alert("factor=>"+factor);
			result=value*factor;	
			result=Math.round(result);
			if(debug)alert("before result=>"+result);
			result=result/factor;
			if(debug)alert("after result=>"+result);
		}
		else
		{
			result=Math.round(value);
		}		
	}
	return result;
}

function checkDuplicateLevel1(srcFieldName1,requiredFieldName,message)
{
	duplicateFound=false;
	for(j=1;j<=totalRecords;j++)
	{
 		srcFieldValue1 = getTabularFieldByIndex(j,srcFieldName1).value;
		
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
	
	for(j=1;j<=totalRecords;j++)
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
					alert(message);
					getTabularFieldByIndex(wi,srcFieldName3).focus();
					duplicateFound=true;
					break;
			}
		}
	}
	return duplicateFound;
}		



function checkDuplicateLevel4(srcFieldName1,srcFieldName2,srcFieldName3,srcFieldName4,requiredFieldName)
{
	duplicateFound=false;
	for(j=1;j<=totalRecords;j++)
	{
 		srcFieldValue1 = getTabularFieldByIndex(j,srcFieldName1).value;
		srcFieldValue2 = getTabularFieldByIndex(j,srcFieldName2).value;
		srcFieldValue3 = getTabularFieldByIndex(j,srcFieldName3).value;		
		srcFieldValue4 = getTabularFieldByIndex(j,srcFieldName4).value;		

		if (debug) alert("in duplicate "+srcFieldValue1+" - "+srcFieldValue2+" - "+srcFieldValue3+" - "+srcFieldValue4)
		for(i=j+1;i<=totalRecords;i++)
		{
			requiredFieldValue=getTabularFieldByIndex(i,requiredFieldName).value;	
			if(requiredFieldValue.length==0)continue;
			
 			srcFieldValue11 = getTabularFieldByIndex(i,srcFieldName1).value;
			srcFieldValue22 = getTabularFieldByIndex(i,srcFieldName2).value;
			srcFieldValue33 = getTabularFieldByIndex(i,srcFieldName3).value;
			srcFieldValue44 = getTabularFieldByIndex(i,srcFieldName4).value;

			if (debug) alert("to check "+srcFieldValue11+" - "+srcFieldValue22+" - "+srcFieldValue33+" - "+srcFieldValue44)
			if (srcFieldValue1=== srcFieldValue11 && srcFieldValue2===srcFieldValue22 && srcFieldValue3===srcFieldValue33 && srcFieldValue4===srcFieldValue44)
			{
					getTabularFieldByIndex(i,srcFieldName4).focus();
					duplicateFound=true;
					break;
			}
		}
	}
	return duplicateFound;
}

function getTabularFieldsAsArray(fieldArray,includeNewRow)
{
	valueArray = new Array();
	iteration = totalRecords;
	if(!includeNewRow)
		iteration--;
	for(i=1;i<=iteration;i++)
	{
		valueArray[i-1] = new Array();
		for(j=0;j<fieldArray.length;j++)
		{
			obj = getTabularFieldByIndex(i,fieldArray[j]);
			if(obj==null || obj.value=='')
				fieldVal = null;
			else
				fieldVal = obj.value;
			
			valueArray[i-1][j]=fieldVal;
		}
	}
	return valueArray;
}

function calculateAmountOnFlatRate(p_objRef,rateFieldName,amountFieldName,targetFieldName,defaultValue)
{	
	if(debug)alert("1");	
	rateFieldValue=getTabularFieldByPeer(p_objRef,rateFieldName).value;
/*
	rateFieldValue=eval(rateFieldValue);
	if(debug)alert("2"+rateFieldValue);		
	amountFieldValue=getTabularFieldByPeer(p_objRef,amountFieldName).value;
	if(rateFieldValue==null || rateFieldValue.length==0)
	{
		//getTabularFieldByPeer(p_objRef,amountFieldName).value=defaultValue;
		getTabularFieldByPeer(p_objRef,targetFieldName).value=defaultValue;
	}
	if(amountFieldValue.length==0)
	{
		//alert("Enter Amount In Amount Field..");
		return;
	}
	else
	{
		if(debug)alert("3amount=>"+amountFieldValue);
*/
		targetField_obj=getTabularFieldByPeer(p_objRef,targetFieldName);
		rateFieldValue=getTabularFieldByPeer(p_objRef,rateFieldName).value;
		//result= amountFieldValue;  // for flat rates just copy amount to base amount //rateFieldValue*amountFieldValue;
		result=rateFieldValue;
		if(debug)alert("4result before=>"+result);
		result=doRound(result,2);
		if(debug)alert("5after"+result);	
		targetField_obj.value=isNaN(result)?defaultValue:result;
	//}
}

function calculatePercentageAmount(p_objRef,rateFieldName,amountFieldName,targetFieldName,defaultValue)
{
	if(debug)alert("1");	
	rateFieldValue=getTabularFieldByPeer(p_objRef,rateFieldName).value;
	rateFieldValue=eval(rateFieldValue);
	if(debug)alert("2"+rateFieldValue);		
	amountFieldValue=getTabularFieldByPeer(p_objRef,amountFieldName).value;
	if(rateFieldValue==null || rateFieldValue.length==0)
	{
		//getTabularFieldByPeer(p_objRef,amountFieldName).value=defaultValue;
		getTabularFieldByPeer(p_objRef,targetFieldName).value=defaultValue;
	}
	if(amountFieldValue.length==0)
	{
		//alert("Enter Amount In Amount Field..");
		return;
	}
	else
	{
		if(debug)alert("3amount=>"+amountFieldValue);
		targetField_obj=getTabularFieldByPeer(p_objRef,targetFieldName);
		result=amountFieldValue * (rateFieldValue/100); 
		
		if(debug)alert("4result before=>"+result);
		result=doRound(result,2);
		if(debug)alert("5after"+result);	
		targetField_obj.value=isNaN(result)?defaultValue:result;
	}
}

function selectAllCheckBoxes(status){
	for (var chkID = 2; chkID <= totalRecords; chkID++){
		document.getElementById("EntryGrid__ctl" + chkID + "_chkDelete").checked=status;
	}
}

function fccheckBoxClicked(chkBox){	
}

function  eventClick1(row){
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