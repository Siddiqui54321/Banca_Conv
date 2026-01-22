var str_URL="";
var str_SendURL="";
var int_ChildFrameNo=1;
var str_ChildPT="";
var childURL = "";
childFrames = null;

bln_ShouldSubmit=true;
valueFound=true; //for auto submission of lov


// funtion to stop Ctrl N "New window opening"
document.onkeydown = function () {
    
    if (((event.keyCode == 78) || (event.keyCode == 110)) && (event.ctrlKey)) {
        event.cancelBubble = true;
        event.returnValue = false;
        event.keyCode = false;
        return false;
    }
}


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

// Changed by SYD KAMRAN (07052015) Field Focus Problem
function blurLOV() {
    for (var eCntr = 0; eCntr < document.forms[0].elements.length; eCntr++) {
        newElement = document.forms[0].elements(eCntr);
	
        if (newElement.className!="hidden" && newElement.type != "hidden" && newElement.disabled != true) {
				newElement.focus();
				break;
			}
	}
}

function DispFieldPressed(){
	if (event.keyCode==13){
	    blurLOV();
		if(valueFound)
		SearchInChild();
	}
}

function SearchInChild(){	
	if (typeof(Page_ClientValidate)!='undefined')
		Page_ClientValidate();
	if (typeof(Page_IsValid)=='undefined' || Page_IsValid){
		if (CustomValidate()){
			DoSearch();
		}			
	}
}
function DoSearch(){
	if (bln_ShouldSubmit==true) {
		if (parent.getMyChildFrames != null)
			childFrames = parent.getMyChildFrames() ;					
		if (childFrames != null && childFrames.length >0){						
			for (jj=0; jj < childFrames.length; jj++){				
				str_URL = childFrames[jj].window.document.URL;				
				//alert("for");
				if (str_URL.indexOf('?')>0)
					childFrames[jj].window.location	= str_URL + '&' + getQueryString();
				else
					childFrames[jj].window.location	= str_URL + '?' + getQueryString();
			}
		}			
		else{
			if (childURL.length==0)
				childURL = parent.frames[int_ChildFrameNo].window.document.URL;
			str_URL = childURL;
			if (str_URL.indexOf('?')>0)
				str_URL += '&';
			else
				str_URL += '?';
			
			str_URL+= getQueryString();
			parent.frames[int_ChildFrameNo].window.location=str_URL;
		}
	}	
}
function getQueryString(){
	strQueryString = "--";
	if (formFields!=null){	
		for (ii=0; ii<formFields.length; ii++ ){			
			strFormField=formFields[ii].replace("txt","");
			strFormField=strFormField.replace("ddl","");
			strFormField=strFormField.replace("lbl","");
			strQueryString += "&r_"+ strFormField +"="+ document.getElementById(formFields[ii]).value ;
		}
	}
	
	strQueryString = strQueryString.replace("--&", "");
	return strQueryString;
}

function CustomValidate()
{
	return true;
}


