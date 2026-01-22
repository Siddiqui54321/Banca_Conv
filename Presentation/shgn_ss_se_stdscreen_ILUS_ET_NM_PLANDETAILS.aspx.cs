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
using Security;
using Bancassurance.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace SHAB.Presentation
{
    //shgn_gs_se_stdgridscreen_
   

    public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS : SHMA.Enterprise.Presentation.TwoStepController
    {
        DataTable dtAgent = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        string Sql;

        //controls


        //		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
        //		protected System.Web.UI.WebControls.DropDownList pagerList;
        protected System.Web.UI.WebControls.Literal _lastEvent;
        protected System.Web.UI.WebControls.Literal _lastEventProcess;

        protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

        protected System.Web.UI.WebControls.Literal MessageScript;
        protected System.Web.UI.WebControls.Literal FooterScript;
        protected System.Web.UI.WebControls.Literal HeaderScript;
        //protected System.Web.UI.WebControls.Literal OtherScript;




        NameValueCollection columnNameValue = null;

        string[] AllProcess = { "shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process" };
        string AllowedProcess = "";
       

        /******************* Entity Fields Decleration *****************/
        //protected SHMA.Enterprise.Presentation.WebControls.DatePopUp txtNP2_COMMENDATE;
        //protected System.Web.UI.WebControls.CompareValidator cfvNP2_COMMENDATE;







        protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PREMIUMTER2;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_PREMIUMTER;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_RETIREMENTAGE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_TOTALANNUALPREM;


        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_INTERESTRATE;


        bool NewRecord = false;
        bool DMLSucceeded = true;
        bool ILLUSTRATE_NOOR = false;
        string chl_level = "";
       
        /************ pk variables declaration ************/

        #region pk variables declaration
        private string NP1_PROPOSAL;
        private double NP2_SETNO;
        private string PPR_PRODCD;

        protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPOSAL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP2_SETNO;
        protected System.Web.UI.WebControls.CompareValidator cfvNPR_BASICFLAG;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP2_COMMENDATE;




        protected System.Web.UI.WebControls.RequiredFieldValidator msgCCB_CODE;

        protected System.Web.UI.WebControls.RequiredFieldValidator msgNP1_RETIREMENTAGE;
        protected System.Web.UI.WebControls.RequiredFieldValidator msgNP1_TOTALANNUALPREM;

        //protected System.Web.UI.WebControls.CompareValidator cfvNPR_TOTPREM;

        private string NP1_RETIREMENTAGE;
        protected System.Web.UI.WebControls.CompareValidator Comparevalidator3;
        private string NP1_TOTALANNUALPREM;
        DataTable dt = new DataTable();

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
        protected override void ValidateParams()
        {
            base.ValidateParams();
            string[] param;
            foreach (string key in Request.Params.AllKeys)
            {
                if (key != null && key.StartsWith("r_"))
                {
                    param = Request[key].Split(',');
                    SessionObject.Set(key.Replace("r_", ""), param[param.Length - 1]);
                }
            }
        }


        sealed protected override DataHolder GetInputData(DataHolder dataHolder)
        {

            GetSessionValues();
            //CheckKeyLevel();
            //recordCount = LNPR_PRODUCTDB.RecordCount;
            return dataHolder;

        }
        sealed protected override void BindInputData(DataHolder dataHolder)
        {

          //  ShowAgent();
            //TODO: SESSION SETTING in behavior ViewInitialState()
            //***********************CUSTOM CODE ***********************/
            ViewInitialState();
            //***********************CUSTOM CODE ***********************/
            IDataReader LPPR_PRODUCTReader0 = LPPR_PRODUCTDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO(); 
            ddlPPR_PRODCD.DataSource = LPPR_PRODUCTReader0;

            if (SessionObject.GetString("s_CCD_CODE") == "5" && SessionObject.GetString("s_CCH_CODE") == "2")
            {

                //				DataTable dtprod = new DataTable();
                //				DataRow dr=new DataRow();
                //				dr[0].ToString()="";
                //				dtprod.Rows.Add(dr);


                ddlPPR_PRODCD.DataValueField = "PPR_PRODCD";
                ddlPPR_PRODCD.DataTextField = "DESC_F";
            }
            ddlPPR_PRODCD.DataBind();
            LPPR_PRODUCTReader0.Close();

            IDataReader LCRL_RELATIONReader0 = LCRL_RELATIONDB.GetDDL_ILUS_BENEFECIARY_CRL_RELEATIOCD_RO(); ;
            ddlCRL_RELEATIOCD.DataSource = LCRL_RELATIONReader0;
            ddlCRL_RELEATIOCD.DataBind();
            LCRL_RELATIONReader0.Close();

            IDataReader PCU_CURRENCYReader1 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO(); ;
            ddlPCU_CURRCODE.DataSource = PCU_CURRENCYReader1;
            ddlPCU_CURRCODE.DataBind();
            PCU_CURRENCYReader1.Close();


            IDataReader LCSD_SYSTEMDTLReader55 = LCSD_SYSTEMDTLDB.GetCRF_FORFEITUCD(); ;
            ddlCFR_FORFEITUCD.DataSource = LCSD_SYSTEMDTLReader55;
            ddlCFR_FORFEITUCD.DataBind();
            LCSD_SYSTEMDTLReader55.Close();

            /*IDataReader CFR_FORFEITUCDReader1 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_RO();;
            ddlCFR_FORFEITUCD.DataSource = CFR_FORFEITUCDReader1 ;
            ddlCFR_FORFEITUCD.DataBind();
            CFR_FORFEITUCDReader1.Close();*/

            IDataReader PCU_CURRENCYReader2 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO(); ;
            ddlPCU_CURRCODE_PRM.DataSource = PCU_CURRENCYReader2;
            ddlPCU_CURRCODE_PRM.DataBind();
            PCU_CURRENCYReader2.Close();
            IDataReader LCMO_MODEReader3 = LCMO_MODEDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO(); ;
            ddlCMO_MODE.DataSource = LCMO_MODEReader3;
            ddlCMO_MODE.DataBind();
            LCMO_MODEReader3.Close();
            IDataReader LCSD_SYSTEMDTLReader4 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_INCLUDELOADINNIV_RO(); ;
            ddlNPR_INCLUDELOADINNIV.DataSource = LCSD_SYSTEMDTLReader4;
            ddlNPR_INCLUDELOADINNIV.DataBind();
            LCSD_SYSTEMDTLReader4.Close();
            //IDataReader LCSD_SYSTEMDTLReader5 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_NPR_COMMLOADING_RO();;
            IDataReader LCSD_SYSTEMDTLReader5 = LCSD_SYSTEMDTLDB.GetExtendedFlag_Lister_RO(); ;
            ddlNPR_COMMLOADING.DataSource = LCSD_SYSTEMDTLReader5;
            ddlNPR_COMMLOADING.DataBind();
            LCSD_SYSTEMDTLReader5.Close();
            IDataReader PCU_CURRENCYReader6 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_AVCURRCODE_RO(); ;
            ddlPCU_AVCURRCODE.DataSource = PCU_CURRENCYReader6;
            ddlPCU_AVCURRCODE.DataBind();
            PCU_CURRENCYReader6.Close();


            IDataReader CCB_CODEReader7 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CCB_CODE_RO(); ;
            ddlCCB_CODE.DataSource = CCB_CODEReader7;
            ddlCCB_CODE.DataBind();
            CCB_CODEReader7.Close();


            IDataReader LAAG_AGENTDBReader8 = LAAG_AGENTDB.GetDDL_ILUS_ET_NM_PROPOSAL_AAG_AGCODE_RO(); ;
            ddlBSCCode.DataSource = LAAG_AGENTDBReader8;
            ddlBSCCode.DataTextField = "AAG_AGCODE";
            ddlBSCCode.DataBind();
            LAAG_AGENTDBReader8.Close();

     
            _lastEvent.Text = "New";


            rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");
            if (LNP2_POLICYMASTR.next())
            {
                txtNP2_AGEPREM.Text = LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
                txtNP2_AGEPREM2ND.Text = LNP2_POLICYMASTR.getDouble("NP2_AGEPREM2ND").ToString();
            }
            else
            {
                txtNP2_AGEPREM.Text = "0";
                txtNP2_AGEPREM2ND.Text = "0";
            }


            FindAndSelectCurrentRecord();

            CSSLiteral.Text = ace.Ace_General.LoadPageStyle();

            HeaderScript.Text = EnvHelper.Parse("");
            FooterScript.Text = EnvHelper.Parse("var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\"); getField('NP1_PROPOSAL').value=v_NP1_PROPOSAL; var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData();");

            if (Convert.ToString(Session["FLAG_RESET_PREMIUM"]) == "Y" && DMLSucceeded == true)
            {
                HeaderScript.Text = HeaderScript.Text + "var reloadPage='Y'; ";
                Session["FLAG_RESET_PREMIUM"] = "";
            }
            else
            {
                HeaderScript.Text = HeaderScript.Text + "var reloadPage=''; ";
                Session["FLAG_RESET_PREMIUM"] = "";

            }

            ddlCCB_CODE.Attributes.Add("onchange", "setFaceValueField(this.value);");
            //txtNPR_SUMASSURED.Attributes.Add("onblur","applyNumberFormat(this,2);validate(this,'SUMASSURED');");
            //txtNPR_SUMASSURED.Attributes.Add("onfocus","validateInfo(this,'SUMASSURED');");

            txtNPR_SUMASSURED.Attributes.Add("onblur", "applyNumberFormat(this,2); if(getField('CCB_CODE').value=='S') validate(this,'SUMASSURED');");
            txtNPR_SUMASSURED.Attributes.Add("onfocus", "if(getField('CCB_CODE').value=='S') validateInfo(this,'SUMASSURED');");
            txtNPR_SUMASSURED.Attributes.Add("onchange", "SummAssured_OnChange(this);");

            txtNPR_TOTPREM.Attributes.Add("onblur", "applyNumberFormat(this,2);if(getField('CCB_CODE').value=='T') validate(this,'TOTPREM');");
            txtNPR_TOTPREM.Attributes.Add("onfocus", "if(getField('CCB_CODE').value=='T') validateInfo(this,'TOTPREM');");
            txtNPR_TOTPREM.Attributes.Add("onchange", "Premium_OnChange(this);");

            txtNPR_EXCESPRMANNUAL.Attributes.Add("onblur", "applyNumberFormat(this,2);");
            txtNPR_BENEFITTERM.Attributes.Add("onblur", "validateBenefitTerm(getField('PPR_PRODCD').value,getField('NPR_BENEFITTERM').value);setPremiumTerm(this);validate(this,'BTERM');setMaturityAge(this)");
            txtNPR_BENEFITTERM.Attributes.Add("onchange", "resetPremiumTerm(this);");
            txtNPR_BENEFITTERM.Attributes.Add("onfocus", "validateInfo(this,'BTERM');");

            txtNPR_PREMIUMTER.Attributes.Add("onblur", "validatePremiumTerm(this);");
            txtNPR_PREMIUMTER.Attributes.Add("onchange", "PremiumTerm_OnChange(this);");

            txtNPR_PAIDUPTOAGE.Attributes.Add("onblur", "setBenefitTerm(this);validate(this,'MATURITYAGE');");
            txtNPR_PAIDUPTOAGE.Attributes.Add("onfocus", "validateInfo(this,'MATURITYAGE');");
            txtNPR_PREMIUMDISCOUNT.Attributes.Add("onblur", "discountPremium(this);applyNumberFormat(this,2);");

            txtNomineeDOB.Attributes.Add("onblur", "setDOB(this, '" + SessionObject.Get("NP2_COMMENDATE") + "');");
            txtNP1_RETIREMENTAGE.Attributes.Add("onblur", "saveButtonFocus(this);");

            ddlPCU_CURRCODE_PRM.Attributes.Add("onchange", "Currency_OnChange(this);");
            ddlNPR_COMMLOADING.Attributes.Add("onchange", "DeathBenefit_OnChange(this);");

            ddlCMO_MODE.Attributes.Add("onchange", "disableViewPremiumUptoAge(this);");

            ddlPPR_PRODCD.Attributes.Add("onchange", "setViewForProduct(this);setOptions(this);filterCurrency(this);parent.frames[2].setFundSettingStatus();");

            ddlNPR_INDEXATION.Attributes.Add("onchange", "setRateField();");
            txtNPR_INDEXRATE.Attributes.Add("onblur", "validateIdexRate(getField('NPR_INDEXRATE').value);");
            txtNPR_INDEXRATE.Attributes.Add("onchange", "IndexRate_OnChange(this);");

            txtNPR_EDUNITS.Attributes.Add("onchange", "FAFactor_OnChange(this);");
            //txtNPR_INTERESTRATE.Attributes.Add("onchange","InterestRate_OnChange(this);");


            //RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            //***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("") ? "0" : AllowedProcess));
            if (SessionObject.GetString("s_USE_TYPE") == "S")
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
                if (ctrl != null)
                {
                    if (ctrl is WebControl)
                    {
                        //TextBox textBox = (TextBox)ctrl;
                        WebControl control = (WebControl)ctrl;
                        if ((control.Enabled == false) && (Request[control.UniqueID] != null))
                        {
                            control.Enabled = true;
                        }
                    }
                }
            }
        }

        //		public void Update_Term_For_Riders()
        //		{
        //			if(ddlPPR_PRODCD.SelectedValue=="020")
        //			{
        //				int benifitTermPlan=21;
        //				DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=" + benifitTermPlan + " WHERE NP1_PROPOSAL='" +txtNP1_PROPOSAL.Text+ "' AND NPR_BASICFLAG='Y'");
        //
        //				int age=Convert.ToInt16(lblAge.Text);
        //				int Benifit_Term;
        //				if(age>Convert.ToInt16(60))
        //				{
        //					Benifit_Term =age-Convert.ToInt16(60);
        //				}
        //				else
        //				{
        //					Benifit_Term =Convert.ToInt16(60)-age;
        //				}
        //				if(Benifit_Term<21)
        //				{
        //					//Update Querry
        //					DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM="+Benifit_Term+" WHERE NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' AND NPR_BASICFLAG='N'");
        //					Session.Add("RIDER_BENEFITTERM",Benifit_Term);
        //				}
        //				else
        //				{
        //					DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM="+benifitTermPlan+" WHERE NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' AND NPR_BASICFLAG='N'");
        //				}
        //			}
        //		}

        sealed protected override void ApplyDomainLogic(DataHolder dataHolder)

        {
            // Imran work Save Agent details
          //  SaveAgent();

            SessionObject.Set("PPR_PRODCD", Convert.ToString(ddlPPR_PRODCD.SelectedValue));

            SessionObject.Set("VALIDATION_ERROR", "");

            SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
            columnNameValue = new NameValueCollection();
            SaveTransaction = true;
            ace.ILUS_ET_NM_PLANDETAILS plandetails = new ace.ILUS_ET_NM_PLANDETAILS();

            shgn.SHGNCommand entityClass = new ace.ILUS_ET_NM_PLANDETAILS();
            entityClass.setNameValueCollection(columnNameValue);
            BeneficiaryData benData = new BeneficiaryData();

            benData.beneficiaryName = txt_NomineeName.Text;
            benData.beneficiaryRelation = ddlCRL_RELEATIOCD.SelectedValue;
            benData.beneficiaryAge = txtNBF_AGE.Text;
            benData.beneficiaryDOB = txtNomineeDOB.Text;
            benData.benefitTerm = txtNPR_BENEFITTERM.Text.Trim();

            NameValueCollection columnNameValueNonBase = new NameValueCollection();

            columnNameValueNonBase.Add("PCU_CURRCODE_PRM", ddlPCU_CURRCODE_PRM.SelectedValue.Trim() == "" ? null : ddlPCU_CURRCODE_PRM.SelectedValue);
            columnNameValueNonBase.Add("CMO_MODE", ddlCMO_MODE.SelectedValue.Trim() == "" ? null : ddlCMO_MODE.SelectedValue);
            columnNameValueNonBase.Add("PCU_AVCURRCODE", ddlPCU_AVCURRCODE.SelectedValue.Trim() == "" ? null : ddlPCU_AVCURRCODE.SelectedValue);
            columnNameValueNonBase.Add("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue.Trim() == "" ? null : ddlPPR_PRODCD.SelectedValue);
            columnNameValueNonBase.Add("CCB_CODE", ddlCCB_CODE.SelectedValue.Trim() == "" ? null : ddlCCB_CODE.SelectedValue.Trim());
            columnNameValueNonBase.Add("NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_SUMASSURED.Text));
            columnNameValueNonBase.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_TOTPREM.Text));
            columnNameValueNonBase.Add("NPR_PAIDUPTOAGE", txtNPR_PAIDUPTOAGE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
            columnNameValueNonBase.Add("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_BENEFITTERM.Text));

            columnNameValueNonBase.Add("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue.Trim() == "" ? null : ddlNPR_INDEXATION.SelectedValue);
            columnNameValueNonBase.Add("NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? (object)double.Parse("0") : (object)double.Parse(txtNPR_INDEXRATE.Text));
            columnNameValueNonBase.Add("NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EDUNITS.Text));
            columnNameValueNonBase.Add("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_INTERESTRATE.Text));

            columnNameValueNonBase.Add("CFR_FORFEITUCD", ddlCFR_FORFEITUCD.SelectedValue.Trim() == "" ? null : ddlCFR_FORFEITUCD.SelectedValue);


            //TODO: Insert value from Session
            //?????
            txtNP1_PROPOSAL.Text = "" + SessionObject.Get("NP1_PROPOSAL");
            txtNP2_SETNO.Text = "1";

            entityClass.setNameValueCollection(columnNameValueNonBase);

            //Custom : Change
            int retirementAge = Convert.ToInt32(lblAge.Text == "" ? "0" : lblAge.Text) + Convert.ToInt32(txtNPR_BENEFITTERM.Text == "" ? "0" : txtNPR_BENEFITTERM.Text);
            if (Convert.ToInt32(txtNP1_RETIREMENTAGE.Text) > retirementAge)
            {
                retirementAge = Convert.ToInt32(txtNP1_RETIREMENTAGE.Text);
            }
            txtNP1_RETIREMENTAGE.Text = retirementAge.ToString();
            SessionObject.Set("NP1_RETIREMENTAGE", retirementAge);
            SessionObject.Set("NP1_TOTALANNUALPREM", txtNP1_TOTALANNUALPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNP1_TOTALANNUALPREM.Text));
            //Custom : Change


            if (_lastEvent.Text == "New") NewRecord = true;
            DMLSucceeded = false;
            string DDLValue="";
            string CHL_level = "";
            int AgentCode=0;
            DDLValue = ddlBSCCode.SelectedValue;
            
            if (DDLValue != "")
            {
                string[] arr = Convert.ToString(ddlBSCCode.SelectedValue).Split('-');
                //CHL_level = ddlBSCCode.SelectedValue.Substring(ddlBSCCode.SelectedValue.Length - 3);
                //AgentCode = Int32.Parse(ddlBSCCode.SelectedValue.Substring(0, 7));
                CHL_level = arr[4].Trim();
                AgentCode = Convert.ToInt32(arr[0]);
              
            }
            SHSM_SecurityPermission security;
            switch ((EnumControlArgs)ControlArgs[0])
            {
                case (EnumControlArgs.Save):
                    _lastEvent.Text = "Save";
                    DB.BeginTransaction();
                    SaveTransaction = true;

                    //TODO: Insert value from Session
                    txtNP1_PROPOSAL.Text = "" + SessionObject.Get("NP1_PROPOSAL");
                    txtNP2_SETNO.Text = "1";


                    dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text, double.Parse(txtNP2_SETNO.Text), ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue.Trim() == "" ? null : ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue.Trim() == "" ? null : ddlPCU_CURRCODE.SelectedValue);

                    //////columnNameValue.Add("CFR_FORFEITUCD",ddlCFR_FORFEITUCD.SelectedValue.Trim()==""?null:ddlCFR_FORFEITUCD.SelectedValue);//Added CFR_FORFEITUCD

                    columnNameValue.Add("CCB_CODE", ddlCCB_CODE.SelectedValue.Trim() == "" ? null : ddlCCB_CODE.SelectedValue.Trim());
                    columnNameValue.Add("CMO_MODE", ddlCMO_MODE.SelectedValue.Trim() == "" ? null : ddlCMO_MODE.SelectedValue.Trim());
                    columnNameValue.Add("NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_SUMASSURED.Text));
                    columnNameValue.Add("NPR_PAIDUPTOAGE", txtNPR_PAIDUPTOAGE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
                    columnNameValue.Add("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_BENEFITTERM.Text));
                    columnNameValue.Add("NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMTER.Text));
                    columnNameValue.Add("NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
                    ////columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
                    columnNameValue.Add("NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim() == "" ? null : ddlNPR_INCLUDELOADINNIV.SelectedValue);
                    columnNameValue.Add("NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
                    columnNameValue.Add("NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue.Trim() == "" ? null : ddlNPR_COMMLOADING.SelectedValue);
                    columnNameValue.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_TOTPREM.Text));
                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));
                    columnNameValue.Add("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue.Trim() == "" ? null : ddlNPR_INDEXATION.SelectedValue);
                    columnNameValue.Add("NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? (object)double.Parse("0") : (object)double.Parse(txtNPR_INDEXRATE.Text));
                    columnNameValue.Add("NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EDUNITS.Text));
                    columnNameValue.Add("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_INTERESTRATE.Text));
                    columnNameValue.Add("NPR_BASICFLAG", "Y");

                 
                   
                    if (DDLValue != "")
                    {
                        var date = DateTime.Now;
                        date = new DateTime(date.Year, date.Month, date.Day);
                        DateTime datewithMonth = Convert.ToDateTime(date);
                        SHMA.Enterprise.Data.ParameterCollection PM_LNCM = new SHMA.Enterprise.Data.ParameterCollection();
                        PM_LNCM.puts("@NP1_PROPOSAL", SessionObject.Get("NP1_PROPOSAL"));
                        PM_LNCM.puts("@NP2_SETNO", 1);
                        PM_LNCM.puts("@CYR_YEARCODE", "01S");
                        PM_LNCM.puts("@USE_USERID", SessionObject.Get("s_USE_USERID"));
                        PM_LNCM.puts("@USE_DATETIME", datewithMonth);
                        PM_LNCM.puts("@NCM_VALUE", 1);
                        PM_LNCM.puts("@NP2_APPROVED", "Y");
                        PM_LNCM.puts("@CONVERT", "");

                        DB.executeDML("INSERT INTO LNCM_COMMISSION(NP1_PROPOSAL,NP2_SETNO,CYR_YEARCODE,USE_USERID,USE_DATETIME,NCM_VALUE,NP2_APPROVED,CONVERT ) values (?,?,?,?,?,?,?,?)", PM_LNCM);

                        SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
                        pm.puts("@NP1_PROPOSAL", SessionObject.Get("NP1_PROPOSAL"));
                        pm.puts("@NP2_SETNO", 1);
                        pm.puts("@CYR_YEARCODE", "01S");
                        pm.puts("@CHL_LEVEL", CHL_level);
                        pm.puts("@AAG_AGCODE", AgentCode);
                        pm.puts("@NOE_OVRATE", 1);
                        pm.puts("@USE_USERID", SessionObject.Get("s_USE_USERID"));
                        pm.puts("@USE_DATETIME", datewithMonth);
                        pm.puts("@NOE_OVDIST", 1);
                        DB.executeDML("INSERT INTO LNOE_OVENTITLEMENT(NP1_PROPOSAL,NP2_SETNO,CYR_YEARCODE,CHL_LEVEL,AAG_AGCODE,NOE_OVRATE,USE_USERID,USE_DATETIME,NOE_OVDIST ) values (?,?,?,?,?,?,?,?,?)", pm);

                    }
                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");

                    if (security.SaveAllowed)
                    {
                        entityClass.fsoperationBeforeSave();

                        new LNPR_PRODUCT(dataHolder).Add(columnNameValue, getAllFields(), "ILUS_ET_NM_PLANDETAILS", null);
                        dataHolder.Update(DB.Transaction);

                        //DB.executeDML("UPDATE LNPR_PRODUCT SET CMO_MODE='" + ddlCMO_MODE.SelectedValue + "' WHERE NP1_PROPOSAL='" + txtNP1_PROPOSAL.Text + "' AND NP2_SETNO=" + txtNP2_SETNO.Text + " AND PPR_PRODCD='" + ddlPPR_PRODCD.SelectedValue + "'");
                        try
                        {
                            entityClass.fsoperationAfterSave();

                            if (Nomineevalidation(ddlPPR_PRODCD.SelectedValue))
                            {
                                plandetails.SaveBeneficiary(benData);
                            }
                        }
                        catch (FieldValidationException ex)
                        {
                            Session["FLAG_RESET_PREMIUM"] = "";
                            SessionObject.Set("VALIDATION_ERROR", ex.Message);
                            //ValidationError.Text = "openValidationError();";
                            //Response.Write("<script>alert('Premium and Benefit term should eqaul.');</script>");
                            throw new ProcessException(ex.Message.Replace("\r", " ").Replace("\t", " ").Replace("\"", "").Replace("<BR>", "").Replace("<br>", "").Replace("<FONT style=COLOR: #336699 size=2 >", "").Replace("<B>", "").Replace("<U>", "").Replace("</B>", "").Replace("</U>", "").Replace("</FONT>", ""));
                            
                        }

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPR_PRODUCT");
                        _lastEvent.Text = "Save";

                        NewRecord = false;
                        DMLSucceeded = true;

                        //PrintMessage("Record has been saved");
                        //Update_Term_For_Riders();
                    }
                    else
                    {
                        PrintMessage("You are not autherized to Save.");
                    }
                    break;
                case (EnumControlArgs.Update):
                    DB.BeginTransaction();
                    SaveTransaction = true;
                    dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text, double.Parse(txtNP2_SETNO.Text), ddlPPR_PRODCD.SelectedValue);

                    //int age=Convert.ToInt32(lblAge.Text)+Convert.ToInt32(txtNPR_BENEFITTERM.Text);
                    columnNameValue.Add("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue.Trim() == "" ? null : ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue.Trim() == "" ? null : ddlPCU_CURRCODE.SelectedValue);

                    ////columnNameValue.Add("CFR_FORFEITUCD",ddlCFR_FORFEITUCD.SelectedValue.Trim()==""?null:ddlCFR_FORFEITUCD.SelectedValue);//Added CFR_FORFEITUCD

                    columnNameValue.Add("CCB_CODE", ddlCCB_CODE.SelectedValue.Trim() == "" ? null : ddlCCB_CODE.SelectedValue.Trim());
                    columnNameValue.Add("CMO_MODE", ddlCMO_MODE.SelectedValue.Trim() == "" ? null : ddlCMO_MODE.SelectedValue.Trim());
                    columnNameValue.Add("NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_SUMASSURED.Text));

                    //columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));

                    columnNameValue.Add("NPR_PAIDUPTOAGE", retirementAge.ToString() == "" ? null : (object)(retirementAge.ToString()));

                    columnNameValue.Add("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_BENEFITTERM.Text));
                    columnNameValue.Add("NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMTER.Text));
                    //columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
                    columnNameValue.Add("NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
                    columnNameValue.Add("NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim() == "" ? null : ddlNPR_INCLUDELOADINNIV.SelectedValue);
                    columnNameValue.Add("NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
                    columnNameValue.Add("NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue.Trim() == "" ? null : ddlNPR_COMMLOADING.SelectedValue);
                    columnNameValue.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_TOTPREM.Text));
                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));
                    columnNameValue.Add("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue.Trim() == "" ? null : ddlNPR_INDEXATION.SelectedValue);
                    columnNameValue.Add("NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? (object)double.Parse("0") : (object)double.Parse(txtNPR_INDEXRATE.Text));
                    columnNameValue.Add("NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EDUNITS.Text));
                    columnNameValue.Add("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_INTERESTRATE.Text));
                    columnNameValue.Add("NPR_BASICFLAG", "Y");


                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
                    if (security.UpdateAllowed)
                    {
                        rowset rsProduct = DB.executeQuery("select PPR_PRODCD from LNPR_PRODUCT where NP1_PROPOSAL='" + txtNP1_PROPOSAL.Text + "' and NPR_BASICFLAG='Y'");
                        rsProduct.next();
                        string proposal = txtNP1_PROPOSAL.Text;
                        //Check if Product been changed
                        if (!ddlPPR_PRODCD.SelectedValue.Trim().Equals(rsProduct.getString("PPR_PRODCD")))
                        {

                            string newProduct = ddlPPR_PRODCD.SelectedValue;
                            double setNo = txtNP2_SETNO.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNP2_SETNO.Text);

                            //**********************************************************************************// 
                            //************* Delete old Product(PLAN) and its related data **********************//
                            //**********************************************************************************//
                            DB.executeDML("delete from LNQD_QUESTIONDETAIL where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNQN_QUESTIONNAIRE  where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNLO_LOADING        where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNLO_LOADING_ACTUAL where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNFU_FUNDS          where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNPR_PRODUCT        where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from lnbf_beneficiary    where NP1_PROPOSAL='" + proposal + "'");

                            //DB.executeDML("delete from LNPR_PRODUCT        where NP1_PROPOSAL='" + proposal + "' and (PPR_PRODCD='" + rsProduct.getString("PPR_PRODCD")+ "')");

                            //**********************************************************************************//
                            //************************** Insert New Product(PLAN) ******************************//
                            //**********************************************************************************//
                            string insertNewPlan = "INSERT INTO LNPR_PRODUCT (NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,NPR_BASICFLAG,NPR_BENEFITTERM,CCB_CODE,NPR_SUMASSURED,NPR_TOTPREM,NPR_PREMIUM,CMO_MODE,PCU_CURRCODE,NPR_PREMIUMTER,NPR_INDEXATION,NPR_INDEXRATE,NPR_EDUNITS,NPR_PAIDUPTOAGE,NPR_PREMIUMDISCOUNT,NPR_INCLUDELOADINNIV,NPR_EXCESSPREMIUM,NPR_COMMLOADING,NPR_INTERESTRATE) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                            pc.puts("@NP1_PROPOSAL", proposal);
                            pc.puts("@NP2_SETNO", setNo);
                            pc.puts("@PPR_PRODCD", newProduct);
                            pc.puts("@NPR_BASICFLAG", "Y");
                            pc.puts("@NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_BENEFITTERM.Text));
                            pc.puts("@CCB_CODE", ddlCCB_CODE.SelectedValue.Trim());
                            pc.puts("@NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_SUMASSURED.Text));
                            pc.puts("@NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_TOTPREM.Text));
                            pc.puts("@NPR_PREMIUM", txtNPR_PREMIUM.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_PREMIUM.Text));
                            pc.puts("@CMO_MODE", ddlCMO_MODE.SelectedValue.Trim());
                            pc.puts("@PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue);
                            //	pc.puts("@CFR_FORFEITUCD",		ddlCFR_FORFEITUCD.SelectedValue);//Added CFR_FORFEITUCD
                            pc.puts("@NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_PREMIUMTER.Text));
                            pc.puts("@NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue);
                            pc.puts("@NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_INDEXRATE.Text));
                            pc.puts("@NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_EDUNITS.Text));
                            pc.puts("@NPR_PAIDUPTOAGE", retirementAge);
                            pc.puts("@NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
                            pc.puts("@NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue);
                            pc.puts("@NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_EXCESPRMANNUAL.Text));
                            pc.puts("@NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue);
                            pc.puts("@NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? double.Parse("0") : double.Parse(txtNPR_INTERESTRATE.Text));
                            DB.executeDML(insertNewPlan, pc);

                        }
                        else
                        {
                            entityClass.fsoperationBeforeUpdate();
                            new LNPR_PRODUCT(dataHolder).Update(Utilities.File2EntityID(this.ToString()), columnNameValue);
                            dataHolder.Update(DB.Transaction);
                        }

                        try
                        {
                            DB.executeDML("delete from LNOE_OVENTITLEMENT  where NP1_PROPOSAL='" + proposal + "'");
                            DB.executeDML("delete from LNCM_COMMISSION     where NP1_PROPOSAL='" + proposal + "'");


                         
                            if (DDLValue != "")
                            {

                                var date = DateTime.Now;
                                date = new DateTime(date.Year, date.Month, date.Day);
                                DateTime datewithMonth = Convert.ToDateTime(date);
                                SHMA.Enterprise.Data.ParameterCollection PM_LNCM = new SHMA.Enterprise.Data.ParameterCollection();
                                PM_LNCM.puts("@NP1_PROPOSAL", SessionObject.Get("NP1_PROPOSAL"));
                                PM_LNCM.puts("@NP2_SETNO", 1);
                                PM_LNCM.puts("@CYR_YEARCODE", "01S");
                                PM_LNCM.puts("@USE_USERID", SessionObject.Get("s_USE_USERID"));
                                PM_LNCM.puts("@USE_DATETIME", datewithMonth);
                                PM_LNCM.puts("@NCM_VALUE", 1);
                                PM_LNCM.puts("@NP2_APPROVED", "Y");
                                PM_LNCM.puts("@CONVERT", "");

                                DB.executeDML("INSERT INTO LNCM_COMMISSION(NP1_PROPOSAL,NP2_SETNO,CYR_YEARCODE,USE_USERID,USE_DATETIME,NCM_VALUE,NP2_APPROVED,CONVERT ) values (?,?,?,?,?,?,?,?)", PM_LNCM);

                                SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
                                pm.puts("@NP1_PROPOSAL", SessionObject.Get("NP1_PROPOSAL"));
                                pm.puts("@NP2_SETNO", 1);
                                pm.puts("@CYR_YEARCODE", "01S");
                                pm.puts("@CHL_LEVEL", CHL_level);
                                pm.puts("@AAG_AGCODE", AgentCode);
                                pm.puts("@NOE_OVRATE", 1);
                                pm.puts("@USE_USERID", SessionObject.Get("s_USE_USERID"));
                                pm.puts("@USE_DATETIME", datewithMonth);
                                pm.puts("@NOE_OVDIST", 1);
                                DB.executeDML("INSERT INTO LNOE_OVENTITLEMENT(NP1_PROPOSAL,NP2_SETNO,CYR_YEARCODE,CHL_LEVEL,AAG_AGCODE,NOE_OVRATE,USE_USERID,USE_DATETIME,NOE_OVDIST ) values (?,?,?,?,?,?,?,?,?)", pm);

                            }

                            entityClass.fsoperationAfterUpdate();
                            if (Nomineevalidation(ddlPPR_PRODCD.SelectedValue))
                            {
                                plandetails.UpdateBeneficiary(benData);
                            }

                        }
                        catch (FieldValidationException ex)
                        {
                            SessionObject.Set("VALIDATION_ERROR", ex.Message);
                            //ValidationError.Text = "openValidationError();";
                            //Response.Write("<script>alert('Premium and Benefit term should eqaul.');</script>");
                            throw new ProcessException(ex.Message.Replace("\r", " ").Replace("\t", " ").Replace("\"", "").Replace("<BR>", "").Replace("<br>", "").Replace("<FONT style=COLOR: #336699 size=2 >", "").Replace("<B>", "").Replace("<U>", "").Replace("</B>", "").Replace("</U>", "").Replace("</FONT>", ""));
                        }

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPR_PRODUCT");
                        //recordSelected = true;
                        //PrintMessage("Record has been updated");
                        //Update_Term_For_Riders();
                        NewRecord = false;
                        DMLSucceeded = true;

                        string anyMessageToInform = ((ace.ILUS_ET_NM_PLANDETAILS)entityClass).strRidersUpdateInformation.Trim();
                        if (anyMessageToInform.Length > 0)
                        {
                            //PrintMessage("Record has been updated.\\n====\\nNOTE:\\n====\\n" + anyMessageToInform);
                            PrintMessage(anyMessageToInform);
                        }
                    }
                    else
                    {
                        PrintMessage("You are not autherized to Update.");
                    }
                    break;
                case (EnumControlArgs.Delete):
                    DB.BeginTransaction();
                    SaveTransaction = true;
                    dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text, double.Parse(txtNP2_SETNO.Text), ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue.Trim() == "" ? null : ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue.Trim() == "" ? null : ddlPCU_CURRCODE.SelectedValue);

                    ////columnNameValue.Add("CFR_FORFEITUCD",ddlCFR_FORFEITUCD.SelectedValue.Trim()==""?null:ddlCFR_FORFEITUCD.SelectedValue);//Added CFR_FORFEITUCD

                    columnNameValue.Add("CCB_CODE", ddlCCB_CODE.SelectedValue.Trim() == "" ? null : ddlCCB_CODE.SelectedValue.Trim());
                    columnNameValue.Add("CMO_MODE", ddlCMO_MODE.SelectedValue.Trim() == "" ? null : ddlCMO_MODE.SelectedValue.Trim());
                    columnNameValue.Add("NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_SUMASSURED.Text));
                    columnNameValue.Add("NPR_PAIDUPTOAGE", txtNPR_PAIDUPTOAGE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
                    columnNameValue.Add("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_BENEFITTERM.Text));
                    columnNameValue.Add("NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMTER.Text));
                    //columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
                    columnNameValue.Add("NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
                    columnNameValue.Add("NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim() == "" ? null : ddlNPR_INCLUDELOADINNIV.SelectedValue);
                    columnNameValue.Add("NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
                    columnNameValue.Add("NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue.Trim() == "" ? null : ddlNPR_COMMLOADING.SelectedValue);
                    columnNameValue.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_TOTPREM.Text));
                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));
                    columnNameValue.Add("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue.Trim() == "" ? null : ddlNPR_INDEXATION.SelectedValue);
                    columnNameValue.Add("NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? (object)double.Parse("0") : (object)double.Parse(txtNPR_INDEXRATE.Text));
                    columnNameValue.Add("NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EDUNITS.Text));
                    columnNameValue.Add("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_INTERESTRATE.Text));
                    columnNameValue.Add("NPR_BASICFLAG", txtNPR_BASICFLAG.Text.Trim() == "" ? null : txtNPR_BASICFLAG.Text);


                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
                    if (security.DeleteAllowed)
                    {
                        entityClass.fsoperationBeforeDelete();
                        new LNPR_PRODUCT(dataHolder).Delete(columnNameValue);

                        dataHolder.Update(DB.Transaction);
                        entityClass.fsoperationAfterDelete();
                        plandetails.DeleteBeneficiary();

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPR_PRODUCT");
                        //PrintMessage("Record has been deleted");				
                        NewRecord = false;
                        DMLSucceeded = true;
                    }
                    else
                    {
                        PrintMessage("You are not autherized to Delete.");
                    }

                    break;
                case (EnumControlArgs.Process):
                    //if (ddlPPR_PRODCD.SelectedValue.Trim()=="075")
                    //{
                    //    string beneficiaryCheck = validateNominee();
                    //    if (int.Parse(beneficiaryCheck) <= 0)
                    //    {
                    //        Response.Write("<script>alert('Please Enter Nominee/Beneficiary First.');</script>");
                    //        return;
                    //    }
                    //}
                    DB.BeginTransaction();
                    SaveTransaction = true;
                    dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text, double.Parse(txtNP2_SETNO.Text), ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue.Trim() == "" ? null : ddlPPR_PRODCD.SelectedValue);
                    columnNameValue.Add("PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue.Trim() == "" ? null : ddlPCU_CURRCODE.SelectedValue);

                    ////columnNameValue.Add("CFR_FORFEITUCD",ddlCFR_FORFEITUCD.SelectedValue.Trim()==""?null:ddlCFR_FORFEITUCD.SelectedValue);

                    columnNameValue.Add("CCB_CODE", ddlCCB_CODE.SelectedValue.Trim() == "" ? null : ddlCCB_CODE.SelectedValue.Trim());
                    columnNameValue.Add("CMO_MODE", ddlCMO_MODE.SelectedValue.Trim() == "" ? null : ddlCMO_MODE.SelectedValue.Trim());
                    columnNameValue.Add("NPR_SUMASSURED", txtNPR_SUMASSURED.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_SUMASSURED.Text));
                    columnNameValue.Add("NPR_PAIDUPTOAGE", txtNPR_PAIDUPTOAGE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
                    columnNameValue.Add("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_BENEFITTERM.Text));
                    columnNameValue.Add("NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMTER.Text));
                    //columnNameValue.Add("NPR_PREMIUM",txtNPR_PREMIUM.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUM.Text));
                    columnNameValue.Add("NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim() == "" ? null : ddlNPR_INCLUDELOADINNIV.SelectedValue);
                    columnNameValue.Add("NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
                    columnNameValue.Add("NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
                    columnNameValue.Add("NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue.Trim() == "" ? null : ddlNPR_COMMLOADING.SelectedValue);
                    columnNameValue.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_TOTPREM.Text));
                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));
                    columnNameValue.Add("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue.Trim() == "" ? null : ddlNPR_INDEXATION.SelectedValue);
                    columnNameValue.Add("NPR_INDEXRATE", txtNPR_INDEXRATE.Text.Trim() == "" ? (object)double.Parse("0") : (object)double.Parse(txtNPR_INDEXRATE.Text));
                    columnNameValue.Add("NPR_EDUNITS", txtNPR_EDUNITS.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_EDUNITS.Text));
                    columnNameValue.Add("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text.Trim() == "" ? null : (object)double.Parse(txtNPR_INTERESTRATE.Text));
                    columnNameValue.Add("NPR_BASICFLAG", txtNPR_BASICFLAG.Text.Trim() == "" ? null : txtNPR_BASICFLAG.Text);

                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
                    string result = "";
                    if (_CustomArgName.Value == "ProcessName")
                    {
                        string processName = _CustomArgVal.Value;
                        if (processName == "RecalculatePremFromPersonalPage")
                        {
                            BA.BAUtility.RecalculatePemium(Convert.ToString(SessionObject.Get("NP1_PROPOSAL")), int.Parse(txtNP1_RETIREMENTAGE.Text));
                        }
                        else
                        {
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
                                    //Update_Term_For_Riders();
                                    NewRecord = false;
                                    DMLSucceeded = true;

                                    if (ace.clsIlasUtility.isNoorIllustrion() && processName == "ace.Calculate_Premium")
                                    {
                                        ILLUSTRATE_NOOR = true;
                                    }

                                }
                            }
                            else
                            {
                                result = "You are not Autherized to Execute Process.";
                            }
                        }
                    }


                    //recordSelected =true;
                    if (result.Length > 0)
                        PrintMessage(result);
                    break;

            }
        }
        public bool Nomineevalidation(string ppr_prodcd)
        {
            try
            {
                rowset rsPlan = DB.executeQuery("SELECT count(*) as ValidationEntry FROM LPVL_VALIDATION WHERE PPR_PRODCD='"+ ppr_prodcd + "' and pvl_validationfor='NOMINEE'");
                if (rsPlan.next())
                {
                    if (Convert.ToInt32(rsPlan.getObject(1).ToString())==1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new FieldValidationException(e.Message);
            }
        }

        private void executeNoorIllustrate()
        {
            try
            {
                ace.IllustrateNoor obj = new ace.IllustrateNoor();
                obj.callNoorIllustration(txtNP1_PROPOSAL.Text.Trim());

                //Update LNPR_PRODUCT NPR_ETASA
                NoorIllustrate.clsIlasUtility.updateLnloLoading(txtNP1_PROPOSAL.Text.Trim());

                //Generate Medical Requirement
                NoorIllustrate.clsSevice.generateMedicalReq(txtNP1_PROPOSAL.Text.Trim(), ddlPPR_PRODCD.SelectedValue);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {

            }
        }


        sealed protected override void DataBind(DataHolder dataHolder)
        {
            try
            {

                LNPR_PRODUCTDB LNPR_PRODUCTDB_obj = new LNPR_PRODUCTDB(dataHolder);
                if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
                {
                    DataRow row = LNPR_PRODUCTDB_obj.FindByPK(NP1_PROPOSAL, NP2_SETNO, PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
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
                    else
                    {
                        if (((EnumControlArgs)ControlArgs[0]).ToString() == "Update")
                            FindAndSelectCurrentRecord();
                    }
                }
                /* a temporary work arround for errors in save replace it later with proper error flow */
                if (_lastEvent.Text == EnumControlArgs.View.ToString())
                {
                    SHSM_SecurityPermission security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
                    if (!security.UpdateAllowed)
                        _lastEvent.Text = EnumControlArgs.View.ToString();
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
                RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("") ? "0" : AllowedProcess));

                CSSLiteral.Text = ace.Ace_General.LoadPageStyle();

                if (ILLUSTRATE_NOOR)
                    this.executeNoorIllustrate();
                //if(Convert.ToString(Session["ILLUSTRATE_NOOR"]) == "Y")
                //	OtherScript.Text = "var IllustrateNoor = 'Y';";
                //else
                //	OtherScript.Text = "var IllustrateNoor = '';";

                HeaderScript.Text = EnvHelper.Parse("");
                FooterScript.Text = EnvHelper.Parse("var v_NP1_PROPOSAL=SV(\"NP1_PROPOSAL\"); getField('NP1_PROPOSAL').value=v_NP1_PROPOSAL;var v_NP2_SETNO=SV(\"NP2_SETNO\");setFetchDataQry(\"SELECT SUM(NVL(NPR_PREMIUM,0)) NP1_TOTALRIDERPREM FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL ='\"+v_NP1_PROPOSAL+\"' AND NP2_SETNO = \"+v_NP2_SETNO+\" AND NVL(NPR_BASICFLAG,'Y') = 'N'\"); fetchData(); ");

                if (Convert.ToString(Session["FLAG_RESET_PREMIUM"]) == "Y" && DMLSucceeded == true)
                {
                    HeaderScript.Text = HeaderScript.Text + "var reloadPage='Y';";
                    Session["FLAG_RESET_PREMIUM"] = "";
                }
                else
                {
                    HeaderScript.Text = HeaderScript.Text + "var reloadPage='';";
                    Session["FLAG_RESET_PREMIUM"] = "";

                }

            }

            catch (Exception ex)
            {
                if (!ex.Message.Contains("doesn't have a primary key.") || !ex.Message.Contains("Table doesnt have a primary key."))
                    throw; // rethrow if it’s a real error
            }
        }
        #endregion

        #region Events
        protected void _CustomEvent_ServerClick(object sender, System.EventArgs e)
        {
            _lastEventProcess.Text = "";
            ControlArgs = new object[1];
            switch (_CustomEventVal.Value)
            {
                case "Update":
                    ControlArgs[0] = EnumControlArgs.Update;
                    CustomDoControl();
                    break;
                case "Save":
                    ControlArgs[0] = EnumControlArgs.Save;
                    CustomDoControl();
                    break;
                case "Delete":
                    ControlArgs[0] = EnumControlArgs.Delete;
                    CustomDoControl();
                    break;
                case "Filter":
                    ControlArgs[0] = EnumControlArgs.Filter;
                    CustomDoControl();
                    break;
                case "Process":
                    _lastEventProcess.Text = "Process";
                    ControlArgs[0] = EnumControlArgs.Process;
                    CustomDoControl();
                    break;

            }
            _CustomEventVal.Value = "";
        }
        protected void Page_Unload(object sender, System.EventArgs e)
        {
            //base.OnUnload(e);
            if (SetFieldsInSession())
            {
                //if(_lastEvent == "New" && val
                if (_lastEvent.Text == "New" && NewRecord == true && DMLSucceeded == false)
                {
                    //SessionObject.Set("NP1_PROPOSAL","");
                    SessionObject.Set("PPR_PRODCD", "");
                    SessionObject.Set("NP2_SETNO", "");
                    SessionObject.Set("NP2_AGEPREM", "");
                    SessionObject.Set("NP2_AGEPREM2ND", "");
                    SessionObject.Set("PCU_CURRCODE", "");
                    SessionObject.Set("CCB_CODE", "");
                    SessionObject.Set("NPR_SUMASSURED", "");
                    SessionObject.Set("PCU_CURRCODE_PRM", "");
                    SessionObject.Set("CMO_MODE", "");
                    SessionObject.Set("NPR_PAIDUPTOAGE", "");
                    SessionObject.Set("NPR_BENEFITTERM", "");
                    SessionObject.Set("NPR_PREMIUMTER", "");
                    SessionObject.Set("NPR_PREMIUM", "");
                    SessionObject.Set("NPR_PREMIUMDISCOUNT", "");
                    SessionObject.Set("NPR_INCLUDELOADINNIV", "");
                    SessionObject.Set("NPR_EXCESSPREMIUM", "");
                    SessionObject.Set("NPR_COMMLOADING", "");
                    //SessionObject.Set("NP1_PERIODICPREM","");
                    SessionObject.Set("PCU_AVCURRCODE", "");
                    //SessionObject.Set("NP1_TOTALRIDERPREM","");
                    SessionObject.Set("NPR_TOTPREM", "");
                    SessionObject.Set("NPR_BASICFLAG", "");
                    //SessionObject.Set("NP1_RETIREMENTAGE",txtNP1_RETIREMENTAGE.Text);
                    //SessionObject.Set("NP1_TOTALANNUALPREM",txtNP1_TOTALANNUALPREM.Text);
                    SessionObject.Set("NPR_INDEXATION", "");
                    SessionObject.Set("NPR_INDEXRATE", "");
                    SessionObject.Set("NPR_EDUNITS", "");
                    SessionObject.Set("NPR_INTERESTRATE", "");

                }
                else
                {
                    //SessionObject.Set("NP2_COMMENDATE",txtNP2_COMMENDATE.Text);
                    //SessionObject.Set("NP2_COMMENDATE",SessionObject.Get("NP2_COMMENDATE"));
                    SessionObject.Set("NP2_AGEPREM", txtNP2_AGEPREM.Text);
                    SessionObject.Set("NP2_AGEPREM2ND", txtNP2_AGEPREM2ND.Text);
                    SessionObject.Set("PPR_PRODCD", ddlPPR_PRODCD.SelectedValue);
                    SessionObject.Set("PCU_CURRCODE", ddlPCU_CURRCODE.SelectedValue);
                    SessionObject.Set("CCB_CODE", ddlCCB_CODE.SelectedValue);
                    SessionObject.Set("NPR_SUMASSURED", txtNPR_SUMASSURED.Text);
                    SessionObject.Set("PCU_CURRCODE_PRM", ddlPCU_CURRCODE_PRM.SelectedValue);
                    SessionObject.Set("CMO_MODE", ddlCMO_MODE.SelectedValue);
                    SessionObject.Set("NPR_PAIDUPTOAGE", txtNPR_PAIDUPTOAGE.Text);
                    SessionObject.Set("NPR_BENEFITTERM", txtNPR_BENEFITTERM.Text);
                    SessionObject.Set("NPR_PREMIUMTER", txtNPR_PREMIUMTER.Text);
                    SessionObject.Set("NPR_PREMIUM", txtNPR_PREMIUM.Text);
                    SessionObject.Set("NPR_PREMIUMDISCOUNT", txtNPR_PREMIUMDISCOUNT.Text);
                    SessionObject.Set("NPR_INCLUDELOADINNIV", ddlNPR_INCLUDELOADINNIV.SelectedValue);
                    SessionObject.Set("NPR_EXCESSPREMIUM", txtNPR_EXCESPRMANNUAL.Text);
                    SessionObject.Set("NPR_COMMLOADING", ddlNPR_COMMLOADING.SelectedValue);
                    SessionObject.Set("NP1_PERIODICPREM", txtNP1_PERIODICPREM.Text);
                    SessionObject.Set("PCU_AVCURRCODE", ddlPCU_AVCURRCODE.SelectedValue);
                    SessionObject.Set("NP1_TOTALRIDERPREM", txtNP1_TOTALRIDERPREM.Text);
                    SessionObject.Set("NPR_TOTPREM", txtNPR_TOTPREM.Text);
                    SessionObject.Set("NP1_PROPOSAL", txtNP1_PROPOSAL.Text);
                    SessionObject.Set("NP2_SETNO", txtNP2_SETNO.Text);
                    SessionObject.Set("NPR_BASICFLAG", txtNPR_BASICFLAG.Text);

                    SessionObject.Set("NP1_RETIREMENTAGE", txtNP1_RETIREMENTAGE.Text);
                    SessionObject.Set("NP1_TOTALANNUALPREM", txtNP1_TOTALANNUALPREM.Text);

                    SessionObject.Set("NPR_INDEXATION", ddlNPR_INDEXATION.SelectedValue);
                    SessionObject.Set("NPR_INDEXRATE", txtNPR_INDEXRATE.Text);

                    SessionObject.Set("NPR_EDUNITS", txtNPR_EDUNITS.Text);
                    SessionObject.Set("NPR_INTERESTRATE", txtNPR_INTERESTRATE.Text);
                }
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
            txtNP2_AGEPREM.Text = "0";
            txtNP2_AGEPREM2ND.Text = "0";
            ddlPPR_PRODCD.Enabled = true;
            ddlPCU_CURRCODE.ClearSelection();
            ddlCRL_RELEATIOCD.ClearSelection();
            txt_NomineeName.Text = "";
            txtNomineeDOB.Text = "";
            txtNBF_AGE.Text = "";
            ddlBSCCode.ClearSelection(); 
            ddlCCB_CODE.ClearSelection();
            txtNPR_SUMASSURED.Text = "";
            txtNPR_SUMASSURED_2.Text = "";
            ddlPCU_CURRCODE_PRM.ClearSelection();
            ddlCMO_MODE.ClearSelection();

            ddlNPR_INDEXATION.ClearSelection();
            txtNPR_INDEXRATE.Text = "0.00";
            txtNPR_EDUNITS.Text = "";
            txtNPR_INTERESTRATE.Text = "";

            txtNPR_PAIDUPTOAGE.Text = "";
            txtNPR_BENEFITTERM.Text = "";
            txtNPR_PREMIUMTER.Text = "";
            txtNPR_PREMIUMDISCOUNT.Text = "";
            txtNPR_PREMIUM.Text = "0";
            ddlNPR_INCLUDELOADINNIV.ClearSelection();
            txtNPR_EXCESPRMANNUAL.Text = "";
            ddlNPR_COMMLOADING.ClearSelection();
            txtNP1_PERIODICPREM.Text = "0";
            ddlPCU_AVCURRCODE.ClearSelection();
            txtNP1_TOTALRIDERPREM.Text = "0";
            txtNPR_TOTPREM.Text = "0";
            txtNPR_TOTPREM_2.Text = "0";
            txtNP1_PROPOSAL.Enabled = true;
            txtNP1_PROPOSAL.Text = "";
            txtNP2_SETNO.Enabled = true;
            txtNP2_SETNO.Text = "0";
            txtNPR_BASICFLAG.Text = "";
            txtNP1_RETIREMENTAGE.Text = "";
            txtNP1_TOTALANNUALPREM.Text = "";
            if (SessionObject.GetString("s_USE_TYPE") == "S")
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

            //Charges/Fees
            rowset LNP2_CHARGES = DB.executeQuery("SELECT NP2_TOTLOAD from LNP2_POLICYMASTR  WHERE NP1_PROPOSAL='" + NP1_PROPOSAL + "' AND NP2_SETNO=1");
            if (LNP2_CHARGES.next())
            {
                txtcharges.Text = LNP2_CHARGES.getDouble("NP2_TOTLOAD").ToString();
            }


            rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + NP1_PROPOSAL + "' AND NP2_SETNO=" + NP2_SETNO);
            if (LNP2_POLICYMASTR.next())
            {
                txtNP2_AGEPREM.Text = LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
                txtNP2_AGEPREM2ND.Text = LNP2_POLICYMASTR.getDouble("NP2_AGEPREM2ND").ToString();
            }

            rowset rowLNPR_PRODUCT = DB.executeQuery("select NPR_PREMIUM, NPR_EXCESSPREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + NP1_PROPOSAL + "' AND NP2_SETNO=" + NP2_SETNO + " AND PPR_PRODCD='" + PPR_PRODCD + "'");
            if (rowLNPR_PRODUCT.next())
            {
                String strNPR_PREMIUM = rowLNPR_PRODUCT.getDouble("NPR_PREMIUM").ToString();
                String strNPR_EXCESSPREMIUM = rowLNPR_PRODUCT.getDouble("NPR_EXCESSPREMIUM").ToString();
                String strtotal_NPR_PREMIUM = Convert.ToString(SessionObject.GetString("total_NPR_PREMIUM"));

                double val1 = 0;
                val1 = strNPR_PREMIUM == String.Empty ? 0 : Double.Parse(strNPR_PREMIUM);
                val1 += strNPR_EXCESSPREMIUM == String.Empty ? 0 : Double.Parse(strNPR_EXCESSPREMIUM);
                txtNP1_PERIODICPREM.Text = val1.ToString();

                //by asif 06 jan - val1+=strtotal_NPR_PREMIUM==String.Empty?0:Double.Parse(strtotal_NPR_PREMIUM);
                //by asif 06 jan - txtNPR_TOTPREM.Text=val1.ToString();

                txtNP1_TOTALRIDERPREM.Text = strtotal_NPR_PREMIUM;
            }

            //by asif 06 jan - txtNPR_TOTPREM.Text = Convert.ToString(ace.Ace_General.getPremium(""+SessionObject.Get("NP1_PROPOSAL")));
            //txtNPR_TOTPREM_2.Text = Convert.ToString(ace.Ace_General.getPremium(""+SessionObject.Get("NP1_PROPOSAL")));


            string query = "SELECT NPH_FULLNAME, NPH_SEX, NPH_BIRTHDATE, NPH_AGE FROM LNPH_PHOLDER WHERE NPH_CODE||NPH_LIFE IN( select NPH_CODE||NPH_LIFE from LNU1_UNDERWRITI WHERE NP1_PROPOSAL ='" + SessionObject.Get("NP1_PROPOSAL") + "')";
            rowset rsPHolder = DB.executeQuery(query);

            if (rsPHolder.next())
            {
                lblName.Text = rsPHolder.getString("NPH_FULLNAME");
                //DateTime dt = rsPHolder.getDate("NPH_BIRTHDATE").Year ;
                lblAge.Text = ace.Ace_General.calculate_Age_InYear(rsPHolder.getDate("NPH_BIRTHDATE")).ToString();
                lblGender.Text = rsPHolder.getString("NPH_SEX");
            }
            //2nd life
            if (rsPHolder.next())
            {
                lblName2.Text = rsPHolder.getString("NPH_FULLNAME");
                lblAge2.Text = ace.Ace_General.calculate_Age_InYear(rsPHolder.getDate("NPH_BIRTHDATE")).ToString();
                lblGender2.Text = rsPHolder.getString("NPH_SEX");
            }


            //Manual Code
            txtNP1_RETIREMENTAGE.Text = Session["NP1_RETIREMENTAGE"] == null ? "" : Session["NP1_RETIREMENTAGE"].ToString();
            txtNP1_TOTALANNUALPREM.Text = Session["NP1_TOTALANNUALPREM"] == null ? "" : Session["NP1_TOTALANNUALPREM"].ToString();
            //Manual Code



            ddlPPR_PRODCD.ClearSelection();
            ListItem item0 = ddlPPR_PRODCD.Items.FindByValue(objRow["PPR_PRODCD"].ToString());
            if (item0 != null)
            {
                item0.Selected = true;
            } ddlPPR_PRODCD.Enabled = false;


            //ddlBSCCode.ClearSelection();
            rowset BSCCode_Result = DB.executeQuery("select aag_agcode || '-' || aag_fullname || '-' || aag_imedsupr || '-' ||aag_imedsupr_name || ' - ' || chl_level aag_agcode from vw_laag_bsc where aag_agcode in (select l.aag_agcode from lnoe_oventitlement L where l.np1_proposal  ='" + SessionObject.Get("NP1_PROPOSAL") + "') ");
            if (BSCCode_Result.next())
            {
                ddlBSCCode.SelectedValue = BSCCode_Result.getString("aag_agcode").ToString();
              
            } //ddlBSCCode.Enabled = false;


            ddlPCU_CURRCODE.ClearSelection();
            ListItem item1 = ddlPCU_CURRCODE.Items.FindByValue(objRow["PCU_CURRCODE"].ToString());
            if (item1 != null)
            {
                item1.Selected = true;
            }
            ddlCCB_CODE.ClearSelection();
            ListItem item8 = ddlCCB_CODE.Items.FindByValue(objRow["CCB_CODE"].ToString());
            if (item8 != null)
            {
                item8.Selected = true;
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


            txtNPR_SUMASSURED.Text = objRow["NPR_SUMASSURED"].ToString();
            txtNPR_SUMASSURED_2.Text = objRow["NPR_SUMASSURED"].ToString();

            txtNPR_PAIDUPTOAGE.Text = objRow["NPR_PAIDUPTOAGE"].ToString();
            txtNPR_BENEFITTERM.Text = objRow["NPR_BENEFITTERM"].ToString();
            txtNPR_PREMIUMTER.Text = objRow["NPR_PREMIUMTER"].ToString();
            txtNPR_PREMIUMDISCOUNT.Text = objRow["NPR_PREMIUMDISCOUNT"].ToString();

                        
            //txtNPR_PREMIUM.Text=objRow["NPR_PREMIUM"].ToString();
            rowset BaseProductInfo = DB.executeQuery("select NPR_PREMIUM FROM lnpr_product WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "' and npr_basicflag='Y'");
            if (BaseProductInfo.next())
            {
                txtNPR_PREMIUM.Text = BaseProductInfo.getDouble("NPR_PREMIUM").ToString();
            }
            else
            {
                txtNPR_PREMIUM.Text = "0";
            }




            ddlNPR_INCLUDELOADINNIV.ClearSelection();

            txtNPR_EDUNITS.Text = objRow["NPR_EDUNITS"].ToString();
            txtNPR_INTERESTRATE.Text = objRow["NPR_INTERESTRATE"].ToString();



            //Add Session
            Session.Add("NPR_SUMASSURED", objRow["NPR_SUMASSURED"].ToString());
            Session.Add("NPR_BENEFITTERM", objRow["NPR_BENEFITTERM"].ToString());

            ListItem item7 = ddlNPR_INCLUDELOADINNIV.Items.FindByValue(objRow["NPR_INCLUDELOADINNIV"].ToString());
            if (item7 != null)
            {
                item7.Selected = true;
            } txtNPR_EXCESPRMANNUAL.Text = objRow["NPR_EXCESSPREMIUM"].ToString();
            ddlNPR_COMMLOADING.ClearSelection();
            ListItem item9 = ddlNPR_COMMLOADING.Items.FindByValue(objRow["NPR_COMMLOADING"].ToString());
            if (item9 != null)
            {
                item9.Selected = true;
            }

            //Custom fill
            String strCRL_RELEATIOCD = "";
            String strNBF_BENNAME = "";
            String strNBF_AGE = "";
            String strNBF_DOB = "";
            rowset rs_lnbf_beneficiary = DB.executeQuery("Select CRL_RELEATIOCD,NBF_BENNAME,NBF_AGE,NBF_DOB from lnbf_beneficiary  where np1_proposal='" + SessionObject.Get("NP1_PROPOSAL") + "'");
            if (rs_lnbf_beneficiary.next())
            {
                 strCRL_RELEATIOCD = rs_lnbf_beneficiary.getString(1); 
                 strNBF_BENNAME = rs_lnbf_beneficiary.getString(2);
                 strNBF_AGE = rs_lnbf_beneficiary.getString(3);
                 strNBF_DOB = rs_lnbf_beneficiary.getString(4).ToString().Split(' ')[0];
            }
            ddlCRL_RELEATIOCD.ClearSelection();
            ListItem itemCRL = ddlCRL_RELEATIOCD.Items.FindByValue(strCRL_RELEATIOCD);
            if (itemCRL != null)
            {
                itemCRL.Selected = true;
            }
            ddlCRL_RELEATIOCD.Enabled = true;
            txtNBF_AGE.Text = strNBF_AGE;
            txt_NomineeName.Text = strNBF_BENNAME;
            txtNomineeDOB.Text = strNBF_DOB;
            //Pick data
            String strPCU_CURRCODE_PRM = "";
            String strCMO_MODE = "";
            String strPCU_AVCURRCODE = "";
            String strCFR_FORFEITUCD = "";

            rowset rsLNP1_POLICYMASTR = DB.executeQuery("select PCU_CURRCODE, CMO_MODE, PCU_AVCURRCODE, CFR_FORFEITUCD from lnp1_policymastr where np1_proposal ='" + SessionObject.Get("NP1_PROPOSAL") + "'");
            if (rsLNP1_POLICYMASTR.next())
            {
                strPCU_CURRCODE_PRM = rsLNP1_POLICYMASTR.getString(1);
                strCMO_MODE = rsLNP1_POLICYMASTR.getString(2);
                strPCU_AVCURRCODE = rsLNP1_POLICYMASTR.getString(3);
                strCFR_FORFEITUCD = rsLNP1_POLICYMASTR.getString(4);
            }
         

            //Set data

            //PCU_CURRCODE_PRM
            ddlPCU_CURRCODE_PRM.ClearSelection();
            ListItem item2 = ddlPCU_CURRCODE_PRM.Items.FindByValue(strPCU_CURRCODE_PRM);
            if (item2 != null)
            {
                item2.Selected = true;
            } ddlPCU_CURRCODE_PRM.Enabled = true;

            //CMO_MODE
            ddlCMO_MODE.ClearSelection();
            ListItem item3 = ddlCMO_MODE.Items.FindByValue(strCMO_MODE);
            if (item3 != null)
            {
                item3.Selected = true;
            } ddlCMO_MODE.Enabled = true;

            //CFR_FORFEITUCD
            ddlCFR_FORFEITUCD.ClearSelection();
            ListItem itemCFR = ddlCFR_FORFEITUCD.Items.FindByValue(strCFR_FORFEITUCD);
            if (itemCFR != null)
            {
                itemCFR.Selected = true;
            } ddlCFR_FORFEITUCD.Enabled = true;

            ddlNPR_INDEXATION.ClearSelection();
            ListItem item5 = ddlNPR_INDEXATION.Items.FindByValue(objRow["NPR_INDEXATION"].ToString());
            if (item5 != null)
            {
                item5.Selected = true;
            } ddlNPR_INDEXATION.Enabled = true;

            txtNPR_INDEXRATE.Text = (objRow["NPR_INDEXRATE"].ToString() == "" ? "0.00" : objRow["NPR_INDEXRATE"].ToString());

            //PCU_AVCURRCODE
            ddlPCU_AVCURRCODE.ClearSelection();
            ListItem item4 = ddlPCU_AVCURRCODE.Items.FindByValue(strPCU_AVCURRCODE);
            if (item4 != null)
            {
                item4.Selected = true;
            } ddlPCU_AVCURRCODE.Enabled = true;


            //Custom fill end


            txtNPR_TOTPREM.Text = objRow["NPR_TOTPREM"].ToString();
            Session.Add("NPR_TOTPREM", txtNPR_TOTPREM.Text);

            //txtNP1_PROPOSAL.Text=objRow["NP1_PROPOSAL"].ToString();
            txtNP1_PROPOSAL.Text = Convert.ToString(SessionObject.Get("NP1_PROPOSAL"));

            txtNP1_PROPOSAL.Enabled = false;
            txtNP2_SETNO.Text = objRow["NP2_SETNO"].ToString();
            txtNP2_SETNO.Enabled = false;
            txtNPR_BASICFLAG.Text = objRow["NPR_BASICFLAG"].ToString();


            if (columnNameValue == null || columnNameValue.Count == 0)
                columnNameValue = Utilities.RowToNameValue(objRow);
            SHSM_SecurityPermission security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
            foreach (string processName in AllProcess)
            {
                if (security.ProcessAllowed(processName))
                {
                    AllowedProcess += "'" + processName + "'" + ",";
                }
            }
            if (AllowedProcess.Length > 0)
                AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length - 1);
            if (!security.UpdateAllowed)
            {
                _lastEvent.Text = EnumControlArgs.View.ToString();
            }
        }


        protected sealed override string ErrorHandle(string message)
        {
            message = base.ErrorHandle(message);
            PrintMessage(message); return message;
        }

        protected void PrintMessage(string message)
        {
            MessageScript.Text += string.Format("alert('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
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
                if (ControlArgs != null)
                {
                    if (ControlArgs[0] != null)
                    {
                        EnumControlArgs arg = (EnumControlArgs)ControlArgs[0];
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
            foreach (object key in columnNameValue.Keys)
            {
                string strKey = key.ToString();
                allFields.add(strKey, columnNameValue.get(strKey));
            }

            foreach (Control c in this.myForm.Controls)
            {
                string _fieldName = "";
                if (c is WebControl)
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            if (c.ID.IndexOf("txt") == 0)
                                _fieldName = c.ID.Replace("txt", "");
                            else
                                _fieldName = c.ID;
                            if (!columnNameValue.Contains(_fieldName))
                            {
                                allFields.add(_fieldName, ((TextBox)c).Text);
                            }
                            break;
                        case "SHMA.Enterprise.Presentation.WebControls.TextBox":
                            if (c.ID.IndexOf("txt") == 0)
                                _fieldName = c.ID.Replace("txt", "");
                            else
                                _fieldName = c.ID;
                            if (!columnNameValue.Contains(_fieldName))
                            {
                                allFields.add(_fieldName, ((TextBox)c).Text);
                            }
                            break;
                        case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
                            if (c.ID.IndexOf("ddl") == 0)
                                _fieldName = c.ID.Replace("ddl", "");
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
                    selected = false;
                }
            }
            return selected;
        }
        private void FindAndSelectCurrentRecord()
        {
            if (IsRecordSelected())
            {
                PPR_PRODCD = SessionObject.GetString("PPR_PRODCD");
                NP1_PROPOSAL = SessionObject.GetString("NP1_PROPOSAL");
                NP2_SETNO = double.Parse(SessionObject.GetString("NP2_SETNO"));

                DataRow selectedRow = new LNPR_PRODUCTDB(dataHolder).FindByPK(NP1_PROPOSAL, NP2_SETNO, PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
                ShowData(selectedRow);
                _lastEvent.Text = "Edit";
            }
        }
        void DisableForm()
        {
            NormalEntryTableDiv.Style.Add("visibility", "hidden");
            CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
            //OtherScript.Text = "var IllustrateNoor = '';";
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

        protected void PrintCustomMessage(string message)
        {
            //With Message
            MessageScript.Text = string.Format("alert('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
            MessageScript.Text += "; _callback();";
        }
        protected void CustomDoControl()
        {
            base.DoControl();
            String lastEvent = _lastEvent.Text;


            if (!_lastEvent.Text.Equals("Delete"))
            {
                if (NewRecord == true)
                {
                    _lastEvent.Text = "New";
                }
                else
                {
                    _lastEvent.Text = "Edit";
                }
            }
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

            SessionObject.Remove("CFR_FORFEITUCD");

            SessionObject.Remove("CCB_CODE");
            SessionObject.Remove("NPR_SUMASSURED");
            SessionObject.Remove("PCU_CURRCODE_PRM");
            SessionObject.Remove("CMO_MODE");

            SessionObject.Remove("NPR_INDEXATION");
            SessionObject.Remove("NPR_INDEXRATE");

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

            SessionObject.Remove("NPR_EDUNITS");
            SessionObject.Remove("NPR_INTERESTRATE");
        }


        private void ViewInitialState()
        {
            //SessionObject.Set("NP1_PROPOSAL","R/07/0010042");
            //SessionObject.Set("NP2_SETNO","1");
            rowset rsPPR_PRODCD = DB.executeQuery("SELECT PPR_PRODCD FROM LNPR_PRODUCT  WHERE NP1_PROPOSAL = '" + SessionObject.Get("NP1_PROPOSAL") + "' AND NVL(NPR_BASICFLAG,'N') = 'Y'");
            if (rsPPR_PRODCD.next())
                SessionObject.Set("PPR_PRODCD", rsPPR_PRODCD.getString(1));

            rowset rsPolicyMaster = DB.executeQuery("select NP1_RETIREMENTAGE, NP1_TOTALANNUALPREM from lnp1_policymastr where NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");
            if (rsPolicyMaster.next())
            {
                SessionObject.Set("NP1_RETIREMENTAGE", rsPolicyMaster.getString("NP1_RETIREMENTAGE"));
                SessionObject.Set("NP1_TOTALANNUALPREM", rsPolicyMaster.getString("NP1_TOTALANNUALPREM"));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

        }

        #region "ImranWork"

        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;

         }
        public DataTable GetData(string sql)
        {

            
            cmd.Connection = GetConn();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter daEmail = new OleDbDataAdapter(cmd);
            DataTable dtEmail = new DataTable();
            dtEmail.Reset();
            dtEmail.Clear();

            daEmail.Fill(dtEmail);
            //cmd.Dispose(); old
            cmd.Connection.Close();

            return dtEmail;


        }

        public string DML(string sql)
        {
            try
            {
                cmd.CommandText = sql;
                cmd.Connection = GetConn();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return "done";
            }

            catch (Exception ex)
            {

                cmd.Connection.Close();
                return ex.Message;

            }

        }

       // void SaveAgent()
       // {
       //     string AgentCodeName = txtAgentCode.Text.ToString() + "-" + txtAgentName.Text.ToString();

       //     Sql = "Select * from larq_request H where H.Use_Userid='" + SessionObject.Get("s_USE_USERID").ToString() + "' and H.Np1_Proposal='" + SessionObject.Get("NP1_PROPOSAL") + "' and H.ARQ_REQUESTYPE='RPOL'";

       //     if (GetData(Sql).Rows.Count > 0)
       //     {
       //         //update
       //         Sql = "Update larq_request M set M.ARQ_COMMENTS='" + AgentCodeName + "' where M.Np1_Proposal='" + SessionObject.Get("NP1_PROPOSAL").ToString() + "' and M.Use_Userid='" + SessionObject.Get("s_USE_USERID").ToString() + "' and M.ARQ_REQUESTYPE='RPOL'";

       //         string QResult = DML(Sql);

       //         if (QResult == "done")
       //         {
       //             //  txtAgentName.Text = "DONE";
       //         }
       //         else
       //         {
       //             txtAgentName.Text = QResult;

       //         }
       //     }

       //     else

       //     {
       //         Sql = "insert into larq_request (pfs_acntyear,arq_requestype,arq_requestno,oac_activicode,use_userid,np1_proposal,use_timedate,arq_reqdate,arq_auto,arq_comments) " +
       //            "Values " +
       //           " ((select A.PFS_ACNTYEAR " +
       //" from   SLILASPRD.PFS_FISCALYR A " +
       //" WHERE A.PFS_CURPRVNXT = 'C') " +
       //",'RPOL', " +
       //"(Select NVL(max(a.arq_requestno),0) + 1 as reqno from larq_request a where a.arq_requestype = 'RPOL' " +
       //" and a.pfs_acntyear = (select b.PFS_ACNTYEAR from   SLILASPRD.PFS_FISCALYR b WHERE b.PFS_CURPRVNXT = 'C')), " +
       //" '1500200156','" + SessionObject.Get("s_USE_USERID").ToString() + "','" + SessionObject.Get("NP1_PROPOSAL") +
       //"',sysdate,(SELECT TRUNC(SYSDATE) AS current_date FROM dual),'A','" + AgentCodeName + "')";

       //         string Msg = DML(Sql);

       //         if (Msg == "done")
       //         {

       //         }

       //         else

       //         {
       //             txtAgentName.Text = Msg;
       //         }

       //     }

        
       // }


        //void ShowAgent()
        //{


        //    Sql = "Select * from larq_request H where H.Use_Userid='" + SessionObject.Get("s_USE_USERID").ToString() + "' and H.Np1_Proposal='" + SessionObject.Get("NP1_PROPOSAL") + "' and H.ARQ_REQUESTYPE='RPOL'";

        //    dtAgent = GetData(Sql);

        //    if (dtAgent.Rows.Count > 0)
        //    {
        //        Session["AgentFind"] = "Yes";
        //        txtAgentCode.Text = Regex.Replace(dtAgent.Rows[0]["ARQ_COMMENTS"].ToString(), @"\D", "").ToString();
        //        txtAgentName.Text = Regex.Replace(dtAgent.Rows[0]["ARQ_COMMENTS"].ToString(), @"[^a-zA-Z\s]", "").ToString();
        //    }
        //    else

        //    {
        //        Session["AgentFind"] = "No";
        //        txtAgentName.Text = "";
        //        txtAgentCode.Text = "";

        //    }
        //    //        Sql = "insert into larq_request (pfs_acntyear,arq_requestype,arq_requestno,oac_activicode,use_userid,np1_proposal,use_timedate,arq_reqdate,arq_auto,arq_comments) " +
        //    //            "Values " +
        //    //           " ((select A.PFS_ACNTYEAR " +
        //    //" from   SLILASPRD.PFS_FISCALYR A " +
        //    //" WHERE A.PFS_CURPRVNXT = 'C') " +
        //    //",'RPOL', " +
        //    //"(Select max(a.arq_requestno) + 1 as reqno from larq_request a where a.arq_requestype = 'RPOL' " +
        //    //" and a.pfs_acntyear = (select b.PFS_ACNTYEAR from   SLILASPRD.PFS_FISCALYR b WHERE b.PFS_CURPRVNXT = 'C')), " +
        //    //" '1500200156','" + SessionObject.Get("s_AAG_NAME").ToString() + "','" + SessionObject.Get("NP1_PROPOSAL") +
        //    //"',sysdate,(SELECT TRUNC(SYSDATE) AS current_date FROM dual),'A','" + AgentCodeName + "')";

        //    //        string Msg = DML(Sql);

        //    //        txtAgentName.Text = Msg;

        //}

        #endregion

       
    }
}

