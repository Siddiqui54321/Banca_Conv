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
	
	public class Compute:shgn.ProcessCommand
	{
		EnvHelper env = new EnvHelper();
		SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

		public override System.String processing()
		{

			NameValueCollection[] nmRows = this.getDataRows();
			NameValueCollection nvc = nmRows[0];
			String strNP1_RETIREMENTAGE = ""+nvc.getObject("NP1_RETIREMENTAGE");

			SessionObject.Set("NP1_RETIREMENTAGE",  ""+nvc.getObject("NP1_RETIREMENTAGE"));  
			SessionObject.Set("NP1_TARGETATTAINAGE", ""+nvc.getObject("NP1_TARGETATTAINAGE"));  
			SessionObject.Set("NP1_TARGETRETURNYEAR", ""+nvc.getObject("NP1_TARGETRETURNYEAR")); 

			DB.executeDML("update LNP1_POLICYMASTR set NP1_RETIREMENTAGE="+nvc.getObject("NP1_RETIREMENTAGE")+", NP1_TARGETATTAINAGE="+nvc.getObject("NP1_TARGETATTAINAGE")+", NP1_TARGETRETURNYEAR="+nvc.getObject("NP1_TARGETRETURNYEAR")+" WHERE np1_proposal='"+nvc.getObject("NP1_PROPOSAL").ToString()+"'");

			ProcedureAdapter call =  new ProcedureAdapter("TargetValues");
			call.Set("P_PROPOSAL", OleDbType.VarChar, 50, nvc.getObject("NP1_PROPOSAL").ToString());
			call.Set("P_SETNO", OleDbType.Numeric, nvc.getObject("NP2_SETNO").ToString());
			call.Execute();
			return "";
		}
	}
}
