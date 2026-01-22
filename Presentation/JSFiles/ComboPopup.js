//var method = "ArrangeQuery";
var method = "getLOVData";

var actions = new Array("LOAD","NEXT","PREV","SEARCH","SORT","FIRST","LAST","INDEX");

function callFunction(methodName, paramString)
{
    var getHTML = RSExecute("RemoteService.aspx", methodName, paramString);
	var comboArray = getHTML.return_value;
	if(comboArray!='undefined'&& comboArray!=null && comboArray.length>1)
	{
		dispHTML = comboArray[0];
		arrInfo = comboArray[1];
		document.getElementById("txtTotalPages").value=arrInfo[0];
		document.getElementById("txtPageNo").value=arrInfo[1];
		document.getElementById("btnNext").disabled=arrInfo[2];
		document.getElementById("btnPre").disabled=arrInfo[3];
		document.getElementById("myTable").innerHTML="";
		document.getElementById("myTable").innerHTML=dispHTML[0];
	}
	else
	{
	    document.getElementById("myTable").innerHTML="Your search result nothing...";
	    document.getElementById("txtTotalPages").value=0;
	    document.getElementById("txtPageNo").value=0;
	}
}

function getQuery()
{
    var query='';
    var query = window.location.search.substring(1);
    var sQuery = query;

 //  var sQuery = unescape(query.replace(/\+/g, " "));
    var strSplit=sQuery.split('^');
//  var gQuery=strSplit[0].toString()+"^"+strSplit[1].toString()+"^"+strSplit[2].toString()+"^"+strSplit[3].toString();
    var gQuery=strSplit[0].toString()+"^"+strSplit[1].toString()+"^"+strSplit[2].toString()+"^"+strSplit[3].toString()+"^"+strSplit[4].toString();
    
    return gQuery;
}

function getSearchParam()
{
    var searchCol=getSearchCol();
    var orderBy=document.getElementById("txtOrder").value;
    var searchValue=document.getElementById("txtSearch").value;
    var pageNo=document.getElementById("txtPageNo").value;
    var param=getQuery()+"^"+pageNo+"^"+searchValue+"^"+searchCol+"^"+orderBy;
    return param;
}

function AddSearchItem()
{
    var str=getQuery();
    var strSplit=str.split('^');
    var strWidth=strSplit[2].toString();
    var strTitle=strSplit[3].toString();
    var width=strWidth.split(',');
    var title=strTitle.split(',');
    for(var i=0;i<title.length;i++)
    {
		// Handling Key_Value case
		if(width[i]>0)
		{
			var oOption = document.createElement("OPTION");
			oOption.text=title[i];
			oOption.value=i;
			var combo = document.getElementById('select1');
			combo.add(oOption);
		}
	}
	combo.selectedIndex=0;
}

function getDataOnLoad()
{
    document.title = "Bank Essential";
    document.getElementById("txtOrder").value="";
    document.getElementById("txtSearch").value="";
    var param=getSearchParam();
    param = param + "^" + actions[0];
    //alert('param : ' + param);
    callFunction(method,param);
}

function getNextPage()
{
    var param=getSearchParam();
    param=param+"^"+actions[1];
    callFunction(method,param);
}

function getPrevPage()
{
    var param=getSearchParam();
    param=param+"^"+actions[2];
    callFunction(method,param);
}

function getCurrentPage() {

    
        var pageNo = document.getElementById("txtPageNo").value;
        var totalPage = document.getElementById("txtTotalPages").value;

        var param = '';
        if (pageNo != totalPage && totalPage > 0) {

            param = getSearchParam();
            param = param + "^" + actions[7]; //For Current Page with Index
            callFunction(method, param);

        }
        else if (pageNo == totalPage) {

            param = getSearchParam();
            param = param + "^" + actions[6]; //For Last Page with Index
            callFunction(method, param);
        }
    

    
}

function getSearch()
{
    document.getElementById("txtOrder").value="";
    var param = getSearchParam();
    param=param+"^"+actions[3];
    callFunction(method,param);
}

function getSort(a) 
{
    document.getElementById("txtOrder").value=a;
    var param=getSearchParam();
    param=param+"^"+actions[4];
    callFunction(method,param);
}

function getFirstPage()
{

    var pageNo=document.getElementById("txtPageNo").value;
    if(pageNo>1)
    {
        var param=getSearchParam();
        param=param+"^"+actions[5];
        callFunction(method,param);
    }
}

function getLastPage()
{
    var pageNo=document.getElementById("txtPageNo").value;
    var totalPage=document.getElementById("txtTotalPages").value;
    if(pageNo!=totalPage && totalPage > 0)
    {
        var param=getSearchParam();
        param=param+"^"+actions[6];
        callFunction(method,param);
    }
}


function getSearchCol()
{

	var str=getQuery();
	var strSplit=str.split('^');
	var strCol=strSplit[1].toString();
	var col=strCol.split(',');
	var select = document.getElementById("select1");
	var index = select.selectedIndex;
	
	// Handling Key_Value case
	var strWidth=strSplit[2].toString();
	var width=strWidth.split(',');
	if(width[0]==0)
		index++;
		
	var colID = col[index];
	return colID;
}

/*
function getSearchQueryCol()
{
    var Query='';
    var width='';
    var desc='';
    var operator='';
    var search = location.search;
    search = search.replace(/\?/,'');
    var searchAttributes = search.split('&');
    for(var no=0;no<searchAttributes.length;no++)
    {
        var items = searchAttributes[no].split('=');
        if(items[0]=='col')
        {   Query+=items[1]+","; }
        if(items[0]=='width')
        {   width+=items[1]+","; }
 	    if(items[0]=='desc')
 	    {   desc+=items[1]+",";  }
 	    if(items[0]=='op')
 	    {   operator+=items[1]+","; }
 	}
    width=width.slice(0,width.length-1)
    desc=desc.slice(0,desc.length-1)
    operator=operator.slice(0,operator.length-1)
    var param=Query+"^"+width+"^"+desc+"^"+operator;
    return param;
}

function getColumnName()
{

    var colName='';
    var search = location.search;
    search = search.replace(/\?/,'');
    var searchAttributes = search.split('&');
    for(var no=0;no<searchAttributes.length;no++)
    {
        var items = searchAttributes[no].split('=');
        if(items[0]=='col')
        {
            colName+=items[1]+",";
        }
    }
    colName=colName.slice(0,colName.length-1)
	return colName;
}


function getSELECTquery()
{

    var query='SELECT ';
    var width='';
    var desc='';
    var operator='';    
    var widthCol=0;
    var search = location.search;
    search = search.replace(/\?/,'');
    var searchAttributes = search.split('&');
    for(var no=0;no<searchAttributes.length;no++)
    {
        var items = searchAttributes[no].split('=');
        if(items[0]=='col')
        {
            query+=items[1]+",";
        }
        else if(items[0]=='tbl')
        {
 	        query=query.slice(0,query.length-1)
	        query+=" FROM "+items[1];
        }
        if(items[0]=='width')
        {
 	        width+=items[1]+",";
 	        widthCol+=parseInt(items[1]);
 	    }
 	    if(items[0]=='desc')
 	    {
 	        desc+=items[1]+",";
 	    }
 	    if(items[0]=='op')
 	    {
     	    operator+=items[1]+",";
 	    }
    }
    width=width.slice(0,width.length-1)
    desc=desc.slice(0,desc.length-1)
    operator=operator.slice(0,operator.length-1)
    var param=query+"^"+width+"^"+desc+"^"+operator;
    document.getElementById("slider02").style.width=widthCol; //200;widthCol;
    return param;
}
*/

function show(e)
{
    
    //alert('e : ' + e);
    var query = '';
    var retValue='';
    var query = window.location.search.substring(1);
    //alert('query : ' + query);
    var sQuery=unescape(query.replace(/\+/g, " "));
    var strSplit=query.split('^');
    var txtID_DESC=strSplit[5].split('-');
    var txtID=txtID_DESC[0].toString();
    var txtDesc=txtID_DESC[1].toString();
    var txtKV=txtID_DESC[0].toString();
    if(txtID_DESC.length>2)
    {
    	txtID=txtID_DESC[1].toString();
    	txtDesc=txtID_DESC[2].toString();
    }
    var tgt = e.target || e.srcElement;
    if (tgt && tgt.nodeName.toLowerCase() == 'td')
    {
        var table = document.getElementById("table");
        
        var row = table.rows[tgt.parentNode.rowIndex];
        var cell = row.cells[0];
        
        var content = cell.firstChild.nodeValue;
        
        retValue = content;

        if (row.cells[1] != undefined) {
            var cell2 = row.cells[1];
            var content2 = '';
            
            if(cell2.firstChild !=null)
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
        //alert(retValue);
        
        var setVal = retValue.split('~');
        var mapColLength = setVal.length;

        //window.opener.document.getElementById(txtID).value = cell.firstChild.nodeValue;

        if (mapColLength == 3)
        {
            document.getElementById(txtID_DESC[0]) = cell.firstChild.nodeValue;
            document.getElementById(txtID_DESC[1]).value = cell2.firstChild.nodeValue;
            if (txtID_DESC[1] != null && mapCol[2].length > 0) document.getElementById(mapCol[2]).value = retVal[2];
        }
        else if (mapColLength == 2)
        {
            window.opener.document.getElementById(txtID_DESC[0]).value = cell.firstChild.nodeValue;
            if (txtID_DESC[1] != null && txtDesc.length > 0) window.opener.document.getElementById(txtID_DESC[1]).value = cell2.firstChild.nodeValue;
        }
        else if (mapColLength == 1)
        {
            window.opener.document.getElementById(txtID_DESC[0]).value = cell.firstChild.nodeValue;
        }

        window.returnValue=retValue;
        window.close();
    }
}


function resetSearchField() {
    document.getElementById("txtSearch").value = "";
}