using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace SHMA.Enterprise.Presentation.WebControls
{
	[DefaultProperty("Text"), ToolboxData("<{0}:FileUpload runat=server></{0}:FileUpload>")]
	public class FileUpload : System.Web.UI.WebControls.HyperLink
	{
		private string  saveToFolder;
		private bool showSaveAs;
		private bool autoClose;
		private string  showFilePath ;
		private string  duplicateFileAction ;
		private string  mappingField ;
		private string  label;
	
		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string SaveToFolder 
		{
			get
			{
				return saveToFolder;
			}

			set
			{
				saveToFolder = value;
			}
		}

		public bool ShowSaveAs 
		{
			get
			{
				return showSaveAs;
			}

			set
			{
				showSaveAs = value;
			}
		}

		public bool AutoClose 
		{
			get
			{
				return autoClose;
			}

			set
			{
				autoClose = value;
			}
		}

		public string ShowFilePath
		{
			get
			{
				return showFilePath;
			}

			set
			{
				showFilePath = value;
			}
		}

		public string DuplicateFileAction 
		{
			get
			{
				return duplicateFileAction;
			}

			set
			{
				duplicateFileAction = value;
			}
		}

		public string MappingField 
		{
			get
			{
				return mappingField;
			}

			set
			{
				mappingField = value;
			}
		}

		protected override void OnPreRender(EventArgs e) 
		{
			base.OnPreRender (e);
			this.Attributes.Add("onclick" , "goUpload(this);" ) ;
			this.Attributes.Add("href" , "#");
			this.Attributes.Add("saveToFolder" , saveToFolder);
			this.Attributes.Add("showSaveAs" , showSaveAs.ToString() );			
			this.Attributes.Add("autoClose" , autoClose.ToString());
			this.Attributes.Add("showFilePath" , showFilePath );
			this.Attributes.Add("duplicateFileAction" , duplicateFileAction );			
			this.Attributes.Add("mappingField", mappingField);
		}

		protected override void Render(HtmlTextWriter output)
		{
			base.Render(output);
			string hdn = "<INPUT type='hidden' name='hdn" + this.ClientID.Substring(3) + "' id='hdn" + this.ClientID.Substring(3) + "' runat='server'>\n"  ;
			output.Write(hdn);
		}
	}
}
