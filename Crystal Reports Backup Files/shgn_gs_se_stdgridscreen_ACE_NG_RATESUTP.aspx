<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_gs_se_stdgridscreen_ACE_NG_RATESUTP.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gs_se_stdgridscreen_ACE_NG_RATESUTP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/RateSetup.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
		if(_lastEvent != "View") 
			_lastEvent="View";

		<!-- <!--column-management-array--> -->
		
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<p class="EntityHeading">Rate Setup</ASP:LITERAL></p>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" style="Z-INDEX: 101" runat="server">
				<!-- <fieldset><legend>Entry</legend> -->
				<fieldset id="EntryFldSet" style="BORDER-BOTTOM: #dee8ca 3px solid; POSITION: absolute; BORDER-LEFT: #dee8ca 3px solid; WIDTH: 360px; HEIGHT: 170px; BORDER-TOP: #dee8ca 3px solid; TOP: 80px; BORDER-RIGHT: #dee8ca 3px solid; LEFT: 150px">
					<TABLE id="entryTable" style="POSITION: absolute; WIDTH: 340px; TOP: 5px; LEFT: 5px" cellSpacing="0"
						cellPadding="2" border="0">
						<TR id='row1' class="TRow_Normal">
							<TD align="right">File Name</TD>
							<TD nowrap><shma:TextBox id="txtFILENAME" class="DisplayOnly test" tabIndex="1" runat="server" Width='13.0pc'
									MaxLength="50" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvFILENAME" runat="server" ControlToValidate="txtFILENAME" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<td style='HEIGHT: 30px'></td>
							<td></td>
							<td style='HEIGHT: 30px'></td>
							<td></td>
						</TR>
						<TR id='row5' class="TRow_Alt">
							<TD align="right">Sheet Name</TD>
							<TD nowrap><shma:TextBox id="txtSHEET" class="DisplayOnly test" tabIndex="2" runat="server" Width='13.0pc'
									MaxLength="50" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvSHEET" runat="server" ControlToValidate="txtSHEET" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<td style='HEIGHT: 30px'></td>
							<td></td>
							<td style='HEIGHT: 30px'></td>
							<td></td>
						</TR>
						<TR id='row7' class="TRow_Normal">
							<TD align="right">Location From</TD>
							<TD nowrap><shma:TextBox id="txtLOCFROM" class="DisplayOnly test" tabIndex="3" runat="server" Width='5.0pc'
									MaxLength="10" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvLOCFROM" runat="server" ControlToValidate="txtLOCFROM" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<td style='HEIGHT: 30px'></td>
							<td></td>
							<td style='HEIGHT: 30px'></td>
							<td></td>
						</TR>
						<TR id='row10' class="TRow_Alt">
							<TD align="right">Location To</TD>
							<TD nowrap><shma:TextBox id="txtLOCTO" class="DisplayOnly test" tabIndex="4" runat="server" Width='5.0pc'
									MaxLength="10" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvLOCTO" runat="server" ControlToValidate="txtLOCTO" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<td style='HEIGHT: 30px'></td>
							<td></td>
							<td style='HEIGHT: 30px'></td>
							<td></td>
						</TR>
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
					</TABLE>
					<table width="100%" BORDER="0" style="POSITION: absolute; WIDTH: 340px; TOP: 128px; LEFT: 5px"
						cellSpacing="0">
						<tr>
							<td align="right">
								<a href="#"><IMG src="../shmalib/images/buttons/compute_1.gif" onmouseover="this.src='../shmalib/images/buttons/compute_2.gif'"
										onmouseout="this.src='../shmalib/images/buttons/compute_1.gif'" border="0" name="btnCompute"
										alt="" onclick="calculateRate();"></a>
							</td>
						</tr>
					</table>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" style="WIDTH: 0px" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" style="WIDTH: 0px" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="_CustomEventVal" style="WIDTH: 0px" name="_CustomEventVal" runat="server">
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<script language="javascript">
				
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
