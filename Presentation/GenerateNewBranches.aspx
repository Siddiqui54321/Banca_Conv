<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateNewBranches.aspx.cs" Inherits="Bancassurance.Presentation.GenerateNewBranches" EnableEventValidation="false" %>

<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register Assembly="Enterprise" Namespace="SHMA.Enterprise.Presentation.WebControls" TagPrefix="SHMA" %>
<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Generate New Branches</title>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <asp:Literal ID="CSSLiteral" runat="server" EnableViewState="true"></asp:Literal>
    <link type="text/css" href="Styles/MenuStyle.css" rel="stylesheet" />
    <link type="text/css" href="Styles/MainPage.css" rel="stylesheet" />
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
    <script language="javascript" src="JSFiles/Comments.js"></script>
    <script language="javascript" language="javascript" src="../shmalib/jscript/Illustration.js"></script>
    <script src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
        parent.closeWait();
        <asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
    </script>
    <script language="javascript" type="text/javascript">
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=200,width=150');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
    <script type="text/javascript"> /*Only Number allowed*/
        window.onload = function () {
            document.getElementById('<%=txtBranchCode.ClientID %>').onkeydown = function (event) {
                if (event.key >= '0' && event.key <= '9') {
                    return true;
                }
                if (event.key === 'Backspace' || event.key === 'Delete' ||
                    event.key === 'ArrowLeft' || event.key === 'ArrowRight' ||
                    event.key === 'Tab') {
                    return true;
                }

                event.preventDefault();

            }
        };
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

        function backspaceFunc1(e) {
            var key = event.keyCode;
            if (e.keychar == '\b') {
                var str = document.getElementById(e).value;
                var newStr = str.substring(0, str.length - 1);
                document.getElementById(e).value = newStr;
            }
        }

        function checkNumeric(e) {
            //Only characters  >>>> var regex=/^[a-zA-Z];
            //Only Numbers     >>>> var regex=/^[0-9];
            //Only HEX Numbers >>>> var regex=/^[0-9a-fA-F];

            var regex = /^[0-9]/;
            var keynum;
            var keychar;

            if (window.event) // IE
            {
                keynum = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                keynum = e.which;
            }

            keychar = String.fromCharCode(keynum);
            return regex.test(keychar);
        }

    </script>
    <style>
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
    <script type="text/javascript">
        function fillTextboxes(BRCode, BRName) {
            document.getElementById("txtBranchCode").value = BRCode;
            document.getElementById("txtBranchName").value = BRName;
        }

    </script>
</head>

<body>
    <div>
        <UC:EntityHeading ID="EntityHeading1" runat="server" ParamValue="Generate New Branches"
            ParamSource="FixValue"></UC:EntityHeading>
        <form id="form1" runat="server">
            <asp:Label ID="lblAlert" runat="server" Text="" Visible="true"></asp:Label>
            <div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server" style="z-index: 0">
                <table id="Table1" align="center" border="0" cellspacing="0" cellpadding="2">
                    <tr class="form_heading">
                        <td height="20" width="750" colspan="12" valign="middle" align="center">GENERATE NEW BRANCHES
                        </td>
                    </tr>
                    <tr>
                        <td height="15" colspan="6"></td>
                    </tr>
                    <tr>
                        <td style="width: 138px; height: 22px" width="138" align="right">Channel:
                        </td>

                        <td id="ctlddlCCH_CHANNELCD" width="186">
                            <!--hyjack-->
                            <!-- <a href="#dialog" name="modal">Simple Window Modal</a>   CssClass="RequiredField" -->
                            <SHMA:DropDownList TabIndex="1" BlankValue="False" runat="server" ID="ddlCCH_CHANNELCD"
                                onkeydown="return cancelBack(0)" Width="160px" DataValueField="CCH_CODE"
                                DataTextField="desc_f">
                            </SHMA:DropDownList>
                        </td>

                        <td style="width: 138px; height: 22px" width="138" align="right">Sub Channel:
                        </td>
                        <td>
                            <SHMA:DropDownList TabIndex="2" runat="server" ID="ddlCCD_CHANNELDTLCD" CssClass="RequiredField"
                                onkeydown="return cancelBack(0)" Width="160px" DataValueField="CCD_CODE"
                                DataTextField="desc_f">
                                <%--BlankValue="True" --%>
                            </SHMA:DropDownList>
                        </td>
                    </tr>
                    <tr class="TRow_Alt">
                        <td width="210" align="right" id="TD3">Branch Code:
                        </td>
                        <td width="186" id="ctltxtBranchCode">
                            <SHMA:TextBox ID="txtBranchCode" TabIndex="3" runat="server" Width="174px"
                                onkeydown="backspaceFunc('txtBranchCode')" MaxLength="4" ReadOnly="false"></SHMA:TextBox>
                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtBranchCode"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td width="106" align="right" id="TD4">Branch Name:                        </td>
                        <td width="186" id="ctltxtBranchName">
                            <SHMA:TextBox ID="txtBranchName" TabIndex="4" runat="server" Width="150px"
                                onkeydown="backspaceFunc1('txtBranchName')" MaxLength="130" ReadOnly="false"></SHMA:TextBox>
                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="txtBranchName"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <%--<tr class="TRow_Alt">
                        <td width="210" align="right" id="TD3">Agency Code:
                        </td>
                        <td width="186" id="ctltxtAgencyCode">
                            <SHMA:TextBox ID="txtAgencyCode" TabIndex="5" runat="server" Width="174px"
                                onkeydown="backspaceFunc('txtAgencyCode')" MaxLength="3" ReadOnly="false" ></SHMA:TextBox>
                        </td>
                    </tr>--%>
                </table>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" CssClass="button2" runat="server" Text=" Add " Visible="true" Enabled="true" OnClick="btnSave_Click" />
                &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" CssClass="button2" runat="server" Text="Search" OnClick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" CssClass="button2" runat="server" Text="Edit" OnClick="btnUpdate_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" CssClass="button2" runat="server" Text="Export" OnClick="btnExport_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUploadDisp" CssClass="button2" runat="server" Text="Upload" OnClick="btnUploadDisp_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMapp" CssClass="button2" runat="server" Text="Br Mapping" OnClick="btnMapp_Click" />
                &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnRef" CssClass="button2" runat="server" Text="::" OnClick="btnRef_Click" />
                <div style="margin-left: 200px;" class="auto-style2">
                    <td>
                        <br />
                        <asp:Label ID="Label2" runat="server" Visible="False" Text="Choose Excel file for upload"></asp:Label>
                        &nbsp;<asp:FileUpload ID="FileUpload1" Visible="False" runat="server" />
                        <asp:Button ID="btnUpload" class="button2" runat="server" Visible="False" Text="Upload Data" OnClick="btnUpload_Click" />
                    </td>
                </div>
                <asp:Panel ID="BrPanel" runat="server" Visible="false">
                    <div style="height: 250px; overflow: auto">
                        <td style="height: 38px" width="186">&nbsp;
                        </td>
                        <div style="margin-left: 110px;" class="auto-style3">
                            <asp:GridView ID="grdBranchDtl" runat="server" OnSelectedIndexChanged="grdBranchDtl_SelectedIndexChanged" OnRowDataBound="grdBranchDtl_RowDataBound"
                                Height="70px" Width="697px" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="CCS_FIELD1" HeaderText="Branch Code" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="CCS_DESCR" HeaderText="Branch Name" ItemStyle-Width="225px" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="BSOUSER" HeaderText="BSO User" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="BMUSER" HeaderText="BM User" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>
                <asp:Panel ID="pnlBrMap" runat="server" Visible="false">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBRMap" runat="server" Text="User_ID: "></asp:Label>
                    <asp:TextBox ID="txtBRMap" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBRMap" runat="server" Text="Submit" OnClick="btnBRMap_Click" />
                    <br />
                    <div style="height: 250px; overflow: auto">
                        <td style="height: 38px" width="186">&nbsp;
                        </td>
                        <div style="margin-left: 110px;" class="auto-style3">
                            <asp:GridView ID="brMapGrid" runat="server" Height="70px" Width="697px" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="userid" HeaderText="User ID" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Br_code" HeaderText="Br. Code" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Br_Name" HeaderText="Br. Name" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <%--                <asp:Panel ID="pnlMapping" runat="server" Visible="false">
                    <div style="height: 250px; overflow: auto">
                        <td style="height: 38px" width="186">&nbsp;
                        </td>
                        <div style="margin-left: 110px;" class="auto-style3">
                            <asp:Label ID="lblMapInput" runat="server" Text="Enter Value:" AssociatedControlID="txtMappingInput"></asp:Label>
                            <asp:TextBox ID="txtMappingInput" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSubmitMapping" runat="server" Text="Submit" OnClick="btnSubmitMapping_Click" />

                            <asp:GridView ID="GMapping" runat="server"></asp:GridView>
                            <columns>
                                <asp:BoundField DataField="CCS_FIELD1" HeaderText="User ID" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="CCS_DESCR" HeaderText="Bank Name" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField DataField="CCS_CODE" HeaderText="Br. Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="CCD_CODE" HeaderText="Br. Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            </columns>
                            <editrowstyle backcolor="#999999" />
                            <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                            <headerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                            <pagerstyle backcolor="#284775" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#F7F6F3" forecolor="#333333" />
                            <selectedrowstyle backcolor="#E2DED6" font-bold="True" forecolor="#333333" />
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </div>
                    </div>
                </asp:Panel>--%>

                <%--                <tr>
                    <td height="15" colspan="6"></td>
                </tr>
                <tr>
                    <td style="width: 138px; height: 22px" width="138" align="right">Total Records:
                    </td>
                    <td><asp:Label ID="lblTotrecord" runat="server" Text=""></asp:Label></td>--%>
            </div>
        </form>
    </div>

</body>
</html>
