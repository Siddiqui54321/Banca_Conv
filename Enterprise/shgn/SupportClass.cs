using System;
using System.Web;

using System.Web.UI.WebControls;
[System.Web.UI.ParseChildren(ChildrenAsProperties = false)]
public abstract class WCBase : WebControl {
	public readonly static int EvalBodyInclude = 1;
	public readonly static int EvalPage = 6;
	public readonly static int SkipBody = 0;
	public readonly static int SkipPage = 5;
	public abstract int doStart();
	public abstract int doEnd();
	public virtual System.Web.HttpResponse GetOut() {
		return new System.Web.HttpResponse(Content.Writer);
	}
	public virtual WCBodyContent GetParentContent() {
		if (!(Parent is WCBase)){
			return new WCBodyContent(Context.Response.Output);
		}
		else {
			return ((WCBase)Parent).Content;
		}
	}
	private WCBodyContent content;	
	public  virtual WCBodyContent Content {
		set {
			this.content = value;
		}
		get {
			return this.content;
		}
	}

	public virtual void RenderWCControl() {
		this.Content = GetParentContent();
		int doStartResult = doStart();
		if (doStartResult != WCBase.SkipBody) {
			RenderWCChildControls();
		}
		int doEndResult = doEnd();

		if (doEndResult == WCBase.SkipPage) {
			Context.Response.End();
		}
	}

	public virtual void RenderWCChildControls() {
		foreach (System.Web.UI.Control currentControl in Controls) {
			if (currentControl is System.Web.UI.LiteralControl) {
				Content.Write(((System.Web.UI.LiteralControl)currentControl).Text);
			}
			if (currentControl is WCBase) {
				((WCBase) currentControl).RenderWCControl();
			}
			else {
				//UNKNOWN Control
			}
		}
	}

	protected override void Render(System.Web.UI.HtmlTextWriter output) {
		this.RenderWCControl();
	}
}

public abstract class WCIterationBase : WCBase {
	public readonly static int EvalBodyAgain = 2;
	public abstract int doAfterBody();
	public override void RenderWCControl() {
		this.Content = GetParentContent();

		int doStartResult = doStart();
			
		if (doStartResult != WCBase.SkipBody) {
			do {
				this.RenderWCChildControls();
			}
			while (doAfterBody() == WCIterationBase.EvalBodyAgain);
		}
		int doEndResult = doEnd();

		if (doEndResult == WCBase.SkipPage) {
			Context.Response.End();
		}
	}

	public static WCBase FindAncestorWithClass(WCBase from, System.Type type) {
		do {
			if ((from.Parent.GetType() == null) || !(from.Parent is WCBase)) {
				return null;
			}
			if (from.Parent.GetType() == type ) {
				return (WCBase)from.Parent;
			}
			else {
				from = (WCBase)from.Parent;
			}
		} while (true);
	}
}

	/*******************************/
/// <summary>
/// Implemente a Writer to manage the body of UserControls as in TagLibs.
/// </summary>
public class WCBodyContent {
	private System.IO.TextWriter writer;
	public WCBodyContent(System.IO.TextWriter writer) {
		this.writer = writer;
	}
		
	/// <summary>
	/// The underlaying Writer used.
	/// </summary>
	public System.IO.TextWriter Writer {
		set {
			writer = value;
		}
		get {
			return writer;
		}
	}

	/// <summary>
	/// Return the value of the body content as a StreamReader.
	/// </summary>
	public System.IO.StreamReader Reader {
		get {
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(new System.IO.MemoryStream());
			streamWriter.Write(this.GetString());
			return new System.IO.StreamReader(streamWriter.BaseStream);
		}
	}

	public virtual void Write(char p) {
		writer.Write(p);
	}

	public virtual void Write(System.Object p) {
		writer.Write(p);
	}

	public virtual void Write(System.String p) {
		writer.Write(p);
	}

	public virtual void Write(System.Boolean p) {
		writer.Write(p);
	}

	public virtual void Write(System.Char[] p) {
		writer.Write(p);
	}

	public virtual void Write(System.Decimal p) {
		writer.Write(p);
	}

	public virtual void Write(System.Double p) {
		writer.Write(p);
	}

	public virtual void Write(System.Int32 p) {
		writer.Write(p);
	}

	public virtual void Write(System.Int64 p) {
		writer.Write(p);
	}

	public virtual void Write(System.Single p) {
		writer.Write(p);
	}
	public void ClearContent() {
		try {
			this.writer=new System.IO.StringWriter();
		}
		catch (System.Exception e) {
		}
	}
	public System.String GetString() {
		return writer.ToString();
	}
	public void WriteContent(System.Web.HttpResponse writeTo) {
		writeTo.Write(writer.ToString());
	}
}

[System.ComponentModel.DefaultProperty("Text"), 
System.Web.UI.ToolboxData("<{0}:WCIterationImpl runat=server></{0}:WCIterationImpl>")]
public  class WCIterationImpl : WCIterationBase {
	private System.Collections.Hashtable values = new System.Collections.Hashtable();
	public override int doAfterBody() {
		return WCBase.SkipBody;
	}
	public override int doStart() {
		return WCBase.SkipBody;
	}
	public override int doEnd() {
		return WCBase.EvalPage;
	}
	public virtual System.Object  GetValue(System.String key) {
		return values[key];
	}
	public virtual void  SetValue(System.String key, System.Object o) {
		values[key]= o;
	}
	public virtual void  RemoveValue(System.String key) {
		values.Remove(key);
	}
	public virtual System.Collections.IEnumerator GetValues() {
		return values.GetEnumerator();
	}
}

public abstract class WCBodyBase : WCIterationBase {
	public WCBodyBase() {
	}

	/// <summary>
	/// Encapsulation of the evaluation of the control body in a TextWriter, it can be otained and modified.
	/// </summary>
	public abstract  WCBodyContent BodyContent {
		set;
		get;
	}
	public readonly static int  EvalBodyBuffered = 2;

	public readonly static int  EvalBodyTag = 2; // the same as EvalBodyAgain
		
	public abstract void doInitBody();

	/// <summary>
	/// Render the body of the control, including child controls.
	/// </summary>
	public override void RenderWCControl() {
		this.Content = GetParentContent();
		int doStartResult = doStart();
			
		if (doStartResult != WCBase.SkipBody) {
			if (doStartResult != WCBodyBase.EvalBodyInclude) {
				// creates the BodyContent System.Object and updates the Content, so child controls will write
				//properly to the body content
				this.BodyContent = new WCBodyContent(new System.IO.StringWriter());
				Content = BodyContent;
				this.doInitBody();
			}
				
			// check if the control has a body
			if( this.Controls.Count > 0) {
				do {
					this.RenderWCChildControls();
				}
				while (doAfterBody() == WCIterationBase.EvalBodyAgain);
			}

			// the previous content must be recovered
			if (doStartResult != WCBodyBase.EvalBodyInclude) {				
				Content = GetParentContent();
			}
		}

		int doEndResult = doEnd();
		if (doEndResult == WCBase.SkipPage) {
			Context.Response.End();
		}
	}

	/// <summary>
	/// Render the instance of Child Controls.
	/// </summary>
	public override void RenderWCChildControls() {
		foreach (System.Web.UI.Control currentControl in Controls) {
			if (currentControl is System.Web.UI.LiteralControl) {
				if (BodyContent!= null)
					BodyContent.Write(((System.Web.UI.LiteralControl)currentControl).Text);
				else 
					Content.Write(((System.Web.UI.LiteralControl)currentControl).Text);
			}
			else if (currentControl is WCBodyBase) {
				((WCBodyBase) currentControl).RenderWCControl();
			}
			else if (currentControl is WCIterationBase) {
				((WCIterationBase) currentControl).RenderWCControl();
			}
			else if (currentControl is WCBase) {
				((WCBase) currentControl).RenderWCControl();
			}
			else {
				//UNKNOWN Control
			}
		}
	}
}

	/*******************************/
/// <summary>
/// Basic implementation of  WCBodyBase for supporting Iteration tags.
/// </summary>
[System.ComponentModel.DefaultProperty("Text"),
System.Web.UI.ToolboxData("<{0}:WCCBodyImpl runat=server></{0}:WCBodyImpl>")]
public class WCBodyImpl : WCBodyBase {

	/// <summary>
	/// Encapsulation of the evaluation of the control body in a TextWriter, it can be otained and modified.
	/// </summary>
	protected WCBodyContent bodyContent;	
	public  override WCBodyContent BodyContent {
		set {
			this.bodyContent = value;
		}
		get {
			return this.bodyContent;
		}
	}
	
	/// <summary>
	/// Returns the previous writer
	/// </summary>
	public virtual System.Web.HttpResponse GetPreviousOut() {
		if (!(Parent is WCBase)){
			return new System.Web.HttpResponse(Context.Response.Output);		
		}
		if ((Parent is WCBodyBase) && (((WCBodyBase)Parent).BodyContent!= null)){
			return new System.Web.HttpResponse(((WCBodyBase)Parent).BodyContent.Writer);		
		}
		else if (Parent is WCBase) {
			return new System.Web.HttpResponse(((WCBase)Parent).Content.Writer);		
		}
		else {
			return new System.Web.HttpResponse(Content.Writer);		
		}
	}
	

	/// <summary>
	/// Default processing for doInitBody, it does Nothing.
	/// </summary>
	public override void doInitBody() {
	}
		
	/// <summary>
	/// Default return value SkipBody.
	/// </summary>
	public override int doAfterBody() {
		return WCBase.SkipBody;
	}

	/// <summary>
	/// Default return value EvalBodyBuffered.
	/// </summary>
	public override int doStart() {
		return WCBodyBase.EvalBodyBuffered;
	}

	/// <summary>
	/// Default return value EvalPage.
	/// </summary>
	public override int doEnd() {
		return WCBase.EvalPage;
	}
}
	/*******************************/
/// <summary>
/// This namespace includes Support Web User Control for supporting scriptlets (<% %>)
/// and expression (<%= %>) used inside the body of custom tags.
/// </summary>
namespace SupportWebUserControls {
	using System;
	using System.Data;
//	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	/// <summary>
	/// This control is used to replace expressions declared inside the body of custom tags.
	/// </summary>
	public class ExpressionSupportControl : WCBodyImpl { 
		private string method; 

		public string Method { 
			get { 
				return method; 
			} 
			set { 
				method = value; 
			} 
		} 

		private System.Object GetMethodValue() { 
			System.Reflection.MethodInfo tempMethodInfo = Page.GetType().GetMethod(Method);
			return tempMethodInfo.Invoke(Page, new object[] {}); 
		} 

		public override int doStart() { 
			System.Web.HttpResponse out_renamed = GetPreviousOut(); 
			out_renamed.Write(GetMethodValue()); 
			return SkipBody; 
		} 

		private void InitializeComponent() { 
			this.Load += new System.EventHandler(this.Page_Load); 
		} 

		private void Page_Load(object sender, System.EventArgs e) { 
		} 
	} 

	/// <summary>
	/// This control is used to replace scriptlets declared inside the body of custom tags.
	/// </summary>
	public class ScriptletSupportControl : WCIterationImpl { 
		private string method; 

		public string Method { 
			get { 
				return method; 
			} 

			set { 
				method = value; 
			} 
		} 

		private void ExecuteMethod() { 
			System.Reflection.MethodInfo tempMethodInfo = Page.GetType().GetMethod(Method); 
			tempMethodInfo.Invoke(Page, new object[] {}); 
		} 

		public override int doStart() { 
			ExecuteMethod(); 
			return SkipBody; 
		} 
	}
} 


/// <summary>
/// Contains conversion support elements such as classes, interfaces and static methods.
/// </summary>
public class SupportClass {
	/// <summary>
	/// Interface used by classes which must be single threaded.
	/// </summary>
	public interface SingleThreadModel {
	}


	/*******************************/
	/// <summary>
	/// Support Class for conversion of Web Applications.
	/// The primary intention of this class is to hide the logic used to call migrated methods.
	/// </summary>
	public class ServletSupport : System.Web.UI.Page {
		/// <summary>
		/// ServletSupport constructor.
		/// </summary>
		public ServletSupport() : base() {
		}

		/// <summary>
		/// Method to be registered with the PageLoad event.
		/// Used to call service method.  Also, this method verifies threading.
		/// If there is no migrated service methods, application calls doGet, doPost, etc
		/// migrated methods depending of the request received by the web application.
		/// If this method is overriden to add functionality, remember to call this method first.
		///
		/// Note:  Intrinsic objects Request and Response are being passed as parameter to mantain the code
		///        the same as the original, in methods doGet, doPost, doHead, doDelete and doPut.
		/// </summary>
		/// <param name="sender">Object calling the method.</param>
		/// <param name="e">Arguments passed by the event.</param>
		protected virtual void Page_Load(object sender, System.EventArgs e) {
			bool IsSingleThreadModel = false;
			Session["request_params"]  = new System.Collections.Hashtable();

			lock(this) {
				foreach(System.Type type in this.GetType().GetInterfaces())
					IsSingleThreadModel  = IsSingleThreadModel  || type.Equals (typeof(SingleThreadModel));
								
			}
			if (IsSingleThreadModel) {
								
				lock(this.GetType()) {
					service(Request,Response);
				}
			}
			else {
				service(Request,Response);
			}
			Session.Remove("request_params"); 
		}

		/// <summary>
		/// Method to override to process POST requests.
		/// </summary>
		/// <param name="request">Receives the intrinsic Request object.</param>
		/// <param name="response">Receives the intrinsic Response object.</param>
											
		protected virtual void doPost(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			throw new System.Web.HttpException("HTTP method doPost is not supported by this URL");
		}

		/// <summary>
		/// Method to override to process GET requests.
		/// </summary>
		/// <param name="request">Receives the intrinsic Request object.</param>
		/// <param name="response">Receives the intrinsic Response object.</param>
											
		protected virtual void doGet(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			throw new System.Web.HttpException("HTTP method doGet is not supported by this URL");
		}

		/// <summary>
		/// Method to override to process HEAD requests.
		/// If doHead method is not present in source code, by default HEAD requests
		/// are processed as GET requests.
		/// </summary>
		/// <param name="request">Receives the intrinsic Request object.</param>
		/// <param name="response">Receives the intrinsic Response object.</param>
											
		protected virtual void doHead(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			doGet(request,response);
		}

		/// <summary>
		/// Method to override to process DELETE requests.
		/// </summary>
		/// <param name="request">Receives the intrinsic Request object.</param>
		/// <param name="response">Receives the intrinsic Response object.</param>
		protected virtual void doDelete(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			throw new System.Web.HttpException("HTTP method doDelete is not supported by this URL");
		}

		/// <summary>
		/// Method to override to process PUT requests.
		/// </summary>
		/// <param name="request">Receives the intrinsic Request object.</param>
		/// <param name="response">Receives the intrinsic Response object.</param>
		protected virtual void doPut(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			throw new System.Web.HttpException("HTTP method doPut is not supported by this URL");
		}

		/// <summary>
		/// OnInit method used to check if the page has been initialized first time.
		/// and filters calling of this method from other applications.
		/// </summary>
		/// <param name="e">EventArgs sent by the Page.Init event.</param>
		protected override void OnInit(System.EventArgs e) {
			lock(this) {
				Application.Lock();
				string InitExecuted = "";
				base.OnInit(e);
				if (Application["Init"] == null) Application["Init"]="";
				else InitExecuted = (System.String)Application["Init"];

				if (InitExecuted.IndexOf(this.GetType().FullName + ")") < 0) {
					InitExecuted += "(" + 
						System.Windows.Forms.Application.ProductVersion +  "," + 
						System.Windows.Forms.Application.ProductName + "," + 
						this.GetType().FullName + ")";
					Application["Init"] = InitExecuted;
					if (! this.GetType().GetMethod ("init").DeclaringType.Equals (typeof(ServletSupport)))
						init();
					else
						init_renamed();
				}
				else {
					if (InitExecuted.IndexOf("(" + 
						System.Windows.Forms.Application.ProductVersion +  "," + 
						System.Windows.Forms.Application.ProductName + "," +
						this.GetType().FullName + ")" ) > -1) {
					}
					else {
						// If another applications tries to instance this ASP.NET
						throw new System.Web.HttpException("Another application is trying to instance "+ this.GetType().FullName);
					}
				}
				Application.UnLock();
			}
		}

		/// <summary>
		/// Method to override when converting the servlet init(ServletConfig) method.
		/// If this method is not present in the source code, by default, the migrated init()
		/// method (or the dummy init_renamed will be called.
		/// </summary>
		public virtual void init() {
		}

		/// <summary>
		/// Dummy method to override when converting the servlet init() method.
		/// </summary>
		public virtual void init_renamed() {
		}

		/// <summary>
		/// Dummy method to override when converting the servlet  service() public method.
		/// </summary>
		/// <param name="request">Receives the Web request</param>
		/// <param name="response">Returns the Web response</param>
		public virtual void service(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			service_renamed(request,response);
		}
									
		/// <summary>
		/// Dummy method to override when converting the servlet service() protected method.
		/// If there is no protected service method in source code, this method will route the 
		/// request received to the appropriate method, depending on the type of request. 
		/// </summary>
		/// <param name="request">Receives the Web request</param>
		/// <param name="response">Returns the Web response</param>
		protected virtual void service_renamed(System.Web.HttpRequest request, System.Web.HttpResponse response) {
			if (request.HttpMethod.Equals("GET"))
				doGet(Request,Response);
			if (request.HttpMethod.Equals("POST"))
				doPost(Request,Response);
			if (request.HttpMethod.Equals("HEAD"))
				doHead(Request,Response);
			if (request.HttpMethod.Equals("DELETE"))
				doDelete(Request,Response);
			if (request.HttpMethod.Equals("PUT"))
				doPut(Request,Response);
		}
				
		/// <summary>
		/// Property to access the request attributes hash table.
		/// </summary>
		public System.Collections.Hashtable RequestParams {
			get { 
				return ((System.Collections.Hashtable)Session["request_params"]);
			}
		}
	}

	/*******************************/
	/// <summary>
	/// Writes the exception stack trace to the received stream
	/// </summary>
	/// <param name="throwable">Exception to obtain information from</param>
	/// <param name="stream">Output sream used to write to</param>
	public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream) {
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	/*******************************/
	/// <summary>
	/// Obtain the query string and forms variables.
	/// </summary>
	/// <param name="QueryUser">The collection of HTTP of query string variables.</param>
	/// <param name="FormUser">The collection of forms variables.</param>
	/// <returns>Returns an enumerator that contains the query string and forms variables.</returns>
	public static System.Collections.IEnumerator GetParameterNames(System.Collections.IEnumerator QueryUser, System.Collections.IEnumerator FormUser) {
		System.Collections.ArrayList ArrayVariables = new System.Collections.ArrayList();

		while (QueryUser.MoveNext())
			ArrayVariables.Add(QueryUser.Current);

		while (FormUser.MoveNext())
			ArrayVariables.Add(FormUser.Current);

		return ArrayVariables.GetEnumerator();
	}


	/*******************************/
	/// <summary>
	/// Creates an instance of a received Type.
	/// </summary>
	/// <param name="classType">The Type of the new class instance to return.</param>
	/// <returns>An Object containing the new instance.</returns>
	public static System.Object CreateNewInstance(System.Type classType) {
		System.Object instance = null;
		System.Type[] constructor = new System.Type[]{};
		System.Reflection.ConstructorInfo[] constructors = null;
       
		constructors = classType.GetConstructors();

		if (constructors.Length == 0)
			throw new System.UnauthorizedAccessException();
		else {
			for(int i = 0; i < constructors.Length; i++) {
				System.Reflection.ParameterInfo[] parameters = constructors[i].GetParameters();

				if (parameters.Length == 0) {
					instance = classType.GetConstructor(constructor).Invoke(new System.Object[]{});
					break;
				}
				else if (i == constructors.Length -1)     
					throw new System.MethodAccessException();
			}                       
		}
		return instance;
	}


	/*******************************/
	/// <summary>
	/// Adds a new key-and-value pair into the hash table
	/// </summary>
	/// <param name="collection">The collection to work with</param>
	/// <param name="key">Key used to obtain the value</param>
	/// <param name="newValue">Value asociated with the key</param>
	/// <returns>The old element associated with the key</returns>
	public static System.Object PutElement(System.Collections.IDictionary collection, System.Object key, System.Object newValue) {
		System.Object element = collection[key];
		collection[key] = newValue;
		return element;
	}

	/*******************************/
	public class TransactionManager {
		public static ConnectionHashTable manager = new ConnectionHashTable();

		public class ConnectionHashTable : System.Collections.Hashtable {
			public System.Data.OleDb.OleDbCommand CreateStatement(System.Data.OleDb.OleDbConnection connection) {
				System.Data.OleDb.OleDbCommand command = connection.CreateCommand();
				System.Data.OleDb.OleDbTransaction transaction;
				if (this[connection] != null) {
					ConnectionProperties Properties = ((ConnectionProperties)this[connection]);
					transaction = Properties.Transaction;
					command.Transaction = transaction;
				}
				else {
					ConnectionProperties TempProp = new ConnectionProperties();
					TempProp.AutoCommit = true;
					TempProp.TransactionLevel = 0;
					command.Transaction = TempProp.Transaction;
					Add(connection, TempProp);
				}
				return command;
			}

			public void Commit(System.Data.OleDb.OleDbConnection connection) {
				if (this[connection] != null && !((ConnectionProperties)this[connection]).AutoCommit) {
					ConnectionProperties Properties = ((ConnectionProperties)this[connection]);
					System.Data.OleDb.OleDbTransaction transaction = Properties.Transaction;
					transaction.Commit();
					if (Properties.TransactionLevel == 0)
						Properties.Transaction = connection.BeginTransaction();
					else
						Properties.Transaction = connection.BeginTransaction(Properties.TransactionLevel);
				}
			}

			public void RollBack(System.Data.OleDb.OleDbConnection connection) {
				if (this[connection] != null && !((ConnectionProperties)this[connection]).AutoCommit) {
					ConnectionProperties Properties = ((ConnectionProperties) this[connection]);
					System.Data.OleDb.OleDbTransaction transaction = Properties.Transaction;
					transaction.Rollback();
					if (Properties.TransactionLevel == 0)
						Properties.Transaction = connection.BeginTransaction();
					else
						Properties.Transaction = connection.BeginTransaction(Properties.TransactionLevel);					
				}
			}

			public void SetAutoCommit(System.Data.OleDb.OleDbConnection connection, bool boolean) {
				if (this[connection] != null) {
					ConnectionProperties Properties = ((ConnectionProperties)this[connection]);
					Properties.AutoCommit = boolean;
					if (!boolean) {						
						if (Properties.TransactionLevel == 0)
							Properties.Transaction = connection.BeginTransaction();
						else
							Properties.Transaction = connection.BeginTransaction(Properties.TransactionLevel);
					}
					else {
						System.Data.OleDb.OleDbTransaction transaction =  Properties.Transaction;
						if (transaction != null) {
							transaction.Commit();
						}
					}
				}
				else {
					ConnectionProperties TempProp = new ConnectionProperties();
					TempProp.AutoCommit = boolean;
					TempProp.TransactionLevel = 0;
					if (!boolean)
						TempProp.Transaction  = connection.BeginTransaction();
					Add(connection, TempProp);
				}
			}

			public System.Data.OleDb.OleDbCommand PrepareStatement(System.Data.OleDb.OleDbConnection connection, string sql,System.Data.OleDb.OleDbTransaction transaction) {
				System.Data.OleDb.OleDbCommand command = this.CreateStatement(connection);
				command.CommandText = sql;
				command.Transaction = transaction;
				command.CommandTimeout = 0;
				return command;
			}

			public System.Data.OleDb.OleDbCommand PrepareStatement(System.Data.OleDb.OleDbConnection connection, string sql) {
				System.Data.OleDb.OleDbCommand command = this.CreateStatement(connection);
				command.CommandText = sql;
				command.CommandTimeout = 0;
				return command;
			}

			public System.Data.OleDb.OleDbCommand PrepareCall(System.Data.OleDb.OleDbConnection connection, string sql) {
				System.Data.OleDb.OleDbCommand command = this.CreateStatement(connection);
				command.CommandText = sql;
				return command;
			}

			public void SetTransactionIsolation(System.Data.OleDb.OleDbConnection connection, int level) {
				ConnectionProperties Properties;
				if(level == (int)System.Data.IsolationLevel.ReadCommitted)
					SetAutoCommit(connection, false);
				else
					if(level == (int)System.Data.IsolationLevel.ReadUncommitted)
					SetAutoCommit(connection, false);
				else
					if(level == (int)System.Data.IsolationLevel.RepeatableRead)
					SetAutoCommit(connection, false);
				else
					if(level == (int)System.Data.IsolationLevel.Serializable)
					SetAutoCommit(connection, false);

				if (this[connection] != null) {
					Properties =((ConnectionProperties)this[connection]);					
					Properties.TransactionLevel = (System.Data.IsolationLevel)level;
				}
				else {	
					Properties = new ConnectionProperties();
					Properties.AutoCommit = true;    					    
					Properties.TransactionLevel = (System.Data.IsolationLevel)level;	
					Add(connection, Properties);
				}
			}
			public void Close(System.Data.OleDb.OleDbConnection Connection)
			{

				if ((this[Connection] != null)&&!(((ConnectionProperties)this[Connection]).AutoCommit))
				{					
					Commit(Connection);	
				}
				Connection.Close();
			}

			public int GetTransactionIsolation(System.Data.OleDb.OleDbConnection connection) 
			{
				if (this[connection] != null) {
					ConnectionProperties Properties = ((ConnectionProperties)this[connection]);
					if (Properties.TransactionLevel != 0)
						return (int)Properties.TransactionLevel;
					else
						return 2;
				}
				else
					return 2;
			}

			public bool GetAutoCommit(System.Data.OleDb.OleDbConnection connection) {
				if (this[connection] != null)
					return ((ConnectionProperties)this[connection]).AutoCommit;
				else
					return true;
			}

			/// <summary>
			/// Sets the value of a parameter using any permitted object.  The given argument object will be converted to the
			/// corresponding SQL type before being sent to the database.
			/// </summary>
			/// <param name="command">Command object to be changed.</param>
			/// <param name="parameterIndex">One-based index of the parameter to be set.</param>
			/// <param name="parameter">The object containing the input parameter value.</param>
			public void SetValue(System.Data.OleDb.OleDbCommand command, int parameterIndex, object parameter) {
				if (command.Parameters.Count < parameterIndex)
					command.Parameters.Add(command.CreateParameter());
				command.Parameters[parameterIndex - 1].Value = parameter;
			}

			/// <summary>
			/// Sets a parameter to SQL NULL.
			/// </summary>
			/// <param name="command">Command object to be changed.</param>
			/// <param name="parameterIndex">One-based index of the parameter to be set.</param>
			/// <param name="targetSqlType">The SQL type to be sent to the database.</param>
			public void SetNull(System.Data.OleDb.OleDbCommand command, int parameterIndex, int sqlType) {
				if (command.Parameters.Count < parameterIndex)
					command.Parameters.Add(command.CreateParameter());
				command.Parameters[parameterIndex - 1].Value = System.Convert.DBNull;
				command.Parameters[parameterIndex - 1].OleDbType = (System.Data.OleDb.OleDbType)sqlType;
			}

			/// <summary>
			/// Sets the value of a parameter using an object.  The given argument object will be converted to the
			/// corresponding SQL type before being sent to the database.
			/// </summary>
			/// <param name="command">Command object to be changed.</param>
			/// <param name="parameterIndex">One-based index of the parameter to be set.</param>
			/// <param name="parameter">The object containing the input parameter value.</param>
			/// <param name="targetSqlType">The SQL type to be sent to the database.</param>
			public void SetObject(System.Data.OleDb.OleDbCommand command, int parameterIndex, object parameter, int targetSqlType) {
				if (command.Parameters.Count < parameterIndex)
					command.Parameters.Add(command.CreateParameter());
				command.Parameters[parameterIndex - 1].Value = parameter;
				command.Parameters[parameterIndex - 1].OleDbType = (System.Data.OleDb.OleDbType)targetSqlType;
			}

			/// <summary>
			/// Sets the value of a parameter using an object.  The given argument object will be converted to the
			/// corresponding SQL type before being sent to the database.
			/// </summary>
			/// <param name="command">Command object to be changed.</param>
			/// <param name="parameterIndex">One-based index of the parameter to be set.</param>
			/// <param name="parameter">The object containing the input parameter value.</param>
			public void SetObject(System.Data.OleDb.OleDbCommand command, int parameterIndex, object parameter) {
				if (command.Parameters.Count < parameterIndex)
					command.Parameters.Add(command.CreateParameter());
				command.Parameters[parameterIndex - 1].Value = parameter;
			}

			/// <summary>
			/// This method is for such prepared statements verify if the Conection is autoCommit for assing the transaction to the command.
			/// </summary>
			/// <param name="command">The command to be tested.</param>
			/// <returns>The number of rows afected.</returns>
			public int ExecuteUpdate(System.Data.OleDb.OleDbCommand command) {
				if (!(((ConnectionProperties)this[command.Connection]).AutoCommit)) {
					command.Transaction = ((ConnectionProperties)this[command.Connection]).Transaction;
					return command.ExecuteNonQuery();
				}
				else
					return command.ExecuteNonQuery();
			}

			class ConnectionProperties {
				public bool AutoCommit;
				public System.Data.OleDb.OleDbTransaction Transaction;
				public System.Data.IsolationLevel TransactionLevel;
			}
		}
	}


	/*******************************/
	/// <summary>
	/// Removes the element with the specified key from a Hashtable instance.
	/// </summary>
	/// <param name="hashtable">The Hashtable instance</param>
	/// <param name="key">The key of the element to remove</param>
	/// <returns>The element removed</returns>  
	public static System.Object HashtableRemove(System.Collections.Hashtable hashtable, System.Object key) {
		System.Object element = hashtable[key];
		hashtable.Remove(key);
		return element;
	}

	/*******************************/
	/// <summary>
	/// The class performs token processing from strings
	/// </summary>
	public class Tokenizer {
		//Element list identified
		private System.Collections.ArrayList elements;
		//Source string to use
		private string source;
		//The tokenizer uses the default delimiter set: the space character, the tab character, the newline character, and the carriage-return character
		private string delimiters = " \t\n\r";		

		/// <summary>
		/// Initializes a new class instance with a specified string to process
		/// </summary>
		/// <param name="source">String to tokenize</param>
		public Tokenizer(string source) {			
			this.elements = new System.Collections.ArrayList();
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		/// <summary>
		/// Initializes a new class instance with a specified string to process
		/// and the specified token delimiters to use
		/// </summary>
		/// <param name="source">String to tokenize</param>
		/// <param name="delimiters">String containing the delimiters</param>
		public Tokenizer(string source, string delimiters) {
			this.elements = new System.Collections.ArrayList();
			this.delimiters = delimiters;
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		/// <summary>
		/// Current token count for the source string
		/// </summary>
		public int Count {
			get {
				return (this.elements.Count);
			}
		}

		/// <summary>
		/// Determines if there are more tokens to return from the source string
		/// </summary>
		/// <returns>True or false, depending if there are more tokens</returns>
		public bool HasMoreTokens() {
			return (this.elements.Count > 0);			
		}

		/// <summary>
		/// Returns the next token from the token list
		/// </summary>
		/// <returns>The string value of the token</returns>
		public string NextToken() {			
			string result;
			if (source == "") throw new System.Exception();
			else {
				this.elements = new System.Collections.ArrayList();
				this.elements.AddRange(this.source.Split(delimiters.ToCharArray()));
				RemoveEmptyStrings();		
				result = (string) this.elements[0];
				this.elements.RemoveAt(0);				
				this.source = this.source.Remove(this.source.IndexOf(result),result.Length);
				this.source = this.source.TrimStart(this.delimiters.ToCharArray());
				return result;					
			}			
		}

		/// <summary>
		/// Returns the next token from the source string, using the provided
		/// token delimiters
		/// </summary>
		/// <param name="delimiters">String containing the delimiters to use</param>
		/// <returns>The string value of the token</returns>
		public string NextToken(string delimiters) {
			this.delimiters = delimiters;
			return NextToken();
		}

		/// <summary>
		/// Removes all empty strings from the token list
		/// </summary>
		private void RemoveEmptyStrings() {
			for (int index=0; index < this.elements.Count; index++)
				if ((string)this.elements[index]== "") {
					this.elements.RemoveAt(index);
					index--;
				}
		}
	}

	/*******************************/
	/// <summary>
	/// This class manages a set of elements.
	/// </summary>
	public class SetSupport : System.Collections.ArrayList {
		/// <summary>
		/// Creates a new set.
		/// </summary>
		public SetSupport(): base() {           
		}

		/// <summary>
		/// Creates a new set initialized with System.Collections.ICollection object
		/// </summary>
		/// <param name="collection">System.Collections.ICollection object to initialize the set object</param>
		public SetSupport(System.Collections.ICollection collection): base(collection) {           
		}

		/// <summary>
		/// Creates a new set initialized with a specific capacity.
		/// </summary>
		/// <param name="capacity">value to set the capacity of the set object</param>
		public SetSupport(int capacity): base(capacity) {           
		}
	 
		/// <summary>
		/// Adds an element to the set.
		/// </summary>
		/// <param name="objectToAdd">The object to be added.</param>
		/// <returns>True if the object was added, false otherwise.</returns>
		public new virtual bool Add(object objectToAdd) {
			if (this.Contains(objectToAdd))
				return false;
			else {
				base.Add(objectToAdd);
				return true;
			}
		}
	 
		/// <summary>
		/// Adds all the elements contained in the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be added.</param>
		/// <returns>Returns true if all the elements were successfuly added. Otherwise returns false.</returns>
		public virtual bool AddAll(System.Collections.ICollection collection) {
			bool result = false;
			if (collection!=null) {
				System.Collections.IEnumerator tempEnumerator = new System.Collections.ArrayList(collection).GetEnumerator();
				while (tempEnumerator.MoveNext()) {
					if (tempEnumerator.Current != null)
						result = this.Add(tempEnumerator.Current);
				}
			}
			return result;
		}
		
		/// <summary>
		/// Adds all the elements contained in the specified support class collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be added.</param>
		/// <returns>Returns true if all the elements were successfuly added. Otherwise returns false.</returns>
		public virtual bool AddAll(CollectionSupport collection) {
			return this.AddAll((System.Collections.ICollection)collection);
		}
	 
		/// <summary>
		/// Verifies that all the elements of the specified collection are contained into the current collection. 
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be verified.</param>
		/// <returns>True if the collection contains all the given elements.</returns>
		public virtual bool ContainsAll(System.Collections.ICollection collection) {
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = collection.GetEnumerator();
			while (tempEnumerator.MoveNext())
				if (!(result = this.Contains(tempEnumerator.Current)))
					break;
			return result;
		}
		
		/// <summary>
		/// Verifies if all the elements of the specified collection are contained into the current collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be verified.</param>
		/// <returns>Returns true if all the elements are contained in the collection. Otherwise returns false.</returns>
		public virtual bool ContainsAll(CollectionSupport collection) {
			return this.ContainsAll((System.Collections.ICollection) collection);
		}		
	 
		/// <summary>
		/// Verifies if the collection is empty.
		/// </summary>
		/// <returns>True if the collection is empty, false otherwise.</returns>
		public virtual bool IsEmpty() {
			return (this.Count == 0);
		}
	 	 
		/// <summary>
		/// Removes an element from the set.
		/// </summary>
		/// <param name="elementToRemove">The element to be removed.</param>
		/// <returns>True if the element was removed.</returns>
		public new virtual bool Remove(object elementToRemove) {
			bool result = false;
			if (this.Contains(elementToRemove))
				result = true;
			base.Remove(elementToRemove);
			return result;
		}
		
		/// <summary>
		/// Removes all the elements contained in the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be removed.</param>
		/// <returns>True if all the elements were successfuly removed, false otherwise.</returns>
		public virtual bool RemoveAll(System.Collections.ICollection collection) { 
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = collection.GetEnumerator();
			while (tempEnumerator.MoveNext()) {
				if ((result == false) && (this.Contains(tempEnumerator.Current)))
					result = true;
				this.Remove(tempEnumerator.Current);
			}
			return result;
		}
		
		/// <summary>
		/// Removes all the elements contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be removed.</param>
		/// <returns>Returns true if all the elements were successfuly removed. Otherwise returns false.</returns>
		public virtual bool RemoveAll(CollectionSupport collection) { 
			return this.RemoveAll((System.Collections.ICollection) collection);
		}		

		/// <summary>
		/// Removes all the elements that aren't contained in the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to verify the elements that will be retained.</param>
		/// <returns>True if all the elements were successfully removed, false otherwise.</returns>
		public virtual bool RetainAll(System.Collections.ICollection collection) {
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = collection.GetEnumerator();
			SetSupport tempSet = (SetSupport)collection;
			while (tempEnumerator.MoveNext())
				if (!tempSet.Contains(tempEnumerator.Current)) {
					result = this.Remove(tempEnumerator.Current);
					tempEnumerator = this.GetEnumerator();
				}
			return result;
		}
		
		/// <summary>
		/// Removes all the elements that aren't contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to verify the elements that will be retained.</param>
		/// <returns>Returns true if all the elements were successfully removed. Otherwise returns false.</returns>
		public virtual bool RetainAll(CollectionSupport collection) {
			return this.RetainAll((System.Collections.ICollection) collection);
		}		
	 
		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <returns>The array containing all the elements of the collection.</returns>
		public new virtual object[] ToArray() {
			int index = 0;
			object[] tempObject= new object[this.Count];
			System.Collections.IEnumerator tempEnumerator = this.GetEnumerator();
			while (tempEnumerator.MoveNext())
				tempObject[index++] = tempEnumerator.Current;
			return tempObject;
		}

		/// <summary>
		/// Obtains an array containing all the elements in the collection.
		/// </summary>
		/// <param name="objects">The array into which the elements of the collection will be stored.</param>
		/// <returns>The array containing all the elements of the collection.</returns>
		public virtual object[] ToArray(object[] objects) {
			int index = 0;
			System.Collections.IEnumerator tempEnumerator = this.GetEnumerator();
			while (tempEnumerator.MoveNext())
				objects[index++] = tempEnumerator.Current;
			return objects;
		}
	}
	/*******************************/
	/// <summary>
	/// This class manages different operation with collections.
	/// </summary>
	public class AbstractSetSupport : SetSupport {
		/// <summary>
		/// The constructor with no parameters to create an abstract set.
		/// </summary>
		public AbstractSetSupport() {
		}
	}


	/*******************************/
	/// <summary> 
	/// This class manages a hash set of elements.
	/// </summary>
	public class HashSetSupport : AbstractSetSupport {
		/// <summary>
		/// Creates a new hash set collection.
		/// </summary>
		public HashSetSupport() {     
		}
	       
		/// <summary>
		/// Creates a new hash set collection.
		/// </summary>
		/// <param name="collection">The collection to initialize the hash set with.</param>
		public HashSetSupport(System.Collections.ICollection collection) {
			this.AddRange(collection);
		}
	       
		/// <summary>
		/// Creates a new hash set with the given capacity.
		/// </summary>
		/// <param name="capacity">The initial capacity of the hash set.</param>
		public HashSetSupport(int capacity) {
			this.Capacity = capacity;
		}
	    
		/// <summary>
		/// Creates a new hash set with the given capacity.
		/// </summary>
		/// <param name="capacity">The initial capacity of the hash set.</param>
		/// <param name="loadFactor">The load factor of the hash set.</param>
		public HashSetSupport(int capacity, float loadFactor) {
			this.Capacity = capacity;
		}

		/// <summary>
		/// Creates a copy of the HashSetSupport.
		/// </summary>
		/// <returns> A copy of the HashSetSupport.</returns>
		public virtual object HashSetClone() {
			return MemberwiseClone();
		}
	}
	/*******************************/
	/// <summary>
	/// This class contains different methods to manage Collections.
	/// </summary>
	public class CollectionSupport : System.Collections.CollectionBase {
		/// <summary>
		/// Creates an instance of the Collection by using an inherited constructor.
		/// </summary>
		public CollectionSupport() : base() {			
		}

		/// <summary>
		/// Adds an specified element to the collection.
		/// </summary>
		/// <param name="element">The element to be added.</param>
		/// <returns>Returns true if the element was successfuly added. Otherwise returns false.</returns>
		public virtual bool Add(System.Object element) {
			return (this.List.Add(element) != -1);
		}	

		/// <summary>
		/// Adds all the elements contained in the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be added.</param>
		/// <returns>Returns true if all the elements were successfuly added. Otherwise returns false.</returns>
		public virtual bool AddAll(System.Collections.ICollection collection) {
			bool result = false;
			if (collection!=null) {
				System.Collections.IEnumerator tempEnumerator = new System.Collections.ArrayList(collection).GetEnumerator();
				while (tempEnumerator.MoveNext()) {
					if (tempEnumerator.Current != null)
						result = this.Add(tempEnumerator.Current);
				}
			}
			return result;
		}


		/// <summary>
		/// Adds all the elements contained in the specified support class collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be added.</param>
		/// <returns>Returns true if all the elements were successfuly added. Otherwise returns false.</returns>
		public virtual bool AddAll(CollectionSupport collection) {
			return this.AddAll((System.Collections.ICollection)collection);
		}

		/// <summary>
		/// Verifies if the specified element is contained into the collection. 
		/// </summary>
		/// <param name="element"> The element that will be verified.</param>
		/// <returns>Returns true if the element is contained in the collection. Otherwise returns false.</returns>
		public virtual bool Contains(System.Object element) {
			return this.List.Contains(element);
		}

		/// <summary>
		/// Verifies if all the elements of the specified collection are contained into the current collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be verified.</param>
		/// <returns>Returns true if all the elements are contained in the collection. Otherwise returns false.</returns>
		public virtual bool ContainsAll(System.Collections.ICollection collection) {
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = new System.Collections.ArrayList(collection).GetEnumerator();
			while (tempEnumerator.MoveNext())
				if (!(result = this.Contains(tempEnumerator.Current)))
					break;
			return result;
		}

		/// <summary>
		/// Verifies if all the elements of the specified collection are contained into the current collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be verified.</param>
		/// <returns>Returns true if all the elements are contained in the collection. Otherwise returns false.</returns>
		public virtual bool ContainsAll(CollectionSupport collection) {
			return this.ContainsAll((System.Collections.ICollection) collection);
		}

		/// <summary>
		/// Verifies if the collection is empty.
		/// </summary>
		/// <returns>Returns true if the collection is empty. Otherwise returns false.</returns>
		public virtual bool IsEmpty() {
			return (this.Count == 0);
		}

		/// <summary>
		/// Removes an specified element from the collection.
		/// </summary>
		/// <param name="element">The element to be removed.</param>
		/// <returns>Returns true if the element was successfuly removed. Otherwise returns false.</returns>
		public virtual bool Remove(System.Object element) {
			bool result = false;
			if (this.Contains(element)) {
				this.List.Remove(element);
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Removes all the elements contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be removed.</param>
		/// <returns>Returns true if all the elements were successfuly removed. Otherwise returns false.</returns>
		public virtual bool RemoveAll(System.Collections.ICollection collection) { 
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = new System.Collections.ArrayList(collection).GetEnumerator();
			while (tempEnumerator.MoveNext()) {
				if (this.Contains(tempEnumerator.Current))
					result = this.Remove(tempEnumerator.Current);
			}
			return result;
		}

		/// <summary>
		/// Removes all the elements contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to extract the elements that will be removed.</param>
		/// <returns>Returns true if all the elements were successfuly removed. Otherwise returns false.</returns>
		public virtual bool RemoveAll(CollectionSupport collection) { 
			return this.RemoveAll((System.Collections.ICollection) collection);
		}

		/// <summary>
		/// Removes all the elements that aren't contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to verify the elements that will be retained.</param>
		/// <returns>Returns true if all the elements were successfully removed. Otherwise returns false.</returns>
		public virtual bool RetainAll(System.Collections.ICollection collection) {
			bool result = false;
			System.Collections.IEnumerator tempEnumerator = this.GetEnumerator();
			CollectionSupport tempCollection = new CollectionSupport();
			tempCollection.AddAll(collection);
			while (tempEnumerator.MoveNext())
				if (!tempCollection.Contains(tempEnumerator.Current)) {
					result = this.Remove(tempEnumerator.Current);
					
					if (result == true) {
						tempEnumerator = this.GetEnumerator();
					}
				}
			return result;
		}

		/// <summary>
		/// Removes all the elements that aren't contained into the specified collection.
		/// </summary>
		/// <param name="collection">The collection used to verify the elements that will be retained.</param>
		/// <returns>Returns true if all the elements were successfully removed. Otherwise returns false.</returns>
		public virtual bool RetainAll(CollectionSupport collection) {
			return this.RetainAll((System.Collections.ICollection) collection);
		}

		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <returns>The array containing all the elements of the collection</returns>
		public virtual System.Object[] ToArray() {	
			int index = 0;
			System.Object[] objects = new System.Object[this.Count];
			System.Collections.IEnumerator tempEnumerator = this.GetEnumerator();
			while (tempEnumerator.MoveNext())
				objects[index++] = tempEnumerator.Current;
			return objects;
		}

		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <param name="objects">The array into which the elements of the collection will be stored.</param>
		/// <returns>The array containing all the elements of the collection.</returns>
		public virtual System.Object[] ToArray(System.Object[] objects) {	
			int index = 0;
			System.Collections.IEnumerator tempEnumerator = this.GetEnumerator();
			while (tempEnumerator.MoveNext())
				objects[index++] = tempEnumerator.Current;
			return objects;
		}

		/// <summary>
		/// Creates a CollectionSupport object with the contents specified in array.
		/// </summary>
		/// <param name="array">The array containing the elements used to populate the new CollectionSupport object.</param>
		/// <returns>A CollectionSupport object populated with the contents of array.</returns>
		public static CollectionSupport ToCollectionSupport(System.Object[] array) {
			CollectionSupport tempCollectionSupport = new CollectionSupport();             
			tempCollectionSupport.AddAll(array);
			return tempCollectionSupport;
		}
	}

	/*******************************/
	/// <summary>
	/// Provides support for DateFormat
	/// </summary>

	/*******************************/
	/// <summary>
	/// Gets the DateTimeFormat instance using the culture passed as parameter and sets the pattern to the time or date depending of the value
	/// </summary>
	/// <param name="dateStyle">The desired date style.</param>
	/// <param name="timeStyle">The desired time style</param>
	/// <param name="culture">The CultureInfo instance used to obtain the DateTimeFormat</param>
	/// <returns>The DateTimeFomatInfo of the culture and with the desired date or time style</returns>
	public static System.Globalization.DateTimeFormatInfo GetDateTimeFormatInstance(int dateStyle, int timeStyle, System.Globalization.CultureInfo culture) {
		System.Globalization.DateTimeFormatInfo format = culture.DateTimeFormat;

		switch (timeStyle) {
			case -1:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "");
				break;

			case 0:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm:ss 'o clock' tt zzz");
				break;

			case 1:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm:ss tt zzz");
				break;

			case 2:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm:ss tt");
				break;

			case 3:
				DateTimeFormatManager.manager.SetTimeFormatPattern(format, "h:mm tt");
				break;
		}

		switch (dateStyle) {
			case -1:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "");
				break;

			case 0:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "dddd, MMMM dd%, yyy");
				break;

			case 1:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "MMMM dd%, yyy" );
				break;

			case 2:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "d-MMM-yy" );
				break;

			case 3:
				DateTimeFormatManager.manager.SetDateFormatPattern(format, "M/dd/yy");
				break;
		}

		return format;
	}

	/*******************************/
	/// <summary>
	/// This class manages different issues for calendars.
	/// The different calendars are internally managed using a hash table structure.
	/// </summary>

	public static int NumericValue(char character)
	{
		int numericValue = 0;

		if(character >= 'a' && character <= 'z')
			numericValue = character - 'a' + 10;
		else if(character >= 'A' && character <= 'Z')
			numericValue = character - 'A' + 10;
		else
			numericValue = (int)System.Char.GetNumericValue(character);
	
		return numericValue;
	}
	public class DateTimeFormatManager
	{
		static public DateTimeFormatHashTable manager = new DateTimeFormatHashTable();

		/// <summary>
		/// Hashtable class to provide functionality for dateformat properties
		/// </summary>
		public class DateTimeFormatHashTable :System.Collections.Hashtable 
		{
			/// <summary>
			/// Sets the format for datetime.
			/// </summary>
			/// <param name="format">DateTimeFormat instance to set the pattern</param>
			/// <param name="newPattern">A string with the pattern format</param>
			public void SetDateFormatPattern(System.Globalization.DateTimeFormatInfo format, System.String newPattern)
			{
				if (this[format] != null)
					((DateTimeFormatProperties) this[format]).DateFormatPattern = newPattern;
				else
				{
					DateTimeFormatProperties tempProps = new DateTimeFormatProperties();
					tempProps.DateFormatPattern  = newPattern;
					Add(format, tempProps);
				}
			}

			/// <summary>
			/// Gets the current format pattern of the DateTimeFormat instance
			/// </summary>
			/// <param name="format">The DateTimeFormat instance which the value will be obtained</param>
			/// <returns>The string representing the current datetimeformat pattern</returns>
			public System.String GetDateFormatPattern(System.Globalization.DateTimeFormatInfo format)
			{
				if (this[format] == null)
					return "d-MMM-yy";
				else
					return ((DateTimeFormatProperties) this[format]).DateFormatPattern;
			}
		
			/// <summary>
			/// Sets the datetimeformat pattern to the giving format
			/// </summary>
			/// <param name="format">The datetimeformat instance to set</param>
			/// <param name="newPattern">The new datetimeformat pattern</param>
			public void SetTimeFormatPattern(System.Globalization.DateTimeFormatInfo format, System.String newPattern)
			{
				if (this[format] != null)
					((DateTimeFormatProperties) this[format]).TimeFormatPattern = newPattern;
				else
				{
					DateTimeFormatProperties tempProps = new DateTimeFormatProperties();
					tempProps.TimeFormatPattern  = newPattern;
					Add(format, tempProps);
				}
			}

			/// <summary>
			/// Gets the current format pattern of the DateTimeFormat instance
			/// </summary>
			/// <param name="format">The DateTimeFormat instance which the value will be obtained</param>
			/// <returns>The string representing the current datetimeformat pattern</returns>
			public System.String GetTimeFormatPattern(System.Globalization.DateTimeFormatInfo format)
			{
				if (this[format] == null)
					return "h:mm:ss tt";
				else
					return ((DateTimeFormatProperties) this[format]).TimeFormatPattern;
			}

			/// <summary>
			/// Internal class to provides the DateFormat and TimeFormat pattern properties on .NET
			/// </summary>
			class DateTimeFormatProperties
			{
				public System.String DateFormatPattern = "d-MMM-yy";
				public System.String TimeFormatPattern = "h:mm:ss tt";
			}
		}	
	}
	/*******************************/
	/// <summary>
	/// Gets the DateTimeFormat instance and date instance to obtain the date with the format passed
	/// </summary>
	/// <param name="format">The DateTimeFormat to obtain the time and date pattern</param>
	/// <param name="date">The date instance used to get the date</param>
	/// <returns>A string representing the date with the time and date patterns</returns>
	public static System.String FormatDateTime(System.Globalization.DateTimeFormatInfo format, System.DateTime date)
	{
		System.String timePattern = DateTimeFormatManager.manager.GetTimeFormatPattern(format);
		System.String datePattern = DateTimeFormatManager.manager.GetDateFormatPattern(format);
		return date.ToString(datePattern + " " + timePattern, format);            
	}

	
	/*******************************/
	/// <summary>
	/// This class manages different features for calendars.
	/// The different calendars are internally managed using a hashtable structure.
	/// </summary>
	public class CalendarManager
	{
		/// <summary>
		/// Field used to get or set the year.
		/// </summary>
		public const int YEAR = 1;

		/// <summary>
		/// Field used to get or set the month.
		/// </summary>
		public const int MONTH = 2;
		
		/// <summary>
		/// Field used to get or set the day of the month.
		/// </summary>
		public const int DATE = 5;
		
		/// <summary>
		/// Field used to get or set the hour of the morning or afternoon.
		/// </summary>
		public const int HOUR = 10;
		
		/// <summary>
		/// Field used to get or set the minute within the hour.
		/// </summary>
		public const int MINUTE = 12;
		
		/// <summary>
		/// Field used to get or set the second within the minute.
		/// </summary>
		public const int SECOND = 13;
		
		/// <summary>
		/// Field used to get or set the millisecond within the second.
		/// </summary>
		public const int MILLISECOND = 14;
		
		/// <summary>
		/// Field used to get or set the day of the year.
		/// </summary>
		public const int DAY_OF_YEAR = 4;
		
		/// <summary>
		/// Field used to get or set the day of the month.
		/// </summary>
		public const int DAY_OF_MONTH = 6;
		
		/// <summary>
		/// Field used to get or set the day of the week.
		/// </summary>
		public const int DAY_OF_WEEK = 7;
		
		/// <summary>
		/// Field used to get or set the hour of the day.
		/// </summary>
		public const int HOUR_OF_DAY = 11;
		
		/// <summary>
		/// Field used to get or set whether the HOUR is before or after noon.
		/// </summary>
		public const int AM_PM = 9;
		
		/// <summary>
		/// Field used to get or set the value of the AM_PM field which indicates the period of the day from midnight to just before noon.
		/// </summary>
		public const int AM = 0;
		
		/// <summary>
		/// Field used to get or set the value of the AM_PM field which indicates the period of the day from noon to just before midnight.
		/// </summary>
		public const int PM = 1;
		
		/// <summary>
		/// The hashtable that contains the calendars and its properties.
		/// </summary>
		static public CalendarHashTable manager = new CalendarHashTable();

		/// <summary>
		/// Internal class that inherits from HashTable to manage the different calendars.
		/// This structure will contain an instance of System.Globalization.Calendar that represents 
		/// a type of calendar and its properties (represented by an instance of CalendarProperties 
		/// class).
		/// </summary>
		public class CalendarHashTable:System.Collections.Hashtable 
		{
			/// <summary>
			/// Gets the calendar current date and time.
			/// </summary>
			/// <param name="calendar">The calendar to get its current date and time.</param>
			/// <returns>A System.DateTime value that indicates the current date and time for the 
			/// calendar given.</returns>
			public System.DateTime GetDateTime(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
					return ((CalendarProperties) this[calendar]).dateTime;
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					return this.GetDateTime(calendar);
				}
			}

			/// <summary>
			/// Sets the specified System.DateTime value to the specified calendar.
			/// </summary>
			/// <param name="calendar">The calendar to set its date.</param>
			/// <param name="date">The System.DateTime value to set to the calendar.</param>
			public void SetDateTime(System.Globalization.Calendar calendar, System.DateTime date)
			{
				if (this[calendar] != null)
				{
					((CalendarProperties) this[calendar]).dateTime = date;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = date;
					this.Add(calendar, tempProps);
				}
			}

			/// <summary>
			/// Sets the corresponding field in an specified calendar with the value given.
			/// If the specified calendar does not have exist in the hash table, it creates a 
			/// new instance of the calendar with the current date and time and then assings it 
			/// the new specified value.
			/// </summary>
			/// <param name="calendar">The calendar to set its date or time.</param>
			/// <param name="field">One of the fields that composes a date/time.</param>
			/// <param name="fieldValue">The value to be set.</param>
			public void Set(System.Globalization.Calendar calendar, int field, int fieldValue)
			{
				if (this[calendar] != null)
				{
					System.DateTime tempDate = ((CalendarProperties) this[calendar]).dateTime;
					switch (field)
					{
						case CalendarManager.DATE:
							tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
							break;
						case CalendarManager.HOUR:
							tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
							break;
						case CalendarManager.MILLISECOND:
							tempDate = tempDate.AddMilliseconds(fieldValue - tempDate.Millisecond);
							break;
						case CalendarManager.MINUTE:
							tempDate = tempDate.AddMinutes(fieldValue - tempDate.Minute);
							break;
						case CalendarManager.MONTH:
							//Month value is 0-based. e.g., 0 for January
							tempDate = tempDate.AddMonths((fieldValue + 1) - tempDate.Month);
							break;
						case CalendarManager.SECOND:
							tempDate = tempDate.AddSeconds(fieldValue - tempDate.Second);
							break;
						case CalendarManager.YEAR:
							tempDate = tempDate.AddYears(fieldValue - tempDate.Year);
							break;
						case CalendarManager.DAY_OF_MONTH:
							tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
							break;
						case CalendarManager.DAY_OF_WEEK:
							tempDate = tempDate.AddDays((fieldValue - 1) - (int)tempDate.DayOfWeek);
							break;
						case CalendarManager.DAY_OF_YEAR:
							tempDate = tempDate.AddDays(fieldValue - tempDate.DayOfYear);
							break;
						case CalendarManager.HOUR_OF_DAY:
							tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
							break;

						default:
							break;
					}
					((CalendarProperties) this[calendar]).dateTime = tempDate;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, field, fieldValue);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) and hour (hour and minute) 
			/// to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			/// <param name="hour">Integer value that represent the hour.</param>
			/// <param name="minute">Integer value that represent the minutes.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
					this.Set(calendar, CalendarManager.HOUR, hour);
					this.Set(calendar, CalendarManager.MINUTE, minute);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day, hour, minute);
				}
			}

			/// <summary>
			/// Sets the corresponding date (day, month and year) and hour (hour, minute and second) 
			/// to the calendar specified.
			/// If the calendar does not exist in the hash table, it creates a new instance and sets 
			/// its values.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="year">Integer value that represent the year.</param>
			/// <param name="month">Integer value that represent the month.</param>
			/// <param name="day">Integer value that represent the day.</param>
			/// <param name="hour">Integer value that represent the hour.</param>
			/// <param name="minute">Integer value that represent the minutes.</param>
			/// <param name="second">Integer value that represent the seconds.</param>
			public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute, int second)
			{
				if (this[calendar] != null)
				{
					this.Set(calendar, CalendarManager.YEAR, year);
					this.Set(calendar, CalendarManager.MONTH, month);
					this.Set(calendar, CalendarManager.DATE, day);
					this.Set(calendar, CalendarManager.HOUR, hour);
					this.Set(calendar, CalendarManager.MINUTE, minute);
					this.Set(calendar, CalendarManager.SECOND, second);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					this.Set(calendar, year, month, day, hour, minute, second);
				}
			}

			/// <summary>
			/// Gets the value represented by the field specified.
			/// </summary>
			/// <param name="calendar">The calendar to get its date or time.</param>
			/// <param name="field">One of the field that composes a date/time.</param>
			/// <returns>The integer value for the field given.</returns>
			public int Get(System.Globalization.Calendar calendar, int field)
			{
				if (this[calendar] != null)
				{
					int tempHour;
					switch (field)
					{
						case CalendarManager.DATE:
							return ((CalendarProperties) this[calendar]).dateTime.Day;
						case CalendarManager.HOUR:
							tempHour = ((CalendarProperties) this[calendar]).dateTime.Hour;
							return tempHour > 12 ? tempHour - 12 : tempHour;
						case CalendarManager.MILLISECOND:
							return ((CalendarProperties) this[calendar]).dateTime.Millisecond;
						case CalendarManager.MINUTE:
							return ((CalendarProperties) this[calendar]).dateTime.Minute;
						case CalendarManager.MONTH:
							//Month value is 0-based. e.g., 0 for January
							return ((CalendarProperties) this[calendar]).dateTime.Month - 1;
						case CalendarManager.SECOND:
							return ((CalendarProperties) this[calendar]).dateTime.Second;
						case CalendarManager.YEAR:
							return ((CalendarProperties) this[calendar]).dateTime.Year;
						case CalendarManager.DAY_OF_MONTH:
							return ((CalendarProperties) this[calendar]).dateTime.Day;
						case CalendarManager.DAY_OF_YEAR:							
							return (int)(((CalendarProperties) this[calendar]).dateTime.DayOfYear);
						case CalendarManager.DAY_OF_WEEK:
							return (int)(((CalendarProperties) this[calendar]).dateTime.DayOfWeek) + 1;
						case CalendarManager.HOUR_OF_DAY:
							return ((CalendarProperties) this[calendar]).dateTime.Hour;
						case CalendarManager.AM_PM:
							tempHour = ((CalendarProperties) this[calendar]).dateTime.Hour;
							return tempHour > 12 ? CalendarManager.PM : CalendarManager.AM;

						default:
							return 0;
					}
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					this.Add(calendar, tempProps);
					return this.Get(calendar, field);
				}
			}

			/// <summary>
			/// Sets the time in the specified calendar with the long value.
			/// </summary>
			/// <param name="calendar">The calendar to set its date and time.</param>
			/// <param name="milliseconds">A long value that indicates the milliseconds to be set to 
			/// the hour for the calendar.</param>
			public void SetTimeInMilliseconds(System.Globalization.Calendar calendar, long milliseconds)
			{
				if (this[calendar] != null)
				{
					((CalendarProperties) this[calendar]).dateTime = new System.DateTime(milliseconds);
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = new System.DateTime(System.TimeSpan.TicksPerMillisecond * milliseconds);
					this.Add(calendar, tempProps);
				}
			}
				
			/// <summary>
			/// Gets what the first day of the week is; e.g., Sunday in US, Monday in France.
			/// </summary>
			/// <param name="calendar">The calendar to get its first day of the week.</param>
			/// <returns>A System.DayOfWeek value indicating the first day of the week.</returns>
			public System.DayOfWeek GetFirstDayOfWeek(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
				{
					if (((CalendarProperties)this[calendar]).dateTimeFormat == null)
					{
						((CalendarProperties)this[calendar]).dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
						((CalendarProperties)this[calendar]).dateTimeFormat.FirstDayOfWeek = System.DayOfWeek.Sunday;
					}
					return ((CalendarProperties) this[calendar]).dateTimeFormat.FirstDayOfWeek;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
					tempProps.dateTimeFormat.FirstDayOfWeek = System.DayOfWeek.Sunday;
					this.Add(calendar, tempProps);
					return this.GetFirstDayOfWeek(calendar);
				}
			}

			/// <summary>
			/// Sets what the first day of the week is; e.g., Sunday in US, Monday in France.
			/// </summary>
			/// <param name="calendar">The calendar to set its first day of the week.</param>
			/// <param name="firstDayOfWeek">A System.DayOfWeek value indicating the first day of the week
			/// to be set.</param>
			public void SetFirstDayOfWeek(System.Globalization.Calendar calendar, System.DayOfWeek  firstDayOfWeek)
			{
				if (this[calendar] != null)
				{
					if (((CalendarProperties)this[calendar]).dateTimeFormat == null)
						((CalendarProperties)this[calendar]).dateTimeFormat = new System.Globalization.DateTimeFormatInfo();

					((CalendarProperties) this[calendar]).dateTimeFormat.FirstDayOfWeek = firstDayOfWeek;
				}
				else
				{
					CalendarProperties tempProps = new CalendarProperties();
					tempProps.dateTime = System.DateTime.Now;
					tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
					this.Add(calendar, tempProps);
					this.SetFirstDayOfWeek(calendar, firstDayOfWeek);
				}
			}

			/// <summary>
			/// Removes the specified calendar from the hash table.
			/// </summary>
			/// <param name="calendar">The calendar to be removed.</param>
			public void Clear(System.Globalization.Calendar calendar)
			{
				if (this[calendar] != null)
					this.Remove(calendar);
			}

			/// <summary>
			/// Removes the specified field from the calendar given.
			/// If the field does not exists in the calendar, the calendar is removed from the table.
			/// </summary>
			/// <param name="calendar">The calendar to remove the value from.</param>
			/// <param name="field">The field to be removed from the calendar.</param>
			public void Clear(System.Globalization.Calendar calendar, int field)
			{
				if (this[calendar] != null)
					this.Set(calendar, field, 0);
			}

			/// <summary>
			/// Internal class that represents the properties of a calendar instance.
			/// </summary>
			class CalendarProperties
			{
				/// <summary>
				/// The date and time of a calendar.
				/// </summary>
				public System.DateTime dateTime;
				
				/// <summary>
				/// The format for the date and time in a calendar.
				/// </summary>
				public System.Globalization.DateTimeFormatInfo dateTimeFormat;
			}
		}
	}

	public class ICollectionSupport
	{
		/// <summary>
		/// Adds a new element to the specified collection.
		/// </summary>
		/// <param name="c">Collection where the new element will be added.</param>
		/// <param name="obj">Object to add.</param>
		/// <returns>true</returns>
		public static bool Add(System.Collections.ICollection c, System.Object obj)
		{
			bool added = false;
			//Reflection. Invoke either the "add" or "Add" method.
			System.Reflection.MethodInfo method;
			try
			{
				//Get the "add" method for proprietary classes
				method = c.GetType().GetMethod("Add");
				if (method == null)
					method = c.GetType().GetMethod("add");
				int index = (int) method.Invoke(c, new System.Object[] {obj});
				if (index >=0)	
					added = true;
			}
			catch (System.Exception e)
			{
				throw e;
			}
			return added;
		}

		/// <summary>
		/// Adds all of the elements of the "c" collection to the "target" collection.
		/// </summary>
		/// <param name="target">Collection where the new elements will be added.</param>
		/// <param name="c">Collection whose elements will be added.</param>
		/// <returns>Returns true if at least one element was added, false otherwise.</returns>
		public static bool AddAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(c).GetEnumerator();
			bool added = false;

			//Reflection. Invoke "addAll" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("addAll");

				if (method != null)
					added = (bool) method.Invoke(target, new System.Object[] {c});
				else
				{
					method = target.GetType().GetMethod("Add");
					while (e.MoveNext() == true)
					{
						bool tempBAdded =  (int) method.Invoke(target, new System.Object[] {e.Current}) >= 0;
						added = added ? added : tempBAdded;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return added;
		}

		/// <summary>
		/// Removes all the elements from the collection.
		/// </summary>
		/// <param name="c">The collection to remove elements.</param>
		public static void Clear(System.Collections.ICollection c)
		{
			//Reflection. Invoke "Clear" method or "clear" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Clear");

				if (method == null)
					method = c.GetType().GetMethod("clear");

				method.Invoke(c, new System.Object[] {});
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Determines whether the collection contains the specified element.
		/// </summary>
		/// <param name="c">The collection to check.</param>
		/// <param name="obj">The object to locate in the collection.</param>
		/// <returns>true if the element is in the collection.</returns>
		public static bool Contains(System.Collections.ICollection c, System.Object obj)
		{
			bool contains = false;

			//Reflection. Invoke "contains" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Contains");

				if (method == null)
					method = c.GetType().GetMethod("contains");

				contains = (bool)method.Invoke(c, new System.Object[] {obj});
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return contains;
		}

		/// <summary>
		/// Determines whether the collection contains all the elements in the specified collection.
		/// </summary>
		/// <param name="target">The collection to check.</param>
		/// <param name="c">Collection whose elements would be checked for containment.</param>
		/// <returns>true id the target collection contains all the elements of the specified collection.</returns>
		public static bool ContainsAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{						
			System.Collections.IEnumerator e =  c.GetEnumerator();

			bool contains = false;

			//Reflection. Invoke "containsAll" method for proprietary classes or "Contains" method for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("containsAll");

				if (method != null)
					contains = (bool)method.Invoke(target, new Object[] {c});
				else
				{					
					method = target.GetType().GetMethod("Contains");
					while (e.MoveNext() == true)
					{
						if ((contains = (bool)method.Invoke(target, new Object[] {e.Current})) == false)
							break;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return contains;
		}

		/// <summary>
		/// Removes the specified element from the collection.
		/// </summary>
		/// <param name="c">The collection where the element will be removed.</param>
		/// <param name="obj">The element to remove from the collection.</param>
		public static bool Remove(System.Collections.ICollection c, System.Object obj)
		{
			bool changed = false;

			//Reflection. Invoke "remove" method for proprietary classes or "Remove" method
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("remove");

				if (method != null)
					method.Invoke(c, new System.Object[] {obj});
				else
				{
					method = c.GetType().GetMethod("Contains");
					changed = (bool)method.Invoke(c, new System.Object[] {obj});
					method = c.GetType().GetMethod("Remove");
					method.Invoke(c, new System.Object[] {obj});
				}
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return changed;
		}

		/// <summary>
		/// Removes all the elements from the specified collection that are contained in the target collection.
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to remove from the target collection.</param>
		/// <returns>true</returns>
		public static bool RemoveAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.ArrayList al = ToArrayList(c);
			System.Collections.IEnumerator e = al.GetEnumerator();

			//Reflection. Invoke "removeAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("removeAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {al});
				else
				{
					method = target.GetType().GetMethod("Remove");
					System.Reflection.MethodInfo methodContains = target.GetType().GetMethod("Contains");

					while (e.MoveNext() == true)
					{
						while ((bool) methodContains.Invoke(target, new System.Object[] {e.Current}) == true)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return true;
		}

		/// <summary>
		/// Retains the elements in the target collection that are contained in the specified collection
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to be retained in the target collection.</param>
		/// <returns>true</returns>
		public static bool RetainAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(target).GetEnumerator();
			System.Collections.ArrayList al = new System.Collections.ArrayList(c);

			//Reflection. Invoke "retainAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("retainAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {c});
				else
				{
					method = c.GetType().GetMethod("Remove");

					while (e.MoveNext() == true)
					{
						if (al.Contains(e.Current) == false)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		/// Returns an array containing all the elements of the collection.
		/// </summary>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c)
		{	
			int index = 0;
			System.Object[] objects = new System.Object[c.Count];
			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objects[index++] = e.Current;

			return objects;
		}

		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <param name="objects">The array into which the elements of the collection will be stored.</param>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c, System.Object[] objects)
		{	
			int index = 0;

			System.Type type = objects.GetType().GetElementType();
			System.Object[] objs = (System.Object[]) Array.CreateInstance(type, c.Count );

			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objs[index++] = e.Current;

			//If objects is smaller than c then do not return the new array in the parameter
			if (objects.Length >= c.Count)
				objs.CopyTo(objects, 0);

			return objs;
		}

		/// <summary>
		/// Converts an ICollection instance to an ArrayList instance.
		/// </summary>
		/// <param name="c">The ICollection instance to be converted.</param>
		/// <returns>An ArrayList instance in which its elements are the elements of the ICollection instance.</returns>
		public static System.Collections.ArrayList ToArrayList(System.Collections.ICollection c)
		{
			System.Collections.ArrayList tempArrayList = new System.Collections.ArrayList();
			System.Collections.IEnumerator tempEnumerator = c.GetEnumerator();
			while (tempEnumerator.MoveNext())
				tempArrayList.Add(tempEnumerator.Current);
			return tempArrayList;
		}

		public static System.Object CreateNewInstance(System.Type classType)
		{
			System.Reflection.ConstructorInfo[] constructors = classType.GetConstructors();

			if (constructors.Length == 0)
				return null;

			System.Reflection.ParameterInfo[] firstConstructor = constructors[0].GetParameters();
			int countParams = firstConstructor.Length;

			System.Type[] constructor = new System.Type[countParams];
			for( int i = 0; i < countParams; i++)
				constructor[i] = firstConstructor[i].ParameterType;

			return classType.GetConstructor(constructor).Invoke(new System.Object[]{});
		}

	
	
	}

}
