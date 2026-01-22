<%@ Page language="c#" Codebehind="jsController.aspx.cs" AutoEventWireup="True" Inherits="Aceins.Presentation.jsController" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
    <title>jsController</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="JSFiles/msrsclient.js"></script>
    <script language="javascript" src="JSFiles/JScriptFG.js"></script>
    <SCRIPT language="JavaScript" src='../shmalib/jscript/Personal.js'></SCRIPT>
    <SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
    
    <script language=javascript >
    
    function addClicked()
    {
		addPersonalDetails();
		
    }

    function updateClicked()
    {
		updatePersonalDetails();
    }
    function saveClicked()
    {
		savePersonalDetails();
		//parent.frames[1].saveClicked();
    }

    function deleteClicked()
    {
		deletePersonalDetails();
		//parent.frames[1].deleteClicked();
    }

    </script>

  
  </head>
  <body MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server">

     </form>
	
  </body>
</html>
