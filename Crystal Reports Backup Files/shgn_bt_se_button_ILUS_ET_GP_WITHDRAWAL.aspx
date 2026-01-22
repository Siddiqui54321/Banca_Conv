<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_WITHDRAWAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_WITHDRAWAL" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
<%
	Response.Write(ace.Ace_General.LoadPageStyle());
%>
</head>
<body >
<table width=100% >
	<tr>
		<td align=right>
			<!--<a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/add_2.gif'" onmouseout="this.src='../shmalib/images/buttons/add.gif'" border="0" name="btnadd" alt="" src="../shmalib/images/buttons/add.gif" onclick="parent.frames[1].addClicked()"></a>-->
			
			<!--<a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/save_2.gif'" onmouseout="this.src='../shmalib/images/buttons/save.gif'" border="0" name="btnsave" alt="" src="../shmalib/images/buttons/save.gif" onclick="parent.frames[1].saveUpdateClicked()"></a>-->
			<!--<a href="#" id="btnsave" name="btnsave" class="button2" onclick="parent.frames[1].saveUpdateClicked()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>-->
			<a href="#" id="btnsave" name="btnsave" class="button2" onclick="saveUpdate()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
			
			<!--<a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/delete_2.gif'" onmouseout="this.src='../shmalib/images/buttons/delete.gif'" border="0" name="btndelete" alt="" src="../shmalib/images/buttons/delete.gif" onclick="parent.frames[1].deleted();"></a>-->
			<!--<IMG alt="" src="../shmalib/images/buttons/add.gif" onclick="parent.frames[1].addClicked()">
			<IMG alt="" src="../shmalib/images/buttons/save.gif" onclick="parent.frames[1].saveUpdateClicked()">
			<!--<IMG alt="" src="../shmalib/images/buttons/update.gif" onclick="parent.frames[1].updateClicked()">--
			<IMG alt="" src="../shmalib/images/buttons/delete.gif" onclick="parent.frames[1].deleted();">-->
		</td>
	</tr>
</table>
</body>
</html>

<script language="javascript">	
	function saveUpdate()
	{
		parent.openWait('saving data');	
		parent.frames[1].saveUpdateClicked()
	}
</script>