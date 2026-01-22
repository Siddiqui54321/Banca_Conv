<%@ Page Language="c#" CodeBehind="ListOfPendingAtCBCProposal.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.ListOfPendingAtCBCProposal" %>

<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <title>Manual Policy Issuance</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <asp:Literal ID="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
    <link rel="stylesheet" type="text/css" href="Styles/style.css">
    <link rel="stylesheet" type="text/css" href="Presentation/Styles/MainPage.css">
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
    <script language="javascript" src="JSFiles/Comments.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/illustration.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
        parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input type="text" value="" id="txtNP1_PROPOSAL" style="display: none" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:LinkButton ID="hlbanca" OnClick="hlbanca_Click" Visible="true" CssClass="text_font" NavigateUrl="#" Text="Click Here to Download." runat="server" />
                </td>
            </tr>
            <tr class="form_heading">
                <td height="20" colspan="4" valign="middle" align="center">Proposal List
                </td>
            </tr>
            <!--<tr>
					<td>					
						<input type="file" name="FileToUpload" id="FileToUpload" runat="server" class="text_font" size="46" > 
						<asp:RequiredFieldValidator id="UploadValidator" runat="server" CssClass="text_font" ErrorMessage="You must select an csv file to upload"
							ControlToValidate="FileToUpload"></asp:RequiredFieldValidator>										
					</td>
				</tr-->

            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:48%">
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <shma:DatePopUp Style="z-index: 0" ID="txtDATEFROM" TabIndex="1" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
                                    ImageUrl="Images/image1.jpg" Width="5.0pc">
                                </shma:DatePopUp>
                                &nbsp;<asp:CompareValidator Style="z-index: 0" ID="cfvDATETO" runat="server" ErrorMessage="Date Format is Incorrect "
                                    ControlToValidate="txtDATETO" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                            </td>
                            <td>
                                 To</td>
                            <td>
                                     <shma:DatePopUp Style="z-index: 0" ID="txtDATETO" TabIndex="2" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
                                    ImageUrl="Images/image1.jpg" Width="5.0pc">
                                </shma:DatePopUp>
                                &nbsp;<asp:CompareValidator Style="z-index: 0" ID="cfvDATEFROM" runat="server" ErrorMessage="Date Format is Incorrect "
                                    ControlToValidate="txtDATEFROM" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btn_getData" Text="Search" OnClick="btn_getData_Click1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>


            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <%--<div style="overflow-y: scroll; height: 380px">--%>
                    <div style="width: 800px; height: 300px; overflow-y:auto; overflow-x:auto">   
                        


                        <asp:DataGrid ID="dGrid" runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
                            AutoGenerateColumns="False" CssClass="text_font" Width="100%">
                            <SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
                            <ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
                                HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
                                HorizontalAlign="Center"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Proposal">
                                    <ItemTemplate>
                                         <asp:Label ID="lblProposal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Proposal Date">
                                    <ItemTemplate>
                                         <asp:Label ID="lblPropdate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPDATE") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Proposal Time">
                                    <ItemTemplate>
                                         <asp:Label ID="lblPropdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.use_datetime")).ToString("hh:mm:ss tt")  %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Plan">
                                    <ItemTemplate>
                                         <asp:Label ID="lblPlan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CNIC">
                                    <ItemTemplate>
                                         <asp:Label ID="lblcnic" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NPH_IDNO") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_fullname") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Account No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCNic" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_accountno") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nad_mobile") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                  <asp:TemplateColumn HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.branch") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CBCStatus") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last Action Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cm_commentdate")).ToString("MM/dd/yy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last Action Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cm_commentdate")).ToString("hh:mm:ss tt") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Commented By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_commentby") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_comments") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>


                    </div>
            
                    </td>
            </tr>
        </table>
        <!--<asp:Button ID="btnSave" Runat="server" Visible="False" Text="Save"></asp:Button>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server">-->
    </form>
    <script language="javascript">     
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
        function setValue(value) {
            document.getElementById("txtNP1_PROPOSAL").value = value;
        }
        function Page_ClientValidate() {
            return true;
        }
        function saveUpdate() {
            //alert(1);
            //alert(document.getElementById('dGrid'));
            //if(document.getElementById('dGrid')==null)
            //{	
            document.getElementById("_CustomEventVal").value = "Save";
            __doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
            //}
            //else
            //{
            //	parent.closeWait();
            //alert('There is no pending proposal to post.');
            //}
        }
        function showComments(porposal) {
            hideComments();
            var divId = '#' + porposal;
            $(divId).css('display', '');
        }
        function hideComments() {
            $('.divComments').css('display', 'none');
        }
    </script>
</body>
</html>
