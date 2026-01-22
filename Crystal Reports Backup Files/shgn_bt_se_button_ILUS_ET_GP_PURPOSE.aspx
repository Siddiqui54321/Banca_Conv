<%@	Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_PURPOSE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_PURPOSE" %>
<%@	Register TagPrefix="CV"	Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
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
		<!--<IMG alt="" src="../shmalib/images/buttons/save.gif" onclick="processPurposes();">-->
			<a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/save_2.gif'" onmouseout="this.src='../shmalib/images/buttons/save.gif'" border="0" name="btnsave" alt="" src="../shmalib/images/buttons/save.gif" onclick="processPurposes();"></a>
		
		</td>
	</tr>
</table>

<script lang=javascript>
	
	function processPurposes()
	{
		parent.openWait('saving data');
		parent.frames[1].executeProcess('ace.PurposeProtection');
	}


</script>

</body>
</html>


