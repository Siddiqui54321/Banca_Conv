using System;
using System.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data
{
    public class LUCH_USERCHANNELDB : SHMA.CodeVision.Data.DataClassBase
    {
        //<constructor>
        public LUCH_USERCHANNELDB(DataHolder dh) : base(dh)







        { }
        //</constructor>
        //<property><property-name>TableName</property-name><property-signature>
        public override String TableName
        {
            //</property-signature><property-body>
            get { return "LUCH_USERCHANNEL"; }
            //			//			//			//			//			//			//			//</property-body>
        }
        //</property>
        //<method><method-name>GetILUS_ET_UC_USERCHANNEL_Data</method-name><method-signature>
        public DataHolder GetILUS_ET_UC_USERCHANNEL_Data()
        {
            string channel = "";
            ////===================== IBAD CODE FOR CHANNEL DETAIL 13-FEB-2019
            IDataReader drCurrentUserChannel = LUCH_USERCHANNELDB.GetUserChannel();
            while (drCurrentUserChannel.Read())
            { channel = Convert.ToString(drCurrentUserChannel["CCH_CODE"]); }
            //		if (drCurrentUserChannel.hasr)
            //		{channel = Convert.ToString(drCurrentUserChannel["CCH_CODE"]);}
            //		else {channel = "2";}		 		
            drCurrentUserChannel.Close();

            ////===================== IBAD CODE FOR CHANNEL DETAIL 13-FEB-2019

            //</method-signature><method-body>
            //StringBuilder sb_query=new StringBuilder(205);//to do we have to Optimize it too.
            StringBuilder sb_query = new StringBuilder(505);//to do we have to Optimize it too.
                                                            //sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,UCH_DEFAULT,USE_USERID FROM LUCH_USERCHANNEL WHERE ((USE_USERID=SV(\"USE_USERID\"))  )   ");

            sb_query.Append("SELECT UCH.CCH_CODE, UCH.CCD_CODE, UCH.CCS_CODE, UCH.UCH_DEFAULT, UCH.USE_USERID ");
            //sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //sb_query.Append("'-'");
            //sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //sb_query.Append(" CCH.CCS_DESCR DESC_F");
            sb_query.Append(" FROM LUCH_USERCHANNEL UCH WHERE UCH.CCH_CODE = '" + channel + "' ");
            sb_query.Append(" AND ((USE_USERID=SV(\"USE_USERID\"))  )");
            String query = EnvHelper.Parse(sb_query.ToString());
            IDbCommand myCommand = DB.CreateCommand(query);
            this.Holder.FillData(myCommand, "LUCH_USERCHANNEL");

            //            sb_query = new StringBuilder(505);
            //            sb_query.Append("SELECT UCH.CCH_CODE, UCH.CCD_CODE, UCH.CCS_CODE, UCH.UCH_DEFAULT, UCH.USE_USERID, CCH.CCS_CODE ");
            //		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //		sb_query.Append("'-'");
            //		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            //		sb_query.Append(" CCH.CCS_DESCR DESC_F");
            //		sb_query.Append(" FROM LUCH_USERCHANNEL UCH, CCS_CHANLSUBDETL CCH WHERE UCH.CCH_CODE = CCH.CCH_CODE");
            //		sb_query.Append(" AND UCH.CCD_CODE = CCH.CCD_CODE   AND UCH.CCS_CODE = CCH.CCS_CODE   AND UCH.CCH_CODE = '"+channel+"' ");
            //		sb_query.Append(" AND ((USE_USERID=SV(\"USE_USERID\"))  )");

            ////		sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,UCH_DEFAULT,USE_USERID FROM LUCH_USERCHANNEL WHERE USE_USERID= '1'");
            //		 query = EnvHelper.Parse(sb_query.ToString());
            //		 myCommand = DB.CreateCommand(query);
            //		this.Holder.FillData(myCommand, "LUCH_USERCHANNEL1");



            return this.Holder;
            //</method-body>
        }
        //</method>


        //<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO()
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(176);//to do we have to Optimize it too.
            sb_query.Append("SELECT CCN_CTRYCD,CCN_CTRYCD");
            sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            sb_query.Append("'-'");
            sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
            sb_query.Append("CCN_DESCR DESC_F  FROM LUCH_USERCHANNEL  ");
            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
        public static IDataReader GetDDL_LUCH_USERCHANNEL_CCS_CODE_RO()
        {
            string concat = SHMA.Enterprise.Data.PortableSQL.getConcateOperator();

            StringBuilder sb_query = new StringBuilder(176);//to do we have to Optimize it too.
            sb_query.Append("SELECT UC.CCH_CODE" + concat + "'~'" + concat + "UC.CCD_CODE" + concat + "'~'" + concat + "UC.CCS_CODE CCS_CODE, ");
            sb_query.Append("CH.CCH_DESCR" + concat + "' - '" + concat + "CD.CCD_DESCR" + concat + "' - '" + concat + "CS.CCS_DESCR DESC_F ");
            sb_query.Append("FROM LUCH_USERCHANNEL UC, CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS ");
            sb_query.Append("WHERE USE_USERID=SV(\"s_USE_USERID\") AND ");
            sb_query.Append("UC.CCH_CODE=CS.CCH_CODE AND UC.CCD_CODE=CS.CCD_CODE AND UC.CCS_CODE=CS.CCS_CODE AND ");
            sb_query.Append("CS.CCH_CODE=CD.CCH_CODE AND CS.CCD_CODE=CD.CCD_CODE AND ");
            sb_query.Append("CD.CCH_CODE=CH.CCH_CODE ");

            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();

        }

        public static IDataReader GetDDL_LUCH_USERCHANNEL_CCS_CODE_RO_FILTER(string USER_ID, string USER_TYPE)
        {
            string concat = SHMA.Enterprise.Data.PortableSQL.getConcateOperator();
            StringBuilder sb_query = new StringBuilder(176);//to do we have to Optimize it too.

            if (USER_TYPE.Equals("A") || USER_TYPE.Equals("P"))
            {
                sb_query.Append("SELECT CCD_CODE ,CCD_DESCR DESC_F FROM CCD_CHANNELDETAIL WHERE CCH_CODE=");
                sb_query.Append("(SELECT CCH_CODE FROM LUCH_USERCHANNEL WHERE UPPER(USE_USERID)='" + USER_ID + "') ");
            }
            else if (USER_TYPE.Equals("B"))
            {
                sb_query.Append("SELECT CCD_CODE ,CCD_DESCR DESC_F FROM CCD_CHANNELDETAIL WHERE CCH_CODE='2'");
            }
            else if (USER_TYPE.Equals("L") || USER_TYPE.Equals("K") || USER_TYPE.Equals("D") || USER_TYPE.Equals("H") || USER_TYPE.Equals("M") || USER_TYPE.Equals("S"))    //chg-20230915
            {
                sb_query.Append("SELECT CCD_CODE ,CCD_DESCR DESC_F FROM CCD_CHANNELDETAIL WHERE CCH_CODE='2' and CCD_CODE ='9'");
            }
            else
            {
                sb_query.Append("SELECT CD.CCD_CODE,CD.CCD_DESCR DESC_F  ");
                sb_query.Append("FROM LUCH_USERCHANNEL UC, CCH_CHANNEL CH, CCD_CHANNELDETAIL CD, CCS_CHANLSUBDETL CS ");
                //			sb_query.Append("WHERE UPPER(USE_USERID)=SV(\"s_USE_USERID\") AND ");
                sb_query.Append("WHERE USE_USERID=SV(\"s_USE_USERID\") AND ");
                sb_query.Append("UC.CCH_CODE=CS.CCH_CODE AND UC.CCD_CODE=CS.CCD_CODE AND UC.CCS_CODE=CS.CCS_CODE AND ");
                sb_query.Append("CS.CCH_CODE=CD.CCH_CODE AND CS.CCD_CODE=CD.CCD_CODE AND ");
                sb_query.Append("CD.CCH_CODE=CH.CCH_CODE ");
            }

            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
        }

        //</method>

        //<method><method-name>GetDDL_ILUS_ET_UC_USERCHANNEL_CCH_CODE_RO</method-name><method-signature>
        public static IDataReader GetUserDefualtChannel()
        {
            string concat = SHMA.Enterprise.Data.PortableSQL.getConcateOperator();

            StringBuilder sb_query = new StringBuilder(176);//to do we have to Optimize it too.
            sb_query.Append("SELECT UC.CCH_CODE" + concat + "'~'" + concat + "UC.CCD_CODE" + concat + "'~'" + concat + "UC.CCS_CODE CCS_CODE ");
            sb_query.Append("FROM LUCH_USERCHANNEL UC WHERE USE_USERID=SV(\"s_USE_USERID\") AND UCH_DEFAULT='Y'");

            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();

        }
        //</method>

        //<method><method-name>GetILUS_ET_UC_USERCHANNEL_Data</method-name><method-signature>
        public static IDataReader GetUserChannel()
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(205);//to do we have to Optimize it too.
            sb_query.Append("SELECT CCH_CODE,CCD_CODE,CCS_CODE,UCH_DEFAULT,USE_USERID FROM LUCH_USERCHANNEL WHERE ((USE_USERID=SV(\"USE_USERID\"))  )   ");
            String query = EnvHelper.Parse(sb_query.ToString());

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();

            //</method-body>
        }
        //</method>


        //<method><method-name>Exists</method-name><method-signature>
        public static Boolean Exists(NameValueCollection pkNameValue)
        {
            //</method-signature><method-body>
            String strQuery = "SELECT count(*) FROM LUCH_USERCHANNEL WHERE CCH_CODE=? AND USE_USERID=? AND CCD_CODE=? AND CCS_CODE=? ";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE", DbType.String, 15, pkNameValue["CCH_CODE"]));
            myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID", DbType.String, 15, pkNameValue["USE_USERID"]));
            myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE", DbType.String, 15, pkNameValue["CCD_CODE"]));
            myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE", DbType.String, 15, pkNameValue["CCS_CODE"]));
            int noOfRecords = (int)myCommand.ExecuteScalar();
            return (noOfRecords >= 1);
            //</method-body>
        }
        //</method>

        //<method><method-name>getAll_RO</method-name><method-signature>
        public static IDataReader getAll_RO()
        {
            //</method-signature><method-body>
            const String strQuery = "SELECT * FROM LUCH_USERCHANNEL";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>FindByPK</method-name><method-signature>
        public DataHolder FindByPK(string CCH_CODE, string USE_USERID, string CCD_CODE, string CCS_CODE)
        {
            //</method-signature><method-body>
            String strQuery = "SELECT * FROM LUCH_USERCHANNEL WHERE CCH_CODE=? AND USE_USERID=? AND CCD_CODE=? AND CCS_CODE=? ";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE", DbType.String, 15, CCH_CODE));
            myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID", DbType.String, 15, USE_USERID));
            myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE", DbType.String, 15, CCD_CODE));
            myCommand.Parameters.Add(DB.CreateParameter("@CCS_CODE", DbType.String, 15, CCS_CODE));

            this.Holder.FillData(myCommand, "LUCH_USERCHANNEL"); return this.Holder;
            //</method-body>
        }
        //</method>

    }
}
