<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LPVF_PPRID_DESCRIPTION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LPVF_PPRID_DESCRIPTION" %>
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
							 myForm.txtPVF_FIELDCOMB.value ="";
			
myForm.ddlPPR_PRODCD.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Mandatory/Forbidden Rider Setup
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP: 20px; MARGIN-LEFT: 320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; Rider Entry
						</td>
					</tr>
				</table>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<SHMA:TextBox id="txtPVF_CODE" width='0.0pc' runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPVF_CODE" runat="server" ControlToValidate="txtPVF_CODE" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					
					<TR id='rowPPR_PRODCD' class="TRow_Normal">
						<TD width="120">Product</TD>
						<TD><SHMA:dropdownlist id="ddlPPR_PRODCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
								tabIndex="1" Width="16.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="ddlPPR_PRODCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					
					<TR id='rowPVF_FIELDCOMB'  class="TRow_Alt">
						<TD width="120">Field Combination</TD>
						<TD><SHMA:TextArea id="txtPVF_FIELDCOMB" tabIndex="2" runat="server" Width="16.0pc" MaxRows="5" MaxLength="1000"></SHMA:TextArea>
							<asp:CompareValidator id="cfvPVF_FIELDCOMB" runat="server" ControlToValidate="txtPVF_FIELDCOMB" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</TD>
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
							<TD onClick="filterLister('LPVF.PPR_PRODCD','Product')">Product</TD>
							<TD onClick="filterLister('LPPR.PPR_DESCR','Description')">Description</TD>
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
										<asp:Label Visible=false ID="lblPVF_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVF_CODE")%>'>
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
										<asp:Label Visible=false ID="lblPVF_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVF_CODE")%>'>
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
					<A class="button2" onclick="addClicked()" href="#">Add New</A> 
					<A class="button2" onclick="saveClicked()" href="#">Save</A> 
					<A class="button2" onclick="updateClicked()" href="#">Update</A> 
					<A class="button2" onclick="deleteClicked()" href="#">Delete</A>
				<td align="right"></td>
			</tr>
		</table>		
	</body>
</HTML>
