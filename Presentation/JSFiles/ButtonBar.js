
// funtion to stop Ctrl N "New window opening"
document.onkeydown = function () {

    if (((event.keyCode == 78) || (event.keyCode == 110)) && (event.ctrlKey)) {
        event.cancelBubble = true;
        event.returnValue = false;
        event.keyCode = false;
        return false;
    }
}


//// funtion to stop Ctrl N "New window opening"
//document.onkeydown = function(){
//	
//	if (((event.keyCode == 78)|| (event.keyCode == 67) || (event.keyCode == 86)) && (event.ctrlKey))
//	{
//		event.cancelBubble = true;
//		event.returnValue = false;
//		event.keyCode = false; 
//		return false;
//	}
//}

entityFound = null;
	
	function getProcessName(btnRef){
		var _btnEvent = btnRef.onclick.toString();
		var _startIndex = _btnEvent.indexOf("executeProcess('") + 16 ;
		_endIndex = _btnEvent.lastIndexOf("'");
		if(_startIndex>15)
			return _btnEvent.substring(_startIndex, _endIndex);
		 else		 
			return "";
	}

	function button_MouseEvent(btnRef, imageName) {

		_imagePath = '../shmalib/images/buttons/' + imageName + '.gif';
		if(btnRef.name!='cmd_Menu')
		{
		_lastEvent = findEntity()._lastEvent;
		}
		
		//_lastEvent = parent.frames[document.getElementById('lfid').value]._lastEvent;		

		processName = getProcessName(btnRef);

		if (processName.length == 0){					//For DML Buttons
			switch (btnRef.name){
				case 'cmd_AddNew':				
					if (_lastEvent != 'None')
						btnRef.src = _imagePath;		
					break;
				case 'cmd_Save':
					if ((_lastEvent == 'New') || (_lastEvent == 'Save'))
						btnRef.src = _imagePath;
					break;
				case 'cmd_Update':
					if ((_lastEvent != 'New') && (_lastEvent != 'Save') && (_lastEvent != 'View'))
						btnRef.src = _imagePath;
					break;
				case 'cmd_Delete':
					if (_lastEvent == 'Edit')
						btnRef.src = _imagePath;
					break;
				case 'cmd_Menu':		
					btnRef.src = _imagePath;
					break;
				default : 
					//alert(_imagePath);
					if (_lastEvent != 'None')
						btnRef.src = _imagePath;
					break;
			}
		}							//For Process Buttons
		else{							
			if (findEntity().IsProcessAllowed(processName)){
				btnRef.src = _imagePath;
			}
		}		
	}	
	function setButtons(){		
		//_lastEvent = parent.frames[document.getElementById('lfid').value]._lastEvent;	
		_lastEvent = findEntity()._lastEvent;			
	}	
	
	function sendMenu() {
		var ppp=parent;
		for (i=0;i<100;i++) {

			if (ppp.parent==null)
			break;
			ppp=ppp.parent;
		}		
		ppp.window.location = s_MenuAddress ;
	}	
	
	
	function closeWindow()	{
		var ppp=parent;
		for (i=0;i<100;i++){
			if (ppp.parent==null)
			break;
			ppp=ppp.parent;
		}
		if (confirm("Are you sure to Close this Window?")==false){
		 	return;
		}
		else{
			ppp.window.close();
		}
	}

	function findEntity(){		
		if (entityFound == null ){
			tEntity = document.getElementById('targetEntity') ;
			
			
			
			
			
			if (tEntity!=null && tEntity.value > '' )
			{
				findTargetEntity( tEntity.value , parent.frames);
				}
			else
			{
				findTargetEntity(document.getElementById('lfid').value, parent.frames);		
				}
			
		}

		return entityFound ;
	}
       var v=0;
       function findTargetEntity(eID, allFrames) {

		if (allFrames.length >0){
			for (frm=0; frm < allFrames.length; frm++ ){
				if(allFrames[frm].name!='')
				{
				  if (allFrames[frm].name == eID || frm==eID){
				    	entityFound = allFrames[frm];				
					    return ;
				}
				else{
					v=frm;
					findTargetEntity( eID,  allFrames[frm].frames );
					frm=v;
				    }	
				}
			}			
		}
	}	