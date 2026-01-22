var _lastEvent;
var debug = false;

function addClicked(){		
	//if (debug) 	alert('in normal add clicked');
	_lastEvent = "Add";
	callEvent('Add','', '');
}
function saveClicked(){
	if (debug) alert('in normal save clicked, validating,Last Event: ' + _lastEvent );
	//if (_lastEvent == 'Add')
	//{
		if (beforeSave())
		{	if (debug) alert('going to save normal');
			_lastEvent = "Save";
			callEvent('Save','', '');					
		}
	//}		
}
function updateClicked(){
	if (_lastEvent != 'Add'){
		if (beforeUpdate())
			callEvent('Update','', '');
	}
}
function deleteClicked(){
	if (_lastEvent != 'Add' && _lastEvent != 'Delete')	
		callEvent('Delete','', '');
}
function deleteDetail(){
	callEvent('Delete','', '');
}
function editClicked(){
	callEvent('Edit','', '');
}
function sendMenu(){
	callEvent('','', '');
}

function send()
{
	callEvent('Save','', '');
}

function fcvalidate(a,t,n) 
{
	return true;
}

//======================================

function callEvent(eventType, arg, argValue)
{			
	if (debug) alert('in call event of Master, lastEvent: ' + _lastEvent + ', eventType: ' + eventType);
	if (eventType == 'Delete')
	{				
		if (confirm("Are you sure to delete?")==false)
			return;			
	}		
	myForm._CustomArgName.value = arg;
	myForm._CustomArgVal.value = argValue;
	myForm._CustomEventVal.value = eventType;
	if (debug) alert ('callEvent of RowsetNormal: going to post back');
	if (eventType == 'Filter'){
		__doPostBack('_CustomEvent','');	
		if (debug) alert ('callEvent of RowsetNormal: posted back');
	}
	else{
		if (debug) alert ('callEvent of RowsetNormal: going to call onClick of Custom event');
		myForm._CustomEvent.onclick();
		if (debug) alert ('callEvent of RowsetNormal: called onClick of Custom event');
	}
}

function beforeSave(){return true;}
function beforeUpdate(){return true;}
//afterDataListerRowClicked(obj_Ref)