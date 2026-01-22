var _lastEvent;
var debug = false;

function HideLister(){		
	if (document.getElementById("ListerDiv").style.visibility=="hidden"){
		(document.getElementById("ListerDiv").style.visibility="visible")
		document.getElementById("GridDiv").className="GridDivWithLister";			
		myForm.btnHideLister.value="Hide";
		myForm.btnHideLister.className="btnHideLister";					
	}
	else{
		document.getElementById("ListerDiv").style.visibility="hidden";
		document.getElementById("GridDiv").className="GridDiv";					
		myForm.btnHideLister.value="UnHide";				
		myForm.btnHideLister.className="btnHideListerPressed";
	}
}

/*================================================================================== */

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
	if (debug) alert('in normal add clicked');
	_lastEvent = "Add";
	callEvent('Add','', '');
}
function saveClicked(){
	if (debug) alert('in Agent save clicked, validating,Last Event: ' + _lastEvent );
	//if (_lastEvent == 'Add')
	//{
		if (beforeSave())
		{	if (debug) alert('going to save normal');
			_lastEvent = "Save";
			callEvent('Save','', '');					
		}
	//}		
}
function updateClicked(){
	if (_lastEvent != 'Add' && _lastEvent != 'Delete'){
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

function send()
{
	callEvent('Save','', '');
}

function fcvalidate(a,t,n) 
{
	return true;
}

//======================================

function callEvent(eventType, arg, argValue)
{			
	if (debug) alert('in call event of Controller Agent, lastEvent: ' + _lastEvent + ', eventType: ' + eventType);
	if (eventType == 'Delete')
	{				
		if (confirm("Are you sure to delete?")==false)
			return;			
	}		
	myForm._CustomArgName.value = arg;
	myForm._CustomArgVal.value = argValue;
	myForm._CustomEventVal.value = eventType;
	if (eventType == 'Filter')
		__doPostBack('_CustomEvent','');	
	else
		myForm._CustomEvent.onclick();
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
		if (debug) alert("Only One Record can be saved as default");
		obj_Ref.value="N";
	}
}


function beforeSave(){return true;}
function beforeUpdate(){return true;}
//afterDataListerRowClicked(obj_Ref)


//************** Method for Error Message ******************

function ErrorMessage(errMsg){
		var shortMessage = new String();
		var longMessage = new String();
		longMessage = shortMessage = errMsg;
		if(longMessage.indexOf("<ErrorMessage>",0)!=-1){
			alert();
			longMessage = longMessage.replace("<ErrorMessage>","Message:");
			longMessage = longMessage.replace("<ErrorMessageDetail>","\n\nDetail:");
			shortMessage = shortMessage.substring(("<ErrorMessage>").length ,shortMessage.indexOf("<ErrorMessageDetail>",0)) + "\n Dont Show Detail?";
			confirm(shortMessage)==false?alert(longMessage):"";
		}
		else
			alert(errMsg);
}