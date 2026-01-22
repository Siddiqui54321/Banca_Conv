<%@ Page language="c#" Codebehind="TransferPolicy_Button.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.TransferPolicy_Button" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html>
	<head>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<%
			Response.Write(ace.Ace_General.loadInnerStyle());
		%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
	</head>
	<body>
		<br>
		<table width="100%">
			<tr>
				<td class="button2TD" align="right" id="td_transfer"> 
					<!--<a href="#" class="button2" onclick="checkboxval();">&nbsp;&nbsp;Mark Status&nbsp;&nbsp;</a>-->
					<a href="#" class="button2" id="btn_MarkStatus" onclick="CallGeneral();">&nbsp;&nbsp;Mark Status&nbsp;&nbsp;</a> 
				</td>
			</tr>
		</table>
	</body>
</html>

<script language="javascript">
    function ShowHideButton()
    {
        var val = parent.frames[0].GetDdl_VALAPP();
        if (val == "0") {
            document.getElementById('td_transfer').style.display = '';
            document.getElementById('td_decline').style.display = 'none';
        }
        else {
            document.getElementById('td_transfer').style.display = 'none';
            document.getElementById('td_decline').style.display = '';
        }
    }

    function CallGeneral() {
        var val = parent.frames[0].GetDdl_VALAPP();
        if (val == "0") {
            func_TransferPolicy();
        }
        else {
            func_DeclineProposal();
        }
    }
	function func_TransferPolicy(){
		parent.frames[0].func_TransferPolicy();
    }

	function func_DeclineProposal() {
	    parent.frames[0].func_DeclinePol();
	}
	function checkboxval(){
		parent.frames[0].checkboxval();		
	}
				
	function show_confirm()
	{
	var r=confirm("Make sure that this file is not downloaded before for payment deduction.. Continue Downloading?");
	if (r==true)
	{
	window.location.replace( "UploadedFiles/downloadProposalStatus.xls" );
	}
	else
	{
	//alert("You pressed Cancel!");
	}
	}
	function callUpdate()
	{ 
		if (parent.frames[0].Page_ClientValidate())
		{
			parent.openWait('Saving Data');
			parent.frames[0].saveUpdate();			
		}
		//DownloadFiles();
	}
	
	var isReload = false;
	function DownloadFiles()
	{
		if(isReload == true)
		{
			isReload = false;
			parent.frames['0'].window.location.reload();
		}
		else
		{
			isReload = true;
			window.setTimeout("DownloadFiles()", 2000);
		}
	}
	
	
</script>
