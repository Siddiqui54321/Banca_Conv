using System;

namespace SHMA.Enterprise.Presentation.WebControls
{
    /// <summary>
    /// Summary description for TextBox.
    /// </summary>
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        // Make a property to render array of formatted controls alongwith its attributes
        private string baseType = "";
        private string subType = null;
        private int precision = 2;
        private int currencyGroupSize = 3;
        private string currencyGroupSeparator = ",";
        private string currencySymbol = "";
        private string decimalSymbol = ".";
        private bool rollBackOnFocus = true; // to be taken from configuration file
        [ThreadStatic]
        private bool onBlurAttached = true;
        [ThreadStatic]
        private bool onFocusAttached = true;
        //	public enum baseTypes {Number=0, Currency=1,String=2};
        private bool highLight = false;
        private int IntialMaxLen = 0;
        public TextBox()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string BaseType
        {
            get
            {
                return baseType;
            }
            set
            {
                baseType = value;
            }
        }

        public string SubType
        {
            get
            {
                return subType;
            }
            set
            {
                subType = value;
            }
        }


        public int Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
            }
        }

        public bool HighLighted
        {
            get
            {
                return highLight;
            }
            set
            {
                highLight = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.EnableViewState = true;
            IntialMaxLen = this.MaxLength;
        }

        protected bool RollbackOnFocus
        {
            get
            {
                return rollBackOnFocus;
            }
            set
            {
                this.rollBackOnFocus = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string onblurTemp = "", onfocusTemp = "";
            System.Globalization.CultureInfo cinfo = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name);
            cinfo.NumberFormat.CurrencyDecimalDigits = this.Precision;
            cinfo.NumberFormat.CurrencyGroupSeparator = this.currencyGroupSeparator;
            int[] grpArray = new int[5];
            for (int i = 0; i < grpArray.Length; i++)
                grpArray[i] = this.currencyGroupSize;
            cinfo.NumberFormat.CurrencyGroupSizes = grpArray;
            cinfo.NumberFormat.CurrencySymbol = this.currencySymbol;
            cinfo.NumberFormat.CurrencyDecimalSeparator = this.decimalSymbol;
            Decimal d = 0;

            if (this.Attributes["onblur"] == null || this.Attributes["onblur"] == "")
                onblurTemp = "";
            else
                onblurTemp = this.Attributes["onblur"].Trim();

            if (this.Attributes["onfocus"] == null || this.Attributes["onfocus"] == "")
                onfocusTemp = "";
            else
                onfocusTemp = this.Attributes["onfocus"].Trim();

            if (!this.Width.IsEmpty)
            {
                Style.Add("width", this.Width.ToString());
            }

            if (this.baseType == "Number" && this.subType != null)
            {
                if (this.SubType == "Currency")
                {
                    if (precision > 0 && base.MaxLength > 0 && base.MaxLength == IntialMaxLen)
                    {
                        base.MaxLength = base.MaxLength + 1;
                    }
                    Style.Add("text-align", "right");
                    if (this.Text.Length > 0)
                    {
                        if (this.Text.ToString().IndexOf("E") > 0)
                            d = Convert.ToDecimal((Convert.ToDouble(this.Text.ToString())));
                        else
                            d = Convert.ToDecimal(this.Text.ToString());
                        this.Text = d.ToString("c", cinfo);
                    }

                    string onchange = this.Attributes["onchange"] == null ? "" : this.Attributes["onchange"].Trim();
                    if (onchange != "" && onchange.Substring(onchange.Length - 1, 1).Equals(";"))
                    {
                        this.Attributes["onchange"] += "return validateFormat(this);";
                    }
                    else
                    {
                        this.Attributes["onchange"] += ";return validateFormat(this);";
                    }


                    if ((onblurTemp != "") && (!onblurTemp.Substring(onblurTemp.Length - 1, 1).Equals(";")) && onBlurAttached)
                    {
                        //this.Attributes["onblur"] += ";if(validateFormat(this)) {applyCurrencyFormat(this, " + this.precision + ");}";//
                        this.Attributes["onblur"] +=  ";applyCurrencyFormat(this, "+ this.precision +");";
                        onBlurAttached = false;
                    }
                    else if (onBlurAttached)
                    {
                        //this.Attributes["onblur"] += "if(validateFormat(this)) {applyCurrencyFormat(this, " + this.precision + ");}";//
                        this.Attributes["onblur"] +=  "applyCurrencyFormat(this, "+ this.precision +");";
                    }
                    if (rollBackOnFocus && onFocusAttached)
                    {
                        if ((onfocusTemp != "") && (!onfocusTemp.Substring(onfocusTemp.Length - 1, 1).Equals(";")))
                            this.Attributes["onfocus"] += ";deformatCurrency(this, " + this.precision + ");";
                        else
                            this.Attributes["onfocus"] += "deformatCurrency(this, " + this.precision + ");";
                        onFocusAttached = false;
                    }
                    this.Attributes["Precision"] = precision.ToString();
                    this.Attributes["SubType"] = subType;

                }
                else if (this.SubType == "FormattedNumber")
                {
                    Style.Add("text-align", "right");
                    cinfo.NumberFormat.CurrencySymbol = "";
                    if (this.Text.Length > 0)
                    {
                        d = Convert.ToDecimal(this.Text.ToString());
                        this.Text = d.ToString("c", cinfo);
                    }
                    //this.Attributes["onblur"] +=  "applyNumberFormat(this, "+ this.precision +");";

                    if ((onblurTemp != "") && (!onblurTemp.Substring(onblurTemp.Length - 1, 1).Equals(";")) && onBlurAttached)
                    {
                        this.Attributes["onblur"] += ";setFieldValueByRef(this, this.value);";
                        onBlurAttached = false;
                    }
                    else if (onBlurAttached)
                    {
                        this.Attributes["onblur"] += "setFieldValueByRef(this, this.value);";
                        onBlurAttached = false;
                    }
                    if (rollBackOnFocus && onFocusAttached)
                    {
                        if ((onfocusTemp != "") && (!onfocusTemp.Substring(onfocusTemp.Length - 1, 1).Equals(";")))
                            this.Attributes["onfocus"] += ";deformatNumber(this, " + this.precision + ");";
                        else
                            this.Attributes["onfocus"] += "deformatNumber(this, " + this.precision + ");";
                        onFocusAttached = false;
                    }
                }
                this.Attributes["Precision"] = precision.ToString();
                this.Attributes["SubType"] = subType;

            }
            else if (this.baseType == "Number" && this.subType == null)
            {
                Style.Add("text-align", "right");
                this.Attributes["SubType"] = "";
                this.Attributes["Precision"] = precision.ToString();
            }
            this.Attributes["BaseType"] = BaseType;
            SetDisabilityStyle();

            if (this.highLight == true)
            {
                CssClass += " " + "highLightControl";
            }

        }

        protected void ClientScript()
        {
            System.Text.StringBuilder sb_Script = new System.Text.StringBuilder();
            sb_Script.Append("<script language=\"javascript\">");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("function getValue(refObj) {");
            sb_Script.Append("\r");
            sb_Script.Append("var strVal = refObj.value;");
            sb_Script.Append("\r");
            sb_Script.Append("if(strVal!=''){");
            sb_Script.Append("\r");
            sb_Script.Append("while(strVal.indexOf(',',0)>-1)");
            sb_Script.Append("\r");
            sb_Script.Append("strVal = strVal.replace(',','');");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("return strVal");
            sb_Script.Append("}");
            sb_Script.Append("function setValue(refObj) {");
            sb_Script.Append("\r");
            sb_Script.Append("if(trim(refObj.value)=='') return; CurrencyFormat(refObj);");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("</script>");
            this.Page.RegisterClientScriptBlock("SHMATextBox", sb_Script.ToString());
            //	return sb_Script.ToString();
        }


        void SetDisabilityStyle()
        {
            string disabilityStyle = System.Configuration.ConfigurationSettings.AppSettings["DisabilityStyle"];
            if (disabilityStyle != "")
            {
                this.Attributes.Add("disabilityStyle", disabilityStyle);
                if (!this.Enabled || this.ReadOnly)
                {
                    if (CssClass.IndexOf(disabilityStyle) < 0)
                    {
                        CssClass += " " + disabilityStyle; ;
                    }
                }
                else
                {
                    CssClass.Replace(disabilityStyle, "");
                }
            }
        }


    }
}
