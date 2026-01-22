using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Enterprise;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
//using SHAB.Data;
//using SHAB.Shared.Exceptions;
using shgn;
using SHMA.CodeVision.Business;


/*

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CrystalDecisions.Web;
//using CrystalDecisions.Web.Design;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.



using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
//using SHAB.Data;
//using SHAB.Shared.Exceptions;
using shgn;
using SHMA.CodeVision.Business;
using System.IO;*/

using System.Configuration;
using System.Data.SqlClient;

namespace Presentation
{
	
	public class CrystalReport : System.Web.UI.Page
	{

		string strconn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
		protected System.Web.UI.WebControls.Literal MessageScript;
		ReportDocument doc = new ReportDocument();
		NameValueCollection NVC = new NameValueCollection();
		public bool bl_isPostBack = false;
		CrystalDecisions.Shared.TableLogOnInfo tl ;
		ConnectionInfo cinfo;
		protected CrystalDecisions.Web.CrystalReportViewer Crv;
		protected System.Web.UI.WebControls.Button btnExp;
		protected System.Web.UI.WebControls.DropDownList ddLExp;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divExp;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblProposal;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		CrystalDecisions.CrystalReports.Engine.EngineException e;
		
	


		private void Page_Load(object sender, System.EventArgs e)
		{
			string str_AbsPath = "";
            string str_ReportName = "";
			bl_isPostBack=IsPostBack;
			//btnPrint.Attributes.Add("onclick", "IllustrationPrint();");

			if (bl_isPostBack==false)
			{
				divExp.Visible=false;
				//btnOK.Visible=true;
				lblError.Visible=false;
				ddLExp.Items.Add(new ListItem("PDF","pdf"));
				//ddLExp.Items.Add(new ListItem("Word","doc"));
				//ddLExp.Items.Add(new ListItem("Excel","xls"));
			}


			if(strconn!=null)
			{
                bool flag = QueryParse();


				if(flag)
				{

					ParseConfig();	
				}
			}
			else
			{
				MessageScript.Text = "ConnError();";
					
			}
		

		}	
		
		private bool QueryParse()
		{
			string str_ReportName = "";
			string str_Param  = "";
			string[] paraarr ;
			string Val = "";
			string key = "";
			string[] strarr;
			string strkey = "";
			string level = "";
			string str_AbsPath = "";
			
			if(Request.QueryString.AllKeys.Length>0)
			{
				try
				{
					level = "1";
					str_ReportName = Request.QueryString["_RepName"];
					str_AbsPath = Server.MapPath(str_ReportName+".rpt");

					//str_AbsPath = AppDomain.CurrentDomain.BaseDirectory.Replace("/", @"\");
					//str_AbsPath = str_AbsPath+"/CrystalReports/"+str_ReportName+".rpt";

					doc.Load(str_AbsPath,OpenReportMethod.OpenReportByDefault);
					level = "2";
					try
					{
						level = "3";
						str_Param  = Request.QueryString["_ParamStr"];
						if(str_Param != null && str_Param != "")
						{
							paraarr = str_Param.Split(';');
						
							level = "4";
							for(int i=0;i<paraarr.Length-1;i++)
							{
								level = "5";
								strarr = paraarr[i].Split(',');
								strkey = strarr[0];
								if(strkey!=null && strkey.StartsWith("_q_c"))
								{
									key = strkey.Replace("_q_c","");
									Val = strarr[1];
								}
								else if(strkey!=null  && strkey.StartsWith("_q_d"))
								{
									key = strkey.Replace("_q_d","");
									Val = strarr[1];
								}
								else if(strkey!=null  && strkey.StartsWith("_q_n"))
								{
									key = strkey.Replace("_q_n","");
									Val = strarr[1];
								}
								if(strkey!=null  && !strkey.StartsWith("NVC"))
								{

									doc.SetParameterValue(key,Val);
								}
								level = "6";
								if(strkey!=null  && strkey.StartsWith("NVC"))
								{
									key = strkey.Replace("NVC","");
									Val = strarr[1];
								}

								level = "7";
								if(SessionObject.GetString("PRINT_FLAG")=="Y" && SessionObject.GetString("PRINT_FLAG")!=null && SessionObject.GetString("PRINT_FLAG")!="")
								{
									
									level = "8";
									if(Val.StartsWith("'")&& Val!=null && Val!="")
									{
										Val = Val.Substring(Val.IndexOf("'")+1,(Val.LastIndexOf("'"))-(Val.IndexOf("'")+1));
									
									}
									NVC.Add(key,Val);
								}
							}
						}
					}

					catch
					{
						MessageScript.Text = "ParamError();";
						return false;
					}
				}
				catch
				{
					MessageScript.Text = "ReportError2('"+str_AbsPath+"', '"+str_Param+"', '"+level+"');";
					return false;
				}
			}
			else
			{
				MessageScript.Text = "QueryError();";
				return false;
			}
			return true;

		}
		
		private void ParseConfig()
		{
			string UserID = "";
			string Password = "";
			string DataSource="";
			string InitialCatalog="";
			string[] conarr;
			string[] arrusr;
			string[] arrpass;
			string[] arrDS;
			string[] arrIC;
			bool Flag = false;

			//				TableLogOnInfo tl = new TableLogOnInfo();
			conarr = strconn.Split(';');
				

			for(int i=0;i<conarr.Length;i++)
			{
				if(conarr[i]!=null  && conarr[i].StartsWith("User ID"))
				{
					arrusr = conarr[i].Split('=');
					UserID = arrusr[1];
				}
				else if(conarr[i]!=null  && conarr[i].StartsWith("Password"))
				{
					arrpass = conarr[i].Split('=');
					Password = arrpass[1];
				}
				else if(conarr[i]!=null  && conarr[i].StartsWith("Data Source"))
				{
					arrDS = conarr[i].Split('=');
					DataSource = arrDS[1];
				}
				else if(conarr[i]!=null  && conarr[i].StartsWith("Initial Catalog"))
				{
					arrIC = conarr[i].Split('=');
					InitialCatalog = arrIC[1];
				}
			}
			//doc.DataSourceConnections[0].SetConnection(DataSource,InitialCatalog,UserID,Password);
			//doc.DataSourceConnections[0].SetConnection(DataSource,InitialCatalog,UserID,Password);
			//doc.DataSourceConnections[0].SetLogon(UserID,Password);  
			//doc.SetDatabaseLogon(null,null,null,null);
//            doc.SetDatabaseLogon("SLBANCAPRD", "SLBANCAPRD", "O12C", InitialCatalog);
            doc.SetDatabaseLogon(UserID, Password, DataSource, InitialCatalog);
			//doc.SetDatabaseLogon(UserID,"",DataSource,InitialCatalog);
			//doc.SetDatabaseLogon(UserID,Password);
			//doc.PrintOptions.PaperSource  =  (CrystalDecisions.Shared.PaperSource)117;
			//doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.Paper11x17;
			//doc.Database.Tables[1].SetDataSource(
			//System.Drawing.Printing.PageSettings = 
 
			//doc.PrintOptions.PaperSize  =  CrystalDecisions.Shared.PaperSize.
			cinfo = new ConnectionInfo();
			cinfo.ServerName = DataSource;
			cinfo.DatabaseName = InitialCatalog;
			cinfo.UserID = UserID;
			cinfo.Password = Password;
			foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in doc.Database.Tables) 
			{
				tl = myTable.LogOnInfo ;
				tl.ConnectionInfo = cinfo;
				myTable.ApplyLogOnInfo(tl) ;

				//myTable.Location = cinfo.DatabaseName+".dbo."+myTable.Location.Substring(myTable.Location.LastIndexOf(".")+1);
				//	myTable.Location = cinfo.DatabaseName+".dbo."+myTable.Name;
				//doc.VerifyDatabase();
				Flag = myTable.TestConnectivity();
			}

			if(Flag)
			{

				//lnph.NPH_CODE, lnu1.NU1_LIFE, gotohell
				//customized code snippet in order to accomodate Arabic characterset fields manually
				
                rowset rs_ARABICNAME = DB.executeQuery("select lnph.NPH_FULLNAMEARABIC from lnu1_underwriti lnu1, lnph_pholder lnph where lnph.NPH_CODE=lnu1.NPH_CODE and lnu1.np1_proposal='"+Session["NP1_PROPOSAL"]+"' and lnu1.NU1_LIFE='F'");

				if (rs_ARABICNAME.next())
				{
					TextObject field;
					try
					{
                        field = (TextObject)doc.ReportDefinition.ReportObjects["NPH_FULLNAMEARABIC"];
						field.Text = "";

						string FULLNAMEARABIC= null; // =System.Text.Encoding.UTF8.GetString(Convert.FromBase64String((rs_ARABICNAME.getObject("NPH_FULLNAMEARABIC")==null?"":rs_ARABICNAME.getString("NPH_FULLNAMEARABIC"))));

						field.Text=Convert.ToString(FULLNAMEARABIC);
					}
					catch(Exception e)
					{
						//goto: hell
					}
				}

				//Session["NP1_PROPOSAL"]


				lblProposal.Text=Session["NP1_PROPOSAL"].ToString();
                doc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Crystal.pdf");

                Response.End();

				//Crv.ReportSource = doc;

				/************ Do not show report in Crystal Report but in PDF ****************/
				//Crv.ReportSource = doc;
                
				//Export in PDF
                /*
                MemoryStream oStream = new MemoryStream(); // using System.IO  
                oStream = (MemoryStream)doc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();  */
                
                /*
				System.IO.MemoryStream oStream = (MemoryStream)doc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
             	Response.ContentType = "application/pdf"; 
				Response.BinaryWrite(oStream.ToArray());
				*/
                 /************ Do not show report in Crystal Report but in PDF - end ****************/


			}
			else
			{
				MessageScript.Text = "DBError();";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{ 
			this.Crv.Init += new System.EventHandler(this.Crv_Init);
			//this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
			//this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.Unload += new System.EventHandler(this.Page_Unload);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Crv_Init(object sender, System.EventArgs e)
		{
		
		}
		private void Page_Unload(object sender, System.EventArgs e)
		{
			Page.Dispose();
			cinfo = null;
			tl = null;
			doc.Dispose();
			Crv.Dispose();
		}
        /*
		private void btnExp_Click(object sender, System.EventArgs e)
		{
			if (divExp.Visible==false)
			{
				divExp.Visible=true;
				btnOK.Visible=true;
			}
			else
			{
				divExp.Visible=false;
				btnOK.Visible=false;
			}
		}*/

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			MemoryStream oStream;

			Response.Clear();
			Response.Buffer= true;
            
            ExportFormatType formatType = ExportFormatType.NoFormat;
            if (ddLExp.SelectedValue == "pdf")
            {
                /*
                formatType = ExportFormatType.PortableDocFormat;
                doc.ExportToHttpResponse(formatType, Response, true, "Proposal" );
                Response.ContentType = "application/pdf"; 
                Response.End();
                */

                doc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Crystal.pdf");
                
                //Response.End();
                //SessionObject.Get("NP1_PROPOSAL")
            }

 /*
			if (ddLExp.SelectedValue=="pdf")
			{
				oStream = (MemoryStream)
					doc.ExportToStream(
					CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
				Response.ContentType = "application/pdf"; 
				Response.BinaryWrite(oStream.ToArray());
			}
			else if (ddLExp.SelectedValue=="doc")
			{
				oStream = (MemoryStream)
					doc.ExportToStream(
					CrystalDecisions.Shared.ExportFormatType.WordForWindows);
				Response.ContentType = "application/msword";
				Response.BinaryWrite(oStream.ToArray());
			}
			else if (ddLExp.SelectedValue=="xls")
			{
				oStream = (MemoryStream)
					doc.ExportToStream(
					CrystalDecisions.Shared.ExportFormatType.Excel);
				Response.ContentType = "application/vnd.ms-excel";
				Response.BinaryWrite(oStream.ToArray());
			}*/
			Response.End();
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
//					btnExp.Visible = false;
//					divExp.Visible=false;
//					btnOK.Visible=false;
//					btnPrint.Visible = false;
//					Crv.DisplayToolbar = false;
//					Crv.SeparatePages = false;
//					Crv.DisplayGroupTree = false;
					
//					Response.Write("<script>window.print();</script>");
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible=true;
			}
			finally
			{
//				btnPrint.Visible = true;
//				btnExp.Visible = true;
//				divExp.Visible=true;
//				btnOK.Visible=true;
				lblError.Visible=false;
//				Crv.DisplayToolbar = true;
			}
		}		
	
	}
}
