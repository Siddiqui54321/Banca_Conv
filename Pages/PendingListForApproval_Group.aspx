<%@ Page language="c#" Codebehind="PendingListForApproval_Group.aspx.cs" AutoEventWireup="True" EnableEventValidation="false"
Inherits="Bancassurance.Presentation.PendingListForApproval_Group" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>


<head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>

<html>
	<iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="280" name="ProposalApproval" src="../Presentation/PendingListForApproval.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes">
	
	<BODY>
	<form>
	<br>
	</form>
	</BODY>
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="PendingListForApproval_Button.aspx" src="../Presentation/PendingListForApproval_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">
</html>
