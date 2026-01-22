using System;
using System.Collections;
using System.ComponentModel;
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

namespace SHAB.Presentation
{
	public partial class shgn_dh_se_enquiry_ILUS_ET_EQ_TARGETVALUES : SHMA.Enterprise.Presentation.TwoStepController{
		//controls
		protected System.Web.UI.WebControls.LinkButton lnkCode;	
		protected System.Web.UI.HtmlControls.HtmlForm myForm;
		private int pageNumber=1;
		private int recordCount=0;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal _lastEvent;
	

		
		int PAGE_SIZE= SHMA.Enterprise.Configuration.AppSettings.GetInt("NoOfListerRows") ;

		//protected NameValueCollection columnPageTotal = new NameValueCollection( (float)0, "orga_code", "orga_code" );
		//protected NameValueCollection columnGrandTotal = new NameValueCollection( (float)0, "orga_code", "orga_code" );

		protected NameValueCollection columnPageTotal =new NameValueCollection( (Decimal)0); 
		protected NameValueCollection columnGrandTotal=new NameValueCollection( (Decimal)0);
				
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		private void InitializeComponent() 
		{    
			this.pagerList.SelectedIndexChanged += new System.EventHandler(this.pagerList_SelectedIndexChanged);

		}
		#endregion		

		#region Major methods of First Step
		protected override void ValidateParams() {
			base.ValidateParams ();			
			string[] param;
			foreach (string key in Request.Params.AllKeys){				
				if (key!=null && key.StartsWith("r_")){
					param = Request[key].Split(',');
					SessionObject.Set(key.Replace("r_",""), param[param.Length-1]); 					
				}
			}
		}

		sealed protected override DataHolder GetInputData(DataHolder dataHolder)
		{				
			GetSessionValues();			
			return   dataHolder;        

		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			DataTable table = new DataTable("LNLO_LOADING");
			IDataReader LNLO_LOADINGReader = LNLO_LOADINGDB.GetILUS_ET_EQ_TARGETVALUES_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			//recordCount = Utilities.Reader2Table(LNLO_LOADINGReader, table, PAGE_SIZE, pageNumber);
			//recordCount = Utilities.Reader2Table ( BN_MS_BM_BANKMASTERReader, table, PAGE_SIZE, pageNumber, columnPageTotal, columnGrandTotal );
			
			recordCount = Utilities.Reader2Table(LNLO_LOADINGReader, table, PAGE_SIZE, pageNumber);

			LNLO_LOADINGReader.Close();
			BindLister(table);			

			HeaderScript.Text = EnvHelper.Parse(" var _sessionEnqVarList='NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,NLO_TYPE,NLO_SUBTYPE,CAD_DESCRIPTION,NLO_AMOUNT,NLO_AMOUNT_LN2,NLO_AMOUNT_LN3';") ;
			FooterScript.Text = EnvHelper.Parse("") ;
			_lastEvent.Text = "New";
		}
		#endregion

		#region Major methods of the final step
		sealed protected override void DataBind(DataHolder dataHolder) 
		{
			IDataReader LNLO_LOADINGReader;
			DataTable table = new DataTable("LNLO_LOADING") ;
			LNLO_LOADINGReader = LNLO_LOADINGDB.GetILUS_ET_EQ_TARGETVALUES_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);				
			//recordCount = Utilities.Reader2Table(LNLO_LOADINGReader, table, PAGE_SIZE, pageNumber);
			recordCount = Utilities.Reader2Table(LNLO_LOADINGReader, table, PAGE_SIZE, pageNumber);
			LNLO_LOADINGReader.Close();

			BindLister(table);

			_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();

		}
	#endregion	
    
		#region Events
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) {
			pageNumber = pagerList.SelectedIndex + 1;
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Pager;
			DoControl();
			pagerList.SelectedIndex = pageNumber -1;
		
		}
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) {
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value){
				case "Edit" :
					string[] fieldComb = _CustomArgName.Value.Split(',');
					string[] valueComb = _CustomArgVal.Value.Split(',');
					for (int i = 0; i<fieldComb.Length; i++){
						SessionObject.Set(fieldComb[i], valueComb[i]);
					}									
				break;
			}
		}
		#endregion 

		private void GetSessionValues()
		{

			
//			if (SessionObject.Get("org_code") == null)
//				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
		}		
		private void BindLister(DataTable table){
			lister.DataSource = table;
			lister.DataBind();			
			pagerList.Items.Clear();
			for (int i=1;recordCount>0; recordCount-=PAGE_SIZE){				
				pagerList.Items.Add(i.ToString());		
				i++;
			}
		}

		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message);
			PrintMessage(message);
			return message;
		}

		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}

		private void lister_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem){
			
			HtmlTableRow tRow = (HtmlTableRow)e.Item.FindControl("EnqRow");
			tRow.Attributes.Add("onclick", string.Format("setEnquiryRow(this);"));

			}
			//tRow.Attributes.Add("onclick", string.Format("setEnquiryRow(this, 'ID,2nd' , myForm.{0}.value+','+2md.value);", e.Item.FindControl("lblID").ClientID) );
		}

		private void lister_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) 
		{
			
			//Response.Write();
		}

		public string GetRefreshInterval()
{ 
	 string rate = ""; 
	 return rate; 


} 

		
		

	}
}

