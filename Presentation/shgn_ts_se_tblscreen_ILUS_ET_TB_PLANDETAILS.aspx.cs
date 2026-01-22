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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal QueryStringParameter;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		
		
		

		protected bool _RecordsUpdated = false ;
		protected bool _RecordsSaved  = false ;
		private SHMA.Enterprise.NameValueCollection nameValueSelected;

		shgn.SHGNCommand entityClass;
		
		double valNPR_PREMIUM = 0.0;
		
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
			dataHolder = new LNPR_PRODUCTDB(dataHolder).GetILUS_ET_TB_PLANDETAILS_Data();
			return   dataHolder;         
		}


		private void setClientUI()
		{
			//************ Fix hiding **************
			EntryGrid.Columns[6].HeaderStyle.CssClass ="hide";
			EntryGrid.Columns[6].ItemStyle.CssClass ="hide";
			EntryGrid.Columns[6].FooterStyle.CssClass ="hide";

			EntryGrid.Columns[7].HeaderStyle.CssClass ="hide";
			EntryGrid.Columns[7].ItemStyle.CssClass ="hide";
			EntryGrid.Columns[7].FooterStyle.CssClass ="hide";

			EntryGrid.Columns[8].HeaderStyle.CssClass ="hide";
			EntryGrid.Columns[8].ItemStyle.CssClass ="hide";
			EntryGrid.Columns[8].FooterStyle.CssClass ="hide";

			EntryGrid.Columns[9].HeaderStyle.CssClass ="hide";
			EntryGrid.Columns[9].ItemStyle.CssClass ="hide";
			EntryGrid.Columns[9].FooterStyle.CssClass ="hide";

			//************ Parameterization *********************
			rowset rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI WHERE PCM_COMPCODE=SV(\"s_PCM_COMPCODE\") AND CCN_CTRYCD=SV(\"s_CCN_CTRYCD\") AND CUI_SCREENID='RIDER'"));
			while(rs.next())
			{
				int index = getColumnIndexByName(rs.getString("CUI_COLUMNID"));
				if(index != -1)
				{
					if(rs.getObject("CUI_VISIBILE") != null)
					{
						if(rs.getString("CUI_VISIBILE").ToUpper() == "Y")
						{
							EntryGrid.Columns[index].HeaderStyle.CssClass ="";
							EntryGrid.Columns[index].ItemStyle.CssClass ="";
							EntryGrid.Columns[index].FooterStyle.CssClass ="";
						}
					}
				}
			}
			//EntryGrid.Columns[2].HeaderText ="test1";
		}
		private int getColumnIndexByName(string columnName)
		{
			if(columnName.IndexOf("NPR_EDUNITS") > -1)
			{
				return 6;
			}
			else if(columnName.IndexOf("NPR_COMMLOADING") > -1)
			{
				return 7;
			}
			else
			{
				return -1;
			}
		}


		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			
			//****************CUSTOM SECTION ******************/
			LoadSelected();
			LoadQueryStringParameter();
			//****************CUSTOM SECTION ******************/


			try
			{
				EntryGrid.DataSource = dataHolder["LNPR_PRODUCT"];
				EntryGrid.DataBind();
				setClientUI();
			}
			catch(Exception e)
			{
				throw e;
			}

			_totalRecords.Text = dataHolder["LNPR_PRODUCT"].Rows.Count.ToString();

			string newValidation = "var newValidation='N';";
			if(ace.ValidationUtility.isNewValidation())
			{
				newValidation = "var newValidation='Y';";
			}

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse(newValidation + "") ;


		}
		
		
		protected bool InsertAllowed 
		{
			/*get	
			{
				return true; 
			}*/

			get	
			{
				String _totalRecordsShadowed = "1";//dataHolder["LNBF_BENEFICIARY"].Rows.Count.ToString();//with a value of 1 want to disable new record mechanism

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
			dataHolder = new LNPR_PRODUCTDB(dataHolder).GetILUS_ET_TB_PLANDETAILS_Data();
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SessionObject.Set("VALIDATION_ERROR","");
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LNPR_PRODUCT LNPR_PRODUCT_obj;
			NameValueCollection columnNameValue=new NameValueCollection();
			entityClass=new ace.ILUS_ET_TB_PLANDETAILS();
			entityClass.setNameValueCollection(columnNameValue);


			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;
					LNPR_PRODUCT_obj =new LNPR_PRODUCT(dataHolder);
					UpdateAll(LNPR_PRODUCT_obj);
					DataGridItem footerItem = (DataGridItem)EntryGrid.Controls[0].Controls[EntryGrid.Controls[0].Controls.Count-1];

					
					//TODO: Insert value from Session
					((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text = ""+SessionObject.Get("NP1_PROPOSAL");//"R/07/0010042";
					((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text = "1";
					//((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text = "0";

					
					//if(((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_SELECTED")).SelectedValue.Length>0)
					if(false)//no new record in model
						{
						//if(((TextBox)footerItem.Cells[0].FindControl("<notnull-field>")).Text.Length>0)
						//{

						
						
						columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
						columnNameValue.Add("NP2_SETNO",((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text));
						//columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text);
						
						//columnNameValue.Add("NPR_SELECTED",((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_SELECTED")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_SELECTED")).SelectedValue);
						columnNameValue.Add("NPR_SELECTED",((CheckBox)footerItem.Cells[0].FindControl("chkNewNPR_SELECTED")).Checked?"Y":"N");


						columnNameValue.Add("NPR_SUMASSURED",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_SUMASSURED")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_SUMASSURED")).Text));
						columnNameValue.Add("NPR_BENEFITTERM",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_BENEFITTERM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_BENEFITTERM")).Text));
						columnNameValue.Add("NPR_EDUNITS",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_EDUNITS")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_EDUNITS")).Text));
						columnNameValue.Add("NPR_PREMIUM",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_PREMIUM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_PREMIUM")).Text));
						columnNameValue.Add("NPR_COMMLOADING",((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_COMMLOADING")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_COMMLOADING")).SelectedValue);
						columnNameValue.Add("NPR_BASICFLAG",((TextBox)footerItem.Cells[0].FindControl("lblNewNPR_BASICFLAG")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNPR_BASICFLAG")).Text);

						entityClass.fsoperationBeforeSave();

						LNPR_PRODUCT_obj.Add(columnNameValue,GetAnItemData(footerItem),"ILUS_ET_TB_PLANDETAILS","PPR_PRODCD");

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterSave();


						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPR_PRODUCT");
						//((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text ="";
						((CheckBox)footerItem.Cells[0].FindControl("chkNewNPR_SELECTED")).Checked = false;
						((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_SUMASSURED")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_BENEFITTERM")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_EDUNITS")).Text ="";
						((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_PREMIUM")).Text ="";
						((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_COMMLOADING")).ClearSelection();

						_RecordsSaved = true ;
					}
					//}

					try
					{
						entityClass.fsoperationAfterSave();
					}
					catch(FieldValidationException ex)
                    {
                        string reDirectUrl = string.Empty;

						_totalRecords.Text = dataHolder["LNPR_PRODUCT"].Rows.Count.ToString();
						SessionObject.Set("VALIDATION_ERROR",ex.Message);
						//ValidationError.Text = "openValidationError();";
                        Response.Redirect("../Presentation/ValidationError.aspx?ErrorSource=Benefit(s) Validation Error");
						//throw new ProcessException("<<Validation Error>>");

					}

					if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
					{
						//PrintMessage("Record(s) succesfully saved.");
						//Manu coding
					}
					
					//Manual Coding for Calculate Premium (by using parent screen (Plan/Rider)
					FooterScript.Text =  FooterScript.Text + "calcPremium();";
					break;
				case (EnumControlArgs.Delete):					
					DB.BeginTransaction();
					SaveTransaction = true;			
					LNPR_PRODUCT_obj =new LNPR_PRODUCT(dataHolder);
					if (DeleteAll(LNPR_PRODUCT_obj))
					{
						//PrintMessage("Record(s) succesfully deleted.");
					}
						

					//DeleteAll(LNPR_PRODUCT_obj);			
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
							proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
							proccessCommand.setTableName("LNPR_PRODUCT");
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

			//****************CUSTOM SECTION ******************/
			LoadSelected();
			//****************CUSTOM SECTION ******************/
			
			
			txtModifiedRows.Text = "" ;

			if(ReFetchOnBind)
			{
				dataHolder.Data.Tables["LNPR_PRODUCT"].Rows.Clear();
				dataHolder = new LNPR_PRODUCTDB(dataHolder).GetILUS_ET_TB_PLANDETAILS_Data();
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LNPR_PRODUCT"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = dataHolder["LNPR_PRODUCT"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = dataHolder["LNPR_PRODUCT"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LNPR_PRODUCT"].Rows.Count.ToString();

			
			
			
			
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

			rowset rsCurrFace = DB.executeQuery("select a.PCU_CURRCODE, PCU_CURRSHORT from  LNPR_PRODUCT a, PCU_CURRENCY b where a.PCU_CURRCODE=b.PCU_CURRCODE and a.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"' and nvl(NPR_BASICFLAG,'N')='Y'");
			rowset rsCurrPrem = DB.executeQuery("select a.PCU_CURRCODE, PCU_CURRSHORT from  lnp1_policymastr a, PCU_CURRENCY b where a.PCU_CURRCODE=b.PCU_CURRCODE and a.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"'");

			
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
				
				TextBox txtPPR_PRODCD = (TextBox)e.Item.Cells[0].FindControl("txtPPR_PRODCD");
				txtPPR_PRODCD.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtPPR_PRODCD.Enabled=false;

				//DropDownList ddlNPR_SELECTED = (DropDownList)e.Item.Cells[0].FindControl("ddlNPR_SELECTED");
				//ddlNPR_SELECTED.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');setValidatorState(this);");

				CheckBox chkNPR_SELECTED = (CheckBox)e.Item.Cells[0].FindControl("chkNPR_SELECTED");
				chkNPR_SELECTED.Attributes.Add("onclick" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');setValidatorState(this);");
				//chkNPR_SELECTED.Attributes.Add("onclick" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');setValidatorState(this);");

				
				TextBox txtNPR_SUMASSURED = (TextBox)e.Item.Cells[0].FindControl("txtNPR_SUMASSURED");
				txtNPR_SUMASSURED.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				//txtNPR_SUMASSURED.Attributes.Add("onblur" ,"applyNumberFormat(this, 2);validateRangeOnBlur(this,'SUMASSURED');");
				txtNPR_SUMASSURED.Attributes.Add("onblur" ,"validateRangeOnBlur(this,'SUMASSURED');");

				txtNPR_SUMASSURED.Attributes.Add("onfocus","validateRangeInfoOnFocus(this,'SUMASSURED');");



				//Face Currency

				TextBox txtFaceCurrency = (TextBox)e.Item.Cells[0].FindControl("txtFaceCurrency");
				if (rsCurrFace.next())
					txtFaceCurrency.Text = rsCurrFace.getString(2);
				else
					txtFaceCurrency.Text = "";
				txtFaceCurrency.ReadOnly = true;

				TextBox txtPremiumCurrency = (TextBox)e.Item.Cells[0].FindControl("txtPremiumCurrency");
				if (rsCurrPrem.next())
					txtPremiumCurrency.Text = rsCurrFace.getString(2);
				else
					txtPremiumCurrency.Text = "";
				txtPremiumCurrency.ReadOnly = true;


				
				TextBox txtNPR_BENEFITTERM = (TextBox)e.Item.Cells[0].FindControl("txtNPR_BENEFITTERM");
				txtNPR_BENEFITTERM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPR_BENEFITTERM.Attributes.Add("onblur","validateRangeOnBlur(this,'BTERM');");
				txtNPR_BENEFITTERM.Attributes.Add("onfocus","validateRangeInfoOnFocus(this,'BTERM');");

				TextBox txtNPR_EDUNITS = (TextBox)e.Item.Cells[0].FindControl("txtNPR_EDUNITS");
				txtNPR_EDUNITS.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPR_EDUNITS.Attributes.Add("onblur","LostFocus_EDUNITS(this);");//validateRangeOnBlur(this,'BTERM');
				//txtNPR_EDUNITS.Attributes.Add("onfocus","validateRangeInfoOnFocus(this,'BTERM');");

				
				
				TextBox txtNPR_PREMIUM = (TextBox)e.Item.Cells[0].FindControl("txtNPR_PREMIUM");
				txtNPR_PREMIUM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				DropDownList ddlNPR_COMMLOADING = (DropDownList)e.Item.Cells[0].FindControl("ddlNPR_COMMLOADING");
				ddlNPR_COMMLOADING.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");


				TextBox txtPPR_DESCR = (TextBox)e.Item.Cells[0].FindControl("txtPPR_DESCR");
				
				//exceptional case should be sent to hell
				string riderDesc = getRiderDescr(txtPPR_PRODCD.Text);
				try
				{
					txtPPR_DESCR.ToolTip = getRiderHelp(txtPPR_PRODCD.Text);
					//txtPPR_DESCR.Text = riderDesc.Split('-')[0];
					txtPPR_DESCR.Text = riderDesc;
				}
				catch(Exception x)
				{
					txtPPR_DESCR.Text = riderDesc;
				}


				valNPR_PREMIUM += Double.Parse(txtNPR_PREMIUM.Text.Equals("")?"0.0":txtNPR_PREMIUM.Text);
				

				//****************CUSTOM SECTION ******************
				//DOMAIN/MODEL LOGIC applied to VIEW
				//take the value of cpu code to query collection whether selected it's yes or not

				CheckBox checkedYN = (CheckBox)e.Item.Cells[0].FindControl("chkNPR_SELECTED");


				string product = txtPPR_PRODCD.Text;

				//get the value of the selected for selected?
				String selectedValue = ""+nameValueSelected.get(product);

				//if selected = 'Y' then checkbox should render selected else wise-versa
				bool selected = selectedValue.Equals("Y")?true:false;
				if (selected)
					checkedYN.Checked = true;


				//Parameterized Enable Disable Mechanism

				TextBox txtPPR_SUMASSURED_ENABLED = (TextBox)e.Item.Cells[0].FindControl("txtPPR_SUMASSURED_ENABLED");
				TextBox txtPPR_BENEFITTERM_ENABLED = (TextBox)e.Item.Cells[0].FindControl("txtPPR_BENEFITTERM_ENABLED");
				TextBox txtNPR_EDUNITS_ENABLED = (TextBox)e.Item.Cells[0].FindControl("txtNPR_EDUNITS_ENABLED");
				TextBox txtPPR_COMMLOADING_ENABLED = (TextBox)e.Item.Cells[0].FindControl("txtPPR_COMMLOADING_ENABLED");
				TextBox txtNPR_SUMASSUREDs = (TextBox)e.Item.Cells[0].FindControl("txtNPR_SUMASSURED");

				

				/*string benefitterm=Convert.ToString(Session["RIDER_BENEFITTERM"]);
				if(benefitterm=="")
				{
					txtNPR_BENEFITTERM.Text=Session["NPR_BENEFITTERM"].ToString();				
				}
				else
				{				
					txtNPR_BENEFITTERM.Text=Session["RIDER_BENEFITTERM"].ToString();
				}
				*/
				//txtNPR_BENEFITTERM.Text = (TextBox)e.Item.Cells[0].FindControl("NPR_BENEFITTERM");

                
				//if(txtNPR_SUMASSURED.Text.Trim()!="" && Convert.ToDecimal(txtNPR_SUMASSURED.Text.Trim())<=0)
				//	txtNPR_SUMASSURED.Text=Session["NPR_SUMASSURED"].ToString();

				
				//txtNPR_BENEFITTERM.Text=Session["NPR_BENEFITTERM"].ToString();				
				
				rowset rsParaDisabilities = DB.executeQuery("SELECT nvl(PPR_SUMASSUREDENABLED,'Y') PPR_SUMASSURED_ENABLED,nvl(PPR_BENEFITTERMENABLED,'Y') PPR_BENEFITTERM_ENABLED,nvl(PPR_COMMLOADINGENABLED,'Y') PPR_COMMLOADING_ENABLED FROM LPPR_PRODUCT  WHERE PPR_PRODCD='" + txtPPR_PRODCD.Text + "'" );
				if (rsParaDisabilities.next())
				{
					txtPPR_SUMASSURED_ENABLED.Text = rsParaDisabilities.getString("PPR_SUMASSURED_ENABLED");
					txtPPR_BENEFITTERM_ENABLED.Text = rsParaDisabilities.getString("PPR_BENEFITTERM_ENABLED");
					txtNPR_EDUNITS_ENABLED.Text = rsParaDisabilities.getString("PPR_BENEFITTERM_ENABLED");
					txtPPR_COMMLOADING_ENABLED.Text = rsParaDisabilities.getString("PPR_COMMLOADING_ENABLED");
				}


				
				//Threshold Mechanism

				TextBox txtMinThresholdValue = (TextBox)e.Item.Cells[0].FindControl("txtMinThresholdValue");
				TextBox txtMaxThresholdValue = (TextBox)e.Item.Cells[0].FindControl("txtMaxThresholdValue");

				//TODO: fill by db
				txtMinThresholdValue.Text = "3000";
				txtMaxThresholdValue.Text = "15000000";

				

				
				
				//****************CUSTOM SECTION ******************/


				
				
				ListItem selectedItem =null;
				
				//selectedItem = ddlNPR_SELECTED.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NPR_SELECTED"].ToString());
				//if (selectedItem!=null)
				//	selectedItem.Selected=true;
				
				IDataReader drNPR_COMMLOADING = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_PLANDETAILS_NPR_COMMLOADING_RO();
				ddlNPR_COMMLOADING.DataSource = drNPR_COMMLOADING;
				ddlNPR_COMMLOADING.DataBind();
				drNPR_COMMLOADING.Close();
				selectedItem = ddlNPR_COMMLOADING.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NPR_COMMLOADING"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;

			}				
			else if (e.Item.ItemType==ListItemType.Footer)
			{	
				
				DropDownList ddlNPR_COMMLOADING = (DropDownList)e.Item.Cells[0].FindControl("ddlNewNPR_COMMLOADING");
				IDataReader drNPR_COMMLOADING = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_PLANDETAILS_NPR_COMMLOADING_RO();
				ddlNPR_COMMLOADING.DataSource = drNPR_COMMLOADING;
				ddlNPR_COMMLOADING.DataBind();
				drNPR_COMMLOADING.Close();

				//TextBox txtNewPPR_PRODCD = (TextBox)e.Item.Cells[0].FindControl("txtNewPPR_PRODCD");
				//Page.RegisterStartupScript("focus", "<script language=javascript>if(document.getElementById('" + txtNewPPR_PRODCD.ClientID + "')!=null) document.getElementById('" + txtNewPPR_PRODCD.ClientID + "').focus();</script>");

				TextBox lblNewNP1_PROPOSAL = (TextBox)e.Item.Cells[0].FindControl("lblNewNP1_PROPOSAL");
				lblNewNP1_PROPOSAL.Text = ""+SessionObject.Get("NP1_PROPOSAL");
				TextBox lblNewNP2_SETNO = (TextBox)e.Item.Cells[0].FindControl("lblNewNP2_SETNO");
				lblNewNP2_SETNO.Text = ""+SessionObject.Get("NP2_SETNO");
				TextBox lblNewNPR_BASICFLAG = (TextBox)e.Item.Cells[0].FindControl("lblNewNPR_BASICFLAG");
				lblNewNPR_BASICFLAG.Text = ""+SessionObject.Get("NPR_BASICFLAG");

				DropDownList ddlNewNPR_COMMLOADING_onblur = (DropDownList)e.Item.Cells[0].FindControl("ddlNewNPR_COMMLOADING");
				ddlNewNPR_COMMLOADING_onblur.Attributes["onkeydown"] += "callSend();";

				
				
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
		private bool DeleteAll(LNPR_PRODUCT LNPR_PRODUCT_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

			entityClass.setNameValueCollection(columnNameValue);

			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				//if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				//{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
					columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text));
					columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text);
					columnNameValue.Add("NPR_SELECTED",((CheckBox)item.Cells[0].FindControl("chkNPR_SELECTED")).Checked?"Y":"N");
					columnNameValue.Add("NPR_SUMASSURED",((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text));
					columnNameValue.Add("NPR_BENEFITTERM",((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text));
					columnNameValue.Add("NPR_EDUNITS",((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text));
					columnNameValue.Add("NPR_PREMIUM",((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text));
					columnNameValue.Add("NPR_COMMLOADING",((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue);
					columnNameValue.Add("NPR_BASICFLAG",((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text);

					//entityClass.fsoperationBeforeDelete();

					LNPR_PRODUCT_obj.Delete(columnNameValue);
					deleted=true;
					dataHolder.Update(DB.Transaction);				
					entityClass.fsoperationAfterDelete();

					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPR_PRODUCT");
				//}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LNPR_PRODUCT LNPR_PRODUCT_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			//shgn.SHGNCommand entityClass=new ace.ILUS_ET_TB_PLANDETAILS();
			entityClass.setNameValueCollection(columnNameValue);

			try
			{

				for (int i=0; i<mRows.Length-1; i++)
				{
					_RecordsUpdated = true ;
					columnNameValue=new NameValueCollection();
					DataGridItem item = EntryGrid.Items[int.Parse(mRows[i].ToString())];
				
					columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text);
					columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text));
					columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text);
					//columnNameValue.Add("NPR_SELECTED",((DropDownList)item.Cells[0].FindControl("ddlNPR_SELECTED")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNPR_SELECTED")).SelectedValue);
					
					CheckBox val = (CheckBox)item.Cells[0].FindControl("chkNPR_SELECTED");
					bool val1 = val.Checked ;
					columnNameValue.Add("NPR_SELECTED",((CheckBox)item.Cells[0].FindControl("chkNPR_SELECTED")).Checked?"Y":"N");
					
					columnNameValue.Add("NPR_SUMASSURED",((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text));
					columnNameValue.Add("NPR_BENEFITTERM",((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text));
					columnNameValue.Add("NPR_EDUNITS",((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text));
					columnNameValue.Add("NPR_PREMIUM",((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text));
					columnNameValue.Add("NPR_COMMLOADING",((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue);
					columnNameValue.Add("NPR_BASICFLAG",((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text);

			
					entityClass.setNameValueCollection(columnNameValue);
					
					entityClass.fsoperationBeforeUpdate();
				
					
					LNPR_PRODUCT_obj.Update(columnNameValue);
				
					dataHolder.Update(DB.Transaction);
					//entityClass.fsoperationAfterUpdate();


					string proposal = ((TextBox)item.Cells[0].FindControl("lblNP1_PROPOSAL")).Text;
					string product  = ((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text;
					double BTerm    = double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text);
					int intBTerm    = Convert.ToInt16(BTerm);
					double EDUnit   = double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text);
					
					DateTime CommDate = ace.clsIlasUtility.getCommencementDate(proposal);
					DateTime MatDate  = new DateTime(CommDate.Year+intBTerm, CommDate.Month, CommDate.Day);	//chg-04032024 Feb-29 issue comments the line 

					SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
					pc.puts("@NPR_BENEFITTERM",  BTerm);
					pc.puts("@NPR_EDUNITS",      EDUnit);
					pc.puts("@NPR_MATURITYDATE", MatDate);      //chg-04032024 Feb-29 issue comments the line 
					pc.puts("@NP1_PROPOSAL",     proposal);
					pc.puts("@PPR_PRODCD",       product);
					//DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=" + BTerm.ToString() +",NPR_EDUNITS=" + EDUnit.ToString() + " WHERE NP1_PROPOSAL = '"+ proposal +"' and PPR_PRODCD='"+ product +"'");
					DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_EDUNITS=?, NPR_MATURITYDATE=? WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ", pc);   //chg-04032024 Feb-29 issue comments the line and add below	
					//DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_EDUNITS=?, NPR_MATURITYDATE=to_date('29/02/2024','DD/MM/YYYY') WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ", pc);

					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPR_PRODUCT");
					//ace.CallValidation.temp();
				}
			}
			catch (Exception e)
			{
				//string sCatch = e.Message;
				//PrintMessage(sCatch);
				throw new ProcessException(e.Message);
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
				columnNameValue.Add("NP2_SETNO",((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("lblNP2_SETNO")).Text));
				columnNameValue.Add("PPR_PRODCD",((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("txtPPR_PRODCD")).Text);
				columnNameValue.Add("NPR_SELECTED",((CheckBox)item.Cells[0].FindControl("chkNPR_SELECTED")).Checked?"Y":"N");
				columnNameValue.Add("NPR_SUMASSURED",((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_SUMASSURED")).Text));
				columnNameValue.Add("NPR_BENEFITTERM",((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_BENEFITTERM")).Text));
				columnNameValue.Add("NPR_EDUNITS",((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_EDUNITS")).Text));
				columnNameValue.Add("NPR_PREMIUM",((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.Cells[0].FindControl("txtNPR_PREMIUM")).Text));
				columnNameValue.Add("NPR_COMMLOADING",((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue.Trim()==""?null:((DropDownList)item.Cells[0].FindControl("ddlNPR_COMMLOADING")).SelectedValue);
				columnNameValue.Add("NPR_BASICFLAG",((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text.Trim()==""?null:((TextBox)item.Cells[0].FindControl("lblNPR_BASICFLAG")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			columnNameValue.Add("NP1_PROPOSAL",((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNP1_PROPOSAL")).Text);
			columnNameValue.Add("NP2_SETNO",((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("lblNewNP2_SETNO")).Text));
			//columnNameValue.Add("PPR_PRODCD",((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("txtNewPPR_PRODCD")).Text);
			columnNameValue.Add("NPR_SELECTED",((CheckBox)footerItem.Cells[0].FindControl("chkNewNPR_SELECTED")).Checked?"Y":"N");
			columnNameValue.Add("NPR_SUMASSURED",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_SUMASSURED")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_SUMASSURED")).Text));
			columnNameValue.Add("NPR_BENEFITTERM",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_BENEFITTERM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_BENEFITTERM")).Text));
			columnNameValue.Add("NPR_EDUNITS",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_EDUNITS")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_EDUNITS")).Text));
			columnNameValue.Add("NPR_PREMIUM",((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_PREMIUM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footerItem.Cells[0].FindControl("txtNewNPR_PREMIUM")).Text));
			columnNameValue.Add("NPR_COMMLOADING",((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_COMMLOADING")).SelectedValue.Trim()==""?null:((DropDownList)footerItem.Cells[0].FindControl("ddlNewNPR_COMMLOADING")).SelectedValue);
			columnNameValue.Add("NPR_BASICFLAG",((TextBox)footerItem.Cells[0].FindControl("lblNewNPR_BASICFLAG")).Text.Trim()==""?null:((TextBox)footerItem.Cells[0].FindControl("lblNewNPR_BASICFLAG")).Text);

			return columnNameValue;		
		}

	
		protected override void PrepareInputUI(DataHolder dataHolder)
		{
			double valNPR_PREMIUM1 = valNPR_PREMIUM;
			SessionObject.Set("total_NPR_PREMIUM", ""+valNPR_PREMIUM);

		}


		private String getRiderDescr(String code)
		{
			rowset product = DB.executeQuery("select PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_PRODCD='"+code+"'");
			if (product.next())
				return product.getString(1);
			else
				return "";
		}

		private String getRiderHelp(String code)
		{
			rowset product = DB.executeQuery("select PPR_COVERAGEDESC  FROM LPPR_PRODUCT   WHERE PPR_PRODCD='"+code+"'");
			if (product.next())
				return product.getString(1);
			else
				return "";
		}

		private void LoadSelected()
		{
			nameValueSelected = new NameValueCollection();
			
			String strSQL = "select * from  LNPR_PRODUCT where NP1_PROPOSAL = '"+SessionObject.Get("NP1_PROPOSAL")+"' and nvl(NPR_BASICFLAG,'N')='N'";//TODO pick value from session object

			rowset rsLNPR_PRODUCT = DB.executeQuery(strSQL);
			
			while (rsLNPR_PRODUCT.next())
			{
				nameValueSelected.Add(rsLNPR_PRODUCT.getString("PPR_PRODCD"),  rsLNPR_PRODUCT.getString("NPR_SELECTED"));
			}

		
		}

		private void LoadQueryStringParameter()
		{
			string strProposal = Request.QueryString["proposal"].ToString();  
			string strAge = Request.QueryString["age"].ToString();
			string strTerm = Request.QueryString["term"].ToString();
			string strSA = Request.QueryString["sa"].ToString();
			string strCalcBasis = Request.QueryString["calcBasis"].ToString();
			QueryStringParameter.Text = "var strProposal='" + strProposal + "'; var strAge=" + strAge + "; var strTerm='" + strTerm + "'; var strSA=" + strSA + "; var strCalcBasis='" + strCalcBasis + "';";
		}
	}

}

