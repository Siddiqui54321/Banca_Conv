using System;

namespace SHMA.Enterprise.Presentation.Filters
{
	/// <summary>
	/// Summary description for FilterFactory.
	/// </summary>
	public class ControlFilterFactory
	{
		public ControlFilterFactory()
		{
		}
		public BaseFieldFilter GetFilter(string filterName)
		{
			if(filterName.ToUpper()=="ProgrammableLengthFilter".ToUpper())
			{
				return new ProgrammableLengthFilter();
			}
			else
				return null;
		}

	}
}
