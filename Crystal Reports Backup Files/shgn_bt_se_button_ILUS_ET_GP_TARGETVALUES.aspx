<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_TARGETVALUES.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_TARGETVALUES" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>
<body >
<table width=100%>
	<tr>
		<td align=right>
<!--		<IMG alt="" src="../shmalib/images/buttons/add.gif" onclick="parent.frames[1].addClicked()">
		<IMG alt="" src="../shmalib/images/buttons/save.gif" onclick="parent.frames[1].saveClicked()">
		<IMG alt="" src="../shmalib/images/buttons/delete.gif" onclick="parent.frames[1].deleteClicked()">
-->

		    <!--			
			<a href="#" ><IMG id="Compute" onmouseover="this.src='../shmalib/images/buttons/generate_2.gif'" onmouseout="this.src='../shmalib/images/buttons/generate.gif'" border="0" name="btngenerate" alt="" src="../shmalib/images/buttons/generate.gif" onclick="parent.frames[1]._lastEvent='Edit';openWait();parent.frames[1].executeProcess('ace.GenerateOffers');"></a>
			<a href="#" ><IMG onmouseover="this.src='../shmalib/images/buttons/update_2.gif'" onmouseout="this.src='../shmalib/images/buttons/update.gif'" border="0" name="btnupdate" alt="" src="../shmalib/images/buttons/update.gif" onclick="callSave();"></a>
		        <a href="#" ><IMG id="Recalculate" onmouseover="this.src='../shmalib/images/buttons/calculate_2.gif'" onmouseout="this.src='../shmalib/images/buttons/calculate.gif'" border="0" name="btndelete" alt="" src="../shmalib/images/buttons/calculate.gif" onclick="parent.frames[1].executeProcess('shma.erp.hr.SHHR_AverageCalculator');"></a> 
		    -->
		        <a href="#" ><IMG src="Theme_Illus/Images/update.gif"   onmouseover="this.src='Theme_Illus/Images/update_2.gif'"   onmouseout="this.src='Theme_Illus/Images/update.gif'"   border="0" name="btnupdate"   onclick="callSave();" alt="" ></a>
				<a href="#" ><IMG src="Theme_Illus/Images/generate.gif" onmouseover="this.src='Theme_Illus/Images/generate_2.gif'" onmouseout="this.src='Theme_Illus/Images/generate.gif'" border="0" name="btngenerate" onclick="parent.frames[1]._lastEvent='Edit';openWait();" id="Compute" alt="" ></a>

		<!--<IMG id="Compute" alt="" src="../shmalib/images/buttons/generate.gif" onclick="parent.frames[1]._lastEvent='Edit';openWait();parent.frames[1].executeProcess('ace.GenerateOffers');">
		<IMG alt="" src="../shmalib/images/buttons/update.gif"  onclick="callSave();">
		<IMG id="Recalculate" alt="" src="../shmalib/images/buttons/calculate.gif" onclick="parent.frames[1].executeProcess('shma.erp.hr.SHHR_AverageCalculator');">-->
		</td>
	</tr>
</table>


</body>
<script language="javascript">

	function callSave()
	{
		parent.frames[1]._lastEvent='Edit'; 
		if (parent.frames[1].Page_ClientValidate()==true)
		{
			parent.openWait('saving data');
			//onclick="parent.frames[1].callEvent('Update','', '');"
			//callEvent('Update','', '')
				//alert(parent.frames[1]._lastEvent);
				//_lastEvent='Update';
				parent.frames[1].callEvent('Update','', '');
				
				//if (parent.frames[1]._lastEvent=='Edit' || parent.frames[1]._lastEvent=='Update')
				//	parent.frames[1].updateClicked();
				//else if (parent.frames[1]._lastEvent=='New')
				//	parent.frames[1].saveClicked();
		}
	}
	
	function openWait()
	{
		
		if (confirm("Plase use COMPUTE button if you want to check target values on different age or return percent year. " +
		"Do you want to proceed to Generation of targe vales?")==false)
			return;			

		if (parent.frames[1]._lastEvent=='Edit' && parent.frames[1].Page_ClientValidate())
		{
			parent.openWait('Generation of target values is in progress');
			parent.frames[1].executeProcess('ace.GenerateOffers');
		}
	}

</script>
</html>

