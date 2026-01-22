<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>

<%@ Page Language="c#" EnableEventValidation="false" CodeBehind="shgn_ss_se_stdscreen_ILUS_ET_CD_COMPANYDET.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.shgn_ss_se_stdscreen_ILUS_ET_CD_COMPANYDET" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <meta content="text/html; charset=windows-1252" http-equiv="Content-Type">
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <asp:Literal ID="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
    <script language="javascript" src="JSFiles/PortableSQL.js"></script>
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/NumberFormat.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
        _lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
        function RefreshFields() {
                //if(myForm.txtNP1_PROPOSAL.disabled==true)
                //	 myForm.txtNP1_PROPOSAL.disabled=false;
                //	 myForm.txtNP1_PROPOSAL.value ="";
                //				 myForm.ddlNPH_FULLNAME.selectedIndex =0;
                //				 myForm.ddlCCN_CTRYCD.selectedIndex =0;
                //				 myForm.ddlUSE_USERID.selectedIndex =0;

                //myForm.txtNP1_PROPOSAL.focus();
                //setDefaultValues();
            }

        /********** dependent combo's queries **********/

        function saveUpdate() {
            if (this.Page_ClientValidate()) {
                __doPostBack("btn_save", "btn_save_Click");
            }

        }
        function CopyCorrespondence() {
            __doPostBack("btn_copycor", "btn_copycor_Click");
        }
        function callUpdate() {
            if (parent.frames[0].Page_ClientValidate()) {
                parent.openWait('Saving Data');
                parent.frames[0].saveUpdate();
                //saveUpdate();
                //parent.closeWait();
            }

        }
        function cancelBack() {
            var key = event.keyCode;
            if (key == 8) {
                return false;
            }
            else {

            }
        }
    </script>
    <script>
        function NTN_Blur(objNTN)
        {
		    if(objNTN.value.length == 7)
		    {
			    var className="ace.ILUS_ET_NM_PER_PERSONALDET";
			    var methodName="checkAndGetClientInfo";
			    var parameters = objNIC.value+ "," + _lastEvent.toUpperCase();
			    var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			
			    if(str_resultArray[0] == "Y")
			    {
                    document.getElementById('txtCNIC_VALUE').readOnly = true;
				    getField("NPH_TITLE").value = str_resultArray[1];
				    getField("NPH_FULLNAME").value = str_resultArray[2];

				    getField("NPH_DOCISSUEDAT").value = str_resultArray[3];
				    getField("NPH_DOCEXPIRDAT").value = str_resultArray[4];
				    getField("NPH_FATHERNAME").value = str_resultArray[5];
				    getField("NPH_MAIDENNAME").value = str_resultArray[6]; 
				
				    getField("NPH_BIRTHDATE").value = str_resultArray[7];
				    getField("NPH_SEX").value = str_resultArray[8];
			}
		    else
		    {
			    var objIdType = getField("NPH_IDTYPE");
			    var index = objIdType.selectedIndex;
			    var Id = objIdType.options[index].text;
			    alert(Id + " must be 7 characters long.");
			    return;	
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
</head>
<body onkeydown="return cancelBack(0)">
    <UC:EntityHeading ID="EntityHeading" runat="server" ParamValue="" ParamSource="FixValue"></UC:EntityHeading>
    <form id="myForm" method="post" name="myForm" runat="server">
        <div style="z-index: 0" id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server">
            <table id="entryTable1" border="0" cellspacing="0" cellpadding="2">
                <tr class="form_heading">
                    <td height="20" colspan="4">&nbsp; Assignee
                    </td>
                </tr>
                <tr>
                    <td height="10" colspan="4"></td>
                </tr>
            </table>
            <table id="entryTable" runat="server" border="0" cellspacing="0" cellpadding="2">
                <tr id="row" class="TRow_Alt">
                    <td style="width: 138px; height: 22px" width="138" align="right">
                        <p>Company</p>
                    </td>
                    <td style="height: 22px" width="186">
                        <p>
                            <SHMA:DropDownList ID="ddl_CompanyLOV" TabIndex="0" runat="server" DataTextField="com_name" DataValueField="com_id" onkeydown="return cancelBack()"
                                Width="184px" AutoPostBack="True" BlankValue="True" CssClass="RequiredField" BackColor="White" OnSelectedIndexChanged="ddl_CompanyLOV_SelectedIndexChanged">
                            </SHMA:DropDownList>
                        </p>
                    </td>
                </tr>
                <tr id="rowNP1_COUNTRY" class="TRow_Normal">
                    <td style="width: 138px; height: 22px" width="138" align="right">NTN</td>
                    <td style="height: 22px" width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_IDNO" onblur="NTN_Blur(this)" TabIndex="1" BackColor="White" runat="server" Width="184px" BaseType="Character" onkeydown="backspaceFunc('txtNAS_IDNO')"
                            MaxLength="7" CssClass="RequiredField" OnTextChanged="txtNAS_IDNO_TextChanged" AutoPostBack="true"></SHMA:TextBox>
                        <asp:CompareValidator ID="cmpNP1_PAGER" runat="server" ControlToValidate="txtNAS_IDNO" Operator="DataTypeCheck"
                            Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
                    </td>

                    <td style="width: 99px; height: 22px" width="99" align="right">Payer/Assignee</td>
                    <td width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_NAME" TabIndex="2" runat="server" Width="184px" BaseType="Character" onkeydown="backspaceFunc('txtNAS_NAME')" BackColor="White"
                            Precision="2" MaxLength="200" CssClass="RequiredField"></SHMA:TextBox>
                    </td>
                </tr>
                <tr id="rowNP1_PROVINCE" class="TRow_Alt">
                    <td style="width: 138px; height: 22px" width="138" align="right">
                        <p>Country</p>
                    </td>
                    <td style="height: 22px" width="186">
                        <p>
                            <SHMA:DropDownList ID="ddlCCN_COUNTRY" TabIndex="3" runat="server" DataTextField="CCN_DESCR" DataValueField="CCN_CTRYCD" onkeydown="return cancelBack()"
                                Width="184px" AutoPostBack="True" BlankValue="True" CssClass="RequiredField" BackColor="White" OnSelectedIndexChanged="ddlCCN_COUNTRY_SelectedIndexChanged">
                            </SHMA:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCCN_COUNTRY" Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                        </p>
                    </td>

                    <td style="width: 99px" width="99" align="right">Telephone</td>
                    <td width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_TELEPHONE" TabIndex="9" runat="server" Width="184px" BaseType="Number" onkeydown="backspaceFunc('txtNAS_TELEPHONE')" BackColor="White"
                            Precision="2" MaxLength="12" CssClass="RequiredField"></SHMA:TextBox>
                        <asp:CompareValidator ID="cmpNAS_TELEPHONE" runat="server" ControlToValidate="txtNAS_TELEPHONE" Operator="DataTypeCheck"
                            Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="rowNP1_CITY" class="TRow_Normal">
                    <td style="width: 138px" width="138" align="right">Province/State</td>
                    <td width="186">
                        <asp:DropDownList Style="z-index: 0" ID="ddlCCN_PROVINCE" TabIndex="4" runat="server" DataTextField="CPR_DESCR" onkeydown="return cancelBack()"
                            AutoPostBack="True" DataValueField="CPR_PROVCD" Width="184px" OnSelectedIndexChanged="ddlCCN_PROVINCE_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td style="width: 99px" width="99" align="right">Fax No</td>
                    <td width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_FAX" TabIndex="10" runat="server" Width="184px" BaseType="Number" onkeydown="backspaceFunc('txtNAS_FAX')"
                            MaxLength="12" BackColor="White" CssClass="RequiredField"></SHMA:TextBox>
                        <asp:CompareValidator ID="cmpNAS_FAX" runat="server" ControlToValidate="txtNAS_FAX" Operator="DataTypeCheck"
                            Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="rowNP1_ADDRESS2" class="TRow_Normal">
                    <td style="width: 138px" width="138" align="right">City</td>
                    <td width="186">
                        <asp:DropDownList ID="ddlCCN_CITY" TabIndex="5" runat="server" BackColor="White" DataTextField="CCT_DESCR" DataValueField="CCT_CITYCD" onkeydown="return cancelBack()"
                            Width="184px" CssClass="RequiredField">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 99px; height: 22px" width="99" align="right">PO Box</td>
                    <td style="height: 22px" width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_POBOX" TabIndex="11" BackColor="White" runat="server" Width="184px" BaseType="Number" onkeydown="backspaceFunc('txtNAS_IDNO')"
                            MaxLength="6" CssClass="RequiredField"></SHMA:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtNAS_POBOX" Operator="DataTypeCheck"
                            Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="rowNP1_ADDRESS1" class="TRow_Alt">
                    <td style="width: 138px; height: 22px" width="138" align="right">Address</td>
                    <td style="height: 22px" colspan="3">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_ADDRESS" TabIndex="6" runat="server" Width="89%" onkeydown="backspaceFunc('txtNAS_ADDRESS')"
                            Precision="2" MaxLength="300"></SHMA:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNAS_ADDRESS"
                            Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr id="rowNP1_ADDRESS3" class="TRow_Alt">
                    <td style="width: 138px" width="138" align="right">Amount</td>
                    <td width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_AMOUNT" TabIndex="7" runat="server" Width="184px" onkeydown="backspaceFunc('txtNAS_AMOUNT')"
                            Precision="2" BaseType="Number" SubType="Currency"></SHMA:TextBox></td>
                    <td style="width: 99px" width="99" align="right">Percentage</td>
                    <td width="186">
                        <SHMA:TextBox Style="z-index: 0" ID="txtNAS_PERCENTAGE" TabIndex="12" runat="server" Width="184px" onkeydown="backspaceFunc('txtNAS_PERCENTAGE')"
                            BaseType="Number" Text="100" Enabled="false" MaxLength="3"></SHMA:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 138px" width="138" align="right">Date</td>
                    <td width="186">
                        <SHMA:DatePopUp ID="txtNAS_ASSDATE" TabIndex="8" runat="server" CssClass="RequiredField"
                            onkeydown="backspaceFunc('txtNAS_ASSDATE')" Width="168px" ImageUrl="Images/image1.jpg"
                            ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
                        </SHMA:DatePopUp>
                        <asp:CompareValidator ID="msgNAS_ASSDATE" runat="server" CssClass="CalendarFormat"
                            Display="Dynamic" ErrorMessage="{dd/mm/yyyy} " ControlToValidate="txtNAS_ASSDATE"
                            Type="Date" Operator="DataTypeCheck" Enabled="true"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="rfvNAS_ASSDATE" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="txtNAS_ASSDATE"></asp:RequiredFieldValidator>
                        <%-- <SHMA:TextBox Style="z-index: 0" ID="txtNAS_ASSDATE" TabIndex="11" runat="server" Width="184px" BaseType="Character" onkeydown="backspaceFunc('txtNAS_ASSDATE')"
                            MaxLength="200"></SHMA:TextBox>--%></td>
                    <td style="width: 99px; visibility: hidden;" width="99" align="right">Company</td>
                    <td width="186" style="visibility: hidden;">
                        <SHMA:DropDownList ID="ddlNP1_ASSIGNMENTCD" TabIndex="0" runat="server" DataTextField="cdesc" DataValueField="ccd_code" onkeydown="return cancelBack()"
                            Width="184px" AutoPostBack="True" BlankValue="True" CssClass="RequiredField" BackColor="White" Enabled="false">
                        </SHMA:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlNP1_ASSIGNMENTCD" Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr id="rowNP1_TELRES" class="button2TD" align="right">
                    <td style="width: 138px" width="138" align="right">
                        <p>
                            <asp:Label Style="z-index: 0" ID="lblServerError" runat="server" ForeColor="Red" EnableViewState="false"
                                Visible="False"></asp:Label>
                        </p>
                    </td>
                    <td style="text-align: right" colspan="3">
                        <a href="#" class="button2" tabindex="12" onclick="saveUpdate();">&nbsp;&nbsp;Save&nbsp;&nbsp;</a>
                    </td>
                </tr>
                <tr id="rowNP1_OFFICE" class="" style="display: none;">
                    <td style="width: 138px; height: 38px" width="138" align="right">
                        <asp:ImageButton Style="z-index: 0" ID="btn_save" runat="server" Width="64px" Height="16px" ImageUrl="Images/savee.JPG"
                            ImageAlign="Right" Visible="False" OnClick="btn_save_Click"></asp:ImageButton>
                    </td>
                    <td style="height: 38px" width="186">&nbsp;</td>
                    <td style="width: 99px; height: 38px" width="99" align="right">
                        <p>&nbsp;&nbsp;&nbsp;</p>
                    </td>
                    <td style="height: 38px" width="186">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <!--</fieldset>-->
        </div>
        <input id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
        <input id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
        <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
        <input style="width: 0px; display: none;" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
            runat="server">
        <input id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
        <script language="javascript">

        </script>
    </form>
    <script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New') addClicked(); fcStandardFooterFunctionsCall();

       
    </script>
    
</body>
</html>
