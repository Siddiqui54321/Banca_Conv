<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LPVD_VALIDATIONDETAIL.aspx.cs" validateRequest="false" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LPVD_VALIDATIONDETAIL" %>
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
			if(myForm.txtPVD_LEVEL.disabled==true)
				 myForm.txtPVD_LEVEL.disabled=false;
				 myForm.txtPVD_LEVEL.value ="";
							 myForm.txtPVD_SEQUENCE.value ="";
							 myForm.ddlPVD_FIELDNATURE.selectedIndex =0;
			if(myForm.txtPVD_VALIDATIONFOR.disabled==true)
				 myForm.txtPVD_VALIDATIONFOR.disabled=false;
				 myForm.txtPVD_VALIDATIONFOR.value ="";
							 myForm.ddlPVD_DATATYPE.selectedIndex =0;
							 myForm.ddlPVD_RELOPERATOR.selectedIndex =0;
							 myForm.txtPVD_VALIDFROM.value ="";
							 myForm.txtPVD_VALIDTO.value ="";
			
myForm.txtPVD_LEVEL.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Validation Detail
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
					<SHMA:TextBox id="txtPPR_PRODCD" runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="txtPPR_PRODCD" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<SHMA:TextBox id="txtPVL_VALIDATIONFOR" runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPVL_VALIDATIONFOR" runat="server" ControlToValidate="txtPVL_VALIDATIONFOR"
						Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
						Display="Dynamic"></asp:CompareValidator>
					<SHMA:TextBox id="txtPVL_LEVEL" runat="server" BaseType="Number" Precision="0"></SHMA:TextBox>
					<asp:CompareValidator id="cfvPVL_LEVEL" runat="server" ControlToValidate="txtPVL_LEVEL" Operator="DataTypeCheck"
						Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
					<TR id='rowPVD_LEVEL'>
						<TD width="90">Level</TD>
						<TD><SHMA:TextBox id="txtPVD_LEVEL" tabIndex="1" runat="server" Width='5.0pc' MaxLength="5" CssClass="RequiredField"
								BaseType="Number" Precision="0"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVD_LEVEL" runat="server" ControlToValidate="txtPVD_LEVEL" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVD_LEVEL" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtPVD_LEVEL"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVD_SEQUENCE'>
						<TD width="90">Sequence</TD>
						<TD><SHMA:TextBox id="txtPVD_SEQUENCE" tabIndex="2" runat="server" Width='5.0pc' MaxLength="5" CssClass="RequiredField"
								BaseType="Number"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVD_SEQUENCE" runat="server" ControlToValidate="txtPVD_SEQUENCE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVD_SEQUENCE" runat="server" ErrorMessage="Required" ControlToValidate="txtPVD_SEQUENCE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVD_FIELDNATURE'>
						<TD width="90">Nature</TD>
						<TD><SHMA:dropdownlist id="ddlPVD_FIELDNATURE" tabIndex="3" Width="6.0pc" CssClass="RequiredField" runat="server">
								<asp:ListItem Selected></asp:ListItem>
								<asp:ListItem Value="D">Delimiter</asp:ListItem>
								<asp:ListItem Value="O">Operator</asp:ListItem>
								<asp:ListItem Value="V">Validation</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPVD_FIELDNATURE" runat="server" ControlToValidate="ddlPVD_FIELDNATURE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVD_FIELDNATURE" runat="server" ErrorMessage="Required" ControlToValidate="ddlPVD_FIELDNATURE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVD_VALIDATIONFOR'>
						<TD width="90">Field</TD>
						<TD><SHMA:TextBox id="txtPVD_VALIDATIONFOR" tabIndex="4" runat="server" Width='6.0pc' MaxLength="50"
								BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVD_VALIDATIONFOR" runat="server" ControlToValidate="txtPVD_VALIDATIONFOR"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowPVD_DATATYPE'>
						<TD width="90">Data Type</TD>
						<TD><SHMA:dropdownlist id="ddlPVD_DATATYPE" tabIndex="5" Width="5.0pc" CssClass="RequiredField" runat="server">
								<asp:ListItem Selected></asp:ListItem>
								<asp:ListItem Value="C">Character</asp:ListItem>
								<asp:ListItem Value="D">Date</asp:ListItem>
								<asp:ListItem Value="N">Numeric</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPVD_DATATYPE" runat="server" ControlToValidate="ddlPVD_DATATYPE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVD_DATATYPE" runat="server" ErrorMessage="Required" ControlToValidate="ddlPVD_DATATYPE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVD_RELOPERATOR'>
						<TD width="90">Operator</TD>
						<TD><SHMA:dropdownlist id="ddlPVD_RELOPERATOR" tabIndex="6" Width="5.0pc" runat="server">
								<asp:ListItem Value=""></asp:ListItem>
								<asp:ListItem Value="BETWEEN">Between</asp:ListItem>
								<asp:ListItem Value="=">Equals</asp:ListItem>
								<asp:ListItem Value="<">Less Than</asp:ListItem>
								<asp:ListItem Value="<=">Less or Equal</asp:ListItem>
								<asp:ListItem Value=">">Greater Than</asp:ListItem>
								<asp:ListItem Value=">=">Greater or Equal</asp:ListItem>
								<asp:ListItem Value="<>">Not Equal To</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvPVD_RELOPERATOR" runat="server" ControlToValidate="ddlPVD_RELOPERATOR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowPVD_VALIDFROM'>
						<TD width="90">From</TD>
						<TD><SHMA:TextBox id="txtPVD_VALIDFROM" tabIndex="7" runat="server" Width='6.0pc' MaxLength="1000"
								CssClass="RequiredField" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVD_VALIDFROM" runat="server" ControlToValidate="txtPVD_VALIDFROM" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvPVD_VALIDFROM" runat="server" ErrorMessage="Required" ControlToValidate="txtPVD_VALIDFROM"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowPVD_VALIDTO'>
						<TD width="90">To</TD>
						<TD><SHMA:TextBox id="txtPVD_VALIDTO" tabIndex="8" runat="server" Width='6.0pc' MaxLength="1000" BaseType="Character"></SHMA:TextBox>
							<asp:CompareValidator id="cfvPVD_VALIDTO" runat="server" ControlToValidate="txtPVD_VALIDTO" Operator="DataTypeCheck"
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
			<DIV class="ListerDiv" id="ListerDiv" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('PVD_SEQUENCE','Sequence')">Sequence</TD>
							<TD onClick="filterLister('PVD_VALIDATIONFOR','Field')">Field</TD>
							<TD onClick="filterLister('PVD_RELOPERATOR','Operator')">Operator</TD>
							<TD onClick="filterLister('PVD_VALIDFROM','Range From')">Range From</TD>
							<TD onClick="filterLister('PVD_VALIDTO','Range To')">Range To</TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_SEQUENCE")%></td>
									<td>
										<asp:linkbutton ID="linkPVD_VALIDATIONFOR1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PVD_VALIDATIONFOR")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_RELOPERATOR")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_VALIDFROM")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_VALIDTO")%></td>
									<td>
										<asp:Label Visible=false ID="lblPPR_PRODCD1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVL_VALIDATIONFOR1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_VALIDATIONFOR")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVL_LEVEL1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_LEVEL")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVD_LEVEL1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVD_LEVEL")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_SEQUENCE")%></td>
									<td>
										<asp:linkbutton ID="linkPVD_VALIDATIONFOR2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PVD_VALIDATIONFOR")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_RELOPERATOR")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_VALIDFROM")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.PVD_VALIDTO")%></td>
									<td>
										<asp:Label Visible=false ID="lblPPR_PRODCD2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVL_VALIDATIONFOR2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_VALIDATIONFOR")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVL_LEVEL2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVL_LEVEL")%>'>
										</asp:Label></td>
									<td>
										<asp:Label Visible=false ID="lblPVD_LEVEL2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PVD_LEVEL")%>'>
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
			<INPUT id="_CustomEvent" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" causesvalidation="true"
				runat="server" onserverclick="_CustomEvent_ServerClick">
                <INPUT id="_CustomEvent1" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" causesvalidation="false"
				runat="server" onserverclick="_CustomEvent_ServerClick"> 
                 <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
			<script language="javascript">
				
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 315px; LEFT: 320px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
					<A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
						Save</A> <A class="button2" onclick="updateClicked()" href="#">Update</A> <A class="button2" onclick="deleteClicked()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
