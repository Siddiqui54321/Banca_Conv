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
using System.Configuration;

namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for DownloadBancassuranceFile.
	/// </summary>
	public partial class DownloadBancassuranceFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		    WriteFileOnClient();
		}

		private void WriteFileOnClient()
		{
			try
			{
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+ConfigurationSettings.AppSettings["downloadBancaName"]);
				string dfn = ConfigurationSettings.AppSettings["downloadBancaName"];
				string attachment = "attachment; filename=downloadProposalBanca.xls";
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
				HttpContext.Current.Response.WriteFile(downloadProposalFile);
				HttpContext.Current.Response.AddHeader("Pragma", "public");
				HttpContext.Current.Response.Write(dfn.ToString());
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
			catch(Exception ex)
			{
				string a = ex.Message;
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
