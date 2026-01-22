
/******************************** Title Event ************************************/
function Title_ChangeEvent(obj)
{
	filterSexComb(obj);
}

function filterSexComb(obj){
	var objTxt = obj.options[obj.selectedIndex].text.toString();
	if(objTxt=="Mr.")
		myForm.ddlNPH_SEX.selectedIndex=1;
	else if(objTxt.indexOf("Miss.")>=0 || objTxt.indexOf("Mrs.")>=0 || objTxt.indexOf("Ms.")>=0)
		myForm.ddlNPH_SEX.selectedIndex=2;
	else
		myForm.ddlNPH_SEX.selectedIndex=0;
}


/******************************** Gender Event ************************************/
function Gender_ChangeEvent(objGender)
{
	//get Title field
	var objTitle = getField("NPH_TITLE");
	var title = objTitle.options[objTitle.selectedIndex].text.toString();
	
	if(objGender.value=="M")
	{
		if(title=="")
		{
			alert("Please select gender first");
		}
		else if(title=="Miss." || title=="Mrs." || title=="Ms." )
		{
			alert("As per Title Geneder must be Female");
			myForm.ddlNPH_SEX.selectedIndex=2;
		}
	}
	else if(objGender.value=="F")
	{
		if(title=="")
		{
			alert("Please select gender first");
		}
		else if(title=="Mr." )
		{
			alert("As per Title Geneder must be Male.");
			myForm.ddlNPH_SEX.selectedIndex=1;
		}	
	}
}

/******************************** NIC Events ************************************/
function NIC_Focus(obj)
{
	getField("HiddenNIC").value = obj.value;
	var ValidChars = "0123456789.";
	var NIC="";
	for (i = 0; i<obj.value.length; i++)
	{
		if(ValidChars.indexOf(obj.value.charAt(i)) != -1)
		{
			NIC = NIC + obj.value.charAt(i);
		}
	}
	obj.value=NIC;
	
}


function NIC_KeyPress(e,obj)
{
	return checkNumeric(e);
}

function NIC_KeyUp(e,obj)
{
	if(obj.value.length==13)
	{
		NIC_Blur(obj);
	}
}

function checkNumeric(e)
{
	//Only characters  >>>> var regex=/^[a-zA-Z];
	//Only Numbers     >>>> var regex=/^[0-9];
	//Only HEX Numbers >>>> var regex=/^[0-9a-fA-F];
	
	var regex=/^[0-9]/;
	var keynum;
	var keychar;
	
	if(window.event) // IE
	{
		keynum = e.keyCode;
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which;
	}
	
	keychar = String.fromCharCode(keynum);
	return regex.test(keychar);
}

function NIC_Blur(objNIC)
{
	if(objNIC.value.length == 15)
	{
		return;
	}
	else if(objNIC.value.length == 13)
	{
		var n=objNIC.value;
		var str;
		var msg = "";
		var i=0;

		for(i=0;i<5;i++)
		{
			msg = msg + n.charAt(i);
		}
		msg=msg+"-";

		///Next Portion
		for(i=5;i<12;i++)
		{
			msg = msg + n.charAt(i);
		}
		msg=msg+"-"+n.charAt(12);
		myForm.txtCNIC_VALUE.value=msg;	
		
		/************ Call Stored Procedure ***********************/
		if(getField("HiddenNIC").value != objNIC.value)
		{
			//Call class remotely
			var className="ace.ILUS_ET_NM_PER_PERSONALDET";
			var methodName="checkAndGetClientInfo";
			var parameters = objNIC.value;
			var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			if(str_resultArray[0]=="Y")
			{
				getField("NPH_TITLE").value = str_resultArray[1];
				getField("NPH_FULLNAME").value = str_resultArray[2];
				getField("NPH_BIRTHDATE").value = str_resultArray[3];
				getField("NPH_SEX").value = str_resultArray[4];
				getField("COP_OCCUPATICD").value = str_resultArray[5];
				getField("CCL_CATEGORYCD").value = str_resultArray[6];
				//Height 
				getField("NPH_HEIGHTTYPE").value = str_resultArray[7];
				getField("NU1_ACTUALHEIGHT").value = str_resultArray[8];
				getField("NU1_CONVERTHEIGHT").value = str_resultArray[9];
				//Weight
				getField("NPH_WEIGHTTTYPE").value = str_resultArray[10];
				getField("NU1_ACTUALWEIGHT").value = str_resultArray[11];
				getField("NU1_CONVERTWEIGHT").value = str_resultArray[12];
				//Customer No
				getField("NPH_CODE").value = str_resultArray[13];
			}
		}
	}
	else
	{
		var Id = getField("NPH_IDTYPE").value;
		alert(Id + " must be 13 characters long.");
		objNIC.focus();
		return;	
	}
}