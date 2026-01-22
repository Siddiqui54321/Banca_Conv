  
/******************************** Title Event ************************************/
function Title_ChangeEvent(obj)
{
	filterSexComb(obj);
}

function filterSexComb(obj){
	var genderPrevious = myForm.ddlNPH_SEX.value;
	var objTxt = obj.options[obj.selectedIndex].text.toString();
	if(objTxt=="Mr.")
		myForm.ddlNPH_SEX.selectedIndex=1;
	else if(objTxt.indexOf("Miss.")>=0 || objTxt.indexOf("Mrs.")>=0 || objTxt.indexOf("Ms.")>=0)
		myForm.ddlNPH_SEX.selectedIndex=2;
	else
		myForm.ddlNPH_SEX.selectedIndex=1;
		
	var genderCurrent = myForm.ddlNPH_SEX.value;
	
	if(genderCurrent != genderPrevious)
	{
		generateID(IDFormat);
	}
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


/********************************************************************************/
/******************************** NIC Events ************************************/
/********************************************************************************/
//Possible ID Formates
var CNIC     = "CNIC";
var OLDNIC   = "OLDNIC";
var NOORID   = "NOORID";
var PASPPORT = "PASPPORT";

var IDFormat = CNIC;

function CNICFormat()
{
	if(IDFormat == CNIC || IDFormat == "")
	{
		return true;
	}
	else
	{
		return false;
	}
}

function NIC_Focus(obj)
{
	if(CNICFormat() == false)
	{
		return;
	}
	else
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
}

function NIC_KeyPress(e,obj)
{
	
	if(CNICFormat() == false)
	{
		return;
	}
	else
	{
		if(obj.value.length>=13)
		{
			e.keyCode = 0;
			e.cancelBubble = true;

			return false;
		}
		return checkNumeric(e);
	}
}

/*function taLimit(Obj) 
{
	if (Obj.value.length > 13) return false;
	return true;
}*/


function NIC_KeyUp(e,obj)
{
	if(CNICFormat() == false)
	{
		return;
	}
	else
	{
		//if(obj.value.length==13)
		//{
		//	NIC_Blur(obj);
		//}
		if(obj.value.length>13)
		{
			
			e.keyCode = 0;
			return false;
			obj.value = obj.value.substring(0,13);
		}
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

function setFieldsBasedOnNIC(objID)
{
	if(objID.disabled == true )
	{
		if(objID.value.length > 0)
		{
			/*var className="ace.ILUS_ET_NM_PER_PERSONALDET";
			var methodName="checkAndGetClientInfo";
			var parameters = objID.value;
			var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			var ClientExist = str_resultArray[0];
			var HaveAnyValidatedProposal = str_resultArray[14];
			disableFields = false;
			
			if(ClientExist=="Y" &&  HaveAnyValidatedProposal == "Y")
			{
				disableFields = true;
			}
			disableFieldsBasedOnNIC(disableFields);*/
			

			var className="ace.ILUS_ET_NM_PER_PERSONALDET";
			var methodName="isClientUpdateAllowed";
			var parameters = objID.value;
			var updateAllowed = executeClass(className + "," + methodName + "," + parameters); 
		
			disableFields = false;
			
			//Update is not allowed here
			if(updateAllowed=="N")
			{
				disableFields = true;
			}
			disableFieldsBasedOnNIC(disableFields);
		}
	}
	else
	{
		if(objID.value.length > 0)
			NIC_Blur(objID);
	}
}




function NIC_Blur(objNIC)
{
	if(objNIC.disabled == false )
	{
		objNIC.value = objNIC.value.replace("-","").replace("-","");
		if(objNIC.value.length == 13)
		{
			
			var disableFields = false;
			var NIC=objNIC.value;
			var formatedNIC = "";
			var i=0;

			//***************** Format NIC ***************************//
			// '-' character at position 5
			for(i=0;i<5 ;i++) {formatedNIC = formatedNIC + NIC.charAt(i);}  formatedNIC = formatedNIC + "-" ;
			// Next '-' character at posistion 12
			for(i=5;i<12;i++) {formatedNIC = formatedNIC + NIC.charAt(i);}  formatedNIC = formatedNIC + "-" + NIC.charAt(12);
				
			
			
			/************ Get client information and set it relative fields ***********************/
			var className="ace.ILUS_ET_NM_PER_PERSONALDET";
			var methodName="checkAndGetClientInfo";
			var parameters = objNIC.value+ "," + _lastEvent.toUpperCase();
			var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			
			if(str_resultArray[0] == "Y")
			{
				if(str_resultArray[14] == "Y")
				{
					disableFields = true;
				}
				
				getField("NPH_TITLE").value = str_resultArray[1];
				getField("NPH_FULLNAME").value = str_resultArray[2];

				getField("NPH_DOCISSUEDAT").value = str_resultArray[3];
				getField("NPH_DOCEXPIRDAT").value = str_resultArray[4];
				getField("NPH_FATHERNAME").value = str_resultArray[5];
				getField("NPH_MAIDENNAME").value = str_resultArray[6]; 
				
				getField("NPH_BIRTHDATE").value = str_resultArray[7];
				getField("NPH_SEX").value = str_resultArray[8];
				
				//OCCUPATION
				//setOccupationByID(str_resultArray[9]);
				getField("COP_OCCUPATICD").value = str_resultArray[9];
				getField("CCL_CATEGORYCD").value = str_resultArray[10];
				
				//Height 
				getField("NPH_HEIGHTTYPE").value = str_resultArray[11];
				getField("NU1_ACTUALHEIGHT").value = str_resultArray[12];
				getField("NU1_CONVERTHEIGHT").value = str_resultArray[13];
				
				//Weight
				getField("NPH_WEIGHTTTYPE").value = str_resultArray[14];
				getField("NU1_ACTUALWEIGHT").value = str_resultArray[15];
				getField("NU1_CONVERTWEIGHT").value = str_resultArray[16];
				
				//Customer No
				getField("NPH_CODE").value = str_resultArray[17];
				getField("CNT_NATCD").value = str_resultArray[21];
				getField("NU1_SMOKER").value = str_resultArray[22];
				
				
				//**** Call functions *****//
				CalculateEntryAge(getField("NPH_BIRTHDATE"));
				//convert_to_feet();
				
				//calculateBMI();
				filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
			}
			
			//**** Set Formated NIC to field *****//
			objNIC.value = formatedNIC;
			
			//**** Set Field status *****//
			disableFieldsBasedOnNIC(disableFields);

		}
		else
		{
			var objIdType = getField("NPH_IDTYPE");
			var index = objIdType.selectedIndex;
			var Id = objIdType.options[index].text;
			alert(Id + " must be 13 characters long.");
			objNIC.focus();
			return;	
		}
	}
}

function disableFieldsBasedOnNIC(flag)
{
	getField("NPH_TITLE").disabled          = flag;
	getField("NPH_FULLNAME").disabled       = flag;
	getField("NPH_FULLNAMEARABIC").disabled = flag;
	getField("NPH_BIRTHDATE").disabled      = flag;
	getField("NPH_SEX").disabled            = flag;
	//getField("COP_OCCUPATICD").disabled     = flag;
	//getField("CCL_CATEGORYCD").disabled     = flag;
	getField("CNT_NATCD").disabled     = flag;
}
/********************************************************************************/
/***************************** NIC Events - End *********************************/
/********************************************************************************/



/********************************************************************************/
/********************************* Noor ID **************************************/
/********************************************************************************/

function IDgenerated(strIDFormat)
{
	if(strIDFormat == NOORID)
	{
		if(getField("CNIC_VALUE").value.length < 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	else
	{
		if(getField("CNIC_VALUE").value.length < 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}


//New Function based on First Second and Last Name fields
function generateID(strIDFormat)
{
	//*********** Remove Extra Space **************
	var strName = trim(getField("NPH_FULLNAME").value);
	var arrName = strName.split(" ") ;
	var firstName="";
	var secndName="";
	var thirdName="";
	var counter=0;
	
	
	
	//*********** Generate ID now **************
	if(strIDFormat == NOORID)
	{
		//if(_lastEvent == 'New')
		{
			var id = "";
			
			var NAME = getField("NPH_FULLNAME").value;
			var DOB  = getField("NPH_BIRTHDATE").value;
			var SEX  = getField("NPH_SEX").value;

			if(trim(NAME).length < 1 || trim(DOB).length < 1 || trim(SEX).length < 1)
			{
				return;
			}
			else
			{
				var FirstName  = getField("NPH_FIRSTNAME").value.substring(0, 2).toUpperCase();//2-char
				var SecondName = getField("NPH_SECONDNAME").value.substring(0, 1).toUpperCase();//1-char
				var LastName   = getField("NPH_LASTNAME").value.substring(0, 2).toUpperCase();//2-char
				
				//Formation for Date
				var DD = DOB.substring(0, DOB.indexOf ("/"));
				var MM = DOB.substring(DOB.indexOf("/")+1, DOB.lastIndexOf ("/"));
				var YY = DOB.substring(DOB.lastIndexOf("/")+1, DOB.length);
				if(DD.length <2) DD = "0" + DD;
				if(MM.length <2) MM = "0" + MM;
				
				id = FirstName + SecondName + LastName + SEX + YY + MM + DD;
				document.getElementById('txtCNIC_VALUE').value = id;
				checkAndGetClientInfoFromSetup(strIDFormat, document.getElementById('txtCNIC_VALUE'));
			}
		}
	}
}

/* OLD Function based on Full Name field
function generateID(strIDFormat)
{
	//*********** Remove Extra Space **************
	var strName = trim(getField("NPH_FULLNAME").value);
	var arrName = strName.split(" ") ;
	var firstName="";
	var secndName="";
	var thirdName="";
	var counter=0;
	for(i=0; i<arrName.length; i++)
	{	//Not
		if(!(arrName[i] == "" || arrName[i] == " "))
		{
			if(counter == 0) strName = arrName[i];
			else             strName = strName + " " + arrName[i];
			
			if(counter == 0)      firstName = arrName[i];
			else if(counter == 1) secndName = arrName[i];
			else if(counter == 2) thirdName = arrName[i];

			counter++;
		}
	}
	getField("NPH_FULLNAME").value = strName;
	
	
	
	//*********** Generate ID now **************
	if(strIDFormat == NOORID)
	{
		//if(_lastEvent == 'New')
		{
			var id = "";
			
			var NAME = getField("NPH_FULLNAME").value;
			var DOB  = getField("NPH_BIRTHDATE").value;
			var SEX  = getField("NPH_SEX").value;

			if(trim(NAME).length < 1 || trim(DOB).length < 1 || trim(SEX).length < 1)
			{
				return;
			}
			else
			{
				var FirstName  = firstName.substring(0, 2).toUpperCase();//2-char
				var SecondName = secndName.substring(0, 1).toUpperCase();//1-char
				var LastName   = thirdName.substring(0, 2).toUpperCase();//2-char
				
				//Formation for Date
				var DD = DOB.substring(0, DOB.indexOf ("/"));
				var MM = DOB.substring(DOB.indexOf("/")+1, DOB.lastIndexOf ("/"));
				var YY = DOB.substring(DOB.lastIndexOf("/")+1, DOB.length);
				if(DD.length <2) DD = "0" + DD;
				if(MM.length <2) MM = "0" + MM;
				
				id = FirstName + SecondName + LastName + SEX + YY + MM + DD;
				document.getElementById('txtCNIC_VALUE').value = id;
				checkAndGetClientInfoFromSetup(strIDFormat, document.getElementById('txtCNIC_VALUE'));
			}
		}
	}
}
*/

function checkAndGetClientInfoFromSetup(IdFormat, objID)
{
	if(IdFormat == NOORID)
	{
		var disableFields = false;
		var clientFound   = false;
		
		/************ Execute class - Remote Class call ***********************/
		var className="ace.ILUS_ET_NM_PER_PERSONALDET";
		var methodName="checkAndGetClientInfo";
		var parameters = objID.value + "," + _lastEvent.toUpperCase() ;
		var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
		
		var ClientExist = str_resultArray[0];
		
		if(ClientExist=="Y")
		{
			var message = "Client ID already exist as per following information : \n" +
			              "  *  Name  : " + str_resultArray[2] + "\n" +
			              "  *  DOB   : " + str_resultArray[3] + "\n" + 
			              "  *  Gender: " + (str_resultArray[4]=="F" ? "Female" : "Male") + "\n\n" +
			              "Click 'OK' to create new ID \nOR  \nClick 'Cancel' to use Existing Client Information.";
			var answer = confirm(message)
			if(answer)
			{   //************************************************************************//
				//************************** Create New ID *******************************//
				//************************************************************************//

				var intIDIlas  = 0;
				var intIDBanca = 0;
				var MaximumID = 0;
				
				
				var MaxIDIlas  = fetchDataArray("select SUBSTR(MAX(NPH_IDNO2),-2,2) FROM ILAS_LNPH_PHOLDER WHERE UPPER(NPH_IDNO2) LIKE '" + objID.value + "%'");
				var MaxIDBanca = fetchDataArray("select SUBSTR(MAX(NPH_IDNO),-2,2)  FROM LNPH_PHOLDER      WHERE UPPER(NPH_IDNO)  LIKE '" + objID.value + "%'");
				
				if(MaxIDIlas == null || MaxIDIlas[1][0] == "" )
				{
					intIDIlas = 0;
				}
				else
				{
					intIDIlas = parseInt(MaxIDIlas[1][0]);
				}
				
				if(MaxIDBanca == null || MaxIDBanca[1][0] == "" )
				{
					intIDBanca = 0;
				}
				else
				{
					intIDBanca = parseInt(MaxIDBanca[1][0]);
				}


				if(intIDIlas == 0 && intIDBanca == 0 )
				{
					alert('Problem in calculating new Serial for ID');
					return;
				}
				else
				{
					MaximumID = intIDIlas;
					if(intIDBanca > intIDIlas)
					{
						MaximumID = intIDBanca;
					}
				}

				//{	//*******************************************************************//
					//***** Assign New Serial (by incremental factor) ******************//
					//***** e.g. if last serial was 04 then new serial will be 05 ******//
					//*****      Now ID would become like this e.g. XXXXGYYYYMMDD05 ****//
					//*******************************************************************//
					//var serialID = "" + (parseInt(MaxID[1][0]) + 1);
					var serialID = "" + (MaximumID + 1);
					if(serialID.length < 2) serialID = "0" + serialID;
					objID.value += serialID;
				//}
				
				//if(MaxID == null || MaxID[1][0] == "" )
				//{
				//	alert('Problem in calculating new Serial for ID');
				//	return;
				//}
				//else
				//{	//*******************************************************************//
				//	//***** Assign New Serial (by incremental factor) ******************//
				//	//***** e.g. if last serial was 04 then new serial will be 05 ******//
				//	//*****      Now ID would become like this e.g. XXXXGYYYYMMDD05 ****//
				//	//*******************************************************************//
				//	var serialID = "" + (parseInt(MaxID[1][0]) + 1);
				//	if(serialID.length < 2) serialID = "0" + serialID;
				//	objID.value += serialID;
				//}
			}
			else
			{	//************************************************************************//
				//******************** Get Existing Client Info **************************//
				//************************************************************************//			
				getField("NPH_TITLE").value = str_resultArray[1];
				getField("NPH_FULLNAME").value = str_resultArray[2];
				getField("NPH_BIRTHDATE").value = str_resultArray[3];
				getField("NPH_SEX").value = str_resultArray[4];
				//OCCUPATION
				setOccupationByID(str_resultArray[5]);
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
				//alert(getField("NPH_CODE").value);
				if(str_resultArray[14] == "Y") disableFields = true;
				
				//Field NPH_IDNO
				/*if(str_resultArray[15].length > 0 )
				{
					getField("CNIC_VALUE").value = str_resultArray[15].toUpperCase();;
				}
				if(str_resultArray[16].length > 0 )
				{
					getField("CNIC_VALUE").value = str_resultArray[16].toUpperCase();;
				}*/
				getField("CNIC_VALUE").value = str_resultArray[16].toUpperCase();;
				//Don't get IDNO2 from Ilas (bcz ILAS.id2= BANCA.id1)
				//getField("NPH_IDNO2").value  = str_resultArray[16];
				
				getField("CNT_NATCD").value  = str_resultArray[17];//Nationality
				getField("NU1_SMOKER").value  = str_resultArray[18];//Smoker
				
				if(str_resultArray[19] == "N")//Updateion not allowed
				{
					disableFields = true;
				}
				
				
				
				//**** Call functions *****//
				CalculateEntryAge(getField("NPH_BIRTHDATE"));
				calculateBMI();
				filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
			}
		}
		else
		{	//Customer Not found
			document.getElementById('txtCNIC_VALUE').value += "01";
		}
		
		//**** Set Field status *****//
		disableFieldsBasedOnNIC(disableFields);
		
	}
}
/********************************************************************************/
/****************************** Noor ID - END ***********************************/
/********************************************************************************/










function NIC_Blur2(objNIC)
{
	if(objNIC.disabled == false )
	{
		objNIC.value = objNIC.value.replace("-","").replace("-","");
		
		if(objNIC.value.length == 13)
		{
			var disableFields = false;
			var NIC=objNIC.value;
			var formatedNIC = "";
			var i=0;

			//***************** Format NIC ***************************//
			// '-' character at position 5
			for(i=0;i<5 ;i++) {formatedNIC = formatedNIC + NIC.charAt(i);}  formatedNIC = formatedNIC + "-" ;
			// Next '-' character at posistion 12
			for(i=5;i<12;i++) {formatedNIC = formatedNIC + NIC.charAt(i);}  formatedNIC = formatedNIC + "-" + NIC.charAt(12);
			
			objNIC.value = formatedNIC;
			
		}
		else
		{
			var objIdType = getField("NPH_IDTYPE");
			var index = objIdType.selectedIndex;
			var Id = objIdType.options[index].text;
			alert(Id + " must be 13 characters long.");
			objNIC.focus();
			return;	
		}
	}
}