<%@ Page language="c#" Codebehind="Calculator.aspx.cs" AutoEventWireup="True" Inherits="ACE.Presentation.Calculator" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Calculator</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" language="javascript">
		
		function cmd_Exit_Click()
		{
			history.back(-1);
			window.close();
		}
			
		</script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body onblur="self.focus()" bgColor="#d4deea" ms_positioning="GridLayout">
		<form id="myForm" method="post" runat="server">
			<DIV class="NormalEntryTableDiv" id="CalculatorTableDiv" ms_positioning="FlowLayout">
				<FIELDSET style="BORDER-RIGHT: #d7e4f2 1px solid; BORDER-TOP: #d7e4f2 1px solid; BORDER-LEFT: #d7e4f2 1px solid; WIDTH: 350px; BORDER-BOTTOM: #d7e4f2 1px solid; HEIGHT: 88px"><LEGEND>Calculator</LEGEND>
					<TABLE id="entryTable" height="60%" cellSpacing="0" cellPadding="2" width="100%" border="0">
						<TR id="rowTargetPremium" class="TRow_Normal">
							<td id="TDtxtTargetPremium" align="right" width="33%">Target Premium
							</td>
							<td width="67%">
								<asp:textbox id="txtTargetPremium" tabIndex="1" runat="server" Width="184px" MaxLength="15" BaseType="Character"
									BackColor="White"></asp:textbox>
							</td>
						</TR>
						<TR id="rowFaceAmount" class="TRow_Alt">
							<td align="right" width="33%">Face Amount
							</td>
							<td width="67%"><asp:textbox id="txtFaceAmount" tabIndex="2" runat="server" Width="184px" MaxLength="15" BaseType="Character"
									ReadOnly="True" ForeColor="Teal" Enabled="False" BackColor="White" CssClass="DisplayOnly"></asp:textbox></td>
						</TR>
					</TABLE>
					<asp:Label id="Message" runat="server" Width="320px" ForeColor="Red"></asp:Label>
				</FIELDSET>
			</DIV>
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 128px; WIDTH: 184px; POSITION: absolute; TOP: 120px; HEIGHT: 32px"
				cellSpacing="2" cellPadding="1" border="0">
				<TR>
					<TD>
						<a href="#">
							<asp:ImageButton id="cmd_Calculate" accessKey="C" runat="server" onmouseover="this.src='../shmalib/images/buttons/calculate_2.gif'"
								onmouseout="this.src='../shmalib/images/buttons/calculate.gif'" border="0" name="btncalculate"
								alt="" src="../shmalib/images/buttons/calculate.gif" onclick="cmd_Calculate_Click" /></a>
					<TD>
						<a href="#">
							<asp:ImageButton id="cmd_Exit" accessKey="X" runat="server" onmouseover="this.src='../shmalib/images/buttons/exit_2.gif'"
								onmouseout="this.src='../shmalib/images/buttons/exit.gif'" border="0" name="btnexit" alt="" src="../shmalib/images/buttons/exit.gif" /></a></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			document.myForm.txtTargetPremium.focus();
		</script>
	</body>
</HTML>
