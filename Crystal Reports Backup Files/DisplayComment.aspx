<%@ Page language="c#" Codebehind="DisplayComment.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.WebForm2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>Comments Detail</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
  <body MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server">

<asp:DataGrid ID="dGrid" Runat="server" CellPadding="2" BorderWidth="1px" BackColor="#DEBA84" BorderStyle="None"
							AutoGenerateColumns="False" Width="100%" PageSize="50" BorderColor="#DEBA84" Font-Size="smaller" Font-Names="Calibri">
<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C">
</SelectedItemStyle>

<ItemStyle Wrap="False" HorizontalAlign="Left" BorderWidth="2px" ForeColor="#8C4510" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle" BackColor="#FFF7E7">
</ItemStyle>

<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#A55129" VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>

<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5">
</FooterStyle>

<Columns>
<asp:TemplateColumn HeaderText="Proposal No.">
<ItemTemplate>
										
										<asp:Label ID="Label5" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' Visible="True">
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn HeaderText="Name">
<ItemTemplate>
										
										<asp:Label ID="Label6" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NPH_FULLNAME") %>' Visible="True">
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn HeaderText="Comment By">
<ItemTemplate>
										
										<asp:Label ID="Label2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CM_COMMENTBY") %>' Visible="True">
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Action">
<ItemTemplate>
										
										<asp:Label ID="Label3" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CM_ACTION") %>' Visible="True">
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Comments">
<ItemTemplate>
										
										<asp:Label ID="Label4" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CM_COMMENTS") %>' Visible="True">
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date">
<ItemTemplate>
										
		<asp:Label ID="Label1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CM_COMMENTDATE") %>' Visible="True">
		</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
</Columns>

<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages">
</PagerStyle>
							</asp:DataGrid>
						

     </form>
	
  </body>
</HTML>
