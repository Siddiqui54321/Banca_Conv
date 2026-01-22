/************************** Dynamic hide column base on setup ******************************/
var disabledFields = "";
function setFieldStatusAsPerClientSetup(screen)
{
	//alert(111);
	var columnArray;
	
	//var columnArray = hiddenColumns.split('~');
	if(screen=='PROPOSAL')
	{
		if(parent.parent.ProposalColumns == '')
			return
		else
			columnArray = parent.parent.ProposalColumns.split('~');
	}
	else if(screen=='PERSONNEL')
	{
		if(parent.parent.personelColumns == '')
			return
		else
			columnArray = parent.parent.personelColumns.split('~');
	}
	else if(screen=='PERSONNEL2')
	{
		if(parent.parent.personel2Columns == '')
			return
		else
			columnArray = parent.parent.personel2Columns.split('~');
	}	
	else if(screen=='PLAN')
	{
		if(parent.parent.planColumns == '')
			return
		else
			columnArray = parent.parent.planColumns.split('~');
	}
	else if(screen=='PLAN_BUTTON')
	{
		if(parent.parent.planButtons == '')
			return
		else
			columnArray = parent.parent.planButtons.split('~');
	}	
	else if(screen=='RIDER')
	{
		if(parent.parent.riderColumns == '')
			return
		else
			columnArray = parent.parent.riderColumns.split('~');
	}
	else if(screen=='ACCEPTANCE')
	{
		if(parent.parent.acceptanceColumns == '')
			return
		else
			columnArray = parent.parent.acceptanceColumns.split('~');
	}

	//alert(columnArray.length);
	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		var columnID   = columnInfo[0];
		var Visibility = columnInfo[1].toUpperCase();
		var Disable    = columnInfo[2].toUpperCase();
		var Caption    = columnInfo[3];
		var defValue   = columnInfo[4];
		var mandatory  = columnInfo[5].toUpperCase();
		var Format     = columnInfo[6].toUpperCase();
		var Criteria   = columnInfo[7];
	
	
		var objField      = document.getElementById(columnID);
		var objColumnCell = document.getElementById('ctl' + columnID);
		var objLabelCell  = document.getElementById('lbl' + columnID);
		//alert(columnID);
		//alert(Visibility);
		
		
		if(objField != null)
		{
			/************* Visiblity *************/
			if(Visibility == "Y")
			{	
				if(objField      != null) {objField.style.display     = ''; objField.style.visibility = 'visible';}
				if(objColumnCell != null) objColumnCell.style.display = '';
				if(objLabelCell  != null) objLabelCell.style.display  = '';

				/************* Disable *************/
				if     (Disable == "Y") {objField.disabled=true; disabledFields += '~' + columnID;}
				else if(Disable == "N") {objField.disabled=false;objField.readOnly=false;}
			}
			else if(Visibility == "N")
			{
				if(objField      != null) {objField.style.visibility  = 'hidden';objField.style.display = 'none';};
				if(objColumnCell != null) {objColumnCell.style.display = 'none';}
				if(objLabelCell  != null) {objLabelCell.style.display  = 'none';}
			}
			
			/************* Default Value *************/
			if(defValue != null && defValue != "") objField.value = defValue;
			
			/************* Caption *************/
			if(Trim(Caption) != "" && objLabelCell != null ) objLabelCell.innerHTML = Caption;

		}
	}
	//// End of for
}

function setFieldsProductWise(screen, thisProduct)
{
	//alert(screen);
	//alert(thisProduct);
	if(screen=='PLAN')
	{
		if(parent.parent.planColumns == '')
			return
		else
			columnArray = parent.parent.planColumns.split('~');
	}
	
	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		var columnID   = columnInfo[0];
		var Visibility = columnInfo[1].toUpperCase();
		var Disable    = columnInfo[2].toUpperCase();
		var Caption    = columnInfo[3];
		var defValue   = columnInfo[4];
		var mandatory  = columnInfo[5].toUpperCase();
		var Format     = columnInfo[6].toUpperCase();
		var Criteria   = columnInfo[7];
	
	
		var objField      = document.getElementById(columnID);
		var objColumnCell = document.getElementById('ctl' + columnID);
		var objLabelCell  = document.getElementById('lbl' + columnID);
		
		if(objField != null)
		{
			/************* NOTE: Buttons are product based *************/
			var blnProdFound = false;
			if(Trim(Criteria) != "")
			{
				var prodArray = Criteria.split(' ');
				for (prodIndex=0; prodIndex < prodArray.length; prodIndex++)
				{
					if(thisProduct == prodArray[prodIndex]) 
					{
						blnProdFound = true;
						break;
					}
					else
					{
						blnProdFound = false;
					}
				}

				if(blnProdFound == false && Visibility == "Y")
				{
					Visibility = "N";
				}
				else if(blnProdFound == false && Visibility == "N")
				{
					Visibility = "Y";
				}	
			}
					
			/************* Visiblity *************/
			if(Visibility == "Y")
			{	
				if(objField      != null) {objField.style.display     = ''; objField.style.visibility = 'visible';}
				if(objColumnCell != null) objColumnCell.style.display = '';
				if(objLabelCell  != null) objLabelCell.style.display  = '';

				/************* Disable *************/
				if     (Disable == "Y") {objField.disabled=true; disabledFields += '~' + columnID;}
				else if(Disable == "N") {objField.disabled=false;objField.readOnly=false;}
			}
			else if(Visibility == "N")
			{	
				if(objField      != null) {objField.style.visibility  = 'hidden';objField.style.display = 'none';objField.value='';};
				if(objColumnCell != null) {objColumnCell.style.display = 'none';}
				if(objLabelCell  != null) {objLabelCell.style.display  = 'none';}				
			}
			
			/************* Default Value *************/
			if(defValue != null && defValue != "") objField.value = defValue;
			//alert(objField.id + ' - ' + Caption);
			
			/************* Caption *************/
			if(Trim(Caption) != "" && objLabelCell != null ) objLabelCell.innerHTML = Caption;

		}
	}
	//// End of for	
}

function setButtonsProductWise(screen, thisProduct)
{
	if(screen=='PLAN_BUTTON')
	{
		if(parent.parent.planButtons == '')
			return
		else
			columnArray = parent.parent.planButtons.split('~');
	}
	
	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		var columnID   = columnInfo[0];
		var Visibility = columnInfo[1].toUpperCase();
		var Disable    = columnInfo[2].toUpperCase();
		var Caption    = columnInfo[3];
		var defValue   = columnInfo[4];
		var mandatory  = columnInfo[5].toUpperCase();
		var Format     = columnInfo[6].toUpperCase();
		var Criteria   = columnInfo[7];
	
		var objField   = document.getElementById(columnID);
		
		if(objField != null)
		{
			/************* NOTE: Buttons are product based *************/
			var blnProdFound = false;
			if(Trim(Criteria) != "")
			{
				var prodArray = Criteria.split(' ');
				for (prodIndex=0; prodIndex < prodArray.length; prodIndex++)
				{
					if(thisProduct == prodArray[prodIndex]) 
					{
						blnProdFound = true;
						break;
					}
					else
					{
						blnProdFound = false;
					}
				}

				if(blnProdFound == false && Visibility == "Y")
				{
					Visibility = "N";
				}
				else if(blnProdFound == false && Visibility == "N")
				{
					Visibility = "Y";
				}	
			}
			//alert(blnProdFound);
			
			/************* Visiblity *************/
			if(Visibility == "Y")
			{	
				if(objField      != null) {objField.style.display = ''; objField.style.visibility = 'visible';}

				/************* Disable *************/
				if     (Disable == "Y") {objField.disabled=true; disabledFields += '~' + columnID;}
				else if(Disable == "N") {objField.disabled=false;objField.readOnly=false;}
			}
			else if(Visibility == "N")
			{
				if(objField      != null) {objField.style.visibility  = 'hidden';objField.style.display = 'none';};
			}
			
			/************* Default Value *************/
			if(defValue != null && defValue != "") objField.value = defValue;
			
			/************* Caption *************/
			if(Trim(Caption) != "" && objLabelCell != null ) objLabelCell.innerHTML = Caption;
			
		}
	}
}


function Trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}

function EnableFieldsBeforeSubmitting()
{
	var disabledColumnArray = disabledFields.split('~');

	for (i = 0; i < disabledColumnArray.length; i++) {
       
       var objField = document.getElementById(disabledColumnArray[i]);
		if(objField  != null) objField.disabled=false;
	}
}



function getFieldCaptionFromSetup(screen, objField)
{
	var fieldCaption = "";
	var columnArray;
	if(screen=='PROPOSAL')
	{
		columnArray = parent.parent.ProposalColumns.split('~');
	}
	else if(screen=='PERSONNEL')
	{
		columnArray = parent.parent.personelColumns.split('~');
	}
	else if(screen=='PERSONNEL2')
	{
		columnArray = parent.parent.personel2Columns.split('~');
	}
	else if(screen=='PLAN')
	{
		columnArray = parent.parent.planColumns.split('~');
	}
	else if(screen=='RIDER')
	{
		columnArray = parent.parent.riderColumns.split('~');
	}
	else if(screen=='ACCEPTANCE')
	{
		columnArray = parent.parent.acceptanceColumns.split('~');
	}	

	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		var columnID   = columnInfo[0];
		//var Visibility = columnInfo[1].toUpperCase();
		//var Disable    = columnInfo[2].toUpperCase();
		var Caption    = columnInfo[3];
		//var defValue   = columnInfo[4];
		
		if(objField.id == columnID)
		{
			fieldCaption = Caption;
			break;
		}
	}
	
	return fieldCaption;
}


function getFieldFormatFromSetup(screen, objField)
{
	var fieldFormat = "";
	var columnArray;
	if(screen=='PROPOSAL')
	{
		columnArray = parent.parent.ProposalColumns.split('~');
	}
	else if(screen=='PERSONNEL')
	{
		columnArray = parent.parent.personelColumns.split('~');
	}
	else if(screen=='PERSONNEL2')
	{
		columnArray = parent.parent.personel2Columns.split('~');
	}
	else if(screen=='PLAN')
	{
		columnArray = parent.parent.planColumns.split('~');
	}
	else if(screen=='RIDER')
	{
		columnArray = parent.parent.riderColumns.split('~');
	}
	else if(screen=='ACCEPTANCE')
	{
		columnArray = parent.parent.acceptanceColumns.split('~');
	}	

	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		
		var columnID   = columnInfo[0];
		//var Visibility = columnInfo[1].toUpperCase();
		//var Disable    = columnInfo[2].toUpperCase();
		//var Caption    = columnInfo[3];
		//var defValue   = columnInfo[4];
		var Format     = columnInfo[6].toUpperCase();
		
		if(objField.id == columnID)
		{
			fieldFormat = Format;
			break;
		}
	}
	return fieldFormat;
}




/*function mandatoryCheck(screen)
{
	var validateMandatory = true;
	var columnArray;
	if(screen=='PERSONNEL')  { columnArray = parent.parent.personelColumns.split('~'); }
	else if(screen=='PLAN')  { columnArray = parent.parent.planColumns.split('~');     }
	else if(screen=='RIDER') { columnArray = parent.parent.riderColumns.split('~');    }

	for (i=0; i<columnArray.length; i++)
	{
		var columnInfo = columnArray[i].split(',');
		var columnID   = columnInfo[0];
		var Visibility = columnInfo[1].toUpperCase();
		var Disable    = columnInfo[2].toUpperCase();
		var Caption    = columnInfo[3];
		var defValue   = columnInfo[4];
		var mandatory  = columnInfo[5].toUpperCase();

		var objField   = document.getElementById(columnID);
		
		if(mandatory == "Y")
		{
			if(objField != null)
			{
				if(Visibility == "Y" || objField.style.visibility == 'visible') 
				{
					if(Disable == "" || Disable == "N"  || objField.disabled==false)
					{
						if(objField.value == null || Trim(objField.value) == "")
						{
							alert(Caption + " is mandatory.");
							validateMandatory = false;
						}
					}
				}
			}
		}//End of Mandatory Check
	}//// End of for
	
	return validateMandatory;
}*/



/*
function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}
function ltrim(stringToTrim) {
	return stringToTrim.replace(/^\s+/,"");
}
function rtrim(stringToTrim) {
	return stringToTrim.replace(/\s+$/,"");
}

function ltrim(str) { 
	for(var k = 0; k < str.length && isWhitespace(str.charAt(k)); k++);
	return str.substring(k, str.length);
}
function rtrim(str) {
	for(var j=str.length-1; j>=0 && isWhitespace(str.charAt(j)) ; j--) ;
	return str.substring(0,j+1);
}
function trim(str) {
	return ltrim(rtrim(str));
}
function isWhitespace(charToCheck) {
	var whitespaceChars = " \t\n\r\f";
	return (whitespaceChars.indexOf(charToCheck) != -1);
}
*/