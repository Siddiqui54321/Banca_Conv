<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_LF_LNFUFUNDS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_LF_LNFUFUNDS" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
<%
	Response.Write(ace.Ace_General.LoadPageStyle());
%>
</head>
<body >
<input type=hidden id='lfid' name='lfid' value=0>
<input type=hidden id='targetEntity' name='targetEntity' value=>
<table width=100% >
	<tr>
		<td align=right>
			<!-- <a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/save_2.gif'" onmouseout="this.src='../shmalib/images/buttons/save.gif'" border="0" name="btnsave" alt="" src="../shmalib/images/buttons/save.gif" onclick="parent.frames[0].send(this,1);"></a> -->
			<a href="#" id="btnsave" name="btnsave" class="button2" onclick="parent.frames[0].send(this,1);">Save</a>
		</td>
	</tr>
</table>


</body>
</html>


