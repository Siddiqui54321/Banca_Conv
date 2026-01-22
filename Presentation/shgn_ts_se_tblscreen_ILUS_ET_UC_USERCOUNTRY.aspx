<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_UC_USERCOUNTRY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_UC_USERCOUNTRY" %>
<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
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
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/JScriptTabular.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			totalRecords=<asp:Literal id="_totalRecords" runat="server" EnableViewState="False"></asp:Literal>+1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					
			//function SetStatus(rowStatus){					
			//	if (myForm.txtModifiedRows.value.indexOf(rowStatus) == -1)
			//		myForm.txtModifiedRows.value += rowStatus;
			//}			
		
		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="User Country Info" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" method="post" runat="server">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; POSITION: absolute; TOP: 80px; LEFT: 496px"
				runat="server" Height="12px" Width="0px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX: 102; POSITION: absolute; TOP: 208px; LEFT: 664px"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
                <INPUT id="_CustomEvent1" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
			<SHMA:DATAGRID id="EntryGrid" runat="server" Width="456px" HeaderStyle-CssClass="GridHeader"					AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow">
				<SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<CENTER>
								<asp:CheckBox id="chkSelectAll" runat="server"></asp:CheckBox></CENTER>
						</HeaderTemplate>
						<ItemTemplate>
							<CENTER>
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate></FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Country Code">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlCCN_CTRYCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField=""
								Width="8.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlCCN_CTRYCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewCCN_CTRYCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField=""
								Width="8.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<shma:Peeredrequiredfieldvalidator id="prfvNewCCN_CTRYCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlNewCCN_CTRYCD"
								ControlsToCheck="ddlNewCCN_CTRYCD" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Default">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlUCN_DEFAULT" Width="8.0pc" runat="server">
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvUCN_DEFAULT" runat="server" ControlToValidate="ddlUCN_DEFAULT" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewUCN_DEFAULT" Width="8.0pc" runat="server">
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNewUCN_DEFAULT" runat="server" ControlToValidate="ddlNewUCN_DEFAULT" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
