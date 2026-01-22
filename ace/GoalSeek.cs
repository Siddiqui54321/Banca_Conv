using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;

using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;


namespace ace
{
	
	public class GoalSeek:shgn.ProcessCommand
	{
		EnvHelper env = new EnvHelper();
		SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
		double pdbl_Return;

		public override System.String processing()
		{

			//NameValueCollection[] nmRows = this.getDataRows();
			//NameValueCollection nvc = nmRows[0];
			String strNP1_RETIREMENTAGE = ""+SessionObject.Get("NP1_RETIREMENTAGE");
			String strNP1_TARGETATTAINAGE = ""+SessionObject.Get("NP1_TARGETATTAINAGE");  
			String strNP1_TARGETRETURNYEAR = ""+SessionObject.Get("NP1_TARGETRETURNYEAR"); 

			String NloType = "" ;
			String strQry = "select nlo_type from lnlo_loading where np1_proposal= '"+SessionObject.Get("NP1_PROPOSAL").ToString()+"' and substr(nlo_type,1,1)='"+SessionObject.Get("TARGETID")+"' and ROUND(nlo_amount)=ROUND("+SessionObject.Get("NLO_AMOUNT").ToString()+")" ;
			rowset rstQry = DB.executeQuery( strQry );
			if ( rstQry.next() )
			{
				NloType = rstQry.getString("NLO_TYPE");
			}

			
			
			DB.executeDML("update LNP1_POLICYMASTR set NP1_RETIREMENTAGE="+Convert.ToString(SessionObject.Get("NP1_RETIREMENTAGE"))+", NP1_TARGETATTAINAGE="+Convert.ToString(SessionObject.Get("NP1_TARGETATTAINAGE"))+", NP1_TARGETRETURNYEAR="+Convert.ToString(SessionObject.Get("NP1_TARGETRETURNYEAR"))+" WHERE np1_proposal='"+Convert.ToString(SessionObject.Get("NP1_PROPOSAL"))+"'");


			ProcedureAdapter call =  new ProcedureAdapter("Goal_Seek");
			call.Set("P_PROPOSAL",OleDbType.VarChar, 50, Convert.ToString(SessionObject.Get("NP1_PROPOSAL")));
			call.Set("P_UPTOAGE", OleDbType.Numeric, strNP1_RETIREMENTAGE);
			call.Set("P_TARGET",  OleDbType.Numeric, SessionObject.Get("TARGETVAL").ToString());
			call.Set("P_START",   OleDbType.Numeric, 1000);
			call.Set("P_TAG",     OleDbType.VarChar, 50, NloType);
			call.RegisetrOutParameter("P_RETURN",OleDbType.Numeric,15);
			call.Execute();
			pdbl_Return =  Double.Parse( call.Get("P_RETURN") == null ? "0.0" : call.Get("P_RETURN").ToString() );

			//call.Close();
			if (pdbl_Return > 0) 
				 return "Successfully Executed. {"+pdbl_Return.ToString()+"}";
			else
				return "Unsuccessfull. {0}";

		}
	}
}
