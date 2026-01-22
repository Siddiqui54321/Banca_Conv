using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Shared;


public partial class ApplicationSelection : SHMA.Enterprise.Presentation.TwoStepController 
{
	protected System.Web.UI.WebControls.RequiredFieldValidator rfvCCS_CODE;
	protected System.Web.UI.WebControls.CompareValidator cfvCCS_CODE;
	protected System.Web.UI.WebControls.Literal FooterScript;

	protected void ImagebuttonBanca_Click(object sender, ImageClickEventArgs e)
	{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.document.location='../Presentation/PersonalPage_menue.aspx'");
				Response.Write("</script>");
	}

	protected void ImagebuttonIllus_Click(object sender, ImageClickEventArgs e)
	{
		Response.Write("<script type='text/javascript'>");
		Response.Write("parent.document.location='../Presentation/GenerateIllustration2.aspx'");
		Response.Write("</script>");
	}

	override protected void OnInit(EventArgs e) 
	{
		InitializeComponent();
		base.OnInit(e);

		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
		Response.Cache.SetNoStore();
	}
		
	private void InitializeComponent()
	{

	}

}
