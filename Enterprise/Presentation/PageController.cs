using System;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Configuration;
using System.IO;
using System.Data;
using System.Collections;
using SHMA.Enterprise;
using System.Threading;
using SHMA.Enterprise.Data;	
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Presentation.ErrorMessageFilter;

namespace SHMA.Enterprise.Presentation
{
	public class PageController : System.Web.UI.Page
	{
		protected bool _SaveTransaction = true;	
		protected DataHolder dataHolder;
		protected object[] ControlArgs;
		public virtual double pageVersion
		{
			get
			{
				return  0.4;
			}			
		}

        /// <summary>
        /// True if the last PostBack was caused by a Page refresh.
        /// </summary>
        public bool IsRefresh
        {
            get;
            set;
        }

		public PageController()
		{	
		}
		#region Viewstate Management
		public bool ServerSideViewStateEnabled
		{
			get
			{
				if(CheckKeys ("ServerSideViewState"))
				{
					return Convert.ToBoolean(ConfigurationSettings.AppSettings["ServerSideViewState"]);
				}
				else
					return false;
			}
		}
		
		public bool StoreStateInCache
		{
			get
			{
				if(ConfigurationSettings.AppSettings["ViewStateStore"].ToUpper ()=="CACHE")
				{
					return true;
				}
				else
					return false;
			}
		}


		public bool StoreStateInSession
		{
			get
			{
				if(ConfigurationSettings.AppSettings["ViewStateStore"].ToUpper ()=="SESSION")
				{
					return true;
				}
				else
					return false;
			}
		}
		
		public bool CheckKeys(string key)
		{
			string [] Keys=ConfigurationSettings .AppSettings .AllKeys ;
			bool flag=false;	
			for(int i=0;i<Keys.Length ;i++)
			{
				if(Keys[i].ToString ().Equals (key))
				{
					flag= true;
					break;
				}
			}
			return flag;
		}
		
		protected bool KeyTrue(string key)
		{
			if(CheckKeys(key) && (ConfigurationSettings.AppSettings["ViewStateStore"].ToUpper ()=="TRUE"))
			{
				return true;
			}
			else
				return false;

		}
		
		public void onRemoveCallBack(string str, object obj, CacheItemRemovedReason reason)
		{   
				
			string SID=null;
			
			string rsn=reason.ToString ();
			if(!rsn.Equals ("Removed")) 
			{
				for(int i=10;i<str.Length ;i++)
				{
					if(str[i].ToString().Equals ("_"))
						break;
					else
						SID=SID+str[i].ToString ();
				}
					
				LosFormatter los = new LosFormatter();
				StringWriter writer = new StringWriter();
				los.Serialize(writer, obj);
				
				string VsData=writer.ToString ();
				System.Data.IDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandText ="INSERT INTO VSLOG (PVD_ID,PVD_SSNID,PVD_VSDATA,PVD_RSN,PVD_DURATION) VALUES('"+str+"','"+SID+"','"+VsData+"','"+rsn+"','"+DateTime.Now +"')";
				cmd.Connection = DB.Connection;
				int r=cmd.ExecuteNonQuery ();
			}
		}
			
		protected override void SavePageStateToPersistenceMedium(object viewState) 
		{
			//If server side enabled use it, otherwise use original base class implementation
		
			if(ServerSideViewStateEnabled )
			{
				object s = viewState;
				if (KeyTrue("SerializeViewState"))
				{
					//System.Web.HttpContext.Current.Response.AppendToLog("Serialize enabled, before serialize: " + s);
					s= ConvertVS2Str(ViewState);
					//System.Web.HttpContext.Current.Response.AppendToLog("                    after serialize: " + s);
				}
				SaveViewState (s);	

			}
			else 
				base.SavePageStateToPersistenceMedium (viewState );
		}
		
		protected override object LoadPageStateFromPersistenceMedium() 
		{
			//If server side enabled use it, otherwise use original base class implementation
			
			if(ServerSideViewStateEnabled )
			{
				object s = LoadViewState ();
				if (KeyTrue("SerializeViewState"))
				{
					//System.Web.HttpContext.Current.Response.AppendToLog("Serialize enabled, before deserialize: " + s);
				
					s = ConvertStr2VS ((string)s);
					//System.Web.HttpContext.Current.Response.AppendToLog("                    after deserialize: " + s);
				}
				return s;

			}
			else
				return base.LoadPageStateFromPersistenceMedium ();
		}
		
		
		private void SaveViewState(object viewState)
		{
			string VSKEY=null;
			
			if(CheckKeys ("ViewStateStore"))
			{
				if (StoreStateInCache )
				{
					VSKEY = "VIEWSTATE_" + Session.SessionID + "_" + Request.RawUrl + "_" + System.DateTime .Now .Ticks .ToString ();
					Cache.Insert (VSKEY, viewState, null,System.DateTime.Now.AddMinutes (Session.Timeout),TimeSpan.Zero ,CacheItemPriority.Default,new CacheItemRemovedCallback (onRemoveCallBack) );
					RegisterHiddenField("__VIEWSTATE_KEY", VSKEY);
				}
				else if (StoreStateInSession)
				{
					VSKEY = "VIEWSTATE_" + Request.RawUrl;
					SessionObject.Set(VSKEY, viewState);
					RegisterHiddenField("__VIEWSTATE_KEY", VSKEY);
					
				}

			}
					
			else
				throw new Exception("ViewStateStore Key in Web.Config not Found");
		}


		private object LoadViewState()
		{
		     try
		     {
			string VSKEY=null;
			VSKEY = Request.Form.Get  ("__VIEWSTATE_KEY");
			
			
			if( VSKEY.StartsWith("VIEWSTATE_"))
			{
				
				//IF View State stored in Cache
				if (StoreStateInCache)
				{
					if(!Cache[VSKEY].Equals (null))
					{
						return  Cache.Remove(VSKEY);	 
					}
					else if (CheckKeys ("SaveToDb")&&ConfigurationSettings.AppSettings["SaveToDb"].ToUpper ()=="TRUE")
					{
						return null;
						//Some Code To Read From DB the View State Value
					}
					else
					{
						throw new Exception("ViewState Not Found in Cache & SaveToDb is False");
					}
				}
					//If View State stored in Session
				else if (StoreStateInSession)
				{
					if(SessionObject.ContainsKey(VSKEY))
					{
						object state = SessionObject.Get(VSKEY);
						SessionObject.Remove(VSKEY);
						return  state;	 
					}
					else
					{
						throw new Exception("ViewState Not Found in Session");
					}

				}
				else 
				{
					throw new Exception ("Unhandled case in Loading Server Side View State");
				}

			}
			else
			{
				throw new Exception("Invalid VIEWSTATE Key: " +VSKEY);
				return null;
			}
		    }
		    catch (Exception e)
		    {
			Response.Redirect("../Presentation/Login.aspx");
			return null;
		    }
		}
		
		public void IntelliCache()
		{
			foreach (string key in Request.QueryString.AllKeys)
			{
				if (key!=null && key.StartsWith("r_")) 
				
				{
					Response.Cache.SetCacheability(HttpCacheability.NoCache  );
				}
				
			}
		}
		protected virtual void setCachePolicy()
		{
			Response.Cache.SetCacheability(Cacheability);
			IntelliCache();
		}
		#endregion

		protected virtual void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                if (!IsPostBack)
                {
                    if (Session[this.ToString() + "REFRESH_CHECK_GUID"] == null)
                    {
                        Session[this.ToString() + "REFRESH_CHECK_GUID"] = System.Guid.NewGuid().ToString();
                    }
                }
				setCachePolicy();
				DoControl();
			}
			catch(SHMA.Enterprise.Exceptions.HandledException ex)
			{
				Console.WriteLine(ex.Message);
				Response.End();
			}
			catch (Exception ex)
			{				
				if (TransactionRequired)
					DB.Transaction.Rollback();
				ErrorHandle(ex);
	
			}
		}

		protected void DoControl()
		{
			try
			{
				ValidateRequest();
				dataHolder = InitDataHolder();
				dataHolder = GetData(dataHolder);
				ValidateData(dataHolder);
                //Add by Syed Kamran
                string VSKEY = null;
                VSKEY = Request.Form.Get("REFRESH_CHECK_GUID");
                if (VSKEY != null && ControlArgs!=null && ControlArgs[0].ToString()!="Pager")
                {
                    if (VSKEY.ToString() == Session[this.ToString() + "REFRESH_CHECK_GUID"].ToString())
                    {

                        ApplyDomainLogic(dataHolder);
                        Session[this.ToString() + "REFRESH_CHECK_GUID"] = System.Guid.NewGuid().ToString();
                        Save(dataHolder);
                        DataBind(dataHolder);
                        PrepareUI(dataHolder);
                    }
                    else
                    {
                        try
                        {
                            Save(dataHolder);
                            DataBind(dataHolder);
                            PrepareUI(dataHolder);
                        }
                        catch (Exception ex)
                        {
                            Shared.Log.CustomExceptionLogger.Log(ex);
                        }
                        throw new Exception("Page Refresh not allowed here");
                    }
                }
                else
                {
                    ApplyDomainLogic(dataHolder);
                    if (ControlArgs != null && ControlArgs[0].ToString() != "Pager")
                        Session[this.ToString() + "REFRESH_CHECK_GUID"] = System.Guid.NewGuid().ToString();
                    Save(dataHolder);
                    DataBind(dataHolder);
                    PrepareUI(dataHolder);

                }

			}
			catch (HandledException ex)
			{
				Console.WriteLine(ex.Message);
				Response.End();
			}
			catch(Exception ex)
			{
				ErrorHandle(ex);

               
			}
			finally 
			{
				DB.DisConnect();
			}
		}
        //Add by Syed Kamran
        protected override void OnPreRender(EventArgs e)
        {
            RegisterHiddenField("REFRESH_CHECK_GUID", Session[this.ToString() + "REFRESH_CHECK_GUID"].ToString());
        }

		protected virtual System.String[] MandatoryParams 
		{
			get 
			{
				return null;
            }
		}
		
		protected virtual void ValidateRequest() 
		{
			//checking missing params from mandatoryParams property, if misssing then throw exception , 
		}

		protected virtual DataHolder GetData(DataHolder dataHolder) 
		{
			return dataHolder; }
		
		protected virtual void ValidateData(DataHolder dataHolder) 
		{
		}

		protected virtual void ApplyDomainLogic(DataHolder dataHolder){}

		protected virtual void Save(DataHolder dataHolder)
		{			
			try
			{
				if (SaveTransaction)
				{
					if (TransactionRequired)
					{
						try
						{
							dataHolder.Update(DB.Transaction);
							DB.Transaction.Commit();
						}
						catch (Exception ex)
						{
							DB.Transaction.Rollback();
							//						Response.Write("Exception Occured Salman123.");
							DB.TransactionEnd();
							throw ex;
							//Response.End();
						}
					}
					else
					{ 
						dataHolder.Update();
					}
				}
			}
			catch (ApplicationException e)
			{
				//Response.Write(e.Message);
				if (TransactionRequired)
					DB.Transaction.Rollback();
				throw e;
			}	

				//lines added by faisal siddiqui.
			finally
			{
				if (TransactionRequired)
				{
					DB.TransactionEnd();
					DB.DisConnect();
				}
			}
		}
		
		protected bool SaveTransaction
		{
			get 
			{
				return _SaveTransaction;
			}
			set 
			{
				_SaveTransaction = value;
			}	
		}

		protected virtual bool TransactionRequired
		{
			get 
			{
				return true;}
		}

		protected virtual HttpCacheability Cacheability 
		{
			get {return HttpCacheability.NoCache;}
		}
		protected virtual void DataBind(DataHolder dataHolder){}
        
		protected virtual void PrepareUI(DataHolder dataHolder) 
		{
		}
		protected virtual void PageRedirect()
		{
			Response.Redirect("abc.aspx",true);
		}

		public override void Dispose()
		{
			//DB.DisConnect();
		}

		protected DataHolder InitDataHolder()
		{
			object _DataHolder = null;
			if (TransactionShared)
			{
				_DataHolder = SessionObject.Get("_DataHolder");
				if (_DataHolder==null)
				{
					_DataHolder = new DataHolder();
					SessionObject.Set("_DataHolder", _DataHolder );
				}
			}
			else
			{
				_DataHolder = new DataHolder();
			}

			return (DataHolder)_DataHolder;
		}
		protected virtual bool  TransactionShared 
		{
			get
			{
				return false;
			}
		}
		protected virtual string ErrorHandle(string message)
		{
			if (DB.isConnected()) 
			{
				try
				{
					if (TransactionRequired)
					{
						DB.RollbackTransaction();
						DB.TransactionEnd();
					}
					DB.DisConnect();
				}
				catch(Exception e)
				{
					message += e.Message;
					//	ErrorMessageFilter.ErrorMessageFilter.Parse(e);
					System.Console.Error.WriteLine(e.StackTrace);
				}

			}
			//Response.Write(message);
			return message;
		}
		protected virtual void ErrorMessagePrint(string message)
		{
			System.Web.UI.WebControls.Literal errMsgTxt;


            if (Master != null)
            {
                errMsgTxt = (System.Web.UI.WebControls.Literal)Master.FindControl("MessageScript");
                //errMsgTxt = (System.Web.UI.WebControls.Literal)Master.FindControl("myForm").FindControl("ContentPlaceHolder1").FindControl("MessageScript");
                if (message != null && errMsgTxt != null)
                    errMsgTxt.Text = string.Format("ErrorMessage('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
            }
            else if(this.FindControl("MessageScript")!=null)
			{
				errMsgTxt= (System.Web.UI.WebControls.Literal)this.FindControl("MessageScript");
				if(message!=null)
					errMsgTxt.Text = string.Format("ErrorMessage('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
			}
		}

        protected virtual void PrintMessage(string message)
        {
            //Master.FindControl  MessageScript.Text = string.Format("alert('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
            System.Web.UI.WebControls.Literal errMsgTxt;
            errMsgTxt = (System.Web.UI.WebControls.Literal)Master.FindControl("MessageScript");
            if (message != null && errMsgTxt != null)
                errMsgTxt.Text = string.Format("alert('{0}')", message.Replace("'", "").Replace("\n", "").Replace("\r", ""));
        }

		protected virtual string ErrorHandle(System.Exception ex)
		{
			string message = null;
			if (DB.isConnected()) 
			{
				try
				{
					if (TransactionRequired) 
					{
						DB.RollbackTransaction();
						DB.TransactionEnd();
					}
					DB.DisConnect();
				}
				catch(Exception e) 
				{
					message += e.Message;
					//ErrorMessageFilter.ErrorMessageFilter.Parse(e);
					System.Console.Error.WriteLine(e.StackTrace);
				}
			}
			//message = ErrorMessageFilter.ErrorMessageFilter.Parse(ex);
			if(DB.isConnected())
				DB.DisConnect();
			
			//Response.Write(message);
			if (pageVersion!=2.0) //Work for page with Master page
			{
				ErrorMessagePrint(message);
			}
			else
				ShowMessage(message);
			return message;
		}	
		protected virtual void ShowMessage(string message){}	

		private string ConvertVS2Str(object  obj)
		{
			System.Web.UI.LosFormatter output = new System.Web.UI.LosFormatter();
			StringWriter writer = new StringWriter();
			output.Serialize(writer, obj);
			return writer.ToString(); 

		} 

		private object ConvertStr2VS( string viewstate)
		{
			System.Web.UI.LosFormatter input = new System.Web.UI.LosFormatter();
			return input.Deserialize(viewstate);

		}

		#region R U N N I N G   T O T A L    ENQUIRY

		protected int calculateRunningTotal(DataTable reader, DataTable table, int pageSize,int page)
		{
			if (page < 1)			page = 1;
			if (pageSize < 0)		pageSize = 1;			
			int totalRecords = 0;
			
			string colName = getRuningTotalColumns();

			if(colName!=null && colName.Trim()!="")
			{
				int startIndex = ((page-1) * pageSize) + 1;
				int endIndex = startIndex+pageSize;
				Decimal prevTotal=0;
				Decimal lastTotal=0;
				
				System.Collections.ArrayList _al = copyTableColumns(reader, table);
				table.Columns.Add("RUNNING_TOTAL", Type.GetType("System.Double"));

				prevTotal = sumUpColumn(reader, startIndex, colName);

				System.Data.DataRow _row;
				for (int j=0; j<reader.Rows.Count; j++) 
				{
					totalRecords++;
					if (totalRecords >= startIndex && totalRecords < endIndex)
					{
						_row = table.NewRow();
						for ( int i = 0; i < _al.Count; i++)
						{
							_row[((System.String) _al[i])]=reader.Rows[j][((string) _al[i])];
						}

						if ( reader.Rows[j][colName] != DBNull.Value )
						{
							if(totalRecords == startIndex)
							{
								_row["RUNNING_TOTAL"]= prevTotal + Convert.ToDecimal(reader.Rows[j][colName]);
							}
							else
							{
								_row["RUNNING_TOTAL"]= lastTotal + Convert.ToDecimal(reader.Rows[j][colName]);
							}
						}
						lastTotal = Convert.ToDecimal(_row["RUNNING_TOTAL"]);

						table.Rows.Add(_row);
					}				
				}
			}
			return totalRecords;
		}

		protected int calculateRunningTotal(DataTable reader, DataTable table, int pageSize,int page,  NameValueCollection totalValues,  NameValueCollection grandTotalValues)
		{
			if (page < 1)			page = 1;
			if (pageSize < 0)		pageSize = 1;			
			int totalRecords = 0;
			
			string colName = getRuningTotalColumns();
			SortedList totalColumns = (SortedList)totalValues.Clone() ;
			SortedList grandTotalColumns = (SortedList)grandTotalValues.Clone() ;

			if(colName!=null && colName.Trim()!="")
			{
				int startIndex = ((page-1) * pageSize) + 1;
				int endIndex = startIndex+pageSize;
				Decimal prevTotal=0;
				Decimal lastTotal=0;
				
				System.Collections.ArrayList _al = copyTableColumns(reader, table);
				table.Columns.Add("RUNNING_TOTAL", Type.GetType("System.Double"));

				prevTotal = sumUpColumn(reader, startIndex, colName);

				System.Data.DataRow _row;
				for (int j=0; j<reader.Rows.Count; j++) 
				{
					totalRecords++;

					foreach (object key in grandTotalColumns.Keys)
					{
						if ( reader.Rows[j][((string)key)] != DBNull.Value )
							grandTotalValues[key] = ((Decimal)grandTotalValues[key]) + Convert.ToDecimal( reader.Rows[j][((string)key)].ToString() );							
					}

					if (totalRecords >= startIndex && totalRecords < endIndex)
					{
						_row = table.NewRow();
						for ( int i = 0; i < _al.Count; i++)
						{
							_row[((System.String) _al[i])]=reader.Rows[j][((string) _al[i])];
						}

						if ( reader.Rows[j][colName] != DBNull.Value )
						{
							if(totalRecords == startIndex)
							{
								_row["RUNNING_TOTAL"]= prevTotal + Convert.ToDecimal(reader.Rows[j][colName]);
							}
							else
							{
								_row["RUNNING_TOTAL"]= lastTotal + Convert.ToDecimal(reader.Rows[j][colName]);
							}
						}
						lastTotal = Convert.ToDecimal(_row["RUNNING_TOTAL"]);
						
						foreach (object key in totalColumns.Keys)
						{
							if ( reader.Rows[j][((string)key)] != DBNull.Value )
								totalValues[key] = ((Decimal)totalValues[key]) + Convert.ToDecimal(reader.Rows[j][((string)key)].ToString() );
						}						

						table.Rows.Add(_row);
					}				
				}
			}
			return totalRecords;
		}


		private System.Collections.ArrayList copyTableColumns(DataTable reader, DataTable table)
		{
			System.Data.DataColumn _dc;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < reader.Columns.Count; i ++) 
			{
				_dc = new System.Data.DataColumn();
				_dc.ColumnName = reader.Columns[i].ColumnName;
				_dc.DataType = reader.Columns[i].DataType;
				_dc.Unique = reader.Columns[i].Unique;
				_dc.AllowDBNull = true; 
				_dc.ReadOnly = reader.Columns[i].ReadOnly;
				_al.Add(_dc.ColumnName);
				table.Columns.Add(_dc);
			}
			return _al;
		}

		private Decimal sumUpColumn(DataTable reader, int endIndex, string colName)
		{
			Decimal Total=0;
			for (int j=0; j<endIndex-1; j++) 
			{
				if ( reader.Rows[j][colName] != DBNull.Value )
					Total = Total + Convert.ToDecimal(reader.Rows[j][colName]);
			}
			return Total;
		}


		protected virtual String getRuningTotalColumns()
		{
			return "";
		}


		#endregion

	}
	
	public enum EnumControlArgs {Add, Edit, Update, Cancel, Save, Delete, Pager, Filter, View, None, Process};

}