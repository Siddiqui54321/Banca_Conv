<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchDataGroup.aspx.cs" Inherits="Bancassurance.Presentation.DispatchDataGroup" %>

<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}
</DIV>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">

     

</head>
<body>
    <form id="form1" runat="server">
        <div>
             <iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="ManualPolicy" src="../Presentation/FormDispatch.aspx" style="mso-linked-frame:auto"  scrolling="yes">


        </div>
    </form>
</body>
</html>
