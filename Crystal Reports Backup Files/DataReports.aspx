<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="DataReports.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.DataReports" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Data DMPs</TITLE>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
	</HEAD>
	<BODY>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/JavaScript" src="../shmalib/jscript/Login.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<!-- <LINK rel="stylesheet" type="text/css" href="Styles/Style.css"> -->
		<%Response.Write(ace.Ace_General.loadInnerStyle());%>
		<asp:literal id="HeaderScript" EnableViewState="True" runat="server"></asp:literal>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Data DMPs
				</td>
			</tr>
		</table>
		<form id="myForm1" method="post" name="myForm1" runat="server">
			<div style="Z-INDEX: 101" id="NormalEntryTableDiv" runat="server">
				<P><LEGEND style="COLOR: #336692"></LEGEND></P>
				<TABLE id="entryTable" border="0" cellSpacing="5" cellPadding="1" width="100%">
					<TR id="row_BANK" class="TRow_Normal">
						<TD style="WIDTH: 194px">Bank:</TD>
						<TD><SHMA:dropdownlist id="ddlCCS_CODE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CCD_CODE"
								tabIndex="1" Width="248px" CssClass="RequiredField"></SHMA:dropdownlist></TD>
					</TR>
					<TR id="row_Date" class="TRow_Normal">
						<TD style="WIDTH: 194px">Date Type:</TD>
						<TD>
							<asp:DropDownList id="ddlDate" runat="server" tabIndex="1" Width="248px" CssClass="RequiredField"
								style="Z-INDEX: 0">
								<asp:ListItem Value="IssueDate" Selected="True">Issued Date</asp:ListItem>
								<asp:ListItem Value="ProposalDate">Proposal Date</asp:ListItem>
							</asp:DropDownList>
						</TD>
					</TR>
					<TR id="rowUSE_USERID" class="TRow_Normal">
						<TD style="WIDTH: 194px">From:</TD>
						<TD><SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATEFROM" tabIndex="2" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATEFROM" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATEFROM" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px">To :</TD>
						<TD>
							<SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATETO" tabIndex="5" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATETO" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATETO" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px; HEIGHT: 25px">Status (For ILAS):</TD>
						<TD>
							<SHMA:dropdownlist id="ddlstatus" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CCD_CODE"
								tabIndex="6" Width="5.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
						</TD>
					</TR>
					<TR id="rowCCN_CTRYCD" class="TRow_Alt">
						<TD style="WIDTH: 194px">
							<asp:HyperLink id="hlbanca" Visible="False" NavigateUrl="#" Text="Click Here to Download." runat="server" />
						</TD>
						<TD><a href="#" class="button2" onclick="saveUpdate('btngeneratebanc');">&nbsp;&nbsp;Generate 
								Banassurance File&nbsp;&nbsp;</a><asp:button id="btngeneratebanc" runat="server" Width="0px" Text="Generate Banassurance File"
								Font-Bold="True" BackColor="InactiveBorder" onclick="btngeneratebanc_Click"></asp:button></TD>
						<TD></TD>
					</TR>
					<TR class="TRow_Alt">
						<TD style="WIDTH: 194px">
							<asp:HyperLink id="hlublfile" Visible="False" NavigateUrl="#" Text="Click Here to Download." runat="server" />
						</TD>
						<TD><a href="#" class="button2" onclick="saveUpdate('btngeneratebank');">&nbsp;&nbsp;Generate 
								Bank 
								File&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a><asp:button id="btngeneratebank" runat="server" Width="0px" Text="Generate UBL File" Font-Bold="True"
								BackColor="InactiveBorder" onclick="btngeneratebank_Click"></asp:button></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px">
							<asp:HyperLink id="hlIlasile" Visible="False" NavigateUrl="#" Text="Click Here to Download." runat="server" />
						</TD>
						<TD><a href="#" class="button2" onclick="saveUpdate('btngenerateilasfile');">&nbsp;&nbsp;Generate 
								ILAS 
								File&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a><asp:button style="Z-INDEX: 0" id="btngenerateilasfile" runat="server" Width="0px" Text="Generate ILAS File"
								Font-Bold="True" BackColor="InactiveBorder" onclick="btngenerateilasfile_Click"></asp:button>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px">
							<asp:HyperLink id="hlDataDumpFile" Visible="False" NavigateUrl="#" Text="Click Here to Download."
								runat="server" />
						</TD>
						<TD><a href="#" class="button2" onclick="saveUpdate('btngeneratedatadumpfile');"> &nbsp;&nbsp;Generate 
								Data Dump File&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
							<asp:button style="Z-INDEX: 0" id="btngeneratedatadumpfile" runat="server" Width="0px" Text="Generate Data Dump File"
								Font-Bold="True" BackColor="InactiveBorder" onclick="btngeneratedatadumpfile_Click"></asp:button></TD>
					</TR>
										<tr>
						<TD></TD>
						<TD></TD>
					<TR>
						<TD style="WIDTH: 194px; HEIGHT: 25px">File Status for MIS:</TD>
						<TD>
							<asp:DropDownList id="ddlFileType" runat="server" tabIndex="6" Width="5.0pc" CssClass="RequiredField">
								<asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
								<asp:ListItem Value="Issued">Issued</asp:ListItem>
							</asp:DropDownList>
						</TD>
					</TR>

					<TR>
						<TD style="WIDTH: 194px">
							<asp:HyperLink id="hlIlasMisFile" Visible="False" NavigateUrl="#" Text="Click Here to Download."
								runat="server" />
						</TD>
						<TD><a href="#" class="button2" onclick="saveUpdate('btngenerateIlasMisfile');">&nbsp;&nbsp;Generate 
								ILAS MIS File &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
							<asp:button style="Z-INDEX: 0" id="btngenerateIlasMisfile" runat="server" Width="0px" Text="Generate ILAS MIS File"
								Font-Bold="True" BackColor="InactiveBorder" onclick="btngenerateIlasMisfile_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px; HEIGHT: 25px"></TD>
						<TD>
							<a href="#" class="button2" onclick="saveUpdate('btnTextFile');">&nbsp;&nbsp;Generate 
								MIS Text File &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
							<asp:button id="btnTextFile" runat="server" Width="0px" Text="Generate MIS Text File" Font-Bold="True" onclick="btnTextFile_Click"></asp:button>
						</TD>
					</TR>
					<TR id="rowUCN_DEFAULT" class="TRow_Alt">
						<TD style="WIDTH: 194px; HEIGHT: 11px">
							<P><asp:label style="Z-INDEX: 0" id="lblServerError" EnableViewState="false" runat="server" Visible="False"
									ForeColor="Red"></asp:label></P>
						</TD>
					</TR>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;
						</td>
						<TD></TD>
						<TD></TD>
					</tr>
					<TR>
						<td style="WIDTH: 194px">
							<P>&nbsp;</P>
						</td>
						<TD>
							<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:imagebutton style="DISPLAY: none" id="btn_Save" runat="server" ImageUrl="Images/savee.JPG" onClientClick="return CheckDates();"></asp:imagebutton></P>
						</TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<INPUT style="WIDTH: 0px" id="_CustomArgName" name="_CustomArgName" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomArgVal" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server"> <INPUT style="WIDTH: 0px" id="_CustomEventVal" name="_CustomEventVal" runat="server">&nbsp;
			<table border="0" width="100%">
				<tr>
					<td align="right"><A href="#"></A>&nbsp; <A href="#"></A>&nbsp; <A href="#"></A>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>
     	
     	function Download_BancaFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalBanca.xls" );
	    }
	    function Download_UblFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalUbl.xls" );
	    }
	    function Download_ILASFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalIlas.xls" );
	    }
	    function saveUpdate(ButtonId)
		{
			 document.all(ButtonId).click();
		}
		</script>
	</BODY>
</HTML>
