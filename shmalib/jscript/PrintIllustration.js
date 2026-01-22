function IllustrationPrint()
{
	//alert("IllustrationPrint");
		var wOpen;
		var sOptions;
        var report_calling=null;
	// var ParamStr = "_q_cProposal," + document.getElementById('txtNP1_PROPOSAL').value + "," + document.getElementById('txtNP1_PROPOSAL').value +";"
    
		var wlocation=window.location.toString();
		
		sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no";
		sOptions = sOptions + ',width=' + (screen.availWidth /2.15).toString();
		sOptions = sOptions + ',height=' + (screen.availHeight/2.7).toString();

		var aw = screen.availWidth - 10;
		var ah = screen.availHeight - 30;

		var xc = ( aw - (screen.availWidth /2.15) ) / 2;
		var yc = ( ah - (screen.availHeight/2.7) ) / 2;

		sOptions += ",left=" + xc + ",screenX=" + xc;
		sOptions += ",top=" + yc + ",screenY=" + yc;


		if (document.getElementById('lblProposal').value=="")
		{
			alert('Please select a proposal number.');
			return;
		}

		wOpen = window.open( '', "Bancassurance", sOptions );
		//var ParamStr = "_q_cProposal," + window.opener.document.getElementById('txtNP1_PROPOSAL').value + "," + window.opener.document.getElementById('txtNP1_PROPOSAL').value +";"
		var ParamStr = wlocation.substr(wlocation.indexOf("?"));
/*
		var str_target;

		var rpt=window.opener.document.getElementById('txtRptName').value;

		//alert(rpt);
		if (rpt=='ENG')
		{

			var sql = "Select INSTR(get_syspara.get_value('GLOBL','AV_PRODUCT'), pr.ppr_prodcd) CNT FROM LNPR_PRODUCT PR WHERE NP1_PROPOSAL='"+ window.opener.document.getElementById('txtNP1_PROPOSAL').value +"' AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'"; 

	     	var result = fetchDataArray(sql);

//			var str_target = "";
			if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
		   {
				if (result[1][0] > 0 )
					str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/"+report_calling;
				else
					str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/"+report_calling;
		   }
			//else
			//   {
		  	//	  //str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/Illustration2";
		  	//	  str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/Illustration";
	        //   }

			//str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/Illustration";
		}
		else
		{
		
			str_target= fcgetReportUrl() +"?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/"+report_calling;
		}
		
*/
		wOpen.location = fcgetReportUrl() + ParamStr;
		
		wOpen.focus();
		return wOpen;
}


function fcgetReportUrl() {
	return "../CrystalReports/ReportPrint.aspx";
}