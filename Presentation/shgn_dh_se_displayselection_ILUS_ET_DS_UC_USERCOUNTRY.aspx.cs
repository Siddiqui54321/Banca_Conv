using System;
using System.Collections;
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
	public partial class shgn_dh_se_displayselection_ILUS_ET_DS_UC_USERCOUNTRY : SHMA.Enterprise.Presentation.PageController
	{
		protected SHMA.Enterprise.Presentation.WebControls.ComboBox USE_USERID ;

		

		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;

		
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		private void InitializeComponent() {    
		}
		#endregion		

	#region Major methods of the final step      		
		sealed protected override DataHolder GetData(DataHolder dataHolder)
		{
			GetSessionValues();
			return   dataHolder;         
		}

		sealed protected override void DataBind(DataHolder dataHolder) {
			SaveTransaction = false;
			
			
			//IDataReader dr<entity-table> = SupplierDB.GetComboColumn_RO();
			//ddlSupplier.DataSource = drSupplier;
			//ddlSupplier.DataBind();
			//drSupplier.Close();
			AcquireSessionValues();

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;

			
			
		}
		void GetSessionValues(){						
		}
	#endregion	

		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message); 
			lblServerError.Text = message;
			message = base.ErrorHandle(message);
			return message;
			
		}
		void AcquireSessionValues(){
			USE_USERID .Text = SessionObject.Get("USE_USERID ")==null?"":SessionObject.GetString("USE_USERID ");

		}
		
		
	}
}

