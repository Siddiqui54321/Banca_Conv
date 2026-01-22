<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	   <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<asp:literal id="CSSLiteral" EnableViewState="True" runat="server"></asp:literal>
		<!--
			<script src="JSFiles/SearchCombo/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="JSFiles/SearchCombo/jquery.hyjack.select.js" type="text/javascript"></script>
    	<link href="JSFiles/SearchCombo/hyjack.css" rel="stylesheet" type="text/css" />
    	-->
		<script src="JSFiles/jquery-1.7.min.js" type="text/javascript"></script>
        <script src="JSFiles/popupWindow.js" type="text/javascript"></script>
        <link href="JSFiles/popupWindow.css" rel="stylesheet" type="text/css">

<%--       <script  type="text/javascript">
         	function showDialog(id, callback){
		//Get the screen height and width
		var maskHeight = $(document).height();
		var maskWidth = $(window).width();
	
		//Set heigth and width to mask to fill up the whole screen
		$('#mask').css({'width':maskWidth,'height':maskHeight});
		
		//transition effect		
		//$('#mask').fadeIn(10);	
		//$('#mask').fadeTo('slow',0.1);	
		$('#mask').show();
		
		//Get the window height and width
		var winH = $(window).height();
		var winW = $(window).width();
              
		//Set the popup window to center
		$(id).css('top',  0);
		$(id).css('left', 0);
	
		//transition effect
		//$(id).fadeIn(50); 
		$(id).show(); 
		if(callback)
			callback();
	}
	function hideDialog(id){
		$(id).hide();
	}

	$(document).ready(function () {

	    //select all the a tag with name equal to modal
	    $('a[name=modal]').click(function (e) {
	        //Cancel the link behavior
	        e.preventDefault();

	        //Get the A tag
	        var id = $(this).attr('href');

	        //Get the screen height and width
	        var maskHeight = $(document).height();
	        var maskWidth = $(window).width();

	        //Set heigth and width to mask to fill up the whole screen
	        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

	        //transition effect		
	        $('#mask').fadeIn(10);
	        $('#mask').fadeTo('slow', 0.1);

	        //Get the window height and width
	        var winH = $(window).height();
	        var winW = $(window).width();

	        //Set the popup window to center
	        $(id).css('top', 0);
	        $(id).css('left', 0);

	        //transition effect
	        $(id).fadeIn(50);

	    });

	    //if close button is clicked
	    $('.window .close').click(function (e) {
	        //Cancel the link behavior
	        e.preventDefault();

	        $('#mask').hide();
	        $('.window').hide();
	    });

	    //if mask is clicked
	    $('#mask').click(function () {
	        $(this).hide();
	        $('.window').hide();
	    });

	    $(window).resize(function () {

	        var box = $('#boxes .window');

	        //Get the screen height and width
	        var maskHeight = $(document).height();
	        var maskWidth = $(window).width();

	        //Set height and width to mask to fill up the whole screen
	        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

	        //Get the window height and width
	        var winH = $(window).height();
	        var winW = $(window).width();

	        //Set the popup window to center
	        box.css('top', winH / 2 - box.height() / 2);
	        box.css('left', winW / 2 - box.width() / 2);

	    });

	})
        
        </script>--%>

		
		<script language="javascript" type="text/javascript">
                function popitup(url) {
	                newwindow = window.open(url,'name','height=200,width=150');
	                if (window.focus) {newwindow.focus()}
	                return false;
                }
		</script>
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


		<!-- <!--column-management-array--> -->
		function RefreshFields()
		{				
			myForm.ddlNPH_TITLE.selectedIndex =0;
			myForm.ddlNPH_SEX.selectedIndex =0;
			myForm.ddlNPH_MARITALSTATUS.selectedIndex =0;
			myForm.txtNPH_FULLNAME.value ="";
			
			myForm.txtNPH_FIRSTNAME.value ="";
			myForm.txtNPH_SECONDNAME.value ="";
			myForm.txtNPH_LASTNAME.value ="";
			
			myForm.txtNPH_FULLNAMEARABIC.value ="";
			myForm.txtNPH_BIRTHDATE.value ="";
			myForm.txtNU1_ACCOUNTNO.value ="";
			myForm.ddlCOP_OCCUPATICD.selectedIndex =0;
			myForm.ddlCCL_CATEGORYCD.selectedIndex =0;
			myForm.ddlNPH_INSUREDTYPE.selectedIndex =0;
			//alert(1);
			//myForm.ddlNPH_IDTYPE.selectedIndex =0;
			//alert(2);
		    myForm.txtCNIC_VALUE.value ="";
		    myForm.txtNPH_ANNUINCOME.value ="";
			myForm.ddlNPH_HEIGHTTYPE.selectedIndex =2;
			if(application != 'ILLUSTRATION')
			{
				myForm.txtNU1_ACTUALHEIGHT.value ="";
				myForm.txtNU1_ACTUALWEIGHT.value ="";
			}
			myForm.txtNU1_CONVERTHEIGHT.value ="";
			myForm.ddlNPH_WEIGHTTTYPE.selectedIndex =1;
			myForm.txtNU1_CONVERTWEIGHT.value ="";
			myForm.txt_bmi.value ="";
			myForm.txtNPH_CODE.vaule="";

			setDefaultValues();
		}

		/******** NOTE: ************************************************/
		/******** 1. Override the JScriptFG.js method.******************/
		/******** 2. ID Existance Error will ask Question to User*******/
		/******** 3. Normal Error will be alert only.*******************/
		/***************************************************************/
		
		/*
		var IDError = "N";
		var ErrorOccured = false;
		var SaveWitNewID = false;
		function ErrorMessage(errMsg)
		{
			ErrorOccured = true;
			if (errMsg == '<<ID EXISTANCE ERROR>>') 
			{   
				var answer = confirm ("Client ID already exist, create new ID?")
				if(answer)
				{
					SaveWitNewID = true;
				}				
			}
			else
			{	
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
		
		function SaveWithNewID()
		{
			if(ErrorOccured == true)
			{
				if(SaveWitNewID == true)
				{
					setFixedValuesInSession("FLAG_IDEXIST=Y");
					parent.frames[3].saveClicked();
					SaveWitNewID = false;
				}
			}
		}*/
		
		/*function UpdateID()
		{
			if(IDError == "Y")
			{
				if(SaveWitNewID == true)
				{
					setFixedValuesInSession("FLAG_IDEXIST=Y");
					parent.frames[3].saveClicked();
					SaveWitNewID = false;
				}
			}
		}		

		function closeWaitDIV()
		{
			if(IDError == "Y")
				parent.Navigate(false);
			else
				parent.Navigate(true);
			return true;
		}*/	

		
			
		
		
		function CalculateEntryAge(objDate)
		{			
			var years = dateDiffYears(objDate.value, sysDate, parent.parent.ageRoundCriteria)
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
		
		/********** dependent combo's queries **********/

		//var str_QryCCL_CATEGORYCD="SELECT CCD_CODE "+getConcateOperator()+" '-' "+getConcateOperator()+" CCD_DESCR,CCD_CODE  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='~NP1_CHANNEL~'  ORDER BY CCD_DESCR";		
		//var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";		
		//var str_QryCCL_CATEGORYCD="SELECT c.ccl_categorycd "+getConcateOperator()+" '-' "+getConcateOperator()+" ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";
		var str_QryCCL_CATEGORYCD="SELECT ccl_description,c.ccl_categorycd  FROM LCOP_OCCUPATION C,LCCL_CATEGORY L WHERE C.CCL_CATEGORYCD = L.CCL_CATEGORYCD AND C.COP_OCCUPATICD='~COP_OCCUPATICD~'";
		var navigation = '';
        	</script>

    </HEAD>
	<body>
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Policy Owner Personal Details" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" method="post" name="myForm" runat="server">
			<div id="NormalEntryTableDiv" class="NormalEntryTableDiv" runat="server" style="Z-INDEX:-111">
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
						<TD id="ctlddlNPH_TITLE" style="HEIGHT: 23px" width="186"><SHMA:DROPDOWNLIST id="ddlNPH_TITLE" tabIndex="1" runat="server" CssClass="RequiredField" Width="184px"
								Onchange="Title_ChangeEvent(this);" DataValueField="CSD_TYPE" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST></TD>
						<TD id="lbltxtCNIC_VALUE" width="110" align="right">
							<SHMA:DROPDOWNLIST id="ddlNPH_IDTYPE" tabIndex="3" style="BORDER-BOTTOM:#444 0px solid; BORDER-LEFT:#444 0px solid; BORDER-TOP:#444 0px solid; BORDER-RIGHT:#444 0px solid"
								runat="server" CssClass="RequiredField" Width="100px" DataValueField="NPH_IDTYPE" DataTextField="desc_f"
								BlankValue="false"></SHMA:DROPDOWNLIST>
						</TD>
						<TD id="ctltxtCNIC_VALUE" width="186" style="HEIGHT: 23px">
							<shma:textbox onblur="NIC_Blur(this)" id="txtCNIC_VALUE" onfocus="NIC_Focus(this)" tabIndex="3"
								onkeypress="return NIC_KeyPress(event,this)" onkeyup="NIC_KeyUp(event,this)" runat="server"
								Width="184px" DESIGNTIMEDRAGDROP="79" MaxLength="15"></shma:textbox>
						</TD>
						<TD id="lbltxtNPH_IDNO2" width="110" style="DISPLAY:none" align="right">Old N.I.C.</TD>
						<TD id="ctltxtNPH_IDNO2" width="186" style="DISPLAY:none">
							<shma:textbox readonly="true" id="txtNPH_IDNO2" tabIndex="3" style="DISPLAY:none" runat="server"
								CssClass="RequiredField" Width="184px" DESIGNTIMEDRAGDROP="79" MaxLength="15"></shma:textbox>
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
													<td><shma:textbox id="txtNPH_FIRSTNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50" BaseType="Character" onblur="toTitleCase(this);"></shma:textbox></td>
													<td><shma:textbox id="txtNPH_SECONDNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50"
															BaseType="Character" onblur="toTitleCase(this);"></shma:textbox></td>
													<td><shma:textbox id="txtNPH_LASTNAME" tabIndex="4" runat="server" Width="160px" MaxLength="50" BaseType="Character" onblur="toTitleCase(this);"></shma:textbox></td>
													<td><a href="#" class="button2" tabIndex="4" onclick="btnOK_Click()">OK</a></td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</div>
							<shma:textbox id="txtNPH_FULLNAME" tabIndex="4" runat="server" CssClass="RequiredField" Width="160px"
								onchange="generateID(IDFormat);"  MaxLength="50" BaseType="Character"></shma:textbox>
							&nbsp; <INPUT class="BUTTON" title="Open Proposal List Of Values" tabIndex="0" onclick="openPersonsLOV();"
								value=".." type="button" name="ProposalLov"> <label id="nameFormat" style="DISPLAY:none">
								<font color="#009900">First&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Middle&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Last</font>
							</label>
								<asp:requiredfieldvalidator id="rfvNPH_FULLNAME" runat="server" Display="Dynamic" 
							ControlToValidate="txtNPH_FULLNAME"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="reNPH_FULLNAME" runat="server" Display="Dynamic" 
							ErrorMessage="String Format is Incorrect"
								ControlToValidate="txtNPH_FULLNAME" ValidationExpression="[A-Za-z\s]+"></asp:regularexpressionvalidator> 
						</TD>


						<TD id="lbltxtNPH_FULLNAMEARABIC" width="110" align="right">Name in Arabic</TD>
						<TD id="ctltxtNPH_FULLNAMEARABIC" width="186">
							<shma:textbox id="txtNPH_FULLNAMEARABIC" tabIndex="0" runat="server" Width="184px" MaxLength="50"
								BaseType="Character"></shma:textbox><asp:comparevalidator id="cfvNPH_FULLNAMEARABIC" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="txtNPH_FULLNAMEARABIC" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator></TD>
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
							&nbsp;<font id="lbltxtNP2_AGEPREM">Age</font>&nbsp;<shma:textbox id="txtNP2_AGEPREM" tabIndex="6" CssClass="DisplayOnly" runat="server" Width="40px"
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
							
							<!--hyjack--> 
							<!-- <a href="#dialog" name="modal">Simple Window Modal</a>  -->
                                <SHMA:DROPDOWNLIST CssClass="RequiredField" id="ddlCOP_OCCUPATICD" tabIndex="8" BlankValue="True" runat="server"
								Width="160px" DataValueField="COP_OCCUPATICD" DataTextField="desc_f"></SHMA:DROPDOWNLIST>
                                
<%--							&nbsp; <INPUT class="BUTTON" title="Open Occupation List Of Values" tabIndex="8" onclick="openOccupationDialog();"
								value=".." type="button" name="ProposalLov "> 
--%>						 
                            <asp:comparevalidator id="cfvCOP_OCCUPATICD" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlCOP_OCCUPATICD" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCOP_OCCUPATICD" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlCOP_OCCUPATICD"></asp:requiredfieldvalidator></TD>
                                
						<TD id="lblddlCCL_CATEGORYCD" width="110" align="right">Occupational&nbsp;Class</TD>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlCCL_CATEGORYCD" tabIndex="9" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="CCL_CATEGORYCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCCL_CATEGORYCD" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlCCL_CATEGORYCD" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCCL_CATEGORYCD" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlCCL_CATEGORYCD"></asp:requiredfieldvalidator></TD>
					</TR>


					<TR id="rowHeightWeight" class="TRow_Alt" runat="server">
						<TD style="HEIGHT: 21px" id="TDtxtNU1_ACCOUNTNO1" width="110" align="right">Height</TD>
						<TD style="HEIGHT: 21px" width="186"><SHMA:DROPDOWNLIST id="ddlNPH_HEIGHTTYPE" tabIndex="9" runat="server" CssClass="RequiredField" Width="65px"
								Onchange="convert_to_feet()">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="I">Inches</asp:ListItem>
								<asp:ListItem Value="F">Feet</asp:ListItem>
								<asp:ListItem Value="C">Centimeter</asp:ListItem>
								<asp:ListItem Value="M">Meters</asp:ListItem>
							</SHMA:DROPDOWNLIST>&nbsp;<shma:textbox onblur="convert_to_feet()" id="txtNU1_ACTUALHEIGHT" tabIndex="9" runat="server"
								CssClass="RequiredField" Width="48px" MaxLength="4" BaseType="Character"></shma:textbox>&nbsp;
							<shma:textbox id="txtNU1_CONVERTHEIGHT" tabIndex="10" runat="server" CssClass="DisplayOnly" Width="48px"
								MaxLength="12" BaseType="Character" ></shma:textbox>m
							<asp:comparevalidator id="Comparevalidator1" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								ControlToValidate="txtNU1_ACTUALHEIGHT" Type="Double" Operator="DataTypeCheck"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNU1_ACTUALHEIGHT"></asp:requiredfieldvalidator></TD>
						<td width="106" align="right">&nbsp; Weight
						</td>
						<TD width="186"><SHMA:DROPDOWNLIST id="ddlNPH_WEIGHTTTYPE" tabIndex="10" runat="server" CssClass="RequiredField" Width="65px"
								Onchange="Weight_Conversion()">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="K">Kilogram</asp:ListItem>
								<asp:ListItem Value="L">LBs</asp:ListItem>
							</SHMA:DROPDOWNLIST>&nbsp;<shma:textbox onblur="Weight_Conversion()" id="txtNU1_ACTUALWEIGHT" tabIndex="10" runat="server"
								CssClass="RequiredField" Width="48px" MaxLength="3" BaseType="Character"></shma:textbox>&nbsp;<shma:textbox id="txtNU1_CONVERTWEIGHT" tabIndex="10" runat="server" CssClass="DisplayOnly" Width="48px"
								MaxLength="12" BaseType="Character" ></shma:textbox>kg<asp:comparevalidator id="Comparevalidator3" runat="server" Display="Dynamic" ErrorMessage="Number Format is Incorrect "
								ControlToValidate="txtNU1_ACTUALWEIGHT" Type="Double" Operator="DataTypeCheck"></asp:comparevalidator>
							<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNU1_ACTUALWEIGHT"></asp:requiredfieldvalidator></TD>
					</TR>
		
					<TR id="rowBMIAccount" class="TRow_Normal" runat="server">
						<TD id="lbltxt_bmi" width="106" align="right">BMI&nbsp;&nbsp;</TD>
						<TD id="ctltxt_bmi" width="186"><shma:textbox id="txt_bmi" tabIndex="111" runat="server" CssClass="RequiredField" Width="184px"
								MaxLength="50" BaseType="Character" ></shma:textbox></TD>
						
						
						
						
						<TD id="lbltxtNU1_ACCOUNTNO" align="right" width="110">Account No</TD>
						<TD id="ctltxtNU1_ACCOUNTNO" width="186">
							<shma:textbox id="txtNU1_ACCOUNTNO" tabIndex="10" runat="server" CssClass="RequiredField" Width="184px"
								onkeypress="return checkNumeric(event)" MaxLength="17" BaseType="Character"></shma:textbox>
							<asp:requiredfieldvalidator id="rfvtxtNU1_ACCOUNTNO" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="txtNU1_ACCOUNTNO"></asp:requiredfieldvalidator>
						</TD>
						
					</TR>
					
					<TR class="TRow_Normal">
						<TD id="TDtxtNU1_ACCOUNTNO" width="110" align="right">Smoker</TD>
						<TD width="186">
							<SHMA:DROPDOWNLIST id="ddlNU1_SMOKER" tabIndex="10" runat="server" CssClass="RequiredField" width="184px">
								<asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:DROPDOWNLIST>
							<asp:comparevalidator id="cfvNU1_SMOKER" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlNU1_SMOKER" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator></TD>
						<asp:comparevalidator id="cfvNU1_ACCOUNTNO" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
							ControlToValidate="txtNU1_ACCOUNTNO" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator></TD>
							
							<TD id="lblddlBranch" align="right" width="110">Branch</TD>
							<TD id="ctlddlBranch" width="186">
								<SHMA:DROPDOWNLIST id="ddlBranch" tabIndex="11" runat="server" CssClass="RequiredField" Width="184px" DataValueField="ccs_field1" DataTextField="ccs_descr"></SHMA:DROPDOWNLIST>
							</td>
						
					</TR>
					<TR class="TRow_Alt">
						<TD width="106" align="right" id="lbltxtNPH_ANNUINCOME">Yearly Income</TD>
						<TD width="186" id="ctltxtNPH_ANNUINCOME">
							<shma:textbox id="txtNPH_ANNUINCOME" tabIndex="20" runat="server" Width="184px" CssClass="RequiredField"
								MaxLength="15" onblur="AnnualIncom_LostFocus(this);" BaseType="Number" subtype="Currency"
								Precision="2" ReadOnly="false"></shma:textbox>
							<asp:comparevalidator id="cfvNPH_ANNUINCOME" runat="server" ControlToValidate="txtNPH_ANNUINCOME" Operator="DataTypeCheck"
								Type="Currency" ErrorMessage="Number Format is Incorrect " Display="Dynamic"></asp:comparevalidator>
						</TD>
						
						<TD style="HEIGHT: 21px" width="106">
							<P align="right">&nbsp;Single Life?</P>
						</TD>
						<TD style="HEIGHT: 21px" width="186"><SHMA:DROPDOWNLIST id="ddlNPH_INSUREDTYPE" tabIndex="11" runat="server" CssClass="RequiredField" Width="184px"
								onchange="setViewSecondLife(this.value)">
								<asp:ListItem Value="Y">Yes</asp:ListItem>
								<asp:ListItem Value="N">No</asp:ListItem>
							</SHMA:DROPDOWNLIST></TD>
						
						
						<TD id="lblddlCNT_NATCD" style="HEIGHT: 23px" align="right" width="106">Nationality</TD>
						<TD id="ctlddlCNT_NATCD" style="HEIGHT: 23px" width="186"><SHMA:DROPDOWNLIST id="ddlCNT_NATCD" tabIndex="21" runat="server" CssClass="RequiredField" Width="184px"
								DataValueField="CNT_NATCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST></TD>
					</TR>
					</TR>
					<TR>
						<TD width="106">
							<P>&nbsp;</P>
						</TD>
						<TD width="186"></TD>
						<TD id="TDtxtNU1_ACCOUNTNO3" width="110" align="right"></TD>
						<TD width="186">
						<asp:comparevalidator id="cfvNPH_INSUREDTYPE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
								ControlToValidate="ddlNPH_INSUREDTYPE" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator>
								<asp:requiredfieldvalidator id="rfvNPH_INSUREDTYPE" runat="server" Display="Dynamic" ErrorMessage="Required"
								ControlToValidate="ddlNPH_INSUREDTYPE"></asp:requiredfieldvalidator>
								</TD>
					</TR>
					<TR id="rowNPH_INSUREDTYPE" class="TRow_Alt">
						<TD id="TDddlNPH_INSUREDTYPE" width="110" align="right"></TD>
						<TD width="186">
							<P><asp:label id="lblServerError" EnableViewState="false" runat="server" Visible="False" ForeColor="Red"></asp:label></P>
						</TD>
					</TR>
				</TABLE>
			</div>
			<INPUT id="HiddenNIC" type="hidden" name="HiddenNIC" runat="server"> 
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server"> 
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick"> 
			<INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<shma:textbox id="txtNPH_CODE" runat="server" Width="0px" BaseType="Character"></shma:textbox>
			<asp:comparevalidator id="cfvNPH_CODE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
			ControlToValidate="txtNPH_CODE" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator>
			<shma:textbox id="txtNPH_LIFE" runat="server" Width="0px" BaseType="Character"></shma:textbox>
			<asp:comparevalidator id="cfvNPH_LIFE" runat="server" Display="Dynamic" ErrorMessage="String Format is Incorrect "
			ControlToValidate="txtNPH_LIFE" EnableClientScript="False" Type="String" Operator="DataTypeCheck"></asp:comparevalidator>
			<SCRIPT language="javascript">
				
			
			function setViewSecondLife(val)
			{				
				//alert("setViewSecondLife");
				//parent.frames[2].style.visibility = 'hidden';
				if (val=='N')
				{
					parent.frames[2].document.getElementById('NormalEntryTableDiv').style.visibility = 'visible'; 
					parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').height = 210;//135
					parent.parent.document.getElementById('mainContentFrame').style.height = '700.0px';//'460.0px';
				}
				else
				{
					parent.frames[2].document.getElementById('NormalEntryTableDiv').style.visibility = 'hidden'; 
					parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS').height = 0;
					parent.parent.document.getElementById('mainContentFrame').style.height = '500.0px';//optional
				}
			}
		
		</SCRIPT>
			<div id="boxes">
				<div id="dialog" class="window">
					<div width="100%">
					<input type="button" value="Close" onclick="closeOccupationDialog();" style="POSITION: relative; RIGHT: 1px"></input>
					<label class="fieldHeading">Search :</label>
					<asp:TextBox Runat="server" ID="txtSearchOccupation"></asp:TextBox>
					<input type="button" id="btnSearch" value="Search" onclick="filterResult();"></input>
					<div id='imgLoading'>
					<img id='img' style="width:15px;" src="Images/loading.gif"/>
					Please Wait...
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
								showDialog('#dialog',function(){
									//alert($('#ulOccupation li').length );
									if($('#ulOccupation li').length==0)  
										$('#imgLoading').show('fast',function(){
											$('#imgLoading').delay(500).queue(function(){
												jasonCallToGetOccupations(function(){
													$('#imgLoading').hide();}
												);
											});
										});
									else{
										$('#imgLoading').hide();}
								});}
                                
							function closeOccupationDialog(){
								hideDialog('#dialog');
								document.getElementById("ddlCCL_CATEGORYCD").focus();
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
		</form>
		<SCRIPT language="javascript">
			
			<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>
			if (_lastEvent == 'New')	
				addClicked(); 	
			fcStandardFooterFunctionsCall();//alert(_lastEvent);
			
			<asp:literal id="ErrorOccured" runat="server" EnableViewState="True"></asp:literal>
		        document.getElementById("txtNU1_CONVERTHEIGHT").readOnly=true;
                document.getElementById("txtNU1_CONVERTWEIGHT").readOnly=true;
                document.getElementById("txt_bmi").readOnly=true;


		Weight_Conversion();
		convert_to_feet();
		myForm.txtNPH_FULLNAMEARABIC.disabled =true;
		function filterClass(obj_Ref)
		{ 
			//if(myForm.ddlCOP_OCCUPATICD.disabled == true)//As now User can select Category if it is disable 
			{   //Show all record if Occupation is disabled (Do not filter)
				//str_QryCCL_CATEGORYCD="SELECT CCL_DESCRIPTION, CCL_CATEGORYCD from LCCL_CATEGORY";
			}
			fcfilterChildCombo(obj_Ref, str_QryCCL_CATEGORYCD, "ddlCCL_CATEGORYCD");
			document.getElementById('ddlCCL_CATEGORYCD').selectedIndex = 1;
		}

		
		
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
			//alert("11");
			if (document.getElementById('ddlNPH_INSUREDTYPE').value=='Y')
			{
				//alert("22");
				parent.frames[2].location=parent.frames[2].location;			
			}
			//alert("calling setViewSecondLife");
			setViewSecondLife(document.getElementById('ddlNPH_INSUREDTYPE').value);
		
		//parent.parent.setPage('shgn_gp_gp_ILUS_ET_GP_BENEFECIARY');
		
		}catch(e){}
		
		
		
		try{CalculateEntryAge(myForm.txtNPH_BIRTHDATE);}catch(e){myForm.txtNP2_AGEPREM.value='0';}
		
		
		//parent.closeWait();
		parent.closeWait2(navigation);
		
		
		attachViewByNameNormal('txtNPH_BIRTHDATE');
		//filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
		attachViewFocus('INPUT');
		attachViewFocus('SELECT');

		
		/************************************************************************/
		/********************* Screen Parameterization **************************/
		//hideColumn();
		setFieldStatusAsPerClientSetup("PERSONNEL");
		setCombosValues();
		
		//Set ID Format
		IDFormat = getFieldFormatFromSetup("PERSONNEL", document.getElementById("txtCNIC_VALUE"));
		
		filterClass(document.getElementById('ddlCOP_OCCUPATICD'));
		
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
			
			/*if(!(getField("NU1_ACCOUNTNO").value.length == 8 || getField("NU1_ACCOUNTNO").value.length == 9 || getField("NU1_ACCOUNTNO").value.length == 11 || getField("NU1_ACCOUNTNO").value.length == 12))
			{
				alert("Length of Account No. can only 8,9,11 or 12.");
				getField("txtNU1_ACCOUNTNO").focus();
				return false;
			}*/
			
			
			if((getField("NU1_ACCOUNTNO").value.length == 0))
			{
				alert("Please Provide Account No.");
				getField("txtNU1_ACCOUNTNO").focus();
				return false;
			}
			
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


			//ID Number (NIC, CNIC, POC etc.)
			//if(getField("CNIC_VALUE").style.visibility == 'visible')
			//{
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
			//}

			//Nationality
			if(getField("CNT_NATCD").style.visibility == 'visible')
			{
				if(getField("CNT_NATCD").disabled == false)
				{
					if(getField("CNT_NATCD").value == "")
					{
						alert("Please select Nationality.");
						getField("CNT_NATCD").focus();
						return false;
					}
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

			if(application == "BANCASSURANCE")
			{
				/*
				if(getField("NU1_ACCOUNTNO").value == "")
				{
					alert("Please enter Account No.");
					getField("NU1_ACCOUNTNO").focus();
					return false;
				}	
				*/
			}
			
			//Page_ClientValidate();//this is commented as all Validators are not required
			var rfv = document.getElementById("rfvtxtNU1_ACCOUNTNO");
			ValidatorValidate(rfv);

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
					
					getField("COP_OCCUPATICD").disabled = false;
					
					return true;
				}
			}
		}
		
		function beforeUpdate()
		{
            //alert("before calling txtNU1_CONVERTHEIGHT  "+document.getElementById("txtNU1_CONVERTHEIGHT").value);
            //alert("before calling txtNU1_CONVERTWEIGH "+document.getElementById("txtNU1_CONVERTWEIGHT").value);
            //    document.getElementById("txtNU1_CONVERTHEIGHT").readOnly = false;
			//	document.getElementById("txtNU1_CONVERTWEIGHT").readOnly = false;
			if(checkMandatoryColumns() == false)
			{
				return false;
			}
			else
			{	
				EnableFieldsBeforeSubmitting();
				disableFieldsBasedOnNIC(false);
				
				getField("COP_OCCUPATICD").disabled = false;
				
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
		setFieldsBasedOnNIC(getField("CNIC_VALUE"));
		
		
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
				generateID(IDFormat);
			}
			else
			{
				alert("First Name can't be empty");
			}
		}
		/*
		$('.hyjack').hyjack_select();	
		$('.hyjack').hyjack_select({            // Defaults
				ddImage: 'JSFiles/SearchCombo/arrow.png',      // arrow_down.png         
				ddCancel: 'JSFiles/SearchCombo/cancel.png',    // cancel.png         
				ddImageClass: 'class_of_arrow',     // hjsel_ddImage         
				ddCancelClass: 'class_of_cancel',   // hjsel_ddCancel         
				emptyMessage: 'No Items Found',   // No Items to Display         
				restrictSearch: true,         // false         
				offset: 12            // false     
			});
			*/
		//**********************************************************************************************************
		//************************ Mulitple Name Chnages - End *****************************************************
		//**********************************************************************************************************
		
		</SCRIPT>
		</DIV>
	</body>
</HTML>
