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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TB_WITHDRAWAL : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		
		
		//protected SHMA.Enterprise.Presentation.WebControls.DataGrid EntryGrid;






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
			this.EntryGrid.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.EntryGrid_ItemDataBound);

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
			ClearSession();
			GetSessionValues();
			dataHolder = new LNTP_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_WITHDRAWAL_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			//EntryGrid.DataSource = FlipDataSet(dataHolder["LNTP_PARTIALWITHDRAWAL"]);
			EntryGrid.DataSource = dataHolder["LNTP_PARTIALWITHDRAWAL"];
			EntryGrid.DataBind();

			
			_totalRecords.Text = dataHolder["LNTP_PARTIALWITHDRAWAL"].Rows.Count.ToString();

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;

			

			

		}


		public DataSet FlipDataSet(DataSet my_DataSet)	
		{		
			DataSet ds = new DataSet();		
			foreach(DataTable dt in my_DataSet.Tables)		
			{			
				DataTable table = new DataTable();			
				for(int i = 0; i <= dt.Rows.Count; i++)			
				{				
					table.Columns.Add(Convert.ToString(i));			
				}			
				DataRow r = null;			
				for(int k = 0; k < dt.Columns.Count; k++)			
				{				
					r = table.NewRow();				
					r[0] = dt.Columns[k].ToString();				
					for(int j = 1; j <= dt.Rows.Count; j++)					
						r[j] = dt.Rows[j - 1][k];				
					table.Rows.Add(r);			
				}			
				ds.Tables.Add(table);		
			}		
			return ds;	
		}


		public DataTable FlipDataSet(DataTable dt)	
		{		
			DataTable table = new DataTable();			
			for(int i = 0; i <= dt.Rows.Count; i++)			
			{				
				table.Columns.Add(Convert.ToString(i));			
			}			
			DataRow r = null;			
			for(int k = 0; k < dt.Columns.Count; k++)			
			{				
				r = table.NewRow();				
				r[0] = dt.Columns[k].ToString();				
				for(int j = 1; j <= dt.Rows.Count; j++)					
					r[j] = dt.Rows[j - 1][k];				
				table.Rows.Add(r);			
			}			
			return table;	
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
			dataHolder = new LNTP_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_WITHDRAWAL_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LNTP_PARTIALWITHDRAWAL LNTP_PARTIALWITHDRAWAL_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LNTP_PARTIALWITHDRAWAL_obj =new LNTP_PARTIALWITHDRAWAL(dataHolder);
					UpdateAll(LNTP_PARTIALWITHDRAWAL_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(false)//new record
					{
						//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
						//{
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
					LNTP_PARTIALWITHDRAWAL_obj =new LNTP_PARTIALWITHDRAWAL(dataHolder);
					if (DeleteAll(LNTP_PARTIALWITHDRAWAL_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LNTP_PARTIALWITHDRAWAL_obj);			
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
							proccessCommand.setPrimaryKeys(LNTP_PARTIALWITHDRAWAL.PrimaryKeys);
							proccessCommand.setTableName("LNTP_PARTIALWITHDRAWAL");
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							result = proccessCommand.processing();
						}
						else
						{
							throw new ApplicationException("Process class '" + processName +  "' not found.");
						}
					}						
					if (result.Length>0)
					{
						PrintMessage(result);
					}
					else
					{
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
				dataHolder.Data.Tables["LNTP_PARTIALWITHDRAWAL"].Rows.Clear();
				dataHolder = new LNTP_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_WITHDRAWAL_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LNTP_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LNTP_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LNTP_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LNTP_PARTIALWITHDRAWAL"].Rows.Count.ToString();

			
			
			
			
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

		private void EntryGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{

			if (e.Item.ItemType==ListItemType.Header)
			{
				//CheckBox chkSelectAll = (CheckBox)e.Item.FindControl("chkSelectAll");
				//if (chkSelectAll!=null)
					//chkSelectAll.Attributes.Add("onclick","selectAllCheckBoxes(this.checked);");						
			}				
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				//((TableRow)e.Item).Attributes.Add("onclick","eventClick1(this);");						

				//CheckBox chkDelete = (CheckBox)e.Item.FindControl("chkDelete");
				//chkDelete.Attributes.Add("onclick" ,"fccheckBoxClicked(this);");
				
				TextBox txtNTP_COLCODE = (TextBox)e.Item.FindControl("txtNTP_COLCODE");
				txtNTP_COLCODE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNTP_COLCODE.Enabled=false;

				
				
				DropDownList ddlNTP_COL1 = (DropDownList)e.Item.FindControl("ddlNTP_COL1");
				ddlNTP_COL1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				DropDownList ddlNTP_COL2 = (DropDownList)e.Item.FindControl("ddlNTP_COL2");
				ddlNTP_COL2.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				TextBox txtNTP_COL3 = (TextBox)e.Item.FindControl("txtNTP_COL3");
				txtNTP_COL3.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				
				ListItem selectedItem =null;
				selectedItem = ddlNTP_COL1.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NTP_COL1"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;
				IDataReader drNTP_COL2 = LCPU_PURPOSEDB.GetDDL_ILUS_ET_TB_WITHDRAWAL_NTP_COL2_RO();
				ddlNTP_COL2.DataSource = drNTP_COL2;
				ddlNTP_COL2.DataBind();
				drNTP_COL2.Close();
				selectedItem = ddlNTP_COL2.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NTP_COL2"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;

			}				
			//else if (e.Item.ItemType==ListItemType.Footer)
			//{	
				
				/*DropDownList ddlNTP_COL2 = (DropDownList)e.Item.FindControl("ddlNewNTP_COL2");
				IDataReader drNTP_COL2 = LCPU_PURPOSEDB.GetDDL_ILUS_ET_TB_WITHDRAWAL_NTP_COL2_RO();
				ddlNTP_COL2.DataSource = drNTP_COL2;
				ddlNTP_COL2.DataBind();
				drNTP_COL2.Close();

				TextBox txtNewNTP_COLCODE = (TextBox)e.Item.FindControl("txtNewNTP_COLCODE");
				Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" + txtNewNTP_COLCODE.ClientID + "')!=null) document.getElementById('" + txtNewNTP_COLCODE.ClientID + "').focus();</script>");

				TextBox lblNewNP1_PROPOSAL = (TextBox)e.Item.FindControl("lblNewNP1_PROPOSAL");
				lblNewNP1_PROPOSAL.Text = (string)SessionObject.Get("NP1_PROPOSAL");

				TextBox txtNewNTP_COL10_onblur = (TextBox)e.Item.FindControl("txtNewNTP_COL10");
				txtNewNTP_COL10_onblur.Attributes["onkeydown"] += "callSend();";*/

				
				
			//}
		}


		#endregion 
		protected override bool TransactionRequired 
		{
			get 
			{
				return true;
			}
		}


		private void ClearSession()
		{
			
			//SessionObject.RemoveAt("<>");
		}		

		private void GetSessionValues()
		{
			if (false)
			{
				
				//_lastEvent.Text = EnumControlArgs.None.ToString();// new induction	
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}

		}		

		protected sealed override string ErrorHandle(string message)
		{
			
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		private bool DeleteAll(LNTP_PARTIALWITHDRAWAL LNTP_PARTIALWITHDRAWAL_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text);
					columnNameValue.Add("NTP_COLCODE",((TextBox)item.FindControl("txtNTP_COLCODE")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COLCODE")).Text);
					columnNameValue.Add("NTP_COL1",((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue);
					columnNameValue.Add("NTP_COL2",((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue);
					columnNameValue.Add("NTP_COL3",((TextBox)item.FindControl("txtNTP_COL3")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COL3")).Text);

					LNTP_PARTIALWITHDRAWAL_obj.Delete(columnNameValue);
					deleted=true;
					dataHolder.Update(DB.Transaction);				
				
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNTP_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNTP_PARTIALWITHDRAWAL");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LNTP_PARTIALWITHDRAWAL LNTP_PARTIALWITHDRAWAL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataListItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("NTP_COLCODE",((TextBox)item.FindControl("txtNTP_COLCODE")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COLCODE")).Text);
				columnNameValue.Add("NTP_COL1",((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue);
				columnNameValue.Add("NTP_COL2",((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue);
				columnNameValue.Add("NTP_COL3",((TextBox)item.FindControl("txtNTP_COL3")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COL3")).Text);
			
								
				LNTP_PARTIALWITHDRAWAL_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNTP_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNTP_PARTIALWITHDRAWAL");
			}
		}

		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}
		bool[] GetSelectedRowIndexes()
		{
			bool[] RowIndexes = new bool[EntryGrid.Items.Count];
			foreach(DataGridItem item in EntryGrid.Items)
			{
				RowIndexes[item.ItemIndex] = ((CheckBox)item.FindControl("chkDelete")).Checked;
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
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("NTP_COLCODE",((TextBox)item.FindControl("txtNTP_COLCODE")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COLCODE")).Text);
				columnNameValue.Add("NTP_COL1",((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL1")).SelectedValue);
				columnNameValue.Add("NTP_COL2",((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNTP_COL2")).SelectedValue);
				columnNameValue.Add("NTP_COL3",((TextBox)item.FindControl("txtNTP_COL3")).Text.Trim()==""?null:((TextBox)item.FindControl("txtNTP_COL3")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	

			return columnNameValue;		
		}

	}
}

