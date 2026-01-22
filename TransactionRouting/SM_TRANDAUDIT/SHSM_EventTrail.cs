using System;

namespace shsm
{
	/// <summary> <p>Title: Security Manager</p>
	/// <p>Description: </p>
	/// <p>Copyright: Copyright (c) 2004</p>
	/// <p>Company: Sidat Hyder Morshed Associates (Pvt) Ltd.</p>
	/// </summary>
	/// <author>  Muhammad Asif
	/// </author>
	/// <version>  1.0
	/// <p>Creation Date:  24-10-2005. </p>
	/// </version>

	using SQLException = System.Data.SqlClient.SqlException;
	using DB = SHMA.Enterprise.Data.DB;
	using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
	using EnvHelper=SHMA.Enterprise.Shared.EnvHelper;
	using NameValueCollection = SHMA.Enterprise.NameValueCollection;
	using shsm.util;
	using SHSM_SecurityFeatures=shsm.security.SHSM_SecurityFeatures;
	using SHMA.Enterprise.Data;

	public class SHSM_EventTrail
	{

		//Point No 102 - Oracle Support - Start
		private static  ParameterCollection ParaColl = new ParameterCollection();

		public SHSM_EventTrail()
		{
		}

		public static void fssaveSuboptionEventData(String str_appCode, String str_programTypeCode, String str_optionCode, String str_suboptionCode)
		{
			//--New Feature - Parametric Security 2 - start
			if (!(SHSM_SecurityFeatures.EVENT_TRAIL))
			{
				return;
			}
			//--New Feature - Parametric Security 2 - end
			
			EnvHelper sessionValues = new EnvHelper();
			String str_logEventMenu = (String)sessionValues.getAttribute("s_SUS_LOGEVENTOPTION");
			if (str_logEventMenu.ToUpper() == "Y".ToUpper())
			{

				String str_userCode		= (String)sessionValues.getAttribute("s_SUS_USERCODE");
				String str_networkID	= (String)sessionValues.getAttribute("s_SUS_NETWORKID");
				String str_ipAddress 	= (String) sessionValues.getAttribute("s_SUS_IPADDRESS");
				
				GlobalConstants obj_global = GlobalConstants.getInstance();
				String str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
				//String str_today_sec = DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today); // .fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP_SEC,str_today);
				String str_today_sec = SHSM_DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today);

				String str_query = "WHERE SAA_APPCODE ='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND SAN_SUBOPTCODE='"+str_suboptionCode+"'";
				String str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_query);

				if (str_activityCode ==null)
					throw new ProcessException("Security Module: \\n" + "Error in getting activity code for Event Audit Log");

				///Point No. 80 <<<<< - Nullable non pk
				str_query = " INSERT INTO SH_SM_EA_EVENTAUDITLOG (SUS_USERCODE,SEV_EVENTCODE,SEA_DATETIME,SEA_DATETIMEACTUAL,SAV_ACTCODE,SAA_APPCODE,SPT_PRGTYPECODE,SAO_OPTCODE,SAN_SUBOPTCODE,SEA_NETWORKID,SEA_IPADDRESS)"
					//+ " VALUES('"+str_userCode+"','04','"+str_today+"','"+str_today_sec+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"','"+str_networkID+"','"+str_ipAddress+"')";

					//Point No 102 - Oracle Support - Start
					//+ " VALUES('"+str_userCode+"','04','"+str_today+"','"+str_today_sec+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"'," +(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+ ","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+")";
					+ " VALUES('"+str_userCode+"','04','"+str_today+"',?,'"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"'," +(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+ ","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+")";
					ParaColl.clear();
					ParaColl.puts("@SEA_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_sec), Types.TIMESTAMP);
					//Point No 102 - Oracle Support - End
				try 
				{
					//Point No 102 - Oracle Support - Start
					//DB.executeDML(str_query);
					DB.executeDML(str_query,ParaColl);
					//Point No 102 - Oracle Support - End
				}
				catch (SQLException e) 
				{
					System.Console.Out.WriteLine("Menu Selection: SQL is -----> " + str_query);
					throw new ProcessException("Security Module: \\n" + "Error in generating Menu Event Log" + e.Message);
				}
				catch (Exception e) 
				{
					//throw new ProcessException(e.Message);
					//-- Message changes 2006/01/09
					throw new ProcessException("Security Module: \\n" + "Error: Failed to save suboption event data \n" +e.Message);
				}
			}
		}

		public static void fssaveListerEventData(String str_entityId, String str_tableCode, String[] str_arrKeyFields,  NameValueCollection obj_colFields) 
		{
			//--New Feature - Parametric Security 2 - start
			if (!(SHSM_SecurityFeatures.EVENT_TRAIL))
			{
				return;
			}
			//--New Feature - Parametric Security 2 - end

			EnvHelper sessionValues = new EnvHelper();
			String str_logEventLister	= (String)sessionValues.getAttribute("s_SUS_LOGEVENTLISTER");
			if (str_logEventLister.ToUpper() == "Y".ToUpper())
			{
				String str_appCode			= (String)sessionValues.getAttribute("s_SAA_APPCODE");
				String str_programTypeCode	= (String)sessionValues.getAttribute("s_SPT_PRGTYPECODE");
				String str_optionCode		= (String)sessionValues.getAttribute("s_SAO_OPTCODE");

				String str_userCode			= (String)sessionValues.getAttribute("s_SUS_USERCODE");
				String str_networkID		= (String)sessionValues.getAttribute("s_SUS_NETWORKID");
				String str_ipAddress 		= (String)sessionValues.getAttribute("s_SUS_IPADDRESS");

				
				GlobalConstants obj_global = GlobalConstants.getInstance();
				String str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
				String str_today_sec = DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today);

				String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);

				String str_query = "WHERE SAA_APPCODE ='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND PSE_ENTITYID='"+str_entityId+"'";
				String str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_query);
				String str_suboptionCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAN_SUBOPTCODE", str_query);

				if (str_activityCode ==null)
					throw new ProcessException("Security Module: \\n" + "Error in getting activity code for Event Audit Log");

				int int_recCount = SHSM_Utility.fsgetRecordCount("SH_SM_EA_EVENTAUDITLOG"," WHERE SUS_USERCODE='"+str_userCode+"' AND SEV_EVENTCODE='05' AND SEA_DATETIME='"+str_today+"'");
				if (int_recCount == 0)
				{
					///Point No. 80 <<<<< - Nullable non pk
					str_query = " INSERT INTO SH_SM_EA_EVENTAUDITLOG (SUS_USERCODE,SEV_EVENTCODE,SEA_DATETIME,SEA_DATETIMEACTUAL,SEA_DOCREF,SAV_ACTCODE,SAA_APPCODE,SPT_PRGTYPECODE,SAO_OPTCODE,SAN_SUBOPTCODE,SEA_NETWORKID,SEA_IPADDRESS,SAB_TABLECODE)"
						//+ " VALUES('"+str_userCode+"','05','"+str_today+"','"+str_today_sec+"','"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"','"+str_networkID+"','"+str_ipAddress+"', '" + str_tableCode + "')";

						//Point No 102 - Oracle Support - Start
						//+ " VALUES('"+str_userCode+"','05','"+str_today+"','"+str_today_sec+"','"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'" + str_tableCode + "')";
						+ " VALUES('"+str_userCode+"','05','"+str_today+"',?,'"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'" + str_tableCode + "')";
						ParaColl.clear();
						ParaColl.puts("@SEA_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_sec), Types.TIMESTAMP);
						//Point No 102 - Oracle Support - End
					try 
					{
						//Point No 102 - Oracle Support - Start
						//DB.executeDML(str_query);
						DB.executeDML(str_query,ParaColl);
						//Point No 102 - Oracle Support - End
					}
					catch (SQLException e) 
					{
						System.Console.Out.WriteLine("Menu Selection: SQL is -----> " + str_query);
						throw new ProcessException("Security Module: \\n" + "Error in generating Lister Event Log" + e.Message);
					}
					catch (Exception e) 
					{
						//throw new ProcessException(e.Message);
						//-- Message changes 2006/01/09
						throw new ProcessException("Security Module: \\n" + "Error: Failed to save Lister event data. \n" + e.Message);
					}
				}
			}
		}

		//Point No 83 -------------
		//public static void fssaveButtonEventData(String str_entityId, String str_tableCode, String[] str_arrKeyFields,  NameValueCollection obj_colFields, char chr_operation, String str_now, bool bln_generate ) 
		public static void fssaveButtonEventData(String str_entityId, String str_tableCode, String[] str_arrKeyFields,  NameValueCollection obj_colFields, char chr_operation, String str_now, bool bln_generate, String str_activityCode) 
		{
			//--New Feature - Parametric Security 2 - start
			if (!(SHSM_SecurityFeatures.EVENT_TRAIL))
			{
				return;
			}
			//--New Feature - Parametric Security 2 - end

			EnvHelper sessionValues = new EnvHelper();
			String str_logEventButton	= (String)sessionValues.getAttribute("s_SUS_LOGEVENTBUTTON");
			if (str_logEventButton.ToUpper() == "Y".ToUpper() || bln_generate)
			{
				String str_appCode			= (String)sessionValues.getAttribute("s_SAA_APPCODE");
				String str_programTypeCode	= (String)sessionValues.getAttribute("s_SPT_PRGTYPECODE");
				String str_optionCode		= (String)sessionValues.getAttribute("s_SAO_OPTCODE");

				String str_userCode			= (String)sessionValues.getAttribute("s_SUS_USERCODE");
				String str_networkID		= (String)sessionValues.getAttribute("s_SUS_NETWORKID");
				String str_ipAddress 		= (String) sessionValues.getAttribute("s_SUS_IPADDRESS");


				GlobalConstants obj_global = GlobalConstants.getInstance();
				String str_today=null;
				if (str_now==null)
				{
					str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
				}
				else
				{
					str_today=str_now;
				}

				String str_today_sec = DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today);


				String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_tableCode, str_arrKeyFields, obj_colFields);

				String str_query = "WHERE SAA_APPCODE ='"+str_appCode+"' AND SPT_PRGTYPECODE='"+str_programTypeCode+"' AND SAO_OPTCODE='"+str_optionCode+"' AND PSE_ENTITYID='"+str_entityId+"'";
				//Point No 83 -------------
				//String str_activityCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAV_ACTCODE", str_query);
				String str_suboptionCode = SHSM_Utility.fsgetColumnAgainstQuery("SH_SM_AN_APPSUBOPTION", "SAN_SUBOPTCODE", str_query);

				//Point No 83 ------------- stopped
				//if (str_activityCode ==null)
				//	throw new ProcessException("Security Module: \\n" + "Error in getting activity code for Event Audit Log");

				int int_recCount = SHSM_Utility.fsgetRecordCount("SH_SM_EA_EVENTAUDITLOG"," WHERE SUS_USERCODE='"+str_userCode+"' AND SEV_EVENTCODE='06' AND SEA_DATETIME='"+str_today+"'");
				if (int_recCount == 0)
				{
					///Point No. 80 <<<<< - Nullable non pk
					str_query = " INSERT INTO SH_SM_EA_EVENTAUDITLOG (SUS_USERCODE,SEV_EVENTCODE,SEA_DATETIME,SEA_DATETIMEACTUAL,SEA_DOCREF,SAV_ACTCODE,SAA_APPCODE,SPT_PRGTYPECODE,SAO_OPTCODE,SAN_SUBOPTCODE,SEA_NETWORKID,SEA_IPADDRESS, SAF_BUTTONCODE, SAB_TABLECODE)"
						//+ " VALUES('"+str_userCode+"','06','"+str_today+"','"+str_today_sec+"','"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"','"+str_networkID+"','"+str_ipAddress+"', '" + chr_operation+ "', '" + str_tableCode + "')";
						
					//Point No 102 - Oracle Support - Start
						//+ " VALUES('"+str_userCode+"','06','"+str_today+"','"+str_today_sec+"','"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'" + chr_operation+ "', '" + str_tableCode + "')";
						+ " VALUES('"+str_userCode+"','06','"+str_today+"',?,'"+str_docRef+"','"+str_activityCode+"','"+str_appCode+"','"+str_programTypeCode+"','"+str_optionCode+"','"+str_suboptionCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'" + chr_operation+ "', '" + str_tableCode + "')";

					ParaColl.clear();
					ParaColl.puts("@SEA_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_sec), Types.TIMESTAMP);
					//Point No 102 - Oracle Support - End
					try 
					{
						//Point No 102 - Oracle Support - Start
						//DB.executeDML(str_query);
						DB.executeDML(str_query,ParaColl);
						//Point No 102 - Oracle Support - End
					}
					catch (SQLException e) 
					{
						System.Console.Out.WriteLine("Menu Selection: SQL is -----> " + str_query);
						throw new ProcessException("Security Module: \\n" + "Error in generating Button Event Log" + e.Message);
					}
					catch (Exception e) 
					{
						//throw new ProcessException(e.Message);
						//-- Message changes 2006/01/09
						throw new ProcessException("Security Module: \\n" + "Error: Failed to save Button event data. \n" +e.Message);
					}
				}
			}
		}

		public static void fssaveMenuEventData(String str_appCode, String str_levelCode)
		{
			//--New Feature - Parametric Security 2 - start
			if (!(SHSM_SecurityFeatures.EVENT_TRAIL))
			{
				return;
			}
			//--New Feature - Parametric Security 2 - end
			
			EnvHelper sessionValues = new EnvHelper();
			String str_logEventMenu = (String)sessionValues.getAttribute("s_SUS_LOGEVENTMENU");
			if (str_logEventMenu.ToUpper() == "Y".ToUpper())
			{
				String str_userCode		= (String)sessionValues.getAttribute("s_SUS_USERCODE");
				String str_networkID	= (String)sessionValues.getAttribute("s_SUS_NETWORKID");
				String str_ipAddress 	= (String)sessionValues.getAttribute("s_SUS_IPADDRESS");
				String str_menuLabel    = SHSM_ApplicationProfile.fsgetMenuDesc(str_appCode, str_levelCode);

				GlobalConstants obj_global = GlobalConstants.getInstance();
				String str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
				//String str_today_sec = DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today); // .fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP_SEC,str_today);
				String str_today_sec = SHSM_DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today);

				///Point No. 80 <<<<< - Nullable non pk
				String str_query = " INSERT INTO SH_SM_EA_EVENTAUDITLOG (SUS_USERCODE,SEV_EVENTCODE,SEA_DATETIME,SEA_DATETIMEACTUAL,SAA_APPCODE,SEA_NETWORKID,SEA_IPADDRESS,SEA_MENULABEL)"
					//+ " VALUES('"+str_userCode+"','03','"+str_today+"','"+str_today_sec+"','"+str_appCode+"','"+str_networkID+"','"+str_ipAddress+"','"+ str_menuLabel +"')";
					
				//Point No 102 - Oracle Support - Start	
					//+ " VALUES('"+str_userCode+"','03','"+str_today+"','"+str_today_sec+"','"+str_appCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'"+ str_menuLabel +"')";
					+ " VALUES('"+str_userCode+"','03','"+str_today+"',?,'"+str_appCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'"+ str_menuLabel +"')";
				ParaColl.clear();
				ParaColl.puts("@SEA_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_sec), Types.TIMESTAMP);
				//Point No 102 - Oracle Support - End

				try 
				{
					//Point No 102 - Oracle Support - Start
					//DB.executeDML(str_query);
					DB.executeDML(str_query,ParaColl);
					//Point No 102 - Oracle Support - End
				}
				catch (SQLException e) 
				{
					System.Console.Out.WriteLine("Menu Selection: SQL is -----> " + str_query);
					throw new ProcessException("Security Module: \\n" + "Error in generating Menu Event Log" + e.Message);
				}
				catch (Exception e) 
				{
					//throw new ProcessException(e.Message);
					//-- Message changes 2006/01/09
					throw new ProcessException("Security Module: \\n" + "Error: Failed to save Menu event data. \n" +e.Message);
				}
			}
		}//End of --> fssaveMenuEventData

		//------ Menu Option switching 20060405 - start 
		public static void fssaveMenuEventData(String str_menuSequence)
		{
			//--New Feature - Parametric Security 2 - start
			if (!(SHSM_SecurityFeatures.EVENT_TRAIL))
			{
				return;
			}
			//--New Feature - Parametric Security 2 - end
			
			String str_appCode = str_menuSequence.Substring(0,2);
			EnvHelper sessionValues = new EnvHelper();
			String str_logEventMenu = (String)sessionValues.getAttribute("s_SUS_LOGEVENTMENU");
			if (str_logEventMenu.ToUpper() == "Y".ToUpper())
			{
				String str_userCode		= (String)sessionValues.getAttribute("s_SUS_USERCODE");
				String str_networkID	= (String)sessionValues.getAttribute("s_SUS_NETWORKID");
				String str_ipAddress 	= (String)sessionValues.getAttribute("s_SUS_IPADDRESS");
				String str_menuLabel    = SHSM_ApplicationProfile.fsgetMenuDesc(str_menuSequence);

				GlobalConstants obj_global = GlobalConstants.getInstance();
				String str_today = DateTimeManager.fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP);
				//String str_today_sec = DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today); // .fsgetFormattedDate(obj_global.FORMAT_TIMESTAMP_SEC,str_today);
				String str_today_sec = SHSM_DateTimeManager.fsgetDateStringFromString(obj_global.FORMAT_TIMESTAMP_SEC,str_today);

				///Point No. 80 <<<<< - Nullable non pk
				String str_query = " INSERT INTO SH_SM_EA_EVENTAUDITLOG (SUS_USERCODE,SEV_EVENTCODE,SEA_DATETIME,SEA_DATETIMEACTUAL,SAA_APPCODE,SEA_NETWORKID,SEA_IPADDRESS,SEA_MENULABEL)"
					//+ " VALUES('"+str_userCode+"','03','"+str_today+"','"+str_today_sec+"','"+str_appCode+"','"+str_networkID+"','"+str_ipAddress+"','"+ str_menuLabel +"')";

				//Point No 102 - Oracle Support - Start
					//+ " VALUES('"+str_userCode+"','03','"+str_today+"','"+str_today_sec+"','"+str_appCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'"+ str_menuLabel +"')";
					+ " VALUES('"+str_userCode+"','03','"+str_today+"',?,'"+str_appCode+"',"+(str_networkID==null ? "NULL" : "'"+str_networkID+"'")+","+(str_ipAddress==null ? "NULL" : "'"+str_ipAddress+"'")+",'"+ str_menuLabel +"')";
				ParaColl.clear();
				ParaColl.puts("@SEA_DATETIMEACTUAL", shgn.SHGNDateUtil.GetDateAsSqlFormat(str_today_sec), Types.TIMESTAMP);
				//Point No 102 - Oracle Support - End

				try 
				{
					//Point No 102 - Oracle Support - Start
					//DB.executeDML(str_query);
					DB.executeDML(str_query,ParaColl);
					//Point No 102 - Oracle Support - End
				}
				catch (SQLException e) 
				{
					System.Console.Out.WriteLine("Menu Selection: SQL is -----> " + str_query);
					throw new ProcessException("Security Module: \\n" + "Error in generating Menu Event Log" + e.Message);
				}
				catch (Exception e) 
				{
					//throw new ProcessException(e.Message);
					//-- Message changes 2006/01/09
					throw new ProcessException("Security Module: \\n" + "Error: Failed to save Menu event data. \n" +e.Message);
				}
			}
		}//End of --> fssaveMenuEventData
	}
}