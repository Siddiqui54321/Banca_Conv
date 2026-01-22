<%@ Page Language="c#" CodeBehind="shgn_gs_se_stdgridscreen_ILUS_ET_UM_USERMANAGMENT.aspx.cs"
    AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_gs_se_stdgridscreen_ILUS_ET_UM_USERMANAGMENT" %>

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
    <link href="Styles/Style.css" type="text/css" rel="stylesheet">
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
			if(myForm.txtUSE_USERID.disabled==true)
				 myForm.txtUSE_USERID.disabled=false;
				 myForm.txtUSE_USERID.value ="";
							 myForm.txtUSE_NAME.value ="";
							 myForm.txtUSE_PASSWORD.value ="";
							 myForm.txtCCH_CODEDEFAULT.value ="";
							 myForm.txtUSE_TYPE.value ="";
myForm.txtUSE_USERID.focus();
			setDefaultValues();
		}
		/********** dependent combo's queries **********/
		

    </script>
</head>
<body>
    <UC:EntityHeading ParamSource="FixValue" ParamValue="User Detail" ID="EntityHeading"
        runat="server"></UC:EntityHeading>
    <form id="myForm" name="myForm" method="post" runat="server">
    <asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up12">
    <ContentTemplate>
 
    <input class="btnHideLister" id="btnHideLister" onclick="HideLister()" type="button"
        value="Hide" name="btnHideLister" runat="server">
    <div class="GridDivWithLister" id="EntryTableDiv" runat="server">
        <fieldset>
            <legend>Entry</legend>
            <table id="entryTable" cellspacing="5" cellpadding="1" border="0">
                <tr id='row1'>
                    <td>
                        User ID
                    </td>
                    <td nowrap>
                        <SHMA:TextBox ID="txtUSE_USERID" TabIndex="2" runat="server" Width='5.0pc' MaxLength="10"
                            CssClass="RequiredField" BaseType="Character"></SHMA:TextBox>
                        <asp:CompareValidator ID="cfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID"
                            Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                            EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="rfvUSE_USERID" runat="server" ErrorMessage="Required"
                            ControlToValidate="txtUSE_USERID" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        User Name
                    </td>
                    <td nowrap>
                        <SHMA:TextBox ID="txtUSE_NAME" TabIndex="3" runat="server" Width='13.0pc' MaxLength="25"
                            CssClass="RequiredField" BaseType="Character"></SHMA:TextBox>
                        <asp:CompareValidator ID="cfvUSE_NAME" runat="server" ControlToValidate="txtUSE_NAME"
                            Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                            EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="rfvUSE_NAME" runat="server" ErrorMessage="Required"
                            ControlToValidate="txtUSE_NAME" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Password
                    </td>
                    <td nowrap>
                        <SHMA:TextBox ID="txtUSE_PASSWORD" TabIndex="4" runat="server" Width='10.0pc' MaxLength="20"
                            BaseType="Character"></SHMA:TextBox>
                        <asp:CompareValidator ID="cfvUSE_PASSWORD" runat="server" ControlToValidate="txtUSE_PASSWORD"
                            Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                            EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id='row4'>
                    <td>
                        Dealult Channel
                    </td>
                    <td nowrap>
                        <SHMA:TextBox ID="txtCCH_CODEDEFAULT" TabIndex="5" runat="server" Width='10.0pc'
                            MaxLength="20" BaseType="Character"></SHMA:TextBox>
                        <asp:CompareValidator ID="cfvCCH_CODEDEFAULT" runat="server" ControlToValidate="txtCCH_CODEDEFAULT"
                            Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                            EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td>
                        Type
                    </td>
                    <td nowrap>
                        <SHMA:TextBox ID="txtUSE_TYPE" TabIndex="6" runat="server" Width='3.0pc' MaxLength="5"
                            BaseType="Character"></SHMA:TextBox>
                        <asp:CompareValidator ID="cfvUSE_TYPE" runat="server" ControlToValidate="txtUSE_TYPE"
                            Operator="DataTypeCheck" Type="String" ErrorMessage="String Format is Incorrect "
                            EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <tr>
                        <td>
                            <p>
                                <asp:Label ID="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:Label></p>
                        </td>
                        <td>
                        </td>
                    </tr>
            </table>
        </fieldset>
    </div>
            <div class="ListerDiv" id="ListerDiv" runat="server">
                <fieldset class="ListerFieldSet" runat="server" id="ListerFieldSet">
                    <legend>List</legend>
                    <table class="Lister" cellspacing="2" cellpadding="0" border="0">
                        <tr class="ListerHeader">
                            <td width="10%" onclick="filterLister('USE_USERID','User ID')">
                                User ID
                            </td>
                            <td width="20%" onclick="filterLister('USE_NAME','User Name')">
                                User Name
                            </td>
                        </tr>
                        <asp:Repeater ID="lister" runat="server">
                            <ItemTemplate>
                                <tr class="ListerItem" id="ListerRow" runat="server">
                                    <td>
                                        <asp:LinkButton ID="linkUSE_USERID1" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID")%>'
                                            CausesValidation="false">
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container, "DataItem.USE_NAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="ListerAlterItem" id="ListerRow" runat="server">
                                    <td>
                                        <asp:LinkButton ID="linkUSE_USERID2" runat="server" CommandName="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.USE_USERID")%>'
                                            CausesValidation="false">
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container, "DataItem.USE_NAME")%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </fieldset>
                Page no:
                <asp:DropDownList ID="pagerList" runat="server" AutoPostBack="True" CssClass="Pager"
                    OnSelectedIndexChanged="pagerList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        
    <input id="_CustomArgName" type="hidden" name="_CustomArgName" runat="server">
    <input id="_CustomArgVal" type="hidden" name="_CustomArgVal" runat="server">
    <input id="_CustomEventVal" type="hidden" name="_CustomEventVal" runat="server">
    <input id="_CustomEvent" style="width: 0px" type="button" value="Button" name="_CustomEvent"
        runat="server" onserverclick="_CustomEvent_ServerClick">
    <input type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
    <input type="hidden" name="FIELD_COMBINATION" id="FIELD_COMBINATION" runat="server">
    <input type="hidden" name="VALUE_COMBINATION" id="VALUE_COMBINATION" runat="server">
       
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    <script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>		if (_lastEvent == 'New')	addClicked(); 	fcStandardFooterFunctionsCall();</script>
</body>
</html>
