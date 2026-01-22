<%@ Page language="c#" Codebehind="MedicalDetail.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.MedicalDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
		<title>Medical Detail</title>
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function test()
			{
				document.getElementById('Save').value = "Y";
			}
			parent.closeWait();
		</script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" name="Form1" method="post" runat="server">
			<input type="hidden" id="Save" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="1" width="250">
				<tr class="form_heading">
					<td height="20" colSpan="4">Medical Detail
					</td>
				</tr>
				<TR id="GridRow">
					<TD id="GridTD">
						<asp:datagrid style="Z-INDEX: 0" id="dg" runat="server" CellPadding="0" BorderWidth="1px" BackColor="White"
							BorderStyle="Solid" AutoGenerateColumns="False" DataKeyField="CQN_CODE" Height="72px" Width="720px"
							CssClass="text_font">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<EditItemStyle HorizontalAlign="Justify"></EditItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Justify" BorderWidth="2px" BorderStyle="Ridge" Width="50px"
								CssClass="ItemStyle"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CQN_CODE" HeaderText="Code"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Description">
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_DESC") %>'>
										</asp:Label>
										<asp:DataGrid style="Z-INDEX: 0" id="Dg_SubGrid" runat="server" Width="420px" DataKeyField="CQN_CODE"
											AutoGenerateColumns="False" ForeColor="Gray">
											<AlternatingItemStyle BackColor="#F4F8FB"></AlternatingItemStyle>
											<ItemStyle Font-Size="XX-Small" Font-Names="Arial" BorderStyle="Ridge" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="CCN_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Answer">
													<ItemTemplate>
														<asp:TextBox id="TXT_SUBANS" Width="380px" CssClass="text_font" runat="server"></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:DataGrid>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_DESC") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Yes/No">
									<ItemStyle Width="45px"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton style="Z-INDEX: 0" id="RadioButton2" runat="server" Text="Yes" GroupName="1" EnableViewState="True"
											OnCheckedChanged="OnChangeHandler" AutoPostBack="False"></asp:RadioButton>
										<BR>
										<asp:RadioButton style="Z-INDEX: 0" id="RadioButton1" runat="server" Text="No" GroupName="1" EnableViewState="True"
											OnCheckedChanged="OnCheck" AutoPostBack="False"></asp:RadioButton>
										<asp:TextBox ID="txt" Visible="False" Runat="server"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Remarks">
									<ItemStyle Width="80px"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="NQN_REMARKS" CssClass="text_font" Width="190" Runat="server"></asp:TextBox>
										<asp:TextBox id="subQuestion" Runat="server" Width="0" Visible="False"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR style="DISPLAY: none">
					<TD>
						<asp:button style="Z-INDEX: 0" id="Button1" runat="server" Width="88px" Text="Underwritee" Visible="False" onclick="Button1_Click"></asp:button></TD>
				</TR>
				<tr height="50">
					<td class="button2TD" align="right" height="30">
						<asp:button id="Button3" OnClientClick="test()" class="button2" runat="server" Width="88px"
							Text="Update" onclick="Button3_Click_1"></asp:button>
						<!-- <a href="#" class="button2" onclick="callUpdate();">&nbsp;&nbsp;Save&nbsp;&nbsp;</a> -->
					</td>
				</tr>
			</TABLE>
		</form>
		<script language="javascript">
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>

			
			function callUpdate()
			{
				if (Page_ClientValidate())
				{
					parent.openWait('Saving Data');
					document.getElementById("Save").value = "Y";
					saveUpdate();
				}				
			}			
			function Page_ClientValidate()
			{
				return true;
			}
			
			function saveUpdate()
			{
				//__doPostBack("Button3", "Button3_Click1");
				__doPostBack("Button3", "Button3_Click_1");
			
			}
				function UnderWriteUpdate()
			{
				__doPostBack("Button1", "Button1_Click");
			
			}
			var tableHeight = document.getElementById('Table1').offsetHeight;
			//var tableHeight = document.getElementById('Table1').style.height;

			//alert(tableHeight);
			if(tableHeight>300){
					tableHeight = tableHeight - document.getElementById('Table1').offsetTop;
			}
			var ppp=parent;
			for (i=0;i<100;i++) 
			{
				if (ppp.parent==null)
				break;
				ppp=ppp.parent;
			}   
			
			
			ppp.document.getElementById('mainContentFrame').style.height = tableHeight;//+100;
			window.parent.document.getElementById('questFrame').height =tableHeight+50;
		</script>
	</body>
</HTML>
