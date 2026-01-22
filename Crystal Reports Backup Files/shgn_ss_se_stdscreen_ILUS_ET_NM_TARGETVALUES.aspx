<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_TARGETVALUES.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_TARGETVALUES" %>
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
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralFunctions.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralView.js'></SCRIPT>
		<script language="javascript">
		
		function _callback()
		{
			//parent.closeWait();
		}
		
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->

		function RefreshFields()
		{				
							 myForm.txtNPR_SUMASSURED.value ="0";
							 myForm.txtNPR_TOTPREM.value ="0";
							 myForm.txtNPR_EXCESSPREMIUM.value ="";
							 myForm.txtNP1_PERIODICPREM.value ="0";
							 myForm.ddlNPR_INCLUDELOADINNIV.selectedIndex =0;
							 myForm.txtNPR_PAIDUPTOAGE.value ="";
							 myForm.txtNPR_PREMIUMTER.value ="";
							 myForm.ddlPCU_AVCURRCODE.selectedIndex =0;
							 myForm.txtNP1_RETIREMENTAGE.value ="";
							 myForm.txtNP1_TARGETATTAINAGE.value ="";
							 myForm.txtNP1_TARGETRETURNYEAR.value ="";
			

			setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<UC:ENTITYHEADING id="EntityHeading" ParamSource="FixValue" ParamValue="Planned Period Premium Details &amp; Target Criteria"
			runat="server"></UC:ENTITYHEADING>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<!--<fieldset id="FldSetPPP" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; LEFT: 0px; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 626px; BORDER-BOTTOM: #e0f3be 1px solid; HEIGHT: 164px"><legend style="WIDTH: 225px; HEIGHT: 8px">Entry
						Planned Period Premium Details</legend></fieldset>-->
				<TABLE id="entryTable" cellSpacing="0" cellPadding="2" border="0" style="WIDTH: 99%">
					<tr class="form_heading">
						<td height="20" colspan="4">&nbsp; Planned Period Premium Details
						</td>
					</tr>
					<tr>
						<td height="10" colspan="4"></td>
					</tr>
					<TR class="TRow_Normal" id="rowPCU_CURRDESC">
						<TD align="right" width="106">Face Currency</TD>
						<TD width="186">
							<shma:textbox id="txtPCU_CURRDESC" tabIndex="1" runat="server" ReadOnly="true" Width="184px" CssClass="DisplayOnly"></shma:textbox>
						</TD>
						<TD align="right" width="106">Face Amount</TD>
						<TD width="186"><shma:textbox id="txtNPR_SUMASSURED" tabIndex="1" runat="server" BaseType="Number" Precision="0"
								MaxLength="15" ReadOnly="true" Width="184px" CssClass="DisplayOnly"></shma:textbox>
							<asp:comparevalidator id="cfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED"></asp:comparevalidator></TD>
					</TR>
					<TR class="TRow_Alt" id="rowNPR_BENEFITTERM">
						<TD align="right" width="106">Benefit Term</TD>
						<TD width="186">
							<shma:textbox id="txtNPR_BENEFITTERM" tabIndex="1" runat="server" ReadOnly="true" Width="184px"
								CssClass="DisplayOnly" BaseType="Number" Precision="0"></shma:textbox>
						</TD>
						<TD align="right" width="106">Basic Premium</TD>
						<TD width="186">
							<shma:textbox id="txtNPR_PREMIUM" tabIndex="1" runat="server" ReadOnly="true" Width="184px" CssClass="DisplayOnly"
								BaseType="Number" Precision="0"></shma:textbox>
							<asp:comparevalidator id="cfvNPR_PREMIUM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUM"></asp:comparevalidator></TD>
						</TD>
					</TR>
					<TR id="rowNPR_PREMIUMTER" class="TRow_Normal">
						<TD id="TDtxtNPR_PREMIUMTER" align="right" width="106">Premium Term</TD>
						<TD width="186"><shma:textbox id="txtNPR_PREMIUMTER" tabIndex="7" runat="server" BaseType="Number" Precision="0"
								MaxLength="3" Width="184px" CssClass="RequiredField"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUMTER" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMTER"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_PREMIUMTER" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_PREMIUMTER" Precision="0"></asp:requiredfieldvalidator></TD>
						<td></td>
						<td></td>
					</TR>
					<TR id="rowNPR_EXCESSPREMIUM" class="TRow_Alt">
						<TD id="TDddlNPR_INCLUDELOADINNIV" align="right" width="106">Excess Premium Type</TD>
						<TD><SHMA:DROPDOWNLIST id="ddlNPR_INCLUDELOADINNIV" tabIndex="5" runat="server" Width="184px" CssClass="RequiredField"
								DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPR_INCLUDELOADINNIV"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_INCLUDELOADINNIV"></asp:requiredfieldvalidator></TD>
						<TD id="TDtxtNPR_EXCESSPREMIUM" align="right" width="106">Excess Premium</TD>
						<TD width="186"><shma:textbox id="txtNPR_EXCESSPREMIUM" tabIndex="3" runat="server" BaseType="Number" Precision="0"
								MaxLength="17" Width="184px" CssClass="RequiredField"></shma:textbox><asp:comparevalidator id="cfvNPR_EXCESSPREMIUM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_EXCESSPREMIUM"></asp:comparevalidator></TD>
					</TR>
					<TR id="rowNPR_PAIDUPTOAGE" class="TRow_Normal">
						<TD id="TDtxtNPR_PAIDUPTOAGE" align="right" width="106">Premium Paid upto Age</TD>
						<TD width="186"><shma:textbox id="txtNPR_PAIDUPTOAGE" tabIndex="6" runat="server" BaseType="Number" Precision="0"
								MaxLength="3" Width="184px" CssClass="RequiredField"></shma:textbox><asp:comparevalidator id="cfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PAIDUPTOAGE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_PAIDUPTOAGE" Precision="0"></asp:requiredfieldvalidator></TD>
						<TD id="TDddlPCU_AVCURRCODE" align="right" width="106">AV Currency</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlPCU_AVCURRCODE" tabIndex="8" runat="server" Width="184px" CssClass="RequiredField"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_AVCURRCODE" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_AVCURRCODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_AVCURRCODE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_AVCURRCODE"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Alt" id="rowNP1_PERIODICPREM">
						<TD align="right" width="106">Planned Periodic Premium</TD>
						<TD width="186"><shma:textbox id="txtNP1_PERIODICPREM" tabIndex="4" runat="server" BaseType="Number" Precision="0"
								MaxLength="17" ReadOnly="true" Width="184px" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNP1_PERIODICPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_PERIODICPREM"></asp:comparevalidator></TD>
						<TD align="right" width="106">Total Premium</TD>
						<TD><shma:textbox id="txtNPR_TOTPREM" tabIndex="2" runat="server" BaseType="Number" Precision="0"
								MaxLength="15" ReadOnly="true" Width="184px" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM"></asp:comparevalidator></TD>
					</TR>
				</TABLE>
				<br>
				<!--<fieldset id="FldStTV" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; LEFT: 0px; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 360px; BORDER-BOTTOM: #e0f3be 1px solid; POSITION: absolute; TOP: 170px"><legend>Entry
						Target Criteria</legend>-->
				<TABLE id="entryTable2" cellSpacing="0" cellPadding="2" border="0" style="WIDTH: 99%">
					<tr class="form_heading">
						<td height="20" colspan="6">&nbsp; Target Criteria
						</td>
					</tr>
					<tr>
						<td height="10" colspan="6"></td>
					</tr>
					<TR id="rowNP1_RETIREMENTAGE" class="TRow_Normal">
						<TD id="TDtxtNP1_RETIREMENTAGE" align="right">Retirement Age &nbsp;</TD>
						<TD><shma:textbox id="txtNP1_RETIREMENTAGE" tabIndex="9" runat="server" BaseType="Number" Precision="0"
								MaxLength="3" Width="100px"></shma:textbox><asp:comparevalidator id="cfvNP1_RETIREMENTAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP1_RETIREMENTAGE"></asp:comparevalidator></TD>
						<!--<td width="106"></td>
							<td width="186"></td></TR>
						<TR id="rowNP1_TARGETRETURNYEAR">-->
						<TD id="TDtxtNP1_TARGETRETURNYEAR" align="right">Return % Year</TD>
						<TD><shma:textbox id="txtNP1_TARGETRETURNYEAR" tabIndex="10" runat="server" BaseType="Number" Precision="0"
								MaxLength="3" Width="100px"></shma:textbox><asp:comparevalidator id="cfvNP1_TARGETRETURNYEAR" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP1_TARGETRETURNYEAR"></asp:comparevalidator></TD>
						<!--<td width="106"></td>
							<td width="186"></td></TR>
						<TR class="TRow_Normal" id="rowNP1_TARGETATTAINAGE">-->
						<TD id="TDtxtNP1_TARGETATTAINAGE" align="right">AV Attain Age</TD>
						<TD><shma:textbox id="txtNP1_TARGETATTAINAGE" tabIndex="11" runat="server" BaseType="Number" Precision="0"
								MaxLength="3" Width="100px"></shma:textbox><asp:comparevalidator id="cfvNP1_TARGETATTAINAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP1_TARGETATTAINAGE"></asp:comparevalidator></TD>
						<!--<td width="106"></td>
							<td width="186">
							</td>--></TR>
					<!--<TR>
							<td style="WIDTH: 157px"><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD style="WIDTH: 216px"></TD>
						</TR>--></TABLE>
				<!--</fieldset>-->
				<!--<IMG id="Compute" style="Z-INDEX: 103; LEFT: 614px; POSITION: absolute; TOP: 249px" alt="" src="..\Presentation\Images\buttons\compute.gif" onclick="parent.openWait('computing.');_lastEvent='Edit';executeProcess('ace.Compute');">-->
				<a href="#"><IMG id="Compute" style="Z-INDEX: 103; POSITION: absolute; TOP: 280px; LEFT: 614px" src="Theme_Illus/Images/compute.gif"
						onmouseover="this.src='Theme_Illus/Images/compute_2.gif'" onmouseout="this.src='Theme_Illus/Images/compute.gif'"
						border="0" name="btncompute" alt="" onclick="_lastEvent='Edit';executeProcess('ace.Compute');"></a>
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<shma:textbox id="txtNP1_PROPOSAL" runat="server" width="0px" BaseType="Character"></shma:textbox><asp:comparevalidator id="cfvNP1_PROPOSAL" runat="server" Display="Dynamic" EnableClientScript="False"
				ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP1_PROPOSAL"></asp:comparevalidator><shma:textbox id="txtNP2_SETNO" runat="server" width="0px" BaseType="Number" Precision="0"></shma:textbox><asp:comparevalidator id="cfvNP2_SETNO" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
				Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP2_SETNO"></asp:comparevalidator><shma:textbox id="txtPPR_PRODCD" runat="server" width="0px" BaseType="Character"></shma:textbox><asp:comparevalidator id="cfvPPR_PRODCD" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
				Type="String" Operator="DataTypeCheck" ControlToValidate="txtPPR_PRODCD"></asp:comparevalidator>
			<script language="javascript">
			


			parent.closeWait();	
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	document.myForm.txtNPR_EXCESSPREMIUM.focus();    fcStandardFooterFunctionsCall(); 
		
		//parent.document.getElementById("divProcessing").style.visibility = "Visible";

		setCurrencySingle("txtNPR_EXCESSPREMIUM");
		setFormatSingle("txtNPR_TOTPREM",2);
		setFormatSingle("txtNP1_PERIODICPREM",2);
		setFormatSingle("txtNPR_SUMASSURED",2);
		
		attachViewFocus('INPUT');
		attachViewFocus('SELECT');

		//alert(".location");
		//parent.frames[2].location = parent.frames[2].location;

		</script>
	</body>
</HTML>
