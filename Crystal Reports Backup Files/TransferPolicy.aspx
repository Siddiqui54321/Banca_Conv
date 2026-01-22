<%@ Page language="c#" Codebehind="TransferPolicy.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.TransferPolicy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<title>Manual Policy Issuance</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		
		<LINK href="Styles/MainPage.css" type="text/css" rel="stylesheet">
		<LINK rel="stylesheet" type="text/css" href="Styles/comments.css">
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<script language="javascript" src="JSFiles/Comments.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/illustration.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		parent.closeWait();
		//<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input type="text" value="" id="txtNP1_PROPOSAL" style="display:none" />
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr class="form_heading">
					<td height="20" colSpan="4" valign="middle" align="center">Pending Proposal List
					</td>
				</tr>
				<tr>
				<td>
				<asp:TextBox style="Z-INDEX: 102; " id="txtSearchEvent" runat="server" 
					 AutoPostBack=False	Width="0px"></asp:TextBox>
				
				<asp:DropDownList style="Z-INDEX: 103;" id="Dropdownlist1" runat="server" tabIndex="1"
						Width="100px" Visible="False">
						<asp:ListItem Value="">All Proposals</asp:ListItem>						
					</asp:DropDownList>
					
					<asp:DropDownList style="Z-INDEX: 103;" id="ddlsearch" runat="server" tabIndex="2"
						Width="110px">
						<asp:ListItem Value="0">Select Search...</asp:ListItem>
						<asp:ListItem Value="1">Proposal No</asp:ListItem>
						<asp:ListItem Value="2">Proposal Date</asp:ListItem>
						<asp:ListItem Value="3">Name</asp:ListItem>
						<asp:ListItem Value="4">CNIC</asp:ListItem>
					</asp:DropDownList>
					<asp:TextBox style="Z-INDEX: 102; " id="txtsearch" runat="server" tabIndex="3"
					 AutoPostBack=False	Width="160px" onblur="ValidateSearch();"></asp:TextBox>
					<asp:Button style="Z-INDEX: 104; " id="Button1" runat="server" tabIndex="4"
						Font-Bold="True" Font-Names="Arial" Text="Search" Height="20px" onclick="Button1_Click"></asp:Button>
				</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="center">
						<asp:DataGrid ID="dGrid" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
							AutoGenerateColumns="False" CssClass="text_font" Width="100%" AllowPaging="True" PageSize="50" OnPageIndexChanged="dGrid_OnPageIndexChanged" AllowCustomPaging="False" >
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"
								HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"
								HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Status" Visible="True">
									<ItemTemplate>
										<input type="checkbox" class="genPolicyClass" id='chk<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>' value='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'/>
										<img src="Images/loading.gif" width="16" style="display:none;" id='img<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'/>
										<asp:DropDownList Visible="False" ID="ddlStatus" Runat="server" Width="4.0pc" CssClass="text_font">
											<asp:ListItem Value=".">&nbsp;</asp:ListItem>
											<asp:ListItem Value="Y">Generate</asp:ListItem>
										</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ok/Not Ok" Visible="True">
									<ItemTemplate>
										<input type="checkbox" class="genPolicyStatusClass" onclick="propstatus(this)" id='chkpropstatus<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>' checked value='<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>'/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Proposal">
									<ItemTemplate>
										<a onClick="setValue('<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>');executeReport('PROFILE');" class="text_font" href="#">
											<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>
										</a>
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
								
								
								<asp:TemplateColumn HeaderText="Commencement Date">
									<ItemTemplate>
										<asp:TextBox ID="txtCommencementDate" Runat="server" Width="5.0pc"  Text='<%# DataBinder.Eval(Container, "DataItem.NP2_COMMENDATE") %>'
										onblur="GetCommDateVal(this);" CssClass='<%# DataBinder.Eval(Container, "DataItem.NP2_COMMENDATE") %>'>
										</asp:TextBox>
										<a href="#" class="button2" visible="false" disabled="true" onclick="SetCommDateVal('<%# DataBinder.Eval(Container, "DataItem.NP2_COMMENDATE") %>'); Update('btnUpdate','<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>', document.getElementById('HdnLabel').value, document.getElementById('HdnTxtCommDateID').value);">&nbsp;Update&nbsp;</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Comments" Visible="false" ItemStyle-Width="150">
									<ItemTemplate>
										<asp:TextBox ID="txtComments" CssClass="text_font" Width="8.0pc" Runat="server" Text=''></asp:TextBox>
										<asp:CustomValidator id="txtSubAnswerVLD" runat="server" CssClass="commentValidator" ClientValidationFunction="isCommentGiven"
											ErrorMessage="Please enter either comment" ToolTip="Please enter answer or either select 'No'."> Required </asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>								
								
								<asp:TemplateColumn>
									<ItemTemplate>
									<img src="Images/icon-comment.png" width="16" onclick="OpenComments(<%# DataBinder.Eval(Container, "DataItem.np1_proposal") %>,event);" />										
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
										<img src="Images/icon-info.png" width="16" onclick="showInfo();" />
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
			<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" Position="Bottom"
							Mode="NumericPages"></PagerStyle>
			</asp:DataGrid>
			<!-- </asp:DataGrid> -->
			</td> </tr>
			<tr style="display:none;">
				<td align="center">
					<asp:DataGrid ID="dGrid2" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
						AutoGenerateColumns="False" Width="90%">
						<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
						<ItemStyle VerticalAlign="Bottom" HorizontalAlign="Center" Font-Names="Arial" Height="20px"
							Font-Size="10"></ItemStyle>
						<HeaderStyle Font-Names="Arial" Height="20px" ForeColor="Black" HorizontalAlign="Center" Font-Bold="True"
							Font-Size="9"></HeaderStyle>
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
			<asp:Button ID="btnUpdate" Runat="server" Text="Update" Width="0px"></asp:Button>
			<asp:Button ID="btnSave" Runat="server" Visible="False" Text="Save"></asp:Button>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server">
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
										Customer Signed Copy of Acceptance / Requirement (which ever is applicable) 
										Letter.
									</td>
								</tr>
								<tr class='CommentItemStyle'>
									<td>
										Valid CNIC Copy.
									</td>
								</tr>
							</Table>
						</div>
					</tr>
				</table>
			</div>
			<asp:Label ID="HdnLabel" Runat="server" />
			<asp:Label ID="HdnTxtCommDateID" Runat="server" />
			<asp:Label ID="HdnOldCommDate" Runat="server" />
			
		</form>
		
		<script language="javascript">     
			<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
			<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>

var commdate=null;
function ValidateDate(txtCommDate)			
			{
			commdate=txtCommDate.value;

			var commdt= new Date(commdate);
			var parts=commdate.split('/');
			commdt.setDate(parts[0]);
			commdt.setMonth(parts[1],parts[0]);
			commdt.setFullYear(parts[2],parts[1]-1,parts[0]);

			var today= new Date();

//alert(commdt);
//alert(today);
					if(commdate == null || commdate == "")
					{
						alert("Please Enter Commencement Date.");
					}
					if(commdt < today -30)
					{					    
						alert("Commencement Date cannot be less than 1 month old.");
					}
					if(commdt > today)
					{
						alert("Commencement Date cannot be more than today.");
					}
			}
			

			function GetCommDateVal(txtCommDateVal)
			{			
					var id = txtCommDateVal.id;
					var OldCommDate = txtCommDateVal.className;
					
					commdate=txtCommDateVal.value;			
					//alert(commdate);
					document.getElementById('HdnLabel').value = commdate;
					document.getElementById('HdnTxtCommDateID').value = id;					
					document.getElementById('HdnOldCommDate').value = OldCommDate;
				
			//alert(document.getElementById('HdnLabel').value);			
			}
			
			function SetCommDateVal(CommDate)
			{		
			
					if (document.getElementById('HdnLabel').value == null)
					{
					  document.getElementById('HdnLabel').value = CommDate;
					}
			}
			
			function Update(ButtonId,ProposalNo,CommDate,txtCommdateID)
			{					
					commdate=CommDate;
					
					var CommDateID = txtCommdateID;

					var commdt= new Date(commdate);
					var parts=commdate.split('/');
					commdt.setDate(parts[0]);
					//commdt.setMonth(parts[1],parts[0]);
					commdt.setMonth(parts[1]-1);
					//commdt.setFullYear(parts[2],parts[1]-1,parts[0]);
					commdt.setFullYear(parts[2]);

					var today= new Date();
					
					var OldDate = new Date();
					
					
					//var cl = new Date(today.getDate() - 30);
					
					//var newd = today.setDate(today.getDate() - 30);
					
					var newd = OldDate.setDate(OldDate.getDate() - 30);
					
			
					if(commdate == null || commdate == "")
					{
						alert("Please Enter Commencement Date.");
					}
					//else if(commdt < today -30)
					else if(commdt < OldDate)
					{					    
						alert("Commencement Date cannot be less than 1 month old.");
						document.getElementById(CommDateID).value = document.getElementById('HdnOldCommDate').value;
						document.getElementById('HdnOldCommDate').value = "";
					}
					else if(commdt > today)
					{
						alert("Commencement Date cannot be more than today.");
						document.getElementById(CommDateID).value = document.getElementById('HdnOldCommDate').value;
						document.getElementById('HdnOldCommDate').value = "";
					}
					else{			
					//alert("buttonupdate");
					//alert("Proposal: "+ ProposalNo);
					//alert(CommDate);
					var parameters =  CommDate + "," + ProposalNo;
						//alert(parameters);
					var retVal_CommDate = executeClass('ace.Ace_General,UpdateCommencementDate_New,'+parameters+'');
					
					document.getElementById('HdnLabel').value = "";
					document.getElementById('HdnOldCommDate').value = "";
					document.getElementById('HdnTxtCommDateID').value = "";
					//window.location.reload();
					
					location.href = location.href;
					}
					//document.all(ButtonId).click();
					//alert("buttonupdate123");
			}
		
			function checkboxval() {
			
			
			var chkedRows = $(".genPolicyClass:checked:visible");
			if(chkedRows.length>0){
				   var chk = chkedRows[0];
				   }
			
			
			var comm = "";
            //get reference of GridView control
            var grid = document.getElementById("<%= dGrid.ClientID %>");
            //variable to contain the cell of the grid
            var checkboxCell;
            var labelCell;
            var BF1Count = 0;
            var iCount = "0";
            
            var dgridval = grid.value;
            
          
            
            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
					for (i = 0; i < grid.rows.length; i++) {									 
					
					checkboxCell = grid.rows[i].cells[0];
                    labelCell = grid.rows[i].cells[7];
						 //loop according to the number of childNodes in the cell
						for (j = 0; j < checkboxCell.childNodes.length; j++) {
							if (checkboxCell.childNodes[j].type == "checkbox") {
									
									if (checkboxCell.childNodes[j].checked == true) {
									
													if (labelCell.childNodes[j].type == "text")
													{ alert(labelCell.childNodes[j].value);}	
									
		                                
											} else {
													if (labelCell.childNodes[j].type == "text")
													{ comm  = labelCell.childNodes[j].value;}				                              
											}
							}
						
						
						}
					}
				}
			}          
            
            	
		
			function func_TransferPolicy(){
				var chkedRows = $(".genPolicyClass:checked:visible");
				
				
				if(chkedRows.length>0){
				   var chk = chkedRows[0];
				   var chk1 = chkedRows[1];
				   
				   var Status="";
				   var CommDate="";
				   			   
				    
				   var chkvalid=document.getElementById("chkpropstatus"+chk.value);
				   
                   if(chkvalid.checked)
                   {
                      Status="Y";
                      
                      var PolNo = chk.value;
                      
                      CommDate = executeClass('ace.Ace_General,Get_CommenDate,'+PolNo+'');
                      //alert(CommDate);
                      
                       /////////////============== NEW CODE COMM DATE CHECK 22-MAY-2018
								commdate=CommDate;
								var commdt= new Date(commdate);				
								
								var FlagWarning = "";
								var parts=commdate.split('/');
								commdt.setDate(parts[0]);					
								commdt.setMonth(parts[1]-1);			
								commdt.setFullYear(parts[2].substring(0, 4),parts[1]-1,parts[0]);
								
								var today= new Date();					
								var OldDate = new Date();					
								
								var newd = OldDate.setDate(OldDate.getDate() - 30);
			           
			           
								if(commdate == null || commdate == "")
								{
									alert("Please Enter Commencement Date.");
									FlagWarning = "1";
								}					
								else if(commdt < OldDate)
								{					    
									alert("Commencement Date cannot be less than 1 month old.");		
									FlagWarning = "1";
								}
								else if(commdt > today)
								{
									alert("Commencement Date cannot be more than today.");			
									FlagWarning = "1";
								}
								
						/////////////============== NEW CODE COMM DATE CHECK 22-MAY-2018
                      
				   
				   }
                   else
                   {
					   //CommDate = "";
					   var PolNo = chk.value;
					   CommDate = executeClass('ace.Ace_General,Get_CommenDate,'+PolNo+'');
                       Status="N";
				   }
           //alert("THIS: "+commdate);
				   //CommDate =document.getElementById("dGrid__ctl2_txtCommencementDate").value;		
				   
				  
				   //var GridID = document.getElementById('#<%= dGrid.ClientID %>').outerHTML;   
				   //CommDate = new Date();
           //alert(CommDate);
           
          
           
				  	var imgId = '#img'+chk.value;
					$(chk).hide("fast",function(){
						
						$(imgId).show("fast",function(){
							
							
							var parameters = chk.value + "," + Status + "," + CommDate;
							if (FlagWarning == "1")
							{var retVal =	"Check Commencement Date";}
							else {
							var retVal = executeClass('ace.Ace_General,transferProposalFromFileToPolicy,'+parameters+'');
							}
							status="";
				  			$('html, body').animate({
								scrollTop: $(chk).offset().top
							}, 2000);

							$(imgId).hide("fast",function(){
								$(chk).parent().append(retVal);
								func_TransferPolicy();
							});
						
						});
						
					});
					
				}
				else
				 alert("All marked proposal(s) are transfered successfully.");
				//
				
			}
			
			
			
			//Problem: Validator Occupy Space in Grid \
			//Solution: Hide before Validation Fired and Show when Validation false
			$('.commentValidator').hide();

			function setValue(value)
			{
				document.getElementById("txtNP1_PROPOSAL").value = value;
				//alert(value);
			}
			
			function Page_ClientValidate()
			{
				return true;
			}
			
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
				
				var x = document.getElementById(porposal);
				//$('.divComments').css('display','block');
				//var x = document.getElementById("divId");
				//var x = porposal;
					//if (x.style.display === "none") {
					//	x.style.display = "block";
					//} else {
					//	x.style.display = "none";
					//}
				
				$(divId).css('display','');
			}
			function showCommentsNew(porposal){
			
			//var x = document.getElementById(porposal);
				var parameters = porposal;
				var retVal = executeClass('ace.Ace_General,Get_Comments,'+parameters+'');
				
				
				var x = document.getElementById(porposal);		
					if (x.style.display === "none") {
						x.style.display = "block";
						
						document.getElementById(txtID).value = retVal;
						
						
					} else {
						x.style.display = "none";
					}
			}
			
			
			
			function hideComments(){
				$('.divComments').css('display','none');
			}	
			
			function propstatus(ddlstat)
			{
			   if(ddlstat.checked==false)
			   {
			      ProposalStatus="N";
			   }
			   else
			   {
			      ProposalStatus="Y";
			   }
			   //alert(ProposalStatus);
			   
			}
				
			
				
				function OpenComments(Proposal,event) {
				    var ProposalNo = Proposal;
				    //var e = window.event;
				    var e = event;
					var posx = e.clientX;
					var posy = e.clientY;
					
						var width  = 700;
						var height = 150;
						//var left   = posx-200;
						//var top    = posy;
						
						var left = (screen.width/2)-(width/2);
						var top = (screen.height/2)-(height/2);
						var params = 'width='+width+', height='+height;
						params += ', top='+top+', left='+left;
						params += ', directories=no';
						params += ', location=no';
						params += ', menubar=no';
						params += ', resizable=no';
						params += ', scrollbars=yes';
						params += ', status=no';
						params += ', toolbar=no';				
					
					
				    //alert(posx);alert(posy);
					//var myWindow = window.open("DisplayComment.aspx?prpno='"+ProposalNo+"'", "", "width=400,height=100,scrollbars=yes,resizable=yes,left='"+posx+"',top='"+posy+"'");
					var myWindow = window.open("DisplayComment.aspx?prpno='"+ProposalNo+"'",'customWindow', params);
					if (window.focus) {myWindow.focus()}
					
					if (!myWindow.closed) {myWindow.focus()}
					return false;

					//myWindow.moveTo(posx, posy);
				}

			
			
			function IsValidDate(Date) {
                var filter = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/
                                return filter.test(Date);
            }
            function IsValidCNIC(cnic){
				var filter = /^\d+$/
				return filter.test(cnic);
            }
            function ValidateSearch() {
            var ddlsearhVal = document.getElementById("ddlsearch").value;
				
				
				
				if (ddlsearhVal == "2" )	{ 
						var txtsearch = document.getElementById('txtsearch');
						if (txtsearch.value != ""){
							var isValid = IsValidDate(txtsearch.value);
							if (isValid) {
								//alert('Correct format');
								
							}
							else {
								alert('Incorrect Date format. Please enter the date in following manner e.g. 27-Aug-2017');
								txtsearch.focus();
							}
							return isValid
						}
					}
				if (ddlsearhVal== "1" || ddlsearhVal== "4"){
						var txtsearch = document.getElementById('txtsearch');
						if (txtsearch.value != ""){
							var isValid = IsValidCNIC(txtsearch.value);
							if (isValid) {
								//alert('Correct format');
								
							}
							else {
								alert('Incorrect Data. Only numbers are required when searching by Proposal No/CNIC');
								txtsearch.focus();
							}
							return isValid	
						}
				}
            }	
			

			
			
		</script>
	</body>
</HTML>
