using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;
using shsm;

using System.Data.OleDb;
using System.Data.OracleClient;


namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDET : SHMA.Enterprise.Presentation.TwoStepController
	{
        // Custom validation for general and IBAN a/c no
        protected void cvAccountNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
         
            // validate fill only one field general account or IBAN
            bool accFilled = !string.IsNullOrWhiteSpace(txtNU1_ACCOUNTNO.Text);
            bool ibanFilled = !string.IsNullOrWhiteSpace(txtNU1_ACCIBAN.Text);

            args.IsValid = accFilled ^ ibanFilled;

           

        }

        //controls


        //		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
        //		protected System.Web.UI.WebControls.DropDownList pagerList;
        protected System.Web.UI.WebControls.Literal _lastEvent;
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

		protected System.Web.UI.WebControls.Literal ltrlApp;
		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal ErrorOccured;
		//protected System.Web.UI.WebControls.Literal IDError;
		

		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";

		#region //******************* Entity Fields Decleration *****************//
		

		

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPH_FULLNAMEARABIC;
		//protected Button  btnSearchOccupation;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_ACCOUNTNO;


		bool NewRecord = false;
		bool DMLSucceeded = true;				

		#endregion
		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NPH_CODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_FULLNAME;
		protected System.Web.UI.WebControls.CompareValidator mfvNPH_BIRTHDATE;
		//protected System.Web.UI.WebControls.CompareValidator cfvNPH_BIRTHDATE;

		protected System.Web.UI.WebControls.RequiredFieldValidator msgNU1_ACCOUNTNO;
		private string NPH_LIFE;
		private string NU1_SMOKER;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator2;
		//protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_SELECTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_cnic;
		

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_SMOKER;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNU1_ACCOUNTNO;
		//protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator4;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator5;
		//	protected System.Web.UI.WebControls.CompareValidator Comparevalidator4;
		private string NU1_ACCOUNTNO;
        private string NU1_IBAN;


        #endregion



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
            

        }
		
		private void InitializeComponent() 
		{

		}
		#endregion		

		#region Major methods of First Step
		protected override void PrepareInputUI(DataHolder dataHolder)
		{
			SetApplicationUI();
			//Set Default Branch of the User

			if (SessionObject.GetString("s_cch_code") == "2" && SessionObject.GetString("s_ccd_code") == "9")
			{
                
                ddlBranch.Enabled=true;
                
			}
			else
			{
				ddlBranch.Enabled=false;
			}

            if(Session["BankCode"].ToString()=="F" || Session["BankCode"].ToString() =="9")
            { lblRefStaffHeading.Visible = true;
                ddl_refStaff.Visible = true;
                
            }
            else
            { lblRefStaffHeading.Visible = false;
                ddl_refStaff.Visible = false;
            }
		}

		private void SetApplicationUI()
		{
			ltrlApp.Text = "var application='" + ace.Ace_General.getApplicationName() + "';";
			if(ace.Ace_General.IsIllustration())
			{
				HtmlTableRow rowAccountBMI = new HtmlTableRow();
				rowAccountBMI = (HtmlTableRow) Page.FindControl("rowBMIAccount");
				rowAccountBMI.Style["display"] = "none";
				rowAccountBMI = (HtmlTableRow) Page.FindControl("rowHeightWeight");
				rowAccountBMI.Style["display"] = "none";
				TextBox txtWeight = new TextBox();
				txtWeight	=(TextBox) Page.FindControl("txtNU1_ACTUALWEIGHT");
				txtWeight.Text  ="55";
				txtWeight	=(TextBox) Page.FindControl("txtNU1_ACTUALHEIGHT");
				txtWeight.Text ="5.5";
			}
			if (SessionObject.Get("s_CCD_CODE").ToString() == "9" || SessionObject.Get("s_CCD_CODE").ToString() =="F")
			{
				forBOP.Style.Add("visibility", "visible");
				forBOP1.Style.Add("visibility", "visible");     /*--chg-25082023--*/
			}
			else
			{
				forBOP.Style.Add("visibility", "hidden");
				forBOP1.Style.Add("visibility", "hidden");  /*--chg-25082023--*/
			}
		}

		protected override void ValidateParams() 
		{
			base.ValidateParams();			
			string[] param;
			foreach (string key in Request.Params.AllKeys)
			{
				if (key!=null && key.StartsWith("r_"))
				{
					param = Request[key].Split(',');
					SessionObject.Set(key.Replace("r_",""), param[param.Length-1]);
				}
			}
		}

		sealed protected override DataHolder GetInputData(DataHolder dataHolder)
		{
			GetSessionValues();
			//CheckKeyLevel();
			//recordCount = LNPH_PHOLDERDB.RecordCount;
			return dataHolder;
		}
		//Bind data 
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			//Comparevalidator4.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");

			//TODO: SESSION SETTING in behavior ViewInitialState()
			//***********************CUSTOM CODE ***********************/
			ViewInitialState();
			//***********************CUSTOM CODE ***********************/

			//Fill DropDown List of Branches
			CCS_CHANLSUBDETLDB branchNamesDB = new CCS_CHANLSUBDETLDB(this.dataHolder);
			IDataReader BranchReader = branchNamesDB.GetBranchNames();
			ddlBranch.DataSource = BranchReader;
			ddlBranch.DataBind();
			BranchReader.Close();

			string DefaultBranchCode = CCS_CHANLSUBDETLDB.GetDefaultBranchName();
			if (DefaultBranchCode != string.Empty)//Solved object reference not set to an instance of an object
			{
				//ddlBranch.Items.FindByValue(DefaultBranchCode).Selected = true;

				if (ddlBranch.Items.Contains(ddlBranch.Items.FindByValue(DefaultBranchCode)) == true)
				{
					ddlBranch.SelectedValue = DefaultBranchCode;
				}
				else
				{
					ddlBranch.SelectedIndex = -1;
				}

			}
			else
			{
				ddlBranch.SelectedIndex = -1;
			}


			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_TITLE_RO();;
			ddlNPH_TITLE.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPH_TITLE.DataBind();
			LCSD_SYSTEMDTLReader0.Close();

			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_IDTYPE_RO();;
			ddlNPH_IDTYPE.DataSource = LCSD_SYSTEMDTLReader1;
			ddlNPH_IDTYPE.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			
			//************ Parameterization *********************
            //if(isOccupationEnabled()) Stoped by Deen Muhammad 25062021
            //{
            //    
                IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO();;
				ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
				ddlCOP_OCCUPATICD.DataBind();
				LCOP_OCCUPATIONReader1.Close();

				IDataReader LCCL_CATEGORYReader2 = LCCL_CATEGORYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCL_CATEGORYCD_RO();
				ddlCCL_CATEGORYCD.DataSource = LCCL_CATEGORYReader2 ;
				ddlCCL_CATEGORYCD.DataBind();
				LCCL_CATEGORYReader2.Close();
			//}
			/*
						IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO();;
						ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
						ddlCOP_OCCUPATICD.DataBind();
						LCOP_OCCUPATIONReader1.Close();
			*/
			/*TODO: Create copy of above table*/
			/*
						IDataReader LCOP_OCCUPATIONReader1_1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO();;
						dgOccupation.DataSource=LCOP_OCCUPATIONReader1_1;
						dgOccupation.DataBind();
						LCOP_OCCUPATIONReader1_1.Close();

						IDataReader LCCL_CATEGORYReader2 = LCCL_CATEGORYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CCL_CATEGORYCD_RO();;
						ddlCCL_CATEGORYCD.DataSource = LCCL_CATEGORYReader2 ;
						ddlCCL_CATEGORYCD.DataBind();
						LCCL_CATEGORYReader2.Close();
			*/
			IDataReader LCNT_NATIONALITYReader3 = LCNT_NATIONALITYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_CNT_NATCD_RO();;
			ddlCNT_NATCD.DataSource = LCNT_NATIONALITYReader3;
			ddlCNT_NATCD.DataBind();
			LCNT_NATIONALITYReader3.Close();

			IDataReader LCNT_NATIONALITYReader4 = LCNT_NATIONALITYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_REF_STAFF(); 
			ddl_refStaff.DataSource = LCNT_NATIONALITYReader4;
			ddl_refStaff.DataBind();
			LCNT_NATIONALITYReader4.Close();

            //--chg-25082023--
            if (Session["BankCode"].ToString() == "9") // For BOP
            {
                IDataReader LCNT_NATIONALITYReader5 = LCNT_NATIONALITYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_REF_STAFF();
                ddl_refStaff2.DataSource = LCNT_NATIONALITYReader5;
                ddl_refStaff2.DataBind();
                LCNT_NATIONALITYReader5.Close();
            }
            else

            {
                // For JS Bank BM_FM Staff
                IDataReader LCNT_BMFM_JS_STAFFReader = LCNT_NATIONALITYDB.GetBMFM_for_JS_STAFF();
                ddl_refStaff2.DataSource = LCNT_BMFM_JS_STAFFReader;
                ddl_refStaff2.DataBind();
                LCNT_BMFM_JS_STAFFReader.Close();



            }
//--chg-end

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();//loadInnerStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("") ;
			ddlCOP_OCCUPATICD.Attributes.Add("onchange","filterClass(this);");
			
			txtNPH_FULLNAME.Attributes.Add("onfocus","Name_GotFocus(this);");
			txtNPH_FULLNAME.Attributes.Add("onblur", "Name_LostFocus(this);");

			//RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			//***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));
		}


		protected bool isOccupationEnabled()
		{
			rowset rs = DB.executeQuery(SHMA.Enterprise.Shared.EnvHelper.Parse("SELECT * FROM LCUI_CLIENTUI UI WHERE UI.CUI_COLUMNID='ddlCOP_OCCUPATICD'"));
			if(rs.next())
			{
				if(rs.getObject("CUI_VISIBILE") != null && rs.getString("CUI_VISIBILE").ToUpper() == "Y")
					if(rs.getObject("CUI_DISABLE") != null && rs.getString("CUI_DISABLE").ToUpper() == "N")
					{
						return true;
					}
			}
			return false;
		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPH_PHOLDER.PrimaryKeys)
			{
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!= null)
				{
					if (ctrl is WebControl)
					{
						//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!= null))
						{
							control.Enabled = true;
						}
					}
				}
			}
		}
        
                   
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
            //save Branch Code from ddlBranch.selectedValue into table lnu1_underwriti
            //ddlBranch.SelectedValue;
            //update lnu1_underwriti set pbb_branchcode = '000' where nu1_accountno = '200729996'//NP1_PROPOSAL

            // For JS bank change || SessionObject.GetString("s_ccd_code") == "F" 02Sep25
            Session["YearlyIncome"] = txtNPH_ANNUINCOME.Text.Trim().ToString();

           

            Session["BankCode"] = SessionObject.GetString("s_ccd_code").ToString();
            if (SessionObject.GetString("s_cch_code") == "2" && (SessionObject.GetString("s_ccd_code") == "9" || SessionObject.GetString("s_ccd_code") == "F"))
			{
                if (txt_accTitle.Text.Trim() == "")
                {
					PrintMessage("Please Enter Account Name");
					return;
                }
				//				if (ddl_refStaff.SelectedValue.Trim() == "0" || ddl_refStaff2.SelectedValue.Trim() == "0")  /*--chg-25082023--*/
				if (ddl_refStaff.SelectedValue.Trim() == "0")  /*--chg-25082023--*/
				{
					PrintMessage("Please Select Referee Staff");
					return;
				}
			}

			ErrorOccured.Text = "navigation = 'N';";

			if (_lastEvent.Text == "New") NewRecord = true;
			DMLSucceeded = false;
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PER_PERSONALDET();
			entityClass.setNameValueCollection(columnNameValue);

			//Custom : Change
			SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?"N":ddlNU1_SMOKER.SelectedValue);
			SessionObject.Set("NU1_ACCOUNTNO", txtNU1_ACCOUNTNO.Text.Trim()== "" ? null : txtNU1_ACCOUNTNO.Text);

            // Imran code
            SessionObject.Set("NU1_IBAN", txtNU1_ACCIBAN.Text.Trim() == "" ? null : txtNU1_ACCIBAN.Text);
            SessionObject.Set("NP1_ACCOUNTNAME", txt_accTitle.Text.Trim() == "" ? null : txt_accTitle.Text.Trim());
			SessionObject.Set("PBR_REFERENCE", ddl_refStaff.SelectedValue.Trim() == "" ? null : ddl_refStaff.SelectedValue.Trim());
			SessionObject.Set("PBR_REFERENCE2", ddl_refStaff2.SelectedValue.Trim() == "" ? null : ddl_refStaff2.SelectedValue.Trim());  /*--chg-25082023--*/

			SessionObject.Set("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
			SessionObject.Set("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
			
			SessionObject.Set("NP2_AGEPREM",txtNP2_AGEPREM.Text == "" ? null:txtNP2_AGEPREM.Text);

            SessionObject.Set("NPH_WEIGHT", txtNU1_CONVERTWEIGHT.Text == "" ? null : txtNU1_CONVERTWEIGHT.Text); 
			
			SessionObject.Set("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
			SessionObject.Set("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
            SessionObject.Set("NPH_HEIGHT", txtNU1_CONVERTHEIGHT.Text == "" ? null : txtNU1_CONVERTHEIGHT.Text);

            SessionObject.Set("NPH_FATHERNAME", txtNPH_FATHERNAME.Text == "" ? null : txtNPH_FATHERNAME.Text);
            SessionObject.Set("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text == "" ? null : txtNPH_MAIDENNAME.Text);
            SessionObject.Set("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text == "" ? null : txtNPH_DOCISSUEDAT.Text);
            SessionObject.Set("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text == "" ? null : txtNPH_DOCEXPIRDAT.Text); 
                        
			SessionObject.Set("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);
            SessionObject.Set("s_NPH_IDNO", txtCNIC_VALUE.Text==""?null:txtCNIC_VALUE.Text.Replace("-",""));
            SessionObject.Set("s_NPH_IDNO2", txtNPH_IDNO2.Text==""?null:txtNPH_IDNO2.Text);
            SessionObject.Set("NP1_JOINT", ddlNPH_INSUREDTYPE1.SelectedValue.Trim() == "N" ? "Y" : "N");
            //Custom : Change
            SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;

					//if(txtNPH_CODE.Text.Equals(""))
					//	txtNPH_CODE.Text = "0";

					txtNPH_LIFE.Text = "D";
					//TODO: NU1_LIFE = "S";
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
					
					//CUSTOM: ARABIC
					//columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_FULLNAMEARABIC",null);
					//CUSTOM: ARABIC
					
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));

					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));

					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE1.SelectedValue);
					//columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE1.SelectedValue);
					//columnNameValue.Add("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?null:ddlNU1_SMOKER.SelectedValue);
					//columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					//columnNameValue.Add("NP1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);
					columnNameValue.Add("NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
					columnNameValue.Add("NPH_IDNO2",txtNPH_IDNO2.Text.Trim()==""?null:txtNPH_IDNO2.Text);
				
					//Izhar-Ul-Haque
					columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
					columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text==""?null:txtNU1_CONVERTWEIGHT.Text);

					columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
					columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);

					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);
					//columnNameValue.Add("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);

					/*string code="";
					int clientExistInIlas  = ace.ILUS_ET_NM_PER_PERSONALDET.ClientExistInIlas(Convert.ToString(columnNameValue.get("NPH_IDNO")));
					int clientExistInBanca = ace.ILUS_ET_NM_PER_PERSONALDET.ClientExist(Convert.ToString(columnNameValue.get("NPH_IDNO")));
					
					if(clientExistInIlas!=0)
					{
						code=clientExistInIlas.ToString();
					}
					else if(clientExistInBanca!=0)
					{
						code=clientExistInBanca.ToString();
					}
					else
					{
						code=ace.ILUS_ET_NM_PER_PERSONALDET.getClientNumber();
					}*/
					if (txtNPH_CODE.Text.Equals("") || txtNPH_CODE.Text.Equals("0"))
					{
						txtNPH_CODE.Text = ace.ILUS_ET_NM_PER_PERSONALDET.getClientNumber();
					}
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()== "" ? null:txtNPH_CODE.Text);


					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "NPH_CODE");
					if (security.SaveAllowed)
					{
						//new LNPH_PHOLDER(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PER_PERSONALDET","NPH_CODE");
						/**************** Changed by Asif: Checking for Existing client ********************/
						/******************************  Checked by NIC ************************************/
						/*if(clientExistInIlas!=0 || clientExistInBanca!=0)
						{	//In case of Existing Person 
							txtNPH_CODE.Text =code;
							entityClass.fsoperationBeforeUpdate();
							columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
							dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
							new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);
						}
						else
						{	//In case of New Person
							entityClass.fsoperationBeforeSave();
							columnNameValue.Add("NPH_CODE",code==""?null:code);
							new LNPH_PHOLDER(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PER_PERSONALDET");
						}*/
						bool newClientForBANCA = isNewClientForBANCA(txtNPH_CODE.Text);
						if(newClientForBANCA)
						{	//Insert
							entityClass.fsoperationBeforeSave();
							new LNPH_PHOLDER(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PER_PERSONALDET");
						}
						else
						{	//Update
							entityClass.fsoperationBeforeUpdate();
							dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
							new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);
						}

						dataHolder.Update(DB.Transaction);
						
						SessionObject.Set("_pk_NPH_CODE",columnNameValue.get("NPH_CODE"));
						
						////SessionObject.Set("_pk_NPH_CODE",code);
						
						//CUSTOM: ARABIC
						DB.executeDML("UPDATE LNPH_PHOLDER SET NPH_FULLNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtNPH_FULLNAMEARABIC.Text)) + "' WHERE NPH_CODE="+columnNameValue["NPH_CODE"]);
						//CUSTOM: ARABIC
						//DB.executeDML("UPDATE LNPH_PHOLDER SET NPH_CODE='" + code + "' WHERE NPH_CODE='"+columnNameValue.get("NPH_CODE")+"'");
						
						//New Code By Izhar-ul-haque
						if(ddlNPH_INSUREDTYPE1.SelectedValue=="Y")
						{
							//DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='"+txtNU1_ACCOUNTNO.Text+ "',NP1_ACCOUNTNAME='"+ SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='"+ SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL")+"'");   /*--chg-25082023--*/
                            DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='" + txtNU1_ACCOUNTNO.Text + "',NP1_IBAN = '" + txtNU1_ACCIBAN.Text + "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");   /*--chg-30092025 Imran--*/
                           
                        }
						else
						{
							//DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='"+txtNU1_ACCOUNTNO.Text+ "',NP1_ACCOUNTNAME='"+ SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='"+ SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL")+"'");   /*--chg-25082023--*/
                            DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='" + txtNU1_ACCOUNTNO.Text + "',NP1_IBAN = '" + txtNU1_ACCIBAN.Text + "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");   /*--chg-30092025 Imran--*/

                        }

                        /**************** Changed by Asif: Checking for Existing client ********************/
                        /*if(clientExistInIlas!=0 || clientExistInBanca!=0)
						{
							entityClass.fsoperationAfterUpdate();
						}
						else
						{
							entityClass.fsoperationAfterSave();
						}*/

                        if (newClientForBANCA == true)
						{
							entityClass.fsoperationAfterSave();
						}
						else
						{
							entityClass.fsoperationAfterUpdate();
						}
						//RefreshDataFields();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPH_PHOLDER");
						_lastEvent.Text = "Save"; 					
						//PrintMessage("Record has been saved");
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
				case (EnumControlArgs.Update):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
					
					//CUSTOM: ARABIC
					//columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_FULLNAMEARABIC",null);
					//CUSTOM: ARABIC
					
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));

					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE1.SelectedValue);
					//columnNameValue.Add("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?null:ddlNU1_SMOKER.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					//columnNameValue.Add("NP1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);
					columnNameValue.Add("NPH_IDNO", txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
					columnNameValue.Add("NPH_IDNO2",txtNPH_IDNO2.Text.Trim()==""?null :txtNPH_IDNO2.Text);
				
					//Izhar-Ul-Haque
					columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);
					columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text==""?null:txtNU1_CONVERTWEIGHT.Text);
				
					columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
					columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);
					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);
					//columnNameValue.Add("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);
					
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.UpdateAllowed)
					{
						entityClass.fsoperationBeforeUpdate();

						new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);

						SessionObject.Set("_pk_NPH_CODE",columnNameValue.get("NPH_CODE"));

						//CUSTOM: ARABIC
						DB.executeDML("UPDATE LNPH_PHOLDER SET NPH_FULLNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtNPH_FULLNAMEARABIC.Text)) + "' WHERE NPH_CODE="+columnNameValue["NPH_CODE"]);
						//CUSTOM: ARABIC
						
						entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPH_PHOLDER");
						//recordSelected = true;
						//PrintMessage("Record has been updated");

						//New Code By Izhar-ul-haque

						if(ddlNPH_INSUREDTYPE1.SelectedValue=="Y")
						{
							// DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='"+txtNU1_ACCOUNTNO.Text+ "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL")+"'"); /*--chg-25082023--*/
                            DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='N',np1_accountno='" + txtNU1_ACCOUNTNO.Text + "',NP1_IBAN = '" + txtNU1_ACCIBAN.Text + "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");   /*--chg-30092025 Imran--*/

                        }
                        else
						{
							//DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='"+txtNU1_ACCOUNTNO.Text+ "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL")+"'"); /*--chg-25082023--*/
                            DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_JOINT='Y',np1_accountno='" + txtNU1_ACCOUNTNO.Text + "',NP1_IBAN = '" + txtNU1_ACCIBAN.Text + "',NP1_ACCOUNTNAME='" + SessionObject.Get("NP1_ACCOUNTNAME") + "',PBR_REFERENCE='" + SessionObject.Get("PBR_REFERENCE") + "',np1_purpose='" + SessionObject.Get("PBR_REFERENCE2") + "' WHERE NP1_PROPOSAL='" + SessionObject.Get("NP1_PROPOSAL") + "'");   /*--chg-30092025 Imran--*/

                        }
                    }
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);

					columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE1.SelectedValue);
					//columnNameValue.Add("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?null:ddlNU1_SMOKER.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					//columnNameValueNonBase.Add("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.DeleteAllowed)
					{
						entityClass.fsoperationBeforeDelete();

						new LNPH_PHOLDER(dataHolder).Delete(columnNameValue);

						dataHolder.Update(DB.Transaction);
						entityClass.fsoperationAfterDelete();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "LNPH_PHOLDER");
						//PrintMessage("Record has been deleted");				
					}
					else
					{
						PrintMessage("You are not autherized to Delete.");
					}
					DMLSucceeded = true;

					break;
				case (EnumControlArgs.Process):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue.Trim()==""?null:ddlNPH_MARITALSTATUS.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);

					columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue.Trim()==""?null:ddlNPH_INSUREDTYPE1.SelectedValue);
					//columnNameValue.Add("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?null:ddlNU1_SMOKER.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text.Trim()==""?null:(object)double.Parse(txtNPH_ANNUINCOME.Text));
					columnNameValue.Add("CNT_NATCD",ddlCNT_NATCD.SelectedValue.Trim()==""?null:ddlCNT_NATCD.SelectedValue);
					//columnNameValueNonBase.Add("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);
					
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					string result="";
					if (_CustomArgName.Value == "ProcessName")
					{
						string processName = _CustomArgVal.Value;
						if (security.ProcessAllowed(processName))
						{
							Type type = Type.GetType(processName);											
							if (type != null)
							{
								shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
								NameValueCollection[] dataRows = new NameValueCollection[1];
								bool[] SelectedRowIndexes = new bool[1];
								dataRows[0] = columnNameValue;
								SelectedRowIndexes[0] = true;
								proccessCommand.setAllFields(columnNameValue);
								proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
								proccessCommand.setPrimaryKeys(LNPH_PHOLDER.PrimaryKeys);
								proccessCommand.setTableName("LNPH_PHOLDER");
								proccessCommand.setDataRows(dataRows);
								proccessCommand.setSelectedRows(SelectedRowIndexes);
								result = proccessCommand.processing();
								//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
							}
						}
						else
						{
							result = "You are not Authorized to Execute Process.";
						}
					}	
					//recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
			}
			string bankCode = "";
			string currentdBank = SessionObject.Get("s_CCH_CODE").ToString() + SessionObject.Get("s_CCD_CODE").ToString();
			rowset rs = DB.executeQuery("SELECT CSD.Csd_Value FROM LCSD_SYSTEMDTL CSD WHERE CSD.CSH_ID = 'CHBNK' AND CSD.CSD_TYPE = '"+ currentdBank +"'");
			if(rs.next())
				bankCode= rs.getString(1);
            //string qry = string.Format("update lnu1_underwriti set pbb_branchcode = '{0}', pbK_bankcode = '{3}' where nu1_accountno = '{1}' and np1_proposal ='{2}' and nu1_life='F'" ,
            //	ddlBranch.SelectedValue,txtNU1_ACCOUNTNO.Text, SessionObject.Get("NP1_PROPOSAL").ToString(),bankCode);

            string accountNo = string.IsNullOrEmpty(txtNU1_ACCOUNTNO.Text)
                   ? txtNU1_ACCIBAN.Text
                   : txtNU1_ACCOUNTNO.Text;

            string qry = string.Format(@"
    UPDATE lnu1_underwriti 
    SET pbb_branchcode = '{0}', pbk_bankcode = '{3}' 
    WHERE nu1_accountno = '{1}' 
      AND np1_proposal = '{2}' 
      AND nu1_life = 'F'",
                ddlBranch.SelectedValue,
                accountNo,
                SessionObject.Get("NP1_PROPOSAL").ToString(),
                bankCode
            );

            DB.executeDML(qry);

			LNP1_POLICYMASTRDB.UpdateBranchDetails(SessionObject.Get("NP1_PROPOSAL").ToString(),ddlBranch.SelectedValue);
		}
	
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			LNPH_PHOLDERDB LNPH_PHOLDERDB_obj = new LNPH_PHOLDERDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
				
				//SELECT * FROM LNPH_PHOLDER WHERE NPH_CODE=? AND NPH_LIFE=? 

				/*rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='F'");
				if (rsLNPH_PHOLDERDB.next())
				{
					NPH_CODE = rsLNPH_PHOLDERDB.getString(1);
					NPH_LIFE = rsLNPH_PHOLDERDB.getString(2);
				}*/

				
				DataRow row = LNPH_PHOLDERDB_obj.FindByPK(NPH_CODE,NPH_LIFE)["LNPH_PHOLDER"].Rows[0];
				ShowData(row);
			}		
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					//	ShowData(dataHolder["LNPH_PHOLDER"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString())
			{
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else
				{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else
			{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else
				{
					_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();			
				}
			}
			//for header & footer script					
			//RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	
			//***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("");
			
		}
		#endregion	

		#region Events
		/*
		private void btnSearchOccupation_Click(object sender, EventArgs e)
		{

		}
		*/
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) 
		{
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value)
			{
				case "Update" :
					ControlArgs[0]=EnumControlArgs.Update;
					CustomDoControl();
					break;
				case "Save" :
					ControlArgs[0]=EnumControlArgs.Save;
					CustomDoControl();
					break;
				case "Delete" :
					ControlArgs[0]=EnumControlArgs.Delete;
					CustomDoControl();
					break;
				case "Filter" :
					ControlArgs[0] = EnumControlArgs.Filter;
					CustomDoControl();
					break;
				case "Process" :
					ControlArgs[0] = EnumControlArgs.Process;
					CustomDoControl();
					break;

			}
			_CustomEventVal.Value="";	
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
		
			//base.OnUnload(e);
			if (SetFieldsInSession())
			{
				if (NewRecord == true  && DMLSucceeded == false)//if (_lastEvent.Text == "New"  && DMLSucceeded == false)
				{
					SessionObject.Set("NPH_TITLE","");
					SessionObject.Set("NPH_IDTYPE","");
					SessionObject.Set("NP1_PROPOSAL","");
					SessionObject.Set("NPH_SEX","");
					SessionObject.Set("NPH_MARITALSTATUS","");
					SessionObject.Set("NPH_FULLNAME","");

					SessionObject.Set("NPH_FIRSTNAME","");
					SessionObject.Set("NPH_SECONDNAME","");
					SessionObject.Set("NPH_LASTNAME","");

					SessionObject.Set("NPH_FULLNAMEARABIC","");
					SessionObject.Set("NPH_BIRTHDATE","");
					SessionObject.Set("COP_OCCUPATICD","");
					SessionObject.Set("CCL_CATEGORYCD","");
					SessionObject.Set("NPH_INSUREDTYPE","");
					SessionObject.Set("NPH_CODE","");
					SessionObject.Set("NPH_LIFE","");
					SessionObject.Set("NU1_SMOKER","");
					SessionObject.Set("NU1_ACCOUNTNO","");
					SessionObject.Set("NPH_ANNUINCOME","");
				}
				else
				{
					SessionObject.Set("NPH_TITLE",ddlNPH_TITLE.SelectedValue);
					SessionObject.Set("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue);
					SessionObject.Set("NPH_SEX",ddlNPH_SEX.SelectedValue);
					SessionObject.Set("NPH_MARITALSTATUS",ddlNPH_MARITALSTATUS.SelectedValue);
					SessionObject.Set("NPH_FULLNAME",txtNPH_FULLNAME.Text);

					SessionObject.Set("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text);
					SessionObject.Set("NPH_SECONDNAME",txtNPH_SECONDNAME.Text);
					SessionObject.Set("NPH_LASTNAME",txtNPH_LASTNAME.Text);

					SessionObject.Set("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text);
					SessionObject.Set("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text);
					SessionObject.Set("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue);
					SessionObject.Set("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue);
					SessionObject.Set("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE1.SelectedValue);
					SessionObject.Set("NPH_CODE",txtNPH_CODE.Text);
					SessionObject.Set("NPH_LIFE",txtNPH_LIFE.Text);
					SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue);
					SessionObject.Set("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text);
                    SessionObject.Set("NU1_IBAN", txtNU1_ACCIBAN.Text);
                    SessionObject.Set("NPH_ANNUINCOME",txtNPH_ANNUINCOME.Text);
					SessionObject.Set("CNT_NATCD",ddlCNT_NATCD.SelectedValue);

                    Session["YearlyIncome"] = txtNPH_ANNUINCOME.Text.ToString();
                }
			}
		}										
	
		#endregion 

		protected override bool TransactionRequired 
		{
			get 
			{
				return true;
			}
		}


		private void GetSessionValues()
		{
			if (false)
			{	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		

		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			ddlNPH_TITLE.ClearSelection();
			ddlNPH_IDTYPE.ClearSelection();
			ddlNPH_SEX.ClearSelection();
			ddlNPH_MARITALSTATUS.ClearSelection();
			txtNPH_FULLNAME.Text="";

            txtNPH_DOCISSUEDAT.Text = "";
            txtNPH_DOCEXPIRDAT.Text = "";
            txtNPH_FATHERNAME.Text = "";
            txtNPH_MAIDENNAME.Text = "";
			txtNPH_FIRSTNAME.Text="";
			txtNPH_SECONDNAME.Text="";
			txtNPH_LASTNAME.Text="";

			txtNPH_FULLNAMEARABIC.Text="";
			txtNPH_BIRTHDATE.Text="";
			ddlCOP_OCCUPATICD.ClearSelection();
			ddlCCL_CATEGORYCD.ClearSelection();
			ddlNPH_INSUREDTYPE1.ClearSelection();
			txtNPH_CODE.Enabled = true;
			txtNPH_CODE.Text="";
			txtNPH_LIFE.Enabled = true;
			txtNPH_LIFE.Text="";
			ddlNU1_SMOKER.ClearSelection();
			txtNU1_ACCOUNTNO.Text="";
            txtNU1_ACCIBAN.Text = "";
            
            txtCNIC_VALUE.Text="";
			txtNPH_IDNO2.Text="";
			txtNPH_ANNUINCOME.Text="";
			ddlCNT_NATCD.ClearSelection();
		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();

			//			ddlBranch.ClearSelection();
			//			CCS_CHANLSUBDETLDB ddlBranchDB = new CCS_CHANLSUBDETLDB(this.dataHolder);
			//			string BranchCode = CCS_CHANLSUBDETLDB.GetBranchCode((string)Session["NP1_PROPOSAL"],(string)Session["NU1_ACCOUNTNO"]);
			//			
			//			if (BranchCode != string.Empty)//Solved object reference not set to an instance of an object
			//			{
			//				//ddlBranch.Items.FindByValue(DefaultBranchCode).Selected = true;
			//
			//				if (ddlBranch.Items.Contains(ddlBranch.Items.FindByValue(BranchCode)) == true)
			//				{
			//					ddlBranch.SelectedValue = BranchCode;
			//				}
			//				else
			//				{
			//					ddlBranch.SelectedIndex = -1;
			//				}
			//
			//			}
			//			else
			//			{
			//				ddlBranch.SelectedIndex = -1;
			//			}

			//ddlBranchDB.FindByPK(
			//select pbb_branchcode from lnu1_underwriti where np1_proposal ='2012100010' and nu1_accountno = '00980098' and  nu1_life='F'
			//objRow["np1_proposal"]
			//accountno from session
			//F is static
			

			ddlNPH_TITLE.ClearSelection();
			ListItem item0 = ddlNPH_TITLE.Items.FindByValue(objRow["NPH_TITLE"].ToString());
			if (item0!= null)
			{
				item0.Selected = true;
			}

			ddlNPH_IDTYPE.ClearSelection();
			ListItem item11 = ddlNPH_IDTYPE.Items.FindByValue(objRow["NPH_IDTYPE"].ToString());
			if (item11!= null)
			{
				item11.Selected = true;
			}
			
			ddlNPH_SEX.ClearSelection();
			ListItem item1 = ddlNPH_SEX.Items.FindByValue(objRow["NPH_SEX"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}

			
			ddlNPH_MARITALSTATUS.ClearSelection();
			ListItem item2=ddlNPH_MARITALSTATUS.Items.FindByValue(objRow["NPH_MARITALSTATUS"].ToString());
			if (item2!= null)
			{
				item2.Selected=true;
			}

			
			txtNPH_FULLNAME.Text=objRow["NPH_FULLNAME"].ToString();
			
			txtNPH_FIRSTNAME.Text=objRow["NPH_FIRSTNAME"].ToString();
			txtNPH_SECONDNAME.Text=objRow["NPH_SECONDNAME"].ToString();
			txtNPH_LASTNAME.Text=objRow["NPH_LASTNAME"].ToString();

			txtCNIC_VALUE.Text = objRow["NPH_IDNO"].ToString();
			txtNPH_IDNO2.Text  = objRow["NPH_IDNO2"].ToString();
			
			//Format for NIC
			string NIC = txtCNIC_VALUE.Text;
			string concat = null;
			if(ace.clsIlasUtility.isNoorID() == false)
			{
				if(NIC.Length==Convert.ToInt16(13))
				{
					for(int i=0;i<=4;i++)
					{
						concat +=NIC[i];
					}
					concat+="-";

					for(int i=5;i<=11;i++)
					{
						concat +=NIC[i];
					}
					concat+="-"+NIC[12];
				}
				else
				{
					concat=NIC.ToString();
				}

				txtCNIC_VALUE.Text=concat.ToString();
			}

			
            
			//New Values

			ddlNPH_HEIGHTTYPE.SelectedValue=objRow["NPH_HEIGHTUOM"].ToString();
			double actualHeight= Math.Round(Convert.ToDouble(objRow["NPH_HEIGHTACTUAL"]),2);
			double acutalWeight = Math.Round(Convert.ToDouble(objRow["NPH_WEIGHTACTUAL"]),2);
			txtNU1_ACTUALHEIGHT.Text=actualHeight.ToString();
			txtNU1_CONVERTHEIGHT.Text=objRow["NPH_HEIGHT"].ToString();

            txtNPH_DOCISSUEDAT.Text = objRow["NPH_DOCISSUEDAT"] == DBNull.Value ? "" : ((DateTime)objRow["NPH_DOCISSUEDAT"]).ToShortDateString();
            txtNPH_DOCEXPIRDAT.Text = objRow["NPH_DOCEXPIRDAT"] == DBNull.Value ? "" : ((DateTime)objRow["NPH_DOCEXPIRDAT"]).ToShortDateString();
            txtNPH_FATHERNAME.Text = objRow["NPH_FATHERNAME"].ToString();
            txtNPH_MAIDENNAME.Text = objRow["NPH_MAIDENNAME"].ToString();

			ddlNPH_WEIGHTTTYPE.SelectedValue=objRow["NPH_WEIGHTUOM"].ToString();
			//txtNU1_ACTUALWEIGHT.Text=objRow["NPH_WEIGHTACTUAL"].ToString();
			txtNU1_ACTUALWEIGHT.Text=acutalWeight.ToString();
			txtNP2_AGEPREM.Text= Convert.ToString(ace.Ace_General.getEntryAge(Convert.ToString(SessionObject.Get("NP1_PROPOSAL"))));
			txtNU1_CONVERTWEIGHT.Text=objRow["NPH_WEIGHT"].ToString();
			
			/***************************************************************
			 Controlling of Height in case of Feet having following Values 
			 e.g. 5` 10``= 5.10 (5 Feet 10 inches)
			 
			NOTE: In DB 5.1 and 5.10 are same so we have to distinguish them.
			*****************************************************************/
			if(ddlNPH_HEIGHTTYPE.SelectedValue.ToUpper() == "F" )
			{
				if(txtNU1_CONVERTHEIGHT.Text == "1.780" || txtNU1_CONVERTHEIGHT.Text == "1.78" )
				{
					txtNU1_ACTUALHEIGHT.Text="5.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "1.470" || txtNU1_CONVERTHEIGHT.Text == "1.47" )
				{
					txtNU1_ACTUALHEIGHT.Text="4.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "2.080" || txtNU1_CONVERTHEIGHT.Text == "2.08" )
				{
					txtNU1_ACTUALHEIGHT.Text="6.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "1.170" || txtNU1_CONVERTHEIGHT.Text == "1.17" )
				{
					txtNU1_ACTUALHEIGHT.Text="3.10";	
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "2.390" || txtNU1_CONVERTHEIGHT.Text == "2.39" )
				{
					txtNU1_ACTUALHEIGHT.Text="7.10";
				}
				else if(txtNU1_CONVERTHEIGHT.Text == "0.860" || txtNU1_CONVERTHEIGHT.Text == "0.86" )
				{
					txtNU1_ACTUALHEIGHT.Text="2.10";	
				}				
			}

            //CUSTOM: Arabic
            /*System.Data.IDbCommand  cmd = DB.Connection.CreateCommand();
			cmd.CommandText = "SELECT NPH_FULLNAMEARABIC FROM LNPH_PHOLDER WHERE NPH_CODE="+objRow["NPH_CODE"];
			System.Data.IDataReader rdr= cmd.ExecuteReader();
			string FULLNAMEARABIC=null;
			if(rdr.Read())
				FULLNAMEARABIC =  System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(rdr["NPH_FULLNAMEARABIC"].ToString()));
			//txtNPH_FULLNAMEARABIC.Text=objRow["NPH_FULLNAMEARABIC"].ToString();
			txtNPH_FULLNAMEARABIC.Text=Convert.ToString(FULLNAMEARABIC);
			*/

            //CUSTOM: Arabic
            try
            {
				rowset titleorStaff = DB.executeQuery("select t.np1_accountname,t.pbr_reference, t.np1_purpose From lnp1_policymastr t where np1_proposal='" + SessionObject.Get("np1_proposal")+"'");  /*--chg-25082023--*/
				if (titleorStaff.next())
				{
					txt_accTitle.Text = titleorStaff.getObject("np1_accountname") == null ? "" : titleorStaff.getObject("np1_accountname").ToString();
					ddl_refStaff.SelectedValue = titleorStaff.getObject("pbr_reference") == null ? "" : titleorStaff.getObject("pbr_reference").ToString();
					ddl_refStaff2.SelectedValue = titleorStaff.getObject("np1_purpose") == null ? "" : titleorStaff.getObject("np1_purpose").ToString();    /*--chg-25082023--*/

				}
			}
            catch (Exception)
            {

            }
			try
			{
				rowset rsArabic = DB.executeQuery("SELECT NPH_FULLNAMEARABIC,NPH_ANNUINCOME FROM LNPH_PHOLDER WHERE NPH_CODE="+objRow["NPH_CODE"]);
				string FULLNAMEARABIC=null;
				string strNPH_ANNUINCOME = "";
				if(rsArabic.next())
				{
					FULLNAMEARABIC =  System.Text.Encoding.UTF8.GetString(Convert.FromBase64String((rsArabic.getObject("NPH_FULLNAMEARABIC")==null?"":rsArabic.getString("NPH_FULLNAMEARABIC"))));
					strNPH_ANNUINCOME = rsArabic.getObject("NPH_ANNUINCOME")==null?"":rsArabic.getString("NPH_ANNUINCOME");
				}
				txtNPH_FULLNAMEARABIC.Text=Convert.ToString(FULLNAMEARABIC);
				txtNPH_ANNUINCOME.Text=Convert.ToString(strNPH_ANNUINCOME);
                Session["YearlyIncome"] = txtNPH_ANNUINCOME.Text.ToString();

            }
			catch(Exception e)
			{
				//goto: hell
			}
			//CUSTOM: ARABIC
			//txtNPH_FULLNAMEARABIC.Text=objRow["NPH_FULLNAMEARABIC"].ToString();
			
			txtNPH_BIRTHDATE.Text=objRow["NPH_BIRTHDATE"]== DBNull.Value?"":((DateTime)objRow["NPH_BIRTHDATE"]).ToShortDateString();


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


			ddlCOP_OCCUPATICD.ClearSelection();

			//TODO: Check From lcui-Tbl to do this or Not
			if(!isOccupationEnabled())//if Disabled then
			{
				IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.getOccupationById(objRow["COP_OCCUPATICD"].ToString());
				ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
				ddlCOP_OCCUPATICD.DataBind();
				LCOP_OCCUPATIONReader1.Close();
				ddlCOP_OCCUPATICD.Enabled=false;
			}

			ListItem item5=ddlCOP_OCCUPATICD.Items.FindByValue(objRow["COP_OCCUPATICD"].ToString());
			if (item5!= null)
			{
				item5.Selected=true;
			}ddlCCL_CATEGORYCD.ClearSelection();
			ListItem item6=ddlCCL_CATEGORYCD.Items.FindByValue(objRow["CCL_CATEGORYCD"].ToString());
			if (item6!= null)
			{
				item6.Selected=true;
			}ddlNPH_INSUREDTYPE1.ClearSelection();
			ListItem item7=ddlNPH_INSUREDTYPE1.Items.FindByValue(objRow["NPH_INSUREDTYPE"].ToString());
			if (item7!= null)
			{
				item7.Selected=true;
			}
            SessionObject.Set("NP1_JOINT", ddlNPH_INSUREDTYPE1.SelectedValue.Trim() == "N" ? "Y" : "N");
            txtNPH_CODE.Text=objRow["NPH_CODE"].ToString();
			txtNPH_CODE.Enabled=false;
			txtNPH_LIFE.Text=objRow["NPH_LIFE"].ToString();
			txtNPH_LIFE.Enabled=false;
			
			//Manual Code
			ddlNU1_SMOKER.ClearSelection();
			ListItem item8=ddlNU1_SMOKER.Items.FindByValue(Session["NU1_SMOKER"].ToString());
			if (item8!= null)
			{
				item8.Selected=true;
			}

			txtNU1_ACCOUNTNO.Text=Session["NU1_ACCOUNTNO"]==null ? "" :  Session["NU1_ACCOUNTNO"].ToString();
            txtNU1_ACCIBAN.Text= Session["NU1_IBAN"] == null ? "" : Session["NU1_IBAN"].ToString();


            ddlCNT_NATCD.ClearSelection();
			ListItem item10=ddlCNT_NATCD.Items.FindByValue(objRow["CNT_NATCD"].ToString());
			if (item10!= null)
			{
				item10.Selected=true;
			}
			//Manual Code


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
			foreach(string processName in AllProcess)
			{
				if (security.ProcessAllowed(processName))
				{
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed)
			{
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
		}


		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}

		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}

		bool SetFieldsInSession()
		{
			bool flag = false;
			if (_lastEvent.Text.Equals(EnumControlArgs.Edit.ToString()))
			{
				flag = true;
			}
			else 
			{				
				if (ControlArgs!=null)
				{
					if (ControlArgs[0]!=null)
					{
						EnumControlArgs arg = (EnumControlArgs)ControlArgs[0] ;
						if (arg.Equals(EnumControlArgs.Save) || arg.Equals(EnumControlArgs.Edit))
						{
							flag = true;
						}
					}					
				}
			}
			return flag;
		}

		private NameValueCollection getAllFields() 
		{
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) 
			{
				string strKey = key.ToString();
				allFields.add(strKey,columnNameValue.get(strKey));
				
			}

			foreach (Control c in this.myForm.Controls) 
			{	
				string _fieldName="";
				if (c is WebControl) 
				{
					switch (c.GetType().ToString()) 
					{
						case "System.Web.UI.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName))
							{ 
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) 
							{
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}

		bool IsRecordSelected()
		{
			bool selected = true;
			foreach (string pk in LNPH_PHOLDER.PrimaryKeys)
			{
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0)
				{
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord()
		{
			if (IsRecordSelected())
			{
				NPH_CODE=SessionObject.GetString("NPH_CODE");
				NPH_LIFE=SessionObject.GetString("NPH_LIFE");
	

				DataRow selectedRow = new LNPH_PHOLDERDB(dataHolder).FindByPK(NPH_CODE,NPH_LIFE)["LNPH_PHOLDER"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm()
		{
			NormalEntryTableDiv.Style.Add("visibility" , "hidden");
			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("");
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields
		{
			get
			{	
				return NormalEntryTableDiv.Controls;
			}
		}



		protected void CustomDoControl() 
		{
			base.DoControl();
			String lastEvent = _lastEvent.Text;
			

			if (!_lastEvent.Text.Equals("Delete"))
				_lastEvent.Text = "Edit";
			else
			{
				ClearSession();
				FirstStep();
			}		
		}


		private void ClearSession()
		{
			SessionObject.Remove("NPH_TITLE");
			SessionObject.Remove("NPH_IDTYPE");
			SessionObject.Remove("NPH_SEX");
			SessionObject.Remove("NPH_MARITALSTATUS");
			SessionObject.Remove("NPH_FULLNAME");
			
			SessionObject.Remove("NPH_FIRSTNAME");
			SessionObject.Remove("NPH_SECONDNAME");
			SessionObject.Remove("NPH_LASTNAME");

			SessionObject.Remove("NPH_FULLNAMEARABIC");
			SessionObject.Remove("NPH_BIRTHDATE");
			SessionObject.Remove("COP_OCCUPATICD");
			SessionObject.Remove("CCL_CATEGORYCD");
			SessionObject.Remove("NPH_INSUREDTYPE");
			SessionObject.Remove("NPH_CODE");
			SessionObject.Remove("NPH_LIFE");
			SessionObject.Remove("NU1_SMOKER");
			SessionObject.Remove("NU1_ACCOUNTNO");

			SessionObject.Remove("NPH_WEIGHTUOM");
			SessionObject.Remove("NPH_WEIGHTACTUAL");
			SessionObject.Remove("NPH_WEIGHT");
			
			SessionObject.Remove("NPH_HEIGHTUOM");
			SessionObject.Remove("NPH_HEIGHTACTUAL");
			SessionObject.Remove("NPH_HEIGHT");
			
			SessionObject.Remove("NU1_OVERUNDERWT");
			SessionObject.Remove("NPR_PREMIUMTER");
			SessionObject.Remove("NPH_ANNUINCOME");
			SessionObject.Remove("CNT_NATCD");

		}



		private void ViewInitialState()
		{
			//rowset rsPPR_PRODCD = DB.executeQuery("SELECT NPH_CODE, NPH_LIFE, NP1_PROPOSAL FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = '"+SessionObject.Get("NP1_PROPOSAL")+"' AND NPH_LIFE = 'D'");
			

			if ((""+Session["flg_SELECETD"]).Equals("Y"))
			{
				SessionObject.Set("NPH_CODE",Session["NPH_CODE"]);
				SessionObject.Set("NPH_LIFE",Session["NPH_LIFE"]);
				SessionObject.Set("NU1_SMOKER",(Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"]));
				SessionObject.Set("NU1_ACCOUNTNO",Session["NU1_ACCOUNTNO"]);
                SessionObject.Set("NU1_IBAN", Session["NU1_IBAN"]);

                NPH_CODE = ""+Session["NPH_CODE"];
				NPH_LIFE = ""+Session["NPH_LIFE"];
				NU1_SMOKER = ""+ (Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"].ToString());
				NU1_ACCOUNTNO = ""+Session["NU1_ACCOUNTNO"];
                NU1_IBAN= ""+Session["NU1_IBAN"];

            }
			else
			{
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE, NU1_SMOKER, NU1_ACCOUNTNO,NU1_IBAN from lnu1_underwriti where np1_proposal='" + SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='F'");
				if (rsLNPH_PHOLDERDB.next())
				{
					//NPH_CODE = rsLNPH_PHOLDERDB.getString(1);
					//NPH_LIFE = rsLNPH_PHOLDERDB.getString(2);
					SessionObject.Set("NPH_CODE",rsLNPH_PHOLDERDB.getString(1));
					SessionObject.Set("NPH_LIFE",rsLNPH_PHOLDERDB.getString(2));
					SessionObject.Set("NU1_SMOKER",rsLNPH_PHOLDERDB.getString(3));
					SessionObject.Set("NU1_ACCOUNTNO",rsLNPH_PHOLDERDB.getString(4));
                    SessionObject.Set("NU1_IBAN", rsLNPH_PHOLDERDB.getString(5));

                }
			}

			SessionObject.Remove("flg_SELECETD");

		}

		private bool isNewClientForBANCA(string nphCode)
		{
			bool foundInBanc = ace.ILUS_ET_NM_PER_PERSONALDET.searchClientByCode_BANCA(nphCode);
			if(foundInBanc == true)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		//		protected override void PrepareInputUI(DataHolder dataHolder) 
		//		{ 
		//			ErrorOccured.Text = "var navigation = 'Y';";
		//		}

	}
}

