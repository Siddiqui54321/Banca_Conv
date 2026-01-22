<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_ILUS_DS_UL_LIMITATION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_ILUS_DS_UL_LIMITATION" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
    	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<UC:DispSelHeader id="dispSelHeader" runat="server"></UC:DispSelHeader>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<LINK href="Styles/ComboBox.css" type="text/css" rel="stylesheet">
			<!-- <LINK href="Styles/Style.css" type="text/css" rel="stylesheet"> -->
			<%Response.Write(ace.Ace_General.loadInnerStyle());%>
				<script language="javascript" src="JSFiles/ComboBox.js"></script>
				<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
				<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
	</HEAD>
	<body>
		<form id="myForm" name="myForm" method="post" runat="server">
			<TABLE class="DispSelTable" id="entryTable">
				<tr class="form_heading">
					<td height="20" colSpan="6">&nbsp; Plan Details
					</td>
				</tr>
				<TR id='row1'>
					<TD width="15%">User Limitation</TD>
					<TD nowrap>
						<CV:ComboBox ListWidth="400" Validation="Y" ValueField="PPR_PRODCD" TextFields="PPR_PRODCD,PPR_DESCR"
							TableName="LPPR_PRODUCT" WhereColumns="" WhereValues="" WhereOperators="" QueryExtraInfo=""
							id="txtPPR_PRODCD" tabIndex="2" runat="server" Width='8.0pc' MaxLength="15" BaseType="Character"
							CssClass="RequiredComboText" onchange="setLimit();"></CV:ComboBox>
						<CV:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="txtPPR_PRODCD" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></CV:CompareValidator>
						<CV:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" ErrorMessage="Required" ControlToValidate="txtPPR_PRODCD"
							Display="Dynamic"></CV:requiredfieldvalidator>
					</TD> <!--				<UC:DispSelButton id=dispSelButton runat="server"></UC:DispSelButton> --></TR>
			</TABLE>
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
