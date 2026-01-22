<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<TITLE>shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script src="../shmalib/jscript/Login.js" type="text/JavaScript"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<!-- <LINK rel="stylesheet" type="text/css" href="Styles/Style.css"> -->
		<%Response.Write(ace.Ace_General.loadInnerStyle());%>
		<asp:literal id="HeaderScript" runat="server" EnableViewState="True"></asp:literal>
	</HEAD>
	<body>
		<table>
			<tr class="form_heading">
				<td colSpan="6" height="20">&nbsp; User Details
				</td>
			</tr>
		</table>
		<form id="myForm1" name="myForm1" method="post" runat="server">
			<div id="NormalEntryTableDiv" style="Z-INDEX: 101" runat="server">
				<P><LEGEND style="COLOR: #336692"></LEGEND></P>
				<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
					<TR class="TRow_Normal" id="rowUSE_USERID">
						<TD style="WIDTH: 194px">Old Password</TD>
						<TD><shma:textbox id="txtUSE_OLDPWD" tabIndex="1" runat="server" BaseType="Character" MaxLength="15"
								Width="8.0pc" CssClass="RequiredField" TextMode="Password"></shma:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ControlToValidate="txtUSE_OLDPWD"
								ErrorMessage="Required"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Alt" id="rowCCN_CTRYCD">
						<TD style="WIDTH: 194px">New Password</TD>
						<TD><shma:textbox id="txtUSE_NEWPWD" onblur="showStrengthDiv(false,'NormalEntryTableDiv');" onkeyup="return passwordChanged(this);"
								onfocus="showStrengthDiv(true,'NormalEntryTableDiv');" tabIndex="2" runat="server" BaseType="Character"
								MaxLength="15" Width="8.0pc" TextMode="Password"></shma:textbox><asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" Display="Dynamic" ControlToValidate="txtUSE_NEWPWD"
								ErrorMessage="Required"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR class="TRow_Normal" id="rowUCN_DEFAULT">
						<TD style="WIDTH: 194px; HEIGHT: 11px">Confirm Password</TD>
						<TD style="HEIGHT: 11px"><shma:textbox id="txtUSE_CNFRMPWD" onblur="validatePwdOnLostFocus('txtUSE_NEWPWD','txtUSE_CNFRMPWD');"
								tabIndex="2" runat="server" BaseType="Character" MaxLength="15" Width="8.0pc" CssClass="RequiredField" TextMode="Password"></shma:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ControlToValidate="txtUSE_CNFRMPWD"
								ErrorMessage="Required"></asp:requiredfieldvalidator><asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="txtUSE_CNFRMPWD" ErrorMessage="Password Mismatch"
								ControlToCompare="txtUSE_NEWPWD" Enabled="False"></asp:comparevalidator></TD>
					</TR>
					<tr>
						<td>&nbsp;</td>
						<td><span id="strength">Type Password</span>
						</td>
					</tr>
					<TR>
						<td style="WIDTH: 194px">
							<P><asp:label id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red" Visible="False"></asp:label></P>
						</td>
						<TD>
							<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<IMG onclick="validateInput_();" height="20" src="images/savee.JPG" width="72" border="1">
								<asp:imagebutton id="btn_Save" style="DISPLAY: none" runat="server" ImageUrl="Images/savee.JPG" onclick="btn_Save_Click"></asp:imagebutton><asp:imagebutton id="btn_Cancel" runat="server" Width="72px" ImageUrl="Images/S1.jpg" CausesValidation="False"
									Height="20px" onclick="btn_Cancel_Click"></asp:imagebutton></P>
						</TD>
					</TR>
					<tr>
						<td></td>
					</tr>
				</TABLE>
			</div>
			<div id="stengthDiv" style="DISPLAY: none; LEFT: 20%; BACKGROUND-IMAGE: url(images/login_08.gif); FONT: 12px arial,sans-serif; MARGIN-LEFT: 20px; POSITION: absolute">
				<ul>
					<li>
					Minimum 8 characters in length
					<li>
						Contains 3/4 of the following items:<br>
						- Uppercase Letters<br>
						- Lowercase Letters<br>
						- Numbers<br>
						- Symbols<br>
					</li>
				</ul>
			</div>
			<INPUT style="WIDTH: 0px" id="_CustomArgName" name="_CustomArgName" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomArgVal" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server"> <INPUT style="WIDTH: 0px" id="_CustomEventVal" name="_CustomEventVal" runat="server">
			<INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<SCRIPT language="javascript">
			document.getElementById("txtUSE_OLDPWD").focus();		
			function validateInput_(){
				var isValidated = validateOldPwd('txtUSE_OLDPWD','txtUSE_NEWPWD','txtUSE_CNFRMPWD');
				if(isValidated){
					validatePassword('txtUSE_NEWPWD','txtUSE_CNFRMPWD','btn_Save');
				}
			}			
				
		</SCRIPT>
			<!--	<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall();</script> --><br>
			<table border="0" width="100%">
				<tr>
					<td align="right"><A href="#"></A>&nbsp; <A href="#"></A>&nbsp; <A href="#"></A>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
