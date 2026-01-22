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
	public partial class shgn_ss_se_stdscreen_ILUS_ST_GUARDIAN : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal RefreshParent;

		

		private int pageNumber=1;
		int PAGE_SIZE= SHMA.Enterprise.Configuration.AppSettings.GetInt("NoOfListerRows") ;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		
		//shgn.SHGNCommand entityClass;
		/******************* Entity Fields *****************/


				
				
	#region pk variables declaration		
			private double  NGU_GUARDCD;
						
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
			this.pagerList.SelectedIndexChanged += new System.EventHandler(this.pagerList_SelectedIndexChanged);
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
			//recordCount = LNGU_GUARDIANDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			//manuall code
			LoadQueryStringParameter();
	
			IDataReader LCRL_RELATIONReader0 = LCRL_RELATIONDB.GetDDL_ILUS_ST_GUARDIAN_CRL_RELEATIOCD_RO();;
ddlCRL_RELEATIOCD.DataSource = LCRL_RELATIONReader0 ;
ddlCRL_RELEATIOCD.DataBind();
LCRL_RELATIONReader0.Close();
			
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("LNGU_GUARDIAN");
			IDataReader LNGU_GUARDIANReader= LNGU_GUARDIANDB.GetILUS_ST_GUARDIAN_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			//recordSelected = IsRecordSelected(LNGU_GUARDIANReader, LNGU_GUARDIAN.PrimaryKeys, "LNGU_GUARDIAN");
			//LNGU_GUARDIANReader= LNGU_GUARDIANDB.GetILUS_ST_GUARDIAN_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(LNGU_GUARDIANReader, table, PAGE_SIZE, LNGU_GUARDIAN.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(LNGU_GUARDIANReader, table, PAGE_SIZE, pageNumber);
			}
			LNGU_GUARDIANReader.Close();
			BindLister(table);							
		

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("getField(\"NP1_PROPOSAL\").value = SV(\"NP1_PROPOSAL\"); getField(\"USE_USERID\").value = SV(\"s_USE_USERID\");  getField(\"USE_DATETIME\").value = SV(\"s_CURR_SYSDATE\");") ;

			
		
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
			foreach (string key in LNGU_GUARDIAN.PrimaryKeys){
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
			//recordCount = LNGU_GUARDIANDB.RecordCount;
			return dataHolder;
		}      
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){
			RefreshParent.Text = "";
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;
			
			shgn.SHGNCommand entityClass=new ace.ILUS_ST_GUARDIAN();
			entityClass.setNameValueCollection(columnNameValue);
			
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
			case (EnumControlArgs.Save):
				_lastEvent.Text = "Save";
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNGU_GUARDIANDB(dataHolder).FindByPK(double.Parse(txtNGU_GUARDCD.Text));
				columnNameValue.Add("NGU_GUARDCD",txtNGU_GUARDCD.Text.Trim()==""?null:(object)double.Parse(txtNGU_GUARDCD.Text));
				columnNameValue.Add("NGU_NAME",txtNGU_NAME.Text.Trim()==""?null:txtNGU_NAME.Text);
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NGU_ADDRESS",txtNGU_ADDRESS.Text.Trim()==""?null:txtNGU_ADDRESS.Text);
				columnNameValue.Add("NGU_ADDRESS2",txtNGU_ADDRESS2.Text.Trim()==""?null:txtNGU_ADDRESS2.Text);
				columnNameValue.Add("NGU_ADDRESS3",txtNGU_ADDRESS3.Text.Trim()==""?null:txtNGU_ADDRESS3.Text);
				columnNameValue.Add("NGU_EMAIL",txtNGU_EMAIL.Text.Trim()==""?null:txtNGU_EMAIL.Text);
				columnNameValue.Add("NGU_IDNO",txtNGU_IDNO.Text.Trim()==""?null:txtNGU_IDNO.Text);
				columnNameValue.Add("NGU_TELENO",txtNGU_TELENO.Text.Trim()==""?null:txtNGU_TELENO.Text);
				columnNameValue.Add("NGU_FAX",txtNGU_FAX.Text.Trim()==""?null:txtNGU_FAX.Text);
				columnNameValue.Add("NGU_AGE",txtNGU_AGE.Text.Trim()==""?null:(object)double.Parse(txtNGU_AGE.Text));
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("USE_USERID",txtUSE_USERID.Text.Trim()==""?null:txtUSE_USERID.Text);
				columnNameValue.Add("USE_DATETIME",txtUSE_DATETIME.Text.Trim()==""?null:(object)DateTime.Parse(txtUSE_DATETIME.Text));


								
				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
				if (security.SaveAllowed){
					entityClass.fsoperationBeforeSave();
					
					new LNGU_GUARDIAN(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ST_GUARDIAN",null);

					dataHolder.Update(DB.Transaction);

                    entityClass.fsoperationAfterSave();

                    auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNGU_GUARDIAN.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNGU_GUARDIAN");
					_lastEvent.Text = "Save"; 					
					//PrintMessage("Record has been saved");
					PrintMessage("Guardian assigned succesfully");
				}
				else{
					PrintMessage("You are not autherized to Save.");
				}

				refreshParentWindow();

				break;
			case (EnumControlArgs.Update):					
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNGU_GUARDIANDB(dataHolder).FindByPK(double.Parse(txtNGU_GUARDCD.Text));				
				columnNameValue.Add("NGU_GUARDCD",txtNGU_GUARDCD.Text.Trim()==""?null:(object)double.Parse(txtNGU_GUARDCD.Text));
				columnNameValue.Add("NGU_NAME",txtNGU_NAME.Text.Trim()==""?null:txtNGU_NAME.Text);
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NGU_ADDRESS",txtNGU_ADDRESS.Text.Trim()==""?null:txtNGU_ADDRESS.Text);
				columnNameValue.Add("NGU_ADDRESS2",txtNGU_ADDRESS2.Text.Trim()==""?null:txtNGU_ADDRESS2.Text);
				columnNameValue.Add("NGU_ADDRESS3",txtNGU_ADDRESS3.Text.Trim()==""?null:txtNGU_ADDRESS3.Text);
				columnNameValue.Add("NGU_EMAIL",txtNGU_EMAIL.Text.Trim()==""?null:txtNGU_EMAIL.Text);
				columnNameValue.Add("NGU_IDNO",txtNGU_IDNO.Text.Trim()==""?null:txtNGU_IDNO.Text);
				columnNameValue.Add("NGU_TELENO",txtNGU_TELENO.Text.Trim()==""?null:txtNGU_TELENO.Text);
				columnNameValue.Add("NGU_FAX",txtNGU_FAX.Text.Trim()==""?null:txtNGU_FAX.Text);
				columnNameValue.Add("NGU_AGE",txtNGU_AGE.Text.Trim()==""?null:(object)double.Parse(txtNGU_AGE.Text));
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("USE_USERID",txtUSE_USERID.Text.Trim()==""?null:txtUSE_USERID.Text);
				columnNameValue.Add("USE_DATETIME",txtUSE_DATETIME.Text.Trim()==""?null:(object)DateTime.Parse(txtUSE_DATETIME.Text));

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
				if (security.UpdateAllowed){
					entityClass.fsoperationBeforeUpdate();
					
					new LNGU_GUARDIAN(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

					dataHolder.Update(DB.Transaction);
					
					entityClass.fsoperationAfterUpdate();
					
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNGU_GUARDIAN");
					recordSelected = true;
					//PrintMessage("Record has been updated");
					PrintMessage("Guardian assigned succesfully");
				}
				else{
					PrintMessage("You are not autherized to Update.");
				}

					refreshParentWindow();

				break;
			case (EnumControlArgs.Delete):
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNGU_GUARDIANDB(dataHolder).FindByPK(double.Parse(txtNGU_GUARDCD.Text));				
				columnNameValue.Add("NGU_GUARDCD",txtNGU_GUARDCD.Text.Trim()==""?null:(object)double.Parse(txtNGU_GUARDCD.Text));
				columnNameValue.Add("NGU_NAME",txtNGU_NAME.Text.Trim()==""?null:txtNGU_NAME.Text);
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NGU_ADDRESS",txtNGU_ADDRESS.Text.Trim()==""?null:txtNGU_ADDRESS.Text);
				columnNameValue.Add("NGU_ADDRESS2",txtNGU_ADDRESS2.Text.Trim()==""?null:txtNGU_ADDRESS2.Text);
				columnNameValue.Add("NGU_ADDRESS3",txtNGU_ADDRESS3.Text.Trim()==""?null:txtNGU_ADDRESS3.Text);
				columnNameValue.Add("NGU_EMAIL",txtNGU_EMAIL.Text.Trim()==""?null:txtNGU_EMAIL.Text);
				columnNameValue.Add("NGU_IDNO",txtNGU_IDNO.Text.Trim()==""?null:txtNGU_IDNO.Text);
				columnNameValue.Add("NGU_TELENO",txtNGU_TELENO.Text.Trim()==""?null:txtNGU_TELENO.Text);
				columnNameValue.Add("NGU_FAX",txtNGU_FAX.Text.Trim()==""?null:txtNGU_FAX.Text);
				columnNameValue.Add("NGU_AGE",txtNGU_AGE.Text.Trim()==""?null:(object)double.Parse(txtNGU_AGE.Text));
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("USE_USERID",txtUSE_USERID.Text.Trim()==""?null:txtUSE_USERID.Text);
				columnNameValue.Add("USE_DATETIME",txtUSE_DATETIME.Text.Trim()==""?null:(object)DateTime.Parse(txtUSE_DATETIME.Text));

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
				if (security.DeleteAllowed)
				{
					entityClass.fsoperationBeforeDelete();
					//Do not delete guardian record physically just remove proposal number
					//new LNGU_GUARDIAN(dataHolder).Delete(columnNameValue);
					//dataHolder.Update(DB.Transaction);
					entityClass.fsoperationAfterDelete();
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNGU_GUARDIAN");
					//PrintMessage("Record has been deleted");				
					PrintMessage("Guardian revoked succesfully");
				}
				else{
					PrintMessage("You are not autherized to Delete.");
				}
					
				refreshParentWindow();

				break;
			case (EnumControlArgs.Process):						
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNGU_GUARDIANDB(dataHolder).FindByPK(double.Parse(txtNGU_GUARDCD.Text));				
				columnNameValue.Add("NGU_GUARDCD",txtNGU_GUARDCD.Text.Trim()==""?null:(object)double.Parse(txtNGU_GUARDCD.Text));
columnNameValue.Add("NGU_NAME",txtNGU_NAME.Text.Trim()==""?null:txtNGU_NAME.Text);
columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
columnNameValue.Add("NGU_ADDRESS",txtNGU_ADDRESS.Text.Trim()==""?null:txtNGU_ADDRESS.Text);
columnNameValue.Add("NGU_ADDRESS2",txtNGU_ADDRESS2.Text.Trim()==""?null:txtNGU_ADDRESS2.Text);
columnNameValue.Add("NGU_ADDRESS3",txtNGU_ADDRESS3.Text.Trim()==""?null:txtNGU_ADDRESS3.Text);
columnNameValue.Add("NGU_EMAIL",txtNGU_EMAIL.Text.Trim()==""?null:txtNGU_EMAIL.Text);
columnNameValue.Add("NGU_IDNO",txtNGU_IDNO.Text.Trim()==""?null:txtNGU_IDNO.Text);
columnNameValue.Add("NGU_TELENO",txtNGU_TELENO.Text.Trim()==""?null:txtNGU_TELENO.Text);
columnNameValue.Add("NGU_FAX",txtNGU_FAX.Text.Trim()==""?null:txtNGU_FAX.Text);
columnNameValue.Add("NGU_AGE",txtNGU_AGE.Text.Trim()==""?null:(object)double.Parse(txtNGU_AGE.Text));
columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
columnNameValue.Add("USE_USERID",txtUSE_USERID.Text.Trim()==""?null:txtUSE_USERID.Text);
columnNameValue.Add("USE_DATETIME",txtUSE_DATETIME.Text.Trim()==""?null:(object)DateTime.Parse(txtUSE_DATETIME.Text));

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
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
							proccessCommand.setPrimaryKeys(LNGU_GUARDIAN.PrimaryKeys);
							proccessCommand.setTableName("LNGU_GUARDIAN");
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
			
		  	
			
			LNGU_GUARDIANDB LNGU_GUARDIANDB_obj = new LNGU_GUARDIANDB(dataHolder);		
			IDataReader LNGU_GUARDIANReader;
			DataTable table = new DataTable("LNGU_GUARDIAN") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit){
				DataRow row = LNGU_GUARDIANDB_obj.FindByPK(NGU_GUARDCD)["LNGU_GUARDIAN"].Rows[0];
				ShowData(row);
			}		
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter){
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					LNGU_GUARDIANReader = LNGU_GUARDIANDB.GetILUS_ST_GUARDIAN_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();				
				else
					LNGU_GUARDIANReader = LNGU_GUARDIANDB.GetILUS_ST_GUARDIAN_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());
				recordCount = Utilities.Reader2Table(LNGU_GUARDIANReader, table, PAGE_SIZE, pageNumber);
				LNGU_GUARDIANReader.Close();
	
				BindLister(table);
                        if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
				RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNGU_GUARDIAN"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString()){
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
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
					NGU_GUARDCD=double.Parse(((LinkButton)e.Item.FindControl("linkNGU_GUARDCD1")).Text);

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem){
					NGU_GUARDCD=double.Parse(((LinkButton)e.Item.FindControl("linkNGU_GUARDCD2")).Text);
	
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
			LinkButton linkNGU_GUARDCD = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item){
				linkNGU_GUARDCD = (LinkButton)e.Item.FindControl("linkNGU_GUARDCD1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem){
				linkNGU_GUARDCD=(LinkButton)e.Item.FindControl("linkNGU_GUARDCD2");	
			}			
			tRow.Attributes.Add("onclick", linkNGU_GUARDCD.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			//Stop  by Rehan it clears session values 24/04/2006
			/*
			if (SetFieldsInSession()){
				SessionObject.Set("NGU_GUARDCD",txtNGU_GUARDCD.Text);
		SessionObject.Set("NGU_NAME",txtNGU_NAME.Text);
		SessionObject.Set("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue);
		SessionObject.Set("NGU_ADDRESS",txtNGU_ADDRESS.Text);
		SessionObject.Set("NGU_ADDRESS2",txtNGU_ADDRESS2.Text);
		SessionObject.Set("NGU_ADDRESS3",txtNGU_ADDRESS3.Text);
		SessionObject.Set("NGU_EMAIL",txtNGU_EMAIL.Text);
		SessionObject.Set("NGU_IDNO",txtNGU_IDNO.Text);
		SessionObject.Set("NGU_TELENO",txtNGU_TELENO.Text);
		SessionObject.Set("NGU_FAX",txtNGU_FAX.Text);
		SessionObject.Set("NGU_AGE",txtNGU_AGE.Text);
		SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
		SessionObject.Set("USE_USERID",txtUSE_USERID.Text);
		SessionObject.Set("USE_DATETIME",txtUSE_DATETIME.Text);
		
			}*/
		}	
	
	#endregion 
		protected override bool TransactionRequired {
	 	 get {
			return true;
		     }
		}

		private void GetSessionValues()
		{
			if (false){	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				



				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		
		
		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			txtNGU_GUARDCD.Enabled = true;
txtNGU_GUARDCD.Text="0";
txtNGU_NAME.Text="";
ddlCRL_RELEATIOCD.ClearSelection();
txtNGU_ADDRESS.Text="";
txtNGU_ADDRESS2.Text="";
txtNGU_ADDRESS3.Text="";
txtNGU_EMAIL.Text="";
txtNGU_IDNO.Text="";
txtNGU_TELENO.Text="";
txtNGU_FAX.Text="";
txtNGU_AGE.Text="0";
txtNP1_PROPOSAL.Text="";
txtUSE_USERID.Text="";
txtUSE_DATETIME.Text="";

		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtNGU_GUARDCD.Text=objRow["NGU_GUARDCD"].ToString();
txtNGU_GUARDCD.Enabled=false;
txtNGU_NAME.Text=objRow["NGU_NAME"].ToString();
ddlCRL_RELEATIOCD.ClearSelection();
ListItem item2=ddlCRL_RELEATIOCD.Items.FindByValue(objRow["CRL_RELEATIOCD"].ToString());
if (item2!= null){
item2.Selected=true;
}txtNGU_ADDRESS.Text=objRow["NGU_ADDRESS"].ToString();
txtNGU_ADDRESS2.Text=objRow["NGU_ADDRESS2"].ToString();
txtNGU_ADDRESS3.Text=objRow["NGU_ADDRESS3"].ToString();
txtNGU_EMAIL.Text=objRow["NGU_EMAIL"].ToString();
txtNGU_IDNO.Text=objRow["NGU_IDNO"].ToString();
txtNGU_TELENO.Text=objRow["NGU_TELENO"].ToString();
txtNGU_FAX.Text=objRow["NGU_FAX"].ToString();
txtNGU_AGE.Text=objRow["NGU_AGE"].ToString();
txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
txtUSE_USERID.Text=objRow["USE_USERID"].ToString();
txtUSE_DATETIME.Text=objRow["USE_DATETIME"]==DBNull.Value?"":((DateTime)objRow["USE_DATETIME"]).ToShortDateString();


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNGU_GUARDIAN.PrimaryKeys, columnNameValue, "LNGU_GUARDIAN");
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
			foreach (string pk in LNGU_GUARDIAN.PrimaryKeys){
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
			foreach(string pk in  LNGU_GUARDIAN.PrimaryKeys){
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound){
				if (item.ItemType == ListItemType.Item){
					NGU_GUARDCD=double.Parse(((LinkButton)e.Item.FindControl("linkNGU_GUARDCD1")).Text);
	
				}
				else{
					NGU_GUARDCD=double.Parse(((LinkButton)e.Item.FindControl("linkNGU_GUARDCD2")).Text);
	
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new LNGU_GUARDIANDB(dataHolder).FindByPK(NGU_GUARDCD)["LNGU_GUARDIAN"].Rows[0];
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

		private void LoadQueryStringParameter()
		{
			string strProposal  = Request.QueryString["PROPOSAL"].ToString();  
			string strBenefiary = Request.QueryString["BENEFICIARY"].ToString();  
			string strGuardian  = Request.QueryString["GUARDIAN"].ToString();
			
			SessionObject.Set("BENEFICIARY_CODE", strBenefiary);
			SessionObject.Set("NGU_GUARDCD", strGuardian);

		}

		private void refreshParentWindow()
		{
			Response.Write("<script language='Javascript'>");
			Response.Write("window.opener.parent.location = window.opener.parent.location;");
			Response.Write("window.close();");
			Response.Write("</script>");
			//RefreshParent.Text = "window.opener.parent.location = window.opener.parent.location;window.close();";
            //Response.Redirect("../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY.aspx");
		}
	}
}

