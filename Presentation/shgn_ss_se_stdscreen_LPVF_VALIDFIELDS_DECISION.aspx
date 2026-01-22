<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LPVF_VALIDFIELDS_DECISION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LPVF_VALIDFIELDS_DECISION" %>
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
			if(myForm.ddlPVF_CODE.disabled==true)
				 myForm.ddlPVF_CODE.disabled=false;
				 myForm.ddlPVF_CODE.selectedIndex =0;
							 myForm.txtPFV_FIELDCOMB.value ="";
			
myForm.ddlPPR_PRODCD.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Product (Decision Setup)
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
						<SHMA:TextBox id="txtPVF_TYPE" runat="server" BaseType="Character" Width="0.0pc" style="visibility:hidden"></SHMA:TextBox>
						<asp:CompareValidator id="cfvPVF_TYPE" runat="server" ControlToValidate="txtPVF_TYPE" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<TR id='rowPPR_PRODCD'>
							<TD width="100.0pc">Product</TD>
							<TD><SHMA:dropdownlist id="ddlPPR_PRODCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="PPR_PRODCD"
									tabIndex="1" Width="20.0pc"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvPPR_PRODCD" runat="server" ControlToValidate="ddlPPR_PRODCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowPVF_CODE' style="display:none;">
							<TD width="100.0pc">Validation&nbsp;Field</TD>
							<TD><SHMA:dropdownlist id="ddlPVF_CODE" runat="server" BlankValue="False" DataTextField="desc_f" DataValueField="VFS_CODE"
									tabIndex="2" Width="20.0pc"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvPVF_CODE" runat="server" ControlToValidate="ddlPVF_CODE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowPFV_FIELDCOMB'>
							<TD width="100.0pc">Field&nbsp;Comb</TD>
							<TD><SHMA:TextArea id="txtPFV_FIELDCOMB" tabIndex="3" runat="server" Width="20.0pc" MaxRows="5" MaxLength="1000"
									CssClass="RequiredField"></SHMA:TextArea>
								<asp:CompareValidator id="cfvPFV_FIELDCOMB" runat="server" ControlToValidate="txtPFV_FIELDCOMB" Operator="DataTypeCheck"
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
							<TR class="ListerHeader">
							<TD onClick="filterLister('PPR_PRODCD','Product')">Product</TD>
							<TD onClick="filterLister('PPR_DESCR','Description')">Description</TD>
						</TR>
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
			<INPUT id="_CustomEvent" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" causesvalidation="true"
				runat="server" onserverclick="_CustomEvent_ServerClick">
            <INPUT id="_CustomEvent1" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" causesvalidation="false"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
			<script language="javascript">
				
		</script>
		</form>
		
		<script language="javascript">
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		
			if (_lastEvent == 'New')	
				addClicked(); 	
			
			fcStandardFooterFunctionsCall();
			
			function beforeSave()
			{
				getField("PVF_CODE").value = "DECISION";
				return true;
			}
			
			
			function beforeUpdate()
			{
				return beforeSave();
			}
			
		</script>
		
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 288px; LEFT: 320px" border="0" cellPadding="0"
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
