<%@ Page language="c#" Codebehind="GoalSeek.aspx.cs" AutoEventWireup="True" Inherits="ACE.Presentation.GoalSeek" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>GoalSeek</title>
	</HEAD>
	<BODY>
		<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; POSITION: absolute; TOP: 115px; HEIGHT: 25px">
			Please wait ... {0}
		</DIV>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		</SCRIPT>
		<form id="myForm" method="post" runat="server">
			<!--<FIELDSET id="FirstFldSt" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 632px; POSITION: absolute; TOP: 8px; HEIGHT: 343px">-->
			<TABLE id="FirstTab" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr1" class="TRow_Normal" runat="server">
					<TD id="TDddLTargetVal" style="WIDTH: 363px" align="right">Do you want to change 
						the Target value</TD>
					<TD width="186"><asp:dropdownlist id="ddLTargetVal" runat="server" Width="184px" BackColor="White" Font-Size="12px"
							Font-Names="Arial" Height="32px" AutoPostBack="True" onselectedindexchanged="ddLTargetVal_SelectedIndexChanged">
							<asp:ListItem Value="Y">Yes</asp:ListItem>
							<asp:ListItem Value="N" Selected="True">No</asp:ListItem>
						</asp:dropdownlist></TD>
					<td></td>
				</TR>
			</TABLE>
			<TABLE id="Tab2" cellSpacing="0" cellPadding="2" border="0" runat="server">
				<TR id="Tr2" class="TRow_Alt">
					<td style="WIDTH: 363px" align="right">Current value for
						<asp:Label id="lblTarget1" runat="server" Height="8px" Font-Names="Arial" Font-Size="12px"
							CssClass="TRow_Alt" /></td>
					<td width="186"><asp:textbox id="txtTRIval" style="TEXT-ALIGN: right" tabIndex="17" runat="server" Width="184px"
							ForeColor="Teal" BackColor="White" Enabled="False"></asp:textbox></td>
					<td></td>
				</TR>
			</TABLE>
			<TABLE id="Tab3" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr3" class="TRow_Normal">
					<td id="TDtxtTargetVal" style="WIDTH: 365px" align="right">Please enter the target value</td>
					<td Width="186">
						<asp:textbox id="txtTargetVal" style="TEXT-ALIGN: right" tabIndex="22" runat="server" BackColor="White"
							Width="184px"></asp:textbox></td>
					<td align="left">&nbsp; <a href="#">
							<asp:ImageButton id="btnTVal" runat="server" onmouseover="this.src='../shmalib/images/buttons/okSmall_2.gif'"
								onmouseout="this.src='../shmalib/images/buttons/okSmall_1.gif'" border="0" name="btncompute"
								alt="" src="../shmalib/images/buttons/okSmall_1.gif" onclick="btnTVal_Click" /></a>
					</td>
				</TR>
			</TABLE>
			<TABLE id="Tab4" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr4" class="TRow_Alt">
					<td id="TDddLChangingFactor" style="WIDTH: 363px" align="right">Please select the changing factor</td>
					<td width="186">
						<asp:dropdownlist id="ddLChangingFactor" runat="server" Height="32px" AutoPostBack="True" Font-Names="Arial"
							Font-Size="12px" BackColor="White" Width="184px" onselectedindexchanged="ddLChangingFactor_SelectedIndexChanged">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="EP">Excess Premium</asp:ListItem>
						</asp:dropdownlist></td>
					<td></td>
				</TR>
			</TABLE>
			<TABLE id="Tab5" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr5" class="TRow_Normal">
					<td style="WIDTH: 363px" align="right">Factor Value</td>
					<td width="186">
						<asp:textbox id="txtFactorVal" style="TEXT-ALIGN: right" tabIndex="17" runat="server" BackColor="White"
							Width="184px" Enabled="False" ForeColor="Teal"></asp:textbox></td>
					<td></td>
				</TR>
			</TABLE>
			<TABLE id="Tab6" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr6" class="TRow_Alt">
					<td align="right">
						<a href="#">
							<asp:ImageButton id="cmd_GoalSeek" runat="server" onmouseover="this.src='../shmalib/images/buttons/goalSeek_2.gif'"
								onmouseout="this.src='../shmalib/images/buttons/goalSeek_1.gif'" border="0" name="btncompute"
								alt="" src="../shmalib/images/buttons/goalSeek_1.gif" onclick="cmd_GoalSeek_Click" />
						</a>
					</td>
				</TR>
			</TABLE>
			<TABLE id="Tab7" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr7" class="TRow_Alt">
					<td style="WIDTH: 363px" align="right">
						Updated&nbsp;value for
						<asp:Label id="lblTarget2" runat="server" Height="8px" Font-Names="Arial" Font-Size="12px"
							CssClass="TRow_Alt" /></td>
					<td width="186">
						<asp:textbox id="txtTRIvalChanged" style="TEXT-ALIGN: right" tabIndex="17" runat="server" BackColor="White"
							Width="184px" Enabled="False" ForeColor="Teal"></asp:textbox></td>
					<td></td>
				</TR>
			</TABLE>
			<TABLE id="Tab8" runat="server" cellSpacing="0" cellPadding="2" border="0">
				<TR id="Tr8" class="TRow_Normal">
					<td style="WIDTH: 354px" align="right">Do you want to update values ?</td>
					<td width="182"></td>
					<td width="59"></td>
				</TR>
			</TABLE>
			<TABLE id="Tab9" runat="server" cellSpacing="0" cellPadding="2" border="0" style="Z-INDEX: 101; LEFT: 422px; WIDTH: 4.25%; POSITION: relative; TOP: 0px; HEIGHT: 38px">
				<TBODY>
					<TR id="Tr81">
						<td width="304" align="right">
							<a href="#">
								<asp:ImageButton id="cmdYes" accessKey="O" runat="server" onmouseover="this.src='../shmalib/images/buttons/ok_2.gif'"
									onmouseout="this.src='../shmalib/images/buttons/ok.gif'" border="0" name="btnok" alt="" src="../shmalib/images/buttons/ok.gif" onclick="cmdYes_Click"></asp:ImageButton>
							</a>
						</td>
						<td width="304" align="right">
							<a href="#">
								<asp:ImageButton id="cmdNo" accessKey="N" runat="server" onmouseover="this.src='../shmalib/images/buttons/cancel_2.gif'"
									onmouseout="this.src='../shmalib/images/buttons/cancel.gif'" border="0" name="btncancel" alt=""
									src="../shmalib/images/buttons/cancel.gif" onclick="cmdNo_Click"></asp:ImageButton></a></td>
					</TR>
				</TBODY>
			</TABLE>
			<!--</FIELDSET>-->
			<SCRIPT language="javascript">
			//document.Form1.ddLTargetVal.focus();
			
			function _callBack()
			{
				window.close();	
				//window.opener.location.href = window.opener.location.href;	
				window.opener.parent.frames[1].location.href = window.opener.parent.frames[1].location.href;	
				window.opener.parent.frames[2].location.href = window.opener.parent.frames[2].location.href;	
				//window.opener.parent.location.href = window.opener.parent.location.href;	
			}

			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>

			</SCRIPT>
			</A></TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
