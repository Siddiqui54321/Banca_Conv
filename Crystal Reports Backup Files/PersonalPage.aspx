<%@ Page language="c#" Codebehind="PersonalPage.aspx.cs" AutoEventWireup="True" Inherits="ACE.Presentation.PersonalPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		
		<title>:: <asp:Literal id="ltrlTitle" runat="server" EnableViewState="True"></asp:Literal> ::</title>
		<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<asp:Literal id="CSSLiteral" runat="server" EnableViewState="True"></asp:Literal>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script src="../shmalib/jscript/Calculator.js" type="text/JavaScript"></script>
		<script src="../shmalib/jscript/Parameters.js" type="text/JavaScript"></script>
		<script src="../shmalib/jscript/Illustration.js" type="text/JavaScript"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
		<!-- <link rel="stylesheet" href="Styles/PPageStyle.css" type="text/css"> -->
		
		<script language="JavaScript">
			var currentScreen = "shgn_gp_gp_ILUS_ET_GP_HOME";
			var currentImage = "imgHome";
			var navigationAllowed = "Y";
			var userType = <asp:Literal id="userType" runat="server" EnableViewState="False"></asp:Literal>;
			var baseProduct = '';
			var allowAVAPScreen = true;
			var ImageSrc = [["shgn_gp_gp_ILUS_ET_GP_PERONAL"	, "Image1", "images/main/btn_proposal.gif"			, "images/main/btn_proposal_o.gif"],
							["shgn_gp_gp_ILUS_ET_GP_PERONAL"	, "Image2", "images/main/btn_medical.gif"			, "images/main/btn_medical_o.gif"],
						   ];		

			function setCurrentImage(objid)
			{
				try{
					obj = document.getElementById(currentImage);
					obj.className="image2";
				}catch(e){}
				
				currentImage = objid;
				obj = document.getElementById(currentImage);
				obj.className="image3";
			}

			
			function setPageByApplyingRules(img, screen)
			{
				/*if (navigationAllowed!="Y")//on planriders only navigate forward in case of Investa N-PR will stop
				{
					if (navigationAllowed=='N-PR' && (screen == 'shgn_gp_gp_ILUS_ET_GP_TARGETVALUES'))
					{
						alert('Navigation not allowed.');
						return false;
					}
				}*/
				if ( (userType == 'N') && (screen == 'shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE'))
				{
						alert('Navigation not allowed.');
						return false;
				}
				
				

				if ( (userType != 'A') && (screen == 'shgn_gp_gp_ILUS_TREE_ADMIN') )
				{
						alert('Navigation not allowed.');
						return false;
				}
				else
				{
					if ( (userType == 'A') && (screen == 'shgn_gp_gp_ILUS_TREE_ADMIN') )
						showTable(1);
					else
						showTable(0);
				}
				
				if(screen == 'shgn_gp_gp_ILUS_ET_GP_WITHDRAWAL')
				{
					if(allowAVAPScreen == false)
					{
						alert("AVAP not applicable");
						return false;
					}
				}
				setCurrentImage(img);
				setPage(screen);
			}
			
			function showTable	(val)
			{
				if (val ==1)
				{
					UserTable = document.getElementById('UserManTable').style;
					UserTable.display='block';
					GenTable = document.getElementById('Table1').style;
					GenTable.display='none';
				}
				else
				{
					UserTable = document.getElementById('UserManTable').style;
					UserTable.display='none';
					GenTable = document.getElementById('Table1').style;
					GenTable.display='block';
				}
				
			}


			function setPage(entityId)
			{
				obj = document.getElementById("mainContentFrame");
				obj.src = "../Presentation/"+entityId+".aspx";
				obj.location = obj.location;
				currentScreen = entityId;
				
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PERONAL')
				{
					document.getElementById('mainContentFrame').style.height = '335.0px';
					setCurrentImage('imgPersonal');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/Plan.jpg";	
				}

				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_WITHDRAWAL')
				{
					document.getElementById('mainContentFrame').style.height = '320.0px';
					setCurrentImage('imgPartialWithdrawals');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/Withdrawal.jpg"			
				}
				
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_BENEFECIARY')
				{
					setCurrentImage('imgBeneficiary');
					document.getElementById('mainContentFrame').style.height = '400.0px';
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/Beneficiary.jpg";			
				}

				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL')
				{
					document.getElementById('mainContentFrame').style.height = '600.0px';
					setCurrentImage('imgMedicalDetail');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/MedicalDetail.jpg";
				}

				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PLANRIDER')
				{
					document.getElementById('mainContentFrame').style.height = '400.0px';
					setCurrentImage('imgPlanRider');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/Personal.jpg";
				}
						
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE')
				{
					document.getElementById('mainContentFrame').style.height = '430.0px';
					setCurrentImage('imgPolicyAcceptance');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/PolicyAcceptance.jpg";
				}
				
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_TARGETVALUES')
				{
					document.getElementById('mainContentFrame').style.height = '645.0px';
					setCurrentImage('imgTargetValues');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/TargetValues.jpg";
				}
				
				if(currentScreen=='shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS')
				{
					document.getElementById('mainContentFrame').style.height = '350.0px';
					setCurrentImage('imgAddress');
					obj1 = document.getElementById("img2");
					obj1.src="images/Navigation/Address.jpg";
				
				}

				if(currentScreen=='shgn_gp_gp_ILUS_TREE_ADMIN' || currentScreen=='shgn_gp_gp_ILUS_TREE_CHANNEL')
				{
					document.getElementById('mainContentFrame').style.height = '450.0px';
					setFixedValuesInSession('USE_USERID=');
					setFixedValuesInSession('CCH_CODE=');
				}
				
				if(currentScreen=='AgentHierarchy')
				{
					document.getElementById('mainContentFrame').style.height = '200.0px';
				}
			}
			
			function setAlertPage(entityId, msg)
			{
				setPage(entityId);
				//alert(msg);
			}
			
			function setPageNavigate()
			{	//here
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PERONAL')
				{
					setPage('shgn_gp_gp_ILUS_ET_GP_PLANRIDER');
					return;
				}
				if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_BENEFECIARY')
				{
					setPage('shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS');
					return;
				}
				//if (currentScreen=='shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS')
				//{
				//	setPage('shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL');
				//	return;
				//}
			}

			<!--
			function MM_findObj(n, d) 
			{ //v4.01
				var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
					d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
				if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
				for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
				if(!x && d.getElementById) x=d.getElementById(n); return x;
			}

			function MM_preloadImages() 
			{ //v3.0
				var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
				var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
				if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
			}

			function MM_swapImgRestore() 
			{ //v3.0
				var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
			}

			function MM_swapImage() 
			{ //v3.0
				var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
				if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
			}
			//-->

		</script>

		<style type="text/css">
	</style>
</HEAD>
	
	
	<body leftmargin="0" topmargin="0" oncontextmenu="return false;">
		
		<!-- Busy Background -->
		<div id="graybackground-div"></div>
        <div id="message-div">
			<div>Processing data, please wait...<br><img src="Images/progressBar/busy10.gif" ></div>
		</div>
	
		
		<form id="Form1" method="post" runat="server">
			<input id="txtNP1_PROPOSAL" name="txtNP1_PROPOSAL" style="VISIBILITY: hidden">
			<input id="txtRptName" name="txtRptName" style="VISIBILITY: hidden">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td class="img__top_bg" height="105">
									<table width="100%" border="0" cellspacing="0" cellpadding="0"> <!--  background="images/top_bg.gif" -->
										<tr>
											<td height="80" valign="middle"><!-- #BeginLibraryItem "/Library/top.lbi" -->
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr height="100">
														<td rowspan="2" width="17%" class=""  ><IMG src="images/CompanyLogo.gif" border="0"></A>
															&nbsp;&nbsp;&nbsp;<a href="Library/index.html"></a><a href="index.html"></a>
														</td>
														<td width="73%" align="left" valign="bottom" class="img__top_bg">
															<table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
																<tr>
																	<td align="center" height="5"></td>
																</tr>
																<tr>
																	<td align="center">
																		<table width="475" border="0" cellspacing="3" cellpadding="3">
																			<tr>
																				<td vAlign="middle" align="center" height=45 class="AppImage"></td>
																			</tr>
																			<tr>
																				<td colspan="3" align="center" height="5">
																					<asp:label id="lblTestVersion" Runat="server" ForeColor="Gray" Font-Names="Verdana"
																						Font-Size="XX-Small" Font-Bold="True"></asp:label>&nbsp;</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr id='trMenubar' style="display:none;">																
																	<td align="center" width="100%"> <!-- <td align="" width="100%"> -->
																		<table border="0" cellpadding="0" cellspacing="0"> <!-- align="center"  width="100%" -->
																			<tr>
																				<!-- 
																				<td align="center"></td>
																				<td align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" background="images/111.gif"><a href="#" class="image2" id="imgHome" onclick="showTable(2);setPage('shgn_gp_gp_ILUS_ET_GP_HOME');">&nbsp; 
																						Home &nbsp;</a></td>
																				-->
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" class="img__777"><a href="#" class="image2" id="imgPersonal" onclick="showTable(2);setPageByApplyingRules('imgPersonal','shgn_gp_gp_ILUS_ET_GP_PERONAL');">&nbsp; 
																						Personal &nbsp;</a></td>
																				<!-- 
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" background="images/111.gif"><a href="#" class="image2" id="imgPurpose" onclick="setPageByApplyingRules('imgPurpose','shgn_gp_gp_ILUS_ET_GP_PURPOSE');">&nbsp; 
																						Purpose &nbsp;</a></td>
																				-->
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" class="img__777"><a href="#" class="image2" id="imgPlanRider" onclick="setPageByApplyingRules('imgPlanRider','shgn_gp_gp_ILUS_ET_GP_PLANRIDER');">&nbsp; 
																						Plan&nbsp;Rider &nbsp;</a></td>
																				
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" background="images/111.gif" id="PartialWithdraw">
																					<a href="#" class="image2" id="imgPartialWithdrawals" onclick="setPageByApplyingRules('imgPartialWithdrawals','shgn_gp_gp_ILUS_ET_GP_WITHDRAWAL');">
																				        &nbsp;AVAP&nbsp;</a></td>
																				        
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" class="img__777" id="Beneficiary"><a href="#" class="image2" id="imgBeneficiary" onclick="setPageByApplyingRules('imgBeneficiary','shgn_gp_gp_ILUS_ET_GP_BENEFECIARY');">&nbsp; 
																						Beneficiary &nbsp;</a></td>

																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" class="img__777" id="Address"><a href="#" class="image2" id="imgAddress" onclick="setPageByApplyingRules('imgAddress','shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS');">&nbsp; 
																						Address &nbsp;</a></td>

																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td id="TargetValues" align="center" background="images/111.gif"><a href="#" class="image2" id="imgTargetValues" onclick="setPageByApplyingRules('imgTargetValues','shgn_gp_gp_ILUS_ET_GP_TARGETVALUES');">&nbsp; 
																						Target Values &nbsp;</a></td>
																																												
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td id="MedicalTab" align="center" class="img__777"><a href="#" class="image2" id="imgMedicalDetail" onclick="setPageByApplyingRules('imgMedicalDetail','shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL');">&nbsp; 
																						Medical Detail &nbsp;</a></td>
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td id="acceptanceTab" align="center" class="img__777" style="VISIBILITY: hidden" ><a href="#" class="image2" id="imgPolicyAcceptance" onclick="setPageByApplyingRules('imgPolicyAcceptance','shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE');">&nbsp; 
																						Acceptance &nbsp;</a></td>
																				<td id="acceptanceTab1" width="1" align="center" style="VISIBILITY: hidden" ><img src="images/divider.gif" width="1" height="25"></td>

																				<td id="adminTab" align="center" class="img__777" style="VISIBILITY: hidden" ><a href="#" class="image2" id="imgUser" onclick="setPageByApplyingRules('imgUser','shgn_gp_gp_ILUS_TREE_ADMIN');">&nbsp; 
																						Admin &nbsp;&nbsp; </a>
																				</td>
																				


																				<!--//////////////////////// 	-->
																				<!--
																				<td width="1" align="center"><img src="images/divider.gif" width="1" height="25"></td>
																				<td align="center" background="images/777.gif"><a href="#" class="image2" id="imgUser" onclick="setPageByApplyingRules('imgUser','shgn_gp_gp_ILUS_ET_UM_USERMANAGMENT');showTable(1)">&nbsp; 
																						Admin &nbsp;&nbsp; </a>
																				</td>
																				-->
																				<!--	
																						<td align="center" ><a href="UserManagmentButtons.ASPX" class="image2" id="imgUser">&nbsp; 
																						Us</a></td>				
																				<!--//////////////////////// -->
																				<td align="center">&nbsp;</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
														<td valign="middle" width="10%" align="left">&nbsp;
														</td>
													</tr>
												</table> <!-- #EndLibraryItem --></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td width="189" valign="top">
												<table width="183" border="0" cellspacing="1" cellpadding="0" id="MenuTable">
													<tr>
														<td width="9" height="11"></td>
														<td width="183" rowspan="5" class="menu_button"><!-- #BeginLibraryItem "/Library/left.lbi" -->
															<table id="TABLE1" width="187" border="0" cellspacing="1" cellpadding="0" style="WIDTH: 187px; HEIGHT: 100%">
																<tr>
																	<!-- <td><img src="images/left_navigation_top_design.gif" width="184" height="55"></td>-->
																	<td class="img__left_navigation_top_design" height="55">
																		<p class="loginuser">
																		User : <asp:Literal id="_loggedUser" runat="server" EnableViewState="False"></asp:Literal> 
																		<br>
																		<asp:Literal id="_loggedBranchLabel" runat="server" EnableViewState="False"></asp:Literal> : <asp:Literal id="_loggedBranch" runat="server" EnableViewState="False"></asp:Literal>
																		</p>
																		
																	</td>
																</tr>
																<!-- 
																<tr>
																	<td><a href="#" onClick="ShowCalculator();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Calculator</a></td>
																</tr>
																<tr>
																	<td><a href="#" onClick="ShowParamWind();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parameters</a></td>
																</tr>
																
																<tr>
																	<td><a href="#" onClick="IllustrationWind();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Illustration</a></td>
																</tr> 
																-->
																<!--
																<tr>
																	<td><a href="#" onClick="setPage('MedicalDetail');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Medical Detail</a></td>
																</tr>
																-->
																<tr id="rptIllustration"><td><a onClick="executeReport('ILLUSTRATION');" class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Illustration Printing</a></td></tr>
																<tr id="rptAdvice">      <td><a onClick="executeReport('ADVICE');"       class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Advice Printing</a>      </td></tr>
																<tr id="rptProfile">     <td><a onClick="executeReport('PROFILE');"      class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal Profile</a>     </td></tr>
																<tr id="rptPolicy">      <td><A onClick="executeReport('POLICY');"       class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Policy Printing</A>      </td></tr>
																<tr id="rptPersonalInfo"><td><A onClick="executeReport('PROPOSALINQ');"  class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Proposal Inquiry</A>     </td></tr>
																<tr>
																	<td align="center" valign="bottom" height="200">
																		<IMG id="img2" name="img2" width="182"  height="100%" src="images/left_navigation_bot_design.gif" >
																	</td>
																</tr>
																<!--
																<tr><td><a href="#" onClick="window.open('HelpFiles/Banc_Assurance_Demo.swf');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;How to Use</a></td></tr>
																-->
																<tr><td><a href="#" onClick="showUnApprovedProposal();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unapproved Proposals</a></td></tr>
																<tr><td><a href="#" onClick="window.open('HelpFiles/BancassurancUsersGuide.pdf');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User Guide</a></td></tr>
																<!--
																<tr><td><a href="#" onClick="window.open('HelpFiles/FAQ.MHT', null,'location:no;menubar:no;titlebar:no');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FAQ</a></td></tr>
																-->
																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD')"
																			class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Change Password</a></td>
																</tr>
																<!--<tr>
																	<td>
																		<a href="#" onclick="downloadFile();" class="image" style="color:yellow" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Adobe Acrobat 9</a>
																	</td>
																</tr>-->
																<tr>
																	<td>
																		<a href="#" onClick="document.getElementById('imgLogoutButton').click();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Logout
																			<asp:ImageButton runat="server" ID="imgLogoutButton" width="0" border="0" name="Image42" OnClick="imgLogout_Click" />
																		</a>
																	</td>
																</tr>
															</table>
															
															<!-- User Managment Table-->
															
															<table width="187" border="0" cellspacing="1" cellpadding="0" style="DISPLAY: none; WIDTH: 187px; HEIGHT: 270px"
																id="UserManTable">
																
																<tr>
																	<td class="img__left_navigation_top_design" height="55">
																		<p class="loginuser">
																			User : <asp:Literal id="_loggedUser2" runat="server" EnableViewState="False"></asp:Literal> 
																			<br>
																			<asp:Literal id="_loggedBranchLabel2" runat="server" EnableViewState="False"></asp:Literal> : <asp:Literal id="_loggedBranch2" runat="server" EnableViewState="False"></asp:Literal>
																		</p>
																	</td>
																</tr>															
																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_TREE_ADMIN')" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User Management
																		</a>
																	</td>
																</tr>
																<!-- These screens are moved into Admin Tree (i.e. shgn_gp_gp_ILUS_TREE_ADMIN)
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_ET_GE_UC_USERCOUNTRY2')" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User 
																			Country</a></td>
																</tr>
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_GE_UC_USERCHANNEL');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User 
																			Channel</a></td>
																</tr>

																<tr>
																	<td><a href="#" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exchange Rate</a></td>
																</tr>
																<tr>
																
																-->

																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_TAB_COUNTRYSETUP');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Country Setup</a></td>
																</tr>

																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_TREE_CHANNEL');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Channel Setup</a></td>
																</tr>
																
																
																<!--
																<tr>
																	<td><a href="#" onClick="setPage('AgentHierarchy');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Agent Hierarchy</a></td>
																</tr>
																-->
																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_BANCAPARAMS');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Technical Parameter</a></td>
																</tr>

																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_TREE_QUESTION');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Question Setup</a></td>
																	
																</tr>
																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILAS_TREE_VALIDATION');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Validation Setup</a></td>
																</tr>


																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILAS_TREE_DECISION');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Decision Setup</a></td>
																	
																</tr>
																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILAS_TREE_MANDRIDER');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rider Mand./Forb.</a></td>
																</tr>

																
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gs_se_stdgridscreen_GENERATEFILE');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generate File</a></td>
																</tr>
																
																<tr>
																	<td><a href="#" onClick="if(ManualAdjustment=='Y') setPage('shgn_ss_se_stdscreen_MANUALADJUSTMENT');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Manual Adjustment</a>
																	</td>
																</tr>																
																<tr>
																		<td>
																			<a href="#" onClick="setPage('ManualPolicyIssuanceOfPendingProposals_Group');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Compliance Check</a>
																		</td>
																</tr>			
																<tr>
																		<td>
																			<a href="#" onClick="setPage('ManualPolicyIssuanceSearch_Group');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Proposals Status</a>
																		</td>
																</tr>																
																<tr>
																	<td>
																		<a href="#" onClick="setPage('PendingListForApproval_Group');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Approved Proposals</a>
																	</td>
																</tr>																
																<tr>
																	<td>
																		<a href="#" onClick="setPage('ListOfApprovedProposal_Group');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Upload Reciepts</a>
																	</td>
																</tr>
																<tr>
																	<td>
																		<a href="#" onClick="setPage('shgn_gp_gp_SECURITY_PARA');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Security Parameters</a>
																	</td>
																</tr>
																																
																<tr id="rptSecurityLog"> <td><A onClick="executeReport('SECURITYLOG');"  class="image" href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Security Log (Report)</A>         </td></tr>
																<!--
																<tr>
																	<td><a href="#" onClick="setPage('shgn_gp_gp_ILUS_GE_UL_LIMITATION');" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Validation 
																			Limit</a></td>
																</tr>
																
																change pasword
																-->
															
																<tr>
																	<td><a href="#" onClick="document.getElementById('imgLogoutButton').click();" class="image">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Logout
																			<asp:ImageButton runat="server" ID="Imagebutton1" width="0" border="0" name="Image42" OnClick="imgLogout_Click" />
																		</a>
																	</td>
																</tr>
																<tr>
																	<td class="img__left_navigation_bot_design" height="225"></td>	
																</tr>
															</table>
															<!-- User Managment Table -->
															<!-- #EndLibraryItem --></td>
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
											<td width="790" valign="top">
												<table style="WIDTH: 613px; HEIGHT: 332px" cellSpacing="5" cellPadding="10" width="613"
													border="0" id="iframetab">
													<tr>
														<td vAlign="top" id="ifrTD">
															<!--file here...-->
															<div class="box1" id="layer1" style="Z-INDEX: 1; OVERFLOW: auto; WIDTH: 771px; POSITION: static">
																<!--Your Working Area -->
																<!--<fieldset id="main" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; BORDER-BOTTOM: #e0f3be 1px solid; height:700px; overflow: none;">-->
																<iframe id="mainContentFrame" style="WIDTH: 770px; HEIGHT: 620px" frameborder="1" name="mainContentFrame"
																	marginWidth="0" marginHeight="0" src="../Presentation/shgn_ss_se_stdscreen_ILUS_ET_NM_WELCOME.aspx"
																	width="655" scrolling="no" height="330"></iframe>
																<!--</fieldset>-->
															</div>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!--
							<tr>
								<td><table width="100%" border="0" cellspacing="2" cellpadding="0">
										<tr>
											<td background="images/bottom_thick_line.gif" height="21"></td>
										</tr>
									</table></td>
							</tr>
							-->
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		document.getElementById('txtNP1_PROPOSAL').value= "";

	//alert(11);
	if(userType=="R")
	setPage('shgn_gp_gp_ILUS_ET_GP_PERONAL2');
	else
	setPage('shgn_gp_gp_ILUS_ET_GP_PERONAL');
	//alert(22);
	setCurrentImage('imgPersonal');//('imgHome');
	//alert(33);

	<asp:Literal id="FooterScript" runat="server" EnableViewState="True"></asp:Literal>
	
	/********* Hide tab(s) based on setup - start ********/
	function hideTabs() {
		if (hiddenTabs != '') {
			var TabsArray = hiddenTabs.split('~');
			for (i=0; i< TabsArray.length; i++)	{
				if(TabsArray[i] != '') {
					var objTab = document.getElementById(TabsArray[i]);//getElementsByName
					if(objTab != null) objTab.style.display='none';
				}
			}
		}
	}
	hideTabs();
	
    function isAdminType(userType)
			{
				var valid = false;
				switch(userType)
				{
					case "A":
						valid = true;
						break;
					case "I":
						valid = true;
						break;
					case "C":
						valid = true;
						break;
					case "M":
						valid = true;
						break;
				}
				return valid;
			}
	
    function isOnlyAdminType(userType)
			{
				var valid = false;
				switch(userType)
				{
					case "A":
						valid = true;
						break;
					case "S":
						valid = true;
						break;
					case "N":
						valid = true;
						break;
				}
				return valid;
			}

	
	function showUnApprovedProposal(){
			setPage('ManualPolicyIssuanceOfPendingProposalsView_Group');
	}
	function userOptions()
	{
        if (isOnlyAdminType(userType)) {
            document.getElementById('trMenubar').style.display='';    
        }
		else{
			
			setPage(firstMenu);
			showTable(1);
		}

		if(userType=='A'){
			document.getElementById('adminTab').style.visibility='visible';
		} else{
			document.getElementById('adminTab').style.visibility='hidden';
		}
		if(userType =='A' || userType =='S'){
			document.getElementById('acceptanceTab').style.visibility='visible';
			document.getElementById('acceptanceTab1').style.visibility='visible';
		} else{
			document.getElementById('acceptanceTab').style.visibility='hidden';
		}
		if(userType == 'M')
		{
			document.getElementById('adminTab').style.visibility='visible';
			document.getElementById('step1').style.display = 'block';			
		}
		else if(userType == 'C')
		{
			document.getElementById('adminTab').style.visibility='visible';
			document.getElementById('step2').style.display = 'block';
			document.getElementById('step3').style.display = 'block';
			
		}
	}
	userOptions();
	
	/********* Hide tab(s) based on setup - end ********/
	function showTip()
	{
		window.showModalDialog('TipOfTheDay.aspx', null,'status:no;dialogWidth:380px;dialogHeight:215px;dialogHide:true;help:no;scroll:no');
	}
	
	<asp:Literal id="ShowTip" runat="server" EnableViewState="False"></asp:Literal>
	
	
	var titleArray = fetchDataArray("Select * from title");
	function showActiveHelp(){
		/*
		if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_HOME')
		{
			window.open("HelpFiles/home.mht", null,"location:no;menubar:no;titlebar:no");
		}
		*/
		if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PERONAL')
		{
			window.open("HelpFiles/personal.mht", null,"location:no;menubar:no;titlebar:no");
		}
		/*if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_BENEFECIARY')
		{
			window.open("HelpFiles/beneficiary.mht", null,"location:no;menubar:no;titlebar:no");
		}*/
		if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PURPOSE')
		{
			window.open("HelpFiles/purpose.mht", null,"location:no;menubar:no;titlebar:no");
		}
		if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_PLANRIDER')
		{
			window.open("HelpFiles/rider.mht", null,"location:no;menubar:no;titlebar:no");
		}
		if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_WITHDRAWAL')
		{
			window.open("HelpFiles/withdrawl.mht", null,"location:no;menubar:no;titlebar:no");
		}
		/*if (currentScreen=='shgn_gp_gp_ILUS_ET_GP_TARGETVALUES')
		{
			window.open("HelpFiles/target.mht", null,"location:no;menubar:no;titlebar:no");
		}*/
	}

		/*********************** Use to Secure the Page - Start **************************/		
		/*** Stop F5 ***/
		function document.onkeydown()
		{
			//if (event.keyCode==116 || event.keyCode==8)
			if(validateKeys(event)==false)
			{
				event.keyCode = 0;
				event.cancelBubble = true;
				return false;
			}
		}
		
		//Common method used
		var controlFocus = false;
		function validateKeys(objEvent) 
		{ 
			if(controlFocus == true)
			{
				return true;
			}
			else
			{
				//if(event.keyCode==116 || event.keyCode==8 || event.keyCode==13) 
				if(objEvent.keyCode==116 || objEvent.keyCode==8 || objEvent.keyCode==13) 
				{ 
					return false; 
				}
			} 
		}
		function downloadFile()
		{	
			window.location.href="../downloads/"+fileName;			
		}
		/*********************** Use to Secure the Page - End **************************/		
		
        </script>
	</body>
</HTML>
