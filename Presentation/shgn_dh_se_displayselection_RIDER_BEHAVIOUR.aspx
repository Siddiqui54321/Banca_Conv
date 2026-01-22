<%@ Register TagPrefix="UC" TagName="DispSelButton" Src="DispSelButton.ascx" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="UC" TagName="DispSelHeader" Src="DispSelHeader.ascx" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_RIDER_BEHAVIOUR.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_RIDER_BEHAVIOUR" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
  <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
		<UC:DispSelHeader id=dispSelHeader runat="server"></UC:DispSelHeader> 
		<%Response.Write(ace.Ace_General.LoadPageStyle());%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>		
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		
		<CV:PageClientScript id="pageClientScript" runat="server"></CV:PageClientScript>
  </HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue"  ParamValue="Rider Behaviour"   id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<TABLE class="DispSelTable" id="entryTable">
			<TR></TR><TR></TR>
				<TR id='row1' ><TD &amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;>Rider Behaviour  </TD><TD nowrap ><CV:dropdownlist id="ddlVFS_CODE" runat="server" BlankValue="False" DataTextField="desc_f"  DataValueField="VFS_CODE"  tabIndex="2" Width="15.0pc" CssClass="RequiredField"   onchange=""
  ></CV:dropdownlist>

<CV:CompareValidator id="cfvVFS_CODE" runat="server"  ControlToValidate="ddlVFS_CODE" Operator="DataTypeCheck"  Type="String" ErrorMessage="String Format is Incorrect "  EnableClientScript="False"  Display="Dynamic"></CV:CompareValidator>

<CV:requiredfieldvalidator id="rfvVFS_CODE"  runat="server"  ErrorMessage="Required" ControlToValidate="ddlVFS_CODE"  Display="Dynamic"></CV:requiredfieldvalidator>
<a href="#" class="button2" onclick="setFixedValuesInSession('VFS_CODE='+getField('VFS_CODE').value );SearchInChild();">&nbsp;&nbsp;Go&nbsp;&nbsp;</a>
</TD>
				<!-- <UC:DispSelButton id=dispSelButton runat="server"></UC:DispSelButton> --></TR>
			</TABLE>
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
		</form>
	</body>
</HTML>
