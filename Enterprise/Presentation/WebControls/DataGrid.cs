using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections ;
using System.Data;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls{
	[DefaultProperty("ShowNoRows"),ToolboxData("<{0}:datagrid runat=server></{0}:datagrid>")]
	public class DataGrid :System.Web.UI.WebControls.DataGrid{		
		
		private bool highLight=false;

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

		protected override void OnPreRender(System.EventArgs e){			
			int i=2;
			foreach (DataGridItem item in this.Items){
				if (item.ItemType==ListItemType.Item || item.ItemType==ListItemType.AlternatingItem){
					item.Attributes.Add("id","row"+i.ToString()) ;
					item.Attributes.Add("onmouseover","document.getElementById('row" + i.ToString() + "').className='" + this.SelectedItemStyle.CssClass+"';");
					item.Attributes.Add("onmouseout","document.getElementById('row" + i.ToString() + "').className='" +this.ItemStyle.CssClass+"';");
					i++;
				}
			}

			if (this.highLight == true)
			{
				CssClass += " " + "highLightControl";					
			}
        }
	}
}
