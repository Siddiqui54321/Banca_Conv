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
	public partial class shgn_ts_se_tblscreen_ILUS_ST_BRANCHPRODUCT : SHMA.Enterprise.Presentation.TwoStepController
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
			dataHolder = new LPCH_CHANNELDB(dataHolder).GetILUS_ST_BRANCHPRODUCT_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder){
			EntryGrid.DataSource = dataHolder["LPCH_CHANNEL"];
					EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LPCH_CHANNEL"].Rows.Count.ToString();

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
				return false;
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
			dataHolder = new LPCH_CHANNELDB(dataHolder).GetILUS_ST_BRANCHPRODUCT_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LPCH_CHANNEL LPCH_CHANNEL_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LPCH_CHANNEL_obj =new LPCH_CHANNEL(dataHolder);
					UpdateAll(LPCH_CHANNEL_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).SelectedValue.Length>0)
{
					//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
					//{
					   columnNameValue.Add("PPR_PRODCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).SelectedValue);
columnNameValue.Add("CCH_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text);
columnNameValue.Add("CCD_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text);
columnNameValue.Add("CCS_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCS_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCS_CODE")).Text);

					   
					   LPCH_CHANNEL_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ST_BRANCHPRODUCT",null);

					   dataHolder.Update(DB.Transaction);
					   

					   auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LPCH_CHANNEL");
					((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).ClearSelection();

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
					LPCH_CHANNEL_obj =new LPCH_CHANNEL(dataHolder);
					if (DeleteAll(LPCH_CHANNEL_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LPCH_CHANNEL_obj);			
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
							proccessCommand.setPrimaryKeys(LPCH_CHANNEL.PrimaryKeys);
							proccessCommand.setTableName("LPCH_CHANNEL");
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
				dataHolder.Data.Tables["LPCH_CHANNEL"].Rows.Clear();
				dataHolder = new LPCH_CHANNELDB(dataHolder).GetILUS_ST_BRANCHPRODUCT_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LPCH_CHANNEL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LPCH_CHANNEL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LPCH_CHANNEL"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LPCH_CHANNEL"].Rows.Count.ToString();

			
			
			
			
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
				
				DropDownList ddlPPR_PRODCD = (DropDownList)e.Item.Cells[0].FindControl("ddlPPR_PRODCD");
ddlPPR_PRODCD.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
ddlPPR_PRODCD.Enabled=false;

				
				
				
				ListItem selectedItem =null;
IDataReader drPPR_PRODCD = LPPR_PRODUCTDB.GetDDL_ILUS_ST_BRANCHPRODUCT_PPR_PRODCD_RO();
ddlPPR_PRODCD.DataSource = drPPR_PRODCD;
ddlPPR_PRODCD.DataBind();
drPPR_PRODCD.Close();
selectedItem = ddlPPR_PRODCD.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["PPR_PRODCD"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer){	
				
				DropDownList ddlPPR_PRODCD = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPPR_PRODCD");
IDataReader drPPR_PRODCD = LPPR_PRODUCTDB.GetDDL_ILUS_ST_BRANCHPRODUCT_PPR_PRODCD_RO();
ddlPPR_PRODCD.DataSource = drPPR_PRODCD;
ddlPPR_PRODCD.DataBind();
drPPR_PRODCD.Close();

				DropDownList ddlNewPPR_PRODCD = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPPR_PRODCD");
Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewPPR_PRODCD.ClientID + "')!=null)document.getElementById('" +ddlNewPPR_PRODCD.ClientID + "').focus();</script>");

				TextBox lblNewCCH_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCCH_CODE");
lblNewCCH_CODE.Text = (string)SessionObject.Get("CCH_CODE");
TextBox lblNewCCD_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCCD_CODE");
lblNewCCD_CODE.Text = (string)SessionObject.Get("CCD_CODE");
TextBox lblNewCCS_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCCS_CODE");
lblNewCCS_CODE.Text = (string)SessionObject.Get("CCS_CODE");

				DropDownList ddlNewPPR_PRODCD_onblur = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPPR_PRODCD");
ddlNewPPR_PRODCD_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
			if (SessionObject.Get("CCH_CODE")==null  || SessionObject.GetString("CCH_CODE")== ""  || SessionObject.Get("CCD_CODE")==null  || SessionObject.GetString("CCD_CODE")== ""  || SessionObject.Get("CCS_CODE")==null  || SessionObject.GetString("CCS_CODE")== "" )
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message){
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		private bool DeleteAll(LPCH_CHANNEL LPCH_CHANNEL_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked){
columnNameValue=new NameValueCollection();
columnNameValue.Add("PPR_PRODCD",((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue);
columnNameValue.Add("CCH_CODE",((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
columnNameValue.Add("CCD_CODE",((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
columnNameValue.Add("CCS_CODE",((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text);

				
				LPCH_CHANNEL_obj.Delete(columnNameValue);
				deleted=true;
				dataHolder.Update(DB.Transaction);				
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LPCH_CHANNEL");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LPCH_CHANNEL LPCH_CHANNEL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("PPR_PRODCD",((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue);
columnNameValue.Add("CCH_CODE",((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
columnNameValue.Add("CCD_CODE",((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
columnNameValue.Add("CCS_CODE",((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text);

			
								
				LPCH_CHANNEL_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LPCH_CHANNEL");
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
				columnNameValue.Add("PPR_PRODCD",((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPPR_PRODCD")).SelectedValue);
columnNameValue.Add("CCH_CODE",((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
columnNameValue.Add("CCD_CODE",((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
columnNameValue.Add("CCS_CODE",((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCCS_CODE")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("PPR_PRODCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPPR_PRODCD")).SelectedValue);
columnNameValue.Add("CCH_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text);
columnNameValue.Add("CCD_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text);
columnNameValue.Add("CCS_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCCS_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCS_CODE")).Text);

			return columnNameValue;		
		}

	}
}

