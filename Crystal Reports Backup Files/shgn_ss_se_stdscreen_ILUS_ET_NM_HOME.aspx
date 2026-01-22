<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_HOME.aspx.cs" AutoEventWireup="false" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_HOME" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/MI_ET_NM_PolicyEntry"></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';


		
		<!-- <!--column-management-array--> -->

		
		function RefreshFields()
		{				
							 myForm.txtNP1_PRODUCER.value ="";
							 myForm.ddlCCN_CTRYCD.selectedIndex =0;
							 myForm.ddlNP1_CHANNEL.selectedIndex =0;
							 myForm.ddlNP1_CHANNELDETAIL.selectedIndex =0;
							 myForm.txtNP2_COMMENDATE.value ="";
			

			setDefaultValues();
		}
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<UC:ENTITYHEADING id="EntityHeading" runat="server" ParamValue="Home" ParamSource="FixValue"></UC:ENTITYHEADING>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" style="Z-INDEX: 101; LEFT: 80px; WIDTH: 521px; TOP: 88px; HEIGHT: 368px"
				runat="server">
				<fieldset id="FldSetProposal" style="BORDER-RIGHT: #e0f3be 1px solid; BORDER-TOP: #e0f3be 1px solid; BORDER-LEFT: #e0f3be 1px solid; WIDTH: 200px; BORDER-BOTTOM: #e0f3be 1px solid"><legend><!--Entry--></legend>
					<TABLE id="entryTable" style="WIDTH: 511px" cellSpacing="5" cellPadding="1" border="0">
						<shma:textbox id="txtNP1_PROPOSAL" runat="server" BaseType="Character" width="0px"></shma:textbox><asp:comparevalidator id="cfvNP1_PROPOSAL" runat="server" Display="Dynamic" EnableClientScript="False"
							ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP1_PROPOSAL"></asp:comparevalidator>
						<TR class="TRow_Normal" id="rowNP1_PRODUCER">
							<TD align="right" width="106">Producer</TD>
							<TD width="186"><shma:textbox id="txtNP1_PRODUCER" tabIndex="1" runat="server" BaseType="Character" CssClass="RequiredField"
									MaxLength="50" Width="184px"></shma:textbox><asp:comparevalidator id="cfvNP1_PRODUCER" runat="server" Display="Dynamic" EnableClientScript="False"
									ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="txtNP1_PRODUCER"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP1_PRODUCER" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="txtNP1_PRODUCER"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR id="rowCCN_CTRYCD">
							<TD align="right" width="106">Country</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlCCN_CTRYCD" tabIndex="2" runat="server" CssClass="RequiredField" Width="184px"
									DataValueField="CCN_CTRYCD" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCCN_CTRYCD" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="String Format is Incorrect "
									Type="String" Operator="DataTypeCheck" ControlToValidate="ddlCCN_CTRYCD"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlCCN_CTRYCD"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Normal" id="rowNP1_CHANNEL">
							<TD align="right" width="106">Channel</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNEL" tabIndex="3" runat="server" CssClass="RequiredField" Width="184px"
									DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNP1_CHANNEL" runat="server" Display="Dynamic" EnableClientScript="False"
									ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNP1_CHANNEL"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP1_CHANNEL" runat="server" Display="Dynamic" ErrorMessage="Required" ControlToValidate="ddlNP1_CHANNEL"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR id="rowNP1_CHANNELDETAIL">
							<TD align="right" width="106">Channel Detail</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNELDETAIL" tabIndex="4" runat="server" CssClass="RequiredField" Width="184px"
									DataValueField="csd_type" DataTextField="desc_f" BlankValue="True"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNP1_CHANNELDETAIL" runat="server" Display="Dynamic" EnableClientScript="False"
									ErrorMessage="String Format is Incorrect " Type="String" Operator="DataTypeCheck" ControlToValidate="ddlNP1_CHANNELDETAIL"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP1_CHANNELDETAIL" runat="server" Display="Dynamic" ErrorMessage="Required"
									ControlToValidate="ddlNP1_CHANNELDETAIL"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Normal" id="rowNP2_COMMENDATE">
							<TD align="right" width="106">Date</TD>
							<TD width="186"><SHMA:DATEPOPUP id="txtNP2_COMMENDATE" tabIndex="5" runat="server" CssClass="RequiredField" Width="100px"
									ImageUrl="Images/CalendarImage/image1.jpg" ExternalResourcePath="jsfiles/DatePopUp.js" maxlength="0">
									<WeekdayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="White"></WeekdayStyle>
									<MonthHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="Yellow"></MonthHeaderStyle>
									<OffMonthStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"
										BackColor="AntiqueWhite"></OffMonthStyle>
									<GoToTodayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="White"></GoToTodayStyle>
									<TodayDayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="LightGoldenrodYellow"></TodayDayStyle>
									<DayHeaderStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="Orange"></DayHeaderStyle>
									<WeekendStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="LightGray"></WeekendStyle>
									<SelectedDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="Yellow"></SelectedDateStyle>
									<ClearDateStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="White"></ClearDateStyle>
									<HolidayStyle Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"
										BackColor="White"></HolidayStyle>
								</SHMA:DATEPOPUP><asp:comparevalidator id="cfvNP2_COMMENDATE" runat="server" Display="Dynamic" ErrorMessage="Date Format is Incorrect "
									Type="Date" Operator="DataTypeCheck" ControlToValidate="txtNP2_COMMENDATE"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP2_COMMENDATE" runat="server" Display="Dynamic" ErrorMessage="Required"
									ControlToValidate="txtNP2_COMMENDATE"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR>
							<td width="106">
								<P><asp:label id="lblServerError" runat="server" EnableViewState="false" ForeColor="Red" Visible="False"></asp:label></P>
							</td>
							<TD width="186"></TD>
						</TR>
					</TABLE>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server">
			<asp:imagebutton id="btnNext" style="Z-INDEX: 102; LEFT: 472px; POSITION: absolute; TOP: 272px" runat="server"
				Width="120px" ImageUrl="file:///F:\virtual\Aceins\Presentation\Images\buttons\next.gif" Height="32px"></asp:imagebutton><INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall(); document.myForm.ddlCCN_CTRYCD.focus();</script>
	</body>
</HTML>
