using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.CodeVision.Presentation;
using SHMA.CodeVision.Presentation.WebControls ;
using SHAB.Data;

namespace SHAB.Presentation{
	public partial class shgn_dh_se_displayselection_BANCAPARAMS : SHMA.CodeVision.Presentation.DisplaySelectionBase {



		#region overriden Methods
		protected sealed override void SetListsSource(){
			ddlCSH_ID.ListSource = new DataReaderDelegate(LCSD_SYSTEMDTLDB.GetDDL_BANCAPARAMS_CSH_ID);
		}
		#endregion 

		#region Getters
		protected sealed override string FilterComboCall{
			get{
				return "";
			}
		} 
		protected sealed override string DependentComboQueries{
			get{
				//return "<dependent-combo-queries>";
				return "";
			}
		} 
		protected sealed override string HeaderScriptCode{
			get{
				return "";
			}
		} 
		protected sealed override string FooterScriptCode{
			get{
				return "";
				//return "bln_ShouldSubmit=true; int_ChildFrameNo=1;";
			}
		} 

		#endregion

		#region Control Getters
		protected sealed override  System.Web.UI.HtmlControls.HtmlForm get_myForm{	
			get{return myForm;}
		}
		protected sealed override SHMA.CodeVision.Presentation.WebControls.PageClientScript get_PageClientScript{
			get{
				return pageClientScript;
			}
		}
		#endregion 	

	}
}

