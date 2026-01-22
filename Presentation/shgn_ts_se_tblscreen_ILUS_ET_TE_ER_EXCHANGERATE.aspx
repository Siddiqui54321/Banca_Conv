<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TE_ER_EXCHANGERATE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TE_ER_EXCHANGERATE" %>
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
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Exchange Rate Detail" id="EntityHeading" runat="server"></UC:EntityHeading>
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
								<shma:TextBox id="lblPCU_BASECURR" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.PCU_BASECURR") %>' runat="server" BaseType="Character">
								</shma:TextBox>
								<asp:CompareValidator id="cfvPCU_BASECURR" runat="server" ControlToValidate="lblPCU_BASECURR" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewPCU_BASECURR" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPCU_BASECURR" runat="server" ControlToValidate="lblNewPCU_BASECURR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Currency Code">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlPCU_CURRCODE" runat="server" class="DropDownField" BlankValue="True" DataTextField="desc_f"
								DataValueField="PCU_CURRCODE" Width="10.0pc"></SHMA:dropdownlist>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewPCU_CURRCODE" runat="server" class="DropDownField" BlankValue="True" DataTextField="desc_f"
								DataValueField="PCU_CURRCODE" Width="10.0pc"></SHMA:dropdownlist>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Rate Type">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlPET_EXRATETYPE" Width="5.0pc" runat="server" class="DropDownField">
								<asp:ListItem Value="S">Selling</asp:ListItem>
								<asp:ListItem Value="B">Buying</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPET_EXRATETYPE" runat="server" ControlToValidate="ddlPET_EXRATETYPE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewPET_EXRATETYPE" Width="5.0pc" runat="server" class="DropDownField">
								<asp:ListItem Value="S">Selling</asp:ListItem>
								<asp:ListItem Value="B">Buying</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNewPET_EXRATETYPE" runat="server" ControlToValidate="ddlNewPET_EXRATETYPE"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Value Date">
						<ItemTemplate>
							<SHMA:DateBox id="txtPEX_VALUEDAT" runat="server" class="DisplayOnly test" maxlength="15" Width='8.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PEX_VALUEDAT")%>' >
							</SHMA:DateBox>
							<asp:CompareValidator id="cfvPEX_VALUEDAT" runat="server" ControlToValidate="txtPEX_VALUEDAT" Operator="DataTypeCheck"
								Type="Date" ErrorMessage="Date Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:DateBox id="txtNewPEX_VALUEDAT" runat="server" class="DisplayOnly test" maxlength="15" Width='8.0pc'></SHMA:DateBox>
							<asp:CompareValidator id="cfvNewPEX_VALUEDAT" runat="server" ControlToValidate="txtNewPEX_VALUEDAT" Operator="DataTypeCheck"
								Type="Date" ErrorMessage="Date Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Serial">
						<ItemTemplate>
							<shma:TextBox id="txtPEX_SERIAL" runat="server" class="DisplayOnly" Width='3.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PEX_SERIAL")%>' MaxLength="5" BaseType="Character">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPEX_SERIAL" runat="server" ControlToValidate="txtPEX_SERIAL" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPEX_SERIAL" runat="server" class="DisplayOnly" Width='3.0pc' MaxLength="5"
								BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPEX_SERIAL" runat="server" ControlToValidate="txtNewPEX_SERIAL" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Rate">
						<ItemTemplate>
							<shma:TextBox id="txtPEX_RATE" runat="server" class="DisplayOnly test" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.PEX_RATE")%>' MaxLength="10" BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvPEX_RATE" runat="server" ControlToValidate="txtPEX_RATE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewPEX_RATE" runat="server" class="DisplayOnly test" Width='5.0pc' MaxLength="10"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewPEX_RATE" runat="server" ControlToValidate="txtNewPEX_RATE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="PCU_BASECURR,PCU_CURRCODE,PET_EXRATETYPE,PEX_VALUEDAT,PEX_SERIAL">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();</script>
		<table width="100%" BORDER="0">
			<tr>
				<td align="right">
					<a href="#"><IMG onmouseover="this.src='../shmalib/images/buttons/save_2.gif'" onmouseout="this.src='../shmalib/images/buttons/save.gif'"
							border="0" name="btnsave" alt="" src="../shmalib/images/buttons/save.gif" onclick="send()"></a>
					<a href="#"><IMG onmouseover="this.src='../shmalib/images/buttons/delete_2.gif'" onmouseout="this.src='../shmalib/images/buttons/delete.gif'"
							border="0" name="btndelete" alt="" src="../shmalib/images/buttons/delete.gif" onclick="deleteDetail()"></a>
				</td>
			</tr>
		</table>
	</body>
</HTML>
