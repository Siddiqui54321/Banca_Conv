using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Text;
 
namespace SHMA.Enterprise.Presentation.WebControls{
	
	[DefaultProperty("Text"), ToolboxData("<{0}:ComboBox runat=server></{0}:ComboBox>")]
	public class ComboBox:			LookupText{

		private string listWidth;
		private string validation="Y";
		
		public string ListWidth{
			set {
				listWidth = value;
			}
		}
		
		public override string Validation
		{
			set 
			{
				validation = value;
			}
			get
			{
				return validation;
			}
		}

		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender (e);			
			this.Attributes.Add( "onkeydown", "comboPress('" + this.ClientID + "');" )  ;
			this.Attributes.Add("title", "'Press F9 or double click to open complete list of values.'" );			
			this.Attributes.Add("ondblclick" , "showHide('" + this.ClientID + "');" ) ;

			//this.Attributes.Add("query" , Query );
			//this.Attributes.Add("mappingQuery" , mappingQuery );
			this.Attributes.Add("arrCode" , "" );
			this.Attributes.Add("arrDesc" , "" );			
			this.Attributes.Add("arrInfo" , "" );
			this.Attributes.Add("pageNo" , "" );
			this.Attributes.Add("divwidth" , listWidth );			
			this.Attributes.Add("textFields", textFields.ToString());
			this.Attributes.Add("validation" , validation );

			if (this.CssClass.IndexOf("ComboText") < 0 )
				this.CssClass += " ComboText";

		}
		protected override void Render(HtmlTextWriter output) {
			base.Render(output);
			string divWidth ="";
			divWidth = string.Format("STYLE=\"width:{0}\"" , 0);
				
			string div = string.Format("<DIV class='listHide' id='div{0}' onkeypress=\"hide('{0}') ;\"  onblur=\"hide('{0}');\" onclick=\"hide('{0}');\" {1}></DIV>\n" , this.ClientID, divWidth) ;
			string frm = "<iframe class='listHide' id='frm" + this.ClientID + "' "+ divWidth +"></iframe>\n"  ;
			
			output.Write(div);
			output.Write(frm);
		}
	}
}
