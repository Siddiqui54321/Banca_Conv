<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_LF_LNFUFUNDS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_LF_LNFUFUNDS" %>
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
		<LINK href="Styles/ComboItem.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/ComboItem.js"></script>
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
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Fund Allocation" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" method="post" runat="server">
			<asp:dropdownlist id="pagerList" runat="server" style="DISPLAY:none" Width="48px" AutoPostBack="True" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; POSITION: absolute; TOP: 80px; LEFT: 496px;visibility:hidden"
				runat="server" Height="12px" Width="0px"></asp:textbox>
			<asp:textbox id="txtOrgCode" style="Z-INDEX: 102; POSITION: absolute; TOP: 208px; LEFT: 664px;visibility:hidden"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
			<SHMA:DATAGRID id="EntryGrid" runat="server" Width="456px" HeaderStyle-CssClass="GridHeader"
					AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow">
				<SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<CENTER>
								<asp:CheckBox id="chkSelectAll" style="display:none" tabIndex="1000" runat="server"></asp:CheckBox></CENTER>
						</HeaderTemplate>
						<ItemTemplate>
							<CENTER>
								<SHMA:TextBox id="lblNP1_PROPOSAL" style="width:0" tabIndex="2000" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="lblNP1_PROPOSAL" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<SHMA:TextBox id="lblNP2_SETNO" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NP2_SETNO") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvNP2_SETNO" runat="server" ControlToValidate="lblNP2_SETNO" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<SHMA:TextBox id="lblPPR_PRODCD" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD") %>' runat="server" BaseType="Character">
								</SHMA:TextBox>
								<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="lblPPR_PRODCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkDelete" style="display:none" runat="server"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:TextBox id="lblNewNP1_PROPOSAL" style="width:0" tabIndex="2000" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewNP1_PROPOSAL" runat="server" ControlToValidate="lblNewNP1_PROPOSAL" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<SHMA:TextBox id="lblNewNP2_SETNO" style="width:0" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewNP2_SETNO" runat="server" ControlToValidate="lblNewNP2_SETNO" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<SHMA:TextBox id="lblNewPPR_PRODCD" style="width:0" runat="server" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewPPR_PRODCD" runat="server" ControlToValidate="lblNewPPR_PRODCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Fund Code">
						<ItemTemplate>
							<SHMA:ComboItem ListWidth="400" tabIndex="3000" Validation="Y" LovType="" FieldTitle="" FieldWidth="" FieldType="" ColumnMapping="txtCFU_DESCR" DescriptionColumn="CFU_DESCR" ValueField="CFU_FUNDCODE" TextFields = "CFU_FUNDCODE,CFU_DESCR" TableName="LCFU_FUNDS" WhereColumns="" WhereValues ="" WhereOperators ="" QueryExtraInfo="" id="txtCFU_FUNDCODE" runat="server" Width='8.0pc' MaxLength="3" BaseType="Character" Text='<%#DataBinder.Eval(Container, "DataItem.CFU_FUNDCODE")%>' >
							</SHMA:ComboItem>
							<asp:CompareValidator id="cfvCFU_FUNDCODE" runat="server" ControlToValidate="txtCFU_FUNDCODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:ComboItem ListWidth="400" tabIndex="4000" Validation="Y" LovType="" FieldTitle="" FieldWidth=""
								FieldType="" ColumnMapping="txtNewCFU_DESCR" DescriptionColumn="CFU_DESCR" ValueField="CFU_FUNDCODE"
								TextFields="CFU_FUNDCODE,CFU_DESCR" TableName="LCFU_FUNDS" WhereColumns="" WhereValues="" WhereOperators=""
								QueryExtraInfo=" PPR_PRODCD=SV(&quot;PPR_PRODCD&quot;)" id="txtNewCFU_FUNDCODE" runat="server"
								Width='8.0pc' MaxLength="3" BaseType="Character"></SHMA:ComboItem>
							<asp:CompareValidator id="cfvNewCFU_FUNDCODE" runat="server" ControlToValidate="txtNewCFU_FUNDCODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Description">
						<ItemTemplate>
							<SHMA:TextBox id="txtCFU_DESCR" runat="server" tabIndex="5000" Width='15.0pc' ReadOnly="true"
								MaxLength="50" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvCFU_DESCR" runat="server" ControlToValidate="txtCFU_DESCR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:TextBox id="txtNewCFU_DESCR" runat="server" tabIndex="6000" Width='15.0pc' ReadOnly="true"
								MaxLength="50" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewCFU_DESCR" runat="server" ControlToValidate="txtNewCFU_DESCR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Distribution (%)">
						<ItemTemplate>
							<SHMA:TextBox id="txtNFU_RATE" runat="server" tabIndex="0" Width='10.0pc' onblur="validateNumber(this);" Text='<%#DataBinder.Eval(Container, "DataItem.NFU_RATE")%>' MaxLength="6" BaseType="Number" Precision="0" >
							</SHMA:TextBox>
							<asp:CompareValidator id="cfvNFU_RATE" runat="server" ControlToValidate="txtNFU_RATE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:TextBox id="txtNewNFU_RATE" runat="server" Width='10.0pc' MaxLength="6" BaseType="Number"></SHMA:TextBox>
							<asp:CompareValidator id="cfvNewNFU_RATE" runat="server" ControlToValidate="txtNewNFU_RATE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<input type="hidden" name="PkColumns" value="NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CFU_FUNDCODE">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();</script>
		<script>
	
	function beforeSave()
	{
		//return validatePercentage();
		return true;
	}
	
	function validateNumber(obj)
	{
		if(eval(obj.value)<0 || eval(obj.value)>100)
		{
			alert('Valid values 1 - 100.');
			obj.focus();
		}
	}

	//function validatePercentage()
	//{
	//	var objid = "txtNFU_RATE";
	//	var val = 0;
	//	var item = "";
	//	var blnreturn ;
	//	for (i=0;i<totalRecords-1;i++) 
	//	{
	//		var id="000000"+i;
	//		id=id.substring(id.length-6);
	//		var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;
	//		if (document.getElementById(item) !=null && document.getElementById(item).value !='')
	//			val += eval(document.getElementById(item).value.replace('%',''));
	//	}
	//	blnreturn = true;
	//	if (eval(val)!=100 )	
	//	{
	//		alert('Warning: Total percentage '+val+'% must be equal to 100%');
	//		blnreturn = false;  
	//	}
	//	return blnreturn ;  
	//}
	
		</script>
	</body>
</HTML>
