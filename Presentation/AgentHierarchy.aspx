<%@ Page language="c#" Codebehind="AgentHierarchy.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.AgentHierarchy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>AgentHierarchy</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			
			function openOracleForm(path)
			{
				var wOpen;
				var sOptions;
				sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
				sOptions = sOptions + ',width=' + (screen.availWidth /2.15).toString();
				sOptions = sOptions + ',height=' + (screen.availHeight/2.7).toString();

				var aw = screen.availWidth - 10;
				var ah = screen.availHeight - 30;

				var xc = ( aw - (screen.availWidth /2.15) ) / 2;
				var yc = ( ah - (screen.availHeight/2.7) ) / 2;

				sOptions += ",left=" + xc + ",screenX=" + xc;
				sOptions += ",top=" + yc + ",screenY=" + yc;
				
				try
				{
					if(wOpen != null)
						if(false ==  wOpen.closed )
							wOpen.close();
					wOpen = window.open('', "wOpen", sOptions );
					//var str_target= "ExecuteReport.aspx?_Proposal=" + proposal + "&_ReportType=" + reportType;
					path = "http://www.google.com.pk";
					var str_target=path;
					wOpen.location = str_target;
					wOpen.focus();

				}
				catch(err)
				{
					//alert('Report printing error: ' + err.message);
				}
			}
		</script>
		
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr class="form_heading">
					<td height="20" colSpan="6">&nbsp; Agent Hierarchy</td>
				</tr>
			</table>
		
			<table align=center>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				<tr><td></td><td></td><td></td></tr>
				
				<tr>
					<td><a href="#" class="button2" onClick="openOracleForm(path + 'Form1');">Ilas Form1</a></td>
					<td><a href="#" class="button2" onClick="openOracleForm(path + 'Form2');">Ilas Form2</a></td>
					<td><a href="#" class="button2" onClick="openOracleForm(path + 'Form3');">Ilas Form3</a></td>
				</tr>
			</table>
			
		</form>
	</body>
</HTML>
