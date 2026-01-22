namespace Bancassurance
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Configuration;
	using System.Collections;
	using System.ComponentModel;
	using System.Web.SessionState;
	using SHMA.Enterprise.Presentation;
	using SHAB.Data;
	using SHAB.Business;
	using SHMA.Enterprise;
	using SHMA.Enterprise.Data;
	using System.Data.OleDb;
	using System.IO;
	using System.Globalization;

	/// <summary>
	///		Summary description for uploader.
	/// </summary>
	public partial class uploader : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		public string uploadFile(string fileName,string folderName)
		{
			 if(fileName=="")
			{
				return "Invalid filename supplied";
			}
//			if(fileUpload.PostedFile.ContentLength==0)
//			{
//				return "Invalid file content";
//			}

			fileName = System.IO.Path.GetFileName(fileName);
			
			string strFilename, strMessage;

			strFilename = fileUpload.PostedFile.FileName.ToString();

			if(folderName=="")
			{
				return "Path not found";
			}  
			try
			{
				if (fileUpload.PostedFile.ContentLength<=2048000)
				{    
					fileUpload.PostedFile.SaveAs(Server.MapPath(folderName)+"\\"+fileName); 
   
					strMessage = ReadFile(strFilename);
					if(strMessage == "")
					{
						strMessage="File uploaded successfully";
					}
					return strMessage;    
				}
				else
				{
					return "Unable to upload,file exceeds maximum limit";
				}
			}
			catch(UnauthorizedAccessException ex)
			{
				return ex.Message + "Permission to upload file denied";
			}
			finally
			{
				fileUpload.Dispose();
			}
		}
		
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			string strFilename, strMessage="";
			strFilename = fileUpload.PostedFile.FileName.ToString();
			if(strFilename=="")
				return;


//			if (strMessage == "")
//			{
				strMessage = uploadFile(ConfigurationSettings.AppSettings["fileName"],ConfigurationSettings.AppSettings["folderPath"]);
//			}
			lblMessage.Text = DateTime.Now.ToString() +" : "+ strMessage;
			lblMessage.ForeColor = Color.Red;
			//l1.Text=strFilename;
		}

		private string ReadFile(string filename)
		{

			OleDbConnection conn=null;
			OleDbCommand cmd=null;
			OleDbDataAdapter da = new OleDbDataAdapter();
			System.Text.StringBuilder errMessage = new System.Text.StringBuilder();

			try
			{
				
				DataSet ds;
				//string query = "SELECT * FROM [Sheet1$]";
				string path = Server.MapPath(ConfigurationSettings.AppSettings["folderPath"]+"\\"+ConfigurationSettings.AppSettings["fileName"]);
				//path = filename;
				string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
				conn = new OleDbConnection(connString);
				conn.Open();
				System.Data.DataTable dtS = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				/*
				string SheetName="";
				foreach(DataRow row in dtS.Rows)
				{
					SheetName = row["TABLE_NAME"].ToString();
					break;
				}
				*/
				string query = "SELECT * FROM [" + dtS.Rows[0]["TABLE_NAME"] + "]";
				cmd = new OleDbCommand(query, conn);
				da = new OleDbDataAdapter(cmd);
				ds = new DataSet();
				da.Fill(ds);
				
				//dg.DataSource=ds;
				//dg.DataBind();
				
				//string ss= "05/09/2015";

//				string myDateTimeValue = "2/16/1992 12:15:12";
//				DateTime myDateTime = DateTime.Parse(myDateTimeValue);
//				Console.WriteLine("1) myDateTime       = {0}", myDateTime);


//				string dateString, format;  
//				DateTime result;
//				CultureInfo provider = CultureInfo.InvariantCulture;
//
//				// Parse date and time with custom specifier.
//				dateString = "Sun 15 Jun 2008 8:30 AM -06:00";
//				format = "ddd dd MMM yyyy h:mm tt zzz";
//				try 
//				{
//					result = DateTime.ParseExact(dateString, format, provider);
//					Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
//				}
//				catch (FormatException) 
//				{
//					Console.WriteLine("{0} is not in the correct format.", dateString);
//				}



				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					//String myNewDate = convertDateTypes("12/01/2006", "DMY");
					//String myNewDate2 = convertDateTypes(dr[10].ToString(), "DMY");

					String current ="";
					String postdate="";

					DateTime CommencDate=Convert.ToDateTime(dr[10].ToString());
					postdate=convertDateTypes(CommencDate.ToShortDateString(),"DMY");
					current=convertDateTypes(DateTime.Now.ToShortDateString(),"DMY");

					//DateTime CommencAADate=Convert.ToDateTime(ss);
					
						//if(CommencDate > DateTime.Now || CommencDate.AddDays(30) < DateTime.Now )
					if(Convert.ToDateTime(postdate) > Convert.ToDateTime(current) || Convert.ToDateTime(postdate).AddDays(30) < Convert.ToDateTime(current) )
					//	if(Convert.ToDateTime(postdate) > DateTime.Now || Convert.ToDateTime(postdate).AddDays(30) < DateTime.Now )
						{
							throw new Exception("Commencement Date cannot be greater than current /n and more than a month old");

						}


				}

				DataTable dt = traverseFile(ds.Tables[0]);

				conn.Close();

				dg.DataSource=dt;
				dg.DataBind();

				errMessage.Append("");
				
			}
			catch(Exception ex)
			{
				errMessage.Append(ex.Message);
			}
			finally
			{
				cmd.Dispose();
				conn.Close();
				conn.Dispose();

			}
			return errMessage.ToString();
		}
		public DataTable traverseFile(DataTable dt){
			
			ArrayList al = new ArrayList();
			DataTable retDt = new DataTable("return Table");
			retDt.Columns.Add("Updated Proposal No.",typeof(string));

			foreach(DataRow dr in dt.Rows)
			{
				string proposal = dr[0].ToString();
				string naration = dr[7].ToString();
				string remarks = dr[11].ToString();
				//DateTime CommencDate=Convert.ToDateTime(dr[10].ToString());
				DateTime CommencDate=ace.Ace_General.getGeneratedCommencementDate(Convert.ToDateTime(dr[10].ToString()));

				//if(CommencDate.Day>28)
				//{
				//	string ComDate="28/"+CommencDate.Month+"/"+CommencDate.Year;
				//	CommencDate=Convert.ToDateTime(ComDate);
				//}



				if(naration.ToUpper().IndexOf("STATE LIFE PREMIUM DEDUCTION AGAINST PROPOSAL")>=0)
				{
					proposal = naration.Substring("STATE LIFE PREMIUM DEDUCTION AGAINST PROPOSAL".Length).Trim();
					if(LNP1_POLICYMASTRDB.getStatus(proposal).ToUpper().Equals("F"))
					{
						if(remarks.Equals("Transaction successfully completed"))
						{
							LNP1_POLICYMASTR.markStatus(proposal,"Y-FromFile");
							LNP1_POLICYMASTR.UpdateCommencmentDate(proposal,CommencDate);
							LNCM_COMMENTS.AddComentsInTable1(proposal,remarks,"PR");
						}
						else
						{
							LNP1_POLICYMASTR.markStatus(proposal,"R");//Reverse to UBLTest

							LNCM_COMMENTS.AddComentsInTable1(proposal,remarks,"PD");
						}
						al.Add(proposal);
						DataRow retDr=retDt.NewRow();
						retDr[0]=proposal;
						retDt.Rows.Add(retDr);
					}
				}
				naration = "";
				proposal = "";
		
			}
			
			return retDt;

		}

		string convertDateTypes(String sDate , String sType ) 
		{
			// MDY = mm/dd/yyyy
			// DMY = mm/dd/yyyy
			string sResponse = "";
			string sSeparator = "/";
			if ( sType == "MDY" ) 
			{
				// convert from DMY to MDY
				string sDay = sDate.Substring(0, 2);
				string sMon = sDate.Substring(3, 2);
				sResponse=sMon + sSeparator + sDay + sSeparator +
					sDate.Substring(sDate.Length-4, 4);
			}
			else 
			{
				// convert from DMY to MDY
				string sDay = sDate.Substring(3, 2);
				string sMon = sDate.Substring(0, 2);
				sResponse=sMon + sSeparator + sDay + sSeparator +
					sDate.Substring(sDate.Length-4, 4);
			}
			return sResponse;
		}

	}
}
