<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_PLANRIDER.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_PLANRIDER" %>


<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="display:none; LEFT: 10px; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 120px; HEIGHT: 22px">
        Please wait ... {0}
</DIV>

<!--Khalid introduced section for user response message section end-->

<head>
	<%Response.Write(ace.Ace_General.LoadPageStyle());%>
</head>

<html>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="115" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="190" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="shgn_bt_se_button_ILUS_ET_GP_PLANRIDER.aspx" src="../Presentation/shgn_bt_se_button_ILUS_ET_GP_PLANRIDER.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">
</html>

