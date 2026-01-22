<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDispatchReport.aspx.cs" Inherits="Bancassurance.Presentation.frmDispatchReport" %>

<%@ Register assembly="Enterprise" namespace="SHMA.Enterprise.Presentation.WebControls" tagprefix="SHMA" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


        .btn {
            /*background-image: url(~/Presentation/images/beneficiary_active.gif);*/
            /*D: \Imran_App\Presentation\images\beneficiary_active.gif*/
            /*background-repeat: repeat-x;*/
            color: #fff;
            background-color: #6699CC;
            text-decoration: none;
            /*font-weight: bold;*/
            /*font-size: 11px;*/
            font-family: Verdana, Arial, Helvetica, sans-serif;
            padding: 4px 12px;
            text-align: center;
            min-width: 75px;
            border: none;
            /*line-height: px;*/
            border-radius: 50%;

        }
       
        .mGridUndispatch {   
    width: 100%;   
    background-color: #fff;   
    margin: 5px 0 10px 0;   
    border: solid 1px #525252;   
    border-collapse:collapse;   
    border-bottom:solid 1px #525252;   
    border-bottom-color:#525252;
}  
.mGridUndispatch th {   
    font-family: Verdana, Arial, Helvetica, sans-serif;
    padding: 4px 2px;   
    color:white;   
    background-color:#6699CC ;
    text-align:center;
    border-left: solid 1px #525252;   
    font-size: 10px;
}
.mGridUndispatch td {
                font-family: Verdana, Arial, Helvetica, sans-serif;
                color: #666666;
                font-size: 10px;
            }

        </style>
</head>
<body>
    <form id="form1" runat="server">
       
        <div>
        <table cellpadding="0" cellspacing="0" width="50%" border="0">
            <tr>
                <td colspan="3" style="text-align: center">
                        <asp:Label ID="lblFinalMsg3" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700" Font-Size="Large">Policy documents are not dispatched</asp:Label>
                    </td>
            </tr>


            <tr>
                <td colspan="3">
                        <asp:Label ID="lblFinalMsg2" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700">Search Criteria</asp:Label>
                    </td>
            </tr>


            <tr>
                <td>
                        <asp:Label ID="lblFinalMsg" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Proposal No</asp:Label>
                    </td>
                <td>
                    <SHMA:TextBox ID="txtProposal" runat="server"></SHMA:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProposal" ErrorMessage="RequiredFieldValidator" ToolTip="Proposal no required" ValidationGroup="Proposal">*</asp:RequiredFieldValidator>
                </td>
                <td><asp:Button ID="btnFindProposal" runat="server" Text="Find"  CssClass="btn" OnClick="btnFindProposal_Click" ValidationGroup="Proposal"  />
                    </td>
            </tr>


            <tr>
                <td>
                        <asp:Label ID="lblFinalMsg0" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Date from</asp:Label>
                    </td>
                <td>
                    <SHMA:TextBox ID="txtFrom" runat="server" placeholder="dd/mm/yyyy"></SHMA:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom" ErrorMessage="RequiredFieldValidator" ToolTip="From date required" ValidationGroup="DataFind">*</asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td>
                        <asp:Label ID="lblFinalMsg1" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Date to</asp:Label>
                    </td>
                <td>
                    <SHMA:TextBox ID="txtTo" runat="server" placeholder="dd/mm/yyyy"></SHMA:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo" ErrorMessage="RequiredFieldValidator" ToolTip="To date required" ValidationGroup="DataFind">*</asp:RequiredFieldValidator>
                </td>
                <td><asp:Button ID="btnFindByDate" runat="server" Text="Find"  CssClass="btn" OnClick="btnFindByDate_Click" ValidationGroup="DataFind"  />
                    </td>
            </tr>


        </table>
        <hr />
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>

                        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"  CssClass="btn" OnClick="btnExportExcel_Click"  />

                    &nbsp;<asp:Button ID="btnShowAll" runat="server" Text="Show All"  CssClass="btn" OnClick="btnShowAll_Click" ValidationGroup="false"  />

                    </td>
                </tr>
                <tr>
                    <td>
                           <asp:GridView ID="gvDispatchInfo" runat="server" CssClass="mGrid" EmptyDataText="No Dispatch information available in system" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnPageIndexChanging="gvDispatchInfo_PageIndexChanging" PageSize="20">
                          
                               <Columns>
                    <asp:BoundField DataField="CONSIGNMENTNO" HeaderText="Consignment No" />
                    <asp:BoundField DataField="CONSIGNMENT_NAME" HeaderText="Consignment Name" />
                     <asp:BoundField DataField="PROPOSALNO" HeaderText="PROPOSAL #" />
              
                <asp:BoundField DataField="POLICYNO" HeaderText="POLICY #" />
				<asp:BoundField DataField="ADDRESS" HeaderText="Address" />
				<asp:BoundField DataField="DOCUMENT_TYPE" HeaderText="DOCUMENT TYPE" />
                <asp:BoundField DataField="PICKUP_DATE" HeaderText="PICKUP DATE" DataFormatString="{0:dd/MM/yyyy}" />

                 <asp:BoundField DataField="ORIGIN_CITY" HeaderText="Origin City" />
                 <asp:BoundField DataField="DEST_CITY" HeaderText="Destination City" />
                <asp:BoundField DataField="DELIVERY_DATE" HeaderText="Delivery Date"  DataFormatString="{0:dd/MM/yyyy}" />

                  <asp:BoundField DataField="RECEIVE_BY" HeaderText="Receiver Name" />
                  <asp:BoundField DataField="DELIVERY_TIME" HeaderText="DELIVERY TIME" />
                 
                  <asp:BoundField DataField="DISPATCH_STATUS" HeaderText="Dispatch Status" />
                   <asp:BoundField DataField="REASON" HeaderText="REASON" />
                        </Columns>

		
                               
                           </asp:GridView>


                        

                       </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatusMsg" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>
    </form>
</body>
</html>
