<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
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
		<!--<SCRIPT language="JavaScript" src='../shmalib/jscript/MI_ET_NM_PolicyEntry'></SCRIPT>-->
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/PersonalDetail.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

	

		function RefreshFields()
		{				
			myForm.ddlNPH_TITLE.selectedIndex =0;
			myForm.ddlNPH_SEX.selectedIndex =0;
			myForm.txtNPH_FULLNAME.value ="";
			myForm.txtNPH_FULLNAMEARABIC.value ="";
			myForm.txtNPH_BIRTHDATE.value ="";
			myForm.ddlCOP_OCCUPATICD.selectedIndex =0;
			myForm.ddlCCL_CATEGORYCD.selectedIndex =0;
			//myForm.ddlNPH_INSUREDTYPE.selectedIndex =0;
			myForm.ddlNPH_IDTYPE.selectedIndex =0;
			myForm.txtCNIC_VALUE.value ="";
			myForm.txtNU1_ACCOUNTNO.value ="";	
			myForm.ddlNPH_HEIGHTTYPE.selectedIndex = 2;
			myForm.txtNU1_ACTUALHEIGHT.value ="";
			myForm.txtNU1_CONVERTHEIGHT.value ="";
			myForm.txt_bmi.value="";
			
	
			myForm.ddlNPH_WEIGHTTTYPE.selectedIndex =1;
			myForm.txtNU1_ACTUALWEIGHT.value ="";
			myForm.txtNU1_CONVERTWEIGHT.value ="0.00";
							 
			setDefaultValues();
					
		}

		function CalculateEntryAge(objDate)
		{
			var years = dateDiffYears(objDate.value, sysDate, parent.parent.ageRoundCriteria)
			if(isNaN(years))
			{
				getField("NP2_AGEPREM").value="";
				return false;
			}
			else
			{
				if(years < 0)
				{
					getField("NP2_AGEPREM").value="0";
					alert("Invalid Date of Birth");
					getField("NPH_BIRTHDATE").focus();
					return false;
				}
				else
				{
					getField("NP2_AGEPREM").value=years;
					return true;
				}
			}
		}

		/********** dependent combo's queries **********/
		var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";		


function formatNumber(myNum, numOfDec) 
    { 
      var decimal = 1 
      for(i=1; i<=numOfDec;i++) 
      decimal = decimal *10 

      var myFormattedNum = (Math.round(myNum * decimal)/decimal).toFixed(numOfDec) 
      return(myFormattedNum) 
} 
//Convert To Feets

function getIt(m)
{


//Number Checking..

var b=IsNumeric(m.value);
if(b==false)
{
alert('Please provide correct NIC number');

}
else
{
n=m.value;
var str;
var msg = "";
var i=0;

for(i=0;i<5;i++)
{
msg = msg + n.charAt(i); ;
//alert(n.charAt(i));
}
msg=msg+"-";

///Next Portion

for(i=5;i<12;i++)
{
msg = msg + n.charAt(i); ;
}
msg=msg+"-"+n.charAt(12);

myForm.txtCNIC_VALUE.value=msg;
    
}
}


function IsNumeric(sText)
{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;

 
   for (i = 0; i<sText.length && IsNumber == true; i++)
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         }
      }
        if(sText.length>13)
        {
         IsNumber = false;
        
        }
   return IsNumber;
   
   }

		</script>
	</HEAD>
	<body>
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Policy Owner Personal Details" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div style="Z-INDEX: 0" id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server">
				<TABLE id="entryTable" border="0" cellSpacing="0" cellPadding="2">
					<tr class="form_heading">
						<td height="20" colSpan="4">&nbsp; Personal Details of Life Insured
						</td>
					</tr>
					<tr>
						<td height="10" colSpan="4"></td>
					</tr>
					<TR id="rowNPH_TITLE" class="TRow_Normal">
						<TD id="lblddlNPH_TITLE" class="<%=getHighLighted()%>" width=110 align=right>Title</TD>
						<TD style="WIDTH: 213px" width="213"><SHMA:DROPDOWNLIST id="ddlNPH_TITLE" tabIndex="1" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="CSD_TYPE" Onchange="Title_ChangeEvent(this);" Width="184px" CssClass="RequiredField" HighLighted="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNPH_TITLE" runat="server" ControlToValidate="ddlNPH_TITLE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPH_TITLE" runat="server" ControlToValidate="ddlNPH_TITLE" ErrorMessage="Required"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD id="lbltxtCNIC_VALUE" width="110" align="right">
							<SHMA:DROPDOWNLIST id="ddlNPH_IDTYPE" tabIndex="3" style="BORDER-BOTTOM:#444 0px solid; BORDER-LEFT:#444 0px solid; BORDER-TOP:#444 0px solid; BORDER-RIGHT:#444 0px solid"
								runat="server" CssClass="RequiredField" Width="100px" DataValueField="NPH_IDTYPE" DataTextField="desc_f"
								BlankValue="false"></SHMA:DROPDOWNLIST>
						</TD>
						<TD id="ctltxtCNIC_VALUE" width="186" style="HEIGHT: 23px">
							<shma:textbox onblur="NIC_Blur(this)" id="txtCNIC_VALUE" onfocus="NIC_Focus(this)" tabIndex="3"
								onkeypress="return NIC_KeyPress(event,this)" onkeyup="NIC_KeyUp(event,this)" runat="server"
								CssClass="RequiredField" Width="184px" DESIGNTIMEDRAGDROP="79" MaxLength="15"></shma:textbox>
						</TD>
						<TD id="lbltxtNPH_IDNO2" width="110" style="DISPLAY:none" align="right">N.I.C(OLD)</TD>
						<TD id="ctltxtNPH_IDNO2" width="186" style="DISPLAY:none">
							<shma:textbox readonly="true" id="txtNPH_IDNO2" tabIndex="3" style="DISPLAY:none" runat="server"
								CssClass="RequiredField" Width="184px" DESIGNTIMEDRAGDROP="79" MaxLength="15"></shma:textbox>
						</TD>
					</TR>
					<tr class="TRow_Alt" class="TRow_Alt">
                    <td width="106" align="right" id="TD5">
                        CNIC Issue Date
                    </td>
                    <td width="186" id="ctltxtNPH_DOCISSUEDAT">
                        <SHMA:DatePopUp ID="txtNPH_DOCISSUEDAT" TabIndex="3" runat="server" CssClass="RequiredField"
                            onkeydown="backspaceFunc('txtNPH_DOCISSUEDAT')" Width="110px" ImageUrl="Images/image1.jpg"
                            ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                        </SHMA:DatePopUp>
                    </td>
                    <td width="106" align="right" id="TD7">
                        CNIC Expiry Date
                    </td>
                    <td width="186" id="ctltxtNPH_DOCEXPIRDAT">
                        <SHMA:DatePopUp ID="txtNPH_DOCEXPIRDAT" TabIndex="3" runat="server" CssClass="RequiredField"
                            onkeydown="backspaceFunc('txtNPH_DOCEXPIRDAT')" Width="110px" ImageUrl="Images/image1.jpg"
                            ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                        </SHMA:DatePopUp>
                    </td>
                </tr>
					<TR id="rowNPH_FULLNAME" class="TRow_Alt">
						<TD id="lbltxtNPH_FULLNAME" class="<%=getHighLighted()%>" width=110 align=right>Name 
							in English</TD>
						<TD id="ctltxtNPH_FULLNAME" style="WIDTH: 213px" width="213"><shma:textbox id="txtNPH_FULLNAME" tabIndex="4" runat="server" Width="183px" CssClass="RequiredField"
								HighLighted="True" MaxLength="50" BaseType="Character"></shma:textbox>&nbsp;<INPUT class="BUTTON" title="Open Proposal List Of Values" tabIndex="5" onclick="openPersonsLOV();"
								style="DISPLAY:none" value=".." type="button" name="ProposalLov">
							<asp:comparevalidator id="cfvNPH_FULLNAME" runat="server" ControlToValidate="txtNPH_FULLNAME" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPH_FULLNAME" runat="server" ControlToValidate="txtNPH_FULLNAME" ErrorMessage="Required"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD id="lbltxtNPH_FULLNAMEARABIC" class="<%=getHighLighted()%>" width=110 align=right>Name 
							in Arabic</TD>
						<TD id="ctltxtNPH_FULLNAMEARABIC" width="186">
							<shma:textbox id="txtNPH_FULLNAMEARABIC" tabIndex="6" runat="server" HighLighted="True" Width="184px"
								BaseType="Character" MaxLength="50"></shma:textbox>
							<asp:comparevalidator id="cfvNPH_FULLNAMEARABIC" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNPH_FULLNAMEARABIC"></asp:comparevalidator></TD>
					</TR>
				 <tr class="TRow_Alt">
                    <td width="106" align="right" id="TD1">
                        Father/Husband Name
                    </td>
                    <td width="186" id="ctltxtNPH_FATHERNAME">
                        <SHMA:TextBox ID="txtNPH_FATHERNAME" TabIndex="7" runat="server" Width="184px" CssClass="RequiredField"
                            onkeydown="backspaceFunc('txtNPH_FATHERNAME')" MaxLength="30" ReadOnly="false"></SHMA:TextBox>
                        <asp:RequiredFieldValidator ID="rfctxtNPH_FATHERNAME" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="txtNPH_FATHERNAME"></asp:RequiredFieldValidator>
                    </td>
                    <td width="106" align="right" id="TD3">
                        Maiden Name
                    </td>
                    <td width="186" id="ctltxtNPH_MAIDENNAME">
                        <SHMA:TextBox ID="txtNPH_MAIDENNAME" TabIndex="8" runat="server" Width="184px" CssClass="RequiredField"
                            onkeydown="backspaceFunc('txtNPH_MAIDENNAME')" MaxLength="30" ReadOnly="false"></SHMA:TextBox>
                        <asp:RequiredFieldValidator ID="rfctxtNPH_MAIDENNAME" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="txtNPH_MAIDENNAME"></asp:RequiredFieldValidator>
                    </td>
                </tr>
					<TR id="rowNPH_BIRTHDATE" class="TRow_Normal">
						<TD id="lbltxtNPH_BIRTHDATE" class="<%=getHighLighted()%>" width="110" align=right>Date 
							of Birth</TD>
						<TD id="ctltxtNPH_BIRTHDATE" style="WIDTH: 213px" width="213"><SHMA:DATEPOPUP id="txtNPH_BIRTHDATE" tabIndex="7" runat="server" onchange="formatDate(this,'DD/MM/YYYY');CalculateEntryAge(this);"
								Width="90px" CssClass="RequiredField" HighLighted="True" maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg">
								<WeekdayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="WeekdayStyle" BackColor="White"></WeekdayStyle>
								<MonthHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="MonthHeaderStyle" BackColor="Yellow"></MonthHeaderStyle>
								<OffMonthStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"
									BackColor="AntiqueWhite"></OffMonthStyle>
								<GoToTodayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									BackColor="White"></GoToTodayStyle>
								<TodayDayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="TodayDayStyle" BackColor="LightGoldenrodYellow"></TodayDayStyle>
								<DayHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="DayHeaderStyle" BackColor="Orange"></DayHeaderStyle>
								<WeekendStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="WeekendStyle" BackColor="LightGray"></WeekendStyle>
								<SelectedDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="SelectedDateStyle" BackColor="Yellow"></SelectedDateStyle>
								<ClearDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									BackColor="White"></ClearDateStyle>
								<HolidayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="HolidayStyle" BackColor="White"></HolidayStyle>
							</SHMA:DATEPOPUP>
							&nbsp;<font id="lbltxtNP2_AGEPREM">Age</font>&nbsp;<shma:textbox id="txtNP2_AGEPREM" tabIndex="8" CssClass="RequiredField" runat="server" Width="40px"
								MaxLength="2" BaseType="Number" ReadOnly="True"></shma:textbox>
							<asp:comparevalidator id="msgNPH_BIRTHDATE" runat="server" CssClass="CalendarFormat" ControlToValidate="txtNPH_BIRTHDATE"
								Operator="DataTypeCheck" Type="Date" ErrorMessage="{dd/mm/yyyy} " Display="Dynamic" Enabled="true"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNPH_BIRTHDATE" runat="server" ControlToValidate="txtNPH_BIRTHDATE" ErrorMessage="Required"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD id="lblddlNPH_SEX" class="<%=getHighLighted()%>" width=110 align=right>Gender</TD>
						<TD id="ctlddlNPH_SEX" width="186">
							<SHMA:DROPDOWNLIST style="Z-INDEX: 0" id="ddlNPH_SEX" Onchange="Gender_ChangeEvent(this);" tabIndex="9"
								runat="server" HighLighted="True" Width="184px">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="M">Male</asp:ListItem>
								<asp:ListItem Value="F">Female</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvNPH_SEX" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNPH_SEX"></asp:comparevalidator>
							<asp:requiredfieldvalidator style="Z-INDEX: 0" id="rfvNPH_SEX" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPH_SEX"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR id="rowCCL_CATEGORYCD" class="TRow_Alt">
						<TD id="lblddlCOP_OCCUPATICD" class="<%=getHighLighted()%>" width=110 align=right>Occupation</TD>
						<TD id="ctlddlCOP_OCCUPATICD" style="WIDTH: 213px" width="213">
							<SHMA:DROPDOWNLIST style="Z-INDEX: 0" id="ddlCOP_OCCUPATICD" tabIndex="10" runat="server" HighLighted="True"
								CssClass="RequiredField" Width="184px" DataValueField="COP_OCCUPATICD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST>
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvCOP_OCCUPATICD" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCOP_OCCUPATICD"></asp:comparevalidator></TD>
						
                        <TD id="lblddlCCL_CATEGORYCD" class="<%=getHighLighted()%>" width=110 align=right>Occupational&nbsp;Class</TD>
						<TD id="ctlddlCCL_CATEGORYCD" width="186"><SHMA:DROPDOWNLIST id="ddlCCL_CATEGORYCD" tabIndex="11" runat="server" BlankValue="True" DataTextField="desc_f"
								DataValueField="CCL_CATEGORYCD" Width="184px" HighLighted="True" style="Z-INDEX: 0"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCCL_CATEGORYCD" runat="server" ControlToValidate="ddlCCL_CATEGORYCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic" style="Z-INDEX: 0"></asp:comparevalidator></TD>
					</TR>
					
                    <TR id="rowNU1_SMOKER" class="TRow_Normal">
						<TD id="lbltxtNU1_ACCOUNTNO" width="110" align="right" CssClass="RequiredField">Account 
							No</TD>
						<TD id="ctltxtNU1_ACCOUNTNO" style="WIDTH: 212px" width="212">
							<shma:textbox style="Z-INDEX: 0" id="txtNU1_ACCOUNTNO" tabIndex="12" runat="server" CssClass="RequiredField"
								Width="184px" BaseType="Character" MaxLength="12"></shma:textbox></TD>


						<TD id="lblddlNU1_SMOKER" width="110" align="right">Smoker</TD>
						<TD id="ctlddlNU1_SMOKER" width="186">
							<SHMA:DROPDOWNLIST style="Z-INDEX: 0" id="ddlNU1_SMOKER" tabIndex="13" runat="server" CssClass="RequiredField"
								Width="184px">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvNU1_SMOKER" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNU1_SMOKER"></asp:comparevalidator></TD>
						<asp:comparevalidator id="cfvNU1_ACCOUNTNO" runat="server" Display="Dynamic" EnableClientScript="False"
							ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNU1_ACCOUNTNO"></asp:comparevalidator></TD></TR>
					</TR>
					<TR class="TRow_Alt">
						<TD id="lblddlNPH_HEIGHTTYPE" style="HEIGHT: 21px" width="110" align="right">Height</TD>
						<TD id="ctlddlNPH_HEIGHTTYPE" style="HEIGHT: 21px" width="186"><SHMA:DROPDOWNLIST style="Z-INDEX: 0" id="ddlNPH_HEIGHTTYPE" tabIndex="14" runat="server" Width="65px"
								Onchange="convert_to_feet()">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="I">Inches</asp:ListItem>
								<asp:ListItem Value="F">Feet</asp:ListItem>
								<asp:ListItem Value="C">Centimeter</asp:ListItem>
								<asp:ListItem Value="M">Meters</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<shma:textbox style="Z-INDEX: 0" id="txtNU1_ACTUALHEIGHT" tabIndex="15" runat="server" CssClass="RequiredField"
								Width="48px" BaseType="Character" MaxLength="4" onchange="convert_to_feet()"></shma:textbox>
							<shma:textbox style="Z-INDEX: 0" id="txtNU1_CONVERTHEIGHT" tabIndex="16" runat="server" CssClass="RequiredField"
								Width="48px" BaseType="Character" MaxLength="12" ReadOnly="True"></shma:textbox>m</TD>
						<TD id="lblddlNPH_WEIGHTTTYPE" width="106" align="right">Weight</TD>
						<TD id="ctlddlNPH_WEIGHTTTYPE" width="186"><SHMA:DROPDOWNLIST style="Z-INDEX: 0" id="ddlNPH_WEIGHTTTYPE" tabIndex="17" runat="server" Width="65px"
								CssClass="requiredfield" Onchange="Weight_Conversion()">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="K">Kilogram</asp:ListItem>
								<asp:ListItem Value="L">LBs</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<shma:textbox style="Z-INDEX: 0" id="txtNU1_ACTUALWEIGHT" tabIndex="18" runat="server" CssClass="RequiredField"
								Width="48px" MaxLength="3" BaseType="Character" onchange="Weight_Conversion()"></shma:textbox>
							<shma:textbox style="Z-INDEX: 0" id="txtNU1_CONVERTWEIGHT" tabIndex="19" runat="server" CssClass="RequiredField"
								Width="48px" MaxLength="12" BaseType="Character" ReadOnly="True"></shma:textbox>kg
							<asp:comparevalidator id="Comparevalidator3" runat="server" ControlToValidate="txtNU1_ACTUALWEIGHT" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
						</TD>
					</TR>
					<TR class="TRow_Normal">
						<TD id="lbltxt_bmi" width="106" align="right">BMI&nbsp;</TD>
						<TD id="ctltxt_bmi" width="186">
							<shma:textbox style="Z-INDEX: 0" id="txt_bmi" tabIndex="20" runat="server" CssClass="RequiredField"
								Width="184px" BaseType="Character" MaxLength="12"></shma:textbox></TD>
						<TD style="HEIGHT: 21px" width="106" align="right">&nbsp;</TD>
						<TD style="HEIGHT: 21px" width="186"></TD>
					</TR>
					<TR id="rowNPH_INSUREDTYPE" class="TRow_Alt">
						<TD id="TDddlNPH_INSUREDTYPE" width="110" align="right"></TD>
						<TD width="186">
							<P style="Z-INDEX: 0">
								<asp:label style="Z-INDEX: 0" id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red"
									Visible="False"></asp:label></P>
						</TD>
					</TR>
				</TABLE> <!--</fieldset>-->
			</div>
			<INPUT id="HiddenNIC" type="hidden" name="HiddenNIC" runat="server"> <INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server"> <INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px;display: none" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<shma:textbox id="txtNPH_CODE" runat="server" BaseType="Character" width="0px"  style="display: none"></shma:textbox>
			<asp:comparevalidator id="cfvNPH_CODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
				Type="String" Operator="DataTypeCheck" ControlToValidate="txtNPH_CODE"></asp:comparevalidator>
			<shma:textbox id="txtNPH_LIFE" runat="server" BaseType="Character" width="0px" style="display: none"></shma:textbox>
			<asp:comparevalidator id="cfvNPH_LIFE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
				Type="String" Operator="DataTypeCheck" ControlToValidate="txtNPH_LIFE" style="display: none"></asp:comparevalidator>
			<SCRIPT language="javascript">
		
			</SCRIPT>
		</form>
		<SCRIPT language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		
		
		Weight_Conversion();
		convert_to_feet();
		myForm.txtNPH_FULLNAMEARABIC.disabled =true;
		function filterClass(obj_Ref)
		{ 
			
			fcfilterChildCombo(obj_Ref,str_QryCCL_CATEGORYCD,"ddlCCL_CATEGORYCD");
			document.getElementById('ddlCCL_CATEGORYCD').selectedIndex = 1;
		}


		function openPersonsLOV()
		{
			var wOpen;
			var sOptions;
			var aURL="../Presentation/PersonSelectionLOV.aspx";
			var aWinName="Persons_list";

			setFixedValuesInSession('opener=S');

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /2).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
			sOptions = sOptions + ',left=300,top=300';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}


		try{
			parent.frames[1].setViewSecondLife(parent.frames[1].document.getElementById('ddlNPH_INSUREDTYPE').value);
		}catch(e){}

		
		//try{CalculateEntryAge(myForm.txtNPH_BIRTHDATE);}catch(e){myForm.txtNP2_AGEPREM.value='0';}
		CalculateEntryAge(myForm.txtNPH_BIRTHDATE);
		

		attachViewByNameNormal('txtNPH_BIRTHDATE');
		attachViewFocus('INPUT');
		attachViewFocus('SELECT');


		/************************************************************************/
		/********************* Screen Parameterization **************************/
		setFieldStatusAsPerClientSetup("PERSONNEL2");
		
		//Set ID Format
		IDFormat = getFieldFormatFromSetup("PERSONNEL", document.getElementById("txtCNIC_VALUE"));
				
		function checkMandatoryColumns()
		{
			//if(getField("NPH_TITLE").value == "")
			//{
			//	alert("Please select Title.");
			//	getField("NPH_TITLE").focus();
			//	return false;
				
			//}
					
			if(getField("NPH_SEX").value == "")
			{
				alert("Please select Gender.");
				getField("NPH_SEX").focus();
				return false;
			}
			
			if(getField("NPH_BIRTHDATE").value == "")
			{
				alert("Please select Date of Birth.");
				getField("NPH_BIRTHDATE").focus();
				return false;
			}
			
			if(CalculateEntryAge(getField("NPH_BIRTHDATE")) == false)
			{
				getField("NPH_BIRTHDATE").focus();
				return false;
			}
			
			return true;
		}

				
		function beforeSave()
		{
			if(checkMandatoryColumns() == false)
			{
				return false;
			}
			else
			{
				//generateID(IDFormat);		
                
                document.getElementById("txtNU1_CONVERTHEIGHT").readOnly = false;
				document.getElementById("txtNU1_CONVERTWEIGHT").readOnly = false;
				
                EnableFieldsBeforeSubmitting();
				return true;
			}
		}
		function beforeUpdate()
		{
			if(checkMandatoryColumns() == false)
			{
				return false;
			}
			else
			{
				//generateID(IDFormat);
				EnableFieldsBeforeSubmitting();
				return true;
			}
		}
		/************************************************************************/
		</SCRIPT>
		<SCRIPT language="C#" runat="server">
			public int getHighLighted()
			{
				return 0;
			}				
		</SCRIPT>
	</body>
</HTML>
