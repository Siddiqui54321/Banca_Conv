using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using shsm;
using SHAB.Data;
using SHMA.Enterprise.Presentation;
using System.Data.SqlClient;
using System.Data.OleDb;
using ace;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;

namespace Bancassurance.Presentation
{
    public partial class GenerateNewBranches : System.Web.UI.Page
    {
        string Sql, Msg, Path;
        Excel_Upload Obj_Excel_Upload = new Excel_Upload();
        DataTable Tissues = new DataTable();
        OleDbCommand CmdDML = new OleDbCommand();
        string use_userid_t4BSO = "";

        public string v_txtBranchCode;
        //SqlCommand cmd = new SqlCommand();
        OleDbCommand cmd = new OleDbCommand();

        public string fixCCSCode = "0";    //1st record 781   - 2nd record 782
        //private const string fixBnkCod = "HBL";
        //private const string fixImmeSup = "912000";
        //private const string Query = "strQrygetaggcode";
        NameValueCollection columnNameValue = new NameValueCollection();
        //private string newValue;
        //private string ccsauto;
        //DateTime dta = DateTime.Parse("05/07/2023", CultureInfo.GetCultureInfo("en-GB"));
        //DateTime thisDate1 = new DateTime(04, 01, 2022);
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);


            if (!IsPostBack)
            {
                if (userType == "A")
                {
                    BindDLLC(); BindDLLCD(); Binddata();
                    BrPanel.Visible = true;

                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    btnUpdate.Visible = true;
                    btnUpdate.Enabled = true;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                    btnUploadDisp.Visible = true;
                    btnUploadDisp.Enabled = true;
                }
                else
                {
                    //lblAlert.ForeColor = System.Drawing.Color.Red;
                    //lblAlert.Text = ("You are not Authorized...");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','You are not Authorized','')", true);
                }
            }
        }
        [Obsolete]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRec();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string use_userid_t4BSO = "";
            string brlen = txtBranchCode.Text;
            int vbrlen = brlen.Length;
            if (vbrlen == 1)
            {
                v_txtBranchCode = "000" + txtBranchCode.Text;
            }
            else if (vbrlen == 2)
            {
                v_txtBranchCode = "00" + txtBranchCode.Text;
            }
            else if (vbrlen == 3)
            {
                v_txtBranchCode = "0" + txtBranchCode.Text;
            }
            else
            {
                v_txtBranchCode = txtBranchCode.Text;
            }

            txtBranchCode.Text = v_txtBranchCode;

            if (ddlCCD_CHANNELDTLCD.SelectedValue.ToString() == "F")
            {
                use_userid_t4BSO = "BCO"; // + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;
            }
            else
            {
                use_userid_t4BSO = "BSO"; // + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;
            }


            if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "" && txtBranchCode.Text != null && txtBranchCode.Text != "")
            {
                //string query = @"SELECT cs.ccs_code, cs.ccs_field1, cs.ccs_descr, cd.ccd_descr FROM ccs_chanlsubdetl cs 
                //inner join CCD_CHANNELDETAIL cd on cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code 
                //and cs.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and cs.ccs_field1 = '" + txtBranchCode.Text + "' order by lpad(CS.CCS_FIELD1, 7)";
                string query = @"select CS.CCD_CODE, CS.CCS_FIELD1, CS.CCS_DESCR, u.use_userid BSOUSER, u1.use_userid BMUSER " +
                "from CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS, use_usermaster u, use_usermaster u1 " +
                "where ch.cch_code=cd.cch_code and cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code " +
                "and(substr(u.use_userid, 1, 3) = '" + use_userid_t4BSO + "' " +
                "and substr(u.use_userid, 4, 1) = cd.cch_code " +
                "and substr(u.use_userid, 5, 1) = cd.ccd_code " +
                "and substr(u.use_userid, 6, 4) = cs.ccs_field1) " +
                "and(substr(u1.use_userid, 1, 2) = 'BM' " +
                "and substr(u1.use_userid, 3, 1) = cd.cch_code " +
                "and substr(u1.use_userid, 4, 1) = cd.ccd_code " +
                "and substr(u1.use_userid, 5, 4) = cs.ccs_field1) " +
                "and ch.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and cs.ccs_field1 = '" + txtBranchCode.Text + "' and cs.ccs_field1 not in ('000','0000') order by to_number(cs.ccs_field1)";

                DataTable dt = DB.getDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    grdBranchDtl.DataSource = dt;
                    grdBranchDtl.DataBind();
                    txtBranchCode.Text = null;
                    txtBranchName.Text = null;
                    lblAlert.Text = "";
                }
                else
                {
                    //lblAlert.ForeColor = System.Drawing.Color.Red;
                    //lblAlert.Text = "Record Not Found...";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Not Found','')", true);
                    grdBranchDtl.DataSource = null;
                    grdBranchDtl.DataBind();
                    txtBranchCode.Text = null;
                    txtBranchName.Text = null;
                }
            }
            else if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "")
            {
                //string query = @"SELECT cs.ccs_code, cs.ccs_field1, cs.ccs_descr, cd.ccd_descr FROM ccs_chanlsubdetl cs 
                //inner join CCD_CHANNELDETAIL cd on cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code 
                //and cs.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' order by lpad(CS.CCS_FIELD1, 7)";
                string query = @"select CS.CCD_CODE, CS.CCS_FIELD1, CS.CCS_DESCR, u.use_userid BSOUSER, u1.use_userid BMUSER " +
                "from CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS, use_usermaster u, use_usermaster u1 " +
                "where ch.cch_code=cd.cch_code and cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code  " +
                "and(substr(u.use_userid, 1, 3) = '" + use_userid_t4BSO + "' " +
                "and substr(u.use_userid, 4, 1) = cd.cch_code " +
                "and substr(u.use_userid, 5, 1) = cd.ccd_code " +
                "and substr(u.use_userid, 6, 4) = cs.ccs_field1) " +
                "and(substr(u1.use_userid, 1, 2) = 'BM' " +
                "and substr(u1.use_userid, 3, 1) = cd.cch_code " +
                "and substr(u1.use_userid, 4, 1) = cd.ccd_code " +
                "and substr(u1.use_userid, 5, 4) = cs.ccs_field1) " +
                "and ch.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and cs.ccs_field1 not in ('000','0000') order by to_number(cs.ccs_field1)";

                DataTable dt = DB.getDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    grdBranchDtl.DataSource = dt;
                    grdBranchDtl.DataBind();
                    txtBranchCode.Text = null;
                    txtBranchName.Text = null;
                    lblAlert.Text = "";
                    //lblTotrecord.Text = dt.Rows.Count.ToString();                    
                }
                else
                {
                    lblAlert.ForeColor = System.Drawing.Color.Red;
                    lblAlert.Text = "";
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Not Found','')", true);
                    grdBranchDtl.DataSource = null;
                    grdBranchDtl.DataBind();
                    txtBranchCode.Text = null;
                    txtBranchName.Text = null;
                }
            }
            else
            {
                lblAlert.ForeColor = System.Drawing.Color.Red;
                lblAlert.Text = "";
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','CCS_CoDE Value is null','')", true);
                grdBranchDtl.DataSource = null;
                grdBranchDtl.DataBind();
                txtBranchCode.Text = null;
                txtBranchName.Text = null;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string brlen = txtBranchCode.Text;
                int vbrlen = brlen.Length;
                if (vbrlen == 1)
                {
                    v_txtBranchCode = "000" + txtBranchCode.Text;
                }
                else if (vbrlen == 2)
                {
                    v_txtBranchCode = "00" + txtBranchCode.Text;
                }
                else if (vbrlen == 3)
                {
                    v_txtBranchCode = "0" + txtBranchCode.Text;
                }
                else
                {
                    v_txtBranchCode = txtBranchCode.Text;
                }

                txtBranchCode.Text = v_txtBranchCode;

                if (txtBranchCode.Text == "" || txtBranchCode.Text == null || txtBranchName.Text == "" || txtBranchName.Text == null)

                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Branch Code and Name must be entered.','')", true);
                }
                else
                {

                    String ccsauto = "";
                    string strQry1 = "Select ccs_code from ccs_chanlsubdetl where cch_code = '" + ddlCCH_CHANNELCD.Text + "' and ccd_code = '" + ddlCCD_CHANNELDTLCD.Text + "' and ccs_field1 = '" + txtBranchCode.Text + "'";
                    rowset rstQry1 = DB.executeQuery(strQry1);
                    if (rstQry1.next())
                    {
                        ccsauto = rstQry1.getString("ccs_code");
                    }
                    if (ccsauto == "" || ccsauto == null)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                            "swal('','Branch Code Not Found.','')", true);
                    }
                    else
                    {
                        SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
                        NameValueCollection columnNameValue = new NameValueCollection();

                        String upccdlogo = "";
                        String strQrylogo = "select ccd_logo from ccd_channeldetail where cch_code = '" + ddlCCH_CHANNELCD.Text + "' and ccd_code = '" + ddlCCD_CHANNELDTLCD.Text + "'";
                        rowset rstQrylogo = DB.executeQuery(strQrylogo);
                        if (rstQrylogo.next())
                        {
                            //upccdcode = rstQrylogo.getString("ccd_code");
                            upccdlogo = rstQrylogo.getString("ccd_logo");
                        }

                        string qryUp = "UPDATE ccs_chanlsubdetl SET ccs_descr='" + upccdlogo + "-" + txtBranchName.Text + "' " +
                        " where cch_code = '" + ddlCCH_CHANNELCD.Text + "' and ccd_code = '" + ddlCCD_CHANNELDTLCD.Text + "' and ccs_code = '" + ccsauto + "'";
                        DB.executeDML(qryUp);
                        String bnkcodeGet = "";
                        String strQrybnkcodeGet = "Select distinct decode(pbk_bankcode, 'UBL', '901', 'SMBL', '907', 'BAL', '903', 'NBP', '904', 'NIB', '905', 'SBL', '906', 'SILK', '908', 'BOP', '909', 'DIB', '910', 'HBL', '912', 'FBL', '913', 'JSB', '910', '000') bCode from LAAG_AGENT a where pbk_bankcode = '" + upccdlogo + "'";
                        rowset rstQrybnkcodeGet = DB.executeQuery(strQrybnkcodeGet);
                        if (rstQrybnkcodeGet.next())
                        {
                            bnkcodeGet = rstQrybnkcodeGet.getString("bCode");
                        }

                        string aag_agcode_t4 = bnkcodeGet + "" + txtBranchCode.Text;

                        string qryuplaag = "update laag_agent set aag_name = '" + upccdlogo + "-" + txtBranchName.Text + "' where aag_agcode = '" + aag_agcode_t4 + "'";
                        DB.executeDML(qryuplaag);
                        //lblAlert.ForeColor = System.Drawing.Color.Green;
                        //lblAlert.Text = "Record Updated ..."; // + ex.Message;
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Updated','')", true);

                        //UPDATE SLILASPRD 
                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string qryupPBB = "update PBB_BANKBRANCH set PBB_BRANCHNAME = '" + upccdlogo + "-" + txtBranchName.Text + "' " +
                                "where PBK_BANKCODE = '" + upccdlogo + "' " +
                                        "and PBB_BRANCHCODE = '" + txtBranchCode.Text + "' "; // '" + columnNameValue.getObject("brCode") + "' ";

                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(qryupPBB, dbCon);
                            int x = dbCom.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in PBB_BANKBRANCH','')", true);
                        }
                        finally
                        {
                            dbCon.Close();
                        }

                        try
                        {
                            string qryuplaag1 = "update laag_agent set aag_name = '" + upccdlogo + "-" + txtBranchName.Text + "' where aag_agcode = '" + aag_agcode_t4 + "'";  //update SLILASPRD.laag_agent remove SLILASPRD.
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(qryuplaag1, dbCon);
                            int x = dbCom.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        finally
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                                "swal('','Record Updated','')", true);

                            dbCon.Close();
                        }


                    }
                }
            }
            catch (Exception) //ex
            {
                //throw new Exception(EX.Message);
                //lblAlert.ForeColor = System.Drawing.Color.Red;
                //lblAlert.Text = "Updated allow only for Branch Name..."; // + ex.Message;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Updated allow only for Branch Name','')", true);
            }
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = true;
            btnExport.Enabled = true;
            btnUploadDisp.Enabled = true;
            btnRefersh();
        }

        protected void grdBranchDtl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string BRCode = e.Row.Cells[0].Text.Replace("'", "\\'");
                string BRName = e.Row.Cells[1].Text.Replace("'", "\\'");
                e.Row.Cells[0].Attributes["onclick"] = $"fillTextboxes('{BRCode}', '{BRName}')";
                e.Row.Cells[0].Style["cursor"] = "pointer";

                e.Row.Cells[1].Attributes["onclick"] = $"fillTextboxes('{BRCode}', '{BRName}')";
                e.Row.Cells[1].Style["cursor"] = "pointer";
            }

        }

        protected void grdBranchDtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = grdBranchDtl.SelectedRow;

            string BRCode = selectedRow.Cells[0].Text;
            string BRName = selectedRow.Cells[1].Text;
        }
        protected void BindDLLC()
        {
            IDataReader LCOP_OCCUPATIONReader2 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCH_CHANNEL_RO(); ;
            ddlCCH_CHANNELCD.DataSource = LCOP_OCCUPATIONReader2;
            ddlCCH_CHANNELCD.DataBind();
            LCOP_OCCUPATIONReader2.Close();
        }

        protected void BindDLLCD()
        {
            IDataReader LCOP_OCCUPATIONReader3 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCD_CHANNELDETAIL_RO(); ;
            ddlCCD_CHANNELDTLCD.DataSource = LCOP_OCCUPATIONReader3;
            ddlCCD_CHANNELDTLCD.DataBind();
            LCOP_OCCUPATIONReader3.Close();
        }

        protected void Binddata()
        {

            //string query = @"SELECT CS.CCS_CODE, CS.CCS_FIELD1, CS.CCS_DESCR, CD.CCD_DESCR FROM CCS_CHANLSUBDETL CS 
            //    INNER JOIN CCH_CHANNEL CH ON CH.CCH_CODE = CS.CCH_CODE
            //    INNER JOIN CCD_CHANNELDETAIL CD ON CD.CCD_CODE = CS.CCD_CODE order by lpad(CS.CCS_FIELD1,7)";

            string useBSOuser = "BSO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;  //BSO23
            string useBSOuser1 = useBSOuser.Substring(0, 3);
            string useBSOuser2 = useBSOuser.Substring(4, 1);
            string useBMuser = "BM" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;    //BM23
            string useBMuser1 = useBMuser.Substring(0, 2);
            string useBMuser2 = useBMuser.Substring(3, 1);

            string query = @"select CS.CCD_CODE, CS.CCS_FIELD1, CS.CCS_DESCR, u.use_userid BSOUSER, u1.use_userid BMUSER
                from CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS, use_usermaster u, use_usermaster u1
                where ch.cch_code=cd.cch_code and cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code  
                and(substr(u.use_userid, 1, 3) = 'BSO'
                and substr(u.use_userid, 4, 1) = cd.cch_code
                and substr(u.use_userid, 5, 1) = cd.ccd_code
                and substr(u.use_userid, 6, 4) = cs.ccs_field1)
                and(substr(u1.use_userid, 1, 2) = 'BM'
                and substr(u1.use_userid, 3, 1) = cd.cch_code
                and substr(u1.use_userid, 4, 1) = cd.ccd_code
                and substr(u1.use_userid, 5, 4) = cs.ccs_field1)                
                and ch.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and cs.ccs_field1 not in ('000','0000') order by to_number(cs.ccs_field1) ";



            DataTable dt = DB.getDataTable(query);
            if (dt.Rows.Count > 0)
            {
                grdBranchDtl.DataSource = dt;
                grdBranchDtl.DataBind();
            }
        }

        protected void btnRefersh()
        {
            BindDLLC(); BindDLLCD(); Binddata();
            //txtAgencyCode.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            lblAlert.Text = "";
            Label2.Visible = false;
            btnUpload.Visible = false;
            FileUpload1.Visible = false;
            BrPanel.Visible = true;
            pnlBrMap.Visible = false;
            //            pnlExl.Visible = false;

        }
        //Validatedata applied as some time data already found in the below validation tables.
        public string validatedata(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string brCdQuerychk = @"SELECT s.ccs_code, s.cch_code, s.ccd_code  FROM ccs_chanlsubdetl s where s.ccs_field1 = '" + brCdP + "' and s.cch_code = '" + cchP + "' and s.ccd_code = '" + ccdP + "' ";
            DataTable dt = DB.getDataTable(brCdQuerychk);
            if (dt.Rows.Count > 0)
            {
                return "G";
            }
            else
            {


                string ccsPKchk = @"SELECT s.ccs_code, s.cch_code, s.ccd_code  FROM ccs_chanlsubdetl s where s.ccs_code = '" + fixCCSCode1 + "' and s.cch_code ='" + cchP + "' and s.ccd_code = '" + ccdP + "' ";
                DataTable dt1 = DB.getDataTable(ccsPKchk);
                if (dt1.Rows.Count > 0)
                {
                    return "H";
                }
                else
                {
                    string usePKchkbso = @"SELECT ue.USE_USERID FROM USE_USERMASTER ue where ue.USE_USERID in ('" + bsouserid + "') ";
                    DataTable dt2 = DB.getDataTable(usePKchkbso);
                    if (dt2.Rows.Count > 0)
                    {
                        return "I";
                    }
                    else
                    {
                        string usePKchkbm = @"SELECT ue.USE_USERID FROM USE_USERMASTER ue where ue.USE_USERID in ('" + bmuserid + "') ";
                        DataTable dta = DB.getDataTable(usePKchkbm);
                        if (dta.Rows.Count > 0)
                        {
                            return "Ia";
                        }
                        else
                        {
                            string counPKchk = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bsouserid + "') ";
                            DataTable dt3 = DB.getDataTable(counPKchk);
                            if (dt3.Rows.Count > 0)
                            {
                                return "J";
                            }
                            else
                            {
                                string counPKchkbm = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bmuserid + "') ";
                                DataTable dtb = DB.getDataTable(counPKchkbm);
                                if (dtb.Rows.Count > 0)
                                {
                                    return "Ja";
                                }

                                else
                                {
                                    string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                                    DataTable dt4 = DB.getDataTable(sysdtl1pk);
                                    if (dt4.Rows.Count > 0)
                                    {
                                        return "K";
                                    }
                                    else
                                    {
                                        string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                                        DataTable dt5 = DB.getDataTable(sysdtl2pk);
                                        if (dt5.Rows.Count > 0)
                                        {
                                            return "L";
                                        }
                                        else
                                        {
                                            string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                            DataTable dt6 = DB.getDataTable(laagpk);
                                            if (dt6.Rows.Count > 0)
                                            {
                                                return "M";
                                            }
                                            else
                                            {
                                                string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                                                DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                                                if (dt7.Rows.Count > 0)
                                                {
                                                    return "O";
                                                }
                                                else
                                                {
                                                    string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                                    DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                                    if (dt8.Rows.Count > 0)
                                                    {
                                                        return "P";
                                                    }
                                                    else
                                                    {
                                                        return "N";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string validatedata1(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {

            string usePKchkbm = @"SELECT ue.USE_USERID FROM USE_USERMASTER ue where ue.USE_USERID in ('" + bmuserid + "') ";
            DataTable dta = DB.getDataTable(usePKchkbm);
            if (dta.Rows.Count > 0)
            {
                return "Ia";
            }
            else
            {
                string counPKchk = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bsouserid + "') ";
                DataTable dt3 = DB.getDataTable(counPKchk);
                if (dt3.Rows.Count > 0)
                {
                    return "J";
                }
                else
                {
                    string counPKchkbm = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bmuserid + "') ";
                    DataTable dtb = DB.getDataTable(counPKchkbm);
                    if (dtb.Rows.Count > 0)
                    {
                        return "Ja";
                    }

                    else
                    {
                        string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                        DataTable dt4 = DB.getDataTable(sysdtl1pk);
                        if (dt4.Rows.Count > 0)
                        {
                            return "K";
                        }
                        else
                        {
                            string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                            DataTable dt5 = DB.getDataTable(sysdtl2pk);
                            if (dt5.Rows.Count > 0)
                            {
                                return "L";
                            }
                            else
                            {
                                string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                DataTable dt6 = DB.getDataTable(laagpk);
                                if (dt6.Rows.Count > 0)
                                {
                                    return "M";
                                }
                                else
                                {
                                    string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                                    DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                                    if (dt7.Rows.Count > 0)
                                    {
                                        return "O";
                                    }
                                    else
                                    {
                                        string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                        DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                        if (dt8.Rows.Count > 0)
                                        {
                                            return "P";
                                        }
                                        else
                                        {
                                            return "N";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string validatedata2(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string counPKchk = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bsouserid + "') ";
            DataTable dt3 = DB.getDataTable(counPKchk);
            if (dt3.Rows.Count > 0)
            {
                return "J";
            }
            else
            {
                string counPKchkbm = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bmuserid + "') ";
                DataTable dtb = DB.getDataTable(counPKchkbm);
                if (dtb.Rows.Count > 0)
                {
                    return "Ja";
                }

                else
                {
                    string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                    DataTable dt4 = DB.getDataTable(sysdtl1pk);
                    if (dt4.Rows.Count > 0)
                    {
                        return "K";
                    }
                    else
                    {
                        string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                        DataTable dt5 = DB.getDataTable(sysdtl2pk);
                        if (dt5.Rows.Count > 0)
                        {
                            return "L";
                        }
                        else
                        {
                            string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                            DataTable dt6 = DB.getDataTable(laagpk);
                            if (dt6.Rows.Count > 0)
                            {
                                return "M";
                            }
                            else
                            {
                                string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                                DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                                if (dt7.Rows.Count > 0)
                                {
                                    return "O";
                                }
                                else
                                {
                                    string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                    DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                    if (dt8.Rows.Count > 0)
                                    {
                                        return "P";
                                    }
                                    else
                                    {
                                        return "N";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string validatedata3(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string counPKchkbm = @"SELECT luc.USE_USERID FROM LUCN_USERCOUNTRY luc where luc.use_userid in ('" + bmuserid + "') ";
            DataTable dtb = DB.getDataTable(counPKchkbm);
            if (dtb.Rows.Count > 0)
            {
                return "Ja";
            }

            else
            {
                string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                DataTable dt4 = DB.getDataTable(sysdtl1pk);
                if (dt4.Rows.Count > 0)
                {
                    return "K";
                }
                else
                {
                    string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                    DataTable dt5 = DB.getDataTable(sysdtl2pk);
                    if (dt5.Rows.Count > 0)
                    {
                        return "L";
                    }
                    else
                    {
                        string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                        DataTable dt6 = DB.getDataTable(laagpk);
                        if (dt6.Rows.Count > 0)
                        {
                            return "M";
                        }
                        else
                        {
                            string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                            DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                            if (dt7.Rows.Count > 0)
                            {
                                return "O";
                            }
                            else
                            {
                                string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                if (dt8.Rows.Count > 0)
                                {
                                    return "P";
                                }
                                else
                                {
                                    return "N";
                                }
                            }
                        }
                    }
                }
            }
            //}
            //}
        }

        public string validatedataluBSO(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string counPKluBSO1 = @"SELECT luch.USE_USERID FROM LUCH_USERCHANNEL luch where luch.use_userid = '" + bsouserid + "' and luch.cch_code = '" + cchP + "' and luch.ccd_code = '" + ccdP + "' and luch.ccs_code='" + fixCCSCode1 + "'";
            DataTable dtbso1 = DB.getDataTable(counPKluBSO1);
            if (dtbso1.Rows.Count > 0)
            {
                return "JSBO";
            }
            else
            {
                string counPKluBM = @"SELECT luch.USE_USERID FROM LUCH_USERCHANNEL luch where luch.use_userid = '" + bmuserid + "' and luch.cch_code = '" + cchP + "' and luch.ccd_code = '" + ccdP + "' and luch.ccs_code='" + fixCCSCode1 + "'";
                DataTable dtbm1 = DB.getDataTable(counPKluBM);
                if (dtbm1.Rows.Count > 0)
                {
                    return "JSBM";
                }
                else
                {
                    string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                    DataTable dt4 = DB.getDataTable(sysdtl1pk);
                    if (dt4.Rows.Count > 0)
                    {
                        return "K";
                    }
                    else
                    {
                        string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                        DataTable dt5 = DB.getDataTable(sysdtl2pk);
                        if (dt5.Rows.Count > 0)
                        {
                            return "L";
                        }
                        else
                        {
                            string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                            DataTable dt6 = DB.getDataTable(laagpk);
                            if (dt6.Rows.Count > 0)
                            {
                                return "M";
                            }
                            else
                            {
                                string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                                DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                                if (dt7.Rows.Count > 0)
                                {
                                    return "O";
                                }
                                else
                                {
                                    string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                    DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                    if (dt8.Rows.Count > 0)
                                    {
                                        return "P";
                                    }
                                    else
                                    {
                                        return "N";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public string validatedataluBM(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string counPKluBM = @"SELECT luch.USE_USERID FROM LUCH_USERCHANNEL luch where luch.use_userid = '" + bmuserid + "' and luch.cch_code = '" + cchP + "' and luch.ccd_code = '" + ccdP + "' and luch.ccs_code='" + fixCCSCode1 + "'";
            DataTable dtbm1 = DB.getDataTable(counPKluBM);
            if (dtbm1.Rows.Count > 0)
            {
                return "JSBM";
            }

            else
            {
                string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
                DataTable dt4 = DB.getDataTable(sysdtl1pk);
                if (dt4.Rows.Count > 0)
                {
                    return "K";
                }
                else
                {
                    string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                    DataTable dt5 = DB.getDataTable(sysdtl2pk);
                    if (dt5.Rows.Count > 0)
                    {
                        return "L";
                    }
                    else
                    {
                        string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                        DataTable dt6 = DB.getDataTable(laagpk);
                        if (dt6.Rows.Count > 0)
                        {
                            return "M";
                        }
                        else
                        {
                            string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                            DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                            if (dt7.Rows.Count > 0)
                            {
                                return "O";
                            }
                            else
                            {
                                string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                                DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                                if (dt8.Rows.Count > 0)
                                {
                                    return "P";
                                }
                                else
                                {
                                    return "N";
                                }
                            }
                        }
                    }
                }
            }
        }

        public string validatedata4(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string sysdtl1pk = @"SELECT SD1.CSD_TYPE FROM LCSD_SYSTEMDTL SD1 where SD1.CSH_ID='CHAGT' AND SD1.CSD_TYPE = '" + sysdtl1 + "' ";
            DataTable dt4 = DB.getDataTable(sysdtl1pk);
            if (dt4.Rows.Count > 0)
            {
                return "K";
            }
            else
            {
                string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
                DataTable dt5 = DB.getDataTable(sysdtl2pk);
                if (dt5.Rows.Count > 0)
                {
                    return "L";
                }
                else
                {
                    string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                    DataTable dt6 = DB.getDataTable(laagpk);
                    if (dt6.Rows.Count > 0)
                    {
                        return "M";
                    }
                    else
                    {
                        string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                        DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                        if (dt7.Rows.Count > 0)
                        {
                            return "O";
                        }
                        else
                        {
                            string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                            DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                            if (dt8.Rows.Count > 0)
                            {
                                return "P";
                            }
                            else
                            {
                                return "N";
                            }
                        }
                    }
                }
            }
            //}
            //}
            //}
        }

        public string validatedata5(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string sysdtl2pk = @"SELECT SD2.CSD_TYPE FROM LCSD_SYSTEMDTL SD2 where SD2.CSH_ID='CHBBR' AND SD2.CSD_TYPE = '" + sysdtl2 + "' ";
            DataTable dt5 = DB.getDataTable(sysdtl2pk);
            if (dt5.Rows.Count > 0)
            {
                return "L";
            }
            else
            {
                string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                DataTable dt6 = DB.getDataTable(laagpk);
                if (dt6.Rows.Count > 0)
                {
                    return "M";
                }
                else
                {
                    string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                    DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                    if (dt7.Rows.Count > 0)
                    {
                        return "O";
                    }
                    else
                    {
                        string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                        DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                        if (dt8.Rows.Count > 0)
                        {
                            return "P";
                        }
                        else
                        {
                            return "N";
                        }
                    }
                }
            }
            //}
            //}
            //}
            //}
        }

        public string validatedata6(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string laagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
            DataTable dt6 = DB.getDataTable(laagpk);
            if (dt6.Rows.Count > 0)
            {
                return "M";
            }
            else
            {
                string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
                DataTable dt7 = GetdataOraOledb(ilasgetlogo);
                if (dt7.Rows.Count > 0)
                {
                    return "O";
                }
                else
                {
                    string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                    DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                    if (dt8.Rows.Count > 0)
                    {
                        return "P";
                    }
                    else
                    {
                        return "N";
                    }
                }
            }
            //}
            //}
            //}
            //}
            //}
        }

        public string validatedata7(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string ilasgetlogo = @"SELECT pb.pbb_branchcode from PBB_BANKBRANCH pb where pb.pbk_bankcode= '" + vallogo + "' AND pb.pbb_branchcode = '" + brCdP + "'";
            DataTable dt7 = GetdataOraOledb(ilasgetlogo);
            if (dt7.Rows.Count > 0)
            {
                return "O";
            }
            else
            {
                string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                if (dt8.Rows.Count > 0)
                {
                    return "P";
                }
                else
                {
                    return "N";
                }
            }
            //}
            //}
            //}
            //}
            //}
            //}
        }

        public string validatedata8(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            //select* from PBA_BANKACCOUNT ba where ba.pbk_bankcode = 'BOP' and ba.pbb_branchcode in ('2508');
            string ilasgetpba = @"SELECT pa.pbb_branchcode from PBA_BANKACCOUNT pa where pa.pbk_bankcode= '" + vallogo + "' AND pa.pbb_branchcode = '" + brCdP + "'";
            DataTable dt7 = GetdataOraOledb(ilasgetpba);
            if (dt7.Rows.Count > 0)
            {
                return "Oa";
            }
            else
            {
                string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
                DataTable dt8 = GetdataOraOledb(ilaslaagpk);
                if (dt8.Rows.Count > 0)
                {
                    return "P";
                }
                else
                {
                    return "N";
                }
            }
        }

        public string validatedata9(string cchP, string ccdP, string brCdP, string fixCCSCode1, string bsouserid, string bmuserid, string sysdtl1, string sysdtl2, string vallogo)
        {
            string ilaslaagpk = @"SELECT ag.aag_agcode FROM laag_agent ag where ag.aag_agcode = '" + sysdtl1 + "' ";
            DataTable dt8 = GetdataOraOledb(ilaslaagpk);
            if (dt8.Rows.Count > 0)
            {
                return "P";
            }
            else
            {
                return "N";
            }
        }

        protected void assessment()
        {


            string respad = "";
            for (int i = 1; i <= 13; i++)
            {

                int ileng = i.ToString().Length;
                if (ileng == 1)
                {
                    respad = i.ToString().PadLeft(2, '0');
                }
                else if (ileng == 2)
                {
                    respad = i.ToString();
                }

                try
                {
                    string queryBOP1 = "insert into LACH_ASSESSMENT (Cqn_Code,\n" +
                    "Cqn_Type,\n" +
                    "Cch_Code,\n" +
                    "Ccd_Code,\n" +
                    "Ccs_Code)\n" +
                    "values(\n" +
                    " '" + "BP" + respad + "', \n" +
                    " '" + "1" + "', \n" +
                    " '" + columnNameValue.getObject("cch_code") + "', \n" +
                    " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                    " '" + fixCCSCode + "' \n" +
                    ")";
                    DB.executeDML(queryBOP1);
                }
                catch (Exception)
                {
                    lblAlert.ForeColor = System.Drawing.Color.Red;
                    lblAlert.Text = ""; // + ex.Message;
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record already found','')", true);
                }
            }
        }



        #region "Mycode"

        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSNILAS"]);
            return conOra;
        }

        private OleDbConnection GetConn1()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //BrPanel.Visible = true;
            //pnlMapping.Visible = false;
            //pnlExl.Visible
            string UserBSO_BCO;
            if (ddlCCD_CHANNELDTLCD.SelectedValue.ToString() == "F")
            {
                UserBSO_BCO = "BCO";
            }
            else
            {
                UserBSO_BCO = "BSO";
            }
            String bkName = "";
            string strQry = "select ccd_logo from ccd_channeldetail where cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "'";
            rowset rstQry = DB.executeQuery(strQry);
            if (rstQry.next())
            {
                bkName = rstQry.getString("ccd_logo");
            }

            //string queryexport1 = @"select ccs_code, ccs_descr, ccs_field1 from ccs_chanlsubdetl where cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
            string queryexport1 = @"select CS.CCS_FIELD1 Branch_Code, CS.CCS_DESCR Branch_Name, u.use_userid BSO_USER, u1.use_userid BM_USER "+
                "from CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS, use_usermaster u, use_usermaster u1 " +
                "where ch.cch_code=cd.cch_code and cd.cch_code=cs.cch_code and cd.ccd_code=cs.ccd_code  " +
                "and(substr(u.use_userid, 1, 3) = '" + UserBSO_BCO + "' " +
                "and substr(u.use_userid, 4, 1) = cd.cch_code " +
                "and substr(u.use_userid, 5, 1) = cd.ccd_code " +
                "and substr(u.use_userid, 6, 4) = cs.ccs_field1) " +
                "and(substr(u1.use_userid, 1, 2) = 'BM' " +
                "and substr(u1.use_userid, 3, 1) = cd.cch_code " +
                "and substr(u1.use_userid, 4, 1) = cd.ccd_code " +
                "and substr(u1.use_userid, 5, 4) = cs.ccs_field1) " +
                "and ch.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and cs.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' and cs.ccs_field1 not in ('000','0000') order by to_number(cs.ccs_field1) ";

            DataTable dt = DB.getDataTable(queryexport1);
            if (dt.Rows.Count > 0)
            {
                //ExportGridToExcel(dtf);
                //dt = GetdataOraOledb(Sql);

                // Update the branch code in the DataTable to ensure leading zeros
                //string bkName = dt.Rows[0]["bknam"].ToString();
                foreach (DataRow row in dt.Rows)
                {
                    row["Branch_Code"] = row["Branch_Code"].ToString().PadLeft(4, '0');
                }

                // Create an Excel package
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Add a worksheet to the package
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Load the DataTable into the worksheet, starting from cell A1
                    worksheet.Cells.LoadFromDataTable(dt, true);

                    // Format the entire "A" column (BranchCode) to show four digits with leading zeros
                    worksheet.Cells["A:A"].Style.Numberformat.Format = "0000";
                    // Align right in Excel
                    worksheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //string filename = "downloadBranches" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                    // Save the package to a file
                    string filePath = Server.MapPath("~/Presentation/GenerateBranches.xlsx");
                    //string filePath = Server.MapPath("~/Presentation/" + filename + ".xlsx");

                    //  File.Delete(filePath);

                    // Directory.CreateDirectory(Path.GetDirectoryName(filePath));


                    package.SaveAs(new FileInfo(filePath));


                    //  Label2.Text = "Excel file saved as 'BranchCodes.xlsx";
                    // File download
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=BranchDetail_" + bkName + "CONV" + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
                    Response.TransmitFile(filePath);
                    Response.End();
                }
            }
            else
            {
                //lblAlert.ForeColor = System.Drawing.Color.Red;
                //lblAlert.Text = "Record Already Found....."; // + ex.Message;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                    "swal('','No Records Found...','')", true);
            }
        }



        #endregion

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

        public string DML1(string sql)
        {
            try
            {
                CmdDML.CommandText = sql;
                CmdDML.Connection = GetConn1();
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

        protected void SaveRec()
        {
            //DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);
            //DateTime crdt =  new DateTime(2008, 3, 9, 16, 5, 7, 123);
            DateTime _date;
            string day = "";
            NameValueCollection columnNameValue = new NameValueCollection();
            string brlen = txtBranchCode.Text;
            int vbrlen = brlen.Length;
            if (vbrlen == 1)
            {
                v_txtBranchCode = "000" + txtBranchCode.Text;
            }
            else if (vbrlen == 2)
            {
                v_txtBranchCode = "00" + txtBranchCode.Text;
            }
            else if (vbrlen == 3)
            {
                v_txtBranchCode = "0" + txtBranchCode.Text;
            }
            else
            {
                v_txtBranchCode = txtBranchCode.Text;
            }

            txtBranchCode.Text = v_txtBranchCode;
            string use_userid_t4BSO = "";

            columnNameValue.Add("cch_code", ddlCCH_CHANNELCD.SelectedValue.Trim() == "" ? null : ddlCCH_CHANNELCD.SelectedValue); //.Trim().Split('-')[0]);
            columnNameValue.Add("ccd_code", ddlCCD_CHANNELDTLCD.SelectedValue.Trim() == "" ? null : ddlCCD_CHANNELDTLCD.SelectedValue);
            columnNameValue.Add("brCode", txtBranchCode.Text.Trim() == "" ? null : txtBranchCode.Text);
            columnNameValue.Add("brName", txtBranchName.Text.Trim() == "" ? null : txtBranchName.Text);

            //string chkdupli = "select count(*) chkcnt from ccs_chanlsubdetl where cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
            //    "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'  and ccs_field1 =  '" + v_txtBranchCode + "' ";
            //rowset chkdupli1 = DB.executeQuery(chkdupli);
            //if (!chkdupli1.next())
            //{

            if (ddlCCD_CHANNELDTLCD.SelectedValue.ToString() == "F")
            {
                use_userid_t4BSO = "BCO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;
            }
            else
            {
                use_userid_t4BSO = "BSO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;
            }
            string use_userid_t4BM = "BM" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;

            //string use_passwordBSO = EncodePasswordToBase64(use_userid_t4BSO);
            //string use_passwordBM = EncodePasswordToBase64(use_userid_t4BM);

            string use_password = "bca864d09031109d726859a3fd8458c1";

            columnNameValue.Add("use_userid_t4BSO", use_userid_t4BSO == "" ? null : use_userid_t4BSO);
            columnNameValue.Add("use_userid_t4BM", use_userid_t4BM == "" ? null : use_userid_t4BM);
            columnNameValue.Add("ccs_field1_t2", txtBranchCode.Text.Trim() == "" ? null : txtBranchCode.Text);
            String ccsauto = "";
            //check number or character
            string chkNumChr = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL  and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
                    "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'  ";
            rowset chkNumChr1 = DB.executeQuery(chkNumChr);
            if (chkNumChr1.next())
            {
                //                string chkNC = chkNumChr1.getString("ccscode");
                string strAlpha = "";
                string jnumber = "";
                bool exitedInner = false;
                for (int i = 65; i <= 90; i++)
                {
                    strAlpha = ((char)i).ToString();
                    for (int j = 1; j <= 99; j++)
                    {
                        int jlen = j.ToString().Length;
                        if (jlen == 1)
                        {
                            jnumber = "0" + j.ToString();
                        }
                        else
                        {
                            jnumber = j.ToString();
                        }
                        string finSeq = strAlpha + jnumber;
                        //string finSeq = "A51"; //for testing
                        string strQry1a = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
                                "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code = '" + finSeq + "' order by ccs_code";
                        rowset rstQry1a = DB.executeQuery(strQry1a);

                        if (rstQry1a.next())
                        {
                            //ccsauto = rstQry1.getString("ccs_code");  //"999"
                        }
                        else
                        {
                            fixCCSCode = finSeq.ToString();   //resultnum.ToString();
                            exitedInner = true;
                            break;
                        }
                    }
                    if (exitedInner == true)
                    {
                        break;
                    }

                }


            }

            string chkNumChrN = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
                    "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code <= '" + "999" + "' and ccs_code != '" + "0" + "'  and ccs_code != '" + "00" + "' "; // and ccs_code != '" + "000" + "' ";
            rowset chkNumChr11 = DB.executeQuery(chkNumChrN);
            if (chkNumChr11.next())
            {
                string chkNC = chkNumChr11.getString("ccscode"); //001
                int chkNC1 = Convert.ToInt16(chkNumChr11.getString("ccscode")); //1
                if (chkNC1 >= 0 && chkNC1 <= 999) //==115--noman  select chr(ASCII('A') + level - 1) as alphafrom from dual connect by level <=26;   
                                                  //if (chkNC1 == 999)    for testing
                {
                    //findNumber();
                    for (int i = 1; i <= 999; i++)
                    {
                        string str = i.ToString();
                        string resultnum = str.PadLeft(3, '0').ToString();

                        string strQry1 = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
                            "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code = '" + resultnum + "' ";
                        rowset rstQry1 = DB.executeQuery(strQry1);
                        if (rstQry1.next())
                        {
                            //ccsauto = rstQry1.getString("ccs_code");  //"999"
                        }
                        else
                        {
                            ccsauto = resultnum.ToString();
                            break;
                        }
                    }
                    if (int.TryParse(ccsauto, out int ccsauto1))
                    {
                        // Add 1 to the integer value
                        int newValue = ccsauto1; // + 1;
                        if (newValue > 0)
                        {
                            string v = newValue.ToString();
                            int vlen = v.Length;
                            if (vlen == 1)
                            {
                                fixCCSCode = "00" + v;
                            }
                            else if (vlen == 2)
                            {
                                fixCCSCode = "0" + v;
                            }
                            else //if (vlen == 3)
                            {
                                fixCCSCode = v;
                            }
                        }
                        else
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "error1";// + ex.Message";
                        }
                    }

                }
            }




            string CSD_TYPE_t7 = ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + fixCCSCode;

            columnNameValue.Add("CSD_TYPE_t7", CSD_TYPE_t7 == "" ? null : CSD_TYPE_t7);

            String logo1 = "";
            string strQry = "select ccd_logo from ccd_channeldetail where cch_code = '" + columnNameValue.getObject("cch_code") + "' and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'";
            rowset rstQry = DB.executeQuery(strQry);
            if (rstQry.next())
            {
                logo1 = rstQry.getString("ccd_logo");
            }
            string logo2 = logo1 + "" + txtBranchCode.Text;
            string ccsDesc = logo1 + "-" + columnNameValue.getObject("brName");


            ///////////////////////////////////use substr			
            String bnkcodeGet = "";

            String strQrybnkcodeGet = "Select distinct decode(pbk_bankcode, 'UBL', '901', 'SMBL', '907', 'BAL', '903', 'NBP', '904', 'NIB', '905', 'SBL', '906', 'SILK', '908', 'BOP', '909', 'DIB', '910', 'HBL', '912', 'FBL', '913', 'JSB', '910', '000') bCode from LAAG_AGENT a where pbk_bankcode = '" + logo1 + "'";

            //String strQrybnkcodeGet = "select ccd.ccd_code bCode from ccd_channeldetail ccd where ccd.ccd_code = REGEXP_REPLACE(ccd.ccd_code, '[A-Z]+', '') and ccd.ccd_logo = '" + logo1 + "'";
            rowset rstQrybnkcodeGet = DB.executeQuery(strQrybnkcodeGet);
            if (rstQrybnkcodeGet.next())
            {
                bnkcodeGet = rstQrybnkcodeGet.getString("bCode");
            }

            string AAG_IMEDSUPR_12t = bnkcodeGet + "" + "0000";

            columnNameValue.Add("AAG_IMEDSUPR_12t", AAG_IMEDSUPR_12t == "" ? null : AAG_IMEDSUPR_12t);


            //String getaggcode = "";
            //String strQrygetaggcode = "Select to_char(max(aag_agcode)) vacode from LAAG_AGENT where pbk_bankcode = '" + logo1 + "'";
            //rowset rstQrygetaggcode = DB.executeQuery(strQrygetaggcode);
            //if (rstQrygetaggcode.next())
            //{
            //    getaggcode = rstQrygetaggcode.getString("vacode");
            //}

            string aag_agcode_t4 = bnkcodeGet + "" + txtBranchCode.Text;
            string ermsg = String.Empty;
            string cchP = ddlCCH_CHANNELCD.SelectedValue;
            string ccdP = ddlCCD_CHANNELDTLCD.SelectedValue;
            string brCdP = txtBranchCode.Text;
            string ans;

            if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "" && txtBranchCode.Text != null && txtBranchCode.Text != "" && txtBranchName.Text != null && txtBranchName.Text != "") // && txtAgencyCode.Text != null && txtAgencyCode.Text != "")
            {
                string queryccsField = @"SELECT s.ccs_code, s.cch_code, s.ccd_code  FROM ccs_chanlsubdetl s where ccs_field1 = '" + txtBranchCode.Text + "' and s.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
                DataTable dtf = DB.getDataTable(queryccsField);

                if (dtf.Rows.Count <= 0)
                {
                    ans = validatedata(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "G" || ans == "H")
                    {
                        lblAlert.ForeColor = System.Drawing.Color.Red;
                        lblAlert.Text = "";
                        //insert into log-table
                    }
                    else
                    {
                        try
                        {
                            string sqlString1 = "insert into ccs_chanlsubdetl (CCS_CODE,\n" +
                                "CCH_CODE,\n" +
                                "CCD_CODE,\n" +
                                "CCS_DESCR,\n" +
                                "CCS_FIELD1)\n" +
                                "values(\n" +
                                " '" + fixCCSCode + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                 " '" + ccsDesc + "', \n" +
                                " '" + columnNameValue.getObject("brCode") + "' \n" +
                                ")";
                            DB.executeDML(sqlString1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;

                        }

                        try
                        {
                            string sqlString2a = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                "cch_code,\n" +
                                "ccd_code,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + "003" + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString2a);


                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;
                                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-003','')", true);
                        }

                        try
                        {
                            string sqlString2b = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                "cch_code,\n" +
                                "ccd_code,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + "005" + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString2b);


                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;
                                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-005','')", true);
                        }

                        try
                        {
                            string sqlString2c = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                "cch_code,\n" +
                                "ccd_code,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + "074" + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString2c);


                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;
                                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-074','')", true);
                        }

                        try
                        {
                            string sqlString2d = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                "cch_code,\n" +
                                "ccd_code,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + "019" + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString2d);


                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;
                                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-019','')", true);
                        }

                        try
                        {
                            string sqlString2e = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                "cch_code,\n" +
                                "ccd_code,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + "075" + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString2e);


                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = ""; // + ex.Message;                            
                        }


                    }

                    if (ans == "I")
                    {
                        //insert into log-table
                        try
                        {
                            string qryupdusemBSO = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
                                "use_name = '" + columnNameValue.getObject("use_userid_t4BSO") + "', " +
                                "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
                                "use_password = '" + use_password + "', " +
                                "use_type = '" + "S" + "', " +
                                "aag_agcode = '" + aag_agcode_t4 + "', " +
                                "use_active = '" + "Y" + "' " +
                                "where use_userid = '" + columnNameValue.getObject("use_userid_t4BSO") + "'";
                            DB.executeDML(qryupdusemBSO);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Error in updateusermaster','')", true);
                        }
                        try
                        {
                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm1 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - use_usermaster-BSO',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }


                    }
                    else
                    {
                        try
                        {
                            string sqlString2 = "insert into use_usermaster (use_userid,\n" +
                                "pcl_locatcode,\n" +
                                "use_name,\n" +
                                "use_jobdescrip,\n" +
                                "use_password,\n" +
                                "use_type,\n" +
                                "aag_agcode,\n" +
                                "use_active)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
                                " '" + "180" + "', \n" +
                                " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
                                " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
                                " '" + use_password + "', \n" +
                                " '" + "S" + "', \n" +
                                " '" + aag_agcode_t4 + "', \n" +
                                " '" + "Y" + "' \n" +
                                ")";
                            DB.executeDML(sqlString2);


                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    ans = validatedata1(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "Ia")
                    {
                        //insert into log-table
                        try
                        {
                            string qryupdusemBM = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
                                "use_name = '" + columnNameValue.getObject("use_userid_t4BM") + "', " +
                                "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
                                "use_password = '" + use_password + "', " +
                                "use_type = '" + "M" + "', " +
                                "aag_agcode = '" + aag_agcode_t4 + "', " +
                                "use_active = '" + "Y" + "' " +
                                "where use_userid = '" + columnNameValue.getObject("use_userid_t4BM") + "'";
                            DB.executeDML(qryupdusemBM);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm2 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - use_usermaster-BM',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm2);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                    }
                    else
                    {
                        try
                        {
                            string sqlString3 = "insert into use_usermaster (use_userid,\n" +
                                "pcl_locatcode,\n" +
                                "use_name,\n" +
                                "use_jobdescrip,\n" +
                                "use_password,\n" +
                                "use_type,\n" +
                                "aag_agcode,\n" +
                                "use_active)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
                                " '" + "180" + "', \n" +
                                " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
                                " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
                                " '" + use_password + "', \n" +
                                " '" + "M" + "', \n" +
                                " '" + aag_agcode_t4 + "', \n" +
                                " '" + "Y" + "' \n" +
                                ")";
                            DB.executeDML(sqlString3);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    ans = validatedata2(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "J")
                    {
                        //insert into log-table
                        //update tables for both BSO and BM users
                        try
                        {
                            string qryupdusconBSO = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
                                "where use_userid = '" + columnNameValue.getObject("use_userid_t4BSO") + "' " +
                                "and CCN_CTRYCD = '" + "001" + "' ";
                            DB.executeDML(qryupdusconBSO);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm3 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LUCN_USERCOUNTRY-BSO',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm3);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    else
                    {
                        try
                        {
                            string sqlString4 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
                                "CCN_CTRYCD,\n" +
                                "UCN_DEFAULT)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
                                " '" + "001" + "', \n" +
                                " '" + "Y" + "' \n" +
                                ")";
                            DB.executeDML(sqlString4);

                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    ans = validatedata3(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "Ja")
                    {
                        //insert into log-table
                        //update tables for both BSO and BM users

                        try
                        {
                            string qryupdusconBM = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
                                "where use_userid = '" + columnNameValue.getObject("use_userid_t4BM") + "' " +
                                "and CCN_CTRYCD = '" + "001" + "' ";
                            DB.executeDML(qryupdusconBM);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm4 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LUCN_USERCOUNTRY-BM',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm4);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }


                    }
                    else
                    {

                        try
                        {
                            string sqlString5 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
                                "CCN_CTRYCD,\n" +
                                "UCN_DEFAULT)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
                                " '" + "001" + "', \n" +
                                " '" + "Y" + "' \n" +
                                ")";
                            DB.executeDML(sqlString5);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }

                    ans = validatedataluBSO(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "JSBO")
                    {
                        //insert into log-table
                        //update tables for both BSO    counPKluBSO1
                        try
                        {
                            string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
                                ", uch_default= '" + "Y" + "' " +
                                ", ccd_code = '" + ccdP + "' " +
                                "where use_userid = '" + use_userid_t4BSO + "' " +
                                "and ccs_code = '" + fixCCSCode + "' ";
                            DB.executeDML(qryupdsysdtl1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {
                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm5 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - luch_userchannel-BSO',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm5);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }


                    }
                    else
                    {
                        try
                        {
                            string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
                                "CCH_CODE,\n" +
                                "UCH_DEFAULT,\n" +
                                "CCD_CODE,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + "Y" + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString8);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }

                    ans = validatedataluBM(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "JSBM")
                    {
                        //insert into log-table
                        //update tables for both BSO 
                        try
                        {
                            string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
                                ", uch_default= '" + "Y" + "' " +
                                ", ccd_code = '" + ccdP + "' " +
                                "where use_userid = '" + use_userid_t4BM + "' " +
                                "and ccs_code = '" + fixCCSCode + "' ";
                            DB.executeDML(qryupdsysdtl1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm5 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - luch_userchannel-BSO',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm5);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }


                    }
                    else
                    {
                        try
                        {
                            string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
                                "CCH_CODE,\n" +
                                "UCH_DEFAULT,\n" +
                                "CCD_CODE,\n" +
                                "CCS_CODE)\n" +
                                "values(\n" +
                                " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
                                " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                " '" + "Y" + "', \n" +
                                " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                " '" + fixCCSCode + "' \n" +
                                ")";
                            DB.executeDML(sqlString8);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    ans = validatedata4(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "K")
                    {
                        //insert into log-table
                        //update tables for both BSO 
                        try
                        {
                            string qryupdsysdtl1 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + aag_agcode_t4 + "' " +
                                "where CSH_ID = '" + "CHAGT" + "' " +
                                "and CSD_TYPE = '" + aag_agcode_t4 + "' ";
                            DB.executeDML(qryupdsysdtl1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm5 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LCSD_SYSTEMDTL-CHAGT',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm5);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }


                    }
                    else
                    {
                        try
                        {
                            string sqlString8 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
                                "CSD_TYPE,\n" +
                                "CSD_VALUE)\n" +
                                "values(\n" +
                                " '" + "CHAGT" + "', \n" +
                                " '" + aag_agcode_t4 + "', \n" +
                                " '" + aag_agcode_t4 + "' \n" +
                                ")";
                            DB.executeDML(sqlString8);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }

                    ans = validatedata5(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "L")
                    {
                        //insert into log-table
                        //update tables for both BM users
                        try
                        {
                            string qryupdsysdtl2 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + columnNameValue.getObject("ccs_field1_t2") + "' " +
                                "where CSH_ID = '" + "CHBBR" + "' " +
                                "and CSD_TYPE = '" + CSD_TYPE_t7 + "' ";
                            DB.executeDML(qryupdsysdtl2);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm6 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LCSD_SYSTEMDTL-CHBBR',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm6);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                    }
                    else
                    {
                        try
                        {
                            string sqlString9 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
                                "CSD_TYPE,\n" +
                                "CSD_VALUE)\n" +
                                "values(\n" +
                                " '" + "CHBBR" + "', \n" +
                                " '" + CSD_TYPE_t7 + "', \n" +
                                " '" + columnNameValue.getObject("ccs_field1_t2") + "' \n" +
                                ")";
                            DB.executeDML(sqlString9);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    ans = validatedata6(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "M")
                    {
                        try
                        {
                            string qryupdlaag1 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
                                "PBK_BANKCODE = '" + logo1 + "', " +
                                "PCM_COMPCODE = '" + "01" + "', " +
                                "CHL_LEVEL = '" + "001" + "', " +
                                "PCL_LOCATCODE = '" + "180" + "', " +
                                "CDG_DESIGCODE = '" + "500" + "', " +
                                "CRG_RELGCD = '" + "999" + "', " +
                                "AAG_NAME = '" + logo1 + "-" + txtBranchName.Text + "', " +
                                "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
                                "AAG_IMEDSUPR = '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', " +
                                "EXT_NACTIVE = '" + "Y" + "', " +
                                "AAG_DIRECT = '" + "N" + "', " +
                                "AAG_BROKER = '" + "B" + "', " +
                                "AAG_STATUS = '" + "C" + "', " +
                                "AAG_SALARIED = '" + "N" + "', " +
                                "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
                                "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
                            DB.executeDML(qryupdlaag1);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm7 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LAAG_AGENT-POS',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm7);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                    }
                    else
                    {
                        //_date = DateTime.Parse("5/13/2012");
                        //day = DateTime.Parse("5/13/2012").ToString("dd-MMM-yyyy");
                        try
                        {
                            string sqlString10 = "insert into LAAG_AGENT (AAG_AGCODE,CNT_NATCD,PBK_BANKCODE,PCM_COMPCODE,CHL_LEVEL,PCL_LOCATCODE,CDG_DESIGCODE,CRG_RELGCD," +
                               " AAG_NAME,AAG_JOINDAT,AAG_IMEDSUPR,EXT_NACTIVE,AAG_DIRECT,AAG_BROKER,AAG_STATUS,AAG_SALARIED,AAG_SALEFCTDAT)" +
                               "values(" + aag_agcode_t4 + ",'586','" + logo1 + "','01','001','180','500','999','" + logo1 + "-" + txtBranchName.Text + "',to_date('04/01/2022','MM/dd/yyyy') " +
                               ",'" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "','Y','N','B','C','N',to_date('04/01/2022','MM/dd/yyyy'))";
                            DB.executeDML(sqlString10);

                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }

                    {
                        string queryBOP = @"SELECT s.ccs_code, s.cch_code, s.ccd_code, s.cqn_type FROM LACH_ASSESSMENT s where s.ccs_code = '" + fixCCSCode + "' and s.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
                        DataTable dtla = DB.getDataTable(queryBOP);
                        //if (ddlCCD_CHANNELDTLCD.SelectedValue == "9" || ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0)
                        if ((ddlCCD_CHANNELDTLCD.SelectedValue == "9" && dtla.Rows.Count <= 0) || (ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0))
                        {
                            //assessment();
                            string respad = "";
                            for (int i = 1; i <= 13; i++)
                            {

                                int ileng = i.ToString().Length;
                                if (ileng == 1)
                                {
                                    respad = i.ToString().PadLeft(2, '0');
                                }
                                else if (ileng == 2)
                                {
                                    respad = i.ToString();
                                }

                                try
                                {
                                    string queryBOP1 = "insert into LACH_ASSESSMENT (Cqn_Code,\n" +
                                    "Cqn_Type,\n" +
                                    "Cch_Code,\n" +
                                    "Ccd_Code,\n" +
                                    "Ccs_Code)\n" +
                                    "values(\n" +
                                    " '" + "BP" + respad + "', \n" +
                                    " '" + "1" + "', \n" +
                                    " '" + columnNameValue.getObject("cch_code") + "', \n" +
                                    " '" + columnNameValue.getObject("ccd_code") + "', \n" +
                                    " '" + fixCCSCode + "' \n" +
                                    ")";
                                    DB.executeDML(queryBOP1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                        }
                    }


                    ans = validatedata7(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "O")
                    {
                        //insert into log-table
                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string qryupdbkbr = "update PBB_BANKBRANCH set CCN_CTRYCD = '" + "001" + "', " +
                                "CCT_CITYCD = '" + "001" + "', " +
                                "PBB_BRANCHNAME = '" + ccsDesc + "', " +
                                "PBB_DDSTEXTFILE = '" + "N" + "' " +
                                "where PBK_BANKCODE = '" + logo1 + "' " +
                                "and PBB_BRANCHCODE = '" + columnNameValue.getObject("brCode") + "' ";
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
                            int x = dbCom.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm8 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - PBB_BANKBRANCH - ILAS',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + columnNameValue.getObject("brCode") + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm8);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    else
                    {
                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string sqlString11 = "insert into PBB_BANKBRANCH (PBK_BANKCODE,\n" +
                                "PBB_BRANCHCODE,\n" +
                                "CCN_CTRYCD,\n" +
                                "CCT_CITYCD,\n" +
                                "PBB_BRANCHNAME,\n" +
                                "PBB_DDSTEXTFILE)\n" +
                                "values(\n" +
                                " '" + logo1 + "', \n" +
                                " '" + columnNameValue.getObject("brCode") + "', \n" +
                                " '" + "001" + "', \n" +
                                " '" + "001" + "', \n" +
                                " '" + ccsDesc + "', \n" +
                                " '" + "N" + "' \n" +
                                ")";
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(sqlString11, dbCon);
                            int x = dbCom.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        finally
                        {
                            dbCon.Close();
                        }
                    }
                    /////////////////////////

                    string pcaAccount = "select lc.csd_value pcaAcc from lcsd_systemdtl lc where lc.csh_id = 'BKPOS' and lc.csd_type = '" + logo1 + "'";

                    DataTable dt_new = new DataTable();
                    dt_new = GetdataOraOledb(pcaAccount);

                    if (dt_new.Rows.Count > 0)
                    {
                        pcaAccount = dt_new.Rows[0]["pcaAcc"].ToString();
                    }
                    else
                    {
                        pcaAccount = "";
                    }

                    ans = validatedata8(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "Oa")
                    {


                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;

                        try
                        {
                            string qryupdbkbr = "update PBA_BANKACCOUNT set PCM_COMPCODE = '" + "01" + "', " +
                                "PCA_ACCOUNT = '" + pcaAccount + "', " +
                                "PCU_CURRCODE = '" + "1" + "', " +
                                "PBA_ACTIVE = '" + "Y" + "', " +
                                "PCL_LOCATCODE = '" + "180" + "' " +
                                "where PBK_BANKCODE = '" + logo1 + "' " +
                                "and PBB_BRANCHCODE = '" + columnNameValue.getObject("brCode") + "' " +
                                "and PBA_SERIAL = '" + "1" + "' ";
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
                            int x = dbCom.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";

                        }

                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm9 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - PBA_BANKACCOUNT - ILAS',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + columnNameValue.getObject("brCode") + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm9);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    else
                    {

                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string sqlString12 = "insert into PBA_BANKACCOUNT (PBK_BANKCODE,\n" +
                                "PBB_BRANCHCODE,\n" +
                                "PBA_SERIAL,\n" +
                                "PCM_COMPCODE,\n" +
                                "PCA_ACCOUNT,\n" +
                                "PCU_CURRCODE,\n" +
                                "PBA_ACTIVE,\n" +
                                "PCL_LOCATCODE)\n" +
                                "values(\n" +
                                " '" + logo1 + "', \n" +
                                " '" + columnNameValue.getObject("brCode") + "', \n" +
                                " '" + "1" + "', \n" +
                                " '" + "01" + "', \n" +
                                " '" + pcaAccount + "', \n" +   //PCA ACCOUNT check it <-------------------------
                                " '" + "1" + "', \n" +
                                " '" + "Y" + "', \n" +
                                " '" + "180" + "' \n" +
                                ")";
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(sqlString12, dbCon);
                            int x = dbCom.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        finally
                        {
                            dbCon.Close();
                        }
                    }



                    ans = validatedata9(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                    if (ans == "P")
                    {
                        lblAlert.ForeColor = System.Drawing.Color.Red;
                        lblAlert.Text = "";
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Already found','')", true);
                        //insert into log-table
                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string sqlString14 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
                                "PBK_BANKCODE = '" + logo1 + "', " +
                                "PCM_COMPCODE = '" + "01" + "', " +
                                "CHL_LEVEL = '" + "001" + "', " +
                                "PCL_LOCATCODE = '" + "180" + "', " +
                                "CDG_DESIGCODE = '" + "500" + "', " +
                                "CRG_RELGCD = '" + "999" + "', " +
                                "AAG_NAME = '" + logo1 + "-" + txtBranchName.Text + "', " +
                                "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
                                "AAG_IMEDSUPR = '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', " +
                                "EXT_NACTIVE = '" + "Y" + "', " +
                                "AAG_DIRECT = '" + "N" + "', " +
                                "AAG_BROKER = '" + "B" + "', " +
                                "AAG_STATUS = '" + "C" + "', " +
                                "AAG_SALARIED = '" + "N" + "', " +
                                "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
                                "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(sqlString14, dbCon);
                            int x = dbCom.ExecuteNonQuery();


                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }

                        try
                        {

                            string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                            string sqlComm10 = "insert into error_log\n" +
                            "  (process_type,\n" +
                            "   procedure_name,\n" +
                            "   error_date,\n" +
                            "   error_code,\n" +
                            "   error_msg,\n" +
                            "   doc_name,\n" +
                            "   username,\n" +
                            "   status_date)\n" +
                            "values\n" +
                            "  ('BNKBRNCH_UPLOAD',\n" +
                            "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                            "  sysdate,\n" +
                            "   '5010',\n" +
                            "  'Record already found - LAAG_AGENT-ILAS',\n" +
                            "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
                            "   '" + user + "',\n" +
                            "  sysdate)";
                            DB.executeDML(sqlComm10);
                        }
                        catch (Exception ex)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                    }
                    else
                    {
                        System.Data.OleDb.OleDbConnection dbCon = null;
                        System.Data.OleDb.OleDbDataAdapter dbAdapter;
                        System.Data.OleDb.OleDbCommand dbCom;
                        try
                        {
                            string sqlString13 = "insert into LAAG_AGENT (AAG_AGCODE,\n" +
                                "CNT_NATCD,\n" +
                                "PBK_BANKCODE,\n" +
                                "PCM_COMPCODE,\n" +
                                "CHL_LEVEL,\n" +
                                "PCL_LOCATCODE,\n" +
                                "CDG_DESIGCODE,\n" +
                                "CRG_RELGCD,\n" +
                                "AAG_NAME,\n" +
                                "AAG_JOINDAT,\n" +
                                "AAG_IMEDSUPR,\n" +
                                "EXT_NACTIVE,\n" +
                                "AAG_DIRECT,\n" +
                                "AAG_BROKER,\n" +
                                "AAG_STATUS,\n" +
                                "AAG_SALARIED,\n" +
                                "AAG_SALEFCTDAT)\n" +
                                "values(\n" +
                                " '" + aag_agcode_t4 + "', \n" +
                                " '" + "586" + "', \n" +
                                " '" + logo1 + "', \n" +
                                " '" + "01" + "', \n" +
                                " '" + "001" + "', \n" +
                                " '" + "180" + "', \n" +
                                " '" + "500" + "', \n" +
                                " '" + "999" + "', \n" +
                                " '" + logo1 + "-" + txtBranchName.Text + "', \n" +
                                " to_date('04/01/2022','MM/dd/yyyy'), \n" +
                                " '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', \n" +
                                " '" + "Y" + "', \n" +
                                " '" + "N" + "', \n" +
                                " '" + "B" + "', \n" +
                                " '" + "C" + "', \n" +
                                " '" + "N" + "', \n" +
                                " to_date('04/01/2022','MM/dd/yyyy') \n" +
                                ")";

                            string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                            dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                            dbCon.Open();
                            dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                            dbCom = new System.Data.OleDb.OleDbCommand(sqlString13, dbCon);
                            int x = dbCom.ExecuteNonQuery();

                            //lblAlert.ForeColor = System.Drawing.Color.Green;
                            //lblAlert.Text = "Recoed Saved... ";
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Recoed Saved','')", true);

                        }
                        catch (Exception)
                        {
                            lblAlert.ForeColor = System.Drawing.Color.Red;
                            lblAlert.Text = "";
                        }
                        finally
                        {
                            dbCon.Close();
                        }
                    }
                    String BSOUS = use_userid_t4BSO;
                    String BMUS = use_userid_t4BM;

                    string message = "Record Saved ";
                    string script = $@" swal({{title: '',text: '{message} - {BSOUS} - {BMUS}',}});";

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", script, true);
                }
                else
                {
                    //lblAlert.ForeColor = System.Drawing.Color.Red;
                    //lblAlert.Text = "Record Already Found....."; // + ex.Message;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                        "swal('','Record Already Found','')", true);
                }
            }

            else
            {
                //lblAlert.ForeColor = System.Drawing.Color.Red;
                //lblAlert.Text = "Blank Files are not Required...";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                    "swal('','Branch Code and Name must be required.','')", true);
            }

        }

        protected void btnMapp_Click(object sender, EventArgs e)
        {
            BrPanel.Visible = false;
            pnlBrMap.Visible = true;
            btnSave.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;
            btnExport.Enabled = false;
            btnUploadDisp.Enabled = false;
        }

        //protected void btnSubmitMapping_Click(object sender, EventArgs e)
        //{
        //    //string userInput = txtMappingInput.Text.Trim();


        //    //string query = @"SELECT u.use_userid userid, (SELECT cd.ccd_logo FROM ccd_channeldetail cd WHERE cd.cch_code = u.cch_code "+
        //    //    "AND cd.ccd_code = u.ccd_code) AS Bank_Name, c.ccs_field1 AS Br_code, c.ccs_descr AS Br_Name "+
        //    //    "FROM luch_userchannel u, ccs_chanlsubdetl c WHERE EXISTS(SELECT 1 FROM ccs_chanlsubdetl s "+
        //    //    "WHERE u.ccs_code = s.ccs_code AND u.ccd_code = s.ccd_code AND u.use_userid = '" + userInput + "') " +
        //    //    "AND c.ccs_code = u.ccs_code AND c.ccd_code = u.ccd_code ";

        //    //DataTable dt = DB.getDataTable(query);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    GMapping.DataSource = dt;
        //    //    GMapping.DataBind();                
        //    //}
        //}

        protected void btnBRMap_Click(object sender, EventArgs e)   //noman here
        {
            string userInput = txtBRMap.Text.Trim(); //txtMappingInput.Text.Trim();


            string query = @"SELECT u.use_userid userid, (SELECT cd.ccd_logo FROM ccd_channeldetail cd WHERE cd.cch_code = u.cch_code " +
                "AND cd.ccd_code = u.ccd_code) AS Bank_Name, c.ccs_field1 AS Br_code, c.ccs_descr AS Br_Name " +
                "FROM luch_userchannel u, ccs_chanlsubdetl c WHERE EXISTS(SELECT 1 FROM ccs_chanlsubdetl s " +
                "WHERE u.ccs_code = s.ccs_code AND u.ccd_code = s.ccd_code AND u.use_userid = '" + userInput + "') " +
                "AND c.ccs_code = u.ccs_code AND c.ccd_code = u.ccd_code ";

            DataTable dt = DB.getDataTable(query);
            if (dt.Rows.Count > 0)
            {
                brMapGrid.DataSource = dt;
                brMapGrid.DataBind();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                "swal('','No Records Found...','')", true);
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
                    btnSave.Visible = true;
                    Msg = DML("truncate table UPLOAD_DISPATCH");
                    string ConStr = "";
                    Path = Server.MapPath(FileUpload1.FileName);
                    FileUpload1.SaveAs(Path);
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

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
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////nomans
                    //required
                    //ccs_code      (symbol from excel)                                                 fixCCSCode
                    //cch_code      fix i.e. 2                                                          get_cchcode = columnNameValue.getObject("cch_code")
                    //ccd_code      (execute from excel bank-symbol)    symbol == "JSB" then 'F'        get_ccdcode = columnNameValue.getObject("ccd_code")
                    //ccs_descr     (Branch_Name from excel)                                            brname      = columnNameValue.getObject("brCode")                                         
                    //ccs_field1    (Branch_code from excel)                                            get_ccsfield1 = txtBranchCode.Text

                    //string chdtl = @"select cch_code, ccd_code, ccd_descr, ccd_logo from ccd_channeldetail where cch_code = '"+ ddlCCH_CHANNELCD.SelectedValue + "' " +
                    //    "and ccd_code = '"+ddlCCD_CHANNELDTLCD.SelectedValue+"' ";
                    //DataTable dt = DB.getDataTable(chdtl);
                    //{
                    //    string logo1 = dt.Rows[0]["ccd_logo"].ToString();
                    //}
                    //if (dt.Rows.Count > 0)

                    string get_cchcode = ddlCCH_CHANNELCD.SelectedValue.ToString();
                    String ccsauto = "";
                    for (int row = 0; row <= GridView1.Rows.Count - 1; row++)
                    {
                        //string symbol = GridView1.Rows[row].Cells[0].Text.ToString();
                        string get_ccsfield1 = GridView1.Rows[row].Cells[0].Text.ToString();
                        string brName = GridView1.Rows[row].Cells[1].Text.ToString();

                        //////  Get ccs_code  ///
                        ///

                        ///get_cchcode

                        ///

                        //////  Get ccd_code  ///done
                        //if (symbol == "UBL")
                        //    get_ccdcode = "1";
                        //else if (symbol == "FWB")
                        //    get_ccdcode = "2";
                        //else if (symbol == "BAL")
                        //    get_ccdcode = "3";
                        //else if (symbol == "NBP")
                        //    get_ccdcode = "4";
                        //else if (symbol == "NIB")
                        //    get_ccdcode = "5";
                        //else if (symbol == "SBL")
                        //    get_ccdcode = "6";
                        //else if (symbol == "SMBL")
                        //    get_ccdcode = "7";
                        //else if (symbol == "SILK")
                        //    get_ccdcode = "8";
                        //else if (symbol == "BOP")
                        //    get_ccdcode = "9";
                        //else if (symbol == "JSB")
                        //    get_ccdcode = "F";
                        //else if (symbol == "HBL")
                        //    get_ccdcode = "B";
                        string get_ccdcode = ddlCCD_CHANNELDTLCD.SelectedValue.ToString();
                        //////////////
                        ///check number or character - ccs_code
                        ///

                        string chkNumChr = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL  and cch_code = '" + get_cchcode + "' " +
                                "and ccd_code = '" + get_ccdcode + "'  ";
                        rowset chkNumChr1 = DB.executeQuery(chkNumChr);
                        if (chkNumChr1.next())
                        {
                            string strAlpha = "";
                            string jnumber = "";
                            bool exitedInner = false;
                            for (int i = 65; i <= 90; i++)
                            {
                                strAlpha = ((char)i).ToString();
                                for (int j = 1; j <= 99; j++)
                                {
                                    int jlen = j.ToString().Length;
                                    if (jlen == 1)
                                    {
                                        jnumber = "0" + j.ToString();
                                    }
                                    else
                                    {
                                        jnumber = j.ToString();
                                    }
                                    string finSeq = strAlpha + jnumber;
                                    //string finSeq = "A51"; //for testing
                                    string strQry1a = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL and cch_code = '" + get_cchcode + "' " +
                                            "and ccd_code = '" + get_ccdcode + "' and ccs_code = '" + finSeq + "' order by ccs_code";
                                    rowset rstQry1a = DB.executeQuery(strQry1a);

                                    if (rstQry1a.next())
                                    {
                                        //ccsauto = rstQry1.getString("ccs_code");  //"999"
                                    }
                                    else
                                    {
                                        fixCCSCode = finSeq.ToString();   //resultnum.ToString();
                                        exitedInner = true;
                                        break;
                                    }
                                }
                                if (exitedInner == true)
                                {
                                    break;
                                }

                            }


                        }

                        string chkNumChrN = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + get_cchcode + "' " +
                                "and ccd_code = '" + get_ccdcode + "' and ccs_code <= '" + "999" + "' and ccs_code != '" + "0" + "'  and ccs_code != '" + "00" + "' "; // and ccs_code != '" + "000" + "' ";
                        rowset chkNumChr11 = DB.executeQuery(chkNumChrN);
                        if (chkNumChr11.next())
                        {
                            string chkNC = chkNumChr11.getString("ccscode"); //001
                            int chkNC1 = Convert.ToInt16(chkNumChr11.getString("ccscode")); //1
                            if (chkNC1 >= 0 && chkNC1 <= 999) //==115--noman  select chr(ASCII('A') + level - 1) as alphafrom from dual connect by level <=26;   
                                                              //if (chkNC1 == 999)    for testing
                            {
                                //findNumber();
                                for (int i = 1; i <= 999; i++)
                                {
                                    string str = i.ToString();
                                    string resultnum = str.PadLeft(3, '0').ToString();

                                    string strQry1 = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + get_cchcode + "' " +
                                        "and ccd_code = '" + get_ccdcode + "' and ccs_code = '" + resultnum + "' ";
                                    rowset rstQry1 = DB.executeQuery(strQry1);
                                    if (rstQry1.next())
                                    {
                                        //ccsauto = rstQry1.getString("ccs_code");  //"999"
                                    }
                                    else
                                    {
                                        ccsauto = resultnum.ToString();
                                        break;
                                    }
                                }
                                if (int.TryParse(ccsauto, out int ccsauto1))
                                {
                                    // Add 1 to the integer value
                                    int newValue = ccsauto1; // + 1;
                                    if (newValue > 0)
                                    {
                                        string v = newValue.ToString();
                                        int vlen = v.Length;
                                        if (vlen == 1)
                                        {
                                            fixCCSCode = "00" + v;
                                        }
                                        else if (vlen == 2)
                                        {
                                            fixCCSCode = "0" + v;
                                        }
                                        else //if (vlen == 3)
                                        {
                                            fixCCSCode = v;
                                        }
                                    }
                                    else
                                    {
                                        lblAlert.ForeColor = System.Drawing.Color.Red;
                                        lblAlert.Text = "error1";// + ex.Message";
                                    }
                                }

                            }
                        }



                        string CSD_TYPE_t7 = get_cchcode + get_ccdcode + fixCCSCode;  //2F001



                        //////   Get ccs_descr  
                        String logo1 = "";
                        string strQry = "select ccd_logo from ccd_channeldetail where cch_code = '" + get_cchcode + "' and ccd_code = '" + get_ccdcode + "' ";
                        rowset rstQry = DB.executeQuery(strQry);
                        if (rstQry.next())
                        {
                            logo1 = rstQry.getString("ccd_logo");
                        }
                        string logo2 = logo1 + "" + get_ccsfield1;
                        string ccsDesc = logo1 + "-" + brName;

                        if (ddlCCD_CHANNELDTLCD.SelectedValue.ToString() == "F")
                        {
                            use_userid_t4BSO = "BCO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + get_ccsfield1; /* txtBranchCode.Text;*/
                        }
                        else
                        {
                            use_userid_t4BSO = "BSO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + get_ccsfield1; /* txtBranchCode.Text;*/
                        }

                        //string use_userid_t4BSO = "BSO" + get_cchcode + get_ccdcode + get_ccsfield1;
                        string use_userid_t4BM = "BM" + get_cchcode + get_ccdcode + get_ccsfield1;


                        string use_password = "bca864d09031109d726859a3fd8458c1";


                        String bnkcodeGet = "";
                        String strQrybnkcodeGet = "Select distinct decode(pbk_bankcode, 'UBL', '901', 'SMBL', '907', 'BAL', '903', 'NBP', '904', 'NIB', '905', 'SBL', '906', 'SILK', '908', 'BOP', '909', 'DIB', '910', 'HBL', '912', 'FBL', '913', 'JSB', '910', '000') bCode from LAAG_AGENT a where pbk_bankcode = '" + logo1 + "'";
                        rowset rstQrybnkcodeGet = DB.executeQuery(strQrybnkcodeGet);
                        if (rstQrybnkcodeGet.next())
                        {
                            bnkcodeGet = rstQrybnkcodeGet.getString("bCode");
                        }
                        string AAG_IMEDSUPR_12t = bnkcodeGet + "" + "0000";


                        string aag_agcode_t4 = bnkcodeGet + "" + get_ccsfield1;
                        string ermsg = String.Empty;
                        string cchP = get_cchcode;
                        string ccdP = get_ccdcode;
                        string brCdP = get_ccsfield1;
                        string ans;

                        //Inserting into tables
                        string queryccsField = @"SELECT s.ccs_code, s.cch_code, s.ccd_code  FROM ccs_chanlsubdetl s where ccs_field1 = '" + get_ccsfield1 + "' and s.cch_code ='" + get_cchcode + "' and s.ccd_code = '" + get_ccdcode + "' ";
                        DataTable dtf = DB.getDataTable(queryccsField);
                        if (dtf.Rows.Count <= 0)
                        {
                            ans = validatedata(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "G" || ans == "H")
                            {
                                lblAlert.ForeColor = System.Drawing.Color.Red;
                                lblAlert.Text = "";
                            }
                            else
                            {
                                try
                                {
                                    string Sql = "insert into ccs_chanlsubdetl (CCS_CODE,\n" +
                                        "CCH_CODE,\n" +
                                        "CCD_CODE,\n" +
                                        "CCS_DESCR,\n" +
                                        "CCS_FIELD1)\n" +
                                        "values(\n" +
                                        " '" + fixCCSCode + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + ccsDesc + "', \n" +
                                        " '" + get_ccsfield1 + "' \n" +
                                        ")";
                                    Msg = DML1(Sql);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;

                                }

                                try
                                {
                                    string sqlString2a = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                        "cch_code,\n" +
                                        "ccd_code,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + "003" + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2a);


                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;
                                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-003','')", true);
                                }

                                try
                                {
                                    string sqlString2b = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                        "cch_code,\n" +
                                        "ccd_code,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + "005" + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2b);


                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;
                                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-005','')", true);
                                }

                                try
                                {
                                    string sqlString2c = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                        "cch_code,\n" +
                                        "ccd_code,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + "074" + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2c);


                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;
                                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-074','')", true);
                                }

                                try
                                {
                                    string sqlString2d = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                        "cch_code,\n" +
                                        "ccd_code,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + "019" + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2d);


                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;
                                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-019','')", true);
                                }

                                try
                                {
                                    string sqlString2e = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
                                        "cch_code,\n" +
                                        "ccd_code,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + "075" + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2e);


                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = ""; // + ex.Message;                            
                                }
                            }

                            if (ans == "I")
                            {
                                //insert into log-table

                                try
                                {
                                    string qryupdusemBSO = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
                                        "use_name = '" + use_userid_t4BSO + "', " +
                                        "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
                                        "use_password = '" + use_password + "', " +
                                        "use_type = '" + "S" + "', " +
                                        "aag_agcode = '" + aag_agcode_t4 + "', " +
                                        "use_active = '" + "Y" + "' " +
                                        "where use_userid = '" + use_userid_t4BSO + "'";
                                    DB.executeDML(qryupdusemBSO);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Error in updateusermaster','')", true);
                                }
                                //working
                                try
                                {
                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm1 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - use_usermaster-BSO',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }


                            }
                            else
                            {
                                try
                                {
                                    string sqlString2 = "insert into use_usermaster (use_userid,\n" +
                                        "pcl_locatcode,\n" +
                                        "use_name,\n" +
                                        "use_jobdescrip,\n" +
                                        "use_password,\n" +
                                        "use_type,\n" +
                                        "aag_agcode,\n" +
                                        "use_active)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BSO + "', \n" +
                                        " '" + "180" + "', \n" +
                                        " '" + use_userid_t4BSO + "', \n" +
                                        " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
                                        " '" + use_password + "', \n" +
                                        " '" + "S" + "', \n" +
                                        " '" + aag_agcode_t4 + "', \n" +
                                        " '" + "Y" + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString2);


                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            ans = validatedata1(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "Ia")
                            {
                                //insert into log-table
                                try
                                {
                                    string qryupdusemBM = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
                                        "use_name = '" + use_userid_t4BM + "', " +
                                        "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
                                        "use_password = '" + use_password + "', " +
                                        "use_type = '" + "M" + "', " +
                                        "aag_agcode = '" + aag_agcode_t4 + "', " +
                                        "use_active = '" + "Y" + "' " +
                                        "where use_userid = '" + use_userid_t4BM + "'";
                                    DB.executeDML(qryupdusemBM);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm2 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - use_usermaster-BM',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm2);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                            }
                            else
                            {
                                try
                                {
                                    string sqlString3 = "insert into use_usermaster (use_userid,\n" +
                                        "pcl_locatcode,\n" +
                                        "use_name,\n" +
                                        "use_jobdescrip,\n" +
                                        "use_password,\n" +
                                        "use_type,\n" +
                                        "aag_agcode,\n" +
                                        "use_active)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BM + "', \n" +
                                        " '" + "180" + "', \n" +
                                        " '" + use_userid_t4BM + "', \n" +
                                        " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
                                        " '" + use_password + "', \n" +
                                        " '" + "M" + "', \n" +
                                        " '" + aag_agcode_t4 + "', \n" +
                                        " '" + "Y" + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString3);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            ans = validatedata2(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "J")
                            {
                                //insert into log-table
                                //update tables for both BSO and BM users
                                try
                                {
                                    string qryupdusconBSO = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
                                        "where use_userid = '" + use_userid_t4BSO + "' " +
                                        "and CCN_CTRYCD = '" + "001" + "' ";
                                    DB.executeDML(qryupdusconBSO);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm3 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LUCN_USERCOUNTRY-BSO',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm3);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            else
                            {
                                try
                                {
                                    string sqlString4 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
                                        "CCN_CTRYCD,\n" +
                                        "UCN_DEFAULT)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BSO + "', \n" +
                                        " '" + "001" + "', \n" +
                                        " '" + "Y" + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString4);

                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            ans = validatedata3(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "Ja")
                            {
                                //insert into log-table
                                //update tables for both BSO and BM users

                                try
                                {
                                    string qryupdusconBM = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
                                        "where use_userid = '" + use_userid_t4BM + "' " +
                                        "and CCN_CTRYCD = '" + "001" + "' ";
                                    DB.executeDML(qryupdusconBM);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm4 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LUCN_USERCOUNTRY-BM',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm4);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }


                            }
                            else
                            {

                                try
                                {
                                    string sqlString5 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
                                        "CCN_CTRYCD,\n" +
                                        "UCN_DEFAULT)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BM + "', \n" +
                                        " '" + "001" + "', \n" +
                                        " '" + "Y" + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString5);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }

                            ans = validatedataluBSO(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "JSBO")
                            {
                                //insert into log-table
                                //update tables for both BSO    counPKluBSO1
                                try
                                {
                                    string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
                                        ", uch_default= '" + "Y" + "' " +
                                        ", ccd_code = '" + ccdP + "' " +
                                        "where use_userid = '" + use_userid_t4BSO + "' " +
                                        "and ccs_code = '" + fixCCSCode + "' ";
                                    DB.executeDML(qryupdsysdtl1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {
                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm5 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - luch_userchannel-BSO',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm5);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }


                            }
                            else
                            {
                                try
                                {
                                    string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
                                        "CCH_CODE,\n" +
                                        "UCH_DEFAULT,\n" +
                                        "CCD_CODE,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BSO + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + "Y" + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString8);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }

                            ans = validatedataluBM(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "JSBM")
                            {
                                //insert into log-table
                                //update tables for both BSO 
                                try
                                {
                                    string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
                                        ", uch_default= '" + "Y" + "' " +
                                        ", ccd_code = '" + ccdP + "' " +
                                        "where use_userid = '" + use_userid_t4BM + "' " +
                                        "and ccs_code = '" + fixCCSCode + "' ";
                                    DB.executeDML(qryupdsysdtl1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm5 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - luch_userchannel-BSO',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm5);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }


                            }
                            else
                            {
                                try
                                {
                                    string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
                                        "CCH_CODE,\n" +
                                        "UCH_DEFAULT,\n" +
                                        "CCD_CODE,\n" +
                                        "CCS_CODE)\n" +
                                        "values(\n" +
                                        " '" + use_userid_t4BM + "', \n" +
                                        " '" + get_cchcode + "', \n" +
                                        " '" + "Y" + "', \n" +
                                        " '" + get_ccdcode + "', \n" +
                                        " '" + fixCCSCode + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString8);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            ans = validatedata4(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "K")
                            {
                                //insert into log-table
                                //update tables for both BSO 
                                try
                                {
                                    string qryupdsysdtl1 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + aag_agcode_t4 + "' " +
                                        "where CSH_ID = '" + "CHAGT" + "' " +
                                        "and CSD_TYPE = '" + aag_agcode_t4 + "' ";
                                    DB.executeDML(qryupdsysdtl1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm5 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LCSD_SYSTEMDTL-CHAGT',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm5);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }


                            }
                            else
                            {
                                try
                                {
                                    string sqlString8 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
                                        "CSD_TYPE,\n" +
                                        "CSD_VALUE)\n" +
                                        "values(\n" +
                                        " '" + "CHAGT" + "', \n" +
                                        " '" + aag_agcode_t4 + "', \n" +
                                        " '" + aag_agcode_t4 + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString8);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }

                            ans = validatedata5(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "L")
                            {
                                //insert into log-table
                                //update tables for both BM users
                                try
                                {
                                    string qryupdsysdtl2 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + get_ccsfield1 + "' " +
                                        "where CSH_ID = '" + "CHBBR" + "' " +
                                        "and CSD_TYPE = '" + CSD_TYPE_t7 + "' ";
                                    DB.executeDML(qryupdsysdtl2);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm6 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LCSD_SYSTEMDTL-CHBBR',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm6);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                            }
                            else
                            {
                                try
                                {
                                    string sqlString9 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
                                        "CSD_TYPE,\n" +
                                        "CSD_VALUE)\n" +
                                        "values(\n" +
                                        " '" + "CHBBR" + "', \n" +
                                        " '" + CSD_TYPE_t7 + "', \n" +
                                        " '" + get_ccsfield1 + "' \n" +
                                        ")";
                                    DB.executeDML(sqlString9);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            ans = validatedata6(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "M")
                            {
                                try
                                {
                                    string qryupdlaag1 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
                                        "PBK_BANKCODE = '" + logo1 + "', " +
                                        "PCM_COMPCODE = '" + "01" + "', " +
                                        "CHL_LEVEL = '" + "001" + "', " +
                                        "PCL_LOCATCODE = '" + "180" + "', " +
                                        "CDG_DESIGCODE = '" + "500" + "', " +
                                        "CRG_RELGCD = '" + "999" + "', " +
                                        "AAG_NAME = '" + logo1 + "-" + brName + "', " +
                                        "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
                                        "AAG_IMEDSUPR = '" + AAG_IMEDSUPR_12t + "', " +
                                        "EXT_NACTIVE = '" + "Y" + "', " +
                                        "AAG_DIRECT = '" + "N" + "', " +
                                        "AAG_BROKER = '" + "B" + "', " +
                                        "AAG_STATUS = '" + "C" + "', " +
                                        "AAG_SALARIED = '" + "N" + "', " +
                                        "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
                                        "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
                                    DB.executeDML(qryupdlaag1);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm7 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LAAG_AGENT-POS',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm7);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                            }
                            else
                            {
                                //_date = DateTime.Parse("5/13/2012");
                                //day = DateTime.Parse("5/13/2012").ToString("dd-MMM-yyyy");
                                try
                                {
                                    string sqlString10 = "insert into LAAG_AGENT (AAG_AGCODE,CNT_NATCD,PBK_BANKCODE,PCM_COMPCODE,CHL_LEVEL,PCL_LOCATCODE,CDG_DESIGCODE,CRG_RELGCD," +
                                       " AAG_NAME,AAG_JOINDAT,AAG_IMEDSUPR,EXT_NACTIVE,AAG_DIRECT,AAG_BROKER,AAG_STATUS,AAG_SALARIED,AAG_SALEFCTDAT)" +
                                       "values(" + aag_agcode_t4 + ",'586','" + logo1 + "','01','001','180','500','999','" + logo1 + "-" + brName + "',to_date('04/01/2022','MM/dd/yyyy') " +
                                       ",'" + AAG_IMEDSUPR_12t + "','Y','N','B','C','N',to_date('04/01/2022','MM/dd/yyyy'))";
                                    DB.executeDML(sqlString10);

                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }

                            {
                                string queryBOP = @"SELECT s.ccs_code, s.cch_code, s.ccd_code, s.cqn_type FROM LACH_ASSESSMENT s where s.ccs_code = '" + fixCCSCode + "' and s.cch_code ='" + get_cchcode + "' and s.ccd_code = '" + get_ccdcode + "' ";
                                DataTable dtla = DB.getDataTable(queryBOP);
                                //if (ddlCCD_CHANNELDTLCD.SelectedValue == "9" || ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0)
                                //if ((ddlCCD_CHANNELDTLCD.SelectedValue == "9" && dtla.Rows.Count <= 0) || (ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0))
                                if ((get_ccdcode == "9" && dtla.Rows.Count <= 0) || (get_ccdcode == "F" && dtla.Rows.Count <= 0))
                                {
                                    //assessment();
                                    string respad = "";
                                    for (int i = 1; i <= 13; i++)
                                    {

                                        int ileng = i.ToString().Length;
                                        if (ileng == 1)
                                        {
                                            respad = i.ToString().PadLeft(2, '0');
                                        }
                                        else if (ileng == 2)
                                        {
                                            respad = i.ToString();
                                        }

                                        try
                                        {
                                            string queryBOP1 = "insert into LACH_ASSESSMENT (Cqn_Code,\n" +
                                            "Cqn_Type,\n" +
                                            "Cch_Code,\n" +
                                            "Ccd_Code,\n" +
                                            "Ccs_Code)\n" +
                                            "values(\n" +
                                            " '" + "BP" + respad + "', \n" +
                                            " '" + "1" + "', \n" +
                                            " '" + get_cchcode + "', \n" +
                                            " '" + get_ccdcode + "', \n" +
                                            " '" + fixCCSCode + "' \n" +
                                            ")";
                                            DB.executeDML(queryBOP1);
                                        }
                                        catch (Exception ex)
                                        {
                                            lblAlert.ForeColor = System.Drawing.Color.Red;
                                            lblAlert.Text = "";
                                        }
                                    }
                                }
                            }


                            ans = validatedata7(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "O")
                            {
                                //insert into log-table
                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;
                                try
                                {
                                    string qryupdbkbr = "update PBB_BANKBRANCH set CCN_CTRYCD = '" + "001" + "', " +
                                        "CCT_CITYCD = '" + "001" + "', " +
                                        "PBB_BRANCHNAME = '" + ccsDesc + "', " +
                                        "PBB_DDSTEXTFILE = '" + "N" + "' " +
                                        "where PBK_BANKCODE = '" + logo1 + "' " +
                                        "and PBB_BRANCHCODE = '" + get_ccsfield1 + "' ";
                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
                                    int x = dbCom.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm8 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - PBB_BANKBRANCH - ILAS',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + get_ccsfield1 + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm8);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            else
                            {
                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;
                                try
                                {
                                    string sqlString11 = "insert into PBB_BANKBRANCH (PBK_BANKCODE,\n" +
                                        "PBB_BRANCHCODE,\n" +
                                        "CCN_CTRYCD,\n" +
                                        "CCT_CITYCD,\n" +
                                        "PBB_BRANCHNAME,\n" +
                                        "PBB_DDSTEXTFILE)\n" +
                                        "values(\n" +
                                        " '" + logo1 + "', \n" +
                                        " '" + get_ccsfield1 + "', \n" +
                                        " '" + "001" + "', \n" +
                                        " '" + "001" + "', \n" +
                                        " '" + ccsDesc + "', \n" +
                                        " '" + "N" + "' \n" +
                                        ")";
                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString11, dbCon);
                                    int x = dbCom.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                finally
                                {
                                    dbCon.Close();
                                }
                            }
                            /////////////////////////

                            string pcaAccount = "select lc.csd_value pcaAcc from lcsd_systemdtl lc where lc.csh_id = 'BKPOS' and lc.csd_type = '" + logo1 + "'";

                            DataTable dt_new = new DataTable();
                            dt_new = GetdataOraOledb(pcaAccount);

                            if (dt_new.Rows.Count > 0)
                            {
                                pcaAccount = dt_new.Rows[0]["pcaAcc"].ToString();
                            }
                            else
                            {
                                pcaAccount = "";
                            }

                            ans = validatedata8(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "Oa")
                            {


                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;

                                try
                                {
                                    string qryupdbkbr = "update PBA_BANKACCOUNT set PCM_COMPCODE = '" + "01" + "', " +
                                        "PCA_ACCOUNT = '" + pcaAccount + "', " +
                                        "PCU_CURRCODE = '" + "1" + "', " +
                                        "PBA_ACTIVE = '" + "Y" + "', " +
                                        "PCL_LOCATCODE = '" + "180" + "' " +
                                        "where PBK_BANKCODE = '" + logo1 + "' " +
                                        "and PBB_BRANCHCODE = '" + get_ccsfield1 + "' " +
                                        "and PBA_SERIAL = '" + "1" + "' ";
                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
                                    int x = dbCom.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";

                                }

                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm9 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - PBA_BANKACCOUNT - ILAS',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + get_ccsfield1 + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm9);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            else
                            {

                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;
                                try
                                {
                                    string sqlString12 = "insert into PBA_BANKACCOUNT (PBK_BANKCODE,\n" +
                                        "PBB_BRANCHCODE,\n" +
                                        "PBA_SERIAL,\n" +
                                        "PCM_COMPCODE,\n" +
                                        "PCA_ACCOUNT,\n" +
                                        "PCU_CURRCODE,\n" +
                                        "PBA_ACTIVE,\n" +
                                        "PCL_LOCATCODE)\n" +
                                        "values(\n" +
                                        " '" + logo1 + "', \n" +
                                        " '" + get_ccsfield1 + "', \n" +
                                        " '" + "1" + "', \n" +
                                        " '" + "01" + "', \n" +
                                        " '" + pcaAccount + "', \n" +   //PCA ACCOUNT check it <-------------------------
                                        " '" + "1" + "', \n" +
                                        " '" + "Y" + "', \n" +
                                        " '" + "180" + "' \n" +
                                        ")";
                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString12, dbCon);
                                    int x = dbCom.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                finally
                                {
                                    dbCon.Close();
                                }
                            }



                            ans = validatedata9(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
                            if (ans == "P")
                            {
                                lblAlert.ForeColor = System.Drawing.Color.Red;
                                lblAlert.Text = "";
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Already found','')", true);
                                //insert into log-table
                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;
                                try
                                {
                                    string sqlString14 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
                                        "PBK_BANKCODE = '" + logo1 + "', " +
                                        "PCM_COMPCODE = '" + "01" + "', " +
                                        "CHL_LEVEL = '" + "001" + "', " +
                                        "PCL_LOCATCODE = '" + "180" + "', " +
                                        "CDG_DESIGCODE = '" + "500" + "', " +
                                        "CRG_RELGCD = '" + "999" + "', " +
                                        "AAG_NAME = '" + logo1 + "-" + brName + "', " +
                                        "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
                                        "AAG_IMEDSUPR = '" + AAG_IMEDSUPR_12t + "', " +
                                        "EXT_NACTIVE = '" + "Y" + "', " +
                                        "AAG_DIRECT = '" + "N" + "', " +
                                        "AAG_BROKER = '" + "B" + "', " +
                                        "AAG_STATUS = '" + "C" + "', " +
                                        "AAG_SALARIED = '" + "N" + "', " +
                                        "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
                                        "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString14, dbCon);
                                    int x = dbCom.ExecuteNonQuery();


                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }

                                try
                                {

                                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                                    string sqlComm10 = "insert into error_log\n" +
                                    "  (process_type,\n" +
                                    "   procedure_name,\n" +
                                    "   error_date,\n" +
                                    "   error_code,\n" +
                                    "   error_msg,\n" +
                                    "   doc_name,\n" +
                                    "   username,\n" +
                                    "   status_date)\n" +
                                    "values\n" +
                                    "  ('BNKBRNCH_UPLOAD',\n" +
                                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
                                    "  sysdate,\n" +
                                    "   '5010',\n" +
                                    "  'Record already found - LAAG_AGENT-ILAS',\n" +
                                    //"   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
                                    "   '" + get_cchcode + "-" + get_ccdcode + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
                                    "   '" + user + "',\n" +
                                    "  sysdate)";
                                    DB.executeDML(sqlComm10);
                                }
                                catch (Exception ex)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                            }
                            else
                            {
                                System.Data.OleDb.OleDbConnection dbCon = null;
                                System.Data.OleDb.OleDbDataAdapter dbAdapter;
                                System.Data.OleDb.OleDbCommand dbCom;
                                try
                                {
                                    string sqlString13 = "insert into LAAG_AGENT (AAG_AGCODE,\n" +
                                        "CNT_NATCD,\n" +
                                        "PBK_BANKCODE,\n" +
                                        "PCM_COMPCODE,\n" +
                                        "CHL_LEVEL,\n" +
                                        "PCL_LOCATCODE,\n" +
                                        "CDG_DESIGCODE,\n" +
                                        "CRG_RELGCD,\n" +
                                        "AAG_NAME,\n" +
                                        "AAG_JOINDAT,\n" +
                                        "AAG_IMEDSUPR,\n" +
                                        "EXT_NACTIVE,\n" +
                                        "AAG_DIRECT,\n" +
                                        "AAG_BROKER,\n" +
                                        "AAG_STATUS,\n" +
                                        "AAG_SALARIED,\n" +
                                        "AAG_SALEFCTDAT)\n" +
                                        "values(\n" +
                                        " '" + aag_agcode_t4 + "', \n" +
                                        " '" + "586" + "', \n" +
                                        " '" + logo1 + "', \n" +
                                        " '" + "01" + "', \n" +
                                        " '" + "001" + "', \n" +
                                        " '" + "180" + "', \n" +
                                        " '" + "500" + "', \n" +
                                        " '" + "999" + "', \n" +
                                        " '" + logo1 + "-" + brName + "', \n" +
                                        " to_date('04/01/2022','MM/dd/yyyy'), \n" +
                                        //" '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', \n" +
                                        " '" + AAG_IMEDSUPR_12t + "', " +
                                        " '" + "Y" + "', \n" +
                                        " '" + "N" + "', \n" +
                                        " '" + "B" + "', \n" +
                                        " '" + "C" + "', \n" +
                                        " '" + "N" + "', \n" +
                                        " to_date('04/01/2022','MM/dd/yyyy') \n" +
                                        ")";

                                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
                                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
                                    dbCon.Open();
                                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
                                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString13, dbCon);
                                    int x = dbCom.ExecuteNonQuery();

                                    //lblAlert.ForeColor = System.Drawing.Color.Green;
                                    //lblAlert.Text = "Recoed Saved... ";
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Recoed Saved','')", true);

                                }
                                catch (Exception)
                                {
                                    lblAlert.ForeColor = System.Drawing.Color.Red;
                                    lblAlert.Text = "";
                                }
                                finally
                                {
                                    dbCon.Close();
                                }
                            }
                            // String BSOUS = use_userid_t4BSO;
                            // String BMUS = use_userid_t4BM;

                            // string message = "Record Saved ";
                            // string script = $@" swal({{title: '',text: '{message} - {BSOUS} - {BMUS}',}});";

                            // ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", script, true);
                            //working1



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

        //protected void SaveRecxls()
        //{
        //    //DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);
        //    //DateTime crdt =  new DateTime(2008, 3, 9, 16, 5, 7, 123);
        //    DateTime _date;
        //    string day = "";
        //    NameValueCollection columnNameValue = new NameValueCollection();
        //    string brlen = txtBranchCode.Text;
        //    int vbrlen = brlen.Length;
        //    if (vbrlen == 1)
        //    {
        //        v_txtBranchCode = "000" + txtBranchCode.Text;
        //    }
        //    else if (vbrlen == 2)
        //    {
        //        v_txtBranchCode = "00" + txtBranchCode.Text;
        //    }
        //    else if (vbrlen == 3)
        //    {
        //        v_txtBranchCode = "0" + txtBranchCode.Text;
        //    }
        //    else
        //    {
        //        v_txtBranchCode = txtBranchCode.Text;
        //    }

        //    txtBranchCode.Text = v_txtBranchCode;

        //    columnNameValue.Add("cch_code", ddlCCH_CHANNELCD.SelectedValue.Trim() == "" ? null : ddlCCH_CHANNELCD.SelectedValue); //.Trim().Split('-')[0]);
        //    columnNameValue.Add("ccd_code", ddlCCD_CHANNELDTLCD.SelectedValue.Trim() == "" ? null : ddlCCD_CHANNELDTLCD.SelectedValue);
        //    columnNameValue.Add("brCode", txtBranchCode.Text.Trim() == "" ? null : txtBranchCode.Text);
        //    columnNameValue.Add("brName", txtBranchName.Text.Trim() == "" ? null : txtBranchName.Text);

        //    //string chkdupli = "select count(*) chkcnt from ccs_chanlsubdetl where cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
        //    //    "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'  and ccs_field1 =  '" + v_txtBranchCode + "' ";
        //    //rowset chkdupli1 = DB.executeQuery(chkdupli);
        //    //if (!chkdupli1.next())
        //    //{


        //    string use_userid_t4BSO = "BSO" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;
        //    string use_userid_t4BM = "BM" + ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + txtBranchCode.Text;

        //    //string use_passwordBSO = EncodePasswordToBase64(use_userid_t4BSO);
        //    //string use_passwordBM = EncodePasswordToBase64(use_userid_t4BM);

        //    string use_password = "bca864d09031109d726859a3fd8458c1";

        //    columnNameValue.Add("use_userid_t4BSO", use_userid_t4BSO == "" ? null : use_userid_t4BSO);
        //    columnNameValue.Add("use_userid_t4BM", use_userid_t4BM == "" ? null : use_userid_t4BM);
        //    columnNameValue.Add("ccs_field1_t2", txtBranchCode.Text.Trim() == "" ? null : txtBranchCode.Text);
        //    String ccsauto = "";
        //    //check number or character
        //    string chkNumChr = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL  and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
        //            "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'  ";
        //    rowset chkNumChr1 = DB.executeQuery(chkNumChr);
        //    if (chkNumChr1.next())
        //    {
        //        //                string chkNC = chkNumChr1.getString("ccscode");
        //        string strAlpha = "";
        //        string jnumber = "";
        //        bool exitedInner = false;
        //        for (int i = 65; i <= 90; i++)
        //        {
        //            strAlpha = ((char)i).ToString();
        //            for (int j = 1; j <= 99; j++)
        //            {
        //                int jlen = j.ToString().Length;
        //                if (jlen == 1)
        //                {
        //                    jnumber = "0" + j.ToString();
        //                }
        //                else
        //                {
        //                    jnumber = j.ToString();
        //                }
        //                string finSeq = strAlpha + jnumber;
        //                //string finSeq = "A51"; //for testing
        //                string strQry1a = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[A-Z]+', '') IS NOT NULL and REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NOT NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
        //                        "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code = '" + finSeq + "' order by ccs_code";
        //                rowset rstQry1a = DB.executeQuery(strQry1a);

        //                if (rstQry1a.next())
        //                {
        //                    //ccsauto = rstQry1.getString("ccs_code");  //"999"
        //                }
        //                else
        //                {
        //                    fixCCSCode = finSeq.ToString();   //resultnum.ToString();
        //                    exitedInner = true;
        //                    break;
        //                }
        //            }
        //            if (exitedInner == true)
        //            {
        //                break;
        //            }

        //        }


        //    }

        //    string chkNumChrN = "Select ccs_code ccscode from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
        //            "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code <= '" + "999" + "' and ccs_code != '" + "0" + "'  and ccs_code != '" + "00" + "' "; // and ccs_code != '" + "000" + "' ";
        //    rowset chkNumChr11 = DB.executeQuery(chkNumChrN);
        //    if (chkNumChr11.next())
        //    {
        //        string chkNC = chkNumChr11.getString("ccscode"); //001
        //        int chkNC1 = Convert.ToInt16(chkNumChr11.getString("ccscode")); //1
        //        if (chkNC1 >= 0 && chkNC1 <= 999) //==115--noman  select chr(ASCII('A') + level - 1) as alphafrom from dual connect by level <=26;   
        //                                          //if (chkNC1 == 999)    for testing
        //        {
        //            //findNumber();
        //            for (int i = 1; i <= 999; i++)
        //            {
        //                string str = i.ToString();
        //                string resultnum = str.PadLeft(3, '0').ToString();

        //                string strQry1 = "Select ccs_code from ccs_chanlsubdetl where REGEXP_REPLACE(ccs_code, '[0-9]+', '') IS NULL and cch_code = '" + columnNameValue.getObject("cch_code") + "' " +
        //                    "and ccd_code = '" + columnNameValue.getObject("ccd_code") + "' and ccs_code = '" + resultnum + "' ";
        //                rowset rstQry1 = DB.executeQuery(strQry1);
        //                if (rstQry1.next())
        //                {
        //                    //ccsauto = rstQry1.getString("ccs_code");  //"999"
        //                }
        //                else
        //                {
        //                    ccsauto = resultnum.ToString();
        //                    break;
        //                }
        //            }
        //            if (int.TryParse(ccsauto, out int ccsauto1))
        //            {
        //                // Add 1 to the integer value
        //                int newValue = ccsauto1; // + 1;
        //                if (newValue > 0)
        //                {
        //                    string v = newValue.ToString();
        //                    int vlen = v.Length;
        //                    if (vlen == 1)
        //                    {
        //                        fixCCSCode = "00" + v;
        //                    }
        //                    else if (vlen == 2)
        //                    {
        //                        fixCCSCode = "0" + v;
        //                    }
        //                    else //if (vlen == 3)
        //                    {
        //                        fixCCSCode = v;
        //                    }
        //                }
        //                else
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "error1";// + ex.Message";
        //                }
        //            }

        //        }
        //    }




        //    string CSD_TYPE_t7 = ddlCCH_CHANNELCD.Text + ddlCCD_CHANNELDTLCD.Text + fixCCSCode;

        //    columnNameValue.Add("CSD_TYPE_t7", CSD_TYPE_t7 == "" ? null : CSD_TYPE_t7);

        //    String logo1 = "";
        //    string strQry = "select ccd_logo from ccd_channeldetail where cch_code = '" + columnNameValue.getObject("cch_code") + "' and ccd_code = '" + columnNameValue.getObject("ccd_code") + "'";
        //    rowset rstQry = DB.executeQuery(strQry);
        //    if (rstQry.next())
        //    {
        //        logo1 = rstQry.getString("ccd_logo");
        //    }
        //    string logo2 = logo1 + "" + txtBranchCode.Text;
        //    string ccsDesc = logo1 + "-" + columnNameValue.getObject("brName");


        //    ///////////////////////////////////use substr			
        //    String bnkcodeGet = "";

        //    String strQrybnkcodeGet = "Select distinct decode(pbk_bankcode, 'UBL', '901', 'SMBL', '907', 'BAL', '903', 'NBP', '904', 'NIB', '905', 'SBL', '906', 'SILK', '908', 'BOP', '909', 'DIB', '910', 'HBL', '912', 'FBL', '913', 'JSB', '910', '000') bCode from LAAG_AGENT a where pbk_bankcode = '" + logo1 + "'";

        //    //String strQrybnkcodeGet = "select ccd.ccd_code bCode from ccd_channeldetail ccd where ccd.ccd_code = REGEXP_REPLACE(ccd.ccd_code, '[A-Z]+', '') and ccd.ccd_logo = '" + logo1 + "'";
        //    rowset rstQrybnkcodeGet = DB.executeQuery(strQrybnkcodeGet);
        //    if (rstQrybnkcodeGet.next())
        //    {
        //        bnkcodeGet = rstQrybnkcodeGet.getString("bCode");
        //    }

        //    string AAG_IMEDSUPR_12t = bnkcodeGet + "" + "0000";

        //    columnNameValue.Add("AAG_IMEDSUPR_12t", AAG_IMEDSUPR_12t == "" ? null : AAG_IMEDSUPR_12t);


        //    //String getaggcode = "";
        //    //String strQrygetaggcode = "Select to_char(max(aag_agcode)) vacode from LAAG_AGENT where pbk_bankcode = '" + logo1 + "'";
        //    //rowset rstQrygetaggcode = DB.executeQuery(strQrygetaggcode);
        //    //if (rstQrygetaggcode.next())
        //    //{
        //    //    getaggcode = rstQrygetaggcode.getString("vacode");
        //    //}

        //    string aag_agcode_t4 = bnkcodeGet + "" + txtBranchCode.Text;
        //    string ermsg = String.Empty;
        //    string cchP = ddlCCH_CHANNELCD.SelectedValue;
        //    string ccdP = ddlCCD_CHANNELDTLCD.SelectedValue;
        //    string brCdP = txtBranchCode.Text;
        //    string ans;

        //    if (ddlCCH_CHANNELCD.SelectedValue != null && ddlCCH_CHANNELCD.SelectedValue != "" && ddlCCD_CHANNELDTLCD.SelectedValue != null && ddlCCD_CHANNELDTLCD.SelectedValue != "" && txtBranchCode.Text != null && txtBranchCode.Text != "" && txtBranchName.Text != null && txtBranchName.Text != "") // && txtAgencyCode.Text != null && txtAgencyCode.Text != "")
        //    {
        //        string queryccsField = @"SELECT s.ccs_code, s.cch_code, s.ccd_code  FROM ccs_chanlsubdetl s where ccs_field1 = '" + txtBranchCode.Text + "' and s.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
        //        DataTable dtf = DB.getDataTable(queryccsField);

        //        if (dtf.Rows.Count <= 0)
        //        {
        //            ans = validatedata(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "G" || ans == "H")
        //            {
        //                lblAlert.ForeColor = System.Drawing.Color.Red;
        //                lblAlert.Text = "";
        //                //insert into log-table
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString1 = "insert into ccs_chanlsubdetl (CCS_CODE,\n" +
        //                        "CCH_CODE,\n" +
        //                        "CCD_CODE,\n" +
        //                        "CCS_DESCR,\n" +
        //                        "CCS_FIELD1)\n" +
        //                        "values(\n" +
        //                        " '" + fixCCSCode + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                         " '" + ccsDesc + "', \n" +
        //                        " '" + columnNameValue.getObject("brCode") + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;

        //                }

        //                try
        //                {
        //                    string sqlString2a = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
        //                        "cch_code,\n" +
        //                        "ccd_code,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + "003" + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2a);


        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;
        //                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-003','')", true);
        //                }

        //                try
        //                {
        //                    string sqlString2b = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
        //                        "cch_code,\n" +
        //                        "ccd_code,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + "005" + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2b);


        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;
        //                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-005','')", true);
        //                }

        //                try
        //                {
        //                    string sqlString2c = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
        //                        "cch_code,\n" +
        //                        "ccd_code,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + "074" + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2c);


        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;
        //                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-074','')", true);
        //                }

        //                try
        //                {
        //                    string sqlString2d = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
        //                        "cch_code,\n" +
        //                        "ccd_code,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + "019" + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2d);


        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;
        //                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Found in producet-019','')", true);
        //                }

        //                try
        //                {
        //                    string sqlString2e = "insert into LPCH_CHANNEL (ppr_prodcd,\n" +
        //                        "cch_code,\n" +
        //                        "ccd_code,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + "075" + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2e);


        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = ""; // + ex.Message;                            
        //                }

        //                //working2
        //            }

        //            if (ans == "I")
        //            {
        //                //insert into log-table
        //                try
        //                {
        //                    string qryupdusemBSO = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
        //                        "use_name = '" + columnNameValue.getObject("use_userid_t4BSO") + "', " +
        //                        "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
        //                        "use_password = '" + use_password + "', " +
        //                        "use_type = '" + "S" + "', " +
        //                        "aag_agcode = '" + aag_agcode_t4 + "', " +
        //                        "use_active = '" + "Y" + "' " +
        //                        "where use_userid = '" + columnNameValue.getObject("use_userid_t4BSO") + "'";
        //                    DB.executeDML(qryupdusemBSO);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Error in updateusermaster','')", true);
        //                }
        //                try
        //                {
        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm1 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - use_usermaster-BSO',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString2 = "insert into use_usermaster (use_userid,\n" +
        //                        "pcl_locatcode,\n" +
        //                        "use_name,\n" +
        //                        "use_jobdescrip,\n" +
        //                        "use_password,\n" +
        //                        "use_type,\n" +
        //                        "aag_agcode,\n" +
        //                        "use_active)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
        //                        " '" + "180" + "', \n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
        //                        " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
        //                        " '" + use_password + "', \n" +
        //                        " '" + "S" + "', \n" +
        //                        " '" + aag_agcode_t4 + "', \n" +
        //                        " '" + "Y" + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString2);


        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            ans = validatedata1(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "Ia")
        //            {
        //                //insert into log-table
        //                try
        //                {
        //                    string qryupdusemBM = "update use_usermaster set pcl_locatcode = '" + "180" + "', " +
        //                        "use_name = '" + columnNameValue.getObject("use_userid_t4BM") + "', " +
        //                        "use_jobdescrip = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', " +
        //                        "use_password = '" + use_password + "', " +
        //                        "use_type = '" + "M" + "', " +
        //                        "aag_agcode = '" + aag_agcode_t4 + "', " +
        //                        "use_active = '" + "Y" + "' " +
        //                        "where use_userid = '" + columnNameValue.getObject("use_userid_t4BM") + "'";
        //                    DB.executeDML(qryupdusemBM);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm2 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - use_usermaster-BM',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm2);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString3 = "insert into use_usermaster (use_userid,\n" +
        //                        "pcl_locatcode,\n" +
        //                        "use_name,\n" +
        //                        "use_jobdescrip,\n" +
        //                        "use_password,\n" +
        //                        "use_type,\n" +
        //                        "aag_agcode,\n" +
        //                        "use_active)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
        //                        " '" + "180" + "', \n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
        //                        " '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', \n" +
        //                        " '" + use_password + "', \n" +
        //                        " '" + "M" + "', \n" +
        //                        " '" + aag_agcode_t4 + "', \n" +
        //                        " '" + "Y" + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString3);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            ans = validatedata2(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "J")
        //            {
        //                //insert into log-table
        //                //update tables for both BSO and BM users
        //                try
        //                {
        //                    string qryupdusconBSO = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
        //                        "where use_userid = '" + columnNameValue.getObject("use_userid_t4BSO") + "' " +
        //                        "and CCN_CTRYCD = '" + "001" + "' ";
        //                    DB.executeDML(qryupdusconBSO);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm3 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LUCN_USERCOUNTRY-BSO',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm3);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString4 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
        //                        "CCN_CTRYCD,\n" +
        //                        "UCN_DEFAULT)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
        //                        " '" + "001" + "', \n" +
        //                        " '" + "Y" + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString4);

        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            ans = validatedata3(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "Ja")
        //            {
        //                //insert into log-table
        //                //update tables for both BSO and BM users

        //                try
        //                {
        //                    string qryupdusconBM = "update LUCN_USERCOUNTRY set UCN_DEFAULT = '" + "Y" + "' " +
        //                        "where use_userid = '" + columnNameValue.getObject("use_userid_t4BM") + "' " +
        //                        "and CCN_CTRYCD = '" + "001" + "' ";
        //                    DB.executeDML(qryupdusconBM);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm4 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LUCN_USERCOUNTRY-BM',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm4);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }


        //            }
        //            else
        //            {

        //                try
        //                {
        //                    string sqlString5 = "insert into LUCN_USERCOUNTRY (USE_USERID,\n" +
        //                        "CCN_CTRYCD,\n" +
        //                        "UCN_DEFAULT)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
        //                        " '" + "001" + "', \n" +
        //                        " '" + "Y" + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString5);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }

        //            ans = validatedataluBSO(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "JSBO")
        //            {
        //                //insert into log-table
        //                //update tables for both BSO    counPKluBSO1
        //                try
        //                {
        //                    string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
        //                        ", uch_default= '" + "Y" + "' " +
        //                        ", ccd_code = '" + ccdP + "' " +
        //                        "where use_userid = '" + use_userid_t4BSO + "' " +
        //                        "and ccs_code = '" + fixCCSCode + "' ";
        //                    DB.executeDML(qryupdsysdtl1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {
        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm5 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - luch_userchannel-BSO',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm5);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
        //                        "CCH_CODE,\n" +
        //                        "UCH_DEFAULT,\n" +
        //                        "CCD_CODE,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BSO") + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + "Y" + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString8);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }

        //            ans = validatedataluBM(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "JSBM")
        //            {
        //                //insert into log-table
        //                //update tables for both BSO 
        //                try
        //                {
        //                    string qryupdsysdtl1 = "update luch_userchannel set cch_code = '" + cchP + "' " +
        //                        ", uch_default= '" + "Y" + "' " +
        //                        ", ccd_code = '" + ccdP + "' " +
        //                        "where use_userid = '" + use_userid_t4BM + "' " +
        //                        "and ccs_code = '" + fixCCSCode + "' ";
        //                    DB.executeDML(qryupdsysdtl1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm5 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - luch_userchannel-BSO',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm5);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString8 = "insert into luch_userchannel (USE_USERID,\n" +
        //                        "CCH_CODE,\n" +
        //                        "UCH_DEFAULT,\n" +
        //                        "CCD_CODE,\n" +
        //                        "CCS_CODE)\n" +
        //                        "values(\n" +
        //                        " '" + columnNameValue.getObject("use_userid_t4BM") + "', \n" +
        //                        " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                        " '" + "Y" + "', \n" +
        //                        " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                        " '" + fixCCSCode + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString8);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            ans = validatedata4(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "K")
        //            {
        //                //insert into log-table
        //                //update tables for both BSO 
        //                try
        //                {
        //                    string qryupdsysdtl1 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + aag_agcode_t4 + "' " +
        //                        "where CSH_ID = '" + "CHAGT" + "' " +
        //                        "and CSD_TYPE = '" + aag_agcode_t4 + "' ";
        //                    DB.executeDML(qryupdsysdtl1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm5 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LCSD_SYSTEMDTL-CHAGT',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BSO + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm5);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString8 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
        //                        "CSD_TYPE,\n" +
        //                        "CSD_VALUE)\n" +
        //                        "values(\n" +
        //                        " '" + "CHAGT" + "', \n" +
        //                        " '" + aag_agcode_t4 + "', \n" +
        //                        " '" + aag_agcode_t4 + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString8);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }

        //            ans = validatedata5(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "L")
        //            {
        //                //insert into log-table
        //                //update tables for both BM users
        //                try
        //                {
        //                    string qryupdsysdtl2 = "update LCSD_SYSTEMDTL set CSD_VALUE = '" + columnNameValue.getObject("ccs_field1_t2") + "' " +
        //                        "where CSH_ID = '" + "CHBBR" + "' " +
        //                        "and CSD_TYPE = '" + CSD_TYPE_t7 + "' ";
        //                    DB.executeDML(qryupdsysdtl2);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm6 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LCSD_SYSTEMDTL-CHBBR',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + use_userid_t4BM + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm6);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sqlString9 = "insert into LCSD_SYSTEMDTL (CSH_ID,\n" +
        //                        "CSD_TYPE,\n" +
        //                        "CSD_VALUE)\n" +
        //                        "values(\n" +
        //                        " '" + "CHBBR" + "', \n" +
        //                        " '" + CSD_TYPE_t7 + "', \n" +
        //                        " '" + columnNameValue.getObject("ccs_field1_t2") + "' \n" +
        //                        ")";
        //                    DB.executeDML(sqlString9);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            ans = validatedata6(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "M")
        //            {
        //                try
        //                {
        //                    string qryupdlaag1 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
        //                        "PBK_BANKCODE = '" + logo1 + "', " +
        //                        "PCM_COMPCODE = '" + "01" + "', " +
        //                        "CHL_LEVEL = '" + "001" + "', " +
        //                        "PCL_LOCATCODE = '" + "180" + "', " +
        //                        "CDG_DESIGCODE = '" + "500" + "', " +
        //                        "CRG_RELGCD = '" + "999" + "', " +
        //                        "AAG_NAME = '" + logo1 + "-" + txtBranchName.Text + "', " +
        //                        "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
        //                        "AAG_IMEDSUPR = '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', " +
        //                        "EXT_NACTIVE = '" + "Y" + "', " +
        //                        "AAG_DIRECT = '" + "N" + "', " +
        //                        "AAG_BROKER = '" + "B" + "', " +
        //                        "AAG_STATUS = '" + "C" + "', " +
        //                        "AAG_SALARIED = '" + "N" + "', " +
        //                        "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
        //                        "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
        //                    DB.executeDML(qryupdlaag1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm7 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LAAG_AGENT-POS',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm7);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //            }
        //            else
        //            {
        //                //_date = DateTime.Parse("5/13/2012");
        //                //day = DateTime.Parse("5/13/2012").ToString("dd-MMM-yyyy");
        //                try
        //                {
        //                    string sqlString10 = "insert into LAAG_AGENT (AAG_AGCODE,CNT_NATCD,PBK_BANKCODE,PCM_COMPCODE,CHL_LEVEL,PCL_LOCATCODE,CDG_DESIGCODE,CRG_RELGCD," +
        //                       " AAG_NAME,AAG_JOINDAT,AAG_IMEDSUPR,EXT_NACTIVE,AAG_DIRECT,AAG_BROKER,AAG_STATUS,AAG_SALARIED,AAG_SALEFCTDAT)" +
        //                       "values(" + aag_agcode_t4 + ",'586','" + logo1 + "','01','001','180','500','999','" + logo1 + "-" + txtBranchName.Text + "',to_date('04/01/2022','MM/dd/yyyy') " +
        //                       ",'" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "','Y','N','B','C','N',to_date('04/01/2022','MM/dd/yyyy'))";
        //                    DB.executeDML(sqlString10);

        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }

        //            {
        //                string queryBOP = @"SELECT s.ccs_code, s.cch_code, s.ccd_code, s.cqn_type FROM LACH_ASSESSMENT s where s.ccs_code = '" + fixCCSCode + "' and s.cch_code ='" + ddlCCH_CHANNELCD.SelectedValue + "' and s.ccd_code = '" + ddlCCD_CHANNELDTLCD.SelectedValue + "' ";
        //                DataTable dtla = DB.getDataTable(queryBOP);
        //                //if (ddlCCD_CHANNELDTLCD.SelectedValue == "9" || ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0)
        //                if ((ddlCCD_CHANNELDTLCD.SelectedValue == "9" && dtla.Rows.Count <= 0) || (ddlCCD_CHANNELDTLCD.SelectedValue == "F" && dtla.Rows.Count <= 0))
        //                {
        //                    //assessment();
        //                    string respad = "";
        //                    for (int i = 1; i <= 13; i++)
        //                    {

        //                        int ileng = i.ToString().Length;
        //                        if (ileng == 1)
        //                        {
        //                            respad = i.ToString().PadLeft(2, '0');
        //                        }
        //                        else if (ileng == 2)
        //                        {
        //                            respad = i.ToString();
        //                        }

        //                        try
        //                        {
        //                            string queryBOP1 = "insert into LACH_ASSESSMENT (Cqn_Code,\n" +
        //                            "Cqn_Type,\n" +
        //                            "Cch_Code,\n" +
        //                            "Ccd_Code,\n" +
        //                            "Ccs_Code)\n" +
        //                            "values(\n" +
        //                            " '" + "BP" + respad + "', \n" +
        //                            " '" + "1" + "', \n" +
        //                            " '" + columnNameValue.getObject("cch_code") + "', \n" +
        //                            " '" + columnNameValue.getObject("ccd_code") + "', \n" +
        //                            " '" + fixCCSCode + "' \n" +
        //                            ")";
        //                            DB.executeDML(queryBOP1);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            lblAlert.ForeColor = System.Drawing.Color.Red;
        //                            lblAlert.Text = "";
        //                        }
        //                    }
        //                }
        //            }


        //            ans = validatedata7(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "O")
        //            {
        //                //insert into log-table
        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;
        //                try
        //                {
        //                    string qryupdbkbr = "update PBB_BANKBRANCH set CCN_CTRYCD = '" + "001" + "', " +
        //                        "CCT_CITYCD = '" + "001" + "', " +
        //                        "PBB_BRANCHNAME = '" + ccsDesc + "', " +
        //                        "PBB_DDSTEXTFILE = '" + "N" + "' " +
        //                        "where PBK_BANKCODE = '" + logo1 + "' " +
        //                        "and PBB_BRANCHCODE = '" + columnNameValue.getObject("brCode") + "' ";
        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();

        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm8 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - PBB_BANKBRANCH - ILAS',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + columnNameValue.getObject("brCode") + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm8);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;
        //                try
        //                {
        //                    string sqlString11 = "insert into PBB_BANKBRANCH (PBK_BANKCODE,\n" +
        //                        "PBB_BRANCHCODE,\n" +
        //                        "CCN_CTRYCD,\n" +
        //                        "CCT_CITYCD,\n" +
        //                        "PBB_BRANCHNAME,\n" +
        //                        "PBB_DDSTEXTFILE)\n" +
        //                        "values(\n" +
        //                        " '" + logo1 + "', \n" +
        //                        " '" + columnNameValue.getObject("brCode") + "', \n" +
        //                        " '" + "001" + "', \n" +
        //                        " '" + "001" + "', \n" +
        //                        " '" + ccsDesc + "', \n" +
        //                        " '" + "N" + "' \n" +
        //                        ")";
        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString11, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();
        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                finally
        //                {
        //                    dbCon.Close();
        //                }
        //            }
        //            /////////////////////////

        //            string pcaAccount = "select lc.csd_value pcaAcc from lcsd_systemdtl lc where lc.csh_id = 'BKPOS' and lc.csd_type = '" + logo1 + "'";

        //            DataTable dt_new = new DataTable();
        //            dt_new = GetdataOraOledb(pcaAccount);

        //            if (dt_new.Rows.Count > 0)
        //            {
        //                pcaAccount = dt_new.Rows[0]["pcaAcc"].ToString();
        //            }
        //            else
        //            {
        //                pcaAccount = "";
        //            }

        //            ans = validatedata8(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "Oa")
        //            {


        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;

        //                try
        //                {
        //                    string qryupdbkbr = "update PBA_BANKACCOUNT set PCM_COMPCODE = '" + "01" + "', " +
        //                        "PCA_ACCOUNT = '" + pcaAccount + "', " +
        //                        "PCU_CURRCODE = '" + "1" + "', " +
        //                        "PBA_ACTIVE = '" + "Y" + "', " +
        //                        "PCL_LOCATCODE = '" + "180" + "' " +
        //                        "where PBK_BANKCODE = '" + logo1 + "' " +
        //                        "and PBB_BRANCHCODE = '" + columnNameValue.getObject("brCode") + "' " +
        //                        "and PBA_SERIAL = '" + "1" + "' ";
        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(qryupdbkbr, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();

        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";

        //                }

        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm9 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - PBA_BANKACCOUNT - ILAS',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + columnNameValue.getObject("brCode") + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm9);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            else
        //            {

        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;
        //                try
        //                {
        //                    string sqlString12 = "insert into PBA_BANKACCOUNT (PBK_BANKCODE,\n" +
        //                        "PBB_BRANCHCODE,\n" +
        //                        "PBA_SERIAL,\n" +
        //                        "PCM_COMPCODE,\n" +
        //                        "PCA_ACCOUNT,\n" +
        //                        "PCU_CURRCODE,\n" +
        //                        "PBA_ACTIVE,\n" +
        //                        "PCL_LOCATCODE)\n" +
        //                        "values(\n" +
        //                        " '" + logo1 + "', \n" +
        //                        " '" + columnNameValue.getObject("brCode") + "', \n" +
        //                        " '" + "1" + "', \n" +
        //                        " '" + "01" + "', \n" +
        //                        " '" + pcaAccount + "', \n" +   //PCA ACCOUNT check it <-------------------------
        //                        " '" + "1" + "', \n" +
        //                        " '" + "Y" + "', \n" +
        //                        " '" + "180" + "' \n" +
        //                        ")";
        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString12, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();
        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                finally
        //                {
        //                    dbCon.Close();
        //                }
        //            }



        //            ans = validatedata9(cchP, ccdP, brCdP, fixCCSCode, use_userid_t4BSO, use_userid_t4BM, aag_agcode_t4, CSD_TYPE_t7, logo1);
        //            if (ans == "P")
        //            {
        //                lblAlert.ForeColor = System.Drawing.Color.Red;
        //                lblAlert.Text = "";
        //                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Record Already found','')", true);
        //                //insert into log-table
        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;
        //                try
        //                {
        //                    string sqlString14 = "update LAAG_AGENT set CNT_NATCD = '" + "586" + "', " +
        //                        "PBK_BANKCODE = '" + logo1 + "', " +
        //                        "PCM_COMPCODE = '" + "01" + "', " +
        //                        "CHL_LEVEL = '" + "001" + "', " +
        //                        "PCL_LOCATCODE = '" + "180" + "', " +
        //                        "CDG_DESIGCODE = '" + "500" + "', " +
        //                        "CRG_RELGCD = '" + "999" + "', " +
        //                        "AAG_NAME = '" + logo1 + "-" + txtBranchName.Text + "', " +
        //                        "AAG_JOINDAT = to_date('04/01/2022', 'MM/dd/yyyy'), " +
        //                        "AAG_IMEDSUPR = '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', " +
        //                        "EXT_NACTIVE = '" + "Y" + "', " +
        //                        "AAG_DIRECT = '" + "N" + "', " +
        //                        "AAG_BROKER = '" + "B" + "', " +
        //                        "AAG_STATUS = '" + "C" + "', " +
        //                        "AAG_SALARIED = '" + "N" + "', " +
        //                        "AAG_SALEFCTDAT = to_date('04/01/2022','MM/dd/yyyy') " +
        //                        "where AAG_AGCODE = '" + aag_agcode_t4 + "' ";
        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString14, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();


        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }

        //                try
        //                {

        //                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
        //                    string sqlComm10 = "insert into error_log\n" +
        //                    "  (process_type,\n" +
        //                    "   procedure_name,\n" +
        //                    "   error_date,\n" +
        //                    "   error_code,\n" +
        //                    "   error_msg,\n" +
        //                    "   doc_name,\n" +
        //                    "   username,\n" +
        //                    "   status_date)\n" +
        //                    "values\n" +
        //                    "  ('BNKBRNCH_UPLOAD',\n" +
        //                    "   '" + logo1 + "_BNKBRNCH" + "',\n" +
        //                    "  sysdate,\n" +
        //                    "   '5010',\n" +
        //                    "  'Record already found - LAAG_AGENT-ILAS',\n" +
        //                    "   '" + ddlCCH_CHANNELCD.Text + "-" + ddlCCD_CHANNELDTLCD.Text + "-" + aag_agcode_t4 + "-" + logo1 + "' ,\n" +
        //                    "   '" + user + "',\n" +
        //                    "  sysdate)";
        //                    DB.executeDML(sqlComm10);
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                System.Data.OleDb.OleDbConnection dbCon = null;
        //                System.Data.OleDb.OleDbDataAdapter dbAdapter;
        //                System.Data.OleDb.OleDbCommand dbCom;
        //                try
        //                {
        //                    string sqlString13 = "insert into LAAG_AGENT (AAG_AGCODE,\n" +
        //                        "CNT_NATCD,\n" +
        //                        "PBK_BANKCODE,\n" +
        //                        "PCM_COMPCODE,\n" +
        //                        "CHL_LEVEL,\n" +
        //                        "PCL_LOCATCODE,\n" +
        //                        "CDG_DESIGCODE,\n" +
        //                        "CRG_RELGCD,\n" +
        //                        "AAG_NAME,\n" +
        //                        "AAG_JOINDAT,\n" +
        //                        "AAG_IMEDSUPR,\n" +
        //                        "EXT_NACTIVE,\n" +
        //                        "AAG_DIRECT,\n" +
        //                        "AAG_BROKER,\n" +
        //                        "AAG_STATUS,\n" +
        //                        "AAG_SALARIED,\n" +
        //                        "AAG_SALEFCTDAT)\n" +
        //                        "values(\n" +
        //                        " '" + aag_agcode_t4 + "', \n" +
        //                        " '" + "586" + "', \n" +
        //                        " '" + logo1 + "', \n" +
        //                        " '" + "01" + "', \n" +
        //                        " '" + "001" + "', \n" +
        //                        " '" + "180" + "', \n" +
        //                        " '" + "500" + "', \n" +
        //                        " '" + "999" + "', \n" +
        //                        " '" + logo1 + "-" + txtBranchName.Text + "', \n" +
        //                        " to_date('04/01/2022','MM/dd/yyyy'), \n" +
        //                        " '" + columnNameValue.getObject("AAG_IMEDSUPR_12t") + "', \n" +
        //                        " '" + "Y" + "', \n" +
        //                        " '" + "N" + "', \n" +
        //                        " '" + "B" + "', \n" +
        //                        " '" + "C" + "', \n" +
        //                        " '" + "N" + "', \n" +
        //                        " to_date('04/01/2022','MM/dd/yyyy') \n" +
        //                        ")";

        //                    string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"];
        //                    dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
        //                    dbCon.Open();
        //                    dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //                    dbCom = new System.Data.OleDb.OleDbCommand(sqlString13, dbCon);
        //                    int x = dbCom.ExecuteNonQuery();

        //                    //lblAlert.ForeColor = System.Drawing.Color.Green;
        //                    //lblAlert.Text = "Recoed Saved... ";
        //                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "swal('','Recoed Saved','')", true);

        //                }
        //                catch (Exception)
        //                {
        //                    lblAlert.ForeColor = System.Drawing.Color.Red;
        //                    lblAlert.Text = "";
        //                }
        //                finally
        //                {
        //                    dbCon.Close();
        //                }
        //            }
        //            String BSOUS = use_userid_t4BSO;
        //            String BMUS = use_userid_t4BM;

        //            string message = "Record Saved ";
        //            string script = $@" swal({{title: '',text: '{message} - {BSOUS} - {BMUS}',}});";

        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", script, true);
        //        }
        //        else
        //        {
        //            //lblAlert.ForeColor = System.Drawing.Color.Red;
        //            //lblAlert.Text = "Record Already Found....."; // + ex.Message;
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "",
        //                "swal('','Record Already Found','')", true);
        //        }
        //    }

        //    else
        //    {
        //        //lblAlert.ForeColor = System.Drawing.Color.Red;
        //        //lblAlert.Text = "Blank Files are not Required...";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "",
        //            "swal('','Branch Code and Name must be required.','')", true);
        //    }


        //}



        private string SUBSTR(string txtAgencyCode, int v1, int v2)
        {
            throw new NotImplementedException();
        }

    }





}
