using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHAB.Data{
public class PEX_EXGRATEDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public PEX_EXGRATEDB             (DataHolder dh):base(dh)
		
	
	
	
	
	
	
	
	
	
	
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "PEX_EXGRATE";}
			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetILUS_ET_TE_ER_EXCHANGERATE_Data</method-name><method-signature>
	public DataHolder GetILUS_ET_TE_ER_EXCHANGERATE_Data()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(285);//to do we have to Optimize it too.
sb_query.Append("SELECT PCU_BASECURR,PCU_CURRCODE,PET_EXRATETYPE,PEX_VALUEDAT,PEX_SERIAL,PEX_RATE FROM PEX_EXGRATE WHERE PCU_BASECURR=SVN(\"PCU_BASECURR\")  ");
String query = EnvHelper.Parse(sb_query.ToString());
		IDbCommand myCommand = DB.CreateCommand(query);
		this.Holder.FillData(myCommand, "PEX_EXGRATE");
		return this.Holder;
			//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
	//</method-signature><method-body>
String strQuery = "SELECT count(*) FROM PEX_EXGRATE WHERE PCU_BASECURR=? AND PCU_CURRCODE=? AND PET_EXRATETYPE=? AND PEX_VALUEDAT=? AND PEX_SERIAL=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PCU_BASECURR",DbType.String, 10, pkNameValue["PCU_BASECURR"]));
myCommand.Parameters.Add(DB.CreateParameter("@PCU_CURRCODE",DbType.String, 10, pkNameValue["PCU_CURRCODE"]));
myCommand.Parameters.Add(DB.CreateParameter("@PET_EXRATETYPE",DbType.String, 10, pkNameValue["PET_EXRATETYPE"]));
myCommand.Parameters.Add(DB.CreateParameter("@PEX_VALUEDAT",DbType.DateTime, pkNameValue["PEX_VALUEDAT"]));
myCommand.Parameters.Add(DB.CreateParameter("@PEX_SERIAL",DbType.String, 5, pkNameValue["PEX_SERIAL"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM PEX_EXGRATE";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
																//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string PCU_BASECURR,string PCU_CURRCODE,string PET_EXRATETYPE,string PEX_SERIAL,DateTime PEX_VALUEDAT)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM PEX_EXGRATE WHERE PCU_BASECURR=? AND PCU_CURRCODE=? AND PET_EXRATETYPE=? AND PEX_VALUEDAT=? AND PEX_SERIAL=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@PCU_BASECURR",DbType.String, 10, PCU_BASECURR));
myCommand.Parameters.Add(DB.CreateParameter("@PCU_CURRCODE",DbType.String, 10, PCU_CURRCODE));
myCommand.Parameters.Add(DB.CreateParameter("@PET_EXRATETYPE",DbType.String, 10, PET_EXRATETYPE));
myCommand.Parameters.Add(DB.CreateParameter("@PEX_VALUEDAT",DbType.DateTime, PEX_VALUEDAT));
myCommand.Parameters.Add(DB.CreateParameter("@PEX_SERIAL",DbType.String, 5, PEX_SERIAL));

this.Holder.FillData(myCommand, "PEX_EXGRATE");return this.Holder;
	//</method-body>
	}
	//</method>

}
}
