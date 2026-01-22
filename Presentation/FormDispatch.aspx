<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDispatch.aspx.cs" Inherits="Bancassurance.Presentation.FormDispatch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Upload Dispatch Information in system</title>
    <style type="text/css">
        .auto-style2 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
             color: #FFFFFF;
             font-weight: bold;
             font-size: 13px;
             vertical-align: middle;
            background-color: #6699cc;
            text-align:center;
        }

        .main_title {
            font-family: Verdana, Arial, Helvetica, sans-serif;
             /*color: #FFFFFF;*/
             font-weight: bold;
             font-size: 13px;
             vertical-align: middle;
            /*background-color: #6699cc;*/
            text-align:center;
            padding:10px;
            
        }
        .subheading {
            font-family: Verdana, Arial, Helvetica, sans-serif;
             color: #FFFFFF;
             font-weight: bold;
              font-size: 12px;
             background-color: #6699cc;
            text-align:center;
        }
        .content {
    font-family: Verdana, Arial, Helvetica, sans-serif;
    font-size: 11px;
}
       
        .auto-style3 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color:black;
            font-weight: bold;
            font-size: 12px;
            background-color: #FFFFFF;
            text-align: left;
        }

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
       
        .gv{

            font-family: Verdana, Arial, Helvetica, sans-serif;
    color: #666666;
    font-size: 10px;

        }

         .mGrid {   
    width: 100%;   
    background-color: #fff;   
    margin: 5px 0 10px 0;   
    border: solid 1px #525252;   
    border-collapse:collapse;   
    border-bottom:solid 1px #525252;   
    border-bottom-color:#525252;
}  
.mGrid td {   
    font-family: Verdana, Arial, Helvetica, sans-serif;
    color: #666666;
    font-size: 10px;


    
}  
.mGrid th {   
    padding: 4px 2px;   
    color:white;   
    background-color:#6699CC ;
    text-align:center;
    border-left: solid 1px #525252;   
    font-size: 0.9em;   
}  

 .navButton {
  color: white;
  text-decoration: none;
  font-family: Helvetica, Arial, sans-serif;
  font-size: 14px;
  text-align: center;
  padding: 0 30px;
  line-height: 30px;
  display: inline-block;
  position: relative;
  border-radius: 20px;
  border:none;
  background-image: linear-gradient(#335b71 45%, #03324c 55%);
  box-shadow: 0 2px 2px #888888;
  transition: color 0.3s, background-image 0.5s, ease-in-out;
}
.navButton:hover {
  background-image: linear-gradient(#b1ccda 49%, #96b4c5 51%);
  color: #03324c;
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

    <script type="text/javascript">
        function openInNewWindow() {


            document.getElementById("myIframe").style.display = "block";
           

            var iframeSrc = document.getElementById("myIframe").src;
            window.open(iframeSrc, "_blank","width=1400,height=600");
        }
        function showDispatchReport()
        {
            setPage('DispatchReportGroup');
        }
    </script>

    <style>
        .pos {
            margin-left: 200px; /* Set left margin to 50 pixels */
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
         <asp:Label ID="errorMsg" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#333300"></asp:Label>

        <asp:Panel ID="PnlDispatch" runat="server" Visible="false">
          <div>

               <table cellpadding="0" cellspacing="0" width="100%" border="0">

                   <tr class="form_heading">
                       <td colspan="2" align="center" class="main_title">
                        <asp:Label ID="Label6" runat="server" Text="Policy Dispatch Information Panel"></asp:Label>
                         <%-- <a href="#" onClick="showDispatchReport();" class="image">Upload Dipatch Data</a>--%>
                           &nbsp;</td>
                     
                   </tr>
                 
                   <tr>
                       <td style="text-align:center"><asp:Button ID="btnAddDispatchInfo" runat="server" Text="Add Dispatch Information" CssClass="navButton" OnClick="btnAddDispatchInfo_Click"   />
                        </td>
                       <td><asp:Button ID="btnUpdateDispStatus" runat="server" Text="Update Dispatch Status" CssClass="navButton" OnClick="btnUpdateDispStatus_Click"   />
                             </td>
                       <td style="text-align:left">
                           <asp:Button ID="btnUndispatched" runat="server" Text="Un-Dispatched" CssClass="navButton"  OnClick="btnUndispatched_Click" />

                       </td>
                       
                   </tr>
                   </table>
              <hr />
            <asp:Panel ID="PnlUploadDispatchInfo" runat="server" Visible="false">
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td colspan="2" align="center" class="auto-style2"  >
                        <asp:Label ID="Label4" runat="server" Text="Dispatch Information upload in system"></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td class="auto-style3" colspan="2">
                        <asp:Label ID="Label2" runat="server" Text="Choose Excel file for upload" style="font-weight: 700"></asp:Label>
                        &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                        &nbsp;<asp:Button ID="btnUploadNew" runat="server" OnClick="btnUploadNew_Click" Text="Upload" CssClass="btn"   />
                        <%--<asp:Button ID="Button1" runat="server" text="save" style="background-image:url(../Presentation/images/bg_btn.gif)"/>--%>
                        &nbsp;<asp:Label ID="lblFileNotSelectMsg" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                        <asp:Button ID="btnUploadStatus" runat="server" CssClass="btn" OnClick="btnUploadStatus_Click" Text="Update Status" Visible="False" />
                    </td>


                </tr>

                <tr>
                    <td class="auto-style3" colspan="2">
                        <asp:GridView ID="gvDummy" runat="server" Visible="true" AutoGenerateColumns="False" CssClass="mGrid" AllowPaging="True" OnPageIndexChanging="gvDummy_PageIndexChanging" EmptyDataText="No data found" ShowHeaderWhenEmpty="True">

                             <Columns>
                    <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                    <asp:BoundField DataField="NP1_CONSIGNMENT_NAME" HeaderText="CONSIGNMENT NAME" />
                    <asp:BoundField DataField="NP1_PROPOSAL" HeaderText="PROPOSAL NO" />
                <asp:BoundField DataField="NP1_POLICYNO" HeaderText="POLICY NO" />
                 <asp:BoundField DataField="NP1_CONS_ADDRESS" HeaderText="ADDRESS" />
                 <asp:BoundField DataField="NP1_DOCUMENT_TYPE" HeaderText="DOCUMENT TYPE" />
                 <asp:BoundField DataField="NP1_PICKUP_DATE" HeaderText="PICKUP DATE"  DataFormatString="{0:dd/MM/yyyy}" />
                                 

                                 

                        </Columns>

                          

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td class="content" colspan="2">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Finalized" OnClick="btnSave_Click" CssClass="btn" Visible="False" />
                        <asp:Label ID="Label8" runat="server"></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td  colspan="2">
                        <asp:Label ID="lblFinalMsg" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="content" colspan="2">
                        <asp:GridView ID="gvFinal" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Visible="true" AllowPaging="True" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"  OnPageIndexChanging="gvFinal_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                                <asp:BoundField DataField="NP1_CONSIGNMENT_NAME" HeaderText="CONSIGNMENT NAME" />
                                <asp:BoundField DataField="NP1_PROPOSAL" HeaderText="PROPOSAL NO" />
                                <asp:BoundField DataField="NP1_POLICYNO" HeaderText="POLICY NO" />
                                <asp:BoundField DataField="NP1_CONS_ADDRESS" HeaderText="ADDRESS" />
                                <asp:BoundField DataField="NP1_DOCUMENT_TYPE" HeaderText="DOCUMENT TYPE" />
                                <asp:BoundField DataField="NP1_PICKUP_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PICKUP DATE" />
                            </Columns>
                        </asp:GridView>

                     
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblNotUpdate" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        
                        &nbsp;</td>


                </tr>

                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvNotFinal" runat="server" AutoGenerateColumns="False" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CssClass="mGrid" Visible="true" AllowPaging="True" OnPageIndexChanging="gvNotFinal_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                                <asp:BoundField DataField="NP1_CONSIGNMENT_NAME" HeaderText="CONSIGNMENT NAME" />
                                <asp:BoundField DataField="NP1_PROPOSAL" HeaderText="PROPOSAL NO" />
                                <asp:BoundField DataField="NP1_POLICYNO" HeaderText="POLICY NO" />
                                <asp:BoundField DataField="NP1_CONS_ADDRESS" HeaderText="ADDRESS" />
                                <asp:BoundField DataField="NP1_DOCUMENT_TYPE" HeaderText="DOCUMENT TYPE" />
                                <asp:BoundField DataField="NP1_PICKUP_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PICKUP DATE" />
                            </Columns>
                        </asp:GridView>


                     
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblConsingmentAlready" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvConsingAlready" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No data found" OnPageIndexChanging="gvNotFinal_PageIndexChanging" ShowHeaderWhenEmpty="True" Visible="true">
                            <Columns>
                                <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                                <asp:BoundField DataField="NP1_CONSIGNMENT_NAME" HeaderText="CONSIGNMENT NAME" />
                                <asp:BoundField DataField="NP1_PROPOSAL" HeaderText="PROPOSAL NO" />
                                <asp:BoundField DataField="NP1_POLICYNO" HeaderText="POLICY NO" />
                                <asp:BoundField DataField="NP1_CONS_ADDRESS" HeaderText="ADDRESS" />
                                <asp:BoundField DataField="NP1_DOCUMENT_TYPE" HeaderText="DOCUMENT TYPE" />
                                <asp:BoundField DataField="NP1_PICKUP_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PICKUP DATE" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </td>
                </tr>

            </table>
                 <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
 
                </asp:Panel>


        </div>
       

        <asp:GridView ID="GridView1" runat="server" Visible="False">
                        </asp:GridView>
     
        <asp:Panel ID="PnlDispatchStatus" runat="server" Visible="false">

               <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr class="form_heading">
                    <td colspan="2" align="center" class="auto-style2"  >
                        <asp:Label ID="Label1" runat="server" Text="Dispatch Status update "></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td class="auto-style3" colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="Choose Excel file for upload status" style="font-weight: 700"></asp:Label>
                        &nbsp;<asp:FileUpload ID="FileUpload2" runat="server" />
                        &nbsp;<asp:Button ID="btnUploadStatusExcel" runat="server" OnClick="btnUploadStatusExcel_Click" Text="Upload Status" CssClass="btn"   />
                        <%--<asp:Button ID="Button1" runat="server" text="save" style="background-image:url(../Presentation/images/bg_btn.gif)"/>--%>
                        &nbsp;<asp:Label ID="lblStatusFileNotFound" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                    </td>


                </tr>

                <tr>
                    <td class="auto-style3" colspan="2">
                        <asp:GridView ID="gvStatusExcel" runat="server" Visible="true" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="mGrid" AllowPaging="True" OnPageIndexChanging="gvDummy_PageIndexChanging">

                             <Columns>
                    <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                    <asp:BoundField DataField="NP1_DELIVERY_DATE" HeaderText="DELIVERY DATE" DataFormatString="{0:dd/MM/yyyy}"  />
                                    
                 <asp:BoundField DataField="NP1_ORIGIN_CITY" HeaderText="ORIGIN CITY" />
                 <asp:BoundField DataField="NP1_DEST_CITY" HeaderText="DEST CITY" />
                 <asp:BoundField DataField="NP1_RECEIVE_BY" HeaderText="RECEIVE BY" />
                 <asp:BoundField DataField="NP1_DELIVERY_TIME" HeaderText="DELIVERY TIME" />
                <asp:BoundField DataField="NP1_DISPATCH_STATUS" HeaderText="DISPATCH STATUS" />
              <asp:BoundField DataField="NP1_REASON" HeaderText="REASON" />

                                 
                       </Columns>

                             
   
                          

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td class="content" colspan="2">
                        &nbsp;<asp:Button ID="btnStatusFinal" runat="server" Text="Finalized" OnClick="btnStatusFinal_Click" CssClass="btn" Visible="False" />
                    </td>


                </tr>

                <tr>
                    <td  colspan="2">
                        <asp:Label ID="lblStatusUpdated" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="content" colspan="2">
                        <asp:GridView ID="gvStatusFinal" runat="server" AutoGenerateColumns="False" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CssClass="mGrid" Visible="true" AllowPaging="True" OnPageIndexChanging="gvFinal_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                                <asp:BoundField DataField="NP1_DELIVERY_DATE" HeaderText="DELIVERY DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="NP1_ORIGIN_CITY" HeaderText="ORIGIN CITY" />
                                <asp:BoundField DataField="NP1_DEST_CITY" HeaderText="DEST CITY" />
                                <asp:BoundField DataField="NP1_RECEIVE_BY" HeaderText="RECEIVE BY" />
                                <asp:BoundField DataField="NP1_DISPATCH_STATUS" HeaderText="DISPATCH STATUS" />
                                <asp:BoundField DataField="NP1_REASON"  HeaderText="REMARKS" />
                            </Columns>
                          

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblStatusNotUpdate" runat="server" Font-Bold="True" ForeColor="Blue" style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        
                        &nbsp;</td>


                </tr>

                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvStatusNotFinal" runat="server" AutoGenerateColumns="False" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CssClass="mGrid" Visible="true" AllowPaging="True" OnPageIndexChanging="gvNotFinal_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NP1_CONSIGNMENTNO" HeaderText="CONSIGNMENT NO" />
                                <asp:BoundField DataField="NP1_DELIVERY_DATE" HeaderText="DELIVERY DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="NP1_ORIGIN_CITY" HeaderText="ORIGIN CITY" />
                                <asp:BoundField DataField="NP1_DEST_CITY" HeaderText="DEST CITY" />
                                <asp:BoundField DataField="NP1_RECEIVE_BY" HeaderText="RECEIVE BY" />
                                <asp:BoundField DataField="NP1_DISPATCH_STATUS" HeaderText="DISPATCH STATUS" />
                                <asp:BoundField DataField="NP1_REASON"  HeaderText="REMARKS" />

                            </Columns>


                           

                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblStatusMsg" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                    </td>
                </tr>

            </table>

            </asp:Panel>


            </asp:Panel>

       <%--  <iframe id="myIframe"  align="center" runat="server"  src="frmDispatchReport.aspx"  style="display: none;margin-left:200px;margin-top:100px;"  ></iframe>--%>

        <asp:Panel ID="PnlUndispatch" runat="server" Visible="false">

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
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Proposal No</asp:Label>
                        &nbsp;</td>
                <td>
                   
                    
                    <asp:TextBox ID="txtProposal" runat="server"></asp:TextBox>
                   
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProposal" ErrorMessage="RequiredFieldValidator" ToolTip="Proposal no required" ValidationGroup="FindProposal">*</asp:RequiredFieldValidator>
                   
                    
                </td>
                <td><asp:Button ID="btnFindProposal" runat="server" Text="Find"  CssClass="btn" OnClick="btnFindProposal_Click" ValidationGroup="FindProposal"  />
                    </td>
            </tr>


            <tr>
                <td>
                        <asp:Label ID="lblFinalMsg0" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Date from</asp:Label>
                    </td>
                <td>
                   
                    <asp:TextBox ID="txtFrom" runat="server" placeholder="20-JAN-2024"></asp:TextBox>
                   
                </td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td>
                        <asp:Label ID="lblFinalMsg1" runat="server" Font-Bold="True" ForeColor="Black" style="font-weight: 700">Date to</asp:Label>
                    </td>
                <td>
                   
                    <asp:TextBox ID="txtTo" runat="server" placeholder="20-JAN-2024"></asp:TextBox>
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo" ErrorMessage="RequiredFieldValidator" ToolTip="Date required" ValidationGroup="FindByDate">*</asp:RequiredFieldValidator>
                   
                </td>
                <td><asp:Button ID="btnFindByDate" runat="server" Text="Find"  CssClass="btn" OnClick="btnFindByDate_Click" ValidationGroup="FindByDate"  />
                    </td>
            </tr>


        </table>
        <hr />
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>

                        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"  CssClass="btn" OnClick="btnExportExcel_Click" ValidationGroup="false"  />

                    &nbsp;<asp:Button ID="btnShowAll" runat="server" Text="Show All"  CssClass="btn" OnClick="btnShowAll_Click" ValidationGroup="false"  />

                    </td>
                </tr>
                <tr>
                    <td>
                           <asp:GridView ID="gvDispatchInfo" runat="server" CssClass="mGridUndispatch" EmptyDataText="No Dispatch information available in system" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnPageIndexChanging="gvDispatchInfo_PageIndexChanging" PageSize="20">
                          
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
                        <asp:Label ID="lblUndispatchMsg" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>

            </asp:Panel>

    </form>
</body>
</html>
