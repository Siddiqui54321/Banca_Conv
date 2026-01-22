<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_LCQN_QUESTIONAIRE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_LCQN_QUESTIONAIRE" %>
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
			if(myForm.txtCQN_CODE.disabled==true)
				 myForm.txtCQN_CODE.disabled=false;
				 myForm.txtCQN_CODE.value ="";
							 myForm.txtCQN_DESC.value ="";
							 myForm.txtCQN_CONDITION.value ="";
			
myForm.txtCQN_CODE.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<!--<UC:EntityHeading ParamSource="FixValue" ParamValue="Question(s) Setup" id="EntityHeading" runat="server"></UC:EntityHeading>-->
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Questionnaire Setup
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post"	runat="server">
			<div style="MARGIN-TOP: 20px; MARGIN-LEFT: 320px; WIDTH=415" id="EntryTableDiv" runat="server">
					<table>
						<tr class="form_heading">
							<td height="20" colSpan="6">&nbsp; Question Entry
							</td>
						</tr>
					</table>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<TR id='rowCQN_CODE' class="TRow_Normal">
							<TD width="10%">Code</TD>
							<TD width="90%"><SHMA:TextBox id="txtCQN_CODE" tabIndex="1" runat="server" Width='6.0pc' MaxLength="16" BaseType="Character"></SHMA:TextBox>
								<asp:CompareValidator id="cfvCQN_CODE" runat="server" ControlToValidate="txtCQN_CODE" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCQN_CODE"  runat="server"  ErrorMessage="Required" ControlToValidate="txtCQN_CODE"  Display="Dynamic"></asp:requiredfieldvalidator>	
							</TD>
						</TR>
						<TR id='rowCQN_DESC' class="TRow_Alt">
							<TD>Description</TD>
							<TD><SHMA:TextArea id="txtCQN_DESC" tabIndex="2" runat="server" Width="20.0pc" MaxRows="10" MaxLength="1000"></SHMA:TextArea>
								<asp:CompareValidator id="cfvCQN_DESC" runat="server" ControlToValidate="txtCQN_DESC" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCQN_DESC"  runat="server"  ErrorMessage="Required" ControlToValidate="txtCQN_DESC"  Display="Dynamic"></asp:requiredfieldvalidator>									
							</TD>
						</TR>
						<TR id='rowCQN_CONDITION' class="TRow_Normal">
							<TD>Condition</TD>
							<TD><SHMA:TextArea id="txtCQN_CONDITION" tabIndex="3" runat="server" Width="20.0pc" MaxRows="5" MaxLength="500"></SHMA:TextArea>
								<asp:CompareValidator id="cfvCQN_CONDITION" runat="server" ControlToValidate="txtCQN_CONDITION" Operator="DataTypeCheck"
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
				<FIELDSET id="ListerFieldSet" class="ListerFieldSet" runat="server"><legend class="LegendStyle">Question 
						List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0" >
						<TR class="ListerHeader">
							<TD width="10%" onClick="filterLister('CQN_CODE','Code')">Code</TD>
							<TD width="90%" onClick="filterLister('CQN_DESC','Description')">Description</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCQN_CODE1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_CODE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CQN_DESC")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkCQN_CODE2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_CODE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CQN_DESC")%></td>
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
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 370px; LEFT: 320px" border="0" cellPadding="0"
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
