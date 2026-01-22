<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_ILUS_ET_DS_ER_EXCHANGERATE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_ILUS_ET_DS_ER_EXCHANGERATE" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
    	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<UC:DispSelHeader id="dispSelHeader" runat="server"></UC:DispSelHeader>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<LINK href="Styles/ComboBox.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="JSFiles/ComboBox.js"></script>
			<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
			<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body>
		<p class="EntityHeading">Exchange Rate Setup</ASP:LITERAL></p>
		<form id="myForm" name="myForm" method="post" runat="server">
			<TABLE class="DispSelTable" id="entryTable">
				<TR id='row1'>
					<TD width="15%">Base Currency</TD>
					<TD nowrap>
						<CV:ComboBox ListWidth="400" Validation="Y" ValueField="PCU_CURRCODE" TextFields="PCU_CURRCODE,PCU_CURRDESC"
							TableName="PCU_CURRENCY" WhereColumns="" WhereValues="" WhereOperators="" QueryExtraInfo=""
							id="txtPCU_BASECURR" tabIndex="2" runat="server" Width='8.0pc' MaxLength="15" BaseType="Character"
							CssClass="RequiredComboText" onchange="setCurrency();"></CV:ComboBox>
						<CV:CompareValidator id="cfvPCU_BASECURR" runat="server" ControlToValidate="txtPCU_BASECURR" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></CV:CompareValidator>
						<CV:requiredfieldvalidator id="rfvPCU_BASECURR" runat="server" ErrorMessage="Required" ControlToValidate="txtPCU_BASECURR"
							Display="Dynamic"></CV:requiredfieldvalidator>
					</TD>
					<!--<UC:DispSelButton id=dispSelButton runat="server"></UC:DispSelButton>--></TR>
			</TABLE>
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
