using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;


namespace Bancassurance.Presentation
{
    public partial class ProposalDetails : System.Web.UI.Page
    {

        string Sql, ProposalNo;


        DataTable dt = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        
       
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ProposalNo = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(ProposalNo))
                {
                    GetProposalInfo();
                    // Example: use it to load data
                    //   Label14.Text = "You clicked record: " + recordId;

                    // Or query your database
                    // LoadDetails(recordId);
                }
            }
        }

        #region "ImranCode"
        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;
        }

        public DataTable Getdata(string sql)
        {

           
            cmd.Connection = GetConn();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Reset();
            dt.Clear();

            da.Fill(dt);
           
            cmd.Connection.Close();

            return dt;


        }

        public void GetProposalInfo()
        {
            Sql= "Select * from VUE_Proposal_Info a where a.proposal='" + ProposalNo + "' AND ROWNUM = 1";
            dt = Getdata(Sql);

            if (dt.Rows.Count > 0)
            {
                dvSingleRecord.DataSource = dt; ;
                dvSingleRecord.DataBind();
            }
            else
            { Response.Write("No Proposal information found"); }


        }
        #endregion

    }
}
