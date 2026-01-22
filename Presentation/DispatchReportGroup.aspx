<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchReportGroup.aspx.cs" Inherits="Bancassurance.Presentation.DispatchReportGroup" %>


<script language="javascript" src="../shmalib/jscript/MI_UI_Messaging.js"></script>

<DIV class="divWaiting" id="divProcessing" style="LEFT: 10px; VISIBILITY: hidden; WIDTH: 97%; VISIBILITY: hidden; POSITION: absolute; TOP: 100px; HEIGHT: 20px">
        Please wait ... {0}

    </DIV>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <div>
             <iframe ALIGN="top" id="ManualPolicy" FRAMEBORDER="0" MARGINWIDTH="0" width="100%" height="400" name="ManualPolicy" src="../Presentation/frmDispatchReport.aspx" style="mso-linked-frame:auto"  scrolling="yes">


        </div>
    </form>
</body>
</html>
