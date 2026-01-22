using System;
using DB = SHMA.Enterprise.Data.DB;
using rowset = SHMA.Enterprise.Data.rowset;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;
using SHMA.Enterprise.Shared;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using shsm.util;
using OleDbException=System.Data.OleDb.OleDbException;
using StringBuilder=System.Text.StringBuilder;
using SQLException = System.Data.SqlClient.SqlException;
using SHSM_SecurityFeatures=shsm.security.SHSM_SecurityFeatures;
using SHMA.Enterprise.Data;

namespace shsm
{
	public class SHSM_AuditTrail
	{
		public const char DML_OPERATION_DELETE = 'D';
		public const char DML_OPERATION_INSERT = 'I';
		public const char DML_OPERATION_UPDATE = 'U';
		
		/* ------ direct changes in dot net -------- */
		public const char DML_OPERATION_VERIFY = 'V';
		public const char DML_OPERATION_APPROVE = 'A';
		public const char DML_OPERATION_REJECT = 'R';
		/* ---------------------------------------- */

		//Point No 102 - Oracle Support
		private static  ParameterCollection ParaColl = new ParameterCollection();

		public SHSM_AuditTrail()
		{
		}	
		

		//public virtual bool fssaveAuditLog(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, char chr_operation, String str_tableCode)
		public virtual bool fssaveAuditLog(String str_entityId, String[] str_arrKeyFields_temp, NameValueCollection obj_colFields, char chr_operation, String str_tableCode)
		{
			// new code 31/12/2005 - start
			EnvHelper sessionValues = new EnvHelper();

			//Direct option feature ----------------
			String str_directOption = (String) sessionValues.getAttribute("DIRECTOPTION");
				
			//Direct option feature ----------------
			if (str_directOption != null )
			{
				return true;
			}

			if (SHSM_SecurityFeatures.AUDIT_TRAIL==false)
			{
				return true;
			}

			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			//Point No 83 ------------- start <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			String str_programTypeCode	= (String)sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode		= (String)sessionValues.getAttribute("s_SAO_OPTCODE");
			String str_suboptionCode	= (String)sessionValues.getAttribute("s_SAN_SUBOPTCODE");
			String str_userCode			= (String)sessionValues.getAttribute("s_SUS_USERCODE");
			String str_networkID		= (String)sessionValues.getAttribute("s_SAD_NETWORKID");
			String str_ipAddress 		= (String) sessionValues.getAttribute("s_SUS_IPADDRESS");       
        
			String str_activityCode = null;
        
			if (str_entityId == null || str_entityId.Trim().Length== 0)
			{
				String str_where =  " WHERE SAA_APPCODE='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND SAN_SUBOPTCODE='"+str_suboptionCode+"' AND "+SHSM_Utility.STATUS_SQL_ACTIVE;
				str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_where);
			}
			else
			{
				str_activityCode = SHSM_Utility.fsgetActivityCode(str_appCode,str_programTypeCode,str_optionCode,str_entityId);
			}        
        
			if (str_activityCode ==null)
			{
				throw new ProcessException("Security Module: \\n Error in getting activity code");
			}
			//Point No 83 ------------- end <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


			String[] str_arrKeyFields=null;
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING || SHSM_SecurityFeatures.AUDIT_TRAIL || SHSM_SecurityFeatures.EVENT_TRAIL )
			{   
				//String str_appCode	  = (String)obj_colFields.getObject("SAA_APPCODE");
				String str_dbKeyCombination = SHSM_Utility.fsgetPKCombination(str_appCode, str_tableCode );  
    
				if (str_dbKeyCombination != null && str_dbKeyCombination.Length > 0)
				{
					str_arrKeyFields = StringUtility.fssplitString(str_dbKeyCombination, '~');
				}
				else 
				{
					throw new ProcessException("Table: "+ str_tableCode+ " not properly defined in SH_SM_AB_APPTABLE");
				}
			}
			// new code 31/12/2005 - end

			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_now = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
			/*---- MC0038 ---------------------START*/
			//Event Log
			//SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,chr_operation,str_now, false);
			/*---- MC0038 ---------------------END*/

			//Point No 83 ------------- start <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			if (SHSM_SecurityFeatures.EVENT_TRAIL )
			{
				SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,chr_operation,str_now, true,str_activityCode);
			}
			//Point No 83 ------------- end <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


			//--New Feature - Parametric Security 3 - start
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
				//Delte Approved record--> move into history
				if (chr_operation == DML_OPERATION_DELETE) 
				{
					SHSM_TransactionRouter.fsmoveApprovedRecordsInHistory(str_tableCode, str_arrKeyFields, obj_colFields);
				}

				// Document Updates Log
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) 
				{
					SHSM_TransactionRouter.fsprocessDMLOperation( str_entityId, str_arrKeyFields, obj_colFields,chr_operation, str_tableCode,  null );
				}

				//Point No 104 - ADT Changes - start
				/*if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) 
				{
					SHSM_TransactionRouter.fsupdateBusinessEntityTable(str_tableCode, str_arrKeyFields, obj_colFields, chr_operation);
				}*/
				//Point No 104 - ADT Changes - end
			}
			//--New Feature - Parametric Security 3 - end
			
			//--New Feature - Parametric Security 1 - start
			bool bln_value=true;
			if (SHSM_SecurityFeatures.AUDIT_TRAIL)
			{
				//Point No 83 ------------- 
				//bln_value = this.fssaveAuditLog(str_arrKeyFields,  obj_colFields, chr_operation, str_tableCode, str_entityId, str_now);
				bln_value = this.fssaveAuditLog(str_arrKeyFields,  obj_colFields, chr_operation, str_tableCode, str_entityId, str_now, str_activityCode );
			}
			//--New Feature - Parametric Security 1 - end


			//--New Feature - Parametric Security 3 - start
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
			
				// Transaction Routing 
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) 
				{
					SHSM_TransactionRouter.fsrouteTransaction( str_entityId, str_arrKeyFields, obj_colFields,chr_operation, str_tableCode,  "shab.TransactionRouting","" );
				}
				//--Improvement point - start - 20
				//if (chr_operation==DML_OPERATION_INSERT) 
				//{
				//	SHSM_TransactionRouter.fscreateDocumentLogEntry(str_entityId, str_tableCode, obj_colFields, str_arrKeyFields);
				//}
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE)  
				{
					SHSM_TransactionRouter.fscreateDocumentLogEntry(str_entityId, str_tableCode, obj_colFields, str_arrKeyFields, chr_operation);
				}
				//--Improvement point - end - 20
			}
			//--New Feature - Parametric Security 3 - end

			return bln_value;
		}	
		

//--Improvement point 24 - start
		//Method specially for Inventory Module requirement - Call Transaction Routing conditionally
		public virtual bool fssaveAuditLog(String str_entityId, String[] str_arrKeyFields, NameValueCollection obj_colFields, char chr_operation, String str_tableCode, bool bln_trCall)
		{
			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_now = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);

			//--New Feature - Parametric Security 3 - start
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
				//Delte Approved record--> move into history
				if (chr_operation == DML_OPERATION_DELETE) 
				{
					SHSM_TransactionRouter.fsmoveApprovedRecordsInHistory( str_tableCode, str_arrKeyFields,obj_colFields );
				}
				// Document Updates Log
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) 
				{
					SHSM_TransactionRouter.fsprocessDMLOperation( str_entityId, str_arrKeyFields, obj_colFields,chr_operation, str_tableCode,  null );
				}
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) 
				{
					SHSM_TransactionRouter.fsupdateBusinessEntityTable(str_tableCode, str_arrKeyFields, obj_colFields, chr_operation);
				}
			}
			//--New Feature - Parametric Security 3 - end

			//--New Feature - Parametric Security 1 - start
			bool bln_value=true;;
			if (SHSM_SecurityFeatures.AUDIT_TRAIL)
			{
				// Audit Trail Log
				//Point No 83
				//bln_value = this.fssaveAuditLog(str_arrKeyFields,  obj_colFields, chr_operation, str_tableCode, str_entityId, str_now);
			    bln_value = this.fssaveAuditLog(str_arrKeyFields,  obj_colFields, chr_operation, str_tableCode, str_entityId, str_now,null);
			}
			//--New Feature - Parametric Security 1 - end

			//--New Feature - Parametric Security 3 - start
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
				// call Transaction Routing conditionally
				if ((chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE) && bln_trCall) 
				{
					SHSM_TransactionRouter.fsrouteTransaction( str_entityId, str_arrKeyFields, obj_colFields,chr_operation, str_tableCode,  "shab.TransactionRouting","" );
				}
				if (chr_operation==DML_OPERATION_INSERT || chr_operation==DML_OPERATION_UPDATE)  
				{
					SHSM_TransactionRouter.fscreateDocumentLogEntry(str_entityId, str_tableCode, obj_colFields, str_arrKeyFields, chr_operation);
				}
			}
			//--New Feature - Parametric Security 3 - end
			return bln_value;
		}	
//--Improvement point 24 - end


		public virtual bool fssaveAuditLog(String[] str_arrKeyFields, NameValueCollection obj_colFields, char str_operation, String str_tableCode)
		{
			//Point No 83
			//return this.fssaveAuditLog(str_arrKeyFields, obj_colFields, str_operation, str_tableCode,  null,null);
			throw new ProcessException("Security Module: \\n" + "Old Audit Trail is being called. Contact to SHMA technical team." );
		}



		// Over loaded method to support for entity id
// To be verified by Tool Team <<<<<<<<<----------------->>>>>>>>>>>
		//public virtual bool fssaveAuditLog(String[] str_arrKeyFields, NameValueCollection obj_colFields, char str_operation, String str_tableCode, String str_entityId, String str_now)
		//Point No 83 ------------- 
		//private bool fssaveAuditLog(String[] str_arrKeyFields, NameValueCollection obj_colFields, char str_operation, String str_tableCode, String str_entityId, String str_now)
		private bool fssaveAuditLog(String[] str_arrKeyFields, NameValueCollection obj_colFields, char str_operation, String str_tableCode, String str_entityId, String str_now,String str_activityCode)
		{
			bool bln_gTable = false;
			bool bln_gColumn = false;
			bool bln_gColumnSet = false;
			// POINT NO 67
			bool bln_tableColumnSet =false;


			
/// point No 41 - Start
/// lines are commit due to point No 41

			///rowset rsSettings = null;
			//try
			//{ 
			//	rsSettings = DB.executeQuery("SELECT * FROM SH_SM_GS_GLOBALSETTING" );}
			//catch (Exception e)
			//{
			//	//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043_3"'
			//	//throw new ProcessException("General Error: " + e.Message);
			//	//-- Message changes 2006/01/09
			//	throw new ProcessException("Security Module: \\n" + "General Error: Failed to save audit log. \n" + e.Message);
			// }
			//try 
			//{ 
			//if (rsSettings == null || rsSettings.size() == 0) 
			//	{
			//throw new ProcessException("Security Module: \\n" + "Global Settings could not be found");	}}
			//catch(Exception e) 
			//{
			//	//throw new ProcessException(e.Message);
			//	//-- Message changes 2006/01/09
			//	throw new ProcessException("Security Module: \\n" + "Error: Failed to save audit trail. \n" + e.Message);
			//}
			//try
			//{
			//	if (rsSettings.next())
			//	{
			EnvHelper sessionValues = new EnvHelper();
					
					//String str_temp = rsSettings.getString("SGS_TABLEAUDIT");
					String str_temp = (String) sessionValues.getAttribute("s_SGS_TABLEAUDIT");
					bln_gTable = str_temp.ToUpper().Equals("Y".ToUpper());
					
					//str_temp = rsSettings.getString("SGS_COLUMNAUDIT");
					str_temp = (String) sessionValues.getAttribute("s_SGS_COLUMNAUDIT");
					bln_gColumn = str_temp.ToUpper().Equals("Y".ToUpper());
					
					//str_temp = rsSettings.getString("SGS_COLUMNSETAUDIT");
					
					str_temp = (String) sessionValues.getAttribute("s_SGS_COLUMNSETAUDIT");
					bln_gColumnSet = str_temp.ToUpper().Equals("Y".ToUpper());
			//	}
			//}
			//catch (OleDbException e)
			//{
			//	throw new ProcessException("Security Module: \\n" + "Global Settings error: " + e.Message);
			//}
			//catch (Exception e)
			//{
			//	throw new ProcessException("Security Module: \\n" + "Global Settings error: " + e.Message);
			//}
			//finally
			//{
			//	try
			//	{
			//		rsSettings.close();
			//	}
			//	catch (OleDbException e)
			//	{
			//	}
			//}
/// point No 41 - End
/// lines are commit due to point No 41

			if (!bln_gTable)
			{
				//Point No 83 ------------- stopped
				//SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,str_operation,str_now, false);
				return true;
			}
			

			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");
			String str_programTypeCode = (String) sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode = (String) sessionValues.getAttribute("s_SAO_OPTCODE");
			String str_suboptionCode = (String) sessionValues.getAttribute("s_SAN_SUBOPTCODE");
			String str_userCode = (String) sessionValues.getAttribute("s_SUS_USERCODE");
			String str_networkID = (String) sessionValues.getAttribute("s_SUS_NETWORKID");
			String str_ipAddress = (String) sessionValues.getAttribute("s_SUS_IPADDRESS");
		
			//Point No 83 ------------- stopped
			//String str_activityCode = null;
				
			//Point No 83 -------------   
			if (str_activityCode == null)
			{
				if (str_entityId == null || str_entityId.Trim().Length == 0) 
				{
					String str_where=  " WHERE SAA_APPCODE='" + str_appCode + "'" 
						+ " AND SPT_PRGTYPECODE='" + str_programTypeCode + "'" 
						+ " AND SAO_OPTCODE='" + str_optionCode + "'" 
						+ " AND SAN_SUBOPTCODE='" + str_suboptionCode + "'"
						+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
					//T00009
					str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_where);
				}
				else 
				{
					str_activityCode = SHSM_Utility.fsgetActivityCode(str_appCode,
						str_programTypeCode,
						str_optionCode,
						str_entityId);
				}
			}

			//Point No 83 -------------  stopped
			//if (str_activityCode == null)
			//{
			//	SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,str_operation,str_now, false);
			//	return true;
			//	// 12122005
			//	//				throw new ProcessException("Error in getting activity code for Audit Trail Log");
			//}

			String str_sql = "";
			
			str_sql= " WHERE SAA_APPCODE='" + str_appCode + "'" 
				+ " AND SAV_ACTCODE='" + str_activityCode + "'" 
				+ " AND SAB_TABLECODE='" + str_tableCode + "'"
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE  ;

			//T00009
			bool bln_skipLog = !SHSM_Utility.fsrecordExists("SH_SM_AU_ACTAUDTRIAL", str_sql);

			if (bln_skipLog)
			{
				//Point No 83 ------------- Stopped
				//SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,str_operation,str_now, false);
				return true;
			}
			//Point No 83 ------------- Stopped
			//SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,str_operation,str_now, true);



			
			// At this point
			// log must be generated
			String[] str_columnCombination = null;
			if (bln_gColumnSet)
			{
				str_sql = " WHERE SAA_APPCODE='" + str_appCode + "'" 
					+ " AND SAV_ACTCODE='" + str_activityCode + "'" 
					+ " AND SAB_TABLECODE='" + str_tableCode + "'"
					+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
				
				str_columnCombination = SHSM_Utility.fsgetColumnValuesAgainstQuery("SH_SM_CD_COLAUDTRIAL", "SAC_COLCODE", str_sql);
				// POINT NO 67
				String str_tempvalue = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AU_ACTAUDTRIAL","SAU_COLUMNAUDITSET",str_sql);
				bln_tableColumnSet =false;
				if(str_tempvalue!=null) 
				{
					bln_tableColumnSet = str_tempvalue.ToUpper().Equals("Y");
				}
			}
			
			System.Collections.IEnumerator itr_fields = obj_colFields.keysIterator();
			System.Collections.IEnumerator itr_values = obj_colFields.iterator();
			
			StringBuilder sb_keyValues = new StringBuilder(100);
			StringBuilder sb_fieldsToSave = new StringBuilder(100);
			StringBuilder sb_valuesToSave = new StringBuilder(100);

			System.Array.Sort(str_arrKeyFields);

			if (str_columnCombination != null)
				System.Array.Sort(str_columnCombination);
			
			while (itr_fields.MoveNext())
			{
				itr_values.MoveNext(); 
				String str_field = System.Convert.ToString(itr_fields.Current);
				System.Collections.DictionaryEntry DE = (System.Collections.DictionaryEntry) itr_values.Current ;

				String str_value = "";

				if (DE.Value != null )
					str_value = DE.Value.ToString() ;
				
				if (System.Array.BinarySearch(str_arrKeyFields, str_field) > - 1)
				{
					sb_keyValues.Append(str_value);
					sb_keyValues.Append('~');
				}
				else
				{
					/// POINT NO 67
					//if (bln_gColumnSet)
					if (bln_gColumnSet && bln_tableColumnSet)
					{
						if (System.Array.BinarySearch(str_columnCombination, str_field) > - 1)
						{
							sb_fieldsToSave.Append(str_field);
							sb_fieldsToSave.Append('~');
							sb_valuesToSave.Append(str_value);
							//--- Point 73 - Audit Trail Log - Replace ~ with two ` character
							//sb_valuesToSave.Append('~');
							sb_valuesToSave.Append("``");
						}
					}
					else if (bln_gColumn) 
					{
						sb_fieldsToSave.Append(str_field);
						sb_fieldsToSave.Append('~');
						sb_valuesToSave.Append(str_value);
						//--- Point 73 - Audit Trail Log - Replace ~ with two ` character
						//sb_valuesToSave.Append('~');
						sb_valuesToSave.Append("``");
					}
				}
			}
			
			
			int i_pos = - 1;
			
			i_pos = sb_keyValues.Length - 1;
			if (sb_keyValues[i_pos] == '~')
				sb_keyValues.Remove(i_pos, 1);
			
			if(sb_fieldsToSave.Length > 0 ) 
			{ 			
				i_pos = sb_fieldsToSave.Length - 1;
				if (sb_fieldsToSave[i_pos] == '~')
					sb_fieldsToSave.Remove(i_pos, 1);
			}
			else
				sb_fieldsToSave.Append(" ");

			if (sb_valuesToSave.Length > 0 ) 
			{
				//--- Point 73 - Audit Trail Log - Replace ~ with two ` character - START
				//i_pos = sb_valuesToSave.Length - 1;
				//if (sb_valuesToSave[i_pos] == '~')
				//	sb_valuesToSave.Remove(i_pos, 1);
				i_pos = sb_valuesToSave.ToString().Length-2;
				if (sb_valuesToSave.ToString().Substring(i_pos,2) == "``")
					sb_valuesToSave.Remove(i_pos, 2);
				//--- Point 73 - Audit Trail Log - Replace ~ with two ` character - END
			}
			else
				sb_valuesToSave.Append(" ");

			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_today = null;
			
			if (str_now==null)
				str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
			else
				str_today = str_now;

			//ASIF OK
			//String str_todayForUI = SHSM_DateTimeManager.fsgetDateStringFromString(SHSM_UserOperation.FORMAT_TIMESTAMP, str_today);
			//String str_todayForUI = DateTimeManager.fsgetFormattedDate( GlobalConstants.FORMAT_TIMESTAMP_UI ) ;
			//V0001-- START
			String str_today_Sec = SHSM_DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC, str_today);
			//V0001 END

			

			//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb ----- START
			
			String str_valueComb1;
			String str_valueComb2;
			String str_valueComb3;
			String str_valueComb4;
			int i_valLen=0;

			//sb_valuesToSave = StringUtility.fsreplace(sb_valuesToSave.ToString(),"'","''");
			//StringUtility.fsreplace(sb_valuesToSave.ToString(),"'","''");
			String str_valueComb=sb_valuesToSave.ToString();
			str_valueComb = str_valueComb.Replace("'","''");
			i_valLen=str_valueComb.Length;

			if (i_valLen<=4000)
			{
				//str_valueComb1="'" + sb_valuesToSave.ToString() + "'";
				str_valueComb1="'" + str_valueComb + "'";
				str_valueComb2="NULL";
				str_valueComb3="NULL";
				str_valueComb4="NULL";
			}
			else
			{
				if (i_valLen<=8000)
				{
					//str_valueComb1="'" + sb_valuesToSave.ToString().Substring(0,4000) + "'";
					//str_valueComb2="'" + sb_valuesToSave.ToString().Substring(4000) + "'";
					str_valueComb1="'" + str_valueComb.Substring(0,4000) + "'";
					str_valueComb2="'" + str_valueComb.Substring(4000) + "'";
					str_valueComb3="NULL";
					str_valueComb4="NULL";
				}
				else
				{
					if (i_valLen<=12000)
					{
						//str_valueComb1="'" + sb_valuesToSave.ToString().Substring(0,4000) + "'";
						//str_valueComb2="'" + sb_valuesToSave.ToString().Substring(4000,4000) + "'";
						//str_valueComb3="'" + sb_valuesToSave.ToString().Substring(8000) + "'";
						str_valueComb1="'" + str_valueComb.Substring(0,4000) + "'";
						str_valueComb2="'" + str_valueComb.Substring(4000,4000) + "'";
						str_valueComb3="'" + str_valueComb.Substring(8000) + "'";
						str_valueComb4="NULL";
					}
					else if (i_valLen>16000)
					{
						throw new ProcessException("Security Module: \\n" + "Error in generating Audit Trail log \\n Value Combination exceeded the limit of 16000 characters");
					}
					else
					{
						//str_valueComb1="'" + sb_valuesToSave.ToString().Substring(0,4000) + "'";
						//str_valueComb2="'" + sb_valuesToSave.ToString().Substring(4000,4000) + "'";
						//str_valueComb3="'" + sb_valuesToSave.ToString().Substring(8000,4000) + "'";
						//str_valueComb4="'" + sb_valuesToSave.ToString().Substring(12000) + "'";
						str_valueComb1="'" + str_valueComb.Substring(0,4000) + "'";
						str_valueComb2="'" + str_valueComb.Substring(4000,4000) + "'";
						str_valueComb3="'" + str_valueComb.Substring(8000,4000) + "'";
						str_valueComb4="'" + str_valueComb.Substring(12000) + "'";
					}
				}
			}
			//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb ----- END



			//T00005 --- OK 1
			/*----MC0019 ---------------------START*/
			//str_sql = "INSERT INTO SH_SM_AD_APPACTAUDLOG " 
			//	+ "(SAA_APPCODE,SAV_ACTCODE,SAB_TABLECODE,SAD_VALUE,SAD_DATETIME,SAD_OPERATION, SUS_USERCODE, SAD_FIELDCOMB, SAD_VALUECOMB, SAD_NETWORKID, SAD_IPADDRESS, SAD_DATETIMEACTUAL) " 
			//	+ "VALUES ( " + "'" + str_appCode + "', " + "'" + str_activityCode + "', " + "'" + str_tableCode + "', " + "'" + StringUtility.fsreplace(sb_keyValues.ToString(),"'","''") + "', " + "'" + str_todayForUI + "', " + "'" + str_operation + "', " + "'" + str_userCode + "', " + "'" + sb_fieldsToSave.ToString() + "', " + "'" + StringUtility.fsreplace(sb_valuesToSave.ToString(),"'","''") + "', " + "'" + str_networkID + "', '"+str_ipAddress+"', '"+str_today+"') ";
			str_sql = "INSERT INTO SH_SM_AD_APPACTAUDLOG "
				//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb
				//+ "(SAA_APPCODE,SAV_ACTCODE,SAB_TABLECODE,SAD_DOCREF,SAD_DATETIME,SAD_OPERATION, SUS_USERCODE, SAD_FIELDCOMB, SAD_VALUECOMB, SAD_NETWORKID, SAD_IPADDRESS, SAD_DATETIMEACTUAL, SAD_COLUMNDATA) "
				+ "(SAA_APPCODE,SAV_ACTCODE,SAB_TABLECODE,SAD_DOCREF,SAD_DATETIME,SAD_OPERATION, SUS_USERCODE, SAD_FIELDCOMB, SAD_VALUECOMB, SAD_VALUECOMB2, SAD_VALUECOMB3, SAD_VALUECOMB4, SAD_NETWORKID, SAD_IPADDRESS, SAD_DATETIMEACTUAL, SAD_COLUMNDATA) "
				
				+ "VALUES ( "
				+ "'" + str_appCode + "', "
				+ "'" + str_activityCode + "', "
				+ "'" + str_tableCode + "', "
				+ "'" + StringUtility.fsreplace(sb_keyValues.ToString(),"'","''") + "', "
				+ "'" + str_today + "', "
				+ "'" + str_operation + "', "
				+ "'" + str_userCode + "', "
				+ "'" + sb_fieldsToSave.ToString() + "', "
				//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb ----- START
				//+ "'" + StringUtility.fsreplace(sb_valuesToSave.ToString(),"'","''") + "', "
				+ str_valueComb1 + ","
				+ str_valueComb2 + ","
				+ str_valueComb3 + ","
				+ str_valueComb4 + ","
				//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb ----- END
				///Point No. 80 <<<<< - Nullable >> str_networkID, str_ipAddress
				//+ "'" + str_networkID + "', "
				//+ "'" + str_ipAddress + "', "
				+ (str_networkID==null ? "NULL," : "'"+str_networkID+"'," )
				+ (str_ipAddress==null ? "NULL," : "'"+str_ipAddress+"'," )
				//V0001 -- Start
				//+ "'" + str_today + "') ";
				//Point No 102 - Oracle Support - start
				//+ "'" + str_today_Sec + "', "
				+ "?,"
				//Point No 102 - Oracle Support - end
				//Dotnet changes - Asif 10/28/2005
				+ "'N')";
			//Dotnet changes - Asif 10/28/2005
			// V0001 -- end
			/*----MC0019 ---------------------END*/

			//Point No 102 - Oracle Support - Start
			ParaColl.clear();
			ParaColl.puts("@SAD_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_Sec), Types.TIMESTAMP);
			//Point No 102 - Oracle Support - end
			

			try
			{
				//Point No 102 - Oracle Support - Start
				//DB.executeDML(str_sql);
				DB.executeDML(str_sql,ParaColl);
				//Point No 102 - Oracle Support - End
			}
			catch (OleDbException e)
			{
				throw new ProcessException("Security Module: \\n" + "Error in generating log" + e.Message);
			}
			catch (Exception e)
			{
				//throw new ProcessException(e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Error: Failed to save audit log. \n" + e.Message);
			}
			

			return true;
		}



		/*---- MC0029 ---------------------START*/
		public static void fsauditColumnData(String str_userCode, String str_appCode, String str_dt_dateFrom, String str_dt_DateTo)
		{
			String str_qry="";
			String 	STR_UpdateColData="";
			rowset rs_entity = null;
			try
			{
				//Point No 102 - Oracle Support - Start
				//String qry = "SELECT * FROM SH_SM_AD_APPACTAUDLOG WHERE SAD_DATETIMEACTUAL BETWEEN '" +str_dt_dateFrom+"' AND '"+str_dt_DateTo+"' AND SAD_COLUMNDATA='N'";
				String qry = "SELECT * FROM SH_SM_AD_APPACTAUDLOG WHERE SAD_DATETIMEACTUAL BETWEEN ? AND ? AND SAD_COLUMNDATA='N'";
				//Point No 102 - Oracle Support - End

				if(str_userCode != null && str_userCode.Length !=0 )
					qry += "AND SUS_USERCODE = '"+str_userCode+"'";

				if(str_appCode != null && str_appCode.Length !=0 )
					qry += "AND SAA_APPCODE = '"+str_appCode+"'";

				//Point No 102 - Oracle Support - Start
				ParaColl.clear();
				ParaColl.puts("@SAD_DATETIMEACTUALFROM", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_dt_dateFrom), Types.DATE);
				ParaColl.puts("@SAD_DATETIMEACTUALTO", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_dt_DateTo), Types.DATE);
				//rs_entity= DB.executeQuery(qry);
				rs_entity= DB.executeQuery(qry,ParaColl);
				//Point No 102 - Oracle Support - end


				String[] arr_field=null;
				String[] arr_value=null;
				//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - Start
				String[] arr_value1=null;
				String[] arr_value2=null;
				String[] arr_value3=null;
				String[] arr_value4=null;
				//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - End
				String[] arr_appfield=null;
				String[] arr_actfield=null;
				String[] arr_tablefield=null;
				String[] arr_docfield=null;
				String[] arr_userfield=null;
				String[] arr_docDate=null;
				// new cdns
				String[] arr_docDateActual =null;
				// new cdns
				String[] arr_operationfield=null;

				if(rs_entity!= null)
				{
					String str_fieldComb  = null ;
					String str_valueComb  = null ;
					//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - START
					String str_valueComb1  = null ;
					String str_valueComb2  = null ;
					String str_valueComb3  = null ;
					String str_valueComb4  = null ;
					//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - END
					String str_appfield   = null ;
					String str_actfield   = null ;
					String str_tablefield = null ;
					String str_docfield   = null ;
					String str_userfield  = null ;
					String str_datefield  = null;
					// NEW CDNS
					DateTime dt_datefieldActual;
					// NEW CDNS 
					String str_operationfield= null;
					

					//Reinitialize array variables
					int int_size =rs_entity.size(); 
					arr_field = new String[int_size];
					arr_value = new String[int_size];
					//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - START
					arr_value1 = new String[int_size];
					arr_value2 = new String[int_size];
					arr_value3 = new String[int_size];
					arr_value4 = new String[int_size];
					//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - END
					arr_appfield = new String[int_size];
					arr_actfield = new String[int_size];
					arr_tablefield = new String[int_size];
					arr_docfield = new String[int_size];
					arr_userfield = new String[int_size];
					arr_docDate = new String[int_size];
					// new cdns
					arr_docDateActual = new String[int_size];
					// new cdns
					arr_operationfield = new String[int_size];

					int rowCount=0;

					
					while( rs_entity.next())
					{

						str_appfield        = rs_entity.getString("SAA_APPCODE");
						str_actfield        = rs_entity.getString("SAV_ACTCODE");
						str_tablefield      = rs_entity.getString("SAB_TABLECODE");
						str_docfield        = rs_entity.getString("SAD_DOCREF");
						str_userfield       = rs_entity.getString("SUS_USERCODE");
						str_fieldComb       = rs_entity.getString("SAD_FIELDCOMB");
						//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - START
						//str_valueComb      = rs_entity.getString("SAD_VALUECOMB");
						str_valueComb1       = rs_entity.getString("SAD_VALUECOMB");
						str_valueComb2       = rs_entity.getString("SAD_VALUECOMB2");
						str_valueComb3       = rs_entity.getString("SAD_VALUECOMB3");
						str_valueComb4       = rs_entity.getString("SAD_VALUECOMB4");
						if (str_valueComb1!=null)
						{
							str_valueComb1 = str_valueComb1.Replace("'","''") ;
						}
						if (str_valueComb2!=null)
						{
							str_valueComb2 = str_valueComb2.Replace("'","''") ;
						}
						if (str_valueComb3!=null)
						{
							str_valueComb3 = str_valueComb3.Replace("'","''") ;
						}
						if (str_valueComb4!=null)
						{
							str_valueComb4 = str_valueComb4.Replace("'","''") ;
						}
						str_valueComb= (str_valueComb1==null?"":str_valueComb1) + (str_valueComb2==null?"":str_valueComb2) + (str_valueComb3==null?"":str_valueComb3) + (str_valueComb4==null?"":str_valueComb4) ;
						//--- Point 72 - Audit Trail Log - 3 new fields for Value Comb - END
						
						//--- Point 73 - Audit Trail Log - Replace ~ with two ` character
						str_valueComb=str_valueComb.Replace("``","`");


						str_datefield       = rs_entity.getString("SAD_DATETIME");
						// NEW CDNS
						dt_datefieldActual = rs_entity.getDate("SAD_DATETIMEACTUAL");
						// NEW CDNS
						str_operationfield  = rs_entity.getString("SAD_OPERATION");


						arr_field[rowCount]     = str_fieldComb;
						arr_value[rowCount]     = str_valueComb;
						arr_appfield[rowCount]  = str_appfield;
						arr_actfield[rowCount]  = str_actfield;
						arr_tablefield[rowCount]= str_tablefield;
						arr_docfield[rowCount]  = str_docfield;
						arr_docDate[rowCount]  = str_datefield;
						// new cdns
						arr_docDateActual[rowCount] = SHSM_DateTimeManager.fsgetFormattedDate(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC,dt_datefieldActual); ;
						// new cdns
						arr_userfield[rowCount] = str_userfield;
						arr_operationfield[rowCount] = str_operationfield;

						rowCount= rowCount+1;
					}
						
					for (int i=0; i<rs_entity.size() ; i++)
					{
						String[] arr_tempField= StringUtility.fssplitString(arr_field[i],'~');
						
						//--- Point 73 - Audit Trail Log - Replace ~ with two ` character
						//String[] arr_tempValue= StringUtility.fssplitString(arr_value[i],'~');
						String[] arr_tempValue;
						if (arr_value[i].IndexOf("`")!=-1)
						{
							arr_tempValue= StringUtility.fssplitString(arr_value[i],'`');
						}
						else
						{
							arr_tempValue= StringUtility.fssplitString(arr_value[i],'~');
						}
						
						

						for (int j=0; j<arr_tempField.Length; j++)
						{
							if ( arr_operationfield[i].ToUpper().Equals("I") )
							{
								//new cdns
								if(arr_tempValue[j].Equals("") || arr_tempValue[j] == null)
								{
									arr_tempValue[j]=" ";
								}
								arr_tempValue[j] = StringUtility.fsreplace(arr_tempValue[j],"'","''");
								//new cdns
								///Point No. 80 <<<<< - Nullable Old value & New Value
								str_qry  = "INSERT INTO SH_SM_AL_AUDITDETAIL (SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SAD_DOCREF, SAD_DATETIME, SAD_DATETIMEACTUAL, SUS_USERCODE , SAC_COLCODE, SAL_COLNEWVALUE, SAL_COLOLDVALUE) ";
								//str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', '"+arr_docDate[i]+"', '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', '"+arr_tempValue[j]+"', '')";
								
								//Point No 102 - Oracle Support - Start
								//str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', '"+arr_docDateActual[i]+"', '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'" )+", NULL)";
								str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', ?, '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'" )+", NULL)";
								ParaColl.clear();
								ParaColl.puts("@SAD_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(arr_docDateActual[i]), Types.TIMESTAMP);
								//Point No 102 - Oracle Support - End
							}
							if ( arr_operationfield[i].ToUpper().Equals("D") )
							{
								///Point No. 80 <<<<< - Nullable Old value & New Value
								str_qry  = "INSERT INTO SH_SM_AL_AUDITDETAIL (SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SAD_DOCREF, SAD_DATETIME, SAD_DATETIMEACTUAL, SUS_USERCODE, SAC_COLCODE, SAL_COLOLDVALUE, SAL_COLNEWVALUE) ";
								//str_qry += " VALUES ('"+ str_appfield+"', '"+str_actfield+"', '"+str_tablefield+"', '"+str_docfield+"', '"+str_datefield+"', '"+str_userfield+"', '"+arr_tempField[j]+"', '', '"+arr_tempValue[j]+"')";

								//Point No 102 - Oracle Support - Start
								//str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', '"+arr_docDateActual[i]+"', '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'" )+", NULL)";
								str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', ?, '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'" )+", NULL)";
								ParaColl.clear();
								ParaColl.puts("@SAD_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(arr_docDateActual[i]), Types.TIMESTAMP);
								//Point No 102 - Oracle Support - End

							}
							if ( arr_operationfield[i].ToUpper().Equals("U") )
							{
								//-- Process dotnet - asif 16/12/2005 -start
								//String str_QryforUpdate = "select SAD_VALUECOMB from sh_sm_ad_appactaudlog WHERE SAA_APPCODE = '"+str_appfield+"'";
								//str_QryforUpdate += " AND SAV_ACTCODE = '"+str_actfield+"' AND  SAB_TABLECODE = '"+str_tablefield+"' AND SAD_DOCREF = '"+str_docfield+"' AND SAD_FIELDCOMB ='"+arr_field[i]+"'";
								//str_QryforUpdate +=" AND SAD_DATETIMEACTUAL < (SELECT MAX(SAD_DATETIMEACTUAL) FROM sh_sm_ad_appactaudlog WHERE SAA_APPCODE = '"+str_appfield+"'";
								//str_QryforUpdate += " AND SAV_ACTCODE = '"+str_actfield+"' AND  SAB_TABLECODE = '"+str_tablefield+"' AND SAD_DOCREF = '"+str_docfield+"' AND SAD_FIELDCOMB = '"+arr_field[i]+"'";
								String str_QryforUpdate = "select SAD_FIELDCOMB, SAD_VALUECOMB from sh_sm_ad_appactaudlog WHERE SAA_APPCODE = '"+str_appfield+"' ";
								str_QryforUpdate += " AND SAV_ACTCODE = '"+str_actfield+"' AND  SAB_TABLECODE = '"+str_tablefield+"' AND SAD_DOCREF = '"+str_docfield+"' AND SAD_FIELDCOMB like  '%"+arr_field[i]+"%' ";
								
								//Point No 102 - Oracle Support - Start
								//str_QryforUpdate +=" AND SAD_DATETIMEACTUAL < '" + arr_docDate[i].ToString() + "' ORDER BY SAD_DATETIMEACTUAL DESC ";
								str_QryforUpdate +=" AND SAD_DATETIMEACTUAL < ? ORDER BY SAD_DATETIMEACTUAL DESC ";
								ParaColl.clear();
								ParaColl.puts("@SAD_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(arr_docDate[i].ToString()), Types.TIMESTAMP);
								//Point No 102 - Oracle Support - end
								
								//-- Process dotnet - asif 16/12/2005 end

								//Point No 102 - Oracle Support - Start
								//rowset rs_temp= DB.executeQuery(str_QryforUpdate);
								rowset rs_temp= DB.executeQuery(str_QryforUpdate,ParaColl);
								//Point No 102 - Oracle Support - End

								String str_fieldName=arr_tempField[j];
								String str_fieldValue="";
								if (rs_temp != null && rs_temp.next())
								{
									//-- Process dotnet - asif 16/12/2005 - START
									String[] arr_fields = StringUtility.fssplitString(rs_temp.getString("SAD_FIELDCOMB"),'~');
									String[] arr_values = StringUtility.fssplitString(rs_temp.getString("SAD_VALUECOMB"),'~');

									int intIndex;
									for (intIndex = 0; intIndex<arr_fields.Length; intIndex++)
									{
										if (arr_fields[intIndex] == str_fieldName )
										{
											str_fieldValue= arr_values[intIndex];
											break;
										}
									}
									rs_temp.close();

								}//end of if

								///Point No. 80 <<<<< - Nullable Old value & New Value
								str_qry  = "INSERT INTO SH_SM_AL_AUDITDETAIL (SAA_APPCODE, SAV_ACTCODE, SAB_TABLECODE, SAD_DOCREF, SAD_DATETIME, SAD_DATETIMEACTUAL, SUS_USERCODE, SAC_COLCODE, SAL_COLNEWVALUE, SAL_COLOLDVALUE) ";
								//str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', '"+arr_docDate[i]+"', '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', '"+arr_tempValue[j]+"', '"+ str_fieldValue+"' )";
								
								//Point No 102 - Oracle Support - Start
								//str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', '"+arr_docDateActual[i]+"', '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'") + "," +  (str_fieldValue==null ? "NULL" : "'"+str_fieldValue+"'") + ")";
								str_qry += " VALUES ('"+ arr_appfield[i]+"', '"+arr_actfield[i]+"', '"+arr_tablefield[i]+"', '"+arr_docfield[i]+"', '"+arr_docDate[i]+"', ?, '"+arr_userfield[i]+"', '"+arr_tempField[j]+"', "+(arr_tempValue[j]==null ? "NULL" : "'"+arr_tempValue[j]+"'") + "," +  (str_fieldValue==null ? "NULL" : "'"+str_fieldValue+"'") + ")";
								ParaColl.clear();
								ParaColl.puts("@SAD_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(arr_docDateActual[i]), Types.TIMESTAMP);
								//Point No 102 - Oracle Support - End

								//str_qry += " VALUES ('"+ str_appfield+"', '"+str_actfield+"', '"+str_tablefield+"', '"+str_docfield+"', '"+str_datefield+"', '"+str_userfield+"', '"+arr_tempField[j]+"', '"+arr_tempValue[j]+"', '"+ str_fieldValue+"')";
								//-- Process dotnet - asif 16/12/2005 - END

							}//end of if "U"


							System.Console.WriteLine("SQL is --> " + str_qry);
							//Point No 102 - Oracle Support - Start
							//DB.executeDML(str_qry);
							DB.executeDML(str_qry,ParaColl);
							//Point No 102 - Oracle Support - End
						}//end of For 'j' loop

						//-- Process dotnet - asif 16/12/2005 - start
						STR_UpdateColData = "UPDATE SH_SM_AD_APPACTAUDLOG SET SAD_COLUMNDATA='Y' WHERE SAA_APPCODE = '"+arr_appfield[i]+"'";
						STR_UpdateColData +=" AND SAV_ACTCODE = '"+arr_actfield[i]+"' AND  SAB_TABLECODE = '"+arr_tablefield[i]+"' AND SAD_DOCREF = '"+arr_docfield[i]+"'";
						STR_UpdateColData +=" AND SAD_DATETIME = '"+arr_docDate[i]+"' AND SUS_USERCODE = '" + arr_userfield[i] + "'" ;
						DB.executeDML(STR_UpdateColData);
						//-- Process dotnet - asif 16/12/2005 - END

					}//end of For 'i' loop

				
				
				
				} //END OF IF
			} //END OF TRY
			catch(SQLException e)
			{
				//throw new ProcessException("Exception in SQL: " + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "Exception in SQL: Failed to save Column data for audit trail. \n" + e.Message);
			}
			catch(Exception e)
			{
				//throw new ProcessException("General Error: " + e.Message);
				//-- Message changes 2006/01/09
				throw new ProcessException("Security Module: \\n" + "General Error: Failed to save Column data for audit trail. \n" + e.Message);
			}
			finally
			{
				try	
				{
					if (rs_entity != null)
						rs_entity.close();
				}
				catch (SQLException e){	}
				rs_entity = null;
			}
		}   //END OF METHOD
		/*---- MC0029 ---------------------END*/

		//---- New Wrapper methods for MIC
		public bool fssaveAuditTrail(String str_entityId, String[] str_arrKeyFields_temp, NameValueCollection obj_colFields, char str_operation, String str_tableCode)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_directOption = (String) sessionValues.getAttribute("DIRECTOPTION");

			if (str_directOption != null )
			{
				return true;
			}

			String str_appCode 		= (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String str_activityCode=fsgetActivity(str_appCode, str_entityId, sessionValues);

			String[] str_arrKeyFields=null;
			if (SHSM_SecurityFeatures.AUDIT_TRAIL)
			{
				String str_dbKeyCombination = SHSM_Utility.fsgetPKCombination(str_appCode, str_tableCode );

				if (str_dbKeyCombination != null && str_dbKeyCombination.Length > 0)
				{
					str_arrKeyFields = StringUtility.fssplitString(str_dbKeyCombination, '~');
				}
				else
				{
					throw new ProcessException("Table: "+ str_tableCode+ " not properly defined in SH_SM_AB_APPTABLE");
				}
			}
			else
			{
				str_arrKeyFields = str_arrKeyFields_temp;
			}

			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_now = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);

			bool bln_value=true;
			if (SHSM_SecurityFeatures.AUDIT_TRAIL)
			{
				bln_value = this.fssaveAuditLog(str_arrKeyFields,  obj_colFields, str_operation, str_tableCode, str_entityId , str_now, str_activityCode );
			}
			return bln_value;
		}

		//---- New Wrapper methods for MIC
		public bool fssaveEventTrail(String str_entityId, String[] str_arrKeyFields_temp, NameValueCollection obj_colFields, char str_operation, String str_tableCode)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_directOption = (String) sessionValues.getAttribute("DIRECTOPTION");

			if (str_directOption != null )
			{
				return true;
			}

			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String str_activityCode=fsgetActivity(str_appCode, str_entityId, sessionValues);

			String[] str_arrKeyFields=null;
			if (SHSM_SecurityFeatures.EVENT_TRAIL)
			{
				String str_dbKeyCombination = SHSM_Utility.fsgetPKCombination(str_appCode, str_tableCode );

				if (str_dbKeyCombination != null && str_dbKeyCombination.Length > 0)
				{
					str_arrKeyFields = StringUtility.fssplitString(str_dbKeyCombination, '~');
				}
				else
				{
					throw new ProcessException("Table: "+ str_tableCode+ " not properly defined in SH_SM_AB_APPTABLE");
				}
			}
			else
			{
				str_arrKeyFields = str_arrKeyFields_temp;
			}

			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_now = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);

			bool bln_value=false;
			if (SHSM_SecurityFeatures.AUDIT_TRAIL)
			{
				SHSM_EventTrail.fssaveButtonEventData(str_entityId,str_tableCode,str_arrKeyFields,obj_colFields,str_operation,str_now, true,str_activityCode);
				bln_value=true;
			}
			return bln_value;
		}

		//---- New Wrapper methods for MIC
		public bool fssaveTransactionRouting(String str_entityId, String[] str_arrKeyFields_temp, NameValueCollection obj_colFields, char str_operation, String str_tableCode)
		{
			EnvHelper sessionValues = new EnvHelper();
			String str_directOption = (String) sessionValues.getAttribute("DIRECTOPTION");

			if (str_directOption != null )
			{
				return true;
			}

			String str_appCode = (String) sessionValues.getAttribute("s_SAA_APPCODE");

			String str_activityCode=fsgetActivity(str_appCode, str_entityId, sessionValues);

			String[] str_arrKeyFields=null;
			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
				String str_dbKeyCombination = SHSM_Utility.fsgetPKCombination(str_appCode, str_tableCode );

				if (str_dbKeyCombination != null && str_dbKeyCombination.Length > 0)
				{
					str_arrKeyFields = StringUtility.fssplitString(str_dbKeyCombination, '~');
				}
				else
				{
					throw new ProcessException("Table: "+ str_tableCode+ " not properly defined in SH_SM_AB_APPTABLE");
				}
			}
			else
			{
				str_arrKeyFields = str_arrKeyFields_temp;
			}

			GlobalConstants obj_global = GlobalConstants.getInstance();
			String str_now = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);

			bool bln_value=false;

			if (SHSM_SecurityFeatures.TRANSACTION_ROUTING)
			{
				//Delte Approved record--> move into history
				if (str_operation == DML_OPERATION_DELETE)
				{
					SHSM_TransactionRouter.fsmoveApprovedRecordsInHistory( str_tableCode, str_arrKeyFields,obj_colFields );
				}

				// Document Updates Log
				if (str_operation==DML_OPERATION_INSERT || str_operation==DML_OPERATION_UPDATE)
				{
					SHSM_TransactionRouter.fsprocessDMLOperation( str_entityId, str_arrKeyFields, obj_colFields,str_operation, str_tableCode,  null );
					SHSM_TransactionRouter.fsupdateBusinessEntityTable(str_tableCode, str_arrKeyFields, obj_colFields, str_operation);
					SHSM_TransactionRouter.fsrouteTransaction( str_entityId, str_arrKeyFields, obj_colFields,str_operation, str_tableCode,  "shab.TransactionRouting","" );
					SHSM_TransactionRouter.fscreateDocumentLogEntry(str_entityId, str_tableCode, obj_colFields, str_arrKeyFields, str_operation);

				}
				return true;
			}


			return bln_value;
		}

		//---- New Wrapper methods for MIC
		private String fsgetActivity(String str_appCode, String str_entityId, EnvHelper sessionValues)
		{
			String str_activityCode=null;

			String str_programTypeCode	= (String)sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			String str_optionCode		= (String)sessionValues.getAttribute("s_SAO_OPTCODE");
			String str_suboptionCode	= (String)sessionValues.getAttribute("s_SAN_SUBOPTCODE");
			String str_userCode		= (String)sessionValues.getAttribute("s_SUS_USERCODE");
			String str_networkID		= (String)sessionValues.getAttribute("s_SAD_NETWORKID");
			String str_ipAddress 		= (String) sessionValues.getAttribute("s_SUS_IPADDRESS");

			if (str_entityId == null || str_entityId.Trim().Length == 0)
			{
				String str_where =  " WHERE SAA_APPCODE='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND SAN_SUBOPTCODE='"+str_suboptionCode+"' AND "+SHSM_Utility.STATUS_SQL_ACTIVE;
				str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_where);
			}
			else
			{
				str_activityCode = SHSM_Utility.fsgetActivityCode(str_appCode,str_programTypeCode,str_optionCode,str_entityId);
			}

			if (str_activityCode ==null)
			{
				throw new ProcessException("Security Module: Error in getting activity code.");
			}
			return str_activityCode;
		}		

	}
}

