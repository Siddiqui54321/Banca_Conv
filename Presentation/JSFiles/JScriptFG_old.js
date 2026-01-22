var _lastEvent;
var ok = true;
var eg;
var debug = false;
var AllowedProcess = new Array();
var g_list_Seperator = "~~!~~";
var g_item_Seperator = "^^|^^";
var vr_lastSearch;
var ctlprefix = "";
var ctlprefixName = "";



function HideLister() {
    if (document.getElementById("ListerDiv").style.visibility == "hidden") {
        (document.getElementById("ListerDiv").style.visibility = "visible")
        document.getElementById("PagerDiv").style.visibility = "visible";

        var gridDiv = document.getElementById("GridDiv");
        if (gridDiv == null) {
            gridDiv = document.getElementById("EntryTableDiv");
        }
        gridDiv.className = "GridDivWithLister";

        myForm.btnHideLister.value = "<";
        myForm.btnHideLister.className = "btnHideLister";
    }
    else {
        document.getElementById("ListerDiv").style.visibility = "hidden";
        document.getElementById("PagerDiv").style.visibility = "hidden";


        var gridDiv = document.getElementById("GridDiv");
        if (gridDiv == null) {
            gridDiv = document.getElementById("EntryTableDiv");
        }
        gridDiv.className = "GridDiv";

        myForm.btnHideLister.value = ">";
        myForm.btnHideLister.className = "btnHideListerPressed";
    }
}

function fcsetInitValues() {
}
/*
function setTextAreaRows(tArea_Ref){
if (tArea_Ref.rows == 1)
tArea_Ref.rows=4; else tArea_Ref.rows = 1;
}*/
function setTextAreaRows(tArea_Ref, noOfRowsOfTextArea) {
    TextAreaRows = 5;
    if (noOfRowsOfTextArea != null && noOfRowsOfTextArea != 'null') {
        TextAreaRows = noOfRowsOfTextArea;
    }

    if (tArea_Ref.rows == 1) {
        tArea_Ref.rows = TextAreaRows;
    }
    else tArea_Ref.rows = 1;
}


function getFieldComb() {
    //fieldComb = myForm._CustomArgName.value;
    fieldComb = getField("_CustomArgName").value;
    return fieldComb;
}

function getSelectedFieldValue(name) {
    //alert(name);
    fieldComb = getFieldComb();
    valueComb = getValueComb();

    //	alert(fieldComb);
    //alert(valueComb);

    s = fieldComb.split(',');
    //v = valueComb.split(',');
    v = valueComb.split(g_item_Seperator);

    for (i = 0; i < s.length; i++) {
        //alert('Field = '+ s[i] + ' Value = ' +v[i]);
        if (name != null) {
            if (name == s[i]) {
                return v[i];
            }
        }
    }
}

function getValueComb() {
    //valueComb = myForm._CustomArgVal.value;
    valueComb = getField("_CustomArgVal").value;
    return valueComb;
}

function filterLister(_column, _colHeading) {
    //if (document.getElementById('_CustomHeaderVal') != null)
    if (getField("_CustomHeaderVal") != null) {
        var HeaderVal = "";
        var HeaderColumn = "";
        var _filterValue = "";

        var Arr = new Array();
        temp = getField("_CustomHeaderVal").value
        Arr = temp.split('~');

        for (var y = 0; y < Arr.length; y++) {
            HeaderColumn = Arr[y].substr(0, Arr[y].indexOf(","));
            if (HeaderColumn == _column) {
                HeaderVal = Arr[y].substr(Arr[y].indexOf(",") + 1)
            }

        }

        if (HeaderVal == "") {
            _filterValue = prompt("Enter a value for " + _colHeading + " (% for all)", "%");
        }
        else {
            _filterValue = prompt("Enter a value for " + _colHeading + " (% for all)", HeaderVal);
        }
    }
    else {
        _filterValue = prompt("Enter a value for " + _colHeading + " (% for all)", "%");
    }
    if (ValidateInput(_filterValue) || _filterValue.indexOf('%') > -1) {
        if (_filterValue != null)
            callEvent('Filter', _column, _filterValue);
    }
    else {
        alert("Invalid Input");
    }
}

function ValidateInput(val) {
    if (val != '') {
        var regExp = /^([a-zA-Z0-9 ]*\.)*[a-zA-Z0-9 ]*$/;
        //--/[^a-zA-Z0-9 ]/
        if (regExp.test(val)) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return true;
    }
}

//Methods to support Farrukh Button Bar
function addClicked() {
    if (_lastEvent != 'View') {
        callEvent('New', '', '');
        _lastEvent = "New";
    }
}

function saveClicked() {
    if (_lastEvent == 'New' || _lastEvent == 'Save') {
        if (beforeSave())
            callEvent('Save', '', '');
    }
}
function updateClicked() {
    if (_lastEvent != 'New' && _lastEvent != 'Save' && _lastEvent != 'View') {
        if (beforeUpdate()) { 
            callEvent('Update', '', '');
        }
    }

}
function deleteClicked() {
    if (_lastEvent == 'Edit')
        if (beforeDelete())
            callEvent('Delete', '', '');
}

function FilterClicked() {
    //alert();
    //if (_lastEvent == 'Edit')
    if (beforeFilter())
        callEvent('Filter', '', '');
}

function deleteDetail() {
    callEvent('Delete', '', '');
}
function editClicked() {
    callEvent('Edit', '', '');
}
function sendMenu() {
    callEvent('', '', '');
}
function send() {
    if (beforeSave())
        callEvent('Save', '', '');
}
function fcsubmitGenProcess() {
    callEvent('Save', '', '');
}

function fcsubmitGenProcess1() {
    callEvent('Save', '', '');
}

function fcsubmitTranProcess(callType) {
    if (callType == "Post") { callCustomEvent('Save', '', ''); }
    else
        if (callType == "Ver") { callCustomEvent('Update', '', ''); }
        else { callCustomEvent('Delete', '', ''); }
}

function executeProcess(proccessName) {
    if (!beforeExecuteProcess())
        return;
    if (_lastEvent == 'Edit' || _lastEvent == 'View') {
        if (proccessName == "shsm.SHSM_VerifyTransaction" || proccessName == "shsm.SHSM_RejectTransaction") {
            if (IsProcessAllowed(proccessName)) {
                callEvent('Process', 'ProcessName', proccessName);
            }
        }
        else {
            callEvent('Process', 'ProcessName', proccessName);
        }
    }
}

function fcvalidate(a, t, n) {
    return true;
}

function sendToFieldValueAutoGenerator() {
    //callEvent('Save','', '');
    saveClicked();
}

function callCustomEvent(eventType, arg, argValue) {
    if (eventType == 'New')
        RefreshFields();
    else {
        //		myForm._CustomArgName.value = arg;
        //		myForm._CustomArgVal.value = argValue;
        //		myForm._CustomEventVal.value = eventType;

        getField("_CustomArgName").value = arg;
        getField("_CustomArgVal").value = argValue;
        getField("_CustomEventVal").value = eventType;

        if (eventType == 'Filter')
            __doPostBack(ctlprefix + '_CustomEvent', '');
        else
            getField("_CustomEvent").onclick();
    }
}

function callEvent(eventType, arg, argValue)
{
    
    if (eventType == 'Delete') {
        if (confirm("Are you sure to delete?") == false)
            return;
    }
    if (eventType == 'New')
        RefreshFields();
    else {

        getField("_CustomArgName").value = arg;
        getField("_CustomArgVal").value = argValue;
        getField("_CustomEventVal").value = eventType;

        //if (eventType == 'Filter' || eventType == 'Delete')
        if (eventType == 'Delete') {
            __doPostBack(ctlprefixName + '_CustomEvent', '');
        }
        else {
            
            getField("_CustomEvent").onclick();
        }
    }
    //if (typeof(Page_IsValid)!='undefined' || Page_IsValid)
    if (typeof (Page_IsValid) != 'undefined') {
        if (Page_IsValid) {
            if (eventType != 'New' && eventType != 'Menu' && Page_IsValid == true)
                blockDoubleSubmit(true);

        }
    }

}
/*=========================================================================================
Start         functions coppied as it is from SHGN_GeneralFuncs.js  *************/
// function for col management 
function fcmanageCol(cboVal, arr_Com, frameNo) {
    var orgVal = trim(cboVal);
    if (orgVal.length == 0) return;
    var bln_cboValueExist = fccheckCboValueExist(cboVal, arr_Com, frameNo);
    if (bln_cboValueExist) {
        for (i = 0; i < arr_Com.length; i++) {
            var str_data = arr_Com[i].split("~");
            var row = parent.frames[frameNo].document.getElementById("row" + str_data[1]);
            if (row == null) continue;
            if (str_data[0] == cboVal) {
                if (str_data[2] == "N") {
                    if (row == null)
                        continue;
                    row.style.display = "none";
                }
                else {
                    if (row == null)
                        continue;
                    row.style.display = "inline";
                }
            }
        }
    }
    else {
        for (i = 0; i < arr_Com.length; i++) {
            var str_data = arr_Com[i].split("~");
            var row = parent.frames[frameNo].document.getElementById("row" + str_data[1]);
            if (row == null)
                continue;
            row.style.display = "inline";
        }
    }
}

// function for col management coppied from SHGN_GeneralFuncs.js
function fccheckCboValueExist(cboVal, arr_Com, frameNo) {
    var bln_cboValueExist = false;
    for (i = 0; i < arr_Com.length; i++) {
        var str_data = arr_Com[i].split("~");
        if (str_data[0] == cboVal) {
            bln_cboValueExist = true;
            break;
        }
    }
    return bln_cboValueExist;
}

function trim(str_Value) {
    str_Value = rtrim(str_Value);
    str_Value = ltrim(str_Value);
    return str_Value;
}
function rtrim(str_Value) {
    if (str_Value.length < 1)
        return str_Value;
    while (str_Value.charAt(0) == " ") {
        str_Value = str_Value.substring(1);
    }
    return str_Value;
}
function ltrim(str_Value) {
    if (str_Value.length < 1)
        return str_Value;
    while (str_Value.charAt(str_Value.length - 1) == " ") {
        str_Value = str_Value.substring(0, str_Value.length - 1);
    }
    return str_Value;
}

/*              Enquiry				
=======================================*/

//for selection of a row
var prevRow;
var prevRowStyle;
function setEnquiryRow(row, fieldComb, valueComb) {


    if (prevRow != null)
        prevRow.className = prevRowStyle;
    if (row != null) {
        prevRowStyle = row.className;
        prevRow = row;
        row.className = "ListerSelItem";
        getField("_CustomArgName").value = getDataListerNames(fieldComb);
        getField("_CustomArgVal").value = getDataListerValues(row);
        afterDataListerRowClicked('');
    }

}

function getDataListerNames(fieldComb) {

    if (typeof _sessionEnqVarList != 'undefined') {
        return _sessionEnqVarList;
    }
    else {
        return fieldComb;
    }
}
function extractRowPrefixFromRowId(row) {
    end = row.id.indexOf("_EnqRow");
    var rowPrefix = row.id.substring(0, end);
    return rowPrefix;
}

function extractRowPostfixFromRowId(row) {
    end = row.id.indexOf("_EnqRow");
    var rowPostfix = row.id.substring(end + 7);
    return rowPostfix;
}

function getDataListerValues(row) {
    var valueList = "";
    var cellName = "";
    var celly;
    var nameList = getField("_CustomArgName").value.split(",");
    var prefix = extractRowPrefixFromRowId(row);
    var postfix = extractRowPostfixFromRowId(row);
    for (i = 0; i < row.cells.length; i++) {
        celly = row.cells[i];
        for (j = 0; j < nameList.length; j++) {
            cellName = prefix + '_lbl' + nameList[j] + postfix;
            if (celly.childNodes[0].id == cellName) {
                valueList = valueList + celly.childNodes[0].value + g_item_Seperator
                break;
            }
        }
    }

    if (valueList.length > 0) {
        valueList = valueList.substring(0, valueList.length - g_item_Seperator.length)
    }
    return valueList;
}

function setDataListerValuesInSession() {

    var fieldAndValueComb = getField("_CustomArgName").value + g_list_Seperator + getField("_CustomArgVal").value;
    var result = RSExecute("RemoteService.aspx", "SetSessionValues", fieldAndValueComb);
    if (result.return_value.length > 0) { alert(result.return_value); }
}

function afterDataListerRowClicked(obj_Ref) { }

function setFixedValuesInSession(fieldValue) {
    //	var fieldAndValueComb = myForm._CustomArgName.value + '~' + myForm._CustomArgVal.value;							
    //alert(fieldValue);
    var result = RSExecute("RemoteService.aspx", "SetFixedSessionValues", fieldValue);
    if (result.return_value.length > 0) { alert(result.return_value); }
}

function getEnquiryFieldByIndex(index, fieldName) {
    if (debug) alert('getEnquiryFieldByIndex, index:' + index + ' fieldName: ' + fieldName);

    index--;
    obj = document.getElementById('lister__ctl' + index + '_lbl' + fieldName);
    if (obj == null)
        obj = document.getElementById('lister__ctl' + index + '_txt' + fieldName);

    return obj;
}
function getEnquiryFieldValue(rowIndex, colName) {
    var returnValue;
    objField = getEnquiryFieldByIndex(rowIndex, colName);
    returnValue = objField.value;
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        returnValue = getDeformattedCurrency(objField, objField.getAttribute("Precision"));
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        returnValue = getDeformattedNumber(objField, objField.getAttribute("Precision"));
    return returnValue;
}



/*					clear combo items
==============================================================================*/
function clearOptions(select) {
    for (var i = select.options.length - 1; i >= 0; i--) {
        select.options[i] = null;
    }
}

/*                    Dependent combo				
==================================================================================*/
function fcfilterChildCombo(obj_Ref, str_Qry, str_ChildName) {
    str_ChildName = (str_ChildName.indexOf("_m_") != -1 ? str_ChildName.substring(0, str_ChildName.length - 3) : str_ChildName);
    str_ChildName = (str_ChildName.indexOf("_pk_") != -1 ? str_ChildName.substring(0, str_ChildName.length - 4) : str_ChildName);
    str_ChildName = (str_ChildName.indexOf("_n_") != -1 ? str_ChildName.substring(0, str_ChildName.length - 3) : str_ChildName);
    str_ChildName = (str_ChildName.indexOf("_d_") != -1 ? str_ChildName.substring(0, str_ChildName.length - 3) : str_ChildName);
    var childCombo = document.getElementsByName(str_ChildName)(0);
    if (childCombo == null)
        childCombo = getField(str_ChildName);
    var childValue = childCombo.value;
    var str_ComboName = obj_Ref.name.replace("ddl", "");
    str_ComboName = str_ComboName.replace("txt", "");
    str_ComboName = (str_ComboName.indexOf("_m_") != -1 ? str_ComboName.substring(0, str_ComboName.length - 3) : str_ComboName);
    str_ComboName = (str_ComboName.indexOf("_pk_") != -1 ? str_ComboName.substring(0, str_ComboName.length - 4) : str_ComboName);
    str_ComboName = (str_ComboName.indexOf("_n_") != -1 ? str_ComboName.substring(0, str_ComboName.length - 3) : str_ComboName);
    str_ComboName = (str_ComboName.indexOf("_d_") != -1 ? str_ComboName.substring(0, str_ComboName.length - 3) : str_ComboName);
    str_Qry = str_Qry.replace("~ddl" + str_ComboName + "~", obj_Ref.value);
    str_Qry = str_Qry.replace("~" + str_ComboName + "~", obj_Ref.value);
    var result = RSExecute("RemoteService.aspx", "filterChildCombo", str_Qry);
    var comboArray = result.return_value;
    fcfillCombo(childCombo, comboArray);
    fcsetComboForValue(childCombo, childValue);
    try {
        if (childCombo.onchange != null) {
            childCombo.onchange();
        }
    } catch (e) {

    }
}

function fcfillCombo(childCombo, comboArray) {
    fcsetArrayValuesInCombo(childCombo, comboArray);
}
function fcsetArrayValuesInCombo(childCombo, comboArray) {
    for (i = childCombo.length - 1; i >= 0; i--)
        childCombo.options[i] = null;

    var opt = new Option("", "");
    childCombo.options[0] = opt;
    for (i = 1; i < comboArray.length + 1; i++) {
        var str_Value = comboArray[i - 1].split("~");
        var str_AllValues = "";
        for (j = 1; j < str_Value.length; j++)
            str_AllValues += str_Value[j] + "~";
        str_AllValues = (str_AllValues.length > 1 && str_AllValues.indexOf("~") != -1 ? str_AllValues.substring(0, str_AllValues.length - 1) : str_AllValues);
        var opt = new Option(str_Value[0], str_AllValues);
        childCombo.options[i] = opt;
    }
}

function fccallOtherFuncsAfterFillingCombo(childCombo, str_ArrayName) { }
function fcsetComboForValue(combo, vl) {
    z = combo
    for (a = 0; a < z.length; a++) {
        if (z.options[a].value == vl) {
            z.options[a].selected = true;
            return;
        }
    }
    z.selectedIndex = -1;
}
function fcfilterChildComboWithQEI(obj_Ref, str_Qry, childCombo, str_QEI) {
    fcfilterChildCombo(obj_Ref, str_Qry + ' ' + str_QEI, childCombo);
}

function fcfilterChildComboWithQEIAndPN(obj_Ref, str_ParentFieldName, str_Qry, str_ChildName, str_QEI) {
    str_Qry = str_Qry.replace("~" + str_ParentFieldName + "~", obj_Ref.value);
    fcfilterChildCombo(obj_Ref, str_Qry + ' ' + str_QEI, str_ChildName);
}

function fcfilterChildComboWithParentName(obj_Ref, str_ParentFieldName, str_Qry, str_ChildName) {
    str_Qry = str_Qry.replace("~" + str_ParentFieldName + "~", obj_Ref.value);
    fcfilterChildCombo(obj_Ref, str_Qry, str_ChildName);
}

function checkKeyLevel(source, arguments) {
    if (numLevels <= 0) {
        arguments.IsValid = true;
    }
    else {
        arguments.IsValid = false;

        var str = arguments.Value;
        var len = 0;

        for (i = 0; i < numLevels; i++) {
            len = len + combinations[i];
            if (str.length == len) {
                arguments.IsValid = true;
                break;
            }
        }
    }
}

function fccheckDefault(obj_Ref, obj_ArrayRef) { }


/*					Fetch Data
=============================================================*/

function checkFetchDataDate(vals) {

    var chkVal = new String();
    chkVal = vals;

    if (chkVal == null || chkVal == "")
        return chkVal;
    if (isNaN(chkVal.substr(1, 1)))
        return chkVal;
    if (chkVal.length != 19)
        return chkVal;

    if (!(chkVal.indexOf("/") > 0 || chkVal.indexOf("-") > 0))
        return chkVal;

    if (!(chkVal.indexOf(":", 1) > 0))
        return chkVal;

    if (!(chkVal.lastIndexOf("/") == 5 || chkVal.lastIndexOf("-") == 5))
        return chkVal;

    return chkVal.substr(0, 10);
}

function fetchData() {
    str_query = document.getElementById("frm_FetchData_qry").value;
    var result = RSExecute("RemoteService.aspx", "FetchData", str_query);
    var SingleArray = result.return_value;
    setValuesInForm(SingleArray);
}

function setValuesInForm(SingleArray) {

    for (arrayi = 0; arrayi < SingleArray.length; arrayi++) {
        var d = SingleArray[arrayi].split("~");

        firstElement = d[0];
        secondElement = d[1];
        // Following line is added against commented code By REhan on 10/10/06
        setFieldValue(firstElement, checkFetchDataDate(secondElement))
        /* By REhan on 10/10/06
        obj = document.getElementById("txt"+firstElement);
        if(obj==null)
        obj = document.getElementById("ddl"+ firstElement);	
        if(obj==null)
        obj = document.getElementById(firstElement);	
        if(obj==null)
        continue;
        obj.value= checkFetchDataDate(secondElement);
        */

    }
}



/*			Check Default
==================================================*/
function fccheckDefault(obj_Ref, obj_ArrayRef) {
    if (obj_ArrayRef.length > 0 && obj_Ref.value == "Y") {
        alert("Only One Record can be saved as default");
        obj_Ref.value = "N";
    }
}
function checkDuplicate(obj_Ref, obj_ArrayRef, str_Message) {
    for (i = 0; i < obj_ArrayRef.length; i++) {
        if (obj_ArrayRef[i] == obj_Ref.value) {
            alert((str_Message == null || str_Message == "" ? "Duplicate Value" : str_Message));
            if (obj_Ref.type != "text")
                obj_Ref.selectedIndex = -1;
            else
                obj_Ref.value = "";
            obj_Ref.focus();
            return;
        }
    }
}
function checkDuplicateTwo(str_Id1, str_Id2, obj_ArrayRef, str_Message) {
    var str_Value = document.getElementById(str_Id1).value + "~" + document.getElementById(str_Id2).value;
    for (i = 0; i < obj_ArrayRef.length; i++) {
        if (obj_ArrayRef[i] == str_Value) {
            alert((str_Message == null || str_Message == "" ? "Duplicate Value" : str_Message));
            if (document.getElementById(str_Id2).type != "text")
                document.getElementById(str_Id2).selectedIndex = -1;
            else
                obj_Ref.value = "";
            document.getElementById(str_Id2).focus();
            return;
        }
    }
}

//Function support
function fcgetMaxDateM(b, bb) {
    var sep = "", c = 0, count = 0, li = 0;
    var day, month, year;
    if (b.indexOf("/") != -1)
        sep = "/";
    else if (b.indexOf("-") != -1)
        sep = "-";
    c = 0, li = 0, si = 0;
    c = b.indexOf(sep, li);
    day = eval(b.substring(si, c));
    li = c + 1; si = li;
    c = b.indexOf(sep, li);
    month = eval(b.substring(si, c));
    li = c + 1;
    year = eval(b.substring(li, b.length));
    sep = "", count = 0;
    var day1, month1, year1;
    if (bb.indexOf("/") != -1)
        sep = "/";
    else if (bb.indexOf("-") != -1)
        sep = "-";
    c = 0, li = 0, si = 0;
    c = bb.indexOf(sep, li);
    day1 = eval(bb.substring(si, c));
    li = c + 1; si = li;
    c = bb.indexOf(sep, li);
    month1 = eval(bb.substring(si, c));
    li = c + 1;
    year1 = eval(bb.substring(li, bb.length));
    if (year > year1)
        return 1;
    else if (year < year1)
        return 2;
    if (month > month1)
        return 1;
    else if (month < month1)
        return 2;
    if (day > day1)
        return 1;
    else if (day < day1)
        return 2;
    return 0;
}


//Add Methods SAFDAR
function beforeSave() { return true; }
function beforeUpdate() { return true; }
function beforeDelete() { return true; }
function beforeExecuteProcess() { return true; }
function beforeFilter() { return true; }

function setFetchDataQry(strQry) {
    var str1 = new String();
    str1 = strQry;
    //alert(str1);
    while (str1.indexOf("+") > 0) {
        str1 = str1.replace("+", " _Concat ");
    }

    document.getElementById("frm_FetchData_qry").value = str1;
    //document.myForm.frm_FetchData_qry.value = str1;
}
function sendToAutoGenerator() {
    saveClicked();
}
function setDefaultValues() { }
function setValidPeriod() { alert('FG'); }

// To change the date format.
// Parameter
// sourceDate		source date
// sourceFormat		format of source date
// targetFormat		target date format
// sourceSep		source data separator
// targetSep		target date separator

function fcchangeFormat(sourceDate, sourceFormat, targetFormat, sourceSep, targetSep) {

    var str_targetFormatDate = "";
    var data = sourceDate.split(sourceSep);
    var day, mon, year;
    if ((sourceFormat == "dd/mm/yyyy") || (sourceFormat == "DD/MM/YYYY")) {
        day = data[0];
        mon = data[1];
        year = data[2];
    }
    if ((sourceFormat == "yyyy-mm-dd") || (sourceFormat == "YYYY-MM-DD")) {
        day = data[2];
        mon = data[1];
        year = data[0];
    }
    if ((targetFormat == "dd/mm/yyyy") || (sourceFormat == "DD/MM/YYYY")) {
        str_targetFormatDate = day + targetSep + mon + targetSep + year;
    }
    if ((targetFormat == "mm/dd/yyyy") || (sourceFormat == "MM/DD/YYYY")) {
        str_targetFormatDate = day + targetSep + mon + targetSep + year;
    }
    return str_targetFormatDate;
}



// -----------------------fcvalidate 
function fcvalidate(a, t, n) {
    // If one component has failed and at same time another object
    // is calling this method return true (not validate that object), b/c
    // untill first component validation succeeds it keeps on attempting
    // and if this function is called onblur() of both components then it creates 
    // problem. So it doesn't call validation of 2nd component until first succeeds.
    if (ok == false && eg != a) {
        ok = true;
        return true;
    }

    // If required is false and component has blank vlaue then return true.
    if (n == false && a.value == "")
        return true;

    // If data type is date (d/D) then call date function and show message if validation fails.
    if (t == "D" || t == "d") {
        if (fcisDateForId(a) == false) {
            alert("Not a Valid Date");
            ok = false;
            eg = a;
            a.focus();
            return false;
        }
    }
    // If data type is number (n/N) then call number function and show message if validation fails.
    else if (t == "N" || t == "n") {
        if (fcisNumberForId(a) == false) {
            alert("Not a Numeric Value");
            ok = false;
            eg = a;
            a.focus();
            return false;
        }
        //numberFormat(a.id);
    }
    // If data type is character (c/C) then call character function and show message if validation fails.
    else if (t == "C" || t == "c") {
        if (fcisEmptyForId(a) == true) {
            alert("Blank not allowed...");
            a.value = "";
            ok = false;
            eg = a;
            a.focus();
            return false;
        }
    }
    ok = true;
    return true;
}
// Check and return true if parameter value is empty after removing 
// trailing spaces other wise true.
function fcisDateForId(xxx) {
    try {
        b = xxx.value;
        var sep = "", c, count = 0, li = 0;
        var day, month, year;
        if (b.indexOf("/") != -1)
            sep = "/";
        else if (b.indexOf("-") != -1)
            sep = "-";
        if (sep == "" && b.length == 8) {
            xxx.value = b.substring(0, 2) + "/" + b.substring(2, 4) + "/" + b.substring(4, 8);
            b = xxx.value;
            sep = "/";
        }
        if (sep != "") {
            while (c != -1) {
                c = b.indexOf(sep, li);
                if (c != -1) {
                    count++;
                    li = c + 1;
                }
            }
        }
        if (count != 2) {
            return false;
        }
        c = 0, li = 0, si = 0;
        c = b.indexOf(sep, li);
        day = b.substring(si, c);
        li = c + 1; si = li;
        c = b.indexOf(sep, li);
        month = b.substring(si, c);
        li = c + 1;
        year = b.substring(li, b.length);

        var tday = eval(day) / 100;
        var tmon = eval(month) / 100;
        var tyear = eval(year) / 100;
        if (year.length != 2 && year.length != 4) {
            return false;
        }
        if (month > 12 || month < 1) {
            return false;
        }
        switch (eval(month)) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                if (day > 31 || day < 1) {
                    return false;
                }
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                if (day > 30 || day < 1) {
                    return false;
                }
                break;
            case 2:
                var newday = (year % 4);
                if (newday == 0)
                    newday = 29;
                else
                    newday = 28
                if (day > newday || day < 1) {
                    return false;
                }

        }
        return true;
    }
    catch (exception) {
        return false;
    }
}

function fcisNumberForId(z) {
    str = z.value;
    str = fcremoveChar(str, ",");
    if (fcisEmptyM(str))
        return false;
    while (str != "0" && str.charAt(0) == "0") {
        if (str.length > 1)
            str = str.substring(1);
    }
    if (isNaN(str) == true || str == '') {
        return false;
    }
    z.value = fcsetNo(str);
    return true;
}

function fcremoveChar(str, r) {
    //return str;
    while (str.indexOf(r) != -1)
        str = str.replace(r, "");
    return str;
}

function fcisEmptyM(z) {
    org = z;
    if (org == null)
        return true;
    counter = 0, lastIndex = 0, startIndex = 0, tmp = "";

    while (counter != -1) {
        counter = org.indexOf(" ", lastIndex);
        if (counter != -1) {
            tmp += org.substring(startIndex, counter);
            lastIndex = counter + 1;
            startIndex = lastIndex;
        }
        else {
            tmp += org.substring(startIndex, org.length);
        }
    }
    if (tmp == "")
        return true;
    else
        return false;
}

function fcsetNo(str) {
    return str;
    var ostr = "";
    if (str.indexOf(".") != -1) {
        ostr = str.substring(str.indexOf("."));
        str = str.substring(0, str.indexOf("."));
    }
    var slen = 3;
    var nstr = "";
    var elen = str.length - slen;
    while (elen > 0) {
        nstr = "," + str.substring(elen, elen + 3) + nstr;
        slen += 3;
        elen = str.length - slen;
    }
    nstr = str.substring(0, elen + 3) + nstr;
    nstr += (ostr.length > 0 ? ostr : "");
    return nstr;
}

function fcisEmptyForId(z) {
    org = z.value;
    if (org == null)
        return true;
    counter = 0, lastIndex = 0, startIndex = 0, tmp = "";
    while (counter != -1) {
        counter = org.indexOf(" ", lastIndex);
        if (counter != -1) {
            tmp += org.substring(startIndex, counter);
            lastIndex = counter + 1;
            startIndex = lastIndex;
        }
        else {
            tmp += org.substring(startIndex, org.length);
        }
    }
    if (tmp == "")
        return true;
    else
        return false;
}

function getTabularFieldByRowPeer(obj, fieldName) {
    index = obj.id;
    index = index.substring(3);
    index = parseInt(index) - 2; // row starts from 2 and fieldId starts from 0
    return getTabularFieldByIndex(index + 1, fieldName);
}

function getTabularFieldByPeer(obj, fieldName) {
    
    
    var isNewRow = false;
    if (debug) alert("1 getTabularFieldByPeer " + obj.id + " " + fieldName);
    var index;
    str = obj.id;

    if (str.indexOf('row') < 0) {
        //ind = str.lastIndexOf('_') + 1;
        ind = parseInt(str.indexOf('_')) + 4;
        var temp = str.substring(ind, ind + 3);
        
        if (temp == "New") {
            isNewRow = true;
            index = 0;
        }
        else {

            ind = str.lastIndexOf('_') + 1;
            index = str.substring(ind);
        }

        if (debug) alert("2 getTabularFieldByPeer " + obj + " " + fieldName + " " + index);
    }
    else {

        ind = str.indexOf('row');
        if (ind >= 0) {
            index = str.substring(3) - 2; // row starts from 2 and fieldId starts from 0
            
            if (debug) alert("3 getTabularFieldByPeer " + obj + " " + fieldName + " " + index);
        }
    }
    if (index < 0) {
        alert('An error in getTabularFieldbyPeer');
    }

    if (debug) alert("4 return getTabularFieldByPeer " + obj + " " + fieldName + " " + index);

    if (isNewRow)
        return getTabularFieldByIndexNewRow(fieldName);
    else
        return getTabularFieldByIndex(parseInt(index) + 1, fieldName);
}

//New method by Rehan
function getTabularRowIndexByPeer(obj, fieldName) {
    var isNewRow = false;
    if (debug) alert("1 getTabularFieldByPeer " + obj.id + " " + fieldName);
    var index;
    str = obj.id;

    if (str.indexOf('row') < 0) {

        ind = parseInt(str.indexOf('_')) + 4;
        var temp = str.substring(ind, ind + 3);

        if (temp == "New") {
            isNewRow = true;
            index = 0;
        }
        else {
            ind = str.lastIndexOf('_') + 1;
            index = str.substring(ind);
        }
        if (debug) alert("2 getTabularFieldByPeer " + obj + " " + fieldName + " " + index);
    }
    else {

        ind = str.indexOf('row');
        if (ind >= 0) {
            index = str.substring(3) - 2; // row starts from 2 and fieldId starts from 0
            if (debug) alert("3 getTabularFieldByPeer " + obj + " " + fieldName + " " + index);
        }
    }
    if (index < 0) {
        alert('An error in getTabularFieldbyPeer');
    }

    if (debug) alert("4 return getTabularFieldByPeer " + obj + " " + fieldName + " " + index);
    return index + 1;
}



function getTabularIndexByField(obj) {
    id = obj.id;
    rowNo = id.substring(id.indexOf('_chkDelete_') + 11);
    rowNo = parseInt(rowNo);
    return rowNo + 1;
}

function getTabularFieldByIndexNewRow(fieldName) {


    obj = checkAndGetTextBoxObject(0, 'New', fieldName);

    if (obj == null)
        obj = checkAndGetComboBoxObject(0, 'New', fieldName);

    if (obj == null)
        obj = checkAndGetLabelObject(0, 'New', fieldName);

    if (obj == null) {
        obj = checkAndGetCheckBoxObject(0, 'New', fieldName);
    }

    return obj;
}

function getTabularFieldByIndex(index, fieldName)
{
    //alert('index :' + index);
    //alert('fieldName :' + fieldName);
   /*
    index--;

//        if (totalRecords  == (index)) {
//            obj = checkAndGetTextBoxObject(index, 'new', fieldName);

//            if (obj == null)
//                obj = checkAndGetComboBoxObject(index, 'new', fieldName);

//            if (obj == null)
//                obj = checkAndGetLabelObject(index, 'new', fieldName);

//            if (obj == null) {
//                obj = checkAndGetCheckBoxObject(index, '', fieldName);
//            }
//        }

    if (totalRecords - 1 == (index)) {
        getTabularFieldByIndexNewRow(fieldName);
        }
        else {

    if (debug) alert('getTabularFieldbyIndex, index:' + index + ' fieldName: ' + fieldName);

    obj = checkAndGetTextBoxObject(index, '', fieldName);

    if (obj == null)
        obj = checkAndGetComboBoxObject(index, '', fieldName);

    if (obj == null)
        obj = checkAndGetLabelObject(index, '', fieldName);

    if (obj == null)
        obj = checkAndGetCheckBoxObject(index, '', fieldName);
}


    return obj;
    */
    index++;
    if(totalRecords==(index-1))
    {       
        obj = checkAndGetTextBoxObject(index,'new',fieldName);
			
        if(obj==null)
            obj = checkAndGetComboBoxObject(index,'new',fieldName);
                        
        if(obj==null)
            obj = checkAndGetLabelObject(index,'new',fieldName);
			
        if(obj==null)
        {
            obj = checkAndGetCheckBoxObject(index,'',fieldName);
				
        }
				
    }
    else
    {       
        if (debug) alert('getTabularFieldbyIndex, index:' + index + ' fieldName: ' + fieldName);
        obj = checkAndGetTextBoxObject(index,'',fieldName);
			
        if(obj==null)
            obj = checkAndGetComboBoxObject(index,'',fieldName);
			
        if(obj==null)
            obj = checkAndGetLabelObject(index,'',fieldName);
			
        if (obj==null)	
            obj = checkAndGetCheckBoxObject(index,'',fieldName);
    }
		
    return obj;
}

function checkAndGetTextBoxObject(index, rowType, fieldName) {

    //alert('index : ' + index);
    //alert('rowType : ' + rowType);
    if (rowType == 'New')
    {
        preFix = 'txtNew';
        return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName);
    }
    else
    {
        preFix = 'txt';
        //alert(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName + '_' + index);
        //EntryGrid_ctl02_txtSAV_AUVCVALUECOMB
        //return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
        if (index <= 9)
        {
            //alert(1);
            //alert(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
        }
        else
        {
            //alert(2)
            //alert(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
        }

    }

}

/*	function internally used	*/
function checkAndGetComboBoxObject(index, rowType, fieldName) {

    if (rowType == 'New')
    {
        preFix = 'ddlNew';
        return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName);
    }
    else
    {
        preFix = 'ddl';
        //return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName + '_' + index);
        if (index <= 9)
        {
            //alert(1);
            //alert(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
        }
        else
        {
            //alert(2)
            //alert(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
        }
    }


}

/*	function internally used	*/
function checkAndGetLabelObject(index, rowType, fieldName) {

    if (rowType == 'New') {
        preFix = 'lblNew';
        return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName);
    }
    else {
        preFix = 'lbl';
        //return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName + '_' + index);
        if (index <= 9) {
            //alert(1);
            //alert(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
        }
        else {
            //alert(2)
            //alert(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
        }
    }


}

function checkAndGetCheckBoxObject(index, rowType, fieldName)
{
    //alert(0);
    //alert('fieldName : ' + fieldName);

    if (fieldName == 'deleted')
        fieldName = "Delete";

    if (rowType == 'New') {
        preFix = 'chkNew';
        return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName);
    }
    else
    {
        //alert(1);
        preFix = 'chk';
        //return document.getElementById(ctlprefix + 'EntryGrid' + '_' + preFix + fieldName + '_' + index);
        if (index <= 9)
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl0' + index + '_' + preFix + fieldName);
        else
            return document.getElementById(ctlprefix + 'EntryGrid' + '_ctl' + index + '_' + preFix + fieldName);
    }


}

function setValuesInSession(obj, fieldNames) {

    
    if (getField("PkColumns") != null && getField("PkColumns").value.length > 0)
        fieldNames = getField("PkColumns").value;

    
    if (getField("PkColumns") != null && getField("PkColumns").value.length > 0) {
        fieldArray = fieldNames.split(",");
        fieldValues = "";
        for (i = 0; i < fieldArray.length; i++) {

            fieldValues = fieldValues + "," + getTabularFieldByPeer(obj, fieldArray[i]).value;

        }
        fieldValues = fieldValues.substring(1, fieldValues.length);
        var fieldAndValueComb = fieldNames + "~" + fieldValues;
        var result = RSExecute("RemoteService.aspx", "SetSessionValues", fieldAndValueComb);
        if (result.return_value.length > 0) { alert(result.return_value); }
    }
    else {
        
        ///"Values are not being set in Session"

    }

}


function getValueByName(name) {
    requiredObj = getField(name);
    return requiredObj.value;
}
function getField(name) {
    var prefix = ctlprefix;
    var tokens = new Array();
    tokens[0] = "txt";
    tokens[1] = "ddl";
    tokens[2] = "lbl";
    tokens[3] = "chk";
    tokens[4] = "";

    if (name == "deleted")
        name = "Delete";

    var requiredObj = null;
    outer:
    for (i = 0; i < 100; i++) {
        for (j = 0; j < tokens.length; j++) {
            requiredObj = document.getElementById(prefix + tokens[j] + name);
            if (requiredObj != null) {
                break outer;
            }
        }
    }
    return requiredObj;
}

var TABULAR = "TBL";
var STANDARD = "STD";
var STANDARD_GRID = "STDG";
var BUTTON = "BTN";
var DISPLAY_HEADER = "DH";
var ENQUIRY = "ENQ";
var GROUP = "GRP";
var OPTION = "OPT";
var TREE = "TREE";
var TAB = "TAB";

function getFileNameByEntity(entityId, layout, mod_name) {
    _prefix = "";
    if (layout == TABULAR)
        _prefix = "shgn_ts_se_tblscreen_";
    else
        if (layout == STANDARD)
            _prefix = "shgn_ss_se_stdscreen_";
        else
            if (layout == STANDARD_GRID)
                _prefix = "shgn_gs_se_stdgridscreen_";
            else
                if (layout == BUTTON)
                    _prefix = "shgn_bt_se_button_";
                else
                    if (layout == DISPLAY_HEADER)
                        _prefix = "shgn_dh_se_displayheader_";
                    else
                        if (layout == ENQUIRY)
                            _prefix = "shgn_dh_se_enquiry_";
                        else
                            if (layout == GROUP)
                                _prefix = "shgn_gp_gp_";
                            else
                                if (layout == OPTION)
                                    _prefix = "shgn_gp_gp_";
                                else
                                    if (layout == TREE)
                                        _prefix = "shgn_op_se_";
                                    else
                                        if (layout == TAB)
                                            _prefix = "shgn_gp_gp_";
    if (mod_name == undefined && mod_name == null) {
        _fileName = _prefix + entityId + ".aspx";
    }
    else {
        _fileName = "../../" + mod_name + "/Presentation/" + _prefix + entityId + ".aspx";

    }
    return _fileName;
}
function getURLByEntity(entityId, layout) {
    url = getFileNameByEntity(entityId, layout) + ".aspx";
    return url;
}

//============================= To Print Error Message Filter ===================================
function ErrorMessage(errMsg) {
    var shortMessage = new String();
    var longMessage = new String();
    longMessage = shortMessage = errMsg;
    if (longMessage.indexOf("<ErrorMessage>", 0) != -1) {
        longMessage = longMessage.replace("<ErrorMessage>", "Message:");
        longMessage = longMessage.replace("<ErrorMessageDetail>", "\n\nDetail:");
        shortMessage = shortMessage.substring(("<ErrorMessage>").length, shortMessage.indexOf("<ErrorMessageDetail>", 0)) + "\n Dont Show Detail?";
        confirm(shortMessage) == false ? alert(longMessage) : "";
    }
    else
        alert(errMsg);
}


function setDisableDatePopUp(_popUpDateName, _blnDisable) {
    var _objpopUpDate = getField(_popUpDateName);
    if (_objpopUpDate != null) {
        _objpopUpDate.readOnly = _blnDisable;
        document.getElementById(_objpopUpDate.name + "_outer").disabled = _blnDisable;
    }
}

function IsProcessAllowed(processName) {
    processAllowed = false;

    for (i = 0; i < AllowedProcess.length; i++) {
        if (AllowedProcess[i] == processName) {
            processAllowed = true;
        }
    }
    return processAllowed;
}

// Added by Naseer 26/01/2005

function checkDuplicateLevel1(srcFieldName1, requiredFieldName, message) {
    duplicateFound = false;
    for (j = 1; j <= totalRecords; j++) {
        srcFieldValue1 = getTabularFieldByIndex(j, srcFieldName1).value;

        for (wi = j + 1; wi <= totalRecords; wi++) {
            requiredFieldValue = getTabularFieldByIndex(wi, requiredFieldName).value;
            if (requiredFieldValue.length = 0) continue;
            srcFieldValue11 = getTabularFieldByIndex(wi, srcFieldName1).value;
            if (srcFieldValue1 === srcFieldValue11) {
                alert(message);
                getTabularFieldByIndex(wi, srcFieldName1).focus();
                duplicateFound = true;
                break;
            }
        }
    }
    return duplicateFound;
}

/*
function getTabularFieldByRowPeer(obj,fieldName)
{
//if (debug) alert ('in getTabularFieldByPeer');
str = obj.id;
ind = str.indexOf('_ctl')+4;
index = str.substring(ind,str.indexOf('_',ind));
alert(index+"  "+ind);
return getTabularFieldByIndex(ind,fieldName);
}
*/
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

function fccheckDecimal(v, nd, d, n) {
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

function SetStatus(rowStatus) {

    if (getField("ModifiedRows").value.indexOf(rowStatus) == -1)
        getField("ModifiedRows").value += rowStatus + ',';

    //alert(getField("ModifiedRows").value);
}

/********************** FUNCTIONS FOR CHECKING NUMBERS**************/

function checkLessThanZero(obj, str_msg) {
    if (eval(obj.value < 0)) {
        alert(str_msg);
        obj.focus();
    }
}


function checkLessThanOne(obj, str_msg) {
    if (eval(obj.value < 1)) {
        alert(str_msg);
        obj.focus();
    }
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
// For Applying Currncy format
function getRoundValue(value, precision) {

    var num = new NumberFormat();
    if (precision == "")
        precision = 2;
    num.setNumber(value);

    num.setPlaces(precision);
    return num.toUnformatted();
}

function applyCurrencyFormat(obj, precision) {
    if (trim(obj.value) == '') {
        return;
    }

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;
    num.setNumber(obj.value);
    num.setPlaces(precision);

    num.setCommas(true);

    obj.value = num.toFormatted();
}

function applyNumberFormat(obj, precision) {
    if (trim(obj.value) == "")
        return;

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;
    num.setNumber(obj.value);
    num.setPlaces(precision);

    num.setCommas(true);

    obj.value = num.toFormatted();
}

function deformatCurrency(obj, precision) {
    if (trim(obj.value) == "")
        return;

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;

    num.setNumber(obj.value);
    num.setPlaces(precision);
    num.setCommas(false);
    obj.value = num.toFormatted();
}

function getDeformattedCurrency(obj, precision) {
    if (trim(obj.value) == "")
        return "";

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;

    num.setNumber(obj.value);
    num.setPlaces(precision);
    num.setCommas(false);
    var rtrnString = num.toFormatted();
    return rtrnString;
}


function getDeformattedNumber(obj, precision) {
    if (trim(obj.value) == "")
        return "";

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;

    num.setNumber(obj.value);
    num.setPlaces(precision);
    num.setCommas(false);
    var rtrnString = num.toFormatted();
    return rtrnString
}

function deformatNumber(obj, precision) {
    if (trim(obj.value) == "")
        return;

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;

    num.setNumber(obj.value);
    num.setPlaces(precision);
    num.setCommas(false);
    obj.value = num.toFormatted();
}

function getCurrencyFormatedValue(Value, precision) {
    var num = new NumberFormat();
    if (precision == "")
        precision = 0;
    num.setNumber(Value);
    num.setPlaces(precision);

    num.setCommas(true);

    return num.toFormatted();
}

function getNumberFormatedValue(Value, precision) {

    var num = new NumberFormat();
    if (precision == "")
        precision = 0;
    num.setNumber(Value);
    num.setPlaces(precision);

    num.setCommas(true);

    return num.toFormatted();
}


function trim(s) {
    while (s.substring(0, 1) == ' ') {
        s = s.substring(1, s.length);
    }
    while (s.substring(s.length - 1, s.length) == ' ') {
        s = s.substring(0, s.length - 1);
    }
    return s;
}

function deleteChar(strVal, charDelete) {
    if (strVal != '') {
        while (strVal.indexOf(',', 0) > -1) {
            strVal = strVal.replace(charDelete, '');
        }
    }
    return strVal;
}

function getFieldValue(colName) {

    var returnValue = "";

    objField = getField(colName);

    if (objField != null) {
        if (objField.tagName == "TABLE") {
            returnValue = getRbtVal(colName);
        }
        else {
            returnValue = objField.value;
        }

        if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
            returnValue = getDeformattedCurrency(objField, objField.getAttribute("Precision"));

        if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
            returnValue = getDeformattedNumber(objField, objField.getAttribute("Precision"));

    }
    else {
        alert("JS-Error : Invalid Field Name in getFieldValue() " + colName);
    }

    return returnValue;
}

function setFieldValue(colName, value) {
    objField = getField(colName);

    // Start Added By Rehan 
    returnValue = value;

    if (objField != null) {


        if (objField.tagName == "TABLE") {
            setRbtVal(colName, returnValue);
        }
        else {
            objField.value = returnValue;
        }
        // End Added By Rehan 

        /* Code Replaced by Rehan 
        if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        returnValue = getDeformattedCurrency(objField, objField.getAttribute("Precision"));
        if(objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        returnValue = getDeformattedNumber(objField, objField.getAttribute("Precision"));
        objField.value = 	returnValue;
        */

        if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
            applyCurrencyFormat(objField, objField.getAttribute("Precision"));
        if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
            applyNumberFormat(objField, objField.getAttribute("Precision"));
    }
    else {
        alert("setFieldValue() :Not able to set value for " + colName);
    }
}

function setTotalFieldValue(colName, precision) {

    var num = new NumberFormat();
    if (precision == "")
        precision = 2;
    num.setNumber(document.getElementById(colName).innerHTML);

    num.setPlaces(precision);
    num.setCommas(true);

    document.getElementById(colName).innerHTML = num.toFormatted();
}

function getTotalFieldValue(colName) {
    var returnValue;
    returnValue = deleteChar(document.getElementById(colName).innerHTML, ",");
    return returnValue;
}


function getFieldValueByRef(objField) {
    var returnValue;
    returnValue = objField.value;
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        returnValue = getDeformattedCurrency(objField, objField.getAttribute("Precision"));
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        returnValue = getDeformattedNumber(objField, objField.getAttribute("Precision"));
    return returnValue;
}

function setFieldValueByRef(objField, value) {
    objField.value = value;
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        applyCurrencyFormat(objField, objField.getAttribute("Precision"));
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        applyNumberFormat(objField, objField.getAttribute("Precision"));
}


function getTabularFieldValue(rowIndex, colName) {
    var returnValue;
    objField = getTabularFieldByIndex(rowIndex, colName);
    returnValue = objField.value;
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        returnValue = getDeformattedCurrency(objField, objField.getAttribute("Precision"));
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        returnValue = getDeformattedNumber(objField, objField.getAttribute("Precision"));
    return returnValue;
}

function setTabularFieldValue(rowIndex, colName, value) {
    objField = getTabularFieldByIndex(rowIndex, colName);
    objField.value = value;
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "Currency")
        applyCurrencyFormat(objField, objField.getAttribute("Precision"));
    if (objField.getAttribute("BaseType") == "Number" && objField.getAttribute("SubType") == "FormattedNumber")
        applyNumberFormat(objField, objField.getAttribute("Precision"));
}

// For Applying Currncy format

function checkNo(obj, scale, precision) {
    var strWholeNumber = new String();
    strWholeNumber = getFieldValueByRef(obj);
    var precisionNumber = new String();
    if (strWholeNumber.indexOf('.') >= 0) {
        precisionNumber = strWholeNumber.substring(strWholeNumber.indexOf('.') + 1, strWholeNumber.length);
        strWholeNumber = strWholeNumber.substring(0, strWholeNumber.indexOf('.'));
    }
    if (strWholeNumber.length > scale - precision) {
        alert("The Number is out of Range");
        obj.focus();
        return false;
    }
    if (precisionNumber.length > precision) {
        alert("The Precision is out of Range");
        obj.focus();
        return false; //alert("precisionNumber is out of range");
    }

    return true;
}

function OpenInNewWindow(entityId, layout, queryStringParameter, fullscreen, top, left, height, width, location, menubar, resizable, scrollbars, status, titlebar, toolbar, mod_name) {
    features = 'channelmode=no';
    features = features + " ,directories=no";
    if (fullscreen != null && fullscreen != 'null' && (fullscreen == true || fullscreen == 'true')) {
        features = features + " ,fullscreen =yes";
    }
    else {
        features = features + " ,top=" + top;
        features = features + " ,left=" + left;
        features = features + " ,height=" + height;
        features = features + " ,width=" + width;
    }
    if (location != null && location != 'null' && location == true)
        features = features + " ,location = yes";
    else
        features = features + " ,location = no";
    if (menubar != null && menubar != 'null' && menubar == true)
        features = features + " ,menubar =  yes";
    else
        features = features + " ,menubar = no";
    if (resizable != null && resizable != 'null' && resizable == true)
        features = features + " ,resizable = yes";
    else
        features = features + " ,resizable = no";
    if (scrollbars != null && scrollbars != 'null' && scrollbars == true)
        features = features + " ,scrollbars =  yes";
    else
        features = features + " ,scrollbars = no";
    if (status != null && status != 'null' && status == true)
        features = features + " ,status =  yes";
    else
        features = features + " ,status = no";
    if (titlebar != null && titlebar != 'null' && titlebar == true)
        features = features + " ,titlebar =  yes";
    else
        features = features + " ,titlebar =no";
    if (toolbar != null && toolbar != 'null' && toolbar == true)
        features = features + " ,toolbar =  yes";
    else
        features = features + " ,toolbar = no";
    windowURL = getFileNameByEntity(entityId, layout, mod_name);
    //alert(windowURL);

    if (queryStringParameter != null && queryStringParameter != 'null' && queryStringParameter.length > 0)
        windowURL = windowURL + "?" + queryStringParameter;

    window.open(windowURL, null, features);

}



function fcStandardFooterFunctionsCall() {
    
    autoTab();
    initmb();
    blockDoubleSubmit(false);
}
//function checkTabKey() {if (event.keyCode==9 && !event.shiftKey) {event.cancelBubble=true;return false;}}
//move The Forcus to the next tab @ no of character entered = max length begin
//for(j = i; (j = (j + 1) % l) != i && (!f[j].type || f[j].disabled || f[j].readOnly) || (f[j].type == "hidden"););
autoTab = function () {
    function next(e) {
        var theKeyCode = e.keyCode;
        var shiftStatus = event.shiftKey;
        if ("form" in e.target && !event.shiftKey) {
            var i, j = e.key, f = (e = e.target).form.elements, l = e.value.length;
            /*if(theKeyCode==9 && !event.shiftKey){
            for(i = l = f.length; f[--i] != e;);
            for(j = i; (j = (j + 1) % l) != i && (!f[j].type || f[j].disabled || f[j].readOnly) || (f[j].type == "hidden"););
            j != i && f[j].focus();
            }
            else */
            //alert('e : ' + e.value);
            //alert('e.value.length : ' + e.value.length);
            //alert('e.maxLength : ' + e.maxLength);
            //alert('l : ' + l)
            //alert('(Math.abs(e.maxLength)) : ' + (Math.abs(e.maxLength)));
            if ((Math.abs(e.maxLength)) != 1 && l >= (Math.abs(e.maxLength)) && theKeyCode != 9 && theKeyCode != 16 && !event.shiftKey)
            {
                for (i = l = f.length; f[--i] != e;);
                for (j = i; (j = (j + 1) % l) != i && (!f[j].type || f[j].disabled) || (f[j].type == "hidden"); );
                j != i && f[j].tabIndex > 0 && f[j].focus();
            }
        }
    }
    for (var f, i = (f = document.forms).length; i; addEvent(f[--i], "keyup", next));
    //for(var f, i = (f = document.forms).length; i; addEvent(f[--i], "keydown", checkTabKey));
};



addEvent = function (o, e, f) {
    var r = o[r = "_" + (e = "on" + e)] = o[r] || (o[e] ? [[o[e], o]] : []), a, c, d;
    r[r.length] = [f || o], o[e] = function (e) {
        try {
            (e = e || event).preventDefault || (e.preventDefault = function () { e.returnValue = false; });
            e.stopPropagation || (e.stopPropagation = function () { e.cancelBubble = true; });
            e.target || (e.target = e.srcElement || null);
            e.key = (e.which + 1 || e.keyCode + 1) - 1 || 0;
        } catch (f) { }
        for (d = 1, f = r.length; f; r[--f] && (a = r[f][0], o = r[f][1], a.call ? c = a.call(o, e) : (o._ = a, c = o._(e), o._ = null), d &= c !== false));
        return e = null, !!d;
    }
};


removeEvent = function (o, e, f, s) {
    for (var i = (e = o["_on" + e] || []).length; i; )
        if (e[--i] && e[i][0] == f && (s || o) == e[i][1])
            return delete e[i];
    return false;
};
function openInSameWindow(entityId, layout, queryStringParameter) {
    windowURL = top.window.location.href;
    //alert(windowURL);
    if (windowURL.lastIndexOf('/') != -1) {
        /*firstpos=location.href.lastIndexOf('/')+1;lastpos=location.href.length;Namer=location.href.substring(firstpos,lastpos);alert('My name is "'+Namer+'"');*/
        pos = windowURL.lastIndexOf('/') + 1;
        parentDir = windowURL.substring(0, pos);
        //alert(parentDir);
        entityId = getFileNameByEntity(entityId, layout);
        //alert(' dir Name >>>>>>>'+parentDir+' file Name >>>>>>>>'+entityId);
        if (queryStringParameter != null && queryStringParameter != 'null' && queryStringParameter.length > 0)
            entityId = entityId + "?" + queryStringParameter;
        top.window.location = parentDir + entityId;
    }
}
/*function setTabularRowInSession(obj)
{
	
	
fieldNames=document.myForm.FieldCombinationForLog.value;//POR_ORGACODE~PLC_LOCACODE
//alert(fieldNames);
fieldArray=fieldNames.split("~");
//alert("replaces >>"+fieldNames);
var str_QS="../shab/shgn_sv_se_setvaluesinsession.jsp?";
	
for(i=0;i<fieldArray.length;i++)
{
str_QS+="r_"+fieldArray[i]+"="+getTabularFieldByPeer(obj,fieldArray[i]).value+"&";
alert(str_QS);
}
str_QS=str_QS.substring(0,str_QS.length-1);
document.frm_ComboForm.action=str_QS;
document.frm_ComboForm.target="RemoteComboIFrame"+(int_CurrentIFrame);
int_CurrentIFrame++;
if (int_CurrentIFrame>8)
int_CurrentIFrame=1;
document.frm_ComboForm.submit();
}*/

//move The Forcus to the next tab @ no of character entered = max length End 



// Fetch Data Array functions

function fetchDataArray(fetchArrayQry) {
    var result = RSExecute("RemoteService.aspx", "FetchDataArray", fetchArrayQry);
    return result.return_value;
}

function fetchRecordCount(fetchQuery, fromDate, toDate) {
   
    var result = RSExecute("RemoteService.aspx", "FetchRecordCount", fetchQuery, fromDate, toDate);
    //return result.return_value;
    return result.return_value;
}

function fetchArray_getRowCount(fetchArray) {

    return fetchArray.length - 1;
}

function fetchAarry_getCellValue(fetchArray, index, columnName) {
    index++;
    metaArray = fetchArray[0];
    for (arrayCount = 0; arrayCount <= metaArray.length; arrayCount++)
        if (metaArray[arrayCount].toUpperCase() == columnName.toUpperCase())
            break;
    dataArray = fetchArray[index];

    return dataArray[arrayCount];
}

// END Fetch Data Array functions
function showPicture(imagePath, height, width, location, menubar, resizable, scrollbars, status, titlebar, toolbar) {
    //alert(1);
    //entityId,fullscreen,top,left,
    entityId = '../../Presentation/shgn_ss_se_stdscreen_ED_TOOL_SHOWPICTURE.aspx';
    fullscreen = false;
    var top = 100;
    left = 100;
    height = (height + 80);
    width = width + 40;
    resizable = true;
    titlebar = false; //TODO
    scrollbars = true;
    features = 'channelmode=no';
    features = features + " ,directories=no";
    if (fullscreen != null && fullscreen != 'null' && (fullscreen == true || fullscreen == 'true')) {
        features = features + " ,fullscreen =yes";
    }
    else {
        features = features + " ,top=" + top;
        features = features + " ,left=" + left;
        features = features + " ,height=" + height;
        features = features + " ,width=" + width;
    }
    if (location != null && location != 'null' && location == true)
        features = features + " ,location = yes";
    else
        features = features + " ,location = no";
    if (menubar != null && menubar != 'null' && menubar == true)
        features = features + " ,menubar =  yes";
    else
        features = features + " ,menubar = no";
    if (resizable != null && resizable != 'null' && resizable == true)
        features = features + " ,resizable = yes";
    else
        features = features + " ,resizable = no";
    if (scrollbars != null && scrollbars != 'null' && scrollbars == true)
        features = features + " ,scrollbars =  yes";
    else
        features = features + " ,scrollbars = no";
    if (status != null && status != 'null' && status == true)
        features = features + " ,status =  yes";
    else
        features = features + " ,status = no";
    if (titlebar != null && titlebar != 'null' && titlebar == true)
        features = features + " ,titlebar =  yes";
    else
        features = features + " ,titlebar =no";
    if (toolbar != null && toolbar != 'null' && toolbar == true)
        features = features + " ,toolbar =  yes";
    else
        features = features + " ,toolbar = no";
    windowURL = entityId + "?src=" + imagePath + "&height=" + (height - 40) + "&width=" + (width - 40);
    ///alert(windowURL);
    window.open(windowURL, null, features);

}
function showThis(image, height, width, location, menubar, resizable, scrollbars, status, titlebar, toolbar) {
    showPicture(image.src, height, width, location, menubar, resizable, scrollbars, status, titlebar, toolbar);
}

/******************************Math Library****************************/

function round(rnum, rlength) {
    if (rnum > 8191 && rnum < 10485) {
        rnum = rnum - 5000;
        var newnumber = Math.round(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
        newnumber = newnumber + 5000;
    } else {
        var newnumber = Math.round(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
    }
    return newnumber;
}

/***************************End of Math Library*************************/

/****************************** Batch Process - Start ****************************/
function getBatchProcInfo(where) {
    var result = RSExecute("RemoteService.aspx", "fetchBatchInfo", where);
    return result.return_value;
}

function executeBatchProcess(proccessName, processDesc) {
    if (!beforeExecuteProcess())
        return;
    if (processDesc == null || processDesc == "")
        processDesc = proccessName;
    //if (_lastEvent == 'Edit' || _lastEvent == 'View'){	
    myForm._CustomEventVal.value = "Process";
    myForm._CustomArgName.value = "BatchProcessName";
    myForm._CustomArgVal.value = proccessName + "~" + processDesc;
    myForm._CustomEvent.onclick();
    //}		
}
/****************************** Batch Process - End ****************************/

/****************************** Client Side Class Calling ****************************/
function executeClass(classNameWithParameter) {
    var result = RSExecute("RemoteService.aspx", "executeClass", classNameWithParameter);
    return result.return_value;
}
/**************************************************************************************/

/****************************** Form Double Submit Blocking using Progress Dialog ****************************/
function blockDoubleSubmit(bvalue) {
    var imgs, i;
    fLen = parent.frames.length - 1;
    //imgs = parent.frames[fLen].document.getElementsByTagName('img');
    imgs = parent.frames[fLen].document.getElementsByTagName('button');
    for (i = 0; i < imgs.length; i++) {
        if (imgs[i].name != "" && imgs[i].name.indexOf("Menu") == -1) {
            imgs[i].disabled = bvalue;
        }
    }

    if (bvalue == true) {
        var w = (screen.availWidth / 4);
        var h = (screen.availHeight / 10);
        sm('Request is in progress, please wait...', w, h);
    }
}

function pageWidth() {
    return window.innerWidth != null ? window.innerWidth : document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body != null ? document.body.clientWidth : null;
}

function pageHeight() {
    return window.innerHeight != null ? window.innerHeight : document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body != null ? document.body.clientHeight : null;
}

function posLeft() {
    return typeof window.pageXOffset != 'undefined' ? window.pageXOffset : document.documentElement && document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft ? document.body.scrollLeft : 0;
}

function posTop() {
    return typeof window.pageYOffset != 'undefined' ? window.pageYOffset : document.documentElement && document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop ? document.body.scrollTop : 0;
}

function $(x) {
    return document.getElementById(x);
}

function scrollFix() {
    var obol = $('ol');
    obol.style.top = posTop() + 'px';
    obol.style.left = posLeft() + 'px'
}

function sizeFix() {
    var obol = $('ol');
    obol.style.height = pageHeight() + 'px';
    obol.style.width = pageWidth() + 'px';
}

function kp(e) {
    ky = e ? e.which : event.keyCode;
    if (ky == 88 || ky == 120)
        hm();
    return false
}

function inf(h) {
    tag = document.getElementsByTagName('select');
    for (i = tag.length - 1; i >= 0; i--)
        tag[i].style.visibility = h;
    tag = document.getElementsByTagName('iframe');
    for (i = tag.length - 1; i >= 0; i--)
        tag[i].style.visibility = h;
    tag = document.getElementsByTagName('object');
    for (i = tag.length - 1; i >= 0; i--)
        tag[i].style.visibility = h;
}

function sm(obl, wd, ht) {
    var h = 'hidden';
    var b = 'block';
    var p = 'px';
    var obol = $('ol');
    var obbxd = $('mbd');
    if (obbxd != null) {
        obbxd.innerHTML = obl;
    }
    if (obol != null) {
        obol.style.height = pageHeight() + p;
        obol.style.width = pageWidth() + p;
        obol.style.top = posTop() + p;
        obol.style.left = posLeft() + p;
        obol.style.display = b;
    }
    var tp = posTop() + ((pageHeight() - ht) / 2) - 12;
    var lt = posLeft() + ((pageWidth() - wd) / 2) - 12;
    var obbx = $('mbox');
    if (obbx != null) {
        obbx.style.top = (tp < 0 ? 0 : tp) + p;
        obbx.style.left = (lt < 0 ? 0 : lt) + p;
        obbx.style.width = wd + p;
        obbx.style.height = ht + p;
        inf(h);
        obbx.style.display = b;
    }
    return false;
}

function hm() {
    var v = 'visible';
    var n = 'none';
    $('ol').style.display = n;
    $('mbox').style.display = n;
    inf(v);
    document.onkeypress = ''
}

function initmb() {
    var ab = 'absolute';
    var n = 'none';
    var obody = document.getElementsByTagName('body')[0];
    var frag = document.createDocumentFragment();
    var obol = document.createElement('div');
    obol.setAttribute('id', 'ol');
    obol.style.display = n;
    obol.style.position = ab;
    obol.style.top = 0;
    obol.style.left = 0;
    obol.style.zIndex = 998;
    obol.style.width = '100%';
    frag.appendChild(obol);
    var obbx = document.createElement('div');
    obbx.setAttribute('id', 'mbox');
    obbx.style.color = '#187A0E';
    obbx.style.backgroundColor = 'white';
    obbx.style.padding = '1.5em';

    obbx.style.border = 'dashed 1px red';
    obbx.style.textAlign = 'center';
    obbx.style.fontWeight = 'bold';
    //obbx.style.fontFamily = 'Bliss 2 Regular';
    obbx.style.fontFamily = 'Verdana';
    obbx.style.fontSize = '11pt';

    obbx.style.display = n;
    obbx.style.position = ab;
    obbx.style.zIndex = 999;

    var img = document.createElement('img');
    img.setAttribute('src', '../../Presentation/Images/progressBar/3.gif');
    img.style.height = '50px';
    img.style.width = '50px';
    obbx.appendChild(img);

    var obl = document.createElement('span');
    obbx.appendChild(obl);
    var obbxd = document.createElement('div');
    obbxd.setAttribute('id', 'mbd');
    obl.appendChild(obbxd);
    frag.insertBefore(obbx, obol.nextSibling);
    obody.insertBefore(frag, obody.firstChild);
    window.onscroll = scrollFix;
    window.onresize = sizeFix;
}


/**************************************************************************************/

/****************************** File Uploading ****************************/

function goUpload(obj) {
    
    var filePath = null;
    var fileName = null;
    var fileMode = null;
    var fileExtn = null;
    var archPath = "";

    var w = 480, h = 340;
    if (document.layers) {
        w = window.innerWidth;
        h = window.innerHeight;
        x = window.screenX;
        y = window.screenY;
    }
    else {
        /* the following is only available after onLoad */
        w = document.body.clientWidth;
        h = document.body.clientHeight;
        x = window.screenTop || window.screenY;
        y = window.screenLeft || window.screenX;
    }

    var popW = 300, popH = 200;
    var leftPos = ((w - popW) / 2) + y, topPos = ((h - popH) / 2) + x;

    // for dynamic file path
    if (getField("FILEPATH") == null) {
        filePath = "../../FilesFolder/ScannedImages";
    }
    else {
        filePath = getField("FILEPATH").value;
    }

    // new code for archiving path
    //alert(getField("ARCHIVEPATH"));
    if (getField("ARCHIVEPATH") != null) {
        archPath = getField("ARCHIVEPATH").value;
    }

    // for dynamic file name
    if (getField("FILENAME") == null) {
        fileName = '';
    }
    else {
        fileName = getField("FILENAME").value;
    }

    if (fileName.length <= 0) {
        fileName = "";
    }
    // for assigning extension
    if (getField("FILEEXT") == null) {
        fileExtn = "default.jpg";
    }
    else if (fileName.length > 0 && getField("FILEEXT").value == "X") {
        fileExtn = ".xls";
    }
    else if (fileName.length > 0 && getField("FILEEXT").value == "I") {
        fileExtn = ".jpg";
    }
    // alert((fileName.length > 0) +"..." +(getField("FILEEXT").value == "I"));
    // alert("filename:" + fileName + ", filepath:" + filePath + ", fileextn:" + fileExtn);

    var url = "initUpload.aspx?";
    url = url + "saf=" + obj.getAttribute("showSaveAs");
    url = url + "&cwf=" + obj.getAttribute("autoClose");
    url = url + "&sil=" + replaceAllOcr(filePath, "'", "");
    url = url + "&ail=" + replaceAllOcr(archPath, "'", "");
    // url = url + "&sil=" + obj.getAttribute("saveToFolder");
    // alert(getField("LOCACODE_FROM").value);
    url = url + "&sfp=" + obj.getAttribute("showFilePath");
    url = url + "&dfa=" + obj.getAttribute("duplicateFileAction");
    url = url + "&mgf=" + obj.getAttribute("mappingField");
    url = url + "&ext=" + obj.getAttribute("extension12");
    // url = url + "&nfn=" + "raheel.jpg";
    // alert((fileName != null) + "..." + (fileName.length > 0));
    if (fileName != null && fileName.length > 0) {
        url = url + "&nfn=" + fileName + fileExtn; // getField("MBM_SCANIMAGENAME").value+".jpg";
    }
    else {
        url = url + "&nfn="
    }
    url = url + "&lid=" + obj.id;
    url = url.replace(/\\/g, "\\\\");


    ///var result = RSExecute("", "doCrypto", url);
    ///url = "initUpload.aspx?" + result;
    // alert(url);
    window.open(url, 'FileUpload', 'toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=' + (screen.availWidth / 2.5) + ',height=' + (screen.availHeight / 3.2) + ',left=' + leftPos + ',top=' + topPos);
}


function replaceAllOcr(strText, matchVal, repVal) {
    var strReplaceAll = strText;
    var intIndexOfMatch = strReplaceAll.indexOf(matchVal);


    // Loop over the string value replacing out each matching
    // substring.
    while (intIndexOfMatch != -1) {
        // Relace out the current instance.
        strReplaceAll = strReplaceAll.replace(matchVal, repVal)


        // Get the index of any next matching substring.
        intIndexOfMatch = strReplaceAll.indexOf(matchVal);
    }


    return strReplaceAll;
}


/**************************************************************************************/

var entityAlertsObject = new Object();
function BuildObjectFromArray() {
    if (typeof (entityAlertsArray) != 'undefined') {
        for (var i = 0; i < entityAlertsArray.length; i++) {
            var theName = entityAlertsArray[i][0];
            entityAlertsObject[theName] = entityAlertsArray[i][1];
        }
    }
}

function getAlert(rid) {

    return entityAlertsObject[rid];
}

function getAlert(rid, phValues) {

    return getAlertMessage(entityAlertsObject[rid], phValues);
}

function getAlertMessage(alertMessage, phValues) {
    var alertMessage = ProcessTemplatedAlert(alertMessage, phValues);
    return alertMessage;
}

function ProcessTemplatedAlert(dbAlert, phValues) {
    var newAlert = dbAlert;

    // Replacing place-holders with passed values in sequential order 
    if (phValues != null) {

        var valueArray = new Array();
        valueArray = phValues.split('~');
        //alert(valueArray.length);
        for (i = 0; i < valueArray.length; i++) {

            var temp = ReplaceFirst(newAlert, "~?~", valueArray[i]);
            //alert(ReplaceFirst(newAlert,"~?~",valueArray[i]));
        }
        //alert("temp "+temp);
        newAlert = temp;

    }
    // If Any, Replacing the extra place-holders with an Empty string 
    //newAlert.Replace("~?~"," ");
    return newAlert;
}

function ReplaceFirst(text, search, replace) {

    var pos = text.indexOf(search);
    if (pos < 0) {
        return text;
    }
    //alert(text.substr(0, pos) + replace + text.substr(pos + search.length));
    return text.substr(0, pos) + replace + text.substr(pos + search.length);
}

/********************************New Mehtod Added By Sarfraz Mughal For TextArea Remaining Character******************************************************/
function wordCount(obj, displayObj) {
    var remChar;
    if (obj.MaxLength == null)
        remChar = 500 - obj.value.length;
    else
        remChar = obj.MaxLength - obj.value.length;
    document.getElementById(displayObj).innerHTML = remChar + " Left";
}
/**************************************************************************************/

/**************************************************************************************/

function SelectFirstRowENQ() {
    if (document.getElementById("lister__ctl0_EnqRow") != null) {
        setEnquiryRow(document.getElementById("lister__ctl0_EnqRow"))
    }

}


/*****************************************************************************************/
function getRbtVal(rbtName) {
    if (document.getElementsByName("ddl" + rbtName)) {
        var rbtGrp = document.getElementsByName("ddl" + rbtName);
        var rbtGrpValue = '0';
        for (var i = 0; i < rbtGrp.length; i++)
            if (rbtGrp[i].checked) {
                rbtGrpValue = rbtGrp[i].value;
                break;
            }
        return rbtGrpValue;
    }
    else
        return '';
}

function setRbtVal(rbtName, newValue) {
    var rbtGrp = document.getElementsByName("ddl" + rbtName);
    for (var i = 0; i < rbtGrp.length; i++) {
        rbtGrp[i].Checked = false;
        var evalue = rbtGrp[i].value
        if (evalue == newValue) {
            rbtGrp[i].checked = true;
        }
    }
}
/*********************************************************************************************/

function fsValidateData(Str_Class, Str_Method, Str_Data) {

    var str_Param = Str_Class + "~~" + Str_Method + "~~" + Str_Data;

    var result = RSExecute("RemoteService.aspx", "fsValidateData", str_Param);
    var SingleArray = result.return_value;

    return SingleArray;
}

/**********************Get Session Value by Name***************************/
function getSessionValueByName(Name) {
    var result = RSExecute("RemoteService.aspx", "GetSessionValue", Name);
    return result.return_value;
}

function getGlobalValueByName(Name) {
    var result = RSExecute("RemoteService.aspx", "GetGlobalValue", Name);
    return result.return_value;
}

//************CURRENCY LENGTH VALIDATOR**********************
function validateFormat(o) {
    if (!o.readOnly) {
        var msg = 'Number format is incorrect. ';
        var v = o.value.replace(/,/g, '');
        if (v.indexOf('.') > -1) {
            if (v.substring(0, v.indexOf('.')).length > (o.maxLength - (parseInt(o.Precision) + 1))) {
                alert(msg);
                o.value = "";
                o.focus();
                return false;
            }
        }
        else {
            if (v.length > (o.maxLength - (parseInt(o.Precision) + 1))) {
                alert(msg);
                o.value = "";
                o.focus();
                return false;
            }
        }
    }
    return true;
}

//************TEXTAREA LENGTH VALIDATOR**********************
function chkLength(o) {
    var val = o.value;
    var len = o.maxLength
    if (val.length >= len) {
        return false; //event.returnValue = false; //o.value = val.substring(0, len);
    }
    return true;
}

function chkLengthPaste(o) {
    var val = o.value;
    var len = o.maxLength
    if (val.length + window.clipboardData.getData("Text").length > len) {
        alert('Length exceeded, Unable to paste.');
        return false; //event.returnValue = false; //o.value = val.substring(0, len);
    }
    return true
}