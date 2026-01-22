<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_NM_WELCOME.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_NM_WELCOME" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
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
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->

		function RefreshFields()
		{				
							 myForm.txtAAG_NAME.value ="";
							 myForm.ddlCCN_CTRYCD.selectedIndex =0;
							 myForm.ddlNP1_CHANNEL.selectedIndex =0;
							 myForm.ddlNP1_CHANNELDETAIL.selectedIndex =0;
							 myForm.txtNP2_COMMENDATE.value ="";
			

			setDefaultValues();
		}
		window.location="shgn_gp_gp_ILUS_ET_GP_PERONAL.aspx";
		/********** dependent combo's queries **********/
		//var str_QryNP1_CHANNEL="SELECT CCH_CODE "+getConcateOperator()+" '-' "+getConcateOperator()+" CCH_DESCR,CCH_CODE  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '784')  AND CCN_CTRYCD='~CCN_CTRYCD~'   ORDER BY CCH_DESCR";
		var str_QryNP1_CHANNEL="SELECT CCH_DESCR,CCH_CODE  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '~CCN_CTRYCD~')  ORDER BY CCH_DESCR";
		
		//var str_QryNP1_CHANNELDETAIL="SELECT CCD_CODE "+getConcateOperator()+" '-' "+getConcateOperator()+" CCD_DESCR,CCD_CODE  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='01'  AND NP1_CHANNEL='~NP1_CHANNEL~'   ORDER BY CCD_DESCR";
		var str_QryNP1_CHANNELDETAIL="SELECT CCD_DESCR,CCD_CODE  FROM CCD_CHANNELDETAIL  WHERE CCH_CODE='~NP1_CHANNEL~'  ORDER BY CCD_DESCR";

			
		</script>
	</HEAD>
	<body onkeydown="enter();">
		<UC:ENTITYHEADING id="EntityHeading" ParamSource="FixValue" ParamValue="Home" runat="server"></UC:ENTITYHEADING>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" runat="server">
				<fieldset id="EntryFldSet" style="BORDER-RIGHT: #dee8ca 3px solid; BORDER-TOP: #dee8ca 3px solid; LEFT: 150px; BORDER-LEFT: #dee8ca 3px solid; WIDTH: 360px; BORDER-BOTTOM: #dee8ca 3px solid; POSITION: absolute; TOP: 72px; HEIGHT: 158px">
					<TABLE id="entryTable" style="LEFT: 5px; WIDTH: 340px; POSITION: absolute; TOP: 0px" cellSpacing="0"
						cellPadding="2" border="0">
						<shma:textbox id="txtNP1_PROPOSAL" runat="server" width="0px" BaseType="Character"></shma:textbox><asp:comparevalidator id="cfvNP1_PROPOSAL" runat="server" ControlToValidate="txtNP1_PROPOSAL" Operator="DataTypeCheck"
							Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator>
						<TR class="TRow_Normal" id="rowAAG_NAME">
							<TD align="right" width="106">Producer</TD>
							<TD width="186"><shma:textbox id="txtAAG_NAME" tabIndex="1" runat="server" BaseType="Character" readonly="true"
									Width="184px" MaxLength="60" CssClass="DisplayOnly" Precision="0"></shma:textbox><asp:requiredfieldvalidator id="rfvAAG_NAME" runat="server" ControlToValidate="txtAAG_NAME" ErrorMessage="Required"
									Display="Dynamic" Precision="0"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Alt" id="rowCCN_CTRYCD">
							<TD align="right" width="106">Country</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlCCN_CTRYCD" tabIndex="2" runat="server" Width="184px" CssClass="RequiredField"
									BlankValue="True" DataTextField="desc_f" DataValueField="CCN_CTRYCD"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvCCN_CTRYCD" runat="server" ControlToValidate="ddlCCN_CTRYCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" ControlToValidate="ddlCCN_CTRYCD" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Normal" id="rowNP1_CHANNEL">
							<TD align="right" width="106">Channel</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNEL" tabIndex="3" runat="server" Width="184px" CssClass="RequiredField"
									BlankValue="True" DataTextField="desc_f" DataValueField="CCH_CODE"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNP1_CHANNEL" runat="server" ControlToValidate="ddlNP1_CHANNEL" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP1_CHANNEL" runat="server" ControlToValidate="ddlNP1_CHANNEL" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Alt" id="rowNP1_CHANNELDETAIL">
							<TD align="right" width="106">Channel Detail</TD>
							<TD width="186"><SHMA:DROPDOWNLIST id="ddlNP1_CHANNELDETAIL" tabIndex="4" runat="server" Width="184px" CssClass="RequiredField"
									BlankValue="True" DataTextField="desc_f" DataValueField="CCD_CODE"></SHMA:DROPDOWNLIST><asp:comparevalidator id="cfvNP1_CHANNELDETAIL" runat="server" ControlToValidate="ddlNP1_CHANNELDETAIL"
									Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP1_CHANNELDETAIL" runat="server" ControlToValidate="ddlNP1_CHANNELDETAIL"
									ErrorMessage="Required" Display="Dynamic"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR class="TRow_Normal" id="rowNP2_COMMENDATE">
							<TD align="right" width="106">Date</TD>
							<TD width="186"><SHMA:DATEPOPUP id="txtNP2_COMMENDATE" tabIndex="5" runat="server" Width="167px" CssClass="RequiredField"
									maxlength="0" ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"></SHMA:DATEPOPUP><asp:comparevalidator id="cfvNP2_COMMENDATE" runat="server" ControlToValidate="txtNP2_COMMENDATE" Operator="DataTypeCheck"
									Type="Date" ErrorMessage="Date Format is Incorrect " Display="Dynamic"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvNP2_COMMENDATE" runat="server" ControlToValidate="txtNP2_COMMENDATE" ErrorMessage="Required"
									Display="Dynamic"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR>
							<td width="106">
								<P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD width="186"></TD>
						</TR>
					</TABLE>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server"> <INPUT id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
			<INPUT id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server"> <INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			
			<A href="#">				
				<IMG id="imgbtnNext" onmouseover="this.src='../shmalib/images/buttons/next_2.gif'" style="Z-INDEX: 102; LEFT: 424px; POSITION: absolute; TOP: 272px" 
					onclick="document.getElementById('btnNext').click();" alt="" src="../shmalib/images/buttons/next.gif" border="0" name="imgbtnNext"
					onmouseout="this.src='../shmalib/images/buttons/next.gif'" />
				<asp:imagebutton id="btnNext" name="btnNext" onclick="btnNext_Click" tabIndex="0" runat="server" width="0" ImageUrl="../shmalib/images/buttons/next.gif"
					border="0" height="0" style="Z-INDEX: 102; POSITION: absolute; TOP: -10px; LEFT: 0px">
				</asp:imagebutton>
			</A>
			
			<INPUT id="frm_FetchData_qry" type="hidden" name="frm_FetchData_qry">
			<script language="javascript">
			
			function renderDefaultView()
			{
				setFetchDataQry("select CCN_CTRYCD  from LUCN_USERCOUNTRY  where USE_USERID='" + strUser + "' AND UCN_DEFAULT='Y'");
				fetchData();
			
				if(getField("CCN_CTRYCD").options.length>1)
				{
					//getField("CCN_CTRYCD").selectedIndex=1;
					//getField("CCN_CTRYCD").onchange();
					filterChannel(getField("CCN_CTRYCD"));
					//Select Default Channel
					setFetchDataQry("select CCH_CODE as NP1_CHANNEL from LUCH_USERCHANNEL where USE_USERID='" + strUser + "' AND UCH_DEFAULT='Y'");
					fetchData();					
				}

				if(getField("NP1_CHANNEL").options.length>1)
				{
					//getField("NP1_CHANNEL").selectedIndex=1;
					filterChannelDetail(getField("NP1_CHANNEL"));
				}
				
				if(getField("NP1_CHANNELDETAIL").options.length>1)
				{
					getField("ddlNP1_CHANNELDETAIL").selectedIndex=1;
				}
				
				getField("NP2_COMMENDATE").value = txtDateNow;
				
				getField("AAG_NAME").value = valAAG_NAME;
				
				getField("NP2_COMMENDATE").disabled=true;

			}
			
			
			
			
			function filterChannel(obj_Ref)
			{ 
				//fcfilterChildCombo(obj_Ref,str_QryPLC_LOCACODE+" order by PLC_LOCACODE ASC","ddlNPW_REQUIREDFOR");
				fcfilterChildCombo(obj_Ref,str_QryNP1_CHANNEL,"ddlNP1_CHANNEL");
				
			}
	

			function filterChannelDetail(obj_Ref)
			{ 
				//fcfilterChildCombo(obj_Ref,str_QryPLC_LOCACODE+" order by PLC_LOCACODE ASC","ddlNPW_REQUIREDFOR");
				fcfilterChildCombo(obj_Ref,str_QryNP1_CHANNELDETAIL,"ddlNP1_CHANNELDETAIL");
			}
			
	
		
		
	
		</script>
		</form>
		<script language="javascript">
		<asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		
		
		if (_lastEvent == 'New')	
			addClicked(); 	
		
		fcStandardFooterFunctionsCall();
		fetchData();
		renderDefaultView();	
			
		function enter(){    
	      if (event.keyCode == 13) {  
            event.returnValue=false;
            event.cancel=true;
            document.getElementById('btnNext').click();
			}
		}

		parent.parent.document.getElementById('txtNP1_PROPOSAL').value="";
		
		this.focus();
		
		</script>
	</body>
</HTML>
