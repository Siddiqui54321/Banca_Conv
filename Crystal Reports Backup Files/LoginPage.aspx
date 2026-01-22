<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>::
			<asp:Literal id="ltrlTitle" runat="server" EnableViewState="True"></asp:Literal>
			::</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	    <script src="~/Presentation/Styles/script.js" type="text/JavaScript"> </script>

        <script>
            function MM_preloadImages() { //v3.0
                var d = document; if (d.images) {
                    if (!d.MM_p) d.MM_p = new Array();
                    var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                        if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
                }
            }

            function MM_swapImgRestore() { //v3.0
                var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
            }

            function MM_findObj(n, d) { //v4.01
                var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                    d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
                }
                if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
                for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
                if (!x && d.getElementById) x = d.getElementById(n); return x;
            }

            function MM_swapImage() { //v3.0
                var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
                    if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
            }
            </script>

	<%--	<script src="~/shmalib/jscript/Login.js" type="text/JavaScript"></script>	--%>	
		

        
		<%Response.Write(ace.Ace_General.LoadGlobalStyle());%>
	</head>
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
												<td colSpan="3" height="15"></td>
											</tr>
                                          
                                            <tr>
												<td vAlign="middle" align="left" width="30%">&nbsp;&nbsp;&nbsp;&nbsp;
                                                <img src="../Presentation/images/CompanyLogo.gif" border="0" >
                                                </td>
												<td class="AppImage" vAlign="middle" align="center"></td>
												<td vAlign="top" align="left" width="30%"><asp:label id="lblTestVersion" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana" ForeColor="Gray"
														Runat="server"></asp:label><br>
													<br>
													<br>
													<br>
													<asp:label id="lblVersion" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana" ForeColor="Gray"
														Runat="server"></asp:label></td>
											</tr>
										</table> <!-- #EndLibraryItem --></td>
								</tr>
								<tr>
									<td class="line" height="2"></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="780" border="0">
											<tr>
												<td vAlign="top" width="283">
													<table cellSpacing="1" cellPadding="0" width="183" border="0">
														<tr>
															<td width="9" height="11"></td>
															<td class="menu_button" width="183" rowSpan="5">
																<table cellSpacing="1" cellPadding="0" width="183" border="0">
																	<tr>
																		<td class="LeftTopHeader" width="183" height="52"></td>
																	</tr>
																	<tr>
																		<td class="LeftTopDown" width="183" height="227"></td>
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
													<table cellSpacing="5" cellPadding="0" width="635" border="0">
														<tr>
															<td align="center">&nbsp;</td>
														</tr>
														<tr>
															<td align="center">&nbsp;</td>
														</tr>
														<tr>
															<td align="center">&nbsp;</td>
														</tr>
														<tr>
															<td align="center"><asp:imagebutton id="Imagebutton1" onclick="imbLoginButton_Click" tabIndex="3" runat="server" ImageUrl="~\Presentation\Images\mylogin_10.gif"
																	width="0" height="0" border="0"></asp:imagebutton><asp:label id="lblMsg" runat="server" Font-Bold="true" Font-Size="9" Font-Names="Arial" ForeColor="Red" style="DISPLAY:none"
																	Visible="True" BorderStyle="None" BorderColor="Transparent"></asp:label>
																<!--************ Login Box ***********--->
																<table cellSpacing="0" cellPadding="0" width="220" border="0">
																	<tr>
																		<td class="loginBox_TopLeft" colSpan="2" height="40"></td>
																		<td class="loginBox_TopCenttre" height="40"><span class="login">&nbsp;&nbsp;Login Information 
                              </span></td>
																		<td class="loginBox_TopRight" align="right" colSpan="3"></td>
																	</tr>
																	<tr>
																		<td class="loginBox_OnePixel" width="1" height="1"></td>
																		<td class="loginBox_bg" width="46">&nbsp;</td>
																		<td class="loginBox_bg" height="92">
																			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<tr>
																					<td class="yesno">Username</td>
																					<td>
																						<asp:textbox id="txtUserCode" tabIndex="1" runat="server" BorderStyle="None" BorderColor="Transparent"
																							Width="96px" CssClass="textbox"></asp:textbox>
																					</td>
																				</tr>
																				<tr>
																					<td height="1"></td>
																				</tr>
																				<tr>
																					<td class="yesno" id="tdPwd" nowrap>Password</td>
																					<td>
																						<asp:textbox id="txtPassword" tabIndex="2" runat="server" BorderStyle="None" BorderColor="Transparent"
																							Width="96px" CssClass="textbox" TextMode="Password"></asp:textbox>
																					</td>
																				</tr>
																			</table>
																		</td>
																		<td class="loginBox_bg" width="23" height="92">&nbsp;</td>
																		<td class="loginBox_OnePixel" width="1"></td>
																		<td width="1"></td>
																	</tr>
																	<tr>
																		<td class="loginBox_FooterLeft" vAlign="top" width="47" colSpan="2" height="32"></td>
																		<td class="loginBox_FooterBG " vAlign="top" align="right" height="32">
																			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<tr>
																					<td width="60%"></td>
																					<td id="img__" class="loginBox_Button" onclick="onLoginClick();"
																						width="40%"></td>
																				</tr>
																			</table>
																		</td>
																		<td class="loginBox_FooterRight" colSpan="3" height="35"></td>
																	</tr>
																	<tr>
																		<td  colSpan="6">
																			<div id="divChgPwd" class="loginBox_bg textbox" style="display:none">
																				<table id="tbl" cellpadding="0" cellspacing="0" border="0">
																					<tr>
																						<td colspan="2" align="center" height="25"><a href="#" onclick="">ChangePassword</a></td>
																					</tr>
																					<tr >
																						<td align="right">New Password</td>
																						<td>
																							<asp:textbox CssClass="textbox" id="txtChangePwd" tabIndex="2" runat="server" BorderColor="Transparent"
																								onfocus="showStrengthDiv(true,'divChgPwd');" onblur="showStrengthDiv(false,'divChgPwd');"
																								onkeyup="return passwordChanged(this);" BorderStyle="None" Width="96px" TextMode="Password"></asp:textbox>
																						</td>
																					</tr>
																					<tr>
																						<td align="right">Confirm Password</td>
																						<td>
																							<asp:textbox CssClass="textbox" id="txtConfirmPwd" tabIndex="2" runat="server" BorderColor="Transparent"
																								onkeypress="return handleEnter('btnChgPwd', event);" BorderStyle="None" Width="96px" TextMode="Password"
																								onblur="validatePwdOnLostFocus('txtChangePwd','txtConfirmPwd');"></asp:textbox>
																						</td>
																					</tr>
																					<tr>
																						<td colspan="2">&nbsp;</td>
																					</tr>
																					<tr>
																						<td colspan="2">
																							<input type="button" id="btnChgPwd" value="Confirm &amp; Login" class="yesno" OnClick="validateInput_();">
																							<asp:Button ID="btnChangePwd" Runat="server" OnClick="btnChangePwd_Click" style="DISPLAY:none" ></asp:Button></td>
																				</tr>
																					<tr>
																						<td colspan="2">
																							<span id="strength">Type Password</span>
																						</td>
																					</tr>																			
																				</table>
																			</div>
																		</td>
																	</tr>
																</table>
															</td>
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
			<div id="stengthDiv" class="yesno" style="BACKGROUND-IMAGE:url(images/login_08.gif);MARGIN-LEFT:20px;POSITION:absolute;display:none;">
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
            </TD></TR></TBODY></TABLE>
		</form>
		<script language="javascript">
			//if(<asp:literal id="SecurityScript" runat="server" EnableViewState="True"></asp:literal>)
				//document.myForm.txtUserCode.focus();
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>			
			//showAlert();
			function validateInput_(){
				var isValidated = validateOldPwd('txtPassword','txtChangePwd','txtConfirmPwd');
				if(isValidated){
					validatePassword('txtChangePwd','txtConfirmPwd','btnChangePwd');
				}
			}


            function onLoginClick(){

	            var loginID = document.getElementById('txtUserCode');
	            var loginPWD = document.getElementById('txtPassword');
	            
                    if(loginID.value == '')
	                {
		                alert('Please enter valid User ID.');
	                }
	                else if(loginPWD.value == '')
	                {
		                alert('Please enter valid Password.');
	                }
	                else
	                {
		                document.getElementById('Imagebutton1').click();
	                }
                }
			
		</script>
		
	</body>
</html>
