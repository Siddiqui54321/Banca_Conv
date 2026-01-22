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
using System.Data.OleDb;
namespace SHAB.Presentation
{
	public partial class shgn_ts_se_tblscreen_ILUS_ET_UC_USERCHANNEL : SHMA.Enterprise.Presentation.TwoStepController
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
			dataHolder = new LUCH_USERCHANNELDB(dataHolder).GetILUS_ET_UC_USERCHANNEL_Data();
			return   dataHolder;         
		}

		

		sealed protected override void BindInputData(DataHolder dataHolder){
				
			
			SetChannelCombos();
			EntryGrid.DataSource = dataHolder["LUCH_USERCHANNEL"];
					EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LUCH_USERCHANNEL"].Rows.Count.ToString();

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse( FooterScript.Text + "") ;

			
			
			
			
		
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
			dataHolder = new LUCH_USERCHANNELDB(dataHolder).GetILUS_ET_UC_USERCHANNEL_Data();
				
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LUCH_USERCHANNEL LUCH_USERCHANNEL_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0]){
				case (EnumControlArgs.Save):
					try
					{
						DB.BeginTransaction();
						SaveTransaction = true;
						LUCH_USERCHANNEL_obj =new LUCH_USERCHANNEL(dataHolder);
						UpdateAll(LUCH_USERCHANNEL_obj);
						DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
						//if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCH_CODE")).SelectedValue.Length>0)
						if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).SelectedValue.Length>0)
						{
							//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
							//{
							columnNameValue.Add("USE_USERID",((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text);
							columnNameValue.Add("CCH_CODE",  ((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text.Trim()  ==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text);
							columnNameValue.Add("CCD_CODE",  ((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text.Trim()  ==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text);
							columnNameValue.Add("CCS_CODE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).SelectedValue);
							columnNameValue.Add("UCH_DEFAULT",((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCH_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCH_DEFAULT")).SelectedValue);
							LUCH_USERCHANNEL_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_UC_USERCHANNEL",null);
							dataHolder.Update(DB.Transaction);

							auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCH_USERCHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LUCH_USERCHANNEL");
							//((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCH_CODE")).ClearSelection();
							//((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCD_CODE")).ClearSelection();
							((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).ClearSelection();
							((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCH_DEFAULT")).ClearSelection();
					

							_RecordsSaved = true ;					
											
							
							//SetChannelCombos();							
							
						
						}					



						//}
						if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
						{
							PrintMessage("Record(s) succesfully saved.");
							dataHolder.Data.Tables["LUCH_USERCHANNEL"].Rows.Clear();
							dataHolder = new LUCH_USERCHANNELDB(dataHolder).GetILUS_ET_UC_USERCHANNEL_Data();
							SetChannelCombos();	
							EntryGrid.DataSource = dataHolder["LUCH_USERCHANNEL"];
							EntryGrid.DataBind();
						}
						break;
					}
					catch(Exception e)
					{
						if(ddlCCH_CODE_1.SelectedValue.Length>0 && ddlCCD_CODE_1.SelectedValue.Length>0)
						{
							DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
							DropDownList ddlNewCCS_CODE = (DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE");
							//((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).DataSource = null;
							IDataReader drNewCCS_CODE = CCS_CHANLSUBDETLDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO(ddlCCH_CODE_1.SelectedValue, ddlCCD_CODE_1.SelectedValue);
							ddlNewCCS_CODE.DataSource = drNewCCS_CODE;
							ddlNewCCS_CODE.DataBind();
							drNewCCS_CODE.Close();
						}
						//throw e;
						PrintMessage(e.Message);
						break;
					}
					
				case (EnumControlArgs.Delete):					
					DB.BeginTransaction();
					SaveTransaction = true;
                    LUCH_USERCHANNEL_obj =new LUCH_USERCHANNEL(dataHolder);
					if (DeleteAll(LUCH_USERCHANNEL_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					SetChannelCombos();
					//DeleteAll(LUCH_USERCHANNEL_obj);			
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
							proccessCommand.setPrimaryKeys(LUCH_USERCHANNEL.PrimaryKeys);
							proccessCommand.setTableName("LUCH_USERCHANNEL");
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
				dataHolder.Data.Tables["LUCH_USERCHANNEL"].Rows.Clear();
                dataHolder = new LUCH_USERCHANNELDB(dataHolder).GetILUS_ET_UC_USERCHANNEL_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LUCH_USERCHANNEL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LUCH_USERCHANNEL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LUCH_USERCHANNEL"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LUCH_USERCHANNEL"].Rows.Count.ToString();

			
			
			
			
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



			if (e.Item.ItemType==ListItemType.Header)
			{
				CheckBox chkSelectAll = (CheckBox)e.Item.Cells[0].FindControl("chkSelectAll");
				if (chkSelectAll!=null)
					chkSelectAll.Attributes.Add("onclick","selectAllCheckBoxes(this.checked);");						
			}				
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				((TableRow)e.Item).Attributes.Add("onclick","eventClick1(this);");						

				CheckBox chkDelete = (CheckBox)e.Item.Cells[0].FindControl("chkDelete");
				chkDelete.Attributes.Add("onclick" ,"fccheckBoxClicked(this);");
				
				//DropDownList ddlCCH_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlCCH_CODE");
				//ddlCCH_CODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); Channel_ChangeEvent(this);");
				//ddlCCH_CODE.Enabled=false;

				//DropDownList ddlCCD_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlCCD_CODE");
				//ddlCCD_CODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); ChannelDetail_ChangeEvent(this);");
				//ddlCCD_CODE.Enabled=false;




				DropDownList ddlCCS_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlCCS_CODE");
				//ddlCCS_CODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); ChannelSubDetail_ChangeEvent(this);");
				ddlCCS_CODE.Enabled=false;

				DropDownList ddlUCH_DEFAULT = (DropDownList)e.Item.Cells[0].FindControl("ddlUCH_DEFAULT");
				ddlUCH_DEFAULT.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); Default_ChangeEvent(this);");
				
				
				ListItem selectedItem =null;
				//IDataReader drCCH_CODE = CCH_CHANNELDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO();

				//ddlCCH_CODE.DataSource = drCCH_CODE;
				//ddlCCH_CODE.DataBind();
				//drCCH_CODE.Close();

				//selectedItem = ddlCCH_CODE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CCH_CODE"].ToString());
				//if (selectedItem!=null)
				//	selectedItem.Selected=true;

				//New code here channel detail
				//IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO(((DataRowView)(e.Item.DataItem)).Row["CCH_CODE"].ToString()) ;
				//ddlCCD_CODE.DataSource = drCCD_CODE;
				//ddlCCD_CODE.DataBind();
				//drCCD_CODE.Close();
				//selectedItem = ddlCCD_CODE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CCD_CODE"].ToString());
				//if (selectedItem!=null)
				//	selectedItem.Selected=true;
					
				////<<<<<<<<<<<<<<<<

				//IDataReader drCCS_CODE = CCS_CHANLSUBDETLDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCS_CODE_RO(((DataRowView)(e.Item.DataItem)).Row["CCH_CODE"].ToString(), ((DataRowView)(e.Item.DataItem)).Row["CCD_CODE"].ToString());
				IDataReader drCCS_CODE = CCS_CHANLSUBDETLDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCS_CODE_RO(ddlCCH_CODE_1.SelectedValue, ddlCCD_CODE_1.SelectedValue);
				ddlCCS_CODE.DataSource = drCCS_CODE;
				ddlCCS_CODE.DataBind();
				drCCS_CODE.Close();
				//selectedItem = ddlCCS_CODE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CCS_CODE"].ToString());
				selectedItem = ddlCCS_CODE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CCS_CODE"].ToString());				
				if (selectedItem!=null)					
					selectedItem.Selected=true;

				selectedItem = ddlUCH_DEFAULT.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["UCH_DEFAULT"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;
//Can Write						

			}	
			
			else if (e.Item.ItemType==ListItemType.Footer)
			{	
//				
//				DropDownList ddlCCH_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCH_CODE");
//				ddlCCH_CODE.Attributes.Add("onchange" ,"Channel_ChangeEvent(this);");
//				IDataReader drCCH_CODE = CCH_CHANNELDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO();
//				ddlCCH_CODE.DataSource = drCCH_CODE;
//				ddlCCH_CODE.DataBind();
//				drCCH_CODE.Close();
//				DropDownList ddlNewCCH_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCH_CODE");
//				Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewCCH_CODE.ClientID + "')!=null)document.getElementById('" +ddlNewCCH_CODE.ClientID + "').focus();</script>");
//				
//				//New code here (for User channel detail)
//				DropDownList ddlCCD_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCD_CODE");
//				ddlCCD_CODE.Attributes.Add("onchange" ,"ChannelDetail_ChangeEvent(this);");
//				IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO("");
//				ddlCCD_CODE.DataSource = drCCD_CODE;
//				ddlCCD_CODE.DataBind();
//				drCCD_CODE.Close();
//				DropDownList ddlNewCCD_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCD_CODE");

				DropDownList ddlCCS_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCS_CODE");
				ddlCCS_CODE.Attributes.Add("onchange" ,"ChannelSubDetail_ChangeEvent(this);");
				IDataReader drCCS_CODE = CCS_CHANLSUBDETLDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCS_CODE_RO(ddlCCH_CODE_1.SelectedValue, ddlCCD_CODE_1.SelectedValue);
				ddlCCS_CODE.DataSource = drCCS_CODE;
				ddlCCS_CODE.DataBind();
				drCCS_CODE.Close();
				DropDownList ddlNewCCS_CODE = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCCS_CODE");
				Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" +ddlNewCCS_CODE.ClientID + "')!=null)document.getElementById('" +ddlNewCCS_CODE.ClientID + "').focus();</script>");

				TextBox lblNewUSE_USERID = (TextBox)e.Item.Cells[0].FindControl("lblNewUSE_USERID");
				lblNewUSE_USERID.Text = (string)SessionObject.Get("USE_USERID");

				TextBox lblNewCCH_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCCH_CODE");
				lblNewCCH_CODE.Text = ddlCCH_CODE_1.SelectedValue;// (string)SessionObject.Get("CCH_CODE");

				TextBox lblNewCCD_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCCD_CODE");
				lblNewCCD_CODE.Text = ddlCCD_CODE_1.SelectedValue;//(string)SessionObject.Get("CCD_CODE");


				DropDownList ddlNewUCH_DEFAULT_onblur = (DropDownList)e.Item.Cells[0].FindControl("ddlNewUCH_DEFAULT");
				ddlNewUCH_DEFAULT_onblur.Attributes["onkeydown"] += "callSend();";
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
		private bool DeleteAll(LUCH_USERCHANNEL LUCH_USERCHANNEL_obj)
		{
            
            SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);
					columnNameValue.Add("CCH_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
					//New code here (for User Channel detail)
					columnNameValue.Add("CCD_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
					columnNameValue.Add("CCS_CODE",((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue);
					columnNameValue.Add("UCH_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue);

                    LUCH_USERCHANNEL_obj.Delete(columnNameValue);
                    deleted = true;

                    //for (int i = dataHolder.Data.Tables[0].Rows.Count - 1; i >= 0; i--)
                    //{
                    //    DataRow dr = dataHolder.Data.Tables[0].Rows[i];
                    //    if (Convert.ToString(dr["CCH_CODE"]) == Convert.ToString(columnNameValue["CCH_CODE"]) && Convert.ToString(dr["CCD_CODE"]) == Convert.ToString(columnNameValue["CCD_CODE"]) && Convert.ToString(dr["CCS_CODE"]) == Convert.ToString(columnNameValue["CCS_CODE"]))
                    //    {
                    //        dr.Delete();
                    //        DB.executeDML("Delete from LUCH_USERCHANNEL where CCH_CODE='"+ Convert.ToString(columnNameValue["CCH_CODE"]) + "' and CCD_CODE='" + Convert.ToString(columnNameValue["CCD_CODE"]) + "' and CCS_CODE='" + Convert.ToString(columnNameValue["CCS_CODE"]) + "' and USE_USERID='" + Convert.ToString(columnNameValue["USE_USERID"]) + "'");
                    //        DB.CommitTransaction();
                    //    }
                    //}
                    //dataHolder.Data.Tables[0].AcceptChanges();
                    
                    dataHolder.Update(DB.Transaction);				
				
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCH_USERCHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LUCH_USERCHANNEL");
				
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LUCH_USERCHANNEL LUCH_USERCHANNEL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];

				columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);
				columnNameValue.Add("CCH_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
				//New code here (for User Channel detail)
				columnNameValue.Add("CCD_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
				columnNameValue.Add("CCS_CODE",((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue);
				columnNameValue.Add("UCH_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue);
								
				LUCH_USERCHANNEL_obj.Update(columnNameValue);
				dataHolder.Update(DB.Transaction);
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LUCH_USERCHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LUCH_USERCHANNEL");
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
			foreach(DataGridItem item in EntryGrid.Items)
			{
				columnNameValue = new NameValueCollection();
				columnNameValue.Add("USE_USERID",((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblUSE_USERID")).Text);
				columnNameValue.Add("CCH_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCH_CODE")).Text);
				//New code here (for User Channel detail)
				columnNameValue.Add("CCD_CODE",  ((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text.Trim()  ==""?null:((TextBox)item.Cells[0].FindControl("lblCCD_CODE")).Text);
				columnNameValue.Add("CCS_CODE",((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCCS_CODE")).SelectedValue);
				columnNameValue.Add("UCH_DEFAULT",((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlUCH_DEFAULT")).SelectedValue);
				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("USE_USERID",((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewUSE_USERID")).Text);
			columnNameValue.Add("CCH_CODE",  ((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text.Trim()  ==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCH_CODE")).Text);
			//New code here (for User Channel detail)
			columnNameValue.Add("CCD_CODE",  ((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text.Trim()  ==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCCD_CODE")).Text);
			columnNameValue.Add("CCS_CODE",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCCS_CODE")).SelectedValue);
			columnNameValue.Add("UCH_DEFAULT",((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCH_DEFAULT")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewUCH_DEFAULT")).SelectedValue);
			return columnNameValue;		
		}

		////////////////// new code 12 jan 2010 - start
		private void SetChannelCombos()
		{
			//New columns for Channel and Channel Detail columns
			//ddlCCH_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); Channel_ChangeEvent(this);");
			//ddlCCD_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); ChannelDetail_ChangeEvent(this);");
			ddlCCH_CODE_1.Attributes.Add("onchange" ,"Channel_ChangeEvent(this);");
			ddlCCD_CODE_1.Attributes.Add("onchange" ,"ChannelDetail_ChangeEvent(this);");
			
			IDataReader drCCH_CODE = CCH_CHANNELDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO();
			ddlCCH_CODE_1.DataSource = drCCH_CODE;
			ddlCCH_CODE_1.DataBind();
			ddlCCH_CODE_1.SelectedIndex = 0;
			drCCH_CODE.Close();
		
				

			int recFound = dataHolder["LUCH_USERCHANNEL"].Rows.Count;
			//recFound=0;
			if(recFound > 0)
			{
				/*IDataReader drCurrentUserChannel = LUCH_USERCHANNELDB.GetUserChannel();
				//drCurrentUserChannel.Read();
				while(drCurrentUserChannel.Read())
				{
					string channel       = Convert.ToString(drCurrentUserChannel["CCH_CODE"]);
					string channelDetail = Convert.ToString(drCurrentUserChannel["CCD_CODE"]);
					//drCurrentUserChannel.Close();
					ddlCCH_CODE_1.SelectedValue = channel;
					ddlCCD_CODE_1.SelectedValue = channelDetail;
					ddlCCH_CODE_1.Enabled=false;

					DataTable Dt = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO_DT(ddlCCH_CODE_1.SelectedValue);
					ddlCCD_CODE_1.DataSource = Dt;
					ddlCCD_CODE_1.DataBind();

					FooterScript.Text = "filterChannelSubDetail(getField(\"CCD_CODE_1\"));";
				}
				drCurrentUserChannel.Close();*/

				IDataReader drCurrentUserChannel = LUCH_USERCHANNELDB.GetUserChannel();
				drCurrentUserChannel.Read();
				string channel       = Convert.ToString(drCurrentUserChannel["CCH_CODE"]);
				string channelDetail = Convert.ToString(drCurrentUserChannel["CCD_CODE"]);
				drCurrentUserChannel.Close();
				ddlCCH_CODE_1.SelectedValue = channel;
				//ddlCCD_CODE_1.SelectedValue = channelDetail;
				ddlCCH_CODE_1.Enabled=false;
				//ddlCCD_CODE_1.Enabled=false;

				IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO(ddlCCH_CODE_1.SelectedValue) ;
				ddlCCD_CODE_1.DataSource = drCCD_CODE;
				ddlCCD_CODE_1.DataBind();
				drCCD_CODE.Close();
				ddlCCD_CODE_1.SelectedValue = channelDetail;
				FooterScript.Text = "filterChannelSubDetail(getField(\"CCD_CODE_1\"));";			
				//FooterScript.Text = "filterChannelSubDetail(getField(\"CCD_CODE_1\"));";
			
			}
			else
			{
				if(SessionObject.GetString("s_USE_TYPE")=="B" && SessionObject.GetString("s_CCD_CODE")=="5")
				{
					IDataReader drowCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO("2") ;
					ddlCCD_CODE_1.DataSource = drowCCD_CODE;
					ddlCCD_CODE_1.DataBind();
					drowCCD_CODE.Close();

					ddlCCH_CODE_1.SelectedValue = "2";
					ddlCCD_CODE_1.SelectedValue = "5";

					ddlCCH_CODE_1.Enabled=false;
					ddlCCD_CODE_1.Enabled=false;

//					FooterScript.Text = "filterChannelSubDetail(getField(\"CCD_CODE_1\"));";

				}				
				else
				{
					ddlCCH_CODE_1.Enabled=true;
					ddlCCD_CODE_1.Enabled=true;

					IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_ILUS_ET_UC_USERCHANNEL_CCD_CODE_RO("") ;
					ddlCCD_CODE_1.DataSource = drCCD_CODE;
					ddlCCD_CODE_1.DataBind();
					drCCD_CODE.Close();
			
					FooterScript.Text = "getField(\"CCH_CODE_1\").value='';getField(\"CCD_CODE_1\").value='';";
				}

			}
		}
		////////////////// new code 12 jan 2010 - end
	}
}

