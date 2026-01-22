<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PURPOSE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PURPOSE" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
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
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->

		function RefreshFields()
		{				
							 myForm.txtNPU_SELECTED.value ="";
			

			setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Purpose of the Basic Plan" id="EntityHeading"
			runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<fieldset><legend>Entry</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<shma:TextBox id="txtNP1_PROPOSAL" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
						<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<shma:TextBox id="txtCPU_CODE" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
						<asp:CompareValidator id="cfvCPU_CODE" runat="server" ControlToValidate="txtCPU_CODE" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<TR id='rowNPU_SELECTED'>
							<TD>Protection</TD>
							<TD><shma:TextBox id="txtNPU_SELECTED" tabIndex="1" runat="server" Width='8.0pc' MaxLength="1" CssClass="RequiredField"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvNPU_SELECTED" runat="server" ControlToValidate="txtNPU_SELECTED" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvNPU_SELECTED" runat="server" ErrorMessage="Required" ControlToValidate="txtNPU_SELECTED"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
					</TABLE>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
