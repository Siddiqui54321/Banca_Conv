using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using shsm;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
using System.Data.OleDb;

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ST_BRANCHPRODUCT : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		private int pageNumber=1;
		int PAGE_SIZE= 20 ;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		
		shgn.SHGNCommand entityClass;
		/******************* Entity Fields *****************/
		protected System.Web.UI.WebControls.Literal ltlCCH_CODE;
		protected System.Web.UI.WebControls.Literal ltlCCD_CODE;
		protected System.Web.UI.WebControls.Literal ltlCCS_CODE;


				
	#region pk variables declaration		
			private string  CCH_CODE;
			private string  CCD_CODE;
			private string  CCS_CODE;
			private string  PPR_PRODCD;
						
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
		
		private void InitializeComponent(){
			this.lister.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.lister_ItemDataBound);
			this.lister.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.lister_ItemCommand);
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
			CheckKeyLevel();
			//recordCount = CCS_CHANLSUBDETLDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
	
			IDataReader BRANCHPRODUCTReader0 = LPPR_PRODUCTDB.GetDDL_ILUS_ST_BRANCHPRODUCT_PPR_PRODCD_RO();
			ddlPPR_PRODCD.DataSource = BRANCHPRODUCTReader0;
			ddlPPR_PRODCD.DataBind();
			BRANCHPRODUCTReader0.Close();

						
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("LPCH_CHANNEL");
			IDataReader LPCH_CHANNELReader= LPCH_CHANNELDB.GetILUS_ST_BRANCHPRODUCT_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(LPCH_CHANNELReader, table, PAGE_SIZE, LPCH_CHANNEL.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(LPCH_CHANNELReader, table, PAGE_SIZE, pageNumber);
			}
			LPCH_CHANNELReader.Close();
			BindLister(table);							
		

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;
				
			
		
			/************** Array Data Script **************/
			
			
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			pagerList.SelectedIndex = pageNumber-1;		
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion
    
	#region Major methods of the final step
		protected override void ValidateRequest() {
			base.ValidateRequest();									
			foreach (string key in CCS_CHANLSUBDETL.PrimaryKeys){
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null){
					if (ctrl is WebControl){
					//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null)){
							control.Enabled = true;
						}				
					}
				}
			}			
		}
		sealed protected override DataHolder GetData(DataHolder dataHolder) {	
			pageNumber = pagerList.SelectedIndex +1;
			//recordCount = CCS_CHANLSUBDETLDB.RecordCount;
			return dataHolder;
		}      
	
/*		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
			case (EnumControlArgs.Save):
				_lastEvent.Text = "Save";
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPCH_CHANNELDB(dataHolder).FindByPK(ddlPPR_PRODCD.SelectedValue,txtCCH_CODE.Text,txtCCD_CODE.Text,txtCCS_CODE.Text);
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
				columnNameValue.Add("CCD_CODE",txtCCD_CODE.Text.Trim()==""?null:txtCCD_CODE.Text);
				columnNameValue.Add("CCS_CODE",txtCCS_CODE.Text.Trim()==""?null:txtCCS_CODE.Text);
				columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
								
				//security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCS_CHANLSUBDETL.PrimaryKeys, columnNameValue, "CCS_CHANLSUBDETL");
				//if (security.SaveAllowed){
					
					new LPCH_CHANNEL(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ST_BRANCHPRODUCT",null);
					dataHolder.Update(DB.Transaction);

					
					//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LPCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LPCH_CHANNEL");
					_lastEvent.Text = "Save"; 					
					PrintMessage("Record has been saved");
				//}
				//else{
				//	PrintMessage("You are not autherized to Save.");
				//}
				break;
			case (EnumControlArgs.Delete):
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new LPCH_CHANNELDB(dataHolder).FindByPK(ddlPPR_PRODCD.SelectedValue,txtCCH_CODE.Text,txtCCD_CODE.Text,txtCCS_CODE.Text);
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
				columnNameValue.Add("CCD_CODE",txtCCD_CODE.Text.Trim()==""?null:txtCCD_CODE.Text);
				columnNameValue.Add("CCS_CODE",txtCCS_CODE.Text.Trim()==""?null:txtCCS_CODE.Text);
				columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);

				//security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCS_CHANLSUBDETL.PrimaryKeys, columnNameValue, "CCS_CHANLSUBDETL");
				//if (security.DeleteAllowed){
				
				new LPCH_CHANNEL(dataHolder).Delete(columnNameValue);

				dataHolder.Update(DB.Transaction);
				
				//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), CCS_CHANLSUBDETL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "CCS_CHANLSUBDETL");
				PrintMessage("Record has been deleted");				
				//}
				//else{
				//	PrintMessage("You are not autherized to Delete.");
				//}

				break;
			}
		}
		*/

		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;
			string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			OleDbConnection conn = new OleDbConnection(str_connString);

			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					//DB.BeginTransaction();
					SaveTransaction = true;
					//dataHolder = new LPCH_CHANNELDB(dataHolder).FindByPK(ddlPPR_PRODCD.SelectedValue,txtCCH_CODE.Text,txtCCD_CODE.Text,txtCCS_CODE.Text);
					columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
					columnNameValue.Add("CCD_CODE",txtCCD_CODE.Text.Trim()==""?null:txtCCD_CODE.Text);
					columnNameValue.Add("CCS_CODE",txtCCS_CODE.Text.Trim()==""?null:txtCCS_CODE.Text);
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);

					try
					{
						OleDbCommand mySqlCommand = conn.CreateCommand();
						mySqlCommand.CommandText ="INSERT INTO lpch_channel (CCH_CODE,CCD_CODE,CCS_CODE,PPR_PRODCD) VALUES ('"+txtCCH_CODE.Text+"','"+txtCCD_CODE.Text+"','"+txtCCS_CODE.Text+"','"+ddlPPR_PRODCD.SelectedValue+"')";
						conn.Open();
						int numberOfRows = mySqlCommand.ExecuteNonQuery();
						if(numberOfRows==1)
						{
							PrintMessage("Record has been Saved.");
						}

					}
					catch(OleDbException oledbExc)
					{
						if(oledbExc.ErrorCode == -2147217873)
						{
							PrintMessage("Record already found.");
						}
						else
						{
							PrintMessage(oledbExc.Message);
						}
					}
					catch( Exception ex)
					{
						PrintMessage(ex.Message);
					}
					finally
					{
						conn.Close();
					}
					break;
		
				case (EnumControlArgs.Delete):
					//DB.BeginTransaction();
					SaveTransaction = true;
					//dataHolder = new LPCH_CHANNELDB(dataHolder).FindByPK(ddlPPR_PRODCD.SelectedValue,txtCCH_CODE.Text,txtCCD_CODE.Text,txtCCS_CODE.Text);
					columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
					columnNameValue.Add("CCD_CODE",txtCCD_CODE.Text.Trim()==""?null:txtCCD_CODE.Text);
					columnNameValue.Add("CCS_CODE",txtCCS_CODE.Text.Trim()==""?null:txtCCS_CODE.Text);
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);

					try
					{
						OleDbCommand mySqlCommand = conn.CreateCommand();
						mySqlCommand.CommandText ="DELETE from lpch_channel WHERE CCH_CODE='"+txtCCH_CODE.Text+"' AND CCD_CODE='"+txtCCD_CODE.Text+"' AND CCS_CODE='"+txtCCS_CODE.Text+"' AND PPR_PRODCD='"+ddlPPR_PRODCD.SelectedValue+"'";
						conn.Open();
						int numberOfRows = mySqlCommand.ExecuteNonQuery();
						if(numberOfRows==1)
						{
							PrintMessage("Record has been Deleted.");
						}
					}
					catch( Exception ex)
					{
						PrintMessage(ex.Message);
					}
					finally
					{
						conn.Close();
					}
					break;
			}
		}
		
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			
		  	
			
			LPCH_CHANNELDB LPCH_CHANNELDB_obj = new LPCH_CHANNELDB(dataHolder);		
			IDataReader LPCH_CHANNELReader;
			DataTable table = new DataTable("LPCH_CHANNEL") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit){
				DataRow row = LPCH_CHANNELDB_obj.FindByPK(PPR_PRODCD,CCH_CODE,CCD_CODE,CCS_CODE)["LPCH_CHANNEL"].Rows[0];
				ShowData(row);
			}		
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter){
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					LPCH_CHANNELReader = LPCH_CHANNELDB.GetILUS_ST_BRANCHPRODUCT_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();
				else
					LPCH_CHANNELReader = LPCH_CHANNELDB.GetILUS_ST_CHANNELSUBDTL_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());

				recordCount = Utilities.Reader2Table(LPCH_CHANNELReader, table, PAGE_SIZE, pageNumber);
				LPCH_CHANNELReader.Close();
	
				BindLister(table);
                        if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
				RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					//ShowData(dataHolder["LPCH_CHANNEL"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString()){
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPCH_CHANNEL.PrimaryKeys, columnNameValue, "LPCH_CHANNEL");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save){
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else{
					if((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					{
						_lastEvent.Text = "New";
					}
					else
						_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();
				}
			}
			//for header & footer script					
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	

			HeaderScript.Text = EnvHelper.Parse("");
			FooterScript.Text = EnvHelper.Parse("");

			pagerList.SelectedIndex = pageNumber - 1;			
			
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion	

	#region Events
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) {
			pageNumber = pagerList.SelectedIndex+1;
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Pager;
			DoControl();
			pagerList.SelectedIndex=pageNumber-1;
		}
		private void btnViewAll_Click(object sender, System.EventArgs e) {
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Cancel  ;
			DoControl();
		}
		
		private void lister_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) {
			foreach (RepeaterItem item in lister.Items){
				if (item == e.Item){
					((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				}
				else{
					if (item.ItemType == ListItemType.Item)
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerItem";
					else
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerAlterItem";
				}
			}
			if (e.CommandName == "Edit") {								
				if (e.Item.ItemType==ListItemType.Item)
				{
					PPR_PRODCD=((LinkButton)e.Item.FindControl("linkPPR_PRODCD1")).Text;
					CCH_CODE=((Label)e.Item.FindControl("lblCCH_CODE1")).Text;
					CCD_CODE=((Label)e.Item.FindControl("lblCCD_CODE1")).Text;
					CCS_CODE=((Label)e.Item.FindControl("lblCCS_CODE1")).Text;

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem)
				{
					PPR_PRODCD=((LinkButton)e.Item.FindControl("linkPPR_PRODCD2")).Text;
					CCH_CODE=((Label)e.Item.FindControl("lblCCH_CODE2")).Text;
					CCD_CODE=((Label)e.Item.FindControl("lblCCD_CODE2")).Text;
					CCS_CODE=((Label)e.Item.FindControl("lblCCS_CODE2")).Text;
				}

				ControlArgs=new object[1];
				ControlArgs[0]=EnumControlArgs.Edit; 
				DoControl();								
			}				
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
		private void lister_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) 
		{
			if (recordSelected)
				FindAndSelectCurrentRecord(e);

			HtmlTableRow tRow = (HtmlTableRow)e.Item.FindControl("ListerRow");
			LinkButton linkPPR_PRODCD = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item)
			{
				linkPPR_PRODCD = (LinkButton)e.Item.FindControl("linkPPR_PRODCD1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem)
			{
				linkPPR_PRODCD=(LinkButton)e.Item.FindControl("linkPPR_PRODCD2");	
			}			
			tRow.Attributes.Add("onclick", linkPPR_PRODCD.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			if (SetFieldsInSession())
			{
				SessionObject.Set("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue);
			}
		}	
	
	#endregion 
		protected override bool TransactionRequired {
	 	 get {
			return true;
		     }
		}

		private void GetSessionValues()
		{
			if (SessionObject.Get("CCH_CODE")==null  || SessionObject.GetString("CCH_CODE")== ""  || SessionObject.Get("CCD_CODE")==null  || SessionObject.GetString("CCD_CODE")== "" || SessionObject.Get("CCS_CODE")==null  || SessionObject.GetString("CCS_CODE")== "" ){	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				txtCCH_CODE.Text=SessionObject.GetString("CCH_CODE");
				txtCCD_CODE.Text=SessionObject.GetString("CCD_CODE");
				txtCCS_CODE.Text=SessionObject.GetString("CCS_CODE");
				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		
		
		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			//*//txtCCH_CODE.Text="";
			//*//txtCCD_CODE.Text="";
			ddlPPR_PRODCD.Enabled = true;
			ddlPPR_PRODCD.ClearSelection();
		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtCCH_CODE.Text=objRow["CCH_CODE"].ToString();
			txtCCH_CODE.Enabled=false;
			txtCCD_CODE.Text=objRow["CCD_CODE"].ToString();
			txtCCD_CODE.Enabled=false;
			txtCCS_CODE.Text=objRow["CCS_CODE"].ToString();
			txtCCS_CODE.Enabled=false;

			ddlPPR_PRODCD.ClearSelection();
			ListItem item1=ddlPPR_PRODCD.Items.FindByValue(objRow["PPR_PRODCD"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}
			ddlPPR_PRODCD.Enabled=false;

			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LPCH_CHANNEL.PrimaryKeys, columnNameValue, "LPCH_CHANNEL");
			foreach(string processName in AllProcess){
				if (security.ProcessAllowed(processName)){
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed){
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
		}

		void SetLastEvent(){
		          if (Request["Operation"] == "View")
	                   _lastEvent.Text = "View" ;
 	        }

		void SetListerVisibility(){

                    Utilities.SetListerVisibility(this);
	        }



		private void BindLister(DataTable table)
		{
			lister.DataSource = table;
			lister.DataBind();
			pagerList.Items.Clear();
			for (int i=1;recordCount>0; recordCount-=PAGE_SIZE){				
				pagerList.Items.Add(i.ToString());		
				i++;
			}
			
			//pagerList.SelectedIndex = pageNumber-1;//commented bcz of pagging error
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

		/**
		 * New Method Added For New Support
	 	 */
		private NameValueCollection getAllFields() {
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) {
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}

			//foreach (Control c in this.myForm.Controls) {	
			foreach (Control c in this.EntryTableDiv.Controls) {	
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
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) {
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}


		bool IsRecordSelected(){
			bool selected = true;
			foreach (string pk in LPCH_CHANNEL.PrimaryKeys){
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0){
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			RepeaterItem item=e.Item;
			bool recordFound = true;
			DataRowView row = (DataRowView)item.DataItem;
			foreach(string pk in  LPCH_CHANNEL.PrimaryKeys){
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound)
			{
				if (item.ItemType == ListItemType.Item)
				{
					PPR_PRODCD=((LinkButton)e.Item.FindControl("linkPPR_PRODCD1")).Text;
					CCH_CODE  =((Label)e.Item.FindControl("lblCCH_CODE1")).Text;
					CCD_CODE  =((Label)e.Item.FindControl("lblCCD_CODE1")).Text;
					CCS_CODE  =((Label)e.Item.FindControl("lblCCS_CODE1")).Text;	
				}
				else
				{
					PPR_PRODCD=((LinkButton)e.Item.FindControl("linkPPR_PRODCD2")).Text;
					CCH_CODE  =((Label)e.Item.FindControl("lblCCH_CODE2")).Text;
					CCD_CODE  =((Label)e.Item.FindControl("lblCCD_CODE2")).Text;
					CCS_CODE  =((Label)e.Item.FindControl("lblCCS_CODE2")).Text;
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new LPCH_CHANNELDB(dataHolder).FindByPK(PPR_PRODCD,CCH_CODE,CCD_CODE,CCS_CODE)["LPCH_CHANNEL"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm(){
			if (btnHideLister!=null)
				btnHideLister.Disabled=true;
			EntryTableDiv.Style.Add("visibility" , "hidden");
			ListerDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields{
			get{	
				return EntryTableDiv.Controls;
			}
		}
	}
}

