<%@ Page language="c#" Codebehind="TEST_PAGE.aspx.cs" AutoEventWireup="false" Inherits="SHAB.Presentation.TEST_PAGE" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
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
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralFunctions.js"></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralView.js'></SCRIPT>
		<script language="javascript">

		_lastEventProcess = '<asp:Literal id="_lastEventProcess" runat="server" EnableViewState="True"></asp:Literal>';
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False">
		parent.closeWait();
		if(_lastEventProcess=='Process')
		{
			//alert(_lastEventProcess);
			_lastEventProcess='';
			this.location=this.location;
		}
		</asp:Literal>
		
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
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
			
myForm.ddlPPR_PRODCD.focus();
			setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		
		//var str_QryNPR_COMMLOADING="SELECT SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-')+1,1) CSD_TYPE ,CSD_VALUE FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3) = ~PPR_PRODCD~ ORDER BY CSD_VALUE, CSD_TYPE";
		var str_QryNPR_COMMLOADING="SELECT CSD_VALUE, SUBSTR(CSD_TYPE,INSTR(CSD_TYPE,'-') _Plus 1,1) FROM LCSD_SYSTEMDTL  WHERE CSH_ID='DTBNF' AND SUBSTR(CSD_TYPE,1,3) = '~PPR_PRODCD~' ORDER BY CSD_VALUE, CSD_TYPE";
		//alert("strQry"+str_QryNPR_COMMLOADING);		
		
		


		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Plan Details" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<TABLE id="entryTable" cellSpacing="0" cellPadding="2" border="1">
					<tr class="form_heading">
						<td height="20"  colspan="6">&nbsp; Plan Details
						</td>
					</tr>
					<tr>
						<td height="10" colspan="4"></td>
					</tr>
					<TR class="TRow_Normal" id="rowPPR_PRODCD" >
						<TD id="TDddlPPR_PRODCD" width="110" align="right"  colspan="1" >Plan</TD>
						<TD width="186"  colspan="1" ><SHMA:DROPDOWNLIST id="ddlPPR_PRODCD" tabIndex="4" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="PPR_PRODCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPPR_PRODCD" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPPR_PRODCD"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"></asp:requiredfieldvalidator></TD>
						<TD id="TDtxtNPR_BENEFITTERM"  width="110" align="right">Benefit Term</TD>
						<TD width="186"><shma:textbox id="txtNPR_BENEFITTERM" tabIndex="10" runat="server" BaseType="Number" Precision="0" subtype="Currency"
								CssClass="RequiredField" Width="184px" MaxLength="2"></shma:textbox><asp:comparevalidator id="cfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_BENEFITTERM"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_BENEFITTERM" Precision="0"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Alt" id="rowPCU_CURRCODE">
						<TD id="TDddlCCB_CODE" width="110" align="right">Calculation Basis</TD>
						<TD width="186">
							<SHMA:dropdownlist id="ddlCCB_CODE" width="184px" tabIndex="8" CssClass="RequiredField" runat="server">
								<asp:ListItem Value="S">Sum Assured</asp:ListItem>
								<asp:ListItem Value="T">Total Premium</asp:ListItem>
							</SHMA:dropdownlist>
							<asp:CompareValidator id="cfvCCB_CODE" runat="server" ControlToValidate="ddlCCB_CODE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvCCB_CODE" runat="server" ErrorMessage="Required" ControlToValidate="ddlCCB_CODE"
								Display="Dynamic"></asp:requiredfieldvalidator>	</TD>
						<TD id="TDtxtNPR_SUMASSURED"  width="110" align="right"> <asp:Label ID="lblFaceValue" Runat=server>Sum Assured/Premium</asp:Label>  </TD>			
						<TD  width="186">
						<!-- Conditional Columns NPR_SUMASSURED -->
						<shma:textbox id="txtNPR_SUMASSURED" tabIndex="6" runat="server" BaseType="Number" Precision="2" subType="Currency"
								CssClass="RequiredField" Width="184px" MaxLength="15"></shma:textbox><asp:comparevalidator id="cfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_SUMASSURED" Precision="0"></asp:requiredfieldvalidator>
							<asp:requiredfieldvalidator id="msgNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtNPR_SUMASSURED"
								Precision="0" Enabled="False"></asp:requiredfieldvalidator>

						<!-- Conditional Columns NPR_SUMASSURED NPR_TOTPREM -->
							<shma:textbox id="txtNPR_TOTPREM" tabIndex="19" runat="server" BaseType="Number" Precision="2" subType="Currency"
								Width="184px" MaxLength="15" ></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM"></asp:comparevalidator>	
						</TD>
						
						
						
						<TD></TD>								
					</TR>
					<TR id="rowPCU_CURRCODE_PRM" class="TRow_Normal">
						<TD id="TDddlCMO_MODE" width="110" align="right">Premium Mode</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlCMO_MODE" tabIndex="8" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="CMO_MODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCMO_MODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCMO_MODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCMO_MODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCMO_MODE"></asp:requiredfieldvalidator></TD>
						<TD id="TDddlPCU_CURRCODE_PRM"  width="110" align="right">Currency</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlPCU_CURRCODE_PRM" tabIndex="7" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR id="rowNPR_COMMLOADING" class="TRow_Alt">
						<TD id="TDddlNPR_COMMLOADING" width="110" align="right">Death Benefit Option</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlNPR_COMMLOADING" tabIndex="15" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNPR_COMMLOADING" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPR_COMMLOADING"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_COMMLOADING" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_COMMLOADING" Enabled="false"></asp:requiredfieldvalidator></TD>																
						<TD width="110" align="right">Retirement Age</TD>
						<TD width="186"><shma:textbox id="txtNP1_RETIREMENTAGE" tabIndex="11" runat="server" BaseType="Number" Precision="0"
								CssClass="DisplayOnly" Width="184px" MaxLength="3"></shma:textbox><asp:comparevalidator id="cfvNP1_RETIREMENTAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP1_RETIREMENTAGE"></asp:comparevalidator>
						</TD>
					</TR>
					<TR id="rowNP1_TOTALANNUALPREM" class="TRow_Normal">
						<TD id="TDtxtNP1_TOTALANNUALPREM" width="110" align="right">Desired Annual Payment</TD>
						<TD width="186"><shma:textbox id="txtNP1_TOTALANNUALPREM" tabIndex="6" runat="server" BaseType="Number" Precision="2" subtype="Currency"
								Width="184px" MaxLength="15"></shma:textbox><asp:comparevalidator id="cfvNP1_TOTALANNUALPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_TOTALANNUALPREM"></asp:comparevalidator>
						<td></td>
						<td></td>
					</TR>
					
					
										
					<!---------------------------------------------------------------->
					<!------------------ Illustration Detail ------------------------->
					<!---------------------------------------------------------------->
					<tr>
						<td height="10" colspan="4"></td>
					</tr>
					<tr id="IllustraionDetailHeading" class="form_heading">
						<td height="20" colspan="6"><a href="#" > <IMG id="imgExtend" border="0" name="btnsave" alt="" src='../shmalib/images/Extend.jpg' onclick="showIllustrationDetail(this);"></a> &nbsp; Ilustration Detail
						</td>
					</tr>
					<tr>
						<td height="10" colspan="4"></td>
					</tr>

					
					<TR id="rowNPR_PREMIUMTER_2" class="TRow_Normal">
						<TD width="110" align="right">Premium Term</TD>
						<TD width="186"><shma:textbox id="txtNPR_PREMIUMTER" tabIndex="11" runat="server" BaseType="Number" Precision="0" SubType="Currency" 
							    ReadOnly="true" CssClass="DisplayOnly" Width="184px" MaxLength="3"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUMTER" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMTER"></asp:comparevalidator>
						</TD>
						<TD align="right"  width="110">Sum Assured</TD>
						<td width="186"><shma:textbox id="txtNPR_SUMASSURED_2" tabIndex="6" runat="server" BaseType="Number" Precision="2" SubType="Currency" 
								ReadOnly="true" CssClass="RequiredField" Width="184px" MaxLength="15"></shma:textbox><asp:comparevalidator id="cfvNPR_SUMASSURED_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED_2"></asp:comparevalidator></td>
				
						<!-- <TD align="right" id="TDtxtNPR_PREMIUMDISCOUNT">Premium Discount</TD> -->
						<shma:textbox id="txtNPR_PREMIUMDISCOUNT" tabIndex="12" runat="server" BaseType="Number" Precision="0"
								ReadOnly="false" Width="0px" MaxLength="15" CssClass="RequiredField"></shma:textbox><asp:comparevalidator id="Comparevalidator1" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMDISCOUNT"></asp:comparevalidator>
						
					</TR>

					<TR id="rowNPR_PREMIUM_2" class="TRow_Alt">
						<TD width="110" align="right">Basic Premium</TD>
						<TD width="186"><shma:textbox id="txtNPR_PREMIUM" tabIndex="12" runat="server" BaseType="Number" SubType="Currency" Precision="2"
								ReadOnly="true" Width="184px" MaxLength="15" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUM"></asp:comparevalidator></TD>
					
						<TD width="110" align="right">Rider Premium</TD>
						<TD width="186"><shma:textbox id="txtNP1_TOTALRIDERPREM" tabIndex="18" runat="server" BaseType="Number" SubType="Currency" Precision="2"
								ReadOnly="true" Width="184px" MaxLength="17" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNP1_TOTALRIDERPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_TOTALRIDERPREM"></asp:comparevalidator></TD>
						<!-- <TD align="right">Total Premium</TD>
						<TD> Display Field for Total Premium</td> -->
					</TR>
					
					<TR id="rowNPR_TOTPREM_2" class="TRow_Normal">
						<TD width="110" align="right">Total Premium</TD>
						<TD width="186"><shma:textbox id="txtNPR_TOTPREM_2" tabIndex="19" runat="server" BaseType="Number" Precision="2" SubType="Currency" 
							ReadOnly="true" Width="184px" MaxLength="15" ></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
							Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM_2"></asp:comparevalidator></TD>
						<td></td>
						<td></td>
					</TR>

					<TR>
						<td>
							<P><asp:label id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red" Visible="False"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
				</TABLE>
				
				<table id="tablePHolder" width="600" border="0" cellspacing="1" cellpadding="2" style="border-collapse:collapse;"
													align="center">
					<tr class="displayTableHeading">
					
						<td >Description</td>
						<td>Life Assured</td>
						<td>2nd Life Assured</td>
					</tr>
					<tr class="displayTable_row_bg">
						<td class="displayTable_form_title">Name</td>
						<td class="displayTable_text_font"><asp:Label ID="lblName" Runat=server></asp:Label></td>
						<td class="displayTable_text_font"><asp:Label ID="lblName2" Runat=server></asp:Label></td>
					</tr>
					<tr>
						<td class="displayTable_form_title">Age</td>
						<td class="displayTable_text_font"><asp:Label ID="lblAge" Runat=server></asp:Label></td>
						<td class="displayTable_text_font"><asp:Label ID="lblAge2" Runat=server></asp:Label></td>
					</tr>
					<tr class="displayTable_row_bg">
						<td class="displayTable_form_title">Gender</td>
						<td class="displayTable_text_font"><asp:Label ID="lblGender" Runat=server></asp:Label></td>
						<td class="displayTable_text_font"><asp:Label ID="lblGender2" Runat=server></asp:Label></td>
					</tr>
					<!---------------------------------------------------------->
					<!------------------- Hidden fields ------------------------>
					<!---------------------------------------------------------->
					<!-- <TR class="TRow_Alt" id="rowNPR_INCLUDELOADINNIV"> -->
					<TR id="rowNPR_INCLUDELOADINNIV">
						<TD id="TDddlNPR_INCLUDELOADINNIV" width="0" align="right"><!--Excess Premium Type--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlNPR_INCLUDELOADINNIV" tabIndex="13" runat="server" CssClass="RequiredField"
								Width="0" DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPR_INCLUDELOADINNIV"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_INCLUDELOADINNIV" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_INCLUDELOADINNIV" Enabled="false"></asp:requiredfieldvalidator></TD>

						<TD id="TDtxtNPR_EXCESPRMANNUAL" align="right"><!--Excess Premium--></TD>
						<TD><shma:textbox id="txtNPR_EXCESPRMANNUAL" tabIndex="14" runat="server" BaseType="Number" Precision="0"
								CssClass="RequiredField" Width="0" MaxLength="17"></shma:textbox><asp:comparevalidator id="cfvNPR_EXCESPRMANNUAL" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_EXCESPRMANNUAL"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_EXCESPRMANNUAL" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_EXCESPRMANNUAL" Precision="0" Enabled="false"></asp:requiredfieldvalidator></TD>
					</TR>

					<!-- <TR id="rowNPR_COMMLOADING" class="TRow_Normal"> -->
					<TR id="rowPCU_CURRCODE_FACE">
						<TD id="TDddlPCU_CURRCODE" width="0" align="right"><!--Face Currency--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlPCU_CURRCODE" tabIndex="5" runat="server" CssClass="RequiredField" Width="0"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_CURRCODE" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_CURRCODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_CURRCODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlPCU_CURRCODE"></asp:requiredfieldvalidator>
						</TD>
						<TD align="right"><!--Planned Periodic Premium--></TD>
						<TD><shma:textbox id="txtNP1_PERIODICPREM" tabIndex="16" runat="server" BaseType="Number" Precision="0"
								ReadOnly="true" Width="0" MaxLength="17" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNP1_PERIODICPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_PERIODICPREM"></asp:comparevalidator></TD>

					</TR>
					
					<TR id="rowNPR_PAIDUPTOAGE">
						<TD id="TDtxtNPR_PAIDUPTOAGE" width="0" align="right"><!--Premium Paid upto Age--></TD>
						<TD><shma:textbox id="txtNPR_PAIDUPTOAGE" tabIndex="9" runat="server" BaseType="Number" Precision="0"
								CssClass="RequiredField" Width="0" MaxLength="3"></shma:textbox><asp:comparevalidator id="cfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PAIDUPTOAGE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPR_PAIDUPTOAGE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_PAIDUPTOAGE" Precision="0"></asp:requiredfieldvalidator></TD>
						<td></td>
						<td></td>
					</TR>
										
					<!-- <TR class="TRow_Alt" id="rowPCU_AVCURRCODE"> -->
					<TR id="rowPCU_AVCURRCODE">
						<TD id="TDddlPCU_AVCURRCODE" width="0" align="right"><!--AV Currency--></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlPCU_AVCURRCODE" tabIndex="17" runat="server" CssClass="RequiredField" Width="0"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvPCU_AVCURRCODE" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_AVCURRCODE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvPCU_AVCURRCODE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_AVCURRCODE" Enabled="false"></asp:requiredfieldvalidator></TD>
						<TD align="right"><!-- Policy Owner Age --></TD>
						<TD><shma:textbox id="txtNP2_AGEPREM" tabIndex="2" runat="server" BaseType="Number" Precision="0"
								ReadOnly="true" Width="0" MaxLength="5" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNP2_AGEPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP2_AGEPREM"></asp:comparevalidator></TD>
					</TR>
					
					<TR id="rowNP2_AGEPREM2ND">
						<TD width="0" align="right"><!-- Life Assured Age --></TD>
						<TD><shma:textbox id="txtNP2_AGEPREM2ND" tabIndex="3" runat="server" BaseType="Number" Precision="0"
								ReadOnly="true" Width="0" MaxLength="5" CssClass="DisplayOnly"></shma:textbox><asp:comparevalidator id="cfvNP2_AGEPREM2ND" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP2_AGEPREM2ND"></asp:comparevalidator></TD>
						<td></td>
						<td></td>
					</TR>
				</table>
				
				<!--</FIELDSET>-->
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<shma:textbox id="txtNP1_PROPOSAL" runat="server" BaseType="Character" width="0px"></shma:textbox>
			<shma:textbox id="txtNP2_SETNO" runat="server" BaseType="Number" width="0px" Precision="0"></shma:textbox>
			<shma:textbox id="txtNPR_BASICFLAG" runat="server" BaseType="Character" width="0px"></shma:textbox>

			<script language="javascript">
			</script>
		</form>
		<script language="javascript">
		
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 
		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();

		document.getElementById('ddlPPR_PRODCD').disabled=false;

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
				document.getElementById('txtNPR_BENEFITTERM').value = parseInt(PaidUpto) - parseInt(document.getElementById('txtNP2_AGEPREM').value) ;

			setPremiumTerm(document.getElementById('txtNPR_BENEFITTERM'));
		}
		
		function setPremiumTerm(obj)
		{
			document.getElementById('txtNPR_PREMIUMTER').value=obj.value;
			
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

		var str_QryFetcher = "select nvl(NP2_AGEPREM,0) NP2_AGEPREM, nvl(NP2_AGEPREM2ND,0) NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value +"'";
		//alert(str_QryFetcher);
		setFetchDataQry(str_QryFetcher);
		fetchData();

		setFormatSingle("txtNP2_AGEPREM",0);
		setFormatSingle("txtNP2_AGEPREM2ND",0);
		
		function validatePremiumTerm(obj)
		{
			return;
			if (parseInt(obj.value) > parseInt(document.getElementById('txtNPR_BENEFITTERM').value) ) 
				alert("Premium term should not exceed benefit term....");

		}

		document.getElementById('rfvNPR_PAIDUPTOAGE').enabled=false;
		function setViewForProduct(obj)
		{
			
			
			var sqlProductAttribs = "select PPR_CSHVAL, PPR_TYPE, PPR_ADHOCPR, NVL(PPR_PRTICIPT,'N') PPR_PRTICIPT from lppr_product where PPR_PRODCD="+obj.value; 
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
					getField('ddlNPR_COMMLOADING').disabled=false;
					//getField('ddlPCU_AVCURRCODE').disabled=false;
					getField('NPR_EXCESPRMANNUAL').disabled=false;
					getField('NPR_PAIDUPTOAGE').disabled=false;
					
					//allow navigation to next tabs
					parent.parent.navigationAllowed = "Y";

					document.getElementById('rfvNPR_INCLUDELOADINNIV').enabled=true;
					document.getElementById('rfvNPR_COMMLOADING').enabled=true;
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
					{	getField('ddlNPR_COMMLOADING').value='';
						getField('ddlNPR_COMMLOADING').disabled=true;
					}
					else
					{	
						getField('ddlNPR_COMMLOADING').disabled=false;
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

					if (ppr_prticipt=="N") // in case of credita, flag will be Y following will remain enabled
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=false;
					}				
					else					
					{
						document.getElementById('rfvNPR_COMMLOADING').enabled=true;
					}				
					
				}
			}
			
			
			setViewForBasicPlanCurrency(obj);
		}
		
		setViewForProduct(getField('PPR_PRODCD'));
		
		
		function disableViewPremiumUptoAge(obj)
		{
			
			if (obj.value=='S'){
				document.getElementById('txtNPR_PAIDUPTOAGE').enabled = true;
				document.getElementById('txtNPR_PAIDUPTOAGE').readOnly = true;
				document.getElementById('txtNPR_PAIDUPTOAGE').className="DisplayOnly";
			}
			else{
				document.getElementById('txtNPR_PAIDUPTOAGE').enabled = false;
				document.getElementById('txtNPR_PAIDUPTOAGE').readOnly = false;
				document.getElementById('txtNPR_PAIDUPTOAGE').className="RequiredField";
			}
		}
		
		function setViewForBasicPlanCurrency(obj)
		{
			
			try{
			
			//var sql = "select PCU_CURRCODE from  LPCU_CURRENCY where PPR_PRODCD="+obj.value+" and PCU_DEFAULT='Y'"; 

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

			
			setDefaultSysPara(obj);

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
					setBenefitTerm(document.getElementById('txtNPR_PAIDUPTOAGE'));
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
			if(_lastEvent == 'New') return;
			
			window.status ="";
			try{
			
			var proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
			var product = document.getElementById('ddlPPR_PRODCD').value; 
			var validatefor = vfor; 
			var validatevalue = getDeformattedNumber(obj,2);
			//var age = document.getElementById('txtNPR_PAIDUPTOAGE').value; 
			var age = document.getElementById('txtNP2_AGEPREM').value; 
			var term = document.getElementById('txtNPR_BENEFITTERM').value; 
			var sa = getDeformattedNumber(document.getElementById('txtNPR_SUMASSURED'),2); 
			var totPrem = getFieldValue("NPR_TOTPREM");
			var calcBasis = getField("CCB_CODE").value;
			var faceValue=0;//It will contain either SUMASSURED or TOTPREM
			var dbopt = "";

			if (term==null || term=="")
				term = '0';
			if (sa==null || sa=="")
				sa = '0';
			if (totPrem==null || totPrem=="")
				totPrem = '0' ;

			//Assign value for one contrail at a time
			if(calcBasis=="T" && vfor=="TOTPREM")
			{   //Total Premium
				faceValue = totPrem;
			}
			else
			{   //Sum Assured
				faceValue = sa;
			}
			
			//var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',"+validatevalue+","+age+","+term+","+sa+","+(dbopt==""?"null":dbopt)+") from dual"; 
			var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',"+validatevalue+","+age+","+term+","+faceValue+","+(dbopt==""?"null":dbopt)+") from dual"; 
			//alert(sql);
			
			var result = fetchDataArray(sql);
			//alert(validatefor);
			if (validatefor=='SUMASSURED')
				caption = 'Sum Assured Amount';
			else if (validatefor=='TOTPREM')
				caption = 'Total Premium Amount';
			else if (validatefor=='TERM')
				caption = 'Benefit Term';
			else if (validatefor=='MATURITYAGE')
				caption = 'Premium Paid upto Age';
			
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				//obj.focus();
				window.status = result[1][0];
				alert(caption + ":" + result[1][0]);
			}
			
			}catch(e){}
		
		}

		//focus event
		function validateInfo(obj, vfor)
		{
			window.status ="";
			try{
			
			var proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
			var product = document.getElementById('ddlPPR_PRODCD').value; 
			var validatefor = vfor; 
			var validatevalue = getDeformattedNumber(obj,2);
			//var age = document.getElementById('txtNPR_PAIDUPTOAGE').value; 
			var age = document.getElementById('txtNP2_AGEPREM').value; 
			var term = document.getElementById('txtNPR_BENEFITTERM').value; 
			var sa = getDeformattedNumber(document.getElementById('txtNPR_SUMASSURED'),2); 
			var totPrem = getFieldValue("NPR_TOTPREM");
			var calcBasis = getField("CCB_CODE").value;
			var faceValue=0;//It will contain either SUMASSURED or TOTPREM
			var dbopt = "";
			
			if (term==null || term=="")
				term = '0';
			if (sa==null || sa=="")
				sa = '0';
			if (totPrem==null || totPrem=="")
				totPrem = '0' ;

			//Assign value for one contrail at a time
			if(calcBasis=="T" && vfor=="TOTPREM")
			{   //Total Premium
				faceValue = totPrem;
			}
			else
			{   //Sum Assured
				faceValue = sa;
			}
			
			//var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+sa+","+(dbopt==""?"null":dbopt)+") from dual"; 
			var sql = "select CHECK_VALIDATION('Y','"+proposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+faceValue+","+(dbopt==""?"null":dbopt)+") from dual"; 
			//alert(sql);
			var result = fetchDataArray(sql);
			
			//alert("Focus Event: " + validatefor);
			if (validatefor=='SUMASSURED')
				caption = 'Sum Assured Amount';
			else if (validatefor=='TOTPREM')
				caption = 'Total Premium Amount';
			else if (validatefor=='TERM')
				caption = 'Benefit Term';
			else if (validatefor=='MATURITYAGE')
				caption = 'Premium Paid upto Age';
			
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				//obj.focus();
				window.status = result[1][0];
				//alert(caption + ":" + result[1][0]);
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
					
					if (t[i].id.indexOf('txtNPR_SUMASSURED')==-1 && t[i].id.indexOf('txtNPR_PAIDUPTOAGE')==-1 && t[i].id.indexOf('txtNPR_BENEFITTERM')==-1)
						t[i].attachEvent('onfocus',  new Function("function1('"+msg+"')"));
				}

			}
		}


		attachViewFocus('INPUT');
		attachViewFocus('SELECT');







		function setOptions(obj_Ref)
		{ 
			//fcfilterChildCombo(obj_Ref,str_QryPLC_LOCACODE+" order by PLC_LOCACODE ASC","ddlNPW_REQUIREDFOR");

			//alert(str_QryNPR_COMMLOADING);
			fcfilterChildCombo(obj_Ref,str_QryNPR_COMMLOADING,"ddlNPR_COMMLOADING");

			try{
			var sql = "SELECT GET_SYSPARA.GET_VALUE('DTBND','"+ document.getElementById('ddlPPR_PRODCD').value +"') FROM DUAL"; 
			var result = fetchDataArray(sql);
			
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
			{
				document.getElementById('TDddlNPR_COMMLOADING').innerHTML=result[1][0];
			}
			
			}catch(e){}


		}

		
		setOptions(document.getElementById('ddlPPR_PRODCD'));

		//alert(document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex );
		if (document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex==0)
			document.getElementById("ddlNPR_INCLUDELOADINNIV").selectedIndex = 1;
	
		if (document.getElementById("ddlCMO_MODE").selectedIndex==0)
			document.getElementById("ddlCMO_MODE").selectedIndex = 1;
			
		if (document.getElementById("ddlNPR_COMMLOADING").selectedIndex==0)
			document.getElementById("ddlNPR_COMMLOADING").selectedIndex = 1;
		//parent.closeWait();

		
		//Hide/unhide the SumAssured/TotalPremium field based on Calculation Basis
		function setFaceValueField(calcBasis)
		{
			if(calcBasis=="S")//Sum Assured
			{
				//document.getElementById("lblFaceValue").outerText = "Sum Assured";
				document.getElementById("lblFaceValue").innerText = "Sum Assured";
				//getField("lblFaceValue").text  = "Sum Assured";
				getField("NPR_SUMASSURED").style.visibility = 'visible';
				getField("NPR_SUMASSURED").style.width = "184px";
				getField("NPR_TOTPREM").style.visibility = 'hidden'
				getField("NPR_TOTPREM").style.width = 0;
				getField("NPR_TOTPREM").value=0;
			}
			else //Total Premium
			{
				//document.getElementById('lblFaceValue').outerText = "Total Premium";
				document.getElementById("lblFaceValue").innerText = "Total Premium";
				//getField("lblFaceValue").text  = "Total Premium";
				getField("NPR_TOTPREM").style.visibility = 'visible'
				getField("NPR_TOTPREM").style.width = "184px";
				getField("NPR_SUMASSURED").style.visibility = 'hidden';
				getField("NPR_SUMASSURED").style.width = 0;
				getField("NPR_SUMASSURED").value=0;
			}
		}
		
		
		//Hide/unhide the Illustration detail
		function showIllustrationDetail(thisObj)
		{	
			if(_lastEvent != 'New')
			{	
				if(thisObj.src.indexOf("Extend.jpg") == -1)
				{
					thisObj.src="../shmalib/images/Extend.jpg";
					parent.parent.document.getElementById('mainContentFrame').style.height = '400.0px';
					parent.document.getElementById('TEST_PAGE').style.height='230.0px';				
				}
				else
				{
					thisObj.src="../shmalib/images/Stretch.jpg";
					parent.parent.document.getElementById('mainContentFrame').style.height = '550.0px';
					parent.document.getElementById('TEST_PAGE').style.height='380.0px';
				}
			}
		}
		
		//Calculat Total Premium for display only
		function calculateTotalPremium_Display()
		{
			var basicPremium = parseFloat(getFieldValue("NPR_PREMIUM"));
			var riderPremium = parseFloat(getFieldValue("NP1_TOTALRIDERPREM"))
			var totPremium = basicPremium + riderPremium;
			setFieldValue("NPR_TOTPREM_2",totPremium);
			
			if(_lastEvent == 'New')
			{
				document.getElementById('IllustraionDetailHeading').style.visibility = 'hidden';
				document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'hidden';
				document.getElementById('tablePHolder').style.visibility = 'hidden';
			}
			else
			{
				document.getElementById('IllustraionDetailHeading').style.visibility = 'visible';
				document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'visible';
				document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'visible';
				document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'visible';
				document.getElementById('tablePHolder').style.visibility = 'visible';			
			}
		}		

		setFaceValueField(getField("CCB_CODE").value);
		showIllustrationDetail(document.getElementById('imgExtend'));
		calculateTotalPremium_Display();
		

		</script>
	</body>
</HTML>
