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
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using shsm;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS.
	/// </summary>
	public partial class shgn_ss_se_stdscreen_ILUS_ET_UC_ADDRESS : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		//protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		//protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
	    //public string AddCat;
		private string countryCode="";
		private string provinceCode="";
		private string cityCode="";


		private void setFieldStatus()
		{
			if(AllowUpdate() == false)
			{
				ddlCCN_COUNTRY.Enabled=false;
				ddlCCN_PROVINCE.Enabled=false;
				ddlCCN_CITY.Enabled=false;
				txtNP1_ADDRESS1.Enabled=false;
				txtNP1_ADDRESS2.Enabled=false;
				txtNP1_ADDRESS3.Enabled=false;
				txtNP1_POBOX.Enabled=false;
				txtNP1_TELRES.Enabled=false;
				txtNP1_OFFICE.Enabled=false;
				txtNP1_FAXNO.Enabled=false;
				txtNP1_MOBILENO.Enabled=false;
				txtNP1_PAGER.Enabled=false;
				txtNP1_EMAIL1.Enabled=false;
				txtNP1_EMAIL2.Enabled=false;
			}
			else
			{
				ddlCCN_COUNTRY.Enabled=true;
				ddlCCN_PROVINCE.Enabled=true;
				ddlCCN_CITY.Enabled=true;
				txtNP1_ADDRESS1.Enabled=true;
				txtNP1_ADDRESS2.Enabled=true;
				txtNP1_ADDRESS3.Enabled=true;
				txtNP1_POBOX.Enabled=true;
				txtNP1_TELRES.Enabled=true;
				txtNP1_OFFICE.Enabled=true;
				txtNP1_FAXNO.Enabled=true;
				txtNP1_MOBILENO.Enabled=true;
				txtNP1_PAGER.Enabled=true;
				txtNP1_EMAIL1.Enabled=true;
				txtNP1_EMAIL2.Enabled=true;
			}
		}

		private bool AllowUpdate()
		{
			/*string BA_ClientCode = Session["NPH_CODE"].ToString();
			string BA_ClientID="";
			if(BA_ClientCode !="") 
			{
				BA_ClientID   = ace.clsIlasUtility.getClientID1FromBanca(BA_ClientCode);
			}

			if(ace.ILUS_ET_NM_PER_PERSONALDET.ClientExistInIlas(BA_ClientID) == 0)
			{
				return true;
			}
			else
			{
				return false;
			}*/
			return true;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//bool allowUpdate = "";
			// Put user code to initialize the page here
			ace.POLICY_ACCEPTANCE Polacc=new ace.POLICY_ACCEPTANCE();
			bool check=false;
			bool validate=false;
			if(Session["NP1_PROPOSAL"]!=null)
			{
				check=Polacc.CheckIfPolicyApproved(Session["NP1_PROPOSAL"].ToString());
				validate=Polacc.CheckIfPolicyValidate(Session["NP1_PROPOSAL"].ToString());			
			}
			
			if (Session["NP1_PROPOSAL"]==null )
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL','Please select values from personal.');");
				Response.Write("</script>");
			}
			else if(check==true)
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL', 'Proposal has been approved.');");
				Response.Write("</script>");
			}
			
			if(validate!=false)
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.setAlertPage('shgn_gp_gp_ILUS_ET_GP_PERONAL','" + BA.BAUtility.Validate_Text() + "')");
				Response.Write("</script>");
			}
			else
			{
				CSSLiteral.Text = ace.Ace_General.LoadPageStyle();

				if(Convert.ToString(ViewState["AddCat"]) == "P")
				{
					LinkButton1.CssClass="LinkButtonSelected";
					LinkButton2.CssClass="LinkButtonNormal";
					LinkButton3.CssClass="LinkButtonNormal";
				}
				else if(Convert.ToString(ViewState["AddCat"]) == "B")
				{
					LinkButton2.CssClass="LinkButtonSelected";
					LinkButton1.CssClass="LinkButtonNormal";
					LinkButton3.CssClass="LinkButtonNormal";
				}
				else
				{
					LinkButton3.CssClass="LinkButtonSelected";
					LinkButton1.CssClass="LinkButtonNormal";
					LinkButton2.CssClass="LinkButtonNormal";
				}

				if(!IsPostBack)
				{
					GetCountryList();
					//GetProvinceById();
					//GetCityById();
					//LinkButton3.CssClass="LinkButtonSelected";
					ViewState["AddCat"]="C";
					btn_copy.Visible = false;
					GetAddressByCategory();
				}
            }
            setFieldStatus();
					
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
	
//Get Country List
		public void GetCountryList()
		{
			IDataReader LCSD_SYSTEMDTLReader0 = null;
			try
			{
				LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getAll_RO();
				ddlCCN_COUNTRY.DataSource = LCSD_SYSTEMDTLReader0 ;
				ddlCCN_COUNTRY.DataBind();
				/*ddlCCN_COUNTRY.Items.Insert(0,new ListItem("",""));
				if(ddlCCN_COUNTRY.Items.Count > 1)
				{
					ddlCCN_COUNTRY.SelectedIndex =1;
				}*/
			}
			catch
			{
			}
			finally
			{
				if (LCSD_SYSTEMDTLReader0.IsClosed == false)
				{
					LCSD_SYSTEMDTLReader0.Close();
				}
			}
		}

//Get City By Country Id.
		public void GetCityById()
		{
			IDataReader LCSD_SYSTEMDTLReader0 = null;
			try
			{
				LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getcity(this.countryCode, this.provinceCode);
				ddlCCN_CITY.DataSource = LCSD_SYSTEMDTLReader0 ;
				ddlCCN_CITY.DataBind();
				ddlCCN_CITY.Items.Insert(0,new ListItem("",""));
				if(ddlCCN_CITY.Items.Count > 1)
				{
					ddlCCN_CITY.SelectedIndex =1;
				}
			}
			catch(Exception EX)
			{
				string  STR=EX.Message;
			}
			finally
			{
				if (LCSD_SYSTEMDTLReader0.IsClosed == false)
				{
					LCSD_SYSTEMDTLReader0.Close();
				}
			}
		}
// Get Province BY Country ID
  
		public void GetProvinceById()
		{
			IDataReader LCSD_SYSTEMDTLReader0 = null;
			try
			{
				LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.GetProvince(this.countryCode);
				ddlCCN_PROVINCE.DataSource = LCSD_SYSTEMDTLReader0 ;
				ddlCCN_PROVINCE.DataBind();
				LCSD_SYSTEMDTLReader0.Close();
				ddlCCN_PROVINCE.Items.Insert(0,new ListItem("",""));
				if(ddlCCN_PROVINCE.Items.Count > 1)
				{
					ddlCCN_PROVINCE.SelectedIndex =1;
				}
			}
			catch(Exception EX)
			{
				string  STR=EX.Message;
			}
			finally
			{
				if (LCSD_SYSTEMDTLReader0.IsClosed == false)
				{
					LCSD_SYSTEMDTLReader0.Close();
				}
			}
		}

		protected void ddlCCN_COUNTRY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(this.countryCode=="")
				{
					this.countryCode = ddlCCN_COUNTRY.SelectedValue;
				}

				GetProvinceById();
				if(this.provinceCode=="")
				{
					this.provinceCode = ddlCCN_PROVINCE.SelectedValue;
				}
				GetCityById();
			}
			catch
			{
			
			}
		}

		protected void ddlCCN_PROVINCE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(this.countryCode=="")
				{
					this.countryCode = ddlCCN_COUNTRY.SelectedValue;
				}
				if(this.provinceCode=="")
				{
					this.provinceCode = ddlCCN_PROVINCE.SelectedValue;
				}
				GetCityById();	
			}
			catch
			{	
			}
		}

//Insert Record.
		private void InsertAll()
		{			
			try
			{
	
					SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
					NameValueCollection columnNameValue=new NameValueCollection();
			
					int test=1; 
				
					//Setting Columns Values			
					columnNameValue.Add("NPH_CODE",Session["NPH_CODE"].ToString()==""?null:Session["NPH_CODE"].ToString());
					columnNameValue.Add("NP1_PROPOSAL",Session["NP1_PROPOSAL"].ToString()==""?null:Session["NP1_PROPOSAL"].ToString());
					columnNameValue.Add("NPH_LIFE",Session["NPH_LIFE"].ToString()==""?null:Session["NPH_LIFE"].ToString());
					columnNameValue.Add("NAD_ADDRESSTYP",ViewState["AddCat"].ToString()==""?null:ViewState["AddCat"].ToString());
					columnNameValue.Add("PFS_ACNTYEAR",test.ToString()==""?null:test.ToString());
					columnNameValue.Add("OAC_ACTIVICODE",test.ToString()==""?null:test.ToString());
					columnNameValue.Add("ARQ_REQUESTNO",test.ToString()==""?null:test.ToString());
					columnNameValue.Add("USE_USERID",Session["s_USE_USERID"].ToString()==""?null:Session["s_USE_USERID"].ToString() );
					columnNameValue.Add("USE_TIMEDATE",DateTime.Now.ToString("dd-MMM-yyyy")==""?null:DateTime.Now.ToString("dd-MMM-yyyy"));
					columnNameValue.Add("NAD_POBOX",txtNP1_POBOX.Text.Trim()==""?null:txtNP1_POBOX.Text.Trim());
					columnNameValue.Add("NAD_ADDRESS1",txtNP1_ADDRESS1.Text.Trim()==""?null:txtNP1_ADDRESS1.Text);
					columnNameValue.Add("CCN_CTRYCD",ddlCCN_COUNTRY.SelectedValue.Trim()==""?null:ddlCCN_COUNTRY.SelectedValue);
					columnNameValue.Add("NAD_ADDRESS2",txtNP1_ADDRESS2.Text.Trim()==""?null:txtNP1_ADDRESS2.Text);
					columnNameValue.Add("CPR_PROVCD",ddlCCN_PROVINCE.SelectedValue.Trim()==""?null:ddlCCN_PROVINCE.SelectedValue);
					columnNameValue.Add("CCT_CITYCD",ddlCCN_CITY.SelectedValue.Trim()==""?null:ddlCCN_CITY.SelectedValue);
					columnNameValue.Add("NAD_ADDRESS3",txtNP1_ADDRESS3.Text.Trim()==""?null:txtNP1_ADDRESS3.Text);
					columnNameValue.Add("NAD_TELNO1",txtNP1_TELRES.Text.Trim()==""?null:txtNP1_TELRES.Text);
					columnNameValue.Add("NAD_TELNO2",txtNP1_OFFICE.Text.Trim()==""?null:txtNP1_OFFICE.Text);
					columnNameValue.Add("NAD_FAXNO",txtNP1_FAXNO.Text.Trim()==""?null:txtNP1_FAXNO.Text);
					columnNameValue.Add("NAD_MOBILE",txtNP1_MOBILENO.Text.Trim()==""?null:txtNP1_MOBILENO.Text);
					columnNameValue.Add("NAD_PAGER",txtNP1_PAGER.Text.Trim()==""?null:txtNP1_PAGER.Text);
					columnNameValue.Add("NAD_EMAIL1",txtNP1_EMAIL1.Text.Trim()==""?null:txtNP1_EMAIL1.Text);
					columnNameValue.Add("NAD_EMAIL2",txtNP1_EMAIL2.Text.Trim()==""?null:txtNP1_EMAIL2.Text);
					columnNameValue.Add("ARQ_REQUESTYPE",test.ToString()==""?null:test.ToString());
					columnNameValue.Add("CONVERT",test.ToString()==""?null:test.ToString());
					columnNameValue.Add("NPH_LASTCHANGEDBY",null==""?null:"");
					columnNameValue.Add("NPH_LASTCHNAGEDATE",null==""?null:"");
				
		
					//Setting Parameter Of Values

					SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
					pc.puts("@NPH_CODE",columnNameValue.getObject("NPH_CODE"));
					pc.puts("@NP1_PROPOSAL",columnNameValue.getObject("NP1_PROPOSAL"));
					pc.puts("@NPH_LIFE",columnNameValue.getObject("NPH_LIFE"));
					pc.puts("@NAD_ADDRESSTYP",columnNameValue.getObject("NAD_ADDRESSTYP"));
					pc.puts("@PFS_ACNTYEAR",columnNameValue.getObject("PFS_ACNTYEAR"));
					pc.puts("@OAC_ACTIVICODE",columnNameValue.getObject("OAC_ACTIVICODE"));
					pc.puts("@ARQ_REQUESTNO",columnNameValue.getObject("ARQ_REQUESTNO"));
					pc.puts("@USE_USERID",columnNameValue.getObject("USE_USERID"));
					pc.puts("@USE_TIMEDATE",columnNameValue.getObject("USE_TIMEDATE"));
					pc.puts("@NAD_POBOX",columnNameValue.getObject("NAD_POBOX"));
					pc.puts("@NAD_ADDRESS1",columnNameValue.getObject("NAD_ADDRESS1"));
					pc.puts("@CCN_CTRYCD",columnNameValue.getObject("CCN_CTRYCD"));
					pc.puts("@NAD_ADDRESS2",columnNameValue.getObject("NAD_ADDRESS2"));
					pc.puts("@CPR_PROVCD",columnNameValue.getObject("CPR_PROVCD"));
					pc.puts("@CCT_CITYCD",columnNameValue.getObject("CCT_CITYCD"));
					pc.puts("@NAD_ADDRESS3",columnNameValue.getObject("NAD_ADDRESS3"));
					pc.puts("@NAD_TELNO1",columnNameValue.getObject("NAD_TELNO1"));
					pc.puts("@NAD_TELNO2",columnNameValue.getObject("NAD_TELNO2"));
					pc.puts("@NAD_FAXNO",columnNameValue.getObject("NAD_FAXNO"));
					pc.puts("@NAD_MOBILE",columnNameValue.getObject("NAD_MOBILE"));
					pc.puts("@NAD_PAGER",columnNameValue.getObject("NAD_PAGER"));
					pc.puts("@NAD_EMAIL1",columnNameValue.getObject("NAD_EMAIL1"));
					pc.puts("@NAD_EMAIL2",columnNameValue.getObject("NAD_EMAIL2"));
					pc.puts("@ARQ_REQUESTYPE",columnNameValue.getObject("ARQ_REQUESTYPE"));
					pc.puts("@CONVERT",columnNameValue.getObject("CONVERT"));
					pc.puts("@NPH_LASTCHANGEDBY",columnNameValue.getObject("NPH_LASTCHANGEDBY"));
					pc.puts("@NPH_LASTCHNAGEDATE",columnNameValue.getObject("NPH_LASTCHNAGEDATE"));
					
					//Executing DML			
					//rowset rs=DB.executeQuery("SELECT NVL(COUNT(1),0) Exist FROM LNAD_ADDRESS WHERE NPH_CODE ="+ columnNameValue.getObject("NPH_CODE")+"  AND NPH_LIFE = '"+columnNameValue.getObject("NPH_LIFE")+"' AND NAD_ADDRESSTYP = '"+columnNameValue.getObject("NAD_ADDRESSTYP")+"' ");
					
				String qry = "insert into LNAD_ADDRESS values("+columnNameValue.getObject("NPH_CODE")+",'"
					+columnNameValue.getObject("NP1_PROPOSAL")+"','"
					+columnNameValue.getObject("NPH_LIFE")+"','"
					+columnNameValue.getObject("NAD_ADDRESSTYP")+"','"
					+columnNameValue.getObject("PFS_ACNTYEAR")+"','"
					+columnNameValue.getObject("OAC_ACTIVICODE")+"',"
					+columnNameValue.getObject("ARQ_REQUESTNO")+",'"
					+columnNameValue.getObject("USE_USERID")+"', SYSDATE,'"
					+columnNameValue.getObject("NAD_POBOX")+"','"
					+columnNameValue.getObject("NAD_ADDRESS1")+"','"
					+columnNameValue.getObject("CCN_CTRYCD")+"','"
					+columnNameValue.getObject("NAD_ADDRESS2")+"','"
					+columnNameValue.getObject("CPR_PROVCD")+"','"
					+columnNameValue.getObject("CCT_CITYCD")+"','"
					+ columnNameValue.getObject("NAD_ADDRESS3")+"','"
					+columnNameValue.getObject("NAD_TELNO1")+"','"
					+columnNameValue.getObject("NAD_TELNO2")+"','"
					+columnNameValue.getObject("NAD_FAXNO")+"','"
					+columnNameValue.getObject("NAD_MOBILE")+"','"
					+ columnNameValue.getObject("NAD_PAGER")+"','"
					+columnNameValue.getObject("NAD_EMAIL1")+"','"
					+columnNameValue.getObject("NAD_EMAIL2")+"','"
					+columnNameValue.getObject("ARQ_REQUESTYPE")+"',"
					+columnNameValue.getObject("CONVERT")+",'"
					+columnNameValue.getObject("NPH_LASTCHANGEDBY")+"',SYSDATE)";
				DB.executeQuery(qry);

					//ViewState["AddCat"]="";
				
			}
			catch(Exception err)
			{
				throw new Exception(err.Message);

			}

		}
			

//Update Address
		public void UpdateRecord()
		{
		
			try
			{

				SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
				NameValueCollection columnNameValue=new NameValueCollection();
			
				int test=1; 
				//Setting Columns Values			
				columnNameValue.Add("NP1_PROPOSAL",Session["NP1_PROPOSAL"].ToString()==""?null:Session["NP1_PROPOSAL"].ToString());
				columnNameValue.Add("NAD_ADDRESSTYP",ViewState["AddCat"].ToString()==""?null:ViewState["AddCat"].ToString());
				columnNameValue.Add("PFS_ACNTYEAR",test.ToString()==""?null:test.ToString());
				columnNameValue.Add("OAC_ACTIVICODE",test.ToString()==""?null:test.ToString());
				columnNameValue.Add("ARQ_REQUESTNO",test.ToString()==""?null:test.ToString());
				columnNameValue.Add("NAD_POBOX",txtNP1_POBOX.Text.Trim()==""?null:txtNP1_POBOX.Text.Trim());
				columnNameValue.Add("NAD_ADDRESS1",txtNP1_ADDRESS1.Text.Trim()==""?null:txtNP1_ADDRESS1.Text);
				columnNameValue.Add("CCN_CTRYCD",ddlCCN_COUNTRY.SelectedValue.Trim()==""?null:ddlCCN_COUNTRY.SelectedValue);
				columnNameValue.Add("NAD_ADDRESS2",txtNP1_ADDRESS2.Text.Trim()==""?null:txtNP1_ADDRESS2.Text);
				columnNameValue.Add("CPR_PROVCD",ddlCCN_PROVINCE.SelectedValue.Trim()==""?null:ddlCCN_PROVINCE.SelectedValue);
				columnNameValue.Add("CCT_CITYCD",ddlCCN_CITY.SelectedValue.Trim()==""?null:ddlCCN_CITY.SelectedValue);
				columnNameValue.Add("NAD_ADDRESS3",txtNP1_ADDRESS3.Text.Trim()==""?null:txtNP1_ADDRESS3.Text);
				columnNameValue.Add("NAD_TELNO1",txtNP1_TELRES.Text.Trim()==""?null:txtNP1_TELRES.Text);
				columnNameValue.Add("NAD_TELNO2",txtNP1_OFFICE.Text.Trim()==""?null:txtNP1_OFFICE.Text);
				columnNameValue.Add("NAD_FAXNO",txtNP1_FAXNO.Text.Trim()==""?null:txtNP1_FAXNO.Text);
				columnNameValue.Add("NAD_MOBILE",txtNP1_MOBILENO.Text.Trim()==""?null:txtNP1_MOBILENO.Text);
				columnNameValue.Add("NAD_PAGER",txtNP1_PAGER.Text.Trim()==""?null:txtNP1_PAGER.Text);
				columnNameValue.Add("NAD_EMAIL1",txtNP1_EMAIL1.Text.Trim()==""?null:txtNP1_EMAIL1.Text);
				columnNameValue.Add("NAD_EMAIL2",txtNP1_EMAIL2.Text.Trim()==""?null:txtNP1_EMAIL2.Text);
				columnNameValue.Add("ARQ_REQUESTYPE",test.ToString()==""?null:test.ToString());
				columnNameValue.Add("CONVERT",test.ToString()==""?null:test.ToString());
				columnNameValue.Add("NPH_LASTCHANGEDBY",Session["s_USE_USERID"].ToString()==""?null:Session["s_USE_USERID"].ToString());
				// columnNameValue.Add("NPH_LASTCHNAGEDATE",DateTime.Now.ToString("dd-MMM-yyyy")==""?null:DateTime.Now.ToString("dd-MMM-yyyy"));
				
		
				//Setting Parameter Of Values

				SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@NP1_PROPOSAL",columnNameValue.getObject("NP1_PROPOSAL"));
				pc.puts("@NAD_ADDRESSTYP",columnNameValue.getObject("NAD_ADDRESSTYP"));
				pc.puts("@PFS_ACNTYEAR",columnNameValue.getObject("PFS_ACNTYEAR"));
				pc.puts("@OAC_ACTIVICODE",columnNameValue.getObject("OAC_ACTIVICODE"));
				pc.puts("@ARQ_REQUESTNO",columnNameValue.getObject("ARQ_REQUESTNO"));
				pc.puts("@NAD_POBOX",columnNameValue.getObject("NAD_POBOX"));
				pc.puts("@NAD_ADDRESS1",columnNameValue.getObject("NAD_ADDRESS1"));
				pc.puts("@CCN_CTRYCD",columnNameValue.getObject("CCN_CTRYCD"));
				pc.puts("@NAD_ADDRESS2",columnNameValue.getObject("NAD_ADDRESS2"));
				pc.puts("@CPR_PROVCD",columnNameValue.getObject("CPR_PROVCD"));
				pc.puts("@CCT_CITYCD",columnNameValue.getObject("CCT_CITYCD"));
				pc.puts("@NAD_ADDRESS3",columnNameValue.getObject("NAD_ADDRESS3"));
				pc.puts("@NAD_TELNO1",columnNameValue.getObject("NAD_TELNO1"));
				pc.puts("@NAD_TELNO2",columnNameValue.getObject("NAD_TELNO2"));
				pc.puts("@NAD_FAXNO",columnNameValue.getObject("NAD_FAXNO"));
				pc.puts("@NAD_MOBILE",columnNameValue.getObject("NAD_MOBILE"));
				pc.puts("@NAD_PAGER",columnNameValue.getObject("NAD_PAGER"));
				pc.puts("@NAD_EMAIL1",columnNameValue.getObject("NAD_EMAIL1"));
				pc.puts("@NAD_EMAIL2",columnNameValue.getObject("NAD_EMAIL2"));
				pc.puts("@ARQ_REQUESTYPE",columnNameValue.getObject("ARQ_REQUESTYPE"));
				pc.puts("@CONVERT",columnNameValue.getObject("CONVERT"));
				pc.puts("@NPH_LASTCHANGEDBY",columnNameValue.getObject("NPH_LASTCHANGEDBY"));
				// pc.puts("@NPH_LASTCHNAGEDATE",columnNameValue.getObject("NPH_LASTCHNAGEDATE"));
					
				//Executing DML					
				// string qry = "UPDATE LNAD_ADDRESS SET NP1_PROPOSAL='" + columnNameValue.getObject("NP1_PROPOSAL") + "', PFS_ACNTYEAR='"+columnNameValue.getObject("PFS_ACNTYEAR")+"',OAC_ACTIVICODE='"+columnNameValue.getObject("OAC_ACTIVICODE")+"',ARQ_REQUESTNO="+columnNameValue.getObject("ARQ_REQUESTNO")+",NAD_POBOX='"+columnNameValue.getObject("NAD_POBOX")+"',NAD_ADDRESS1='"+columnNameValue.getObject("NAD_ADDRESS1")+"',CCN_CTRYCD='"+columnNameValue.getObject("CCN_CTRYCD")+"',NAD_ADDRESS2='"+columnNameValue.getObject("NAD_ADDRESS2")+"',CPR_PROVCD='"+columnNameValue.getObject("CPR_PROVCD")+"',CCT_CITYCD='"+columnNameValue.getObject("CCT_CITYCD")+"',NAD_ADDRESS3='"+ columnNameValue.getObject("NAD_ADDRESS3")+"',NAD_TELNO1='"+columnNameValue.getObject("NAD_TELNO1")+"',NAD_TELNO2='"+columnNameValue.getObject("NAD_TELNO2")+"',NAD_FAXNO='"+columnNameValue.getObject("NAD_FAXNO")+"',NAD_MOBILE='"+columnNameValue.getObject("NAD_MOBILE")+"',NAD_PAGER='"+ columnNameValue.getObject("NAD_PAGER")+"',NAD_EMAIL1='"+columnNameValue.getObject("NAD_EMAIL1")+"',NAD_EMAIL2='"+columnNameValue.getObject("NAD_EMAIL2")+"',ARQ_REQUESTYPE='"+columnNameValue.getObject("ARQ_REQUESTYPE")+"',CONVERT="+columnNameValue.getObject("CONVERT")+",NPH_LASTCHANGEDBY='"+columnNameValue.getObject("NPH_LASTCHANGEDBY")+"',NPH_LASTCHNAGEDATE='"+columnNameValue.getObject("NPH_LASTCHNAGEDATE")+"' " +
				string qry = "UPDATE LNAD_ADDRESS SET NP1_PROPOSAL='" 
					+ columnNameValue.getObject("NP1_PROPOSAL") 
					+ "', PFS_ACNTYEAR='"+ columnNameValue.getObject("PFS_ACNTYEAR") 
					+ "',OAC_ACTIVICODE='" + columnNameValue.getObject("OAC_ACTIVICODE") 
					+ "',ARQ_REQUESTNO=" + columnNameValue.getObject("ARQ_REQUESTNO") 
					+ ",NAD_POBOX='" + columnNameValue.getObject("NAD_POBOX") 
					+ "',NAD_ADDRESS1='" + columnNameValue.getObject("NAD_ADDRESS1") 
					+ "',CCN_CTRYCD='" + columnNameValue.getObject("CCN_CTRYCD") 
					+ "',NAD_ADDRESS2='" + columnNameValue.getObject("NAD_ADDRESS2") 
					+ "',CPR_PROVCD='" + columnNameValue.getObject("CPR_PROVCD") 
					+ "',CCT_CITYCD='" + columnNameValue.getObject("CCT_CITYCD") 
					+ "',NAD_ADDRESS3='" + columnNameValue.getObject("NAD_ADDRESS3") 
					+ "',NAD_TELNO1='" + columnNameValue.getObject("NAD_TELNO1") 
					+ "',NAD_TELNO2='" + columnNameValue.getObject("NAD_TELNO2") 
					+ "',NAD_FAXNO='" + columnNameValue.getObject("NAD_FAXNO") 
					+ "',NAD_MOBILE='" + columnNameValue.getObject("NAD_MOBILE") 
					+ "',NAD_PAGER='" + columnNameValue.getObject("NAD_PAGER")
					+ "',NAD_EMAIL1='" + columnNameValue.getObject("NAD_EMAIL1") 
					+ "', CONVERT=" + columnNameValue.getObject("CONVERT")
					+ ",NAD_EMAIL2='" + columnNameValue.getObject("NAD_EMAIL2") 
					+ "',ARQ_REQUESTYPE='" + columnNameValue.getObject("ARQ_REQUESTYPE") 
					+ "', NPH_LASTCHANGEDBY='" + columnNameValue.getObject("NPH_LASTCHANGEDBY") 
					+ "', NPH_LASTCHNAGEDATE=SYSDATE  " 
					+ " WHERE NPH_CODE =" + Session["NPH_CODE"]+"  AND NPH_LIFE = '"+ Session["NPH_LIFE"]+"' AND NAD_ADDRESSTYP = '"+columnNameValue.getObject("NAD_ADDRESSTYP")+"'";
				DB.executeDML(qry);
				//ViewState["AddCat"]="";
			}
			catch(Exception EX)
			{
				throw new Exception(EX.Message);
			
			}
		
		
		}
//Retreive Old Record
		public void GetAddressByCategory()
		{
			try
			{
				string AddCat=ViewState["AddCat"].ToString();
				string proposalno=Session["NP1_PROPOSAL"].ToString();
				if(proposalno!="" && (SessionObject.Get("NPH_CODE")==null||SessionObject.Get("NPH_CODE").ToString()=="0"))
				{
					rowset rsNPHCode = DB.executeQuery("select NPH_CODE from LNU1_UNDERWRITI where NU1_LIFE='F' AND NP1_PROPOSAL='"+ proposalno +"'");
					if(rsNPHCode.next())
					{
						SessionObject.Set("NPH_CODE",rsNPHCode.getObject("NPH_CODE"));
					}
				}
				
				//rowset rsLNPH_PHOLDERDB = DB.executeQuery("select * from LNAD_ADDRESS where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and  NAD_ADDRESSTYP ='"+AddCat.ToString()+"'");
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select * from LNAD_ADDRESS where NPH_CODE='"+SessionObject.Get("NPH_CODE")+"' AND NPH_LIFE='"+ SessionObject.Get("NPH_LIFE") +"' and  NAD_ADDRESSTYP ='"+AddCat.ToString()+"'");
				
				if (rsLNPH_PHOLDERDB.next())
				{
					this.countryCode=rsLNPH_PHOLDERDB.getString("CCN_CTRYCD");
					this.provinceCode=rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
					this.cityCode=rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
					ddlCCN_COUNTRY.SelectedValue=countryCode;
					
					
					txtNP1_ADDRESS1.Text=rsLNPH_PHOLDERDB.getString("NAD_ADDRESS1");
					txtNP1_ADDRESS2.Text=rsLNPH_PHOLDERDB.getString("NAD_ADDRESS2");
					txtNP1_ADDRESS3.Text=rsLNPH_PHOLDERDB.getString("NAD_ADDRESS3");
					txtNP1_TELRES.Text=rsLNPH_PHOLDERDB.getString("NAD_TELNO1");
					txtNP1_OFFICE.Text=rsLNPH_PHOLDERDB.getString("NAD_TELNO2");
					txtNP1_FAXNO.Text=rsLNPH_PHOLDERDB.getString("NAD_FAXNO");
					txtNP1_MOBILENO.Text=rsLNPH_PHOLDERDB.getString("NAD_MOBILE");	
					txtNP1_PAGER.Text=rsLNPH_PHOLDERDB.getString("NAD_PAGER");
					txtNP1_EMAIL1.Text=rsLNPH_PHOLDERDB.getString("NAD_EMAIL1");
					txtNP1_EMAIL2.Text=rsLNPH_PHOLDERDB.getString("NAD_EMAIL2");
					txtNP1_POBOX.Text=rsLNPH_PHOLDERDB.getString("NAD_POBOX");	
					GetProvinceById(ddlCCN_COUNTRY.SelectedValue);
					GetCityById(ddlCCN_COUNTRY.SelectedValue,rsLNPH_PHOLDERDB.getString("CPR_PROVCD"));	
					ddlCCN_PROVINCE.SelectedValue=rsLNPH_PHOLDERDB.getString("CPR_PROVCD");
					ddlCCN_CITY.SelectedValue=rsLNPH_PHOLDERDB.getString("CCT_CITYCD");
					//GetCityById();
					//GetProvinceById();	                
				}
				else
				{
					txtNP1_ADDRESS1.Text="";
					txtNP1_ADDRESS2.Text="";
					txtNP1_ADDRESS3.Text="";
					txtNP1_TELRES.Text="";
					txtNP1_OFFICE.Text="";
					txtNP1_FAXNO.Text="";
					txtNP1_MOBILENO.Text="";	
					txtNP1_PAGER.Text="";
					txtNP1_EMAIL1.Text="";
					txtNP1_EMAIL2.Text="";
					txtNP1_POBOX.Text="";	
					ddlCCN_PROVINCE.SelectedValue="";
					ddlCCN_CITY.SelectedValue="";
	                ddlCCN_COUNTRY.SelectedValue="";

				}
				//Filter Combo 
				//GetProvinceById();
				//GetCityById();
				//Slect the Saved values
				ddlCCN_COUNTRY.SelectedValue=countryCode;
				ddlCCN_PROVINCE.SelectedValue=provinceCode;
				ddlCCN_CITY.SelectedValue=cityCode;
			}
			catch(Exception EX)
			{
				string str=EX.Message;
			}
		}
//Check Whether To Update Or Insert.
	   
		public void GetCityById(string countryCode,string provinceCode)
		{
			try
			{	
				IDataReader LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.getcity(countryCode,provinceCode);
				ddlCCN_CITY.DataSource = LCSD_SYSTEMDTLReader0 ;
				ddlCCN_CITY.DataBind();
				LCSD_SYSTEMDTLReader0.Close();
			}
			catch(Exception EX)
			{
				string  STR=EX.Message;
			}
		}
		public void GetProvinceById(string countryCode)
		{
			try
			{				
				IDataReader LCSD_SYSTEMDTLReader0 = LCCN_COUNTRYDB.GetProvince(countryCode);
				ddlCCN_PROVINCE.DataSource = LCSD_SYSTEMDTLReader0 ;
				ddlCCN_PROVINCE.DataBind();
				LCSD_SYSTEMDTLReader0.Close();
			}
			catch(Exception EX)
			{
				string  STR=EX.Message;
			}
		
		
		}
		public void CheckInsertOrUpdate()
		{
		
			try
			{
				string proposalno=Session["NP1_PROPOSAL"].ToString();
				string AddCat=ViewState["AddCat"].ToString();
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select 1 from LNAD_ADDRESS where NPH_CODE='"+SessionObject.Get("NPH_CODE")+"' AND NPH_LIFE='"+ SessionObject.Get("NPH_LIFE") +"' and  NAD_ADDRESSTYP ='"+AddCat.ToString()+"'");
				
					if (rsLNPH_PHOLDERDB.next())
					{
						UpdateRecord();
					}
					else
					{
						InsertAll();
				   
					}
			}
			catch (Exception EX)
			{

				//throw new Exception(EX.Message);

				//Response.Write("<script type='text/javascript'>");
				//Response.Write("alert('Please provide the required information');");

				//Response.Write("alert('Please select any above link to define address category');");

				//Response.Write("</script>");

			}
				
		}
		bool saveUpdateClick=false;
		protected void btn_save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				CheckInsertOrUpdate();	
				saveUpdateClick=true;
				//************* Activity Log *************//			
				Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.ADDRESS_UPDATED);
		    }
			catch
			{
			
			}
			

		}
		protected void btn_copycor_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//CheckInsertOrUpdate();
				//saveUpdateClick = true;
				////************* Activity Log *************//			
				//Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.ADDRESS_UPDATED);
			}
			catch
			{

			}


		}

		protected void LinkButton1_Click(object sender, System.EventArgs e)
		{
			try
			{
				LinkButton1.CssClass="LinkButtonSelected";
				LinkButton2.CssClass="LinkButtonNormal";
				LinkButton3.CssClass="LinkButtonNormal";
				btn_copy.Visible = true;
				ViewState["AddCat"]="P";
				GetAddressByCategory();
			}
			catch
			{
			}
		}

		protected void LinkButton2_Click(object sender, System.EventArgs e)
		{
			LinkButton2.CssClass="LinkButtonSelected";
			LinkButton1.CssClass="LinkButtonNormal";
			LinkButton3.CssClass="LinkButtonNormal";
			btn_copy.Visible = true;
			ViewState["AddCat"]="B";
			GetAddressByCategory();
		}

		protected void LinkButton3_Click(object sender, System.EventArgs e)
		{
			LinkButton3.CssClass="LinkButtonSelected";
			LinkButton1.CssClass="LinkButtonNormal";
			LinkButton2.CssClass="LinkButtonNormal";
			btn_copy.Visible = false;
			ViewState["AddCat"]="C";
			GetAddressByCategory();		
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render (writer);
			if(saveUpdateClick)
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.parent.setPageNavigate();");
				Response.Write("</script>");
			}

		}

        protected void btn_copy_Click(object sender, EventArgs e)
        {
            try
				{
				string category = ViewState["AddCat"].ToString();
				string proposalno = Session["NP1_PROPOSAL"].ToString();
				rowset rs= DB.executeQuery("SELECT COUNT(*) address fROM LNAD_ADDRESS WHERE NAD_ADDRESSTYP='"+ category + "' and np1_proposal='"+ proposalno + "'");
                if (rs.next())
                {
					if (Convert.ToInt32(rs.getObject("address")) == 0)
					{
						string sqlString = "insert into LNAD_ADDRESS\n" +
						"  SELECT NPH_CODE,\n" +
						"         NP1_PROPOSAL,\n" +
						"         NPH_LIFE,\n" +
						"         '" + category + "',\n" +
						"         PFS_ACNTYEAR,\n" +
						"         OAC_ACTIVICODE,\n" +
						"         ARQ_REQUESTNO,\n" +
						"         USE_USERID,\n" +
						"         USE_TIMEDATE,\n" +
						"         NAD_POBOX,\n" +
						"         NAD_ADDRESS1,\n" +
						"         CCN_CTRYCD,\n" +
						"         NAD_ADDRESS2,\n" +
						"         CPR_PROVCD,\n" +
						"         CCT_CITYCD,\n" +
						"         NAD_ADDRESS3,\n" +
						"         NAD_TELNO1,\n" +
						"         NAD_TELNO2,\n" +
						"         NAD_FAXNO,\n" +
						"         NAD_MOBILE,\n" +
						"         NAD_PAGER,\n" +
						"         NAD_EMAIL1,\n" +
						"         NAD_EMAIL2,\n" +
						"         ARQ_REQUESTYPE,\n" +
						"         CONVERT,\n" +
						"         NPH_LASTCHANGEDBY,\n" +
						"         NPH_LASTCHNAGEDATE\n" +
						"    fROM LNAD_ADDRESS\n" +
						"   WHERE NP1_PROPOSAL = '" + proposalno + "'\n" +
						"     and nad_addresstyp = 'C'";
						DB.executeDML(sqlString);
						GetAddressByCategory();
						saveUpdateClick = true;
						Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.ADDRESS_UPDATED);
					}
					else
					{
						DB.executeDML("Delete from LNAD_ADDRESS where NAD_ADDRESSTYP='" + category + "' and np1_proposal='" + proposalno + "'");
						string sqlString = "insert into LNAD_ADDRESS\n" +
						"  SELECT NPH_CODE,\n" +
						"         NP1_PROPOSAL,\n" +
						"         NPH_LIFE,\n" +
						"         '" + category + "',\n" +
						"         PFS_ACNTYEAR,\n" +
						"         OAC_ACTIVICODE,\n" +
						"         ARQ_REQUESTNO,\n" +
						"         USE_USERID,\n" +
						"         USE_TIMEDATE,\n" +
						"         NAD_POBOX,\n" +
						"         NAD_ADDRESS1,\n" +
						"         CCN_CTRYCD,\n" +
						"         NAD_ADDRESS2,\n" +
						"         CPR_PROVCD,\n" +
						"         CCT_CITYCD,\n" +
						"         NAD_ADDRESS3,\n" +
						"         NAD_TELNO1,\n" +
						"         NAD_TELNO2,\n" +
						"         NAD_FAXNO,\n" +
						"         NAD_MOBILE,\n" +
						"         NAD_PAGER,\n" +
						"         NAD_EMAIL1,\n" +
						"         NAD_EMAIL2,\n" +
						"         ARQ_REQUESTYPE,\n" +
						"         CONVERT,\n" +
						"         NPH_LASTCHANGEDBY,\n" +
						"         NPH_LASTCHNAGEDATE\n" +
						"    fROM LNAD_ADDRESS\n" +
						"   WHERE NP1_PROPOSAL = '" + proposalno + "'\n" +
						"     and nad_addresstyp = 'C'";
						DB.executeDML(sqlString);
						GetAddressByCategory();
						saveUpdateClick = true;
						Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.ADDRESS_UPDATED);
					}
				}
            }
            catch (Exception)
            {
            }
        }
    }
}
