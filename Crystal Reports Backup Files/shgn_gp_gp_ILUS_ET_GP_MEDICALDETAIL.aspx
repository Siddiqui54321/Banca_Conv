<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>


<head>
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>

<html>
	<iframe ALIGN="top" id="questFrame" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="500" name="MedicalDetail" src="../Presentation/MedicalDetail_page.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes">
	<!--<iframe ALIGN="top" id="questFrame" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="300" name="MedicalDetail" src="../Presentation/MedicalDetail.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto" >-->
	<BODY>
	<form>
	<br>
	<br>
	<br>
	</form>
	</BODY>
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="MedicalDetail_Button.aspx" src="../Presentation/MedicalDetail_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">
</html>

