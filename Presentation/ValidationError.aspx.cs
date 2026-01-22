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

namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for ValidationError.
	/// </summary>
	public partial class ValidationError : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ErrorSrc.Text = Request.QueryString["ErrorSource"].ToString();

			// Put user code to initialize the page here
			ErrorMessage.Text = Convert.ToString(Session["VALIDATION_ERROR"]);
			Session["VALIDATION_ERROR"] = "";
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

       // var page = "../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS.aspx
		
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
