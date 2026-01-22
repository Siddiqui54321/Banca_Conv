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
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_PROPOSAL : SHMA.Enterprise.Presentation.TwoStepController
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
		//protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlAAG_AGCODE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvAAG_AGCODE;

		protected System.Web.UI.WebControls.CompareValidator cfvNP2_COMMENDATE;
		protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPDATE;

		//protected System.Web.UI.WebControls.CompareValidator cfvAAG_AGCODE;

		

						

		/************ pk variables declaration ************/
				
		#region pk variables declaration		
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
			//recordCount = LNP1_POLICYMASTRDB.RecordCount;
			return   dataHolder;         

		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{

			//SessionObject.Set("NP1_PROPOSAL", "R/07/0010042");
			
			IDataReader LNPH_PHOLDERReader0 = LNPH_PHOLDERDB.GetDDL_ILUS_ET_NM_PROPOSAL_NPH_FULLNAME_RO();;
			ddlNPH_FULLNAME.DataSource = LNPH_PHOLDERReader0 ;
			ddlNPH_FULLNAME.DataBind();
			LNPH_PHOLDERReader0.Close();
			//IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNEL_RO();;
			IDataReader LCSD_SYSTEMDTLReader1 = LCCN_COUNTRYDB.GetDDL_ILUS_ET_NM_PROPOSAL_CCN_CTRYCD_RO();;
			ddlCCN_CTRYCD.DataSource = LCSD_SYSTEMDTLReader1 ;
			ddlCCN_CTRYCD.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			//IDataReader LCSD_SYSTEMDTLReader2 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PROPOSAL_NP1_CHANNELDETAIL_RO();;
			IDataReader LCSD_SYSTEMDTLReader2 = USE_USERMASTERDB.GetDDL_ILUS_ET_NM_PROPOSAL_USE_USERID_RO();;
			ddlUSE_USERID.DataSource = LCSD_SYSTEMDTLReader2 ;
			ddlUSE_USERID.DataBind();
			LCSD_SYSTEMDTLReader2.Close();
			//IDataReader LAAG_AGENTReader3 = LAAG_AGENTDB.GetDDL_ILUS_ET_NM_PROPOSAL_AAG_AGCODE_RO();;
			//ddlAAG_AGCODE.DataSource = LAAG_AGENTReader3 ;
			//ddlAAG_AGCODE.DataBind();
			//LAAG_AGENTReader3.Close();

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("") ;
			//FooterScript.Text = EnvHelper.Parse("getField(\"CCN_CTRYCD\").value=SV(\"s_CCN_CTRYCD\");getField(\"USE_USERID\").value=SV(\"s_USE_USERID\"); getField(\"NP2_COMMENDATE\").value=SV(\"NP2_COMMENDATE\"); getField(\"NP1_PROPDATE\").value=SV(\"NP1_PROPDATE\"); getField(\"NP2_COMMENDATE\").disabled=true; getField(\"NP1_PROPDATE\").disabled=true;") ;
			FooterScript.Text = EnvHelper.Parse("getField(\"CCN_CTRYCD\").value=SV(\"s_CCN_CTRYCD\");getField(\"USE_USERID\").value=SV(\"s_USE_USERID\"); getField(\"NP1_PROPDATE\").value=SV(\"NP1_PROPDATE\"); getField(\"NP1_PROPDATE\").disabled=true;") ;
		
			
			//txtNP2_COMMENDATE.Enabled = false;
			//txtNP1_PROPDATE.Enabled = false;

			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);

		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNP1_POLICYMASTR.PrimaryKeys)
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
			//entityClass=new mi.MI_ET_NM_PolicyEntry();
			//entityClass.setNameValueCollection(columnNameValue);

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("USE_USERID",ddlUSE_USERID.SelectedValue.Trim()==""?null:ddlUSE_USERID.SelectedValue);
					//columnNameValue.Add("AAG_AGCODE",ddlAAG_AGCODE.SelectedValue.Trim()==""?null:(object)double.Parse(ddlAAG_AGCODE.SelectedValue));
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
					if (security.SaveAllowed)
					{
						//entityClass.fsoperationBeforeSave();

						new LNP1_POLICYMASTR(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PROPOSAL",null);

						dataHolder.Update(DB.Transaction);
						//entityClass.fsoperationAfterSave();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNP1_POLICYMASTR");
						_lastEvent.Text = "Save"; 					
						PrintMessage("Record has been saved");
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);				
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("USE_USERID",ddlUSE_USERID.SelectedValue.Trim()==""?null:ddlUSE_USERID.SelectedValue);
					//columnNameValue.Add("AAG_AGCODE",ddlAAG_AGCODE.SelectedValue.Trim()==""?null:(object)double.Parse(ddlAAG_AGCODE.SelectedValue));

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
					if (security.UpdateAllowed)
					{
						//entityClass.fsoperationBeforeUpdate();

						new LNP1_POLICYMASTR(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);
						//entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNP1_POLICYMASTR");
						//recordSelected = true;
						PrintMessage("Record has been updated");
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);				
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("USE_USERID",ddlUSE_USERID.SelectedValue.Trim()==""?null:ddlUSE_USERID.SelectedValue);
					//columnNameValue.Add("AAG_AGCODE",ddlAAG_AGCODE.SelectedValue.Trim()==""?null:(object)double.Parse(ddlAAG_AGCODE.SelectedValue));

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
					if (security.DeleteAllowed)
					{
						//entityClass.fsoperationBeforeDelete();

						new LNP1_POLICYMASTR(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
						//entityClass.fsoperationAfterDelete();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNP1_POLICYMASTR");
						PrintMessage("Record has been deleted");				
					}
					else
					{
						PrintMessage("You are not autherized to Delete.");
					}
					break;
				case (EnumControlArgs.Process):						
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);				
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("USE_USERID",ddlUSE_USERID.SelectedValue.Trim()==""?null:ddlUSE_USERID.SelectedValue);
					//columnNameValue.Add("AAG_AGCODE",ddlAAG_AGCODE.SelectedValue.Trim()==""?null:(object)double.Parse(ddlAAG_AGCODE.SelectedValue));

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
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
								proccessCommand.setPrimaryKeys(LNP1_POLICYMASTR.PrimaryKeys);
								proccessCommand.setTableName("LNP1_POLICYMASTR");
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
					//recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					break;
			}
		}
	
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			LNP1_POLICYMASTRDB LNP1_POLICYMASTRDB_obj = new LNP1_POLICYMASTRDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				DataRow row = LNP1_POLICYMASTRDB_obj.FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNP1_POLICYMASTR"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
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

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("");
			FooterScript.Text = EnvHelper.Parse("getField(\"CCN_CTRYCD\").value=SV(\"s_CCN_CTRYCD\");getField(\"USE_USERID\").value=SV(\"s_USE_USERID\");");

		
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
				//SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
				//SessionObject.Set("NPH_FULLNAME",ddlNPH_FULLNAME.SelectedValue);
				//SessionObject.Set("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue);
				//SessionObject.Set("USE_USERID",ddlUSE_USERID.SelectedValue);
				//SessionObject.Set("AAG_AGCODE",ddlAAG_AGCODE.SelectedValue);
		
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
			txtNP1_PROPOSAL.Enabled = true;
			txtNP1_PROPOSAL.Text="";
			ddlNPH_FULLNAME.ClearSelection();
			ddlCCN_CTRYCD.ClearSelection();
			ddlUSE_USERID.ClearSelection();
			//ddlAAG_AGCODE.ClearSelection();

		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP2_COMMENDATE.Text=objRow["NP2_COMMENDATE"]==DBNull.Value?"":((DateTime)objRow["NP2_COMMENDATE"]).ToShortDateString();
			
			if(txtNP2_COMMENDATE.Text.Length > 0)
				txtNP2_COMMENDATE.Enabled = false;

			txtNP1_PROPDATE.Text=objRow["NP1_PROPDATE"]==DBNull.Value?"":((DateTime)objRow["NP1_PROPDATE"]).ToShortDateString();
			txtNP1_PROPOSAL.Enabled=false;
			ddlCCN_CTRYCD.ClearSelection();
			ListItem item1=ddlCCN_CTRYCD.Items.FindByValue(Session["s_CCN_CTRYCD"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}ddlUSE_USERID.ClearSelection();
			ListItem item2=ddlUSE_USERID.Items.FindByValue(Session["s_USE_USERID"].ToString());
			if (item2!= null)
			{
				item2.Selected=true;
			}
			//ddlAAG_AGCODE.ClearSelection();
			//ListItem item3=ddlAAG_AGCODE.Items.FindByValue(Session["AAG_AGCODE"].ToString());
			//if (item3!= null)
			//{
			//	item3.Selected=true;
			//}

			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
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
			foreach (string pk in LNP1_POLICYMASTR.PrimaryKeys)
			{
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0)
				{
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord()
		{
			if (IsRecordSelected())
			{
				NP1_PROPOSAL=SessionObject.GetString("NP1_PROPOSAL");
	

				DataRow selectedRow = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
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

