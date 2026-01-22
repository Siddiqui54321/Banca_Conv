using System;
using shgn;
using SHSM_SecurityFeatures=shsm.security.SHSM_SecurityFeatures;
using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;

namespace shsm
{
	public class SHSM_RejectTransaction:ProcessCommand
	{
		public SHSM_RejectTransaction()
		{
			
		}
		public override string processing()
		{
			//--New Feature - Parametric Security 3 - start
			if (!(SHSM_SecurityFeatures.TRANSACTION_ROUTING))
			{
				return "Routing Feature is not available.";
			}
			//--New Feature - Parametric Security 3 - end

			if (getAllFields()==null)				
				throw new ApplicationException("Security Module: \\n" + "Process Error!");

			//MC0033-6 START 29/10/2005
			//String str_msg = SHSM_TransactionRouter.fsrouteRejection(getEntityID(), getPrimaryKeys(), getAllFields(),	getTableName());
			//String str_msg = SHSM_TransactionRouter.fsrouteRejection(getEntityID(), getPrimaryKeys(), getAllFields(),	getTableName(),"");
			//MC0033-6 END 29/10/2005

			/*------- Routing Remarks - start ------*/
			EnvHelper sessionValues = new EnvHelper();
			String str_routingRemarks = (String) sessionValues.getAttribute("s_ROUTINGREMARKS");
			if(str_routingRemarks==null)
			{
				str_routingRemarks="";
			}
			String str_msg = SHSM_TransactionRouter.fsrouteRejection(getEntityID(), getPrimaryKeys(), getAllFields(),	getTableName(),str_routingRemarks);
			/*------- Routing Remarks - end ------*/


			return SHSM_TransactionRouter.fsgetMessageFromLevelCode(str_msg);
		}
	}
}
