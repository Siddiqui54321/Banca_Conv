<%@	Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@	Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TB_WITHDRAWAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TB_WITHDRAWAL" %>
<%@	Register TagPrefix="UC"	TagName="EntityHeading"	Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML	4.0	Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html;	charset=windows-1252">
		<meta content="Microsoft Visual	Studio 7.0" name="GENERATOR">
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
			<asp:Literal id="MessageScript"	runat="server" EnableViewState="False"></asp:Literal>
			totalRecords=<asp:Literal id="_totalRecords" runat="server"	EnableViewState="False"></asp:Literal>+1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					
			//function SetStatus(rowStatus){					
			//	if (myForm.txtModifiedRows.value.indexOf(rowStatus)	== -1)
			//		myForm.txtModifiedRows.value +=	rowStatus;
			//}			
		
		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" method="post" runat="server">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; LEFT: 496px;	POSITION: absolute;	TOP: 80px"
				runat="server" Height="12px" Width="0px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX:	102; LEFT: 664px; POSITION:	absolute; TOP: 208px"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
			
			<table style="LEFT: 20px; POSITION: absolute; TOP: 0px">
				<tr><td>Code</td></tr>
				<tr><td>Column 1</td></tr>
				<tr><td>Column 2</td></tr>
				<tr><td>Column 3</td></tr>
			</table>
			
			<asp:DataList id="EntryGrid" runat="server" Width="456px" style="LEFT: 150px; POSITION: absolute; TOP: 0px" 
				AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader" SelectedItemStyle-CssClass="GridSelRow" RepeatDirection="Vertical" RepeatColumns="10">
				
				<ItemTemplate>

					<asp:TextBox id="lblNP1_PROPOSAL" style="width:0"	Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>'  runat="server" BaseType="Character"></asp:TextBox><asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="lblNP1_PROPOSAL" Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is	Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<asp:TextBox id="txtNTP_COLCODE" runat="server"	 Width='8.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NTP_COLCODE")%>'  MaxLength="10" CssClass="RequiredField"	BaseType="Character">
					</asp:TextBox><asp:CompareValidator id="cfvNTP_COLCODE" runat="server" ControlToValidate="txtNTP_COLCODE" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is	Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<asp:requiredfieldvalidator id="rfvNTP_COLCODE" runat="server" ErrorMessage="Required" ControlToValidate="txtNTP_COLCODE"
						Display="Dynamic"></asp:requiredfieldvalidator>
					<SHMA:dropdownlist id="ddlNTP_COL1" Width="8.0pc" runat="server">
						<asp:ListItem Value="Y">Yes</asp:ListItem>
						<asp:ListItem Value="N">No</asp:ListItem>
					</SHMA:dropdownlist>
					<asp:CompareValidator id="cfvNTP_COL1" runat="server" ControlToValidate="ddlNTP_COL1" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String	Format is Incorrect	" EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<SHMA:dropdownlist id="ddlNTP_COL2" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CPU_CODE"
						Width="5.0pc"></SHMA:dropdownlist>
					<asp:TextBox	id="txtNTP_COL3"  runat="server"  Width='8.0pc'	Text='<%#DataBinder.Eval(Container,	"DataItem.NTP_COL3")%>'	 MaxLength="20"	BaseType="Character">
					</asp:TextBox>
					<asp:CompareValidator id="cfvNTP_COL3" runat="server" ControlToValidate="txtNTP_COL3" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String	Format is Incorrect	" EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
				</ItemTemplate>
			</asp:DataList>
			<input type="hidden" name="PkColumns" value="NP1_PROPOSAL,NTP_COLCODE">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server"	EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();
		
		function saveUpdateClicked()
		{
			send();
		}
		
		</script>
	</body>
</HTML>
