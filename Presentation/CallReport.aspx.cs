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

namespace Insurance.Presentation
{
	/// <summary>
	/// Summary description for testReport.
	/// </summary>
	public partial class CallReport : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.CheckBox chkApplication;
		protected System.Web.UI.WebControls.CheckBox chkProposal;
		protected System.Web.UI.WebControls.CheckBox chkPolicy;
		protected System.Web.UI.WebControls.Label lblCreationDate;
		protected System.Web.UI.WebControls.Label lblIssueDate;
		protected System.Web.UI.WebControls.CheckBox chkCardNumber;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.TextBox txtAccountNumber;

		private string POLICYID ;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*POLICYID = ((Session["POLICYID"]==null||Session["POLICYID"].ToString().Equals(""))?"NULL":Session["POLICYID"].ToString());
			string strPolicyData = "Select ProposalNb, PolicyNb, ProposalDate, PolicyIssueDate from POLICY WHERE POLICYID = ISNULL ("+ POLICYID +",0)";
			rowset rstPolicyData = DB.executeQuery ( strPolicyData );
			if (rstPolicyData.next())
			{
				chkApplication.Text = "Application: " + (rstPolicyData.getObject("ProposalNb")== null ? "": rstPolicyData.getObject("ProposalNb").ToString());
				chkProposal.Text = "Proposal: "+ (rstPolicyData.getObject("ProposalNb")== null ? "": rstPolicyData.getObject("ProposalNb").ToString());
				chkPolicy.Text = "Policy: "+ (rstPolicyData.getObject("PolicyNb")== null ? "": rstPolicyData.getObject("PolicyNb").ToString());
				lblCreationDate.Text = "Date: "+(rstPolicyData.getObject("ProposalDate")== null ? "": (DateTime.Parse( rstPolicyData.getObject("ProposalDate").ToString())).ToShortDateString());
				lblIssueDate.Text= "Issue Date: "+(rstPolicyData.getObject("PolicyIssueDate")== null ? "": (DateTime.Parse( rstPolicyData.getObject("PolicyIssueDate").ToString())).ToShortDateString());
			}
			rstPolicyData.close();
			//ClearAdherentList();*/
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

		}
		#endregion

/*
		private void chkCardNumber_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkCardNumber.Checked)
			{
				FillAdherentList();
			}
			else
			{
				ClearAdherentList();
			}
		}
*/
	}
}
