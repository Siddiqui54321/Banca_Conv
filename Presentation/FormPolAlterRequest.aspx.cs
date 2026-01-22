using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using SHMA.Enterprise.Presentation;
using System.Collections;
using System.Web.SessionState;








namespace Bancassurance.Presentation
{
    public partial class FormPolAlterRequest : System.Web.UI.Page
    {
       
        string Sql, Msg, Search, Doc, Path;
        Excel_Upload Obj_Excel_Upload = new Excel_Upload();
        DataTable Tissues = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand CmdDML = new OleDbCommand();
        DataTable dt = new DataTable();
        string ToEmail, CCEmail, BCCEmail;

        Hashtable MyVal = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // if (SessionObject.Get("s_USE_TYPE").ToString() == "A") // Admin Login Check

                string UserEmail= System.Convert.ToString(Session["s_USE_USEREMAIL"]);

                  if (UserEmail == "") // Email from address not found
                {
                    // errorMsg.Text = "";
                    // PnlDispatch.Visible = true;
                    lblEmailNotfound.Text = "Your Email address not available. Policy alternation can to proced";
                    Button1.Enabled = false;

                }
                else
                {
                    lblEmailNotfound.Text = "";
                    Button1.Enabled = true;
                    txtEmailFin.Text = UserEmail;
                    txtEmailNonFin.Text = UserEmail;
                    txtSuMailFrom.Text = UserEmail;



                }
                }
                }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCNIC.Text == "")
            {
                lblError.Text = "First input CNIC or Policy or  Proposal #";
                RadioButtonList1.ClearSelection();
            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    Search = "NIC" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();

                    PolicyFind();
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    Search = "POL" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();
                    PolicyFind();
                }

                else if (RadioButtonList1.SelectedIndex == 2)
                {
                    Search = "PRO" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();
                    PolicyFind();
                }

                else if (RadioButtonList1.SelectedIndex == 3)
                {
                    // Reset code
                    txtCNIC.Text = "";
                  //  RadioButtonList1.ClearSelection();
                    txtCNIC.Focus();
                    Label2.Text = "";

                    LovMultiRec.Visible = false;
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;
                    //  PnlView.Visible = false;
                    lblError.Text = "";
                    PnlView.Visible = false;
                    PnlOption.Visible = false;
                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();




                }
            }
        }

      

        protected void LovMultiRec_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (RadioButtonList1.SelectedIndex == 0)
            {
                Search = "NIC" + txtCNIC.Text.ToString();
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                PnlSu.Visible = false;

                cboAlterType.ClearSelection();
                cboAlterType.SelectedIndex = 0;
                cboSubalter.Items.Clear();

                PolicyFind();
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Search = "POL" + txtCNIC.Text.ToString();
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                PnlSu.Visible = false;

                cboAlterType.ClearSelection();
                cboAlterType.SelectedIndex = 0;
                cboSubalter.Items.Clear();
                PolicyFind();
            }

            else if (RadioButtonList1.SelectedIndex == 2)
            {
                Search = "PRO" + txtCNIC.Text.ToString();
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                PnlSu.Visible = false;

                cboAlterType.ClearSelection();
                cboAlterType.SelectedIndex = 0;
                cboSubalter.Items.Clear();
                PolicyFind();
            }

            else if (RadioButtonList1.SelectedIndex == 3)
            {
                // Reset code
                txtCNIC.Text = "";
                //  RadioButtonList1.ClearSelection();
                txtCNIC.Focus();
                Label2.Text = "";

                LovMultiRec.Visible = false;
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                PnlSu.Visible = false;
                //  PnlView.Visible = false;
                lblError.Text = "";
                PnlView.Visible = false;
                PnlOption.Visible = false;
                cboAlterType.ClearSelection();
                cboAlterType.SelectedIndex = 0;
                cboSubalter.Items.Clear();




            }

          //  PolicyFindByNIC();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCNIC.Text = "";
            PnlMultiRec.Visible = false;
            PnlView.Visible = false;
           // RadioButtonList1.ClearSelection();
            txtCNIC.Text = "";
            PnlFinancial.Visible = false;
            PnlNonFin.Visible = false;
            cboAlterType.SelectedIndex = 0;
            cboSubalter.ClearSelection();
            cboSubalter.Items.Clear();
            DownloadLink.Visible = false;
            PnlButtonFin.Visible = false;
            PnlButtonNonFin.Visible = false;
            PnlSu.Visible = false;
            
        }

        protected void cboAlterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAlterType.SelectedIndex == 1)
            {
                // Financial
                cboSubalter.Items.Clear();
                DownloadLink.Visible = false;
                PnlButtonFin.Visible = false;
                PnlButtonNonFin.Visible = false;
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                //Surrender
                PnlSu.Visible = false;
               

                Session["Upload"] = "1";
                cboSubalter.Visible = true;
                cboSubalter.Enabled = true;

                cboSubalter.Items.Insert(0, "Select Financial Alternation");
                cboSubalter.Items.Add(new ListItem("Addition of riders", "1500200145"));
                cboSubalter.Items.Add(new ListItem("Benefit Term Increase", "1500200110"));
                cboSubalter.Items.Add(new ListItem("Benefit term decrease", "1500200112"));
                cboSubalter.Items.Add(new ListItem("Cancel policy - client request", "1500200142"));
                cboSubalter.Items.Add(new ListItem("Conversion To Paid Up – Manually", "1500200153"));
                cboSubalter.Items.Add(new ListItem("Death Claim", "3000400051"));
                cboSubalter.Items.Add(new ListItem("Duplicate document", "1500000001"));
                cboSubalter.Items.Add(new ListItem("Deletion of RIDER", "1500200146"));
                cboSubalter.Items.Add(new ListItem("Loan Request", "3000400037"));
                cboSubalter.Items.Add(new ListItem("Maturity", "3000100039"));
                cboSubalter.Items.Add(new ListItem("Payment mode change (big to small)", "1500200116"));
                cboSubalter.Items.Add(new ListItem("Payment mode change (small to big)", "1500200115"));
                cboSubalter.Items.Add(new ListItem("Re-instatement of paid-up", "1500200168"));
                cboSubalter.Items.Add(new ListItem("REINSTATEMENT", "1500200134"));
                cboSubalter.Items.Add(new ListItem("Refund Request", "3000400036"));
                cboSubalter.Items.Add(new ListItem("Survival Benefit", "1500200110"));
                cboSubalter.Items.Add(new ListItem("Sum assured increased", "1500200105"));
                cboSubalter.Items.Add(new ListItem("Sum assured decrease", "1500200106"));



                lblError.Text = ""; // Clear previous error messages
                lblFinSubmit.Text = "";
                lblNonFinSubmitError.Text = "";
                lblSuError.Text = "";


            }
            else if (cboAlterType.SelectedIndex == 2)
            {
                // non Financial
                cboSubalter.Items.Clear();
                DownloadLink.Visible = false;
                Session["Upload"] = "2";

                PnlButtonFin.Visible = false;
                PnlButtonNonFin.Visible = false;

                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;

                //Surrender
                PnlSu.Visible = false;
               

                cboSubalter.Visible = true;
                cboSubalter.Enabled = true;
                cboSubalter.Items.Insert(0, "Select Non Financial Alternation");
                cboSubalter.Items.Add(new ListItem("Address ( Permanent ) change", "1500100100"));
                cboSubalter.Items.Add(new ListItem("Address ( Correspondence ) change", "1500100101"));
                cboSubalter.Items.Add(new ListItem("Address(Business) change", "1500100102"));
                cboSubalter.Items.Add(new ListItem("Add / change / remove debit order detail", "1500100142"));
                cboSubalter.Items.Add(new ListItem("Add / remove / update Beneficiary / Nominee", "1500100105"));
                cboSubalter.Items.Add(new ListItem("Client name change (female only)", "1500100110"));
                cboSubalter.Items.Add(new ListItem("Change in guardian", "1500100106"));
                cboSubalter.Items.Add(new ListItem("Change Payee name / Payment method", "1500100135"));
                cboSubalter.Items.Add(new ListItem("Change Client date of birth", "1500100107"));
                cboSubalter.Items.Add(new ListItem("Client sex change", "1500100111"));
                cboSubalter.Items.Add(new ListItem("CHANGE ANF OPTION", "1500200195"));



                lblError.Text = ""; // Clear previous error messages
                lblFinSubmit.Text = "";
                lblNonFinSubmitError.Text = "";
                lblSuError.Text = "";

            }

            else if (cboAlterType.SelectedIndex == 3)
            {
                // Surrender
                //Surrender
                Session["AltTypeCode"] = "3000400024";
                PnlSu.Visible = true;
                               
                cboSubalter.Visible = true; // No sub option for surrender
                cboSubalter.Items.Clear();
                cboSubalter.Items.Insert(0, "No sub activity, Upload below documents");
                cboSubalter.Enabled = false;

              //  RenameAndDownloadPDF();
               // DownloadLink.Visible = true;
                DownloadLink.Visible = false;
                // Financial off
                PnlFinancial.Visible = false;
                PnlButtonFin.Visible = false;

                // Non Financial off
                PnlNonFin.Visible = false;
                PnlButtonNonFin.Visible = false;

                Session["Upload"] = "3";
                lblError.Text = ""; // Clear previous error messages
                lblFinSubmit.Text = "";
                lblNonFinSubmitError.Text = "";
                lblSuError.Text = "";

            }
            else
            {
                cboSubalter.Items.Clear();
                DownloadLink.Visible = false;
                PnlFinancial.Visible = false;
                PnlNonFin.Visible = false;
                PnlButtonFin.Visible = false;
                PnlButtonNonFin.Visible = false;

                PnlSu.Visible = false;
               


            }
        }

        protected void cboSubalter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Alternation form not required for Loan, Maturity, Refund, Duplicate 

            if (cboSubalter.SelectedItem.Text == "Duplicate document" || cboSubalter.SelectedItem.Text == "Loan Request" || cboSubalter.SelectedItem.Text == "Maturity" || cboSubalter.SelectedItem.Text == "Refund Request")
            {
                DownloadLink.Visible = false;
                Session["AltTypeCode"] = cboSubalter.SelectedValue.ToString();



            }
            else
            {
                DownloadLink.Visible = true;
            

           // if (cboSubalter.SelectedIndex > 0)
          //  {


                RenameAndDownloadPDF();
                Session["AltTypeCode"] = cboSubalter.SelectedValue.ToString();
                DownloadLink.Visible = true;
                if (Session["Upload"].ToString() == "1")
                {
                    PnlFinancial.Visible = true;
                    PnlButtonFin.Visible = true;
                    PnlNonFin.Visible = false;
                    PnlButtonNonFin.Visible = false;

                }

                else if (Session["Upload"].ToString() == "2")
                {
                    PnlFinancial.Visible = false;
                    PnlButtonFin.Visible = false;
                    PnlNonFin.Visible = true;
                    PnlButtonNonFin.Visible = true;

                    if (cboSubalter.SelectedItem.Text == "Client sex change" || cboSubalter.SelectedItem.Text == "Add / change / remove debit order detail" || cboSubalter.SelectedItem.Text == "Change Payee name / Payment method" || cboSubalter.SelectedItem.Text == "Client name change (female only)")
                    {
                        lblGuardianCnic.Visible = false;
                        NonFUploadGuarCNIC.Visible = false;
                        Session["AltTypeCode"] = cboSubalter.SelectedValue.ToString();
                    }
                    else
                    {

                        lblGuardianCnic.Visible = true;
                        NonFUploadGuarCNIC.Visible = true;
                        Session["AltTypeCode"] = cboSubalter.SelectedValue.ToString();
                    }


                    }


              //  }


          //  }


            //else

            //{
            //    DownloadLink.Visible = false;
            //    PnlFinancial.Visible = false;
            //    PnlNonFin.Visible = false;
            //    // PnlButton.Visible = false;

            }
        }

        protected void btnSubmitFin_Click(object sender, EventArgs e)
        {
            // Financial

            Session["TrackingNo"] = txtTrackingNoFin.Text.ToString();
            if (Page.IsValid)

            {

                List<string> savedFilePaths = new List<string>();

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(); // Configure your SMTP settings

                try
                {
                    FileUpload[] uploadControls = new FileUpload[]
                   {

                       FinFileUpload1,
                       FinFileUpload2,
                       FinFileUpload3,
                       FinFileUpload4,
                       FinFileUpload5,
                       FinFileUpload6
                       
                   };


                    foreach (FileUpload fu in uploadControls)
                    {
                        if (fu.HasFile)
                        {
                            string fileName = System.IO.Path.GetFileName(fu.FileName);
                            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                            try
                            {

                                fu.SaveAs(filePath);
                                savedFilePaths.Add(filePath);
                                Attachment attachment = new Attachment(filePath);
                                mail.Attachments.Add(attachment);
                            }
                            catch (IOException)
                            {

                                try
                                {

                                    foreach (string path in savedFilePaths)
                                    {
                                        if (File.Exists(path))
                                        {
                                            try { File.Delete(path); } catch { }
                                        }
                                    }

                                    mail.Dispose();

                                }
                                catch { }

                                throw new Exception("Duplicate file name detected. Please rename your file before uploading.");
                                throw;

                            }
                        }
                    }




                    ToEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_TO FROM USE_USERMASTER a WHERE a.Email_Flag = 'T'").Rows[0]["USER_EMAIL_TO"].ToString();
                    CCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_CC FROM USE_USERMASTER a WHERE a.Email_Flag = 'C'").Rows[0]["USER_EMAIL_CC"].ToString();
                    BCCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_BCC FROM USE_USERMASTER a WHERE a.Email_Flag = 'B'").Rows[0]["USER_EMAIL_BCC"].ToString();

                    if (!string.IsNullOrEmpty(ToEmail))
                    {
                        mail.To.Add(ToEmail);
                    }

                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        mail.CC.Add(CCEmail);
                    }

                    if (!string.IsNullOrEmpty(BCCEmail))
                    {
                        mail.Bcc.Add(BCCEmail);
                    }


                    mail.From = new MailAddress("ILASADMIN@gba.com.pk", "Branch officer");
                    mail.ReplyToList.Add(txtEmailFin.Text.ToString());

                    mail.Subject = "Policy Alteration Request (Financial) Tracking # " + txtTrackingNoFin.Text + " Policy # " + Session["PolicyNo"].ToString();

                    mail.Body = "Email sent from:" + txtEmailFin.Text.ToString() + "<br /> <br /> <b>  Please find attached and Proceed. </b> <br /><br /> " +
                                       "Tracking #." + txtTrackingNoFin.Text;


                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                  //  smtp.Host = "128.1.100.43"; //For LIVE
                   smtp.Host = "110.93.216.180"; // for local
                    smtp.Port = 27;
                    smtp.Credentials = new System.Net.NetworkCredential("ILASADMIN@gba.com.pk", "G?A=b@D@t4");

                    smtp.EnableSsl = false;


                    smtp.Send(mail);
                    SaveData();

                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    // Dispose the MailMessage (which disposes all attachments)
                    mail.Dispose();



                    lblError.Text = "The request has been successfully sent via email.";
                    PnlButtonFin.Visible = false;
                   
                    lblFinSubmit.Text = "";
                    lblNonFinSubmitError.Text = "";
                    lblSuError.Text = "";
                    DownloadLink.Text = "";
                    PnlView.Visible = false;
                    PnlMultiRec.Visible = false;
                    txtTrackingNoFin.Text = "";
                    txtCNIC.Focus();




                }
                catch (Exception Ex)
                {

                    lblFinSubmit.Text = "Duplicate file name detected. Please rename your file before uploading.";

                    lblFinSubmit.Text = Ex.Message;

                }

                finally
                {
                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    mail.Dispose();

                }

            }
        }

        protected void btnResetFin_Click(object sender, EventArgs e)
        {
            txtCNIC.Text = "";
            PnlMultiRec.Visible = false;
            PnlView.Visible = false;
           // RadioButtonList1.ClearSelection();
            txtCNIC.Text = "";
            PnlFinancial.Visible = false;
            PnlNonFin.Visible = false;
            cboAlterType.SelectedIndex = 0;
            cboSubalter.ClearSelection();
            cboSubalter.Items.Clear();
            DownloadLink.Visible = false;
            PnlButtonFin.Visible = false;
            PnlButtonNonFin.Visible = false;
            PnlSu.Visible = false;
           
        }

        protected void btnSuSubmit_Click(object sender, EventArgs e)
        {
            //Surrender
            lblSuError.Text = "";
            Session["TrackingNo"] = txtSuTracking.Text.ToString();
            if (Page.IsValid)

            {

                List<string> savedFilePaths = new List<string>();

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(); // Configure your SMTP settings

                try
                {
                    FileUpload[] uploadControls = new FileUpload[]
                   {

                       SuFileUpload1,
                       SuFileUpload2,
                       SuFileUpload3,
                       SuFileUpload4,
                       SuFileUpload5,
                       SuFileUpload6,
                       SuFileUpload7,
                       SuFileUpload8,
                       SuFileUpload9,
                       SuFileUpload10
                   };


                    foreach (FileUpload fu in uploadControls)
                    {
                        if (fu.HasFile)
                        {
                            string fileName = System.IO.Path.GetFileName(fu.FileName);
                            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                            try
                            {

                                fu.SaveAs(filePath);
                                savedFilePaths.Add(filePath);
                                Attachment attachment = new Attachment(filePath);
                                mail.Attachments.Add(attachment);
                            }
                            catch (IOException)
                            {

                                try
                                {

                                    foreach (string path in savedFilePaths)
                                    {
                                        if (File.Exists(path))
                                        {
                                            try { File.Delete(path); } catch { }
                                        }
                                    }

                                    mail.Dispose();

                                }
                                catch { }

                                throw new Exception("Duplicate file name detected. Please rename your file before uploading.");
                                throw;

                            }
                        }
                    }




                    ToEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_TO FROM USE_USERMASTER a WHERE a.Email_Flag = 'T'").Rows[0]["USER_EMAIL_TO"].ToString();
                    CCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_CC FROM USE_USERMASTER a WHERE a.Email_Flag = 'C'").Rows[0]["USER_EMAIL_CC"].ToString();
                    BCCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_BCC FROM USE_USERMASTER a WHERE a.Email_Flag = 'B'").Rows[0]["USER_EMAIL_BCC"].ToString();

                    if (!string.IsNullOrEmpty(ToEmail))
                    {
                        mail.To.Add(ToEmail);
                    }

                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        mail.CC.Add(CCEmail);
                    }

                    if (!string.IsNullOrEmpty(BCCEmail))
                    {
                        mail.Bcc.Add(BCCEmail);
                    }


                    mail.From = new MailAddress("ILASADMIN@gba.com.pk", "Branch officer");
                    mail.ReplyToList.Add(txtSuMailFrom.Text.ToString());

                    mail.Subject = "Policy Alternation Request (Surrender) Tracking # " + txtSuTracking.Text + " Policy # " + Session["PolicyNo"].ToString();

                    mail.Body = "Email sent from:" + txtSuMailFrom.Text.ToString() + "<br /> <br /> <b>  Please find attached and Proceed. </b> <br /><br /> " +
                "Tracking # " + txtSuTracking.Text;


                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                   // smtp.Host = "128.1.100.43"; //For LIVE
                     smtp.Host = "110.93.216.180"; // for local
                    smtp.Port = 27;
                    smtp.Credentials = new System.Net.NetworkCredential("ILASADMIN@gba.com.pk", "G?A=b@D@t4");

                    smtp.EnableSsl = false;


                    smtp.Send(mail);
                    SaveData();

                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    // Dispose the MailMessage (which disposes all attachments)
                    mail.Dispose();



                    lblError.Text = "The request has been successfully sent via email.";
                   
                    lblFinSubmit.Text = "";
                    lblNonFinSubmitError.Text = "";
                    lblSuError.Text = "";
                    txtSuTracking.Text = "";
                    PnlView.Visible = false;
                    PnlMultiRec.Visible = false;
                    DownloadLink.Text = "";
                    txtCNIC.Focus();




                }
                catch (Exception Ex)
                {

                    lblSuError.Text = "Duplicate file name detected. Please rename your file before uploading.";
                    lblSuError.Text = Ex.Message;

                }

                finally
                {
                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    mail.Dispose();

                }

            }
        }

        protected void btnSuRest_Click(object sender, EventArgs e)
        {
            txtCNIC.Text = "";
            PnlMultiRec.Visible = false;
            PnlView.Visible = false;
          //  RadioButtonList1.ClearSelection();
            txtCNIC.Text = "";
            PnlFinancial.Visible = false;
            PnlNonFin.Visible = false;
            cboAlterType.SelectedIndex = 0;
            cboSubalter.ClearSelection();
            cboSubalter.Items.Clear();
            DownloadLink.Visible = false;
            PnlButtonFin.Visible = false;
            PnlButtonNonFin.Visible = false;

            PnlSu.Visible = false;
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // For sent non Financial
          //  System.Threading.Thread.Sleep(3000);
            Session["TrackingNo"] = txtTrackingNoNonFin.Text.ToString();
            if (Page.IsValid)

            {

                List<string> savedFilePaths = new List<string>();

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(); // Configure your SMTP settings

                try
                {

                    lblError.Text = ""; // Clear previous error messages
                    lblFinSubmit.Text = "";
                    lblNonFinSubmitError.Text = "";
                    lblSuError.Text = "";

                    FileUpload[] uploadControls = new FileUpload[]
                   {

                       NonFUploadPolDoc,
                       NonFUploadAlterForm,
                       NonFUploadBenCNIC,
                       NonFUploadCustReq,
                       NonFUploadGuarCNIC

                   };


                    foreach (FileUpload fu in uploadControls)
                    {
                        if (fu.HasFile)
                        {
                            string fileName = System.IO.Path.GetFileName(fu.FileName);
                            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                            try
                            {

                                fu.SaveAs(filePath);
                                savedFilePaths.Add(filePath);
                                Attachment attachment = new Attachment(filePath);
                                mail.Attachments.Add(attachment);
                            }
                            catch (IOException)
                            {

                                try
                                {

                                    foreach (string path in savedFilePaths)
                                    {
                                        if (File.Exists(path))
                                        {
                                            try { File.Delete(path); } catch { }
                                        }
                                    }

                                    mail.Dispose();

                                }
                                catch { }

                                throw new Exception("Duplicate file name detected. Please rename your file before uploading.");
                                throw;

                            }
                        }
                    }




                    ToEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_TO FROM USE_USERMASTER a WHERE a.Email_Flag = 'T'").Rows[0]["USER_EMAIL_TO"].ToString();
                    CCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_CC FROM USE_USERMASTER a WHERE a.Email_Flag = 'C'").Rows[0]["USER_EMAIL_CC"].ToString();
                    BCCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_BCC FROM USE_USERMASTER a WHERE a.Email_Flag = 'B'").Rows[0]["USER_EMAIL_BCC"].ToString();

                    if (!string.IsNullOrEmpty(ToEmail))
                    {
                        mail.To.Add(ToEmail);
                    }

                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        mail.CC.Add(CCEmail);
                    }

                    if (!string.IsNullOrEmpty(BCCEmail))
                    {
                        mail.Bcc.Add(BCCEmail);
                    }


                    mail.From = new MailAddress("ILASADMIN@gba.com.pk", "Branch officer");
                    mail.ReplyToList.Add(txtEmailNonFin.Text.ToString());

                    mail.Subject = "Policy Alteration Request (Non Financial) Tracking # " + txtTrackingNoNonFin.Text + " Policy # " + Session["PolicyNo"].ToString();
                    mail.Body = "Email sent from:" + txtEmailNonFin.Text.ToString() + "<br /> <br /> <b>  Please find attached and Proceed. </b> <br /><br /> " +
                "Tracking # " + txtTrackingNoNonFin.Text;



                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                   // smtp.Host = "128.1.100.43"; //For LIVE
                    smtp.Host = "110.93.216.180"; // for local
                    smtp.Port = 27;
                    smtp.Credentials = new System.Net.NetworkCredential("ILASADMIN@gba.com.pk", "G?A=b@D@t4");

                    smtp.EnableSsl = false;


                    smtp.Send(mail);
                    SaveData();

                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    // Dispose the MailMessage (which disposes all attachments)
                    mail.Dispose();



                    lblError.Text = "The request has been successfully sent via email.";
                    PnlButtonNonFin.Visible = false;
                  
                    lblFinSubmit.Text = "";
                    lblNonFinSubmitError.Text = "";
                    lblSuError.Text = "";
                    PnlView.Visible = false;
                    PnlMultiRec.Visible = false;
                    txtTrackingNoNonFin.Text = "";
                    DownloadLink.Text = "";
                    txtCNIC.Focus();




                }
                catch (Exception Ex)
                {

                    lblFinSubmit.Text = "Duplicate file name detected. Please rename your file before uploading.";
                    lblNonFinSubmitError.Text = Ex.Message;

                }

                finally
                {
                    foreach (string path in savedFilePaths)
                    {
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }

                    mail.Dispose();

                }

            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PnlReport.Visible = false;
            if (txtCNIC.Text == "")
            {
                lblError.Text = "First input CNIC or Policy or  Proposal #";
                PnlView.Visible = false;
                PnlMultiRec.Visible = false;
                // RadioButtonList1.ClearSelection();
            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    Search = "NIC" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();

                    PolicyFind();
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    Search = "POL" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();
                    PolicyFind();
                }

                else if (RadioButtonList1.SelectedIndex == 2)
                {
                    Search = "PRO" + txtCNIC.Text.ToString();
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;

                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();
                    PolicyFind();
                }

                else if (RadioButtonList1.SelectedIndex == 3)
                {
                    // Reset code
                    txtCNIC.Text = "";
                   // RadioButtonList1.ClearSelection();
                    txtCNIC.Focus();
                    Label2.Text = "";

                    LovMultiRec.Visible = false;
                    PnlFinancial.Visible = false;
                    PnlNonFin.Visible = false;
                    PnlSu.Visible = false;
                    //  PnlView.Visible = false;
                    lblError.Text = "";
                    PnlView.Visible = false;
                    PnlOption.Visible = false;
                    cboAlterType.ClearSelection();
                    cboAlterType.SelectedIndex = 0;
                    cboSubalter.Items.Clear();

                }
            }
        }

        protected void btnForLOV_Click(object sender, EventArgs e)
        {
            DataTable dtLov = new DataTable();

            Sql = "Select * FROM view_policyinfo_aftersale pi where pi.np1_policyno='" + LovMultiRec.SelectedValue.ToString() + "'";
            dtLov= GetdataOraOledb(Sql);

            if(dtLov.Rows.Count>0)
            {
                PnlView.Visible = true;
                PnlMultiRec.Visible = true;
                lblError.Text = "";
                Session["PolicyNo"] = dtLov.Rows[0]["np1_policyno"].ToString();
                Session["ProposalNo"] = dtLov.Rows[0]["np1_proposal"].ToString();
                lblPol.Text = dtLov.Rows[0]["np1_policyno"].ToString();
                lblCNIC.Text = dtLov.Rows[0]["nph_idno"].ToString();
                lblPro.Text = dtLov.Rows[0]["np1_proposal"].ToString();
                //  lblZone.Text = dt.Rows[0]["Zone"].ToString();
                //pi.np1_policyno,pi.nph_idno,np1_issuedate,pi.np2_nextduedat,pi.np2_ageprem
                lblName.Text = dtLov.Rows[0]["nph_fullname"].ToString();
                lblGender.Text = dtLov.Rows[0]["NPH_SEX"].ToString();
                lblDob.Text = Convert.ToDateTime(dtLov.Rows[0]["nph_birthdate"]).ToString("dd-MMM-yyyy");
                lblAge.Text = dtLov.Rows[0]["nph_age"].ToString();

                lblIssueDate.Text = Convert.ToDateTime(dtLov.Rows[0]["np1_issuedate"]).ToString("dd-MMM-yyyy");
                lblDueDate.Text = Convert.ToDateTime(dtLov.Rows[0]["np2_nextduedat"]).ToString("dd-MMM-yyyy");
                lblMaturityDate.Text = Convert.ToDateTime(dtLov.Rows[0]["npr_maturitydate"]).ToString("dd-MMM-yyyy");
                //  lblRiskDate.Text = dt.Rows[0]["risk_date"].ToString();


                lblSum.Text = Convert.ToDouble(dtLov.Rows[0]["npr_sumassured"].ToString()).ToString("#,##");
                //   lblTermIssue.Text = dt.Rows[0]["term_of_assurance"].ToString();
                lblTerm.Text = dtLov.Rows[0]["npr_benefitterm"].ToString();
                lblPremimum.Text = Convert.ToDouble(dtLov.Rows[0]["np2_totpremium"].ToString()).ToString("#,##");




            }

            else
            {

                PnlView.Visible = false;

                lblError.Text = "Policy not issued";
                PnlMultiRec.Visible = false;
            }

          
        }




        #region MyCode
        void PolicyFind()
        {
           
            Sql = "select * FROM view_policyinfo_aftersale pi " + // for after sale portal
  " where case when substr('" + Search + "', 1, 3) = 'POL'" +
  " then pi.np1_policyno when substr('" + Search + "', 1, 3) = 'PRO'" +
  " then pi.np1_proposal when substr('" + Search + "', 1, 3) = 'NIC'" +
  " then pi.nph_idno end = substr('" + Search + "', 4) " +
  "   AND pi.aag_agcode='" + SessionObject.Get("s_AAG_AGCODE").ToString() + "'" +
 " order by pi.nph_idno, pi.np1_policyno ";

            dt = GetdataOraOledb(Sql);

            if (dt.Rows.Count >= 2)

            {


                PnlOption.Visible = true;
                PnlMultiRec.Visible = true;
                Label2.Text = "NIC have multiple Policies please select one";
                LovMultiRec.Visible = true;
                PnlView.Visible = false;
                lblError.Text = "";
                LovMultiRec.DataSource = dt;
                LovMultiRec.DataTextField = "np1_policyno";
                LovMultiRec.DataValueField = "np1_policyno";
                LovMultiRec.DataBind();
                LovMultiRec.Items.Insert(0, "Select Policy No");
               // RadioButtonList1.ClearSelection();

            }
            else if (dt.Rows.Count == 1)
            {

                PnlOption.Visible = true;
                PnlView.Visible = true;
                PnlMultiRec.Visible = false;
                lblError.Text = "";
                Session["PolicyNo"] = dt.Rows[0]["np1_policyno"].ToString();
                Session["ProposalNo"] = dt.Rows[0]["np1_proposal"].ToString();
                
                lblPol.Text = dt.Rows[0]["np1_policyno"].ToString();
                lblCNIC.Text = dt.Rows[0]["nph_idno"].ToString();
                lblPro.Text = dt.Rows[0]["np1_proposal"].ToString();
                //  lblZone.Text = dt.Rows[0]["Zone"].ToString();
                //pi.np1_policyno,pi.nph_idno,np1_issuedate,pi.np2_nextduedat,pi.np2_ageprem
                lblName.Text = dt.Rows[0]["nph_fullname"].ToString();
                lblGender.Text = dt.Rows[0]["NPH_SEX"].ToString();
                lblDob.Text = Convert.ToDateTime(dt.Rows[0]["nph_birthdate"]).ToString("dd-MMM-yyyy"); 
                lblAge.Text = dt.Rows[0]["nph_age"].ToString();

                lblIssueDate.Text = Convert.ToDateTime(dt.Rows[0]["np1_issuedate"]).ToString("dd-MMM-yyyy"); 
                lblDueDate.Text = Convert.ToDateTime(dt.Rows[0]["np2_nextduedat"]).ToString("dd-MMM-yyyy");
                lblMaturityDate.Text = Convert.ToDateTime(dt.Rows[0]["npr_maturitydate"]).ToString("dd-MMM-yyyy");
                //  lblRiskDate.Text = dt.Rows[0]["risk_date"].ToString();

                //   RadioButtonList1.ClearSelection();



                lblSum.Text = Convert.ToDouble(dt.Rows[0]["npr_sumassured"].ToString()).ToString("#,##");
             //   lblTermIssue.Text = dt.Rows[0]["term_of_assurance"].ToString();
                lblTerm.Text = dt.Rows[0]["npr_benefitterm"].ToString();
                lblPremimum.Text = Convert.ToDouble(dt.Rows[0]["np2_totpremium"].ToString()).ToString("#,##");

            }
            else
            {

                PnlView.Visible = false;
                PnlOption.Visible = false;
                lblError.Text = "The policy does not belong to the logged-in user";
                PnlMultiRec.Visible = false;
             //   RadioButtonList1.ClearSelection();

            }


        }
      

        void PolicyFindByNIC()
        {
 //           Search = "POL" + LovMultiRec.SelectedValue.ToString();

 //           Sql = "select pi.pcl_locatdesc Zone," +
 //               "pi.np1_proposal Proposal," +
 //      "pi.np1_policyno policyno," +
 //      "pi.nph_title || pi.nph_fullname fullname," +
 //      "pi.nph_sex gender," +
 //      "to_char(TO_DATE(pi.np1_issuedate,'DD/MM/YYYY')) risk_date," +
 //      "pi.npr_benefitterm term," +
 //      "to_char(TO_DATE(pi.nph_birthdate,'DD/MM/YYYY')) date_of_birth," +
 //      "pi.np2_ageprem age," +
 //      "pi.npr_premiumter term_of_assurance," +
 //      "to_char(TO_DATE(pi.npr_maturitydate,'DD/MM/YYYY')) maturity_date," +
 //      "pi.np1_creditbalance amount_in_suspense," +
 //      "case " +
 //       " when pi.np2_substandar <> 'Y' then " +
 //        " 'Standard' " +
 //       " else " +
 //        "       'Sub-Standard' " +
 //      " end np2_substandar, " +
 //      " pi.npr_sumassured sum_assured, " +
 //      " case " +
 //       " when cmo_mode = 'M' then " +
 //       "  'Monthly' " +
 //       " when cmo_mode = 'Q' then " +
 //       "  'Quarterly' " +
 //       " when cmo_mode = 'H' then " +
 //        " 'Half yearly' " +
 //        " else " +
 //          "     'Yearly' " +
 //      " end as policy_Mode, " +
 //      " nvl(pi.np2_totpremium, 0) + nvl(pi.np2_totload, 0) installment_premium, " +
 //      " pi.cst_descr, " +
 //      " pi.cfr_desc cfr_option, " +
 //      " to_char(TO_DATE(pi.np2_nextduedat,'DD/MM/YYYY')) np2_nextduedat, " +
 //      " to_char(TO_DATE(pi.np1_issuedate,'DD/MM/YYYY'))  np1_issuedate ," +
 //      "pi.aag_name," +
 //      " substr(pi.nph_idno, 1, 5) || '-' || substr(pi.nph_idno, 6, 7) || '-' || " +
 //      " substr(pi.nph_idno, 13, 1) as cnic " +
 // //"from view_Api_PolicyInfo pi " +

 // "FROM view_policyinfo_aftersale pi " + // for after sale portal
 // " where case when substr('" + Search + "', 1, 3) = 'POL'" +
 // " then pi.np1_policyno when substr('" + Search + "', 1, 3) = 'PRO'" +
 // " then pi.np1_proposal when substr('" + Search + "', 1, 3) = 'NIC'" +
 // " then pi.nph_idno end = substr('" + Search + "', 4) " +
 //" order by pi.nph_idno, pi.np1_policyno ";

 //           dt = GetdataOraOledb(Sql);


 //           if (dt.Rows.Count > 0)
 //           {
 //               PnlView.Visible = true;
 //               PnlMultiRec.Visible = true;
 //               lblError.Text = "";
 //               lblPol.Text = dt.Rows[0]["policyno"].ToString();
 //               lblCNIC.Text = dt.Rows[0]["CNIC"].ToString();
 //               lblPro.Text = dt.Rows[0]["Proposal"].ToString();
 //               lblZone.Text = dt.Rows[0]["Zone"].ToString();

 //               lblName.Text = dt.Rows[0]["fullname"].ToString();
 //               lblGender.Text = dt.Rows[0]["gender"].ToString();
 //               lblDob.Text = dt.Rows[0]["date_of_birth"].ToString();
 //               lblAge.Text = dt.Rows[0]["Age"].ToString();

 //               lblIssueDate.Text = dt.Rows[0]["np1_issuedate"].ToString();
 //               lblDueDate.Text = dt.Rows[0]["np2_nextduedat"].ToString();
 //               lblMaturityDate.Text = dt.Rows[0]["maturity_date"].ToString();
 //               lblRiskDate.Text = dt.Rows[0]["risk_date"].ToString();
 //               lblSum.Text = Convert.ToDouble(dt.Rows[0]["sum_assured"].ToString()).ToString("#,##");

 //               lblTermIssue.Text = dt.Rows[0]["term_of_assurance"].ToString();
 //               lblTerm.Text = dt.Rows[0]["TERM"].ToString();
 //               lblPremimum.Text = Convert.ToDouble(dt.Rows[0]["installment_premium"].ToString()).ToString("#,##");

 //           }
 //           else
 //           {
 //               PnlView.Visible = false;

 //               lblError.Text = "Policy not issued";
 //               PnlMultiRec.Visible = false;

 //           }


        }

            

        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSNILAS"]);
            return conOra;


        }

        protected void btnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDateFrom.Text.ToString() == "" || txtDateTo.Text.ToString() == "")
                {
                    lblReport.Text = "Please provide date";
                }
                else
                {
                    lblReport.Text = "";
                    string user = System.Convert.ToString(Session["s_USE_USERID"]);
                    if (SessionObject.Get("s_AAG_NAME").ToString().ToUpper() == "ADMINISTRATOR")
                    {
                        Sql = "SELECT PROPOSAL_NO, POLICY_NO, POLICY_STATUS, ACTIVITY_CODE, ACTIVITY_DESC, ENTERED_USER, ENTERED_DATE, rso_employeeno TRACKING#, ccd_descr BANK_NAME, ccs_descr BRANCH_NAME \n " + 
                                    "FROM Sales_Portal_Report where trunc(ENTERED_DATE) between to_Date('" + txtDateFrom.Text + "', 'dd/MM/yyyy') and to_Date('" + txtDateTo.Text + "','dd/MM/yyyy')";

                        lblReport.Text = "";

                        dt = GetEmail(Sql);
                        if (dt.Rows.Count > 0)
                        {
                            lblReport.Text = "Report successfully download in Excel.";
                            ExportGridToExcel(dt);
                            //  Response.Write("");
                            lblReport.Text = "Report successfully download in Excel.";
                        }
                        else
                        {
                            lblReport.Text = "No Records Found...";
                        }
                    }


                    else
                    {
                        lblReport.Text = "";
                        Sql = "SELECT PROPOSAL_NO, POLICY_NO, POLICY_STATUS, ACTIVITY_CODE, ACTIVITY_DESC, ENTERED_USER, ENTERED_DATE, rso_employeeno TRACKING#, ccd_descr BANK_NAME, ccs_descr BRANCH_NAME \n " +
                                     "FROM Sales_Portal_Report  where trunc(ENTERED_DATE) between to_Date('" + txtDateFrom.Text + "', 'dd/MM/yyyy') and to_Date('" + txtDateTo.Text + "','dd/MM/yyyy')\n " +
                                     "and ENTERED_USER = '" + user + "'  ";

                        dt = GetEmail(Sql);
                        if (dt.Rows.Count > 0)
                        {
                            lblReport.Text = "Report successfully download in Excel.";
                            ExportGridToExcel(dt);
                            //  Response.Write("");
                            lblReport.Text = "Report successfully download in Excel.";
                        }

                        else
                        {
                            lblReport.Text = "No Records Found...";
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                lblReport.Text = ex.Message;
            }
            



        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PnlReport.Visible = true;
        }

        private OleDbConnection GetConnForEmail ()
        {
            OleDbConnection conOraEmail = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOraEmail;


        }

      

        public string DML(string sql)
        {
            try
            {
                CmdDML.CommandText = sql;
                CmdDML.Connection = GetConnForEmail();
                CmdDML.Connection.Open();
                CmdDML.ExecuteNonQuery();
                CmdDML.Connection.Close();

                return "done";
            }

            catch (Exception ex)
            {

                CmdDML.Connection.Close();
                return ex.Message;

            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        public DataTable GetdataOraOledb(string sql)
        {

            //  cmd.Connection = conOra; old
            cmd.Connection = GetConn();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Reset();
            dt.Clear();

            da.Fill(dt);
            //cmd.Dispose(); old
            cmd.Connection.Close();

            return dt;


        }
        public DataTable GetEmail(string sql)
        {

            //  Function for get Email To,CC,BCC
            cmd.Connection = GetConnForEmail();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter daEmail = new OleDbDataAdapter(cmd);
            DataTable dtEmail = new DataTable();
            dtEmail.Reset();
            dtEmail.Clear();

            daEmail.Fill(dtEmail);
            //cmd.Dispose(); old
            cmd.Connection.Close();

            return dtEmail;


        }
        void RenameAndDownloadPDF()
        {
            string originalFilePath = Server.MapPath("~/Presentation/FormPdf/AlterationForm.pdf");
            string renamedFilePath = Server.MapPath("~/Presentation/FormPdf/" + Session["PolicyNo"].ToString() + ".pdf");


            // Rename the file
            if (File.Exists(originalFilePath))
            {
                File.Copy(originalFilePath, renamedFilePath, true);
            }

            // Generate the download link
            string downloadUrl = ResolveUrl("~/Presentation/FormPdf/" + Session["PolicyNo"].ToString() + ".pdf");
            DownloadLink.NavigateUrl = downloadUrl;
            DownloadLink.Text ="Policy Alteration Form_" + Session["PolicyNo"].ToString() + ".pdf";
        }

        void SaveData()
        {
            //Session["AltTypeCode"]
            Sql = "insert into larq_request (pfs_acntyear, arq_requestype, arq_requestno, use_userid, np1_proposal, use_timedate, arq_reqdate, arq_auto,OAC_ACTIVICODE,RSO_EMPLOYEENO) " +

        " Values((select A.PFS_ACNTYEAR from SLILASPRD.PFS_FISCALYR A WHERE  A.PFS_CURPRVNXT = 'C'),'RALT'," +
    " (select max(a.arq_requestno) + 1 as Req_no from larq_request a " +
    " where a.arq_requestype = 'RALT' " +
    " and a.pfs_acntyear = (select b.PFS_ACNTYEAR " +
    " from   SLILASPRD.PFS_FISCALYR b " +
    " WHERE b.PFS_CURPRVNXT = 'C' )), " +
  "'" + SessionObject.Get("s_AAG_NAME").ToString() + "','" + Session["ProposalNo"].ToString() + "',sysdate,(SELECT TRUNC(SYSDATE) AS current_date FROM dual)," + 
  "'A','" + Session["AltTypeCode"].ToString() + "','" + Session["TrackingNo"].ToString() + "')";

            
            string QryResult = DML(Sql);

            Session["DataSaveResult"] = QryResult;
           




        }

        private void ExportGridToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                lblReport.Text = "";                
                //string filename = "Alteration_Request_List_" + System.Convert.ToString(Session["s_USE_USERID"]) + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
                string filename = "Alteration_Request_List_" + System.Convert.ToString(Session["s_USE_USERID"]) + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write("<b><u>Policy Alteration Report</u></b>");
                Response.Write("<br />");
                //  Response.Write(Label28.Text + " " + lblDateFrom.Text + "  To  " + lblDateTo.Text + "<br />");
                Response.Write("<br />");
                //Result detail

                Response.Write(tw.ToString());
                Response.End();
            }



        }

        public void ProcessAttachmentsAndSendEmail()
        {
            // List to keep track of file paths for deletion later.
            List<string> savedFilePaths = new List<string>();

            // Create a MailMessage (make sure to dispose it later)
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(); // Configure your SMTP settings

            try
            {
                // Create an array (or list) of FileUpload controls.
                // Adjust these names based on your actual controls.
                FileUpload[] uploadControls = new FileUpload[]
                {
            NonFUploadPolDoc,
            NonFUploadAlterForm,
            NonFUploadBenCNIC,
            NonFUploadCustReq,
            NonFUploadGuarCNIC
                };

                // Loop through each FileUpload control.
                foreach (FileUpload fu in uploadControls)
                {
                    if (fu.HasFile)
                    {
                        // Get just the file name.
                        string fileName = System.IO.Path.GetFileName(fu.FileName);
                        // Create a path to save the file. (Adjust the folder as needed.)
                        string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                        try
                        {
                            // Save the file to disk.
                            fu.SaveAs(filePath);
                            // Add the saved file path for later deletion.
                            savedFilePaths.Add(filePath);

                            // Create an attachment from the saved file.
                            // We do not wrap this in a using block because we want the attachment
                            // to remain available until the email is sent.
                            Attachment attachment = new Attachment(filePath);
                            mail.Attachments.Add(attachment);
                        }
                        catch (IOException ioEx)
                        {
                            // For example, if the file is in use, report it.
                            ///  int errorCode = ioEx.HResult & 0xFFFF;
                            //   if (errorCode == 32) // ERROR_SHARING_VIOLATION
                            //   {
                            // Optionally log or show a meaningful message.
                            // You might want to delete any file that was partially saved.
                            //       if (File.Exists(filePath))
                            //    //  {
                            try { File.Delete(filePath); } catch { }
                            //    //   }
                            throw new Exception("The file " + fileName + " is currently in use. Please close it and try again.", ioEx);

                            // }
                            // else
                            // {
                            throw;
                            // }
                        }
                    }
                }

                // Add additional email properties (subject, body, recipients, etc.)
                mail.From = new MailAddress("ILASADMIN@gba.com.pk", "Branch officer");
                mail.ReplyToList.Add(txtEmailNonFin.Text.ToString());

                ToEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_TO FROM USE_USERMASTER a WHERE a.Email_Flag = 'T'").Rows[0]["USER_EMAIL_TO"].ToString();
                CCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_CC FROM USE_USERMASTER a WHERE a.Email_Flag = 'C'").Rows[0]["USER_EMAIL_CC"].ToString();
                BCCEmail = GetEmail("SELECT LISTAGG(a.USER_EMAIL, ',') WITHIN GROUP(ORDER BY a.USER_EMAIL) AS USER_EMAIL_BCC FROM USE_USERMASTER a WHERE a.Email_Flag = 'B'").Rows[0]["USER_EMAIL_BCC"].ToString();

                if (!string.IsNullOrEmpty(ToEmail))
                {
                    mail.To.Add(ToEmail);
                }

                if (!string.IsNullOrEmpty(CCEmail))
                {
                    mail.CC.Add(CCEmail);
                }

                if (!string.IsNullOrEmpty(BCCEmail))
                {
                    mail.Bcc.Add(BCCEmail);
                }


                mail.Subject = "Policy Alteration Request (Non Financial) Tracking # " + txtTrackingNoNonFin.Text + " Policy # " + Session["PolicyNo"].ToString();

                mail.Body = "Email sent from:" + txtEmailNonFin.Text.ToString() + "<br /> <br /> <b>  Please find attached and Proceed. </b> <br /><br /> " +
                    "Tracking # " + txtTrackingNoNonFin.Text;

                smtpClient.Host = "110.93.216.180"; // for local
                smtpClient.Port = 27;
                smtpClient.Credentials = new System.Net.NetworkCredential("ILASADMIN@gba.com.pk", "G?A=b@D@t4");

                smtpClient.EnableSsl = false;

                smtpClient.Send(mail);
                SaveData();
                lblNonFinSubmitError.Text = "The request has been successfully sent via email.";
                foreach (string path in savedFilePaths)
                {
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); } catch { }
                    }
                }

                // Dispose the MailMessage (which disposes all attachments)
                mail.Dispose();

            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed.
                // For example, display an error message:
                lblNonFinSubmitError.Text = "An error occurred: " + ex.Message;

            }
            finally
            {
                // Clean up: delete the saved files.
                foreach (string path in savedFilePaths)
                {
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); } catch { }
                    }
                }

                // Dispose the MailMessage (which disposes all attachments)
                mail.Dispose();
            }
        }

        #endregion

        // Undispatch work





    }
}