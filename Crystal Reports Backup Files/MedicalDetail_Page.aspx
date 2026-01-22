<%@ Page language="c#"  EnableEventValidation="false"  Codebehind="MedicalDetail_Page.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.MedicalDetail_Page" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MedicalDetail</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/jquery-1.4.3.min.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		parent.closeWait();
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr class="form_heading">
					<td height="20" colSpan="4">Medical Detail
					</td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid ID="dGrid" Runat="server" CellPadding="0" BorderWidth="1px" BackColor="White" BorderStyle="Solid"
							AutoGenerateColumns="False" CssClass="text_font">
							<SelectedItemStyle Font-Bold="True" ForeColor="Red" Width="50px" BackColor="#FFE0C0"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="ItemStyleAlt"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BorderWidth="2px" BorderStyle="Ridge" Width="50px" CssClass="ItemStyle"></ItemStyle>
							<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Code">
									<ItemTemplate>
										<asp:Label ID="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description">
									<ItemTemplate>
										<table cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<asp:Label ID="lblDesc" Runat="server" CssClass="text_font" Text='<%# DataBinder.Eval(Container, "DataItem.CQN_DESC") %>'>
													</asp:Label>
												</td>
											</tr>
											<tr>
												<td>
													<div id="subDiv" runat="server">
														<asp:DataGrid ID="dSubGrid" Runat="server" CellPadding="0" GridLines="None" CellSpacing="1" AutoGenerateColumns="False">
															<HeaderStyle Font-Names="Helvetica" Height="22px" ForeColor="White" CssClass="form_heading_2"></HeaderStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="Description" HeaderStyle-HorizontalAlign="Center">
																	<ItemTemplate>
																		<asp:Label ID="lblColumnID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_COLUMNID") %>'>
																		</asp:Label>
																		<asp:Label ID="lblDescription" CssClass="text_font" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCN_DESCRIPTION") %>'>
																		</asp:Label>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Answer">
																	<ItemTemplate>
																		<!--<asp:Label ID="lblSubAnswer" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.NQD_ANSWER") %>' ></asp:Label>-->
																		<!--<asp:RadioButton ID="rdoSubAns" Width="380px" CssClass="text_font" runat="server"></asp:RadioButton> <!--OnCheckedChanged="rdoSubAns_OnCheckedChanged"-->
																		<asp:TextBox ID="txtSubAnswer" Runat="server" CssClass="text_font" Width="380px" Text='<%# DataBinder.Eval(Container, "DataItem.NQD_ANSWER") %>'>
																		</asp:TextBox>
																		<asp:CustomValidator id="txtSubAnswerVLD" CssClass="text_font" runat="server" ClientValidationFunction="isAnswerGiven"
																			ErrorMessage="Please enter either answer" ToolTip="Please enter answer or either select 'No'."> Required </asp:CustomValidator>
																	</ItemTemplate>
																</asp:TemplateColumn>
															</Columns>
														</asp:DataGrid>
													</div>
												</td>
											</tr>
										</table>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Yes/No">
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblAnswer" Runat="server" Text=''></asp:Label>
										<asp:RadioButton ID="rdoYes" Runat="server" Text="Yes" GroupName="1"></asp:RadioButton><br>
										<asp:RadioButton ID="rdoNo" Runat="server" Text="No" GroupName="1"></asp:RadioButton>
										<asp:TextBox ID="txtIsChildExist" Runat="server" ReadOnly="True" Width="0.0pc"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Remarks">
									<ItemTemplate>
										<asp:TextBox ID="txtRemarks" Runat="server" CssClass="text_font"></asp:TextBox>
										<asp:TextBox ID="txtVisible" Runat="server" ReadOnly="True" Width="0.0pc"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
			<asp:Button ID="btnSave" Runat="server" Visible="False" Text="Save"></asp:Button>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
		</form>
		<script language="javascript">
		<asp:Literal id="_result" runat="server" EnableViewState="False"></asp:Literal>
		
		function isAnswerGiven(sender, args){
				//txtSubAnswerVLD
			    //txtSubAnswer		    
				
				var txtID = sender.id.substring(0,sender.id.lastIndexOf('_')+1)+'txtSubAnswer';
				var txt = document.getElementById(txtID);
				var jqTxt = $('#'+txtID);
				
				var subDivId = sender.id.substring(0, sender.id.indexOf('__ctl')+7) +'subDiv';				
				var subdiv = document.getElementById(subDivId);
			
				//txtID = sender.id.substr(0,sender.id.length-3);
				
				if(subdiv.style.display=='block' && txt.value == ""){
					
					//alert(subdiv.style.display+" - "+subDivId+" - "+txtID+"  -  " + txt.value + " - "+ txt.className);
					args.IsValid = false;
					return;
					
				}
			    
				args.IsValid = true;
				return;
			    
			}
			/*
			void Validate(Object sender, EventArgs e){
			if (Page.IsValid == false)
			{
				if(status=="N")
					window.alert("helllloooooo");
			}
			else
				window.alert("empty");
			}
			*/
		/*
		function Page_ClientValidate()
		{
			return true;
		}*/
		function saveUpdate()
		{					
			if(document.getElementById('dGrid')!=null)
			{
				document.getElementById("_CustomEventVal").value = "Save";
				__doPostBack("_CustomEvent", "_CustomEvent_ServerClick");
			}
			else
			{
				parent.closeWait();
				alert('There is nothing to save in case of minor');
			}
		}		
		
		function fnOnClickRadio(subgrid,obj,objId)
		{	
			var subDivId = obj.id.substring(0,obj.id.lastIndexOf('_')+1)+'subDiv';				
			var subdiv = document.getElementById(subDivId);
			var rdoArray = subgrid.getElementsByTagName("input");
			if(subdiv!=null)
			{	
				if(objId == 'rdoYes')
				{
					if(rdoArray.length > 0)
					{	
						if (subdiv.id == 'dGrid__ctl9_subDiv')
						{  slideToggle('#'+subDivId,false);	}					
						else {slideToggle('#'+subDivId,true);	}
					}
					else
					{	if (subdiv.id == 'dGrid__ctl9_subDiv')
						{  slideToggle('#'+subDivId,true);	}	
						else {slideToggle('#'+subDivId,false);}
					}
				}
				else
				{	
					if(rdoArray.length > 0)
					{	
						if (subdiv.id == 'dGrid__ctl9_subDiv')
						{  slideToggle('#'+subDivId,true);	}
						else { slideToggle('#'+subDivId,false); }
					}
					else
					{
						if (subdiv.id == 'dGrid__ctl9_subDiv')
						{  slideToggle('#'+subDivId,false);	}						
						else {	slideToggle('#'+subDivId,true);	}
					}					
				}
			}
		}
		
		function slideToggle(el, bShow)
		{
			var $el = $(el), height = $el.data("originalHeight"), visible = $el.is(":visible");
			if( arguments.length == 1 ) bShow = !visible;
			if( bShow == visible ) return false;		  
			
			if( !height )
			{	
				height = $el.show().height();
				if (height == '24')
				{height = '45';}
				$el.data("originalHeight", height);				
				if( !visible ) $el.hide().css({height: 0});
			}
			
			if( bShow )
			{								
				//$el.slideDown(500);				
				$el.slideDown(700);				
			} 
			else 
			{
				$el.animate({height: 0}, {duration: 250, complete:function (){
					$el.hide();
				}
				});
			}
		}
		
		function uncheckIfSelected(subgridObj)
		{
			var rdoArray = subgridObj.getElementsByTagName("input");
			for(i=0;i<=rdoArray.length-1;i++)  
			{  
				if(rdoArray[i].type == 'radio')
				{	  
					rdoArray[i].checked = false;  						  
				}					
			}
		}
		
		function getNextRowObj(rowID)
		{			
			var nextRowNum = parseInt(rowID.substring(rowID.lastIndexOf('l')+1,rowID.length))+1;
			var nextRowID = rowID.substring(0,rowID.lastIndexOf('l')+1)+ nextRowNum;
			return nextRowID;			
		}
		function setQuestionState(objRow,txtVis,visible)
		{
			if(visible)
			{	
				objRow.style.display = 'block';			
				txtVis.value = 'block';
			}
			else
			{	
				objRow.style.display = 'none';			
				txtVis.value = 'none';
			}
		}
		
		function showHideSubQuestion(rdoY,rdoN,objRow,chkSH,prevRowObj,txtVis)
		{			
			
			if(parseInt(chkSH)==1)
			{				
				setQuestionState(objRow,txtVis,true)
			}
			else
			{
				rdoY.checked = false;
				rdoN.checked = false;								
				setQuestionState(objRow,txtVis,false);				
				var RowID = getNextRowObj(objRow.id);
				var nextRowObj = document.getElementById(RowID);				
				var nextRdoY = document.getElementById(RowID+'_rdoYes');
				var nextRdoN = document.getElementById(RowID+'_rdoNo');
				var nextTxtVis = document.getElementById(RowID+'_txtVisible');		
				
				if(document.getElementById(objRow.id+'_txtIsChildExist').value == RowID)
				{			
					showHideSubQuestion(nextRdoY,nextRdoN,nextRowObj,0,null,nextTxtVis);				
				}
			}			
		}
		
		</script>
	</body>
</HTML>
