<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFOpeningGroup.aspx.cs" Inherits="Bancassurance.Presentation.PDFOpeningGroup" %>
<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>


<head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
</head>

<html>
	<%--<iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="ManualPolicy" src="../Presentation/FormPDF.aspx?pid=0&amp;<%=ClientParams%>" style="mso-linked-frame:auto"  scrolling="yes">--%>
	
        <iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="ManualPolicy" src="../Presentation/FormPDF.aspx" style="mso-linked-frame:auto"  scrolling="yes">


        
        <!--
	<iframe ALIGN="top" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="50" name="ManualPolicyIssuanceOfPendingProposalsView_Button.aspx" src="../Presentation/ManualPolicyIssuanceSearch_Button.aspx; style="mso-linked-frame:auto"  scrolling="No">
	-->
</html>