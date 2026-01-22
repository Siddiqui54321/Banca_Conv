<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_UM_USERMANAGMENT2.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_UM_USERMANAGMENT2" smartNavigation="False" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!-- <LINK rel="stylesheet" type="text/css" href="Styles/Style.css"> -->
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
			if(myForm.txtUSE_USERID.disabled==true)
				 myForm.txtUSE_USERID.disabled=false;
				 myForm.txtUSE_USERID.value ="";
				 myForm.txtUSE_NAME.value ="";
				 myForm.txtUSE_USERID.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; User Profile
				</td>
			</tr>
		</table>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div style="MARGIN-TOP: 20px; MARGIN-LEFT: 320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" >&nbsp; User Entry
						</td>
					</tr>
				</table>
				<TABLE id="entryTable" border="0" cellSpacing="5" cellPadding="1">
					<TR id="rowUSE_USERID" class="TRow_Normal">
						<TD width="40">User&nbsp;ID</TD>
						<TD width="100"><shma:textbox id="txtUSE_USERID" tabIndex="1" runat="server" BaseType="Character" CssClass="RequiredField"
								MaxLength="10" Width="8.0pc"></shma:textbox><asp:comparevalidator id="cfvUSE_USERID" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="txtUSE_USERID"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvUSE_USERID" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtUSE_USERID"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR id="rowUSE_NAME" class="TRow_Alt">
						<TD width="40">User&nbsp;Name</TD>
						<TD width="170"><shma:textbox id="txtUSE_NAME" tabIndex="2" runat="server" BaseType="Character" MaxLength="25"
								Width="16pc"></shma:textbox><asp:comparevalidator id="cfvUSE_NAME" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="txtUSE_NAME"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvUSE_NAME" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtUSE_NAME"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR>
						<td>
							<P><asp:label id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red" Visible="False"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<DIV id="ListerDiv" class="ListerDiv1" runat="server">
				<FIELDSET id="ListerFieldSet" class="ListerFieldSet" runat="server"><legend class="LegendStyle">User 
						List</legend>
					<TABLE class="Lister" border="0" cellSpacing="2" cellPadding="0">
						<TR class="ListerHeader">
							<TD onclick="filterLister('USE_USERID','User ID')">User ID</TD>
							<TD onclick="filterLister('USE_NAME','User Name')">User Name</TD>
						</TR>
						<asp:repeater id="lister" runat="server" >
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
				<asp:dropdownlist id="pagerList" runat="server" CssClass="RequiredField" AutoPostBack="True" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist></DIV>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<script language="javascript">
				
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 110px; LEFT:205px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
			</td>
			</tr>
			
			<tr>
				<td align="right">
					 <A class="button2" onclick="updateClicked()" href="#">Reset Password</A>
					<td align="right"></td>
			</tr>
			
		</table>
	</body>
</HTML>
