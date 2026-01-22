using System;
using System.Data;
using System.Text;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Data {
	/// <summary>
	/// Summary description for KeyNumGenFrequencyDB.
	/// </summary>
	public class KeyNumGenFrequencyDB:SHMA.Enterprise.DataGateway{
		public KeyNumGenFrequencyDB(DataHolder dh):base(dh){}

		public override String TableName{
			get {return "PR_ED_NF_NUMGENFREQUECY";}
		}

		public static String CheckPeriodNumber(string orgaCode, string entityID, double levelNo, double srno, string acntyear, string periodType, double periodNo){
			string rtrnValue="";
			StringBuilder strQuery = new StringBuilder();
			strQuery.Append(" SELECT PNF_LASTNUMBER FROM PR_ED_NF_NUMGENFREQUENCY ");
			strQuery.Append(" WHERE POR_ORGACODE = ? ");
			strQuery.Append(" AND PSE_ENTITYID = ? ");
			strQuery.Append(" AND PKL_LVLNO = ? ");
			strQuery.Append(" AND PKN_SRNO = ? ");
			strQuery.Append(" AND PFS_ACNTYEAR = ? ");
			strQuery.Append(" AND PPT_PTYPCODE = ? ");
			strQuery.Append(" AND PPD_PERDNO = ? ");
			IDbCommand myCommand = DB.CreateCommand(strQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, orgaCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityID));
			myCommand.Parameters.Add(DB.CreateParameter("@PKL_LVLNO", DbType.Double, levelNo));
			myCommand.Parameters.Add(DB.CreateParameter("@PKN_SRNO", DbType.Double, srno));
			myCommand.Parameters.Add(DB.CreateParameter("@PFS_ACNTYEAR", DbType.String, acntyear));
			myCommand.Parameters.Add(DB.CreateParameter("@PPT_PTYPCODE", DbType.String, periodType));
			myCommand.Parameters.Add(DB.CreateParameter("@PPD_PERDNO", DbType.Double, periodNo));
			IDataReader keyReader = myCommand.ExecuteReader();
			if (keyReader.Read()){
				rtrnValue = keyReader.GetValue(0).ToString();
			}
			keyReader.Close();
			return rtrnValue;
		}

		public static bool UpdateLastNumber(string newNumber, string lastNumber, string orgaCode, string entityID, double levelNo, double srno, string acntyear, string periodType, double periodNo){
			StringBuilder strQuery = new StringBuilder();
			strQuery.Append(" UPDATE PR_ED_NF_NUMGENFREQUENCY set PNF_LASTNUMBER = ? ");
			strQuery.Append(" WHERE POR_ORGACODE = ? ");
			strQuery.Append(" AND PSE_ENTITYID = ? ");
			strQuery.Append(" AND PKL_LVLNO = ? ");
			strQuery.Append(" AND PKN_SRNO = ? ");
			strQuery.Append(" AND PFS_ACNTYEAR = ? ");
			strQuery.Append(" AND PPT_PTYPCODE = ? ");
			strQuery.Append(" AND PPD_PERDNO = ? ");
			strQuery.Append(" AND PNF_LASTNUMBER = ? ");
			IDbCommand myCommand = DB.CreateCommand(strQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@PNF_LASTNUMBER", DbType.String, newNumber));
			myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, orgaCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityID));
			myCommand.Parameters.Add(DB.CreateParameter("@PKL_LVLNO", DbType.Double, levelNo));
			myCommand.Parameters.Add(DB.CreateParameter("@PKN_SRNO", DbType.Double, srno));
			myCommand.Parameters.Add(DB.CreateParameter("@PFS_ACNTYEAR", DbType.String, acntyear));
			myCommand.Parameters.Add(DB.CreateParameter("@PPT_PTYPCODE", DbType.String, periodType));
			myCommand.Parameters.Add(DB.CreateParameter("@PPD_PERDNO", DbType.Double, periodNo));
			myCommand.Parameters.Add(DB.CreateParameter("@PNF_LASTNUMBER", DbType.Double, lastNumber));
			if (myCommand.ExecuteNonQuery()>0)
				return true;
			else
				return false;
		}

		public static bool InsertLastNumber(string newNumber,string orgaCode, string entityID, double levelNo, double srno, string acntyear, string periodType, double periodNo){
			StringBuilder strQuery = new StringBuilder();
			strQuery.Append(" INSERT INTO PR_ED_NF_NUMGENFREQUENCY(PNF_LASTNUMBER,POR_ORGACODE,PSE_ENTITYID,PKL_LVLNO,PKN_SRNO,PFS_ACNTYEAR,PPT_PTYPCODE,PPD_PERDNO) ");
			strQuery.Append(" VALUES (?, ?, ?, ?, ?, ?, ?, ? ) ") ;
			IDbCommand myCommand = DB.CreateCommand(strQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@PNF_LASTNUMBER", DbType.String, newNumber));
			myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, orgaCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityID));
			myCommand.Parameters.Add(DB.CreateParameter("@PKL_LVLNO", DbType.Double, levelNo));
			myCommand.Parameters.Add(DB.CreateParameter("@PKN_SRNO", DbType.Double, srno));
			myCommand.Parameters.Add(DB.CreateParameter("@PFS_ACNTYEAR", DbType.String, acntyear));
			myCommand.Parameters.Add(DB.CreateParameter("@PPT_PTYPCODE", DbType.String, periodType));
			myCommand.Parameters.Add(DB.CreateParameter("@PPD_PERDNO", DbType.Double, periodNo));
			if (myCommand.ExecuteNonQuery()>0)
				return true;
			else
				return false;
		}
	}
}
