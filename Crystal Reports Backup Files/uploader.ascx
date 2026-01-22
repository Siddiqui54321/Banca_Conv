<%@ Control Language="c#" AutoEventWireup="True" Codebehind="uploader.ascx.cs" Inherits="Bancassurance.uploader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td><input id="fileUpload" type="file" Runat="server" NAME="fileUpload">
			<asp:button id="btnSave" runat="server" Text="Upload.." Font-Size="10px" Font-Name="Helvetica" onclick="btnSave_Click"></asp:button></td>
	</tr>
	<tr>
		<td><asp:label id="lblMessage" runat="server" Width="100%" Font-Size="10" Font-Name="verdana" CssClass="ItemStyle text_font"></asp:label></td>
	</tr>
	<tr>
		<td><asp:DataGrid ID="dg" Runat="server" CssClass="form_heading_2" Width="100%" ItemStyle-CssClass="ItemStyle text_font"
				AlternatingItemStyle-CssClass="ItemStyleAlt text_font"></asp:DataGrid></td>
	</tr>
</table>
