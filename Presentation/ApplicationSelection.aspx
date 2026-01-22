<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page Language="c#" AutoEventWireup="True" Inherits="ApplicationSelection" CodeBehind="ApplicationSelection.aspx.cs" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>:: Bancassurance ::</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<script src="style/script.js" type="text/JavaScript"> </script>
		<LINK href="style/mytylesheet.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="JSFiles/JScriptFG.js"></script>
			<script language="JavaScript">
			</script>
			<LINK href="Styles/mytylesheet.css" type="text/css" rel="stylesheet">
				<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
					<style type="text/css">.style7 { FONT-FAMILY: Verdana, Tahoma, Arial, Helvetica, sans-serif; FONT-WEIGHT: bold }
	</style>
	<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
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
													
												</td>
												<td vAlign="top" width="497">
													<table cellSpacing="1" cellPadding="0" width="183" border="0">
														<tr>
															<td align="left" height="8">&nbsp;</td>
														</tr>
														<tr>
															<td align="left" height="20"><h2>Application Selection</h2>
															</td>
														</tr>
														<tr>
															<td align="left" height="14">&nbsp;</td>
														</tr>
														<tr>
															<td align="left" height="38">
																<asp:imagebutton id="ImagebuttonBanca" onclick="ImagebuttonBanca_Click" tabIndex="3" runat="server"
																	ImageUrl="..\Presentation\Images\ilasbanc.jpg" Width="160px" Height="30px"></asp:imagebutton>
															</td>
														</tr>
														<tr>
															<td align="left">&nbsp;</td>
														</tr>
														<tr>
															<td align="left" height="39">
																<asp:imagebutton id="ImagebuttonIllus" onclick="ImagebuttonIllus_Click" tabIndex="3" runat="server"
																	ImageUrl="..\Presentation\Theme_Illus\Images\ilasIllustrator.jpg" Width="160px" Height="30px"></asp:imagebutton>
															</td>
														</tr>
														<tr>
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
		</TD></TR></TBODY></TABLE>

		</form>
	</body>
</HTML>
