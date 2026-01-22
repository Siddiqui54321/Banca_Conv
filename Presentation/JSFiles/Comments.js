
function isCommentGiven(sender, args){

	var valID = '#'+sender.id;

	var txtID = sender.id.substring(0,sender.id.lastIndexOf('_')+1)+'txtComments';
	var txt = document.getElementById(txtID);
	var ddlID = sender.id.substring(0,sender.id.lastIndexOf('_')+1)+'ddlStatus';
	var ddl = document.getElementById(ddlID);
	if(ddl.value=="Y" || ddl.value=="."){
		args.IsValid = true;
		$(valID).hide();
		return;
	}
	if(txt.value == ""){
		args.IsValid = false;
		$(valID).show();
		return;
	}
	args.IsValid = true;
	$(valID).hide();
	return;

}


function showInfo(){
	$("#divInfo").css('display','block');
}
function hideInfo(){
	$("#divInfo").css('display','none');
}
