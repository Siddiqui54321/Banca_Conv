using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
using shsm;

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_TARGETVALUES : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls


		//		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		//		protected System.Web.UI.WebControls.DropDownList pagerList;
		protected System.Web.UI.WebControls.Literal _lastEvent;
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_RETIREMENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_TARGETATTAINAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_TARGETRETURNYEAR;

						

		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NP1_PROPOSAL;
		private double  NP2_SETNO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_EXCESSPREMIUM;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		private string  PPR_PRODCD;
						
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
		
		private void InitializeComponent() 
		{

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
			//CheckKeyLevel();
			//recordCount = LNPR_PRODUCTDB.RecordCount;
			return   dataHolder;         

		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{

			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_TARGETVALUES_NPR_INCLUDELOADINNIV_RO();;
			ddlNPR_INCLUDELOADINNIV.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPR_INCLUDELOADINNIV.DataBind();
			LCSD_SYSTEMDTLReader0.Close();
			IDataReader PCU_CURRENCYReader1 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_TARGETVALUES_PCU_AVCURRCODE_RO();;
			ddlPCU_AVCURRCODE.DataSource = PCU_CURRENCYReader1 ;
			ddlPCU_AVCURRCODE.DataBind();
			PCU_CURRENCYReader1.Close();

			_lastEvent.Text = "New";		

		
			//SetSessionValues();

			FindAndSelectCurrentRecord();
			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;
			

			txtNPR_EXCESSPREMIUM.Attributes.Add("onblur","applyNumberFormat(this,2);");

			
		
		
		

			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);

		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPR_PRODUCT.PrimaryKeys)
			{
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null)
				{
					if (ctrl is WebControl)
					{
						//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null))
						{
							control.Enabled = true;
						}				
					}
				}
			}			
		}
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
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

					//((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text = "R/07/0010042";
					//((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text = "1";

					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),txtPPR_PRODCD.Text);
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESSPREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESSPREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.SaveAllowed)
					{
				
						new LNPR_PRODUCT(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_TARGETVALUES",null);

						dataHolder.Update(DB.Transaction);
				
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPR_PRODUCT");
						_lastEvent.Text = "Save"; 					
						//PrintMessage("Record has been saved");
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;

					SetSessionValues();
//					txtNP1_PROPOSAL.Text = ""+SessionObject.Get("NP1_PROPOSAL");//"R/07/0010042";
//					txtNP2_SETNO.Text = "1";
//					txtPPR_PRODCD.Text = "450";
//					NP2_SETNO=double.Parse(SessionObject.GetString("NP2_SETNO"));
//					PPR_PRODCD=SessionObject.GetString("PPR_PRODCD");

					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),txtPPR_PRODCD.Text);				
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESSPREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESSPREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);
//					columnNameValue.Add("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_RETIREMENTAGE.Text));
					
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.UpdateAllowed)
					{
				
						new LNPR_PRODUCT(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);

						NameValueCollection columnNameValueNonBase=new NameValueCollection();
						dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
						columnNameValueNonBase.Add("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_RETIREMENTAGE.Text));
						columnNameValueNonBase.Add("NP1_TARGETATTAINAGE",txtNP1_TARGETATTAINAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_TARGETATTAINAGE.Text));
						columnNameValueNonBase.Add("NP1_TARGETRETURNYEAR",txtNP1_TARGETRETURNYEAR.Text.Trim()==""?null:(object)double.Parse(txtNP1_TARGETRETURNYEAR.Text));
						columnNameValueNonBase.Add("PCU_AVCURRCODE",ddlPCU_AVCURRCODE.SelectedValue.Trim()==""?null:ddlPCU_AVCURRCODE.SelectedValue);
						columnNameValueNonBase.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
						new LNP1_POLICYMASTR(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValueNonBase);
						dataHolder.Update(DB.Transaction);

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPR_PRODUCT");
						//recordSelected = true;
						//PrintMessage("Record has been updated");
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),txtPPR_PRODCD.Text);				
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESSPREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESSPREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.DeleteAllowed)
					{
			
						new LNPR_PRODUCT(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
			
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPR_PRODUCT");
						//PrintMessage("Record has been deleted");				
					}
					else
					{
						PrintMessage("You are not autherized to Delete.");
					}
					break;
				case (EnumControlArgs.Process):						
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),txtPPR_PRODCD.Text);				
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESSPREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESSPREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("PPR_PRODCD",txtPPR_PRODCD.Text.Trim()==""?null:txtPPR_PRODCD.Text);

					columnNameValue.Add("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_RETIREMENTAGE.Text));
					columnNameValue.Add("NP1_TARGETATTAINAGE",txtNP1_TARGETATTAINAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_TARGETATTAINAGE.Text));
					columnNameValue.Add("NP1_TARGETRETURNYEAR",txtNP1_TARGETRETURNYEAR.Text.Trim()==""?null:(object)double.Parse(txtNP1_TARGETRETURNYEAR.Text));
					

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					string result="";					
					if (_CustomArgName.Value == "ProcessName")
					{
						string processName = _CustomArgVal.Value;	
						if (security.ProcessAllowed(processName))
						{
							Type type = Type.GetType(processName);											
							if (type != null)
							{
								shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
								NameValueCollection[] dataRows = new NameValueCollection[1];
								bool[] SelectedRowIndexes = new bool[1];
								dataRows[0] = columnNameValue;
								SelectedRowIndexes[0] = true;
								proccessCommand.setAllFields(columnNameValue);
								proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
								proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
								proccessCommand.setTableName("LNPR_PRODUCT");
								proccessCommand.setDataRows(dataRows);
								proccessCommand.setSelectedRows(SelectedRowIndexes);
								
								try
								{
									result = proccessCommand.processing();
									FooterScript.Text = EnvHelper.Parse("parent.frames[2].location = parent.frames[2].location;") ;

								}
								catch (Exception exp)
								{
									result = exp.Message;
								}
								//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
							}
						}
						else
						{
							result = "You are not Autherized to Execute Process.";
						}						
					}	
					//recordSelected =true;
					if (result.Length>0)
						PrintCustomMessage(result);
					break;
			}
		}
	
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			LNPR_PRODUCTDB LNPR_PRODUCTDB_obj = new LNPR_PRODUCTDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				DataRow row = LNPR_PRODUCTDB_obj.FindByPK(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNPR_PRODUCT"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else
				{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else
				{
					_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();			
				}
			}
			//for header & footer script					
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	

			HeaderScript.Text = EnvHelper.Parse("");
			String FScript = FooterScript.Text ;
			FooterScript.Text = EnvHelper.Parse(FScript);

		
		}
		#endregion	

		#region Events
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
		{
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value)
			{
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
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			if (SetFieldsInSession())
			{
				SessionObject.Set("NPR_SUMASSURED",txtNPR_SUMASSURED.Text);
				SessionObject.Set("NPR_TOTPREM",txtNPR_TOTPREM.Text);
				SessionObject.Set("NPR_EXCESSPREMIUM",txtNPR_EXCESSPREMIUM.Text);
				SessionObject.Set("NP1_PERIODICPREM",txtNP1_PERIODICPREM.Text);
				SessionObject.Set("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue);
				SessionObject.Set("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text);
				SessionObject.Set("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text);
				SessionObject.Set("PCU_AVCURRCODE",ddlPCU_AVCURRCODE.SelectedValue);
				SessionObject.Set("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text);
				SessionObject.Set("NP1_TARGETATTAINAGE",txtNP1_TARGETATTAINAGE.Text);
				SessionObject.Set("NP1_TARGETRETURNYEAR",txtNP1_TARGETRETURNYEAR.Text);
				SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
				SessionObject.Set("NP2_SETNO",txtNP2_SETNO.Text);
				SessionObject.Set("PPR_PRODCD",txtPPR_PRODCD.Text);
		
			}
		}										
	
		#endregion 

		protected override bool TransactionRequired 
		{
			get 
			{
				return true;
			}
		}


		private void GetSessionValues()
		{
			if (false)
			{	
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
			txtNPR_SUMASSURED.Text="0";
			txtNPR_TOTPREM.Text="0";
			txtNPR_EXCESSPREMIUM.Text="";
			txtNP1_PERIODICPREM.Text="0";
			ddlNPR_INCLUDELOADINNIV.ClearSelection();
			txtNPR_PAIDUPTOAGE.Text="";
			txtNPR_PREMIUMTER.Text="";
			ddlPCU_AVCURRCODE.ClearSelection();
			txtNP1_RETIREMENTAGE.Text="";
			txtNP1_TARGETATTAINAGE.Text="";
			txtNP1_TARGETRETURNYEAR.Text="";
			txtNP1_PROPOSAL.Enabled = true;
			txtNP1_PROPOSAL.Text="";
			txtNP2_SETNO.Enabled = true;
			txtNP2_SETNO.Text="0";
			txtPPR_PRODCD.Enabled = true;
			txtPPR_PRODCD.Text="";

		}		

		protected void ShowData(DataRow objRow)
		{
			//		RefreshDataFields();

			rowset rsCurr = DB.executeQuery("select a.PCU_CURRCODE, PCU_CURRDESC from  LNPR_PRODUCT a, PCU_CURRENCY b where a.PCU_CURRCODE=b.PCU_CURRCODE and a.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"'");
			if (rsCurr.next())
				txtPCU_CURRDESC.Text = rsCurr.getString(2);
			else
				txtPCU_CURRDESC.Text = "";
			txtPCU_CURRDESC.ReadOnly = true;

			txtNPR_BENEFITTERM.Text  = Convert.ToString(objRow["NPR_BENEFITTERM"]);
			txtNPR_PREMIUM.Text  = Convert.ToString(objRow["NPR_PREMIUM"]);
			
			DataRow selectedRowLNP1 = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
			txtNP1_RETIREMENTAGE.Text=selectedRowLNP1["NP1_RETIREMENTAGE"].ToString();
			txtNP1_TARGETATTAINAGE.Text=selectedRowLNP1["NP1_TARGETATTAINAGE"].ToString();
			txtNP1_TARGETRETURNYEAR.Text=selectedRowLNP1["NP1_TARGETRETURNYEAR"].ToString();
			
			ddlPCU_AVCURRCODE.ClearSelection();
			ListItem item3=ddlPCU_AVCURRCODE.Items.FindByValue(selectedRowLNP1["PCU_AVCURRCODE"].ToString());
			if (item3!= null)
			{
				item3.Selected=true;
			}

			String	item4= Convert.ToString(objRow["NPR_PREMIUM"]);
			String	item5= Convert.ToString(objRow["NPR_EXCESSPREMIUM"]);
			//Commented the mechanism from session, picking right away from the DB.
			//String	item6=Convert.ToString(SessionObject.GetString("total_NPR_PREMIUM"));
			String	item6=Convert.ToString(ace.Ace_General.getPremium(""+SessionObject.Get("NP1_PROPOSAL")));

			double val1=0;
			val1=item4==String.Empty?0:Double.Parse(item4) ;
			val1+=item5==String.Empty?0:Double.Parse(item5);
			txtNP1_PERIODICPREM.Text=val1.ToString();

			val1+=item6==String.Empty?0:Double.Parse(item6);
			txtNPR_TOTPREM.Text=val1.ToString();

			txtNPR_TOTPREM.Text = item6;


			


			//double.Parse(objRow["NPR_PREMIUM"].ToString());


			txtNPR_SUMASSURED.Text=objRow["NPR_SUMASSURED"].ToString();
			txtNPR_EXCESSPREMIUM.Text=objRow["NPR_EXCESSPREMIUM"].ToString();
			ddlNPR_INCLUDELOADINNIV.ClearSelection();
			ListItem item2=ddlNPR_INCLUDELOADINNIV.Items.FindByValue(objRow["NPR_INCLUDELOADINNIV"].ToString());
			if (item2!= null)
			{
				item2.Selected=true;
			}txtNPR_PAIDUPTOAGE.Text=objRow["NPR_PAIDUPTOAGE"].ToString();
			txtNPR_PREMIUMTER.Text=objRow["NPR_PREMIUMTER"].ToString();
			txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP1_PROPOSAL.Enabled=false;
			txtNP2_SETNO.Text=objRow["NP2_SETNO"].ToString();
			txtNP2_SETNO.Enabled=false;
			txtPPR_PRODCD.Text=objRow["PPR_PRODCD"].ToString();
			txtPPR_PRODCD.Enabled=false;

			SetSessionValues();

			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);

			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
			foreach(string processName in AllProcess)
			{
				if (security.ProcessAllowed(processName))
				{
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed)
			{
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
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


		protected void PrintCustomMessage(string message)
		{
			//With Message
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
			MessageScript.Text += "; _callback();";
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

		private NameValueCollection getAllFields() 
		{
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) 
			{
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}

			foreach (Control c in this.myForm.Controls) 
			{	
				string _fieldName="";
				if (c is WebControl) 
				{
					switch (c.GetType().ToString()) 
					{
						case "System.Web.UI.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName))
							{ 
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}
		bool IsRecordSelected()
		{
			bool selected = true;
			foreach (string pk in LNPR_PRODUCT.PrimaryKeys)
			{
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0)
				{
					selected  = false;
				}				
			}
			return selected ;
		}

		//Code By Rehan Hasan 24-May 2008
		private void SetSessionValues()
		{
			//string strNP1_PROPOSAL= ""+SessionObject.Get("NP1_PROPOSAL");//"R/07/0010042";
			//double dblNP2_SETNO=double.Parse(SessionObject.GetString("NP2_SETNO"));
			//string strPPR_PRODCD="" + SessionObject.GetString("PPR_PRODCD");
			string strNPR_EXCESSPREMIUM=txtNPR_EXCESSPREMIUM.Text;
			SessionObject.Set("NPR_EXCESSPREMIUM",strNPR_EXCESSPREMIUM);
			//SessionObject.Set("NP2_SETNO",dblNP2_SETNO);
			//SessionObject.Set("PPR_PRODCD",strPPR_PRODCD);
			
		}
		//---------------------------		

		private void FindAndSelectCurrentRecord()
		{
			try 
			{
				if (IsRecordSelected())
				{
					NP1_PROPOSAL=SessionObject.GetString("NP1_PROPOSAL");
					NP2_SETNO=double.Parse(SessionObject.GetString("NP2_SETNO"));
					PPR_PRODCD=SessionObject.GetString("PPR_PRODCD");
			
					DataRow selectedRow = new LNPR_PRODUCTDB(dataHolder).FindByPK(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
					ShowData(selectedRow);							
					_lastEvent.Text = "Edit";
				}
			}
			catch(Exception e)
			{
				//e.printStackTrace();
				throw new ProcessException (e.Message );
			}
		}

		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields
		{
			get
			{	
				return NormalEntryTableDiv.Controls;
			}
		}

	}
}

