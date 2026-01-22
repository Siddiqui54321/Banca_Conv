using System;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using DB = SHMA.Enterprise.Data.DB;
using rowset = SHMA.Enterprise.Data.rowset;
//using NameValueCollection = SHMA.Enterprise.NameValueCollection;
//using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;
//using SHMA.Enterprise.Data;
//using shsm.util;

namespace shsm
{
	/// <summary>
	/// Summary description for SHSM_AllowEntity.
	/// </summary>
	public class  SHSM_AllowEntity
	{
		public SHSM_AllowEntity()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//Created Date	:	28 Feb 2011
		//Purpose		:	To check the allow entity of the user
		public static bool fsallowEntity(string userID, string entityID, string strSAA_APPCODE,string strSPT_PRGTYPECODE,string strSAO_OPTCODE )
		{
			rowset rs_field = null;
			String str_sql = "";

			try
			{
				/*				str_sql = " SELECT DISTINCT UO.SUS_USERCODE,AN.PSE_ENTITYID FROM SH_SM_UO_USERAUTOPTION UO "
									+ "INNER JOIN SH_SM_AF_APPBUTTON AF ON AF.SAA_APPCODE=UO.SAA_APPCODE AND AF.SPT_PRGTYPECODE=UO.SPT_PRGTYPECODE AND "
									+ " AF.SAO_OPTCODE=UO.SAO_OPTCODE AND AF.SAN_SUBOPTCODE=UO.SAN_SUBOPTCODE AND AF.SAF_BUTTONCODE=UO.SAF_BUTTONCODE "
									+ " INNER JOIN sh_sm_an_appsuboption AN ON AN.SAA_APPCODE=AF.SAA_APPCODE AND AN.SPT_PRGTYPECODE=AF.SPT_PRGTYPECODE AND "
									+ " AN.SAO_OPTCODE=AF.SAO_OPTCODE AND AN.SAN_SUBOPTCODE=AF.SAN_SUBOPTCODE "
									+ " INNER JOIN SH_SM_AO_APPOPTION AO ON AO.SAA_APPCODE=AN.SAA_APPCODE AND AO.SPT_PRGTYPECODE=AN.SPT_PRGTYPECODE AND "
									+ " AO.SAO_OPTCODE=AN.SAO_OPTCODE INNER JOIN sh_sm_am_appmenu AM ON AM.SAA_APPCODE=AN.SAA_APPCODE AND "
									+ " AM.SPT_PRGTYPECODE=AN.SPT_PRGTYPECODE AND AM.SAO_OPTCODE=AN.SAO_OPTCODE AND AM.SAN_SUBOPTCODE=AN.SAN_SUBOPTCODE "
									+ " WHERE UO.SUS_USERCODE='" + userID + "' AND AN.PSE_ENTITYID='"+ entityID +"'";

				*/				
				if(entityID.IndexOf("Not a Valid Entity Name") < 0)
				{
					str_sql = " SELECT DISTINCT UO.SUS_USERCODE,AN.PSE_ENTITYID FROM SH_SM_AN_APPSUBOPTION AN "
						+ " INNER JOIN SH_SM_UO_USERAUTOPTION UO "
						+ " ON AN.SAA_APPCODE = UO.SAA_APPCODE "
						+ " AND AN.SPT_PRGTYPECODE = UO.SPT_PRGTYPECODE "
						+ " AND AN.SAO_OPTCODE = UO.SAO_OPTCODE AND UO.SAA_APPCODE ='" + strSAA_APPCODE + "' AND UO.SPT_PRGTYPECODE = '" + strSPT_PRGTYPECODE + "' AND UO.SAO_OPTCODE = '" + strSAO_OPTCODE + "' "
						//+ "AND AN.SAN_SUBOPTCODE = UO.SAN_SUBOPTCODE "
						+ " WHERE UO.SUS_USERCODE='" + userID + "' AND AN.PSE_ENTITYID='"+ entityID +"'";

					
					rs_field = DB.executeQuery(str_sql);
				
					if (rs_field.next())
						return true;				
					else
						return false;
				}
				else
				{
						return true;				
				}
			}
			catch (Exception e)
			{				
				throw new ProcessException(e.Message);
			}

		}

	}
}
