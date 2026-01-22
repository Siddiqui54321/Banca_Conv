<%@ Page Language="c#" CodeBehind="PendingListForCallCenterApproval.aspx.cs" AutoEventWireup="True" EnableEventValidation="false" Inherits="SHAB.Presentation.PendingListForCallCenterApproval" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <%--	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
    --%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Manual Policy Issuance</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="refresh" content="300" />
    <asp:Literal ID="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
    <link rel="stylesheet" type="text/css" href="Styles/comments.css">
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script src="JSFiles/DatePopUp.js"></script>
    <script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
    <script language="javascript" src="JSFiles/Comments.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/illustration.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
        parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
    </script>

        <%-- --- Imran Code ------%>
        <style type="text/css">
.modal {
  display: none;
  position: fixed;
  z-index: 1000;
  top: 0; left: 0;
  width: 100%; height: 100%;
  background-color: rgba(0,0,0,0.5);
  
  /* Centering */
  /*display: flex;*/
  align-items: center;
  justify-content: center;
}

.modal-content {
  background: #fff;
  padding: 15px;
  width: 90%;        /* make it large */
  height: 80%;       /* taller box */
  max-width: 1200px; /* optional limit */
  border-radius: 10px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.3);
  position: relative;
}
.close-btn {
  position: absolute;
  top: 8px; right: 12px;
  cursor: pointer;
  font-size: 20px;
  font-weight: bold;
}
</style>

        <script type="text/javascript">
                function openModal(url) {
                    document.getElementById("popupFrame2").src = url;
                    document.getElementById("myModal").style.display = "flex";
                }
            function closeModal() {
                document.getElementById("myModal").style.display = "none";
            }
</script>
    <script type="text/javascript">

        window.onload = function () {
            var bankCode = '<%= Session["BankCode"] %>';

            // --- GridView 1 (dGrid) ---
            var grid2 = document.getElementById('<%= dGrid.ClientID %>');
            if (grid2) {
                var dropdowns = grid2.getElementsByTagName("select");

                for (var i = 0; i < dropdowns.length; i++) {
                    var ddl = dropdowns[i];

                    var dGridOptions = [];

                    if (bankCode === "F") {
                        dGridOptions = [
                            { value: ".", text: "." },
                            { value: "Y", text: "CBC Approved" },
                            { value: "N", text: "Discrepant" },
                            { value: "N", text: "Return" },
                            { value: "H", text: "Hold" },
                            { value: "D", text: "Declined" }
                           
                        ];
                    } else {
                        dGridOptions = [
                            { value: ".", text: "." },
                            { value: "Y", text: "Approved" },
                            { value: "N", text: "Return" },
                            { value: "H", text: "Hold" },
                            { value: "D", text: "Declined" }
                        ];
                    }

                    ddl.options.length = 0; // clear existing

                    dGridOptions.forEach(function (item) {
                        var opt = document.createElement("option");
                        opt.value = item.value;
                        opt.text = item.text;
                        ddl.add(opt);
                    });

                    // Default selection = "."
                    ddl.value = ".";
                }
            }

            // --- GridView 2 (BMGrid) ---
            var gridBM = document.getElementById('<%= BMGrid.ClientID %>');
            if (gridBM) {
                var dropdownsBM = gridBM.getElementsByTagName("select");

                for (var i = 0; i < dropdownsBM.length; i++) {
                    var ddlBM = dropdownsBM[i];

                    var bmOptions = [];

                    if (bankCode === "F") {
                        bmOptions = [
                            { value: ".", text: "." },
                            { value: "Y", text: "Approved (Premium Debited)" },
                            { value: "N", text: "Declined (Transaction Declined)" }
                        ];
                    } else {
                        bmOptions = [
                            { value: ".", text: "." },
                            { value: "Y", text: "Collected" },
                            { value: "N", text: "UnCollected" }
                        ];
                    }

                    ddlBM.options.length = 0; // clear existing

                    bmOptions.forEach(function (item) {
                        var opt = document.createElement("option");
                        opt.value = item.value;
                        opt.text = item.text;
                        ddlBM.add(opt);
                    });

                    // Default selection
                    ddlBM.value = "."; // always start with dot
                }
            }
        };


</script>


</head>
<body ms_positioning="GridLayout">

    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager ID="sm_1" runat="server"></asp:ScriptManager>
        <input type="text" value="" id="txtNP1_PROPOSAL" style="display: none" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr class="form_heading">
                <td height="20" colspan="4" valign="middle" align="center">Call Center Proposals Approved for Collection</td>
            </tr>
            <tr>
                <td></td>
                <asp:HyperLink ID="hlbanca" Visible="False" CssClass="text_font" NavigateUrl="#" Text="Click Here to Download Last Generated File." runat="server" />
            </tr>

            <tr>
                <td align="center">
                    <asp:DataGrid ID="dGrid" runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
                        AutoGenerateColumns="False" CssClass="text_font" Width="120%" OnItemDataBound="dGrid_ItemDataBound1">
                        <SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
                        <ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
                            HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
                            HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Status" Visible="True">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="4.0pc" CssClass="text_font">
                                        <asp:ListItem Value=".">&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Y">Approved</asp:ListItem>
                                        <asp:ListItem Value="N">Returned</asp:ListItem>
                                        <asp:ListItem Value="H">Hold</asp:ListItem>
                                        <asp:ListItem Value="D">Declined</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Proposal">
                                <ItemTemplate>
                                    <!--<a onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PROFILE');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>-->
                                   
                                 <%--   <a onclick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('DDAFORM');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>--%>

                                <a 
            onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'); 
                     executeReport('<%# (Session["BankCode"] != null && Session["BankCode"].ToString() == "F") ? "DDAFORM" : "PDILLUS" %>');" 
            class="text_font" 
            href="#">
            <%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>
        </a>

                                    
                                    
                                    
                                    <asp:Label ID="lblProposal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>' Visible="False">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Proposal Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblProposalDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.np1_propdate")).ToString("dd/MM/yyyy") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Proposal Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblPropTime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.use_datetime")).ToString("hh:mm:ss tt") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.branch")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Last Action Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cm_commentdate")).ToString("dd/MM/yyyy") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Last Action Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionTime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cm_commentdate")).ToString("hh:mm:ss tt") %>'>
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
                                    <asp:Label ID="lblAccount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_accountno") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CNIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCNic" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_idno") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nad_mobile") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Comments [max.250 characters]" Visible="True" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComments" CssClass="text_font" Width="10pc" runat="server" Text='' MaxLength="250">
                                    </asp:TextBox>
                                    <asp:CustomValidator ID="txtSubAnswerVLD"
                                        runat="server"
                                        CssClass="commentValidator"
                                        ClientValidationFunction="isCommentGiven"
                                        ErrorMessage="Please enter either comment"
                                        ToolTip="Please enter answer or either select 'No'."> Required </asp:CustomValidator>

                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <img src="Images/icon-comment.png" width="16" onmouseover="showComments(<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>);" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <img src="Images/icon-info.png" width="16" onclick="showInfo();" />
                                </ItemTemplate>
                            </asp:TemplateColumn>


                            <%--New for Policy Information--%>
                                <asp:TemplateColumn>
									<ItemTemplate>
									<img alt="" src="Images/Detail_Icon.png" width="16" title="Click for show more details" onclick="openModal('ProposalDetails.aspx?id=<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');" />
									 
                                    </ItemTemplate>
								</asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <div style="display: none; margin-left: 120%" class="divComments" id='<%# DataBinder.Eval(Container.DataItem, "NP1_PROPOSAL") %>'>
                                        <asp:Repeater ID='repAllComments' runat="server">
                                            <HeaderTemplate>
                                                <table class="text_font">
                                                    <tr class='CommentGridHeading'>
                                                        <td>Comment By
                                                        </td>
                                                        <td>Action
                                                        </td>
                                                        <td class="tdComment">Comment
                                                        </td>
                                                        <td>Date
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class='CommentItemStyle'>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTBY") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_ACTION") %>
															
                                                    </td>
                                                    <td class="tdComment">
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTS") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTDATE") %>															
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class='CommentItemStyleAlt'>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTBY") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_ACTION") %>
															
                                                    </td>
                                                    <td class="tdComment">
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTS") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTDATE") %>															
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </Table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

                    <asp:DataGrid ID="BMGrid" runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
                        AutoGenerateColumns="False" CssClass="text_font" Width="100%" OnItemDataBound="BMGrid_ItemDataBound">
                        <SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
                        <ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
                            HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
                            HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Status" Visible="True">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="4.0pc" CssClass="text_font">
                                        <asp:ListItem Value=".">&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Y">Collected</asp:ListItem>
                                        <asp:ListItem Value="N">UnCollected</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Proposal">
                                <ItemTemplate>
                                    <!--<a onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PROFILE');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>-->
                                    <a onclick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PDILLUS');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>
                                    <asp:Label ID="lblProposal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>' Visible="False">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Proposal Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPropDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.np1_propdate")).ToString("dd/MM/yyyy") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_fullname") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CNIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCNic" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_idno") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nad_mobile") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total Premium">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotPremium" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_TOTDIFPREM") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Collection Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCollectionAmount" runat="server" Width="4.0pc" CssClass="text_font">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Collection Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_collection_Date" placeholder="DD/MM/YYYY" runat="server" Width="5.0pc" CssClass="text_font">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Comments [max.250 characters]" Visible="True" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComments" CssClass="text_font" Width="10pc" runat="server" Text='' MaxLength="250">
                                    </asp:TextBox>
                                    <asp:CustomValidator ID="txtSubAnswerVLD"
                                        runat="server"
                                        CssClass="commentValidator"
                                        ClientValidationFunction="isCommentGiven"
                                        ErrorMessage="Please enter either comment"
                                        ToolTip="Please enter answer or either select 'No'."> Required </asp:CustomValidator>

                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <img src="Images/icon-comment.png" width="16" onmouseover="showComments(<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>);" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <img src="Images/icon-info.png" width="16" onclick="showInfo();" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                              <%--New for Policy Information--%>
                                <asp:TemplateColumn>
									<ItemTemplate>
									<img alt="" src="Images/Detail_Icon.png" width="16" title="Click for show more details" onclick="openModal('ProposalDetails.aspx?id=<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');" />
									 
                                    </ItemTemplate>
								</asp:TemplateColumn>


                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <div style="display: none;margin-left:80%" class="divComments" id='<%# DataBinder.Eval(Container.DataItem, "NP1_PROPOSAL") %>'>
                                        <asp:Repeater ID='repAllComments' runat="server">
                                            <HeaderTemplate>
                                                <table class="text_font">
                                                    <tr class='CommentGridHeading'>
                                                        <td>Comment By
                                                        </td>
                                                        <td>Action
                                                        </td>
                                                        <td class="tdComment">Comment
                                                        </td>
                                                        <td>Date
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class='CommentItemStyle'>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTBY") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_ACTION") %>
															
                                                    </td>
                                                    <td class="tdComment">
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTS") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTDATE") %>															
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class='CommentItemStyleAlt'>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTBY") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_ACTION") %>
															
                                                    </td>
                                                    <td class="tdComment">
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTS") %>
															
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "CM_COMMENTDATE") %>															
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </Table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr style="display: none;">
                <td align="center">
                    <asp:DataGrid ID="dGrid2" runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid" AutoGenerateColumns="False" Width="90%">
                        <SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
                        <ItemStyle VerticalAlign="Bottom" HorizontalAlign="Center" Font-Names="Arial" Height="20px" Font-Size="10"></ItemStyle>
                        <HeaderStyle Font-Names="Arial" Height="20px" ForeColor="Black" HorizontalAlign="Center" Font-Bold="True" Font-Size="9"></HeaderStyle>
                        <Columns>

                            <asp:TemplateColumn HeaderText="Transaction Identifier" ItemStyle-Width="192">
                                <ItemTemplate>
                                    <asp:Label ID="Label0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRANS_ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From Branch Code" ItemStyle-Width="113">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_BRANCH_CODE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From Account No" ItemStyle-Width="105">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_TYPE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From Account Type" ItemStyle-Width="117">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_NO") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From CLSL" ItemStyle-Width="69">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_CLSL") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From Account Currency" ItemStyle-Width="143">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_CURR") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Branch Code" ItemStyle-Width="98">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_BRANCH_CODE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Account No" ItemStyle-Width="89">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_NO") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Account Type" ItemStyle-Width="103">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_TYPE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Account Currency" ItemStyle-Width="128">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_CURR") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To CLSL" ItemStyle-Width="64">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CLSL") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount" ItemStyle-Width="64">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Transaction Currency" ItemStyle-Width="132">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRANS_CURR") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Value Date" ItemStyle-Width="68">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VAL_DATE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Narration" ItemStyle-Width="386">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USER_NAR") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Instrument No" ItemStyle-Width="87">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INST_NO") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="From CRC" ItemStyle-Width="71">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_CRC") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To CRC" ItemStyle-Width="70">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CRC") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Wht Flag" ItemStyle-Width="56">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WHT_FLG") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tran_code" ItemStyle-Width="68">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRAN_CODE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnSave" runat="server" Visible="False" Text="Save"></asp:Button>
        <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
        <input id="_CustomEvent" style="width: 0px; visibility: hidden" type="button" value="Button" name="_CustomEvent"
            runat="server" onserverclick="_CustomEvent_ServerClick">
         <div id="divInfo" class="form_heading_2 floatingDiv" style="margin-left: 70%;width:50%">
                        <table>
                            <tr>
                                <div width="100%">
                                    <input type="button" value="Close" onclick="hideInfo();" class="closeButton"></input>
                                </div>
                                <div width="100%">
                                    <table class="text_font">
                                        <tr class='CommentGridHeading'>

                                            <td>Submitted Documents
                                            </td>
                                        </tr>
                                        <tr class='CommentItemStyle'>

                                            <td>Customer Signed Application.
                                            </td>
                                        </tr>
                                        <tr class='CommentItemStyleAlt'>

                                            <td>Customer Signed Standing Order.
                                            </td>
                                        </tr>
                                        <tr class='CommentItemStyle'>

                                            <td>Customer Signed Disclaimer Form.
                                            </td>
                                        </tr>
                                        <tr class='CommentItemStyleAlt'>

                                            <td>Customer Signed Copy of Acceptance / Requirement (which ever is applicable) Letter.
                                            </td>
                                        </tr>
                                        <tr class='CommentItemStyle'>

                                            <td>Valid CNIC Copy.
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </tr>
                        </table>
                    </div>
    </form>
    <script language="javascript">     
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
			<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>

        //Problem: Validator Occupy Space in Grid \
        //Solution: Hide before Validation Fired and Show when Validation false
        $('.commentValidator').hide();

        function setValue(value) {
            document.getElementById("txtNP1_PROPOSAL").value = value;
           // alert(value);
        }
        function saveUpdate() {
            if (document.getElementById('dGrid') != null || document.getElementById('BMGrid') != null) {
                parent.closeWait();
                document.getElementById("_CustomEventVal").value = "Save";
                __doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
            }
            else {
                parent.closeWait();
                alert('There is no pending proposal to post.');
            }
        }
        function RefreshClick() {
            document.getElementById("_CustomEventVal").value = "Process";
            __doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
        }
        function showComments(porposal) {
            hideComments();
            var divId = porposal;
            document.getElementById(divId).style.display = '';//('display', '');
        }
        function hideComments() {
            $('.divComments').css('display', 'none');
        }

        function DownloadFile(FilePath) {
            var r = confirm("Make sure that this file is not downloaded before for payment deduction.. Continue Downloading?");
            if (r == true) {
                window.location.replace("UploadedFiles/" + FilePath + "");
            }
            else {
                //alert("You pressed Cancel!");
            }
        }
    </script>

       <%-- --- new style by Imran-----%>

       
<div id="myModal" class="modal">
  <div class="modal-content">
    <span class="close-btn" onclick="closeModal()">✖</span>
    <iframe id="popupFrame2" src="" width="100%" height="95%" frameborder="0"></iframe>
  </div>
</div>
   
</body>
</html>
