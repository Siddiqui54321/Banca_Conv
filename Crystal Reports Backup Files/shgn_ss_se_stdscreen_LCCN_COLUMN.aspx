<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LCCN_COLUMN.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LCCN_COLUMN" %>
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
			if(myForm.txtCCN_COLUMNID.disabled==true)
				 myForm.txtCCN_COLUMNID.disabled=false;
				 myForm.txtCCN_COLUMNID.value ="";
							 myForm.txtCCN_DESCRIPTION.value ="";
							 myForm.txtCCN_SQLSTATEMENT.value ="";
							 myForm.ddlCCN_DATATYPE.selectedIndex =0;
			
myForm.txtCCN_COLUMNID.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<!-- <UC:EntityHeading ParamSource="FixValue"  ParamValue="Question Detail Columns Setup"   id="EntityHeading" runat="server"></UC:EntityHeading> -->
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">Question Detail Columns Setup
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP: 20px; WIDTH: 415px; MARGIN-LEFT: 320px" id="Div1" runat="server">
				<fieldset><legend class="LegendStyle">Column Entry</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<TR id='rowCCN_COLUMNID' class="TRow_Normal">
							<TD>Column ID</TD>
							<TD><SHMA:TextBox id="txtCCN_COLUMNID" tabIndex="1" runat="server" Width='6.0pc' MaxLength="5" CssClass="RequiredField"
									BaseType="Character"></SHMA:TextBox>
								<asp:CompareValidator id="cfvCCN_COLUMNID" runat="server" ControlToValidate="txtCCN_COLUMNID" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCCN_COLUMNID" runat="server" ErrorMessage="Required" ControlToValidate="txtCCN_COLUMNID"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>
						<TR id='rowCCN_DESCRIPTION' class="TRow_Alt">
							<TD>Description</TD>
							<TD><SHMA:TextArea id="txtCCN_DESCRIPTION" tabIndex="2" runat="server" Width="20.0pc" MaxRows="5" MaxLength="60"
									CssClass="RequiredField"></SHMA:TextArea>
								<asp:CompareValidator id="cfvCCN_DESCRIPTION" runat="server" ControlToValidate="txtCCN_DESCRIPTION" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCCN_DESCRIPTION" runat="server" ErrorMessage="Required" ControlToValidate="txtCCN_DESCRIPTION"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>
						<TR id='rowCCN_SQLSTATEMENT' class="TRow_Normal">
							<TD>SQL Statement</TD>
							<TD><SHMA:TextArea id="txtCCN_SQLSTATEMENT" tabIndex="3" runat="server" Width="20.0pc" MaxRows="10"
									MaxLength="2000"></SHMA:TextArea>
								<asp:CompareValidator id="cfvCCN_SQLSTATEMENT" runat="server" ControlToValidate="txtCCN_SQLSTATEMENT"
									Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowCCN_DATATYPE' class="TRow_Alt">
							<TD>Data Type</TD>
							<TD><SHMA:dropdownlist id="ddlCCN_DATATYPE" tabIndex="4" Width="8.0pc" runat="server">
									<asp:ListItem Value=""></asp:ListItem>
									<asp:ListItem Value="A">Alpha Numeric</asp:ListItem>
									<asp:ListItem Value="D">Date</asp:ListItem>
									<asp:ListItem Value="N">Numeric</asp:ListItem>
								</SHMA:dropdownlist>
								<asp:CompareValidator id="cfvCCN_DATATYPE" runat="server" ControlToValidate="ddlCCN_DATATYPE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
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
			<DIV class="ListerDiv1" id="ListerDiv" runat="server">
				<FIELDSET id="ListerFieldSet" class="ListerFieldSet" runat="server"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('CCN_COLUMNID','Column ID')">Column ID</TD>
							<TD onClick="filterLister('CCN_DESCRIPTION','Description')">Description</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCCN_COLUMNID1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_COLUMNID")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CCN_DESCRIPTION")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCCN_COLUMNID2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_COLUMNID")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CCN_DESCRIPTION")%></td>
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
			<table style="POSITION: absolute; WIDTH: 425px; TOP: 352px; LEFT: 320px" border="0" cellPadding="0"
				width="100%">
				<tr>
					<td align="right">
						<A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
							Save</A> <A class="button2" onclick="updateClicked()" href="#">Update</A> <A class="button2" onclick="deleteClicked()" href="#">
							Delete</A>
					<td align="right"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		function setColumnPrefix(objColumn)
		{
			var columnId = objColumn.value.toUpperCase();
			if (columnId.substring(0, 2) != "FK")
			{
				columnId = "FK"+columnId;
			}
			objColumn.value = columnId
		}
		
		</script>
	</body>
</HTML>
