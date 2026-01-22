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


using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Presentation;
using shsm;

using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;

using System.Data.OleDb;

using System.Web.Configuration;
using System.Configuration;

namespace Aceins.Presentation
{
	/// <summary>
	/// Summary description for VerticalGrider.
	/// </summary>
	public partial class VerticalGrider : System.Web.UI.Page
	{
		private OleDbConnection myconn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DBConn();
			BindData();
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




		private void BindData()
		{
			DataSet ds = this.PartialWithdrawals(); // Some DataSet
			DataSet new_ds = FlipDataSet(ds); // Flip the DataSet
			DataView my_DataView = new_ds.Tables[0].DefaultView;
			this.DataGrid.DataSource = my_DataView;
			this.DataGrid.DataBind();
		}


		public DataSet FlipDataSet(DataSet my_DataSet)	
		{		
			DataSet ds = new DataSet();		
			foreach(DataTable dt in my_DataSet.Tables)		
			{			
				DataTable table = new DataTable();			
				for(int i = 0; i <= dt.Rows.Count; i++)			
				{				
					table.Columns.Add(Convert.ToString(i));			
				}			
				DataRow r = null;			
				for(int k = 0; k < dt.Columns.Count; k++)			
				{				
					r = table.NewRow();				
					r[0] = dt.Columns[k].ToString();				
					for(int j = 1; j <= dt.Rows.Count; j++)					
						r[j] = dt.Rows[j - 1][k];				
					table.Rows.Add(r);			
				}			
				ds.Tables.Add(table);		
			}		
			return ds;	
		}

		public DataSet PartialWithdrawals()
		{
			//string StrQry="SELECT * FROM LNPW_PARTIALWITHDRAWAL order by np1_proposal, npw_year";
			string StrQry="SELECT * FROM LNPW_PARTIALWITHDRAWAL ";
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

	}

}
