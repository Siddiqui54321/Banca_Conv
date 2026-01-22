<%@ Page Language="c#" CodeBehind="shgn_ss_se_stdscreen_ILUS_ST_CHANNELSUBDTL.aspx.cs"
    AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ST_CHANNELSUBDTL" %>

<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls"
    Assembly="Enterprise" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <%Response.Write(ace.Ace_General.LoadPageStyle());%>
    <script language="javascript" src="JSFiles/PortableSQL.js"></script>
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/NumberFormat.js"></script>
    <script language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></script>
    <script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
		_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
		function RefreshFields()
		{				
			if(myForm.txtCCS_CODE.disabled==true)
				 myForm.txtCCS_CODE.disabled=false;
				 myForm.txtCCS_CODE.value ="";
							 myForm.txtCCS_DESCR.value ="";
			
myForm.txtCCS_CODE.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

    </script>
</head>
<body>
    <table>
        <tr class="form_heading">
            <td height="20" colspan="6">
                &nbsp; Channel Sub Detail Setup
            </td>
        </tr>
    </table>
    <form id="myForm" name="myForm" method="post" runat="server">
    <div style="margin-top: 10px; margin-left: 320px" id="EntryTableDiv" runat="server">
        <table>
            <tr class="form_heading">
                <td height="20" colspan="6">
                    &nbsp; Entry 
                </td>
            </tr>
        </table>
        <table id="entryTable" cellspacing="5" cellpadding="1" border="0">
            <tr id='rowCCS_CODE' class="TRow_Normal">
                <td>
                    Code
                </td>
                <td>
                    <SHMA:TextBox ID="txtCCS_CODE" TabIndex="1" runat="server" Width='4.0pc' MaxLength="5"
                        CssClass="RequiredField" BaseType="Character"></SHMA:TextBox>
                    <asp:CompareValidator ID="cfvCCS_CODE" runat="server" ControlToValidate="txtCCS_CODE"
                        Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                        EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="rfvCCS_CODE" runat="server" ErrorMessage="Required"
                        ControlToValidate="txtCCS_CODE" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id='rowCCS_DESCR' class="TRow_Alt">
                <td>
                    Description
                </td>
                <td>
                    <SHMA:TextBox ID="txtCCS_DESCR" TabIndex="2" runat="server" Width='8.0pc' MaxLength="100"
                        CssClass="RequiredField" BaseType="Character"></SHMA:TextBox>
                    <asp:CompareValidator ID="cfvCCS_DESCR" runat="server" ControlToValidate="txtCCS_DESCR"
                        Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                        EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="rfvCCS_DESCR" runat="server" ErrorMessage="Required"
                        ControlToValidate="txtCCS_DESCR" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <SHMA:TextBox ID="txtCCH_CODE" Style="width: 0" runat="server" BaseType="Character"></SHMA:TextBox>
                    <asp:CompareValidator ID="cfvCCH_CODE" runat="server" ControlToValidate="txtCCH_CODE"
                        Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                        EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    <SHMA:TextBox ID="txtCCD_CODE" Style="width: 0" runat="server" BaseType="Character"></SHMA:TextBox>
                    <asp:CompareValidator ID="cfvCCD_CODE" runat="server" ControlToValidate="txtCCD_CODE"
                        Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                        EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <asp:Label ID="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:Label></p>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="ListerDiv" class="ListerDiv1" runat="server">
        <fieldset class="ListerFieldSet" runat="server" id="ListerFieldSet">
            <legend class="LegendStyle">List</legend>
            <table class="Lister" cellspacing="2" cellpadding="0" border="0">
                <tr class="ListerHeader">
                    <td width="" onclick="filterLister('CCS_CODE','Code')">
                        Code
                    </td>
                    <td width="" onclick="filterLister('CCS_DESCR','Description')">
                        Description
                    </td>
                    <td width="0">
                    </td>
                    <td width="0">
                    </td>
                </tr>
                <asp:Repeater ID="lister" runat="server">
                    <ItemTemplate>
                        <tr class="ListerItem" id="ListerRow" runat="server">
                            <td>
                                <asp:LinkButton ID="linkCCS_CODE1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCS_CODE")%>'
                                    CausesValidation="false"> </asp:LinkButton>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container, "DataItem.CCS_DESCR")%>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblCCH_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblCCD_CODE1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="ListerAlterItem" id="ListerRow" runat="server">
                            <td>
                                <asp:LinkButton ID="linkCCS_CODE2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.CCS_CODE")%>'
                                    CausesValidation="false"> </asp:LinkButton>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container, "DataItem.CCS_DESCR")%>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblCCH_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCH_CODE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblCCD_CODE2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CCD_CODE")%>'></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </fieldset>
        Page no:
        <asp:DropDownList ID="pagerList" runat="server" AutoPostBack="True" CssClass="RequiredField"
            OnSelectedIndexChanged="pagerList_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <input id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
    <input id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
    <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
    <input id="_CustomEvent" style="width: 0px" type="button" value="Button" name="_CustomEvent"
        runat="server" onserverclick="_CustomEvent_ServerClick">
    <input id="_CustomEvent1" style="width: 0px" type="button" value="Button" name="_CustomEvent"
        runat="server" onserverclick="_CustomEvent_ServerClick" causesvalidation="false">
    <input type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
    <input type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
    <input type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
    <script language="javascript">
				
    </script>
    </form>
    <script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
    <table style="position: absolute; width: 425px; top: 288px; left: 320px" border="0"
        cellpadding="0" width="100%">
        <tr>
            <td align="right">
                <tr>
                    <td align="right">
                        <a class="button2" onclick="addClicked()" href="#">Add New</a> <a class="button2"
                            onclick="saveClicked()" href="#">Save</a> <a class="button2" onclick="updateClicked()"
                                href="#">Update</a> <a class="button2" onclick="deleteClicked()" href="#">Delete</a>
                        <td align="right">
                        </td>
                </tr>
    </table>
</body>
</html>
