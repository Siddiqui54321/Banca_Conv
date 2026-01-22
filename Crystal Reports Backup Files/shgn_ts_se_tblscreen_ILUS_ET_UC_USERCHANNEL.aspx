<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_UC_USERCHANNEL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_UC_USERCHANNEL" %>
<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/JScriptTabular.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/UserChannel.js"></SCRIPT>
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
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="User Channel Setup" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" runat="server">

			<table border=0>
				<tr><td width="15%">Channel &nbsp;</td><td><SHMA:dropdownlist id="ddlCCH_CODE_1" class="DropDownField" runat="server" Width="12.0pc" DataValueField="CCH_CODE" DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></SHMA:dropdownlist></td></tr>
				<tr><td width="15%">Channel Detail&nbsp;</td><td> <SHMA:dropdownlist id="ddlCCD_CODE_1" class="DropDownField" runat="server" Width="12.0pc" DataValueField="CCD_CODE" DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></SHMA:dropdownlist></td></tr>
			</table>
			
			<SHMA:DATAGRID id=EntryGrid runat="server" Width="400px" SelectedItemStyle-CssClass="GridSelRow" ShowFooter="<%# ShowFooter %>" AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader">
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

								<shma:TextBox id="lblCCH_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE") %>' runat="server" BaseType="Character">
								</shma:TextBox>
								<asp:CompareValidator id="cfvCCH_CODE" runat="server" ControlToValidate="lblCCH_CODE" Operator="DataTypeCheck" 
 Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
									
								<shma:TextBox id="lblCCD_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE") %>' runat="server" BaseType="Character">
								</shma:TextBox>
								<asp:CompareValidator id="cfvCCD_CODE" runat="server" ControlToValidate="lblCCD_CODE" Operator="DataTypeCheck" 
 Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>																		
									
								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewUSE_USERID" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewUSE_USERID" runat="server" ControlToValidate="lblNewUSE_USERID" Operator="DataTypeCheck" 
 Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

							<shma:TextBox id="lblNewCCH_CODE" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewCCH_CODE" runat="server" ControlToValidate="lblNewCCH_CODE" Operator="DataTypeCheck" 
 Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
														
							<shma:TextBox id="lblNewCCD_CODE" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewCCD_CODE" runat="server" ControlToValidate="lblNewCCD_CODE" Operator="DataTypeCheck" 
 Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								
						</FooterTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Branch / Location">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlCCS_CODE" class="DropDownField" runat="server" Width="19.0pc" CssClass="REQUIREDFIELD" 
 BlankValue="True" DataTextField="desc_f" DataValueField="CCS_CODE"></SHMA:dropdownlist>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewCCS_CODE" class="DropDownField" runat="server" Width="19.0pc" CssClass="REQUIREDFIELD" 
 BlankValue="True" DataTextField="desc_f" DataValueField="CCS_CODE"></SHMA:dropdownlist>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Default">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlUCH_DEFAULT" class="DropDownField" runat="server" Width="50px" CssClass="REQUIREDFIELD">
								<asp:ListItem Value="N">No</asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvUCH_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False" 
 ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlUCH_DEFAULT"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewUCH_DEFAULT" class="DropDownField" runat="server" Width="50px" CssClass="REQUIREDFIELD">
								<asp:ListItem Value="N">No</asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNewUCH_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False" 
 ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNewUCH_DEFAULT"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input value="CCH_CODE,USE_USERID,CCD_CODE,CCS_CODE" type="hidden" name="PkColumns">
			<asp:textbox style="Z-INDEX: 103; POSITION: absolute; TOP: 80px; LEFT: 496px" id="txtModifiedRows" runat="server" Width="0px" Height="12px"></asp:textbox>
			<asp:textbox style="Z-INDEX: 102; POSITION: absolute; TOP: 208px; LEFT: 664px" id="txtOrgCode" runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> 
			<INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 
		function beforeSave()
		{
			var counter = 0;
			var YN = '';

			for(j=1;j<=totalRecords;j++)
			{ 
				YN = getTabularFieldByIndex(j,'UCH_DEFAULT').value;
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
		fcStandardFooterFunctionsCall();
		
		</script>
		<table border="0" width="100%">
			<tr>
				<td align="right"><A class="button2" onclick="send()" href="#">Save</A> <A class="button2" onclick="deleteDetail()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
