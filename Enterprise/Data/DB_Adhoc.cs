using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data;

namespace SHMA.Enterprise.Data
//	namespace SHMA.Enterprise.Shared
{
	public sealed class DB_Adhoc		
	{
        public static DataAccessType dataAccessType
		{
			get
			{
				return DataAccessType.OleDB;				
			}
		}


		public static DataBaseType dataBaseType 
		{
			get 
			{
				switch(database)
				{
					case "SQLServer":return DataBaseType.SQLServer;
					case "Oracle":return DataBaseType.Oracle;
					case "DB2":return DataBaseType.DB2;
					case "MySQL":return DataBaseType.MySQL;
					default:return 0;
				}

				//return DataBaseType.Oracle; 	
			}
		}

		//WHY is the following line, is it just for Java compatibility????
		private static readonly DB_Adhoc instance = new DB_Adhoc();   
		//
		private static string database = System.Configuration.ConfigurationSettings.AppSettings["Database_Adhoc"]==null? "SQLServer":System.Configuration.ConfigurationSettings.AppSettings["Database_Adhoc"];

		[ThreadStatic]
		private static IDbConnection objConnection;
		//private static System.String strConnectionstring = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=SA;Initial Catalog=RAPIDDB;Data Source=DBSERVER02"; //System.Configuration.ConfigurationSettings.AppSettings["DSN"];
        private static System.String strEncryptedConnStr = System.Configuration.ConfigurationSettings.AppSettings["ECS"]==null? "N":System.Configuration.ConfigurationSettings.AppSettings["ECS"];
		private static System.String strConnectionstring = strEncryptedConnStr=="Y"? Encryption64.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["DSN_ADHOC"],"1p8dMo5BlQ83okH3"):System.Configuration.ConfigurationSettings.AppSettings["DSN_ADHOC"];		
		private static int TimeOutValue = 30;
        private static int DeadlockRetryCount = 3;
        private static bool showErrQry = false;

		[ThreadStatic]
		private static IDbTransaction transaction; 
		private DB_Adhoc(){}

		public static void TransactionEnd()
		{
			transaction = null;
		}

		public static IDbConnection Connection
		{
			get
			{
				if (objConnection == null)
				{
					objConnection = getConnection();
				}
				else if (objConnection.State.Equals(System.Data.ConnectionState.Closed) )
				{
					objConnection.ConnectionString = strConnectionstring;
					objConnection.Open();
				}
				return objConnection;
			}
			set
			{
				objConnection = value;
			}
		}	

		public static IDbConnection AdhocConnection()
		{
			return getConnection();
		}

		public static IDbConnection AdhocConnection(string DSN)
		{
			return getConnection(DSN);
		}

		private static IDbConnection getConnection()
		{
			IDbConnection objConn;
			switch(dataAccessType)
			{
				case DataAccessType.Sql:
					objConn = new SqlConnection(strConnectionstring);        
					break;
				case DataAccessType.OleDB:
					objConn =new OleDbConnection(strConnectionstring);        
					break;
				default:
					objConn = new SqlConnection(strConnectionstring);        
					break;
			}
			objConn.Open();
			return objConn;
		}

		private static IDbConnection getConnection(String DSN)
		{
			string strConnectionstring2 = strEncryptedConnStr == "Y" ? Encryption64.Decrypt(System.Configuration.ConfigurationSettings.AppSettings[DSN], "1p8dMo5BlQ83okH3") : System.Configuration.ConfigurationSettings.AppSettings[DSN];
			if (strConnectionstring2 != null)
			{
				IDbConnection objConn;
				switch (dataAccessType)
				{
					case DataAccessType.Sql:
						objConn = new SqlConnection(strConnectionstring2);
						break;
					case DataAccessType.OleDB:
						objConn = new OleDbConnection(strConnectionstring2);
						break;
					default:
						objConn = new SqlConnection(strConnectionstring2);
						break;
				}
				objConn.Open();
				return objConn;
			}
			else 
			{

				return null;
			}
		}
	
//		public static System.Data.ConnectionState ConnectionState
//		{
//			get
//			{
//				return  objConnection.State; 
//			}
//		}

		public static bool isConnected()
		{

				   bool connectedFlag = false;
				   if (objConnection != null){
					   if (objConnection.State ==ConnectionState.Open){
						   connectedFlag = true;
					   }
				   }

				   return connectedFlag;
		}
		public static bool isInTransaction(){

			bool trnasactionFlag = false;
			if (transaction != null){
					trnasactionFlag = true;
			}

			return trnasactionFlag;
		}

		public static void Begin()
		{
		}

		public static void DisConnect()
		{
			if (objConnection != null)
			{
				if (objConnection.State == ConnectionState.Open)
				{
					objConnection.Close();
				}
			}
		}

		public static void End()
		{
			
		}
		public static IDbTransaction Transaction		
		{
			get
			{			
				if (transaction == null)
				{
					transaction = Connection.BeginTransaction();
				}
				return transaction;
			}
			set
			{
				transaction = value;
			}
		}	
		#region Added to make java compliant
		public static void BeginTransaction(){
			if (transaction == null) {
				transaction = Connection.BeginTransaction();
			}			
		}
		public static void CommitTransaction()
		{
			DB_Adhoc.Transaction.Commit();
		}
		public static void RollbackTransaction()
		{
			if (transaction !=null){
				DB_Adhoc.Transaction.Rollback();
			}
		}
 		public static void openConnection()
		{
			
		}
		public static void closeConnection()
		{
		}
		public static void executeDML(String query)
		{
            int result = 0;
            IDbCommand cmd = DB_Adhoc.CreateCommand(query);
			if(transaction !=null)
				cmd.Transaction = transaction;

			result = cmd.ExecuteNonQuery();

			
            cmd.Dispose();
            //return result;
		}

		public static void executeDML(String query, ParameterCollection parameters)
		{
            int result = 0;
            IDbCommand cmd = DB_Adhoc.CreateCommand(query,DB_Adhoc.Connection);
			if (transaction != null)
				cmd.Transaction = transaction; 
			cmd = setCommandParameters(cmd,parameters);

			result = cmd.ExecuteNonQuery();


            cmd.Dispose();
            //return result;
		}

		public static void executeDML(String query, ParameterCollection parameters, int timeout)
		{
            int result = 0;
            IDbCommand cmd = DB_Adhoc.CreateCommand(query,DB_Adhoc.Connection);
			if (transaction != null)
				cmd.Transaction = transaction; 
			cmd.CommandTimeout = timeout ;
			cmd = setCommandParameters(cmd,parameters);

            result = cmd.ExecuteNonQuery();


            cmd.Dispose();
            //return result;
		}

		//Execute parameterized queries with timeout of insert, update, delete on Adhoc Connection.
        public static void executeDML_Adhoc(String query, ParameterCollection parameters, int timeout)
        {
            int result = 0;
                
			IDbConnection objConnection = DB_Adhoc.AdhocConnection();
			try
			{
				   IDbCommand cmd = objConnection.CreateCommand();
				   cmd.CommandText = query;
				   if (parameters != null)
				   {
					   cmd = setCommandParameters(cmd, parameters);

				   }
				   if (timeout != -1)
				   {
					   cmd.CommandTimeout = timeout;
				   }
				   result = cmd.ExecuteNonQuery();
			   }
			finally
				{
					objConnection.Close();
				}
			//return result ;
		}

		public static void executeDML_Adhoc(String query, ParameterCollection parameters, int timeout, string DSN)
		{
			int result = 0;
                
			IDbConnection objConnection = DB_Adhoc.AdhocConnection(DSN);
			if (objConnection == null) 
			{
				throw new Exception("Connection string not defined for " + DSN);
			}

			try
			{
				IDbCommand cmd = objConnection.CreateCommand();
				cmd.CommandText = query;
				if (parameters != null)
				{
					cmd = setCommandParameters(cmd, parameters);

				}
				if (timeout != -1)
				{
					cmd.CommandTimeout = timeout;
				}
				result = cmd.ExecuteNonQuery();
			}
			finally
			{
				objConnection.Close();
			}
		}


        public static rowset executeQuery(String query)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataReader dataReader = cmd.ExecuteReader();			
			DataTable dataTable = new DataTable();
			SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
			dataReader.Close();
			cmd.Dispose();
			return(new rowset(dataTable));
		}

		public static rowset executeQuery(String query, int timeout)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			if (transaction != null)
				cmd.Transaction = transaction; 

			cmd.CommandTimeout = timeout ;
			IDataReader dataReader = cmd.ExecuteReader();			
			DataTable dataTable = new DataTable();
			SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
			dataReader.Close();
			cmd.Dispose();
			return(new rowset(dataTable));
		}

		public static rowset executeQuery(String query, ParameterCollection parameters)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return(new rowset(dataTable));
		}

		public static rowset executeQuery(String query, ParameterCollection parameters, int timeout)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			cmd.CommandTimeout = timeout ;
			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return(new rowset(dataTable));
		}

		public static rowset executeQuery_Adhoc(String query, ParameterCollection parameters, String DSN)
		{
			//IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			IDbConnection objConnection = DB_Adhoc.AdhocConnection(DSN);
			if (objConnection == null)
			{
				throw new Exception("Connection string not defined for " + DSN);
			}

			IDbCommand cmd = objConnection.CreateCommand();
			cmd.CommandText = query;
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return(new rowset(dataTable));
		}

		public static DataTable getDataTable_Adhoc(String query, ParameterCollection parameters, String DSN)
		{
			//IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			IDbConnection objConnection = DB_Adhoc.AdhocConnection(DSN);
			if (objConnection == null)
			{
				throw new Exception("Connection string not defined for " + DSN);
			}

			IDbCommand cmd = objConnection.CreateCommand();
			cmd.CommandText = query;
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return dataTable ;
		}

		public static DataTable getDataTable(String query)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataReader dataReader = cmd.ExecuteReader();			
			DataTable dataTable = new DataTable();
			SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
			dataReader.Close();
			cmd.Dispose();
			return dataTable;
		}

        public static DataTable getDataTable(String query, int timeout)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);

			if (transaction != null)
				cmd.Transaction = transaction; 

			cmd.CommandTimeout = timeout ;
			IDataReader dataReader = cmd.ExecuteReader();			
			DataTable dataTable = new DataTable();
			SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
			dataReader.Close();
			cmd.Dispose();
			return dataTable;
		}

        public static DataTable getDataTable(String query, ParameterCollection parameters)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return dataTable ;
		}

        public static DataTable getDataTable(String query, ParameterCollection parameters, int timeout)
		{
			IDbCommand cmd = DB_Adhoc.CreateCommand(query);
			cmd = setCommandParameters(cmd,parameters);
			DataSet ds = new DataSet();
			if (transaction != null)
				cmd.Transaction = transaction; 

			cmd.CommandTimeout = timeout ;
			IDataAdapter dataAdapter = DB_Adhoc.CreateDataAdapter(cmd);
			dataAdapter.Fill(ds);
			cmd.Dispose();
			
			DataTable dataTable = ds.Tables[0];
			return dataTable ;
		}


		public static IDbCommand setCommandParameters(IDbCommand pstatement, ParameterCollection Parameter) 
		{
			Object obj = new Object();
			int Counter=0;
			
			for(int counter=0; counter<Parameter.Count;counter++)
			{
				obj =Parameter.gets(Parameter.Keys[counter]);
				System.Data.DbType objType = Parameter.getType(Counter);
				Counter++;
				if(obj == null)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], objType, DBNull.Value));
				else
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], objType, Parameter.gets(Parameter.Keys[counter])));
				
/*				if  (obj is Int16)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], DbType.Int64,Parameter.gets(Parameter.Keys[counter])));
				if  (obj is Int32)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], DbType.Decimal,Parameter.gets(Parameter.Keys[counter])));
				else if(obj is Double)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], DbType.Double,Parameter.gets(Parameter.Keys[counter])));
				else if(obj is String)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], DbType.String,Parameter.gets(Parameter.Keys[counter])));
				else if(obj is DateTime)
					pstatement.Parameters.Add(DB_Adhoc.CreateParameter(Parameter.Keys[counter], DbType.DateTime,Parameter.gets(Parameter.Keys[counter])));
				else
					throw new Exception("Paramter Data Type Mismatch or Object is Null");*/
			}
			return(pstatement);
		}

		// End of java added functionality
#endregion
		#region Factory Methods
		public static IDbCommand CreateCommand()
		{
			IDbCommand cmd =DB_Adhoc.Connection.CreateCommand();
			if(transaction !=null)
				cmd.Transaction = transaction;
            cmd.CommandTimeout = TimeOutValue;
			return cmd;
		}
		public static IDbCommand CreateCommand(string cmdText)
		{
			IDbCommand cmd = DB_Adhoc.Connection.CreateCommand();
			if(transaction !=null)
				cmd.Transaction = transaction;
			cmd.CommandText = cmdText;
            cmd.CommandTimeout = TimeOutValue;
			return cmd;
		}
		public static IDbCommand CreateCommand(string cmdText, IDbConnection conn)
		{
			IDbCommand cmd = conn.CreateCommand();
			if(transaction != null)
				cmd.Transaction = transaction;
			cmd.CommandText = cmdText;
            cmd.CommandTimeout = TimeOutValue;
			return cmd;
		}

		
		public static System.Data.IDataParameter CreateParameter(string ParamName, object ParamValue){
			if (ParamValue is double || ParamValue is decimal )
				return CreateParameter(ParamName, DbType.Decimal, ParamValue);
			else if (ParamValue is DateTime)
				return CreateParameter(ParamName, DbType.DateTime, ParamValue);
			else
				return CreateParameter(ParamName, DbType.String, ParamValue);
		}
		
		public static System.Data.IDataParameter CreateParameter(string ParamName, DbType ParamType, object ParamValue){
			//TODO: Optimize this approach to have a faster decision but the runtime flexibilty
			// shouldn't be compromised. There should be such an approach that if a user modifies the DB class before proting
			// he gets a fastest run-time but if he leaves, he might have a flexible but slower run-time decsion -- just a dirty idea
			IDataParameter parameter = null;
			switch (dataAccessType)
			{
				case (DataAccessType.OleDB):
				{
					switch(ParamType)
					{
						case (DbType.String):
							parameter = new OleDbParameter(ParamName, OleDbType.VarChar);
							break;
						case (DbType.VarNumeric): 
							parameter = new OleDbParameter(ParamName, OleDbType.VarNumeric);
							break;
						case (DbType.Decimal): 
							parameter = new OleDbParameter(ParamName, OleDbType.Decimal);
							break;
						case (DbType.Double): 
							parameter = new OleDbParameter(ParamName, OleDbType.Double);
							break;
						case (DbType.DateTime): 
							parameter = new OleDbParameter(ParamName, OleDbType.Date);
							//******added to support SQL server 2008 Providor SQLNCLI10.1
							((OleDbParameter)parameter).Scale = 3;

							//******
							break;
						case (DbType.Date): 
							parameter = new OleDbParameter(ParamName, OleDbType.DBTimeStamp);
							//******added to support SQL server 2008 Providor SQLNCLI10.1
							((OleDbParameter)parameter).Scale = 3;
							//******
							break;
						default : 
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
					}		
					break;
				}
				case (DataAccessType.Sql):
				{
					switch(ParamType)
					{
						case (DbType.String): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.VarChar);
							break;
						}
						case (DbType.VarNumeric): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.Float);
							break;
						}
						case (DbType.DateTime): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.DateTime);
							break;
						}						
						default : 
						{
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
						}
					}					
				}
					break;
			}
			parameter.Value = ParamValue;
			return parameter;
		}
		public static System.Data.IDataParameter CreateParameter(string ParamName, DbType ParamType, int size, object ParamValue)
		{
			//TODO: Optimize this approach to have a faster decision but the runtime flexibilty
			// shouldn't be compromised. There should be such an approach that if a user modifies the DB class before proting
			// he gets a fastest run-time but if he leaves, he might have a flexible but slower run-time decsion -- just a dirty idea
			IDataParameter parameter= null;
			switch (dataAccessType)
			{
				case (DataAccessType.OleDB):
				{
					switch(ParamType)
					{
						case (DbType.String):
							parameter = new OleDbParameter(ParamName, OleDbType.VarChar, size);
							break;
						case (DbType.VarNumeric): 
							parameter = new OleDbParameter(ParamName, OleDbType.VarNumeric, size);
							break;
						case (DbType.DateTime): 
							parameter = new OleDbParameter(ParamName, OleDbType.Date, size);
							break;
						case (DbType.Double): 
							parameter = new OleDbParameter(ParamName, OleDbType.VarNumeric, size);
							break;
						case (DbType.Decimal): 
							parameter = new OleDbParameter(ParamName, OleDbType.Decimal, size, ParameterDirection.Input, true,15,0,"ChequeNo",DataRowVersion.Default,12);
							break;
						case (DbType.Int32):
							parameter = new OleDbParameter(ParamName, OleDbType.Integer, size);
							break;
						case (DbType.Int16):
							parameter = new OleDbParameter(ParamName, OleDbType.SmallInt, size);
							break;
						default : 
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
					}		
					break;
				}
				case (DataAccessType.Sql):
				{
					switch(ParamType)
					{
						case (DbType.String): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.VarChar, size);
							break;
						}
						case (DbType.VarNumeric): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.Float, size);
							break;
						}
						case (DbType.DateTime): 
						{
							parameter = new SqlParameter(ParamName, SqlDbType.DateTime, size);
							break;
						}						
						default : 
						{
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
						}
					}					
				}
					break;
			}
			parameter.Value = ParamValue;
			return parameter;
		}
		public static System.Data.IDataParameter CreateParameter(string ParamName, DbType ParamType, int size, byte precision, byte scale, object ParamValue)
		{
			//TODO: Optimize this approach to have a faster decision but the runtime flexibilty
			// shouldn't be compromised. There should be such an approach that if a user modifies the DB class before proting
			// he gets a fastest run-time but if he leaves, he might have a flexible but slower run-time decsion -- just a dirty idea
			IDataParameter parameter= null;			
			switch (dataAccessType)
			{
				case (DataAccessType.OleDB):
				{
					switch(ParamType)
					{
						case (DbType.VarNumeric): 
							OleDbParameter param1 = new OleDbParameter(ParamName, OleDbType.VarNumeric, size);
							param1.Precision= precision;
							param1.Scale= scale;
							parameter = param1;
							break;
						case (DbType.Double): 
							OleDbParameter param2 = new OleDbParameter(ParamName, OleDbType.Double, size);
							param2.Precision= precision;
							param2.Scale= scale;
							parameter = param2;
							break;
						case (DbType.Decimal): 
							OleDbParameter param3 = new OleDbParameter(ParamName, OleDbType.Decimal, size);
							param3.Precision= precision;
							param3.Scale= scale;
							parameter = param3;
							break;
						default : 
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
					}		
					break;
				}
				case (DataAccessType.Sql):
				{
					switch(ParamType)
					{
						case (DbType.VarNumeric): 
							SqlParameter param1 = new SqlParameter(ParamName, SqlDbType.Variant , size);
							param1.Precision = precision;
							param1.Scale= scale;
							parameter = param1;
							break;
						case (DbType.Double): 
							SqlParameter param2 = new SqlParameter(ParamName, SqlDbType.Variant , size);
							param2.Precision= precision;
							param2.Scale= scale;
							parameter = param2;
							break;
						case (DbType.Decimal): 
							SqlParameter param3 = new SqlParameter(ParamName, SqlDbType.Variant , size);
							param3.Precision= precision;
							param3.Scale= scale;
							parameter = param3;
							break;				
						default : 
						{
							throw (new ApplicationException("Unsupported Parameter Type Requested in Parameter Factory"));
						}
					}					
					break;
				}
					//break;
			}
			parameter.Value = ParamValue;
			return parameter;
		}

		public static DbDataAdapter CreateDataAdapter(IDbCommand cmd)
		{
			if (dataAccessType == DataAccessType.OleDB)
				return new OleDbDataAdapter((OleDbCommand) cmd);				
			else
				return new SqlDataAdapter((SqlCommand)cmd);			
		}

		public static DbDataAdapter CreateDataAdapter(string strTax)
		{
			if (dataAccessType == DataAccessType.OleDB)
				return new OleDbDataAdapter(new OleDbCommand(strTax, (OleDbConnection)Connection));				
			else
				return new SqlDataAdapter(new SqlCommand(strTax, (SqlConnection)Connection));			
		}

		//		public static IDataAdapter CreateDataAdapter(IDbCommand cmd){
		//			if (dataAccessType == DataAccessType.OleDB)
		//				return new OleDbDataAdapter((OleDbCommand)cmd);				
		//			else
		//				return new SqlDataAdapter((SqlCommand)cmd);			
		//		}

		//		public static OleDbDataReader ExecuteOleDBReader(string query, OleDbConnection conn){
		//			return new OleDbCommand(query, conn).ExecuteReader();
		//		}
		//
		//		public static OleDbDataReader ExecuteOleDBReader(string query){
		//			return new OleDbCommand(query, DB_Adhoc.Connection).ExecuteReader();
		//		}
		#endregion

		public enum DataAccessType
		{
			OleDB = 0,
			Sql = 1,
			DB2=2
		}
		public enum DataBaseType {
			SQLServer = 0,
			Oracle = 1,
			DB2 = 2,
			MySQL = 3
		}
		~DB_Adhoc()
		{
		}
	}
}
