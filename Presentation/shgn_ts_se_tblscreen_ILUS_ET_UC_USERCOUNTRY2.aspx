<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_UC_USERCOUNTRY2.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_UC_USERCOUNTRY2" %>
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
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
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
		<form id="myForm" method="post" runat="server">
			<SHMA:DATAGRID id="EntryGrid" runat="server" Width="420px" HeaderStyle-CssClass="GridHeader"					AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow">
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
								<shma:TextBox id="lblUSE_USERID" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID") %>' runat="server" BaseType="Character">
								</shma:TextBox>
								<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="lblUSE_USERID" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewUSE_USERID" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewUSE_USERID" runat="server" ControlToValidate="lblNewUSE_USERID" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Country Code">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlCCN_CTRYCD" class="DropDownField" runat="server" Width="20.0pc" DataValueField="CCN_CTRYCD"
								DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></SHMA:dropdownlist>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewCCN_CTRYCD" class="DropDownField" runat="server" Width="20.0pc" DataValueField="CCN_CTRYCD"
								DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></SHMA:dropdownlist>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Default">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlUCN_DEFAULT" class="DropDownField" runat="server" Width="4.0pc" CssClass="REQUIREDFIELD">
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvUCN_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlUCN_DEFAULT"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewUCN_DEFAULT" class="DropDownField" runat="server" Width="4.0pc" CssClass="REQUIREDFIELD">
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNewUCN_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNewUCN_DEFAULT"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="CCN_CTRYCD,USE_USERID">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; POSITION: absolute;display:none; TOP: 80px; LEFT: 496px"
				runat="server" Height="12px" Width="0px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX: 102;display:none;  POSITION: absolute; TOP: 208px; LEFT: 664px"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px; display:none; " type="button" value="Button" name="_CustomEvent" causesvalidation="true"
				runat="server" onserverclick="_CustomEvent_ServerClick">
                <INPUT id="_CustomEvent1" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent" causesvalidation="false"
				runat="server" onserverclick="_CustomEvent_ServerClick">

		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 
		function beforeSave(){
			var counter = 0;
			var YN = '';

			for(j=1;j<=totalRecords;j++)
			{ 
				YN = getTabularFieldByIndex(j,'UCN_DEFAULT').value;
				if(YN=='Y')
					counter++;
			}

			if(counter>1)
			{
   				alert("Default already marked Yes!");   
   				return false;
			}
			return true;
		}
		fcStandardFooterFunctionsCall();</script>
		<table width="100%" BORDER="0">
			<tr>
				<td align="right"><A class="button2" onclick="send()" href="#">Save</A> <A class="button2" onclick="deleteDetail()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
