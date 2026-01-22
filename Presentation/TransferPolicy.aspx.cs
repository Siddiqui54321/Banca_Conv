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
	public partial class TransferPolicy : SHMA.Enterprise.Presentation.TwoStepController
	{
		

		#region " First Step "		 
		
		protected override DataHolder GetInputData(DataHolder dataHolder)
		{	
			dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();
            GridData.DataSource = getDataSourceforAdmin(dataHolder);
            GridData.DataBind();
            try
            {
                string id = (string)SessionObject.Get("PendingList");
                ddl_VALAPP.SelectedValue = id;
            }
            catch (Exception)
            {

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
         
			/*string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);			
			dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();			
			
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
			new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("F");
			DataTable proposalDT = dataHolder["LNP1_POLICYMASTR_DATA"];

			try
			{													
				WriteFile(proposalDT);					
			}
			catch(Exception exc)
			{
				errMessage.Append(exc.Message);
			}				

			if(errMessage.Length != 0)
			{
				showAlertMessage(errMessage.ToString());					
			}*/
		}
		public string addStringCVS(string str, string val){
			if(str=="")
				str = val;
			else
				str = str + "," + val;
			return str;
		}

		protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
        
           
			/*string proposal = string.Empty;
			string status = string.Empty;
			string comments = string.Empty;
			string cvsProposalNos = string.Empty;
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
			dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
			LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);
			if(dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
			{
				foreach(DataGridItem item in dGrid.Items)
				{
					status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
					
					proposal = ((Label)item.FindControl("lblProposal")).Text;
					comments = ((TextBox)item.FindControl("txtComments")).Text;					
					
					if(status=="."){continue;}//Nothing

					if(status=="Y")//Ok
					{
						status = "F";
						cvsProposalNos=addStringCVS(cvsProposalNos,proposal);
					}
					else if(status=="N")//Not Ok
						status = "P";

					try
					{						
						LNP1_POLICYMASTR.markStatus(proposal,status);				
						cmObj.AddComentsInTable(proposal,comments,status);
					}
					catch(Exception ex)
					{
						errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n","").Replace("\"","'").Replace("\r",""));
						errMessage.Append("\\n");						
					}
				}

				dGrid.DataSource = getDataSource(dataHolder);
				dGrid.DataBind();		

				if(cvsProposalNos!=string.Empty)
				{
					IDataReader dr = LNP1_POLICYMASTRDB.GetProposalForExcelReport(cvsProposalNos);
					DataTable dt = new DataTable();
					Utilities.Reader2Table(dr, dt);
					dr.Close();
					WriteExcel(dt);
					WriteFileOnClient();
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

			ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("Y",user,userType,bankCode,branchCode,true);
			return ds["LNP1_POLICYMASTR_DATA"];
		}

        private DataTable getDataSourceforAdmin(DataHolder ds)
        {
            ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalListForAdmin();
            return ds["LNP1_POLICYMASTR_DATA_ADMIN"];
        }

		//================= New Method Added to Counter Data Reload after Index Changed (09-Jan-2018) =================//
		private DataTable getDataSource_NEW(DataHolder dataHolder)
        {
            DataHolder ds = new DataHolder();
            if (ddl_VALAPP.SelectedValue == "0")
            {
                
                string user = System.Convert.ToString(Session["s_USE_USERID"]);
                string userType = ace.Ace_General.getUserType(user);
                string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
                string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);

                bool SClick = false;
                if (SearchClicked == true)
                { SClick = true; }
                else { SClick = false; }


                //ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList("Y",user,userType,bankCode,branchCode,true);
                ds = new LNP1_POLICYMASTRDB(dataHolder).getPendingProposalList_New("Y", user, userType, bankCode, branchCode, true, txtSearchEvent.Text, SClick, ddlsearch.SelectedValue, txtsearch.Text);

                return ds["LNP1_POLICYMASTR_DATA"];
            }
            else
            {
                ds = new LNP1_POLICYMASTRDB(dataHolder).getSearchedPendingProposalListForAdmin(ddlsearch.SelectedValue, txtsearch.Text);
                return ds["LNP1_POLICYMASTR_DATA_ADMIN"];
            }
		}
		//================= New Method Added to Counter Data Reload after Index Changed (09-Jan-2018) =================//
		private void WriteExcel(DataTable dt){
		
			try
			{
				string portotypeFilePath = Server.MapPath(ConfigurationSettings.AppSettings["prototypeFilePath"]);
				string dfn = ConfigurationSettings.AppSettings["downloadFileName"];
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+ConfigurationSettings.AppSettings["downloadFileName"]);
				File.Copy(portotypeFilePath, downloadProposalFile, true);
				string ConnectionString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+downloadProposalFile+";Extended Properties="+"\""+"Excel 12.0;HDR=YES;READONLY=FALSE"+"\"";
				OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
				string ExcelQuery = null;
				ExcelQuery = "SELECT * FROM [Sheet1$]";
				OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
				ExcelConnection.Open();
				//ExcelQuery = "UPDATE [Sheet1$] SET A='1', B='3', C='Update'";
				//ExcelQuery = "INSERT INTO [Sheet1$B5:B5] (F1) VALUES ('1')";
				foreach(DataRow dr in dt.Rows)
				{
					ExcelQuery = "INSERT INTO [Sheet1$] ([Transaction Identifier], [From Branch Code], [From Account No], "+
						"[From Account Type], [From CLSL], [From Account Currency], [To Branch Code], [To Account No], "+
						"[To Account Type], [To Account Currency], [To CLSL], [Amount], [Transaction Currency], [Value Date], "+
						"[User Narration], [Instrument No], [From CRC], [To CRC], [Wht Flag], [Tran_code]) "+
						"VALUES ("+
						"'"+ dr[0].ToString() +"', '"+ dr[1].ToString() +"', '"+ dr[2].ToString() +"', '"+ dr[3].ToString() +"', "+
						"'"+ dr[4].ToString() +"', '"+ dr[5].ToString() +"', '"+ dr[6].ToString() +"', '"+ dr[7].ToString() +"', "+
						"'"+ dr[8].ToString() +"', '"+ dr[9].ToString() +"', '"+ dr[10].ToString() +"', '"+ dr[11].ToString() +"', "+
						"'"+ dr[12].ToString() +"', '"+ dr[13].ToString() +"', '"+ dr[14].ToString() +"', '"+ dr[15].ToString() +"', "+
						"'"+ dr[16].ToString() +"', '"+ dr[17].ToString() +"', '"+ dr[18].ToString() +"', '"+ dr[19].ToString() +"')";
					ExcelCommand = new OleDbCommand(ExcelQuery,ExcelConnection);
					ExcelCommand.ExecuteNonQuery();
				}
				ExcelConnection.Close();
                ExcelConnection.Dispose();
			}
			catch(Exception ex)
			{
				StringBuilder errMessage = new StringBuilder();
				errMessage.Append(ex.Message);
			}
		}

		private void WriteFileOnClient()
		{
			/*
			try
			{
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+ConfigurationSettings.AppSettings["downloadFileName"]);
				string dfn = ConfigurationSettings.AppSettings["downloadFileName"];
				string attachment = "attachment; filename=downloadProposalStatus.xls";
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
				HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
				HttpContext.Current.Response.WriteFile(downloadProposalFile);
				HttpContext.Current.Response.AddHeader("Pragma", "public");
				HttpContext.Current.Response.Write(dfn.ToString());
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
			catch(Exception ex){
				string a = ex.Message;
			}
			*/
			string popupScript = " window.open('DownloadProposalFile.aspx','DownloadExcelFile','width=1,height=1,location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,top=1000');";

			callJs.Text=popupScript;
			
		}

		#endregion

		#region " Events "


		private void Get_Comments(string Proposal)
		{
			
//			IDataReader temp = LNCM_COMMENTS.GetComments(Proposal);
//			Response.Write("<Table class='text_font'>");
//			Response.Write("<tr class='CommentGridHeading'><td>Comment By</td>");
//			Response.Write("<td>Action</td>");
//			Response.Write("<td class='tdComment'>Comment</td>");
//			Response.Write("<td>Date</td></tr>");
//            while (temp.Read())
//			{
//				string Np1_Proposal = temp["NP1_PROPOSAL"].ToString();
//				string CM_SERIAL_NO = temp["CM_SERIAL_NO"].ToString();
//				string CM_COMMENTDATE = temp["CM_COMMENTDATE"].ToString();
//				string CM_COMMENTBY = temp["CM_COMMENTBY"].ToString();
//			 
//			}

			

		}

		private void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
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



		//================= New Event Added for Paging (09-Jan-2018) =================// 		
		protected void dGrid_OnPageIndexChanged(object source, DataGridPageChangedEventArgs e) 
		{  
			DataHolder dataHolder = new DataHolder();	
			this.dGrid.DataSource = getDataSource_NEW(dataHolder);
			this.dGrid.DataBind();	

			dGrid.CurrentPageIndex  = e.NewPageIndex;  
			dGrid.DataBind();		
	

		}
        protected void DataGrid_OnPageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataHolder dataHolder = new DataHolder();
            this.GridData.DataSource = getDataSourceforAdmin(dataHolder);
            this.GridData.DataBind();

            GridData.CurrentPageIndex = e.NewPageIndex;
            GridData.DataBind();


        }
		//================= New Event Added for Paging (09-Jan-2018) =================// 		


		protected void Button1_Click(object sender, System.EventArgs e)
		{
			if(IsPostBack)
			{
				SearchClicked = true;
				dGrid.CurrentPageIndex = 0;
				txtSearchEvent.Text = System.Convert.ToString(ddlsearch.SelectedIndex);
				DataHolder dataHolder = new DataHolder();
                if (ddl_VALAPP.SelectedValue == "0")
                {
                    this.dGrid.DataSource = getDataSource_NEW(dataHolder);
                    this.dGrid.DataBind();

                }
                else
                {
                    this.GridData.DataSource = getDataSource_NEW(dataHolder);
                    this.GridData.DataBind();
                }
				
				//this.LoadPersonalInfo();
			}
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
            string proposal = string.Empty;
            string CommencementDate = string.Empty;

            foreach (DataGridItem item in dGrid.Items)
            {
                proposal = ((Label)item.FindControl("lblProposal")).Text;
                CommencementDate = ((TextBox)item.FindControl("txtCommencementDate")).Text;
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)item.FindControl("chk" + proposal));

                if (chk == null)
                    continue;

                if (chk != null && !chk.Checked)
                    return;


                try
                {
                    if (CommencementDate == null || CommencementDate == "")
                    {
                        showAlertMessage("Please Enter Commencement Date.");
                        return;
                    }
                    if (Convert.ToDateTime(CommencementDate) < DateTime.Now.AddMonths(-1))
                    {
                        showAlertMessage("Commencement Date cannot be less than 1 month old....");
                        return;
                    }
                    if (Convert.ToDateTime(CommencementDate).Date > DateTime.Now.Date)
                    {
                        showAlertMessage("Commencement Date cannot be more than today.");
                        return;
                    }

                    //CommencementDate=	ace.Ace_General.getGeneratedCommencementDate(DateTime.Now).ToString("dd/MM/yyyy");
                    CommencementDate = ace.Ace_General.getGeneratedCommencementDate(Convert.ToDateTime(CommencementDate)).ToString("dd/MM/yyyy");
                    SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(CommencementDate));

                    ((TextBox)item.FindControl("txtCommencementDate")).Text = CommencementDate;

                    showAlertMessage("Commencement Date Updated Successfully.");
                }
                catch (Exception ex)
                {
                    showAlertMessage(ex.Message);
                }

            }
        }
		#endregion

		#region " Class Variable "

		protected System.Web.UI.WebControls.Literal _result;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.HyperLink hlk;
		protected System.Web.UI.WebControls.Literal callJs;

		//======== Added 09012018

		private bool SearchClicked = false;
		//======== Added 09012018

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
