<%@ Page Language="c#" CodeBehind="ValidationError.aspx.cs" AutoEventWireup="True"
    Inherits="Bancassurance.Presentation.ValidationError" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <title>Validation Error!</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%Response.Write(ace.Ace_General.LoadPageStyle());%>
</head>
<body class="ErrorPage_bg">
    <form id="Form1" method="post" runat="server">
    <table id="tblValError" border="0" cellspacing="0" cellpadding="2">
        <tr class="form_heading ErrorHead_bg" height="20">
            <td valign="middle" height="20" colspan="6" style="padding-left: 25px;">
                <asp:Literal ID="ErrorSrc" runat="server" EnableViewState="False"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
    </form>
</body>
</html>
