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

namespace Bancassurance.Presentation
{
    /// <summary>
    /// Summary description for DataReports.
    /// </summary>
    public partial class PolicyIssuanceReport : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Literal callJs;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected System.Web.UI.WebControls.HyperLink hlIlasMisTextFile;
        protected System.Web.UI.WebControls.Button btngenerateIlasMisTextfile;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
                this.txtDATEFROM.Text = "01/" + sysDate.Month + "/" + sysDate.Year;
                this.txtDATETO.Text = sysDate.Day + "/" + sysDate.Month + "/" + sysDate.Year;
                BindUsers();
                Session["PageReloadTime"] = 1000;
            }
        }

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

        private void InitializeComponent()
        {

        }
        #endregion


        public void SetDownloadLink(System.Web.UI.WebControls.HyperLink hlcrtl, string FileName, string prototype, string FileType)
        {
            string portotypeFilePath = Server.MapPath(prototype);
            string dfn = FileName;
            string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"] + "\\" + FileName);

            if (!File.Exists(downloadProposalFile))
            {
                hlcrtl.Enabled = false;
                hlcrtl.NavigateUrl = "#";
            }
            else
            {
                hlcrtl.Enabled = true;
                hlcrtl.NavigateUrl = downloadProposalFile;

                Response.Clear();
                Response.ContentType = "Application/.xls";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                //Response.TransmitFile(downloadProposalFile);
                Response.WriteFile(downloadProposalFile);
                Response.Flush();
                DeleteFile(downloadProposalFile);
                Response.End();

            }

        }

        public void DeleteFile(string Path)
        {
            if (Path != "")
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
            }
        }


        public void BindUsers()
        {
            string UserID = Session["s_USE_USERID"].ToString().ToUpper();
            string UserType = SessionObject.GetString("s_USE_TYPE");
            if (UserID == "ADMIN")
            {
                IDataReader use_userMaster0 = SHAB.Data.USE_USERMASTERDB.GetDDL_Users_data();
                ddl_Users.DataSource = use_userMaster0;
                ddl_Users.DataBind();
                use_userMaster0.Close();
                ddl_Users.Items.Insert(0, new ListItem("All Users", ""));
            }

        }
        private void WriteExcel(DataTable dt1, string FileName, string prototype, string FileType)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName));
                Response.ContentType = "application/ms-excel";
                DataTable dt = dt1;
                string str = string.Empty;
                foreach (DataColumn dtcol in dt.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                SessionObject.Set("DownloadCompleted","True");
                Response.End();
            }
            catch (Exception)
            {
            }
           
        }

        protected void btnGenerateExcel_Click(object sender, System.EventArgs e)
        {
            string UserCODE = "";
            if (Session["s_USE_USERID"].ToString().ToUpper() == "ADMIN")
            {
                

                if (ddl_Users.SelectedItem.Text == "All Users")
                {
                    UserCODE = "ALL";
                }
                else
                {
                    UserCODE = ddl_Users.SelectedValue;
                }

                DataTable dt = LNP1_POLICYMASTRDB.GetExcelReportForPolicyIssuance(txtDATEFROM.Text, txtDATETO.Text, UserCODE);

                WriteExcel(dt, "PolicyIssuance" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls", "UploadedFiles\\PolicyIssuanceFile.xls", GenerateFileType.PolicyIssuance.ToString());
                //WriteFileOnClient("DownloadUblFile.aspx");
                
            }
        }
        protected void btnTextFile_Click(object sender, System.EventArgs e)
        {
            string BANKCODE = "";
            string BRANCHCODE = Session["s_CCS_CODE"].ToString();
            bool IsAdmin = false;

            if (Session["s_USE_USERID"].ToString().ToUpper() == "ADMIN")
            {
                IsAdmin = true;
            }
            //if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
            //{
            //	BANKCODE="ALL";
            //}
            //else
            //{
            //	BANKCODE=ddlCCS_CODE.SelectedValue;
            //}

            string DateType = "IssueDate";
            if (ddlDate.SelectedValue == "ProposalDate")
            {
                DateType = "ProposalDate";
            }

            DataTable dt = null;
            WriteTextFile(dt);
        }


        private void WriteTextFile(DataTable dt)
        {
            string attachment = "attachment; filename=ExportProposalList.txt";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text";
            //			HttpContext.Current.Response.AddHeader("Pragma", "public");

            //			DataTable dt = new DataTable();
            //			dt.Columns.Add("ID");
            //			dt.Columns.Add("Name");
            //			DataRow workRow;
            //			for (int x = 0; x <= 5; x++) 
            //			{
            //				workRow = dt.NewRow();
            //				workRow[0] = x;
            //				workRow[1] = "CustName" + x.ToString();
            //				dt.Rows.Add(workRow);
            //			}

            //			int iCol = dt.Columns.Count-1;
            //			int iRow = dt.Rows.Count;
            ////			WriteColumnName();
            //			for(int j = 0; j < iRow; j++)
            //			{
            //				DataRow dRow = dt.Rows[j];				
            //				for (int i = 0; i < iCol; i++)
            //				{					
            //					if (!Convert.IsDBNull(dRow[i]))
            //					{
            //						HttpContext.Current.Response.Write(dRow[i].ToString());
            //					}
            //					if ( i < iCol - 1)
            //					{
            //						HttpContext.Current.Response.Write(",");
            //					}
            //				}
            //				HttpContext.Current.Response.Write(Environment.NewLine);
            //			}			

            HttpContext.Current.Response.Write(exportData(dt));

            HttpContext.Current.Response.End();
        }

        private string exportData(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    //					if(col.DataType == typeof(System.DateTime))
                    //					{						
                    //						sb.Append(row[col].ToString());
                    //					}
                    //					else 
                    //					{			
                    if (col.ColumnName != "NP1_PROPDATE")
                    {

                        if ("" == "Pending")
                        {
                            if (col.ColumnName == "ISSUEDDATE" || col.ColumnName == "COMMENCEMENTDATE"
                                || col.ColumnName == "MATURITYDATE" || col.ColumnName == "NEXTDUEDATE")
                            {
                                sb.Append("");
                                sb.Append("|");
                            }
                            else
                            {
                                sb.Append(row[col].ToString());
                                sb.Append("|");
                            }
                        }
                        else
                        {
                            if (col.ColumnName == "ISSUEDDATE")
                            {
                                if (row[col] != null && row[col].ToString() != "")
                                {
                                    sb.Append(Convert.ToDateTime(row[col]).Date.ToString("MM/dd/yyyy"));
                                    sb.Append("|");
                                }
                                else
                                {
                                    sb.Append("");
                                    sb.Append("|");

                                }
                            }
                            else
                            {
                                sb.Append(row[col].ToString());
                                sb.Append("|");

                            }
                        }
                    }

                    //					}
                    //					if(col.Ordinal < dt.Columns.Count - 1)
                    //					{
                    //						sb.Append(",");
                    //					}
                }
                sb.Append(Environment.NewLine);
            }
            sb.Replace("00:00:00", "");

            return sb.ToString();
        }

        enum GenerateFileType
        {
            Banca = 1,
            UBL = 2,
            Ilas = 3,
            DataDump = 4,
            IlasMis = 5,
            PolicyIssuance=6
        };


    }
}
