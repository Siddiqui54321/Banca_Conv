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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_NM_PURPOSE : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		
		
		//protected SHMA.Enterprise.Presentation.WebControls;

		protected bool _RecordsUpdated = false ;
		protected bool _RecordsSaved  = false ;

		shgn.SHGNCommand entityClass;
		private SHMA.Enterprise.NameValueCollection nameValueDetail;
		
		
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
			dataHolder = new LCPU_PURPOSEDB(dataHolder).GetILUS_ET_NM_PURPOSE_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{

			//****************CUSTOM SECTION ******************/
			LoadPurposeDtl();
			//****************CUSTOM SECTION ******************/
			
			EntryGrid.DataSource = dataHolder["LCPU_PURPOSE"];
			EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LCPU_PURPOSE"].Rows.Count.ToString();

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
			dataHolder = new LCPU_PURPOSEDB(dataHolder).GetILUS_ET_NM_PURPOSE_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LCPU_PURPOSE LCPU_PURPOSE_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
                        

			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LCPU_PURPOSE_obj =new LCPU_PURPOSE(dataHolder);
					UpdateAll(LCPU_PURPOSE_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];
					if(((TextBox)footerItem.Cells[0].FindControl("txtNewNP1_PROPOSAL")).Text.Length>0)
					{
						//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
						//{
						columnNameValue.Add("CPU_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCPU_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCPU_CODE")).Text);
						columnNameValue.Add("CPU_DESCR",((TextBox)footerItem.Cells[0].FindControl("txtNewCPU_DESCR")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewCPU_DESCR")).Text);

					   
						LCPU_PURPOSE_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_NM_PURPOSE",null);

						dataHolder.Update(DB.Transaction);
					   

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LCPU_PURPOSE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LCPU_PURPOSE");
						((TextBox)footerItem.Cells[0].FindControl("txtNewCPU_DESCR")).Text ="";

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
					LCPU_PURPOSE_obj =new LCPU_PURPOSE(dataHolder);
					if (DeleteAll(LCPU_PURPOSE_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LCPU_PURPOSE_obj);			
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
							proccessCommand.setPrimaryKeys(LCPU_PURPOSE.PrimaryKeys);
							proccessCommand.setTableName("LCPU_PURPOSE");
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
					//else
					//{
					//	PrintMessage ("Process " + processName + " executed successfully!");
					//}
					break;
			}
		}
		sealed protected override void DataBind(DataHolder dataHolder) 
		{
			//****************CUSTOM SECTION ******************/

			LoadPurposeDtl();
			//****************CUSTOM SECTION ******************/

			
			txtModifiedRows.Text = "" ;

			if(ReFetchOnBind)
			{
				dataHolder.Data.Tables["LCPU_PURPOSE"].Rows.Clear();
				dataHolder = new LCPU_PURPOSEDB(dataHolder).GetILUS_ET_NM_PURPOSE_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LCPU_PURPOSE"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LCPU_PURPOSE"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LCPU_PURPOSE"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LCPU_PURPOSE"].Rows.Count.ToString();

			
			
			
			
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

		private void EntryGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

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
				
				//****************CUSTOM SECTION ******************
				//DOMAIN/MODEL LOGIC applied to VIEW
				//take the value of cpu code to query collection whether it's selected or not
				TextBox txtCPU = (TextBox)e.Item.Cells[0].FindControl("lblCPU_CODE");
				string txtCPUValue = txtCPU.Text;

				//get the value of the cpu code for selected?
				String selectedValue = ""+nameValueDetail.get(txtCPU.Text);

				//if selected = 'Y' then checkbox should render selected else wise-versa
				bool selected = selectedValue.Equals("Y")?true:false;
				if (selected)
					chkDelete.Checked = true;
				//****************CUSTOM SECTION ******************/

				chkDelete.Attributes.Add("onclick" ,"fccheckBoxClicked(this);");
				
				TextBox txtCPU_DESCR = (TextBox)e.Item.Cells[0].FindControl("txtCPU_DESCR");
				txtCPU_DESCR.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				
				
				ListItem selectedItem =null;

			}				
			else if (e.Item.ItemType==ListItemType.Footer)
			{	
				
				
				TextBox txtNewCPU_DESCR = (TextBox)e.Item.Cells[0].FindControl("txtNewCPU_DESCR");
				Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" + txtNewCPU_DESCR.ClientID + "')!=null) document.getElementById('" + txtNewCPU_DESCR.ClientID + "').focus();</script>");

				TextBox lblNewNP1_PROPOSAL = (TextBox)e.Item.Cells[0].FindControl("lblNewNP1_PROPOSAL");
				lblNewNP1_PROPOSAL.Text = (string)SessionObject.Get("NP1_PROPOSAL");
				TextBox lblNewCPU_CODE = (TextBox)e.Item.Cells[0].FindControl("lblNewCPU_CODE");
				lblNewCPU_CODE.Text = (string)SessionObject.Get("CPU_CODE");

				TextBox txtNewCPU_DESCR_onblur = (TextBox)e.Item.Cells[0].FindControl("txtNewCPU_DESCR");
				txtNewCPU_DESCR_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
		private bool DeleteAll(LCPU_PURPOSE LCPU_PURPOSE_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

                        
			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("CPU_CODE",((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text);
					columnNameValue.Add("CPU_DESCR",((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text);

				
					LCPU_PURPOSE_obj.Delete(columnNameValue);
					deleted=true;
					dataHolder.Update(DB.Transaction);				
				
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LCPU_PURPOSE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LCPU_PURPOSE");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LCPU_PURPOSE LCPU_PURPOSE_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

                        

			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("CPU_CODE",((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text);
				columnNameValue.Add("CPU_DESCR",((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text);

			
								
				LCPU_PURPOSE_obj.Update(columnNameValue);
				
				dataHolder.Update(DB.Transaction);
				

				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LCPU_PURPOSE.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LCPU_PURPOSE");
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
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("CPU_CODE",((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblCPU_CODE")).Text);
				columnNameValue.Add("CPU_DESCR",((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtCPU_DESCR")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
			columnNameValue.Add("CPU_CODE",((TextBox)footerItem.Cells[0].FindControl("lblNewCPU_CODE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewCPU_CODE")).Text);
			columnNameValue.Add("CPU_DESCR",((TextBox)footerItem.Cells[0].FindControl("txtNewCPU_DESCR")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewCPU_DESCR")).Text);

			return columnNameValue;		
		}


		private void LoadPurposeDtl()
		{
			nameValueDetail = new NameValueCollection();
			
			String strSQL = "select * from  LNPU_PURPOSE where NP1_PROPOSAL = '"+SessionObject.Get("NP1_PROPOSAL")+"'";//TODO pick value from session object

			rowset rsLNPU_PURPOSE = DB.executeQuery(strSQL);
			
			while (rsLNPU_PURPOSE.next())
			{
				nameValueDetail.Add(rsLNPU_PURPOSE.getString("CPU_CODE"),  rsLNPU_PURPOSE.getString("NPU_SELECTED"));
			}

		
		}
	}
}



