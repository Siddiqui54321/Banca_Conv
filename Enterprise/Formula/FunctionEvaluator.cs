using System;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Exceptions;

namespace SHMA.Enterprise.Formula {
	public abstract class FunctionEvaluator{
		protected NameValueCollection dataFields;
		protected NameValueCollection argument;
		protected NameValueCollection baseTableFields, parameter;
		
		public void setDataFields(NameValueCollection _dataFields) {
			dataFields = _dataFields;
		}
		public void setArgument(NameValueCollection _argument ){
			argument = _argument;
		}
		public void setBaseTableFields(NameValueCollection _baseTableFields ){
			baseTableFields = _baseTableFields;
		}

		public void setParameter(NameValueCollection _parameter){
			parameter = _parameter;
		}

		public object getDataFieldValue(string key){
			object keyValue;
			if(dataFields.Contains(key) || baseTableFields.Contains(key)){
				if(dataFields.Contains(key) && dataFields[key]!=null)
					keyValue = dataFields[key];
				else if(baseTableFields.Contains(key) && baseTableFields[key]!=null)
					keyValue = baseTableFields[key];
				else 
					throw new ApplicationException("DataField contain null value");
				}
			else
				throw new ApplicationException("The DataFieldCollecion does not contain DataField:" + key);
			return keyValue;
		}

		public abstract object getValue();
	}

	public  class Substr:FunctionEvaluator {
		
		public override object getValue(){
			int startIndex,length;
			if(argument["0"]==null || argument["1"]==null || argument["2"]==null)
				throw new ApplicationException("Substr have less argument then expected");
			
			startIndex = Convert.ToInt32(argument["1"]);
			length = Convert.ToInt32(argument["2"]);

			return getDataFieldValue(argument["0"].ToString()).ToString().Substring(startIndex,length);
		}
	}
	
	public class Month:		FunctionEvaluator{
		public override object getValue(){
			DateTime dtValue;
			string strValue;
			if(argument["0"]==null)
				throw new ApplicationException("Month argument not set");

			dtValue = Convert.ToDateTime(getDataFieldValue(argument["0"].ToString()));
			strValue = dtValue.Month.ToString();
			if(strValue.Length==1)
				return "0" + strValue;
			else
				return strValue;
		}
	}
	
	public class Year:		FunctionEvaluator{
		public override object getValue(){
			DateTime dtValue;
			string strValue;
			int len;
			if(argument["0"]==null || argument["1"]==null)
				throw new ApplicationException("Year arguments not set");

			dtValue = Convert.ToDateTime(getDataFieldValue(argument["0"].ToString()));
			len = Convert.ToInt32(argument["1"]);
			strValue = dtValue.Year.ToString();
			if(len==2)
				return  strValue.Substring(2,2);
			else if(len==4)
				return strValue;
			else
				throw new ApplicationException("Unable to generate the year value");
		}
	}

	
	public class Const:FunctionEvaluator{
		public override object getValue(){
			if(argument["0"]==null)
				throw new ApplicationException("Const argument not set");
			return(argument["0"]);
		}
	}
	
	public class Text:FunctionEvaluator{

		public override object getValue(){
			if(argument["0"]==null)
				throw new ApplicationException("NoFunction argument not set");
			return(getDataFieldValue(argument["0"].ToString()));
		}
	}
	
	public class AutoNumber:FunctionEvaluator{
		public override object getValue(){
			string autoNumber;
			int numberWidth;
			if(argument["0"]==null || argument["1"]==null )
				throw new ApplicationException("AutoNumber arguments not set");
			numberWidth = Int32.Parse(argument["1"].ToString());
			autoNumber = getDataFieldValue(argument["0"].ToString()).ToString();
			//			if(autoNumber.Length!= Convert.ToInt32(argument["1"].ToString()))
			//				throw new Exception("Invalid length of Generated Number");
			decimal checkNumber = Convert.ToDecimal(autoNumber);
			if(checkNumber.ToString().Length > numberWidth)
				throw new InvalidNumberWidthException();
				
			return autoNumber.Substring(autoNumber.Length - numberWidth, numberWidth);
		}
	}
	
	
	public class CustomFunction:FunctionEvaluator{

		public override object getValue(){
			if(argument["0"]==null)
				throw new ApplicationException("NoFunction argument not set");
			string classType;
			AppDomain currentDomain = AppDomain.CurrentDomain;
			classType = argument["0"].ToString();
			SHMA.Enterprise.ClassLoader cloader = new ClassLoader();
			Object obj = cloader.loadType(classType);
			if(obj !=null) {
				FunctionEvaluator fe = (FunctionEvaluator)obj;
				fe.setDataFields(this.dataFields);
				fe.setBaseTableFields(this.baseTableFields);
				obj = fe.getValue();
			}
			else{
				throw new ApplicationException("CustomFunction Not Found");
			}
			return obj;
		}
	}

	public class PadLeft:FunctionEvaluator
	{
		public override object getValue()
		{
			int length;
			string charToPad, actualField;
			if(argument["0"]==null || argument["1"]==null || argument["2"]==null)
				throw new Exception("PadLeft have less argument then expected");
		
			length = Convert.ToInt32(argument["1"]);
			charToPad = argument["2"].ToString().Trim();

			actualField=getDataFieldValue(argument["0"].ToString()).ToString().Trim();
			
			while(actualField.Length<length)
				actualField = charToPad+actualField;
			return actualField;
		}
	}
	
	public class PadRight:FunctionEvaluator
	{
		public override object getValue()
		{
			int length;
			string charToPad, actualField;
			if(argument["0"]==null || argument["1"]==null || argument["2"]==null)
				throw new Exception("PadLeft have less argument then expected");
		
			length = Convert.ToInt32(argument["1"]);
			charToPad = argument["2"].ToString().Trim();

			actualField=getDataFieldValue(argument["0"].ToString()).ToString().Trim();
			while(actualField.Length<length)
				actualField = actualField + charToPad;
			return actualField;
		}
	}

	public class NewNumber:FunctionEvaluator
	{
		public override object getValue(){
			string autoNumber;
			int numberWidth;
			if(argument["0"]==null || parameter==null )
				throw new ApplicationException("NewNumber arguments not set");
			numberWidth = Int32.Parse(argument["0"].ToString());
			autoNumber = GenerateNumber(numberWidth);
//						if(autoNumber.Length!= Convert.ToInt32(argument["1"].ToString()))
//							throw new Exception("Invalid length of Generated Number");
			decimal checkNumber = Convert.ToDecimal(autoNumber);
			if(checkNumber.ToString().Length > numberWidth)
				throw new InvalidNumberWidthException();
				
			return autoNumber.Substring(autoNumber.Length - numberWidth, numberWidth);
		}

		private string GenerateNumber(int numberWidth){
			decimal startNumber, stopNumber, lastNumber, decNewNumber;
			string entityId,  pec_colsrno,ctr_colsrno,newNumber;
		
			startNumber = (decimal)parameter["STARTNO"];
			lastNumber = (decimal)parameter["LASTNO"];
			stopNumber = (decimal)parameter["STOPNO"];
			entityId = (string)parameter["ENTITYID"];
			pec_colsrno = (string)parameter["SERIALCOLUMN_PEC"];
			ctr_colsrno = (string)parameter["SERIALCOLUMN_CTR"];

			newNumber = GetNewNum(lastNumber.ToString(), numberWidth);
			decNewNumber = decimal.Parse(newNumber);
			if(decNewNumber< startNumber){
				newNumber = GetNewNum((startNumber-1).ToString(), numberWidth);
				if(stopNumber < decimal.Parse(newNumber) && stopNumber!=0)
					throw new InvalidNumberStopException();
			}
			else if(lastNumber == stopNumber && stopNumber!=0)
				throw new InvalidNumberStopException();

			ColumnTransformDB.updateLastNumber(entityId,pec_colsrno,ctr_colsrno,newNumber);
			
			return newNumber;
		}
		
		private string GetNewNum(string oldNumber, int numberWidth) {
			string strNewNumber;
			decimal dblNewNumber;		
			dblNewNumber = decimal.Parse(oldNumber) + 1;
			strNewNumber = dblNewNumber.ToString().PadLeft(int.Parse(numberWidth.ToString()),'0');
			return strNewNumber;
		}
	}
}
