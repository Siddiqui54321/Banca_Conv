<%@ Page language="c#" Codebehind="shgn_dh_se_displayheader_ILUS_DH_CHANNELDETAIL.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayheader_ILUS_DH_CHANNELDETAIL" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
    	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<UC:EntityHeading ParamSource="FixValue"  ParamValue=""   id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="Form1" method="post" runat="server">
			<TABLE id="DisplayTable" class="DisplayHeaderTable">
				<table width="100%" cellspacing=0 >
<tr class="TRow_Normal">
<td width="120" class="DHFieldCaption" id=SHTITLE1>&nbsp;<U>Channel Code: </U></td>
<td width="180" class="DHFieldValue" id=SHTEXT1 NOWRAP>&nbsp;<%#CCH_CODE%></td>
<td width="40" class="DHFieldCaption" id=SHTITLE2>&nbsp;<U>Description :</U></td>
<td width="260" class="DHFieldValue" id=SHTEXT2 NOWRAP>&nbsp;<%#CN%></td>
</tr>


			</TABLE>
			<br>
			<asp:Label id="lblServerError" runat="server" CssClass="ServerError" Visible="False">Label</asp:Label>
		</form>
	</body>
</HTML>

