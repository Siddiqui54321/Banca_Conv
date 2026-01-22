<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManualStaffChannelIssuance.aspx.cs" Inherits="Bancassurance.Presentation.ManualStaffChannelIssuance" EnableEventValidation="false" %>

<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls"
    Assembly="Enterprise" %>
<%@ Register Assembly="Enterprise" Namespace="SHMA.Enterprise.Presentation.WebControls"
    TagPrefix="SHMA" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Manual Policy Issuance</title>
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
    <script language="javascript" src="../shmalib/jscript/Illustration.js"></script>
    <script language="javascript" src="../shmalib/jscript/WebUIValidation.js"></script>
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

        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Only Numbers Allow");
                return false;
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
</head>
<body>
    <div>
        <UC:EntityHeading ID="EntityHeading1" runat="server" ParamValue="Staff Channel Issuance"
            ParamSource="FixValue"></UC:EntityHeading>
        <form id="Form2" runat="server">
            <asp:Label ID="lblAlert" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblArert2" runat="server" Text="" Visible="true" ForeColor="Green"></asp:Label>



            <div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server" style="z-index: 0">
                <table id="Table1" align="center" border="0" cellspacing="0" cellpadding="2">
                    <tr class="form_heading">
                        <td height="20" width="750" colspan="12" valign="middle" align="center">STAFF CHANNEL MAPPING     <%--valign="middle" align="center"--%>                   
                        </td>
                    </tr>
                    <tr>
                        <td height="10" colspan="6"></td>
                    </tr>
                    <%--<br />--%>


                    <tr id="rowNPH_TITLE" class="TRow_Normal">
                        <td id="lbl_Channel" width="110" align="right">Channel:        
                        </td>

                        <td id="ctlddlCCH_CHANNELCD" style="height: 23px" width="186">
                            <SHMA:DropDownList ID="ddlCCH_CHANNELCD" TabIndex="1" runat="server" BlankValue="False"
                                onkeydown="return cancelBack(0)" Width="184px" Onchange="Title_ChangeEvent(this);"
                                DataValueField="CCH_CODE" DataTextField="desc_f" Font-Italic="False">
                            </SHMA:DropDownList>
                        </td>

                        <td id="ctlSubChannel" width="110" align="left">Sub Channel:    
                        </td>
                        <td id="ctlddlCCD_CHANNELDTLCD" width="186">
                            <SHMA:DropDownList TabIndex="2" BlankValue="False" runat="server" ID="ddlCCD_CHANNELDTLCD"
                                onkeydown="return cancelBack(0)" Width="160px" DataValueField="CCD_CODE"
                                DataTextField="desc_f">
                            </SHMA:DropDownList>
                        </td>
                    </tr>


                    <tr class="TRow_Alt">
                        <%--id="lblStaffID" --%>

                        <td width="106" align="right" id="TD3">Staff ID:
                        </td>
                        <td width="186" id="clttxtStaffID">
                            <SHMA:TextBox ID="txtStaffID" TabIndex="3" runat="server" onkeypress="return functionx(event)" Width="184px"
                                onkeydown="backspaceFunc('txtStaffID')" MaxLength="6" ReadOnly="false" CssClass="RequiredField"></SHMA:TextBox>
                        </td>
                        <td id="ctlName" width="110" align="left">Staff Name:
                        </td>
                        <td width="186" id="ctltxtStaffName">
                            <SHMA:TextBox ID="txtStaffName" TabIndex="4" runat="server" Width="160px"
                                onkeydown="backspaceFunc('txtStaffName')" MaxLength="30" ReadOnly="false"
                                CssClass="RequiredField"></SHMA:TextBox>
                        </td>
                    </tr>

                </table>
                <tr>
                    <td>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                        <asp:Button ID="btnSave" class="button2" runat="server" Text="Save" Visible="True" OnClick="btnSave_Click" />
                    </td>
                    <td>&nbsp;<asp:Button ID="btnSearch" class="button2" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                    <td>&nbsp;<asp:Button ID="Update" class="button2" runat="server" Text="Update" OnClick="Update_Click" />
                    </td>
                    <td>&nbsp;<asp:Button ID="btnExport" class="button2" runat="server" Text="Export" OnClick="btnExport_Click" />
                    </td>
                    <td>&nbsp;<asp:Button ID="btnUploadDisp" class="button2" runat="server" Text="Upload" OnClick="btnUploadDisp_Click" />
                    </td>
                    <td>&nbsp;<asp:Button ID="Refresh" class="button2" runat="server" Text="::" OnClick="Refresh_Click" />
                    </td>
                    </tr>
                <div style="margin-left: 200px;" class="auto-style2">
                        <td>
                            <br />
                            <asp:Label ID="Label2" runat="server" Visible="False" Text="Choose Excel file for upload"></asp:Label>
                            &nbsp;<asp:FileUpload ID="FileUpload1" Visible="False" runat="server" />
                            <asp:Button ID="btnUpload" class="button2" runat="server" Visible="False" Text="Upload Data" OnClick="btnUpload_Click" />
                        </td>                    
                </div>

                <div style="height: 250px; overflow: auto">
                    <td style="height: 38px" width="186">&nbsp;
                    </td>
                    <div style="margin-left: 110px; overflow: auto;">
                        <%-- class="auto-style3"--%>

                        <%--200px--%>
                        <asp:GridView ID="grdStaffChMap" runat="server" Height="70px" OnSelectedIndexChanged="grdStaffChMap_SelectedIndexChanged"
                            Width="697px" CellPadding="4" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False"
                            OnRowDataBound="grdStaffChMap_RowDataBound">

                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="staff_id" HeaderText="Staff ID" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="staff_name" HeaderText="Staff Name" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField DataField="Ccd_Descr" HeaderText="Channel Detail" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
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
                        <br />
                        <br />
                        <br />

                    </div>
                </div>





                <%--                <tr>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <td>
                        <asp:Button ID="btnUpload" class="button2" runat="server" visible="false" Text="Upload Data" OnClick="btnUpload_Click" />
                    </td>

                    <td>&nbsp;<asp:FileUpload ID="FileUpload1" Visible="false" runat="server" /></td>
                </tr>--%>



                <asp:GridView ID="GridView1" runat="server" Visible="False">
                </asp:GridView>

                <%--                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </td>
                </tr>--%>

                <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>


            </div>

        </form>

    </div>
</body>
</html>
