using System;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;

namespace ace
{
	/// <summary>
	/// Summary description for clsIlasValidateOnAccept.
	/// </summary>
	public class clsIlasValidateOnAccept
	{
		public clsIlasValidateOnAccept()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region "Validation Keywords (Constants)"
		//**************** ILAS FORMUL RELATED RESERVED WORDS ************
		private const string FORM_IF     = "<IF>";
		private const string FORM_THEN   = "<THEN>";
		private const string FORM_ELSEIF = "<ELSEIF>";
		private const string FORM_ELSE   = "<ELSE>";
		private const string FORM_ENDIF  = "<ENDIF>";
		//**************** SQL RELATED RESERVED WORDS ************
		private const string _CASEWHEN = " CASE WHEN ";
		private const string _THEN     = " THEN ";
		private const string _WHEN     = " WHEN ";
		private const string _ELSE     = " ELSE ";
		private const string _END      = " END ";

		/************** More Constant ******************/
		private const string CHARACTER = "C";
		private const string NUMERIC   = "N";
		private const string DATE      = "D";

		private const string DELIMITER        = "D";
		private const string LOGICAL_OPERATOR = "O";
		private const string VALIDATION_FIELD = "V";
		private const string VALIDATION_QUERY = "Q";

		#endregion
		/*private const string AND = "AND";
		private const string OR = "OR";
		private const string BETWEEN  = "BETWEEN";
		private const string IN       = "IN";
		private const string EQUALS   = "=";
		private const string NOTEQUAL = "<>";
		private const string GREATER  = ">";
		private const string LESSTHAN = "<";*/

		private const string UNKNOWNVALUE = "'-999'";

		string proposal   = "";
	//	string productCode = "";
		int setNo = 0;
		string product  = "";
		private VFC vfCollection;
		private FieldRegister vfRegister;
		
		public clsIlasValidateOnAccept(string Proposal)
		{
			this.proposal   = Proposal;
	//		this.productCode = productCode;
			this.vfCollection = new VFC();
		}

		#region "Process  Calling"
		public string getProposalInformation()
		{
			LoadCollection();
			return startProcess();
		}

		private string startProcess()
		{
			string product = vfCollection.getString("PPR_PRODCD");
			string vField  = clsIlasConstant.VALIDATEOTHER;
			int vfLevel = 0;
			try
			{
				//************** Field combination **************//
				string fieldComb = "";
				string validationType = "C";
				rowset rsVF = DB.executeQuery("SELECT PVF_TYPE,PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD='" + product + "' AND PVF_CODE='" + vField + "'");
				if(rsVF.next())
				{
					if(rsVF.getObject("PVF_FIELDCOMB") != null)
					{
						fieldComb = rsVF.getString("PVF_FIELDCOMB").Trim();
					}
					validationType = rsVF.getString("PVF_TYPE").ToUpper().Trim();
				}
				else
				{
					//********** Search Report setting for Global Product - 999 ***************//
					product = "999";
					rsVF = DB.executeQuery("SELECT PVF_TYPE,PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD='" + product + "' AND PVF_CODE='" + vField + "'");
					if(rsVF.next())
					{
						if(rsVF.getObject("PVF_FIELDCOMB") != null)
						{
							fieldComb = rsVF.getString("PVF_FIELDCOMB").Trim();
						}
						validationType = rsVF.getString("PVF_TYPE").ToUpper().Trim();
					}
					
				}


				/******************************** Get Master records For Report *******************************/
				//Current Product
				string strQuery = "SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' " + getQueryForValueCombination(fieldComb);
				rowset rsReport = DB.executeQuery(strQuery);
				if (rsReport.size() < 1)
				{   
					product = clsIlasConstant.GLOBAL_PRODUCT;
					strQuery = "SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' " + getQueryForValueCombination(fieldComb);
					//strQuery = "SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + clsIlasConstant.GLOBAL_PRODUCT + "' AND PVL_VALIDATIONFOR='" + vField + "' " + getQueryForValueCombination(fieldComb);
					rsReport = DB.executeQuery(strQuery);
//					if (rsReport.size() < 1)
//					{
//						throw new Exception("REPORT setup not defined.");
//					}
				}


				while(rsReport.next())
				{
					//This will be the final query 
					string query = "SELECT 'A' FROM DUAL ";

					//Get Master record for condition now
					vfLevel = rsReport.getInt("PVL_LEVEL");
					rowset rsConditions = DB.executeQuery("SELECT * FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' AND PVL_LEVEL=" + vfLevel + " ORDER BY PVD_SEQUENCE ");
					if (rsConditions.size() > 0)
					{
						query += " WHERE ";
					}

					//Getting each condition and make it part of the query
					while(rsConditions.next())
					{
						string fieldNature  = rsConditions.getString("PVD_FIELDNATURE").ToUpper().Trim();
						string field        = rsConditions.getString("PVD_VALIDATIONFOR").ToUpper().Trim();
						string rider        = "";
						string dataType     = rsConditions.getObject("PVD_DATATYPE")==null    ? "" : rsConditions.getString("PVD_DATATYPE").ToUpper().Trim();
						string relOpertor   = rsConditions.getObject("PVD_RELOPERATOR")==null ? "" : rsConditions.getString("PVD_RELOPERATOR").ToUpper().Trim();

						//****** Pick Rider code if it is embaded with Validation Field ***********
						if (field.IndexOf("-") > 1)
						{
							string[] fieldArray = field.Split(new char[] { '-' });
							field = fieldArray[0];
							rider = fieldArray[1];
						}

						if(fieldNature == LOGICAL_OPERATOR || fieldNature == DELIMITER)
						{
							query += " "  + field + " ";
						}
						else
						{
							string columnName = "";
							string inputValue = "";

							//Get Input value
							try
							{
								columnName = this.vfRegister.getFieldName(field);
								//Rider Code is embaded with Validation Field (e.g. BTERM-701)
								if(rider.Trim().Length > 0)
								{
									columnName = columnName + rider;
								}
								
								//This check is used for Questions Code (Used as Validation fields)
								if (columnName == "") 
								{
									columnName = field;
								}
							}
							catch(Exception e)
							{	
								//field might be a question which can be find directly in collection by its code (as this is not a db field)
								columnName = field;
							}

							inputValue = vfCollection.getString(columnName);
							if (inputValue.Trim() == "")
							{
								//If input value not found the assign an unusual value to it
								//????? Need to confirm this 
								inputValue = UNKNOWNVALUE;
							}

							if(inputValue == UNKNOWNVALUE)
							{
								query += inputValue + "<>" +  UNKNOWNVALUE;
							}
							else
							{
								//Data Type 
								if(dataType == CHARACTER || dataType == DATE)
								{
									inputValue = " '" + inputValue + "' ";
									inputValue = inputValue.Replace(" ''"," '");
									inputValue = inputValue.Replace("'' ","' ");
								}
								inputValue = " " + inputValue + " ";
						
								//Relational Operator
								if (relOpertor == "BETWEEN" || relOpertor == "")
								{
									string fromValue = " " + Evaluate(rsConditions.getString("PVD_VALIDFROM").ToUpper().Trim(),dataType) + " ";
									string toValue   = " " + Evaluate(rsConditions.getString("PVD_VALIDTO").ToUpper().Trim(),dataType)   + " ";
									query += inputValue + relOpertor +  fromValue + " AND " + toValue ;
								}
								else 
								{
									string fromValue  =   " " + Evaluate(rsConditions.getString("PVD_VALIDFROM").ToUpper().Trim(),dataType) + " ";
									if (relOpertor == "IN")
									{
										fromValue = "(" + fromValue + ")";
										fromValue = fromValue.Replace("((","(");
										fromValue = fromValue.Replace("))",")");
										fromValue = " " + fromValue + " ";
									}
									query += inputValue + relOpertor + fromValue;
								}
							}
						}//End of --> if(fieldNature == LOGICAL_OPERATOR || fieldNature == DELIMITER)
					}//end of --> while(rsConditions.next())
					
					rowset rs ;
					try
					{
						rs = DB.executeQuery(query);
					}
					catch(Exception e)
					{
						throw new Exception("Setup is not properly defined.");
					}
					
					if(rs.next())
					{
						string reportName = rsReport.getString("PVL_VALIDRANGE");
						//Pick more paremters(other than Proposal) from this field PVL_VALIDFROM
						string reportParameters = "";
						if(rsReport.getObject("PVL_VALIDFROM") != null)
						{
							if(rsReport.getString("PVL_VALIDFROM").Trim().Length > 0)
							{
								reportParameters = getExecutableRptParams(rsReport.getString("PVL_VALIDFROM"));
							}
						}
						return reportName + "~" + reportParameters;
					}
				}//End of while(rsDecision.next())
				string errorInValidation= getAccumulatedError(product,vfLevel);
				if(errorInValidation.Length>10)
					throw new Exception( errorInValidation);
				else
					return "";
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		private string getExecutableRptParams(string parameters)
		{
			try
			{
				//********** Validate Paramter setting ********************//
				string[] paramsArray = parameters.Split(new char[] { ';' });
				for (int i = 0; i <= paramsArray.Length - 1; i++)
				{
					if (paramsArray[i].Trim().Length > 0)
					{
						string[] paramsAttributes = paramsArray[i].Split(new char[] { ',' });

						if (paramsAttributes.Length != 3)
						{
							throw new Exception("Report Parameter(s) not properly defined.");
						}
					}
				}
		
				//********** Get Executable Paramter now ********************//
				parameters = getExecuteableExpression(parameters, this.vfCollection);
				return parameters;
			}
			catch (Exception e)
			{
				throw new Exception("Report Parameter is not properly defined.");
			}
		}


		public static string getExecuteableExpression(string _expression, VFC objVFC)
		{
			do 
			{
				int index = _expression.IndexOf(":[");
				if (index < 0) 
				{
					break; // TODO: might not be correct. Was : Exit Do
				}
				else 
				{
					string field  = "";
					string fvalue = "";
					
					try
					{
						field = _expression.Substring(index + 2, _expression.IndexOf("]:") - (index + 2));
					}
					catch(Exception e)
					{
						throw new Exception("Expression not properly defined. Database column must be like >> :[columnName]:  ");
					}

					try
					{
						fvalue =  objVFC.getString(field);
					}
					catch(Exception e)
					{
						throw new Exception("Error in getting field(" + field  + ") value.");
					}
					
					_expression = _expression.Replace(":[" + field + "]:", fvalue);
				}
			}while (true);

			//Finallay return the process value
			return _expression;
		}

		private string getQueryForValueCombination(string fieldComb)
		{
			string valueComb = getValueComb(fieldComb);
			if (valueComb=="") 
			{
				return "";
			}
			else 
			{
				return " AND PVL_VALUECOMB ='" + valueComb + "'";
           
			}
		}
		private string getValueComb(string fieldComb)
		{
			if (fieldComb != "") 
			{
				string valueComb = "";
				string[] fieldArray = fieldComb.Split(new char[] {','});
				for (int i=0; i<=fieldArray.Length - 1; i++) 
				{
					valueComb = valueComb + this.vfCollection.getString(fieldArray[i]) + ",";
				}
				valueComb = valueComb.Remove(valueComb.LastIndexOf(","), 1);
           
				return valueComb;
			}
			else 
			{
				return fieldComb;
			}
		}

		private object Evaluate(string expression, string dataType)
		{
			string result = Convert.ToString(Validation.evaluateExpressionByQuery(Validation.getExecuteableExpression(expression,this.vfCollection), dataType));
			if(dataType != "N")
			{
				result = "'" + result + "'";
			}
			return result;
		}
		#endregion

		#region "Add fields and their values in Collection Object"
		private void LoadCollection()
		{
			setValidationFieldsForPlan();
			setMoreFieldsInCollection();
		}
		private void setMoreFieldsInCollection()
		{
			try
			{
			//	vfCollection.setValue("PPR_PRODCD", this.productCode);

				EnvHelper env = new EnvHelper();
				this.vfRegister = new FieldRegister();
				rowset rsFields = DB.executeQuery("SELECT * FROM LVFS_FIELDSETUP WHERE VFS_SOURCE <> 'N'");
				while(rsFields.next())
				{
					string vfID    = rsFields.getString("VFS_CODE");
					string vfDesc  = rsFields.getString("VFS_DESC");
					string dbID    = rsFields.getString("VFS_ID");
					string vfType  = rsFields.getString("VFS_DATATYPE");
					string Source  = rsFields.getString("VFS_SOURCE");
					string Query   = rsFields.getString("VFS_QUERY");
					
					//Set all fields in Field Register
					vfRegister.setDescription(vfID,vfDesc);
					vfRegister.setFieldName(vfID,dbID);
					vfRegister.setDataType(vfID,vfType);
					
					//Set only Query(Q)/Session(S) based columns in vfCollection
					if(Source == "Q")
					{
						try
						{
							//Get Expression and then set Session values in Session place holder (i.e SV("col"))
							rowset rsRes = DB.executeQuery(EnvHelper.Parse(Validation.getExecuteableExpression(Query, vfCollection)));
							if(rsRes.next())
							{
								if(vfType == "N")
								{
									vfCollection.setValue(dbID, rsRes.getDouble(dbID));
								}
								else if(vfType == "D")
								{
									vfCollection.setValue(dbID, rsRes.getDate(dbID));
								}
								else
								{
									vfCollection.setValue(dbID, rsRes.getString(dbID));
								}
							}
						}
						catch(Exception e)
						{	
						}
					}
					else if(Source == "S")
					{
						if(vfType == "N")
						{
							vfCollection.setValue(dbID, Convert.ToDouble(env.getAttribute(dbID)));
						}
						else if(vfType == "D")
						{
							vfCollection.setValue(dbID, Convert.ToString(env.getAttribute(dbID)));
						}
						else
						{
							vfCollection.setValue(dbID, Convert.ToString(env.getAttribute(dbID)));
						}
					}
				}
			}
			catch(Exception e)
			{
			}
		}

		private void setValidationFieldsForPlan()
		{
			rowset rs = DB.executeQuery("SELECT * FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='" + this.proposal + "' AND NPR_BASICFLAG='Y'");
			if (rs.next())
			{
				this.setNo =  rs.getInt("NP2_SETNO");
				this.product = rs.getString("PPR_PRODCD");

				/******* Base Product Columns ********/
				vfCollection.setValue("NP1_PROPOSAL", proposal);
				//Base Product
				vfCollection.setValue("PPR_PRODCDB", rs.getString("PPR_PRODCD"));
				//Current Product (might be Rider or Base Product itself)
				vfCollection.setValue("PPR_PRODCD", rs.getString("PPR_PRODCD"));
				vfCollection.setValue("NPR_BENEFITTERM", rs.getDouble("NPR_BENEFITTERM"));
				vfCollection.setValue("NPR_PREMIUMTER", (rs.getDouble("NPR_PREMIUMTER")==0 ? rs.getDouble("NPR_BENEFITTERM") : rs.getDouble("NPR_PREMIUMTER")));
				vfCollection.setValue("NPR_SUMASSURED", rs.getDouble("NPR_SUMASSURED"));
				vfCollection.setValue("NPR_PREMIUM", rs.getDouble("NPR_PREMIUM"));
				vfCollection.setValue("NPR_LOADING", rs.getObject("NPR_LOADING") == null ? 0.0 : rs.getDouble("NPR_LOADING"));
				vfCollection.setValue("NPR_TOTPREM", rs.getObject("NPR_TOTPREM") == null ? 0.0 : rs.getDouble("NPR_TOTPREM"));
				vfCollection.setValue("NPR_PREMRATE", rs.getObject("NPR_PREMRATE") == null ? 0.0 : rs.getDouble("NPR_PREMRATE"));
				//vfCollection.setValue("NPR_EXTMORTRATE", rs.getObject("NPR_EXTMORTRATE") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE"));
				//vfCollection.setValue("NPR_ENDORSMT", rs.getString("NPR_ENDORSMT"));
				vfCollection.setValue("NPR_LIFE", rs.getString("NPR_LIFE"));
				//vfCollection.setValue("NPR_SAR", rs.getObject("NPR_SAR") == null ? 0.0 : rs.getDouble("NPR_SAR"));
				vfCollection.setValue("NPR_ETASA", rs.getObject("NPR_ETASA") == null ? 0.0 : rs.getDouble("NPR_ETASA"));
				vfCollection.setValue("NPR_PAIDUPSA", rs.getObject("NPR_PAIDUPSA") == null ? 0.0 : rs.getDouble("NPR_PAIDUPSA"));
				vfCollection.setValue("NPR_ETATERM", rs.getObject("NPR_ETATERM") == null ? 0.0 : rs.getDouble("NPR_ETATERM"));
				//vfCollection.setValue("NPR_PURENDOWMENT", rs.getObject("NPR_PURENDOWMENT") == null ? 0.0 : rs.getDouble("NPR_PURENDOWMENT"));
				vfCollection.setValue("NPR_INDEXATION", rs.getString("NPR_INDEXATION"));
				vfCollection.setValue("NPR_INDEXRATE", rs.getDouble("NPR_INDEXRATE"));
				//vfCollection.setValue("NPR_ETAPREMIUM", rs.getObject("NPR_ETAPREMIUM") == null ? 0.0 : rs.getDouble("NPR_ETAPREMIUM"));
				//vfCollection.setValue("NPR_CASHVALUE", rs.getObject("NPR_CASHVALUE") == null ? 0.0 : rs.getDouble("NPR_CASHVALUE"));
				vfCollection.setValue("NPR_FACTOR", rs.getObject("NPR_FACTOR") == null ? 0.0 : rs.getDouble("NPR_FACTOR"));
				vfCollection.setValue("NPR_ETADAYS", rs.getObject("NPR_ETADAYS") == null ? 0.0 : rs.getDouble("NPR_ETADAYS"));
				vfCollection.setValue("NPR_EDUNITS", rs.getDouble("NPR_EDUNITS"));
				vfCollection.setValue("NPR_AGEPREM", rs.getObject("NPR_AGEPREM") == null ? 0.0 : rs.getInt("NPR_AGEPREM"));
				vfCollection.setValue("NPR_AGEPREM2ND", rs.getObject("NPR_AGEPREM2ND") == null ? 0.0 : rs.getInt("NPR_AGEPREM2ND"));
				//CONVERT
				//NPR_COMMLOADING
				//NPR_INCLUDELOADINNIV
				//NPR_MATURITYDATE
				//vfCollection.setValue("NPR_AGEDIFFERENCE", rs.getObject("NPR_AGEDIFFERENCE") == null ? 0.0 : rs.getDouble("NPR_AGEDIFFERENCE"));
				//vfCollection.setValue("CCB_CODE", rs.getString("CCB_CODE"));
				////NPR_PAID
				//vfCollection.setValue("NPR_INTERESTRATE", rs.getObject("NPR_INTERESTRATE") == null ? 0.0 : rs.getDouble("NPR_INTERESTRATE"));
				//vfCollection.setValue("NPR_EXTMORTRATE2", rs.getObject("NPR_EXTMORTRATE2") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE2"));
				//vfCollection.setValue("NP2_AGEPREM", rs.getObject("NP2_AGEPREM") == null ? 0.0 : rs.getInt("NP2_AGEPREM"));
				//vfCollection.setValue("NPR_PAIDUPTOAGE", rs.getObject("NPR_PAIDUPTOAGE") == null ? 0.0 : rs.getInt("NPR_PAIDUPTOAGE"));
				//vfCollection.setValue("CMO_MODE", rs.getString("CMO_MODE"));
				//vfCollection.setValue("NPR_BASICPRMANNUAL", rs.getObject("NPR_BASICPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_BASICPRMANNUAL"));
				//vfCollection.setValue("NPR_EXCESPRMANNUAL", rs.getObject("NPR_EXCESPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESPRMANNUAL"));
				////NPR_SELECTED
				//vfCollection.setValue("NPR_AGEPREM2", rs.getObject("NPR_AGEPREM2") == null ? 0.0 : rs.getInt("NPR_AGEPREM2"));
				//vfCollection.setValue("PCU_CURRCODE", rs.getString("PCU_CURRCODE"));
				//vfCollection.setValue("NPR_EXCESSPREMIUM", rs.getObject("NPR_EXCESSPREMIUM") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM"));
				//vfCollection.setValue("NPR_EXCESSPREMIUM_ACTUAL", rs.getObject("NPR_EXCESSPREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM_ACTUAL"));
				//vfCollection.setValue("NPR_PREMIUM_FC", rs.getObject("NPR_PREMIUM_FC") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_FC"));
				//vfCollection.setValue("NPR_PREMIUM_AV", rs.getObject("NPR_PREMIUM_AV") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_AV"));
				//vfCollection.setValue("NPR_PREMIUM_ACTUAL", rs.getObject("NPR_PREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_ACTUAL"));
				//vfCollection.setValue("NPR_PREMIUMDISCOUNT", rs.getObject("NPR_PREMIUMDISCOUNT") == null ? 0.0 : rs.getDouble("NPR_PREMIUMDISCOUNT"));
			}
			else
			{
				throw new Exception("Plan not defined.");
			}
		}
		#endregion


		private string getAccumulatedError(string product, int serialNo)
		{
			string dataType = "";//this.vfRegister.getDataType(vField);
			string query = "";//PVD_VALIDATIONFOR 
			//query = " SELECT 0 SEQ,       '"+dataType+"' DATATYPE, NVL(VFS_DESC,PVL_VALIDATIONFOR) VFIELD, 'V'             FNATURE, 'BETWEEN'       OPERATOR, PVL_VALIDFROM VFROM, PVL_VALIDTO VTO FROM LPVL_VALIDATION A LEFT OUTER JOIN LVFS_FIELDSETUP B ON A.PVL_VALIDATIONFOR=B.VFS_CODE       WHERE PPR_PRODCD='"+product+"' AND PVL_VALIDATIONFOR='"+vField+"' AND PVL_LEVEL="+serialNo
			//	+ " UNION "
			string vField ="";
			query = " SELECT PVD_SEQUENCE SEQ,PVD_DATATYPE DATATYPE, NVL(VFS_DESC,PVD_VALIDATIONFOR) VFIELD, PVD_FIELDNATURE FNATURE, PVD_RELOPERATOR OPERATOR, PVD_VALIDFROM VFROM, PVD_VALIDTO VTO, PVD_DATATYPE FROM LPVD_VALIDATIONDETAIL A LEFT OUTER JOIN LVFS_FIELDSETUP B ON A.PVD_VALIDATIONFOR=B.VFS_CODE WHERE PPR_PRODCD='"+product+"' AND PVL_LEVEL = " +  serialNo 
				+ " ORDER BY SEQ ";

			rowset rsValidationFields = DB.executeQuery(query);
			
			string errorReport = "";
			while(rsValidationFields.next())
			{
				//errorReport += rsValidationFields.getString("ERROR_MESSAGE");
				vField = rsValidationFields.getString("PVD_DATATYPE")==null?"":rsValidationFields.getString("PVD_DATATYPE");
				dataType = this.vfRegister.getDataType(vField);
				string vfNature   = rsValidationFields.getString("FNATURE");
				string vfName    = rsValidationFields.getString("VFIELD");
				string vfOperator = "";
				string vfDataType  = "";
				string htmlFormatStart = "<B>";//<BR><B><U><I>";
				string htmlFormatEnd   = "</B>";//</I></U></B>";
				string processedError ="";
				if(vfNature == "V")
				{
					vfOperator = " " + rsValidationFields.getString("OPERATOR") + " ";
					vfDataType = rsValidationFields.getString("DATATYPE");
					string rangeFrom = Convert.ToString(Evaluate(rsValidationFields.getString("VFROM"), vfDataType));
					//					rowset rsRangeFrom = DB.executeQuery("Select "+ rangeFrom +" FVal FROM DUAL");
					//					if(rsRangeFrom.next())
					//					{
					//						rangeFrom = rangeFrom.getString("FVal");
					//					}

					if(dataType == NUMERIC) rangeFrom = double.Parse(rangeFrom).ToString("#,###,###,###,##0");


					if(vfOperator.Trim() == "BETWEEN")
					{
						string rangeTo = Convert.ToString(Evaluate(rsValidationFields.getString("VTO"), vfDataType));
						if(dataType == NUMERIC) rangeTo = double.Parse(rangeTo).ToString("#,###,###,###,##0");
						//processedError = " " + htmlFormatStart + vfName + htmlFormatEnd + " Range is From " + rangeFrom + " To " + rangeTo + " ";
						if(rangeFrom == rangeTo)
						{
							processedError = " " + htmlFormatStart + vfName + htmlFormatEnd + " should be " + rangeFrom + " ";
						}
						else
						{
							processedError = " " + htmlFormatStart + vfName + htmlFormatEnd + " should be in between " + rangeFrom + " to " + rangeTo + " ";
						}
					}
					else
					{
						if(vfOperator.Trim().Equals(">")) 
							vfOperator = " Greater then ";
						else if(vfOperator.Trim().Equals("<") )
							vfOperator = " Less then ";
						else if(vfOperator.Trim().Equals(">=") )
							vfOperator = " Greater then or Equal to ";
						else if(vfOperator.Trim().Equals("<=") )
							vfOperator = " Less then or Equal to ";

						processedError = " "  + vfName + " Should be " +  vfOperator + rangeFrom + " ";
					}
				}
				else
				{
					processedError =   " " + vfName + " ";
				}
				errorReport += processedError;
			}
			return serialNo + ". " + errorReport;
		}
	
	}
}
