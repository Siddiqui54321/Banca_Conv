/**AUTHOR Khalid Messaging support built in SafeMed
Reintroduced in  ACE
**/
function openWait(msg)
{
	var message = document.getElementById("divProcessing").innerHTML;
	message = message.replace('{0}',msg);
	document.getElementById("divProcessing").innerHTML = message;
	document.getElementById("divProcessing").style.visibility = "Visible";
	//alert();
	return true;
}

function closeWait()
{
	if (document.getElementById("divProcessing").style.visibility=='visible')
	{
		//if(parent.frames[1].ErrorOccured == false)
		//{
			parent.parent.setPageNavigate();
		//}
	}
	document.getElementById("divProcessing").style.visibility = "Hidden";
	document.getElementById("divProcessing").innerHTML = "Please wait ... {0}";
	return true;
}

function closeWait2(blnNavigate)
{
	if (document.getElementById("divProcessing").style.visibility=='visible')
	{
		if(blnNavigate == "Y")
		{
			parent.parent.setPageNavigate();
		}
	}
	document.getElementById("divProcessing").style.visibility = "Hidden";
	document.getElementById("divProcessing").innerHTML = "Please wait ... {0}";
	return true;
}

function Navigate(navigation)
{
	if (document.getElementById("divProcessing").style.visibility == 'visible')
	{
		document.getElementById("divProcessing").style.visibility = "Hidden";
		document.getElementById("divProcessing").innerHTML = "Please wait ... {0}";	
		if(navigation == true)
		{
			parent.parent.setPageNavigate();
		}
	}

	return true;
}