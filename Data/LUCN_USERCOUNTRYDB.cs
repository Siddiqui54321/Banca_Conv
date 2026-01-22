using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class LUCN_USERCOUNTRYDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LUCN_USERCOUNTRYDB        (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LUCN_USERCOUNTRY";}
			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LUCN_USERCOUNTRY WHERE CCN_CTRYCD=? AND USE_USERID=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 10, pkNameValue["CCN_CTRYCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID",DbType.String, 15, pkNameValue["USE_USERID"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_UC_USERCOUNTRY2_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_UC_USERCOUNTRY2_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(207);//to do we have to Optimize it too.
sb_query.Append("SELECT CCN_CTRYCD,UCN_DEFAULT,USE_USERID FROM LUCN_USERCOUNTRY WHERE ((USE_USERID=SV(\"USE_USERID\"))  )   ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LUCN_USERCOUNTRY");
		return this.Holder;
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_UC_USERCOUNTRY_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_UC_USERCOUNTRY_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(153);//to do we have to Optimize it too.
sb_query.Append("SELECT CCN_CTRYCD,UCN_DEFAULT FROM LUCN_USERCOUNTRY where USE_USERID = SV(\"USE_USERCODE\") ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LUCN_USERCOUNTRY");
		return this.Holder;
								//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LUCN_USERCOUNTRY";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
											//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CCN_CTRYCD,string USE_USERID)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LUCN_USERCOUNTRY WHERE CCN_CTRYCD=? AND USE_USERID=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@CCN_CTRYCD",DbType.String, 10, CCN_CTRYCD));
myCommand.Parameters.Add(DB.CreateParameter("@USE_USERID",DbType.String, 15, USE_USERID));

this.Holder.FillData(myCommand, "LUCN_USERCOUNTRY");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
