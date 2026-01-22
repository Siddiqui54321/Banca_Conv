/// <summary> </summary>
/// <author> 	Kashif Iqbal Khan
/// </author>
/// <date		07-12-2004>  </date		07-12-2004>
using System;
using ArrayList = System.Collections.ArrayList;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using DB = SHMA.Enterprise.Data.DB;
using rowset = SHMA.Enterprise.Data.rowset;
using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;
using RoutingController = SHMA.Enterprise.TransactionRouting.RoutingController;
using TransactionException = shsm.exceptions.TransactionException;
using DBException = shsm.exceptions.DBException;

using OleDbException = System.Data.OleDb.OleDbException;
using SQLException = System.Data.SqlClient.SqlException;

using InvalidDataException = System.Data.InvalidExpressionException;

using shsm.util;

using SHMA.Enterprise.Data;

namespace shsm
{
	
	
	public sealed class SHSM_TransactionRouter
	{
		internal const String OP_EQUALS = "01";
		internal const String OP_NOTEQUALS = "02";
		internal const String OP_LESSER = "03";
		internal const String OP_GREATER = "04";
		internal const String OP_LESSOREQUAL = "05";
		internal const String OP_GREATEROREQUAL = "06";
		internal const String OP_BETWEEN = "07";


		public const String LEVEL_APPROVAL_DIRECT = "D1";
		public const String LEVEL_APPROVAL = "A1";
		public const String LEVEL_APPROVED = "A2";
		public const String LEVEL_REJECTED = "R1";
		
		
		//		public const String STATE_BEFORE_ROUTE_FAILED = "SHTR_BR";
		//		public const String STATE_AFTER_ROUTE_FAILED = "SHTR_AR";

		public const String MSG_TR_INCOMPLETE = "Incomplete";
		public const String MSG_TR_VERIFICATION = "Forwarded for verification successfully";
		public const String MSG_TR_VERIFIED = "Forwarded for next verification successfully";
		public const String MSG_TR_REJECTED = "Rejected successfully";
		public const String MSG_TR_APPROVAL = "Forwarded for approval successfully";
		public const String MSG_TR_APPROVED = "Approved successfully";

		//this should be on the class level
		static int int_sdd_srno=0;

		//Point No 102 - Oracle Support - Start
		private static  ParameterCollection ParaColl = new ParameterCollection();


		/*------- Routing Remarks - start -------*/
		public static String fsgetAccumulatedRemarks(String str_appCode, String str_tableCode, String str_docRef,  String str_reason, String str_userCode, String str_entityID)
		{
			String str_lastRemarks="";
			String str_accRemarks="";

			//Picking routing style Old / New
			String[] obj_arrFields ={"STR_OLDROUTING"};
			Object[] obj_arrValues = null;
			
			obj_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_TR_TRANSROUTEENTITY", obj_arrFields, " WHERE PSE_ENTITYID='" + str_entityID + "'");	
			String str_oldRouting ="N";

			if(obj_arrValues[0]!=null)
			{
				str_oldRouting = obj_arrValues[0].ToString() ;
			}

			if (str_oldRouting != "Y") //New Routing
			{
				str_accRemarks="<<<<< " +str_userCode + " >>>>> \\n" + str_reason + "\\n";
			}

			String str_query = " SELECT SDL_REASON FROM SH_SM_DL_DOCUMENTLOG " 
				+ " WHERE SAA_APPCODE='"+ str_appCode + "' AND SAB_TABLECODE='"+ str_tableCode +"' AND SDL_DOCREF='" + str_docRef + "'" 
				+ " ORDER BY SDL_DATETIMEACTUAL DESC";

			rowset rs_remarks = null;

			try
			{
				//pick the last remarks
				rs_remarks = DB.executeQuery(str_query);
				if (rs_remarks != null && rs_remarks.next())
				{
					str_lastRemarks = rs_remarks.getString("SDL_REASON");
				}
				if (str_lastRemarks != null)
				{
					str_accRemarks=str_accRemarks + str_lastRemarks;
				}

			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error Executing query(Routing remarks)\nQurey: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				throw new ProcessException("Security Module: \\n" + "Error: Accumulate routing remarks. \n" + e.Message);
			}
			return str_accRemarks;
		}
		/*------- Routing Remarks - start -------*/

		/*------------------------------------------------------------------------*/
		/* --------------  Called From Data Entry Option -------------------------*/
		/*------------------------------------------------------------------------*/

		//MC0033-6 START 29/10/2005
		//public static String fsrouteTransaction(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, char chr_operation, String str_tableCode, String str_package)
		public static String fsrouteTransaction(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, char chr_operation, String str_tableCode, String str_package, String str_reason)
		{
			//MC0033-6 END 29/10/2005
			// str_Reason should be use after framework support

			String str_level = null;
			String str_query = "SELECT * FROM SH_SM_TR_TRANSROUTEENTITY WHERE PSE_ENTITYID='" + str_entityId + "'";

			bool bln_route = false; 
			bool bln_beforeRoute = false;
			bool bln_afterRoute = false;
			bool bln_oldRouting = false;
			
			String str_tableToUpdate = null;
			
			String str_routingTable = str_tableCode;

			String[] str_arrKeysOfUpdateTable = null;



			EnvHelper sessionValues = new EnvHelper();
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			if (str_appCode == null || str_appCode.Length==0)
				throw new ProcessException("Security Module: \\n"  + "Your session has expired. Login again.");
			
			
			rowset rs_entity = null;

			//	Point No 99 - Change in OldRouting Logic (M - Mixed)
			String str_oldRoutingFlag="";

			try
			{
				rs_entity = DB.executeQuery(str_query);
				if (rs_entity != null && rs_entity.next())
				{
					bln_route = true;
					
					String str_value = rs_entity.getString("STR_BEFOREROUTE");
					if (str_value != null && str_value.Length >0 && str_value[0] == 'Y')					
						bln_beforeRoute = true;


					str_value = rs_entity.getString("STR_AFTERROUTE");
					if (str_value != null && str_value.Length >0  && str_value[0] == 'Y')					
						bln_afterRoute = true;

					str_value = rs_entity.getString("STR_OLDROUTING");
					
					//	Point No 99 - Change in OldRouting Logic (M - Mixed)
					//if (str_value != null && str_value.Length >0  && str_value[0] == 'Y')					
					if (str_value != null && str_value.Length >0  && ( str_value[0] == 'Y' || str_value[0] == 'M'))
						bln_oldRouting = true;

					str_value = rs_entity.getString("SAB_TABLECODE");
					if (str_value != null && str_value.Length >0 )
						str_routingTable= str_value;

					str_value = rs_entity.getString("STR_TABLECODE");
					if (str_value != null && str_value.Length >0 )
						str_tableToUpdate = str_value;
					else
						str_tableToUpdate = str_routingTable;
				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error Executing query\nQurey: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to route transaction. \n" + e.Message);
			}
			finally
			{
				try
				{
					if (rs_entity != null)
						rs_entity.close();
				}
				catch (OleDbException e){	}
				rs_entity = null;

			}

//	Point No 99 - Change in OldRouting Logic (M - Mixed)
			//if (!bln_oldRouting)
			if (str_oldRoutingFlag!= "Y")
			{
				if (chr_operation == SHSM_AuditTrail.DML_OPERATION_INSERT || chr_operation == SHSM_AuditTrail.DML_OPERATION_UPDATE)
					return "";
			}
				
			if (!bln_route)
				return null;

			String str_className = str_entityId + "Transaction";
			
			if (str_package != null && str_package.Length != 0)
				str_className = str_package + "." + str_className;

			SHMA.Enterprise.TransactionRouting.RoutingController obj_transaction = null;

			if (bln_beforeRoute || bln_afterRoute) 
			{
				try 
				{
					obj_transaction = (RoutingController)SupportClass.CreateNewInstance(System.Type.GetType(str_className));
				}
				catch(Exception e)
				{
					throw new ProcessException("Security Module: \\n" + str_className + " class for BEFOREROUTE/AFTERROUTE could not be found");
				}

				if (obj_transaction == null)
					throw new ProcessException("Security Module: \\n" + str_className + " class for BEFOREROUTE/AFTERROUTE could not be found");

				obj_transaction.setFields(obj_colFields);
			}

			String str_docRef   = SHSM_Utility.fsgetDocumentReference(str_appCode, str_routingTable, str_arrKeyFields, obj_colFields) ;
			String str_docRefUI = SHSM_Utility.fsgetDocumentReferenceUI( str_appCode, str_routingTable, str_arrKeyFields, obj_colFields) ;

			/*------- Routing Remarks -------*/
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			str_reason=fsgetAccumulatedRemarks(str_appCode, str_tableCode, str_docRef, str_reason, str_userCode,str_entityId);
			bool bln_proceed = true;

			try 
			{
				if (bln_beforeRoute)
					bln_proceed = obj_transaction.beforeRoute();
			}
			catch (TransactionException e) 
			{
				bln_proceed = false;
				String str_msg = e.Message;

				if (str_msg != null && str_msg.Trim().Length > 0)
					str_msg = " : " + str_msg;

				str_msg =  "Cannot send for verification" + str_msg;

				throw new ProcessException("Security Module: \\n" + str_msg);
			}

					
			if (bln_proceed)
			{
				//T00005 --- OK 1
				// deleted temporary record from Inbox that was created for creater and initial router
				// after rejection of document
				//				String str_delSql	= "DELETE FROM SH_SM_IN_INBOX "
				//					+ " WHERE SAA_APPCODE='" + str_appCode + "'"
				//					+ " AND SAB_TABLECODE='" + str_tableCode + "'"
				//					+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'"
				//					+ " AND SIN_TYPE='R' ";

				//T00005 --- OK 1
				String str_delSql	= "UPDATE SH_SM_IN_INBOX "
					+ " SET SIN_STATUS='R' "
					+ " WHERE SAA_APPCODE='" + str_appCode + "'"
					+ " AND SAB_TABLECODE='" + str_routingTable + "'"
					+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'"
					+ " AND SIN_TYPE='R' ";

				try 
				{
					DB.executeDML(str_delSql);
				}
				catch(Exception e)
				{ 
					//throw new ProcessException(e.Message); 
					//-- Message changes 2006/01/09
					throw new ProcessException("Security Module: \\n" + "Query Error: Failed to route transaction \n" +e.Message); 
				};
				

				if (fsisNewDocument(str_appCode, str_routingTable, str_docRef)) 
				{
					// Change the creator only if document is not in routing
					//str_level = fsrouteNewTransaction(str_entityId, str_tableCode, str_arrKeyFields, obj_colFields, str_package);
					str_level = fsrouteNewTransaction(str_entityId, str_routingTable, str_arrKeyFields, obj_colFields, str_package,str_reason);
				}	
								
				else 
				{
					//str_level = fsrouteVerification(str_tableCode, str_docRef, str_arrKeyFields, obj_colFields );
					str_level = fsrouteVerification(str_entityId, str_routingTable, str_docRef, str_arrKeyFields, obj_colFields ,str_reason);
					
				}
				//
				fsupdateBusinessEntityTable(str_tableToUpdate, str_arrKeyFields, obj_colFields, str_level);


				if (bln_beforeRoute && obj_transaction.hasMessageSupport())
				{
					String str_temp = obj_transaction.getBeforeRouteMessage();
					if (str_temp.Trim().Length > 0)
						str_level +=  " : " + str_temp;
				}
				
				if (bln_afterRoute && str_level != null && str_level.StartsWith(LEVEL_APPROVED)) 
				{
					try 
					{
						str_level = LEVEL_APPROVED;
						if (obj_transaction.hasMessageSupport())
							str_level += " : " + obj_transaction.afterApproval();
						else
							obj_transaction.afterRoute();
					}
					catch(TransactionException e)
					{
						String str_msg = e.Message;

						if (str_msg != null && str_msg.Trim().Length > 0)
							str_msg = " : " + str_msg;

						str_msg =  "Cannot approve/post" + str_msg;

						throw new ProcessException("Security Module: \\n" + str_msg);
					
					}
				}
					
			}
			
			return str_level;

		}

		//MC0033-6 Start 31/10/2005
		//public static String fsrouteRejection(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_tableCode)
		public static String fsrouteRejection(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_tableCode,String str_reason)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_appCode  = (String) sessionValues.getAttribute("s_SAA_APPCODE");
			String str_docRef   = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);
			String str_docRefUI = SHSM_Utility.fsgetDocumentReferenceUI(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			
			Object[] obj_array = fsgetDateAndActivityFromInbox(str_userCode, str_appCode, str_tableCode, str_docRef);
			
			String str_receivingDate = (String)obj_array[0];
			String str_activityCode = (String)obj_array[1];

			String str_processId = null;
			int int_valueSerial = - 1;


			rowset rs_docLog = null;
			String str_query= "SELECT * FROM SH_SM_DL_DOCUMENTLOG "
				+ " WHERE SAA_APPCODE='" + str_appCode + "'"
				+ " AND SAB_TABLECODE='" + str_tableCode + "'"
				+ " AND SDL_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'"
				+ " AND SUL_LEVELCODE != 'N1' ";

			if (str_activityCode != null)
				str_query += " AND SAV_ACTCODE='" + str_activityCode + "' ";

			str_query += " ORDER BY SDL_DATETIMEACTUAL DESC";


			try
			{
				rs_docLog = DB.executeQuery(str_query);
				if (rs_docLog != null && rs_docLog.next())
				{
					str_processId = rs_docLog.getString("SFC_AUFCPROCESSID");
					int_valueSerial = rs_docLog.getInt("SVC_SRNO");
				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error executing query: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Rejection Error: " + e.Message);
			}
			finally
			{
				try
				{
					if (rs_docLog != null)
						rs_docLog.close();
				}
				catch (OleDbException e)
				{
				}
				rs_docLog = null;
			}




			if (int_valueSerial == - 1)
			{
				throw new ProcessException("Security Module: \\n" + "Previous forward list not found");
			}

			fsperformRoutingUpdate(str_appCode,str_processId, int_valueSerial, LEVEL_REJECTED, str_arrKeyFields, obj_colFields);

			Object[] obj_arrValues = null;
			obj_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_TR_TRANSROUTEENTITY",
				new String[] {"STR_TABLECODE"},	"WHERE PSE_ENTITYID='" + str_entityId + "'");

			if (obj_arrValues != null && obj_arrValues != SHSM_Utility.DB_NO_RECORD) 
			{


				String str_tableToUpdate = (String)obj_arrValues[0];
				if (str_tableToUpdate == null || str_tableToUpdate.Length==0)
					str_tableToUpdate = str_tableCode;

				
				fsupdateBusinessEntityTable(str_tableToUpdate, str_arrKeyFields, obj_colFields, LEVEL_REJECTED);
			}

			/*------- Routing Remarks -------*/
			//String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			str_reason=fsgetAccumulatedRemarks(str_appCode,str_tableCode,str_docRef,str_reason, str_userCode, str_entityId);

			String str_level = fsrejectVerification(str_entityId, str_tableCode, str_docRef, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
						
			return str_level;
		}

		// MC0033-6 START 29/10/2005
		//private static String fsrouteNewTransaction(String str_tableCode, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_packageName, String str_reason)
		private static String fsrouteNewTransaction(String str_entityId, String str_tableCode, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_packageName, String str_reason)
		{
			// MC0033-6 END 29/10/2005	
			
			// T00001 OK
			//DateTime dt_now = DateTime.Now;
			//String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now);
			String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);

			EnvHelper sessionValues = new EnvHelper();
			String str_fromUser = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");
			String str_programTypeCode = (String) sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode = (String) sessionValues.getAttribute("s_SAO_OPTCODE");


			
			String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields) ;
			String str_docRefUI = SHSM_Utility.fsgetDocumentReferenceUI(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields) ;


			
			//1.	Get Activity on the basis of passed arguments from screen (Application, Program Type, Option, Sub option)
			String str_activityCode = fsgetActivityCode(str_appCode, str_programTypeCode, str_optionCode, str_entityId);
			if (str_activityCode == null || str_activityCode.Length == 0)
				throw new ProcessException("Security Module: \\n" + "Activity code for  MODULE:" + str_appCode+", PROGRAMTYPE:"+ str_programTypeCode+", OPTION:"+str_optionCode+", ENTITY:"+str_entityId + " is undefined.");
				
			
			//2.	Get Process ID on the basis of passed arguments from screen (Application, Entity)
			String str_processId = fsgetProcessId(str_appCode, str_entityId);


			// if no process id the direct approve
			if (str_processId == null || str_processId.Length == 0) 
			{
				throw new ProcessException("Security Module: \\n" + "Routing Formula for MODULE:" + str_appCode+", ENTITY:"+str_entityId + " is undefined.");
				//return fsapproveTransactionDirect( str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser, str_date, str_docRefUI);
			}

			
			//3.	Read Field Combination of Process ID from Field Combination table
			String str_fieldCombination = fsgetFieldCombination(str_appCode, str_processId);
			
			String[] str_arrFields = StringUtility.fssplitString(str_fieldCombination, '~');
			
			//4.	Make Value Combination by evaluating Field Combination
			String[] str_arrValues = fsevaluateFieldCombinationToValue(str_appCode, str_arrFields, str_arrKeyFields, obj_colFields);
			
			String str_valueCombination = StringUtility.fsjoinToString(str_arrValues, '~');
			
			//5.	Match evaluated Value Combination with defined Value Combination in table
			//6.	Get reference (serial no.) of matched Value Combination
			int int_valueSerial = fsgetValueCombSerialNo(str_appCode, str_processId, str_valueCombination, str_arrKeyFields, obj_colFields);
			
			if (int_valueSerial == - 1)
			{
				// change on 15 jan 2004
				//throw new ProcessException("No effective value combination is defined for MODULE: "+ str_appCode+", PROCESS: "+ str_processId+", FIELDCOMBINATION: " + str_fieldCombination);
				/****** MC0033 - 9 **********************************************START*/
				String str_message="";
				str_message = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_TR_TRANSROUTEENTITY","STR_MESSAGE"," WHERE PSE_ENTITYID='"+str_entityId+"'");
				if (str_message==null || str_message.Length==0 || str_message=="")
					throw new ProcessException("Security Module: \\n" + "No effective value combination is defined for MODULE: "+ str_appCode+", PROCESS: "+ str_processId+", FIELDCOMBINATION: " + str_fieldCombination);
				else if (str_message!=null && str_message.Length!=0 && str_message!="")
					throw new ProcessException("Security Module: \\n" + str_message);
				/****** MC0033 - 9 **********************************************END*/


				//return fsapproveTransactionDirect( str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser, str_date, str_docRefUI);
			}
			

			String str_approvalLevel = fsgetApprovalLevelDefined(str_appCode, str_processId, int_valueSerial);
			if (str_approvalLevel == null)
				throw new ProcessException("Security Module: \\n" + "No Approval level is defined for for MODULE: "+ str_appCode+", PROCESS: "+ str_processId+", VALUECOMBINATION: " + str_valueCombination);



			//7.	Get first verification from Forwarding List table on the basis of matched Value Combination
			String str_userLevel = fsgetFirstForwardingLevelCode(str_appCode, str_processId, int_valueSerial);
			
			// check if no valid userlevel found
			if (str_userLevel == null || str_userLevel.Length == 0)
				return null;
			
			
			//--Improvement point 8 - start ************************************************//
			//String str_creator = fsgetDocumentCreater(str_appCode, str_activityCode, str_tableCode, str_docRef, str_processId);
			String str_creator = null;
			//--Improvement point 8 - end ************************************************//			

			if (str_creator==null || str_creator.Length == 0) 
			{
				// change on 24 Feb 2005
				str_creator = str_fromUser;

				//throw new ProcessException("Corrupt data. Unable to determine data entry user" );
			}


			// T0015 Stop calling of method “fscreateDocumentLogEntry” from duplicate location
			// should be in if condition e.g if (str_creator.Equals(str_fromUser))
			
			// fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser,  str_date, "N1", null, -1, str_docRefUI);
	
			/*  01/12/2005 - start*/
			//			if (str_creator.Equals(str_fromUser)) 
			//			{
			//				// T00001 OK
			//				//DateTime dt_now2 =  dt_now.AddSeconds(-1);
			//				//String str_date2 = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now2);
			//				//fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_creator,  str_date2, "N1", null, -1, str_docRefUI);
			//				//MC0033-6 Start 31/10/2005
			//				//fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser,  str_date, "N1", null, -1, str_docRefUI, str_reason);
			//				fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser,  str_date, "N1", null, -1, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
			//				//MC0033-6 Start 31/10/2005
			//			}
			//			else 
			//			{
			//
			//				fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_creator,  str_date, "N1", null, -1, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
			//			}
			/*  01/12/2005 - end */			


			
			bool bln_cannotApproveSelf = !fscanApproveSelf(str_creator);
				
			
			
			//10.	Create record(s) in inbox of all users obtained in step 8
			

			if (LEVEL_APPROVAL_DIRECT == str_approvalLevel) 
			{
				if (bln_cannotApproveSelf)
					throw new ProcessException("Security Module: \\n" + "Direct Approval Failed: You are not allowed to authorize own transaction");

				//return fsapproveTransactionDirect(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser, str_date, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
				String str_status = fsapproveTransactionDirect(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser, str_date, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
				//java fsupdateDirectApproval(str_appCode, str_processId, int_valueSerial,str_arrKeyFields, obj_colFields);
				//     fsupdateDirectApproval(str_appCode, str_processId, int_valueSerial,str_arrKeyFields, obj_colFields )
				fsperformRoutingUpdate(str_appCode,str_processId,int_valueSerial,str_userLevel,str_arrFields,obj_colFields);				

				return str_status;
			}



			//9.	Get user(s) of first verification
			String[][] str_arrUserGroups = fsgetAllForwardingUsersWithFlag (str_appCode, str_processId, int_valueSerial, str_userLevel);
			bool bln_forwarded=false;
			if (str_arrUserGroups != null)
			{
				for (int i = 0; i < str_arrUserGroups.Length; i++)
				{
					String str_toAppCode = str_arrUserGroups[i][0];
					String str_toGroupCode = str_arrUserGroups[i][1];
					String str_allowEdit = str_arrUserGroups[i][2];
					// -- MC New Routing ----- start
					//if (bln_cannotApproveSelf && str_creator.Equals(str_toUserGroup) )
					//	continue;
					// -- MC New Routing ----- end

					//MC0033-6 START 29/10/2005
					//fscreateInboxEntryWithFlag(str_processId, int_valueSerial,  str_date, str_fromUser, str_toUser, str_appCode, str_userLevel, str_activityCode, str_tableCode, str_docRef, str_docRefUI, str_arrUsers[i][1]);
					fscreateInboxEntryWithFlag(str_entityId, str_processId,int_valueSerial, str_date, str_fromUser, str_toAppCode, str_toGroupCode, "#", str_appCode,str_userLevel, str_activityCode, str_tableCode, str_docRef, str_docRefUI, str_allowEdit, str_reason);
					//MC0033-6 END 29/10/2005
					bln_forwarded = true;
				}
				//2006/03/17 - Setting Inbox PKs in session - start
				//fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_date, str_entityId);
				//2006/03/17 - Setting Inbox PKs in session - end
			}
				// T0021
			else
			{
				throw new ProcessException("Security Module: \\n" + "No Group Profile Found for Forwarding  - MODULE: "+ str_appCode+", PROCESS: "+ str_processId+", VALUE CRITERIA S.NO: " + int_valueSerial);
			}
			
			if (bln_forwarded) 
			{
				//8.	Create two records in Document Log with reference of Field Combination, Value Combination and next Verification
				fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_fromUser,  str_date, str_userLevel, str_processId, int_valueSerial, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);

				//2006/03/17 - Setting Inbox PKs in session - start
				fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_date, str_entityId,"V");
				//2006/03/17 - Setting Inbox PKs in session - end

				//11.	Return transaction status to data entry option
				return str_userLevel;
			}
			return fsrouteNextVerification(str_entityId, str_fromUser,str_appCode, str_activityCode, str_processId, str_tableCode, str_docRef, str_docRefUI, int_valueSerial, str_userLevel,  str_arrKeyFields, obj_colFields, str_reason);
		}

		//MC0033-6 START 29/10/2005
		//public static String fsrouteVerification( String str_tableCode, String str_docRef, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		public static String fsrouteVerification(String str_entityId, String str_tableCode, String str_docRef, String[] str_arrKeyFields, NameValueCollection obj_colFields,String str_reason )
			//MC0033-6 END 29/10/2005		
		{
			
	
			//0.	Get Activity on the basis of passed arguments from screen (Application, Program Type, Option, Sub option)
			//String str_activityCode = fsgetActivityCode(str_appCode, str_programTypeCode, str_optionCode, str_suboptionCode);
			
			EnvHelper sessionValues = new EnvHelper();
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			//T00005 --- OK 1
			//			String str_where = " WHERE SUS_USERCODE='" + str_userCode +"' "
			//								    + " AND SAA_APPCODE='" + str_appCode +"' "
			//								    + " AND SAB_TABLECODE='" + str_tableCode +"' "
			//								    + " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") +"' "
			//									+ " AND SIN_STATUS='U'" ;
			//
			//			DateTime dt_receivingDate = (DateTime) shsm.SHSM_Utility.fsgetColumnValueAgainstQuery("SH_SM_IN_INBOX", "SIN_DATETIME" , str_where);

			
			Object[] obj_array = fsgetDateAndActivityFromInbox(str_userCode, str_appCode, str_tableCode, str_docRef);
			
			if (obj_array == null || obj_array[0] == null)
				throw new ProcessException("Security Module: \\n" + "Transaction routing failed: Transaction data does not match with Routing data");
			
			
			//CDNSOracleRehan DateTime dt_receivingDate = (DateTime) obj_array[0];
			
			String dt_receivingDate = (String)obj_array[0];
			String str_activityCode = (String)obj_array[1];


			//CDNSOracleRehan String str_receivingDate = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_receivingDate);
			
			String str_receivingDate = dt_receivingDate;
			//1.	Mark Inbox record as 'Read'
			fsmarkInboxEntryAsRead(str_userCode, str_appCode, str_activityCode, str_tableCode, str_docRef, str_receivingDate);
			
			
			//2.	Get reference of Field Combination, Value Combination and next Verification from Document Log
			String str_processId = null;
			String str_userLevel = null;
			int int_valueSerial = - 1;
			String str_docRefUI = null;
			
			rowset rs_docLog = null;
			//T00005 --- OK 1
			String str_query = "SELECT * FROM SH_SM_DL_DOCUMENTLOG " + " WHERE SAA_APPCODE='" + str_appCode + "' AND SAB_TABLECODE='" + str_tableCode + "'" + " AND SDL_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'" + " AND SDL_DATETIME ='" + str_receivingDate + "' AND SUL_LEVELCODE != 'N1' ";
			
			if (str_activityCode != null)
				str_query += " AND SAV_ACTCODE='" + str_activityCode + "' ";
			
			//ASIF OK
			str_query += " ORDER BY SDL_DATETIMEACTUAL DESC";


			try
			{
				rs_docLog = DB.executeQuery(str_query);
				if (rs_docLog != null && rs_docLog.next())
				{
					str_processId = rs_docLog.getString("SFC_AUFCPROCESSID");
					int_valueSerial = rs_docLog.getInt("SVC_SRNO");
					str_userLevel = rs_docLog.getString("SUL_LEVELCODE");
					str_docRefUI = rs_docLog.getString("SDL_DOCREFUI");
				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error executing query\nQurey: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to route transaction for verification. \n" + e.Message);
			}
			finally
			{
				try
				{
					if (rs_docLog != null)
						rs_docLog.close();
				}
				catch (OleDbException e)
				{
				}
				rs_docLog = null;
			}
						
			if (int_valueSerial == - 1)
			{
				// System.Console.Out.WriteLine("No matching value seial number");
				// change on 15 jan 2004
				throw new ProcessException("Security Module: \\n" + "Previous forward list not found");
				//return null;
			}
			
			String str_approvalLevel = fsgetApprovalLevelDefined(str_appCode, str_processId, int_valueSerial);
			if (str_approvalLevel == null)
				throw new ProcessException("Security Module: \\n" + "No approval level is defined for MODULE: "+ str_appCode+", PROCESS:"+ str_processId+", VALUECOMBINATION SERIAL NO:"+ int_valueSerial);
			
			return fsrouteNextVerification(str_entityId, str_userCode, str_appCode, str_activityCode, str_processId, str_tableCode, str_docRef, str_docRefUI, int_valueSerial, str_userLevel, str_arrKeyFields,obj_colFields, str_reason);
		}



		//MC0033-6 Start 29/10/2005
		//private static String fsrouteNextVerification(String str_userCode, String str_appCode, String str_activityCode, String str_processId, String str_tableCode, String str_docRef, String str_docRefUI, int int_valueSerial,String str_userLevel, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		private static String fsrouteNextVerification(String str_entityId, String str_userCode, String str_appCode, String str_activityCode, String str_processId, String str_tableCode, String str_docRef, String str_docRefUI, int int_valueSerial,String str_userLevel, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_reason)
			//MC0033-6 End 29/10/2005
		{

			// T00001 OK
			//DateTime dt_now = DateTime.Now;
			//String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now);
			String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);

			char chr_level = str_userLevel[0];
			chr_level = System.Char.ToUpper(chr_level);

			fsperformRoutingUpdate(str_appCode,str_processId, int_valueSerial, str_userLevel, str_arrKeyFields, obj_colFields);
			
			String str_creator = null;			
			if (str_creator==null || str_creator.Length == 0) 
			{
				// change on 24 Feb 2005
				str_creator = fsgetDocumentLastRouterOfLevel(str_appCode, str_activityCode, str_tableCode, str_docRef, null,"N1") ;
				//throw new ProcessException("Security Module: \\n" + "Corrupt data. Unable to determine data entry user" );
			}

			// the document is approved
			if (chr_level == 'A')
			{	
				//MC0033-6 Strat 31/10/2005
				//return fsapproveDocument( str_tableCode, str_docRef, str_docRefUI, str_activityCode, str_reason);
				
				// improvement for point no 41
				//bool bln_cannotApproveSelf = fscanApproveSelf(str_creator);
				//if (str_userCode == str_creator &&  !bln_cannotApproveSelf )
				//{
				//throw new ProcessException("Security Module: \\n" + "Transaction cannot be forwarded for approval because " + str_userCode + " is not authorized to approve his/her own transaction");
				//}
				
				// improvement for point no 41
			
				
				return fsapproveDocument(str_entityId, str_tableCode, str_docRef, str_docRefUI, str_activityCode, str_reason, obj_colFields, str_arrKeyFields);
				
				//MC0033-6 Strat 31/10/2005
			}
			
			String str_userLevelNext = null;
			
			bool bln_forwarded=false;
			
			//--Improvement point 8 - start ************************************************//
			//String str_creator = fsgetDocumentCreater(str_appCode, str_activityCode, str_tableCode, str_docRef, str_processId);

			//--Improvement point 8 - end ************************************************//


			if (chr_level == 'V')
			{
				// this means verification level
				str_userLevelNext = fsgetNextVerificationLevelCode(str_appCode, str_processId, int_valueSerial, str_userLevel);
				
				// check if no valid verification userlevel found
				if (str_userLevelNext == null || str_userLevelNext.Length == 0)
				{
					str_userLevelNext = fsgetApprovalLevelCode(str_appCode, str_processId, int_valueSerial);
				}

				if (str_userLevelNext != null && str_userLevelNext.Length != 0)
				{
					// CONTINUE WITH VERIFICATION PROCESS
					//3.	Create one record in Document Log with reference of Field Combination, Value Combination and next Verification
					
					//bool bln_cannotApproveSelf = !fscanApproveSelf(str_creator);

					//4.	Get user(s) of next verification
					String[][] str_arrUserGroups = fsgetAllForwardingUsersWithFlag(str_appCode, str_processId, int_valueSerial, str_userLevelNext);
					
					//5.	Create record(s) in inbox of all users obtained in step 4
					if (str_arrUserGroups != null)
					{
						for (int i = 0; i < str_arrUserGroups.Length; i++)
						{
							//-- MC New Routing ----- start
							String str_toAppCode = str_arrUserGroups[i][0];
							String str_toGroupCode = str_arrUserGroups[i][1];
							String str_allowEdit = str_arrUserGroups[i][2];

							//if (bln_cannotApproveSelf && str_creator.Equals(str_toUser) )
							//	continue;

							//MC0033-6 start 29/10/2005	
							//fscreateInboxEntryWithFlag(str_processId, int_valueSerial,  str_now, str_userCode, str_toUser, str_appCode, str_userLevelNext, str_activityCode, str_tableCode, str_docRef, str_docRefUI, str_arrUsers[i][1]);
							fscreateInboxEntryWithFlag(str_entityId, str_processId, int_valueSerial,  str_now, str_userCode, str_toAppCode, str_toGroupCode,"#" ,str_appCode , str_userLevelNext, str_activityCode, str_tableCode, str_docRef, str_docRefUI, str_allowEdit, str_reason);
							//MC0033-6 End 29/10/2005
							//-- MC New Routing ----- end							
							bln_forwarded = true;
						}
						//2006/03/17 - Setting Inbox PKs in session - start
						//fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId);
						//2006/03/17 - Setting Inbox PKs in session - end

					}

						// T0021
					else
					{
						throw new ProcessException("Security Module: \\n" + "No Group Profile Found for Forwarding  - MODULE: "+ str_appCode+", PROCESS: "+ str_processId+", VALUE CRITERIA S.NO: " + int_valueSerial);
					}
					
					
					if (bln_forwarded)
					{
						fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_now, str_userLevelNext, str_processId, int_valueSerial, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);

						//2006/03/17 - Setting Inbox PKs in session - start
						fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId,"V");
						//2006/03/17 - Setting Inbox PKs in session - end
					}

				}

			}
			
			//6.	Return transaction status to verification option

				if (bln_forwarded)
					return str_userLevelNext;

			if (LEVEL_APPROVAL.Equals(str_userLevelNext)) 
			{
				String str_userName = new shsm.SHSM_UserProfile(str_creator).fsgetUserName();
				throw new ProcessException("Security Module: \\n" + "Transaction cannot be forwarded to " + str_userName+" ("+ str_creator+ ") for approval because he/she is not authorized to approve his/her own transaction");
			}
			 

			//MC0033-6 Start 31/10/2005
			//return fsrouteNextVerification(str_userCode, str_appCode, str_activityCode, str_processId, str_tableCode, str_docRef, str_docRefUI, int_valueSerial,str_userLevelNext,str_arrKeyFields,obj_colFields);
			return fsrouteNextVerification(str_entityId, str_userCode, str_appCode, str_activityCode, str_processId, str_tableCode, str_docRef, str_docRefUI, int_valueSerial,str_userLevelNext,str_arrKeyFields,obj_colFields, str_reason);
			//MC0033-6 Start 31/10/2005
		}

		
	
		//MC0033-6 Start 31/10/2005
		//private static String fsrejectVerification( String str_tableCode, String str_docRef, String str_docRefUI)
		private static String fsrejectVerification(String str_entityId, String str_tableCode, String str_docRef, String str_docRefUI, String str_reason,NameValueCollection obj_colFields, String[] str_arrKeyFields)
			//MC0033-6 End 31/10/2005
		{
			
			// T00001 OK
			//DateTime dt_now = DateTime.Now;
			//String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now);
			String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);
			
			//0.	Get Activity on the basis of passed arguments from screen (Application, Program Type, Option, Sub option)
			//String str_activityCode = fsgetActivityCode(str_appCode, str_programTypeCode, str_optionCode, str_suboptionCode);
			
			EnvHelper sessionValues = new EnvHelper();

			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			//T00005 --- OK 1
			//			String str_where = " WHERE SUS_USERCODE='" + str_userCode +"' "
			//				+ " AND SAA_APPCODE='" + str_appCode +"' "
			//				+ " AND SAB_TABLECODE='" + str_tableCode +"' "
			//				+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") +"' "
			//				+ " AND SIN_STATUS='U'" ;
			//			DateTime dt_receivingDate = (DateTime) shsm.SHSM_Utility.fsgetColumnValueAgainstQuery("SH_SM_IN_INBOX", "SIN_DATETIME" , str_where);
			

			Object[] obj_array = fsgetDateAndActivityFromInbox(str_userCode, str_appCode, str_tableCode, str_docRef);
			//CDNSOracleRehan DateTime dt_receivingDate = (DateTime) obj_array[0];
			
			String dt_receivingDate = (String)obj_array[0];
			String str_activityCode = (String)obj_array[1];
			
			//CDNSOracleRehan String str_receivingDate = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_receivingDate);
			String str_receivingDate = dt_receivingDate;

			
			//1.	Mark Inbox record as 'Read'
			fsmarkInboxEntryAsRead(str_userCode, str_appCode, str_activityCode, str_tableCode, str_docRef, str_receivingDate);
			
			//2.	Notify the creators
			String str_processId=null;
			String str_initialRouter = fsgetDocumentLastRouterOfLevel(str_appCode, str_activityCode, str_tableCode, str_docRef, str_processId, "N1");

			//--Improvement point 8 - start ************************************************//
			//String str_creator = fsgetDocumentUpdatedBy(str_appCode, str_activityCode, str_tableCode, str_docRef, str_processId);
			String str_creator = null;
			//--Improvement point 8 - end ************************************************//

			if (str_creator==null || str_creator.Length == 0) 
			{
				// change on 24 Feb 2005
				str_creator = str_initialRouter;

				//throw new ProcessException("Security Module: \\n" + "Corrupt data. Unable to determine data entry user" );
			}

			// MC0033-6 Start 29/10/2005	
			// fscreateInboxEntryWithFlag(null, -1, str_now, str_userCode, str_creator, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y");
			//-- MC New Routing -----start
			//fscreateInboxEntryWithFlag(null, -1, str_now, str_userCode, str_creator, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y", str_reason);
			fscreateInboxEntryWithFlag(str_entityId, null, -1, str_now, str_userCode, "#", "#", str_creator, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y", str_reason);

			//2006/03/17 - Setting Inbox PKs in session - start
			fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId,"R");
			//2006/03/17 - Setting Inbox PKs in session - end

			//-- MC New Routing ----- end
			// MC0033-6 End 29/10/2005

			if (!str_creator.Equals(str_initialRouter))
			{
				//-- MC New Routing -----
				fscreateInboxEntryWithFlag(str_entityId, null, -1, str_now, str_userCode, "#", "#",  str_initialRouter, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y", str_reason);
				//2006/03/17 - Setting Inbox PKs in session - start
				//fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId);
				//2006/03/17 - Setting Inbox PKs in session - end
			}

			// MC0033-6 Start 29/10/2005	
			//fscreateInboxEntryWithFlag(null, -1, str_now, str_userCode, str_initialRouter, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y");
			//fscreateInboxEntryWithFlag(null, -1, str_now, str_userCode, str_initialRouter, str_appCode,LEVEL_REJECTED, str_activityCode, str_tableCode, str_docRef, str_docRefUI, "Y", str_reason);
			
			//fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_now, LEVEL_REJECTED, null, -1, str_docRefUI);
			fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_now, LEVEL_REJECTED, null, -1, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
			// MC0033-6 End 29/10/2005

			//2006/03/17 - Setting Inbox PKs in session - start
			fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId,"R");
			//2006/03/17 - Setting Inbox PKs in session - end

				
			return LEVEL_REJECTED;
			
			//2.	Get reference of Field Combination, Value Combination and next Verification from Document Log
			//			String str_processId = null;
			//			String str_toUser = null;
			//			int int_valueSerial = - 1;
			//			
			//			rowset rs_docLog = null;
			//	T00005 --- OK 1
			//			String str_query = "SELECT * FROM SH_SM_DL_DOCUMENTLOG " + " WHERE SAA_APPCODE='" + str_appCode + "' AND SAB_TABLECODE='" + str_tableCode + "'" + " AND SDL_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'" 
			//			//						+ " AND SDL_DATETIME ='" + str_receivingDate + "'"
			//									+ " AND SUL_LEVELCODE = 'N1' "
			//									+ " ORDER BY SDL_DATETIME";
			//			
			//			if (str_activityCode != null)
			//				str_query += " AND SAV_ACTCODE='" + str_activityCode + "' ";
			//			
			//			str_query += " ORDER BY SDL_DATETIME DESC";
			//
			//
			//			try
			//			{
			//				rs_docLog = DB.executeQuery(str_query);
			//				if (rs_docLog != null && rs_docLog.next())
			//				{
			//					str_processId = rs_docLog.getString("SFC_AUFCPROCESSID");
			//					int_valueSerial = rs_docLog.getInt("SVC_SRNO");
			//					str_toUser = rs_docLog.getString("SUS_USERCODE");
			//				}
			//			}
			//			catch (OleDbException e)
			//			{
			//				throw new ProcessException("");
			//			}
			//			catch (Exception e)
			//			{
			//				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
			//				throw new ProcessException(e.Message);
			//			}
			//			finally
			//			{
			//				try
			//				{
			//					if (rs_docLog != null)
			//						rs_docLog.close();
			//				}
			//				catch (OleDbException e)
			//				{
			//				}
			//				rs_docLog = null;
			//			}
			//			
			//
			//			if (int_valueSerial == - 1)
			//			{
			//				System.Console.Out.WriteLine("No matching value seial number");
			//				return null;
			//			}
			//			
			//
			//			if (str_toUser != null && str_toUser.Length != 0)
			//			{
			//				// CONTINUE WITH VERIFICATION PROCESS
			//				//3.	Create one record in Document Log with reference of Field Combination, Value Combination and next Verification
			//					
			//					
			//				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
			//				fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  dt_now, "R1", str_processId, int_valueSerial);
			//					
			//				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
			//				//fscreateInboxEntry(str_processId, int_valueSerial,  dt_now, str_userCode, str_toUser, str_appCode, "R1", str_activityCode, str_tableCode, str_docRef);
			//			}

		}

		
		
		


		/*------------------------------------------------------------------------*/
		/* --------------  Called From Approval Option -------------------------*/
		/*------------------------------------------------------------------------*/
		//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'	
		//MC0033-6 Start 31/10/2005
		//public static String fsrouteApproval(String str_tableCode, String str_docRef, String str_docRefUI)
		public static String fsrouteApproval(String str_entityId, String str_tableCode, String str_docRef, String str_docRefUI,String str_reason, NameValueCollection obj_colFields, String[] str_arrKeyFields)
			//MC0033-6 End 31/10/2005
		{
			// T00001 OK
			//DateTime dt_now = DateTime.Now;
			//String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now);
			String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);
			
			EnvHelper sessionValues = new EnvHelper();

			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String str_activityCode = null;
			
			//CDNS imran DateTime dt_receivingDate = DateTime.Now;
			String dt_receivingDate = null;

			
			//T00005 --- OK 1
			String str_query = " SELECT * FROM SH_SM_IN_INBOX "
				+ " WHERE SUS_USERCODE='" + str_userCode +"' "
				+ " AND SAA_APPCODE='" + str_appCode +"' "
				+ " AND SAB_TABLECODE='" + str_tableCode +"' "
				+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") +"' "
				+ " AND SIN_STATUS='U'" ;

			
			rowset rs_inbox = null;
			try
			{
				rs_inbox = DB.executeQuery(str_query);
				if (rs_inbox != null && rs_inbox.next())
				{
					str_activityCode = rs_inbox.getString("SAV_ACTCODE");
					//CDNS ALREADY CHANGED					
					dt_receivingDate = rs_inbox.getString("SIN_DATETIME");

				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to route transaction for approval. \n" +e.Message);
			}
			finally
			{
				try
				{
					if (rs_inbox != null)
						rs_inbox.close();
				}
				catch (OleDbException e)
				{
				}
				rs_inbox = null;
			}
			// CDNS MODIFY BY IMRAN 16082005
			//String str_receivingDate = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_receivingDate);
			String str_receivingDate = dt_receivingDate;			

			//1.	Mark Inbox record as 'Read'
			fsmarkInboxEntryAsRead(str_userCode, str_appCode, str_activityCode, str_tableCode, str_docRef, str_receivingDate);
			
			
			//			//2.	Create one record in Document Log with 'Approval' status
			//			String str_userLevelNext = LEVEL_APPROVED;
			//			
			//			//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
			//			fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_now, str_userLevelNext, null, -1, str_docRefUI);
			//			
			//			
			//
			//			//3.	Return transaction status to approval option
			//			return str_userLevelNext;
			//MC0033-6 Start 31/10/2005
			//return fsapproveDocument(str_tableCode, str_docRef, str_docRefUI, str_activityCode);
			return fsapproveDocument(str_entityId, str_tableCode, str_docRef, str_docRefUI, str_activityCode, str_reason, obj_colFields, str_arrKeyFields);
			//MC0033-6 Start 31/10/2005
		}
		//MC0033-6 Start 31/10/2005
		//private static String fsapproveDocument(String str_tableCode, String str_docRef, String str_docRefUI, String str_activityCode, String str_reason)
		private static String fsapproveDocument(String str_entityId, String str_tableCode, String str_docRef, String str_docRefUI, String str_activityCode, String str_reason, NameValueCollection obj_colFields, String[] str_arrKeyFields  )
			//MC0033-6 End 31/10/2005
		{
			
			// T0001 OK
			//DateTime dt_now = DateTime.Now;
			//String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_now);
			String str_now = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);
			
			EnvHelper sessionValues = new EnvHelper();

			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

		
			//2.	Create one record in Document Log with 'Approval' status
			String str_userLevelNext = LEVEL_APPROVED;
			
			//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
			fscreateDocumentLogEntry(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_now, str_userLevelNext, null, -1, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);

			//2006/03/17 - Setting Inbox PKs in session - start
			fssetRoutingRemarksInput(str_appCode, str_tableCode, str_docRef, str_now, str_entityId,"A");
			//2006/03/17 - Setting Inbox PKs in session - end

			
			//Move data in history tables
			fsmoveRoutingRecordsInHistory(str_appCode,str_tableCode, str_docRef);

			//3.	Return transaction status to approval option
			return str_userLevelNext;
		}
		
	
		
		
		public static String fsgetActivityCode(String str_appCode, String str_programTypeCode, String str_optionCode, String str_entityId)
		{
			String str_where = " WHERE SAA_APPCODE='" + str_appCode + "'" 
							+ " AND SPT_PRGTYPECODE='" + str_programTypeCode + "'" 
							+ " AND SAO_OPTCODE='" + str_optionCode + "'" 
							+ " AND PSE_ENTITYID = '" + str_entityId + "'";
			//									+ " AND SAN_SUBOPTCODE='" + str_suboptionCode + "'";
			
			return SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_where);
		}
		

		public static String fsgetProcessId(String str_appCode, String str_entityId)
		{
			String str_where = " WHERE SAA_APPCODE='" + str_appCode + "'" + " AND PSE_ENTITYID='" + str_entityId + "'";
			return SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_EP_ENTITYPROCESSMAP", "SFC_AUFCPROCESSID", str_where);
		}
		
		
		private static String fsgetFieldCombination(String str_appCode, String str_processId)
		{
			String str_where= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SFC_AUFCPROCESSID='" + str_processId + "'"
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
							
			Object obj_fieldComb = SHSM_Utility.fsgetColumnValueAgainstQuery("SH_SM_FC_FIELDCOMB", "SFC_AUFCFIELDCOMB", str_where);
			// CDNS ALREADY CHANGED
			return SHSM_Utility.fsgetClobAsString( obj_fieldComb);
		}
		
		
		private static String[] fsevaluateFieldCombinationToValue(String str_appCode, String[] str_arrFields, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			String[] str_arrValues = new String[str_arrFields.Length];
			
			for (int i = 0; i < str_arrFields.Length; i++)
			{

				//  ********************* Change ON 25 April 2005 ********************
				FormulaField obj_formulaField = new FormulaField(str_appCode, str_arrFields[i].Trim());
				Object obj_value = obj_formulaField.fsevaluateValueOfField(str_arrKeyFields, obj_colFields);
					
				//Object obj_value = fsevaluateValueOfField(str_appCode, str_arrFields[i].Trim(), str_arrKeyFields, obj_colFields);
				//  ******************************************************************
		
				
				if (obj_value == null)
					throw new ProcessException("Security Module: \\n" + "LOGICAL FIELD:" + str_arrFields[i] + " is undefined");
				else if (obj_value is DateTime)
					str_arrValues[i] = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE, (DateTime) obj_value);
				else
				{
					str_arrValues[i] = obj_value.ToString();
				}
			}
			
			return str_arrValues;
		}
		
		
		/* // move to shsm.FormulaField
		private static Object fsevaluateValueOfField(String str_appCode, String str_fieldCode, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			
			Object obj_value = null;
			rowset rs_field = null;
			String str_sql = "";
			
			try
			{
				str_sql = "SELECT * FROM SH_SM_FS_FIELDS " 
					+ " WHERE SAA_APPCODE='" + str_appCode + "'" 
					+ " AND SFS_FIELDCODE='" + str_fieldCode + "'"
					+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;

				
				rs_field = DB.executeQuery(str_sql);
				System.Console.Out.WriteLine(rs_field.size() + " records found for " + str_sql);
				
				if (rs_field.next())
				{
					String str_fieldType = rs_field.getString("SFS_FIELDTYPE");
					String str_tableCode = rs_field.getString("SAB_TABLECODE");
					String str_columnCode = rs_field.getString("SAC_COLCODE");
					Object obj_whereClause = rs_field.getObject("SFS_FETCHWHERE");
					Object obj_userQuery = rs_field.getObject("SFS_FETCHQUERY");
					String str_constantValue = rs_field.getString("SFS_VALUE");
					
					System.Console.Out.WriteLine("Type of of field is " + str_fieldType);
					
					if (str_fieldType.Equals("C"))
					{
						// a constant value
						obj_value = str_constantValue;
					}
					else if (str_fieldType.Equals("S"))
					{
						// session value
						EnvHelper obj_env = new EnvHelper();
						try
						{
							obj_value = obj_env.getAttribute(str_constantValue);
						}
						catch (System.NullReferenceException e)
						{
						}
					}
					else if (str_fieldType.Equals("Q"))
					{
						// query specified
						//UPGRADE_TODO: Interface 'java.sql.Clob' was converted to 'System.Char[]' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlClob_3"'
						String str_userQuery = SHSM_Utility.fsgetClobAsString( obj_userQuery);
						String str_generatedSql = SHSM_Utility.fsgetExceutableQuery(str_userQuery, str_arrKeyFields, obj_colFields);
						obj_value = SHSM_Utility.fsgetColumnValueAgainstQuery(str_generatedSql);
					}
					else if (str_fieldType.Equals("T"))
					{
						// value from table
						//UPGRADE_TODO: Interface 'java.sql.Clob' was converted to 'System.Char[]' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlClob_3"'
						String str_whereClause = SHSM_Utility.fsgetClobAsString( obj_whereClause);
						String str_generatedSql = " SELECT " + str_columnCode + " FROM " + str_tableCode + " " + str_whereClause;
						str_generatedSql = SHSM_Utility.fsgetExceutableQuery(str_generatedSql, str_arrKeyFields, obj_colFields);
						obj_value = SHSM_Utility.fsgetColumnValueAgainstQuery(str_generatedSql);
					}
				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Error Executing query\nQurey: "  + str_sql + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
				throw new ProcessException(e.Message);
			}
			finally
			{
				try
				{
					if (rs_field != null)
						rs_field.close();
				}
				catch (OleDbException e)
				{
				}
				rs_field = null;
			}
			
			
			
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
			System.Console.Out.WriteLine("Value of field is " + obj_value);
			return obj_value;
		}
		*/
		
		private static int fsgetValueCombSerialNo(String str_appCode, String str_processId, String str_valueCombination, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			String str_today = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE, DateTime.Now);

//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////
/// Point No 81 - ELSE condition in VC - Start
//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////
			//String str_sql = "SELECT *  FROM SH_SM_VC_VALUECOMB " 
			//	+ " WHERE SAA_APPCODE='" + str_appCode + "'" 
			//	+ " AND SFC_AUFCPROCESSID='" + str_processId + "'" 
			//	+ " AND SVC_AUVCVALUECOMB LIKE '" + str_valueCombination + "'"
			//	+ " AND '" + str_today + "' BETWEEN SVC_EFFECTFROM AND SVC_EFFECTTO "
			//	+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
			EnvHelper sessionValues = new EnvHelper();
			String str_defaultVC =(String)  sessionValues.getAttribute("s_SGS_DEFAULTSTRING");
  
			String[] arrPowerSet=StringUtility.fsgetPowerSet(str_valueCombination,str_defaultVC);
			String str_list = "";
			    
			for(int i=0;i<arrPowerSet.Length;i++)
			{
				str_list += "'"+arrPowerSet[i]+"',";
			}

			str_list = str_list.Substring(0,str_list.Length-1 );
  
			String str_sql = "SELECT *  FROM SH_SM_VC_VALUECOMB "
				+ " WHERE SAA_APPCODE='" + str_appCode + "'"
				+ " AND SFC_AUFCPROCESSID='" + str_processId + "'"
				+ " AND SVC_AUVCVALUECOMB IN (" + str_list + ")"
				//Point No 102 - Oracle Support - Start
				//+ " AND '" + str_today + "' BETWEEN SVC_EFFECTFROM AND SVC_EFFECTTO "
				+ " AND ? BETWEEN SVC_EFFECTFROM AND SVC_EFFECTTO "
				//Point No 102 - Oracle Support - Start
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE
				+ " ORDER BY SVC_SRNO";
//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////
		/// Point No 81 - ELSE condition in VC - Start
//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////
			

			//Point No 102 - Oracle Support - Start
			ParaColl.clear();
			ParaColl.puts("@EffectDate", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today), Types.DATE);
			//Point No 102 - Oracle Support - end


			rowset rs_values = null;
			bool bln_stop = true;

			int NO_VALUE_SERIAL = - 1;
			
			try
			{
				System.Console.Out.WriteLine("Matching ValueCombination: " + str_sql);

				//Point No 102 - Oracle Support - Start
				//rs_values = DB.executeQuery(str_sql);
				rs_values = DB.executeQuery(str_sql,ParaColl);
				//Point No 102 - Oracle Support - End

				bln_stop = (rs_values == null || rs_values.size() <= 0);
				System.Console.Out.WriteLine("Records found: " + rs_values.size());
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: "  + str_sql + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
				throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to get Serial number from value comb. \n" + e.Message);
			}
			finally
			{
				if (bln_stop)
				{
					try
					{
						if (rs_values != null)
							rs_values.close();
					}
					catch (OleDbException e)
					{
					}
					rs_values = null;
				}
			}
			
			if (bln_stop)
			{
				//////////////////////////////////////////////
				/// Point No 81 - ELSE condition in VC - start
				//////////////////////////////////////////////

				// ************************************************************************
				// check for default value combination
				//String str_defaultVC = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_GS_GLOBALSETTING","SGS_DEFAULTSTRING", "WHERE SGS_SRNO=1");
				
				//if (str_defaultVC != null && str_defaultVC.Length>0 && !str_defaultVC.Equals(str_valueCombination))
				//	return fsgetValueCombSerialNo(str_appCode, str_processId, str_defaultVC, str_arrKeyFields, obj_colFields);

				// ************************************************************************

				//////////////////////////////////////////////
				/// Point No 81 - ELSE condition in VC - end
				//////////////////////////////////////////////
				return NO_VALUE_SERIAL;
			}
			
			
			// we have some records
			int int_returnValue = NO_VALUE_SERIAL;
			bool bln_runLoop = true;
			
			try
			{
				while (rs_values.next() && bln_runLoop)
				{
					int_returnValue = rs_values.getInt("SVC_SRNO");
					String str_field = rs_values.getString("SFS_FIELDCODE");
					String str_opCode = rs_values.getString("SOS_OPCODE");
					String str_operandFrom = rs_values.getString("SVC_OPERAND1");
					String str_operandTo = rs_values.getString("SVC_OPERAND2");
					
					
					/* ------ direct changes in dot net -------- */
					if (str_field == null || str_field.Length == 0)
					{
						bln_runLoop = false;
						break;
					}
					/* ------ -------------------------- -------- */
					
					//  ********************* Change ON 25 April 2005 ********************
					FormulaField obj_formulaField = new FormulaField(str_appCode, str_field);
					Object obj_fieldValue = obj_formulaField.fsevaluateValueOfField(str_arrKeyFields, obj_colFields);
					
					//Object obj_fieldValue = fsevaluateValueOfField(str_appCode, str_field, str_arrKeyFields, obj_colFields);
					//  ******************************************************************
					System.Console.Out.WriteLine("Value of field for matching ValueCombination is: " + obj_fieldValue + " (" + obj_fieldValue.GetType().FullName + ")");
					
					
					if (obj_fieldValue is DateTime)
					{
						System.Console.Out.WriteLine("obj_fieldValue is a date");
						
						//UPGRADE_TODO: The 'DateTime' structure does not have an equivalent to NULL. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1291_3"'
						DateTime dt_dateFrom = DateTime.Now;
						if (str_operandFrom != null)
							dt_dateFrom = SHSM_DateTimeManager.fsgetDateFromString(SHSM_UserOperation.FORMAT_DATE, str_operandFrom);
						
						//UPGRADE_TODO: The 'DateTime' structure does not have an equivalent to NULL. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1291_3"'
						DateTime dt_dateTo  = DateTime.Now;
						if (str_operandTo != null)
							dt_dateTo = SHSM_DateTimeManager.fsgetDateFromString(SHSM_UserOperation.FORMAT_DATE, str_operandTo);
						
						DateTime dt_fieldValue = (DateTime) obj_fieldValue;
						//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
						bln_runLoop = !fsparseCondition(str_opCode,  dt_dateFrom,  dt_dateTo,  dt_fieldValue);
					}
					else if (obj_fieldValue is String)
					{
						bln_runLoop = !fsparseCondition(str_opCode, str_operandFrom, str_operandTo, (String) obj_fieldValue);
					}
					else if (obj_fieldValue is System.ValueType)
					{
						System.Console.Out.WriteLine("obj_fieldValue is a double");
						
						double dbl_valueFrom = 0;
						if (str_operandFrom != null)
							dbl_valueFrom = System.Double.Parse(str_operandFrom);
						
						double dbl_valueTo = 0;
						if (str_operandTo != null)
							dbl_valueTo = System.Double.Parse(str_operandTo);
						
						double dbl_fieldValue = System.Convert.ToDouble(((System.ValueType) obj_fieldValue));
						bln_runLoop = !fsparseCondition(str_opCode, dbl_valueFrom, dbl_valueTo, dbl_fieldValue);
					}
				}
				if (bln_runLoop)
					int_returnValue = NO_VALUE_SERIAL;
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to get serial number from value comb. \n" + e.Message);
			}
			catch (Exception e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
				throw new ProcessException("Security Module: \\n" + "Error: Failed to get serial number from value comb. \n" + e.Message);
			}
			finally
			{
				try
				{
					if (rs_values != null)
						rs_values.close();
				}
				catch (Exception e)
				{
				}
				rs_values = null;
			}
			
			
			return int_returnValue;
		}
		
		
		
		private static bool fsparseCondition(String str_opCode, String str_operandFrom, String str_operandTo, String str_value)
		{
			if (str_opCode.Equals(OP_EQUALS))
			{
				return str_value.Equals(str_operandFrom);
			}
			else if (str_opCode.Equals(OP_NOTEQUALS))
			{
				return !str_value.Equals(str_operandFrom);
			}
			else if (str_opCode.Equals(OP_LESSER))
			{
				return String.CompareOrdinal(str_value, str_operandTo) < 0;
			}
			else if (str_opCode.Equals(OP_GREATER))
			{
				return String.CompareOrdinal(str_value, str_operandFrom) > 0;
			}
			else if (str_opCode.Equals(OP_LESSOREQUAL))
			{
				return String.CompareOrdinal(str_value, str_operandTo) <= 0;
			}
			else if (str_opCode.Equals(OP_GREATEROREQUAL))
			{
				return String.CompareOrdinal(str_value, str_operandFrom) >= 0;
			}
			else if (str_opCode.Equals(OP_BETWEEN))
			{
				return (String.CompareOrdinal(str_operandFrom, str_value) <= 0 && String.CompareOrdinal(str_operandTo, str_value) >= 0);
			}
			
			return false;
		}
		
		private static bool fsparseCondition(String str_opCode, double dbl_operandFrom, double dbl_operandTo, double dbl_value)
		{
			if (str_opCode.Equals(OP_EQUALS))
			{
				return dbl_value == dbl_operandFrom;
			}
			else if (str_opCode.Equals(OP_NOTEQUALS))
			{
				return dbl_value != dbl_operandFrom;
			}
			else if (str_opCode.Equals(OP_LESSER))
			{
				return dbl_value < dbl_operandTo;
			}
			else if (str_opCode.Equals(OP_GREATER))
			{
				return dbl_value > dbl_operandFrom;
			}
			else if (str_opCode.Equals(OP_LESSOREQUAL))
			{
				return dbl_value <= dbl_operandTo;
			}
			else if (str_opCode.Equals(OP_GREATEROREQUAL))
			{
				return dbl_value >= dbl_operandFrom;
			}
			else if (str_opCode.Equals(OP_BETWEEN))
			{
				return (dbl_value >= dbl_operandFrom && dbl_value <= dbl_operandTo);
			}
			
			return false;
		}
		
		private static bool fsparseCondition(String str_opCode, DateTime dt_operandFrom,  DateTime dt_operandTo, DateTime dt_value)
		{
			if (str_opCode.Equals(OP_EQUALS))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandFrom) == 0;
			}
			else if (str_opCode.Equals(OP_NOTEQUALS))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandFrom) != 0;
			}
			else if (str_opCode.Equals(OP_LESSER))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandTo) < 0;
			}
			else if (str_opCode.Equals(OP_GREATER))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandTo) > 0;
			}
			else if (str_opCode.Equals(OP_LESSOREQUAL))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandTo) <= 0;
			}
			else if (str_opCode.Equals(OP_GREATEROREQUAL))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fscompareDateIgnoreTime( dt_value,  dt_operandTo) >= 0;
			}
			else if (str_opCode.Equals(OP_BETWEEN))
			{
				//UPGRADE_NOTE:  keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
				return SHSM_DateTimeManager.fsisDateInPeriodInclusiveIgnoreTime( dt_operandFrom,  dt_operandTo,  dt_value);
			}
			
			return false;
		}
		
		
		
		private static String fsgetFirstForwardingLevelCode(String str_appCode, String str_processid, int int_vcSerialNumber)
		{
			String str_condition= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SFC_AUFCPROCESSID='" + str_processid + "'" 
				+ " AND SVC_SRNO=" + int_vcSerialNumber
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;

			String str_vQuery = " AND SUL_LEVELCODE LIKE 'V%' ";
			String str_orderBy = " ORDER BY SVC_SRNO";

			String str_returnValue = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition + str_vQuery + str_orderBy);

			if (str_returnValue == null) 
			{
				String str_aQuery = " AND (SUL_LEVELCODE LIKE 'A%' OR SUL_LEVELCODE LIKE 'D%')";
				str_returnValue = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition + str_aQuery + str_orderBy);
			}

			return str_returnValue ;
		}
		
		
		private static String fsgetNextForwardingLevelCode(String str_appCode, String str_processid, int int_vcSerialNumber, String str_userLevelCode)
		{
			String str_value = null;
			String str_condition= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SFC_AUFCPROCESSID='" + str_processid + "'" 
				+ " AND SVC_SRNO=" + int_vcSerialNumber
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;

			
			char chr_levelCode = str_userLevelCode[0];
			chr_levelCode = System.Char.ToUpper(chr_levelCode);
			bool bln_isLastVLevel = false;
			
			
			if (chr_levelCode == 'V')
			{
				// get next verification level
				//char chr_level = str_userLevelCode.charAt(1);
				String str_nextLevelQuery = " AND SUL_LEVELCODE LIKE 'V%' " + " AND SUL_LEVELCODE > '" + str_userLevelCode + "'";
				
				String str_nextLevel = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition + str_nextLevelQuery);
				
				if (str_nextLevel == null || str_nextLevel.Equals(""))
					bln_isLastVLevel = true;
				else
					str_value = str_nextLevel;
			}
			
			
			if (chr_levelCode == 'A' || bln_isLastVLevel)
			{
				// get approval level
				String str_approvalQuery = " AND SUL_LEVELCODE LIKE 'A%' ";
				str_value = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition + str_approvalQuery);
			}
			
			return str_value;
		}

		
		private static String fsgetNextVerificationLevelCode(String str_appCode, String str_processid, int int_vcSerialNumber, String str_userLevelCode)
		{
			String str_value = null;
			String str_condition= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SFC_AUFCPROCESSID='" + str_processid + "'" 
				+ " AND SVC_SRNO=" + int_vcSerialNumber + " AND SUL_LEVELCODE LIKE 'V%' " + " AND SUL_LEVELCODE > '" + str_userLevelCode + "'"
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
			
			String str_nextLevel = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition);
			
			
			return str_nextLevel;
		}
		
		
		private static String fsgetApprovalLevelCode(String str_appCode, String str_processid, int int_vcSerialNumber)
		{
			String str_value = null;
			String str_condition= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SFC_AUFCPROCESSID='" + str_processid + "'" 
				+ " AND SVC_SRNO=" + int_vcSerialNumber 
				+ " AND SUL_LEVELCODE LIKE 'A%' "
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
			
			str_value = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition);
			
			return str_value;
		}
		
				
		
		/* //moved to utility
		private static String fsgetExceutableQuery(String str_query, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			int int_start = 0;
			int int_end = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'CHR_TOKEN '. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1003_3"'
			char CHR_TOKEN = '~';
			EnvHelper obj_helper = new EnvHelper();

			
			String str_newQuery = "";
			
			int_start = str_query.IndexOf((System.Char) CHR_TOKEN) + 1;
			//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101_3"'
			int_end = str_query.IndexOf((System.Char) CHR_TOKEN, int_start);
			
			
			if (int_start > 0)
				str_newQuery = str_query.Substring(0, (int_start - 1) - (0));
			else
				str_newQuery = str_query;
			
			while (int_start > 0 && int_end > 0)
			{
				
				String str_key = str_query.Substring(int_start, (int_end) - (int_start));
				
				// we have the key here and now append it to geneated sql
				Object obj_value = null;
				
				// decide if the key is from session or one of the key field of the screen
				if (str_key.StartsWith("s_") || str_key.StartsWith("S_")) 
					obj_value = obj_helper.getAttribute(str_key);
				else
					obj_value = obj_colFields.getObject(str_key);
				
				if (obj_value == null)
					throw new ProcessException("KEY: "+ str_key +" used in routing query is not in routing query format");
				else if (obj_value is DateTime)
					str_newQuery = str_newQuery + "'" + SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE,  (DateTime)obj_value) + "'";
				else if (obj_value is String)
				{
					str_newQuery = str_newQuery + "'" + obj_value + "'";
				}
				else if (obj_value is System.ValueType)
				{
					str_newQuery = str_newQuery + obj_value;
				}
				
				int_start = str_query.IndexOf((System.Char) CHR_TOKEN, int_end + 1) + 1;
				
				if (int_start > 0)
					str_newQuery += str_query.Substring(int_end + 1, (int_start - 1) - (int_end + 1));
				else
					str_newQuery += str_query.Substring(int_end + 1);
				
				
				int_end = str_query.IndexOf((System.Char) CHR_TOKEN, int_start);
			}
			
			System.Console.Out.WriteLine("Generated Query is " + str_newQuery);
			return str_newQuery;
		}
		*/ //moved to utility
		
		private static Object[] fsgetDateAndActivityFromInbox(String str_userCode, String str_appCode, String str_tableCode, String str_docRef)
		{

			Object[] obj_array = new Object[2];

			//T00005 --- OK 1
			String str_query = " SELECT SIN_DATETIME,SAV_ACTCODE  FROM SH_SM_IN_INBOX "
				+ " WHERE "
				+ " SAA_APPCODE='" + str_appCode +"' "
				+ " AND SAB_TABLECODE='" + str_tableCode +"' "
				+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") +"' "
				+ " AND SIN_STATUS='U' AND "
				+ " ( "
				+ " ( SIN_TYPE !='R' AND exists(SELECT 'V' FROM SH_SM_RG_ROUTINGGROUPVIEW RG WHERE RG.saa_appcode = SH_SM_IN_INBOX.sin_appcode and RG.sug_groupcode = SH_SM_IN_INBOX.sug_groupcode AND RG.SUS_USERCODE = '" + str_userCode + "') )" 
				+ " OR "
				+ " ( SIN_TYPE ='R' AND SUS_USERCODE = '" + str_userCode + "') " 
				+ " ) ";



			
			rowset rs_inbox = null;
			try
			{
				rs_inbox = DB.executeQuery(str_query);
				if (rs_inbox != null && rs_inbox.next())
				{
					//CDNSoracle obj_array[0] = rs_inbox.getDate("SIN_DATETIME");
					obj_array[0] = rs_inbox.getString("SIN_DATETIME");
					obj_array[1] = rs_inbox.getString("SAV_ACTCODE");
				}
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: "  + str_query + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to get date and activity from inbox \n" + e.Message);
			}
			finally
			{
				try
				{
					if (rs_inbox != null)
						rs_inbox.close();
				}
				catch (OleDbException e)
				{
				}
				rs_inbox = null;
			}

			return obj_array;		
		}
		//MC0033-6 Start 31/10/2005
		//private static void  fscreateDocumentLogEntry(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_userLevelCode, String str_processId, int int_vcSerialNumber, String str_docRefUI)
		private static void  fscreateDocumentLogEntry(String str_entityID, String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_userLevelCode, String str_processId, int int_vcSerialNumber, String str_docRefUI, String str_reason, NameValueCollection obj_colFields, String[] str_arrKeyFields)
			//MC0033-6 End 31/10/2005
		{
			String str_sql = null;
			//ASIF OK
			String str_date_ui = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_date);
			
			//V0001 -- Start
			//String str_date_sec = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC, dt_date);
			String str_date_sec = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC, str_date);
			//V0001 -- End

			//String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_date);
			String str_remarks = null;
			
			bool bln_isApproved = false;
			char chr_level = str_userLevelCode[0];
			chr_level = System.Char.ToUpper(chr_level);
			

			
			String str_wrapperLevel = str_userLevelCode;



			if (LEVEL_APPROVAL_DIRECT.Equals(str_wrapperLevel))
				str_wrapperLevel = LEVEL_APPROVED;

			str_remarks = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_UL_USERLEVEL","SUL_DESC", "WHERE SUL_LEVELCODE='" + str_userLevelCode + "'");
			
			bln_isApproved = ( str_userLevelCode.ToUpper().Equals(LEVEL_APPROVED ) );
			
			//ASIF OK
			//T00005 --- OK 1

			EnvHelper sessionValues = new EnvHelper();
			System.String str_orgaCode = (System.String) sessionValues.getAttribute("s_POR_ORGACODE");
			System.String str_locaCode = (System.String) sessionValues.getAttribute("s_PLC_LOCACODE");


			//2006/03/17 - Setting Inbox PKs in session - start
			if (str_reason=="")
			{
				str_reason=fsgetAccumulatedRemarks(str_appCode, str_tableCode, str_docRef,   str_reason , str_userCode, str_entityID);
			}
			//2006/03/17 - Setting Inbox PKs in session - end


			///Point No. 80 <<<<< reason & remarks
			str_sql = " INSERT INTO SH_SM_DL_DOCUMENTLOG " 
				//MC0033-6 sTART 31/10/2005
				//+ " (SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SDL_DOCREF, SUS_USERCODE, SDL_DATETIME, SDL_DATETIMEACTUAL, SUL_LEVELCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDL_REMARKS, SDL_DOCREFUI) "
				+ " (POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SDL_DOCREF, SUS_USERCODE, SDL_DATETIME, SDL_DATETIMEACTUAL, SUL_LEVELCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDL_REMARKS, SDL_DOCREFUI,SDL_REASON) "
				//MC0033-6 END 31/10/2005
				+ " VALUES ("
				+ "'" + str_orgaCode + "', "
				+ "'" + str_locaCode + "', "
				+ "'" + str_appCode + "', "
				+ "'" + str_activityCode + "', " 
				+ "'" + str_tableCode + "', " 
				+ "'" + StringUtility.fsreplace(str_docRef,"'","''") + "', " 
				+ "'" + str_userCode + "', " 
				+ "'" + str_date_ui + "', " 
				// V0001 -- start
				//+ "'" + str_date + "', " 
				//Point No 102 - Oracle Support - Start
				//+ "'" + str_date_sec + "', "
				+ "?, "
				//Point No 102 - Oracle Support - End
				// V0001 -- end
				+ "'" + str_wrapperLevel + "'";

			if (str_processId == null)
				str_sql += ", NULL" ;
			else
				str_sql += ", '" + str_processId + "'";
				
			if (int_vcSerialNumber == -1)
				str_sql += ", NULL" ;
			else
				str_sql += ", " + int_vcSerialNumber ;
			
			///Point No. 80 <<<<<
			//str_sql = str_sql + ", '" + str_remarks + "','" + StringUtility.fsreplace(str_docRefUI,"'","''") + "','" + str_reason + "')";	
			str_sql = str_sql + ", " + (str_remarks==null ? "NULL" : "'"+str_remarks+"'") + ",'" + StringUtility.fsreplace(str_docRefUI,"'","''") + "'," + (str_reason==null ? "NULL" : "'"+str_reason+"'") + ")";
			
			//			//T00005 --- OK 1
			//			str_sql = str_sql + ", "
			//				+ "'" + str_remarks + "',"
			//				+ "'" + StringUtility.fsreplace(str_docRefUI,"'","''") + "')"
			//				//MC0033-6 sTART 31/10/2005
			//				+ "'" +str_reason + "'";
			//			//MC0033-6 END 31/10/2005

			
			//Point No 102 - Oracle Support - Start
			ParaColl.clear();
			ParaColl.puts("@SDL_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_date_sec), Types.TIMESTAMP);
			//Point No 102 - Oracle Support - end


			try
			{
				//Point No 102 - Oracle Support - Start
				//DB.executeDML(str_sql);
				DB.executeDML(str_sql,ParaColl);
				//Point No 102 - Oracle Support - End

				// MC0033 Start 28/10/2005
				fscreateDocumentLogDetail(str_entityID, str_arrKeyFields, obj_colFields, str_appCode, str_tableCode, str_docRef, str_date_ui, str_date_sec, str_userLevelCode);
				//MC0033 end 28/10/2005
				
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query\nQurey: "  + str_sql + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to create document log entry. \n" + e.Message);
			}

			if (bln_isApproved)
				fsmoveRoutingRecordsInHistory(str_appCode, str_tableCode, str_docRef);

		}
		
		private static void  fsmarkInboxEntryAsRead(String str_userCode, String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_date)
		{
			//T00005 --- OK 1
			String str_where = " WHERE SAA_APPCODE='" + str_appCode + "' AND SAB_TABLECODE='" + str_tableCode + "'" + " AND SIN_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'" + " AND SIN_DATETIME ='" + str_date + "'";

			if (str_activityCode != null)
				str_where += " AND SAV_ACTCODE='" + str_activityCode + "'" ;

			
			SHSM_Utility.fsupdateColumn("SH_SM_IN_INBOX", "SIN_STATUS", "R", str_where);

			//fsmoveInboxEntriesToHistory(str_appCode, str_activityCode, str_tableCode, str_docRef, str_date);


		}

		private static void  fsmoveInboxEntriesToHistory( String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_date)
		{
			//			
			//			String str_select	= " SELECT  " 
			//								+ " SUS_USERCODE, SIN_USERCODE, SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SIN_DOCREF, SIN_SUBJECT, SIN_DATETIME, SIN_FORWARDSTATUS, SIN_TYPE, SIN_DOCREFUI "
			//								+ " FROM SH_SM_IN_INBOX ";
			//
			//T00005 --- OK 1
			//			String str_where  = " WHERE SAA_APPCODE='" + str_appCode + "'"
			//									+ " AND SAB_TABLECODE='" + str_tableCode + "'" 
			//									+ " AND SIN_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'" 
			//									+ " AND SIN_DATETIME ='" + str_date + "'";
			//
			//			if (str_activityCode != null)
			//				str_where += " AND SAV_ACTCODE='" + str_activityCode + "'" ;
			//
			//			//T00005 OK ???
			//			String str_query= " INSERT INTO SH_SM_IH_INBOXHISTORY " 
			//							+ " ( SUS_USERCODE, SIH_USERCODE, SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SIH_DOCREF, SIH_SUBJECT, SIH_DATETIME, SIH_FORWARDSTATUS, SIH_TYPE, SIH_DOCREFUI ) "
			//							+ str_select 
			//							+ str_where; 
			//
			//			try
			//			{
			//				DB.executeDML(str_query);
			//				//DB.executeDML(" DELETE FROM SH_SM_IN_INBOX "        + str_where);
			//			}
			//			catch (OleDbException e)
			//			{
			//				SupportClass.WriteStackTrace(e, Console.Error);
			//				throw new ProcessException("Security Module: \\n" + "Error Executing query\nQurey: "  + str_query + "\nError: " + e.Message);
			//			}
			//			catch (Exception e)
			//			{
			//				SupportClass.WriteStackTrace(e, Console.Error);
			//				throw new ProcessException("Security Module: \\n" + e.Message);
			//			}

		}



		private static void  fsmoveDocumentLogsToHistory(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef)
		{
			//			String str_sql = null;
			//			
			//			String str_select	= " SELECT " 
			//								+ "SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SDL_DOCREF, SUS_USERCODE, SDL_DATETIME, SUL_LEVELCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDL_REMARKS, SDL_DOCREFUI "
			//								+ " FROM SH_SM_DL_DOCUMENTLOG ";
			//T00005 --- OK 1
			//			String str_where  = " WHERE SAA_APPCODE='" + str_appCode + "'"
			//							  + " AND SAB_TABLECODE='" + str_tableCode + "'" 
			//							  + " AND SDL_DOCREF LIKE '" + StringUtility.fsreplace(str_docRef,"'","''") + "'"; 
			//
			//			if (str_activityCode != null)
			//				str_where += " AND SAV_ACTCODE='" + str_activityCode + "'" ;
			////T00005 --- OK ???
			//			String str_query= "INSERT INTO  sh_sm_dh_documenthistory " 
			//							+ " (SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SDH_DOCREF, SUS_USERCODE, SDH_DATETIME, SUL_LEVELCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDH_REMARKS, SDH_DOCREFUI) "
			//							+ str_select  + str_where; 
			//
			//			try
			//			{
			//				DB.executeDML(str_query);
			//				//DB.executeDML("DELETE FROM  sh_sm_dl_documentlog " + str_where);
			//			}
			//			catch (OleDbException e)
			//			{
			//				SupportClass.WriteStackTrace(e, Console.Error);
			//				throw new ProcessException("Security Module: \\n" + e.Message);
			//			}
			//			catch (Exception e)
			//			{
			//				SupportClass.WriteStackTrace(e, Console.Error);
			//				throw new ProcessException("Security Module: \\n" + e.Message);
			//			}
		}


		private static String fsgetApprovalLevelDefined(String str_appCode, String str_processId, int int_valueSerial)
		{
			String str_condition= " WHERE SAA_APPCODE='" + str_appCode + "'"
				+ " AND SFC_AUFCPROCESSID='" + str_processId + "'" 
				+ " AND SVC_SRNO=" + int_valueSerial
				+ " AND (SUL_LEVELCODE='" + LEVEL_APPROVAL + "' OR SUL_LEVELCODE = '" + LEVEL_APPROVAL_DIRECT + "')"
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;


			String str_level =  SHSM_Utility.fsgetColumnAgainstQuery( "SH_SM_RC_RANGECOMB", "SUL_LEVELCODE", str_condition);
			
			if (LEVEL_APPROVAL.Equals(str_level))
				return LEVEL_APPROVAL;
			else if (LEVEL_APPROVAL_DIRECT.Equals(str_level))
				return LEVEL_APPROVAL_DIRECT;

			return null;

		}


		private static String fsapproveTransactionDirect(String str_entityId, String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_docRefUI, String str_reason, NameValueCollection obj_colFields, String[] str_arrKeyFields)
		{
			return fsapproveTransactionDirect(str_entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode, str_date, str_docRefUI, null, -1,str_reason,obj_colFields, str_arrKeyFields);
		}
		//MC0033-6 Start 31/10/2005
		//private static String fsapproveTransactionDirect(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_docRefUI, String str_processId, int int_vcSerial)
		private static String fsapproveTransactionDirect(String entityId, String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_docRefUI, String str_processId, int int_vcSerial, String str_reason, NameValueCollection obj_colFields, String[] str_arrKeyFields)
			//MC0033-6 End 31/10/2005
		{
			//	Create one record in Document Log with 'Approval' status
			String str_userLevelNext = LEVEL_APPROVAL_DIRECT;			
			//MC0033-6 Start 31/10/2005
			//fscreateDocumentLogEntry(str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_date, str_userLevelNext, str_processId, int_vcSerial, str_docRefUI);
			fscreateDocumentLogEntry(entityId, str_appCode, str_activityCode, str_tableCode, str_docRef, str_userCode,  str_date, str_userLevelNext, str_processId, int_vcSerial, str_docRefUI, str_reason, obj_colFields, str_arrKeyFields);
			//MC0033-6 End 31/10/2005

			//	Return transaction status to approval option
			return LEVEL_APPROVED;
		}


		private static bool fsisNewDocument(String str_appCode, String str_tableCode, String str_docRef  )
		{
	
			//T00005 --- OK 1
			String str_innerQuery	= " SELECT sus_usercode from sh_sm_dl_documentlog "
				+ "WHERE Sdl_DOCREF='"+ StringUtility.fsreplace(str_docRef,"'","''") + "' "
				+ " AND saa_appcode='"+ str_appCode +"' "
				+ " AND sab_tablecode='"+ str_tableCode +"' AND SUL_LEVELCODE <> 'N1' ";

			//ASIF OK
			//T00005 --- OK 1
			String str_inboxQuery	= "SELECT MAX(l.sdl_datetimeACTUAL) from sh_sm_dl_documentlog l "
				+ "where l.Sdl_DOCREF='"+ StringUtility.fsreplace(str_docRef,"'","''") +"' "
				+ " AND l.saa_appcode='"+ str_appCode +"' "
				+ " AND l.sab_tablecode='" + str_tableCode + "'";

			//ASIF OK
			String str_where		= " WHERE "
				+ " EXISTS (" + str_innerQuery + ")"
				+ " AND NOT EXISTS  (" + str_innerQuery 
				//--Improvement point 23
				//+ " AND sul_levelcode IN ('"+ LEVEL_REJECTED +"','" + LEVEL_APPROVED + "')" 
				+ " AND sul_levelcode IN ('"+ LEVEL_REJECTED +"','" + LEVEL_APPROVED + "', 'U1' )" 
				+ " AND SDL_DATETIMEACTUAL=(" + str_inboxQuery + ") "
				+ ")";

			return !SHSM_Utility.fsrecordExists("SH_SM_DL_DOCUMENTLOG", str_where );
		}




		public static String[][] fsgetAllForwardingUsersWithFlag(String str_appCode, String str_processid, int int_vcSerialNumber, String str_userLevelCode)
		{
			
			//-- MC New Routing -----
			//rowset rs_users = null;
			rowset rs_userGroups = null;
			
			/*
			(
			SELECT distinct (u.SUS_USERCODE), r.SRC_SRNO FROM SH_SM_US_USER u, SH_SM_RC_RANGECOMB r
			WHERE
			r.SAA_APPCODE='98' AND r.SAA_APPCODE=u.SAA_APPCODE AND r.SUG_GROUPCODE=u.SUG_GROUPCODE AND
			SFC_AUFCPROCESSID='ACQ' AND SVC_SRNO=1 AND SUL_LEVELCODE='V1'
			
			UNION
			SELECT g.SUS_USERCODE, r.SRC_SRNO FROM SH_SM_UF_USERAUTGROUP g, SH_SM_RC_RANGECOMB r
			WHERE
			r.SAA_APPCODE='98' AND g.SAA_APPCODE=r.SAA_APPCODE AND g.SUG_GROUPCODE=r.SUG_GROUPCODE AND
			SFC_AUFCPROCESSID='ACQ' AND SVC_SRNO=1 AND SUL_LEVELCODE='V1'
			)
			UNION
			
			SELECT r.SUS_USERCODE, r.SRC_SRNO FROM SH_SM_RC_RANGECOMB r
			WHERE
			r.SAA_APPCODE='98' AND r.SFC_AUFCPROCESSID='ACQ' AND r.SVC_SRNO=1 AND r.SUL_LEVELCODE='V1'
			AND r.SUS_USERCODE IS NOT NULL
			*/
			
			/*
			If a user is selected from a group as well as its individual record is also defined
			in forward list, then the smaller src_serial number will take preference
			
			*/
			
			String str_condition = " AND SFC_AUFCPROCESSID='" + str_processid + "' " + " AND SVC_SRNO=" + int_vcSerialNumber + " AND SUL_LEVELCODE='" + str_userLevelCode + "'";
			
			//MC0033 start 28/10/2005
			//String str_grpUsers = " SELECT distinct (u.SUS_USERCODE), r.SRC_ALLOWEDIT FROM SH_SM_US_USER u, SH_SM_RC_RANGECOMB r " + " WHERE r.SAA_APPCODE='" + str_appCode + "' " + " AND r.SAA_APPCODE=u.SAA_APPCODE AND r.SUG_GROUPCODE=u.SUG_GROUPCODE " + str_condition + " UNION " + " SELECT g.SUS_USERCODE, r.SRC_ALLOWEDIT FROM SH_SM_UF_USERAUTGROUP g, SH_SM_RC_RANGECOMB r " + " WHERE r.SAA_APPCODE='" + str_appCode + "' " + " AND r.SAA_APPCODE=g.SAA_APPCODE AND r.SUG_GROUPCODE=g.SUG_GROUPCODE " + str_condition;
			//String str_users = " SELECT SUS_USERCODE, SRC_ALLOWEDIT FROM SH_SM_RC_RANGECOMB " + " WHERE SAA_APPCODE='" + str_appCode + "' " + str_condition + " AND SUS_USERCODE IS NOT NULL ";
			
			//-- MC New Routing -----
			//String str_users = " SELECT SUG_GROUPCODE, SRC_ALLOWEDIT FROM SH_SM_RC_RANGECOMB " + " WHERE SAA_APPCODE='" + str_appCode + "' " + str_condition + " AND SUG_GROUPCODE IS NOT NULL ";
			String str_userGroups = " SELECT SRC_APPCODE, SUG_GROUPCODE, SRC_ALLOWEDIT FROM SH_SM_RC_RANGECOMB  WHERE SAA_APPCODE='" + str_appCode + "' " + str_condition;
			
			//String str_sql = "(" + str_grpUsers + ") UNION " + str_users;
			String str_sql =  str_userGroups;
			//--System.Console.Out.WriteLine("str_sql FOR GROUP IN FARWARD LIST: " + str_sql);
			//MC0033 End 28/10/2005
			
			//-- MC New Routing -----
			//--String[][] str_arrUsers = null;
			String[][] str_arrUserGroups = null;
			try
			{
				System.Console.Out.WriteLine("INBOX users: " + str_sql);
				rs_userGroups = DB.executeQuery(str_sql);
				if (rs_userGroups != null)
				{
					//-- MC New Routing -----
					//--ArrayList obj_users = new ArrayList();
					ArrayList obj_userGroups = new ArrayList();
					int int_size = rs_userGroups.size();
					for (int i = 0; i < int_size; i++)
					{
						rs_userGroups.next();
						//-- MC New Routing -----start
						//String str_userCode = rs_users.getString("SUS_USERCODE");
						//String str_allowedit = rs_users.getString("SRC_ALLOWEDIT");
						String str_toAppCode = rs_userGroups.getString("SRC_APPCODE");
						String str_toGroupCode = rs_userGroups.getString("SUG_GROUPCODE");
						String str_allowedit = rs_userGroups.getString("SRC_ALLOWEDIT");
						//-- MC New Routing -----end

						//-- MC New Routing -----start
						//if (fsisValidForwardingUser(ref obj_users, str_userCode)) 
						//{
						if (str_allowedit == null)
							str_allowedit = "N";
						//--obj_users.Add(new String[]{str_userCode, str_allowedit});
						obj_userGroups.Add(new String[]{str_toAppCode, str_toGroupCode, str_allowedit});
						//}
						//-- MC New Routing -----END
					}
					//str_arrUsers = new String[obj_users.Count][];
					str_arrUserGroups = new String[obj_userGroups.Count][];
					SupportClass.ICollectionSupport.ToArray( obj_userGroups , str_arrUserGroups );
				}
				System.Console.Out.WriteLine("Records found: " + rs_userGroups.size());
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: " + str_sql + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: " + str_sql + "\nError: " + e.Message);
			}
			finally
			{
				try
				{
					if (rs_userGroups != null)
						rs_userGroups.close();
				}
				catch (OleDbException e)
				{
				}
				rs_userGroups = null;
			}
			
			return str_arrUserGroups;
		}
		
		
		
		
		// argument added
		// MC0033-6 Start 31/10/2005
		//private static void  fscreateInboxEntryWithFlag(String str_processId, int int_vcSerialNumber, String str_date, String str_fromUser, String str_toUser, String str_appCode, String str_userLevelCode, String str_activityCode, String str_tableCode, String str_docRef, String str_docRefUI, String str_allowEdit)
		//private static void  fscreateInboxEntryWithFlag(String str_processId, int int_vcSerialNumber, String str_date, String str_fromUser, String str_toUser, String str_appCode, String str_userLevelCode, String str_activityCode, String str_tableCode, String str_docRef, String str_docRefUI, String str_allowEdit, String str_reason)
		//-- MC New Routing -----

		
		//private static void  fscreateInboxEntryWithFlag(String str_processId, int int_vcSerialNumber, String str_date, String str_fromUser, String str_toAppCode, String str_toGroupCode, String str_toUser, String str_appCode, String str_userLevelCode, String str_activityCode, String str_tableCode, String str_docRef, String str_docRefUI, String str_allowEdit, String str_reason)
		// MC0033-6 End 31/10/2005
		/************* Navigation in Inbox **********************/
		private static void  fscreateInboxEntryWithFlag(String str_entityId, String str_processId, int int_vcSerialNumber, String str_date, String str_fromUser, String str_toAppCode, String str_toGroupCode, String str_toUser, String str_appCode, String str_userLevelCode, String str_activityCode, String str_tableCode, String str_docRef, String str_docRefUI, String str_allowEdit, String str_reason)
		{
			String str_date_ui = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_date);
			//V0001 Start
			String str_date_sec = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC, str_date);
			//V0001 End
			String str_sql = null;
			String str_subject = null;
			
			char chr_status = System.Char.ToUpper(str_userLevelCode[0]);
			
			str_subject = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_UL_USERLEVEL", "SUL_DESC", "WHERE SUL_LEVELCODE='" + str_userLevelCode + "'");
			str_subject = "Fwd: " + str_subject;
			
			//			String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP, dt_date);
			
			/************* Navigation in Inbox - start **********************/
			//String str_oldRouting = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_TR_TRANSROUTEENTITY", "STR_OLDROUTING", "WHERE PSE_ENTITYID='" + str_userLevelCode + "'");
			EnvHelper sessionValues = new EnvHelper();
			//String str_progTypeCode = (String) sessionValues.getAttribute("s_SPT_PRGTYPECODE") ;
			//String str_optCode = (String) sessionValues.getAttribute("s_SAO_OPTCODE");
			//String str_subOptCode = (String) sessionValues.getAttribute("s_SAN_SUBOPTCODE");
			//String str_source = (String) sessionValues.getAttribute("s_SAN_SOURCE");
			//String str_levelCode = (String) sessionValues.getAttribute("s_SAM_LEVELCODE");
			String str_progTypeCode = "'" + sessionValues.getAttribute("s_SPT_PRGTYPECODE").ToString() + "'" ;
			String str_optCode      = "'" + sessionValues.getAttribute("s_SAO_OPTCODE").ToString() + "'" ;
			String str_subOptCode   = "'" + sessionValues.getAttribute("s_SAN_SUBOPTCODE").ToString() + "'" ;
			String str_source       = "'" + sessionValues.getAttribute("s_SAN_SOURCE").ToString() + "'" ;
			String str_levelCode    = "'" + sessionValues.getAttribute("s_SAM_LEVELCODE").ToString() + "'" ;

			//2006/03/17 - Setting Inbox PKs in session
			String str_oldRouting ="N";

			

			try
			{
				
				String[] obj_arrFields={"STR_OLDROUTING","SPT_PRGTYPECODE","SAO_OPTCODE","SAN_SUBOPTCODE","SAN_SOURCE","SAM_LEVELCODE","SPT_PRGTYPECODE_R","SAO_OPTCODE_R","SAN_SUBOPTCODE_R","SAN_SOURCE_R","SAM_LEVELCODE_R"};
				Object[] obj_arrValues = null;

				obj_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_TR_TRANSROUTEENTITY", obj_arrFields, " WHERE PSE_ENTITYID='" + str_entityId + "'");

				//String str_oldRouting ="N";
				if(obj_arrValues[0]!=null)
				{
					str_oldRouting = obj_arrValues[0].ToString() ;
				}
				// Point No 99 - Change in OldRouting Logic (M - Mixed) start
				//if (str_oldRouting=="Y")
				if (str_oldRouting.ToUpper().Equals("Y") || str_oldRouting.ToUpper().Equals("M"))
				{
					if(chr_status!='R') // Approval or Verification
					{
						/*
						if(obj_arrValues[1]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Program Type (Verification/Approval) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[2]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Option Code (Verification/Approval) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[3]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Suboption Code (Verification/Approval) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[4]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Page Source (Verification/Approval) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[5]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Menu Level Code (Verification/Approval) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						*/
						if(obj_arrValues[1]==null || obj_arrValues[2]==null || obj_arrValues[3]==null || obj_arrValues[4]==null || obj_arrValues[5]==null )
						{
							str_progTypeCode = "null";
							str_optCode = "null";
							str_subOptCode = "null";
							str_source = "null";
							str_levelCode = "null";
						}
						else
						{
							str_progTypeCode = "'" +obj_arrValues[1].ToString()+ "'";
							str_optCode = "'" +obj_arrValues[2].ToString()+ "'";
							str_subOptCode = "'" +obj_arrValues[3].ToString()+ "'";
							str_source = "'" +obj_arrValues[4].ToString()+ "'";
							str_levelCode =  "'" +obj_arrValues[5].ToString()+ "'";
						}
					}
					else //Rejection
					{
						/*
						if(obj_arrValues[6]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Program Type (Entry) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[7]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Option Code (Entry) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[8]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Suboption Code (Entry) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[9]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Page Source (Entry) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						if(obj_arrValues[10]==null)
						{
							throw new ProcessException("Security Module: \\n"  + "Menu Level Code (Entry) is not set in Routing Entity Setup for Entity:"+str_entityId);
						}
						*/
						if(obj_arrValues[6]==null || obj_arrValues[7]==null || obj_arrValues[8]==null || obj_arrValues[9]==null || obj_arrValues[10]==null )
						{
							str_progTypeCode = "null";
							str_optCode = "null";
							str_subOptCode = "null";
							str_source = "null";
							str_levelCode = "null";
						}
						else
						{
							str_progTypeCode = "'" + obj_arrValues[6].ToString()+ "'";
							str_optCode = "'" + obj_arrValues[7].ToString()+ "'";
							str_subOptCode = "'" + obj_arrValues[8].ToString()+ "'";
							str_source = "'" + obj_arrValues[9].ToString()+ "'";
							str_levelCode = "'" + obj_arrValues[10].ToString() + "'"  ;
						}
					}			
				}//End of (Check of Old/new routing)
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error in getting Trans Entity information.   \\n " + e.Message);
			}

			
			/************* Navigation in Inbox - end **********************/
			
			try
			{
				//ASIF OK
				//T00005 --- OK 1
				//-- MC New Routing -----
				//				str_sql = " INSERT INTO SH_SM_IN_INBOX " 
				//					+ " (SUS_USERCODE, SIN_USERCODE, SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SIN_DOCREF, SIN_SUBJECT, SIN_DATETIME, SIN_DATETIMEACTUAL, SIN_FORWARDSTATUS, SIN_TYPE, SIN_STATUS, SIN_DOCREFUI, SIN_ALLOWEDIT) "
				//					+ " VALUES (" 
				//					+ "'" + str_toUser + "', " + "'" + str_fromUser + "', " + "'" + str_appCode + "', " + "'" + str_activityCode + "', " + "'" + str_tableCode + "', " + "'" + StringUtility.fsreplace(str_docRef,"'","''") + "', " + "'" + str_subject + "', '" + str_date_ui + "', '" + str_date_sec + "', "  + "'A','" + chr_status + "'," + "'U'," + "'" + StringUtility.fsreplace(str_docRefUI,"'","''") + "'," + "'" + str_allowEdit + "'" + ")";
				
				/************* Navigation in Inbox - start **********************/
				//str_sql = " INSERT INTO SH_SM_IN_INBOX " 
				//	+ " (SIN_APPCODE, SUG_GROUPCODE, SAA_APPCODE, SAB_TABLECODE, SIN_DOCREF, SIN_DATETIME, SUS_USERCODE , SIN_DATETIMEACTUAL, SIN_USERCODE, SAV_ACTCODE, SIN_SUBJECT, SIN_FORWARDSTATUS, SIN_TYPE, SIN_STATUS, SIN_DOCREFUI, SIN_ALLOWEDIT, SIN_REMARKS)"
				//	+ " VALUES (" 
				//	+ "'" + str_toAppCode + "', '" + str_toGroupCode+ "', '" + str_appCode + "', '" + str_tableCode + "', '" + StringUtility.fsreplace(str_docRef,"'","''") + "', '" + str_date_ui + "', '" + str_toUser + "', '" + str_date_sec + "', '" + str_fromUser + "', '" + str_activityCode +  "','" + str_subject + "',  'A', '" + chr_status + "', 'U', '" + str_docRefUI + "', '" + str_allowEdit + "', '" + str_reason+ "')";

				
				//2006/03/17 - Setting Inbox PKs in session - start
				if (str_reason=="")
				{
					str_reason=fsgetAccumulatedRemarks(str_appCode, str_tableCode, str_docRef,  str_reason, str_toUser,str_entityId );
				}
				//2006/03/17 - Setting Inbox PKs in session - end
				
				///Point No. 80 <<<<< SIN_REMARKS,SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE,
				str_sql = " INSERT INTO SH_SM_IN_INBOX " 
						+ " (SIN_APPCODE, SUG_GROUPCODE, SAA_APPCODE, SAB_TABLECODE, SIN_DOCREF, SIN_DATETIME, SUS_USERCODE , SIN_DATETIMEACTUAL, SIN_USERCODE, SAV_ACTCODE, SIN_SUBJECT, SIN_FORWARDSTATUS, SIN_TYPE, SIN_STATUS, SIN_DOCREFUI, SIN_ALLOWEDIT, SIN_REMARKS,SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE, SAM_LEVELCODE )"
						+ " VALUES (" 
						//+ "'" + str_toAppCode + "', '" + str_toGroupCode+ "', '" + str_appCode + "', '" + str_tableCode + "', '" + StringUtility.fsreplace(str_docRef,"'","''") + "', '" + str_date_ui + "', '" + str_toUser + "', '" + str_date_sec + "', '" + str_fromUser + "', '" + str_activityCode +  "','" + str_subject + "',  'A', '" + chr_status + "', 'U', '" + str_docRefUI + "', '" + str_allowEdit + "', '" + str_reason + "', " + str_progTypeCode + ", " + str_optCode  + ", " + str_subOptCode + ", " + str_source + ", " + str_levelCode + ")";
				//Point No 102 - Oracle Support - Start
						+ "'" + str_toAppCode + "', '" + str_toGroupCode+ "', '" + str_appCode + "', '" + str_tableCode + "', '" + StringUtility.fsreplace(str_docRef,"'","''") + "', '" + str_date_sec + "', '" + str_toUser + "', ?, '" + str_fromUser + "', '" + str_activityCode +  "','" + str_subject + "',  'A', '" + chr_status + "', 'U', '" + str_docRefUI + "', '" + str_allowEdit + "', " + (str_reason==null ? "NULL" : "'"+str_reason+"'") +  "," + str_progTypeCode + ", " + str_optCode  + ", " + str_subOptCode + ", " + str_source + ", " + str_levelCode + ")";
				ParaColl.clear();
				ParaColl.puts("@SIN_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_date_sec), Types.TIMESTAMP);
				//Point No 102 - Oracle Support - end


				/************* Navigation in Inbox - end **********************/


				System.Console.Out.WriteLine("INBOX entry: " + str_sql);

				//Point No 102 - Oracle Support - Start
				//DB.executeDML(str_sql);
				DB.executeDML(str_sql,ParaColl);
				//Point No 102 - Oracle Support - End
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: " + str_sql + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to write in Inbox. \n" + e.Message);
			}
		}
		
		
		
		private static bool fsisValidForwardingUser(ref ArrayList  obj_users, String str_userCode)
		{
			if (str_userCode == null || str_userCode.Length == 0)
				return false;
			
			String[] str_userData = null;
			
			for (int i = 0; i < obj_users.Count; i++)
			{
				str_userData = (String[]) obj_users[i];
				if (str_userCode.Equals(str_userData[0]))
					return false;
			}
			
			return true;
		}
		
		
		
		


		public static String fsprocessDMLOperation(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, char str_operation,String str_tableCode, String str_className)
		{
			String str_returnValue = "";
			
			if (!SHSM_Utility.fsrecordExists("SH_SM_TR_TRANSROUTEENTITY", "WHERE PSE_ENTITYID='" + str_entityId + "'"))
				return str_returnValue;

			EnvHelper sessionValues = new EnvHelper();
			String str_appCode			= (String) sessionValues.getAttribute("s_SAA_APPCODE");
			String str_programTypeCode	= (String) sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode		= (String) sessionValues.getAttribute("s_SAO_OPTCODE");
			String str_userCode			= (String) sessionValues.getAttribute("s_SUS_USERCODE");

			String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);
			String str_activityCode = SHSM_Utility.fsgetActivityCode(str_appCode, str_programTypeCode, str_optionCode, str_entityId);

			switch( str_operation)
			{
				case SHSM_AuditTrail.DML_OPERATION_UPDATE:
					str_returnValue = fsprocessUpdate(str_userCode, str_appCode, str_programTypeCode, str_optionCode, str_docRef, str_entityId, str_activityCode, str_operation, str_tableCode, str_className);
					break;

				case SHSM_AuditTrail.DML_OPERATION_INSERT:
					str_returnValue = fsprocessSave(str_userCode, str_appCode, str_programTypeCode, str_optionCode, str_docRef, str_entityId, str_activityCode, str_operation, str_tableCode, str_className);
					break;

				case SHSM_AuditTrail.DML_OPERATION_DELETE:
					str_returnValue = fsprocessDelete(str_appCode, str_programTypeCode, str_optionCode, str_docRef, str_entityId, str_activityCode, str_operation, str_tableCode, str_className);
					break;
			}
			

			return str_returnValue;
		}

		
		
		private static String fsprocessDelete(	String str_appCode, 
			String str_programTypeCode,
			String str_optionCode,
			String str_docRef,
			String str_entityId,
			String str_activityCode,
			char str_operation,
			String str_tableCode,
			String str_className)
		{

			
			String str_where= " WHERE SAA_APPCODE='" + str_appCode + "'"
				+ " AND SAB_TABLECODE='" + str_tableCode + "'";


			if (str_activityCode != null && str_activityCode.Length > 0)
				str_where += " AND SAV_ACTCODE='" + str_activityCode + "'";


			String str_docRefQuery = null;
			String str_query = null;

			try 
			{
				// delete from inbox
				//T00005 --- OK 1
				str_docRefQuery = " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'";
				str_query = "DELETE FROM SH_SM_IN_INBOX " + str_where + str_docRefQuery;
				DB.executeDML(str_query);


				// delete from inbox-history
				
				//T00005 --- OK 1
				str_docRefQuery = " AND SIH_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'";
				str_query = "DELETE FROM SH_SM_IH_INBOXHISTORY " + str_where + str_docRefQuery;
				DB.executeDML(str_query);
			

				// delete from document-log
				//T00005 --- OK 1
				str_docRefQuery = " AND SDL_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'";
				str_query = "DELETE FROM SH_SM_DL_DOCUMENTLOG " + str_where + str_docRefQuery;
				DB.executeDML(str_query);

				// delete from document-history
				//T00005 --- OK 1
				str_docRefQuery = " AND SDH_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "'";
				str_query = "DELETE FROM SH_SM_DH_DOCUMENTHISTORY " + str_where + str_docRefQuery;
				DB.executeDML(str_query);
			}
			catch(Exception e)
			{
				throw new ProcessException("Security Module: \\n" + "Error deleting routing data of deleted transaction: " + e.Message);
			}

			return "D1";
		}




		private static String fsprocessSave(	String str_userCode,
			String str_appCode, 
			String str_programTypeCode,
			String str_optionCode,
			String str_docRef,
			String str_entityId,
			String str_activityCode,
			char str_operation,
			String str_tableCode,
			String str_className)
		{
			String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);		
			//--Improvement point 8 *******************************************************************
			//			fscreateDocumentUpdateEntry(str_appCode, 
			//				str_activityCode,
			//				str_tableCode,
			//				str_docRef,
			//				str_userCode,
			//				str_date,
			//				"S1",
			//				null,
			//				"Saved" );

			return "S1";
		}

		
		private static String fsprocessUpdate(	String str_userCode,
			String str_appCode, 
			String str_programTypeCode,
			String str_optionCode,
			String str_docRef,
			String str_entityId,
			String str_activityCode,
			char str_operation,
			String str_tableCode,
			String str_className)
		{	
			String str_date = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);		
			//--Improvement point 8 *******************************************************************
			//			fscreateDocumentUpdateEntry(str_appCode, 
			//				str_activityCode,
			//				str_tableCode,
			//				str_docRef,
			//				str_userCode,
			//				str_date,
			//				"U1",
			//				null,
			//				"Update" );


			return "U1";
		}

	
		//--Improvement point 8 **************************************************//
		//		private static void  fscreateDocumentUpdateEntry(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_userCode,  String str_date, String str_userLevelCode, String str_processId, String str_remarks)
		//		{
		//			String str_date_ui = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_date);
		//			str_appCode = "'" + str_appCode + "'";
		//
		//			if (str_activityCode == null || str_activityCode.Length == 0)
		//				str_activityCode = "NULL";
		//			else
		//				str_activityCode = "'" + str_activityCode + "'";
		//			
		//			str_tableCode = "'" + str_tableCode + "'";			
		//			//T00005 --- OK 1
		//			str_docRef = "'" + StringUtility.fsreplace(str_docRef,"'","''") + "'";
		//			str_userCode = "'" + str_userCode + "'";
		//			str_date = "'" + str_date + "'";
		//			str_date_ui = "'" + str_date_ui + "'";
		//			str_userLevelCode = "'" + str_userLevelCode + "'";
		//
		//			if (str_processId == null || str_processId.Length == 0)
		//				str_processId = "NULL";
		//			else
		//				str_processId = "'" + str_processId + "'";
		//
		//			if (str_remarks == null || str_remarks.Length == 0)
		//				str_remarks = "NULL";
		//			else
		//				str_remarks = "'" + str_remarks + "'";
		//
		//
		//					
		//			//ASIF OK
		//			String str_query= "INSERT INTO SH_SM_DU_DOCUMENTUPDATES "
		//				+ "(SAA_APPCODE,         SAV_ACTCODE,           SAB_TABLECODE,     SDU_DOCREF,                                     SUS_USERCODE,    SDU_DATETIME, SDU_DATETIMEACTUAL,  SUL_LEVELCODE,     SFC_AUFCPROCESSID, SDU_REMARKS) VALUES "
		//				+ "(" + str_appCode+","+ str_activityCode+ ","+ str_tableCode+ ","+ str_docRef+ ","+str_userCode+", "+str_date_ui+", "+str_date+", "+str_userLevelCode+","+str_processId+","+ str_remarks+")"; 
		//
		//			DB.executeDML(str_query);
		//		}	








		//--Improvement point 8 ************************************************//
		//		private static String fsgetDocumentCreater(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_processId)
		//		{
		//			//T00005 --- OK 1
		//			String str_query= " WHERE SAA_APPCODE='" + str_appCode + "' "
		//				+ " AND SAB_TABLECODE='" + str_tableCode + "' "
		//				+ " AND SDU_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "' "
		//				+ " AND SUL_LEVELCODE='S1' ";
		//
		//		
		//			if (!(str_activityCode == null || str_activityCode.Length == 0))
		//				str_query += " AND SAV_ACTCODE='" + str_activityCode + "'";
		//			
		//			//			if (!(str_processId == null || str_processId.Length == 0))
		//			//				str_query += " AND SFC_AUFCPROCESSID='" + str_processId + "'";
		//
		//
		//			return SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_DU_DOCUMENTUPDATES", "SUS_USERCODE", str_query);
		//
		//		}


		//--Improvement point 8 ************************************************//
		//		private static String fsgetDocumentUpdatedBy(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_processId)
		//		{
		//			//T00005 --- OK 1
		//			String str_query= " WHERE SAA_APPCODE='" + str_appCode + "' "
		//				+ " AND SAB_TABLECODE='" + str_tableCode + "' "
		//				+ " AND SDU_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "' "
		//				+ " AND (SUL_LEVELCODE='S1' OR SUL_LEVELCODE='U1')";
		//
		//
		//			if (!(str_activityCode == null || str_activityCode.Length == 0))
		//				str_query += " AND SAV_ACTCODE='" + str_activityCode + "'";
		//
		//			//			if (!(str_processId == null || str_processId.Length == 0))
		//			//				str_query += " AND SFC_AUFCPROCESSID='" + str_processId + "'";
		//
		//			//ASIF OK
		//			str_query += " ORDER BY SDU_DATETIMEACTUAL DESC";
		//			return SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_DU_DOCUMENTUPDATES", "SUS_USERCODE", str_query);
		//		}

		private static String fsgetDocumentLastRouterOfLevel(String str_appCode, String str_activityCode, String str_tableCode, String str_docRef, String str_processId, String str_userLevel)
		{
			//T00005 --- OK 1
			String str_query= " WHERE SAA_APPCODE='" + str_appCode + "' "
				+ " AND SAB_TABLECODE='" + str_tableCode + "' "
				+ " AND SDL_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "' "
				//--Improvement point 25
				+ " AND SUL_LEVELCODE in ('" + str_userLevel + "','U1') ";
			//+ " AND SUL_LEVELCODE='" + str_userLevel + "' ";

		
			if (!(str_activityCode == null || str_activityCode.Length == 0))
				str_query += " AND SAV_ACTCODE='" + str_activityCode + "'";
			
			if (!(str_processId == null || str_processId.Length == 0))
				str_query += " AND SFC_AUFCPROCESSID='" + str_processId + "'";

			//ASIF OK
			String str_subQuery = "(SELECT MAX(SDL_DATETIMEACTUAL) FROM SH_SM_DL_DOCUMENTLOG " + str_query+") ";
			//ASIF OK
			str_query +=  " AND SDL_DATETIMEACTUAL=" +   str_subQuery;


			return SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_DL_DOCUMENTLOG", "SUS_USERCODE", str_query);

		}


		private static bool fscanApproveSelf(String str_userCode) 
		{
			String str_flag = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_GS_GLOBALSETTING", "SGS_APPROVEOWNTRANS", " WHERE SGS_SRNO=1");
			bool bln_cannotApproveSelf = "N".Equals(str_flag);

			if (!bln_cannotApproveSelf)
			{
				String str_userSelect = " FROM SH_SM_US_USER WHERE SUS_USERCODE='" + str_userCode + "'" ;

				String str_where = " WHERE SAA_APPCODE=(SELECT SAA_APPCODE"+str_userSelect+")"
					+ " AND SUG_GROUPCODE=(SELECT SUG_GROUPCODE"+str_userSelect+")"
					+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE ;
				
				str_flag = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_UG_USERGROUP", "SUG_APPROVEOWNTRANS", str_where);
				bln_cannotApproveSelf = "N".Equals(str_flag);
			}

			if (!bln_cannotApproveSelf)
			{
				/// point No 41 - Start
				/// lines are commit due to point No 41
							
				//str_flag = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_US_USER", "SUS_APPROVEOWNTRANS", " WHERE SUS_USERCODE='" + str_userCode + "' AND " + SHSM_Utility.STATUS_SQL_ACTIVE);
				EnvHelper sessionValues = new EnvHelper();
				str_flag = (String) sessionValues.getAttribute("s_SUS_APPROVEOWNTRANS");
				
				/// point No 41 - End
				/// lines are commit due to point No 41
				
				bln_cannotApproveSelf = "N".Equals(str_flag);
			}


			return !bln_cannotApproveSelf;
		}


	


		private static Object[] fsgetDateAndActivityFromDocLog(String str_appCode, String str_tableCode, String str_docRef)
		{

			Object[] obj_array = new Object[2];
			//T00005 --- OK 1
			String str_inner= " WHERE SAA_APPCODE='" + str_appCode +"' "
				+ " AND SAB_TABLECODE='" + str_tableCode +"' "
				+ " AND SDL_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") +"' ";

			//ASIF OK
			String str_where= str_inner + " AND SDL_DATETIMEACTUAL=(SELECT SDL_DATETIMEACTUAL FROM SH_SM_DL_DOCUMENTLOG "+ str_inner+")";

			obj_array = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_DL_DOCUMENTLOG", new String[] {"SDL_DATETIME","SAV_ACTCODE"}, str_where);

			return obj_array;		
		}



		public static String fsgetMessageFromLevelCode(String str_levelCode)
		{
			String str_message = "";
			
			if (str_levelCode== null)
				return  MSG_TR_INCOMPLETE ;

			if (str_levelCode.StartsWith("V") || str_levelCode.StartsWith("R1") || str_levelCode.StartsWith("A1") ||  str_levelCode.StartsWith("A2"))
			{
				str_levelCode=str_levelCode.Substring(0,2);
				//System.out.println("---------------------->>>>>>>>"+str_levelCode);
				str_message = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_UL_USERLEVEL","SUL_MESSAGE"," WHERE SUL_LEVELCODE ='"+str_levelCode+"'");
			}
			else
			{
				str_message = str_levelCode;
			}
/*
			if (str_levelCode.StartsWith(LEVEL_APPROVED))
				str_message = MSG_TR_APPROVED;
			else if (str_levelCode.StartsWith(LEVEL_REJECTED))
				str_message = MSG_TR_REJECTED;
			else if (str_levelCode.StartsWith(LEVEL_APPROVAL)) 
				str_message = MSG_TR_APPROVAL;
			else if (str_levelCode.StartsWith("V1"))
				str_message = MSG_TR_VERIFICATION;
			else if (str_levelCode.StartsWith("V"))
				str_message = MSG_TR_VERIFIED;
			else
				str_message = str_levelCode;

			int _pos = str_levelCode.IndexOf(':');

			if (_pos > -1)
				str_message += str_levelCode.Substring(_pos);
*/
			return str_message;
		}


		/*---- MC0026 ---------------------START*/
		public static void fschangeRecordOwner(String str_appCode, String str_actCode, String str_tableCode, String str_pkComb, String str_userCode)
		{
			String str_qry="";
			rowset rs_entity = null;
			try
			{
				String qry = "SELECT * FROM SH_SM_AB_APPTABLE WHERE SAA_APPCODE = '"+ str_appCode+"' AND SAB_TABLECODE = '"+ str_tableCode +"'";
				rs_entity= DB.executeQuery(qry);
				System.Console.Out.WriteLine("Query1 .........: "+qry);
				//if(str_appCode != null && str_appCode.Length !=0 ){ }
				//if(str_actCode != null && str_actCode.Length !=0 ){ }
				//if(str_tableCode != null && str_tableCode.Length !=0 )
				str_qry +="UPDATE "+str_tableCode;

				//if(str_userCode != null && str_userCode.Length !=0 )
				//{
				String str_UserField = null;
				if (rs_entity != null && rs_entity.next())
					str_UserField = rs_entity.getString("SAB_USERCODEFIELD");
				str_qry += " SET " + str_UserField +" = '"+ str_userCode+"'";
				//}

				if(str_pkComb == null || str_pkComb.Length ==0 )
					throw new ProcessException("Security Module: \\n" + "Primary key Reference required....");

				//if(str_pkComb != null && str_pkComb.Length !=0 )
				//{
				//if (rs_entity != null && rs_entity.next()){
				String str_pkFields = rs_entity.getString("SAB_PKCOMB");
				String[] arr_pkComb = StringUtility.fssplitString(str_pkFields,'~');
				int int_intIndex;
				String str_pkFieldsConverted = "";
				String str_pkValuesConverted = null;

				String str_DB = "";
				/// point No 41 - Start
				/// lines are commit due to point No 41

				//str_DB = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_GS_GLOBALSETTING","SGS_DATABASE"," WHERE SGS_SRNO=1");
				EnvHelper sessionValues = new EnvHelper();
				str_DB = (String) sessionValues.getAttribute("s_SGS_DATABASE");

				/// point No 41 - End
				/// lines are commit due to point No 41
				
				
				if (str_DB.ToUpper().Equals(SHSM_Utility.DB_SQL))
				{
					for(int_intIndex=0 ;int_intIndex<arr_pkComb.Length;int_intIndex++)
					{
						str_pkFieldsConverted +=  "CONVERT(VARCHAR,"+arr_pkComb[int_intIndex]+")+'~'+";
					}
					str_pkFieldsConverted = str_pkFieldsConverted.Substring(0,str_pkFieldsConverted.Length-5);
				}
				else if (str_DB.ToUpper().Equals(SHSM_Utility.DB_DB2)) 
				{
					for(int_intIndex=0 ;int_intIndex<=arr_pkComb.Length-1;int_intIndex++)
					{
						str_pkFieldsConverted +=  "CHAR("+arr_pkComb[int_intIndex]+")||'~'||";
					}
					str_pkFieldsConverted = str_pkFieldsConverted.Substring(0,str_pkFieldsConverted.Length-7);
				}
				else if (str_DB.ToUpper().Equals(SHSM_Utility.DB_ORACLE))
				{
					for(int_intIndex=0 ;int_intIndex<=arr_pkComb.Length-1;int_intIndex++)
					{
						str_pkFieldsConverted +=  "CHAR("+arr_pkComb[int_intIndex]+")||'~'||";
					}
					str_pkFieldsConverted = str_pkFieldsConverted.Substring(0,str_pkFieldsConverted.Length-7);
				}
				else if (str_DB.ToUpper().Equals(SHSM_Utility.DB_SYBASE))
				{
					for(int_intIndex=0 ;int_intIndex<arr_pkComb.Length;int_intIndex++)
					{
						str_pkFieldsConverted +=  "CONVERT(VARCHAR,"+arr_pkComb[int_intIndex]+")+'~'+";
					}
					str_pkFieldsConverted = str_pkFieldsConverted.Substring(0,str_pkFieldsConverted.Length-5);
				}
				else
					throw new InvalidDataException("Security Module: \\n" + "Argument for database type " + str_DB + " is unknown");


				str_qry += " WHERE "+str_pkFieldsConverted+" = '" +str_pkComb + "'" ;
				//}
				//}
				System.Console.Out.WriteLine("Final Query .........: "+str_qry);
				DB.executeDML(str_qry);
			}
			catch (SQLException e)
			{
				System.Console.Out.WriteLine("Erro in query --> " + str_qry);
				//throw new ProcessException("Security Module: \\n" + "Error Executing query " );
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error Executing query.\n" + e.Message );
			}
			catch (Exception e)
			{
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to change record owner. \n" +e.Message);
			}
			finally
			{
				try
				{
					if (rs_entity != null)	rs_entity.close();
				}
				catch (SQLException e){	}
				rs_entity = null;
			}
		}
		/*---- MC0026 ---------------------END*/



		//AL - 27/10/2005
		//private static void fsupdateBusinessEntityTable(String str_tableCode, String str_routingStatusField, String str_routingStatus, String str_userWhere, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		//		public static void fsupdateBusinessEntityTable(String str_tableCode, String str_routingStatusField, String str_routingStatus, String str_userWhere, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		//		{
		//			if (str_tableCode == null || str_tableCode.Trim().Length==0)
		//				return;
		//			str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_userWhere, str_arrKeyFields, obj_colFields);
		//
		//			//T00009
		//			SHSM_Utility.fsupdateColumn(str_tableCode, str_routingStatusField, str_routingStatus, str_userWhere);
		//
		//		}


		//AL - 27/10/2005
		//private static void fsupdateBusinessEntityTable(String str_tableCode, String str_routingStatusField, String str_routingStatus, String str_userCodeField, String str_userCode, String str_userWhere, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		//		public static void fsupdateBusinessEntityTable(String str_tableCode, String str_routingStatusField, String str_routingStatus, String str_userCodeField, String str_userCode, String str_userWhere, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		//		{
		//			
		//			//Dotnet changes - Asif
		//			if (str_tableCode == null || str_tableCode.Trim().Length==0 || str_userCodeField.Trim().Length==0 || str_routingStatusField.Trim().Length==0)
		//				return;
		//			str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_userWhere, str_arrKeyFields, obj_colFields);
		//
		//			String[] str_arrColumns = new String[] {str_userCodeField, str_routingStatusField};
		//
		//			str_userCode = "'" +str_userCode + "'";
		//
		//			if (str_routingStatus == null || str_routingStatus.Length==0)
		//				str_routingStatus = "NULL";
		//			else
		//				str_routingStatus = "'" + str_routingStatus + "'";
		//
		//			String[] str_arrValues  = new String[] {str_userCode, str_routingStatus};
		//
		//			//T00009
		//			SHSM_Utility.fsupdateColumn(str_tableCode, str_arrColumns, str_arrValues, str_userWhere);
		//
		//		}

		public static void fsupdateBusinessEntityTable(String str_tableCode, String[] str_arrKeyFields, NameValueCollection obj_colFields, char chr_operation)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String[] str_arrColumns = new String[] {"SAB_TRSTATUSFIELD", "SAB_USERCODEFIELD", "SAB_LRPUSERCODEFIELD", "SAB_LASTUSERCODEFIELD", "SAB_DATECREATEDFIELD", "SAB_DATEMODIFIEDFIELD"};
			Object[] str_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_AB_APPTABLE", str_arrColumns, " WHERE SAA_APPCODE='"+ str_appCode + "' AND SAB_TABLECODE='" + str_tableCode +"'" );

			String str_dateNow = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC);
			String str_userWhere ="";

			String str_routingStatusField = null;
			String str_userCodeField      =	null;
			String str_LRPUserCodeField  =	null;
			String str_lastUserCodeField =	null;
			String str_dateCreatedField  =	null;
			String str_dateModifiedField =	null;


			if (str_arrValues!=null)
			{
				//String str_keyCombination=SHSM_TransactionRouter.fsbuildWhereClauseForBusinessEntity(str_arrKeyFields);
				String str_keyCombination=SHSM_TransactionRouter.fsbuildWhereClauseFromDataDictionary( str_tableCode, str_appCode);


				if(str_arrValues[0]!=null)
				{
					str_routingStatusField = str_arrValues[0].ToString();
				}
				if(str_arrValues[1]!=null)
				{
					str_userCodeField	=	str_arrValues[1].ToString();
				}
				if(str_arrValues[2]!=null)
				{
					str_LRPUserCodeField  =	str_arrValues[2].ToString();
				}
				if(str_arrValues[3]!=null)
				{
					str_lastUserCodeField =	str_arrValues[3].ToString();
				}
				if(str_arrValues[4]!=null)
				{
					str_dateCreatedField  =	str_arrValues[4].ToString();
				}
				if(str_arrValues[5]!=null)
				{
					str_dateModifiedField =	str_arrValues[5].ToString();
				}


				Boolean bln_updateTable = false;
				String str_sql = " UPDATE " +  str_tableCode + " SET ";

				if (str_routingStatusField!=null && str_routingStatusField!="")
				{
					str_sql += str_routingStatusField + "='N1', ";
					bln_updateTable = true;
				}
				if (str_userCodeField!=null && str_userCodeField!="")
				{
					if (chr_operation==SHSM_AuditTrail.DML_OPERATION_INSERT)
					{
						str_sql += str_userCodeField + "='" + str_userCode + "', ";
						bln_updateTable = true;
					}
				}
				if (str_LRPUserCodeField!=null && str_LRPUserCodeField!="")
				{
					str_sql += str_LRPUserCodeField + "='" + str_userCode + "', ";
					bln_updateTable = true;
				}
				if (str_lastUserCodeField!=null && str_lastUserCodeField!="")
				{
					str_sql += str_lastUserCodeField + "='" + str_userCode + "', ";
					bln_updateTable = true;
				}

				//Point No 102 - Oracle Support - Start
				ParaColl.clear();
				//Point No 102 - Oracle Support - End

				if (str_dateCreatedField!=null && str_dateCreatedField!="")
				{
					if (chr_operation==SHSM_AuditTrail.DML_OPERATION_INSERT)
					{
						//Point No 102 - Oracle Support - Start
						//str_sql += str_dateCreatedField + "='" + str_dateNow + "', ";
						str_sql += str_dateCreatedField + "=?, ";
						ParaColl.puts("@DateCreated", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_dateNow), Types.TIMESTAMP);
						//Point No 102 - Oracle Support - End
						bln_updateTable = true;
					}
				}

				//Point No 102 - Oracle Support - Start
				//if (str_dateModifiedField!=null && str_dateModifiedField!="")
				//{
				//Point No 102 - Oracle Support - end

					//Point No 102 - Oracle Support - Start
					//str_sql += str_dateModifiedField + "='" + str_dateNow + "', ";
					str_sql += str_dateModifiedField + "=?, ";
					ParaColl.puts("@DateModified", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_dateNow), Types.TIMESTAMP);
					//Point No 102 - Oracle Support - End

					bln_updateTable = true;
				//}


				//SHSM_TransactionRouter.fsupdateBusinessEntityTable(str_tableCode, str_routingStatusField, "N1", str_userCodeField, str_userCode, str_keyCombination, str_arrKeyFields, obj_colFields);
				if (bln_updateTable) 
				{					
					//str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_keyCombination, str_arrKeyFields, obj_colFields);
					str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_keyCombination, str_arrKeyFields, obj_colFields,ref ParaColl);
					str_sql = str_sql.Substring(0,str_sql.Length-2);
					str_sql += str_userWhere;
					try
					{
						//Point No 102 - Oracle Support - Start
						//DB.executeDML(str_sql);
						DB.executeDML(str_sql,ParaColl);
						//Point No 102 - Oracle Support - End
					}
					catch(SQLException e)
					{
						throw new ProcessException("Security Module: \\n" + "Exception in Business table update SQL: " + e.Message);
					}
				}
			}
			else
			{
				throw new ProcessException("Security Module: \\n" + "Table " + str_tableCode + " is not set in Data Dictionary.");
			}
		}

		public static void fsupdateBusinessEntityTable(String str_tableCode, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_level)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String[] str_arrColumns = new String[] {"SAB_TRSTATUSFIELD", "SAB_LRPUSERCODEFIELD", "SAB_LASTROUTINGDATEFIELD"};
			Object[] str_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_AB_APPTABLE", str_arrColumns, " WHERE SAA_APPCODE='"+ str_appCode + "' AND SAB_TABLECODE='" + str_tableCode +"'" );

			String str_dateNow = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC);
			String str_userWhere ="";

			String str_routingStatusField = null;
			String str_LRPUserCodeField  =	null;
			String str_lastRoutingDateField  =	null;

			if (str_arrValues!=null)
			{
				//String str_keyCombination=SHSM_TransactionRouter.fsbuildWhereClauseForBusinessEntity(str_arrKeyFields);
				String str_keyCombination=SHSM_TransactionRouter.fsbuildWhereClauseFromDataDictionary(str_tableCode,str_appCode);

				if(str_arrValues[0]!=null)
				{
					str_routingStatusField = str_arrValues[0].ToString();
				}
				if(str_arrValues[1]!=null)
				{
					str_LRPUserCodeField  =	str_arrValues[1].ToString();
				}
				if(str_arrValues[2]!=null)
				{
					str_lastRoutingDateField  =	str_arrValues[2].ToString();
				}

				Boolean bln_updateTable = false;
				String str_sql = " UPDATE " +  str_tableCode + " SET ";

				if (str_routingStatusField!=null && str_routingStatusField!="")
				{
					str_sql += str_routingStatusField + "='" + str_level + "', ";
					bln_updateTable = true;
				}
				if (str_LRPUserCodeField!=null && str_LRPUserCodeField!="")
				{
					str_sql += str_LRPUserCodeField + "='" + str_userCode + "', ";
					bln_updateTable = true;
				}
				
				ParaColl.clear();

				
				if (str_lastRoutingDateField!=null && str_lastRoutingDateField!="")
				{
					//str_sql += str_lastRoutingDateField + "='" + str_dateNow + "', ";
					str_sql += str_lastRoutingDateField + "= ? ,";
					ParaColl.puts("@DateLastRouting", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_dateNow), Types.TIMESTAMP);
					bln_updateTable = true;
				}


				if (bln_updateTable) 
				{
					//str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_keyCombination, str_arrKeyFields, obj_colFields);
					str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_keyCombination, str_arrKeyFields, obj_colFields,ref ParaColl);
					str_sql = str_sql.Substring(0,str_sql.Length-2);
					str_sql += str_userWhere;
					try
					{
						//DB.executeDML(str_sql);
						DB.executeDML(str_sql,ParaColl);
					}
					catch(SQLException e)
					{
						throw new ProcessException("Security Module: \\n" + "Exception in Business table update SQL: " + e.Message);
					}
				}
			}
			else
			{
				throw new ProcessException("Security Module: \\n" + "Table " + str_tableCode + " is not set in Data Dictionary.");
			}
		}

		//AL - 27/10/2005
		//private static String fsbuildWhereClauseForBusinessEntity(String[] str_arrKeyFields) 
		public static String fsbuildWhereClauseForBusinessEntity(String[] str_arrKeyFields) 
		{
			String str_userTableWhere = " WHERE ";
			for (int i=0 ; i< str_arrKeyFields.Length; i++)
			{
				str_userTableWhere += " " + str_arrKeyFields[i] + "=~" + str_arrKeyFields[i] + "~ AND";
			}
			str_userTableWhere = str_userTableWhere.Substring(0, str_userTableWhere.Length-3);
			return str_userTableWhere;
		}


		private static void fsparseAndExecuteQuery(String str_userTable, String str_userField, String str_userNature, Object obj_userValue, String str_userWhere, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			String str_userValue = null;
			if (str_userTable != null && str_userTable.Length > 0)
			{
				if (str_userNature == null || str_userNature.Equals("C"))
					str_userValue = "'"  + obj_userValue.ToString() + "'";
				else if (str_userValue.Equals("D"))
					str_userValue = "'"  + SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE, (DateTime)obj_userValue) + "'";


				//Point No 102 - Oracle Support - Start
				//String str_queryToExecute = "UPDATE " + str_userTable
				//	+ " SET " + str_userField + "=" + str_userValue + " ";
				String str_queryToExecute = "UPDATE " + str_userTable + " SET " + str_userField + " = ? ";
				ParaColl.clear();
				if (str_userNature == null || str_userNature.Equals("C"))
				{
					ParaColl.puts("@UserData", str_userValue, Types.CHAR);
				}
				else if (str_userValue.Equals("D"))
				{
					ParaColl.puts("@UserData", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_userValue), Types.DATE);
				}
				else//Numberic Value
				{
					ParaColl.puts("@UserData", str_userValue, Types.INTEGER);
				}
				//Point No 102 - Oracle Support - End

				if (str_userWhere != null && str_userWhere.Length>0)
				{
					//str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_userWhere, str_arrKeyFields, obj_colFields);
					str_userWhere = SHSM_Utility.fsgetExceutableQuery(str_userWhere, str_arrKeyFields, obj_colFields,ref ParaColl);
					str_queryToExecute += str_userWhere;
				}

				try
				{
					//Point No 102 - Oracle Support - Start
					//DB.executeDML( str_queryToExecute);
					DB.executeDML( str_queryToExecute,ParaColl);
					//Point No 102 - Oracle Support - End
				}
				catch (Exception e)
				{
					throw new DBException(e, str_queryToExecute, "Security Module: \\n" + "Defined WHERE Clause of routing forward list" );
				}
			}
		}

		/**** MC0033-11 ***************************************START*/
		public static void fsmoveRoutingRecordsInHistory(String str_appCode, String str_tableCode, String str_docRef)
		{
			String str_qry="";
			try
			{
				/****** Inserting data into History tables ****************************/

				//--Improvement point 8 ************************************************//
				//SH_SM_DU_DOCUMENTUPDATES
				//str_qry = " Insert into SH_SM_DP_DOCUPDHISTORY "
				//	+ "       (SAA_APPCODE, SAB_TABLECODE, SDP_DOCREF, SDP_DATETIME, SUL_LEVELCODE, SDP_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SDP_REMARKS)"
				//	+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDU_DOCREF, SDU_DATETIME, SUL_LEVELCODE, SDU_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SDU_REMARKS FROM SH_SM_DU_DOCUMENTUPDATES "
				//	+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDU_DOCREF='"+str_docRef+"'";
				//DB.executeDML(str_qry);

				//SH_SM_DL_DOCUMENTLOG
				str_qry = " Insert into SH_SM_DH_DOCUMENTHISTORY "
					+ "       (POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAB_TABLECODE, SDH_DOCREF, SDH_DATETIME, SUL_LEVELCODE, SDH_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDH_REMARKS, SDH_DOCREFUI, SDH_REASON)"
					+ " SELECT POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAB_TABLECODE, SDL_DOCREF, SDL_DATETIME, SUL_LEVELCODE, SDL_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDL_REMARKS, SDL_DOCREFUI, SDL_REASON FROM SH_SM_DL_DOCUMENTLOG "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DD_DOCUMENTDETAIL
				str_qry = " Insert into SH_SM_DS_DOCDETHISTORY "
					+ "       (SAA_APPCODE, SAB_TABLECODE, SDS_DOCREF, SDS_DATETIME, SUL_LEVELCODE, SDS_SRNO, SDS_APPCODE, SFS_FIELDCODE, SDS_FIELDSRNO, SDS_FIELDVALUE, SDL_DATETIMEACTUAL,SDS_APPLYSEARCH)"
					+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDL_DOCREF, SDL_DATETIME, SUL_LEVELCODE, SDD_SRNO, SDD_APPCODE, SFS_FIELDCODE, SDD_FIELDSRNO, SDD_FIELDVALUE, SDL_DATETIMEACTUAL,SDD_APPLYSEARCH FROM SH_SM_DD_DOCUMENTDETAIL "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_IN_INBOX
				/************* Navigation in Inbox - start **********************/
				str_qry = " Insert into SH_SM_IH_INBOXHISTORY "
					+ "       (SIH_APPCODE,SUG_GROUPCODE,SAA_APPCODE,SAB_TABLECODE,SIH_DOCREF,SIH_DATETIME,SUS_USERCODE,SIH_DATETIMEACTUAL,SIH_USERCODE,SAV_ACTCODE,SIH_SUBJECT,SIH_FORWARDSTATUS,SIH_TYPE,SIH_DOCREFUI,SIH_REMARKS,SPT_PRGTYPECODE,SAO_OPTCODE,SAN_SUBOPTCODE,SAN_SOURCE,SAM_LEVELCODE)"
					+ " SELECT SIN_APPCODE,SUG_GROUPCODE,SAA_APPCODE,SAB_TABLECODE,SIN_DOCREF,SIN_DATETIME,SUS_USERCODE,SIN_DATETIMEACTUAL,SIN_USERCODE,SAV_ACTCODE,SIN_SUBJECT,SIN_FORWARDSTATUS,SIN_TYPE,SIN_DOCREFUI,SIN_REMARKS,SPT_PRGTYPECODE,SAO_OPTCODE,SAN_SUBOPTCODE,SAN_SOURCE,SAM_LEVELCODE FROM SH_SM_IN_INBOX "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIN_DOCREF='"+str_docRef+"'";
				/************* Navigation in Inbox - end **********************/
				DB.executeDML(str_qry);

				/****** Removing data from Log tables ****************************/
				//--Improvement point 8 - start ************************************************//
				//SH_SM_DU_DOCUMENTUPDATES
				//str_qry = " Delete from SH_SM_DU_DOCUMENTUPDATES WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDU_DOCREF='"+str_docRef+"'";
				//DB.executeDML(str_qry);

				//SH_SM_DD_DOCUMENTDETAIL
				str_qry = " Delete from SH_SM_DD_DOCUMENTDETAIL WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DL_DOCUMENTLOG
				str_qry = " Delete from SH_SM_DL_DOCUMENTLOG WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_IN_INBOX
				str_qry = " Delete from SH_SM_IN_INBOX WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIN_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);
			}
			catch (SQLException e)
			{
				System.Console.Out.WriteLine("Error in query --> " + str_qry);
				throw new ProcessException("Security Module: \\n" + "Error executing query to move data into history tables");
			}
			catch (Exception e)
			{
				//throw new ProcessException("Security Module: \\n" + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to move records in History." + e.Message);
			}
		}
		/**** MC0033-11 ***************************************END*/

		/**** MC0033-11 ***************************************START*/
		public static void fsmoveApprovedRecordsInHistory(String str_tableCode, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{
			
			EnvHelper sessionValues = new EnvHelper();
			String str_appCode	= (String)sessionValues.getAttribute("s_SAA_APPCODE");
			String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);
			
			
			
			String str_qry="";
			try
			{

				/****** ASIF Inserting data into History tables ****************************/
				//SH_SM_DL_DOCUMENTLOG
				str_qry = " Insert into SH_SM_LM_DOCTLOGHIST "
					+ "       (SAA_APPCODE, SAB_TABLECODE, SLM_DOCREF, SLM_DATETIME, SUL_LEVELCODE, SLM_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SLM_REMARKS, SLM_DOCREFUI, POR_ORGACODE, PLC_LOCACODE, SLM_REASON) "
					+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDL_DOCREF, SDL_DATETIME, SUL_LEVELCODE, SDL_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDL_REMARKS, SDL_DOCREFUI, POR_ORGACODE, PLC_LOCACODE, SDL_REASON FROM SH_SM_DL_DOCUMENTLOG "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DD_DOCUMENTDETAIL
				str_qry = " Insert into SH_SM_DM_DOCDETHIST "
					+ "       (SAA_APPCODE, SAB_TABLECODE, SDM_DOCREF, SDM_DATETIME, SUL_LEVELCODE, SDM_SRNO, SDM_APPCODE, SFS_FIELDCODE, SDM_FIELDSRNO, SDM_FIELDVALUE, SDL_DATETIMEACTUAL, SDM_APPLYSEARCH) "
					+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDL_DOCREF, SDL_DATETIME, SUL_LEVELCODE, SDD_SRNO, SDD_APPCODE, SFS_FIELDCODE, SDD_FIELDSRNO, SDD_FIELDVALUE, SDL_DATETIMEACTUAL,SDD_APPLYSEARCH FROM SH_SM_DD_DOCUMENTDETAIL "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);				

				//SH_SM_IN_INBOX
				str_qry = " Insert into SH_SM_NM_INBOXHIST "
					+ "       (SNM_APPCODE, SUG_GROUPCODE, SAA_APPCODE, SAB_TABLECODE, SNM_DOCREF, SNM_DATETIME, SUS_USERCODE, SNM_DATETIMEACTUAL, SNM_USERCODE, SAV_ACTCODE, SNM_SUBJECT, SNM_FORWARDSTATUS, SNM_TYPE, SNM_DOCREFUI, SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE, SAM_LEVELCODE, SNM_REMARKS) "
					+ " SELECT SIN_APPCODE, SUG_GROUPCODE, SAA_APPCODE, SAB_TABLECODE, SIN_DOCREF, SIN_DATETIME, SUS_USERCODE, SIN_DATETIMEACTUAL, SIN_USERCODE, SAV_ACTCODE, SIN_SUBJECT, SIN_FORWARDSTATUS, SIN_TYPE, SIN_DOCREFUI, SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE, SAM_LEVELCODE, SIN_REMARKS FROM SH_SM_IN_INBOX "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIN_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				////////////////////////////// DELETING RECORDS
				//SH_SM_DD_DOCUMENTDETAIL
				str_qry = " Delete from SH_SM_DD_DOCUMENTDETAIL WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DL_DOCUMENTLOG
				str_qry = " Delete from SH_SM_DL_DOCUMENTLOG WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDL_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DM_DOCDETHIST
				str_qry = " Delete from SH_SM_IN_INBOX WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIN_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				
				/****** ASIF Inserting data into History tables ****************************/

				/****** Inserting data into History tables ****************************/
				//SH_SM_DP_DOCUPDHISTORY
				//str_qry = " Insert into SH_SM_DT_DOCUPDHIST "
				//	+ "       (SAA_APPCODE, SAB_TABLECODE, SDT_DOCREF, SDT_DATETIME, SUL_LEVELCODE, SDT_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SDT_REMARKS)"
				//	+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDP_DOCREF, SDP_DATETIME, SUL_LEVELCODE, SDP_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SDP_REMARKS FROM SH_SM_DP_DOCUPDHISTORY "
				//	+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDP_DOCREF='"+str_docRef+"'";
				//DB.executeDML(str_qry);

				//SH_SM_DH_DOCUMENTHISTORY
				str_qry = " Insert into SH_SM_LM_DOCTLOGHIST "
					+ "       (POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAB_TABLECODE, SLM_DOCREF, SLM_DATETIME, SUL_LEVELCODE, SLM_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SLM_REMARKS, SLM_DOCREFUI, SLM_REASON)"
					+ " SELECT POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAB_TABLECODE, SDH_DOCREF, SDH_DATETIME, SUL_LEVELCODE, SDH_DATETIMEACTUAL, SAV_ACTCODE, SUS_USERCODE, SFC_AUFCPROCESSID, SVC_SRNO, SDH_REMARKS, SDH_DOCREFUI, SDH_REASON FROM SH_SM_DH_DOCUMENTHISTORY "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDH_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DS_DOCDETHISTORY
				str_qry = " Insert into SH_SM_DM_DOCDETHIST "
					+ "       (SAA_APPCODE, SAB_TABLECODE, SDM_DOCREF, SDM_DATETIME, SUL_LEVELCODE, SDM_SRNO, SDM_APPCODE, SFS_FIELDCODE, SDM_FIELDSRNO, SDM_FIELDVALUE, SDL_DATETIMEACTUAL)"
					+ " SELECT SAA_APPCODE, SAB_TABLECODE, SDS_DOCREF, SDS_DATETIME, SUL_LEVELCODE, SDS_SRNO, SDS_APPCODE, SFS_FIELDCODE, SDS_FIELDSRNO, SDS_FIELDVALUE, SDL_DATETIMEACTUAL FROM SH_SM_DS_DOCDETHISTORY "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDS_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_IH_INBOXHISTORY
				/************* Navigation in Inbox - start **********************/
				str_qry = " Insert into SH_SM_NM_INBOXHIST "
					+ "       (SNM_APPCODE,SUG_GROUPCODE,SAA_APPCODE,SAB_TABLECODE,SNM_DOCREF,SNM_DATETIME,SUS_USERCODE,SNM_DATETIMEACTUAL,SNM_USERCODE,SAV_ACTCODE,SNM_SUBJECT,SNM_FORWARDSTATUS,SNM_TYPE,SNM_DOCREFUI,SNM_REMARKS,SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE,SAM_LEVELCODE)"
					+ " SELECT SIH_APPCODE,SUG_GROUPCODE,SAA_APPCODE,SAB_TABLECODE,SIH_DOCREF,SIH_DATETIME,SUS_USERCODE,SIH_DATETIMEACTUAL,SIH_USERCODE,SAV_ACTCODE,SIH_SUBJECT,SIH_FORWARDSTATUS,SIH_TYPE,SIH_DOCREFUI,SIH_REMARKS,SPT_PRGTYPECODE, SAO_OPTCODE, SAN_SUBOPTCODE, SAN_SOURCE,SAM_LEVELCODE FROM SH_SM_IH_INBOXHISTORY "
					+ " WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIH_DOCREF='"+str_docRef+"'";
				/************* Navigation in Inbox - end **********************/
				DB.executeDML(str_qry);

				/****** Removing data from Log tables ****************************/
				//SH_SM_DP_DOCUPDHISTORY
				//str_qry = " Delete from SH_SM_DP_DOCUPDHISTORY WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDP_DOCREF='"+str_docRef+"'";
				//DB.executeDML(str_qry);

				//SH_SM_DS_DOCDETHISTORY
				str_qry = " Delete from SH_SM_DS_DOCDETHISTORY WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDS_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_DH_DOCUMENTHISTORY
				str_qry = " Delete from SH_SM_DH_DOCUMENTHISTORY WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SDH_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);

				//SH_SM_IH_INBOXHISTORY
				str_qry = " Delete from SH_SM_IH_INBOXHISTORY WHERE SAA_APPCODE='"+str_appCode+"' AND SAB_TABLECODE='"+str_tableCode+ "' AND SIH_DOCREF='"+str_docRef+"'";
				DB.executeDML(str_qry);
			}
			catch (SQLException e)
			{
				System.Console.Out.WriteLine("Error in query --> " + str_qry);
				throw new ProcessException("Security Module: \\n" + "Error executing query to move approved data into history tables");
			}
			catch (Exception e)
			{
				throw new ProcessException("Security Module: \\n" + e.Message);
			}
		}
		/**** MC0033-11 ***************************************END*/

		/**** MC0033-2 ***************************************START*/
		public static void fssaveRecordOwner(String str_appCode, String str_tableCode, String str_docRef)
		{
			String str_qry="";
			String str_SQL="";
			try
			{
				/****** Making Update table Query for Record Owner ****************************/
				//            str_SQL = " UPDATE " + str_table " SET " + str_userCodeField "='" + str_userCode + ", " + str_trStatusField + "='" + str_trStatus + "' "
				//                    + " WHERE "
				DB.executeDML(str_SQL);
			}
			catch (SQLException e)
			{
				System.Console.Out.WriteLine("Error in query --> " + str_qry);
				throw new ProcessException("Security Module: \\n" + "Error Executing query ");
			}
			catch (Exception e)
			{
				throw new ProcessException("Security Module: \\n" + e.Message);
			}
		}
		/**** MC0033-2 ***************************************END*/

		//MC0033 Start 28/10/2005
		public static void fscreateDocumentLogDetail(String str_entityID, String[] str_arrKeyFields, NameValueCollection obj_colFields, String str_appCode, String str_tableCode, String str_docRef, String str_date_ui, String str_date_sec,  String str_userLevelCode)
		{
			if (str_entityID != null || str_entityID.Length > 0) 
			{ 
	
				String str_sql = null;
				//String str_date_ui = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_date);
				//String str_date_sec = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC, str_date);
	
				// step # 1
				//get the total no of records
	
				rowset obj_rs = null;
				String str_docAppCode = null;
				String str_docFieldCode = null;
				String applySearch = null;
				int int_srno;
				str_sql = "SELECT SAA_APPCODE, SFS_FIELDCODE, SDF_SRNO,SDF_APPLYSEARCH FROM SH_SM_DF_DOCUMENTFIELD WHERE PSE_ENTITYID = '"+str_entityID+"' AND SST_STATUSCODE = 'Y'";
				try
				{
					obj_rs = DB.executeQuery(str_sql);

					while (obj_rs.next())
					{
						str_docAppCode 		= obj_rs.getString("SAA_APPCODE");
						str_docFieldCode	= obj_rs.getString("SFS_FIELDCODE");
						int_srno			= obj_rs.getInt("SDF_SRNO");
						applySearch			= obj_rs.getString("SDF_APPLYSEARCH");
						FormulaField obj_formulaField = new FormulaField(str_docAppCode, str_docFieldCode);
						Object obj_value = obj_formulaField.fsevaluateValueOfField(str_arrKeyFields, obj_colFields);

						///Point No. 80 <<<<< sdd_fieldvalue
						str_sql = " INSERT INTO SH_SM_DD_DOCUMENTDETAIL " 
							
							+ " (SAA_APPCODE, SAB_TABLECODE, SDL_DOCREF,  SDL_DATETIME, SUL_LEVELCODE, SDD_SRNO, SDD_APPCODE, SFS_FIELDCODE, SDD_FIELDSRNO, SDD_FIELDVALUE, SDL_DATETIMEACTUAL,SDD_APPLYSEARCH ) "
							+ " VALUES ("
							+ "'" + str_appCode + "', "
							+ "'" + str_tableCode + "', " 
							+ "'" + StringUtility.fsreplace(str_docRef,"'","''") + "', " 
							+ "'" + str_date_ui + "', " 
							+ "'" + str_userLevelCode + "', "
							+ int_srno +", "
							+ "'" + str_docAppCode + "', "
							+ "'" + str_docFieldCode + "', "
							+ " 1, "

							///Point No. 80
							//+ "'" + obj_value.ToString() + "', "
							+ (obj_value.ToString()==null ? "NULL" : "'"+obj_value.ToString()+"'") + ", "

							//Point No 102 - Oracle Support - Start
							//+ "'" + str_date_sec + "')";
							+ "? ,"
							+ "'" + applySearch + "')";

							ParaColl.clear();
							ParaColl.puts("@SDL_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_date_sec), Types.TIMESTAMP);
							//Point No 102 - Oracle Support - End

						try
						{
							//Point No 102 - Oracle Support - Start
							//DB.executeDML(str_sql);
							DB.executeDML(str_sql,ParaColl);
							//Point No 102 - Oracle Support - Start
						}
						catch (SQLException e)
						{
							throw new ProcessException("Security Module: \\n" + "SQL Exception for document detail creation" + e.Message);
						}

					}//end of while
				}
				catch (SQLException e)
				{
					throw new ProcessException("Security Module: \\n" + "SQL Exception for getting document detail" + e.Message);
				}
				finally
				{
					try
					{
						if (obj_rs != null)
							obj_rs.close();
					}
					catch (OleDbException e){	}
					obj_rs = null;
				}
			}
		}
//		//MC0033 end 28/10/2005
//		public static void fsperformRoutingUpdate(String str_appCode, String str_processId, int int_valueSerial, String str_userLevel, String[] str_arrKeyFields, NameValueCollection obj_colFields)
//		{
//
//			rowset rs_userQuery = null;
//
//			String str_userQuery = "SELECT * FROM SH_SM_RU_ROUTINGUPDATE " 
//				+ " WHERE SAA_APPCODE='" + str_appCode + "'"
//				+ " AND SFC_AUFCPROCESSID='" + str_processId + "'" 
//				+ " AND SVC_SRNO=" + int_valueSerial
//				+ " AND SUL_LEVELCODE = '" + str_userLevel + "'"
//				+ " AND SST_STATUSCODE = 'Y'";
//			try
//			{
//				rs_userQuery = DB.executeQuery(str_userQuery);
//			}
//			catch (SQLException e)
//			{
//				throw new ProcessException("Security Module: \\n" + "Error Executing query - Routing Update \nQurey: "  + str_userQuery + "\nError: " + e.Message);
//			}
//			catch (Exception e)
//			{
//				throw new ProcessException("Security Module: \\n" + e.Message);
//			}
//
//			if (rs_userQuery!=null)
//			{
//				String str_Table = null;
//				String str_Field = null;
//				String str_Value = null;
//				String str_Where = null;
//				String str_Nature = null;
//				String str_queryToExecute = null;
//				while(rs_userQuery.next())
//				{
//					str_Table = rs_userQuery.getString("SAB_TABLECODE");
//					str_Field = rs_userQuery.getString("SAC_COLCODE");
//					str_Value = rs_userQuery.getString("SRU_VALUE");
//					str_Where = rs_userQuery.getString("SRU_FETCHWHERE");
//					str_Nature = rs_userQuery.getString("SRU_FIELDNATURE");
//
//					bool bln_constantValue=true;
//
//					if (str_Value.IndexOf("~")>=0 || str_Value.ToUpper().IndexOf("SV(")>=0 || str_Value.ToUpper().IndexOf("SVN(")>=0 )
//					{
//						bln_constantValue = false;
//					}
//
//					if(bln_constantValue == false)
//					{
//						str_Value = SHSM_Utility.fsgetExceutableQuery(str_Value, str_arrKeyFields, obj_colFields);
//					}
//					else
//					{
//						if (str_Nature.Equals("C"))
//						{
//							str_Value = "'"  + str_Value + "'";
//						}
//						else if (str_Nature.Equals("D"))
//						{
//							str_Value = "'"  + SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE, DateTime.Parse(str_Value)) + "'";
//						}
//					}
//
//					str_queryToExecute = "UPDATE " + str_Table + " SET " + str_Field + "=" + str_Value + " ";
//					if (str_Where != null && str_Where.Length>0) 
//					{
//						str_Where = SHSM_Utility.fsgetExceutableQuery(str_Where, str_arrKeyFields, obj_colFields);
//						str_queryToExecute += str_Where;
//					}
//															
//					try
//					{
//						DB.executeDML(str_queryToExecute);
//					}
//					catch (Exception e)
//					{
//						throw new ProcessException("Security Module: \\n" + "Defined WHERE Clause of Routing Update is incorrect\\n MODULE:"+str_appCode+", PROCESS:"+str_processId+", VALUECOMBINATION SERIAL NO:"+int_valueSerial+", FORWARD LIST LEVEL:"+str_userLevel+"\\n query: " + str_queryToExecute + "\\nMessage: " + e.Message );
//					}
//				}//End of while
//			}
//			try
//			{
//				if (rs_userQuery != null)
//					rs_userQuery.close();
//		
//			}
//			catch (OleDbException e){	}
//
//		}//End of function


		//MC0033 end 28/10/2005
		public static void fsperformRoutingUpdate(String str_appCode, String str_processId, int int_valueSerial, String str_userLevel, String[] str_arrKeyFields, NameValueCollection obj_colFields)
		{

			rowset rs_userQuery = null;

			String str_userQuery = "SELECT * FROM SH_SM_RU_ROUTINGUPDATE " 
				+ " WHERE SAA_APPCODE='" + str_appCode + "'"
				+ " AND SFC_AUFCPROCESSID='" + str_processId + "'" 
				+ " AND SVC_SRNO=" + int_valueSerial
				+ " AND SUL_LEVELCODE = '" + str_userLevel + "'"
				+ " AND SST_STATUSCODE = 'Y'";
			try
			{
				rs_userQuery = DB.executeQuery(str_userQuery);
			}
			catch (SQLException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error Executing query - Routing Update \nQurey: "  + str_userQuery + "\nError: " + e.Message);
			}
			catch (Exception e)
			{
				throw new ProcessException("Security Module: \\n" + e.Message);
			}

			if (rs_userQuery!=null)
			{
				String str_Table = null;
				String str_Field = null;
				String str_Value = null;
				String str_Where = null;
				String str_Nature = null;
				String str_queryToExecute = null;
				ParameterCollection pCol = new ParameterCollection();
				while(rs_userQuery.next())
				{

					str_Table = rs_userQuery.getString("SAB_TABLECODE");
					str_Field = rs_userQuery.getString("SAC_COLCODE");
					str_Value = rs_userQuery.getString("SRU_VALUE");
					str_Where = rs_userQuery.getString("SRU_FETCHWHERE");
					str_Nature = rs_userQuery.getString("SRU_FIELDNATURE");

					bool bln_constantValue=true;

					if (str_Value.IndexOf("~")>=0 || str_Value.ToUpper().IndexOf("SV(")>=0 || str_Value.ToUpper().IndexOf("SVN(")>=0 )
					{
						bln_constantValue = false;
					}

					if(bln_constantValue == false)
					{
						//str_Value = SHSM_Utility.fsgetExceutableQuery(str_Value, str_arrKeyFields, obj_colFields);
						str_Value = SHSM_Utility.fsgetExceutableQuery(str_Value,str_arrKeyFields, obj_colFields,ref pCol );
					}
					else
					{
						if (str_Nature.Equals("C"))
						{
							str_Value = "'"  + str_Value + "'";
						}
						else if (str_Nature.Equals("D"))
						{
							str_Value = "'"  + SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_DATE, DateTime.Parse(str_Value)) + "'";
						}
					}
					str_queryToExecute = "UPDATE " + str_Table + " SET " + str_Field + "=" + str_Value + " ";
					if (str_Where != null && str_Where.Length>0) 
					{
						//str_Where = SHSM_Utility.fsgetExceutableQuery(str_Where, str_arrKeyFields, obj_colFields);
						str_Where = SHSM_Utility.fsgetExceutableQuery(str_Where, str_arrKeyFields, obj_colFields,ref pCol);
						str_queryToExecute += str_Where;
					}
															
					try
					{
						//DB.executeDML(str_queryToExecute);
						DB.executeDML(str_queryToExecute,pCol);
					}
					catch (Exception e)
					{
						throw new ProcessException("Security Module: \\n" + "Defined WHERE Clause of Routing Update is incorrect\\n MODULE:"+str_appCode+", PROCESS:"+str_processId+", VALUECOMBINATION SERIAL NO:"+int_valueSerial+", FORWARD LIST LEVEL:"+str_userLevel+"\\n query: " + str_queryToExecute + "\\nMessage: " + e.Message );
					}
				}//End of while
			}

			try
			{
				if (rs_userQuery != null)
					rs_userQuery.close();
		
			}
			catch (OleDbException e){	}

		}//End of function


		//--Improvement point - 20
		//public static void  fscreateDocumentLogEntry(String str_entityID, String str_tableCode, NameValueCollection obj_colFields, String[] str_arrKeyFields)
		public static void fscreateDocumentLogEntry(String str_entityID, String str_tableCode, NameValueCollection obj_colFields, String[] str_arrKeyFields, char chr_operation)
		{
			EnvHelper sessionValues = new EnvHelper();
			System.String str_orgaCode = (System.String) sessionValues.getAttribute("s_POR_ORGACODE");
			System.String str_locaCode = (System.String) sessionValues.getAttribute("s_PLC_LOCACODE");
			System.String str_userCode =  (System.String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_appCode			= (String)sessionValues.getAttribute("s_SAA_APPCODE");
			String str_programTypeCode	= (String)sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode		= (String)sessionValues.getAttribute("s_SAO_OPTCODE");

			String str_query = "WHERE SAA_APPCODE ='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND PSE_ENTITYID='"+str_entityID+"'";
			String str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_query);

//			if (str_activityCode ==null)
//				throw new ProcessException("Security Module: \\n" + "Error in getting activity code for Document Log Entry");
			//--Improvement point 27
			if (SHSM_Utility.fsgetRecordCount("SH_SM_TR_TRANSROUTEENTITY","WHERE PSE_ENTITYID = '" + str_entityID + "'" )> 0)
			{
				if (str_activityCode ==null)
					throw new ProcessException("Security Module: \\n" + "Error in getting activity code for Document Log Entry");

				String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);
				String str_docRefUI = SHSM_Utility.fsgetDocumentReferenceUI(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);

				String str_date_ui = DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP);
				String str_date_sec = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC, str_date_ui);
						
				String str_sql = null;
				String str_remarks = null;
				str_remarks = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_UL_USERLEVEL","SUL_DESC", "WHERE SUL_LEVELCODE='N1'");
				
				//--Improvement point 26 - start
				//Check for U1 and N1 duplication
				String str_levelCode="N1";
				if (chr_operation == SHSM_AuditTrail.DML_OPERATION_UPDATE) 
				{
					str_levelCode="U1";
				}
				str_sql = "WHERE SAA_APPCODE = '" + str_appCode + "' AND SAB_TABLECODE = '" + str_tableCode + "' AND SDL_DOCREF = '" + StringUtility.fsreplace(str_docRef,"'","''") + "' AND SUL_LEVELCODE in ('N1','U1')";
				int int_recordCount = SHSM_Utility.fsgetRecordCount("SH_SM_DL_DOCUMENTLOG",str_sql);
				if (int_recordCount<1)
				{
					str_sql = null;
					//--Improvement point 26 - End
				
					//--Improvement point - start - 20
					//str_levelCode="N1";
					if (chr_operation == SHSM_AuditTrail.DML_OPERATION_UPDATE)
					{
						//str_levelCode="U1";
						fsRemoveDocumentLog(str_appCode, str_tableCode, str_docRef, str_levelCode );
					}
					//--Improvement point - end - 20

					///Point No. 80 <<<<< SDL_REMARKS
					str_sql = " INSERT INTO SH_SM_DL_DOCUMENTLOG " 
						+ " (POR_ORGACODE, PLC_LOCACODE, SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SDL_DOCREF, SUS_USERCODE, SDL_DATETIME, SDL_DATETIMEACTUAL, SUL_LEVELCODE, SDL_REMARKS,SDL_DOCREFUI) "
						+ " VALUES ("
						+ "'" + str_orgaCode + "', "
						+ "'" + str_locaCode + "', "
						+ "'" + str_appCode + "', "
						+ "'" + str_activityCode + "', " 
						+ "'" + str_tableCode + "', " 
						+ "'" + StringUtility.fsreplace(str_docRef,"'","''") + "', " 
						+ "'" + str_userCode + "', " 
						+ "'" + str_date_ui + "', " 

						//Point No 102 - Oracle Support - Start
						//+ "'" + str_date_sec + "', "
						+ "?, "
						//Point No 102 - Oracle Support - End


						//--Improvement point - 20
						+ "'" + str_levelCode + "'"; //+ "'N1'";
					
					///Point No. 80 <<<<< SDL_REMARKS
					//str_sql = str_sql + ", '" + str_remarks + "','" + StringUtility.fsreplace(str_docRefUI,"'","''") + "')";	
					str_sql = str_sql + ", " + (str_remarks==null ? "NULL" : "'"+str_remarks+"'") + ",'" + StringUtility.fsreplace(str_docRefUI,"'","''") + "')";
					
					//Point No 102 - Oracle Support - Start
					ParaColl.clear();
					ParaColl.puts("@SDL_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_date_sec), Types.TIMESTAMP);
					//Point No 102 - Oracle Support - end

					try
					{
						//Point No 102 - Oracle Support - Start
						//DB.executeDML(str_sql);
						DB.executeDML(str_sql,ParaColl);
						//Point No 102 - Oracle Support - End

						//--Improvement point - 20
						//fscreateDocumentLogDetail(str_entityID, str_arrKeyFields, obj_colFields, str_appCode, str_tableCode, str_docRef, str_date_ui, str_date_sec, "N1");
						fscreateDocumentLogDetail(str_entityID, str_arrKeyFields, obj_colFields, str_appCode, str_tableCode, str_docRef, str_date_ui, str_date_sec, str_levelCode);
					}
					catch (OleDbException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
						throw new ProcessException("Security Module: \\n" + "Error Executing query.\nQurey: "  + str_sql + "\nError: " + e.Message);
					}
					catch (Exception e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
						throw new ProcessException("Security Module: \\n" + e.Message);
					}
				}// END OF _DL_ if
			}// END OF _TR_ if
		}

		public static String fsbuildWhereClauseFromDataDictionary(String str_tableCode, String str_appCode) 
		{
			String str_keyCombination=null;
			try
			{
				str_keyCombination = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AB_APPTABLE","SAB_PKCOMB", " WHERE SAB_TABLECODE='"+str_tableCode+"' AND SAA_APPCODE='" + str_appCode +"'");
				String[] str_arrKeyFields = StringUtility.fssplitString(str_keyCombination, '~');
				return fsbuildWhereClauseForBusinessEntity(str_arrKeyFields);
			}
			catch(Exception e)
			{
				throw new ProcessException("Security Module: \\n" + "Business table is not properly defined or not defined in data dictionary" + e.Message);
			}
		}

		//--Improvement point - start - 20
		private static void fsRemoveDocumentLog(String str_appCode, String str_tableCode, String str_docRef, String str_levelCode)
		{
			String str_where=" WHERE SAA_APPCODE='" + str_appCode + "' AND SAB_TABLECODE='" + str_tableCode + "' AND SDL_DOCREF='" + str_docRef + "' AND SUL_LEVELCODE='" + str_levelCode + "'";
			String str_sqlDD = " Delete from SH_SM_DD_DOCUMENTDETAIL " + str_where;
			String str_sqlDL = " Delete from SH_SM_DL_DOCUMENTLOG    " + str_where;
			try
			{
				DB.executeDML(str_sqlDD);
				DB.executeDML(str_sqlDL);
			}
			catch (OleDbException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException("Security Module: \\n" + "Error Executing query for removing Document log. \nError: " + e.Message);
			}
			catch (Exception e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to remove document log. \n" + e.Message);
			}
		}
		//2006/03/17 - Setting Inbox PKs in session - start
		private static void fssetRoutingRemarksInput(String str_appCode, String str_tableCode, String str_docRef, String str_date, String str_entityId, String str_level)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_date_ui = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_date);
			
			Object[] obj_arrValues = null;
			String str_oldRouting ="N";
			string str_routingWhere="";

			//Default setting (for new routing)
			sessionValues.setAttribute("GET_ROUTING_REMARKS","0");
			

			//Checking Routing style
			String[] obj_arrFields={"STR_OLDROUTING"};
			obj_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_TR_TRANSROUTEENTITY", obj_arrFields, " WHERE PSE_ENTITYID='" + str_entityId + "'");
			if(obj_arrValues[0]!=null)
			{
				str_oldRouting = obj_arrValues[0].ToString() ;
			}

			if (str_oldRouting =="Y")
			{
				if (str_level=="V" || str_level=="R")
				{
					sessionValues.setAttribute("GET_ROUTING_REMARKS","1");
					
					//Building Where for table 'SH_SM_IN_INBOX'
					str_routingWhere = " Where SAA_APPCODE = '" + str_appCode + "' AND SAB_TABLECODE = '" + str_tableCode + "' AND SIN_DOCREF = '" + str_docRef + "' AND SIN_DATETIME = '" + str_date_ui + "'";
					sessionValues.setAttribute("ROUTING_WHERE_INBOX",str_routingWhere);
					
					//Building Where for table 'SH_SM_DL_DOCUMENTLOG'
					str_routingWhere = " Where SAA_APPCODE = '" + str_appCode + "' AND SAB_TABLECODE = '" + str_tableCode + "' AND SDL_DOCREF = '" + str_docRef + "' AND SDL_DATETIME = '" + str_date_ui + "'";
					sessionValues.setAttribute("ROUTING_WHERE_DOCLOG",str_routingWhere);				
				}
				else if (str_level=="A")//On approval set where for history tables
				{
					sessionValues.setAttribute("GET_ROUTING_REMARKS","2");
					
					//Building Where for table 'SH_SM_IN_INBOX'
					str_routingWhere = " Where SAA_APPCODE = '" + str_appCode + "' AND SAB_TABLECODE = '" + str_tableCode + "' AND SIH_DOCREF = '" + str_docRef + "' AND SIH_DATETIME = '" + str_date_ui + "'";
					sessionValues.setAttribute("ROUTING_WHERE_INBOX",str_routingWhere);
					
					//Building Where for table 'SH_SM_DL_DOCUMENTLOG'
					str_routingWhere = " Where SAA_APPCODE = '" + str_appCode + "' AND SAB_TABLECODE = '" + str_tableCode + "' AND SDH_DOCREF = '" + str_docRef + "' AND SDH_DATETIME = '" + str_date_ui + "'";
					sessionValues.setAttribute("ROUTING_WHERE_DOCLOG",str_routingWhere);				
				}
			}
		}
		//2006/03/17 - Setting Inbox PKs in session - end

		//--Improvement point - end - 20
	}
}