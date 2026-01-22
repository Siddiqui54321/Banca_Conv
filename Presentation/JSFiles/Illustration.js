//ReportType
var reportType_ILLUSTRATION = "ILLUSTRATION";
var reportType_ADVICE       = "ADVICE";
var reportType_PROFILE      = "PROFILE";
var reportType_PDILLUS      = "PDILLUS";
var reportType_POLICY       = "POLICY";
var reportType_PROPOSALINQ  = "PROPOSALINQ";
var reportType_SECURITYLOG  = "SECURITYLOG";
var reportType_PROPSUMMARY  = "PROP_SUMMARY";


var wOpen;
function executeReport(reportType)
{
	
	try
	{	
		if(validateCriteria(reportType) == true)
		{ 
	
			openReportInPopup(reportType);
		}
		else
		{
			return false;
		}
	}
	catch(err)
	{
		//alert('Report printing error: ' + err.message);
	} 	
}

function validateCriteria(reportType)
{
	if(reportType == reportType_PROPOSALINQ || reportType == reportType_SECURITYLOG || reportType == reportType_PROPSUMMARY)
	{
		return true;
	}
	else
	{
		//Proposal Check
		var proposal = document.getElementById('txtNP1_PROPOSAL').value;
		//alert(proposal);
		
		if (proposal=="" || proposal==null)
		{
			alert('Please select a proposal number.');
			return false;
		}	
		
		//Some more Validation from Server
		var className="ace.Ace_General";
		var methodName="validateReportCriteria";
		var parameters = proposal + ',' + reportType;
		var strError = executeClass(className + "," + methodName + "," + parameters); 		
		//alert(1);
		if(strError.length > 0)
		{	
			return false;
		}
		
		return true;
    }
}

function openReportInPopup(reportType)
{
	var sOptions;
	sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
	sOptions = sOptions + ',width=' + (screen.availWidth /2.15).toString();
	sOptions = sOptions + ',height=' + (screen.availHeight/2.7).toString();

	var aw = screen.availWidth - 10;
	var ah = screen.availHeight - 30;

	var xc = ( aw - (screen.availWidth /2.15) ) / 2;
	var yc = ( ah - (screen.availHeight/2.7) ) / 2;

	sOptions += ",left=" + xc + ",screenX=" + xc;
	sOptions += ",top=" + yc + ",screenY=" + yc;

	if(wOpen != null)
		if(false ==  wOpen.closed )
			wOpen.close();
			
	wOpen = window.open('', "wOpen", sOptions );
	var str_target ='';
	if(reportType == reportType_PROPSUMMARY)
	{
		var reportParameters = "";
		//reportParameters += "_q_cProposal," + proposal                  + "," + proposal + ";";
		reportParameters += "_q_cDateFrom," + getFieldValue("DATEFROM") + "," + getFieldValue("DATEFROM") + ";";
		reportParameters += "_q_cDateTo,"   + getFieldValue("DATETO")   + "," + getFieldValue("DATETO") + ";";
		
		str_target= "../CrystalReports/CrystalReport.aspx?_ParamStr=" + reportParameters + "&_RepName=" + "../CrystalReports/prop_summary";
	}
	else
	{	
		var proposal = document.getElementById('txtNP1_PROPOSAL').value;
		str_target= "ExecuteReport.aspx?_Proposal=" + proposal + "&_ReportType=" + reportType;
		
	}	
	
	wOpen.location = str_target;
	wOpen.focus();
}

