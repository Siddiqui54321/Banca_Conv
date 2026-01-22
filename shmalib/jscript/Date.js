function dateDiffYears(date1, date2, roundingCriteria)
{
	var day1, day2;
	var month1, month2;
	var year1, year2;

	value1 = date1
	value2 = date2;//"01/02/2008";

	day1 = value1.substring (0, value1.indexOf ("/"));
	month1 = value1.substring (value1.indexOf ("/")+1, value1.lastIndexOf ("/"));
	year1 = value1.substring (value1.lastIndexOf ("/")+1, value1.length);

	day2 = value2.substring (0, value2.indexOf ("/"));
	month2 = value2.substring (value2.indexOf ("/")+1, value2.lastIndexOf ("/"));
	year2 = value2.substring (value2.lastIndexOf ("/")+1, value2.length);

	date1 = year1+"/"+month1+"/"+day1;
	date2 = year2+"/"+month2+"/"+day2;

	var firstDate = Date.parse(date1)
	var secondDate= Date.parse(date2)
	
	msPerDay = 24 * 60 * 60 * 1000 * 365.25;
	
	if(roundingCriteria.toUpperCase() == "FLOOR")
	{
		dbd = Math.floor((secondDate.valueOf()-firstDate.valueOf())/ msPerDay) ;
	}
	else if(roundingCriteria.toUpperCase() == "CEIL")
	{
		dbd = Math.ceil((secondDate.valueOf()-firstDate.valueOf())/ msPerDay) ;
	}
	else
	{
		dbd = Math.round((secondDate.valueOf()-firstDate.valueOf())/ msPerDay) ;
	}

	return dbd;
}

function getDateObject(strDate)
{
	var day   = strDate.substring (0, strDate.indexOf ("/"));
	var month = strDate.substring (strDate.indexOf ("/")+1, strDate.lastIndexOf ("/"));
	var year  = strDate.substring (strDate.lastIndexOf ("/")+1, strDate.length);
	return Date.parse(year + "/" + month + "/" + day);
}

function getNextDateAsDate(strDate)
{
	var day   = strDate.substring (0, strDate.indexOf ("/"));
	var month = strDate.substring (strDate.indexOf ("/")+1, strDate.lastIndexOf ("/"));
	var year  = strDate.substring (strDate.lastIndexOf ("/")+1, strDate.length);

	var myDate = new Date(month + "/" + day + "/" + year);
	myDate.setDate(myDate.getDate()+1);
	return myDate;
}

function getNextDateAsString(strDate)
{
	var day   = strDate.substring (0, strDate.indexOf ("/"));
	var month = strDate.substring (strDate.indexOf ("/")+1, strDate.lastIndexOf ("/"));
	var year  = strDate.substring (strDate.lastIndexOf ("/")+1, strDate.length);

	var myDate = new Date(month + "/" + day + "/" + year);
	myDate.setDate(myDate.getDate()+1);
	return myDate.getDate()+"/"+ (myDate.getMonth()+1) +"/"+myDate.getYear();
}

function formatDate(t,format){
	var txtValue = t.value;
	var DD;
	var MM;
	var YYYY;
	var txtLength = txtValue.length;
	if(txtLength>=6 && txtLength<=8){
	if(txtLength==6){
		DD = '0' + txtValue.substring(0,1);
		MM = '0' + txtValue.substring(1,2);
		YYYY = txtValue.substring(2,6);
		}
	else if(txtLength==7){
		DD = txtValue.substring(0,2)
		MM = '0' + txtValue.substring(2,3)
		YYYY = txtValue.substring(3,7)
		}
	else if(txtLength==8){
		DD = txtValue.substring(0,2)
		MM = txtValue.substring(2,4)
		YYYY = txtValue.substring(4,8)
		}
	format=format.replace(/DD/,DD);
	format=format.replace(/MM/,MM);
	format=format.replace(/YYYY/,YYYY);
	t.value=format;
	}
}
