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
	public partial class ManualPolicyIssuanceSearch : SHMA.Enterprise.Presentation.TwoStepController
	{

        OleDbCommand cmd = new OleDbCommand(); // Imran code
		DataTable dtDispatchUpload = new DataTable();
        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
            //    // SessionObject.Set("NP1PROPOSAL", txtSearch.Text);
            //    Label1.Text=Session["UserType"].ToString();

            }

        }


        #region " First Step "		 


        protected override DataHolder GetInputData(DataHolder dataHolder)
		{	
			Session["dataHolder"] = dataHolder;
			dGrid.DataSource = getDataSourceReader(dataHolder,"1<>1");
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
		/*
			string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);			
			dGrid.DataSource = getDataSource(dataHolder);
			dGrid.DataBind();
			*/
		}

//		protected override void ApplyDomainLogic(DataHolder dataHolder)
//		{
//			string proposal = string.Empty;
//			string status = string.Empty;
//			string comments = string.Empty;
//			string statusText = string.Empty;
//			DB.BeginTransaction();
//			SaveTransaction = true;
//			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();
//			dataHolder = new LNCM_COMMENTSDB(dataHolder).findByPK(string.Empty);
//			
//			LNCM_COMMENTS cmObj = new LNCM_COMMENTS(dataHolder);
//
//			if(dGrid.Items.Count > 0 && (EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
//			{
//				foreach(DataGridItem item in dGrid.Items)
//				{
//					status = ((DropDownList)item.FindControl("ddlStatus")).SelectedValue;
//
//					statusText = ((DropDownList)item.FindControl("ddlStatus")).SelectedItem.ToString();
//					proposal = ((Label)item.FindControl("lblProposal")).Text;
//					comments = ((TextBox)item.FindControl("txtComments")).Text;					
//						
//					if(status=="RePost")
//					{
//						try
//						{						
//							LNP1_POLICYMASTR.markStatus(proposal,"P"/*status*/);												
//							cmObj.AddComments(proposal,comments,statusText);
//						}
//						catch(Exception ex)
//						{
//							errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n","").Replace("\"","'").Replace("\r",""));
//							errMessage.Append("\\n");						
//						}
//					}
//					else if (status=="Modify")
//					{					
//						try
//						{						
//							LNP1_POLICYMASTR.markStatus(proposal,"null"/*status*/);												
//							cmObj.AddComments(proposal,comments,statusText);
//						}
//						catch(Exception ex)
//						{
//							errMessage.Append(proposal).Append(" : ").Append(ex.Message.Replace("\n","").Replace("\"","'").Replace("\r",""));
//							errMessage.Append("\\n");						
//						}
//					}
//					if(errMessage.Length != 0)
//					{
//						showAlertMessage(errMessage.ToString());					
//					}
//				}
//			}
//
//			else
//			{
//				showAlertMessage("There is nothing to save.");
//			}
//		}
//

		#endregion

		#region " Supported Methods "

		private void showAlertMessage(string message_)
		{	
			HeaderScript.Text = "alert(\""+message_+"\");";
		}
		private DataTable getDataSourceReader(DataHolder ds, string extraClause)
		{	
			
			string user = System.Convert.ToString(Session["s_USE_USERID"]);
			string userType = ace.Ace_General.getUserType(user);		
			string branchCode = System.Convert.ToString(Session["s_CCS_CODE"]);
			string bankCode = System.Convert.ToString(Session["s_CCD_CODE"]);
            Session["BankCode"] = bankCode;


            DataTable table = new DataTable("LNP1_POLICYMASTR_DATA");
			
			IDataReader tempReader= new LNP1_POLICYMASTRDB(dataHolder).getProposalSatusList(extraClause,user,userType,branchCode,bankCode);

			Utilities.Reader2Table(tempReader, table);

			tempReader.Close();
			return(table);
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

				if(repAllComments.Items.Count==0)
					repAllComments.Visible=false;
				
				//e.Item.Attributes.Add("onmouseover","this.style.backgroundColor='lime';");
				e.Item.Attributes.Add("onmouseover","showComments("+porposal+")");
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
			this.btnSearch.Click+=new EventHandler(btnSearch_Click); 
			this.dGrid.ItemDataBound+=new DataGridItemEventHandler(dGrid_ItemDataBound);
           // DisplayDispatchInfo();

		}
		#endregion

		private void btnSearch_Click(object sender, EventArgs e)
		{

			String searchClause=string.Empty;
			SessionObject.Set("NP1_PROPOSAL", txtSearch.Text);
         //   string Sql = "Select SUBSTR(Use_UserID, 5, 1) AS BCode from lnp1_policymastr A where A.NP1_PROPOSAL='" + txtSearch.Text + "'";
         //   Session["BankCode"] = GetdataOraOledb(Sql).Rows[0]["BCode"].ToString();

            if (txtSearch.Text!="")
			{
				searchClause = "upper("+ddlSearchBy.SelectedValue+") like upper('"+txtSearch.Text.Replace("'","").Trim()+"')";//"np1_proposal="+txtSearch.Text;
			}
			//TODO:CNIC SEARCH//String searchClause = "inq.nph_idno="+txtSearch.Text;
				


			String strSearch = txtSearch.Text;
			dataHolder=(DataHolder)Session["dataHolder"];
			
			//IDataReader temp =getDataSourceReader(dataHolder, String.Empty);
			DataTable temp =getDataSourceReader(dataHolder, searchClause);

			dGrid.DataSource = temp;
			dGrid.DataBind();


			// Imran work for Dispatch Info
			// DisplayDispatchInfo();



		}

        protected void btnPStat_Click(object sender, EventArgs e)
        {
            string UserID = Session["s_USE_USERID"].ToString().ToUpper();
         //   lblAlert.Text = UserID + "test";
            //SessionObject.Set("NP1_PROPOSAL", txtSearch.Text);


        }
        protected void btnHist_Click(object sender, EventArgs e)            //chg-his
        {
            SessionObject.Set("NP1_PROPOSAL", txtSearch.Text);

            string abc = System.Convert.ToString(Session["NP1_PROPOSAL"]);
            string aaa = System.Convert.ToString(Session["NP1_PROPOSAL"]);
            btnHist.Attributes.Add("onclick", "setValue('" + btnHist.Text + "');executeReport('HISTORY');");
            showAlertMessage(aaa);
           // &nbsp; < asp:Button CssClass = "inputClass" ID = "btnHist" Text = "Payment History" Runat = "server" OnClick = "btnHist_Click" OnClientClick = "executeReport('HISTORY');" ></ asp:Button >

         //   LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
           // btn.Attributes.Add("onclick", "setValue('" + btn.Text + "');executeReport('PROFILE');");
           // btn.Text


        }

        // Imran Code-------------------
        #region "Imran-code"
        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;


        }

        public DataTable GetdataOraOledb(string sql)
        {

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
        void DisplayDispatchInfo()
        {
            try
            {
                string Sql = "SELECT N.NP1_CONSIGNMENTNO,N.NP1_CONSIGNMENT_NAME,N.NP1_DOCUMENT_TYPE,N.NP1_CONS_ADDRESS,N.NP1_ORIGIN_CITY,N.NP1_DEST_CITY,N.NP1_RECEIVE_BY,N.NP1_DELIVERY_DATE,N.NP1_DISPATCH_STATUS " +
                               " FROM LNP1_DISPATCH_INFO N where N.NP1_PROPOSAL = '" + txtSearch.Text.ToString() + "'";




				//  PnlDispatch.Visible = true;
				
				lblStatusMsg.Text = "Policy document dispatch information";

					gvDispatchInfo.DataSource = GetdataOraOledb(Sql);
					gvDispatchInfo.DataBind();
				

				
            }
            catch (Exception Ex)

            {
                // lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
        }

        #endregion

        protected void btnDispatchInfo_Click(object sender, EventArgs e)
        {
            try
            {
              // DisplayDispatchInfo();

            }
            catch (Exception)
            {

               // lblStatusMsg.Text = Ex.Message;
            }

        }

        protected void btnHist_Click1(object sender, EventArgs e)
        {
			SessionObject.Set("NP1_PROPOSAL", txtSearch.Text);

			string abc = System.Convert.ToString(Session["NP1_PROPOSAL"]);
			string aaa = System.Convert.ToString(Session["NP1_PROPOSAL"]);
			btnHist.Attributes.Add("onclick", "setValue('" + btnHist.Text + "');executeReport('HISTORY');");
			showAlertMessage(aaa);
			// &nbsp; < asp:Button CssClass = "inputClass" ID = "btnHist" Text = "Payment History" Runat = "server" OnClick = "btnHist_Click" OnClientClick = "executeReport('HISTORY');" ></ asp:Button >

			//   LinkButton btn = (LinkButton)e.Item.FindControl("lblProposal");
			// btn.Attributes.Add("onclick", "setValue('" + btn.Text + "');executeReport('PROFILE');");
			// btn.Text

		}
	}

















}
