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
    /// <summary>
    /// Summary description for ReferredListForCallCenterApproval.
    /// </summary>
    public partial class ReferredListForCallCenterApproval : SHMA.Enterprise.Presentation.TwoStepController
    {
        #region " First Step "		 

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            Session["dataHolder"] = dataHolder;
            dGrid.DataSource = getDataSourceReader(dataHolder, "1<>1");
            dGrid.DataBind();
            return dataHolder;
        }

        sealed protected override void BindInputData(SHMA.Enterprise.DataHolder dataHolder)
        {
            CSSLiteral.Text = ace.Ace_General.loadMainStyle();
        }

        protected override void PrepareInputUI(DataHolder dataHolder)
        {
        }

        #endregion

        #region " Second Step "
        protected override void DataBind(DataHolder dataHolder)
        {

        }

        protected override void ApplyDomainLogic(DataHolder dataHolder)
        {
            string proposal = string.Empty;
            string status = string.Empty;
            string comments = string.Empty;
            string cvsProposalNos = string.Empty;
            System.Text.StringBuilder errMessage = new System.Text.StringBuilder();

            dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
            LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);

            string ChannelCode = System.Convert.ToString(Session["s_CCH_CODE"]);
            string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);

            if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
            {
                foreach (DataGridItem item in dGrid.Items)
                {
                    //status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                    ////============ ADDED 06-FEB-2018
                    status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                    ////============ ADDED 06-FEB-2018
                    proposal = ((Label)item.FindControl("lblProposal")).Text;
                    comments = ((TextBox)item.FindControl("txtComments")).Text;

                    if (status == ".") { continue; }//Nothing


                    try
                    {
                        if (status == "Y")//Update Comment to Scpecific Proposal
                        {
                            status = "CBC-Decs";
                        }
                        else if (status == "K")//Reffered Hold
                            status = "K";
                        else if (status == "D")//Declined
                            status = "D";

                        ////============ ADDED 06-FEB-2018
                        //if (status=="Y")//Update Comment to Scpecific Proposal
                        //{

                        //}
                        string stforcbc = string.Empty;

                        if (status == "CBC-Decs")
                        {
                            stforcbc = "P";
                        }
                        else
                        {
                            stforcbc = status;
                        }

                        LNP1_POLICYMASTR.markStatus(proposal, stforcbc);

                        cmObj.AddComentsInTable(proposal, comments, status);
                        ////============ ADDED 06-FEB-2018
                    }
                    catch (Exception ex)
                    {
                        errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                        errMessage.Append("\\n");
                    }
                }

                dGrid.DataSource = getDataSourceReader(dataHolder, "1<>1");
                dGrid.DataBind();
            }
            else
            {
                showAlertMessage("There is nothing to save.");
            }
        }


        #endregion

        #region " Supported Methods "

        private void showAlertMessage(string message_)
        {
            HeaderScript.Text = "alert(\"" + message_ + "\");";
        }
        private DataTable getDataSourceReader(DataHolder ds, string extraClause)
        {

            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);
            string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);
            string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);

            DataTable table = new DataTable("LNP1_POLICYMASTR_DATA");

            IDataReader tempReader = new LNP1_POLICYMASTRDB(dataHolder).getProposalListforDecision(extraClause, user, userType, branchCode, bankCode);

            Utilities.Reader2Table(tempReader, table);

            tempReader.Close();
            return (table);
        }
        #endregion

        #region " Events "

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
        private void dGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
                string porposal = ((DataRowView)(e.Item.DataItem)).Row.ItemArray[0].ToString();//NP1_PROPOSAL
                IDataReader temp = cmt.getCommentsOfPorposal(porposal);

                Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
                repAllComments.DataSource = temp;
                repAllComments.DataBind();
                temp.Close();

                //					string action = cmt.getActionforProposal(porposal);
                //
                //					DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
                //					ddlStatus.SelectedValue= action;


                if (repAllComments.Items.Count == 0)
                    repAllComments.Visible = false;

                e.Item.Attributes.Add("onmouseout", "hideComments(" + porposal + ")");

                //LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
                //btn.Attributes.Add("onclick","setValue('"+btn.Text+"');executeReport('PROFILE');");
                //btn.Text
            }
        }


        #endregion

        #region " Class Variable "

        protected System.Web.UI.WebControls.TextBox txtSearch;
        protected System.Web.UI.WebControls.DropDownList ddlSearchBy;
        protected System.Web.UI.WebControls.Button btnSearch;
        protected System.Web.UI.WebControls.Literal _result;
        protected System.Web.UI.WebControls.Literal HeaderScript;

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
            this.dGrid.ItemDataBound += new DataGridItemEventHandler(dGrid_ItemDataBound);

        }
        #endregion

    }

















}
