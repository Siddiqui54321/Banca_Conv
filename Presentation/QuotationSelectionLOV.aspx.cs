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

namespace Insurance.Presentation
{
	/// <summary>
	/// Summary description for ProposalSelectionLOV.
	/// </summary>
	public partial class QuotationSelectionLOV : System.Web.UI.Page
	{
		
		private bool SearchClicked = false;

		private const string PENDING  = "Pending";
		private const string POSTED   = "Posted";
		private const string REFERRED = "Referred";
		private const string APPROVED = "Approved";
		private const string DECLINED = "Declined";
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//******** Application Oriented Logic ****************//
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			//******** Application Oriented Logic - End ****************//

			if(!IsPostBack)
			{
				if(Request.QueryString["SrcScreen"] != null)
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
			if ( src.Equals("Select") )
			{
				string proposalid = e.Item.Cells[1].Text;
				string prodcd = ace.Ace_General.getPPR_PRODCD(proposalid);

				if(ViewState["SrcSreen"] == null)
				{
					Session.Add("NP1_PROPOSAL", proposalid);

					if (prodcd!=null)
					{
						Session.Add("PPR_PRODCD", prodcd);
						Session.Add("QUOTATION_SELECTED", "Y");
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
					//UpdateCommencmentDate(proposalid);

					//************* Activity Log *************//
					Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_SELECTED);
				}
				else
				{
					if (prodcd!=null)
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
		}

		private void UpdateCommencmentDate(string proposal)
		{
			try
			{
				string userId  = Convert.ToString(Session["s_USE_USERID"]).ToUpper();
				proposal = proposal.ToUpper();

				//******* If User is Admin Type then return *********//
				rowset rs = DB.executeQuery("select 'A' from USE_USERMASTER WHERE UPPER(USE_USERID)='"+userId+"' AND USE_TYPE='A' ");
				if(rs.next())
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
					if(rs.next())
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

						SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal,commDate);
						//SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDateByProposalDate(Convert.ToString(Session["NP1_PROPOSAL"]));					
	
					}
				}				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);	
			}		
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			if(IsPostBack)
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

			if(txtSearchEvent.Text == "1" || txtSearchEvent.Text == "2" || txtSearchEvent.Text == "3")
			{
				if(SearchClicked == false)
				{
					ddlsearch.SelectedIndex = Convert.ToInt16(txtSearchEvent.Text.Trim()=="" ? "0" : txtSearchEvent.Text);
				}

				if(ddlsearch.SelectedIndex == 1)//Proposal Search
				{
					SearchCriteria = " AND UPPER(NP1_PROPOSAL) LIKE UPPER('"+txtsearch.Text+"')";
				}
				else if(ddlsearch.SelectedIndex == 2)//Name Search
				{
					SearchCriteria = " AND UPPER(NPH_FULLNAME) LIKE UPPER('"+txtsearch.Text+"') ";
				}
				else if(ddlsearch.SelectedIndex == 3)//NIC Search
				{
					SearchCriteria = "  and (NPH_IDNO) LIKE '"+txtsearch.Text+"'";
				}

			}

//			if(ddlStatus.SelectedValue == "O")//OPEN/PENDING
//			{
//				SearchCriteria += " AND LNP1.STATUS='"+ PENDING +"' ";
//			}
//			else if(ddlStatus.SelectedValue == "P")
//			{
//				SearchCriteria += " AND LNP1.STATUS='"+ POSTED +"' ";
//			}
//			else if(ddlStatus.SelectedValue == "A")
//			{
//				SearchCriteria += " AND LNP1.STATUS='"+ APPROVED +"' ";
//			}
//			else if(ddlStatus.SelectedValue == "R")
//			{
//				//SearchCriteria += " AND LNP1.STATUS='"+ REFERRED +"' ";
//				SearchCriteria += " AND (LNP1.STATUS='"+ REFERRED +"' OR LNP1.STATUS='" + DECLINED + "') ";
//			}
			//else if(ddlStatus.SelectedValue == "D")
			//{
			//	SearchCriteria += " AND LNP1.STATUS='"+ DECLINED +"' ";
			//}
			
			/******* Preparing Query to pick Personal Information now ********/
			string dsn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			OleDbConnection cnn = new OleDbConnection( dsn );
			string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);
			
			string ProposalStatus ="";
//			ProposalStatus = " CASE "
//								  + "   WHEN NP1_SELECTED = 'Y' and (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B WHERE B.NP1_PROPOSAL = A.NP1_PROPOSAL) = 'Y' THEN '" + REFERRED + "' "
//								  + "   WHEN NP1_SELECTED = 'Y'   THEN '" + POSTED   + "' "
//								  + "   WHEN CDC_CODE     = '001' THEN '" + APPROVED + "' "
//								  + "   WHEN CDC_CODE     = '002' THEN '" + REFERRED + "' "
//								  + "   WHEN CDC_CODE     = '003' THEN '" + REFERRED + "' "
//								  + "   WHEN (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B WHERE B.NP1_PROPOSAL = A.NP1_PROPOSAL) = 'Y' THEN '" + REFERRED + "' "
//								  + "   ELSE '" + PENDING + "' "
//								  + " END STATUS  ";

			string query="";

			if(ViewState["SrcSreen"] == null)
			{
//				if(userType=="S")
//				{
//					query =" SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
//						+  " FROM "
//						+  "     LNPH_PHOLDER, "
//						+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD='"+Session["s_CCN_CTRYCD"]+"' AND NP1_CHANNEL='"+Session["s_CCH_CODE"]+"' AND NP1_CHANNELDETAIL='"+Session["s_CCD_CODE"]+"' AND NP1_CHANNELSDETAIL='"+Session["s_CCS_CODE"]+"' ) LNP1, "
//						+  "     LCCN_COUNTRY CCN "
//						+  " WHERE  "
//						+  "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code in (SELECT ccd_code FROM luch_userchannel d WHERE m.CCH_CODE = D.CCH_CODE and m.ccd_code = d.ccd_code)) "
//						+  "  AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
//						+  "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
//						+  "  AND LNP1.NP1_SELECTED IS NULL AND LNP1.USE_USERID='"  + user + "'"
//						+ SearchCriteria
//						+  " order by 1 ";
//				}
//				else
//				{
//					query =  " SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
//						+  " FROM "
//						+  "     LNPH_PHOLDER, "
//						+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, " + ProposalStatus + ", NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_CHANNEL=SV(\"s_CCH_CODE\") AND NP1_CHANNELDETAIL=SV(\"s_CCD_CODE\") AND NP1_CHANNELSDETAIL=SV(\"s_CCS_CODE\") AND USE_USERID=SV(\"s_USE_USERID\") ) LNP1,  "
//						+  "     LCCN_COUNTRY CCN "
//						+  " WHERE  "
//						+  "      LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
//						+  "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
//						+  "  AND LNP1.NP1_SELECTED IS NULL " 
//						+ SearchCriteria
//						+  " order by 1 ";

					query= "SELECT LNP1.NP1_PROPOSAL, NPH_FULLNAME,NPH_IDNO "
						+  "	FROM LNPH_PHOLDER,"
						+  "	(select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, NP1_SELECTED from lnp1_policymastr A where substr(NP1_PROPOSAL, 0, 3) = 'ILS' AND USE_USERID=SV(\"s_USE_USERID\")) LNP1"
						+  "	WHERE "
						+  "	(NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')"
						+  "	AND LNP1.NP1_SELECTED IS NULL order by 1";

//				}
			}
			else
			{
				string allowedPlansList = ace.clsIlasUtility.getListFromSysDetail("APPBH","MANADJPLANS", false);
				if(Convert.ToString(ViewState["SrcSreen"]).Equals("MANADJUSTMENT"))
				{
					query =" SELECT LNP1.NP1_PROPOSAL, STATUS, NPH_FULLNAME, NPH_IDNO, CCN_DESCR "
						+  " FROM "
						+  "     LNPH_PHOLDER, "
						+  "     (select NP1_PROPOSAL, CCN_CTRYCD, USE_USERID, 'Pending' STATUS, NP1_SELECTED from lnp1_policymastr A where CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND NP1_SELECTED IS NULL AND CDC_CODE IS NULL  "
						+  "         AND EXISTS (SELECT 'A' FROM LNPR_PRODUCT LNPR WHERE LNPR.NP1_PROPOSAL=A.NP1_PROPOSAL AND LNPR.PPR_PRODCD IN " + allowedPlansList + ") ) LNP1, "
						+  "     LCCN_COUNTRY CCN "
						+  " WHERE  "
						+  "     LNP1.USE_USERID IN (SELECT USE_USERID FROM luch_userchannel m WHERE m.ccd_code = (SELECT ccd_code FROM luch_userchannel d WHERE USE_USERID = '"  + user + "'"
						+  "     and m.CCH_CODE=D.CCH_CODE and m.ccd_code=d.ccd_code )) "
						+  "     AND LNP1.CCN_CTRYCD=CCN.CCN_CTRYCD AND "
						+  "     (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = LNP1.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F') "
						+  " order by 1 ";
				}
			}

			/******** Executing Query ************/
			query = EnvHelper.Parse(query);
			OleDbDataAdapter da = new OleDbDataAdapter(query, cnn);
			DataSet ds = new DataSet(); 
			da.Fill(ds, "lnp1_policymastr"); 
			dgProposalLOV.DataSource = ds.Tables["lnp1_policymastr"];
			dgProposalLOV.DataBind();

			if(ViewState["SrcSreen"] == null )
			{
				SetControlValues();
			}
			else
			{
//				ddlStatus.Visible = false;
				ddlsearch.Visible = false;
				txtsearch.Visible = false;
				Button1.Visible = false;
			}
		}


		private void SetControlValues()
		{
			if(txtSearchEvent.Text == "1")
			{
				ddlsearch.SelectedIndex = 1;
			}
			else if(txtSearchEvent.Text == "2")
			{
				ddlsearch.SelectedIndex = 2;
			}
			else if(txtSearchEvent.Text == "3")
			{
				ddlsearch.SelectedIndex = 3;
			}
			else
			{
				ddlsearch.SelectedIndex = 0;
				txtsearch.Text="";
			}
		}
	}
}
