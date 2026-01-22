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
using System.IO;
using System.Configuration;
using System.Text;

namespace SHAB.Presentation
{
    /// <summary>
    /// Summary description for ManualPolicyIssuanceOfPendingProposals.
    /// </summary>
    public partial class PendingListForCallCenterApproval : SHMA.Enterprise.Presentation.TwoStepController
    {
        #region " First Step "	
        Timer timer = new Timer();

        protected override DataHolder GetInputData(DataHolder dataHolder)
        {
            if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "M" || SessionObject.GetString("s_USE_TYPE") == "W") //BM also for JSB WMO user
            {
                BMGrid.Visible = true;
                dGrid.Visible = false;
                BMGrid.DataSource = getDataSource(dataHolder);
                BMGrid.DataBind();
            }
            else
            {
                BMGrid.Visible = false;
                dGrid.Visible = true;
                dGrid.DataSource = getDataSource(dataHolder);
                dGrid.DataBind();
            }
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
            bool validate = true;
            bool Amountvalidate = true;
            bool Datevalidate = true;
            string proposal = string.Empty;
            string status = string.Empty;
            string comments = string.Empty;
            double collection = 0;
            double collectioncheck = 0;
            string collectionDate = "";
            string cvsProposalNos = string.Empty;
            System.Text.StringBuilder errMessage = new System.Text.StringBuilder();

            dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
            LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);
            switch ((EnumControlArgs)ControlArgs[0])
            {
                case (EnumControlArgs.Save):
                    string ChannelCode = System.Convert.ToString(Session["s_CCH_CODE"]);
                    string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
                    if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "M" || SessionObject.GetString("s_USE_TYPE") == "W") //BM and WMO JSB
                    {
                        if (BMGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
                        {
                            foreach (DataGridItem item in BMGrid.Items)
                            {
                                status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                                proposal = ((Label)item.FindControl("lblProposal")).Text;
                                comments = ((TextBox)item.FindControl("txtComments")).Text;

                                if (status == ".") { continue; }//Nothing

                                collection = Convert.ToDouble(((TextBox)item.FindControl("txtCollectionAmount")).Text);
                                collectioncheck = Convert.ToDouble(((Label)item.FindControl("lblTotPremium")).Text);
                                collectionDate = ((TextBox)item.FindControl("txt_collection_Date")).Text;

                                if (status == "Y")//Collected
                                {
                                    //chg_closing
                                    //if (Convert.ToString(Session["ClossingFlag"]) == "P")
                                    //{
                                    //    if (Convert.ToDateTime(collectionDate).ToString("dd/MM/yyyy") != Convert.ToDateTime(Convert.ToString(Session["ClossingDate"])).ToString("dd/MM/yyyy"))
                                    //    {
                                    //        showAlertMessage("Please enter previous Month's Last Date");
                                    //        return;
                                    //    }
                                    //}
                                    //chg_closing_end
                                    status = "Y-FromCallCenter";// Approved by BM and Call Center Agent
                                    if (collection != collectioncheck)
                                    {
                                        Amountvalidate = false;
                                        break;
                                    }
                                    if (collectionDate == "")
                                    {
                                        Datevalidate = false;
                                        break;
                                    }
                                    else
                                    {
                                        if (!CheckDateformat(collectionDate))
                                        {
                                            Datevalidate = false;
                                            break;
                                        }
                                    }
                                    comments = "Payment Collected";
                                }
                                else if (status == "N")//UnCollected
                                {
                                    status = "C";
                                    comments = "Payment not Collected";
                                }
                                try
                                {
                                    SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(Session["s_COMM_DATE"]));
                                    LNP1_POLICYMASTR.markStatus(proposal, status);
                                    cmObj.AddComentsInTable(proposal, comments, status == "Y-FromCallCenter" ? "Collected" : "UnCollected");
                                    if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "M" && status == "Y-FromCallCenter") //BM
                                    {
                                        LNP1_POLICYMASTR.insertCollection(proposal, SessionObject.GetString("s_USE_USERID"), collection, Convert.ToDateTime(collectionDate).ToString("yyyy-MM-dd"));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                                    errMessage.Append("\\n");
                                }
                            }
                            if (validate == false)
                            {
                                showAlertMessage("Enter Comments");
                                return;
                            }
                            if (Amountvalidate == false)
                            {
                                showAlertMessage("Collection Amount not equal to Premium Amount.");
                                return;
                            }
                            if (Datevalidate == false)
                            {
                                showAlertMessage("Collection Date is empty and Invalid format");
                                return;
                            }
                            BMGrid.DataSource = getDataSource(dataHolder);
                            BMGrid.DataBind();
                        }
                        else
                        {
                            showAlertMessage("There is nothing to save...");
                        }
                    }
                    else if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "O")
                    {
                        if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
                        {
                            foreach (DataGridItem item in dGrid.Items)
                            {
                                status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                                proposal = ((Label)item.FindControl("lblProposal")).Text;
                                comments = ((TextBox)item.FindControl("txtComments")).Text;

                                if ((status == "D" || status == "H") && (comments == null || comments == ""))
                                {
                                    validate = false;
                                    break;
                                }

                                if (status == ".") { continue; }//Nothing

                                if (status == "Y")//Approved
                                {
                                    status = "R";
                                }
                                else if (status == "N")//Returned
                                    status = "";
                                else if (status == "H")//Hold
                                    status = "H";
                                else if (status == "D")//Declined
                                    status = "D";
                                else if (status == "A")//Discrepant
                                    status = "";
                              //  else if (status == "B")//Rejected
                              //      status = "D";

                                try
                                {
                                    SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(Session["s_COMM_DATE"]));
                                    //SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(Session["ClossingDate"]));    //chg_closing   comments above
                                    LNP1_POLICYMASTR.markStatus(proposal, status);
                                    cmObj.AddComentsInTable(proposal, comments, status);
                                }
                                catch (Exception ex)
                                {
                                    errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                                    errMessage.Append("\\n");
                                }
                            }
                            if (validate == false)
                            {
                                showAlertMessage("Enter Comments");
                                return;
                            }
                            dGrid.DataSource = getDataSource(dataHolder);
                            dGrid.DataBind();

                        }
                        else
                        {
                            showAlertMessage("There is nothing to save.");
                        }
                    }
                    else
                    {
                        if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
                        {
                            foreach (DataGridItem item in dGrid.Items)
                            {
                                status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                                proposal = ((Label)item.FindControl("lblProposal")).Text;
                                comments = ((TextBox)item.FindControl("txtComments")).Text;

                                if ((status == "D" || status == "H") && (comments == null || comments == ""))
                                {
                                    validate = false;
                                    break;
                                }

                                if (status == ".") { continue; }//Nothing

                                if (status == "Y")//Approved
                                {
                                    status = "C";
                                }
                                else if (status == "N")//Returned
                                {
                                    status = "P";
                                }
                                    //if (Session["BankCode"].ToString() == "F")
                                    //{ status = "R"; }  // for JSB return to WMO }
                                    //else

                                  

                                else if (status == "H")//Hold
                                    status = "H";
                                else if (status == "D")//Declined
                                    status = "D";

                                try
                                {
                                    SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(Session["s_COMM_DATE"]));
                                    //SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(Session["ClossingDate"]));  //chg_closing  comments above
                                    LNP1_POLICYMASTR.markStatus(proposal, status);
                                    cmObj.AddComentsInTable(proposal, comments, status);
                                }
                                catch (Exception ex)
                                {
                                    errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                                    errMessage.Append("\\n");
                                }
                            }
                            if (validate == false)
                            {
                                showAlertMessage("Enter Comments");
                                return;
                            }
                            dGrid.DataSource = getDataSource(dataHolder);
                            dGrid.DataBind();

                        }
                        else
                        {
                            showAlertMessage("There is nothing to save.#");
                        }
                    }
                    break;
                case (EnumControlArgs.Process):
                    if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "M" || SessionObject.GetString("s_USE_TYPE") == "W") //BM
                    {
                        BMGrid.Visible = true;
                        dGrid.Visible = false;
                        BMGrid.DataSource = getDataSource(dataHolder);
                        BMGrid.DataBind();
                    }
                    else
                    {
                        BMGrid.Visible = false;
                        dGrid.Visible = true;
                        dGrid.DataSource = getDataSource(dataHolder);
                        dGrid.DataBind();
                    }
                    break;
            }
            

        }
        private bool CheckDateformat(string date)
        {
            DateTime dDate;
            if (DateTime.TryParse(date, out dDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region " Supported Methods "

        private void showAlertMessage(string message_)
        {
            HeaderScript.Text = "alert(\"" + message_ + "\");";
        }
        private DataTable getDataSource(DataHolder ds)
        {
            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);
            string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
            string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);

            if (userType == "M" || userType == "W")
            {
                ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("C", user, userType, bankCode, branchCode, false);
            }
            else if (userType == "L")
            {
                ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("R", user, userType, bankCode, branchCode, false);
            }
            else if (userType == "O")
            {
                ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("C", user, userType, bankCode, branchCode, false);
            }

            string Comments = "Re-Calculate Premium as Client Age has been Changed";
            string Status = "NOT OK";
            string proposal = "";

            //Remove Proposals In Which Client Age has been Changed
            //			for(int i=0;i<ds["LNP1_POLICYMASTR_DATA"].Rows.Count;i++)
            //			{
            //				proposal=ds["LNP1_POLICYMASTR_DATA"].Rows[i]["np1_proposal"].ToString();					
            //				bool IsClientAgeChanged= LNP1_POLICYMASTRDB.CheckAge(proposal);
            //				if(!IsClientAgeChanged)
            //				{
            //					try
            //					{
            //						LNP1_POLICYMASTR.markStatus(proposal,"ReCal"/*status*/);												
            //						LNCM_COMMENTSDB.AddUserComments(proposal,Comments,Status);
            //						ds["LNP1_POLICYMASTR_DATA"].Rows[i].Delete();
            //					}
            //					catch(Exception ex)
            //					{
            //					
            //					}
            //				}
            //			}

            return ds["LNP1_POLICYMASTR_DATA"];
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
                string porposal = ((DataRowView)(e.Item.DataItem)).Row["NP1_PROPOSAL"].ToString();
                IDataReader temp = cmt.getCommentsOfPorposal(porposal);

                Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
                repAllComments.DataSource = temp;
                repAllComments.DataBind();

                temp.Close();

                if (repAllComments.Items.Count == 0)
                    repAllComments.Visible = false;

                //e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
                //e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
                e.Item.Attributes.Add("onmouseout", "hideComments(" + porposal + ")");

                //LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
                //btn.Attributes.Add("onclick","setValue('"+btn.Text+"');executeReport('PROFILE');");
                //btn.Text
            }
        }

        #endregion

        #region " Class Variable "

        protected System.Web.UI.WebControls.Literal _result;
        protected System.Web.UI.WebControls.Literal HeaderScript;
        protected System.Web.UI.WebControls.HyperLink hlk;
        protected System.Web.UI.WebControls.Literal callJs;

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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
        private void InitializeComponent()
        {
            this.dGrid.ItemDataBound += new DataGridItemEventHandler(dGrid_ItemDataBound);
            this.BMGrid.ItemDataBound += new DataGridItemEventHandler(dGrid_ItemDataBound);
        }

        protected void BMGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            dGrid_ItemDataBound(sender, e);
        }

        protected void dGrid_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            dGrid_ItemDataBound(sender,e);
        }
        #endregion

        //protected void OnTimerEvent(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        dataHolder = new DataHolder();
        //        if (SessionObject.Get("s_USE_TYPE") != null && SessionObject.GetString("s_USE_TYPE") == "M") //BM
        //        {
        //            BMGrid.Visible = true;
        //            dGrid.Visible = false;
        //            BMGrid.DataSource = getDataSource(dataHolder);
        //            BMGrid.DataBind();
        //        }
        //        else
        //        {
        //            BMGrid.Visible = false;
        //            dGrid.Visible = true;
        //            dGrid.DataSource = getDataSource(dataHolder);
        //            dGrid.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}
