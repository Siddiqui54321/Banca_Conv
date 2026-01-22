<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_SutabilityAssessment.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL" %>
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
	<iframe ALIGN="top" id="SuitabilityAssessment_Page.aspx" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="540" name="SuitabilityAssessment_Page.aspx" src="../Presentation/SuitabilityAssessment_Page.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>
    <iframe ALIGN="top" id="Assessment_Button.aspx" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="Assessment_Button.aspx" src="../Presentation/Assessment_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>
     </BODY>
</html>

