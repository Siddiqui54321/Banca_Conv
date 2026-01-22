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
	/// Summary description for download_page.
	/// </summary>
	public partial class download_page : System.Web.UI.Page
	{
		private void showAlertMessage(string message_)
		{
			Response.Write("<script>alert('"+message_+"')</script>");
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string appName = System.Configuration.ConfigurationSettings.AppSettings["DownloadAppName"];
				Response.ContentType = "application/x-msdownload";
				Response.AppendHeader("Content-Disposition","attachment; filename="+appName);
				Response.WriteFile(Server.MapPath("~/downloads/"+appName));
				//Response.TransmitFile( Server.MapPath("~/images/googletalk-setup") );
				Response.End();
			}
			catch(Exception ex)
			{
				showAlertMessage(ex.Message);
				//Response.Write(ex.Message);
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
		}
		#endregion
	}
}
