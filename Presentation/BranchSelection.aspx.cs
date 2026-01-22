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


public partial class BranchSelection : SHMA.Enterprise.Presentation.TwoStepController 
{
	protected System.Web.UI.WebControls.ImageButton imbLoginButton;
	protected System.Web.UI.WebControls.RequiredFieldValidator rfvCCS_CODE;
	protected System.Web.UI.WebControls.CompareValidator cfvCCS_CODE;
	protected System.Web.UI.WebControls.Literal FooterScript;

	/*protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
		}
	}*/

	sealed protected override void BindInputData(DataHolder dataHolder)
	{
		
		IDataReader LUCH_USERCHANNEL0 = SHAB.Data.LUCH_USERCHANNELDB.GetDDL_LUCH_USERCHANNEL_CCS_CODE_RO();;
		
		ddlCCS_CODE.DataSource = LUCH_USERCHANNEL0;
		ddlCCS_CODE.DataBind();
		LUCH_USERCHANNEL0.Close();

		//string DefaultChannel = Convert.ToString(SHAB.Data.LUCH_USERCHANNELDB.GetUserDefualtChannel()["CCS_CODE"]);
		IDataReader Channel = SHAB.Data.LUCH_USERCHANNELDB.GetUserDefualtChannel();
		Channel.Read();
		string DefaultChannel = Convert.ToString(Channel["CCS_CODE"]);
		Channel.Close();

		FooterScript.Text = EnvHelper.Parse("getField(\"CCS_CODE\").value='" + DefaultChannel  + "'; getField(\"CCS_CODE\").focus();") ; 
		ddlCCS_CODE.Attributes.Add("onkeypress","returnKeyPressed(event)");
	}

	protected void imbLoginButton_Click(object sender, ImageClickEventArgs e)
	{
		if (IsPostBack)
		{
			string channel = ddlCCS_CODE.SelectedValue;
			if(channel == null || channel == "")
			{
				Response.Write("<script language='javascript'>alert('Channel is not selected.');</script>"); 
				return;
			}
			else
			{
				string [] ChannelInfo = channel.Split('~');
				SessionObject.Set("s_CCH_CODE", ChannelInfo[0] );
				SessionObject.Set("s_CCD_CODE", ChannelInfo[1]);
				SessionObject.Set("s_CCS_CODE", ChannelInfo[2]);
				SessionObject.Set("s_ISMULTIBRANCH", "Y");

				Response.Write("<script type='text/javascript'>");

				Response.Write("document.location='../Presentation/ApplicationSelection.aspx'");

//				Response.Write("parent.document.location='../Presentation/PersonalPage_menue.aspx'");
				Response.Write("</script>");
				//Server.Transfer("PersonalPage.aspx");
			}
		}
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
