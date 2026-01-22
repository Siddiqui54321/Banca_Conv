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
	/// Summary description for shgn_gp_gp_ILUS_ET_GP_PERONAL.
	/// </summary>
	public partial class shgn_gp_gp_ILUS_ET_GP_PERONAL  : SHMA.CodeVision.Presentation.GroupBase
	{

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
		protected override void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				//if (Session["s_USE_USERID"].ToString().ToUpper() == "SHAHMIR")
			//	{
			//		PnlEntryForm.Visible = false;
				//}
			}


			//String loaded = "yes";
			/*if (Session["CCN_CTRYCD"]==null ) || Session["NP1_CHANNEL"]==null || Session["NP1_CHANNELDETAIL"]==null )
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_HOME', 'Please select values from home.');");
				Response.Write("</script>");
			}*/

			//CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
		}




	}


}

