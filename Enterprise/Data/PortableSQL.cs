using System;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for PortableSQL.
	/// </summary>
	public class PortableSQL
	{
		static SHMA.Enterprise.Data.DB.DataBaseType DBType;
		private PortableSQL()
		{
			DBType = DB.dataBaseType;
		}
		
		public static string getConcateOperator(){
			string rtrnOperator = "";
			DBType = DB.dataBaseType;
			switch(DBType){
				case DB.DataBaseType.SQLServer: rtrnOperator = "+"; break;
				case DB.DataBaseType.Oracle: rtrnOperator = "||"; break;
				case DB.DataBaseType.DB2: rtrnOperator = "||"; break;
				case DB.DataBaseType.MySQL: rtrnOperator = "||"; break;
				default: throw new ApplicationException("No Support Avaialable");
			}
			return rtrnOperator;
		}

		public static string makeCharFunc(string expr){
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType){
				case DB.DataBaseType.SQLServer: 
					rtrnMethod = " CAST("+ expr +" AS VARCHAR) "; 
					break;
				case DB.DataBaseType.Oracle: 
					rtrnMethod = " TO_CHAR("+ expr +") "; 
					break;
				case DB.DataBaseType.DB2: 
					rtrnMethod = " CHAR("+ expr +",usa) "; 
					break;
				case DB.DataBaseType.MySQL: rtrnMethod =  " CAST("+ expr +" AS CHAR) "; break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		}
		
		public static string makeDateToCharFunc(string expr){
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType){
				case DB.DataBaseType.SQLServer: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString() == "en-GB")
						rtrnMethod = " CONVERT(VARCHAR,"+ expr +",103) "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-US")
						rtrnMethod = " CONVERT(VARCHAR,"+ expr +",101) "; 
					break;
				case DB.DataBaseType.Oracle: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-GB")
						rtrnMethod = " TO_CHAR("+ expr +",'dd/mm/yyyy') "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-US")
						rtrnMethod = " TO_CHAR("+ expr +",'mm/dd/yyyy') "; 
					break;
				case DB.DataBaseType.DB2: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString()=="en-GB")
						rtrnMethod = " REPLACE(CHAR("+ expr +",eur),'.','/') "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString()=="en-US")
						rtrnMethod = " CHAR("+ expr +",usa) "; 
					break;
				case DB.DataBaseType.MySQL: rtrnMethod =  " CAST("+ expr +" AS DATE) "; break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		}
		
		public static string makeCharToDateFunc(string expr)
		{
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString() == "en-GB")
						rtrnMethod = " CONVERT(DATETIME,"+ expr +",103) "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-US")
						rtrnMethod = " CONVERT(DATETIME,"+ expr +",101) "; 
					break;
				case DB.DataBaseType.Oracle: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-GB")
						rtrnMethod = " TO_DATE("+ expr +",'dd/mm/yyyy') "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString() =="en-US")
						rtrnMethod = " TO_DATE("+ expr +",'mm/dd/yyyy') "; 
					break;
				case DB.DataBaseType.DB2: 
					if(System.Globalization.CultureInfo.CurrentCulture.ToString()=="en-GB")
						rtrnMethod = " DATE(REPLACE('"+ expr +"'),'/','.') "; 
					else if(System.Globalization.CultureInfo.CurrentCulture.ToString()=="en-US")
						rtrnMethod = " DATE('"+ expr +"') "; 
					break;
				case DB.DataBaseType.MySQL: rtrnMethod =  " CAST("+ expr +" AS DATE) "; break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		}


		public static string isNumeric(string expr){
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: 
					rtrnMethod = " isNumeric(" + expr +") "; 
					break;
				case DB.DataBaseType.Oracle: 
					rtrnMethod = " decode(translate(" + expr +",'x0123456789','x'),null,1, 0)"; 
					break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		
		          
		}

		public static string getAddOperator()
		{
			string rtrnOperator = "";
			DBType = DB.dataBaseType;
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: rtrnOperator = "+"; break;
				case DB.DataBaseType.Oracle: rtrnOperator = "+"; break;
				case DB.DataBaseType.DB2: rtrnOperator = "+"; break;
				case DB.DataBaseType.MySQL: rtrnOperator = "+"; break;
				default: throw new ApplicationException("No Support Avaialable");
			}
			return rtrnOperator;
		}

		public static string indexOffFunc(string expr1,string charvalue)
		{
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: 
					rtrnMethod = " CHARINDEX('"+charvalue + "',"+ expr1 +") "; 
					break;
				case DB.DataBaseType.Oracle: 
					rtrnMethod = " INSTR("+ expr1 +",'" + charvalue + "',1) "; 
					break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		}

 		public static string substringFunc(string expr,int len)
		{
			string rtrnMethod="";
			DBType = DB.dataBaseType;
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: 
					rtrnMethod = " SUBSTRING("+ expr +",1,"+ len +") "; 
					break;
				case DB.DataBaseType.Oracle: 
					rtrnMethod = " SUBSTR("+ expr +",1,"+ len +") "; 
					break;
				default:throw new ApplicationException("No Support Avaialable");
			}
			return rtrnMethod;
		}

		public static String NVL(String expr1, String expr2)
		{
			String rtnNVL = "";
			DBType = DB.dataBaseType;
			
			if(expr2==null || expr2.Equals(""))
				expr2="''";
			
			if(expr2.IndexOf("'")==-1 && expr2.IndexOf("?")==-1)
				expr2="'" + expr2 +"'";
                        
			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: rtnNVL = " ISNULL("+expr1+","+expr2+")"; break;             //SQL SERVER
				case DB.DataBaseType.Oracle: rtnNVL = " NVL("+expr1+","+expr2+")"; break;                  //ORACLE
				case DB.DataBaseType.DB2: rtnNVL = " NVL("+expr1+","+expr2+")"; break;                  //DB2
				default: throw new ApplicationException("No Support Avaialable");
			}
			
			return rtnNVL;
		}
            
		public static String NVL(String expr1, int expr2)
		{
			String rtnNVL = "";
			DBType = DB.dataBaseType;

			switch(DBType)
			{
				case DB.DataBaseType.SQLServer: rtnNVL = " ISNULL("+expr1+","+expr2+")"; break;             //SQL SERVER
				case DB.DataBaseType.Oracle: rtnNVL = " NVL("+expr1+","+expr2+")"; break;                  //ORACLE
				case DB.DataBaseType.DB2: rtnNVL = " NVL("+expr1+","+expr2+")"; break;                  //DB2
				default: throw new ApplicationException("No Support Avaialable");
			}
			return rtnNVL;
		}

        public static String DBDateFunc()
        {
            String rtnFunc = "";
            DBType = DB.dataBaseType;

            switch (DBType)
            {
                case DB.DataBaseType.SQLServer: rtnFunc = " Getdate() "; break;             //SQL SERVER
                case DB.DataBaseType.Oracle: rtnFunc = " Sysdate "; break;                  //ORACLE
                case DB.DataBaseType.DB2: rtnFunc = " Sysdate "; break;                  //DB2
                default: throw new ApplicationException("No Support Avaialable");
            }
            return rtnFunc;
        }

        public static String getNoLock()
        {
            String rtnFunc = "";
            DBType = DB.dataBaseType;

            switch (DBType)
            {
                case DB.DataBaseType.SQLServer: rtnFunc = " WITH(NOLOCK) "; break;             //SQL SERVER
                case DB.DataBaseType.Oracle: rtnFunc = " "; break;                  //ORACLE
                case DB.DataBaseType.DB2: rtnFunc = " "; break;                  //DB2
                default: throw new ApplicationException("No Support Avaialable");
            }
            return rtnFunc;
        }


	}
}
