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

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ET_UC_USERCOUNTRY : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls


		protected System.Web.UI.WebControls.Literal _lastEvent;
		
		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;

		bool recordSelected = false;
		
		
		NameValueCollection columnNameValue=null;
		

						

		/************ pk variables declaration ************/
			private string  USE_USERID;
	private string  CCN_CTRYCD;

		
		
						
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
		//recordCount = LUCN_USERCOUNTRYDB.RecordCount;
		return   dataHolder;         

	}
	sealed protected override void BindInputData(DataHolder dataHolder)
	{

		
		
		_lastEvent.Text = "View";
		
		HeaderScript.Text = EnvHelper.Parse("") ;
		FooterScript.Text = EnvHelper.Parse("") ;
			
		
		
		
	}
#endregion
    
#region Major methods of the final step
	sealed protected override DataHolder GetData(DataHolder dataHolder) {	
		
		//recordCount = LUCN_USERCOUNTRYDB.RecordCount;
		return dataHolder;
	}      
	
	sealed protected override void ApplyDomainLogic(DataHolder dataHolder) 
	{
		columnNameValue=new NameValueCollection();
		SaveTransaction = false;
		switch ((EnumControlArgs)ControlArgs[0])
		{
			case (EnumControlArgs.Save):
				SaveTransaction = true;
				PrintMessage("Record has been saved");
				break;
			case (EnumControlArgs.Update):					
				SaveTransaction = true;
				PrintMessage("Record has been updated");
				break;
			case (EnumControlArgs.Delete):
				SaveTransaction = true;
				PrintMessage("Record has been deleted");				
				break;
			case (EnumControlArgs.Process):						
				DB.BeginTransaction();
				SaveTransaction = true;
				//dataHolder = new DUMMYDB(dataHolder).FindByPK();				
				columnNameValue.Add("USE_USERID",txtUSE_USERID.Text.Trim()==""?null:txtUSE_USERID.Text);
columnNameValue.Add("CCN_CTRYCD",txtCCN_CTRYCD.Text.Trim()==""?null:txtCCN_CTRYCD.Text);
columnNameValue.Add("UCN_DEFAULT",ddlUCN_DEFAULT.SelectedValue.Trim()==""?null:ddlUCN_DEFAULT.SelectedValue);

				//security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), DUMMY.PrimaryKeys, columnNameValue, "DUMMY");
				string result="";					
				if (_CustomArgName.Value == "ProcessName"){
					string processName = _CustomArgVal.Value;	
					//if (security.ProcessAllowed(processName)){
						Type type = Type.GetType(processName);											
						if (type != null){
							shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
							NameValueCollection[] dataRows = new NameValueCollection[1];
							bool[] SelectedRowIndexes = new bool[1];
							dataRows[0] = getAllFields();
							SelectedRowIndexes[0] = true;
							proccessCommand.setAllFields(columnNameValue);
							proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
							//proccessCommand.setPrimaryKeys(LUCN_USERCOUNTRY.PrimaryKeys);
							//proccessCommand.setTableName("LUCN_USERCOUNTRY");
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							result = proccessCommand.processing();
							//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
						}
					//}
					//else{
					//	result = "You are not Autherized to Execute Process.";
					//}						
				}	
				recordSelected =true;
				if (result.Length>0)
					PrintMessage(result);
				break;
		}
	}
	
	sealed protected override void DataBind(DataHolder dataHolder){		
		_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();
		HeaderScript.Text = EnvHelper.Parse("");
		FooterScript.Text = EnvHelper.Parse("");				
		
	}
#endregion	

#region Events
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
#endregion 

		private void GetSessionValues()
		{
			//SessionObject.Get("org_code") == null || SessionObject.Get("org_code") == null
			if (false)
			{
				_lastEvent.Text = EnumControlArgs.None.ToString();	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				



				//ltlorg_code.Text = SessionObject.GetString("org_code");
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

		protected void Page_Unload(object sender, System.EventArgs e)
		{

			//base.OnUnload(e);
			//Stop  by Rehan it clears session values 24/04/2006
			/*SessionObject.Set("USE_USERID",txtUSE_USERID.Text);
		SessionObject.Set("CCN_CTRYCD",txtCCN_CTRYCD.Text);
		SessionObject.Set("UCN_DEFAULT",ddlUCN_DEFAULT.SelectedValue);
		*/ 
		}
		/**
		 * New Method Added For New Support
	 	 */
		private NameValueCollection getAllFields() {
			NameValueCollection allFields = new NameValueCollection();
			if(columnNameValue!=null)
			{
			foreach(object key in columnNameValue.Keys) {
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}
			}
			//foreach (Control c in this.myForm.Controls) {	
			foreach (Control c in this.NormalEntryTableDiv.Controls) {	
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
						case "SHMA.Enterprise.Presentation.WebControls.DatePopUp":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)){
								allFields.add(_fieldName, ((SHMA.Enterprise.Presentation.WebControls.DatePopUp)c).SelectedDate);
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
						case "SHMA.Enterprise.Presentation.WebControls.ComboBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;

					}
				}
			}	
			return allFields;
		}

	}
}

