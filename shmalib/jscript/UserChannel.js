/***************************** Events ********************************************/
function Channel_ChangeEvent(objChannel)
{
	filterChannelDetail(objChannel);
}

function ChannelDetail_ChangeEvent(objChannelDtl)
{
	filterChannelSubDetail(objChannelDtl);
}

function ChannelSubDetail_ChangeEvent(objChannelSubDtl)
{}

function Default_ChangeEvent(objDefault)
{}

/***************************** Supporting methods ********************************************/
function filterChannelDetail(objChannel)
{ 
    try {
        var str_QryCCD_CODE = "SELECT D.CCD_CODE || '-' ||CCD_DESCR,D.CCD_CODE  FROM CCD_CHANNELDETAIL D, CCH_CHANNEL C WHERE D.CCH_CODE=C.CCH_CODE AND C.CCH_CODE='" + objChannel.value + "'";
        fcfilterChildCombo(objChannel, str_QryCCD_CODE, getField("CCD_CODE_1").name);
        filterChannelSubDetail(getField("CCD_CODE_1"));
    } catch (e) {

    }
	
}

function filterChannelSubDetail(objChannelDetail)
{ 
    try {
        getTabularFieldByIndex(totalRecords, "CCH_CODE").value = getField("ddlCCH_CODE_1").value;
        getTabularFieldByIndex(totalRecords, "CCD_CODE").value = getField("ddlCCD_CODE_1").value;

        var str_QryCCS_CODE = "SELECT S.CCS_CODE || '-' || CCS_DESCR,S.CCS_CODE  FROM CCS_CHANLSUBDETL S, CCD_CHANNELDETAIL D WHERE S.CCD_CODE=D.CCD_CODE AND S.CCH_CODE=D.CCH_CODE AND S.CCH_CODE='" + getField("ddlCCH_CODE_1").value + "' AND S.CCD_CODE='" + objChannelDetail.value + "'";
        fcfilterChildCombo(objChannelDetail, str_QryCCS_CODE, getTabularFieldByIndex(totalRecords, "CCS_CODE").name);
	//getTabularFieldByIndex(totalRecords,"CCS_CODE").selectedIndex = 1;
    } catch (e) {

    }
	
}