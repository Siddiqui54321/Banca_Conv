comboPressdOn=null;
debug1=false;
inner="";
listHeight=0;
pChanged = true;		//for change inparent combo value 

function getLOV_QEI(cboID){
	return "";
}

function parentChanged( parentObj,  cboID){


    comboText = getField(cboID);
    comboText.value = "";
	comboDesc = null;
	
	columnMapping = comboText.getAttribute("columnMapping");
	if (columnMapping!=null && columnMapping.length>0){
		comboDesc = document.getElementById(columnMapping);
	}
	
	if (comboDesc!=null)
		comboDesc.value="";

    var valueColumns = '';
    if(comboText.getAttribute("valueField")!=null)
	    valueColumns = comboText.getAttribute("valueField").split(',');
	
	if(valueColumns!=null && valueColumns.length>2){
		for(cntr=1;cntr<valueColumns.length;cntr++){
			getField(valueColumns[cntr]).value="";
		}
	}

	if (comboText.onchange!=null){
		//comboText.onchange();
		var newMethod = new Function(comboText.getAttribute("JavascriptOnChangeFunction"));
		newMethod();
	}


	pChanged =true;
}

function showHide(cboID){
	//alert('showHide');
	txt = document.getElementById(cboID);
	var lovType=txt.getAttribute("lovType");
	if (lovType != null && lovType.length > 0 && lovType == 'P') {
	    showPopup(cboID);
	}
	else {
	    //showNormal(cboID);
	    showPopup(cboID);
	}
}

function showNormal(cboID){

	comboDiv = document.getElementById('div'+cboID);
	comboText = document.getElementById(cboID);	
	if (!comboText.readOnly){
		if (comboDiv.className=="listHide"){	
			txtCombo = document.getElementById(cboID);
			pageNo = txtCombo.getAttribute("pageNo");
			if(pageNo =="") pageNo = 1;
			show(cboID, pageNo, '');
		}	
		else{		
			hide(comboDiv, comboText);		
		}
	}
}

function getselectedval()
{
    var query = '';
    var retValue = '';
    var query = window.location.search.substring(1);

    var sQuery = unescape(query.replace(/\+/g, " "));
    var strSplit = query.split('^');
    var txtID_DESC = strSplit[5].split('-');
    var txtID = txtID_DESC[0].toString();
    var txtDesc = txtID_DESC[1].toString();
    var txtKV = txtID_DESC[0].toString();
    if (txtID_DESC.length > 2) {
        txtID = txtID_DESC[1].toString();
        txtDesc = txtID_DESC[2].toString();
    }
    var tgt = e.target || e.srcElement;
    if (tgt && tgt.nodeName.toLowerCase() == 'td') {
        var table = document.getElementById("table");
        var row = table.rows[tgt.parentNode.rowIndex];
        var cell = row.cells[0];
        var content = cell.firstChild.nodeValue;
        retValue = content;

        if (row.cells[1] != undefined) {
            var cell2 = row.cells[1];
            var content2 = '';

            if (cell2.firstChild != null)
                content2 = cell2.firstChild.nodeValue;

            if (txtDesc != 'null') {
                retValue += "~" + content2;
            }
            if (txtID_DESC.length > 2) {
                var cell3 = row.cells[2];
                var content3 = cell3.firstChild.nodeValue;
                retValue += "~" + content3;
            }
        }
        else
            retValue += "~";
        window.returnValue = retValue;
        //alert(retValue);
    }
}


function showPopup(cboID)
{
    str_Qry = GetQuery(cboID);
	txt = document.getElementById(cboID);	
	var txtID=trim(txt.id);
	var txtCM=txt.getAttribute("columnMapping");
	if (txtCM==null || txtCM.length==0)
		txtCM="";
	var txtTF=trim(txt.getAttribute("textFields"));

    var txtFW = trim(txt.getAttribute("fieldsWidth")) == "null,null" ? "150,400" : trim(txt.getAttribute("fieldsWidth"));
	var txtFT = trim(txt.getAttribute("fieldsTitle")) == "null,null" ? "Code, Description" : trim(txt.getAttribute("fieldsTitle"));
	var txtFY = trim(txt.getAttribute("fieldsType")) == "null,null" ? "Character,Character" : trim(txt.getAttribute("fieldsType"));

	//var Url='LovPopup.htm?'+str_Qry+'^'+txtTF+'^'+txtFW+'^'+txtFT;
	var Url='LovPopup.htm?'+str_Qry+'^'+txtTF+'^'+txtFW+'^'+txtFT+'^'+txtFY;
	//alert(Url);
	var txtVF=trim(txt.getAttribute("valueField"));
	var strSplit=txtVF.split(',');
	if(strSplit.length>=2)
	{
		field = getField(strSplit[0]);
		var txtKeyCol= field.id;
		Url+='^'+txtKeyCol+'-'+txtID+'-'+txtCM;
	}
	else
	{
		Url+='^'+txtID+'-'+txtCM;
	}
	
	
	var left = (screen.width/2)-(600/2);
	var top = (screen.height/2)-(500/2);
    //alert(left); 
	//var retValue = window.open(Url, 'window', 'left:yes;dialogWidth:100px;dialogHeight:100px;status:off;help:off;top=' + top + ';left=' + left);
	var retValue = window.open(Url, 'window', "width=600,height=500");
	
	//var retValue = showModalDialog(Url, 'window', 'center:yes;dialogWidth:600px;dialogHeight:500px;status:off;help:off;top=' + top + ';left=' + left);
}

function show(cboID, pageNo, criterion)
{
    //alert(11);
	comboFrame = document.getElementById('frm'+cboID);
	comboText = document.getElementById(cboID);
	comboDiv = document.getElementById('div'+cboID);

	comboDiv.className   = "listShow";
	comboFrame.className = "frameShow";
		
	listWidth = comboText.getAttribute("divWidth");
	comboFrame.style.width = listWidth;
	comboDiv.style.width = listWidth;

	yPos = getComboTop(comboText);
	xPos = getComboLeft(comboText);
		
	comboDiv.style.top  = yPos;
	comboDiv.style.left = xPos;
	comboFrame.style.top  = yPos;
	comboFrame.style.left = xPos;
	if(comboText.getAttribute("criterion")!=null)
		criterion=comboText.getAttribute("criterion");
	else
		criterion ="";
	comboDiv.innerHTML = getInnerText(cboID, pageNo, criterion);
		
	if (comboDiv.innerHTML.length==0){
		comboDiv.innerHTML = "Record not found!";
		listHeight = 25;
		comboDiv.style.height = listHeight;
		comboFrame.style.height = listHeight;
	}
	else
		reSize(comboDiv, comboFrame, comboText);	
	
	//comboDiv.style.height = listHeight;
	//comboFrame.style.height = listHeight;		
	
	comboText.disabled=true;
	comboDiv.focus();
	
}

function reSize(comboDiv, comboFrame, comboText){

	if (listHeight>250) listHeight=250;
	var framePosition = getFrameHeightWidth();
	frameHeight = f_clientHeight();
	frameWidth = framePosition[1];
	framePosition = getFrameTopLeft();
	frameTop = f_scrollTop();
	frameLeft = framePosition[0];
	if(listHeight >= frameHeight)
		listHeight = frameHeight;
	//else if(listHeight > frameHeight)
	//	listHeight = frameHeight;

	if(comboDiv.style.width > frameWidth && frameWidth >30){
		comboDiv.style.width = frameWidth-30;
		comboFrame.style.width = frameWidth-30;
	}
	else if(comboDiv.style.width > frameWidth){
		comboDiv.style.width = frameWidth;
		comboFrame.style.width = frameWidth;
	}
	
	comboDiv.style.height = listHeight;
	comboFrame.style.height = listHeight;
	if(listHeight  > frameHeight - (frameTop +listHeight) || getComboTop(comboText)+listHeight  >= frameHeight){
		comboDiv.style.top = frameTop;
		comboFrame.style.top = frameTop;
	}


}

function getFrameTopLeft(){
	if (document.documentElement && document.documentElement.offsetLeft)
	{
		frameLeft = document.documentElement.offsetLeft;
		frameTop = document.documentElement.offsetTop;
	}
	else if (document.body)
	{
		frameLeft = document.body.offsetLeft;
		frameTop = document.body.offsetTop;
	}	
	else if (self.offsetLeft)
	{
		frameLeft = self.offsetLeft;
		frameTop = self.offsetTop;
	}

return [frameLeft, frameTop]
}

function getFrameHeightWidth(){
	if (document.documentElement && document.documentElement.clientWidth)
	{
		frameWidth = document.documentElement.clientWidth;
		frameHeight = document.documentElement.clientHeight;
	}
	else if (document.body)
	{
		frameWidth = document.body.clientWidth;
		frameHeight = document.body.clientHeight;
	}
	else if (self.innerWidth)
	{
		frameWidth = self.innerWidth;
		frameHeight = self.innerHeight;
	}

return [frameHeight, frameWidth]
}

function f_clientHeight() {
	return f_filterResults (
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}

function f_scrollTop() {
	return f_filterResults (
		window.pageYOffset ? window.pageYOffset : 0,
		document.documentElement ? document.documentElement.scrollTop : 0,
		document.body ? document.body.scrollTop : 0
	);
}

function f_filterResults(n_win, n_docel, n_body) {
	var n_result = n_win ? n_win : 0;
	if (n_docel && (!n_result || (n_result > n_docel)))
		n_result = n_docel;
	return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}

function getInnerText( cboID, pageNo, criterion){
	if (debug1 ) alert('getInnerText');
	
	txtCombo = document.getElementById( cboID );
	arrCode = "";
	arrDesc = "";
	arrInfo = "";
	arrCodes = "";
	arrDescs = "";
	
	if(pageNo=='' || pageNo<=0) pageNo = 1;	
	
	tarrCode = txtCombo.getAttribute("arrCode");
	tarrDesc = txtCombo.getAttribute("arrDesc");
	tarrInfo = txtCombo.getAttribute("arrInfo");
	tpageNo = eval(txtCombo.getAttribute("pageNo"));
	tarrCodes = txtCombo.getAttribute("arrCodes");
	tarrDescs = txtCombo.getAttribute("arrDescs");
	
	if (tarrCode != "" && tpageNo ==  pageNo && !pChanged && (criterion==null || criterion == '') ){
		arrCode = tarrCode;
		arrDesc = tarrDesc;
		arrInfo = tarrInfo;
		arrCodes = tarrCodes;
		arrDescs = tarrDescs;
	}
	else{
		if(criterion == null || criterion =='')
			str_Qry = GetQuery(cboID);
		else
			str_Qry = GetSearchQuery(cboID, criterion);
//		str_Qry = document.getElementById( cboID ).query;
		
		str_Qry =str_Qry.replace(/\\'/g,"\"");
		str_Qry = str_Qry.replace(/\\'/g, "\"");
		
		var result = RSExecute("RemoteService.aspx", "GetComboArray", str_Qry + ' ~~' + pageNo);
		
		var comboArray = result.return_value;
		arrCode = comboArray[0];
		arrDesc = comboArray[1];
		arrInfo = comboArray[2];
		arrCodes = comboArray[3];
		arrDescs = comboArray[4];
	}
	
	//pChanged=false;
	inner="";
	flag = 1;
	if(arrInfo!="" && arrInfo!=null && (arrInfo[2]=='1' || parseInt(arrInfo[0])>1) ){
		var imgPath =new String();
		imgPath =  window.location.toString();
		imgPath = (imgPath.substring(0,imgPath.lastIndexOf('/')));
		pageNav = "<a class=Prev  onmouseout=\"this.style.color='white';\"  onmouseover=\"this.style.color='blue';\" onclick = 'show(\"" + cboID + "\", " + eval(arrInfo[0]-1) + ")' title='Previous Page' >&nbsp;<<----</a>";
		pageNav += "<a class=Next onmouseout=\"this.style.color='white';\"  onmouseover=\"this.style.color='blue';\" onclick='show(\"" + cboID + "\", " + arrInfo[1] +")' title='Next Page' >---->>&nbsp;</a>";
		pageNav += "<a class=PageNo onmouseout=\"this.style.color='white';\"  onmouseover=\"this.style.color='blue';\" onclick='goLOVPage(\"" + cboID + "\")' title='Click to go to a perticular page. Total Pages " + arrInfo[3] + "' >&nbsp;Page # " + arrInfo[0]+ " of " + arrInfo[3] + "&nbsp;</a><br>";
		inner += pageNav ;
	}

	if(arrCode!=null && arrCode.length>0){
		inner += "<a class=ColHeading onclick='searchCode(\"" + cboID + "\")' style='width:30%;' onmouseout=\"this.style.fontWeight='normal';\" onmouseover=\"this.style.fontWeight='bold';\" title='Click to Search Code'> Code </a>";
		inner += "<a class=ColHeading onclick='searchDesc(\"" + cboID + "\")' style='width:70%;' onmouseout=\"this.style.fontWeight='normal';\" onmouseover=\"this.style.fontWeight='bold';\" title='Click to Search Description'> Description </a><br>";
	}

	for (i=0; i<arrCode.length;i++){
		if (flag==1) flag=2;	 else flag =1;
		//inner += "<a class=ComboItem" + flag + " onmouseout=\"this.style.fontWeight='normal';\" onmouseover=\"this.style.fontWeight='bold';\" onclick=\"putValue('" + arrCode[i] + "','" + arrDesc[i] + "','" + cboID + "','" + i + "');\" title='" + arrCode[i] + "'>" + arrCode[i] + ' - ' + arrDesc[i]+ "</a><br>";
		inner += "<a class=ComboItem" + flag + " style='width:30%' onmouseout=\"this.style.fontWeight='normal';\" onmouseover=\"this.style.fontWeight='bold';\" onclick=\"putValue('" + arrCode[i] + "','" + arrDesc[i] + "','" + cboID + "','" + i + "');\" title='" + arrCode[i] + "'>" + arrCode[i] + "</a>";
		inner += "<a class=ComboItem" + flag + " style='width:70%' onmouseout=\"this.style.fontWeight='normal';\" onmouseover=\"this.style.fontWeight='bold';\" onclick=\"putValue('" + arrCode[i] + "','" + arrDesc[i] + "','" + cboID + "','" + i + "');\" title='" + arrCode[i] + "'>" + arrDesc[i] + "</a><br>";
	}
	
	if(arrInfo[2]=='1' || parseInt(arrInfo[0])>1){
		inner += "<br>" + pageNav;
	}
	
	if( arrInfo[2]=='1' || parseInt(arrInfo[0])>1 )
	{		
		listHeight = (arrCode.length + 2)*14;
	}
	else
	{		
		listHeight = (arrCode.length *14)+25;
	}
	txtCombo.setAttribute("arrCode",arrCode); 
	txtCombo.setAttribute("arrDesc",arrDesc);	
	txtCombo.setAttribute("arrInfo",arrInfo);
	txtCombo.setAttribute("pageNo",pageNo);
	txtCombo.setAttribute("arrCodes", arrCodes);
	txtCombo.setAttribute("arrDescs", arrDescs);
		
	return inner;
}

function putValue(strCode,strDesc, cboID, selectedIndex){	
	comboText = document.getElementById(cboID);
	hide( cboID);
	valueField = comboText.getAttribute("valueField");
	if (comboText.value!=strCode || valueField.indexOf(",")>1){
		comboText.value = strCode;
		if(valueField.indexOf(",")>1)
			putOtherValues(comboText, selectedIndex);
		
		if (comboText.onchange!=null){
			//comboText.onchange();
			var newMethod = new Function(comboText.getAttribute("JavascriptOnChangeFunction"));
			newMethod();
		}
	}		
}

function putOtherValues(lov, seletecdIndex){
	valueFields = lov.getAttribute("valueField");
	arrValues = valueFields.split(',');
	if(arrValues.length >1){
		for(cntr=0;cntr < arrValues.length;cntr++){
			arrCodes = lov.getAttribute("arrCodes");
			if(lov.arrCodes==null){
				arrCodes = arrCodes.split(",");
				getField(arrValues[cntr]).value = arrCodes[ cntr + (seletecdIndex * arrValues.length)];	
			}
			else
				getField(arrValues[cntr]).value = arrCodes[ cntr + (seletecdIndex * arrValues.length)];
		}
	}
}

function hide(cboID){	
	if (debug1 ) alert('hide');
	comboFrame = document.getElementById('frm'+cboID);
	comboText = document.getElementById(cboID);
	comboDiv = document.getElementById('div'+cboID);
	comboText.setAttribute("criterion",null);
	comboDiv.className="listHide";	
	comboFrame.className="frameHide";
	comboFrame.style.width = 0;
	comboDiv.style.width = 0;

	comboText.disabled=false;
	comboText.focus();
	comboDiv.innerHTML="";

}

document.onkeypress = function (evt) {
     evt = evt || window.event;
     var keyCode = evt.keyCode ? evt.keyCode :evt.charCode ? evt.charCode : evt.which;
	if (keyCode==120 && comboPressdOn!=null){
		cmb =comboPressdOn;
		comboPressdOn=null;
		showHide(cmb);
	}
	
   }; 

function comboPress(cboID){	
	comboPressdOn=cboID;
	if (debug1 ) alert('comboPress');
	if (event.keyCode==120){		
		showHide(cboID);
	}
}

//======================================================

function Combo_Up_findPosX(obj){
	var curleft = 0;
	if (obj.offsetParent){		
		while (obj.offsetParent){			
			if (obj.id != 'EntryTableDiv')
				curleft += obj.offsetLeft;			
			obj = obj.offsetParent;			
		}
	}
	else if (obj.x) {
		curleft += obj.x;
	}
	return curleft;
}

function Combo_Up_findPosY(obj)
{
	var curtop = 0;
	if (obj.offsetParent)
	{
		
		while (obj.offsetParent)
		{
			if (obj.id != 'EntryTableDiv')
				curtop += obj.offsetTop;
			obj = obj.offsetParent;
		}
	}
	else if (obj.y)
		curtop += obj.y;
	return curtop ;
}

function CalendarPopup_Up_LostFocus(e)
{
    
    CalendarPopup_Up_HideNonCurrentCalendar('', '');
}

function putComboDescription(cboID, descID) {
    if (debug1)
        alert('putComboDescription');

    found = false;
    //alert(1);
    txt = document.getElementById(cboID);
    field = document.getElementById(descID);
    descriptionColumn = txt.getAttribute("descriptionColumn");
    valueField = txt.getAttribute("valueField");
    validation = txt.getAttribute("validation");
    textFields = txt.getAttribute("textFields");
    tableName = txt.getAttribute("tableName");
    whereColumn = txt.getAttribute("whereColumns");
    whereValue = txt.getAttribute("whereValues");
    whereOperator = txt.getAttribute("whereOperators");
    queryExtraInfo = txt.getAttribute("QEI");
    queryExtraVALUE = txt.getAttribute("queryExtraInfo");
    descriptionColumn = txt.getAttribute("descriptionColumn");
    valueField = txt.getAttribute("valueField");
    var wherePortion = "", seperatedValues = "", result = "", descValue = "";
    wherePortion = getWhereValues(whereColumn, whereValue, whereOperator, valueField, cboID).split('^');
    //whereColumn = wherePortion[0];
    //whereOperator = wherePortion[1];
    whereValue = wherePortion[2];
    seperatedValues = parseSendValueInfo(queryExtraVALUE);

    if (textFields == null)
        textFields = valueField +"," + descriptionColumn;

    //result = RSExecute("RemoteService.aspx", "GetComboDescriptionQuery", textFields + '^' + tableName + '^' + whereColumn + '^' + whereOperator + '^' + whereValue + '^' + queryExtraInfo + '^' + descriptionColumn + '^' + seperatedValues + '^' + pageNo);

   
    if (txt.value.length == 0) {

        found = true;
    }
    else if (field != null && descriptionColumn != null) {
        field.value = "";
        //str_Query = GetMappingQuery(cboID);
        if (valueField.indexOf(",") > 1) {
            valField = valueField.split(",");
            descValue += valField[1] + "=,";
            descValue = descValue.substring(0, descValue.length - 1);
            // str_Query = str_Query.replace(" where ", " where " + valField[1] + "= '" + txt.value + "' and ");
        }
        else {
            
            descValue += valueField + "=,";
            descValue = descValue.substring(0, descValue.length - 1);
            //str_Query = str_Query.replace(" where ", " where " + valueField + "= '" + txt.value + "' and ");
        }
        if (descValue.length > 0) {
            
            result = RSExecute("RemoteService.aspx", "GetComboDescriptionQuery", textFields + '^' + tableName + '^' + whereColumn + '^' + whereOperator + '^' + whereValue + '^' + queryExtraInfo + '^' + descriptionColumn + '^' + seperatedValues + '^' + '1' + '^' + descValue + '^' + txt.value);
            if (result.return_value != null && result.return_value != "") {
                found = putOtherDescription(txt, result.return_value);
            }
        }
    }
    else {
        //str_Query = GetMappingQuery(cboID);
        if (valueField.indexOf(',') > 1) {
            valField = valueField.split(",");
            descValue += valField[1] + "=,";
            descValue = descValue.substring(0, descValue.length - 1);
        }
        else {
            descValue += valueField + "=,";
            descValue = descValue.substring(0, descValue.length - 1);
        }
        if (descValue.length > 0) {
            result = RSExecute("RemoteService.aspx", "GetComboDescriptionQuery", textFields + '^' + tableName + '^' + whereColumn + '^' + whereOperator + '^' + whereValue + '^' + queryExtraInfo + '^' + descriptionColumn + '^' + seperatedValues + '^' + '1' + '^' + descValue + '^' + txt.value);
            if (result.return_value != null && result.return_value != "") {
                found = putOtherDescription(txt, result.return_value);
            }
        }
    }
    if (!found) {
        if (validation == 'N') {
            event.returnValue = true;
        }
        else {
            
            txt.value = '';
            if (document.activeElement == txt) {
                valueFound = false;
                //alert(str_Query);
                alert("Incorrect Value !");
                if (txt.valueField !=undefined) {
                    valueColumns = txt.valueField.split(',');
                    if (valueColumns.length > 2) {
                        for (cntr = 1; cntr < valueColumns.length; cntr++) {
                            getField(valueColumns[cntr]).value = "";
                        }
                    }
                }
                txt.focus();
            }
            
            if(event!=null)
                event.returnValue = false;
        }
    }
    else
        valueFound = true;
}

function putOtherDescription(field, val) {


    vals = val;
	if(vals[0]!=null && vals[1]!=null){
		descs =  vals[0];
		vals  =	 vals[1];
		var arrayDescs;
		var arrayValues;
		if(field.descriptionColumn !=null){
			descriptionColumn = field.descriptionColumn;
			arrayDescs = descriptionColumn.split(',');
		}
		if(field.valueField !=null){
			descriptionColumn = field.valueField;
			arrayValues = descriptionColumn.split(',');
		}
		if(arrayValues !=null && arrayValues.length>1)
		{
			for(cntr=0; cntr < arrayValues.length; cntr++)
			{
				valField = getField(arrayValues[cntr]);
				//Commented By Sarfraz Mughal On 23 April 2010 For Input 
				/*
				if(valField.id!=field.id && !(valField.getAttribute("readonly")) && (!(valField.getAttribute("type")=="hidden")))
				{
					alert("Value fields should be readonly");
					return false;
				}
				*/
				if(vals[cntr]!=null)
					valField.value = vals[cntr];
				else
					valField.value = "";
			}
		}
		
		columnMapping = field.getAttribute("columnMapping");
		if(columnMapping!=null){
			if(descs[0]!=null)
				document.getElementById(columnMapping).value = descs[0];
			else
				document.getElementById(columnMapping).value = "";
		}
		//if(arrayDescs !=null && arrayDescs.length>0){
		//	for(cntr=0; cntr < arrayDescs.length; cntr++){
		//		if(descs[cntr]!=null)
		//			getField(arrayDescs[cntr]).value = descs[cntr];
		//		else
		//			getField(arrayDescs[cntr]).value = "";
		//			
		//	}
		//}
	return true;
	}
	return false;
}
function goLOVPage(cboID){
	var pageValue = prompt("Enter a page number where you want to move","");
	if (pageValue!=null && parseInt(pageValue)>=1 && pageValue.length <= 4){
		show(cboID ,parseInt(pageValue), '');	
	}
}

function searchCode(cboID){
	var pageValue = prompt("Enter Code which you want to search","%");
	pageValue = "~~code~~ like '" + pageValue + "'";
	var txt = document.getElementById(cboID);
	if (pageValue!=null){
		txt.setAttribute("criterion",pageValue);
		show(cboID , 1, pageValue);
		
	}
}			

function searchDesc(cboID){
	var pageValue = prompt("Enter Description which you want to Search","%");
	switch(DBType)
	{
		case 1: 
			pageValue = "~~desc~~ like upper('" + pageValue + "')";
			break;
		case 3: pageValue = "~~desc~~ like '" + pageValue + "'";
			break;
		case 2: pageValue = "~~desc~~ like '" + pageValue + "'";
			break;
	}
	var txt = document.getElementById(cboID);
	if (pageValue!=null){
		txt.setAttribute("criterion",pageValue);
		show(cboID , 1, pageValue);
	
	}
}

function filterData(fieldId, cboID){
	
}

function GetSearchQuery(cboID, criterion){

	txt = document.getElementById(cboID);	
	textFields = txt.getAttribute("textFields");
	tableName = txt.getAttribute("tableName");
	whereColumn = txt.getAttribute("whereColumns");
	whereValue = txt.getAttribute("whereValues");
	whereOperator = txt.getAttribute("whereOperators");
	queryExtraInfo = txt.getAttribute("queryExtraInfo");
	descriptionColumn = txt.getAttribute("descriptionColumn");
	valueField = txt.getAttribute("valueField");
	
	 textFieldsArray = textFields.split(',');
	 var txtSearch=new String();
	 txtSearch = criterion;

	 if(textFieldsArray.length >= 1 && txtSearch.indexOf('~~code~~') >= 0) 
		txtSearch = textFieldsArray[0] + txtSearch.replace('~~code~~','');
	else if(textFieldsArray.length >= 2 && txtSearch.indexOf('~~desc~~') >= 0)
	{		
		// txtSearch = " upper(" + textFieldsArray[1] + ")"+ txtSearch.replace('~~desc~~','');
		switch(DBType)
		{
			case 1: 
				txtSearch = " upper(" + textFieldsArray[1] + ")"+ txtSearch.replace('~~desc~~','');
				break;
			case 3: 
			case 2: 
				txtSearch = textFieldsArray[1] + txtSearch.replace('~~desc~~','');
				break;
		}
	}	
	if (textFields!=null && textFields.length>0){
		query="select  " + textFields;
		if(descriptionColumn!=null && descriptionColumn.length>0 && descriptionColumn.indexOf(",") > 1)
		{
			query="select  selectCols " + textFields + " selectCols , ";
			query += " descCols " + descriptionColumn + " descCols ";
		}
		if(valueField!=null && valueField.length>0 && valueField.indexOf(",")>1)
		{
			if(!(query.indexOf("selectCols")>1))
				query="select  selectCols " + textFields + " selectCols , ";
			if(descriptionColumn!=null && descriptionColumn.length>0 && descriptionColumn.indexOf(",") > 1)
				query += ", valueCols " + valueField + " valueCols ";
			else
				query += " valueCols " + valueField + " valueCols ";
		}
	}

	else
		query="select  1";
	
	query += " from " + tableName + " where '1'='1' and " + txtSearch + " ";
		
	if (whereColumn!=null && whereColumn.length>0){
		whereColumns = whereColumn.split(',');
		whereValues = whereValue.split(',');
		whereOperators = whereOperator.split(',');
		valueColumns = valueField.split(',');
		var include=false;
		for(cnt=0; cnt<whereColumns.length; cnt++){					
			if(valueColumns!=null && valueColumns.length>1)
			{
			for(cntr=0;cntr<valueColumns.length;cntr++){
				if(valueColumns[cntr].indexOf(whereColumns[cnt])>-1)
					{
					include=true;	
					break; 
					}
				}
			}
			if(include){include=false;break;}
			query+=" and ";
			query += whereColumns[cnt] + whereOperators[cnt];		
			if	(whereValues[cnt].indexOf("_cl")>=0) {
				whereValues[cnt] = whereValues[cnt].replace("'","").replace("'","");
				query += "'" +  document.getElementById( GetMyRowField(cboID, whereValues[cnt].replace("_cl",""))).value + "'";
			}
			else{
				query+= whereValues[cnt];
			}			
		}				
	}		
	//query += getHandledExtraInfo(txt.queryExtraInfo);
	query += parseExtraInfo(  getHandledExtraInfo(queryExtraInfo) );		
	//query += getHandledExtraInfo( getLOV_QEI(cboID));
	//query = query.replace(/\\'/g,"\"").replace(/\\'/g,"\"");	
	return query;
}

function GetQuery(cboID)
{
	if (debug1 ) alert('GetQuery');
	var seperatedValues = '';
	txt = document.getElementById(cboID);
	textFields = txt.getAttribute("textFields");
	tableName = txt.getAttribute("tableName");
	whereColumn = txt.getAttribute("whereColumns");
	whereValue = txt.getAttribute("whereValues");
	whereOperator = txt.getAttribute("whereOperators");
	queryExtraInfo = txt.getAttribute("QEI");
	queryExtraVALUE = txt.getAttribute("queryExtraInfo");
	descriptionColumn = txt.getAttribute("descriptionColumn");
	valueField = txt.getAttribute("valueField");
	wherePortion = getWhereValues(whereColumn, whereValue, whereOperator, valueField, cboID).split('^');
    //whereColumn = wherePortion[0];
	//whereOperator = wherePortion[1];
	whereValue = wherePortion[2];
    if (queryExtraVALUE != null) {
	    seperatedValues = parseSendValueInfo(queryExtraVALUE);
	}
    var query = textFields + ':' + tableName + ':' + whereColumn + ':' + whereOperator + ':' + whereValue + ':' + queryExtraInfo + ':' + descriptionColumn + ':' + seperatedValues;
	return query;
}

function GetMappingQuery(cboID){
	
	txt = document.getElementById(cboID);			
	descriptionColumn = txt.getAttribute("descriptionColumn");
	tableName = txt.getAttribute("tableName");
	whereColumn = txt.getAttribute("whereColumns");
	whereValue = txt.getAttribute("whereValues");
	whereOperator = txt.getAttribute("whereOperators");
	queryExtraInfo = txt.getAttribute("queryExtraInfo");
	//valueField = txt.getAttribute("textFields");
	
    if(txt.getAttribute("lovType")=="P")
		valueField = txt.getAttribute("textFields");	                               
		else
		    valueField = txt.getAttribute("valueField");
        	
	if(descriptionColumn!=null && descriptionColumn.length>0){
		query="select " + descriptionColumn;
		if(valueField!=null && valueField.length>0 && valueField.indexOf(",")>1){
			query="select descCols" + descriptionColumn + "descCols";
			query+= ",valueCols" + valueField + "valueCols";
		}
	}
	else if(descriptionColumn==null && valueField!=null && valueField.length>0 && valueField.indexOf(",")>1)
		query+= "select valueCols" + valueField + "valueCols";
	else
		query="select 1";

	query += " from " + tableName + " where '1'='1' ";
	if (whereColumn!=null && whereColumn.length>0){
		whereColumns = whereColumn.split(',');
		whereValues = whereValue.split(',');
		whereOperators = whereOperator.split(',');
		//valueField = txt.getAttribute("textFields")
		if(txt.getAttribute("lovType")=="P")
		valueField = txt.getAttribute("textFields");	                               
		else
		valueField = txt.getAttribute("valueField");	
		valueColumns = valueField.split(',');
		include = false;		
		for(cnt=0; cnt<whereColumns.length; cnt++){
			if(valueColumns!=null && valueColumns.length>1)
			{
			for(cntr=0;cntr<valueColumns.length;cntr++){
				if(valueColumns[cntr].indexOf(whereColumns[cnt])>-1)
					{
					include=true;	
					break; 
					}
				}
			}
			if(include){include=false;break;}
			query+=" and ";
			query += whereColumns[cnt] + whereOperators[cnt];		
			if	(whereValues[cnt].indexOf("_cl")>=0) {
				whereValues[cnt] = whereValues[cnt].replace("'","").replace("'","");
				query += "'" +  document.getElementById( GetMyRowField(cboID, whereValues[cnt].replace("_cl",""))).value + "'";
			}
			else{
				query+= whereValues[cnt];
			}			
		}
}

	query += parseExtraInfo(  getHandledExtraInfo(queryExtraInfo) );		
	return query;
}

function getHandledExtraInfo(queryExtraInfo)
{		
	if (queryExtraInfo!='null' && queryExtraInfo!=null && queryExtraInfo.length>0){	
		queryExtraInfo = trim(queryExtraInfo);
		if ((queryExtraInfo.length>9) && (queryExtraInfo.substring(0,8).toLowerCase() == "order by" || queryExtraInfo.substring(0,8).toLowerCase() == "group by")){
		}
		else{		
			if (queryExtraInfo.substring(0,5).toUpperCase() == 'WHERE' )
				queryExtraInfo = " and " + queryExtraInfo.substring( 5, queryExtraInfo.length); 					
			else if (queryExtraInfo.substring(0,3).toUpperCase() != 'AND' )
				queryExtraInfo = " and " + queryExtraInfo;
		}	
		return " " + queryExtraInfo;
	}
	return "";
}

function parseExtraInfo(qei){
	temp = qei;
	start  =  -1;
	end  =  -1;
	fieldName ="";			
	while (true){
		start = temp.indexOf("~~");
		if (start > -1){
			start += 2;
			end = temp.indexOf("~~", start);
            fieldName = temp.substring( start , end);
			val = getField(fieldName).value;
			if (val !=null)					
				qei = qei.replace("~~" + fieldName + "~~", val);
			temp = temp.replace("~~" + fieldName + "~~");
		}
		else
			break;
	}
	return qei;
}

function parseSendValueInfo(qei) {
    temp = qei;
    start = -1;
    end = -1;
    fieldName = "";
    fieldNames = "";
    fieldValues = "";

    if(temp != null)
    {
	    while (true) {
		start = temp.indexOf("~~");
		if (start > -1) {
		    start += 2;
		    end = temp.indexOf("~~", start);
		    fieldName = temp.substring(start, end);
		    val = getField(fieldName).value;
		    if (val != null) {
			fieldNames = fieldNames + "," + fieldName;
			fieldValues = fieldValues + "," + val;
		    }
		    temp = temp.replace("~~" + fieldName + "~~");
		}
		else
		    break;
	    }
    }
    return fieldNames + "~" + fieldValues;
}

function getWhereValues(whereColumn, whereValue, whereOperator, valueField, cboID) {

    var query = "";
    var whereColumns = "";
    var whereOperators = "";
    var query_whereValues = "";
    if (whereValue != null && whereValue.length > 0) {
        var include = false;
        // whereColumns = whereColumn.split(',');
        whereValues = whereValue.split(',');
        //whereOperators = whereOperator.split(',');
        valueColumns = valueField.split(',');
        for (cnt = 0; cnt < whereValues.length; cnt++) {
            //            if (valueColumns != null && valueColumns.length > 1) {
            //                for (cntr = 0; cntr < valueColumns.length; cntr++) {
            //                    if (valueColumns[cntr].indexOf(whereColumns[cnt]) > -1) {
            //                        include = true;
            //                        break;
            //                    }
            //                }
            //            }
            //            if (include) { include = false; break; }


            //query += " and ";
            //query += whereColumns[cnt] + whereOperators[cnt];

            //            query_whereColumns   += whereColumns[0] + ",";
            //query_whereOperators += whereOperators[cnt] + ",";

            if (whereValues[cnt].indexOf("_cl") >= 0) {

                whereValues[cnt] = whereValues[cnt].replace("'", "").replace("'", "");

                //query += "'" + document.getElementById(GetMyRowField(cboID, whereValues[cnt].replace("_cl", ""))).value + "'";
                query_whereValues += "'" + document.getElementById(GetMyRowField(cboID, whereValues[cnt].replace("_cl", ""))).value + "',";
                //alert(GetMyRowField(cboID, whereValues[cnt].replace("_cl", "")));
            }
            else {
                //query += whereValues[cnt];
                query_whereValues += whereValues[cnt] + ",";
            }
        }
    }

    //query_whereColumns = query_whereColumns.substring(0, query_whereColumns.length - 1);
    //query_whereOperators = query_whereOperators.substring(0, query_whereOperators.length - 1);
    query_whereValues = query_whereValues.substring(0, query_whereValues.length - 1);



    return whereColumns + "^" + whereOperators + "^" + query_whereValues;
}

/***********************************************************************
						Differences b/w Box and Item
***********************************************************************/

function getComboLeft(comboText){
	return Combo_Up_findPosX(comboText)+3;
}
function getComboTop(comboText){
	return Combo_Up_findPosY(comboText) +15;
}

function GetMyRowField( siblingID, myID){	
//	ind = cboID.replace('__','--').indexOf('_')+1;	
//	myId = cboID.substring(ind, cboID.length);	
	return myID;
}

function isSelectOneCombo(objField){
	if(objField.getAttribute("valueField")!= null && objField.getAttribute("valueField").length>0)
	{
		return true;
	}
	return false;
}


