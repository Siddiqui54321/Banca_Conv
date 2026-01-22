<%@ Page language="c#" Codebehind="PolicyAcceptance_Button.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.PolicyAcceptance_Button" %>
<%@ Register TagPrefix="CV" Namespace="SHMA.CodeVision.Presentation.WebControls" Assembly="CodeVision" %>
<html><head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		
<!-- <LINK href="Styles/Style.css" type="text/css" rel="stylesheet"> -->
<%
	Response.Write(ace.Ace_General.loadInnerStyle());
%>

		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js">			
		</script>
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
<br>
<table width=100% >
	<tr>
		<td class="button2TD" align=right>		
			<a href="#" class="button2" onclick="callSave1();">&nbsp;&nbsp;Validate&nbsp;&nbsp;</a>
			<a href="#" class="button2" id="postBtn" onclick="callSave();">&nbsp;&nbsp;Post&nbsp;&nbsp;</a>
			
		</td>
	</tr>
</table>

</body>
</html>

<script language="javascript">
	function callSave()
	{
		var decisionNote=parent.frames[0].getField("txtNP1_DECISIONNOTE").value;
		if(decisionNote.length>50)
		{
			alert('Decision note should be less then 50 characters.');	  
			return;
		}
		
		//if(parent.frames[0].getField('lblPolicyStatus').innerHTML!=''){
		//	return;
		//}
		
		if(document.getElementById('postBtn').disabled==true)
		{
			return;
		}
		
		if(Number(parent.frames[0].getField("txtNP1_AMOUNTOFPREMIUM").value)<=0)
		{
			alert('Please caculate premium from plan rider');	  
			return;
		}
	  
		if(Number(parent.frames[0].getField("txtNP1_AMOUNTTRANSFERED").value)!=Number(parent.frames[0].getField("txtNP1_AMOUNTOFPREMIUM").value))
		{
			alert('Transfered amount is not equal to amount of premium');
			return;
			parent.frames[0].getField("txtNP1_BALANCEOS").value="";
		}
		else
		{
			balance=parent.frames[0].getField("txtNP1_AMOUNTOFPREMIUM").value - parent.frames[0].getField("txtNP1_AMOUNTTRANSFERED").value;
			parent.frames[0].getField("txtNP1_BALANCEOS").value=formatNumber(balance,2);
			
			if (parent.frames[0].Page_ClientValidate())
			{
				//Validation are required here
				 parent.frames[0].getField("txtNP1_ISSUEDATE").disabled=false;
				 parent.frames[0].getField("txtNP1_ISSUEDATE").readonly=true;
				parent.openWait('Saving Data');
				parent.frames[0].updateClicked();
				document.getElementById('postBtn').disabled=true;
			}
		}
	}
    function validateBeforeDecision() {
        var objPayment = parent.frames[0].document.getElementById('ddlNP1_PAYMENTMET');
        if (objPayment.style.visibility == 'visible' && objPayment.disabled == false) {
            if (objPayment.value == null || objPayment.value == "") {
                alert("Payment Type is mandatory.");
                objPayment.focus();
                return false;
            }
            else {
                if (parent.frames[0].document.getElementById('ddlNP1_PAYMENTMET').value == "B") {
                    if (document.getElementById('txtNP1_ACCOUNTNO').value == "") {
                        alert('Please enter Account Number');
                        return false;
                    }
                }
                else if (parent.frames[0].document.getElementById('ddlNP1_PAYMENTMET').value == "R") {
                    if (parent.frames[0].document.getElementById('txtNP1_CCNUMBER').value == "") {
                        alert('Please enter Credit Card Number');
                        return false;

                    }
                    if (parent.frames[0].document.getElementById('txtNP1_CCNUMBER').value.length != 16) {
                        alert('Credit Card Number must be 16 digits long');
                        return false;
                    }
                    if (parent.frames[0].document.getElementById('txtNP1_CCEXPIRY').value == "") {
                        alert('Please enter Expiry Date');
                        return false;
                    }
                }
            }
        }
        return true;
    }
	//var btnClicked = "";
	function callSave1()
	{
		btnClicked = "VALIDATE";

    	//Validation are required here
		//parent.openWait('Saving Data');
		//parent.frames[0].updateClicked();
		//alert('222');
		//if(parent.frames[0].getField('lblPolicyStatus').innerHTML!=''){
		//	return;
		//}

		if(validateBeforeDecision() == false)
		{
			return;
		}
		/*if(parent.frames[0].document.getElementById('ddlNP1_PAYMENTMET').value == "R")
alert(4);
		{
			if(parent.frames[0].document.getElementById('txtNP1_CCNUMBER').value == "")
			{
				alert('Please enter Credit Card Number');
				return false;	

			}
			if(parent.frames[0].document.getElementById('txtNP1_CCNUMBER').value.length != 16)
			{
				alert('Credit Card Number must be 16 digits long');
				return false;			
			}
			if(parent.frames[0].document.getElementById('txtNP1_CCEXPIRY').value == "")
			{
				alert('Please enter Expiry Date');
				return false;	
			}	
		}*/
		
		if(Number(parent.frames[0].getField("txtNP1_AMOUNTOFPREMIUM").value)<=0)
		{
			alert('Please caculate premium from plan rider');	  
			return;
		}
	  
	    if(Number(parent.frames[0].getField("txtNP1_AMOUNTTRANSFERED").value)!=Number(parent.frames[0].getField("txtNP1_AMOUNTOFPREMIUM").value))
		{
			alert('Transfered amount is not equal to amount of premium');
			return;
		}
		
		parent.frames[0].getField("Button1").click();
		
		//else
		//{
  	    // 	alert("Policy already Approved.");
		//}
		//}
	}

	function formatNumber(myNum, numOfDec) 
    { 
		var decimal = 1 
		for(i=1; i<=numOfDec;i++) 
		{
			decimal = decimal *10 
		}
		
		var myFormattedNum = (Math.round(myNum * decimal)/decimal).toFixed(numOfDec) 
		return(myFormattedNum) 
	}		
	
</script>
