using System;
using System.Web.UI;
using eWorld.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using TextBox = System.Web.UI.WebControls.TextBox;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {	
	public class DatePopUp :  eWorld.UI.CalendarPopup{		
		private bool baseTable = true;
		string text="";
		bool readOnly = false;
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

		protected override void OnInit(EventArgs e) {
			base.OnInit (e);

			this.Culture = System.Globalization.CultureInfo.CurrentCulture;
			this.ControlDisplay = DisplayType.TextBoxImage;			
			//ImageUrl = @"images\image.gif";
			this.DisableTextboxEntry=false;
			this.CalendarLocation = DisplayLocation.Bottom;
			this.UseExternalResource= true;
			this.Style.Add("z-index", "40");
			this.Nullable = true;
		}
		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender (e);			
			if ((ExternalResourcePath.Trim().Length == 0) || (ExternalResourcePath == null))
				throw new ApplicationException("JS file path for calendar has not given !");			

			System.Web.UI.WebControls.TextBox textBox = new System.Web.UI.WebControls.TextBox();
			HyperLink hyperLink = new HyperLink();
			foreach (WebControl ctrl in this.Controls){				
				if (ctrl is System.Web.UI.WebControls.TextBox){
					textBox =  (System.Web.UI.WebControls.TextBox)ctrl ;
				}
				else if (ctrl is HyperLink){
					hyperLink =  (HyperLink)ctrl ;
				}
			}			

			if (this.Attributes["onclick"] != null){
				textBox.Attributes["onclick"] += this.Attributes["onclick"];
				this.Attributes.Remove("onclick");
			}
			if (this.Attributes["onkeypress"] != null){
				textBox.Attributes["onkeypress"] += this.Attributes["onkeypress"];
				this.Attributes.Remove("onkeypress");
			}

			Style.Add( "width", this.Width.ToString());
			textBox.CssClass = this.CssClass;
			if (Enabled && ReadOnly){			// ReadOnly = true
				textBox.ReadOnly = true;
				hyperLink.Enabled = false;
			}
			else if(Enabled && !ReadOnly){		// ReadOnly = false			
				textBox.ReadOnly = false;
				hyperLink.Enabled = true;
			}			
			else if(!Enabled && !ReadOnly){		// Disabled 				
				textBox.ReadOnly = false;
			}								
			else if(!Enabled && ReadOnly){		// Disabled 
			}			
			
			textBox.Attributes.Add("ondblclick", "if (this.readOnly==false)  " +  textBox.UniqueID + "_Up_CallClick(event);");						
			hyperLink.NavigateUrl = string.Format("javascript:if (document.getElementById('{0}').readOnly == false && document.getElementById('{0}').disabled == false) {0}_Up_CallClick(1);", textBox.UniqueID);
			textBox.Text = Text;	
			
			//	Modified By : Ahmed
			//	Date		: 18-4-05
			//	Purpose		: on blur in case of date selection from calendar
			if (this.Attributes["onchange"] != null || this.Attributes["onblur"] != null ){
				string strValue ="";

				//this.JavascriptOnChangeFunction += string.Format("document.getElementById(\\\'{0}\\\').focus();", textBox.ClientID)  ;
				//this.JavascriptOnChangeFunction += string.Format("myForm.{0}.focus();", textBox.ClientID)  ;
				if (Attributes["onchange"] != null ){
					textBox.Attributes["onchange"] += Attributes["onchange"];
					strValue = textBox.Attributes["onchange"].ToString() ;
					//strValue += string.Format("document.getElementById('{0}').onchange();", textBox.ClientID)  ;
					Attributes.Remove("onchange");
					//strValue += string.Format( ";" , this.Attributes["onblur"].ToString() );
				}
				if (Attributes["onblur"] != null ){
					textBox.Attributes["onblur"] += Attributes["onblur"];
					strValue += string.Format("document.getElementById('{0}').focus();", textBox.ClientID)  ;
					Attributes.Remove("onblur");
				}				

				if (strValue.IndexOf("this")>=0)					
					strValue = strValue.Replace( "this", "document.getElementById('" + textBox.ClientID + "')");

				this.JavascriptOnChangeFunction = strValue.Replace("'", @"\\\'") ;								
			}


		//	Added   : Ahmed
			//  Date    : 15-04-05
			//	Problem : requied focus in textbox after date changes from calender button
			//this.JavascriptOnChangeFunction += string.Format("document.getElementById('{0}').focus();", textBox.ClientID)  ;


//			if (this.Attributes["onchange"] != null){
//				string strValue = this.Attributes["onchange"];				
//				textBox.Attributes["onchange"] = strValue;
//
//				//this.JavascriptOnChangeFunction = string.Format("if (document.getElementById('{0}').onchange!=null) document.getElementById('{0}').onchange();", textBox.ClientID);				
//				this.JavascriptOnChangeFunction = string.Format("alert(document.getElementById(\\\'{0}\\\')", textBox.ClientID);
//				this.Attributes.Remove("onchange");
//			}

			if (this.highLight == true)
			{
				CssClass += " " + "highLightControl";					
			}
		}					
	
		public override void RenderBeginTag(HtmlTextWriter writer) {						
//			base.RenderBeginTag (writer);

		}
		public override void RenderEndTag(HtmlTextWriter writer) {
//			base.RenderEndTag (writer);
		}

		public new string Text{
			get{
				return text;
			}			
			set{
				try{						
					SelectedDate = DateTime.Parse(value);						
					ViewState["Text"] = value;
					text = value;
				}
				catch(Exception e){
					//if the value is invalid or empty initialize value
					ViewState["Text"] = "";
					text = "";
				}
			}
		}	
		public bool ReadOnly{
			get{
				return readOnly;
			}
			set{
				readOnly = value;
			}
		}
			
		protected override void LoadViewState(object savedState) {
			/*	becase we r using Text property instead of SelectedDate 
			 *  we have to handle all postback cases ourself i.e
			 * 1- if the textbox has disabled at client side .
			 * 2- in case of empty value from client or server.
			 * 3- in case of invalid value from client or server.
			 * 4- in case of valid value from client.
			*/
			base.LoadViewState (savedState);
			if (Page.IsPostBack){			
				string enteredVal = Page.Request[this.UniqueID];
				if (enteredVal == null){						// if textbox has disabled at client seide
					if (ViewState["Text"]!=null)
						Text = ViewState["Text"].ToString();			
				}
				else{
					Text = enteredVal;
				}
			}
			else{
				ViewState["Text"] = ""; 
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