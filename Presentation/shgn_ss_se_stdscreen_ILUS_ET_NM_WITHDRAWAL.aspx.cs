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
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_WITHDRAWAL : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		private int pageNumber=1;
		int PAGE_SIZE= SHMA.Enterprise.Configuration.AppSettings.GetInt("NoOfListerRows") ;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		
		shgn.SHGNCommand entityClass;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPW_ADHOCEXCESPRM;
				
				
		#region pk variables declaration		
		private double  NPW_YEAR;
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
		
		private void InitializeComponent()
		{
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
			//recordCount = LNPW_PARTIALWITHDRAWALDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
	
			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_PURPOSE_RO();;
			ddlNPW_PURPOSE.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPW_PURPOSE.DataBind();
			LCSD_SYSTEMDTLReader0.Close();
			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQUIREDFOR_RO();;
			ddlNPW_REQUIREDFOR.DataSource = LCSD_SYSTEMDTLReader1 ;
			ddlNPW_REQUIREDFOR.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			IDataReader LNBF_BENEFICIARYReader2 = LNBF_BENEFICIARYDB.GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_REQIREDFORCD_RO();;
			ddlNPW_REQIREDFORCD.DataSource = LNBF_BENEFICIARYReader2 ;
			ddlNPW_REQIREDFORCD.DataBind();
			LNBF_BENEFICIARYReader2.Close();
			IDataReader LCSD_SYSTEMDTLReader3 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_WITHDRAWAL_NPW_DEATHBENEFITOPTION_RO();;
			ddlNPW_DEATHBENEFITOPTION.DataSource = LCSD_SYSTEMDTLReader3 ;
			ddlNPW_DEATHBENEFITOPTION.DataBind();
			LCSD_SYSTEMDTLReader3.Close();
			
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("LNPW_PARTIALWITHDRAWAL");
			IDataReader LNPW_PARTIALWITHDRAWALReader= LNPW_PARTIALWITHDRAWALDB.GetILUS_ET_NM_WITHDRAWAL_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(LNPW_PARTIALWITHDRAWALReader, table, PAGE_SIZE, LNPW_PARTIALWITHDRAWAL.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(LNPW_PARTIALWITHDRAWALReader, table, PAGE_SIZE, pageNumber);
			}
			LNPW_PARTIALWITHDRAWALReader.Close();
			BindLister(table);							
		

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;
				
			ddlNPW_REQUIREDFOR.Attributes.Add("onchange","setName(this);");
			ddlNPW_REQIREDFORCD.Attributes.Add("onchange","fetchDOB('" + SessionObject.Get("NP2_COMMENDATE") + "');");
		
			txtNPW_PW.Attributes.Add("onblur","applyNumberFormat(this,2);");
			txtNPW_ALLOWAMOUNT.Attributes.Add("onblur","applyNumberFormat(this,2);");
			txtNPW_ADHOCEXCESPRM.Attributes.Add("onblur","applyNumberFormat(this,2);");
		
			/************** Array Data Script **************/
			
			
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			pagerList.SelectedIndex = pageNumber-1;		
			
			SetLastEvent();
			SetListerVisibility();
		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPW_PARTIALWITHDRAWAL.PrimaryKeys)
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
		sealed protected override DataHolder GetData(DataHolder dataHolder) 
		{	
			pageNumber = pagerList.SelectedIndex +1;
			//recordCount = LNPW_PARTIALWITHDRAWALDB.RecordCount;
			return dataHolder;
		}      
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			entityClass=new ace.ILUS_ET_NM_WITHDRAWAL();
			entityClass.setNameValueCollection(columnNameValue);

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;


					//TODO: NP1_PROPOSAL = pick value from session
					//TODO: Generate key for year? NPW_YEAR

					txtNP1_PROPOSAL.Text = ""+SessionObject.Get("NP1_PROPOSAL");//"R/07/0010042";
					
					dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNPW_YEAR.Text));
					columnNameValue.Add("NPW_YEAR",txtNPW_YEAR.Text.Trim()==""?null:(object)double.Parse(txtNPW_YEAR.Text));
					columnNameValue.Add("NPW_PW",txtNPW_PW.Text.Trim()==""?null:(object)double.Parse(txtNPW_PW.Text));
					columnNameValue.Add("NPW_PURPOSE",ddlNPW_PURPOSE.SelectedValue.Trim()==""?null:ddlNPW_PURPOSE.SelectedValue);
					columnNameValue.Add("NPW_REQUIREDFOR",ddlNPW_REQUIREDFOR.SelectedValue.Trim()==""?null:ddlNPW_REQUIREDFOR.SelectedValue);
					columnNameValue.Add("NPW_REQIREDFORCD",ddlNPW_REQIREDFORCD.SelectedValue.Trim()==""?null:(object)double.Parse(ddlNPW_REQIREDFORCD.SelectedValue));
					columnNameValue.Add("NPW_ALLOWAMOUNT",txtNPW_ALLOWAMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPW_ALLOWAMOUNT.Text));
					columnNameValue.Add("NPW_CUMWITHDRAWAL",txtNPW_CUMWITHDRAWAL.Text.Trim()==""?null:(object)double.Parse(txtNPW_CUMWITHDRAWAL.Text));
					columnNameValue.Add("NPW_ADHOCEXCESPRM",txtNPW_ADHOCEXCESPRM.Text.Trim()==""?null:(object)double.Parse(txtNPW_ADHOCEXCESPRM.Text));
					columnNameValue.Add("NPW_DEATHBENEFITOPTION",ddlNPW_DEATHBENEFITOPTION.SelectedValue.Trim()==""?null:ddlNPW_DEATHBENEFITOPTION.SelectedValue);
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");

					string qry="SELECT NPW_YEAR FROM LNPW_PARTIALWITHDRAWAL WHERE NP1_PROPOSAL ='" + txtNP1_PROPOSAL.Text + "' AND NPW_YEAR=" + txtNPW_YEAR.Text;
					rowset rowPW = DB.executeQuery(qry);
					if (rowPW.next())
						PrintMessage("The Year already exists.");	
					else
					{
						if (security.SaveAllowed)
						{
							entityClass.fsoperationBeforeSave();

							new LNPW_PARTIALWITHDRAWAL(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_WITHDRAWAL",null);

							dataHolder.Update(DB.Transaction);
							entityClass.fsoperationAfterSave();

							auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPW_PARTIALWITHDRAWAL");
							_lastEvent.Text = "Save"; 					
							//PrintMessage("Record has been saved");
						}
						else
						{
							PrintMessage("You are not autherized to Save.");
						}
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNPW_YEAR.Text));				
					columnNameValue.Add("NPW_YEAR",txtNPW_YEAR.Text.Trim()==""?null:(object)double.Parse(txtNPW_YEAR.Text));
					columnNameValue.Add("NPW_PW",txtNPW_PW.Text.Trim()==""?null:(object)double.Parse(txtNPW_PW.Text));
					columnNameValue.Add("NPW_PURPOSE",ddlNPW_PURPOSE.SelectedValue.Trim()==""?null:ddlNPW_PURPOSE.SelectedValue);
					columnNameValue.Add("NPW_REQUIREDFOR",ddlNPW_REQUIREDFOR.SelectedValue.Trim()==""?null:ddlNPW_REQUIREDFOR.SelectedValue);
					columnNameValue.Add("NPW_REQIREDFORCD",ddlNPW_REQIREDFORCD.SelectedValue.Trim()==""?null:(object)double.Parse(ddlNPW_REQIREDFORCD.SelectedValue));
					columnNameValue.Add("NPW_ALLOWAMOUNT",txtNPW_ALLOWAMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPW_ALLOWAMOUNT.Text));
					columnNameValue.Add("NPW_CUMWITHDRAWAL",txtNPW_CUMWITHDRAWAL.Text.Trim()==""?null:(object)double.Parse(txtNPW_CUMWITHDRAWAL.Text));
					columnNameValue.Add("NPW_ADHOCEXCESPRM",txtNPW_ADHOCEXCESPRM.Text.Trim()==""?null:(object)double.Parse(txtNPW_ADHOCEXCESPRM.Text));
					columnNameValue.Add("NPW_DEATHBENEFITOPTION",ddlNPW_DEATHBENEFITOPTION.SelectedValue.Trim()==""?null:ddlNPW_DEATHBENEFITOPTION.SelectedValue);
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");
					if (security.UpdateAllowed)
					{
						entityClass.fsoperationBeforeUpdate();

						new LNPW_PARTIALWITHDRAWAL(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPW_PARTIALWITHDRAWAL");
						recordSelected = true;
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
					dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNPW_YEAR.Text));				
					columnNameValue.Add("NPW_YEAR",txtNPW_YEAR.Text.Trim()==""?null:(object)double.Parse(txtNPW_YEAR.Text));
					columnNameValue.Add("NPW_PW",txtNPW_PW.Text.Trim()==""?null:(object)double.Parse(txtNPW_PW.Text));
					columnNameValue.Add("NPW_PURPOSE",ddlNPW_PURPOSE.SelectedValue.Trim()==""?null:ddlNPW_PURPOSE.SelectedValue);
					columnNameValue.Add("NPW_REQUIREDFOR",ddlNPW_REQUIREDFOR.SelectedValue.Trim()==""?null:ddlNPW_REQUIREDFOR.SelectedValue);
					columnNameValue.Add("NPW_REQIREDFORCD",ddlNPW_REQIREDFORCD.SelectedValue.Trim()==""?null:(object)double.Parse(ddlNPW_REQIREDFORCD.SelectedValue));
					columnNameValue.Add("NPW_ALLOWAMOUNT",txtNPW_ALLOWAMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPW_ALLOWAMOUNT.Text));
					columnNameValue.Add("NPW_CUMWITHDRAWAL",txtNPW_CUMWITHDRAWAL.Text.Trim()==""?null:(object)double.Parse(txtNPW_CUMWITHDRAWAL.Text));
					columnNameValue.Add("NPW_ADHOCEXCESPRM",txtNPW_ADHOCEXCESPRM.Text.Trim()==""?null:(object)double.Parse(txtNPW_ADHOCEXCESPRM.Text));
					columnNameValue.Add("NPW_DEATHBENEFITOPTION",ddlNPW_DEATHBENEFITOPTION.SelectedValue.Trim()==""?null:ddlNPW_DEATHBENEFITOPTION.SelectedValue);
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");
					if (security.DeleteAllowed)
					{
						entityClass.fsoperationBeforeDelete();

						new LNPW_PARTIALWITHDRAWAL(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterDelete();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPW_PARTIALWITHDRAWAL");
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
					dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNPW_YEAR.Text));				
					columnNameValue.Add("NPW_YEAR",txtNPW_YEAR.Text.Trim()==""?null:(object)double.Parse(txtNPW_YEAR.Text));
					columnNameValue.Add("NPW_PW",txtNPW_PW.Text.Trim()==""?null:(object)double.Parse(txtNPW_PW.Text));
					columnNameValue.Add("NPW_PURPOSE",ddlNPW_PURPOSE.SelectedValue.Trim()==""?null:ddlNPW_PURPOSE.SelectedValue);
					columnNameValue.Add("NPW_REQUIREDFOR",ddlNPW_REQUIREDFOR.SelectedValue.Trim()==""?null:ddlNPW_REQUIREDFOR.SelectedValue);
					columnNameValue.Add("NPW_REQIREDFORCD",ddlNPW_REQIREDFORCD.SelectedValue.Trim()==""?null:(object)double.Parse(ddlNPW_REQIREDFORCD.SelectedValue));
					columnNameValue.Add("NPW_ALLOWAMOUNT",txtNPW_ALLOWAMOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPW_ALLOWAMOUNT.Text));
					columnNameValue.Add("NPW_CUMWITHDRAWAL",txtNPW_CUMWITHDRAWAL.Text.Trim()==""?null:(object)double.Parse(txtNPW_CUMWITHDRAWAL.Text));
					columnNameValue.Add("NPW_ADHOCEXCESPRM",txtNPW_ADHOCEXCESPRM.Text.Trim()==""?null:(object)double.Parse(txtNPW_ADHOCEXCESPRM.Text));
					columnNameValue.Add("NPW_DEATHBENEFITOPTION",ddlNPW_DEATHBENEFITOPTION.SelectedValue.Trim()==""?null:ddlNPW_DEATHBENEFITOPTION.SelectedValue);
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");
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
								//dataRows[0] = columnNameValue;
								dataRows[0] = getAllFields();
								SelectedRowIndexes[0] = true;
								proccessCommand.setAllFields(getAllFields());
								proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
								proccessCommand.setPrimaryKeys(LNPW_PARTIALWITHDRAWAL.PrimaryKeys);
								proccessCommand.setTableName("LNPW_PARTIALWITHDRAWAL");
								proccessCommand.setDataRows(dataRows);
								proccessCommand.setSelectedRows(SelectedRowIndexes);
								result = proccessCommand.processing();
								//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
							}
						}
						else
						{
							result = "You are not Autherized to Execute Process.";
						}						
					}	
					recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					break;
			}
		}
		
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
			{
				entityClass.fsoperationAfterComittingSave();
			}
			else if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Update)
			{
				entityClass.fsoperationAfterComittingUpdate();
			}
	
			
			LNPW_PARTIALWITHDRAWALDB LNPW_PARTIALWITHDRAWALDB_obj = new LNPW_PARTIALWITHDRAWALDB(dataHolder);		
			IDataReader LNPW_PARTIALWITHDRAWALReader;
			DataTable table = new DataTable("LNPW_PARTIALWITHDRAWAL") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				DataRow row = LNPW_PARTIALWITHDRAWALDB_obj.FindByPK(NP1_PROPOSAL,NPW_YEAR)["LNPW_PARTIALWITHDRAWAL"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter)
				{
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					LNPW_PARTIALWITHDRAWALReader = LNPW_PARTIALWITHDRAWALDB.GetILUS_ET_NM_WITHDRAWAL_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();				
				else
					LNPW_PARTIALWITHDRAWALReader = LNPW_PARTIALWITHDRAWALDB.GetILUS_ET_NM_WITHDRAWAL_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());
				recordCount = Utilities.Reader2Table(LNPW_PARTIALWITHDRAWALReader, table, PAGE_SIZE, pageNumber);
				LNPW_PARTIALWITHDRAWALReader.Close();
	
				BindLister(table);
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNPW_PARTIALWITHDRAWAL"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");
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
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) 
		{
			pageNumber = pagerList.SelectedIndex+1;
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Pager;
			DoControl();
			pagerList.SelectedIndex=pageNumber-1;
		}
		private void btnViewAll_Click(object sender, System.EventArgs e) 
		{
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Cancel  ;
			DoControl();
		}
		
		private void lister_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) 
		{
			foreach (RepeaterItem item in lister.Items)
			{
				if (item == e.Item)
				{
					((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				}
				else
				{
					if (item.ItemType == ListItemType.Item)
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerItem";
					else
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerAlterItem";
				}
			}
			if (e.CommandName == "Edit") 
			{								
				if (e.Item.ItemType==ListItemType.Item)
				{									
					NPW_YEAR=double.Parse(((LinkButton)e.Item.FindControl("linkNPW_YEAR1")).Text);
					NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL1")).Text;

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem)
				{
					NPW_YEAR=double.Parse(((LinkButton)e.Item.FindControl("linkNPW_YEAR2")).Text);
					NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL2")).Text;
	
				}
				ControlArgs=new object[1];
				ControlArgs[0]=EnumControlArgs.Edit; 
				DoControl();								
			}				
		}	
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
		private void lister_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) 
		{
			if (recordSelected)
				FindAndSelectCurrentRecord(e);
			HtmlTableRow tRow = (HtmlTableRow)e.Item.FindControl("ListerRow");
			LinkButton linkNPW_YEAR = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item)
			{
				linkNPW_YEAR = (LinkButton)e.Item.FindControl("linkNPW_YEAR1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem)
			{
				linkNPW_YEAR=(LinkButton)e.Item.FindControl("linkNPW_YEAR2");	
			}			
			tRow.Attributes.Add("onclick", linkNPW_YEAR.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			if (SetFieldsInSession())
			{
				SessionObject.Set("NPW_YEAR",txtNPW_YEAR.Text);
				SessionObject.Set("NPW_PW",txtNPW_PW.Text);
				SessionObject.Set("NPW_PURPOSE",ddlNPW_PURPOSE.SelectedValue);
				SessionObject.Set("NPW_REQUIREDFOR",ddlNPW_REQUIREDFOR.SelectedValue);
				SessionObject.Set("NPW_REQIREDFORCD",ddlNPW_REQIREDFORCD.SelectedValue);
				SessionObject.Set("NPW_ATTAINAGE",txtNPW_ATTAINAGE.Text);
				SessionObject.Set("NPW_ALLOWAMOUNT",txtNPW_ALLOWAMOUNT.Text);
				SessionObject.Set("NPW_CUMWITHDRAWAL",txtNPW_CUMWITHDRAWAL.Text);
				SessionObject.Set("NPW_ADHOCEXCESPRM",txtNPW_ADHOCEXCESPRM.Text);
				SessionObject.Set("NPW_ADHOCEPPW",txtNPW_ADHOCEPPW.Text);
				SessionObject.Set("NPW_DEATHBENEFITOPTION",ddlNPW_DEATHBENEFITOPTION.SelectedValue);
				SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
				SessionObject.Set("NBF_BENNAME",txtNBF_BENNAME.Text);
		
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
			txtNPW_YEAR.Enabled = true;
			txtNPW_YEAR.Text="0";
			txtNPW_PW.Text="";
			ddlNPW_PURPOSE.ClearSelection();
			ddlNPW_REQUIREDFOR.ClearSelection();
			ddlNPW_REQIREDFORCD.ClearSelection();
			txtNPW_ATTAINAGE.Text="0";
			txtNPW_ALLOWAMOUNT.Text="";
			txtNPW_CUMWITHDRAWAL.Text="0";
			txtNPW_ADHOCEXCESPRM.Text="";
			txtNPW_ADHOCEPPW.Text="0";
			ddlNPW_DEATHBENEFITOPTION.ClearSelection();
			txtNP1_PROPOSAL.Enabled = true;
			txtNP1_PROPOSAL.Text="";
			txtNBF_BENNAME.Text="";

		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();


			rowset rowLNPW_PARTIALWITHDRAWAL = DB.executeQuery("select NPW_ADHOCEXCESPRM, NPW_PW FROM LNPW_PARTIALWITHDRAWAL WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"' AND NPW_YEAR="+NPW_YEAR);
			if (rowLNPW_PARTIALWITHDRAWAL.next())
			{
				String	strNPW_ADHOCEXCESPRM= rowLNPW_PARTIALWITHDRAWAL.getDouble("NPW_ADHOCEXCESPRM").ToString();
				String	strNPW_PW= rowLNPW_PARTIALWITHDRAWAL.getDouble("NPW_PW").ToString();

				double val1=0;
				val1=strNPW_ADHOCEXCESPRM==String.Empty?0:Double.Parse(strNPW_ADHOCEXCESPRM) ;
				val1-=strNPW_PW==String.Empty?0:Double.Parse(strNPW_PW);
				txtNPW_ADHOCEPPW.Text=val1.ToString();
			}


			txtNPW_YEAR.Text=objRow["NPW_YEAR"].ToString();
			txtNPW_YEAR.Enabled=true;
			txtNPW_PW.Text=objRow["NPW_PW"].ToString();
			ddlNPW_PURPOSE.ClearSelection();
			ListItem item2=ddlNPW_PURPOSE.Items.FindByValue(objRow["NPW_PURPOSE"].ToString());
			if (item2!= null)
			{
				item2.Selected=true;
			}ddlNPW_REQUIREDFOR.ClearSelection();
			ListItem item3=ddlNPW_REQUIREDFOR.Items.FindByValue(objRow["NPW_REQUIREDFOR"].ToString());
			if (item3!= null)
			{
				item3.Selected=true;
			}ddlNPW_REQIREDFORCD.ClearSelection();
			ListItem item4=ddlNPW_REQIREDFORCD.Items.FindByValue(objRow["NPW_REQIREDFORCD"].ToString());
			if (item4!= null)
			{
				item4.Selected=true;
			}txtNPW_ALLOWAMOUNT.Text=objRow["NPW_ALLOWAMOUNT"].ToString();
			txtNPW_CUMWITHDRAWAL.Text=objRow["NPW_CUMWITHDRAWAL"].ToString();
			txtNPW_ADHOCEXCESPRM.Text=objRow["NPW_ADHOCEXCESPRM"].ToString();
			ddlNPW_DEATHBENEFITOPTION.ClearSelection();
			ListItem item8=ddlNPW_DEATHBENEFITOPTION.Items.FindByValue(objRow["NPW_DEATHBENEFITOPTION"].ToString());
			if (item8!= null)
			{
				item8.Selected=true;
			}txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP1_PROPOSAL.Enabled=false;


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, "LNPW_PARTIALWITHDRAWAL");
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

		void SetLastEvent()
		{
			if (Request["Operation"] == "View")
				_lastEvent.Text = "View" ;
		}

		void SetListerVisibility()
		{

			Utilities.SetListerVisibility(this);
		}



		private void BindLister(DataTable table)
		{
			lister.DataSource = table;
			lister.DataBind();
			pagerList.Items.Clear();
			for (int i=1;recordCount>0; recordCount-=PAGE_SIZE)
			{				
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
		private NameValueCollection getAllFields() 
		{
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) 
			{
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}

			//foreach (Control c in this.myForm.Controls) {	
			foreach (Control c in this.EntryTableDiv.Controls) 
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
			foreach (string pk in LNPW_PARTIALWITHDRAWAL.PrimaryKeys)
			{
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0)
				{
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
			foreach(string pk in  LNPW_PARTIALWITHDRAWAL.PrimaryKeys)
			{
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound)
			{
				if (item.ItemType == ListItemType.Item)
				{
					NPW_YEAR=double.Parse(((LinkButton)e.Item.FindControl("linkNPW_YEAR1")).Text);
					NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL1")).Text;
	
				}
				else
				{
					NPW_YEAR=double.Parse(((LinkButton)e.Item.FindControl("linkNPW_YEAR2")).Text);
					NP1_PROPOSAL=((Label)e.Item.FindControl("lblNP1_PROPOSAL2")).Text;
	
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new LNPW_PARTIALWITHDRAWALDB(dataHolder).FindByPK(NP1_PROPOSAL,NPW_YEAR)["LNPW_PARTIALWITHDRAWAL"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			if (btnHideLister!=null)
				btnHideLister.Disabled=true;
			EntryTableDiv.Style.Add("visibility" , "hidden");
			ListerDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields
		{
			get
			{	
				return EntryTableDiv.Controls;
			}
		}
	}
}

