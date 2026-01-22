<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>

<%@ Page Language="c#" CodeBehind="shgn_ts_se_tblscreen_ILUS_ET_UC_USERCHANNEL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_UC_USERCHANNEL" %>

<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <meta content="text/html; charset=windows-1252" http-equiv="Content-Type">
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%Response.Write(ace.Ace_General.LoadPageStyle());%>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/PortableSQL.js"></script>
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/JScriptTabular.js"></script>
    <script language="javascript" src="JSFiles/NumberFormat.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/UserChannel.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
        <asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
        totalRecords = <asp:Literal id="_totalRecords" runat="server" EnableViewState="False"></asp:Literal> + 1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>									
    </script>
</head>
<body>
    <UC:EntityHeading ID="EntityHeading" runat="server" ParamValue="User Channel Setup" ParamSource="FixValue"></UC:EntityHeading>
    <form id="myForm" method="post" runat="server">

        <table border="0">
            <tr>
                <td width="15%">Channel &nbsp;</td>
                <td>
                    <shma:DropDownList ID="ddlCCH_CODE_1" class="DropDownField" runat="server" Width="12.0pc" DataValueField="CCH_CODE" DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></shma:DropDownList></td>
            </tr>
            <tr>
                <td width="15%">Channel Detail&nbsp;</td>
                <td>
                    <shma:DropDownList ID="ddlCCD_CODE_1" class="DropDownField" runat="server" Width="12.0pc" DataValueField="CCD_CODE" DataTextField="desc_f" BlankValue="True" CssClass="REQUIREDFIELD"></shma:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="height:280px;overflow-y:scroll">
                           <shma:DataGrid ID="EntryGrid" runat="server" Width="400px" SelectedItemStyle-CssClass="GridSelRow" ShowFooter="<%# ShowFooter %>" AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader">
                        <SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <center>
                                        <asp:CheckBox ID="chkSelectAll" runat="server"></asp:CheckBox></center>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center>
                                        <shma:TextBox ID="lblUSE_USERID" Style="width: 0;display:none" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID") %>' runat="server" BaseType="Character">
                                        </shma:TextBox>
                                        <asp:CompareValidator ID="cfvUSE_USERID" runat="server" ControlToValidate="lblUSE_USERID" Operator="DataTypeCheck"
                                            Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                        <shma:TextBox ID="lblCCH_CODE" Style="width: 0;display:none" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE") %>' runat="server" BaseType="Character">
                                        </shma:TextBox>
                                        <asp:CompareValidator ID="cfvCCH_CODE" runat="server" ControlToValidate="lblCCH_CODE" Operator="DataTypeCheck"
                                            Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                        <shma:TextBox ID="lblCCD_CODE" Style="width: 0;display:none" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE") %>' runat="server" BaseType="Character">
                                        </shma:TextBox>
                                        <asp:CompareValidator ID="cfvCCD_CODE" runat="server" ControlToValidate="lblCCD_CODE" Operator="DataTypeCheck"
                                            Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                        <asp:CheckBox ID="chkDelete" runat="server"></asp:CheckBox></center>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <shma:TextBox ID="lblNewUSE_USERID" Style="width: 0; display: none;" runat="server" BaseType="Character"></shma:TextBox>
                                    <asp:CompareValidator ID="cfvNewUSE_USERID" runat="server" ControlToValidate="lblNewUSE_USERID" Operator="DataTypeCheck"
                                        Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                    <shma:TextBox ID="lblNewCCH_CODE" Style="width: 0; display: none;" runat="server" BaseType="Character"></shma:TextBox>
                                    <asp:CompareValidator ID="cfvNewCCH_CODE" runat="server" ControlToValidate="lblNewCCH_CODE" Operator="DataTypeCheck"
                                        Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                    <shma:TextBox ID="lblNewCCD_CODE" Style="width: 0; display: none;" runat="server" BaseType="Character"></shma:TextBox>
                                    <asp:CompareValidator ID="cfvNewCCD_CODE" runat="server" ControlToValidate="lblNewCCD_CODE" Operator="DataTypeCheck"
                                        Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

                                </FooterTemplate>

                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Branch / Location">
                                <ItemTemplate>
                                    <shma:DropDownList ID="ddlCCS_CODE" class="DropDownField" runat="server" Width="19.0pc" CssClass="REQUIREDFIELD"
                                        BlankValue="True" DataTextField="desc_f" DataValueField="CCS_CODE">
                                    </shma:DropDownList>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <shma:DropDownList ID="ddlNewCCS_CODE" class="DropDownField" runat="server" Width="19.0pc" CssClass="REQUIREDFIELD"
                                        BlankValue="True" DataTextField="desc_f" DataValueField="CCS_CODE">
                                    </shma:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Default">
                                <ItemTemplate>
                                    <shma:DropDownList ID="ddlUCH_DEFAULT" class="DropDownField" runat="server" Width="50px" CssClass="REQUIREDFIELD">
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </shma:DropDownList>
                                    <asp:CompareValidator ID="cfvUCH_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False"
                                        ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlUCH_DEFAULT"></asp:CompareValidator>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <shma:DropDownList ID="ddlNewUCH_DEFAULT" class="DropDownField" runat="server" Width="50px" CssClass="REQUIREDFIELD">
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </shma:DropDownList>
                                    <asp:CompareValidator ID="cfvNewUCH_DEFAULT" runat="server" Display="Dynamic" EnableClientScript="False"
                                        ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNewUCH_DEFAULT"></asp:CompareValidator>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </shma:DataGrid>
                    </div>
                 

                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <a class="button2" onclick="send()" href="#">Save</a> 
                    <a class="button2" onclick="deleteDetail()" href="#">Delete</a>
                <td align="right"></td>
            </tr>
        </table>

        <input value="CCH_CODE,USE_USERID,CCD_CODE,CCS_CODE" type="hidden" name="PkColumns">
        <asp:TextBox Style="z-index: 103; position: absolute; top: 80px; display: none; left: 496px" ID="txtModifiedRows" runat="server" Width="0px" Height="12px"></asp:TextBox>
        <asp:TextBox Style="z-index: 102; position: absolute; top: 208px; display: none; left: 664px" ID="txtOrgCode" runat="server" Width="0px"></asp:TextBox>
        <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<input id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
        <input id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
        <input id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
        <input id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
        <input style="width: 0px; display: none;" id="_CustomEvent" value="Button" type="button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick">
    </form>
    <table border="0" width="100%">
    </table>
    <script language="javascript">
				<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>
        function beforeSave() {
            var counter = 0;
            var YN = '';

            for (j = 1; j <= totalRecords; j++) {
                YN = getTabularFieldByIndex(j, 'UCH_DEFAULT').value;
                if (YN == 'Y')
                    counter++;
            }

            if (counter > 1) {
                alert("Default already marked Yes!");
                return false;
            }
            return true;
        }
        fcStandardFooterFunctionsCall();

    </script>

</body>
</html>
