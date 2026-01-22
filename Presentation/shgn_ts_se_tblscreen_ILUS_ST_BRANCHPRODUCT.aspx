<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ST_BRANCHPRODUCT.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ST_BRANCHPRODUCT" %>
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
		<UC:EntityHeading ParamSource="FixValue" ParamValue="" id="EntityHeading" runat="server"></UC:EntityHeading>
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
			<SHMA:DATAGRID id="EntryGrid" runat="server" Width="456px" HeaderStyle-CssClass="GridHeader"
					AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow">
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
								<SHMA:TextBox id="lblCCH_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvCCH_CODE" runat="server" ControlToValidate="lblCCH_CODE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<SHMA:TextBox id="lblCCD_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvCCD_CODE" runat="server" ControlToValidate="lblCCD_CODE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<SHMA:TextBox id="lblCCS_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CCS_CODE") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvCCS_CODE" runat="server" ControlToValidate="lblCCS_CODE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:TextBox id="lblNewCCH_CODE" style="width:0" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewCCH_CODE" runat="server" ControlToValidate="lblNewCCH_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<SHMA:TextBox id="lblNewCCD_CODE" style="width:0" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewCCD_CODE" runat="server" ControlToValidate="lblNewCCD_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<SHMA:TextBox id="lblNewCCS_CODE" style="width:0" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewCCS_CODE" runat="server" ControlToValidate="lblNewCCS_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlPPR_PRODCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
								Width="8.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewPPR_PRODCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
								Width="8.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<SHMA:Peeredrequiredfieldvalidator id="prfvNewPPR_PRODCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlNewPPR_PRODCD"
								ControlsToCheck="ddlNewPPR_PRODCD" Display="Dynamic"></SHMA:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="PPR_PRODCD,CCH_CODE,CCD_CODE,CCS_CODE">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
