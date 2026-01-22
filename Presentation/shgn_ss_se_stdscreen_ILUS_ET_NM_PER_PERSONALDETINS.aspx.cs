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

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ET_NM_PER_PERSONALDETINS : SHMA.Enterprise.Presentation.TwoStepController
	{
	
		//controls


		//		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		//		protected System.Web.UI.WebControls.DropDownList pagerList;
		protected System.Web.UI.WebControls.Literal _lastEvent;
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden FIELD_COMBINATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden VALUE_COMBINATION;

		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		NameValueCollection columnNameValue=null;

		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";

		
		/******************* Entity Fields Decleration *****************/
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPH_FULLNAMEARABIC;
		//protected System.Web.UI.WebControls.CompareValidator cfvNPH_BIRTHDATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOP_OCCUPATICD;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvNPH_INSUREDTYPE;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_ACCOUNTNO;						

		/************ pk variables declaration ************/
				
		#region pk variables declaration		
		private string  NPH_CODE;
		//protected System.Web.UI.WebControls.CompareValidator cfvNPH_INSUREDTYPE;

		//protected System.Web.UI.WebControls.RequiredFieldValidator msgNU1_ACCOUNTNO;

		private string  NPH_LIFE;
		private string  NU1_SMOKER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvNU1_SMOKER;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList Dropdownlist3;
		protected SHMA.Enterprise.Presentation.WebControls.TextBox TextBox2;
		protected SHMA.Enterprise.Presentation.WebControls.DropDownList ddlNPH_SELECTION;


		//protected System.Web.UI.WebControls.RequiredFieldValidator rfv_cnic;
		private string NU1_ACCOUNTNO;
						
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
		protected override void ValidateParams() 
		{
			base.ValidateParams ();			
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
			return   dataHolder;         

		}
		sealed protected override void BindInputData(DataHolder dataHolder)
		{

			//TODO: SESSION SETTING in behavior ViewInitialState()
			//***********************CUSTOM CODE ***********************/
			ViewInitialState();
			//***********************CUSTOM CODE ***********************/

			IDataReader LCSD_SYSTEMDTLReader0 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_NPH_TITLE_RO();;
			ddlNPH_TITLE.DataSource = LCSD_SYSTEMDTLReader0 ;
			ddlNPH_TITLE.DataBind();
			LCSD_SYSTEMDTLReader0.Close();
			
			IDataReader LCSD_SYSTEMDTLReader1 = LCSD_SYSTEMDTLDB.GetDDL_ILUS_ET_NM_PER_PERSONALDET_NPH_IDTYPE_RO();;
			ddlNPH_IDTYPE.DataSource = LCSD_SYSTEMDTLReader1;
			ddlNPH_IDTYPE.DataBind();
			LCSD_SYSTEMDTLReader1.Close();


			IDataReader LCOP_OCCUPATIONReader1 = LCOP_OCCUPATIONDB.GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_COP_OCCUPATICD_RO();;
			ddlCOP_OCCUPATICD.DataSource = LCOP_OCCUPATIONReader1 ;
			ddlCOP_OCCUPATICD.DataBind();
			LCOP_OCCUPATIONReader1.Close();

			IDataReader LCCL_CATEGORYReader2 = LCCL_CATEGORYDB.GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_CCL_CATEGORYCD_RO();;
			ddlCCL_CATEGORYCD.DataSource = LCCL_CATEGORYReader2 ;
			ddlCCL_CATEGORYCD.DataBind();
			LCCL_CATEGORYReader2.Close();

			_lastEvent.Text = "New";		

			FindAndSelectCurrentRecord();

			CSSLiteral.Text = ace.Ace_General.LoadPageStyle();
			HeaderScript.Text = EnvHelper.Parse("var sysDate=SV(\"s_CURR_SYSDATE\");");
			FooterScript.Text = EnvHelper.Parse("") ;
			
		
			ddlCOP_OCCUPATICD.Attributes.Add("onchange","filterClass(this);");
            txtNPH_FULLNAME.Attributes.Add("onfocus", "Name_GotFocus(this);");
            txtNPH_FULLNAME.Attributes.Add("onblur", "Name_LostFocus(this);");

            //RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            //***Changed from: RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
            RegisterArrayDeclaration("AllowedProcess", (AllowedProcess.Equals("")?"0":AllowedProcess));	


		}
		#endregion
    
		#region Major methods of the final step
		protected override void ValidateRequest() 
		{
			base.ValidateRequest();									
			foreach (string key in LNPH_PHOLDER.PrimaryKeys)
			{
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null)
				{
					if (ctrl is WebControl)
					{
						//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null))
						{
							control.Enabled = true;
						}				
					}
				}
			}			
		}
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder)
		{
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			shgn.SHGNCommand entityClass= new ace.ILUS_ET_NM_PER_PERSONALDETINS();
			entityClass.setNameValueCollection(columnNameValue);

            string ownerNIC=SessionObject.Get("s_NPH_IDNO").ToString();
            if (ownerNIC== txtCNIC_VALUE.Text.Replace("-", ""))
            {
                //throw new Exception("Owner and Insured should not be same");
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Owner and Insured should not be same')", true);
                 return;
            }
			//Custom : Change
			SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue.Trim()==""?"N":ddlNU1_SMOKER.SelectedValue);
			//SessionObject.Set("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);



			SessionObject.Set("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
			SessionObject.Set("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text==""?null:txtNU1_ACTUALWEIGHT.Text);

			SessionObject.Set("NP2_AGEPREM",txtNP2_AGEPREM.Text==""?null:txtNP2_AGEPREM.Text);

            //SessionObject.Set("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text==""?null:txtNU1_CONVERTWEIGHT.Text);
            SessionObject.Set("NPH_WEIGHT",convertToKiloGram(ddlNPH_WEIGHTTTYPE.SelectedValue.Trim(),Convert.ToDouble(txtNU1_ACTUALWEIGHT.Text)));

            SessionObject.Set("NPH_FATHERNAME", txtNPH_FATHERNAME.Text == "" ? null : txtNPH_FATHERNAME.Text);
            SessionObject.Set("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text == "" ? null : txtNPH_MAIDENNAME.Text);
            SessionObject.Set("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text == "" ? null : txtNPH_DOCISSUEDAT.Text);
            SessionObject.Set("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text == "" ? null : txtNPH_DOCEXPIRDAT.Text);

            SessionObject.Set("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
			SessionObject.Set("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text==""?null:txtNU1_ACTUALHEIGHT.Text);
			//SessionObject.Set("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text==""?null:txtNU1_CONVERTHEIGHT.Text);
            SessionObject.Set("NPH_HEIGHT", convertToMeters(ddlNPH_HEIGHTTYPE.SelectedValue.Trim(), Convert.ToDouble(txtNU1_ACTUALHEIGHT.Text)));
            SessionObject.Set("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);
	


			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
				case (EnumControlArgs.Save):
					_lastEvent.Text = "Save";
					DB.BeginTransaction();
					SaveTransaction = true;


					if(txtNPH_CODE.Text.Equals(""))
						txtNPH_CODE.Text = "0";

					txtNPH_LIFE.Text = "D";
					//TODO: NU1_LIFE = "F";

					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					//CUSTOM: ARABIC
					//columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_FULLNAMEARABIC",null);
					//CUSTOM: ARABIC

					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",null);
                    //>>>>columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
                    //>>>>columnNameValue.Add("NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
                    //>>>>columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));


                    columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					//columnNameValue.Add("NP1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text.Trim()==""?null:txtNU1_ACCOUNTNO.Text);
					columnNameValue.Add("NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
					columnNameValue.Add("NPH_IDNO2",txtNPH_IDNO2.Text.Trim()==""?null:txtNPH_IDNO2.Text);

						

	
					//Izhar-Ul-Haque
					columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text.Trim()==""?null:txtNU1_ACTUALWEIGHT.Text);
					//columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text.Trim()==""?null:txtNU1_CONVERTWEIGHT.Text);
					columnNameValue.Add("NPH_WEIGHT", convertToKiloGram(ddlNPH_WEIGHTTTYPE.SelectedValue.Trim(), Convert.ToDouble(txtNU1_ACTUALWEIGHT.Text)));

                    columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text.Trim()==""?null:txtNU1_ACTUALHEIGHT.Text);
                    //columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text.Trim()==""?null:txtNU1_CONVERTHEIGHT.Text);
                    columnNameValue.Add("NPH_HEIGHT", convertToMeters(ddlNPH_HEIGHTTYPE.SelectedValue.Trim(), Convert.ToDouble(txtNU1_ACTUALHEIGHT.Text)));
                    //columnNameValue.Add("NU1_OVERUNDERWT",txt_bmi.Text==""?null:txt_bmi.Text);


                    string code="";
					int clientExistInIlas = ace.ILUS_ET_NM_PER_PERSONALDET.ClientExistInIlas(Convert.ToString(columnNameValue.get("NPH_IDNO")));
					int clientExist = ace.ILUS_ET_NM_PER_PERSONALDET.ClientExist(Convert.ToString(columnNameValue.get("NPH_IDNO")));
					if(clientExistInIlas!=0)
					{
						code=clientExistInIlas.ToString();
					}
					else if(clientExist!=0)
					{
						code=clientExist.ToString();
					}
					else
					{
						code=ace.ILUS_ET_NM_PER_PERSONALDET.getClientNumber();
					}

					security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.SaveAllowed)
					{

						//new LNPH_PHOLDER(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ET_NM_PER_PERSONALDETINS","NPH_CODE");

						/**************** Changed by Asif: Checking for Existing client ********************/
						/******************************  Checked by NIC ************************************/
						//bool clientExist = ace.ILUS_ET_NM_PER_PERSONALDET.ClientExist(Convert.ToString(columnNameValue.get("NPH_IDNO")));
						if(clientExist!=0)
						{	//In case of Existing Person 
							txtNPH_CODE.Text = code;
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
						}

						dataHolder.Update(DB.Transaction);
						SessionObject.Set("NPH_CODE_s",columnNameValue.get("NPH_CODE"));

						dataHolder.Update(DB.Transaction);
						SessionObject.Set("NPH_CODE_s",columnNameValue.get("NPH_CODE"));


						//CUSTOM: ARABIC
						DB.executeDML("UPDATE LNPH_PHOLDER SET NPH_FULLNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtNPH_FULLNAMEARABIC.Text)) + "' WHERE NPH_CODE="+columnNameValue["NPH_CODE"]);
						//CUSTOM: ARABIC

						/**************** Changed by Asif: Checking for Existing client ********************/
						if(clientExist!=0)
						{
							entityClass.fsoperationAfterUpdate();
						}
						else
						{
							entityClass.fsoperationAfterSave();
						}

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "LNPH_PHOLDER");
						_lastEvent.Text = "Save"; 					
						//PrintMessage("Record has been saved");
						
					}
					else
					{
						PrintMessage("You are not autherized to Save.");
					}
					break;
				case (EnumControlArgs.Update):					
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);

					//CUSTOM: ARABIC
					//columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_FULLNAMEARABIC",null);
					//CUSTOM: ARABIC
					
					
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",null);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);
					columnNameValue.Add("NPH_IDNO",txtCNIC_VALUE.Text.Trim()==""?null:txtCNIC_VALUE.Text.Replace("-",""));
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));

                    //Izhar-Ul-Haque
                    columnNameValue.Add("NPH_WEIGHTUOM",ddlNPH_WEIGHTTTYPE.SelectedValue.Trim()==""?null:ddlNPH_WEIGHTTTYPE.SelectedValue);
					columnNameValue.Add("NPH_WEIGHTACTUAL",txtNU1_ACTUALWEIGHT.Text.Trim()==""?null:txtNU1_ACTUALWEIGHT.Text);
					//columnNameValue.Add("NPH_WEIGHT",txtNU1_CONVERTWEIGHT.Text.Trim()==""?null:txtNU1_CONVERTWEIGHT.Text);
                    columnNameValue.Add("NPH_WEIGHT", convertToKiloGram(ddlNPH_WEIGHTTTYPE.SelectedValue.Trim(), Convert.ToDouble(txtNU1_ACTUALWEIGHT.Text)));

                    columnNameValue.Add("NPH_HEIGHTUOM",ddlNPH_HEIGHTTYPE.SelectedValue.Trim()==""?null:ddlNPH_HEIGHTTYPE.SelectedValue);
					columnNameValue.Add("NPH_HEIGHTACTUAL",txtNU1_ACTUALHEIGHT.Text.Trim()==""?null:txtNU1_ACTUALHEIGHT.Text);
					//columnNameValue.Add("NPH_HEIGHT",txtNU1_CONVERTHEIGHT.Text.Trim()==""?null:txtNU1_CONVERTHEIGHT.Text);
                    columnNameValue.Add("NPH_HEIGHT", convertToMeters(ddlNPH_HEIGHTTYPE.SelectedValue.Trim(), Convert.ToDouble(txtNU1_ACTUALHEIGHT.Text)));



                    security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, "LNPH_PHOLDER");
					if (security.UpdateAllowed)
					{
						entityClass.fsoperationBeforeUpdate();

						new LNPH_PHOLDER(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

						dataHolder.Update(DB.Transaction);
						SessionObject.Set("NPH_CODE_s",columnNameValue.get("NPH_CODE"));

						//CUSTOM: ARABIC
						DB.executeDML("UPDATE LNPH_PHOLDER SET NPH_FULLNAMEARABIC='" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtNPH_FULLNAMEARABIC.Text)) + "' WHERE NPH_CODE="+columnNameValue["NPH_CODE"]);
						//CUSTOM: ARABIC

						
						entityClass.fsoperationAfterUpdate();

						auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), LNPH_PHOLDER.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "LNPH_PHOLDER");
						//recordSelected = true;
						//PrintMessage("Record has been updated");
					}
					else
					{
						PrintMessage("You are not autherized to Update.");
					}
					break;
				case (EnumControlArgs.Delete):
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);
					columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));
                    columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",null);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);

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
					break;
				case (EnumControlArgs.Process):						
					DB.BeginTransaction();
					SaveTransaction = true;
					dataHolder = new LNPH_PHOLDERDB(dataHolder).FindByPK(txtNPH_CODE.Text,txtNPH_LIFE.Text);				
					columnNameValue.Add("NPH_TITLE",ddlNPH_TITLE.SelectedValue.Trim()==""?null:ddlNPH_TITLE.SelectedValue);
					columnNameValue.Add("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue.Trim()==""?null:ddlNPH_IDTYPE.SelectedValue);
					columnNameValue.Add("NPH_SEX",ddlNPH_SEX.SelectedValue.Trim()==""?null:ddlNPH_SEX.SelectedValue);
					columnNameValue.Add("NPH_FULLNAME",txtNPH_FULLNAME.Text.Trim()==""?null:txtNPH_FULLNAME.Text);
                    columnNameValue.Add("NPH_FATHERNAME", txtNPH_FATHERNAME.Text.Trim() == "" ? null : txtNPH_FATHERNAME.Text);
                    columnNameValue.Add("NPH_MAIDENNAME", txtNPH_MAIDENNAME.Text.Trim() == "" ? null : txtNPH_MAIDENNAME.Text);
                    columnNameValue.Add("NPH_DOCISSUEDAT", txtNPH_DOCISSUEDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCISSUEDAT.Text));
                    columnNameValue.Add("NPH_DOCEXPIRDAT", txtNPH_DOCEXPIRDAT.Text.Trim() == "" ? null : (object)DateTime.Parse(txtNPH_DOCEXPIRDAT.Text));
                    columnNameValue.Add("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text.Trim()==""?null:txtNPH_FULLNAMEARABIC.Text);
					columnNameValue.Add("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text.Trim()==""?null:(object)DateTime.Parse(txtNPH_BIRTHDATE.Text));
					columnNameValue.Add("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue.Trim()==""?null:ddlCOP_OCCUPATICD.SelectedValue);
					columnNameValue.Add("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue.Trim()==""?null:ddlCCL_CATEGORYCD.SelectedValue);
					columnNameValue.Add("NPH_INSUREDTYPE",null);
					columnNameValue.Add("NPH_CODE",txtNPH_CODE.Text.Trim()==""?null:txtNPH_CODE.Text);
					columnNameValue.Add("NPH_LIFE",txtNPH_LIFE.Text.Trim()==""?null:txtNPH_LIFE.Text);

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
							result = "You are not Autherized to Execute Process.";
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
				/*rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='S'");
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
					ShowData(dataHolder["LNPH_PHOLDER"].Rows[0]);
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
				SessionObject.Set("NPH_TITLE",ddlNPH_TITLE.SelectedValue);
				SessionObject.Set("NPH_IDTYPE",ddlNPH_IDTYPE.SelectedValue);
				SessionObject.Set("NPH_SEX",ddlNPH_SEX.SelectedValue);
				SessionObject.Set("NPH_FULLNAME",txtNPH_FULLNAME.Text);
				SessionObject.Set("NPH_FULLNAMEARABIC",txtNPH_FULLNAMEARABIC.Text);
				SessionObject.Set("NPH_BIRTHDATE",txtNPH_BIRTHDATE.Text);
				SessionObject.Set("COP_OCCUPATICD",ddlCOP_OCCUPATICD.SelectedValue);
				SessionObject.Set("CCL_CATEGORYCD",ddlCCL_CATEGORYCD.SelectedValue);
				//SessionObject.Set("NPH_INSUREDTYPE",ddlNPH_INSUREDTYPE.SelectedValue);
				SessionObject.Set("NPH_CODE_s",txtNPH_CODE.Text);
				SessionObject.Set("NPH_LIFE_s",txtNPH_LIFE.Text);
				SessionObject.Set("NU1_SMOKER",ddlNU1_SMOKER.SelectedValue);
				//SessionObject.Set("NU1_ACCOUNTNO",txtNU1_ACCOUNTNO.Text);
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
			txtNPH_FULLNAME.Text="";

			txtNPH_FULLNAMEARABIC.Text="";
			txtNPH_BIRTHDATE.Text="";
			ddlCOP_OCCUPATICD.ClearSelection();
			ddlCCL_CATEGORYCD.ClearSelection();
			//ddlNPH_INSUREDTYPE.ClearSelection();
			txtNPH_CODE.Enabled = true;
			txtNPH_CODE.Text="";
			txtNPH_LIFE.Enabled = true;
			txtNPH_LIFE.Text="";
			//txtNU1_ACCOUNTNO.Text="";
			txtCNIC_VALUE.Text="";
			txtNPH_IDNO2.Text="";


			ddlNPH_HEIGHTTYPE.SelectedValue="";
			txtNU1_ACTUALHEIGHT.Text="";
			txtNU1_CONVERTHEIGHT.Text="";
			
			
	
			ddlNPH_WEIGHTTTYPE.SelectedValue="";
			txtNU1_ACTUALWEIGHT.Text="";
			txtNU1_CONVERTWEIGHT.Text="";
			
			

		}		

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			ddlNPH_TITLE.ClearSelection();
			ListItem item0=ddlNPH_TITLE.Items.FindByValue(objRow["NPH_TITLE"].ToString());
			if (item0!= null)
			{
				item0.Selected=true;
			}
			
			ddlNPH_IDTYPE.ClearSelection();
			ListItem item11=ddlNPH_IDTYPE.Items.FindByValue(objRow["NPH_IDTYPE"].ToString());
			if (item11!= null)
			{
				item11.Selected=true;
			}			

			ddlNPH_SEX.ClearSelection();
			ListItem item1=ddlNPH_SEX.Items.FindByValue(objRow["NPH_SEX"].ToString());
			if (item1!= null)
			{
				item1.Selected=true;
			}txtNPH_FULLNAME.Text=objRow["NPH_FULLNAME"].ToString();

			txtCNIC_VALUE.Text = objRow["NPH_IDNO"].ToString();
			txtNPH_IDNO2.Text  = objRow["NPH_IDNO2"].ToString();

			string NIC = txtCNIC_VALUE.Text;
			string concat=null;
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
			
			//txtCNIC_VALUE.Text=objRow["NPH_IDNO"].ToString();

//New Values
			ddlNPH_HEIGHTTYPE.SelectedValue=objRow["NPH_HEIGHTUOM"].ToString();
			txtNU1_ACTUALHEIGHT.Text=objRow["NPH_HEIGHTACTUAL"].ToString();
			txtNP2_AGEPREM.Text= Convert.ToString(ace.Ace_General.getEntryAge(Convert.ToString(SessionObject.Get("NP1_PROPOSAL"))));
			txtNU1_CONVERTHEIGHT.Text=objRow["NPH_HEIGHT"].ToString();

            txtNPH_DOCISSUEDAT.Text = objRow["NPH_DOCISSUEDAT"] == DBNull.Value ? "" : ((DateTime)objRow["NPH_DOCISSUEDAT"]).ToShortDateString();
            txtNPH_DOCEXPIRDAT.Text = objRow["NPH_DOCEXPIRDAT"] == DBNull.Value ? "" : ((DateTime)objRow["NPH_DOCEXPIRDAT"]).ToShortDateString();
            txtNPH_FATHERNAME.Text = objRow["NPH_FATHERNAME"].ToString();
            txtNPH_MAIDENNAME.Text = objRow["NPH_MAIDENNAME"].ToString();

            ddlNPH_WEIGHTTTYPE.SelectedValue=objRow["NPH_WEIGHTUOM"].ToString();
			if(objRow["NPH_WEIGHTACTUAL"] != System.DBNull.Value )
				txtNU1_ACTUALWEIGHT.Text= System.Math.Round(Convert.ToDouble(objRow["NPH_WEIGHTACTUAL"]),2).ToString();
			txtNU1_CONVERTWEIGHT.Text=objRow["NPH_WEIGHT"].ToString();


			/***************************************************************
			 Controlling of Height in case of Feet having Values 
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
			rowset rsArabic = DB.executeQuery("SELECT NPH_FULLNAMEARABIC FROM LNPH_PHOLDER WHERE NPH_CODE="+objRow["NPH_CODE"]);
			string FULLNAMEARABIC=null;
			if(rsArabic.next())
				FULLNAMEARABIC =  System.Text.Encoding.UTF8.GetString(Convert.FromBase64String((rsArabic.getObject("NPH_FULLNAMEARABIC")==null?"":rsArabic.getString("NPH_FULLNAMEARABIC"))));

			txtNPH_FULLNAMEARABIC.Text=Convert.ToString(FULLNAMEARABIC);


			//CUSTOM: ARABIC

			//txtNPH_FULLNAMEARABIC.Text=objRow["NPH_FULLNAMEARABIC"].ToString();
			
			
			
			
			txtNPH_BIRTHDATE.Text=objRow["NPH_BIRTHDATE"]==DBNull.Value?"":((DateTime)objRow["NPH_BIRTHDATE"]).ToShortDateString();
			ddlCOP_OCCUPATICD.ClearSelection();
			ListItem item5=ddlCOP_OCCUPATICD.Items.FindByValue(objRow["COP_OCCUPATICD"].ToString());
			if (item5!= null)
			{
				item5.Selected=true;
			}ddlCCL_CATEGORYCD.ClearSelection();
			ListItem item6=ddlCCL_CATEGORYCD.Items.FindByValue(objRow["CCL_CATEGORYCD"].ToString());
			if (item6!= null)
			{
				item6.Selected=true;
			}
			/*ddlNPH_INSUREDTYPE.ClearSelection();
			ListItem item7=ddlNPH_INSUREDTYPE.Items.FindByValue(objRow["NPH_INSUREDTYPE"].ToString());
			if (item7!= null)
			{
				item7.Selected=true;
			}*/
			
			//Manual Code
			ddlNU1_SMOKER.ClearSelection();
			ListItem item8=ddlNU1_SMOKER.Items.FindByValue(Session["NU1_SMOKER"].ToString());
			if (item8!= null)
			{
				item8.Selected=true;
			}

			//txtNU1_ACCOUNTNO.Text=Session["NU1_ACCOUNTNO"]==null ? "" :  Session["NU1_ACCOUNTNO"].ToString();
			//Manual Code

			txtNPH_CODE.Text=objRow["NPH_CODE"].ToString();
			txtNPH_CODE.Enabled=false;
			txtNPH_LIFE.Text=objRow["NPH_LIFE"].ToString();
			txtNPH_LIFE.Enabled=false;


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
				allFields.add(strKey, columnNameValue.get(strKey));
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
				
				string pk1 = pk;
				if (pk.Equals("NPH_CODE"))
					pk1 += "_s";
				if (pk.Equals("NPH_LIFE"))
					pk1 += "_s";

				string strPK = SessionObject.GetString(pk1);
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
				NPH_CODE=SessionObject.GetString("NPH_CODE_s");
				NPH_LIFE=SessionObject.GetString("NPH_LIFE_s");
	

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
			FooterScript.Text = "";
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
			//this is to make all event for update to edit so delete and update buttons are enabled
			if (lastEvent.Equals("Update"))
				_lastEvent.Text = "Edit";

		}


		private void ClearSession()
		{
			SessionObject.Remove("NPH_TITLE");
			SessionObject.Remove("NPH_IDTYPE");
			SessionObject.Remove("NPH_SEX");
			SessionObject.Remove("NPH_FULLNAME");
			SessionObject.Remove("NPH_FULLNAMEARABIC");
			SessionObject.Remove("NPH_BIRTHDATE");
			SessionObject.Remove("COP_OCCUPATICD");
			SessionObject.Remove("CCL_CATEGORYCD");
			//SessionObject.Remove("NPH_INSUREDTYPE");
			SessionObject.Remove("NPH_CODE_s");
			SessionObject.Remove("NPH_LIFE_s");
			SessionObject.Remove("NU1_SMOKER");
			SessionObject.Remove("NU1_ACCOUNTNO");

			SessionObject.Remove("NPH_WEIGHTUOM");
			SessionObject.Remove("NPH_WEIGHTACTUAL");
			SessionObject.Remove("NPH_WEIGHT");
			
			SessionObject.Remove("NPH_HEIGHTUOM");
			SessionObject.Remove("NPH_HEIGHTACTUAL");
			SessionObject.Remove("NPH_HEIGHT");
			
			SessionObject.Remove("NU1_OVERUNDERWT");
	

		}

		private void ViewInitialState()
		{
			//rowset rsPPR_PRODCD = DB.executeQuery("SELECT NPH_CODE, NPH_LIFE, NP1_PROPOSAL FROM LNU1_UNDERWRITI WHERE NP1_PROPOSAL = '"+SessionObject.Get("NP1_PROPOSAL")+"' AND NPH_LIFE = 'D'");

			if ((""+Session["flg_SELECETD"]).Equals("Y"))
			{
				SessionObject.Set("NPH_CODE",Session["NPH_CODE_s"]);
				SessionObject.Set("NPH_LIFE",Session["NPH_LIFE_s"]);
				SessionObject.Set("NU1_SMOKER",(Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"]));
				SessionObject.Set("NU1_ACCOUNTNO",Session["NU1_ACCOUNTNO"]);
				NPH_CODE = ""+Session["NPH_CODE_s"];
				NPH_LIFE = ""+Session["NPH_LIFE_s"];
				NU1_SMOKER = ""+ (Session["NU1_SMOKER"]==null ? "N":Session["NU1_SMOKER"].ToString());
				NU1_ACCOUNTNO = ""+Session["NU1_ACCOUNTNO"];
			}
			else
			{
				rowset rsLNPH_PHOLDERDB = DB.executeQuery("select NPH_CODE, NPH_LIFE, NU1_SMOKER, NU1_ACCOUNTNO from lnu1_underwriti where np1_proposal='"+SessionObject.Get("NP1_PROPOSAL")+"' and nu1_life='S'");

				if (rsLNPH_PHOLDERDB.next())
				{
					//NPH_CODE = rsLNPH_PHOLDERDB.getString(1);
					//NPH_LIFE = rsLNPH_PHOLDERDB.getString(2);
					SessionObject.Set("NPH_CODE_s",rsLNPH_PHOLDERDB.getString(1));
					SessionObject.Set("NPH_LIFE_s",rsLNPH_PHOLDERDB.getString(2));
					SessionObject.Set("NU1_SMOKER",rsLNPH_PHOLDERDB.getString(3));
					SessionObject.Set("NU1_ACCOUNTNO",rsLNPH_PHOLDERDB.getString(4));
				}
				else// avoid binding screen to the record of previous proposal selected in the screen
				{
					SessionObject.Remove("NPH_CODE_s");
					SessionObject.Remove("NPH_LIFE_s");
				}
			}

			SessionObject.Remove("flg_SELECETD");

		}

        private double convertToMeters(string unit,double actualvalue)
        {
            if (unit == "I")
            {
                return actualvalue *0.0254;
            }
            else if (unit == "F")
            {
                return actualvalue * 0.3048;
            }
            else if (unit == "C")
            {
                return actualvalue * 0.01;
            }
            else
            {
                return actualvalue;
            }
        }
        private double convertToKiloGram(string unit, double actualvalue)
        {
            if (unit == "L")
            {
                return actualvalue / 2.2;
            }
            else
            {
                return actualvalue;
            }
        }

    }
}

