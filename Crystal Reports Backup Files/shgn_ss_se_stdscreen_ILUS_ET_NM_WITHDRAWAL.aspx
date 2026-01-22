<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_WITHDRAWAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_WITHDRAWAL" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
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
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralFunctions.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
			if(myForm.txtNPW_YEAR.disabled==true)
				 myForm.txtNPW_YEAR.disabled=false;
				 myForm.txtNPW_YEAR.value ="1";
							 myForm.txtNPW_PW.value ="";
							 myForm.ddlNPW_PURPOSE.selectedIndex =0;
							 myForm.ddlNPW_REQUIREDFOR.selectedIndex =0;
							 myForm.ddlNPW_REQIREDFORCD.selectedIndex =0;
							 myForm.txtNPW_ATTAINAGE.value ="0";
							 myForm.txtNPW_ALLOWAMOUNT.value ="";
							 myForm.txtNPW_CUMWITHDRAWAL.value ="0";
							 myForm.txtNPW_ADHOCEXCESPRM.value ="";
							 myForm.txtNPW_ADHOCEPPW.value ="0";
							 myForm.ddlNPW_DEATHBENEFITOPTION.selectedIndex =0;
			
myForm.txtNPW_YEAR.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		//var str_QryNPW_REQIREDFORCD="SELECT NBF_BENEFCD "+getConcateOperator()+" '-' "+getConcateOperator()+" NBF_BENNAME,NBF_BENEFCD  FROM LNBF_BENEFICIARY  where NP1_PROPOSAL='R/07/0010042'  AND NPW_REQUIREDFOR='~NPW_REQUIREDFOR~'   ORDER BY NBF_BENNAME";
		//REAL/var str_QryNPW_REQIREDFORCD="SELECT NBF_BENEFCD "+getConcateOperator()+" '-' "+getConcateOperator()+" NBF_BENNAME,NBF_BENEFCD  FROM VW_REQUIREDFOR  where NP1_PROPOSAL='R/07/0010042'  AND NPW_REQUIREDFOR='~NPW_REQUIREDFOR~'   ORDER BY NBF_BENNAME";
		//var str_QryNPW_REQIREDFORCD="SELECT NBF_BENEFCD "+getConcateOperator()+" '-' "+getConcateOperator()+" NBF_BENNAME,NBF_BENEFCD  FROM LNBF_BENEFICIARY  where NP1_PROPOSAL='R/07/0010042'  AND NPW_REQUIREDFOR='~NPW_REQUIREDFOR~'   ORDER BY NBF_BENNAME";


		var subView = "select NBF_BENEFCD, NBF_BENNAME, 'B' npw_requiredfor, np1_proposal from lnbf_beneficiary "
						+ " UNION "
						+ " SELECT ph.nph_code, ph.nph_fullname, 'I' code,  u1.np1_proposal "
						+ " FROM LNPH_PHOLDER ph, lnu1_underwriti u1 "
						+ " WHERE "
						+ " ph.nph_code = u1.nph_code "
						+ " and ph.nph_life = u1.nph_life ";

		var str_QryNPW_REQIREDFORCD="SELECT NBF_BENEFCD "+getConcateOperator()+" '-' "+getConcateOperator()+" NBF_BENNAME,NBF_BENEFCD  FROM (" + subView + ") VW_REQUIREDFOR  where NP1_PROPOSAL=SV(\"NP1_PROPOSAL\")  AND NPW_REQUIREDFOR='~NPW_REQUIREDFOR~'   ORDER BY NBF_BENNAME";
		

		</script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Table of Partial Withdrawals" id="EntityHeading"
			runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="EntryTableDiv" id="EntryTableDiv" runat="server" style="Z-INDEX: 100; LEFT: 345px; WIDTH: 390px; HEIGHT: 300px">
				<!--<fieldset id="EntryFldSet" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 300px; BORDER-BOTTOM: #e0f3be 1px solid; HEIGHT: 370px"><legend><Entry></legend>-->
				<TABLE id="entryTable" cellSpacing="0" cellPadding="2" border="0">
					<TR id='rowNPW_YEAR' class="TRow_Normal">
						<TD align="right" width="47%">Withdrawal Year</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_YEAR" tabIndex="1" runat="server" Width='184px' MaxLength="2" CssClass="RequiredField"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_YEAR" runat="server" ControlToValidate="txtNPW_YEAR" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_YEAR" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPW_YEAR"
								Precision="0"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_PW' class="TRow_Alt">
						<TD align="right" width="47%">Amount to be Withdrawn</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_PW" tabIndex="2" runat="server" Width='184px' MaxLength="17" CssClass="RequiredField"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_PW" runat="server" ControlToValidate="txtNPW_PW" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_PW" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNPW_PW"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_PURPOSE' class="TRow_Normal">
						<TD align="right" width="47%">Purpose</TD>
						<TD width="51%"><SHMA:dropdownlist id="ddlNPW_PURPOSE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="csd_type"
								tabIndex="3" Width="184px" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNPW_PURPOSE" runat="server" ControlToValidate="ddlNPW_PURPOSE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_PURPOSE" runat="server" ErrorMessage="Required" ControlToValidate="ddlNPW_PURPOSE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_REQUIREDFOR' class="TRow_Alt">
						<TD align="right" width="47%">Required For</TD>
						<TD width="51%"><SHMA:dropdownlist id="ddlNPW_REQUIREDFOR" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="csd_type" tabIndex="4" Width="184px" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNPW_REQUIREDFOR" runat="server" ControlToValidate="ddlNPW_REQUIREDFOR" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_REQUIREDFOR" runat="server" ErrorMessage="Required" ControlToValidate="ddlNPW_REQUIREDFOR"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_REQIREDFORCD' class="TRow_Normal">
						<TD align="right" width="47%">Name</TD>
						<TD width="51%"><SHMA:dropdownlist id="ddlNPW_REQIREDFORCD" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="NBF_BENEFCD" tabIndex="5" Width="184px" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNPW_REQIREDFORCD" runat="server" ControlToValidate="ddlNPW_REQIREDFORCD"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_REQIREDFORCD" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="ddlNPW_REQIREDFORCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_ATTAINAGE' class="TRow_Alt">
						<TD align="right" width="47%">Attained Age</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_ATTAINAGE" tabIndex="6" runat="server" Width='184px' ReadOnly="true"
								MaxLength="10" BaseType="Number" Precision="0" CssClass="DisplayOnly"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_ATTAINAGE" runat="server" ControlToValidate="txtNPW_ATTAINAGE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowNPW_ALLOWAMOUNT' class="TRow_Normal">
						<TD align="right" width="47%">Allowed Amount</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_ALLOWAMOUNT" tabIndex="7" runat="server" Width='184px' MaxLength="17"
								CssClass="RequiredField" BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_ALLOWAMOUNT" runat="server" ControlToValidate="txtNPW_ALLOWAMOUNT" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_ALLOWAMOUNT" runat="server" Precision="0" ErrorMessage="Required" ControlToValidate="txtNPW_ALLOWAMOUNT"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR id='rowNPW_CUMWITHDRAWAL' class="TRow_Alt">
						<TD align="right" width="47%">Cummulative Withdrawal</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_CUMWITHDRAWAL" tabIndex="8" runat="server" Width='184px' ReadOnly="true"
								MaxLength="17" BaseType="Number" Precision="0" CssClass="DisplayOnly"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_CUMWITHDRAWAL" runat="server" ControlToValidate="txtNPW_CUMWITHDRAWAL"
								Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowNPW_ADHOCEXCESPRM' class="TRow_Normal">
						<TD width="47%" align="right">Adhoc Excess Premium</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_ADHOCEXCESPRM" tabIndex="9" runat="server" Width='184px' MaxLength="17"
								BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_ADHOCEXCESPRM" runat="server" ControlToValidate="txtNPW_ADHOCEXCESPRM"
								Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowNPW_ADHOCEPPW' class="TRow_Alt">
						<TD width="47%" align="right">Adhoc EP Less Partial Withdrawal</TD>
						<TD width="51%"><shma:TextBox id="txtNPW_ADHOCEPPW" tabIndex="10" runat="server" Width='184px' ReadOnly="true"
								CssClass="DisplayOnly" MaxLength="17" BaseType="Number" Precision="0"></shma:TextBox>
							<asp:CompareValidator id="cfvNPW_ADHOCEPPW" runat="server" ControlToValidate="txtNPW_ADHOCEPPW" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
						</TD>
					</TR>
					<TR id='rowNPW_DEATHBENEFITOPTION' class="TRow_Normal">
						<TD width="47%" align="right">Death Benefit Option Switching</TD>
						<TD width="51%"><SHMA:dropdownlist id="ddlNPW_DEATHBENEFITOPTION" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="csd_type" tabIndex="11" Width="184px" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNPW_DEATHBENEFITOPTION" runat="server" ControlToValidate="ddlNPW_DEATHBENEFITOPTION"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNPW_DEATHBENEFITOPTION" runat="server" ErrorMessage="Required" ControlToValidate="ddlNPW_DEATHBENEFITOPTION"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</TR>
					<TR>
						<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD width="51%"></TD>
					</TR>
				</TABLE>
				<!--</fieldset>-->
			</div>
			<DIV class="ListerDiv" id="ListerDiv" runat="server" style="Z-INDEX: 101; LEFT: 16px; WIDTH: 380px; TOP: 5px; HEIGHT: 300px">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet" style="WIDTH: 316px; HEIGHT: 270px"><legend><!--List-->
						Table of Partial Withdrawals</legend>
					<TABLE cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD width="10%" onClick="filterLister('NPW_YEAR','Year')">Year</TD>
							<TD width="30%" onClick="filterLister('NPW_CUMWITHDRAWAL','Withdrawal')">Partial</TD>
							<TD width="30%" onClick="filterLister('NPW_CUMWITHDRAWAL','Cummulative')">Cummulative</TD>
							<TD width="50%" onClick="filterLister('BF.NBF_BENNAME','Name')">Name</TD>
							<TD width="0"></TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNPW_YEAR1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' CausesValidation="false" ForeColor="Black">
										</asp:linkbutton></td>
									<td><%# String.Format("{0:N}",DataBinder.Eval(Container, "DataItem.NPW_PW"))%></td>
									<td><%# String.Format("{0:N}",DataBinder.Eval(Container, "DataItem.NPW_CUMWITHDRAWAL"))%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NBF_BENNAME")%></td>
									<td>
										<asp:Label Visible=false ID="lblNP1_PROPOSAL1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>'>
										</asp:Label></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNPW_YEAR2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' CausesValidation="false" ForeColor="Black">
										</asp:linkbutton></td>
									<td><%# String.Format("{0:N}",DataBinder.Eval(Container, "DataItem.NPW_PW"))%></td>
									<td><%# String.Format("{0:N}",DataBinder.Eval(Container, "DataItem.NPW_CUMWITHDRAWAL"))%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NBF_BENNAME")%></td>
									<td>
										<asp:Label Visible=false ID="lblNP1_PROPOSAL2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>'>
										</asp:Label></td>
								</tr>
							</AlternatingItemTemplate>
						</asp:repeater></TABLE>
				</FIELDSET>
				<br>
				<font size="1" face="arial">Page no:</font>
				<asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="Pager" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
			</DIV>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 592px; POSITION: absolute; TOP: 552px" runat="server"
				Width="80px" Visible="False" Height="24px">Label</asp:Label>
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
			<shma:TextBox id="txtNP1_PROPOSAL" runat="server" BaseType="Character" width="0"></shma:TextBox>
			<shma:TextBox id="txtNBF_DOB" runat="server" BaseType="Character" width="0"></shma:TextBox>
			<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" Operator="DataTypeCheck"
				Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
			<shma:TextBox id="txtNBF_BENNAME" runat="server" BaseType="Character" width="0"></shma:TextBox>
			<asp:CompareValidator id="cfvNBF_BENNAME" runat="server" ControlToValidate="txtNBF_BENNAME" Operator="DataTypeCheck"
				Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
			<script language="javascript">
			if(getField("NPW_REQUIREDFOR").options.length>1)

			getField("NPW_REQUIREDFOR").onchange();
			
			function setName(obj_Ref)
			{ 
				//fcfilterChildCombo(obj_Ref,str_QryPLC_LOCACODE+" order by PLC_LOCACODE ASC","ddlNPW_REQUIREDFOR");

				fcfilterChildCombo(obj_Ref,str_QryNPW_REQIREDFORCD,"ddlNPW_REQIREDFORCD");

			}
			
			
			function deleted()
			{
				if (deleteClicked()==true)
					parent.openWait('deleting data');
				
			}

			function saveUpdateClicked()
			{
				if (this.Page_ClientValidate()==true)
				{

					parent.openWait('saving data');
				
					if(_lastEvent=='New')
						saveClicked();
					else
						updateClicked();
				}

			}
	
			function fetchDOB(commenceDate)
			{
			
				//str_QryFetcher =" select Code, DOB, Type, Proposal from (";
				str_QryFetcher =" select DOB NBF_DOB from (";
				str_QryFetcher +=" select NBF_BENEFCD Code, TO_CHAR(NBF_DOB,'DD/MM/YYYY') DOB, 'B' Type, NP1_PROPOSAL Proposal from lnbf_beneficiary "; 

					str_QryFetcher +=	" Union ";

					str_QryFetcher +=	" SELECT ph.nph_code Code, TO_CHAR(NPH_BIRTHDATE,'DD/MM/YYYY') DOB, 'I' Type,  u1.np1_proposal Proposal "
						+ " FROM LNPH_PHOLDER ph, lnu1_underwriti u1 "
						+ " WHERE "
						+ " ph.nph_code = u1.nph_code "
						+ " and ph.nph_life = u1.nph_life) BenfIns ";
					
					str_QryFetcher +=	" where Code="+document.getElementById("ddlNPW_REQIREDFORCD").value
										+" and Type ='"+document.getElementById("ddlNPW_REQUIREDFOR").value 
										+"' and Proposal='"+parent.frames[0].document.getElementById("txtNP1_PROPOSAL").value+"'";


				//alert(str_QryFetcher);
				setFetchDataQry(str_QryFetcher);
				fetchData();
				
				setDOB(document.getElementById('txtNBF_DOB').value, commenceDate);
				
				
			}

			function setDOB(dob, commncedate)
			{
				//alert(dob + ', ' + commncedate);
				var value = dateDiffYears(dob, commncedate, parent.parent.ageRoundCriteria);
				if(isNaN(value))
					document.getElementById('txtNPW_ATTAINAGE').value = '';
				else
					document.getElementById('txtNPW_ATTAINAGE').value = dateDiffYears(dob, commncedate, parent.parent.ageRoundCriteria);

			}
		
		
			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		
		parent.closeWait();
		
		
		setCurrencySingle("txtNPW_PW");
		setCurrencySingle("txtNPW_ALLOWAMOUNT");
		setCurrencySingle("txtNPW_ADHOCEXCESPRM");
		
		//setFormatSingle("txtNPW_ATTAINAGE",2);
		setFormatSingle("txtNPW_CUMWITHDRAWAL",2);
		setFormatSingle("txtNPW_ADHOCEPPW",2);
		
		
		</script>
	</body>
</HTML>
