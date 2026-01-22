using System;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHSM_SecurityFeatures=shsm.security.SHSM_SecurityFeatures;
using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;

namespace shsm
{
	
	public class SHSM_VerifyTransaction: shgn.ProcessCommand
	{

		//private NameValueCollection allFormFields;
		//		private string entiityId;

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
			//String str_msg = SHSM_TransactionRouter.fsrouteTransaction( getEntityID() , getPrimaryKeys(), getAllFields(), SHSM_AuditTrail.DML_OPERATION_VERIFY ,  getTableName(), "shab.TransactionRouting");
			//String str_msg = SHSM_TransactionRouter.fsrouteTransaction( getEntityID() , getPrimaryKeys(), getAllFields(), SHSM_AuditTrail.DML_OPERATION_VERIFY ,  getTableName(), "shab.TransactionRouting","");
			//MC0033-6 END 29/10/2005  

			/*------- Routing Remarks - start ------*/
			EnvHelper sessionValues = new EnvHelper();
			String str_routingRemarks = (String) sessionValues.getAttribute("s_ROUTINGREMARKS");
			if(str_routingRemarks==null)
			{
				str_routingRemarks="";
			}
			String str_msg = SHSM_TransactionRouter.fsrouteTransaction( getEntityID() , getPrimaryKeys(), getAllFields(), SHSM_AuditTrail.DML_OPERATION_VERIFY ,  getTableName(), "shab.TransactionRouting",str_routingRemarks);
			/*------- Routing Remarks - end ------*/
						
			
			return SHSM_TransactionRouter.fsgetMessageFromLevelCode(str_msg);
		}		

		//		public override setNameValue(NameValueCollection allFields){
		//			allFormFields = allFields;
		//		}
		//		public override setEntiityId(string eId){
		//			entiityId = eId;
		//		}
	}
}