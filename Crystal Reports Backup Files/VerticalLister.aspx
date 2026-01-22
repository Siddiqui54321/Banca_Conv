<%@ Page language="c#" Codebehind="VerticalLister.aspx.cs" AutoEventWireup="True" Inherits="Aceins.Presentation.VerticalLister" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>VerticalLister</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<table style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 224px">
				<tr>
					<td>
						Proposal Number
					</td>
				</tr>
				<tr>
					<td>Year
					</td>
				</tr>
				<tr>
					<td>Amount
					</td>
				</tr>
			</table>
			<asp:DataList id="DataList1" style="Z-INDEX: 102; LEFT: 152px; POSITION: absolute; TOP: 224px"
				runat="server" RepeatDirection="Vertical" RepeatColumns="10">
				<ItemTemplate>
					<table>
						<tr>
							<td>
								<asp:TextBox id="lblNP1_PROPOSAL" style="width:100" Text='<%# DataBinder.Eval(Container.DataItem, "NP1_PROPOSAL") %>' runat="server" BaseType="Character">
								</asp:TextBox>
							</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox id="Textbox1" style="width:100" Text='<%# DataBinder.Eval(Container.DataItem, "NPW_YEAR") %>' runat="server" BaseType="Character">
								</asp:TextBox>
							</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox id="Textbox2" style="width:100" Text='<%# DataBinder.Eval(Container.DataItem, "NPW_PW") %>' runat="server" BaseType="Character">
								</asp:TextBox>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:DataList>
			<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 344px; POSITION: absolute; TOP: 168px" runat="server"
				Text="Button" onclick="Button1_Click"></asp:Button>
		</form>
	</body>
</HTML>
