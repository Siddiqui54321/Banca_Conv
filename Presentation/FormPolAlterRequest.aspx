<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="FormPolAlterRequest.aspx.cs" Inherits="Bancassurance.Presentation.FormPolAlterRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Policy Alteration Request</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js">

    </script>

<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }

    function HideProgress() {
        $(".loading").hide();
        $(".modal").remove(); // Removes the overlay modal
    }

     //Replace .live() with .on() for better performance and compatibility
    $(document).on("submit", "form", function (e) {
        ShowProgress();

        // Simulate the completion of the server operation (e.g., Excel export)
        // Replace this with your actual callback or success handler
        setTimeout(function () {
            HideProgress();
        }, 3000); // Adjust the timeout based on the actual process duration
    });


</script>

   <%-- ----------Loader Style--------------------%>
    <script type="text/javascript">
        function triggerButtonClick() { document.getElementById('<%= btnForLOV.ClientID %>').click(); }

    </script>

    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>


   <style type="text/css">

       .posbtn{
            background-image: url('images/bg_btn.gif');
           background-repeat:repeat-x;font-weight: bold;
           font-size: 11px;
           font-family: Verdana, Arial, Helvetica, sans-serif;
           padding: 4px 12px;text-align: center;
           min-width: 75px;
           
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

        .lblHeading {

            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight:700;
        }

        .rcorners1 {
  border-radius: 100px;
  /*background: #73AD21;*/
  margin-left:auto;
  padding: 10px;
  width: 900px;
  filter: drop-shadow(10px 10px 10px rgba(0,0,0,0.3));
}

.tablecss {
    border-collapse:separate;
    border:solid black 4px;
    border-radius:20px;
    border-color:cadetblue;
}
.lblnew {
    border-radius:12px;
    background-color:#00AEEF;
    width:300px;
    color:black
}

         hr.new3 {
   border: 10px solid green;
  border-radius: 6px;
}
.txt
{

    text-align:left;
    background-color: transparent;
      outline: none;
      border:none;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-size:medium;
    font-weight:700;
}

.txtTracking
{

    text-align:left;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-size:large;
    font-weight:700;
}
         </style>
   
     <script type="text/javascript">
         function validateDate(input) {
             // Regular expression for dd/mm/yy format
             var datePattern = /^([0-2][0-9]|3[0-1])\/(0[1-9]|1[0-2])\/(\d{4})$/;

             if (!datePattern.test(input.value)) {
                 alert("Please provide date in format in dd/mm/yy like 10/05/2025");
                 input.value = ""; // Clear invalid input
                 input.disabled = true; // Disable the TextBox
                 input.focus(); // Bring the focus back to the TextBox
                 input.disabled = false;
             }
         }
    </script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;
          
        if (
            charCode === 8 || // Backspace
            charCode === 46 || // Delete
            (charCode >= 37 && charCode <= 40) || // Arrow keys
            (charCode >= 48 && charCode <= 57) // Numbers
            )
            return true;
        else
            return false;
        }

        function MaxSize(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;

            // Get the current value of the input field
            var input = evt.target;

              // If the key is a number, ensure the total length is less than 7
            if (input.value.length >= 10) {
                return false; // Block if length exceeds 10 characters
            }
            else {
                 return true; // Allow maximum 10 characters

            }

            
        }


</script>

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script>
    function showLoading() {
        $("#loadingSpinner").show(); // Show spinner
        $("#btnSendEmail").prop("disabled", true); // Disable button
    }
</script>

   

    <style>
    #loadingSpinner {
        display: none;
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-size: 20px;
        background: rgba(0, 0, 0, 0.7);
        color: white;
        padding: 20px;
        border-radius: 10px;
        text-align: center;
    }
        .auto-style1 {
            height: 32px;
        }
        .auto-style2 {
            height: 52px;
        }
        
        .lblSurrender {
            font-size:small;
        }

        .ddc {
    width:850px;
    position:absolute;

}
        .auto-style3 {
            border-collapse: separate;
            border: 4px solid cadetblue;
            border-radius: 20px;
            width: 850px;
        }
        .auto-style4 {
            width: 176px;
        }
    </style>

     <%--File upload  validation--%>
  <script>

      let groupFiles = {
          1: new Set(),
          2: new Set(),
          3: new Set()
      };

      // This map tracks which file name was added by which input element
      let inputFileMap = new Map();

      function validateFileUpload(input, group) {
          let files = input.files;
          let message = document.getElementById("messageGroup" + group);
          let fileSet = groupFiles[group];

          message.innerHTML = ""; // Clear previous messages

          // Remove the previously stored file from this input (if any)
          if (inputFileMap.has(input)) {
              let oldFileName = inputFileMap.get(input);
              fileSet.delete(oldFileName);
          }

          // Check for duplicates
          for (let i = 0; i < files.length; i++) {
              if (fileSet.has(files[i].name)) {
                  message.innerHTML = "Duplicate file detected: " + files[i].name;
                  input.value = ""; // Clear file input
                  inputFileMap.delete(input); // also remove from tracking map
                  return false;
              }
          }

          // Add new file name(s) to the set and tracking map
          for (let i = 0; i < files.length; i++) {
              fileSet.add(files[i].name);
              inputFileMap.set(input, files[i].name); // associate input with the file name
          }

          return true;
      }

      // Bind handlers
      document.querySelectorAll("input[type='file']").forEach(input => {
          let group = parseInt(input.getAttribute("data-group"));
          if (!group) return;

          input.addEventListener("change", function () {
              validateFileUpload(this, group);
          });

          input.addEventListener("click", function () {
              // Optional: clear value so selecting same file again is detected
              this.value = "";
          });
      });


  </script>




</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <%--  <div style="Z-INDEX: 101" id="NormalEntryTableDiv" runat="server">--%>

            <asp:Panel ID="PnlReport" runat="server" Visible="false" >
            <table width="800px">
                <tr>
                    <td>
                        
                        <asp:Label ID="Label56" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" placeholder="dd/mm/yyyy" onblur="validateDate(this)" ValidationGroup="Report" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtDateFrom" ToolTip="Date Required" ValidationGroup="Report">*</asp:RequiredFieldValidator>
                    </td>
                    <td>

                        <asp:Label ID="Label57" runat="server" Text="Date To"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo" runat="server" onblur="validateDate(this)" placeholder="dd/mm/yyyy" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtDateTo" ToolTip="Date Required" ValidationGroup="Report">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnExcelReport" runat="server" CssClass="posbtn"  BorderStyle="None" Font-Bold="True" ForeColor="White" OnClick="btnExcelReport_Click" Text="Excel Report" ValidationGroup="Report" />
                    </td>
                </tr>
                  <tr>
                    <td colspan="3">
                        <asp:Label ID="lblReport" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <div align="center">
    <h2>Plan Modification Request Form
            </div>
    
          <table  width="900px">
        <tr>
            <td colspan="2" style="background-color:#00AEEF;color:white;font-weight:700">
                <asp:Label ID="Label13" runat="server" Text="Policy Searching Criteria" Font-Bold="True" ForeColor="White"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblEmailNotfound" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="CNIC / Policy / Proposal No"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCNIC" CssClass="txtTracking" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Find" CssClass="posbtn" OnClick="Button1_Click" BorderStyle="None" Font-Bold="True" ForeColor="White" />
                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="posbtn"  BorderStyle="None" Font-Bold="True" ForeColor="White" OnClick="btnReport_Click" />
                &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">CNIC</asp:ListItem>
                    <asp:ListItem>Policy</asp:ListItem>
                    <asp:ListItem>Proposal</asp:ListItem>
                   
                </asp:RadioButtonList>
            </td>
            <td>
              
            </td>
        </tr>

        <tr>
            <td>
                  <div class="loading" align="center">
    Please wait.<br />
    <br />
    <img src="images/loading.gif" alt="No Image" />

                     
                
</div>

            </td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="2" style="text-align:center;">
                <asp:Panel ID="PnlMultiRec" runat="server" Visible="false">
                    <asp:Label ID="Label2" runat="server"  CssClass="lblHeading" Text="NIC have multiple Policies please select one"></asp:Label>
                   <br />
                    <asp:DropDownList ID="LovMultiRec" runat="server"  Font-Size="Large"  onchange="triggerButtonClick()"></asp:DropDownList>

                    
                    <asp:Button ID="btnForLOV" runat="server" Text="Button" OnClick="btnForLOV_Click" Style="display:none;"  />
                </asp:Panel>     
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
            </table>

    <div class="rcorners1" style="margin-left:5px">
        <asp:Panel ID="PnlView" runat="server" Visible="false">

    <table cellpadding="2" style="height:150px;background-color:white;" width="850px">

        <tr>
            <td style="background-color:#00AEEF;color:black;font-weight:700" colspan="8">
                 <asp:Label ID="Label3" runat="server" Text="   Policy Information" Font-Bold="True" ForeColor="Black" Font-Size="Large"></asp:Label>


            </td>

        </tr>

            <tr>
                <td colspan="8" style="text-align:left;">
                    <asp:Label ID="Label23" runat="server" CssClass="lblHeading" Font-Bold="True" ForeColor="#3333CC" Text="Policy / CNIC / Proposal No"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label14" runat="server" CssClass="lbl" Font-Bold="True" Text="Policy No"></asp:Label>
                    <br />
                    <asp:Label ID="lblPol" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label16" runat="server" CssClass="lbl" Font-Bold="True" Text="CNIC No"></asp:Label>
                    <br />
                    <asp:Label ID="lblCNIC" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label17" runat="server" CssClass="lbl" Font-Bold="True" Text="Proposal No"></asp:Label>
                    <br />
                    <asp:Label ID="lblPro" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label18" runat="server" CssClass="lbl" Font-Bold="True" Text="ZONE"></asp:Label>
                    <br />
                    <asp:Label ID="lblZone" runat="server" CssClass="lbl"></asp:Label>
                </td>
            </tr>

            <tr>
            <td colspan="8" style="text-align:left;">
                <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="#3333CC" Text="Personal Information" CssClass="lblHeading"></asp:Label>
                </td>
        </tr>
       
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label19" runat="server" CssClass="lbl" Font-Bold="True" Text="Client Name"></asp:Label>
                    <br />
                    <asp:Label ID="lblName" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label20" runat="server" CssClass="lbl" Font-Bold="True" Text="Gender"></asp:Label>
                    <br />
                    <asp:Label ID="lblGender" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label21" runat="server" CssClass="lbl" Font-Bold="True" Text="Date of Birth"></asp:Label>
                    <br />
                    <asp:Label ID="lblDob" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label22" runat="server" CssClass="lbl" Font-Bold="True" Text="Age"></asp:Label>
                    <br />
                    <asp:Label ID="lblAge" runat="server" CssClass="lbl"></asp:Label>
                </td>
            </tr>
       
            <tr>
            <td colspan="8" style="text-align:left">
                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="#3333CC" Text="Date Limit" CssClass="lblHeading"></asp:Label>
                </td>
        </tr>
       
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label26" runat="server" CssClass="lbl" Font-Bold="True" Text="Issue Date"></asp:Label>
                    <br />
                    <asp:Label ID="lblIssueDate" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label27" runat="server" CssClass="lbl" Font-Bold="True" Text="Next Due Date"></asp:Label>
                    <br />
                    <asp:Label ID="lblDueDate" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label28" runat="server" CssClass="lbl" Font-Bold="True" Text="Maturity Date"></asp:Label>
                    <br />
                    <asp:Label ID="lblMaturityDate" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label29" runat="server" CssClass="lbl" Font-Bold="True" Text="Risk Date"></asp:Label>
                    <br />
                    <asp:Label ID="lblRiskDate" runat="server" CssClass="lbl"></asp:Label>
                </td>
            </tr>
       
            <tr>
            <td colspan="8" style="text-align:left">
                <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="#3333CC" Text="Financial Info" CssClass="lblHeading"></asp:Label>
                </td>
        </tr>
       
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label31" runat="server" CssClass="lbl" Font-Bold="True" Text="Sum Assured"></asp:Label>
                    <br />
                    <asp:Label ID="lblSum" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label32" runat="server" CssClass="lbl" Font-Bold="True" Text="Premium"></asp:Label>
                    <br />
                    <asp:Label ID="lblPremimum" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label33" runat="server" CssClass="lbl" Font-Bold="True" Text="Term of assurance"></asp:Label>
                    <br />
                    <asp:Label ID="lblTermIssue" runat="server" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="Label34" runat="server" CssClass="lbl" Font-Bold="True" Text="TERM"></asp:Label>
                    <br />
                    <asp:Label ID="lblTerm" runat="server" CssClass="lbl"></asp:Label>
                </td>
            </tr>
       
            <tr>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
                <td>&nbsp;</td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
        </tr>
       
    </table>
            <br />
            
            <asp:Panel ID="PnlOption" runat="server" Visible="false">
            <table cellpadding="2" class="auto-style3" style="background-color:white">
                <tr>
                    <td colspan="2" style="color:black;font-weight:700;font-size:large;text-align:center"><asp:Label ID="Label44" runat="server" CssClass="lblnew" Text="Plan Modification Requirement" Width="300px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label42" runat="server" CssClass="lbl" Font-Bold="True" Text="Select Activity Type"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:DropDownList ID="cboAlterType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboAlterType_SelectedIndexChanged">
                            <asp:ListItem>Select Activity Type</asp:ListItem>
                            <asp:ListItem Value="1">Financial</asp:ListItem>
                            <asp:ListItem Value="2">Non Financial</asp:ListItem>
                            <asp:ListItem Value="3">Surrender</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                          
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label43" runat="server" CssClass="lbl" Font-Bold="True" Text="Select Sub Activity Type"></asp:Label>
                    </td>
                    <td>
                       
                        <asp:DropDownList ID="cboSubalter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSubalter_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:HyperLink ID="DownloadLink" runat="server" Target="_blank" Visible="False">[DownloadLink]</asp:HyperLink>
                   
                            </td>
                </tr>
                </table>
                </asp:Panel>
           
            <asp:Panel ID="PnlFinancial" runat="server" Visible="false">
           <table cellpadding="2" class="tablecss" style="background-color:white" width="850px">
               
                
                <tr>
                    <td colspan="2" style="color:black;font-weight:700;font-size:large;text-align:center">
                        <asp:Label ID="Label46" runat="server" CssClass="lblnew" Text="Mandatory Documents upload for Financial alteration"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label35" runat="server" CssClass="lbl" Font-Bold="True" Text="Original Policy Document"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FinFileUpload1" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FinFileUpload1" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$"  ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="FinFileUpload1" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label36" runat="server" CssClass="lbl" Font-Bold="True" Text="Alteration Request Form"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FinFileUpload2" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" AutoPostBack="false"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FinFileUpload2" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FinFileUpload2" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label37" runat="server" CssClass="lbl" Font-Bold="True" Text="Customer Written Request"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FinFileUpload3" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="FinFileUpload3" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FinFileUpload3" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label38" runat="server" CssClass="lbl" Font-Bold="True" Text="Customer Valid CNIC"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FinFileUpload4" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="FinFileUpload4" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="FinFileUpload4" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label39" runat="server" CssClass="lbl" Font-Bold="True" Text="Revised Illustration"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FinFileUpload5" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="FinFileUpload5" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="FinFileUpload5" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label48" runat="server" CssClass="lbl" Font-Bold="True" Text="Auto Debit Form"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:FileUpload ID="FinFileUpload6" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 1)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="FinFileUpload6" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitFin"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="FinFileUpload6" ErrorMessage="PDF file required" ValidationGroup="SubmitFin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" colspan="2" align="center">
                        <hr />
                        <div id="fileListGroup1"></div>
    <div id="messageGroup1" style="color: red;">
</div>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label52" runat="server" CssClass="lbl" Font-Bold="True" Text="Tracking #"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTrackingNoFin" runat="server" CssClass="txtTracking" Font-Bold="True" Font-Size="Medium" placeholder ="Tracking no required" onkeypress="return MaxSize(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTrackingNoFin" ErrorMessage="Tracking no Required" ForeColor="#FF3300" ToolTip="Tracking no Required" ValidationGroup="SubmitFin">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label53" runat="server" CssClass="lbl" Font-Bold="True" Text="From (Email address)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailFin" runat="server" CssClass="txt" Font-Bold="True" Font-Size="Medium" Width="350px" placeholder ="E-mail address required" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                </table>
           
                </asp:Panel>
            <asp:Panel ID="PnlButtonFin" runat="server" Visible="false">
                <table cellpadding="2" class="tablecss" style="background-color:white" width="850px">
                    <tr>
                        <td style="text-align:center;">

                       
                 <asp:Button ID="btnSubmitFin" runat="server"  CssClass="posbtn" Font-Bold="True"  ForeColor="White" OnClick="btnSubmitFin_Click" Text="Submit" ValidationGroup="SubmitFin" BorderStyle="None" />
                        <asp:Button ID="btnResetFin" runat="server"  CssClass="posbtn" Font-Bold="True"  ForeColor="White" OnClick="btnResetFin_Click" Text="Reset" ValidationGroup="false" BorderStyle="None" />
                        <asp:Label ID="lblFinSubmit" runat="server" ForeColor="Red"></asp:Label>

                             </td>
                    </tr>
                    </table>
                </asp:Panel>
               <asp:Panel ID="PnlNonFin" runat="server" Visible="false">
           <table cellpadding="2" class="tablecss" style="background-color:white" width="850px">
         
                     <tr>
                    <td colspan="2" style="color:black;font-weight:700;font-size:large;text-align:center">
                        <asp:Label ID="Label47" runat="server" CssClass="lblnew" Text="Mandatory Documents upload for Non-Financial alteration"></asp:Label>
                    </td>
                </tr>
                     <tr>
                         <td>
                             <asp:Label ID="Label4" runat="server" CssClass="lbl" Font-Bold="True" Text="Original Policy Document"></asp:Label>
                         </td>
                         <td>
                             <asp:FileUpload ID="NonFUploadPolDoc" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 2)" />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="NonFUploadPolDoc" ErrorMessage="Only PDF & JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitNonFin" Display="Dynamic" EnableClientScript="true"></asp:RegularExpressionValidator>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="NonFUploadPolDoc" ErrorMessage="PDF file required" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="lbl" Font-Bold="True" Text="Alteration Request Form"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="NonFUploadAlterForm" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 2)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="NonFUploadAlterForm" ErrorMessage="Only PDF & JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitNonFin" Display="Dynamic" EnableClientScript="true"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="NonFUploadAlterForm" ErrorMessage="PDF file required" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" CssClass="lbl" Font-Bold="True" Text="Customer/Beneficiary CNIC"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="NonFUploadBenCNIC" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 2)"  />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="NonFUploadBenCNIC" ErrorMessage="Only PDF & JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="NonFUploadBenCNIC" ErrorMessage="PDF file required" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="lbl" Font-Bold="True" Text="Customer Written Request"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="NonFUploadCustReq" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 2)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="NonFUploadCustReq" ErrorMessage="Only PDF & JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="NonFUploadCustReq" ErrorMessage="PDF file required" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuardianCnic" runat="server" CssClass="lbl" Font-Bold="True" Text="Guardian CNIC </br> (If guardian change alteration is selected)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="NonFUploadGuarCNIC" runat="server" ClientIDMode="Static" onchange="validateFileUpload(this, 2)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="NonFUploadGuarCNIC" ErrorMessage="Only PDF & JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="NonFUploadGuarCNIC" ErrorMessage="PDF file required" ValidationGroup="SubmitNonFin" Display="Dynamic"></asp:RequiredFieldValidator>
                                          
                    </td>
                </tr>
                     <tr>
                         <td colspan="2" align="center">
                             <hr />
                              <div id="fileListGroup2"></div>
    <div id="messageGroup2" style="color: red;"></div>
                             </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:Label ID="Label54" runat="server" CssClass="lbl" Font-Bold="True" Text="Tracking #"></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox ID="txtTrackingNoNonFin" runat="server" CssClass="txtTracking" Font-Bold="True" Font-Size="Medium" onkeypress="return MaxSize(event)" placeholder="Tracking no required"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTrackingNoNonFin" ErrorMessage="Tracking no required" ForeColor="#FF3300" onkeypress="return MaxSize(event)" ToolTip="Tracking no Required" ValidationGroup="SubmitNonFin">*</asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:Label ID="Label55" runat="server" CssClass="lbl" Font-Bold="True" Text="From (Email address)"></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox ID="txtEmailNonFin" runat="server" CssClass="txt" Font-Bold="True" Font-Size="Medium" Width="350px" placeholder ="Enter E-mail here" Enabled="False"></asp:TextBox>
                         </td>
                     </tr>
               </table>
                   
            </asp:Panel>

<asp:Panel ID="PnlButtonNonFin" runat="server" Visible="false">            

            <table cellpadding="2" class="tablecss" style="background-color:white" width="850px">
        
                <tr>
                    <td style="text-align:center;">
                        <asp:Button ID="btnSubmit" runat="server"  CssClass="posbtn" Font-Bold="True" ForeColor="White" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="SubmitNonFin" BorderStyle="None"    />
                        <%--<div id="loadingSpinner">Processing... Please wait</div>--%>
                        <asp:Button ID="btnReset" runat="server"  CssClass="posbtn" Font-Bold="True"  ForeColor="White" OnClick="btnReset_Click" Text="Reset" ValidationGroup="false" BorderStyle="None"  />
                        <asp:Label ID="lblNonFinSubmitError" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                </table>

    </asp:Panel>

            <%--Last Completed--%>

             <asp:Panel ID="PnlSu" runat="server" Visible="false" Height="450px">
           <table cellpadding="2" class="tablecss" style="background-color:white; height: auto;" width="850px";>
               
                
                <tr>
                    <td colspan="2" style="color:black;font-weight:700;font-size:large;text-align:center">
                        <asp:Label ID="Label9" runat="server" CssClass="lblnew" Text="Mandatory Documents upload for Surrender"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Customer Valid CNIC (Bank Attested)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload1" runat="server" ClientIDMode="Static" CssClass="lblSurrender" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="SuFileUpload1" CssClass="lblSurrender" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu"></asp:RegularExpressionValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="SuFileUpload1" CssClass="lblSurrender" ErrorMessage="PDF file required" ValidationGroup="SubmitSu"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Written Request (Bank Attested)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload2" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="SuFileUpload2" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SuFileUpload2" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="CZ-50 (Waiting period is 30 days)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload3" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="SuFileUpload3" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="SuFileUpload3" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Original Policy Document"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload4" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="SuFileUpload4" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="SuFileUpload4" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label58" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Account Maintenance Certificate"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload8" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="SuFileUpload8" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="SuFileUpload8" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label59" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Account Closing Certificate (If Old Account Closed) "></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload9" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="SuFileUpload9" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label40" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Stop Auto-Debit Request (Bank Attested and BM Verified)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload5" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="SuFileUpload5" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="SuFileUpload5" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label60" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Indemnity Bond for Surrender (If Original Policy Document Not Available)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload10" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="SuFileUpload10" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label41" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Newspaper Add (Waiting period is 30 days)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload6" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="SuFileUpload6" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="SuFileUpload6" ErrorMessage="PDF file required" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; background-color: #C0C0C0;">
                        <asp:Label ID="Label61" runat="server" CssClass="lbl" Font-Bold="True" Text="For Overseas Customers"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label45" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Provide the proof of visiting Pakistan (Copy of Passport where the Entry and Exit Date shown)"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="SuFileUpload7" runat="server" CssClass="lblSurrender" ClientIDMode="Static" onchange="validateFileUpload(this, 3)" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="SuFileUpload7" ErrorMessage="Only PDF &amp; JPG files allowed" ForeColor="#FF3300" ValidationExpression="^.*\.(pdf|jpg|jpeg)$" ValidationGroup="SubmitSu" CssClass="lblSurrender"></asp:RegularExpressionValidator>
                    
                     <hr />
   <div id="fileListGroup3"></div>
    <div id="messageGroup3" style="color: red;"></div>
                    
                    </td>
                </tr>
              
                <tr>
                    <td>
                        <asp:Label ID="Label50" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="Tracking #"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSuTracking" runat="server" CssClass="txtTracking" Font-Bold="True" Font-Size="Medium" placeholder ="Tracking no required" onkeypress="return MaxSize(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtSuTracking" ErrorMessage="Tracking no Required" ForeColor="#FF3300" ToolTip="Tracking no Required" onkeypress="return MaxSize(event)" ValidationGroup="SubmitSu">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label51" runat="server" CssClass="lblSurrender" Font-Bold="True" Text="From (Email address)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSuMailFrom" runat="server" CssClass="txt" Font-Bold="True" Font-Size="Medium" Width="500px" placeholder ="E-mail address required" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                   <td colspan="2" align="center">
                                         

                       <asp:Button ID="btnSuSubmit" runat="server"  CssClass="posbtn" Font-Bold="True" ForeColor="White" OnClick="btnSuSubmit_Click" Text="Submit" ValidationGroup="SubmitSu" BorderStyle="None"  />
                        <asp:Button ID="btnSuRest" runat="server"  CssClass="posbtn" Font-Bold="True"  ForeColor="White" OnClick="btnSuRest_Click" Text="Reset" ValidationGroup="false" BorderStyle="None"  />
                        <asp:Label ID="lblSuError" runat="server" ForeColor="Red"></asp:Label>
                                         </td>
               </tr>
                </table>
           
                </asp:Panel>

            
         
            </asp:Panel>
   

            </div>
      
        
    
          <%--  </div>--%>
     

    </form>
</body>
</html>
