function ShowCalculator()
{
	var wOpen;
	var sOptions;

	sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=no,toolbar=no";
	sOptions = sOptions + ',width=' + (screen.availWidth /2.35).toString();
	sOptions = sOptions + ',height=' + (screen.availHeight/3.8).toString();

	var aw = screen.availWidth - 10;
	var ah = screen.availHeight - 30;

	var xc = ( aw - (screen.availWidth /2.35) ) / 2;
	var yc = ( ah - (screen.availHeight/3.8) ) / 2;

	sOptions += ",left=" + xc + ",screenX=" + xc;
	sOptions += ",top=" + yc + ",screenY=" + yc;

	wOpen = window.open( '', "Calculator", sOptions );
	wOpen.location = "../Presentation/Calculator.aspx";
	wOpen.focus();
	return wOpen;
}
