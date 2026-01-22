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
	public partial class ManualPolicyIssuanceOfPendingProposalsView : SHMA.Enterprise.Presentation.TwoStepController
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

			if(dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
			{
				foreach(DataGridItem item in dGrid.Items)
				{
					status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;

					statusText = ((DropDownList)item.FindControl("ddlStatus")).SelectedItem.ToString();
					proposal = ((Label)item.FindControl("lblProposal")).Text;
					comments = ((TextBox)item.FindControl("txtComments")).Text;					
					
					if(status=="."){continue;}//Nothing
					
					if(status=="RePost")
					{
						try
						{						
							LNP1_POLICYMASTR.markStatus(proposal,"P"/*status*/);												
							cmObj.AddComments(proposal,comments,statusText);
						}
						catch(Exception ex)
						{
							errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n","").Replace("\"","'").Replace("\r",""));
							errMessage.Append("\\n");						
						}
					}
					else if (status=="Modify")
					{					
						try
						{						
							LNP1_POLICYMASTR.markStatus(proposal,"null"/*status*/);												
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
			ds = new LNP1_POLICYMASTRDB(dataHolder).getUnApprovedProposalList(string.Empty,  System.Convert.ToString(Session["s_USE_USERID"]));
			
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
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
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

				LNCM_COMMENTSDB cmt = new LNCM_COMMENTSDB(dataHolder);
				string porposal = ((DataRowView)(e.Item.DataItem)).Row["NP1_PROPOSAL"].ToString();
				IDataReader temp = cmt.getCommentsOfPorposal(porposal);

				Repeater repAllComments = ((Repeater)e.Item.FindControl("repAllComments"));
				repAllComments.DataSource = temp;
				repAllComments.DataBind();

				temp.Close();

				if(repAllComments.Items.Count==0)
					repAllComments.Visible=false;
				
				//e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
				//e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
				e.Item.Attributes.Add("onmouseout","hideComments("+porposal+")");

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
			this.dGrid.ItemDataBound+=new DataGridItemEventHandler(dGrid_ItemDataBound);

		}
		#endregion
		
	}
}
