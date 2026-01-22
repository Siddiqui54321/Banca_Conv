<%@ Control Language="c#" AutoEventWireup="false" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %><%@ Register TagPrefix="cv" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<img src="../shmalib/images/buttons/BottomBar.gif" width="101.6%" height="100%"> 
<script>s_MenuAddress="<%=Session["s_MenuAddress"]%>";</script>
<div style="LEFT: 8px;	POSITION: absolute;	TOP: 10px">
<table width="420" align=center border="0" collapsecellspacing="0" id=Table1 cellPadding=0>
 <tr>
<td	width="40%"	class=optionheading	nowrap>&nbsp;<b>Date:</b><%=Session["s_BAR_DATE"]%></td>
<td	width="30%"	class=optionheading	nowrap>&nbsp;<b>User:</b>&nbsp;&nbsp;&nbsp;<%=Session["s_SUS_NAME"]%></td>
<td	width="40%"	class=optionheading	nowrap>&nbsp;<b>Branch:</b>&nbsp;&nbsp;&nbsp;<%=Session["s_PLC_LOCADESC"]%></td>
  </tr>
</table>
</div>   								 
<input type=hidden id='lfid' name='lfid' value=<%=Request["lfid"]%>>
<input type=hidden id='targetEntity' name='targetEntity' value=<%=Request["targetEntity"]%>>
