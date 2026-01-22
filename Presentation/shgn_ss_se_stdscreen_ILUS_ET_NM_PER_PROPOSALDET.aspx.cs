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

using System.Data.OracleClient;
namespace SHAB.Presentation
{
    //shgn_gs_se_stdgridscreen_
    public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET : SHMA.Enterprise.Presentation.TwoStepController
    {

        //controls


        //		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
        //		protected System.Web.UI.WebControls.DropDownList pagerList;
        protected System.Web.UI.WebControls.Literal _lastEvent;
        protected System.Web.UI.WebControls.Literal _deleteEvent;

        protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

        protected System.Web.UI.WebControls.Literal MessageScript;
        protected System.Web.UI.WebControls.Literal FooterScript;
        protected System.Web.UI.WebControls.Literal HeaderScript;


        NameValueCollection columnNameValue = null;

        string[] AllProcess = { "shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process" };
        string AllowedProcess = "";


        /******************* Entity Fields Decleration *****************/
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCCN_CTRYCD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_USERID;

        // Asif - Restore following fields
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_CHANNEL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_CHANNELDETAIL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_PRODUCER;


        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_PROPOSAL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP2_COMMENDATE;
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPDATE;
        protected System.Web.UI.WebControls.CompareValidator cfvCCN_CTRYCD;
        protected System.Web.UI.WebControls.CompareValidator cfvUSE_USERID;

        // Asif - Restore following fields
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_CHANNEL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_CHANNELDETAIL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_PRODUCER;


        protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPOSAL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP2_COMMENDATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_PROPDATE;


        protected SHMA.Enterprise.Presentation.WebControls.DatePopUp TDtxtNP1_PROPDATEoptional;





        /************ pk variables declaration ************/

        #region pk variables declaration
        private string NP1_PROPOSAL;
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


        //Change date (upto 28) if date is 29,30 or 31
        public void setCommencementDate()
        {
            if (txtNP2_COMMENDATE.Text.Length <= 0)
            {
                
                string s=ace.clsIlasUtility.GetPCTControlParam();
                if (s == "ENABLEDLAST")
                {
                    txtNP2_COMMENDATE.Text = Convert.ToString(Session["s_COMM_DATE"]);
                    txtNP2_COMMENDATE.Enabled = false;
                }
                else
                {
                    string Date = Convert.ToString(Session["s_COMM_DATE"]);
                    string[] DateArr = Date.Split('/');
                    if (DateArr[0] == "29" || DateArr[0] == "30" || DateArr[0] == "31")
                    {
                        txtNP2_COMMENDATE.Text = "28/" + DateArr[1] + "/" + DateArr[2];
                    }
                    else
                    {
                        txtNP2_COMMENDATE.Text = Date;
                    }
                }
                
            }

            if (txtNP2_COMMENDATE.Text.Length == 0)
            {
                string Date = Convert.ToString(Session["s_CURR_SYSDATE"]);
                string str = null;
                string[] DateArr = null;
                str = Date;
                char[] splitchar = { '/' };
                DateArr = str.Split(splitchar);

                int currentDay = Convert.ToInt32(DateArr[0]);
                int maxCommDay = ace.clsIlasUtility.getMaximumCommencementDate();

                //if (currentDay > 27)
                //{
                //}

                if (DateArr[0] == "29" || DateArr[0] == "30" || DateArr[0] == "31")
                {
                    txtNP2_COMMENDATE.Text = "28/" + DateArr[1] + "/" + DateArr[2];
                }
                else
                {
                    txtNP2_COMMENDATE.Text = Date;
                }
            }
        }


        sealed protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            GetSessionValues();
            //CheckKeyLevel();
            //recordCount = LNP1_POLICYMASTRDB.RecordCount;
            return dataHolder;

        }
        sealed protected override void BindInputData(DataHolder dataHolder)
        {

            IDataReader LCCN_COUNTRYReader0 = LCCN_COUNTRYDB.GetDDL_ILUS_ET_NM_PER_PROPOSALDET_CCN_CTRYCD_RO(); ;
            ddlCCN_CTRYCD.DataSource = LCCN_COUNTRYReader0;
            ddlCCN_CTRYCD.DataBind();
            LCCN_COUNTRYReader0.Close();
            IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNEL_RO(); ;
            ddlNP1_CHANNEL.DataSource = LCSD_SYSTEMDTLReader1;
            ddlNP1_CHANNEL.DataBind();
            LCSD_SYSTEMDTLReader1.Close();

            IDataReader LCSD_SYSTEMDTLReader2 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PROPOSALDET_NP1_CHANNELDETAIL_RO(); ;
            ddlNP1_CHANNELDETAIL.DataSource = LCSD_SYSTEMDTLReader2;
            ddlNP1_CHANNELDETAIL.DataBind();
            LCSD_SYSTEMDTLReader2.Close();

            //For Bank Assurance			
            IDataReader LCSD_SYSTEMDTLReader3 = USE_USERMASTERDB.GetDDL_ILUS_ET_NM_PER_PROPOSALDET_USE_USERID_RO(); ;
            ddlUSE_USERID.DataSource = LCSD_SYSTEMDTLReader3;
            ddlUSE_USERID.DataBind();
            LCSD_SYSTEMDTLReader3.Close();

            _lastEvent.Text = "New";

            FindAndSelectCurrentRecord();

            string np1selected = "";
            string strDetailInfo = "";
            string validateproposal = "";
            ace.POLICY_ACCEPTANCE Polacc = new ace.POLICY_ACCEPTANCE();
            bool validate = false;


            //Asif - Get Detail information for Proposal - start
            strDetailInfo = "var PolicyApproved='N'; var PremiumExist='N';";
            if (IsRecordSelected())
            {

                string policyStatus = "var PolicyApproved='N';";
                if (LNP1_POLICYMASTRDB.PolicyApproved(SessionObject.GetString("NP1_PROPOSAL")))
                {
                    policyStatus = "var PolicyApproved='Y';";
                }

                if (policyStatus == "var PolicyApproved='N';")
                {
                    validate = Polacc.CheckIfPolicyValidate(Session["NP1_PROPOSAL"].ToString());
                    if (validate != false)
                    {
                        validateproposal = "var validate ='Y';";

                    }

                }

                string PremiumStatus = "var PremiumExist='N';";
                if (LNPR_PRODUCTDB.PremiumExist(SessionObject.GetString("NP1_PROPOSAL")) == true)
                {
                    PremiumStatus = "var PremiumExist='Y';";
                }
                string UnderWrite = "var UnderWritten='N';";
                if (LNPR_PRODUCTDB.UnderWritten(SessionObject.GetString("NP1_PROPOSAL")) == true)
                {
                    UnderWrite = "var UnderWritten='Y';";
                }
                strDetailInfo = policyStatus + PremiumStatus + UnderWrite + validateproposal;

            }
            //Asif - Get Detail information for Proposal - end


            //CSSLiteral.Text = ace.Ace_General.loadInnerStyle();
            CSSLiteral.Text = ace.Ace_General.LoadPageStyle();//loadInnerStyle();

            HeaderScript.Text = EnvHelper.Parse("var valSYSDATE=SV(\"s_CURR_SYSDATE\");");
            //FooterScript.Text = EnvHelper.Parse("getField(\"NP2_COMMENDATE\").value=SV(\"NP2_COMMENDATE\"); getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");") ;
            //FooterScript.Text = EnvHelper.Parse("getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");") ;
            //stop by asif - FooterScript.Text = EnvHelper.Parse("getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"USE_USERID\").value=SV(\"USE_USERID\");") ;
            //FooterScript.Text = EnvHelper.Parse("getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");getField(\"USE_USERID\").value=SV(\"USE_USERID\");" + strDetailInfo) ;
            FooterScript.Text = EnvHelper.Parse("getField(\"AAG_NAME\").value=SV(\"s_AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"s_AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"s_CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"s_CCH_CODE\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"s_CCD_CODE\");getField(\"USE_USERID\").value=SV(\"s_USE_USERID\");" + strDetailInfo);

            //RegisterArrayDeclaration("AllowedProcess", AllowedProcess);

            //***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("") ? "0" : AllowedProcess));

            if (SessionObject.GetString("USE_TYPE") == "S")
            {
                //txtNP2_COMMENDATE.Enabled = true;
                txtNP1_PROPDATE.Enabled = true;
            }
            else
            {
                //txtNP2_COMMENDATE.Enabled = false;
                txtNP1_PROPDATE.Enabled = false;
            }

            //Change date (upto 28) if date is 29,30 or 31
            setCommencementDate();

        }
        #endregion

        #region Major methods of the final step
        protected override void ValidateRequest()
        {
            base.ValidateRequest();
            foreach (string key in LNP1_POLICYMASTR.PrimaryKeys)
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

        sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
        {
            SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
            columnNameValue = new NameValueCollection();
            SaveTransaction = false;
            shgn.SHGNCommand entityClass = new ace.ILUS_ET_NM_PER_PROPOSALDET();
            entityClass.setNameValueCollection(columnNameValue);

            _deleteEvent.Text = "N";

            string propdt = "";
            string payref = "";

            SHSM_SecurityPermission security;
            switch ((EnumControlArgs)ControlArgs[0])
            {
                case (EnumControlArgs.Save):
                    _lastEvent.Text = "Save";
                    DB.BeginTransaction();
                    SaveTransaction = true;

                    //txtNP1_PROPOSAL.Text = numberGeneration("R/"+String.Format("{0:yy}",DateTime.Now)+"/"+String.Format("{0:MM}",DateTime.Now),null,2,"NP1_PROPOSAL","LNP1_POLICYMASTR");
                    if (ace.ValidationUtility.isManualProposal() == false)
                    {
                        ace.ProcedureAdapter cs = new ace.ProcedureAdapter("GENERATE_PROPOSALNO_CALL");
                        cs.RegisetrOutParameter("PROPOSALNO", System.Data.OleDb.OleDbType.VarChar, 1000);
                        cs.Execute();
                        txtNP1_PROPOSAL.Text = Convert.ToString(cs.Get("PROPOSALNO"));
                    }

                    txtNP2_SETNO.Text = "1";

                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);

                    //New Code//
                    //string str=Session["NPH_INSUREDTYPE"].ToString();

                    //asif - 
                    columnNameValue.Add("CCN_CTRYCD", ddlCCN_CTRYCD.SelectedValue.Trim() == "" ? null : ddlCCN_CTRYCD.SelectedValue);
                    columnNameValue.Add("NP1_CHANNEL", ddlNP1_CHANNEL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNEL.SelectedValue);
                    columnNameValue.Add("NP1_CHANNELDETAIL", ddlNP1_CHANNELDETAIL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNELDETAIL.SelectedValue);
                    columnNameValue.Add("NP1_PRODUCER", txtNP1_PRODUCER.Text.Trim() == "" ? null : txtNP1_PRODUCER.Text);
                    columnNameValue.Add("USE_USERID", ddlUSE_USERID.SelectedValue.Trim() == "" ? null : ddlUSE_USERID.SelectedValue);
                    //columnNameValue.Add("NP1_JOINT ",Session["NPH_INSUREDTYPE"].ToString()==""?null:Session["NPH_INSUREDTYPE"].ToString()); 


                    //asif - columnNameValue.Add("CCN_CTRYCD",Session["CCN_CTRYCD"]);
                    //asif - columnNameValue.Add("USE_USERID",Session["USE_USERID"]);

                    //columnNameValue.Add("NP1_CHANNELDETAIL",Session["NP1_CHANNELDETAIL"]);
                    //columnNameValue.Add("NP1_PRODUCER",Session["NP1_PRODUCER"]);

                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_COMMENDATE", txtNP2_COMMENDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP2_COMMENDATE.Text));

                    //optionalsave
                    //					if(dtNP1_PROPDATEoptional.Text == null || dtNP1_PROPDATEoptional.Text == "")
                    //					{
                    columnNameValue.Add("NP1_PROPDATE", txtNP1_PROPDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_PROPDATE.Text));
                    //					}
                    //					else
                    //					{
                    propdt = dtNP1_PROPDATEoptional.Text.Trim() == "" ? null : dtNP1_PROPDATEoptional.Text;
                    propdt = "ProposalSignDate : " + propdt;

                    payref = txtNP1_PAYMENTREF.Text.Trim() == "" ? null : txtNP1_PAYMENTREF.Text;
                    payref = "PaymentReference : " + payref;

                    columnNameValue.Add("NP1_COMMENTS", propdt + " | " + payref);
                    //					}
                    columnNameValue.Add("NP1_PROPOSALREF", txtNP1_PROPREF.Text.Trim() == "" ? null : txtNP1_PROPREF.Text);
                    //columnNameValue.Add("NP1_QUOTATIONREF",txtNP1_PAYMENTREF.Text.Trim()==""?null:txtNP1_PAYMENTREF.Text);

                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));

                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
                    if (security.SaveAllowed)
                    {
                        entityClass.fsoperationBeforeSave();

                        new LNP1_POLICYMASTR(dataHolder).Add(columnNameValue, getAllFields(), "ILUS_ET_NM_PER_PROPOSALDET", null);

                        dataHolder.Update(DB.Transaction);

                        SessionObject.Set("key_for_aftersave_NP1_PROPOSAL", txtNP1_PROPOSAL.Text);
                        entityClass.fsoperationAfterSave();

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNP1_POLICYMASTR");
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
                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);

                    //asif - 
                    columnNameValue.Add("CCN_CTRYCD", ddlCCN_CTRYCD.SelectedValue.Trim() == "" ? null : ddlCCN_CTRYCD.SelectedValue);
                    columnNameValue.Add("NP1_CHANNEL", ddlNP1_CHANNEL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNEL.SelectedValue);
                    columnNameValue.Add("NP1_CHANNELDETAIL", ddlNP1_CHANNELDETAIL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNELDETAIL.SelectedValue);
                    columnNameValue.Add("NP1_PRODUCER", txtNP1_PRODUCER.Text.Trim() == "" ? null : txtNP1_PRODUCER.Text);
                    columnNameValue.Add("USE_USERID", ddlUSE_USERID.SelectedValue.Trim() == "" ? null : ddlUSE_USERID.SelectedValue);

                    //columnNameValue.Add("NP1_JOINT ",Session["NPH_INSUREDTYPE"].ToString()==""?null:Session["NPH_INSUREDTYPE"].ToString()); 


                    //asif - columnNameValue.Add("CCN_CTRYCD",Session["CCN_CTRYCD"]);
                    //asif - columnNameValue.Add("USE_USERID",Session["USE_USERID"]);


                    //columnNameValue.Add("NP1_CHANNELDETAIL",Session["NP1_CHANNELDETAIL"]);
                    //columnNameValue.Add("NP1_PRODUCER",Session["NP1_PRODUCER"]);


                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_COMMENDATE", txtNP2_COMMENDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP2_COMMENDATE.Text));


                    //optionalupdate
                    propdt = dtNP1_PROPDATEoptional.Text.Trim() == "" ? null : dtNP1_PROPDATEoptional.Text;
                    propdt = "ProposalSignDate : " + propdt;
                    payref = txtNP1_PAYMENTREF.Text.Trim() == "" ? null : txtNP1_PAYMENTREF.Text;
                    payref = "PaymentReference : " + payref;
                    columnNameValue.Add("NP1_COMMENTS", propdt + " | " + payref);
                    columnNameValue.Add("NP1_PROPOSALREF", txtNP1_PROPREF.Text.Trim() == "" ? null : txtNP1_PROPREF.Text);

                    columnNameValue.Add("NP1_PROPDATE", txtNP1_PROPDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_PROPDATE.Text));

                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));

                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
                    if (security.UpdateAllowed)
                    {
                        entityClass.fsoperationBeforeUpdate();

                        new LNP1_POLICYMASTR(dataHolder).Update(Utilities.File2EntityID(this.ToString()), columnNameValue);

                        dataHolder.Update(DB.Transaction);
                        entityClass.fsoperationAfterUpdate();

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNP1_POLICYMASTR");
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
                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("CCN_CTRYCD", ddlCCN_CTRYCD.SelectedValue.Trim() == "" ? null : ddlCCN_CTRYCD.SelectedValue);
                    //asif - 
                    columnNameValue.Add("NP1_CHANNEL", ddlNP1_CHANNEL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNEL.SelectedValue);
                    columnNameValue.Add("NP1_CHANNELDETAIL", ddlNP1_CHANNELDETAIL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNELDETAIL.SelectedValue);
                    columnNameValue.Add("NP1_PRODUCER", txtNP1_PRODUCER.Text.Trim() == "" ? null : txtNP1_PRODUCER.Text);
                    columnNameValue.Add("USE_USERID", ddlUSE_USERID.SelectedValue.Trim() == "" ? null : ddlUSE_USERID.SelectedValue);

                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_COMMENDATE", txtNP2_COMMENDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP2_COMMENDATE.Text));
                    columnNameValue.Add("NP1_PROPDATE", txtNP1_PROPDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_PROPDATE.Text));
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));

                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
                    if (security.DeleteAllowed)
                    {
                        entityClass.fsoperationBeforeDelete();

                        new LNP1_POLICYMASTR(dataHolder).Delete(columnNameValue);

                        dataHolder.Update(DB.Transaction);
                        entityClass.fsoperationAfterDelete();

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNP1_POLICYMASTR");
                        _deleteEvent.Text = "Y";
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
                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("CCN_CTRYCD", ddlCCN_CTRYCD.SelectedValue.Trim() == "" ? null : ddlCCN_CTRYCD.SelectedValue);
                    columnNameValue.Add("NP1_CHANNEL", ddlNP1_CHANNEL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNEL.SelectedValue);
                    columnNameValue.Add("NP1_CHANNELDETAIL", ddlNP1_CHANNELDETAIL.SelectedValue.Trim() == "" ? null : ddlNP1_CHANNELDETAIL.SelectedValue);
                    columnNameValue.Add("NP1_PRODUCER", txtNP1_PRODUCER.Text.Trim() == "" ? null : txtNP1_PRODUCER.Text);
                    columnNameValue.Add("USE_USERID", ddlUSE_USERID.SelectedValue.Trim() == "" ? null : ddlUSE_USERID.SelectedValue);

                    columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP2_COMMENDATE", txtNP2_COMMENDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP2_COMMENDATE.Text));
                    columnNameValue.Add("NP1_PROPDATE", txtNP1_PROPDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_PROPDATE.Text));
                    columnNameValue.Add("NP2_SETNO", txtNP2_SETNO.Text.Trim() == "" ? null : (object)double.Parse(txtNP2_SETNO.Text));

                    security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
                    string result = "";
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
                                proccessCommand.setPrimaryKeys(LNP1_POLICYMASTR.PrimaryKeys);
                                proccessCommand.setTableName("LNP1_POLICYMASTR");
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
                    if (result.Length > 0)
                        PrintMessage(result);
                    break;
            }
        }

        sealed protected override void DataBind(DataHolder dataHolder)
        {
            LNP1_POLICYMASTRDB LNP1_POLICYMASTRDB_obj = new LNP1_POLICYMASTRDB(dataHolder);



            if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
            {
                DataRow row = LNP1_POLICYMASTRDB_obj.FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
                ShowData(row);
            }
            else
            {
                if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
                    RefreshDataFields();
                if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
                {
                    ShowData(dataHolder["LNP1_POLICYMASTR"].Rows[0]);
                }
            }
            /* a temporary work arround for errors in save replace it later with proper error flow */
            if (_lastEvent.Text == EnumControlArgs.View.ToString())
            {
                SHSM_SecurityPermission security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
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

            HeaderScript.Text = EnvHelper.Parse("var valSYSDATE=SV(\"s_CURR_SYSDATE\");");
            //getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");getField(\"CCN_CTRYCD\").disabled=true;getField(\"NP1_CHANNEL\").disabled=true;getField(\"NP1_CHANNELDETAIL\").disabled=true;
            //FooterScript.Text = EnvHelper.Parse("function fcStandardFooterFunctionsCall(){parent.frames[3].callControllerLogic(_lastEvent);} getField(\"NP2_COMMENDATE\").value=SV(\"NP2_COMMENDATE\"); getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");");
            //BY ASIF - FooterScript.Text = EnvHelper.Parse("function fcStandardFooterFunctionsCall(){parent.frames[3].callControllerLogic(_lastEvent);} ");
            //FooterScript.Text = EnvHelper.Parse("function fcStandardFooterFunctionsCall(){parent.frames[3].callControllerLogic(_lastEvent);} getField(\"NP2_COMMENDATE\").value=SV(\"s_CURR_SYSDATE\"); getField(\"NP1_PROPDATE\").value=SV(\"s_CURR_SYSDATE\"); getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");");
            FooterScript.Text = EnvHelper.Parse("function fcStandardFooterFunctionsCall(){parent.frames[3].callControllerLogic(_lastEvent);} getField(\"NP2_COMMENDATE\").value=SV(\"s_COMM_DATE\"); getField(\"NP1_PROPDATE\").value=SV(\"s_CURR_SYSDATE\"); getField(\"AAG_NAME\").value=SV(\"s_AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"s_AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"s_CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"s_CCH_CODE\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"s_CCD_CODE\");");

            //FooterScript.Text = EnvHelper.Parse("");

        }
        #endregion

        #region Events
        protected void _CustomEvent_ServerClick(object sender, System.EventArgs e)
        {
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
                string toDayDate= DateTime.Now.ToString("dd/MM/yyyy"); //  added by Imran on 27/11/2024 to resolve the null proposal date issue

                //SessionObject.Set("CCN_CTRYCD",ddlCCN_CTRYCD.SelectedValue);
                //SessionObject.Set("NP1_CHANNEL",ddlNP1_CHANNEL.SelectedValue);
                //SessionObject.Set("NP1_CHANNELDETAIL",ddlNP1_CHANNELDETAIL.SelectedValue);
                //SessionObject.Set("NP1_PRODUCER",txtNP1_PRODUCER.Text);
                SessionObject.Set("NP1_PROPOSAL", txtNP1_PROPOSAL.Text);
                SessionObject.Set("NP2_COMMENDATE", txtNP2_COMMENDATE.Text);
                SessionObject.Set("NP1_PROPDATE", txtNP1_PROPDATE.Text);
                SessionObject.Set("NP2_SETNO", txtNP2_SETNO.Text);

                // //  added by Imran on 27/11/2024 to resolve the null proposal date issue
                if (txtNP1_PROPDATE.Text == "")
                {
                    SessionObject.Set("NP1_PROPDATE", toDayDate);
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
            //if (Session["CCN_CTRYCD"]==null || Session["NP1_CHANNEL"]==null || Session["NP1_CHANNELDETAIL"]==null)
            if (Session["s_CCN_CTRYCD"] == null || Session["s_CCH_CODE"] == null || Session["s_CCD_CODE"] == null)
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
            ddlCCN_CTRYCD.ClearSelection();
            ddlNP1_CHANNEL.ClearSelection();
            ddlNP1_CHANNELDETAIL.ClearSelection();
            txtNP1_PRODUCER.Text = "";
            ddlUSE_USERID.ClearSelection();
            txtNP1_PROPOSAL.Enabled = true;

            txtNP1_PROPOSAL.Text = "";
            txtNP2_COMMENDATE.Text = "";
            txtNP1_PROPDATE.Text = "";

            if (SessionObject.GetString("USE_TYPE") == "S")
            {
                //txtNP2_COMMENDATE.Enabled = true;
                txtNP1_PROPDATE.Enabled = true;
            }
            else
            {
                //txtNP2_COMMENDATE.Enabled = false;
                txtNP1_PROPDATE.Enabled = false;
            }

            txtNP2_SETNO.Text = "0";

        }

        protected void ShowData(DataRow objRow)
        {
            RefreshDataFields();
            ddlCCN_CTRYCD.ClearSelection();
            ListItem item0 = ddlCCN_CTRYCD.Items.FindByValue(objRow["CCN_CTRYCD"].ToString());
            if (item0 != null)
            {
                item0.Selected = true;
            }

            ddlNP1_CHANNEL.ClearSelection();
            ListItem item1 = ddlNP1_CHANNEL.Items.FindByValue(objRow["NP1_CHANNEL"].ToString());
            if (item1 != null)
            {
                item1.Selected = true;
            }

            ddlNP1_CHANNELDETAIL.ClearSelection();
            ListItem item2 = ddlNP1_CHANNELDETAIL.Items.FindByValue(objRow["NP1_CHANNELDETAIL"].ToString());
            if (item2 != null)
            {
                item2.Selected = true;
            }

            ddlUSE_USERID.ClearSelection();
            ListItem item3 = ddlUSE_USERID.Items.FindByValue(objRow["USE_USERID"].ToString());
            if (item3 != null)
            {
                item3.Selected = true;
            }

            txtNP1_PRODUCER.Text = objRow["NP1_PRODUCER"].ToString();

            txtNP1_PROPOSAL.Text = objRow["NP1_PROPOSAL"].ToString();
            txtNP1_PROPOSAL.Enabled = false;

            txtNP2_COMMENDATE.Text = objRow["NP2_COMMENDATE"] == DBNull.Value ? "" : ((DateTime)objRow["NP2_COMMENDATE"]).ToShortDateString();
            if (txtNP2_COMMENDATE.Text.Length > 0)
                txtNP2_COMMENDATE.Enabled = false;

            //optionalset
            txtNP1_PROPDATE.Text = objRow["NP1_PROPDATE"] == DBNull.Value ? "" : ((DateTime)objRow["NP1_PROPDATE"]).ToShortDateString();

            if (objRow["NP1_COMMENTS"].ToString() != null && objRow["NP1_COMMENTS"].ToString() != "")
            {
                string[] comments = objRow["NP1_COMMENTS"].ToString().Split('|');

                string[] propdt = comments[0].Split(':');
                //propdt=propdt[1].ToString().Trim();
                string[] payref = comments[1].Split(':');
                //payref=payref[1].ToString().Trim();

                dtNP1_PROPDATEoptional.Text = propdt[1].ToString().Trim();
                txtNP1_PAYMENTREF.Text = payref[1].ToString().Trim();
            }
            //			dtNP1_PROPDATEoptional.Text=objRow["NP1_PROPDATE"]==DBNull.Value?"":((DateTime)objRow["NP1_PROPDATE"]).ToShortDateString();
            //			txtNP1_PAYMENTREF.Text=objRow["NP1_QUOTATIONREF"].ToString();

            txtNP1_PROPREF.Text = objRow["NP1_PROPOSALREF"].ToString();



            txtNP2_SETNO.Text = objRow["NP2_SETNO"].ToString();

            if (columnNameValue == null || columnNameValue.Count == 0)
                columnNameValue = Utilities.RowToNameValue(objRow);
            SHSM_SecurityPermission security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
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
            MessageScript.Text = string.Format("alert('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
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
            foreach (string pk in LNP1_POLICYMASTR.PrimaryKeys)
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
                NP1_PROPOSAL = SessionObject.GetString("NP1_PROPOSAL");


                DataRow selectedRow = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
                ShowData(selectedRow);
                _lastEvent.Text = "Edit";
            }
        }
        void DisableForm()
        {
            /*Response.Write("<script language='javascript'>parent.parent.setPage('shgn_gp_gp_ILUS_ET_GP_HOME');</scrip>");*/

            NormalEntryTableDiv.Style.Add("visibility", "hidden");

            CSSLiteral.Text = ace.Ace_General.LoadPageStyle();

            HeaderScript.Text = EnvHelper.Parse("var valSYSDATE='" + DateTime.Now.ToString("dd/MM/yyyy") + "';");
            //FooterScript.Text = "getField(\"NP2_COMMENDATE\").value=SV(\"NP2_COMMENDATE\"); getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");";
            FooterScript.Text = "getField(\"NP2_COMMENDATE\").value=SV(\"s_COMM_DATE\"); getField(\"NP1_PROPDATE\").value=SV(\"s_CURR_SYSDATE\"); getField(\"AAG_NAME\").value=SV(\"AAG_NAME\");getField(\"NP1_PRODUCER\").value=SV(\"AAG_AGCODE\");getField(\"CCN_CTRYCD\").value=SV(\"CCN_CTRYCD\");getField(\"NP1_CHANNEL\").value=SV(\"NP1_CHANNEL\");getField(\"NP1_CHANNELDETAIL\").value=SV(\"NP1_CHANNELDETAIL\");getField(\"USE_USERID\").value=SV(\"USE_USERID\");";
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

            //SessionObject.Remove("CCN_CTRYCD");
            //SessionObject.Remove("NP1_CHANNEL");
            //SessionObject.Remove("NP1_CHANNELDETAIL");
            //SessionObject.Remove("NP1_PRODUCER");
            SessionObject.Remove("NP1_PROPOSAL");
            //SessionObject.Remove("NP2_COMMENDATE");
            SessionObject.Remove("NP2_SETNO");
        }

        private string numberGeneration(string prefix, string postfix, int split_serial, string field_name, string table_name)
        {
            //R/01/0010105

            rowset rsMax = DB.executeQuery("select max(" + field_name + ") from " + table_name + " where " + field_name + " like '" + prefix + "%'");

            string max_NP1_PROPOSAL = string.Format("{0:00000}", "0");
            if (rsMax.next())
                max_NP1_PROPOSAL = rsMax.getString(1) == null ? max_NP1_PROPOSAL : rsMax.getString(1).ToString();

            string serialportion = max_NP1_PROPOSAL.Replace(prefix, "");
            int numserial = Int16.Parse(serialportion);
            numserial += 1;
            return "" + (prefix + string.Format("{0:00000}", numserial) + postfix);
        }
    }



}

