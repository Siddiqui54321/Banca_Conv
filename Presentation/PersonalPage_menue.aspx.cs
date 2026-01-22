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
using System.Web.Security;

namespace ACE.Presentation
{
    /// <summary>
    /// Summary description for PersonalPage.
    /// </summary>
    public partial class PersonalPage_menue : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.Literal ShowTip;
        protected System.Web.UI.WebControls.Image Image1;
        protected System.Web.UI.WebControls.Literal userType;
        protected System.Web.UI.WebControls.Literal FooterScript;

        private bool isLeftMenuSettingExists = false;


        private void designLeftMenuDiv()
        {
            IDataReader rdr2 = SHAB.Data.LCSD_SYSTEMDTLDB.getUIMenuExist(SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE"));
            while (rdr2.Read())
            {

                this.isLeftMenuSettingExists = true;
                break;
            }
            rdr2.Close();
            rdr2.Dispose();

            IDataReader rdr = SHAB.Data.LCSD_SYSTEMDTLDB.getUIMenu(SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE"));
            System.Text.StringBuilder menuSB = new System.Text.StringBuilder();
            menuSB.Append("<table border='0' cellspacing='1' cellpadding='0'>");
            string[] menu = null;
            string firstMenu = string.Empty;
            while (rdr.Read())
            {
                menu = rdr["csd_value"].ToString().Split(',');
                menuSB.Append("<tr><td>").Append("<a href='#' ");
                menuSB.Append("onClick=\"");
                if (menu[0].IndexOf("execute") >= 0)
                {
                    menuSB.Append(menu[0] + "\"");
                }
                else
                {
                    menuSB.Append("setPage('").Append(menu[0]).Append("')\"");
                }
                if (firstMenu.Equals(string.Empty))
                {
                    firstMenu = menu[0];
                }
                menuSB.Append(" class='image'>");
                menuSB.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Append(menu[1]).Append("</a></td></tr>");
            }
            rdr.Close();
            rdr.Dispose();
            userManagementDiv.InnerHtml = menuSB.Append("</table>").ToString();
            if (SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE") == "R")
                firstMenu = "";
            FooterScript.Text += "var firstMenu = '" + firstMenu + "';";
            //FooterScript.Text=menuSB.Append("</table>").ToString();
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            ltrlTitle.Text = ace.Ace_General.getApplicationTitle();
            //Show TipOfTheDay based on cookie "HIDEONLOGIN"
            if (!IsPostBack)
            {
                if (Session["BankCode"] != null && Session["BankCode"].ToString() == "F")
                {
                    OptDDAJSB.Visible = true;
                }
                else
                {
                    OptDDAJSB.Visible = false;
                }

            
                // new code start
             //   if (Session["s_USE_USERID"].ToString().ToUpper() == "ADMIN")

              //  {
               //     OptionDispatch.Visible = true;
                    //  OptionPolAlterRequest.Visible = true;
             //   }

                //else if (Session["s_USE_USERID"].ToString().ToUpper() == "SHAHMIR")
                //{
                //    Panel1.Visible = false;
                //    OptionIllustration.Visible = false;
                //    OptionProposProfile.Visible = false;
                //    OptionPolicyPrint.Visible = false;
                //    OptionInquiry.Visible = false;
                //    OptionFormPrint.Visible = false;
                //    OptionUnapprovPro.Visible = false;
                //    OptionUserGuide.Visible = false;
                //    _loggedBranchLabel.Visible = false;
                //    _loggedBranch.Visible = false;
                //    OptionDispatch.Visible = true;




                //}

              //  else
               // {
              //      OptionDispatch.Visible = false;
                    //  OptionPolAlterRequest.Visible = false;
              //  }


                // new code end
                designLeftMenuDiv();

                if (!this.isLeftMenuSettingExists)
                {
                    Server.Transfer("../Presentation/PersonalPage.aspx");
                }

                //Work only for Test Server if Its value is defined in Web.config
                lblTestVersion.Text = "";
                if (ace.Ace_General.IsTestVersion())
                {
                    lblTestVersion.Text = ace.Ace_General.getTestVersionInfo();
                }
                else
                {
                    lblTestVersion.Width = 0;
                    lblTestVersion.Height = 0;
                    lblTestVersion.Visible = false;
                }

                CSSLiteral.Text = ace.Ace_General.LoadGlobalStyle(); //ace.Ace_General.loadMainStyle();

                //ShowTip_Option();

                string user = Convert.ToString(Session["s_USE_USERID"]);

                if (user.ToUpper().StartsWith("RBH") || user.ToUpper().StartsWith("RSM") || user.ToUpper().StartsWith("WMO"))
                {
                    user += "-" + Session["s_USE_NAME"].ToString();
                }

                _loggedUser.Text = user;
                _loggedUser2.Text = user;
                //chg_closing
                //if (_loggedMonth != null && _loggedMonth2 != null)  //zl-11082023
                //_loggedMonth.Text = Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).ToString("MMMM") + "-" + Convert.ToString(Session["ClossingDate"]);
                //_loggedMonth2.Text = Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).ToString("MMMM") + "-" + Convert.ToString(Session["ClossingDate"]);
                //chg_closing_end

                string branch = SHAB.Data.CCS_CHANLSUBDETLDB.GetLogOnBranch();
                _loggedBranch.Text = branch;
                _loggedBranch2.Text = branch;

                if (ace.Ace_General.IsBancaasurance())
                    _loggedBranchLabel.Text = "Branch";
                else
                    _loggedBranchLabel.Text = "Agent";

                _loggedBranchLabel2.Text = _loggedBranchLabel.Text;
                userType.Text = "'" + ace.Ace_General.getUserType(user) + "'";
                string newValidation = "var newValidation='N';";
                if (ace.ValidationUtility.isNewValidation())
                {
                    newValidation = "var newValidation='Y';";
                }
                string assessCount = SHAB.Data.LCQD_QUESTIONSUBDETAIL.getQuestionChannelMapping(Convert.ToString(Session["s_CCH_CODE"]), Convert.ToString(Session["s_CCD_CODE"]), Convert.ToString(Session["s_CCS_CODE"]));
                if (Convert.ToInt32(assessCount) > 0)
                {
                    imgSuitabilityAssessment.Style.Add("display", "");
                    assessmentDivider.Style.Add("display", "");
                }
                else
                {
                    imgSuitabilityAssessment.Style.Add("display", "none");
                    assessmentDivider.Style.Add("display", "none");
                }
                //--****** LCUI_CLIENTUI (table) - Get columns list to be use to apply style *******--//
                string columnsStyle = ace.clsIlasUtility.getColumsStyle();

                //Get Tabs list to be hide
                string hiddenTabs = "var hiddenTabs='" + ace.clsIlasUtility.getHiddenTabs() + "';";

                //Get Buttons needs to be hide
                //TODO : Need to remove this logic as it is now avaliable in LCUI_CLIENTUI Setup
                string hiddenButtons = "var hiddenButtons='" + ace.clsIlasUtility.getHiddenButtons() + "';";

                //Get Buttons needs to be hide
                string RoundYearsDifference = "var ageRoundCriteria ='" + ace.clsIlasUtility.getAgeRoundingCriteria() + "';";

                string clientSpecificSetting = newValidation + columnsStyle + hiddenTabs + hiddenButtons + RoundYearsDifference;

                string ManualAdjustment = "var ManualAdjustment = 'N';";
                if (ace.clsIlasUtility.isManualAdjustmentAllowed())
                    ManualAdjustment = "var ManualAdjustment = 'Y';";

                //FooterScript.Text = SHMA.Enterprise.Shared.EnvHelper.Parse(clientSpecificSetting + "var userType=SV(\"s_USE_TYPE\"); if(userType=='A'){document.getElementById(\"adminTab\").style.visibility='visible';} else{document.getElementById(\"adminTab\").style.visibility='hidden';}if(userType=='A'||userType=='S'){document.getElementById(\"acceptanceTab\").style.visibility='visible';document.getElementById(\"acceptanceTab1\").style.visibility='visible';} else{document.getElementById(\"acceptanceTab\").style.visibility='hidden';}" + ManualAdjustment);
                FooterScript.Text += SHMA.Enterprise.Shared.EnvHelper.Parse(clientSpecificSetting + "var userType=SV(\"s_USE_TYPE\");" + ManualAdjustment);


                //TODO - Show/Hide tabs based on Application Type
                //FooterScript.Text += "";


            }

        }

        private void ShowTip_Option()
        {
            ShowTip.Text = "showTip();";
            HttpCookie cookie = Request.Cookies["HIDEONLOGIN"];

            if (cookie != null)
                if (cookie.Value == "1")
                    ShowTip.Text = "";
        }

        /*private void SetVersion()
		{
			object version = SHMA.Enterprise.Presentation.SessionObject.Get("s_Version");
			if (version != null)
			{
				if(version.ToString().Trim().Length > 0)
					lblVersion.Text = "Version:<br>" +  version.ToString();
			}
		}*/


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

            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    if (Session["s_USE_USERID"] == null)
                    {
                        Response.Redirect("LoginPage.aspx");
                    }
                }
            }

        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void imgLogout_Click(object sender, ImageClickEventArgs e)
        {
            //Session.Abandon();


            //if (IsPostBack)
            {


                Session["NP1_PROPOSAL"] = "";
                Session["POLICYID"] = "";
                Session["PageReloadTime"] = "";
                Session["s_CURR_SYSDATE"] = "";
                Session["DownloadCompleted"] = "";
                Session["s_USE_USERID"] = "";
                Session["s_CCH_CODE"] = "";
                Session["s_CCD_CODE"] = "";
                Session["s_CCS_CODE"] = "";
                Session["FLAG_RESET_PREMIUM"] = "";
                Session["NU1_ACCOUNTNO"] = "";
                Session["NPH_CODE"] = "";
                Session["NPH_CODE_s"] = "";
                Session["flg_SELECETD"] = "";
                Session["NPH_LIFE"] = "";
                Session["NPH_LIFE_s"] = "";
                Session["NU1_SMOKER"] = "";
                Session["s_COMM_DATE"] = "";
                Session["PPR_PRODCD"] = "";
                Session["s_USE_NAME"] = "";
                Session["opener"] = "";
                Session["s_CURR_SYSDATE"] = "";
                Session["paymenttype"] = "";
                Session["POSTED"] = "";
                Session["s_CCN_CTRYCD"] = "";
                Session["CCN_CTRYCD"] = "";
                Session["NP1_CHANNEL"] = "";
                Session["NP1_CHANNELDETAIL"] = "";
                Session["NPH_INSUREDTYPE"] = "";
                Session["NP1_PRODUCER"] = "";
                Session["NP1_TOTALANNUALPREM"] = "";
                Session["AAG_AGCODE"] = "";
                Session["RIDER_BENEFITTERM"] = "";
                Session["NPR_BENEFITTERM"] = "";
                Session["NPR_SUMASSURED"] = "";
                Session["CreationDate"] = "";
                Session["ProposalStatus"] = "";
                Session["ProposalValidity"] = "";
                Session["qlc_noofquestion"] = "";
                Session["qnc_noofquestion"] = "";
                Session["NP1_RETIREMENTAGE"] = "";
                Session["s_MenuAddress"] = "";
                Session["s_BAR_DATE"] = "";
                Session["s_SUS_NAME"] = "";
                Session["s_PLC_LOCADESC"] = "";
                Session["VALIDATION_ERROR"] = "";
                Session["BankCode"] = "";


                Session.Clear();
                Security.LogingUtility.Logout();
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Write("<script type='text/javascript'>");
                Response.Write("parent.document.location='../Presentation/LoginPage.aspx'");
                Response.Write("</script>");
            }
        }

    }
}
