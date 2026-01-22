<%@ Page language="c#" Codebehind="shgn_gs_se_stdgridscreen_GENERATEFILE.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gs_se_stdgridscreen_GENERATEFILE" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<script src="../shmalib/jscript/Illustration.js" type="text/JavaScript"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
			_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
			
		/********** dependent combo's queries **********/
		

		</script>
		
		
	</HEAD>
	<body >
		<!-- <UC:EntityHeading ParamSource="FixValue"  ParamValue="Generate File"   id="EntityHeading" runat="server"></UC:EntityHeading>-->
		<table><tr class="form_heading"><td height="20" colSpan="6">Generate File</td></tr></table>
		
		<form id="myForm" name="myForm" method="post" runat="server">
			<div id="NormalEntryTableDiv" style="Z-INDEX: 101" runat="server">
				
					<TABLE id="entryTable" cellSpacing="0" cellPadding="0" border="0">
						<TR id='row1' class="TRow_Normal"><TD width=20%>Issue Date From</TD><TD nowrap><SHMA:DatePopUp  id="txtDATEFROM" runat="server"  tabIndex="2"  maxlength="10" Width='5.0pc'  ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"  ></SHMA:DatePopUp>

<asp:CompareValidator id="cfvDATEFROM" runat="server"  ControlToValidate="txtDATEFROM" Operator="DataTypeCheck"  Type="Date" ErrorMessage="Date Format is Incorrect "  Display="Dynamic"></asp:CompareValidator>
</TD>
</TR><TR id='row4' class="TRow_Alt"><TD width=20%>Issue Date To</TD><TD nowrap><SHMA:DatePopUp  id="txtDATETO" runat="server"  tabIndex="5"  maxlength="10" Width='5.0pc'  ExternalResourcePath="jsfiles/DatePopUp.js" ImageUrl="Images/image1.jpg"  ></SHMA:DatePopUp>

<asp:CompareValidator id="cfvDATETO" runat="server"  ControlToValidate="txtDATETO" Operator="DataTypeCheck"  Type="Date" ErrorMessage="Date Format is Incorrect "  Display="Dynamic"></asp:CompareValidator>
</TD>
					<TR><td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P></TD><TD></TD></TR>
					</TABLE>
				

			</div>
			
			<INPUT id="_CustomArgName" style="WIDTH: 0px;visibility:hidden" type="text" name="_CustomArgName" runat="server" >
			<INPUT id="_CustomArgVal" style="WIDTH: 0px;visibility:hidden" type="text" name="_CustomArgVal" runat="server" >&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT id="_CustomEvent" style="WIDTH: 0px;visibility:hidden" type="button" value="Button" name="_CustomEvent" runat="server" onserverclick="_CustomEvent_ServerClick"> 
			<INPUT id="_CustomEventVal" style="WIDTH: 0px;visibility:hidden" name="_CustomEventVal" runat="server">
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		<script language="javascript">
		function generateFile(){
		var date=new Date();
		while ((new Date()).getTime()-date.getTime()< 1000);
		executeProcess('ace.GenerateFile');
		}
		</script>
		
			<table style="POSITION: absolute; TOP: 80px; LEFT: 10px" border="0" cellPadding="0" width="100%">
			<tr style="background-color:#F4F8FB;">
				<td width=33% > </td>
				<td align="LEFT">
					<A class="button2" onclick="executeReport('PROP_SUMMARY'); generateFile();" href="#">Process</A> 
				<td align="right"></td>
			</tr>
		</table>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall(); 
		</script>
	</body>
</HTML>

