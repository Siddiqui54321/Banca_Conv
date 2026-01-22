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

namespace SHAB.Presentation
{
	/// <summary>
	/// Summary description for sideFrame.
	/// </summary>
	public partial class sideFrame : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*** Session Values For Testing (remove later) ****/
			Session.Add("ProposalNb","2007/0010");
			Session.Add("CreationDate","08/06/2007");
			Session.Add("ProposalStatus","<Proposal/Policy Status>");
			Session.Add("ProposalValidity","<Proposal validity date>");
			//Session.Add("SUS_USERCODE", "SHMA000");

			// Put user code to initialize the page here
			lblProposalNb.Text = (Session ["ProposalNb"]==null)?"":Session ["ProposalNb"].ToString();
			lblCreationDate.Text = (Session ["CreationDate"]==null)?"":Session ["CreationDate"].ToString();
			txtProposalStatus.Text = (Session ["ProposalStatus"]==null)?"":Session["ProposalStatus"].ToString();
			txtProposalValidity.Text = (Session ["ProposalValidity"]==null)?"":Session["ProposalValidity"].ToString();
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
	}
}
