<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_MANUALADJUSTMENT.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_MANUALADJUSTMENT" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
			myForm.txtNP1_PROPOSAL.disabled=true;
			myForm.ddlPPR_PRODCD.disabled=true;

			if(myForm.ddlNLO_TYPE.disabled==true)
				 myForm.ddlNLO_TYPE.disabled=false;
				 
			myForm.ddlNLO_TYPE.selectedIndex =0;
			myForm.txtNLO_VALUE.value ="";

			myForm.ddlNLO_TYPE.focus();
			setDefaultValues();
		}
		
		/******** NOTE: *********************************************/
		/******** 1. Override the JScriptFG.js method.******************/
		/******** 2. Validation Error will be open in in Popup.*********/
		/******** 3. Normal Error will be alert only.*******************/
		/************************************************************/
		
		function ErrorMessage(errMsg)
		{
			if (errMsg=='<<Validation Error>>') 
			{   
				blnValidationError = true;
				//open Popup for Validation error
				var wOpen;
				var sOptions;
				var aURL="../Presentation/ValidationError.aspx?ErrorSource=Manual Adjustment Validation Error";
				var aWinName="VALIDATIONERROR";

				sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
				sOptions = sOptions + ',width=' + (screen.availWidth /1.8).toString();
				sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
				sOptions = sOptions + ',left=300,top=300';

				wOpen = window.open( '', aWinName, sOptions );
				wOpen.location = aURL;
				wOpen.focus();
				return wOpen;
			}
			else
			{	
				blnValidationError = false;
				//Normal alert for other than Validation error - Based on JScriptFG.js file
				var shortMessage = new String();
				var longMessage = new String();
				longMessage = shortMessage = errMsg;
				if(longMessage.indexOf("<ErrorMessage>",0)!=-1)
				{
					longMessage = longMessage.replace("<ErrorMessage>","Message:");
					longMessage = longMessage.replace("<ErrorMessageDetail>","\n\nDetail:");
					shortMessage = shortMessage.substring(("<ErrorMessage>").length ,shortMessage.indexOf("<ErrorMessageDetail>",0)) + "\n Dont Show Detail?";
					confirm(shortMessage)==false?alert(longMessage):"";
				}
				else
				{
					alert(errMsg);
				}
			}
		}		
		
		
		/********** dependent combo's queries **********/
		
		function openProposalLOV()
		{
			
			var wOpen;
			var sOptions;
			var aURL="../Presentation/ProposalSelectionLOV.aspx?SrcScreen=MANADJUSTMENT";
			var aWinName="PROPOSAL_LIST";

			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /1.5).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
			sOptions = sOptions + ',left=220,top=300';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}
		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Manual Adjustment
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<DIV style="POSITION: absolute; WIDTH: 600px; FONT-FAMILY: arial; HEIGHT: 380px; OVERFLOW: auto; TOP: 30px; LEFT: 16px"
				id="Div1" runat="server">
				<table cellSpacing="5" cellPadding="1" border="0">
					<tr>
						<td>Proposal
							<shma:TextBox id="txtNP1_PROPOSAL" tabIndex="3" runat="server" Width='165px' MaxLength="12" CssClass="RequiredField"
								ReadOnly="true" Enabled="False" BaseType="Character"></shma:TextBox>
							<INPUT type="button" class="BUTTON" value=".." title="Open Proposal List Of Values" name="ProposalLov"
								onclick="openProposalLOV();">
						</td>
						<td>Plan
							<SHMA:DROPDOWNLIST id="ddlPPR_PRODCD" tabIndex="2" runat="server" BlankValue="True" DataTextField="desc_f"
								Enabled="False" DataValueField="PPR_PRODCD" Width="184px" CssClass="RequiredField"></SHMA:DROPDOWNLIST>
						</td>
					</tr>
				</table>
			</DIV>
			<div style="MARGIN-TOP: 45px; MARGIN-LEFT: 320px" id="EntryTableDiv" runat="server">
				<table>
					<tr class="form_heading">
						<td height="20" colSpan="6">&nbsp; Entry</td>
					</tr>
				</table>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<SHMA:TextBox id="txtNP2_SETNO" runat="server" BaseType="Character" Value="1"></SHMA:TextBox>
					<SHMA:TextBox id="txtNLO_SUBTYPE" runat="server" BaseType="Character" Value="O"></SHMA:TextBox>
					<SHMA:TextBox id="txtNLO_SAPRNTPR" runat="server" BaseType="Character" Value="N"></SHMA:TextBox>
					<TR id='rowNLO_TYPE'>
						<TD width="120">Type</TD>
						<TD><SHMA:dropdownlist id="ddlNLO_TYPE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="NLO_TYPE"
								tabIndex="3" Width="17.0pc"></SHMA:dropdownlist>
							<asp:CompareValidator id="cfvNLO_TYPE" runat="server" ControlToValidate="ddlNLO_TYPE" Operator="DataTypeCheck"
								Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator></TD>
					<TR id='rowNLO_VALUE'>
						<TD width="120">Factor</TD>
						<TD><SHMA:TEXTBOX id="txtNLO_VALUE" tabIndex="4" runat="server" CssClass="RequiredField" MaxLength="5"
								Precision="2" BaseType="Number" subType="Currency"></SHMA:TEXTBOX>
							<asp:comparevalidator id="cfvNLO_VALUE" runat="server" ControlToValidate="txtNLO_VALUE" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="rfvNLO_VALUE" runat="server" ControlToValidate="txtNLO_VALUE" ErrorMessage="Required"
								Display="Dynamic"></asp:requiredfieldvalidator>
						<TD>
					<TR>
						<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<DIV style="POSITION: absolute; WIDTH: 300px; FONT-FAMILY: arial; HEIGHT: 380px; OVERFLOW: auto; TOP: 55px; LEFT: 16px"
				id="ListerDiv" runat="server">
				<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet"><legend class="LegendStyle">List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD onClick="filterLister('NLO_TYPE','Field')">Type</TD>
							<TD onClick="filterLister('NLO_DESC','Description')">Description</TD>
							<TD onClick="filterLister('NLO_VALUE','Factor')">Factor</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNLO_TYPE1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_TYPE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NLO_VALUE")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNLO_TYPE2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_TYPE")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NLO_VALUE")%></td>
								</tr>
							</AlternatingItemTemplate>
						</asp:repeater>
					</TABLE>
				</FIELDSET>
				Page no:
				<asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="Pager" onselectedindexchanged="pagerList_SelectedIndexChanged"></asp:dropdownlist>
			</DIV>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<INPUT type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
			<INPUT type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
			<script language="javascript">
				
			</script>
		</form>
		<script language="javascript">
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>
					if (_lastEvent == 'New')
						addClicked(); 	
					fcStandardFooterFunctionsCall();
					
			function beforeSave()
			{
				if(getField("NP1_PROPOSAL").value == "")
				{
					alert("Select Proposal first");
					//_lastEvent = 'New';
					window.location = window.location;
					return false;
				}

				if(getField("PPR_PRODCD").value == "")
				{
					alert("Select Proposal first");
					//_lastEvent = 'New';
					window.location = window.location;
					return false;
				}
				
				if(getField("NLO_TYPE").value == "")
				{
					alert("Select Type");
					window.location = window.location;
					return false;
				}

				if(getField("NLO_VALUE").value == "")
				{
					alert("Enter Factor");
					window.location = window.location;
					return false;
				}
				return true;
			}
			function beforeUpdate()
			{
				return beforeSave();
			}
			
		</script>
		<table style="POSITION: absolute; WIDTH: 425px; TOP: 288px; LEFT: 320px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
					<A class="button2" onclick="addClicked()" href="#">Add New</A> <A class="button2" onclick="saveClicked()" href="#">
						Save</A> <A class="button2" onclick="updateClicked()" href="#">Update</A> <A class="button2" onclick="deleteClicked()" href="#">
						Delete</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
