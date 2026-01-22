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
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using shsm;
using SHAB.Data;
using SHAB.Business;
using SHAB.Shared.Exceptions;
namespace Bancassurance.Presentation
{
    public partial class shgn_ss_se_stdscreen_ILUS_ET_CD_COMPANYDET : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.ImageButton ImageButton1;
        private string countryCode = "";
        private string provinceCode = "";
        private string cityCode = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ace.POLICY_ACCEPTANCE Polacc = new ace.POLICY_ACCEPTANCE();
            bool check = false;
            bool validate = false;

            if (Session["NP1_PROPOSAL"] != null)
            {
                check = Polacc.CheckIfPolicyApproved(Session["NP1_PROPOSAL"].ToString());
                validate = Polacc.CheckIfPolicyValidate(Session["NP1_PROPOSAL"].ToString());
            }

            if (Session["NP1_PROPOSAL"] == null)
            {
                Response.Write("<script type='text/javascript'>");
                Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL','Please select values from personal.');");
                Response.Write("</script>");
            }
            else if (check == true)
            {
                Response.Write("<script type='text/javascript'>");
                Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL', 'Proposal has been approved.');");
                Response.Write("</script>");
            }

            if (validate != false)
            {
                Response.Write("<script type='text/javascript'>");
                Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL','" + BA.BAUtility.Validate_Text() + "')");
                Response.Write("</script>");
            }
            else
            {
                CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
                if (!IsPostBack)
                {
                    GetAssigneeList();
                    GetCountryList();
                    GetCompanyList();
                    GetAddressByCategory();
                    txtNAS_PERCENTAGE.Text = "100";
                }
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

        //Get Country List
        public void GetCountryList()
        {
            IDataReader LCSD_SYSTEMDTLReader0 = null;
            try
            {
                LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getAll_RO();
                ddlCCN_COUNTRY.DataSource = LCSD_SYSTEMDTLReader0;
                ddlCCN_COUNTRY.DataBind();
                /*ddlCCN_COUNTRY.Items.Insert(0,new ListItem("",""));
				if(ddlCCN_COUNTRY.Items.Count > 1)
				{
					ddlCCN_COUNTRY.SelectedIndex =1;
				}*/
            }
            catch
            {
            }
            finally
            {
                if (LCSD_SYSTEMDTLReader0.IsClosed == false)
                {
                    LCSD_SYSTEMDTLReader0.Close();
                }
            }
        }
        public void GetCompanyList()
        {
            IDataReader LCSD_SYSTEMDTLReader0 = null;
            try
            {
                LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getAll_Companies();
                ddlNP1_ASSIGNMENTCD.DataSource = LCSD_SYSTEMDTLReader0;
                ddlNP1_ASSIGNMENTCD.DataBind();
                ddlNP1_ASSIGNMENTCD.SelectedValue = Convert.ToString(Session["s_ccd_code"]);
            }
            catch
            {
            }
            finally
            {
                if (LCSD_SYSTEMDTLReader0.IsClosed == false)
                {
                    LCSD_SYSTEMDTLReader0.Close();
                }
            }
        }
        public void GetAssigneeList()
        {
            IDataReader LCSD_SYSTEMDTLReader0 = null;
            try
            {
                LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getAll_Assignee();
                ddl_CompanyLOV.DataSource = LCSD_SYSTEMDTLReader0;
                ddl_CompanyLOV.DataBind();
            }
            catch
            {
            }
            finally
            {
                if (LCSD_SYSTEMDTLReader0.IsClosed == false)
                {
                    LCSD_SYSTEMDTLReader0.Close();
                }
            }
        }

        //Get City By Country Id.
        public void GetCityById()
        {
            IDataReader LCSD_SYSTEMDTLReader0 = null;
            try
            {
                LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getcity(this.countryCode, this.provinceCode);
                ddlCCN_CITY.DataSource = LCSD_SYSTEMDTLReader0;
                ddlCCN_CITY.DataBind();
                ddlCCN_CITY.Items.Insert(0, new ListItem("", ""));
                if (ddlCCN_CITY.Items.Count > 1)
                {
                    ddlCCN_CITY.SelectedIndex = 1;
                }
            }
            catch (Exception EX)
            {
                string STR = EX.Message;
            }
            finally
            {
                if (LCSD_SYSTEMDTLReader0.IsClosed == false)
                {
                    LCSD_SYSTEMDTLReader0.Close();
                }
            }
        }
        // Get Province BY Country ID

        public void GetProvinceById()
        {
            IDataReader LCSD_SYSTEMDTLReader0 = null;
            try
            {
                LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.GetProvince(this.countryCode);
                ddlCCN_PROVINCE.DataSource = LCSD_SYSTEMDTLReader0;
                ddlCCN_PROVINCE.DataBind();
                LCSD_SYSTEMDTLReader0.Close();
                ddlCCN_PROVINCE.Items.Insert(0, new ListItem("", ""));
                if (ddlCCN_PROVINCE.Items.Count > 1)
                {
                    ddlCCN_PROVINCE.SelectedIndex = 1;
                }
            }
            catch (Exception EX)
            {
                string STR = EX.Message;
            }
            finally
            {
                if (LCSD_SYSTEMDTLReader0.IsClosed == false)
                {
                    LCSD_SYSTEMDTLReader0.Close();
                }
            }
        }

        protected void ddlCCN_COUNTRY_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.countryCode == "")
                {
                    this.countryCode = ddlCCN_COUNTRY.SelectedValue;
                }

                GetProvinceById();
                if (this.provinceCode == "")
                {
                    this.provinceCode = ddlCCN_PROVINCE.SelectedValue;
                }
                GetCityById();
            }
            catch
            {

            }
        }

        protected void ddlCCN_PROVINCE_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.countryCode == "")
                {
                    this.countryCode = ddlCCN_COUNTRY.SelectedValue;
                }
                if (this.provinceCode == "")
                {
                    this.provinceCode = ddlCCN_PROVINCE.SelectedValue;
                }
                GetCityById();
            }
            catch
            {
            }
        }

        private void InsertAll()
        {
            try
            {
                SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
                NameValueCollection columnNameValue = new NameValueCollection();

                //Setting Columns Values			
                columnNameValue.Add("NP1_ASSIGNMENTCD", ddlNP1_ASSIGNMENTCD.SelectedItem.Text.Trim() == "" ? null : ddlNP1_ASSIGNMENTCD.SelectedItem.Text.Trim().Split('-')[0]);
                columnNameValue.Add("CCN_CTRYCD", ddlCCN_COUNTRY.SelectedValue.Trim() == "" ? null : ddlCCN_COUNTRY.SelectedValue);
                columnNameValue.Add("CPR_PROVCD", ddlCCN_PROVINCE.SelectedValue.Trim() == "" ? null : ddlCCN_PROVINCE.SelectedValue);
                columnNameValue.Add("CCT_CITYCD", ddlCCN_CITY.SelectedValue.Trim() == "" ? null : ddlCCN_CITY.SelectedValue);
                columnNameValue.Add("NAS_NAME", txtNAS_NAME.Text.Trim() == "" ? null : txtNAS_NAME.Text);
                columnNameValue.Add("NAS_ADDRESS", txtNAS_ADDRESS.Text.Trim() == "" ? null : txtNAS_ADDRESS.Text);
                columnNameValue.Add("NAS_POBOX", txtNAS_POBOX.Text.Trim() == "" ? "1" : txtNAS_POBOX.Text.Trim());
                columnNameValue.Add("NAS_TELEPHONE", txtNAS_TELEPHONE.Text.Trim() == "" ? "1" : txtNAS_TELEPHONE.Text);
                columnNameValue.Add("NAS_FAX", txtNAS_FAX.Text.Trim() == "" ? "1" : txtNAS_FAX.Text);
                columnNameValue.Add("NAS_ASSDATE", txtNAS_ASSDATE.Text.Trim() == "" ? null : txtNAS_ASSDATE.Text);
                columnNameValue.Add("NAS_AMOUNT", txtNAS_AMOUNT.Text.Trim() == "" ? null : txtNAS_AMOUNT.Text);
                columnNameValue.Add("NAS_PERCENTAGE", txtNAS_PERCENTAGE.Text.Trim() == "" ? null : txtNAS_PERCENTAGE.Text);
                columnNameValue.Add("NAS_IDNO", txtNAS_IDNO.Text.Trim() == "" ? null : txtNAS_IDNO.Text);

                DataTable dt = DB.getDataTable("Select com_id,com_ntn From LNCO_COMPANY where (com_ntn = '" + txtNAS_IDNO.Text.Trim() + "' or upper(com_name) = '" + txtNAS_NAME.Text.Trim() + "')");
                if (dt.Rows.Count > 0)
                {

                    string sqlUpdateCompany = "update LNCO_COMPANY\n" +
                    "   set CCN_CTRYCD = '" + columnNameValue.getObject("CCN_CTRYCD") + "', CPR_PROVCD = '" + columnNameValue.getObject("CPR_PROVCD") + "', CCT_CITYCD = '" + columnNameValue.getObject("CCT_CITYCD") + "',\n" +
                    "                    COM_ADDRESS = '" + columnNameValue.getObject("NAS_ADDRESS") + "', NAD_TELNO1 = '" + columnNameValue.getObject("NAS_TELEPHONE") + "', NAD_POBOX = '" + columnNameValue.getObject("NAS_POBOX") + "',\n" +
                    "                    NAD_FAXNO = '" + columnNameValue.getObject("NAS_FAX") + "', USE_USERID = '" + Convert.ToString(Session["s_use_userid"]) + "', USE_TIMEDATE = sysdate\n" +
                    " where com_id = '" + Convert.ToString(dt.Rows[0]["com_id"]) + "'\n" +
                    "   and COM_NTN = '" + Convert.ToString(dt.Rows[0]["com_ntn"]) + "'";
                    DB.executeDML(sqlUpdateCompany);
                }
                else
                {
                    string sqlInsertCompany = "insert into LNCO_COMPANY\n" +
                    "  (COM_ID,\n" +
                    "   COM_NAME,\n" +
                    "   CCN_CTRYCD,\n" +
                    "   CPR_PROVCD,\n" +
                    "   CCT_CITYCD,\n" +
                    "   COM_NTN,\n" +
                    "   COM_ADDRESS,\n" +
                    "   NAD_TELNO1,\n" +
                    "   NAD_POBOX,\n" +
                    "   NAD_FAXNO,\n" +
                    "   USE_USERID,\n" +
                    "   USE_TIMEDATE)\n" +
                    "values\n" +
                    "  (Companycd_seq.Nextval,\n" +
                    "   '" + columnNameValue.getObject("NAS_NAME") + "',\n" +
                    "   '" + columnNameValue.getObject("CCN_CTRYCD") + "',\n" +
                    "   '" + columnNameValue.getObject("CPR_PROVCD") + "',\n" +
                    "   '" + columnNameValue.getObject("CCT_CITYCD") + "',\n" +
                    "   '" + columnNameValue.getObject("NAS_IDNO") + "',\n" +
                    "   '" + columnNameValue.getObject("NAS_ADDRESS") + "',\n" +
                    "   '" + columnNameValue.getObject("NAS_TELEPHONE") + "',\n" +
                    "   '" + columnNameValue.getObject("NAS_POBOX") + "',\n" +
                    "   '" + columnNameValue.getObject("NAS_FAX") + "',\n" +
                    "   '" + Convert.ToString(Session["s_use_userid"]) + "',\n" +
                    "   sysdate)";
                    DB.executeDML(sqlInsertCompany);
                    GetCompanyList();
                }
                //Executing DML
                //

                string sqlString = "insert into  LNAS_ASSIGNEE (NP1_PROPOSAL,\n" +
                "NP1_ASSIGNMENTCD,\n" +
                "NAS_ASSIGNMENTNO,\n" +
                "USE_USERID,\n" +
                "USE_DATETIME,\n" +
                "CCN_CTRYCD,\n" +
                "CPR_PROVCD,\n" +
                "CCT_CITYCD,\n" +
                "NAS_NAME,\n" +
                "NAS_ADDRESS,\n" +
                "NAS_POBOX,\n" +
                "NAS_TELEPHONE,\n" +
                "NAS_FAX,\n" +
                "NAS_ASSDATE,\n" +
                "NAS_AMOUNT,\n" +
                "NAS_PERCENTAGE,\n" +
                "NAS_IDNO)\n" +
                "values(\n" +
                " '" + Session["NP1_PROPOSAL"].ToString() + "', \n" +
                " '" + columnNameValue.getObject("NP1_ASSIGNMENTCD") + "', \n" +
                "  assignee_seq.nextval,\n" +
                " '" + Session["s_USE_USERID"].ToString() + "', \n" +
                "  sysdate, \n" +
                " '" + columnNameValue.getObject("CCN_CTRYCD") + "', \n" +
                " '" + columnNameValue.getObject("CPR_PROVCD") + "', \n" +
                " '" + columnNameValue.getObject("CCT_CITYCD") + "', \n" +
                " '" + columnNameValue.getObject("NAS_NAME") + "', \n" +
                " '" + columnNameValue.getObject("NAS_ADDRESS") + "', \n" +
                " '" + columnNameValue.getObject("NAS_POBOX").ToString().Replace(",", "") + "', \n" +
                " '" + columnNameValue.getObject("NAS_TELEPHONE") + "', \n" +
                " '" + columnNameValue.getObject("NAS_FAX") + "', \n" +
                " to_date('" + columnNameValue.getObject("NAS_ASSDATE") + "','dd/MM/yyyy'), \n" +
                " '" + columnNameValue.getObject("NAS_AMOUNT").ToString().Replace(",", "") + "', \n" +
                " '" + columnNameValue.getObject("NAS_PERCENTAGE") + "', \n" +
                " '" + columnNameValue.getObject("NAS_IDNO") + "' \n" +
                ")";

                DB.executeDML(sqlString);
            }
            catch (Exception err)
            {


            }

        }

        public void UpdateRecord()
        {
            try
            {
                SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
                NameValueCollection columnNameValue = new NameValueCollection();

                //Setting Columns Values			
                columnNameValue.Add("NP1_ASSIGNMENTCD", ddlNP1_ASSIGNMENTCD.SelectedItem.Text.Trim() == "" ? null : ddlNP1_ASSIGNMENTCD.SelectedItem.Text.Trim().Split('-')[0]);
                columnNameValue.Add("CCN_CTRYCD", ddlCCN_COUNTRY.SelectedValue.Trim() == "" ? null : ddlCCN_COUNTRY.SelectedValue);
                columnNameValue.Add("CPR_PROVCD", ddlCCN_PROVINCE.SelectedValue.Trim() == "" ? null : ddlCCN_PROVINCE.SelectedValue);
                columnNameValue.Add("CCT_CITYCD", ddlCCN_CITY.SelectedValue.Trim() == "" ? null : ddlCCN_CITY.SelectedValue);
                columnNameValue.Add("NAS_NAME", txtNAS_NAME.Text.Trim() == "" ? null : txtNAS_NAME.Text);
                columnNameValue.Add("NAS_ADDRESS", txtNAS_ADDRESS.Text.Trim() == "" ? null : txtNAS_ADDRESS.Text);
                columnNameValue.Add("NAS_POBOX", txtNAS_POBOX.Text.Trim() == "" ? "1" : txtNAS_POBOX.Text.Trim());
                columnNameValue.Add("NAS_TELEPHONE", txtNAS_TELEPHONE.Text.Trim() == "" ? "1" : txtNAS_TELEPHONE.Text);
                columnNameValue.Add("NAS_FAX", txtNAS_FAX.Text.Trim() == "" ? "1" : txtNAS_FAX.Text);
                columnNameValue.Add("NAS_ASSDATE", txtNAS_ASSDATE.Text.Trim() == "" ? null : txtNAS_ASSDATE.Text);
                columnNameValue.Add("NAS_AMOUNT", txtNAS_AMOUNT.Text.Trim() == "" ? null : txtNAS_AMOUNT.Text);
                columnNameValue.Add("NAS_PERCENTAGE", txtNAS_PERCENTAGE.Text.Trim() == "" ? null : txtNAS_PERCENTAGE.Text);
                columnNameValue.Add("NAS_IDNO", txtNAS_IDNO.Text.Trim() == "" ? null : txtNAS_IDNO.Text);


                DataTable dt = DB.getDataTable("Select com_id,com_ntn From LNCO_COMPANY where (com_ntn = '" + txtNAS_IDNO.Text.Trim() + "' or upper(com_name) = '" + txtNAS_NAME.Text.Trim() + "')");
                if (dt.Rows.Count > 0)
                {

                    string sqlUpdateCompany = "update LNCO_COMPANY\n" +
                    "   set CCN_CTRYCD = '" + columnNameValue.getObject("CCN_CTRYCD") + "', CPR_PROVCD = '" + columnNameValue.getObject("CPR_PROVCD") + "', CCT_CITYCD = '" + columnNameValue.getObject("CCT_CITYCD") + "',\n" +
                    "                    COM_ADDRESS = '" + columnNameValue.getObject("NAS_ADDRESS") + "', NAD_TELNO1 = '" + columnNameValue.getObject("NAS_TELEPHONE") + "', NAD_POBOX = '" + columnNameValue.getObject("NAS_POBOX") + "',\n" +
                    "                    NAD_FAXNO = '" + columnNameValue.getObject("NAS_FAX") + "', USE_USERID = '" + Convert.ToString(Session["s_use_userid"]) + "', USE_TIMEDATE = sysdate\n" +
                    " where com_id = '" + Convert.ToString(dt.Rows[0]["com_id"]) + "'\n" +
                    "   and COM_NTN = '" + Convert.ToString(dt.Rows[0]["com_ntn"]) + "'";
                    DB.executeDML(sqlUpdateCompany);
                }


                //Executing DML					
                string qry = "UPDATE LNAS_ASSIGNEE SET CCN_CTRYCD='" + columnNameValue.getObject("CCN_CTRYCD") + "', CPR_PROVCD='" + columnNameValue.getObject("CPR_PROVCD") + "'," +
                    "CCT_CITYCD='" + columnNameValue.getObject("CCT_CITYCD") + "',NAS_NAME='" + columnNameValue.getObject("NAS_NAME") + "'," +
                    "NAS_ADDRESS='" + columnNameValue.getObject("NAS_ADDRESS") + "',NAS_POBOX='" + columnNameValue.getObject("NAS_POBOX").ToString().Replace(",", "") + "'," +
                    "NAS_TELEPHONE='" + columnNameValue.getObject("NAS_TELEPHONE") + "',NAS_FAX='" + columnNameValue.getObject("NAS_FAX") + "'," +
                    "NAS_ASSDATE=to_date('" + columnNameValue.getObject("NAS_ASSDATE") + "','dd/MM/yyyy'),NAS_AMOUNT='" + columnNameValue.getObject("NAS_AMOUNT").ToString().Replace(",", "") + "'," +
                    "NAS_IDNO='" + columnNameValue.getObject("NAS_IDNO") + "',NAS_PERCENTAGE='" + columnNameValue.getObject("NAS_PERCENTAGE") + "'" +
                    " WHERE NP1_PROPOSAL ='" + Session["NP1_PROPOSAL"].ToString() + "'  AND NP1_ASSIGNMENTCD = '" + columnNameValue.getObject("NP1_ASSIGNMENTCD") + "' ";

                DB.executeDML(qry);
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public void GetAddressByCategory()
        {
            try
            {
                string proposalno = Session["NP1_PROPOSAL"].ToString();
                rowset rsLNPH_PHOLDERDB = DB.executeQuery("select * from LNAS_ASSIGNEE where np1_proposal='" + proposalno + "'");

                if (rsLNPH_PHOLDERDB.next())
                {
                    this.countryCode = rsLNPH_PHOLDERDB.getString("CCN_CTRYCD");
                    this.provinceCode = rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
                    this.cityCode = rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
                    ddlCCN_COUNTRY.SelectedValue = countryCode;
                    txtNAS_NAME.Text = rsLNPH_PHOLDERDB.getString("NAS_NAME");
                    txtNAS_ADDRESS.Text = rsLNPH_PHOLDERDB.getString("NAS_ADDRESS");
                    txtNAS_TELEPHONE.Text = rsLNPH_PHOLDERDB.getString("NAS_TELEPHONE");
                    txtNAS_FAX.Text = rsLNPH_PHOLDERDB.getString("NAS_FAX");
                    txtNAS_ASSDATE.Text = DateTime.Parse(rsLNPH_PHOLDERDB.getString("NAS_ASSDATE").ToString()).ToString("dd/MM/yyyy");
                    txtNAS_IDNO.Text = rsLNPH_PHOLDERDB.getString("NAS_IDNO");
                    txtNAS_AMOUNT.Text = rsLNPH_PHOLDERDB.getString("NAS_AMOUNT");
                    txtNAS_PERCENTAGE.Text = rsLNPH_PHOLDERDB.getString("NAS_PERCENTAGE");
                    txtNAS_POBOX.Text = rsLNPH_PHOLDERDB.getString("NAS_POBOX");
                    GetProvinceById(ddlCCN_COUNTRY.SelectedValue);
                    GetCityById(ddlCCN_COUNTRY.SelectedValue, rsLNPH_PHOLDERDB.getString("CPR_PROVCD"));
                    ddlCCN_PROVINCE.SelectedValue = rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
                    ddlCCN_CITY.SelectedValue = rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
                    txtNAS_IDNO.Enabled = false;
                    txtNAS_NAME.Enabled = false;
                }
                else
                {
                    txtNAS_ADDRESS.Text = "";
                    txtNAS_TELEPHONE.Text = "";
                    txtNAS_FAX.Text = "";
                    txtNAS_ASSDATE.Text = "";
                    txtNAS_IDNO.Text = "";
                    txtNAS_AMOUNT.Text = "";
                    txtNAS_PERCENTAGE.Text = "";
                    ddlCCN_PROVINCE.SelectedValue = "";
                    ddlCCN_CITY.SelectedValue = "";
                    ddlCCN_COUNTRY.SelectedValue = "";
                    txtNAS_POBOX.Text = "";
                }
                ddlCCN_COUNTRY.SelectedValue = countryCode;
                ddlCCN_PROVINCE.SelectedValue = provinceCode;
                ddlCCN_CITY.SelectedValue = cityCode;
            }
            catch (Exception EX)
            {
                string str = EX.Message;
            }
        }

        public void GetCityById(string countryCode, string provinceCode)
        {
            try
            {
                IDataReader LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getcity(countryCode, provinceCode);
                ddlCCN_CITY.DataSource = LCSD_SYSTEMDTLReader0;
                ddlCCN_CITY.DataBind();
                LCSD_SYSTEMDTLReader0.Close();
            }
            catch (Exception EX)
            {
                string STR = EX.Message;
            }
        }

        public void GetProvinceById(string countryCode)
        {
            try
            {
                IDataReader LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.GetProvince(countryCode);
                ddlCCN_PROVINCE.DataSource = LCSD_SYSTEMDTLReader0;
                ddlCCN_PROVINCE.DataBind();
                LCSD_SYSTEMDTLReader0.Close();
            }
            catch (Exception EX)
            {
                string STR = EX.Message;
            }


        }

        public void CheckInsertOrUpdate()
        {
            try
            {
                string proposalno = Session["NP1_PROPOSAL"].ToString();
                rowset rsLNPH_PHOLDERDB = DB.executeQuery("select 1 from LNAS_ASSIGNEE R WHERE R.NP1_PROPOSAL='" + proposalno + "'");

                if (rsLNPH_PHOLDERDB.next())
                {
                    UpdateRecord();
                }
                else
                {
                    InsertAll();
                }
            }
            catch
            {
                Response.Write("<script type='text/javascript'>");
                Response.Write("alert('Please select any above link to define address category');");
                Response.Write("</script>");
            }
        }

        bool saveUpdateClick = false;

        protected void btn_save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                CheckInsertOrUpdate();
                saveUpdateClick = true;
                //************* Activity Log *************//			
                Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.ADDRESS_UPDATED);
            }
            catch
            {

            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (saveUpdateClick)
            {
                //Response.Write("<script type='text/javascript'>");
                //Response.Write("parent.parent.setPageNavigate();");
                //Response.Write("</script>");
            }

        }

        protected void ddl_CompanyLOV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CompanyLOV.SelectedValue != null && ddl_CompanyLOV.SelectedValue != "" && ddl_CompanyLOV.SelectedIndex!=0)
                {
                    rowset rsLNPH_PHOLDERDB = DB.executeQuery("select * from LNCO_COMPANY where com_id='" + ddl_CompanyLOV.SelectedValue + "'");

                    if (rsLNPH_PHOLDERDB.next())
                    {
                        this.countryCode = rsLNPH_PHOLDERDB.getString("CCN_CTRYCD");
                        this.provinceCode = rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
                        this.cityCode = rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
                        ddlCCN_COUNTRY.SelectedValue = countryCode;
                        GetProvinceById(ddlCCN_COUNTRY.SelectedValue);
                        GetCityById(ddlCCN_COUNTRY.SelectedValue, rsLNPH_PHOLDERDB.getString("CPR_PROVCD"));
                        txtNAS_NAME.Text = rsLNPH_PHOLDERDB.getString("com_name");
                        txtNAS_ADDRESS.Text = rsLNPH_PHOLDERDB.getString("com_address");
                        txtNAS_TELEPHONE.Text = rsLNPH_PHOLDERDB.getString("nad_telno1");
                        txtNAS_FAX.Text = rsLNPH_PHOLDERDB.getString("nad_faxno");
                        txtNAS_IDNO.Text = rsLNPH_PHOLDERDB.getString("com_ntn");
                        txtNAS_POBOX.Text = rsLNPH_PHOLDERDB.getString("nad_pobox");
                        txtNAS_NAME.Enabled = false;
                        txtNAS_IDNO.Enabled = false;
                    }
                    else
                    {
                        txtNAS_NAME.Text = "";
                        txtNAS_ADDRESS.Text = "";
                        txtNAS_TELEPHONE.Text = "";
                        txtNAS_FAX.Text = "";
                        txtNAS_IDNO.Text = "";
                        ddlCCN_COUNTRY.SelectedIndex = 0;
                        ddlCCN_PROVINCE.Items.Clear();
                        ddlCCN_CITY.Items.Clear();
                        txtNAS_POBOX.Text = "";
                        txtNAS_AMOUNT.Text = "";
                        txtNAS_ASSDATE.Text = "";
                        txtNAS_NAME.Enabled = true;
                        txtNAS_IDNO.Enabled = true;

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void txtNAS_IDNO_TextChanged(object sender, EventArgs e)
        {
            if (txtNAS_IDNO.Text.Length == 7)
            {
                try
                {
                    rowset rsLNPH_PHOLDERDB = DB.executeQuery("select * from LNCO_COMPANY where com_ntn='" + txtNAS_IDNO.Text + "'");

                    if (rsLNPH_PHOLDERDB.next())
                    {
                        this.countryCode = rsLNPH_PHOLDERDB.getString("CCN_CTRYCD");
                        this.provinceCode = rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
                        this.cityCode = rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
                        ddlCCN_COUNTRY.SelectedValue = countryCode;
                        GetProvinceById(ddlCCN_COUNTRY.SelectedValue);
                        GetCityById(ddlCCN_COUNTRY.SelectedValue, rsLNPH_PHOLDERDB.getString("CPR_PROVCD"));
                        txtNAS_NAME.Text = rsLNPH_PHOLDERDB.getString("com_name");
                        txtNAS_ADDRESS.Text = rsLNPH_PHOLDERDB.getString("com_address");
                        txtNAS_TELEPHONE.Text = rsLNPH_PHOLDERDB.getString("nad_telno1");
                        txtNAS_FAX.Text = rsLNPH_PHOLDERDB.getString("nad_faxno");
                        txtNAS_IDNO.Text = rsLNPH_PHOLDERDB.getString("com_ntn");
                        txtNAS_POBOX.Text = rsLNPH_PHOLDERDB.getString("nad_pobox");

                        txtNAS_NAME.Enabled = false;
                        txtNAS_IDNO.Enabled = false;

                        // ddlCCN_PROVINCE.SelectedValue = rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
                        //ddlCCN_CITY.SelectedValue = rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
