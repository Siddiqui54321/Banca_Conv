using SHAB.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using shsm;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Collections;


namespace Bancassurance.Presentation
{
    public partial class ManualStaffChannelIssuance : System.Web.UI.Page
    {
        string Sql, Msg, Path;
        Excel_Upload Obj_Excel_Upload = new Excel_Upload();
        DataTable Tissues = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand CmdDML = new OleDbCommand();

        Hashtable MyVal = new Hashtable();
        NameValueCollection columnNameValue = new NameValueCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);

            if (!IsPostBack)
            {
                if (userType == "A")
                {
                    BindDLL(); BindDLL1(); Binddata();
                }
                else
                {
                    lblAlert.Text = ("You are not Authorized...");
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NameValueCollection columnNameValue = new NameValueCollection();

                columnNameValue.Add("cch_code", ddlCCH_CHANNELCD.SelectedValue.Trim() == "" ? null : ddlCCH_CHANNELCD.SelectedValue); //.Trim().Split('-')[0]);
                columnNameValue.Add("ccd_code", ddlCCD_CHANNELDTLCD.SelectedValue.Trim() == "" ? null : ddlCCD_CHANNELDTLCD.SelectedValue);
                columnNameValue.Add("staff_id", txtStaffID.Text.Trim() == "" ? null : txtStaffID.Text);
                columnNameValue.Add("staff_name", txtStaffName.Text.Trim() == "" ? null : txtStaffName.Text);

                if (txtStaffID.Text == "" || txtStaffID.Text == null || txtStaffName.Text == "" || txtStaffName == null)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Please Enter Required Fields','')", true);
                }
                else
                {
                    string querychk = @"select ls.cch_code, ls.ccd_code, ls.staff_id from LSCH_STAFFCHANNELMAPPING ls where ls.cch_code = '" + ddlCCH_CHANNELCD.SelectedValue + "' and ls.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and ls.staff_id = '" + txtStaffID.Text + "'";
                    DataTable dt = DB.getDataTable(querychk);
                    if (dt.Rows.Count < 1)
                    {
                        string sqlString = "insert into LSCH_STAFFCHANNELMAPPING (cch_code,\n" +
                        "ccd_code,\n" +
                        "staff_id,\n" +
                        "staff_name)\n" +
                        "values(\n" +
                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                        " '" + columnNameValue.getObject("staff_id") + "', \n" +
                        " '" + columnNameValue.getObject("staff_name") + "' \n" +
                        ")";
                        DB.executeDML(sqlString);
                        //lblAlert.ForeColor = System.Drawing.Color.Green;
                        //lblAlert.Text = "Record Saved...";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                            "swal('','Record Saved','')", true);

                    }
                    else
                    {
                        //lblAlert.ForeColor = System.Drawing.Color.Red;
                        //lblAlert.Text = "Record Already Found..."; // + ex.Message;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                           "swal('','Record Already Found1','')", true);
                    }


                }
            }
            catch (Exception ex)
            {
                lblAlert.ForeColor = System.Drawing.Color.Red;
                lblAlert.Text = "Error......" + ex.Message;
            }
        }

        protected void Binddata()
        {
            string query = @"SELECT S.STAFF_ID,S.STAFF_NAME,Cd.Ccd_Descr FROM LSCH_STAFFCHANNELMAPPING S INNER JOIN cch_channel ch on ch.cch_code = s.cch_code INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code order by LENGTH(ltrim(rtrim(S.STAFF_ID))) asc, ltrim(rtrim(S.STAFF_ID)) asc"; 
            //string query = @"SELECT S.STAFF_ID,S.STAFF_NAME,Cd.Ccd_Descr FROM LSCH_STAFFCHANNELMAPPING S  
            //                INNER JOIN cch_channel ch on ch.cch_code = s.cch_code
            //                INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code
            //                order by staff_id asc";
            DataTable dt = DB.getDataTable(query);
            if (dt.Rows.Count > 0)
            {
                grdStaffChMap.DataSource = dt;
                grdStaffChMap.DataBind();
            }
        }

        protected void BindDLL()
        {
            IDataReader LCOP_OCCUPATIONReader2 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCH_CHANNEL_RO(); 
            ddlCCH_CHANNELCD.DataSource = LCOP_OCCUPATIONReader2;
            ddlCCH_CHANNELCD.DataBind();
            LCOP_OCCUPATIONReader2.Close();
        }

        protected void BindDLL1()
        {
            IDataReader LCOP_OCCUPATIONReader3 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCD_CHANNELDETAIL_RO_staff(); 
            ddlCCD_CHANNELDTLCD.DataSource = LCOP_OCCUPATIONReader3;
            ddlCCD_CHANNELDTLCD.DataBind();
            LCOP_OCCUPATIONReader3.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "" && txtStaffID.Text != null && txtStaffID.Text != "")
            {
                //string query = @"SELECT S.STAFF_ID,S.STAFF_NAME,Ch.Cch_Descr,Cd.Ccd_Descr FROM LSCH_STAFFCHANNELMAPPING S
                //            INNER JOIN cch_channel ch on ch.cch_code = s.cch_code
                //            INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code
                //            and cd.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and s.staff_id = '" + txtStaffID.Text + "'";
                string query = @"SELECT S.STAFF_ID,S.STAFF_NAME,Ch.Cch_Descr,Cd.Ccd_Descr FROM LSCH_STAFFCHANNELMAPPING S INNER JOIN cch_channel ch on ch.cch_code = s.cch_code INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code and cd.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and s.staff_id = '" + txtStaffID.Text + "' order by LENGTH(ltrim(rtrim(S.STAFF_ID))) asc, ltrim(rtrim(S.STAFF_ID)) asc"; ;
                DataTable dt = DB.getDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    grdStaffChMap.DataSource = dt;
                    grdStaffChMap.DataBind();
                    txtStaffID.Text = null;
                    txtStaffName.Text = null;
                    lblAlert.Text = "";
                    //btnSave.Text = "Update";
                }
                else
                {
                    //lblAlert.Text = "Record Not Found...";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Record Not Found','')", true);
                    grdStaffChMap.DataSource = null;
                    grdStaffChMap.DataBind();
                    txtStaffID.Text = null;
                    txtStaffName.Text = null;

                }
            }
            else if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "") // && txtStaffID.Text == null && txtStaffID.Text == "")
            {
                string query1 = @"SELECT S.STAFF_ID,S.STAFF_NAME,Ch.Cch_Descr,Cd.Ccd_Descr FROM LSCH_STAFFCHANNELMAPPING S
                            INNER JOIN cch_channel ch on ch.cch_code = s.cch_code
                            INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code
                            and cd.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "'  order by LENGTH(ltrim(rtrim(S.STAFF_ID))) asc, ltrim(rtrim(S.STAFF_ID)) asc";
                DataTable dt1 = DB.getDataTable(query1);
                if (dt1.Rows.Count > 0)
                {
                    grdStaffChMap.DataSource = dt1;
                    grdStaffChMap.DataBind();
                    txtStaffID.Text = null;
                    txtStaffName.Text = null;
                    lblAlert.Text = "";
                    //btnSave.Text = "Update";
                }
                else
                {
                    //lblAlert.Text = "Record Not Found...";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Record Not Found','')", true);
                    grdStaffChMap.DataSource = null;
                    grdStaffChMap.DataBind();
                    txtStaffID.Text = null;
                    txtStaffName.Text = null;

                }
            }
            else
            {
                //lblAlert.Text = "Record Not Found...";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                    "swal('','Record Not Found','')", true);
                grdStaffChMap.DataSource = null;
                grdStaffChMap.DataBind();
                txtStaffID.Text = null;
                txtStaffName.Text = null;
            }
        }

        public void refresh()
        {
            txtStaffID.Text = null;
            txtStaffName.Text = null;
            lblAlert.Text = "";
        }

        protected void grdStaffChMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txtStaffID_1 = grdStaffChMap.SelectedRow.Cells[0].Text;
            string txtStaffName_1 = grdStaffChMap.SelectedRow.Cells[1].Text;
            txtStaffID.Text = txtStaffID_1;
            txtStaffName.Text = txtStaffName_1;
        }

        protected void grdStaffChMap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdStaffChMap, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            BindDLL();
            BindDLL1();
            Binddata();
            txtStaffID.Text = null;
            txtStaffName.Text = null;
            lblAlert.Text = "";
            btnSave.Text = "Save";
            Label2.Visible = false;
            btnUpload.Visible = false;
            FileUpload1.Visible = false;

        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (txtStaffID.Text != null && txtStaffID.Text != "" && txtStaffName.Text != null && txtStaffName.Text != "")
            {


                try
                {
                    SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
                    NameValueCollection columnNameValue = new NameValueCollection();
                    //Setting Columns Values			
                    columnNameValue.Add("cch_code", ddlCCH_CHANNELCD.SelectedValue.Trim() == "" ? null : ddlCCH_CHANNELCD.SelectedValue); //.Trim().Split('-')[0]);
                    columnNameValue.Add("ccd_code", ddlCCD_CHANNELDTLCD.SelectedValue.Trim() == "" ? null : ddlCCD_CHANNELDTLCD.SelectedValue);
                    columnNameValue.Add("staff_id", txtStaffID.Text.Trim() == "" ? null : txtStaffID.Text);
                    columnNameValue.Add("staff_name", txtStaffName.Text.Trim() == "" ? null : txtStaffName.Text);
                    //Executing DML					
                    string qry = "UPDATE LSCH_STAFFCHANNELMAPPING SET STAFF_NAME='" + columnNameValue.getObject("staff_name") + "'" +
                        " where cch_code = '" + columnNameValue.getObject("cch_code") + "' and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and staff_id = '" + columnNameValue.getObject("staff_id") + "'";
                    DB.executeDML(qry);
                    btnSearch_Click(null, null);
                    //lblAlert.ForeColor = System.Drawing.Color.Green;
                    //lblAlert.Text = "Record Successfully Updated"; // + ex.Message;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Record Updated','')", true);
                }
                catch (Exception EX)
                {
                    lblAlert.Text = EX.Message;
                }
            }
            else
            {
                //lblAlert.ForeColor = System.Drawing.Color.Red;
                //lblAlert.Text = "Please Enter Required Fields...1";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                    "swal('','Please Enter the Required Fields...','')", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //DataTable dt = LNP1_POLICYMASTRDB.GetExcelReportForStaffChannel(ddlCCH_CHANNELCD.Text, ddlCCD_CHANNELDTLCD.Text);

            string query1 = @"SELECT S.STAFF_ID,S.STAFF_NAME,Cd.Ccd_Descr BANK_NAME FROM LSCH_STAFFCHANNELMAPPING S
                            INNER JOIN cch_channel ch on ch.cch_code = s.cch_code
                            INNER JOIN CCD_CHANNELDETAIL cd on cd.ccd_code = s.ccd_code
                            and cd.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
            DataTable dt1 = DB.getDataTable(query1);

            if (dt1.Rows.Count > 0)
            {
                ExportGridToExcel(dt1);
                Response.Write("");
            }
            else
            {
                //lblAlert.ForeColor = System.Drawing.Color.Red;
                //lblAlert.Text = "Please Search the Required Channel Detail...";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                    "swal('','Record Not Found...','')", true);
            }

        }
        private void ExportGridToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                string filename = "downloadStaffChannel" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
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
                Response.Write("<b><u>Staff Channel Detail</u></b>");
                Response.Write("<br />");
                //  Response.Write(Label28.Text + " " + lblDateFrom.Text + "  To  " + lblDateTo.Text + "<br />");
                Response.Write("<br />");
                //Result detail

                Response.Write(tw.ToString());
                Response.End();
            }



        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int NotUpLoadRec = 0;
            int uploadedRec = 0;
            try
            {
                if (FileUpload1.HasFile)
                {
                    //lblFileNotSelectMsg.Text = "";
                    //gvDummy.Visible = true;
                    btnSave.Visible = true;



                    Msg = DML("truncate table UPLOAD_DISPATCH");
                    string ConStr = "";
                    Path = Server.MapPath(FileUpload1.FileName);
                    // Label1.Text = Path;
                    FileUpload1.SaveAs(Path);


                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";



                    // Obj_Excel_Upload.GridView1 = this.GridView1; Obj_Excel_Upload.fuDocument = this.FileUpload1; Tissues = Obj_Excel_Upload.SKU_Load("SELECT * From [Sheet1$];"); Session.Add("Tissues", Tissues);

                    string query = "SELECT * FROM [Sheet1$]";

                    OleDbConnection conn = new OleDbConnection(ConStr);

                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    GridView1.DataSource = ds.Tables[0];

                    GridView1.DataBind();

                    conn.Close();



                    for (int row = 0; row <= GridView1.Rows.Count - 1; row++)
                    {
                        Sql = "Insert into LSCH_STAFFCHANNELMAPPING (staff_id,staff_name,cch_code,ccd_code)" +
                            " values('" + GridView1.Rows[row].Cells[0].Text.ToString() + "'," +
                       "'" + GridView1.Rows[row].Cells[1].Text.ToString() + "'," +
                       //"'" + "2" + "'," +                       
                       "'" + ddlCCH_CHANNELCD.SelectedValue + "', " +
                       "'" + ddlCCD_CHANNELDTLCD.SelectedValue + "')"; 
                        //SessionObject.Set("s_USE_USERID"
                        //Label5.Text = Sql;
                        //lblAlert.Text = Sql;

                        Msg = DML(Sql);
                        if (Msg == "done")
                        {
                            uploadedRec = uploadedRec + 1;
                        }
                        else
                        {
                            NotUpLoadRec = NotUpLoadRec + 1;

                        }

 
                    }

                    btnSave.Visible = true;

                    System.IO.File.Delete(Path);


                    string message = " record(s) upload out of ";
                    int totRec = uploadedRec + NotUpLoadRec;
                    string script = $@"
                    swal({{
                    title: '',
                    text: '{uploadedRec}-{message} - {totRec}',           
                    }});";

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", script, true);
                }
                else
                {


                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Please Select Excel File','')", true);
                }
            }



            catch (Exception Ex)
            {


                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Please use Excel 97-2003 format XLS','')", true);



            }



        }

        protected void btnUploadDisp_Click(object sender, EventArgs e)
        {
            Label2.Visible = true;
            btnUpload.Visible = true;
            FileUpload1.Visible = true;
        }

        public string DML(string sql)
        {
            try
            {
                CmdDML.CommandText = sql;
                CmdDML.Connection = GetConn();
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

        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;


        }




    }
}