<%@ Page language="c#" Codebehind="ProposalSelectionLOV.aspx.cs" AutoEventWireup="True" Inherits="Insurance.Presentation.ProposalSelectionLOV" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Proposal Selection</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
	</HEAD>
	<body MS_POSITIONING="GridLayout" oncontextmenu="return true;">
		<form id="Form1" method="post" runat="server">
			<table border=0>
				<tr>
					<td>
					<asp:TextBox style="Z-INDEX: 102; " id="txtSearchEvent" runat="server" 
					 AutoPostBack=False	Width="0px"></asp:TextBox>
					
					<asp:DropDownList style="Z-INDEX: 103;" id="ddlStatus" runat="server" tabIndex="1"
						Width="100px">
						<asp:ListItem Value="">All Proposals</asp:ListItem>
						<asp:ListItem Value="O">Pending</asp:ListItem>
						<asp:ListItem Value="P">Posted</asp:ListItem>
						<asp:ListItem Value="R">Referred</asp:ListItem>
						<asp:ListItem Value="A">Approved</asp:ListItem>
					</asp:DropDownList>
					 
					<asp:DropDownList style="Z-INDEX: 103;" id="ddlsearch" runat="server" tabIndex="2"
						Width="110px">
						<asp:ListItem Value="0">Select Search...</asp:ListItem>
						<asp:ListItem Value="1">Proposal No</asp:ListItem>
						<asp:ListItem Value="2">Name</asp:ListItem>
						<asp:ListItem Value="3">ID</asp:ListItem>
					</asp:DropDownList>
					<asp:TextBox style="Z-INDEX: 102; " id="txtsearch" runat="server" tabIndex="3"
					 AutoPostBack=False	Width="160px"></asp:TextBox>
					<asp:Button style="Z-INDEX: 104; " id="Button1" runat="server" tabIndex="4"
						Font-Bold="True" Font-Names="Arial" Text="Search" Height="20px" onclick="Button1_Click"></asp:Button>
					</td>
				</tr>
				<tr>
				<td>
					<asp:DataGrid id="dgProposalLOV" SelectedItemStyle-CssClass="GridSelRow" HeaderStyle-CssClass="GridHeader" tabIndex="4"
						runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
						AllowPaging="True" Width="104%" AutoGenerateColumns="False" style="Z-INDEX: 101;">
						<SelectedItemStyle Font-Bold="True" CssClass="GridSelRow"></SelectedItemStyle>
						<AlternatingItemStyle CssClass="ListRowAlt"></AlternatingItemStyle>
						<ItemStyle Font-Size="Smaller" Font-Names="Arial" BorderWidth="3px" BorderStyle="None" CssClass="ListRowItem"></ItemStyle>
						<HeaderStyle Font-Size="Larger" Font-Names="Arial" Font-Bold="True" CssClass="GridHeader"></HeaderStyle>
						<Columns>
							<asp:ButtonColumn DataTextField="NP1_PROPOSAL" HeaderText="Proposal #" CommandName="Select"> <HeaderStyle Font-Bold="True"></HeaderStyle></asp:ButtonColumn>
							<asp:BoundColumn Visible="False" DataField="NP1_PROPOSAL" ReadOnly="True" HeaderText="Proposal #"></asp:BoundColumn>
							
							<asp:ButtonColumn DataTextField="STATUS" HeaderText="Status" CommandName="Select"> <HeaderStyle Font-Bold="True"></HeaderStyle></asp:ButtonColumn>

							<asp:ButtonColumn DataTextField="NPH_FULLNAME" HeaderText="Proposed Name" CommandName="Select">
								<HeaderStyle Font-Bold="True"></HeaderStyle>
							</asp:ButtonColumn>
							<asp:ButtonColumn DataTextField="NPH_IDNO" HeaderText="ID" CommandName="Select">
								<HeaderStyle Font-Bold="True"></HeaderStyle>
							</asp:ButtonColumn>
							
							<asp:ButtonColumn DataTextField="CCN_DESCR" HeaderText="Country" CommandName="Select">
								<HeaderStyle Font-Bold="True"></HeaderStyle>
							</asp:ButtonColumn>
							
						</Columns>
						<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" Position="Top"
							Mode="NumericPages"></PagerStyle>
					</asp:DataGrid>
				</td></tr>
			</table>		
		</form>
		<script language="javascript">
			try
			{
				document.getElementById("ddlStatus").focus();
			}
			catch(e)
			{
			}
		</script>
	</body>
</HTML>
