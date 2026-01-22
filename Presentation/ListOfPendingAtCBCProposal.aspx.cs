using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SHMA.Enterprise.Presentation;
using SHAB.Data;
using SHAB.Business;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using System.Data.OleDb;
using System.Configuration;

namespace SHAB.Presentation
{
    /// <summary>
    /// Summary description for ManualPolicyIssuanceOfPendingProposals.
    /// </summary>
    public partial class ListOfPendingAtCBCProposal : SHMA.Enterprise.Presentation.TwoStepController
    {
        #region " First Step "		 

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            //this.dt = getDataSource(dataHolder);			
            //dGrid.DataSource = this.dt;

            //this.dt = getDataSource(dataHolder);			
            //dGrid.DataSource = this.dt;
            //dGrid.DataBind();
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
            //string user = System.Convert.ToString(Session["s_USE_USERID"]);
            //string userType = ace.Ace_General.getUserType(user);			
            //this.dt = getDataSource(dataHolder);			
            dGrid.DataSource = this.dt;
            //dGrid.DataSource = getDataSource(dataHolder);
            dGrid.DataBind();
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
                //LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
                //string porposal = ((DataRowView)(e.Item.DataItem)).Row["NP1_PROPOSAL"].ToString();
                //IDataReader temp = cmt.getCommentsOfPorposal(porposal);

                //Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
                //repAllComments.DataSource = temp;
                //repAllComments.DataBind();

                //temp.Close();

                //if(repAllComments.Items.Count==0)
                //	repAllComments.Visible=false;

                ////e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
                ////e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
                //e.Item.Attributes.Add("onmouseout","hideComments("+porposal+")");

                //LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
                //btn.Attributes.Add("onclick","setValue('"+btn.Text+"');executeReport('PROFILE');");
                //btn.Text
            }
        }

        #endregion

        #region " Class Variable "

        protected System.Web.UI.WebControls.Literal _result1;
        protected System.Web.UI.WebControls.Literal HeaderScript1;
        private DataTable dt = null;

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
        {
            //this._CustomEvent.ServerClick += new System.EventHandler(this._CustomEvent_ServerClick);
            this.dGrid.ItemDataBound += new DataGridItemEventHandler(dGrid_ItemDataBound);

        }

        private DataTable getCBCData()
        {
            try
            {
                string user = System.Convert.ToString(Session["s_USE_USERID"]);
                string userType = ace.Ace_General.getUserType(user);
                string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
                string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);
                string fromdate = txtDATEFROM.Text;
                string todate = txtDATETO.Text;
                string query = "select nvl(p1.np1_selected, 'D') np1_selected,\n" +
               "       cm.cm_commentdate,\n" +
               "       p1.np1_proposal,\n" +
               "       TO_CHAR(p1.np1_propdate, 'MM/DD/YY') np1_propdate,pr.ppr_descr plan,p2.use_datetime,\n" +
               "       TO_CHAR(p2.np2_commendate, 'MM/DD/YY') np2_commendate,\n" +
               "       ph.nph_fullname,\n" +
               "       ph.nph_idno,\n" +
               "       ad.nad_mobile,\n" +
               "       p1.np1_accountno,\n" +
               "       (SELECT SUM(NVL(NPR_PREMIUM, 0)) + SUM(NVL(NPR_LOADING, 0))\n" +
               "          from LNPR_PRODUCT\n" +
               "         WHERE NP1_PROPOSAL = p1.NP1_PROPOSAL),\n" +
               "       cm.cm_commentby,\n" +
               "       cm.cm_comments,\n" +
               "       p1.NP1_TOTDIFPREM,\n" +
               " case when cm.cm_action='C' then 'Approved'\n" +
               "      when cm.cm_action='P' then 'Returned'\n" +
               "      when cm.cm_action in ('H','K') then 'Hold'\n" +
               "      when cm.cm_action in ('CBC-Decs') then 'Referred Approved'\n" +
               "      when cm.cm_action='D' then 'Declined' end CBCStatus,\n" +
               "       csd.ccs_field1 || '-' || csd.ccs_descr branch\n" +
 
                "  from lnp1_policymastr p1\n" +
                " inner join lnp2_policymastr p2\n" +
                "    on p1.np1_proposal = p2.np1_proposal\n" +
                "   and p2.np2_setno = 1\n" +
                " inner join lnu1_underwriti u1\n" +
                "    on u1.np1_proposal = p1.np1_proposal\n" +
                "   and u1.nph_life = 'D'\n" +
                "   and u1.nu1_life = 'F'\n" +
                " inner join lnpr_product pp\n" +
                " on pp.np1_proposal=p1.np1_proposal\n" +
                " and pp.npr_basicflag='Y'\n" +
                " inner join lppr_product pr\n" +
                " on pr.ppr_prodcd=pp.ppr_prodcd\n" +
                " inner join ccs_chanlsubdetl csd\n" +
                "    on p1.np1_channel = csd.cch_code\n" +
                "   and p1.np1_channeldetail = csd.ccd_code\n" +
                "   and p1.np1_channelsdetail = csd.ccs_code\n" +
                " inner join lnph_pholder ph\n" +
                "    on ph.nph_code = u1.nph_code\n" +
                "   and ph.nph_life = u1.nph_life\n" +
                " inner join lnad_address ad\n" +
                "    on ad.nph_code = ph.nph_code\n" +
                "   and ad.nph_life = ph.nph_life\n" +
                "   and ad.nad_addresstyp = 'C'\n" +
                " inner join lncm_comments cm\n" +
                "    on cm.np1_proposal = p1.np1_proposal\n" +
                " /*  and cm.cm_serial_no =\n" +
                "       (select max(cm_serial_no)\n" +
                "          from lncm_comments\n" +
                "         where np1_proposal = p1.np1_proposal)*/\n";

                if (userType == "L" || userType == "l")
                {
                    query += "   and cm.cm_commentby = '" + user + "'\n";
                }
                else
                {
                    query += "   and cm.cm_commentby like '" + user.Substring(0, 5) + "%'\n";
                }
                query += "   and trunc(cm.cm_commentdate) between to_date('" + fromdate + "', 'dd/MM/yyyy') and\n" +
                "       to_date('" + todate + "', 'dd/MM/yyyy')\n" +
                " WHERE (1 = 1)\n" +
                "   AND p1.np1_channeldetail = '" + bankCode + "'\n"+
                "   order by 3 desc";

                Session["DataQuery"] = query;
                DataTable dt = DB.getDataTable(query);
                return dt;
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }


        private DataTable getCBCDataForExcel()
        {
            try
            {
                string user = System.Convert.ToString(Session["s_USE_USERID"]);
                string userType = ace.Ace_General.getUserType(user);
                string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
                string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);
                string todate = txtDATEFROM.Text;
                string fromdate = txtDATETO.Text;
                string query = "select p1.np1_proposal Proposal,\n" +
                "       TO_CHAR(p1.np1_propdate, 'MM/DD/YY') Proposal_Date,\n" +
                "       TO_CHAR(p2.use_datetime, 'hh:mm:ss AM') Proposal_Time,\n" +
                "       pr.ppr_descr plan,\n" +
                "       ph.nph_idno CNIC,\n" +
                "       ph.nph_fullname Name,\n" +
                "       p1.np1_accountno Account_No,\n" +
                "       ad.nad_mobile Mobile_No,\n" +
                "       csd.ccs_field1 || '-' || csd.ccs_descr branch,\n" +
                " case when cm.cm_action='C' then 'Approved'\n" +
                "      when cm.cm_action='P' then 'Returned'\n" +
                "      when cm.cm_action in ('H','K') then 'Hold'\n" +
                "      when cm.cm_action in ('CBC-Decs') then 'Referred Approved'\n" +
                "      when cm.cm_action='D' then 'Declined' end Status,\n" +
                "       TO_CHAR(cm.cm_commentdate, 'MM/DD/YY') Last_Action_Date,\n" +
                "       TO_CHAR(cm.cm_commentdate, 'hh:mm:ss AM') Last_Action_Time,\n" +
                "       cm.cm_commentby Commended_By,\n" +
                "       cm.cm_comments Comments\n" +
                "  from lnp1_policymastr p1\n" +
                " inner join lnp2_policymastr p2\n" +
                "    on p1.np1_proposal = p2.np1_proposal\n" +
                "   and p2.np2_setno = 1\n" +
                " inner join lnu1_underwriti u1\n" +
                "    on u1.np1_proposal = p1.np1_proposal\n" +
                "   and u1.nph_life = 'D'\n" +
                "   and u1.nu1_life = 'F'\n" +
                " inner join lnpr_product pp\n" +
                " on pp.np1_proposal=p1.np1_proposal\n" +
                " and pp.npr_basicflag='Y'\n" +
                " inner join lppr_product pr\n" +
                " on pr.ppr_prodcd=pp.ppr_prodcd\n" +
                " inner join ccs_chanlsubdetl csd\n" +
                "    on p1.np1_channel = csd.cch_code\n" +
                "   and p1.np1_channeldetail = csd.ccd_code\n" +
                "   and p1.np1_channelsdetail = csd.ccs_code\n" +
                " inner join lnph_pholder ph\n" +
                "    on ph.nph_code = u1.nph_code\n" +
                "   and ph.nph_life = u1.nph_life\n" +
                " inner join lnad_address ad\n" +
                "    on ad.nph_code = ph.nph_code\n" +
                "   and ad.nph_life = ph.nph_life\n" +
                "   and ad.nad_addresstyp = 'C'\n" +
                " inner join lncm_comments cm\n" +
                "    on cm.np1_proposal = p1.np1_proposal\n" +
                " /*  and cm.cm_serial_no =\n" +
                "       (select max(cm_serial_no)\n" +
                "          from lncm_comments\n" +
                "         where np1_proposal = p1.np1_proposal)*/\n";

                if (userType == "L" || userType == "l")
                {
                    query += "   and cm.cm_commentby = '" + user + "'\n";
                }
                else
                {
                    query += "   and cm.cm_commentby like '" + user.Substring(0, 5) + "%'\n";
                }
                query += "   and trunc(cm.cm_commentdate) between to_date('" + todate + "', 'dd/MM/yyyy') and\n" +
                "       to_date('" + fromdate + "', 'dd/MM/yyyy')\n" +
                " WHERE (1 = 1)\n" +
                "   AND p1.np1_channeldetail = '" + bankCode + "'\n" +
                "   order by 1 desc";

                Session["DataQuery"] = query;
                DataTable dt = DB.getDataTable(query);
                return dt;
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }
        protected void btn_getData_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = getCBCData();
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "";
                    dGrid.DataSource = dt;
                    dGrid.DataBind();
                }
                else
                {
                    lblMsg.Text = "No Data found!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data found!');", true);

                    dGrid.DataSource = null;
                    dGrid.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ExportGridToExcel()
        {
            //try
            //{
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.Charset = "";
            //    string FileName = "CBC_Performance" + DateTime.Now + ".xls";
            //    StringWriter strwritter = new StringWriter();
            //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //    //dGrid.GridLines = GridLines.Both;
            //    dGrid.HeaderStyle.Font.Bold = true;
            //    dGrid.HeaderStyle.ForeColor = Color.Black;
            //    dGrid.RenderControl(htmltextwrtter);
            //    Response.Write(strwritter.ToString());
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //}

            //DataTable dt = getCBCData();
            DataTable dt_n = getCBCDataForExcel();
            if (dt_n.Rows.Count > 0)
            {
                //WriteExcel(dt);
                ExportToExcelHtml(dt_n);
                //SetDownloadLink();
            }
                  

        }

        // Excel function
        public void ExportToExcelHtml(DataTable table)
        {
            try
            {

                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                //context.Response.Write("<b>Un-dispatched Policy Documents List</b>");
                //context.Response.Write("<BR>");
                context.Response.Write("<BR>");
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;line-height:3.0pt;text-align:justif'> <TR>");

                //Begin Table
                //context.Response.Write("<table><tr>");

                //Write Header
                foreach (DataColumn column in table.Columns)
                {
                    context.Response.Write("<th>" + column.ColumnName + "</th>");
                }
                context.Response.Write("</tr>");

                //Write Data
                foreach (DataRow row in table.Rows)
                {
                    context.Response.Write("<tr>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        context.Response.Write("<td>" + row[i].ToString().Replace(",", string.Empty) + "</td>");
                    }
                    context.Response.Write("</tr>");
                }

                //End Table
                context.Response.Write("</table>");

                context.Response.ContentType = "application/ms-excel";
                context.Response.AppendHeader("Content-Disposition", "attachment;filename=CBC_Performances.xls");
                context.Response.End();
            }
            catch (Exception)
            {
                // lblUndispatchMsg.Text = "There is some issue found to export to Excel";
            }
        }
        ////////


        public void SetDownloadLink()
        {
            try
            {
                string FileName = Convert.ToString(ViewState["FileName"]);
                string downloadProposalFile = Convert.ToString(ViewState["downloadProposalFile"]);
                Response.Clear();
                Response.ContentType = "Application/.xls";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                //Response.TransmitFile(downloadProposalFile);
                Response.WriteFile(downloadProposalFile);
                Response.Flush();
                DeleteFile(downloadProposalFile);
                Response.End();
            }
            catch (Exception )
            {

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
        private void WriteExcel(DataTable dt)
        {
            try
            {
                string FileName = "CBC_Performance" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
                ViewState["FileName"] = FileName;
                string portotypeFilePath = Server.MapPath(ConfigurationSettings.AppSettings["prototypeCBCFilePath"]);
                string dfn = FileName;
                string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"] + "\\" + FileName);
                if (File.Exists(downloadProposalFile))
                {
                    File.Delete(downloadProposalFile);
                }
                ViewState["downloadProposalFile"] = downloadProposalFile;
                File.Copy(portotypeFilePath, downloadProposalFile);
                string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + downloadProposalFile + ";Extended Properties=" + "\"" + "Excel 8.0;HDR=YES;READONLY=FALSE" + "\"";
                OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
                string ExcelQuery = null;
                ExcelQuery = "SELECT * FROM [Sheet1$]";
                OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
                ExcelConnection.Open();
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    ExcelQuery = "INSERT INTO [Sheet1$] ([SNO],[Proposal],[Proposal Date],[Proposal Time],[Plan], [CNIC]," +
                        "[Name],[Account No],[Mobile No],[Branch]," +
                        "[Status],[Last Action Date],[Last Action Time],[Commented By],[Comments]) " +

                        "VALUES (" +
                        "'" + i.ToString() + "','" + dr["np1_proposal"].ToString() + "', '" + Convert.ToDateTime(dr["NP1_PROPDATE"].ToString()).ToString("MM/dd/yy") + "','" + Convert.ToDateTime(dr["use_datetime"].ToString()).ToString("hh:mm:ss tt") + "', '" + dr["plan"].ToString() + "','" + dr["NPH_IDNO"].ToString() + "','" + dr["nph_fullname"].ToString() + "'," +
                        "'" + dr["np1_accountno"].ToString() + "','" + dr["nad_mobile"].ToString() + "', '" + dr["branch"].ToString() + "', " +
                        "'" + dr["CBCStatus"].ToString() + "','" + Convert.ToDateTime(dr["cm_commentdate"].ToString()).ToString("MM/dd/yy") + "', '" + Convert.ToDateTime(dr["cm_commentdate"].ToString()).ToString("hh:mm:ss tt") + "', '" + dr["cm_commentby"].ToString() + "', '" + dr["cm_comments"].ToString() + "')";
                    ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
                    ExcelCommand.ExecuteNonQuery();
                    i++;
                }
                ExcelConnection.Close();
            }
            catch (Exception ex)
            {
                StringBuilder errMessage = new StringBuilder();
                errMessage.Append(ex.Message);
                ErrorHandle(ex);
            }
        }

        protected void hlbanca_Click(object sender, EventArgs e)
        {
            try
            {

                if (dGrid.Items.Count > 0)
                {

                    ExportGridToExcel();
                }
                else
                {
                    ShowMessage("No Item to Download");
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void btn_getData_Click1(object sender, EventArgs e)
        {
            btn_getData_Click(null, null);
        }
        #endregion
    }
}
