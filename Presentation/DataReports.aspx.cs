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





namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for DataReports.
	/// </summary>
	public partial class DataReports : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal callJs;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.HyperLink hlIlasMisTextFile;
		protected System.Web.UI.WebControls.Button btngenerateIlasMisTextfile;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
				this.txtDATEFROM.Text = "01/" + sysDate.Month + "/" + sysDate.Year;
				this.txtDATETO.Text   = sysDate.Day + "/" + sysDate.Month + "/" + sysDate.Year;
			    BindBranches();
				BindStatus();
                Session["PageReloadTime"] = 2000;
                Session["DownloadCompleted"] =false;
				string UserID = Session["s_USE_USERID"].ToString().ToUpper();
				string USER_TYPE = SessionObject.GetString("s_USE_TYPE");
				if (UserID != "ADMIN" || USER_TYPE.Equals("L") || USER_TYPE.Equals("K") || USER_TYPE.Equals("D") || USER_TYPE.Equals("H") || USER_TYPE.Equals("M"))    //chg-20230915 datareport
				{
					c_btngeneratebank.Style.Add("visibility", "hidden");
					c_btngenerateilasfile.Style.Add("visibility", "hidden");
					c_btngeneratedatadumpfile.Style.Add("visibility", "hidden");
					c_btngenerateIlasMisfile.Style.Add("visibility", "hidden");
					c_btnTextFile.Style.Add("visibility", "hidden");
					c_btnTextFile.Style.Add("visibility", "hidden");
					c_ddlFileType.Style.Add("visibility", "hidden");
				}
			}

            //SetDownloadLink(hlbanca,ConfigurationSettings.AppSettings["downloadBancaName"],ConfigurationSettings.AppSettings["prototypeBancaFilePath"],GenerateFileType.Banca.ToString());
			//SetDownloadLink(hlIlasile,ConfigurationSettings.AppSettings["downloadIlasName"],ConfigurationSettings.AppSettings["prototypeIlasFilePath"],GenerateFileType.Ilas.ToString());
			//SetDownloadLink(hlublfile,ConfigurationSettings.AppSettings["downloadUblName"],ConfigurationSettings.AppSettings["prototypeUblFilePath"],GenerateFileType.UBL.ToString());
		}

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


		public void SetDownloadLink(System.Web.UI.WebControls.HyperLink hlcrtl,string FileName,string prototype,string FileType)
		{
			string portotypeFilePath = Server.MapPath(prototype);
			string dfn = FileName;
			string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+FileName);
				
			if (!File.Exists(downloadProposalFile))
			{
				hlcrtl.Enabled=false;
				hlcrtl.NavigateUrl="#";
			}
			else
			{
                try
                {
                    hlcrtl.Enabled = true;
                    hlcrtl.NavigateUrl = downloadProposalFile;

                    Response.Clear();
                    Response.ContentType = "Application/.xls";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                    //Response.TransmitFile(downloadProposalFile);
                    Response.WriteFile(downloadProposalFile);
                    Response.Flush();
                    DeleteFile(downloadProposalFile);
                    SessionObject.Set("DownloadCompleted", "True");
                    Response.End();
                }
                catch (Exception)
                {
                   
                }
				

			}
			
		}

		public void DeleteFile(string Path)
		{
			if(Path!="")
			{
				if(File.Exists(Path))
				{
				   File.Delete(Path);
				}
			}
		}

		public void BindBranches()
		{
			string UserID=Session["s_USE_USERID"].ToString().ToUpper();
			string UserType=SessionObject.GetString("s_USE_TYPE");

			IDataReader LUCH_USERCHANNEL0 = SHAB.Data.LUCH_USERCHANNELDB.GetDDL_LUCH_USERCHANNEL_CCS_CODE_RO_FILTER(UserID,UserType);;
		
			ddlCCS_CODE.DataSource = LUCH_USERCHANNEL0;
			ddlCCS_CODE.DataBind();
			LUCH_USERCHANNEL0.Close();

			if(UserID=="ADMIN" || UserType =="B" || UserType.Equals("P"))
			{
				ddlCCS_CODE.Items.Insert(0,new ListItem("All Bank",""));
			}
		
		}

		public void BindStatus()
		{
			ddlstatus.Items.Add(new ListItem("Issued","I"));
			ddlstatus.Items.Add(new ListItem("Pending","P"));
			ddlstatus.Items.Add(new ListItem("All","A"));

			if(!IsPostBack)
			{
				ddlstatus.SelectedValue="I";
			}
		}

//		private void WriteExcel(DataTable dt,string FileName,string prototype,string FileType)
//		{
//			try
//			{
//				string portotypeFilePath = Server.MapPath(prototype);
//				string dfn = FileName;
//				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+FileName);
//				
//				if(File.Exists(downloadProposalFile))
//				{
//					File.Delete(downloadProposalFile);
//				}
//				
//				File.Copy(portotypeFilePath, downloadProposalFile);
//				string ConnectionString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+downloadProposalFile+";Extended Properties="+"\""+"Excel 12.0;HDR=YES;READONLY=FALSE"+"\"";
//				OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
//				string ExcelQuery = null;
//				ExcelQuery = "SELECT * FROM [Sheet1$]";
//				OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
//				ExcelConnection.Open();
//				//ExcelQuery = "UPDATE [Sheet1$] SET A='1', B='3', C='Update'";
//				//ExcelQuery = "INSERT INTO [Sheet1$B5:B5] (F1) VALUES ('1')";
//				foreach(DataRow dr in dt.Rows)
//				{
//					
//					if(GenerateFileType.Banca.ToString()==FileType)
//					{
//					
//						ExcelQuery = "INSERT INTO [Sheet1$] ([USE_USERID],[USERNAME], [BRANCHCODE], "+
//							"[BRANCHNAME], [PROPOSALDATE], [COMMENCENTDATE], [Proposal_Number], [Policy_Number], "+
//							"[CLIENTNAME], [PLANDESC], [PAYMENTMODE], [PREMIUM], [SUMASSURED], [TERM], "+
//							"[POLICY_STATUS], [NP1_ACCOUNTNO]) "+
//							"VALUES ("+
//							"'"+ dr["USE_USERID"].ToString() +"','"+ dr["USERNAME"].ToString() +"', '"+ dr["BRANCHCODE"].ToString() +"', '"+ dr["BRANCHNAME"].ToString() +"', '"+ dr["PROPOSALDATE"].ToString() +"', "+
//							"'"+ dr["COMMENCENTDATE"].ToString() +"', '"+ dr["Proposal_Number"].ToString() +"', '"+ dr["Policy_Number"].ToString() +"', '"+ dr["CLIENTNAME"].ToString() +"', "+
//							"'"+ dr["PLANDESC"].ToString() +"', '"+ dr["PAYMENTMODE"].ToString() +"', '"+ dr["PREMIUM"].ToString() +"', '"+ dr["SUMASSURED"].ToString() +"', "+
//							"'"+ dr["TERM"].ToString() +"', '"+ dr["POLICY_STATUS"].ToString() +"', '"+ dr["NP1_ACCOUNTNO"].ToString() +"' )";
//					}
//					else if(GenerateFileType.UBL.ToString()==FileType)
//					{
//					
//						ExcelQuery = "INSERT INTO [Sheet1$] ([SNO], [Proposal No],[Policy Number], "+
//							"[Proposal Entry Date], [Company Name], [Premium Deduction Date], [Issuance Date], [Customer Name], "+
//							"[CNIC], [UBL Account No], [Office No], [Residence No], [ADDRESS], [Mobile No],[Beneficiary],[Relation to Customer],[CNIC/B Form No of beneficiary], "+
//							"[PLAN],[Riders Detail], [PAYMODE],[TERM],[Modal Premium],[Sum Assured],[Branch Code],[Cluster],[Region],[District],[BRANCHNAME],[User ID],[USERNAME],[POS Status],[Proposal Status (ILAS)],[REMARKS]) "+
//							"VALUES ("+
//							"'"+ dr["SNO"].ToString() +"',"+
//							"'"+ Convert.ToString(dr["Proposal No"]) +"',"+
//							"'"+ Convert.ToString(dr["Policy Number"]) +"',"+
//							"'"+ Convert.ToString(dr["Proposal Entry Date"]) +"',"+
//							"'"+ Convert.ToString(dr["Company Name"]) +"',"+
//							"'"+ Convert.ToString(dr["Premium Deduction Date"]) +"',"+
//							"'"+ Convert.ToString(dr["Issuance Date"]) +"',"+
//							"'"+ Convert.ToString(dr["Customer Name"]) +"',"+
//							"'"+ Convert.ToString(dr["CNIC"]) +"', "+
//							"'"+ Convert.ToString(dr["UBL Account No"]) +"',"+
//							"'"+ Convert.ToString(dr["Office No"]) +"',"+
//							"'"+ Convert.ToString(dr["Residence No"]) +"',"+
//							"'"+ Convert.ToString(dr["ADDRESS"]) +"', "+
//							"'"+ Convert.ToString(dr["Mobile No"]) +"',"+
//							"'"+ Convert.ToString(dr["Beneficiary"]) +"',"+
//							"'"+ Convert.ToString(dr["Relation to Customer"]) +"',"+
//							"'"+ Convert.ToString(dr["CNIC/B Form No of beneficiary"]) +"',"+
//							"'"+ Convert.ToString(dr["PLAN"]) +"',"+
//							"'"+ Convert.ToString(dr["Riders Detail"]) +"',"+// add this farrukh
//							"'"+ Convert.ToString(dr["PAYMODE"]) +"',"+
//							"'"+ Convert.ToString(dr["TERM"]) +"',"+
//							"'"+ Convert.ToString(dr["Modal Premium"]) +"',"+
//							"'"+ Convert.ToString(dr["Sum Assured"]) +"',"+
//							"'"+ Convert.ToString(dr["Branch Code"]) +"',"+
//							"'"+ Convert.ToString(dr["Cluster"]) +"',"+
//							"'"+ Convert.ToString(dr["Region"]) +"',"+
//							"'"+ Convert.ToString(dr["District"]) +"',"+
//							"'"+ Convert.ToString(dr["BRANCHNAME"]) +"',"+
//							"'"+ Convert.ToString(dr["User ID"]) +"',"+
//							"'"+ Convert.ToString(dr["USERNAME"]) +"',"+
//							"'"+ Convert.ToString(dr["POS Status"]) +"',"+ 
//							"'"+ Convert.ToString(dr["Proposal Status (ILAS)"]) +"',"+
//							"'"+ Convert.ToString(dr["REMARKS"]) +"')";
//					}
//					
//					else if(GenerateFileType.Ilas.ToString()==FileType)
//					{
//					
//						ExcelQuery = "INSERT INTO [Sheet1$] ([Proposal], [Policy],[Full_Name],[CNIC], [UBL Account No], [Office No], [Residence No], [ADDRESS], [Mobile No],[Beneficiary],[Relation to Customer],[CNIC/B Form No of beneficiary], "+
//							"[Status], [IssueDate], [ComncDate], [PayFreq],  "+
//							"[ColctBranch], [1stYearCmsn], [Prod],[Riders Detail], [BnftTerm], [PayTerm], [Prod_SmAsurd], "+
//							"[Basic Premium],[Rider Premium],[Modal Premium]) "+
//							"VALUES ("+
//							"'"+ dr["Proposal"].ToString() +"',"+
//							"'"+ Convert.ToString(dr["Policy"]) +"',"+
//							"'"+ Convert.ToString(dr["Full_Name"]) +"',"+
//							"'"+ Convert.ToString(dr["CNIC"]) +"', "+
//							"'"+ Convert.ToString(dr["UBL Account No"]) +"',"+
//							"'"+ Convert.ToString(dr["Office No"]) +"',"+
//							"'"+ Convert.ToString(dr["Residence No"]) +"',"+
//							"'"+ Convert.ToString(dr["ADDRESS"]) +"', "+
//							"'"+ Convert.ToString(dr["Mobile No"]) +"',"+
//							"'"+ Convert.ToString(dr["Beneficiary"]) +"',"+
//							"'"+ Convert.ToString(dr["Relation to Customer"]) +"',"+
//							"'"+ Convert.ToString(dr["CNIC/B Form No of beneficiary"]) +"',"+
//							"'"+ Convert.ToString(dr["Status"]) +"',"+
//							"'"+ Convert.ToString(dr["IssueDate"]) +"',"+
//							"'"+ Convert.ToString(dr["ComncDate"]) +"',"+
//							"'"+ Convert.ToString(dr["PayFreq"]) +"',"+
//							"'"+ Convert.ToString(dr["ColctBranch"]) +"', "+
//							"'"+ Convert.ToString(dr["1stYearCmsn"]) +"',"+
//							"'"+ Convert.ToString(dr["Prod"]) +"',"+
//							"'"+ Convert.ToString(dr["Riders Detail"]) +"',"+
//							"'"+ Convert.ToString(dr["BnftTerm"]) +"',"+
//							"'"+ Convert.ToString(dr["PayTerm"]) +"', "+
//							"'"+ Convert.ToString(dr["Prod_SmAsurd"]) +"',"+
//							"'"+ Convert.ToString(dr["Basic Premium"]) +"',"+
//							"'"+ Convert.ToString(dr["Rider Premium"]) +"',"+
//							"'"+ Convert.ToString(dr["Modal Premium"]) +"')";
//
//
//
//					}
//
//					ExcelCommand = new OleDbCommand(ExcelQuery,ExcelConnection);
//					ExcelCommand.ExecuteNonQuery();
//				
//				}
//				ExcelConnection.Close();				
//			}
//			catch(Exception ex)
//			{
//				StringBuilder errMessage = new StringBuilder();
//				errMessage.Append(ex.Message);
//			}
//		}




		private void WriteExcel(DataTable dt,string FileName,string prototype,string FileType)
		{
			try
			{
				string portotypeFilePath = Server.MapPath(prototype);
				string dfn = FileName;
				string downloadProposalFile = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+FileName);
				
				if(File.Exists(downloadProposalFile))
				{
					File.Delete(downloadProposalFile);
				}
				
				File.Copy(portotypeFilePath, downloadProposalFile);
	//			string ConnectionString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+downloadProposalFile+";Extended Properties="+"\""+"Excel 12.0;HDR=YES;READONLY=FALSE"+"\"";
				string ConnectionString=@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+downloadProposalFile+";Extended Properties="+"\""+"Excel 8.0;HDR=YES;READONLY=FALSE"+"\"";
				OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
				string ExcelQuery = null;
				ExcelQuery = "SELECT * FROM [Sheet1$]";
				OleDbCommand ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
				ExcelConnection.Open();
				//ExcelQuery = "UPDATE [Sheet1$] SET A='1', B='3', C='Update'";
				//ExcelQuery = "INSERT INTO [Sheet1$B5:B5] (F1) VALUES ('1')";
				int i = 1;
				foreach(DataRow dr in dt.Rows)
				{
					
					if(GenerateFileType.Banca.ToString()==FileType)
					{
					string l =dr["CurrentStatus"].ToString();

						if(l.Length > 250)
						{
							l=l.Substring(0,249);
						}
						ExcelQuery = "INSERT INTO [Sheet1$] ([SNO],[POLICY_NUMBER],[BRANCH CODE],[BRANCH NAME],[BANK NAME],[CLIENT NAME], [CNIC]," +
							"[ACCOUNTNO],[AC BRANCH CODE],[AC BRANCH NAME],[PROPOSAL_NUMBER]," +
							"[CELL_NO],[ADDRESS],[CLIENT DOB],[CLIENT AGE],[POSTING DATE], " +
							"[RBH DECISION],[RBH DECISION DATE],[CBC DECISION],[CBC DECISION DATE]," +
							"[CBC USER],[BM_SUPERVISION_DATE],[COMISSION_PERCENTAGE],[PREMIUM AMOUNT],[PREMIUM CR DATE IN SLIC ACCOUNT]," +
							"[SUM ASSURED],[Total No of Premiums Received],[Total Premium Amount Recived]," +
							"[POLICY YEAR],[Premium Remarks], [Last Premium Received],"+
							"[Conventional/ Takaful],[PLANDESC],[TERM], [PAYMENTMODE],[PROPOSALDATE]," +
							" [PROPSIGNDATE],[PROPOSALREF],[PAYMENTREF],[COMMENCENTDATE],[STAFFID]," +
							" [STAFFNAME], " +
							"[RIDERS DETAIL],[BASIC PREMIUM],[RIDER PREMIUM],[MODAL PREMIUM]," +
							"[USERNAME],[CURRENTSTATUS], [OPD Dispatch Date], [Consign NO AND Courier Company]," +
							" [OPD Rec By Customer Date],[Receive By],[Bank Commission Payment Month]) " +
							
							"VALUES (" +
							"'" + i.ToString() + "','"+ dr["Policy_Number"].ToString() +"', '"+ dr["BranchCode"].ToString() +"','"+ dr["BranchName"].ToString() +"', '"+ dr["BANKNAME"].ToString() + "','" + dr["CLIENTNAME"].ToString() + "','" + dr["CNIC"].ToString() + "'," +
							"'" + dr["ACCOUNTNO"].ToString() + "','" + dr["acc_branch"].ToString() +"', '', '"+ dr["PROPOSAL_NUMBER"].ToString() +"', "+
							"'" + dr["NAD_MOBILE"].ToString() + "','" + dr["ADDRESS"].ToString().Trim('\'').Trim('\"').Trim('`') + "', '" + Convert.ToDateTime(dr["CLIENTDOB"]).ToString("dd/MM/yyyy") + "', '" + dr["CLIENTAGE"].ToString() + "', '" + dr["PROPOSALPOSTINGDATE"].ToString() + "', " +
							"'" + dr["RBHDECISION"].ToString() + "','" + dr["RBHDATE"].ToString() + "', '" + dr["CBCDECISION"].ToString() + "', '" + dr["CBCDATE"].ToString() + "', '" + dr["CBCUSER"].ToString() + "','" + dr["BMSupervisionDate"].ToString() + "','" + dr["CommissionPer"].ToString() + "', " +
							"'" + dr["Basic Premium"].ToString() +"', '','"+ dr["SUMASSURED"].ToString() +"','1','"+ dr["COLLECTION_AMOUNT"].ToString() + "', "+
							"'" + dr["policyyear"].ToString() + "', '','"+ dr["COLLECTION_DATE"].ToString() + "','Conventional', " +
							" '"+ dr["PLANDESC"].ToString() + "','" + dr["TERM"].ToString() + "', '" + dr["PAYMENTMODE"].ToString() +"', "+
							"'"+ dr["PROPOSALPOSTINGDATE"].ToString() + "', '" + Convert.ToString(dr["PROPOSALDATE"]) + "','" + Convert.ToString(dr["PROPOSALREF"]) + "','" + dr["PAYMENTREF"].ToString() +"', "+
							"'" + dr["COMMENCENTDATE"].ToString() + "', '" + Convert.ToString(dr["STAFFID"]) + "','" + Convert.ToString(dr["STAFFNAME"]) + "', " +
							"'" + Convert.ToString(dr["Riders Detail"]) +"','"+ Convert.ToString(dr["Basic Premium"]) +"',"+
							"'"+ Convert.ToString(dr["Rider Premium"]) +"','"+ Convert.ToString(dr["Modal Premium"]) +"',"+	
							"'"+ dr["USERNAME"].ToString() +"',"+
							"'"+ l  +"','','','','','')";
						///------------ New WORKING QUERY ADDED CNIC Rename ACCOUNTNO ----------///////
					}
					else if(GenerateFileType.UBL.ToString()==FileType)
					{
					
						string l =dr["CurrentStatus"].ToString();

						if(l.Length > 250)
						{
							l=l.Substring(0,249);
						}

						///------------ Changed UBL Account No to Account No ----------///////
						ExcelQuery = "INSERT INTO [Sheet1$] ([SNO], [Proposal No],[Policy Number], "+
							"[Proposal Entry Date], [PROPSIGNDATE],[PROPOSALREF],[PAYMENTREF],[Company Name], [Premium Deduction Date], [Issuance Date], [Customer Name],[Customer DOB],[Customer Age],[Bank Name], "+
							"[CNIC], [Account No], [Office No], [Residence No], [ADDRESS], [Mobile No],[Beneficiary],[Relation to Customer],[CNIC/B Form No of beneficiary], "+
							"[PLAN],[Riders Detail], [PAYMODE],[TERM],[Sum Assured],[Branch Code],[Cluster],[Region],[District],[BRANCHNAME],[User ID],[USERNAME],[POS Status],[Proposal Status (ILAS)], "+
							"[Basic Premium],[Rider Premium],[Modal Premium],[REMARKS], [CURRENTSTATUS]) "+
							"VALUES ("+
							"'"+ dr["SNO"].ToString() +"',"+
							"'"+ Convert.ToString(dr["Proposal No"]) +"',"+
							"'"+ Convert.ToString(dr["Policy Number"]) +"',"+
							"'"+ Convert.ToString(dr["Proposal Entry Date"]) +"',"+
							"'"+ Convert.ToString(dr["PropSignDate"]) +"',"+
							"'"+ Convert.ToString(dr["ProposalRef"]) +"',"+
							"'"+ Convert.ToString(dr["PaymentRef"]) +"',"+
							"'"+ Convert.ToString(dr["Company Name"]) +"',"+
							"'"+ Convert.ToString(dr["Premium Deduction Date"]) +"',"+
							"'"+ Convert.ToString(dr["Issuance Date"]) +"',"+
							"'"+ Convert.ToString(dr["Customer Name"]) +"',"+
							"'"+ Convert.ToString(dr["CustomerDOB"]) +"',"+
							"'"+ Convert.ToString(dr["CustomerAge"]) +"',"+
							"'"+ Convert.ToString(dr["BankName"]) +"',"+
							"'"+ Convert.ToString(dr["CNIC"]) +"', "+
							"'"+ Convert.ToString(dr["Account No"]) +"',"+
							"'"+ Convert.ToString(dr["Office No"]) +"',"+
							"'"+ Convert.ToString(dr["Residence No"]) +"',"+
							"'"+ Convert.ToString(dr["ADDRESS"]).Trim('\'').Trim('\"').Trim('`') + "', "+
							"'"+ Convert.ToString(dr["Mobile No"]) +"',"+
							"'"+ Convert.ToString(dr["Beneficiary"]) +"',"+
							"'"+ Convert.ToString(dr["Relation to Customer"]) +"',"+
							"'"+ Convert.ToString(dr["CNIC/B Form No of beneficiary"]) +"',"+
							"'"+ Convert.ToString(dr["PLAN"]) +"',"+
							"'"+ Convert.ToString(dr["Riders Detail"]) +"',"+// add this farrukh
							"'"+ Convert.ToString(dr["PAYMODE"]) +"',"+
							"'"+ Convert.ToString(dr["TERM"]) +"',"+
							//							"'"+ Convert.ToString(dr["Modal Premium"]) +"',"+
							"'"+ Convert.ToString(dr["Sum Assured"]) +"',"+
							"'"+ Convert.ToString(dr["Branch Code"]) +"',"+
							"'"+ Convert.ToString(dr["Cluster"]) +"',"+
							"'"+ Convert.ToString(dr["Region"]) +"',"+
							"'"+ Convert.ToString(dr["District"]) +"',"+
							"'"+ Convert.ToString(dr["BRANCHNAME"]) +"',"+
							"'"+ Convert.ToString(dr["User ID"]) +"',"+
							"'"+ Convert.ToString(dr["USERNAME"]) +"',"+
							"'"+ Convert.ToString(dr["POS Status"]) +"',"+ 
							"'"+ Convert.ToString(dr["Proposal Status (ILAS)"]) +"',"+

							//"'"+ Convert.ToString(dr["Riders Detail"]) +"',"+
							"'"+ Convert.ToString(dr["Basic Premium"]) +"',"+
							"'"+ Convert.ToString(dr["Rider Premium"]) +"',"+
							"'"+ Convert.ToString(dr["Modal Premium"]) +"',"+

							"'"+ Convert.ToString(dr["REMARKS"]) +"','"+ l +"')";
					}
					
					else if(GenerateFileType.Ilas.ToString()==FileType)
					{

                        ExcelQuery = "INSERT INTO [Sheet1$] ([Proposal], [Policy],[ClientCode],[Customer Name],[Customer DOB],[Customer Age],[CNIC], [Account No], [Office No], [Residence No]," +
    "[ADDRESSC],[CITYCODE],[PROVCODE],[ADDRESSP], [ADDRESSB],  [Mobile No],[Beneficiary],[Relation to Customer],[CNIC/B Form No of beneficiary], " +
    "[Status], [IssueDate], [ComncDate],[NextDueDate], [PayFreq],  " +
    "[BranchCode], [ColctBranch], [Bank Name], [1stYearCmsn], [Prod],[Riders Detail], [BnftTerm], [PayTerm], [Prod_SmAsurd], " +
    "[Basic Premium],[Rider Premium],[Modal Premium],[CREDIT BALANCE],[AGENT CODE],[AGENT NAME]) " +
    "VALUES (" +
    "'" + dr["Proposal"].ToString() + "'," +
    "'" + Convert.ToString(dr["Policy"]) + "'," +
    "'" + Convert.ToString(dr["ClientCode"]) + "'," +
    "'" + Convert.ToString(dr["CustomerName"]) + "'," +
    "'" + Convert.ToString(dr["CustomerDOB"]) + "'," +
    "'" + Convert.ToString(dr["CustomerAge"]) + "'," +
    "'" + Convert.ToString(dr["CNIC"]) + "', " +
    "'" + Convert.ToString(dr["UBL Account No"]) + "'," +
    "'" + Convert.ToString(dr["Office No"]) + "'," +
    "'" + Convert.ToString(dr["Residence No"]) + "'," +
    "'" + Convert.ToString(dr["ADDRESSC"]).Trim('\'').Trim('\"').Trim('`') + "', " +
    "'" + Convert.ToString(dr["CITYCODE"]) + "'," +
    "'" + Convert.ToString(dr["PROVCODE"]) + "'," +
    "'" + Convert.ToString(dr["ADDRESSP"]).Trim('\'').Trim('\"').Trim('`') + "', " +
    "'" + Convert.ToString(dr["ADDRESSB"]).Trim('\'').Trim('\"').Trim('`') + "', " +
    "'" + Convert.ToString(dr["Mobile No"]) + "'," +
    "'" + Convert.ToString(dr["Beneficiary"]) + "'," +
    "'" + Convert.ToString(dr["Relation to Customer"]) + "'," +
    "'" + Convert.ToString(dr["CNIC/B Form No of beneficiary"]) + "'," +
    "'" + Convert.ToString(dr["Status"]) + "'," +
    "'" + Convert.ToString(dr["IssueDate"]) + "'," +
    "'" + Convert.ToString(dr["ComncDate"]) + "'," +
    "'" + Convert.ToString(dr["NextDueDate"]) + "'," +
    //"'"+ Convert.ToString(dr["ProposalRef"]) +"',"+
    //"'"+ Convert.ToString(dr["PaymentRef"]) +"',"+
    "'" + Convert.ToString(dr["PayFreq"]) + "'," +

    ////==== BRANCH CODE ADDED
    "'" + Convert.ToString(dr["BranchCode"]) + "', " +
    ///===== BRANCH CODE ADDED

    "'" + Convert.ToString(dr["ColctBranch"]) + "', " +
    "'" + Convert.ToString(dr["BankName"]) + "'," +
    "'" + Convert.ToString(dr["1stYearCmsn"]) + "'," +
    "'" + Convert.ToString(dr["Prod"]) + "'," +
    "'" + Convert.ToString(dr["Riders Detail"]) + "'," +
    "'" + Convert.ToString(dr["BnftTerm"]) + "'," +
    "'" + Convert.ToString(dr["PayTerm"]) + "', " +
    "'" + Convert.ToString(dr["Prod_SmAsurd"]) + "'," +
    "'" + Convert.ToString(dr["Basic Premium"]) + "'," +
    "'" + Convert.ToString(dr["Rider Premium"]) + "'," +
    "'" + Convert.ToString(dr["Modal Premium"]) + "'," +
////============== CREDIT BALANCE ADDED 01-02-2018
"'" + Convert.ToString(dr["CREDIT BALANCE"]) + "'," +
////============== CREDIT BALANCE ADDED 01-02-2018
"'" + Convert.ToString(dr["AGENT CODE"]) + "'," +
"'" + Convert.ToString(dr["AGENT NAME"]) + "')";




                    }

                    else if(GenerateFileType.DataDump.ToString()==FileType)
					{					
						ExcelQuery = "INSERT INTO [Sheet1$] ([Application No], [Customer Name],[Customer CNIC Number],[Nominee Name],"+
							"[Relation With Nominee],[ADDRESS],[Residence Number], [Office Number], [Cell Number], [Plan Name],"+
							"[Premium Amount],[Mode], [Sum Assured],  [Date of Birth],[Gender],[FAP Amount],[Counter], "+
							"[Insurance Remarks], [Policy No], [Policy Status],[Commencement Date], [Maturity Date],[Policy Issue Date], [Renewal Due Date],  "+
							"[Bank Commission Rate], [Bank Commission]) "+
							"VALUES ("+
							"'"+ Convert.ToString(dr["ApplicationNo"]) +"',"+
							"'"+ Convert.ToString(dr["CustomerName"]) +"',"+
							"'"+ Convert.ToString(dr["CustomerCNICNumber"]) +"',"+
							"'"+ Convert.ToString(dr["NomineeName"]) +"',"+
							"'"+ Convert.ToString(dr["RelationWithNominee"]) +"',"+
							"'"+ Convert.ToString(dr["ADDRESS"]).Trim('\'') +"', "+
							"'"+ Convert.ToString(dr["ResidenceNumber"]) +"',"+
							"'"+ Convert.ToString(dr["OfficeNumber"]) +"',"+
							"'"+ Convert.ToString(dr["CellNumber"]) +"',"+
							"'"+ Convert.ToString(dr["PlanName"]) +"', "+
							"'"+ Convert.ToString(dr["PremiumAmount"]) +"', "+
							"'"+ Convert.ToString(dr["PayMode"]) +"', "+
							"'"+ Convert.ToString(dr["SumAssured"]) +"',"+
							"'"+ Convert.ToString(dr["DateofBirth"]) +"',"+
							"'"+ Convert.ToString(dr["Gender"]) +"',"+
							"'"+ Convert.ToString(dr["FAPAmount"]) +"',"+
							"'"+ Convert.ToString(dr["Counter"]) +"',"+
							"'"+ Convert.ToString(dr["InsuranceRemarks"]) +"',"+
							"'"+ Convert.ToString(dr["PolicyNo"]) +"',"+
							"'"+ Convert.ToString(dr["PolicyStatus"]) +"',"+
							"'"+ Convert.ToString(dr["CommencementDate"]) +"',"+
							"'"+ Convert.ToString(dr["MaturityDate"]) +"',"+
							"'"+ Convert.ToString(dr["PolicyIssueDate"]) +"',"+
							"'"+ Convert.ToString(dr["RenewalDueDate"]) +"', "+
							"'"+ Convert.ToString(dr["BankCommissionRate"]) +"',"+
							"'"+ Convert.ToString(dr["BankCommission"]) +"')";



					}

					
					else if(GenerateFileType.IlasMis.ToString()==FileType)
					{					
						if(ddlFileType.SelectedValue == "Pending")
						{
							ExcelQuery = "INSERT INTO [Sheet1$] ([SNo], [Input Date],[Client],[Date of Birth],"+
								"[Application No],[Policy No],[Product], [Account No], [Branch], [Bank Sale Officer],"+
								"[Mode of Premium],[FAP],[Modal Premium], [Annual Premium], [Status], [Requirements],"+
								"[Reasons],[Policy Year],[Issue Date], [Next Due Date], [Status Date], [Commencement Date],"+
								"[Maturity Date],[Term], [Total Premium Received],  [Agent Code],[BSC Coordinator],[Mobile],[Phone],[Address], "+
								"[Credit Balance],[Commission], [Branch Code],  [Dispatch Date],[CNIC],[Agent Name],[Cheque Date],[Counter], "+
								"[Sum Assured]) "+
								"VALUES ("+
								"'"+ Convert.ToString(dr["SNo"]) +"',"+
								"'"+ Convert.ToDateTime(dr["InputDate"]).ToString("MM/dd/yyyy")  +"',"+
								"'"+ Convert.ToString(dr["Client"]) +"',"+
								"'"+ Convert.ToString(dr["DateofBirth"]) +"',"+
								"'"+ Convert.ToString(dr["ApplicationNo"]) +"',"+
								"'"+ Convert.ToString(dr["PolicyNo"]) +"', "+
								"'"+ Convert.ToString(dr["Product"]) +"',"+
								"'"+ Convert.ToString(dr["AccountNo"]) +"',"+
								"'"+ Convert.ToString(dr["BankBranch"]) +"',"+
								"'"+ Convert.ToString(dr["BankSaleOfficer"]) +"', "+
								"'"+ Convert.ToString(dr["ModeofPremium"]) +"', "+
								"'"+ Convert.ToString(dr["FAP"]) +"', "+
								"'"+ Convert.ToString(dr["ModalPremium"]) +"',"+
								"'"+ Convert.ToString(dr["AnnualPremium"]) +"',"+
								"'"+ Convert.ToString(dr["Status"]) +"',"+
								"'"+ Convert.ToString(dr["Requirements"]) +"',"+
								"'"+ Convert.ToString(dr["Reasons"]) +"',"+
								"'"+ Convert.ToString(dr["PolicyYear"]) +"',"+
								"'"+ Convert.ToString(dr["IssuedDate"])+"',"+
								"'"+ Convert.ToString(dr["NextDueDate"])+"',"+
								"'"+ Convert.ToString(dr["StatusDate"])+"',"+
								"'"+ Convert.ToString(dr["CommencementDate"])+"',"+
								"'"+ Convert.ToString(dr["MaturityDate"])+"',"+
								"'"+ Convert.ToString(dr["Term"]) +"', "+
								"'"+ Convert.ToString(dr["TotalPremiumReceived"]) +"',"+
								"'"+ Convert.ToString(dr["AgentCode"]) +"',"+
								"'"+ Convert.ToString(dr["BSCCoordinator"]) +"',"+
								"'"+ Convert.ToString(dr["Mobile"]) +"',"+
								"'"+ Convert.ToString(dr["Phone"]) +"',"+
								"'"+ Convert.ToString(dr["Address"]).Trim('\'').Trim('\"').Trim('`') + "',"+
								"'"+ Convert.ToString(dr["CreditBalance"]) +"',"+
								"'"+ Convert.ToString(dr["Commission"]) +"',"+
								"'"+ Convert.ToString(dr["BranchCode"]) +"',"+
								"'"+ Convert.ToString(dr["DispatchDate"]) +"',"+
								"'"+ Convert.ToString(dr["CNIC"]) +"',"+
								"'"+ Convert.ToString(dr["agentname"]) +"',"+
								"'"+ Convert.ToString(dr["ChequeDate"]) +"',"+
								"'"+ Convert.ToString(dr["Counter"]) +"',"+
								"'"+ Convert.ToString(dr["SumAssured"]) +"')";
						}
						else
						{
							ExcelQuery = "INSERT INTO [Sheet1$] ([SNo], [Input Date],[Client],[Date of Birth],"+
								"[Application No],[Policy No],[Product], [Account No], [Branch], [Bank Sale Officer],"+
								"[Mode of Premium],[FAP],[Modal Premium], [Annual Premium], [Status], [Requirements],"+
								"[Reasons],[Policy Year],[Issue Date], [Next Due Date], [Status Date], [Commencement Date],"+
								"[Maturity Date],[Term], [Total Premium Received],  [Agent Code],[BSC Coordinator],[Mobile],[Phone],[Address], "+
								"[Credit Balance],[Commission], [Branch Code],  [Dispatch Date],[CNIC],[Agent Name],[Cheque Date],[Counter], "+
								"[Sum Assured]) "+
								"VALUES ("+
								"'"+ Convert.ToString(dr["SNo"]) +"',"+
								"'"+ Convert.ToDateTime(dr["InputDate"]).ToString("MM/dd/yyyy")  +"',"+
								"'"+ Convert.ToString(dr["Client"]) +"',"+
								"'"+ Convert.ToString(dr["DateofBirth"]) +"',"+
								"'"+ Convert.ToString(dr["ApplicationNo"]) +"',"+
								"'"+ Convert.ToString(dr["PolicyNo"]) +"', "+
								"'"+ Convert.ToString(dr["Product"]) +"',"+
								"'"+ Convert.ToString(dr["AccountNo"]) +"',"+
								"'"+ Convert.ToString(dr["BankBranch"]) +"',"+
								"'"+ Convert.ToString(dr["BankSaleOfficer"]) +"', "+
								"'"+ Convert.ToString(dr["ModeofPremium"]) +"', "+
								"'"+ Convert.ToString(dr["FAP"]) +"', "+
								"'"+ Convert.ToString(dr["ModalPremium"]) +"',"+
								"'"+ Convert.ToString(dr["AnnualPremium"]) +"',"+
								"'"+ Convert.ToString(dr["Status"]) +"',"+
								"'"+ Convert.ToString(dr["Requirements"]) +"',"+
								"'"+ Convert.ToString(dr["Reasons"]) +"',"+
								"'"+ Convert.ToString(dr["PolicyYear"]) +"',"+
								"'"+ Convert.ToDateTime(dr["IssuedDate"]).ToString("MM/dd/yyyy")  +"',"+
								"'"+ Convert.ToDateTime(dr["NextDueDate"]).ToString("MM/dd/yyyy") +"',"+
								"'"+ Convert.ToDateTime(dr["StatusDate"]).ToString("MM/dd/yyyy")+"',"+
								"'"+ Convert.ToDateTime(dr["CommencementDate"]).ToString("MM/dd/yyyy") +"',"+
								"'"+ Convert.ToDateTime(dr["MaturityDate"]).ToString("MM/dd/yyyy")  +"',"+
								"'"+ Convert.ToString(dr["Term"]) +"', "+
								"'"+ Convert.ToString(dr["TotalPremiumReceived"]) +"',"+
								"'"+ Convert.ToString(dr["AgentCode"]) +"',"+
								"'"+ Convert.ToString(dr["BSCCoordinator"]) +"',"+
								"'"+ Convert.ToString(dr["Mobile"]) +"',"+
								"'"+ Convert.ToString(dr["Phone"]) +"',"+
								"'"+ Convert.ToString(dr["Address"]).Trim('\'').Trim('\"').Trim('`') + "',"+
								"'"+ Convert.ToString(dr["CreditBalance"]) +"',"+
								"'"+ Convert.ToString(dr["Commission"]) +"',"+
								"'"+ Convert.ToString(dr["BranchCode"]) +"',"+
								"'"+ Convert.ToString(dr["DispatchDate"]) +"',"+
								"'"+ Convert.ToString(dr["CNIC"]) +"',"+
								"'"+ Convert.ToString(dr["agentname"]) +"',"+
								"'"+ Convert.ToString(dr["ChequeDate"]) +"',"+
								"'"+ Convert.ToString(dr["Counter"]) +"',"+
								"'"+ Convert.ToString(dr["SumAssured"]) +"')";
						}


					}
                    try
                    {
						ExcelCommand = new OleDbCommand(ExcelQuery, ExcelConnection);
						ExcelCommand.ExecuteNonQuery();
						i++;
					}
                    catch (Exception ex)
                    {
                        throw;
                    }
				
				}
				ExcelConnection.Close();
				ExcelConnection.Dispose();
				ExcelCommand.Dispose();
                SessionObject.Set("DownloadCompleted", "True");
            }
			catch(Exception ex)
			{
				SessionObject.Set("DownloadCompleted", "True");
				StringBuilder errMessage = new StringBuilder();
				errMessage.Append(ex.Message);
			}
		}

		private void WriteFileOnClient(string DownloadPage)
		{
			string popupScript = " window.open('"+DownloadPage+"','DownloadExcelFile','width=1,height=1,location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,top=1000');";
			callJs.Text=popupScript;
			
		}

        // Excel Function
        private void ExportGridToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                string filename = "Generate_Banca_file " + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write("<b><u>Banca Proposal Data MIS</u></b>");
                Response.Write("<br />");
              //  Response.Write(Label28.Text + " " + lblDateFrom.Text + "  To  " + lblDateTo.Text + "<br />");
                Response.Write("<br />");
                //Result detail

                Response.Write(tw.ToString());
                Response.End();
            }



        }
        private void GenerateBancaToExcel(DataTable dt)
        {

           // if (dt.Rows.Count > 0)
           // {
                string filename = "Generate_Banca_file " + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write("<b><u>Generate Banca file</u></b>");
                Response.Write("<br />");
                //  Response.Write(Label28.Text + " " + lblDateFrom.Text + "  To  " + lblDateTo.Text + "<br />");
                Response.Write("<br />");
                //Result detail

                Response.Write(tw.ToString());
                Response.End();
          //  }



        }
        private void ExportIlastoExcel(DataTable dt)
        {


         //   if (dt == null || dt.Rows.Count == 0) return;

            // Remove first column (index 0)
            if (dt.Columns.Count > 0)
                dt.Columns.RemoveAt(0);

           

            // Prepare filename
            string filename = "downloadProposalIlas" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";

            // Build HTML table manually (reliable)
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html><head>");
            sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>");

            //  Use Calibri 11pt, no explicit border styling (so Excel gridlines show)
            sb.AppendLine("<style>");
            sb.AppendLine("body, table, td, th { font-family: Calibri, Arial, sans-serif; font-size: 11pt; }");
            sb.AppendLine("td { mso-number-format:'\\@'; }");
            sb.AppendLine("th { mso-number-format:'\\@'; font-weight:bold; background-color:#D9D9D9; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head><body>");

            //  Keep border='1' for default thin Excel gridlines
            sb.AppendLine("<table border='1' cellpadding='3' cellspacing='0'>");

            // Header row
            sb.AppendLine("<tr>");
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(HttpUtility.HtmlEncode(col.ColumnName));
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Insert three blank rows AFTER the header
            for (int b = 0; b < 3; b++)
            {
                sb.AppendLine("<tr>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    sb.AppendLine("<td>&nbsp;</td>");
                }
                sb.AppendLine("</tr>");
            }

            // Data rows
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("<tr>");
                foreach (DataColumn col in dt.Columns)
                {
                    string cell = row[col] == null ? string.Empty : row[col].ToString();
                    sb.Append("<td>");
                    sb.Append(HttpUtility.HtmlEncode(cell));
                    sb.AppendLine("</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            // Send to client
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.Write(sb.ToString());
            Response.End();



        }

        private void ExportBankFiletoExcel(DataTable dt)
        {


           // if (dt == null || dt.Rows.Count == 0) return;

            string[] removeColumns = { "ISSUEDDATE", "NP1_PROPDATE", "PROPSIGNDATE", "PAYMENTREF", "PROPOSALREF", "CUSTOMERDOB", "CUSTOMERAGE", "BANKNAME", 
                "Rider Premium","Modal Premium","USERNAME","CURRENTSTATUS" };

                                          
            foreach (string col in removeColumns)
            {
                if (dt.Columns.Contains(col))
                    dt.Columns.Remove(col);
            }

               


            // Remove first column (index 0)
            //if (dt.Columns.Count > 0)
            //    dt.Columns.RemoveAt(0);

            // Remove columns 1 
            //if (dt.Columns.Count > 1)
            //    dt.Columns.RemoveAt(1);

            //if (dt.Columns.Count > 0)
            //    dt.Columns.RemoveAt(0);


            // Prepare filename
            string filename = "downloadProposalUbl" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";

            // Build HTML table manually (reliable)
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html><head>");
            sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>");

            //  Use Calibri 11pt, no explicit border styling (so Excel gridlines show)
            sb.AppendLine("<style>");
            sb.AppendLine("body, table, td, th { font-family: Calibri, Arial, sans-serif; font-size: 11pt; }");
            sb.AppendLine("td { mso-number-format:'\\@'; }");
            sb.AppendLine("th { mso-number-format:'\\@'; font-weight:bold; background-color:#D9D9D9; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head><body>");

            //  Keep border='1' for default thin Excel gridlines
            sb.AppendLine("<table border='1' cellpadding='3' cellspacing='0'>");

            // Header row
            sb.AppendLine("<tr>");
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(HttpUtility.HtmlEncode(col.ColumnName));
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Insert three blank rows AFTER the header
            for (int b = 0; b < 3; b++)
            {
                sb.AppendLine("<tr>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    sb.AppendLine("<td>&nbsp;</td>");
                }
                sb.AppendLine("</tr>");
            }

            // Data rows
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("<tr>");
                foreach (DataColumn col in dt.Columns)
                {
                    string cell = row[col] == null ? string.Empty : row[col].ToString();
                    sb.Append("<td>");
                    sb.Append(HttpUtility.HtmlEncode(cell));
                    sb.AppendLine("</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            // Send to client
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.Write(sb.ToString());
            Response.End();



        }
        protected void btngeneratebank_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
			    IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
			   BANKCODE="ALL";
			}
			else
			{
			   BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
			DateType="ProposalDate";
			}
			DataTable dt = LNP1_POLICYMASTRDB.GetExcelReportForUbl(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
            //    	WriteExcel(dt,ConfigurationSettings.AppSettings["downloadUblName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeUblFilePath"],GenerateFileType.UBL.ToString());
            //WriteFileOnClient("DownloadUblFile.aspx"); by default comment
            //	SetDownloadLink(hlublfile,ConfigurationSettings.AppSettings["downloadUblName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeUblFilePath"],GenerateFileType.UBL.ToString());

            // Imran Code
            ExportBankFiletoExcel(dt);

             //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data found!');", true);
           

        }
		protected void btngeneratebanc_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
				IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
				BANKCODE="ALL";
			}
			else
			{
				BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
				DateType="ProposalDate";
			}

			DataTable dt = LNP1_POLICYMASTRDB.GetExcelReportForBanca(txtDATEFROM.Text, txtDATETO.Text, BANKCODE, BRANCHCODE, IsAdmin, DateType);

            
                WriteExcel(dt, ConfigurationSettings.AppSettings["downloadBancaName"] + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls", ConfigurationSettings.AppSettings["prototypeBancaFilePath"], GenerateFileType.Banca.ToString());
                //WriteFileOnClient("DownloadBancassuranceFile.aspx");by default
                SetDownloadLink(hlbanca, ConfigurationSettings.AppSettings["downloadBancaName"] + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls", ConfigurationSettings.AppSettings["prototypeBancaFilePath"], GenerateFileType.Banca.ToString());
           
            // Imran code
          //      GenerateBancaToExcel(dt);
            
            



            }

        protected void btngenerateilasfile_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string USE_USERID=Session["s_USE_USERID"].ToString().ToUpper();
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
				IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
				BANKCODE="ALL";
			}
			else
			{
				BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
				DateType="ProposalDate";
			}
			string downloadFileName = ConfigurationSettings.AppSettings["downloadIlasName"] + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
			DataTable dt = new DataTable();

			dt = LNP1_POLICYMASTRDB.GetExcelReportForIlas(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,ddlstatus.SelectedValue,DateType);
            //	WriteExcel(dt, downloadFileName, ConfigurationSettings.AppSettings["prototypeIlasFilePath"],GenerateFileType.Ilas.ToString());
            //WriteFileOnClient("DownloadIlasFile.aspx");
            //	SetDownloadLink(hlIlasile, downloadFileName, ConfigurationSettings.AppSettings["prototypeIlasFilePath"],GenerateFileType.Ilas.ToString());

            // Imran code 02 NOV 25
            ExportIlastoExcel(dt);

            





        }

		protected void btngeneratedatadumpfile_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
				IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
				BANKCODE="ALL";
			}
			else
			{
				BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
				DateType="ProposalDate";
			}

			DataTable dt = LNP1_POLICYMASTRDB.GetExcelReportForDataDump(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
			WriteExcel(dt,ConfigurationSettings.AppSettings["downloadDataDumpName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeDataDumpFilePath"],GenerateFileType.DataDump.ToString());
			SetDownloadLink(hlDataDumpFile,ConfigurationSettings.AppSettings["downloadDataDumpName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeDataDumpFilePath"],GenerateFileType.DataDump.ToString());
			
		}

		protected void btngenerateIlasMisfile_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
				IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
				BANKCODE="ALL";
			}
			else
			{
				BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
				DateType="ProposalDate";
			}

			DataTable dt=null;
			if(ddlFileType.SelectedValue == "Pending")
			{
                DateType = "Pending";
				dt = LNP1_POLICYMASTRDB.GetExcelReportForBancaMis(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
			}
			else
			{
				dt = LNP1_POLICYMASTRDB.GetExcelReportForIlasMis(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
			}

          
            WriteExcel(dt,ConfigurationSettings.AppSettings["downloadIlasMisName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeIlasMisFilePath"],GenerateFileType.IlasMis.ToString());
			SetDownloadLink(hlIlasMisFile,ConfigurationSettings.AppSettings["downloadIlasMisName"]+DateTime.Now.ToString("yyyyMMddHHmm")+".xls",ConfigurationSettings.AppSettings["prototypeIlasMisFilePath"],GenerateFileType.IlasMis.ToString());
		}

		protected void btnTextFile_Click(object sender, System.EventArgs e)
		{
			string BANKCODE="";
			string BRANCHCODE=Session["s_CCS_CODE"].ToString();
			bool IsAdmin=false;
			
			if(Session["s_USE_USERID"].ToString().ToUpper()=="ADMIN")
			{
				IsAdmin=true;
			}
			if(ddlCCS_CODE.SelectedItem.Text=="All Bank")
			{
				BANKCODE="ALL";
			}
			else
			{
				BANKCODE=ddlCCS_CODE.SelectedValue;
			}

			string DateType="IssueDate";
			if(ddlDate.SelectedValue == "ProposalDate")
			{
				DateType="ProposalDate";
			}

			DataTable dt=null;
			if(ddlFileType.SelectedValue == "Pending")
			{
				dt = LNP1_POLICYMASTRDB.GetBancaMisUpload(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
			}
			else 
			{
				dt = LNP1_POLICYMASTRDB.GetIlasMisUpload(txtDATEFROM.Text,txtDATETO.Text,BANKCODE,BRANCHCODE,IsAdmin,DateType);
			}
				WriteTextFile(dt);
		}


		private void WriteTextFile(DataTable dt)
		{
            try
            {
                string attachment = "attachment; filename=ExportProposalList.txt";
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                HttpContext.Current.Response.ContentType = "text";
                //			HttpContext.Current.Response.AddHeader("Pragma", "public");

                //			DataTable dt = new DataTable();
                //			dt.Columns.Add("ID");
                //			dt.Columns.Add("Name");
                //			DataRow workRow;
                //			for (int x = 0; x <= 5; x++) 
                //			{
                //				workRow = dt.NewRow();
                //				workRow[0] = x;
                //				workRow[1] = "CustName" + x.ToString();
                //				dt.Rows.Add(workRow);
                //			}

                //			int iCol = dt.Columns.Count-1;
                //			int iRow = dt.Rows.Count;
                ////			WriteColumnName();
                //			for(int j = 0; j < iRow; j++)
                //			{
                //				DataRow dRow = dt.Rows[j];				
                //				for (int i = 0; i < iCol; i++)
                //				{					
                //					if (!Convert.IsDBNull(dRow[i]))
                //					{
                //						HttpContext.Current.Response.Write(dRow[i].ToString());
                //					}
                //					if ( i < iCol - 1)
                //					{
                //						HttpContext.Current.Response.Write(",");
                //					}
                //				}
                //				HttpContext.Current.Response.Write(Environment.NewLine);
                //			}			

                HttpContext.Current.Response.Write(exportData(dt));

                HttpContext.Current.Response.End();
            }
            catch (Exception)
            {
                Session["DownloadCompleted"] = true;
            }
			
		}

		private string exportData(DataTable dt)
		{			
			StringBuilder sb = new StringBuilder();
			foreach(DataRow row in dt.Rows)
			{
				foreach(DataColumn col in dt.Columns)
				{
//					if(col.DataType == typeof(System.DateTime))
//					{						
//						sb.Append(row[col].ToString());
//					}
//					else 
//					{			
					if(col.ColumnName != "NP1_PROPDATE")
					{		

					if(ddlFileType.SelectedValue == "Pending")
					{					    
						if(col.ColumnName == "ISSUEDDATE" || col.ColumnName == "COMMENCEMENTDATE"
							|| col.ColumnName == "MATURITYDATE" || col.ColumnName == "NEXTDUEDATE")
						{		
								sb.Append("");
								sb.Append("|");
						}
						else
						{
							sb.Append(row[col].ToString());
							sb.Append("|");
						}
					}
					else
					{
						if(col.ColumnName == "ISSUEDDATE" )
						{		
							if (row[col] != null && row[col].ToString() != "")
							{
								sb.Append(Convert.ToDateTime(row[col]).Date.ToString("MM/dd/yyyy"));
								sb.Append("|");
							}
							else
							{
								sb.Append("");
								sb.Append("|");

							}
						}
						else
						{
							sb.Append(row[col].ToString());
							sb.Append("|");

						}
					}
					}

//					}
//					if(col.Ordinal < dt.Columns.Count - 1)
//					{
//						sb.Append(",");
//					}
				}				
				sb.Append(Environment.NewLine);				
			}
			sb.Replace("00:00:00","");

			return sb.ToString();
		}

		enum GenerateFileType
		{
			Banca=1,
			UBL=2,
			Ilas=3,
			DataDump=4,
			IlasMis=5
		};

        protected void btnUBLMIS_Click(object sender, EventArgs e)
        {
			MISRepClass MyClass = new MISRepClass();
			DataTable dt_MIS = new DataTable();
			string Sql;
			try
			{
				Sql = "Select A.Result from View_MIS_UBL_Text A where trunc(A.Issue_Date) between to_date('" + txtDATEFROM.Text.ToString() + "','dd/mm/yyyy') AND to_date('" + txtDATETO.Text.ToString() + "','dd/mm/yyyy')";

				dt_MIS = GetdataOraOledb(Sql);

				if (dt_MIS.Rows.Count > 0)
				{
					string filePath = Server.MapPath("~/Presentation/TextFile/UBL_NB.txt"); //@"C:\Imran\output.txt";


					// Export DataTable to text file and open it

					MyClass.ExportDataTableToTextFile(dt_MIS, filePath, ',');
					DisplayFileContent(filePath);


					
				}
				else
				{
					ShowAlert("No Data Found");
				}
			}
			catch (Exception Ex)
			{
				ShowAlert(Ex.Message);
				


			}





		}

        protected void btnUBLPHS_Click(object sender, EventArgs e)
        {
			MISRepClass MyClass = new MISRepClass();
			DataTable dt_MIS = new DataTable();
			string Sql;
			try
			{
				
				Sql = "Select c.UBL_ReferenceNo,c.Policy_Status,c.Policy_Status_Date from View_UBL_PHS c" +
					" where trunc(c.np1_issuedate) between to_date('" + txtDATEFROM.Text.ToString() + "','dd/mm/yyyy') AND to_date('" + txtDATETO.Text.ToString() + "','dd/mm/yyyy')";


				dt_MIS = GetdataOraOledb(Sql);

				if (dt_MIS.Rows.Count > 0)
				{

					string filePath = Server.MapPath("~/Presentation/TextFile/UBL_PHS.txt"); //@"C:\Imran\output.txt";


					// Export DataTable to text file 

					MyClass.ExportDataTableToTextFile(dt_MIS, filePath, ',');
					DisplayFileContent(filePath); // Open text file
					
				}

				else

				{
					ShowAlert("No Data Found");
				}
			}
			catch (Exception Ex)
			{
				ShowAlert(Ex.Message);
				


			}
		}


		#region "MISCodeForUBL"
		private OleDbConnection GetOraCon()
		{
			OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSNILAS"]);
			return conOra;
		}

		public DataTable GetdataOraOledb(string sql)
		{
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = GetOraCon();
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
		public partial class MISRepClass // This class use for UBL MIS Report in txt form
		{


			// Example method to export DataTable to a text file and open it
			public void ExportDataTableToTextFile(DataTable dataTable, string filePath, char delimiter = ',')
			{
				// Use a StringBuilder to build the output text
				StringBuilder fileContent = new StringBuilder();

				// Add column headers to the first line
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					fileContent.Append(dataTable.Columns[i].ColumnName);

					// Add delimiter if not the last column
					if (i < dataTable.Columns.Count - 1)
						fileContent.Append(delimiter);
				}
				fileContent.AppendLine();

				// Add each row's data
				foreach (DataRow row in dataTable.Rows)
				{
					for (int i = 0; i < dataTable.Columns.Count; i++)
					{
						fileContent.Append(row[i].ToString());

						// Add delimiter if not the last column
						if (i < dataTable.Columns.Count - 1)
							fileContent.Append(delimiter);
					}
					fileContent.AppendLine();
				}

				// Write the content to the specified file path
				File.WriteAllText(filePath, fileContent.ToString());

			

			}
		

		}

		protected void ShowAlert(string message)
		{
			string script = $"alert('{message}');";
			//ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
		}

		protected void DisplayFileContent(string filePath)
		{
			string fileContent = File.ReadAllText(filePath);
			
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
			Response.Write(fileContent);
			Response.End();
		}
		#endregion



	}
}
