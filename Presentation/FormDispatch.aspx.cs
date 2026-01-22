using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop;
//using Microsoft.Office.Interop.Excel;
using System.Configuration;
using SHMA.Enterprise.Presentation;
using System.Collections;
//using OfficeOpenXml;
using System.IO;






namespace Bancassurance.Presentation
{
    public partial class FormDispatch : System.Web.UI.Page
    {
        string Sql, Msg,Path;
        Excel_Upload Obj_Excel_Upload = new Excel_Upload();
        DataTable Tissues = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand CmdDML = new OleDbCommand();

        Hashtable MyVal = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (SessionObject.Get("s_USE_TYPE").ToString() == "A") // Admin Login Check
                {
                    errorMsg.Text = "";
                    PnlDispatch.Visible = true;

                }
                else
                {
                    
                        PnlDispatch.Visible = false;
                    errorMsg.Text = "For this page, you are not authorised.";

                    }
                }
                }



                protected void btnUploadNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    lblFileNotSelectMsg.Text = "";
                    gvDummy.Visible = true;
                    btnSave.Visible = true;
                    lblConsingmentAlready.Text = "";
                    gvConsingAlready.Visible = false;




                   // Msg = DML("truncate table TBL_DISPATCH_UPLOAD_DUMMY");// Delete all rows from dummy table 

                    Msg = DML("truncate table LNP1_DISPATCH_INFO_TEMP");// Delete all rows from dummy table 
                    string ConStr = "";
                    Path = Server.MapPath(FileUpload1.FileName);
                   // Label1.Text = Path;
                    FileUpload1.SaveAs(Path);

                    
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";



                    // Obj_Excel_Upload.GridView1 = this.GridView1; Obj_Excel_Upload.fuDocument = this.FileUpload1; Tissues = Obj_Excel_Upload.SKU_Load("SELECT * From [Sheet1$];"); Session.Add("Tissues", Tissues);

                    string query = "SELECT * FROM [Sheet1$]";
                   
                    OleDbConnection conn = new OleDbConnection(ConStr);
                    
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                   
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                   
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                   
                    da.Fill(ds);
                    
                    GridView1.DataSource = ds.Tables[0];
                    
                    GridView1.DataBind();
                    
                    conn.Close();

                    // Extract last four characters from Login Id as Branch Code

                    string originalString = SessionObject.Get("s_USE_USERID").ToString();

                   string BranchCode= originalString.Substring(originalString.Length - 4);

                    ///////////////////////////////////////////////////////////

                    for (int row = 0; row <= GridView1.Rows.Count - 1; row++)
                {

                        // With Origin city
//                    Sql = "Insert into LNP1_DISPATCH_INFO_TEMP(NP1_CONSIGNMENTNO,NP1_CONSIGNMENT_NAME,NP1_PROPOSAL,NP1_POLICYNO,NP1_CONS_ADDRESS,NP1_DOCUMENT_TYPE,NP1_PICKUP_DATE,NP1_ORIGIN_CITY,NP1_DEST_CITY,USE_USERID,PBK_BANKCODE,PBB_BRANCHCODE,NP1_DATE)" +

////NP1_DISPATCH_STATUS           VARCHAR2(10)

//                         " values('" + GridView1.Rows[row].Cells[0].Text.ToString() + "'," +
//                         "'" + GridView1.Rows[row].Cells[1].Text.ToString() + "','" + GridView1.Rows[row].Cells[2].Text.ToString() +
//                         "','" + GridView1.Rows[row].Cells[3].Text.ToString() + "','" + GridView1.Rows[row].Cells[4].Text.ToString() +
//                          "','" + GridView1.Rows[row].Cells[5].Text.ToString() + "',to_char(TO_DATE('" + GridView1.Rows[row].Cells[6].Text.ToString() + "', 'DD/MM/YYYY HH24:MI:SS'))" +
//                          ",'" + GridView1.Rows[row].Cells[7].Text.ToString() +
//                          "','" + GridView1.Rows[row].Cells[8].Text.ToString() + "','" + SessionObject.Get("s_USE_USERID").ToString() + "','" + SessionObject.Get("s_CCD_CODE").ToString() +
//                          "','" + BranchCode + "',sysdate)";




                        // Without Origin City
                        Sql = "Insert into LNP1_DISPATCH_INFO_TEMP(NP1_CONSIGNMENTNO,NP1_CONSIGNMENT_NAME,NP1_PROPOSAL,NP1_POLICYNO,NP1_CONS_ADDRESS,NP1_DOCUMENT_TYPE,NP1_PICKUP_DATE,USE_USERID,PBK_BANKCODE,PBB_BRANCHCODE,NP1_DATE)" +

                       //NP1_DISPATCH_STATUS           VARCHAR2(10)

                       " values('" + GridView1.Rows[row].Cells[0].Text.ToString() + "'," +
                       "'" + GridView1.Rows[row].Cells[1].Text.ToString() + "','" + GridView1.Rows[row].Cells[2].Text.ToString() +
                       "','" + GridView1.Rows[row].Cells[3].Text.ToString() + "','" + GridView1.Rows[row].Cells[4].Text.ToString() +
                        "','" + GridView1.Rows[row].Cells[5].Text.ToString() + "',to_char(TO_DATE('" + GridView1.Rows[row].Cells[6].Text.ToString() + "', 'DD/MM/YYYY HH24:MI:SS'))" +
                        ",'" +  SessionObject.Get("s_USE_USERID").ToString() + "','" + SessionObject.Get("s_CCD_CODE").ToString() +
                        "','" + BranchCode + "',sysdate)";


                        //SessionObject.Set("s_USE_USERID"
                        Label5.Text = Sql;

                    Msg = DML(Sql);

                    if (Msg == "done")
                    { Label5.Text = ""; }

                    else
                    {
                        Label5.Text = Msg;

                    }

                    }

                    btnSave.Visible = true;
                    gvDummy.Visible = true;

                    gvFinal.Visible = false;
                    gvNotFinal.Visible = false;

                        lblFinalMsg.Text = "";
                        lblNotUpdate.Text = "";
                        lblErrMsg.Text = "";
                    DisplayFromDummy(); // done
                    //  System.IO.File.Delete(FileUpload1.FileName);
                    System.IO.File.Delete(Path);



                }//
               else
                {

                    lblFileNotSelectMsg.Text = "Please Select Excel File";
                }
            }
            catch(Exception)
            {

                lblErrMsg.Text = "1-)Please use Excel 2003 format XLS \n 2-)Excel sheet columns must be as per prescribe format";
            }
            //
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
            Msg = DML_by_pr_withoutParameter("CALL_DISPATCH_UPLOAD");
            if (Msg == "done")
            {

                lblErrMsg.Text = "Operation completed";

                gvFinal.Visible = true;
               // gvNotFinal.Visible = true;
                gvConsingAlready.Visible = true;
                gvDummy.Visible = false;
                btnSave.Visible = false;

                   

                
              //  lblNotUpdate.Text = "Below Policies are not found in system";
               

                DisplayFinalUpload(); // done
                DisplayNotUpload();  // done
                DisplayConsingAlready(); //done



            }
            else
            {
                    gvFinal.Visible = true;
                    gvNotFinal.Visible = true;
                    gvConsingAlready.Visible = true;

                    lblErrMsg.Text = Msg;
                    
                }

        }
        catch(Exception Ex)
            {
               // lblErrMsg.Text = Ex.Message;
                //  lblErrMsg.Text = "Consingment No already available";
                Console.WriteLine("Handled exception: " + Ex.Message);

            }
            finally
                {

             //   Label8.Text = "Done after exception";
               

            }
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        ////    Application excelApplication = new Application();

        ////    Workbook workbook = excelApplication.Workbooks.Open(FileUpload1.FileName);
        ////   // Worksheet worksheet = workbook.Worksheets["Sheet1"];



        ////    for (int row = 2; row <= worksheet.UsedRange.Rows.Count; row++)
        ////    {
        ////       / string column1Value = worksheet.Cells[row, 1].Value.ToString();

        ////        string column2Value = worksheet.Cells[row, 2].Value.ToString();

        ////        Sql = "Insert into tbl_Excel_Upload(Emp_Id,Emp_Name)" +
        ////           " values(" + column1Value.ToString() + "," +
        ////           "'" + column2Value.ToString() + "')";


        ////        Msg = DML(Sql);

        ////        if (Msg == "done")
        ////        { Label5.Text = "Done"; }

        ////        else
        ////        { Label5.Text = Msg; }

        ////    }

        ////    workbook.Close();
        ////    excelApplication.Quit();
        //}

        #region MyCode

        public string DML(string sql)
        {
            try
            {
                CmdDML.CommandText = sql;
                CmdDML.Connection = GetConn();
                CmdDML.Connection.Open();
                CmdDML.ExecuteNonQuery();
                CmdDML.Connection.Close();

                return "done";
            }

            catch (Exception ex)
            {

                 CmdDML.Connection.Close();
                return ex.Message;

            }

        }

        public DataTable GetdataOraOledb(string sql)
        {
            
          //  cmd.Connection = conOra; old
            cmd.Connection= GetConn();
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

        public string DML_by_pro_Parameter(string procedureName ,Hashtable Parameters)
        {

            {

                try
                {
                   // OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
                   // conOra.Open();

                   // OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Connection = conOra;
                    cmd.Connection = GetConn();
                   
                   // cmd.Connection.Open();
                    int loopCounter = 0;
                    ICollection ParamKeys = Parameters.Keys;
                    foreach (object key in ParamKeys)
                    {

                        //cmd.Parameters.Add(new SqlParameter(key.ToString(), Parameters[key.ToString()]));
                        cmd.Parameters.Add(new OleDbParameter(key.ToString(), Parameters[key.ToString()]));


                    }

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Parameters.Clear();
                    return "done";
                }

                catch (Exception ex)
                {
                    cmd.Parameters.Clear();
                    cmd.Connection.Close();
                    return ex.Message;
                }


            }


        }

        public string DML_by_pr_withoutParameter(string procedureName)
        {

            {

                try
                {
                    // OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
                    // conOra.Open();

                    // OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Connection = conOra;
                    cmd.Connection = GetConn();

                    // cmd.Connection.Open();
                    int loopCounter = 0;
                    //ICollection ParamKeys = Parameters.Keys;
                    //foreach (object key in ParamKeys)
                    //{

                    //    //cmd.Parameters.Add(new SqlParameter(key.ToString(), Parameters[key.ToString()]));
                    //    cmd.Parameters.Add(new OleDbParameter(key.ToString(), Parameters[key.ToString()]));


                    //}

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Parameters.Clear();
                    return "done";
                }

                catch (Exception ex)
                {
                    cmd.Parameters.Clear();
                    cmd.Connection.Close();
                    return ex.Message;
                }


            }


        }
        void DisplayFromDummy()
        {
            try
            {
                Sql = "Select A.NP1_CONSIGNMENTNO,A.NP1_CONSIGNMENT_NAME,A.NP1_PROPOSAL,A.NP1_POLICYNO,A.NP1_CONS_ADDRESS,A.NP1_DOCUMENT_TYPE,A.NP1_PICKUP_DATE from LNP1_DISPATCH_INFO_TEMP A " +
                       " WHERE A.USE_USERID = '" + SessionObject.Get("s_USE_USERID").ToString() + "'";

                gvDummy.DataSource = GetdataOraOledb(Sql);
                gvDummy.DataBind();

                //Insert into LNP1_DISPATCH_INFO_TEMP(,,,,,,,NP1_ORIGIN_CITY,NP1_DEST_CITY,USE_USERID,PBK_BANKCODE,PBB_BRANCHCODE,NP1_DATE)" +




            }
            catch (Exception Ex)

            {
                lblErrMsg.Text = "No data available";
                lblErrMsg.Text = Ex.Message;
            }
        }


        void DisplayFinalUpload()
        {
            try
            {
                DataTable dt_final = new DataTable();

                Sql = "Select A.NP1_CONSIGNMENTNO,A.NP1_CONSIGNMENT_NAME,A.NP1_PROPOSAL,A.NP1_POLICYNO,A.NP1_CONS_ADDRESS,A.NP1_DOCUMENT_TYPE,A.NP1_PICKUP_DATE from LNP1_DISPATCH_INFO_TEMP A " +
                     " WHERE A.NP1_DISPATCH_STATUS = 'Done'";

                dt_final = GetdataOraOledb(Sql);

                if (dt_final.Rows.Count > 0)
                {
                    gvFinal.Visible = true;
                    gvFinal.DataSource = dt_final;
                    gvFinal.DataBind();
                    lblFinalMsg.Text = "Below Consignment Number is successfully uploaded.";

                }

                else
                {

                    gvFinal.Visible = false;
                    lblFinalMsg.Text = "";
                }

            }
            catch (Exception Ex)

            {
                lblErrMsg.Text = "No data available";
                lblErrMsg.Text = Ex.Message;
            }
        }

        void DisplayNotUpload()
        {
            try
            {
                DataTable dt_not_Found = new DataTable();
                Sql = "Select A.NP1_CONSIGNMENTNO,A.NP1_CONSIGNMENT_NAME,A.NP1_PROPOSAL,A.NP1_POLICYNO,A.NP1_CONS_ADDRESS,A.NP1_DOCUMENT_TYPE,A.NP1_PICKUP_DATE from LNP1_DISPATCH_INFO_TEMP A " +
                    " WHERE A.NP1_DISPATCH_STATUS = 'Not Done'";

                dt_not_Found= GetdataOraOledb(Sql);
                if(dt_not_Found.Rows.Count>0)
                {
                    gvNotFinal.Visible = true;
                     gvNotFinal.DataSource = dt_not_Found;
                    gvNotFinal.DataBind();
                    lblNotUpdate.Text = "Below Policies are not found in system";
                }

                else
                {
                    gvNotFinal.Visible = false;
                    lblNotUpdate.Text = "";


                }
                
               


            }
            catch (Exception Ex)

            {
                lblErrMsg.Text = "No data available";
                lblErrMsg.Text = Ex.Message;
            }
        }

        void DisplayConsingAlready()
        {
            try
            {
                DataTable dt_Already = new DataTable();

                Sql = "Select A.NP1_CONSIGNMENTNO,A.NP1_CONSIGNMENT_NAME,A.NP1_PROPOSAL,A.NP1_POLICYNO,A.NP1_CONS_ADDRESS,A.NP1_DOCUMENT_TYPE,A.NP1_PICKUP_DATE from LNP1_DISPATCH_INFO_TEMP A " +
                     " WHERE A.NP1_DISPATCH_STATUS = 'AL'";

                dt_Already= GetdataOraOledb(Sql);

                if(dt_Already.Rows.Count>0)
                {
                    gvConsingAlready.Visible = true;
                    gvConsingAlready.DataSource = dt_Already;
                   gvConsingAlready.DataBind();
                    lblConsingmentAlready.Text = "Below Consignment Data is already uploaded.";
                    }
                else
                {
                    gvConsingAlready.Visible = false;
                    lblConsingmentAlready.Text = "";


                }
               




            }
            catch (Exception Ex)

            {
                lblErrMsg.Text = "No data available";
                lblErrMsg.Text = Ex.Message;
            }
        }
        protected void gvDummy_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvDummy.PageIndex = e.NewPageIndex;
            DisplayFromDummy(); 

        }

        protected void gvFinal_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvFinal.PageIndex = e.NewPageIndex;
            DisplayFinalUpload();

        }

        protected void gvNotFinal_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvNotFinal.PageIndex = e.NewPageIndex;
            DisplayNotUpload();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
          //  ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string Fname;
            Fname = FileUpload1.FileName;
            // string filePath = Path.GetFileName(FileUpload1.FileName);
            String Path = HttpContext.Current.Request.PhysicalApplicationPath + FileUpload1.FileName;

           //string fileNameWithPath="@" + Path;
           // string fileNameWithPath = "@D:/Imran_App/Test_new.xlsx";
           // string fileNameWithPath=   @"C:\Path\To\Your\File.xlsx";

            // string filePath = @"D:/Imran_App/Test_new.xlsx";

           
            // forward slash \ normal /
            // Label5.Text = filePath;
            //Label6.Text = Path;


            //  string fileNameWithPath = FileUpload1.PostedFile.FileName;

            //using (var package = new ExcelPackage(new FileInfo(filePath)))
            //{
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            //    int rowCount = worksheet.Dimension.Rows;

            //    for (int row = 1; row <= rowCount; row++)
            //    {
            //        // Access cell values using the Cells property
            //        string cellValue1 = worksheet.Cells[row, 1].Text; // Column 1
            //        string cellValue2 = worksheet.Cells[row, 2].Text; // Column 2

            //        // Process the cell values as needed
            //        // ...
            //        Label5.Text = cellValue1;

            //    }
            //}
        }

        protected void btnUploadStatus_Click(object sender, EventArgs e)
        {
            PnlDispatchStatus.Visible = true;
            PnlUploadDispatchInfo.Visible = false;
        }

        protected void btnUploadStatusExcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (FileUpload2.HasFile)
                {
                  lblStatusFileNotFound.Text = "";
                    lblStatusMsg.Text = "";
                    gvStatusExcel.Visible = true;
                    btnStatusFinal.Visible = true;

                    Msg = DML("truncate table LNP1_DISPATCH_STATUS_TEMP");
                    
                    string ConStr = "";
                    Path = Server.MapPath(FileUpload2.FileName);
                    //Label1.Text = Path;
                    FileUpload2.SaveAs(Path);


                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                    string query = "SELECT * FROM [Sheet1$]";
                   
     

                    
                    OleDbConnection conn = new OleDbConnection(ConStr);
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    conn.Close();




                    // Obj_Excel_Upload.GridView1 = this.GridView1; Obj_Excel_Upload.fuDocument = this.FileUpload2; Tissues = Obj_Excel_Upload.SKU_Load("SELECT * From [Sheet1$];"); Session.Add("Tissues", Tissues);

                    for (int row = 0; row <= GridView1.Rows.Count - 1; row++)
                    {
                        DateTime DeliveryTime = DateTime.Parse(GridView1.Rows[row].Cells[5].Text);
                       // string DeliverTime = yourDateTime.ToString("hh:mm tt");

                        Sql = "INSERT INTO LNP1_DISPATCH_STATUS_TEMP(NP1_CONSIGNMENTNO,NP1_DELIVERY_DATE,NP1_ORIGIN_CITY,NP1_DEST_CITY,NP1_RECEIVE_BY,NP1_DELIVERY_TIME,NP1_DISPATCH_STATUS,NP1_REASON,NP1_ENTER_DATE,USE_USERID)" +

                             " values('" + GridView1.Rows[row].Cells[0].Text.ToString() + "'," +
                             "  to_char(TO_DATE('" + GridView1.Rows[row].Cells[1].Text.ToString() + "','DD/MM/YYYY HH24:MI:SS')), '" + GridView1.Rows[row].Cells[2].Text.ToString() +
                             "','" + GridView1.Rows[row].Cells[3].Text.ToString() + "','" + GridView1.Rows[row].Cells[4].Text.ToString() +
                             "','" + DeliveryTime.ToShortTimeString() + "','" + GridView1.Rows[row].Cells[6].Text.ToString() + "'," +
                              "'" + GridView1.Rows[row].Cells[7].Text.ToString() + "',sysdate,'" + SessionObject.Get("s_USE_USERID").ToString() + "')";

 
                        // lblStatusUpdated.Text = Sql;

                        Msg = DML(Sql);

                        if (Msg == "done")
                        { lblStatusUpdated.Text = "";

                            btnStatusFinal.Visible = true;
                            gvStatusExcel.Visible = true;
                           // gvStatusFinal.DataSource = null;
                          //  gvStatusFinal.DataBind();
                           // gvStatusNotFinal.DataSource = null;
                           // gvStatusNotFinal.DataBind();
                            lblStatusUpdated.Text = "";
                            lblStatusNotUpdate.Text = "";
                            lblStatusMsg.Text = "";
                            

                            
                        }

                        else
                        {
                            lblStatusUpdated.Text = Msg;

                        }

                        
                        DisplayStatusFromDummy();
                        System.IO.File.Delete(Path);

                    }

                }//
                else
                {

                    lblFileNotSelectMsg.Text = "Please Select Excel File";
                }
            }
            catch (Exception)
            {

                lblStatusMsg.Text = "Please use Excel 2003 file format like Test.XLS";
            }

        }

        private OleDbConnection GetConn()
        {
            OleDbConnection conOra = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["DSN"]);
            return conOra;


        }

        protected void btnStatusFinal_Click(object sender, EventArgs e)
        {
            try
            {
                Msg = DML_by_pr_withoutParameter("CALL_DISPATCH_STATUS_UPDATE");
                if (Msg == "done")
                {
                    lblStatusUpdated.Text = "Below Consingment status updated in system";
                    lblStatusNotUpdate.Text = "Below Consingment status not updated in system";
                    gvStatusFinal.Visible = true;
                    gvStatusNotFinal.Visible = true;
                    DisplayStatusUpdated(); //done
                    DisplayStatusNotUpdatedy(); //done




                }

                else
                {
                    lblStatusMsg.Text = Msg;

                }

            }

            catch(Exception Ex)
            {

                lblStatusMsg.Text = Ex.Message;
            }
            
        }

        // Dispatch Status update working

        void DisplayStatusFromDummy()
        {
            try
            {
                
                Sql = " Select A.NP1_CONSIGNMENTNO, A.NP1_DELIVERY_DATE, A.NP1_ORIGIN_CITY, A.NP1_DEST_CITY, A.NP1_RECEIVE_BY, A.NP1_DELIVERY_TIME, A.NP1_DISPATCH_STATUS, A.NP1_REASON, A.NP1_ENTER_DATE, A.USE_USERID " +
                    " FROM LNP1_DISPATCH_STATUS_TEMP A";

              gvStatusExcel.DataSource = GetdataOraOledb(Sql);
                gvStatusExcel.DataBind();

            }
            catch (Exception Ex)

            {
                lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
        }

        
          void DisplayStatusUpdated()
        {
            try
            {
                Sql = "Select P.NP1_CONSIGNMENTNO,P.NP1_DELIVERY_DATE,P.NP1_ORIGIN_CITY,P.NP1_DEST_CITY,P.NP1_RECEIVE_BY,P.NP1_DISPATCH_STATUS,P.NP1_STATUS " +
                       ",REPLACE(REPLACE(REGEXP_REPLACE(P.NP1_reason, '<[^>]*>|&nbsp;', ''),'&',''),Chr(13) || Chr(10),'') NP1_REASON" +
                       " FROM LNP1_DISPATCH_STATUS_TEMP P where P.NP1_STATUS = 'Done'";

                gvStatusFinal.DataSource = GetdataOraOledb(Sql);
                gvStatusFinal.DataBind();

            }
            catch (Exception Ex)

            {
                lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
        }

        protected void btnAddDispatchInfo_Click(object sender, EventArgs e)
        {
            PnlUploadDispatchInfo.Visible = true;
            lblFinalMsg.Text = "";
            lblNotUpdate.Text = "";
            lblErrMsg.Text = "";
            gvDummy.Visible = false;
            gvFinal.Visible = false;
            gvNotFinal.Visible = false;
            btnSave.Visible = false;

            PnlDispatchStatus.Visible = false;

            PnlUndispatch.Visible = false;
        }

        protected void btnUndispatched_Click(object sender, EventArgs e)
        {
            // string FilePath = Server.MapPath("~/Presentation/frmDispatchReport.aspx");

            //Response.Redirect("../Presentation/frmDispatchReport.aspx");
            // Response.Redirect(FilePath);

            //PnlDispatch.Visible = false;
            PnlDispatchStatus.Visible = false;
            PnlUploadDispatchInfo.Visible = false;
            PnlUndispatch.Visible = true;
            DisplayUnDispatchInfo();

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt_Retrive = (DataTable)Session["Data"];
            ExportToExcelHtml(dt_Retrive);

        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            DisplayUnDispatchInfo();
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
     " to_char(N.NP1_PICKUP_DATE,'DD-MON-YYYY') PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     " to_char(N.NP1_DELIVERY_DATE,'DD-MON-YYYY') DELIVERY_DATE," +
     
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where upper(N.NP1_DISPATCH_STATUS) like 'NOT%'" +
      " and N.NP1_PROPOSAL='" + txtProposal.Text.ToString() + "'";


                Tissues = GetdataOraOledb(Sql);

                Session["Data"] = Tissues;

                gvDispatchInfo.DataSource = Tissues;
                gvDispatchInfo.DataBind();

            }
            catch (Exception Ex)

            {
                // lblStatusMsg.Text = "No data available";
                lblUndispatchMsg.Text = Ex.Message;
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
     "   to_char(N.NP1_PICKUP_DATE,'DD-MON-YYYY') PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     "  to_char(N.NP1_DELIVERY_DATE,'DD-MON-YYYY') DELIVERY_DATE," +
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where upper(N.NP1_DISPATCH_STATUS) like 'NOT%'" +
      " and TRUNC(N.NP1_PICKUP_DATE) between TO_DATE('" + txtFrom.Text.ToString() + "','DD-MON-YYYY') and TO_DATE('" + txtTo.Text.ToString() + "','DD-MON-YYYY')";

                // lblStatusMsg.Text = Sql;

                Tissues = GetdataOraOledb(Sql);

                Session["Data"] = Tissues;

                gvDispatchInfo.DataSource = Tissues;
                gvDispatchInfo.DataBind();

            }
            catch (Exception Ex)

            {

                lblUndispatchMsg.Text = Ex.Message;
            }

        }

        protected void btnUpdateDispStatus_Click(object sender, EventArgs e)
        {
            PnlDispatchStatus.Visible = true;
            gvStatusExcel.Visible = false;
            lblFileNotSelectMsg.Text = "";
            lblStatusMsg.Text = "";
            lblStatusUpdated.Text = "";
            lblStatusNotUpdate.Text = "";
            btnStatusFinal.Visible = false;

            gvStatusFinal.Visible = false;
            gvStatusNotFinal.Visible = false;


            PnlUploadDispatchInfo.Visible = false;

            PnlUndispatch.Visible = false;
        }

        protected void gvDispatchInfo_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvDispatchInfo.PageIndex = e.NewPageIndex;
            DisplayDispatchInfo();

        }

        void DisplayStatusNotUpdatedy()
        {
            try
            {
              
                Sql = "Select P.NP1_CONSIGNMENTNO,P.NP1_DELIVERY_DATE,P.NP1_ORIGIN_CITY,P.NP1_DEST_CITY,P.NP1_RECEIVE_BY,P.NP1_DISPATCH_STATUS,P.NP1_STATUS, " +
                      "REPLACE(REPLACE(REGEXP_REPLACE(P.NP1_reason, '<[^>]*>|&nbsp;', ''),'&',''),Chr(13) || Chr(10),'') NP1_REASON" +
                       " FROM LNP1_DISPATCH_STATUS_TEMP P where P.NP1_STATUS = 'Not Done'";

               

                gvStatusNotFinal.DataSource = GetdataOraOledb(Sql);
                gvStatusNotFinal.DataBind();

            }
            catch (Exception Ex)

            {
                lblStatusMsg.Text = "No data available";
                lblStatusMsg.Text = Ex.Message;
            }
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
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where upper(N.NP1_DISPATCH_STATUS) like 'NOT%'";


                Tissues = GetdataOraOledb(Sql);

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
        #endregion

        // Undispatch work

        void DisplayUnDispatchInfo()
        {
            try
            {
                string Sql = " SELECT N.NP1_CONSIGNMENTNO CONSIGNMENTNO," +
     " N.NP1_CONSIGNMENT_NAME CONSIGNMENT_NAME," +
     "   N.NP1_PROPOSAL PROPOSALNO, " +
     "   N.NP1_POLICYNO POLICYNO, " +
     "   N.NP1_CONS_ADDRESS ADDRESS," +
     "  N.NP1_DOCUMENT_TYPE DOCUMENT_TYPE," +
     "   to_char(N.NP1_PICKUP_DATE,'DD-MON-YYYY') PICKUP_DATE," +
     "   N.NP1_ORIGIN_CITY ORIGIN_CITY," +
     "   N.NP1_DEST_CITY DEST_CITY," +
     
     "   to_char(N.NP1_DELIVERY_DATE,'DD-MON-YYYY') DELIVERY_DATE," +
     "   N.NP1_RECEIVE_BY RECEIVE_BY," +
     "   N.NP1_DELIVERY_TIME DELIVERY_TIME," +
     "   N.NP1_DISPATCH_STATUS DISPATCH_STATUS," +
      "  N.NP1_REASON REASON  FROM LNP1_DISPATCH_INFO N where upper(N.NP1_DISPATCH_STATUS) like 'NOT%'";


                Tissues = GetdataOraOledb(Sql);

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
            try
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
            catch(Exception)
            {
                lblUndispatchMsg.Text = "There is some issue found to export to Excel";
            }
            }
        
    }
}