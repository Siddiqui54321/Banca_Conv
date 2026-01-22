using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class LNFU_FUNDSDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LNFU_FUNDSDB (DataHolder dh):base(dh)
		
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LNFU_FUNDS";}
			//			//</property-body>
		}
		//</property>
	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM LNFU_FUNDS WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? AND CFU_FUNDCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO",DbType.String, 3, pkNameValue["NP2_SETNO"]));
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, pkNameValue["PPR_PRODCD"]));
myCommand.Parameters.Add(DB.CreateParameter("@CFU_FUNDCODE",DbType.String, 3, pkNameValue["CFU_FUNDCODE"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LNFU_FUNDS";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
				//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_LF_LNFUFUNDS_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_LF_LNFUFUNDS_Data()
	{
	//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(349);//to do we have to Optimize it too.
		//sb_query.Append("SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CFU_FUNDCODE,NFU_RATE FROM LNFU_FUNDS WHERE ((NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND NP2_SETNO=SV(\"NP2_SETNO\") AND PPR_PRODCD=SV(\"PPR_PRODCD\")) )  AND   NP1_PROPOSAL=SV(\"NP1_PROPOSAL\")  AND NP2_SETNO=1 AND CFU_FUNDCODE IN (SELECT CSD_TYPE FROM LCSD_SYSTEMDTL WHERE CSH_ID='FUNDS' AND CSD_STATUS='Y' ) ");
		//sb_query.Append("SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CFU_FUNDCODE,NFU_RATE FROM LNFU_FUNDS WHERE ((NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND NP2_SETNO=SV(\"NP2_SETNO\") AND PPR_PRODCD=SV(\"PPR_PRODCD\")) )  AND   NP1_PROPOSAL=SV(\"NP1_PROPOSAL\")  AND NP2_SETNO=1  ");
		sb_query.Append("SELECT NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CFU_FUNDCODE,NFU_RATE FROM LNFU_FUNDS WHERE ((NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") AND NP2_SETNO=SV(\"NP2_SETNO\") AND PPR_PRODCD=SV(\"PPR_PRODCD\")) )  ");
		String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "LNFU_FUNDS");
		return this.Holder;
			//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CFU_FUNDCODE,string NP1_PROPOSAL,string NP2_SETNO,string PPR_PRODCD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LNFU_FUNDS WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? AND CFU_FUNDCODE=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
myCommand.Parameters.Add(DB.CreateParameter("@NP2_SETNO",DbType.String, 3, NP2_SETNO));
myCommand.Parameters.Add(DB.CreateParameter("@PPR_PRODCD",DbType.String, 3, PPR_PRODCD));
myCommand.Parameters.Add(DB.CreateParameter("@CFU_FUNDCODE",DbType.String, 3, CFU_FUNDCODE));

this.Holder.FillData(myCommand, "LNFU_FUNDS");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
