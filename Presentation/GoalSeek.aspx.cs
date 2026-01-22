using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise.Presentation;


using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ACE.Presentation
{
	/// <summary>
	/// Summary description for GoalSeek.
	/// </summary>
	public partial class GoalSeek : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomArgName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomArgVal;
		protected System.Web.UI.HtmlControls.HtmlInputHidden _CustomEventVal;

		protected System.Web.UI.WebControls.Literal MessageScript;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				InitData();
				cmd_GoalSeek.Attributes.Add("onclick", "openWait('Goal Seek');");
				
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void ddLTargetVal_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddLTargetVal.SelectedValue=="Y")
			{
				Tab2.Visible=true;
				Tab3.Visible=true;
				//txtTRIval.Text="13557";
				//txtTRIval.Text=""+(String)new SHMA.Enterprise.Shared.EnvHelper().getAttribute("NLO_AMOUNT_LN2");
				txtTRIval.Text=SessionObject.GetString("NLO_AMOUNT");
				lblTarget1.Text=SessionObject.GetString("LABLE");

				//NASEER lblTarget2.Text=SessionObject.GetString("LABLE");
				
				//Response.Write("<script type='text/javascript'>");
				//window.opener.document.forms["Form1"].elements["NLO_AMOUNT"].value
				//window.opener.document.myForm3.lister__ctl0_lblNLO_AMOUNT.value
				//window.opener.document.getElementById('XYZ').value
				//Response.Write("alert(window.opener.document.forms["myForm3"].elements["NLO_AMOUNT"].value)");
				//Response.Write("</script>");
			}
			else
			{
				Tab2.Visible=false;
				Tab3.Visible=false;
			}
		}
		
		private void InitData()
		{
			//if (ddLTargetVal.Items.Count>0) ddLInsured.Items.FindByValue("Y").Selected=true;
			/*			ddLTargetVal.SelectedValue="Y";
						Tab2.Visible=true;
						Tab3.Visible=true;
						txtTRIval.Text=SessionObject.GetString("NLO_AMOUNT");
						lblTarget1.Text=SessionObject.GetString("LABLE");
						lblTarget2.Text=SessionObject.GetString("LABLE");
			*/
			if (ddLTargetVal.SelectedValue=="N") 
			{
				Tab2.Visible=false;
				Tab3.Visible=false;
				Tab4.Visible=false;
				Tab5.Visible=false;
				//cmd_GoalSeek.Visible=false;
				Tab6.Visible=false;
				Tab7.Visible=false;
				Tab8.Visible=false;
				//cmdYes.Visible=false;
				//Tab9.Style=style="LEFT: 422px; WIDTH: 4.25%; POSITION: relative; TOP: 0px; HEIGHT: 38px";
			}
		}

		protected void ddLChangingFactor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddLChangingFactor.SelectedValue=="EP")
			{
				Tab5.Visible=true;
				//				txtFactorVal.Text="870000";
				txtFactorVal.Text=SessionObject.GetString("NPR_EXCESSPREMIUM");
				//cmd_GoalSeek.Visible=true;
				Tab6.Visible=true;
			}
		}

		protected void cmd_GoalSeek_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//ddLTargetVal.SelectedValue="Y";

			Tab7.Visible=true;
			Tab8.Visible=true;

			SessionObject.Set("TARGETVAL",txtTargetVal.Text);

			string result = "111";
			Type type = Type.GetType("ace.GoalSeek");											
			if (type != null)
			{
				shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
				/*NameValueCollection[] dataRows = new NameValueCollection[1];
				bool[] SelectedRowIndexes = new bool[1];
				dataRows[0] = columnNameValue;
				SelectedRowIndexes[0] = true;
				proccessCommand.setAllFields(columnNameValue);
				proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
				proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
				proccessCommand.setTableName("LNPR_PRODUCT");
				proccessCommand.setDataRows(dataRows);
				proccessCommand.setSelectedRows(SelectedRowIndexes);*/
								
				try
				{
					result = proccessCommand.processing();
				}
				catch (Exception exp)
				{
					result = exp.Message;
				}
				
				if (result.Length>0)
				{
					MessageScript.Text = string.Format("alert('{0}')", result.Replace("'","").Replace("\n","").Replace("\r",""));

					
					String strExcessPrem = "Select npr_excesspremium from lnpr_product where np1_proposal = '"+SessionObject.Get("NP1_PROPOSAL").ToString()+"'" 
						+ " and np2_setno=1 and nvl(npr_basicflag,'N')='Y'";

					//System.out.println(strBasicPlan);
					rowset rstExcessPrem = DB.executeQuery( strExcessPrem );
					if ( rstExcessPrem.next() )
					{
						txtTRIvalChanged.Text = rstExcessPrem.getString( "NPR_EXCESSPREMIUM" );
					}
					
					//txtFactorVal.Text = result.Substring(result.IndexOf("{") + 1, result.LastIndexOf("}"));
				}
			}
		}

		protected void btnTVal_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Tab4.Visible=true;
		}

		protected void cmdYes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string result = "";
			Type type = Type.GetType("ace.GoalSeek_Ok");
			if (type != null)
			{
				shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
				/*NameValueCollection[] dataRows = new NameValueCollection[1];
				bool[] SelectedRowIndexes = new bool[1];
				dataRows[0] = columnNameValue;
				SelectedRowIndexes[0] = true;
				proccessCommand.setAllFields(columnNameValue);
				proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
				proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
				proccessCommand.setTableName("LNPR_PRODUCT");
				proccessCommand.setDataRows(dataRows);
				proccessCommand.setSelectedRows(SelectedRowIndexes);*/								
				try
				{
					//result = proccessCommand.processing();
				}
				catch (Exception exp)
				{
					result = exp.Message;
				}				
				if (result.Length>0)
				{
					MessageScript.Text = string.Format("alert('{0}'); _callBack();", result);
				}
				else
				{
					MessageScript.Text = string.Format("_callBack();");
				}


				

			}
		}

		protected void cmdNo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string result = "113";
			Type type = Type.GetType("ace.GoalSeek_Cancel");
			if (type != null)
			{
				shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
				/*NameValueCollection[] dataRows = new NameValueCollection[1];
				bool[] SelectedRowIndexes = new bool[1];
				dataRows[0] = columnNameValue;
				SelectedRowIndexes[0] = true;
				proccessCommand.setAllFields(columnNameValue);
				proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
				proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
				proccessCommand.setTableName("LNPR_PRODUCT");
				proccessCommand.setDataRows(dataRows);
				proccessCommand.setSelectedRows(SelectedRowIndexes);*/								
				try
				{
					result = proccessCommand.processing();
				}
				catch (Exception exp)
				{
					result = exp.Message;
				}				
				if (result.Length>0)
				{
					MessageScript.Text = string.Format("alert('{0}'); _callBack2();", result);
				}
				else
				{
					MessageScript.Text = string.Format("_callBack();");
				}
			}		
		}
	}
}
