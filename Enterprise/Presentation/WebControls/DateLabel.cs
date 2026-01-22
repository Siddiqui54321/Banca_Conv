using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DateLabel runat=server></{0}:DateLabel>")]
	public class DateLabel : System.Web.UI.WebControls.Literal {
		string ss;
		[Bindable(true), Category("Appearance"), DefaultValue("")] 		
		public new string Text{
			get {				
				return base.Text;
			}
			set {
				try{
					base.Text = DateTime.Parse(value).ToShortDateString();
				}
				catch(Exception e){
					base.Text = value;
				}				
			}
		}	
	}
}