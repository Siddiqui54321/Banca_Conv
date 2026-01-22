using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCAD_ADJUSTMENTSDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCAD_ADJUSTMENTSDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCAD_ADJUSTMENTS";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCAD_ADJUSTMENTS";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}



	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT count(*) FROM LCAD_ADJUSTMENTS WHERE CCL_CATEGORYCD=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CCL_CATEGORYCD",DbType.String, 3, pkNameValue["CCL_CATEGORYCD"]));
		int noOfRecords=(int)myCommand.ExecuteScalar();
		return(noOfRecords>=1);
		//</method-body>
	}
	//</method>

	
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCAD_ADJUSTMENTS ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CAD_TYPE, string CAD_SUBTYPE)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT * FROM LCAD_ADJUSTMENTS WHERE CCL_CATEGORYCD=? AND CAD_SUBTYPE = ? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CAD_TYPE",DbType.String, 3, CAD_TYPE));
		myCommand.Parameters.Add(DB.CreateParameter("@CAD_SUBTYPE",DbType.String, 1, CAD_SUBTYPE));

		this.Holder.FillData(myCommand, "LCAD_ADJUSTMENTS");
		return this.Holder;
		//</method-body>
	}
	//</method>

	public static IDataReader GetDDL_MANUALADJUSTMENT_CAD_TYPE_RO()
	{
		StringBuilder sb_query=new StringBuilder(217);
		sb_query.Append("SELECT CAD_TYPE NLO_TYPE, CAD_TYPE ");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("CAD_DESCRIPTION DESC_F FROM LCAD_ADJUSTMENTS WHERE CAD_TYPE IN  ");
		string Val = ace.clsIlasUtility.getListFromSysDetail("APPBH","MANADJUSTMENT", false);
		if (Val.Length<4){
		sb_query.Append("('')");
		}
		else {
			sb_query.Append(Val);}
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
	}

}
}
