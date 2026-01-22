<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_displayselection_ILUS_ET_GE_UC_USERCOUNTRY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_displayselection_ILUS_ET_GE_UC_USERCOUNTRY" %>
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
			var str_URL="";
			var str_SendURL="";
    			var int_ChildFrameNo=1;
    			var str_ChildPT="";

			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>					

			function fssetFilterValues() 
			{
				if (bln_ShouldSubmit==true) {
					str_URL = parent.frames[int_ChildFrameNo].window.document.URL;
					if (str_URL.indexOf('?')>0)
						str_URL += '&';
					else
						str_URL += '?';
					
					str_URL+= str_SendURL;
					parent.frames[int_ChildFrameNo].window.location=str_URL;
				}
			}			

			function fcsetValuesInFrame(str_EntityId,str_ColName,str_ColValue,int_FrameNo) {
				str_URL = parent.frames[int_FrameNo].window.document.URL;
				if (str_URL.indexOf('?')>0)
					str_URL += '&';
				else
					str_URL += '?';
				str_URL+="r_"+str_ColName+"="+str_ColValue;
				parent.frames[int_FrameNo].window.location=str_URL;
			}

		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Country Setup" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="sample" method="post" runat="server">
			&nbsp; <INPUT id="_CustomEventVal" style="WIDTH: 0px" name="_CustomEventVal" runat="server">&nbsp;
			<div id="SelectionDiv" class="SelectionDiv"><fieldset><legend>Detail</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" width="60%" border="0">
						<TR id='row1'>
							<TD>User ID</TD>
							<TD nowrap><SHMA:dropdownlist id="ddlUSE_USERID" runat="server" BlankValue="True" DataTextField="desc_f" DataValueField=""
									tabIndex="2" Width="8.0pc"></SHMA:dropdownlist>
								<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="ddlUSE_USERID" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
					</TABLE>
					<br>
					<asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" style="WIDTH: 0px" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" style="WIDTH: 0px" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server"> <INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<script language="javascript">

			</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall();</script>
	</body>
</HTML>
