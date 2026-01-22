using System;

namespace SHMA.Enterprise.Presentation.WebControls
{
	/// <summary>
	/// Summary description for CurrencyValidator.
	/// </summary>
	public class CompareValidator:System.Web.UI.WebControls.CompareValidator
	{
		string baseType="";
		string subType="";
		string precision=null;
		
		public CompareValidator()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public string BaseType{
			get {
				return baseType;
			}
			set {
				baseType = value;
			}
		}

		public string SubType{
			get {
				return subType;
			}
			set {
				subType = value;
			}
		}

		public string Precision
		{
			get
			{
				return precision;
			}
			set
			{
				string _var = value;
				double tempValue;
				if(Double.TryParse(_var,  System.Globalization.NumberStyles.Integer,System.Globalization.CultureInfo.CurrentCulture.NumberFormat,out tempValue))
					precision = _var.Trim();
			}
		}

		protected override void OnInit(EventArgs e) 
		{
			base.OnInit(e);
			base.Display  = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
			base.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;

			if(baseType=="Number") {
				if(subType=="Currency" || subType=="FormattedNumber")
					base.Type = System.Web.UI.WebControls.ValidationDataType.Currency;
				else
					base.Type = System.Web.UI.WebControls.ValidationDataType.Double;
				base.ErrorMessage = "Number format is incorrect";
				if(precision!=null)
					base.Attributes["digits"]=precision;
			}
			else if(baseType=="Date") {
				base.Type = System.Web.UI.WebControls.ValidationDataType.Date;
				base.ErrorMessage = "Date format is incorrect";
			}
			else if(baseType=="Character") {
				base.Type = System.Web.UI.WebControls.ValidationDataType.String;
				base.ErrorMessage = "String format is incorrect";
			}
		}

		protected override bool EvaluateIsValid()
		{
			double tempValue;
			if(precision!=null)
			{
				System.Web.UI.WebControls.TextBox ctrl=null;
				ctrl = (System.Web.UI.WebControls.TextBox)this.Parent.FindControl(this.ControlToValidate.Trim());
				if(ctrl.Text.Trim()=="")
					return true;
				if(Double.TryParse(ctrl.Text.Trim(),  System.Globalization.NumberStyles.Currency,System.Globalization.CultureInfo.CurrentCulture.NumberFormat,out tempValue))
					return true;
				else 
					return false;
			}
			else
				return base.EvaluateIsValid();
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (baseType == "Number")
            {
                if (subType == "Currency" || subType == "FormattedNumber")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), ClientID + "_CustomVal", "if(typeof " + ClientID + " != 'undefined') " + ClientID + ".digits='" + precision + "';", true); 
                }
            }
        }



	
	}
}
