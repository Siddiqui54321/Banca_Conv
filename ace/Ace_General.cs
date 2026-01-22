using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Configuration;
using SHAB.Business;
using System.Collections.Generic;


namespace ace
{
    public class Ace_General
    {
        static bool callComp = true;
        #region Business General Methods

        public static string transferProposalFromFileToPolicy(string proposal, string Status, string commDate)
        {
            //ProcedureAdapter cs = new ProcedureAdapter("TRANSFER_BAPROPOSAL");
            SessionObject.Set("PendingList", "0");
            //DB.BeginTransaction();
            string retVal = "DONE";
            OleDbCommand cmd = null;
            string commDate1 = Convert.ToDateTime(commDate).ToString("dd-MMM-yyyy");
            if (callComp)
            {
                UpdateCommencementDate(commDate, proposal);
                // throw new Exception ("test");
                try
                {
                    if (Status == "N")
                    {
                        //Mark Proposal as Sub Standard for Alteration in ILAS
                        SHAB.Data.LNP1_POLICYMASTRDB.MarkSubStand(proposal, "Y");
                    }
                    else
                    {
                        SHAB.Data.LNP1_POLICYMASTRDB.MarkSubStand(proposal, "N");
                    }

                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                    string sqlString = "insert into lncm_comments\n" +
                    "  (NP1_PROPOSAL,\n" +
                    "   CM_SERIAL_NO,\n" +
                    "   CM_COMMENTDATE,\n" +
                    "   CM_COMMENTBY,\n" +
                    "   CM_ACTION,\n" +
                    "   CM_COMMENTS)\n" +
                    "values\n" +
                    "  ('" + proposal + "',\n" +
                    "   (Select nvl(max((nvl(CM_SERIAL_NO, 0) + 1)), 1)\n" +
                    "      from lncm_comments\n" +
                    "     where np1_proposal = '" + proposal + "'),\n" +
                    "     sysdate,\n" +
                    "   '" + user + "',\n" +
                    //chg_closing   comments above three lines
                    //"     where np1_proposal = '" + proposal + "'),\n";
                    //if (SessionObject.Get("ClossingFlag").ToString() == "P")
                    //{
                    //    sqlString += "     to_date('" + Convert.ToDateTime(SessionObject.Get("ClossingDate")).ToString("MM/dd/yyyy") + "','MM/dd/yyyy'),\n";
                    //}
                    //else
                    //{
                    //    sqlString += "  sysdate ,\n";
                    //}
                    //sqlString += "   '" + user + "',\n" +
//chg_closing_end

                    "   'T',\n" +
                    "   'Transferred')";

                    cmd = (OleDbCommand)DB.CreateCommand(sqlString, DB.Connection);
                    cmd.ExecuteNonQuery();
                    cmd = (OleDbCommand)DB.CreateCommand("TRANSFERTOILAS", DB.Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    OleDbParameter param = new OleDbParameter("ERRMSG", OleDbType.VarChar, 1000);
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new OleDbParameter("PFROMPROPOSAL", OleDbType.VarChar)).Value = proposal;
                    cmd.Parameters.Add(new OleDbParameter("PTOPROPOSAL", OleDbType.VarChar)).Value = proposal;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    string errorMessage = cmd.Parameters["ERRMSG"].Value == null ? "" : cmd.Parameters["ERRMSG"].Value.ToString();

                    //	cs.Set("PFROMPROPOSAL", OleDbType.VarChar, proposal);
                    //	cs.Set("PTOPROPOSAL", OleDbType.VarChar, proposal);
                    //	cs.RegisetrOutParameter("ERRORMSG", OleDbType.VarChar, 2000);

                    //DB.BeginTransaction();
                    //	cs.Execute();
                    //DB.CommitTransaction();

                    //string strError = Convert.ToString(cs.Get("ERRORMSG"));
                    if (errorMessage.Length > 0)
                    {
                        DB.RollbackTransaction();
                        return (errorMessage);
                    }
                    else
                    {
                        callComp = false;
                        //DB.CommitTransaction();
                    }
                }
                catch (Exception ex)
                {
                    DB.RollbackTransaction();
                    string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
                    return (ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    //cs=null;
                    //DB.CommitTransaction();
                    DB.Connection.Close();
                    DB.Connection.Dispose();
                }
                rowset policyNo = DB.executeQuery("select p1.np1_policyno from lnp1_policymastr p1 where p1.np1_proposal='" + proposal + "'");
              
                if (policyNo.next())
                {
                    retVal = retVal + " - " + policyNo.getString(1);
                }
            }
            return retVal;
        }

        //chg_closing
        //public static void SetLoginSession(string closingFlag, DateTime closingDate) 
        //{
        //    OleDbCommand cmd = null;
        //    try
        //    {
        //        cmd = (OleDbCommand)DB.CreateCommand("BA_SESSION_DEFAULT", DB.Connection);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new OleDbParameter("p_session_date", OleDbType.Date)).Value = closingDate;
        //        cmd.Parameters.Add(new OleDbParameter("p_month_type", OleDbType.VarChar)).Value = closingFlag;
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}
        //chg_closing_end

        public static void SetEmp(String empno, String empname)
        {
            OleDbCommand cmd = null;
            try
            {

                cmd = (OleDbCommand)DB.CreateCommand("raise_sal", DB.Connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new OleDbParameter("p_empno", OleDbType.VarChar)).Value = empno;
                cmd.Parameters.Add(new OleDbParameter("p_empname", OleDbType.VarChar)).Value = empname;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DB.RollbackTransaction();
                throw new ProcessException(ex.Message);
            }
            finally
            {
                cmd.Dispose();
            }

        }

        public static string PopUpFlag()
        {
            return SessionObject.Get("DownloadCompleted").ToString();
        }
        public static void SetPopUpFlag()
        {
            SessionObject.Set("DownloadCompleted","False");
        }
        public static void AdmindeclineProposalCallStatus()
        {
            SessionObject.Set("PendingList", "1");
            callComp = true;
        }
        public static string AdmindeclineProposal(string propStr)
        {
            string status = string.Empty;
            if (callComp)
            {
                propStr = propStr.Substring(1, propStr.Length - 2);
                try
                {
                    List<string> QueryList = new List<string>();
                    string selectedIds = string.Empty;
                    string[] ids = propStr.Split('|');
                    foreach (string item in ids)
                    {
                        string sqlString1 = "insert into lncm_comments values('" + item + "',(Select nvl(max((nvl(CM_SERIAL_NO,0) + 1)),1) from lncm_comments where np1_proposal='" + item + "'),sysdate,'admin','Decline','Decline by Admin')";
                        QueryList.Add(sqlString1);
                        selectedIds += item + ",";
                    }
                    selectedIds = selectedIds.Substring(0, selectedIds.Length - 1);
                    OleDbCommand cmd = null;
                    string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                    string sqlString = "update lnp1_policymastr set cst_statuscd='14',np1_statusDate=sysdate,np1_selected=''\n" +
                    "where np1_proposal in (" + selectedIds + ")";
                    int j = 0;
                    foreach (string item in QueryList)
                    {
                        cmd = (OleDbCommand)DB.CreateCommand(item, DB.Connection);
                        j += cmd.ExecuteNonQuery();
                    }

                    if (j == QueryList.Count)
                    {
                        cmd = (OleDbCommand)DB.CreateCommand(sqlString, DB.Connection);
                        int i = cmd.ExecuteNonQuery();

                        if (i > 0)
                        {
                            callComp = false;
                            DB.CommitTransaction();
                            status = "Successfully Declined";
                        }
                        else
                        {
                            status = "Server Error";
                            DB.RollbackTransaction();
                        }
                    }
                    else
                    {
                        status = "Server Error";
                    }

                }
                catch (Exception ex)
                {
                    DB.RollbackTransaction();
                    string error = ex.Message.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\"", "");
                    status = ex.Message;
                }
                finally
                {
                    DB.Connection.Close();
                    DB.Connection.Dispose();

                }
            }
            return status;
        }

        public static string CheckForDeclineProposal(string propStr)
        {
            string status = "";
            string strQuery = "select case when nvl(CST_STATUSCD, '0') = '14' then 'Declined' else 'Other' end status from lnp1_policymastr where NP1_PROPOSAL=" + propStr + "";
            status = SHAB.Business.LNP1_POLICYMASTR.GetProposalStatus(strQuery);
            return status;
        }

        public static string IS_BYPASS
        {
            get
            {
                return (ConfigurationSettings.AppSettings["BYPASS_PWD_POLICY"] != null) ? (ConfigurationSettings.AppSettings["BYPASS_PWD_POLICY"].ToString()) : "N";
            }
        }

        public static object Session { get; private set; }

        public static bool BYPASS_PWD_CHECK(string IsAdmin)
        {
            if (IsAdmin == "ADMIN")
            {
                return false;
            }
            else if (IS_BYPASS == "Y")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static string ValidateSECPValidation(string nicuser)
        {
            string errorMessage = string.Empty;
            rowset rsPlan = DB.executeQuery("Select VFS_Query from LVFS_FIELDSETUP where VFS_CODE='SECPVALIDATION'");
            if (rsPlan.next())
            {
                string procedure = rsPlan.getObject(1).ToString();
                string[] paramlist = procedure.Split('-');
                string nic = nicuser.Replace("-", "");
                try
                {
                    OleDbCommand cmd = null;
                    cmd = (OleDbCommand)DB.CreateCommand(paramlist[0], DB.Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    OleDbParameter param = new OleDbParameter("user_Validation", OleDbType.VarChar, 200);
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new OleDbParameter("user_NPHCODE", OleDbType.VarChar)).Value = nic;
                    cmd.Parameters.Add(new OleDbParameter("lapsYear", OleDbType.Numeric)).Value =Convert.ToInt32(paramlist[1]);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    if (!cmd.Parameters["user_Validation"].Value.ToString().Equals("Valid"))
                    {
                        errorMessage = cmd.Parameters["user_Validation"].Value.ToString();
                    }

                }
                catch (Exception e)
                {
                    errorMessage = "Error " + e.Message;
                }
            }
            else
            {
                errorMessage ="";
            }
            return errorMessage;
        }

        //chg_closing
        //public static DataTable Get_SystemParameters()    
        //{
        //    try
        //    {
        //        string sqlString = "select 'C' Code, 'Current Month' || '-' ||\n" +
        //        "       TO_CHAR(Case\n" +
        //        "          When Months_Between(last_day(Trunc(SysDate)),TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY')) = 0\n" +
        //        "          Then TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY')\n" +
        //        "          Else Add_Months(TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY'),1)\n" +
        //        "       End,'dd/MM/yyyy') \"MonthDate\"\n" +
        //        "from dual\n" +
        //        "Union All\n" +
        //        "select 'P', 'Previous Month' || '-' ||\n" +
        //        "       TO_CHAR(Case\n" +
        //        "          When Months_Between(last_day(Trunc(SysDate)),TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY')) <> 0\n" +
        //        "          Then TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY')\n" +
        //        "          Else Add_Months(TO_DATE(Get_Syspara_BO.GET_VALUE('GLOBL','MONTH_END_DATE'), 'DD/MM/YYYY'),-1)\n" +
        //        "       End,'dd/MM/yyyy') \"MonthDate\"\n" +
        //        "from dual";
        //        return DB.getDataTable(sqlString);
        //    }
        //    catch (Exception)
        //    {
        //        return new DataTable();
        //    }
        //}
        //chg_closing_end
        public static string Get_CommenDate(string Proposal)
        {
            string CommDate = "";
            CommDate = SHAB.Business.LNP1_POLICYMASTR.GetCommencementDate(Proposal);
            return CommDate;
        }
        public static void UpdateCommencementDate_New(string commencementDate, string proposal)
        {
            try
            {

                SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate_New(commencementDate, proposal);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private static void UpdateCommencementDate(string commencementDate, string proposal)
        {
            //String commencementDate1 = "29/02/2024";    //chg-04032024 Feb-29 Add line
            try
            {
                ace.Ace_General.getGeneratedCommencementDate(Convert.ToDateTime(commencementDate)).ToString("dd/MM/yyyy");
                SHAB.Business.LNP1_POLICYMASTR.UpdateCommencmentDate(proposal, Convert.ToDateTime(commencementDate));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static DateTime getGeneratedCommencementDate(DateTime dtCurrentDate)
        {
            //**************************************************************************************//
            //******************* By default Current date is Commencement Date *********************//
            //**************************************************************************************//
            DateTime dtCommDate = new DateTime(dtCurrentDate.Year, dtCurrentDate.Month, dtCurrentDate.Day);


            //**************************************************************************************//
            //******************** Get alternate Commencement Date if needed ***********************//
            //**************************************************************************************//
            DateTime CommencDate = dtCurrentDate;
            try
            {
                //we don’t assign 28-31 days of a month as a commencement date on proposals.
                if (CommencDate.Day > 27)
                {
                    string ComDate = "27/" + CommencDate.Month + "/" + CommencDate.Year;    //chg-04032024 Feb-29 comments and Add below

                    //string ComDate = "29/" + CommencDate.Month + "/" + CommencDate.Year;

                    //string ComDate = CommencDate.Day + "/" + CommencDate.Month + "/" + CommencDate.Year;
                    CommencDate = Convert.ToDateTime(ComDate);
                }

            }
            catch (Exception ex)
            {

            }
            return dtCurrentDate;
        }




        public static bool IsBranchByPass(string BANKCODE)
        {
            //1- UBL // 3-BAL
            //			string[] BRARRAY = { "1","3","5","4","6","7" };
            //			foreach (string val in BRARRAY)
            //			{
            //				if (val==BANKCODE)
            //				{
            //					return true;
            //				}
            //			}
            //			return false;

            if (BANKCODE != "2")
            {
                return true;
            }
            return false;

        }

        public static bool MultiBranchCase(string BANKCODE)
        {
            //3- BAL
            string[] BRARRAY = { "3" };
            foreach (string val in BRARRAY)
            {
                if (val == BANKCODE)
                {
                    return true;
                }
            }
            return false;
        }



        public static bool IsPaymentFromYFile(string BANKCODE)
        {
            //2- FWB
            string[] BRARRAY = { "2" };
            foreach (string val in BRARRAY)
            {
                if (val == BANKCODE)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsByPassDownLoadFile(string BANKCODE)
        {
            //2- FWB
            string[] BRARRAY = { "2" };
            foreach (string val in BRARRAY)
            {
                if (val == BANKCODE)
                {
                    return true;
                }
            }
            return false;
        }


        public static string getPPR_PRODCD(string proposal)
        {
            rowset rsPlan = DB.executeQuery("SELECT PPR_PRODCD FROM LNPR_PRODUCT  WHERE  NP1_PROPOSAL ='" + proposal + "' AND NVL(NPR_BASICFLAG,'N') = 'Y'");
            if (rsPlan.next())
            {
                return "" + rsPlan.getObject(1);
            }
            return null;
        }


        public static double getPremium(string np1_proposal)
        {

            rowset rowLNPR_PRODUCT = DB.executeQuery("select sum(NPR_PREMIUM)+sum(NPR_EXCESSPREMIUM)+ SUM(NPR_LOADING) FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + np1_proposal + "'");
            if (rowLNPR_PRODUCT.next())
            {
                return rowLNPR_PRODUCT.getDouble(1);
            }
            return 0;
        }

        public static bool isLoginExpired()
        {
            rowset rsExpiry = DB.executeQuery("select CSD_VALUE from lcsd_systemdtl where  CSH_ID ='GLOBL' and CSD_TYPE ='EXPIRY_DATE'");
            if (rsExpiry.next())
            {
                string str_expiry_date = rsExpiry.getString(1);
                DateTime dte_expiry_date = new DateTime(Int32.Parse(str_expiry_date.Split('/')[2]), Int32.Parse(str_expiry_date.Split('/')[1]), Int32.Parse(str_expiry_date.Split('/')[0]));
                return (DateTime.Now >= dte_expiry_date);
            }

            return false;
        }

        public static bool isLoginExpired(string str_expiry_date)
        {
            try
            {
                DateTime dte_expiry_date = new DateTime(Int32.Parse(str_expiry_date.Split('/')[2]), Int32.Parse(str_expiry_date.Split('/')[1]), Int32.Parse(str_expiry_date.Split('/')[0]));
                return (DateTime.Now >= dte_expiry_date);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static int getEntryAge(string proposal)
        {
            try
            {
                int entryAge = 0;
                rowset rs = DB.executeQuery("SELECT NP2_AGEPREM FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='" + proposal + "'");
                if (rs.next())
                {
                    if (rs.getObject("NP2_AGEPREM") != null)
                    {
                        entryAge = rs.getInt("NP2_AGEPREM");
                    }
                }
                return entryAge;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public static int calculate_Age_InYear(DateTime dtDOB)
        {
            int age = DateTime.Now.Year - dtDOB.Year;
            // subtract another year if we're before the birth day in the current year
            DateTime todayDate = DateTime.Now;
            TimeSpan totalDiff = todayDate.Subtract(dtDOB);

            string roundingCriteria = clsIlasUtility.getAgeRoundingCriteria().ToUpper();
            int totalYears = 0;
            if (roundingCriteria == "FLOOR")
                totalYears = Convert.ToInt32(Math.Floor(totalDiff.Days / 365.25));
            else if (roundingCriteria == "CEIL")
                totalYears = Convert.ToInt32(Math.Ceiling(totalDiff.Days / 365.25));
            else
                totalYears = Convert.ToInt32(Math.Round(totalDiff.Days / 365.25, 0));

            return totalYears;
        }

        public static string getUserType(string user)
        {
            string query = "SELECT USE_TYPE FROM USE_USERMASTER WHERE USE_USERID='" + user + "'";
            rowset rs = DB.executeQuery(query);
            if (rs.next())
            {
                if (rs.getObject("USE_TYPE") == null)
                {
                    return "'N'";
                }
                else
                {
                    return rs.getString("USE_TYPE");
                }

            }
            else
            {
                return "N";
            }

        }

        ///////============= UPDATE UNCOLLECTED STATUS - 08-May-2018
        public static void markStatus_Uncollected(string proposal)
        {
            try
            {
                string[] ChannelDet = SHAB.Data.CCS_CHANLSUBDETLDB.GetBranchDetails(proposal).Split(',');
                if (ChannelDet[0].ToString() == "2" && ChannelDet[1].ToString() == "4")
                {
                    //LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "C"); 
                    SHAB.Business.LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "C");
                }
                else
                {
                    //LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "R"); 
                    SHAB.Business.LNP1_POLICYMASTR.markStatus_Uncollected(proposal, "R");
                }


            }
            catch (Exception ex)
            { ex.ToString(); }
            //LNP1_POLICYMASTRDB.MarkStatus_Payment(proposal, status);
        }
        //////////============= UPDATE UNCOLLECTED STATUS - 08-May-2018

        #endregion

        #region Personlization for Application
        private static string APPLICATION = "";
        public const string APP_BANCA = "BANCASSURANCE";
        public const string APP_ILLUS = "ILLUSTRATION";
        public const string BANCACHANNEL = "2";
        public const string AGENTPREFIX = "90";

        public static string LoadGlobalStyle()
        {
            string application = getApplicationName();
            if (application == APP_ILLUS)
            {
                return "<LINK rel=\"stylesheet\" href=\"Theme_Illus/Styles/MainPage.css\" type=\"text/css\" >";
            }
            else if (application == APP_BANCA)
            {
                return "<LINK rel=\"stylesheet\" href=\"Styles/MainPage.css\" type=\"text/css\" >";
            }
            else if (application == "")
            {
                return "<script language=\"JavaScript\">alert('Application is not properly configured.');</script>";
                /*window.close();self.close();*/
            }
            else
            {
                return "<script language=\"JavaScript\">alert('" + application + "');</script>";
            }
        }

        public static string LoadPageStyle()
        {
            string application = getApplicationName();
            if (application == APP_ILLUS)
            {
                return "<LINK href=\"Theme_Illus/Styles/Style.css\" type=\"text/css\" rel=\"stylesheet\">";
            }
            else if (application == APP_BANCA)
            {
                return "<LINK href=\"Styles/Style.css\" type=\"text/css\" rel=\"stylesheet\">";
            }
            else if (application == "")
            {
                return "<script language=\"JavaScript\">alert('Application is not properly configured.');</script>";
                /*window.close();self.close();*/
            }
            else
            {
                return "";
            }

        }

        /*public static string getApplicationInfo()
        {
            try
            {
                return Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppName"]);
            }
            catch
            {
                return "";
            }
        }*/

        public static void setApplicationName()
        {
            try
            {
                string appName = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppName"]);
                APPLICATION = appName;
            }
            catch { }
        }

        public static string getApplicationName()
        {
            return APPLICATION.ToUpper();
        }

        public static string getApplicationTitle()
        {
            return APPLICATION;
        }

        public static bool IsBancaasurance()
        {
            if (getApplicationName() == APP_BANCA)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIllustration()
        {
            if (getApplicationName() == APP_ILLUS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Dynamic Style (Personalization)
        private const string MAINCSS = "MAIN_";
        private const string INNERCSS = "INNER_";
        private const string CHANNEL = "CCH_CODE";
        private const string CHANNEL_DETAIL = "CCD_CODE";





        public static string loadMainStyle()
        {
            return loadStyle(MAINCSS);
        }
        public static string loadInnerStyle()
        {
            return loadStyle(INNERCSS);
        }

        private static string loadStyle(string cssPrefix)
        {

            try
            {
                string user = Convert.ToString(SessionObject.Get("s_USE_USERID"));
                string styleBasis = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["StyleBasis"]).ToUpper();
                string styleInfo = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings[styleBasis]).ToUpper();

                if (styleBasis == null || styleBasis.Trim() == "" || styleInfo == null || styleInfo.Trim() == "")
                {
                    //Load Default style
                    return LoadDefaultStyle(cssPrefix);
                }
                else
                {
                    string query = "";
                    if (styleBasis == CHANNEL)
                        query = "SELECT CCH_CODE AS CODE FROM LUCH_USERCHANNEL WHERE USE_USERID='" + user + "'";
                    else if (styleBasis == CHANNEL_DETAIL)
                        query = "SELECT CCH_CODE" + SHMA.Enterprise.Data.PortableSQL.getConcateOperator() + "CCD_CODE AS CODE FROM LUCH_USERCHANNEL WHERE USE_USERID='" + user + "'";

                    rowset rs = DB.executeQuery(query);
                    if (rs.next())
                    {
                        //Get all style information in array and traverse the array for CSS
                        string[] AllStylesInfoArray = styleInfo.Split('~');
                        for (int i = 0; i < AllStylesInfoArray.Length; i++)
                        {
                            string[] SingleStyleInfoArray = AllStylesInfoArray[i].Split(',');
                            string cssName = "";
                            if (rs.getString("CODE") == SingleStyleInfoArray[0])
                            {
                                for (int j = 1; j < SingleStyleInfoArray.Length; j++)
                                {
                                    if (SingleStyleInfoArray[j].IndexOf(cssPrefix) > -1)
                                    {
                                        cssName = SingleStyleInfoArray[j];
                                        break;
                                    }
                                }

                                if (cssName.Trim() != "")
                                {
                                    return LoadStyle(cssName);
                                }
                            }
                        }
                        //Load Default style
                        return LoadDefaultStyle(cssPrefix);
                    }
                    else
                    {
                        //Load Default style
                        return LoadDefaultStyle(cssPrefix);
                    }
                }
            }
            catch (Exception e)
            {
                //Load Default style
                return LoadDefaultStyle(cssPrefix);
            }
        }

        private static string LoadDefaultStyle(string cssPrefix)
        {
            if (cssPrefix == MAINCSS)
            {
                return "<LINK href=\"Styles/MainPage.css\" type=\"text/css\" rel=\"stylesheet\">";
            }
            else
            {
                return "<LINK href=\"Styles/Style.css\" type=\"text/css\" rel=\"stylesheet\">";
            }
        }

        private static string LoadStyle(string CSSName)
        {
            string fileName = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + @"Presentation\Styles\" + CSSName + ".css";
            if (System.IO.File.Exists(@fileName))
            {
                return "<LINK href=\"Styles/" + CSSName + ".css\" type=\"text/css\" rel=\"stylesheet\">";
            }
            else
            {
                if (CSSName.IndexOf(MAINCSS) > -1)
                {
                    return LoadDefaultStyle(MAINCSS);
                }
                else
                {
                    return LoadDefaultStyle(INNERCSS);
                }
            }
        }
        #endregion

        #region Ajax Call

        //Called from Illustration.js 
        public static Array getPremiumProductwise(string proposal)
        {
            ///*var sql1 = "Select SUM(NPR_PREMIUM),PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+ document.getElementById('txtNP1_PROPOSAL').value +"' and ppr_prodcd!=120 group by ppr_prodcd"; 
            string query = "Select SUM(NPR_PREMIUM) NPR_PREMIUM, PPR_PRODCD FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + proposal + "' and ppr_prodcd!=120 group by ppr_prodcd";
            return FetchDataArray(query);
        }

        //Called from Illustration.js 
        public static Array getProductFromGlobal(string proposal)
        {
            /////var sql = "Select INSTR(get_syspara.get_value('GLOBL','AV_PRODUCT'), pr.ppr_prodcd) CNT FROM LNPR_PRODUCT PR WHERE NP1_PROPOSAL='"+ document.getElementById('txtNP1_PROPOSAL').value +"' AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'"; 
            string query = "Select INSTR(get_syspara.get_value('GLOBL','AV_PRODUCT'), pr.ppr_prodcd) CNT FROM LNPR_PRODUCT PR WHERE NP1_PROPOSAL='" + proposal + "' AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'";
            return FetchDataArray(query);
        }

        //Called from Illustration.js 
        public static Array getLetter(string proposal)
        {
            ///var Letter= "select lnp2.np2_substandar, lnp1.np1_policyno from lnp1_policymastr lnp1, lnp2_policymastr lnp2 WHERE  lnp1.np1_proposal = lnp2.np1_proposal and lnp1.NP1_PROPOSAL='"+ document.getElementById('txtNP1_PROPOSAL').value +"'"; 
            string query = "select lnp2.np2_substandar, lnp1.np1_policyno from lnp1_policymastr lnp1, lnp2_policymastr lnp2 WHERE  lnp1.np1_proposal = lnp2.np1_proposal and lnp1.NP1_PROPOSAL='" + proposal + "'";
            return FetchDataArray(query);
        }

        //Called from Illustration.js 
        public static string Get_OCCUPATICD_Grid(string code)
        {
            //return "<a href='SearchItemClicked(\"12\")'>MUk</a>";
            string query1 = string.Empty;
            //string query = "SELECT COP_OCCUPATICD,COP_DESCR DESC_F  FROM LCOP_OCCUPATION  where rownum = 1 ORDER BY COP_DESCR "; 
            string query = "SELECT COP_OCCUPATICD,COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD NOT IN('A56','H05','H31','H32','H33','H38','H39','H40','J88','L12','M25','M26','M27','M32','M33','M81','N15','O69','R96','R97','T20','X41','X42','X43','X44','AD1','AD6','AD7','AG7','AG8','AK2','AK3','AK4','Ac4','Ac5','Ac6','Ac7','Ac8','AD0','Ac9','AF5','AF6','AG9','AH0','AH1','AH2','AH8','AH9','AI0','AI1','AI2','AD2','AD3','AD4','AD5','AH5','AH6','AH7','AJ5','AJ6','AJ7','AJ8','AH4','BO1','BO2') ORDER BY COP_DESCR ";
            if (code == "0")
            {
                query1 = "SELECT COP_OCCUPATICD,COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE rownum<21 ORDER BY COP_DESCR ";
            }
            else
            {
                query1 = "SELECT COP_OCCUPATICD,COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE lower(COP_DESCR) like lower('%" + code + "%')  and rownum<=20 ORDER BY COP_DESCR ";
            }

            query = EnvHelper.Parse(query1);
            IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query1));
            IDataReader reader = myCommand.ExecuteReader();



            string retVal = "";

            retVal += "";

            retVal += "<ul id='ulOccupation' class='ulSearch'>";
            retVal += "";

            bool flag = true;

            while (reader.Read())
            {
                if (flag)
                {
                    //retVal+="<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(\""+reader.GetValue(0).ToString()+"\");'>";
                    retVal += "<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(this,\"" + reader.GetValue(0).ToString() + "\");'>";
                    flag = false;
                }
                else
                {
                    //retVal+="<li class='ItemStyle ListRowAlt' onclick='SearchItemClicked(\""+reader.GetValue(0).ToString()+"\");'>";
                    retVal += "<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(this,\"" + reader.GetValue(0).ToString() + "\");'>";
                    flag = true;
                }

                retVal += "<a href='javascript:void(0);' onclick=''>";
                retVal += reader.GetValue(1).ToString();
                retVal += "</a>";
                retVal += "</li>";
            }
            reader.Close();
            /*
            Array db = FetchDataArray(query); 
            for(int i = 1; i<db.Length; i++){
                if(i%2==0)
                    retVal+="<li class='ItemStyle ListRowItem' onclick='SearchItemClicked('"+((Array)db.GetValue(i)).GetValue(0).ToString()+"');'>";
                else
                    retVal+="<li class='ItemStyle ListRowAlt' onclick='SearchItemClicked('"+((Array)db.GetValue(i)).GetValue(0).ToString()+"');'>";

                retVal+="<a href='#' onclick=''>";
                retVal+=((Array)db.GetValue(i)).GetValue(1).ToString();
                retVal+="</a>";
                retVal+="</li>";
            }
            */

            retVal += "</ul>";
            return retVal;
        }


        public static string Get_BSO_Grid(string code)
        {
            //return "<a href='SearchItemClicked(\"12\")'>MUk</a>";
            string query1 = string.Empty;
            if (code == "0")
            {
                query1 = "select aag_agcode || '-' || aag_fullname || '-' || aag_imedsupr || '-' ||\n" +
                                      "  aag_imedsupr_name || ' - ' || chl_level AAG_AGCODE\n" +
                                  " from vw_laag_bsc\n" +
                                  " where rownum<=20\n" +
                                  "order by aag_agcode";
            }
            else
            {
                query1 = "Select * From(\n" +
                            "select aag_agcode || '-' || aag_fullname || '-' || aag_imedsupr || '-' ||\n" +
                            "       aag_imedsupr_name || ' - ' || chl_level AAG_AGCODE\n" +
                            "  from vw_laag_bsc)\n" +
                            "  where lower(AAG_AGCODE) like lower('%" + code + "%')\n" +
                            "  and rownum<=20\n" +
                            " order by aag_agcode";
            }

            query1 = EnvHelper.Parse(query1);
            IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query1));
            IDataReader reader = myCommand.ExecuteReader();



            string retVal = "";

            retVal += "";

            retVal += "<ul id='ulOccupation' class='ulSearch'>";
            retVal += "";

            bool flag = true;

            while (reader.Read())
            {
                if (flag)
                {
                    //retVal+="<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(\""+reader.GetValue(0).ToString()+"\");'>";
                    retVal += "<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(this,\"" + reader.GetValue(0).ToString() + "\");'>";
                    flag = false;
                }
                else
                {
                    //retVal+="<li class='ItemStyle ListRowAlt' onclick='SearchItemClicked(\""+reader.GetValue(0).ToString()+"\");'>";
                    retVal += "<li class='ItemStyle ListRowItem' onclick='SearchItemClicked(this,\"" + reader.GetValue(0).ToString() + "\");'>";
                    flag = true;
                }

                retVal += "<a href='javascript:void(0);' onclick=''>";
                retVal += reader.GetValue(0).ToString();
                retVal += "</a>";
                retVal += "</li>";
            }
            reader.Close();
            /*
            Array db = FetchDataArray(query); 
            for(int i = 1; i<db.Length; i++){
                if(i%2==0)
                    retVal+="<li class='ItemStyle ListRowItem' onclick='SearchItemClicked('"+((Array)db.GetValue(i)).GetValue(0).ToString()+"');'>";
                else
                    retVal+="<li class='ItemStyle ListRowAlt' onclick='SearchItemClicked('"+((Array)db.GetValue(i)).GetValue(0).ToString()+"');'>";

                retVal+="<a href='#' onclick=''>";
                retVal+=((Array)db.GetValue(i)).GetValue(1).ToString();
                retVal+="</a>";
                retVal+="</li>";
            }
            */

            retVal += "</ul>";
            return retVal;
        }
        /*****************************************************************************************/
        /********************************* Execute Query *****************************************/
        /*****************************************************************************************/
        private static Array FetchDataArray(string query)
        {
            //System.Globalization.Calendar Clnd ; 

            ArrayList fetchInner;
            ArrayList fetchOuter = new ArrayList();
            string str_TempValue = null;
            try
            {
                query = query.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
                query = EnvHelper.Parse(query);
                IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query));
                IDataReader reader = myCommand.ExecuteReader();
                DataTable schema = reader.GetSchemaTable();
                fetchInner = new ArrayList();

                for (int i = 0; i < schema.Rows.Count; i++)
                {
                    str_TempValue = schema.Rows[i][0].ToString().ToUpper();
                    fetchInner.Add(str_TempValue);
                }

                fetchOuter.Add(fetchInner.ToArray());

                while (reader.Read())
                {
                    fetchInner = new ArrayList();
                    for (int i = 0; i < schema.Rows.Count; i++)
                    {
                        str_TempValue = reader.GetValue(i).ToString();
                        fetchInner.Add(str_TempValue);
                    }
                    //Parse_Errors(fetchInner.ToArray());
                    fetchOuter.Add(fetchInner.ToArray());
                }
                reader.Close();
            }
            catch (System.Exception e) { }
            finally
            {
                DB.DisConnect();
            }

            return fetchOuter.ToArray();
        }


        public static string validateTerm(string product, string term)
        {
            double dblTerm = double.Parse(term);
            try
            {
                rowset rsTerm = DB.executeQuery("SELECT PVL_VALIDFROM, PVL_VALIDTO FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='BTERM'");
                if (rsTerm.next())
                {
                    if (rsTerm.getObject("PVL_VALIDFROM") == null || rsTerm.getObject("PVL_VALIDTO") == null)
                    {
                        throw new Exception("Validation not properly defined. Either Range From or Range To is null");
                    }

                    double rangeFrom = double.Parse(rsTerm.getString("PVL_VALIDFROM"));
                    double rangeTo = double.Parse(rsTerm.getString("PVL_VALIDTO"));
                    if (dblTerm < rangeFrom || dblTerm > rangeTo)
                    {
                        throw new Exception("Valid Range is from " + rangeFrom + " to " + rangeTo);
                    }
                }
                else
                {
                    throw new Exception("Validation is not defined.");
                }

                /******** No Exception found ***********/
                return "";
            }
            catch (Exception e)
            {
                return "Error when validating Benefit Term. " + e.Message;
            }
        }

        public static string validateRate(string rate)
        {
            double dblrate = double.Parse(rate);
            try
            {
                rowset rsRate = DB.executeQuery("SELECT PVL_VALIDFROM, PVL_VALIDTO FROM LPVL_VALIDATION WHERE PPR_PRODCD='999' AND PVL_VALIDATIONFOR='INDEXRATE'");

                if (rsRate.size() < 1)
                {
                    throw new Exception("Validation is not defined for Indexation Rate");
                }

                string message = "";
                while (rsRate.next())
                {
                    if (rsRate.getObject("PVL_VALIDFROM") == null || rsRate.getObject("PVL_VALIDTO") == null)
                    {
                        throw new Exception("Validation not properly defined. Either Range From or Range To is null");
                    }

                    double rangeFrom = double.Parse(rsRate.getString("PVL_VALIDFROM"));
                    double rangeTo = double.Parse(rsRate.getString("PVL_VALIDTO"));
                    if (dblrate >= rangeFrom && dblrate <= rangeTo)
                    {
                        message = "";
                        break;
                    }
                    else //if (dblrate < rangeFrom || dblrate > rangeTo)
                    {
                        if (message == "")
                        {
                            message = message + "[" + rangeFrom + " - " + rangeTo + "]";
                        }
                        else
                        {
                            message = message + " AND [" + rangeFrom + " - " + rangeTo + "]";
                        }
                    }
                }

                /******** No Exception found ***********/
                if (message == "")
                {
                    return "";
                }
                else
                {
                    throw new Exception("Valid Range(s) : " + message);
                }
            }
            catch (Exception e)
            {
                return "Error when validating Indexation Rate. " + e.Message;
            }
        }


        public static string validateReportCriteria(string proposal, string reportType)
        {
            string errorMessage = "";
            try
            {
                //******* Preimum Check (For All Report type) *********
                //rowset rs = DB.executeQuery("Select NPR_PREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+ proposal +"' AND NPR_BASICFLAG='Y' AND NPR_PREMIUM IS NOT NULL");
                rowset rs = DB.executeQuery("Select NPR_PREMIUM, NPR_SUMASSURED FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + proposal + "' AND NPR_BASICFLAG='Y' ");

                if (rs.next())
                {
                    if (rs.getObject("NPR_PREMIUM") == null || rs.getDouble("NPR_PREMIUM") == 0)
                    {
                        throw new Exception("Please Calculate Premium from Plan-Rider.");
                    }
                    //if(rs.getObject("NPR_SUMASSURED") == null || rs.getDouble("NPR_SUMASSURED") == 0 )
                    //{
                    //	throw new Exception("Sum Assured not defined.");
                    //}
                }
                else
                {
                    throw new Exception("Plan not defined.");
                }


                //******* Correspondence Address (Only for PROFILE) *********
                if (reportType == clsIlasConstant.REPORTTYPE_PROFILE)
                {
                    //rs = DB.executeQuery("Select NPH_CODE FROM LNAD_ADDRESS WHERE NP1_PROPOSAL='"+ proposal +"' AND NAD_ADDRESSTYP='C'");
                    rs = DB.executeQuery("Select COUNT(A.NPH_CODE) FROM LNAD_ADDRESS A, LNU1_UNDERWRITI U WHERE U.NPH_CODE = A.NPH_CODE AND U.NP1_PROPOSAL = '" + proposal + "' AND NAD_ADDRESSTYP='C'");
                    if (rs.next() == false)
                    {
                        throw new Exception("Please enter Information till Correspondence Address.");
                    }
                }

                //******* Policy Number (Only for POLICY) *********
                if (reportType == clsIlasConstant.REPORTTYPE_POLICY)
                {
                    /*
                    //Standard Case
                    rs = DB.executeQuery("select 'A' FROM LNP2_POLICYMASTR where NP1_PROPOSAL='"+ proposal +"' AND NP2_SUBSTANDAR <> 'Y'");
                    if(rs.next())
                    {	//Policy issued or not
                        rs = DB.executeQuery("select NP1_POLICYNO from LNP1_POLICYMASTR WHERE  NP1_PROPOSAL='" + proposal +"' AND NP1_POLICYNO IS NULL");
                        if(rs.next())
                        {
                            throw new Exception("Policy not issued");
                        }
                    }
                    */
                    //rs = DB.executeQuery("select NVL(LNP1.NP1_POLICYNO,' ') as NP1_POLICYNO, NVL(LNP2.NP2_SUBSTANDAR,' ') NP2_SUBSTANDAR FROM LNP1_POLICYMASTR LNP1, LNP2_POLICYMASTR LNP2 WHERE LNP1.NP1_PROPOSAL=LNP2.NP1_PROPOSAL AND LNP1.NP1_PROPOSAL='"+ proposal +"' ");
                    rs = DB.executeQuery("select LNP1.NP1_SELECTED, LNP2.NP2_SUBSTANDAR, LNP1.NP1_POLICYNO FROM LNP1_POLICYMASTR LNP1, LNP2_POLICYMASTR LNP2 WHERE LNP1.NP1_PROPOSAL=LNP2.NP1_PROPOSAL AND LNP1.NP1_PROPOSAL='" + proposal + "' ");
                    if (rs.next())
                    {
                        string posted = rs.getObject("NP1_SELECTED") == null ? "N" : rs.getString("NP1_SELECTED").Trim();
                        string subStand = rs.getObject("NP2_SUBSTANDAR") == null ? "" : rs.getString("NP2_SUBSTANDAR").Trim();
                        string policyNo = rs.getObject("NP1_POLICYNO") == null ? "" : rs.getString("NP1_POLICYNO").Trim();

                        //***** Must be posted *****//
                        if (posted != "N")
                        {	//***** Standard Case *****//
                            if (subStand == "N")
                            {	//***** Policy must be issue *****//
                                if (policyNo == "")
                                {
                                    //throw new Exception("Policy not issued.");
                                    // REMARKED FOR STATE LIFE
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Please post the proposal first.");
                        }
                    }
                    else
                    {
                        throw new Exception("Not able generate Policy(Report).");
                    }

                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return errorMessage;
        }
        #endregion

        #region Version (Test)
        public static bool IsTestVersion()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["TestVersion"] == null)
                return false;
            else
                return true;
        }

        public static string getTestVersionInfo()
        {
            return Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["TestVersion"]).Trim();
        }
        #endregion
    }
}
