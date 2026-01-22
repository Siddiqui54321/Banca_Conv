<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Page language="c#" Codebehind="shgn_dh_se_enquiry_ILUS_ET_EQ_TARGETVALUES.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_dh_se_enquiry_ILUS_ET_EQ_TARGETVALUES" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Register TagPrefix="cc1" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
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
		<script language="JavaScript" src="../shmalib/jscript/js"></script>
		<SCRIPT language="JavaScript" src='../shmalib/jscript/GeneralFunctions.js'></SCRIPT>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
			<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>			
			<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
			_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';
		
			function SetGoalSeekValuesinSession(id, val)
			{
				//SHMA.Enterprise.Presentation.SessionObject.Set(id,val);
			}
		</script>
		
	</HEAD>
	<BODY ms_positioning="GridLayout">
		<UC:EntityHeading ParamSource="FixValue" ParamValue="Target Values" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm3" name="sample" method="post" runat="server">
		<FIELDSET class="ListerFieldSet" runat="server" id="ListerFieldSet" style="WIDTH: 99%; HEIGHT: 100px"><legend><!--List-->
				Target Vlaues</legend>		
			<TABLE id="Table2" cellSpacing="0" cellPadding="2" border="0" style="width:99%">
				<TR class="ListerHeader">
					<TD ></TD>
					<TD ></TD>
					<TD ></TD>
					<TD ></TD>
					<TD ></TD>
					<TD  >Description</TD>
					<TD  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Retirement Income</TD>
					<TD ></TD>
					<TD >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Return %</TD>
					<TD ></TD>
					<TD >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Account Value</TD>
					<TD ></TD>
				</TR>
				<asp:repeater id="lister" runat="server">
					<ItemTemplate>
						<tr class="TRow_Normal" style="cursor:hand" runat="server" ID="EnqRow">
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNP1_PROPOSAL" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNP2_SETNO" Text='<%# DataBinder.Eval(Container, "DataItem.NP2_SETNO")%>' Width=0 BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblPPR_PRODCD" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_TYPE" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_TYPE")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_SUBTYPE" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_SUBTYPE")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblCAD_DESCRIPTION" Text='<%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>' Width='8.0pc' BaseType="Character" >
								</shma:TextBox></td>
							<td id="TDTRI" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT")%>' Width='8.0pc' BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRI" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT")%>&LABLE=Target Retirement Income - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=X')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>
							<td id="TDTRT" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT_LN2" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN2")%>' Width='8.0pc' BaseType="Number" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRT" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN2")%>&LABLE=Target Return Percent - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=Y')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>
							<td id="TDTRV" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT_LN3" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN3")%>' Width='8.0pc' BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRV" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN3")%>&LABLE=Target Account Value - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=Z')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>								
						</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
						<tr class="TRow_Alt" style="cursor:hand" runat="server" ID="EnqRow">
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNP1_PROPOSAL" Text='<%# DataBinder.Eval(Container, "DataItem.NP1_PROPOSAL")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNP2_SETNO" Text='<%# DataBinder.Eval(Container, "DataItem.NP2_SETNO")%>' Width=0 BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblPPR_PRODCD" Text='<%# DataBinder.Eval(Container, "DataItem.PPR_PRODCD")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_TYPE" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_TYPE")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_SUBTYPE" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_SUBTYPE")%>' Width=0 BaseType="Character" >
								</shma:TextBox></td>
							<td>
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblCAD_DESCRIPTION" Text='<%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>' Width='8.0pc' BaseType="Character" >
								</shma:TextBox></td>
							<td id="TDTRI" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT")%>' Width='8.0pc' BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRI" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT")%>&LABLE=Target Retirement Income - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=X')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>
							<td id="TDTRT" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT_LN2" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN2")%>' Width='8.0pc' BaseType="Number" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRT" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN2")%>&LABLE=Target Return Percent - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=Y')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>
							<td id="TDTRV" class="NumericCell">
								<shma:TextBox ReadOnly=True CssClass="LabelLikeText" runat="server" ID="lblNLO_AMOUNT_LN3" Text='<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN3")%>' Width='8.0pc' BaseType="Number" Precision = "0" >
								</shma:TextBox></td>
							<td><INPUT class="BUTTON" id="btnTRV" onclick="openGoalSeek();setFixedValuesInSession('NLO_AMOUNT=<%# DataBinder.Eval(Container, "DataItem.NLO_AMOUNT_LN3")%>&LABLE=Target Account Value - <%# DataBinder.Eval(Container, "DataItem.CAD_DESCRIPTION")%>&TARGETID=Z')" title="Goal Seek" type="button" name="btnGoalSeek" /></td>
						</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:repeater>
			</TABLE>
			</FIELDSET>
			<!--<asp:dropdownlist id="pagerList" runat="server" Width="48px" AutoPostBack="True" CssClass="Pager"></asp:dropdownlist>-->
			<INPUT id="_CustomEvent" type="button" name="_CustomEvent" runat="server" style="WIDTH:0px" onserverclick="_CustomEvent_ServerClick">
			<INPUT id="_CustomEventVal" type="text" name="_CustomEventVal" runat="server" style="WIDTH:0px">&nbsp;
			<INPUT id="_CustomArgName" style="WIDTH: 0px" type="text" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" style="WIDTH: 0px" type="text" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True" ></asp:literal> </script>
		</form>
	</BODY>
</HTML>


<script lang=javasript>

setCurrencyLister("lblNLO_AMOUNT");
setCurrencyLister("lblNLO_AMOUNT_LN3");
setCurrencyLister("lblNLO_AMOUNT_LN3");

/*document.getElementById("lister__ctl0_TDTRI").style.backgroundColor="#B4C78E";
document.getElementById("lister__ctl0_TDTRT").style.backgroundColor="#B4C78E";
document.getElementById("lister__ctl0_TDTRV").style.backgroundColor="#B4C78E";

document.getElementById("lister__ctl1_TDTRI").style.backgroundColor="#C8D8A9";
document.getElementById("lister__ctl1_TDTRT").style.backgroundColor="#C8D8A9";
document.getElementById("lister__ctl1_TDTRV").style.backgroundColor="#C8D8A9";

document.getElementById("lister__ctl2_TDTRI").style.backgroundColor="#9BB170";
document.getElementById("lister__ctl2_TDTRT").style.backgroundColor="#9BB170";
document.getElementById("lister__ctl2_TDTRV").style.backgroundColor="#9BB170";
*/
</script>
