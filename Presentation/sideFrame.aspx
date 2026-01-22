<%@ Page language="c#" Codebehind="sideFrame.aspx.cs" AutoEventWireup="True"  Inherits="SHAB.Presentation.sideFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<title>sideFrame</title>
		<LINK href="Styles/StyleSideFrame.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			var currentButton="btnProposalEntry";
			var imgArray   =[
                    ["btnProposalEntry"          ,  "Proposal-Policy-Button.gif"	, "ProposalEntryMainFrame"],
                    ["btnMedicalQuestionnaire"   ,  "Medical-Questionnaire.gif"		, "shgn_gp_gp_MI_ET_GP_MEDQUESTIONAIRE"],
                    ["btnDocumentRequirement"    ,  "Document-Requirement.gif"		, "shgn_gp_gp_MI_ET_GP_UNDERWRITINGREQ"],
                    ["btnSubLimits"				 ,  "Sub-Limits-Selection.gif"		, "shgn_gp_gp_MI_ET_GP_SUBLIMITMAIN"],
                    ["btnProposalFollowUp"       ,  "Proposal-Follow-Up.gif"		, "shgn_gp_gp_MI_ET_GP_FOLLOWUP"],
                    ["btnPaymentInstallments"    ,  "Payment-Installment.gif"		, "shgn_gp_gp_MI_ET_GP_PAYMENTDETAILSMAIN"]
                    ];
			function setPage(entityId)
			{
				var url = new String ( document.location );
				url = url.substring ( 0, url.indexOf('/', url.indexOf('/')+2 )+1 );
				parent.window.location = url+"Insurance/Presentation/"+entityId+".aspx";
			}
			
			function setSource(obj_ref, eventType)
			{
				if (obj_ref.name == currentButton)
					return;

				var imageUrl ="";
				var imgSrc = "";
				for (a=0; a< imgArray.length; a++)
				{
					if (imgArray[a][0] == obj_ref.name)
						imageUrl = imgArray[a][1];
						
					if (eventType == 'SELECT' )
						imgSrc = "Images/W_"+imageUrl;
				}
								
				if (eventType == 'OVER' || eventType == 'SELECT' )
					imgSrc = "Images/W_"+imageUrl;
				else
					imgSrc = "Images/"+imageUrl;
				
				obj_ref.src = imgSrc;
			}
		</script>
	</HEAD>
	<body background="Images/Side-Bar.gif" bgcolor="#d4deea" MS_POSITIONING="GridLayout" oncontextmenu="return <%=System.Configuration.ConfigurationSettings.AppSettings["RightMouseKey"]%>;">
		<form id="myForm" method="post" runat="server">
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 60px; HEIGHT: 20px">
				<img name="btnProposalEntry" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src="Images/Proposal-Policy-Button.gif" onclick="setPage('ProposalEntryMainFrame');setSource(this,'SELECT');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 90px; HEIGHT: 20px">
				<img name="btnMedicalQuestionnaire" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src = "Images/Medical-Questionnaire.gif" onclick="setPage('shgn_gp_gp_MI_ET_GP_MEDQUESTIONAIRE');setSource(this,'SELECT');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 120px; HEIGHT: 20px">
				<img name="btnDocumentRequirement" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src="Images/Document-Requirement.gif" onclick="setPage('shgn_gp_gp_MI_ET_GP_UNDERWRITINGREQ');setSource(this,'SELECT');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 150px; HEIGHT: 20px">
				<img name="btnSubLimits" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src="Images/Sub-Limits-Selection.gif" onclick="setPage('shgn_gp_gp_MI_ET_GP_SUBLIMITMAIN');setSource(this,'SELECT');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 180px; HEIGHT: 20px">
				<img name="btnProposalFollowUp" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src="Images/Proposal-Follow-Up.gif" onclick="setPage('shgn_gp_gp_MI_ET_GP_FOLLOWUP');setSource(this,'SELECT');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 210px; HEIGHT: 20px">
				<img name="btnPaymentInstallments" onmouseover="setSource(this,'OVER');" onmouseout="setSource(this,'OUT');" src="Images/Payment-Installment.gif" onclick="setPage('shgn_gp_gp_MI_ET_GP_PAYMENTDETAILSMAIN');setSource(this,'SELECT');" />
			</DIV>
			<!--
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 60px; HEIGHT: 20px">
				<img name="btnProposalEntry" onmouseover="setSource(this,'W_Proposal-Policy-Button.gif');" onmouseout="setSource(this,'Proposal-Policy-Button.gif');" src="Images/Proposal-Policy-Button.gif" onclick="parent.window.location=setPage('ProposalEntryMainFrame');setSource(this,'Proposal-Policy-Button.gif');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 90px; HEIGHT: 20px">
				<img name="btnMedicalQuestionnaire" onmouseover="setSource(this,'W_Medical-Questionnaire.gif');" onmouseout="setSource(this,'Medical-Questionnaire.gif');" src="Images/Medical-Questionnaire.gif" onclick="parent.window.location=setPage('shgn_gp_gp_MI_ET_GP_MEDQUESTIONAIRE');setSource(this,'W_Medical-Questionnaire.gif');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 120px; HEIGHT: 20px">
				<img name="btnDocumentRequirement" onmouseover="setSource(this,'W_Document-Requirement.gif');" onmouseout="setSource(this,'Document-Requirement.gif');" src="Images/Document-Requirement.gif" onclick="parent.window.location=setPage('shgn_gp_gp_MI_ET_GP_UNDERWRITINGREQ');setSource(this,'W_Document-Requirement.gif');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 150px; HEIGHT: 20px">
				<img name="btnSubLimits" onmouseover="setSource(this,'W_Sub-Limits-Selection.gif');" onmouseout="setSource(this,'Sub-Limits-Selection.gif');" src="Images/Sub-Limits-Selection.gif" onclick="/*parent.window.location=setPage('shgn_gp_gp_MI_ET_GP_FOLLOWUP');*/setSource(this,'W_Sub-Limits-Selection.gif');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 180px; HEIGHT: 20px">
				<img name="btnProposalFollowUp" onmouseover="setSource(this,'W_Proposal-Follow-Up.gif');" onmouseout="setSource(this,'Proposal-Follow-Up.gif');" src="Images/Proposal-Follow-Up.gif" onclick="parent.window.location=setPage('shgn_gp_gp_MI_ET_GP_FOLLOWUP');setSource(this,'W_Proposal-Follow-Up.gif');" />
			</DIV>
			<DIV style="LEFT: 16px; WIDTH: 152px; POSITION: absolute; TOP: 210px; HEIGHT: 20px">
				<img name="btnPaymentInstallments" onmouseover="setSource(this,'W_Payment-Installment.gif');" onmouseout="setSource(this,'Payment-Installment.gif');" src="Images/Payment-Installment.gif" onclick="/*parent.window.location=setPage('shgn_gp_gp_MI_ET_GP_FOLLOWUP');*/setSource(this,'W_Payment-Installment.gif');" />
			</DIV>
			-->
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 12px; WIDTH: 152px; POSITION: absolute; TOP: 300px; HEIGHT: 128px"
				cellSpacing="1" cellPadding="1" width="152" align="center" border="0">
				<!-- Side Bar Buttons 
				<TR align="center">
					<TD style="HEIGHT: 38px">
						<img name="btnProposalEntry" src="Images/Proposal-Policy-Button.gif" onclick="alert('Open Proposal Entry');" />
					</TD>
				</TR>
				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>
				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>
				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>
				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>
				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>

				<TR align="center">
					<TD style="HEIGHT: 38px">
						&nbsp;
					</TD>
				</TR>
				Buttons End Here -->
								
				<TR align="center">
					<TD style="HEIGHT: 38px">
						<P>Proposal#
							<asp:label id="lblProposalNb" runat="server" Width="130px" Font-Size="XX-Small">2007/0010</asp:label></P>
					</TD>
				</TR>
				<TR align="center">
					<TD>CreationDate
						<asp:label id="lblCreationDate" runat="server" Width="130px" Font-Size="XX-Small">08/06/2007</asp:label></TD>
				</TR>
				<TR align="center">
					<TD><asp:textbox id="txtProposalStatus" runat="server" Width="140px" ReadOnly="True" Font-Size="XX-Small"
							BorderStyle="None">&lt;Proposal/Policy Status&gt;</asp:textbox></TD>
				</TR>
				<TR align="center">
					<TD><asp:textbox id="txtProposalValidity" runat="server" Width="140px" ReadOnly="True" Font-Size="XX-Small"
							BorderStyle="None">&lt;Proposal validity date&gt;</asp:textbox></TD>
				</TR>
			</TABLE>
		</form>
	</body>
	<script language="javascript">
		for (a=0; a< imgArray.length; a++)
		{
			if ( new String(parent.window.location).indexOf(imgArray[a][2]) > 0 )
			{
				currentButton = imgArray[a][0];
				var obj = document.getElementById(currentButton);
				obj.src = "Images/W_"+imgArray[a][1];
			}
		}
	</script>
</HTML>
