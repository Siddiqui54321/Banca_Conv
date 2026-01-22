using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;

using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;

namespace SHAB.Presentation{
	public partial class shgn_dh_se_displayheader_ILUS_DH_CHANNELSUBDTL : SHMA.Enterprise.Presentation.PageController
	{				
		protected string CCH_CODE;
protected string CN;
protected string CCD_CODE;
protected string CCD_DESCR;



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

#region Major methods of the final step      		
		protected override DataHolder GetData(DataHolder dataHolder) {		
			GetSessionValues();
			return dataHolder;
		}     
		protected override void ApplyDomainLogic(DataHolder dataHolder) 
		{		
			SaveTransaction = false;
		}
		
		sealed protected override void DataBind(DataHolder dataHolder) 
		{

			Page.DataBind();
		}
#endregion	

		private void GetSessionValues()
		{
			CCH_CODE=(string)SessionObject.Get("CCH_CODE");
CN=CCH_CHANNELDB.GetDESC_ILUS_DH_CHANNELSUBDTL_CN_RO();CCD_CODE=(string)SessionObject.Get("CCD_CODE");
CCD_DESCR=CCD_CHANNELDETAILDB.GetDESC_ILUS_DH_CHANNELSUBDTL_CCD_DESCR_RO();
			//orderID = SessionObject.Get("ID").ToString();
			//if (orderID==null)
			//	throw new SHAB.Shared.Exceptions.SessionValNotFoundException();
		}		
		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message);
			RegisterStartupScript("ClientMessage" , string.Format("<script language=javascript>alert(\"{0}\");</script>", message.Replace("\n","\\n")));return message;
		}
		
		public string GetRefreshInterval()
{ 
	 string rate = ""; 
	 return rate; 
} 

	}
}

