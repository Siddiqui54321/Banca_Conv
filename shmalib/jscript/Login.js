var pwdAccept = false;
function handleEnter (obj, event)
{       
	var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;       
	if (keyCode == 13)
	{                   
		document.getElementById(obj).click();
		return false;       
	}       
	else  
	{
		return true;  
	}  
} 	
function showAlert()
{	
	
	var msg = document.getElementById('lblMsg').innerHTML;
	
	if(msg != null && msg != '')
	{
		document.getElementById('txtPassword').focus();
		alert(msg);
		return false;		
	}
}
function showDiv()
{					
	document.getElementById('tdPwd').innerHTML = 'Old Password';
	document.getElementById('divChgPwd').style.display = 'block';			
	document.getElementById('txtPassword').focus();
	document.getElementById('img__').style.visibility = 'hidden';			
}
function showStrengthDiv(show,chgDivID){
	var divStyle = document.getElementById('stengthDiv').style;				
	if(show){
		var chgDiv = document.getElementById(chgDivID);
		var width = chgDiv.offsetWidth;
		var topValue= 0,leftValue= 0;
		while(chgDiv){
			leftValue+= chgDiv.offsetLeft;
			topValue+= chgDiv.offsetTop;
			chgDiv = chgDiv.offsetParent;
		}		
		divStyle.display = 'block';
		if(chgDivID != 'NormalEntryTableDiv'){
			divStyle.top = topValue;
			divStyle.left = leftValue+width+2;	
		}	
	}
	else{
		divStyle.display='none';					
	}
}
function validateOldPwd(oldPwdID,newPwdID,confirmPwdID)
{
	var oldPwd = document.getElementById(oldPwdID);
	var newPwd = document.getElementById(newPwdID);
	if(oldPwd.value == '')
	{
		alert('Please enter valid Old Password.');
		oldPwd.focus();
		return false;					
	}
	else if(oldPwd.value == newPwd.value)
	{		
		alert('Old Password must not equal to New Password,Please re-enter New Password.');
		resetPwdTextOnly(newPwd,document.getElementById(confirmPwdID));
		//newPwd.focus();
		return false;					
	}	
	return true;
}
function onLoginClick()
{
	var loginID = document.getElementById('txtUserCode');
	var loginPWD = document.getElementById('txtPassword');
	if(loginID.value == '')
	{
		alert('Please enter valid User ID.');
	}
	else if(loginPWD.value == '')
	{
		alert('Please enter valid Password.');
	}
	else
	{
		document.getElementById('Imagebutton1').click();
	}
}
function isPwdChecked(id1,id2)
{	
	var pwdText = document.getElementById(id1);
	var confPwdText = document.getElementById(id2);					
	return pwdText.value == confPwdText.value ? true : false;
}
function getThis(a)
{
	alert(a);
}
function validatePwdOnLostFocus(id1,id2)
{
	if(!isPwdChecked(id1,id2))
	{
		var pwdText = document.getElementById(id1);
		var confPwdText = document.getElementById(id2);					
		resetPwdText(pwdText,confPwdText);
	}
}
function resetPwdText(obj1,obj2)
{	
	alert('Confirm Password failed, Please re-enter password');	
	resetPwdTextOnly(obj1,obj2);
}
function resetPwdTextOnly(obj1_,obj2_)
{	
	var strength = document.getElementById('strength');	
	strength.innerHTML = 'Type Password';
	obj2_.value = '';
	obj1_.value = '';	
	obj1_.focus();
}
function validatePassword(id1,id2,btnId)
{	
	var pwdText = document.getElementById(id1);
	var confPwdText = document.getElementById(id2);	
	if(pwdText.value == '' || confPwdText.value == '')
	{	
		alert('Please enter New Password and Confirm Password.');						
		pwdText.focus();
		return false;
	}	
	else
	{		
		if(!isPwdChecked(id1,id2))
		{	
			resetPwdText(pwdText,confPwdText);
			return false;
		}
		else
		{	
			if(pwdAccept)
			{
				var status = confirm('Are you sure to change your password');
				if(!status)
				{		
					resetPwdText(pwdText,confPwdText);
					return false;
				}
				else
				{
					document.getElementById(btnId).click();					
					return true;
				}
			}
			else
			{	
				resetPwdTextOnly(pwdText,confPwdText);
				alert('Entered Password is not strong enough.');
				return false;
			}
		}
	}	
}


function passwordChanged(pwd) {	
	var strength = document.getElementById('strength');
	var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
	var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
	var enoughRegex = new RegExp("(?=.{6,}).*", "g");	
	if (pwd.value.length==0) {
		strength.innerHTML = 'Type Password';
		pwdAccept = false;
	} else if (false == enoughRegex.test(pwd.value)) {
		strength.innerHTML = 'More Characters';
		pwdAccept = false;
	} else if (strongRegex.test(pwd.value)) {
		strength.innerHTML = '<span style="color:green">Strong!</span>';
		pwdAccept = true;
	} else if (mediumRegex.test(pwd.value)) {
		strength.innerHTML = '<span style="color:orange">Medium!</span>';
		pwdAccept = true;
	} else {
		strength.innerHTML = '<span style="color:red">Weak!</span>';
		pwdAccept = false;
	}
}

