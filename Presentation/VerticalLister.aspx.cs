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
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;

namespace Aceins.Presentation
{
	/// <summary>
	/// Summary description for VerticalLister.
	/// </summary>
	public partial class VerticalLister : System.Web.UI.Page
	{
		private OleDbConnection myconn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if(!IsPostBack)
			{
				DBConn();
				DataSet ds = PartialWithdrawals();
				DataTable dt = ds.Tables["LNPW_PARTIALWITHDRAWAL"];

				
				DataRow r = null;			

				while (dt.Rows.Count < 7)
				{
					r = dt.NewRow();				
					r[0] = dt.Rows[0][0];				
					//for(int j = 1; j <= dt.Rows.Count; j++)					
						//r[j] = dt.Rows[j - 1][k];				
					dt.Rows.Add(r);
				}



				//DataList1.DataSource = ds.Tables["LNPW_PARTIALWITHDRAWAL"].DefaultView;
				DataList1.DataSource = dt.DefaultView;
				DataList1.DataBind();
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
			this.DataList1.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.DataList1_ItemDataBound);

		}
		#endregion



		public DataSet PartialWithdrawals()
		{
			//string StrQry="SELECT * FROM LNPW_PARTIALWITHDRAWAL order by np1_proposal, npw_year";
			string StrQry="SELECT * FROM LNPW_PARTIALWITHDRAWAL where NP1_PROPOSAL='R/07/0010042'";
			OleDbDataAdapter myda = new OleDbDataAdapter(StrQry, myconn);
			DataSet ds = new DataSet();
			myda.Fill (ds,"LNPW_PARTIALWITHDRAWAL");



		
			
			//myconn.Close();
			return ds;
		}


		

		public void DBConn()
		{
			string strConn=ConfigurationSettings.AppSettings["DSN"];
			myconn = new OleDbConnection(strConn);
			myconn.Open();
		}

		private void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{

			string abc = "";

		
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			string abc = "";
			Response.Write("<script language=javascript>this.location=this.location;</script>");

		
		}


	}
}
