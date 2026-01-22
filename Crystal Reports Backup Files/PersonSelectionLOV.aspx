<%@ Page language="c#" Codebehind="PersonSelectionLOV.aspx.cs" AutoEventWireup="True" Inherits="Insurance.Presentation.PersonSelectionLOV" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Persons Selection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body oncontextmenu="return true;" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="dgProposalLOV" runat="server" AutoGenerateColumns="False" Width="104%" AllowPaging="True"
				GridLines="Horizontal" CellPadding="3" BorderWidth="1px" BorderStyle="Solid" SelectedItemStyle-CssClass="GridSelRow"
				HeaderStyle-CssClass="GridHeader">
				<SelectedItemStyle Font-Bold="True" CssClass="GridSelRow"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="ListRowAlt"></AlternatingItemStyle>
				<ItemStyle Font-Size="Smaller" Font-Names="Arial" BorderWidth="3px" BorderStyle="None" CssClass="ListRowItem"></ItemStyle>
				<HeaderStyle Font-Size="Larger" Font-Names="Arial" Font-Bold="True" CssClass="GridHeader"></HeaderStyle>
				<Columns>
					<asp:ButtonColumn DataTextField="NPH_FULLNAME" HeaderText="Proposer's Name" CommandName="Select">
						<HeaderStyle Font-Bold="True"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_CODE" ReadOnly="True">
						<ItemStyle Font-Bold="True" CssClass="ListRowLink"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_LIFE" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_FULLNAME" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_FULLNAMEARABIC" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="COP_OCCUPATICD" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="CCL_CATEGORYCD" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_INSUREDTYPE" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NPH_TITLE" ReadOnly="True"></asp:BoundColumn>
					<asp:ButtonColumn DataTextField="NPH_SEX" HeaderText="Gender" CommandName="Select">
						<HeaderStyle Font-Bold="True"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="NPH_BIRTHDATE" HeaderText="Date of Birth" CommandName="Select">
						<HeaderStyle Font-Bold="True"></HeaderStyle>
					</asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="COP_DESCR" HeaderText="Occupation" CommandName="Select">
						<HeaderStyle Font-Bold="True"></HeaderStyle>
					</asp:ButtonColumn>
				</Columns>
				<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" Position="Top"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></form>
	</body>
</HTML>
