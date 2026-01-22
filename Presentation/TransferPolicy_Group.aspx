<%@ Page language="c#" Codebehind="TransferPolicy_Group.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.TransferPolicy_Group" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>


<head>
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>

<html>
	<iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="TransferPolicy" src="../Presentation/TransferPolicy.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="TransferPolicy_Button" src="../Presentation/TransferPolicy_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
</html>
