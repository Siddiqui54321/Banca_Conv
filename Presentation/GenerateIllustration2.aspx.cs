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
	public partial class GenerateIllustration2 : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls


		protected System.Web.UI.WebControls.Literal _lastEvent;
	

		protected System.Web.UI.WebControls.Literal ltrlApp;
		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		protected System.Web.UI.WebControls.Literal ErrorOccured;
		

		/******************* Entity Fields Decleration *****************/
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_AGEPREM2ND;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPPR_PRODCD;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlPCU_CURRCODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_CURRCODE;

		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_SUMASSURED_2;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPCU_CURRCODE_PRM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCMO_MODE;

		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPR_INDEXATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_INDEXATION;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_BENEFITTERM;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPR_PREMIUMTER;

		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_PREMIUM;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNP2_SETNO;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox txtNPR_BASICFLAG;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_USERID;


		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";

		#region //******************* Entity Fields Decleration *****************//
		
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlBranch;

		

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOP_OCCUPATICD;
		
		bool NewRecord = false;
		bool DMLSucceeded = true;				

		#endregion
		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NPH_CODE;
		protected System.Web.UI.WebControls.CompareValidator cfvNPH_FULLNAME;
		protected System.Web.UI.WebControls.CompareValidator cfvCOP_OCCUPATICD;
		protected System.Web.UI.WebControls.CompareValidator mfvNPH_BIRTHDATE;
		//protected System.Web.UI.WebControls.CompareValidator cfvNPH_BIRTHDATE;

		private string NPH_LIFE;
		protected System.Web.UI.WebControls.Label lblServerError;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_cnic;
		

		protected System.Web.UI.HtmlControls.HtmlTableRow rowHeightWeight;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowBMIAccount;
		private string NU1_ACCOUNTNO;
		private string  NP1_PROPOSAL;
		private double  NP2_SETNO;
		private string  PPR_PRODCD;
		
						
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
			if (_lastEvent.Text.Equals(EnumControlArgs.Add.ToString()) || _lastEvent.Text.Equals("New"))
			{
				//				ace.ProcedureAdapter cs = new  ace.ProcedureAdapter("GENERATE_PROPOSALNO_CALL");
				//				cs.RegisetrOutParameter("PROPOSALNO", System.Data.OleDb.OleDbType.VarChar, 1000 );
				//				cs.Execute();
				//txtNP1_PROPOSAL.Text = Convert.ToString(cs.Get("PROPOSALNO"));
				//txtNP1_PROPOSAL.Text = "ILS00001";
			}

		}



		sealed protected override DataHolder GetInputData(DataHolder dataHolder)
		{
			return dataHolder;
		}

		sealed protected override void BindInputData(DataHolder dataHolder)
		{
			ViewInitialState();

			//***********************CUSTOM CODE ***********************/


			//For Bank Assurance			
			IDataReader LCSD_SYSTEMDTLReader3 = USE_USERMASTERDB.GetDDL_ILUS_ET_NM_PER_PROPOSALDET_USE_USERID_RO();;
			ddlUSE_USERID.DataSource = LCSD_SYSTEMDTLReader3 ;
			ddlUSE_USERID.DataBind();
			ddlUSE_USERID.SelectedValue=Session["s_USE_USERID"].ToString();
			LCSD_SYSTEMDTLReader3.Close();

			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_TITLE_RO();;
			ddlNPH_TITLE.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPH_TITLE.DataBind();
			LCSD_SYSTEMDTLReader0.Close();

			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_IDTYPE_RO();;
			ddlNPH_IDTYPE.DataSource = LCSD_SYSTEMDTLReader1;
			ddlNPH_IDTYPE.DataBind();
			LCSD_SYSTEMDTLReader1.Close();
			
			//************ Parameterization *********************
			if(isOccupationEnabled())
			{
				IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO();;
				ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1;
				ddlCOP_OCCUPATICD.DataBind();
				LCOP_OCCUPATIONReader1.Close();
			}

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();

			string RoundYearsDifference = "var ageRoundCriteria ='" + ace.clsIlasUtility.getAgeRoundingCriteria() + "';";

			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");" + RoundYearsDifference);
			FooterScript.Text = EnvHelper.Parse("") ;
			ddlCOP_OCCUPATICD.Attributes.Add("onchange","filterClass(this);");
			
			txtNPH_FULLNAME.Attributes.Add("onfocus","Name_GotFocus(this);");
			txtNPH_FULLNAME.Attributes.Add("onblur", "Name_LostFocus(this);");

			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));

			BindInputPlanData();
		}

		protected void BindInputPlanData()
		{
			IDataReader LPPR_PRODUCTReader0 = LPPR_PRODUCTDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO_FOR_NIB();;
			ddlPPR_PRODCD.DataSource = LPPR_PRODUCTReader0 ;

			if(SessionObject.GetString("s_CCD_CODE") =="5" && SessionObject.GetString("s_CCH_CODE")=="2")
			{
				ddlPPR_PRODCD.DataValueField="PPR_PRODCD";
				ddlPPR_PRODCD.DataTextField="DESC_S";
			}
			ddlPPR_PRODCD.DataBind();
			LPPR_PRODUCTReader0.Close();

			IDataReader PCU_CURRENCYReader2 = PCU_CURRENCYDB.GetDDL_ILUS_ET_NM_PLANDETAILS_PCU_CURRCODE_PRM_RO();;
			ddlPCU_CURRCODE_PRM.DataSource = PCU_CURRENCYReader2 ;
			ddlPCU_CURRCODE_PRM.DataBind();
			PCU_CURRENCYReader2.Close();

			IDataReader LCMO_MODEReader3 = LCMO_MODEDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CMO_MODE_RO();;
			ddlCMO_MODE.DataSource = LCMO_MODEReader3 ;
			ddlCMO_MODE.DataBind();
			LCMO_MODEReader3.Close();

			IDataReader CCB_CODEReader7 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PLANDETAILS_CCB_CODE_RO();;
			ddlCCB_CODE.DataSource = CCB_CODEReader7;
			ddlCCB_CODE.DataBind();
			CCB_CODEReader7.Close();

			rowset LNP2_POLICYMASTR = DB.executeQuery("select NP2_AGEPREM, NP2_AGEPREM2ND FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL='"+ SessionObject.Get("NP1_PROPOSAL")+"'");
			if (LNP2_POLICYMASTR.next())
			{
				txtNP2_AGEPREM.Text=LNP2_POLICYMASTR.getDouble("NP2_AGEPREM").ToString();
			}
			else
			{
				txtNP2_AGEPREM.Text="0";
			}
			
			ddlPCU_CURRCODE_PRM.Attributes.Add("onchange","Currency_OnChange(this);");
			ddlCMO_MODE.Attributes.Add("onchange","disableViewPremiumUptoAge(this);");
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

		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SaveProposalData(dataHolder);
			SavePersonalData(dataHolder);
			SavePlanData(dataHolder);
		}
	

		private void SaveProposalData(DataHolder dataHolder)
		{
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PER_PROPOSALDET();
			entityClass.setNameValueCollection(columnNameValue);

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;

					try
					{
						string insertLnp1  = "INSERT INTO LNP1_POLICYMASTR (NP1_PROPOSAL,NP2_SETNO,USE_USERID,NP1_QUOTATIONREF,NP1_CHANNEL,NP1_CHANNELDETAIL) VALUES(?,?,?,?,?,?)"; 
						SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

						pc.puts("@NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
						pc.puts("@NP2_SETNO","1");
						pc.puts("@USE_USERID",ddlUSE_USERID.SelectedValue.Trim()==""?null:ddlUSE_USERID.SelectedValue); 
						pc.puts("@NP1_QUOTATIONREF",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
						pc.puts("@NP1_CHANNEL","2");
						pc.puts("@NP1_CHANNELDETAIL","5");

						DB.executeDML(insertLnp1,pc);
					}
					catch(Exception ex)
					{
						throw ex;
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text);				
					


					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP1_CHANNEL","2");
					columnNameValue.Add("NP1_CHANNELDETAIL","5");
					columnNameValue.Add("NP2_SETNO","1");

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, "LNP1_POLICYMASTR");
					if (security.UpdateAllowed)
					{
						new LNP1_POLICYMASTR(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNP1_POLICYMASTR.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNP1_POLICYMASTR");
//						recordSelected = true;
						PrintMessage("Record has been updated");
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
			}
		}

		private void SavePersonalData(DataHolder dataHolder)
		{
			ErrorOccured.Text = "navigation = 'N';";

			if (_lastEvent.Text == "New") NewRecord = true;
			DMLSucceeded = false;
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PER_PERSONALDET();
			entityClass.setNameValueCollection(columnNameValue);

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;

					if(!searchClientByNIC())
					{
						try
						{
							string insertLnph  = @"INSERT INTO LNPH_PHOLDER (NPH_CODE, NPH_LIFE, COP_OCCUPATICD, NPH_FULLNAME, NPH_TITLE, 
												NPH_FIRSTNAME, NPH_SECONDNAME, NPH_LASTNAME, NPH_SEX, NPH_BIRTHDATE, NPH_IDNO, NPH_IDNO2, NPH_INSUREDTYPE, NPH_IDTYPE)
													VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)"; 
							SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

							pc.puts("@NPH_CODE",double.Parse("0"));
							pc.puts("@NPH_LIFE","D");
							pc.puts("@COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
							pc.puts("@NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);
							pc.puts("@NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
							pc.puts("@NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
							pc.puts("@NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
							pc.puts("@NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
							pc.puts("@NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
							pc.puts("@NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
							pc.puts("@NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
							pc.puts("@NPH_IDNO2",txtNP1_PROPOSAL.Text.Trim());
							pc.puts("@NPH_INSUREDTYPE","Y");
							pc.puts("@NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);

							DB.executeDML(insertLnph,pc);
						


							SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();

							pm.puts("@NP1_PROPOSAL", txtNP1_PROPOSAL.Text);
							pm.puts("@NPH_CODE",double.Parse("0"));
							pm.puts("@NPH_LIFE","D");
							pm.puts("@NU1_LIFE","F");
							pm.puts("@NU1_PAYER","B");
					
							DB.executeDML("delete from lnu1_underwriti where NPH_CODE=0 and NP1_PROPOSAL='"+ txtNP1_PROPOSAL.Text +"' and NU1_LIFE='F'");
								DB.executeDML(@"insert into lnu1_underwriti (np1_proposal,nph_code,nph_life,nu1_life,nu1_payer) 
												values (?,?,?,?,?)", pm);
								DB.executeDML("update lnp2_policymastr set np2_ageprem=" + txtNP2_AGEPREM.Text + " Where np1_proposal = '"+txtNP1_PROPOSAL.Text+"' ");
						}
						catch(Exception ex)
						{
							throw ex;
						}
					}
					else
					{
						DMLSucceeded = false;
						ErrorOccured.Text = "navigation = 'N';";
						ErrorHandle("Client with CNIC No "+ txtCNIC_VALUE.Text +"Already Exist");
					}

					_lastEvent.Text = "Save"; 					
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
				case (EnumControlArgs.Update):
					DB.BeginTransaction();
					SaveTransaction = true;

					txtNPH_CODE.Text="0";
					txtNPH_LIFE.Text="D";

					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindIllusByPK(txtNP1_PROPOSAL.Text,txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);
					columnNameValue.Add("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text.Trim()==""?null:txtNPH_FIRSTNAME.Text);
					columnNameValue.Add("NPH_SECONDNAME",txtNPH_SECONDNAME.Text.Trim()==""?null:txtNPH_SECONDNAME.Text);
					columnNameValue.Add("NPH_LASTNAME",txtNPH_LASTNAME.Text.Trim()==""?null:txtNPH_LASTNAME.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_IDNO", txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.UpdateAllowed)
					{
						try
						{
							new LNPH_PHOLDER(dataHolder).Update(columnNameValue);
							dataHolder.Update(DB.Transaction);
						}
						catch(Exception ex)
						{
							throw ex;
						}

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPH_PHOLDER");

					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					DMLSucceeded = true;
					ErrorOccured.Text = "navigation = 'Y';";
					break;
			}
		}

		private void SavePlanData(DataHolder dataHolder)
		{
			SessionObject.Set("PPR_PRODCD", Convert.ToString(ddlPPR_PRODCD.SelectedValue));

			SessionObject.Set("VALIDATION_ERROR","");
			
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = true;		
			shgn.SHGNCommand entityClass=new ace.ILUS_ET_NM_PLANDETAILS();
			entityClass.setNameValueCollection(columnNameValue);

			NameValueCollection columnNameValueNonBase = new NameValueCollection();
			
			entityClass.setNameValueCollection(columnNameValueNonBase);

			if (_lastEvent.Text == "New") NewRecord = true;
			DMLSucceeded = false;

			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;
								
					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.SaveAllowed)
					{
						string insertNewPlan  = @"INSERT INTO LNPR_PRODUCT (NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,NPR_BASICFLAG,NPR_BENEFITTERM
													,CCB_CODE,NPR_SUMASSURED,CMO_MODE,PCU_CURRCODE,NPR_PREMIUMTER) 
													VALUES(?,?,?,?,?,?,?,?,?,?)"; 
						SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

						pc.puts("@NP1_PROPOSAL",		txtNP1_PROPOSAL.Text);
						pc.puts("@NP2_SETNO",			"1");
						pc.puts("@PPR_PRODCD",			 ddlPPR_PRODCD.SelectedValue);
						pc.puts("@NPR_BASICFLAG",		"Y");
						pc.puts("@NPR_BENEFITTERM",		txtNPR_BENEFITTERM.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_BENEFITTERM.Text));
						pc.puts("@CCB_CODE",			ddlCCB_CODE.SelectedValue.Trim());
						pc.puts("@NPR_SUMASSURED",		txtNPR_SUMASSURED.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_SUMASSURED.Text));
						pc.puts("@CMO_MODE",			ddlCMO_MODE.SelectedValue.Trim());
						pc.puts("@PCU_CURRCODE",		ddlPCU_CURRCODE_PRM.SelectedValue);
						pc.puts("@NPR_PREMIUMTER",		txtNPR_PREMIUMTER.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_PREMIUMTER.Text));
						
						DB.executeDML(insertNewPlan,pc);

						try
						{
							entityClass.fsoperationAfterSave();
						}
						catch(FieldValidationException ex)
						{
							Session["FLAG_RESET_PREMIUM"] = "";
							SessionObject.Set("VALIDATION_ERROR",ex.Message);
							throw new ProcessException("<<Validation Error>>");
						}
				
						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPR_PRODUCT");
						_lastEvent.Text = "Save";

						NewRecord = false;
						DMLSucceeded = true;

						PrintMessage("Record has been saved");
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;

					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,double.Parse("1"),ddlPPR_PRODCD.SelectedValue);				
					
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE_PRM.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE_PRM.SelectedValue);
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("CMO_MODE",ddlCMO_MODE.SelectedValue.Trim()==""?null:ddlCMO_MODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",double.Parse("1"));
					columnNameValue.Add("NPR_BASICFLAG","Y");


					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					if (security.UpdateAllowed)
					{
						rowset rsProduct = DB.executeQuery("select PPR_PRODCD from LNPR_PRODUCT where NP1_PROPOSAL='"+txtNP1_PROPOSAL.Text+"' and NPR_BASICFLAG='Y'" );
						rsProduct.next();

						//Check if Product been changed
						if (!ddlPPR_PRODCD.SelectedValue.Trim().Equals(rsProduct.getString("PPR_PRODCD")))
						{
							string proposal = txtNP1_PROPOSAL.Text;
							string newProduct = ddlPPR_PRODCD.SelectedValue;
							double setNo      = txtNP2_SETNO.Text.Trim()=="" ? double.Parse("0") : double.Parse(txtNP2_SETNO.Text);

							//**********************************************************************************// 
							//************* Delete old Product(PLAN) and its related data **********************//
							//**********************************************************************************//
							DB.executeDML("delete from LNQD_QUESTIONDETAIL where NP1_PROPOSAL='" + proposal + "'");
							DB.executeDML("delete from LNQN_QUESTIONNAIRE  where NP1_PROPOSAL='" + proposal + "'");
							DB.executeDML("delete from LNLO_LOADING        where NP1_PROPOSAL='" + proposal + "'");
							DB.executeDML("delete from LNLO_LOADING_ACTUAL where NP1_PROPOSAL='" + proposal + "'");
							DB.executeDML("delete from LNFU_FUNDS          where NP1_PROPOSAL='" + proposal + "'");
							DB.executeDML("delete from LNPR_PRODUCT        where NP1_PROPOSAL='" + proposal + "'");
							//DB.executeDML("delete from LNPR_PRODUCT        where NP1_PROPOSAL='" + proposal + "' and (PPR_PRODCD='" + rsProduct.getString("PPR_PRODCD")+ "')");

							//**********************************************************************************//
							//************************** Insert New Product(PLAN) ******************************//
							//**********************************************************************************//
							string insertNewPlan  = "INSERT INTO LNPR_PRODUCT (NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,NPR_BASICFLAG,NPR_BENEFITTERM,CCB_CODE,NPR_SUMASSURED,NPR_TOTPREM,NPR_PREMIUM,CMO_MODE,PCU_CURRCODE,NPR_PREMIUMTER,NPR_INDEXATION,NPR_INDEXRATE,NPR_EDUNITS,NPR_PAIDUPTOAGE,NPR_PREMIUMDISCOUNT,NPR_INCLUDELOADINNIV,NPR_EXCESSPREMIUM,NPR_COMMLOADING,NPR_INTERESTRATE) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"; 
							SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
							pc.puts("@NP1_PROPOSAL",		proposal);
							pc.puts("@NP2_SETNO",			setNo);
							pc.puts("@PPR_PRODCD",			newProduct);
							pc.puts("@NPR_BASICFLAG",		"Y");
							pc.puts("@NPR_BENEFITTERM",		txtNPR_BENEFITTERM.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_BENEFITTERM.Text));
							pc.puts("@CCB_CODE",			ddlCCB_CODE.SelectedValue.Trim());
							pc.puts("@NPR_SUMASSURED",		txtNPR_SUMASSURED.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_SUMASSURED.Text));
							pc.puts("@NPR_TOTPREM",			txtNPR_TOTPREM.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_TOTPREM.Text));
							pc.puts("@NPR_PREMIUM",			txtNPR_PREMIUM.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_PREMIUM.Text));
							pc.puts("@CMO_MODE",			ddlCMO_MODE.SelectedValue.Trim());
							pc.puts("@PCU_CURRCODE",		ddlPCU_CURRCODE.SelectedValue);
							pc.puts("@NPR_PREMIUMTER",		txtNPR_PREMIUMTER.Text.Trim()==""?double.Parse("0"):double.Parse(txtNPR_PREMIUMTER.Text));
							pc.puts("@NPR_INDEXATION",		ddlNPR_INDEXATION.SelectedValue);
							DB.executeDML(insertNewPlan,pc);
						}
						else
						{
							new LNPR_PRODUCT(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);
							dataHolder.Update(DB.Transaction);
						}

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPR_PRODUCT");
//						recordSelected = true;
						PrintMessage("Record has been updated");

						NewRecord = false;
						DMLSucceeded = true;

						string anyMessageToInform = ((ace.ILUS_ET_NM_PLANDETAILS) entityClass).strRidersUpdateInformation.Trim();
						if(anyMessageToInform.Length >0)
						{
							PrintMessage(anyMessageToInform);
						}
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
					
				case (EnumControlArgs.Process):						
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPR_PRODUCTDB(dataHolder).FindByPK(txtNP1_PROPOSAL.Text,1,ddlPPR_PRODCD.SelectedValue);				
					columnNameValue.Add("PPR_PRODCD",ddlPPR_PRODCD.SelectedValue.Trim()==""?null:ddlPPR_PRODCD.SelectedValue);
					columnNameValue.Add("PCU_CURRCODE",ddlPCU_CURRCODE_PRM.SelectedValue.Trim()==""?null:ddlPCU_CURRCODE_PRM.SelectedValue);
					
					columnNameValue.Add("CCB_CODE",ddlCCB_CODE.SelectedValue.Trim()==""?null:ddlCCB_CODE.SelectedValue.Trim());
					columnNameValue.Add("CMO_MODE",ddlCMO_MODE.SelectedValue.Trim()==""?null:ddlCMO_MODE.SelectedValue.Trim());
					columnNameValue.Add("NPR_SUMASSURED",txtNPR_SUMASSURED.Text.Trim()==""?null:(object)double.Parse(txtNPR_SUMASSURED.Text));
//					columnNameValue.Add("NPR_PAIDUPTOAGE",txtNPR_PAIDUPTOAGE.Text.Trim()==""?null:(object)double.Parse(txtNPR_PAIDUPTOAGE.Text));
					columnNameValue.Add("NPR_BENEFITTERM",txtNPR_BENEFITTERM.Text.Trim()==""?null:(object)double.Parse(txtNPR_BENEFITTERM.Text));
					columnNameValue.Add("NPR_PREMIUMTER",txtNPR_PREMIUMTER.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMTER.Text));
//					columnNameValue.Add("NPR_INCLUDELOADINNIV",ddlNPR_INCLUDELOADINNIV.SelectedValue.Trim()==""?null:ddlNPR_INCLUDELOADINNIV.SelectedValue);
//					columnNameValue.Add("NPR_PREMIUMDISCOUNT",txtNPR_PREMIUMDISCOUNT.Text.Trim()==""?null:(object)double.Parse(txtNPR_PREMIUMDISCOUNT.Text));
//					columnNameValue.Add("NPR_EXCESSPREMIUM",txtNPR_EXCESPRMANNUAL.Text.Trim()==""?null:(object)double.Parse(txtNPR_EXCESPRMANNUAL.Text));
//					columnNameValue.Add("NPR_COMMLOADING",ddlNPR_COMMLOADING.SelectedValue.Trim()==""?null:ddlNPR_COMMLOADING.SelectedValue);
					columnNameValue.Add("NPR_TOTPREM",txtNPR_TOTPREM.Text.Trim()==""?null:(object)double.Parse(txtNPR_TOTPREM.Text));
					columnNameValue.Add("NP1_PROPOSAL",txtNP1_PROPOSAL.Text.Trim()==""?null:txtNP1_PROPOSAL.Text);
					columnNameValue.Add("NP2_SETNO",txtNP2_SETNO.Text.Trim()==""?null:(object)double.Parse(txtNP2_SETNO.Text));
					columnNameValue.Add("NPR_INDEXATION",ddlNPR_INDEXATION.SelectedValue.Trim()==""?null:ddlNPR_INDEXATION.SelectedValue);
//					columnNameValue.Add("NPR_INDEXRATE",txtNPR_INDEXRATE.Text.Trim()==""?(object)double.Parse("0"):(object)double.Parse(txtNPR_INDEXRATE.Text));
//					columnNameValue.Add("NPR_EDUNITS",txtNPR_EDUNITS.Text.Trim()==""?null:(object)double.Parse(txtNPR_EDUNITS.Text));
//					columnNameValue.Add("NPR_INTERESTRATE",txtNPR_INTERESTRATE.Text.Trim()==""?null:(object)double.Parse(txtNPR_INTERESTRATE.Text));
					columnNameValue.Add("NPR_BASICFLAG",txtNPR_BASICFLAG.Text.Trim()==""?null:txtNPR_BASICFLAG.Text);

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPR_PRODUCT.PrimaryKeys, columnNameValue, "LNPR_PRODUCT");
					string result="";					
					if (_CustomArgName.Value == "ProcessName")
					{
						string processName = _CustomArgVal.Value;	
						if(processName == "RecalculatePremFromPersonalPage")
						{
//							BA.BAUtility.RecalculatePemium(Convert.ToString(SessionObject.Get("NP1_PROPOSAL")),int.Parse(txtNP1_RETIREMENTAGE.Text));
						}
						else
						{
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
									proccessCommand.setPrimaryKeys(LNPR_PRODUCT.PrimaryKeys);
									proccessCommand.setTableName("LNPR_PRODUCT");
									proccessCommand.setDataRows(dataRows);
									proccessCommand.setSelectedRows(SelectedRowIndexes);
									result = proccessCommand.processing();
									//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
									//Update_Term_For_Riders();
									NewRecord = false;
									DMLSucceeded = true;

									if(ace.clsIlasUtility.isNoorIllustrion() && processName == "ace.Calculate_Premium")
									{
								//		ILLUSTRATE_NOOR = true;
									}

								}
							}
							else
							{
								result = "You are not Autherized to Execute Process.";
							}						
						}
					}	


					//recordSelected =true;
					if (result.Length>0)
						PrintMessage(result);
					break;
			}

		}

		
		sealed protected override void DataBind(DataHolder dataHolder)
		{			
			LNPH_PHOLDERDB LNPH_PHOLDERDB_obj = new LNPH_PHOLDERDB(dataHolder);		
			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit)
			{
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
			RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("");
			
		}
		#endregion	

		#region Events
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
			if (SetFieldsInSession())
			{
				if (NewRecord == true  && DMLSucceeded == false)
				{
					SessionObject.Set("NPH_TITLE","");
					SessionObject.Set("NPH_IDTYPE","");
					SessionObject.Set("NP1_PROPOSAL","");
					SessionObject.Set("NPH_SEX","");
					SessionObject.Set("NPH_FULLNAME","");

					SessionObject.Set("NPH_FIRSTNAME","");
					SessionObject.Set("NPH_SECONDNAME","");
					SessionObject.Set("NPH_LASTNAME","");

					SessionObject.Set("NPH_BIRTHDATE","");
					SessionObject.Set("COP_OCCUPATICD","");
					SessionObject.Set("NPH_CODE","");
					SessionObject.Set("NPH_LIFE","");
				}
				else
				{
					SessionObject.Set("NPH_TITLE",ddlNPH_TITLE.SelectedValue);
					SessionObject.Set("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue);
					SessionObject.Set("NPH_SEX",ddlNPH_SEX.SelectedValue);
					SessionObject.Set("NPH_FULLNAME",txtNPH_FULLNAME.Text);

					SessionObject.Set("NPH_FIRSTNAME",txtNPH_FIRSTNAME.Text);
					SessionObject.Set("NPH_SECONDNAME",txtNPH_SECONDNAME.Text);
					SessionObject.Set("NPH_LASTNAME",txtNPH_LASTNAME.Text);

					SessionObject.Set("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text);
					SessionObject.Set("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue);
				}
			}
		}										
	
		
		protected void btntest_click(object sender, EventArgs e) 
		{
			ControlArgs = new object[1];
			if(_lastEvent.Text== "Edit")
			{
				_CustomEventVal.Value ="Update";
				ControlArgs[0]=EnumControlArgs.Update;
			}
			else
			{
				_CustomEventVal.Value ="Save";
				ControlArgs[0]=EnumControlArgs.Save;
			}
			CustomDoControl();
		}

		protected void btnBack_click(object sender, EventArgs e) 
		{
			Response.Redirect("../Presentation/ApplicationSelection.aspx");
		}

		#endregion 

		void RefreshDataFields()
		{
			ddlNPH_TITLE.ClearSelection();
			ddlNPH_IDTYPE.ClearSelection();
			ddlNPH_SEX.ClearSelection();
			txtNPH_FULLNAME.Text="";

			txtNPH_FIRSTNAME.Text="";
			txtNPH_SECONDNAME.Text="";
			txtNPH_LASTNAME.Text="";

			txtNPH_BIRTHDATE.Text="";
			ddlCOP_OCCUPATICD.ClearSelection();
			txtCNIC_VALUE.Text="";
		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();

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
			
			txtNPH_FULLNAME.Text=objRow["NPH_FULLNAME"].ToString();
			
			txtNPH_FIRSTNAME.Text=objRow["NPH_FIRSTNAME"].ToString();
			txtNPH_SECONDNAME.Text=objRow["NPH_SECONDNAME"].ToString();
			txtNPH_LASTNAME.Text=objRow["NPH_LASTNAME"].ToString();

			txtCNIC_VALUE.Text = objRow["NPH_IDNO"].ToString();
			
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

			txtNPH_BIRTHDATE.Text=objRow["NPH_BIRTHDATE"]== DBNull.Value?"":((DateTime)objRow["NPH_BIRTHDATE"]).ToShortDateString();

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
			}
			

		}

		protected void ShowPlanData(DataRow objRow)
		{
			ddlPPR_PRODCD.ClearSelection();
			ListItem item6=ddlPPR_PRODCD.Items.FindByValue(Convert.ToString(objRow["PPR_PRODCD"]));
			ddlPPR_PRODCD.SelectedValue=Convert.ToString(objRow["PPR_PRODCD"]);
			if (item6!= null)
			{
				item6.Selected=true;
			}

			ddlCCB_CODE.ClearSelection();
			ListItem item7=ddlCCB_CODE.Items.FindByValue(Convert.ToString(objRow["CCB_CODE"]));
			ddlCCB_CODE.SelectedValue=Convert.ToString(objRow["CCB_CODE"]);
			if (item7!= null)
			{
				item7.Selected=true;
			}

			ddlCMO_MODE.ClearSelection();
			ListItem item8=ddlCMO_MODE.Items.FindByValue(Convert.ToString(objRow["CMO_MODE"]));
			ddlCMO_MODE.SelectedValue=Convert.ToString(objRow["CMO_MODE"]);
			if (item8!= null)
			{
				item8.Selected=true;
			}
				
			ddlPCU_CURRCODE_PRM.ClearSelection();
			ListItem item9=ddlPCU_CURRCODE_PRM.Items.FindByValue(Convert.ToString(objRow["PCU_CURRCODE"]));
			ddlPCU_CURRCODE_PRM.SelectedValue=Convert.ToString(objRow["PCU_CURRCODE"]);
			if (item9!= null)
			{
				item9.Selected=true;
			}
					
			txtNPR_PREMIUMTER.Text=objRow["NPR_PREMIUMTER"].ToString();
			txtNPR_SUMASSURED.Text=objRow["NPR_SUMASSURED"].ToString();
			txtNPR_BENEFITTERM.Text=objRow["NPR_BENEFITTERM"].ToString();
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

			if(SessionObject.GetString("QUOTATION_SELECTED") != "Y")
			{
				selected  = false;
			}

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
	
				NP1_PROPOSAL=SessionObject.GetString("NP1_PROPOSAL");
				NP2_SETNO=1;
				PPR_PRODCD=SessionObject.GetString("PPR_PRODCD");
			
				DataRow selectedProposalRow = new LNP1_POLICYMASTRDB(dataHolder).FindByPK(NP1_PROPOSAL)["LNP1_POLICYMASTR"].Rows[0];
				txtNP1_PROPOSAL.Text=selectedProposalRow["NP1_PROPOSAL"].ToString();
				ddlUSE_USERID.SelectedValue=selectedProposalRow["USE_USERID"].ToString();

				DataRow selectedRow = new LNPH_PHOLDERDB(dataHolder).FindByPK(NPH_CODE,NPH_LIFE)["LNPH_PHOLDER"].Rows[0];
				ShowData(selectedRow);							
				
				DataRow selectedPlanRow = new LNPR_PRODUCTDB(dataHolder).FindByPK(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD)["LNPR_PRODUCT"].Rows[0];
				ShowPlanData(selectedPlanRow);

				_lastEvent.Text = "Edit";

				Session.Add("QUOTATION_SELECTED", "");

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
			SessionObject.Remove("NPH_FULLNAME");
			
			SessionObject.Remove("NPH_FIRSTNAME");
			SessionObject.Remove("NPH_SECONDNAME");
			SessionObject.Remove("NPH_LASTNAME");

			SessionObject.Remove("NPH_BIRTHDATE");
			SessionObject.Remove("COP_OCCUPATICD");

		}


		private void ViewInitialState()
		{
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE, NU1_SMOKER, NU1_ACCOUNTNO from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='F'");
				if (rsLNPH_PHOLDERDB.next())
				{
					SessionObject.Set("NPH_CODE",rsLNPH_PHOLDERDB.getString(1));
					SessionObject.Set("NPH_LIFE",rsLNPH_PHOLDERDB.getString(2));
					SessionObject.Set("NU1_SMOKER",rsLNPH_PHOLDERDB.getString(3));
					SessionObject.Set("NU1_ACCOUNTNO",rsLNPH_PHOLDERDB.getString(4));
				}

		}


		private bool searchClientByNIC()
		{
			string query = "SELECT 'A' FROM LNPH_PHOLDER WHERE NPH_IDNO ="+ txtCNIC_VALUE.Text ;
			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}

