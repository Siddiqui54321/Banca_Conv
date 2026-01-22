using System;

namespace SHMA.Enterprise.Formula
{
	/// <summary>
	/// Summary description for FunctionEvaluatorFactory.
	/// </summary>
	public class FunctionEvaluatorFactory
	{

		public FunctionEvaluatorFactory(){
		}
		public FunctionEvaluator EvaluatorFactory(string functionName) {
			switch(functionName){
				case "SUBSTR":
					return new Substr();
				case "CONST":
					return new Const();
				case "MONTH":
					return new Month();
				case "AUTONUMBER":
					return new AutoNumber();
				case "TEXT":
					return new Text();
				case "YEAR":
					return new Year();
				case "NEWNUMBER":
					return new NewNumber();
				case "CUSTOMFUNCTION":
					return new CustomFunction();
				case "PADLEFT":
					return new PadLeft();
				case "PADRIGHT":
					return new PadRight();
				default:
					throw new ApplicationException("UnDefine Function Call");
			}
		}
	}
}
