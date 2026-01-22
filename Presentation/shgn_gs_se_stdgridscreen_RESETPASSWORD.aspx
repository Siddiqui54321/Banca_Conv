<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_gs_se_stdgridscreen_RESETPASSWORD.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gs_se_stdgridscreen_RESETPASSWORD" %>
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
		<script language="javascript" src="JSFiles/NumberFormat.js"></script
		>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
			_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
			
		/********** dependent combo's queries **********/
		

		</script>
</HEAD>
	<body >
		<form id="myForm" name="myForm" method="post" runat="server">
			<div id="NormalEntryTableDiv" style="Z-INDEX: 101;display:none" runat="server">
				
					<TABLE id="entryTable" cellSpacing="0" cellPadding="0" border="0">
						<TR id='row1' class="TRow_Normal">
							<TD width="16%">&nbsp;&nbsp;Password</TD>
							<TD nowrap><asp:textbox id="txtUSE_PASSWORD" CssClass="PasswordLoc" tabIndex="1" runat="server" MaxLength="50"  TextMode="Password"> </asp:textbox>
								   <asp:CompareValidator id="cfvUSE_PASSWORD" runat="server" Enabled="False"  ControlToValidate="txtUSE_PASSWORD" Operator="DataTypeCheck"  Type="String" ErrorMessage="String Format is Incorrect "  EnableClientScript="False"  Display="Dynamic"></asp:CompareValidator>
								   <asp:requiredfieldvalidator id="rfvUSE_PASSWORD"  runat="server" Enabled="False"  ErrorMessage="Required" ControlToValidate="txtUSE_PASSWORD"  Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>

						<TR id='row2' class="TRow_Alt">
							<TD width="16%">&nbsp;&nbsp;Confirm&nbsp;Password</TD>
							<TD nowrap><asp:textbox id="txtUSE_PASSWORD2" CssClass="PasswordLoc" tabIndex="2" runat="server" MaxLength="50"  TextMode="Password"> </asp:textbox>
								   <asp:CompareValidator id="cfvUSE_PASSWORD2" runat="server" Enabled="False"  ControlToValidate="txtUSE_PASSWORD2" Operator="DataTypeCheck"  Type="String" ErrorMessage="String Format is Incorrect "  EnableClientScript="False"  Display="Dynamic"></asp:CompareValidator>
								   <asp:requiredfieldvalidator id="rfvUSE_PASSWORD2"  Enabled="False" runat="server"  ErrorMessage="Required" ControlToValidate="txtUSE_PASSWORD2"  Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>
												
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P></td>
							<TD></TD>
						</TR>
					</TABLE>
			</div>
			
			<table style="LEFT: 10px; POSITION: absolute; TOP: 50px" border="0" cellPadding="0" width="100%">
				<tr style="BACKGROUND-COLOR:#f4f8fb">
					<td width="33%" > </td>
					<td align="left">
						<A class="button2" onclick="changePassword();" href="#">Reset Password</A> 
					<td align="right"></td>
				</tr>
			</table>

			
			<INPUT id="_CustomArgName" style="WIDTH: 0px; display:none;" type="text" name="_CustomArgName" runat="server" >
			<INPUT id="_CustomArgVal" style="WIDTH: 0px; display:none;" type="text" name="_CustomArgVal" runat="server" >&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT id="_CustomEvent" style="WIDTH: 0px; display:none;" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick"> 
			<INPUT id="_CustomEventVal" style="WIDTH: 0px; display:none;" name="_CustomEventVal" runat="server">
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		<script language="javascript">
			function changePassword()
			{
				if(validateInput())
					executeProcess('ace.ResetPassword');
			}
			
			function validateInput()
			{
				return true;
				var pwd1 = trim(document.getElementById('txtUSE_PASSWORD').value);
				var pwd2 = trim(document.getElementById('txtUSE_PASSWORD2').value);
				
				
				document.getElementById('txtUSE_PASSWORD').value = pwd1;
				document.getElementById('txtUSE_PASSWORD2').value = pwd2;
				
				if(pwd1.length == 0)
				{
					alert("Empty Password is not allowed.");
					return false;
				}
				
				if(pwd2.length == 0)
				{
					alert("Empty Password is not allowed.");
					return false;
				}
				
				if(pwd1 != pwd2)
				{
					alert("Passwords does not match.");
					return false;
				}
				
				return true;
			}
		</script>
		
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
