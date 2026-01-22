function getURLbyEntityID(entityID)
{
	return '../Presentation/'+getEntityNameByEntityID(entityID)+'.aspx';
}

function getURLbyEntityName(entityName)
{
	return '../Presentation/'+entityName+'.aspx';
}
function getURLbyEntityObject(obj)
{
	return '../Presentation/'+obj.entityID+'.aspx';
}