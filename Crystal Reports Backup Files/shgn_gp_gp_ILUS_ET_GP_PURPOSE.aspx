<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_PURPOSE.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_PURPOSE" %>

<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>


<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; POSITION: absolute; TOP: 300px; HEIGHT: 25px">
        Please wait ... {0}
</DIV>

<!--Khalid introduced section for user response message section end-->

<head>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
</head>


<html>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="120" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="no">
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="200px" name="shgn_ts_se_tblscreen_ILUS_ET_NM_PURPOSE" src="../Presentation/shgn_ts_se_tblscreen_ILUS_ET_NM_PURPOSE.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto">
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="shgn_bt_se_button_ILUS_ET_GP_PURPOSE.aspx" src="../Presentation/shgn_bt_se_button_ILUS_ET_GP_PURPOSE.aspx?lfid=1&<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="no">								

</html>

<script language=javascript>

	document.getElementById('shgn_gp_gp_ILUS_ET_GP_PURPOSE').style.height = '365.0px'

</script>
