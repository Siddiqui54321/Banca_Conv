function openGoalSeek()
{
	var wOpen;
	var sOptions;

	sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
	sOptions = sOptions + ',width=' + (screen.availWidth /1.5).toString();
	sOptions = sOptions + ',height=' + (screen.availHeight/2.5).toString();

	var aw = screen.availWidth - 10;
	var ah = screen.availHeight - 30;

	var xc = ( aw - (screen.availWidth /1.5) ) / 2;
	var yc = ( ah - (screen.availHeight/2.5) ) / 2;

	sOptions += ",left=" + xc + ",screenX=" + xc;
	sOptions += ",top=" + yc + ",screenY=" + yc;

	wOpen = window.open( '', "GoalSeek", sOptions );

	wOpen.location = "../Presentation/GoalSeek.aspx";
	wOpen.focus();

	return wOpen;
}


function SetGoalSeekValuesinSession()
{


}