<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>

<html>
<head>
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
             <script type="text/javascript" language="javascript">

                 function cancelBack(val) {
                     if (val == 0) {
                         var key = event.keyCode;
                         if (key == 8) {
                             return false;
                         }
                         else {
                         }
                     }
                     else {
                     }

                 }

                 function backspaceFunc(e) {
                     var key = event.keyCode;
                     if (key == 8) {
                         var str = document.getElementById(e).value;
                         var newStr = str.substring(0, str.length - 1);
                         document.getElementById(e).value = newStr;
                     }
                 }
    </script>
</head>

	<BODY onkeydown="return cancelBack(0)">
	<iframe ALIGN="top" id="questFrame" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="500" name="MedicalDetail" src="../Presentation/MedicalDetail_page.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>
	<!--<iframe ALIGN="top" id="questFrame" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="300px" name="MedicalDetail" src="../Presentation/MedicalDetail.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto" >-->


	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="MedicalDetail_Button.aspx" src="../Presentation/MedicalDetail_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
	</BODY>
</html>

