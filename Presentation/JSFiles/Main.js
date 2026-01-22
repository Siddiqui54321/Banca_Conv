//alert('Main.js included');

/*
jQuery(document).ready(function ($) {
	"use strict";
	$('.scrollContainer').perfectScrollbar();
});
*/

var selectedPage;
var totalPages;


jQuery(document).ready(function($) {
    "use strict";

    $('.scrollContainer').perfectScrollbar();

    if ($("#maxPagerPages") != null) {
        totalPages = $("#maxPagerPages").val();
        selectedPage = $(".inputSelectedPage").val();

        if (totalPages == "0") {
            $(".inputSelectedPage").val("0");
        }
        $(".statusPagination").html(" of " + totalPages + " Pages");

        hideButtons();
    }
});


function submitPagiantionRequest(element, args) {
    __doPostBack(element, '')
}

function hideButtons() {
    if (parseInt(totalPages) == 1 || parseInt(selectedPage) == 1) {
        jQuery(".previousPagiantionButton").attr("disabled", "disabled");
        jQuery(".firstPagiantionButton").attr("disabled", "disabled");
    } else {
        jQuery(".previousPagiantionButton").removeAttr('disabled');
        jQuery(".firstPagiantionButton").removeAttr('disabled');
    }

    if (parseInt(totalPages) == parseInt(selectedPage)) {
        jQuery(".nextPagiantionButton").attr("disabled", "disabled");
        jQuery(".lastPagiantionButton").attr("disabled", "disabled");
    } else {
        jQuery(".nextPagiantionButton").removeAttr('disabled');
        jQuery(".lastPagiantionButton").removeAttr('disabled');
    }
}

function mappingPageChanged(element, operation) {
    hideButtons();
    if (operation == "F") {
        jQuery(".inputSelectedPage").val('1');
    }

    if (operation == "L") {
        jQuery(".inputSelectedPage").val(totalPages);
    }

    if (operation == "P") {
        jQuery(".inputSelectedPage").val(--selectedPage);
    }

    if (operation == "N") {
        jQuery(".inputSelectedPage").val(++selectedPage);
    }
    hideButtons();

    submitPagiantionRequest(jQuery(".inputSelectedPage").attr("id"), '');
}

function PagerSearch(e, element) {
    if (window.event) {
        if (window.event.keyCode == '13') {
            searchPage(element);
            return false;
        }
    }
    else if (e) {
        if (e.which == '13') {

            searchPage(element);
            e.preventDefault();
            return false;
        }
    }
}

function searchPage(element) {

    if (element.value > totalPages) {
        alert("Page Number Must be Less than " + (parseInt(totalPages) + 1));
        jQuery(".inputSelectedPage").val(selectedPage);
    } else if (element.value < 1) {

        alert("Page Number Must be between 1 and  " + totalPages);
        jQuery(".inputSelectedPage").val(selectedPage);

    } else {
        jQuery(".inputSelectedPage").val(element.value);
        submitPagiantionRequest(jQuery(".inputSelectedPage").attr("id"), '');
    }
}

function fcgetReportUrl()
{
    //return "../../../../SHMA-CR64/CrystalReports/CrystalReport.aspx";
    return "http://10.1.30.27/SHMA-CR64/CrystalReports/CrystalReport.aspx";
    //return "../../../../SHMA-CR4/CrystalReports/CrystalReport.aspx";
    //return "http://128.1.103.20/Rpt/CrystalReports/CrystalReport.aspx";

    // CrystalReports / CrystalReport.aspx
    //return "http://\ws020/Testing/Presentation/webform2.aspx";
}