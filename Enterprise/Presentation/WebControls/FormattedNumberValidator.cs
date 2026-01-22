using System;

namespace SHMA.Enterprise.Presentation.WebControls
{
	/// <summary>
	/// Summary description for FormattedNumber.
	/// </summary>
	public class FormattedNumberValidator:System.Web.UI.WebControls.CompareValidator
	{
		public FormattedNumberValidator()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		protected override void OnInit(EventArgs e) {
			base.OnInit (e);
			base.Type = System.Web.UI.WebControls.ValidationDataType.Currency;
		}
	}
}
