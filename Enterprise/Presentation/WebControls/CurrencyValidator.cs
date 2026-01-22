using System;

namespace SHMA.Enterprise.Presentation.WebControls
{
	/// <summary>
	/// Summary description for CurrencyValidator.
	/// </summary>
	public class CurrencyValidator:System.Web.UI.WebControls.CompareValidator
	{
		public CurrencyValidator()
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
