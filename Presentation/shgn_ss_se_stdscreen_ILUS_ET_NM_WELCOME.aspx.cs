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
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_WELCOME : SHMA.Enterprise.Presentation.TwoStepController
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
		protected System.Web.UI.WebControls.CompareValidator cfvAAG_NAME;

						

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

			//remove any proposal and other codes for existing records
			SessionObject.Remove("NP1_PROPOSAL");
			SessionObject.Remove("PPR_PRODCD");
			SessionObject.Remove("NPH_CODE");
			SessionObject.Remove("NPH_LIFE");
			SessionObject.Remove("NPH_CODE_s");
			SessionObject.Remove("NPH_LIFE_s");
			//remove any proposal and other codes for existing records
			
			IDataReader LCCN_COUNTRYReader0 = LCCN_COUNTRYDB.GetDDL_ILUS_ET_NM_WELCOME_CCN_CTRYCD_RO();;
			ddlCCN_CTRYCD.DataSource = LCCN_COUNTRYReader0 ;
			ddlCCN_CTRYCD.DataBind();
			LCCN_COUNTRYReader0.Close();
			IDataReader CCH_CHANNELReader1 = CCH_CHANNELDB.GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNEL_RO();;
			ddlNP1_CHANNEL.DataSource = CCH_CHANNELReader1 ;
			ddlNP1_CHANNEL.DataBind();
			CCH_CHANNELReader1.Close();
			IDataReader CCD_CHANNELDETAILReader2 = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_NM_WELCOME_NP1_CHANNELDETAIL_RO();;
			ddlNP1_CHANNELDETAIL.DataSource = CCD_CHANNELDETAILReader2 ;
			ddlNP1_CHANNELDETAIL.DataBind();
			CCD_CHANNELDETAILReader2.Close();

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();
			


			
			HeaderScript.Text = EnvHelper.Parse("var strUser=SV(\"s_USE_USERID\");  var txtDateNow='"+DateTime.Now.ToString("dd/MM/yyyy")+"'; var valAAG_NAME = '"+ SessionObject.Get("s_AAG_NAME") + "';") ;
			//FooterScript.Text = EnvHelper.Parse("var valAAG_NAME='"+GetAAG_NAME(""+Session["AAG_AGCODE"])+"'") ;			
			//FooterScript.Text = EnvHelper.Parse(" var strUser=SV(\"s_USE_USERID\");  var valAAG_NAME='"+GetAAG_NAME(""+Session["AAG_AGCODE"])+"';  setFetchDataQry(\"select CCN_CTRYCD  from LUCN_USERCOUNTRY  where USE_USERID=SV(\"USE_USERID\") AND UCN_DEFAULT='Y'\");    fetchData();  ") ; 
			FooterScript.Text = EnvHelper.Parse(" var valAAG_NAME='"+GetAAG_NAME(""+Session["s_AAG_AGCODE"])+"';  ") ; 


			ddlCCN_CTRYCD.Attributes.Add("onchange","filterChannel(this);");
			ddlNP1_CHANNEL.Attributes.Add("onchange","filterChannelDetail(this);");



			//txtNP2_COMMENDATE.Text = DateTime.Now.ToString("dd/MM/yyyy");
		
		

		

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
					columnNameValue.Add("AAG_NAME",txtAAG_NAME.Text.Trim()==""?null:(object)double.Parse(txtAAG_NAME.Text));
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue.Trim()==""?null:ddlNP1_CHANNEL.SelectedValue);
					columnNameValue.Add("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue.Trim()==""?null:ddlNP1_CHANNELDETAIL.SelectedValue);
					columnNameValue.Add("NP2_COMMENDATE",txtNP2_COMMENDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP2_COMMENDATE.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
					if (security.SaveAllowed)
					{
						//entityClass.fsoperationBeforeSave();

						new LNP1_POLICYMASTR(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_WELCOME",null);

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
					columnNameValue.Add("AAG_NAME",txtAAG_NAME.Text.Trim()==""?null:(object)double.Parse(txtAAG_NAME.Text));
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue.Trim()==""?null:ddlNP1_CHANNEL.SelectedValue);
					columnNameValue.Add("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue.Trim()==""?null:ddlNP1_CHANNELDETAIL.SelectedValue);
					columnNameValue.Add("NP2_COMMENDATE",txtNP2_COMMENDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP2_COMMENDATE.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

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
					columnNameValue.Add("AAG_NAME",txtAAG_NAME.Text.Trim()==""?null:(object)double.Parse(txtAAG_NAME.Text));
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue.Trim()==""?null:ddlNP1_CHANNEL.SelectedValue);
					columnNameValue.Add("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue.Trim()==""?null:ddlNP1_CHANNELDETAIL.SelectedValue);
					columnNameValue.Add("NP2_COMMENDATE",txtNP2_COMMENDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP2_COMMENDATE.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

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
					columnNameValue.Add("AAG_NAME",txtAAG_NAME.Text.Trim()==""?null:(object)double.Parse(txtAAG_NAME.Text));
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue.Trim()==""?null:ddlCCN_CTRYCD.SelectedValue);
					columnNameValue.Add("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue.Trim()==""?null:ddlNP1_CHANNEL.SelectedValue);
					columnNameValue.Add("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue.Trim()==""?null:ddlNP1_CHANNELDETAIL.SelectedValue);
					columnNameValue.Add("NP2_COMMENDATE",txtNP2_COMMENDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP2_COMMENDATE.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);

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





			//HeaderScript.Text = EnvHelper.Parse("");
			//FooterScript.Text = EnvHelper.Parse("var v_AgentCode=SV(\"AAG_AGCODE\");   setFetchDataQry(\"SELECT AAG_NAME FROM LAAG_AGENT WHERE AAG_AGCODE='\"+v_AgentCode+\"'\");    fetchData();");
			HeaderScript.Text = EnvHelper.Parse("var txtDateNow='"+DateTime.Now.ToString("dd/MM/yyyy")+"';") ;
			//FooterScript.Text = EnvHelper.Parse("var valAAG_NAME='"+GetAAG_NAME(""+Session["AAG_AGCODE"])+"'") ;
			FooterScript.Text = EnvHelper.Parse("var valAAG_NAME='"+GetAAG_NAME(""+Session["s_AAG_AGCODE"])+"';  setFetchDataQry(\"select CCN_CTRYCD  from LUCN_USERCOUNTRY  where USE_USERID=SV(\"s_USE_USERID\") AND UCN_DEFAULT='Y'\");    fetchData(); ") ; 
			


		
		}
		#endregion	
		private String GetAAG_NAME(String code)
		{
			rowset rsAAG_NAME = DB.executeQuery("SELECT AAG_NAME FROM LAAG_AGENT WHERE AAG_AGCODE='"+ code +"'");
			if(rsAAG_NAME.next())
				return rsAAG_NAME.getString(1);
			return "";
		}

		#region Events
		private void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
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
			/*if (SetFieldsInSession())
			{
				SessionObject.Set("AAG_NAME",txtAAG_NAME.Text);
				SessionObject.Set("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue);
				SessionObject.Set("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue);
				SessionObject.Set("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue);
				SessionObject.Set("NP2_COMMENDATE",txtNP2_COMMENDATE.Text);
				SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
		
			}*/
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
			txtAAG_NAME.Text="";
			ddlCCN_CTRYCD.ClearSelection();
			ddlNP1_CHANNEL.ClearSelection();
			ddlNP1_CHANNELDETAIL.ClearSelection();
			txtNP2_COMMENDATE.Text="";
			txtNP1_PROPOSAL.Enabled = true;
			txtNP1_PROPOSAL.Text="";

		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtAAG_NAME.Text=""+SessionObject.Get("s_AAG_NAME");
			
			
			ddlCCN_CTRYCD.ClearSelection();
			ListItem item1= ddlCCN_CTRYCD.Items.FindByValue(""+SessionObject.Get("s_CCN_CTRYCD"));
			if (item1!= null)
			{
				item1.Selected=true;
			}ddlNP1_CHANNEL.ClearSelection();
			ListItem item2=ddlNP1_CHANNEL.Items.FindByValue(""+SessionObject.Get("s_CCH_CODE"));
			if (item2!= null)
			{
				item2.Selected=true;
			}ddlNP1_CHANNELDETAIL.ClearSelection();
			ListItem item3=ddlNP1_CHANNELDETAIL.Items.FindByValue(""+SessionObject.Get("s_CCD_CODE"));
			if (item3!= null)
			{
				item3.Selected=true;
			}
			
			txtNP2_COMMENDATE.Text=""+SessionObject.Get("NP2_COMMENDATE");

			txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP1_PROPOSAL.Enabled=false;


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


		protected void ShowData()
		{

			RefreshDataFields();
			txtAAG_NAME.Text=""+SessionObject.Get("s_AAG_NAME");
			
			
			ddlCCN_CTRYCD.ClearSelection();
			ListItem item1= ddlCCN_CTRYCD.Items.FindByValue(""+SessionObject.Get("s_CCN_CTRYCD"));
			if (item1!= null)
			{
				item1.Selected=true;
			}ddlNP1_CHANNEL.ClearSelection();
			ListItem item2=ddlNP1_CHANNEL.Items.FindByValue(""+SessionObject.Get("s_CCH_CODE"));
			if (item2!= null)
			{
				item2.Selected=true;
			}ddlNP1_CHANNELDETAIL.ClearSelection();
			ListItem item3=ddlNP1_CHANNELDETAIL.Items.FindByValue(""+SessionObject.Get("s_CCD_CODE"));
			if (item3!= null)
			{
				item3.Selected=true;
			}
			
			txtNP2_COMMENDATE.Text=""+SessionObject.Get("NP2_COMMENDATE");

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

			if(SessionObject.Get("s_CCN_CTRYCD")==null)
				selected = false;

			return selected ;
		}
		private void FindAndSelectCurrentRecord()
		{
			if (IsRecordSelected())
			{
	

				//DataRow selectedRow = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
				ShowData();							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}

		protected void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//remove any existing records
			SessionObject.Remove("NP1_PROPOSAL");
			SessionObject.Remove("PPR_PRODCD");
			SessionObject.Remove("NPH_CODE");
			SessionObject.Remove("NPH_LIFE");
			SessionObject.Remove("NPH_CODE_s");
			SessionObject.Remove("NPH_LIFE_s");
			//remove any existing records
			
			SessionObject.Set("s_AAG_NAME",txtAAG_NAME.Text);
			SessionObject.Set("s_CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue);
			SessionObject.Set("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue);
			SessionObject.Set("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue);
			SessionObject.Set("NP2_COMMENDATE",txtNP2_COMMENDATE.Text);

			//SessionObject.Set("NEXT","Y");
			Response.Write("<script language=javascript>parent.parent.setPage('shgn_gp_gp_ILUS_ET_GP_PERONAL');</script>");
			


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

