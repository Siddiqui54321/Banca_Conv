	<%@ Page language="c#" Codebehind="GenerateIllustration.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.GenerateIllustration" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>:: Bancassurance ::</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="CSSLiteral" EnableViewState="True" runat="server"></asp:literal>
		<script src="JSFiles/jquery-1.7.min.js" type="text/javascript"></script>
		<script src="JSFiles/popupWindow.js" type="text/javascript"></script>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/PersonalDetail.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/Validation/CallValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
		<link href="JSFiles/popupWindow.css" rel="stylesheet" type="text/css">
		<LINK href="Styles/mytylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<style type="text/css">.style7 { FONT-FAMILY: Verdana, Tahoma, Arial, Helvetica, sans-serif; FONT-WEIGHT: bold }
		</style>
		<%Response.Write(ace.Ace_General.LoadGlobalStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/GeneralView.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/PersonalDetail.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/Validation/CallValidation.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/Date.js'></SCRIPT>
	
		<script language="javascript">
		
		<asp:Literal id="ltrlApp" runat="server" EnableViewState="True"></asp:Literal>
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';


	
		
		function CalculateEntryAge(objDate)
		{			
			var years = dateDiffYears(objDate.value, sysDate, ageRoundCriteria)
			
			if(isNaN(years))
			{
				getField("NP2_AGEPREM").value="";
				return false;
			}
			else
			{
				if(years < 18)
				{
					alert("Age must be 18 years or above");
					getField("NPH_BIRTHDATE").focus();
					return false;
				}
				else
				{
					getField("NP2_AGEPREM").value=years;
					return true;
				}
		    }
		}
		
		</script>
	</HEAD>
	<body>
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
							<td vAlign="middle" align="left" width="30%">&nbsp;&nbsp;&nbsp;&nbsp;<A href="index.html">
									<IMG src="images/CompanyLogo.gif" border="0"></A></td>
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
											<!-- 	<table cellSpacing="1" cellPadding="0" width="183" border="0">
												<tr>
													<td class="LeftTopHeader" height="52" width="183"></td>
												</tr>
												<tr>
													<td class="LeftTopDown" height="227" width="183"></td>
												</tr>
											</table>#EndLibraryItem -->
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div id="NormalEntryTableDiv" class="" runat="server" style="Z-INDEX:-111">
				<TABLE id="entryTable" border="0" cellSpacing="0" cellPadding="2">
					<tr class="form_heading">
						<td height="20" colSpan="4">&nbsp; Policy Owner Personal Details
						</td>
					</tr>
					<tr>
						<td height="10" colSpan="4"></td>
					</tr>
					<TR id="rowNPH_TITLE" class="TRow_Normal">
						<TD id="lblddlNPH_TITLE" style="HEIGHT: 23px" width="110" align="right">Title</TD>
						<TD id="ctlddlNPH_TITLE" style="HEIGHT: 23px" width="186">
						<SHMA:DROPDOWNLIST id="ddlNPH_TITLE" tabIndex="1" runat="server" CssClass="RequiredField" Width="184px"
								Onchange="Title_ChangeEvent(this);" DataValueField="CSD_TYPE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST>
								</TD>
						<TD id="lbltxtCNIC_VALUE" width="110" align="right">
							<SHMA:DROPDOWNLIST id="ddlNPH_IDTYPE" tabIndex="3" 
								runat="server" CssClass="RequiredField" Width="100px" DataValueField="NPH_IDTYPE" DataTextField="desc_f"
								BlankValue="false"></SHMA:DROPDOWNLIST>
						</TD>
						<TD id="ctltxtCNIC_VALUE" width="186" style="HEIGHT: 23px">
							<shma:textbox onblur="NIC_Blur2(this)" id="txtCNIC_VALUE" runat="server" Width="184px" DESIGNTIMEDRAGDROP="79"
								MaxLength="15"></shma:textbox>
						</TD>
					</TR>
					<TR id="rowNPH_FULLNAME" class="TRow_Alt">
						<TD id="TDtxtNPH_FULLNAME" width="110" align="right">Name in English</TD>
						<TD width="186">
							<div id="personNameDiv" class="form_heading" style="Z-INDEX: 1000; DISPLAY: none">
								<table>
									<tr>
										<td>
											<table border="0">
												<tr style="COLOR: #e1e1e1">
													<td>First Name</td>
													<td>Middle Name</td>
													<td>Last Name</td>
													<td></td>
												</tr>
												<tr>
													<td><shma:textbox id="txtNPH_FIRSTNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50" BaseType="Character"></shma:textbox></td>
													<td><shma:textbox id="txtNPH_SECONDNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50"
															BaseType="Character"></shma:textbox></td>
													<td><shma:textbox id="txtNPH_LASTNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50" BaseType="Character"></shma:textbox></td>
													<td><a href="#" class="button2" tabIndex="4" onclick="btnOK_Click()">OK</a></td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</div>
							<shma:textbox id="txtNPH_FULLNAME" tabIndex="4" runat="server" CssClass="RequiredField" Width="160px"
								onchange="generateID(IDFormat);" ReadOnly="True" MaxLength="50" BaseType="Character"></shma:textbox>
							&nbsp; <INPUT class="BUTTON" title="Open Proposal List Of Values" tabIndex="0" onclick="openPersonsLOV();"
								value=".." type="button" name="ProposalLov"> 
								<label id="nameFormat" style="DISPLAY:none">
								<font color="#009900">First&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Middle&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Last</font>
							</label>
							<asp:requiredfieldvalidator id="rfvNPH_FULLNAME" runat="server" Display="Dynamic"  ControlToValidate="txtNPH_FULLNAME"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="reNPH_FULLNAME" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect"
								ControlToValidate="txtNPH_FULLNAME" ValidationExpression="[A-Za-z\s]+"></asp:regularexpressionvalidator>
						</TD>
					</TR>
					<TR id="rowNPH_BIRTHDATE" class="TRow_Normal">
						<TD id="TDtxtNPH_BIRTHDATE" width="110" align="right">Date of Birth</TD>
						<TD width="186">
							<SHMA:DATEPOPUP id="txtNPH_BIRTHDATE" tabIndex="6" runat="server" CssClass="RequiredField" Width="90px"
								onchange="formatDate(this,'DD/MM/YYYY');CalculateEntryAge(this);generateID(IDFormat);" ImageUrl="Images/image1.jpg"
								ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
								<WeekdayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="WeekdayStyle" BackColor="White"></WeekdayStyle>
								<MonthHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="MonthHeaderStyle" BackColor="Yellow"></MonthHeaderStyle>
								<OffMonthStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"
									BackColor="AntiqueWhite"></OffMonthStyle>
								<GoToTodayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									BackColor="White"></GoToTodayStyle>
								<TodayDayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="TodayDayStyle" BackColor="LightGoldenrodYellow"></TodayDayStyle>
								<DayHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="DayHeaderStyle" BackColor="Orange"></DayHeaderStyle>
								<WeekendStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="WeekendStyle" BackColor="LightGray"></WeekendStyle>
								<SelectedDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="SelectedDateStyle" BackColor="Yellow"></SelectedDateStyle>
								<ClearDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									BackColor="White"></ClearDateStyle>
								<HolidayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
									CssClass="HolidayStyle" BackColor="White"></HolidayStyle>
							</SHMA:DATEPOPUP>
							&nbsp;<font id="lbltxtNP2_AGEPREM">Age</font>&nbsp;
							<shma:textbox id="txtNP2_AGEPREM" tabIndex="6" CssClass="DisplayOnly" runat="server" Width="40px"
								MaxLength="2" BaseType="Number" ReadOnly="True"></shma:textbox>
							<asp:comparevalidator id="msgNPH_BIRTHDATE" runat="server" CssClass="CalendarFormat" Display="Dynamic"
								ErrorMessage="{dd/mm/yyyy} " ControlToValidate="txtNPH_BIRTHDATE" Type="Date" Operator="DataTypeCheck"
								Enabled="true"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="rfvNPH_BIRTHDATE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPH_BIRTHDATE"></asp:requiredfieldvalidator>
						</TD>
						<TD id="TDddlNPH_SEX" width="110" align="right">Gender</TD>
						<TD width="186">
							<SHMA:DROPDOWNLIST id="ddlNPH_SEX" tabIndex="7" runat="server" CssClass="RequiredField" Width="82px"
								Onchange="Gender_ChangeEvent(this);generateID(IDFormat);">
								<asp:ListItem Selected></asp:ListItem>
								<asp:ListItem Value="M">Male</asp:ListItem>
								<asp:ListItem Value="F">Female</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<asp:comparevalidator id="cfvNPH_SEX" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlNPH_SEX" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="rfvNPH_SEX" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlNPH_SEX"></asp:requiredfieldvalidator>
							<SHMA:DROPDOWNLIST id="ddlNPH_MARITALSTATUS" tabIndex="7" runat="server" CssClass="RequiredField" Width="90px"
								style="DISPLAY:none">
								<asp:ListItem Selected></asp:ListItem>
								<asp:ListItem Value="S">Single</asp:ListItem>
								<asp:ListItem Value="M">Married</asp:ListItem>
							</SHMA:DROPDOWNLIST>
						</TD>
					</TR>
					<TR id="rowddlCOP_OCCUPATICD" class="TRow_Alt">
						<TD id="lblddlCOP_OCCUPATICD" width="110" align="right">Occupation</TD>
						<TD id="ctlddlCOP_OCCUPATICD" width="186">
							<SHMA:DROPDOWNLIST CssClass="RequiredField" id="ddlCOP_OCCUPATICD" tabIndex="8" BlankValue="True" runat="server"
								Width="160px" DataValueField="COP_OCCUPATICD" DataTextField="desc_f"></SHMA:DROPDOWNLIST>
							&nbsp; <INPUT class="BUTTON" title="Open Occupation List Of Values" tabIndex="8" onclick="openOccupationDialog();"
								value=".." type="button" name="ProposalLov">
								<!--<asp:comparevalidator id="cfvCOP_OCCUPATICD" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlCOP_OCCUPATICD" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="rfvCOP_OCCUPATICD" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlCOP_OCCUPATICD"></asp:requiredfieldvalidator>-->
								</TD>
					</TR>
					<TR class="TRow_Alt">
					</TR>
					</TR>
				</TABLE>
			</div>
			<div id="boxes">
				<div id="dialog" class="window">
					<div width="100%">
						<input type="button" value="Close" onclick="closeOccupationDialog();" style="POSITION: relative; RIGHT: 1px">
						<label class="fieldHeading">Search :</label>
						<asp:TextBox Runat="server" ID="txtSearchOccupation"></asp:TextBox>
						<input type="button" id="btnSearch" value="Search" onclick="filterResult();">
						<div id='imgLoading'>
							<img id='img' style="width:15px;" src="Images/loading.gif" /> Please Wait...
						</div>
						<div id='noResult' style="display:none;">
							No Result Found.
						</div>
					</div>
					<div style="WIDTH: 550px; margin-top:5px" class="form_heading">Occupation</div>
					<div id='divOccupation' style="WIDTH: 550px; HEIGHT: 150px; OVERFLOW: auto">
						<asp:Repeater Runat="server" ID="dgOccupation">
							<HeaderTemplate>
								<ul id='ulOccupation' class="ulSearch">
							</HeaderTemplate>
							<ItemTemplate>
								<li class='ItemStyle ListRowItem' onclick="SearchItemClicked('<%# DataBinder.Eval(Container, "DataItem.COP_OCCUPATICD") %>');">
									<a href="#" onclick="">
										<%# DataBinder.Eval(Container, "DataItem.DESC_F") %>
									</a>
								</li>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<li class='ItemStyle ListRowAlt' onclick="SearchItemClicked('<%# DataBinder.Eval(Container, "DataItem.COP_OCCUPATICD") %>');">
									<a href="#" onclick="">
										<%# DataBinder.Eval(Container, "DataItem.DESC_F") %>
									</a>
								</li>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</ul>
							</FooterTemplate>
						</asp:Repeater>
						<SCRIPT language="javascript">
							function SearchItemClicked(li,id){
								if($("#ddlCOP_OCCUPATICD").length==1){
								$("#ddlCOP_OCCUPATICD").empty();
								
								$('#ddlCOP_OCCUPATICD')
										.append($("<option></option>")
										.attr("value",id)
										.text($($(li).html()).html())); 
								}
								$("#ddlCOP_OCCUPATICD").val(id);
								closeOccupationDialog();
		
								filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
								
							}
							function setOccupationByID(id){
								if($("#ddlCOP_OCCUPATICD").length==1){
								var str_Query = "SELECT COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD='"+id+"'";
								var result = RSExecute("RemoteService.aspx", "GetComboDescription", str_Query  );			
								
								$("#ddlCOP_OCCUPATICD").empty();
								
								$('#ddlCOP_OCCUPATICD')
										.append($("<option></option>")
										.attr("value",id)
										.text(result.return_value)); 
								}
								$("#ddlCOP_OCCUPATICD").val(id);
								closeOccupationDialog();
		
								filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
								
							}
							function openOccupationDialog(){
								showDialog('#dialog',
								function(){
									//alert($('#ulOccupation li').length );
									if($('#ulOccupation li').length==0)
										$('#imgLoading').show('fast',
										function(){
											$('#imgLoading').delay(500).queue(
											function(){
												jasonCallToGetOccupations(
												function(){
													$('#imgLoading').hide();
												}
												);
											});
										});
									else{
										$('#imgLoading').hide();
									}
								});
								
							}
							function closeOccupationDialog(){
								hideDialog('#dialog');
								//document.getElementById("ddlCCL_CATEGORYCD").focus();
							}
							/*jQuery.extend(
								jQuery.expr[':'], { 
									Contains : "jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase())>=0" 
							});*/
							jQuery.expr[':'].Contains = function(a, i, m) { 
								return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0; 
								};

							//TODO: to clear Cache programtically
							///localStorage.removeItem("ulOccupation")
							function jasonCallToGetOccupations(callback){
								try{
								if(localStorage){
									if(localStorage["ulOccupation"]==null){
										var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
										$('#divOccupation').html(tbl);
										localStorage["ulOccupation"]=tbl;
									}
									else{
										$('#divOccupation').html(localStorage["ulOccupation"]);							
									}
								}
								else
									var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
									$('#divOccupation').html(tbl);					
								}
								catch(err){
									var tbl = executeClass('ace.Ace_General,Get_OCCUPATICD_Grid');
									$('#divOccupation').html(tbl);
								}
								
								if(callback)
									callback();
							}

							function filterResult(){
								$('#noResult').hide();
								$('#imgLoading').show();
								$('#ulOccupation li').hide();
								if($('#txtSearchOccupation').val()==''){
									$('#ulOccupation li').show();
									$('#imgLoading').hide();
								}
								else{
									$('#ulOccupation li:Contains("'+$('#txtSearchOccupation').val()+'")').show(function(){$('#imgLoading').hide();});
								}
								if($('#ulOccupation li:visible').length==0){
										$('#imgLoading').hide();
										$('#noResult').show();
								}
							}
						</SCRIPT>
					</div>
				</div>
			</div>
			<div class="NormalEntryTableDivPlan" id="Div1" runat="server">
				<TABLE id="entryTablePlan" cellSpacing="0" cellPadding="2" border="0">
					<tr class="form_heading">
						<td colSpan="6" height="20">&nbsp; Plan Details
						</td>
					</tr>
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<TR class="TRow_Normal" id="rowPPR_PRODCD">
						<TD id="lblddlPPR_PRODCD" align="right" width="130">Plan</TD>
						<TD id="ctlddlPPR_PRODCD" align="right" width="205">
							<SHMA:DROPDOWNLIST id="ddlPPR_PRODCD" onblur="disableButtons();" tabIndex="1" runat="server" AutoPostBack="False"
								CssClass="RequiredField" Width="184px" DataValueField="PPR_PRODCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST>
								<!--<asp:comparevalidator id="cfvPPR_PRODCD" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPPR_PRODCD"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvPPR_PRODCD" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlPPR_PRODCD"></asp:requiredfieldvalidator>-->
								</TD>
						<TD id="lbltxtNPR_BENEFITTERM" align="right" width="130">Policy Term</TD>
						<TD id="ctltxtNPR_BENEFITTERM" align="right" width="205"><shma:textbox id="txtNPR_BENEFITTERM" tabIndex="2" runat="server" CssClass="RequiredField" Width="184px"
								BaseType="Number" Precision="0" subtype="Currency" MaxLength="2"></shma:textbox>
								<!--<asp:comparevalidator id="cfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_BENEFITTERM"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvNPR_BENEFITTERM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_BENEFITTERM" Precision="0"></asp:requiredfieldvalidator>-->
								</TD>
					</TR>
					<TR class="TRow_Alt" id="rowPCU_CURRCODE">
						<TD id="lblddlCCB_CODE" align="right" width="130">Calculation Basis</TD>
						<TD id="ctlddlCCB_CODE" align="right" width="205">
						<SHMA:DROPDOWNLIST id="ddlCCB_CODE" onblur="disableButtons();" tabIndex="3" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="CCB_CODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST>
								<asp:comparevalidator id="cfvCCB_CODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCCB_CODE"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvCCB_CODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCCB_CODE"></asp:requiredfieldvalidator>
								</TD>
						<TD id="lbltxtNPR_SUMASSURED" align="right" width="130">
						<asp:label id="lblFaceValue" EnableViewState="false" Runat="server">Sum Assured/Premium</asp:label>
						</TD>
						<TD id="ctltxtNPR_SUMASSURED" align="right" width="205">
							<shma:textbox id="txtNPR_SUMASSURED" tabIndex="4" runat="server" CssClass="RequiredField" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" subType="Currency" >
								</shma:textbox><asp:comparevalidator id="cfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED">
								</asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNPR_SUMASSURED" Precision="0"></asp:requiredfieldvalidator>
								<asp:requiredfieldvalidator id="msgNPR_SUMASSURED" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtNPR_SUMASSURED"
								Precision="0" Enabled="False"></asp:requiredfieldvalidator>
							<shma:textbox id="txtNPR_TOTPREM" tabIndex="4" runat="server" CssClass="RequiredField" Width="0px"
								BaseType="Number" Precision="2" MaxLength="15" subType="Currency"></shma:textbox>
						<!--		<asp:comparevalidator id="cfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPR_TOTPREM"
								Precision="0"></asp:requiredfieldvalidator>
								<asp:requiredfieldvalidator id="msgNPR_TOTPREM" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtNPR_TOTPREM"
								Precision="0" Enabled="False"></asp:requiredfieldvalidator> -->
								</TD>
						<TD></TD>
					</TR>
					<TR class="TRow_Normal" id="rowPCU_CURRCODE_PRM">
						<TD id="lblddlCMO_MODE" align="right" width="130">Premium Mode</TD>
						<TD id="ctlddlCMO_MODE" align="right" width="205">
						<SHMA:DROPDOWNLIST id="ddlCMO_MODE" onblur="disableButtons();" tabIndex="5" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="CMO_MODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST>
							<!--	<asp:comparevalidator id="cfvCMO_MODE" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
								Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCMO_MODE"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvCMO_MODE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCMO_MODE"></asp:requiredfieldvalidator> -->
								</TD>
						<TD id="lblddlPCU_CURRCODE_PRM" align="right" width="130">Currency</TD>
						<TD id="ctlddlPCU_CURRCODE_PRM" align="right" width="205">
						<SHMA:DROPDOWNLIST id="ddlPCU_CURRCODE_PRM" tabIndex="6" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="PCU_CURRCODE" DataTextField="desc_f" BlankValue="false"></SHMA:DROPDOWNLIST>
								<!-- <asp:comparevalidator id="cfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvPCU_CURRCODE_PRM" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlPCU_CURRCODE_PRM"></asp:requiredfieldvalidator> -->
								</TD>
					</TR>
					<TR class="TRow_Normal" id="rowNPR_INDEXATION" style="VISIBILITY: hidden">
						<td id="lblNPR_INDEXATION" align="right" width="130">Escalation / Indexation</td>
						<TD id="ctlNPR_INDEXATION" align="right" width="205"><SHMA:DROPDOWNLIST id="ddlNPR_INDEXATION" tabIndex="8" runat="server" CssClass="RequiredField" width="50px">
								<asp:ListItem Value="N">No</asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<asp:requiredfieldvalidator id="rfvNPR_INDEXATION" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPR_INDEXATION"></asp:requiredfieldvalidator><shma:textbox id="txtNPR_INDEXRATE" tabIndex="9" runat="server" CssClass="RequiredField" Width="125px"
								BaseType="Number" Precision="2" MaxLength="5" subType="Currency"></shma:textbox>
							<!--	<asp:comparevalidator id="cfvNPR_INDEXRATE" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_INDEXRATE"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvNPR_INDEXRATE" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNPR_INDEXRATE"
								Precision="0"></asp:requiredfieldvalidator> -->
								</TD>
						<TD id="lbltxtNPR_PREMIUMTER" align="right" width="130">Premium Term</TD>
						<TD id="ctltxtNPR_PREMIUMTER" align="right" width="205">
						<shma:textbox id="txtNPR_PREMIUMTER" tabIndex="7" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="0" MaxLength="3" SubType="Currency"></shma:textbox>
							<!--	<asp:comparevalidator id="cfvNPR_PREMIUMTER" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Double" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMTER"></asp:comparevalidator> -->
								</TD>
					</TR>
					<!---------------------------->
					<TR class="TRow_Normal" id="rowCFR_FORFEITUCD" style="VISIBILITY: hidden">
						<TD id="lblddlCFR_FORFEITUCD" align="right" width="130">ANF</TD>
						<TD id="ctlddlCFR_FORFEITUCD" align="right" width="205">
							<SHMA:DROPDOWNLIST id="ddlCFR_FORFEITUCD" onblur="disableButtons();" tabIndex="7" runat="server" CssClass="RequiredField"
								Width="184px" DataValueField="csd_type" DataTextField="desc_f" BlankValue="true"></SHMA:DROPDOWNLIST>
						<!--	<asp:comparevalidator id="cfvCFR_FORFEITUCD" runat="server" Display="Dynamic" EnableClientScript="False"
								ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCFR_FORFEITUCD"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvCFR_FORFEITUCD" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlCFR_FORFEITUCD"></asp:requiredfieldvalidator> -->
						</TD>
					</TR>
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<!-- <tr class="form_heading" id="IllustraionDetailHeading" style="VISIBILITY: hidden">
						<td colSpan="6" height="20">&nbsp; Premium Detail 
						<IMG id="imgExtend" style="HEIGHT: 0px; VISIBILITY: hidden" onclick="showIllustrationDetail(this);"
								alt="" src="../shmalib/images/Extend.jpg" border="0" name="btnsave">
						</td>
					</tr> -->
					<tr>
						<td colSpan="4" height="10"></td>
					</tr>
					<TR class="TRow_Normal" id="rowNPR_PREMIUMTER_2" style="VISIBILITY: hidden">
						<TD id="lbltxtNP1_RETIREMENTAGE" align="right" width="130">Maturity Age</TD>
						<TD id="ctltxtNP1_RETIREMENTAGE" align="right" width="205">
						<shma:textbox id="txtNP1_RETIREMENTAGE" tabIndex="11" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="0" MaxLength="3" readonly="true"></shma:textbox>
								<!--<asp:comparevalidator id="cfvNP1_RETIREMENTAGE" runat="server" ControlToValidate="txtNP1_RETIREMENTAGE"
								Operator="DataTypeCheck" Type="Double" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>-->
								</TD>
						<TD id="lbltxtNPR_SUMASSURED_2" align="right" width="130">Sum Assured</TD>
						<td id="ctltxtNPR_SUMASSURED_2" align="right" width="205">
						<shma:textbox id="txtNPR_SUMASSURED_2" tabIndex="12" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ReadOnly="true"></shma:textbox>
							<!--	<asp:comparevalidator id="cfvNPR_SUMASSURED_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_SUMASSURED_2"></asp:comparevalidator> -->
								</td>
						<shma:textbox id="txtNPR_PREMIUMDISCOUNT" tabIndex="11" runat="server" CssClass="RequiredField"
							Width="0px" BaseType="Number" Precision="0" MaxLength="15" ReadOnly="false"></shma:textbox>
							<!-- <asp:comparevalidator id="cfvNPR_PREMIUMDISCOUNT" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
							Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_PREMIUMDISCOUNT"></asp:comparevalidator> -->
							</TR>
					<!--<TR class="TRow_Alt" id="rowNPR_PREMIUM_2" style="VISIBILITY: hidden">
						<TD id="lbltxtNPR_PREMIUM" align="right" width="130">Basic Premium</TD>
						<TD id="ctltxtNPR_PREMIUM" align="right" width="205"><shma:textbox id="txtNPR_PREMIUM" tabIndex="13" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ReadOnly="true"></shma:textbox><asp:comparevalidator id="cfvNPR_PREMIUM" runat="server" ControlToValidate="txtNPR_PREMIUM" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator></TD>
						<TD id="lbltxtNP1_TOTALRIDERPREM" align="right" width="130">Rider Premium</TD>
						<TD id="ctltxtNP1_TOTALRIDERPREM" align="right" width="205"><shma:textbox id="txtNP1_TOTALRIDERPREM" tabIndex="14" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="17" SubType="Currency" ReadOnly="true"></shma:textbox><asp:comparevalidator id="cfvNP1_TOTALRIDERPREM" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNP1_TOTALRIDERPREM"></asp:comparevalidator></TD>
					<TR class="TRow_Normal" id="rowNPR_TOTPREM_2" style="VISIBILITY: hidden">
						<TD id="lbltxtNPR_TOTPREM_2" align="right" width="130">Total Premium</TD>
						<TD id="ctltxtNPR_TOTPREM_2" align="right" width="205"><shma:textbox id="txtNPR_TOTPREM_2" tabIndex="15" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="15" SubType="Currency" ReadOnly="true"></shma:textbox><asp:comparevalidator id="cfvNPR_TOTPREM_2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtNPR_TOTPREM_2"></asp:comparevalidator></TD>
						<td id="lbltxtcharges" align="right" width="130">Other Charges/Fee</td>
						<td id="ctltxtcharges" align="right" width="205"><shma:textbox id="txtcharges" tabIndex="16" runat="server" CssClass="DisplayOnly" Width="184px"
								BaseType="Number" Precision="2" MaxLength="17" SubType="Currency" ReadOnly="true"></shma:textbox><asp:comparevalidator id="Comparevalidator2" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								Type="Currency" Operator="DataTypeCheck" ControlToValidate="txtcharges"></asp:comparevalidator></td>
					</TR>
					<TR>
						<td>
							<P><asp:label id="lblServerError2" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
						</td>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR> -->
				</TABLE>
				<br>
				
				</div>
			
<table width=100% >
	<tr>
		<td class="button2TD" align=right>
			<asp:Button ID="btntest" Runat="server" CssClass="button2" OnClick="btntest_click" Text="test" Width="100" Height="20"/>
		
			<a href="#" id="btnAdd"class="button2" onclick="window.parent.location.reload();">Add New</a>
			<a href="#" id="btnSave" class="button2" onclick="save_()">&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;</a>
			<a href="#" id="btnGenerateIllustration" class="button2" onclick="calculate_Premium()">Generate Illustration</a>
		</td>
	</tr>
</table>
		</form>
		<SCRIPT language="javascript">
			
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>
			if (_lastEvent == 'New')	
				addClicked(); 	
			//fcStandardFooterFunctionsCall();//alert(_lastEvent);
			
			<asp:literal id="ErrorOccured" runat="server" EnableViewState="True"></asp:literal>

		function openPersonsLOV()
		{
			var wOpen;
			var sOptions;
			var aURL="../Presentation/PersonSelectionLOV.aspx";
			var aWinName="Persons_list";

			setFixedValuesInSession('opener=F');
			
			sOptions = "status=yes,menubar=no,scrollbars=no,resizable=no,toolbar=no";
			sOptions = sOptions + ',width=' + (screen.availWidth /2).toString();
			sOptions = sOptions + ',height=' + (screen.availHeight /2.6).toString();
			sOptions = sOptions + ',left=300,top=300';

			wOpen = window.open( '', aWinName, sOptions );
			wOpen.location = aURL;
			wOpen.focus();
			return wOpen;
		}
		
		try{
		
		
		}catch(e){}
		
		
		
//		try{CalculateEntryAge(myForm.txtNPH_BIRTHDATE);}catch(e){myForm.txtNP2_AGEPREM.value='0';}
		
		parent.closeWait2(navigation);
		
//		attachViewByNameNormal('txtNPH_BIRTHDATE');
//		attachViewFocus('INPUT');
//		attachViewFocus('SELECT');

		
		/************************************************************************/
		/********************* Screen Parameterization **************************/
//		setCombosValues();
		
		//Set ID Format
//		IDFormat = getFieldFormatFromSetup("PERSONNEL", document.getElementById("txtCNIC_VALUE"));
		
//		filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
		
		function setCombosValues()
		{
			if(getField("NPH_MARITALSTATUS").style.visibility == 'visible')
			{
				if(getField("NPH_MARITALSTATUS").disabled == false)
				{
					if(getField("NPH_MARITALSTATUS").value == "")
					{
						getField("NPH_MARITALSTATUS").value = "S";
					}
				}
			}
		}
		
		function checkMandatoryColumns()
		{
			if(getField("NPH_TITLE").value == "")
			{
				alert("Please select Title.");
				getField("NPH_TITLE").focus();
				return false;
			}
					
			if(getField("NPH_SEX").value == "")
			{
				alert("Please select Gender.");
				getField("NPH_SEX").focus();
				return false;
			}
			
			if(getField("NPH_MARITALSTATUS").style.visibility == 'visible')
			{
				if(getField("NPH_MARITALSTATUS").disabled == false)
				{
					if(getField("NPH_MARITALSTATUS").value == "")
					{
						alert("Please select Marital Status.");
						getField("NPH_MARITALSTATUS").focus();
						return false;
					}
				}
			}


				if(getField("CNIC_VALUE").disabled == false)
				{
					if(getField("CNIC_VALUE").value == "")
					{
						var idType = getField("NPH_IDTYPE").value;
						alert("Please select " + idType);
						getField("CNIC_VALUE").focus();
						return false;
					}
				}
			if(getField("NPH_BIRTHDATE").value == "")
			{
				alert("Please select Date of Birth.");
				getField("NPH_BIRTHDATE").focus();
				return false;
			}
			
			if(CalculateEntryAge(getField("NPH_BIRTHDATE")) == false)
			{
				getField("NPH_BIRTHDATE").focus();
				return false;
			}

			

			if(Page_IsValid == false){
				return false;
			}
			
			return true;
		}
		
		function beforeSave()
		{
			if(checkMandatoryColumns() == false)
			{
				return false;
			}
			else
			{
				//generateID(IDFormat);
				if(IDgenerated(IDFormat) == false)
				{
					alert("ID can't be empty");
					return false;
				}
				else
				{
					EnableFieldsBeforeSubmitting();
					disableFieldsBasedOnNIC(false);
					return true;
				}
			}
		}
		
		function beforeUpdate()
		{
			if(checkMandatoryColumns() == false)
			{
				return false;
			}
			else
			{	
				EnableFieldsBeforeSubmitting();
				
				disableFieldsBasedOnNIC(false);
				return true;
			}
		}
		
		function AnnualIncom_LostFocus(obj)
		{
			if(obj.style.visibility == 'visible')
			{
				if(obj.disabled == false)
				{
					if(obj.value == null || obj.value == "")
					{
						alert("Annual Income is mandatory.");
						obj.focus();
					}
				}
			}
		}
		
		function Name_GotFocus(objName)
		{
			document.getElementById("nameFormat").style.display = "";
			showNameDiv(true);
		}
		function Name_LostFocus(objName)
		{
			document.getElementById("nameFormat").style.display = "none";
		}
		/************************************************************************/
//		setFieldsBasedOnNIC(getField("CNIC_VALUE"));
		
		
		//**********************************************************************************************************
		//************************ Mulitple Name Chnages ***********************************************************
		//**********************************************************************************************************
		function getElementTop(Elem) {
			if(document.getElementById) {    
				var elem = document.getElementById(Elem);
			} else if (document.all) {
				var elem = document.all[Elem];
			}

			var yPos = elem.offsetTop;
			tempEl = elem.offsetParent;
			while (tempEl != null) {
				yPos += tempEl.offsetTop;
				tempEl = tempEl.offsetParent;
			}
			return yPos - 10 + 'px';
		}
		
		function getElementLeft(Elem) {
			if(document.getElementById) {    
				var elem = document.getElementById(Elem);
			} else if (document.all) {
				var elem = document.all[Elem];
			}

			var xPos = elem.offsetLeft;
			tempEl = elem.offsetParent;
			while (tempEl != null) {
				xPos += tempEl.offsetLeft;
				tempEl = tempEl.offsetParent;
			}
			return xPos -16 + 'px';
		}		

		function setDivPos()
		{
			personNameDiv.style.position = "absolute";
			personNameDiv.style.top = getElementTop("txtNPH_FULLNAME");
			personNameDiv.style.left = getElementLeft("txtNPH_FULLNAME");
		}
	
	
		//Variable related to NAMES Div only - should work only for IE-6
		var CombosHide = false;
		function showNameDiv(bln)
		{
			var gender = getField("NPH_SEX");
			var MaritalStatus = getField("NPH_MARITALSTATUS");
			var Occupation = getField("COP_OCCUPATICD");
			var OccupationClass = getField("CCL_CATEGORYCD");
			
		
			if(bln == true)
			{
				personNameDiv.style.display = "";
				setDivPos();
				getField("NPH_FIRSTNAME").focus();

				if(gender.style.display == '' || gender.style.visibility == 'visible')                  gender.style.visibility = 'hidden';
				if(MaritalStatus.style.display == '' || MaritalStatus.style.visibility == 'visible')    MaritalStatus.style.visibility = 'hidden';
				if(Occupation.style.display == '' || Occupation.style.visibility == 'visible')          Occupation.style.visibility = 'hidden';
				if(OccupationClass.style.display == '' || OccupationClass.style.visibility == 'visible')OccupationClass.style.visibility = 'hidden';
					
				CombosHide = true;
			}
			else
			{
				personNameDiv.style.display = "none";

				if(CombosHide == true)
				{
					if(gender.style.visibility == 'hidden')         gender.style.visibility = 'visible';
					if(MaritalStatus.style.visibility == 'hidden')  MaritalStatus.style.visibility = 'visible';
					if(Occupation.style.visibility == 'hidden')     Occupation.style.visibility = 'visible';
					if(OccupationClass.style.visibility == 'hidden')OccupationClass.style.visibility = 'visible';
				}
				CombosHide = false;
				
				getField("NPH_BIRTHDATE").focus();
			}
		}
		
		function btnOK_Click()
		{
			if(Trim(getField("NPH_FIRSTNAME").value).length > 0)
			{
				//*********************** Set Name first ***************************//
				//First Name
				getField("NPH_FULLNAME").value = Trim(getField("NPH_FIRSTNAME").value);
				//Second Name
				if(Trim(getField("NPH_SECONDNAME").value).length > 0) getField("NPH_FULLNAME").value += " " + Trim(getField("NPH_SECONDNAME").value);
				//Last Name
				if(Trim(getField("NPH_LASTNAME").value).length > 0)   getField("NPH_FULLNAME").value += " " + Trim(getField("NPH_LASTNAME").value);

				//*********************** Generate ID Now ***************************//
				showNameDiv(false);
				//Generate ID
			//	generateID(IDFormat);
			}
			else
			{
				alert("First Name can't be empty");
			}
		}
		</SCRIPT>
		</div>
	</body>
</HTML>
