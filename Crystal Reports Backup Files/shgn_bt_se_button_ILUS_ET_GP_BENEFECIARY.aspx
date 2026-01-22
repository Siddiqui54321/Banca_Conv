<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_BENEFECIARY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_BENEFECIARY" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html>
<head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
<%
	Response.Write(ace.Ace_General.LoadPageStyle());
%>

</head>
<body >
<table width=100% >
	<tr>
		<td >&nbsp;</td>
	</tr>
	<tr>
		<td class="button2TD" align=right>
			<a href="#" class="button2" onclick="refreshNameField();parent.frames[1].addClicked();">Add New</a>
			<a href="#" class="button2" onclick="updated();">&nbsp;&nbsp;Save&nbsp;&nbsp;</a>
			<a href="#" class="button2" onclick="deleted();">&nbsp;&nbsp;Delete&nbsp;&nbsp;</a>
		</td>
	</tr>
</table>

<script language=javascript>

function deleted()
{
	refreshNameField();
	if(parent.frames[1].callEvent('Delete','', '')==true)
	{
		//parent.openWait('deleting data');
	}
}

function updated()
{
	refreshNameField();
	if(validateInputs()==true)
	{
		//alert('true');
		//parent.frames[1].callSend();
		//alert('send');
		parent.frames[1].send();
		parent.openWait('saving data');
	}
	else
	{
		return false
	}	
}

function validateInputs()
{
	var BY_PERCENT="02";
	var index=1;
	var totRecords = parent.frames[1].totalRecords;
	var blnPercentExist = false;
	var totPercent = 0;

	for(index=1; index<=totRecords; index++)
	{
		if(parent.frames[1].getTabularFieldByIndex(index,"NBF_BENNAME") != null )
		{
			if(parent.frames[1].getTabularFieldByIndex(index,"NBF_BASIS").value == BY_PERCENT)
			{
				blnPercentExist = true;	
				totPercent += parseFloat(parent.frames[1].getTabularFieldByIndex(index,"NBF_PERCNTAGE").value);
			}
		}
	}
	
	if(blnPercentExist == true)
	{
		if(totPercent == 100)
		{
			return true;
		}
		else if(totPercent > 100)
		{
			alert("Total Percentage is exceeding from 100%");
			return false;
		}
		else if(totPercent < 100 && totPercent>0)
		{
			alert("Total Percentage must be 100%");
			return false;
		}
		else
		{
			alert("Total Percentage must be 100%");
			return false;			
		}		
	}
}


		function refreshNameField()
		{
		
		    for (var i=1; i<=parent.frames[1].totalRecords; i++)
			{
				if(parent.frames[1].getTabularFieldByIndex(i,"NGU_NAME") != null)
				{
					var row = parseInt(i) + 1;
					parent.frames[1].getTabularFieldByIndex(i,"NGU_NAME").value = "";
				}
			}
		}

</script>


</body>
</html>

