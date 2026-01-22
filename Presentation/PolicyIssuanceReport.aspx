<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="PolicyIssuanceReport.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.PolicyIssuanceReport" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!--Khalid introduced section for user response message -->

<HTML>
	<HEAD>
		<TITLE>Policy Issuance Report</TITLE>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
        	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/JavaScript" src="../shmalib/jscript/Login.js"></script>
		<script type="text/JavaScript" language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
        <script type="text/JavaScript" language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script type="text/JavaScript" language="javascript" src="JSFiles/msrsclient.js"></script>
        <script type="text/JavaScript" language="javascript" src="JSFiles/PortableSQL.js"></script>
        <script type="text/JavaScript" language="javascript" src="JSFiles/NumberFormat.js"></script>
        <script type="text/JavaScript" language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>
        <%Response.Write(ace.Ace_General.loadInnerStyle());%>
		<asp:literal id="HeaderScript" EnableViewState="True" runat="server"></asp:literal>
        <style type="text/css">
            .modal
            {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }
            .loading
            {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }
        </style>
	</HEAD>
	<BODY>
        <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>
	

       
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Policy Issuance Report
				</td>
			</tr>
		</table>
		<form id="myForm1" method="post" name="myForm1" runat="server">
            
                 <DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 118px; HEIGHT: 22px">
                Please wait ... {0}
        </DIV>
			<div style="Z-INDEX: 101" id="NormalEntryTableDiv" runat="server">
				<P><LEGEND style="COLOR: #336692"></LEGEND></P>
				<TABLE id="entryTable" border="0" cellSpacing="5" cellPadding="1" width="100%">
                    <TR id="row_BANK" class="TRow_Normal">
						<TD style="WIDTH: 194px">Users:</TD>
						<TD><SHMA:dropdownlist id="ddl_Users" runat="server" BlankValue="True" DataTextField="USE_NAME" DataValueField="USE_USERID"
								tabIndex="1" Width="248px" CssClass="RequiredField"></SHMA:dropdownlist></TD>
					</TR>
					<TR id="row_Date" class="TRow_Normal">
						<TD style="WIDTH: 194px">Date Type:</TD>
						<TD>
							<asp:DropDownList id="ddlDate" runat="server" tabIndex="1" Width="248px" CssClass="RequiredField"
								style="Z-INDEX: 0">
								<asp:ListItem Value="IssueDate" Selected="True">Issued Date</asp:ListItem>
							</asp:DropDownList>
						</TD>
					</TR>
					<TR id="rowUSE_USERID" class="TRow_Normal">
						<TD style="WIDTH: 194px">From:</TD>
						<TD><SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATEFROM" tabIndex="2" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATEFROM" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATEFROM" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px">To :</TD>
						<TD>
							<SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATETO" tabIndex="5" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATETO" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATETO" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px; HEIGHT: 25px"></TD>
						<TD>
							<a href="#" class="button2" onclick="saveUpdate('btnGenerateExcel');">&nbsp;&nbsp;Generate 
								Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
							<asp:button id="btnGenerateExcel" runat="server" style="visibility:hidden" Width="0px" OnClientClick="DownloadFiles();" Text="Generate MIS Text File" Font-Bold="True" onclick="btnGenerateExcel_Click"></asp:button>
						</TD>
					</TR>
					<TR id="rowUCN_DEFAULT" class="TRow_Alt">
						<TD style="WIDTH: 194px; HEIGHT: 11px">
							<P><asp:label style="Z-INDEX: 0" id="lblServerError" EnableViewState="false" runat="server" Visible="False"
									ForeColor="Red"></asp:label></P>
						</TD>
					</TR>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;
						</td>
						<TD></TD>
						<TD></TD>
					</tr>
					<TR>
						<td style="WIDTH: 194px">
							<P>&nbsp;</P>
						</td>
						<TD>
							<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:imagebutton style="DISPLAY: none" id="btn_Save" runat="server" ImageUrl="Images/savee.JPG" onClientClick="return CheckDates();"></asp:imagebutton></P>
						</TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<INPUT style="WIDTH: 0px;visibility:hidden" id="_CustomArgName" name="_CustomArgName" runat="server">
			<INPUT style="WIDTH: 0px;visibility:hidden" id="_CustomArgVal" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT style="WIDTH: 0px;visibility:hidden" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server"> <INPUT style="WIDTH: 0px;visibility:hidden" id="_CustomEventVal" name="_CustomEventVal" runat="server">&nbsp;
			<table border="0" width="100%">
				<tr>
					<td align="right"><A href="#"></A>&nbsp; <A href="#"></A>&nbsp; <A href="#"></A>
					</td>
				</tr>
			</table>
                
		</form>
                
		<script language="javascript">
		<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>
     	
     	function Download_BancaFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalBanca.xls" );
	    }
	    function Download_UblFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalUbl.xls" );
	    }
	    function Download_ILASFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalIlas.xls" );
	    }
	    function saveUpdate(ButtonId)
        {
            //parent.document.getElementById('message-div').style.display='';
            openWait('Report Generating');
            document.all(ButtonId).click();
            
            }
		</script>
        <script type="text/javascript" language="javascript">
            var ReloadTimeCounter = 2;
            var reloadTime = '<%= Session["PageReloadTime"] %>';
            function DownloadFiles() {
                var DownloadCompleted = executeClass('ace.Ace_General,PopUpFlag');
                if (DownloadCompleted=="True") {
                    DownloadCompleted = executeClass('ace.Ace_General,SetPopUpFlag');
                    closeWait();
                    return;
                }
                else {
                    reloadTime = ReloadTimeCounter * Number(reloadTime);
                }
                window.setTimeout("DownloadFiles()", Number(reloadTime));
            }
            <%--var isReload = false;
            function DownloadFiles() {

                var reloadTime = '<%= Session["PageReloadTime"] %>';
                var DownloadCompleted = '<%= Session["DownloadCompleted"] %>';

                if (isReload == DownloadCompleted) {
                    isReload = false;
                    window.location.reload();
                }
                else {
                    isReload = true;
                }
                // window.setTimeout("DownloadFiles()", 20000);
            }--%>
    </script>
	</BODY>
</HTML>
