using System;
using System.Data;
using System.Text;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Data {
	/// <summary>
	/// Summary description for PeriodTypeDB.
	/// </summary>
	public class PeriodTypeDB:SHMA.Enterprise.DataGateway{
		public PeriodTypeDB(DataHolder dh):base(dh){}

		public override String TableName{
			get {return "PR_GN_PD_PERIOD";}
		}
	
		public static string[] GetPeriodNo(string orgaCode,DateTime periodDate, string periodType){
			string[] rtrnValues=new String[2]; 
			StringBuilder strQuery = new StringBuilder();
			strQuery.Append(" SELECT PFS_ACNTYEAR, PPD_PERDNO FROM PR_GN_PD_PERIOD ");
			strQuery.Append(" WHERE ? BETWEEN PPD_PERDDATEFROM AND PPD_PERDDATETO ");
			strQuery.Append(" AND POR_ORGACODE = ? ");
			strQuery.Append(" AND PPT_PTYPCODE = ? ");
			strQuery.Append(" AND (PPD_PERDACTIVE IS NULL OR PPD_PERDACTIVE <> 'N' )");
			IDbCommand myCommand = DB.CreateCommand(strQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@PERIODDATE", DbType.DateTime, periodDate));
			myCommand.Parameters.Add(DB.CreateParameter("@POR_ORGACODE", DbType.String, orgaCode));
			myCommand.Parameters.Add(DB.CreateParameter("@PPT_PTYPCODE", DbType.String, periodType));
			IDataReader keyReader = myCommand.ExecuteReader();
			while (keyReader.Read()){
				rtrnValues[0] = Convert.ToString(keyReader[0]);
				rtrnValues[1] = Convert.ToString(keyReader[1]);
			}
			if(!keyReader.IsClosed)
				keyReader.Close();
			return rtrnValues;

		}

	}
}
