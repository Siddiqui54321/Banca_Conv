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
using SHAB.Business;
using SHMA.Enterprise;
using SHAB.Data; 

namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for WebForm2.
	/// </summary>
	public partial class WebForm2 : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string Proposal = Request.QueryString["prpno"].ToString();
			Get_Comments(Proposal);
			

		}
		private void Get_Comments(string Proposal)
		{
			
			//IDataReader temp = LNCM_COMMENTS.GetComments(Proposal);
			//DataHolder ds = new LNCM_COMMENTS( LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("Y",user,userType,bankCode,branchCode,true);
			DataHolder dataHolder = new DataHolder();

			this.dGrid.DataSource = getDataSource(dataHolder);	
			this.dGrid.DataBind();

		}

		private DataTable getDataSource(DataHolder dataHolder)
		{
			DataHolder ds = new DataHolder();
			string Proposal = Request.QueryString["prpno"].ToString();
			ds = new  LNCM_COMMENTSDB(dataHolder).getCommentsOfPorposal_New(Proposal);
			return ds["LNCM_COMMENTS_DATA"];
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
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
