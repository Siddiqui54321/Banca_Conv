<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_UC_USERCOUNTRY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_UC_USERCOUNTRY" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<%@ Register TagPrefix="CUC" TagName="ToolbarLabel" Src="ToolbarLabel.ascx" %>
<%@ Register TagPrefix="CUC" TagName="ToolBarHeader" Src="ToolBarHeader.ascx" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
<CUC:ToolBarHeader runat="server"></CUC:ToolBarHeader>
</head>
<body class=ToolBarBody>
<CUC:ToolbarLabel runat="server"></CUC:ToolbarLabel>

<CV:ButtonContainer runat="server">

<CV:CustomButton id="cmd_Save" Caption="Save" AccessKey="S"  ImageFile="Save" JavaScriptOnclick="parent.frames[lfid.value].send(this,1)" runat="server"></CV:CustomButton>
<CV:CustomButton id="cmd_Delete" Caption="Delete" AccessKey="D"  ImageFile="Delete" JavaScriptOnclick="parent.frames[lfid.value].deleteDetail()" runat="server"></CV:CustomButton>
<CV:CustomButton id="cmd_Menu" Caption="Menu" AccessKey="M"  ImageFile="Menu" JavaScriptOnclick="sendMenu()" runat="server"></CV:CustomButton>
</CV:ButtonContainer>

</body>
</html>

