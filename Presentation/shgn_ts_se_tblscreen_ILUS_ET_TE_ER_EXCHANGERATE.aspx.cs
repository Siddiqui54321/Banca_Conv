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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TE_ER_EXCHANGERATE : SHMA.Enterprise.Presentation.TwoStepController
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
			dataHolder = new PEX_EXGRATEDB(dataHolder).GetILUS_ET_TE_ER_EXCHANGERATE_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder){
			EntryGrid.DataSource = dataHolder["PEX_EXGRATE"];
					EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["PEX_EXGRATE"].Rows.Count.ToString();

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
			dataHolder = new PEX_EXGRATEDB(dataHolder).GetILUS_ET_TE_ER_EXCHANGERATE_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			PEX_EXGRATE PEX_EXGRATE_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					PEX_EXGRATE_obj =new PEX_EXGRATE(dataHolder);
					UpdateAll(PEX_EXGRATE_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).SelectedValue.Length>0)
{
					//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
					//{
					   columnNameValue.Add("PCU_BASECURR",((TextBox)footerItem.Cells[0].FindControl("lblNewPCU_BASECURR")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPCU_BASECURR")).Text);
columnNameValue.Add("PCU_CURRCODE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).SelectedValue);
columnNameValue.Add("PET_EXRATETYPE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPET_EXRATETYPE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPET_EXRATETYPE")).SelectedValue);
columnNameValue.Add("PEX_VALUEDAT",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_VALUEDAT")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_VALUEDAT")).Text));
columnNameValue.Add("PEX_SERIAL",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_SERIAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_SERIAL")).Text);
columnNameValue.Add("PEX_RATE",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_RATE")).Text));

					   
					   PEX_EXGRATE_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_TE_ER_EXCHANGERATE",null);

					   dataHolder.Update(DB.Transaction);
					   

					   auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),PEX_EXGRATE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "PEX_EXGRATE");
					((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).ClearSelection();
((DropDownList)footerItem.Cells[0].FindControl("ddlNewPET_EXRATETYPE")).ClearSelection();
((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_VALUEDAT")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_SERIAL")).Text ="";
((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_RATE")).Text ="";

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
					PEX_EXGRATE_obj =new PEX_EXGRATE(dataHolder);
					if (DeleteAll(PEX_EXGRATE_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(PEX_EXGRATE_obj);			
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
							proccessCommand.setPrimaryKeys(PEX_EXGRATE.PrimaryKeys);
							proccessCommand.setTableName("PEX_EXGRATE");
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
				dataHolder.Data.Tables["PEX_EXGRATE"].Rows.Clear();
				dataHolder = new PEX_EXGRATEDB(dataHolder).GetILUS_ET_TE_ER_EXCHANGERATE_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["PEX_EXGRATE"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["PEX_EXGRATE"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["PEX_EXGRATE"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["PEX_EXGRATE"].Rows.Count.ToString();

			
			
			
			
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
				
				DropDownList ddlPCU_CURRCODE = (DropDownList)e.Item.Cells[0].FindControl("ddlPCU_CURRCODE");
ddlPCU_CURRCODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
ddlPCU_CURRCODE.Enabled=false;
DropDownList ddlPET_EXRATETYPE = (DropDownList)e.Item.Cells[0].FindControl("ddlPET_EXRATETYPE");
ddlPET_EXRATETYPE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
ddlPET_EXRATETYPE.Enabled=false;
TextBox txtPEX_VALUEDAT = (TextBox)e.Item.Cells[0].FindControl("txtPEX_VALUEDAT");
txtPEX_VALUEDAT.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
txtPEX_VALUEDAT.Enabled=false;
TextBox txtPEX_SERIAL = (TextBox)e.Item.Cells[0].FindControl("txtPEX_SERIAL");
txtPEX_SERIAL.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
txtPEX_SERIAL.Enabled=false;
TextBox txtPEX_RATE = (TextBox)e.Item.Cells[0].FindControl("txtPEX_RATE");
txtPEX_RATE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				
				
				ListItem selectedItem =null;
IDataReader drPCU_CURRCODE = PCU_CURRENCYDB.GetDDL_ILUS_ET_TE_ER_EXCHANGERATE_PCU_CURRCODE_RO();
ddlPCU_CURRCODE.DataSource = drPCU_CURRCODE;
ddlPCU_CURRCODE.DataBind();
drPCU_CURRCODE.Close();
selectedItem = ddlPCU_CURRCODE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["PCU_CURRCODE"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;
selectedItem = ddlPET_EXRATETYPE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["PET_EXRATETYPE"].ToString());
if (selectedItem!=null)
selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer){	
				
				DropDownList ddlPCU_CURRCODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPCU_CURRCODE");
IDataReader drPCU_CURRCODE = PCU_CURRENCYDB.GetDDL_ILUS_ET_TE_ER_EXCHANGERATE_PCU_CURRCODE_RO();
ddlPCU_CURRCODE.DataSource = drPCU_CURRCODE;
ddlPCU_CURRCODE.DataBind();
drPCU_CURRCODE.Close();

				DropDownList ddlNewPCU_CURRCODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewPCU_CURRCODE");
Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewPCU_CURRCODE.ClientID + "')!=null)document.getElementById('" +ddlNewPCU_CURRCODE.ClientID + "').focus();</script>");

				TextBox lblNewPCU_BASECURR = (TextBox)e.Item.Cells[0].FindControl("lblNewPCU_BASECURR");
lblNewPCU_BASECURR.Text = (string)SessionObject.Get("PCU_BASECURR");

				TextBox txtNewPEX_RATE_onblur = (TextBox)e.Item.Cells[0].FindControl("txtNewPEX_RATE");
txtNewPEX_RATE_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
			if (SessionObject.Get("PCU_BASECURR")==null  || SessionObject.GetString("PCU_BASECURR")== "" )
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message){
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		private bool DeleteAll(PEX_EXGRATE PEX_EXGRATE_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked){
columnNameValue=new NameValueCollection();
columnNameValue.Add("PCU_BASECURR",((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text);
columnNameValue.Add("PCU_CURRCODE",((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue);
columnNameValue.Add("PET_EXRATETYPE",((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue);
columnNameValue.Add("PEX_VALUEDAT",((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text));
columnNameValue.Add("PEX_SERIAL",((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text);
columnNameValue.Add("PEX_RATE",((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text));

				
				PEX_EXGRATE_obj.Delete(columnNameValue);
				deleted=true;
				dataHolder.Update(DB.Transaction);				
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),PEX_EXGRATE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PEX_EXGRATE");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(PEX_EXGRATE PEX_EXGRATE_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("PCU_BASECURR",((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text);
columnNameValue.Add("PCU_CURRCODE",((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue);
columnNameValue.Add("PET_EXRATETYPE",((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue);
columnNameValue.Add("PEX_VALUEDAT",((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text));
columnNameValue.Add("PEX_SERIAL",((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text);
columnNameValue.Add("PEX_RATE",((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text));

			
								
				PEX_EXGRATE_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),PEX_EXGRATE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "PEX_EXGRATE");
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
				columnNameValue.Add("PCU_BASECURR",((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPCU_BASECURR")).Text);
columnNameValue.Add("PCU_CURRCODE",((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPCU_CURRCODE")).SelectedValue);
columnNameValue.Add("PET_EXRATETYPE",((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlPET_EXRATETYPE")).SelectedValue);
columnNameValue.Add("PEX_VALUEDAT",((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_VALUEDAT")).Text));
columnNameValue.Add("PEX_SERIAL",((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPEX_SERIAL")).Text);
columnNameValue.Add("PEX_RATE",((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtPEX_RATE")).Text));

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("PCU_BASECURR",((TextBox)footerItem.Cells[0].FindControl("lblNewPCU_BASECURR")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPCU_BASECURR")).Text);
columnNameValue.Add("PCU_CURRCODE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPCU_CURRCODE")).SelectedValue);
columnNameValue.Add("PET_EXRATETYPE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewPET_EXRATETYPE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewPET_EXRATETYPE")).SelectedValue);
columnNameValue.Add("PEX_VALUEDAT",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_VALUEDAT")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_VALUEDAT")).Text));
columnNameValue.Add("PEX_SERIAL",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_SERIAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_SERIAL")).Text);
columnNameValue.Add("PEX_RATE",((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewPEX_RATE")).Text));

			return columnNameValue;		
		}

	}
}

