using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
//[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","SHMA")]
namespace SHMA.Enterprise.Presentation.WebControls
{
	/// <summary>
	/// Summary description for CustomValidator.
	/// </summary>
	public class PeeredRequiredFieldValidator:System.Web.UI.WebControls.CustomValidator {

		string controlsToCheck=" ";
		string primaryControlToValidate;

		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender (e);
			ClientScript();
			this.EnableViewState = true;
			this.ClientValidationFunction = this.ID;
		}

		public string ControlsToCheck {
			get {
				return controlsToCheck;
			}
			set {
				if(value.Length > 1)
					controlsToCheck = value;
				else
					controlsToCheck = " ";
			}
		}

		public new string ControlToValidate {
			get {
				return primaryControlToValidate;
			}
			set {
				primaryControlToValidate = value;
			}
		}

		private string getClientId(){
			System.Web.UI.Control ctrl=null;
			string[] arrOfCtrlsName=null;
			StringBuilder arrayToBuild=new StringBuilder(); 
			if(this.controlsToCheck.Trim().Length>1){
				arrOfCtrlsName = controlsToCheck.Split("~".ToCharArray());
				arrayToBuild.Append("ctrlArr  = new Array(" + (arrOfCtrlsName.Length+1) + ");");
				arrayToBuild.Append("\r");
			}
			else {
				 arrayToBuild.Append("ctrlArr  = new Array(" + (1) + ");");
				arrayToBuild.Append("\r");
			}
			ctrl = this.Parent.FindControl(this.ControlToValidate.Trim());
			arrayToBuild.Append("ctrlArr[0] = '" + ctrl.ClientID + "';");
			arrayToBuild.Append("\r");
			if(this.controlsToCheck.Trim().Length>1){
				for(int i=0;i<arrOfCtrlsName.Length;i++){
					ctrl = null;
					ctrl = this.Parent.FindControl(arrOfCtrlsName[i].Trim());
					arrayToBuild.Append("ctrlArr["+ Convert.ToString(i+1) +"] = '" + ctrl.ClientID + "';");
					arrayToBuild.Append("\r");
				}
			}
			return arrayToBuild.ToString();
		}

		protected void ClientScript() {
			string arrScript = getClientId();
            PageController thisPage = (PageController)this.Page;
            double pageversion = thisPage.pageVersion;
            string JsFilePath = string.Empty;
            if (pageversion == 2.5)
            {
                JsFilePath = "<script language=\"javascript\" type=\"text/javascript\" src=\"../../Presentation/JSFiles/shma_validator.js\"></script>";
            }
            else
            {
                JsFilePath = "<script language=\"javascript\" type=\"text/javascript\" src=\"JSFiles/shma_validator.js\"></script>";
            }
            this.Page.RegisterClientScriptBlock("SHMA_VALIDATOR", JsFilePath);
			

            StringBuilder sb_Script = new StringBuilder();
			sb_Script.Append( "<script language=\"javascript\">" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "function " + this.ID +  "(source, arguments) {" );
			sb_Script.Append( "\r" );
			sb_Script.Append( arrScript);
			sb_Script.Append( " PeeredValidate(arguments, ctrlArr)");
		/*	sb_Script.Append( "\r" );
			sb_Script.Append( "var boolFoundValue;" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "for(itrateArr=0;itrateArr<ctrlArr.length;itrateArr++){");
			sb_Script.Append( "\r" );
			sb_Script.Append( "if(itrateArr==0)");
			sb_Script.Append( "\r" );
			sb_Script.Append( "valOfBaseControl = document.getElementById(ctrlArr[itrateArr]).value;" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "else{" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "if(document.getElementById(ctrlArr[itrateArr]).value !='')");
			sb_Script.Append( "\r" );
			sb_Script.Append( "{boolFoundValue=true; break;}" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "else" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "boolFoundValue=false;" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "}}");
			sb_Script.Append( "\r" );
			sb_Script.Append( "if(ctrlArr.length==1 && valOfBaseControl==''){");
			sb_Script.Append( "\r" );
			sb_Script.Append( "arguments.IsValid=false;return;}" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "if(ctrlArr.length==1 && valOfBaseControl!=''){");
			sb_Script.Append( "\r" );
			sb_Script.Append( "arguments.IsValid=true;return;}" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "if(boolFoundValue && valOfBaseControl=='')");
			sb_Script.Append( "\r" );
			sb_Script.Append( "arguments.IsValid=false;" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "else if(boolFoundValue && valOfBaseControl!='')");
			sb_Script.Append( "\r" );
			sb_Script.Append( "arguments.IsValid=true;" );
			sb_Script.Append( "\r" );
			sb_Script.Append( "}" );
			sb_Script.Append( "\r" ); 
		*/	sb_Script.Append( "}</script>" );
			this.Page.RegisterClientScriptBlock( this.ID , sb_Script.ToString() );
			//			sb_Script.Append( "if(!arguments.IsValid)");
			//			sb_Script.Append( "\r" );
			//			sb_Script.Append( " document.getElementById('" + this.ID +  "').InnerHtml='" + this.ErrorMessage + "';" ); 
			//			sb_Script.Append( "\r" );
			//			sb_Script.Append( "source.value = 'Required';");
		}
	}
}
