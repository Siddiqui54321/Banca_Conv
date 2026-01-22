function getChildren(name)
{
	if(name==null||name=='null'||name.length<0)
	name=arr[0][0].name;
	childrenArray = new Array();
	for(i=0;i<arr.length;i++)
	{	
	
	if(arr[i][0].name==name)
		for(j=1;j<arr[i].length;j++)
		{
			childrenArray[j-1]=arr[i][j].name;
		}
	}
	return childrenArray;
}			
function getFrameObject(name)
{	
	return parent.frames[name];
}
function getMyChildFrames()
{
	tmp = getChildren();
	childFrames=new Array();
	for(i=0;i<tmp.length;i++)
	{	
		childFrames[i]=getFrameObject(tmp[i]);
	}
	return childFrames;
}			