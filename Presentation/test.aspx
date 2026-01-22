<%@ Page language="c#" Codebehind="test.aspx.cs" AutoEventWireup="false" Inherits="Aceins.Presentation.test" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>test</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			function RadioClicked(obj)
			{
				alert(obj);
				var rdolist = obj; //document.getElementsByName("rdolist");
				alert(rdolist);
				if (rdolist[1].checked)
				{
					alert(1);
				}
				else if (rdolist[1].checked)
				{
					alert(2);
				}
				else
				{
					alert(3);
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Button1" style="Z-INDEX: 101; LEFT: 528px; POSITION: absolute; TOP: 152px" runat="server"
				Text="Button"></asp:Button>
			<asp:RadioButtonList ID="rdolist" RepeatLayout="Flow" RepeatDirection="Horizontal" Runat="server" onClick="alert();"  >
				<asp:ListItem Value="1" Selected="True">First</asp:ListItem>
				<asp:ListItem Value="2">Second</asp:ListItem>
				<asp:ListItem Value="3">Third</asp:ListItem>
			</asp:RadioButtonList></form>
	</body>
</HTML>
