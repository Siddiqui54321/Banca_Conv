<%@ Page language="c#" Codebehind="TipOfTheDay.aspx.cs" AutoEventWireup="True" Inherits="Aceins.Presentation.LCDT_DAILYTIP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Today's Tip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<META http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="-1">
		<base target="_self">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="txtTip" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				ReadOnly="True" TextMode="MultiLine" Height="152px" CssClass="RequiredField" BorderStyle="None"
				ForeColor="Transparent" Width="376px">Welcome To Ilas Bancassurance</asp:textbox>
			<asp:button id="Button1" style="Z-INDEX: 102; LEFT: 296px; POSITION: absolute; TOP: 168px" runat="server"
				CssClass="RequiredField" Text="Next Tip" onclick="Button1_Click"></asp:button>
			<asp:checkbox id="ChkNextTime" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 168px"
				runat="server" Height="24px" CssClass="RequiredField" Width="152px" Text=" Don't Show on Login" AutoPostBack="True" oncheckedchanged="ChkNextTime_CheckedChanged"></asp:checkbox>
		</form>
		
		<script language="javascript">
			function showTip(){
		window.showModalDialog('TipOfTheDay.aspx', null,'status:no;dialogWidth:370px;dialogHeight:430px;dialogHide:true;help:no;scroll:no');
	}
		</script>
	</body>
</HTML>
