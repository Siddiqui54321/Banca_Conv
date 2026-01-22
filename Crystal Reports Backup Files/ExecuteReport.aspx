<%@ Page language="c#" Codebehind="ExecuteReport.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.ExecuteReport" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>ExecuteReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
		<script lang="javascript">
		function Channel_ChangeEvent(objChannel)
		{ 
			
			var str_QryCCD_CODE="SELECT D.CCD_CODE "+getConcateOperator()+" '-' "+getConcateOperator()+" CCD_DESCR,D.CCD_CODE  FROM CCD_CHANNELDETAIL D, CCH_CHANNEL C WHERE D.CCH_CODE=C.CCH_CODE AND C.CCH_CODE='" + objChannel.value + "'";
			fcfilterChildCombo(objChannel,str_QryCCD_CODE, getField("CCD_CODE_1").name);
		}
		
		function ChannelDetail_ChangeEvent()
		{
		}
		</script>
		
		<style type="text/css">
		.form_heading { FONT-WEIGHT: bold; FONT-SIZE: 15px; VERTICAL-ALIGN: middle; COLOR: #ffffff; PADDING-TOP: 6px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #336699; TEXT-ALIGN: left }
		</style>
</HEAD>
	<body MS_POSITIONING="GridLayout" style="FONT-SIZE:10px;FILTER:progid:DXImageTransform.Microsoft.Gradient(endColorstr='#C0CFE2', startColorstr='#FFFFFF', gradientType='0');COLOR:#666666;FONT-FAMILY:Verdana, Arial, Helvetica, sans-serif">
		<TABLE id="heading" border="0" cellSpacing="0" cellPadding="2" width="100%">
			<tr class="form_heading">
				<td valign="middle" height="20" colSpan="6"><asp:Literal id="ReportHeading" runat="server" EnableViewState="False"></asp:Literal></td>
			</tr>
		</TABLE>
		<form id="Form1" method="post" runat="server">
			<TABLE id="errorDesc" border="0" cellSpacing="0" cellPadding="2" width="100%">
				<tr>
					<td valign="middle" height="20" colSpan="6"><asp:Literal id="ReportError" runat="server" EnableViewState="False"></asp:Literal></td>
				</tr>
			</TABLE>
			<div id="NormalEntryTableDiv" style="Z-INDEX: 101" runat="server">
				
				<asp:Panel ID="pnlCommonInput" Runat="server" >
<TABLE id=entryTable cellSpacing=0 cellPadding=0 border=0>
<asp:Panel id=pnlSecurityLog Runat="server">
  <TBODY>
  <TR>
    <TD width="20%">Report Type</TD>
    <TD>
<SHMA:dropdownlist class=DropDownField id=ddlREPORT runat="server" CssClass="REQUIREDFIELD" Width="6pc">
									<asp:ListItem Value="LOGINLOG">Login Log</asp:ListItem>
									<asp:ListItem Value="ACTIVITYLOG">Activity Log</asp:ListItem>
								</SHMA:dropdownlist></TD>
    <TD width="20%"></TD>
    <TD></TD></TR>
  <TR class=TRow_Alt id=row2>
    <TD width="20%">Channel &nbsp;</TD>
    <TD>
<SHMA:dropdownlist class=DropDownField id=ddlCCH_CODE_1 runat="server" CssClass="REQUIREDFIELD" Width="12.0pc" BlankValue="True" DataTextField="desc_f" DataValueField="CCH_CODE"></SHMA:dropdownlist></TD>
    <TD width="20%">Channel Detail&nbsp;</TD>
    <TD>
<SHMA:dropdownlist class=DropDownField id=ddlCCD_CODE_1 runat="server" CssClass="REQUIREDFIELD" Width="12.0pc" BlankValue="True" DataTextField="desc_f" DataValueField="CCD_CODE"></SHMA:dropdownlist></TD></TR></asp:Panel>
  <TR class=TRow_Alt id=row3>
    <TD width="20%">Date From</TD>
    <TD noWrap>
<SHMA:DatePopUp id=txtDATEFROM tabIndex=2 runat="server" Width="5.0pc" ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="10"></SHMA:DatePopUp>
<asp:CompareValidator id=cfvDATEFROM runat="server" Display="Dynamic" ErrorMessage="Date Format is Incorrect " Type="Date" Operator="DataTypeCheck" ControlToValidate="txtDATEFROM"></asp:CompareValidator></TD>
    <TD width="20%">Date To</TD>
    <TD noWrap>
<SHMA:DatePopUp id=txtDATETO tabIndex=5 runat="server" Width="5.0pc" ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="10"></SHMA:DatePopUp>
<asp:CompareValidator id=cfvDATETO runat="server" Display="Dynamic" ErrorMessage="Date Format is Incorrect " Type="Date" Operator="DataTypeCheck" ControlToValidate="txtDATETO"></asp:CompareValidator></TD></TR>
  <TR class=TRow_Normal id=row4>
    <TD align=center colSpan=4>
<asp:Button id=btnGenerate runat="server" CssClass="" Width="48px" Text="Continuee" onclick="btnGenerate_Click"></asp:Button></TD></TR></TBODY></TABLE>
				</asp:Panel>

			</div>
		</form>
		
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 
			
		</script>
		
	</body>
</HTML>
