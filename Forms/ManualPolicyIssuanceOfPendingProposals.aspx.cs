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
	/// Summary description for ManualPolicyIssuanceOfPendingProposals.
	/// </summary>
	public partial class ManualPolicyIssuanceOfPendingProposals : SHMA.Enterprise.Presentation.TwoStepController
	{
		#region " First Step "		 
		
		protected override DataHolder GetInputData(DataHolder dataHolder)
		{	
			dGrid.DataSource = getDataSource(dataHolder);
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
			string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);			
			dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();
			
		}

		protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			string proposal = string.Empty;
			string status = string.Empty;
			string comments = string.Empty;
			string statusText = string.Empty;
			DB.BeginTransaction();
			SaveTransaction = true;
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
			dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
			LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);
           

            if (dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
			{
				foreach(DataGridItem item in dGrid.Items)
				{
                    status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
                    statusText = ((DropDownList)item.FindControl("ddlStatus")).SelectedItem.ToString();
                    proposal = ((Label)item.FindControl("lblProposal")).Text;
                    comments = ((TextBox)item.FindControl("txtComments")).Text;


                    //DropDownList ddlStatus = (DropDownList)item.FindControl("ddlStatus");
                    //Label lblProposal = (Label)item.FindControl("lblProposal");
                    //TextBox txtComments = (TextBox)item.FindControl("txtComments");

                    // status = ddlStatus.SelectedValue;
                    //statusText = ddlStatus.SelectedItem.Text;   
                    // proposal = lblProposal.Text;
                    // comments = txtComments.Text;


                    if (status=="."){continue;}//Nothing

					try
					{
                        

                        LNP1_POLICYMASTR.markStatus(proposal,status);												
						cmObj.AddComments(proposal,comments,statusText);
					}
					catch(Exception ex)
					{
						errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n","").Replace("\"","'").Replace("\r",""));
						errMessage.Append("\\n");						
					}
				}
				if(errMessage.Length != 0)
				{
					showAlertMessage(errMessage.ToString());					
				}
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
			HeaderScript.Text = "alert(\""+message_+"\");";
		}
		private DataTable getDataSource(DataHolder ds)
		{	

			string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);		
			string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
			string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);

			ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList(string.Empty, user,userType,bankCode, branchCode,false);
			
			string Comments="Re-Calculate Premium as Client Age has been Changed";
			string Status="NOT OK";
			string proposal="";

			//Remove Proposals In Which Client Age has been Changed
			for(int i=0;i<ds["LNP1_POLICYMASTR_DATA"].Rows.Count;i++)
			{
				proposal=ds["LNP1_POLICYMASTR_DATA"].Rows[i]["np1_proposal"].ToString();					
				bool IsClientAgeChanged= LNP1_POLICYMASTRDB.CheckAge(proposal);
				if(!IsClientAgeChanged)
				{
					try
					{
						LNP1_POLICYMASTR.markStatus(proposal,"ReCal"/*status*/);												
						LNCM_COMMENTSDB.AddUserComments(proposal,Comments,Status);
						ds["LNP1_POLICYMASTR_DATA"].Rows[i].Delete();
					}
					catch(Exception ex)
					{
					
					}
				}
			}

			
			return ds["LNP1_POLICYMASTR_DATA"];
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
				case "Update" :
					ControlArgs[0]=EnumControlArgs.Update;
					DoControl();
					break;
				case "Save" :
					ControlArgs[0]=EnumControlArgs.Save;
					DoControl();
					break;
				case "Delete" :
					ControlArgs[0]=EnumControlArgs.Delete;
					DoControl();
					break;
				case "Filter" :
					ControlArgs[0] = EnumControlArgs.Filter;
					DoControl();
					break;
				case "Process" :
					ControlArgs[0] = EnumControlArgs.Process;
					DoControl();
					break;	

			}
			_CustomEventVal.Value="";										
		}
        private void dGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // old code
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string status = ((DataRowView)(e.Item.DataItem)).Row["np1_selected"].ToString();
                DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
                //ddlStatus.Enabled = status.Equals("P"); old for showing only np1_selected=P

                ddlStatus.Enabled = status.Equals("P") || status.Equals("G"); // enable


                /*
       ListItem item = ddlStatus.Items.FindByValue(status.Equals("R") ? "Y" : "N");
       if(item != null)
       {
           item.Selected = true;
       }
       */

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
    



        // old code
        //if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //	string status = ((DataRowView)(e.Item.DataItem)).Row["np1_selected"].ToString();
        //	DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
        //             //ddlStatus.Enabled = status.Equals("P"); old for showing only np1_selected=P

        //             ddlStatus.Enabled = status.Equals("P") || status.Equals("G"); // enable


        //             /*
        //	ListItem item = ddlStatus.Items.FindByValue(status.Equals("R") ? "Y" : "N");
        //	if(item != null)
        //	{
        //		item.Selected = true;
        //	}
        //	*/

        //             LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
        //	string porposal = ((DataRowView)(e.Item.DataItem)).Row["NP1_PROPOSAL"].ToString();
        //	IDataReader temp = cmt.getCommentsOfPorposal(porposal);

        //	Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
        //	repAllComments.DataSource = temp;
        //	repAllComments.DataBind();

        //	temp.Close();

        //	if(repAllComments.Items.Count==0)
        //		repAllComments.Visible=false;

        //	//e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
        //	//e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
        //	e.Item.Attributes.Add("onmouseout","hideComments("+porposal+")");

        //	//LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
        //	//btn.Attributes.Add("onclick","setValue('"+btn.Text+"');executeReport('PROFILE');");
        //	//btn.Text
        //}
        // }





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
			this.dGrid.ItemDataBound+=new DataGridItemEventHandler(dGrid_ItemDataBound);

		}
        #endregion

        protected void dGrid_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            //{
            //    if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            //        return;

            //    // --- Retrieve data for this row ---
            //    DataRowView rowView = (DataRowView)e.Item.DataItem;
            //    string status = rowView["np1_selected"].ToString();
            //    string proposal = rowView["NP1_PROPOSAL"].ToString();

            //    // --- Find controls ---
            //    DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");
            //    Repeater repAllComments = (Repeater)e.Item.FindControl("repAllComments");

            //    if (ddlStatus != null)
            //    {
            //        // --- Populate LOV only if empty (preserves user selection on postback) ---
            //        if (ddlStatus.Items.Count == 0)
            //        {
            //            string bankCode = Convert.ToString(Session["BankCode"]);
            //            string userType = Convert.ToString(Session["UserType"]);

            //            if (bankCode == "F" && userType == "W")
            //            {
            //                ddlStatus.Items.Add(new ListItem(".", "."));
            //                ddlStatus.Items.Add(new ListItem("Proceed to CBC", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Discrepant", "T"));
            //                ddlStatus.Items.Add(new ListItem("Rejected", "R"));
            //                ddlStatus.Items.Add(new ListItem("Declined", "D"));
            //            }
            //            else if (bankCode == "F" && userType == "M")
            //            {
            //                ddlStatus.Items.Add(new ListItem(".", "."));
            //                ddlStatus.Items.Add(new ListItem("Referred to GM", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Rejected", "N"));
            //            }
            //            else if (bankCode == "F" && userType == "H")
            //            {
            //                ddlStatus.Items.Add(new ListItem(".", "."));
            //                ddlStatus.Items.Add(new ListItem("Approved", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Rejected", "N"));
            //            }
            //            else if (bankCode == "9" && (userType == "M" || userType == "H"))
            //            {
            //                ddlStatus.Items.Add(new ListItem(".", "."));
            //                ddlStatus.Items.Add(new ListItem("OK", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Not OK", "N"));
            //            }
            //            else
            //            {
            //                // Default LOV
            //                ddlStatus.Items.Add(new ListItem(".", "."));
            //                ddlStatus.Items.Add(new ListItem("Ok", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Proceed to CBC", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Referred to GM", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Approved", "Y"));
            //                ddlStatus.Items.Add(new ListItem("Discrepant", "T"));
            //                ddlStatus.Items.Add(new ListItem("Rejected", "N"));
            //                ddlStatus.Items.Add(new ListItem("Declined", "N"));
            //                ddlStatus.Items.Add(new ListItem("Not OK", "N"));
            //            }
            //        }

            //        // --- Set selected value from np1_selected ---
            //        ListItem selectedItem = ddlStatus.Items.FindByValue(status);
            //        if (selectedItem != null)
            //            selectedItem.Selected = true;

            //        // --- Enable/disable dropdown based on status ---
            //        ddlStatus.Enabled = status.Equals("P") || status.Equals("G");
            //    }

            //    // --- Bind comments repeater ---
            //    if (repAllComments != null)
            //    {
            //        LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
            //        using (IDataReader temp = cmt.getCommentsOfPorposal(proposal))
            //        {
            //            repAllComments.DataSource = temp;
            //            repAllComments.DataBind();
            //        }

            //        repAllComments.Visible = repAllComments.Items.Count > 0;
            //    }

            //    // --- Add row onmouseout attribute ---
            //    e.Item.Attributes.Add("onmouseout", "hideComments(" + proposal + ")");
            //}
        }
    }
}
