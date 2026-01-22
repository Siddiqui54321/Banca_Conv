//TODO GET THE DBType, locale DYNAMICALLY
//DBType Oracle 1; DB2 2; SQLServer 3; locale
var DBType = 3;
var locale = "dd/MM/yyyy";
function getConcateOperator(){
	var rtrnOperator;
	switch(DBType){
		case 3: rtrnOperator = "_Concat"; break;
		case 1: rtrnOperator = "||"; break;
		case 2: rtrnOperator = "||"; break;
		//case DB.DataBaseType.MySQL: rtrnOperator = "||"; break;
		default: throw new ApplicationException("No Support Avaialable");
	}
	return rtrnOperator;
}

function makeDateToCharFunc(expr){
	var rtrnMethod="";
	switch(DBType){
		case 3: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " CONVERT(VARCHAR,"+ expr +",103) "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " CONVERT(VARCHAR,"+ expr +",101) "; 
			break;
		case 1: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " TO_CHAR("+ expr +",'dd/mm/yyyy') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " TO_CHAR("+ expr +",'mm/dd/yyyy') "; 
			 break;
		case 2: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " REPLACE(CHAR("+ expr +",eur),'.','/') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " CHAR("+ expr +",usa) "; 
			break;
		//case DB.DataBaseType.MySQL: rtrnMethod =  "CAST("+ expr +" AS DATE)"; break;
		default:throw new ApplicationException("No Support Avaialable");
	}
	return rtrnMethod;
}


//deprecated function
function makeDateToChar(expr){
	var rtrnMethod="";
	switch(DBType){
		case 3: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " CONVERT(VARCHAR,"+ expr +",103) "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " CONVERT(VARCHAR,"+ expr +",101) "; 
			break;
		case 1: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " TO_CHAR("+ expr +",'dd/mm/yyyy') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " TO_CHAR("+ expr +",'mm/dd/yyyy') "; 
			 break;
		case 2: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " REPLACE(CHAR("+ expr +",eur),'.','/') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " CHAR("+ expr +",usa) "; 
			break;
		//case DB.DataBaseType.MySQL: rtrnMethod =  "CAST("+ expr +" AS DATE)"; break;
		default:throw new ApplicationException("No Support Avaialable");
	}
	return rtrnMethod;
}

function makeCharToDate(expr){
	var rtrnMethod="";
	switch(DBType){
		case 3: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " CONVERT(DATETIME,"+ expr +",103) "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " CONVERT(DATETIME,"+ expr +",101) "; 
			break;
		case 1: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " TO_DATE("+ expr +",'dd/mm/yyyy') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " TO_DATE("+ expr +",'mm/dd/yyyy') "; 
			 break;
		case 2: 
			if(locale=="dd/MM/yyyy")
				rtrnMethod = " DATE(REPLACE("+ expr +"),'/','.') "; 
			else if(locale=="MM/dd/yyyy")
				rtrnMethod = " DATE("+ expr +") "; 
			break;
		//case DB.DataBaseType.MySQL: rtrnMethod =  "CAST("+ expr +" AS DATE)"; break;
		default:throw new ApplicationException("No Support Avaialable");
	}
	return rtrnMethod;
}


function makeCharFunc(expr){
	var rtrnMethod="";
	switch(DBType){
		case 3: 
			rtrnMethod = " CAST("+ expr +" AS VARCHAR) "; 
			break;
		case 1: 
			rtrnMethod = " TO_CHAR("+ expr +") "; 
			break;
		case 2: 
			rtrnMethod = " CHAR("+ expr +",usa) "; 
			break;
		default:throw new ApplicationException("No Support Avaialable");
	}
	return rtrnMethod;
}
