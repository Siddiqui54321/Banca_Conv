using System;
using System.Data;
using System.Data.OleDb;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data
{
    public class USE_USERMASTERDB : SHMA.CodeVision.Data.DataClassBase
    {
        //<constructor>
        public USE_USERMASTERDB(DataHolder dh) : base(dh)




        { }
        //</constructor>
        //<property><property-name>TableName</property-name><property-signature>
        public override String TableName
        {
            //</property-signature><property-body>
            get { return "USE_USERMASTER"; }
            //			//			//			//			//</property-body>
        }
        //</property>
        //<property><property-name>RecordCount</property-name><property-signature>
        public static int RecordCount
        {
            //</property-signature><property-body>
            get
            {
                const string strQuery = "SELECT COUNT(*) FROM USE_USERMASTER";
                return (int)DB.CreateCommand(strQuery).ExecuteScalar();
            }
            //			//			//			//			//</property-body>
        }
        //</property>
        //<method><method-name>Exists</method-name><method-signature>
        public static Boolean Exists(NameValueCollection pkNameValue)
        {
            //</method-signature><method-body>
            String strQuery = "SELECT count(*) FROM USE_USERMASTER WHERE USE_USERID=? ";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID", DbType.String, 10, pkNameValue["USE_USERID"]));
            int noOfRecords = (int)myCommand.ExecuteScalar();
            return (noOfRecords >= 1);
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_UM_USERMANAGMENT_lister_filter_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_UM_USERMANAGMENT_lister_filter_RO(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(148);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID,USE_NAME FROM USE_USERMASTER   WHERE  ({0} like '{1}') order by USE_NAME ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_UM_USERMANAGMENT_lister_filter_RO_ByBank</method-name><method-signature>
        public static IDataReader GetILUS_ET_UM_USERMANAGMENT_lister_filter_RO_ByBank(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(148);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID,USE_NAME FROM USE_USERMASTER   WHERE  ({0} like '{1}') AND AAG_AGCODE LIKE '95%' order by USE_NAME ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>
        //<method><method-name>getAll_RO</method-name><method-signature>
        public static IDataReader getAll_RO()
        {
            //</method-signature><method-body>
            const String strQuery = "SELECT USE_USERID,USE_NAME,USE_PASSWORD, CCH_CODEDEFAULT, USE_TYPE FROM USE_USERMASTER";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_UM_USERMANAGMENT_lister_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_UM_USERMANAGMENT_lister_RO(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(148);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID,USE_NAME FROM USE_USERMASTER  order by USE_NAME ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_UM_USERMANAGMENT_lister_RO_ByBank</method-name><method-signature>
        public static IDataReader GetILUS_ET_UM_USERMANAGMENT_lister_RO_ByBank(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(148);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID,USE_NAME FROM USE_USERMASTER WHERE AAG_AGCODE LIKE '95%' ORDER BY USE_NAME ");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>


        //<method><method-name>FindByPK</method-name><method-signature>
        public DataHolder FindByPK(string USE_USERID)
        {
            //</method-signature><method-body>
            //========================== Agency Setup IBAD CODE 18-FEB-2019
            //String strQuery = "SELECT USE_USERID,USE_NAME,USE_PASSWORD, CCH_CODEDEFAULT, USE_TYPE, AAG_AGCODE, USE_ACTIVE FROM USE_USERMASTER WHERE USE_USERID=? ";
            String strQuery = "SELECT USE_USERID,USE_NAME,USE_PASSWORD, CCH_CODEDEFAULT, USE_TYPE, AAG_AGCODE, USE_DESIGNATI, USE_JOBDESCRIP, USE_USERTYPE,USE_ACTIVE FROM USE_USERMASTER WHERE USE_USERID=? ";
            //========================== Agency Setup IBAD CODE 18-FEB-2019
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID", DbType.String, 10, USE_USERID));

            this.Holder.FillData(myCommand, "USE_USERMASTER"); return this.Holder;
            //</method-body>
        }
        //</method>

        //<method><method-name>GetDDL_ILUS_ET_GE_UC_USERCOUNTRY_USE_USERID _RO</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_GE_UC_USERCOUNTRY_USE_USERID_RO()
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(130);//to do we have to Optimize it too.
            sb_query.Append(" DESC_F  FROM USE_USERMASTER  ");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>


        //shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET.aspx
        //<method><method-name>GetDDL_ILUS_ET_NM_PER_PROPOSALDET_USE_USERID_RO</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_NM_PER_PROPOSALDET_USE_USERID_RO()
        {

            //filter mechanism 
            //SELECT CCH_CODE || '-' || CCH_DESCR,CCH_CODE  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '')
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(209);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID, ");
            //			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //			sb_query.Append("'-'");
            //			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            sb_query.Append("USE_NAME DESC_F  FROM USE_USERMASTER ORDER BY USE_NAME");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>


        //shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PROPOSALDET.aspx
        //<method><method-name>GetDDL_ILUS_ET_NM_PER_PROPOSALDET_USE_USERID_RO</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_NM_PROPOSAL_USE_USERID_RO()
        {
            //filter mechanism 
            //SELECT CCH_CODE || '-' || CCH_DESCR,CCH_CODE  FROM CCH_CHANNEL  WHERE CCH_CODE IN (SELECT CCH_CODE FROM LCCC_COUNTRYCHANNEL WHERE CCN_CTRYCD = '')
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(209);//to do we have to Optimize it too.
            sb_query.Append("SELECT USE_USERID, ");
            //			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //			sb_query.Append("'-'");
            //			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            sb_query.Append("USE_NAME DESC_F  FROM USE_USERMASTER ORDER BY USE_NAME");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        public static IDataReader GetDDL_Users_data()
        {
            StringBuilder sb_query = new StringBuilder(209);
            sb_query.Append("Select use_userid,Use_Name from use_usermaster where use_type='P'");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
     
        }

        public static string getPasswordListHistory(string userID)
        {
            const string query_ = "select use_expression from use_usermaster where upper(use_userid) = ?";
            IDbCommand myCommand = DB.CreateCommand(query_);
            myCommand.Parameters.Add(DB.CreateParameter("@use_userid", DbType.String, 20, userID.ToUpper()));
            object strObj = myCommand.ExecuteScalar();
            return strObj != null ? strObj.ToString() : "";
        }
        public static string getLastUpdatedPasswordDate(string userID)
        {
            const string query_ = "select use_jobdescrip from use_usermaster where upper(use_userid) = ?";
            IDbCommand myCommand = DB.CreateCommand(query_);
            myCommand.Parameters.Add(DB.CreateParameter("@use_userid", DbType.String, 20, userID.ToUpper()));
            object strObj = myCommand.ExecuteScalar();
            return strObj != null ? strObj.ToString() : "";
        }
        public static string getPassword(string userID)
        {
            const string query_ = "select use_password from use_usermaster where upper(use_userid) = ?";
            IDbCommand myCommand = DB.CreateCommand(query_);
            myCommand.Parameters.Add(DB.CreateParameter("@use_userid", DbType.String, 20, userID.ToUpper()));
            object strObj = myCommand.ExecuteScalar();
            return strObj != null ? strObj.ToString() : "";
        }
    }
}
