using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls {
	[DefaultProperty("Text"), ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
	public class DropDownList : System.Web.UI.WebControls.DropDownList{
		bool _BlankValue=false;
		private string baseType="";
		private bool highLight=false;
		
		[Bindable(true), Category("Appearance"), DefaultValue("false")]
		public bool BlankValue {			
			get{return _BlankValue;}
			set{_BlankValue = value;}
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

		protected override void OnPreRender(System.EventArgs e)
		{
			Style.Add( "width", this.Width.ToString());
			if (_BlankValue){
				if (this.Items.Count > 0){
					if (this.Items[0].Value.Length > 0 && this.Items[0].Text.Length > 0)
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
		protected override void OnLoad(System.EventArgs e) {
			base.OnLoad (e);			
			ListItem item; 
			//try{
			if (Page.IsPostBack && Page.Request[this.ClientID]!=null)
			{
				if (base.Items.FindByValue(Page.Request[this.ClientID])==null)
				{
					this.ClearSelection();
					item = new ListItem( Page.Request[this.ClientID], Page.Request[this.ClientID]);
					item.Selected=true;
					this.Items.Add(item);
				}
			}
			else if (Page.IsPostBack && Page.Request[this.UniqueID]!=null)
			{
				if (base.Items.FindByValue(Page.Request[this.UniqueID])==null)
				{
					this.ClearSelection();
					item = new ListItem( Page.Request[this.UniqueID], Page.Request[this.UniqueID]);
					item.Selected=true;
					this.Items.Add(item);
				}
			}
			//}catch(System.ApplicationException e){}
		}	
        

//		public override string SelectedValue{
//			get {
//				if (Page.IsPostBack && Page.Request[this.ClientID]!=null){
//					if (base.SelectedValue!=Page.Request[this.ClientID])
//						return Page.Request[this.ClientID];
//					else
//						return base.SelectedValue;
//				}
//				else
//					return base.SelectedValue;
//			}
//			set {
//				if (this.Items.FindByValue(value)!=null){
//					base.SelectedValue = value;
//				}
//			}
//		}

//		string _text ;
//		[Bindable(true), Category("Appearance"), DefaultValue("")]
//		public string Text {			
//			get{	return _text;}
//			set{	_text = value;}
//		}
//
////		protected override void Render(System.Web.UI.HtmlTextWriter output){
////			this.
////			RenderBaseControl(output);
////		}
//
//		protected override void OnPreRender(System.EventArgs e){
//			if (_allowNull){				
//				if (Items.Count>0 && Items[0].Value.Length>0)			
//					Items.Insert(0, (string)null);
//			}
//			base.OnPreRender(e);
//			Page.RegisterClientScriptBlock("SmartDropDownScript", GetScriptBlock());
//			if (_addBlankValue){
//				base.ClearSelection();			
//				System.Web.UI.WebControls.ListItem lItem= new System.Web.UI.WebControls.ListItem("","");			
//				lItem.Selected=true;
//				Items.Add(lItem);
//			}
//		}

//		void RenderBaseControl(System.Web.UI.HtmlTextWriter output){
//			base.Attributes["onKeypress"] = "return (dodefaultaction()==''); ";
//			base.Attributes["onKeydown"] = "return (dodefaultaction()==''); ";
//			base.Attributes["onKeyup"] = "return (change(" + base.ID + "));";
//			base.Attributes["onfocus"] = "txtval = '';";
//			base.Attributes["onblur"] = "txtval = '';";
//			base.Render(output);
//		}

//		void RenderHiddenInput(System.Web.UI.HtmlTextWriter output){
//			output.Write("<input type='hidden' value='' name='hidden'" + base.ID + "'>");
//		}

//		string GetScriptBlock(){
//			return ("<SCRIPT>" + 
//				"var txtval = '';" + 
//				"var curlist;" + 
//				"function select(trigger){ " + 
//				"curlist=trigger;" + 
//				"for (n=0;n<curlist.length;n++){" + 
//				"if(curlist[n].text.toLowerCase().indexOf(txtval.toLowerCase())==0){" + 
//				"curlist.selectedIndex=n;break;" + 
//				" }else{" + 
//				" if (n==curlist.length-1) txtval=''; curlist.selectedIndex=0;}}}" + 
//				"function dodefaultaction(e){ " + 
//				"var code; " + 
//				"if (!e) var e = window.event; " + 
//				"if (e.keyCode) code = e.keyCode; " + 
//				"else if (e.which) code = e.which;" + 
//				"if(code == '9' | code == '40' | code == '38') return ''; " + 
//				"else return code;}" + 
//				"function change(trigger){ " + 
//				"var code = dodefaultaction(); " + 
//				"if(code == ''){ txtval='';return false;} " + 
//				"else{" + 
//				"if(code == '8'){ txtval='';} " + 
//				"else{" + 
//				"txtval = txtval + String.fromCharCode(code);}select(trigger); return true;}}</SCRIPT>");
//		}
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