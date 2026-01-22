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
	public partial class shgn_ss_se_stdscreen_ILUS_BENEFECIARY : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal GuardinaLiteral;
		

		private int pageNumber=1;
		//int PAGE_SIZE= SHMA.Enterprise.Configuration.AppSettings.GetInt("NoOfListerRows") ;
		int PAGE_SIZE = 100;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		protected System.Web.UI.WebControls.Literal ltlNP1_PROPOSAL;
		
		private bool blnIndividualReltion = true;
				
				
	#region pk variables declaration		
			private string  NBF_BENEFCD;
	private string  NP1_PROPOSAL;
						
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
			//recordCount = LNBF_BENEFICIARYDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
            if (SessionObject.Get("PPR_PRODCD").ToString() == "075")
            {
                txtNBF_PERCNTAGE.Enabled = false;// .Style.Add("Enabled", "false");
            }
            else
            {
                txtNBF_PERCNTAGE.Enabled = true;
                //txtNBF_PERCNTAGE.Style.Add("Enabled","true");
            }
			IDataReader LCRL_RELATIONReader0 = LCRL_RELATIONDB.GetDDL_ILUS_BENEFECIARY_CRL_RELEATIOCD_RO();;
			ddlCRL_RELEATIOCD.DataSource = LCRL_RELATIONReader0 ;
			ddlCRL_RELEATIOCD.DataBind();
			LCRL_RELATIONReader0.Close();
			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_BENEFECIARY_NBF_BASIS_RO();;
			ddlNBF_BASIS.DataSource = LCSD_SYSTEMDTLReader1 ;
			ddlNBF_BASIS.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("LNBF_BENEFICIARY");
			IDataReader LNBF_BENEFICIARYReader= LNBF_BENEFICIARYDB.GetILUS_BENEFECIARY_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			//recordSelected = IsRecordSelected(LNBF_BENEFICIARYReader, LNBF_BENEFICIARY.PrimaryKeys, "LNBF_BENEFICIARY");
			//LNBF_BENEFICIARYReader= LNBF_BENEFICIARYDB.GetILUS_BENEFECIARY_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(LNBF_BENEFICIARYReader, table, PAGE_SIZE, LNBF_BENEFICIARY.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(LNBF_BENEFICIARYReader, table, PAGE_SIZE, pageNumber);
			}
			LNBF_BENEFICIARYReader.Close();
			BindLister(table);							
		
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();//LoadPageStyle
			HeaderScript.Text = EnvHelper.Parse("var individualRelation=true;");
			FooterScript.Text = EnvHelper.Parse("function setDefaultValues(){getField(\"NBF_BENEFCD\").value = '0';} ") ;
				
			txtNBF_DOB.Attributes.Add("onblur" ,"setDOB(this, '" + SessionObject.Get("NP2_COMMENDATE") + "');setCNICField();");
			txtNBF_PERCNTAGE.Attributes.Add("onblur" ,"validatePecentage(this);");
			ddlCRL_RELEATIOCD.Attributes.Add("onblur" ,"validatePecentage(this);");
			ddlCRL_RELEATIOCD.Attributes.Add("onchange" ,"getRelationType(this);");
			
		
			/************** Array Data Script **************/
			
			
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			pagerList.SelectedIndex = pageNumber-1;		
			
			SetLastEvent();
			SetListerVisibility();
			btnGuardian.Visible = false;
		}
	#endregion
    
	#region Major methods of the final step
		protected override void ValidateRequest() {
			base.ValidateRequest();									
			foreach (string key in LNBF_BENEFICIARY.PrimaryKeys){
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
			//recordCount = LNBF_BENEFICIARYDB.RecordCount;
			return dataHolder;
		}      
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;

			GuardinaLiteral.Text = "";

			shgn.SHGNCommand entityClass=new ace.ILUS_ST_BENEFICIARY();
			entityClass.setNameValueCollection(columnNameValue);
			
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
			case (EnumControlArgs.Save):
				_lastEvent.Text = "Save";
				DB.BeginTransaction();
				SaveTransaction = true;
				txtNBF_BENEFCD.Text = "0";
				dataHolder = new LNBF_BENEFICIARYDB(dataHolder).FindByPK(txtNBF_BENEFCD.Text,txtNP1_PROPOSAL.Text);
				columnNameValue.Add("NBF_BENNAME",txtNBF_BENNAME.Text.Trim()==""?null:txtNBF_BENNAME.Text);
				columnNameValue.Add("NBF_BENNAMEARABIC",null);
				columnNameValue.Add("NBF_DOB",txtNBF_DOB.Text.Trim()==""?null:(object)DateTime.Parse(txtNBF_DOB.Text));
				columnNameValue.Add("NBF_AGE",txtNBF_AGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_AGE.Text));
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NBF_BASIS",ddlNBF_BASIS.SelectedValue.Trim()==""?null:ddlNBF_BASIS.SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",txtNBF_AMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNBF_AMOUNT.Text));
				columnNameValue.Add("NBF_PERCNTAGE",txtNBF_PERCNTAGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_PERCNTAGE.Text));
				columnNameValue.Add("NBF_BENEFCD",txtNBF_BENEFCD.Text.Trim()==""?null:txtNBF_BENEFCD.Text);
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("NBF_IDNO",txtNBF_IDNO.Text.Trim()==""?null:txtNBF_IDNO.Text);
								
				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
				if (security.SaveAllowed){
					entityClass.fsoperationBeforeSave();

					new LNBF_BENEFICIARY(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_BENEFECIARY",null);

					dataHolder.Update(DB.Transaction);

                    try
                    {
                        entityClass.fsoperationAfterSave();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Total Percentage is exceeding from 100%');</script>");
                        DB.RollbackTransaction();
                    }


					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNBF_BENEFICIARY");
					_lastEvent.Text = "Save"; 
					
					//****** Ask Guardian - For an Individual only ******
					if(ace.clsIlasUtility.isIndividual_Relation(Convert.ToString(columnNameValue["CRL_RELEATIOCD"])))
					{
						if(ace.clsIlasUtility.askGuardianInfo() == true)
						{
							if(Convert.ToInt16(columnNameValue["NBF_AGE"]) < ace.clsIlasConstant.AGE_LIMIT)
							{
								string loadGuardian = "openGuardian('" + Convert.ToString(columnNameValue["NP1_PROPOSAL"]) + "', " + Convert.ToString(columnNameValue["NBF_BENEFCD"]) + ",-999);";
								GuardinaLiteral.Text = loadGuardian;
							}
						}
					}
					//PrintMessage("Record has been saved");
				}
				else{
					//PrintMessage("You are not autherized to Save.");
				}
				break;
			case (EnumControlArgs.Update):					
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNBF_BENEFICIARYDB(dataHolder).FindByPK(txtNBF_BENEFCD.Text,txtNP1_PROPOSAL.Text);				
				columnNameValue.Add("NBF_BENNAME",txtNBF_BENNAME.Text.Trim()==""?null:txtNBF_BENNAME.Text);
				columnNameValue.Add("NBF_BENNAMEARABIC",null);
				columnNameValue.Add("NBF_DOB",txtNBF_DOB.Text.Trim()==""?null:(object)DateTime.Parse(txtNBF_DOB.Text));
				columnNameValue.Add("NBF_AGE",txtNBF_AGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_AGE.Text));
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NBF_BASIS",ddlNBF_BASIS.SelectedValue.Trim()==""?null:ddlNBF_BASIS.SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",txtNBF_AMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNBF_AMOUNT.Text));
				columnNameValue.Add("NBF_PERCNTAGE",txtNBF_PERCNTAGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_PERCNTAGE.Text));
				columnNameValue.Add("NBF_BENEFCD",txtNBF_BENEFCD.Text.Trim()==""?null:txtNBF_BENEFCD.Text);
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("NBF_IDNO",txtNBF_IDNO.Text.Trim()==""?null:txtNBF_IDNO.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
				if (security.UpdateAllowed){
					
					new LNBF_BENEFICIARY(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

                    try
                    {
                        entityClass.fsoperationAfterSave();
                        dataHolder.Update(DB.Transaction);
                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNBF_BENEFICIARY");
                        recordSelected = true;
                        PrintMessage("Record has been updated");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Total Percentage is exceeding from 100%');</script>");
                        DB.RollbackTransaction();
                    }
				}
				else{
					PrintMessage("You are not autherized to Update.");
				}
				break;
			case (EnumControlArgs.Delete):
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNBF_BENEFICIARYDB(dataHolder).FindByPK(txtNBF_BENEFCD.Text,txtNP1_PROPOSAL.Text);				
				columnNameValue.Add("NBF_BENNAME",txtNBF_BENNAME.Text.Trim()==""?null:txtNBF_BENNAME.Text);
				columnNameValue.Add("NBF_BENNAMEARABIC",null);
				columnNameValue.Add("NBF_DOB",txtNBF_DOB.Text.Trim()==""?null:(object)DateTime.Parse(txtNBF_DOB.Text));
				columnNameValue.Add("NBF_AGE",txtNBF_AGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_AGE.Text));
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NBF_BASIS",ddlNBF_BASIS.SelectedValue.Trim()==""?null:ddlNBF_BASIS.SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",txtNBF_AMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNBF_AMOUNT.Text));
				columnNameValue.Add("NBF_PERCNTAGE",txtNBF_PERCNTAGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_PERCNTAGE.Text));
				columnNameValue.Add("NBF_BENEFCD",txtNBF_BENEFCD.Text.Trim()==""?null:txtNBF_BENEFCD.Text);
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
				if (security.DeleteAllowed){
				
				new LNBF_BENEFICIARY(dataHolder).Delete(columnNameValue);

				dataHolder.Update(DB.Transaction);
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNBF_BENEFICIARY");
				PrintMessage("Record has been deleted");				
				}
				else{
					PrintMessage("You are not autherized to Delete.");
				}

				break;
			case (EnumControlArgs.Process):						
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LNBF_BENEFICIARYDB(dataHolder).FindByPK(txtNBF_BENEFCD.Text,txtNP1_PROPOSAL.Text);				
				columnNameValue.Add("NBF_BENNAME",txtNBF_BENNAME.Text.Trim()==""?null:txtNBF_BENNAME.Text);
				columnNameValue.Add("NBF_BENNAMEARABIC",null);
				columnNameValue.Add("NBF_DOB",txtNBF_DOB.Text.Trim()==""?null:(object)DateTime.Parse(txtNBF_DOB.Text));
				columnNameValue.Add("NBF_AGE",txtNBF_AGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_AGE.Text));
				columnNameValue.Add("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue.Trim()==""?null:ddlCRL_RELEATIOCD.SelectedValue);
				columnNameValue.Add("NBF_BASIS",ddlNBF_BASIS.SelectedValue.Trim()==""?null:ddlNBF_BASIS.SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",txtNBF_AMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNBF_AMOUNT.Text));
				columnNameValue.Add("NBF_PERCNTAGE",txtNBF_PERCNTAGE.Text.Trim()==""?null:(object)double.Parse(txtNBF_PERCNTAGE.Text));
				columnNameValue.Add("NBF_BENEFCD",txtNBF_BENEFCD.Text.Trim()==""?null:txtNBF_BENEFCD.Text);
				columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
				columnNameValue.Add("NBF_IDNO",txtNBF_IDNO.Text.Trim()==""?null:txtNBF_IDNO.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
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
							proccessCommand.setPrimaryKeys(LNBF_BENEFICIARY.PrimaryKeys);
							proccessCommand.setTableName("LNBF_BENEFICIARY");
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
			
		  	
			
			LNBF_BENEFICIARYDB LNBF_BENEFICIARYDB_obj = new LNBF_BENEFICIARYDB(dataHolder);		
			IDataReader LNBF_BENEFICIARYReader;
			DataTable table = new DataTable("LNBF_BENEFICIARY") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit){
				DataRow row = LNBF_BENEFICIARYDB_obj.FindByPK(NBF_BENEFCD,NP1_PROPOSAL)["LNBF_BENEFICIARY"].Rows[0];
				ShowData(row);
			}		
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter){
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					LNBF_BENEFICIARYReader = LNBF_BENEFICIARYDB.GetILUS_BENEFECIARY_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();				
				else
					LNBF_BENEFICIARYReader = LNBF_BENEFICIARYDB.GetILUS_BENEFECIARY_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());
				recordCount = Utilities.Reader2Table(LNBF_BENEFICIARYReader, table, PAGE_SIZE, pageNumber);
				LNBF_BENEFICIARYReader.Close();
	
				BindLister(table);
                        if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
				RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNBF_BENEFICIARY"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString()){
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
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

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			if(this.blnIndividualReltion == false) 
			{
				HeaderScript.Text = EnvHelper.Parse("var individualRelation=false;");
			}
			else
			{
				HeaderScript.Text = EnvHelper.Parse("var individualRelation=true;");
			}
			FooterScript.Text = EnvHelper.Parse("function setDefaultValues(){getField(\"NBF_BENEFCD\").value = '0';}");

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
					NBF_BENEFCD=((LinkButton)e.Item.FindControl("linkNBF_BENEFCD1")).Text;
NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL1")).Text;

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem){
					NBF_BENEFCD=((LinkButton)e.Item.FindControl("linkNBF_BENEFCD2")).Text;
NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL2")).Text;
	
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
			LinkButton linkNBF_BENEFCD = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item){
				linkNBF_BENEFCD = (LinkButton)e.Item.FindControl("linkNBF_BENEFCD1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem){
				linkNBF_BENEFCD=(LinkButton)e.Item.FindControl("linkNBF_BENEFCD2");	
			}			
			tRow.Attributes.Add("onclick", linkNBF_BENEFCD.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			//Stop  by Rehan it clears session values 24/04/2006
			
			if (SetFieldsInSession())
			{
				SessionObject.Set("NBF_BENNAME",txtNBF_BENNAME.Text);
				SessionObject.Set("NBF_DOB",txtNBF_DOB.Text);
				SessionObject.Set("NBF_AGE",txtNBF_AGE.Text);
				SessionObject.Set("CRL_RELEATIOCD",ddlCRL_RELEATIOCD.SelectedValue);
				SessionObject.Set("NBF_BASIS",ddlNBF_BASIS.SelectedValue);
				SessionObject.Set("NBF_AMOUNT",txtNBF_AMOUNT.Text);
				SessionObject.Set("NBF_PERCNTAGE",txtNBF_PERCNTAGE.Text);
				SessionObject.Set("NBF_BENEFCD",txtNBF_BENEFCD.Text);
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
			if (SessionObject.Get("NP1_PROPOSAL")==null  || SessionObject.GetString("NP1_PROPOSAL")== "" ){	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				txtNP1_PROPOSAL.Text=SessionObject.GetString("NP1_PROPOSAL");




				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		
		
		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			txtNBF_BENNAME.Text="";
			txtNBF_BENNAMEARABIC.Text="";
txtNBF_DOB.Text="";
txtNBF_AGE.Text="0";
ddlCRL_RELEATIOCD.ClearSelection();
ddlNBF_BASIS.ClearSelection();
txtNBF_AMOUNT.Text="0";
txtNBF_PERCNTAGE.Text="0";

txtNBF_BENEFCD.Enabled = true;
txtNBF_BENEFCD.Text="";
//*//txtNP1_PROPOSAL.Text="";

		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtNBF_BENNAME.Text=objRow["NBF_BENNAME"].ToString();
			this.blnIndividualReltion = ace.clsIlasUtility.isIndividual_Relation(objRow["CRL_RELEATIOCD"].ToString());
			if(this.blnIndividualReltion == false)
			{
				txtNBF_DOB.Text="";
				txtNBF_AGE.Text="";
				txtNBF_DOB.Enabled = false;
				txtNBF_AGE.Enabled = false;
				//txtNBF_DOB.BackColor = System.Drawing.Color.Silver;
				//txtNBF_AGE.BackColor = System.Drawing.Color.Silver;
			}
			else
			{
				txtNBF_DOB.Enabled = true;
				txtNBF_AGE.Enabled = true;
				//txtNBF_DOB.BackColor = System.Drawing.Color.White;
				//txtNBF_AGE.BackColor = System.Drawing.Color.White;
				txtNBF_DOB.Text=objRow["NBF_DOB"]==DBNull.Value?"":((DateTime)objRow["NBF_DOB"]).ToShortDateString();
				txtNBF_AGE.Text=objRow["NBF_AGE"].ToString();
			}

			btnGuardian.Visible = false;
			if(objRow["NBF_AGE"] != null &&  objRow["NBF_AGE"] != System.DBNull.Value )
			{
				if(Convert.ToInt16(objRow["NBF_AGE"]) < 18)
				{
					btnGuardian.Visible = true;
				}
			}
			ddlCRL_RELEATIOCD.ClearSelection();
			ListItem item3=ddlCRL_RELEATIOCD.Items.FindByValue(objRow["CRL_RELEATIOCD"].ToString());
			
			if (item3!= null)
			{
				item3.Selected=true;
			}
			
			ddlNBF_BASIS.ClearSelection();
			ListItem item4=ddlNBF_BASIS.Items.FindByValue(objRow["NBF_BASIS"].ToString());
			if (item4!= null)
			{
				item4.Selected=true;
			}
			
			txtNBF_AMOUNT.Text=objRow["NBF_AMOUNT"].ToString();
			txtNBF_PERCNTAGE.Text=objRow["NBF_PERCNTAGE"].ToString();
			txtNBF_BENEFCD.Text=objRow["NBF_BENEFCD"].ToString();
			txtNBF_BENEFCD.Enabled=false;
			txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP1_PROPOSAL.Enabled=false;
			txtNBF_IDNO.Text = objRow["NBF_IDNO"].ToString();

			try
			{
				rowset rsArabic = DB.executeQuery("SELECT NBF_BENNAMEARABIC FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + objRow["NP1_PROPOSAL"].ToString() + "' AND NBF_BENEFCD='" + objRow["NBF_BENEFCD"].ToString() + "'");
				string FULLNAMEARABIC=null;
				if(rsArabic.next())
				{
					FULLNAMEARABIC =  System.Text.Encoding.UTF8.GetString(Convert.FromBase64String((rsArabic.getObject("NBF_BENNAMEARABIC")==null?"":rsArabic.getString("NBF_BENNAMEARABIC"))));
				}
				txtNBF_BENNAMEARABIC.Text=Convert.ToString(FULLNAMEARABIC);
			}
			catch(Exception e)
			{
				//goto: hell
			}

			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, "LNBF_BENEFICIARY");
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
			foreach (string pk in LNBF_BENEFICIARY.PrimaryKeys){
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
			foreach(string pk in  LNBF_BENEFICIARY.PrimaryKeys){
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound){
				if (item.ItemType == ListItemType.Item){
					NBF_BENEFCD=((LinkButton)e.Item.FindControl("linkNBF_BENEFCD1")).Text;
NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL1")).Text;
	
				}
				else{
					NBF_BENEFCD=((LinkButton)e.Item.FindControl("linkNBF_BENEFCD2")).Text;
NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL2")).Text;
	
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new LNBF_BENEFICIARYDB(dataHolder).FindByPK(NBF_BENEFCD,NP1_PROPOSAL)["LNBF_BENEFICIARY"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm(){
			if (btnHideLister!=null)
				btnHideLister.Disabled=true;
			EntryTableDiv.Style.Add("visibility" , "hidden");
			ListerDiv.Style.Add("visibility" , "hidden");

			CSSLiteral.Text = "";
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

