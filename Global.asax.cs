using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Data;
using SHMA.Enterprise.Shared;
//using SHAB.Data;


using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;



namespace mi 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			/*if((System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"]!=null)
				&& (System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"]!="Session"))
				ApplicationObject.LoadGlobalApplicationVariables(true); 
			*/
			//if(!(Security.ValidateSoftware.validateKey())
			//	throw new ApplicationException("The software is not valid");
				
			ace.Ace_General.setApplicationName();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

			Exception ex = Server.GetLastError();
				if (ex != null && ex.Message.Contains("primary key"))
				{
					// Suppress or log the message
					Server.ClearError();
				}
			

		}

		protected void Session_End(Object sender, EventArgs e)
		{
			Security.LogingUtility.Logout();
			SHMA.Enterprise.Shared.EnvHelper.RefreshStateVariables();
            //Response.Redirect("~/Presentation/LoginPage.aspx");
			
		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

