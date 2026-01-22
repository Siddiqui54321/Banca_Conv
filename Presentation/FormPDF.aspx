<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPDF.aspx.cs" Inherits="Bancassurance.Presentation.FormPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

 




    <title>PDF Forms download</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            width: 73px;
        }
        .auto-style2 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
             color: #FFFFFF;
             font-weight: bold;
             font-size: 13px;
             vertical-align: middle;
            background-color: #336699;
            text-align:center;
        }

        .subheading {
            font-family: Verdana, Arial, Helvetica, sans-serif;
             color: #FFFFFF;
             font-weight: bold;
              font-size: 12px;
             background-color: #6699cc;
            text-align:center;
        }
        .content {
    font-family: Verdana, Arial, Helvetica, sans-serif;
    font-size: 11px;
}
       
        .auto-style3 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #FFFFFF;
            font-weight: bold;
            font-size: 12px;
            background-color: #6699cc;
            text-align: left;
        }
       
        .auto-style4 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            height: 15px;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlBOP" runat="server" Visible="false">
            <table cellpadding="0" cellspacing="0" width="100%" border="1">
                <tr class="form_heading">
                    <td colspan="3" align="center" class="auto-style2"  >
                        <asp:Label ID="Label4" runat="server" Text="PDF Forms"></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td class="subheading" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="Serial No" style="font-weight: 700"></asp:Label>
                    </td>
                    <td class="auto-style3" colspan="2">
                        <asp:Label ID="Label2" runat="server" Text="Form Title" style="font-weight: 700"></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td style="text-align: center" class="content">1</td>
                    <td class="content">AIB Claim Application English1</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Presentation/FormPdf/AIB Claim Application English1.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td style="text-align: center" class="content">2</td>
                    <td class="content">AIB Claim Form B English1</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Presentation/FormPdf/AIB Claim Form B English1.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="content" style="text-align: center">3</td>
                    <td class="content">Alteration Form - Final</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Presentation/FormPdf/Alteration Form - Final.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="content" style="text-align: center">4</td>
                    <td class="content">Change-In-Contact-Details-Correspondence-Address</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Presentation/FormPdf/CHANGE-IN-CONTACT-DETAILS-CORRESPONDENCE-ADDRESS.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="content" style="text-align: center">5</td>
                    <td class="content"><%--Check-List-Form (Death Claim)--%>
                        Death-Claim-Form-A-English

                    </td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Presentation/FormPdf/Death-Claim-Form-A-English.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="content" style="text-align: center">6</td>
                    <td class="content">Death-Claim-Form-B-English</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Presentation/FormPdf/Death-Claim-Form-B-English.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="content" style="text-align: center">7</td>
                    <td class="content">Death-Claim-Form-C-English</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Presentation/FormPdf/Death-Claim-Form-C-English.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="content" style="text-align: center">8</td>
                    <td class="content">Death-Claim-Form-D-English</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Presentation/FormPdf/Death-Claim-Form-D-English.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style4" style="text-align: center">9</td>
                    <td class="auto-style4">Condolence-and-Requirement-letter</td>
                    <td class="auto-style4">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Presentation/FormPdf/Condolence-and-Requirement-letter.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="auto-style4" style="text-align: center">10</td>
                    <td class="auto-style4"><%--Death Claim Application English1--%>Policyloan-New</td>
                    <td class="auto-style4">
                        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Presentation/FormPdf/policyloan-New.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="content" style="text-align: center">11</td>
                    <td class="content">CZ-50 updated Format</td>
                    <td class="content">
                        <%--<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Presentation/FormPdf/Death Claim Application English1.pdf" Target="_blank">Download</asp:HyperLink>--%>
                        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Presentation/FormPdf/CZ-50 updated Format.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>


                </tr>

                <tr>
                    <td class="auto-style4" style="text-align: center">12</td>
                    <td class="auto-style4">Newspaper Specimen</td>
                    <td class="auto-style4">
                        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/Presentation/FormPdf/Newspaper Specimen.jpg" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>

                <tr>
                    <td class="content" style="text-align: center">13</td>
                    <td class="content">Indemnity Bond for Duplicate Policy</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/Presentation/FormPdf/Indemnity Bond for Duplicate Policy.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="content" style="text-align: center">14</td>
                    <td class="content">Indemnity Form for Surrender</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/Presentation/FormPdf/Indemnity Form for Surrender.pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="content" style="text-align: center">15</td>
                    <td class="content">Declaration of Good Health (New Format)</td>
                    <td class="content">
                        <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/Presentation/FormPdf/Declaration of Good Health (New Format).pdf" Target="_blank">Download</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

            </table>

                </asp:Panel>

        </div>
        <p>
                        <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#333300"></asp:Label>
                    </p>
    </form>
</body>
</html>
