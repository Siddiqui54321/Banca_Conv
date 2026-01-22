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
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text ;
using SHMA.Enterprise.Data;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;
using System.Data.OracleClient;

namespace SHAB.Presentation
{
	/// <summary>
	/// Medical Detail (Questioner).
	/// </summary>
	/// 
	public partial class MedicalDetail : System.Web.UI.Page
	{

		protected System.Web.UI.WebControls.Literal _result;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
        protected System.Web.UI.WebControls.Literal MessageScript;
		//string str_connString;
		private OleDbConnection oledbConn;
		
		private bool SaveEvent = false;
		private bool eventAttchedWithRadio = false;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Button3.Attributes.Add("onclick","test();");

		//	this.oledbConn = (OleDbConnection)DB.Connection;
			CSSLiteral.Text = ace.Ace_General.loadMainStyle();
			
			string save = (((HtmlInputHidden)Form1.FindControl("Save"))).Value;
			if(save == "Y")
			{
				this.SaveEvent = true;
				(((HtmlInputHidden)Form1.FindControl("Save"))).Value = "";
			}
			
			//Button3.Visible=false;
			 
			/*
			Response.Write(System.Web.HttpContext.Current.Request.FilePath);
			Response.Write(System.Web.HttpContext.Current.Request.CurrentExecutionFilePath);
			Response.Write(System.Web.HttpContext.Current.Request.RawUrl);
			Response.Write(System.Web.HttpContext.Current.Request.Url);*/

			//Registration raise event from javascript
			//Page.GetPostBackEventReference(Button3); 
			
			//String str_Query = "execute sp_dept '10'" ;//TextBox1.Text ;

			string proposal= "";
			string product = "";

			try
			{
				//proposal = (Session["NP1_PROPOSAL"]==null ? "" : Session["NP1_PROPOSAL"].ToString());
				//product  = (Session["PPR_PRODCD"]==null ? "" : Session["PPR_PRODCD"].ToString());
				proposal = Session["NP1_PROPOSAL"].ToString();
				product  = Session["PPR_PRODCD"].ToString();

				if(product=="")
				{
					_result.Text = "alert('Plan is not defined.');";
				}

			}
			catch(Exception ex)
			{
				_result.Text = "alert('Plan is not defined.');";
				return;
			}


			String str_Query = " Select * from LnQN_QUESTIONNAIRE";
			//20-Dec-2010 str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			if (Page.IsPostBack == false)
			{
				bool check_value=Check_Data();
				
				if(check_value==true)
				{
					GetSavedData();
				}
				else
				{
					str_Query="select A.CQN_CODE,A.CQN_DESC,A.CQN_CONDITION FROM lcqn_questionnaire A "+
					" INNER JOIN lpqn_questionnaire B "+
					" ON B.CQN_CODE=A.CQN_CODE "+
					" AND B.PPR_PRODCD='"+product+"' ORDER BY A.CQN_CODE";
					fillGrid(str_Query );
				}
			}
		}

		
		protected void Page_UnLoad(object sender, System.EventArgs e)
		{
			try
			{
				if (this.oledbConn != null)
				{
					//if (this.oledbConn.State == ConnectionState.Open)
					{
						this.oledbConn.Close();
						this.oledbConn.Dispose();
					}
				}
			}
			catch(Exception ex)
			{
			
			}
		}

		public bool Check_Data()
		{
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			bool val=false;
			try
			{
				//Put user code to initialize the page here
				//20-Dec-2010 conn.Open ();
				string Query="select * from lnqn_questionnaire where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"'";
				DataSet ds = new DataSet ();
				OleDbDataAdapter dr = new OleDbDataAdapter (Query,this.oledbConn);
				dr.Fill (ds,"DD");
				if(ds.Tables[0].Rows.Count>0)
				{
					return  true;
				}
				else
				{
					return  false;
				}
			}
			catch(Exception e)
			{
				//_result.Text = "alert('" + e.Message + "');";
				return  false;
			}
			finally
			{
				//20-Dec-2010 conn.Close();
			}
		
		}
		private void fillGrid(string Query)
		{
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			try
			{
				// Put user code to initialize the page here
				//20-Dec-2010 conn.Open ();
				DataSet ds = new DataSet();
				OleDbDataAdapter dr = new OleDbDataAdapter(Query,this.oledbConn);
				dr.Fill (ds,"DD");
				int[] Mark_Rows = new int[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					string Ques_Code=ds.Tables[0].Rows[i]["CQN_CODE"].ToString();
					string Ques_Condition=ds.Tables[0].Rows[i]["CQN_CONDITION"].ToString();
					
					if(Ques_Condition!="") // && Ques_Condition!="H")
					{
						string Check_Conditions=Check_Condition(Ques_Code.ToString(),Ques_Condition.ToString());
						if(Check_Conditions=="N")
						{
							if(i==0)
							{
								Mark_Rows[i]=-100;
							}
							else
							{
								Mark_Rows[i]=i;
							}
							//ds.AcceptChanges();
						}
					}
					/*else if(Ques_Condition!="H")
					{
						RadioButton r2=(RadioButton)dg.Items[i].Cells[2].FindControl("RadioButton2");
						RadioButton r1=(RadioButton)dg.Items[i].Cells[2].FindControl("RadioButton1");
						r1.Visible=false;

					}*/
				}
				for(int j=0;j<Mark_Rows.Length;j++)
				{
					if(Mark_Rows[j]>0 || Mark_Rows[j]==-100)
					{
						if(Mark_Rows[j]==-100)
						{
							ds.Tables[0].Rows[0].Delete();
						}
						else
							ds.Tables[0].Rows[j].Delete();
					}
				}
				ds.AcceptChanges();
				if(ds.Tables[0].Rows.Count>0)
				{
					dg.DataSource  = ds;
					dg.DataBind();
				}
			}
			catch(Exception e)
			{
				_result.Text = "alert('" + e.Message + "');";
			}
			finally
			{
				//20-Dec-2010 conn.Close();
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
			this.dg.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dg_OnItemDataBound);
			this.Unload += new System.EventHandler(this.Page_UnLoad);

		}
		#endregion

		/*private void dg_ItemCommand(object sender, DataGridCommandEventArgs e)
		{
			int selectedProductID = (int) dg.DataKeys[e.Item.ItemIndex]; 
			if(e.CommandName=="RemoveButton")
			{
				//Response.Write("Hello"); 
			}
		}

		private void dg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}*/
	
		//Insert Querry Temporary After Radio Button Checked Yes
		public void Add_Temporary(string proposal,int setNo,string productID,string CQN_CODE,string Answer)
		{
			try
			{
				SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
				pm.puts("@NP1_PROPOSAL", proposal);
				pm.puts("@NP2_SETNO", setNo);
				pm.puts("@PPR_PRODCD",productID);
				pm.puts("@CQN_CODE",CQN_CODE);
				pm.puts("NQN_ANSWER",Answer);
				
				DB.executeDML("delete from lnqd_questiondetail where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"' AND CQN_CODE='"+CQN_CODE+"'");
				DB.executeDML("delete from LNQN_QUESTIONNAIRE where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"' AND CQN_CODE='"+CQN_CODE+"'");			
				DB.executeDML("Insert into LNQN_QUESTIONNAIRE (NP1_PROPOSAL, NP2_SETNO,PPR_PRODCD, CQN_CODE,NQN_ANSWER) values (?,?,?,?,?)", pm);				
			}
			catch(Exception ex)
			{
				//Response.Write(ex.Message);
			}
			finally
			{
				//conn.Close();
			}
		}
		

		protected void OnChangeHandler(object sender, System.EventArgs e)
		{ 
			//20-Dec-2010 str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			try
			{
				CheckBox ck1 = (CheckBox)sender;
				DataGridItem dgItem = (DataGridItem)ck1.NamingContainer;
				DataGrid Dg_SubGrids;
				string Answer;
				string CQN_CODE=dgItem.Cells[0].Text;
				RadioButton rdo_ans=(RadioButton)dg.Items[dgItem.ItemIndex].Cells[2].FindControl("RadioButton2");
				
				if(rdo_ans.Checked==true)
				{
					Answer="Y";
				}
				else
				{
					Answer="N";
				}

				string product = Session["PPR_PRODCD"].ToString();
				
				string Query="SELECT B.cqn_code,B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION FROM LCCN_COLUMN A "+
					" INNER JOIN LCQD_QUESTIONDETAIL B "+
					" ON B.CCN_COLUMNID=A.CCN_COLUMNID "+
					" INNER JOIN lcqn_questionnaire C ON "+
					" C.CQN_CODE=B.CQN_CODE "+
					" INNER JOIN lpqn_questionnaire D "+
					" ON D.CQN_CODE=B.CQN_CODE "+
					" INNER JOIN LPPR_PRODUCT E "+
					" ON E.PPR_PRODCD=D.PPR_PRODCD "+
					" WHERE B.CQN_CODE='"+CQN_CODE+"' "+
					" AND D.PPR_PRODCD='"+product+"' ";
				// Put user code to initialize the page here
			
				//20-Dec-2010 conn.Open ();
				DataSet ds = new DataSet ();
				OleDbDataAdapter dr = new OleDbDataAdapter(Query, this.oledbConn);
				dr.Fill (ds,"Data");
				Dg_SubGrids=(DataGrid)dg.Items[dgItem.ItemIndex].Cells[1].FindControl("Dg_SubGrid");
				
				//Temporary Insertion
				Add_Temporary(Session["NP1_PROPOSAL"].ToString(),1,Session["PPR_PRODCD"].ToString(),CQN_CODE.ToString(),Answer.ToString());

				//checking Conditions
				int[] Mark_Rows = new int[ds.Tables[0].Rows.Count];
				if(ds.Tables[0].Rows.Count>0)
				{
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						string condition=Check_Condition(ds.Tables[0].Rows[i]["CQN_CODE"].ToString(),ds.Tables[0].Rows[i]["CQD_CONDITION"].ToString());
						if(condition=="N")
						{
							if(i==0)
							{
								Mark_Rows[i]=-100;
							}
							else
								Mark_Rows[i]=i;
							//Remove Rows
							//ds.Tables[0].Rows[i].Delete();
							//ds.AcceptChanges();
						}
					}
					for(int j=0;j<Mark_Rows.Length;j++)
					{
						if(Mark_Rows[j]>0 || Mark_Rows[j]==-100) 
						{
							if(Mark_Rows[j]==-100)
							{
								ds.Tables[0].Rows[0].Delete();
							}
							else
								ds.Tables[0].Rows[j].Delete();
							
						}
					}
					ds.AcceptChanges();
				}
				//Response.Write(condition.ToString());
				
				if(ds.Tables[0].Rows.Count>0)
				{
					Dg_SubGrids.DataKeyField="CCN_COLUMNID";
					Dg_SubGrids.DataSource=ds;
					Dg_SubGrids.DataBind();
					//New Code..
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						string CCN_COLUMNID=ds.Tables[0].Rows[i]["CCN_COLUMNID"].ToString();
						string CQN_CODEs=ds.Tables[0].Rows[i]["CQN_CODE"].ToString();
													
						SHMA.Enterprise.Data.ParameterCollection pmr = new SHMA.Enterprise.Data.ParameterCollection();
						pmr.puts("@NP1_PROPOSAL",Session["NP1_PROPOSAL"]);
						pmr.puts("@NP2_SETNO",1);
						pmr.puts("@PPR_PRODCD",Session["PPR_PRODCD"]);
						pmr.puts("@CQN_CODE",CQN_CODEs);
						pmr.puts("@CCN_COLUMNID",CCN_COLUMNID);
						pmr.puts("@NQD_ANSWER","");
						//DB.executeDML("delete from LNQD_QUESTIONDETAIL where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"' AND CQN_CODE='"+CQN_CODE+"'");					
						DB.executeDML("Insert into LNQD_QUESTIONDETAIL(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CQN_CODE,CCN_COLUMNID,NQD_ANSWER) values (?,?,?,?,?,?)", pmr);
					}
				}
				//Response.Write("<script type='text/javascript'>");
				//Response.Write("var ppp=parent;for (i=0;i<100;i++){if (ppp.parent==null) break;ppp=ppp.parent;}ppp.setPage('shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL');");
				//Response.Write("</script>");
			}
			catch(Exception eg)
			{
				//Response.Write( eg.Message );
			}
			finally
			{
				//20-Dec-2010 conn.Close();
			} 		
		}

//Get Saved Data ..
		public void GetSavedData()
		{
			//20-Dec-2010 str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			try
			{
				// Put user code to initialize the page here
				//20-Dec-2010 conn.Open ();
				string Query="SELECT D.CQN_DESC,D.CQN_CONDITION,D.CQN_CODE,A.NQN_REMARKS,A.NQN_ANSWER,A.NP1_PROPOSAL FROM lnqn_questionnaire A "+
					" RIGHT OUTER JOIN lcqn_questionnaire D "+
					" ON D.CQN_CODE=A.CQN_CODE "+
					" AND NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"' INNER JOIN lPqn_questionnaire P ON D.CQN_CODE = P.CQN_CODE "+ 
					" AND P.PPR_PRODCD='"+ Session["PPR_PRODCD"].ToString() +"' ORDER BY D.CQN_CODE";   

				DataSet ds = new DataSet ();
				OleDbDataAdapter dr = new OleDbDataAdapter (Query,this.oledbConn);
				dr.Fill (ds,"DATAGRID");

				//Check Condition Again.
				int[] Mark_Rows = new int[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					string cqn_code=ds.Tables[0].Rows[i]["CQN_CODE"].ToString();
					string str=Check_Condition(cqn_code.ToString(),ds.Tables[0].Rows[i]["CQN_CONDITION"].ToString());
					if(str=="N")
					{
						//Mark Rows And Then Delete
						if(i==0)
						{
							Mark_Rows[i]=-100;
						}
						else
							Mark_Rows[i]=i;
					}
					cqn_code="";
				} 
				
				for(int j=0;j<Mark_Rows.Length;j++)
				{
					if(Mark_Rows[j]>0 || Mark_Rows[j]==-100)
					{
						if(Mark_Rows[j]==-100)
						{
							ds.Tables[0].Rows[0].Delete();
						}
						else
							ds.Tables[0].Rows[j].Delete();
					}
				}
				ds.AcceptChanges();
				dg.DataSource  = ds;
				dg.DataBind();

				//Set Data
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					if(ds.Tables[0].Rows[i]["NQN_ANSWER"].ToString()=="N")
					{
						RadioButton rdo_chk_false=(RadioButton) dg.Items[i].FindControl("RadioButton1");
						rdo_chk_false.Checked=true;
					}
					else
					{
						string CQN_CODE=ds.Tables[0].Rows[i]["CQN_CODE"].ToString();
						if(ds.Tables[0].Rows[i]["NQN_ANSWER"].ToString()=="Y")
						{
							RadioButton rdo_chk_true=(RadioButton)dg.Items[i].FindControl("RadioButton2");
							rdo_chk_true.Checked=true;
						}
						else
						{
							dg.Items[i].BackColor=System.Drawing.Color.LightYellow;
						}

						//Check Child Records.
						DataGrid Dg_SubGrid=(DataGrid)dg.Items[i].FindControl("Dg_SubGrid");
						//						string Querys=" SELECT DISTINCT A.NQN_ANSWER,C.CCN_COLUMNID,A.CQN_CODE,A.NP1_PROPOSAL,A.NQN_ANSWER,B.NQD_ANSWER,C.CCN_DESCRIPTION,D.CQN_DESC FROM lnqn_questionnaire A "+
						//							" INNER JOIN LNQD_QUESTIONDETAIL B ON "+
						//							" B.CQN_CODE=A.CQN_CODE "+
						//							" INNER JOIN LCCN_COLUMN C "+
						//							" ON C.CCN_COLUMNID=B.CCN_COLUMNID "+
						//							" INNER JOIN lcqn_questionnaire D "+
						//							" ON D.CQN_CODE=A.CQN_CODE "+
						//							" AND A.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"' AND A.CQN_CODE='"+CQN_CODE+"' ";
						//

						string Querys="SELECT B.NQN_ANSWER, A.nqd_answer,C.CCN_COLUMNID,B.CQN_CODE,B.NP1_PROPOSAL,B.NQN_ANSWER, "+
							" B.NQN_ANSWER, C.CCN_DESCRIPTION,D.CQN_DESC  "+
							" FROM lnqd_questiondetail A "+
							" INNER JOIN lnqn_questionnaire B "+
							" ON B.CQN_CODE=A.CQN_CODE "+
							" INNER JOIN lcqn_questionnaire D "+
							" ON D.CQN_CODE=A.CQN_CODE "+
							" INNER JOIN LCCN_COLUMN C "+
							" ON C.CCN_COLUMNID=A.CCN_COLUMNID "+
							" WHERE A.CQN_CODE='"+CQN_CODE+"' AND A.NP1_PROPOSAL=B.NP1_PROPOSAL "+
							" AND A.NP1_PROPOSAL='"+Session["NP1_PROPOSAL"]+"' ORDER BY B.CQN_CODE";

						DataSet dss = new DataSet ();
						OleDbDataAdapter drr = new OleDbDataAdapter(Querys,this.oledbConn);
						drr.Fill (dss,"DATAGRIDS");
                        
						if(dss.Tables[0].Rows.Count>0)
						{
							Dg_SubGrid.DataKeyField="CCN_COLUMNID";
							Dg_SubGrid.DataSource=dss;
							Dg_SubGrid.DataBind();	
						}

						for(int k=0;k<dss.Tables[0].Rows.Count;k++)
						{
							TextBox TXT_SUBANS=(TextBox)Dg_SubGrid.Items[k].FindControl("TXT_SUBANS");
							TXT_SUBANS.Text=dss.Tables[0].Rows[k]["NQD_ANSWER"].ToString();
						}
					}
				
					TextBox Txt_Remarks =(TextBox)dg.Items[i].FindControl("NQN_REMARKS");
					Txt_Remarks.Text=ds.Tables[0].Rows[i]["NQN_REMARKS"].ToString();
				}

			}
			catch(Exception e)
			{
				Response.Write("<script type='text/javascript'>");
				Response.Write("alert('"+ e.Message.Replace("'","").Replace("\n"," ").Replace("\r"," ").Replace("\t"," ").Replace(Environment.NewLine," ") +"');");
				Response.Write("</script>");
			}
			finally
			{
				//20-Dec-2010 conn.Close();
			}
		
		}


//Calling Procedure for Insertion.
        
		public string Check_Condition(string P_QUESTION,string P_CONDITION)
		{
			string mRtrnString="";
			if(P_CONDITION=="")
			{
				mRtrnString = "EMPTY_CONDITION";
			}
			else
			{
				ProcedureAdapter call =  new ProcedureAdapter("CHECK_LCQNCONDITION_CALL", this.oledbConn);
				call.Set("P_PROPOSAL", OleDbType.VarChar, Convert.ToString(Session["NP1_PROPOSAL"]));
				call.Set("P_PRODCD",   OleDbType.VarChar, Convert.ToString(Session["PPR_PRODCD"]));
				call.Set("P_SETNO",    OleDbType.Numeric, 1);
				call.Set("P_QUESTION", OleDbType.VarChar, P_QUESTION);
				call.Set("P_CONDITION",OleDbType.VarChar, P_CONDITION);
				call.RegisetrOutParameter("MRTRNSTRING",OleDbType.VarChar,1000);
				call.Execute();

				mRtrnString = Convert.ToString(call.Get("MRTRNSTRING"));
			}
			return mRtrnString; 
 		}


		protected void OnCheck(object sender, System.EventArgs e)
		{
			if(!SaveEvent)
			{
				CheckBox ck1 = (CheckBox)sender;
				DataGridItem dgItem = (DataGridItem)ck1.NamingContainer;			
				string Answer;
				string CQN_CODE=dgItem.Cells[0].Text;
				RadioButton rdo_ans=(RadioButton)dg.Items[dgItem.ItemIndex].Cells[2].FindControl("RadioButton2");
			
				if(rdo_ans.Checked==true)
				{
					Answer="Y";
				}
				else
				{
					Answer="N";
				}
				Add_Temporary(Session["NP1_PROPOSAL"].ToString(),1,Session["PPR_PRODCD"].ToString(),CQN_CODE.ToString(),Answer.ToString());
				GetSavedData();
			}
			//Response.Write("<script type='text/javascript'>");
			//Response.Write("var ppp=parent;for (i=0;i<100;i++){if (ppp.parent==null) break;ppp=ppp.parent;}ppp.setPage('shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL');");
			//Response.Write("</script>");
	    }
//Saving Data/Update Data

		public void ApplicationDomainLogic()
		{
			//20-Dec-2010 str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			try
			{
				//Delete Old Records.
				DB.executeDML("delete from lnqd_questiondetail where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'");
				DB.executeDML("delete from LNQN_QUESTIONNAIRE where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'");
				
				//Getting Data from Parent DataGrid				
				for(int i=0;i<dg.Items.Count;i++)
				{
					string CQN_CODE=dg.DataKeys[i].ToString();
					string Answer="";
					string Remarks="";
					TextBox TxtRemarks=(TextBox)dg.Items[i].FindControl("NQN_REMARKS");
                    Remarks=TxtRemarks.Text;
					
					RadioButton rdo_ans =(RadioButton)dg.Items[i].Cells[2].FindControl("RadioButton2");
			
					if(rdo_ans.Checked==true)
					{
						Answer="Y";
					}
					else
					{
						Answer="N";
					}

					SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
					pm.puts("@NP1_PROPOSAL",Session["NP1_PROPOSAL"]);
					pm.puts("@NP2_SETNO",1);
					pm.puts("@PPR_PRODCD",Session["PPR_PRODCD"]);
					pm.puts("@CQN_CODE",CQN_CODE);
					pm.puts("NQN_ANSWER",Answer);
					pm.puts("NQN_REMARKS",Remarks);
					DB.executeDML("Insert into LNQN_QUESTIONNAIRE (NP1_PROPOSAL, NP2_SETNO,PPR_PRODCD, CQN_CODE,NQN_ANSWER,NQN_REMARKS) values (?,?,?,?,?,?)", pm);
			
					//Getting Data From Child DataGrid
					DataGrid Dg_SubGrids=(DataGrid)dg.Items[i].Cells[1].FindControl("Dg_SubGrid");
				    
					if(Dg_SubGrids.Items.Count>0)
					{
						for(int j=0;j<Dg_SubGrids.Items.Count;j++)
						{
							TextBox TxtNQD_ANSWER=(TextBox) Dg_SubGrids.Items[j].FindControl("TXT_SUBANS");

							string CCN_COLUMNID=Dg_SubGrids.DataKeys[j].ToString();
							string NQD_ANSWER=TxtNQD_ANSWER.Text;
							
							SHMA.Enterprise.Data.ParameterCollection pmr = new SHMA.Enterprise.Data.ParameterCollection();
							pmr.puts("@NP1_PROPOSAL",Session["NP1_PROPOSAL"]);
							pmr.puts("@NP2_SETNO",1);
							pmr.puts("@PPR_PRODCD",Session["PPR_PRODCD"]);
							pmr.puts("@CQN_CODE",CQN_CODE);
							pmr.puts("@CCN_COLUMNID",CCN_COLUMNID);
							pmr.puts("@NQD_ANSWER",NQD_ANSWER);
							DB.executeDML("Insert into LNQD_QUESTIONDETAIL(NP1_PROPOSAL,NP2_SETNO,PPR_PRODCD,CQN_CODE,CCN_COLUMNID,NQD_ANSWER) values (?,?,?,?,?,?)", pmr);
						}
					}
				}
			}
			catch(Exception eg)
			{
				Response.Write( eg.Message );
			}
			finally
			{
				//20-Dec-2010 conn.Close();
			}
		
		}

		private void dg_OnItemDataBound(Object sender,DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string product  = Session["PPR_PRODCD"].ToString();
				string cqnCode = e.Item.Cells[0].Text;

				/*string query = "SELECT B.cqn_code, C.CQN_DESC, B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION " + 
					"FROM LCCN_COLUMN A   " + 
					"     INNER JOIN LCQD_QUESTIONDETAIL B ON B.CCN_COLUMNID=A.CCN_COLUMNID " + 
					"     INNER JOIN LCQN_QUESTIONNAIRE  C ON C.CQN_CODE=B.CQN_CODE  " + 
					"     INNER JOIN LPQN_QUESTIONNAIRE  D ON D.CQN_CODE=B.CQN_CODE  " + 
					"     INNER JOIN LPPR_PRODUCT        E ON E.PPR_PRODCD=D.PPR_PRODCD  " + 
					"WHERE B.CQN_CODE='" + cqnCode + "'  AND D.PPR_PRODCD='" + product + "'" ;

				rowset rs = DB.executeQuery(query);
				if(rs.next())*/

				TextBox txtSubQuestion = (TextBox) e.Item.FindControl("subQuestion"); 
				txtSubQuestion.Text = "N";
				this.eventAttchedWithRadio = false;
				if(subQuestionPresent(product, cqnCode) == true)
				{
					RadioButton rbYes = (RadioButton) e.Item.FindControl("RadioButton2"); 
					RadioButton rbNo  = (RadioButton) e.Item.FindControl("RadioButton1"); 
					rbYes.AutoPostBack = true;
					rbNo.AutoPostBack  = true;
					txtSubQuestion.Text = "Y";
					this.eventAttchedWithRadio = true;
				}

			}
		}

		/*private void dg_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if(!SaveEvent)
				{
					string product  = Session["PPR_PRODCD"].ToString();

					ListItemType elemType = e.Item.ItemType;
					if ((elemType == ListItemType.Item)||(elemType == ListItemType.AlternatingItem)) 
					{ 
						string cqnCode = e.Item.Cells[0].Text;
						//RadioButton rd1 = (RadioButton)sender;
						//DataGridItem dgItem = (DataGridItem)rd1.NamingContainer;			
						//string cqnCode=dgItem.Cells[0].Text;
						TextBox txtSubQuestion = (TextBox) e.Item.FindControl("subQuestion"); 

						//if(subQuestionPresent(product, cqnCode) == true)
						//if(txtSubQuestion.Text == "Y")
						if(this.eventAttchedWithRadio)
						{
							// *** Event Handler for Checkbox *** 
							RadioButton cBox = (RadioButton) e.Item.FindControl("RadioButton2");
							cBox.CheckedChanged += new EventHandler(OnChangeHandler);
						}
					} 
					if ((elemType == ListItemType.Item)||(elemType == ListItemType.AlternatingItem)) 
					{ 
						string cqnCode = e.Item.Cells[0].Text;
						//RadioButton rd1 = (RadioButton)sender;
						//DataGridItem dgItem = (DataGridItem)rd1.NamingContainer;			
						//string cqnCode=dgItem.Cells[0].Text;
						TextBox txtSubQuestion = (TextBox) e.Item.FindControl("subQuestion"); 

						//if(subQuestionPresent(product, cqnCode) == true)
						//if(txtSubQuestion.Text == "Y")
						if(this.eventAttchedWithRadio)
						{
							// *** Event Handler for Checkbox *** 
							RadioButton cBox = (RadioButton) e.Item.FindControl("RadioButton1"); 
							cBox.CheckedChanged += new EventHandler(OnCheck);
						}
					}
				}
			}
		}*/

		private bool subQuestionPresent(string product, string question)
		{
			string query = "SELECT B.cqn_code, C.CQN_DESC, B.CQD_CONDITION,A.CCN_COLUMNID, A.CCN_DESCRIPTION " + 
				"FROM LCCN_COLUMN A   " + 
				"     INNER JOIN LCQD_QUESTIONDETAIL B ON B.CCN_COLUMNID=A.CCN_COLUMNID " + 
				"     INNER JOIN LCQN_QUESTIONNAIRE  C ON C.CQN_CODE=B.CQN_CODE  " + 
				"     INNER JOIN LPQN_QUESTIONNAIRE  D ON D.CQN_CODE=B.CQN_CODE  " + 
				"     INNER JOIN LPPR_PRODUCT        E ON E.PPR_PRODCD=D.PPR_PRODCD  " + 
				"WHERE B.CQN_CODE='" + question + "'  AND D.PPR_PRODCD='" + product + "'" ;

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
			 
		public void Scenarios()
		{
			//20-Dec-2010 str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
			//20-Dec-2010 OleDbConnection conn = new OleDbConnection(str_connString);
			string scenario1="NNNNN";
			string scenario2="NNYYYNN";
			string concat=null;

			for(int i=0;i<dg.Items.Count;i++)
			{
				CheckBox chk_box1=(CheckBox)dg.Items[i].FindControl("RadioButton1");
				CheckBox chk_box2=(CheckBox)dg.Items[i].FindControl("RadioButton2");
	
				if(chk_box1.Checked==true)
				{
					concat+="N";
				}
				
				if(chk_box2.Checked==true)
				{
					concat+="Y";
				}
			}

			try
			{
				string Update=null;
				//20-Dec-2010 conn.Open ();
				string Query="select * from Lnph_Pholder where NPH_CODE='"+Session["NPH_CODE"]+"'";
				DataSet ds = new DataSet ();
				OleDbDataAdapter dr = new OleDbDataAdapter(Query, this.oledbConn);
				dr.Fill (ds,"DD");
				double BMI = (Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_WEIGHT"]))/(Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_HEIGHT"]) * Convert.ToDouble(ds.Tables[0].Rows[0]["NPH_HEIGHT"]));

				Query="select NP2_AGEPREM from lnp2_policymastr WHERE NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'";
				DataSet dss = new DataSet ();
				OleDbDataAdapter drr = new OleDbDataAdapter(Query, this.oledbConn);
				drr.Fill (dss,"DD");
				double Age = Convert.ToDouble(dss.Tables[0].Rows[0]["NP2_AGEPREM"].ToString());
        
				//Checking Condition
				if(concat.Equals(scenario1) &&(((Convert.ToDouble(BMI)>Convert.ToDouble(32)) && (Age>=0 ||Age<=34)) ||
					((Convert.ToDouble(BMI)>Convert.ToDouble(36)) && (Age>=35))))
				{
					//Medical Criteria Rejected.
					DB.executeDML("UPDATE lnp2_policymastr set np2_substandar='N' where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'");
					Update="Y";
				}
				else if(concat.Equals(scenario2) &&(((Convert.ToDouble(BMI)<Convert.ToDouble(32)) && (Age>=0 ||Age<=34)) ||
					((Convert.ToDouble(BMI)<=Convert.ToDouble(36)) && (Age>=35))))
				{
					//Medical Criteria Rejected.
					DB.executeDML("UPDATE lnp2_policymastr set np2_substandar='N' where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'");
					Update="Y";
				}

				else if(Update==null)
				{
					//Medical Criteria Updated.
					DB.executeDML("UPDATE lnp2_policymastr set np2_substandar='Y' where NP1_PROPOSAL='"+Session["NP1_PROPOSAL"].ToString()+"'");		
				}
				Update=null;
			}
			catch(Exception eg)
			{
				//Response.Write( eg.Message );
			}
			finally
			{
				//20-Dec-2010 conn.Close();
			}
		}

		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}

		protected void Button3_Click_1(object sender, System.EventArgs e)
		{
			int values=0;
			for(int i=0;i<dg.Items.Count;i++)
			{
				RadioButton r1=(RadioButton)dg.Items[i].Cells[2].FindControl("RadioButton2");
				RadioButton r2=(RadioButton)dg.Items[i].Cells[2].FindControl("RadioButton1");
				if(r1.Checked==false && r2.Checked==false)
				{
					values=values+1;
				}
			}

			if(values>0)
			{
				Response.Write("<Script>alert('Please provide all the answers of question')</Script>");
			}
			else
			{
				ApplicationDomainLogic();
				//Scenarios();
				GetSavedData();
				
				//Response.Write("<script type='text/javascript'>");
				//Response.Write("var ppp=parent;for (i=0;i<100;i++){if (ppp.parent==null) break;ppp=ppp.parent;}ppp.setPage('shgn_gp_gp_ILUS_ET_GP_MEDICALDETAIL');");
				//Response.Write("</script>");

				//************* Activity Log *************//			
				Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.MEDICAL_QUESTIONS_UPDATED);
			}
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Scenarios();
		}

	}
}
