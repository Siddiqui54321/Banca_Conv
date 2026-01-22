/**************************** Business Input Valiadation *****************************/
function validateTerm(product, term)
{
	var parameters = product + "," + term;
	var methodName = "validateTerm";
	var className  = "ace.Ace_General";
	return executeClass(className+ "," +methodName+ "," +parameters); 
}


function validateRate(rate)
{
	var parameters = rate;
	var methodName = "validateRate";
	var className  = "ace.Ace_General";
	return executeClass(className+ "," +methodName+ "," +parameters); 
}