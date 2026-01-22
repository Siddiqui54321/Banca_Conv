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
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Presentation;
using shsm;

using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;

namespace SHAB.Presentation
{
	public partial class shgn_ts_se_tblscreen_ILUS_TE_UL_LIMITATION : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		
		

		protected bool _RecordsUpdated = false ;
		protected bool _RecordsSaved  = false ;

		shgn.SHGNCommand entityClass;
		
		
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
			this.EntryGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.EntryGrid_ItemDataBound);

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
			ClearSession();
			GetSessionValues();
			dataHolder = new LPVL_VALIDATIONDB(dataHolder).GetILUS_TE_UL_LIMITATION_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder){
			EntryGrid.DataSource = dataHolder["LPVL_VALIDATION"];
					EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LPVL_VALIDATION"].Rows.Count.ToString();

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;

			

			

		}
		
		
		protected bool InsertAllowed 
		{
			get	
			{
				return true; 
			}
		}

		protected bool ShowFooter
		{
			get
			{
				return (InsertAllowed || SummaryExists);
			}
		}

		protected bool ReFetchOnBind
		{
			get
			{
				return true;
			}
		}

		protected bool SummaryExists
		{
			get
			{
				return false;
			}
		}




	#endregion
      
	#region Major methods of the final step      		
		sealed protected override DataHolder GetData(DataHolder dataHolder) 
		{	
			dataHolder = new LPVL_VALIDATIONDB(dataHolder).GetILUS_TE_UL_LIMITATION_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LPVL_VALIDATION LPVL_VALIDATION_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LPVL_VALIDATION_obj =new LPVL_VALIDATION(dataHolder);
					UpdateAll(LPVL_VALIDATION_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text.Length>0)
{
					//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
					//{
					   columnNameValue.Add("PVL_VALIDATIONFOR",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR")).SelectedValue);
columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text);
columnNameValue.Add("PVL_LEVEL",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text));
columnNameValue.Add("PVL_AGEFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGEFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGEFROM")).Text));
columnNameValue.Add("PVL_AGETO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGETO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGETO")).Text));
columnNameValue.Add("PVL_TERMFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMFROM")).Text));
columnNameValue.Add("PVL_TERMTO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMTO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMTO")).Text));
columnNameValue.Add("PVL_VALIDFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDFROM")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDFROM")).Text);
columnNameValue.Add("PVL_VALIDTO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDTO")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDTO")).Text);

					   
					   LPVL_VALIDATION_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_TE_UL_LIMITATION",null);

					   dataHolder.Update(DB.Transaction);
					   

					   auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPVL_VALIDATION.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LPVL_VALIDATION");
					((DropDownList)footerItem.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR")).ClearSelection();
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGEFROM")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGETO")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMFROM")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMTO")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDFROM")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDTO")).Text ="";

					_RecordsSaved = true ;
					}
					//}
					if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
						{
							PrintMessage("Record(s) succesfully saved.");
						}
					break;
				case (EnumControlArgs.Delete):					
					DB.BeginTransaction();
					SaveTransaction = true;			
					LPVL_VALIDATION_obj =new LPVL_VALIDATION(dataHolder);
					if (DeleteAll(LPVL_VALIDATION_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LPVL_VALIDATION_obj);			
					//PrintMessage("Record(s) succesfully deleted.");
					break;
				case (EnumControlArgs.Process):
					DB.BeginTransaction();
					SaveTransaction = true;			
					string result="";
					string processName = null;
					if (_CustomArgName.Value == "ProcessName")
					{
						processName = _CustomArgVal.Value;	
						Type type = Type.GetType(processName);											
						if (type != null)
						{
							NameValueCollection[] dataRows = GetAllItemsData();
							bool[] SelectedRowIndexes = GetSelectedRowIndexes();
							shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
							proccessCommand.setAllFields(columnNameValue);
							proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
							proccessCommand.setPrimaryKeys(LPVL_VALIDATION.PrimaryKeys);
							proccessCommand.setTableName("LPVL_VALIDATION");
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							result = proccessCommand.processing();
						}
						else{
							throw new ApplicationException("Process class '" + processName +  "' not found.");
						}
					}						
					if (result.Length>0){
						PrintMessage(result);
					}
					else{
						PrintMessage ("Process " + processName + " executed successfully!");
					}
					break;
			}
		}
		sealed protected override void DataBind(DataHolder dataHolder) 
		{
			txtModifiedRows.Text = "" ;

			if(ReFetchOnBind)
			{
				dataHolder.Data.Tables["LPVL_VALIDATION"].Rows.Clear();
				dataHolder = new LPVL_VALIDATIONDB(dataHolder).GetILUS_TE_UL_LIMITATION_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LPVL_VALIDATION"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LPVL_VALIDATION"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LPVL_VALIDATION"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LPVL_VALIDATION"].Rows.Count.ToString();

			
			
			
			
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

		private void EntryGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e){

			if (e.Item.ItemType==ListItemType.Header){
				CheckBox chkSelectAll = (CheckBox)e.Item.Cells[0].FindControl("chkSelectAll");
				if (chkSelectAll!=null)
					chkSelectAll.Attributes.Add("onclick","selectAllCheckBoxes(this.checked);");						
			}				
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem){
				((TableRow)e.Item).Attributes.Add("onclick","eventClick1(this);");						

				CheckBox chkDelete = (CheckBox)e.Item.Cells[0].FindControl("chkDelete");
				chkDelete.Attributes.Add("onclick" ,"fccheckBoxClicked(this);");
				
				DropDownList ddlPVL_VALIDATIONFOR = (DropDownList)e.Item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR");
ddlPVL_VALIDATIONFOR.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
ddlPVL_VALIDATIONFOR.Enabled=false;
TextBox txtPVL_LEVEL = (TextBox)e.Item.Cells[0].FindControl("txtPVL_LEVEL");
txtPVL_LEVEL.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
txtPVL_LEVEL.Enabled=false;
TextBox txtPVL_AGEFROM = (TextBox)e.Item.Cells[0].FindControl("txtPVL_AGEFROM");
txtPVL_AGEFROM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
TextBox txtPVL_AGETO = (TextBox)e.Item.Cells[0].FindControl("txtPVL_AGETO");
txtPVL_AGETO.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
TextBox txtPVL_TERMFROM = (TextBox)e.Item.Cells[0].FindControl("txtPVL_TERMFROM");
txtPVL_TERMFROM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
TextBox txtPVL_TERMTO = (TextBox)e.Item.Cells[0].FindControl("txtPVL_TERMTO");
txtPVL_TERMTO.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
TextBox txtPVL_VALIDFROM = (TextBox)e.Item.Cells[0].FindControl("txtPVL_VALIDFROM");
txtPVL_VALIDFROM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
TextBox txtPVL_VALIDTO = (TextBox)e.Item.Cells[0].FindControl("txtPVL_VALIDTO");
txtPVL_VALIDTO.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				
				
				ListItem selectedItem =null;
selectedItem = ddlPVL_VALIDATIONFOR.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["PVL_VALIDATIONFOR"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer){	
				
				
				DropDownList ddlNewPVL_VALIDATIONFOR = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR");
Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewPVL_VALIDATIONFOR.ClientID + "')!=null)document.getElementById('" +ddlNewPVL_VALIDATIONFOR.ClientID + "').focus();</script>");

				TextBox lblNewPPR_PRODCD = (TextBox)e.Item.Cells[0].FindControl("lblNewPPR_PRODCD");
lblNewPPR_PRODCD.Text = (string)SessionObject.Get("PPR_PRODCD");

				TextBox txtNewPVL_VALIDTO_onblur = (TextBox)e.Item.Cells[0].FindControl("txtNewPVL_VALIDTO");
txtNewPVL_VALIDTO_onblur.Attributes["onkeydown"] += "callSend();";

				
				
			}
		}


#endregion 
		protected override bool TransactionRequired {
	 	 get {
			return true;
		     }
		}


		private void ClearSession()
		{
			
			//SessionObject.RemoveAt("<>");
		}		

		private void GetSessionValues()
		{
			if (SessionObject.Get("PPR_PRODCD")==null  || SessionObject.GetString("PPR_PRODCD")== "" )
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message){
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		private bool DeleteAll(LPVL_VALIDATION LPVL_VALIDATION_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked){
columnNameValue=new NameValueCollection();
columnNameValue.Add("PVL_VALIDATIONFOR",((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue);
columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
columnNameValue.Add("PVL_LEVEL",((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text));
columnNameValue.Add("PVL_AGEFROM",((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text));
columnNameValue.Add("PVL_AGETO",((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text));
columnNameValue.Add("PVL_TERMFROM",((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text));
columnNameValue.Add("PVL_TERMTO",((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text));
columnNameValue.Add("PVL_VALIDFROM",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text);
columnNameValue.Add("PVL_VALIDTO",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text);

				
				LPVL_VALIDATION_obj.Delete(columnNameValue);
				deleted=true;
				dataHolder.Update(DB.Transaction);				
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPVL_VALIDATION.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LPVL_VALIDATION");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LPVL_VALIDATION LPVL_VALIDATION_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("PVL_VALIDATIONFOR",((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue);
columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
columnNameValue.Add("PVL_LEVEL",((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text));
columnNameValue.Add("PVL_AGEFROM",((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text));
columnNameValue.Add("PVL_AGETO",((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text));
columnNameValue.Add("PVL_TERMFROM",((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text));
columnNameValue.Add("PVL_TERMTO",((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text));
columnNameValue.Add("PVL_VALIDFROM",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text);
columnNameValue.Add("PVL_VALIDTO",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text);

			
								
				LPVL_VALIDATION_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPVL_VALIDATION.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LPVL_VALIDATION");
			}
		}

		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}
		bool[] GetSelectedRowIndexes(){
			bool[] RowIndexes = new bool[EntryGrid.Items.Count];
			foreach(DataGridItem item in EntryGrid.Items){
				RowIndexes[item.ItemIndex] = ((CheckBox)item.Cells[0].FindControl("chkDelete")).Checked;
			}
			return RowIndexes;
		}
		NameValueCollection[] GetAllItemsData()
		{						
			NameValueCollection columnNameValue=new NameValueCollection();
			NameValueCollection[] dataRows = new NameValueCollection[EntryGrid.Items.Count];	
			foreach(DataGridItem item in EntryGrid.Items){
				columnNameValue = new NameValueCollection();
				columnNameValue.Add("PVL_VALIDATIONFOR",((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPVL_VALIDATIONFOR")).SelectedValue);
columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
columnNameValue.Add("PVL_LEVEL",((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_LEVEL")).Text));
columnNameValue.Add("PVL_AGEFROM",((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGEFROM")).Text));
columnNameValue.Add("PVL_AGETO",((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_AGETO")).Text));
columnNameValue.Add("PVL_TERMFROM",((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMFROM")).Text));
columnNameValue.Add("PVL_TERMTO",((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPVL_TERMTO")).Text));
columnNameValue.Add("PVL_VALIDFROM",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDFROM")).Text);
columnNameValue.Add("PVL_VALIDTO",((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPVL_VALIDTO")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("PVL_VALIDATIONFOR",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPVL_VALIDATIONFOR")).SelectedValue);
columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text);
columnNameValue.Add("PVL_LEVEL",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_LEVEL")).Text));
columnNameValue.Add("PVL_AGEFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGEFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGEFROM")).Text));
columnNameValue.Add("PVL_AGETO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGETO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_AGETO")).Text));
columnNameValue.Add("PVL_TERMFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMFROM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMFROM")).Text));
columnNameValue.Add("PVL_TERMTO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMTO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_TERMTO")).Text));
columnNameValue.Add("PVL_VALIDFROM",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDFROM")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDFROM")).Text);
columnNameValue.Add("PVL_VALIDTO",((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDTO")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPVL_VALIDTO")).Text);

			return columnNameValue;		
		}

	}
}

