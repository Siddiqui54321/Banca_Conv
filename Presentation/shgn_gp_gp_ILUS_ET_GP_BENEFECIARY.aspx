<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_BENEFECIARY.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_BENEFECIARY" %>


<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 118px; HEIGHT: 22px">
        Please wait ... {0}
</DIV>

<!--Khalid introduced section for user response message section end-->
<html>
<head>
	<%Response.Write(ace.Ace_General.LoadPageStyle());%>
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
<body onkeydown="return cancelBack(0)">

		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="120" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="no"></iframe>
		<!--
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="96%" height="220px" name="shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY" src="../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto" ></iframe>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="60" name="shgn_bt_se_button_ILUS_ET_GP_BENEFECIARY.aspx" src="../Presentation/shgn_bt_se_button_ILUS_ET_GP_BENEFECIARY.aspx?lfid=1&<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="no"></iframe>
		-->
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="240px" name="shgn_ss_se_stdscreen_ILUS_BENEFECIARY" src="../Presentation/shgn_ss_se_stdscreen_ILUS_BENEFECIARY.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto" ></iframe>
    <div runat="server" id="assignee">	
    <iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="280px" name="shgn_ss_se_stdscreen_ILUS_ET_CD_COMPANYDET" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_CD_COMPANYDET.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto" ></iframe>  
    </div>
</body>
</html>

