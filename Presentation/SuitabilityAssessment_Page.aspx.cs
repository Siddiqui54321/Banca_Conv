using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise.Presentation;
using SHAB.Data;
using SHAB.Business;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;

namespace SHAB.Presentation
{
    public partial class SuitabilityAssessment_Page : SHMA.Enterprise.Presentation.TwoStepController
    {
        double? cachedPercentage = null;

        #region " First Step "

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            this.proposal = SessionObject.GetString("NP1_PROPOSAL");
            this.prodCode = SessionObject.GetString("PPR_PRODCD");

            this.isValidAge = true;
            return dataHolder;
        }

        sealed protected override void BindInputData(SHMA.Enterprise.DataHolder dataHolder)
        {
            if (this.isValidAge)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CQN_CODE", typeof(string));
                dt.Columns.Add("CQN_DESC", typeof(string));
                dt.Columns.Add("CQN_TYPE", typeof(string));
                dt.Columns.Add("CQN_CRTLTYPE", typeof(string));
                dt.Columns.Add("CQN_QTYPE", typeof(string));

                getDataSource(dataHolder);
                DataRow dr = dt.NewRow();
                dr["CQN_DESC"] = "Qualitative Criteria";
                dt.Rows.Add(dr);
                int i = 0;
                foreach (DataRow item in dataHolder.Data.Tables[0].Rows)
                {
                    if (item["CQN_QTYPE"].ToString() == "QLC")
                    {
                        dr = dt.NewRow();
                        dr["CQN_CODE"] = item["CQN_CODE"];
                        dr["CQN_DESC"] = item["CQN_DESC"];
                        dr["CQN_TYPE"] = item["CQN_TYPE"];
                        dr["CQN_CRTLTYPE"] = item["CQN_CRTLTYPE"];
                        dr["CQN_QTYPE"] = item["CQN_QTYPE"];
                        dt.Rows.Add(dr);
                    }

                    else
                    {
                        if (i == 0)
                        {
                            dr = dt.NewRow();
                            dr["CQN_DESC"] = "Quantitative Criteria";
                            dt.Rows.Add(dr);
                            i = 1;
                        }
                        dr = dt.NewRow();
                        dr["CQN_CODE"] = item["CQN_CODE"];
                        dr["CQN_DESC"] = item["CQN_DESC"];
                        dr["CQN_TYPE"] = item["CQN_TYPE"];
                        dr["CQN_CRTLTYPE"] = item["CQN_CRTLTYPE"];
                        dr["CQN_QTYPE"] = item["CQN_QTYPE"];
                        dt.Rows.Add(dr);
                    }

                }
                dt.AcceptChanges();
                dGrid1.DataSource = dt;
                dGrid1.DataBind();
                DataRow[] qlc = dt.Select("CQN_QTYPE='QLC'");
                DataRow[] qnc = dt.Select("CQN_QTYPE='QNC'");
                Session["qlc_noofquestion"] = qlc.Length;
                Session["qnc_noofquestion"] = qnc.Length;
            }
            CSSLiteral.Text = ace.Ace_General.loadMainStyle();
        }

        protected override void PrepareInputUI(DataHolder dataHolder)
        {
            if (this.proposal.Equals(string.Empty))
            {
                HeaderScript.Text += "alert('Please Select or Enter Proposal for Suitability Assessment')";
            }
        }


        #endregion

        #region " Second Step "

        sealed protected override SHMA.Enterprise.DataHolder GetData(SHMA.Enterprise.DataHolder dataHolder)
        {
            proposal = SessionObject.GetString("NP1_PROPOSAL");
            prodCode = SessionObject.GetString("PPR_PRODCD");
            dataHolder = new LNAN_QUESTIONNAIREDB(dataHolder).LoadAssessmentQuestionnaireData(proposal, prodCode);
            return dataHolder;
        }
        sealed protected override void ApplyDomainLogic(SHMA.Enterprise.DataHolder dataHolder)
        {
            if (dGrid1.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            {
                LNAN_QUESTIONNAIRE quesBusinessObj = new LNAN_QUESTIONNAIRE(dataHolder);
                DB.BeginTransaction();
                SaveTransaction = true;
                string cqnCode = string.Empty;
                string answer = string.Empty;
                string visible = string.Empty;
                bool isChecked = false;
                NameValueCollection nvc = null;
                DataGrid subdGrid1 = null;
                System.Text.StringBuilder sbMessage = new System.Text.StringBuilder("Question # ");
                foreach (DataGridItem item in dGrid1.Items)
                {
                    cqnCode = ((Label)item.FindControl("lblCode")).Text;
                    if (cqnCode != "" && cqnCode != null)
                    {
                        answer = ((DropDownList)item.FindControl("ddl_values")).SelectedValue;
                        item.ID = item.ClientID;
                        nvc = getNVCforAssessmentQuestions(cqnCode, answer.Split('-')[1], answer.Split('-')[0]);

                        if (dataHolder["LNAN_QUESTIONNAIRE"].Select(getFilterExp(cqnCode)).Length != 0)
                        {
                            quesBusinessObj.Update(nvc);
                        }
                        else
                        {
                            quesBusinessObj.Add(nvc, "");
                        }
                    }
                }
                dataHolder.Update(DB.Transaction);

                double per = getTotalPercentage();
                double validation = checkValidation();
                if (validation > 0)
                {
                    string q = "update lnp1_policymastr set np1_checked='Y' where np1_proposal='" + this.proposal + "'";
                    DB.executeDML(q);
                }
                else
                {
                    if (per >= 20 && per <= 30)
                    {
                        string q = "update lnp1_policymastr set np1_checked='Y' where np1_proposal='" + this.proposal + "'";
                        DB.executeDML(q);
                    }
                    else if (per > 30)
                    {
                        string q = "update lnp1_policymastr set np1_checked='N' where np1_proposal='" + this.proposal + "'";
                        DB.executeDML(q);
                    }
                }

                if (sbMessage.Length > 11)
                {
                    sbMessage.Length = sbMessage.Length - 1;
                    sbMessage.Append("\\n are not complete, Please enter answers of these questions");
                    showAlertMessage(sbMessage.ToString());
                    messageArray = sbMessage.ToString().Split(new char[] { ',' });
                }
                else
                {
                    Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.MEDICAL_QUESTIONS_UPDATED);
                }
            }
        }

        sealed protected override void DataBind(DataHolder dataHolder)
        {
            if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CQN_CODE", typeof(string));
                dt.Columns.Add("CQN_DESC", typeof(string));
                dt.Columns.Add("CQN_TYPE", typeof(string));
                dt.Columns.Add("CQN_CRTLTYPE", typeof(string));
                dt.Columns.Add("CQN_QTYPE", typeof(string));

                getDataSource(dataHolder);
                DataRow dr = dt.NewRow();
                dr["CQN_DESC"] = "Qualitative Criteria";
                dt.Rows.Add(dr);
                int i = 0;
                foreach (DataRow item in dataHolder.Data.Tables[1].Rows)
                {
                    if (item["CQN_QTYPE"].ToString() == "QLC")
                    {
                        dr = dt.NewRow();
                        dr["CQN_CODE"] = item["CQN_CODE"];
                        dr["CQN_DESC"] = item["CQN_DESC"];
                        dr["CQN_TYPE"] = item["CQN_TYPE"];
                        dr["CQN_CRTLTYPE"] = item["CQN_CRTLTYPE"];
                        dr["CQN_QTYPE"] = item["CQN_QTYPE"];
                        dt.Rows.Add(dr);
                    }

                    else
                    {
                        if (i == 0)
                        {
                            dr = dt.NewRow();
                            dr["CQN_DESC"] = "Quantitative Criteria";
                            dt.Rows.Add(dr);
                            i = 1;
                        }
                        dr = dt.NewRow();
                        dr["CQN_CODE"] = item["CQN_CODE"];
                        dr["CQN_DESC"] = item["CQN_DESC"];
                        dr["CQN_TYPE"] = item["CQN_TYPE"];
                        dr["CQN_CRTLTYPE"] = item["CQN_CRTLTYPE"];
                        dr["CQN_QTYPE"] = item["CQN_QTYPE"];
                        dt.Rows.Add(dr);
                    }

                }
                dt.AcceptChanges();
                dGrid1.DataSource = dt;
                dGrid1.DataBind();
            }
        }

        #endregion

        #region " Supporting Methods "

        private void showAlertMessage(string message_)
        {
            Response.Write("<script>alert('" + message_ + "')</script>");
        }
        private DataTable getDataSource(DataHolder ds)
        {
            string default_ = string.Empty;
            string cch_code = string.Empty;
            string ccd_code = string.Empty;
            string ccs_code = string.Empty;
            if (this.proposal.Equals(string.Empty))
            {
                default_ = "0";
            }
            else
            {
                default_ = "1";
            }
            cch_code = SessionObject.Get("s_CCH_CODE").ToString();
            ccd_code = SessionObject.Get("s_CCD_CODE").ToString();
            ccs_code = SessionObject.Get("s_CCS_CODE").ToString();

            ds = new LCQD_QUESTIONSUBDETAIL(dataHolder).getAssessmentQuestionnaireData(cch_code, ccd_code, ccs_code, default_);
            return ds["LNQN_QUESTIONNAIRE_DATA"];
        }
        private string getFilterExp(string code)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("NP1_PROPOSAL = '");
            sb.Append(proposal);
            sb.Append("' AND NP2_SETNO = 1 ");
            sb.Append("  AND PPR_PRODCD = '");
            sb.Append(prodCode);
            sb.Append("' AND CQN_CODE = '");
            sb.Append(code);
            sb.Append("'");
            return sb.ToString();
        }
        private NameValueCollection getNVCforAssessmentQuestions(string code, string ans, string scode)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.add("NP1_PROPOSAL", proposal);
            nvc.add("NPH_CODE", SessionObject.Get("Nph_code"));
            nvc.add("NP2_SETNO", 1);
            nvc.add("PPR_PRODCD", prodCode);
            nvc.add("CQN_CODE", code);
            nvc.add("CQN_SUBCODE", scode);
            nvc.add("NQN_ANSWER", ans);
            nvc.add("NQN_REMARKS", "");
            return nvc;
        }

        #endregion

        #region " Events "

        private void dGrid1_OnItemDataBound(Object sender, DataGridItemEventArgs e)
        {
            try
            {
                string processedValue = string.Empty;
                string assignementValue = string.Empty;
                string query = string.Empty;
                string res = string.Empty;
                string QuestionType = string.Empty; // new variable for JS09

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string cqnCode = ((Label)e.Item.FindControl("lblCode")).Text;
                    if (((Label)e.Item.FindControl("lblCode")).Text != null && ((Label)e.Item.FindControl("lblCode")).Text != "")
                    {
                        processedValue = string.Empty;
                        assignementValue = string.Empty;
                        DropDownList ddl_option = null;
                        DataHolder options = new DataHolder();
                        options = new LCQD_QUESTIONSUBDETAIL(dataHolder).getAssessmentQuestionnaireOptionsData(cqnCode);
                        if (options["lcqd_questionsubdetail_DATA"] != null)
                        {
                            DataTable dt = options["lcqd_questionsubdetail_DATA"];
                            DataRow[] dr = dt.Select("CQN_SUBCODE='0'");
                            TotalScores += Convert.ToDouble(dr[0]["CQN_SCORE"].ToString().Split('-')[1]);
                            QuestionType = dr[0]["CQN_VALUETYPE"].ToString();
                            if (dr[0]["CQN_VALUETYPE"].ToString() == "Q")
                            {
                                query = dr[0]["CQN_VALUEDESC"].ToString();
                                res = LCQD_QUESTIONSUBDETAIL.getQueryResult(query.Split('-')[1], SessionObject.Get("NP1_PROPOSAL").ToString());
                                if (query.Split('-')[0] == "AGE")
                                {
                                    DataRow[] temp = dt.Select("CQN_RANGEFROM<='" + res + "' and CQN_RANGETO>='" + res + "'");
                                    processedValue = temp[0]["CQN_SCORE"].ToString();
                                    assignementValue = processedValue.Split('-')[1];
                                }
                                if (query.Split('-')[0] == "GENDER")
                                {
                                    DataRow[] temp = dt.Select("CQN_VALUEDESC='" + res + "'");
                                    processedValue = temp[0]["CQN_SCORE"].ToString();
                                    assignementValue = processedValue.Split('-')[1];
                                }
                                if (query.Split('-')[0] == "ANNUALINCOME")
                                {
                                    DataRow[] temp = dt.Select("CQN_RANGEFROM<='" + Convert.ToInt32(Convert.ToDouble(res)) + "' and CQN_RANGETO>='" + Convert.ToInt32(Convert.ToDouble(res)) + "'");
                                    processedValue = temp[0]["CQN_SCORE"].ToString();
                                    assignementValue = processedValue.Split('-')[1];
                                }
                            }

                            ((TextBox)e.Item.FindControl("txtAssignScores")).Text = dr[0]["CQN_SCORE"].ToString().Split('-')[1];
                            dt.Rows.Remove(dr[0]);
                            dt.AcceptChanges();

                            ddl_option = (DropDownList)e.Item.FindControl("ddl_values");
                            ddl_option.Attributes.Add("onChange", "fnOnIndexChanged(this,'" + e.Item.ClientID + "')");
                            ddl_option.DataTextField = "CQN_SUBDESC";
                            ddl_option.DataValueField = "CQN_SCORE";
                            ddl_option.DataSource = dt;
                            ddl_option.DataBind();
                            if (processedValue != "" && processedValue != null)
                            {
                                ddl_option.SelectedValue = processedValue;
                                ((TextBox)e.Item.FindControl("txtAssignScores")).Text = assignementValue;
                                ddl_option.Enabled = false;
                            }
                            else
                            {
                                ddl_option.Enabled = true;
                            }
                        }
                        string answer1 = LNAN_QUESTIONNAIREDB.getAnswerOfQuestion(proposal, prodCode, cqnCode);
                        if (answer1 != "" && QuestionType != "Q") // && subCode != null && answer1 == subCode) // new condition for JS09
                        {
                            ddl_option.SelectedValue = answer1;
                            ((TextBox)e.Item.FindControl("txtScores")).Text = answer1.Split('-')[1];
                            scoresTotal += Convert.ToDouble(answer1.Split('-')[1]);
                        }
                        else
                        {
                            if (assignementValue != "" && assignementValue != null)
                            {
                                ((TextBox)e.Item.FindControl("txtScores")).Text = assignementValue;
                            }
                            else
                            {
                                ((TextBox)e.Item.FindControl("txtScores")).Text = options["lcqd_questionsubdetail_DATA"].Rows[0][4].ToString().Split('-')[1];
                            }

                            scoresTotal += double.Parse(options["lcqd_questionsubdetail_DATA"].Rows[0][4].ToString().Split('-')[1]);
                        }


                        lbl_TotalAssignScores.Text = TotalScores.ToString();
                        lbl_totalScores.Text = scoresTotal.ToString();

                        // double per = getTotalPercentage();
                        // lbl_TotalPercentage.Text = String.Format("{0:N0}", per) + "%";

                        if (cachedPercentage == null)

                            cachedPercentage = getTotalPercentage();

                        double per = cachedPercentage.Value;
                        System.Diagnostics.Debug.WriteLine("getTotalPercentage() = " + per.ToString());
                        lbl_TotalPercentage.Text = String.Format("{0:N1}", cachedPercentage) + "%";
                        lbl_TotalPercentage.Attributes["data-value"] = lbl_TotalPercentage.Text;
                        System.Diagnostics.Debug.WriteLine("After label set: " + lbl_TotalPercentage.Text);



                        // ✅ Old Conditions
                        if (per > 30)
                        {
                            lbl_Decisions.Text = "Normal case no approval required";
                        }
                        else if (per >= 20 && per <= 30)
                        {
                            if (Session["BankCode"].ToString() == "9")
                                lbl_Decisions.Text = "RHB Approval Required";
                            else
                                lbl_Decisions.Text = "GM Approval Required";
                        }
                        else
                        {
                            lbl_Decisions.Text = "Declined";
                        }


                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblDesc")).Style.Add("font-weight", "bold");
                        ((Label)e.Item.FindControl("lblDesc")).Style.Add("font-size", "12px");
                        ((Label)e.Item.FindControl("lblDesc")).Style.Add("font-size", "12px");
                        ((DropDownList)e.Item.FindControl("ddl_values")).Style.Add("display", "none");
                        ((TextBox)e.Item.FindControl("txtScores")).Style.Add("display", "none");
                        ((TextBox)e.Item.FindControl("txtRemarks")).Style.Add("display", "none");
                    }


                }

                // For check Illrate , Widow , Senior citize new code
                bool mustRHB = false;

                // loop through grid rows
                foreach (DataGridItem row in dGrid1.Items)
                {
                    if (row.ItemType == ListItemType.Item || row.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblCode = row.FindControl("lblCode") as Label;
                        DropDownList ddl = row.FindControl("ddl_values") as DropDownList;

                        if (lblCode == null || ddl == null)
                            continue;

                        string code = lblCode.Text.Trim();
                        string value = ddl.SelectedValue;

                        if ((code == "BP03" && value == "1-0") ||
                            (code == "BP04" && value == "1-2") ||
                            (code == "BP05" && value == "15-4") ||
                            (code == "BP05" && value == "14-2"))
                        {
                            mustRHB = true;
                            break; // stop loop, same as JS
                        }
                    }
                }

                // Now apply decision
                if (mustRHB)
                {
                    if (Session["BankCode"] != null && Session["BankCode"].ToString() == "9")
                    {
                        lbl_Decisions.Text = "RHB Approval Required";
                    }
                    else
                    {
                        lbl_Decisions.Text = "GM Approval Required";
                    }
                }
            }
            catch (Exception ex)
            {
            }


            //if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    e.Item.Cells[1].Style.Add("font-weight", "bold");
            //    e.Item.Cells[1].Style.Add("font-size", "12px");
            //    e.Item.Cells[2].Style.Add("font-size", "12px");
            //    e.Item.Cells[3].Style.Add("font-size", "12px");
            //    e.Item.Cells[4].Style.Add("font-size", "12px");
            //    e.Item.Cells[1].Style.Add("text-align", "right");
            //    e.Item.Cells[2].Style.Add("text-align", "right");
            //    e.Item.Cells[3].Style.Add("text-align", "right");
            //    e.Item.Cells[4].Style.Add("text-align", "right");
            //    e.Item.Cells[1].Text = "Grand Total";
            //    e.Item.Cells[2].Text = "130";
            //    e.Item.Cells[3].Text = scoresTotal.ToString();
            //    e.Item.Cells[4].Text = String.Format("{0:N0}",(scoresTotal/130)*100).ToString()+"%";
            //    scoresTotal = 0;
                    }
        protected void _CustomEvent_ServerClick(object sender, System.EventArgs e)
        {
            ControlArgs = new object[1];
            switch (_CustomEventVal.Value)
            {
                case "Update":
                    ControlArgs[0] = EnumControlArgs.Update;
                    DoControl();
                    break;
                case "Save":
                    ControlArgs[0] = EnumControlArgs.Save;
                    DoControl();
                    break;
                case "Delete":
                    ControlArgs[0] = EnumControlArgs.Delete;
                    DoControl();
                    break;
                case "Filter":
                    ControlArgs[0] = EnumControlArgs.Filter;
                    DoControl();
                    break;
                case "Process":
                    ControlArgs[0] = EnumControlArgs.Process;
                    DoControl();
                    break;

            }
            _CustomEventVal.Value = "";
        }
        #endregion

        #region " Class Variable "

        protected System.Web.UI.WebControls.Literal _result;
        protected System.Web.UI.WebControls.Literal HeaderScript;

        private RadioButton rdoBtnY = null;
        private RadioButton rdoBtnN = null;
        private TextBox txtIsChildExist = null;
        private double scoresTotal = 0;
        private double TotalScores = 0;
        private string proposal = string.Empty;
        private string prodCode = string.Empty;
        private int assuredAge = 0;
        private bool isValidAge = false;
        private string[] messageArray = null;

        //private ace.OracleClientAdapter call = null;

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dGrid1_OnItemDataBound);

        }
        #endregion
        private double getTotalPercentage()
        {
            try
            {
                string sqlString = "select sum((obtain_marks / tot_marks) * case\n" +
                   "                 when qcn_type = 'QNC' then\n" +
                   "                  60\n" +
                   "                 when qcn_type = 'QLC' then\n" +
                   "                  40\n" +
                   "                 else\n" +
                   "                  100\n" +
                   "               end)\n" +
                   "            perc\n" +
                   "  from (select lq.cqn_qtype qcn_type,\n" +
                   "               sum(to_number(la.nqn_answer)) obtain_marks,\n" +
                   "               sum(ld.cqn_score) tot_marks\n" +
                   "          from lnp1_policymastr       p1,\n" +
                   "               lnan_questionnaire     la,\n" +
                   "               lcqn_questionnaire     lq,\n" +
                   "               lcqd_questionsubdetail ld,\n" +
                   "               lach_assessment        lc\n" +
                   "         where p1.np1_proposal = la.np1_proposal\n" +
                   "           and la.cqn_code = lq.cqn_code\n" +
                   "           and lq.cqn_code = ld.cqn_code\n" +
                   "           and lq.cqn_type = ld.cqn_type\n" +
                   "           and lq.cqn_code = lc.cqn_code\n" +
                   "           and lc.cch_code = p1.np1_channel\n" +
                   "           and lc.ccd_code = p1.np1_channeldetail\n" +
                   "           and lc.ccs_code = p1.np1_channelsdetail\n" +
                   "           and la.np1_proposal = '" + this.proposal + "'\n" +
                   "           and ld.cqn_subcode = '0'\n" +
                   "         group by lq.cqn_qtype)";
                rowset rs = DB.executeQuery(sqlString);
                double per = 0;
                if (rs.next())
                {
                    per = rs.getDouble(1);
                }
                return per;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        private double checkValidation()
        {
            try
            {
                string sqlString = "Select count(*)\n" +
                "From LPVL_VALIDATION vl\n" +
                "inner join lnan_questionnaire qn\n" +
                "on vl.ppr_prodcd=qn.ppr_prodcd\n" +
                "and substr(PVL_VALUECOMB,0,4)=qn.cqn_code\n" +
                "and substr(PVL_VALUECOMB,6,6)=qn.cqn_subcode\n" +
                "and vl.PVL_VALIDATIONFOR='SUITABILITY'\n" +
                "where qn.np1_proposal='" + this.proposal + "'";
                rowset rs = DB.executeQuery(sqlString);
                double per = 0;
                if (rs.next())
                {
                    per = rs.getDouble(1);
                }
                return per;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
