<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_PERONAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_PERONAL" %>
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
	

		<td class="button2TD" align=right>
			<a href="#" class="button2" onclick="window.parent.location.reload();parent.frames[3].addClicked();">Add New</a>
			<a href="#" class="button2" onclick="save_()">&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;</a>
<!--
			<a href="#" class="button"><IMG onmouseover="this.src='../shmalib/images/buttons/add_2.gif'" onmouseout="this.src='../shmalib/images/buttons/add.gif'" border="0" name="btnadd" alt="" src="../shmalib/images/buttons/add.gif" onclick="parent.frames[3].addClicked()"></a>
			<a href="#" class="button"><IMG onmouseover="this.src='../shmalib/images/buttons/save_2.gif'" onmouseout="this.src='../shmalib/images/buttons/save.gif'" border="0" name="btnsave" alt="" src="../shmalib/images/buttons/save.gif" onclick="parent.frames[3].saveClicked()"></a>
-->		
		</td>
		
	</tr>
</table>


</body>
</html>

<script language="javascript">
	function save_()
	{	
		var annualIncomeObj = parent.frames[1].document.getElementById('txtNPH_ANNUINCOME');
		if(annualIncomeObj.style.visibility == 'visible')
		{
			if(annualIncomeObj.disabled == false)
			{
				if(annualIncomeObj.value == null || annualIncomeObj.value == "")
				{
					alert("Annual Income is mandatory.");
					annualIncomeObj.focus();
					return;
				}
			}
		}		
		parent.frames[3].saveClicked();		
	}
</script>