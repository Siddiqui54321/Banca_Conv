<%@ Page language="c#" Codebehind="ValidationError.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.ValidationError" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Validation Error!</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>	
	</HEAD>
	
	<body class="ErrorPage_bg">
					
		<form id="Form1" method="post" runat="server">
		<TABLE id="tblValError" border="0" cellSpacing="0" cellPadding="2" >
			<tr class="form_heading ErrorHead_bg" height="20"  >
				<td valign=middle  height="20" colSpan="6" style="PADDING-LEFT: 25px;" ><asp:Literal id="ErrorSrc" runat="server" EnableViewState="False"></asp:Literal></td>
			</tr>
		</table>
		
			<asp:Literal id="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
			
		</form>
	</body>
</HTML>
