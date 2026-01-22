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
using System.Data.SqlClient;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;

namespace Insurance.Presentation
{
	/// <summary>
	/// Summary description for PersonSelectionLOV.
	/// </summary>
	public partial class PersonSelectionLOV : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string dsn = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			//SqlConnection cnn = new SqlConnection(dsn); 
			OleDbConnection cnn = new OleDbConnection( dsn );

			/*Template Query
			 * SELECT A.NP1_PROPOSAL, NPH_FULLNAME, NPH_BIRTHDATE, NPH_INSUREDTYPE, case when NPH_SEX = 'M' then 'Male' else 'F' end NPH_SEX, NPH_CODE,NPH_LIFE 
				FROM LNPH_PHOLDER ,  (select NP1_PROPOSAL from lnp1_policymastr where CCN_CTRYCD='586' and NP1_CHANNEL='01' and NP1_CHANNELDETAIL = '01') A
				WHERE (NPH_CODE, NPH_LIFE) = (SELECT NPH_CODE, NPH_LIFE FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = A.NP1_PROPOSAL AND NPH_LIFE = 'D' AND NU1_LIFE = 'F')
				order by 3, 1*/
			
			String query =  "SELECT NPH_CODE, NPH_LIFE, NPH_FULLNAME, NPH_FULLNAMEARABIC, NPH_BIRTHDATE, NPH.CCL_CATEGORYCD, NPH_INSUREDTYPE, NPH_LIFE, NPH_SEX, NPH_TITLE, NPH_BIRTHDATE, NPH.COP_OCCUPATICD, COP_DESCR "
							+ " FROM LNPH_PHOLDER NPH,  LCOP_OCCUPATION COP  "
							+ " WHERE NPH.COP_OCCUPATICD=COP.COP_OCCUPATICD ";

			query = EnvHelper.Parse (query);

			
			//SqlDataAdapter da = new SqlDataAdapter(query, cnn); 
			OleDbDataAdapter da = new OleDbDataAdapter(query, cnn);

			DataSet ds = new DataSet(); 
			da.Fill(ds, "LNPH_PHOLDER"); 
			dgProposalLOV.DataSource = ds.Tables["LNPH_PHOLDER"];
			dgProposalLOV.DataBind();

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
			this.dgProposalLOV.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgProposalLOV_ItemCommand);
			this.dgProposalLOV.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgProposalLOV_PageIndexChanged);

		}
		#endregion

		private void dgProposalLOV_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgProposalLOV.CurrentPageIndex = e.NewPageIndex;
			dgProposalLOV.DataBind();
		}

		private void dgProposalLOV_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string src = ((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).CommandName;
			if ( src.Equals("Select") )
			{
				if ((""+Session["opener"]).Equals("F"))
				{
					Session.Add("NPH_CODE", e.Item.Cells[1].Text);
					Session.Add("NPH_LIFE", e.Item.Cells[2].Text);
				}
				else  
				{
					Session.Add("NPH_CODE_s", e.Item.Cells[1].Text);
					Session.Add("NPH_LIFE_s", e.Item.Cells[2].Text);
				}

				Session.Add("NPH_FULLNAME", e.Item.Cells[3].Text);
				Session.Add("NPH_FULLNAMEARABIC", e.Item.Cells[4].Text);
				Session.Add("COP_OCCUPATICD", e.Item.Cells[5].Text);
				Session.Add("CCL_CATEGORYCD", e.Item.Cells[6].Text);
				Session.Add("NPH_INSUREDTYPE", e.Item.Cells[7].Text);
				Session.Add("NPH_TITLE", e.Item.Cells[8].Text);

				Session.Add("NPH_SEX", e.Item.Cells[9].Text);
				Session.Add("NPH_BIRTHDATE", e.Item.Cells[10].Text);
				Session.Add("flg_SELECETD", "Y");



				Response.Write("<script language='Javascript'>");
				Response.Write("window.opener.location = window.opener.location;");
				Response.Write("window.close();");
				Response.Write("</script>");
			}
		}
	}
}
