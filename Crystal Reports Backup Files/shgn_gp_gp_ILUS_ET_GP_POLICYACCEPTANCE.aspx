<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE" %>


<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; POSITION: absolute; TOP: 190px; HEIGHT: 25px">
        Please wait ... {0}
</DIV>


<head>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
</head>

<html>
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="30" name="PolicyAcceptance" src="../Presentation/PolicyAcceptance.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="Yes">
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="80" name="PolicyAcceptance_Button.aspx" src="../Presentation/PolicyAcceptance_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">

</html>

