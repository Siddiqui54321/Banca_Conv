namespace shsm 
{

	/// <author>   Kashif Iqbal Khan
	/// </author>

	
	using System;
	using NameValueCollection = SHMA.Enterprise.NameValueCollection;
	using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
	using DB = SHMA.Enterprise.Data.DB;
	using rowset = SHMA.Enterprise.Data.rowset;
	using EnvHelper = SHMA.Enterprise.Shared.EnvHelper;
	using StringUtility = shsm.util.StringUtility;
	using SHSM_SecurityFeatures=shsm.security.SHSM_SecurityFeatures;	
	using SHMA.Enterprise.Data;

	public class SHSM_SecurityPermission 
	{
		virtual public bool AddNewAllowed 
		{
			get 
			{
				return this.bln_allowAddNew;
			}
			
		}
		virtual public bool SaveAllowed 
		{
			get 
			{
				return this.bln_allowSave;
			}
			
		}
		virtual public bool UpdateAllowed 
		{
			get 
			{
				return this.bln_allowUpdate;
			}
			
		}
		virtual public bool DeleteAllowed 
		{
			get 
			{
				return this.bln_allowDelete;
			}
			
		}
		virtual public bool VerifyAllowed 
		{
			get 
			{
				return this.bln_allowVerify;
			}
			
		}
		virtual public bool RejectAllowed 
		{
			get 
			{
				return this.bln_allowReject;
			}
			
		}
		virtual public bool ProcessAllowed(string processName)
		{
			if (processName.Equals("shsm.SHSM_VerifyTransaction"))
				return this.bln_allowVerify ;
			if (processName.Equals("shsm.SHSM_RejectTransaction"))
				return this.bln_allowReject;
			return true;
		}




		virtual internal bool AllowAddNew 
		{
			set 
			{
				this.bln_allowAddNew = value;
			}
			
		}
		virtual internal bool AllowSave 
		{
			set 
			{
				this.bln_allowSave = value;
			}
			
		}
		virtual internal bool AllowUpdate 
		{
			set 
			{
				this.bln_allowUpdate = value;
			}
			
		}
		virtual internal bool AllowDelete 
		{
			set 
			{
				this.bln_allowDelete = value;
			}
			
		}
		virtual internal bool AllowVerify 
		{
			set 
			{
				this.bln_allowVerify = value;
			}
			
		}
		virtual internal bool AllowRejection 
		{
			set 
			{
				this.bln_allowReject = value;
			}
			
		}
		virtual internal bool AllowProcess 
		{
			set 
			{
				this.bln_allowProcess = value;
			}
			
		}
		virtual internal bool AllNormal 
		{
			set 
			{
				AllowAddNew = value;
				AllowSave = value;
				AllowUpdate = value;
				AllowDelete = value;
				AllowProcess = value;
			}
			
		}
		
		
		private bool bln_allowAddNew = false;
		private bool bln_allowSave  = false;
		private bool bln_allowUpdate= false;
		private bool bln_allowDelete= false;
		private bool bln_allowVerify= false;
		private bool bln_allowReject= false;
		private bool bln_allowProcess= false;

		

		private System.String mstr_entityId;
		private System.String[] mstr_arrKeyFields=null;
		private System.String mstr_tableCode;
		private NameValueCollection obj_allFields;
		
		
		
		public SHSM_SecurityPermission(System.String str_entityId, System.String[] str_arrKeyFields, NameValueCollection obj_colFields, System.String str_tableCode) 
		{			
			//			System.String str_where = " WHERE PTC_ENTITYCHILD='" + str_entityId + "'" 
			//									+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
			
			this.mstr_arrKeyFields = str_arrKeyFields;
			this.mstr_entityId = str_entityId;
			this.mstr_tableCode = str_tableCode;
			this.obj_allFields = obj_colFields;

			System.Console.WriteLine(mstr_arrKeyFields +":"+ mstr_entityId +":"+ mstr_tableCode +":"+ obj_allFields);


			EnvHelper sessionValues = new EnvHelper();
			System.String str_appCode = (System.String) sessionValues.getAttribute("s_SAA_APPCODE");
			System.String str_programTypeCode = (System.String) sessionValues.getAttribute("s_SPT_PRGTYPECODE");
			System.String str_optionCode = (System.String) sessionValues.getAttribute("s_SAO_OPTCODE");
			System.String str_userCode = (System.String) sessionValues.getAttribute("s_SUS_USERCODE");
			
			/*---- MC0038 ---------------------START*/
			SHSM_EventTrail.fssaveListerEventData( str_entityId, str_tableCode, str_arrKeyFields, obj_colFields);
			/*---- MC0038 ---------------------END*/

			//--New Feature - Parametric Security 3 - start
			if (!(SHSM_SecurityFeatures.TRANSACTION_ROUTING))
			{
				this.bln_allowAddNew = true;
				this.bln_allowSave   = true;
				this.bln_allowUpdate = true;
				this.bln_allowDelete = true;
				this.bln_allowVerify = false;
				this.bln_allowReject = false;
				return;
			}
			//--New Feature - Parametric Security 3 - end
			
			String str_entitiesSql = "SELECT PSE_ENTITYID FROM SH_SM_AN_APPSUBOPTION "
				+ " WHERE saa_appcode='" + str_appCode + "' "
				+ " AND spt_prgtypeCode='" + str_programTypeCode + "' "
				+ " AND sao_optcode='" + str_optionCode + "' "
				+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE ;

			
			System.String str_where = " WHERE PSE_ENTITYID IN (" + str_entitiesSql + ") " 
				+ " AND PTC_ENTITYCHILD='" + str_entityId  + "' ";
			//+ " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;


			System.String str_parentEntity = SHSM_Utility.fsgetColumnAgainstQuery( "SH_SM_TC_TRANSROUTECHILD", "PSE_ENTITYID", str_where);
			
			
			bool bln_isParentEntity = (str_parentEntity == null || str_parentEntity.Length == 0);

			
			if (bln_isParentEntity) 
			{
				str_where = " WHERE PSE_ENTITYID='" + str_entityId + "'";
			}
			else 
			{
				str_where = " WHERE PSE_ENTITYID='" + str_parentEntity + "'";
			}
			
			//		str_where += " AND " + SHSM_Utility.STATUS_SQL_ACTIVE;
			
			// This entity or its parent are not involved in Routing
			if (!SHSM_Utility.fsrecordExists("SH_SM_TR_TRANSROUTEENTITY", str_where)) 
			{
				this.AllNormal = true;
				return ;
			}
			
			//bln_isParentEntity = true;
			Object[] obj_entityData = null;

			obj_entityData = SHSM_Utility.fsgetRecordAgainstQuery( "SH_SM_TR_TRANSROUTEENTITY",
				new String[] {"SAB_TABLECODE", "STR_UPDATEAFTERROUTE", "STR_DELETEAFTERROUTE"},
				str_where);
			
			String str_parentTable = (String)obj_entityData[0];
			bool bln_editableAfterRoute   = "Y".Equals(obj_entityData[1]) ;
			bool bln_deleteableAfterRoute = "Y".Equals(obj_entityData[2]) ;
			
			if (str_parentTable == null || str_parentTable.Length == 0)
				throw new ProcessException("Security Module: \\n" + "Table of parent entity is not defined");
			
			
			// At this point this entity or its parent is involved in Routing


			// Change on 07-March-2005 To support Number generation (Number is generated after this call )
			for (int i = 0; i < str_arrKeyFields.Length; i++)
			{
				
				System.Object obj_keyValue = obj_colFields.getObject(str_arrKeyFields [i]);
				
				if (obj_keyValue == null) 
				{
					// This is an unusual condition
					this.bln_allowAddNew=true;
					this.bln_allowSave=true;
					return;
				}
			}
			// -------------------------------------------------------------------------------------------


			System.String str_docRef = SHSM_Utility.fsgetDocumentReference(str_appCode, str_parentTable, str_arrKeyFields, obj_colFields);
			
			//T00005 --- OK 1
			str_where	= " WHERE " 
				+ " SAA_APPCODE = '" + str_appCode + "' " 
				+ " AND SAB_TABLECODE = '" + str_parentTable + "' " 
				+ " AND SIN_DOCREF='" + StringUtility.fsreplace(str_docRef,"'","''") + "' " 
				+ " AND sin_status='U'"
				+ " AND SIN_TYPE !='R'"
				+ " AND exists(SELECT 'V' FROM SH_SM_RG_ROUTINGGROUPVIEW RG WHERE RG.saa_appcode = SH_SM_IN_INBOX.sin_appcode and RG.sug_groupcode = SH_SM_IN_INBOX.sug_groupcode AND RG.SUS_USERCODE = '" + str_userCode + "')" ;

			bool bln_isInProcessDocument = SHSM_Utility.fsrecordExists("SH_SM_IN_INBOX", str_where);
			bool bln_isInboxDocument = bln_isInProcessDocument;
			String str_allowEdit = "N";
			bool bln_isApproved = false;

			if (!bln_isInboxDocument)
			{
				/*------- Routing View Mode - start -------*/
				String str_viewMode = (System.String) sessionValues.getAttribute("s_VIEWMODE");
				if(str_viewMode=="1")
				{
					this.bln_allowAddNew = false;
					this.bln_allowSave   = false;
					this.bln_allowUpdate = false;
					this.bln_allowDelete = false;
					this.bln_allowVerify = false;
					this.bln_allowReject = false;
					return;
				}
				/*------- Routing View Mode - end -------*/

				if (bln_isInProcessDocument) 
				{
					this.bln_allowUpdate = false;
					this.bln_allowDelete = false;
					this.bln_allowVerify = false;
					this.bln_allowReject = false;
				
					if (!bln_isParentEntity) 
					{
						this.bln_allowAddNew = false;
						this.bln_allowSave = false;
					}
					return;
				}


/////////////////////////
				String str_routingField = (String)SHSM_Utility.fsgetColumnValueAgainstQuery( "SH_SM_AB_APPTABLE", "SAB_TRSTATUSFIELD", " WHERE SAA_APPCODE='"+ str_appCode + "' AND SAB_TABLECODE='" + str_parentTable +"'" );
				bln_isApproved = false;
				if (str_routingField!=null)
				{
					ParameterCollection pCol = new ParameterCollection();

					String str_whereKey=SHSM_TransactionRouter.fsbuildWhereClauseForBusinessEntity(str_arrKeyFields);
					
					//String str_tableWhere = SHSM_Utility.fsgetExceutableQuery(str_whereKey, str_arrKeyFields, obj_colFields);
					//String str_routingStatus = (String)SHSM_Utility.fsgetColumnValueAgainstQuery( str_parentTable, str_routingField, str_tableWhere);

					String str_tableWhere = SHSM_Utility.fsgetExceutableQuery(str_whereKey, str_arrKeyFields, obj_colFields,ref pCol);
					String str_routingStatus = (String)SHSM_Utility.fsgetColumnValueAgainstQuery( str_parentTable, str_routingField, str_tableWhere, pCol);
					
					if (str_routingStatus!=null && str_routingStatus==SHSM_TransactionRouter.LEVEL_APPROVED)
					{
						bln_isApproved = true;
					}
				}
////////////////////////
			}


			if (bln_isParentEntity) 
			{
				if (bln_isApproved)
				{
					/////////////////////////
					String[] str_arrColumns = new String[] {"SAB_LASTROUTINGDATEFIELD", "SAB_DATEMODIFIEDFIELD"};
					Object[] str_arrValues = SHSM_Utility.fsgetRecordAgainstQuery("SH_SM_AB_APPTABLE", str_arrColumns, " WHERE SAA_APPCODE='"+ str_appCode + "' AND SAB_TABLECODE='" + str_parentTable +"'" );
					bool bln_isUpdated = false;
					if (str_arrValues!=null)
					{
						String[] str_arrFields = new String[2];
						str_arrFields[0] = str_arrValues[0].ToString();
						str_arrFields[1] = str_arrValues[1].ToString();
						
						ParameterCollection pCol = new ParameterCollection();

						String str_whereKey=SHSM_TransactionRouter.fsbuildWhereClauseForBusinessEntity(str_arrKeyFields);
						//String str_tableWhere = SHSM_Utility.fsgetExceutableQuery(str_whereKey, str_arrKeyFields, obj_colFields);
						//Object[] str_fieldValues = SHSM_Utility.fsgetRecordAgainstQuery(str_parentTable, str_arrFields, str_tableWhere );
						
						String str_tableWhere = SHSM_Utility.fsgetExceutableQuery(str_whereKey, str_arrKeyFields, obj_colFields,ref pCol);
						Object[] str_fieldValues = SHSM_Utility.fsgetRecordAgainstQuery(str_parentTable, str_arrFields, str_tableWhere,pCol );
						
						if (str_fieldValues!=null)

						{
							DateTime dt_lastRoutingDate = DateTime.Now;
							DateTime dt_modifiedDate = DateTime.Now;
							if (str_fieldValues[0]!=null)
							{
								//dt_lastRoutingDate = SHSM_DateTimeManager.fsgetDateFromString(SHSM_UserOperation.FORMAT_UI_TIMESTAMP, str_fieldValues[0].ToString());
								dt_lastRoutingDate = System.DateTime.Parse(str_fieldValues[0].ToString()) ;
							}
							if (str_fieldValues[1]!=null)
							{
								//dt_modifiedDate = SHSM_DateTimeManager.fsgetDateFromString(SHSM_UserOperation.FORMAT_TIMESTAMP_SEC,str_fieldValues[1].ToString());
								dt_modifiedDate = System.DateTime.Parse(str_fieldValues[1].ToString()) ;
							}
							if (dt_modifiedDate > dt_lastRoutingDate)
							{
								bln_isUpdated = true;
							}
						}
					}
					////////////////////////

					
					this.bln_allowVerify = bln_editableAfterRoute && bln_isUpdated;

					if (bln_isInboxDocument)
						this.bln_allowReject = bln_editableAfterRoute;
				}
				else 
				{
					this.bln_allowVerify = true;

					if (bln_isInboxDocument)
						this.bln_allowReject = true;
				}
			}


			bool bln_isReadOnly = (bln_isInboxDocument && str_allowEdit.ToUpper().Equals("N")) || (!bln_isInboxDocument && bln_isApproved)  ;


			this.AllNormal = !bln_isReadOnly;
			
			if (bln_isParentEntity) 
			{
				if (bln_isApproved) 
				{
					this.bln_allowUpdate = bln_editableAfterRoute;
					this.bln_allowDelete = bln_deleteableAfterRoute;
				}
			}

		}
	}
}