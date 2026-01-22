<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_BANCAPARAMS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_BANCAPARAMS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
    	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<UC:DispSelHeader id="dispSelHeader" runat="server"></UC:DispSelHeader>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Product Selection" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<br>
			<TABLE class="DispSelTable" id="entryTable">
				<TR id='row1'>
					<TD>Product
						<CV:dropdownlist id="ddlCSH_ID" runat="server" BlankValue="true" DataTextField="desc_f" DataValueField="CSH_ID"
							tabIndex="2" Width="15.0pc" CssClass="RequiredField" onchange=""></CV:dropdownlist>
						<CV:CompareValidator id="cfvCSH_ID" runat="server" ControlToValidate="ddlCSH_ID" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></CV:CompareValidator>
						<CV:requiredfieldvalidator id="rfvCSH_ID" runat="server" ErrorMessage="Required" ControlToValidate="ddlCSH_ID"
							Display="Dynamic"></CV:requiredfieldvalidator>
						<a href="#" class="button2" onclick="setFixedValuesInSession('CSH_ID='+getField('CSH_ID').value );SearchInChild();">
							&nbsp;&nbsp;Go&nbsp;&nbsp;</a>
					</TD>
				</TR>
			</TABLE>
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
