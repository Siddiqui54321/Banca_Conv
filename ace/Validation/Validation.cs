using System;
using System.Data;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;


namespace ace
{
	public class Validation
	{
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
   
		#region "Constructor and Member Variables"
		private string product="";
		private string validationField="";
		private object inputValue;
		private VFC collection;
		private FieldRegister vfRegister;
		
		public Validation(string p_Product, string p_ValidationField, object p_InputValue, VFC p_Collection, FieldRegister p_VFRegister)
		{
			this.product = p_Product;
			this.validationField = p_ValidationField;
			this.inputValue = p_InputValue;
			this.collection = p_Collection;
			this.vfRegister = p_VFRegister;
		}

		#endregion

		#region "Main Process"
		public void validateProduct()
		{
			try 
			{
				string fieldComb = "";
				string validationType = "C";
				string query = "SELECT PVF_TYPE,PVF_FIELDCOMB FROM LPVF_VALIDFIELDS WHERE PPR_PRODCD=? AND PVF_CODE=? ";
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.clear();
				pc.puts("@PPR_PRODCD",product);
				pc.puts("@PVF_CODE",validationField);
				rowset rsVF = DB.executeQuery(query,pc);
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
					throw new Exception(vfRegister.getDescription(validationField) + "(" + validationField + ") is not defined in Product Field Setup (LPVF_VALIDFIELDS)");
				}

				try
				{
					string qryValidation = "SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD=? AND PVL_VALIDATIONFOR=? " + getQueryForValueCombination(fieldComb);
					SHMA.Enterprise.Data.ParameterCollection pcValidation=new SHMA.Enterprise.Data.ParameterCollection();
					pcValidation.puts("@PPR_PRODCD", this.product);
					pcValidation.puts("@PVL_VALIDATIONFOR", this.validationField);
					rowset rsValidation = DB.executeQuery(qryValidation, pcValidation);

					//********* Call Range Validation ***************
					ValidateRange(rsValidation);
				}
				catch (Exception ex) 
				{
					throw new Exception("<BR><B><U>" + vfRegister.getDescription(validationField) + "</U></B>" + ex.Message + "<BR>");
					//throw ex;
				}
			}
			catch (Exception ex) 
			{
				//Throw New Exception("Validating " & clsIlasUtility.getValFieldDesc(validationField) & ". Error: " & ex.Message)
				throw ex;
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
					valueComb = valueComb + this.collection.getString(fieldArray[i]) + ",";
				}
				valueComb = valueComb.Remove(valueComb.LastIndexOf(","), 1);
           
				return valueComb;
			}
			else 
			{
				return fieldComb;
			}
		}
		#endregion
   
		#region "Validation"
		/*********************** Finally Validate the input value now ****************************/
		private void ValidateRange(rowset rsValidation)
		{	
			string rangeError = "";
			try 
			{
				if(rsValidation.size() < 1)
				{
					throw new Exception("Validation is not defined");
				}

				bool blnContinuee = true;
				//bool firstError = true;

				while (rsValidation.next() && blnContinuee) 
				{
					string dataType = this.vfRegister.getDataType(this.validationField);
					
					object fromExpression;
					try {fromExpression = Evaluate(rsValidation.getString("PVL_VALIDFROM"),dataType);}
					catch(Exception e){throw new Exception("Error in evaluating Range From. " + e.Message);}
					
					if(dataType == NUMERIC)
					{
						object toExpression;
						try {toExpression = Evaluate(rsValidation.getString("PVL_VALIDTO"),dataType);}
						catch(Exception e){throw new Exception("Error in evaluating Range To. " + e.Message);}

						double dbInputValue = Convert.ToDouble(inputValue);
						double rangeFrom = Convert.ToDouble(fromExpression);
						double rangeTo = Convert.ToDouble(toExpression);

						if (dbInputValue >= rangeFrom && dbInputValue <= rangeTo) 
						{
							rangeError = "";
							//Check for More Validation (in Detail) for 
							string detailValidationStatus = DetailValidation(rsValidation);
							if(detailValidationStatus == "")
							{
								rangeError = "";
								//Break the loop 
								blnContinuee = false;
							}
							else
							{
								rangeError += detailValidationStatus;
							}
						}
						else 
						{
							//Get Detail Error 
							//if(firstError == false) rangeError += "\\n       OR      ";
							rangeError += getAccumulatedError(rsValidation.getString("PPR_PRODCD"), rsValidation.getString("PVL_VALIDATIONFOR"), rsValidation.getInt("PVL_LEVEL"));
							//firstError = false;
						}
					}
					else//CHARACTER
					{
						string strInputValue = "'" + Convert.ToString(inputValue).ToUpper() + "'";
						string rangeFrom = "'" + Convert.ToString(fromExpression).ToUpper() + "'";
						rangeFrom = rangeFrom.Replace("''","'");
						

						if (strInputValue == rangeFrom) 
						{
							rangeError = "";
							//Check for More Validation (in Detail) for 
							string detailValidationStatus = DetailValidation(rsValidation);
							if(detailValidationStatus == "")
							{
								rangeError = "";

								//Break the loop 
								blnContinuee = false;
							}
							else
							{
								rangeError += detailValidationStatus;
							}
						}
						else 
						{
							//Get Detail Error 
							//if(firstError == false) rangeError += "\\n       OR      ";
							rangeError += getAccumulatedError(rsValidation.getString("PPR_PRODCD"), rsValidation.getString("PVL_VALIDATIONFOR"), rsValidation.getInt("PVL_LEVEL"));
							//firstError = false;
						}
					}
				}
           
				if (rangeError != "" ) 
				{
					//if(rangeError.Substring(rangeError.Length-1,1) != "]")
					//{
					//	if(rangeError.Substring(rangeError.Length-1,1) == " ")
					//		rangeError = rangeError.Trim();
					//	else
					//		rangeError += "]";
					//}
					throw new Exception(rangeError);
				}
			}
			catch (Exception ex) 
			{
				throw ex;         
			}
		}

		
		/********************** Do the Detail Validation *************************************/
		private string DetailValidation(rowset rsMainValidation)
		{
			try
			{
				//while(rsMainValidation.next())
				//{
					//This query will execute all detail validations at once
					string query = "SELECT 'A' FROM DUAL ";

					//Master record (LPVL_VALIDATION) information
					string product = rsMainValidation.getString("PPR_PRODCD");
					string vField  = rsMainValidation.getString("PVL_VALIDATIONFOR");
					int    vfLevel = rsMainValidation.getInt("PVL_LEVEL");

					//Getting detail conditions from LPVD_VALIDATIONDETAIL
					rowset rsConditions = DB.executeQuery("SELECT * FROM LPVD_VALIDATIONDETAIL WHERE PPR_PRODCD='" + product + "' AND PVL_VALIDATIONFOR='" + vField + "' AND PVL_LEVEL=" + vfLevel + " ORDER BY PVD_SEQUENCE ");
					if (rsConditions.size() > 0)
					{
						//
						query += " WHERE 1=1 ";

						//Getting each condition and make it part of the query
						while(rsConditions.next())
						{
							string fieldNature  = rsConditions.getString("PVD_FIELDNATURE").ToUpper().Trim();
							string field        = rsConditions.getString("PVD_VALIDATIONFOR").ToUpper().Trim();
							string dataType     = rsConditions.getObject("PVD_DATATYPE")==null    ? "" : rsConditions.getString("PVD_DATATYPE").ToUpper().Trim();
							string relOpertor   = rsConditions.getObject("PVD_RELOPERATOR")==null ? "" : rsConditions.getString("PVD_RELOPERATOR").ToUpper().Trim();

							if(fieldNature == LOGICAL_OPERATOR || fieldNature == DELIMITER)
							{
								query += " "  + field + " ";
							}
//							else if(fieldNature == VALIDATION_QUERY)
//							{
//								string subQuery = getExpression(query, collection);
//							}
							else
							{
								/*********** Getting Input Value **************/
								string columnName = "";
								string inputValue = "";
								//Get Input value
								try
								{
									columnName = this.vfRegister.getFieldName(field);

									if(vfRegister.getSourceType(field) == "P")
									{
										if (ValidationUtility.isRider(product) == true)
										{
											columnName = columnName + product;
										}
									}									
								}
								catch(Exception e)
								{	
									//field might be a question which can be find directly in collection by its code (as this is not a db field)
									columnName = field;
								}
								
								inputValue = this.collection.getString(columnName);
								if (inputValue.Trim() == "")
								{
									throw new Exception("Unknown field [" + field + "]");
								}

								/************ Data Type **********/
								if(dataType == CHARACTER || dataType == DATE)
								{
									inputValue = "'" + inputValue + "'";
									inputValue = " " + inputValue.Replace("''","'") + " ";
								}
								inputValue = " " + inputValue + " ";
						
								/************ Relational Operator **********/
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

						if(rs.next()== false)
						{
							throw new Exception("Range validation error.");
						}
					}//end of -->if (rsConditions.size() > 0)
				//}//end of --> while(rsMainValidation.next()) 
				return "";
			}
			catch(Exception e)
			{
				return getAccumulatedError(rsMainValidation.getString("PPR_PRODCD"),rsMainValidation.getString("PVL_VALIDATIONFOR"),rsMainValidation.getInt("PVL_LEVEL"));
			}
		}


		#endregion

		#region "Display Accumulated Validation Error"
		private string getAccumulatedError(string product, string vField, int serialNo)
		{
			string dataType = this.vfRegister.getDataType(vField);
			string query = "";//PVD_VALIDATIONFOR 
            query = " SELECT 0 SEQ,       '"+dataType+"' DATATYPE, NVL(VFS_DESC,PVL_VALIDATIONFOR) VFIELD, 'V'             FNATURE, 'BETWEEN'       OPERATOR, PVL_VALIDFROM VFROM, PVL_VALIDTO VTO FROM LPVL_VALIDATION A LEFT OUTER JOIN LVFS_FIELDSETUP B ON A.PVL_VALIDATIONFOR=B.VFS_CODE       WHERE PPR_PRODCD='"+product+"' AND PVL_VALIDATIONFOR='"+vField+"' AND PVL_LEVEL="+serialNo
                + " UNION "
                + " SELECT PVD_SEQUENCE SEQ,PVD_DATATYPE DATATYPE, NVL(VFS_DESC,PVD_VALIDATIONFOR) VFIELD, PVD_FIELDNATURE FNATURE, PVD_RELOPERATOR OPERATOR, PVD_VALIDFROM VFROM, PVD_VALIDTO VTO FROM LPVD_VALIDATIONDETAIL A LEFT OUTER JOIN LVFS_FIELDSETUP B ON A.PVD_VALIDATIONFOR=B.VFS_CODE WHERE PPR_PRODCD='"+product+"' AND PVL_VALIDATIONFOR='"+vField+"' AND PVL_LEVEL="+serialNo
                + " ORDER BY SEQ ";

			rowset rsValidationFields = DB.executeQuery(query);
			
			string errorReport = "";
			while(rsValidationFields.next())
			{
				//errorReport += rsValidationFields.getString("ERROR_MESSAGE");
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
						processedError = " " + htmlFormatStart + vfName + htmlFormatEnd + vfOperator + rangeFrom + " ";
					}
				}
				else
				{
					processedError =   " <B><I>" + vfName + "</I></B> ";
				}
				errorReport += processedError;
			}
			return "<BR>" + serialNo + ". " + errorReport;
		}
		#endregion

		#region "Expression and Evaluation"
		
		private object Evaluate(string expression, string dataType)
		{
			//evaluateExpression(getExpression(dataReader.getString("PVL_VALIDTO"), collection));}
			return evaluateExpressionByQuery(getExecuteableExpression(expression,this.collection), dataType);
		}

//		private string getExecuteableExpression(string _expression)
//		{
//			do 
//			{
//				int index = _expression.IndexOf(":[");
//				if (index < 0) 
//				{
//					break; // TODO: might not be correct. Was : Exit Do
//				}
//				else 
//				{
//					string field  = "";
//					string fvalue = "";
//					
//					try
//					{
//						field = _expression.Substring(index + 2, _expression.IndexOf("]:") - (index + 2));
//					}
//					catch(Exception e)
//					{
//						throw new Exception("Expression not properly defined. Database column must be like >> :[columnName]:  ");
//					}
//
//					try
//					{
//						fvalue =  this.collection.getString(field);
//					}
//					catch(Exception e)
//					{
//						throw new Exception("Error in getting field(" + field  + ") value.");
//					}
//					
//					_expression = _expression.Replace(":[" + field + "]:", fvalue);
//				}
//			}while (true);
//
//			//Finallay return the process value
//			return _expression;
//		}

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

		//private object evaluateExpressionByQuery(string _expression, string dataType)
		public static object evaluateExpressionByQuery(string _expression, string dataType)
		{
			string query = "";
			if (dataType == "N")
			{
				query = "SELECT round(" + evaluateConditionalOperator(_expression) + ",0) exprResult FROM DUAL ";
			}
			else
			{
				query = "SELECT '" + evaluateConditionalOperator(_expression) + "' exprResult FROM DUAL ";
			}
			
			rowset dataReader = DB.executeQuery(query);
			
			if(dataReader.next())
			{
				return dataReader.getString("exprResult");
				//if(dataReader.getObject("exprResult")== null)
				//{
				//	return null;
				//}
				//else
				//{
				//	if(dataType == "N")
                //        return dataReader.getString("exprResult");
				//	else
				//		return "'" & dataReader.getString("exprResult") & "'";
				//}
			}
			else
			{
				return null;
			}
		}

		private static string evaluateConditionalOperator(string _expression)
		{
			_expression = clsIlasUtility.ReplaceIgnoreCase(_expression, FORM_IF, _CASEWHEN);
			_expression = clsIlasUtility.ReplaceIgnoreCase(_expression, FORM_THEN, _THEN);
			_expression = clsIlasUtility.ReplaceIgnoreCase(_expression, FORM_ELSEIF, _WHEN);
			_expression = clsIlasUtility.ReplaceIgnoreCase(_expression, FORM_ELSE, _ELSE);
			_expression = clsIlasUtility.ReplaceIgnoreCase(_expression, FORM_ENDIF, _END);
			return _expression;
		}
		#endregion
	
		#region "MANUAL ADJUSTMENT"

		public static void ValidateManualAdjustmentFactor(string product, double factor)
		{
			try
			{
				string qryValidation = "SELECT * FROM LPVL_VALIDATION WHERE PPR_PRODCD=? AND PVL_VALIDATIONFOR=? ";
				SHMA.Enterprise.Data.ParameterCollection pcValidation=new SHMA.Enterprise.Data.ParameterCollection();
				pcValidation.puts("@PPR_PRODCD", product);
				pcValidation.puts("@PVL_VALIDATIONFOR", "MANADJFACTOR");
				rowset rsValidation = DB.executeQuery(qryValidation, pcValidation);
				ValidateRange_ManualAdjustment(rsValidation, factor);
			}
			catch (Exception ex) 
			{
				//throw new Exception("<BR><B><U>" + "Manual Adjustment" + "</U></B>" + ex.Message + "<BR>");
				throw ex;
			}
		}

		private static void ValidateRange_ManualAdjustment(rowset rsValidation, double inputValue )
		{	
			string rangeError = "";
			try 
			{
				if(rsValidation.size() < 1)
				{
					throw new Exception("Validation setup is not defined for Manual Adjustment Factor");
				}

				while (rsValidation.next() ) 
				{
					string dataType = NUMERIC;//this.vfRegister.getDataType(this.validationField);
					
					object fromExpression;
					//try {fromExpression = Evaluate(rsValidation.getString("PVL_VALIDFROM"),dataType);}
					//catch(Exception e){throw new Exception("Error in evaluating Range From. " + e.Message);}
					try {fromExpression = rsValidation.getDouble("PVL_VALIDFROM");}
					catch(Exception e){throw new Exception("Error in evaluating Facotr From. " + e.Message);}

					
					if(dataType == NUMERIC)
					{
						object toExpression;
						//try {toExpression = Evaluate(rsValidation.getString("PVL_VALIDTO"),dataType);}
						//catch(Exception e){throw new Exception("Error in evaluating Range To. " + e.Message);}
						try {toExpression = rsValidation.getDouble("PVL_VALIDTO");}
						catch(Exception e){throw new Exception("Error in evaluating Facotr To. " + e.Message);}


						double dbInputValue = Convert.ToDouble(inputValue);
						double rangeFrom = Convert.ToDouble(fromExpression);
						double rangeTo = Convert.ToDouble(toExpression);

						if (dbInputValue >= rangeFrom && dbInputValue <= rangeTo) 
						{
							rangeError = "";
							//Check for More Validation (in Detail) for 
							/*string detailValidationStatus = DetailValidation(rsValidation);
							if(detailValidationStatus == "")
							{
								rangeError = "";
								//Break the loop 
								blnContinuee = false;
							}
							else
							{
								rangeError += detailValidationStatus;
							}*/
						}
						else 
						{
							//Get Detail Error 
							//if(firstError == false) rangeError += "\\n       OR      ";
							//rangeError += getAccumulatedError(rsValidation.getString("PPR_PRODCD"), rsValidation.getString("PVL_VALIDATIONFOR"), rsValidation.getInt("PVL_LEVEL"));
							rangeError += " <B>" + "Factor" + "</B>" + " should be in between " + rangeFrom + " to " + rangeTo + " ";
							//firstError = false;
						}
					}
					/*else//CHARACTER
					{
						string strInputValue = "'" + Convert.ToString(inputValue).ToUpper() + "'";
						string rangeFrom = "'" + Convert.ToString(fromExpression).ToUpper() + "'";
						rangeFrom = rangeFrom.Replace("''","'");
						

						if (strInputValue == rangeFrom) 
						{
							rangeError = "";
							//Check for More Validation (in Detail) for 
							string detailValidationStatus = DetailValidation(rsValidation);
							if(detailValidationStatus == "")
							{
								rangeError = "";

								//Break the loop 
								blnContinuee = false;
							}
							else
							{
								rangeError += detailValidationStatus;
							}
						}
						else 
						{
							//Get Detail Error 
							//if(firstError == false) rangeError += "\\n       OR      ";
							rangeError += getAccumulatedError(rsValidation.getString("PPR_PRODCD"), rsValidation.getString("PVL_VALIDATIONFOR"), rsValidation.getInt("PVL_LEVEL"));
							//firstError = false;
						}
					}*/
				}
           
				if (rangeError != "" ) 
				{
					//if(rangeError.Substring(rangeError.Length-1,1) != "]")
					//{
					//	if(rangeError.Substring(rangeError.Length-1,1) == " ")
					//		rangeError = rangeError.Trim();
					//	else
					//		rangeError += "]";
					//}
					throw new Exception(rangeError);
				}
			}
			catch (Exception ex) 
			{
				throw ex;         
			}
		}

		/*private static string getFormatedError(string product, string vField, int serialNo)
		{
			
			string errorReport = "";
			while(rsValidationFields.next())
			{
				//errorReport += rsValidationFields.getString("ERROR_MESSAGE");
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
						processedError = " " + htmlFormatStart + vfName + htmlFormatEnd + vfOperator + rangeFrom + " ";
					}
				}
				else
				{
					processedError =   " <B><I>" + vfName + "</I></B> ";
				}
				errorReport += processedError;
			}
			return "<BR>" + serialNo + ". " + errorReport;
		}*/
		#endregion

		
	}
}
