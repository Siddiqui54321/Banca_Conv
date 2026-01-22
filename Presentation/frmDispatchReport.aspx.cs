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
    public partial class frmDispatchReport : System.Web.UI.Page
    {
        string Sql, Msg, Path;
       
        DataTable Tissues = new DataTable();
       // DataTable dt = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    DisplayDispatchInfo();
                }
            }

            catch(Exception Ex)
            {

                lblStatusMsg.Text = Ex.Message;
            }

        }

        protected void gvDispatchInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDispatchInfo.PageIndex = e.NewPageIndex;
            DisplayDispatchInfo();
        }

        #region "Mycode"
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt_Retrive = (DataTable)Session["Data"];
            ExportToExcelHtml(dt_Retrive);

        }

        protected void btnFindProposal_Click(object sender, EventArgs e)
        {
            try
            {
                string Sql = " SELECT N.NP1_CONSIGNMENTNO CONSIGNMENTNO," +
     " N.NP1_CONSIGNMENT_NAME CONSIGNMENT_NAME," +
     "   N.NP1_PROPOSAL PROPOSALNO, " +
     "   N.NP1_POLICYNO POLICYNO, " +
     "   N.NP1_CONS_ADDRESS ADDRESS," +
     "  N.NP1_DOCUMENT_TYPE DOCUMENT_TYPE," +
     "   N.NP1_PICKUP_DATE PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     "   N.NP1_DELIVERY_DATE DELIVERY_DATE," +
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where N.NP1_DISPATCH_STATUS='Not Delivered'" +
      " and N.NP1_PROPOSAL='" + txtProposal.Text.ToString() + "'";


                Tissues = GetdataOraOledb(Sql);

                Session["Data"] = Tissues;
                
                gvDispatchInfo.DataSource = Tissues;
                gvDispatchInfo.DataBind();

            }
            catch (Exception Ex)

            {
                // lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
        }

        protected void btnFindByDate_Click(object sender, EventArgs e)
        {
            try
            {
                string Sql = " SELECT N.NP1_CONSIGNMENTNO CONSIGNMENTNO," +
     " N.NP1_CONSIGNMENT_NAME CONSIGNMENT_NAME," +
     "   N.NP1_PROPOSAL PROPOSALNO, " +
     "   N.NP1_POLICYNO POLICYNO, " +
     "   N.NP1_CONS_ADDRESS ADDRESS," +
     "  N.NP1_DOCUMENT_TYPE DOCUMENT_TYPE," +
     "   N.NP1_PICKUP_DATE PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     "   N.NP1_DELIVERY_DATE DELIVERY_DATE," +
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where N.NP1_DISPATCH_STATUS='Not Delivered'" +
      " and N.NP1_DATE between to_date('" + txtFrom.Text.ToString() + "','dd/mm/yyyy') and to_date('" + txtTo.Text.ToString() + "','dd/mm/yyyy')";

                // lblStatusMsg.Text = Sql;

                Tissues = GetdataOraOledb(Sql);

                Session["Data"] = Tissues;

                gvDispatchInfo.DataSource = Tissues;
                gvDispatchInfo.DataBind();

            }
            catch (Exception Ex)

            {
                
                lblStatusMsg.Text = Ex.Message;
            }

            //A.NP1_DATE between to_date('03/04/2024','dd/mm/yyyy') and to_date('05/04/2024','dd/mm/yyyy')

        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            DisplayDispatchInfo();
        }

        void DisplayDispatchInfo()
        {
            try
            {
                string Sql = " SELECT N.NP1_CONSIGNMENTNO CONSIGNMENTNO," +
     " N.NP1_CONSIGNMENT_NAME CONSIGNMENT_NAME," +
     "   N.NP1_PROPOSAL PROPOSALNO, " +
     "   N.NP1_POLICYNO POLICYNO, " +
     "   N.NP1_CONS_ADDRESS ADDRESS," +
     "  N.NP1_DOCUMENT_TYPE DOCUMENT_TYPE," +
     "   N.NP1_PICKUP_DATE PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     "   N.NP1_DELIVERY_DATE DELIVERY_DATE," +
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where N.NP1_DISPATCH_STATUS='Not Delivered'";


                Tissues= GetdataOraOledb(Sql);

                Session["Data"] = Tissues;


                // PnlDispatch.Visible = true;


                gvDispatchInfo.DataSource = Tissues;
                gvDispatchInfo.DataBind();

            }
            catch (Exception Ex)

            {
                // lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
        }

        public void ExportToExcelHtml(DataTable table)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<b>Un-dispatched Policy Documents List</b>");
            context.Response.Write("<BR>");
            context.Response.Write("<BR>");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;line-height:3.0pt;text-align:justif'> <TR>");

            //Begin Table
            //context.Response.Write("<table><tr>");

            //Write Header
            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write("<th>" + column.ColumnName + "</th>");
            }
            context.Response.Write("</tr>");

            //Write Data
            foreach (DataRow row in table.Rows)
            {
                context.Response.Write("<tr>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    context.Response.Write("<td>" + row[i].ToString().Replace(",", string.Empty) + "</td>");
                }
                context.Response.Write("</tr>");
            }

            //End Table
            context.Response.Write("</table>");

            context.Response.ContentType = "application/ms-excel";
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=Un-dispatched Proposal List.xls");
            context.Response.End();
        }



        #endregion
    }
}