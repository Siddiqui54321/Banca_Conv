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
using System.Data.SqlClient;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;

namespace Insurance.Presentation
{
    /// <summary>
    /// Summary description for ProposalSelectionLOV.
    /// </summary>
    public partial class ProposalSelectionLOV : System.Web.UI.Page
    {

        private bool SearchClicked = false;

        private const string PENDING = "Pending";
        private const string POSTED = "Posted";
        private const string REFERRED = "Referred";
        private const string APPROVED = "Approved";
        private const string DECLINED = "Declined";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //******** Application Oriented Logic ****************//
            CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
            if (ace.Ace_General.getApplicationName() == ace.Ace_General.APP_ILLUS)
            {
                ddlStatus.Visible = false;
                dgProposalLOV.Columns[2].Visible = false;
            }
            //******** Application Oriented Logic - End ****************//


            if (!IsPostBack)
            {
                if (Request.QueryString["SrcScreen"] != null)
                    ViewState["SrcSreen"] = Request.QueryString["SrcScreen"];

                this.LoadPersonalInfo();
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgProposalLOV.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgProposalLOV_ItemCommand);
            this.dgProposalLOV.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgProposalLOV_PageIndexChanged);

        }
        #endregion

        private void dgProposalLOV_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.LoadPersonalInfo();
            dgProposalLOV.CurrentPageIndex = e.NewPageIndex;
            dgProposalLOV.DataBind();
        }

        private void dgProposalLOV_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string src = ((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).CommandName;
            if (src.Equals("Select"))
            {
                string status = e.Item.Cells[3].Text;
                if (status != "Declined")
                {
                    string proposalid = e.Item.Cells[1].Text;
                    string prodcd = ace.Ace_General.getPPR_PRODCD(proposalid);

                    if (ViewState["SrcSreen"] == null)
                    {
                        Session.Add("NP1_PROPOSAL", proposalid);

                        if (prodcd != null)
                        {
                            Session.Add("PPR_PRODCD", prodcd);
                        }
                        else
                        {
                            Session.Remove("PPR_PRODCD");
                        }

                        Response.Write("<script language='Javascript'>");
                        Response.Write("window.opener.parent.location = window.opener.parent.location;");
                        Response.Write("window.close();");
                        Response.Write("</script>");

                        //***** Update Commencement Date if Proposal is not validated and User is not admin ******//
                        UpdateCommencmentDate(proposalid);

                        //************* Activity Log *************//
                        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_SELECTED);
                    }
                    else
                    {
                        if (prodcd != null)
                        {
                            Session.Add("PROPOSAL_ID", proposalid);
                            Session.Add("PLAN_ID", prodcd);
                        }
                        Response.Write("<script language='Javascript'>");
                        Response.Write("window.opener.location = window.opener.location;");
                        Response.Write("window.close();");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    Response.Write("<script language='Javascript'>");
                    Response.Write("alert('Selected Proposal has been Declined')");
                    Response.Write("</script>");
 
                }
            }
        }

        private void UpdateCommencmentDate(string proposal)
        {
            try
            {
                string userId = Convert.ToString(Session["s_USE_USERID"]).ToUpper();
                proposal = proposal.ToUpper();

                //******* If User is Admin Type then return *********//
                rowset rs = DB.executeQuery("select 'A' from USE_USERMASTER WHERE UPPER(USE_USERID)='" + userId + "' AND USE_TYPE='A' ");
                if (rs.next())
                {	//**** No need to update Commencement Date for Admin Type User *****//
                    return;
                }
                else
                {
                    /////    Confirm it from Bashir bhai ///////
                    //******* If Proposal validated then return *********//
                    //rs = DB.executeQuery("select 'A' from LNP2_POLICYMASTR WHERE UPPER(NP1_PROPOSAL)='"+proposal+"' AND NP2_SUBSTANDAR IS NOT NULL ");

                    //******* If Proposal Posted then return *********//
                    rs = DB.executeQuery("SELECT 'A' FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='" + proposal + "' AND NP1_SELECTED = 'Y'");
                    if (rs.next())
                    {
                        return;
                    }
                    else
                    {
                        //******* Update Commencement Date *********//
                        //DateTime dtNow = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day);
                        DateTime commDate = Convert.ToDateTime(Session["s_COMM_DATE"]);
                        //SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                        //pc.clear();
                        //pc.puts("@NP2_COMMENDATE", commDate, DbType.Date);
                        //pc.puts("@NP1_PROPOSAL",   proposal, DbType.String);
                        //DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP2_COMMENDATE=? WHERE NP1_PROPOSAL=? ", pc);
                        //DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_COMMENDATE=? WHERE NP1_PROPOSAL=? ", pc);

                        SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, commDate);
                        //SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDateByProposalDate(Convert.ToString(Session["NP1_PROPOSAL"]));					

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            if (IsPostBack)
            {
                SearchClicked = true;
                dgProposalLOV.CurrentPageIndex = 0;
                txtSearchEvent.Text = System.Convert.ToString(ddlsearch.SelectedIndex);
                this.LoadPersonalInfo();
            }
        }


        private void LoadPersonalInfo()
        {

            /********* Preparing Searching Criteria Query ********/
            string SearchCriteria = "";

            if (txtSearchEvent.Text == "1" || txtSearchEvent.Text == "2" || txtSearchEvent.Text == "3")
            {
                if (SearchClicked == false)
                {
                    ddlsearch.SelectedIndex = Convert.ToInt16(txtSearchEvent.Text.Trim() == "" ? "0" : txtSearchEvent.Text);
                }

                if (ddlsearch.SelectedIndex == 1)//Proposal Search
                {
                    SearchCriteria = " AND UPPER(NP1_PROPOSAL) LIKE UPPER('" + txtsearch.Text + "')";
                }
                else if (ddlsearch.SelectedIndex == 2)//Name Search
                {
                    SearchCriteria = " AND UPPER(NPH_FULLNAME) LIKE UPPER('" + txtsearch.Text + "') ";
                }
                else if (ddlsearch.SelectedIndex == 3)//NIC Search
                {
                    SearchCriteria = "  and (NPH_IDNO) LIKE '" + txtsearch.Text + "'";
                }

            }

            if (ddlStatus.SelectedValue == "O")//OPEN/PENDING
            {
                SearchCriteria += " AND LNP1.STATUS='" + PENDING + "' ";
            }
            else if (ddlStatus.SelectedValue == "P")
            {
                SearchCriteria += " AND LNP1.STATUS='" + POSTED + "' ";
            }
            else if (ddlStatus.SelectedValue == "A")
            {
                SearchCriteria += " AND LNP1.STATUS='" + APPROVED + "' ";
            }
            else if (ddlStatus.SelectedValue == "D")
            {
                SearchCriteria += " AND LNP1.STATUS='" + DECLINED + "' ";
            }
            else if (ddlStatus.SelectedValue == "R")
            {
                //SearchCriteria += " AND LNP1.STATUS='"+ REFERRED +"' ";
                SearchCriteria += " AND (LNP1.STATUS='" + REFERRED + "' OR LNP1.STATUS='" + DECLINED + "') ";
            }
            //else if(ddlStatus.SelectedValue == "D")
            //{
            //	SearchCriteria += " AND LNP1.STATUS='"+ DECLINED +"' ";
            //}

            /******* Preparing Query to pick Personal Information now ********/
            string dsn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
            OleDbConnection cnn = new OleDbConnection(dsn);
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);



            string ProposalStatus = " CASE "
                                  + "   WHEN NP1_SELECTED = 'Y' and (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B WHERE B.NP1_PROPOSAL = A.NP1_PROPOSAL) = 'Y' THEN '" + REFERRED + "' "
                                  + "   WHEN NP1_SELECTED = 'Y'   THEN '" + POSTED + "' "
                                  + "   WHEN CDC_CODE     = '001'  and A.CST_STATUSCD != '14' THEN '" + APPROVED + "' "
                //+ "   WHEN CDC_CODE     = '002' THEN '" + DECLINED + "' "
                                  + "   WHEN CDC_CODE     = '002' and A.CST_STATUSCD != '14' THEN '" + REFERRED + "' "
                                  + "   WHEN CDC_CODE     = '003' and A.CST_STATUSCD != '14' THEN '" + REFERRED + "' "
                                  + "   WHEN (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B WHERE B.NP1_PROPOSAL = A.NP1_PROPOSAL) = 'Y' THEN '" + REFERRED + "' "
                                  + "   WHEN A.CST_STATUSCD=14 then '" + DECLINED + "' "
                                  + "   ELSE '" + PENDING + "' "
                                  + " END STATUS  ";

            string query = "";

            //******** Application Oriented Logic ****************//
            if (ace.Ace_General.getApplicationName() == ace.Ace_General.APP_ILLUS)
            {
                SearchCriteria += " AND LNP1.STATUS='" + PENDING + "' ";
            }
            //******** Application Oriented Logic - End ****************//

            if (ViewState["SrcSreen"] == null)
            {

                //if(isAdminType(userType))				
                if (userType == "A" || userType == "B" || userType == "P")
                {
                    query = " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                    //query = "select * from ( SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR,np1_propdate "   //chg_closing comments above
                        + " FROM "
                        + "     LNPH_PHOLDER, "
                        + "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                        //+ "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED, A.np1_propdate from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "  //chg_closing comments above
                        + "     LCCN_COUNTRY CCN "
                        + " WHERE  "
                        + "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code in (SELECT ccd_code FROM luch_userchannel d WHERE m.CCH_CODE = D.CCH_CODE and m.ccd_code = d.ccd_code)) "
                        + "     AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        + "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') " //AND LNP1.USE_USERID='"  + user + "'
                        + SearchCriteria
                        + " order by 1 ";   //chg_closing comment line
//chg_closing
                    //    + " order by 1 ) a"
                    //+ " ";
                    //if (Convert.ToString(SessionObject.Get("ClossingFlag")) == "P")
                    //{
                    //    query += " where trunc(a.np1_propdate) <= to_Date('" + Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate"))).ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ";
//chg_closing_end
                    }
//chg_closing
                //    else
                //    {
                //        var today = Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate")));
                //        var monthStart = new DateTime(today.Year, today.Month, 1);
                //        var lastMonthEnd = monthStart.AddDays(-1);
                //        query += " where trunc(a.np1_propdate) > to_Date('" + lastMonthEnd.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ";
                //    }
                //}
//chg_closing_end
                else if (userType == "R")
                {
                    query = " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                        + " FROM "
                        + "     LNPH_PHOLDER, "
                        //						+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                        + "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD='" + Session["s_CCN_CTRYCD"] + "' AND NP1_CHANNEL='" + Session["s_CCH_CODE"] + "' AND NP1_CHANNELDETAIL='" + Session["s_CCD_CODE"] + "' ) LNP1, "
                        + "     LCCN_COUNTRY CCN "
                        + " WHERE  "
                        + "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code in (SELECT ccd_code FROM luch_userchannel d WHERE m.CCH_CODE = D.CCH_CODE and m.ccd_code = d.ccd_code)) "
                        + "     AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        + "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') " //AND LNP1.USE_USERID='"  + user + "'
                        + SearchCriteria
                        + " order by 1 ";
                }

                else if (userType == "S")
                {
                    /*System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("select m.np1_proposal, ccn_descr, nph_fullname, nph_idno,m.use_userid, ");
                    sb.Append(ProposalStatus);
                    sb.Append(" from lnp1_policymastr m ");
                    sb.Append("inner join luch_userchannel l on ");
                    sb.Append("		l.cch_code||l.ccd_code||l.ccs_code = m.pcl_locatcode and ");
                    sb.Append("		l.use_userid = m.use_userid "); //and m.np1_propdate > (sysdate-30)
                    sb.Append("inner join lnu1_underwriti u on ");
                    sb.Append("		u.np1_proposal = m.np1_proposal and  ");
                    //sb.Append("--   u.use_userid = m.use_userid and  ");
                    sb.Append("		u.nph_life = 'D' and  ");
                    sb.Append("		u.nu1_life = 'F' ");
                    sb.Append("inner join lnph_pholder h on ");
                    sb.Append("		h.nph_code = u.nph_code and  ");
                    sb.Append("		h.nph_life = u.nph_life ");
                    sb.Append("inner join lccn_country c on ");
                    sb.Append("		c.ccn_ctrycd = m.ccn_ctrycd       ");
                    sb.Append("where	ccn_ctrycd=SV(\"s_CCN_CTRYCD\") and  ");
                    sb.Append("			m.np1_selected is null and  ");
                    sb.Append("			m.use_userid = '");
                    sb.Append(user);
                    sb.Append("' and ");
                    sb.Append("			l.cch_code = SV(\"s_CCH_CODE\") and ");
                    sb.Append("			l.ccd_code = SV(\"s_CCD_CODE\") and "); 
                    sb.Append("			l.ccs_code = SV(\"s_CCS_CODE\") ");
                    sb.Append(SearchCriteria);
                    sb.Append(" order by 1");
                    query = sb.ToString();*/

                    query = " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                    //query = "Select * from( SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR,np1_propdate  " //chg_closing comments above
                        + " FROM "
                        + "     LNPH_PHOLDER, "
                        //+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, NP1_SELECTED from lnp1_policymastr where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                        + "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD='" + Session["s_CCN_CTRYCD"] + "' AND NP1_CHANNEL='" + Session["s_CCH_CODE"] + "' AND NP1_CHANNELDETAIL='" + Session["s_CCD_CODE"] + "' AND NP1_CHANNELSDETAIL='" + Session["s_CCS_CODE"] + "' ) LNP1, "
                        //+ "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED, A.np1_propdate from lnp1_policymastr A where CCN_CTRYCD='" + Session["s_CCN_CTRYCD"] + "' AND NP1_CHANNEL='" + Session["s_CCH_CODE"] + "' AND NP1_CHANNELDETAIL='" + Session["s_CCD_CODE"] + "' AND NP1_CHANNELSDETAIL='" + Session["s_CCS_CODE"] + "' ) LNP1, "    //chg_closing comment above
                        + "     LCCN_COUNTRY CCN "
                        + " WHERE  "
                        + "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code in (SELECT ccd_code FROM luch_userchannel d WHERE m.CCH_CODE = D.CCH_CODE and m.ccd_code = d.ccd_code)) "
                        + "  AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        + "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                        + "  AND (LNP1.NP1_SELECTED ='D' or LNP1.NP1_SELECTED is null) AND LNP1.USE_USERID='" + user + "'"
                        + SearchCriteria
                        + " order by 1 ";
//chg_closing
                    //    + " order by 1 ) a"
                    //    + " ";
                    //if (Convert.ToString(SessionObject.Get("ClossingFlag")) == "P")
                    //{
                    //    query += " where trunc(a.np1_propdate) <= to_Date('" + Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate"))).ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ";
                    //}
                    //else
                    //{
                    //    var today = Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate")));
                    //    var monthStart = new DateTime(today.Year, today.Month, 1);
                    //    var lastMonthEnd = monthStart.AddDays(-1);
                    //    query += " where trunc(a.np1_propdate) > to_Date('" + lastMonthEnd.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ";
                    //}
//chg_closing_end


                    /*query =" SELECT LNP1.NP1_PROPOSAL, CCN_DESCR, NPH_FULLNAME, NPH_IDNO "
                        +  " FROM "
                        +  "     LNPH_PHOLDER, "
                        //+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, NP1_SELECTED from lnp1_policymastr where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                        +  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, NP1_SELECTED from lnp1_policymastr where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_CHANNEL=SV(\"s_CCH_CODE\") AND NP1_CHANNELDETAIL=SV(\"s_CCD_CODE\") AND NP1_CHANNELSDETAIL=SV(\"s_CCS_CODE\") ) LNP1, "
                        +  "     LCCN_COUNTRY CCN "
                        +  " WHERE  "
                        +  "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.CCS_CODE = (SELECT CCS_CODE FROM luch_userchannel d WHERE USE_USERID='"  + user + "'"
                        +  "     and m.CCH_CODE=D.CCH_CODE and m.ccd_code=d.ccd_code and m.ccs_code=d.ccs_code)) "

                        +  "  AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        +  "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                        +  "  AND LNP1.NP1_SELECTED IS NULL "
                        + SearchCriteria
                        +  " order by 1 ";*/
                }
                else
                {
                    query = " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                        + " FROM "
                        + "     LNPH_PHOLDER, "
                        //+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, NP1_SELECTED from lnp1_policymastr where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                        + "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_CHANNEL=SV(\"s_CCH_CODE\") AND NP1_CHANNELDETAIL=SV(\"s_CCD_CODE\") AND NP1_CHANNELSDETAIL=SV(\"s_CCS_CODE\") AND USE_USERID=SV(\"s_USE_USERID\")  ) LNP1,  "
                        + "     LCCN_COUNTRY CCN "
                        + " WHERE  "
                        //+  "     LNP1.USE_USERID='"  + user + "'"
                        + "      LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        + "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                        + "  AND LNP1.NP1_SELECTED IS NULL "
                        + SearchCriteria
                        + " order by 1 ";
                }
            }
            else
            {
                //string allowedPlansList = ace.clsIlasUtility.getListFromSysDetail("APPBH","MANADJPLANS", false);
                string allowedPlansList = ace.clsIlasUtility.getListFromSysDetail("APPBH", "MANADJPLANS", false);
                if (allowedPlansList.Length < 4)
                {
                    allowedPlansList = "('')";
                }
                else
                {
                    allowedPlansList = allowedPlansList;
                }

                if (Convert.ToString(ViewState["SrcSreen"]).Equals("MANADJUSTMENT"))
                {
                    query = " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                        + " FROM "
                        + "     LNPH_PHOLDER, "
                        + "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, 'Pending' STATUS, NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_SELECTED IS NULL AND CDC_CODE IS NULL  "
                        + "         AND EXISTS (SELECT 'A' FROM LNPR_PRODUCT LNPR WHERE LNPR.NP1_PROPOSAL=A.NP1_PROPOSAL AND LNPR.PPR_PRODCD IN " + allowedPlansList + ") ) LNP1, "
                        + "     LCCN_COUNTRY CCN "
                        + " WHERE  "
                        + "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code = (SELECT ccd_code FROM luch_userchannel d WHERE USE_USERID = '" + user + "'"
                        + "     and m.CCH_CODE=D.CCH_CODE and m.ccd_code=d.ccd_code )) "
                        + "     AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                        + "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                        + " order by 1 ";
                }
            }


            /*
                        String query = " SELECT LNP1.NP1_PROPOSAL, CCN_DESCR, NPH_FULLNAME, NPH_IDNO "
                                     + " FROM "
                                     + "    LNPH_PHOLDER, "
                                     + "    (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID from lnp1_policymastr where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") ) LNP1, "
                                     + "    LCCN_COUNTRY CCN "
                                     + " WHERE  "
                                     + "    LNP1.USE_USERID='"  + System.Convert.ToString(SHMA.Enterprise.Presentation.SessionObject.Get("s_USE_USERID")) + "' AND "
                                     + "    LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                                     + "    (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                                     + SearchCriteria
                                     + " ORDER BY 1 ";

                        /******** Executing Query ************/
            query = EnvHelper.Parse(query);
            OleDbDataAdapter da = new OleDbDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "lnp1_policymastr");
            dgProposalLOV.DataSource = ds.Tables["lnp1_policymastr"];
            dgProposalLOV.DataBind();

            if (ViewState["SrcSreen"] == null)
            {
                SetControlValues();
            }
            else
            {
                ddlStatus.Visible = false;
                ddlsearch.Visible = false;
                txtsearch.Visible = false;
                Button1.Visible = false;
            }
        }

        /*private void LoadPersonalInfo(string SourceScreen)
        {
			
            if(SourceScreen.Equals("MANADJUSTMENT"))
            {
                string dsn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
                OleDbConnection cnn = new OleDbConnection( dsn );
                string user = System.Convert.ToString(Session["s_USE_USERID"]);
                string userType = ace.Ace_General.getUserType(user);

                ddlStatus.Visible = false;
                ddlsearch.Visible = false;
                txtsearch.Visible = false;
                Button1.Visible = false;

                string query =" SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
                    +  " FROM "
                    +  "     LNPH_PHOLDER, "
                    +  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, 'Pending' STATUS, NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_SELECTED IS NULL AND CDC_CODE IS NULL) LNP1, "
                    +  "     LCCN_COUNTRY CCN "
                    +  " WHERE  "
                    +  "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code = (SELECT ccd_code FROM luch_userchannel d WHERE USE_USERID = '"  + user + "'"
                    +  "     and m.CCH_CODE=D.CCH_CODE and m.ccd_code=d.ccd_code )) "
                    +  "     AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
                    +  "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
                    +  " order by 1 ";

                query = EnvHelper.Parse(query);
                OleDbDataAdapter da = new OleDbDataAdapter(query, cnn);
                DataSet ds = new DataSet(); 
                da.Fill(ds, "lnp1_policymastr"); 
                dgProposalLOV.DataSource = ds.Tables["lnp1_policymastr"];
                dgProposalLOV.DataBind();		
            }
        }*/

        private bool isAdminType(string userType)
        {
            bool valid = false;
            switch (userType)
            {
                case "A":
                    valid = true;
                    break;
                case "I":
                    valid = true;
                    break;
                case "C":
                    valid = true;
                    break;
                case "M":
                    valid = true;
                    break;
            }
            return valid;
        }

        private void SetControlValues()
        {
            if (txtSearchEvent.Text == "1")
            {
                ddlsearch.SelectedIndex = 1;
            }
            else if (txtSearchEvent.Text == "2")
            {
                ddlsearch.SelectedIndex = 2;
            }
            else if (txtSearchEvent.Text == "3")
            {
                ddlsearch.SelectedIndex = 3;
            }
            else
            {
                ddlsearch.SelectedIndex = 0;
                txtsearch.Text = "";
            }
        }
    }
}
