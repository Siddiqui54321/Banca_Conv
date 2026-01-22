using System;

namespace SHMA.Enterprise.Presentation.Filters
{
	/// <summary>
	/// Summary description for BaseFieldFilter.
	/// </summary>
	public abstract class BaseFieldFilter
	{

		public abstract void applyFilter(string cntrolName,System.Web.UI.Control cntrol);
	}
}
