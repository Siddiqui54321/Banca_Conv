<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>

<%@ Page Language="c#" CodeBehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET.aspx.cs"
    AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET" %>

<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls"
    Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <meta content="text/html; charset=windows-1252" http-equiv="Content-Type">
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <asp:Literal ID="CSSLiteral" EnableViewState="True" runat="server"></asp:Literal>
    <!--
			<script src="JSFiles/SearchCombo/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="JSFiles/SearchCombo/jquery.hyjack.select.js" type="text/javascript"></script>
    	<link href="JSFiles/SearchCombo/hyjack.css" rel="stylesheet" type="text/css" />
    	-->
    <script src="JSFiles/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="JSFiles/popupWindow.js" type="text/javascript"></script>
    <link href="JSFiles/popupWindow.css" rel="stylesheet" type="text/css">
    <script language="javascript" type="text/javascript">
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=200,width=150');
            if (window.focus) { newwindow.focus() }
            return false;
        }

        function validateAccount(sender, args) {
            var acc = document.getElementById('<%= txtNU1_ACCOUNTNO.ClientID %>').value.trim();
            var iban = document.getElementById('<%= txtNU1_ACCIBAN.ClientID %>').value.trim();

            //args.IsValid = !(acc === "" && iban === "")  ; //old condition
          
            // Validate accept only one field account no or IBAN
            if ((acc === "" && iban === "") || (acc !== "" && iban !== "")) {
                args.IsValid = false;
            } else {
                args.IsValid = true;
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
            if (e != 'txtCNIC_VALUE') {
                var key = event.keyCode;
                if (key == 8) {
                    var str = document.getElementById(e).value;
                    var newStr = str.substring(0, str.length - 1);
                    document.getElementById(e).value = newStr;
                }
            }
            else {
                if (document.getElementById('txtCNIC_VALUE').readOnly != true) {
                    var key = event.keyCode;
                    if (key == 8) {
                        var str = document.getElementById(e).value;
                        var newStr = str.substring(0, str.length - 1);
                        document.getElementById(e).value = newStr;
                    }
                }

            }
        }
    </script>
    <script language="javascript" src="JSFiles/PortableSQL.js"></script>
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/NumberFormat.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/GeneralView.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/PersonalDetail.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/Validation/CallValidation.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></script>
    <script language="JavaScript" src='../shmalib/jscript/Date.js'></script>
    <%--    <script language="JavaScript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
   <link href="../shmalib/jscript/select2.css" rel="stylesheet" />
    <script language="JavaScript" src="../shmalib/jscript/select2.js"></script>--%>
    <script>


		
		<asp:Literal id="ltrlApp" runat="server" EnableViewState="True"></asp:Literal>
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
        _lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
        

            function RefreshFields() {
                myForm.ddlNPH_TITLE.selectedIndex = 0;
                myForm.ddlNPH_SEX.selectedIndex = 0;
                myForm.ddlNPH_MARITALSTATUS.selectedIndex = 0;
                myForm.txtNPH_FULLNAME.value = "";

                myForm.txtNPH_FIRSTNAME.value = "";
                myForm.txtNPH_SECONDNAME.value = "";
                myForm.txtNPH_LASTNAME.value = "";

                myForm.txtCNIC_VALUE.readOnly = false;
                myForm.txtNPH_FULLNAMEARABIC.value = "";
                myForm.txtNPH_BIRTHDATE.value = "";
                myForm.txtNU1_ACCOUNTNO.value = "";
                myForm.ddlCOP_OCCUPATICD.selectedIndex = 0;
                myForm.ddlCCL_CATEGORYCD.selectedIndex = 0;
                myForm.ddlNPH_INSUREDTYPE1.selectedIndex = 0;
                //alert(1);
                //myForm.ddlNPH_IDTYPE.selectedIndex =0;
                //alert(2);
                myForm.txtCNIC_VALUE.value = "";
                myForm.txtNPH_ANNUINCOME.value = "";
                myForm.ddlNPH_HEIGHTTYPE.selectedIndex = 2;
                if (application != 'ILLUSTRATION') {
                    myForm.txtNU1_ACTUALHEIGHT.value = "";
                    myForm.txtNU1_ACTUALWEIGHT.value = "";
                }
                myForm.txtNU1_CONVERTHEIGHT.value = "";
                myForm.ddlNPH_WEIGHTTTYPE.selectedIndex = 1;
                myForm.txtNU1_CONVERTWEIGHT.value = "";
                myForm.txt_bmi.value = "";
                myForm.txtNPH_CODE.vaule = "";
                setDefaultValues();
            }

        /******** NOTE: ************************************************/
        /******** 1. Override the JScriptFG.js method.******************/
        /******** 2. ID Existance Error will ask Question to User*******/
        /******** 3. Normal Error will be alert only.*******************/
        /***************************************************************/

        /*
        var IDError = "N";
        var ErrorOccured = false;
        var SaveWitNewID = false;
        function ErrorMessage(errMsg)
        {
            ErrorOccured = true;
            if (errMsg == '<<ID EXISTANCE ERROR>>') 
            {   
                var answer = confirm ("Client ID already exist, create new ID?")
                if(answer)
                {
                    SaveWitNewID = true;
                }				
            }
            else
            {	
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
    	
        function SaveWithNewID()
        {
            if(ErrorOccured == true)
            {
                if(SaveWitNewID == true)
                {
                    setFixedValuesInSession("FLAG_IDEXIST=Y");
                    parent.frames[3].saveClicked();
                    SaveWitNewID = false;
                }
            }
        }*/

        /*function UpdateID()
        {
            if(IDError == "Y")
            {
                if(SaveWitNewID == true)
                {
                    setFixedValuesInSession("FLAG_IDEXIST=Y");
                    parent.frames[3].saveClicked();
                    SaveWitNewID = false;
                }
            }
        }		

        function closeWaitDIV()
        {
            if(IDError == "Y")
                parent.Navigate(false);
            else
                parent.Navigate(true);
            return true;
        }*/





        function CalculateEntryAge(objDate) {
            var years = dateDiffYears(objDate.value, sysDate, parent.parent.ageRoundCriteria)
            if (isNaN(years)) {
                getField("NP2_AGEPREM").value = "";
                return false;
            }
            else {
                if (years < 18) {
                    alert("Age must be 18 years or above");
                    getField("NPH_BIRTHDATE").focus();
                    return false;
                }
                else {
                    getField("NP2_AGEPREM").value = years;
                    return true;
                }
            }
        }




        /********** dependent combo's queries **********/

        //var str_QryCCL_CATEGORYCD="SELECT CCD_CODE "+getConcateOperator()+" '-' "+getConcateOperator()+" CCD_DESCR,CCD_CODE  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='~NP1_CHANNEL~'  ORDER BY CCD_DESCR";		
        //var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";		
        //var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";
        var str_QryCCL_CATEGORYCD = "SELECT ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";
        var navigation = '';
    </script>
    <script type="text/javascript">
        function changeLabelText() {
            // Get bankcode from server session
            var bankcode = '<%= Session["BankCode"] != null ? Session["bankcode"].ToString() : "" %>';

            var label = document.getElementById('<%= lblStaff.ClientID %>');
            var lblRefStaffHeading = document.getElementById('<%= lblRefStaffHeading.ClientID %>');
            var ddlRefStaff = document.getElementById('<%= ddl_refStaff.ClientID %>');

            if (!label) return;

            if (bankcode === "F") {
                label.innerText = "BM/FM";
              //  if (lblRefStaffHeading) lblRefStaffHeading.style.display = "";
              //  if (ddlRefStaff) ddlRefStaff.style.display = "";
            }

            if (bankcode === "9") {
                label.innerText = "2nd Referral Staff";
             //  if (lblRefStaffHeading) lblRefStaffHeading.style.display = "";
              //  if (ddlRefStaff) ddlRefStaff.style.display = "";
            }

            else {
                // hide both controls
              //  if (lblRefStaffHeading) lblRefStaffHeading.style.display = "none";
               // if (ddlRefStaff) ddlRefStaff.style.display = "none";
            }
        }

        // Run automatically on page load
        window.onload = changeLabelText;
</script>


    <style>

        .hidden { display: none; }
        .alert {
            padding: 5px;
            background-color: #336690;
            color: white;
            opacity: 1;
            transition: opacity 0.6s;
            margin-bottom: 15px;
            position: relative;
        }

            .alert.success {
                background-color: #04AA6D;
            }

            .alert.info {
                background-color: #2196F3;
            }

            .alert.warning {
                background-color: #ff9800;
            }

        .closebtn {
            color: white;
            font-weight: bold;
            float: right;
            font-size: 22px;
            cursor: pointer;
        }

            .closebtn:hover {
                color: black;
            }
    </style>
</head>
<body onkeydown="return cancelBack(0)">
    <div>
        <UC:EntityHeading ID="EntityHeading" runat="server" ParamValue="Policy Owner Personal Details"
            ParamSource="FixValue"></UC:EntityHeading>
        <form id="myForm" method="post" name="myForm" runat="server">
            <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            <div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server" style="z-index: 0">
                <table id="entryTable" border="0" cellspacing="0" cellpadding="2">
                    <tr class="form_heading">
                        <td height="20" colspan="4">&nbsp; Policy Owner Personal Details
                        </td>
                    </tr>
                    <tr>
                        <td height="10" colspan="4"></td>
                    </tr>
                    <tr id="rowNPH_TITLE" class="TRow_Normal">
                        <td id="lblddlNPH_TITLE" style="height: 23px" width="110" align="right">Title
                        </td>
                        <td id="ctlddlNPH_TITLE" style="height: 23px" width="186">
                            <SHMA:DropDownList ID="ddlNPH_TITLE" TabIndex="1" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px" Onchange="Title_ChangeEvent(this);"
                                DataValueField="CSD_TYPE" DataTextField="desc_f" BlankValue="True" Font-Italic="False">
                            </SHMA:DropDownList>
                        </td>
                        <td id="lbltxtCNIC_VALUE" width="110" align="right">
                            <SHMA:DropDownList ID="ddlNPH_IDTYPE" TabIndex="3" Style="border-bottom: #444 0px solid; border-left: #444 0px solid; border-top: #444 0px solid; border-right: #444 0px solid"
                                onkeydown="return cancelBack(0)" runat="server" CssClass="RequiredField" Width="100px"
                                DataValueField="NPH_IDTYPE" DataTextField="desc_f" BlankValue="false" Visible="false">
                            </SHMA:DropDownList>
                            CNIC</td>
                        <td id="ctltxtCNIC_VALUE" width="186" style="height: 23px">
                            <SHMA:TextBox onblur="NIC_Blur(this)" ID="txtCNIC_VALUE" onfocus="NIC_Focus(this)"
                                onkeydown="backspaceFunc('txtCNIC_VALUE')" TabIndex="3" onkeypress="return NIC_KeyPress(event,this)"
                                onkeyup="NIC_KeyUp(event,this)" runat="server" Width="184px" DESIGNTIMEDRAGDROP="79"
                                MaxLength="15" onclick="CheckReadOnly()"></SHMA:TextBox>
                        </td>
                        <td id="lbltxtNPH_IDNO2" width="110" style="display: none" align="right">Old N.I.C.
                        </td>
                        <td id="ctltxtNPH_IDNO2" width="186" style="display: none">
                            <SHMA:TextBox ReadOnly="true" ID="txtNPH_IDNO2" TabIndex="3" Style="display: none"
                                runat="server" CssClass="RequiredField" Width="184px" DESIGNTIMEDRAGDROP="79"
                                MaxLength="15"></SHMA:TextBox>
                        </td>
                    </tr>
                    <tr class="TRow_Alt" class="TRow_Alt">
                        <td width="106" align="right" id="TD5">CNIC Issue Date
                        </td>
                        <td width="186" id="ctltxtNPH_DOCISSUEDAT">
                            <SHMA:DatePopUp ID="txtNPH_DOCISSUEDAT" TabIndex="3" runat="server" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNPH_DOCISSUEDAT')" Width="110px" ImageUrl="Images/image1.jpg"
                                ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                            </SHMA:DatePopUp>
                        </td>
                        <td width="106" align="right" id="TD7">CNIC Expiry Date
                        </td>
                        <td width="186" id="ctltxtNPH_DOCEXPIRDAT">
                            <SHMA:DatePopUp ID="txtNPH_DOCEXPIRDAT" TabIndex="3" runat="server" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNPH_DOCEXPIRDAT')" Width="110px" ImageUrl="Images/image1.jpg"
                                ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                            </SHMA:DatePopUp>
                        </td>
                    </tr>
                    <tr id="rowNPH_FULLNAME" class="TRow_Normal">
                        <td id="TDtxtNPH_FULLNAME" width="110" align="right">Name in English
                        </td>
                        <td width="186">
                            <div id="personNameDiv" class="form_heading" style="z-index: 1000; display: none">
                                <table>
                                    <tr>
                                        <td>
                                            <table border="0">
                                                <tr style="color: #e1e1e1">
                                                    <td>First Name
                                                    </td>
                                                    <td>Middle Name
                                                    </td>
                                                    <td>Last Name
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <SHMA:TextBox ID="txtNPH_FIRSTNAME" TabIndex="4" runat="server" Width="160px" MaxLength="50"
                                                            onkeydown="backspaceFunc('txtNPH_FIRSTNAME')" BaseType="Character" onblur="toTitleCase(this);"></SHMA:TextBox>
                                                    </td>
                                                    <td>
                                                        <SHMA:TextBox ID="txtNPH_SECONDNAME" TabIndex="4" runat="server" Width="160px" MaxLength="50"
                                                            onkeydown="backspaceFunc('txtNPH_SECONDNAME')" BaseType="Character" onblur="toTitleCase(this);"></SHMA:TextBox>
                                                    </td>
                                                    <td>
                                                        <SHMA:TextBox ID="txtNPH_LASTNAME" TabIndex="4" runat="server" Width="160px" MaxLength="50"
                                                            onkeydown="backspaceFunc('txtNPH_LASTNAME')" BaseType="Character" onblur="toTitleCase(this);"></SHMA:TextBox>
                                                    </td>
                                                    <td>
                                                        <a href="#" class="button2" tabindex="4" onclick="btnOK_Click()">OK</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <SHMA:TextBox ID="txtNPH_FULLNAME" TabIndex="4" runat="server" CssClass="RequiredField"
                                Width="160px" onchange="generateID(IDFormat);" MaxLength="50" BaseType="Character"></SHMA:TextBox>
                            &nbsp;
                        <input class="BUTTON" title="Open Proposal List Of Values" tabindex="5" onclick="openPersonsLOV();"
                            value=".." type="button" name="ProposalLov">
                            <label id="nameFormat" style="display: none">
                                <font color="#009900">First&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Middle&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Last</font>
                            </label>
                            <asp:RequiredFieldValidator ID="rfvNPH_FULLNAME" runat="server" Display="Dynamic"
                                ControlToValidate="txtNPH_FULLNAME"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reNPH_FULLNAME" runat="server" Display="Dynamic"
                                ErrorMessage="String Format is Incorrect" ControlToValidate="txtNPH_FULLNAME"
                                ValidationExpression="[A-Za-z\s]+"></asp:RegularExpressionValidator>
                        </td>
                        <td id="lbltxtNPH_FULLNAMEARABIC" width="110" align="right">Name in Arabic
                        </td>
                        <td id="ctltxtNPH_FULLNAMEARABIC" width="186">
                            <SHMA:TextBox ID="txtNPH_FULLNAMEARABIC" TabIndex="6" runat="server" Width="184px"
                                onkeydown="backspaceFunc('txtNPH_FULLNAMEARABIC')" MaxLength="50" BaseType="Character"></SHMA:TextBox><asp:CompareValidator
                                    ID="cfvNPH_FULLNAMEARABIC" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                    ControlToValidate="txtNPH_FULLNAMEARABIC" EnableClientScript="False" Type="String"
                                    Operator="DataTypeCheck"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr class="TRow_Alt">
                        <td width="106" align="right" id="TD1">Father/Husband Name
                        </td>
                        <td width="186" id="ctltxtNPH_FATHERNAME">
                            <SHMA:TextBox ID="txtNPH_FATHERNAME" TabIndex="7" runat="server" Width="184px" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNPH_FATHERNAME')" MaxLength="30" ReadOnly="false"></SHMA:TextBox>
                            <asp:RequiredFieldValidator ID="rfctxtNPH_FATHERNAME" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtNPH_FATHERNAME"></asp:RequiredFieldValidator>
                        </td>
                        <td width="106" align="right" id="TD3">Mother's Name
                        </td>
                        <td width="186" id="ctltxtNPH_MAIDENNAME">
                            <SHMA:TextBox ID="txtNPH_MAIDENNAME" TabIndex="8" runat="server" Width="184px" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNPH_MAIDENNAME')" MaxLength="30" ReadOnly="false"></SHMA:TextBox>
                            <asp:RequiredFieldValidator ID="rfctxtNPH_MAIDENNAME" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtNPH_MAIDENNAME"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="rowNPH_BIRTHDATE" class="TRow_Normal">
                        <td id="TDtxtNPH_BIRTHDATE" width="110" align="right">Date of Birth
                        </td>
                        <td width="186">
                            <SHMA:DatePopUp ID="txtNPH_BIRTHDATE" TabIndex="9" runat="server" CssClass="RequiredField"
                                Width="90px" onchange="formatDate(this,'DD/MM/YYYY');CalculateEntryAge(this);generateID(IDFormat);"
                                ImageUrl="Images/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                                <WeekdayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
                                    CssClass="WeekdayStyle" BackColor="White"></WeekdayStyle>
                                <MonthHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                    ForeColor="Black" CssClass="MonthHeaderStyle" BackColor="Yellow"></MonthHeaderStyle>
                                <OffMonthStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"
                                    BackColor="AntiqueWhite"></OffMonthStyle>
                                <GoToTodayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                    ForeColor="Black" BackColor="White"></GoToTodayStyle>
                                <TodayDayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
                                    CssClass="TodayDayStyle" BackColor="LightGoldenrodYellow"></TodayDayStyle>
                                <DayHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                    ForeColor="Black" CssClass="DayHeaderStyle" BackColor="Orange"></DayHeaderStyle>
                                <WeekendStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
                                    CssClass="WeekendStyle" BackColor="LightGray"></WeekendStyle>
                                <SelectedDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                    ForeColor="Black" CssClass="SelectedDateStyle" BackColor="Yellow"></SelectedDateStyle>
                                <ClearDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                    ForeColor="Black" BackColor="White"></ClearDateStyle>
                                <HolidayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
                                    CssClass="HolidayStyle" BackColor="White"></HolidayStyle>
                            </SHMA:DatePopUp>
                            &nbsp;<font id="lbltxtNP2_AGEPREM">Age</font>&nbsp;<SHMA:TextBox ID="txtNP2_AGEPREM"
                                TabIndex="10" CssClass="DisplayOnly" runat="server" Width="40px" MaxLength="2"
                                BaseType="Number" ReadOnly="True"></SHMA:TextBox>
                            <asp:CompareValidator ID="msgNPH_BIRTHDATE" runat="server" CssClass="CalendarFormat"
                                Display="Dynamic" ErrorMessage="{dd/mm/yyyy} " ControlToValidate="txtNPH_BIRTHDATE"
                                Type="Date" Operator="DataTypeCheck" Enabled="true"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rfvNPH_BIRTHDATE" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtNPH_BIRTHDATE"></asp:RequiredFieldValidator>
                        </td>
                        <td id="TDddlNPH_SEX" width="110" align="right">Gender
                        </td>
                        <td width="186">
                            <SHMA:DropDownList ID="ddlNPH_SEX" TabIndex="11" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="82px" Onchange="Gender_ChangeEvent(this);generateID(IDFormat);">
                                <asp:ListItem Selected></asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </SHMA:DropDownList>
                            <asp:CompareValidator ID="cfvNPH_SEX" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="ddlNPH_SEX" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rfvNPH_SEX" runat="server" Display="Dynamic" ErrorMessage="Required"
                                ControlToValidate="ddlNPH_SEX"></asp:RequiredFieldValidator>
                            <SHMA:DropDownList ID="ddlNPH_MARITALSTATUS" TabIndex="12" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="90px" Style="display: none">
                                <asp:ListItem Selected></asp:ListItem>
                                <asp:ListItem Value="S">Single</asp:ListItem>
                                <asp:ListItem Value="M">Married</asp:ListItem>
                            </SHMA:DropDownList>
                        </td>
                    </tr>
                    <tr id="rowddlCOP_OCCUPATICD" class="TRow_Alt">
                        <td id="lblddlCOP_OCCUPATICD" width="110" align="right" style="vertical-align: top">Occupation
                        </td>
                        <td id="ctlddlCOP_OCCUPATICD" width="186">
                            <!--hyjack-->
                            <!-- <a href="#dialog" name="modal">Simple Window Modal</a>   CssClass="RequiredField" -->
                            <SHMA:DropDownList TabIndex="13" BlankValue="True" runat="server" ID="ddlCOP_OCCUPATICD"
                                onkeydown="return cancelBack(0)" Width="160px" DataValueField="COP_OCCUPATICD"
                                DataTextField="desc_f">
                            </SHMA:DropDownList>
                            &nbsp;
                        <input class="BUTTON" title="Open Occupation List Of Values" tabindex="8" onclick="openOccupationDialog();"
                            value=".." type="button" name="ProposalLov ">
                            <div class="alert" style="display: none; overflow: scroll; height: 150px; width: 180%"
                                id="div_alert">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50px">
                                                        <span style="color: White; font-weight: bold">Occupation</span>
                                                    </td>
                                                    <td style="width: 70px">
                                                        <asp:TextBox runat="server" ID="txt_occupationserch" onkeydown="backspaceFunc('txt_occupationserch')"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <input type="button" id="btn_SearchOcc" value="Search" onclick="searchInList()" />
                                                    </td>
                                                    <td>
                                                        <span class="closebtn" onclick="openOccupationDialog();">&times;</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="myList" style="width: 100%">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:CompareValidator ID="cfvCOP_OCCUPATICD" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="ddlCOP_OCCUPATICD" EnableClientScript="False" Type="String"
                                Operator="DataTypeCheck"></asp:CompareValidator><asp:RequiredFieldValidator ID="rfvCOP_OCCUPATICD"
                                    runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCOP_OCCUPATICD"></asp:RequiredFieldValidator>
                        </td>
                        <td id="lblddlCCL_CATEGORYCD" width="110" align="right">Occupational&nbsp;Class
                        </td>
                        <td width="186">
                            <SHMA:DropDownList ID="ddlCCL_CATEGORYCD" TabIndex="14" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px" DataValueField="CCL_CATEGORYCD"
                                DataTextField="desc_f" BlankValue="True">
                            </SHMA:DropDownList>
                            <asp:CompareValidator ID="cfvCCL_CATEGORYCD" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="ddlCCL_CATEGORYCD" EnableClientScript="False" Type="String"
                                Operator="DataTypeCheck"></asp:CompareValidator><asp:RequiredFieldValidator ID="rfvCCL_CATEGORYCD"
                                    runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCCL_CATEGORYCD"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="rowHeightWeight" class="TRow_Normal" runat="server">
                        <td style="height: 21px" id="TDtxtNU1_ACCOUNTNO1" width="110" align="right">Height
                        </td>
                        <td style="height: 21px" width="186">
                            <SHMA:DropDownList ID="ddlNPH_HEIGHTTYPE" TabIndex="15" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="65px" Onchange="convert_to_feet()">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem Value="I">Inches</asp:ListItem>
                                <asp:ListItem Value="F">Feet</asp:ListItem>
                                <asp:ListItem Value="C">Centimeter</asp:ListItem>
                                <asp:ListItem Value="M">Meters</asp:ListItem>
                            </SHMA:DropDownList>
                            &nbsp;<SHMA:TextBox onblur="convert_to_feet()" ID="txtNU1_ACTUALHEIGHT" TabIndex="16"
                                onkeydown="backspaceFunc('txtNU1_ACTUALHEIGHT')" runat="server" CssClass="RequiredField"
                                Width="38px" MaxLength="4" BaseType="Character"></SHMA:TextBox>&nbsp;
                    <SHMA:TextBox ID="txtNU1_CONVERTHEIGHT" TabIndex="17" runat="server" CssClass="DisplayOnly"
                        Width="38px" MaxLength="12" BaseType="Character"></SHMA:TextBox>m
                    <asp:CompareValidator ID="Comparevalidator1" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
                        ControlToValidate="txtNU1_ACTUALHEIGHT" Type="Double" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtNU1_ACTUALHEIGHT"></asp:RequiredFieldValidator>
                        </td>
                        <td width="106" align="right">&nbsp; Weight
                        </td>
                        <td width="186">
                            <SHMA:DropDownList ID="ddlNPH_WEIGHTTTYPE" TabIndex="18" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="65px" Onchange="Weight_Conversion()">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem Value="K">Kilogram</asp:ListItem>
                                <asp:ListItem Value="L">LBs</asp:ListItem>
                            </SHMA:DropDownList>
                            &nbsp;<SHMA:TextBox onblur="Weight_Conversion()" ID="txtNU1_ACTUALWEIGHT" TabIndex="19"
                                onkeydown="backspaceFunc('txtNU1_ACTUALWEIGHT')" runat="server" CssClass="RequiredField"
                                Width="38px" MaxLength="3" BaseType="Character"></SHMA:TextBox>&nbsp;<SHMA:TextBox
                                    ID="txtNU1_CONVERTWEIGHT" TabIndex="20" runat="server" CssClass="DisplayOnly"
                                    Width="38px" MaxLength="12" BaseType="Character"></SHMA:TextBox>kg<asp:CompareValidator
                                        ID="Comparevalidator3" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
                                        ControlToValidate="txtNU1_ACTUALWEIGHT" Type="Double" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtNU1_ACTUALWEIGHT"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="rowBMIAccount" class="TRow_Alt" runat="server">
                        <td id="lbltxt_bmi" width="106" align="right">BMI&nbsp;&nbsp;
                        </td>
                        <td id="ctltxt_bmi" width="186">
                            <SHMA:TextBox ID="txt_bmi" TabIndex="21" runat="server" CssClass="RequiredField"
                                Width="184px" MaxLength="50" BaseType="Character"></SHMA:TextBox>
                        </td>
                        <td width="106" align="right">Single Life&nbsp;
                        </td>
                        <td style="height: 21px" width="186">
                                <SHMA:DropDownList ID="ddlNPH_INSUREDTYPE1" TabIndex="26" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px" onchange="setViewSecondLife(this.value)">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </SHMA:DropDownList>
                        </td>
                    </tr>
                    <tr id="rowAccount" class="TRow_Alt" runat="server">
                        <td id="lbltxtNU1_ACCOUNTNO" align="right" width="110">Account No</td>
                        <td id="ctltxtNU1_ACCOUNTNO" width="186">
                            <SHMA:TextBox ID="txtNU1_ACCOUNTNO" TabIndex="22" runat="server" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNU1_ACCOUNTNO')" Width="184px" onkeypress="return checkNumeric(event)"
                                MaxLength="17" BaseType="Character"></SHMA:TextBox>
                            

                        </td>
                        <td id="lbltxtNU1_ACCOUNTIBANNO" align="right" width="110">IBAN No</td>
                        <td id="ctltxtNU1_ACCOUNTIBANNO" width="186">
                            <SHMA:TextBox ID="txtNU1_ACCIBAN" TabIndex="22" runat="server" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNU1_ACCIBAN')" Width="184px" Style="text-transform:uppercase;"
                                MaxLength="24" BaseType="Character"></SHMA:TextBox>
                            <asp:CustomValidator ID="cvAccountNo" runat="server" 
                                   OnServerValidate="cvAccountNo_ServerValidate"
                                     ClientValidationFunction="validateAccount"
                                        Display="Dynamic" 
                                EnableClientScript="true"
                                  ErrorMessage="Either General Account or IBAN Account is required."
                                
                                 ForeColor="Red">
                </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="TRow_Normal">
                        <td id="TDtxtNU1_ACCOUNTNO" width="110" align="right">Smoker
                        </td>
                        <td width="186">
                            <SHMA:DropDownList ID="ddlNU1_SMOKER" TabIndex="23" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </SHMA:DropDownList>
                            <asp:CompareValidator ID="cfvNU1_SMOKER" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="ddlNU1_SMOKER" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:CompareValidator ID="cfvNU1_ACCOUNTNO" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="txtNU1_ACCOUNTNO" EnableClientScript="False" Type="String"
                                Operator="DataTypeCheck"></asp:CompareValidator>

                        </td>
                        <td id="lblddlBranch" align="right" width="110">Branch
                        </td>
                        <td id="ctlddlBranch" width="186">
                            <SHMA:DropDownList ID="ddlBranch" TabIndex="24" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px" DataValueField="ccs_field1" DataTextField="ccs_descr">
                            </SHMA:DropDownList>
                        </td>
                    </tr>

                    <tr class="TRow_Alt">
                        <td width="106" align="right" id="lbltxtNPH_ANNUINCOME">Yearly Income
                        </td>
                        <td width="186" id="ctltxtNPH_ANNUINCOME">
                            <SHMA:TextBox ID="txtNPH_ANNUINCOME" TabIndex="25" runat="server" Width="184px" CssClass="RequiredField"
                                onkeydown="backspaceFunc('txtNPH_ANNUINCOME')" MaxLength="15" onBlur="AnnualIncom_LostFocus(this);"
                                BaseType="Number" SubType="Currency" Precision="2" ReadOnly="false"></SHMA:TextBox>
                            <asp:CompareValidator ID="cfvNPH_ANNUINCOME" runat="server" ControlToValidate="txtNPH_ANNUINCOME"
                                Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect "
                                Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td style="height: 21px" width="106">
                            <p align="right">
                                <asp:Label ID="lblRefStaffHeading" runat="server" Text="Referral Staff" Visible="false" ></asp:Label>
                        </p>
                        </td>
                        <td style="height: 21px" width="186">
                            <SHMA:DropDownList runat="server" ID="ddl_refStaff" TabIndex="28"
                                Width="184px" DataValueField="staff_id" DataTextField="staff_name" AppendDataBoundItems="true" Visible="False" >
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                            </SHMA:DropDownList>
                        </td>
                        <td id="lblddlCNT_NATCD" style="height: 23px" align="right" width="106">Nationality
                        </td>
                        <td id="ctlddlCNT_NATCD" style="height: 23px" width="186">
                            <SHMA:DropDownList ID="ddlCNT_NATCD" TabIndex="21" runat="server" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="184px" DataValueField="CNT_NATCD" DataTextField="desc_f"
                                BlankValue="True">
                            </SHMA:DropDownList>
                        </td>
                    </tr>
                    <tr class="TRow_Normal" runat="server" id="forBOP">
                        <td id="TDtxtNU1_AccTitle" width="110" align="right" style="font-size: 10px">Account Title
                        </td>
                        <td>
                            <SHMA:TextBox runat="server" ID="txt_accTitle" TabIndex="27" onkeydown="backspaceFunc('txt_accTitle')" Width="184px"></SHMA:TextBox>
                        </td>
                        <td id="TDddl_RefreeStaff" width="110" align="right" style="font-size: 10px">
                            <asp:Label ID="lblStaff" runat="server" Text="2nd Referral Staff"></asp:Label>
                        </td>
                        <td>
                            <SHMA:DropDownList runat="server" ID="ddl_refStaff2" TabIndex="28"
                                Width="184px" DataValueField="staff_id" DataTextField="staff_name" AppendDataBoundItems="true">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                            </SHMA:DropDownList>
                        </td>
                    </tr>
                    <%--chg-25082023--%>
                    <tr class="TRow_Alt" runat="server" id="forBOP1">
                         <td id="TDddl_RefreeStaff2" width="100" align="right" style="font-size: 10px">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <%--chg-end--%>
                    <tr>
                        <td width="106">
                            <p>
                                &nbsp;
                            </p>
                        </td>
                        <td width="186"></td>
                        <td id="TDtxtNU1_ACCOUNTNO3" width="110" align="right"></td>
                        <td width="186">
                            <asp:CompareValidator ID="cfvNPH_INSUREDTYPE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                                ControlToValidate="ddlNPH_INSUREDTYPE1" Type="String"
                                Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rfvNPH_INSUREDTYPE" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="ddlNPH_INSUREDTYPE1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="rowNPH_INSUREDTYPE" class="TRow_Alt">
                        <td id="TDddlNPH_INSUREDTYPE1" width="110" align="right"></td>
                        <td width="186">
                            <p>
                                <asp:Label ID="lblServerError" EnableViewState="false" runat="server" Visible="False"
                                    ForeColor="Red"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
            <input id="HiddenNIC" type="hidden" name="HiddenNIC" runat="server">
            <input id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
            <input id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
            <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
            <input style="width: 0px; display: none" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
                runat="server" onserverclick="_CustomEvent_ServerClick">
            <input id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
            <div style="display: none">

                <SHMA:TextBox ID="txtNPH_CODE" runat="server" Width="0px" BaseType="Character" Style="display: none"></SHMA:TextBox>
                <asp:CompareValidator ID="cfvNPH_CODE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                    ControlToValidate="txtNPH_CODE" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:CompareValidator>
                <SHMA:TextBox ID="txtNPH_LIFE" runat="server" Width="0px" BaseType="Character" Style="display: none"></SHMA:TextBox>
                <asp:CompareValidator ID="cfvNPH_LIFE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
                    ControlToValidate="txtNPH_LIFE" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:CompareValidator>

            </div>
            <script language="javascript">


                function setViewSecondLife(val) {

                    if (val == 'N') {
                        if (parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS') == null) {
                            parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.display = "block"; //135
                            parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.height = "250px"; //135
                            parent.document.getElementById('mainContentFrame').style.height = '740.0px'; //'460.0px';
                        }
                        else {
                            parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.display = "block"; //135
                            parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.height = "250px"; //135
                            parent.parent.document.getElementById('mainContentFrame').style.height = '740.0px'; //'460.0px';
                        }
                    }
                    else {
                        if (parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS') == null) {
                            parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.display = "none";
                            parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.height = 0;
                            parent.document.getElementById('mainContentFrame').style.height = '490.0px'; //optional
                        }
                        else {
                            parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.display = "none";
                            parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').style.height = 0;
                            parent.parent.document.getElementById('mainContentFrame').style.height = '490.0px'; //optional
                        }
                    }
                }

            </script>
            <div id="boxes">
                <div id="dialog" class="window">
                    <div width="100%">
                        <input type="button" value="Close" onclick="closeOccupationDialog();" style="position: relative; right: 1px"></input>
                        <label class="fieldHeading">
                            Search :</label>
                        <asp:TextBox runat="server" ID="txtSearchOccupation"></asp:TextBox>
                        <input type="button" id="btnSearch" value="Search" onclick="filterResult();"></input>
                        <div id='imgLoading'>
                            <img id='img' style="width: 15px;" src="Images/loading.gif" />
                            Please Wait...
                        </div>
                        <div id='noResult' style="display: none;">
                            No Result Found.
                        </div>
                    </div>
                    <div style="width: 550px; margin-top: 5px" class="form_heading">
                        Occupation
                    </div>
                    <div id='divOccupation' style="width: 550px; height: 150px; overflow: auto">
                        <asp:Repeater runat="server" ID="dgOccupation">
                            <HeaderTemplate>
                                <ul id='ulOccupation' class="ulSearch">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class='ItemStyle ListRowItem' onclick="SearchItemClicked('<%# DataBinder.Eval(Container, "DataItem.COP_OCCUPATICD") %>');">
                                    <a href="#" onclick="">
                                        <%# DataBinder.Eval(Container, "DataItem.DESC_F") %>
                                    </a></li>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <li class='ItemStyle ListRowAlt' onclick="SearchItemClicked('<%# DataBinder.Eval(Container, "DataItem.COP_OCCUPATICD") %>');">
                                    <a href="#" onclick="">
                                        <%# DataBinder.Eval(Container, "DataItem.DESC_F") %>
                                    </a></li>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <script language="javascript" type="text/javascript">
                            function SearchItemClicked(li, id) {
                                var dropelement = document.getElementById('ddlCOP_OCCUPATICD');
                                dropelement.value = id;
                                document.getElementById("<%=txt_occupationserch.ClientID%>").value = '';
                                openOccupationDialog();
                                filterClass(dropelement);
                            } /*
							function setOccupationByID(id){
								if($("#ddlCOP_OCCUPATICD").length==1){
								var str_Query = "SELECT COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD='"+id+"'";
								var result = RSExecute("RemoteService.aspx", "GetComboDescription", str_Query  );			
								
								$("#ddlCOP_OCCUPATICD").empty();
								
								$('#ddlCOP_OCCUPATICD')
										.append($("<option></option>")
										.attr("value",id)
										.text(result.return_value)); 
								}
								$("#ddlCOP_OCCUPATICD").val(id);
								closeOccupationDialog();
		
								filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
								
							}
							function openOccupationDialog(){
								showDialog('#dialog',function(){
									//alert($('#ulOccupation li').length );
									if($('#ulOccupation li').length==0)  
										$('#imgLoading').show('fast',function(){
											$('#imgLoading').delay(500).queue(function(){
												jasonCallToGetOccupations(function(){
													$('#imgLoading').hide();}
												);
											});
										});
									else{
										$('#imgLoading').hide();}
								});}
                                
							function closeOccupationDialog(){
								hideDialog('#dialog');
								document.getElementById("ddlCCL_CATEGORYCD").focus();
							}*/
                            /*jQuery.extend(
                            jQuery.expr[':'], { 
                            Contains : "jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase())>=0" 
                            });*/
                            jQuery.expr[':'].Contains = function (a, i, m) {
                                return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
                            };

                            //TODO: to clear Cache programtically
                            ///localStorage.removeItem("ulOccupation")

                            function openOccupationDialog() {
                                if (document.getElementById('div_alert').style.display == 'block') {
                                    document.getElementById('div_alert').style.display = 'none';
                                    document.getElementById("<%=txtNU1_ACTUALHEIGHT.ClientID%>").focus();
                                    document.getElementById('lblddlCCL_CATEGORYCD').style.display = 'block';
                                    document.getElementById('ddlCCL_CATEGORYCD').style.display = 'block';

                                }
                                else {
                                    document.getElementById('div_alert').style.display = 'block';
                                    document.getElementById("<%=txt_occupationserch.ClientID%>").focus();
                                    document.getElementById('lblddlCCL_CATEGORYCD').style.display = 'none';
                                    document.getElementById('ddlCCL_CATEGORYCD').style.display = 'none';
                                    var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid,0');
                                    document.getElementById('myList').innerHTML = tbl;
                                }

                            }
                            function searchInList() {
                                var textvalue = document.getElementById("<%=txt_occupationserch.ClientID%>");
                                if (textvalue.value != '' && textvalue.value != null) {
                                    var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid,' + textvalue.value + '');
                                    document.getElementById('myList').innerHTML = tbl;
                                }
                                else {
                                    alert("Please Enter Occupation to Search");
                                    textvalue.focus();
                                }
                            }
                            function jasonCallToGetOccupations(callback) {
                                try {
                                    if (localStorage) {
                                        if (localStorage["ulOccupation"] == null) {
                                            var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
                                            $('#divOccupation').html(tbl);
                                            localStorage["ulOccupation"] = tbl;
                                        }
                                        else {
                                            $('#divOccupation').html(localStorage["ulOccupation"]);
                                        }
                                    }
                                    else
                                        var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
                                    $('#divOccupation').html(tbl);
                                }
                                catch (err) {
                                    var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
                                    $('#divOccupation').html(tbl);
                                }

                                if (callback)
                                    callback();
                            }

                            function filterResult() {
                                $('#noResult').hide();
                                $('#imgLoading').show();
                                $('#ulOccupation li').hide();
                                if ($('#txtSearchOccupation').val() == '') {
                                    $('#ulOccupation li').show();
                                    $('#imgLoading').hide();
                                }
                                else {
                                    $('#ulOccupation li:Contains("' + $('#txtSearchOccupation').val() + '")').show(function () { $('#imgLoading').hide(); });
                                }
                                if ($('#ulOccupation li:visible').length == 0) {
                                    $('#imgLoading').hide();
                                    $('#noResult').show();
                                }
                            }
                        </script>
                    </div>
                </div>
        </form>
        <script language="javascript">



            <asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>
            if (_lastEvent == 'New')
                addClicked();
            fcStandardFooterFunctionsCall();//alert(_lastEvent);

            <asp:literal id="ErrorOccured" runat="server" EnableViewState="True"></asp:literal>
            document.getElementById("txtNU1_CONVERTHEIGHT").readOnly = true;
            document.getElementById("txtNU1_CONVERTWEIGHT").readOnly = true;
            document.getElementById("txt_bmi").readOnly = true;


            Weight_Conversion();
            convert_to_feet();
            myForm.txtNPH_FULLNAMEARABIC.disabled = true;
            function filterClass(obj_Ref) { /*
			if(myForm.ddlCOP_OCCUPATICD.disabled == true)//As now User can select Category if it is disable 
			{   
                //Show all record if Occupation is disabled (Do not filter)
				str_QryCCL_CATEGORYCD="SELECT CCL_DESCRIPTION, CCL_CATEGORYCD from LCCL_CATEGORY";
			}
            */
                fcfilterChildCombo(obj_Ref, str_QryCCL_CATEGORYCD, "ddlCCL_CATEGORYCD");
                document.getElementById('ddlCCL_CATEGORYCD').selectedIndex = 1;
            }



            function openPersonsLOV() {
                var wOpen;
                var sOptions;
                var aURL = "../Presentation/PersonSelectionLOV.aspx";
                var aWinName = "Persons_list";

                setFixedValuesInSession('opener=F');

                sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
                sOptions = sOptions + ',width=' + (screen.availWidth / 2).toString();
                sOptions = sOptions + ',height=' + (screen.availHeight / 2.6).toString();
                sOptions = sOptions + ',left=300,top=300';

                wOpen = window.open('', aWinName, sOptions);
                wOpen.location = aURL;
                wOpen.focus();
                return wOpen;
            }

            try {
                //alert("11");
                if (document.getElementById('ddlNPH_INSUREDTYPE1').value == 'Y') {
                    //alert("22");
                    parent.frames[2].location = parent.frames[2].location;
                }
                //alert("calling setViewSecondLife");
                setViewSecondLife(document.getElementById('ddlNPH_INSUREDTYPE1').value);

                //parent.parent.setPage('shgn_gp_gp_ILUS_ET_GP_BENEFECIARY');

            } catch (e) { }



            try { CalculateEntryAge(myForm.txtNPH_BIRTHDATE); } catch (e) { myForm.txtNP2_AGEPREM.value = '0'; }


            //parent.closeWait();
            parent.closeWait2(navigation);


            attachViewByNameNormal('txtNPH_BIRTHDATE');
            //filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
            attachViewFocus('INPUT');
            attachViewFocus('SELECT');


            /************************************************************************/
            /********************* Screen Parameterization **************************/
            //hideColumn();
            setFieldStatusAsPerClientSetup("PERSONNEL");
            setCombosValues();

            //Set ID Format
            IDFormat = getFieldFormatFromSetup("PERSONNEL", document.getElementById("txtCNIC_VALUE"));

            filterClass(document.getElementById('ddlCOP_OCCUPATICD'));

            function setCombosValues() {
                if (getField("NPH_MARITALSTATUS").style.visibility == 'visible') {
                    if (getField("NPH_MARITALSTATUS").disabled == false) {
                        if (getField("NPH_MARITALSTATUS").value == "") {
                            getField("NPH_MARITALSTATUS").value = "S";
                        }
                    }
                }
            }

            function checkMandatoryColumns() {

                /*if(!(getField("NU1_ACCOUNTNO").value.length == 8 || getField("NU1_ACCOUNTNO").value.length == 9 || getField("NU1_ACCOUNTNO").value.length == 11 || getField("NU1_ACCOUNTNO").value.length == 12))
                {
                    alert("Length of Account No. can only 8,9,11 or 12.");
                    getField("txtNU1_ACCOUNTNO").focus();
                    return false;
                }*/


                //if ((getField("NU1_ACCOUNTNO").value.length == 0)) { // old code for check A/c no
                //    alert("Please Provide Account No.");
                //    getField("txtNU1_ACCOUNTNO").focus();
                //    return false;
                //}

                // New code for check general and IBAN A/c
                var acc = document.getElementById('<%= txtNU1_ACCOUNTNO.ClientID %>').value.trim();
                var iban = document.getElementById('<%= txtNU1_ACCIBAN.ClientID %>').value.trim();

              
                //  validate fill only one field general account or IBAN

                if ((acc.length === 0 && iban.length === 0) || (acc.length > 0 && iban.length > 0)) {
                    alert("Please provide only one: either General Account No. or IBAN Account (not both).");
                    getField("txtNU1_ACCOUNTNO").focus(); // focus first field
                    return false;
                }
               



                // validate general account length if provided
                <%--if (acc.length > 0 && !(acc.length === 8 || acc.length === 9 || acc.length === 11 || acc.length === 12)) {
                    alert("Length of Account No. can only be 8, 9, 11 or 12.");
                    document.getElementById('<%= txtNU1_ACCOUNTNO.ClientID %>').focus();
                    return false;
                }--%>

                // validate IBAN length if provided (standard: 15–34 chars)
                
                if (iban.length > 0 && (iban.length !== 24 || !/^[a-zA-Z0-9]+$/.test(iban))) {
                    alert("Length of IBAN must be exactly 24 characters alphanumeric.");
                    document.getElementById('<%= txtNU1_ACCIBAN.ClientID %>').focus();
                    return false;
                }


                return true; // passes validation
                // End of new code for A/c and IBAN 

                if (document.getElementById('forBOP').style.visibility == "visible") {
                    if (document.getElementById('txt_accTitle').value == "") {
                        alert("Please Enter Account Title.");
                        document.getElementById('txt_accTitle').focus();
                        return false;
                    }
                    if ((document.getElementById('ddl_refStaff').value == "0")) {
                        alert("Please Select Referee Staff");
                        document.getElementById('ddl_refStaff').focus();
                        return false;
                    }
                }
<%--chg-25082023--%>
                if (document.getElementById('forBOP1').style.visibility == "visible") {
                    if (document.getElementById('txt_accTitle').value == "") {
                        alert("Please Enter Account Title.");
                        document.getElementById('txt_accTitle').focus();
                        return false;
                    }
                    //if ((document.getElementById('ddl_refStaff2').value == "0")) {
                    //    alert("Please Select Referee Staff");
                    //    document.getElementById('ddl_refStaff2').focus();
                    //    return false;
                    //}
                }
<%--chg-end--%>

                //if(getField("NPH_TITLE").value == "")
                //{
                //	alert("Please select Title.");
                //	getField("NPH_TITLE").focus();
                //	return false;
                //}

                if (getField("NPH_SEX").value == "") {
                    alert("Please select Gender.");
                    getField("NPH_SEX").focus();
                    return false;
                }

                if (getField("NPH_MARITALSTATUS").style.visibility == 'visible') {
                    if (getField("NPH_MARITALSTATUS").disabled == false) {
                        if (getField("NPH_MARITALSTATUS").value == "") {
                            alert("Please select Marital Status.");
                            getField("NPH_MARITALSTATUS").focus();
                            return false;
                        }
                    }
                }


                //ID Number (NIC, CNIC, POC etc.)
                //if(getField("CNIC_VALUE").style.visibility == 'visible')
                //{
                if (getField("CNIC_VALUE").disabled == false) {
                    if (getField("CNIC_VALUE").value == "") {
                        var idType = getField("NPH_IDTYPE").value;
                        alert("Please select " + idType);
                        getField("CNIC_VALUE").focus();
                        return false;
                    }
                }
                //}

                //Nationality
                if (getField("CNT_NATCD").style.visibility == 'visible') {
                    if (getField("CNT_NATCD").disabled == false) {
                        if (getField("CNT_NATCD").value == "") {
                            alert("Please select Nationality.");
                            getField("CNT_NATCD").focus();
                            return false;
                        }
                    }
                }

                if (getField("NPH_BIRTHDATE").value == "") {
                    alert("Please select Date of Birth.");
                    getField("NPH_BIRTHDATE").focus();
                    return false;
                }

                if (CalculateEntryAge(getField("NPH_BIRTHDATE")) == false) {
                    getField("NPH_BIRTHDATE").focus();
                    return false;
                }

                if (application == "BANCASSURANCE") {
                    /*
                    if(getField("NU1_ACCOUNTNO").value == "")
                    {
                        alert("Please enter Account No.");
                        getField("NU1_ACCOUNTNO").focus();
                        return false;
                    }	
                    */
                }

                //Page_ClientValidate();//this is commented as all Validators are not required
                var rfv = document.getElementById("rfvtxtNU1_ACCOUNTNO");
                ValidatorValidate(rfv);

                if (Page_IsValid == false) {
                    return false;
                }

                return true;
            }

            function beforeSave() {

                if (checkMandatoryColumns() == false) {
                    return false;
                }
                else {
                    //generateID(IDFormat);
                    if (IDgenerated(IDFormat) == false) {
                        alert("ID can't be empty");
                        return false;
                    }
                    else {

                        EnableFieldsBeforeSubmitting();

                        disableFieldsBasedOnNIC(false);

                        getField("COP_OCCUPATICD").disabled = false;

                        return true;
                    }
                }
            }

            function beforeUpdate() {
                //alert("before calling txtNU1_CONVERTHEIGHT  "+document.getElementById("txtNU1_CONVERTHEIGHT").value);
                //alert("before calling txtNU1_CONVERTWEIGH "+document.getElementById("txtNU1_CONVERTWEIGHT").value);
                //    document.getElementById("txtNU1_CONVERTHEIGHT").readOnly = false;
                //	document.getElementById("txtNU1_CONVERTWEIGHT").readOnly = false;
                if (checkMandatoryColumns() == false) {
                    return false;
                }
                else {
                    EnableFieldsBeforeSubmitting();
                    disableFieldsBasedOnNIC(false);

                    getField("COP_OCCUPATICD").disabled = false;

                    return true;
                }
            }

            function AnnualIncom_LostFocus(obj) {
                if (obj.style.visibility == 'visible') {
                    if (obj.disabled == false) {
                        if (obj.value == null || obj.value == "") {
                            alert("Annual Income is mandatory.");

                        }
                    }
                }
            }

            function Name_GotFocus(objName) {
                document.getElementById("nameFormat").style.display = "";
                showNameDiv(true);
            }
            function Name_LostFocus(objName) {
                document.getElementById("nameFormat").style.display = "none";
            }
            /************************************************************************/
            setFieldsBasedOnNIC(getField("CNIC_VALUE"));


            //**********************************************************************************************************
            //************************ Mulitple Name Chnages ***********************************************************
            //**********************************************************************************************************
            function getElementTop(Elem) {
                if (document.getElementById) {
                    var elem = document.getElementById(Elem);
                } else if (document.all) {
                    var elem = document.all[Elem];
                }

                var yPos = elem.offsetTop;
                tempEl = elem.offsetParent;
                while (tempEl != null) {
                    yPos += tempEl.offsetTop;
                    tempEl = tempEl.offsetParent;
                }
                return yPos - 10 + 'px';
            }

            function getElementLeft(Elem) {
                if (document.getElementById) {
                    var elem = document.getElementById(Elem);
                } else if (document.all) {
                    var elem = document.all[Elem];
                }

                var xPos = elem.offsetLeft;
                tempEl = elem.offsetParent;
                while (tempEl != null) {
                    xPos += tempEl.offsetLeft;
                    tempEl = tempEl.offsetParent;
                }
                return xPos - 16 + 'px';
            }

            function setDivPos() {
                personNameDiv.style.position = "absolute";
                personNameDiv.style.top = getElementTop("txtNPH_FULLNAME");
                personNameDiv.style.left = getElementLeft("txtNPH_FULLNAME");
            }


            //Variable related to NAMES Div only - should work only for IE-6
            var CombosHide = false;
            function showNameDiv(bln) {
                var gender = getField("NPH_SEX");
                var MaritalStatus = getField("NPH_MARITALSTATUS");
                var Occupation = getField("COP_OCCUPATICD");
                var OccupationClass = getField("CCL_CATEGORYCD");


                if (bln == true) {
                    personNameDiv.style.display = "";
                    setDivPos();
                    getField("NPH_FIRSTNAME").focus();

                    if (gender.style.display == '' || gender.style.visibility == 'visible') gender.style.visibility = 'hidden';
                    if (MaritalStatus.style.display == '' || MaritalStatus.style.visibility == 'visible') MaritalStatus.style.visibility = 'hidden';
                    if (Occupation.style.display == '' || Occupation.style.visibility == 'visible') Occupation.style.visibility = 'hidden';
                    if (OccupationClass.style.display == '' || OccupationClass.style.visibility == 'visible') OccupationClass.style.visibility = 'hidden';

                    CombosHide = true;
                }
                else {
                    personNameDiv.style.display = "none";

                    if (CombosHide == true) {
                        if (gender.style.visibility == 'hidden') gender.style.visibility = 'visible';
                        if (MaritalStatus.style.visibility == 'hidden') MaritalStatus.style.visibility = 'visible';
                        if (Occupation.style.visibility == 'hidden') Occupation.style.visibility = 'visible';
                        if (OccupationClass.style.visibility == 'hidden') OccupationClass.style.visibility = 'visible';
                    }
                    CombosHide = false;

                    getField("NPH_FATHERNAME").focus();
                }
            }

            function btnOK_Click() {
                if (Trim(getField("NPH_FIRSTNAME").value).length > 0) {
                    //*********************** Set Name first ***************************//
                    //First Name
                    getField("NPH_FULLNAME").value = Trim(getField("NPH_FIRSTNAME").value);
                    //Second Name
                    if (Trim(getField("NPH_SECONDNAME").value).length > 0) getField("NPH_FULLNAME").value += " " + Trim(getField("NPH_SECONDNAME").value);
                    //Last Name
                    if (Trim(getField("NPH_LASTNAME").value).length > 0) getField("NPH_FULLNAME").value += " " + Trim(getField("NPH_LASTNAME").value);

                    //*********************** Generate ID Now ***************************//
                    showNameDiv(false);
                    //Generate ID
                    generateID(IDFormat);
                }
                else {
                    alert("First Name can't be empty");
                }
            }
            function CheckReadOnly() {
                if (document.getElementById('txtCNIC_VALUE').readOnly == true) {
                    alert('CNIC cannot be change.');
                }

            }

        </script>
    </div>
</body>
</html>
