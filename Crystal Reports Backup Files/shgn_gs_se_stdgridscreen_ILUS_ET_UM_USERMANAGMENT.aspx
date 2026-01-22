<%@ Page language="c#" Codebehind="shgn_gs_se_stdgridscreen_ILUS_ET_UM_USERMANAGMENT.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gs_se_stdgridscreen_ILUS_ET_UM_USERMANAGMENT" %>
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
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
			if(myForm.txtUSE_USERID.disabled==true)
				 myForm.txtUSE_USERID.disabled=false;
				 myForm.txtUSE_USERID.value ="";
							 myForm.txtUSE_NAME.value ="";
							 myForm.txtUSE_PASSWORD.value ="";
							 myForm.txtCCH_CODEDEFAULT.value ="";
							 myForm.txtUSE_TYPE.value ="";
myForm.txtUSE_USERID.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="User Detail" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<input class="btnHideLister" id="btnHideLister" onclick="HideLister()" type="button" value="Hide"
				name="btnHideLister" runat="server">
			<div class="GridDivWithLister" id="EntryTableDiv" runat="server">
				<fieldset><legend>Entry</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<TR id='row1'>
							<TD>User ID</TD>
							<TD nowrap><shma:TextBox id="txtUSE_USERID" tabIndex="2" runat="server" Width='5.0pc' MaxLength="10" CssClass="RequiredField"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvUSE_USERID" runat="server" ErrorMessage="Required" ControlToValidate="txtUSE_USERID"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
							<TD>User Name</TD>
							<TD nowrap><shma:TextBox id="txtUSE_NAME" tabIndex="3" runat="server" Width='13.0pc' MaxLength="25" CssClass="RequiredField"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvUSE_NAME" runat="server" ControlToValidate="txtUSE_NAME" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvUSE_NAME" runat="server" ErrorMessage="Required" ControlToValidate="txtUSE_NAME"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
							<TD>Password</TD>
							<TD nowrap><shma:TextBox id="txtUSE_PASSWORD" tabIndex="4" runat="server" Width='10.0pc' MaxLength="20" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvUSE_PASSWORD" runat="server" ControlToValidate="txtUSE_PASSWORD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='row4'>
							<TD>Dealult Channel</TD>
							<TD nowrap><shma:TextBox id="txtCCH_CODEDEFAULT" tabIndex="5" runat="server" Width='10.0pc' MaxLength="20"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvCCH_CODEDEFAULT" runat="server" ControlToValidate="txtCCH_CODEDEFAULT" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<TD>Type</TD>
							<TD nowrap><shma:TextBox id="txtUSE_TYPE" tabIndex="6" runat="server" Width='3.0pc' MaxLength="5" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvUSE_TYPE" runat="server" ControlToValidate="txtUSE_TYPE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						<TR>				
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
					</TABLE>
				</fieldset>
			</div>
			<DIV class="ListerDiv" id="ListerDiv" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend>List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD width="10%" onClick="filterLister('USE_USERID','User ID')">User ID</TD>
							<TD width="20%" onClick="filterLister('USE_NAME','User Name')">User Name</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkUSE_USERID1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.USE_NAME")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkUSE_USERID2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.USE_NAME")%></td>
								</tr>
							</AlternatingItemTemplate>
						</asp:repeater></TABLE>
				</FIELDSET>
				Page no:
				<asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="Pager" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
			</DIV>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
			<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
