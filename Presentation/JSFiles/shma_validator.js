function PeeredValidate(arguments, ctrlArr){
var boolFoundValue;
for(itrateArr=0;itrateArr<ctrlArr.length;itrateArr++){
	if(itrateArr==0)
		valOfBaseControl = document.getElementById(ctrlArr[itrateArr]).value;
	else{
		if(document.getElementById(ctrlArr[itrateArr]).value !='')
			{boolFoundValue=true; break;}
		else
			boolFoundValue=false;
	}
}
if(ctrlArr.length==1 && valOfBaseControl==''){
	arguments.IsValid=false;return;}
if(ctrlArr.length==1 && valOfBaseControl!=''){
	arguments.IsValid=true;return;}
if(boolFoundValue && valOfBaseControl=='')
	arguments.IsValid=false;
else if(boolFoundValue && valOfBaseControl!='')
	arguments.IsValid=true;
}
