using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class LNTP_PARTIALWITHDRAWALDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNTP_PARTIALWITHDRAWALDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNTP_PARTIALWITHDRAWAL";}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_TB_WITHDRAWAL_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_TB_WITHDRAWAL_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(309);//to do we have to Optimize it too.
sb_query.Append("SELECT NP1_PROPOSAL,NTP_COLCODE,NTP_COL1,NTP_COL2,NTP_COL3 FROM LNTP_PARTIALWITHDRAWAL  WHERE NP1_PROPOSAL =SV(\"NP1_PROPOSAL\")  ORDER BY NTP_COLCODE ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNTP_PARTIALWITHDRAWAL");
		return this.Holder;
			//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LNTP_PARTIALWITHDRAWAL WHERE NP1_PROPOSAL=? AND NTP_COLCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 0, pkNameValue["NP1_PROPOSAL"]));
myCommand.Parameters.Add(DB.CreateParameter("@NTP_COLCODE",DbType.String, 10, pkNameValue["NTP_COLCODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LNTP_PARTIALWITHDRAWAL";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string NP1_PROPOSAL,string NTP_COLCODE)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LNTP_PARTIALWITHDRAWAL WHERE NP1_PROPOSAL=? AND NTP_COLCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 0, NP1_PROPOSAL));
myCommand.Parameters.Add(DB.CreateParameter("@NTP_COLCODE",DbType.String, 10, NTP_COLCODE));

this.Holder.FillData(myCommand, "LNTP_PARTIALWITHDRAWAL");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
