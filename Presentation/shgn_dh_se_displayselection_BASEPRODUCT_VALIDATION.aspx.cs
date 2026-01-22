using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.CodeVision.Presentation;
using SHMA.CodeVision.Presentation.WebControls ;
using SHAB.Data;

namespace SHAB.Presentation{
	public partial class shgn_dh_se_displayselection_BASEPRODUCT_VALIDATION : SHMA.CodeVision.Presentation.DisplaySelectionBase {



		#region overriden Methods
		protected sealed override void SetListsSource(){
			ddlPPR_PRODCD_S.ListSource = new DataReaderDelegate(LPPR_PRODUCTDB.GetDDL_BASEPRODUCT_VALIDATION_PPR_PRODCD_S_RO);
		
			//PSY_SYSTCODE.ListSource = new DataReaderDelegate(PR_GN_SY_SYSTEMDB.GetDDL_PR_GN_ST_DS_GENERALPARAMASTER_PSY_SYSTCODE_RO); 
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

