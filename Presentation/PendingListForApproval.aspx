<%@ Page language="c#" Codebehind="PendingListForApproval.aspx.cs" AutoEventWireup="True" EnableEventValidation="false" Inherits="SHAB.Presentation.PendingListForApproval" %>
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
		<LINK rel="stylesheet" type="text/css" href="Styles/comments.css">	
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<script language="javascript" src="JSFiles/Comments.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/illustration.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<LINK rel="stylesheet" type="text/css" href="/Presentation/Styles/MainPage.css">
		<script language="javascript">
		parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input type="text" value="" id="txtNP1_PROPOSAL" style="display:none" />
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr class="form_heading">
					<td height="20" colSpan="4" valign="middle" align="center">Pending Proposal List-4
					</td>
				</tr>
				<tr>
					<td></td>
					<asp:HyperLink id="hlbanca" Visible="True" CssClass="text_font" NavigateUrl="#" Text="Click Here to Download Last Generated File." runat="server" />
				</tr>
				
				<tr>
					<td align="center">
						<asp:DataGrid ID="dGrid" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
							AutoGenerateColumns="False" CssClass="text_font" Width="100%" OnItemDataBound="dGrid_ItemDataBound1">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
								HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
								HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Status" Visible="True">
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
								<asp:TemplateColumn HeaderText="Posted Date (dd/mm/yyyy)">
									<ItemTemplate>
										<asp:TextBox ID="txtPostDate" CssClass="text_font" Width="6.0pc" Runat="server" Text=''>
										</asp:TextBox>
								</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Comments" Visible="True" ItemStyle-Width="150">
									<ItemTemplate >
										<asp:TextBox ID="txtComments" CssClass="text_font" Width="8.0pc" Runat="server" Text=''>
										</asp:TextBox>
										<asp:CustomValidator id="txtSubAnswerVLD" 
											runat="server" 
											CssClass="commentValidator"
											ClientValidationFunction="isCommentGiven" 
											ErrorMessage="Please enter either comment" 
											ToolTip="Please enter answer or either select 'No'."> Required </asp:CustomValidator>
											
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
									<img src="Images/icon-comment.png" width="16" onmouseover="showComments(<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>);"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
									<img src="Images/icon-info.png" width="16" onclick="showInfo();"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
										<div style="display:none;" class="divComments" id='<%# DataBinder.Eval(Container.DataItem, "NP1_PROPOSAL") %>'>
											<asp:Repeater ID='repAllComments' runat="server">
											<HeaderTemplate>
												<Table class="text_font">
													<tr class='CommentGridHeading'>
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
													<tr class='CommentItemStyle'>
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
											<AlternatingItemTemplate>
											<tr class='CommentItemStyleAlt'>
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
											</AlternatingItemTemplate>
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
				
				
				<tr style="display:none;">
					<td align="center">
						<asp:DataGrid ID="dGrid2" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid" AutoGenerateColumns="False" Width="90%">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<ItemStyle VerticalAlign=Bottom HorizontalAlign=Center Font-Names="Arial" Height="20px" Font-Size="10"></ItemStyle>
							<HeaderStyle Font-Names="Arial" Height="20px" ForeColor="Black" HorizontalAlign="Center" Font-Bold=True Font-Size="9"></HeaderStyle>
							<Columns>
								
								<asp:TemplateColumn HeaderText="Transaction Identifier" ItemStyle-Width="192">
									<ItemTemplate>  
										<asp:Label ID="Label0" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRANS_ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From Branch Code" ItemStyle-Width="113">
									<ItemTemplate>
										<asp:Label ID="Label1" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_BRANCH_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From Account No" ItemStyle-Width="105">
									<ItemTemplate>
										<asp:Label ID="Label2" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From Account Type" ItemStyle-Width="117">
									<ItemTemplate>
										<asp:Label ID="Label3" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_NO") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From CLSL" ItemStyle-Width="69">
									<ItemTemplate>
										<asp:Label ID="Label4" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_CLSL") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From Account Currency" ItemStyle-Width="143">
									<ItemTemplate>
										<asp:Label ID="Label5" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_ACC_CURR") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To Branch Code" ItemStyle-Width="98">
									<ItemTemplate>
										<asp:Label ID="Label6" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_BRANCH_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To Account No" ItemStyle-Width="89">
									<ItemTemplate>
										<asp:Label ID="Label7" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_NO") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To Account Type" ItemStyle-Width="103">
									<ItemTemplate>
										<asp:Label ID="Label8" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To Account Currency" ItemStyle-Width="128">
									<ItemTemplate>
										<asp:Label ID="Label10" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_ACC_CURR") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To CLSL" ItemStyle-Width="64">
									<ItemTemplate>
										<asp:Label ID="Label9" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CLSL") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount" ItemStyle-Width="64">
									<ItemTemplate>
										<asp:Label ID="Label11" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Currency" ItemStyle-Width="132">
									<ItemTemplate>
										<asp:Label ID="Label12" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRANS_CURR") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Value Date" ItemStyle-Width="68">
									<ItemTemplate>
										<asp:Label ID="Label13" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VAL_DATE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="User Narration" ItemStyle-Width="386">
									<ItemTemplate>
										<asp:Label ID="Label14" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USER_NAR") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Instrument No" ItemStyle-Width="87">
									<ItemTemplate>
										<asp:Label ID="Label15" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INST_NO") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From CRC" ItemStyle-Width="71">
									<ItemTemplate>
										<asp:Label ID="Label16" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "F_CRC") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="To CRC" ItemStyle-Width="70">
									<ItemTemplate>
										<asp:Label ID="Label17" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CRC") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Wht Flag" ItemStyle-Width="56">
									<ItemTemplate>
										<asp:Label ID="Label18" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WHT_FLG") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tran_code" ItemStyle-Width="68">
									<ItemTemplate>
										<asp:Label ID="Label19" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TRAN_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
			<asp:Button ID="btnSave" Runat="server" Visible="False" Text="Save"></asp:Button>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
				<div id="divInfo" class="form_heading_2 floatingDiv">
				<table>
					<tr>
						<div width="100%">
						<input type="button" value="Close" onclick="hideInfo();" class="closeButton"></input>
						</div>
						<div width="100%">
						<Table class="text_font">
							<tr class='CommentGridHeading'>
								
								<td>
									Submitted Documents
								</td>
							</tr>
							<tr class='CommentItemStyle'>
								
								<td>
									Customer Signed Application.
								</td>
							</tr>
							<tr class='CommentItemStyleAlt'>
								
								<td>
									Customer Signed Standing Order.
								</td>
							</tr>
							<tr class='CommentItemStyle'>
								
								<td>
									Customer Signed Disclaimer Form.
								</td>
							</tr>
							<tr class='CommentItemStyleAlt'>
								
								<td>
									Customer Signed Copy of Acceptance / Requirement (which ever is applicable) Letter.
								</td>
							</tr>
							<tr class='CommentItemStyle'>
								
								<td>
									Valid CNIC Copy.
								</td>
							</tr>
						</table>	
						</div>					
					</tr>
				</table>
			</div>
		</form>
		<script language="javascript">     
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
			<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>
			
			//Problem: Validator Occupy Space in Grid \
			//Solution: Hide before Validation Fired and Show when Validation false
			$('.commentValidator').hide();
			
			function setValue(value)
			{
				document.getElementById("txtNP1_PROPOSAL").value = value;
				//alert(value);
			}
			/*
			function Page_ClientValidate()
			{
				return true;
			}
			*/
			function saveUpdate()
			{					
				if(document.getElementById('dGrid')!=null)
				{	
					parent.closeWait();
					document.getElementById("_CustomEventVal").value = "Save";
					__doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
				}
				else
				{
					parent.closeWait();
					alert('There is no pending proposal to post.');
				}
			}	
			function showComments(porposal){
				hideComments();
				var divId='#'+porposal;
				$(divId).css('display','');
			}
			function hideComments(){
				$('.divComments').css('display','none');
			}
			
			function DownloadFile(FilePath)
	        {
	           var r=confirm("Make sure that this file is not downloaded before for payment deduction.. Continue Downloading?");
	           if (r==true)
	           {
	              window.location.replace( "UploadedFiles/"+FilePath+"" );
	           }
	           else
	           {
	             //alert("You pressed Cancel!");
	           }
	        }	
		</script>
	</body>
</HTML>
