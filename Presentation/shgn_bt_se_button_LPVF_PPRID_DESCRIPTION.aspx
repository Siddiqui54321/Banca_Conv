<%@ Page language="c#" Codebehind="shgn_bt_se_button_LPVF_PPRID_DESCRIPTION.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_LPVF_PPRID_DESCRIPTION" %>
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

<CV:AddButton  runat="server"></CV:AddButton>
<CV:SaveButton runat="server"></CV:SaveButton>
<CV:UpdateButton runat="server"></CV:UpdateButton>
<CV:DeleteButton runat="server"></CV:DeleteButton >
</CV:ButtonContainer>

</body>
</html>

