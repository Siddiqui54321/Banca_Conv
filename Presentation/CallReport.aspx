<%@ Page language="c#" Codebehind="CallReport.aspx.cs" AutoEventWireup="True" Inherits="Insurance.Presentation.CallReport" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Print</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language=JavaScript src='../shmalib/jscript/crystalparameter.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	
	<script language=javascript>
		var rpt_Name="Proposal"; //Name of report
		function selectReport(opt)
		{
			if (opt == 1) 
				rpt_Name = "Proposal";
			if (opt == 2) 
				rpt_Name = "Policy";
		}
	</script>
	
	<body MS_POSITIONING="GridLayout" oncontextmenu="return <%=System.Configuration.ConfigurationSettings.AppSettings["RightMouseKey"]%>;">
		<form id="myForm" method="post" runat="server">

			<table style="WIDTH: 480px; HEIGHT: 243px" cellSpacing="2">
				<tr>
					<td width="100%" colSpan="2"><font face="ARIAL" size="2"><b><i>Please select the document to 
									print</i></b></font>
					</td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;
								<input type="radio" name="rdoReport" value="1" checked onclick="selectReport(1);">
									<font color="black" style="Arial" checked>Proposal</font>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 294px">&nbsp;
								<input type="radio" name="rdoReport" value="2" onclick="selectReport(2);">
									<font color="black" style="Arial">Policy</font>
					</td>
				</tr>
				<tr>
				</tr>
				<tr>
					<td style="WIDTH: 294px">&nbsp;
					</td>
					<td>&nbsp;
					</td>
				</tr>
				<tr>
					<TD >&nbsp;From</TD>
					<TD >
						<shma:TextBox name="txtProposalNoFrom" id="txtProposalNoFrom" tabIndex="1" runat="server" Width='10.0pc' MaxLength="30" BaseType="Character" ></shma:TextBox>
					</TD>
					<td>&nbsp;
					</td>
				</tr>

				<tr>
					<TD>&nbsp;To</TD>
					<TD >
						<shma:TextBox name="txtProposalNoTo" id="txtProposalNoTo" tabIndex="2" runat="server" Width='10.0pc' MaxLength="30" BaseType="Character" ></shma:TextBox>
					</TD>
					<td>&nbsp;
					</td>
				</tr>

				<tr>
					<td></td>
					<td align="right">
					<Input type=button id="Button1" value="Print" onclick="GetValue()" style="WIDTH: 200px" class=button>
					</td>
				</tr>
			</table>







			
			
			
		</form>
	</body>
</HTML>
