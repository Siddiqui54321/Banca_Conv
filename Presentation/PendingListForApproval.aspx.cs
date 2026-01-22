using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise.Presentation;
using SHAB.Data;
using SHAB.Business;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Text;

namespace SHAB.Presentation
{
    /// <summary>
    /// Summary description for ManualPolicyIssuanceOfPendingProposals.
    /// </summary>
    public partial class PendingListForApproval : SHMA.Enterprise.Presentation.TwoStepController
    {
        #region " First Step "		 

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            LastDownloadLinkPath();
            dGrid.DataSource = getDataSource(dataHolder);
            dGrid.DataBind();
            return dataHolder;

        }

        sealed protected override void BindInputData(SHMA.Enterprise.DataHolder dataHolder)
        {
            CSSLiteral.Text = ace.Ace_General.loadMainStyle();

        }

        protected override void PrepareInputUI(DataHolder dataHolder)
        {
        }

        #endregion

        #region " Second Step "



        protected override void DataBind(DataHolder dataHolder)
        {
            /*string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);			
			dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();			
			
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
			new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("F");
			DataTable proposalDT = dataHolder["LNP1_POLICYMASTR_DATA"];m

			try
			{													
				WriteFile(proposalDT);					
			}
			catch(Exception exc)
			{
				errMessage.Append(exc.Message);
			}				

			if(errMessage.Length != 0)
			{
				showAlertMessage(errMessage.ToString());					
			}*/
        }
        public string addStringCVS(string str, string val)
        {
            if (str == "")
                str = val;
            else
                str = str + "," + val;
            return str;
        }

        protected override void ApplyDomainLogic(DataHolder dataHolder)
        {
            string proposal = string.Empty;
            string status = string.Empty;
            string comments = string.Empty;
            string commdate = string.Empty;
            string cvsProposalNos = string.Empty;
            System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
            dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
            LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);

            string ChannelCode = System.Convert.ToString(Session["s_CCH_CODE"]);
            string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);

            if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            {
                foreach (DataGridItem item in dGrid.Items)
                {
                    status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;

                    proposal = ((Label)item.FindControl("lblProposal")).Text;
                    comments = ((TextBox)item.FindControl("txtComments")).Text;
                    commdate = ((TextBox)item.FindControl("txtPostDate")).Text;

                    if (status == ".") { continue; }//Nothing

                    if (status == "Y")//Ok
                    {
                        //chg_closing
                        //if (Convert.ToString(Session["ClossingFlag"]) == "P")
                        //{
                        //	if (Convert.ToDateTime(commdate).ToString("dd/MM/yyyy") != Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).ToString("dd/MM/yyyy"))
                        //	{
                        //		showAlertMessage("Please enter previous Month's Last Date");
                        //		return;
                        //	}
                        //}
                        //else
                        //{
                        //chg_closing_end
                        if (commdate == null || commdate == "")
                        {
                            showAlertMessage("Please Enter Post Date.");
                            return;
                        }
                        if (Convert.ToDateTime(commdate) < DateTime.Now.AddMonths(-1))
                        //if (Convert.ToDateTime(commdate) < Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).AddMonths(-1)) //chg_closing	comments above
                        {
                            showAlertMessage("Post Date cannot be less than 1 month old.");
                            return;
                        }
                        if (Convert.ToDateTime(commdate).Date > DateTime.Now.Date)
                        //if (Convert.ToDateTime(commdate).Date > Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).Date) //chg_closing	comments above
                        {
                            showAlertMessage("Post Date cannot be more than today.");
                            return;

                        }

                        //if(ChannelCode=="2" && bankCode=="2" )// bancassurance and FWB
                        if (ace.Ace_General.IsPaymentFromYFile(bankCode))
                        {
                            status = "Y-FromFile";          // this is just because in in bussiness its  identification of OK at BO or BSO level 
                            commdate = ace.Ace_General.getGeneratedCommencementDate(DateTime.Now).ToString("dd/MM/yyyy");
                        }
                        else
                        {                                   // bancassurance and UBL
                                                            //status = "F";
                            status = "Y-FromFile";          // this is just because in in bussiness its  identification of OK at BO or BSO level 
                            commdate = ace.Ace_General.getGeneratedCommencementDate(Convert.ToDateTime(commdate)).ToString("dd/MM/yyyy");
                        }
                        cvsProposalNos = addStringCVS(cvsProposalNos, proposal);
                    }
                    //Not Ok
                    //Need to remove two Levels BM (Compliance Level) and Collection File uploading (CP User File uploading only as we are doing in FWBL) 
                    //from UBL, BAL & FWBL 
                    else if (status == "N")
                    {
                        string BOPSFlow = ConfigurationSettings.AppSettings["BOPSFlow"].ToString();
                        string CompanyFlow = ConfigurationSettings.AppSettings["CompanyFlow"].ToString();
                        if (BOPSFlow.Contains(Convert.ToString(SessionObject.GetString("s_CCD_CODE"))) && SessionObject.GetString("s_CCH_CODE") == "2")
                        {
                            status = "null";
                        }
                        else if (CompanyFlow.Contains(Convert.ToString(SessionObject.GetString("s_CCD_CODE"))) && SessionObject.GetString("s_CCH_CODE") == "2")
                        {
                            status = "C";
                        }
                        else
                        {
                            status = "P";
                        }
                    }

                    try
                    {
                        if (status == "Y" || status == "Y-FromFile")
                        {
                            SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(commdate));
                            LNP1_POLICYMASTR.markStatus(proposal, status);
                            cmObj.AddComentsInTable(proposal, comments, status);
                        }
                        else
                        {
                            LNP1_POLICYMASTR.markStatus(proposal, status);
                            cmObj.AddComentsInTable(proposal, comments, status);
                        }

                    }
                    catch (Exception ex)
                    {
                        errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                        errMessage.Append("\\n");
                    }
                }

                dGrid.DataSource = getDataSource(dataHolder);
                dGrid.DataBind();

                //if(ChannelCode=="2" && bankCode=="1" )// bancassurance and UBL
                if (!ace.Ace_General.IsByPassDownLoadFile(bankCode))// bancassurance and UBL
                {
                    if (cvsProposalNos != string.Empty)
                    {
                        IDataReader dr = LNP1_POLICYMASTRDB.GetProposalForExcelReport(cvsProposalNos);
                        DataTable dt = new DataTable();
                        Utilities.Reader2Table(dr, dt);
                        dr.Close();
                        WriteExcel(dt);
                        WriteFileOnClient();
                        LastDownloadLinkPath();
                        //SetDownloadLink(ConfigurationSettings.AppSettings["downloadFileName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeUblFilePath"],GenerateFileType.UBL.ToString());
                    }
                }
            }
            else
            {
                showAlertMessage("There is nothing to save.");
            }
        }

        public void SetDownloadLink(string FileName)
        {

            Response.Clear();
            Response.ContentType = "Application/.xls";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            //Response.TransmitFile(downloadProposalFile);
            Response.WriteFile(FileName);
            Response.Flush();
            //DeleteFile(FileName);
            Response.End();



        }


        #endregion

        #region " Supported Methods "

        private void showAlertMessage(string message_)
        {
            HeaderScript.Text = "alert(\"" + message_ + "\");";
        }
        private DataTable getDataSource(DataHolder ds)
        {
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);
            string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
            string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);

            //ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("R");
            ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("R", user, userType, bankCode, branchCode, false);

            string Comments = "Re-Calculate Premium as Client Age has been Changed";
            string Status = "NOT OK";
            string proposal = "";

            //Remove Proposals In Which Client Age has been Changed
            for (int i = 0; i < ds["LNP1_POLICYMASTR_DATA"].Rows.Count; i++)
            {
                proposal = ds["LNP1_POLICYMASTR_DATA"].Rows[i]["np1_proposal"].ToString();
                bool IsClientAgeChanged = LNP1_POLICYMASTRDB.CheckAge(proposal);
                if (!IsClientAgeChanged)
                {
                    try
                    {
                        LNP1_POLICYMASTR.markStatus(proposal, "ReCal"/*status*/);
                        LNCM_COMMENTSDB.AddUserComments(proposal, Comments, Status);
                        ds["LNP1_POLICYMASTR_DATA"].Rows[i].Delete();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return ds["LNP1_POLICYMASTR_DATA"];
        }

        private void WriteExcel(DataTable dt)
        {

            try
            {
                string portotypeFilePath = Server.MapPath(ConfigurationSettings.AppSettings["prototypeFilePath"]);
                string dfn = ConfigurationSettings.AppSettings["downloadFileName"];
                string[] FileName = dfn.Split('.');
                string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"] + "\\" + FileName[0].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");

                Session.Remove("downloadProposalFile");
                Session.Add("downloadProposalFile", downloadProposalFile);

                File.Copy(portotypeFilePath, downloadProposalFile, true);
                string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + downloadProposalFile + ";Extended Properties=" + "\"" + "Excel 12.0;HDR=YES;READONLY=FALSE" + "\"";
                OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
                string ExcelQuery = null;
                ExcelQuery = "SELECT * FROM [Sheet1$]";
                OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
                ExcelConnection.Open();
                //ExcelQuery = "UPDATE [Sheet1$] SET A='1', B='3', C='Update'";
                //ExcelQuery = "INSERT INTO [Sheet1$B5:B5] (F1) VALUES ('1')";
                foreach (DataRow dr in dt.Rows)
                {
                    ExcelQuery = "INSERT INTO [Sheet1$] ([Transaction Identifier], [From Branch Code], [From Account No], " +
                        "[From Account Type], [From CLSL], [From Account Currency], [To Branch Code], [To Account No], " +
                        "[To Account Type], [To Account Currency], [To CLSL], [Amount], [Transaction Currency], [Value Date], " +
                        "[User Narration], [Instrument No], [From CRC], [To CRC], [Wht Flag], [Tran_code]) " +
                        "VALUES (" +
                        "'" + dr[0].ToString() + "', '" + dr[1].ToString() + "', '" + dr[2].ToString() + "', '" + dr[3].ToString() + "', " +
                        "'" + dr[4].ToString() + "', '" + dr[5].ToString() + "', '" + dr[6].ToString() + "', '" + dr[7].ToString() + "', " +
                        "'" + dr[8].ToString() + "', '" + dr[9].ToString() + "', '" + dr[10].ToString() + "', '" + dr[11].ToString() + "', " +
                        "'" + dr[12].ToString() + "', '" + dr[13].ToString() + "', '" + dr[14].ToString() + "', '" + dr[15].ToString() + "', " +
                        "'" + dr[16].ToString() + "', '" + dr[17].ToString() + "', '" + dr[18].ToString() + "', '" + dr[19].ToString() + "')";
                    ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
                    ExcelCommand.ExecuteNonQuery();
                }
                ExcelConnection.Close();

                LNP1_POLICYMASTRDB.UpdateLastFileName(Path.GetFileName(downloadProposalFile));
            }
            catch (Exception ex)
            {
                StringBuilder errMessage = new StringBuilder();
                errMessage.Append(ex.Message);
            }
        }

        public void LastDownloadLinkPath()
        {
            string FileName = LNP1_POLICYMASTRDB.GetLastUpdatedFileName();
            string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"] + "\\" + FileName);

            if (!File.Exists(downloadProposalFile))
            {
                hlbanca.Enabled = false;
                hlbanca.NavigateUrl = "#";
            }
            else
            {
                hlbanca.Enabled = true;
                hlbanca.Attributes.Add("onClick", "DownloadFile('" + Path.GetFileName(downloadProposalFile) + "');");
            }

        }



        private void WriteFileOnClient()
        {
            /*
			try
			{
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+ConfigurationSettings.AppSettings["downloadFileName"]);
				string dfn = ConfigurationSettings.AppSettings["downloadFileName"];
				string attachment = "attachment; filename=downloadProposalStatus.xls";
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
				HttpContext.Current.Response.WriteFile(downloadProposalFile);
				HttpContext.Current.Response.AddHeader("Pragma", "public");
				HttpContext.Current.Response.Write(dfn.ToString());
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
			catch(Exception ex){
				string a = ex.Message;
			}
			*/
            string popupScript = " window.open('DownloadProposalFile.aspx','DownloadExcelFile','width=1,height=1,location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,top=1000');";

            callJs.Text = popupScript;

        }

        #endregion

        #region " Events "

        protected void _CustomEvent_ServerClick(object sender, System.EventArgs e)
        {
            ControlArgs = new object[1];
            switch (_CustomEventVal.Value)
            {
                case "Update":
                    ControlArgs[0] = EnumControlArgs.Update;
                    DoControl();
                    break;
                case "Save":
                    ControlArgs[0] = EnumControlArgs.Save;
                    DoControl();
                    break;
                case "Delete":
                    ControlArgs[0] = EnumControlArgs.Delete;
                    DoControl();
                    break;
                case "Filter":
                    ControlArgs[0] = EnumControlArgs.Filter;
                    DoControl();
                    break;
                case "Process":
                    ControlArgs[0] = EnumControlArgs.Process;
                    DoControl();
                    break;

            }
            _CustomEventVal.Value = "";
        }


        private void dGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
                string porposal = ((DataRowView)(e.Item.DataItem)).Row["NP1_PROPOSAL"].ToString();
                IDataReader temp = cmt.getCommentsOfPorposal(porposal);

                Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
                repAllComments.DataSource = temp;
                repAllComments.DataBind();

                temp.Close();

                if (repAllComments.Items.Count == 0)
                    repAllComments.Visible = false;

                //e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
                //e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
                e.Item.Attributes.Add("onmouseout", "hideComments(" + porposal + ")");

                //LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
                //btn.Attributes.Add("onclick","setValue('"+btn.Text+"');executeReport('PROFILE');");
                //btn.Text


                string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
                if (ace.Ace_General.IsPaymentFromYFile(bankCode))
                {
                    //					foreach(DataGridItem item in dGrid.Items)
                    //					{
                    ((TextBox)e.Item.FindControl("txtPostDate")).Text = ace.Ace_General.getGeneratedCommencementDate(DateTime.Now).ToString("dd/MM/yyyy");
                    //					}
                }

            }
        }

        #endregion

        #region " Class Variable "

        protected System.Web.UI.WebControls.Literal _result;
        protected System.Web.UI.WebControls.Literal HeaderScript;
        protected System.Web.UI.WebControls.HyperLink hlk;
        protected System.Web.UI.WebControls.Literal callJs;

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    //Disabled by Rizwanon19062021 to as DataGrid was re-initilalzing on BOP230000
             //this.dGrid.ItemDataBound+=new DataGridItemEventHandler(dGrid_ItemDataBound);

        }

        private void dgrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dGrid.CurrentPageIndex = e.NewPageIndex;
            dGrid.DataBind();
        }

        protected void dGrid_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            dGrid_ItemDataBound(sender, e);
        }
        #endregion

        /*private void dGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string status = ((DataRowView)(e.Item.DataItem)).Row["np1_selected"].ToString();
				DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
				ddlStatus.Enabled = status.Equals("D");

			}
		}*/
    }
}
