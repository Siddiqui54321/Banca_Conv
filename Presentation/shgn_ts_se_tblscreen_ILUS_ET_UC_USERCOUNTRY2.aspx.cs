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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_UC_USERCOUNTRY2 : SHMA.Enterprise.Presentation.TwoStepController
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
			dataHolder = new LUCN_USERCOUNTRYDB(dataHolder).GetILUS_ET_UC_USERCOUNTRY2_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder){
			EntryGrid.DataSource = dataHolder["LUCN_USERCOUNTRY"];
					EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LUCN_USERCOUNTRY"].Rows.Count.ToString();

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
			dataHolder = new LUCN_USERCOUNTRYDB(dataHolder).GetILUS_ET_UC_USERCOUNTRY2_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LUCN_USERCOUNTRY LUCN_USERCOUNTRY_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LUCN_USERCOUNTRY_obj =new LUCN_USERCOUNTRY(dataHolder);
					UpdateAll(LUCN_USERCOUNTRY_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).SelectedValue.Length>0)
{
					//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
					//{
					   columnNameValue.Add("CCN_CTRYCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).SelectedValue);
columnNameValue.Add("UCN_DEFAULT",((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCN_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCN_DEFAULT")).SelectedValue);
columnNameValue.Add("USE_USERID",((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text);

					   
					   LUCN_USERCOUNTRY_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_UC_USERCOUNTRY2",null);

					   dataHolder.Update(DB.Transaction);
					   

					   auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCN_USERCOUNTRY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LUCN_USERCOUNTRY");
					((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).ClearSelection();
((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCN_DEFAULT")).ClearSelection();

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
					LUCN_USERCOUNTRY_obj =new LUCN_USERCOUNTRY(dataHolder);
					if (DeleteAll(LUCN_USERCOUNTRY_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LUCN_USERCOUNTRY_obj);			
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
							proccessCommand.setPrimaryKeys(LUCN_USERCOUNTRY.PrimaryKeys);
							proccessCommand.setTableName("LUCN_USERCOUNTRY");
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
				dataHolder.Data.Tables["LUCN_USERCOUNTRY"].Rows.Clear();
				dataHolder = new LUCN_USERCOUNTRYDB(dataHolder).GetILUS_ET_UC_USERCOUNTRY2_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LUCN_USERCOUNTRY"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LUCN_USERCOUNTRY"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LUCN_USERCOUNTRY"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LUCN_USERCOUNTRY"].Rows.Count.ToString();

			
			
			
			
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
				
				DropDownList ddlCCN_CTRYCD = (DropDownList)e.Item.Cells[0].FindControl("ddlCCN_CTRYCD");
ddlCCN_CTRYCD.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
ddlCCN_CTRYCD.Enabled=false;
DropDownList ddlUCN_DEFAULT = (DropDownList)e.Item.Cells[0].FindControl("ddlUCN_DEFAULT");
ddlUCN_DEFAULT.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				
				
				ListItem selectedItem =null;
IDataReader drCCN_CTRYCD = LCCN_COUNTRYDB.GetDDL_ILUS_ET_UC_USERCOUNTRY2_CCN_CTRYCD_RO();
ddlCCN_CTRYCD.DataSource = drCCN_CTRYCD;
ddlCCN_CTRYCD.DataBind();
drCCN_CTRYCD.Close();
selectedItem = ddlCCN_CTRYCD.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CCN_CTRYCD"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;
selectedItem = ddlUCN_DEFAULT.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["UCN_DEFAULT"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer){	
				
				DropDownList ddlCCN_CTRYCD = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCN_CTRYCD");
IDataReader drCCN_CTRYCD = LCCN_COUNTRYDB.GetDDL_ILUS_ET_UC_USERCOUNTRY2_CCN_CTRYCD_RO();
ddlCCN_CTRYCD.DataSource = drCCN_CTRYCD;
ddlCCN_CTRYCD.DataBind();
drCCN_CTRYCD.Close();

				DropDownList ddlNewCCN_CTRYCD = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCN_CTRYCD");
Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewCCN_CTRYCD.ClientID + "')!=null)document.getElementById('" +ddlNewCCN_CTRYCD.ClientID + "').focus();</script>");

				TextBox lblNewUSE_USERID = (TextBox)e.Item.Cells[0].FindControl("lblNewUSE_USERID");
lblNewUSE_USERID.Text = (string)SessionObject.Get("USE_USERID");

				DropDownList ddlNewUCN_DEFAULT_onblur = (DropDownList)e.Item.Cells[0].FindControl("ddlNewUCN_DEFAULT");
ddlNewUCN_DEFAULT_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
			if(SessionObject.Get("s_USE_TYPE")==null || SessionObject.GetString("s_USE_TYPE")!="A" && SessionObject.GetString("s_USE_TYPE")!="B" )
			{
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("You are not authorized for this option");
			}
			if (SessionObject.Get("USE_USERID")==null  || SessionObject.GetString("USE_USERID")== "" )
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message){
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		private bool DeleteAll(LUCN_USERCOUNTRY LUCN_USERCOUNTRY_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked){
columnNameValue=new NameValueCollection();
columnNameValue.Add("CCN_CTRYCD",((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue);
columnNameValue.Add("UCN_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue);
columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);

				
				LUCN_USERCOUNTRY_obj.Delete(columnNameValue);
				deleted=true;
				dataHolder.Update(DB.Transaction);				
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCN_USERCOUNTRY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LUCN_USERCOUNTRY");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LUCN_USERCOUNTRY LUCN_USERCOUNTRY_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("CCN_CTRYCD",((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue);
columnNameValue.Add("UCN_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue);
columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);

			
								
				LUCN_USERCOUNTRY_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCN_USERCOUNTRY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LUCN_USERCOUNTRY");
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
				columnNameValue.Add("CCN_CTRYCD",((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCN_CTRYCD")).SelectedValue);
columnNameValue.Add("UCN_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCN_DEFAULT")).SelectedValue);
columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("CCN_CTRYCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCN_CTRYCD")).SelectedValue);
columnNameValue.Add("UCN_DEFAULT",((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCN_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCN_DEFAULT")).SelectedValue);
columnNameValue.Add("USE_USERID",((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text);

			return columnNameValue;		
		}

	}
}

