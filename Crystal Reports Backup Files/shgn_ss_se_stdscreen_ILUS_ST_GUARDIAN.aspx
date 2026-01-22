<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ST_GUARDIAN.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ST_GUARDIAN" %>
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
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
			if(myForm.txtNGU_GUARDCD.disabled==true)
				 myForm.txtNGU_GUARDCD.disabled=false;
				 myForm.txtNGU_GUARDCD.value ="0";
							 myForm.txtNGU_NAME.value ="";
							 myForm.ddlCRL_RELEATIOCD.selectedIndex =0;
							 myForm.txtNGU_ADDRESS.value ="";
							 myForm.txtNGU_ADDRESS2.value ="";
							 myForm.txtNGU_ADDRESS3.value ="";
							 myForm.txtNGU_EMAIL.value ="";
							 myForm.txtNGU_IDNO.value ="";
							 myForm.txtNGU_TELENO.value ="";
							 myForm.txtNGU_FAX.value ="";
							 myForm.txtNGU_AGE.value ="0";
			
myForm.txtNGU_GUARDCD.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Guardian Info
				</td>
			</tr>
		</table>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div style="POSITION:absolute; WIDTH:600px; FONT-FAMILY:arial; HEIGHT:1000px; OVERFLOW:visible; TOP:105px; LEFT:16px"
				id="EntryTableDiv" runat="server">
				<fieldset><legend>Entry</legend>
					<TABLE id="entryTable" cellSpacing="2" cellPadding="0" border="0">
						<TR id='rowNGU_GUARDCD' class="TRow_Normal">
							<TD>Code</TD>
							<TD><SHMA:TextBox id="txtNGU_GUARDCD" tabIndex="1" runat="server" Width='4.0pc' MaxLength="6" BaseType="Number"
									SubType="FormattedNumber" readOnly="true" Precision="0" text="0" CssClass="RequiredField"></SHMA:TextBox>
								<asp:requiredfieldvalidator id="rfvNGU_GUARDCD" runat="server" ControlToValidate="txtNGU_GUARDCD" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator>
								<SHMA:CompareValidator id="cfvNGU_GUARDCD" runat="server" ControlToValidate="txtNGU_GUARDCD" Operator="DataTypeCheck"
									BaseType="Number" SubType="FormattedNumber" Precision="0"></SHMA:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_NAME' class="TRow_Alt">
							<TD>Name</TD>
							<TD><SHMA:TextBox id="txtNGU_NAME" tabIndex="2" runat="server" Width='18.0pc' MaxLength="60" BaseType="Character"
									CssClass="RequiredField"></SHMA:TextBox>
								<asp:requiredfieldvalidator id="rfvNGU_NAME" runat="server" ControlToValidate="txtNGU_NAME" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator>
								<asp:CompareValidator id="cfvNGU_NAME" runat="server" ControlToValidate="txtNGU_NAME" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowCRL_RELEATIOCD' class="TRow_Normal">
							<TD>Relation</TD>
							<TD><SHMA:dropdownlist id="ddlCRL_RELEATIOCD" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CRL_RELEATIOCD"
									tabIndex="3" Width="18.0pc" CssClass="RequiredField"></SHMA:dropdownlist>
								<asp:requiredfieldvalidator id="rfvCRL_RELEATIOCD" runat="server" ControlToValidate="ddlCRL_RELEATIOCD" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator>
								<asp:CompareValidator id="cfvCRL_RELEATIOCD" runat="server" ControlToValidate="ddlCRL_RELEATIOCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_ADDRESS' class="TRow_Alt">
							<TD>Address</TD>
							<TD><SHMA:TextBox id="txtNGU_ADDRESS" tabIndex="4" runat="server" Width='14.0pc' MaxLength="30" BaseType="Character"
									CssClass="RequiredField"></SHMA:TextBox>
								<asp:requiredfieldvalidator id="rfvNGU_ADDRESS" runat="server" ControlToValidate="txtNGU_ADDRESS" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator>
								<asp:CompareValidator id="cfvNGU_ADDRESS" runat="server" ControlToValidate="txtNGU_ADDRESS" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_ADDRESS2' class="TRow_Normal">
							<TD></TD>
							<TD><SHMA:TextBox id="txtNGU_ADDRESS2" tabIndex="5" runat="server" Width='14.0pc' MaxLength="30" BaseType="Character"></SHMA:TextBox>
								<asp:CompareValidator id="cfvNGU_ADDRESS2" runat="server" ControlToValidate="txtNGU_ADDRESS2" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_ADDRESS3' class="TRow_Alt">
							<TD></TD>
							<TD><SHMA:TextBox id="txtNGU_ADDRESS3" tabIndex="6" runat="server" Width='14.0pc' MaxLength="30" BaseType="Character"></SHMA:TextBox>
								<asp:CompareValidator id="cfvNGU_ADDRESS3" runat="server" ControlToValidate="txtNGU_ADDRESS3" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_EMAIL' class="TRow_Normal">
							<TD>Email</TD>
							<TD><SHMA:TextBox id="txtNGU_EMAIL" tabIndex="7" runat="server" Width='18.0pc' MaxLength="50" BaseType="Character"></SHMA:TextBox>
								<asp:regularexpressionvalidator style="Z-INDEX: 0" id="rgeNGU_EMAIL" runat="server" ErrorMessage="Email address not valid"
									ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ControlToValidate="txtNGU_EMAIL"></asp:regularexpressionvalidator>
								<asp:CompareValidator id="cfvNGU_EMAIL" runat="server" ControlToValidate="txtNGU_EMAIL" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_IDNO' class="TRow_Alt">
							<TD>NID</TD>
							<TD><SHMA:TextBox id="txtNGU_IDNO" tabIndex="8" runat="server" Width='7.0pc' MaxLength="20" BaseType="Character"></SHMA:TextBox>
								<asp:CompareValidator id="cfvNGU_IDNO" runat="server" ControlToValidate="txtNGU_IDNO" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowNGU_TELENO' class="TRow_Normal">
							<TD>Tel. No.</TD>
							<TD><SHMA:TextBox id="txtNGU_TELENO" tabIndex="9" runat="server" Width='6.0pc' MaxLength="12" BaseType="Character"></SHMA:TextBox>
								<asp:comparevalidator id="cfvNGU_TELENO" runat="server" ControlToValidate="txtNGU_TELENO" Operator="DataTypeCheck"
									Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
							</TD>
						</TR>
						<TR id='rowNGU_FAX' class="TRow_Alt">
							<TD>Fax.</TD>
							<TD><SHMA:TextBox id="txtNGU_FAX" tabIndex="10" runat="server" Width='6.0pc' MaxLength="12" BaseType="Character"></SHMA:TextBox>
								<asp:comparevalidator id="cfvNGU_FAX" runat="server" ControlToValidate="txtNGU_FAX" Operator="DataTypeCheck"
									Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
							</TD>
						</TR>
						<TR id='rowNGU_AGE' class="TRow_Normal">
							<TD>Age</TD>
							<TD><SHMA:TextBox id="txtNGU_AGE" tabIndex="11" runat="server" Width='3.0pc' MaxLength="3" BaseType="Number"
									SubType="FormattedNumber" Precision="0"></SHMA:TextBox>
								<SHMA:CompareValidator id="cfvNGU_AGE" runat="server" ControlToValidate="txtNGU_AGE" Operator="DataTypeCheck"
									BaseType="Number" SubType="FormattedNumber" Precision="0"></SHMA:CompareValidator>
							</TD>
						</TR>
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
						<SHMA:TextBox id="txtNP1_PROPOSAL" runat="server" BaseType="Character"></SHMA:TextBox>
						<asp:requiredfieldvalidator id="rfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" ErrorMessage="Required"
							Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:CompareValidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<SHMA:TextBox id="txtUSE_USERID" runat="server" BaseType="Character"></SHMA:TextBox>
						<asp:requiredfieldvalidator id="rfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID" ErrorMessage="Required"
							Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
						<SHMA:DateBox id="txtUSE_DATETIME" runat="server" BaseType="Date"></SHMA:DateBox>
						<asp:requiredfieldvalidator id="rfvUSE_DATETIME" runat="server" ControlToValidate="txtUSE_DATETIME" ErrorMessage="Required"
							Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:CompareValidator id="cfvUSE_DATETIME" runat="server" ControlToValidate="txtUSE_DATETIME" Operator="DataTypeCheck"
							Type="Date" ErrorMessage="Date Format is Incorrect " Display="Dynamic"></asp:CompareValidator>
					</TABLE>
				</fieldset>
			</div>
			<DIV style="POSITION:absolute; WIDTH:600px; FONT-FAMILY:arial; HEIGHT:90px; OVERFLOW:auto; TOP:20px; LEFT:16px"
				id="ListerDiv" runat="server">
				<FIELDSET style="FONT-FAMILY:arial; HEIGHT:80px; COLOR:#9cba62; FONT-WEIGHT:bold" runat="server"
					id="ListerFieldSet"><legend>List</legend>
					<TABLE class="Lister" cellSpacing="2" cellPadding="0" border="0">
						<TR class="ListerHeader">
							<TD width="15%" onClick="filterLister('NGU_GUARDCD','Code')">Guardian Code</TD>
							<TD width="30%" onClick="filterLister('NGU_NAME','Name')">Name</TD>
							<TD width="15%" onClick="filterLister('CRL_RELEATIOCD','Relation')">Relation Code
							</TD>
							<TD width="30%" onClick="filterLister('NGU_NAME','Name')">Name</TD>
						</TR>
						<asp:repeater id="lister" runat="server">
							<ItemTemplate>
								<tr class="ListerItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNGU_GUARDCD1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NGU_GUARDCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NGU_NAME")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CRL_RELEATIOCD")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CRL_DESCR")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="ListerAlterItem" id="ListerRow" runat="server">
									<td>
										<asp:linkbutton ID="linkNGU_GUARDCD2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.NGU_GUARDCD")%>' CausesValidation="false">
										</asp:linkbutton></td>
									<td><%# DataBinder.Eval(Container, "DataItem.NGU_NAME")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CRL_RELEATIOCD")%></td>
									<td><%# DataBinder.Eval(Container, "DataItem.CRL_DESCR")%></td>
								</tr>
							</AlternatingItemTemplate>
						</asp:repeater></TABLE>
				</FIELDSET>
				<!--Page no: <asp:dropdownlist id="pagerList" runat="server" AutoPostBack="True" CssClass="Pager"></asp:dropdownlist> -->
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
		
		/*function setAge(obj, commncedate)
		{
			var DateValue=dateDiffYears(obj.value, commncedate,"");
			
			if(DateValue=="NaN")
			{
				document.getElementById(item_indexed).value="";
				alert(DateValue);
			}
			else
			{
				getField("NGU_AGE").value = dateDiffYears(obj.value, commncedate, "");
		    }
		}*/
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();
		
		<asp:literal id="RefreshParent" runat="server" EnableViewState="True"></asp:literal>
		
		/***************************************************************************/
		/****************** Overloaded framework method  ***************************/
		/******************    change only for alert     ***************************/
		/***************************************************************************/
		function callEvent(eventType, arg, argValue){			
			if (eventType == 'Delete'){				
				if (confirm("Are you sure to Revoke this guardian from beneficiary?")==false)
					return false;			
			}		
			if (eventType == 'New')
				RefreshFields();		
			else{										
				myForm._CustomArgName.value = arg;
				myForm._CustomArgVal.value = argValue;
				myForm._CustomEventVal.value = eventType;
				if (eventType == 'Filter' || eventType == 'Delete')
					__doPostBack('_CustomEvent','');	
				else
					myForm._CustomEvent.onclick();
			
			return true;
			}
		}		
		function SaveGuardian()
		{
			if (_lastEvent == 'New')
			{
				saveClicked()
			}
			else
			{
				updateClicked()
			}
		}
		
		
		if(_lastEvent == 'Save')
		{
			_lastEvent = 'New';
		}
		
		</script>
		<table style="POSITION: absolute; WIDTH: 600px; TOP: 400px; LEFT: 16px" border="0" cellPadding="0"
			width="100%">
			<tr>
				<td align="right">
			<tr>
				<td align="right">
					<!-- <A class="button2" onclick="addClicked()" href="#">Add New</A>  -->
					<A class="button2" onclick="SaveGuardian()" href="#">Save And Assign</A> 
					<!-- <A class="button2" onclick="updateClicked()" href="#">Update</A> -->
					<A class="button2" onclick="deleteClicked()" href="#">Revoke</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
