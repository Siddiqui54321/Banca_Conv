using System;
using System.Data;
using System.Text;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for ColumnTransformDB.
	/// </summary>
	public class ColumnTransformDB
	{
		private ColumnTransformDB()
		{
		}

		public static DataTable GetColumnTranformationData(string entityId){
			StringBuilder sbQuery = new StringBuilder(" SELECT CT.*, EC.PEC_COLFIELDID  From PR_ED_CT_COLUMNTRANSFORM CT LEFT OUTER JOIN PR_ED_EC_ENTITYCOLUMN EC ON");
			sbQuery.Append(" CT.PSE_ENTITYID = EC.PSE_ENTITYID AND ");
			sbQuery.Append(" CT.PEC_COLSRNO = EC.PEC_COLSRNO ");
			//sbQuery.Append(" WHERE    CT.PSE_ENTITYID = ? AND ISNULL(CT.CTR_ACTIVE,'1') = '1' ");
			sbQuery.Append(" WHERE    CT.PSE_ENTITYID = ? AND (Case CT.CTR_ACTIVE  when null then '1' when '' then '1' else CT.CTR_ACTIVE end ) = '1' ");
			sbQuery.Append(" Order By PEC_COLFIELDID ");

			IDbCommand myCommand = DB.CreateCommand(sbQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityId));
			System.Data.Common.DbDataAdapter da = DB.CreateDataAdapter(myCommand);
			DataTable myTable=new DataTable();
			da.Fill(myTable);
			return myTable;
		}

		public static DataTable GetDistinctColumn(string entityId){
			StringBuilder sbQuery = new StringBuilder(" SELECT DISTINCT EC.PEC_COLFIELDID From PR_ED_CT_COLUMNTRANSFORM CT INNER JOIN PR_ED_EC_ENTITYCOLUMN EC ON ");
			sbQuery.Append(" CT.PSE_ENTITYID = EC.PSE_ENTITYID AND ");
			sbQuery.Append(" CT.PEC_COLSRNO = EC.PEC_COLSRNO ");
			//sbQuery.Append(" WHERE     CT.PSE_ENTITYID = ? AND ISNULL(CT.CTR_ACTIVE,'1') = '1' ");
			sbQuery.Append(" WHERE     CT.PSE_ENTITYID = ? AND (Case CT.CTR_ACTIVE  when null then '1' when '' then '1' else CT.CTR_ACTIVE end ) = '1' ");
			
			sbQuery.Append(" Order By EC.PEC_COLFIELDID ");

			IDbCommand myCommand = DB.CreateCommand(sbQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityId));
			System.Data.Common.DbDataAdapter da = DB.CreateDataAdapter(myCommand);
			DataTable myTable=new DataTable();
			da.Fill(myTable);
			return myTable;
		}

		public static int updateLastNumber(string entityId, string pec_colsrno, string ctr_colsrno,string newNumber){
			StringBuilder sbQuery = new StringBuilder(" UPDATE PR_ED_CT_COLUMNTRANSFORM ");
			sbQuery.Append(" SET CTR_LASTNO = ? ");
			sbQuery.Append(" WHERE  PSE_ENTITYID = ? AND PEC_COLSRNO = ? AND CTR_COLSRNO = ? ");

			IDbCommand myCommand = DB.CreateCommand(sbQuery.ToString());
			myCommand.Parameters.Add(DB.CreateParameter("@CTR_LASTNO", DbType.String, newNumber));
			myCommand.Parameters.Add(DB.CreateParameter("@PSE_ENTITYID", DbType.String, entityId));
			myCommand.Parameters.Add(DB.CreateParameter("@PEC_COLSRNO", DbType.String, pec_colsrno));
			myCommand.Parameters.Add(DB.CreateParameter("@CTR_COLSRNO", DbType.String, ctr_colsrno));
			int affectedRows = myCommand.ExecuteNonQuery();
			return(affectedRows);
		}

	}
}
