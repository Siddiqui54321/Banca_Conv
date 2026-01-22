<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="web" TagName="WebControl" Src="~/Presentation/uploader.ascx"%>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_USERUPLOAD.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_USERUPLOAD" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
		<UC:DispSelHeader id="dispSelHeader" runat="server"></UC:DispSelHeader>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<LINK href="Styles/ComboBox.css" type="text/css" rel="stylesheet">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/ComboBox.js"></script>
		<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="myForm" name="myForm" method="post" runat="server">
			<TABLE id="entryTable" cellSpacing="5" cellPadding="1">
				<tr class="form_heading">
					<td height="20" colSpan="2">&nbsp; User Upload
					</td>
				</tr>
				<TR id='row1'>
					<TD>File</TD>
				    <td>
						<web:WebControl ID="cnt" runat="server"></web:WebControl>
					</td>
				</TR>
				<TR>
					<TD colspan="2"><hr></TD>
				</TR>
			</TABLE>
		<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
