<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_BENEFECIARY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_BENEFECIARY" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
        <title></title>
		<meta content="IE=EmulateIE8" http-equiv="X-UA-Compatible">
		<meta name="vs_snapToGrid" content="True">
		<meta name="vs_showGrid" content="False">
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:literal>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/Date.js"></SCRIPT>
		<script language="javascript" type="text/javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" type="text/javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" type="text/javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" type="text/javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>

		<script language="javascript" type="text/javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
             
		var ByAmount  = "01";
		var ByPercent = "02";
		
		function RefreshFields()
		{				
							 myForm.txtNBF_BENNAME.value ="";
							 myForm.txtNBF_DOB.value ="";
							 myForm.txtNBF_AGE.value ="0";
							 myForm.ddlCRL_RELEATIOCD.selectedIndex =0;
							 myForm.txtNBF_AMOUNT.value ="0";
							 myForm.txtNBF_PERCNTAGE.value ="0";
			

			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		
		
		//***************************************************************************//
		//******************* Guardian Related function - Start *********************//
		//***************************************************************************//
		function openGuardian(proposal, beneficiary, guardian)
		{
			var wOpen;
			var sOptions;
			var aURL="../Presentation/shgn_ss_se_stdscreen_ILUS_ST_GUARDIAN.aspx?PROPOSAL=" + proposal + "&BENEFICIARY=" + beneficiary + "&GUARDIAN=" + guardian;
			var aWinName="ILUS_ET_TB_BENEFECIARY";

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /1.6).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /1.6).toString();
			sOptions = sOptions + ',left=200,top=150';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}		
		
		
		function showGaurdianInToolTip(obj)
		{
			//lister__ctl0_ListerRow
			//alert(document.getElementById('lister__ctl1_linkNBF_BENEFCD2').innerHTML);
			
			var proposal  = getField("NP1_PROPOSAL").value;
			var id = obj.id;
			var serial = id.replace("lister__ctl","").replace("_ListerRow","");
			
			var arrBenAge = document.getElementsByName("BenAge");
			var age = parseInt(arrBenAge[serial].value);
			
			if(age < 18)
			{
				var text = "Guardian Not defined.";
				var arrGuardCode = document.getElementsByName("NGU_GUARDCD");
				var guardCode = arrGuardCode[serial].value;
				
				if(trim(guardCode).length > 0)
				{
					text = loadGuardianInfo(proposal, guardCode);
				}
				Tip(text, SHADOW, true, TITLE, 'Guardian Info', PADDING, 9);
			}
		}

		function loadGuardianInfo(proposal, guardian)
		{
			var className="ace.ILUS_ST_GUARDIAN";
			var methodName="getGuardianInfo";
			var parameters = proposal + "," + guardian;
			var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			
			var text = "";
			if(str_resultArray[0]!="")
			{
				text = "<TABLE border=1 Width='350px' cellspacing=0 cellpadding=0>";
				text += "<TR><td Width='70px'><b>Guardian:</b></td><td Width='280px'>" + str_resultArray[0] + " - " + str_resultArray[1] +  "</td></TR>";
				text += "<TR><td Width='70px'><b>Relation:</b></td><td Width='280px'>" + str_resultArray[2] + " - " + str_resultArray[3] + "</td></TR>";
				text += "</TABLE>";
			}
			return text;

		}
		
		//***************************************************************************//
		//******************* Guardian Related function - End *********************//
		//***************************************************************************//

        function cancelBack() {
             var key = event.keyCode;
             if (key == 8) {
                 return false;
             }
             else {
             
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
	<body onkeydown="return cancelBack(0)"> <!--****************************************************************************-->
		<!--******************************* Tool Tip Related ***************************-->
		<!--***** Link Web Site http://www.walterzorn.com/tooltip/tooltip_e.htm ********-->
		<!--****************************************************************************-->
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/ToolTips/wz_tooltip.js"></SCRIPT>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/ToolTips/tip_centerwindow.js"></SCRIPT>
		<SCRIPT language="JavaScript" type="text/javascript" src="../shmalib/jscript/ToolTips/tip_followscroll.js"></SCRIPT>
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Benefeciary" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<table style="WIDTH:100%; LEFT: 50px" border="0">
				<tr>
					<td>
						<div style="Z-INDEX: -111" id="EntryTableDiv" runat="server">
							<TABLE id="entryTable1" border="0" cellSpacing="0" cellPadding="2">
								<SHMA:TEXTBOX style="visibility:hidden" id="txtNBF_BENEFCD" runat="server" BaseType="Character"></SHMA:TEXTBOX>
								<asp:comparevalidator id="cfvNBF_BENEFCD" runat="server" Display="Dynamic" EnableClientScript="False"
									ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNBF_BENEFCD"></asp:comparevalidator>
								<SHMA:TEXTBOX style="visibility:hidden" id="txtNP1_PROPOSAL" runat="server" BaseType="Character"></SHMA:TEXTBOX>
								<asp:comparevalidator id="cfvNP1_PROPOSAL" runat="server" Display="Dynamic" EnableClientScript="False"
									ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP1_PROPOSAL"></asp:comparevalidator>
								<tr class="form_heading">
									<td height="20" colSpan="4">&nbsp; Beneficiary
									</td>
								</tr>
								<tr>
									<td height="10" colSpan="4"></td>
								</tr>
							</TABLE>
							<TABLE id="entryTable" border="0" cellSpacing="0" cellPadding="2">
								<TR id="rowNBF_BENNAME" class="TRow_Normal">
									<TD style="HEIGHT: 23px" id="lbltxtNBF_BENNAME" width="116" align="right">Name&nbsp;&nbsp;</TD>
									<TD style="HEIGHT: 23px" id="ctltxtNBF_BENNAME" width="186"><SHMA:TEXTBOX id="txtNBF_BENNAME" tabIndex="1" runat="server" BaseType="Character" MaxLength="60" onkeydown="backspaceFunc('txtNBF_BENNAME')"
											Width="13.0pc" onchange="valbenfstr(this);" onblur="toTitleCase(this);"></SHMA:TEXTBOX><asp:comparevalidator id="cfvNBF_BENNAME" runat="server" Display="Dynamic" EnableClientScript="False"
											ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNBF_BENNAME"></asp:comparevalidator></TD>
									<TD id="lblddlCRL_RELEATIOCD" width="106" align="right">Relation&nbsp;&nbsp;</TD>
									<TD style="HEIGHT: 23px" id="ctlddlCRL_RELEATIOCD" width="186">
                                        <SHMA:DROPDOWNLIST id="ddlCRL_RELEATIOCD" tabIndex="2" runat="server" Width="13.0pc" DataValueField="CRL_RELEATIOCD" onkeydown="return cancelBack()"
											DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST>
                                        <asp:comparevalidator id="cfvCRL_RELEATIOCD" runat="server" Display="Dynamic" EnableClientScript="False"
											ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCRL_RELEATIOCD"></asp:comparevalidator>

									</TD>
								</TR>
								<TR id="rowNBF_AGE" class="TRow_Alt">
									<TD style="HEIGHT: 23px" id="lbltxtNBF_DOB" width="116" align="right">Date of 
										Birth&nbsp;&nbsp;</TD>
									<TD style="HEIGHT: 23px" id="ctltxtNBF_DOB" width="186">
										<table>
											<tr>
												<td><SHMA:DATEPOPUP id="txtNBF_DOB" tabIndex="3" runat="server" Width="4.5pc" maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js"
														ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="cfvNBF_DOB" runat="server" Display="Dynamic" ErrorMessage="Date Format is Incorrect "
														Type="Date" Operator="DataTypeCheck" ControlToValidate="txtNBF_DOB"></asp:comparevalidator><asp:comparevalidator id="msgNBF_DOB" runat="server" Display="Dynamic" ErrorMessage="{dd/mm/yyyy} " Type="Date"
														Operator="DataTypeCheck" ControlToValidate="txtNBF_DOB" Enabled="true" CssClass="CalendarFormat"></asp:comparevalidator></td>
												<td>&nbsp;Age&nbsp;<SHMA:TEXTBOX id="txtNBF_AGE" tabIndex="4" runat="server" BaseType="Number" MaxLength="5" Width="3.0pc" onkeydown="backspaceFunc('txtNBF_AGE')"
														Precision="0" SubType="Currency" onblur="setCNICField();"></SHMA:TEXTBOX>

													<asp:comparevalidator id="cfvNBF_AGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
														Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNBF_AGE"></asp:comparevalidator></td>

												<td><input id="btnGuardian" class="button2" onmouseover="Tip('Show Guardian');" onmouseout="UnTip()"
														onclick="openGuardian(getField('NP1_PROPOSAL').value, getField('NBF_BENEFCD').value, getField('NGU_GUARDCD')==null?'':getField('NGU_GUARDCD').value);"
														value="..." type="button" name="btnGuardian" runat="server"></td>
												<td></td>
											</tr>
										</table>
									</TD>
									<TD id="lblddlNBF_BASIS" width="106" align="right">Basis&nbsp;&nbsp;</TD>
									<TD style="HEIGHT: 23px" id="ctlddlNBF_BASIS" width="186"><SHMA:DROPDOWNLIST id="ddlNBF_BASIS" tabIndex="5" runat="server" Width="6.0pc" DataValueField="CSD_TYPE" onkeydown="return cancelBack()"
											DataTextField="desc_f" onchange="setAmountPercent(this);"></SHMA:DROPDOWNLIST>
                                            
                                            <asp:comparevalidator id="cfvNBF_BASIS" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
											Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNBF_BASIS"></asp:comparevalidator>

                                            <SHMA:TEXTBOX id="txtNBF_PERCNTAGE" tabIndex="6" runat="server" BaseType="String" MaxLength="3" onkeydown="backspaceFunc('txtNBF_PERCNTAGE')"
											Width="3.0pc" Precision="0" SubType="Currency"></SHMA:TEXTBOX>
                                            
                                            <asp:comparevalidator id="cfvNBF_PERCNTAGE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
											Type="String" Operator="DataTypeCheck" ControlToValidate="txtNBF_PERCNTAGE"></asp:comparevalidator>
											
                                            <SHMA:TEXTBOX style="DISPLAY: none" id="txtNBF_AMOUNT" tabIndex="6" runat="server" BaseType="Number"
											MaxLength="15" Width="5.0pc" Precision="2" SubType="Currency"></SHMA:TEXTBOX>
											
                                            <SHMA:COMPAREVALIDATOR id="cfvNBF_AMOUNT" runat="server" BaseType="Number" Operator="DataTypeCheck" ControlToValidate="txtNBF_AMOUNT"
											Precision="2" SubType="Currency"></SHMA:COMPAREVALIDATOR></TD>
								</TR>
								<TR id="rowNBF_IDNO" class="TRow_Normal">
									<TD id="lbltxtNBF_IDNO" width="116" align="right">C.N.I.C./Form B&nbsp;&nbsp;</TD>
									<TD id="ctltxtNBF_IDNO" width="186"><shma:textbox onblur="validateNIC(this);formatNIC(this)" id="txtNBF_IDNO" onfocus="deFormatNIC(this)" onkeydown="backspaceFunc('txtNBF_IDNO')"
											tabIndex="6" onkeyup="formatNIC(this)" runat="server" BaseType="Character" MaxLength="15" Width="13.0pc" CssClass="RequiredField"></shma:textbox><asp:comparevalidator id="cfvNBF_IDNO" runat="server" Display="Dynamic" ErrorMessage="NIC Format is Incorrect"
											Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtNBF_IDNO" Enabled="False"></asp:comparevalidator></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR id="rowNBF_BENNAMEARABIC" class="TRow_Normal">
									<TD style="DISPLAY: none" id="lbltxtNBF_BENNAMEARABIC" width="116" align="right">Name 
										in Arabic&nbsp;&nbsp;</TD>
									<TD style="DISPLAY: none" id="ctltxtNBF_BENNAMEARABIC" width="186"><shma:textbox id="txtNBF_BENNAMEARABIC" tabIndex="7" runat="server" BaseType="Character" MaxLength="50"
											Width="184px"></shma:textbox><asp:comparevalidator id="cfvNBF_BENNAMEARABIC" runat="server" Display="Dynamic" EnableClientScript="False"
											ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNBF_BENNAMEARABIC"></asp:comparevalidator></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<tr>
									<td height="10" colSpan="4"></td>
								</tr>
								<TR>
									<td>
										<P><asp:label id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red" Visible="False"></asp:label></P>
									</td>
									<TD></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td class="button2TD" align="right"><A class="button2" onclick="addClicked()" href="#">Add 
							New</A> <A class="button2" onclick="SaveBeneficiary()" href="#">Save</A> 
						<!-- <A class="button2" onclick="saveClicked()" href="#">Save</A> 
					<A class="button2" onclick="updateClicked()" href="#">Update</A>  --><A class="button2" onclick="deleteClicked()" href="#">Delete</A>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td align="right"></td>
				</tr>
				<tr>
					<td>
						<DIV id="ListerDiv" runat="server">
							<TABLE class="Lister" border="0" cellSpacing="2" cellPadding="0">
								<TR class="GridHeader">
									<TD onclick="filterLister('NBF_BENNAME','Name')">Name</TD>
									<TD onclick="filterLister('B.CRL_DESCR','')">Relation</TD>
									<TD onclick="filterLister('NBF_AGE','')">Age</TD>
									<TD id="tdBasis" onclick="filterLister('BASIS','')">Percentage</TD>
									<TD style="display:none" onclick="filterLister('NGU_GUARDCD','NGU_GUARDCD')">NGU_GUARDCD</TD>
									<TD style="display:none" onclick="filterLister('NBF_BENEFCD','NBF_BENEFCD')">NBF_BENEFCD</TD>
									<TD style="display:none" onclick="filterLister('NP1_PROPOSAL','NP1_PROPOSAL')">NP1_PROPOSAL</TD>
								</TR>
								<asp:repeater id="lister" runat="server">
									<ItemTemplate>
										<tr class="ListerItem" id="ListerRow" runat="server" onmouseover="showGaurdianInToolTip(this);"
											onmouseout="UnTip()">
											<td><a href="#">
													<%# DataBinder.Eval(Container, "DataItem.NBF_BENNAME")%>
												</a>
											</td>
											<td><%# DataBinder.Eval(Container, "DataItem.CRL_DESCR")%></td>
											<td><%# DataBinder.Eval(Container, "DataItem.AGE")%>
												<input style="display:none" type=hidden name="BenAge" value='<%# DataBinder.Eval(Container, "DataItem.AGE")%>'></td>
											<td><%# DataBinder.Eval(Container, "DataItem.BASIS")%></td>
											<td style="display:none"><input style="display:none" type=hidden name="NGU_GUARDCD" value='<%# DataBinder.Eval(Container, "DataItem.NGU_GUARDCD")%>'></td>
											<td style="display:none"><input style="display:none" type=hidden name="BenCode" value='<%# DataBinder.Eval(Container, "DataItem.NBF_BENEFCD")%>'>
												<asp:linkbutton ID="linkNBF_BENEFCD1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NBF_BENEFCD")%>' CausesValidation="false">
												</asp:linkbutton>
											</td>
											<td style="display:none">
												<asp:Label ID="lblNP1_PROPOSAL1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>'>
												</asp:Label></td>
										</tr>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tr class="ListerAlterItem" id="ListerRow" runat="server" onmouseover="showGaurdianInToolTip(this);"
											onmouseout="UnTip()">
											<td><a href="#"><%# DataBinder.Eval(Container, "DataItem.NBF_BENNAME")%></a></td>
											<td><%# DataBinder.Eval(Container, "DataItem.CRL_DESCR")%></td>
											<td><%# DataBinder.Eval(Container, "DataItem.AGE")%>
												<input style="display:none" type=hidden name="BenAge" value='<%# DataBinder.Eval(Container, "DataItem.AGE")%>'></td>
											<td><%# DataBinder.Eval(Container, "DataItem.BASIS")%></td>
											<td style="display:none"><input style="display:none" type=hidden name="NGU_GUARDCD" value='<%# DataBinder.Eval(Container, "DataItem.NGU_GUARDCD")%>'></td>
											<td style="display:none"><input style="display:none" type=hidden name="BenCode" value='<%# DataBinder.Eval(Container, "DataItem.NBF_BENEFCD")%>'>
												<asp:linkbutton ID="linkNBF_BENEFCD2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NBF_BENEFCD")%>' CausesValidation="false">
												</asp:linkbutton>
											</td>
											<td style="display:none">
												<asp:Label ID="lblNP1_PROPOSAL2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>'>
												</asp:Label></td>
										</tr>
									</AlternatingItemTemplate>
								</asp:repeater></TABLE>
							<asp:dropdownlist style="DISPLAY: none" id="pagerList" runat="server" CssClass="Pager" AutoPostBack="True" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist></DIV>
					</td>
				</tr>
			</table>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px; display:none;" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<script language="javascript" type="text/javascript">
		
			//************ Before Applying DML - Start ******************//
			function beforeSave()
			{
				return validateInput();
			}
			
			function beforeUpdate()
			{
				return validateInput();
			}
			
			function SaveBeneficiary()
			{
				if (_lastEvent == 'New')
				{
					saveClicked();
				}
				else
				{
					updateClicked();
				}
			}

			function validateInput()
			{
				if(trim(getField("NBF_BENNAME").value).length < 1)
				{
					alert("Name is missing");
					getField("NBF_BENNAME").value = "";
					getField("NBF_BENNAME").focus();
					return false;
				}
				
				if(getField("CRL_RELEATIOCD").value == "")
				{
					alert("Relation is missing");
					getField("CRL_RELEATIOCD").focus();
					return false;
				}
				
				if(individualRelation == true)
				{
					if(trim(getField("NBF_DOB").value).length < 1)
					{
						alert("Date of Birth is missing");
						getField("NBF_DOB").focus();
						//return false;
					}
				}
				
				if(getField("NBF_BASIS").value == "01")
				{
					if(trim(getField("NBF_AMOUNT").value).length < 1)
					{
						alert("Amount is missing");
						getField("NBF_AMOUNT").focus();
						return false;
					}
				}
				else if(getField("NBF_BASIS").value == "02")
				{
					if(trim(getField("NBF_PERCNTAGE").value).length < 1)
					{
						alert("Percentage is missing");
						getField("NBF_PERCNTAGE").focus();
						return false;
					}
				}
				
				getField("NBF_BASIS").disabled=false;
				getField("NBF_DOB").disabled = false;
				getField("NBF_AGE").disabled = false;
				return true;
			}
			
			function isNumeric(elem){
			var numericExpression = /^[0-9]+$/;
			if(elem.match(numericExpression)){
				return true;
			}else{				
				return false;
			}
		}

		function deFormatNIC(obj){
			obj.value = obj.value.replace('-','').replace('-','');			
		}
		function validateNIC(obj){
			if(obj.value!=''){
				var nic = obj.value.replace('-','').replace('-','');				
				if(!isNumeric(nic)){
					alert('CNIC must be in Number Format.');
					return false;
				}
				else if(nic.length != 13){
					alert('CNIC must be equal to 13 character');
					return false;
				}
				return true;
			}			
		}
		function formatNIC(obj){	
			if(obj.value!=''){
				var nic = obj.value.replace('-','').replace('-','');
				if(nic.length == 13 && isNumeric(nic)){	
					var i=0;
					var msg = '';
					for(i=0;i<5;i++){
						msg = msg + nic.charAt(i);
					}
					msg=msg+"-";
					for(i=5;i<12;i++){
						msg = msg + nic.charAt(i);
					}
					msg=msg+"-"+nic.charAt(12);
					obj.value = msg;
					return true;
				}
			}			
		}
			
			function getRelationType(objRelation)
			{
				var className  = "ace.clsIlasUtility";
				var methodName = "isIndividualRelation";
				var parameters = objRelation.value;
				var strResult  = executeClass(className + "," + methodName + "," + parameters); 
			
				if(strResult == "false")
				{
					individualRelation = false;
					getField("NBF_DOB").value ="";
					getField("NBF_AGE").value ="";
					getField("NBF_DOB").disabled = true;
					getField("NBF_AGE").disabled = true;
				}
				else
				{
					individualRelation = true;
					getField("NBF_DOB").disabled = false;
					getField("NBF_AGE").disabled = false;
				}

			}

			//************ Before Applying DML - End ******************//
			
	
			
			function setDOB(obj, commncedate)
			{
				obj.value = trim(obj.value);
				var dateofBirth      = getDateObject(obj.value);
				var commencementDate = getDateObject(commncedate);
				
				if(commencementDate < dateofBirth)
				{
					alert("Date of Birht can't be greater than Commencement Date");
					return;
				}
				
				var DateValue = dateDiffYears(obj.value, commncedate, parent.parent.ageRoundCriteria);
				
				if(isNaN(DateValue))
				{
					document.getElementById("txtNBF_AGE").value="0";
				}
				else
				{
					if(parseInt(DateValue) < 0)
					{
						alert("Invalid Date");
						obj.focus();
					}
					document.getElementById("txtNBF_AGE").value = DateValue;
				}
			}

			function setAmountPercent(objBasis)
			{
				
				var objPercnt = getField("NBF_PERCNTAGE");
				var objAmount = getField("NBF_AMOUNT");
				
				if(objBasis.value == ByPercent)
				{
					objAmount.style.display = "none";
					objAmount.value = "0.00";
					
					objPercnt.style.display = "";
					objPercnt.focus();
					
					//Set the Heading of Tabular Lister to Percentage
					document.getElementById("tdBasis").innerHTML = "Percentage";
				}
				else if(objBasis.value == ByAmount)
				{
					objPercnt.style.display = "none";
					objPercnt.value = "0";
					
					objAmount.style.display = "";
					objAmount.focus();
					
					//Set the Heading of Tabular Lister to Amount
					document.getElementById("tdBasis").innerHTML = "Amount";
				}				
				else
				{
					alert("Invalid value");
					objBasis.focus();
				}
			}
			
			function valbenfstr(obj)
			{
			//alert('asd');
			var regex = /^[a-zA-Z\s]*$/;
			if (regex.test(obj.value)) {
				return true;
			} else {
				alert('Alphabets Only, No Characters');
				obj.value='';
				return false;
			}
			}
			
			function validatePecentage(objPercentage) {

			    //alert("value must be greater than 0 Deen Muhammad");
			
				if(parseInt(objPercentage.value) <= 0)
				{
					alert("value must be greater than 0");
				}

				if(parseInt(objPercentage.value) > 100)
				{
					alert("Maximum value is 100");
					objPercentage.value = 100;
				}
				
				else if(parseInt(objPercentage.value) < 0)
				{
					alert("Invalid value");
					objPercentage.value = 0;
				}
			}
			
			function setBasisHeading()
			{
				var basis = fetchDataArray("SELECT NBF_BASIS  FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + getField("NP1_PROPOSAL").value + "'");
				
				document.getElementById("tdBasis").innerHTML = "Percentage";
				getField("NBF_BASIS").value = ByPercent;
				getField("NBF_PERCNTAGE").style.display = "";
				getField("NBF_AMOUNT").style.display = "none";
				
				if(basis != "NBF_BASIS" )
				{
					if (basis != null && basis.length>0 && basis[1][0]!=null && basis[1][0]!="" )
					{
						if(basis[1][0] == ByAmount)
						{
							document.getElementById("tdBasis").innerHTML = "Amount";
							getField("NBF_BASIS").value = ByAmount;
							getField("NBF_AMOUNT").style.display = "";
							getField("NBF_PERCNTAGE").style.display = "none";

						}
						getField("NBF_BASIS").disabled=true;
					}
				}
			}
			
			function setCNICField()
			{
				var age_ = document.getElementById("txtNBF_AGE").value;
				var idno = document.getElementById("txtNBF_IDNO");
				if(age_	< 18)
				{					
					idno.value = '';
					idno.readOnly = true;
				}
				else
				{
					idno.readOnly = false;
				}
			}
			
			
			setAmountPercent(getField("NBF_BASIS"));
			setBasisHeading();
			setCNICField();
			
			//Show Date format Message on focus - Registration (definition is in GeneralView.js)
			attachViewByNameNormal('txtNBF_DOB');
			
			//Set Focus to first field
			getField("NBF_BENNAME").focus();
			</script>
		</form>
		<script language="javascript" type="text/javascript">
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		
			<asp:literal id="GuardinaLiteral" runat="server" EnableViewState="True"></asp:literal>
			if (_lastEvent == 'New') {
                addClicked();
            }
			 	
			//fcStandardFooterFunctionsCall();
		</script>
	</body>
</HTML>
