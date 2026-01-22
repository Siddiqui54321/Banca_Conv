<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_WITHDRAWALPOPS.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_WITHDRAWALPOPS" %>

<HEAD><TITLE>Partial Withdrawals (Maximized)</TITLE></HEAD>

<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; POSITION: absolute; TOP: 410px; HEIGHT: 25px">
        Please wait ... {0}
</DIV>

<!--Khalid introduced section for user response message section end-->

<head>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
</head>
<html>
		<iframe ALIGN="top" FRAMEBORDER="2" MARGINWIDTH="0" width="100%" height="0%" name="" src="" style="mso-linked-frame:auto"  scrolling="auto">
		<iframe ALIGN="top" FRAMEBORDER="2" MARGINWIDTH="0" width="100%" height="90%" name="shgn_ts_se_tblscreen_ILUS_ET_TB_VERTICALWITHDRAWAL" src="../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_VERTICALWITHDRAWAL.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto">
		<iframe ALIGN="top" FRAMEBORDER="2" MARGINWIDTH="0" width="100%" height="10%" name="shgn_bt_se_button_ILUS_ET_GP_WITHDRAWAL.aspx" src="../Presentation/shgn_bt_se_button_ILUS_ET_GP_WITHDRAWALPOPS.aspx?lfid=1&<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="no">								

</html>
