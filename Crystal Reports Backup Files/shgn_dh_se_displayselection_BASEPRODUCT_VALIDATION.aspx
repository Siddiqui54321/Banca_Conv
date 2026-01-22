<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_BASEPRODUCT_VALIDATION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_BASEPRODUCT_VALIDATION" %>
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
						<CV:dropdownlist id="ddlPPR_PRODCD_S" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
							tabIndex="2" Width="15.0pc" CssClass="RequiredField" onchange=""></CV:dropdownlist>
						<CV:CompareValidator id="cfvPPR_PRODCD_S" runat="server" ControlToValidate="ddlPPR_PRODCD_S" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></CV:CompareValidator>
						<CV:requiredfieldvalidator id="rfvPPR_PRODCD_S" runat="server" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD_S"
							Display="Dynamic"></CV:requiredfieldvalidator>
						<a href="#" class="button2" onclick="setFixedValuesInSession('PPR_PRODCD_S='+getField('PPR_PRODCD_S').value );SearchInChild();">
							&nbsp;&nbsp;Go&nbsp;&nbsp;</a>
					</TD> <!--<UC:DispSelButton id=dispSelButton runat="server"></UC:DispSelButton>-->
				</TR>
			</TABLE>
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
