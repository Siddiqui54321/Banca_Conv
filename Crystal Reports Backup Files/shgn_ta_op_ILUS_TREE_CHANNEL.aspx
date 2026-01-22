<%@ Page CodeBehind="shgn_ta_op_ILUS_TREE_CHANNEL.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ta_op_ILUS_TREE_CHANNEL" %>
<html>
<%Response.Write(ace.Ace_General.LoadPageStyle());%>

<body bottomMargin=0px topMargin=0px rightMargin=0px leftMargin=12px>
<table cellspacing=0 cellpadding=0 id="tabTable">
<tr>
<td>
  <DIV id=TabL0 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_ST_CHANNEL.aspx?pt=ILUS_ST_CHANNEL&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Channel
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD0 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_ST_CHANNEL.aspx?pt=ILUS_ST_CHANNEL&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Channel
      </iSDS>
    </DIV>
  </DIV>
</td>
<td>
  <DIV id=TabL1 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_CHANNELDETAIL.aspx?pt=ILUS_GP_CHANNELDETAIL&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Detail
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD1 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_CHANNELDETAIL.aspx?pt=ILUS_GP_CHANNELDETAIL&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Detail
      </iSDS>
    </DIV>
  </DIV>
</td>

<td>
  <DIV id=TabL2 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_CHANNELSUBDTL.aspx?pt=ILUS_GP_CHANNELSUBDTL&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Sub Detail
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD2 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_CHANNELSUBDTL.aspx?pt=ILUS_GP_CHANNELSUBDTL&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Sub Detail
      </iSDS>
    </DIV>
  </DIV>
</td>

<td>
  <DIV id=TabL3 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_BRANCHPRODUCT.aspx?pt=ILUS_GP_BRANCHPRODUCT&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Product
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD3 onclick="setTab('../Presentation/shgn_gp_gp_ILUS_GP_BRANCHPRODUCT.aspx?pt=ILUS_GP_BRANCHPRODUCT&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Product
      </iSDS>
    </DIV>
  </DIV>
</td>

</tr>
</table>
<Script language="JavaScript" src="..\shmalib\jscript\SHGN_GeneralFuncsTB.js"></script>  
<Script Language=JavaScript>
	var totalTab=4;
	var currTab=document.getElementById("TabL0");
	var prevTab=document.getElementById("TabL0");

	var barType="TAB";

	function setTab(pos,tabType,objRef) 
	{
		if (currTab.id!="TabL0" && currTab!=null && currTab.id.substring(4)==objRef.id.substring(4))
			return;
		if (prevTab!=currTab) {
			prevTab=currTab;
			currTab=objRef;
		}
		else
			currTab=objRef;
		for (i=0;i<totalTab;i++) {
			document.getElementById("TabD"+i).style.visibility="hidden";
			document.getElementById("TabL"+i).style.visibility="visible";
		}	
		var tabId="TabD";
		if (tabType=="L")
			tabId="TabL";
		tabId+=objRef.id.substring(4);
		objRef.style.visibility="hidden";
		document.getElementById(tabId).style.visibility="visible";
		send(pos);
	}
	var int_FrameId=

<%=(System.Object) Request["pid"] == null?"1":Request["pid"]%>;
	function send(pos)
	{
	        var a= parent.frames[int_FrameId];
		/*while (a==null) {
			int_FrameId--;
			a= parent.frames[int_FrameId];
		}*/
		a.location=pos;
	}
	setTab("../Presentation/shgn_gp_gp_ILUS_ST_CHANNEL.aspx?pt=ILUS_ST_CHANNEL&<%=ClientParams%>",'D',document.getElementById("TabL0"));
</Script>
</body>
</html>

