<%@ Page language="c#" Codebehind="shgn_bt_se_button_ILUS_ET_GP_PLANRIDER.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_bt_se_button_ILUS_ET_GP_PLANRIDER" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
<%
	Response.Write(ace.Ace_General.LoadPageStyle());
%>
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/ClientUI/UIParameterization.js"></SCRIPT>
		   <script type="text/javascript" language="javascript">

		       function cancelBack(val) {
		           if (val == 0) {
		               var key = event.keyCode;
		               if (key == 8) {
		                   return false;
		               }
		               else {
		               }
		           }
		           else {
		           }

		       }

		       function backspaceFunc(e) {
		           var key = event.keyCode;
		           if (key == 8) {
		               var str = document.getElementById(e).value;
		               var newStr = str.substring(0, str.length - 1);
		               document.getElementById(e).value = newStr;
		           }
		       }
    </script>	
</head>

<body onkeydown="return cancelBack(0)">

<script >
<%
	/*Response.Write(Session["FLAG_RESET_PREMIUM"]==null?"var Reset_Premium='N';"  :  "var Reset_Premium='" +Session["FLAG_RESET_PREMIUM"].ToString() + "';");*/
%>

var selected = false;
function initialSelect()
{
	selected = parent.frames[2].isAllSelected();
	selectDeselectImage('v');
}

//function hideButton()
//{
//	var ButtonsList = parent.parent.hiddenButtons;
//	if (ButtonsList == '')
//	{
//		return;
//	}
//	else
//	{
//		var buttonArray = ButtonsList.split('~');
//
//		for (i=0; i<buttonArray.length; i++)
//		{
//			if(buttonArray[i] != '')
//			{
//				//Search object in button frame
//				var objButton = document.getElementById(buttonArray[i]);
//				if(objButton != null) objButton.style.display='none';
//			}
//		}
//	}
//}
function allToggle()
{

	if (parent.frames[2].isAllSelected()==true)
	{
		parent.frames[2].toggleUnSelectAll();
		selected = false;
		return;
	}
		
	if (isAllSelected()==false)
	{
		parent.frames[2].toggleSelectAll();
		selected = true;
		return;
	}

}

function selectDeselectImage(type)
{
	if (selected==true && type=='u')
		document.getElementById('toggle').src='../shmalib/images/buttons/dSelRider_u.gif';

	if (selected==true && type=='v')
		document.getElementById('toggle').src='../shmalib/images/buttons/dSelRider_v.gif';

	if (selected==false && type=='u')
		document.getElementById('toggle').src='../shmalib/images/buttons/selRider_u.gif';

	if (selected==false && type=='v')
		document.getElementById('toggle').src='../shmalib/images/buttons/selRider_v.gif';
}

function openFund()
{
	var wOpen;
	var sOptions;

	sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=no,toolbar=no";
	//sOptions = sOptions + ',width=' + (screen.availWidth /2.35).toString();
	//sOptions = sOptions + ',height=' + (screen.availHeight/3.8).toString();

	sOptions = sOptions + ',width='  + (screen.availWidth/1.38).toString();
	sOptions = sOptions + ',height=' + (screen.availHeight/2).toString();

	var aw = screen.availWidth  - 30;
	var ah = screen.availHeight - 30;

	var xc = ( aw - (screen.availWidth /1.35) ) / 2;
	var yc = ( ah - (screen.availHeight/1.5) ) / 2;

	sOptions += ",left=" + xc + ",screenX=" + xc;
	sOptions += ",top=" + yc + ",screenY=" + yc;

	wOpen = window.open( '', "Riders", sOptions );
	
	wOpen.location = "../Presentation/shgn_gp_gp_ILUS_ET_LF_LNFUFUNDS.aspx";
	wOpen.focus();
	return wOpen;
}

function openRiders()
{
	if(document.getElementById('showRider').disabled){
		alert('Calculate Premium first!');
		return;
	}
	
    var sqls = "select h.ccl_categorycd from lnph_pholder h where h.nph_code = (select nph_code from lnu1_underwriti u where u.np1_proposal = '" + parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value +"' and u.nph_life='D' and u.nu1_life='F' and (u.nu1_payer = 'O' or u.nu1_payer = 'B'))";
	var results = fetchDataArray(sqls);
	if ( results!= null && results.length>0 && results[1][0] != null && results[1][0] !="")
	{
		if(results[1][0]=="454")
		{
			alert('Riders are not Allowed for the selected Occupational Category.');
			return;
		}
	}
	
	var wOpen;
	var sOptions;

	sOptions = "status=yes,menubar=no,scrollbars=yes,resizable=no,toolbar=no";

	sOptions = sOptions + ',width='  + (screen.availWidth/1.38).toString();
	sOptions = sOptions + ',height=' + (screen.availHeight/2).toString();

	var aw = screen.availWidth  - 30;
	var ah = screen.availHeight - 30;
	
	

	var xc = ( aw - (screen.availWidth /1.35) ) / 2;
	var yc = ( ah - (screen.availHeight/1.5) ) / 2;

	sOptions += ",left=" + xc + ",screenX=" + xc;
	sOptions += ",top=" + yc + ",screenY=" + yc;
	sOptions += ",modal=yes";

	//wOpen = window.open( '', "Riders", sOptions );
	
	
	var InsuredType = "1";
	var age=0;
	var sqlInsuredType = "SELECT DECODE(NVL(NPH_INSUREDTYPE,'N'), 'Y', '1', '2') "+
		" FROM LNP2_POLICYMASTR NP2, LNPH_PHOLDER NPH "+
		" WHERE NPH.NPH_CODE = (SELECT NPH_CODE FROM LNU1_UNDERWRITI "+
		" WHERE NP1_PROPOSAL=NP2.NP1_PROPOSAL AND NPH_LIFE='D' AND NU1_LIFE='F') "+
		" AND (NP2.NP1_PROPOSAL, NP2.NP2_SETNO) IN (SELECT '"+parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value+"', 1 FROM DUAL) " ;

	result = fetchDataArray(sqlInsuredType);
	if ( result!= null && result.length>0 && result[1][0] != null && result[1][0] !="")
	{
		InsuredType = result[1][0];
	}	
	//alert(InsuredType)
	if (InsuredType=='1')
	{
		age = parent.frames[1].document.getElementById('txtNP2_AGEPREM').value; 
	}
	else
	{
		age = parent.frames[1].document.getElementById('txtNP2_AGEPREM2ND').value; 	
	}
	//return;
	
	var s_proposal = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
	var s_age = age; 
	var s_term = parent.frames[1].document.getElementById('txtNPR_BENEFITTERM').value;
	var s_sa = getDeformattedNumber(parent.frames[1].document.getElementById('txtNPR_SUMASSURED'),2); 
	var s_calcbasis = parent.frames[1].document.getElementById('ddlCCB_CODE').value; 

	var parameterString="proposal=" + s_proposal + "&age=" + s_age + "&term=" + s_term + "&sa=" + s_sa + "&calcBasis=" + s_calcbasis;
	//wOpen.location = "../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS.aspx?"+parameterString;
	var page = "../Presentation/shgn_ts_se_tblscreen_ILUS_ET_TB_PLANDETAILS.aspx?"+parameterString;
	
	//if (window.showModalDialog) 
	//{
	//	wOpen = window.showModalDialog(page, "Riders", sOptions);
	//}
	//else
	{
		wOpen = window.open(page, "Riders", sOptions );
	}
	
	wOpen.focus();
	
	
	return wOpen;
}
</script>

<table width=100% >
	<tr>
		<%--<td class="button2TD" align=left>
			<a href="#" id="FundSetting" class="button2" onclick="openFund()" >Funds Setting</a>
		</td>--%>
		<td class="button2TD" align=right>
			<a href="#" id="button2" class="button2" onclick="callSave()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
			<a href="#" id="CalcPremium" class="button2" onclick="calculate_Premium()">Calculate</a>
			<a href="#" id="showRider" class="button2"  onclick="openRiders()">Show Riders</a>
		</td>
	</tr>
</table>

</body>
</html>


<script language="javascript">
	
	//Hide button
	//hideButton();
	function setFundSettingStatus()
	{
		//alert(parent.frames[1].getField("PPR_PRODCD").value);
		//setFieldStatusAsPerClientSetup("PLAN_BUTTON");

		try
		{
			setButtonsProductWise("PLAN_BUTTON",parent.frames[1].getField("PPR_PRODCD").value);
		}
		catch(err)
		{
		
		} 	
	}
	setFundSettingStatus();
	
	function callSave()
	{
	
	if(document.getElementById('button2').disabled){
			alert('Please Calculate Premium again after saving plan details!');
			return;
		}	
      //  alert('1111');*/
		var prodcd = parent.frames[1].document.getElementById("ddlPPR_PRODCD").value;
       // alert("Product Code" + prodcd);
		if (prodcd == "075") {
           // alert('2222');
            var name = parent.frames[1].document.getElementById("txt_NomineeName").value;
            if (name=="") {
                alert("Please Enter Nominee Name");
                parent.frames[1].document.getElementById("txt_NomineeName").focus();
                return;
            }
            var dob = parent.frames[1].document.getElementById("txtNomineeDOB").value;
            if (dob == "") {
                alert("Please Enter Nominee Date of Birth");
                parent.frames[1].document.getElementById("txtNomineeDOB").focus();
                return;
            }
            var age = parent.frames[1].document.getElementById("txtNBF_AGE").value;
            if (age == "") {
                alert("Please Enter Nominee Age");
                parent.frames[1].document.getElementById("txtNBF_AGE").focus();
                return;
            }
            var reltation = parent.frames[1].document.getElementById("ddlCRL_RELEATIOCD").value;
            if (reltation == "0" || reltation == "" || reltation == null) {
                alert("Please Select Relation");
                parent.frames[1].document.getElementById("ddlCRL_RELEATIOCD").focus();
                return;
			}
          // alert('4444');*/
        }
        
		var calcBasis = parent.frames[1].document.getElementById("ddlCCB_CODE").value;
		if(calcBasis == "S")
		{	//Sum Assured
			var sumAssured = parent.frames[1].document.getElementById("txtNPR_SUMASSURED").value;
			sumAssured = sumAssured.replace(/\,/g,'')
			if(eval(sumAssured)<=0){
				alert('Please enter valid value for Sum Assured');
				return;
			}
		}
		else if(calcBasis == "T")
		{	//Total Premium
			var totalPremium = parent.frames[1].document.getElementById("txtNPR_TOTPREM").value;
			totalPremium = totalPremium.replace(/\,/g,'')
			if(eval(totalPremium)<=0){
				alert('Please enter valid value for Total Premium.');
				return;
			}
		}
		//parent.frames[1].document.getElementById("txtNPR_TOTPREM").style.display = "";
		
		//alert(parent.frames[1].document.getElementById('txtNPR_TOTPREM').value);
		//alert(parent.frames[1].document.getElementById('txtNPR_SUMASSURED').value);
		/*
		if(parent.frames[2].totalRecords > 1 && parent.frames[2].Page_ClientValidate())
		{
			saveUpdate();
		}
		else if (parent.frames[1].Page_ClientValidate())
		{
			saveUpdate();	
		}
		*/

		//New Validation (from setup)
		if(parent.frames[1].validatePlan() == false)
		{
			return;
		}
		
		parent.frames[1].getField("PCU_CURRCODE").value = parent.frames[1].getField("PCU_CURRCODE_PRM").value
		//alert(parent.frames[1].getField("PCU_CURRCODE").value);
		//alert(parent.frames[1].getField("PCU_CURRCODE_PRM").value);
		if (parent.frames[1].Page_ClientValidate())
		{
			//alert("saveUpdate");
			//alert(parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value);
			parent.frames[1].document.getElementById('txtNP1_PROPOSAL').value = parent.frames[0].document.getElementById('txtNP1_PROPOSAL').value ;
			
			saveUpdate();
			document.getElementById('showRider').disabled=false;
			document.getElementById('CalcPremium').disabled=false;
			document.getElementById('button2').disabled = true; 

			//parent.openWait('generating riders');
			//alert("ace.GenerateRiders");
			//parent.frames[1].executeProcess('ace.GenerateRiders');
			//parent.closeWait();
        }
       // alert("Save close");
	}

   
	function saveUpdate()
	{
			parent.frames[1].document.getElementById("txtNPR_PAIDUPTOAGE").disabled=false;
			if (parent.frames[1]._lastEvent=='Edit' || parent.frames[1]._lastEvent=='Update')
			{
				parent.frames[1].updateClicked();
			}
			else if (parent.frames[1]._lastEvent=='New')
			{
				//alert(parent.frames[1].document.getElementById('txtNP1_PROPOSAL').value);
				parent.frames[1].saveClicked();
			}
			parent.openWait('saving data');
			
			//if(parseFloat(parent.frames[1].getFieldValue("NPR_PREMIUM"))>0)
			//{
			//	parent.openWait('calculating premium');
			//	setTimeout("parent.frames[1].executeProcess('ace.Calculate_Premium');", 1000); 
			//}

       // alert("Update close");
	}
	

	
	function calculate_Premium()
    {
     
		if(document.getElementById('CalcPremium').disabled){
			alert('Please Save first!');
			return;
		}		
			if (parent.frames[1].Page_ClientValidate())
			{
				//callSave();
				parent.openWait('calculating premium');
				setTimeout("parent.frames[1].executeProcess('ace.Calculate_Premium');", 1000); 
			}
		//}
	}
	
	function test()
	{
	//	alert("Parent Window");
	}

	try
	{
		initialSelect();
	}catch(e)
	{
	
	}

	/*
	function ResetPremium()
	{
		if(Reset_Premium=="Y")
		{
			//if <IMG SRC="file:///C:\DOCUME~1\MUHAMM~1.ASI\LOCALS~1\Temp\rl7h4ulf.bmp">(parent.frames[1].document.readyState == "complete" || parent.frames[1].document.readyState == "interactive") 
			if (document.readyState == "complete" || document.readyState == "interactive") 
			{
				parent.openWait('calculating premium');
				setTimeout("parent.frames[1].executeProcess('ace.Calculate_Premium');", 1000); 
			}
		}
	}
	*/
	
	function disableButtonsStatus()
	{
		//button2
		document.getElementById('showRider').disabled = true;
		document.getElementById('CalcPremium').disabled = true; 
	}
	
	function disableRiderButtonOnly()
	{
		document.getElementById('showRider').disabled = true;
		document.getElementById('CalcPremium').disabled = false; 
	}

	
	function enableButtonsStatus()
	{
		//button2
		document.getElementById('showRider').disabled = false;
		document.getElementById('CalcPremium').disabled = false; 
	}
</script>