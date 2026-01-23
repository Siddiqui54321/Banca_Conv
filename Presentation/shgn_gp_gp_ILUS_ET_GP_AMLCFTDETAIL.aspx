<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shgn_gp_gp_ILUS_ET_GP_AMLCFTDETAIL.aspx.cs" Inherits="Bancassurance.Presentation.shgn_gp_gp_ILUS_ET_GP_AMLCFTDETAIL" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>
<!DOCTYPE html>
<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
<body>
	<iframe ALIGN="top" id="questFrame" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="500" name="AMLCFTDetail" src="../Presentation/AMLCFT_page.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes"></iframe>    
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="MedicalDetail_Button.aspx" src="../Presentation/AMLCFT_Button.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
</body>
</html>
<%--AMLCFT_page--%>
<%--MedicalDetail_page--%>