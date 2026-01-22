<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_NM_PURPOSE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_NM_PURPOSE" %>
<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>

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
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>

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
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Purpose of the Basic Plan" id="EntityHeading" runat="server"></UC:EntityHeading>
		
		
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
			<SHMA:DATAGRID id="EntryGrid" runat="server" HeaderStyle-CssClass="GridHeader" AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" >
				<SelectedItemStyle ></SelectedItemStyle>
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<CENTER>
								<asp:CheckBox id="chkSelectAll" runat="server" ></asp:CheckBox></CENTER>
						</HeaderTemplate>
						<ItemTemplate>
							<CENTER>
								<shma:TextBox id="lblNP1_PROPOSAL" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
								<shma:TextBox id="lblCPU_CODE" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.CPU_CODE") %>' runat="server" BaseType="Character"></shma:TextBox>
								
								<!--<table border=0><tr><td style="BACKGROUND-COLOR: #C2DA78;">-->
									<asp:CheckBox id="chkDelete" runat="server" style="width:10px"></asp:CheckBox></CENTER>
								<!--</td></tr></table>-->
						
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewNP1_PROPOSAL" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<shma:TextBox id="lblNewCPU_CODE" style="width:0" runat="server" BaseType="Character"></shma:TextBox>

						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Protection">
						<ItemTemplate>
							<shma:TextBox id="txtCPU_DESCR" runat="server" Width='95%' Text='<%#DataBinder.Eval(Container, "DataItem.CPU_DESCR")%>' MaxLength="60" CssClass="TextToLable" BaseType="Character" ReadOnly="true">
							</shma:TextBox>
							<asp:CompareValidator id="cfvCPU_DESCR" runat="server" ControlToValidate="txtCPU_DESCR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvCPU_DESCR" runat="server" ErrorMessage="Required" ControlToValidate="txtCPU_DESCR"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewCPU_DESCR" runat="server" Width='95%' MaxLength="60" CssClass="RequiredField"
								BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewCPU_DESCR" runat="server" ControlToValidate="txtNewCPU_DESCR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:Peeredrequiredfieldvalidator id="prfvNewCPU_DESCR" runat="server" ErrorMessage="Required" ControlToValidate="txtNewCPU_DESCR"
								ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="CPU_CODE">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();
		
		
		parent.closeWait();
		
		
		function setTextToLableColor()
		{
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+"txtCPU_DESCR";

				if ((i%2)==0)
					document.getElementById(item).className="TextToLableAlt";
				else
					document.getElementById(item).className="TextToLable";
			}
		}
		setTextToLableColor();

		
		
		
		</script>
	</body>
</HTML>
