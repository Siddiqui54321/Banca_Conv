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
using SHMA.Enterprise.Data;

namespace Aceins.Presentation
{
	/// <summary>
	/// Summary description for LCDT_DAILYTIP.
	/// </summary>
	public partial class LCDT_DAILYTIP : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton btnNextTip;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			setTipDescription();
			
			if(!IsPostBack)
			{
				setCheckBoxValue();
			}
		}

		private void setCheckBoxValue()
		{
			HttpCookie cookie = Request.Cookies["HIDEONLOGIN"];

			if(cookie != null)
				if(cookie.Value=="1")
					ChkNextTime.Checked=true;
		}

		private int MaxTipId()
		{
			int rtrnValue=0;
			rowset rs = DB.executeQuery("Select max(CDT_SERIAL) from LCDT_DAILYTIP");
			if(rs.next() && rs.getObject(1)!=null)
			{
				rtrnValue= rs.getInt(1);
			}
			return rtrnValue;

		}

		private int MinTipId()
		{
			int rtrnValue=0;
			rowset rs = DB.executeQuery("Select min(CDT_SERIAL) from LCDT_DAILYTIP");
			if(rs.next() && rs.getObject(1)!=null)
			{
				rtrnValue=rs.getInt(1);
			}
			return rtrnValue;
		}

		private string getTipDescription(int tipId)
		{
			string rtrnValue="";
			rowset rs = DB.executeQuery("Select CDT_DESCR  from LCDT_DAILYTIP where CDT_SERIAL="+tipId);
			if(rs.next() && rs.getObject(1)!=null)
			{
				rtrnValue=rs.getString(1);
			}
			return rtrnValue;
		}

		protected void btnNextTip_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			setTipDescription();
		}

		private void setTipDescription()
		{
			int tipId=0;
			HttpCookie cookie = Request.Cookies["LCDT_DAILYTIP"];
			if (cookie == null)
			{
				tipId= MinTipId();
				txtTip.Text = getTipDescription(tipId);
			}
			else
			{
				tipId = Convert.ToInt32(cookie["tipId"]);
				int maxTipId = MaxTipId();
				if(tipId>=maxTipId)
				{
					tipId=MinTipId();
				}
				else
					tipId++;
				txtTip.Text = getTipDescription(tipId);
			}

			if (cookie == null)
			{
				cookie = new HttpCookie("LCDT_DAILYTIP");
			}

			cookie["tipId"] = Convert.ToString(tipId);
			cookie.Expires = DateTime.Now.AddYears(1);
			Response.Cookies.Add(cookie);
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			setTipDescription();
		}

		protected void ChkNextTime_CheckedChanged(object sender, System.EventArgs e)
		{
			HttpCookie cookie = new HttpCookie("HIDEONLOGIN");
			
			if(ChkNextTime.Checked)
				cookie.Value="1";
			else
				cookie.Value="0";
			
			cookie.Expires = DateTime.Now.AddYears(1);
			Response.Cookies.Add(cookie);

		}

	}
}
