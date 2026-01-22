using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.CodeVision.Presentation;
//using SHMA.CodeVision.Presentation.WebControls ;
using SHAB.Data;

using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Presentation;
using shsm;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
using System.Data.OleDb;

namespace SHAB.Presentation{
	public partial class shgn_dh_se_displayselection_ILUS_DS_UC_USERCHANNEL : SHMA.CodeVision.Presentation.DisplaySelectionBase {

		protected SHMA.CodeVision.Presentation.WebControls.ComboBox USE_USERID;

		
		/*protected SHMA.Enterprise.Presentation.WebControls.ComboBox txtCCH_CODE;
		protected System.Web.UI.WebControls.TextBox txtCCH_DESCR;
		protected SHMA.Enterprise.Presentation.WebControls.ComboBox txtCCD_CODE;
		protected System.Web.UI.WebControls.TextBox txtCCD_DESCR;*/


		private void InitializeComponent()
		{
		
		}

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
				return "";
			}
		} 
		protected sealed override string FooterScriptCode{
			get{
				return "getField(\"USE_USERID\").value=SV(\"USE_USERID\");getField(\"USE_USERID\").disabled=true;function setChannelNull(){setFixedValuesInSession(\"r_USE_USERID=''\");parent.frames[1].location=parent.frames[1].location;}function setChannel(){setFixedValuesInSession(\"r_USE_USERID=\"+getField(\"USE_USERID\").value);parent.frames[1].location=parent.frames[1].location;}";
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

