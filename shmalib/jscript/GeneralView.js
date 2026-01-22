
function fnTextFocus(varname)
{
   // var objTXT = document.getElementById(varname)
    //objTXT.style.borderColor = "Blue";
}

function fnTextLostFocus(varname)
{
    //var objTXT = document.getElementById(varname)
    //objTXT.style.borderColor = "white";
}

function fnShowMsg(varname)
{
    try
    {
    var objTXT = document.getElementById(varname.replace('txt', 'msg'))
    //objTXT.style.color = "#f8f8f8";
    objTXT.style.color = "green";
    objTXT.style.display = "inline";
    }catch(e){}
}

function fnHideMsg(varname)
{
    try{
    var objTXT = document.getElementById(varname.replace('txt', 'msg'))
    //objTXT.style.color = "blue";
    objTXT.style.display = "none";
    }catch(e){}

}

function focusDate(varname)
{
	fnTextFocus(varname);
	fnShowMsg(varname);
}

function lostfocusDate(varname)
{
	fnTextLostFocus(varname);
	fnHideMsg(varname);
}

//riders
function focusWithValue(varname, idx)
{

	var objTXT = document.getElementById(varname.replace('txt', 'msg'))
	var objValue = document.getElementById(varname);
	if(objValue.readOnly==true)
		return;

	
	var objFrom = getTabularFieldByIndex(idx, 'MinThresholdValue');
	var objTo = getTabularFieldByIndex(idx, 'MaxThresholdValue');
	//alert(document.getElementById(objFrom.id).value +" - " + document.getElementById(objTo.id).value);
	
	objTXT.innerHTML = "<br>" +applyCommaSeperators(objFrom, 2) + " - " + applyCommaSeperators(objTo, 2);

	fnShowMsg(varname);
}

function lostfocusWithValue(varname, idx)
{

	var objFrom = getTabularFieldByIndex(idx, 'MinThresholdValue');
	var objTo = getTabularFieldByIndex(idx, 'MaxThresholdValue');

	var objValue = getTabularFieldByIndex(idx, 'NPR_SUMASSURED');


	var objTXT = document.getElementById(varname.replace('txt', 'msg'))
	
	
	if(objValue.readOnly==true)
		return;
	
	var checkValue = getDeformattedNumber(objValue, 2);
	var fromValue = getDeformattedNumber(objFrom, 2);
	var toValue = getDeformattedNumber(objTo, 2);
	
	
	//alert(checkValue + ", from: " + fromValue + ", to: " + toValue);
	//alert((checkValue < eval(fromValue) || checkValue > eval(toValue)));	
	//alert(getDeformattedNumber(objValue, 2) > getDeformattedNumber(objTo,2));


	
	
	if((checkValue < eval(fromValue) || checkValue > eval(toValue)))
	{
		
		//objValue.focus();
		//try{
		objTXT.style.color = "red";
		objTXT.enabled=true;
		return;
		//}catch(e){}
	}
	objTXT.enabled=false;
	fnHideMsg(varname);
}
//riders

function attachViewNormal()
{
    var t = document.getElementsByTagName('INPUT');
    var i;
    for(i=0;i<t.length;i++)
    {
        if(t[i].type == "text" && t[i].id.indexOf('DATE')!=-1 )
        {
        if(navigator.userAgent.toLowerCase().indexOf('msie') != -1){
            t[i].attachEvent('onfocus', new Function("focusDate('"+t[i].id+ "')"));
            t[i].attachEvent('onblur', new Function("lostfocusDate('"+t[i].id+ "')"));
        }
        else
        {
        	t[i].addEventListener('onfocus', new Function("focusDate('"+t[i].id+ "')"));
            t[i].addEventListener('onblur', new Function("lostfocusDate('"+t[i].id+ "')"));
        }
		}
        
    }
}


function attachViewByNameNormal(id)
{
			var obj = document.getElementById(id);

		if(navigator.userAgent.toLowerCase().indexOf('msie') != -1){
			obj.attachEvent('onfocus', new Function("focusDate('"+obj.id+ "')"));
			obj.attachEvent('onblur', new Function("lostfocusDate('"+obj.id+ "')"));
		}
		else{
			obj.addEventListener('onfocus', new Function("focusDate('"+obj.id+ "')"));
			obj.addEventListener('onblur', new Function("lostfocusDate('"+obj.id+ "')"));
		}
			return true;
}

function attachViewByNameTabular(id)
{
			for ( a=1; a<=totalRecords-1; a++)
			{
				var obj = getTabularFieldByIndex(a, id);

				obj.attachEvent('onfocus', new Function("focusDate('"+obj.id+ "')"));
				obj.attachEvent('onblur', new Function("lostfocusDate('"+obj.id+ "')"));
			}
			return true;
}

function attachViewRiders(id)
{
			for ( a=1; a<=totalRecords-1; a++)
			{
				var obj = getTabularFieldByIndex(a, id);
				if(navigator.userAgent.toLowerCase().indexOf('msie') != -1){
					obj.attachEvent('onfocus', new Function("focusWithValue('"+obj.id+ "','"+a+ "')"));
					obj.attachEvent('onblur', new Function("lostfocusWithValue('"+obj.id+ "','"+a+ "')"));
				}
				else{
					obj.addEventListener('onfocus', new Function("focusWithValue('"+obj.id+ "','"+a+ "')"));
					obj.addEventListener('onblur', new Function("lostfocusWithValue('"+obj.id+ "','"+a+ "')"));
				}
				
			}
			return true;
}




function attachViewBenificiary()
{
    var t = document.getElementsByTagName('INPUT');
    var i;
    for(i=0;i<t.length;i++)
    {
        if(t[i].type == "text" && t[i].id.indexOf('DOB')!=-1 )
        {
	   if(navigator.userAgent.toLowerCase().indexOf('msie') != -1){
    		t[i].attachEvent('onfocus', new Function("focusDate('"+t[i].id+ "')"));
		t[i].attachEvent('onblur', new Function("lostfocusDate('"+t[i].id+ "')"));
            }
	   else {
		t[i].addEventListener('onfocus', new Function("focusDate('"+t[i].id+ "')"));
		t[i].addEventListener('onblur', new Function("lostfocusDate('"+t[i].id+ "')"));
		}
            }
        }
    
}

function applyCommaSeperators(obj, precision)
{
	if(trim(obj.value) == "")
		return;

	var num = new NumberFormat();
	if(precision =="")
		precision=0;
	num.setNumber(obj.value);
	num.setPlaces(precision);
	
	num.setCommas(true);
	
	return num.toFormatted();
}


function function1(msg)
{
		if(msg.length!=0)
			window.status=msg + ' goes here';
		else
			window.status='';
}

function attachViewFocus(tag)
{
	var t = document.getElementsByTagName(tag);
	var i;
	for(i=0;i<t.length;i++)
	{
		if(document.getElementById('TD' + t[i].id)!=null)
		{
			obj = document.getElementById('TD' + t[i].id);
			//alert('TD' + t[i].id);
			var msg= obj.innerHTML;
			if(navigator.userAgent.toLowerCase().indexOf('msie') != -1)
				t[i].attachEvent('onfocus',  new Function("function1('"+msg+"')"));
			else
				t[i].addEventListener('onfocus',  new Function("function1('"+trim(msg.replace(/(\r\n|\n|\r)/gm,""))+"')"));
		}

	}
}

function attachViewFocusTabularVertical(tag)
{
	var t = document.getElementsByTagName(tag);
	var i;
	for(i=0;i<t.length;i++)
	{
		//alert(t[i].name.split(':')[2]);
		if(document.getElementById(t[i].name.split(':')[2])!=null)
		{
			obj = document.getElementById(t[i].name.split(':')[2]);
			//alert('TD' + t[i].id);
			//try{
			var msg= obj.innerHTML;
			//}catch(e){}
			if(navigator.userAgent.toLowerCase().indexOf('msie') != -1)
				t[i].attachEvent('onfocus',  new Function("function1('"+msg+"')"));
			else
				t[i].addEventListener('onfocus',  new Function("function1('"+msg+"')"));
		}

	}
}


function attachViewFocusTabular(tag)
{
	var t = document.getElementsByTagName(tag);
	var i;
	for(i=0;i<t.length;i++)
	{
		if(t[i].id!='')
		{
			obj = document.getElementById(t[i].id);
			attachCaptionsTabular(obj.id);
		}
	}
}


/************************************************************************/
/*********** Methods use to show/hide the controls  - 2009 **************/
/************************************************************************/
		//var objLabel;
		//Hide/unhide the SumAssured/TotalPremium field based on Calculation Basis
		function setFaceValueField(calcBasis)
		{
			//disbleButtonFram();
			//alert(calcBasis);
			
			var SummAssuredLabel = getFieldCaptionFromSetup("PLAN", getField("NPR_SUMASSURED"));
			var PremiumLabel     = getFieldCaptionFromSetup("PLAN", getField("NPR_TOTPREM"));
			
			
			if (SummAssuredLabel == "")  SummAssuredLabel = "Sum Assured";
			if (PremiumLabel     == "")  PremiumLabel     = "Premium";
			
			//NOTE: Temporary blocked Total Premium Field
			//document.getElementById("lblFaceValue").innerText = SummAssuredLabel;//"Face Amount";//"Sum Assured";
			document.getElementById("lbltxtNPR_SUMASSURED").innerText = PremiumLabel;
			
			//if(objLabel == null)
			//	objLabel = document.getElementById("lbltxtNPR_SUMASSURED");//lblFaceValue
			//objLabel.innerText = SummAssuredLabel;
			
			getField("NPR_SUMASSURED").style.visibility = 'visible';
			getField("NPR_SUMASSURED").style.display = "";
			getField("NPR_SUMASSURED").style.width = "184px";
			//getField("NPR_TOTPREM").value=0;
			getField("NPR_TOTPREM").style.visibility = 'hidden';
			getField("NPR_TOTPREM").style.display = "none";
			getField("NPR_TOTPREM").style.width = 0;

			if(calcBasis=="S")//Sum Assured
			{
				//document.getElementById("lblFaceValue").innerText = SummAssuredLabel;//"Face Amount";;//"Sum Assured";
				//objLabel.innerText = SummAssuredLabel
				document.getElementById("lbltxtNPR_SUMASSURED").innerText = SummAssuredLabel;
				
				getField("NPR_SUMASSURED").style.visibility = 'visible';
				getField("NPR_SUMASSURED").style.display = "";
				getField("NPR_SUMASSURED").style.width = "184px";
				//getField("NPR_TOTPREM").value=0;
				getField("NPR_TOTPREM").style.visibility = 'hidden';
				getField("NPR_TOTPREM").style.display = "none";
				getField("NPR_TOTPREM").style.width = 0;
			}
			else //Total Premium
			{
				//document.getElementById("lblFaceValue").innerText = PremiumLabel;//"Modal Premium";
				//objLabel.innerText = PremiumLabel
				document.getElementById("lbltxtNPR_SUMASSURED").innerText = PremiumLabel;
				
				getField("NPR_TOTPREM").style.visibility = 'visible'
				getField("NPR_TOTPREM").style.display = "";
				getField("NPR_TOTPREM").style.width = "184px";
				//getField("NPR_SUMASSURED").value=0;
				getField("NPR_SUMASSURED").style.visibility = 'hidden';
				getField("NPR_SUMASSURED").style.display = "none";
				getField("NPR_SUMASSURED").style.width = 0;
			}
		}
		
		
		//Calculat Total Premium for display only
		function DisplayCalculatedPremium()
		{
			var basicPremium = parseFloat(getFieldValue("NPR_PREMIUM"));
			var riderPremium = parseFloat(getFieldValue("NP1_TOTALRIDERPREM"));
            var charges = parseFloat(getDeformattedCurrency(document.getElementById("txtcharges"),2));
			if(isNaN(basicPremium)) basicPremium = 0;
			if(isNaN(riderPremium)) riderPremium = 0;
			if(isNaN(charges)) charges = 0;
				
			var totPremium = basicPremium + riderPremium+charges;
			setFieldValue("NPR_TOTPREM_2",totPremium);
			
			if(_lastEvent == 'New')
			{
				document.getElementById('IllustraionDetailHeading').style.visibility = 'hidden';
				document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'hidden';
				document.getElementById('tablePHolder').style.visibility = 'hidden';
			}
			else
			{
				document.getElementById('IllustraionDetailHeading').style.visibility = 'visible';
				document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'visible';
				document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'visible';
				document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'visible';
				document.getElementById('tablePHolder').style.visibility = 'visible';
			}
		}
		
		
		//Hide/unhide the Illustration detail
		function showIllustrationDetail(thisObj)
		{	
			var basicPrem = parseFloat(document.getElementById('txtNPR_PREMIUM').value);
			var riderPrem = parseFloat(document.getElementById('txtNP1_TOTALRIDERPREM').value);
			
			//if(_lastEvent != 'New')
			if(basicPrem > 0)
			{	
				if(thisObj.src.indexOf("Extend.jpg") == -1)
				{
					thisObj.src="../shmalib/images/Extend.jpg";
					//parent.parent.document.getElementById('
					//	').style.height = '400.0px';
					if (parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS') == null) {
						parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '230.0px';
					}
					else
					{
						parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '230.0px';
					}
					
					//alert(document.getElementById('txtNPR_SUMASSURED_2').style.visibility+'1');
					document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'hidden';
					document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'hidden';
					document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'hidden';
					document.getElementById('tablePHolder').style.visibility = 'hidden';					
				}
				else
				{
					thisObj.src="../shmalib/images/Stretch.jpg";
					parent.parent.document.getElementById('mainContentFrame').style.height = '550.0px';
					if (parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS') == null) {
						parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '290.0px';
					}
					else
					{
						parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '390.0px';
					}
					
					document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'visible';
					document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'visible';
					document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'visible';
					document.getElementById('tablePHolder').style.visibility = 'visible';		
					//alert(document.getElementById('txtNPR_SUMASSURED_2').style.visibility+'2');
				}
			}
			else
			{
				document.getElementById('IllustraionDetailHeading').style.visibility = 'hidden';
				
				thisObj.src="../shmalib/images/Extend.jpg";
				parent.parent.document.getElementById('mainContentFrame').style.height = '400.0px';

				if (parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS') == null) {
					parent.mainContentFrame.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '230.0px';
				}
				else
				{
					parent.document.getElementById('shgn_ss_se_stdscreen_ILUS_ET_NM_PLANDETAILS').style.height = '230.0px';
				}
				
				document.getElementById('rowNPR_PREMIUMTER_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_PREMIUM_2').style.visibility = 'hidden';
				document.getElementById('rowNPR_TOTPREM_2').style.visibility = 'hidden';
				document.getElementById('tablePHolder').style.visibility = 'hidden';			
				//document.getElementById('txtNPR_SUMASSURED_2').style.visibility = 'hidden';			
				//alert(document.getElementById('txtNPR_SUMASSURED_2').style.visibility+'3');
			}
		}
		
		
		
function formatNumber(myNum, numOfDec) 
    { 
      var decimal = 1 
      for(i=1; i<=numOfDec;i++) 
      decimal = decimal *10 

      var myFormattedNum = (Math.round(myNum * decimal)/decimal).toFixed(numOfDec) 
      return(myFormattedNum) 
} 

//Convert To Feets

function convert_to_feet() {

  
var meters; //variable to hold meters value
var to_feet;//variable to hold the feet conversion amount

/*
if(isNaN(myForm.txtNU1_ACTUALHEIGHT.value)){
	alert('Please enter numeric Values');
	myForm.txtNU1_ACTUALHEIGHT.focus();
}
*/
meters = eval(myForm.txtNU1_ACTUALHEIGHT.value);
//alert('Please enter non negative values');

if(meters<=0 && myForm.txtNU1_ACTUALHEIGHT.value!=""){
	alert('Please enter non negative values');
	myForm.txtNU1_ACTUALHEIGHT.focus();
	return;
}

//Check Combo Value

if(myForm.ddlNPH_HEIGHTTYPE.value=='I')
{
to_feet= formatNumber((meters * 0.0254),2) ;
}
else if(myForm.ddlNPH_HEIGHTTYPE.value=='F')
{
    var feet = document.getElementById('txtNU1_ACTUALHEIGHT').value;
    var inches="0";
    if(feet.indexOf(".")>0){
        inches = feet.substring(feet.lastIndexOf(".")+1);
        feet = feet.substring(0,feet.indexOf("."));
    }
    if(eval(inches)>11)
        inches = inches.substring(0,1);
    var meters = eval(feet*12) + eval(inches);
    to_feet= formatNumber((meters * 0.0254),2);
}
else if(myForm.ddlNPH_HEIGHTTYPE.value=='C')
{
to_feet= formatNumber((meters/100 ),2) ;
}
else if(myForm.ddlNPH_HEIGHTTYPE.value=='M')
{
to_feet=formatNumber(meters,2);
}
else if(myForm.ddlNPH_HEIGHTTYPE.value=='')
{
to_feet="";
}
//alert(to_feet);
//check to make sure they entered a positive number
if (to_feet< 0 || to_feet=="NaN" || to_feet=='')
     {
     to_feet=0;
     }
else
 {
//display feet conversion
     myForm.txtNU1_CONVERTHEIGHT.value = to_feet;
    // alert(myForm.txtNU1_CONVERTHEIGHT.value + '...Final Result of Weight and Covert Feet Value...' + to_feet);

calculateBMI();

}

}


//Calculate BMI
function calculateBMI(){
if(myForm.txtNU1_CONVERTWEIGHT.value!="" && myForm.txtNU1_CONVERTHEIGHT.value!="")
  {
  var BMI=myForm.txtNU1_CONVERTWEIGHT.value/(myForm.txtNU1_CONVERTHEIGHT.value*myForm.txtNU1_CONVERTHEIGHT.value);
  myForm.txt_bmi.value=Math.round(BMI);
  }
}

function roundNumber(num, dec) {
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}

//Weight Conversion Script
function Weight_Conversion()
{
	var weight; //variable to hold meters value
	var to_convert;//variable to hold the feet conversion amount

	weight = eval(myForm.txtNU1_ACTUALWEIGHT.value);

if(weight<=0 && myForm.txtNU1_ACTUALWEIGHT.value!=""){
	alert('Please enter non negative values');
	myForm.txtNU1_ACTUALWEIGHT.focus();
	return;
}

	//Check Combo Value

	if(myForm.ddlNPH_WEIGHTTTYPE.value=='K')
	{
	to_convert=formatNumber(weight,2);

	}
	else if(myForm.ddlNPH_WEIGHTTTYPE.value=='L')
	{
	to_convert=formatNumber(weight/2.2,2);

	}
	else if(myForm.ddlNPH_WEIGHTTTYPE.value=='')
	{
	to_convert="";

	}
	//alert(to_feet);
	//check to make sure they entered a positive number
	if (to_convert< 0 || to_convert=="NaN" || to_convert=='')
		{
		to_convert= 0;
		}
	else
	{
	//display feet conversion
	    myForm.txtNU1_CONVERTWEIGHT.value = to_convert;
	    //alert(myForm.txtNU1_CONVERTWEIGHT.value+'...Final Result of Weight and Covert Value...' + to_convert);

	//Calculate BMI
	calculateBMI();
}

}

		///////////////////// New Work 
		function validateBenefitTerm(product, term)
		{
			
			if (parent.parent.newValidation != 'Y')
			{
				var result = validateTerm(product, term) ;
				if (result != "")
				{
					// alert(result);
					//getField('NPR_BENEFITTERM').focus();
				}
			}
		}
		
		
		function setRateField()
		{
			disbleButtonFram();
			
			if (getField("NPR_INDEXATION").value == "N")
			{
				getField("NPR_INDEXRATE").value="0.00";
				getField("NPR_INDEXRATE").readOnly=true;
				getField("NPR_INDEXRATE").style.color ='#747170';
			}
			else
			{
				getField("NPR_INDEXRATE").readOnly=false;
				getField("NPR_INDEXRATE").style.color ='#000000';
			}
		}
				
		
		function validateIdexRate(rate)
		{
			if (parent.parent.newValidation != 'Y')
			{
				if (getField("NPR_INDEXATION").value != "Y")
				{
					var result = validateRate(rate) ;
					if (result != "")
					{
						//alert(result);
						//getField('NPR_INDEXRATE').focus();
					}
				}
			}
		}

		function toTitleCase(str)
		{
			var str1 = str.value;

			str1 = str1.replace(/[^\s]+/g, function(word) {return word.replace(/^./, function(first) {return first.toUpperCase();});});

			str.value=str1;
			// alert(str1);
		}