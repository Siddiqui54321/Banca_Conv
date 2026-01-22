<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<asp:literal id="CSSLiteral" EnableViewState="True" runat="server"></asp:literal>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralFunctions.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/IlasAjax.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/Validation/CallValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<script language="javascript">
		
		
		/************ NOTE: ********************************************************************/
		/************ This variable declared to not refresh the Plan screen     ****************/
		/************ in case of Fresh(New) record, if Validation error occured.****************/
		var blnValidationError=false;
		/***************************************************************************************/
		
		
		_lastEventProcess = '<asp:Literal id="_lastEventProcess" runat="server" EnableViewState="True"></asp:Literal>';
		
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False">
		
		if(_lastEventProcess=='Process')
		{
			//alert(_lastEventProcess);
			_lastEventProcess='';
			this.location=this.location;
		}
		</asp:Literal>
		
		<asp:Literal id="HeaderScript" runat="server" ></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->

		var BTIncrFactor = "0";
		var PaidUpto	 = "0";
		var PTFactor = "0";

		function RefreshFields()
		{				
							 //myForm.txtNP2_COMMENDATE.value ="";
							 
							//myForm.txtNP2_AGEPREM.value ="0";
							 //myForm.txtNP2_AGEPREM2ND.value ="0";
			
			if(myForm.ddlPPR_PRODCD.disabled==true)
				 myForm.ddlPPR_PRODCD.disabled=false;
				 myForm.ddlPPR_PRODCD.selectedIndex =0;
							 myForm.ddlPCU_CURRCODE.selectedIndex =0;
							 myForm.txtNPR_SUMASSURED.value ="0";
							 myForm.txtNPR_SUMASSURED_2.value ="";
							 myForm.ddlPCU_CURRCODE_PRM.selectedIndex =0;
							 myForm.ddlCMO_MODE.selectedIndex =0;
							 myForm.txtNPR_PAIDUPTOAGE.value ="";
							 myForm.txtNPR_BENEFITTERM.value ="";
							 myForm.txtNPR_PREMIUMTER.value ="";
							 myForm.txtNPR_PREMIUM.value ="0";
							 myForm.ddlNPR_INCLUDELOADINNIV.selectedIndex =0;
							 myForm.txtNPR_EXCESPRMANNUAL.value ="";
							 myForm.ddlNPR_COMMLOADING.selectedIndex =0;
							 myForm.txtNP1_PERIODICPREM.value ="0";
							 myForm.ddlPCU_AVCURRCODE.selectedIndex =0;
							 myForm.txtNP1_TOTALRIDERPREM.value ="0";
							 myForm.txtNPR_TOTPREM.value ="0";
							 myForm.txtNPR_TOTPREM_2.value ="0";
							 myForm.txtNPR_EXCESPRMANNUAL.value="0";
							 myForm.ddlCCB_CODE.selectedIndex =0;
							 myForm.ddlNPR_INDEXATION.selectedIndex =0;
							 myForm.txtNPR_INDEXRATE.value ="";
							 myForm.txtNPR_EDUNITS.value ="";
			
myForm.ddlPPR_PRODCD.focus();
			setDefaultValues();
		}
			
		/******** NOTE: *********************************************/
		/******** 1. Override the JScriptFG.js method.******************/
		/******** 2. Validation Error will be open in in Popup.*********/
		/******** 3. Normal Error will be alert only.*******************/
		/************************************************************/
		
		function ErrorMessage(errMsg)
		{
			if (errMsg=='<<Validation Error>>') 
			{   
				blnValidationError = true;
				//open Popup for Validation error
				var wOpen;
				var sOptions;
				var aURL="../Presentation/ValidationError.aspx?ErrorSource=Plan Validation Error";
				var aWinName="VALIDATIONERROR";

				sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
				sOptions = sOptions + ',width=' + (screen.availWidth /1.8).toString();
				sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
				sOptions = sOptions + ',left=300,top=300';

				wOpen = window.open( '', aWinName, sOptions );
				wOpen.location = aURL;
				wOpen.focus();
				return wOpen;
			}
			else
			{	
				blnValidationError = false;
				//Normal alert for other than Validation error - Based on JScriptFG.js file
				var shortMessage = new String();
				var longMessage = new String();
				longMessage = shortMessage = errMsg;
				if(longMessage.indexOf("<ErrorMessage>",0)!=-1)
				{
					longMessage = longMessage.replace("<ErrorMessage>","Message:");
					longMessage = longMessage.replace("<ErrorMessageDetail>","\n\nDetail:");
					shortMessage = shortMessage.substring(("<ErrorMessage>").length ,shortMessage.indexOf("<ErrorMessageDetail>",0)) + "\n Dont Show Detail?";
					confirm(shortMessage)==false?alert(longMessage):"";
				}
				else
				{
					alert(errMsg);
				}
			}
		}			
			
		/********** dependent combo's queries **********/
		
		//var str_QryNPR_COMMLOADING="SELECT SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-')+1,1) CSD_TYPE ,CSD_VALUE FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3) = ~PPR_PRODCD~ ORDER BY CSD_VALUE, CSD_TYPE";
		var str_QryNPR_COMMLOADING="SELECT CSD_VALUE, SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-') _Plus 1,1) FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3) = '~PPR_PRODCD~' ORDER BY CSD_VALUE, CSD_TYPE";
		//alert("strQry"+str_QryNPR_COMMLOADING);		
		
		var str_QryNPR_PREMODE="select B.CMO_DESCRIPTION,B.CMO_MODE from lpmo_mode A INNER JOIN LCMO_MODE B ON B.CMO_MODE=A.CMO_MODE WHERE A.PPR_PRODCD='~PPR_PRODCD~'";
		
		var str_QryPCU_CURRCODE_PRM = "SELECT PCU_CURRDESC DESC_F, PCU_CURRCODE  FROM PCU_CURRENCY  WHERE PCU_CURRCODE IN (SELECT PCU_CURRCODE FROM LPCU_CURRENCY WHERE PPR_PRODCD='~PPR_PRODCD~') ORDER BY PCU_CURRDESC ";
		
		function IsPremiumGenerated()
		{
			//alert("select 'A' RESULT from LNPR_PRODUCT F WHERE F.NP1_PROPOSAL='" + v_NP1_PROPOSAL + "' AND NPR_BASICFLAG='Y' AND F.NPR_PREMIUM IS NOT NULL AND F.NPR_PREMIUM > 0");
			var Preimum = fetchDataArray("select 'A' RESULT from LNPR_PRODUCT F WHERE F.NP1_PROPOSAL='" + v_NP1_PROPOSAL + "' AND NPR_BASICFLAG='Y' AND F.NPR_PREMIUM IS NOT NULL AND F.NPR_PREMIUM > 0");
			if (fetchArray_getRowCount(Preimum) > 0 ) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		function setButtonStatus()
		{
			try	{
				if(_lastEvent == 'New')
				{
					parent.frames[2].disableButtonsStatus();
					
				}
				else
				{
					//alert(IsPremiumGenerated());
					if(IsPremiumGenerated() == false)
					{
						parent.frames[2].disableRiderButtonOnly();
					}	
				}
			}
			catch(e){}
		}
		
		function enableButtonFram()
		{
			try	
			{
				if(IsPremiumGenerated()==true)
				{
					parent.frames[2].enableButtonsStatus();
				}
			}
			catch(e){}
		}
		
		function disbleButtonFram()
		{
			try	{parent.frames[2].disableButtonsStatus();}
			catch(e){}
		}
		
		

		</script>
	</HEAD>
	<body onload="ReloadPageForPremium();setButtonStatus();">
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamSource="FixValue" ParamValue="Plan Details"></UC:ENTITYHEADING>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<TABLE id="entryTable" cellSpacing="0" cellPadding="2" border="0">
					<tr class="form_heading">
						<td colSpan="6" height="20">&nbsp; Plan Details
						</td>
					</tr>
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<TR class="TRow_Normal" id="rowPPR_PRODCD">
						<TD id="lblddlPPR_PRODCD" align="right" width="130">Plan</TD>
						<TD id="ctlddlPPR_PRODCD" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlPPR_PRODCD" onblur="disableButtons();" tabIndex="1" runat="server" AutoPostBack="False"
								CssClass="RequiredField" Width="184px" DataValueField="PPR_PRODCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPPR_PRODCD" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPPR_PRODCD"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"></asp:requiredfieldvalidator></TD>
						<TD id="lbltxtNPR_BENEFITTERM" align="right" width="130">Policy Term</TD>
						<TD id="ctltxtNPR_BENEFITTERM" align="right" width="205"><shma:textbox id="txtNPR_BENEFITTERM" tabIndex="2" runat="server" CssClass="RequiredField" Width="184px"
								BaseType="Number" Precision="0" subtype="Currency" MaxLength="2"></shma:textbox><asp:comparevalidator id="cfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_BENEFITTERM"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_BENEFITTERM" Precision="0"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Alt" id="rowPCU_CURRCODE">
						<TD id="lblddlCCB_CODE" align="right" width="130">Calculation Basis</TD>
						<TD id="ctlddlCCB_CODE" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlCCB_CODE" onblur="disableButtons();" tabIndex="3" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="CCB_CODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCCB_CODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCCB_CODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCCB_CODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCCB_CODE"></asp:requiredfieldvalidator></TD>
						<TD id="lbltxtNPR_SUMASSURED" align="right" width="130"><asp:label id="lblFaceValue" EnableViewState="false" Runat="server">Sum Assured/Premium</asp:label></TD>
						<TD id="ctltxtNPR_SUMASSURED" align="right" width="205">
							<!-- Conditional Columns NPR_SUMASSURED --><shma:textbox id="txtNPR_SUMASSURED" tabIndex="4" runat="server" CssClass="RequiredField" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" subType="Currency" onchange="disableButtons();"></shma:textbox><asp:comparevalidator id="cfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_SUMASSURED" Precision="0"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="msgNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtNPR_SUMASSURED"
								Precision="0" Enabled="False"></asp:requiredfieldvalidator>
							<!-- Conditional Columns NPR_SUMASSURED NPR_TOTPREM --><shma:textbox id="txtNPR_TOTPREM" tabIndex="4" runat="server" CssClass="RequiredField" Width="0px"
								BaseType="Number" Precision="2" MaxLength="15" subType="Currency"></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPR_TOTPREM"
								Precision="0"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="msgNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtNPR_TOTPREM"
								Precision="0" Enabled="False"></asp:requiredfieldvalidator></TD>
						<TD></TD>
					</TR>
					<TR class="TRow_Normal" id="rowPCU_CURRCODE_PRM">
						<TD id="lblddlCMO_MODE" align="right" width="130">Premium Mode</TD>
						<TD id="ctlddlCMO_MODE" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlCMO_MODE" onblur="disableButtons();" tabIndex="5" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="CMO_MODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCMO_MODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCMO_MODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCMO_MODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCMO_MODE"></asp:requiredfieldvalidator></TD>
						<TD id="lblddlPCU_CURRCODE_PRM" align="right" width="130">Currency</TD>
						<TD id="ctlddlPCU_CURRCODE_PRM" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlPCU_CURRCODE_PRM" tabIndex="6" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Normal" id="rowNPR_INDEXATION">
						<td id="lblNPR_INDEXATION" align="right" width="130">Escalation / Indexation</td>
						<TD id="ctlNPR_INDEXATION" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlNPR_INDEXATION" tabIndex="8" runat="server" CssClass="RequiredField" width="50px">
								<asp:ListItem Value="N">No</asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
							</SHMA:DROPDOWNLIST><asp:requiredfieldvalidator id="rfvNPR_INDEXATION" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_INDEXATION"></asp:requiredfieldvalidator><shma:textbox id="txtNPR_INDEXRATE" tabIndex="9" runat="server" CssClass="RequiredField" Width="125px"
								BaseType="Number" Precision="2" MaxLength="5" subType="Currency"></shma:textbox><asp:comparevalidator id="cfvNPR_INDEXRATE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_INDEXRATE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_INDEXRATE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPR_INDEXRATE"
								Precision="0"></asp:requiredfieldvalidator></TD>
						<TD id="lbltxtNPR_PREMIUMTER" align="right" width="130">Premium Term</TD>
						<TD id="ctltxtNPR_PREMIUMTER" align="right" width="205"><shma:textbox id="txtNPR_PREMIUMTER" tabIndex="7" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="0" MaxLength="3" SubType="Currency"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUMTER" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMTER"></asp:comparevalidator></TD>
					</TR>
					<!---------------------------->
					<TR class="TRow_Normal" id="rowCFR_FORFEITUCD">
						<TD id="lblddlCFR_FORFEITUCD" align="right" width="130">ANF</TD>
						<TD id="ctlddlCFR_FORFEITUCD" align="right" width="205">
							<SHMA:DROPDOWNLIST id="ddlCFR_FORFEITUCD" onblur="disableButtons();" tabIndex="7" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="csd_type" DataTextField="desc_f" BlankValue="true"></SHMA:DROPDOWNLIST>
							<asp:comparevalidator id="cfvCFR_FORFEITUCD" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCFR_FORFEITUCD"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCFR_FORFEITUCD" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlCFR_FORFEITUCD"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<!---------------------------->
					<!--
					<TR id="rowNP1_TOTALANNUALPREM" class="TRow_Normal" width=0 height=0><!--
						<TD id="TDtxtNP1_TOTALANNUALPREM" width="0" align="right">Desired Annual Payment</TD>-->
					<TR class="TRow_Alt" id="rowNPR_COMMLOADING">
						<TD id="lblddlNPR_COMMLOADING" align="right" width="130" style="visibility:hidden">Extended 
							Flag</TD>
						<TD id="ctlddlNPR_COMMLOADING" align="right" width="205" style="visibility:hidden;DISPLAY: none">
							<SHMA:DROPDOWNLIST id="ddlNPR_COMMLOADING" tabIndex="7" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST>
							<asp:comparevalidator id="cfvNPR_COMMLOADING" runat="server" Display="Dynamic" EnableClientScript="true"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPR_COMMLOADING"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="rfvNPR_COMMLOADING" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_COMMLOADING"></asp:requiredfieldvalidator></TD>
						<td id="lbltxtNPR_EDUNITS" align="right" width="130" style="visibility:hidden">Face 
							Amount Multiple</td>
						<TD id="ctltxtNPR_EDUNITS" align="right" width="205" style="visibility:hidden"><shma:textbox id="txtNPR_EDUNITS" tabIndex="10" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="0" MaxLength="6"></shma:textbox><asp:comparevalidator id="cfvNPR_EDUNITS" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_EDUNITS"></asp:comparevalidator><shma:textbox id="txtNP1_TOTALANNUALPREM" style="DISPLAY: none" tabIndex="11" runat="server" Width="0px"
								BaseType="Number" Precision="2" subtype="Currency" MaxLength="15"></shma:textbox><asp:comparevalidator id="cfvNP1_TOTALANNUALPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_TOTALANNUALPREM"></asp:comparevalidator></TD>
						<td id="lbltxtNPR_INTERESTRATE" align="right" width="130" style="visibility:hidden">Ineterest 
							Rate</td>
						<TD id="ctltxtNPR_INTERESTRATE" style="visibility:hidden;DISPLAY: none" align="right"
							width="205"><shma:textbox id="txtNPR_INTERESTRATE" tabIndex="10" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="6" subType="Currency"></shma:textbox><asp:comparevalidator id="cfvNPR_INTERESTRATE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_INTERESTRATE"></asp:comparevalidator></TD>
					</TR>
					<!--</TR>-->
					<!---------------------------------------------------------------->
					<!------------------ Illustration Detail ------------------------->
					<!---------------------------------------------------------------->
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<tr class="form_heading" id="IllustraionDetailHeading" style="VISIBILITY: hidden">
						<td colSpan="6" height="20">&nbsp; Premium Detail <IMG id="imgExtend" style="HEIGHT: 0px; VISIBILITY: hidden" onclick="showIllustrationDetail(this);"
								alt="" src="../shmalib/images/Extend.jpg" border="0" name="btnsave">
						</td>
					</tr>
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<TR class="TRow_Normal" id="rowNPR_PREMIUMTER_2">
						<TD id="lbltxtNP1_RETIREMENTAGE" align="right" width="130">Maturity Age</TD>
						<TD id="ctltxtNP1_RETIREMENTAGE" align="right" width="205"><shma:textbox id="txtNP1_RETIREMENTAGE" tabIndex="11" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="0" MaxLength="3" ></shma:textbox><asp:comparevalidator id="cfvNP1_RETIREMENTAGE" runat="server" ControlToValidate="txtNP1_RETIREMENTAGE"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator></TD>
						<TD id="lbltxtNPR_SUMASSURED_2" align="right" width="130">Sum Assured</TD>
						<td id="ctltxtNPR_SUMASSURED_2" align="right" width="205"><shma:textbox id="txtNPR_SUMASSURED_2" tabIndex="12" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ></shma:textbox><asp:comparevalidator id="cfvNPR_SUMASSURED_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED_2"></asp:comparevalidator></td>
						<!-- <TD align="right" id="TDtxtNPR_PREMIUMDISCOUNT">Premium Discount</TD> --><shma:textbox id="txtNPR_PREMIUMDISCOUNT" tabIndex="11" runat="server" CssClass="RequiredField"
							Width="0px" BaseType="Number" Precision="0" MaxLength="15" ReadOnly="false"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUMDISCOUNT" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
							Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMDISCOUNT"></asp:comparevalidator></TR>
					<TR class="TRow_Alt" id="rowNPR_PREMIUM_2">
						<TD id="lbltxtNPR_PREMIUM" align="right" width="130">Basic Premium</TD>
						<TD id="ctltxtNPR_PREMIUM" align="right" width="205"><shma:textbox id="txtNPR_PREMIUM" tabIndex="13" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUM" runat="server" ControlToValidate="txtNPR_PREMIUM" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator></TD>
						<TD id="lbltxtNP1_TOTALRIDERPREM" align="right" width="130">Rider Premium</TD>
						<TD id="ctltxtNP1_TOTALRIDERPREM" align="right" width="205"><shma:textbox id="txtNP1_TOTALRIDERPREM" tabIndex="14" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="17" SubType="Currency" ></shma:textbox><asp:comparevalidator id="cfvNP1_TOTALRIDERPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_TOTALRIDERPREM"></asp:comparevalidator></TD>
						<!-- <TD align="right">Total Premium</TD>
						<TD> Display Field for Total Premium</td> --></TR>
					<TR class="TRow_Normal" id="rowNPR_TOTPREM_2">
						<TD id="lbltxtNPR_TOTPREM_2" align="right" width="130">Total Premium</TD>
						<TD id="ctltxtNPR_TOTPREM_2" align="right" width="205"><shma:textbox id="txtNPR_TOTPREM_2" tabIndex="15" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM_2"></asp:comparevalidator></TD>
						<td id="lbltxtcharges" align="right" width="130">Other Charges/Fee</td>
						<td id="ctltxtcharges" align="right" width="205"><shma:textbox id="txtcharges" tabIndex="16" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="17" SubType="Currency" ></shma:textbox><asp:comparevalidator id="Comparevalidator2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtcharges"></asp:comparevalidator></td>
					</TR>
					<TR>
						<td>
							<P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
				<br>
				<!-- 
				<table id="tablePHolder" border="0" cellspacing="1" cellpadding="2" style="border-collapse:collapse; LEFT: 20px; WIDTH: 708px;" align="center">
				-->
				<table id="tablePHolder" style="BORDER-COLLAPSE: collapse" cellSpacing="1" cellPadding="2"
					border="0">
					<tr class="form_heading">
						<td>Description</td>
						<td>Life Assured&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
						</td>
						<td>2nd Life Assured&nbsp; &nbsp; &nbsp;
						</td>
					</tr>
					<tr class="TRow_Normal">
						<td class="displayTable_form_title">Name</td>
						<td class="displayTable_text_font"><asp:label id="lblName" Runat="server"></asp:label></td>
						<td class="displayTable_text_font"><asp:label id="lblName2" Runat="server"></asp:label></td>
					</tr>
					<tr class="TRow_Alt">
						<td class="displayTable_form_title">Age</td>
						<td class="displayTable_text_font"><asp:label id="lblAge" Runat="server"></asp:label></td>
						<td class="displayTable_text_font"><asp:label id="lblAge2" Runat="server"></asp:label></td>
					</tr>
					<tr class="TRow_Normal">
						<td class="displayTable_form_title">Gender</td>
						<td class="displayTable_text_font"><asp:label id="lblGender" Runat="server"></asp:label></td>
						<td class="displayTable_text_font"><asp:label id="lblGender2" Runat="server"></asp:label></td>
					</tr>
					<!---------------------------------------------------------->
					<!------------------- Hidden fields ------------------------>
					<!---------------------------------------------------------->
					<!-- <TR class="TRow_Alt" id="rowNPR_INCLUDELOADINNIV"> -->
					<TR id="rowNPR_INCLUDELOADINNIV">
						<TD id="TDddlNPR_INCLUDELOADINNIV" align="right" width="0"><!--Excess Premium Type--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlNPR_INCLUDELOADINNIV" tabIndex="50" runat="server" CssClass="RequiredField"
								Width="0" DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPR_INCLUDELOADINNIV"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_INCLUDELOADINNIV" Enabled="false"></asp:requiredfieldvalidator></TD>
						<TD id="TDtxtNPR_EXCESPRMANNUAL" align="right"><!--Excess Premium--></TD>
						<TD><shma:textbox id="txtNPR_EXCESPRMANNUAL" tabIndex="51" runat="server" CssClass="RequiredField"
								Width="0" BaseType="Number" Precision="0" MaxLength="17"></shma:textbox><asp:comparevalidator id="cfvNPR_EXCESPRMANNUAL" runat="server" ControlToValidate="txtNPR_EXCESPRMANNUAL"
								Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_EXCESPRMANNUAL" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_EXCESPRMANNUAL" Precision="0" Enabled="false"></asp:requiredfieldvalidator></TD>
					</TR>
					<!-- <TR id="rowNPR_COMMLOADING" class="TRow_Normal"> -->
					<TR id="rowPCU_CURRCODE_FACE">
						<TD id="TDddlPCU_CURRCODE" align="right" width="0"><!--Face Currency--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlPCU_CURRCODE" tabIndex="52" runat="server" CssClass="RequiredField" Width="0"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_CURRCODE" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_CURRCODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_CURRCODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlPCU_CURRCODE"></asp:requiredfieldvalidator></TD>
						<TD align="right"><!--Planned Periodic Premium--></TD>
						<TD><shma:textbox id="txtNP1_PERIODICPREM" tabIndex="53" runat="server" CssClass="DisplayOnly" Width="0"
								BaseType="Number" Precision="0" MaxLength="17" ></shma:textbox><asp:comparevalidator id="cfvNP1_PERIODICPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_PERIODICPREM"></asp:comparevalidator></TD>
					</TR>
					<TR id="rowNPR_PAIDUPTOAGE">
						<TD id="TDtxtNPR_PAIDUPTOAGE" align="right" width="0"><!--Premium Paid upto Age--></TD>
						<TD><shma:textbox id="txtNPR_PAIDUPTOAGE" tabIndex="54" runat="server" Width="0" CssClass="RequiredField"
								MaxLength="3" Precision="0" BaseType="Number"></shma:textbox><asp:comparevalidator id="cfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PAIDUPTOAGE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_PAIDUPTOAGE" Precision="0"></asp:requiredfieldvalidator></TD>
						<td></td>
						<td></td>
					</TR>
					<!-- <TR class="TRow_Alt" id="rowPCU_AVCURRCODE"> -->
					<TR id="rowPCU_AVCURRCODE">
						<TD id="TDddlPCU_AVCURRCODE" align="right" width="0"><!--AV Currency--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlPCU_AVCURRCODE" tabIndex="55" runat="server" CssClass="RequiredField" Width="0"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_AVCURRCODE" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_AVCURRCODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_AVCURRCODE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_AVCURRCODE" Enabled="false"></asp:requiredfieldvalidator></TD>
						<TD align="right"><!-- Policy Owner Age --></TD>
						<TD><shma:textbox id="txtNP2_AGEPREM" tabIndex="56" runat="server" CssClass="DisplayOnly" Width="0"
								BaseType="Number" Precision="0" MaxLength="5" ></shma:textbox><asp:comparevalidator id="cfvNP2_AGEPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP2_AGEPREM"></asp:comparevalidator></TD>
					</TR>
					<TR id="rowNP2_AGEPREM2ND">
						<TD align="right" width="0"><!-- Life Assured Age --></TD>
						<TD><shma:textbox id="txtNP2_AGEPREM2ND" tabIndex="57" runat="server" CssClass="DisplayOnly" Width="0"
								BaseType="Number" Precision="0" MaxLength="5" ></shma:textbox><asp:comparevalidator id="cfvNP2_AGEPREM2ND" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP2_AGEPREM2ND"></asp:comparevalidator></TD>
						<td></td>
						<td></td>
					</TR>
				</table>
				<!--</FIELDSET>--></div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<shma:textbox id="txtNP1_PROPOSAL" runat="server" BaseType="Character" width="0px"></shma:textbox><shma:textbox id="txtNP2_SETNO" runat="server" Precision="0" BaseType="Number" width="0px"></shma:textbox><shma:textbox id="txtNPR_BASICFLAG" runat="server" BaseType="Character" width="0px"></shma:textbox>
			<script language="javascript">
			</script>
		</form>
		<script language="javascript">
		
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 

                		
        if (_lastEvent == 'New')	
		{
			if(blnValidationError == false)
			{
				addClicked(); 	
			}
		}
		
		fcStandardFooterFunctionsCall();

		setFaceValueField(getField("CCB_CODE").value);
		setRateField();
		
		document.getElementById('ddlPPR_PRODCD').disabled=false;
		document.getElementById('ddlNPR_INDEXATION').disabled=true;
        
        document.getElementById("txtNP1_RETIREMENTAGE").readOnly=true;
        document.getElementById("txtNPR_SUMASSURED_2").readOnly=true;
        document.getElementById("txtNPR_PREMIUM").readOnly=true;
        document.getElementById("txtNP1_TOTALRIDERPREM").readOnly=true;
        document.getElementById("txtNPR_TOTPREM_2").readOnly=true;
        document.getElementById("txtcharges").readOnly=true;
        document.getElementById("txtNP2_AGEPREM").readOnly=true;
        document.getElementById("txtNP2_AGEPREM2ND").readOnly=true;

		try
		{
			if (_lastEvent=='Edit' || _lastEvent=='Update')
			{
				//check if >1 otherwise send will throw error
				if (parent.frames[2].totalRecords>1)//send to update tabular data of frame 2
					parent.frames[2].sendTb();
				else//refresh frame 2 for may be a process occur like generate rider
					parent.frames[2].location = parent.frames[2].location;
			}

		}catch(e){}

		
		//delete both the frames
		function deleteFrames()
		{
			parent.frames[2].deletedAll();
		}

		
		setCurrencySingle("txtNPR_SUMASSURED");
		//setCurrencySingle("txtNPR_SUMASSURED_2");
		setCurrencySingle("txtNPR_EXCESPRMANNUAL");
		
		setFormatSingle("txtNP1_PERIODICPREM",2);
		setFormatSingle("txtNPR_TOTPREM",2);
		setFormatSingle("txtNP1_TOTALRIDERPREM",2);
		setFormatSingle("txtNPR_EXCESPRMANNUAL",2);
		setFormatSingle("txtNPR_PREMIUM",2);
		setFormatSingle("txtNPR_PREMIUMDISCOUNT",2);
		

		setDefaultSysPara(getField('PPR_PRODCD')) ;

		
		function setBenefitTerm(obj)
		{	var incFactor ;
			//18/09/2008 document.getElementById('txtNPR_BENEFITTERM').value = parseInt(obj.value) - parseInt(document.getElementById('txtNP2_AGEPREM').value) + parseInt(BTIncrFactor) ;

			//18/09/2008
			if (parseInt(PaidUpto) > 0 )
			{
				//alert();
				document.getElementById('txtNPR_BENEFITTERM').value = parseInt(PaidUpto) - parseInt(document.getElementById('txtNP2_AGEPREM').value) ;
			}
			setPremiumTerm(document.getElementById('txtNPR_BENEFITTERM'));
		}
		
		function resetPremiumTerm(objBenefitTerm)
		{
			disbleButtonFram();
			//document.getElementById('txtNPR_PREMIUMTER').value=objBenefitTerm.value;
		}
		
		function setPremiumTerm(obj)
		{
			//Assign Benefit term to Premium term, If Premium term is read only
			//if(parent.parent.planColumns.indexOf("NPR_PREMIUMTER") < 0) 
			//	document.getElementById('txtNPR_PREMIUMTER').value=obj.value;
			var premiumTermAsBenefit = true;
			var columnArray = parent.parent.planColumns.split('~');	
			for (i=0; i<columnArray.length; i++)
			{
				var columnInfo = columnArray[i].split(',');
				var columnID   = columnInfo[0];
				//var Visibility = columnInfo[1].toUpperCase();
				var Disable    = columnInfo[2].toUpperCase();
				//var Caption    = columnInfo[3];
				//var defValue   = columnInfo[4];
				
				if(columnID == "txtNPR_PREMIUMTER")
				{
					if(Disable.toUpperCase() == "N")
					{
						premiumTermAsBenefit = false;
					}
					break;
				}
			}
			
			if(premiumTermAsBenefit == true)
			{
				document.getElementById('txtNPR_PREMIUMTER').value=obj.value;
			}
			var mode_ = document.getElementById('ddlCMO_MODE').value;
			if(mode_ == 'S')
			{
				document.getElementById('txtNPR_PREMIUMTER').value="1";
			}
			
			
			
			/** Confirmed by Naseer
			//18/09/2008
			if (parseInt(document.getElementById('txtNPR_PAIDUPTOAGE').value) > 0)
			{
				document.getElementById('txtNPR_PREMIUMTER').value = parseInt(document.getElementById('txtNPR_PAIDUPTOAGE').value) - parseInt(document.getElementById('txtNP2_AGEPREM').value) + parseInt(BTIncrFactor) ;
				//alert(document.getElementById('txtNPR_PREMIUMTER').value);
			}
			else
			{
				if (parseFloat(PTFactor) > 0)
				{   //alert("PTfactor "+eval(document.getElementById('txtNPR_BENEFITTERM').value * PTFactor));	
					document.getElementById('txtNPR_PREMIUMTER').value = parseInt(eval(document.getElementById('txtNPR_BENEFITTERM').value * PTFactor)) ;
				}
				else
				{
					document.getElementById('txtNPR_PREMIUMTER').value = document.getElementById('txtNPR_BENEFITTERM').value;
				}
			}
			return;
			
			document.getElementById('txtNPR_BENEFITTERM').value = parseInt(obj.value) - parseInt(document.getElementById('txtNP2_AGEPREM').value) ;
			//document.getElementById('txtNPR_PREMIUMTER').value=obj.value;
			***/
		}

		function setMaturityAge(objBenefitTerm)
		{
			var proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value;
			var result = fetchDataArray("select np2_ageprem from lnp2_policymastr WHERE np1_proposal='" + proposal + "'");
			if (result != null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				myForm.txtNP1_RETIREMENTAGE.value = parseInt(result[1][0]) + parseInt(objBenefitTerm.value);
			}
		}
		
		var str_QryFetcher = "select nvl(NP2_AGEPREM,0) NP2_AGEPREM, nvl(NP2_AGEPREM2ND,0) NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value +"'";
		//alert(str_QryFetcher);
		setFetchDataQry(str_QryFetcher);
		fetchData();

		setFormatSingle("txtNP2_AGEPREM",0);
		setFormatSingle("txtNP2_AGEPREM2ND",0);
		
		
		function validatePremiumTerm(obj)
		{
			if (parent.parent.newValidation == 'Y')
			{
				return;
			}
			
			return;
			if (parseInt(obj.value) > parseInt(document.getElementById('txtNPR_BENEFITTERM').value) ) 
				alert("Premium term should not exceed benefit term....");

		}

		document.getElementById('rfvNPR_PAIDUPTOAGE').enabled=false;
		
		//******* It would work only for ALICO *********//
		function setProposalInfoInPersonelPage(plan)
		{	
			if (plan == "921" || plan == "922" || plan == "923") 
			{
				parent.parent.allowAVAPScreen = false;
			}
			else
			{
				parent.parent.allowAVAPScreen = true;
			}
		}
				
		function setViewForProduct(obj)
		{
			try
			{

			setProposalInfoInPersonelPage(obj.value);
		
			if(obj.value == null || obj.value == "")
			{
				return;
			}
			
			//alert(1);
			setFieldsProductWise("PLAN",getField("PPR_PRODCD").value);
			setFaceValueField(getField("CCB_CODE").value);
			//alert(2);
			
			var sqlProductAttribs = "select PPR_CSHVAL, PPR_TYPE, PPR_ADHOCPR, NVL(PPR_PRTICIPT,'N') PPR_PRTICIPT from lppr_product where PPR_PRODCD='"+obj.value+ "'"; 
			var result = fetchDataArray(sqlProductAttribs);
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				var ppr_cshval = result[1][0];
				var ppr_type = result[1][1];
				var ppr_adhocpr = result[1][2];
				var ppr_prticipt = result[1][3];

				
				//Investa
				if (ppr_cshval=='Y' && (ppr_type=='A' || ppr_type=='U') && ppr_adhocpr=='Y')
				{
					getField('ddlNPR_INCLUDELOADINNIV').disabled=false;
					//getField('ddlNPR_COMMLOADING').disabled=false;
					//getField('ddlPCU_AVCURRCODE').disabled=false;
					getField('NPR_EXCESPRMANNUAL').disabled=false;
					getField('NPR_PAIDUPTOAGE').disabled=false;
					
					//allow navigation to next tabs
					parent.parent.navigationAllowed = "Y";

					document.getElementById('rfvNPR_INCLUDELOADINNIV').enabled=true;
					//document.getElementById('rfvNPR_COMMLOADING').enabled=true;
					//document.getElementById('rfvPCU_AVCURRCODE').enabled=true;
					document.getElementById('rfvNPR_EXCESPRMANNUAL').enabled=true;

					//18/09/2008
					document.getElementById('rfvNPR_PAIDUPTOAGE').enabled=true;


				}
				else//Protecta550/credita420
				{
					getField('ddlNPR_INCLUDELOADINNIV').value='';
					getField('ddlNPR_INCLUDELOADINNIV').disabled=true;
					getField('ddlPCU_AVCURRCODE').value='';
					getField('ddlPCU_AVCURRCODE').disabled=true;
					getField('NPR_EXCESPRMANNUAL').value='';
					getField('NPR_EXCESPRMANNUAL').disabled=true;
					
					if (ppr_prticipt=="N") // in case of credita, flag will be Y following will remain enabled
					{	
						//getField('ddlNPR_COMMLOADING').value='';
						//getField('ddlNPR_COMMLOADING').disabled=true;
						//getField('NPR_COMMLOADING').value='';
						//getField('NPR_COMMLOADING').disabled=true;
					}
					else
					{	
						//getField('ddlNPR_COMMLOADING').disabled=false;
						//getField('NPR_COMMLOADING').disabled=false;
					}


					// 18/09/2008
					getField('NPR_PAIDUPTOAGE').value='0';
					getField('NPR_PAIDUPTOAGE').disabled=true;
					

					//dis-allow navigation to next tabs
					parent.parent.navigationAllowed = "N-PR";
					
					document.getElementById('rfvNPR_INCLUDELOADINNIV').enabled=false;
					document.getElementById('rfvPCU_AVCURRCODE').enabled=false;
					document.getElementById('rfvNPR_EXCESPRMANNUAL').enabled=false;
					document.getElementById('rfvNPR_PAIDUPTOAGE').enabled=false;

					/*if (ppr_prticipt=="N") // in case of credita, flag will be Y following will remain enabled
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=false;
					}				
					else					
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=true;
					}*/				
					if(!document.getElementById('ddlNPR_COMMLOADING').enable)
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=false;
					}				
					else					
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=true;
					}
					
				}
			}
			
			
		//	setViewForBasicPlanCurrency(obj);
			}
			catch(err)
			{
				
			}
		}
		

		setViewForProduct(getField('PPR_PRODCD'));
		
		
		function disableViewPremiumUptoAge(obj)
		{
			disbleButtonFram();
			if (obj.value=='S'){
				document.getElementById('txtNPR_PAIDUPTOAGE').enabled = true;
				document.getElementById('txtNPR_PAIDUPTOAGE').readOnly = true;
				document.getElementById('txtNPR_PAIDUPTOAGE').className="DisplayOnly";
				document.getElementById('txtNPR_PREMIUMTER').value = "1";
			}
			else{
				document.getElementById('txtNPR_PAIDUPTOAGE').enabled = false;
				document.getElementById('txtNPR_PAIDUPTOAGE').readOnly = false;
				document.getElementById('txtNPR_PAIDUPTOAGE').className="RequiredField";
				document.getElementById('txtNPR_PREMIUMTER').value = document.getElementById('txtNPR_BENEFITTERM').value;
			}
		}
		
		function setViewForBasicPlanCurrency(obj)
		{
			
			try{
			
			//var sql = "select PCU_CURRCODE from  LPCU_CURRENCY where PPR_PRODCD='"+obj.value+"' and PCU_DEFAULT='Y'"; 

			var sql = " select A.PCU_CURRCODE, B.PCU_CURRCODE, C.PCU_CURRCODE "+
				" FROM LPCU_CURRENCY A, LPCU_CURRENCY B, LPCU_CURRENCY C "+
				" WHERE A.PPR_PRODCD='"+obj.value+"' AND B.PPR_PRODCD='"+obj.value+"' AND C.PPR_PRODCD='"+obj.value+"' "+
				" AND NVL(A.PCU_DEFAULT,'N')='Y' AND NVL(B.PCU_DEFAULTPREMIUM,'N')='Y' AND NVL(C.PCU_DEFAULTAV,'N')='Y' ";
			//alert("sql "+sql);
			var result = fetchDataArray(sql);
			
			
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				//if (document.getElementById('ddlPCU_CURRCODE').value==null || document.getElementById('ddlPCU_CURRCODE').value=="")
					document.getElementById('ddlPCU_CURRCODE').value=result[1][0];
				//if (document.getElementById('ddlPCU_CURRCODE_PRM').value==null || document.getElementById('ddlPCU_CURRCODE_PRM').value=="")
					document.getElementById('ddlPCU_CURRCODE_PRM').value=result[1][1];
				//if (document.getElementById('ddlPCU_AVCURRCODE').value==null || document.getElementById('ddlPCU_AVCURRCODE').value=="")
					document.getElementById('ddlPCU_AVCURRCODE').value=result[1][2];
			}

			
			//setDefaultSysPara(obj);

			}catch(e){}
			
		}
		

		function setDefaultSysPara(obj)
		{
			var sqlPaidupto = "select NVL(GET_SYSPARA.GET_VALUE('DEFLT','"+obj.value+"-PAIDUPTO'),0) PAIDUPTO, "+
			"  NVL(GET_SYSPARA.GET_VALUE('DEFLT','"+obj.value+"-BT_INCFACT'),0) INCFACTOR, "+
			" NVL(GET_SYSPARA.GET_VALUE('DEFLT','"+obj.value+"-PRMTERMFACT'),0) PRMTERMFACTOR FROM DUAL"; 
			//alert("sql"+sqlPaidupto);
			result = fetchDataArray(sqlPaidupto);

			if ( result!= null && result.length>0 && result[1][1] != null && result[1][1] !="")
			{
				BTIncrFactor = result[1][1];
				//alert("IncFactor "+BTIncrFactor);
			}

			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				PaidUpto = result[1][0];
				
				if (document.getElementById('txtNPR_PAIDUPTOAGE').value==null || document.getElementById('txtNPR_PAIDUPTOAGE').value=="" || document.getElementById('txtNPR_PAIDUPTOAGE').value=="0")
				{
					document.getElementById('txtNPR_PAIDUPTOAGE').value=result[1][0];
					if(blnValidationError == false) 
					{
						setBenefitTerm(document.getElementById('txtNPR_PAIDUPTOAGE'));
					}
					//setPremiumTerm(document.getElementById('txtNPR_BENEFITTERM'));
				}
			}

			if ( result!= null && result.length>0 && result[1][2] != null && result[1][2] !="")
			{
				//alert("init PTFactor"+PTFactor);
				PTFactor = result[1][2];
			}

		}
		
		
		//blur event
		function validate(obj,vfor)
		{
			///if(_lastEvent == 'New') return;
			
			///window.status ="";
			///try{
			
			///var proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
			///var product = document.getElementById('ddlPPR_PRODCD').value; 
			///var validatefor = vfor; 
			///var validatevalue = getDeformattedNumber(obj,2);
			///var age = document.getElementById('txtNP2_AGEPREM').value; 
			///var term = document.getElementById('txtNPR_BENEFITTERM').value; 
			///var sa = getDeformattedNumber(document.getElementById('txtNPR_SUMASSURED'),2); 
			///var totPrem = getFieldValue("NPR_TOTPREM");
			///var calcBasis = getField("CCB_CODE").value;
			///var faceValue=0;//It will contain either SUMASSURED or TOTPREM
			///var dbopt = "";

			///if (term==null || term=="")
			///	term = '0';
			///if (sa==null || sa=="")
			///	sa = '0';
		///	if (totPrem==null || totPrem=="")
			///	totPrem = '0' ;

			//Assign value for one contrail at a time
			///if(calcBasis=="T" && vfor=="TOTPREM")
			{   //Total Premium
				///faceValue = totPrem;
			}
			///else
			{   //Sum Assured
		///		faceValue = sa;
			}
			
		///	var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',"+validatevalue+","+age+","+term+","+faceValue+","+(dbopt==""?"null":dbopt)+") from dual"; 
			
			
		///	var result = fetchDataArray(sql);
			
		///	if (validatefor=='SUMASSURED')
		///		caption = 'Sum Assured Amount';
		///	else if (validatefor=='TOTPREM')
		///		caption = 'Total Premium Amount';
		///	else if (validatefor=='BTERM')
			///	caption = 'Benefit Term';
		///	else if (validatefor=='MATURITYAGE')
			///	caption = 'Premium Paid upto Age';
			
			
			///if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			///{
				
			///	window.status = result[1][0];
			///	alert(caption + ":" + result[1][0]);
			
			///}
			
			///}catch(e){}
		
		}

		//focus event
		function validateInfo(obj, vfor)
		{
			if (parent.parent.newValidation == 'Y')
			{
				return true;
			}
			
			
			window.status ="";
			
			try{
			var proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
			var product = document.getElementById('ddlPPR_PRODCD').value; 
			var validatefor = vfor; 
			var validatevalue = getDeformattedNumber(obj,2);
			var age = document.getElementById('txtNPR_PAIDUPTOAGE').value; 
			var age = document.getElementById('txtNP2_AGEPREM').value; 
			var term = document.getElementById('txtNPR_BENEFITTERM').value; 
			var sa = getDeformattedNumber(document.getElementById('txtNPR_SUMASSURED'),2); 
			var totPrem = getFieldValue("NPR_TOTPREM");
			var calcBasis = getField("CCB_CODE").value;
			var faceValue=0;//It will contain either SUMASSURED or TOTPREM
			var dbopt = "";
			
			//if (term==null || term=="")
			//	term = '0';
			//if (sa==null || sa=="")
			//	sa = '0';
			//if (totPrem==null || totPrem=="")
			//	totPrem = '0' ;
			//Assign value for one contrail at a time
			//if(calcBasis=="T" && vfor=="TOTPREM")
			//{   //Total Premium
			//	faceValue = totPrem;
			//}
			//else
			//{   //Sum Assured
			//	faceValue = sa;
			//}
			
			//var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+sa+","+(dbopt==""?"null":dbopt)+") from dual"; 
			//var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+faceValue+","+(dbopt==""?"null":dbopt)+") from dual"; 
		  //alert(validatefor);
			//var result = fetchDataArray(sql);
			//var result11;
			var result =getvalidationforproduct(vfor);
			//var str=result.lastIndexOf(",");
			
           // result11 = result.replace(str,'');
           // alert(result11);
           // var t=result.replace(/;$/,"");    
			//alert(result);
			//alert("Focus Event: " + validatefor);
			if (validatefor=='SUMASSURED')
			window.status='valid sumassured'+' '+result; 
			//	caption = 'Sum Assured Amount';
			else if (validatefor=='TOTPREM')
			window.status=result; 
			//	caption = 'Total Premium Amount';
			else if (validatefor=='BTERM')
			window.status='valid term'+' '+result; 
			//	caption = 'Benefit Term';
			else if (validatefor=='ENTRYAGE')
			window.status="SUM2"; 
			//	caption = 'Premium Paid upto Age';
			
			//if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				//obj.focus();
				//window.status = result[1][0];
				//alert(caption + ":" + result[1][0]);
			//alert(result[2][0]);
			}
			
			}catch(e){}
		
		}
		
		
		var actualPremium = getDeformattedNumber(document.getElementById('txtNPR_PREMIUM'),2);
		
		function discountPremium()
		{
			var discount = getDeformattedNumber(document.getElementById('txtNPR_PREMIUMDISCOUNT'), 2);
			var discountedpremium = actualPremium - discount;
			
			document.getElementById('txtNPR_PREMIUM').value = discountedpremium;
			applyNumberFormat(document.getElementById('txtNPR_PREMIUM'),2);
		}


		function attachViewFocus(tag)
		{
			var t = document.getElementsByTagName(tag);
			var i;
			for(i=0;i<t.length;i++)
			{
				if(document.getElementById('TD' + t[i].id)!=null)
				{
					obj = document.getElementById('TD' + t[i].id);
					//alert('TD' + t[i].id);
					var msg= obj.innerHTML;
					
					if (t[i].id.indexOf('txtNPR_SUMASSURED')==-1 && t[i].id.indexOf('txtNPR_PAIDUPTOAGE')==-1 && t[i].id.indexOf('txtNPR_BENEFITTERM')==-1 && t[i].id.indexOf('txtNP1_RETIREMENTAGE')==-1)
						t[i].attachEvent('onfocus',  new Function("function1('"+msg+"')"));
				}

			}
		}
		
		attachViewFocus('INPUT');
		attachViewFocus('SELECT');
		




		function saveButtonFocus(obj)
		{
			parent.frames[2].document.getElementById("button2").focus();
		}

		
		function filterCurrency(objProduct)
		{
			fcfilterChildCombo(objProduct,str_QryPCU_CURRCODE_PRM,"ddlPCU_CURRCODE_PRM");
		}
		
		function setOptions(obj_Ref)
		{ 
			disbleButtonFram();
			//fcfilterChildCombo(obj_Ref,str_QryPLC_LOCACODE+" order by PLC_LOCACODE ASC","ddlNPW_REQUIREDFOR");
			//alert(str_QryNPR_COMMLOADING);
			//fcfilterChildCombo(obj_Ref,str_QryNPR_COMMLOADING,"ddlNPR_COMMLOADING");
			//New Code
            fcfilterChildCombo(obj_Ref,str_QryNPR_PREMODE,"ddlCMO_MODE");

			////
			try
			{
				if(obj_Ref.value != "")
				{
					var sqls = " select pvl_validfrom,pvl_validto from lpvl_validation where ppr_prodcd='"+ document.getElementById('ddlPPR_PRODCD').value +"' and pvl_validationfor='BTERM'"; 
					var sqls1 = "select np2_ageprem from lnp2_policymastr WHERE np1_proposal='"+parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value+"'"; 
					
					var results = fetchDataArray(sqls);
					var results1 = fetchDataArray(sqls1);
					//alert(results[1][0]); from
					//alert(results[1][1]); to
				
					if ( results!= null && results.length>0 && results[1][0] != null && results[1][0] !="")
					{
						//alert("setOptions");
						myForm.txtNPR_BENEFITTERM.value=parseInt(results[1][0]); 
						myForm.txtNPR_PREMIUMTER.value=parseInt(results[1][0]);
						myForm.txtNP1_RETIREMENTAGE.value=parseInt(results[1][0])+ parseInt(results1[1][0]);
						//alert(parseInt(results[1][0]);
					}
				
					//if ( results[1][0]== results[1][1] && (!(results.length>2)))
					//{
					//	myForm.txtNPR_BENEFITTERM.readOnly=true;
					//}
					//else
					//{
					//	myForm.txtNPR_BENEFITTERM.readOnly=false;
					//}
				}
			}
			catch(e)
			{
			}

			///
			try
			{
				var sql = "SELECT GET_SYSPARA.GET_VALUE('DTBND','"+ document.getElementById('ddlPPR_PRODCD').value +"') FROM DUAL"; 
				var result = fetchDataArray(sql);
			
				if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
				{
					document.getElementById('lblddlNPR_COMMLOADING').innerHTML=result[1][0];
				}
			}
			catch(e)
			{
			}
		}



		
		
		function setBenefitTerm()
		{
			try
			{
				var sql = "SELECT case when NPR_BENEFITTERM is null or NPR_BENEFITTERM=0 then PPR_PTRMIN else NPR_BENEFITTERM end TERM FROM LNPR_PRODUCT npr inner join LPPR_PRODUCT ppr ON ppr.ppr_prodcd=npr.ppr_prodcd AND NP1_PROPOSAL='" +parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value+"' AND npr.PPR_PRODCD='" + document.getElementById("ddlPPR_PRODCD").value + "' AND npr.NP2_SETNO=1"; 
				var result = fetchDataArray(sql);

  	  			var sqls = "SELECT np2_ageprem from lnp2_policymastr WHERE np1_proposal='"+parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value+"'"; 
				var results = fetchDataArray(sqls);

				
				if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
				{
					//alert("setBenefitTerm");
					document.getElementById("txtNPR_BENEFITTERM").value=result[1][0];
					document.getElementById("txtNP1_RETIREMENTAGE").value=parseInt(result[1][0])+ parseInt(results[1][0]);
					setPremiumTerm(document.getElementById('txtNPR_BENEFITTERM'));
				}			
			}
			catch(e)
			{
			
			}
		}
		
		//setOptions(document.getElementById('ddlPPR_PRODCD'));
		//myForm.ddlCCB_CODE.remove(1);
	
		//alert(document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex );
		if (document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex==0)
			document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex = 1;
	
		//if (document.getElementById("ddlCMO_MODE").selectedIndex==0)
		//	document.getElementById("ddlCMO_MODE").selectedIndex = 1;
			
		//if (document.getElementById("ddlNPR_COMMLOADING").selectedIndex==0)
		//	document.getElementById("ddlNPR_COMMLOADING").selectedIndex = 1;
		//parent.closeWait();

		
		
	
		//New Method.
		

		/******  Recaculate Premium if it was already calculated **********/
		//Called from <body onload="ResetPremium();">
		function disableButtons(){
			parent.frames[2].document.getElementById('button2').disabled=false;
			//parent.frames[2].document.getElementById('showRider').disabled=true;
			//parent.frames[2].document.getElementById('CalcPremium').disabled=true;
		}
		
		function ReloadPageForPremium()
		{
			if(reloadPage=="Y")
			{
				//setFixedValuesInSession("CALCULATE_PREMIUM_FROM_PERSONAL=Y");
				executeProcess('RecalculatePremFromPersonalPage');
			}
			
		}
		
		/**************** Disable Show Riders and Calculate Premium button *********************/
		function SummAssured_OnChange(objSA)
		{
			disbleButtonFram();
		}
		
		function Premium_OnChange(objPremium)
		{
			disbleButtonFram();
		}
		
		function Currency_OnChange(objCurrency)
		{
			disbleButtonFram();
		}
		
		function DeathBenefit_OnChange(objDB)
		{
			//setInterestRate(objDB);
			disbleButtonFram();
		}
		
		function PremiumTerm_OnChange(objPT)
		{
			disbleButtonFram();
		}
		
		function IndexRate_OnChange(objIndexRate)
		{
			disbleButtonFram();
		}
		
		function FAFactor_OnChange(objFAFactor)
		{
			disbleButtonFram();
		}
		
		
		/******************** Set Initial Values ********************/
		/** show SumAssured or TotalPremium field for input **/
		setFaceValueField(getField("CCB_CODE").value);
		setRateField();
		
		//setBenefitTerm();
		
		/** Hide Illustration Detail for New data entry mode **/
		//alert();
		DisplayCalculatedPremium();
		fcfilterChildCombo(document.getElementById("ddlPPR_PRODCD"),str_QryNPR_PREMODE,"ddlCMO_MODE");
		filterCurrency(document.getElementById("ddlPPR_PRODCD"));
		
		/** show/hide the Illustration Detail frame on the click of Image **/
		showIllustrationDetail(document.getElementById('imgExtend'));

		
		//Enable Button Frame		
		enableButtonFram();
		
		/************************************************************************/
		/********************* Screen Parameterization **************************/
		//Set screen fields status with respect to client requirements
		//setFieldStatusAsPerClientSetup();
		//setFieldStatusAsPerClientSetup("PLAN");
		setFieldsProductWise("PLAN",getField("PPR_PRODCD").value);
		
		setFaceValueField(getField("CCB_CODE").value);
		//setRateField();
		
		/*function setInterestRate(objLoad)
		{
			if(objLoad.value=='2')
			{
				document.getElementById('txtNPR_INTERESTRATE').value = '7';
			}
			else
			{
				document.getElementById('txtNPR_INTERESTRATE').value = '0';
			}
		}*/
		
		function beforeSave()
		{
			EnableFieldsBeforeSubmitting();
			return true;
		}
		function beforeUpdate()
		{
			EnableFieldsBeforeSubmitting();
			return true;
		}
		function hideSumassured_2()
		{
			
			if(document.getElementById('rowNPR_PREMIUMTER_2').style.visibility == 'visible')
				document.getElementById('txtNPR_SUMASSURED_2').style.visibility = 'visible';
			else
				document.getElementById('txtNPR_SUMASSURED_2').style.visibility = 'hidden';
			
		}
		hideSumassured_2();
		//setInterestRate(document.getElementById('ddlNPR_COMMLOADING'));
		/************************************************************************/
		</script>
	</body>
</HTML>
