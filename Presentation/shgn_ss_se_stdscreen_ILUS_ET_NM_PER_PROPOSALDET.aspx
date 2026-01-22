<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="IE=EmulateIE8" http-equiv="X-UA-Compatible">
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="CSSLiteral" EnableViewState="True" runat="server"></asp:literal>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
		_deleteEvent = '<asp:Literal id="_deleteEvent" runat="server" EnableViewState="True"></asp:Literal>';
		
		

		<!-- <!--column-management-array--> -->

		function RefreshFields()
		{				
							 //myForm.ddlCCN_CTRYCD.selectedIndex =0;
							 //myForm.ddlUSE_USERID.selectedIndex =0;
							 //myForm.ddlNP1_CHANNELDETAIL.selectedIndex =0;
							 //myForm.txtNP1_PRODUCER.value ="";
			if(myForm.txtNP1_PROPOSAL.disabled==true)
				 myForm.txtNP1_PROPOSAL.disabled=false;
				 myForm.txtNP1_PROPOSAL.value ="";
							 //myForm.txtNP2_COMMENDATE.value = valSYSDATE;
							 myForm.txtNP1_PROPDATE.value = valSYSDATE;
							 myForm.dtNP1_PROPDATEoptional.value = valSYSDATE;
			
			//myForm.txtNP1_PROPOSAL.focus();
			setFixedValuesInSession('NP1_PROPOSAL=');
			setFixedValuesInSession('NP2_SETNO=');
			setFixedValuesInSession('NPH_CODE=');
			setFixedValuesInSession('NPH_LIFE=');
			setFixedValuesInSession('PPR_PRODCD=');
			setFixedValuesInSession('CCB_CODE=');
			setFixedValuesInSession('CMO_MODE=');
			setFixedValuesInSession('NP1_TOTALRIDERPREM=');
			setFixedValuesInSession('NPR_TOTPREM=');
			//setFixedValuesInSession('NP1_PROPOSAL=');
			setFixedValuesInSession('NP2_SETNO=');
			setFixedValuesInSession('NPR_BASICFLAG=');
			setFixedValuesInSession('NP1_RETIREMENTAGE=');
			setFixedValuesInSession('NP1_TOTALANNUALPREM=');

			setDefaultValues();
			
		}
			
		function setProposalInfoInPersonelPage()
		{
			//alert("select 'A' RESULT from LNPR_PRODUCT F WHERE F.NP1_PROPOSAL='" + v_NP1_PROPOSAL + "' AND NPR_BASICFLAG='Y' AND F.NPR_PREMIUM IS NOT NULL AND F.NPR_PREMIUM > 0");
			var allowAVAP = fetchDataArray("select 'A' RESULT from LNPR_PRODUCT WHERE NP1_PROPOSAL='" + myForm.txtNP1_PROPOSAL.value + "' AND NPR_BASICFLAG='Y' AND PPR_PRODCD in('921','922','923') ");
			
			//alert("select 'A' RESULT from LNPR_PRODUCT WHERE NP1_PROPOSAL='" + myForm.txtNP1_PROPOSAL.value + "' AND NPR_BASICFLAG='Y' AND PPR_PRODCD in('921','922','923')");
			//alert(fetchArray_getRowCount(allowAVAP));
			if (fetchArray_getRowCount(allowAVAP) > 0 ) 
			{
				parent.parent.allowAVAPScreen = false;
			}
			else
			{
				parent.parent.allowAVAPScreen = true;
			}
		}
		
			

		/********** dependent combo's queries **********/
		function Get_PropRef()
		{		
			var PropRef = fetchDataArray("SELECT NP1_PROPOSALREF FROM lnp1_policymastr  WHERE NP1_PROPOSALREF = '"+myForm.txtNP1_PROPREF.value+"' ");			
			if (fetchArray_getRowCount(PropRef) > 0 ) 
			{
				alert("Proposal reference no. already assigned. Please enter some other Proposal reference no.");
				getField("txtNP1_PROPREF").focus();
				getField("txtNP1_PROPREF").select();
				
				return false;
			}
			else 
			{
				return true;
			}
		}
		
		
		</script>
          <script type="text/javascript" language="javascript">

              function cancelBack(val) {
                  if (val == 0) {
                      var key = event.keyCode;
                      if (key == 8) {
                          return false;
                      }
                      else {
                      }
                  }
                  else {
                  }

              }

              function backspaceFunc(e) {
                  var key = event.keyCode;
                  if (key == 8) {
                      var str = document.getElementById(e).value;
                      var newStr = str.substring(0, str.length - 1);
                      document.getElementById(e).value = newStr;
                  }
              }
    </script>
	</HEAD>
	<body onload="setProposalInfoInPersonelPage();" onkeydown="return cancelBack(0)">
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Personal Proposal Details" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server">
				<!--<fieldset id="FldSetProposal" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; BORDER-BOTTOM: #e0f3be 1px solid"><legend>Entry</legend>-->
				<TABLE id="entryTable" border="0" cellSpacing="0" cellPadding="2">
					<tr class="form_heading">
						<td height="20" width="1000" colSpan="4">&nbsp; Proposal
						</td>
					</tr>
					<tr>
						<td height="10" colSpan="4"></td>
					</tr>
					<TR id="rowCCN_CTRYCD" class="TRow_Normal">
						<TD id="TDddlCCN_CTRYCD" width="110" align="right">Country
						</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlCCN_CTRYCD" tabIndex="1" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="CCN_CTRYCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST></TD>
						<TD id="lbltxtNP1_PROPOSAL" width="110" align="right">Proposal</TD>
						<TD id="ctltxtNP1_PROPOSAL" width="186"><shma:textbox onblur="Proposal_LostFocus(this);" id="txtNP1_PROPOSAL" tabIndex="3" runat="server"
								CssClass="RequiredField" Width="165px" BaseType="Character" ReadOnly="true" MaxLength="12"></shma:textbox><INPUT class="BUTTON" title="Open Proposal List Of Values" onclick="openProposalLOV();"
								value=".." type="button" name="ProposalLov">
						</TD>
					</TR>
					<TR id="rowNP1_PROPOSAL" class="TRow_Alt">
						<TD id="TDtxtNP1_PROPDATE" width="110" align="right">Proposal Date</TD>
						<TD width="186"><SHMA:DATEPOPUP id="txtNP1_PROPDATE" tabIndex="3" runat="server" CssClass="RequiredField" Width="170px"
								ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0"></SHMA:DATEPOPUP><asp:comparevalidator id="msgNP1_PROPDATE" runat="server" CssClass="CalendarFormat" Display="Dynamic"
								ErrorMessage="{dd/mm/yyyy} " Type="Date" Operator="DataTypeCheck" ControlToValidate="txtNP1_PROPDATE"></asp:comparevalidator></TD>
						<TD id="TDtxtNP2_COMMENDATE" width="110" align="right">Commencement Date</TD>
						<TD width="186"><SHMA:DATEPOPUP id="txtNP2_COMMENDATE" tabIndex="4" runat="server" CssClass="RequiredField" Width="170px"
								ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0"></SHMA:DATEPOPUP><asp:comparevalidator id="msgNP2_COMMENDATE" runat="server" CssClass="CalendarFormat" Display="Dynamic"
								ErrorMessage="{dd/mm/yyyy} " Type="Date" Operator="DataTypeCheck" ControlToValidate="txtNP2_COMMENDATE"></asp:comparevalidator></TD>
					</TR>
					<TR id="optionalrowNP1_PROPOSALREF" class="TRow_Alt">
						<TD id="TDtxtNP1_PROPREF" width="110" align="right">Proposal Reference</TD>
						<TD><shma:textbox id="txtNP1_PROPREF" runat="server" CssClass="RequiredField" BaseType="Character" onkeydown="backspaceFunc('txtNP1_PROPREF')"
								MaxLength="50" onblur="Get_PropRef();"></shma:textbox>(Optional)</TD>
						<TD id="TDtxtNP1_PAYMENTREF" width="110" align="right">Payment Reference</TD>
						<TD><shma:textbox id="txtNP1_PAYMENTREF" runat="server" CssClass="RequiredField" BaseType="Character" onkeydown="backspaceFunc('txtNP1_PAYMENTREF')"
								MaxLength="50"></shma:textbox>(Optional)</TD>
					</TR>
					<TR id="optionalrowNP1_PROPOSAL" class="TRow_Alt">
						<TD id="TDtxtNP1_PROPDATEoptional" width="110" align="right">Proposal Signing Date</TD>
						<TD><SHMA:DATEPOPUP id="dtNP1_PROPDATEoptional" tabIndex="3" runat="server" CssClass="RequiredField"
								Width="110px" onblur="CheckEntryDate();" ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js"
								maxlength="0"></SHMA:DATEPOPUP>(Optional)
							<asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="CalendarFormat" Display="Dynamic"
								ErrorMessage="{dd/mm/yyyy} " Type="Date" Operator="DataTypeCheck" ControlToValidate="dtNP1_PROPDATEoptional"></asp:comparevalidator></TD>
						<TD width="110"></TD>
						<TD></TD>
					</TR>
					<TR id="rowNP1_CHANNEL">
						<TD width="0" align="right"><!--Channel--></TD>
						<TD width="0"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNEL" tabIndex="7" runat="server" CssClass="RequiredField" Width="0" style="display:none;"
								DataValueField="csd_type" DataTextField="desc_f" BlankValue="False"></SHMA:DROPDOWNLIST></TD>
						<TD width="110" align="right"><!--Channel Detail--></TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNELDETAIL" tabIndex="8" runat="server" CssClass="RequiredField" Width="0" style="display:none;"
								DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST></TD>
					</TR>
					<TR id="rowNP1_CHANNELDETAIL">
						<TD width="0" align="right"><!-- User --></TD>
						<TD><SHMA:DROPDOWNLIST id="ddlUSE_USERID" tabIndex="2" runat="server" CssClass="RequiredField" Width="0" style="display:none;"
								DataValueField="USE_USERID" DataTextField="desc_f" BlankValue="False"></SHMA:DROPDOWNLIST></TD>
						<TD width="0" align="right"><!--Producer--></TD>
						<TD width="0"><shma:textbox id="txtAAG_NAME" tabIndex="9" runat="server" CssClass="RequiredField" Width="0" style="display:none;"
								BaseType="Character" MaxLength="50"></shma:textbox></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<td width="106">
							<P><asp:label id="lblServerError" EnableViewState="false" runat="server" ForeColor="Red" Visible="False"></asp:label></P>
						</td>
						<TD width="186"></TD>
						<TD width="106"></TD>
						<TD width="186"></TD>
					</TR>
				</TABLE>
				<!--</fieldset>--></div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server" >
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px;display: none;" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry" >
			<shma:textbox id="txtNP2_SETNO" runat="server" Width="0px" BaseType="Number" Precision="0" Height="0px" style="display: none" ></shma:textbox><shma:textbox id="txtNP1_PRODUCER" runat="server" Width="0px" BaseType="Character" Height="0px" style="display: none" ></shma:textbox><asp:comparevalidator id="cfvNP2_SETNO" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
				Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNP2_SETNO"></asp:comparevalidator>
			<script language="javascript">
				
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall(); 
		try{
			parent.frames[3].deleteLogic(_deleteEvent);
		}catch(e){}
		
		function openProposalLOV()
		{
			
			var wOpen;
			var sOptions;
			var aURL="../Presentation/ProposalSelectionLOV.aspx";
			var aWinName="PROPOSAL_LIST";

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /1.5).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
			sOptions = sOptions + ',left=220,top=300';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}
		
		getField("CCN_CTRYCD").disabled=true;
		getField("USE_USERID").disabled=true;
		//getField("NP2_COMMENDATE").disabled=true;

		parent.parent.document.getElementById('txtNP1_PROPOSAL').value = getField("NP1_PROPOSAL").value;
		document.getElementById('txtNP1_PROPOSAL').value = getField("NP1_PROPOSAL").value;

		attachViewByNameNormal('txtNP2_COMMENDATE');
		
		
		/************************************************************************/
		/********************* Screen Parameterization **************************/
		//hideColumn();
		setFieldStatusAsPerClientSetup("PROPOSAL");
		
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
		
		function Proposal_LostFocus(obj)
		{
			if(obj.style.visibility == 'visible')
			{
				if(obj.disabled == false)
				{
					if(obj.value == null || obj.value == "")
					{
						alert("Proposal is mandatory.");
						obj.focus();
						return false;
					}
				}
			}
		}
		
		if(myForm.txtNP1_PROPOSAL.disabled == false)
		{
			if(myForm.txtNP1_PROPOSAL.readOnly == false)
			{
				if(_lastEvent != 'New')
				{
					myForm.txtNP1_PROPOSAL.disabled = true;
				}
			}
		}
		
		//optional
		function CheckEntryDate(){

	var propdate= myForm.dtNP1_PROPDATEoptional.value;
    var pparts = propdate.split("/");
    var pday = parseInt(pparts[0], 10);
    var pmonth = parseInt(pparts[1], 10);
    var pyear = parseInt(pparts[2], 10);
    var pd=pday + "/" + pmonth + "/" + pyear;
    var pnow= new Date(pyear,pmonth-1,pday);
        
    var sysdate= valSYSDATE;
    var sparts = sysdate.split("/");
    var sday = parseInt(sparts[0], 10);
    var smonth = parseInt(sparts[1], 10);
    var syear = parseInt(sparts[2], 10);
    var sd=sday + "/" + smonth + "/" + syear;
    var snow= new Date(syear,smonth-1,sday);

    
	//	if (pyear == syear && pmonth == smonth && pday > sday 
	//	|| pyear == syear && pmonth > smonth 
	//	|| pyear > syear ) {
		//if (myForm.dtNP1_PROPDATEoptional.value > valSYSDATE) {
		
		if (pnow > snow) {
              alert('Date is greater than current system Date ');
              myForm.dtNP1_PROPDATEoptional.value = valSYSDATE;
        }
        
		//if (pyear == syear && pmonth < smonth && pday < sday-30
		//|| pyear < syear ) {
		//if (myForm.dtNP1_PROPDATEoptional.value +30 < valSYSDATE) {
		
		pnow=pnow.setDate(pnow.getDate()+30);
		if (pnow < snow) {
              if (confirm('Date is older than current system Date \n Do you want to proceed ?')) {
                  //return true;
              }
              else {
              myForm.dtNP1_PROPDATEoptional.value = valSYSDATE;
                  //return false;
              }
        }
		}
		
		/************************************************************************/		
		</script>
	</body>
</HTML>
