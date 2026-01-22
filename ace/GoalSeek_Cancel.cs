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
	
	public class GoalSeek_Cancel:shgn.ProcessCommand
	{
		EnvHelper env = new EnvHelper();
		SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
		String delt = "N";

		public override System.String processing()
		{
			String strNP1_RETIREMENTAGE = ""+SessionObject.Get("NP1_RETIREMENTAGE");
			String strExcessPrem = "Select count(1) cnt FROM LNLO_LOADING_ACTUAL WHERE np1_proposal = '"+SessionObject.Get("NP1_PROPOSAL").ToString()+"'" ;
			rowset rstExcessPrem = DB.executeQuery( strExcessPrem );
			if ( rstExcessPrem.next() )
			{
				if (rstExcessPrem.getInt("CNT") > 0 )
                	delt = "Y";
			}

			if (delt.Equals("Y"))
			{
				DB.executeDML("DELETE FROM LNLO_LOADING WHERE np1_proposal='"+SessionObject.Get("NP1_PROPOSAL").ToString()+"'");
				DB.executeDML("INSERT INTO LNLO_LOADING SELECT * FROM LNLO_LOADING_ACTUAL WHERE np1_proposal='"+SessionObject.Get("NP1_PROPOSAL").ToString()+"'");
			}
			DB.executeDML("update LNP1_POLICYMASTR set NP1_RETIREMENTAGE="+SessionObject.Get("NP1_RETIREMENTAGE")+", NP1_TARGETATTAINAGE="+SessionObject.Get("NP1_TARGETATTAINAGE")+", NP1_TARGETRETURNYEAR="+SessionObject.Get("NP1_TARGETRETURNYEAR")+" WHERE np1_proposal='"+SessionObject.Get("NP1_PROPOSAL").ToString()+"'");

			ProcedureAdapter call =  new ProcedureAdapter("TargetValues");
			call.Set("P_PROPOSAL",OleDbType.VarChar, 50, Convert.ToString(SessionObject.Get("NP1_PROPOSAL")));
			call.Set("P_SETNO",   OleDbType.Numeric, "1");
			call.Execute();

			return "";
		}
	}
}
