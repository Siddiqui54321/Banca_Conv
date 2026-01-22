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
using System.Configuration;
using ace;
namespace SHAB.Presentation
{
    //shgn_gs_se_stdgridscreen_
    public partial class PolicyAcceptance : SHMA.Enterprise.Presentation.TwoStepController
    {

        //controls
        EnvHelper env = new EnvHelper();
        int error = 0;


        //		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
        //		protected System.Web.UI.WebControls.DropDownList pagerList;
        protected System.Web.UI.WebControls.Literal _lastEvent;

        protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

        protected System.Web.UI.WebControls.Literal MessageScript;
        protected System.Web.UI.WebControls.Literal FooterScript;
        protected System.Web.UI.WebControls.Literal HeaderScript;




        NameValueCollection columnNameValue = null;

        string[] AllProcess = { "shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process" };
        string AllowedProcess = "";


        /******************* Entity Fields Decleration *****************/

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCRE_REASCODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_PROPOSAL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_PROPOSAL;
        protected System.Web.UI.WebControls.CompareValidator cfvNP1_ISSUEDATE;
        protected System.Web.UI.WebControls.CompareValidator cfvNP2_COMMENDATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP2_COMMENDATE;


        protected System.Web.UI.WebControls.CompareValidator cfvNP1_DESCIONDATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_DESCIONDATE;

        /***** ################# ******/


        /************ pk variables declaration ************/
        #region pk variables declaration		
        private string NP1_PROPOSAL;
        #endregion


        private const string APPROVED = "001";
        private const string DECLINED = "002";
        private const string POSTPONED = "003";

        private const string STANDARD = "STANDARD";//Standard
        private const string SUBSTANDARD = "SUBSTANDARD";//Sub Standard
        private const string REFER = "REFER";//


        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_DECISIONNOTE;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNP1_TRANSFERSLIP;
        protected System.Web.UI.HtmlControls.HtmlTableRow A;
        protected System.Web.UI.HtmlControls.HtmlTableRow B;



        protected System.Web.UI.HtmlControls.HtmlTableRow rowNP1_CCEXPIRY;
        //protected System.Web.UI.HtmlControls.HtmlTableRow rowNP1_PAYMENTMET;
        protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            //Following method is used to not allow Refresh option in this page.
            //base.setCachePolicy();// 
            setCachePolicy();


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
            //recordCount = LNPH_PHOLDERDB.RecordCount;
            return dataHolder;

        }

        public void scenario()
        {
            string[] scenraios = ((string)ConfigurationSettings.AppSettings["Scenarios"]).Split(',');
            string concat = "";
            string sql = "SELECT NQN_ANSWER FROM lnqn_questionnaire WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND PPR_PRODCD=? ORDER BY CQN_CODE";
            SHMA.Enterprise.Data.ParameterCollection pmr = new SHMA.Enterprise.Data.ParameterCollection();
            pmr.puts("@NP1_PROPOSAL", Session["NP1_PROPOSAL"]);
            pmr.puts("@PPR_PRODCD", Session["PPR_PRODCD"]);
            rowset rs = DB.executeQuery(sql, pmr);

            while (rs.next())
            {
                if (rs.getObject("NQN_ANSWER") != null && rs.getObject("NQN_ANSWER").ToString().Trim() == "Y")
                {
                    concat += "Y";
                }
                else
                {
                    concat += "N";
                }
            }
            try
            {
                Double BMI;
                string Query = "select H.NPH_WEIGHT, H.NPH_HEIGHT FROM LNPH_PHOLDER H INNER JOIN LNU1_UNDERWRITI U " +
                    " ON H.NPH_CODE = U.NPH_CODE  AND U.NP1_PROPOSAL='" + Session["NP1_PROPOSAL"] + "' AND " +
                    " U.NPH_CODE = (SELECT MAX(NPH_CODE) FROM LNU1_UNDERWRITI U1 WHERE U1.NP1_PROPOSAL=U.NP1_PROPOSAL )";

                DataSet ds = new DataSet();
                OleDbDataAdapter dr = new OleDbDataAdapter(Query, (OleDbConnection)DB.Connection);
                dr.Fill(ds, "DD");
                BMI = (Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_WEIGHT"])) / (Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_HEIGHT"]) * Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_HEIGHT"]));

                Query = "select NP2_AGEPREM,NP2_AGEPREM2ND from lnp2_policymastr " +
                    " WHERE NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'";
                DataSet dss = new DataSet();
                OleDbDataAdapter drr = new OleDbDataAdapter(Query, (OleDbConnection)DB.Connection);
                drr.Fill(dss, "DD");
                Double Age;
                if (dss.Tables[0].Rows[0]["NP2_AGEPREM2ND"] != null && dss.Tables[0].Rows[0]["NP2_AGEPREM2ND"].ToString() != "")
                {
                    Age = Convert.ToDouble(dss.Tables[0].Rows[0]["NP2_AGEPREM2ND"].ToString());
                }
                else
                {
                    Age = Convert.ToDouble(dss.Tables[0].Rows[0]["NP2_AGEPREM"].ToString());
                }
                bool bmi = false;
                bool decision = false;
                if ((((Convert.ToDouble(BMI) >= 18 && Convert.ToDouble(BMI) <= 29) && (Age >= 18 && Age <= 29)) || ((Convert.ToDouble(BMI) >= 18 && Convert.ToDouble(BMI) <= 30) && (Age >= 30 && Age <= 45))
                    || ((Convert.ToDouble(BMI) >= 19 && Convert.ToDouble(BMI) <= 31) && (Age >= 46 && Age <= 59)) || ((Convert.ToDouble(BMI) >= 19 && Convert.ToDouble(BMI) <= 33) && (Age >= 60))))
                {
                    bmi = true;
                }

                for (int i = 0; i < scenraios.Length; i++)
                {
                    if (concat.Equals(scenraios[i]))
                    {
                        decision = true;
                        break;
                    }
                }
                bool occCategory = false;
                occCategory = evaluateOccupation();

                if (decision && bmi && occCategory)
                {
                    //Medical Criteria Rejected.
                    DB.executeDML("UPDATE lnp2_policymastr set np2_substandar='N' where NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'");

                    /*if(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"])=="true")
					{
						Session.Add("validate_check","true");
					}*/
                    if (Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"]) == "false")
                    {
                        Session.Add("validate_check", "false");
                    }
                    else
                    {
                        Session.Add("validate_check", "true");
                    }
                }
                else //if(Update==null)
                {
                    //Medical Criteria Updated.
                    DB.executeDML("UPDATE lnp2_policymastr set np2_substandar='Y' where NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'");
                }

                //Cancel those guardians which were not used in this Proposal / Policy
                ace.ILUS_ST_GUARDIAN.RemoveUnusedGuardianRef(Convert.ToString(Session["NP1_PROPOSAL"]));
            }
            catch (Exception eg)
            {
                //Response.Write( eg.Message );
            }


        }

        private void scenarioNew()
        {
            try
            {
                //DB.BeginTransaction();
                string proposal = Convert.ToString(Session["NP1_PROPOSAL"]);
                bool isReferred = false;
                string ReasonOfSubStandard = "";
                string decision;

                SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
                //pm.puts("@NP2_COMMENDATE", getCommencementDate(), DbType.Date);
                pm.puts("@NP1_PROPOSAL", proposal, DbType.String);

                //ace.clsIlasUtility.getClientLife(proposal);
                isReferred = SHAB.Data.LNP1_POLICYMASTRDB.PolicyReferred(ace.clsIlasUtility.getClientCode(proposal));

                if (!isReferred)
                {
                    clsIlasDecision objDecision = new clsIlasDecision(proposal);


                    if (ConfigurationSettings.AppSettings["CreateReasons"] == null || ConfigurationSettings.AppSettings["CreateReasons"] != "Y")
                        decision = objDecision.getDecision();
                    else
                        decision = objDecision.getDecision(ref ReasonOfSubStandard);


                    if (decision == APPROVED)
                    {   //Accepted
                        //DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + APPROVED + "', CST_STATUSCD='" + APPROVED + "', NP2_COMMENDATE=?  WHERE NP1_PROPOSAL=? ", pm);
                        //DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='N', NP2_COMMENDATE=?  WHERE NP1_PROPOSAL=? ", pm);

                        DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + APPROVED + "', CST_STATUSCD='" + APPROVED + "' WHERE NP1_PROPOSAL=? ", pm);
                        DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='N' WHERE NP1_PROPOSAL=? ", pm);

                        if (Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"]).ToLower() == "false")
                        {
                            Session.Add("validate_check", "false");
                        }
                        else
                        {
                            Session.Add("validate_check", "true");
                        }

                        //************* Activity Log - Standard *************//			
                        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_VALIDATED_STANDARD);

                    }
                    else if (decision == POSTPONED)
                    {   //Refer to Company
                        //DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + POSTPONED + "', CST_STATUSCD='" + POSTPONED + "', NP2_COMMENDATE=?  WHERE NP1_PROPOSAL=? ", pm);
                        //DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='R', NP2_COMMENDATE=?  WHERE NP1_PROPOSAL=? ", pm);
                        DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + POSTPONED + "', CST_STATUSCD='" + POSTPONED + "' WHERE NP1_PROPOSAL=? ", pm);
                        DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='R' WHERE NP1_PROPOSAL=? ", pm);

                        //************* Activity Log - Sub Standard *************//			
                        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_VALIDATED_SUBSTANDARD);
                    }
                    else //if(Update==null)
                    {   //Rejected	
                        //DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + DECLINED + "', CST_STATUSCD='" + DECLINED + "', NP2_COMMENDATE=?  WHERE NP1_PROPOSAL=? ", pm);
                        //DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='Y', NP2_COMMENDATE=? WHERE NP1_PROPOSAL=? ", pm);
                        SaveReasonsOfSubStandard(ReasonOfSubStandard, proposal);
                        ShowReasons(proposal);
                        DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + DECLINED + "', CST_STATUSCD='" + DECLINED + "' WHERE NP1_PROPOSAL=? ", pm);
                        DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='Y' WHERE NP1_PROPOSAL=? ", pm);

                        //************* Activity Log - Sub Standard *************//			
                        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_VALIDATED_SUBSTANDARD);
                    }
                }
                else
                {
                    SaveReasonsOfSubStandard("05", proposal);
                    ShowReasons(proposal);
                    DB.executeDML("UPDATE LNP1_POLICYMASTR SET CDC_CODE='" + DECLINED + "', CST_STATUSCD='" + DECLINED + "' WHERE NP1_PROPOSAL=? ", pm);
                    DB.executeDML("UPDATE LNP2_POLICYMASTR SET NP2_SUBSTANDAR='Y' WHERE NP1_PROPOSAL=? ", pm);

                    //************* Activity Log - Sub Standard *************//			
                    Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_VALIDATED_SUBSTANDARD);

                }
                //SAVE REASONS OF SUB-STANDARD
                //				SaveReasonsOfSubStandard(ReasonOfSubStandard,proposal);
                //				ShowReasons(proposal);
                //Cancel those guardians which were not used in this Proposal / Policy
                ace.ILUS_ST_GUARDIAN.RemoveUnusedGuardianRef(proposal);

                //DB.Transaction.Commit(); 
            }
            catch (Exception ex)
            {
                //DB.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                //DB.TransactionEnd();
                //DB.DisConnect();
            }
        }
        private void ShowReasons(string proposal)
        {
            if (ConfigurationSettings.AppSettings["CreateReasons"] == null || ConfigurationSettings.AppSettings["CreateReasons"] != "Y")
            {
                pnlReason.Visible = false;
                return;
            }
            /*
			SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL",  proposal, DbType.String);
			rowset rsConditions = DB.executeQuery(" SELECT T2.RRS_REASONCODE, T2.RRS_REASONDESC FROM LNRP_REFEREDPROPOSAL T1 INNER JOIN RRS_REFEREDREASON T2 " +
												  " ON T1.RRS_REASONCODE=T2.RRS_REASONCODE  WHERE NP1_PROPOSAL=? ", pm);
			if (rsConditions.size() < 0)
			{
			}
			while(rsConditions.next())
			{
				string code  = rsConditions.getString("RRS_REASONCODE").ToUpper().Trim();
				string desc  = rsConditions.getString("RRS_REASONDESC").ToUpper().Trim();
				lstReasons.Items.Add(new ListItem(code+"-"+desc));

			}
			*/


            string strQuery = " SELECT T2.RRS_REASONCODE, T2.RRS_REASONDESC FROM LNRP_REFEREDPROPOSAL T1 INNER JOIN RRS_REFEREDREASON T2 " +
                " ON T1.RRS_REASONCODE=T2.RRS_REASONCODE  WHERE NP1_PROPOSAL=? ";

            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, proposal));


            this.dataHolder.FillData(myCommand, "RRS_DATA");
            DataTable dt = dataHolder["RRS_DATA"].Copy();
            this.dataHolder.Data.Tables.Remove("RRS_DATA");

            //IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query));
            //IDataReader reader = myCommand.ExecuteReader();

            dgReason.DataSource = dt;
            dgReason.DataBind();

            if (DecisionValues.Text == SUBSTANDARD &&
                canViewReasons())
                pnlReason.Visible = true;
            else
                pnlReason.Visible = false;
        }
        private bool canViewReasons()
        {
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);

            if (ConfigurationSettings.AppSettings["ShowReasonsTo"] == null)
                return false;
            else
            {
                string[] allowedUserTypes = ConfigurationSettings.AppSettings["ShowReasonsTo"].Split(',');

                foreach (string allowedUserType in allowedUserTypes)
                {
                    if (allowedUserType == userType)
                        return true;
                }
                return false;
            }
        }
        private void SaveReasonsOfSubStandard(string ReasonOfSubStandard, string proposal)
        {
            if (ConfigurationSettings.AppSettings["CreateReasons"] == null || ConfigurationSettings.AppSettings["CreateReasons"] != "Y")
                return;

            //TODO:Delete All Previous Reasons of Proposal
            SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
            pm.puts("@NP1_PROPOSAL", proposal, DbType.String);

            DB.executeDML("DELETE LNRP_REFEREDPROPOSAL WHERE NP1_PROPOSAL=? ", pm);

            string[] reasons = ReasonOfSubStandard.Split(',');
            Array.Sort(reasons);
            string prevReason = "";
            foreach (string reason in reasons)
            {
                if (prevReason != reason)
                {
                    //TODO:Insert Reason of Proposal
                    pm = new SHMA.Enterprise.Data.ParameterCollection();
                    pm.puts("@NP1_PROPOSAL", proposal, DbType.String);
                    pm.puts("@RRS_REASONCODE", reason, DbType.String);

                    DB.executeDML("insert into LNRP_REFEREDPROPOSAL (NP1_PROPOSAL, RRS_REASONCODE) values (?, ?)", pm);


                }
                prevReason = reason;

            }
            if (DB.isInTransaction())
            {
                DB.CommitTransaction();
                DB.BeginTransaction();
            }
        }
        private bool evaluateOccupation()
        {
            bool evalResult = true;
            string[] products = null;
            if (productOccupation == null)
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["AllowedCategory"] != null)
                {
                    productOccupation = System.Configuration.ConfigurationSettings.AppSettings["AllowedCategory"];
                }
                else
                    productOccupation = "";
            }

            if (productOccupation != null && productOccupation != "")
            {
                products = productOccupation.Split(';');
            }

            if (products != null && products.Length >= 1)
            {
                string categoryQry = "SELECT NPR.PPR_PRODCD, CCL_CATEGORYCD FROM LNPH_PHOLDER NPH INNER JOIN LNU1_UNDERWRITI NU1 " +
                    "ON NPH.NPH_CODE = NU1.NPH_CODE AND NPH.NPH_LIFE = 'D' AND NU1.NP1_PROPOSAL = '" + Session["NP1_PROPOSAL"].ToString() + "'" +
                    "INNER JOIN LNPR_PRODUCT NPR ON NU1.NP1_PROPOSAL = NPR.NP1_PROPOSAL AND NPR.NP2_SETNO = 1 AND NPR.PPR_PRODCD IN " +
                    " (SELECT PPR_PRODCD FROM LPPR_PRODUCT L WHERE L.PPR_BASRIDR='B') ";

                rowset rs = DB.executeQuery(categoryQry);
                if (rs.next() && rs.getObject("CCL_CATEGORYCD") != null)
                {
                    string productCode = rs.getObject("PPR_PRODCD").ToString();
                    string categoryCode = rs.getObject("CCL_CATEGORYCD").ToString();
                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i].IndexOf(productCode) >= 0 && products[i].IndexOf(categoryCode) >= 0)
                        {
                            evalResult = true;
                            break;
                        }
                        else
                            evalResult = false;
                    }

                }
                else throw new Exception("Occupational Category not Selected");
            }
            return evalResult;
        }
        private static string productOccupation = null;


        //Change date (upto 28) if date is 29,30 or 31
        public void setCommencementDate()
        {
            /*rowset rs = DB.executeQuery("select  NP1_PROPDATE, NP2_COMMENDATE from LNP1_POLICYMASTR WHERE NP1_PROPOSAL='" + Convert.ToString(Session["NP1_PROPOSAL"]) +"'");
			if(rs.next())
			{
				string propDate = Convert.ToString(rs.getDate("NP1_PROPDATE"));
				string commDate = Convert.ToString(rs.getDate("NP2_COMMENDATE"));
				if(propDate != commDate)
				{
					if(Convert.ToString(Session["s_CURR_SYSDATE"]) != Convert.ToString(Session["s_COMM_DATE"]))
					{
						lblPolicyStatus.Text="Commencement Date would be " + Convert.ToString(Session["s_COMM_DATE"]);
					}
				}
			}*/
            /*string Date =Convert.ToString(Session["s_CURR_SYSDATE"]);
			string str = null;
			string[] DateArr = null;
			str = Date;
			char[] splitchar = { '/' };
			DateArr = str.Split(splitchar);

			int intCommencDateLimit = ace.clsIlasUtility.getMaximumCommencementDate();
			int intCurrentDate = Convert.ToInt16(DateArr[0]);

			//if(DateArr[0]=="29" ||DateArr[0]=="30"||DateArr[0]=="31")
			if(intCurrentDate > intCommencDateLimit)
			{			
				string DateParse = Convert.ToString(intCommencDateLimit) + "/" + DateArr[1] + "/" + DateArr[2];
				lblPolicyStatus.Text="Commencement Date would be " + DateParse;
			}*/
        }

        private DateTime getCommencementDate()
        {
            return Convert.ToDateTime(Session["s_COMM_DATE"]);
            /*
			string CurrentDate =Convert.ToString(Session["s_CURR_SYSDATE"]);
			string str = null;
			string[] DateArr = null;
			str = CurrentDate;
			char[] splitchar = { '/' };
			DateArr = str.Split(splitchar);

			int intCommencDateLimit = ace.clsIlasUtility.getMaximumCommencementDate();
			int intCurrentDate = Convert.ToInt16(DateArr[0]);

			if(intCurrentDate > intCommencDateLimit)
			{
				return new DateTime(Convert.ToInt16(DateArr[2]),Convert.ToInt16(DateArr[1]), intCommencDateLimit);
			}
			else
			{
				return new DateTime(Convert.ToInt16(DateArr[2]),Convert.ToInt16(DateArr[1]), intCurrentDate);
			}
			*/
        }

        sealed protected override void BindInputData(DataHolder dataHolder)
        {

            //string paymenttype=Convert.ToString(Session["paymenttype"]);
            //if(paymenttype!="")
            //{
            //	ddlPayemntType.SelectedValue=paymenttype;
            //}

            //TODO: SESSION SETTING in behavior 
            //***********************CUSTOM CODE ***********************/
            //<<<<<<here  ViewInitialState();
            //***********************CUSTOM CODE ***********************/
            //HERE DECISION  FIELD <<<<<<<<<<<<<<<<<<<<<<<<<<<<


            /////////scenario();

            //			rowset rowLNPR_PRODUCTS = DB.executeQuery("SELECT sum(b.npr_premium) ,a.np2_substandar FROM LNP2_POLICYMASTR a "+
            //				" inner join lnpr_product b "+
            //				" on b.np1_proposal=a.np1_proposal "+
            //				" WHERE B.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'"+ 
            //				" group by a.np2_substandar ");
            //			
            //			if (rowLNPR_PRODUCTS.next())
            //			{
            //							
            //				string Medical_value=rowLNPR_PRODUCTS.getString(2);
            //				
            //				if(Medical_value=="Y")
            //				{
            //					IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
            //					ddlCDC_CODE.DataSource = LCDC_DECISIONReader2 ;
            //					ddlCDC_CODE.DataBind();
            //					
            //					ddlCDC_CODE.SelectedValue=DECLINED;
            //					//ddlCDC_CODE.Enabled=false;
            //
            //					LCDC_DECISIONReader2.Close();
            //					DecisionDescription.Text = SUBSTANDARD;
            //					DecisionDescription.Visible=true;
            //					DecisionDescription.ForeColor=System.Drawing.Color.Red;
            //				
            //
            //				}
            //				else if(Medical_value==null)
            //				{
            //					IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
            //					ddlCDC_CODE.DataSource =null ;
            //					ddlCDC_CODE.DataBind();
            //					LCDC_DECISIONReader2.Close();
            //
            //					DecisionDescription.Text="Please Auto Under Write";
            //					DecisionDescription.Visible=true;
            //					DecisionDescription.ForeColor=System.Drawing.Color.DarkOrange;
            //				
            //
            //					
            //				}
            //
            //			}



            IDataReader LCRE_REASONReader2 = LCRE_REASONDB.GetDDL_PolicyAcceptance_CRE_REASCODE_RO(); ;
            ddlCRE_REASCODE.DataSource = LCRE_REASONReader2;
            ddlCRE_REASCODE.DataBind();
            LCRE_REASONReader2.Close();

            IDataReader NP1_PAYMENTMETReader2 = LCSD_SYSTEMDTLDB.GetDDL_PolicyAcceptance_NP1_PAYMENTMET_RO();
            ddlNP1_PAYMENTMET.DataSource = NP1_PAYMENTMETReader2;
            ddlNP1_PAYMENTMET.DataBind();
            NP1_PAYMENTMETReader2.Close();



            txtNP2_COMMENDATE.Enabled = false;
            /****** ############### ********/



            _lastEvent.Text = "New";

            FindAndSelectCurrentRecord();

            HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
            FooterScript.Text += EnvHelper.Parse("getField(\"NP1_DESCIONDATE\").value=sysDate;");

            ddlCDC_CODE.Attributes.Add("onchange", "setAcceptanceFields(this);");
            txtNP1_CCNUMBER.Attributes.Add("onblur", "LostFocus_CreditCardNumber(this);");
            txtNP1_ACCOUNTNO.Attributes.Add("onblur", "LostFocus_AccountNumber(this);");
            txtNP1_CCEXPIRY.Attributes.Add("onblur", "LostFocus_CreditCardExpiry(this);");


            //RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            //***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("") ? "0" : AllowedProcess));

            //Change date (upto 28) if date is 29,30 or 31
            setCommencementDate();



        }
        #endregion

        #region Major methods of the final step
        protected override void ValidateRequest()
        {
            base.ValidateRequest();
            foreach (string key in LNPH_PHOLDER.PrimaryKeys)
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
            if (Session["POSTED"] != null)
            {
                Session["POSTED"] = null;
                return;
            }

            SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
            columnNameValue = new NameValueCollection();
            SaveTransaction = false;
            shgn.SHGNCommand entityClass = new ace.POLICY_ACCEPTANCE();
            entityClass.setNameValueCollection(columnNameValue);
            LNCM_COMMENTS cmt = new LNCM_COMMENTS(dataHolder);
            bool blnPosted = false;


            SHSM_SecurityPermission security;
            switch ((EnumControlArgs)ControlArgs[0])
            {
                case (EnumControlArgs.Update):
                    //Donot allow Re-posting the page
                    ace.POLICY_ACCEPTANCE obj = new ace.POLICY_ACCEPTANCE();
                    if (obj.CheckIfPolicyApproved(txtNP1_PROPOSAL.Text))
                    {
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("parent.parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL', 'Proposal/Policy already been posted.');");
                        Response.Write("</script>");
                        return;
                    }

                    bool clickvalidate = ValidateClick();

                    if (clickvalidate)
                    {

                        DB.BeginTransaction();
                        SaveTransaction = true;
                        dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);

                        columnNameValue.Add("NP1_PROPOSAL", txtNP1_PROPOSAL.Text.Trim() == "" ? null : txtNP1_PROPOSAL.Text);
                        columnNameValue.Add("CDC_CODE", ddlCDC_CODE.SelectedValue.Trim() == "" ? null : ddlCDC_CODE.SelectedValue);
                        columnNameValue.Add("CRE_REASCODE", ddlCRE_REASCODE.SelectedValue.Trim() == "" ? null : ddlCRE_REASCODE.SelectedValue);
                        columnNameValue.Add("NP1_DECISIONNOTE", txtNP1_DECISIONNOTE.Text.Trim() == "" ? null : txtNP1_DECISIONNOTE.Text);
                        columnNameValue.Add("NP1_POLICYNO", txtNP1_POLICYNO.Text.Trim() == "" ? null : txtNP1_POLICYNO.Text);
                        columnNameValue.Add("NP1_ISSUEDATE", txtNP1_ISSUEDATE.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_ISSUEDATE.Text));

                        columnNameValue.Add("NP1_PAYMENTMET", ddlNP1_PAYMENTMET.SelectedValue.Trim() == "" ? null : (ddlNP1_PAYMENTMET.SelectedValue));
                        columnNameValue.Add("NP1_CCEXPIRY", txtNP1_CCEXPIRY.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_CCEXPIRY.Text));
                        columnNameValue.Add("NP1_CCNUMBER", txtNP1_CCNUMBER.Text.Trim() == "" ? null : (txtNP1_CCNUMBER.Text));
                        columnNameValue.Add("NP1_ACCOUNTNO", txtNP1_ACCOUNTNO.Text.Trim() == "" ? null : (txtNP1_ACCOUNTNO.Text));

                        //columnNameValue.Add("NP1_DEFFERPERIOD",txtNP1_DEFFERPERIOD.Text.Trim()==""?null:(object)double.Parse(txtNP1_DEFFERPERIOD.Text));
                        //columnNameValue.Add("NP1_DESCIONDATE",txtNP1_DESCIONDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP1_DESCIONDATE.Text));
                        columnNameValue.Add("NP1_TOTDIFPREM", txtNP1_AMOUNTTRANSFERED.Text.Trim() == "" ? null : (txtNP1_AMOUNTTRANSFERED.Text));
                        columnNameValue.Add("NP1_ACCOUNTNAME", txtNP1_TRANSFERSLIP.Text.Trim() == "" ? null : (txtNP1_TRANSFERSLIP.Text));

                        if (ace.ValidationUtility.isPostingFound())
                        {
                            //Need to remove two Levels BM (Compliance Level) and Collection File uploading (CP User File uploading only as we are doing in FWBL) 
                            //from UBL, BAL & FWBL
                            string bopsFlowCode = ConfigurationSettings.AppSettings["BOPSFlow"].ToString();
                            string companyFlowCode = ConfigurationSettings.AppSettings["CompanyFlow"].ToString();
                            if (bopsFlowCode.Contains(SessionObject.GetString("s_CCD_CODE"))  && SessionObject.GetString("s_CCH_CODE") == "2")
                            {
                                if (DecisionValues.Text == SUBSTANDARD)
                                {
                                    columnNameValue.Add("NP1_SELECTED", "");
                                    cmt.AddComentsInTable(txtNP1_PROPOSAL.Text, "Refered", "");
                                }
                                else
                                {
                                    columnNameValue.Add("NP1_SELECTED", "R");
                                    cmt.AddComentsInTable(txtNP1_PROPOSAL.Text, "Posted", "R");
                                }
                            }
                            else if (SessionObject.GetString("s_CCH_CODE") == "2" && companyFlowCode.Contains(SessionObject.GetString("s_CCD_CODE")))
                            {
                                if (DecisionValues.Text == SUBSTANDARD)
                                {
                                    columnNameValue.Add("NP1_SELECTED", "");
                                    cmt.AddComentsInTable(txtNP1_PROPOSAL.Text, "Refered", "");
                                }
                                else
                                {
                                    columnNameValue.Add("NP1_SELECTED", "C");
                                    cmt.AddComentsInTable(txtNP1_PROPOSAL.Text, "Posted", "C");
                                }
                            }
                            else
                            {
                                columnNameValue.Add("NP1_SELECTED", "P");
                                cmt.AddComentsInTable(txtNP1_PROPOSAL.Text, "Posted", "P");
                            }
                        }
                        //						else if(status.Equals("Y"))
                        //						{
                        //							LNP1_POLICYMASTRDB.MarkStatus(proposal,"R");							
                        //						}


                        //correspondance column
                        string corres_value = "";
                        if (chk_corres.Checked == true)
                        {
                            corres_value = "N";
                        }
                        else
                        {
                            corres_value = "Y";

                        }
                        columnNameValue.Add("NP1_NOCORRESPE", corres_value);

                        security = new SHSM_SecurityPermission(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");

                        if (security.UpdateAllowed)
                        {
                            //Change For New Posting flag
                            //entityClass.fsoperationBeforeUpdate();

                            //Set Flag either Policy will be issue for Sub Standard or not
                            bool PolicyForSubStandard = false;
                            if (DecisionValues.Text == SUBSTANDARD)
                            {
                                PolicyForSubStandard = ace.ValidationUtility.isPolicyNumberNeedForSubStandard();
                            }

                            //Generate Policy Number
                            if (txtNP1_POLICYNO.Text.Trim() == "" && (ddlCDC_CODE.SelectedValue.Trim() == APPROVED || PolicyForSubStandard == true))
                            {
                                if (ace.ValidationUtility.genPolicyBeforeTransferToIlas())
                                {
                                    try
                                    {
                                        /*
										ProcedureAdapter cs = new ProcedureAdapter("GENENRATE_POLICYNO_CALL");
										cs.Set("p_proposal", OleDbType.VarChar, Convert.ToString(Session["NP1_PROPOSAL"]));
										cs.RegisetrOutParameter("policyNumber", OleDbType.VarChar, 1000 );
										cs.Execute();
										string policy_Number = Convert.ToString(cs.Get("policyNumber"));
										*/

                                        string policy_Number = POLICY_ACCEPTANCE.generatePolicyNumber(Convert.ToString(Session["NP1_PROPOSAL"]));
                                        if (policy_Number != null)
                                        {
                                            if (policy_Number.Trim().Length > 0)
                                            {
                                                txtNP1_POLICYNO.Text = policy_Number;
                                                columnNameValue["NP1_POLICYNO"] = policy_Number;
                                                columnNameValue["NP1_ISSUEDATE"] = Convert.ToString(Session["s_CURR_SYSDATE"]);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
                                        throw new ProcessException(ex.Message);
                                    }
                                }
                            }
                            new LNP1_POLICYMASTR(dataHolder).Update(Utilities.File2EntityID(this.ToString()), columnNameValue);
                            dataHolder.Update(DB.Transaction);

                            if (DecisionValues.Text == SUBSTANDARD)
                            {
                                entityClass.fsoperationAfterUpdate();
                            }

                            //Get Assigned Policy Number (If it is generated by this method or entityclass AfterUpdate method)
                            string qryPolicyMaster = "SELECT NP1_POLICYNO, NP1_ISSUEDATE, NP1_SELECTED FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "' AND NVL(NP1_POLICYNO,'U')<>'U' ";
                            rowset rsPolicyMasterInfo = DB.executeQuery(qryPolicyMaster);
                            if (rsPolicyMasterInfo.next())
                            {
                                txtNP1_POLICYNO.Text = rsPolicyMasterInfo.getObject("NP1_POLICYNO") == null ? "" : rsPolicyMasterInfo.getString("NP1_POLICYNO");
                                txtNP1_ISSUEDATE.Text = rsPolicyMasterInfo.getObject("NP1_ISSUEDATE") == null ? "" : (rsPolicyMasterInfo.getDate("NP1_ISSUEDATE")).ToShortDateString();
                                columnNameValue["NP1_POLICYNO"] = txtNP1_POLICYNO.Text;
                                columnNameValue["NP1_ISSUEDATE"] = Convert.ToString(Session["s_CURR_SYSDATE"]);

                                if (rsPolicyMasterInfo.getString("NP1_SELECTED").ToUpper() == "Y")
                                {
                                    blnPosted = true;

                                    //************* Activity Log *************//			
                                    Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_POSTED);
                                }

                            }

                            auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNP1_POLICYMASTR");
                        }
                        else
                        {
                            PrintMessage("You are not autherized to Update.");
                        }

                        if (ddlCDC_CODE.SelectedValue.Trim() == APPROVED)
                        {
                            ddlCDC_CODE.Enabled = false;
                            txtNP1_POLICYNO.Enabled = false;
                            txtNP1_ISSUEDATE.Enabled = false;
                            txtNP1_DECISIONNOTE.Enabled = false;
                            txtNP1_CCNUMBER.Enabled = false;
                            txtNP1_ACCOUNTNO.Enabled = false;
                            txtNP1_CCEXPIRY.Enabled = false;

                            //Proper message should be show
                            if (blnPosted == true && txtNP1_POLICYNO.Text.Length > 0)
                            {
                                //lblPolicyStatus.Text="Proposal has been approved and policy issued successfully" ;
                                lblPolicyStatus.Text = "Proposal has been posted and Policy issued successfully";
                                FooterScript.Text += " posted(); ";

                            }
                            else if (blnPosted == true && txtNP1_POLICYNO.Text.Length == 0)
                            {
                                lblPolicyStatus.Text = "Proposal has been Posted.";
                                FooterScript.Text += " posted(); ";

                            }
                            else if (ace.ValidationUtility.isPostingFound())
                            {
                                lblPolicyStatus.Text = "Proposal has been Posted for verification.";
                                FooterScript.Text += " posted(); ";

                            }
                            else
                            {
                                lblPolicyStatus.Text = "Proposal not Posted.";
                            }

                        }
                        else if (ddlCDC_CODE.SelectedValue.Trim() == DECLINED)
                        {
                            ddlCDC_CODE.Enabled = false;
                            lblPolicyStatus.Text = "Proposal has been approved for further processing by Head Office";

                            FooterScript.Text += " posted(); ";

                            //ddlCRE_REASCODE.Enabled=false;
                            //txtNP1_DECISIONNOTE.Enabled=false;
                        }
                    }
                    else
                    {
                        //Break Code
                        throw new ProcessException("Please validate first!");

                    }

                    break;
                /*case (EnumControlArgs.Delete):
                    DB.BeginTransaction();
                    SaveTransaction = true;
                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("CDC_CODE",ddlCDC_CODE.SelectedValue.Trim()==""?null:ddlCDC_CODE.SelectedValue);
                    columnNameValue.Add("CRE_REASCODE",ddlCRE_REASCODE.SelectedValue.Trim()==""?null:ddlCRE_REASCODE.SelectedValue);
                    columnNameValue.Add("NP1_DECISIONNOTE",txtNP1_DECISIONNOTE.Text.Trim()==""?null:txtNP1_DECISIONNOTE.Text);
                    columnNameValue.Add("NP1_POLICYNO",txtNP1_POLICYNO.Text.Trim()==""?null:txtNP1_POLICYNO.Text);
                    columnNameValue.Add("NP1_ISSUEDATE",txtNP1_ISSUEDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP1_ISSUEDATE.Text));
                    //columnNameValue.Add("NP1_DEFFERPERIOD",txtNP1_DEFFERPERIOD.Text.Trim()==""?null:(object)double.Parse(txtNP1_DEFFERPERIOD.Text));
                    //columnNameValue.Add("NP1_DESCIONDATE",txtNP1_DESCIONDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP1_DESCIONDATE.Text));


                    security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
                    if (security.DeleteAllowed)
                    {
                        entityClass.fsoperationBeforeDelete();

                        new LNPH_PHOLDER(dataHolder).Delete(columnNameValue);

                        dataHolder.Update(DB.Transaction);
                        entityClass.fsoperationAfterDelete();

                        auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNP1_POLICYMASTR");
                        //PrintMessage("Record has been deleted");				
                    }
                    else
                    {
                        PrintMessage("You are not autherized to Delete.");
                    }
                    break;*/
                /*case (EnumControlArgs.Process):						
                    DB.BeginTransaction();
                    SaveTransaction = true;
                    dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
                    columnNameValue.Add("CDC_CODE",ddlCDC_CODE.SelectedValue.Trim()==""?null:ddlCDC_CODE.SelectedValue);
                    columnNameValue.Add("CRE_REASCODE",ddlCRE_REASCODE.SelectedValue.Trim()==""?null:ddlCRE_REASCODE.SelectedValue);
                    columnNameValue.Add("NP1_DECISIONNOTE",txtNP1_DECISIONNOTE.Text.Trim()==""?null:txtNP1_DECISIONNOTE.Text);
                    columnNameValue.Add("NP1_POLICYNO",txtNP1_POLICYNO.Text.Trim()==""?null:txtNP1_POLICYNO.Text);
                    columnNameValue.Add("NP1_ISSUEDATE",txtNP1_ISSUEDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP1_ISSUEDATE.Text));
                    //columnNameValue.Add("NP1_DEFFERPERIOD",txtNP1_DEFFERPERIOD.Text.Trim()==""?null:(object)double.Parse(txtNP1_DEFFERPERIOD.Text));
                    //columnNameValue.Add("NP1_DESCIONDATE",txtNP1_DESCIONDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNP1_DESCIONDATE.Text));


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
                    if (result.Length>0)
                        PrintMessage(result);
                    break;*/
                case EnumControlArgs.Pager:
                    //if(!LNP1_POLICYMASTRDB.PolicyValidated(SessionObject.GetString("NP1_PROPOSAL")))
                    ValidatingWholeCase();
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

            HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
            FooterScript.Text += EnvHelper.Parse("getField(\"NP1_DESCIONDATE\").value=sysDate;");


        }
        #endregion

        #region Events
        protected void _CustomEvent_ServerClick(object sender, System.EventArgs e)
        {
            ControlArgs = new object[1];
            switch (_CustomEventVal.Value)
            {
                case "Update":
                    if (ddlCDC_CODE.SelectedValue.Trim() == APPROVED)
                    {
                        //Check Medical Rejection
                        rowset rowLNPR_PRODUCT = DB.executeQuery("SELECT sum(b.npr_premium) ,a.np2_substandar FROM LNP2_POLICYMASTR a " +
                            " inner join lnpr_product b " +
                            " on b.np1_proposal=a.np1_proposal " +
                            " WHERE B.NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'" +
                            " group by a.np2_substandar ");
                        if (rowLNPR_PRODUCT.next())
                        {

                            string Medical_value = rowLNPR_PRODUCT.getString(2);
                            Double NPR_PREMIUM = rowLNPR_PRODUCT.getDouble(1);
                            int condition = 0;

                            if (Medical_value == "Y")
                            {
                                condition += 1;
                                PrintMessage("Medical Case Has Been Rejected");


                            }
                            else if (Medical_value == null)
                            {
                                condition += 1;
                                PrintMessage("Please Generate UnderWritee From Medical");

                            }
                            // 
                            if (NPR_PREMIUM <= Convert.ToDouble(0))
                            {
                                condition += 1;
                                PrintMessage("Please Caculate Premium From Plan Rider");
                            }

                            //Final checking 
                            if (condition == 0)
                            {
                                ControlArgs[0] = EnumControlArgs.Update;
                                CustomDoControl();

                            }
                            rowLNPR_PRODUCT.close();
                        }
                        else
                        {
                            PrintMessage("Please Caculate Premium From Plan Rider");

                        }
                        //					
                    }
                    else if (ddlCDC_CODE.SelectedValue.Trim() == DECLINED)
                    {
                        ControlArgs[0] = EnumControlArgs.Update;
                        CustomDoControl();

                    }
                    else if (ddlCDC_CODE.SelectedValue.Trim() == POSTPONED)
                    {
                        ControlArgs[0] = EnumControlArgs.Update;
                        CustomDoControl();

                    }

                    //ControlArgs[0]=EnumControlArgs.Update;
                    //CustomDoControl();	

                    break;
                case "Save":
                    if (ddlCDC_CODE.SelectedValue.Trim() == APPROVED)
                    {
                        //Check Medical Rejection
                        rowset rowLNPR_PRODUCT = DB.executeQuery("SELECT np2_substandar FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + Session["np1_proposal"].ToString() + "'");
                        if (rowLNPR_PRODUCT.next())
                        {
                            string Medical_value = rowLNPR_PRODUCT.getString(1);

                            if (Medical_value.ToString() == "" || Medical_value.ToString() == null)
                            {

                                ControlArgs[0] = EnumControlArgs.Save;
                                CustomDoControl();
                            }
                            else
                            {
                                //txt_approved.Text="R";
                                PrintMessage("Medical case has been rejected");
                            }
                        }

                        else
                        {
                            PrintMessage("Please caculate premium from plan rider");

                        }
                    }
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
                SessionObject.Set("CDC_CODE", ddlCDC_CODE.SelectedValue);
                SessionObject.Set("CRE_REASCODE", ddlCRE_REASCODE.SelectedValue);
                SessionObject.Set("NP1_PAYMENTMET", ddlNP1_PAYMENTMET.SelectedValue);
                SessionObject.Set("NP1_DECISIONNOTE", txtNP1_DECISIONNOTE.Text);
                SessionObject.Set("NP1_PROPOSAL", txtNP1_PROPOSAL.Text);
                SessionObject.Set("NP1_POLICYNO", txtNP1_POLICYNO.Text);
                //SessionObject.Set("NP1_DEFFERPERIOD",txtNP1_DEFFERPERIOD.Text);
                //SessionObject.Set("NP1_DESCIONDATE",txtNP1_DESCIONDATE.Text);
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
            //ddlCDC_CODE.ClearSelection();
            ddlCRE_REASCODE.ClearSelection();
            ddlNP1_PAYMENTMET.ClearSelection();
            txtNP1_DECISIONNOTE.Text = "";
            //txtNP1_PROPOSAL.Enabled = true;
            //txtNP1_PROPOSAL.Text="";
            txtNP1_POLICYNO.Text = "";
            //txtNP1_DEFFERPERIOD.Text="";
            //txtNP1_DESCIONDATE.Text="";
        }

        protected void ShowData(DataRow objRow)
        {
            //Checking Medical Rejection//

            string Medical_value = null;
            rowset rowLNPR_PRODUCT = DB.executeQuery("SELECT sum(b.npr_premium) ,a.np2_substandar FROM LNP2_POLICYMASTR a " +
                " inner join lnpr_product b " +
                " on b.np1_proposal=a.np1_proposal " +
                " WHERE B.NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'" +
                " group by a.np2_substandar ");

            if (rowLNPR_PRODUCT.next())
            {
                Medical_value = rowLNPR_PRODUCT.getString(2);
            }

            if (Medical_value == "N")//Approved
            {
                IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
                ddlCDC_CODE.DataSource = LCDC_DECISIONReader2;
                ddlCDC_CODE.DataBind();

                ddlCDC_CODE.SelectedValue = APPROVED;
                //ddlCDC_CODE.Enabled=false;

                LCDC_DECISIONReader2.Close();
                DecisionValues.Text = STANDARD;
                DecisionDescription.Text = ace.ValidationUtility.getApprovePhrase();//STANDARD;
                DecisionDescription.Visible = true;
                DecisionDescription.ForeColor = System.Drawing.Color.Green;
            }
            else if (Medical_value == "Y")//Declined
            {
                IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
                ddlCDC_CODE.DataSource = LCDC_DECISIONReader2;
                ddlCDC_CODE.DataBind();
                ddlCDC_CODE.SelectedValue = DECLINED;
                //ddlCDC_CODE.Enabled=false;

                LCDC_DECISIONReader2.Close();
                DecisionValues.Text = SUBSTANDARD;
                DecisionDescription.Text = ace.ValidationUtility.getDeclinedPhrase();//SUBSTANDARD
                DecisionDescription.Visible = true;
                DecisionDescription.ForeColor = System.Drawing.Color.Red;
            }
            else if (Medical_value == "R")//Refer to company
            {
                IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
                ddlCDC_CODE.DataSource = LCDC_DECISIONReader2;
                ddlCDC_CODE.DataBind();
                ddlCDC_CODE.SelectedValue = POSTPONED;
                //ddlCDC_CODE.Enabled=false;

                LCDC_DECISIONReader2.Close();
                DecisionValues.Text = REFER;
                DecisionDescription.Text = ace.ValidationUtility.getPostponedPhrase();//SUBSTANDARD
                DecisionDescription.Visible = true;
                DecisionDescription.ForeColor = System.Drawing.Color.Red;
            }
            else if (Medical_value == null)
            {
                IDataReader LCDC_DECISIONReader2 = LCDC_DECISIONDB.GetDDL_PolicyAcceptance_CDC_CODE_RO();
                ddlCDC_CODE.DataSource = null;
                ddlCDC_CODE.DataBind();
                LCDC_DECISIONReader2.Close();

                DecisionDescription.Text = "Please Validate...";
                DecisionDescription.Visible = true;
                DecisionDescription.ForeColor = System.Drawing.Color.DarkOrange;
            }

            RefreshDataFields();



            ddlCRE_REASCODE.ClearSelection();
            ListItem item7 = ddlCRE_REASCODE.Items.FindByValue(objRow["CRE_REASCODE"].ToString());
            if (item7 != null)
            {
                item7.Selected = true;
            }

            string PPR_PRODCD = SessionObject.GetString("PPR_PRODCD");
            string NP1_PROPOSAL = SessionObject.GetString("NP1_PROPOSAL");
            double NP2_SETNO = double.Parse(SessionObject.GetString("NP2_SETNO"));

            //rowset LNP2_POLICYMASTR = DB.executeQuery("SELECT sum(NVL(NPR_PREMIUM,0))+ sum(nvl(NPR_LOADING,0)) AS NPR_TOTPREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+NP1_PROPOSAL+"'");
            //rowset LNP2_POLICYMASTR = DB.executeQuery("SELECT sum(NVL(NPR_PREMIUM,0)) + sum(nvl(NPR_LOADING,0)) +  MAX(NVL(B.NLO_AMOUNT,0)) AS NPR_TOTPREMIUM FROM LNPR_PRODUCT A LEFT OUTER JOIN LNLO_LOADING B ON A.NP1_PROPOSAL = B.NP1_PROPOSAL WHERE A.NP1_PROPOSAL='"+NP1_PROPOSAL+"'");
            rowset LNP2_POLICYMASTR = DB.executeQuery("SELECT sum(NVL(NPR_PREMIUM,0)) + sum(nvl(NPR_LOADING,0)) AS NPR_TOTPREMIUM FROM LNPR_PRODUCT A WHERE A.NP1_PROPOSAL='" + NP1_PROPOSAL + "'");
            if (LNP2_POLICYMASTR.next())
            {
                txtNP1_AMOUNTOFPREMIUM.Text = LNP2_POLICYMASTR.getDouble("NPR_TOTPREMIUM").ToString();
                txtNP1_AMOUNTTRANSFERED.Text = LNP2_POLICYMASTR.getDouble("NPR_TOTPREMIUM").ToString();

            }

            txtNP1_DECISIONNOTE.Text = objRow["NP1_DECISIONNOTE"].ToString();
            txtNP1_PROPOSAL.Text = objRow["NP1_PROPOSAL"].ToString();

            ShowReasons(txtNP1_PROPOSAL.Text);

            //ddlPayemntType.ClearSelection();
            //ddlPayemntType.SelectedValue = objRow["NP1_PAYMENTMET"]==DBNull.Value?"":(objRow["NP1_PAYMENTMET"]).ToString();

            ddlNP1_PAYMENTMET.ClearSelection();
            ListItem item8 = ddlNP1_PAYMENTMET.Items.FindByValue(objRow["NP1_PAYMENTMET"].ToString());
            if (item8 != null)
            {
                item8.Selected = true;
            }

            txtNP1_ACCOUNTNO.Text = objRow["NP1_ACCOUNTNO"] == DBNull.Value ? "" : (objRow["NP1_ACCOUNTNO"]).ToString();
            txtNP1_CCNUMBER.Text = objRow["NP1_CCNUMBER"] == DBNull.Value ? "" : (objRow["NP1_CCNUMBER"]).ToString();
            txtNP1_CCEXPIRY.Text = objRow["NP1_CCEXPIRY"] == DBNull.Value ? "" : ((DateTime)objRow["NP1_CCEXPIRY"]).ToShortDateString();

            if (Medical_value != null && Medical_value.Trim().Length > 0)
            {
                ddlNP1_PAYMENTMET.Enabled = false;
                txtNP1_ACCOUNTNO.Enabled = false;
                txtNP1_CCNUMBER.Enabled = false;
                txtNP1_CCEXPIRY.Enabled = false;
            }



            //string paymenttype=Convert.ToString(Session["paymenttype"]);
            //if(paymenttype!="")
            //{
            //	ddlPayemntType.SelectedValue=paymenttype;
            //}

            txtNP1_PROPOSAL.Enabled = false;


            //string qryPolicyMaster = "SELECT NP1_POLICYNO, NP1_ISSUEDATE, NP2_COMMENDATE FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='" + Session["NP1_PROPOSAL"].ToString() + "'";
            //rowset rsPolicyMasterInfo = DB.executeQuery(qryPolicyMaster);
            //if(rsPolicyMasterInfo.next())
            //{
            //	txtNP1_POLICYNO.Text  = rsPolicyMasterInfo.getObject("NP1_POLICYNO")   == null ? "" : rsPolicyMasterInfo.getString("NP1_ISSUEDATE");
            //	txtNP1_ISSUEDATE.Text = rsPolicyMasterInfo.getObject("NP1_ISSUEDATE")  == null ? "" : (rsPolicyMasterInfo.getDate("NP1_ISSUEDATE")).ToShortDateString();
            //	txtNP2_COMMENDATE.Text= rsPolicyMasterInfo.getObject("NP2_COMMENDATE") == null ? "" : (rsPolicyMasterInfo.getDate("NP2_COMMENDATE")).ToShortDateString();
            //}

            txtNP1_POLICYNO.Text = objRow["NP1_POLICYNO"].ToString();
            txtNP1_ISSUEDATE.Text = objRow["NP1_ISSUEDATE"] == DBNull.Value ? "" : ((DateTime)objRow["NP1_ISSUEDATE"]).ToShortDateString();
            txtNP2_COMMENDATE.Text = objRow["NP2_COMMENDATE"] == DBNull.Value ? "" : ((DateTime)objRow["NP2_COMMENDATE"]).ToShortDateString();



            txtNP2_COMMENDATE.Enabled = false;
            //txtNP1_DESCIONDATE.Text=objRow["NP1_DESCIONDATE"]==DBNull.Value?"":((DateTime)objRow["NP1_DESCIONDATE"]).ToShortDateString();
            //txtNP1_DEFFERPERIOD.Text=objRow["NP1_DEFFERPERIOD"].ToString();
            //txtNP1_AMOUNTTRANSFERED.Text=objRow["NP1_TOTDIFPREM"].ToString();
            txtNP1_TRANSFERSLIP.Text = objRow["NP1_ACCOUNTNAME"].ToString();
            //Approved
            if (ddlCDC_CODE.SelectedValue == APPROVED && txtNP1_POLICYNO.Text.Trim().Length > 0 && txtNP1_ISSUEDATE.Text.Trim().Length > 0)
            {
                ddlCDC_CODE.Enabled = false;
                txtNP1_DECISIONNOTE.Enabled = false;
                txtNP1_POLICYNO.Enabled = false;
                txtNP1_ISSUEDATE.Enabled = false;
                //txtNP1_DEFFERPERIOD.Enabled=false;
                ddlCRE_REASCODE.Enabled = false;
            }
            //Declined
            else if (ddlCDC_CODE.SelectedValue == DECLINED && ddlCRE_REASCODE.SelectedValue.Trim().Length > 0)
            {
                //ddlCDC_CODE.Enabled=false;
                //txtNP1_DECISIONNOTE.Enabled=false;
                txtNP1_POLICYNO.Enabled = false;
                txtNP1_ISSUEDATE.Enabled = false;
                //txtNP1_DEFFERPERIOD.Enabled=false;
                //ddlCRE_REASCODE.Enabled=false;
            }

            //<<<<<<< here
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
            NormalEntryTableDiv.Style.Add("visibility", "hidden");
            HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
            FooterScript.Text += EnvHelper.Parse("getField(\"NP1_DESCIONDATE\").value=sysDate;");
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
            //this is to make all event for update to edit so delete and update buttons are enabled
            if (lastEvent.Equals("Update"))
                _lastEvent.Text = "Edit";

        }

        private void ClearSession()
        {
            SessionObject.Remove("CDC_CODE");
            SessionObject.Remove("CRE_REASCODE");
            SessionObject.Remove("NP1_PAYMENTMET");
            SessionObject.Remove("NP1_PROPOSAL");
            SessionObject.Remove("NP1_POLICYNO");
            SessionObject.Remove("NP1_DEFFERPERIOD");
        }

        #region "Validation"
        public bool ValidateClick()
        {
            return LNP1_POLICYMASTRDB.PolicyValidated(SessionObject.GetString("NP1_PROPOSAL"));
            /*string dsn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			OleDbConnection cnn = new OleDbConnection( dsn );
			OleDbConnection cnn = (OleDbConnection)DB.Connection;
				
			try
			{
				string sql = "select A.np2_substandar from lnp2_policymastr A "+
							 " INNER JOIN LNP1_POLICYMASTR B ON B.NP1_PROPOSAL=A.NP1_PROPOSAL " +
							 " INNER JOIN LNPR_PRODUCT C ON C.NP1_PROPOSAL=B.NP1_PROPOSAL " +
							 " WHERE C.PPR_PRODCD='"+Session["PPR_PRODCD"]+"' AND C.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"'";

				sql = EnvHelper.Parse (sql);
				OleDbDataAdapter da = new OleDbDataAdapter(sql, cnn);
				DataSet ds = new DataSet(); 
				da.Fill(ds, "LNDETAILS"); 
				if(Convert.ToString(ds.Tables["LNDETAILS"].Rows[0][0])!="")									
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				   return false;
			}
			finally
			{
				cnn.Close();
			}*/


        }

        private void ValidateAddress()
        {
            string errorMessage = "<script language='javascript'>alert('Correspondence Address not entered or saved');</script>";
            string qryAddress = "SELECT CCN_CTRYCD, CCT_CITYCD, CPR_PROVCD, NAD_ADDRESS1 FROM LNAD_ADDRESS WHERE NAD_ADDRESSTYP='C' AND NP1_PROPOSAL = ? ";
            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
            pc.puts("@NP1_PROPOSAL", SessionObject.Get(("NP1_PROPOSAL")));
            rowset rs = DB.executeQuery(qryAddress, pc);
            if (rs.size() <= 0)
            {
                Response.Write(errorMessage);
                error = 1;
            }
            while (rs.next())
            {
                //if(rs.getObject("CCT_CITYCD")==null || rs.getObject("CPR_PROVCD")==null || rs.getObject("NAD_ADDRESS1")==null)
                if (rs.getObject("CCN_CTRYCD") == null || rs.getObject("NAD_ADDRESS1") == null)
                {
                    Response.Write(errorMessage);
                    error = 1;
                }
            }

        }

        private void ValidateAccountNo()
        {
            if (env.getAttribute("s_CCH_CODE").ToString().Equals("2"))
            {
                string qryAddress = "SELECT NU1_ACCOUNTNO ACCOUNTNO,NU1_IBAN IBANNO FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL =? AND NU1_LIFE='F' ";
                SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                pc.puts("@NP1_PROPOSAL", SessionObject.Get(("NP1_PROPOSAL")));
                rowset rs = DB.executeQuery(qryAddress, pc);
                if (rs.size() <= 0)
                {
                    Response.Write("<script language='javascript'>alert('Personal Information Missing');</script>");
                    error = 1;
                }
                while (rs.next())
                {
                   // if (rs.getObject("ACCOUNTNO") == null || rs.getObject("ACCOUNTNO").ToString().Equals("")
                    //    && (rs.getObject("IBANNO") == null || rs.getObject("IBANNO").ToString().Trim().Equals("")))

                        if (rs.getObject("ACCOUNTNO") == null && rs.getObject("IBANNO") == null)
                        
                        {
                        Response.Write("<script language='javascript'>alert('Account Number not Entered.');</script>");
                        error = 1;
                    }
                }
            }
        }

        private void ValidateBeneficiary()
        {
            const string BY_PERCENT = "02";
            bool blnPercentExist = false;
            double totPercent = 0;

            string query = "select NBF_BASIS, NBF_PERCNTAGE, NBF_AGE, NGU_GUARDCD, NBF_BENNAME, CRL_RELEATIOCD from LNBF_BENEFICIARY where NP1_PROPOSAL=? ";
            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
            pc.puts("@NP1_PROPOSAL", SessionObject.Get(("NP1_PROPOSAL")));
            rowset rs = DB.executeQuery(query, pc);

            if (rs.size() < 1)
            {
                Response.Write("<script language='javascript'>alert('Beneficiary is not defined.');</script>");
                error = 1;
            }

            while (rs.next())
            {
                if (rs.getString("NBF_BASIS") == BY_PERCENT)
                {
                    blnPercentExist = true;
                    totPercent += rs.getDouble("NBF_PERCNTAGE");

                    //****** Check Age Limit - For an Individual ******
                    if (clsIlasUtility.isIndividual_Relation(rs.getString("CRL_RELEATIOCD")))
                    {
                        //****** Ask Guardian or not ******
                        if (clsIlasUtility.askGuardianInfo())
                        {
                            //****** Check Age Limit now ******
                            if (rs.getInt("NBF_AGE") < ace.clsIlasConstant.AGE_LIMIT)
                            {
                                if (rs.getObject("NGU_GUARDCD") == null || rs.getString("NGU_GUARDCD").Trim() == "")
                                {
                                    Response.Write("<script language='javascript'>alert('Guardian is missing for Beneficiary [" + rs.getString("NBF_BENNAME") + "]');</script>");
                                    error = 1;
                                }
                            }
                        }
                    }
                }
            }

            if (blnPercentExist == true)
            {
                if (totPercent < 100 || totPercent > 100)
                {
                    Response.Write("<script language='javascript'>alert('Total Beneficiary Percentage is " + totPercent + ". It should be 100%');</script>");
                    error = 1;
                }
            }
        }
        /*private void ValidateTSR()
		{
			string cnic="";
			if(env.getAttribute("s_NPH_IDNO")==null)
			{
				string qryIDNo = " SELECT NPH_IDNO FROM LNPH_PHOLDER PH INNER JOIN LNU1_UNDERWRITI U1 " +
					" ON PH.NPH_CODE = U1.NPH_CODE AND PH.NPH_LIFE = U1.NPH_LIFE AND PH.NPH_LIFE = 'D' " +
					" AND U1.NP1_PROPOSAL ='"+env.getAttribute("NP1_PROPOSAL").ToString()+"' ";
				
				rowset rs1=DB.executeQuery(qryIDNo);
				if(rs1.next())
				{
					cnic = rs1.getObject("NPH_IDNO").ToString();
				}
			}
			else
			{
				cnic = env.getAttribute("s_NPH_IDNO").ToString();
			}

			ace.ProcedureAdapter cs = new ace.ProcedureAdapter("TSRFORIDNO_CALL");
			cs.Set("pIDNo", OleDbType.VarChar, cnic);
			cs.RegisetrOutParameter("code", OleDbType.Numeric,1000);
			cs.Execute();
			Decimal prevSA = Convert.ToDecimal(cs.Get("code"));
			Decimal TSR=0;
			
			if(System.Configuration.ConfigurationSettings.AppSettings["TSR"]!=null)
			{
				TSR = Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings["TSR"]);
			}
			else
			{
				//throw new ProcessException("Total Sum at Risk not Defined");
				Response.Write("<script language='javascript'>alert('Total Sum at Risk not Defined');</script>"); 				
				error=1;	
			}
			
			
			Decimal currentSA=0;
			string saQry = "SELECT NPR_SUMASSURED FROM LNPR_PRODUCT WHERE NP1_PROPOSAL = '"+env.getAttribute("NP1_PROPOSAL").ToString()+"'  " + 
				"AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPPR_PRODUCT L WHERE L.PPR_BASRIDR='B')";
			rowset rs = DB.executeQuery(saQry);
			if(rs.next() && rs.getObject("NPR_SUMASSURED")!=null)
			{
				currentSA=Convert.ToDecimal(rs.getObject("NPR_SUMASSURED"));
			}

			Decimal tsrValue =Convert.ToDecimal(cs.Get("code"));
			if((currentSA+prevSA)>TSR)
			{
				//throw new ProcessException("Total Sum Assured "+ (currentSA+prevSA)  +" is more then Sum at Risk "+TSR);
				Response.Write("<script language='javascript'>alert('Total Sum Assured "+ (currentSA+prevSA) +" is more then Sum at Risk " +TSR +"');</script>");
				error=1;
			}
		}*/

        /*private void CheckMedical()
		{
			string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			
			OleDbConnection conn = new OleDbConnection(str_connString);
			try
			{
				// Put user code to initialize the page here
				conn.Open ();
				string proposal = Session["NP1_PROPOSAL"].ToString();
				string product  = Session["PPR_PRODCD"].ToString();
				int quest;
				int Answer=0;
				string Query=" select A.CQN_CODE,A.CQN_DESC,A.CQN_CONDITION FROM lcqn_questionnaire A "+
							 " INNER JOIN lpqn_questionnaire B ON B.CQN_CODE=A.CQN_CODE AND B.PPR_PRODCD='"+product+"' " +
							 " ORDER BY A.CQN_CODE";
				DataSet ds = new DataSet ();
				OleDbDataAdapter dr = new OleDbDataAdapter (Query,conn);
				dr.Fill (ds,"DD");
				int[] Mark_Rows = new int[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
				
					string Ques_Code=ds.Tables[0].Rows[i]["CQN_CODE"].ToString();
					string Ques_Condition=ds.Tables[0].Rows[i]["CQN_CONDITION"].ToString();
					
					if(Ques_Condition!="") // && Ques_Condition!="H")
					{
						string Check_Conditions=Check_Condition(Ques_Code.ToString(),Ques_Condition.ToString());
				
						if(Check_Conditions=="N")
						{
							if(i==0)
							{
								Mark_Rows[i]=-100;
							}
							else
							{
								Mark_Rows[i]=i;
							}
						}
					}
				}
				for(int j=0;j<Mark_Rows.Length;j++)
				{
					if(Mark_Rows[j]>0 || Mark_Rows[j]==-100)
					{
						if(Mark_Rows[j]==-100)
						{
							ds.Tables[0].Rows[0].Delete();
						}
						else
							ds.Tables[0].Rows[j].Delete();
					}
				}
				ds.AcceptChanges();
				if(ds.Tables[0].Rows.Count>0)
				{
					quest=ds.Tables[0].Rows.Count;
				

					//Check Saved Question
					string Query1="SELECT D.CQN_DESC,D.CQN_CONDITION,D.CQN_CODE,A.NQN_REMARKS,A.NQN_ANSWER,A.NP1_PROPOSAL FROM lnqn_questionnaire A "+
						" RIGHT OUTER JOIN lcqn_questionnaire D "+
						" ON D.CQN_CODE=A.CQN_CODE "+
						" INNER JOIN lPqn_questionnaire P ON D.CQN_CODE = P.CQN_CODE "+ 
						" AND P.PPR_PRODCD='"+ Session["PPR_PRODCD"].ToString() +"' AND NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"' ORDER BY D.CQN_CODE";   

					DataSet dss = new DataSet ();
					OleDbDataAdapter dr1 = new OleDbDataAdapter (Query1,conn);
					dr1.Fill (dss,"DATAGRID");
					
					if(dss.Tables[0].Rows.Count>0)
					{
						Answer=dss.Tables[0].Rows.Count;						
					}

					//Check Final
					if(quest!=Answer)
					{
						error=1;
						Response.Write("<script language='javascript'>alert('Medical detail is not complete');</script>"); 				
					}
				}
			}
			catch(Exception e)
			{
				//_result.Text = "alert('" + e.Message + "');";
			}
			finally
			{
				conn.Close();
			}
		}
		*/
        private void ValidateFunds()
        {
            if (ace.clsIlasUtility.askFundInfo(ace.Ace_General.getPPR_PRODCD(Convert.ToString(Session["NP1_PROPOSAL"]))))
            {
                try
                {
                    ace.LNFU_FUNDS.validateInputPercentage();
                }
                catch (Exception e)
                {
                    Response.Write("<script language='javascript'>alert('" + e.Message + "');</script>");
                    error = 1;
                }
            }
        }

        #endregion
        //Calling Procedure for Insertion.

        /*public string Check_Condition(string P_QUESTION,string P_CONDITION)
		{
			string mRtrnString="";
			if(P_CONDITION=="")
			{
				mRtrnString = "EMPTY_CONDITION";
			}
			else
			{
				ProcedureAdapter call =  new ProcedureAdapter("CHECK_LCQNCONDITION_CALL");
				call.Set("P_PROPOSAL", OleDbType.VarChar, Convert.ToString(Session["NP1_PROPOSAL"]));
				call.Set("P_PRODCD",   OleDbType.VarChar, Convert.ToString(Session["PPR_PRODCD"]));
				call.Set("P_SETNO",    OleDbType.Numeric, 1);
				call.Set("P_QUESTION", OleDbType.VarChar, P_QUESTION);
				call.Set("P_CONDITION",OleDbType.VarChar, P_CONDITION);
				call.RegisetrOutParameter("MRTRNSTRING",OleDbType.VarChar,1000);
				call.Execute();
				mRtrnString= call.Get("MRTRNSTRING").ToString();
			}
			return mRtrnString; 
		}*/

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            ControlArgs = new object[1];
            ControlArgs[0] = EnumControlArgs.Pager;
            DoControl();
        }

        private void ValidatingWholeCase()
        {
            try
            {
                string companyCodes = System.Configuration.ConfigurationSettings.AppSettings["CompanyCodes"].ToString();
                DB.BeginTransaction();
                SaveTransaction = true;

                ValidateInputDataInAcceptance();
                if (error == 0) updateOtherTablesFor();//Update Policy Master for Payment Detail
                if (error == 0) ValidatePlan();
                if (error == 0) ValidateAddress();
                if (error == 0) ValidateBeneficiary();
                if (error == 0) ValidateAccountNo();
                //if(error==0)//ValidateTSR();//This check is being stop by Zulfi Bhai, this has been convered in POLICY_ACCEPTANCE.calcTotalSumAtRisk
                //if(error==0)CheckMedical();
                if (error == 0) ValidatingMedicalDetails();
                if (Session["s_CCH_CODE"].ToString() == "2" && Session["s_CCD_CODE"].ToString() == "9")
                {
                    if (error == 0) ValidatingAssessmentDetails();
                }
                if (companyCodes.Contains(Session["s_CCD_CODE"].ToString()))
                {
                    if (error == 0) ValidatingAssignee();
                }
                //if(error==0)ValidatePremium();//This check is the part of ValidatePlan() function - 
                if (error == 0) ValidateFunds();
                if (error == 0) ValidateOther();
                if (error == 0)
                {
                    //******** Calculate Total Sum At Risk on Validaton (in banca) *******
                    double totalSAR = POLICY_ACCEPTANCE.calcTotalSumAtRisk(Session["NP1_PROPOSAL"].ToString());

                    //Default Behaviour (must be) is to call scenarioNew()
                    if (ValidationUtility.isNewValidation() == true)
                    {

                        scenarioNew();
                    }
                    else
                    {   //*****************************************************************************//
                        //**** NOTE: This method will have to discard later (used only in Adamjee) ****//
                        //*****************************************************************************//
                        scenario();
                    }
                    //To show this page again (written due to UI Problem)
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("var ppp=parent;for (i=0;i<100;i++){if (ppp.parent==null) break;ppp=ppp.parent;}ppp.setPage('shgn_gp_gp_ILUS_ET_GP_POLICYACCEPTANCE');");
                    Response.Write("</script>");
                    //DB.Transaction.Commit();
                }
                else
                {
                    //DB.Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                //DB.Transaction.Rollback();
                string error = ex.Message.Replace("'", "").Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace(Environment.NewLine, " ");
                //MessageScript.Text = "alert('" + ex.Message.Replace("\"","").Replace(Environment.NewLine,"")  + "');";
                MessageScript.Text = "alert('" + error + "');";
            }
            /*finally
			{
				//DB.TransactionEnd();
				//DB.DisConnect();
			}*/
        }

        private void ValidatingMedicalDetails()
        {
            string proposal = SessionObject.GetString("NP1_PROPOSAL");
            //int ageOfInsured_ = LNU1_UNDERWRITIDB.getAgeOfLifeAssured(proposal);
            //if (ageOfInsured_ > 12)
            //{
            System.Text.StringBuilder query_ = new System.Text.StringBuilder();
            query_.Append("select count(*) total_questions, sum(answered) answered, sum(not_answered) not_answered ");
            query_.Append("  from (select case ");
            query_.Append("                 when nqn_answer is not null then ");
            query_.Append("                  1 ");
            query_.Append("               end answered, ");
            query_.Append("               case ");
            query_.Append("                 when nqn_answer is null then ");
            query_.Append("                  1 ");
            query_.Append("               end not_answered ");
            query_.Append("          from lnqn_questionnaire ");
            query_.Append("         where np1_proposal = ? and np2_setno = 1) a ");
            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
            pc.puts("@NP1_PROPOSAL", proposal);
            rowset rs = DB.executeQuery(query_.ToString(), pc);
            if (rs.next())
            {
                if (rs.getInt("total_questions") == 0)
                {
                    Response.Write("<script language='javascript'>alert('Please make sure to answer all questions on Medical Detail Tab.')</script>");
                    error = 1;
                }
                else
                {

                    int medicalQuestionsAnswered = rs.getInt("answered");
                    int medicalQuestionsHaveToAnswered = rs.getInt("total_questions");
                    int medicalQuestionsNotAnswered = rs.getInt("not_answered");
                    string ppr_prodcd = SessionObject.GetString("PPR_PRODCD");
                    if (medicalQuestionsAnswered < medicalQuestionsHaveToAnswered)
                    {
                        Response.Write("<script language='javascript'>alert('Please complete the Medical Details :\\n\\nTotal Questions : " + medicalQuestionsHaveToAnswered + "\\nAnswered : " + medicalQuestionsAnswered + "\\nNot Answered : " + medicalQuestionsNotAnswered + "')</script>");
                        error = 1;
                        return;
                    }
                    rowset totalassignQuestion = DB.executeQuery("Select count(*) as TotalQuestions from lpqn_questionnaire where ppr_prodcd='" + ppr_prodcd + "'  and cqn_code not in ('0950','0951')");
                    //if (totalassignQuestion.next())
                    //{
                    //    if (int.Parse(totalassignQuestion.getInt("TotalQuestions").ToString()) > medicalQuestionsAnswered)
                    //    {
                    //        Response.Write("<script language='javascript'>alert('Please complete the Medical Details for Both Client')</script>");
                    //        error = 1;
                    //        return;
                    //    }
                    //}

                }
                //}
            }
        }
        private void ValidatingAssessmentDetails()
        {
            string proposal = SessionObject.GetString("NP1_PROPOSAL");
            string assessCount = SHAB.Data.LCQD_QUESTIONSUBDETAIL.getQuestionChannelMapping(Session["s_CCH_CODE"].ToString(), Session["s_CCD_CODE"].ToString(), Session["s_CCS_CODE"].ToString());
            if (Convert.ToInt32(assessCount) > 0)
            {
                System.Text.StringBuilder query_ = new System.Text.StringBuilder();
                query_.Append("Select count(lach.cqn_code) AssignedQuestion, ");
                query_.Append(" count(la.cqn_code) AnsweredQuestion ");
                query_.Append("  from LACH_ASSESSMENT lach");
                query_.Append("  left join lnan_questionnaire la ");
                query_.Append("  on lach.cqn_code=la.cqn_code ");
                query_.Append("  and np1_proposal=? ");
                query_.Append("  where cch_code=? ");
                query_.Append("  and ccd_code=? ");
                query_.Append("  and ccs_code=? ");

                SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                pc.puts("@NP1_PROPOSAL", proposal);
                pc.puts("@CCH_CODE", Session["s_CCH_CODE"].ToString());
                pc.puts("@CCD_CODE", Session["s_CCD_CODE"].ToString());
                pc.puts("@CCS_CODE", Session["s_CCS_CODE"].ToString());
                rowset rs = DB.executeQuery(query_.ToString(), pc);
                if (rs.next())
                {
                    if (rs.getInt("AnsweredQuestion") == 0)
                    {
                        Response.Write("<script language='javascript'>alert('Please make sure to answer all questions on Assessment Tab.')</script>");
                        error = 1;
                    }
                    else
                    {

                        int QuestionsAnswered = rs.getInt("AnsweredQuestion");
                        int QuestionsHaveToAnswered = rs.getInt("AssignedQuestion");

                        if (QuestionsAnswered < QuestionsHaveToAnswered)
                        {
                            Response.Write("<script language='javascript'>alert('Please complete the Suitability Assessment :\\n\\nTotal Questions : " + QuestionsHaveToAnswered + "\\nNot Answered : " + QuestionsAnswered + "')</script>");
                            error = 1;
                            return;
                        }
                    }
                }
            }
        }

        private void ValidatingAssignee()
        {
            string proposal = SessionObject.GetString("NP1_PROPOSAL");
            string companyCode = Session["s_CCD_CODE"].ToString();

            System.Text.StringBuilder query_ = new System.Text.StringBuilder();
            query_.Append("select 1 From lnas_assignee a,lcch_channel c where a.np1_assignmentcd=c.np1_assignmentcd and a.NP1_PROPOSAL=? and c.CCD_CODE=? ");

            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
            pc.puts("@NP1_PROPOSAL", proposal);
            pc.puts("@CCD_CODE", companyCode);
            rowset rs = DB.executeQuery(query_.ToString(), pc);
            if (rs.next())
            {
                error = 0;
            }
            else 
            {
                Response.Write("<script language='javascript'>alert('Please enter Assignee Information')</script>");
                error = 1;
            }
        }

        private void ValidateInputDataInAcceptance()
        {
            try
            {   //Check only For Credit Card
                if (ddlNP1_PAYMENTMET.SelectedValue == "R")
                {
                    DateTime sysDate = Convert.ToDateTime(env.getAttribute("s_CURR_SYSDATE"));
                    DateTime expDate = DateTime.Parse(txtNP1_CCEXPIRY.Text);
                    if (expDate <= sysDate)
                    {
                        error = 1;
                        Response.Write("<script language='javascript'>alert('Credit Card Expiry Date should be greater than System Date');</script>");
                    }
                }
            }
            catch (Exception e)
            {
                error = 1;
                Response.Write("<script language='javascript'>alert('Validating input: " + e.Message + "');</script>");
            }
        }

        public void ValidateOther()
        {
            try
            {
                string proposal = Session["NP1_PROPOSAL"].ToString();
                clsIlasValidateOnAccept objValidate = new clsIlasValidateOnAccept(proposal);
                objValidate.getProposalInformation();

            }
            catch (Exception e)
            {
                error = 1;
                Response.Write("<script language='javascript'>alert('Validating Other Info: " + e.Message + "');</script>");
            }
        }

        private void updateOtherTablesFor()

        {
            try
            {
                //**** Update Policy Master for Payment Detail ****//
                SHMA.Enterprise.Data.ParameterCollection pmr = new SHMA.Enterprise.Data.ParameterCollection();
                pmr.puts("@NP1_PAYMENTMET", ddlNP1_PAYMENTMET.SelectedValue, DbType.String);
                pmr.puts("@NP1_ACCOUNTNO", txtNP1_ACCOUNTNO.Text, DbType.String);
                if (ddlNP1_PAYMENTMET.SelectedValue == "R")
                {
                    pmr.puts("@NP1_CCNUMBER", txtNP1_CCNUMBER.Text.Trim() == "" ? null : txtNP1_CCNUMBER.Text, DbType.String);
                    pmr.puts("@NP1_CCEXPIRY", txtNP1_CCEXPIRY.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNP1_CCEXPIRY.Text), DbType.Date);
                }
                else
                {
                    pmr.puts("@NP1_CCNUMBER", null, DbType.String);
                    pmr.puts("@NP1_CCEXPIRY", null, DbType.Date);
                }
                pmr.puts("@NP1_PROPOSAL", Session["NP1_PROPOSAL"], DbType.String);
                DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_PAYMENTMET=?, NP1_ACCOUNTNO=?, NP1_CCNUMBER=?, NP1_CCEXPIRY=? where NP1_PROPOSAL=?", pmr);

                //**** Update LNU1_UNDERWRITI for account number, if Payment is Bank Debit Order****//
                if (ddlNP1_PAYMENTMET.SelectedValue == "B")//
                {
                    pmr.clear();
                    pmr.puts("@NU1_ACCOUNTNO", txtNP1_ACCOUNTNO.Text, DbType.String);
                    pmr.puts("@NP1_PROPOSAL", Session["NP1_PROPOSAL"], DbType.String);
                    DB.executeDML("UPDATE LNU1_UNDERWRITI SET NU1_ACCOUNTNO=? where NP1_PROPOSAL=?", pmr);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ValidatePlan()
        {
            try
            {
                ace.POLICY_ACCEPTANCE obj = new ace.POLICY_ACCEPTANCE();
                obj.ValidateBaseProduct();
            }
            catch (Exception e)
            {
                error = 1;
                Response.Write("<script language='javascript'>alert('" + e.Message + "');</script>");
            }
        }
        //private void ValidatePremium()
        //{
        //	try
        //	{
        //		ace.POLICY_ACCEPTANCE obj = new ace.POLICY_ACCEPTANCE();
        //		obj.ValidatePremium();
        //	}
        //	catch(Exception e)
        //	{
        //		error=1;
        //		Response.Write("<script language='javascript'>alert('" + e.Message  + "');</script>"); 
        //	}
        //}

        /*private void ViewInitialState()
		{
			if ((""+Session["flg_SELECETD"]).Equals("Y"))
			{
				SessionObject.Set("NPH_CODE",Session["NPH_CODE_s"]);
				SessionObject.Set("NPH_LIFE",Session["NPH_LIFE_s"]);
				SessionObject.Set("NU1_SMOKER",(Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"]));
				SessionObject.Set("NU1_ACCOUNTNO",Session["NU1_ACCOUNTNO"]);
				NPH_CODE = ""+Session["NPH_CODE_s"];
				NPH_LIFE = ""+Session["NPH_LIFE_s"];
				NU1_SMOKER = ""+ (Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"].ToString());
				NU1_ACCOUNTNO = ""+Session["NU1_ACCOUNTNO"];
			}
			else
			{
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE, NU1_SMOKER, NU1_ACCOUNTNO from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='S'");

				if (rsLNPH_PHOLDERDB.next())
				{
					SessionObject.Set("NPH_CODE_s",rsLNPH_PHOLDERDB.getString(1));
					SessionObject.Set("NPH_LIFE_s",rsLNPH_PHOLDERDB.getString(2));
					SessionObject.Set("NU1_SMOKER",rsLNPH_PHOLDERDB.getString(3));
					SessionObject.Set("NU1_ACCOUNTNO",rsLNPH_PHOLDERDB.getString(4));
				}
				else// avoid binding screen to the record of previous proposal selected in the screen
				{
					SessionObject.Remove("NPH_CODE_s");
					SessionObject.Remove("NPH_LIFE_s");
				}
			}
			SessionObject.Remove("flg_SELECETD");
		}
		*/


        protected virtual void setCachePolicy()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

    }
}

