<%@ Page language="c#" Codebehind="ListOfPendingAtUsersProposal.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.ListOfPendingAtUsersProposal" %>
<%@ Register TagPrefix="web" TagName="WebControl" Src="~/Presentation/uploader.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>Manual Policy Issuance</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<LINK rel="stylesheet" type="text/css" href="Styles/style.css">	
		<LINK rel="stylesheet" type="text/css" href="Presentation/Styles/MainPage.css">	
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<script language="javascript" src="JSFiles/Comments.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/illustration.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<input type="text" value="" id="txtNP1_PROPOSAL" style="display:none" />
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr>
					<td>
					<asp:LinkButton id="hlbanca" OnClick="hlbanca_Click" Visible="true" CssClass="text_font" NavigateUrl="#" Text="Click Here to Download." runat="server" />
					</td>
				</tr>
				<tr class="form_heading">
					<td height="20" colSpan="4" valign="middle" align="center">Pending Proposal List</td>
				</tr>
				<!--<tr>
					<td>					
						<input type="file" name="FileToUpload" id="FileToUpload" runat="server" class="text_font" size="46" > 
						<asp:RequiredFieldValidator id="UploadValidator" runat="server" CssClass="text_font" ErrorMessage="You must select an csv file to upload"
							ControlToValidate="FileToUpload"></asp:RequiredFieldValidator>										
					</td>
				</tr-->
				
				<tr>
					<td>
					 &nbsp;
					</td>
				</tr>
				<tr>
				<td>
					<asp:DropDownList runat="server" ID="ddl_SelectOption" Width="200px"> 
						<asp:ListItem Value="1" Text="Pending with BM"></asp:ListItem>
						<asp:ListItem Value="2" Text="Pending with RBH"></asp:ListItem>
						<asp:ListItem Value="3" Text="Pending with CBC"></asp:ListItem>
					</asp:DropDownList>
					<asp:Button runat="server" ID="btn_getData" Text="Search" OnClick="btn_getData_Click"/>
				</td>
				</tr>
				<tr>
					<td>
                        <asp:Label ID="lblMsg" runat="server" BorderColor="Red" ForeColor="Red"></asp:Label>
                    </td>
				</tr>
			
				
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="center">
						<div style="overflow-y:scroll;height:380px">
							
						<asp:DataGrid ID="dGrid" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
							AutoGenerateColumns="False" CssClass="text_font" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
								HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
								HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Proposal">
									<ItemTemplate>
										<%--<a onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PROFILE');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>
										--%><asp:Label ID="lblProposal" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Proposal Date">
									<ItemTemplate>
										<asp:Label ID="lblPropDate" Runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.np1_propdate")).ToString("MM/dd/yy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name">
									<ItemTemplate>
										<asp:Label ID="lblName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_fullname") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="CNIC">
									<ItemTemplate>
										<asp:Label ID="lblCNic" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nph_idno") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Commented By">
									<ItemTemplate>
										<asp:Label ID="lblMobile" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_commentby") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Comments">
									<ItemTemplate>
										<asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_comments") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Pending Status">
									<ItemTemplate>
										<asp:Label ID="txtComments" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.screens") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						</div>
					</td>
				</tr>
			</table>
            <!--<asp:Button ID="btnSave" Runat="server" Visible="False" Text="Save"></asp:Button>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server">-->
		</form>
		<script language="javascript">     
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
			function setValue(value)
			{
				document.getElementById("txtNP1_PROPOSAL").value = value;
			}
			function Page_ClientValidate()
			{
				return true;
			}
			function saveUpdate()
			{					
				//alert(1);
				//alert(document.getElementById('dGrid'));
				//if(document.getElementById('dGrid')==null)
				//{	
					document.getElementById("_CustomEventVal").value = "Save";
					__doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
				//}
				//else
				//{
				//	parent.closeWait();
					//alert('There is no pending proposal to post.');
				//}
			}	
			function showComments(porposal){
				hideComments();
				var divId='#'+porposal;
				$(divId).css('display','');
			}
			function hideComments(){
				$('.divComments').css('display','none');
			}	
		</script>
	</body>
</HTML>
