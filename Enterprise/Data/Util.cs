using System;

namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		public Util()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string AddInQuery(string text, string[] AddArray)
		{	
			string concatString="";
			string returnString="";
			int fieldCount=0;
			if(text.IndexOf(" * ")>0 && text.ToUpper().IndexOf(" FROM ")>text.IndexOf(" * "))
				return text;
			
			for(int i=0;i<AddArray.Length;i++)
			{
				if(text.ToUpper().IndexOf(AddArray[i])>=0)
				{
					fieldCount++;
				}
			}
			if(fieldCount==AddArray.Length)
				return text;

			for(int i=0;i<AddArray.Length;i++)
			{
				concatString += " ," + AddArray[i];
			}
			if(text.ToUpper().IndexOf(" FROM ") == text.ToUpper().LastIndexOf(" FROM "))
				returnString=text.ToUpper().Replace(" FROM ", concatString + " FROM ");
			else if(text.ToUpper().IndexOf(" FROM ")>0)
			{
				//returnString = text.Substring(0,text.ToUpper().IndexOf(" FROM ")-1) + concatString + " " + text.Substring(text.ToUpper().IndexOf(" FROM ")-1) ;
				returnString = text.Substring(0,text.ToUpper().IndexOf(" FROM ")) + concatString + " " + text.Substring(text.ToUpper().IndexOf(" FROM ")) ;
			}

			return returnString;
		}
	}
}
