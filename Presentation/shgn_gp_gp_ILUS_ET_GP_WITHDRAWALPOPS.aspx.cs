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
	/// Summary description for shgn_gp_gp_ILUS_ET_GP_WITHDRAWAL.
	/// </summary>
	public partial class shgn_gp_gp_ILUS_ET_GP_WITHDRAWALPOPS  : SHMA.CodeVision.Presentation.GroupBase
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
			//String loaded = "yes";
			/*if (Session["CCN_CTRYCD"]==null || Session["NP1_CHANNEL"]==null || Session["NP1_CHANNELDETAIL"]==null )
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_HOME', 'Please select values from home.');");
				Response.Write("</script>");
			}

			if (Session["NP1_PROPOSAL"]==null )
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL', 'Please select values from personal.');");
				Response.Write("</script>");
			}


			if (Session["PPR_PRODCD"]==null )
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PLANRIDER', 'Please select values from plan riders.');");
				Response.Write("</script>");
			}*/

		}


	}
}

