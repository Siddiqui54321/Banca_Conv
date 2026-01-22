<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/JScriptTabular.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
		<script language="javascript" src="../shmalib/jscript/GeneralFunctions.js"></script>
		<script language="javascript" src="../shmalib/jscript/ValidationMechanism.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralView.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			totalRecords=<asp:Literal id="_totalRecords" runat="server" EnableViewState="False"></asp:Literal>+1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					
			//function SetStatus(rowStatus){					
			//	if (myForm.txtModifiedRows.value.indexOf(rowStatus) == -1)
			//		myForm.txtModifiedRows.value += rowStatus;
			//}	
			
			parent.parent.document.getElementById('mainContentFrame').style.height = '400.0px';		
		
		</script>
		<script language="Javascript">
		<!--
		function FocusCtl(obj)
		{
			document.getElementById(obj).focus();
		}
		
		function ShowPop(obj)
		{
			var main = document.getElementById(obj);

			//main.style.visibility = "hidden";
			//alert(main);

			if (document.getElementById(obj).value=='')
			{			

				cap = main.id.replace('txt','cap').replace('ddl','cap');
				cap = cap.replace('EntryGrid__ctl','');
				cap = cap.substring(2);


				//iframe if any, only for select windowed control
				document.getElementById(cap).style.visibility = "visible";

				if(document.getElementById('i'+cap)!=null)
				{
					document.getElementById('i'+cap).style.visibility = "visible";
					//alert(123);
				}
			
			}
			
		}
		function HidePop(obj)
		{
			var main = document.getElementById(obj);

			cap = main.id.replace('txt','cap').replace('ddl','cap');
			cap = cap.replace('EntryGrid__ctl','');
			cap = cap.substring(2);

			document.getElementById(cap).style.visibility = "hidden";

			//iframe if any, only for select windowed control
			if(document.getElementById('i'+cap)!=null)
			{
				document.getElementById('i'+cap).style.visibility = "hidden";
				//alert(123);
			}
		}
		//-->
		</script>
		
		<!-- 
		<style type="text/css">
		.popup { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 4px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 4px; FONT-SIZE: 10px; LEFT: 0px; VISIBILITY: visible; PADDING-BOTTOM: 4px; VERTICAL-ALIGN: top; BORDER-LEFT: #000000 1px solid; COLOR: WHITE; PADDING-TOP: 4px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; POSITION: absolute; TOP: 0px; BACKGROUND-COLOR: #6699cc }
		</style>
		-->
		
		
	</HEAD>
	<body>
		
		
		<!--****************************************************************************-->
		<!--******************************* Tool Tip Related ***************************-->
		<!--***** Link Web Site http://www.walterzorn.com/tooltip/tooltip_e.htm ********-->
		<!--****************************************************************************-->
		<SCRIPT language="JavaScript" src='../shmalib/jscript/ToolTips/wz_tooltip.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/ToolTips/tip_centerwindow.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/ToolTips/tip_followscroll.js'></SCRIPT>
		
		
		
		<UC:EntityHeading ParamSource="FixValue" ParamValue="" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" method="post" runat="server">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; LEFT: 496px; POSITION: absolute; TOP: 80px"
				runat="server" Height="12px" Width="0px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX: 102; LEFT: 664px; POSITION: absolute; TOP: 208px"
				runat="server" Width="0px"></asp:textbox>
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
			<SHMA:DATAGRID id="EntryGrid" runat="server" Width="456px" CssClass="GridStyle" HeaderStyle-CssClass="GridHeader" AutoGenerateColumns="False" ShowFooter="<%# ShowFooter %>" SelectedItemStyle-CssClass="GridSelRow" ShowHeader="false" Border="0" BorderColor="Black" BackColor="#F4F8FB" BorderWidth="1px">
				<SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="ListRowItem"></AlternatingItemStyle>
				<ItemStyle Height="50px" CssClass="ListRowAlt"></ItemStyle>
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<CENTER>
								<asp:CheckBox id="chkSelectAll" runat="server"></asp:CheckBox></CENTER>
						</HeaderTemplate>
						<ItemTemplate>
							<CENTER>
								<shma:TextBox id="lblNP1_PROPOSAL" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>' runat="server" BaseType="Character"></shma:TextBox>
									<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="lblNP1_PROPOSAL" Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<shma:TextBox id="lblNBF_BENEFCD" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NBF_BENEFCD")%>' runat="server" BaseType="Character"></shma:TextBox>
									<asp:CompareValidator id="cfvNBF_BENEFCD" runat="server" ControlToValidate="lblNBF_BENEFCD" Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>

								<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox>
								<input type=button onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" id="btnGuardian" value="..." runat="server">
								
								<shma:TextBox id="lblNGU_GUARDCD" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NGU_GUARDCD")%>' runat="server" BaseType="Character"> </shma:TextBox>
									<asp:CompareValidator id="cfvNGU_GUARDCD" runat="server" ControlToValidate="lblNGU_GUARDCD" Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<shma:TextBox id="lblNGU_NAME" style="width:0" runat="server" BaseType="Character"> </shma:TextBox>

									
							</CENTER>
						</ItemTemplate>
						<FooterTemplate>
							<shma:TextBox id="lblNewNP1_PROPOSAL" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNP1_PROPOSAL" runat="server" ControlToValidate="lblNewNP1_PROPOSAL" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="lblNewNBF_BENEFCD" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
							<asp:CompareValidator id="cfvNewNBF_BENEFCD" runat="server" ControlToValidate="lblNewNBF_BENEFCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="lblNewNGU_GUARDCD" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvNewNGU_GUARDCD" runat="server" ControlToValidate="lblNewNGU_GUARDCD" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="lblNewNGU_NAME" style="width:0" runat="server" BaseType="Character"></shma:TextBox>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Beneficiary Name Arabic Name">
						<ItemTemplate>
							<shma:TextBox id="txtNBF_BENNAME" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" Width='14.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NBF_BENNAME")%>' MaxLength="60" CssClass="RequiredField" BaseType="Character">
							</shma:TextBox>
							<asp:RegularExpressionValidator id="revNBF_BENNAME" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect"
								ControlToValidate="txtNBF_BENNAME" ValidationExpression="[A-Za-z\s]+"></asp:RegularExpressionValidator>
							<asp:requiredfieldvalidator id="rfvNBF_BENNAME" runat="server" ErrorMessage="Required" ControlToValidate="txtNBF_BENNAME"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<shma:TextBox id="txtNBF_BENNAMEARABIC" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" Width='14.0pc' readonly=true Text='<%#DataBinder.Eval(Container, "DataItem.NBF_BENNAMEARABIC")%>' MaxLength="60" CssClass="RequiredField" BaseType="Character">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNBF_BENNAMEARABIC" runat="server" ControlToValidate="txtNBF_BENNAMEARABIC"
								Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
								Display="Dynamic"></asp:CompareValidator>
							<shma:TextBox id="txtNBF_IDNO" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" onfocus="deFormatNIC(this)" onblur="validateNIC(this);formatNIC(this)" onKeyUp="formatNIC(this)" runat="server" Width='14.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NBF_IDNO")%>' MaxLength="15" CssClass="RequiredField" BaseType="Character">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNBF_IDNO" runat="server" ControlToValidate="txtNBF_IDNO"
								Operator="DataTypeCheck" Type="Integer" ErrorMessage="NIC Format is Incorrect " Enabled="False"
								Display="Dynamic"></asp:CompareValidator>
						</ItemTemplate>
						<FooterTemplate>
							<span style="position: relative;"><span id="capNewNBF_BENNAME" style="WIDTH: 14pc; HEIGHT: 2px; Z-INDEX: 101;" class="popup">
									<strong>
										<center>Beneficiary Name</center>
									</strong></span>
								<shma:TextBox id="txtNewNBF_BENNAME" runat="server" Width='14.0pc' tabIndex="1" MaxLength="60" CssClass="RequiredField"
									BaseType="Character"></shma:TextBox>
								<!--EntryGrid__ctl<%#Container.ItemIndex+2%>-->
								<shma:TextBox id="txtNewNBF_IDNO" runat="server" Width='14.0pc' tabIndex="2" MaxLength="15" CssClass="RequiredField" onblur="validateNIC(this);formatNIC(this)" onKeyUp="formatNIC(this)" onfocus="deFormatNIC(this)"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvNewNBF_IDNO" runat="server" ControlToValidate="txtNewNBF_IDNO"
									Operator="DataTypeCheck" Type="Integer" ErrorMessage="NIC Format is Incorrect" Enabled="False"
									Display="Dynamic"></asp:CompareValidator>
								<shma:TextBox id="txtNewNBF_BENNAMEARABIC" runat="server" Width='14.0pc' tabIndex="11" readonly=true MaxLength="60" CssClass="RequiredField"
									BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvNewNBF_BENNAMEARABIC" runat="server" ControlToValidate="txtNewNBF_BENNAMEARABIC"
									Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect (Arabic Name)" EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator>
								<asp:RegularExpressionValidator id="revNewNBF_BENNAME" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect"
								ControlToValidate="txtNewNBF_BENNAME" ValidationExpression="[A-Za-z\s]+"></asp:RegularExpressionValidator>								<shma:Peeredrequiredfieldvalidator id="prfvNewNBF_BENNAME" runat="server" ErrorMessage="Required (Beneficiary Name)"
									ControlToValidate="txtNewNBF_BENNAME" ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
								<iframe id='icapNewNBF_BENNAMEARABIC' src="" scrolling="no" frameborder="0" style="position:absolute;width:14pc;height:24px;top:17px;left:0px;border:none;display:block;z-index:0"
									class="popup"></iframe><span id="capNewNBF_BENNAMEARABIC" style="WIDTH: 14pc; HEIGHT: 2px; position:absolute; left:0px; top:20; Z-INDEX: 101;"
									class="popup"><strong>
										<center>
											Arabic Name</center>
									</strong></span></span>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date of Birth &lt;BR&gt; Distribution of DB">
						<ItemTemplate>
							<SHMA:DateBox id="txtNBF_DOB" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" maxlength="10" Width='7.0pc' CssClass="RequiredField" Text='<%#DataBinder.Eval(Container, "DataItem.NBF_DOB")%>' >
							</SHMA:DateBox>
<asp:comparevalidator style="Z-INDEX: 0" id="Comparevalidator13" ValueToCompare = '<%# DateTime.Now.ToString("dd/MM/yyyy") %>' runat="server" Display="Dynamic" Operator="LessThanEqual"
										errormessage="The date must be less than today" controltovalidate="txtNBF_DOB" type="Date"></asp:comparevalidator>
							<asp:CompareValidator id="cfvNBF_DOB" runat="server" ControlToValidate="txtNBF_DOB" Operator="DataTypeCheck"
								Type="Date" ErrorMessage="Date Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
							<asp:CompareValidator id="msgNBF_DOB" runat="server" ControlToValidate="txtNBF_DOB" Operator="DataTypeCheck"
								CssClass="CalendarFormat" Type="Date" ErrorMessage="{dd/mm/yyyy} " Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNBF_DOB" runat="server" ErrorMessage="Required" ControlToValidate="txtNBF_DOB"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<shma:TextBox id="txtNBF_AGE" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" Width='7.0pc' Text='<%# DataBinder.Eval(Container, "DataItem.NBF_AGE").ToString().Replace(".00","") %>' ReadOnly="true" MaxLength="6" BaseType="Number">
							</shma:TextBox>
						</ItemTemplate>
						<FooterTemplate>
							<span style="position: relative;"><span id="capNewNBF_DOB" style="WIDTH: 7pc; HEIGHT: 8px; Z-INDEX: 101;" class="popup">
									<strong>
										<center>Date of Birth</center>
									</strong></span>
								<SHMA:DateBox id="txtNewNBF_DOB" runat="server" maxlength="10" Width='7.0pc' tabIndex="2" CssClass="RequiredField"></SHMA:DateBox>
								<span style="position: relative;"><span id="capNewNBF_AGE" style="WIDTH: 7pc; HEIGHT: 8px; Z-INDEX: 101;" class="popup">
										<strong>
											<center>Age</center>
										</strong></span>
									<shma:TextBox id="txtNewNBF_AGE" runat="server" Width='7.0pc' tabIndex="3" ReadOnly="true" MaxLength="6" BaseType="Number"></shma:TextBox>
									<asp:CompareValidator id="cfvNewNBF_AGE" runat="server" ControlToValidate="txtNewNBF_AGE" Operator="DataTypeCheck"
										Type="Double" ErrorMessage="Number Format is Incorrect (Age)" Display="Dynamic"></asp:CompareValidator>
									<asp:CompareValidator id="cfvNewNBF_DOB" runat="server" ControlToValidate="txtNewNBF_DOB" Operator="DataTypeCheck"
										Type="Date" ErrorMessage="Date Format is Incorrect (D.O.B.)" Display="Dynamic"></asp:CompareValidator>
									<shma:Peeredrequiredfieldvalidator id="prfvNewNBF_DOB" runat="server" ErrorMessage="Required (D.O.B.)" ControlToValidate="txtNewNBF_DOB"
										ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
<asp:comparevalidator style="Z-INDEX: 0" id="Comparevalidator12" ValueToCompare = '<%# DateTime.Now.ToString("dd/MM/yyyy") %>' runat="server" Display="Dynamic" Operator="LessThanEqual"
										errormessage="The date must be less than today" controltovalidate="txtNewNBF_DOB" type="Date"></asp:comparevalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Age &lt;br&gt;Percent">
						<ItemTemplate>
							<SHMA:dropdownlist id="ddlCRL_RELEATIOCD" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CRL_RELEATIOCD"
								Width="13.5pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:requiredfieldvalidator id="rfvCRL_RELEATIOCD" runat="server" ErrorMessage="Required" ControlToValidate="ddlCRL_RELEATIOCD"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<SHMA:dropdownlist id="ddlNBF_BASIS" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="csd_type"
								Width="13.5pc" CssClass="RequiredField"></SHMA:dropdownlist>
							<asp:requiredfieldvalidator id="rfvNBF_BASIS" runat="server" ErrorMessage="Required" ControlToValidate="ddlNBF_BASIS"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<span style="position: relative;">
								<iframe id='icapNewCRL_RELEATIOCD' src="" scrolling="no" frameborder="0" style="position:absolute;width:13.5pc;height:20px;top:0px;left:0px;border:none;display:block;z-index:0" class="popup"></iframe>
								<span id="capNewCRL_RELEATIOCD" style="WIDTH: 13.5pc; position:absolute; left:0px; top:0; border:solid 1px black;z-index:0" class="popup">
									<strong>
										<center>Relation</center>
									</strong>
								</span>
								<SHMA:dropdownlist id="ddlNewCRL_RELEATIOCD" runat="server" BlankValue="True" DataTextField="desc_f"
									DataValueField="CRL_RELEATIOCD" Width="13.5pc" tabIndex="4" CssClass="RequiredField"></SHMA:dropdownlist>
								<SHMA:dropdownlist id="ddlNewNBF_BASIS" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="csd_type"
									Width="13.5pc" tabIndex="5" CssClass="RequiredField"></SHMA:dropdownlist>
								<shma:Peeredrequiredfieldvalidator id="prfvNewNBF_BASIS" runat="server" ErrorMessage="Required (Distribution)" ControlToValidate="ddlNewNBF_BASIS"
									ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
								<iframe id='icapNewNBF_BASIS' src="" scrolling="no" frameborder="0" style="position:absolute;width:13.5pc;height:24px;top:22px;left:0px;border:none;display:block;z-index:0" class="popup"></iframe>
								<span id="capNewNBF_BASIS" style="position:absolute;width:13.5pc;height:24px;top:17px;left:0px;border:none;display:block;z-index:0" class="popup">
									<strong><center>Basis</center></strong>
								</span>
							</span>
							<shma:Peeredrequiredfieldvalidator id="prfvNewCRL_RELEATIOCD" runat="server" ErrorMessage="Required (Relationship)"
								ControlToValidate="ddlNewCRL_RELEATIOCD" ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Relationship">
						<ItemTemplate>
							<shma:TextBox id="txtNBF_AMOUNT" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" Width='7.0pc' readonly=true Text='<%#DataBinder.Eval(Container, "DataItem.NBF_AMOUNT")%>' MaxLength="15" CssClass="RequiredField" BaseType="Currency" style="text-align: right;">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNBF_AMOUNT" runat="server" ControlToValidate="txtNBF_AMOUNT" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNBF_AMOUNT" runat="server" ErrorMessage="Required" ControlToValidate="txtNBF_AMOUNT"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<shma:TextBox id="txtNBF_PERCNTAGE" onmouseover="showGaurdianInToolTip(this)" onmouseout="UnTip()" runat="server" Width='7.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NBF_PERCNTAGE")%>' MaxLength="6" CssClass="RequiredField" BaseType="Double" style="text-align: right;">
							</shma:TextBox>
							<asp:CompareValidator id="cfvNBF_PERCNTAGE" runat="server" ControlToValidate="txtNBF_PERCNTAGE" Operator="DataTypeCheck"
								Type="Double" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							<asp:requiredfieldvalidator id="rfvNBF_PERCNTAGE" runat="server" ErrorMessage="Required" ControlToValidate="txtNBF_PERCNTAGE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</ItemTemplate>
						<FooterTemplate>
							<span style="position: relative;">
								<shma:TextBox id="txtNewNBF_AMOUNT" runat="server" Width='7.0pc' tabIndex="12" readonly=true MaxLength="10" CssClass="RequiredField" 
									BaseType="Currency" style="text-align: right;"></shma:TextBox>
								<iframe id='icapNewNBF_AMOUNT' src="" scrolling="no" frameborder="3" style="position:absolute;width:7.0pc;height:4px;top:20px;left:0px;border:none;display:block;z-index:0"
									class="popup"></iframe><span id="capNewNBF_AMOUNT" style="WIDTH: 7pc; position:absolute; left:0px; top:0; border:solid 1px black;z-index:0"
									class="popup"><strong>
										<center>Amount</center>
									</strong></span>
								<shma:TextBox id="txtNewNBF_PERCNTAGE" runat="server" Width='7.0pc' tabIndex="6" MaxLength="6" CssClass="RequiredField" 
									style="text-align: right;" BaseType="Double"></shma:TextBox>
								<asp:CompareValidator id="cfvNewNBF_PERCNTAGE" runat="server" ControlToValidate="txtNewNBF_PERCNTAGE"
									Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect (Percent)" EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator>
								<shma:Peeredrequiredfieldvalidator id="prfvNewNBF_PERCNTAGE" runat="server" ErrorMessage="Required (Percent)" ControlToValidate="txtNewNBF_PERCNTAGE"
									ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
								<asp:CompareValidator id="cfvNewNBF_AMOUNT" runat="server" ControlToValidate="txtNewNBF_AMOUNT" Operator="DataTypeCheck"
									style="text-align: right;" Type="Currency" ErrorMessage="Number Format is Incorrect (Amount)" EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator>
								<shma:Peeredrequiredfieldvalidator id="prfvNewNBF_AMOUNT" runat="server" ErrorMessage="Required (Amount)" ControlToValidate="txtNewNBF_AMOUNT"
									ControlsToCheck="" Display="Dynamic"></shma:Peeredrequiredfieldvalidator>
								<iframe id='icapNewNBF_PERCNTAGE' src="" scrolling="no" frameborder="3" style="position:absolute;width:7.0pc;height:24px;top:20px;left:0px;border:none;display:block;z-index:0"
									class="popup"></iframe><span id="capNewNBF_PERCNTAGE" style="WIDTH: 7pc; position:absolute; left:0px; top:20; border:solid 1px black;z-index:0"
									class="popup"><strong>
										<center>Percent</center>
									</strong></span></span>
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</SHMA:DATAGRID>
			<!--<table cellspacing="1" cellpadding="2" bordercolor="Snow" border="0" style="color:Black;background-color:#eeeeee;border-color:Snow;border-width:2px;border-style:Outset;font-family:verdana;font-size:8pt;width:100px;">
			<tr><td onmouseover="this.style.background ='#255B97';" onclick="gotoFooterRow();" onmouseout="this.style.background ='#eeeeee';"><a title="Click to Add New Row" style="color:Black;width:100%;text-decoration:none;">Add</a></td></tr>
		</table>-->
			<input type="hidden" name="PkColumns" value="NP1_PROPOSAL,NBF_BENEFCD">
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 


		function beforeSave()
		{
			var validationFlag = true;
			//return validatePercentage();
			
			//alert(totalRecords);
			
			
			for (var i=1; i<=totalRecords; i++)
			{
				if(getTabularFieldByIndex(i,"NBF_BENNAME") == null )
					return true;

				if(Trim(getTabularFieldByIndex(i,"NBF_BENNAME").value) == "")
				{
					alert("Beneficiary Name is missing.");
					validationFlag = false;
					break;
				}
				
				if(Trim(getTabularFieldByIndex(i,"NBF_DOB").value) == "")
				{
					alert("Date of Birth is missing.");
					validationFlag = false;
					break;
				}

				if(Trim(getTabularFieldByIndex(i,"NBF_AGE").value) == "")
				{
					alert("Age is missing.");
					validationFlag = false;
					break;
				}	
				
				if(Trim(getTabularFieldByIndex(i,"CRL_RELEATIOCD").value) == "")
				{
					alert("Relation is missing.");
					validationFlag = false;
					break;
				}
				
				if(Trim(getTabularFieldByIndex(i,"NBF_BASIS").value) == "")
				{
					alert("Beneficiary Basis is missing.");
					validationFlag = false;
					break;
				}
				else
				{
					if(Trim(getTabularFieldByIndex(i,"NBF_BASIS").value) == "02")
					{
						
						if(Trim(getTabularFieldByIndex(i,"NBF_PERCNTAGE").value) == "")
						{
							alert("Beneficiary Pecent is missing.");
							validationFlag = false;
							break;	
						}
					}
					else if(Trim(getTabularFieldByIndex(i,"NBF_BASIS").value) == "01")
					{
						if(Trim(getTabularFieldByIndex(i,"NBF_AMOUNT").value) == "")
						{
							alert("Beneficiary Amount is missing.");
							validationFlag = false;
							break;	
						}
					}
				}
			}
			return validationFlag;
		}

		

		
		
		fcStandardFooterFunctionsCall();
		
		function addClicked()
		{
			setFixedValuesInSession ("FLAG_INSERTALLOWED=Y");
			send();
		}

		function applyNumberFormatPercent(obj, precision)
		{
			if(trim(obj.value) == "")
				return;

			obj.value = obj.value.replace('%','');
			if(eval(obj.value>100))
				obj.value = "100";
			
			
			var num = new NumberFormat();
			if(precision =="")
				precision=0;
			num.setNumber(obj.value);
			num.setPlaces(precision);
			
			num.setCommas(true);
			
			obj.value = num.toFormatted();
			obj.value = num.toFormatted()+'%';
		}



		function setDOB(obj, commncedate)
		{
			var index = obj.id.split('_')[2].replace('ctl','');
			if (index<=totalRecords)
				item_indexed = "EntryGrid__ctl" + index + "_txtNBF_AGE";
			else
			
			item_indexed = "EntryGrid__ctl" + index + "_txtNewNBF_AGE";
			
			var DateValue=dateDiffYears(obj.value, commncedate, parent.parent.ageRoundCriteria);
			
			if(DateValue=="NaN")
			{
			document.getElementById(item_indexed).value="";
			alert(DateValue);
			}
			else
			{
			document.getElementById(item_indexed).value = dateDiffYears(obj.value, commncedate, parent.parent.ageRoundCriteria);
		    }
		
		}
		
		setCurrencyTabular("txtNBF_AMOUNT");
		setPercentTabular("txtNBF_PERCNTAGE");

		//onexecute_EachTabularEntry("ddlNBF_BASIS", "setAmtPctStatus(?obj)");
		//validateTabular("ddlNBF_BASIS", "NBF_AMOUNT:=ddlNBF_BASIS=='01',NBF_PERCNTAGE:=ddlNBF_BASIS=='02'");
		
		parent.closeWait();
		
		
		//lazy load function passed in by reference to be modified and hooked to relevant invocation
		function validate()
		{
			if (document.getElementById('ddlNBF_BASIS').value=='01')
			{
				document.getElementById('txtNBF_PERCNTAGE').value = '';
				//document.getElementById('txtNBF_PERCNTAGE').style.visibility = "hidden";
				document.getElementById('txtNBF_PERCNTAGE').readOnly = true;
				document.getElementById('rfvNBF_PERCNTAGE').enabled = false;

				//document.getElementById('txtNBF_AMOUNT').style.visibility = "visible";
				document.getElementById('txtNBF_AMOUNT').readOnly = false;
				document.getElementById('rfvNBF_AMOUNT').enabled = true;
			}
			
			else if (document.getElementById('ddlNBF_BASIS').value=='02')
			{
				//document.getElementById('txtNBF_PERCNTAGE').style.visibility = "visible";
				document.getElementById('txtNBF_PERCNTAGE').readOnly = false;

				document.getElementById('rfvNBF_PERCNTAGE').enabled = true;

				document.getElementById('txtNBF_AMOUNT').value = '';
				//document.getElementById('txtNBF_AMOUNT').style.visibility = "hidden";
				document.getElementById('txtNBF_AMOUNT').readOnly = true;
				document.getElementById('rfvNBF_AMOUNT').enabled = false;
			}
			else
			{
				document.getElementById('txtNBF_PERCNTAGE').value = '';
				//document.getElementById('txtNBF_PERCNTAGE').style.visibility = "hidden";
				document.getElementById('txtNBF_PERCNTAGE').readOnly = true;
				document.getElementById('rfvNBF_PERCNTAGE').enabled = false;

				//document.getElementById('txtNBF_AMOUNT').style.visibility = "visible";
				document.getElementById('txtNBF_AMOUNT').readOnly = true;

				document.getElementById('rfvNBF_AMOUNT').enabled = false;				
			}
		}

		//clean application by applying a lazy load function mechanism for complex business logics
		function setAmtPctStatus(obj)
		{
			applyValidationSingleTabular(obj,validate,"ddlNBF_BASIS,txtNBF_AMOUNT,txtNBF_PERCNTAGE,rfvNBF_AMOUNT,rfvNBF_PERCNTAGE");
			//this.Page_ClientValidate();
		}
		
		applyValidationTabular(validate,"ddlNBF_BASIS,txtNBF_AMOUNT,txtNBF_PERCNTAGE,rfvNBF_AMOUNT,rfvNBF_PERCNTAGE"); 


		/*function setToolTip()
		{
			for ( a=1; a<=totalRecords-1; a++)
			{
				
				var dob = getTabularFieldByIndex(a, "NBF_DOB");
				dob.title = "Date format {dd/mm/yyyy}"; 
			}
			return true;
		}
		
		
		setToolTip();*/
		
		attachViewByNameTabular('NBF_DOB');
		
		function attachCaptionsTabular(item)
		{
			var caption = '';
			if (item.indexOf('NBF_BENNAME')!=-1)
				caption = 'Beneficiary Name';

			if (item.indexOf('NBF_BENNAMEARABIC')!=-1)
				caption = 'Arabic Name';

			if (item.indexOf('NBF_DOB')!=-1)
				caption = 'Date of Birth';

			if (item.indexOf('NBF_BASIS')!=-1)
				caption = 'Distribution of DB';

			if (item.indexOf('NBF_AGE')!=-1)
				caption = 'Age';

			if (item.indexOf('NBF_AMOUNT')!=-1)
				caption = 'Amount';

			if (item.indexOf('CRL_RELEATIOCD')!=-1)
				caption = 'Relation';

			if (item.indexOf('NBF_PERCNTAGE')!=-1)
				caption = 'Percent';
				
				
			document.getElementById(item).attachEvent('onfocus',  new Function("function1('"+caption+"')"));
			

		}

		attachViewFocusTabular('INPUT');
		attachViewFocusTabular('SELECT');


		function attachCaptions(tag)
		{
			var t = document.getElementsByTagName(tag);
			var i;
			for(i=0;i<t.length;i++)
			{
				if(t[i].id!='' && t[i].id.indexOf('New')!=-1)
				{
					cap = t[i].id.replace('txt','cap').replace('ddl','cap');
					cap = cap.replace('EntryGrid__ctl','');
					cap = cap.substring(2);
					
					
					var wap = document.getElementById(cap);

					//EntryGrid__ctl5_capNewNBF_BENNAME
					

					if(wap!=null)
					{
						obj = document.getElementById(t[i].id);
						
						//if(obj!=null) alert(wap.id);
						
						obj.attachEvent('onfocus',  new Function("HidePop('"+obj.id+"')"));

						obj.attachEvent('onblur',  new Function("ShowPop('"+obj.id+"')"));
						
						//alert(obj.id);
						//obj.style.visibility = "hidden";

						wap.attachEvent('onfocus',  new Function("FocusCtl('"+obj.id+"')"));
					}
				
				}
			}
		}
		
		
		attachCaptions('INPUT');
		attachCaptions('SELECT');
		//alert(getElementById('txtNBF_BENNAME').height);
		
		function checkNumber(arg){
			
			var pvalue = arg.value;
			pvalue =pvalue.replace("%","");
			if(isNaN(pvalue)){
				alert("Please enter valid values(1% - 100%)");
				//arg.value="100";
				arg.focus();
				return false;
			}
			if(eval(pvalue)<=0||eval(pvalue)>100){
				alert("Please enter valid values(1% - 100%)");
				//arg.value="100";
				arg.focus();
				return false;
			}
			return true;
		}
		function validatePercentage()
		{
//Izhar Ul Haque

//check which function has to call
////////////////////

			var blnreturn ;
            var objid = "txtNBF_AMOUNT";
			var val = 0;
			var item = "";
			//alert(eval(totalRecords));
				for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
			
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;

				if (document.getElementById(item) !=null && document.getElementById(item).value !='')
					val += eval(document.getElementById(item).value.replace(',',''));
			}
			
		    item = "EntryGrid__ctl"+eval(totalRecords+1)+"_"+"txtNewNBF_AMOUNT";
			if (document.getElementById(item) !=null && document.getElementById(item).value !='')
					val += eval(document.getElementById(item).value.replace(',',''));
			
			
			
			//alert(eval(val));
//////////////
if(eval(val)>0)
{
            var vals= Sumassured();
            blnreturn=vals;
           // alert(blnreturn);
}
else
{            
			
			var objid = "txtNBF_PERCNTAGE";
			var val = 0;
			var item = "";
			for (i=0;i<totalRecords-1;i++) 
			{
			
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;
                
				if (document.getElementById(item) !=null && document.getElementById(item).value !='')
					val += eval(document.getElementById(item).value.replace('%',''));
				
			}
			
			
			item = "EntryGrid__ctl"+eval(totalRecords+1)+"_"+"txtNewNBF_PERCNTAGE";
			if (document.getElementById(item) !=null && document.getElementById(item).value !='')
					val += eval(document.getElementById(item).value.replace('%',''));
			
			blnreturn = true;

			if (eval(val)>100)
			{
				alert('Total percentage '+val+' exceeds 100%');
				blnreturn = false;	
				
			}
			if (eval(val)>0 && eval(val)<100 )	
			{
			   alert('Total percentage '+val+'% must be equal to 100%');
			   blnreturn = false;
			}
//Sumassured Checking
}
			return blnreturn ;  //(100>=eval(val));



		}
//Function Check Total SumAssured Amount

		function Sumassured()
		{
		//Izhar Ul Haque
		          
					var sql = "select sum(npr_sumassured) SUMASSURED from lnpr_product where np1_proposal='"+document.getElementById('EntryGrid:_ctl2:lblNewNP1_PROPOSAL').value+"' "; 
					var result = fetchDataArray(sql);	       
					var SUMASSURED=result[1][0];
					var objid = "txtNBF_AMOUNT";
					var val = 0;
					var item = "";
					var blnreturn ;
					//alert(eval(totalRecords));
					for (i=0;i<totalRecords-1;i++) 
					{
						var id="000000"+i;
						id=id.substring(id.length-6);
					
						var item = "EntryGrid__ctl"+eval(eval(id)+2)+"_"+objid;

						if (document.getElementById(item) !=null && document.getElementById(item).value !='')
							val += eval(document.getElementById(item).value.replace(',',''));
					}
					
					item = "EntryGrid__ctl"+eval(totalRecords+1)+"_"+"txtNewNBF_AMOUNT";
					if (document.getElementById(item) !=null && document.getElementById(item).value !='')
							val += eval(document.getElementById(item).value.replace(',',''));
					
					blnreturn = true;

					if (eval(val)>eval(SUMASSURED))
					{
						alert('Total Amount '+val+' exceeds '+SUMASSURED+'');
						blnreturn = false;	
						
					}
						//alert(blnreturn);
					return blnreturn ;				
		}

		function openGuardian(proposal, beneficiary, guardian)
		{
			var wOpen;
			var sOptions;
			var aURL="../Presentation/shgn_ss_se_stdscreen_ILUS_ST_GUARDIAN.aspx?PROPOSAL=" + proposal + "&BENEFICIARY=" + beneficiary + "&GUARDIAN=" + guardian;
			var aWinName="ILUS_ET_TB_BENEFECIARY";

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /1.6).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /1.6).toString();
			sOptions = sOptions + ',left=200,top=150';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}

		/************** Overloaded method of SHMA Framework ******************/
		function callSend()
		{ 
			if (window.event.keyCode == 9)
			{
				//parent.frames[2].refreshNameField();
				//send();
				parent.frames[2].updated();
			}
		}
		
		function fccheckBoxClicked(objChckBox)
		{
			/*openGuardian(getTabularFieldByPeer(objChckBox,"NP1_PROPOSAL").value,
			             getTabularFieldByPeer(objChckBox,"NBF_BENEFCD").value,
			             getTabularFieldByPeer(objChckBox,"NGU_GUARDCD").value);
			*/
			
			//alert(getTabularFieldByIndex(1,"NP1_PROPOSAL").value);
			/*alert(getTabularFieldByPeer(objChckBox,"NP1_PROPOSAL").value);
			alert(getTabularFieldByPeer(objChckBox,"NBF_BENEFCD").value);
			alert(getTabularFieldByPeer(objChckBox,"NGU_GUARDCD").value);
			alert(getTabularFieldByPeer(objChckBox,"NBF_BENNAME").value);*/
		}
		
		function eventClick1()
		{
			//alert(obj);
		}
		
		function LoadGuardian(obj)
		{
			openGuardian(getTabularFieldByPeer(obj,"NP1_PROPOSAL").value,
			             getTabularFieldByPeer(obj,"NBF_BENEFCD").value,
			             getTabularFieldByPeer(obj,"NGU_GUARDCD").value);

		}
		
		function setRowsStyle()
		{
		
			for (var i=1; i<totalRecords; i++)
			{
				var row = parseInt(i) + 1;
				if(parseInt(getTabularFieldByIndex(i,"NBF_AGE").value) < parseInt(18) )
				{
					getTabularFieldByIndex(i,"NGU_NAME").value = loadGuardianInfo(getTabularFieldByIndex(i,"NP1_PROPOSAL").value, getTabularFieldByIndex(i,"NGU_GUARDCD").value);
					//Set Row Style Now
					getTabularFieldByIndex(i,"NBF_BENNAME").className = 'Prominent';
					getTabularFieldByIndex(i,"NBF_BENNAMEARABIC").className = 'Prominent';
					getTabularFieldByIndex(i,"NBF_DOB").className = 'Prominent';
					getTabularFieldByIndex(i,"NBF_AGE").className = 'Prominent';
					getTabularFieldByIndex(i,"NBF_AMOUNT").className = 'Prominent';
					getTabularFieldByIndex(i,"NBF_PERCNTAGE").className = 'Prominent';
					getTabularFieldByIndex(i,"CRL_RELEATIOCD").className = 'ProminentCombo';
					getTabularFieldByIndex(i,"NBF_BASIS").className = 'ProminentCombo';
					document.getElementById("EntryGrid:_ctl" + row + ":btnGuardian").className = 'ProminentCombo';
					document.getElementById("EntryGrid:_ctl" + row + ":chkDelete").className = 'ProminentCombo';
					
				}
				else
				{
					document.getElementById("EntryGrid:_ctl" + row + ":btnGuardian").style.visibility="hidden";
					
				}
				 
				getTabularFieldByIndex(i,"NP1_PROPOSAL").style.display="none";
				getTabularFieldByIndex(i,"NBF_BENEFCD").style.display="none";
				getTabularFieldByIndex(i,"NGU_GUARDCD").style.display="none";
				getTabularFieldByIndex(i,"NGU_NAME").style.display="none";
			}
		}
		
		function loadGuardianInfo(proposal, guardian)
		{
			var className="ace.ILUS_ST_GUARDIAN";
			var methodName="getGuardianInfo";
			var parameters = proposal + "," + guardian;
			var str_resultArray = executeClass(className + "," + methodName + "," + parameters); 
			
			var text = "";
			if(str_resultArray[0]!="")
			{
				text = "<TABLE border=1 Width='350px' cellspacing=0 cellpadding=0>";
				text += "<TR><td Width='70px'><b>Guardian:</b></td><td Width='280px'>" + str_resultArray[0] + " - " + str_resultArray[1] +  "</td></TR>";
				text += "<TR><td Width='70px'><b>Relation:</b></td><td Width='280px'>" + str_resultArray[2] + " - " + str_resultArray[3] + "</td></TR>";
				text += "</TABLE>";
			}
			return text;

		}
		
		setRowsStyle();
		

		function showGaurdianInToolTip(obj)
		{
			var age = parseInt(getTabularFieldByPeer(obj,"NBF_AGE").value);
			if(age < 18)
			{
				var text = "Guardian Not defined.";
				var guardCode = getTabularFieldByPeer(obj,"NGU_GUARDCD").value;
				
				if(ltrim(rtrim(guardCode)).length > 0)
				{
					text = getTabularFieldByPeer(obj,"NGU_NAME").value;
				}
				
				Tip(text, SHADOW, true, TITLE, 'Guardian Info', PADDING, 9);
			}
		}		
		</script>
	</body>
</HTML>
