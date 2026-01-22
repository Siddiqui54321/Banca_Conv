debug = false;
focusAfterEvent = true;
entityName=''; entityField='';
selectedRecordEntity = '';
numberGenerationField = '';
prevEvent = _lastEvent;
followUpOperation = new Object();
followUpOperation.onsave = "Add";
followUpOperation.onupdate = "Add";

function getMaster(name)
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				if(arr[i][j].name==name)
					return getFrameObject(arr[i][0].name);
			}
		}
		return null;
	}	
	
	function setSelectedRecordEntity(name)
	{
		selectedRecordEntity=name;
	}
	
	function setSelectedRecordEntityByID(entityID)
	{
			name = getEntityNameByEntityID(entityID);
			selectedRecordEntity=name;
	}
	
	function getFrameObject(name)
	{	
		if (debug) alert ('Frame requested, name: ' + name);
		return parent.frames[name];
	}
	
	function getArray()
	{
		//alert('GetArray');
		return arr;
	}
	function errorHandle(name)
	{
		_lastEvent = prevEvent;
		//if (debug) alert ('Error reported to group by an entity, _lastEvent :' + _lastEvent + ' prevEvent ' + prevEvent);
		//submitChilds(name,'','');	
	}
	function detailDelete()
	{
		if(debug) alert('detail delete of group');
		var found = false;
		if (selectedRecordEntity != ''){
			var frameForDeletion = getFrameObject(selectedRecordEntity);
			if (frameForDeletion != null){
				found  = true;
				frameForDeletion.deleteDetail();
			}
		}
		if (!found) {
			alert('No row selected for deletion');
		}
		
	}
	function setFocusAfterEvent(entName,fieldName)
	{
		if (debug) alert ('setFocusAfterEvent, entName: ' + entName + ' fieldNAme: ' + fieldName);
		entityName=entName;
		entityField=fieldName;
		focusAfterEvent = true;
	}
	function setFocusControl(auto)
	{
		focusAfterEvent = auto;
		entityName = '';
		entityField = '';
	}
	function setFollowUpOperation(currentOperation,followUpOperationVal)
		{
			if(currentOperation=="Save")
				followUpOperation.onsave = followUpOperationVal;
			else if(currentOperation=="Update")
				followUpOperation.onupdate = followUpOperationVal;
		}
		
		function executeFollowUpEvent(currentOperation)
		{
			if(currentOperation=="Save")
			{
				if(followUpOperation.onsave=="Add")
					addClicked();
				else if(followUpOperation.onsave=="Edit")
					editClicked();
			}
			else if(currentOperation=="Update")
			{
				if(followUpOperation.onupdate=="Add")
					addClicked();
				else if(followUpOperation.onupdate=="Edit")
					editClicked();
			}
	
	}
	
	function reportEventCA(eventType, flag,surpressMessage)
	{

		if(debug)alert ('In reportEventCA, eventType: ' + eventType + ' flag: ' + flag);
		if (flag == 0)
		{
			if(eventType=='Save')
			{
				if(!surpressMessage)
					alert ('Saved successfully');
				executeFollowUpEvent("Save");
				//editClicked();
			}
			else if(eventType=='Update')
			{
				if(!surpressMessage)
					alert ('Updated successfully');
				executeFollowUpEvent("Update");
				//editClicked();
			}
		}
		else 
		{
			
			_lastEvent = prevEvent;
			
			//alert ('An Error reported to group by Controller Agent, _lastEvent :' + _lastEvent + ' prevEvent ' + prevEvent);
		
			if (flag == 2)
			{
				if (debug) alert ('flag is 2');
				if ((numberGenerationField != null) && (numberGenerationField!= ''))
				{
				
					frameObj = getFrameObject(arr[0][0].name);
					
					obj = frameObj.document.getElementById(numberGenerationField);
					
					if (debug) alert (obj);
					if (obj.disabled = false) {
						obj.focus();
					}
				}
			}
			else 
			if(flag == 1)
			{
				if(!surpressMessage)
					alert('Some Error Occured While Commiting Records..!');
			}
		}
		
	}
/* old..
	function reportEventCA(eventType, flag )
	{
		if(debug) alert ('In reportEventCA, eventType: ' + eventType + ' flag: ' + flag);
				if (flag == 0){
					if(eventType=='Save'){
						//alert ('Saved successfully');
						executeFollowUpEvent("Save");
						//editClicked();
					}
					else if(eventType=='Update'){
						//alert ('Updated successfully');
						executeFollowUpEvent("Update");
				//editClicked();
			}
		}
	

		else {
			
			_lastEvent = prevEvent;
			
			//alert ('An Error reported to group by Controller Agent, _lastEvent :' + _lastEvent + ' prevEvent ' + prevEvent);
		
			if (flag == 2){
				if (debug) alert ('flag is 2');
				if ((numberGenerationField != null) && (numberGenerationField!= '')){
				
					frameObj = getFrameObject(arr[0][0].name);
					
					obj = frameObj.document.getElementById(numberGenerationField);
					
					if (debug) alert (obj);
					if (obj.disabled = false) {
						obj.focus();
					}
				}
			}
			else {
			if (debug) alert('Some Error');
			
			}
		}
		
	}
*/
	function setFocus()
	{
		//alert('In setFocus, entityName: ' + entityName);
		frameObj = getFrameObject(entityName)
		//alert(entityField);
		obj = frameObj.document.getElementById(entityField);
		if(obj==null){
			var totalRecords = frameObj.getTotalRecords();
			if (totalRecords != null){
				obj = frameObj.getTabularFieldByIndex(totalRecords,entityField);
			}
		}
		//getFrameObject(entityName).document.getElementById(entityField).focus();
		if(obj!=null){
			if (obj.disabled==false){
			obj.focus();
			}
		}
		
	}
	function setFocusToControl(entityName,fieldName)
	{
		obj = getFrameObject(entityName);
		obj.getTabularFieldByIndex(1,fieldName).focus();
	}
	function saveClicked()
	{
		if (debug) alert ('Group saveClicked called, lastEvent was: ' + _lastEvent);
		if ((_lastEvent == 'Add') || (_lastEvent == 'Save')) {
			_lastEvent = 'Save';
			setAllSaved(false);		
			getFrameObject(arr[0][0].name).saveClicked();
		}
	}
	function updateClicked()
	{
		if (debug) alert ('Group updateClicked called, lastEvent was: ' + _lastEvent);
		if ((_lastEvent == 'Edit') || (_lastEvent == 'Update')){
			_lastEvent = 'Update';
			setAllUpdated(false);
			getFrameObject(arr[0][0].name).updateClicked();
		}
	}
	function isAllSaved()
	{
		
		for(i=0;i<arr.length;i++)
		{
			for(j=0;j<arr[i].length;j++)
			{
				obj = arr[i][j];			
				if(obj.insertable)
				{
					if(!obj.saved)
						return false;		
				}
			}
		}
		return true;
	}
	function isAllUpdated()
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				obj = arr[i][j];			
				if(obj.insertable)
				{
					if(!obj.updated)
						return false;		
				}
			}
		}
		return true;
	}
	function setAllSaved(flag)
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				obj = arr[i][j];
				obj.saved=false;
			}
		}
	}
	function setAllUpdated(flag)
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				obj = arr[i][j];
				obj.update=false;
			}
		}
	}
	function setSaved(entityName,flag)
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				obj = arr[i][j];			
				if(obj.name==entityName)
				{
					obj.saved=flag;
					return;
				}
			}
		}
	}
	function setUpdated(entityName,flag)
	{
		for(i=0;i<arr.length;i++)
		{
			for(j=1;j<arr[i].length;j++)
			{
				obj = arr[i][j];			
				if(obj.name==entityName)
				{
					obj.updated=flag;
					return;
				}
			}
		}
	}
	function filterChild(name,extraArguments){
	if (debug) alert('In groups filterChild, lastEvent was: ' + _lastEvent); 
		str_URL = '../Presentation/'+name+'.aspx?';
		str_URL+=extraArguments+('&operation='+_lastEvent);
		//str_URL+=extraArguments+('&operation=Add');
		if(debug)alert(str_URL);
		getFrameObject(name).window.location=str_URL;
	}
	function filterclikced()
	{
	}
	function submitChilds(name,status,extraArguments)
	{
		if (debug) alert ('submitChilds of Master called, name: ' + name + ', status: ' + status + ', My last Event: ' + _lastEvent);
		if(status=='Save' && _lastEvent=='Save')
		{
			setSaved(name,true);
			if(isAllSaved()){
				if (debug) alert ('AllSaved is true');
				if (debug) alert ('calling : ' + getFrameObject(controllerAgent));		
				getFrameObject(controllerAgent).saveClicked();
			}
			else {
				if (debug) alert ('AllSaved is false');
			}
		}
		else if(status=='Update' && _lastEvent=='Update')
		{
			setUpdated(name,true);
			if(isAllUpdated()){
				if (debug) alert ('In Group, AllUpdates is true');
				getFrameObject(controllerAgent).updateClicked();
			}
		}
		else if (status == 'Add' && _lastEvent == 'Add'){
			if(arr[0][0].name==name){
				//alert ('master coming first time in Add mode');
				return ;
			}
		}
		//alert('before for');
		for(i=0;i<arr.length;i++)
		{
			childExists = false;
			if(arr[i][0].name==name)
			{
			
				//alert (i + " - " + arr[i][0].name);
				childExists = true;
				if (debug) alert('After ChildExists');
				for(j=1;j<arr[i].length;j++)
				{
					if(status=='Filter')
					{
						filterChild(arr[i][j].name,extraArguments);
					}
					else if(_lastEvent=='Add')
					{
						if (debug) alert ('inside if lastEvent = Add, locVal = ' + 'next frame name: ' + arr[i][j].name);
						locVal = getFrameObject(arr[i][j].name).document.URL;
						if(locVal.indexOf(arr[i][j].name)!=-1)
						{
							if (debug) alert ('Group: calling  addClicked of '  + arr[i][j].name);
							getFrameObject(arr[i][j].name).addClicked();
						}
						else
						{
							if (debug) alert ('Group: populating frame: '  + arr[i][j].name + ' with url: ' + '../Presentation/'+arr[i][j].name+'.aspx?operation=Add');
							getFrameObject(arr[i][j].name).document.URL='../Presentation/'+arr[i][j].name+'.aspx?operation=Add';
						}
					}
					else if(_lastEvent=='Edit')
					{
						locVal = getFrameObject(arr[i][j].name).document.URL;
						if(locVal.indexOf(arr[i][j].name)!=-1)
						{
							getFrameObject(arr[i][j].name).editClicked();
						}
						else
						{
							if (debug) alert ('Group: populating frame: '  + arr[i][j].name + ' with url: ' + '../Presentation/'+arr[i][j].name+'.aspx?operation=Edit');
							getFrameObject(arr[i][j].name).document.URL='../Presentation/'+arr[i][j].name+'.aspx?operation=Edit';
						}
						
					}
					else if(_lastEvent=='Save'){
						 var locVal = getFrameObject(arr[i][j].name);
						 if("saveClicked" in locVal)
								locVal.saveClicked();
						}
					else if(_lastEvent=='Update'){
						if (debug) alert ('In group, calling updateClicked of ' + arr[i][j].name);
						if (debug) alert ('In group, calling updateClicked of the object:' + getFrameObject(arr[i][j].name));			
						getFrameObject(arr[i][j].name).updateClicked();
					}
					else if(_lastEvent=='View')
					{		
						//alert('Here');			
						locVal = getFrameObject(arr[i][j].name).document.URL;
						if(locVal.indexOf(arr[i][j].name)!=-1)
						{
							//getFrameObject(arr[i][j].name).viewClicked();
							getFrameObject(arr[i][j].name).document.URL='../Presentation/'+arr[i][j].name+'.aspx?operation=View';
						}
						else
						{
							if (debug) alert ('Group: populating frame: '  + arr[i][j].name + ' with url: ' + '../Presentation/'+arr[i][j].name+'.aspx?operation=View');
							getFrameObject(arr[i][j].name).document.URL='../Presentation/'+arr[i][j].name+'.aspx?operation=View';
						}
						
					}									
				}
			}
		}

		if(!childExists && focusAfterEvent && entityName!='' && entityField!='')
			setFocus();
	}
	function getLink()
	{
		return './'+myLink+'.aspx';
	}
	
	function addClicked()
	{
		//if (debug)		 alert ('Group addClicked called, lastEvent was: ' + _lastEvent);
		_lastEvent = 'Add';
		
		window.location=getLink()+'?operation=Add';
		//setFocusAfterEvent('shgn_gs_se_stdgridscreen_FN_GL_TR_NG_VCHCAPNMS','ddlPVT_VCHTTYPE');
		//getFrameObject(arr[0][0].name).addClicked();
	}
	
	function editClicked()
	{
		if (debug) alert ('Group editClicked called, lastEvent was: ' + _lastEvent);
		_lastEvent = 'Edit';
		
		window.location=getLink()+'?operation=Edit';
		//setFocusAfterEvent('shgn_gs_se_stdgridscreen_FN_GL_TR_NG_VCHCAPNMS','ddlPVT_VCHTTYPE');
		//getFrameObject(arr[0][0].name).addClicked();
	}
	function viewClicked()
	{
		if (debug) alert ('Group viewClicked called, lastEvent was: ' + _lastEvent);
		_lastEvent = 'View';		
		window.location=getLink()+'.aspx?operation=View';
		//setFocusAfterEvent('shgn_gs_se_stdgridscreen_FN_GL_TR_NG_VCHCAPNMS','ddlPVT_VCHTTYPE');
		//getFrameObject(arr[0][0].name).addClicked();
	}	
	function getEntityIDByEntityName(entityName)
		{
			for(row=0;row<arr.length;row++)
			{
				for(col=0;col<arr[row].length;col++)
				{
					obj = arr[row][col];
					if(obj.name==entityName)
						return obj.entityID;
				}
			}
			return '';
		}
		
		function getEntityNameByEntityID(entityID)
		{
			for(row=0;row<arr.length;row++)
			{
				for(col=0;col<arr[row].length;col++)
				{
					obj = arr[row][col];
					if(obj.entityID==entityID)
						return obj.name;
				}
			}
			return '';
		}
	function ErrorMessage(errMsg){
			var shortMessage = new String();
			var longMessage = new String();
			longMessage = shortMessage = errMsg;
			if(longMessage.indexOf("<ErrorMessage>",0)!=-1){
				alert();
				longMessage = longMessage.replace("<ErrorMessage>","Message:");
				longMessage = longMessage.replace("<ErrorMessageDetail>","\n\nDetail:");
				shortMessage = shortMessage.substring(("<ErrorMessage>").length ,shortMessage.indexOf("<ErrorMessageDetail>",0)) + "\n Dont Show Detail?";
				confirm(shortMessage)==false?alert(longMessage):"";
			}
			else
				alert(errMsg);
}
