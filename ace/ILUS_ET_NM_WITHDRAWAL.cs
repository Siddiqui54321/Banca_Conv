using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;


namespace ace
{
	public class ILUS_ET_NM_WITHDRAWAL:shgn.SHGNCommand
	{
		
		EnvHelper env = new EnvHelper();

		public override void fsoperationAfterSave()
		{
			fsperformbusiness_accumulatePW();
		}

		public override void fsoperationAfterUpdate()
		{
			fsperformbusiness_accumulatePW();
		}

		public override void fsoperationAfterDelete()
		{
			fsperformbusiness_accumulatePW();
		}

		private void fsperformbusiness_accumulatePW()
		{
			DB.executeDML("update LNPW_PARTIALWITHDRAWAL set  NPW_CUMWITHDRAWAL =  "
				+ " (SELECT (SELECT SUM(NPW_PW) FROM LNPW_PARTIALWITHDRAWAL WHERE NP1_PROPOSAL = A.NP1_PROPOSAL AND NPW_YEAR <= A.NPW_YEAR) RUNNING  "
				+ " FROM LNPW_PARTIALWITHDRAWAL A WHERE A.NP1_PROPOSAL =LNPW_PARTIALWITHDRAWAL.NP1_PROPOSAL AND a.NPW_YEAR=LNPW_PARTIALWITHDRAWAL.NPW_YEAR)  " 
				+ " WHERE NP1_PROPOSAL='"+this.get("NP1_PROPOSAL")+"'");
		}
	}
}