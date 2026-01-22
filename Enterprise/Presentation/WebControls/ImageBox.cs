using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:ImageBox runat=server></{0}:ImageBox>")]
	public class ImageBox : System.Web.UI.WebControls.Image {	
		private bool baseTable = true;
		public string Text{
			get {
				return base.ImageUrl;
			}
			set {
				base.ImageUrl = value;
			}
		}			
		public bool BaseTable	{
			get{
				return baseTable;
			}
			set{
				baseTable = value;	
			}
		}

	}
}
