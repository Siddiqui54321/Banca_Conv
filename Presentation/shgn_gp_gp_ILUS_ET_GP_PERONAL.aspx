<%@ Page CodeBehind="shgn_gp_gp_ILUS_ET_GP_PERONAL.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gp_gp_ILUS_ET_GP_PERONAL" %>

<!--Khalid introduced section for user response message -->
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>




<!--Khalid introduced section for user response message section end-->

<head>
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
        <SCRIPT>
            window.onload = function () {
                window.history.forward();
            }
            function noBack() {
                window.history.forward();
            }
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

        </SCRIPT>
</head>
<html>
 <body onkeydown="return cancelBack(0)">
 
<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; WIDTH: 97%; visibility: hidden; POSITION: absolute; TOP: 90px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>

     <asp:Panel ID="PnlEntryForm" runat="server">

	<frameset>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="150" id="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET" src="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="310" id="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET" src="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="210" id="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS" name="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS" src="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto;display:none;"  scrolling="No"></iframe>
		<iframe height="0"  name='_blank' src="../Presentation/jsController.aspx" style='mso-linked-frame:auto; display:none;' scrolling="no"></iframe>
		<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="30" id="shgn_bt_se_button_MI_ET_GP_ProposalEntry.aspx" name="shgn_bt_se_button_MI_ET_GP_ProposalEntry.aspx" src="shgn_bt_se_button_ILUS_ET_GP_PERONAL.aspx?lfid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="No"></iframe>
        </frameset>

         </asp:Panel>
    </body> 
	
</html>

