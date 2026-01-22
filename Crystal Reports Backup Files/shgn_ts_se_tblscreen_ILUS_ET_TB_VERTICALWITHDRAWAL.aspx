<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TB_VERTICALWITHDRAWAL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TB_VERTICALWITHDRAWAL" %>
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
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/JScriptTabular.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/WebUIValidation.js'></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralView.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			totalRecords=<asp:Literal id="_totalRecords" runat="server" EnableViewState="False"></asp:Literal>+1;
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					
			//function SetStatus(rowStatus){					
			//	if (myForm.txtModifiedRows.value.indexOf(rowStatus) == -1)
			//		myForm.txtModifiedRows.value += rowStatus;
			//}			
		
		
			var totalPW = 0;
			var totalAD = 0;
		</script>
		<style type="text/css">

	TABLE.caption { BORDER-BOTTOM: #ccc 1px solid; BORDER-LEFT: #ccc 1px solid; WIDTH: 300px; BORDER-TOP: #ccc 1px solid; BORDER-RIGHT: #ccc 1px solid }

	TABLE.caption TD { BORDER-BOTTOM: #ccc 0px solid; HEIGHT: 18px }

		</style>
	</HEAD>
	<body>
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Table of Partial Withdrawals" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" runat="server">
			<asp:textbox id="txtModifiedRows" style="Z-INDEX: 103; POSITION: absolute; TOP: 80px; LEFT: 496px"
				runat="server" Width="0px" Height="12px"></asp:textbox><asp:textbox id="txtOrgCode" style="Z-INDEX: 102; POSITION: absolute; TOP: 208px; LEFT: 664px"
				runat="server" Width="0px"></asp:textbox><INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">&nbsp;
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="FIELD_COMBINATION" type="hidden" name="FIELD_COMBINATION" runat="server">
			<INPUT id="VALUE_COMBINATION" type="hidden" name="VALUE_COMBINATION" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick">
			<table style="POSITION: absolute; WIDTH: 1600px; DISPLAY: none; TOP: 10px; LEFT: 10px">
				<tr class="form_heading">
					<td height="20">Partial Withdrawal <INPUT type="button" class="BUTTON" value=".." title="maximize" onclick="opePOPS();">
					</td>
				</tr>
			</table>
			<table id="EntryGrid" style="POSITION: absolute; WIDTH: 1600px; DISPLAY: none; TOP: 40px; LEFT: 10px"
				class="caption">
				<tr>
					<td id="ddlPWREQUIRED">Do you want any Partial withdrawal</td>
				</tr>
				<tr>
					<td id="txtNoOfYears">How many payments
					</td>
				</tr>
				<tr>
					<td id="txtNPW_YEAR">Which policy year (put in order)</td>
				</tr>
				<tr>
					<td id="txtNPW_PW">Partial withdrawal amount to be paid</td>
				</tr>
				<tr>
					<td id="txtNPW_ALLOWAMOUNT">Maximum amount allowed</td>
				</tr>
				<tr>
					<td id="txtNPW_CUMWITHDRAWAL">Cummulative Withdrawal</td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM: #ccc 0px solid" id="ddlNPW_PURPOSE">Purpose</td>
				</tr>
			</table>
			<table style="DISPLAY:none">
				<tr>
					<td>
						<asp:DataList id="EntryGrid" runat="server" Width="200px" AutoGenerateColumns="False" ShowFooter="False"
							SelectedItemStyle-CssClass="GridSelRow" RepeatDirection="Vertical" RepeatColumns="10" style="POSITION: absolute; TOP: 40px; LEFT: 300px"
							CaptionAlign="Left" GridLines="Vertical">
							<SelectedItemStyle CssClass="GridSelRow"></SelectedItemStyle>
							<HeaderTemplate>
								<SHMA:dropdownlist id="ddlPWREQUIRED" runat="server" BlankValue="True" Width="5.0pc" CssClass="RequiredField"
									HighLighted="True">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="Y" Selected>Yes</asp:ListItem>
									<asp:ListItem Value="N">No</asp:ListItem>
								</SHMA:dropdownlist>
								<br>
								<asp:TextBox id="txtNoOfYears" runat="server" Width='5.0pc' MaxLength="17" CssClass="RequiredField"
									BaseType="Number" Precision="0" HighLighted="True" style="text-align:right"></asp:TextBox>
								<asp:CompareValidator id="cfvNoOfYears" runat="server" ControlToValidate="txtNoOfYears" Operator="DataTypeCheck"
									Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
								<asp:CheckBox id="chkSelectAll" runat="server" Visible="False"></asp:CheckBox>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:TextBox id="txtLASTCUMMPW" runat="server" Width='0.0pc' MaxLength="2" BaseType="Number"
									Precision="0" HighLighted="True"></asp:TextBox>
								<asp:TextBox id="txtALLOWEDEXCLUDECUMMPW" runat="server" Width='0.0pc' MaxLength="2" BaseType="Number"
									Precision="0" HighLighted="True"></asp:TextBox>
								<asp:TextBox id="txtNPW_YEAR_HIDE" runat="server" Width='0.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" >
								</asp:TextBox>
								<asp:TextBox id="lblNP1_PROPOSAL" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' runat="server" BaseType="Character">
								</asp:TextBox>
								<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="lblNP1_PROPOSAL" Operator="DataTypeCheck"
									Type="String" Enabled="False" ErrorMessage="String Format is Incorrect " EnableClientScript="False"
									Display="Dynamic"></asp:CompareValidator><!--<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox></CENTER>-->  <!--HeaderText="Year"-->
								<asp:TextBox id="txtNPW_YEAR" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" style="text-align:right">
								</asp:TextBox>
								<asp:CompareValidator id="cfvNPW_YEAR" runat="server" ControlToValidate="txtNPW_YEAR" Operator="DataTypeCheck"
									Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
								<!--HeaderText="Partial Withdrawal"-->
								<asp:TextBox id="txtNPW_PW" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_PW")%>' MaxLength="17" CssClass="RequiredField" BaseType="Number" Precision="0" HighLighted="True" style="text-align:right">
								</asp:TextBox>
								<asp:CompareValidator id="cfvNPW_PW" runat="server" ControlToValidate="txtNPW_PW" Operator="DataTypeCheck"
									Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvNPW_PW" runat="server" Precision="0" Enabled="False" ErrorMessage="Required"
									ControlToValidate="txtNPW_PW" Display="Dynamic"></asp:requiredfieldvalidator>
								<!--HeaderText="Allowed Amount"-->
								<asp:TextBox id="txtNPW_ALLOWAMOUNT" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_ALLOWAMOUNT")%>' MaxLength="17" BaseType="Number" Precision="0" HighLighted="True" ReadOnly=True CssClass="DisplayOnly" style="text-align:right">
								</asp:TextBox>
								<asp:CompareValidator id="cfvNPW_ALLOWAMOUNT" runat="server" ControlToValidate="txtNPW_ALLOWAMOUNT" Operator="DataTypeCheck"
									Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvNPW_ALLOWAMOUNT" runat="server" Precision="0" Enabled="False" ErrorMessage="Required"
									ControlToValidate="txtNPW_ALLOWAMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>
								<!--HeaderText="Cumulative Withdrawal"-->
								<asp:TextBox id="txtNPW_CUMWITHDRAWAL" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_CUMWITHDRAWAL")%>' ReadOnly="true" MaxLength="17" BaseType="Number" Precision="0" HighLighted="True" CssClass="DisplayOnly" style="text-align:right">
								</asp:TextBox>
								<asp:CompareValidator id="cfvNPW_CUMWITHDRAWAL" runat="server" ControlToValidate="txtNPW_CUMWITHDRAWAL"
									Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
								<!--HeaderText="Purpose"-->
								<asp:dropdownlist id="ddlNPW_PURPOSE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="csd_type"
									Width="5.0pc" CssClass="RequiredField" HighLighted="True"></asp:dropdownlist>
								<asp:requiredfieldvalidator id="rfvNPW_PURPOSE" runat="server" Enabled="False" ErrorMessage="Required" ControlToValidate="ddlNPW_PURPOSE"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</ItemTemplate>
						</asp:DataList>
					</td>
				</tr>
			</table>
			<table style="POSITION: absolute; WIDTH: 720px; TOP: 10px; LEFT: 10px">
				<tr class="form_heading">
					<td height="20">&nbsp;&nbsp;AVAP</td> <!-- Excess Premium -->
				</tr>
			</table>
			<table id="EntryGrid" style="POSITION: absolute; WIDTH: 1600px; TOP: 40px; LEFT: 10px"
				class="caption">
				<tr class="TRow_Normal">
					<td id="ddlPWREQUIRED_Adhoc">Account Value Accelaration Premiums(AVAP)</td> <!-- Do you want to pay Adhoc Excess Premium -->
				</tr>
				<tr class="TRow_Alt">
					<td id="txtNoOfYears_Adhoc">How many payments</td>
				</tr>
				<tr class="TRow_Normal">
					<td id="txtNPW_YEAR_ADHOC">Which policy year (put in order)</td>
				</tr>
				<tr class="TRow_Alt">
					<td id="txtNPW_ADHOCEXCESPRM">Adhoc Excess Premium to be paid</td>
				</tr>
				<tr class="TRow_Normal" style="DISPLAY: none">
					<td style="BORDER-BOTTOM: #ccc 0px solid" id="txtNPW_ADHOCEPPW">Adhoc EP less 
						Partial Withdrawal</td>
				</tr>
			</table>
			<asp:DataList id="EntryGrid_Adhoc" runat="server" Width="200px" AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>"  RepeatDirection="Vertical" RepeatColumns="10"  
					style="POSITION: absolute; TOP: 40px; LEFT: 310px" >
				<HeaderTemplate>
					<SHMA:dropdownlist id="ddlPWREQUIRED_Adhoc" runat="server" BlankValue="True" Width="5.0pc" CssClass="RequiredField"
						HighLighted="True">
						<asp:ListItem></asp:ListItem>
						<asp:ListItem Value="Y" Selected>Yes</asp:ListItem>
						<asp:ListItem Value="N">No</asp:ListItem>
					</SHMA:dropdownlist>
					<br>
					<asp:TextBox id="txtNoOfYears_Adhoc" runat="server" Width='5.0pc' MaxLength="17" CssClass="RequiredField"
						BaseType="Number" Precision="0" HighLighted="True" style="text-align:right"></asp:TextBox>
					<asp:CompareValidator id="cfvNoOfYears_Adhoc" runat="server" ControlToValidate="txtNoOfYears_Adhoc" Operator="DataTypeCheck"
						Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
					<CENTER>
						<asp:CheckBox id="Checkbox1" runat="server" Visible="False"></asp:CheckBox></CENTER>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:TextBox id="lblNP1_PROPOSAL_ADHOC" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' runat="server" BaseType="Character">
					</asp:TextBox><!--HeaderText="Year"-->
					<asp:TextBox id="txtNPW_YEAR_ADHOC" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" style="text-align:right">
					</asp:TextBox>
					<asp:TextBox id="txtNPW_YEAR_HIDE_AD" runat="server" Width='0.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" >
					</asp:TextBox>
					<!--HeaderText="Adhoc Access Premium"-->
					<asp:TextBox id="txtNPW_ADHOCEXCESPRM" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_ADHOCEXCESPRM")%>' MaxLength="17" CssClass="RequiredField" BaseType="Number" Precision="0" HighLighted="True" style="text-align:right">
					</asp:TextBox>
					<asp:CompareValidator id="cfvNPW_ADHOCEXCESPRM" runat="server" ControlToValidate="txtNPW_ADHOCEXCESPRM"
						Operator="DataTypeCheck" Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
					<asp:requiredfieldvalidator id="rfvNPW_ADHOCEXCESPRM" runat="server" Precision="0" Enabled="False" ErrorMessage="Required"
						ControlToValidate="txtNPW_ADHOCEXCESPRM" Display="Dynamic"></asp:requiredfieldvalidator>
					<!--HeaderText="Adhoc EP Less Partial Withdrawal"-->
					<asp:TextBox id="txtNPW_ADHOCEPPW" runat="server" Width='0pc' ReadOnly="true" MaxLength="17" Text='<%#DataBinder.Eval(Container, "DataItem.NPW_ADHOCEPPW")%>' CssClass="DisplayOnly" style="text-align:right" 
 BaseType="Number" Precision="0" HighLighted="True">
					</asp:TextBox>
					<asp:CompareValidator id="cfvNPW_ADHOCEPPW" runat="server" ControlToValidate="txtNPW_ADHOCEPPW" Operator="DataTypeCheck"
						Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
				</ItemTemplate>
			</asp:DataList>
			<table style="POSITION: absolute; WIDTH: 1600px; DISPLAY: none; TOP: 330px; LEFT: 10px">
				<tr class="form_heading">
					<td height="20">Switching Options</td>
				</tr>
			</table>
			<table id="EntryGrid" style="POSITION: absolute; WIDTH: 1600px; DISPLAY: none; TOP: 360px; LEFT: 10px"
				class="caption">
				<tr>
					<td id="txtNPW_YEAR_SWITCH">Which policy year (put in order)</td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM: #ccc 0px solid" id="ddlNPW_DEATHBENEFITOPTION">Death 
						benefit option switching</td>
				</tr>
			</table>
			<table style=" DISPLAY:none">
				<tr>
					<td>
						<asp:DataList id="EntryGrid_Switch" runat="server" Width="200px" AutoGenerateColumns="False" ShowFooter="<%#ShowFooter%>" SelectedItemStyle-CssClass="GridSelRow" RepeatDirection="Vertical" RepeatColumns="10"
					style="POSITION: absolute; TOP: 360px; LEFT: 300px" >
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<CENTER>
									<asp:TextBox id="lblNP1_PROPOSAL_SWITCH" style="width:0" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL") %>' runat="server" BaseType="Character">
									</asp:TextBox><!--HeaderText="Year"-->
									<asp:TextBox id="txtNPW_YEAR_SWITCH" runat="server" Width='5.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" style="text-align:right">
									</asp:TextBox>
									<asp:TextBox id="txtNPW_YEAR_HIDE_SW" runat="server" Width='0.0pc' Text='<%#DataBinder.Eval(Container, "DataItem.NPW_YEAR")%>' MaxLength="2" BaseType="Number" Precision="0" HighLighted="True" >
									</asp:TextBox>
									<!--HeaderText="Death Benefit Option Switching"-->
									<SHMA:dropdownlist id="ddlNPW_DEATHBENEFITOPTION" runat="server" BlankValue="True" DataTextField="desc_f"
										DataValueField="csd_type" Width="5.0pc" CssClass="RequiredField" HighLighted="True"></SHMA:dropdownlist>
									<asp:requiredfieldvalidator id="rfvNPW_DEATHBENEFITOPTION" runat="server" ErrorMessage="Required" ControlToValidate="ddlNPW_DEATHBENEFITOPTION"
										Display="Dynamic" Enabled="false"></asp:requiredfieldvalidator>
								<!--HeaderText="Adhoc EP Less Partial Withdrawal"-->
							</ItemTemplate>
						</asp:DataList>
					</td>
				</tr>
			</table>
			<br>
			<input type="hidden" name="PkColumns" value="NPW_YEAR,NP1_PROPOSAL">
		</form>
		<script language="javascript">
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal> 
			<asp:literal id="processCall" runat="server" EnableViewState="True"></asp:literal> 
			fcStandardFooterFunctionsCall();
		function saveUpdateClicked()
		{
			send();
		}
		
		function setPWNoOfTransactions(val)
		{
			if (val=='N'){
				totalPW = document.getElementById('EntryGrid:_ctl0:txtNoOfYears').value;
				document.getElementById('EntryGrid:_ctl0:txtNoOfYears').value = 0;
				document.getElementById('EntryGrid:_ctl0:txtNoOfYears').disabled = true;
			}
			else
			{
				document.getElementById('EntryGrid:_ctl0:txtNoOfYears').value = totalPW;
				document.getElementById('EntryGrid:_ctl0:txtNoOfYears').disabled = false;
			}
		}

		function setADNoOfTransactions(val)
		{
			if (val=='N'){
				totalAD = document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc').value;
				document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc').value = 0;
				document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc').disabled = true;
			}
			else
			{
				document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc').value = totalAD;
				document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc').disabled = false;
			}
			
		}
		
		
		function opePOPS()
		{
			var wOpen;
			var sOptions;
			var aURL="../Presentation/shgn_gp_gp_ILUS_ET_GP_WITHDRAWALPOPS.aspx";
			var aWinName="withdrawals";

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=yes,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth/1.5).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight/1.8).toString();
			sOptions = sOptions + ',left=200,top=200';

			wOpen = window.open( 'ABC', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}
		
		function setAllowed(index)
		{
			var intindex = eval(index)+1;
			var indexStr = "EntryGrid:_ctl" + intindex + ":";

			var intindexLast = eval(index);
			var indexStrLast = "EntryGrid:_ctl" + intindexLast + ":";

			//alert('setAllowed()-'+document.getElementById(indexStr+"txtALLOWEDEXCLUDECUMMPW").value+','+document.getElementById(indexStr+"txtLASTCUMMPW").value);

			document.getElementById(indexStr+"txtNPW_ALLOWAMOUNT").value = 
				eval(document.getElementById(indexStr+"txtALLOWEDEXCLUDECUMMPW").value) 
				- 
				eval(document.getElementById(indexStrLast+"txtNPW_CUMWITHDRAWAL")==null?0:getDeformattedNumber(document.getElementById(indexStrLast+"txtNPW_CUMWITHDRAWAL"),2));
				
				
			applyNumberFormat(document.getElementById(indexStr+"txtNPW_ALLOWAMOUNT"),2);
			
		}
		
		function setCummulative(index)
		{
			var intindex = eval(index)+1;
			var indexStr = "EntryGrid:_ctl" + intindex + ":";

			var intindexLast = eval(index);
			var indexStrLast = "EntryGrid:_ctl" + intindexLast + ":";


			document.getElementById(indexStr+"txtNPW_CUMWITHDRAWAL").value = 
				eval(getDeformattedNumber(document.getElementById(indexStr+"txtNPW_PW"),2)) 
				+ 
				eval(document.getElementById(indexStrLast+"txtNPW_CUMWITHDRAWAL")==null?0:getDeformattedNumber(document.getElementById(indexStrLast+"txtNPW_CUMWITHDRAWAL"),2));
				
			applyNumberFormat(document.getElementById(indexStr+"txtNPW_CUMWITHDRAWAL"),2);

		}

		function setAdhocEPLessPW(index)
		{
			// remarked 19/09/2008
			/*
			var intindex = eval(index)+1;
			var indexStr = "EntryGrid:_ctl" + intindex + ":";
			var indexStrAd = "EntryGrid_Adhoc:_ctl" + intindex + ":";

			document.getElementById(indexStrAd+"txtNPW_ADHOCEPPW").value = 
				eval(getDeformattedNumber(document.getElementById(indexStr+"txtNPW_PW"),2)) 
				- 
				eval(getDeformattedNumber(document.getElementById(indexStrAd+"txtNPW_ADHOCEXCESPRM"),2));
			applyNumberFormat(document.getElementById(indexStrAd+"txtNPW_ADHOCEPPW"),2);
			*/

		    //code by nawab
			var intindex = eval(index)+1;
			var indexStrAd = "EntryGrid_Adhoc:_ctl" + intindex + ":";
			
			var objAdhocExcPrem = document.getElementById(indexStrAd+"txtNPW_ADHOCEXCESPRM");
			if(getDeformattedNumber(objAdhocExcPrem, 2) < 100000)
			{
				alert("Minimum value for Adhoc Excess Premium is 100,000.00");
				objAdhocExcPrem.value = 100000.00;
			}
			
			
			document.getElementById(indexStrAd+"txtNPW_ADHOCEPPW").value =  "0";
			
			
			for (i=1;i<=10;i++) 
			{
			    if(document.getElementById(indexStrAd+"txtNPW_YEAR_ADHOC").value == document.getElementById("EntryGrid:_ctl"+ i + ":txtNPW_YEAR").value)
			    {			       
			      var indexStr = "EntryGrid:_ctl" + i + ":";
			      document.getElementById(indexStrAd+"txtNPW_ADHOCEPPW").value = 
				      eval(getDeformattedNumber(document.getElementById("EntryGrid:_ctl"+ i + ":txtNPW_PW"),2)) 
				      - 
				      eval(getDeformattedNumber(document.getElementById(indexStrAd+"txtNPW_ADHOCEXCESPRM"),2));
					break;
			    }
			}


			applyNumberFormat(document.getElementById(indexStrAd+"txtNPW_ADHOCEPPW"),2);
			//end code by nawab
			
				
		}

		function performViewEntryGrid(tag)
		{
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid__ctl"+eval(eval(id)+1)+"_";
				//alert(item+"ddlNPW_PURPOSE");


				if (tag=='N')
				{
					document.getElementById(item+"ddlNPW_PURPOSE").disabled=true;
					document.getElementById(item+"txtNPW_PW").disabled=true;
					document.getElementById(item+"txtNPW_YEAR").readonly=true;

					document.getElementById(item+"txtNPW_ALLOWAMOUNT").value="";
					document.getElementById(item+"txtNPW_CUMWITHDRAWAL").value="";
					document.getElementById(item+"ddlNPW_PURPOSE").value="";
					document.getElementById(item+"txtNPW_PW").value="";
					document.getElementById(item+"txtNPW_YEAR").value="";
					
					document.getElementById(item+"txtNPW_PW").className="DisplayOnly";
					document.getElementById(item+"txtNPW_YEAR").className="DisplayOnly";
				}
				else if (tag=='Y')
				{
					if (eval(i) >= eval(document.getElementById("EntryGrid:_ctl0:txtNoOfYears").value))
					{
						document.getElementById(item+"ddlNPW_PURPOSE").disabled=true;
						document.getElementById(item+"txtNPW_PW").disabled=true;
						document.getElementById(item+"txtNPW_YEAR").readonly=true;

						document.getElementById(item+"txtNPW_ALLOWAMOUNT").value="";
						document.getElementById(item+"txtNPW_CUMWITHDRAWAL").value="";
						document.getElementById(item+"ddlNPW_PURPOSE").value="";
						document.getElementById(item+"txtNPW_PW").value="";
						document.getElementById(item+"txtNPW_YEAR").value="";
						
						
						document.getElementById(item+"txtNPW_PW").className="DisplayOnly";
						document.getElementById(item+"txtNPW_YEAR").className="DisplayOnly";
					}
					else
					{
						document.getElementById(item+"ddlNPW_PURPOSE").disabled=false;
						document.getElementById(item+"txtNPW_PW").disabled=false;
						document.getElementById(item+"txtNPW_YEAR").readonly=false;
						
						document.getElementById(item+"txtNPW_PW").className="RequiredField";
						document.getElementById(item+"txtNPW_YEAR").className="";

						applyNumberFormat(document.getElementById(item+"txtNPW_ALLOWAMOUNT"), 2);		
						applyNumberFormat(document.getElementById(item+"txtNPW_CUMWITHDRAWAL"), 2);		
						applyNumberFormat(document.getElementById(item+"txtNPW_PW"), 2);		

					}
				}
				
			}
		}


		function performViewEntryGrid_Adhoc(tag)
		{
			var objNoOfYears = document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc');
			if (parseInt(objNoOfYears.value) > 10)
			{
				alert("Payments cannot be Greater than 10 ");
				objNoOfYears.value = 10 ;
			}
			
			for (i=0;i<totalRecords-1;i++) 
			{
				var id="000000"+i;
				id=id.substring(id.length-6);
				var item = "EntryGrid_Adhoc__ctl"+eval(eval(id)+1)+"_";
				//alert(item+"ddlNPW_PURPOSE");
				
				if (tag=='N')
				{
						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").disabled=true;
						document.getElementById(item+"txtNPW_YEAR_ADHOC").disabled=true;

						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").value="";
						document.getElementById(item+"txtNPW_YEAR_ADHOC").value="";
						document.getElementById(item+"txtNPW_ADHOCEPPW").value="";

						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").className="DisplayOnly";
						document.getElementById(item+"txtNPW_YEAR_ADHOC").className="DisplayOnly";
						
				}
				else if (tag=='Y')
				{
					if (eval(i) >= eval(document.getElementById("EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc").value))
					{
						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").disabled=true;
						document.getElementById(item+"txtNPW_YEAR_ADHOC").disabled=true;

						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").value="";
						document.getElementById(item+"txtNPW_YEAR_ADHOC").value="";
						document.getElementById(item+"txtNPW_ADHOCEPPW").value="";

						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").className="DisplayOnly";
						document.getElementById(item+"txtNPW_YEAR_ADHOC").className="DisplayOnly";


					}
					else
					{
						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").disabled=false;
						document.getElementById(item+"txtNPW_YEAR_ADHOC").disabled=false;
						//document.getElementById(item+"txtNPW_ADHOCEPPW").disabled=false;
						

						document.getElementById(item+"txtNPW_ADHOCEXCESPRM").className="RequiredField";
						document.getElementById(item+"txtNPW_YEAR_ADHOC").className="";
						
						applyNumberFormat(document.getElementById(item+"txtNPW_ADHOCEXCESPRM"), 2);		
						applyNumberFormat(document.getElementById(item+"txtNPW_ADHOCEPPW"), 2);	
						
							


					}
				}
				
			}
		}
		parent.closeWait();
		


		performViewEntryGrid('Y');
		performViewEntryGrid_Adhoc('Y');

		attachViewFocusTabularVertical('INPUT');
		attachViewFocusTabularVertical('SELECT');
		
			
		
		
		
		function SetInputStatusBasedOnProduct()
		{
			//alert(1);
			if(product == "921" ||	product == "922" || product == "923")
			{
				  
				  //var objNoOfYears = document.getElementById('EntryGrid_Adhoc:_ctl0:txtNoOfYears_Adhoc');
				  //objNoOfYears.value = 0;
				  //objNoOfYears.disabled = true;
				  
				  var objAhocPrem = document.getElementById('EntryGrid_Adhoc__ctl0_ddlPWREQUIRED_Adhoc');
				  objAhocPrem.selectedIndex = 2;
				  objAhocPrem.value = "N";
				  setADNoOfTransactions(objAhocPrem.value);
				  performViewEntryGrid_Adhoc(objAhocPrem.value);
				  objAhocPrem.disabled = true;
				  
			}
		}
		
		
		SetInputStatusBasedOnProduct();
		

		
		</script>
	</body>
</HTML>
