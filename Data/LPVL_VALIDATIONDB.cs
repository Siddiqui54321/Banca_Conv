using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data
{
	public class LPVL_VALIDATIONDB:SHMA.CodeVision.Data.DataClassBase
	{
		//<constructor>
		public LPVL_VALIDATIONDB  (DataHolder dh):base(dh)
		
	
		{	}
		//</constructor>
		//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
			get {return "LPVL_VALIDATION";}
			//			//			//</property-body>
		}
		//</property>
		//<method><method-name>Exists</method-name><method-signature>
		public static Boolean IsCallValidationExist(Object ProductID, Object callValidationFor/*, Object ValidRange*/)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT count(*) FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR='CALLVALIDATION'"+
								" AND PPR_PRODCD='"+ProductID.ToString()+"' AND PVL_VALIDRANGE='"+callValidationFor.ToString()+"' ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
 			int noOfRecords=int.Parse(myCommand.ExecuteScalar().ToString());
			return(noOfRecords>=1);
			//</method-body>
		}

		public static void AddCallValidation(Object ProductID, Object CallValidationFor)
		{
			string query = "insert into lpvl_validation (PPR_PRODCD, PVL_VALIDATIONFOR, PVL_LEVEL, PVL_AGEFROM, PVL_AGETO, PVL_TERMFROM, PVL_TERMTO, PVL_VALIDFROM, PVL_VALIDTO, PVL_VALIDRANGE, PVL_VALUECOMB)"+
				"values (?, 'CALLVALIDATION', "+
				"(select max(pvl_level)+1 from lpvl_validation where (PPR_PRODCD='"+ProductID.ToString()+"'"+
				" AND PVL_VALIDATIONFOR='CALLVALIDATION'))"+
				", null, null, null, null, '', '', ?, '')";

			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.clear();
			pc.puts("@PPR_PRODCD",ProductID);
			pc.puts("@PVL_VALIDRANGE",CallValidationFor);
			DB.executeDML(query,pc);
		}

		public static void RemoveCallValidation(Object ProductID, Object CallValidationFor)
		{
			string query = "DELETE FROM lpvl_validation "+
				"WHERE (PPR_PRODCD=? AND PVL_VALIDATIONFOR='CALLVALIDATION' AND PVL_VALIDRANGE=?)";

			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.clear();
			pc.puts("@PPR_PRODCD",ProductID);
			pc.puts("@PVL_VALIDRANGE",CallValidationFor);
			DB.executeDML(query,pc);
		}


		//</property>
		//<method><method-name>Exists</method-name><method-signature>
		public static Boolean Exists(NameValueCollection pkNameValue)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT count(*) FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR=? AND PPR_PRODCD=? AND PVL_LEVEL=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@PVL_VALIDATIONFOR",DbType.String, 15, pkNameValue["PVL_VALIDATIONFOR"]));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 10, pkNameValue["PPR_PRODCD"]));
			myCommand.Parameters.Add(DB.CreateParameter("@PVL_LEVEL",DbType.Decimal, 5, pkNameValue["PVL_LEVEL"]));
			int noOfRecords=(int)myCommand.ExecuteScalar();
			return(noOfRecords>=1);
			//</method-body>
		}
		//</method>

		//<method><method-name>GetILUS_TE_UL_LIMITATION_Data</method-name><method-signature>
		public DataHolder GetILUS_TE_UL_LIMITATION_Data()
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(283);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_VALIDATIONFOR,PPR_PRODCD,PVL_LEVEL,PVL_AGEFROM,PVL_AGETO,PVL_TERMFROM,PVL_TERMTO,PVL_VALIDFROM,PVL_VALIDTO FROM LPVL_VALIDATION WHERE ((PPR_PRODCD=SVN(\"PPR_PRODCD\"))  )   ");
			String query = EnvHelper.Parse(sb_query.ToString());
			IDbCommand myCommand = DB.CreateCommand(query);
			this.Holder.FillData(myCommand, "LPVL_VALIDATION");
			return this.Holder;
			//</method-body>
		}
		//</method>

		//<method><method-name>getAll_RO</method-name><method-signature>
		public static IDataReader getAll_RO()
		{
			//</method-signature><method-body>
			const String strQuery = "SELECT * FROM LPVL_VALIDATION";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>FindByPK</method-name><method-signature>
		public DataHolder FindByPK(string PPR_PRODCD,double PVL_LEVEL,string PVL_VALIDATIONFOR)
		{
			//</method-signature><method-body>
			String strQuery = "SELECT * FROM LPVL_VALIDATION WHERE PVL_VALIDATIONFOR=? AND PPR_PRODCD=? AND PVL_LEVEL=? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@PVL_VALIDATIONFOR",DbType.String, 15, PVL_VALIDATIONFOR));
			myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 10, PPR_PRODCD));
			myCommand.Parameters.Add(DB.CreateParameter("@PVL_LEVEL",DbType.Decimal, 5, PVL_LEVEL));

			this.Holder.FillData(myCommand, "LPVL_VALIDATION");return this.Holder;
			//</method-body>
		}
		//</method>

		public static IDataReader GetLPVL_VALIDATION_VALIDATION_lister_RO(int offset, int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(264);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_VALIDATIONFOR,PVL_LEVEL,PVL_VALUECOMB,PPR_PRODCD FROM LPVL_VALIDATION WHERE ((PPR_PRODCD=SV(\"PPR_PRODCD_S\")) )  AND  PVL_VALIDATIONFOR=SV(\"PVF_CODE\")  ");
			string query = sb_query.ToString(); query = EnvHelper.Parse(query);

			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetLPVL_VALIDATION_DECISION_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetLPVL_VALIDATION_DECISION_lister_filter_RO(string columnName, string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(293);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_LEVEL,LCDC.CDC_DESCRIPTION,PPR_PRODCD,PVL_VALIDATIONFOR FROM LPVL_VALIDATION LPVL,  LCDC_DECISION LCDC  WHERE  ({0} like '{1}')  AND LPVL.PVL_VALIDRANGE = LCDC.CDC_CODE AND LPVL.PPR_PRODCD=SV(\"PPR_PRODCD\") AND PVL_VALIDATIONFOR='DECISION'  ");
			string query = sb_query.ToString(); query = EnvHelper.Parse(query);

			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>Exists</method-name><method-signature>
		
		//<method><method-name>GetLPVL_VALIDATION_VALIDATION_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetLPVL_VALIDATION_VALIDATION_lister_filter_RO(string columnName, string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(264);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_VALIDATIONFOR,PVL_LEVEL,PVL_VALUECOMB,PPR_PRODCD FROM LPVL_VALIDATION WHERE  ({0} like '{1}')  AND ((PPR_PRODCD=SV(\"PPR_PRODCD_S\")) )  AND  PVL_VALIDATIONFOR=SV(\"PVF_CODE\")  ");
			string query = sb_query.ToString(); query = EnvHelper.Parse(query);

			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetLPVL_VALIDATION_DECISION_lister_RO</method-name><method-signature>
		public static IDataReader GetLPVL_VALIDATION_DECISION_lister_RO(int offset, int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query = new StringBuilder(350);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_LEVEL,LCDC.CDC_DESCRIPTION,LPVL.PPR_PRODCD,P.PPR_DESCR,PVL_VALIDATIONFOR FROM LPVL_VALIDATION LPVL,  LCDC_DECISION LCDC ,LPPR_PRODUCT P WHERE LPVL.PVL_VALIDRANGE = LCDC.CDC_CODE AND LPVL.PPR_PRODCD=SV(\"PPR_PRODCD\") AND LPVL.PPR_PRODCD = P.PPR_PRODCD AND PVL_VALIDATIONFOR='DECISION'  ");
			string query = sb_query.ToString(); query = EnvHelper.Parse(query);

			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

		//<method><method-name>GetLPVL_RIDER_INFORMATION_lister_filter_RO</method-name><method-signature>
		public static IDataReader GetLPVL_RIDER_INFORMATION_lister_filter_RO(string columnName,string columnValue)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(265);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_LEVEL,PVL_VALIDRANGE,PPR_PRODCD,PVL_VALIDATIONFOR FROM LPVL_VALIDATION WHERE  ({0} like '{1}')  AND ((PPR_PRODCD=SV(\"PPR_PRODCD\")) )  AND  PVL_VALIDATIONFOR=SV(\"PVF_CODE\")  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			query = string.Format(query, columnName, columnValue);
			query = string.Format(query, columnName, columnValue);
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>
	
		//<method><method-name>GetLPVL_RIDER_INFORMATION_lister_RO</method-name><method-signature>
		public static IDataReader GetLPVL_RIDER_INFORMATION_lister_RO(int offset,int numRows)
		{
			//</method-signature><method-body>
			StringBuilder sb_query=new StringBuilder(265);//to do we have to Optimize it too.
			sb_query.Append("SELECT PVL_LEVEL,PVL_VALIDRANGE,PPR_PRODCD,PVL_VALIDATIONFOR FROM LPVL_VALIDATION WHERE ((PPR_PRODCD=SV(\"PPR_PRODCD\")) )  AND  PVL_VALIDATIONFOR=SV(\"PVF_CODE\")  ");
			string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
			//</method-body>
		}
		//</method>

	}
}
