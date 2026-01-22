<%@ Page language="c#" Codebehind="MedicalDetail_Button.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.MedicalDetail_Button" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		

<!-- <LINK href="Styles/Style.css" type="text/css" rel="stylesheet"> -->
<%
	Response.Write(ace.Ace_General.loadInnerStyle());
%>

		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
</head>
<body >
<br>
<table width=100% >
<tr>
		<td class="button2TD" align=right>
			
			<a href="#" class="button2" onclick="callUpdate();">&nbsp;&nbsp;Save&nbsp;&nbsp;</a>
		
		</td>
		
		
	</tr>
</table>

</body>
</html>

<script language="javascript">

	function callUpdate()
	{
		if (parent.frames[0].Page_ClientValidate())
		{
			parent.openWait('Saving Data');
			parent.frames[0].saveUpdate();
			//saveUpdate();
			//parent.closeWait();
		}
		
	}
	
	function UnderWritee()
	{
		if (parent.frames[0].Page_ClientValidate())
		{
			parent.openWait('Saving Data');
			parent.frames[0].UnderWriteUpdate();
			//saveUpdate();
			//parent.closeWait();
		}
		
	}
	
	/*function saveUpdate()
	{
		if (parent.frames[0]._lastEvent=='Edit' || parent.frames[1]._lastEvent=='Update')
		{
			parent.frames[0].updateClicked();
		}
		else if (parent.frames[0]._lastEvent=='New')
		{
			parent.frames[1].saveClicked();
		}
		//parent.openWait('saving data');
	}*/




</script>
