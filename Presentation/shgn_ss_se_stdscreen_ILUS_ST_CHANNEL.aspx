<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ST_CHANNEL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ST_CHANNEL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			if(myForm.txtCCH_CODE.disabled==true)
				 myForm.txtCCH_CODE.disabled=false;
				 myForm.txtCCH_CODE.value ="";
							 myForm.txtCCH_DESCR.value ="";
			
myForm.txtCCH_CODE.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
</HEAD>
	<body  >
		<table >
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Channel Setup
				</td>
			</tr>
		</table>		
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP:10px; MARGIN-LEFT:320px"  id="EntryTableDiv" runat="server">
				<table >
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; Entry
						</td>
					</tr>
				</table>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0" >
						<TR id='rowCCH_CODE' class="TRow_Normal">
						<TD width="40">Code</TD>
						<TD width="200"><shma:TextBox id="txtCCH_CODE"  tabIndex="1"  runat="server"  Width='4.0pc' MaxLength="5" CssClass="RequiredField"  BaseType="Character"></shma:TextBox>
										<asp:CompareValidator id="cfvCCH_CODE" runat="server"  ControlToValidate="txtCCH_CODE" Operator="DataTypeCheck"  Type="String" ErrorMessage="String Format is Incorrect "  EnableClientScript="False"  Display="Dynamic"></asp:CompareValidator>
										<asp:requiredfieldvalidator id="rfvCCH_CODE"  runat="server"  ErrorMessage="Required" ControlToValidate="txtCCH_CODE"  Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						</TR>
						
						<TR id='rowCCH_DESCR' class="TRow_Alt">
						<TD width="40">Name</TD>
						<TD width="200"><shma:TextBox id="txtCCH_DESCR"  tabIndex="2"  runat="server"  Width='8.0pc' MaxLength="100" CssClass="RequiredField"  BaseType="Character"></shma:TextBox>
										<asp:CompareValidator id="cfvCCH_DESCR" runat="server"  ControlToValidate="txtCCH_DESCR" Operator="DataTypeCheck"  Type="String" ErrorMessage="String Format is Incorrect "  EnableClientScript="False"  Display="Dynamic"></asp:CompareValidator>
										<asp:requiredfieldvalidator id="rfvCCH_DESCR"  runat="server"  ErrorMessage="Required" ControlToValidate="txtCCH_DESCR"  Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						</TR>
					<TR>
						<td>
							<P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
					
					</TABLE>

			</div>
			<DIV id="ListerDiv" class="ListerDiv1" runat="server">
				<FIELDSET id="ListerFieldSet" class="ListerFieldSet" runat="server"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
								<TD onClick="filterLister('CCH_CODE','Code')">Code</TD>	
								<TD onClick="filterLister('CCH_DESCR','Name')">Name</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
										<td >
											<asp:linkbutton ID="linkCCH_CODE1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>' CausesValidation="false"> </asp:linkbutton>
										</td>
										<td ><%# DataBinder.Eval(Container, "DataItem.CCH_DESCR")%></td>

								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td >
<asp:linkbutton ID="linkCCH_CODE2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>' CausesValidation="false"> </asp:linkbutton></td>
	<td ><%# DataBinder.Eval(Container, "DataItem.CCH_DESCR")%></td>

								</tr>
							</AlternatingItemTemplate>
						</asp:repeater></TABLE>
				</FIELDSET>
				Page no:
				<asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="RequiredField" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
			</DIV>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server" >
			<INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server" >
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick" causesvalidation="true"> 
<INPUT id="_CustomEvent1" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick" causesvalidation="false"> 

			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">

		<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
		
		<table style="LEFT: 320px; WIDTH: 425px; POSITION: absolute; TOP: 288px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
			<tr>
				<td align="right"><A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
						Save</A> <A class="button2" onclick="updateClicked()" href="#">Update</A> <A class="button2" onclick="deleteClicked()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>		
			
	</body>
</HTML>
