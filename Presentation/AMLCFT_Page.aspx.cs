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
using System.Data.OleDb;

namespace SHAB.Presentation
{
    public partial class AMLCFT_Page : SHMA.Enterprise.Presentation.TwoStepController
    {
        OleDbCommand CmdDML = new OleDbCommand();
        string Sql;
        #region " First Step "

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            this.proposal = SessionObject.GetString("NP1_PROPOSAL");
            this.prodCode = SessionObject.GetString("PPR_PRODCD");
            this.jointLife = SessionObject.GetString("NP1_JOINT");
            if (jointLife == "Y")
            {
                ddlNPH_JOINTlIFE.Enabled = true;
            }
            else
            {
                ddlNPH_JOINTlIFE.Enabled = true;

            }
            //this.assuredAge = LNU1_UNDERWRITIDB.getAgeOfLifeAssured(proposal);
            //this.isValidAge = validatingAgeOfLifeAssured();
            this.isValidAge = true;
            return dataHolder;
        }

        sealed protected override void BindInputData(SHMA.Enterprise.DataHolder dataHolder)
        {
            if (this.isValidAge)
            {
                // Original

                 //dGrid.DataSource = getDataSource(dataHolder);
                // dGrid.DataBind();

                // New code by Imran 23 July 2025



                if (SessionObject.GetString("NP1_PROPOSAL").ToString() == "")   
                {
                    btnSave.Enabled = false;
                    ShowAlert("Please select Proposal for AML-CFT Page");
                }

                else if (SessionObject.GetString("PPR_PRODCD").ToString() == "")
                {
                    btnSave.Enabled = false;
                    ShowAlert("Please select Proposal and also product from Plan Rider tab for AML-CFT Page");


                }
                else
                {
                    btnSave.Enabled = true;
                    DataTable dtProposal = new DataTable();
                    // Check AML answers are available before also not include medical questions
                    Sql = "Select * from LNQN_QUESTIONNAIRE r where r.np1_proposal='" + proposal + "' and r.Cqn_Code not IN ('0900','0910','0920','0930','0940','0960','0970','9310','9320','9300','950','0950')"; 
  
                    dtProposal = GetdataOraOledb(Sql);

                    if(dtProposal.Rows.Count>0)
                    {
                       // Check AML answers are available before

                       Sql = " Select q.cqn_code,q.cqn_desc,r.nqn_answer Answer,r.nqn_remarks Remarks" +
                            " from lcqn_questionnaire q , LNQN_QUESTIONNAIRE r " +
                            " where q.cqn_code = r.cqn_code " +
                            " and r.Cqn_Code not IN ('0900','0910','0920','0930','0940','0960','0970','9310','9320','9300','950','0950') " +
                            " and r.np1_proposal = '" + proposal + "'   order by q.cqn_code";

                    }
                    else
                    {
                        // Store AML answer first time
                        Sql = @"select q.cqn_code, q.cqn_desc , q.cqn_condition,q.cqn_short,n.pqn_default Answer,null Remarks
                                from lcqn_questionnaire q, lpqn_questionnaire n
                                where q.cqn_code = n.cqn_code
                                and substr(q.cqn_code,1,5)='AMLQR' 
                                and n.ppr_prodcd='" + prodCode +
                              "'    order by q.cqn_code";
                    }


                    DataTable dtProDetails = new DataTable();
                    dtProDetails = GetdataOraOledb(Sql);

                    
                    GridView1.DataSource = dtProDetails;
                    GridView1.DataBind();


                   // CSSLiteral.Text = ace.Ace_General.loadMainStyle();

                }

            }
          //  CSSLiteral.Text = ace.Ace_General.loadMainStyle();
        }

        protected override void PrepareInputUI(DataHolder dataHolder)
        {
            if (this.prodCode.Equals(string.Empty))
            {
          //      HeaderScript.Text += "alert('Please select Proposal for AML-CFT Page')";

            }
        }


        #endregion

        #region " Second Step "

        sealed protected override SHMA.Enterprise.DataHolder GetData(SHMA.Enterprise.DataHolder dataHolder)
        {
            proposal = SessionObject.GetString("NP1_PROPOSAL");
            prodCode = SessionObject.GetString("PPR_PRODCD");
            //this.assuredAge = LNU1_UNDERWRITIDB.getAgeOfLifeAssured(proposal);
            dataHolder = new LNQN_QUESTIONNAIREDB(dataHolder).LoadMedicalQuestionnaireData(proposal, prodCode);
            dataHolder = new LNQD_QUESTIONDETAILDB(dataHolder).LoadMedicalSubQuestionnaireData(proposal, prodCode);
            return dataHolder;
        }
        sealed protected override void ApplyDomainLogic(SHMA.Enterprise.DataHolder dataHolder)
        {

            Response.Write("Data Save");
            //if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            //{
            //    LNQN_QUESTIONNAIRE quesBusinessObj = new LNQN_QUESTIONNAIRE(dataHolder);
            //    LNQD_QUESTIONDETAIL subQuesBusinessObj = new LNQD_QUESTIONDETAIL(dataHolder);

            //    DB.BeginTransaction();
            //    SaveTransaction = true;
            //    string cqnCode = string.Empty;
            //    string answer = string.Empty;
            //    string visible = string.Empty;
            //    bool isChecked = false;
            //    NameValueCollection nvc = null;
            //    DataGrid subdGrid = null;
            //    System.Text.StringBuilder sbMessage = new System.Text.StringBuilder("Question # ");
            //    foreach (DataGridItem item in dGrid.Items)
            //    {
            //        visible = ((TextBox)item.FindControl("txtVisible")).Text;
            //        cqnCode = ((Label)item.FindControl("lblCode")).Text;
            //        if (visible.Equals("block"))
            //        {
            //            answer = string.Empty;
            //            item.ID = item.ClientID;
            //            subdGrid = (DataGrid)item.FindControl("dSubGrid");
            //            isChecked = ((RadioButton)item.FindControl("rdoYes")).Checked;
            //            if (isChecked)
            //            {
            //                answer = "Y";
            //                if (cqnCode == "0970")
            //                {
            //                    deleteSubGridData(subQuesBusinessObj, subdGrid, cqnCode);
            //                }
            //                else
            //                {
            //                    if (subdGrid.Items.Count != 0)
            //                    {
            //                        saveSubGridData(subQuesBusinessObj, subdGrid, cqnCode);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                isChecked = ((RadioButton)item.FindControl("rdoNo")).Checked;
            //                if (isChecked)
            //                {
            //                    answer = "N";
            //                    if (cqnCode == "0970")
            //                    {
            //                        saveSubGridData(subQuesBusinessObj, subdGrid, cqnCode);
            //                    }
            //                    else
            //                    {
            //                        deleteSubGridData(subQuesBusinessObj, subdGrid, cqnCode);
            //                    }
            //                }
            //                else
            //                {
            //                    sbMessage.Append(cqnCode);
            //                    sbMessage.Append(",");
            //                }
            //            }
            //            nvc = getNVCforMedicalQuestions(cqnCode, answer);
            //            if (dataHolder["LNQN_QUESTIONNAIRE"].Select(getFilterExp(cqnCode)).Length != 0)
            //            {
            //                quesBusinessObj.Update(nvc);
            //            }
            //            else
            //            {
            //                quesBusinessObj.Add(nvc, "");
            //            }
            //        }
            //        else
            //        {
            //            if (dataHolder["LNQN_QUESTIONNAIRE"].Select(getFilterExp(cqnCode)).Length != 0)
            //            {
            //                nvc = getNVCforMedicalQuestions(cqnCode, string.Empty);
            //                quesBusinessObj.Delete(nvc);
            //            }

            //        }
            //    }
            //    dataHolder.Update(DB.Transaction);
            //    if (sbMessage.Length > 11)
            //    {
            //        sbMessage.Length = sbMessage.Length - 1;
            //        sbMessage.Append("\\n are not complete, Please enter answers of these questions");
            //        showAlertMessage(sbMessage.ToString());
            //        messageArray = sbMessage.ToString().Split(new char[] { ',' });
            //    }
            //    else
            //    {
            //        Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.MEDICAL_QUESTIONS_UPDATED);
            //    }
            //}
      
        // end previouse code
        }

        sealed protected override void DataBind(DataHolder dataHolder)
        {
            if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            {
                dGrid.DataSource = getDataSource(dataHolder);
                dGrid.DataBind();
                highlightUnmarkedQuestions();
            }
        }

        #endregion

        #region " Supporting Methods "

        private void showAlertMessage(string message_)
        {
            Response.Write("<script>alert('" + message_ + "')</script>");
        }
        private void saveSubGridData(LNQD_QUESTIONDETAIL subQuesObj, DataGrid dSubGrid, string cqnCode)
        {
            string colID = string.Empty;
            string answer = string.Empty;
            NameValueCollection nvc = null;
            foreach (DataGridItem item in dSubGrid.Items)
            {
                answer = string.Empty;
                colID = ((Label)item.FindControl("lblColumnID")).Text;
                answer = ((TextBox)item.FindControl("txtSubAnswer")).Text;
                nvc = getNVCforMedicalQuestions(cqnCode, colID, answer);
                if (dataHolder["LNQD_QUESTIONDETAIL"].Select(getFilterExp(cqnCode, colID)).Length != 0)
                {
                    subQuesObj.Update(nvc);
                }
                else
                {
                    subQuesObj.Add(nvc, "");
                }
            }
        }
        private void deleteSubGridData(LNQD_QUESTIONDETAIL subQuesObj, DataGrid dSubGrid, string cqnCode)
        {
            string colID = string.Empty;
            NameValueCollection nvc = null;
            foreach (DataGridItem item in dSubGrid.Items)
            {
                colID = ((Label)item.FindControl("lblColumnID")).Text;
                nvc = getNVCforMedicalQuestions(cqnCode, colID, "");
                if (dataHolder["LNQD_QUESTIONDETAIL"].Select(getFilterExp(cqnCode, colID)).Length != 0)
                {
                    subQuesObj.Delete(nvc);
                }
            }
        }
        private DataTable getDataSource(DataHolder ds)
        {
            //string default_ = assuredAge > 45 || (assuredAge > 12 && assuredAge < 18) ? "Y" : "N";			
            string default_ =ddlNPH_JOINTlIFE.SelectedValue;
            ds = new LNQN_QUESTIONNAIREDB(dataHolder).getMedicalQuestionnaireData_AML(proposal, prodCode, default_);
            return ds["LNQN_QUESTIONNAIRE_DATA"];
        }
        private string getFilterExp(string code, string columnid)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("NP1_PROPOSAL = '");
            sb.Append(proposal);
            sb.Append("' AND NP2_SETNO = 1 ");
            sb.Append("  AND PPR_PRODCD = '");
            sb.Append(prodCode);
            sb.Append("' AND CQN_CODE = '");
            sb.Append(code);
            sb.Append("'AND CCN_COLUMNID = '");
            sb.Append(columnid);
            sb.Append("'");
            return sb.ToString();
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
        private NameValueCollection getNVCforMedicalQuestions(string code, string ans)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.add("NP1_PROPOSAL", proposal);
            nvc.add("NP2_SETNO", 1);
            nvc.add("PPR_PRODCD", prodCode);
            nvc.add("CQN_CODE", code);
            nvc.add("NQN_ANSWER", ans);
            nvc.add("NQN_DETAILS", "");
            nvc.add("NQN_REMARKS", "");
            return nvc;
        }
        private NameValueCollection getNVCforMedicalQuestions(string code, string columnid, string ans)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.add("NP1_PROPOSAL", proposal);
            nvc.add("NP2_SETNO", 1);
            nvc.add("PPR_PRODCD", prodCode);
            nvc.add("CQN_CODE", code);
            nvc.add("CCN_COLUMNID", columnid);
            nvc.add("NQD_ANSWER", ans);
            return nvc;
        }

        private bool validatingAgeOfLifeAssured()
        {
            bool valid = true;
            if (prodCode.Equals("212") || prodCode.Equals("213")) // KASB
            {
                if (this.assuredAge > 4 && this.assuredAge < 13)
                {
                    _result.Text = "alert('As insured age is belong to 5-12 years.Medical details may require by Adamjee Life head office after initial process of the Application');";
                    valid = false;
                }
            }
            else if (prodCode.Equals("211")) // Future Assure
            {
                if (this.assuredAge > 2 && this.assuredAge < 13)
                {
                    _result.Text = "alert('As insured age is belong to 3-12 years.Medical details may require by Adamjee Life head office after initial process of the Application');";
                    valid = false;
                }
            }
            else if (prodCode.Equals("301")) // Future Assure
            {
                if (this.assuredAge > 4 && this.assuredAge < 13)
                {
                    _result.Text = "alert('As insured age is belong to 5-12 years.Medical details may require by Adamjee Life head office after initial process of the Application');";
                    valid = false;
                }
            }
            return valid;
        }

        private void highlightUnmarkedQuestions()
        {
            if (messageArray==null)
            {
                messageArray = new string[0];
            }
            if (messageArray.Length > 0)
            {
                string cqnCode = string.Empty;
                foreach (DataGridItem item in dGrid.Items)
                {
                    foreach (string code in messageArray)
                    {
                        cqnCode = ((Label)item.FindControl("lblCode")).Text;
                        if (cqnCode.Equals(code))
                        {
                            item.BackColor = Color.LightYellow;
                            break;
                        }
                    }
                }
            }
        }


        #endregion

        #region " Events "

        //		private void dGrid_OnItemCreated(Object sender, DataGridItemEventArgs e)
        //		{
        //			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //			{
        //				DataGrid subGrid = (DataGrid)e.Item.FindControl("dSubGrid");
        //				//subGrid.ItemDataBound += new DataGridItemEventHandler(subGrid_OnItemDataBound);
        //			}
        //		}
        private void dGrid_OnItemDataBound(Object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string condition = ((DataRowView)(e.Item.DataItem)).Row["cqn_condition"].ToString();
                string cqnCode = ((Label)e.Item.FindControl("lblCode")).Text;
                string answer = LNQN_QUESTIONNAIREDB.getAnswerOfQuestion(proposal, prodCode, cqnCode);
                string parentQuesID = ((DataRowView)(e.Item.DataItem)).Row["cqn_short"].ToString();
                TextBox txtVisible = (TextBox)e.Item.FindControl("txtVisible");
                HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("subDiv");



                if (answer.Equals("Y"))
                {
                    ((RadioButton)e.Item.FindControl("rdoYes")).Checked = true;
                }
                else if (answer.Equals("N"))
                {
                    ((RadioButton)e.Item.FindControl("rdoNo")).Checked = true;
                }
                DataGrid sGrid = (DataGrid)e.Item.FindControl("dSubGrid");
                IDataReader rdr = SHAB.Data.LNQD_QUESTIONDETAILDB.getMedicalSubQuestions(cqnCode, prodCode, proposal);
                sGrid.DataSource = rdr;
                sGrid.DataBind();
                rdr.Close();

                if (((RadioButton)e.Item.FindControl("rdoYes")).Checked && sGrid.Items.Count > 0)
                {
                    ////////////////================ CODE 04-MAY-2018 (0970 Question)
                    if (cqnCode == "0970" || cqnCode == "0971")
                    {
                        div.Style.Add("display", "none");
                    }
                    ////////////////================ CODE 04-MAY-2018 (0970 Question)
                    else { div.Style.Add("display", "block"); }
                }
                else if (((RadioButton)e.Item.FindControl("rdoNo")).Checked && sGrid.Items.Count > 0)
                {
                    if (cqnCode == "0970" || cqnCode == "0971")
                    {
                        div.Style.Add("display", "block");
                    }
                    else
                    {
                        div.Style.Add("display", "none");
                    }
                }
                else
                {
                    div.Style.Add("display", "none");
                }

                e.Item.Attributes.Add("id", e.Item.ClientID);
                txtVisible.Text = "block";

                if (!LNQN_QUESTIONNAIREDB.isConditionTrue(this.prodCode, this.proposal, cqnCode, condition))
                {
                    e.Item.Visible = false;
                    //e.Item.Style.Add("display", "none");
                    txtVisible.Text = "none";
                }
                else
                {
                    e.Item.Visible = true;
                    //e.Item.Style.Add("display", "block");
                    txtVisible.Text = "block";
                }

                if (prevQuesID.Equals(parentQuesID) && prevQuesID != string.Empty)
                {
                    this.txtIsChildExist.Text = e.Item.ClientID;
                    this.rdoBtnY.Attributes.Add("onclick", "showHideSubQuestion(" + ((RadioButton)e.Item.FindControl("rdoYes")).ClientID + "," + ((RadioButton)e.Item.FindControl("rdoNo")).ClientID + "," + e.Item.ClientID + ",1," + this.itemClientID + "," + txtVisible.ClientID + ");fnOnClickRadio(" + subGridClientID + ",this,'rdoYes');");
                    this.rdoBtnN.Attributes.Add("onclick", "showHideSubQuestion(" + ((RadioButton)e.Item.FindControl("rdoYes")).ClientID + "," + ((RadioButton)e.Item.FindControl("rdoNo")).ClientID + "," + e.Item.ClientID + ",0," + this.itemClientID + "," + txtVisible.ClientID + ");fnOnClickRadio(" + subGridClientID + ",this,'rdoNo');");

                }

                string subGridID = sGrid.ClientID;
                this.txtIsChildExist = (TextBox)e.Item.FindControl("txtIsChildExist");
                RadioButton rdoBtn = (RadioButton)e.Item.FindControl("rdoYes");
                rdoBtn.Attributes.Add("onclick", "fnOnClickRadio(" + subGridID + ",this,'rdoYes');");
                this.rdoBtnY = rdoBtn;
                rdoBtn = (RadioButton)e.Item.FindControl("rdoNo");
                rdoBtn.Attributes.Add("onclick", "fnOnClickRadio(" + subGridID + ",this,'rdoNo');");
                this.rdoBtnN = rdoBtn;
                this.itemClientID = e.Item.ClientID;
                prevQuesID = cqnCode;
                subGridClientID = subGridID;
            }
        }
        //		private void subGrid_OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        //		{
        //			ListItemType elemType = e.Item.ItemType;			
        //			if ((elemType == ListItemType.Item)||(elemType == ListItemType.AlternatingItem)) 
        //			{
        //				//DataGrid dgrid = (DataGrid)sender;
        //				//RadioButton rdo = (RadioButton)e.Item.FindControl("rdoSubAns");
        //				//rdo.Attributes.Add("onclick","fnCheckUnCheck("+dgrid.ClientID+",this);");
        //				//string subanswer = ((Label)e.Item.FindControl("lblSubAnswer")).Text;
        //				//rdo.Checked = subanswer != string.Empty ? true : false;
        //			}
        //		}

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

        private string proposal = string.Empty;
        private string jointLife = string.Empty;
        private string prodCode = string.Empty;
        private string prevQuesID = string.Empty;
        private string itemClientID = string.Empty;
        private string subGridClientID = string.Empty;
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
            this.dGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dGrid_OnItemDataBound);

        }
        #endregion

        protected void ddlNPH_JOINTlIFE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SHMA.Enterprise.DataHolder dataHolder = new SHMA.Enterprise.DataHolder();
                this.proposal = SessionObject.GetString("NP1_PROPOSAL");
                this.prodCode = SessionObject.GetString("PPR_PRODCD");
                dGrid.DataSource = getDataSource(dataHolder);
                dGrid.DataBind();
            }
            catch (Exception err)
            {

            }
           
        }

        #region "ImranCode"
        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;


        }
        public DataTable GetdataOraOledb(string sql)
        {
            OleDbCommand cmd = new OleDbCommand();
            //  cmd.Connection = conOra; old
            cmd.Connection = GetConn();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Reset();
            dt.Clear();

            da.Fill(dt);
            //cmd.Dispose(); old
            cmd.Connection.Close();

            return dt;


        }

        public string DML(string sql)
        {
            try
            {
                CmdDML.CommandText = sql;
                CmdDML.Connection = GetConn();
                CmdDML.Connection.Open();
                CmdDML.ExecuteNonQuery();
                CmdDML.Connection.Close();

                return "done";
            }

            catch (Exception ex)
            {

                CmdDML.Connection.Close();
                return ex.Message;

            }

        }

        protected void ShowAlert(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
        }


        private void SaveOrUpdateQuestion(string proposalNo,string ProdCode, string qCode, string answer, string remarks)
        {
           
            string connStr = System.Configuration.ConfigurationManager.AppSettings["DSN"];

            using (OleDbConnection conn = new  OleDbConnection(connStr))
            {
                conn.Open();

                string checkQuery = @"SELECT COUNT(*) FROM LNQN_QUESTIONNAIRE WHERE np1_proposal = ? AND cqn_code = ?";
                OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Proposal_No", proposalNo);
                checkCmd.Parameters.AddWithValue("@QCode", qCode);
              
             //   int count = (int)checkCmd.ExecuteScalar();

                object result = checkCmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                string query;
                if (count > 0)
                {
                    // Update
                    query = "UPDATE LNQN_QUESTIONNAIRE SET nqn_answer='" + answer + "' , nqn_remarks='" + remarks + 
                        "' WHERE np1_proposal ='" + proposalNo + "' AND cqn_code ='" + qCode + "'";

                    string Msg = DML(query);

                }
                else
                {
                    // Insert
                    query = @"INSERT INTO LNQN_QUESTIONNAIRE(np1_proposal,np2_setno,ppr_prodcd,cqn_code,nqn_answer,nqn_remarks) 

                      VALUES (?,'1',?, ?, ?, ?)";
                }

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter { Value = proposalNo });
                    cmd.Parameters.Add(new OleDbParameter { Value = ProdCode });
                    cmd.Parameters.Add(new OleDbParameter { Value = qCode });
                    cmd.Parameters.Add(new OleDbParameter { Value = answer });
                    cmd.Parameters.Add(new OleDbParameter { Value = remarks });

                    
                    

                    cmd.ExecuteNonQuery();
                }

               


            }
        }



        #endregion

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Get the data value from the current row's DataItem
                string answer = DataBinder.Eval(e.Row.DataItem, "Answer")?.ToString();

                // Get radio button controls
                RadioButton rdoYes = (RadioButton)e.Row.FindControl("rdoYes");
                RadioButton rdoNo = (RadioButton)e.Row.FindControl("rdoNo");

                // Set checked based on value from database
                if (answer == "Y")
                {
                    rdoYes.Checked = true;
                }
                else if (answer == "N")
                {
                    rdoNo.Checked = true;
                }

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string M_Proposal = SessionObject.GetString("NP1_PROPOSAL");
            string PrdCode= SessionObject.GetString("PPR_PRODCD");

           // ShowAlert(M_Proposal);
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                   // string qCode = ((Label)row.FindControl("CQN_CODE"))?.Text ?? ""; // use BoundField or Label

                    string qCode = GridView1.DataKeys[row.RowIndex].Value.ToString();
                    RadioButton rdoYes = (RadioButton)row.FindControl("rdoYes");
                    RadioButton rdoNo = (RadioButton)row.FindControl("rdoNo");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                    string answer = rdoYes.Checked ? "Y" : rdoNo.Checked ? "N" : "";
                    string remarks = txtRemarks.Text.Trim();


                    SaveOrUpdateQuestion(M_Proposal, PrdCode, qCode, answer, remarks);
                }
            }
            ShowAlert("Saved successfully!");

            // Optional: success message

            // Response.Write("<script> alert('')</script>");


        }


    }
}
