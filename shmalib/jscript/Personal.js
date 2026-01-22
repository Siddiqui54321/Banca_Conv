//deprecated to put on save and update both on same button
var gResetPremium="N";
function updatePersonalDetails()
{
	
	if (validatePages(2))
	{
		parent.frames[0].getField("NP2_COMMENDATE").disabled=false;
		parent.frames[0].getField("NP1_PROPDATE").disabled=false;
		parent.frames[0].getField("CCN_CTRYCD").disabled=false;
		parent.frames[0].getField("USE_USERID").disabled=false;
		parent.frames[0].saveClicked();
		//parent.frames[1].updateClicked();
	}
}

function addPersonalDetails()
{
	parent.frames[0].addClicked();
	parent.frames[1].addClicked();
	parent.frames[2].addClicked();
	//parent.frames[3].addClicked();
}

function validateBeforeSave()
{
	//Check 1st life Input
	if(parent.frames[1].checkMandatoryColumns() == true)
	{
		if(parent.frames[1].getField("NPH_INSUREDTYPE1").value == "N")
		{
			return parent.frames[2].checkMandatoryColumns();
		}
		else
		{
			return true;
		}
		return true;
	}
	else
	{
		return false;
	}
	//return parent.frames[1].checkMandatoryColumns();
}

function savePersonalDetails()
{
	if(checkDetailInfo()==false)
	{
		return false;
	}
	
	if(validateBeforeSave() == false)
	{
		return false;
	}
	
	var validNo = 2;
	
	//if equals 3 then validate all 
	if (parent.frames[1].document.getElementById('ddlNPH_INSUREDTYPE1').value == 'N')
		validNo = 3;
	
	
	
	//alert(validNo);
	if (validatePages(validNo))
	{
		parent.frames[0].getField("NP2_COMMENDATE").disabled=false;
		parent.frames[0].getField("NP1_PROPDATE").disabled=false;
		parent.frames[0].getField("CCN_CTRYCD").disabled=false;
		parent.frames[0].getField("USE_USERID").disabled=false;		
		setFixedValuesInSession("FLAG_RESET_PREMIUM=" + gResetPremium);
		parent.frames[0].saveClicked();
		parent.frames[0].updateClicked();
		parent.openWait('saving data');
	}

	//alert(parent.frames[0].Page_ClientValidate());
	//parent.frames[0].Page_ClientValidate();
}


function deletePersonalDetails()
{
	if(parent.frames[0].deleteClicked()==true)
	{
		parent.openWait('deleting data');
	}
}



function validatePages(noOfPages)
{
	var validated = false;
	
	for (i=0; i<noOfPages; i++)
	{
		validated = parent.frames[i].Page_ClientValidate();
		if(!validated)
			return false;
	}
	return validated;
}


function callControllerLogic(eventType, deleteType)
{
	
	
	//parent.frames[1].saveClicked();
	
	//alert('Caller event: ' + eventType);
	//if second screen is open
	var ismodel2 = (parent.frames[1].document.getElementById('ddlNPH_INSUREDTYPE1').value=='Y'?false:true);

	var total_screens = (ismodel2==true?3:2);
	
	//alert('sibling screens ' +total_screens);
	for (i=1; i<total_screens; i++)
	{
		//alert('js controller logic called: page event: ' + parent.frames[1]._lastEvent + ', caller event: ' + eventType);



		//alert('Frame['+i+']: '+parent.frames[i]._lastEvent);
		
		if (parent.frames[i]._lastEvent=='New')
			parent.frames[i].saveClicked();
		if (parent.frames[i]._lastEvent=='Edit')
			parent.frames[i].updateClicked();
	}
	
	
}

function deleteLogic(deleted)
{

	//if deleted then refresh sibling screens
	//alert(deleted);
	if (deleted=='Y')
	{
		//alert('deleted refresh');
		parent.frames[1].location = parent.frames[1].location;
		parent.frames[2].location = parent.frames[2].location;
	}
}	

function checkDetailInfo()
{
	//if(parent.frames[0].Proposal_LostFocus(parent.frames[0].myForm.txtNP1_PROPOSAL) == false) 

	var flag = parent.frames[0].Proposal_LostFocus(parent.frames[0].myForm.txtNP1_PROPOSAL);
	if( flag == false) 
	{
		return false;
	}
	
	var approvedPolicy = parent.frames[0].PolicyApproved;
	var validate = parent.frames[0].validate;
	if(validate=='Y')
	{
		alert("Cannot Save validated proposal.");
		return false;
	}
	else if(approvedPolicy=='Y')
	{
		alert("Cannot Save approved proposal.");
		return false;
	}
 	var premExist = parent.frames[0].PremiumExist;
	
	
	if(premExist == 'Y')
	{
		/*
		if(confirm("Current Premium will be refresh. \n Continuee... ?"))
		{
			gResetPremium = "Y";
			return true;
		}
		else
		{
			gResetPremium = "N";
			return false;
		}
		*/
		gResetPremium = "Y";
		return true;
	 }
	
	
	else
	{
		return true;
	}
		
}