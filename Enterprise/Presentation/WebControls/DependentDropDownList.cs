using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[DefaultProperty("Text"), ToolboxData("<{0}:DependentDropDown runat=server></{0}:DependentDropDown>")]
	public class DependentDropDown: System.Web.UI.WebControls.DropDownList{
		protected override void OnLoad(System.EventArgs e){
			if (Page.IsPostBack){
				if (Page.Request[this.ClientID]!=null)
					this.ClearSelection();
					ListItem item = new ListItem(Page.Request[this.ClientID], Page.Request[this.ClientID]);
					item.Selected = true;
					this.Items.Add(item);
			}
		}
void SetDisabilityStyle(){
       string disabilityStyle = System.Configuration.ConfigurationSettings.AppSettings["DisabilityStyle"] ;
       this.Attributes.Add("disabilityStyle" , disabilityStyle);
       if (!this.Enabled ){    
		   if (CssClass.IndexOf(disabilityStyle) < 0)
		   {
			   CssClass += " ";
			   CssClass += disabilityStyle ;   
		   }


        }
       else{    
            CssClass.Replace(disabilityStyle , "");
       }  
  }


	}
}