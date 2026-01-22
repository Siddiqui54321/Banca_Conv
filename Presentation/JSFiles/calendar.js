/*
Jalali (Shamsi) Calendar Date Picker Version 1.00 (JavaScript)
-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Written By : Amin Habibi Shahri
E-mail : habibiamin@gmail.com
Homepage: http://habibiamin.googlepages.com
Patched by Navid Torabazary (Nov 14 2007)
--------------------------------------------------------------------
MODIFIED BY: W A S I M   U L L A H   K H A N
PURPOSE: HIJRI ARABIC CALENDAR
DATE: 31st December 2008
--------------------------------------------------------------------
*/

var datePickerDivID = "datepicker";
var iFrameDivID = "datepickeriframe";

// these variables define the date formatting we're expecting and outputting.
// If you want to use a different format by default, change the defaultDateSeparator
// and defaultDateFormat variables either here or on your HTML page.
var defaultDateSeparator = "/";        // common values would be "/" or "."
var defaultDateFormat = "dmy"    // valid values are "mdy", "dmy", and "ymd"
var dateSeparator = defaultDateSeparator;
var dateFormat = defaultDateFormat;

function displayDatePicker(dateFieldName,displayBelowThisObject, dtFormat, dtSep)
{
  var targetDateField = document.getElementsByName (dateFieldName).item(0);	
  // if we weren't told what node to display the datepicker beneath, just display it
  // beneath the date field we're updating
  if (!displayBelowThisObject)
    displayBelowThisObject = targetDateField; 
  // if a date separator character was given, update the dateSeparator variable
  if (dtSep)
    dateSeparator = dtSep;
  else
    dateSeparator = defaultDateSeparator; 
  // if a date format was given, update the dateFormat variable
  if (dtFormat)
    dateFormat = dtFormat;
  else
    dateFormat = defaultDateFormat;
 
  var x = displayBelowThisObject.offsetLeft;
  var y = displayBelowThisObject.offsetTop + displayBelowThisObject.offsetHeight ;
 
  // deal with elements inside tables and such
  var parent = displayBelowThisObject;
  while (parent.offsetParent) {
    parent = parent.offsetParent;
    x += parent.offsetLeft;
    y += parent.offsetTop ;
  }
 
  drawDatePicker(targetDateField, x, y);
  
}


/**
Draw the datepicker object (which is just a table with calendar elements) at the
specified x and y coordinates, using the targetDateField object as the input tag
that will ultimately be populated with a date.

This function will normally be called by the displayDatePicker function.
*/
function drawDatePicker(targetDateField, x, y)
{
  var dt = getFieldDate(targetDateField.value );
 
  // the datepicker table will be drawn inside of a <div> with an ID defined by the
  // global datePickerDivID variable. If such a div doesn't yet exist on the HTML
  // document we're working with, add one.
  if (!document.getElementById(datePickerDivID)) {
    // don't use innerHTML to update the body, because it can cause global variables
    // that are currently pointing to objects on the page to have bad references
    //document.body.innerHTML += "<div id='" + datePickerDivID + "' class='dpDiv'></div>";
    var newNode = document.createElement("div");
    newNode.setAttribute("id", datePickerDivID);
    newNode.setAttribute("class", "dpDiv");
    newNode.setAttribute("style", "visibility: hidden;");
    document.body.appendChild(newNode);
  }
 
  // move the datepicker div to the proper x,y coordinate and toggle the visiblity
  var pickerDiv = document.getElementById(datePickerDivID);
  pickerDiv.style.position = "absolute";
  pickerDiv.style.left = x + "px";
  pickerDiv.style.top = y + "px";
  pickerDiv.style.visibility = (pickerDiv.style.visibility == "visible" ? "hidden" : "visible");
  pickerDiv.style.display = (pickerDiv.style.display == "block" ? "none" : "block");
  pickerDiv.style.zIndex = 10000;
 
  // draw the datepicker table
  refreshDatePicker(targetDateField.name, dt[0], dt[1], dt[2]);
}


/**
This is the function that actually draws the datepicker calendar.
*/
function refreshDatePicker(dateFieldName, year, month, day)
{
  // if no arguments are passed, use today's date; otherwise, month and year
  // are required (if a day is passed, it will be highlighted later)
  //alert("Year:"+ year+ " Month:"+month+" Day:"+day);
  var thisDay = getTodayHijri();
  var weekday = (thisDay[3] - thisDay[2] + 1)%7;
  if(!day)
	  day=1;
  if (month >= 1 && year > 1200)
  {
     thisDay = getSelectedHijri(year,month,1);
     //alert("Year:"+ thisDay[0]+ " Month:"+thisDay[1]+" Day:"+thisDay[2] + " weekday: " + thisDay[3] + " len: " + thisDay[4]);
     weekday = thisDay[3];
     thisDay = new Array(year,month,day,weekday,thisDay[4]);
     thisDay[2] = 1;
  } 
  else 
  {
    	day = thisDay[2];
    	thisDay[2] = 1;
  }
 
  // the calendar will be drawn as a table
  // you can customize the table elements with a global CSS style sheet,
  // or by hardcoding style and formatting elements below
  var crlf = "\r\n";
  var TABLE = "<table cols=7 class='dpTable'>" + crlf;
  var xTABLE = "</table>" + crlf;
  var TR = "<tr class='dpTR'>";
  var TR_title = "<tr class='dpTitleTR'>";
  var TR_days = "<tr class='dpDayTR'>";
  var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
  var xTR = "</tr>" + crlf;
  var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver=' this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
  var TD_title = "<td colspan=3 class='dpTitleTD'>";
  var TD_buttons = "<td class='dpButtonTD'>";
  var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'>";
  var TD_days = "<td class='dpDayTD'>";
  var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
  var TD_hide = "<td class='' onclick='hidePicker();'>"
  var xTD = "</td>" + crlf;
  var DIV_title = "<div class='dpTitleText'>";
  var DIV_selected = "<div class='dpDayHighlight'>";
  var xDIV = "</div>";
 
  // start generating the code for the calendar table
  var html = TABLE;
    
  // this is the title bar, which displays the month and the buttons to
  // go back to a previous month or forward to the next month
  html += TR_title;
  html += TD_buttons + getButtonCodeYear(dateFieldName, thisDay, -1, "&lt;&lt;") + xTD;// Navid //
  html += TD_buttons + getButtonCode(dateFieldName, thisDay, -1, "&lt;") + xTD;
  html += TD_title + DIV_title + monthArrayLong[ thisDay[1] - 1 ]+ getYearUniCode(thisDay[0]) + xDIV + xTD;
  html += TD_buttons + getButtonCode(dateFieldName, thisDay, 1, "&gt;") + xTD ;
  html += TD_buttons + getButtonCodeYear(dateFieldName, thisDay, 1, "&gt;&gt;") + xTD ;// Navid //
  html += xTR;
 
  // this is the row that indicates which day of the week we're on
  html += TR_days;
  var i;
  for(i = dayArray.length-1; i>=0 ; i--)
    html += TD_days + dayArray[i] + xTD;
  html += xTR;
 
  // now we'll start populating the table with days of the month
  var dates = "";
    
  // first, the leading blanks
  if(weekday != 7)
    for (i = 0; i < weekday; i++)
        dates += TD_hide + "&nbsp;" + xTD;
 
  //now, the days of the month
  var len = thisDay[4];
  for(var dayNum = 1; dayNum <= len; dayNum++) 
  {
    TD_onclick = " onclick=\"updateDateField('" + dateFieldName + "', '" + getDateString(thisDay) + "');\">";
    if (dayNum == day)
      dates = TD_selected + TD_onclick + DIV_selected + getNumUniCode(dayNum) + xDIV + xTD + dates;
    else
      dates = TD + TD_onclick + getNumUniCode(dayNum) + xTD + dates;
          
    // if this is a Friday, start a new row
    if (weekday == 6)
    {
	    html += TR + dates + xTR;
	    dates = "";
	}
	weekday = (weekday % 7) + 1;
    
    // increment the day
    thisDay[2]++;
  } 
 
  // fill in any trailing blanks
  if(len==29)
  {
    jj = (6 - thisDay[3]);
    if(jj==-1) jj=6;
  }
  else
  {
    jj = (6 - thisDay[3] - 1);
    if(jj==-1) jj=6;
    if(jj==-2) jj=5;
  }
      
  for (i = 0; i<jj; i++)
    dates = TD_hide + "&nbsp;" + xTD + dates;
  
  html += TR + dates + xTR;
   
  //html += TR_todaybutton + TD_todaybutton;
  //html += "<button class='dpTodayButton' onClick='refreshDatePicker(\"" + dateFieldName + "\");'>&#1575;&#1605;&#1585;&#1608;&#1586;</button> ";
  //html += "<button class='dpTodayButton' onClick='updateDateField(\"" + dateFieldName + "\");'>&#1576;&#1587;&#1578;&#1606;</button>";
  //html += xTD + xTR;
 
  // and finally, close the table
  html += xTABLE;
  
   // add a button to allow the user to easily return to today, or close the calendar
  //var today = new Date()
  //var todayString = "Today is " + dayArrayMed[today.getDay()] + ", " + monthArrayMed[ today.getMonth()] + " " + today.getDate();
  //html += TABLE;
  //html += TR_title;
  //html += TD_days + todayString + xTD
  //html += xTR;
  //html += xTABLE;
 
  document.getElementById(datePickerDivID).innerHTML = html;
  // add an "iFrame shim" to allow the datepicker to display above selection lists
  adjustiFrame();
}

function hidePicker()
{
  var pickerDiv = document.getElementById(datePickerDivID);
  pickerDiv.style.visibility = "hidden";
  pickerDiv.style.display = "none";
  adjustiFrame();
  targetDateField.focus();
}

/**
Convenience function for writing the code for the buttons that bring us back or forward
a month.
*/
function getButtonCode(dateFieldName, dateVal, adjust, label)
{
  var newMonth = (dateVal[1] + adjust) % 12;
  var newYear = dateVal[0] + parseInt((dateVal[1] + adjust) / 12);
  if (newMonth < 1) {
    newMonth += 12;
    newYear += -1;
  }
 
  return "<button class='dpButton' onClick='refreshDatePicker(\"" + dateFieldName + "\", " + newYear + ", " + newMonth + ");'>" + label + "</button>";
}

/**
Convenience function for writing the code for the buttons that bring us back or forward
a Year.
*/
function getButtonCodeYear(dateFieldName, dateVal, adjust, label)
{
  var newMonth = dateVal[1];
  var newYear = (dateVal[0] + adjust);
 // if (newMonth < 1) {
 //   newMonth += 12;
 //   newYear += -1;
 // }
 
  return "<button class='dpButton' onClick='refreshDatePicker(\"" + dateFieldName + "\", " + newYear + ", " + newMonth + ");'>" + label + "</button>";
}

/**
Convert a JavaScript Date object to a string, based on the dateFormat and dateSeparator
variables at the beginning of this script library.
*/
function getDateString(dateVal)
{
  var dayString = "00" + dateVal[2];
  var monthString = "00" + (dateVal[1]);
  dayString = dayString.substring(dayString.length - 2);
  monthString = monthString.substring(monthString.length - 2);
 
  switch (dateFormat) {
    case "dmy" :
      return dayString + dateSeparator + monthString + dateSeparator + dateVal[0];
    case "ymd" :
      return dateVal[0] + dateSeparator + monthString + dateSeparator + dayString;
    case "mdy" :
    default :
      return monthString + dateSeparator + dayString + dateSeparator + dateVal[0];
  }
}


/**
Convert a string to a JavaScript Date object.
*/
function getFieldDate(dateString)
{
  var dateVal;
  var dArray;
  var d, m, y;
 
  try 
  {
    dArray = splitDateString(dateString);
    if (dArray) 
    {
      switch (dateFormat) 
      {
        case "dmy" :
          d = parseInt(dArray[0], 10);
          m = parseInt(dArray[1], 10);
          y = parseInt(dArray[2], 10);
          break;
        case "ymd" :
          d = parseInt(dArray[2], 10);
          m = parseInt(dArray[1], 10);
          y = parseInt(dArray[0], 10);
          break;
        case "mdy" :
        default :
          d = parseInt(dArray[1], 10);
          m = parseInt(dArray[0], 10);
          y = parseInt(dArray[2], 10);
          break;
      }
      dateVal = new Array(y, m, d);
    } 
    else if (dateString) 
    {
      dateVal = getTodayHijri();
    } 
    else 
    {
      dateVal = getTodayHijri();
    }
  } 
  catch(e) 
  {
    dateVal = getTodayHijri();
  }
 
  return dateVal;
}


/**
Try to split a date string into an array of elements, using common date separators.
If the date is split, an array is returned; otherwise, we just return false.
*/
function splitDateString(dateString)
{
  var dArray;
  if (dateString.indexOf("/") >= 0)
    dArray = dateString.split("/");
  else if (dateString.indexOf(".") >= 0)
    dArray = dateString.split(".");
  else if (dateString.indexOf("-") >= 0)
    dArray = dateString.split("-");
  else if (dateString.indexOf("\\") >= 0)
    dArray = dateString.split("\\");
  else
    dArray = false;
 
  return dArray;
}

function updateDateField(dateFieldName, dateString)
{
  var targetDateField = document.getElementsByName (dateFieldName).item(0);
  if (dateString)
    targetDateField.value = dateString;
 
  var pickerDiv = document.getElementById(datePickerDivID);
  pickerDiv.style.visibility = "hidden";
  pickerDiv.style.display = "none";
 
  adjustiFrame();
  targetDateField.focus();
 
  // after the datepicker has closed, optionally run a user-defined function called
  // datePickerClosed, passing the field that was just updated as a parameter
  // (note that this will only run if the user actually selected a date from the datepicker)
  if ((dateString) && (typeof(datePickerClosed) == "function"))
    datePickerClosed(targetDateField);
}

function adjustiFrame(pickerDiv, iFrameDiv)
{
  // we know that Opera doesn't like something about this, so if we
  // think we're using Opera, don't even try
  var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);
  if (is_opera)
    return;
  
  // put a try/catch block around the whole thing, just in case
  try 
  {
    if (!document.getElementById(iFrameDivID)) {
      // don't use innerHTML to update the body, because it can cause global variables
      // that are currently pointing to objects on the page to have bad references
      //document.body.innerHTML += "<iframe id='" + iFrameDivID + "' src='javascript:false;' scrolling='no' frameborder='0'>";
      var newNode = document.createElement("iFrame");
      newNode.setAttribute("id", iFrameDivID);
      newNode.setAttribute("src", "javascript:false;");
      newNode.setAttribute("scrolling", "no");
      newNode.setAttribute ("frameborder", "0");
      document.body.appendChild(newNode);
    }
    
    if (!pickerDiv)
      pickerDiv = document.getElementById(datePickerDivID);
    if (!iFrameDiv)
      iFrameDiv = document.getElementById(iFrameDivID);
    
    try 
    {
      iFrameDiv.style.position = "absolute";
      iFrameDiv.style.width = pickerDiv.offsetWidth;
      iFrameDiv.style.height = pickerDiv.offsetHeight ;
      iFrameDiv.style.top = pickerDiv.style.top;
      iFrameDiv.style.left = pickerDiv.style.left;
      iFrameDiv.style.zIndex = pickerDiv.style.zIndex - 1;
      iFrameDiv.style.visibility = pickerDiv.style.visibility ;
      iFrameDiv.style.display = pickerDiv.style.display;
    } 
    catch(e) 
    {
    }
  } 
  catch (ee) 
  {
  }
}







