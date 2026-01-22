using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DateBox runat=server></{0}:DateBox>")]
	public class DateBox : System.Web.UI.WebControls.TextBox {
		private bool baseTable = true;
		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public override string Text		{
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
		public bool BaseTable{
			get{
				return baseTable;
			}
			set{
				baseTable = value;	
			}
		}
		void SetDisabilityStyle()
		{
			string disabilityStyle = System.Configuration.ConfigurationSettings.AppSettings["DisabilityStyle"] ;
			this.Attributes.Add("disabilityStyle" , disabilityStyle);
			if (!this.Enabled )
			{    
				if (CssClass.IndexOf(disabilityStyle) < 0)
				{
					CssClass += " ";
					CssClass += disabilityStyle ;   
				}

			}
			else
			{    
				CssClass.Replace(disabilityStyle , "");
			}  
		}


	}
}
