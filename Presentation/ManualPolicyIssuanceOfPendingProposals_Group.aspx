<%@ Page language="c#" Codebehind="ManualPolicyIssuanceOfPendingProposals_Group.aspx.cs" AutoEventWireup="True"  EnableEventValidation="false"
Inherits="SHAB.Presentation.ManualPolicyIssuanceOfPendingProposals_Group" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>


<head>
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>

<html>
	<iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="ManualPolicy" src="../Presentation/ManualPolicyIssuanceOfPendingProposals.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="ManualPolicyIssuanceOfPendingProposals_Button.aspx" src="../Presentation/ManualPolicyIssuanceOfPendingProposals_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
</html>
