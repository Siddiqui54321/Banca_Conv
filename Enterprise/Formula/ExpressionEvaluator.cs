using System;
using SHMA.Enterprise;
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Business;
namespace SHMA.Enterprise.Formula
{
	/// <summary>
	/// Summary description for ExpressionEvaluator.
	/// </summary>
	
	public class ExpressionEvaluator
	{
		
		NameValueCollection dataFields;
		NameValueCollection arguments=new NameValueCollection();
		NameValueCollection baseTableFields, parameter;
		NumGenInfo numGenInfo;
		string expression;
		string separator="~";
		string autoNumberField;
		
		public ExpressionEvaluator()
		{
		}

		public void setDataFields(NameValueCollection _dataFields){
			dataFields = _dataFields;
		}

		public void setExpression(string _expression){
			expression = _expression;
		}

		public void setSeparator(string _separator){
			separator = _separator;
		}

		public void setAutoNumberField(string _autoNumberField){
			autoNumberField = _autoNumberField;
		}

		public void setBaseTableFields(NameValueCollection _baseTableFields) {
			baseTableFields = _baseTableFields;
		}

		public void setParameter(NameValueCollection _parameter){
			parameter = _parameter;
		}

		public string Evaluate(){
			string[] singleExpresion;
			string evaluatedExpression="";
			FunctionEvaluatorFactory evFactory = new FunctionEvaluatorFactory();
			FunctionEvaluator getFunctionClass;
			string callName;

			if(expression!="" || expression!=null){
				//throw new ApplicationException("The Expression of Number Generation is Empty");
				singleExpresion = BreakStringExpression();
				for(int i=0;i<singleExpresion.Length;i++){
					callName = MakeSetArguments(singleExpresion[i]);
					getFunctionClass =  evFactory.EvaluatorFactory(callName);
					getFunctionClass.setArgument(arguments);
					getFunctionClass.setDataFields(dataFields);
					getFunctionClass.setBaseTableFields(baseTableFields);
					getFunctionClass.setParameter(parameter); 
					evaluatedExpression += getFunctionClass.getValue().ToString();
				}
			}
			return evaluatedExpression;
		}

		private string MakeSetArguments(string singleExpression){
			int openPT = singleExpression.IndexOf("(");
			string callName;
			string callLessString;
			string[] argumentsArray;
			if(openPT<0) {
				arguments["0"] = expression.Replace("\"","");
				callName = "NOFUNCTION";
			}
			else {
				callName = singleExpression.Substring(0,openPT).ToUpper();
				callLessString = singleExpression.Substring(openPT+1,singleExpression.Length-openPT-2);
				if(callName.ToUpper() == "AUTONUMBER")
					callLessString = autoNumberField + "," + callLessString;
				argumentsArray = callLessString.Split(",".ToCharArray());
				for(int i=0;i<argumentsArray.Length;i++)
					arguments[i.ToString()] = argumentsArray[i];
			}
			return callName;
		}
		private string[] BreakStringExpression(){
			return expression.Split(separator.ToCharArray());
		}
	}
}
