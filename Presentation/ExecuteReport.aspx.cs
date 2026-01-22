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

using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

using SHAB.Data;
using SHMA.Enterprise;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for ExecuteReport.
	/// </summary>
	
	
	public partial class ExecuteReport : System.Web.UI.Page
	{

		protected System.Web.UI.WebControls.Literal FooterScript;
		



		public const string RPT_ILLUSTRATION = "ILLUSTRATION";
        public const string RPT_ILLUSTRATIONJOINT = "ILLUSTRATIONJOINT";
        public const string RPT_POLICY       = "POLICY";
		public const string RPT_HISTORY = "HISTORY";            //chg-his
		public const string RPT_STATUS = "STATUS";            //chg-his
		public const string RPT_ADVICE       = "ADVICE";
		public const string RPT_PROFILE      = "PROFILE";
        public const string RPT_PROFILEJOINT = "PROFILEJOINTLIFE";
        public const string RPT_PROPOSALINQ  = "PROPOSALINQ";
		public const string RPT_SECURITYLOG  = "SECURITYLOG";
		public const string RPT_PROPSUMMARY  = "PROP_SUMMARY";
        public const string RPT_DDAFORM = "DDAFORM"; // for JSB 17 page report PersonalDetailand Illustration
        public const string RPT_DDAJSB = "DDAJSB"; // for JSB 1 page new report only for JSB
        public const string RPT_PROFILEJS = "PROFILEJS"; //JSB profile


        public const string RPT_PDILLUS       = "PDILLUS";



		string strProposal  = "";
		
		string strReportType= "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			pnlCommonInput.Visible = false;
			pnlSecurityLog.Visible = false;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["_Proposal"]))
                {
                    Session["NP1_PROPOSAL"] = Request.QueryString["_Proposal"];
                }

               
            }




            this.strProposal  = Request.QueryString["_Proposal"].ToString();
			this.strReportType= Request.QueryString["_ReportType"].ToString().ToUpper();

            if ((strReportType == RPT_HISTORY) || (strReportType == RPT_STATUS)) //chg-his
			{                                                 

                //if (Session["Username"] != null)
                //{
                //    string username = (string)Session["Username"];
                //    // Use the username as needed
                //}

                //if (Session["NP1PROPOSAL"] != null)
                //    strProposal = (string)Session["NP1_PROPOSAL"];
                this.strProposal = System.Convert.ToString(Session["NP1_PROPOSAL"]);
				
				//string userType = ace.Ace_General.getUserType(user);
				//string aaa = System.Convert.ToString(Session["NP1_PROPOSAL"]);
			}


			Security.ACTIVITY reportActivity = Security.ACTIVITY.NONE;

			ReportHeading.Text = "Report";
			if(strReportType == RPT_ILLUSTRATION)
			{
				ReportHeading.Text = "Illustration";
				reportActivity = Security.ACTIVITY.ILLUSTRATION_PRINTED;
			}
            else if (strReportType == RPT_DDAFORM)
            {
                ReportHeading.Text = "DDAFORM Printing";
                reportActivity = Security.ACTIVITY.RPT_DDAFORM;
            }

            else if (strReportType == RPT_DDAJSB)
            {
                ReportHeading.Text = "DDAJSB Printing";
                reportActivity = Security.ACTIVITY.RPT_DDAJSB;
            }

            else if (strReportType == RPT_PROFILEJS)
            {
                ReportHeading.Text = "JSB PROFILE Printing";
                reportActivity = Security.ACTIVITY.RPT_PROFILEJS;
            }



            else if(strReportType == RPT_POLICY)
			{
				ReportHeading.Text = "Policy Printing";
				reportActivity = Security.ACTIVITY.POLICY_PRINTED;
			}
			else if (strReportType == RPT_HISTORY)  //CHG-HIS
			{
				ReportHeading.Text = "Payment History";
				reportActivity = Security.ACTIVITY.PAYMENT_HISTORY;				
			}
			else if (strReportType == RPT_STATUS)  //CHG-HIS
			{
				ReportHeading.Text = "Policy Status";
				reportActivity = Security.ACTIVITY.POLICY_STATUS;
			}
			else if(strReportType == RPT_ADVICE)
			{
				ReportHeading.Text = "Advice x";
				reportActivity = Security.ACTIVITY.ADVICE_PRINTED;
			}
			else if(strReportType == RPT_PROFILE || strReportType == RPT_PDILLUS)
			{
				Session["NP1_PROPOSAL"]=this.strProposal;
				ReportHeading.Text = "Profile Printing";
				reportActivity = Security.ACTIVITY.PERSONAL_PROFILE_PRINTED;
			}
			else if(strReportType == RPT_PROPOSALINQ)
			{
				Session["NP1_PROPOSAL"]=this.strProposal;
				ReportHeading.Text = "BSO Proposal Inquiry";
				reportActivity = Security.ACTIVITY.PROPOSAL_INQUIRY_PRINTED;
			}
			else if(strReportType == RPT_SECURITYLOG)
			{
				ReportHeading.Text = "Security Log";
			}
			else if(strReportType == RPT_PROPSUMMARY)
			{
				ReportHeading.Text = "Proposal Summary";
			}
			else
			{
				throw new Exception("Unknown Report Type");
			}


			try
			{	
				if(strReportType == RPT_PROPOSALINQ)
				{
					pnlCommonInput.Visible = true;
					if(!IsPostBack)
					{
						DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
						this.txtDATEFROM.Text = "01/" + sysDate.Month + "/" + sysDate.Year;
						this.txtDATETO.Text   = sysDate.Day + "/" + sysDate.Month + "/" + sysDate.Year;
					}
				}
				else if(strReportType == RPT_SECURITYLOG)
				{
					pnlCommonInput.Visible = true;
					pnlSecurityLog.Visible= true;
					SetChannelCombos();
					if(!IsPostBack)
					{
						DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
						DateTime dateFrom = sysDate.AddDays(-7);
						this.txtDATEFROM.Text = dateFrom.Day + "/" + dateFrom.Month + "/" + dateFrom.Year;
						this.txtDATETO.Text   = sysDate.Day  + "/" + sysDate.Month  + "/" + sysDate.Year;
					}
				}
				else
				{
					//************* Activity Log *************//
					Security.LogingUtility.GenerateActivityLog(reportActivity);

					//Get Report Information (Name and its Parameters)
					string[] arrReportInfo = getReportInfoFromSetup().Split(new char[]{'~'});
					string reportName = arrReportInfo[0];
					string moreReportParms = arrReportInfo[1];

					string ParamStr = "_q_cProposal," + strProposal + "," + strProposal + ";" + moreReportParms;
					string URL = "../CrystalReports/CrystalReport.aspx?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/" + reportName;
								
					Response.Redirect(URL, false);
									



				}
			}
			catch(Exception ex)
			{
				//Response.Write(ex.Message);
				ReportError.Text = ex.Message;
			}
		}

		private string getReportInfoFromSetup()
		{
			try
			{
                if (Convert.ToString(SessionObject.Get("NP1_JOINT"))=="Y" && this.strReportType=="PROFILE")
                {
                    this.strReportType = "PROFILEJOINTLIFE";
                }
                if (Convert.ToString(SessionObject.Get("NP1_JOINT")) == "Y" && this.strReportType == "ILLUSTRATION")
                {
                    this.strReportType = "ILLUSTRATIONJOINT";
                }
                ace.clsIlasReport objReport = new ace.clsIlasReport(this.strProposal,this.strReportType);
				return objReport.getReportInformation();
			}
			catch(Exception e)
			{
				throw new Exception("Error in getting Report." + e.Message);
			}
		}

		#region "Mandatory Checks"
		private bool validate()
		{
			if(strReportType == RPT_ILLUSTRATION)
			{
				return validateIllustration();
			}
			else if(strReportType == RPT_POLICY)
			{
				return validatePolicy();
			}
			else if(strReportType == RPT_ADVICE)
			{
				return validateAdvice();
			}
			else if(strReportType == RPT_PROFILE)
			{
				return validateProfile();
			}
            else if (strReportType == RPT_DDAFORM)
            {
                return validateProfile();
            }
            else if(strReportType == RPT_PDILLUS)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool validateIllustration()
		{
			checkPremium();
			return true;
		}

		private bool validatePolicy()
		{
			checkPremium();
			//**** Check either Policy Number is issued or not ****//
			rowset rs = DB.executeQuery("Select 'A' FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='"+ this.strProposal +"' AND NP1_POLICYNO IS NOT NULL ");
			if(rs.next() == false)
			{
				throw new Exception("Please complete Acceptance for this Proposal");
			}
			return true;
		}

		private bool validateAdvice()
		{
			checkPremium();
			return true;
		}

		private bool validateProfile()
		{
			checkPremium();
			//**** Check either Policy Number is issued or not ****//
			rowset rs = DB.executeQuery("Select 'A' FROM LNAD_ADDRESS WHERE NP1_PROPOSAL='"+ this.strProposal +"' and NAD_ADDRESSTYP='C' ");
			if(rs.next() == false)
			{
				throw new Exception("Please enter Information till Correspondence Address");
			}
			return true;
		}

		private void checkPremium()
		{
			string query = "SELECT NPR_PREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+ this.strProposal +"'  AND NPR_BASICFLAG='Y' AND NVL(NPR_PREMIUM,0) > 0 "; 
			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				if(rs.getObject("NPR_PREMIUM") == null)
				{
					throw new Exception("Please calculate Premium from Plan-Rider");
				}

				if(rs.getDouble("NPR_PREMIUM") == 0)
				{
					throw new Exception("Please calculate Premium from Plan-Rider");
				}

			}
			else
			{
				throw new Exception("Please calculate Premium from Plan-Rider");
			}		
		}
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

		}
		#endregion

		protected void btnGenerate_Click(object sender, System.EventArgs e)
		{
			string userId = Convert.ToString(Session["s_USE_USERID"]);

			string reportName = "";
			string ParamStr = ""; 

			if(pnlSecurityLog.Visible == true)
			{
				if(ddlREPORT.SelectedValue == "LOGINLOG")
				{
					reportName = "login_Access_log";

				}
				else if(ddlREPORT.SelectedValue == "ACTIVITYLOG")
				{
					reportName = "login_Act_log";

				}
				ParamStr += "_q_cSLL_DATE_FROM," + txtDATEFROM.Text + "," + txtDATEFROM.Text + ";";
				ParamStr += "_q_cSLL_DATE_TO,"   + txtDATETO.Text   + "," + txtDATETO.Text   + ";";

				ParamStr += "_q_cCCH_CODE," + ddlCCH_CODE_1.SelectedValue + "," + ddlCCH_CODE_1.SelectedValue + ";";
				ParamStr += "_q_cCCD_CODE," + ddlCCD_CODE_1.SelectedValue + "," + ddlCCD_CODE_1.SelectedValue + ";";
			}
			else
			{
				//************* Activity Log *************//
				Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_INQUIRY_PRINTED);
				reportName = "ProposalInquiry";

				ParamStr += "_q_cP_UserId,"   + userId           + "," + userId           + ";"; 
				ParamStr += "_q_cP_DateFrom," + txtDATEFROM.Text + "," + txtDATEFROM.Text + ";";
				ParamStr += "_q_cP_DateTo,"   + txtDATETO.Text   + "," + txtDATETO.Text   + ";";

			}

			string URL = "../CrystalReports/CrystalReport.aspx?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/" + reportName;
			Response.Redirect(URL, false);
		}
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
            try
            {
				string userId = Convert.ToString(Session["s_USE_USERID"]);
				string sqlString = "SELECT P1.USE_USERID,\n" +
				"       P1.NP1_CHANNEL,\n" +
				"       P1.NP1_CHANNELDETAIL,\n" +
				"       P1.NP1_CHANNELSDETAIL,\n" +
				"       ccs.ccs_descr BranchName,\n" +
				"       p1.np1_proposal,\n" +
				"       PH.NPH_CODE,\n" +
				"       ph.nph_fullname ClientName,\n" +
				"       LR.PPR_DESCR PlanDesc,\n" +
				"       CR.PCU_CURRDESC CurDesc,\n" +
				"       pr.npr_totprem,\n" +
				"       p1.np1_propdate,\n" +
				"case when np1_selected='Y' and  p1.np1_policyno is not null then 'Issued'\n" +
				"            when np1_selected='Y' and  p1.np1_policyno is null then 'Reffered'\n" +
				"            when np1_selected<>'Y' then 'Pending'\n" +
				"       end as pStatus  ,\n" +
				"case when np1_paymentmet is null then ''\n" +
				"           when np1_paymentmet='R' then 'Credit Card'\n" +
				"           when np1_paymentmet='B' then 'Bank Debit'\n" +
				"           end  as np1_paymentmet,\n" +
				"       p1.np1_ccnumber,\n" +
				"       p1.NP1_ACCOUNTNO,\n" +
				"       p1.np1_policyno,\n" +
				"       CM.CM_COMMENTBY,\n" +
				"       CM.CM_COMMENTS\n" +
				"  FROM LNP1_POLICYMASTR P1\n" +
				" INNER JOIN LNU1_UNDERWRITI u1\n" +
				"    on p1.np1_proposal = u1.np1_proposal\n" +
				" INNER join LNPH_PHOLDER ph\n" +
				"    on PH.NPH_CODE = U1.NPH_CODE\n" +
				"   AND PH.NPH_LIFE = U1.NPH_LIFE\n" +
				"   AND U1.NU1_LIFE = 'F'\n" +
				" INNER join LNPR_PRODUCT PR\n" +
				"    on pr.np1_proposal = p1.np1_proposal\n" +
				"   and pr.npr_basicflag = 'Y'\n" +
				"--      left outer join\n" +
				" INNER join luch_userchannel uch\n" +
				"    on uch.cch_code = p1.np1_channel\n" +
				"   and uch.ccd_code = p1.np1_channeldetail\n" +
				"   and uch.ccs_code = p1.np1_channelsdetail\n" +
				"   and uch.use_userid = P1.Use_Userid\n" +
				" INNER join CCS_CHANLSUBDETL ccs\n" +
				"    on ccs.cch_code = uch.cch_code\n" +
				"   and ccs.ccd_code = uch.ccd_code\n" +
				"   and ccs.ccs_code = uch.ccs_code\n" +
				" inner join LPPR_PRODUCT LR\n" +
				"    ON LR.PPR_PRODCD = PR.PPR_PRODCD\n" +
				" INNER join PCU_CURRENCY CR\n" +
				"    ON CR.PCU_CURRCODE = PR.PCU_CURRCODE\n" +
				" INNER JOIN LNCM_COMMENTS CM\n" +
				"    ON CM.NP1_PROPOSAL = P1.NP1_PROPOSAL\n" +
				"   AND CM.CM_SERIAL_NO =\n" +
				"       (SELECT MAX(CM_SERIAL_NO)\n" +
				"          FROM LNCM_COMMENTS\n" +
				"         WHERE NP1_PROPOSAL = P1.NP1_PROPOSAL)\n" +
				"\n" +
				" WHERE\n" +
				"--P1.USE_USERID='" + userId + "' and\n" +
				" P1.USE_USERID LIKE (CASE\n" +
				"   WHEN (SELECT USE_TYPE\n" +
				"           FROM USE_USERMASTER UM\n" +
				"          WHERE UM.USE_USERID = '"+ userId + "') = 'A' THEN\n" +
				"    '%'\n" +
				"   ELSE\n" +
				"    '"+ userId + "'\n" +
				" END)\n" +
				" and trunc(P1.NP1_PROPDATE) BETWEEN to_date('"+ txtDATEFROM.Text + "','dd/MM/yyyy') AND to_date('"+ txtDATETO.Text + "','dd/MM/yyyy')\n" +
				"\n" +
				" ORDER BY p1.use_userid,\n" +
				"          p1.np1_channel,\n" +
				"          p1.np1_channeldetail,\n" +
				"          p1.np1_channelsdetail,\n" +
				"          p1.np1_propdate";
				DataTable dt= DB.getDataTable(sqlString);
				WriteExcel(dt);
				SetDownloadLink();

			}
			catch (Exception ex)
            {
            }
		}
		public void SetDownloadLink()
		{
			string FileName = "UploadedFiles\\" + ViewState["FileName"].ToString();
			string downloadProposalFile = Server.MapPath(FileName);

			if (!File.Exists(downloadProposalFile))
			{
				//hlcrtl.Enabled = false;
				//hlcrtl.NavigateUrl = "#";
			}
			else
			{
				try
				{
					//hlcrtl.Enabled = true;
					//hlcrtl.NavigateUrl = downloadProposalFile;

					Response.Clear();
					Response.ContentType = "Application/.xls";
					Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
					//Response.TransmitFile(downloadProposalFile);
					Response.WriteFile(downloadProposalFile);
					Response.Flush();
					DeleteFile(downloadProposalFile);
					SessionObject.Set("DownloadCompleted", "True");
					Response.End();
				}
				catch (Exception)
				{

				}


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
				string portotypeFilePath = Server.MapPath(ConfigurationSettings.AppSettings["prototypeInquiryFilePath"]);
				string dfn = ConfigurationSettings.AppSettings["downloadProposalInquiry"];
				string[] FileName = dfn.Split('.');
				string fname = FileName[0].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"] + "\\" + fname);
				ViewState["FileName"] = fname;
				Session.Remove("downloadProposalInquiry");
				Session.Add("downloadProposalInquiry", downloadProposalFile);

				File.Copy(portotypeFilePath, downloadProposalFile, true);
				string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + downloadProposalFile + ";Extended Properties=" + "\"" + "Excel 8.0;HDR=YES;READONLY=FALSE" + "\"";
				OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
				string ExcelQuery = null;
				ExcelQuery = "SELECT * FROM [Sheet1$]";
				OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
				ExcelConnection.Open();
				//ExcelQuery = "UPDATE [Sheet1$] SET A='1', B='3', C='Update'";
				//ExcelQuery = "INSERT INTO [Sheet1$B5:B5] (F1) VALUES ('1')";
				foreach (DataRow dr in dt.Rows)
				{
					ExcelQuery = "INSERT INTO [Sheet1$] ([User Entry], [Location], [Proposal No],[Client Name], " +
						"[Plan], [Currency], [Total Premium], [Proposal Date], [Policy Status], [Payment Type], " +
						"[Payment Reference], [Policy No], [Commented By], [Comments]) " +
						"VALUES (" +
						"'" + dr[0].ToString() + "', '" + dr[4].ToString() + "', '" + dr[5].ToString() + "', '" + dr[7].ToString() + "', " +
						"'" + dr[8].ToString() + "', '" + dr[9].ToString() + "', '" + dr[10].ToString() + "', '" + dr[11].ToString() + "', " +
						"'" + dr[12].ToString() + "', '" + dr[13].ToString() + "', '" + dr[14].ToString() + "', '" + dr[15].ToString() + "', " +
						"'" + dr[17].ToString() + "', '" + dr[18].ToString() + "')";
					ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
					ExcelCommand.ExecuteNonQuery();
				}
				ExcelConnection.Close();

				//LNP1_POLICYMASTRDB.UpdateLastFileName(Path.GetFileName(downloadProposalFile));
			}
			catch (Exception ex)
			{
				StringBuilder errMessage = new StringBuilder();
				errMessage.Append(ex.Message);
			}
		}
		private void SetChannelCombos()
		{
			//New columns for Channel and Channel Detail columns
			//ddlCCH_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); Channel_ChangeEvent(this);");
			//ddlCCD_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); ChannelDetail_ChangeEvent(this);");
			ddlCCH_CODE_1.Attributes.Add("onchange" ,"Channel_ChangeEvent(this);");
			ddlCCD_CODE_1.Attributes.Add("onchange" ,"ChannelDetail_ChangeEvent(this);");
			
			IDataReader drCCH_CODE = CCH_CHANNELDB.GetDDL_CHANNELS();
			ddlCCH_CODE_1.DataSource = drCCH_CODE;
			ddlCCH_CODE_1.DataBind();
			drCCH_CODE.Close();

			if(ddlCCH_CODE_1.Items.Count > 0)
			{
				ddlCCH_CODE_1.SelectedIndex = 0;

			
				IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_CHANNELDETAIL(ddlCCH_CODE_1.SelectedValue) ;
				ddlCCD_CODE_1.DataSource = drCCD_CODE;
				ddlCCD_CODE_1.DataBind();
				drCCD_CODE.Close();

				if(ddlCCD_CODE_1.Items.Count > 0)
					ddlCCD_CODE_1.SelectedIndex = 0;

			}
			FooterScript.Text = "Channel_ChangeEvent(document.getElementById('ddlCCH_CODE_1'));";

		}
	}
}
