<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->

		function RefreshFields()
		{				
						
			myForm.txtNP1_PROPOSAL.focus();
			if(myForm.txtNP1_PROPOSAL.disabled==true)
				 myForm.txtNP1_PROPOSAL.disabled=false;
				 myForm.txtNP1_PROPOSAL.value ="";
							 myForm.ddlNPH_FULLNAME.selectedIndex =0;
							 myForm.ddlCCN_CTRYCD.selectedIndex =0;
							 myForm.ddlUSE_USERID.selectedIndex =0;
			setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<!--<fieldset id="FldSetProposal" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 616px; BORDER-BOTTOM: #e0f3be 1px solid"><legend><Entry></legend>-->
					<TABLE id="entryTable" cellSpacing="0" cellPadding="2" border="0">
                        <tr class="form_heading">
                          <td height="20" colspan="4" >&nbsp; Proposal </td>
                        </tr>
                        <tr >
                          <td height="10.0pc" colspan="4" ></td>
                        </tr>					
						<TR id='rowNP1_PROPOSAL' class="TRow_Normal">
							<TD width="106" align="right">Country</TD>
							<TD width="186"><SHMA:dropdownlist id="ddlCCN_CTRYCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CCN_CTRYCD"
									tabIndex="3" Width="184px" CssClass="RequiredField"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvCCN_CTRYCD" runat="server" ControlToValidate="ddlCCN_CTRYCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlCCN_CTRYCD"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
							<TD width="106" align="right">Proposal</TD>
							<TD width="186"><shma:TextBox id="txtNP1_PROPOSAL" tabIndex="1" runat="server" Width='184px' ReadOnly="true" MaxLength="12"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNP1_PROPDATE' class="TRow_Alt">
							<TD id="TDtxtNP1_PROPDATE" align="right" width="110">Proposal Date</TD>
							<TD width="186">
								<SHMA:DatePopUp id="txtNP1_PROPDATE" runat="server" tabIndex="3" maxlength="0" Width='170px' CssClass="RequiredField"
									ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DatePopUp>
								<asp:CompareValidator id="msgNP1_PROPDATE" runat="server" ControlToValidate="txtNP1_PROPDATE" Operator="DataTypeCheck"
									CssClass="CalendarFormat" Type="Date" ErrorMessage="{dd/mm/yyyy} " Display="Dynamic"></asp:CompareValidator>
							</TD>
							<TD id="TDtxtNP2_COMMENDATE" align="right" width="110">Commencement Date</TD>
							<TD width="186">
								<SHMA:DatePopUp id="txtNP2_COMMENDATE" runat="server" tabIndex="4" maxlength="0" Width='170px' CssClass="RequiredField"
									ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DatePopUp>
								<asp:CompareValidator id="msgNP2_COMMENDATE" runat="server" ControlToValidate="txtNP2_COMMENDATE" Operator="DataTypeCheck"
									CssClass="CalendarFormat" Type="Date" ErrorMessage="{dd/mm/yyyy} " Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>

						<TR id='rowNPH_FULLNAME' class="TRow_Normal">
							<TD width="106" align="right">Policy Owner</TD>
							<TD width="186"><SHMA:dropdownlist id="ddlNPH_FULLNAME" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="NPH_CODE"
									tabIndex="2" Width="184px"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvNPH_FULLNAME" runat="server" ControlToValidate="ddlNPH_FULLNAME" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
							<TD width="106" align="right"><!--User--></TD>
							<TD width="186"><SHMA:dropdownlist id="ddlUSE_USERID" runat="server" BlankValue="True" DataTextField="desc_f"
									DataValueField="USE_USERID" tabIndex="4" Width="0" CssClass="RequiredField"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="ddlUSE_USERID"
									Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvUSE_USERID" runat="server" ErrorMessage="Required" ControlToValidate="ddlUSE_USERID"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>

						</TR>						
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
					</TABLE>
				<!--</fieldset>-->
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		
				myForm.ddlNPH_FULLNAME.selectedIndex =1;
				document.getElementById("ddlNPH_FULLNAME").disabled=true;
				document.getElementById("ddlCCN_CTRYCD").disabled=true;
				document.getElementById("ddlUSE_USERID").disabled=true;
		</script>
	</body>
</HTML>
