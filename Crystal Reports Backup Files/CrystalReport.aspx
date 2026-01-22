<%@ Assembly Name="CrystalReports" %>
<%@ Page language="c#" Codebehind="CrystalReport.aspx.cs" AutoEventWireup="false" Inherits="Presentation.CrystalReport" %>
<%@ Register TagPrefix="cr1" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../Presentation/JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="../Presentation/JSFiles/msrsclient.js"></script>
		<script language="javascript" src="../shmalib/jscript/Crystal.js"></script>
		<script language="javascript" src="../shmalib/jscript/PrintIllustration.js"></script>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<cr1:crystalreportviewer id="Crv" style="Z-INDEX: 101; POSITION: absolute; TOP: 56px; LEFT: 16px" runat="server"
				Height="50px" Width="350px"></cr1:crystalreportviewer><asp:button id="btnExp" style="Z-INDEX: 102; POSITION: absolute; TOP: 8px; LEFT: 88px" runat="server"
				Height="24px" Width="56px" Text="Export"></asp:button>
			<DIV id="divExp" style="Z-INDEX: 103; BORDER-BOTTOM: gray 1px solid; POSITION: absolute; BORDER-LEFT: gray 1px solid; WIDTH: 184px; HEIGHT: 40px; BORDER-TOP: gray 1px solid; TOP: 8px; BORDER-RIGHT: gray 1px solid; LEFT: 160px"
				runat="server" ms_positioning="GridLayout"><asp:dropdownlist id="ddLExp" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" runat="server"
					Height="24px" Width="128px"></asp:dropdownlist><asp:button id="btnOK" style="Z-INDEX: 104; POSITION: absolute; TOP: 8px; LEFT: 144px" runat="server"
					Width="32px" Text="ok"></asp:button></DIV>
			<asp:label id="lblError" style="Z-INDEX: 104; POSITION: absolute; TOP: 16px; LEFT: 360px" runat="server"
				Height="32px" Width="96px"></asp:label><INPUT id="btnPrint1" style="Z-INDEX: 105; POSITION: absolute; WIDTH: 48px; HEIGHT: 24px; TOP: 8px; LEFT: 32px"
				onclick="IllustrationPrint();" type="button" value="Preview">
			<asp:label id="lblProposal" style="Z-INDEX: 106; POSITION: absolute; TOP: 16px; LEFT: 552px"
				runat="server" Height="24px" Width="80px" ForeColor="White">Label</asp:label></form>
	</body>
</HTML>
