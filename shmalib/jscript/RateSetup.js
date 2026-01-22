
function calculateRate()
{
	
	var objFileName = getField("FILENAME");
	var objSheet = getField("SHEET");
	var objCellFrom = getField("LOCFROM");
	var objCellTo = getField("LOCTO");
	
	var strFileName = objFileName.value;
	var strSheet = objSheet.value;
	var strCellFrom = objCellFrom.value;
	var strCellTo = objCellTo.value;

	if(objFileName == null || strFileName=="")
	{
		//objFileName.focus();
		alert("File Name cannot be empty.");
		return false;
	}

	if(objSheet == null || strSheet=="")
	{
		//objFileName.focus();
		alert("Sheet Name cannot be empty.");
		return false;
	}

	if(objCellFrom == null || strCellFrom=="")
	{
		alert("Location From cannot be empty.");
		///objCellFrom.focus();
		return false;
	}
	
	if(objCellTo == null || strCellTo=="")
	{
		alert("Location To cannot be empty.");
		//objCellTo.focus();
		return false;
	}
	
	executeProcess('ace.CalculateRate');
	return true;	
	

}