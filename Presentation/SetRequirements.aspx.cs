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
    /// Summary description for SetRequirements.
    /// </summary>
    
    public partial class SetRequirements : SHMA.Enterprise.Presentation.TwoStepController
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
                    status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                    proposal = ((Label)item.FindControl("lblProposal")).Text;
                    comments = ((TextBox)item.FindControl("txtComments")).Text;
                    Session["ProposalNo"] = proposal;


                    ///======== MARK PROPOSAL UNCOLLECTED -------- 07-May-2018
                    CheckBox ChkUncollected = ((CheckBox)item.FindControl("ChkUncollected"));

                    if (ChkUncollected.Checked == true)
                    {


                        string[] ChannelDet = CCS_CHANLSUBDETLDB.GetBranchDetails(proposal).Split(',');
                        if (ChannelDet[0].ToString() == "2" && ChannelDet[1].ToString() == "4")
                        { 
                            LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "C"); 
                        }
                        else 
                        { 
                            LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "R"); 
                        }
                        Bind_Data();
                    }

                    ///======== MARK PROPOSAL UNCOLLECTED -------- 07-May-2018	

                    if (status == ".")
                    { continue; }//Nothing

                    //					if(SessionObject.Get("s_USE_TYPE")!=null && SessionObject.GetString("s_USE_TYPE")=="M") //BM
                    //					{
                    //						if(status=="Y"  )//Collected
                    //						{
                    //							status = "Y-FromCallCenter";			// Approved by BM and Call Center Agent
                    //						}
                    //						else if(status=="N")//UnCollected
                    //							status = "C";
                    //						comments="Payment not Collected";
                    //					}
                    //					else if(SessionObject.Get("s_USE_TYPE")!=null && SessionObject.GetString("s_USE_TYPE")=="L") //Call Center Agent
                    //					{
                    //						if(status=="Y"  )//Approved
                    //						{
                    //							status = "C";
                    //						}
                    //						else if(status=="N")//Returned
                    //							status = "P";
                    //					}

                    try
                    {
                        //SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal,Convert.ToDateTime(Session["s_COMM_DATE"]));					
                      //  LNP1_POLICYMASTR.markStatus(proposal,status);	// already stop
                        if (status == "Decs")
                        {
                            RemoveSubstandard();

                            string sqlString = "Select p.np1_selected, p.cdc_code, c.cm_comments\n" +
                            "  From lnp1_policymastr p\n" +
                            "  left outer join lncm_comments c\n" +
                            "    on p.np1_proposal = c.np1_proposal\n" +
                            " where p.np1_proposal in ('"+proposal+"')";

                            rowset rs = DB.executeQuery(sqlString);
                            if (rs.next())
                            {
                                if (Convert.ToString(rs.getObject(1)) == "" || Convert.ToString(rs.getObject(2)) == "002" || Convert.ToString(rs.getObject(3)) == "Refered")
                                {
                                    cmObj.AddComentsInTable(proposal, comments, status);
                                    showAlertMessage("Successfully mark the Proposal to Decision.");
                                }
                                else
                                {
                                    showAlertMessage("You cannot mark the Proposal to Decision.");
                                }
                            }
                        }
                        else 
                        {
                            cmObj.AddComentsInTable(proposal, comments, status);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n", "").Replace("\"", "'").Replace("\r", ""));
                        errMessage.Append("\\n");
                    }
                }

                //				dGrid.DataSource = getDataSource(dataHolder);
                //				dGrid.DataBind();		
            }
            else
            {
                showAlertMessage("There is nothing to save.");
            }
        }


        #endregion

        #region " Supported Methods "
        private void RemoveSubstandard()
        {
            string Sql = "Select p.np2_substandar,c.cm_comments   From lnp2_policymastr p   left outer join lncm_comments c " +
                       " on p.np1_proposal = c.np1_proposal  where p.np1_proposal = '" + Session["ProposalNo"].ToString()  + "' and c.cm_serial_no = 1 and c.cm_commentby like 'BCO%'";
            rowset rs = DB.executeQuery(Sql);

            if (rs.next())
            {
                if (Convert.ToString(rs.getObject(1)) == "Y" && Convert.ToString(rs.getObject(2)) == "Posted")
                {
                    DB.executeDML("update lnp2_policymastr set np2_substandar = 'N' where np1_proposal = '" + Session["ProposalNo"].ToString() + "'");
                 
                }

            }

            }
            private void showAlertMessage(string message_)
        {
            HeaderScript.Text = "alert(\"" + message_ + "\");";
        }
        private DataTable getDataSourceReader(DataHolder ds, string extraClause)
        {

            string user = System.Convert.ToString(Session["s_USE_USERID"]);
            string userType = ace.Ace_General.getUserType(user);
            string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);

            DataTable table = new DataTable("LNP1_POLICYMASTR_DATA");

            IDataReader tempReader = new LNP1_POLICYMASTRDB(dataHolder).getProposalListforDecision(extraClause, user, userType, branchCode);

            Utilities.Reader2Table(tempReader, table);

            tempReader.Close();
            return (table);
        }
        /*private NameValueCollection getNameValueCollection(string proposal,string comments,string status)
		{
			NameValueCollection nvc = new NameValueCollection();
			nvc.add("NP1_PROPOSAL",proposal);
			nvc.add("CM_SERIAL_NO",LNCM_COMMENTSDB.getNextSerial(proposal).ToString());			
			nvc.add("CM_COMMENTDATE",DateTime.Now);			
			nvc.add("CM_COMMENTBY",SessionObject.GetString("s_USE_USERID"));
			nvc.add("CM_ACTION",status);
			nvc.add("CM_COMMENTS",comments);
			return nvc;
		}*/

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
                /*
				string status = ((DataRowView)(e.Item.DataItem)).Row["np1_selected"].ToString();

				Label lblStatus = ((Label)e.Item.FindControl("lblStatus"));
				lblStatus.Text = status.Equals("R") ? "Ok" : "Not Ok";

				DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
				//ddlStatus.Enabled = status.Equals("P");
				ListItem item = ddlStatus.Items.FindByValue(status.Equals("R") ? "Y" : "N");
				if(item != null)
				{
					item.Selected = true;
				}
*/
                LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
                string porposal = ((DataRowView)(e.Item.DataItem)).Row.ItemArray[0].ToString();//NP1_PROPOSAL
                IDataReader temp = cmt.getCommentsOfPorposal(porposal);

                Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
                repAllComments.DataSource = temp;
                repAllComments.DataBind();
                temp.Close();


                string action = cmt.getActionforProposal(porposal);

                DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
                //ddlStatus.SelectedValue=dt.Rows[0]["CM_ACTION"].ToString();
                if (action == "CBC-Decs" || action == "Decs")
                {
                    action = "Decs";
                    ddlStatus.SelectedValue = action;
                }
                else if (action == "Req")
                {
                    ddlStatus.SelectedValue = action;
                }


                //========== FOR UNCOLLECTED CHECKBOX -------- 07-May-2018		
                Label lblStatus = ((Label)e.Item.FindControl("lblStatus"));
                CheckBox ChkUncollected = ((CheckBox)e.Item.FindControl("ChkUncollected"));
                if (lblStatus.Text == "Payment Deducted")
                { ChkUncollected.Visible = true; }
                else { ChkUncollected.Visible = false; }
                //========== FOR UNCOLLECTED CHECKBOX -------- 07-May-2018		

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
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.dGrid.ItemDataBound += new DataGridItemEventHandler(dGrid_ItemDataBound);

        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String searchClause = string.Empty;

            if (txtSearch.Text != "")
            {
                searchClause = "upper(" + ddlSearchBy.SelectedValue + ") like upper('" + txtSearch.Text.Replace("'", "").Trim() + "')";//"np1_proposal="+txtSearch.Text;
            }
            //TODO:CNIC SEARCH//String searchClause = "inq.nph_idno="+txtSearch.Text;



            String strSearch = txtSearch.Text;
            dataHolder = (DataHolder)Session["dataHolder"];

            //IDataReader temp =getDataSourceReader(dataHolder, String.Empty);
            DataTable temp = getDataSourceReader(dataHolder, searchClause);

            dGrid.DataSource = temp;
            dGrid.DataBind();
            //temp.Close();

        }

        private void Bind_Data()
        {
            String searchClause = string.Empty;

            if (txtSearch.Text != "")
            {
                searchClause = "upper(" + ddlSearchBy.SelectedValue + ") like upper('" + txtSearch.Text.Replace("'", "").Trim() + "')";//"np1_proposal="+txtSearch.Text;
            }
            //TODO:CNIC SEARCH//String searchClause = "inq.nph_idno="+txtSearch.Text;



            String strSearch = txtSearch.Text;
            dataHolder = (DataHolder)Session["dataHolder"];

            //IDataReader temp =getDataSourceReader(dataHolder, String.Empty);
            DataTable temp = getDataSourceReader(dataHolder, searchClause);

            dGrid.DataSource = temp;
            dGrid.DataBind();
        }
    }

















}
