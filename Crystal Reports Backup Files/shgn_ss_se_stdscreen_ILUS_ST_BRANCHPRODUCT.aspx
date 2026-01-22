<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ST_BRANCHPRODUCT.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ST_BRANCHPRODUCT" %>
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
			if(myForm.ddlPPR_PRODCD.disabled==true)
				 myForm.ddlPPR_PRODCD.disabled=false;
			myForm.ddlPPR_PRODCD.selectedIndex =0;
			myForm.ddlPPR_PRODCD.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Branch Product(s)
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP:10px; MARGIN-LEFT:320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; Entry
						</td>
					</tr>
				</table>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<TR id='rowCCS_CODE' class="TRow_Normal">
						<TD>Product</TD>
						<TD><SHMA:dropdownlist id="ddlPPR_PRODCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
								tabIndex="6" Width="15.0pc"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="ddlPPR_PRODCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR>
						<td>
							<shma:TextBox id="txtCCH_CODE" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvCCH_CODE" runat="server" ControlToValidate="txtCCH_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="txtCCD_CODE" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvCCD_CODE" runat="server" ControlToValidate="txtCCD_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="txtCCS_CODE" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvCCS_CODE" runat="server" ControlToValidate="txtCCS_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</td>
					</TR>
					<TR>
						<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<DIV id="ListerDiv" class="ListerDiv1" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('PPR_PRODCD','Code')">Code</TD>
							<TD onClick="filterLister('PPR_DESCR','Description')">Description</TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkPPR_PRODCD1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PPR_DESCR")%></td>
									<td>
										<asp:Label Visible=false ID="lblCCH_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCD_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCS_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCS_CODE")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkPPR_PRODCD2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PPR_DESCR")%></td>
									<td>
										<asp:Label Visible=false ID="lblCCH_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCD_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblCCS_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCS_CODE")%>'>
										</asp:Label></td>
								</tr>
							</AlternatingItemTemplate>
						</asp:repeater></TABLE>
				</FIELDSET>
				Page no:
				<asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="RequiredField" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
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
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 288px; LEFT: 320px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
			<tr>
				<td align="right">
					<A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
						Save</A> 
					<!-- <A class="button2" onclick="updateClicked()" href="#">Update</A> -->
					<A class="button2" onclick="deleteClicked()" href="#">Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
