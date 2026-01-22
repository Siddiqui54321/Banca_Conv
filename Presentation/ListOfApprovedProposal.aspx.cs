using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
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
    public partial class ListOfApprovedProposal : SHMA.Enterprise.Presentation.TwoStepController
	{
		#region " First Step "		 
		
		protected override DataHolder GetInputData(DataHolder dataHolder)
		{	
			//this.dt = getDataSource(dataHolder);			
			//dGrid.DataSource = this.dt;
			
			//this.dt = getDataSource(dataHolder);			
			//dGrid.DataSource = this.dt;
			//dGrid.DataBind();
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
			//string user = System.Convert.ToString(Session["s_USE_USERID"]);
			//string userType = ace.Ace_General.getUserType(user);			
			//this.dt = getDataSource(dataHolder);			
			dGrid.DataSource = this.dt;
			//dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();			
		}

		protected override void ApplyDomainLogic(DataHolder dataHolder)
		{	
			string fileName = FileToUpload.PostedFile.FileName;			
			//FileInfo fileInfo = new FileInfo(fileName);
			Stream inputStream = null;
			//this.dt = getDataSource(dataHolder);
			ArrayList proposalList = new ArrayList();
			ArrayList commentList = new ArrayList();
			StringBuilder errorList = new StringBuilder();
			//int index = 0;
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
			if(fileInfo.Extension.Equals(".csv"))
			{
				this.dt = getDataSource(dataHolder);
				inputStream = FileToUpload.PostedFile.InputStream;
				System.IO.StreamReader sr = new System.IO.StreamReader(inputStream);
				while (sr.Peek() >= 0)
				{
					String[] strRow = sr.ReadLine().Split(',');
					proposalList.Add(strRow[0]);
					if(strRow.Length==8)
					{
						commentList.Add(strRow[7]);
					}
					else
					{
						commentList.Add("OK");
					}

				}
			}	
			LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);
			int itr=0;
			foreach(string proposal in proposalList)
			{
				if(this.dt.Select("np1_proposal = '"+ proposal +"'").Length != 0 )
				{
					if(commentList[itr].ToString().ToUpper().IndexOf("OK")>=0)
						LNP1_POLICYMASTR.markStatus(proposal,"A");
					else
					{
						LNP1_POLICYMASTR.markStatus(proposal,"P");
						cmObj.AddComentsInTable(proposal,commentList[itr].ToString(),"NOT OK");
					}
				}
				else
				{
					errorList.Append(proposal).Append(",");
				}
				itr++;
			}
			if(errorList.Length != 0)
			{				
				showAlertMessage(errorList.Append("\\n error occured during uploading of proposals.").ToString());
			}
			int rowsCount = this.dt.Rows.Count;
			DataBind(dataHolder);
			/*ace.CSVFileGeneration fileRe = new ace.CSVFileGeneration("D:\\abc\\","abc.csv");
			string[] proposalList = fileRe.ReadFromFile();
			StringBuilder errorList = new StringBuilder();
			foreach(string proposal in proposalList)
			{
				if(this.dt.Select("np1_proposal = '"+ proposal +"'").Length != 0)
				{
					LNP1_POLICYMASTR.markStatus(proposal,"Y");
				}
				else
				{
					errorList.Append(proposal).Append(",");                    
				}
			}
			if(errorList.Length != 0)
			{
				throw new Exception(errorList.Append("\\n error occured during uploading of proposals.").ToString());
			}*/
			//verifyProposalList();
			/*string proposal = string.Empty;
			string status = string.Empty;
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
			if(dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
			{
				foreach(DataGridItem item in dGrid.Items)
				{
					//status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
					status = "F";
					proposal = ((Label)item.FindControl("lblProposal")).Text;
					try
					{						
						LNP1_POLICYMASTR.markStatus(proposal,status);												
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
			}*/
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
           	

			string Comments="Re-Calculate Premium as Client Age has been Changed";
			string Status="NOT OK";
			string proposal="";

			ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("F", user, userType,bankCode, branchCode,false);

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
		private void verifyProposalList()
		{
			ace.CSVFileGeneration fileRe = new ace.CSVFileGeneration("D:\\abc\\","abc.csv");
			string[] proposalList = fileRe.ReadFromFile();
			StringBuilder errorList = new StringBuilder();
			foreach(string proposal in proposalList)
			{
				if(this.dt.Select("np1_proposal = '"+ proposal +"'").Length != 0)
				{
					LNP1_POLICYMASTR.markStatus(proposal,"Y");
				}
				else
				{
					errorList.Append(proposal).Append(",");                    
				}
			}
			if(errorList.Length != 0)
			{
				throw new Exception(errorList.Append("\\n error occured during uploading of proposals.").ToString());
			}
		}

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
		private DataTable dt = null;

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
			this._CustomEvent.ServerClick += new System.EventHandler(this._CustomEvent_ServerClick);
			this.dGrid.ItemDataBound+=new DataGridItemEventHandler(dGrid_ItemDataBound);

		}
		#endregion

		/*private void dGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string status = ((DataRowView)(e.Item.DataItem)).Row["np1_selected"].ToString();
				DropDownList ddlStatus = ((DropDownList)e.Item.FindControl("ddlStatus"));
				ddlStatus.Enabled = status.Equals("D");

			}
		}*/
	}
}
