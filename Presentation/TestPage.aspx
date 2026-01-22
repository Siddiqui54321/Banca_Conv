<%@ Page language="c#" Codebehind="TestPage.aspx.cs" AutoEventWireup="True" Inherits="Insurance.Presentation.TestPage" %>
<%@ Register TagPrefix="DNJValidators" Namespace="ACEValidators" Assembly="ACEValidators" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<BODY>
		<FORM RUNAT="SERVER" ID="Form1">
			<TABLE>
				<TR>
					<TD>
						<DNJValidators:LengthValidator ID="valMessage" ControlToValidate="txtText" RUNAT="SERVER" LowerLength="10" UpperLength="15">** </DNJValidators:LengthValidator>
					</TD>
				</TR>
				<TR>
					<TD>
						<ASP:TextBox ID="txtText" RUNAT="SERVER" />
					</TD>
				</TR>
				<TR>
					<TD>
						<ASP:Button RUNAT="SERVER" TEXT="Validate" ID="Button1" NAME="Button1" />
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<P>&nbsp;</P>
		<P>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
				<TR>
					<TD width=200>ddd</TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD>ddd</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD>ddd</TD>
				</TR>
			</TABLE>
		</P>
	</BODY>
</HTML>
