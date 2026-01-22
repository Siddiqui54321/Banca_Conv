using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using shsm;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_LPVD_VALIDATIONDETAIL_DECISION : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		private int pageNumber=1;
		int PAGE_SIZE= 50 ;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		
		shgn.SHGNCommand entityClass;
		/******************* Entity Fields *****************/
protected System.Web.UI.WebControls.Literal ltlPPR_PRODCD;
protected System.Web.UI.WebControls.Literal ltlPVL_VALIDATIONFOR;
protected System.Web.UI.WebControls.Literal ltlPVL_LEVEL;

				
				
	#region pk variables declaration		
			private string  PPR_PRODCD;
	private string  PVL_VALIDATIONFOR;
	private double  PVL_LEVEL;
	private double  PVD_LEVEL;
	private string  PVD_VALIDATIONFOR;
						
	#endregion
				
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		private void InitializeComponent(){
			this.lister.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.lister_ItemDataBound);
			this.lister.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.lister_ItemCommand);
		}
	#endregion		


	#region Major methods of First Step		
		protected override void ValidateParams() 
		{
			base.ValidateParams ();			
			string[] param;
			foreach (string key in Request.Params.AllKeys)
			{
				if (key!=null && key.StartsWith("r_"))
				{
					param = Request[key].Split(',');
					SessionObject.Set(key.Replace("r_",""), param[param.Length-1]); 					
				}
			}
		}
		sealed protected override DataHolder GetInputData(DataHolder dataHolder)
		{
			GetSessionValues();
			CheckKeyLevel();
			//recordCount = LPVD_VALIDATIONDETAILDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
	
						
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("LPVD_VALIDATIONDETAIL");
			IDataReader LPVD_VALIDATIONDETAILReader= LPVD_VALIDATIONDETAILDB.GetLPVD_VALIDATIONDETAIL_DECISION_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			//recordSelected = IsRecordSelected(LPVD_VALIDATIONDETAILReader, LPVD_VALIDATIONDETAIL.PrimaryKeys, "LPVD_VALIDATIONDETAIL");
			//LPVD_VALIDATIONDETAILReader= LPVD_VALIDATIONDETAILDB.GetLPVD_VALIDATIONDETAIL_DECISION_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(LPVD_VALIDATIONDETAILReader, table, PAGE_SIZE, LPVD_VALIDATIONDETAIL.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(LPVD_VALIDATIONDETAILReader, table, PAGE_SIZE, pageNumber);
			}
			LPVD_VALIDATIONDETAILReader.Close();
			BindLister(table);							
		

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;
				
			
		
			/************** Array Data Script **************/
			
			
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			pagerList.SelectedIndex = pageNumber-1;		
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion
    
	#region Major methods of the final step
		protected override void ValidateRequest() {
			base.ValidateRequest();									
			foreach (string key in LPVD_VALIDATIONDETAIL.PrimaryKeys){
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null){
					if (ctrl is WebControl){
					//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null)){
							control.Enabled = true;
						}				
					}
				}
			}			
		}
		sealed protected override DataHolder GetData(DataHolder dataHolder) {	
			pageNumber = pagerList.SelectedIndex +1;
			//recordCount = LPVD_VALIDATIONDETAILDB.RecordCount;
			return dataHolder;
		}      
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
			case (EnumControlArgs.Save):
				_lastEvent.Text = "Save";
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPVD_VALIDATIONDETAILDB(dataHolder).FindByPK(txtPPR_PRODCD.Text,double.Parse(txtPVD_LEVEL.Text),txtPVD_VALIDATIONFOR.Text,double.Parse(txtPVL_LEVEL.Text),txtPVL_VALIDATIONFOR.Text);
				columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
columnNameValue.Add("PVL_VALIDATIONFOR",txtPVL_VALIDATIONFOR.Text.Trim()==""?null:txtPVL_VALIDATIONFOR.Text);
columnNameValue.Add("PVL_LEVEL",txtPVL_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVL_LEVEL.Text));
columnNameValue.Add("PVD_LEVEL",txtPVD_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVD_LEVEL.Text));
columnNameValue.Add("PVD_SEQUENCE",txtPVD_SEQUENCE.Text.Trim()==""?null:(object)double.Parse(txtPVD_SEQUENCE.Text));
columnNameValue.Add("PVD_FIELDNATURE",ddlPVD_FIELDNATURE.SelectedValue.Trim()==""?null:ddlPVD_FIELDNATURE.SelectedValue);
columnNameValue.Add("PVD_VALIDATIONFOR",txtPVD_VALIDATIONFOR.Text.Trim()==""?null:txtPVD_VALIDATIONFOR.Text);
columnNameValue.Add("PVD_DATATYPE",ddlPVD_DATATYPE.SelectedValue.Trim()==""?null:ddlPVD_DATATYPE.SelectedValue);
columnNameValue.Add("PVD_RELOPERATOR",ddlPVD_RELOPERATOR.SelectedValue.Trim()==""?null:ddlPVD_RELOPERATOR.SelectedValue);
columnNameValue.Add("PVD_VALIDFROM",txtPVD_VALIDFROM.Text.Trim()==""?null:txtPVD_VALIDFROM.Text);
columnNameValue.Add("PVD_VALIDTO",txtPVD_VALIDTO.Text.Trim()==""?null:txtPVD_VALIDTO.Text);
								
				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
				if (security.SaveAllowed){
					
					new LPVD_VALIDATIONDETAIL(dataHolder).Add(columnNameValue,getAllFields(),"LPVD_VALIDATIONDETAIL_DECISION",null);

					dataHolder.Update(DB.Transaction);
					
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LPVD_VALIDATIONDETAIL");
					_lastEvent.Text = "Save"; 					
					PrintMessage("Record has been saved");
				}
				else{
					PrintMessage("You are not autherized to Save.");
				}
				break;
			case (EnumControlArgs.Update):					
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPVD_VALIDATIONDETAILDB(dataHolder).FindByPK(txtPPR_PRODCD.Text,double.Parse(txtPVD_LEVEL.Text),txtPVD_VALIDATIONFOR.Text,double.Parse(txtPVL_LEVEL.Text),txtPVL_VALIDATIONFOR.Text);				
				columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
columnNameValue.Add("PVL_VALIDATIONFOR",txtPVL_VALIDATIONFOR.Text.Trim()==""?null:txtPVL_VALIDATIONFOR.Text);
columnNameValue.Add("PVL_LEVEL",txtPVL_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVL_LEVEL.Text));
columnNameValue.Add("PVD_LEVEL",txtPVD_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVD_LEVEL.Text));
columnNameValue.Add("PVD_SEQUENCE",txtPVD_SEQUENCE.Text.Trim()==""?null:(object)double.Parse(txtPVD_SEQUENCE.Text));
columnNameValue.Add("PVD_FIELDNATURE",ddlPVD_FIELDNATURE.SelectedValue.Trim()==""?null:ddlPVD_FIELDNATURE.SelectedValue);
columnNameValue.Add("PVD_VALIDATIONFOR",txtPVD_VALIDATIONFOR.Text.Trim()==""?null:txtPVD_VALIDATIONFOR.Text);
columnNameValue.Add("PVD_DATATYPE",ddlPVD_DATATYPE.SelectedValue.Trim()==""?null:ddlPVD_DATATYPE.SelectedValue);
columnNameValue.Add("PVD_RELOPERATOR",ddlPVD_RELOPERATOR.SelectedValue.Trim()==""?null:ddlPVD_RELOPERATOR.SelectedValue);
columnNameValue.Add("PVD_VALIDFROM",txtPVD_VALIDFROM.Text.Trim()==""?null:txtPVD_VALIDFROM.Text);
columnNameValue.Add("PVD_VALIDTO",txtPVD_VALIDTO.Text.Trim()==""?null:txtPVD_VALIDTO.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
				if (security.UpdateAllowed){
					
					new LPVD_VALIDATIONDETAIL(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

					dataHolder.Update(DB.Transaction);
					
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LPVD_VALIDATIONDETAIL");
					recordSelected = true;
					PrintMessage("Record has been updated");
				}
				else{
					PrintMessage("You are not autherized to Update.");
				}
				break;
			case (EnumControlArgs.Delete):
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPVD_VALIDATIONDETAILDB(dataHolder).FindByPK(txtPPR_PRODCD.Text,double.Parse(txtPVD_LEVEL.Text),txtPVD_VALIDATIONFOR.Text,double.Parse(txtPVL_LEVEL.Text),txtPVL_VALIDATIONFOR.Text);				
				columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
columnNameValue.Add("PVL_VALIDATIONFOR",txtPVL_VALIDATIONFOR.Text.Trim()==""?null:txtPVL_VALIDATIONFOR.Text);
columnNameValue.Add("PVL_LEVEL",txtPVL_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVL_LEVEL.Text));
columnNameValue.Add("PVD_LEVEL",txtPVD_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVD_LEVEL.Text));
columnNameValue.Add("PVD_SEQUENCE",txtPVD_SEQUENCE.Text.Trim()==""?null:(object)double.Parse(txtPVD_SEQUENCE.Text));
columnNameValue.Add("PVD_FIELDNATURE",ddlPVD_FIELDNATURE.SelectedValue.Trim()==""?null:ddlPVD_FIELDNATURE.SelectedValue);
columnNameValue.Add("PVD_VALIDATIONFOR",txtPVD_VALIDATIONFOR.Text.Trim()==""?null:txtPVD_VALIDATIONFOR.Text);
columnNameValue.Add("PVD_DATATYPE",ddlPVD_DATATYPE.SelectedValue.Trim()==""?null:ddlPVD_DATATYPE.SelectedValue);
columnNameValue.Add("PVD_RELOPERATOR",ddlPVD_RELOPERATOR.SelectedValue.Trim()==""?null:ddlPVD_RELOPERATOR.SelectedValue);
columnNameValue.Add("PVD_VALIDFROM",txtPVD_VALIDFROM.Text.Trim()==""?null:txtPVD_VALIDFROM.Text);
columnNameValue.Add("PVD_VALIDTO",txtPVD_VALIDTO.Text.Trim()==""?null:txtPVD_VALIDTO.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
				if (security.DeleteAllowed){
				
				new LPVD_VALIDATIONDETAIL(dataHolder).Delete(columnNameValue);

				dataHolder.Update(DB.Transaction);
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LPVD_VALIDATIONDETAIL");
				PrintMessage("Record has been deleted");				
				}
				else{
					PrintMessage("You are not autherized to Delete.");
				}

				break;
			case (EnumControlArgs.Process):						
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPVD_VALIDATIONDETAILDB(dataHolder).FindByPK(txtPPR_PRODCD.Text,double.Parse(txtPVD_LEVEL.Text),txtPVD_VALIDATIONFOR.Text,double.Parse(txtPVL_LEVEL.Text),txtPVL_VALIDATIONFOR.Text);				
				columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
columnNameValue.Add("PVL_VALIDATIONFOR",txtPVL_VALIDATIONFOR.Text.Trim()==""?null:txtPVL_VALIDATIONFOR.Text);
columnNameValue.Add("PVL_LEVEL",txtPVL_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVL_LEVEL.Text));
columnNameValue.Add("PVD_LEVEL",txtPVD_LEVEL.Text.Trim()==""?null:(object)double.Parse(txtPVD_LEVEL.Text));
columnNameValue.Add("PVD_SEQUENCE",txtPVD_SEQUENCE.Text.Trim()==""?null:(object)double.Parse(txtPVD_SEQUENCE.Text));
columnNameValue.Add("PVD_FIELDNATURE",ddlPVD_FIELDNATURE.SelectedValue.Trim()==""?null:ddlPVD_FIELDNATURE.SelectedValue);
columnNameValue.Add("PVD_VALIDATIONFOR",txtPVD_VALIDATIONFOR.Text.Trim()==""?null:txtPVD_VALIDATIONFOR.Text);
columnNameValue.Add("PVD_DATATYPE",ddlPVD_DATATYPE.SelectedValue.Trim()==""?null:ddlPVD_DATATYPE.SelectedValue);
columnNameValue.Add("PVD_RELOPERATOR",ddlPVD_RELOPERATOR.SelectedValue.Trim()==""?null:ddlPVD_RELOPERATOR.SelectedValue);
columnNameValue.Add("PVD_VALIDFROM",txtPVD_VALIDFROM.Text.Trim()==""?null:txtPVD_VALIDFROM.Text);
columnNameValue.Add("PVD_VALIDTO",txtPVD_VALIDTO.Text.Trim()==""?null:txtPVD_VALIDTO.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
				string result="";					
				if (_CustomArgName.Value == "ProcessName"){
					string processName = _CustomArgVal.Value;	
					if (security.ProcessAllowed(processName)){
						Type type = Type.GetType(processName);											
						if (type != null){
							shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
							NameValueCollection[] dataRows = new NameValueCollection[1];
							bool[] SelectedRowIndexes = new bool[1];
							//dataRows[0] = columnNameValue;
							dataRows[0] = getAllFields();
							SelectedRowIndexes[0] = true;
							proccessCommand.setAllFields(getAllFields());
							proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
							proccessCommand.setPrimaryKeys(LPVD_VALIDATIONDETAIL.PrimaryKeys);
							proccessCommand.setTableName("LPVD_VALIDATIONDETAIL");
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							result = proccessCommand.processing();
							//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
						}
					}
					else{
						result = "You are not Autherized to Execute Process.";
					}						
				}	
				recordSelected =true;
				if (result.Length>0)
					PrintMessage(result);
				break;
			}
		}
		
		sealed protected override void DataBind(DataHolder dataHolder){			
			
		  	
			
			LPVD_VALIDATIONDETAILDB LPVD_VALIDATIONDETAILDB_obj = new LPVD_VALIDATIONDETAILDB(dataHolder);		
			IDataReader LPVD_VALIDATIONDETAILReader;
			DataTable table = new DataTable("LPVD_VALIDATIONDETAIL") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit){
				DataRow row = LPVD_VALIDATIONDETAILDB_obj.FindByPK(PPR_PRODCD,PVD_LEVEL,PVD_VALIDATIONFOR,PVL_LEVEL,PVL_VALIDATIONFOR)["LPVD_VALIDATIONDETAIL"].Rows[0];
				ShowData(row);
			}		
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter){
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					LPVD_VALIDATIONDETAILReader = LPVD_VALIDATIONDETAILDB.GetLPVD_VALIDATIONDETAIL_DECISION_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();				
				else
					LPVD_VALIDATIONDETAILReader = LPVD_VALIDATIONDETAILDB.GetLPVD_VALIDATIONDETAIL_DECISION_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());
				recordCount = Utilities.Reader2Table(LPVD_VALIDATIONDETAILReader, table, PAGE_SIZE, pageNumber);
				LPVD_VALIDATIONDETAILReader.Close();
	
				BindLister(table);
                        if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
				RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LPVD_VALIDATIONDETAIL"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString()){
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save){
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else{
					if((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					{
						_lastEvent.Text = "New";
					}
					else
						_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();
				}
			}
			//for header & footer script					
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	

			HeaderScript.Text = EnvHelper.Parse("");
			FooterScript.Text = EnvHelper.Parse("");

			pagerList.SelectedIndex = pageNumber - 1;			
			
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion	

	#region Events
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) {
			pageNumber = pagerList.SelectedIndex+1;
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Pager;
			DoControl();
			pagerList.SelectedIndex=pageNumber-1;
		}
		private void btnViewAll_Click(object sender, System.EventArgs e) {
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Cancel  ;
			DoControl();
		}
		
		private void lister_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) {
			foreach (RepeaterItem item in lister.Items){
				if (item == e.Item){
					((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				}
				else{
					if (item.ItemType == ListItemType.Item)
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerItem";
					else
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerAlterItem";
				}
			}
			if (e.CommandName == "Edit") {								
				if (e.Item.ItemType==ListItemType.Item){									
					PVD_VALIDATIONFOR=((LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR1")).Text;
PPR_PRODCD=((Label)e.Item.FindControl("lblPPR_PRODCD1")).Text;
PVL_VALIDATIONFOR=((Label)e.Item.FindControl("lblPVL_VALIDATIONFOR1")).Text;
PVL_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVL_LEVEL1")).Text);
PVD_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVD_LEVEL1")).Text);

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem){
					PVD_VALIDATIONFOR=((LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR2")).Text;
PPR_PRODCD=((Label)e.Item.FindControl("lblPPR_PRODCD2")).Text;
PVL_VALIDATIONFOR=((Label)e.Item.FindControl("lblPVL_VALIDATIONFOR2")).Text;
PVL_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVL_LEVEL2")).Text);
PVD_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVD_LEVEL2")).Text);
	
				}
				ControlArgs=new object[1];
				ControlArgs[0]=EnumControlArgs.Edit; 
				DoControl();								
			}				
		}	
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) {
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value){
				case "Update" :
					ControlArgs[0]=EnumControlArgs.Update;
					DoControl();
					break;
				case "Save" :
					ControlArgs[0]=EnumControlArgs.Save;
					DoControl();
					break;
				case "Delete" :
					ControlArgs[0]=EnumControlArgs.Delete;
					DoControl();
					break;
				case "Filter" :
					ControlArgs[0] = EnumControlArgs.Filter;
					DoControl();
					break;
				case "Process" :
					ControlArgs[0] = EnumControlArgs.Process;
					DoControl();
					break;	
			}
			_CustomEventVal.Value="";										
		}		
		private void lister_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) 
		{
			if (recordSelected)
				FindAndSelectCurrentRecord(e);
			HtmlTableRow tRow = (HtmlTableRow)e.Item.FindControl("ListerRow");
			LinkButton linkPVD_VALIDATIONFOR = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item){
				linkPVD_VALIDATIONFOR = (LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem){
				linkPVD_VALIDATIONFOR=(LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR2");	
			}			
			tRow.Attributes.Add("onclick", linkPVD_VALIDATIONFOR.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			//Stop  by Rehan it clears session values 24/04/2006
			//PPR_PRODCD","PVL_VALIDATIONFOR","PVL_LEVEL","PVD_LEVEL","PVD_VALIDATIONFOR
			
			if (SetFieldsInSession())
			{
				//PPR_PRODCD
				//PVL_VALIDATIONFOR
				//PVL_LEVEL
				SessionObject.Set("PVD_VALIDATIONFOR",txtPVD_VALIDATIONFOR.Text);
				SessionObject.Set("PVD_LEVEL",txtPVD_LEVEL.Text);
				//SessionObject.Set("PVD_SEQUENCE",txtPVD_SEQUENCE.Text);
				//SessionObject.Set("PVD_FIELDNATURE",ddlPVD_FIELDNATURE.SelectedValue);
				//SessionObject.Set("PVD_DATATYPE",ddlPVD_DATATYPE.SelectedValue);
				//SessionObject.Set("PVD_RELOPERATOR",ddlPVD_RELOPERATOR.SelectedValue);
				//SessionObject.Set("PVD_VALIDFROM",txtPVD_VALIDFROM.Text);
				//SessionObject.Set("PVD_VALIDTO",txtPVD_VALIDTO.Text);
			}
		}	
	
	#endregion 
		protected override bool TransactionRequired {
	 	 get {
			return true;
		     }
		}

		private void GetSessionValues()
		{
			if (SessionObject.Get("PPR_PRODCD")==null  || SessionObject.GetString("PPR_PRODCD")== ""  || SessionObject.Get("PVL_VALIDATIONFOR")==null  || SessionObject.GetString("PVL_VALIDATIONFOR")== ""  || SessionObject.Get("PVL_LEVEL")==null  || SessionObject.GetString("PVL_LEVEL")== "" ){	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				txtPPR_PRODCD.Text=SessionObject.GetString("PPR_PRODCD");
txtPVL_VALIDATIONFOR.Text=SessionObject.GetString("PVL_VALIDATIONFOR");
txtPVL_LEVEL.Text=SessionObject.GetString("PVL_LEVEL");




				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		
		
		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			//*//txtPPR_PRODCD.Text="";
//*//txtPVL_VALIDATIONFOR.Text="";
//*//txtPVL_LEVEL.Text="0";
txtPVD_LEVEL.Enabled = true;
txtPVD_LEVEL.Text="";
txtPVD_SEQUENCE.Text="";
ddlPVD_FIELDNATURE.ClearSelection();
txtPVD_VALIDATIONFOR.Enabled = true;
txtPVD_VALIDATIONFOR.Text="";
ddlPVD_DATATYPE.ClearSelection();
ddlPVD_RELOPERATOR.ClearSelection();
txtPVD_VALIDFROM.Text="";
txtPVD_VALIDTO.Text="";

		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtPPR_PRODCD.Text=objRow["PPR_PRODCD"].ToString();
txtPPR_PRODCD.Enabled=false;
txtPVL_VALIDATIONFOR.Text=objRow["PVL_VALIDATIONFOR"].ToString();
txtPVL_VALIDATIONFOR.Enabled=false;
txtPVL_LEVEL.Text=objRow["PVL_LEVEL"].ToString();
txtPVL_LEVEL.Enabled=false;
txtPVD_LEVEL.Text=objRow["PVD_LEVEL"].ToString();
txtPVD_LEVEL.Enabled=false;
txtPVD_SEQUENCE.Text=objRow["PVD_SEQUENCE"].ToString();
ddlPVD_FIELDNATURE.ClearSelection();
ListItem item5=ddlPVD_FIELDNATURE.Items.FindByValue(objRow["PVD_FIELDNATURE"].ToString());
if (item5!= null){
item5.Selected=true;
}txtPVD_VALIDATIONFOR.Text=objRow["PVD_VALIDATIONFOR"].ToString();
txtPVD_VALIDATIONFOR.Enabled=false;
ddlPVD_DATATYPE.ClearSelection();
ListItem item7=ddlPVD_DATATYPE.Items.FindByValue(objRow["PVD_DATATYPE"].ToString());
if (item7!= null){
item7.Selected=true;
}ddlPVD_RELOPERATOR.ClearSelection();
ListItem item8=ddlPVD_RELOPERATOR.Items.FindByValue(objRow["PVD_RELOPERATOR"].ToString());
if (item8!= null){
item8.Selected=true;
}txtPVD_VALIDFROM.Text=objRow["PVD_VALIDFROM"].ToString();
txtPVD_VALIDTO.Text=objRow["PVD_VALIDTO"].ToString();


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPVD_VALIDATIONDETAIL.PrimaryKeys, columnNameValue, "LPVD_VALIDATIONDETAIL");
			foreach(string processName in AllProcess){
				if (security.ProcessAllowed(processName)){
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed){
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
		}

		void SetLastEvent(){
		          if (Request["Operation"] == "View")
	                   _lastEvent.Text = "View" ;
 	        }

		void SetListerVisibility(){

                    Utilities.SetListerVisibility(this);
	        }



		private void BindLister(DataTable table)
		{
			lister.DataSource = table;
			lister.DataBind();
			pagerList.Items.Clear();
			for (int i=1;recordCount>0; recordCount-=PAGE_SIZE){				
				pagerList.Items.Add(i.ToString());		
				i++;
			}
			
			//pagerList.SelectedIndex = pageNumber-1;//commented bcz of pagging error
		}

		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}

		bool SetFieldsInSession()
		{
			bool flag = false;
			if (_lastEvent.Text.Equals(EnumControlArgs.Edit.ToString()))
			{
				flag = true;
			}
			else 
			{				
				if (ControlArgs!=null)
				{
					if (ControlArgs[0]!=null)
					{
						EnumControlArgs arg = (EnumControlArgs)ControlArgs[0] ;
						if (arg.Equals(EnumControlArgs.Save) || arg.Equals(EnumControlArgs.Edit))
						{
							flag = true;
						}
					}					
				}
			}
			return flag;
		}

		/**
		 * New Method Added For New Support
	 	 */
		private NameValueCollection getAllFields() {
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) {
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}

			//foreach (Control c in this.myForm.Controls) {	
			foreach (Control c in this.EntryTableDiv.Controls) {	
				string _fieldName="";
				if (c is WebControl) {
					switch (c.GetType().ToString()) {
						case "System.Web.UI.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) {
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) {
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}


		bool IsRecordSelected(){
			bool selected = true;
			foreach (string pk in LPVD_VALIDATIONDETAIL.PrimaryKeys){
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0){
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			RepeaterItem item=e.Item;
			bool recordFound = true;
			DataRowView row = (DataRowView)item.DataItem;
			foreach(string pk in  LPVD_VALIDATIONDETAIL.PrimaryKeys){
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound){
				if (item.ItemType == ListItemType.Item){
					PVD_VALIDATIONFOR=((LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR1")).Text;
PPR_PRODCD=((Label)e.Item.FindControl("lblPPR_PRODCD1")).Text;
PVL_VALIDATIONFOR=((Label)e.Item.FindControl("lblPVL_VALIDATIONFOR1")).Text;
PVL_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVL_LEVEL1")).Text);
PVD_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVD_LEVEL1")).Text);
	
				}
				else{
					PVD_VALIDATIONFOR=((LinkButton)e.Item.FindControl("linkPVD_VALIDATIONFOR2")).Text;
PPR_PRODCD=((Label)e.Item.FindControl("lblPPR_PRODCD2")).Text;
PVL_VALIDATIONFOR=((Label)e.Item.FindControl("lblPVL_VALIDATIONFOR2")).Text;
PVL_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVL_LEVEL2")).Text);
PVD_LEVEL=double.Parse(((Label)e.Item.FindControl("lblPVD_LEVEL2")).Text);
	
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new LPVD_VALIDATIONDETAILDB(dataHolder).FindByPK(PPR_PRODCD,PVD_LEVEL,PVD_VALIDATIONFOR,PVL_LEVEL,PVL_VALIDATIONFOR)["LPVD_VALIDATIONDETAIL"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm(){
			if (btnHideLister!=null)
				btnHideLister.Disabled=true;
			EntryTableDiv.Style.Add("visibility" , "hidden");
			ListerDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields{
			get{	
				return EntryTableDiv.Controls;
			}
		}
	}
}

