/*
* Created on Feb 23, 2004
*
*/

using System;
namespace shgn
{
	
	/// <author>  muhammad.mansoor
	/// 
	/// <p>Copyright: Copyright (c) 2004 </p>
	/// <p>Company: SHMA</p>
	/// <p> // TODO  Document Me ! </p>
	/// 
	/// </author>
	public class SHGNDateUtil
	{
		private static System.String FISCAL_YEAR_QUERY = "SELECT YR.PFS_ACNTYEAR from PR_GN_FS_FISCALYR YR where (CURRENT_DATE) BETWEEN (PFS_ACNTDATEFROM) and (PFS_ACNTDATETO)";
		//private const System.String DATE_FORMAT = "dd/MM/yyyy";
		private const System.String DATE_FORMAT = "dd/MM/yyyy";
		
		//UPGRADE_ISSUE: Class 'java.text.SimpleDateFormat' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javatextSimpleDateFormat"'
	/*	public static SimpleDateFormat getSimpleDateFormat()
		{
			//UPGRADE_ISSUE: Constructor 'java.text.SimpleDateFormat.SimpleDateFormat' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javatextSimpleDateFormat"'

			return new SimpleDateFormat() = DATE_FORMAT;
		}
	*/
		 public static int getDays(System.String datefr, System.String dateto)
		{
			int mdays = 0;
			
			System.Globalization.GregorianCalendar mGetDays1 = new System.Globalization.GregorianCalendar();
			System.Globalization.GregorianCalendar mGetDays2 = new System.Globalization.GregorianCalendar();
			
			//UPGRADE_TODO: Format of parameters of method 'java.util.Calendar.setTime' are different in the equivalent in .NET. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092"'
			SupportClass.CalendarManager.manager.SetDateTime(mGetDays2, SHGNDateUtil.parseDate(dateto));
			//UPGRADE_TODO: Format of parameters of method 'java.util.Calendar.setTime' are different in the equivalent in .NET. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092"'
			SupportClass.CalendarManager.manager.SetDateTime(mGetDays1, SHGNDateUtil.parseDate(datefr));
			//UPGRADE_TODO: Method 'java.util.Date.getTime' was converted to 'System.DateTime.Ticks' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073"'
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
			mdays = (int) ((SupportClass.CalendarManager.manager.GetDateTime(mGetDays2).Ticks - SupportClass.CalendarManager.manager.GetDateTime(mGetDays1).Ticks) / 1000 / 60 / 60 / 24) + 1;

			return mdays;
		}
		
		public static System.String getSystemDateAsString()
		{
			SHMA.Enterprise.Shared.EnvHelper env = new SHMA.Enterprise.Shared.EnvHelper();
			return (String)env.getAttribute("s_SYSDATE");			
		//	System.DateTime sysDate = System.DateTime.Now;
		//	return sysDate.ToString(DATE_FORMAT);
		}
		
		public static System.String getDateAsString(System.DateTime date)
		{
			//UPGRADE_ISSUE: Class 'java.text.SimpleDateFormat' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javatextSimpleDateFormat"'
			return date.ToString(); 
		}
		
		public static System.DateTime parseDate(System.String str_Date)
		{
			//UPGRADE_ISSUE: Class 'java.text.SimpleDateFormat' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javatextSimpleDateFormat"'
			//SimpleDateFormat dFormat = getSimpleDateFormat();
			System.DateTime myDate = System.DateTime.Now;
			try
			{
				myDate = System.DateTime.Parse(str_Date);
			}
			catch (System.FormatException e)
			{
				//SupportClass.WriteStackTrace(e, Console.Error);
			}
			return myDate;
		}
		
		public static System.DateTime getSystemDate()
		{
			SHMA.Enterprise.Shared.EnvHelper env = new SHMA.Enterprise.Shared.EnvHelper();
			return Convert.ToDateTime(env.getAttribute("s_SYSDATE"), System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
			//return System.DateTime.Now;
		}
		
		private static System.Globalization.GregorianCalendar getCalendar()
		{
			return new System.Globalization.GregorianCalendar();
		}
		
		public static System.String getSysDateAsString()
		{
			return getSystemDateAsString();
		}
		
		public static System.String setDate(String strDate) {
			return strDate ;
		}

		public static System.String getMMMDate(String DateUK)
		{
			int month;
			int day;
			string year;
			string[] months= new System.String[12];
			months[0]="JAN";
			months[1]="FEB";
			months[2]="MAR";
			months[3]="APR";
			months[4]="MAY";
			months[5]="JUN";
			months[6]="JUL";
			months[7]="AUG";
			months[8]="SEP";
			months[9]="OCT";
			months[10]="NOV";
			months[11]="DEC";
			string separator=null;
			if(DateUK.IndexOf("/") > -1)
				separator = "/";
			else if(DateUK.IndexOf("-") > -1)
				separator = "-";
			else if(DateUK.IndexOf(".") > -1)
				separator = ".";

			day = Int32.Parse(DateUK.Substring(0,DateUK.IndexOf(separator)));
			month = Int32.Parse(DateUK.Substring(DateUK.IndexOf(separator)+1,DateUK.LastIndexOf(separator)-DateUK.IndexOf(separator)-1));
			year = DateUK.Substring(DateUK.LastIndexOf(separator)+1);
			return day +"-"+months[month-1]+"-"+year;
			
		}

		public static int getPreviousMonth(System.DateTime date1, int no) {
			return	date1.Month - no;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
		public static int getCurrentDay(System.DateTime date1) {
			return date1.Day;
		}
		public static System.DateTime getFirstDay(System.DateTime datefr) {
			return new DateTime(datefr.Year,datefr.Month,1);
		}
		
		public static System.DateTime getNextDay(System.DateTime datefr, int i) {
			System.Globalization.GregorianCalendar ca =  new System.Globalization.GregorianCalendar();
			return ca.AddDays(datefr, i);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
		public static System.DateTime getPreviousDay(System.DateTime date1, int no) {
			System.Globalization.GregorianCalendar ca =  new System.Globalization.GregorianCalendar();
			return ca.AddDays(date1, -no);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
		public static int getDays(System.DateTime datefr, System.DateTime dateto) {

			TimeSpan mdays = new TimeSpan();
			mdays = dateto - datefr;
			return mdays.Days + 1;
			
		}
		
		public static System.DateTime getQuaterlyMaxDat(System.DateTime dateid) {
			System.DateTime returnDate;
			if (dateid.Month>=1 && dateid.Month<=3) 
				returnDate = new DateTime(dateid.Year,3,31);
			else if (dateid.Month>=4 && dateid.Month<=6) 
				returnDate = new DateTime(dateid.Year,6,30);
			else if (dateid.Month>=7 && dateid.Month<=9) 
				returnDate = new DateTime(dateid.Year,9,30);
			else if (dateid.Month>=10 && dateid.Month<=12) 
				returnDate = new DateTime(dateid.Year,12,31);
			else 
				returnDate = DateTime.Now ;
			
			return returnDate;
		}
		
		public static System.DateTime getQuaterlyMinDat(System.DateTime dateid) {
//updated by rehan on 06/01/2006 
			System.DateTime returnDate ;
			if (dateid.Month>=1 && dateid.Month<=3) 
				returnDate = new DateTime(dateid.Year,1,1);
			else if (dateid.Month>=4 && dateid.Month<=6) 
				returnDate = new DateTime(dateid.Year,4,1);
			else if (dateid.Month>=7 && dateid.Month<=9) 
				returnDate = new DateTime(dateid.Year,7,1);
			else if (dateid.Month>=10 && dateid.Month<=12) 
				returnDate = new DateTime(dateid.Year,10,1); 
			else 
				returnDate = DateTime.Now ;

			return returnDate;
		}

		public static System.DateTime getHalfMinDat( System.DateTime dateid) {

			System.DateTime returnDate;
			if (dateid.Month>=1 && dateid.Month<=6) 
				returnDate = new DateTime(dateid.Year,1,1);
			else if (dateid.Month>=7 && dateid.Month<=12) 
				returnDate = new DateTime(dateid.Year,7,1);
			else 
				returnDate = DateTime.Now ;
			return returnDate;
		}

		public static System.DateTime getHalfMaxDat( System.DateTime dateid){
			System.DateTime returnDate;
			if (dateid.Month>=1 && dateid.Month<=6) 
				returnDate = new DateTime(dateid.Year,6,30);
			else if (dateid.Month>=7 && dateid.Month<=12) 
				returnDate = new DateTime(dateid.Year,12,31);
			else 
				returnDate = DateTime.Now ;

			return returnDate;
		}
		
		public static int getYearDays( System.DateTime date1) {
			int int_YearDays = 0;
			if (DateTime.IsLeapYear(date1.Year))
				int_YearDays = 366;
			else
				int_YearDays = 365;
          			
			return int_YearDays;
		}
		
		public static System.DateTime getActualDate(System.DateTime opndate, System.DateTime frmdate) {
			if (opndate.CompareTo(frmdate) <= 0) {
				return frmdate;
			}
			else {
				return opndate;
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1303_3"'
		public static System.DateTime getLastDay(System.DateTime dateto) {
			/*System.DateTime newDateTime;
			newDateTime = Convert.ToDateTime("01" + "/" + dateto.Month + "/" + dateto.Year);
			return newDateTime;*/
			System.Globalization.GregorianCalendar ca =  new System.Globalization.GregorianCalendar();
			
			return new DateTime(dateto.Year, dateto.Month, ca.GetDaysInMonth(dateto.Year,dateto.Month));

		}
		
		public static System.DateTime GetDateAsSqlFormat(System.String str_Date) {
			return Convert.ToDateTime(str_Date);
		}

		/*
		/// <summary> </summary>
		/// <returns> Returns 1 for Jan, 2 for Feb and so on ...
		/// </returns>
		 public static System.String getCurrentMonth()
		{
			//UPGRADE_TODO: Method 'java.util.Calendar.get' was converted to 'SupportClass.CalendarManager.Get' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilCalendarget_int"'
			return (SupportClass.CalendarManager.manager.Get(getCalendar(), SupportClass.CalendarManager.MONTH) + 1) + "";
		}
		
		/// <summary> </summary>
		/// <returns> Returns LastMonth 0 if in Jan, 1 if in Feb , 2 if in Mar
		/// </returns>
		public static System.String getLastMonth()
		{
			//UPGRADE_TODO: Method 'java.util.Calendar.get' was converted to 'SupportClass.CalendarManager.Get' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilCalendarget_int"'
			return (SupportClass.CalendarManager.manager.Get(getCalendar(), SupportClass.CalendarManager.MONTH)) + "";
		}
		
		
		public static System.String getSysDateAsStringForDB2()
		{
			return getSystemDateAsString().Replace('/', '.');
		}
		
		public static System.String getAccountigYear()
		{
			return new SHGNDataAccess().fsgetColumnAgainstQuery(FISCAL_YEAR_QUERY);
		}
		
		public static System.String getThisMonthsStartingDate()
		{
			System.Globalization.GregorianCalendar calendar = getCalendar();
			SupportClass.CalendarManager.manager.Set(calendar, SupportClass.CalendarManager.DAY_OF_MONTH, 1);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
			return getDateAsString(SupportClass.CalendarManager.manager.GetDateTime(calendar));
		}
		
		public static System.String getThisMonthsStartingDateDB2()
		{
			return getThisMonthsStartingDate().Replace('/', '.');
		}
		
		public static System.String getYesterdayDate()
		{
			System.Globalization.GregorianCalendar calendar = getCalendar();
			//UPGRADE_ISSUE: Method 'java.util.GregorianCalendar.roll' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javautilGregorianCalendarroll_int_boolean"'
			calendar.roll(SupportClass.CalendarManager.DAY_OF_MONTH, false);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
			return getDateAsString(SupportClass.CalendarManager.manager.GetDateTime(calendar));
		}
		
		public static System.String getYesterdayDateDB2()
		{
			return getYesterdayDate().Replace('/', '.');
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Text.StringBuilder sBuffer = new System.Text.StringBuilder();
			sBuffer.Append("----------------------- SHGNDateUtil Test Begin -----------------------\n");
			sBuffer.Append("Date Output in dd/MM/yyyy Format : " + getSysDateAsString() + "\n");
			sBuffer.Append("Date Output for DB2 ( '.' Separated ) : " + getSysDateAsStringForDB2() + "\n");
			sBuffer.Append("getThisMonthsStartingDate Format : " + getThisMonthsStartingDate() + "\n");
			sBuffer.Append("getYesterdayDate() Format : " + getYesterdayDate() + "\n");
			sBuffer.Append("----------------------- SHGNDateUtil Test Ended -----------------------\n");
			//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.WriteLine' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073"'
			System.Console.Out.WriteLine(sBuffer);
		}
		
		public static System.String getDayBefore(System.String str_Date)
		{
			System.Globalization.GregorianCalendar calendar = getCalendar();
			//UPGRADE_TODO: Format of parameters of method 'java.util.Calendar.setTime' are different in the equivalent in .NET. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092"'
			SupportClass.CalendarManager.manager.SetDateTime(calendar, parseDate(str_Date));
			//UPGRADE_ISSUE: Method 'java.util.GregorianCalendar.roll' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javautilGregorianCalendarroll_int_boolean"'
			calendar.roll(SupportClass.CalendarManager.DAY_OF_MONTH, false);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
			return getDateAsString(SupportClass.CalendarManager.manager.GetDateTime(calendar));
		}
		
		public static System.String getStartingMonthDate(System.String str_date)
		{
			System.Globalization.GregorianCalendar calendar = getCalendar();
			//UPGRADE_TODO: Format of parameters of method 'java.util.Calendar.setTime' are different in the equivalent in .NET. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092"'
			SupportClass.CalendarManager.manager.SetDateTime(calendar, parseDate(str_date));
			SupportClass.CalendarManager.manager.Set(calendar, SupportClass.CalendarManager.DAY_OF_MONTH, 1);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
			return getDateAsString(SupportClass.CalendarManager.manager.GetDateTime(calendar));
		}*/
		public static DateTime getNextYear( DateTime dt,int a)
		{
			return dt.AddYears(a);

		}
		private class  SimpleDateFormat {
			public SimpleDateFormat(){
			}
		}
		public static System.DateTime getNextMonth(System.DateTime date1,int no) {
			System.Globalization.GregorianCalendar ca =  new System.Globalization.GregorianCalendar();
			return ca.AddMonths(date1,no);
		}

		public static DateTime getDate()
		{
			SHMA.Enterprise.Shared.EnvHelper env = new SHMA.Enterprise.Shared.EnvHelper();
			return Convert.ToDateTime(env.getAttribute("s_SYSDATE"), System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
			//return DateTime.Now.Date;
		}

	}
	
}