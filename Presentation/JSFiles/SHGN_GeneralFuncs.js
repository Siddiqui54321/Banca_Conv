var org,counter,lastIndex,startIndex,tmp;
var ok=true; 
var eg;
function restrictNode(int_NodeId) {
	var ppp=parent;
	for (i=0;i<100;i++) {
		if (ppp.parent==null)
			break;
		ppp=ppp.parent;
	}
	ppp.frames[1].frames[0].str_NAIndex[ppp.frames[1].frames[0].str_NAIndex.length]=int_NodeId
}
function allowNode(int_NodeId) {
	var ppp=parent;
	for (var i=0;i<100;i++) {
		if (ppp.parent==null)
			break;
		ppp=ppp.parent;
	}
	var fr_Parent=ppp.frames[1].frames[0];
	for (var i=0;i<fr_Parent.str_NAIndex.length;i++) 
	{
		if (fr_Parent.str_NAIndex[i]==int_NodeId)
			fr_Parent.str_NAIndex[i]="";
	}
}
function checkData(str_CompId, str_Message)
{
	document.frm_FetchData.action="../servlet/shgn.DataChecker?component="+str_CompId+"&message="+str_Message;
	document.frm_FetchData.target=("RemoteComboIFrame"+int_CurrentIFrame);
	int_CurrentIFrame++;
	if (int_CurrentIFrame>8)
		int_CurrentIFrame=1;
	document.frm_FetchData.submit();
}
function valueExists(str_ComponentId, str_Message) {
	alert(str_Message);
	document.getElementById(str_ComponentId).focus();
}
function fcvalidate(a,t,n) 
{
	if (ok==false && eg!=a) 
	{
		ok=true;
		return true;
	}
	if (n==false && a.value=="")
		return true;
	if (t=="D" || t=="d") 
	{
		if (fcisDateForId(a)==false) 
		{
			alert ("Not a Valid Date");
			a.focus();
			ok=false;
			eg=a;
			return false;
		}
	}
	if (t=="N" || t=="n") 
	{
		if (fcisNumberForId(a)==false) 
		{
			alert ("Not a Numeric Value");
			ok=false;
			eg=a;
			a.focus();
			return false;
		}
		//numberFormat(a.id);
	}
	if (t=="C" || t=="c") 
	{
		if (fcisEmptyForId(a)==true) 
		{
			alert ("Blank not allowed...");
			a.value="";
			ok=false;
			eg=a;
			a.focus();
			return false;
		}
	}
	ok=true;
	return true;
}
function fcisEmptyM(z){
	org=z;
	if (org==null)
		return true;
	counter=0,lastIndex=0,startIndex=0,tmp="";

	while (counter!=-1)
	{
		counter=org.indexOf(" ",lastIndex);
		if (counter!=-1)
		{
			tmp+=org.substring(startIndex,counter);
			lastIndex=counter+1;
			startIndex=lastIndex;
		}
		else
		{
			tmp+=org.substring(startIndex,org.length);
		}
	}
	if (tmp=="")
		return true;
	else
		return false;
}
function fcisEmptyForId(z)
{
	org=z.value;
	if (org==null)
		return true;
	counter=0,lastIndex=0,startIndex=0,tmp="";
	while (counter!=-1)
	{
		counter=org.indexOf(" ",lastIndex);
		if (counter!=-1)
		{
			tmp+=org.substring(startIndex,counter);
			lastIndex=counter+1;
			startIndex=lastIndex;
		}
		else
		{
			tmp+=org.substring(startIndex,org.length);
		}
	}
	if (tmp=="")
		return true;
	else
		return false;
}
function removePrefixZero(z)
{
	str=z.value;
	while (str!="0" && str.charAt(0)=="0") {
		if (str.length>1)
			str=str.substring(1);
	}
	z.value=str;
}
function fcisNumberForId(z)
{
	str=z.value;
	str=fcremoveChar(str,",");
	if (fcisEmptyM(str))
		return false;
	while (str!="0" && str.charAt(0)=="0") {
		if (str.length>1)
			str=str.substring(1);
	}
	if (isNaN(str)==true || str=='') 
	{ 
		return false;
	} 
	z.value=fcsetNo(str);
	return true;
}

function fcsetNo(str) {
	return str;
	var ostr="";
	if (str.indexOf(".")!=-1) {
		ostr=str.substring(str.indexOf("."));
		str=str.substring(0,str.indexOf("."));
	}
	var slen=3; 
	var nstr="";
	var elen=str.length-slen;
	while (elen>0) {
		nstr=","+str.substring(elen,elen+3)+nstr;
		slen+=3;
		elen=str.length-slen;
	}
	nstr=str.substring(0,elen+3)+nstr;
	nstr+=(ostr.length>0?ostr:"");
	return nstr;
}

function fcremoveChar(str,r) {
	//return str;
	while (str.indexOf(r)!=-1)
		str=str.replace(r,"");
	return str;
}

function removeChar(str,r) {
	return fcremoveChar(str,r);
}

function fcisNumberM(a)
{
	if (fcisEmptyM(a))
		return false;
	if (isNaN(a)==true || a=='') 
	{ 
		return false;
	} 
	return true;
}
function fcisDateM(b)
{
     try {
	var sep="",c,count=0,li=0;
	var day,month,year;
	if (b.indexOf("/")!=-1)
		sep="/";
	else if (b.indexOf("-")!=-1)
		sep="-";
	if (sep=="" && b.length==8) {
		b=b.substring(0,2)+"/"+b.substring(2,4)+"/"+b.substring(4,8);
		//b=xxx.value;
		sep="/";
	}
	if (sep!="")
	{
		while (c!=-1)
		{
			c=b.indexOf(sep,li);
			if (c!=-1)
			{
				count++;
				li=c+1;
			}
		}
	}
	if (count!=2)
	{
		return false;
	}
	c=0,li=0,si=0;
	c=b.indexOf(sep,li);
	day = b.substring(si,c);
	li=c+1;	si=li;
	c=b.indexOf(sep,li);
	month = b.substring(si,c);
	li=c+1;
	year = b.substring(li,b.length);

	var tday=eval(day)/100;
	var tmon=eval(month)/100;
	var tyear=eval(year)/100;

	if (year.length!=2 && year.length!=4)
	{
		return false;
	}
	if (month>12 || month<1)
	{
		return false;
	}
	switch(eval(month))
	{
		case 1:
		case 3:
		case 5:
		case 7:
		case 8:
		case 10:
		case 12:
			if (day>31 || day<1)
			{
				return false;
			}
			break;
		case 4:
		case 6:
		case 9:
		case 11:
			if (day>30 || day<1)
			{
				return false;
			}
			break;
		case 2:
			var newday=(year%4);
			if (newday==0)
				newday=29;
			else
				newday=28
			if (day>newday || day<1)
			{
				return false;
			}
			
	}
	return true;
   }
   catch(exception) {
	return false;
   }
}
function fcisDate(b)
{
   try {
	var sep="",c,count=0,li=0;
	var day,month,year;
	if (b.indexOf("/")!=-1)
		sep="/";
	else if (b.indexOf("-")!=-1)
		sep="-";
	if (sep=="" && b.length==8) {
		b=b.substring(0,2)+"/"+b.substring(2,4)+"/"+b.substring(4,8);
		//b=xxx.value;
		sep="/";
	}
	if (sep!="")
	{
		while (c!=-1)
		{
			c=b.indexOf(sep,li);
			if (c!=-1)
			{
				count++;
				li=c+1;
			}
		}
	}
	if (count!=2)
	{
		return false;
	}
	c=0,li=0,si=0;
	c=b.indexOf(sep,li);
	day = b.substring(si,c);
	li=c+1;	si=li;
	c=b.indexOf(sep,li);
	month = b.substring(si,c);
	li=c+1;
	year = b.substring(li,b.length);
	var tday=eval(day)/100;
	var tmon=eval(month)/100;
	var tyear=eval(year)/100;
	if (year.length!=2 && year.length!=4)
	{
		return false;
	}
	if (month>12 || month<1)
	{
		return false;
	}
	switch(eval(month))
	{
		case 1:
		case 3:
		case 5:
		case 7:
		case 8:
		case 10:
		case 12:
			if (day>31 || day<1)
			{
				return false;
			}
			break;
		case 4:
		case 6:
		case 9:
		case 11:
			if (day>30 || day<1)
			{
				return false;
			}
			break;
		case 2:
			var newday=(year%4);
			if (newday==0)
				newday=29;
			else
				newday=28
			if (day>newday || day<1)
			{
				return false;
			}
			
	}
	return b;
   }
   catch(exception) {
	return false;
   }
}

function fcisDateForId(xxx)
{
   try {
	b=xxx.value;
	var sep="",c,count=0,li=0;
	var day,month,year;
	if (b.indexOf("/")!=-1)
		sep="/";
	else if (b.indexOf("-")!=-1)
		sep="-";
	if (sep=="" && b.length==8) {
		xxx.value=b.substring(0,2)+"/"+b.substring(2,4)+"/"+b.substring(4,8);
		b=xxx.value;
		sep="/";
	}
	if (sep!="")
	{
		while (c!=-1)
		{
			c=b.indexOf(sep,li);
			if (c!=-1)
			{
				count++;
				li=c+1;
			}
		}
	}
	if (count!=2)
	{
		return false;
	}
	c=0,li=0,si=0;
	c=b.indexOf(sep,li);
	day = b.substring(si,c);
	li=c+1;	si=li;
	c=b.indexOf(sep,li);
	month = b.substring(si,c);
	li=c+1;
	year = b.substring(li,b.length);

	var tday=eval(day)/100;
	var tmon=eval(month)/100;
	var tyear=eval(year)/100;
	if (year.length!=2 && year.length!=4)
	{
		return false;
	}
	if (month>12 || month<1)
	{
		return false;
	}
	switch(eval(month))
	{
		case 1:
		case 3:
		case 5:
		case 7:
		case 8:
		case 10:
		case 12:
			if (day>31 || day<1)
			{
				return false;
			}
			break;
		case 4:
		case 6:
		case 9:
		case 11:
			if (day>30 || day<1)
			{
				return false;
			}
			break;
		case 2:
			var newday=(year%4);
			if (newday==0)
				newday=29;
			else
				newday=28
			if (day>newday || day<1)
			{
				return false;
			}
			
	}
	return true;
   }
   catch(exception) {
	return false;
   }
}
function fcgetMaxDateM(b,bb){
	var sep="",c=0,count=0,li=0;
	var day,month,year;
	if (b.indexOf("/")!=-1)
		sep="/";
	else if (b.indexOf("-")!=-1)
		sep="-";
	c=0,li=0,si=0;
	c=b.indexOf(sep,li);
	day = eval(b.substring(si,c));
	//if (day.length>1)
	//	day=day.charAt(0)=="0"?day.charAt(1):day;
	li=c+1;	si=li;
	c=b.indexOf(sep,li);
	month = eval(b.substring(si,c));
	//if (month.length>1)
	//	month=month.charAt(0)=="0"?month.charAt(1):month;
	li=c+1;
	year = eval(b.substring(li,b.length));
//---------------------------------------------
	sep="",count=0;
	var day1,month1,year1;
	if (bb.indexOf("/")!=-1)
		sep="/";
	else if (bb.indexOf("-")!=-1)
		sep="-";
	c=0,li=0,si=0;
	c=bb.indexOf(sep,li);
	day1 = eval(bb.substring(si,c));
	//if (day1.length>1)
	//	day1=day1.charAt(0)=="0"?day1.charAt(1):day1;
	li=c+1;	si=li;
	c=bb.indexOf(sep,li);
	month1 = eval(bb.substring(si,c));
	//if (month1.length>1)
	//	month1=month1.charAt(0)=="0"?month1.charAt(1):month1;
	li=c+1;
	year1 = eval(bb.substring(li,bb.length));
	if (year>year1) 
		return 1;
	else if (year<year1)
		return 2;
	if (month>month1)
		return 1;
	else if (month<month1)
		return 2;
	if (day>day1)
		return 1;
	else if (day<day1)
		return 2;
	return 0;
}
function fcsetCombo(id,vl)  { 
  	z=document.getElementById(id); 
 	for (a=0;a<z.length;a++) 
 	{ 
 		if (z.options[a].text==vl) 
 		{ 
 			z.options[a].selected=true; 
 			break; 
 		} 
 	}	 
} 
function fcsetComboForValue(id,vl)  { 
  	z=document.getElementById(id); 
 	for (a=0;a<z.length;a++) 
 	{ 	
 		if (z.options[a].value===vl) 
 		{ 	
 			z.options[a].selected=true; 
 			return; 
 		} 
 	}
	z.selectedIndex=-1;
} 
function fccheckDecimal(v,nd,d,n){
	if (ok==false && eg!=v) {
		ok=true;
		return true;
	}
	if (n==false && v.value=="")
		return true;
	var vv=v.value;
	vv=fcremoveChar(vv,",");
	var znd="",zd="";
	for (z=0;z<nd;z++)
		znd+="9";
	for (z=0;z<d;z++)
		zd+="9";
	if (fcisNumberM(vv)==false) {
		alert ("Not a valid No...");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	var a=vv.indexOf(".");
	if (a==-1 && vv.length>nd) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	var b=vv.substring(0,a);
	if (b.length>nd) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	if (a==-1) {
		ok=true;
		return true;
	}
	b=vv.substring(a+1,vv.length);
	if (b.length>d) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	ok=true;
	v.value=fcsetNo(vv);

	//numberFormat(v.id);
	return true;
}
function fccheckDecimalWithBlank(v,nd,d,n,blk){
	if(blk=="blank")
	{
		if(v.value.length==0)
			return true;
	}
	
	if (ok==false && eg!=v) {
		ok=true;
		return true;
	}
	if (n==false && v.value=="")
		return true;
	var vv=v.value;
	vv=fcremoveChar(vv,",");
	var znd="",zd="";
	for (z=0;z<nd;z++)
		znd+="9";
	for (z=0;z<d;z++)
		zd+="9";
	if (fcisNumberM(vv)==false) {
		alert ("Not a valid No...");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	var a=vv.indexOf(".");
	if (a==-1 && vv.length>nd) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	var b=vv.substring(0,a);
	if (b.length>nd) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	if (a==-1) {
		ok=true;
		return true;
	}
	b=vv.substring(a+1,vv.length);
	if (b.length>d) {
		alert ("Not a valid format ("+znd+"."+zd+")");
		ok=false;
		eg=v;
		v.focus();
		return false;
	}
	ok=true;
	//v.value=setNo(vv);
	//numberFormat(v.id);
	return true;
}

function fcisPositiveNo(a,b) {
	if (ok==false && eg!=a) {
		ok=true;
		return true;
	}
	if (b==true && a.value=="") {
		ok=true;
		return true;
	}
	var str=a.value;
	str=fcremoveChar(str,",");
	if (fcisNumberM(a.value)) {
		if (a.value>0) {
			ok=true;
			//a.value=setNo(str);
			return true;
		}
	}
	alert ("Not a valid positive No.");
	a.focus();
	ok=false;
	//numberFormat(a.id);
	return false;
}
function fcsetColSum(aa,b) {
	document.getElementById(b).value=fcgetColSum(aa);
}
function fcgetColSum(aa) {
	var str="000000",sa=0,zz;
	for (i=0;i<100000;i++) {
		zz=str+i;
		a=document.getElementById(aa+zz.substring(zz.length-6,zz.length));
		if (a==null) 
			break;
		if (fcisNumberM(a.value)) {
			///sa=eval(a.value)+eval(sa);
			sa=parseFloat(a.value)+parseFloat(sa);
		}
	}
	return sa;
}
function fcreplace(c) {
	a=c.value;
	b=a.length;	
	z="";
	for(i=0;i<b;i++){
		if(a.charAt(i)=="'")
			z+="'";
		z+=a.charAt(i);	
	}	
	c.value=z;
}
 function fcclicked(cid){
	if(cid.options[0]!=null)
		a = cid.selectedIndex;
	
 }
function fccheckSpace(z,len){
	if (ok==false && eg!=a) {
		ok=true;
		return true;
	}
	org=z;
	if (org==null)
		return false;
	counter=0,lastIndex=0,startIndex=0,tmp="";
	while (counter!=-1)
	{
		counter=org.indexOf(" ",lastIndex);
		if (counter!=-1)
		{
			tmp+=org.substring(startIndex,counter);
			lastIndex=counter+1;
			startIndex=lastIndex;
		}
		else
		{
			tmp+=org.substring(startIndex,org.length);
		}
	}
	if (tmp.length!=len) {
		return false;
		ok=false;
		eg=a;
	}
	else {
		ok=true;
		return true;
	}
}
function fcsetColSumWL(aa,b) {
	//alert("aa : "+aa);
	//alert("b : "+b);
	document.getElementById(b).value=fcgetColSumWL(aa);
	//alert("End..");
}
function fcgetColSumWL(aa) {
	var str="000000",sa=0,zz,xz;
	for (i=0;i<100000;i++) {
		zz=str+i;
		xz=str+(i+1);
		if (document.getElementById(aa+xz.substring(xz.length-6,xz.length))==null)
			break;
		a=document.getElementById(aa+zz.substring(zz.length-6,zz.length));
		if (a==null) 
			break;
		if (fcisNumberM(a.value)){
			sa=eval(a.value)+eval(sa);
		}
	}
	return sa;
}
function fcisPositiveNumber(a,bbb) {
	if (bbb==true && a.value=="")
		return;
	org=a.value;
	org=removeChar(org,",");
	if (ok==false && eg!=a) {
		ok=true;
		return false;
	}
	/*
	if (fcisNull(org)) {
		alert("Not a Valid Number");
		a.focus();
		ok=false;
		eg=a;
		return false;
	}*/
	while (org.charAt(0)=="0") {
		if (org.length>1)
			org=org.substring(1);
	}
	if (isNaN(org)==true || org<1) { 
		alert("Not a Valid Positive Number");
		a.focus();
		ok=false;
		eg=a;
		return false;
	} 
	ok=true;
	a.value=org;
	//a.value=setNo(org);
	return true;
}
function fccheckDateInRange(minDate,maxDate,chkDate) {
	
	var r=fcgetMaxDateM(chkDate,maxDate);
	
	if(r==2)
	{
		r=fcgetMaxDateM(minDate,chkDate);
		if(r==2)
			return true;
		else
			return false;
	}
	else
		return false;
 }


	/*******************************************************************************
		This Function Will return Ture If baseValue starts With the contents 
		of value
	******************************************************************************/

function fcstartsWith(value,baseValue)
{
	l=value.length;
	tmp=baseValue.substring(0,l);
	if(value==tmp)
		return true;
	else
		return false;
 }


	/*******************************************************************************
		This Function Will UnHide The Lister and Call The filterCustomCombo Function 
		To Insert data Into Lister
	******************************************************************************/
	
function fcunhide(textField,arraydata,valueField,top,left,listClass)
{
	layer="\n<DIV id=\"opt\" class=listClass style=\"Z-INDEX: 1;Top:"+top+"px; Left:"+left+"px; VISIBILITY: hidden; WIDTH: 205px; POSITION: absolute; \"><SELECT onkeypress= \"hide(this,'"+textField.id+"','"+valueField+"')\"  onclick=\"hide(this,'"+textField.id+"','"+valueField+"')\"  onmouseout=\"hide(this,'"+textField.id+"','"+valueField+"')\"  size=\"4\" name=\"lister\" id=\"lister\"  style=\"BORDER-BOTTOM: none; BORDER-LEFT: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; FONT-FAMILY: tahoma,sans-serif; FONT-SIZE: 11px; BACKGROUND-COLOR: lightblue; LEFT: 0px; VISIBILITY: hidden; WIDTH: 205px; TOP: 0px; HEIGHT: 75px\"> </SELECT></div>"; 
	innercode=document.myForm.innerHTML;
	document.myForm.innerHTML=innercode+layer;
	ref_layer=document.getElementById("opt");
	ref_layer.style.visibility="visible";

	ref_lister=document.getElementById("lister");
	ref_lister.size='2';
	ref_lister.style.visibility="visible";
	fcfilterCustomCombo(textField,arraydata);

	ref_lister.focus();		 
 }
	
	/*******************************************************************************
		This Function Will Hide The Lister and Insert The Selection Into TextBox And Hidden Field
	******************************************************************************/
	
function fchide(lister,descriptionField,valueFieldName)
{
	//alert(fld.value);
	if(lister.value!='')
		document.getElementById(descriptionField).value=lister.options[lister.selectedIndex].text;
		
	document.getElementById(valueFieldName).value=lister.value;
	ref_layer=document.getElementById("lister");
	ref_layer.style.visibility="hidden";
	//alert(document.getElementById(valueFieldName).value);
 	document.getElementById('b').focus();
 }


	/*******************************************************************************
		This Function Will Filter The Data And Insert The Data In Lister
	******************************************************************************/

function fcfilterCustomCombo(textField,pvccode){
	txt_value=textField.valu ;
	
	lister_ref=document.getElementById("lister");

//****************This Bock Of Code will Empty The ComboBox**************
	combolength=lister_ref.options.length;
	//alert("before loop=="+combolength);
	a=0;
	do{
		lister_ref.options[a]=null;
		combolength--;
  	   }while(combolength>0);	
		//alert("after loop=="+lister_ref.options.length);
//******************************************************************************
	count=0;
	if(txt_value==='')
	{
		for(i=pvccode.length-1;i>=0;i--)
		{
			splitted_data=pvccode[i].split("~");
			lister_ref.options[i]=new Option(splitted_data[1],splitted_data[0]);
		}
	}
	else if(txt_value.charAt(0)==='%'  && txt_value.charAt(txt_value.length-1)==='%' )
	{
		for(i=0;i<pvccode.length;i++)
		{
			splitted_data=pvccode[i].split("~").valueOf();
			if(splitted_data[1].toString().toUpperCase().indexOf(txt_valu .substring(1,txt_value.length-1).toUpperCase())!=-1 )
			{
				//alert(splitted_data[0]+"=="+splitted_data[1]);
			   lister_ref.options[count++]=new Option(splitted_data[1].valueOf(),splitted_data[0].valueOf());
			}	
		}
	 }
	 else if(txt_value.charAt(0)!=='%' && txt_value.charAt(txt_value.length-1)!=='%')
	 {
		//alert("normal");alert("combolength=>"+lister_ref.options.length);
		for(i=0;i<pvccode.length;i++)
		{
			splitted_data=pvccode[i].split("~").valueOf();//alert(splitted_data[0]+"=="+splitted_data[1]);
			if(startsWith(txt_value.toUpperCase(),splitted_data[1].toUpperCase()))
			{ 
			   //alert(splitted_data[0]+"=="+splitted_data[1]);
			   lister_ref.options[count++]=new Option(splitted_data[1].valueOf(),splitted_data[0].valueOf());
			}	
		 }


	  }
	  else if(txt_value.charAt(0)!=='%' && txt_value.charAt(txt_value.length-1)==='%')
	  {
		//alert("ends with any alphabets");
		}
	  else
	  {
	 	//alert("starts from any alphabets");	 
	   }
	//alert(count+"===="+lister_ref.options.length);
   }
  function eventClick(fld)
    {
  	var cmp=null;
  	for(i=0;i<totalRecords;i++)
  	{
  		id='00000'+i;
  		id=id.substring(id.length-6);
  		id='trrow'+id;
  		row=document.getElementById(id);
  		//alert(row);
  		if(row==null)continue;

		//there is no need to change background of tr
  		tds2=row.children;
  		for(j=0;j<tds2.length;j++)
  		{
  			cmp=tds2[j].children;
  			if(cmp.length<1)continue;
  			if(cmp[0].type==="button" || cmp[0].type==="BUTTON")
  			{
  				cmp[0].runtimeStyle.backgroundColor = '#b6e7ef';//this is button colour
  			}
  			else
  			{	
				if(i+1==totalRecords)
				{
					cmp[0].runtimeStyle.backgroundColor = 'white'; //'#f0e6d5';
				}
				else
					cmp[0].runtimeStyle.backgroundColor = 'white'; //'#eadbc4';//this is default color of text
  			}

			if(i+1==totalRecords)
			{
				tds2[j].runtimeStyle.backgroundColor ='white'; //'#f0e6d5';
			}
			else 			
  				tds2[j].runtimeStyle.backgroundColor = 'white'; //'#eadbc4';	
  		}
  	}
  
  	tds1=fld.children;
  	//fld.runtimeStyle.backgroundColor = '#eadbc4';
  	for(i=0;i<tds1.length;i++)
  	{
  	   component=tds1[i].children;
  	   if(component.length<1)continue;
  	   component[0].runtimeStyle.backgroundColor = 'lightblue';  //'#b6e7ef';//'#99CCFF';//'#dec0a1';
  	   component[0].runtimeStyle.fontColor = 'blue';
  	   tds1[i].runtimeStyle.backgroundColor = 'lightblue';//'#b6e7ef';//'#99CCFF';//dark brown to be highlited
  	}
	fcsetDeletedBGColor();
}

var obj_DeletedCheckBoxRef=new Array();

function fcsetDeletedBGColor() {
	for (j=0;j<obj_DeletedCheckBoxRef.length;j++) {
		if (obj_DeletedCheckBoxRef[j]==null || obj_DeletedCheckBoxRef[j]=="null")
			continue;
	  	tds1=document.getElementById("trrow"+obj_DeletedCheckBoxRef[j].substring(7)).children;
		document.getElementById(obj_DeletedCheckBoxRef[j]).style.backgroundColor="lightyellow";
  		for(i=0;i<tds1.length;i++)
	  	{
  		   component=tds1[i].children;
	  	   if(component.length<1)
			continue;
  		   component[0].runtimeStyle.backgroundColor = 'lightyellow';  //'#b6e7ef';//'#99CCFF';//'#dec0a1';
	  	   component[0].runtimeStyle.fontColor = 'lightyellow';
  		   tds1[i].runtimeStyle.backgroundColor = 'lightyellow';//'#b6e7ef';//'#99CCFF';//dark brown to be highlited
	  	}
	}
}
function fcsubmitForm() {
	window.myForm.action="";
	window.myForm.submit();
}
function fcsubmitProcess(str_CommandName) {
	document.myForm.ft.value=str_CommandName;
	var str_URL="../servlet/shgl.SHGLGLController";
	document.myForm.action=str_URL;
	document.myForm.submit();
}
function fcsubmitGenProcess() {
	var str_URL="../servlet/shgn.SHGNController";
	document.myForm.action=str_URL;
	document.myForm.submit();
}
function fcsetDataListerSelectedRowColor() {
	var bln_IsCurrRow=false;
	for (i=0;i<100000;i++) {
		var objRef=document.getElementById("row"+i);
		if (objRef==null)
			return;
		bln_IsCurrRow=true;
		for (j=0;j<100000;j++) {
			var objColRef=document.getElementById("row"+i+"col"+j);
			if (objColRef==null)
				break;

			var txt_Obj = document.getElementById("text"+j);
			if(txt_Obj==null) break;

			var txt_ObjName=document.getElementById("text"+j).name;
			if (txt_ObjName.substring(txt_ObjName.length-7)!="_pk__m_" && txt_ObjName.substring(txt_ObjName.length-8)!="_Tok__m_")
				continue;
			if (document.getElementById("text"+j).value!=trim(objColRef.innerText)) {
				bln_IsCurrRow=false;
				break;
			}
		}
		if (bln_IsCurrRow==true) {
			objRef.style.backgroundColor="lightyellow";
			return;
		}
	}
}
function trim(str_Value) {
	str_Value=rtrim(str_Value);
	str_Value=ltrim(str_Value);
	return str_Value;
}
function rtrim(str_Value) {
	if (str_Value.length<1)
		return str_Value;
	while (str_Value.charAt(0)==" ") {
		str_Value=str_Value.substring(1);
	}
	return str_Value;
}
function ltrim(str_Value) {
	if (str_Value.length<1)
		return str_Value;
	while (str_Value.charAt(str_Value.length-1)==" ") {
		str_Value=str_Value.substring(0,str_Value.length-1);
	}
	return str_Value;
}
function fcsetDisabledTabular(str_CompName,bln_Disabled) {
	
	for (i=0;i<totalRecords-1;i++) {
		var str_ID="000000"+i;
		str_ID=str_ID.substring(str_ID.length-6);
		document.getElementById(str_CompName+str_ID).disabled=bln_Disabled;
	}
}
function setDisplayTabular(str_CompName,bln_Hide, int_ColNo) 
{
      try {	
	for (i=0;i<totalRecords;i++) {
		var str_ID="000000"+i;
		str_ID=str_ID.substring(str_ID.length-6);
		document.getElementById(str_CompName+str_ID).parentElement.style.display=(bln_Hide==true?"none":"inline");
	}
	document.getElementById("title"+int_ColNo).style.display=(bln_Hide==true?"none":"inline");
      } catch(e) {
      }
}
function fcsetDisabledTabularAll(str_CompName,bln_Disabled) {
	
	for (i=0;i<10000;i++) {
		var str_ID="000000"+i;
		str_ID=str_ID.substring(str_ID.length-6);
		if (document.getElementById(str_CompName+str_ID)==null)
			break;
		document.getElementById(str_CompName+str_ID).disabled=bln_Disabled;
	}
}
function fcsetReadonlyTabular(str_CompName,bln_ReadOnly) {
	
	for (i=0;i<totalRecords-1;i++) {
		var str_ID="000000"+i;
		str_ID=str_ID.substring(str_ID.length-6);
		document.getElementById(str_CompName+str_ID).readOnly=bln_ReadOnly;
	}
}
function fcchangeFormat(sourceDate,sourceFormat,targetFormat,sourceSep,targetSep)
{
	var str_targetFormatDate="";
	var data = sourceDate.split(sourceSep);	
	var day,mon,year;
	if((sourceFormat=="dd/mm/yyyy") || (sourceFormat=="DD/MM/YYYY"))
	{
		day=data[0];
		mon=data[1];
		year=data[2];
	}

	//************************************************************
	//
	//		Start of Code Added By AQ on 06/02/2004	
	//
	//************************************************************
	if((sourceFormat=="yyyy-mm-dd") || (sourceFormat=="YYYY-MM-DD"))
	{
		day=data[2];
		mon=data[1];
		year=data[0];
	}

	if((targetFormat=="dd/mm/yyyy") || (sourceFormat=="DD/MM/YYYY"))
	{
		str_targetFormatDate=day+targetSep+mon+targetSep+year;
	}
	//************************************************************
	//
	//		End of Code Added By AQ on 06/02/2004	
	//
	//************************************************************


	
	
	if((targetFormat=="mm/dd/yyyy") || (sourceFormat=="MM/DD/YYYY"))
	{
		str_targetFormatDate=mon+targetSep+day+targetSep+year;
	}

	return str_targetFormatDate;
}




function fcvalidateForm() {
	for (i=0;i<str_RequiredFieldsArray.length;i++) {
		if (!fcvalidate(document.getElementById(str_RequiredFieldsArray[i]),'C',true)) {
			return false;
		}
	}
	return true;
}
function fcvalidateTabularForm() {
	for (j=0;j<totalRecords-1;j++) {
		str_Id="000000"+j;
		str_Id=str_Id.substring(str_Id.length-6);
		for (i=0;i<str_RequiredFieldsArray.length;i++) {
			if (!fcvalidate(document.getElementById(str_RequiredFieldsArray[i]+str_Id),'C',true)) {
				return false;
			}
		}
	}
	return true;
}
function fccheckDefault(obj_Ref,obj_ArrayRef) {
	if (obj_ArrayRef.length>0 && obj_Ref.value=="Y") {
		alert("Only One Record can be saved as default");
		obj_Ref.value="N";
	}
}
function checkDuplicate(obj_Ref,obj_ArrayRef,str_Message) {
	for (i=0;i<obj_ArrayRef.length;i++) {
		if (obj_ArrayRef[i]==obj_Ref.value) {
			alert((str_Message==null || str_Message==""?"Duplicate Value":str_Message));
			if (obj_Ref.type!="text")
				obj_Ref.selectedIndex=-1;
			else
				obj_Ref.value="";
			obj_Ref.focus();
			return;
		}
	}
}
function checkDuplicateTwo(str_Id1,str_Id2,obj_ArrayRef,str_Message) {
	var str_Value=document.getElementById(str_Id1).value+"~"+document.getElementById(str_Id2).value;
	for (i=0;i<obj_ArrayRef.length;i++) {
		if (obj_ArrayRef[i]==str_Value) {
			alert((str_Message==null || str_Message==""?"Duplicate Value":str_Message));
			if (document.getElementById(str_Id2).type!="text")
				document.getElementById(str_Id2).selectedIndex=-1;
			else
				obj_Ref.value="";
			document.getElementById(str_Id2).focus();
			return;
		}
	}
}
function setComboText(z,vl)  { 
	if (vl=="" || vl==null)
		return;
 	for (a=0;a<z.length;a++) 
 	{ 	
 		if (z.options[a].text.substring(0,vl.length).toUpperCase()===vl.toUpperCase()) 
 		{ 	
 			z.options[a].selected=true; 
 			break; 
 		} 
 	}
 	z.focus();
	if (z.onchange!=null)
		z.onchange();
}
	function removeCommas(id){
		num=document.getElementById(id).value;
		var pos1 = 0
		var pos2 = num.indexOf(",")
		var result=""
		var temp
		while(pos2 != -1 ){
			temp = num.substring(pos1,pos2)
			result = result + temp
			pos1 = pos2+1
			pos2 = num.indexOf(",",pos1)
		}
		if(pos1 < num.length){
			temp = num.substring(pos1,num.length)
			if(!isNaN(temp)){
				result = result + temp
			}
		}
		document.getElementById(id).value=result;
		return result
	}

	function numberFormat(id){
		//return document.getElementById(id).value;
		//num = removeCommas(num)
		num=document.getElementById(id).value;
		removeCommas(id)
		var desc= ''
		var pos1
		var result
		var temp
		pos1 = num.indexOf(".")
		if(pos1 != -1) {
			desc = num.substring(pos1, num.length)
			num = num.substring(0, pos1)
		}
		result = desc
		var count=0
		while(num.length > 0){
			if(num.length <= 3){
				if(count == 0){
					result = num + result
				}
				else{
					result = num + "," + result
				}
				break
			}
			else{
				temp = num.substr(num.length-3,3)
				num = num.substring(0, num.length-3)
				if(count == 0){
					result = temp + result
				}
				else{
					result = temp + "," + result
				}
			}
			count = count + 1
		}
	document.getElementById(id).value = result
	return result
}
function removeAllFormComa() {
 	for (i=0;i<1000;i++) { 
 		a=document.getElementById("text"+i); 
 		if (a==null)  
 			break; 
		if (a.type=="text" && a.name.length>10 && (a.name.substring(a.name.length-10)=="_n__pk__m_" || a.name.substring(a.name.length-6)=="_n__m_")) {
 			removeCommas(a.id)
		}
 	} 
}
function numberFormatAllForm() {
 	for (i=0;i<1000;i++) { 
 		a=document.getElementById("text"+i); 
 		if (a==null)  
 			break; 
		if (a.type=="text" && a.name.length>10 && (a.name.substring(a.name.length-10)=="_n__pk__m_" || a.name.substring(a.name.length-6)=="_n__m_")) {
 			numberFormat(a.id)
		}
 	} 
}
function removeAllComaFromTabForm() {
	var str_Id="";
	for (j=0;j<str_NumericFieldsName.length;j++) {
	 	for (i=0;i<10000;i++) { 
			str_Id="000000"+i;
			str_Id=str_Id.substring(str_Id.length-6);
 			a=document.getElementById(str_NumericFieldsName[j]+str_Id); 
 			if (a==null)  
	 			break; 
			removeCommas(a.id)
 		} 
	}
}
function numberFormatInAllTabForm() {
	var str_Id="";
	for (j=0;j<str_NumericFieldsName.length;j++) {
	 	for (i=0;i<10000;i++) { 
			str_Id="000000"+i;
			str_Id=str_Id.substring(str_Id.length-6);
 			a=document.getElementById(str_NumericFieldsName[j]+str_Id); 
 			if (a==null)  
	 			break; 
 			numberFormat(a.id)
 		} 
	}
}
function setLister(int_Size) {
	Lister.style.width=(eval(Lister.style.width.substring(0,Lister.style.width.length-2))-int_Size)+"px";
	Main.style.left=(eval(Main.style.left.substring(0,Main.style.left.length-2))-int_Size)+"px";
}

//******************************************************************************
//	Author : AQ
//	Date   : 08/01/2004
//	Purpose: This function is used to control the display of components
//		 on the basis of some value
//
//******************************************************************************
function fcmanageCol(cboVal,arr_Com,frameNo)
{
	var orgVal = trim(cboVal);
	if(orgVal.length==0) return;
	var bln_cboValueExist=fccheckCboValueExist(cboVal,arr_Com,frameNo);
//	alert('');
	if(bln_cboValueExist)
	{
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			if(row==null) continue;
	
			if(str_data[0]==cboVal)
			{
		
				if(str_data[2]=="N")
				{
					if (row==null)
						continue;
					row.style.display="none";
				}
				else
				{
					if (row==null)
						continue;
					row.style.display="inline";
				}
	
			}
	
		}
	}
	else
	{
	
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			if (row==null)
				continue;
			row.style.display="inline";
		}
	
	}
	
}
function fcmanageCol(cboVal,arr_Com)
{
	var orgVal = trim(cboVal);
	if(orgVal.length==0) return;


	var bln_cboValueExist=fccheckCboValueExist(cboVal,arr_Com);
	//alert("bln_cboValueExist : "+bln_cboValueExist);

	if(bln_cboValueExist)
	{
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			//alert("str_data : "+str_data);
			//alert("Row Dat :"+parent.frames[frameNo].str_data[1]);
			var row = document.getElementById("row"+str_data[1]);
			//alert("i :row : "+i+":"+row);
			if(row==null) continue;
			if(str_data[0]==cboVal)
			{
		
				if(str_data[2]=="N")
				{
					row.style.display="none";
				}
				else
				{
					row.style.display="inline";
				}
	
			}
	
		}
	}
	else
	{
	
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			var row = document.getElementById("row"+str_data[1]);
			if (row==null)
				continue;
			
			row.style.display="inline";
		}
	
	}
	
}


function fccheckCboValueExist(cboVal,arr_Com,frameNo)
{
	var bln_cboValueExist=false;
	for(i=0;i<arr_Com.length;i++)
	{
		var str_data=arr_Com[i].split("~");
		if(str_data[0]==cboVal)
		{
			bln_cboValueExist=true;
			break;
		}

	}

	return bln_cboValueExist;
}

function fccheckCboValueExist(cboVal,arr_Com)
{
	var bln_cboValueExist=false;
	for(i=0;i<arr_Com.length;i++)
	{
		var str_data=arr_Com[i].split("~");
		if(str_data[0]==cboVal)
		{
			bln_cboValueExist=true;
			break;
		}

	}

	return bln_cboValueExist;
}


function fcmanageMultiColGen(cboVal,arr_Com,frameNo,isMove)
{
	var orgVal = trim(cboVal);
	if(orgVal.length==0) return;


	var bln_cboValueExist=fccheckCboValueExist(cboVal,arr_Com,frameNo);
	//alert("bln_cboValueExist : "+bln_cboValueExist);
	
	if(bln_cboValueExist)
	{
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			//var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			var col1 = parent.frames[frameNo].document.getElementById("col1"+str_data[1]);
			var col2= parent.frames[frameNo].document.getElementById("col2"+str_data[1]);
			
	
			if(str_data[0]==cboVal)
			{
		
				if(str_data[2]=="N")
				{
					//row.style.display="none";
					if(isMove)
					{
						fcsetColumnMove(col1,col2,false);			
					}
					else
					{
						fcsetColumnFix(col1,col2,false);
					}
					
				}
				else
				{
					//row.style.display="inline";
					if(isMove)
					{
						fcsetColumnMove(col1,col2,true);			
					}
					else
					{
						fcsetColumnFix(col1,col2,true);
					}

					
				}
	
			}
	
		}
	}
	else
	{
	
		for(i=0;i<arr_Com.length;i++)
		{
			var str_data=arr_Com[i].split("~");
			//var row = parent.frames[frameNo].document.getElementById("row"+str_data[1]);
			var col1 = parent.frames[frameNo].document.getElementById("col1"+str_data[1]);
			var col2 = parent.frames[frameNo].document.getElementById("col2"+str_data[1]);
			
			//row.style.display="inline";

			if(isMove)
			{
				fcsetColumnMove(col1,col2,true);			
			}
			else
			{
				fcsetColumnFix(col1,col2,true);
			}
		}
	
	}
	
}


function fcsetColumnMove(objLabel,objText,isDisplay)
{
	if(isDisplay)
	{
		objLabel.style.display="inline";
		objText.style.display="inline";
	}
	else
	{
		objLabel.style.display="none";
		objText.style.display="none";
	}

}

function fcsetColumnFix(objLabel,objText,isDisplay)
{
	if(isDisplay)
	{
		objLabel.style.visibility="VISIBLE";
		objText.style.visibility="VISIBLE";
	}
	else
	{
		objLabel.style.visibility="HIDDEN";
		objText.style.visibility="HIDDEN";
	}

}



//******************************************************************************
//	
//				End
//
//******************************************************************************
function fccallOtherFuncsAfterFillingCombo(str_ComboName,str_ArrayName){
	//alert(str_ComboName.name);
}
function fetchData()
{
	document.frm_FetchData.action="../servlet/shgn.SHGNFetchData";
	document.frm_FetchData.target=("RemoteComboIFrame"+int_CurrentIFrame);
	int_CurrentIFrame++;
	if (int_CurrentIFrame>8)
		int_CurrentIFrame=1;
	document.frm_FetchData.submit();
}

function fillData(str_Fields, str_Values)
{
	for(var i=0; i<=1000; i++)
      	{
		if (setDataInForm(str_Fields, str_Values, i)==false)
			break;
      	}
	for(var i=200; i<=1000; i++)
      	{
		if (setDataInForm(str_Fields, str_Values, i)==false)
			break;
      	}
}
function setDataInForm(str_Fields, str_Values, int_Index) {
	var id = document.getElementById("text"+int_Index);
	if(id==null) {
		return false;
	}
	var totalFields = str_Fields.length;
	var fieldCounter=0;
	var name = document.getElementById("text"+int_Index).name;
	for(var j=0; j<str_Fields.length; j++) {
		if(name.length >= str_Fields[j].length) {
	          		if(str_Fields[j] == name.substring(0,str_Fields[j].length)) {
      				id.value=(str_Values[j]==null || str_Values[j]=="null"?"":str_Values[j]);
	      			break;
	    		}
 		}
       	}
	return true;
}
function fcisNotNullFieldEmpty(notNullField,columnIndex,lastRowId)
{
	//alert("columnIndex : "+columnIndex);
	//alert("notNullField : "+notNullField);
	//alert("lastRowId : "+lastRowId);
	
	var notFieldObjVal = document.getElementById(columnIndex+notNullField+lastRowId).value;
	//alert("notFieldObjVal : "+notFieldObjVal);
	var finalObjVal = trim(notFieldObjVal);
	if(finalObjVal.length==0)
	{
		//alert("False");
		return true;
	}
	else
		return false;
}

function fcchangeNewRowStatus(lastRowId,status)
{
	document.getElementById("change"+lastRowId).value=status;
}

function fchandleNewRowStatus(notNullField,columnIndex,lastRowId)
{
	if(fcisNotNullFieldEmpty(notNullField,columnIndex,lastRowId))
	{
		fcchangeNewRowStatus(lastRowId,false);
	}
	else
	{
		fcchangeNewRowStatus(lastRowId,"new");
	}
	
}
function eventClick1(obj_Ref) {
}
function setButtonDisabled(obj_Component,bln_Disabled) {
     try {
	if (obj_Component==null || obj_Component==undefined)
		return;
	//To be uncomented, when using security manager buttons
	/*var bln_Found=false;
	for (i=0;i<parent.frames["buttonFrame"].str_AuthButtons.length;i++) {
		if (obj_Component.name==parent.frames["buttonFrame"].str_AuthButtons[i]) {
			bln_Found=true;
			break;
		}
	}
	if (!bln_Found)
		return;
	*/
	obj_Component.disabled=bln_Disabled;	//cmd_Save	btn_cmd_Save
	parent.frames["buttonFrame"].document.getElementById("Col_"+obj_Component.id).disabled=bln_Disabled;
	parent.frames["buttonFrame"].document.getElementById("btn_"+obj_Component.id).disabled=bln_Disabled;
     } catch(e) {
	alert(e);
     }
}
function round(no,decPlace)
{
	var str_No = no;
	//alert("str_No:"+str_No);
	var rNo=0;
	var mFac=Math.pow(10,decPlace);
	//alert("mFac:"+mFac);
	rNo = no*mFac;
	//alert("1.rNo:"+rNo);
	rNo = Math.round(rNo);
	//alert("2.rNo:"+rNo);
	rNo = rNo/mFac;
	//alert("3.rNo:"+rNo);

	return rNo;
}
function convertDate(source_date,targetFormat)
{
	//source date format is 2003-10-15(yyyy-mm-dd)
	var day=source_date.substring(source_date.lastIndexOf("-")+1);
	var mon=source_date.substring(source_date.indexOf("-")+1,source_date.lastIndexOf("-"));
	var year=source_date.substring(0,source_date.indexOf("-"));
	var retDate="";
	if(targetFormat=="dd/mm/yyyy")
	{
		retDate=day+"/"+mon+"/"+year;
	}

	return retDate;
}

function setDecimal(obj)
{
	var amt = obj.value;
	if((amt.length>0) && (amt.indexOf(".")==-1))
	{
		//alert("parseFloat(amt):"+parseFloat(amt));
		obj.value=parseFloat(amt)+".0";
	}
}

//*************************************************************************
//
//	The following function is added to support hiding of auto no field 
//
//*************************************************************************
function fcsetAutoNoFieldStatus(INFO,typeId,mode)
{
	var arr_INFO = INFO;
	//var arr_INFO = parent.frames[1].VOUCHER_INFO;
	//var str_Type = document.getElementById("text3").value;
	var str_Type = document.getElementById(typeId).value;
	
	//alert("arr_INFO.length:"+arr_INFO.length);
	try
	{


		for(i=0;i<arr_INFO.length;i++)
		{
			//alert("arr_INFO["+i+"]:"+arr_INFO[i]);
			//if(arr_INFO[i]==null) continue;
			
			var str_ArrayType = arr_INFO[i].substring(0,arr_INFO[i].indexOf("~"));
			var str_AM = arr_INFO[i].substring(arr_INFO[i].indexOf("~")+1);
			//alert("str_ArrayType:"+str_ArrayType);
			//alert("str_AM:"+str_AM);
			//if((str_ArrayType==str_VchType) && ((str_AM=="M") || (str_AM=="m") ))
			
			if((str_ArrayType==str_Type) && ((str_AM=="A") || (str_AM=="a") ))
			{
				if(mode==="ADD_MODE")	
				{
					fchideAutoNuFieldText();
					break;
				}
			}
		
		}
	}
	catch(e)
	{
		//alert("Exc.."+e.messageText);
	}
	
}
function replaceAll(str,f,r) {
	while (str.indexOf(f)!=-1)
		str=str.replace(f,r);
	return str;
}
function checkDuplicateRows3(x1,x2,x3,val) {
	var str1,str2,str3,str4,str5,str6,str,ls1,ls2,ls3;
	for (k=0;k<1000000;k++) {
		ls="000000"+k;
		ls=ls.substring(ls.length-6,ls.length);
		str1=document.getElementById(x1+ls);
		str2=document.getElementById(x2+ls);
		str3=document.getElementById(x3+ls);

		ls3="000000"+(k+1);
		ls3=ls3.substring(ls3.length-6,ls3.length);
		if (str1==null){
			break;
		}
		if (document.getElementById(x1+ls3)==null && document.getElementById(val+ls).value==""){
			break;
		}
		for (j=k+1;j<1000000;j++) {
			ls1="000000"+j;
			ls1=ls1.substring(ls1.length-6,ls1.length);
			ls2="000000"+(j+1);
			ls2=ls2.substring(ls2.length-6,ls2.length);
			str4=document.getElementById(x1+ls1);
			str5=document.getElementById(x2+ls1);
			str6=document.getElementById(x3+ls1);
			if (str4==null){
				break;
			}
			if (document.getElementById(x1+ls2)==null  && document.getElementById(val+ls1).value=="") {
				break;
			}
			if (((str1.value+str2.value+str3.value)==(str4.value+str5.value+str6.value)) && ((str4.value+str5.value+str6.value)!=null || (str4.value+str5.value+str6.value)!=""))
				return false;
		}
	}
	return true;
}
function checkDuplicateRows2(x1,x2,val) {
	var str1,str2,str4,str5,str,ls1,ls2,ls3;
	for (k=0;k<1000000;k++) {
		ls="000000"+k;
		ls=ls.substring(ls.length-6,ls.length);
		str1=document.getElementById(x1+ls);
		str2=document.getElementById(x2+ls);

		ls3="000000"+(k+1);
		ls3=ls3.substring(ls3.length-6,ls3.length);
		if (str1==null){
			break;
		}
		if (document.getElementById(x1+ls3)==null && document.getElementById(val+ls).value==""){
			break;
		}
		for (j=k+1;j<1000000;j++) {
			ls1="000000"+j;
			ls1=ls1.substring(ls1.length-6,ls1.length);
			ls2="000000"+(j+1);
			ls2=ls2.substring(ls2.length-6,ls2.length);
			str4=document.getElementById(x1+ls1);
			str5=document.getElementById(x2+ls1);
			if (str4==null){
				break;
			}
			if (document.getElementById(x1+ls2)==null  && document.getElementById(val+ls1).value=="") {
				break;
			}
			if ((str1.value+str2.value)==(str4.value+str5.value))
				return false;
		}
	}
	return true;
}
function checkDuplicateRows1(x1,val) {
	var str1,str4,str,ls1,ls2,ls3;
	for (k=0;k<1000000;k++) {
		ls="000000"+k;
		ls=ls.substring(ls.length-6,ls.length);
		str1=document.getElementById(x1+ls);
		ls3="000000"+(k+1);
		ls3=ls3.substring(ls3.length-6,ls3.length);
		if (str1==null){
			break;
		}
		if (document.getElementById(x1+ls3)==null && document.getElementById(val+ls).value==""){
			break;
		}
		for (j=k+1;j<1000000;j++) {
			ls1="000000"+j;
			ls1=ls1.substring(ls1.length-6,ls1.length);
			ls2="000000"+(j+1);
			ls2=ls2.substring(ls2.length-6,ls2.length);
			str4=document.getElementById(x1+ls1);
			if (str4==null){
				break;
			}
			if (document.getElementById(x1+ls2)==null  && document.getElementById(val+ls1).value=="") {
				break;
			}
			if (str1.value==str4.value)
				return false;
		}
	}
	return true;
}
function checkDuplicateRows4(x1,x2,x3,x4,val) {
	var str1,str2,str3,str4,str5,str6,str7,str8,str,ls1,ls2,ls3;
	for (k=0;k<1000000;k++) {
		ls="000000"+k;
		ls=ls.substring(ls.length-6,ls.length);
		str1=document.getElementById(x1+ls);
		str2=document.getElementById(x2+ls);
		str3=document.getElementById(x3+ls);
		str4=document.getElementById(x4+ls);

		ls3="000000"+(k+1);
		ls3=ls3.substring(ls3.length-6,ls3.length);
		if (str1==null){
			break;
		}
		if (document.getElementById(x1+ls3)==null && document.getElementById(val+ls).value==""){
			break;
		}
		for (j=k+1;j<1000000;j++) {
			ls1="000000"+j;
			ls1=ls1.substring(ls1.length-6,ls1.length);
			ls2="000000"+(j+1);
			ls2=ls2.substring(ls2.length-6,ls2.length);
			str5=document.getElementById(x1+ls1);
			str6=document.getElementById(x2+ls1);
			str7=document.getElementById(x3+ls1);
			str8=document.getElementById(x4+ls1);
			if (str5==null){
				break;
			}
			if (document.getElementById(x1+ls2)==null  && document.getElementById(val+ls1).value=="") {
				break;
			}
			if ((str1.value+str2.value+str3.value+str4.value)==(str5.value+str6.value+str7.value+str8.value))
				return false;
		}
	}
	return true;
}
function checkDuplicateRows5(x1,x2,x3,x4,val) {
	var str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str,ls1,ls2,ls3;
	for (k=0;k<1000000;k++) {
		ls="000000"+k;
		ls=ls.substring(ls.length-6,ls.length);
		str1=document.getElementById(x1+ls);
		str2=document.getElementById(x2+ls);
		str3=document.getElementById(x3+ls);
		str4=document.getElementById(x4+ls);
		str5=document.getElementById(val+ls);

		ls3="000000"+(k+1);
		ls3=ls3.substring(ls3.length-6,ls3.length);
		if (str1==null){
			break;
		}
		if (document.getElementById(x1+ls3)==null && document.getElementById(val+ls).value==""){
			break;
		}

		for (j=k+1;j<1000000;j++) {
			ls1="000000"+j;
			ls1=ls1.substring(ls1.length-6,ls1.length);
			ls2="000000"+(j+1);
			ls2=ls2.substring(ls2.length-6,ls2.length);
			str6=document.getElementById(x1+ls1);
			str7=document.getElementById(x2+ls1);
			str8=document.getElementById(x3+ls1);
			str9=document.getElementById(x4+ls1);
			str10=document.getElementById(val+ls1);
			if (str6==null){
				break;
			}
/*			if (document.getElementById(x1+ls2)==null  && document.getElementById(val+ls1).value=="") {
				break;
			}*/
			if ((str1.value+str2.value+str3.value+str4.value+str5.value)==(str6.value+str7.value+str8.value+str9.value+str10.value))
				return false;
		}
	}
	return true;
}