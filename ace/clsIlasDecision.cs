using System;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Shared;

namespace ace
{
	/// <summary>
	/// Summary description for clsIlasDecision.
	/// </summary>
	public class clsIlasDecision
	{
		private const string CHARACTER = "C";
		private const string NUMERIC   = "N";
		private const string DATE      = "D";

		private const string DELIMITER        = "D";
		private const string VALIDATION_FIELD = "V";
		private const string LOGICAL_OPERATOR = "O";

		/*private const string AND = "AND";
		private const string OR = "OR";
		private const string BETWEEN  = "BETWEEN";
		private const string IN       = "IN";
		private const string EQUALS   = "=";
		private const string NOTEQUAL = "<>";
		private const string GREATER  = ">";
		private const string LESSTHAN = "<";*/

		private const string UNKNOWNVALUE = "'-999'";

		string proposal = "";
		int setNo = 0;
		string product  = "";
		private VFC vfCollection;
		private FieldRegister vfRegister;
		
		public clsIlasDecision(string Proposal)
		{
			this.proposal = Proposal;
			this.vfCollection = new VFC();
		}

		#region "Decision Related functions"
		public string getDecision()
		{
			LoadCollection();
			return startProcess();
		}
		public string getDecision(ref String ReasonOfSubStandard)
		{
			LoadCollection();
			return startProcess(ref ReasonOfSubStandard);
		}
		private string startProcess()
		{
			string product = vfCollection.getString("PPR_PRODCD");
			string vField  = clsIlasConstant.DECISION;
			int vfLevel = 0;
			try
			{
				//???????? Field combination ?????????????
				string fieldComb = "";
				string validationType = "C";
				string queryVF = "SELECT PVF_TYPE,PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD='" + product + "' AND PVF_CODE='" + vField + "'";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				rowset rsVF = DB.executeQuery(queryVF);
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
					throw new Exception("DECISION is not defined in Product Field Setup (LPVF_VALIDFIELDS)");
				}


				/******************************** Get Master records For Decision *******************************/
				//rowset rsDecision = DB.executeQuery("SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "'");
				rowset rsDecision = DB.executeQuery("SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "'" + getQueryForValueCombination(fieldComb));
				if (rsDecision.size() < 0)
				{
					throw new Exception("DECISION setup not defined.");
				}

				//This will be the final query 
				string query = "SELECT 'A' FROM DUAL ";

				while(rsDecision.next())
				{
					//Get Master record for condition now
					vfLevel = rsDecision.getInt("PVL_LEVEL");
					rowset rsConditions = DB.executeQuery("SELECT * FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' AND PVL_LEVEL=" + vfLevel + " ORDER BY PVD_SEQUENCE ");
					if (rsConditions.size() < 0)
					{
						throw new Exception("Decision conditions not being set.");
					}
					else
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
								//columnName = "";//???? clsIlasValidation.getSourcColName(field);
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
								//throw new Exception("Unknown field [" + field + "]");
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
					//	throw new Exception(query);
					}
					catch(Exception e)
					{
						throw new Exception("Setup is not properly defined.");
					}
					if(rs.next())
					{
						return rsDecision.getString("PVL_VALIDRANGE");
					}
					//else
					//{
					//	throw new Exception("Range validation error.");
					//}
				}//End of while(rsDecision.next())

				return "";
			}
			catch(Exception e)
			{
				throw new Exception("Error when making decision: " + e.Message);
			}

		}

		private string startProcess(ref String ReasonOfSubStandard)
		{
			string product = vfCollection.getString("PPR_PRODCD");
			string vField  = clsIlasConstant.DECISION;
			int vfLevel = 0;
			try
			{
				//???????? Field combination ?????????????
				string fieldComb = "";
				string validationType = "C";
				string queryVF = "SELECT PVF_TYPE,PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD='" + product + "' AND PVF_CODE='" + vField + "'";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				rowset rsVF = DB.executeQuery(queryVF);
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
					throw new Exception("DECISION is not defined in Product Field Setup (LPVF_VALIDFIELDS)");
				}


				/******************************** Get Master records For Decision *******************************/
				//rowset rsDecision = DB.executeQuery("SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "'");
				rowset rsDecision = DB.executeQuery("SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "'" + getQueryForValueCombination(fieldComb));
				if (rsDecision.size() < 0)
				{
					throw new Exception("DECISION setup not defined.");
				}

				//This will be the final query 
				string query = "SELECT 'A' FROM DUAL ";
				string reasonQuery = "SELECT '' "; //FROM DUAL
				const string CaseForReason =" || CASE WHEN [C] THEN ',' ELSE '[V],' END ";
				while(rsDecision.next())
				{
					//Get Master record for condition now
					vfLevel = rsDecision.getInt("PVL_LEVEL");
					rowset rsConditions = DB.executeQuery("SELECT * FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' AND PVL_LEVEL=" + vfLevel + " ORDER BY PVD_SEQUENCE ");
					if (rsConditions.size() < 0)
					{
						throw new Exception("Decision conditions not being set.");
					}
					else
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
						
						string reasonCode  = rsConditions.getString("PVD_REASON");

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
								//columnName = "";//???? clsIlasValidation.getSourcColName(field);
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
								//throw new Exception("Unknown field [" + field + "]");
								//If input value not found the assign an unusual value to it
								//????? Need to confirm this 
								inputValue = UNKNOWNVALUE;
							}

							if(inputValue == UNKNOWNVALUE)
							{
								string cond = inputValue + "<>" +  UNKNOWNVALUE;
								query += cond;

							//	reasonQuery += CaseForReason.Replace("[C]",cond).Replace("[V]",reasonCode);
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
									string cond=inputValue + relOpertor +  fromValue + " AND " + toValue ;
									query += cond;

									reasonQuery += CaseForReason.Replace("[C]",cond).Replace("[V]",reasonCode);

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
									string cond=inputValue + relOpertor + fromValue;
									query += cond;

									reasonQuery += CaseForReason.Replace("[C]",cond).Replace("[V]",reasonCode);
								}
							}
						}//End of --> if(fieldNature == LOGICAL_OPERATOR || fieldNature == DELIMITER)
					}//end of --> while(rsConditions.next())

					reasonQuery += " CSVCodes FROM DUAL ";
					
					rowset rs ;
					try
					{
						rs = DB.executeQuery(reasonQuery);
					}
					catch(Exception e)
					{
						throw new Exception("Resons are not properly defined.");
					}

					if(rs.next())
					{
						ReasonOfSubStandard=rs.getString("CSVCodes");
						rs.close();
					}

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
						return rsDecision.getString("PVL_VALIDRANGE");
					}

					//else
					//{
					//	throw new Exception("Range validation error.");
					//}
				}//End of while(rsDecision.next())

				return "";
			}
			catch(Exception e)
			{
				throw new Exception("Error when making decision: " + e.Message);
			}

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
			setValidationFieldsForRider();
			setMoreFieldsInCollection();
			//setBeneficiaryInCollection();
			setQuestionsInCollection();
			setQuestionsDtlInCollection();
            setAssessmentHealthQuestionInCollection();

        }

		
		#region "Load more Fields (Query/Session)"
		private void setMoreFieldsInCollection()
		{
			try
			{
				/*
				SELECT PVL_VALIDRANGE    VFFIELD FROM LPVL_VALIDATION       WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION'                                                                                                                                                              UNION //Validation Call
				SELECT PVL_VALIDFROM     VFFIELD FROM LPVL_VALIDATION       WHERE PPR_PRODCD='901' AND PVL_VALIDFROM LIKE '%:[%'                         AND PVL_VALIDATIONFOR IN(select DISTINCT PVL_VALIDRANGE FROM LPVL_VALIDATION T WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION') UNION //From Range
				SELECT PVL_VALIDTO       VFFIELD FROM LPVL_VALIDATION       WHERE PPR_PRODCD='901' AND PVL_VALIDTO   LIKE '%:[%'                         AND PVL_VALIDATIONFOR IN(select DISTINCT PVL_VALIDRANGE FROM LPVL_VALIDATION T WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION') UNION //To Range
				SELECT PVD_VALIDATIONFOR VFFIELD FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='901' AND PVD_FIELDNATURE='V'                               AND PVL_VALIDATIONFOR IN(select DISTINCT PVL_VALIDRANGE FROM LPVL_VALIDATION T WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION') UNION //Detail Fields
				SELECT PVD_VALIDFROM     VFFIELD FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='901' AND PVD_FIELDNATURE='V' AND PVD_VALIDFROM LIKE '%:[%' AND PVL_VALIDATIONFOR IN(select DISTINCT PVL_VALIDRANGE FROM LPVL_VALIDATION T WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION') UNION //Detail Range From
				SELECT PVD_VALIDTO       VFFIELD FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='901' AND PVD_FIELDNATURE='V' AND PVD_VALIDTO LIKE '%:[%'   AND PVL_VALIDATIONFOR IN(select DISTINCT PVL_VALIDRANGE FROM LPVL_VALIDATION T WHERE PPR_PRODCD='901' AND PVL_VALIDATIONFOR='CALLVALIDATION')       //Detail Range To
				*/

				EnvHelper env = new EnvHelper();
				this.vfRegister = new FieldRegister();
				rowset rsFields = DB.executeQuery("SELECT * FROM LVFS_FIELDSETUP");
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
								{	//vfCollection.setValue(dbID, rsRes.getDouble(dbID));
									vfCollection.setValue(dbID, rsRes.getDouble(1));
								}
								else if(vfType == "D")
								{
									vfCollection.setValue(dbID, rsRes.getDate(1));
								}
								else
								{
									vfCollection.setValue(dbID, rsRes.getString(1));
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
							//vfCollection.setValue(vfID, Convert.ToDateTime(env.getAttribute(vfID)));
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
		#endregion
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
				vfCollection.setValue("NPR_EXTMORTRATE", rs.getObject("NPR_EXTMORTRATE") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE"));
				vfCollection.setValue("NPR_ENDORSMT", rs.getString("NPR_ENDORSMT"));
				vfCollection.setValue("NPR_LIFE", rs.getString("NPR_LIFE"));
				vfCollection.setValue("NPR_SAR", rs.getObject("NPR_SAR") == null ? 0.0 : rs.getDouble("NPR_SAR"));
				vfCollection.setValue("NPR_ETASA", rs.getObject("NPR_ETASA") == null ? 0.0 : rs.getDouble("NPR_ETASA"));
				vfCollection.setValue("NPR_PAIDUPSA", rs.getObject("NPR_PAIDUPSA") == null ? 0.0 : rs.getDouble("NPR_PAIDUPSA"));
				vfCollection.setValue("NPR_ETATERM", rs.getObject("NPR_ETATERM") == null ? 0.0 : rs.getDouble("NPR_ETATERM"));
				vfCollection.setValue("NPR_PURENDOWMENT", rs.getObject("NPR_PURENDOWMENT") == null ? 0.0 : rs.getDouble("NPR_PURENDOWMENT"));
				vfCollection.setValue("NPR_INDEXATION", rs.getString("NPR_INDEXATION"));
				vfCollection.setValue("NPR_INDEXRATE", rs.getDouble("NPR_INDEXRATE"));
				vfCollection.setValue("NPR_ETAPREMIUM", rs.getObject("NPR_ETAPREMIUM") == null ? 0.0 : rs.getDouble("NPR_ETAPREMIUM"));
				vfCollection.setValue("NPR_CASHVALUE", rs.getObject("NPR_CASHVALUE") == null ? 0.0 : rs.getDouble("NPR_CASHVALUE"));
				vfCollection.setValue("NPR_FACTOR", rs.getObject("NPR_FACTOR") == null ? 0.0 : rs.getDouble("NPR_FACTOR"));
				vfCollection.setValue("NPR_ETADAYS", rs.getObject("NPR_ETADAYS") == null ? 0.0 : rs.getDouble("NPR_ETADAYS"));
				vfCollection.setValue("NPR_EDUNITS", rs.getDouble("NPR_EDUNITS"));
				vfCollection.setValue("NPR_AGEPREM", rs.getObject("NPR_AGEPREM") == null ? 0.0 : rs.getInt("NPR_AGEPREM"));
				vfCollection.setValue("NPR_AGEPREM2ND", rs.getObject("NPR_AGEPREM2ND") == null ? 0.0 : rs.getInt("NPR_AGEPREM2ND"));
				//CONVERT
				//NPR_COMMLOADING
				//NPR_INCLUDELOADINNIV
				//NPR_MATURITYDATE
				vfCollection.setValue("NPR_AGEDIFFERENCE", rs.getObject("NPR_AGEDIFFERENCE") == null ? 0.0 : rs.getDouble("NPR_AGEDIFFERENCE"));
				vfCollection.setValue("CCB_CODE", rs.getString("CCB_CODE"));
				//NPR_PAID
				vfCollection.setValue("NPR_INTERESTRATE", rs.getObject("NPR_INTERESTRATE") == null ? 0.0 : rs.getDouble("NPR_INTERESTRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE2", rs.getObject("NPR_EXTMORTRATE2") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE2"));
				vfCollection.setValue("NP2_AGEPREM", rs.getObject("NP2_AGEPREM") == null ? 0.0 : rs.getInt("NP2_AGEPREM"));
				vfCollection.setValue("NPR_PAIDUPTOAGE", rs.getObject("NPR_PAIDUPTOAGE") == null ? 0.0 : rs.getInt("NPR_PAIDUPTOAGE"));
				vfCollection.setValue("CMO_MODE", rs.getString("CMO_MODE"));
				vfCollection.setValue("NPR_BASICPRMANNUAL", rs.getObject("NPR_BASICPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_BASICPRMANNUAL"));
				vfCollection.setValue("NPR_EXCESPRMANNUAL", rs.getObject("NPR_EXCESPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESPRMANNUAL"));
				//NPR_SELECTED
				vfCollection.setValue("NPR_AGEPREM2", rs.getObject("NPR_AGEPREM2") == null ? 0.0 : rs.getInt("NPR_AGEPREM2"));
				vfCollection.setValue("PCU_CURRCODE", rs.getString("PCU_CURRCODE"));
				vfCollection.setValue("NPR_EXCESSPREMIUM", rs.getObject("NPR_EXCESSPREMIUM") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM"));
				vfCollection.setValue("NPR_EXCESSPREMIUM_ACTUAL", rs.getObject("NPR_EXCESSPREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUM_FC", rs.getObject("NPR_PREMIUM_FC") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_FC"));
				vfCollection.setValue("NPR_PREMIUM_AV", rs.getObject("NPR_PREMIUM_AV") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_AV"));
				vfCollection.setValue("NPR_PREMIUM_ACTUAL", rs.getObject("NPR_PREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_PREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUMDISCOUNT", rs.getObject("NPR_PREMIUMDISCOUNT") == null ? 0.0 : rs.getDouble("NPR_PREMIUMDISCOUNT"));
                vfCollection.setValue("CCH_CODE", SessionObject.Get("s_cch_code"));
                vfCollection.setValue("CCD_CODE", SessionObject.Get("s_ccd_code"));
                vfCollection.setValue("CCS_CODE", SessionObject.Get("s_ccs_code"));
            }
			else
			{
				throw new Exception("Record not found for Product");
			}
		}

		private void setValidationFieldsForRider()
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal);
			rowset rs = DB.executeQuery("SELECT * FROM LNPR_PRODUCT LNPR WHERE NP1_PROPOSAL=? AND NVL(NPR_BASICFLAG,'Y')='N'", pc);
			while(rs.next())
			{
				string rider = rs.getString("PPR_PRODCD"); 
				//vfCollection.setRiderSumAssured(rs.getString("PPR_PRODCD"),  rs.getDouble("NPR_SUMASSURED"));
				//vfCollection.setRiderTotalPremium(rs.getString("PPR_PRODCD"), rs.getDouble("NPR_TOTPREM"));
				//vfCollection.setRiderBenefitTerm(rs.getString("PPR_PRODCD"),  rs.getInt("NPR_BENEFITTERM"));
				//vfCollection.setRiderFrequency(rs.getString("PPR_PRODCD"),    rs.getDouble("NPR_ETASA"));
				
				vfCollection.setValue("PPR_PRODCD"+rider,     rs.getString("PPR_PRODCD"));
				vfCollection.setValue("NPR_BENEFITTERM"+rider,rs.getDouble("NPR_BENEFITTERM"));
				vfCollection.setValue("NPR_PREMIUMTER"+rider, rs.getDouble("NPR_PREMIUMTER"));
				vfCollection.setValue("NPR_SUMASSURED"+rider, rs.getDouble("NPR_SUMASSURED"));
				vfCollection.setValue("NPR_PREMIUM"+rider,    rs.getDouble("NPR_PREMIUM"));
				vfCollection.setValue("NPR_LOADING"+rider,    rs.getObject("NPR_LOADING")     == null ? 0.0 : rs.getDouble("NPR_LOADING"));
				vfCollection.setValue("NPR_TOTPREM"+rider,    rs.getObject("NPR_TOTPREM")     == null ? 0.0 : rs.getDouble("NPR_TOTPREM"));
				vfCollection.setValue("NPR_PREMRATE"+rider,   rs.getObject("NPR_PREMRATE")    == null ? 0.0 : rs.getDouble("NPR_PREMRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE"+rider,rs.getObject("NPR_EXTMORTRATE") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE"));
				vfCollection.setValue("NPR_ENDORSMT"+rider,   rs.getString("NPR_ENDORSMT"));
				vfCollection.setValue("NPR_LIFE"+rider,       rs.getString("NPR_LIFE"));
				vfCollection.setValue("NPR_SAR"+rider,        rs.getObject("NPR_SAR")           == null ? 0.0 : rs.getDouble("NPR_SAR"));
				vfCollection.setValue("NPR_ETASA"+rider,      rs.getObject("NPR_ETASA")         == null ? 0.0 : rs.getDouble("NPR_ETASA"));
				vfCollection.setValue("NPR_PAIDUPSA"+rider,   rs.getObject("NPR_PAIDUPSA")      == null ? 0.0 : rs.getDouble("NPR_PAIDUPSA"));
				vfCollection.setValue("NPR_ETATERM"+rider,    rs.getObject("NPR_ETATERM")       == null ? 0.0 : rs.getDouble("NPR_ETATERM"));
				vfCollection.setValue("NPR_PURENDOWMENT"+rider,rs.getObject("NPR_PURENDOWMENT") == null ? 0.0 : rs.getDouble("NPR_PURENDOWMENT"));
				vfCollection.setValue("NPR_INDEXATION"+rider, rs.getString("NPR_INDEXATION"));
				vfCollection.setValue("NPR_INDEXRATE"+rider,  rs.getDouble("NPR_INDEXRATE"));
				vfCollection.setValue("NPR_ETAPREMIUM"+rider, rs.getObject("NPR_ETAPREMIUM") == null ? 0.0 : rs.getDouble("NPR_ETAPREMIUM"));
				vfCollection.setValue("NPR_CASHVALUE"+rider,  rs.getObject("NPR_CASHVALUE")  == null ? 0.0 : rs.getDouble("NPR_CASHVALUE"));
				vfCollection.setValue("NPR_FACTOR"+rider,     rs.getObject("NPR_FACTOR")     == null ? 0.0 : rs.getDouble("NPR_FACTOR"));
				vfCollection.setValue("NPR_ETADAYS"+rider,    rs.getObject("NPR_ETADAYS")    == null ? 0.0 : rs.getDouble("NPR_ETADAYS"));
				vfCollection.setValue("NPR_EDUNITS"+rider,    rs.getDouble("NPR_EDUNITS"));
				vfCollection.setValue("NPR_AGEPREM"+rider,    rs.getObject("NPR_AGEPREM")    == null ? 0.0 : rs.getInt("NPR_AGEPREM"));
				vfCollection.setValue("NPR_AGEPREM2ND"+rider, rs.getObject("NPR_AGEPREM2ND") == null ? 0.0 : rs.getInt("NPR_AGEPREM2ND"));
				//CONVERT
				//NPR_COMMLOADING
				//NPR_INCLUDELOADINNIV
				//NPR_MATURITYDATE
				vfCollection.setValue("NPR_AGEDIFFERENCE"+rider, rs.getObject("NPR_AGEDIFFERENCE") == null ? 0.0 : rs.getDouble("NPR_AGEDIFFERENCE"));
				vfCollection.setValue("CCB_CODE"+rider,          rs.getString("CCB_CODE"));
				//NPR_PAID
				vfCollection.setValue("NPR_INTERESTRATE"+rider,  rs.getObject("NPR_INTERESTRATE") == null ? 0.0 : rs.getDouble("NPR_INTERESTRATE"));
				vfCollection.setValue("NPR_EXTMORTRATE2"+rider,  rs.getObject("NPR_EXTMORTRATE2") == null ? 0.0 : rs.getDouble("NPR_EXTMORTRATE2"));
				vfCollection.setValue("NP2_AGEPREM"+rider,       rs.getObject("NP2_AGEPREM")      == null ? 0.0 : rs.getInt("NP2_AGEPREM"));
				vfCollection.setValue("NPR_PAIDUPTOAGE"+rider,   rs.getObject("NPR_PAIDUPTOAGE")  == null ? 0.0 : rs.getInt("NPR_PAIDUPTOAGE"));
				vfCollection.setValue("CMO_MODE"+rider,          rs.getString("CMO_MODE"));
				vfCollection.setValue("NPR_BASICPRMANNUAL"+rider,rs.getObject("NPR_BASICPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_BASICPRMANNUAL"));
				vfCollection.setValue("NPR_EXCESPRMANNUAL"+rider,rs.getObject("NPR_EXCESPRMANNUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESPRMANNUAL"));
				//NPR_SELECTED
				vfCollection.setValue("NPR_AGEPREM2"+rider,      rs.getObject("NPR_AGEPREM2") == null ? 0.0 : rs.getInt("NPR_AGEPREM2"));
				vfCollection.setValue("PCU_CURRCODE"+rider,      rs.getString("PCU_CURRCODE"));
				vfCollection.setValue("NPR_EXCESSPREMIUM"+rider, rs.getObject("NPR_EXCESSPREMIUM") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM"));
				vfCollection.setValue("NPR_EXCESSPREMIUM_ACTUAL"+rider, rs.getObject("NPR_EXCESSPREMIUM_ACTUAL") == null ? 0.0 : rs.getDouble("NPR_EXCESSPREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUM_FC"+rider,    rs.getObject("NPR_PREMIUM_FC")       == null ? 0.0 : rs.getDouble("NPR_PREMIUM_FC"));
				vfCollection.setValue("NPR_PREMIUM_AV"+rider,    rs.getObject("NPR_PREMIUM_AV")       == null ? 0.0 : rs.getDouble("NPR_PREMIUM_AV"));
				vfCollection.setValue("NPR_PREMIUM_ACTUAL"+rider,rs.getObject("NPR_PREMIUM_ACTUAL")   == null ? 0.0 : rs.getDouble("NPR_PREMIUM_ACTUAL"));
				vfCollection.setValue("NPR_PREMIUMDISCOUNT"+rider,rs.getObject("NPR_PREMIUMDISCOUNT") == null ? 0.0 : rs.getDouble("NPR_PREMIUMDISCOUNT"));
			}
		}

		//private void setBeneficiaryInCollection()
		//{
		//	int benRelation = -1;
		//	SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
		//	pc.puts("@NP1_PROPOSAL",this.proposal);
		//	rowset rs = DB.executeQuery("select MAX(TO_NUMBER(CRL_RELEATIOCD)) CRL_RELEATIOCD from LNBF_BENEFICIARY WHERE NP1_PROPOSAL=? ", pc);
		//	if(rs.next())
		//	{
		//		benRelation = rs.getInt("CRL_RELEATIOCD");
		//	}
		//	vfCollection.setValue("BENRELATION", benRelation);
		//}

		private void setQuestionsInCollection()
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL", this.proposal);
			pc.puts("@NP2_SETNO", this.setNo);
			pc.puts("@PPR_PRODCD", this.product);
			rowset rs = DB.executeQuery("SELECT CQN_CODE, NQN_ANSWER FROM LNQN_QUESTIONNAIRE WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? ", pc);
			while(rs.next())
			{
				vfCollection.setValue(rs.getString("CQN_CODE"), rs.getString("NQN_ANSWER"));
			}
		}
        private void setAssessmentHealthQuestionInCollection()
        {
            SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
            pc.puts("@NP1_PROPOSAL", this.proposal);
            pc.puts("@NP2_SETNO", this.setNo);
            pc.puts("@PPR_PRODCD", this.product);
            rowset rs = DB.executeQuery("SELECT CQN_CODE, NQN_ANSWER FROM lnan_questionnaire WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? and CQN_CODE='BP08'", pc);
            if (rs.next())
            {
               
                    vfCollection.setValue(rs.getString("CQN_CODE"), rs.getString("NQN_ANSWER"));
               
            }
            else
            {
                vfCollection.setValue("BP08", "10" );
            }
        }
        private void setQuestionsDtlInCollection()
		{
			SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL", this.proposal);
			pc.puts("@NP2_SETNO", this.setNo);
			pc.puts("@PPR_PRODCD", this.product);
			rowset rs = DB.executeQuery("SELECT CQN_CODE, CCN_COLUMNID, NQD_ANSWER FROM LNQD_QUESTIONDETAIL WHERE NP1_PROPOSAL=? AND NP2_SETNO=? AND PPR_PRODCD=? ", pc);
			while(rs.next())
			{
				vfCollection.setValue(rs.getString("CQN_CODE")+"~"+rs.getString("CCN_COLUMNID"), rs.getString("NQD_ANSWER"));
			}
		}
		#endregion

	}
}
