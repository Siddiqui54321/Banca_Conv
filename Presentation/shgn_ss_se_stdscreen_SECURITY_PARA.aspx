<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_SECURITY_PARA.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_SECURITY_PARA" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<!--<script language="JavaScript" src='../../shmalib/jscript/shma/Main.js'></script>-->
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
							 myForm.ddlSEC_ACTIVITYLOG .selectedIndex =0;
							 myForm.ddlSEC_LOGINLOG .selectedIndex =0;
							 myForm.txtSEC_PASSWORDEXPIRYDAYS .value ="0";
							 myForm.txtSEC_MSGBEFOREEXPIRYDAYS.value ="0";
							 myForm.txtSEC_PASSWORDHISTORYSAVED .value ="0";
							 myForm.txtSEC_PASSWORDATTEMPTSALLOWED .value ="0";
							 myForm.ddlSEC_ACTIVESCHEME .selectedIndex =0;
			

			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="SECURITY PARAMETER" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="MARGIN-TOP:10px; MARGIN-LEFT:320px"  id="EntryTableDiv" runat="server">
				<fieldset><legend>Entry</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<SHMA:TextBox id="txtPCM_COMPCODE" runat="server" visible="false" BaseType="Character"></SHMA:TextBox>
						<asp:CompareValidator id="cfvPCM_COMPCODE" runat="server" ControlToValidate="txtPCM_COMPCODE" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<SHMA:TextBox id="txtCCN_CTRYCD" visible="false" runat="server" BaseType="Character"></SHMA:TextBox>
						<asp:CompareValidator id="cfvCCN_CTRYCD" runat="server" ControlToValidate="txtCCN_CTRYCD" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<TR id='rowSEC_ACTIVITYLOG'>
							<TD>Log Activity</TD>
							<TD><SHMA:dropdownlist id="ddlSEC_ACTIVITYLOG" tabIndex="1" Width="5.0pc" runat="server">
									<asp:ListItem Value="N">No</asp:ListItem>
									<asp:ListItem Value="Y">Yes</asp:ListItem>
								</SHMA:dropdownlist>
								<asp:CompareValidator id="cfvSEC_ACTIVITYLOG" runat="server" ControlToValidate="ddlSEC_ACTIVITYLOG"
									Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="True"
									Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_LOGINLOG'>
							<TD>Login Log</TD>
							<TD><SHMA:dropdownlist id="ddlSEC_LOGINLOG" tabIndex="2" Width="5.0pc" runat="server">
									<asp:ListItem Value="N">No</asp:ListItem>
									<asp:ListItem Value="Y">Yes</asp:ListItem>
								</SHMA:dropdownlist>
								<asp:CompareValidator id="cfvSEC_LOGINLOG" runat="server" ControlToValidate="ddlSEC_LOGINLOG" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="True" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_PASSWORDEXPIRYDAYS'>
							<TD>Password Expiry Days</TD>
							<TD><SHMA:TextBox id="txtSEC_PASSWORDEXPIRYDAYS" tabIndex="3" runat="server" Width='5.0pc' MaxLength="2"
									BaseType="Number" Precision="0"></SHMA:TextBox>
								<asp:CompareValidator id="cfvSEC_PASSWORDEXPIRYDAYS" runat="server" ControlToValidate="txtSEC_PASSWORDEXPIRYDAYS"
									Operator="DataTypeCheck" Type="Integer" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_MSGBEFOREEXPIRYDAYS'>
							<TD>Msg Before Expiry Days</TD>
							<TD><SHMA:TextBox id="txtSEC_MSGBEFOREEXPIRYDAYS" tabIndex="4" runat="server" Width='5.0pc' MaxLength="2"
									BaseType="Number" Precision="0"></SHMA:TextBox>
								<asp:CompareValidator id="cfvSEC_MSGBEFOREEXPIRYDAYS" runat="server" ControlToValidate="txtSEC_MSGBEFOREEXPIRYDAYS"
									Operator="DataTypeCheck" Type="Integer" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_PASSWORDHISTORYSAVED'>
							<TD>Password History</TD>
							<TD><SHMA:TextBox id="txtSEC_PASSWORDHISTORYSAVED" tabIndex="5" runat="server" Width='5.0pc' MaxLength="2"
									BaseType="Number" Precision="0"></SHMA:TextBox>
								<asp:CompareValidator id="cfvSEC_PASSWORDHISTORYSAVED" runat="server" ControlToValidate="txtSEC_PASSWORDHISTORYSAVED"
									Operator="DataTypeCheck" Type="Integer" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_PASSWORDATTEMPTSALLOWED'>
							<TD>Password Attempts</TD>
							<TD><SHMA:TextBox id="txtSEC_PASSWORDATTEMPTSALLOWED" tabIndex="6" runat="server" Width='5.0pc' MaxLength="1"
									BaseType="Number" Precision="0"></SHMA:TextBox>
								<asp:CompareValidator id="cfvSEC_PASSWORDATTEMPTSALLOWED" runat="server" ControlToValidate="txtSEC_PASSWORDATTEMPTSALLOWED"
									Operator="DataTypeCheck" Type="Integer" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowSEC_ACTIVESCHEME'>
							<TD>Active Scheme</TD>
							<TD><SHMA:dropdownlist id="ddlSEC_ACTIVESCHEME" tabIndex="7" Width="5.0pc" runat="server">
									<asp:ListItem Value="N">No</asp:ListItem>
									<asp:ListItem Value="Y">Yes</asp:ListItem>
								</SHMA:dropdownlist>
								<asp:CompareValidator id="cfvSEC_ACTIVESCHEME" runat="server" ControlToValidate="ddlSEC_ACTIVESCHEME"
									Operator="DataTypeCheck" Type="String" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
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
			<DIV id="ListerDiv" class="ListerDiv1" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend>List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('PCM_COMPCODE','Company Code')">Company Code</TD>
							<TD onClick="filterLister('CCN_CTRYCD','Country Code')">Country Code</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:LinkButton ID="lnkPCM_COMPCODE1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.PCM_COMPCODE")%>' CommandName="Edit" CausesValidation="false">
										</asp:LinkButton></td>
									<td>
										<asp:Label ID="lblCCN_CTRYCD1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.CCN_CTRYCD")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:LinkButton ID="lnkPCM_COMPCODE2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.PCM_COMPCODE")%>' CommandName="Edit" CausesValidation="false">
										</asp:LinkButton></td>
									<td>
										<asp:Label ID="lblCCN_CTRYCD2" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.CCN_CTRYCD")%>'>
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
			<INPUT id="_CustomHeaderVal" type="hidden" name="_CustomHeaderVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick" causesvalidation="true"> 				
			<INPUT id="_CustomEvent1" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick" causesvalidation="false"> 
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
				<td align="right">
						 <A class="button2" onclick="updateClicked()" href="#">Update</A> 						
				<td align="right"></td>
			</tr>
		</table>		
	</body>
</HTML>
