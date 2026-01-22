<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="PolicyAcceptance.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.PolicyAcceptance" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!-- <LINK rel="stylesheet" type="text/css" href="Styles/Style.css"> -->
		<%
			Response.Write(ace.Ace_General.loadInnerStyle());
		%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<!--<SCRIPT language="JavaScript" src='../shmalib/jscript/MI_ET_NM_PolicyEntry'></SCRIPT>-->
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		var APPROVED="001";
		var DECLINED="002";
		var POSTPONED="003";
		
		parent.closeWait();
		function RefreshFields()
		{
			myForm.ddlCDC_CODE.selectedIndex =0;
			myForm.txtNP1_DECISIONNOTE.value ="";
			myForm.txtNP1_POLICYNO.value ="";
			myForm.txtNP1_DEFFERPERIOD.value ="";
			myForm.ddlCRE_REASCODE.selectedIndex =0;
			myForm.txtNP1_DESCIONDATE.value = sysDate;
			
			//setDefaultValues();
		}
	function formatNumber(myNum, numOfDec) 
    { 
      var decimal = 1 
      for(i=1; i<=numOfDec;i++) 
      decimal = decimal *10 

      var myFormattedNum = (Math.round(myNum * decimal)/decimal).toFixed(numOfDec) 
      return(myFormattedNum) 
} 

//Calculate Balance
  
       function calbalance()
       {
      // myForm.txtNP1_AMOUNTOFPREMIUM.value=0;
       if(Number(myForm.txtNP1_AMOUNTTRANSFERED.value)>Number(myForm.txtNP1_AMOUNTOFPREMIUM.value))
       {
       alert('Transfered amount is greater than amount of premium');
       myForm.txtNP1_BALANCEOS.value="";
       return true;
       }
       else
       {
       balance=myForm.txtNP1_AMOUNTOFPREMIUM.value - myForm.txtNP1_AMOUNTTRANSFERED.value;
       myForm.txtNP1_BALANCEOS.value=formatNumber(balance,2);
       return false;
       }
       
       }	

		/********** dependent combo's queries **********/
		//var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";		

		function saveUpdate()
		{
			__doPostBack("btn_save", "btn_save_Click");
		}
			
		function LostFocus_CreditCardNumber(objCCNumber)
		{
			if(objCCNumber.value.length != 16)
			{
				alert("Credit Card Number must be 16 digit long");
				objCCNumber.focus();
			}
		}

		function LostFocus_CreditCardExpiry(objCCExpiry)
		{
		
		}
		</script>
</HEAD>
	<body onkeydown="return parent.parent.validateKeys(event);" onload="hide(document.getElementById('ddlNP1_PAYMENTMET'));">
		<UC:ENTITYHEADING id="EntityHeading" ParamSource="FixValue" ParamValue="Policy Acceptance" runat="server"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server">
				<TABLE id="entryTable" border="0" cellSpacing="0" cellPadding="2" runat="server">
					<tr id="policy_acceptance" class="form_heading" runat="server">
						<td height="20" colSpan="4">&nbsp; Policy Acceptance
						</td>
					</tr>
					<tr>
						<td height="10" colSpan="4"></td>
					</tr>
					<TR id="rowCDC_CODE" class="TRow_Normal">
						<TD id=TDddlCDC_CODE class="<%=getHighLighted()%>" width=130 align=right>Decision&nbsp;&nbsp;&nbsp;</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlCDC_CODE" tabIndex="1" runat="server" Enabled="False" BlankValue="True" DataTextField="desc_f"
								DataValueField="CDC_CODE" Width="232px" CssClass="RequiredField" HighLighted="True"></SHMA:DROPDOWNLIST></TD>
						<td width="140"><asp:label Width="140" id="DecisionDescription" runat="server" ForeColor="White" Visible="False"></asp:label>
							<asp:label id="DecisionValues" runat="server" ForeColor="White" Visible="False"></asp:label>
						</td>
					</TR>
					<TR id="rowNP1_DECISIONNOTE" class="TRow_Alt">
						<TD width="110" align="right">Decision Note</TD>
						<TD><SHMA:TEXTAREA id="txtNP1_DECISIONNOTE" tabIndex="1" runat="server" Width="25.0pc" CssClass="RequiredField"
								onFocus="parent.parent.controlFocus=true" onBlur="parent.parent.controlFocus=false" MaxRows="4.0"
								MaxLength="50"></SHMA:TEXTAREA><asp:comparevalidator id="cfvNP1_DECISIONNOTE" runat="server" ControlToValidate="txtNP1_DECISIONNOTE"
								ErrorMessage="String Format is Incorrect " Display="Dynamic" Operator="DataTypeCheck" Type="String" EnableClientScript="False"></asp:comparevalidator></TD>
					</TR>
					<TR id="rowNP1_PROPOSAL" class="TRow_Normal">
						<TD id="TDtxtNP1_PROPOSAL" width="110" align="right">Proposal</TD>
						<TD width="186"><shma:textbox id="txtNP1_PROPOSAL" tabIndex="3" runat="server" Width="184px" CssClass="RequiredField"
								MaxLength="12" readonly="true" BaseType="Character"></shma:textbox></TD>
						<td></td>
					</TR>
					<tr id="rowNP2_COMMENDATE" class="TRow_Alt">
						<TD id="TDtxtNP2_COMMENDATE" width="110" align="right">Commencement Date</TD>
						<TD width="186"><SHMA:DATEPOPUP id="txtNP2_COMMENDATE" tabIndex="4" runat="server" Width="170px" CssClass="RequiredField"
								maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="msgNP2_COMMENDATE" runat="server" CssClass="CalendarFormat" ControlToValidate="txtNP2_COMMENDATE"
								ErrorMessage="{dd/mm/yyyy} " Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:comparevalidator></TD>
					</tr>
					<tr id="rowNP2_COMMENDATE1" class="TRow_Normal">
						<TD id="TDtxtNP2_COMMENDATE1" width="110" align="right">&nbsp;Correspondence 
							require</TD>
						<TD width="186"><asp:checkbox id="chk_corres" runat="server" Checked="True" EnableViewState="False"></asp:checkbox></TD>
					</tr>
					<tr id="rowPaymentHeading" class="form_heading" runat="server">
						<td colSpan="4">&nbsp; Payment Details
						</td>
					</tr>
					<TR id="rowddlNP1_PAYMENTMET" class="TRow_Normal">
						<TD id="lblddlNP1_PAYMENTMET" class="<%=getHighLighted()%>" width=110 align=right>
							<P>Payment Type&nbsp;
							</P>
						</TD>
						<TD id="ctlddlNP1_PAYMENTMET">
							<SHMA:DROPDOWNLIST id="ddlNP1_PAYMENTMET" tabIndex="5" runat="server" BlankValue="True" DataTextField="desc_f"
								onchange="hide(this);" DataValueField="NP1_PAYMENTMET" Width="184px" HighLighted="True"></SHMA:DROPDOWNLIST>
							<asp:comparevalidator id="cfvNP1_PAYMENTMET" runat="server" ControlToValidate="ddlNP1_PAYMENTMET" ErrorMessage="String Format is Incorrect "
								Display="Dynamic" Operator="DataTypeCheck" Type="String" EnableClientScript="False"></asp:comparevalidator>
						</TD>
						<TD width="186"></TD>
					</TR>
					<TR id="rowtxtNP1_ACCOUNTNO" style="DISPLAY:none" class="TRow_Normal">
						<TD id="lbltxtNP1_ACCOUNTNO" width="110" align="right">Account Number</TD>
						<TD id="ctltxtNP1_ACCOUNTNO" width="186"><shma:textbox id="txtNP1_ACCOUNTNO" tabIndex="3" runat="server" Width="184px" CssClass="RequiredField"
								MaxLength="16" BaseType="Character"></shma:textbox></TD>
						<td></td>
					</TR>
					<TR id="rowtxtNP1_CCNUMBER" style="DISPLAY:none" class="TRow_Normal">
						<TD id="lbltxtNP1_CCNUMBER" width="110" align="right">Credit Card Number</TD>
						<TD id="ctltxtNP1_CCNUMBER" width="186"><shma:textbox id="txtNP1_CCNUMBER" tabIndex="3" runat="server" Width="184px" CssClass="RequiredField"
								MaxLength="16" BaseType="Character"></shma:textbox></TD>
						<td></td>
					</TR>
					<TR id="rowtxtNP1_CCEXPIRY" style="DISPLAY:none" class="TRow_Alt">
						<TD id="lbltxtNP1_CCEXPIRY" width="110" align="right">Expiry Date</TD>
						<TD id="ctltxtNP1_CCEXPIRY" width="186"><SHMA:DATEPOPUP id="txtNP1_CCEXPIRY" tabIndex="4" runat="server" Width="170px" CssClass="RequiredField"
								maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="msgNP1_CCEXPIRY" runat="server" CssClass="CalendarFormat" ControlToValidate="txtNP1_CCEXPIRY"
								ErrorMessage="{dd/mm/yyyy} " Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:comparevalidator></TD>
						<td></td>
					</TR>
					<TR id="rowPremAmount" class="TRow_Normal" runat="server">
						<TD class="<%=getHighLighted()%>" width=110 align=right>Amount of Premium</TD>
						<TD width="186">
							<P><shma:textbox id="txtNP1_AMOUNTOFPREMIUM" tabIndex="5" runat="server" Width="184px" HighLighted="True"
									MaxLength="50" BaseType="Character" ReadOnly="True">0</shma:textbox>&nbsp;&nbsp;&nbsp;</P>
						</TD>
					</TR>
					<TR id="C" class="TRow_Alt inVisible" runat="server">
						<TD class="<%=getHighLighted()%>" width=110 align=right>Amount Transfered&nbsp;</TD>
						<TD width="186">
							<P><shma:textbox id="txtNP1_AMOUNTTRANSFERED" tabIndex="5" runat="server" Width="184px" HighLighted="True"
									MaxLength="50" BaseType="Character" onchange="calbalance()"></shma:textbox><asp:comparevalidator id="cfvNP1_AMOUNTTRANSFERED" runat="server" ControlToValidate="txtNP1_AMOUNTTRANSFERED"
									ErrorMessage="Number Format is Incorrect " Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:comparevalidator><asp:requiredfieldvalidator id="rfv_amounttransfered" runat="server" ControlToValidate="txtNP1_AMOUNTTRANSFERED"
									ErrorMessage="Required" Display="Dynamic"></asp:requiredfieldvalidator></P>
						</TD>
					</TR>
					<TR id="D" class="TRow_Normal inVisible" runat="server">
						<TD class="<%=getHighLighted()%>" width=110 align=right>Balance o/s</TD>
						<TD width="186">
							<P><shma:textbox id="txtNP1_BALANCEOS" tabIndex="5" runat="server" Width="184px" HighLighted="True"
									onFocus="parent.parent.controlFocus=true" onBlur="parent.parent.controlFocus=false" MaxLength="50"
									BaseType="Character" ReadOnly="True"></shma:textbox></P>
						</TD>
					</TR>
					<TR id="E" class="TRow_Alt inVisible" runat="server">
						<TD class="<%=getHighLighted()%>" width=110 align=right>Transfer Slip#</TD>
						<TD width="186"><shma:textbox id="txtNP1_TRANSFERSLIP" tabIndex="2" runat="server" Width="184px" CssClass="RequiredField"
								onFocus="parent.parent.controlFocus=true" onBlur="parent.parent.controlFocus=false" HighLighted="True"
								MaxLength="50" BaseType="Character"></shma:textbox></TD>
					</TR>
					<tr id="F" class="form_heading" runat="server">
						<td height="20" colSpan="4">&nbsp; <b id="boldStuff" class="form_heading"></b>
						</td>
					</tr>
					<tr id="G" runat="server">
						<td height="10" colSpan="4"></td>
					</tr>
					<TR id="rowNP1_POLICYNO" class="TRow_Normal" runat="server">
						<TD style="HEIGHT: 21px" id=TDtxtNP1_POLICYNO class="<%=getHighLighted()%>" width=110 align=right>Policy 
							Number</TD>
						<TD style="HEIGHT: 21px" width="186">
							<P><shma:textbox id="txtNP1_POLICYNO" tabIndex="5" runat="server" Width="184px" HighLighted="True"
									MaxLength="50" BaseType="Character" ReadOnly="True"></shma:textbox></P>
						</TD>
					</TR>
					<tr id="rowNP1_ISSUEDATE" class="TRow_Alt" runat="server">
						<TD id="TDtxtNP1_ISSUEDATE" width="110" align="right">Issue Date</TD>
						<TD width="186"><SHMA:DATEPOPUP id="txtNP1_ISSUEDATE" tabIndex="4" runat="server" Width="170px" CssClass="RequiredField"
								maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="CalendarFormat" ControlToValidate="txtNP1_ISSUEDATE"
								ErrorMessage="{dd/mm/yyyy} " Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:comparevalidator></TD>
					</tr>
					<TR id="rowCRE_REASCODE" class="TRow_Normal" runat="server">
						<TD id=TDddlCRE_REASCODE class="<%=getHighLighted()%>" width=110 align=right>Reason</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlCRE_REASCODE" tabIndex="7" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="CRE_REASCODE" Width="184px" HighLighted="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCRE_REASCODE" runat="server" ControlToValidate="ddlCRE_REASCODE" ErrorMessage="String Format is Incorrect "
								Display="Dynamic" Operator="DataTypeCheck" Type="String" EnableClientScript="False"></asp:comparevalidator></TD>
					</TR>
					<TR id="rowNP1_DEFFERPERIOD" class="TRow_Alt" runat="server">
						<TD id="TDtxtNP1_DEFFERPERIOD" width="110" align="right">Period (in Days)</TD>
						<TD width="210"><shma:textbox id="txtNP1_DEFFERPERIOD" tabIndex="8" runat="server" Width="184px" MaxLength="3"
								BaseType="Number" Precision="0" subtype="Currency"></shma:textbox><SHMA:DATEPOPUP id="txtNP1_DESCIONDATE" tabIndex="9" runat="server" Width="0" CssClass="RequiredField"
								maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="cfvNP1_DEFFERPERIOD" runat="server" ControlToValidate="txtNP1_DEFFERPERIOD"
								ErrorMessage="Number Format is Incorrect " Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:comparevalidator></TD>
					</TR>
					<tr class="TRow_Normal">
						<td style="HEIGHT: 20px" colSpan="2" align="center"><asp:label id="lblPolicyStatus" runat="server" ForeColor="Blue" EnableViewState="false"></asp:label></td>
					</tr>
					<TR style="VISIBILITY: hidden" id="rowNP1_DESCIONDATE" class="TRow_Alt">
						<TD id="TDtxtNP1_DESCIONDATE" width="0" align="right"></TD>
						<TD width="0"><asp:comparevalidator id="msgNP1_DESCIONDATE" runat="server" CssClass="CalendarFormat" ControlToValidate="txtNP1_DESCIONDATE"
								ErrorMessage="{dd/mm/yyyy} " Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:comparevalidator></TD>
					</TR>
					<TR>
						<td width="106">
							<P><asp:label id="lblServerError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:label></P>
						</td>
					</TR>
					<tr id="TrHeading" class="rowValidationForPost form_heading" runat="server">
						<td height="20" colSpan="4">&nbsp; Required Documents</B>
						</td>
					</tr>
					<tr id="Tr7" runat="server" class="TRow_Normal">
						<td height="10" colSpan="4"></td>
					</tr>
					<tr class="rowValidationForPost" runat="server" ID="TrCb1">
						<td colspan="3">
							<asp:CheckBox ID="cb1" Checked="False" runat="server" Text=" Customer Signed Illustration"></asp:CheckBox>
							<asp:CustomValidator id="cb1_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr class="rowValidationForPost TRow_Normal" runat="server" ID="TrCb2">
						<td colspan="3">
							<asp:CheckBox ID="cb2" Checked="False" runat="server" Text=" Customer Signed Application"></asp:CheckBox>
							<asp:CustomValidator id="cb2_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr class="rowValidationForPost" runat="server" ID="TrCb3">
						<td colspan="3">
							<asp:CheckBox ID="cb3" Checked="False" runat="server" Text=" Customer Signed Standing Order"></asp:CheckBox>
							<asp:CustomValidator id="cb3_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr class="rowValidationForPost TRow_Normal" runat="server" ID="TrCb4">
						<td colspan="3">
							<asp:CheckBox ID="cb4" Checked="False" runat="server" Text=" Customer Signed Disclaimer Form"></asp:CheckBox>
							<asp:CustomValidator id="cb4_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr class="rowValidationForPost" runat="server" ID="TrCb5">
						<td colspan="3">
							<asp:CheckBox ID="cb5" Checked="False" runat="server" Text=" Customer Signed Copy of Acceptance / Requirement (which ever is applicable) Letter"></asp:CheckBox>
							<asp:CustomValidator id="cb5_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr class="rowValidationForPost TRow_Normal" runat="server" ID="TrCb6">
						<td colspan="3">
							<asp:CheckBox ID="cb6" Checked="False" runat="server" Text=" Valid CNIC Copy"></asp:CheckBox>
							<asp:CustomValidator id="cb6_Validate" runat="server" ClientValidationFunction="isChecked" ErrorMessage="Please check the required documents."
								ToolTip="Please check the required documents."> Required </asp:CustomValidator>
						</td>
					</tr>
					<tr id="Tr11">
						<td width="100%" colSpan="4">
							<asp:Panel ID="pnlReason" Runat="server" Visible="False">
      <TABLE style="WIDTH: 100%" id=entryTable12 border=0 cellSpacing=0 
      cellPadding=2>
        <TR id=Tr1 class="rowReason form_heading" 
        onclick="slideToggle('#pnlReason2');">
          <TD height=20 colSpan=4>&nbsp; Sub-Standard Reasons</B> 
        </TD></TR></TABLE>
<asp:Panel id=pnlReason2 CssClass="reasonPanel" Visible="True" Runat="server">
      <TABLE style="WIDTH: 100%" id=entryTable121 border=0 cellSpacing=0 
      cellPadding=2>
        <TR id=Tr2>
          <TD height=20 colSpan=4>
            <STYLE type=text/css>.reasonLst {
	BACKGROUND-COLOR: #f4f8fb; WIDTH: 100%; COLOR: red; FONT-WEIGHT: normal
}
.reasonLstHeader {
	DISPLAY: none
}
.reasonPanel {
	FONT-WEIGHT: normal
}
.AltItem {
	BACKGROUND-COLOR: white
}
</STYLE>
<asp:DataGrid id=dgReason CssClass="reasonLst" Width="100%" Runat="server" CellPadding="0" BorderWidth="0px" AutoGenerateColumns="False" HeaderStyle-CssClass="reasonLstHeader" AlternatingItemStyle-CssClass="AltItem">
													<Columns>
														<asp:TemplateColumn HeaderText="Sub-Standard Reasons">
															<ItemTemplate>
																<%# DataBinder.Eval(Container, "DataItem.RRS_REASONCODE") %>
																-
																<%# DataBinder.Eval(Container, "DataItem.RRS_REASONDESC") %>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:DataGrid></TD></TR></TABLE></asp:Panel>
							</asp:Panel>
						</td>
					</tr>
				</TABLE>
			</div>
			<div id="reasonDiv" class="NormalEntryTableDiv" runat="server">
			</div>
			<asp:Button id="Button1" runat="server" Width="0px" style="display:none;" onclick="Button1_Click"></asp:Button>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px; display:none;" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<script language="javascript">
		
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	/*fcStandardFooterFunctionsCall();*/
		/*
		function Page_ClientValidate()
		{
			return true;
		}
		*/
		function slideToggle(el, bShow)
		{
			var $el = $(el), height = $el.data("originalHeight"), visible = $el.is(":visible");
			if( arguments.length == 1 ) bShow = !visible;
			if( bShow == visible ) return false;		  
			
			if( !height )
			{	
				height = $el.show().height();
				$el.data("originalHeight", height);				
				if( !visible ) $el.hide().css({height: 0});
			}
			
			if( bShow )
			{								
				$el.slideDown();	
				$el.focus();			
			} 
			else 
			{
				$el.animate({height: 0}, {duration: 250, complete:function (){
					$el.hide();
				}
				});
			}
			
		}
		function posted(){
			for(i=1; i<=6; i++){
				var id = '#cb'+i;
				$(id).attr('checked', true);
				$(id).attr('disabled', true);
			}
		}
		function showChkIfStandard(){
			
			$('.rowValidationForPost').hide();
			var i=1;
			for(i=1; i<=6; i++){
				var id = '#cb'+i;
				$(id).hide();
			}
			
			
			if(document.getElementById('DecisionDescription').innerHTML.toUpperCase()=="STANDARD"){
				
				$('.rowValidationForPost').show();//Show All Rows
				
				for(i=1; i<=6; i++){
				var id = '#cb'+i;
				$(id).show();
				}
				
				
			}
			else if(document.getElementById('DecisionDescription').innerHTML.toUpperCase()!="PLEASE VALIDATE...")
			{//if(document.getElementById('DecisionDescription').innerHTML.toUpperCase()=="SUB STANDARD"){
				
				//$('.rowValidationForPost').show();//Show All Rows
				$("#TrHeading").show();
				
				for(i=1; i<=6; i++){
					var id = '#cb'+i;
					$(id).show();
					var trId = '#TrCb'+i;
					$(trId).show();
				}
				
				//$('#cb3').hide();
				//$('#TrCb3').hide();
				
			}
			
		}	showChkIfStandard();
		
		var btnClicked = "";
		
		function isChecked(sender, args){
			if(btnClicked == "VALIDATE"){//BTN Validate is clicked then stop Validators
				args.IsValid = true;
				return;
			}
			
			var chkID = sender.id.substring(0,sender.id.lastIndexOf('_'));
			var chk = document.getElementById(chkID);
			//alert(chk.style.display);
			if(!chk.checked && chk.style.display=='inline'){
				args.IsValid = false;
				return;
			}
			
			args.IsValid = true;
			return;

		}

		
		function hide(objPaymentType)
		{
			var objDecision = document.getElementById('ddlCDC_CODE');
			
			if(objPaymentType.value == "B")//Bank Debit Order
			{ 
				document.getElementById('rowtxtNP1_ACCOUNTNO').style.display="";
				document.getElementById('rowtxtNP1_CCNUMBER').style.display="none";
				document.getElementById('rowtxtNP1_CCEXPIRY').style.display="none";

				
				if(objDecision.value == APPROVED || objDecision.value == DECLINED || objDecision.value == POSTPONED)
				{
					document.getElementById('txtNP1_ACCOUNTNO').disabled=true;
					objPaymentType.disabled=true;
					parent.document.getElementById('PolicyAcceptance').height = 450;
					parent.parent.document.getElementById('mainContentFrame').style.height = '510.0px';
				}
			}
			else if(objPaymentType.value == "R")//Credit Card
			{ 
				document.getElementById('rowtxtNP1_ACCOUNTNO').style.display="none";
				document.getElementById('rowtxtNP1_CCNUMBER').style.display="";
				document.getElementById('rowtxtNP1_CCEXPIRY').style.display="";

				
				if(objDecision.value == APPROVED || objDecision.value == DECLINED || objDecision.value == POSTPONED)
				{
					document.getElementById('txtNP1_CCNUMBER').disabled=true;
					document.getElementById('txtNP1_CCEXPIRY').disabled=true;
					objPaymentType.disabled=true;
					parent.document.getElementById('PolicyAcceptance').height = 460;
					parent.parent.document.getElementById('mainContentFrame').style.height = '520.0px';//'460.0px';
				}
			}
			else
			{
				document.getElementById('txtNP1_CCNUMBER').value = "";
				document.getElementById('txtNP1_CCEXPIRY').value = "";
				document.getElementById('rowtxtNP1_ACCOUNTNO').style.display="none";
				document.getElementById('rowtxtNP1_CCNUMBER').style.display="none";
				document.getElementById('rowtxtNP1_CCEXPIRY').style.display="none";
				parent.document.getElementById('PolicyAcceptance').height = 405;
				parent.parent.document.getElementById('mainContentFrame').style.height = '460.0px';//'460.0px';



				//if(objDecision.value == APPROVED || objDecision.value == DECLINED || objDecision.value == POSTPONED)
				//{
				//	objPaymentType.disabled=true;
				//}				

				/*document.getElementById('poltxt').innerHTML = ' ';				
				document.getElementById('rowNP1_ISSUEDATE').style.display="none"; 
				document.getElementById('rowCRE_REASCODE').style.display="none"; 
				document.getElementById('rowNP1_DEFFERPERIOD').style.display="none"; 
				document.getElementById('rowNP1_POLICYNO').style.display="none"; 
				document.getElementById('rowNP1_DEFFERPERIOD').style.display="none";
				document.getElementById('rowCRE_REASCODE').style.display="none";*/
			}
		}

		function setAcceptanceFields(objDecision)
		{
			//if(objDecision.value == APPROVED || objDecision.value == DECLINED)
			if(objDecision.value == APPROVED || objDecision.value == DECLINED || objDecision.value == POSTPONED)
			{ 
			    document.getElementById('boldStuff').innerHTML = 'Approved Details';
				document.getElementById('rowNP1_POLICYNO').style.display="";
				document.getElementById('rowNP1_ISSUEDATE').style.display="";
				document.getElementById('rowNP1_DEFFERPERIOD').style.display="none";
				document.getElementById('rowCRE_REASCODE').style.display="none";
				document.getElementById('txtNP1_DEFFERPERIOD').value="";
				document.getElementById('ddlCRE_REASCODE').selectedIndex =0;
				document.getElementById('txtNP1_ISSUEDATE').value=sysDate;
     	        //document.getElementById('A').style.display="";
     	        document.getElementById('rowPaymentHeading').style.display="";
				document.getElementById('rowPremAmount').style.display="";
				document.getElementById('C').style.display="";
				document.getElementById('D').style.display="";
				document.getElementById('E').style.display="";
			}
			else
			{
				if(objDecision.value == DECLINED)
				{
			        document.getElementById('boldStuff').innerHTML = 'Declined Details';
					document.getElementById('rowNP1_DEFFERPERIOD').style.display="none";
					document.getElementById('txtNP1_DEFFERPERIOD').value="";
					//document.getElementById('A').style.display="none";
					document.getElementById('rowPremAmount').style.display="none";
					document.getElementById('C').style.display="none";
					document.getElementById('D').style.display="none";
					document.getElementById('E').style.display="none";
				}
				else
				{
				//	document.getElementById('boldStuff').innerHTML = 'Postpond Details';
					document.getElementById('rowNP1_DEFFERPERIOD').style.display="";
					//document.getElementById('A').style.display="none";
					document.getElementById('rowPremAmount').style.display="none";
					document.getElementById('C').style.display="none";
					document.getElementById('D').style.display="none";
					document.getElementById('E').style.display="none";
					 document.getElementById('rowCRE_REASCODE').style.display="none";
					document.getElementById('rowNP1_DEFFERPERIOD').style.display="none";
				}
				document.getElementById('rowNP1_POLICYNO').style.display="none";
				document.getElementById('rowNP1_ISSUEDATE').style.display="none";
				document.getElementById('txtNP1_POLICYNO').value="";
				document.getElementById('txtNP1_ISSUEDATE').value="";
				
				if(document.getElementById('ddlNP1_PAYMENTMET').style.display == "" ||  document.getElementById('rowPremAmount').style.display == "")
				{
					document.getElementById('rowPaymentHeading').style.display="";
				}
				else
				{
					document.getElementById('rowPaymentHeading').style.display="none";
				}
			}
			
		}
		document.getElementById('txtNP1_ISSUEDATE').disabled=true;
		try{
		parent.frames[1].document.getElementById('postBtn').disabled=false;
		}
		catch(ex){
		}
		setAcceptanceFields(document.getElementById('ddlCDC_CODE'))

		/*********************** Use to Secure the Page - Start **************************/		
		/*** Stop F5 ***/
		function document.onkeydown()
		{
			if (event.keyCode==116 /*|| event.keyCode==8*/)
			{
				event.keyCode = 0;
				event.cancelBubble = true;
				return false;
			}
		}		
		/*********************** Use to Secure the Page - End **************************/		
		
		/************************************************************************/
		/********************* Screen Parameterization **************************/
		setFieldStatusAsPerClientSetup("ACCEPTANCE");
		if(document.getElementById('ddlNP1_PAYMENTMET').style.display=="" ||  document.getElementById('rowPremAmount').style.display=="")
		{
			document.getElementById('rowPaymentHeading').style.display="";
		}
		else
		{
			document.getElementById('rowPaymentHeading').style.display="none";
		}		

		function beforeSave()
		{
			validateBeforeDecision();
			EnableFieldsBeforeSubmitting();
			return true;
		}
		function beforeUpdate()
		{
			validateBeforeDecision();
			EnableFieldsBeforeSubmitting();
			return true;
		}		
		
		function validateBeforeDecision()
		{
			var objPayment = document.getElementById('ddlNP1_PAYMENTMET');
			if(objPayment.style.visibility == 'visible' && objPayment.disabled == false)
			{
				if(objPayment.value == null || objPayment.value == "")
				{
					alert("Payment Type is mandatory.");
					objPayment.focus();
					return false;
				}
				else
				{
					if(document.getElementById('ddlNP1_PAYMENTMET').value == "B")
					{
						if(document.getElementById('txtNP1_ACCOUNTNO').value == "")
						{
							alert('Please enter Account Number');
							return false;	
						}
					}
					else if(document.getElementById('ddlNP1_PAYMENTMET').value == "R")
					{
						if(document.getElementById('txtNP1_CCNUMBER').value == "")
						{
							alert('Please enter Credit Card Number');
							return false;	

						}
						if(document.getElementById('txtNP1_CCNUMBER').value.length != 16)
						{
							alert('Credit Card Number must be 16 digits long');
							return false;			
						}
						if(document.getElementById('txtNP1_CCEXPIRY').value == "")
						{
							alert('Please enter Expiry Date');
							return false;	
						}	
					}					
				}
			}
			return true;
		}
		/************************************************************************/		
		</script>
		<script language="C#" runat="server">
			public int getHighLighted()
			{
				return 0;
			}
		</script>
	</body>
</HTML>
