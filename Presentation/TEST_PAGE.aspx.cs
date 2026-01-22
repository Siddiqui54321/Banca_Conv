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

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public class TEST_PAGE : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls
		protected System.Web.UI.HtmlControls.HtmlForm myForm;

		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomArgName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomEventVal;
		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomArgVal;
		protected System.Web.UI.HtmlControls.HtmlInputButton _CustomEvent;

		//		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		//		protected System.Web.UI.WebControls.DropDownList pagerList;
		protected System.Web.UI.WebControls.Literal _lastEvent;
		protected System.Web.UI.WebControls.Literal _lastEventProcess;
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;

		protected System.Web.UI.WebControls.Label lblFaceValue;
		
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblName2;
		protected System.Web.UI.WebControls.Label lblAge;
		protected System.Web.UI.WebControls.Label lblAge2;
		protected System.Web.UI.WebControls.Label lblGender;
		protected System.Web.UI.WebControls.Label lblGender2;
		
		protected System.Web.UI.HtmlControls.HtmlGenericControl NormalEntryTableDiv;

		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";

		
		/******************* Entity Fields Decleration *****************/
		//protected SHMA.Enterprise.Presentation.WebControls.DatePopUp txtNP2_COMMENDATE;
		//protected System.Web.UI.WebControls.CompareValidator cfvNP2_COMMENDATE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_AGEPREM;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_AGEPREM2ND;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPPR_PRODCD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPPR_PRODCD;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPCU_CURRCODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_CURRCODE;

		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlCCB_CODE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_SUMASSURED;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_SUMASSURED_2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCCB_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_SUMASSURED;


		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPCU_CURRCODE_PRM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_CURRCODE_PRM;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlCMO_MODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCMO_MODE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PAIDUPTOAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_PAIDUPTOAGE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_BENEFITTERM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_BENEFITTERM;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PREMIUMTER;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PREMIUMDISCOUNT;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_PREMIUMTER;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PREMIUM;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPR_INCLUDELOADINNIV;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_INCLUDELOADINNIV;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_EXCESPRMANNUAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_EXCESPRMANNUAL;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPR_COMMLOADING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_COMMLOADING;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP1_PERIODICPREM;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPCU_AVCURRCODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_AVCURRCODE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP1_TOTALRIDERPREM;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_TOTPREM;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_TOTPREM_2;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP1_PROPOSAL;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_SETNO;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_BASICFLAG;
		
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP1_RETIREMENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_RETIREMENTAGE;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP1_TOTALANNUALPREM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_TOTALANNUALPREM;

		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NP1_PROPOSAL;
		private double  NP2_SETNO;
		private string  PPR_PRODCD;

		protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPOSAL;
		protected System.Web.UI.WebControls.CompareValidator cfvNP2_SETNO;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_BASICFLAG;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP2_COMMENDATE;
		protected System.Web.UI.WebControls.CompareValidator cfvNP2_AGEPREM;
		protected System.Web.UI.WebControls.CompareValidator cfvNP2_AGEPREM2ND;
		protected System.Web.UI.WebControls.CompareValidator cfvPPR_PRODCD;
		protected System.Web.UI.WebControls.CompareValidator cfvPCU_CURRCODE;
		
		protected System.Web.UI.WebControls.CompareValidator cfvCCB_CODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_SUMASSURED;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_SUMASSURED_2;

		protected System.Web.UI.WebControls.CompareValidator cfvPCU_CURRCODE_PRM;
		protected System.Web.UI.WebControls.CompareValidator cfvCMO_MODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_PAIDUPTOAGE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_BENEFITTERM;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_PREMIUMTER;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_PREMIUM;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_INCLUDELOADINNIV;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_EXCESPRMANNUAL;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_COMMLOADING;
		protected System.Web.UI.WebControls.CompareValidator cfvNP1_PERIODICPREM;
		protected System.Web.UI.WebControls.CompareValidator cfvPCU_AVCURRCODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNP1_TOTALRIDERPREM;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_TOTPREM;
		protected System.Web.UI.WebControls.CompareValidator cfvNPR_TOTPREM_2;

		protected System.Web.UI.WebControls.RequiredFieldValidator msgCCB_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator msgNPR_SUMASSURED;

		protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
		protected System.Web.UI.WebControls.Label lblServerError;

		protected System.Web.UI.WebControls.CompareValidator cfvNP1_RETIREMENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator msgNP1_RETIREMENTAGE;
		protected System.Web.UI.WebControls.CompareValidator cfvNP1_TOTALANNUALPREM;
		protected System.Web.UI.WebControls.RequiredFieldValidator msgNP1_TOTALANNUALPREM;

		private string NP1_RETIREMENTAGE;
		private string NP1_TOTALANNUALPREM;
						
		#endregion
		
		
						
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent() 
		{
			this._CustomEvent.ServerClick += new System.EventHandler(this._CustomEvent_ServerClick);
			this.Unload += new System.EventHandler(this.Page_Unload);
			this.Load += new System.EventHandler(this.Page_Load);

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
			//CheckKeyLevel();
			//recordCount = LNPR_PRODUCTDB.RecordCount;
			return   dataHolder;         

		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{


			//TODO: SESSION SETTING in behavior ViewInitialState()
			//***********************CUSTOM CODE ***********************/
			ViewInitialState();
			//***********************CUSTOM CODE ***********************/



			IDataReader LPPR_PRODUCTReader0 = LPPR_PRODUCTDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO();;
			ddlPPR_PRODCD.DataSource = LPPR_PRODUCTReader0 ;
			ddlPPR_PRODCD.DataBind();
			LPPR_PRODUCTReader0.Close();
			IDataReader PCU_CURRENCYReader1 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO();;
			ddlPCU_CURRCODE.DataSource = PCU_CURRENCYReader1 ;
			ddlPCU_CURRCODE.DataBind();
			PCU_CURRENCYReader1.Close();
			IDataReader PCU_CURRENCYReader2 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO();;
			ddlPCU_CURRCODE_PRM.DataSource = PCU_CURRENCYReader2 ;
			ddlPCU_CURRCODE_PRM.DataBind();
			PCU_CURRENCYReader2.Close();
			IDataReader LCMO_MODEReader3 = LCMO_MODEDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO();;
			ddlCMO_MODE.DataSource = LCMO_MODEReader3 ;
			ddlCMO_MODE.DataBind();
			LCMO_MODEReader3.Close();
			IDataReader LCSD_SYSTEMDTLReader4 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_INCLUDELOADINNIV_RO();;
			ddlNPR_INCLUDELOADINNIV.DataSource = LCSD_SYSTEMDTLReader4 ;
			ddlNPR_INCLUDELOADINNIV.DataBind();
			LCSD_SYSTEMDTLReader4.Close();
			IDataReader LCSD_SYSTEMDTLReader5 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_COMMLOADING_RO();;
			ddlNPR_COMMLOADING.DataSource = LCSD_SYSTEMDTLReader5 ;
			ddlNPR_COMMLOADING.DataBind();
			LCSD_SYSTEMDTLReader5.Close();
			IDataReader PCU_CURRENCYReader6 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_AVCURRCODE_RO();;
			ddlPCU_AVCURRCODE.DataSource = PCU_CURRENCYReader6 ;
			ddlPCU_AVCURRCODE.DataBind();
			PCU_CURRENCYReader6.Close();

			_lastEvent.Text = "New";		


			rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+ SessionObject.Get("NP1_PROPOSAL")+"'");
			if (LNP2_POLICYMASTR.next())
			{
				txtNP2_AGEPREM.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
				txtNP2_AGEPREM2ND.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM2ND").ToString();
			}
			else
			{
				txtNP2_AGEPREM.Text="0";
				txtNP2_AGEPREM2ND.Text="0";
			}

			
			FindAndSelectCurrentRecord();
			HeaderScript.Text = EnvHelper.Parse("") ;




			//FooterScript.Text = EnvHelper.Parse("getField(\"NP2_COMMENDATE\").value='"+SessionObject.Get("NP2_COMMENDATE")+"';var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\");var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");
			FooterScript.Text = EnvHelper.Parse("var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\");var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");

			ddlCCB_CODE.Attributes.Add("onchange","setFaceValueField(this.value);");
			//txtNPR_SUMASSURED.Attributes.Add("onblur","applyNumberFormat(this,2);validate(this,'SUMASSURED');");
			//txtNPR_SUMASSURED.Attributes.Add("onfocus","validateInfo(this,'SUMASSURED');");

			txtNPR_SUMASSURED.Attributes.Add("onblur","applyNumberFormat(this,2); if(getField('CCB_CODE').value=='S') validate(this,'SUMASSURED');");
			txtNPR_SUMASSURED.Attributes.Add("onfocus","if(getField('CCB_CODE').value=='S') validateInfo(this,'SUMASSURED');");

			txtNPR_TOTPREM.Attributes.Add("onblur","applyNumberFormat(this,2);if(getField('CCB_CODE').value=='T') validate(this,'TOTPREM');");
			txtNPR_TOTPREM.Attributes.Add("onfocus","if(getField('CCB_CODE').value=='T') validateInfo(this,'TOTPREM');");
			
			txtNPR_EXCESPRMANNUAL.Attributes.Add("onblur","applyNumberFormat(this,2);");
			txtNPR_BENEFITTERM.Attributes.Add("onblur","setPremiumTerm(this);validate(this,'TERM');");
			txtNPR_BENEFITTERM.Attributes.Add("onfocus","validateInfo(this,'TERM');");

			txtNPR_PREMIUMTER.Attributes.Add("onblur","validatePremiumTerm(this);");

			txtNPR_PAIDUPTOAGE.Attributes.Add("onblur","setBenefitTerm(this);validate(this,'MATURITYAGE');");
			txtNPR_PAIDUPTOAGE.Attributes.Add("onfocus","validateInfo(this,'MATURITYAGE');");
			txtNPR_PREMIUMDISCOUNT.Attributes.Add("onblur","discountPremium(this);applyNumberFormat(this,2);");

			

			
			ddlCMO_MODE.Attributes.Add("onchange","disableViewPremiumUptoAge(this);");

			ddlPPR_PRODCD.Attributes.Add("onchange","setViewForProduct(this);setOptions(this);");



			
			
		
		
		

			//RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			//***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	
			if (SessionObject.GetString("USE_TYPE")=="S")
			{
				txtNPR_PREMIUMDISCOUNT.Enabled = true;
			}
			else
			{
				txtNPR_PREMIUMDISCOUNT.Enabled = false;
			}

		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPR_PRODUCT.PrimaryKeys)
			{
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null)
				{
					if (ctrl is WebControl)
					{
						//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null))
						{
							control.Enabled = true;
						}				
					}
				}
			}			
		}
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PLANDETAILS();
			entityClass.setNameValueCollection(columnNameValue);



			NameValueCollection columnNameValueNonBase = new NameValueCollection();

			columnNameValueNonBase.Add("PCU_CURRCODE_PRM",ddlPCU_CURRCODE_PRM.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE_PRM.SelectedValue);
			columnNameValueNonBase.Add("CMO_MODE",ddlCMO_MODE.SelectedValue.Trim()==""?null:ddlCMO_MODE.SelectedValue);
			columnNameValueNonBase.Add("PCU_AVCURRCODE",ddlPCU_AVCURRCODE.SelectedValue.Trim()==""?null:ddlPCU_AVCURRCODE.SelectedValue);
			columnNameValueNonBase.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
			columnNameValueNonBase.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
			columnNameValueNonBase.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
			columnNameValueNonBase.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
			columnNameValueNonBase.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
			columnNameValueNonBase.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));

			//TODO: Insert value from Session
			//?????
			txtNP1_PROPOSAL.Text = ""+SessionObject.Get("NP1_PROPOSAL");
			txtNP2_SETNO.Text = "1";
			
			entityClass.setNameValueCollection(columnNameValueNonBase);

			//Custom : Change
			SessionObject.Set("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text.Trim()==""?null:(object)double.Parse(txtNP1_RETIREMENTAGE.Text));
			SessionObject.Set("NP1_TOTALANNUALPREM",txtNP1_TOTALANNUALPREM.Text.Trim()==""?null:(object)double.Parse(txtNP1_TOTALANNUALPREM.Text));
			//Custom : Change
		

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;
					
					//TODO: Insert value from Session
					txtNP1_PROPOSAL.Text = ""+SessionObject.Get("NP1_PROPOSAL");
					txtNP2_SETNO.Text = "1";

					
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE.SelectedValue);
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
					columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
					columnNameValue.Add("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue.Trim()==""?null:ddlNPR_COMMLOADING.SelectedValue);
					columnNameValue.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("NPR_BASICFLAG","Y");
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.SaveAllowed)
					{
						entityClass.fsoperationBeforeSave();
				
						new LNPR_PRODUCT(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PLANDETAILS",null);


						
						dataHolder.Update(DB.Transaction);
						
						
						entityClass.fsoperationAfterSave();
				
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPR_PRODUCT");
						_lastEvent.Text = "Save"; 					
						//PrintMessage("Record has been saved");
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),ddlPPR_PRODCD.SelectedValue);				
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE.SelectedValue);
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
					columnNameValue.Add("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
					columnNameValue.Add("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue.Trim()==""?null:ddlNPR_COMMLOADING.SelectedValue);
					columnNameValue.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("NPR_BASICFLAG","Y");

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.UpdateAllowed)
					{
				

						rowset rsProduct = DB.executeQuery("select PPR_PRODCD from LNPR_PRODUCT where NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' and NPR_BASICFLAG='Y'" );
						rsProduct.next();

						if (!ddlPPR_PRODCD.SelectedValue.Trim().Equals(rsProduct.getString("PPR_PRODCD")))
						{
							string insert = "insert into LNPR_PRODUCT (PPR_PRODCD, PCU_CURRCODE, CCB_CODE, NPR_SUMASSURED, NPR_PAIDUPTOAGE, NPR_BENEFITTERM, NPR_PREMIUMDISCOUNT, NPR_PREMIUMTER, NPR_PREMIUM, NPR_INCLUDELOADINNIV, NPR_EXCESSPREMIUM, NPR_COMMLOADING, NPR_TOTPREM, NP1_PROPOSAL, NP2_SETNO, NPR_BASICFLAG)" 
									+ " select '"+ddlPPR_PRODCD.SelectedValue+"', PCU_CURRCODE, CCB_CODE, NPR_SUMASSURED, NPR_PAIDUPTOAGE, NPR_BENEFITTERM, NPR_PREMIUMDISCOUNT, NPR_PREMIUMTER, NPR_PREMIUM, NPR_INCLUDELOADINNIV, NPR_EXCESSPREMIUM, NPR_COMMLOADING, NPR_TOTPREM, NP1_PROPOSAL, NP2_SETNO, NPR_BASICFLAG from LNPR_PRODUCT where NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' and NPR_BASICFLAG='Y'";
							string delete = "delete from LNPR_PRODUCT where NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' and (PPR_PRODCD='" + rsProduct.getString("PPR_PRODCD")+ "')";
							string delete1 = "delete from LNLO_LOADING where  NP1_PROPOSAL ='"+txtNP1_PROPOSAL.Text+"'";

							DB.executeDML(insert);
							DB.executeDML(delete1);
							DB.executeDML(delete);

						}
						else
						{
							entityClass.fsoperationBeforeUpdate();
							new LNPR_PRODUCT(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);
							dataHolder.Update(DB.Transaction);
						}

				
						entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPR_PRODUCT");
						//recordSelected = true;
						//PrintMessage("Record has been updated");
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),ddlPPR_PRODCD.SelectedValue);				
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE.SelectedValue);
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
					columnNameValue.Add("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
					columnNameValue.Add("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue.Trim()==""?null:ddlNPR_COMMLOADING.SelectedValue);
					columnNameValue.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("NPR_BASICFLAG",txtNPR_BASICFLAG.Text.Trim()==""?null:txtNPR_BASICFLAG.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.DeleteAllowed)
					{
			
						entityClass.fsoperationBeforeDelete();
						new LNPR_PRODUCT(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterDelete();
			
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPR_PRODUCT");
						//PrintMessage("Record has been deleted");				
					}
					else
					{
						PrintMessage("You are not autherized to Delete.");
					}
					break;
				case (EnumControlArgs.Process):						
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse(txtNP2_SETNO.Text),ddlPPR_PRODCD.SelectedValue);				
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE.SelectedValue);
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
					columnNameValue.Add("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
					columnNameValue.Add("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue.Trim()==""?null:ddlNPR_COMMLOADING.SelectedValue);
					columnNameValue.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("NPR_BASICFLAG",txtNPR_BASICFLAG.Text.Trim()==""?null:txtNPR_BASICFLAG.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
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
								proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
								proccessCommand.setTableName("LNPR_PRODUCT");
								proccessCommand.setDataRows(dataRows);
								proccessCommand.setSelectedRows(SelectedRowIndexes);
								result = proccessCommand.processing();
								//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
							}
						}
						else
						{
							result = "You are not Autherized to Execute Process.";
						}						
					}	
					//recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					break;
			}
		}

		
	
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			

			
			
			LNPR_PRODUCTDB LNPR_PRODUCTDB_obj = new LNPR_PRODUCTDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				DataRow row = LNPR_PRODUCTDB_obj.FindByPK(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["LNPR_PRODUCT"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
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
			//RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	
			//***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	

			HeaderScript.Text = EnvHelper.Parse("");
			//FooterScript.Text = EnvHelper.Parse("getField(\"NP2_COMMENDATE\").value='"+SessionObject.Get("NP2_COMMENDATE")+"';var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\");var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");
			FooterScript.Text = EnvHelper.Parse("var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\");var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");

		
		}
		#endregion	

		#region Events
		private void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
		{
			_lastEventProcess.Text = "";
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
					_lastEventProcess.Text = "Process";
					ControlArgs[0] = EnumControlArgs.Process;
					CustomDoControl();
					break;

			}
			_CustomEventVal.Value="";	
		}
		private void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			if (SetFieldsInSession())
			{
				//SessionObject.Set("NP2_COMMENDATE",txtNP2_COMMENDATE.Text);
				//SessionObject.Set("NP2_COMMENDATE",SessionObject.Get("NP2_COMMENDATE"));
				SessionObject.Set("NP2_AGEPREM",txtNP2_AGEPREM.Text);
				SessionObject.Set("NP2_AGEPREM2ND",txtNP2_AGEPREM2ND.Text);
				SessionObject.Set("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue);
				SessionObject.Set("PCU_CURRCODE",ddlPCU_CURRCODE.SelectedValue);
				SessionObject.Set("CCB_CODE",ddlCCB_CODE.SelectedValue);
				SessionObject.Set("NPR_SUMASSURED",txtNPR_SUMASSURED.Text);
				SessionObject.Set("PCU_CURRCODE_PRM",ddlPCU_CURRCODE_PRM.SelectedValue);
				SessionObject.Set("CMO_MODE",ddlCMO_MODE.SelectedValue);
				SessionObject.Set("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text);
				SessionObject.Set("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text);
				SessionObject.Set("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text);
				SessionObject.Set("NPR_PREMIUM",txtNPR_PREMIUM.Text);
				SessionObject.Set("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text);
				SessionObject.Set("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue);
				SessionObject.Set("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text);
				SessionObject.Set("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue);
				SessionObject.Set("NP1_PERIODICPREM",txtNP1_PERIODICPREM.Text);
				SessionObject.Set("PCU_AVCURRCODE",ddlPCU_AVCURRCODE.SelectedValue);
				SessionObject.Set("NP1_TOTALRIDERPREM",txtNP1_TOTALRIDERPREM.Text);
				SessionObject.Set("NPR_TOTPREM",txtNPR_TOTPREM.Text);
				SessionObject.Set("NP1_PROPOSAL",txtNP1_PROPOSAL.Text);
				SessionObject.Set("NP2_SETNO",txtNP2_SETNO.Text);
				SessionObject.Set("NPR_BASICFLAG",txtNPR_BASICFLAG.Text);
				
				SessionObject.Set("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text);
				SessionObject.Set("NP1_TOTALANNUALPREM",txtNP1_TOTALANNUALPREM.Text);
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


		private void GetSessionValues()
		{
			if (false)
			{	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				



				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		

		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			//txtNP2_COMMENDATE.Text="";
			txtNP2_AGEPREM.Text="0";
			txtNP2_AGEPREM2ND.Text="0";
			ddlPPR_PRODCD.Enabled = true;
			ddlPPR_PRODCD.ClearSelection();
			ddlPCU_CURRCODE.ClearSelection();
			ddlCCB_CODE.ClearSelection();
			txtNPR_SUMASSURED.Text="";
			txtNPR_SUMASSURED_2.Text="";
			ddlPCU_CURRCODE_PRM.ClearSelection();
			ddlCMO_MODE.ClearSelection();
			txtNPR_PAIDUPTOAGE.Text="";
			txtNPR_BENEFITTERM.Text="";
			txtNPR_PREMIUMTER.Text="";
			txtNPR_PREMIUMDISCOUNT.Text = "";
			txtNPR_PREMIUM.Text="0";
			ddlNPR_INCLUDELOADINNIV.ClearSelection();
			txtNPR_EXCESPRMANNUAL.Text="";
			ddlNPR_COMMLOADING.ClearSelection();
			txtNP1_PERIODICPREM.Text="0";
			ddlPCU_AVCURRCODE.ClearSelection();
			txtNP1_TOTALRIDERPREM.Text="0";
			txtNPR_TOTPREM.Text="0";
			txtNPR_TOTPREM_2.Text="0";
			txtNP1_PROPOSAL.Enabled = true;
			txtNP1_PROPOSAL.Text="";
			txtNP2_SETNO.Enabled = true;
			txtNP2_SETNO.Text="0";
			txtNPR_BASICFLAG.Text="";
			txtNP1_RETIREMENTAGE.Text="";
			txtNP1_TOTALANNUALPREM.Text="";
			if (SessionObject.GetString("USE_TYPE")=="S")
			{
				txtNPR_PREMIUMDISCOUNT.Enabled = true;
			}
			else
			{
				txtNPR_PREMIUMDISCOUNT.Enabled = false;
			}
		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();

			rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"' AND NP2_SETNO="+NP2_SETNO);
			if (LNP2_POLICYMASTR.next())
			{
				txtNP2_AGEPREM.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
				txtNP2_AGEPREM2ND.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM2ND").ToString();
			}
			
			rowset rowLNPR_PRODUCT = DB.executeQuery("select NPR_PREMIUM, NPR_EXCESSPREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"' AND NP2_SETNO="+NP2_SETNO+ " AND PPR_PRODCD='"+ PPR_PRODCD+ "'");
			if (rowLNPR_PRODUCT.next())
			{
				String	strNPR_PREMIUM= rowLNPR_PRODUCT.getDouble("NPR_PREMIUM").ToString();
				String	strNPR_EXCESSPREMIUM= rowLNPR_PRODUCT.getDouble("NPR_EXCESSPREMIUM").ToString();
				String	strtotal_NPR_PREMIUM=Convert.ToString(SessionObject.GetString("total_NPR_PREMIUM"));

				double val1=0;
				val1=strNPR_PREMIUM==String.Empty?0:Double.Parse(strNPR_PREMIUM) ;
				val1+=strNPR_EXCESSPREMIUM==String.Empty?0:Double.Parse(strNPR_EXCESSPREMIUM);
				txtNP1_PERIODICPREM.Text=val1.ToString();

				//by asif 06 jan - val1+=strtotal_NPR_PREMIUM==String.Empty?0:Double.Parse(strtotal_NPR_PREMIUM);
				//by asif 06 jan - txtNPR_TOTPREM.Text=val1.ToString();

				txtNP1_TOTALRIDERPREM.Text=strtotal_NPR_PREMIUM;
			}

			//by asif 06 jan - txtNPR_TOTPREM.Text = Convert.ToString(ace.Ace_General.getPremium(""+SessionObject.Get("NP1_PROPOSAL")));
			//txtNPR_TOTPREM_2.Text = Convert.ToString(ace.Ace_General.getPremium(""+SessionObject.Get("NP1_PROPOSAL")));


			string query ="SELECT NPH_FULLNAME, NPH_SEX, NPH_BIRTHDATE, NPH_AGE FROM LNPH_PHOLDER WHERE NPH_CODE||NPH_LIFE IN( select NPH_CODE||NPH_LIFE from LNU1_UNDERWRITI WHERE NP1_PROPOSAL ='"+SessionObject.Get("NP1_PROPOSAL")+"')";
			rowset rsPHolder = DB.executeQuery(query);
			
			if(rsPHolder.next())
			{
				lblName.Text = rsPHolder.getString("NPH_FULLNAME");
				//DateTime dt = rsPHolder.getDate("NPH_BIRTHDATE").Year ;
				lblAge.Text = ace.Ace_General.calculate_Age_InYear(rsPHolder.getDate("NPH_BIRTHDATE")).ToString() ;
				lblGender.Text = rsPHolder.getString("NPH_SEX");
			}
			//2nd life
			if(rsPHolder.next())
			{
				lblName2.Text = rsPHolder.getString("NPH_FULLNAME");
				lblAge2.Text = ace.Ace_General.calculate_Age_InYear(rsPHolder.getDate("NPH_BIRTHDATE")).ToString() ;
				lblGender2.Text = rsPHolder.getString("NPH_SEX");
			}
			

			//Manual Code
			txtNP1_RETIREMENTAGE.Text=Session["NP1_RETIREMENTAGE"]==null ? "" :  Session["NP1_RETIREMENTAGE"].ToString();
			txtNP1_TOTALANNUALPREM.Text=Session["NP1_TOTALANNUALPREM"]==null ? "" :  Session["NP1_TOTALANNUALPREM"].ToString();
			//Manual Code



			ddlPPR_PRODCD.ClearSelection();
			ListItem item0=ddlPPR_PRODCD.Items.FindByValue(objRow["PPR_PRODCD"].ToString());
			if (item0!= null)
			{
				item0.Selected=true;
			}ddlPPR_PRODCD.Enabled=false;

			ddlPCU_CURRCODE.ClearSelection();
			ListItem item1=ddlPCU_CURRCODE.Items.FindByValue(objRow["PCU_CURRCODE"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}
			ddlCCB_CODE.ClearSelection();
			ListItem item8=ddlCCB_CODE.Items.FindByValue(objRow["CCB_CODE"].ToString());
			if (item8!= null)
			{
				item8.Selected=true;
			}

			/*if(objRow["CCB_CODE"].ToString()=="S")
			{
				lblFaceValue.Text="Sum Assured";
				txtNPR_TOTPREM.Visible=false;
				txtNPR_TOTPREM.Width=0;
			}
			else
			{
				lblFaceValue.Text="Total Premium";
				txtNPR_SUMASSURED.Visible=false;			
				txtNPR_SUMASSURED.Width=0;
			}*/
			
			
			txtNPR_SUMASSURED.Text=objRow["NPR_SUMASSURED"].ToString();
			txtNPR_SUMASSURED_2.Text=objRow["NPR_SUMASSURED"].ToString();

			txtNPR_PAIDUPTOAGE.Text=objRow["NPR_PAIDUPTOAGE"].ToString();
			txtNPR_BENEFITTERM.Text=objRow["NPR_BENEFITTERM"].ToString();
			txtNPR_PREMIUMTER.Text=objRow["NPR_PREMIUMTER"].ToString();
			txtNPR_PREMIUMDISCOUNT.Text = objRow["NPR_PREMIUMDISCOUNT"].ToString();
			txtNPR_PREMIUM.Text=objRow["NPR_PREMIUM"].ToString();
			ddlNPR_INCLUDELOADINNIV.ClearSelection();


			ListItem item7=ddlNPR_INCLUDELOADINNIV.Items.FindByValue(objRow["NPR_INCLUDELOADINNIV"].ToString());
			if (item7!= null)
			{
				item7.Selected=true;
			}txtNPR_EXCESPRMANNUAL.Text=objRow["NPR_EXCESSPREMIUM"].ToString();
			ddlNPR_COMMLOADING.ClearSelection();
			ListItem item9=ddlNPR_COMMLOADING.Items.FindByValue(objRow["NPR_COMMLOADING"].ToString());
			if (item9!= null)
			{
				item9.Selected=true;
			}
			
			//Custom fill

			
			//Pick data
			String strPCU_CURRCODE_PRM = "";
			String strCMO_MODE = "";
			String strPCU_AVCURRCODE = "";


			rowset rsLNP1_POLICYMASTR = DB.executeQuery("select PCU_CURRCODE, CMO_MODE, PCU_AVCURRCODE from lnp1_policymastr where np1_proposal ='"+SessionObject.Get("NP1_PROPOSAL") + "'");
			if (rsLNP1_POLICYMASTR.next())
			{
				strPCU_CURRCODE_PRM = rsLNP1_POLICYMASTR.getString(1);
				strCMO_MODE = rsLNP1_POLICYMASTR.getString(2);
				strPCU_AVCURRCODE = rsLNP1_POLICYMASTR.getString(3);
			}

			//Set data
			
			//PCU_CURRCODE_PRM
			ddlPCU_CURRCODE_PRM.ClearSelection();
			ListItem item2=ddlPCU_CURRCODE_PRM.Items.FindByValue(strPCU_CURRCODE_PRM);
			if (item2!= null)
			{
				item2.Selected=true;
			}ddlPCU_CURRCODE_PRM.Enabled=true;

			//CMO_MODE
			ddlCMO_MODE.ClearSelection();
			ListItem item3=ddlCMO_MODE.Items.FindByValue(strCMO_MODE);
			if (item3!= null)
			{
				item3.Selected=true;
			}ddlCMO_MODE.Enabled=true;

			//PCU_AVCURRCODE
			ddlPCU_AVCURRCODE.ClearSelection();
			ListItem item4=ddlPCU_AVCURRCODE.Items.FindByValue(strPCU_AVCURRCODE);
			if (item4!= null)
			{
				item4.Selected=true;
			}ddlPCU_AVCURRCODE.Enabled=true;


			//Custom fill end

			
			txtNPR_TOTPREM.Text=objRow["NPR_TOTPREM"].ToString();
			txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
			txtNP1_PROPOSAL.Enabled=false;
			txtNP2_SETNO.Text=objRow["NP2_SETNO"].ToString();
			txtNP2_SETNO.Enabled=false;
			txtNPR_BASICFLAG.Text=objRow["NPR_BASICFLAG"].ToString();


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
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
			MessageScript.Text += string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
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
				allFields.add(strKey, columnNameValue.get(strKey));
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
			foreach (string pk in LNPR_PRODUCT.PrimaryKeys)
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
				PPR_PRODCD=SessionObject.GetString("PPR_PRODCD");
				NP1_PROPOSAL=SessionObject.GetString("NP1_PROPOSAL");
				NP2_SETNO=double.Parse(SessionObject.GetString("NP2_SETNO"));
	

				DataRow selectedRow = new LNPR_PRODUCTDB(dataHolder).FindByPK(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
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
			//SessionObject.Remove("NP2_COMMENDATE");
			SessionObject.Remove("NP2_AGEPREM");
			SessionObject.Remove("NP2_AGEPREM2ND");
			SessionObject.Remove("PPR_PRODCD");
			SessionObject.Remove("PCU_CURRCODE");
			SessionObject.Remove("CCB_CODE");
			SessionObject.Remove("NPR_SUMASSURED");
			SessionObject.Remove("PCU_CURRCODE_PRM");
			SessionObject.Remove("CMO_MODE");
			SessionObject.Remove("NPR_PAIDUPTOAGE");
			SessionObject.Remove("NPR_BENEFITTERM");
			SessionObject.Remove("NPR_PREMIUMTER");
			SessionObject.Remove("NPR_PREMIUM");
			SessionObject.Remove("NPR_INCLUDELOADINNIV");
			SessionObject.Remove("NPR_EXCESSPREMIUM");
			SessionObject.Remove("NPR_COMMLOADING");
			SessionObject.Remove("NP1_PERIODICPREM");
			SessionObject.Remove("PCU_AVCURRCODE");
			SessionObject.Remove("NP1_TOTALRIDERPREM");
			SessionObject.Remove("NPR_TOTPREM");
			//SessionObject.Remove("NP1_PROPOSAL");
			SessionObject.Remove("NP2_SETNO");
			SessionObject.Remove("NPR_BASICFLAG");
			SessionObject.Remove("NP1_RETIREMENTAGE");
			SessionObject.Remove("NP1_TOTALANNUALPREM");
		}


		private void ViewInitialState()
		{
			//SessionObject.Set("NP1_PROPOSAL","R/07/0010042");
			//SessionObject.Set("NP2_SETNO","1");
			rowset rsPPR_PRODCD = DB.executeQuery("SELECT PPR_PRODCD FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL = '"+SessionObject.Get("NP1_PROPOSAL")+"' AND NVL(NPR_BASICFLAG,'N') = 'Y'");
			if (rsPPR_PRODCD.next())
				SessionObject.Set("PPR_PRODCD",rsPPR_PRODCD.getString(1));

			rowset rsPolicyMaster = DB.executeQuery("select NP1_RETIREMENTAGE, NP1_TOTALANNUALPREM from lnp1_policymastr where NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"'");
			if (rsPolicyMaster.next())
			{
				SessionObject.Set("NP1_RETIREMENTAGE",rsPolicyMaster.getString("NP1_RETIREMENTAGE"));
				SessionObject.Set("NP1_TOTALANNUALPREM",rsPolicyMaster.getString("NP1_TOTALANNUALPREM"));
			}

		}

	}
}

