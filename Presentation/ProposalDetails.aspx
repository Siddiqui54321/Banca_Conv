<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProposalDetails.aspx.cs" Inherits="Bancassurance.Presentation.ProposalDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="/Presentation/Styles/MainPage.css">
		<link rel="stylesheet" type="text/css" href="Styles/comments.css">				
     <style type="text/css">
     .backstyle{
            background-image: url('images/bg_btn.gif');
           background-repeat:repeat-x;font-weight: bold;
           font-size: 11px;
           font-family: Verdana, Arial, Helvetica, sans-serif;
           padding: 4px 12px;text-align: center;
           min-width: 75px;
           

       }

      .grid_font{
	font-family:Verdana, Arial, Helvetica, sans-serif;
	color:#666666;
	font-size:10px;
}

      .details-view {
    border: 1px solid #b3d9ff;
    border-radius: 8px;
    background-color: #e6f2ff; 
    font-family: Arial, sans-serif;
    width: 100%;
    max-width: 500px;
}

.details-view th {
    background-color: #99ccff; /* header light blue */
    color: #000;
    padding: 8px 12px;
    text-align: left;
    font-size:small;
}

.details-view td {
    background-color: #cce6ff; /* alternating light blue */
    padding: 8px 12px;
     font-size:small;
}

.details-view tr:nth-child(even) td {
    background-color: #e6f2ff; /* slightly different blue for stripes */
}

.divComments
{
	margin:7px;
	left: 150px;
    position: absolute;
    background-color:#FFFECE;
}
         </style>

   

</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr class="form_heading">
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label13" runat="server"  Text="Proposal details information" Font-Bold="True" ForeColor="White" Font-Size="Small"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                      <asp:DetailsView ID="dvSingleRecord" runat="server"  AutoGenerateRows="False" 
    DataKeyNames="PROPOSAL" CssClass="details-view" Width="400px" >
                     
    <Fields>
        <asp:BoundField DataField="PROPOSAL" HeaderText="Proposal #" />
        <asp:BoundField DataField="POLICY" HeaderText="Policy #" />
       <asp:BoundField DataField="PROPOSAL_DATE" HeaderText="Proposal Date" DataFormatString="{0:dd-MMM-yyyy}"  />
        <asp:BoundField DataField="NAME" HeaderText="Client Name" />

        <asp:BoundField DataField="CNIC" HeaderText="CNIC" />
        <asp:BoundField DataField="AGE" HeaderText="Age" />

        <asp:BoundField DataField="education" HeaderText="Education" />
        <asp:BoundField DataField="Matrial_Status" HeaderText="Matrial Status" />
        <asp:BoundField DataField="OCCUPATION" HeaderText="Occupation" />
                       

        <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="Account/IBAN #" />

        <asp:BoundField DataField="PHONE_NUMBER" HeaderText="Mobile #" />
        <asp:BoundField DataField="FAX_NUMBER" HeaderText="Int'l / alternate Mobile Number #" />
        <asp:BoundField DataField="ADDRESS" HeaderText="Address" />
        
        <asp:BoundField DataField="PLAN_TERM" HeaderText="Plan Term" />

        <asp:BoundField DataField="SUM_ASSURED" HeaderText="Sum Assured" DataFormatString="{0:N2}"  />
        <asp:BoundField DataField="ANNUAL_PREMIUM" HeaderText="Annual Premium" DataFormatString="{0:N2}"  />
        <asp:BoundField DataField="PREMIUM_MODE" HeaderText="Premium Mode" />
        <asp:BoundField DataField="Comments" HeaderText="Comments" />

        <asp:BoundField DataField="Comments_date" HeaderText="Comments Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"  />

         <asp:BoundField DataField="cbc_date" HeaderText="CBC Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"  />

        <asp:BoundField DataField="Transaction_Status" HeaderText="Transaction Status" />
        <asp:BoundField DataField="Transaction_Date" HeaderText="Transaction Date" />
        <asp:BoundField DataField="FT_Number" HeaderText="FT Number" />

        

    </Fields>

															

</asp:DetailsView>

                </td>
                <td></td>

            </tr>
            <tr>
                <td></td>
                <td></td>

            </tr>
            <tr>
                <td>

                 

                </td>
                <td></td>

            </tr>
        </table>
    </form>
</body>
</html>
