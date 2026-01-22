<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_ILUS_DS_UC_USERCHANNEL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_ILUS_DS_UC_USERCHANNEL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
    	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<UC:DispSelHeader id="dispSelHeader" runat="server"></UC:DispSelHeader>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<LINK href="Styles/ComboBox.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/ComboBox.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="myForm" name="myForm" method="post" runat="server">
			<TABLE class="DispSelTable" id="entryTable">
				<tr class="form_heading">
					<td height="20" colSpan="2" width="100%">&nbsp; User Authorized Branch(s)</td>
				</tr>

				<TR id='row1'>
					<TD width="15%">User ID </TD>
					<TD>
						<CV:ComboBox ListWidth="400" Validation="Y" ValueField="USE_USERID" TextFields="USE_USERID,USE_NAME"
							TableName="USE_USERMASTER" WhereColumns="" WhereValues="" WhereOperators="" QueryExtraInfo=""
							id="txtUSE_USERID" tabIndex="2" runat="server" Width='8.0pc' MaxLength="15" BaseType="Character"
							CssClass="RequiredField" onchange="setChannel();"></CV:ComboBox>
						<CV:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></CV:CompareValidator>
						<CV:requiredfieldvalidator id="rfvUSE_USERID" runat="server" ErrorMessage="Required" ControlToValidate="txtUSE_USERID"
							Display="Dynamic"></CV:requiredfieldvalidator>
					</TD>
				</TR>
				<TR>
					<TD colspan=2><hr></TD>
				</TR>
					<!--				<UC:DispSelButton id=dispSelButton runat="server"></UC:DispSelButton> --></TR>
			</TABLE>
			
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
