<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_BANCAPARAMS.aspx.cs" AutoEventWireup="True" validateRequest="false" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_BANCAPARAMS" %>
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
			if(myForm.txtCSD_TYPE.disabled==true)
				 myForm.txtCSD_TYPE.disabled=false;
				 myForm.txtCSD_TYPE.value = "";
							 myForm.txtCSD_VALUE.value = "";
			
myForm.txtCSD_TYPE.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Application Parameters
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
					<SHMA:TextBox id="txtCSH_ID" runat="server" BaseType="Character"></SHMA:TextBox>
					<asp:CompareValidator id="cfvCSH_ID" runat="server" ControlToValidate="txtCSH_ID" Operator="DataTypeCheck"
						Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
					<TR id='rowCSD_TYPE'>
						<TD width="120">Parameter</TD>
						<TD><SHMA:TextBox id="txtCSD_TYPE" tabIndex="1" runat="server" Width='15.0pc' MaxLength="15" BaseType="Character"
								CssClass="RequiredField"></SHMA:TextBox>
							<asp:requiredfieldvalidator id="rfvCSD_TYPE" runat="server" ControlToValidate="txtCSD_TYPE" ErrorMessage="Required"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<SHMA:CompareValidator id="cfvCSD_TYPE" runat="server" ControlToValidate="txtCSD_TYPE" Operator="DataTypeCheck"
								BaseType="Character"></SHMA:CompareValidator></TD>
					</TR>
					<TR id='rowCSD_VALUE'>
						<TD width="120">Value</TD>
						<TD><SHMA:TextArea id="txtCSD_VALUE" tabIndex="2" runat="server" Width="15.0pc" MaxRows="5" MaxLength="1000"></SHMA:TextArea>
							<asp:CompareValidator id="cfvCSD_VALUE" runat="server" ControlToValidate="txtCSD_VALUE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
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
							<TD onClick="filterLister('CSD_TYPE','Field')">Parameter</TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCSD_TYPE1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CSD_TYPE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td>
										<asp:Label Visible=false ID="lblCSH_ID1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CSH_ID")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCSD_TYPE2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CSD_TYPE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td>
										<asp:Label Visible=false ID="lblCSH_ID2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CSH_ID")%>'>
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
			<INPUT id="_CustomEvent1" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent" causesvalidation="false"
				runat="server" onserverclick="_CustomEvent_ServerClick"> 
            <INPUT id="_CustomEvent" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent" causesvalidation="true"
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
					<A class="button2" onclick="addClicked()" id="btnAdd" runat="server" href="#">Add 
						New</A> <A class="button2" onclick="saveClicked()" id="btnSave" runat="server" href="#">
						Save</A> <A class="button2" onclick="updateClicked()" id="btnUpdate" runat="server" href="#">
						Update</A> <A class="button2" onclick="deleteClicked()" id="btnDel" runat="server" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
