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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_LF_LNFUFUNDS : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		private int pageNumber=1;
		private int recordCount=0;
		int PAGE_SIZE= 200;
		

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
			dataHolder = new LNFU_FUNDSDB(dataHolder).GetILUS_ET_LF_LNFUFUNDS_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder){
			BindGrid(dataHolder["LNFU_FUNDS"]);
			

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;

			

			

		}
		
		
		protected bool InsertAllowed 
		{
			get	
			{
				return false; 
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
			dataHolder = new LNFU_FUNDSDB(dataHolder).GetILUS_ET_LF_LNFUFUNDS_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LNFU_FUNDS LNFU_FUNDS_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
			entityClass=new ace.LNFU_FUNDS();
			entityClass.setNameValueCollection(columnNameValue);
			//entityClass.setAllFieldCollection(getAllFields());


			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LNFU_FUNDS_obj =new LNFU_FUNDS(dataHolder);
					UpdateAll(LNFU_FUNDS_obj);
					/*DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((TextBox)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text.Length>0)
					{
					//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
					//{
						columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
						columnNameValue.Add("NP2_SETNO",((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text);
						columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text);
						columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text);
						columnNameValue.Add("NFU_RATE",((TextBox)footerItem.Cells[0].FindControl("txtNewNFU_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNFU_RATE")).Text));

						entityClass.fsoperationBeforeSave();

						LNFU_FUNDS_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_LF_LNFUFUNDS",null);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterSave();


						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNFU_FUNDS.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNFU_FUNDS");
						((SHMA.Enterprise.Presentation.WebControls.ComboItem)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNFU_RATE")).Text ="";

					_RecordsSaved = true ;
					}*/
					//}
					_RecordsSaved = true ;
					if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
						{
							PrintMessage("Record(s) succesfully saved.");
						}
					break;
				case (EnumControlArgs.Delete):					
					DB.BeginTransaction();
					SaveTransaction = true;			
					LNFU_FUNDS_obj =new LNFU_FUNDS(dataHolder);
					if (DeleteAll(LNFU_FUNDS_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LNFU_FUNDS_obj);			
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
							proccessCommand.setPrimaryKeys(LNFU_FUNDS.PrimaryKeys);
							proccessCommand.setTableName("LNFU_FUNDS");
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
				dataHolder.Data.Tables["LNFU_FUNDS"].Rows.Clear();
				dataHolder = new LNFU_FUNDSDB(dataHolder).GetILUS_ET_LF_LNFUFUNDS_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					BindGrid(dataHolder["LNFU_FUNDS"]);
					break;
				case (EnumControlArgs.Delete):
					BindGrid(dataHolder["LNFU_FUNDS"]);
					break;
				case (EnumControlArgs.Process):
					BindGrid(dataHolder["LNFU_FUNDS"]);
					break;
				case (EnumControlArgs.Pager):
					BindGrid(dataHolder["LNFU_FUNDS"]);
					break;	
			}
			
			

			
			
			
			
		}
	#endregion	
	
	
	 private void BindGrid(DataTable table)
	 {
		 DataTable _table = new DataTable("LNFU_FUNDS");
		_totalRecords.Text = Utilities.PaginateTable(table, _table,PAGE_SIZE,pageNumber).ToString();
		recordCount = int.Parse(_totalRecords.Text);
		_totalRecords.Text = _table.Rows.Count.ToString();
		EntryGrid.DataSource = _table;
		EntryGrid.DataBind();
		pagerList.Items.Clear();
		for (int i=1;recordCount>0; recordCount-=PAGE_SIZE)
		{
		pagerList.Items.Add(i.ToString());
		i++;
		}
	 }
	
	
	
	#region Events		
	
		
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) {
				pageNumber = pagerList.SelectedIndex + 1;
				ControlArgs=new object[1];
				ControlArgs[0]=EnumControlArgs.Pager;
				DoControl();
				pagerList.SelectedIndex = pageNumber -1;
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
				
				SHMA.Enterprise.Presentation.WebControls.ComboItem txtCFU_FUNDCODE = (SHMA.Enterprise.Presentation.WebControls.ComboItem)e.Item.Cells[0].FindControl("txtCFU_FUNDCODE");
txtCFU_FUNDCODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
txtCFU_FUNDCODE.Enabled=false;
TextBox txtNFU_RATE = (TextBox)e.Item.Cells[0].FindControl("txtNFU_RATE");
txtNFU_RATE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				
				
				ListItem selectedItem =null;

			}				
			else if (e.Item.ItemType==ListItemType.Footer){	
				
				
				TextBox txtNewCFU_FUNDCODE = (TextBox)e.Item.Cells[0].FindControl("txtNewCFU_FUNDCODE");
Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" + txtNewCFU_FUNDCODE.ClientID + "')!=null) document.getElementById('" + txtNewCFU_FUNDCODE.ClientID + "').focus();</script>");

				TextBox lblNewNP1_PROPOSAL = (TextBox)e.Item.Cells[0].FindControl("lblNewNP1_PROPOSAL");
lblNewNP1_PROPOSAL.Text = (string)SessionObject.Get("NP1_PROPOSAL");
TextBox lblNewNP2_SETNO = (TextBox)e.Item.Cells[0].FindControl("lblNewNP2_SETNO");
lblNewNP2_SETNO.Text = (string)SessionObject.Get("NP2_SETNO");
TextBox lblNewPPR_PRODCD = (TextBox)e.Item.Cells[0].FindControl("lblNewPPR_PRODCD");
lblNewPPR_PRODCD.Text = (string)SessionObject.Get("PPR_PRODCD");

				TextBox txtNewNFU_RATE_onblur = (TextBox)e.Item.Cells[0].FindControl("txtNewNFU_RATE");
txtNewNFU_RATE_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
			if (SessionObject.Get("NP1_PROPOSAL")==null  || SessionObject.GetString("NP1_PROPOSAL")== ""  || SessionObject.Get("NP2_SETNO")==null  || SessionObject.GetString("NP2_SETNO")== ""  || SessionObject.Get("PPR_PRODCD")==null  || SessionObject.GetString("PPR_PRODCD")== "" )
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message){
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);
			foreach (DataGridItem item in EntryGrid.Items)
			{
				TextBox txtBx = (TextBox)item.Cells[0].FindControl("txtNFU_RATE");
				if(!txtBx.Text.Trim().Equals("") && !txtBx.Text.Trim().Equals("0"))
				{
					if(txtBx.Text.IndexOf(".") > -1)
						txtBx.Text = string.Format("{0}", txtBx.Text.Substring(0,txtBx.Text.IndexOf(".")+3));
				}
			}

		}

		private bool DeleteAll(LNFU_FUNDS LNFU_FUNDS_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        entityClass.setNameValueCollection(columnNameValue);

			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked){
columnNameValue=new NameValueCollection();
columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text);
columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text);
columnNameValue.Add("NFU_RATE",((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text));

				entityClass.fsoperationBeforeDelete();

				LNFU_FUNDS_obj.Delete(columnNameValue);
				deleted=true;
				dataHolder.Update(DB.Transaction);				
				entityClass.fsoperationAfterDelete();

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNFU_FUNDS.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNFU_FUNDS");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LNFU_FUNDS LNFU_FUNDS_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			shgn.SHGNCommand entityClass=new ace.LNFU_FUNDS();
			entityClass.setNameValueCollection(columnNameValue);

			//**** Reset the existing Rates ****//
			//ace.LNFU_FUNDS.resetRate();
			
			//for (int i=0; i<mRows.Length-1; i++)
			//for (int i=0; i<EntryGrid.Items.Count ; i++)
			foreach(DataGridItem item in EntryGrid.Items)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();

				//DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text);
				columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
				columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text);
				columnNameValue.Add("NFU_RATE",((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text));
				
				/*
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("NP2_SETNO",   ((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:   ((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblNP2_SETNO")).Text);
				columnNameValue.Add("PPR_PRODCD",  ((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:  ((TextBox)EntryGrid.Items[i].Cells[0].FindControl("lblPPR_PRODCD")).Text);
				columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)EntryGrid.Items[i].Cells[0].FindControl("txtCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)EntryGrid.Items[i].Cells[0].FindControl("txtCFU_FUNDCODE")).Text);
				columnNameValue.Add("NFU_RATE",    ((TextBox)EntryGrid.Items[i].Cells[0].FindControl("txtNFU_RATE")).Text.Trim()==""?null:    (object)double.Parse(((TextBox)EntryGrid.Items[i].Cells[0].FindControl("txtNFU_RATE")).Text));
				*/

				entityClass.fsoperationBeforeUpdate();
				LNFU_FUNDS_obj.Update(columnNameValue);
				dataHolder.Update(DB.Transaction);
				entityClass.fsoperationAfterUpdate();
				//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNFU_FUNDS.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNFU_FUNDS");
			}
			
			//**** Check the input values - must be 100% and max 10 funds are allowed to select  ****//
			ace.LNFU_FUNDS.validateInputPercentage();
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
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text);
columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblPPR_PRODCD")).Text);
columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)item.Cells[0].FindControl("txtCFU_FUNDCODE")).Text);
columnNameValue.Add("CFU_DESCR",((TextBox)item.Cells[0].FindControl("txtCFU_DESCR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtCFU_DESCR")).Text);
columnNameValue.Add("NFU_RATE",((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNFU_RATE")).Text));

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
columnNameValue.Add("NP2_SETNO",((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text);
columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewPPR_PRODCD")).Text);
columnNameValue.Add("CFU_FUNDCODE",((SHMA.Enterprise.Presentation.WebControls.ComboItem)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text.Trim()==""?null:((SHMA.Enterprise.Presentation.WebControls.ComboItem)footerItem.Cells[0].FindControl("txtNewCFU_FUNDCODE")).Text);
columnNameValue.Add("CFU_DESCR",((TextBox)footerItem.Cells[0].FindControl("txtNewCFU_DESCR")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewCFU_DESCR")).Text);
columnNameValue.Add("NFU_RATE",((TextBox)footerItem.Cells[0].FindControl("txtNewNFU_RATE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNFU_RATE")).Text));

			return columnNameValue;		
		}

	}
}

