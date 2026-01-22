using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[ToolboxData("<{0}:MultiTextDropDown runat=server></{0}:MultiTextDropDown>")]
	public class MultiTextDropDown : eWorld.UI.MultiTextDropDownList{
		bool _BlankValue=false;
		private bool highLight=false;

		[Bindable(true), Category("Appearance"), DefaultValue("false")]
		public bool BlankValue {			
			get{return _BlankValue;}
			set{_BlankValue = value;}
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

		protected override void OnPreRender(System.EventArgs e){
			if (_BlankValue){
				if (this.Items.Count > 0){
					if (this.Items[0].Value.Length==0 && this.Items[0].Text.Length==0)
						Items.Insert(0, (string)null);
				}
				else
					Items.Insert(0, (string)null);
			}

			if (this.highLight == true)
			{
				CssClass += " " + "highLightControl";					
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