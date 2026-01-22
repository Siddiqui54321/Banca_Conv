<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS" %>
<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<title>Riders / Benefits</title>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/JScriptTabular.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<script language="javascript" src="JSFiles/shma_validator.js"></script>
		<script language="javascript" src="../shmalib/jscript/ValidationMechanism.js"></script>
		<script language="javascript" src="../shmalib/jscript/GeneralFunctions.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralView.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/Validation/CallValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
		<STYLE type="text/css">
			<!--
			.hide {display:none}
			-->			
			.RIDER_TABLE{
				font-family: Verdana, Arial, Helvetica, sans-serif;
				color: #2f530A;
				font-weight: bold;
				font-size: 11px;
				width:auto;
				/*vertical-align: middle;
				align:center;*/
				/*table-layout: fixed;
				margin-left: 50%;
				margin-top: 50%;*/
				margin:auto;
				
			}
		</STYLE> 
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			totalRecords=<asp:Literal id="_totalRecords" runat="server" EnableViewState="False"></asp:Literal>+1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					
			//function SetStatus(rowStatus){					
			//	if (myForm.txtModifiedRows.value.indexOf(rowStatus) == -1)
			//		myForm.txtModifiedRows.value += rowStatus;
			//}	
			
			
			<asp:Literal id="QueryStringParameter" runat="server" EnableViewState="True"></asp:Literal>					
			

			/******** NOTE: *********************************************/
			/******** 1. Override the JScriptFG.js method.******************/
			/******** 2. Validation Error will be open in in Popup.*********/
			/******** 3. Normal Error will be alert only.*******************/
			/************************************************************/
			function ErrorMessage(errMsg)
			{
           
				if (errMsg=='<<Validation Error>>') 
				{   //open Popup for Validation error
					var wOpen;
					var sOptions;
					var aURL="../Presentation/ValidationError.aspx?ErrorSource=Benefit(s) Validation Error";
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
				{	//Normal alert for other than Validation error - Picked from JScriptFG.js file
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
		
		</script>
	</HEAD>
	<body onload="setBuiltinRiderFields();setSizeOfWindow();">
		
		<form id="myForm" method="post" runat="server">
			<br>
			<table class="RIDER_TABLE" id="riderTable">
            <tr class="form_heading"><td height="20" colspan="4" align=center >Riders / Benefits</td></tr>
			
			<tr>
			<td>
			<SHMA:DATAGRID id="EntryGrid" runat="server" HeaderStyle-CssClass="GridHeader"	AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow">
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
				<ItemStyle Font-Size="Smaller" Font-Names="Arial" BorderWidth="3px" BorderStyle="None" CssClass="ListRowItem"></ItemStyle>
				<AlternatingItemStyle CssClass="ListRowAlt"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
				
				<Columns>
					<asp:TemplateColumn>
						
						<HeaderTemplate><asp:CheckBox id="chkSelectAll" runat="server" style="width:0; display:none;" visible=False></asp:CheckBox></HeaderTemplate>
						
						<ItemTemplate>
								<shma:TextBox id="txtPPR_SUMASSURED_ENABLED" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtPPR_BENEFITTERM_ENABLED" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtNPR_EDUNITS_ENABLED" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtPPR_COMMLOADING_ENABLED" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtMinThresholdValue" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtMaxThresholdValue" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="lblNP1_PROPOSAL" style="width:0; display:none;" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="lblNP2_SETNO" style="width:0; display:none;" Text='<%# DataBinder.Eval(Container, "DataItem.NP2_SETNO") %>' runat="server" BaseType="Number" Precision="0"></shma:TextBox><shma:TextBox id="lblNPR_BASICFLAG" style="width:0; display:none;" Text='<%# DataBinder.Eval(Container, "DataItem.NPR_BASICFLAG") %>' runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="txtPPR_PRODCD" runat="server" Width='0' style="display:none;" Text='<%#DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>' ReadOnly="true" BaseType="Character" ></shma:TextBox><asp:CheckBox id="chkDelete" runat="server" Visible="false"></asp:CheckBox>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewNP1_PROPOSAL" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox><shma:TextBox id="lblNewNP2_SETNO" style="width:0; display:none;" runat="server" BaseType="Number" Precision="0"></shma:TextBox><shma:TextBox id="lblNewNPR_BASICFLAG" style="width:0; display:none;" runat="server" BaseType="Character"></shma:TextBox>
						</FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="&nbsp;&nbsp;\/">
						<ItemTemplate>
							<asp:CheckBox id="chkNPR_SELECTED" runat="server" style="width:10px"></asp:CheckBox></CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<asp:CheckBox id="chkNewNPR_SELECTED" runat="server" style="width:10px"></asp:CheckBox></CENTER>
						</FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Rider">
						<ItemTemplate>
							<shma:TextBox id="txtPPR_DESCR" runat="server" Width='20.0pc' Text='' ReadOnly="true" MaxLength="3"
								BaseType="Character" CssClass="DisplayOnly"></shma:TextBox>
						</ItemTemplate>
						<FooterTemplate></FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Benefit Amount">
						<ItemTemplate>
							<shma:TextBox id="txtNPR_SUMASSURED" runat="server" Width='7.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPR_SUMASSURED")%>' MaxLength="15" CssClass="RequiredField" 
								BaseType="Number" SubType="Currency" Precision="0"  >
							</shma:TextBox>
							<asp:CompareValidator id="cfvNPR_SUMASSURED" runat="server" ControlToValidate="txtNPR_SUMASSURED" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							
							<asp:requiredfieldvalidator id="rfvNPR_SUMASSURED" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNPR_SUMASSURED"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewNPR_SUMASSURED" runat="server" Width='6.0pc' MaxLength="15" CssClass="RequiredField"
								BaseType="Number" SubType="Currency" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNPR_SUMASSURED" runat="server" ControlToValidate="txtNewNPR_SUMASSURED"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<shma:Peeredrequiredfieldvalidator id="prfvNewNPR_SUMASSURED" runat="server" Precision="0" ErrorMessage="Required"
								ControlToValidate="txtNewNPR_SUMASSURED" ControlsToCheck="chkNewNPR_SELECTED" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Term">
						<ItemTemplate>
							<shma:TextBox id="txtNPR_BENEFITTERM" Width='3.0pc' runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.NPR_BENEFITTERM")%>' MaxLength="2" CssClass="RequiredField" 
								BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNPR_BENEFITTERM" runat="server" ControlToValidate="txtNPR_BENEFITTERM" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPR_BENEFITTERM" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNPR_BENEFITTERM"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewNPR_BENEFITTERM" Width='3.0pc' runat="server" MaxLength="2" CssClass="RequiredField"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNPR_BENEFITTERM" runat="server" ControlToValidate="txtNewNPR_BENEFITTERM"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<shma:Peeredrequiredfieldvalidator id="prfvNewNPR_BENEFITTERM" runat="server" Precision="0" ErrorMessage="Required"
								ControlToValidate="txtNewNPR_BENEFITTERM" ControlsToCheck="chkNewNPR_SELECTED" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>


					<asp:TemplateColumn HeaderText="Contribution">
						<ItemTemplate>
							<shma:TextBox id="txtNPR_PREMIUM" runat="server" Width='7.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPR_PREMIUM")%>' ReadOnly="true" MaxLength="15" BaseType="Number" SubType="Currency" Precision="2" CssClass="DisplayOnly"> </shma:TextBox>
							<asp:CompareValidator id="cfvNPR_PREMIUM" runat="server" ControlToValidate="txtNPR_PREMIUM" Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewNPR_PREMIUM" runat="server" Width='7.0pc' ReadOnly="true" MaxLength="15" BaseType="Number" SubType="Currency" Precision="2"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNPR_PREMIUM" runat="server" ControlToValidate="txtNewNPR_PREMIUM" Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<shma:Peeredrequiredfieldvalidator id="prfvNewNPR_EDUNITS" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNewNPR_EDUNITS" ControlsToCheck="chkNewNPR_SELECTED" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn HeaderText="%age">
						<ItemTemplate>
							<shma:TextBox id="txtNPR_EDUNITS" runat="server" Width='3.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPR_EDUNITS")%>' MaxLength="3" CssClass="RequiredField" 
								BaseType="Number" Precision="0">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNPR_EDUNITS" runat="server" ControlToValidate="txtNPR_EDUNITS" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPR_EDUNITS" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNPR_EDUNITS"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="txtNewNPR_EDUNITS" runat="server" Width='3.0pc' MaxLength="2" CssClass="RequiredField"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNPR_EDUNITS" runat="server" ControlToValidate="txtNewNPR_EDUNITS"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</FooterTemplate>
					</asp:TemplateColumn>
										
					<asp:TemplateColumn HeaderText="Option">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlNPR_COMMLOADING" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="csd_type" Width="10.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:requiredfieldvalidator id="rfvNPR_COMMLOADING" Enabled=False runat="server" ErrorMessage="Required" ControlToValidate="ddlNPR_COMMLOADING"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<SHMA:dropdownlist id="ddlNewNPR_COMMLOADING" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="csd_type" Width="10.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<shma:Peeredrequiredfieldvalidator  id="prfvNewNPR_COMMLOADING" runat="server" ErrorMessage="Required" ControlToValidate="ddlNewNPR_COMMLOADING"
								ControlsToCheck="chkNewNPR_SELECTED" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="">
						<ItemTemplate>
							<shma:TextBox id="txtFaceCurrency" runat="server" Width='0.0pc' style="display:none;" MaxLength="2" CssClass="DisplayOnly" BaseType="Character">
							</shma:TextBox>
						</ItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText=""> 
						<ItemTemplate>
							<shma:TextBox id="txtPremiumCurrency" runat="server" Width='0.0pc'  style="display:none;" MaxLength="2" CssClass="DisplayOnly" BaseType="Character" >
							</shma:TextBox>
						</ItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:TemplateColumn>

				</Columns>
			</SHMA:DATAGRID>
			</td>
			<tr>
			<td class="button2TD" align=right>
				<a href="#" class="button2" onclick="if(validateBenefits()==true) send(this,1);">&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;</a>
				<a href="#" class="button2" onclick="allToggle();">Select All</a>
				<a href="javascript: self.close ()" class="button2">&nbsp;&nbsp;&nbsp;Close&nbsp;&nbsp;&nbsp;</a>
			</td>
			</tr>
			<tr class="form_heading"><td height="20" colspan="4" align=center >
				<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; LEFT: 496px;display:none; POSITION: absolute; TOP: 80px"
					runat="server" Height="12px" Width="0px"></asp:textbox>
				<asp:textbox id="txtOrgCode" style="display:none;Z-INDEX: 102; LEFT: 664px; POSITION: absolute; TOP: 208px"
					runat="server" Width="0px"></asp:textbox>
				<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
				<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
				<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
				<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
				<INPUT id="_CustomEvent" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent"
					runat="server" onserverclick="_CustomEvent_ServerClick"> <input type="hidden" name="PkColumns" value="NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD">
				<input type="hidden" name="conditionCols" value="NPR_SUMASSURED,NPR_BENEFITTERM,NPR_EDUNITS,NPR_COMMLOADING">
				<input type="hidden" name="conditionVars" value="ddlNPR_SELECTED"> <input type="hidden" name="conditionVals" value="ddlNPR_SELECTED=='Y'">
			</td></tr>
			</table>

			

		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> fcStandardFooterFunctionsCall();
		
		function sendTb()
		{
			send();
			
		}
		
		function addClicked()
		{
			
			setFixedValuesInSession ("FLAG_INSERTALLOWED=Y");
			
			send();
			
			
		}




		function validate()
		{
			document.getElementById('rfvNPR_SUMASSURED').enabled = false;
			document.getElementById('rfvNPR_BENEFITTERM').enabled = false;
			//alert();
			document.getElementById('rfvNPR_EDUNITS').enabled = false;
			//alert(2);
			document.getElementById('rfvNPR_COMMLOADING').enabled = false;

			//*
			//SECTION 1 VALIDATIONS
			//*//
			if (document.getElementById('chkNPR_SELECTED').checked==true)
			{
				/*document.getElementById('rfvNPR_SUMASSURED').enabled = true;
				document.getElementById('rfvNPR_BENEFITTERM').enabled = true;
				
				document.getElementById('txtNPR_SUMASSURED').readOnly = false;
				document.getElementById('txtNPR_BENEFITTERM').readOnly = false;
				
				document.getElementById('txtNPR_SUMASSURED').className="RequiredField";
				document.getElementById('txtNPR_BENEFITTERM').className="RequiredField";

				
				document.getElementById('rfvNPR_COMMLOADING').enabled = false;//on requirements from client 02/07/08
				*/
				
				//*
				//SECTION 2 VALIDATIONS
				//*//

				//Sum Assured Parameter Based  Enable and Disable
				
				if (document.getElementById('txtPPR_SUMASSURED_ENABLED').value=='Y')
				{
					document.getElementById('rfvNPR_SUMASSURED').enabled = true;
					document.getElementById('txtNPR_SUMASSURED').readOnly = false;
					document.getElementById('txtNPR_SUMASSURED').className="RequiredField";
				}
				else
				{
					document.getElementById('rfvNPR_SUMASSURED').enabled = false;
					document.getElementById('txtNPR_SUMASSURED').readOnly = true;
					document.getElementById('txtNPR_SUMASSURED').className="DisplayOnly";
				}
				
				
				//Benefit Term Parameter Based  Enable and Disable
				if (document.getElementById('txtPPR_BENEFITTERM_ENABLED').value=='Y')
				{
					document.getElementById('rfvNPR_BENEFITTERM').enabled = true;
					document.getElementById('txtNPR_BENEFITTERM').readOnly = false;
					document.getElementById('txtNPR_BENEFITTERM').className="RequiredField";
				}
				else
				{
					document.getElementById('rfvNPR_BENEFITTERM').enabled = false;
					document.getElementById('txtNPR_BENEFITTERM').readOnly = true;
					document.getElementById('txtNPR_BENEFITTERM').className="DisplayOnly";
				}

				//alert(document.getElementById('txtPPR_BENEFITTERM_ENABLED').value);
				//if (document.getElementById('txtNPR_EDUNITS_ENABLED').value == 'Y')
				if (document.getElementById('txtPPR_BENEFITTERM_ENABLED').value == 'Y')
				{
					document.getElementById('rfvNPR_EDUNITS').enabled = true;
					document.getElementById('txtNPR_EDUNITS').readOnly = false;
					document.getElementById('txtNPR_EDUNITS').className="RequiredField";
				}
				else
				{
					document.getElementById('rfvNPR_EDUNITS').enabled = false;
					document.getElementById('txtNPR_EDUNITS').readOnly = true;
					document.getElementById('txtNPR_EDUNITS').className="DisplayOnly";
				}


				//Option Parameter Based  Enable and Disable
				if (document.getElementById('txtPPR_COMMLOADING_ENABLED').value=='Y')
				{
					document.getElementById('rfvNPR_COMMLOADING').enabled = false;  //true;
					document.getElementById('ddlNPR_COMMLOADING').disabled = false;
					document.getElementById('ddlNPR_COMMLOADING').className="RequiredField";
				}
				else
				{
					document.getElementById('rfvNPR_COMMLOADING').enabled = false;
					document.getElementById('ddlNPR_COMMLOADING').disabled = true;
					document.getElementById('ddlNPR_COMMLOADING').className="DisplayOnly";
				}
				
			}
			
			else 
			{

				//alert("validate "+document.getElementById('chkNPR_SELECTED').checked);

				document.getElementById('txtNPR_SUMASSURED').readOnly = true;
				
				document.getElementById('txtNPR_SUMASSURED').value = '0';
				document.getElementById('txtNPR_BENEFITTERM').value = '0';
				document.getElementById('txtNPR_BENEFITTERM').readOnly = true;
				document.getElementById('txtNPR_EDUNITS').readOnly = true;
				document.getElementById('ddlNPR_COMMLOADING').readOnly = true;
				

				document.getElementById('txtNPR_SUMASSURED').className="DisplayOnly";
				document.getElementById('txtNPR_BENEFITTERM').className="DisplayOnly";
				document.getElementById('txtNPR_EDUNITS').className="DisplayOnly";
				document.getElementById('ddlNPR_COMMLOADING').className="DisplayOnly";


				document.getElementById('rfvNPR_SUMASSURED').enabled = false;
				document.getElementById('rfvNPR_BENEFITTERM').enabled = false;
				document.getElementById('rfvNPR_EDUNITS').enabled = false;
				document.getElementById('rfvNPR_COMMLOADING').enabled = false;
				
				if(document.getElementById('chkNPR_SELECTED').enabled==true){
					document.getElementById('txtNPR_BENEFITTERM').value =0;
					document.getElementById('txtNPR_SUMASSURED').value=0;
					document.getElementById('txtNPR_PREMIUM').value=0;
				}
			}
			
		}
			
		//apply on specific tabular row when ddl changes
		//clean application by applying a lazy load function mechanism for complex business logics
		function setValidatorState(obj)
		{
			applyValidationSingleTabular(obj,validate,"chkNPR_SELECTED,rfvNPR_SUMASSURED,rfvNPR_BENEFITTERM,rfvNPR_EDUNITS,rfvNPR_COMMLOADING,txtNPR_SUMASSURED,ddlNPR_COMMLOADING,txtNPR_BENEFITTERM,txtNPR_EDUNITS,txtPPR_SUMASSURED_ENABLED,txtPPR_BENEFITTERM_ENABLED,txtNPR_EDUNITS_ENABLED,txtPPR_COMMLOADING_ENABLED");
			this.Page_ClientValidate();
		}

		//apply on all tabular grid when first renders
		applyValidationTabular(validate,"chkNPR_SELECTED,rfvNPR_SUMASSURED,rfvNPR_BENEFITTERM,rfvNPR_EDUNITS,rfvNPR_COMMLOADING,txtNPR_SUMASSURED,txtNPR_BENEFITTERM,txtNPR_EDUNITS,ddlNPR_COMMLOADING,txtPPR_SUMASSURED_ENABLED,txtPPR_BENEFITTERM_ENABLED,txtNPR_EDUNITS_ENABLED,txtPPR_COMMLOADING_ENABLED");

		
		
		function deletedAll()
		{
			/*myForm._CustomArgName.value = '';
			myForm._CustomArgVal.value = '';
			myForm._CustomEventVal.value = 'Delete';
			__doPostBack('_CustomEvent','');	
			*/
			
			if (callEvent('Delete','', '')==true)
				parent.openWait('deleting data');
		}
		
	
		setCurrencyTabular("txtNPR_SUMASSURED");
		//attachViewRiders('NPR_SUMASSURED');


		//focus event
		function validateRangeInfoOnFocus(obj, vfor)
		{
			if (parent.parent.newValidation == 'Y')
			{
				return;
			}
			
			window.status ="";
			try
			{
				var id = 'EntryGrid__'+(obj.id.split('_')[2])+"_";
				var selected = "N";
				if (document.getElementById(id+'chkNPR_SELECTED').checked==true) selected="Y";
				
				var product = document.getElementById(id+'txtPPR_PRODCD').value; 
				var validatefor = vfor; 
				var validatevalue = getDeformattedNumber(obj,2);
				var dbopt = document.getElementById(id+'ddlNPR_COMMLOADING').value ;
			
				//following columns comes from query string
				var proposal = strProposal;
				var age = strAge;
				var term = strTerm;
				var sa = strSA;//Sum Assured from Basic Plan
				var BasicPlan_calcBasisFlag = strCalcBasis;
				
				//Check Calculation basis in Basis Plan
				if(BasicPlan_calcBasisFlag == 'T') //Total Premium instead of 
				{
					//Pick Sum Assured from current Rider
					sa = getDeformattedNumber(document.getElementById(id+'txtNPR_SUMASSURED'),2) ;
				}
				
				//Check validation
				var sql = "select CHECK_VALIDATION('"+selected+"','"+proposal+"','"+product+"','"+validatefor+"',null,"+age+","+term+","+sa+","+(dbopt==""?"null":dbopt)+") from dual"; 
				//alert("onfocus : " + sql);
				var result = fetchDataArray(sql);
			
				var caption = '';
				if (validatefor=='SUMASSURED')
					caption = 'Benefit Amount';//'Sum Assured Amount';
				else if (validatefor=='BTERM')
					caption = 'Term';//'Benefit Term';
				else if (validatefor=='MATURITYAGE')
					caption = 'Premium Paid upto Age';
			
				if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
				{
					window.status = result[1][0];
				}
			}
			catch(e)
			{
				alert("Exception : " +e.message);
			}
		}
		
		//blur event
		function validateRangeOnBlur(obj,vfor)
		{
			if (parent.parent.newValidation == 'Y')
			{
				return;
			}
			
			window.status ="";
			try
			{
				var index = parseInt( (obj.id.split('_')[2]).replace("ctl","") -2);
				//alert(index);
				//alert(myForm.txtModifiedRows.value);
				var id = 'EntryGrid__'+(obj.id.split('_')[2])+"_";
				var selected = "N"; 
				if (document.getElementById(id+'chkNPR_SELECTED').checked==true) selected="Y";

				var product = document.getElementById(id+'txtPPR_PRODCD').value; 
				var validatefor = vfor; 
				var validatevalue = getDeformattedNumber(obj,2);
				var dbopt = document.getElementById(id+'ddlNPR_COMMLOADING').value ;
			
				//following columns comes from query string
				var proposal = strProposal ;
				var age = strAge;
				var term = strTerm;
				var sa = strSA;//Sum Assured from Basic Plan
				var BasicPlan_calcBasisFlag = strCalcBasis;
				
				
				//Check Calculation basis in Basis Plan
				if(BasicPlan_calcBasisFlag == 'T') //Total Premium instead of 
				{
					sa = getDeformattedNumber(document.getElementById(id+'txtNPR_SUMASSURED'),2) ;
				}

				//Check validation
				var sql = "select CHECK_VALIDATION('"+selected+"','"+proposal+"','"+product+"','"+validatefor+"',"+validatevalue+","+age+","+term+","+sa+ ","+(dbopt==""?"null":dbopt)+") from dual"; 
				//alert("Blur : " + sql);
				var result = fetchDataArray(sql);

				var caption = '';
				if (validatefor=='SUMASSURED')
					caption = 'Benefit Amount';//'Sum Assured Amount';
				else if (validatefor=='BTERM')
					caption = 'Term';//'Benefit Term';
				else if (validatefor=='MATURITYAGE')
					caption = 'Premium Paid upto Age';
			
				if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
				{
					window.status = result[1][0];
					alert(caption + " : " + result[1][0]);
					//code by nawab 
					if (validatefor=='SUMASSURED')
					{
						var fld1 = document.getElementById(id + 'txtNPR_SUMASSURED');
						var rslt = result[1][0];
						fld1.value =rslt.substring(rslt.lastIndexOf("TO")+3);
						applyNumberFormat(fld1,2);
					}
					else if (validatefor=='BTERM')
					{
						var fld2 = document.getElementById(id + 'txtNPR_BENEFITTERM');
						var rslt = result[1][0];
						fld2.value = rslt.substring(rslt.lastIndexOf("TO")+3);
						applyNumberFormat(fld2,0);
					}
					//end code by nawab
					
					SetStatus(index);
					//alert(index);
				}
			}
			catch(e)
			{
				alert("Exception : " +e.message);
			}
		}		
		
		function toggleUnSelectAll()
		{
			//alert(totalRecords);
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+"chkNPR_SELECTED";
				document.getElementById(item).checked=false;
				setValidatorState(document.getElementById(item));
				SetStatus(i);
			}
			//alert(myForm.txtModifiedRows.value);
		}

		function toggleSelectAll()
		{
			//alert(totalRecords);
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+"chkNPR_SELECTED";
				document.getElementById(item).checked=true;
				//
				setValidatorState(document.getElementById(item));
				//alert(i);
				SetStatus(i);
			}
			//alert(myForm.txtModifiedRows.value);
			
		}

		function isAllSelected()
		{
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+"chkNPR_SELECTED";
				if(document.getElementById(item).checked==false)
					return false;
			}
			return true;
		}
		//try{parent.frames[3].initialSelect();}catch(e){}
		
		function attachCaptionsTabular(item)
		{
			var caption = '';
			if (item.indexOf('NPR_SUMASSURED')!=-1)
				caption = 'Required Amount';

			if (item.indexOf('NPR_BENEFITTERM')!=-1)
				caption = 'Benefit Term';

			if (item.indexOf('NPR_PREMIUM')!=-1)
				caption = 'Premium';

			if (item.indexOf('NPR_COMMLOADING')!=-1)
				caption = 'Option';
				
			if (item.indexOf('NPR_SUMASSURED')==-1 && item.indexOf('NPR_BENEFITTERM')==-1)
				document.getElementById(item).attachEvent('onfocus',  new Function("function1('"+caption+"')"));

			/*if (item.indexOf('NPR_SUMASSURED')!=-1)
			{
				document.getElementById(item).attachEvent('onfocus',  new Function("validateRangeInfoOnFocus(this,'SUMASSURED');"));
			}

			if (item.indexOf('NPR_BENEFITTERM')!=-1)
				document.getElementById(item).attachEvent('onfocus',  new Function("validateRangeInfoOnFocus(this,'TERM');"));
			*/


		}

		attachViewFocusTabular('INPUT');
		attachViewFocusTabular('SELECT');

		var selected = false;
		function selectDeselectImage(type)
		{
			if (selected==true && type=='u')
				document.getElementById('toggle').src='../shmalib/images/buttons/dSelRider_u.gif';

			if (selected==true && type=='v')
				document.getElementById('toggle').src='../shmalib/images/buttons/dSelRider_v.gif';

			if (selected==false && type=='u')
				document.getElementById('toggle').src='../shmalib/images/buttons/selRider_u.gif';

			if (selected==false && type=='v')
				document.getElementById('toggle').src='../shmalib/images/buttons/selRider_v.gif';
		}
		
		function allToggle()
		{

			if (isAllSelected()==true)
			{
				toggleUnSelectAll();
				selected = false;
				return;
			}
				
			if (isAllSelected()==false)
			{
				toggleSelectAll();
				selected = true;
				return;
			}

		}
		
		
		
		function calcPremium()
		{
			window.opener.parent.frames[2].calculate_Premium();
			window.close();
			//window.opener.frames[3].test();
		}
		
		/*function CloseWindow(){window.close();}*/
		
	/*function calculate_Premium()
	{
		if (confirm("Are you sure to calculate premium?")==true)
		{
			//if (parent.frames[1].Page_ClientValidate())
			//{
				//saveUpdate();
				//sparent.openWait('calculating premium');
				//setTimeout("parent.frames[1].executeProcess('ace.Calculate_Premium');", 1000); 
				executeProcess('ace.Calculate_Premium');
			//}
		}
	}*/
	
		//alert(getTabularFieldByIndex(1,"NP1_PROPOSAL").value);
		function setBuiltinRiderFields()
		{
			var proposal = getTabularFieldByIndex(1,"NP1_PROPOSAL").value;
			for (i=1;i<totalRecords;i++) 
			{
				var rider = getTabularFieldByIndex(i,"PPR_PRODCD").value
				var query = "SELECT PRI_BUILTIN FROM LPRI_RIDER LPRI WHERE LPRI.PPR_RIDER='" + rider + "' "
				          + "AND LPRI.PPR_PRODCD=(SELECT DISTINCT PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + proposal + "' AND NPR_BASICFLAG='Y') ";
				var builtin = fetchDataArray(query);
				if (builtin!=null && builtin.length>0 && builtin[1][0]!=null && builtin[1][0]!="")
				{
					if(builtin[1][0] == "Y")
					{
						var item = "EntryGrid__ctl"+eval(eval("000000"+i)+1)+"_";
						document.getElementById(item+"chkNPR_SELECTED").checked=true;
						document.getElementById(item+"chkNPR_SELECTED").disabled=true;
						document.getElementById(item+"txtNPR_SUMASSURED").disabled=true;
						document.getElementById(item+"txtPPR_DESCR").disabled=true;
						document.getElementById(item+"txtNPR_BENEFITTERM").disabled=true;
						document.getElementById(item+"txtNPR_EDUNITS").disabled=true;
						document.getElementById(item+"txtNPR_PREMIUM").disabled=true;
						document.getElementById(item+"ddlNPR_COMMLOADING").disabled=true;
					}
				}
			}
		}	
		
		function LostFocus_EDUNITS(obj)
		{
		
		}
		for(i=1; i<totalRecords; i++)
		{
			//var item = "EntryGrid__ctl"+eval(eval("000000"+i)+1)+"_";
			var obj = document.getElementById("EntryGrid__ctl"+eval(eval("000000"+i)+1)+"_txtNPR_SUMASSURED");
			applyNumberFormat(obj, 0);
			
			obj = document.getElementById("EntryGrid__ctl"+eval(eval("000000"+i)+1)+"_txtNPR_PREMIUM");
			applyNumberFormat(obj, 0);

			//alert(i);
		}
		//applyNumberFormat(this, 2);
		
		function setSizeOfWindow()
		{
			var width  = document.getElementById('riderTable').offsetWidth;
			var height = document.getElementById('riderTable').offsetHeight;
			
			resizeOuterTo(width+50,height+90)
			
			var left = parseInt((parent.parent.screen.availWidth/2) - (width/2));
			var top = parseInt((parent.parent.screen.availHeight/2) - (height/2));
			self.moveTo(left,top);
		}
		
		function resizeOuterTo(w,h) 
		{
			if (parseInt(navigator.appVersion)>3) 
			{
				if (navigator.appName=="Netscape") 
				{
					top.outerWidth=w;
					top.outerHeight=h;
				}
				else 
				{
					top.resizeTo(w,h);
				}
			}
		}

		</script>
	</body>
</HTML>
