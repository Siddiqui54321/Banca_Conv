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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TB_BENEFECIARY : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		
		

		protected bool _RecordsUpdated = false ;
		protected bool _RecordsSaved  = false ;

		//shgn.SHGNCommand //entityClass;
		
		
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
			dataHolder = new LNBF_BENEFICIARYDB(dataHolder).GetILUS_ET_TB_BENEFECIARY_Data();
			return   dataHolder;         
		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			EntryGrid.DataSource = dataHolder["LNBF_BENEFICIARY"];
			EntryGrid.DataBind();
			_totalRecords.Text = dataHolder["LNBF_BENEFICIARY"].Rows.Count.ToString();

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;

			

			

		}
		
		
		protected bool InsertAllowed 
		{
			//default code commented
			/*get	
			{
				return true; 
			}*/

			get	
			{
				//// If user has selected to add adherent then show footer row
				

				String _totalRecordsShadowed = dataHolder["LNBF_BENEFICIARY"].Rows.Count.ToString();

				if (_totalRecordsShadowed.Equals("0"))
					return true;

				if ( SessionObject.Get("FLAG_INSERTALLOWED") != null && !SessionObject.Get("FLAG_INSERTALLOWED").ToString().Equals("") && !SessionObject.Get("FLAG_INSERTALLOWED").ToString().Equals("NULL"))
				{
					Session.Remove( "FLAG_INSERTALLOWED" );
					return true;
				}
				else
				{
					Session.Remove( "FLAG_INSERTALLOWED" );
					return false;
				}
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
			dataHolder = new LNBF_BENEFICIARYDB(dataHolder).GetILUS_ET_TB_BENEFECIARY_Data();
			
			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LNBF_BENEFICIARY LNBF_BENEFICIARY_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
			//entityClass=new mi.MI_ET_TB_Adherents();
			//entityClass.setNameValueCollection(columnNameValue);


			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LNBF_BENEFICIARY_obj =new LNBF_BENEFICIARY(dataHolder);
					UpdateAll(LNBF_BENEFICIARY_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];


					((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text = ""+SessionObject.Get("NP1_PROPOSAL");//"R/07/0010042";//TODO: Insert value from Session
					((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text = "0";

					//if(((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Length>0)
					if(((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text.Length>0)
					{
						//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
						//{
						columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
						columnNameValue.Add("NBF_BENEFCD",((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text);
						columnNameValue.Add("NBF_BENNAME",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text);
						
						//CUSTOM: ARABIC
						//columnNameValue.Add("NBF_BENNAMEARABIC",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text);
						columnNameValue.Add("NBF_BENNAMEARABIC",null);
						//CUSTOM: ARABIC


						columnNameValue.Add("NBF_DOB",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_DOB")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_DOB")).Text));
						columnNameValue.Add("NBF_AGE",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AGE")).Text));
						columnNameValue.Add("CRL_RELEATIOCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCRL_RELEATIOCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCRL_RELEATIOCD")).SelectedValue);
						columnNameValue.Add("NBF_BASIS",((DropDownList)footerItem.Cells[0].FindControl("ddlNewNBF_BASIS")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewNBF_BASIS")).SelectedValue);
						columnNameValue.Add("NBF_AMOUNT",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AMOUNT")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AMOUNT")).Text);
						columnNameValue.Add("NBF_PERCNTAGE",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_PERCNTAGE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_PERCNTAGE")).Text.Replace("%",""));

						//entityClass.fsoperationBeforeSave();

						LNBF_BENEFICIARY_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_TB_BENEFECIARY","NBF_BENEFCD");

						dataHolder.Update(DB.Transaction);


						//NBF_BENNAMEARABIC FROM LNBF_BENEFICIARY  WHERE NP1_PROPOSAL ='"+Session["NP1_PROPOSAL"] + "'"
						//CUSTOM: ARABIC

						if (((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text.Trim()!="")
						{
							String strNewNBF_BENNAMEARABIC = ((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text;
							String strNewNBF_BENEFCD = ((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text;
							DB.executeDML("UPDATE LNBF_BENEFICIARY SET NBF_BENNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strNewNBF_BENNAMEARABIC)) + "' WHERE NP1_PROPOSAL ='"+Session["NP1_PROPOSAL"] + "' and NBF_BENEFCD="+columnNameValue.getObject("NBF_BENEFCD"));
						}
						//CUSTOM: ARABIC

						//entityClass.fsoperationAfterSave();
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNBF_BENEFICIARY");
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_DOB")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AGE")).Text ="";
						((DropDownList)footerItem.Cells[0].FindControl("ddlNewCRL_RELEATIOCD")).ClearSelection();
						((DropDownList)footerItem.Cells[0].FindControl("ddlNewNBF_BASIS")).ClearSelection();
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AMOUNT")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_PERCNTAGE")).Text ="";

						_RecordsSaved = true ;
						//if(Convert.ToInt16(columnNameValue["NBF_AGE]) < ace.
						if(Convert.ToInt16(columnNameValue["NBF_AGE"]) < ace.clsIlasConstant.AGE_LIMIT)
						{
							string loadGuardian = "openGuardian('" + Convert.ToString(columnNameValue["NP1_PROPOSAL"]) + "', " + Convert.ToString(columnNameValue["NBF_BENEFCD"]) + ",-999);";
							FooterScript.Text = loadGuardian;
						}
						else
						{
							FooterScript.Text = "";
						}
					}
					//}
					if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
					{
						//PrintMessage("Record(s) succesfully saved.");
					}
					break;
				case (EnumControlArgs.Delete):	
					FooterScript.Text = "";
					DB.BeginTransaction();
					SaveTransaction = true;			
					LNBF_BENEFICIARY_obj =new LNBF_BENEFICIARY(dataHolder);
					if (DeleteAll(LNBF_BENEFICIARY_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LNBF_BENEFICIARY_obj);			
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
							proccessCommand.setPrimaryKeys(LNBF_BENEFICIARY.PrimaryKeys);
							proccessCommand.setTableName("LNBF_BENEFICIARY");
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
				dataHolder.Data.Tables["LNBF_BENEFICIARY"].Rows.Clear();
				dataHolder = new LNBF_BENEFICIARYDB(dataHolder).GetILUS_ET_TB_BENEFECIARY_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LNBF_BENEFICIARY"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LNBF_BENEFICIARY"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LNBF_BENEFICIARY"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LNBF_BENEFICIARY"].Rows.Count.ToString();

			
			
			
			
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
			rowset rsCurr = DB.executeQuery("select a.PCU_CURRCODE, PCU_CURRDESC from  LNPR_PRODUCT a, PCU_CURRENCY b where a.PCU_CURRCODE=b.PCU_CURRCODE and a.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"' and nvl(NPR_BASICFLAG,'N')='Y'");


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

				//Button btnGuardian = (Button) e.Item.Cells[0].FindControl("btnGuardian");
				HtmlInputControl btnGuardian = (HtmlInputControl) e.Item.Cells[0].FindControl("btnGuardian");
				btnGuardian.Attributes.Add("onclick" ,"LoadGuardian(this);");

				
				TextBox txtNBF_BENNAME = (TextBox)e.Item.Cells[0].FindControl("txtNBF_BENNAME");
				txtNBF_BENNAME.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				TextBox txtNBF_BENNAMEARABIC = (TextBox)e.Item.Cells[0].FindControl("txtNBF_BENNAMEARABIC");
				txtNBF_BENNAMEARABIC.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				


				TextBox txtNBF_BENEFCD = (TextBox)e.Item.Cells[0].FindControl("lblNBF_BENEFCD");
				TextBox txtNGU_GUARDCD = (TextBox)e.Item.Cells[0].FindControl("lblNGU_GUARDCD");
				
				//Face Currency readonly view
				/*TextBox txtFACE_CURRENCY = (TextBox)e.Item.Cells[0].FindControl("txtFACE_CURRENCY");

				if (rsCurr.next())
					txtFACE_CURRENCY.Text = rsCurr.getString(2);
				else
					txtFACE_CURRENCY.Text = "";
				txtFACE_CURRENCY.ReadOnly = true;*/
				


				//CUSTOM: Arabic
				rowset rsArabic = DB.executeQuery("SELECT NBF_BENNAMEARABIC FROM LNBF_BENEFICIARY  WHERE NP1_PROPOSAL ='"+Session["NP1_PROPOSAL"] + "' and NBF_BENEFCD="+txtNBF_BENEFCD.Text);

				string BENNAMEARABIC=null;
				if(rsArabic.next())
					BENNAMEARABIC =  System.Text.Encoding.UTF8.GetString(Convert.FromBase64String((rsArabic.getObject("NBF_BENNAMEARABIC")==null?"":rsArabic.getString("NBF_BENNAMEARABIC"))));

				txtNBF_BENNAMEARABIC.Text=Convert.ToString(BENNAMEARABIC);

				//CUSTOM: ARABIC

				
				TextBox txtNBF_DOB = (TextBox)e.Item.Cells[0].FindControl("txtNBF_DOB");
				txtNBF_DOB.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNBF_DOB.Attributes.Add("onblur" ,"setDOB(this, '" + SessionObject.Get("NP2_COMMENDATE") + "');");

				TextBox txtNBF_AGE = (TextBox)e.Item.Cells[0].FindControl("txtNBF_AGE");
				txtNBF_AGE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				DropDownList ddlCRL_RELEATIOCD = (DropDownList)e.Item.Cells[0].FindControl("ddlCRL_RELEATIOCD");
				ddlCRL_RELEATIOCD.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				DropDownList ddlNBF_BASIS = (DropDownList)e.Item.Cells[0].FindControl("ddlNBF_BASIS");
				ddlNBF_BASIS.Attributes.Add("onchange" ,"setAmtPctStatus(this);SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				TextBox txtNBF_AMOUNT = (TextBox)e.Item.Cells[0].FindControl("txtNBF_AMOUNT");
				txtNBF_AMOUNT.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNBF_AMOUNT.Attributes.Add("onblur" ,"applyNumberFormat(this,2);");

				TextBox txtNBF_PERCNTAGE = (TextBox)e.Item.Cells[0].FindControl("txtNBF_PERCNTAGE");
				txtNBF_PERCNTAGE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNBF_PERCNTAGE.Attributes.Add("onblur" ,"checkNumber(this);applyNumberFormatPercent(this,2);");
				
				

				
				
				ListItem selectedItem =null;
				IDataReader drCRL_RELEATIOCD = LCRL_RELATIONDB.GetDDL_ILUS_ET_TB_BENEFECIARY_CRL_RELEATIOCD_RO();
				ddlCRL_RELEATIOCD.DataSource = drCRL_RELEATIOCD;
				ddlCRL_RELEATIOCD.DataBind();
				drCRL_RELEATIOCD.Close();
				selectedItem = ddlCRL_RELEATIOCD.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["CRL_RELEATIOCD"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;
				IDataReader drNBF_BASIS = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_BENEFECIARY_NBF_BASIS_RO();
				ddlNBF_BASIS.DataSource = drNBF_BASIS;
				ddlNBF_BASIS.DataBind();
				drNBF_BASIS.Close();
				selectedItem = ddlNBF_BASIS.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NBF_BASIS"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer)
			{	
				
				DropDownList ddlCRL_RELEATIOCD = (DropDownList)e.Item.Cells[0].FindControl("ddlNewCRL_RELEATIOCD");
				IDataReader drCRL_RELEATIOCD = LCRL_RELATIONDB.GetDDL_ILUS_ET_TB_BENEFECIARY_CRL_RELEATIOCD_RO();
				ddlCRL_RELEATIOCD.DataSource = drCRL_RELEATIOCD;
				ddlCRL_RELEATIOCD.DataBind();
				drCRL_RELEATIOCD.Close();
				DropDownList ddlNBF_BASIS = (DropDownList)e.Item.Cells[0].FindControl("ddlNewNBF_BASIS");
				IDataReader drNBF_BASIS = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_BENEFECIARY_NBF_BASIS_RO();
				ddlNBF_BASIS.DataSource = drNBF_BASIS;
				ddlNBF_BASIS.DataBind();
				drNBF_BASIS.Close();

				TextBox txtNewNBF_BENNAME = (TextBox)e.Item.Cells[0].FindControl("txtNewNBF_BENNAME");
				Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" + txtNewNBF_BENNAME.ClientID + "')!=null) document.getElementById('" + txtNewNBF_BENNAME.ClientID + "').focus();</script>");

				TextBox lblNewNP1_PROPOSAL = (TextBox)e.Item.Cells[0].FindControl("lblNewNP1_PROPOSAL");
				lblNewNP1_PROPOSAL.Text = (string)SessionObject.Get("NP1_PROPOSAL");
				TextBox lblNewNBF_BENEFCD = (TextBox)e.Item.Cells[0].FindControl("lblNewNBF_BENEFCD");
				lblNewNBF_BENEFCD.Text = (string)SessionObject.Get("NBF_BENEFCD");

				TextBox txtNewNBF_PERCNTAGE_onblur = (TextBox)e.Item.Cells[0].FindControl("txtNewNBF_PERCNTAGE");
				txtNewNBF_PERCNTAGE_onblur.Attributes["onkeydown"] += "if(checkNumber(this))callSend();";
				txtNewNBF_PERCNTAGE_onblur.Attributes["onblur"]    += "checkNumber(this);";
				//txtNewNBF_PERCNTAGE_onblur.Attributes.Add("onblur","if(checkNumber(this)) callSend();");

				TextBox txtNewNBF_DOB = (TextBox)e.Item.Cells[0].FindControl("txtNewNBF_DOB");
				txtNewNBF_DOB.Attributes.Add("onblur" ,"setDOB(this, '" + SessionObject.Get("NP2_COMMENDATE") + "');");

				TextBox txtNewNBF_AMOUNT = (TextBox)e.Item.Cells[0].FindControl("txtNewNBF_AMOUNT");
				txtNewNBF_AMOUNT.Attributes.Add("onblur" ,"applyNumberFormat(this,2);");


				//Face Currency readonly view
				/*TextBox txtNewFACE_CURRENCY = (TextBox)e.Item.Cells[0].FindControl("txtNewFACE_CURRENCY");

				if (rsCurr.next())
					txtNewFACE_CURRENCY.Text = rsCurr.getString(2);
				else
					txtNewFACE_CURRENCY.Text = "";
				txtNewFACE_CURRENCY.ReadOnly = true;*/



				DropDownList ddlNewNBF_BASIS = (DropDownList)e.Item.Cells[0].FindControl("ddlNewNBF_BASIS");
				ddlNewNBF_BASIS.Attributes.Add("onchange" ,"setAmtPctStatus(this);");

				
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
		private bool DeleteAll(LNBF_BENEFICIARY LNBF_BENEFICIARY_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

			//entityClass.setNameValueCollection(columnNameValue);

			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
					columnNameValue.Add("NBF_BENEFCD",((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text);
					columnNameValue.Add("NGU_GUARDCD",((TextBox)item.Cells[0].FindControl("lblNGU_GUARDCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNGU_GUARDCD")).Text);
					columnNameValue.Add("NBF_BENNAME",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text);
					columnNameValue.Add("NBF_BENNAMEARABIC",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text);
					columnNameValue.Add("NBF_DOB",((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text));
					columnNameValue.Add("NBF_AGE",((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text));
					columnNameValue.Add("CRL_RELEATIOCD",((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue);
					columnNameValue.Add("NBF_BASIS",((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue);
					columnNameValue.Add("NBF_AMOUNT",((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text);
					columnNameValue.Add("NBF_PERCNTAGE",((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text);

					//entityClass.fsoperationBeforeDelete();

					LNBF_BENEFICIARY_obj.Delete(columnNameValue);
					deleted=true;
					dataHolder.Update(DB.Transaction);				
					//entityClass.fsoperationAfterDelete();

					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNBF_BENEFICIARY");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LNBF_BENEFICIARY LNBF_BENEFICIARY_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			//shgn.SHGNCommand //entityClass=new mi.MI_ET_TB_Adherents();
			//entityClass.setNameValueCollection(columnNameValue);


			for (int i=0; i<mRows.Length-1; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
				columnNameValue.Add("NBF_BENEFCD",((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text);
				columnNameValue.Add("NBF_BENNAME",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text);
				
				//CUSTOM: ARABIC
				//columnNameValue.Add("NBF_BENNAMEARABIC",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text);
				
				String strNBF_BENNAMEARABIC = ((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text;
				//columnNameValue.Add("NBF_BENNAMEARABIC",Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strNBF_BENNAMEARABIC)));
				columnNameValue.Add("NBF_BENNAMEARABIC",(strNBF_BENNAMEARABIC==null?null:Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strNBF_BENNAMEARABIC))));
				//CUSTOM: ARABIC


				columnNameValue.Add("NBF_DOB",((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text));
				columnNameValue.Add("NBF_AGE",((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text));
				columnNameValue.Add("CRL_RELEATIOCD",((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue);
				columnNameValue.Add("NBF_BASIS",((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text);
				columnNameValue.Add("NBF_PERCNTAGE",((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text.Replace("%",""));

				//Beneficiary Guardian
				//if(Convert.ToInt16(item.Cells[0].FindControl("txtNBF_AGE")) >= 18)
				//((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()
				if(Convert.ToInt16(columnNameValue["NBF_AGE"]) >= 18)
				{
						columnNameValue.Add("NGU_GUARDCD",null);
				}
			
				//entityClass.fsoperationBeforeUpdate();
				LNBF_BENEFICIARY_obj.Update(columnNameValue);
				dataHolder.Update(DB.Transaction);
				
				//CUSTOM: ARABIC
				//String strNBF_BENNAMEARABIC = ((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text;
				//String strNBF_BENEFCD = ((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text;
				//DB.executeDML("UPDATE LNBF_BENEFICIARY SET NBF_BENNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strNBF_BENNAMEARABIC)) + "' WHERE NP1_PROPOSAL ='"+Session["NP1_PROPOSAL"] + "' and NBF_BENEFCD="+ strNBF_BENEFCD);
				//CUSTOM: ARABIC
				
				//entityClass.fsoperationAfterUpdate();
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNBF_BENEFICIARY.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNBF_BENEFICIARY");
			}

			//Beneficiary Guardian
			//if(LNBF_BENEFICIARYDB.IsGarbageGuardianExist(item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim())
			//{
			//	//LNGU_GUARDIANDB.R
			//}
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
				columnNameValue.Add("NBF_BENEFCD",((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNBF_BENEFCD")).Text);
				columnNameValue.Add("NGU_GUARDCD",((TextBox)item.Cells[0].FindControl("lblNGU_GUARDCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNGU_GUARDCD")).Text);
				columnNameValue.Add("NBF_BENNAME",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAME")).Text);
				columnNameValue.Add("NBF_BENNAMEARABIC",((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_BENNAMEARABIC")).Text);
				columnNameValue.Add("NBF_DOB",((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_DOB")).Text));
				columnNameValue.Add("NBF_AGE",((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNBF_AGE")).Text));
				columnNameValue.Add("CRL_RELEATIOCD",((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlCRL_RELEATIOCD")).SelectedValue);
				columnNameValue.Add("NBF_BASIS",((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNBF_BASIS")).SelectedValue);
				columnNameValue.Add("NBF_AMOUNT",((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_AMOUNT")).Text);
				columnNameValue.Add("NBF_PERCNTAGE",((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtNBF_PERCNTAGE")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
			columnNameValue.Add("NBF_BENEFCD",((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNBF_BENEFCD")).Text);
			//columnNameValue.Add("NGU_GUARDCD",((TextBox)footerItem.Cells[0].FindControl("lblNewNGU_GUARDCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNGU_GUARDCD")).Text);
			columnNameValue.Add("NBF_BENNAME",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAME")).Text);
			columnNameValue.Add("NBF_BENNAMEARABIC",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_BENNAMEARABIC")).Text);
			columnNameValue.Add("NBF_DOB",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_DOB")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_DOB")).Text));
			columnNameValue.Add("NBF_AGE",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AGE")).Text));
			columnNameValue.Add("CRL_RELEATIOCD",((DropDownList)footerItem.Cells[0].FindControl("ddlNewCRL_RELEATIOCD")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewCRL_RELEATIOCD")).SelectedValue);
			columnNameValue.Add("NBF_BASIS",((DropDownList)footerItem.Cells[0].FindControl("ddlNewNBF_BASIS")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewNBF_BASIS")).SelectedValue);
			columnNameValue.Add("NBF_AMOUNT",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AMOUNT")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_AMOUNT")).Text);
			columnNameValue.Add("NBF_PERCNTAGE",((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_PERCNTAGE")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewNBF_PERCNTAGE")).Text);

			return columnNameValue;		
		}

	}
}

