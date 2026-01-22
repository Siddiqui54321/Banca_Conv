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
	public partial class shgn_ts_se_tblscreen_ILUS_ET_TB_VERTICALWITHDRAWAL : SHMA.Enterprise.Presentation.TwoStepController
	{




		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal _totalRecords;
		protected System.Web.UI.WebControls.Literal processCall;
		
		
		
		//protected SHMA.Enterprise.Presentation.WebControls.DataGrid EntryGrid;
		
		protected bool _RecordsUpdated = false ;
		protected bool _RecordsSaved  = false ;
		int shrinkSerial = -1;
		private int countPW = 0;
		private int countAD = 0;
		private int countSW = 0;

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
			this.EntryGrid_Adhoc.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.EntryGrid_Adhoc_ItemDataBound);
			this.EntryGrid_Switch.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.EntryGrid_Switch_ItemDataBound);

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
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_Data();
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_ADHOC_Data();
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_SWITCH_Data();
			
			//dataHolder = ExpandDataHolder(dataHolder);

			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL");
			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_ADHOC");
			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_SWITCH");

			
			return   dataHolder;         
		}

		protected DataHolder ExpandDataHolder(DataHolder dataHolder, string dataTableName)
		{

			DataRow r = null;
			
			//int shrinkSerial = -1; 
			
			if (dataTableName.Equals("LNPW_PARTIALWITHDRAWAL"))
				countPW = dataHolder["LNPW_PARTIALWITHDRAWAL"].Rows.Count;
			else if (dataTableName.Equals("LNPW_PARTIALWITHDRAWAL_ADHOC"))
				countAD = dataHolder["LNPW_PARTIALWITHDRAWAL_ADHOC"].Rows.Count;
			else if (dataTableName.Equals("LNPW_PARTIALWITHDRAWAL_SWITCH"))
				countSW = dataHolder["LNPW_PARTIALWITHDRAWAL_SWITCH"].Rows.Count;

			
			while (dataHolder.Data.Tables[dataTableName].Rows.Count < 10)
			{
				r = dataHolder.Data.Tables[dataTableName].NewRow();				
			
				//row's col 10 or index 9th is NP1_PROPOSAL
				r[0] = ""+(shrinkSerial--);
				r["NP1_PROPOSAL"] = ""+Session["NP1_PROPOSAL"];//dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL"].Rows[0][9];				
				//for(int j = 1; j <= dt.Rows.Count; j++)					
				//r[j] = dt.Rows[j - 1][k];				
				dataHolder.Data.Tables[dataTableName].Rows.Add(r);
			}
			return dataHolder; 
		}


		protected DataHolder ShrinkDataHolder(DataHolder dataHolder, string dataTableName)
		{

			/*foreach (DataRow r in dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL"].Rows)
			{
				if(r[0] is System.DBNull)
					dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL"].Rows.Remove(r);
			}*/

			for (int i =0; i< dataHolder.Data.Tables[dataTableName].Rows.Count; i++)
			{
				DataRow r = dataHolder.Data.Tables[dataTableName].Rows[i];
				//if(r[0] is System.DBNull)
				if(System.Decimal.Parse("-1").CompareTo(r[0])>=0)
				{
					dataHolder.Data.Tables[dataTableName].Rows.Remove(r);
					i--;
				}
			}


			return dataHolder; 

		}




		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			EntryGrid.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL"];
			EntryGrid_Adhoc.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL_ADHOC"];
			EntryGrid_Switch.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL_SWITCH"];
			EntryGrid.DataBind();
			EntryGrid_Adhoc.DataBind();
			EntryGrid_Switch.DataBind();
			
			_totalRecords.Text = dataHolder["LNPW_PARTIALWITHDRAWAL"].Rows.Count.ToString();

			HeaderScript.Text = EnvHelper.Parse("var product=SV(\"PPR_PRODCD\");");
			FooterScript.Text = EnvHelper.Parse("");
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
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_Data();
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_ADHOC_Data();
			dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_SWITCH_Data();

			//dataHolder = ExpandDataHolder(dataHolder);

			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL");
			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_ADHOC");
			dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_SWITCH");


			
			
			return dataHolder;
		}      
		
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{			
			processCall.Text = "";
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			SaveTransaction = false;			
			LNPW_PARTIALWITHDRAWAL LNPW_PARTIALWITHDRAWAL_obj;

			NameValueCollection columnNameValue=new NameValueCollection();
			entityClass=new ace.ILUS_ET_TB_VERTICALWITHDRAWAL();
			entityClass.setNameValueCollection(columnNameValue);


			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					DB.BeginTransaction();
					SaveTransaction = true;

					dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_ALL_Data();

					dataHolder = ShrinkDataHolder(dataHolder,"LNPW_PARTIALWITHDRAWAL");
					dataHolder = ShrinkDataHolder(dataHolder,"LNPW_PARTIALWITHDRAWAL_ADHOC");
					dataHolder = ShrinkDataHolder(dataHolder,"LNPW_PARTIALWITHDRAWAL_SWITCH");

					LNPW_PARTIALWITHDRAWAL_obj =new LNPW_PARTIALWITHDRAWAL(dataHolder);


					DB.executeDML("delete from LNPW_PARTIALWITHDRAWAL where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"'");

					UpdateAll(LNPW_PARTIALWITHDRAWAL_obj);
					UpdateAll_Adhoc(LNPW_PARTIALWITHDRAWAL_obj);
					UpdateAll_Switch(LNPW_PARTIALWITHDRAWAL_obj);

					if ((_RecordsSaved == true) || (_RecordsUpdated == true) )
					{
						//Manual Coding
						//PrintMessage("Record(s) succesfully saved.");
						processCall.Text = "executeProcess('ace.Call_Illustration');";
					}
					break;
				case (EnumControlArgs.Delete):					
					DB.BeginTransaction();
					SaveTransaction = true;			
					LNPW_PARTIALWITHDRAWAL_obj =new LNPW_PARTIALWITHDRAWAL(dataHolder);
					if (DeleteAll(LNPW_PARTIALWITHDRAWAL_obj))			
						PrintMessage("Record(s) succesfully deleted.");

					//DeleteAll(LNPW_PARTIALWITHDRAWAL_obj);			
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
							/* Manual change
							 * =============
							   Due to unknow reason these code are not working and these lines
							   are also not required rite now, that's why we comment these code
							   
							NameValueCollection[] dataRows = GetAllItemsData();
							bool[] SelectedRowIndexes = GetSelectedRowIndexes();
							
							*/
							shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
							proccessCommand.setAllFields(columnNameValue);
							proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
							proccessCommand.setPrimaryKeys(LNPW_PARTIALWITHDRAWAL.PrimaryKeys);
							proccessCommand.setTableName("LNPW_PARTIALWITHDRAWAL");
							/* Manual change
							 * =============
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							*/
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
						/* Manual change */
						//PrintMessage ("Process " + processName + " executed successfully!");
					}
					break;
			}
		}
		sealed protected override void DataBind(DataHolder dataHolder) 
		{
			txtModifiedRows.Text = "" ;

			if(ReFetchOnBind)
			{
				dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL"].Rows.Clear();
				dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL_ADHOC"].Rows.Clear();
				dataHolder.Data.Tables["LNPW_PARTIALWITHDRAWAL_SWITCH"].Rows.Clear();
				
				dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_Data();
				dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_ADHOC_Data();
				dataHolder = new LNPW_PARTIALWITHDRAWALDB(dataHolder).GetILUS_ET_TB_VERTICALWITHDRAWAL_SWITCH_Data();
				
				
				dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL");
				dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_ADHOC");
				dataHolder = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL_SWITCH");
			
			}

			switch ((EnumControlArgs)ControlArgs[0]) 
			{
				case (EnumControlArgs.Save):
					EntryGrid.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					EntryGrid_Adhoc.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL_ADHOC"];
					EntryGrid_Adhoc.DataBind();
					EntryGrid_Switch.DataSource = dataHolder["LNPW_PARTIALWITHDRAWAL_SWITCH"];
					EntryGrid_Switch.DataBind();
					break;
				case (EnumControlArgs.Delete):
					EntryGrid.DataSource = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL")["LNPW_PARTIALWITHDRAWAL"];//dataHolder["LNPW_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					break;
				case (EnumControlArgs.Process):
					EntryGrid.DataSource = ExpandDataHolder(dataHolder, "LNPW_PARTIALWITHDRAWAL")["LNPW_PARTIALWITHDRAWAL"];//dataHolder["LNPW_PARTIALWITHDRAWAL"];
					EntryGrid.DataBind();
					break;
					
			}
			
			_totalRecords.Text = dataHolder["LNPW_PARTIALWITHDRAWAL"].Rows.Count.ToString();

			
			
			
			
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
				CheckBox chkSelectAll = (CheckBox)e.Item.FindControl("chkSelectAll");
				if (chkSelectAll!=null)
					chkSelectAll.Attributes.Add("onclick","selectAllCheckBoxes(this.checked);");
				
		
				TextBox txtNoOfYears = (TextBox)e.Item.FindControl("txtNoOfYears");
				txtNoOfYears.Text = ""+countPW;
				txtNoOfYears.Attributes.Add("onblur" ,"performViewEntryGrid('Y');");


				DropDownList ddlPWREQUIRED = (DropDownList)e.Item.FindControl("ddlPWREQUIRED");
				ddlPWREQUIRED.Attributes.Add("onchange" ,"setPWNoOfTransactions(this.value);performViewEntryGrid(this.value)");



				
				
				/*string query = "SELECT count(NP1_PROPOSAL) FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and (NPW_PW is not null or NPW_ALLOWAMOUNT is not null or NPW_CUMWITHDRAWAL is not null) ORDER BY NPW_YEAR "; 
				query = EnvHelper.Parse(query.ToString());
				rowset rsCount = DB.executeQuery(query);
				if(rsCount.next())
				{
					txtNoOfYears.Text = rsCount.getString(1);
				}*/


			}				
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				//((TableRow)e.Item).Attributes.Add("onclick","eventClick1(this);");						

				CheckBox chkDelete = (CheckBox)e.Item.FindControl("chkDelete");
				chkDelete.Attributes.Add("onclick" ,"fccheckBoxClicked(this);");
				
				TextBox txtNPW_YEAR = (TextBox)e.Item.FindControl("txtNPW_YEAR");
				txtNPW_YEAR.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_YEAR.Attributes.Add("onblur", "setAllowed('" + e.Item.ItemIndex.ToString()+"');");

				txtNPW_YEAR.Enabled=true;
				if(Double.Parse(txtNPW_YEAR.Text)<1)
					txtNPW_YEAR.Text = "";


				
				TextBox txtNPW_PW = (TextBox)e.Item.FindControl("txtNPW_PW");
				txtNPW_PW.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_PW.Attributes.Add("onblur", "setCummulative('" + e.Item.ItemIndex.ToString()+"');applyNumberFormat(this,2);");
				DropDownList ddlNPW_PURPOSE = (DropDownList)e.Item.FindControl("ddlNPW_PURPOSE");
				ddlNPW_PURPOSE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				//DropDownList ddlNPW_REQUIREDFOR = (DropDownList)e.Item.FindControl("ddlNPW_REQUIREDFOR");
				//DropDownList ddlNPW_REQIREDFORCD = (DropDownList)e.Item.FindControl("ddlNPW_REQIREDFORCD");
				//ddlNPW_REQIREDFORCD.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				//TextBox txtNPW_ATTAINAGE = (TextBox)e.Item.FindControl("txtNPW_ATTAINAGE");
				//txtNPW_ATTAINAGE.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				TextBox txtNPW_ALLOWAMOUNT = (TextBox)e.Item.FindControl("txtNPW_ALLOWAMOUNT");
				txtNPW_ALLOWAMOUNT.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_ALLOWAMOUNT.Attributes.Add("onblur" ,"applyNumberFormat(this,2);");

				
				TextBox txtNPW_CUMWITHDRAWAL = (TextBox)e.Item.FindControl("txtNPW_CUMWITHDRAWAL");
				txtNPW_CUMWITHDRAWAL.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_CUMWITHDRAWAL.Attributes.Add("onblur" ,"applyNumberFormat(this,2);");
				
				
				//TextBox txtNPW_ADHOCEXCESPRM = (TextBox)e.Item.FindControl("txtNPW_ADHOCEXCESPRM");
				//txtNPW_ADHOCEXCESPRM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				//TextBox txtNPW_ADHOCEPPW = (TextBox)e.Item.FindControl("txtNPW_ADHOCEPPW");
				//txtNPW_ADHOCEPPW.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				
				//DropDownList ddlNPW_DEATHBENEFITOPTION = (DropDownList)e.Item.FindControl("ddlNPW_DEATHBENEFITOPTION");
				//ddlNPW_DEATHBENEFITOPTION.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				
				//ddlNPW_REQUIREDFOR.Attributes.Add("onchange","SetStatus('" + e.Item.ItemIndex.ToString()+"');setName(this);");
				
				double CUMMPW = 0;
				

				string query1 = "select nvl(sum(NPW_PW),0) from LNPW_PARTIALWITHDRAWAL where NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") and NPW_YEAR<"+ (txtNPW_YEAR.Text.Trim()==""?"100":txtNPW_YEAR.Text);
				query1 =	EnvHelper.Parse(query1);
				rowset rsCummPW = DB.executeQuery(query1);
				if(rsCummPW.next())
				{
					CUMMPW = rsCummPW.getDouble(1);
				}

				TextBox txtLASTCUMMPW = (TextBox)e.Item.FindControl("txtLASTCUMMPW");
				txtLASTCUMMPW.Text = ""+CUMMPW;



				TextBox txtALLOWEDEXCLUDECUMMPW = (TextBox)e.Item.FindControl("txtALLOWEDEXCLUDECUMMPW");

				
				
				//19/09/2008 string query = "select nvl(NPR_SUMASSURED,0)-nvl(NPR_PREMIUM,0) from lnpr_product where NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") and PPR_PRODCD=SV(\"PPR_PRODCD\") and NPR_BASICFLAG='Y'";
				string query = "select nvl(NPR_SUMASSURED,0) from lnpr_product where NP1_PROPOSAL = SV(\"NP1_PROPOSAL\") and PPR_PRODCD=SV(\"PPR_PRODCD\") and NPR_BASICFLAG='Y'";
				query =	EnvHelper.Parse(query);
				rowset rsAllowed = DB.executeQuery(query);
				if(rsAllowed.next())
				{
					txtALLOWEDEXCLUDECUMMPW.Text = rsAllowed.getString(1);

					//for first year
					if (txtNPW_YEAR.Text.Trim()!="" )
					{
						txtNPW_ALLOWAMOUNT.Text = ""+(Double.Parse(rsAllowed.getString(1))-CUMMPW);
					}
				}

				// 19/09/2008 code by nawab
				double productcode = 0;
				double ageprem = 0;
				
				string qry1 = "SELECT A.PPR_PRODCD, B.NP2_AGEPREM FROM LNPR_PRODUCT A, LNP2_POLICYMASTR B WHERE A.NP1_PROPOSAL=B.NP1_PROPOSAL AND A.NP2_SETNO=B.NP2_SETNO AND B.NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND B.NP2_SETNO=1 AND NVL(A.NPR_BASICFLAG,'N')='Y'";
				qry1 =	EnvHelper.Parse(qry1);
				rowset rsAllowed2 = DB.executeQuery(qry1);
				if(rsAllowed2.next())
				{
					productcode = rsAllowed2.getDouble(1);
					ageprem = rsAllowed2.getDouble(2);			
				}

				string qry = "select cvc_value from lcvc_valuecomb where ppr_prodcd='999' and cmp_processid='080' and csp_processid='100' and cfc_conditionid='020' and cvc_valuecomb='" + Convert.ToString(productcode) + "," + Convert.ToString(ageprem)+  "'" ;
				qry =	EnvHelper.Parse(qry);
				rowset rsAllowed1 = DB.executeQuery(qry);
				if(rsAllowed1.next())
				{
					txtALLOWEDEXCLUDECUMMPW.Text = Convert.ToString(Convert.ToDouble(txtALLOWEDEXCLUDECUMMPW.Text) - rsAllowed1.getDouble(1));
				}
				//end code by nawab


				if (txtNPW_YEAR.Text.Trim()!="" )
					txtNPW_CUMWITHDRAWAL.Text = ""+(CUMMPW + double.Parse(txtNPW_PW.Text.Trim()==""?"0":txtNPW_PW.Text));




				ListItem selectedItem =null;
				IDataReader drNPW_PURPOSE = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_PURPOSE_RO();
				ddlNPW_PURPOSE.DataSource = drNPW_PURPOSE;
				ddlNPW_PURPOSE.DataBind();
				drNPW_PURPOSE.Close();
				selectedItem = ddlNPW_PURPOSE.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NPW_PURPOSE"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;
				
				

			}				
			else if (e.Item.ItemType==ListItemType.Footer)
			{	
				
		
				
			}
		}


		private void EntryGrid_Adhoc_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{

			if (e.Item.ItemType==ListItemType.Header )
			{
				TextBox txtNoOfYears_Adhoc = (TextBox)e.Item.FindControl("txtNoOfYears_Adhoc");
				txtNoOfYears_Adhoc.Text = "0";
				txtNoOfYears_Adhoc.Text = ""+countAD;
				txtNoOfYears_Adhoc.Attributes.Add("onblur" ,"performViewEntryGrid_Adhoc('Y');");


				DropDownList ddlPWREQUIRED_Adhoc = (DropDownList)e.Item.FindControl("ddlPWREQUIRED_Adhoc");
				ddlPWREQUIRED_Adhoc.Attributes.Add("onchange" ,"setADNoOfTransactions(this.value);performViewEntryGrid_Adhoc(this.value);");

			}

			
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				
				TextBox txtNPW_YEAR_ADHOC = (TextBox)e.Item.FindControl("txtNPW_YEAR_ADHOC");
				//txtNPW_YEAR_ADHOC.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_YEAR_ADHOC.Enabled=true;
				if(Double.Parse(txtNPW_YEAR_ADHOC.Text)<1)
					txtNPW_YEAR_ADHOC.Text = "";


				TextBox txtNPW_ADHOCEXCESPRM = (TextBox)e.Item.FindControl("txtNPW_ADHOCEXCESPRM");
				//txtNPW_ADHOCEXCESPRM.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				TextBox txtNPW_ADHOCEPPW = (TextBox)e.Item.FindControl("txtNPW_ADHOCEPPW");
				//txtNPW_ADHOCEPPW.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				txtNPW_ADHOCEXCESPRM.Attributes.Add("onblur", "setAdhocEPLessPW('" + e.Item.ItemIndex.ToString()+"');applyNumberFormat(this,2);");
				txtNPW_YEAR_ADHOC.Attributes.Add("onblur", "setAdhocEPLessPW('" + e.Item.ItemIndex.ToString()+"');");


			}				

		}

		private void EntryGrid_Switch_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{


			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				
				TextBox txtNPW_YEAR_SWITCH = (TextBox)e.Item.FindControl("txtNPW_YEAR_SWITCH");
				//txtNPW_YEAR_ADHOC.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");
				txtNPW_YEAR_SWITCH.Enabled=true;
				if(Double.Parse(txtNPW_YEAR_SWITCH.Text)<1)
					txtNPW_YEAR_SWITCH.Text = "";

				DropDownList ddlNPW_DEATHBENEFITOPTION = (DropDownList)e.Item.FindControl("ddlNPW_DEATHBENEFITOPTION");
				//ddlNPW_DEATHBENEFITOPTION.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"');");

				IDataReader drNPW_DEATHBENEFITOPTION = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_TB_VERTICALWITHDRAWAL_NPW_DEATHBENEFITOPTION_RO();
				ddlNPW_DEATHBENEFITOPTION.DataSource = drNPW_DEATHBENEFITOPTION;
				ddlNPW_DEATHBENEFITOPTION.DataBind();
				drNPW_DEATHBENEFITOPTION.Close();
				
				ListItem selectedItem =null;
				selectedItem = ddlNPW_DEATHBENEFITOPTION.Items.FindByValue(((DataRowView)(e.Item.DataItem)).Row["NPW_DEATHBENEFITOPTION"].ToString());
				if (selectedItem!=null)
					selectedItem.Selected=true;


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
		private bool DeleteAll(LNPW_PARTIALWITHDRAWAL LNPW_PARTIALWITHDRAWAL_obj)
		{
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();	
			bool deleted = false;
			NameValueCollection columnNameValue=new NameValueCollection();

			entityClass.setNameValueCollection(columnNameValue);

			foreach (DataGridItem item in EntryGrid.Items)
			{						
				
				if(((CheckBox)item.Cells[item.Cells.Count-1].FindControl("chkDelete")).Checked)
				{
					columnNameValue=new NameValueCollection();
					columnNameValue.Add("NPW_YEAR",((TextBox)item.FindControl("txtNPW_YEAR")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_YEAR")).Text));
					columnNameValue.Add("NPW_PW",((TextBox)item.FindControl("txtNPW_PW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_PW")).Text));
					columnNameValue.Add("NPW_PURPOSE",((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue);
					columnNameValue.Add("NPW_REQUIREDFOR",((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue);
					columnNameValue.Add("NPW_REQIREDFORCD",((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue.Trim()==""?null:(object)double.Parse(((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue));
					columnNameValue.Add("NPW_ALLOWAMOUNT",((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text));
					columnNameValue.Add("NPW_CUMWITHDRAWAL",((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text));
					columnNameValue.Add("NPW_ADHOCEXCESPRM",((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text));
					columnNameValue.Add("NPW_DEATHBENEFITOPTION",((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue);
					columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text);

					entityClass.fsoperationBeforeDelete();

					LNPW_PARTIALWITHDRAWAL_obj.Delete(columnNameValue);
					deleted=true;
					dataHolder.Update(DB.Transaction);				
					entityClass.fsoperationAfterDelete();

					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPW_PARTIALWITHDRAWAL");
				}//end of if	
			}
			return deleted;
		}
		private void UpdateAll(LNPW_PARTIALWITHDRAWAL LNPW_PARTIALWITHDRAWAL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue=new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			shgn.SHGNCommand entityClass=new ace.ILUS_ET_TB_VERTICALWITHDRAWAL();
			entityClass.setNameValueCollection(columnNameValue);
			
			TextBox txtNoOfYears = (TextBox)EntryGrid.Controls[0].FindControl("txtNoOfYears");
			int noOfRecords = Int32.Parse(txtNoOfYears.Text.Trim()==""?"0":txtNoOfYears.Text);
			noOfRecords--;



			for (int i=0; i<EntryGrid.Items.Count; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataListItem item = EntryGrid.Items[i];
				
				//if year entered
				//if(((TextBox)item.FindControl("txtNPW_YEAR")).Text.Trim()!="" || i <= noOfRecords)
				//{


				
				columnNameValue.Add("NPW_YEAR",((TextBox)item.FindControl("txtNPW_YEAR")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_YEAR")).Text));
				columnNameValue.Add("NPW_PW",((TextBox)item.FindControl("txtNPW_PW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_PW")).Text));
				columnNameValue.Add("NPW_PURPOSE",((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue);
				//columnNameValue.Add("NPW_REQUIREDFOR",((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue);
				//columnNameValue.Add("NPW_REQIREDFORCD",((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue.Trim()==""?null:(object)double.Parse(((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue));
				columnNameValue.Add("NPW_ALLOWAMOUNT",((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text));
				columnNameValue.Add("NPW_CUMWITHDRAWAL",((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text));
				//columnNameValue.Add("NPW_ADHOCEXCESPRM",((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text));
				//columnNameValue.Add("NPW_DEATHBENEFITOPTION",((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue);
				columnNameValue.Add("NP1_PROPOSAL",(Session["NP1_PROPOSAL"]==null?null:Session["NP1_PROPOSAL"]));

				entityClass.fsoperationBeforeUpdate();


				if(((TextBox)item.FindControl("txtNPW_YEAR")).Text.Trim()!="" && Int32.Parse(((TextBox)item.FindControl("txtNPW_YEAR")).Text)>0)
				{
						
					//LNPW_PARTIALWITHDRAWAL_obj.Add(columnNameValue,"ILUS_ET_TB_VERTICALWITHDRAWAL");


					string insert = "insert into LNPW_PARTIALWITHDRAWAL (NP1_PROPOSAL, NPW_YEAR, NPW_PW, NPW_PURPOSE, NPW_ALLOWAMOUNT, NPW_CUMWITHDRAWAL) values(?,?,?,?,?,?)";

					SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
					pc.puts("@NP1_PROPOSAL", columnNameValue.getObject("NP1_PROPOSAL"));
					pc.puts("@NPW_YEAR", columnNameValue.getObject("NPW_YEAR"));
					pc.puts("@NPW_PW", columnNameValue.getObject("NPW_PW"), System.Data.DbType.Decimal);
					pc.puts("@NPW_PURPOSE", columnNameValue.getObject("NPW_PURPOSE"));
					pc.puts("@NPW_ALLOWAMOUNT", columnNameValue.getObject("NPW_ALLOWAMOUNT"), System.Data.DbType.Decimal);
					pc.puts("@NPW_CUMWITHDRAWAL", columnNameValue.getObject("NPW_CUMWITHDRAWAL"), System.Data.DbType.Decimal);


					IDbCommand cmd = DB.CreateCommand(insert,DB.Connection);
					cmd.Transaction = DB.Transaction; 

					cmd = DB.setCommandParameters(cmd, pc);
					int count = cmd.ExecuteNonQuery();
					count = count;

					if(count==0)
						throw new Exception("Cannot insert or update.");

				}
			
				dataHolder.Update(DB.Transaction);
				entityClass.fsoperationAfterUpdate();
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPW_PARTIALWITHDRAWAL");
					
				//}//end if year entered


			}
		}

		private void UpdateAll_Adhoc(LNPW_PARTIALWITHDRAWAL LNPW_PARTIALWITHDRAWAL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue = new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			shgn.SHGNCommand entityClass=new ace.ILUS_ET_TB_VERTICALWITHDRAWAL();
			entityClass.setNameValueCollection(columnNameValue);

			TextBox textNos = (TextBox)EntryGrid_Adhoc.Controls[0].FindControl("txtNoOfYears_Adhoc");

			int noOfRecords = Int32.Parse(textNos.Text.Trim()==""?"0":textNos.Text); 
			noOfRecords--;
			
			
			for (int i=0; i<EntryGrid_Adhoc.Items.Count; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataListItem item = EntryGrid_Adhoc.Items[i];
				
				//if year entered
				//if(((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text.Trim()!="" || i <= noOfRecords)
				//{

				columnNameValue.Add("NPW_YEAR",((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text));
				//columnNameValue.Add("NPW_PW",((TextBox)item.FindControl("txtNPW_PW_ADHOC")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_PW_ADHOC")).Text));
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL_ADHOC")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL_ADHOC")).Text);
				columnNameValue.Add("NPW_ADHOCEXCESPRM",((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text));
				columnNameValue.Add("NPW_ADHOCEPPW",((TextBox)item.FindControl("txtNPW_ADHOCEPPW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEPPW")).Text));

			
				//entityClass.fsoperationBeforeUpdate();
				



				if(((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text.Trim()!="" && Int32.Parse(((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text)>0)
				{
						

					int year = Int32.Parse(((TextBox)item.FindControl("txtNPW_YEAR_ADHOC")).Text );
					string query = "SELECT count(NP1_PROPOSAL) FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and NPW_YEAR = "+ year +" and (NPW_PW is not null or NPW_ALLOWAMOUNT is not null or NPW_CUMWITHDRAWAL is not null or NPW_DEATHBENEFITOPTION is not null ) ORDER BY NPW_YEAR "; 
					query = EnvHelper.Parse(query.ToString());
					rowset rsCount = DB.executeQuery(query);
					if(rsCount.next())
					{

						string dml = "";
						SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
						if(rsCount.getInt(1).Equals(0))
						{
							dml = "insert into LNPW_PARTIALWITHDRAWAL (NP1_PROPOSAL, NPW_YEAR, NPW_ADHOCEXCESPRM, NPW_ADHOCEPPW) values(?,?,?,?)";

							pc.puts("NP1_PROPOSAL", columnNameValue.getObject("NP1_PROPOSAL"));
							pc.puts("NPW_YEAR", columnNameValue.getObject("NPW_YEAR"));
							pc.puts("NPW_ADHOCEXCESPRM", columnNameValue.getObject("NPW_ADHOCEXCESPRM"), System.Data.DbType.Decimal);
							pc.puts("NPW_ADHOCEPPW", columnNameValue.getObject("NPW_ADHOCEPPW"), System.Data.DbType.Decimal);
						}
						else
						{
							dml = "update LNPW_PARTIALWITHDRAWAL set NPW_ADHOCEXCESPRM =? , NPW_ADHOCEPPW =? where NP1_PROPOSAL=? and NPW_YEAR=?";

							pc.puts("NPW_ADHOCEXCESPRM", columnNameValue.getObject("NPW_ADHOCEXCESPRM"), System.Data.DbType.Decimal);
							pc.puts("NPW_ADHOCEPPW", columnNameValue.getObject("NPW_ADHOCEPPW"), System.Data.DbType.Decimal);
							pc.puts("NP1_PROPOSAL", columnNameValue.getObject("NP1_PROPOSAL"));
							pc.puts("NPW_YEAR", columnNameValue.getObject("NPW_YEAR"));
						}
					
						IDbCommand cmd = DB.CreateCommand(dml,DB.Connection);
						cmd.Transaction = DB.Transaction; 

						cmd = DB.setCommandParameters(cmd, pc);
						int count = cmd.ExecuteNonQuery();
						count = count;

						if(count==0)
							throw new Exception("Cannot insert or update.");

					
					}



				}

				dataHolder.Update(DB.Transaction);
				entityClass.fsoperationAfterUpdate();
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPW_PARTIALWITHDRAWAL");
				
				//}//end if year entered
			}
		}


		private void UpdateAll_Switch(LNPW_PARTIALWITHDRAWAL LNPW_PARTIALWITHDRAWAL_obj)
		{			

			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			NameValueCollection columnNameValue = new NameValueCollection();
			string[] mRows = txtModifiedRows.Text.Split(',');

			shgn.SHGNCommand entityClass=new ace.ILUS_ET_TB_VERTICALWITHDRAWAL();
			entityClass.setNameValueCollection(columnNameValue);

			for (int i=0; i<EntryGrid_Switch.Items.Count; i++)
			{
				_RecordsUpdated = true ;
				columnNameValue=new NameValueCollection();
				DataListItem item = EntryGrid_Switch.Items[i];
				

				columnNameValue.Add("NPW_YEAR",((TextBox)item.FindControl("txtNPW_YEAR_SWITCH")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_YEAR_SWITCH")).Text));
				columnNameValue.Add("NP1_PROPOSAL",Session["NP1_PROPOSAL"]);
				columnNameValue.Add("NPW_DEATHBENEFITOPTION",((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue);					
				//columnNameValue.Add("NPW_SWITCHDATE",((TextBox)item.FindControl("txtNPW_SWITCHDATE")).Text.Trim()==""?null:(object)DateTime.Parse(((TextBox)item.FindControl("txtNPW_SWITCHDATE")).Text));



				if(((TextBox)item.FindControl("txtNPW_YEAR_SWITCH")).Text.Trim()!="" && Int32.Parse(((TextBox)item.FindControl("txtNPW_YEAR_SWITCH")).Text)>0)
				{
						

					int year = Int32.Parse(((TextBox)item.FindControl("txtNPW_YEAR_SWITCH")).Text);
					string query = "SELECT count(NP1_PROPOSAL) FROM LNPW_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and NPW_YEAR = "+ year +" and (NPW_PW is not null or NPW_ALLOWAMOUNT is not null or NPW_CUMWITHDRAWAL is not null or NPW_PW is not null or NPW_ALLOWAMOUNT is not null or NPW_CUMWITHDRAWAL is not null) ORDER BY NPW_YEAR "; 
					query = EnvHelper.Parse(query.ToString());
					rowset rsCount = DB.executeQuery(query);
					if(rsCount.next())
					{
							
						string dml ="";
						SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
						if(rsCount.getInt(1).Equals(0))
						{
							dml = "insert into LNPW_PARTIALWITHDRAWAL (NP1_PROPOSAL, NPW_YEAR, NPW_DEATHBENEFITOPTION) values(?,?,?)";

							pc.puts("@NP1_PROPOSAL", columnNameValue.getObject("NP1_PROPOSAL"));
							pc.puts("@NPW_YEAR", columnNameValue.getObject("NPW_YEAR"));
							pc.puts("@NPW_DEATHBENEFITOPTION", columnNameValue.getObject("NPW_DEATHBENEFITOPTION"));


						}
						else
						{
							dml = "update LNPW_PARTIALWITHDRAWAL set NPW_DEATHBENEFITOPTION =? where NP1_PROPOSAL=? and NPW_YEAR=?";

							pc.puts("@NPW_DEATHBENEFITOPTION", columnNameValue.getObject("NPW_DEATHBENEFITOPTION"));
							pc.puts("@NP1_PROPOSAL", columnNameValue.getObject("NP1_PROPOSAL"));
							pc.puts("@NPW_YEAR", columnNameValue.getObject("NPW_YEAR"));
						}

						IDbCommand cmd = DB.CreateCommand(dml,DB.Connection);
						cmd.Transaction = DB.Transaction; 

						cmd = DB.setCommandParameters(cmd, pc);
						int count = cmd.ExecuteNonQuery();
						count = count;

						if(count==0)
							throw new Exception("Cannot insert or update.");

					}

				}

					
				dataHolder.Update(DB.Transaction);
				entityClass.fsoperationAfterUpdate();
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPW_PARTIALWITHDRAWAL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPW_PARTIALWITHDRAWAL");
			}//end if year entered
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
				columnNameValue.Add("NPW_YEAR",((TextBox)item.FindControl("txtNPW_YEAR")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_YEAR")).Text));
				columnNameValue.Add("NPW_PW",((TextBox)item.FindControl("txtNPW_PW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_PW")).Text));
				columnNameValue.Add("NPW_PURPOSE",((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_PURPOSE")).SelectedValue);
				columnNameValue.Add("NPW_REQUIREDFOR",((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_REQUIREDFOR")).SelectedValue);
				columnNameValue.Add("NPW_REQIREDFORCD",((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue.Trim()==""?null:(object)double.Parse(((DropDownList)item.FindControl("ddlNPW_REQIREDFORCD")).SelectedValue));
				columnNameValue.Add("NPW_ATTAINAGE",((TextBox)item.FindControl("txtNPW_ATTAINAGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ATTAINAGE")).Text));
				columnNameValue.Add("NPW_ALLOWAMOUNT",((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ALLOWAMOUNT")).Text));
				columnNameValue.Add("NPW_CUMWITHDRAWAL",((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_CUMWITHDRAWAL")).Text));
				columnNameValue.Add("NPW_ADHOCEXCESPRM",((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEXCESPRM")).Text));
				columnNameValue.Add("NPW_ADHOCEPPW",((TextBox)item.FindControl("txtNPW_ADHOCEPPW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)item.FindControl("txtNPW_ADHOCEPPW")).Text));
				columnNameValue.Add("NPW_DEATHBENEFITOPTION",((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue.Trim()==""?null:((DropDownList)item.FindControl("ddlNPW_DEATHBENEFITOPTION")).SelectedValue);
				columnNameValue.Add("NP1_PROPOSAL",((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)item.FindControl("lblNP1_PROPOSAL")).Text);

				dataRows[item.ItemIndex] = columnNameValue;							
			}
			return dataRows;
		}
		NameValueCollection GetAnItemData(DataGridItem footerItem)
		{		
			NameValueCollection columnNameValue=new NameValueCollection();	
			/*columnNameValue.Add("NPW_YEAR",((TextBox)footeritem.FindControl("txtNewNPW_YEAR")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_YEAR")).Text));
			columnNameValue.Add("NPW_PW",((TextBox)footeritem.FindControl("txtNewNPW_PW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_PW")).Text));
			columnNameValue.Add("NPW_PURPOSE",((DropDownList)footeritem.FindControl("ddlNewNPW_PURPOSE")).SelectedValue.Trim()==""?null:((DropDownList)footeritem.FindControl("ddlNewNPW_PURPOSE")).SelectedValue);
			columnNameValue.Add("NPW_REQUIREDFOR",((DropDownList)footeritem.FindControl("ddlNewNPW_REQUIREDFOR")).SelectedValue.Trim()==""?null:((DropDownList)footeritem.FindControl("ddlNewNPW_REQUIREDFOR")).SelectedValue);
			columnNameValue.Add("NPW_REQIREDFORCD",((DropDownList)footeritem.FindControl("ddlNewNPW_REQIREDFORCD")).SelectedValue.Trim()==""?null:(object)double.Parse(((DropDownList)footeritem.FindControl("ddlNewNPW_REQIREDFORCD")).SelectedValue));
			columnNameValue.Add("NPW_ATTAINAGE",((TextBox)footeritem.FindControl("txtNewNPW_ATTAINAGE")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_ATTAINAGE")).Text));
			columnNameValue.Add("NPW_ALLOWAMOUNT",((TextBox)footeritem.FindControl("txtNewNPW_ALLOWAMOUNT")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_ALLOWAMOUNT")).Text));
			columnNameValue.Add("NPW_CUMWITHDRAWAL",((TextBox)footeritem.FindControl("txtNewNPW_CUMWITHDRAWAL")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_CUMWITHDRAWAL")).Text));
			columnNameValue.Add("NPW_ADHOCEXCESPRM",((TextBox)footeritem.FindControl("txtNewNPW_ADHOCEXCESPRM")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_ADHOCEXCESPRM")).Text));
			columnNameValue.Add("NPW_ADHOCEPPW",((TextBox)footeritem.FindControl("txtNewNPW_ADHOCEPPW")).Text.Trim()==""?null:(object)double.Parse(((TextBox)footeritem.FindControl("txtNewNPW_ADHOCEPPW")).Text));
			columnNameValue.Add("NPW_DEATHBENEFITOPTION",((DropDownList)footeritem.FindControl("ddlNewNPW_DEATHBENEFITOPTION")).SelectedValue.Trim()==""?null:((DropDownList)footeritem.FindControl("ddlNewNPW_DEATHBENEFITOPTION")).SelectedValue);
			columnNameValue.Add("NP1_PROPOSAL",((TextBox)footeritem.FindControl("lblNewNP1_PROPOSAL")).Text.Trim()==""?null:((TextBox)footeritem.FindControl("lblNewNP1_PROPOSAL")).Text);*/

			return columnNameValue;		
		}

	}
}

