using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Presentation;
using ace;
using System.Data.OleDb;


public partial class LoginPage : System.Web.UI.Page 
{
    /*
	protected System.Web.UI.WebControls.ImageButton imbForgotPassword;
	protected System.Web.UI.WebControls.ImageButton imbLoginButton;
	protected System.Web.UI.WebControls.Literal SecurityScript;
	protected System.Web.UI.WebControls.Image Image1;
	protected System.Web.UI.WebControls.Literal HeaderScript;
	private System.Text.StringBuilder jScript = new System.Text.StringBuilder();
	private int remainingDays = 0;
	private Security.SecurityParams secPara = null;
	 */
    
	protected System.Web.UI.WebControls.ImageButton imbForgotPassword;
	protected System.Web.UI.WebControls.ImageButton imbLoginButton;
	private System.Text.StringBuilder jScript = new System.Text.StringBuilder();
	private int remainingDays = 0;
	private Security.SecurityParams secPara = null;
    OleDbCommand CmdDML = new OleDbCommand(); // Imran work



    protected void Page_Load(object sender, EventArgs e)
	{
        
		//ltrlTitle.Text = ace.Ace_General.getApplicationTitle();
//		try
//		{
//			secPara = new Security.SecurityParams();
//		}
//		catch{}

		//txtUserCode.Text="DEMO";
		if (!IsPostBack)
		{
			HeaderScript.Text = "document.myForm.txtUserCode.focus();";
            //SHMA.Enterprise.Shared.EnvHelper.RefreshStateVariables();
            //Session.Abandon();
            //FormsAuthentication.SignOut();

            //LoadSecurityInformationInSession();
//chg_closing
            //if (ddlClosing != null)   //zl-11082023     //closing

            //    SetClosingSchdule();
//chg_closing_end
            //Work only for Test Server if Its value is defined in Web.config
            lblTestVersion.Text="";
			if(ace.Ace_General.IsTestVersion())
			{
				lblTestVersion.Text = ace.Ace_General.getTestVersionInfo() + "";
			}
			else
			{
				lblTestVersion.Width = 0;
				lblTestVersion.Height = 0;
				lblTestVersion.Visible = false;
			}
		}
		lblMsg.Text = string.Empty;

        
	}
//chg_closing
    //void SetClosingSchdule()
    //{
    //    DataTable dt = ace.Ace_General.Get_SystemParameters();
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlClosing.Items.Clear();
    //        ddlClosing.DataTextField = "MonthDate";
    //        ddlClosing.DataValueField = "CODE";
    //        ddlClosing.DataSource = dt;
    //        ddlClosing.DataBind();
    //    }
    //}
    //chg_closing_end

    private void LoadSecurityInformationInSession()
    {
        try
        {
            Security.SecurityUtility objSec = new Security.SecurityUtility();
            string[] secFile = objSec.ReadSecurityFile(Server.MapPath("Security.enc"));
            //string ConnString=secFile[0];
            string ExpiryDate = secFile[1];
            string Version = secFile[2];

            SessionObject.Set("s_ExpiryDate", ExpiryDate);


            SessionObject.Set("s_Version", Version);
            lblVersion.Text = "Version :   " + Version;

            SecurityScript.Text = "true";
        }
        catch (Exception e)
        {
            this.lblMsg.Text = e.Message;
            txtUserCode.Enabled = false;
            txtPassword.Enabled = false;
            SecurityScript.Text = "false";
        }

    }

    protected void imgForgotPassword_Click(object sender, ImageClickEventArgs e)
    {
    }

    protected void btnChangePwd_Click(object sender, System.EventArgs e)
    {
        try
        {
            secPara = new Security.SecurityParams();
            validatingChangePassword();
        }
        catch (Exception ex)
        {
            HeaderScript.Text = jScript.ToString();
            lblMsg.Text = ex.Message;
        }
    }
    private void validatingChangePassword()
    {
        ace.USE_USERMASTER useMastr = new ace.USE_USERMASTER();
        bool validPwd = useMastr.validatePassword(txtUserCode.Text, txtPassword.Text);
        if (validPwd)
        {

            //setMacIp();
            SessionObject.Set("s_USE_USERID", txtUserCode.Text);
            if (useMastr.validatingPasswordHistory(txtUserCode.Text, txtConfirmPwd.Text, secPara.getPasswordHistorySaved()))
            {
                txtPassword.Text = txtConfirmPwd.Text;
                validateLogin();
            }
            else
            {
                Response.Write("<script>alert('Changed Password Exists in History, Please change it again.');</script>");
                jScript.Append("showDiv();");
                throw new Exception("Changed Password Exists in History, Please change it again.");
            }
        }
        else
        {
            validatingAttempts();
        }
    }

    protected void imbLoginButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string Chk_Date = ace.ValidationClass.Validate_Date();
            string DB_Return = ace.ValidationClass.Validate_Date();
            string[] Chk_Date = DB_Return.Split(',');
            string sysdate = System.DateTime.Now.ToString("dd/MM/yyyy");

            if (Chk_Date[0].ToString() == "1")
            {
                secPara = new Security.SecurityParams();
                validatePwdExpiry();
                validateLogin();
                //chg_closing
                //if (ddlClosing.SelectedValue == "C")      
                //{
                //    Session["ClossingDate"] = DateTime.Now.ToString("dd/MM/yyyy");
                //}
                //else
                //{
                //    Session["ClossingDate"] = Convert.ToDateTime(ddlClosing.SelectedItem.Text.Split('-')[1].ToString()).ToString("dd/MM/yyyy");
                //}
                //Session["ClossingFlag"] = ddlClosing.SelectedValue;
                //if (Convert.ToString(Session["s_USE_TYPE"]) == "S" || Convert.ToString(Session["s_USE_TYPE"]) == "A")
                //{
                //    Ace_General.SetLoginSession(ddlClosing.SelectedValue, Convert.ToDateTime(ddlClosing.SelectedItem.Text.Split('-')[1].ToString()));
                //}
                //chg_closing_end
            }
            else
            {
                /*this.lblMsg.Text = "You cannot Login this time. Please try again Later. System Date: " + sysdate +
                    " Database Date: "+ Chk_Date[1].ToString(); */
                Response.Write("<script>alert('You cannot login to system. System Date: " + sysdate + " Database Date: "+ Chk_Date[1].ToString() + " Please contact System Administrator/GBA.');</script>");
                //this.lblMsg.Text = "You cannot login to system. System Date: " + sysdate + " Database Date: " +
                //    Chk_Date[1].ToString() + " Please contact System Administrator/GBA.";
            }
        }
        catch (Exception ex)
        {
            HeaderScript.Text = jScript.ToString();
            lblMsg.Text = ex.Message;
            Response.Write("<script>alert('"+ex.Message+ " Please Enter Correct User Id and Password.');</script>");
            txtPassword.Focus();  
        }
    }

    private void showAlertMessage(string message_)
    {
        Response.Write("<script>alert('" + message_ + "')</script>");
    }
    private void validatePwdExpiry()
    {
        ace.USE_USERMASTER useMastr = new ace.USE_USERMASTER();
        bool validPwd = useMastr.validatePassword(txtUserCode.Text, txtPassword.Text);
        if (validPwd)
        {
            if (Ace_General.BYPASS_PWD_CHECK(txtUserCode.Text.ToString().ToUpper()))
            {
                if (secPara.getPasswordExpiryDays() != 0)
                {
                    remainingDays = useMastr.getRemainingExpiryDays(txtUserCode.Text, secPara.getPasswordExpiryDays());
                    if (this.remainingDays < 0)
                    {
                        jScript.Append("showDiv();document.myForm.txtPassword.focus();");
                        throw new Exception("Your password is expired, Please change your password.");
                        Response.Write("<script>alert('Your password is expired, Please change your password')</script>");
                    }
                }
            }
        }
        else
        {
            validatingAttempts();
        }
    }
    private void deActivateUserAccount(string userID)
    {
        System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
        //conn.ConnectionString = SHMA.Enterprise.Data.DB.connStr;

        System.Data.OleDb.OleDbCommand mySqlCommand = conn.CreateCommand();
        try
        {
            conn.Open();
            mySqlCommand.CommandText = "update use_usermaster set use_active = 'N' where upper(use_userid) = '" + userID.ToUpper() + "'";
            mySqlCommand.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            this.lblMsg.Text = e.Message;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    private void validateLogin()
    {
        if (IsPostBack)
        {

            CheckBrowser();

            if (Session["BrowserAllow"].ToString() == "NO")

            {
                Response.Write("<script>alert('The application is not compatible with IE open with Edge or Chrome')</script>");

            }
            else
            {
              //  const string strUserQry = "Select USE_USERID, USE_NAME, AAG_AGCODE, USE_PASSWORD, CCH_CODEDEFAULT, USE_USERTYPE||USE_TYPE USE_TYPE, USE_ACTIVE from use_usermaster where UPPER(USE_USERID)=? "; 
            //Below query for add Email column for after sale portal
           
              const string strUserQry = "Select USE_USERID, USE_NAME, AAG_AGCODE, USE_PASSWORD, CCH_CODEDEFAULT, USE_USERTYPE, USE_TYPE , USE_ACTIVE,USER_EMAIL from use_usermaster where UPPER(USE_USERID)=? ";
           
                SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
                pm.puts("@UserId", txtUserCode.Text.ToUpper(), DbType.String);
                rowset rstUser = DB.executeQuery(strUserQry, pm);

                if (!rstUser.next())
                {
                    //this.lblMsg.Text = "Invalid username";
                    Response.Write("<script>alert('Invalid username')</script>");
                }
                else
                {
                    if (rstUser.getObject("USE_ACTIVE") == null || !rstUser.getString("USE_ACTIVE").Equals("Y"))
                    {
                        //this.lblMsg.Text = "User must be Active";
                        Response.Write("<script>alert('User must be Active')</script>");
                        Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, "Inactive User");
                    }
                    else
                    {
                        object expiryDate = SessionObject.Get("s_ExpiryDate");
                        string userType = rstUser.getString("USE_TYPE");
                        SessionObject.Set("s_USE_USERID", rstUser.getString("USE_USERID"));
                        SessionObject.Set("s_USE_NAME", rstUser.getString("USE_NAME"));
                        SessionObject.Set("s_CCH_CODEDEFAULT", rstUser.getString("CCH_CODEDEFAULT"));
                        SessionObject.Set("s_USE_TYPE", rstUser.getString("USE_TYPE"));
                        SessionObject.Set("s_AAG_AGCODE", rstUser.getString("AAG_AGCODE"));
                        SessionObject.Set("s_AAG_NAME", rstUser.getString("USE_NAME"));

                       SessionObject.Set("s_USE_USEREMAIL", rstUser.getString("USER_EMAIL"));

                        Session["UserType"] = rstUser.getString("USE_TYPE").ToString();

                      //  CheckBrowser();

                        //setMacIp(); //Set MAC & IP
                        rowset rsDefaultCountry = DB.executeQuery("select CCN_CTRYCD from LUCN_USERCOUNTRY where USE_USERID='" + rstUser.getString("USE_USERID") + "' AND UCN_DEFAULT='Y'");
                        if (!rsDefaultCountry.next())
                        {
                            //this.lblMsg.Text = "Default Country is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Country' Setup";
                            Response.Write("<script>alert('Default Country is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Country' Setup')</script>");

                            Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, "Default Country not defined");
                            return;
                        }
                        SessionObject.Set("s_CCN_CTRYCD", rsDefaultCountry.getString("CCN_CTRYCD"));
                        SessionObject.Set("s_PCM_COMPCODE", "01");
                        bool blnBranchSelection = false;
                        if (!userType.Equals("IA"))
                        {
                            rowset rsBranch = DB.executeQuery("SELECT CCH_CODE, CCD_CODE, CCS_CODE, UCH_DEFAULT  FROM LUCH_USERCHANNEL WHERE USE_USERID='" + rstUser.getString("USE_USERID") + "'");


                            if (rsBranch.size() <= 0)
                            {
                                //this.lblMsg.Text = "Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup";
                                Response.Write("<script>alert('Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup')</script>");
                                Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, "Channel is not defined");
                                return;
                            }
                            else
                            {

                                bool DefaultChannelFound = false;
                                while (rsBranch.next())
                                {
                                    if (rsBranch.getString("UCH_DEFAULT").ToUpper() == "Y")
                                    {
                                        DefaultChannelFound = true;
                                        SessionObject.Set("s_CCH_CODE", rsBranch.getString("CCH_CODE"));
                                        SessionObject.Set("s_CCD_CODE", rsBranch.getString("CCD_CODE"));
                                        SessionObject.Set("s_CCS_CODE", rsBranch.getString("CCS_CODE"));
                                        Session["BankCode"] = SessionObject.Get("s_CCD_CODE").ToString();

                                        //SessionObject.Set("RM_RSM",SHAB.Data.USE_USERMASTERDB.getRMAndRSM(string.Concat(rsBranch.getString("CCH_CODE"),rsBranch.getString("CCD_CODE"),rsBranch.getString("CCS_CODE"))));
                                    }
                                }
                                if (rsBranch.size() > 1)
                                {
                                    if (SessionObject.Get("s_CCD_CODE").ToString() == "9" && SessionObject.Get("s_CCH_CODE").ToString() == "2" && (txtUserCode.Text.ToString().ToUpper().StartsWith("RBH") || txtUserCode.Text.ToString().ToUpper().StartsWith("RSM")))
                                    {
                                        blnBranchSelection = false;
                                    }

                                else if(SessionObject.Get("s_CCD_CODE").ToString() == "F" && SessionObject.Get("s_CCH_CODE").ToString() == "2" && (txtUserCode.Text.ToString().ToUpper().StartsWith("GM")))

                                            {
                                        Session["BankCode"] = SessionObject.Get("s_CCD_CODE").ToString();
                                        blnBranchSelection = false;

                                             }

                                    else if (SessionObject.Get("s_CCD_CODE").ToString() == "F" && SessionObject.Get("s_CCH_CODE").ToString() == "2" && (txtUserCode.Text.ToString().ToUpper().StartsWith("WMO")))

                                    {
                                        Session["BankCode"] = SessionObject.Get("s_CCD_CODE").ToString();
                                        blnBranchSelection = false;

                                    }


                                    else
                                    {
                                        blnBranchSelection = true;
                                    }
                                }
                                if (DefaultChannelFound == false)
                                {
                                    //this.lblMsg.Text = "Default Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup";
                                    Response.Write("<script>alert('Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup')</script>");
                                    Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, "Default Channel is not defined");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            SessionObject.Set("s_CCH_CODE", "0");
                            SessionObject.Set("s_CCD_CODE", "0");
                            SessionObject.Set("s_CCS_CODE", "000");
                        }
                        DateTime dtCurrentDate = DateTime.Now;
                        SessionObject.Set("s_CURR_SYSDATE", DateTime.Now.ToString("dd/MM/yyyy"));
                        SessionObject.Set("s_COMM_DATE", ace.Ace_General.getGeneratedCommencementDate(dtCurrentDate).ToString("dd/MM/yyyy"));
                        SessionObject.Set("s_LOGINTIME", dtCurrentDate);
                        System.Web.Security.FormsAuthentication.SetAuthCookie("SafeMedLogIn", false);
                        Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, dtCurrentDate);
                        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.LOGIN);

                        /*************************** Mulitple Branches - Start *********************************/
                        string reDirectUrl = string.Empty;

                        //Need to remove two Levels BM (Compliance Level) and Collection File uploading (CP User File uploading only as we are doing in FWBL) 
                        //from UBL, BAL & FWBL 
                        //Adding new Bank Alfalah BM Role
                        //if ((SessionObject.GetString("s_CCD_CODE") == "1" || SessionObject.GetString("s_CCD_CODE") == "2" || SessionObject.GetString("s_CCD_CODE") == "5"
                        //    || SessionObject.GetString("s_CCD_CODE") == "6" || SessionObject.GetString("s_CCD_CODE") == "7" || SessionObject.GetString("s_CCD_CODE") == "8")
                        //    && userType == "M" && SessionObject.GetString("s_CCH_CODE") == "2")


                        if ((SessionObject.GetString("s_CCD_CODE") == "1" || SessionObject.GetString("s_CCD_CODE") == "2" || SessionObject.GetString("s_CCD_CODE") == "5" || SessionObject.GetString("s_CCD_CODE") == "3"
                        || SessionObject.GetString("s_CCD_CODE") == "6" || SessionObject.GetString("s_CCD_CODE") == "7" || SessionObject.GetString("s_CCD_CODE") == "8")
                        && userType == "M" && SessionObject.GetString("s_CCH_CODE") == "2")


                        {
                            reDirectUrl = "";
                        }

                      


                        else
                                {
                            if (blnBranchSelection == false)
                            {
                                reDirectUrl = "document.location='../Presentation/PersonalPage.aspx';";
                                //if(SessionObject.GetString("s_CCD_CODE") =="5")
                                //								if(userType=="S")
                                //							{
                                //								reDirectUrl = "document.location='../Presentation/ApplicationSelection.aspx';";
                                //							}
                                //							else
                                //							{
                                reDirectUrl = "document.location='../Presentation/PersonalPage_menue.aspx';";
                                //							}
                            }
                            else
                            {
                                reDirectUrl = "document.location='../Presentation/BranchSelection.aspx';";
                            }
                        }

                        if (Ace_General.BYPASS_PWD_CHECK(txtUserCode.Text.ToString().ToUpper()))
                        {
                            if (this.remainingDays != 0)
                            {
                                int confirmMsgDays = secPara.getMsgBeforepasswordDays();
                                if (this.remainingDays < confirmMsgDays)
                                {
                                    jScript.Append("var status =  confirm('Your password will expire in ( ");
                                    jScript.Append(remainingDays);
                                    jScript.Append(" ) Days, Do You want to change your password now?');");
                                    jScript.Append("if(!status)");
                                    jScript.Append("{");
                                    jScript.Append(reDirectUrl);
                                    jScript.Append("}");
                                    jScript.Append("else");
                                    jScript.Append("{");
                                    jScript.Append("	showDiv();");
                                    jScript.Append("}");
                                }
                            }
                        }
                        HeaderScript.Text = jScript.Length > 0 ? jScript.ToString() : reDirectUrl;
                    }
                }
            } // close for browser check 
        }
    }


    private void validatingAttempts()
    {
        if (Ace_General.BYPASS_PWD_CHECK(txtUserCode.Text.ToString().ToUpper()))
        {
            Security.LogingUtility.GenerateLoginLog(txtUserCode.Text, "Invalid User");
            if (ViewState["Attempts"] != null)
            {
                ViewState["Attempts"] = (int)ViewState["Attempts"] + 1;
            }
            else
            {
                ViewState["Attempts"] = 1;
            }
            int attempts = secPara.getPasswordAttemptAllowed(); //int.Parse(ace.ConfigSettings.getDecryptedEntry("NoOfAllowedAttempts"));
            if (Convert.ToInt32(ViewState["Attempts"]) > attempts)
            {
                deActivateUserAccount(txtUserCode.Text);
                throw new Exception("Your account has been locked due to exceeding attempts for login.");
                Response.Write("<script>alert('Your account has been locked due to exceeding attempts for login.')</script>");
            }
        }
        throw new Exception("Invalid Password.");
    }


    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }

    private void InitializeComponent()
    {

    }

    //	private DateTime getGeneratedCommencementDate(DateTime dtCurrentDate)
    //	{
    //		//**************************************************************************************//
    //		//******************* By default Current date is Commencement Date *********************//
    //		//**************************************************************************************//
    //		/*SHMA.Enterprise.Shared.EnvHelper env = new SHMA.Enterprise.Shared.EnvHelper();
    //		object objCurrentDate  = env.getAttribute("s_CURR_SYSDATE");
    //		DateTime dtCurrentDate = Convert.ToDateTime(objCurrentDate);
    //		if(objCurrentDate != null) dtCurrentDate = Convert.ToDateTime(objCurrentDate);*/
    //		DateTime dtCommDate = new DateTime(dtCurrentDate.Year,dtCurrentDate.Month, dtCurrentDate.Day);
    //
    //		
    //		//**************************************************************************************//
    //		//******************** Get alternate Commencement Date if needed ***********************//
    //		//**************************************************************************************//
    //		
    //		try
    //		{
    //			int currentDay = dtCurrentDate.Day;
    //			int maxComDate = ace.clsIlasUtility.getMaximumCommencementDate();
    //
    //			if(currentDay > maxComDate)
    //			{
    //				int alternateCommDate = ace.clsIlasUtility.getAlternateCommencementDate();
    //
    //				//***** If alternate comm date is b/w 0-10, thats mean next month date *************//
    //				if(alternateCommDate > 0 && alternateCommDate < 10)
    //				{	
    //						
    //					dtCommDate = dtCommDate.AddMonths(1);
    //				}
    //				dtCommDate = new DateTime(dtCommDate.Year, dtCommDate.Month, alternateCommDate);
    //			}
    //		}
    //		catch(Exception e)
    //		{
    //		}
    //		return dtCommDate;
    //	}
    //


    //	private DateTime getGeneratedCommencementDate(DateTime dtCurrentDate)
    //	{
    //		//**************************************************************************************//
    //		//******************* By default Current date is Commencement Date *********************//
    //		//**************************************************************************************//
    //		DateTime dtCommDate = new DateTime(dtCurrentDate.Year,dtCurrentDate.Month, dtCurrentDate.Day);
    //
    //		
    //		//**************************************************************************************//
    //		//******************** Get alternate Commencement Date if needed ***********************//
    //		//**************************************************************************************//
    //		DateTime CommencDate=dtCurrentDate;
    //		try
    //		{
    //			if(CommencDate.Day>28)
    //			{
    //				string ComDate="28/"+CommencDate.Month+"/"+CommencDate.Year;
    //				CommencDate=Convert.ToDateTime(ComDate);
    //			}
    //
    //		}
    //		catch(Exception ex)
    //		{
    //		
    //		}
    //         return dtCommDate;
    //	}
    //
    //	

    #region " Login Old"
    //	protected void imbLoginButton_Click(object sender, ImageClickEventArgs e)
    //	{
    //		if (IsPostBack)
    //		{
    ////			string strUserQry = "Select UserCode, Password, CompanyId, UserId, GroupId, lower(isnull(Status,'inactive')) Status from ShafiUser where UserCode='"+ txtUserCode.Text +"'";
    //			//string strUserQry = "Select USE_USERID, USE_PASSWORD, CCH_CODEDEFAULT, USE_TYPE from use_usermaster where USE_USERID IN (select USE_USERID from laag_agent where UPPER(USE_USERID)='" + txtUserCode.Text.ToUpper() + "')";
    //			string strUserQry = "Select USE_USERID, USE_NAME, AAG_AGCODE, USE_PASSWORD, CCH_CODEDEFAULT, USE_TYPE, USE_ACTIVE from use_usermaster where UPPER(USE_USERID)=? "; // "'" + txtUserCode.Text.ToUpper() + "'";
    //			SHMA.Enterprise.Data.ParameterCollection pm=new SHMA.Enterprise.Data.ParameterCollection();
    //			pm.puts("@UserId",txtUserCode.Text.ToUpper(),DbType.String);
    //			rowset rstUser = DB.executeQuery(strUserQry,pm);
    //
    //			if (!rstUser.next())
    //			{
    //				this.lblMsg.Text = "Invalid username";
    //				Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Invalid User");
    //			}
    //			else
    //			{
    //				if(rstUser.getObject("USE_ACTIVE") == null)
    //				{
    //					this.lblMsg.Text = "User must be Active";
    //					Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Inactive User");
    //				}
    //				else if(rstUser.getString("USE_ACTIVE") != "Y")
    //				{
    //					this.lblMsg.Text = "User must be Active";
    //					Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Inactive User");
    //				}
    //				else
    //				{
    //					//if ( rstUser.getString("USE_PASSWORD").Equals(txtPassword.Text))
    //					if (Security.UserOperation.validatePassword(txtPassword.Text, rstUser.getString("USE_PASSWORD")))
    //					{
    //						//SHMA.Enterprise.Shared.EnvHelper.RefreshStateVariables();
    //
    ///*						String strOtherUserInfo = "Select BrokerId, BrokerName from Broker Where UserID="+rstUser.getString("UserId");
    //						rowset rstOtherUserInfo = DB.executeQuery (strOtherUserInfo );
    //						if (rstOtherUserInfo.next())
    //						{
    //							Session.Add ("BrokerName", rstOtherUserInfo.getString("BrokerName"));
    //							Session.Add ("LoginBrokerId", rstOtherUserInfo.getString("BrokerId"));
    //							SHMA.Enterprise.Presentation.SessionObject.Set("LoginBrokerId", rstOtherUserInfo.getString("BrokerId"));
    //							SHMA.Enterprise.Presentation.SessionObject.Set("BrokerName", rstOtherUserInfo.getString("BrokerName"));
    //						}
    //*/
    ////						Session.Add("CompanyId", rstUser.getString("CompanyId"));
    ////						Session.Add("LoginUserID", rstUser.getString("UserId"));
    ////						Session.Add ("LoginGroupId", rstUser.getString("GroupId"));
    //
    //
    //
    //						object expiryDate = SessionObject.Get("s_ExpiryDate");
    //						//string s = (string) SessionObject.Get("s_ExpiryDate");
    //						//if(SessionObject.Get("s_ExpiryDate") == null)
    //						//if(expiryDate==null || expiryDate.ToString().Trim()=="")
    //						//{
    //						this.lblMsg.Text = "Expiration Date not found. May be Security.enc file is missing or incorrect.";
    //						//}
    //						//else
    //						//{
    //							//if(ace.Ace_General.isLoginExpired())
    //							//if(ace.Ace_General.isLoginExpired(expiryDate.ToString()))
    //							//{
    //							//	this.lblMsg.Text = "Application has been Expired";
    //							//}
    //
    //							//else
    //							//{
    //								//string strAgent = "select aag_agcode from laag_agent where UPPER(USE_USERID)='" + txtUserCode.Text.ToUpper() + "'";
    //								//rowset rstAgent = DB.executeQuery(strAgent);
    //								//if (rstAgent.next())
    //								//{
    //									//SessionObject.Set("s_AAG_AGCODE", rstAgent.getString("AAG_AGCODE"));
    //									SessionObject.Set("s_USE_USERID", rstUser.getString("USE_USERID"));
    //									SessionObject.Set("s_USE_NAME", rstUser.getString("USE_NAME"));
    //									SessionObject.Set("s_CCH_CODEDEFAULT", rstUser.getString("CCH_CODEDEFAULT"));
    //									SessionObject.Set("s_USE_TYPE", rstUser.getString("USE_TYPE"));
    //									SessionObject.Set("s_AAG_AGCODE", rstUser.getString("AAG_AGCODE"));
    //									SessionObject.Set("s_AAG_NAME", rstUser.getString("USE_NAME"));
    //								//}
    //								
    //								/***** Values set by Home Page - before *****/
    //								//Producer / AAG_NAME
    //								//rowset rsAAG_NAME = DB.executeQuery("SELECT AAG_NAME FROM LAAG_AGENT WHERE AAG_AGCODE='"+ rstAgent.getString("AAG_AGCODE") +"'");
    //								//if(!rsAAG_NAME.next())
    //								//{
    //								//	this.lblMsg.Text = "Producer is not defined not defined for code [" + rstAgent.getString("AAG_AGCODE") + "]" ;
    //								//	return;
    //								//}//SessionObject.Set("s_AAG_NAME", rsAAG_NAME.getString("AAG_NAME"));
    //
    //								//Default Country
    //								rowset rsDefaultCountry = DB.executeQuery("select CCN_CTRYCD from LUCN_USERCOUNTRY where USE_USERID='" + rstUser.getString("USE_USERID") + "' AND UCN_DEFAULT='Y'");
    //								if (!rsDefaultCountry.next())
    //								{
    //									this.lblMsg.Text = "Default Country is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Country' Setup" ;
    //									Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Default Country not defined");
    //									return;
    //								}SessionObject.Set("s_CCN_CTRYCD", rsDefaultCountry.getString("CCN_CTRYCD"));
    //								
    //
    //								//Company
    //								//rowset rsCompany = DB.executeQuery("SELECT PCM_COMPCODE FROM PCM_COMPANY where CCN_CTRYCD='" + rsDefaultCountry.getString("CCN_CTRYCD") + "'");
    //								rowset rsCompany = DB.executeQuery("SELECT PCM_COMPCODE FROM PCM_COMPANY");
    //								if (!rsCompany.next())
    //								{
    //									this.lblMsg.Text = "Company Setup not properly defined. (Might be Country Code is missing as per Logon user)" ;
    //									Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Compay Setup not properly defined");
    //									return;
    //								}SessionObject.Set("s_PCM_COMPCODE", rsCompany.getString("PCM_COMPCODE"));
    //
    //
    //
    //								/*************************** Mulitple Branches - Start *********************************/
    //								/***************| Pick all channels instead of only Default,|***************************/
    //								/***************| To support Multiple Branch Selection if,  |***************************/
    //								/***************| multiple branches are set for a user.     |***************************/
    //								
    //								//rowset rsDefaultChannel = DB.executeQuery("SELECT CCH_CODE, CCD_CODE, CCS_CODE FROM LUCH_USERCHANNEL WHERE USE_USERID='" + rstUser.getString("USE_USERID") + "' AND UCH_DEFAULT='Y'");
    //								//if (!rsDefaultChannel.next())
    //								//{
    //								//	this.lblMsg.Text = "Default Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup" ;
    //								//	return;
    //								//}
    //								//SessionObject.Set("s_CCH_CODE", rsDefaultChannel.getString("CCH_CODE"));
    //								//SessionObject.Set("s_CCD_CODE", rsDefaultChannel.getString("CCD_CODE"));
    //								//SessionObject.Set("s_CCS_CODE", rsDefaultChannel.getString("CCS_CODE"));
    //
    //								
    //								rowset rsBranch = DB.executeQuery("SELECT CCH_CODE, CCD_CODE, CCS_CODE, UCH_DEFAULT  FROM LUCH_USERCHANNEL WHERE USE_USERID='" + rstUser.getString("USE_USERID") + "'");
    //								
    //								bool blnBranchSelection = false;
    //								
    //								if (rsBranch.size()<=0)
    //								{
    //									this.lblMsg.Text = "Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup" ;
    //									Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Channel is not defined");
    //									return;
    //								}
    //								else
    //								{
    //									if (rsBranch.size()>1)
    //									{
    //										blnBranchSelection = true;
    //									}
    //									
    //									bool DefaultChannelFound = false;
    //									while (rsBranch.next())
    //									{
    //										if(rsBranch.getString("UCH_DEFAULT").ToUpper() == "Y")
    //										{
    //											DefaultChannelFound = true;
    //											SessionObject.Set("s_CCH_CODE", rsBranch.getString("CCH_CODE"));
    //											SessionObject.Set("s_CCD_CODE", rsBranch.getString("CCD_CODE"));
    //											SessionObject.Set("s_CCS_CODE", rsBranch.getString("CCS_CODE"));
    //										}
    //									}
    //									if(DefaultChannelFound == false)
    //									{
    //										this.lblMsg.Text = "Default Channel is not defined for User [" + rstUser.getString("USE_USERID") + "] in 'User Default Channel' Setup" ;
    //										Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Default Channel is not defined");
    //										return;
    //									}
    //								}
    //								/*************************** Mulitple Branches - End *********************************/
    //
    //
    //								/*
    //								//Default Channel Detail
    //								rowset rsChannelDetail = DB.executeQuery("SELECT CCD_CODE FROM CCD_CHANNELDETAIL WHERE CCH_CODE='" + rsDefaultChannel.getString("CCH_CODE") + "'");
    //								if (!rsChannelDetail.next())
    //								{
    //									this.lblMsg.Text = "Channel Detail is not defined for Chanel [" + rsDefaultChannel.getString("CCH_CODE") + "] in 'Channel Detail' Setup" ;
    //									return;
    //								}SessionObject.Set("NP1_CHANNELDETAIL", rsChannelDetail.getString("CCD_CODE"));
    //								*/
    //
    //
    //
    //								//SessionObject.Set("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue);
    //								//SessionObject.Set("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue);
    //								
    //								//SessionObject.Set("s_CURR_SYSDATE",dtCurrDate.ToString("dd/MM/yyyy"));
    //								DateTime dtCurrentDate = DateTime.Now;
    //								SessionObject.Set("s_CURR_SYSDATE",dtCurrentDate.ToString("dd/MM/yyyy"));
    //								SessionObject.Set("s_COMM_DATE",   getGeneratedCommencementDate(dtCurrentDate).ToString("dd/MM/yyyy"));
    //								SessionObject.Set("s_LOGINTIME",   dtCurrentDate);
    //
    //
    //
    //
    //								//SHMA.Enterprise.Presentation.SessionObject.Set("CompanyId", rstUser.getString("CompanyId"));
    //								//SHMA.Enterprise.Presentation.SessionObject.Set("LoginUserID", rstUser.getString("UserId"));
    //								//SHMA.Enterprise.Presentation.SessionObject.Set("LoginGroupId", rstUser.getString("GroupId"));
    //
    //								System.Web.Security.FormsAuthentication.SetAuthCookie("SafeMedLogIn",false);
    //
    //								//************* Login Log *************//
    //								Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,dtCurrentDate);
    //								Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.LOGIN);
    //
    //								/*************************** Mulitple Branches - Start *********************************/
    //								if(blnBranchSelection == false)
    //								{
    //									/*Response.Write("<script type='text/javascript'>");
    //									Response.Write("parent.document.location='../Presentation/PersonalPage.aspx'");
    //									Response.Write("</script>");*/
    //									Response.Redirect("../Presentation/PersonalPage.aspx");
    //								}
    //								else
    //								{
    //									/*Response.Write("<script type='text/javascript'>");
    //									Response.Write("parent.document.location='../Presentation/BranchSelection.aspx'");
    //									Response.Write("</script>");*/
    //									//Server.Transfer("BranchSelection.aspx");
    //									Response.Redirect("../Presentation/BranchSelection.aspx");
    //								}
    //								
    //								/*************************** Mulitple Branches - End *********************************/
    //
    //							//}
    //						//}
    //
    //					}
    //					else
    //					{
    //						this.lblMsg.Text = "Invalid password";
    //						Security.LogingUtility.GenerateLoginLog(txtUserCode.Text,"Wrong Password");
    //					}
    //				}
    //			}
    //		}
    //	}



    // Imran work for check browser on login time and save browser Name in table BROWSER_CHECK_LOG

    private OleDbConnection GetConn()
    {
        OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
        return conOra;


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

    void CheckBrowser()
    {

        string userAgent = Request.UserAgent.ToLower();
       // string Browser;
       

        if (userAgent.Contains("edg")) // Edge user agent usually contains "Edg"
        {
            // Edge browser detected
           // Browser = "Edge browser";
            Session["BrowserAllow"] = "YES";

        }
        else if (userAgent.Contains("chrome"))
        {
            // Chrome browser detected
           // Browser = "Chrome browser";
            Session["BrowserAllow"] = "YES";

        }
        else if (userAgent.Contains("firefox"))
        {
            // Firefox browser detected
           // Browser = "Firefox browser";
            Session["BrowserAllow"] = "NO";


        }
        else if (userAgent.Contains("safari"))
        {
            // Safari browser detected
           // Browser = "Safari browser";
            Session["BrowserAllow"] = "NO";

        }
        else if (userAgent.Contains("msie") || userAgent.Contains("trident"))
        {
            // Internet Explorer browser detected
           // Browser = "Internet Explorer browser";
            Session["BrowserAllow"] = "NO";

        }
        else
        {
            // Browser not recognized
           // Browser = "browser is not recognized";
            Session["BrowserAllow"] = "NO";

        }

        //string Sql = "INSERT INTO BROWSER_CHECK_LOG(USE_USERID,LOGIN_DATETIME,BROWSER_NAME) VALUES('" +
           // txtUserCode.Text + "',SYSDATE,'" + Browser + "')";

        //string Msg = DML(Sql);
        

    }
    #endregion
}
