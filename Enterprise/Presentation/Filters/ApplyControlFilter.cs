using System;
using System.Web;
namespace SHMA.Enterprise.Presentation.Filters{

	/// <summary>
	/// Summary description for ApplyControlFilter.
	/// </summary>
	public class ApplyControlFilter
	{
		
		public ApplyControlFilter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		

		public static void ApplyFilter(string controlName,System.Web.UI.Control control)
		{
			string[] filters = availableFilters();
			if(filters!=null)
			{
				for(int i=0;i<filters.Length;i++)
				{
					Type type = Type.GetType("SHMA.Enterprise.Presentation.Filters." + filters[i]);
					BaseFieldFilter bFilter = (BaseFieldFilter)Activator.CreateInstance(type);
					bFilter.applyFilter(controlName, control);
				}
			}
		}

		private static string[] availableFilters()
		{
			string filters=null;
			if(System.Configuration.ConfigurationSettings.AppSettings["FILTERS"]!=null)
			{
				filters= System.Configuration.ConfigurationSettings.AppSettings["FILTERS"];
			}
			if(filters!=null)
				return filters.Split(',');
			else
				return null;
		}
	}
}
