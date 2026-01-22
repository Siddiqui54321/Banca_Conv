<%@ Page language="c#" Codebehind="ListOfApprovedProposal.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.ListOfApprovedProposal" %>
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
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<script language="javascript" src="JSFiles/Comments.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/illustration.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		//parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<input type="text" value="" id="txtNP1_PROPOSAL" style="display:none" />
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr class="form_heading">
					<td height="20" colSpan="4" valign="middle" align="center">Upload Approved Proposal List
					</td>
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
				<web:WebControl ID="cnt" runat="server"></web:WebControl>
				</td>
				</tr>
				<tr>
					<td align="center">
						<asp:DataGrid ID="dGrid" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
							AutoGenerateColumns="False" CssClass="text_font" Width="90%">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
								HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
								HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Status" Visible="False">
									<ItemTemplate>
										<asp:DropDownList ID="ddlStatus" Runat="server" Width="4.0pc" CssClass="text_font">
											<asp:ListItem Value=".">&nbsp;</asp:ListItem>
											<asp:ListItem Value="Y">Ok</asp:ListItem>
											<asp:ListItem Value="N">Not Ok</asp:ListItem>
										</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Proposal">
									<ItemTemplate>
										<a onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PROFILE');" class="text_font" href="#"><%# DataBinder.Eval(Container, "DataItem.np1_proposal") %></a>
										<asp:Label ID="lblProposal" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>' Visible="False">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Proposal Date">
									<ItemTemplate>
										<asp:Label ID="lblPropDate" Runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.np1_propdate")).ToString("dd/MM/yyyy") %>'>
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
								<asp:TemplateColumn HeaderText="Mobile No">
									<ItemTemplate>
										<asp:Label ID="lblMobile" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nad_mobile") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Comments" Visible="False">
									<ItemTemplate>
										<asp:TextBox ID="txtComments" CssClass="text_font" Width="8.0pc" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.np1_comments") %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
									<img src="Images/icon-comment.png" width="16" onmouseover="showComments(<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>);"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
										<div style="display:none;" class="divComments" id='<%# DataBinder.Eval(Container.DataItem, "NP1_PROPOSAL") %>'>
											<asp:Repeater ID='repAllComments' runat="server">
											<HeaderTemplate>
												<Table class="text_font">
													<tr class='form_heading_2'>
														<td>
															Comment By
														</td>
														<td>
															Action
														</td>
														<td class="tdComment">
															Comment
														</td>
														<td>
															Date
														</td>
													</tr>
											</HeaderTemplate>
											<ItemTemplate>
													<tr class='ItemStyle'>
														<td>
												<%# DataBinder.Eval(Container.DataItem, "CM_COMMENTBY") %>
															
														</td>
														<td>
												<%# DataBinder.Eval(Container.DataItem, "CM_ACTION") %>
															
														</td>
														<td class="tdComment">
												<%# DataBinder.Eval(Container.DataItem, "CM_COMMENTS") %>
															
														</td>
														<td>
												<%# DataBinder.Eval(Container.DataItem, "CM_COMMENTDATE") %>															
														</td>
													</tr>
											</ItemTemplate>
											<FooterTemplate>
												</Table>
											</FooterTemplate>
											</asp:Repeater>
										</div>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
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
