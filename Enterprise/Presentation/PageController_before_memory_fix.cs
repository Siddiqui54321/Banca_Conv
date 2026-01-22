using System;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Configuration;
using System.IO;
using SHMA.Enterprise;
using System.Threading;
using SHMA.Enterprise.Data;	
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Exceptions;
using SHMA.Enterprise.Presentation.ErrorMessageFilter;
namespace SHMA.Enterprise.Presentation{
	public class PageController : System.Web.UI.Page{
		protected bool _SaveTransaction = true;	
		protected DataHolder dataHolder;
		protected object[] ControlArgs;
		protected virtual double pageVersion{
			get{
				return  0.4;
			}			
		}

		public PageController(){	
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
				SaveViewState (viewState );	
			else 
				base.SavePageStateToPersistenceMedium (viewState );
		}
		
		protected override object LoadPageStateFromPersistenceMedium() 
		{
			//If server side enabled use it, otherwise use original base class implementation
			
			if(ServerSideViewStateEnabled )
				return LoadViewState ();
			else
				return base.LoadPageStateFromPersistenceMedium ();
		}
		
		
		private void SaveViewState(object viewState)
		{
			string VSKEY=null;
			VSKEY = "VIEWSTATE_" + Session.SessionID + "_" + Request.RawUrl + "_" + System.DateTime .Now .Ticks .ToString ();
			if(CheckKeys ("ViewStateStore"))
			{
				if (StoreStateInCache )
				{
					Cache.Insert (VSKEY, viewState, null,System.DateTime.Now.AddMinutes (Session.Timeout),TimeSpan.Zero ,CacheItemPriority.Default,new CacheItemRemovedCallback (onRemoveCallBack) );
					RegisterHiddenField("__VIEWSTATE_KEY", VSKEY);
				}
			}
					
			else
				throw new Exception("ViewStateStore Key in Web.Config not Found");
		}


		private object LoadViewState()
		{
			string VSKEY=null;
			VSKEY = Request.Form.Get  ("__VIEWSTATE_KEY");
				
			if( VSKEY.StartsWith("VIEWSTATE_"))
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
			else
			{
				throw new Exception("Invalid VIEWSTATE Key: " +VSKEY);
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
			try{
				setCachePolicy();
				DoControl();
			}
			catch(SHMA.Enterprise.Exceptions.HandledException ex){
				Console.WriteLine(ex.Message);
				Response.End();
			}
			catch (Exception ex){				
				if (TransactionRequired)
					DB.Transaction.Rollback();
				ErrorHandle(ex);
	
			}
		}

		protected void DoControl(){
			try{
				ValidateRequest();
				dataHolder = InitDataHolder();
				dataHolder = GetData(dataHolder);
				ValidateData(dataHolder);
				ApplyDomainLogic(dataHolder);
				Save(dataHolder);				
				DataBind(dataHolder);
				PrepareUI(dataHolder);

			}
			catch (HandledException ex){
				Console.WriteLine(ex.Message);
				Response.End();
			}
			catch(Exception ex){
				ErrorHandle(ex);
			}
			finally {
				DB.DisConnect();
			}
		}
		protected virtual System.String[] MandatoryParams {
			get {
			 return null;}
		}
		
		protected virtual void ValidateRequest() {
			//checking missing params from mandatoryParams property, if misssing then throw exception , 
		}

		protected virtual DataHolder GetData(DataHolder dataHolder) {
		  return dataHolder; }
		
		protected virtual void ValidateData(DataHolder dataHolder) {
		}

		protected virtual void ApplyDomainLogic(DataHolder dataHolder){}

		protected virtual void Save(DataHolder dataHolder){			
			try{
				if (SaveTransaction){
					if (TransactionRequired){
						try{
							dataHolder.Update(DB.Transaction);
							DB.Transaction.Commit();
						}
						catch (Exception ex){
							DB.Transaction.Rollback();
							//						Response.Write("Exception Occured Salman123.");
							DB.TransactionEnd();
							throw ex;
							//Response.End();
						}
					}
					else{ 
						dataHolder.Update();
					}
				}
			}
			catch (ApplicationException e){
				//Response.Write(e.Message);
				if (TransactionRequired)
					DB.Transaction.Rollback();
				throw e;
			}	

				//lines added by faisal siddiqui.
			finally{
				if (TransactionRequired){
					DB.TransactionEnd();
					DB.DisConnect();
				}
			}
		}
		
		protected bool SaveTransaction{
			get {
				return _SaveTransaction;
			}
			set {
				_SaveTransaction = value;
			}	
		}

		protected virtual bool TransactionRequired{
			get {
			 return true;}
		}

		protected virtual HttpCacheability Cacheability {
			get {return HttpCacheability.NoCache;}
		}
		protected virtual void DataBind(DataHolder dataHolder){}
        
		protected virtual void PrepareUI(DataHolder dataHolder) {
		}
		protected virtual void PageRedirect(){
			Response.Redirect("abc.aspx",true);
		}

		public override void Dispose(){
			//DB.DisConnect();
		}

		protected DataHolder InitDataHolder(){
			object _DataHolder = null;
			if (TransactionShared){
				_DataHolder = SessionObject.Get("_DataHolder");
				if (_DataHolder==null){
					_DataHolder = new DataHolder();
					SessionObject.Set("_DataHolder", _DataHolder );
				}
			}
			else{
				_DataHolder = new DataHolder();
			}

			return (DataHolder)_DataHolder;
		}
		protected virtual bool  TransactionShared {
			get{
				return false;
			}
		}
		protected virtual string ErrorHandle(string message){
			if (DB.isConnected()) {
				try{
					if (TransactionRequired){
						DB.RollbackTransaction();
						DB.TransactionEnd();
					}
					DB.DisConnect();
				}
				catch(Exception e){
					message += e.Message;
					//	ErrorMessageFilter.ErrorMessageFilter.Parse(e);
					System.Console.Error.WriteLine(e.StackTrace);
				}

			}
			//Response.Write(message);
			return message;
		}
		protected virtual void ErrorMessagePrint(string message){
			System.Web.UI.WebControls.Literal errMsgTxt;
			if(this.FindControl("MessageScript")!=null){
				errMsgTxt= (System.Web.UI.WebControls.Literal)this.FindControl("MessageScript");
				if(message!=null)
					errMsgTxt.Text = string.Format("ErrorMessage('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
			}
		}
		protected virtual string ErrorHandle(System.Exception ex){
			string message = null;
			if (DB.isConnected()) {
				try{
					if (TransactionRequired) {
						DB.RollbackTransaction();
						DB.TransactionEnd();
					}
					DB.DisConnect();
				}
				catch(Exception e) {
					message += e.Message;
					ErrorMessageFilter.ErrorMessageFilter.Parse(e);
					System.Console.Error.WriteLine(e.StackTrace);
				}
			}
			message = ErrorMessageFilter.ErrorMessageFilter.Parse(ex);
			if(DB.isConnected())
				DB.DisConnect();
			
			//Response.Write(message);
			if (pageVersion<=0.4){
				ErrorMessagePrint(message);
			}
			else
				ShowMessage(message);
			return message;
		}	
	protected virtual void ShowMessage(string message){}	
	}
	
	public enum EnumControlArgs {Add, Edit, Update, Cancel, Save, Delete, Pager, Filter, View, None, Process};
}