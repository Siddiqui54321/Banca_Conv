<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LPVL_VALIDATION_DECISION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LPVL_VALIDATION_DECISION" %>
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
			if(myForm.txtPVL_LEVEL.disabled==true)
				 myForm.txtPVL_LEVEL.disabled=false;
				 myForm.txtPVL_LEVEL.value ="0";
							 myForm.ddlPVL_VALIDRANGE.selectedIndex =0;
							 myForm.txtPVL_VALUECOMB.value ="";
			
myForm.txtPVL_LEVEL.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Decision (Decision Setup)
				</td>
			</tr>
		</table>		
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP: 20px; MARGIN-LEFT: 320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; Entry</td>
					</tr>
				</table>				
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<SHMA:TextBox id="txtPPR_PRODCD" runat="server" BaseType="Character" Width="0.0pc"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="txtPPR_PRODCD" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<SHMA:TextBox id="txtPVL_VALIDATIONFOR" runat="server" BaseType="Character" Width="0.0pc"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPVL_VALIDATIONFOR" runat="server" ControlToValidate="txtPVL_VALIDATIONFOR"
						Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
						Display="Dynamic"></asp:CompareValidator>
					<TR id='rowPVL_LEVEL'>
						<TD width="100.0pc">Level</TD>
						<TD><SHMA:TextBox id="txtPVL_LEVEL" tabIndex="1" runat="server" Width='5.0pc' MaxLength="5" BaseType="Number"
								Precision="0"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVL_LEVEL" runat="server" ControlToValidate="txtPVL_LEVEL" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowPVL_VALIDRANGE'>
						<TD width="100.0pc">Decision</TD>
						<TD><SHMA:dropdownlist id="ddlPVL_VALIDRANGE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CDC_CODE"
								tabIndex="2" Width="15.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPVL_VALIDRANGE" runat="server" ControlToValidate="ddlPVL_VALIDRANGE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVL_VALIDRANGE" runat="server" ErrorMessage="Required" ControlToValidate="ddlPVL_VALIDRANGE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVL_VALUECOMB'>
						<TD width="100.0pc">Value Comb</TD>
						<TD><SHMA:TextArea id="txtPVL_VALUECOMB" tabIndex="3" runat="server" Width="15.0pc" MaxRows="5" MaxLength="1000"></SHMA:TextArea>
							<asp:CompareValidator id="cfvPVL_VALUECOMB" runat="server" ControlToValidate="txtPVL_VALUECOMB" Operator="DataTypeCheck"
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
			<DIV class="ListerDiv1" id="ListerDiv" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('PVL_LEVEL','Level')">Level</TD>
							<TD onClick="filterLister('LCDC.CDC_DESCRIPTION','Decision')">Decision</TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkPVL_LEVEL1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_LEVEL")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CDC_DESCRIPTION")%></td>
									<td>
										<asp:Label Visible=false ID="lblPPR_PRODCD1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>'>
										</asp:Label></td>
										
									<td>
										<asp:Label Visible=false ID="lblPVL_VALIDATIONFOR1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_VALIDATIONFOR")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkPVL_LEVEL2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_LEVEL")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CDC_DESCRIPTION")%></td>
									<td>
										<asp:Label Visible=false ID="lblPPR_PRODCD2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>'>
										</asp:Label></td>										
									<td>
										<asp:Label Visible=false ID="lblPVL_VALIDATIONFOR2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_VALIDATIONFOR")%>'>
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
					<A class="button2" onclick="addClicked()" href="#">Add New</A> 
					<A class="button2" onclick="saveClicked()" href="#">Save</A> 
					<A class="button2" onclick="updateClicked()" href="#">Update</A> 
					<A class="button2" onclick="deleteClicked()" href="#">Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
