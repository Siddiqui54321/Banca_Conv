using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.OleDb;
using Microsoft;
using System.Data.Sql;
using System.IO;
using System.Configuration;
//using Microsoft.Office.Interop.Excel;

namespace Bancassurance.Presentation
{
    public class Excel_Upload
    {
        DataTable dt_return;

        GridView _GridView1;
        FileUpload _fuDocument;
        public FileUpload fuDocument { get { return _fuDocument; } set { _fuDocument = value; } }

        public GridView GridView1 { get { return _GridView1; } set { _GridView1 = value; } }


        public DataTable SKU_Load(string select_query)
        {

            if (fuDocument.HasFile)
            {
                string FileName = Path.GetFileName(fuDocument.PostedFile.FileName);
                string Extension = Path.GetExtension(fuDocument.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["../upload/"];

                // string FilePath = HttpContext.Current.Server.MapPath(FolderPath + "App_Data/" + FileName);
                string FilePath = HttpContext.Current.Server.MapPath(FolderPath + FileName);
               
                fuDocument.SaveAs(FilePath);
                //     Import_To_Grid4SKU("SELECT * From [Additions$] t where t.NameofEmployee<>''",FilePath, Extension, "Yes");
                dt_return = Import_To_Grid4SKU(select_query, FilePath, Extension, "Yes");
            }

            return dt_return;
        }

        private DataTable Import_To_Grid4SKU(string sql_query, string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //string SheetName = dtExcelSchema.Rows[0][0].ToString(); // Edit by Imran
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            // cmdExcel.CommandText = "SELECT * From " + SheetName; 
            //  cmdExcel.CommandText = sql_query;
            //cmdExcel.CommandText = "SELECT * From [SQLResults$]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            return dt;
        }

    }
}