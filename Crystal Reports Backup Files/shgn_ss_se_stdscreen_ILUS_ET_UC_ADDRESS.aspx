<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" EnableEventValidation="false"  Codebehind="shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
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
			//if(myForm.txtNP1_PROPOSAL.disabled==true)
			//	 myForm.txtNP1_PROPOSAL.disabled=false;
			//	 myForm.txtNP1_PROPOSAL.value ="";
			//				 myForm.ddlNPH_FULLNAME.selectedIndex =0;
			//				 myForm.ddlCCN_CTRYCD.selectedIndex =0;
			//				 myForm.ddlUSE_USERID.selectedIndex =0;
			
			//myForm.txtNP1_PROPOSAL.focus();
			//setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		
function saveUpdate()
			{
				if ( this.Page_ClientValidate()){
				__doPostBack("btn_save", "btn_save_Click");
				}
			
			}
			
function callUpdate()
	{
		if (parent.frames[0].Page_ClientValidate())
		{
			parent.openWait('Saving Data');
			parent.frames[0].saveUpdate();
			//saveUpdate();
			//parent.closeWait();
		}
		
	}			
			
		</script>
	</HEAD>
	<body>
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div style="Z-INDEX: 0" id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server">
				<!--<fieldset id="FldSetProposal" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 616px; BORDER-BOTTOM: #e0f3be 1px solid"><legend><Entry></legend>-->
				<TABLE id="entryTable" runat="server" border="0" cellSpacing="0" cellPadding="2">
					<tr>
						<td height="10" colSpan="4">
							<P>&nbsp;|
								<asp:linkbutton style="Z-INDEX: 0" id="LinkButton3" runat="server"  Font-Size="X-Small" 
									CausesValidation="False" onclick="LinkButton3_Click">Correspondence Address</asp:linkbutton>&nbsp;|
								<asp:linkbutton style="Z-INDEX: 0" id="LinkButton1" runat="server" Font-Size="X-Small" 
									BorderColor="White" CausesValidation="False" onclick="LinkButton1_Click">Permanent Address</asp:linkbutton>
								<asp:linkbutton style="Z-INDEX: 0" id="LinkButton2" runat="server" Font-Size="X-Small"
									CausesValidation="False" onclick="LinkButton2_Click">Business Address</asp:linkbutton></P>
							<P>&nbsp;</P>
						</td>
					</tr>
					<TR id="rowNP1_COUNTRY" class="TRow_Normal">
						<TD style="WIDTH: 138px; HEIGHT: 22px" width="138" align="right">
							<P>Country</P>
						</TD>
						<TD style="HEIGHT: 22px" width="186">
							<P><SHMA:dropdownlist id="ddlCCN_COUNTRY" tabIndex="2" runat="server" DataTextField="CCN_DESCR" DataValueField="CCN_CTRYCD"
									Width="184px" AutoPostBack="True" BlankValue="True" CssClass="RequiredField" BackColor="White" onselectedindexchanged="ddlCCN_COUNTRY_SelectedIndexChanged"></SHMA:dropdownlist>
								<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCCN_COUNTRY" Display="Dynamic"
									ErrorMessage="Required"></asp:RequiredFieldValidator></P>
						</TD>
						<TD style="WIDTH: 99px; HEIGHT: 22px" width="99" align="right">Tel Res</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_TELRES" tabIndex="8" runat="server" Width="184px" BaseType="Character"
											Precision="2" MaxLength="200" CssClass="RequiredField"></shma:textbox>
										<asp:comparevalidator id="cmpNP1_TELRES" runat="server" ControlToValidate="txtNP1_TELRES" Operator="DataTypeCheck" 
											Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
								</TD>
					</TR>
					<TR id="rowNP1_PROVINCE" class="TRow_Alt">
						<TD style="WIDTH: 138px" width="138" align="right">Province/State</TD>
						<TD width="186"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCCN_PROVINCE" tabIndex="2" runat="server" DataTextField="CPR_DESCR"  
								AutoPostBack="True" DataValueField="CPR_PROVCD" Width="184px" onselectedindexchanged="ddlCCN_PROVINCE_SelectedIndexChanged"></asp:dropdownlist>
						</TD>
						<TD style="WIDTH: 99px" width="99" align="right">Office</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_OFFICE" tabIndex="9" runat="server" Width="184px" BaseType="Character"
											MaxLength="200"></shma:textbox>
										<asp:comparevalidator id="cmpNP1_OFFICE" runat="server" ControlToValidate="txtNP1_OFFICE" Operator="DataTypeCheck" 
											Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
								
						</TD>
					</TR>
					<TR id="rowNP1_CITY" class="TRow_Normal">
						<TD style="WIDTH: 138px" width="138" align="right">City</TD>
						<TD width="186"><asp:dropdownlist id="ddlCCN_CITY" tabIndex="3" runat="server" DataTextField="CCT_DESCR" DataValueField="CCT_CITYCD"
								Width="184px" CssClass="RequiredField"></asp:dropdownlist>
						</TD>
						<TD style="WIDTH: 99px" width="99" align="right">Fax No</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_FAXNO" tabIndex="10" runat="server" Width="184px" BaseType="Character"
											MaxLength="200" CssClass="RequiredField"></shma:textbox>
										<asp:comparevalidator id="cmpNP1_FAXNO" runat="server" ControlToValidate="txtNP1_FAXNO" Operator="DataTypeCheck" 
											Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>	
										</TD>
					</TR>
					<TR id="rowNP1_ADDRESS1" class="TRow_Alt">
						<TD style="WIDTH: 138px; HEIGHT: 22px" width="138" align="right">Address 1</TD>
						<TD style="HEIGHT: 22px" width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_ADDRESS1" tabIndex="4" runat="server" Width="184px"
								Precision="2" MaxLength="200"></shma:textbox>
							<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtNP1_ADDRESS1"
								Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>	
						</TD>
						<TD style="WIDTH: 99px; HEIGHT: 22px" width="99" align="right">Mobile</TD>
						<TD style="HEIGHT: 22px" width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_MOBILENO" tabIndex="11" runat="server" Width="184px" BaseType="Character"
																MaxLength="200"></shma:textbox>
															<asp:comparevalidator id="cmpNP1_MOBILENO" runat="server" ControlToValidate="txtNP1_MOBILENO" Operator="DataTypeCheck" 
																Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
								 </TD>
					</TR>
					<TR id="rowNP1_ADDRESS2" class="TRow_Normal">
						<TD style="WIDTH: 138px" width="138" align="right">Address 2</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_ADDRESS2" tabIndex="5" runat="server" Width="184px"
								Precision="2" MaxLength="200" CssClass="RequiredField"></shma:textbox></TD>
						<TD style="WIDTH: 99px" width="99" align="right">Pager</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_PAGER" tabIndex="12" runat="server" Width="184px" BaseType="Character"
												MaxLength="200" CssClass="RequiredField"></shma:textbox>
										<asp:comparevalidator id="cmpNP1_PAGER" runat="server" ControlToValidate="txtNP1_PAGER" Operator="DataTypeCheck" 
												Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>

								</TD>
					</TR>
					<TR id="rowNP1_ADDRESS3" class="TRow_Alt">
						<TD style="WIDTH: 138px" width="138" align="right">Address 3</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_ADDRESS3" tabIndex="6" runat="server" Width="184px"
								Precision="2" MaxLength="200"></shma:textbox></TD>
						<TD style="WIDTH: 99px" width="99" align="right">Email 1</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_EMAIL1" tabIndex="13" runat="server" Width="184px"
								Precision="2" MaxLength="200"></shma:textbox><asp:regularexpressionvalidator style="Z-INDEX: 0" id="RegularExpressionValidator2" runat="server" ErrorMessage="Email address not valid"
								ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ControlToValidate="txtNP1_EMAIL1"></asp:regularexpressionvalidator></TD>
					</TR>
					<TR id="rowNP1_POBOX" class="TRow_Normal">
						<TD style="WIDTH: 138px" width="138" align="right">P.O Box</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_POBOX" tabIndex="7" runat="server" Width="184px" BaseType="Character" 
											MaxLength="200" CssClass="RequiredField"></shma:textbox>
										<asp:comparevalidator id="cmpNP1_POBOX" runat="server" ControlToValidate="txtNP1_POBOX" Operator="DataTypeCheck" 
											Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
								</TD>
						<TD style="WIDTH: 99px" width="99" align="right">Email 2</TD>
						<TD width="186"><shma:textbox style="Z-INDEX: 0" id="txtNP1_EMAIL2" tabIndex="14" runat="server" Width="184px"
								Precision="2" MaxLength="200" CssClass="RequiredField"></shma:textbox><asp:regularexpressionvalidator style="Z-INDEX: 0" id="RegularExpressionValidator1" runat="server" ErrorMessage="Email address not valid"
								ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ControlToValidate="txtNP1_EMAIL2"></asp:regularexpressionvalidator></TD>
					</TR>
					<tr>
					</tr>

					<TR id="rowNP1_TELRES" class="button2TD" align="right">
						<TD style="WIDTH: 138px" width="138" align="right">
							<P><asp:label style="Z-INDEX: 0" id="lblServerError" runat="server" ForeColor="Red" EnableViewState="false"
									Visible="False"></asp:label></P>
						</TD>
						<TD width="186"></TD>
						<TD style="WIDTH: 99px" width="99" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD width="186">
							<P>
								<a href="#" class="button2" onclick="saveUpdate();">&nbsp;&nbsp;Save&nbsp;&nbsp;</a></P>
						</TD>
					</TR>
					<TR id="rowNP1_OFFICE" class="" style="display:none;">
						<TD style="WIDTH: 138px; HEIGHT: 38px" width="138" align="right">
							<asp:imagebutton style="Z-INDEX: 0" id="btn_save" runat="server" Width="64px" Height="16px" ImageUrl="Images/savee.JPG"
								ImageAlign="Right" Visible="False" onclick="btn_save_Click"></asp:imagebutton></TD>
						<TD style="HEIGHT: 38px" width="186">&nbsp;</TD>
						<TD style="WIDTH: 99px; HEIGHT: 38px" width="99" align="right">
							<P>&nbsp;&nbsp;&nbsp;</P>
						</TD>
						<TD style="HEIGHT: 38px" width="186">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR>
				</TABLE> <!--</fieldset>--></div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> 
			<INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<SCRIPT language="javascript">
				
		</SCRIPT>
		</form>
		<SCRIPT language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		
				//myForm.ddlNPH_FULLNAME.selectedIndex =1;
				//document.getElementById("ddlNPH_FULLNAME").disabled=true;
				//document.getElementById("ddlCCN_CTRYCD").disabled=true;
				//document.getElementById("ddlUSE_USERID").disabled=true;
		</SCRIPT>
	</body>
</HTML>
