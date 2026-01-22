<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page Language="c#" AutoEventWireup="True" Inherits="BranchSelection" CodeBehind="BranchSelection.aspx.cs" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>:: Bancassurance ::</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<script src="style/script.js" type="text/JavaScript"> </script>
		<LINK href="style/mytylesheet.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="JSFiles/JScriptFG.js"></script>
			<script language="JavaScript">
			</script>
			<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
			<LINK href="Styles/mytylesheet.css" type="text/css" rel="stylesheet">
				<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
					<style type="text/css">.style7 { FONT-FAMILY: Verdana, Tahoma, Arial, Helvetica, sans-serif; FONT-WEIGHT: bold }
	</style>
					<%Response.Write(ace.Ace_General.LoadGlobalStyle());%>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="myForm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td height="10"></td>
								</tr>
								<tr>
									<td class="top_bg"><!-- #BeginLibraryItem "/Library/top.lbi" -->
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td colspan="3" height="15"></td>
											</tr>
											<tr>
												<td vAlign="middle" align="left" width="30%">&nbsp;&nbsp;&nbsp;&nbsp;<A href="index.html"><IMG src="images/CompanyLogo.gif" border="0"></A></td>
												<td vAlign="middle" align="center" class="AppImage"></td>
												<td vAlign="top" align="left" width="30%">
													<asp:label style="Z-INDEX: 0" id="lblTestVersion" Runat="server" ForeColor="Gray" Font-Names="Verdana"
														Font-Size="XX-Small" Font-Bold="True"></asp:label>
													<br>
													<br>
													<br>
													<br>
													<asp:label id="lblVersion" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana" ForeColor="Gray"
														Runat="server"></asp:label></td>
											</tr>
										</table> <!-- #EndLibraryItem -->
									</td>
								</tr>
								<tr>
									<td background="images/top_bg_blue_line.gif" height="2"></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="780" border="0">
											<tr>
												<td vAlign="top" width="283">
													<table cellSpacing="1" cellPadding="0" width="183" border="0">
														<tr>
															<td width="9" height="11"></td>
															<td class="menu_button" width="183" rowSpan="5"><!-- #BeginLibraryItem "/Library/left.lbi" -->
																<table cellSpacing="1" cellPadding="0" width="183" border="0">
																	<tr>
																		<td class="LeftTopHeader" height="52" width="183"></td>
																	</tr>
																	<tr>
																		<td class="LeftTopDown" height="227" width="183"></td>
																	</tr>
																</table> <!-- #EndLibraryItem --></td>
														</tr>
														<tr>
															<td width="9" height="7"></td>
														</tr>
														<tr>
															<td width="9" height="10"></td>
														</tr>
														<tr>
															<td width="9" height="9"></td>
														</tr>
														<tr>
															<td width="9" height="2"></td>
														</tr>
														<tr>
															<td width="9"></td>
															<td>&nbsp;</td>
														</tr>
													</table>
												</td>
												<td vAlign="top" width="497">
													<table cellSpacing="1" cellPadding="0" width="183" border="0">
														<tr>
															<td align="left" height="8">&nbsp;</td>
														</tr>
														<tr>
															<td align="left" height="20"><B>Branch Selection</B></td>
														</tr>
														<tr>
															<td align="left" height="8">&nbsp;</td>
														</tr>
														<tr>
															<td align="left"><SHMA:dropdownlist id="ddlCCS_CODE" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField="CCS_CODE"
																	tabIndex="1" Width="480px" CssClass="RequiredField"></SHMA:dropdownlist></td>
														</tr>
														<tr>
															<td align="left">&nbsp;</td>
														</tr>
														<tr>
															<td align="right"><asp:imagebutton id="Imagebutton1" onclick="imbLoginButton_Click" tabIndex="3" runat="server" border="0"
																	ImageUrl="..\Presentation\Images\Go.png"></asp:imagebutton></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td></td>
								</tr>
							</table>
		</form>
		<script language="javascript">
		getField("CCS_CODE").focus();
		
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>

		function returnKeyPressed(obj_event)
		{
			if (obj_event.keyCode == 13)
			{
				//Imagebutton1.click();
				//imbLoginButton_Click
				 document.getElementById("Imagebutton1").click();
			}
		}
		</script>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
