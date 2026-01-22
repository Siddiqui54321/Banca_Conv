using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
[assembly: TagPrefix("SHMA.Enterprise.Presentation.WebControls", "SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls
{
    [DefaultProperty("Text"), ToolboxData("<{0}:TextArea runat=server></{0}:TextArea>")]
    public class TextArea : System.Web.UI.WebControls.TextBox
    {
        private string maxRows = "5";
        private bool highLight = false;

        public string MaxRows
        {
            set
            {
                maxRows = value;
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
        protected override void OnPreRender(EventArgs e)
        {
            this.TextMode = TextBoxMode.MultiLine;
            this.Rows = 1;
            base.OnPreRender(e);
            this.Attributes.Add("ondblclick", String.Format("setTextAreaRows(this,{0});", ((maxRows == null || maxRows.Equals("")) ? "null" : maxRows)));
            this.Attributes.Add("maxLength", Convert.ToString(this.MaxLength));
            this.Attributes.Add("onkeypress", "return chkLength(this);");
            this.Attributes.Add("onpaste", "return chkLengthPaste(this);");
            if (this.highLight == true)
            {
                CssClass += " " + "highLightControl";
            }
            Style.Add("width", this.Width.ToString());
        }
        void SetDisabilityStyle()
        {
            string disabilityStyle = System.Configuration.ConfigurationSettings.AppSettings["DisabilityStyle"];
            this.Attributes.Add("disabilityStyle", disabilityStyle);
            if (!this.Enabled)
            {
                if (CssClass.IndexOf(disabilityStyle) < 0)
                {
                    CssClass += " ";
                    CssClass += disabilityStyle;
                }
            }
            else
            {
                CssClass.Replace(disabilityStyle, "");
            }
        }


    }
}
