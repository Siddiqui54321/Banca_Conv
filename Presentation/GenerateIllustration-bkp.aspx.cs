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
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
using shsm;

using System.Data.OleDb;
using System.Data.OracleClient;


namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class GenerateIllustrationbkp : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls


		protected System.Web.UI.WebControls.Literal _lastEvent;
	

		protected System.Web.UI.WebControls.Literal ltrlApp;
		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal ErrorOccured;
		

		/******************* Entity Fields Decleration *****************/
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_AGEPREM2ND;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPCU_CURRCODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_CURRCODE;







		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_PREMIUMTER;

		

		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";

		#region //******************* Entity Fields Decleration *****************//
		
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlBranch;

		

		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPH_FULLNAMEARABIC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPH_FULLNAMEARABIC;
		//protected Button  btnSearchOccupation;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlCCL_CATEGORYCD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCCL_CATEGORYCD;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_INSUREDTYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPH_INSUREDTYPE;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNU1_SMOKER;
		
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNU1_ACCOUNTNO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_ACCOUNTNO;

		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPH_ANNUINCOME;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlCNT_NATCD;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_ANNUINCOME;

		bool NewRecord = false;
		bool DMLSucceeded = true;				

		#endregion
		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NPH_CODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_FULLNAME;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_FULLNAMEARABIC;
		protected System.Web.UI.WebControls.CompareValidator cfvCCL_CATEGORYCD;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_INSUREDTYPE;
		protected System.Web.UI.WebControls.CompareValidator mfvNPH_BIRTHDATE;
		//protected System.Web.UI.WebControls.CompareValidator cfvNPH_BIRTHDATE;

		protected System.Web.UI.WebControls.CompareValidator cfvNU1_ACCOUNTNO;
		protected System.Web.UI.WebControls.RequiredFieldValidator msgNU1_ACCOUNTNO;
		private string NPH_LIFE;
		private string NU1_SMOKER;
		protected System.Web.UI.WebControls.CompareValidator cfvNU1_SMOKER;
		//protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_SELECTION;
		protected System.Web.UI.WebControls.Label lblServerError;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPH_IDNO2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_cnic;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_HEIGHTTYPE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNU1_ACTUALHEIGHT;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNU1_CONVERTHEIGHT;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_WEIGHTTTYPE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNU1_ACTUALWEIGHT;
		

		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNU1_CONVERTWEIGHT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_SMOKER;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txt_bmi;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator3;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNU1_ACCOUNTNO;
		//protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator4;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator5;
		//	protected System.Web.UI.WebControls.CompareValidator Comparevalidator4;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowHeightWeight;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowBMIAccount;
		private string NU1_ACCOUNTNO;

						
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
		
		private void InitializeComponent() 
		{

		}
		#endregion		

		#region Major methods of First Step
		protected override void PrepareInputUI(DataHolder dataHolder)
		{
		//	SetApplicationUI();
			//Set Default Branch of the User

			//ddlBranch.Enabled=false;



		}

		private void SetApplicationUI()
		{
			ltrlApp.Text = "var application='" + ace.Ace_General.getApplicationName() + "';";
			if(ace.Ace_General.IsIllustration())
			{
				HtmlTableRow rowAccountBMI = new HtmlTableRow();
				rowAccountBMI = (HtmlTableRow) Page.FindControl("rowBMIAccount");
				rowAccountBMI.Style["display"] = "none";
				rowAccountBMI = (HtmlTableRow) Page.FindControl("rowHeightWeight");
				rowAccountBMI.Style["display"] = "none";
				TextBox txtWeight = new TextBox();
				txtWeight	=(TextBox) Page.FindControl("txtNU1_ACTUALWEIGHT");
				txtWeight.Text  ="55";
				txtWeight	=(TextBox) Page.FindControl("txtNU1_ACTUALHEIGHT");
				txtWeight.Text ="5.5";
			}
		}

		protected override void ValidateParams() 
		{
			base.ValidateParams();			
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
			return dataHolder;
		}
		//Bind data 
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			//TODO: SESSION SETTING in behavior ViewInitialState()
			//***********************CUSTOM CODE ***********************/
			ViewInitialState();
			//***********************CUSTOM CODE ***********************/

			//Fill DropDown List of Branches
//			CCS_CHANLSUBDETLDB branchNamesDB = new CCS_CHANLSUBDETLDB(this.dataHolder);
//			IDataReader BranchReader = branchNamesDB.GetBranchNames();
//			ddlBranch.DataSource = BranchReader;
//			ddlBranch.DataBind();
//			BranchReader.Close();
//
//			string DefaultBranchCode = CCS_CHANLSUBDETLDB.GetDefaultBranchName();
//			if (DefaultBranchCode != string.Empty)//Solved object reference not set to an instance of an object
//			{
//				//ddlBranch.Items.FindByValue(DefaultBranchCode).Selected = true;
//
//				if (ddlBranch.Items.Contains(ddlBranch.Items.FindByValue(DefaultBranchCode)) == true)
//				{
//					ddlBranch.SelectedValue = DefaultBranchCode;
//				}
//				else
//				{
//					ddlBranch.SelectedIndex = -1;
//				}
//
//			}
//			else
//			{
//				ddlBranch.SelectedIndex = -1;
//			}


			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_TITLE_RO();;
			ddlNPH_TITLE.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPH_TITLE.DataBind();
			LCSD_SYSTEMDTLReader0.Close();

			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_IDTYPE_RO();;
			ddlNPH_IDTYPE.DataSource = LCSD_SYSTEMDTLReader1;
			ddlNPH_IDTYPE.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			
			//************ Parameterization *********************
			if(isOccupationEnabled())
			{
				IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO();;
				ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
				ddlCOP_OCCUPATICD.DataBind();
				LCOP_OCCUPATIONReader1.Close();

				IDataReader LCCL_CATEGORYReader2 = LCCL_CATEGORYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCL_CATEGORYCD_RO();
				ddlCCL_CATEGORYCD.DataSource = LCCL_CATEGORYReader2 ;
				ddlCCL_CATEGORYCD.DataBind();
				LCCL_CATEGORYReader2.Close();
			}

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();

			string RoundYearsDifference = "var ageRoundCriteria ='" + ace.clsIlasUtility.getAgeRoundingCriteria() + "';";

			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");" + RoundYearsDifference);
			FooterScript.Text = EnvHelper.Parse("") ;
			ddlCOP_OCCUPATICD.Attributes.Add("onchange","filterClass(this);");
			
			txtNPH_FULLNAME.Attributes.Add("onfocus","Name_GotFocus(this);");
			txtNPH_FULLNAME.Attributes.Add("onblur", "Name_LostFocus(this);");

			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));

			BindInputPlanData();
		}

		protected void BindInputPlanData()
		{
			IDataReader LPPR_PRODUCTReader0 = LPPR_PRODUCTDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO_FOR_NIB();;
			ddlPPR_PRODCD.DataSource = LPPR_PRODUCTReader0 ;

			if(SessionObject.GetString("s_CCD_CODE") =="5" && SessionObject.GetString("s_CCH_CODE")=="2")
			{
				ddlPPR_PRODCD.DataValueField="PPR_PRODCD";
				ddlPPR_PRODCD.DataTextField="DESC_S";
			}
			ddlPPR_PRODCD.DataBind();
			LPPR_PRODUCTReader0.Close();

			IDataReader PCU_CURRENCYReader1 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO();;
			ddlPCU_CURRCODE_PRM.DataSource = PCU_CURRENCYReader1 ;
			ddlPCU_CURRCODE_PRM.DataBind();
			PCU_CURRENCYReader1.Close();


			IDataReader LCSD_SYSTEMDTLReader55 = LCSD_SYSTEMDTLDB.GetCRF_FORFEITUCD();;			
			ddlCFR_FORFEITUCD.DataSource = LCSD_SYSTEMDTLReader55 ;
			ddlCFR_FORFEITUCD.DataBind();
			LCSD_SYSTEMDTLReader55.Close();


			IDataReader PCU_CURRENCYReader2 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO();;
			ddlPCU_CURRCODE_PRM.DataSource = PCU_CURRENCYReader2 ;
			ddlPCU_CURRCODE_PRM.DataBind();
			PCU_CURRENCYReader2.Close();
			IDataReader LCMO_MODEReader3 = LCMO_MODEDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO();;
			ddlCMO_MODE.DataSource = LCMO_MODEReader3 ;
			ddlCMO_MODE.DataBind();
			LCMO_MODEReader3.Close();


			IDataReader CCB_CODEReader7 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CCB_CODE_RO();;
			ddlCCB_CODE.DataSource = CCB_CODEReader7;
			ddlCCB_CODE.DataBind();
			CCB_CODEReader7.Close();



			_lastEvent.Text = "New";		


			rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+ SessionObject.Get("NP1_PROPOSAL")+"'");
			if (LNP2_POLICYMASTR.next())
			{
				txtNP2_AGEPREM.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
				//txtNP2_AGEPREM2ND.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM2ND").ToString();
			}
			else
			{
				txtNP2_AGEPREM.Text="0";
			//	txtNP2_AGEPREM2ND.Text="0";
			}


			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\"); getField('NP1_PROPOSAL').value=v_NP1_PROPOSAL; var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");

			if(Convert.ToString(Session["FLAG_RESET_PREMIUM"]) == "Y" && DMLSucceeded == true)
			{
				HeaderScript.Text = HeaderScript.Text + "var reloadPage='Y'; ";
				Session["FLAG_RESET_PREMIUM"] = "";
			}
			else
			{
				HeaderScript.Text = HeaderScript.Text + "var reloadPage=''; ";
				Session["FLAG_RESET_PREMIUM"] = "";
			
			}

//			ddlCCB_CODE.Attributes.Add("onchange","setFaceValueField(this.value);");
//			txtNPR_SUMASSURED.Attributes.Add("onblur","applyNumberFormat(this,2);validate(this,'SUMASSURED');");
//			txtNPR_SUMASSURED.Attributes.Add("onfocus","validateInfo(this,'SUMASSURED');");
//
//			txtNPR_SUMASSURED.Attributes.Add("onblur","applyNumberFormat(this,2); if(getField('CCB_CODE').value=='S') validate(this,'SUMASSURED');");
//			txtNPR_SUMASSURED.Attributes.Add("onfocus","if(getField('CCB_CODE').value=='S') validateInfo(this,'SUMASSURED');");
//			txtNPR_SUMASSURED.Attributes.Add("onchange","SummAssured_OnChange(this);");
//
//			txtNPR_TOTPREM.Attributes.Add("onblur","applyNumberFormat(this,2);if(getField('CCB_CODE').value=='T') validate(this,'TOTPREM');");
//			txtNPR_TOTPREM.Attributes.Add("onfocus","if(getField('CCB_CODE').value=='T') validateInfo(this,'TOTPREM');");
//			txtNPR_TOTPREM.Attributes.Add("onchange","Premium_OnChange(this);");
//			
//			txtNPR_BENEFITTERM.Attributes.Add("onblur","validateBenefitTerm(getField('PPR_PRODCD').value,getField('NPR_BENEFITTERM').value);setPremiumTerm(this);validate(this,'BTERM');setMaturityAge(this)");
//			txtNPR_BENEFITTERM.Attributes.Add("onchange","resetPremiumTerm(this);");
//			txtNPR_BENEFITTERM.Attributes.Add("onfocus","validateInfo(this,'BTERM');");
//
//			txtNPR_PREMIUMTER.Attributes.Add("onblur","validatePremiumTerm(this);");
//			txtNPR_PREMIUMTER.Attributes.Add("onchange","PremiumTerm_OnChange(this);");
//
//
//			
			
			ddlPCU_CURRCODE_PRM.Attributes.Add("onchange","Currency_OnChange(this);");

			ddlCMO_MODE.Attributes.Add("onchange","disableViewPremiumUptoAge(this);");

//			ddlPPR_PRODCD.Attributes.Add("onchange","setViewForProduct(this);setOptions(this);filterCurrency(this);parent.frames[2].setFundSettingStatus();");
			

		


		}
		protected bool isOccupationEnabled()
		{
			rowset rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI UI WHERE UI.CUI_COLUMNID='ddlCOP_OCCUPATICD'"));
			if(rs.next())
			{
				if(rs.getObject("CUI_VISIBILE") != null && rs.getString("CUI_VISIBILE").ToUpper() == "Y")
					if(rs.getObject("CUI_DISABLE") != null && rs.getString("CUI_DISABLE").ToUpper() == "N")
					{
						return true;
					}
			}
			return false;
		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPH_PHOLDER.PrimaryKeys)
			{
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!= null)
				{
					if (ctrl is WebControl)
					{
						//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!= null))
						{
							control.Enabled = true;
						}
					}
				}
			}
		}

		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{


			ErrorOccured.Text = "navigation = 'N';";

			if (_lastEvent.Text == "New") NewRecord = true;
			DMLSucceeded = false;
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PER_PERSONALDET();
			entityClass.setNameValueCollection(columnNameValue);

			//Custom : Change
			SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?"N":ddlNU1_SMOKER.SelectedValue);
			SessionObject.Set("NU1_ACCOUNTNO", txtNU1_ACCOUNTNO.Text.Trim()== "" ? null : txtNU1_ACCOUNTNO.Text);			

			SessionObject.Set("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
			SessionObject.Set("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
			
			SessionObject.Set("NP2_AGEPREM",txtNP2_AGEPREM.Text == "" ? null:txtNP2_AGEPREM.Text);

			SessionObject.Set("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text == "" ? null:txtNU1_CONVERTWEIGHT.Text);
			
			SessionObject.Set("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
			SessionObject.Set("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
			SessionObject.Set("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);
			
			SessionObject.Set("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);
			SessionObject.Set("s_NPH_IDNO", txtCNIC_VALUE.Text==""?null:txtCNIC_VALUE.Text.Replace("-",""));
			SessionObject.Set("s_NPH_IDNO2", txtNPH_IDNO2.Text==""?null:txtNPH_IDNO2.Text);
				
			//Custom : Change
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;


					txtNPH_LIFE.Text = "D";
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
					
					
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
					columnNameValue.Add("NPH_IDNO2",txtNPH_IDNO2.Text.Trim()==""?null:txtNPH_IDNO2.Text);
				
					//Izhar-Ul-Haque
					columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
					columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text==""?null:txtNU1_CONVERTWEIGHT.Text);

					columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
					columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);

					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);
				
					if(txtNPH_CODE.Text.Equals("") || txtNPH_CODE.Text.Equals("0"))
					{
						txtNPH_CODE.Text = ace.ILUS_ET_NM_PER_PERSONALDET.getClientNumber();
					}
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()== "" ? null:txtNPH_CODE.Text);


					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "NPH_CODE");
					if (security.SaveAllowed)
					{
						bool newClientForBANCA = isNewClientForBANCA(txtNPH_CODE.Text);
						if(newClientForBANCA)
						{	//Insert
							entityClass.fsoperationBeforeSave();
							new LNPH_PHOLDER(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PER_PERSONALDET");
						}
						else
						{	//Update
							entityClass.fsoperationBeforeUpdate();
							dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
							new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);
						}

						dataHolder.Update(DB.Transaction);
						
						SessionObject.Set("_pk_NPH_CODE",columnNameValue.get("NPH_CODE"));
						
						
						
						//New Code By Izhar-ul-haque
						if(ddlNPH_INSUREDTYPE.SelectedValue=="Y")
						{
							DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='"+txtNU1_ACCOUNTNO.Text+"' WHERE NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");
						}
						else
						{
							DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='"+txtNU1_ACCOUNTNO.Text+"' WHERE NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");		
						}
						

						if(newClientForBANCA == true)
						{
							entityClass.fsoperationAfterSave();
						}
						else
						{
							entityClass.fsoperationAfterUpdate();
						}

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPH_PHOLDER");
						_lastEvent.Text = "Save"; 					
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
				case (EnumControlArgs.Update):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
					
					
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_IDNO", txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
					columnNameValue.Add("NPH_IDNO2",txtNPH_IDNO2.Text.Trim()==""?null :txtNPH_IDNO2.Text);
				
					//Izhar-Ul-Haque
					columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
					columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text==""?null:txtNU1_CONVERTWEIGHT.Text);
				
					columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
					columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);
					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);
	

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.UpdateAllowed)
					{
						entityClass.fsoperationBeforeUpdate();

						new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);

						SessionObject.Set("_pk_NPH_CODE",columnNameValue.get("NPH_CODE"));

						
						entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPH_PHOLDER");

						//New Code By Izhar-ul-haque

						if(ddlNPH_INSUREDTYPE.SelectedValue=="Y")
						{
							DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='"+txtNU1_ACCOUNTNO.Text+"' WHERE NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");
						}
						else
						{
							DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='"+txtNU1_ACCOUNTNO.Text+"' WHERE NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");		
						}
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);

					columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.DeleteAllowed)
					{
						entityClass.fsoperationBeforeDelete();

						new LNPH_PHOLDER(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterDelete();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPH_PHOLDER");
					}
					else
					{
						PrintMessage("You are not autherized to Delete.");
					}
					DMLSucceeded = true;

					break;
				case (EnumControlArgs.Process):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);

					columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					string result="";
					if (_CustomArgName.Value == "ProcessName")
					{
						string processName = _CustomArgVal.Value;
						if (security.ProcessAllowed(processName))
						{
							Type type = Type.GetType(processName);											
							if (type != null)
							{
								shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
								NameValueCollection[] dataRows = new NameValueCollection[1];
								bool[] SelectedRowIndexes = new bool[1];
								dataRows[0] = columnNameValue;
								SelectedRowIndexes[0] = true;
								proccessCommand.setAllFields(columnNameValue);
								proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
								proccessCommand.setPrimaryKeys(LNPH_PHOLDER.PrimaryKeys);
								proccessCommand.setTableName("LNPH_PHOLDER");
								proccessCommand.setDataRows(dataRows);
								proccessCommand.setSelectedRows(SelectedRowIndexes);
								result = proccessCommand.processing();
							}
						}
						else
						{
							result = "You are not Authorized to Execute Process.";
						}
					}	
					//recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
			}
			string bankCode = "";
			string currentdBank = SessionObject.Get("s_CCH_CODE").ToString() + SessionObject.Get("s_CCD_CODE").ToString();
			rowset rs = DB.executeQuery("SELECT CSD.Csd_Value FROM LCSD_SYSTEMDTL CSD WHERE CSD.CSH_ID = 'CHBNK' AND CSD.CSD_TYPE = '"+ currentdBank +"'");
			if(rs.next())
				bankCode= rs.getString(1);
			string qry = string.Format("update lnu1_underwriti set pbb_branchcode = '{0}', pbK_bankcode = '{3}' where nu1_accountno = '{1}' and np1_proposal ='{2}' and nu1_life='F'" ,
				ddlBranch.SelectedValue,txtNU1_ACCOUNTNO.Text,SessionObject.Get("NP1_PROPOSAL").ToString(),bankCode);
			DB.executeDML(qry);

			LNP1_POLICYMASTRDB.UpdateBranchDetails(SessionObject.Get("NP1_PROPOSAL").ToString(),ddlBranch.SelectedValue);
		}
	
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			LNPH_PHOLDERDB LNPH_PHOLDERDB_obj = new LNPH_PHOLDERDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				

				
				DataRow row = LNPH_PHOLDERDB_obj.FindByPK(NPH_CODE,NPH_LIFE)["LNPH_PHOLDER"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					//	ShowData(dataHolder["LNPH_PHOLDER"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else
				{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else
				{
					_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();			
				}
			}
			//for header & footer script					
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("");
			
		}
		#endregion	

		#region Events
		/*
		private void btnSearchOccupation_Click(object sender, EventArgs e)
		{

		}
		*/
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
		{
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value)
			{
				case "Update" :
					ControlArgs[0]=EnumControlArgs.Update;
					CustomDoControl();
					break;
				case "Save" :
					ControlArgs[0]=EnumControlArgs.Save;
					CustomDoControl();
					break;
				case "Delete" :
					ControlArgs[0]=EnumControlArgs.Delete;
					CustomDoControl();
					break;
				case "Filter" :
					ControlArgs[0] = EnumControlArgs.Filter;
					CustomDoControl();
					break;
				case "Process" :
					ControlArgs[0] = EnumControlArgs.Process;
					CustomDoControl();
					break;

			}
			_CustomEventVal.Value="";	
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			if (SetFieldsInSession())
			{
				if (NewRecord == true  && DMLSucceeded == false)
				{
					SessionObject.Set("NPH_TITLE","");
					SessionObject.Set("NPH_IDTYPE","");
					SessionObject.Set("NP1_PROPOSAL","");
					SessionObject.Set("NPH_SEX","");
					SessionObject.Set("NPH_MARITALSTATUS","");
					SessionObject.Set("NPH_FULLNAME","");

					SessionObject.Set("NPH_FIRSTNAME","");
					SessionObject.Set("NPH_SECONDNAME","");
					SessionObject.Set("NPH_LASTNAME","");

					SessionObject.Set("NPH_FULLNAMEARABIC","");
					SessionObject.Set("NPH_BIRTHDATE","");
					SessionObject.Set("COP_OCCUPATICD","");
					SessionObject.Set("CCL_CATEGORYCD","");
					SessionObject.Set("NPH_INSUREDTYPE","");
					SessionObject.Set("NPH_CODE","");
					SessionObject.Set("NPH_LIFE","");
					SessionObject.Set("NU1_SMOKER","");
					SessionObject.Set("NU1_ACCOUNTNO","");
					SessionObject.Set("NPH_ANNUINCOME","");
				}
				else
				{
					SessionObject.Set("NPH_TITLE",ddlNPH_TITLE.SelectedValue);
					SessionObject.Set("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue);
					SessionObject.Set("NPH_SEX",ddlNPH_SEX.SelectedValue);
					SessionObject.Set("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue);
					SessionObject.Set("NPH_FULLNAME",txtNPH_FULLNAME.Text);

					SessionObject.Set("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text);
					SessionObject.Set("NPH_SECONDNAME",txtNPH_SECONDNAME.Text);
					SessionObject.Set("NPH_LASTNAME",txtNPH_LASTNAME.Text);

					SessionObject.Set("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text);
					SessionObject.Set("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text);
					SessionObject.Set("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue);
					SessionObject.Set("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue);
					SessionObject.Set("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue);
					SessionObject.Set("NPH_CODE",txtNPH_CODE.Text);
					SessionObject.Set("NPH_LIFE",txtNPH_LIFE.Text);
					SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue);
					SessionObject.Set("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text);
					SessionObject.Set("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text);
					SessionObject.Set("CNT_NATCD",ddlCNT_NATCD.SelectedValue);
				}
			}
		}										
	
		
		protected void btntest_click(object sender, EventArgs e) 
		{
			_CustomEventVal.Value ="Update";
			CustomDoControl();
		}
		#endregion 

		protected override bool TransactionRequired 
		{
			get 
			{
				return true;
			}
		}


		private void GetSessionValues()
		{
			if (false)
			{	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
			}
		}		

		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			ddlNPH_TITLE.ClearSelection();
			ddlNPH_IDTYPE.ClearSelection();
			ddlNPH_SEX.ClearSelection();
			ddlNPH_MARITALSTATUS.ClearSelection();
			txtNPH_FULLNAME.Text="";

			txtNPH_FIRSTNAME.Text="";
			txtNPH_SECONDNAME.Text="";
			txtNPH_LASTNAME.Text="";

			txtNPH_FULLNAMEARABIC.Text="";
			txtNPH_BIRTHDATE.Text="";
			ddlCOP_OCCUPATICD.ClearSelection();
			ddlCCL_CATEGORYCD.ClearSelection();
			ddlNPH_INSUREDTYPE.ClearSelection();
			txtNPH_CODE.Enabled = true;
			txtNPH_CODE.Text="";
			txtNPH_LIFE.Enabled = true;
			txtNPH_LIFE.Text="";
			ddlNU1_SMOKER.ClearSelection();
			txtNU1_ACCOUNTNO.Text="";
			txtCNIC_VALUE.Text="";
			txtNPH_IDNO2.Text="";
			txtNPH_ANNUINCOME.Text="";
			ddlCNT_NATCD.ClearSelection();
		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();

			ddlBranch.ClearSelection();
			CCS_CHANLSUBDETLDB ddlBranchDB = new CCS_CHANLSUBDETLDB(this.dataHolder);
			string BranchCode = CCS_CHANLSUBDETLDB.GetBranchCode((string)Session["NP1_PROPOSAL"],(string)Session["NU1_ACCOUNTNO"]);
			
			if (BranchCode != string.Empty)//Solved object reference not set to an instance of an object
			{

				if (ddlBranch.Items.Contains(ddlBranch.Items.FindByValue(BranchCode)) == true)
				{
					ddlBranch.SelectedValue = BranchCode;
				}
				else
				{
					ddlBranch.SelectedIndex = -1;
				}

			}
			else
			{
				ddlBranch.SelectedIndex = -1;
			}

			

				ddlNPH_TITLE.ClearSelection();
			ListItem item0 = ddlNPH_TITLE.Items.FindByValue(objRow["NPH_TITLE"].ToString());
			if (item0!= null)
			{
				item0.Selected = true;
			}

			ddlNPH_IDTYPE.ClearSelection();
			ListItem item11 = ddlNPH_IDTYPE.Items.FindByValue(objRow["NPH_IDTYPE"].ToString());
			if (item11!= null)
			{
				item11.Selected = true;
			}
			
			ddlNPH_SEX.ClearSelection();
			ListItem item1 = ddlNPH_SEX.Items.FindByValue(objRow["NPH_SEX"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}

			
			ddlNPH_MARITALSTATUS.ClearSelection();
			ListItem item2=ddlNPH_MARITALSTATUS.Items.FindByValue(objRow["NPH_MARITALSTATUS"].ToString());
			if (item2!= null)
			{
				item2.Selected=true;
			}

			
			txtNPH_FULLNAME.Text=objRow["NPH_FULLNAME"].ToString();
			
			txtNPH_FIRSTNAME.Text=objRow["NPH_FIRSTNAME"].ToString();
			txtNPH_SECONDNAME.Text=objRow["NPH_SECONDNAME"].ToString();
			txtNPH_LASTNAME.Text=objRow["NPH_LASTNAME"].ToString();

			txtCNIC_VALUE.Text = objRow["NPH_IDNO"].ToString();
			txtNPH_IDNO2.Text  = objRow["NPH_IDNO2"].ToString();
			
			//Format for NIC
			string NIC = txtCNIC_VALUE.Text;
			string concat = null;
			if(ace.clsIlasUtility.isNoorID() == false)
			{
				if(NIC.Length==Convert.ToInt16(13))
				{
					for(int i=0;i<=4;i++)
					{
						concat +=NIC[i];
					}
					concat+="-";

					for(int i=5;i<=11;i++)
					{
						concat +=NIC[i];
					}
					concat+="-"+NIC[12];
				}
				else
				{
					concat=NIC.ToString();
				}

				txtCNIC_VALUE.Text=concat.ToString();
			}

			
            
			//New Values

			ddlNPH_HEIGHTTYPE.SelectedValue=objRow["NPH_HEIGHTUOM"].ToString();
			double actualHeight= Math.Round(Convert.ToDouble(objRow["NPH_HEIGHTACTUAL"]),2);
			double acutalWeight = Math.Round(Convert.ToDouble(objRow["NPH_WEIGHTACTUAL"]),2);
			txtNU1_ACTUALHEIGHT.Text=actualHeight.ToString();
			txtNU1_CONVERTHEIGHT.Text=objRow["NPH_HEIGHT"].ToString();


			ddlNPH_WEIGHTTTYPE.SelectedValue=objRow["NPH_WEIGHTUOM"].ToString();
			txtNU1_ACTUALWEIGHT.Text=acutalWeight.ToString();
			txtNP2_AGEPREM.Text= Convert.ToString(ace.Ace_General.getEntryAge(Convert.ToString(SessionObject.Get("NP1_PROPOSAL"))));
			txtNU1_CONVERTWEIGHT.Text=objRow["NPH_WEIGHT"].ToString();
			
			/***************************************************************
			 Controlling of Height in case of Feet having following Values 
			 e.g. 5` 10``= 5.10 (5 Feet 10 inches)
			 
			NOTE: In DB 5.1 and 5.10 are same so we have to distinguish them.
			*****************************************************************/
			if(ddlNPH_HEIGHTTYPE.SelectedValue.ToUpper() == "F" )
			{
				if(txtNU1_CONVERTHEIGHT.Text == "1.780" || txtNU1_CONVERTHEIGHT.Text == "1.78" )
				{
					txtNU1_ACTUALHEIGHT.Text="5.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "1.470" || txtNU1_CONVERTHEIGHT.Text == "1.47" )
				{
					txtNU1_ACTUALHEIGHT.Text="4.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "2.080" || txtNU1_CONVERTHEIGHT.Text == "2.08" )
				{
					txtNU1_ACTUALHEIGHT.Text="6.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "1.170" || txtNU1_CONVERTHEIGHT.Text == "1.17" )
				{
					txtNU1_ACTUALHEIGHT.Text="3.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "2.390" || txtNU1_CONVERTHEIGHT.Text == "2.39" )
				{
					txtNU1_ACTUALHEIGHT.Text="7.10";
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "0.860" || txtNU1_CONVERTHEIGHT.Text == "0.86" )
				{
					txtNU1_ACTUALHEIGHT.Text="2.10";	
				}				
			}


			
			
			
			txtNPH_BIRTHDATE.Text=objRow["NPH_BIRTHDATE"]== DBNull.Value?"":((DateTime)objRow["NPH_BIRTHDATE"]).ToShortDateString();


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


			ddlCOP_OCCUPATICD.ClearSelection();

			//TODO: Check From lcui-Tbl to do this or Not
			if(!isOccupationEnabled())//if Disabled then
			{
				IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.getOccupationById(objRow["COP_OCCUPATICD"].ToString());
				ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
				ddlCOP_OCCUPATICD.DataBind();
				LCOP_OCCUPATIONReader1.Close();
				ddlCOP_OCCUPATICD.Enabled=false;
			}

			ListItem item5=ddlCOP_OCCUPATICD.Items.FindByValue(objRow["COP_OCCUPATICD"].ToString());
			if (item5!= null)
			{
				item5.Selected=true;
			}ddlCCL_CATEGORYCD.ClearSelection();
			ListItem item6=ddlCCL_CATEGORYCD.Items.FindByValue(objRow["CCL_CATEGORYCD"].ToString());
			if (item6!= null)
			{
				item6.Selected=true;
			}ddlNPH_INSUREDTYPE.ClearSelection();
			ListItem item7=ddlNPH_INSUREDTYPE.Items.FindByValue(objRow["NPH_INSUREDTYPE"].ToString());
			if (item7!= null)
			{
				item7.Selected=true;
			}txtNPH_CODE.Text=objRow["NPH_CODE"].ToString();
			txtNPH_CODE.Enabled=false;
			txtNPH_LIFE.Text=objRow["NPH_LIFE"].ToString();
			txtNPH_LIFE.Enabled=false;
			
			//Manual Code
			ddlNU1_SMOKER.ClearSelection();
			ListItem item8=ddlNU1_SMOKER.Items.FindByValue(Session["NU1_SMOKER"].ToString());
			if (item8!= null)
			{
				item8.Selected=true;
			}

			txtNU1_ACCOUNTNO.Text=Session["NU1_ACCOUNTNO"]==null ? "" :  Session["NU1_ACCOUNTNO"].ToString();

			ddlCNT_NATCD.ClearSelection();
			ListItem item10=ddlCNT_NATCD.Items.FindByValue(objRow["CNT_NATCD"].ToString());
			if (item10!= null)
			{
				item10.Selected=true;
			}
			//Manual Code


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
			foreach(string processName in AllProcess)
			{
				if (security.ProcessAllowed(processName))
				{
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed)
			{
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
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

		private NameValueCollection getAllFields() 
		{
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) 
			{
				string strKey = key.ToString();
				allFields.add(strKey,columnNameValue.get(strKey));
				
			}

			foreach (Control c in this.myForm.Controls) 
			{	
				string _fieldName="";
				if (c is WebControl) 
				{
					switch (c.GetType().ToString()) 
					{
						case "System.Web.UI.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName))
							{ 
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}

		bool IsRecordSelected()
		{
			bool selected = true;
			foreach (string pk in LNPH_PHOLDER.PrimaryKeys)
			{
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0)
				{
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord()
		{
			if (IsRecordSelected())
			{
				NPH_CODE=SessionObject.GetString("NPH_CODE");
				NPH_LIFE=SessionObject.GetString("NPH_LIFE");
	

				DataRow selectedRow = new LNPH_PHOLDERDB(dataHolder).FindByPK(NPH_CODE,NPH_LIFE)["LNPH_PHOLDER"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("");
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields
		{
			get
			{	
				return NormalEntryTableDiv.Controls;
			}
		}



		protected void CustomDoControl() 
		{
			base.DoControl();
			String lastEvent = _lastEvent.Text;
			

			if (!_lastEvent.Text.Equals("Delete"))
				_lastEvent.Text = "Edit";
			else
			{
				ClearSession();
				FirstStep();
			}		
		}


		private void ClearSession()
		{
			SessionObject.Remove("NPH_TITLE");
			SessionObject.Remove("NPH_IDTYPE");
			SessionObject.Remove("NPH_SEX");
			SessionObject.Remove("NPH_MARITALSTATUS");
			SessionObject.Remove("NPH_FULLNAME");
			
			SessionObject.Remove("NPH_FIRSTNAME");
			SessionObject.Remove("NPH_SECONDNAME");
			SessionObject.Remove("NPH_LASTNAME");

			SessionObject.Remove("NPH_FULLNAMEARABIC");
			SessionObject.Remove("NPH_BIRTHDATE");
			SessionObject.Remove("COP_OCCUPATICD");
			SessionObject.Remove("CCL_CATEGORYCD");
			SessionObject.Remove("NPH_INSUREDTYPE");
			SessionObject.Remove("NPH_CODE");
			SessionObject.Remove("NPH_LIFE");
			SessionObject.Remove("NU1_SMOKER");
			SessionObject.Remove("NU1_ACCOUNTNO");

			SessionObject.Remove("NPH_WEIGHTUOM");
			SessionObject.Remove("NPH_WEIGHTACTUAL");
			SessionObject.Remove("NPH_WEIGHT");
			
			SessionObject.Remove("NPH_HEIGHTUOM");
			SessionObject.Remove("NPH_HEIGHTACTUAL");
			SessionObject.Remove("NPH_HEIGHT");
			
			SessionObject.Remove("NU1_OVERUNDERWT");
			SessionObject.Remove("NPR_PREMIUMTER");
			SessionObject.Remove("NPH_ANNUINCOME");
			SessionObject.Remove("CNT_NATCD");

		}



		private void ViewInitialState()
		{
			if ((""+Session["flg_SELECETD"]).Equals("Y"))
			{
				SessionObject.Set("NPH_CODE",Session["NPH_CODE"]);
				SessionObject.Set("NPH_LIFE",Session["NPH_LIFE"]);
				SessionObject.Set("NU1_SMOKER",(Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"]));
				SessionObject.Set("NU1_ACCOUNTNO",Session["NU1_ACCOUNTNO"]);
				NPH_CODE = ""+Session["NPH_CODE"];
				NPH_LIFE = ""+Session["NPH_LIFE"];
				NU1_SMOKER = ""+ (Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"].ToString());
				NU1_ACCOUNTNO = ""+Session["NU1_ACCOUNTNO"];
			}
			else
			{
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE, NU1_SMOKER, NU1_ACCOUNTNO from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='F'");
				if (rsLNPH_PHOLDERDB.next())
				{
					SessionObject.Set("NPH_CODE",rsLNPH_PHOLDERDB.getString(1));
					SessionObject.Set("NPH_LIFE",rsLNPH_PHOLDERDB.getString(2));
					SessionObject.Set("NU1_SMOKER",rsLNPH_PHOLDERDB.getString(3));
					SessionObject.Set("NU1_ACCOUNTNO",rsLNPH_PHOLDERDB.getString(4));
				}
			}

			SessionObject.Remove("flg_SELECETD");

		}

		private bool isNewClientForBANCA(string nphCode)
		{
			bool foundInBanc = ace.ILUS_ET_NM_PER_PERSONALDET.searchClientByCode_BANCA(nphCode);
			if(foundInBanc == true)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


	}
}

