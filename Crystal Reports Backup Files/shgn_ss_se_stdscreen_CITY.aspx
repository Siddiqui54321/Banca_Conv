<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_CITY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_CITY" %>
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
			if(myForm.txtCCT_CITYCD.disabled==true)
				 myForm.txtCCT_CITYCD.disabled=false;
				 myForm.txtCCT_CITYCD.value ="";
							 myForm.txtCCT_DESCR.value ="";
			
myForm.txtCCT_CITYCD.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Province/State Setup
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP: 20px; MARGIN-LEFT: 320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; User Entry
						</td>
					</tr>
				</table>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<SHMA:TextBox id="txtCCN_CTRYCD" runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvCCN_CTRYCD" runat="server" ControlToValidate="txtCCN_CTRYCD" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<SHMA:TextBox id="txtCCT_PROVCD" runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvCCT_PROVCD" runat="server" ControlToValidate="txtCCT_PROVCD" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<TR id='rowCCT_CITYCD'>
						<TD width="10%">Code</TD>
						<TD width="90%"><SHMA:TextBox id="txtCCT_CITYCD" tabIndex="1" runat="server" Width='7.0pc' MaxLength="3" CssClass="RequiredField"
								BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvCCT_CITYCD" runat="server" ControlToValidate="txtCCT_CITYCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvCCT_CITYCD" runat="server" ErrorMessage="Required" ControlToValidate="txtCCT_CITYCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowCCT_DESCR'>
						<TD width="10%">Name</TD>
						<TD width="90%"><SHMA:TextBox id="txtCCT_DESCR" tabIndex="2" runat="server" Width='20.0pc' MaxLength="100" CssClass="RequiredField"
								BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvCCT_DESCR" runat="server" ControlToValidate="txtCCT_DESCR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvCCT_DESCR" runat="server" ErrorMessage="Required" ControlToValidate="txtCCT_DESCR"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR>
						<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<DIV class="ListerDiv" id="ListerDiv" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('CCT_CITYCD','Code')">Code</TD>
							<TD onClick="filterLister('CCT_DESCR','Name')">Name</TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCCT_CITYCD1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCT_CITYCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CCT_DESCR")%></td>
									<td>
										<asp:Label Visible=false ID="lblCCN_CTRYCD1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_CTRYCD")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCT_PROVCD1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCT_PROVCD")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCCT_CITYCD2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCT_CITYCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CCT_DESCR")%></td>
									<td>
										<asp:Label Visible=false ID="lblCCN_CTRYCD2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_CTRYCD")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCT_PROVCD2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCT_PROVCD")%>'>
										</asp:Label></td>
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
			<table style="POSITION: absolute; WIDTH: 425px; TOP: 352px; LEFT: 320px" border="0" cellPadding="0"
				width="100%">
				<TBODY>
					<tr>
						<td align="right">
							<A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
								Save</A> <A class="button2" onclick="updateClicked()" href="#">Update</A> <A class="button2" onclick="deleteClicked()" href="#">
								Delete</A>
						<td align="right"></td>
					</tr>
					<script language="javascript">
				
					</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
		</TBODY></TABLE>
	</body>
</HTML>
