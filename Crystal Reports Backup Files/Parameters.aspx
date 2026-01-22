<%@ Page language="c#" Codebehind="Parameters.aspx.cs" AutoEventWireup="True" Inherits="ACE.Presentation.Parameters" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
		<title>Parameters</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body onblur="self.focus()" bgColor="#d4deea" ms_positioning="GridLayout">
		<form id="myForm" method="post" runat="server">
			<DIV class="NormalEntryTableDiv" id="CalculatorTableDiv" ms_positioning="FlowLayout">
				<FIELDSET style="BORDER-RIGHT: #d7e4f2 1px solid; BORDER-TOP: #d7e4f2 1px solid; LEFT: -8px; BORDER-LEFT: #d7e4f2 1px solid; WIDTH: 353px; BORDER-BOTTOM: #d7e4f2 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: 128px"><LEGEND>Parameters</LEGEND>
					<TABLE id="entryTable" height="60%" cellSpacing="0" cellPadding="2" border="0">
						<TR id="rowInterestSpreadPercent" class="TRow_Normal">
							<td align="right" width="33%">Interest Spread%
							</td>
							<td width="67%"><asp:textbox id="txtInterestSpreadPercent" tabIndex="1" runat="server" ForeColor="Black" style="TEXT-ALIGN: right"
									Enabled="False" BackColor="White" value="1.5" Width="184px" MaxLength="15" BaseType="Numeric"></asp:textbox></td>
						</TR>
						<TR class="TRow_Alt">
							<TD align="right" width="106" height="16">Exchange Rate Date &nbsp;</TD>
							<TD width="186" height="16"><SHMA:DATEPOPUP id="txtExRateDate" tabIndex="12" runat="server" Width="10.5pc" value="4/15/2008"
									maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg" BackColor="White"
									ReadOnly="True"></SHMA:DATEPOPUP></TD>
						</TR>
						<TR class="TRow_Normal">
							<TD align="right" width="106" height="16">Exchange Rate
							</TD>
							<TD width="186" height="16"><asp:textbox id="txtExchangeRate" style="TEXT-ALIGN: right" tabIndex="20" runat="server" Width="184px"
									Enabled="False" value="3.67" BackColor="White"></asp:textbox></TD>
						</TR>
					</TABLE>
				</FIELDSET>
			</DIV>
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" border="0" style="Z-INDEX: 102; LEFT: 120px; WIDTH: 18.31%; POSITION: absolute; TOP: 152px; HEIGHT: 34px">
				<TR>
					<TD>
						<a href="#" ><asp:ImageButton id="cmd_Exit" accessKey="X" runat="server" onmouseover="this.src='../shmalib/images/buttons/exit_2.gif'" onmouseout="this.src='../shmalib/images/buttons/exit.gif'" border="0" name="btnexit" alt="" src="../shmalib/images/buttons/exit.gif" /></a>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		//document.myForm.txtInterestSpreadPercent.focus();
		</script>
	</body>
</HTML>
