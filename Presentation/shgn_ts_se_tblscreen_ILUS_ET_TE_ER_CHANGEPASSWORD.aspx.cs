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
using ace ;
using Aceins;
namespace SHAB.Presentation
{
	/// <summary>
	/// Summary description for shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD.
	/// </summary>
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TE_ER_CHANGEPASSWORD : System.Web.UI.Page
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			// Put user code to initialize the page here
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

		protected void btn_Save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		    //SessionObject.Set("USER_ID",USE_USERID);
			ace.Change_Password changepwd=new ace.Change_Password();
			bool chkpwd=changepwd.chkuserpwd(Session["s_USE_USERID"].ToString(),txtUSE_OLDPWD.Text);
			if(txtUSE_OLDPWD.Text==txtUSE_NEWPWD.Text)
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("alert('old password and new password are same')");
				Response.Write("</script>");
			
			}
			else
			{
				if(chkpwd==true)
				{
					//Update Password....
					try
                    {
                        //changepwd.updateUserPWD(Session["s_USE_USERID"].ToString(),Security.ACTIVITY.NONE);
						changepwd.updateuserpwd(Session["s_USE_USERID"].ToString(),txtUSE_NEWPWD.Text,Security.ACTIVITY.NONE);
						emptyvalues();
						Response.Write("<script type='text/javascript'>");
						Response.Write("alert('Password changed successfully.')");
						Response.Write("</script>");
					}
					catch(Exception ex)
					{
						Response.Write("<script type='text/javascript'>");
						Response.Write("alert('"+ex.Message+"')");
						Response.Write("</script>");
					}
				}
				else
				{
					Response.Write("<script type='text/javascript'>");
					Response.Write("alert('password does not match')");
					Response.Write("</script>");
				}
		
			}
		}

		//empty value...
		public void emptyvalues()
		{
		txtUSE_CNFRMPWD.Text="";
        txtUSE_NEWPWD.Text="";
		txtUSE_OLDPWD.Text="";
		
		}
		protected void btn_Cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
        emptyvalues();		
		}
	}
}
