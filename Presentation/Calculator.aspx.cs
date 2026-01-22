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
using System.Data.OracleClient;

using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;

namespace ACE.Presentation
{
	/// <summary>
	/// Summary description for POP_Calculator.
	/// </summary>
	public partial class Calculator : System.Web.UI.Page
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			cmd_Exit.Attributes.Add("onclick", "window.close();");
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

		protected void cmd_Calculate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{

			txtFaceAmount.Text = calculateFaceAmount(""+Session["NP1_PROPOSAL"], txtTargetPremium.Text); 
		
		}



		private string calculateFaceAmount(string proposal, string targetPremium)
		{
			Message.Text = "";
			string returnValue = "";
			try
			{
				ProcedureAdapter cs = new ProcedureAdapter ("CALCULATE_FACEAMOUNT_CALL");
				cs.Set("P_PROPOSAL",  System.Data.OleDb.OleDbType.VarChar, proposal);
				cs.Set("P_TARGETPREM",System.Data.OleDb.OleDbType.Numeric, targetPremium);
				cs.RegisetrOutParameter( "returnvalue", System.Data.OleDb.OleDbType.Numeric, 20);
				cs.Execute();
				returnValue  = ""+cs.Get("returnvalue");
			}
			catch (Exception e)
			{
				Message.Text = "Cannot calculate! please select a proposal, or enter correct value.";
			}
			return returnValue ;
		}
	}
}
