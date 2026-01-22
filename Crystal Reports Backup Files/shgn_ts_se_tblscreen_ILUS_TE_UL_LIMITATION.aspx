<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_TE_UL_LIMITATION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_TE_UL_LIMITATION" %>
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
		<!-- <LINK href="Styles/Style.css" type="text/css" rel="stylesheet"> -->
		<%Response.Write(ace.Ace_General.loadInnerStyle());%>
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
		<UC:EntityHeading ParamSource="FixValue" ParamValue="User Limitation Setup" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" method="post" runat="server">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; LEFT: 496px; POSITION: absolute; TOP: 80px"
				runat="server" Height="12px" Width="0px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX: 102; LEFT: 664px; POSITION: absolute; TOP: 208px"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
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
								<shma:TextBox id="lblPPR_PRODCD" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD") %>' runat="server" BaseType="Character">
								</shma:TextBox>
								<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="lblPPR_PRODCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewPPR_PRODCD" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPPR_PRODCD" runat="server" ControlToValidate="lblNewPPR_PRODCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Validation For">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlPVL_VALIDATIONFOR" class="DropDownField" Width="8.0pc" runat="server">
								<asp:ListItem Value="SUMASSURED">SUMASSURED</asp:ListItem>
								<asp:ListItem Value="TERM">TERM</asp:ListItem>
								<asp:ListItem Value="MATURITYAGE">MATURITY AGE</asp:ListItem>
								<asp:ListItem Value="ENTRYAGE">ENTRY AGE</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPVL_VALIDATIONFOR" runat="server" ControlToValidate="ddlPVL_VALIDATIONFOR"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewPVL_VALIDATIONFOR" class="DropDownField" Width="8.0pc" runat="server">
								<asp:ListItem Value="SUMASSURED">SUMASSURED</asp:ListItem>
								<asp:ListItem Value="TERM">TERM</asp:ListItem>
								<asp:ListItem Value="MATURITYAGE">MATURITY AGE</asp:ListItem>
								<asp:ListItem Value="ENTRYAGE">ENTRY AGE</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNewPVL_VALIDATIONFOR" runat="server" ControlToValidate="ddlNewPVL_VALIDATIONFOR"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Checking Sequence">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_LEVEL" class="DisplayOnly test" runat="server" Width='4.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_LEVEL")%>' MaxLength="5" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_LEVEL" runat="server" ControlToValidate="txtPVL_LEVEL" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_LEVEL" class="DisplayOnly test" runat="server" Width='4.0pc' MaxLength="5"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_LEVEL" runat="server" ControlToValidate="txtNewPVL_LEVEL" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Clients Age From">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_AGEFROM" runat="server" Width='3.0pc' class="DisplayOnly test" Text='<%#DataBinder.Eval(Container, "DataItem.PVL_AGEFROM")%>' MaxLength="5" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_AGEFROM" runat="server" ControlToValidate="txtPVL_AGEFROM" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_AGEFROM" class="DisplayOnly test" runat="server" Width='3.0pc' MaxLength="5"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_AGEFROM" runat="server" ControlToValidate="txtNewPVL_AGEFROM" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Clients Age To">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_AGETO" runat="server" class="DisplayOnly test" Width='3.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_AGETO")%>' MaxLength="5" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_AGETO" runat="server" ControlToValidate="txtPVL_AGETO" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_AGETO" class="DisplayOnly test" runat="server" Width='3.0pc' MaxLength="5"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_AGETO" runat="server" ControlToValidate="txtNewPVL_AGETO" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Term From">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_TERMFROM" class="DisplayOnly test" runat="server" Width='3.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_TERMFROM")%>' MaxLength="5" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_TERMFROM" runat="server" ControlToValidate="txtPVL_TERMFROM" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_TERMFROM" class="DisplayOnly test" runat="server" Width='3.0pc' MaxLength="5"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_TERMFROM" runat="server" ControlToValidate="txtNewPVL_TERMFROM" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Term To">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_TERMTO" runat="server" class="DisplayOnly test" Width='3.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_TERMTO")%>' MaxLength="5" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_TERMTO" runat="server" ControlToValidate="txtPVL_TERMTO" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_TERMTO" runat="server" class="DisplayOnly test" Width='3.0pc' MaxLength="5"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_TERMTO" runat="server" ControlToValidate="txtNewPVL_TERMTO" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Valid Range From">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_VALIDFROM" runat="server" class="DisplayOnly test" Width='10.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_VALIDFROM")%>' MaxLength="20" BaseType="Character">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_VALIDFROM" runat="server" ControlToValidate="txtPVL_VALIDFROM" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_VALIDFROM" class="DisplayOnly test" runat="server" Width='10.0pc'
								MaxLength="20" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_VALIDFROM" runat="server" ControlToValidate="txtNewPVL_VALIDFROM"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Valid Range TO">
						<ItemTemplate>
							<shma:TextBox id="txtPVL_VALIDTO" runat="server" class="DisplayOnly test" Width='10.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PVL_VALIDTO")%>' MaxLength="20" BaseType="Character">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPVL_VALIDTO" runat="server" ControlToValidate="txtPVL_VALIDTO" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPVL_VALIDTO" class="DisplayOnly test" runat="server" Width='10.0pc' MaxLength="20"
								BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPVL_VALIDTO" runat="server" ControlToValidate="txtNewPVL_VALIDTO" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="PVL_VALIDATIONFOR,PPR_PRODCD,PVL_LEVEL">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();</script>
		<table width="100%" BORDER="0">
			<tr>
				<td align="right"><A class="button2" onclick="send()" href="#">Save</A> <A class="button2" onclick="deleteDetail()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
