using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.CodeVision.Presentation;
using SHMA.CodeVision.Presentation.WebControls ;
using SHAB.Data;

namespace SHAB.Presentation{
	public partial class shgn_dh_se_displayselection_ILUS_ET_DS_ER_EXCHANGERATE : SHMA.CodeVision.Presentation.DisplaySelectionBase {

		protected SHMA.CodeVision.Presentation.WebControls.ComboBox PCU_BASECURR;


		#region overriden Methods
		protected sealed override void SetListsSource(){
					
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
				return "function setCurrencyNull(){setFixedValuesInSession(\"r_PCU_BASECURR='0'\");parent.frames[1].location=parent.frames[1].location;}function setCurrency(){setFixedValuesInSession(\"r_PCU_BASECURR=\"+getField(\"PCU_BASECURR\").value);parent.frames[1].location=parent.frames[1].location;}";
			}
		} 
		protected sealed override string FooterScriptCode{
			get{
				return " setCurrencyNull();";
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

