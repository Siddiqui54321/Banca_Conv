<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_TARGETVALUES.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_TARGETVALUES" %>


<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; POSITION: absolute; TOP: 530px; HEIGHT: 25px">
        Please wait ... {0}
</DIV>

<!--Khalid introduced section for user response message section end-->


<HTML>
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
		<iframe ALIGN="top"  FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="130" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
		<iframe ALIGN="top"  FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="324" name="shgn_ss_se_stdscreen_ILUS_ET_NM_TARGETVALUES" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_TARGETVALUES.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto"></iframe>
		<iframe ALIGN="top"  FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="135" name="shgn_dh_se_enquiry_ILUS_ET_EQ_TARGETVALUES" src="../Presentation/shgn_dh_se_enquiry_ILUS_ET_EQ_TARGETVALUES.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="auto"></iframe>
		<iframe ALIGN="top"  FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="shgn_bt_se_button_ILUS_ET_GP_TARGETVALUES" src="../Presentation/shgn_bt_se_button_ILUS_ET_GP_TARGETVALUES.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No">	</iframe>							
</body>
</html>
