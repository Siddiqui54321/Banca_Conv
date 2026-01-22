using System;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Data; 
namespace shgn
{
	/// <summary> 
	/// <p>Title: Manipulate Data</p>
	/// <p>Description: All the Data accessing Logic is Written in this class </p>
	/// <p>Copyright: Copyright (c) 2002</p>
	/// <p>Company: Sidat Hyder</p>
	/// </summary>
	/// <author>  Syed Farrukh Hashmi
	/// </author>
	/// <version>  1.0
	/// </version>
	public class SHGNDataAccess
	{
		public static DB SHGNConnectionPool = null;
		public System.Data.OleDb.OleDbConnection conn = null; //, cn=null;
		public static System.String ip;
		public static System.String port;
		public static System.String instance;
		public static System.String user;
		public static System.String password;
		private static System.String str_Database;
		public static int database;
		public const int ORACLE = 1;
		public const int DB2 = 2;
		public const int SQLSERVER = 3;
		public long errorCode = 0;
		public virtual System.String fsgetErrorDescription()
		{
			System.String str_CodeCol = "";
			if (database == SHGNDataAccess.DB2)
				str_CodeCol = "PEM_ERMSDB2CODE";
			else if (database == SHGNDataAccess.ORACLE)
				str_CodeCol = "PEM_ERMSORACODE";
			else if (database == SHGNDataAccess.SQLSERVER)
				str_CodeCol = "PEM_ERMSSQLCODE";
			try
			{
				System.String str_Result = this.fsgetCol("PR_GN_EM_ERRORMESSAGE", "PEM_ERMSDESC", str_CodeCol, System.Convert.ToString(this.errorCode));
				return ((System.Object) str_Result == null || str_Result.Equals("")?"Undefined Error":str_Result);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				return e.Message;
			}
		}
		internal static void  setDataBase()
		{
			//if (str_Database.ToLower().Equals("oracle"))
			if (DB.DataAccessType.OleDB == DB.dataAccessType)
				database = ORACLE;
			//else if (str_Database.ToLower().Equals("db2"))
			else if (DB.DataAccessType.DB2==DB.dataAccessType)
				database = DB2;
			//else if (str_Database.ToLower().Equals("sqlserver"))
			else if (DB.DataAccessType.Sql==DB.dataAccessType)
				database = SQLSERVER;
		}
		public SHGNDataAccess()
		{
			/*
			try{
			conn=DB.Connection;  
			}
			catch(SQLException e)
			{
			System.out.println("Error during Operation "+e.Message);
			}*/
		}
		
		/// <summary> <br> To directly server a Connection.</summary>
		/// <returns> Connection
		/// </returns>
		public virtual System.Data.OleDb.OleDbConnection fsgetConnection()
		{
			try
			{
				conn =(System.Data.OleDb.OleDbConnection) DB.AdhocConnection(); // fsgetConnection();
			}
			catch (System.Data.OleDb.OleDbException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine("Error during Operation " + e.Message);
				//SupportClass.WriteStackTrace(e, Console.Error);
			}
			return conn;
		}
		public virtual System.Data.OleDb.OleDbConnection fsgetConnection(int S) {
			try {
				conn =(System.Data.OleDb.OleDbConnection) DB.Connection; // fsgetConnection();
			}
			catch (System.Data.OleDb.OleDbException e) {
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine("Error during Operation " + e.Message);
				//SupportClass.WriteStackTrace(e, Console.Error);
			}
			return conn;
		}
		public virtual System.Data.OleDb.OleDbTransaction fsgetTransaction(){
				   return (System.Data.OleDb.OleDbTransaction)DB.Transaction;
		}
		/// <summary> <br> To directly server a New Connection.</summary>
		/// <returns> Connection
		/// </returns>
		public virtual System.Data.OleDb.OleDbConnection fsgetNewConnection()
		{
			try
			{
				conn =(System.Data.OleDb.OleDbConnection) DB.Connection; 
			}
			catch (System.Data.OleDb.OleDbException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine("Error during Operation " + e.Message);
			}
			return conn;
		}
		/// <summary> <br> To return a New Connection.</summary>
		/// <returns> void
		/// </returns>
		public virtual void  fsreturnNewConnection(System.Data.OleDb.OleDbConnection conn)
		{
			try
			{
			//	SHGNConnectionPool.fsreturnNewConnection(conn);
			}
			catch (System.Data.OleDb.OleDbException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine("Error during Operation " + e.Message);
			}
		}
		
		/// <summary> <br> Delete Record with Number of Condition Cols.</summary>
		/// <param name="">tName
		/// </param>
		/// <param name="">cols
		/// </param>
		/// <param name="">values
		/// </param>
		/// <returns> int
		/// </returns>
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values)
		{
			
			System.String strQry = "Delete From " + tName + " where ";
			
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			
			return fsexecuteUpdate(strQry, 1);
		}
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values, System.String[] cols1, long[] values1)
		{
			
			System.String strQry = "Delete From " + tName + " where ";
			
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			for (int i = 0; i < cols1.Length; i++)
			{
				strQry += (cols1[i] + " = " + values1[i] + " AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			
			return fsexecuteUpdate(strQry, 1);
		}
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values, System.String[] cols1, long[] values1, System.Data.OleDb.OleDbConnection cn)
		{
			
			System.String strQry = "Delete From " + tName + " where ";
			
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			for (int i = 0; i < cols1.Length; i++)
			{
				strQry += (cols1[i] + " = " + values1[i] + " AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			
			return fsexecuteUpdate(strQry, 1, cn);
		}
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values, System.String[] cols1, System.String[] values1)
		{
			
			System.String strQry = "Delete From " + tName + " where ";
			
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			for (int i = 0; i < cols1.Length; i++)
			{
				strQry += (cols1[i] + " = " + values1[i] + " AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			
			return fsexecuteUpdate(strQry, 1);
		}
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values, System.String[] cols1, System.String[] values1, System.Data.OleDb.OleDbConnection cn)
		{
			
			System.String strQry = "Delete From " + tName + " where ";
			
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			for (int i = 0; i < cols1.Length; i++)
			{
				strQry += (cols1[i] + " = " + values1[i] + " AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			
			return fsexecuteUpdate(strQry, 1, cn);
		}
		
		/// <summary><br> Delete Record with Number of Condition Cols., it take Connection as argument.</summary>
		/*		for (int i=0;i<cols.length;i++)
		{
		strQry	+=	cols[i]+" = '"+values[i] +"' AND ";
		}*/
		public virtual int fsdelete(System.String tName, System.String[] cols, System.String[] values, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Delete From " + tName + " where ";
			for (int i = 0; i < cols.Length; i++)
			{
				strQry += (cols[i] + " = '" + values[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			return fsexecuteUpdate(strQry, 1, cn);
		}
		
		/// <summary><br> Update Record with Number of Condition Cols. return int</summary>
		
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, int returnType)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			//System.out.println("Query is: "+strQry);
			return fsexecuteUpdate(strQry, 1);
		}
		
		/// <summary><br> Update Record with Number of Condition Cols. return int, it take Connection as argument.</summary>
		
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, int returnType, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry, 1, cn);
		}
		
		
		/// <summary><br> Update Record with Number of Condition Cols. return int, it take Connection as argument.
		/// <br> Not enclose values in 'single quote'
		/// </summary>
		
		public virtual int fsupdateVch(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, System.String extraConCol, System.String extraConColVal, int returnType, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "=" + cValue[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			strQry += (" AND " + extraConCol + " > '" + extraConColVal + "'");
			//System.out.println(strQry);
			
			return fsexecuteUpdate(strQry, 1, cn);
		}
		
		/// <summary><br> Update Record with Number of Condition Cols. return int,  <br> Not enclose values in 'single quote'</summary>
		
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, System.String extraConCol, System.String extraConColVal, int returnType)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "=" + cValue[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			strQry += (" AND " + extraConCol + " > '" + extraConColVal + "'");
			//System.out.println(strQry);
			
			return fsexecuteUpdate(strQry, 1);
		}
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, System.String[] ncondColumn, System.String[] ncondition, System.String extraConCol, System.String extraConColVal, int returnType)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "=" + cValue[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			for (int i = 0; i < ncondition.Length; i++)
			{
				cols += (ncondColumn[i] + "=" + ncondition[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			strQry += (" AND " + extraConCol + " > " + extraConColVal + "");
			//System.out.println(strQry);
			
			return fsexecuteUpdate(strQry, 1);
		}
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition, System.String[] ncondColumn, System.String[] ncondition, System.String extraConCol, System.String extraConColVal, int returnType, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "=" + cValue[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			for (int i = 0; i < ncondition.Length; i++)
			{
				cols += (ncondColumn[i] + "=" + ncondition[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			strQry += (" AND " + extraConCol + " > " + extraConColVal + "");
			//System.out.println(strQry);
			
			return fsexecuteUpdate(strQry, 1, cn);
		}
		
		/// <summary><br> Update Record with Number of Condition Cols. return boolean</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] condColumn, System.String[] condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condition.Length; i++)
			{
				cols += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Delete Record from Table by a where String Condition. </summary>
		public virtual bool fsdelete(System.String tName, System.String cName, System.String cValue)
		{
			System.String strQry = "Delete from " + tName + " where " + cName + "='" + cValue + "'";
			return fsexecuteUpdate(strQry);
		}
		
		/// <summary><br> Delete Record from Table by a where String Condition return int. </summary>
		public virtual int fsdelete(System.String tName, System.String cName, System.String cValue, int i)
		{
			System.String strQry = "Delete from " + tName + " where " + cName + "='" + cValue + "'";
			return fsexecuteUpdate(strQry, 1);
		}
		
		
		/// <summary><br> Serve the internal methods by executing updateable queries. </summary>
		
		public virtual bool fsexecuteUpdate(System.String strQry)
		{
			long l = 0;
			System.Data.OleDb.OleDbCommand stmt = null;
			strQry = removeNull(strQry);
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
			//System.out.println("\n\n"+strQry);
		//	try
		//	{
				//Connection conn=ConnectionManager.fsgetConnection();
				conn =(System.Data.OleDb.OleDbConnection) DB.Connection;  
				stmt = SupportClass.TransactionManager.manager.CreateStatement(conn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = strQry;
				l = temp_OleDbCommand.ExecuteNonQuery();
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//stmt.close()();
				//SHGNConnectionPool.fsreturnConnection(conn);(conn);
				errorCode = 0;
		/*	}
			catch (System.Data.OleDb.OleDbException e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				System.Console.Out.WriteLine("Error code is: " + e.ErrorCode);
				errorCode = e.ErrorCode;
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			finally
			{
				returnConnection(conn);
			}*/
			return (l > 0);
		}
		internal virtual System.String fsprocessDateInQuery(System.String str_Qry)
		{
			return replace(str_Qry, "'__null__'", "null");
		}
		internal virtual System.String removeNull(System.String str_Qry)
		{
			//System.out.println(replace(str_Qry,"=''","=null"));
			return replace(str_Qry, "='',", "=null,");
		}
		
		/// <summary><br> Serve the internal methods by executing updateable queries, it take connection as argument. </summary>
		
		public virtual bool fsexecuteUpdate(System.String strQry, System.Data.OleDb.OleDbConnection cn)
		{
			long l = 0;
			System.Data.OleDb.OleDbCommand stmt = null;
			strQry = removeNull(strQry);
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
		//	try
		//	{
				stmt = SupportClass.TransactionManager.manager.CreateStatement(cn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				l = temp_OleDbCommand.ExecuteNonQuery();
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				////stmt.close()()();
				errorCode = 0;
		/*	}
			catch (System.Data.OleDb.OleDbException e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				System.Console.Out.WriteLine("Error code is: " + e.ErrorCode);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
				errorCode = e.ErrorCode;
			}*/
			return (l > 0);
		}
		
		public virtual int fsexecuteUpdate(System.String strQry, int i)
		{
			long l = 0;
			System.Data.OleDb.OleDbCommand stmt = null;
			strQry = removeNull(strQry);
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
		//	try
		//	{
				conn = (System.Data.OleDb.OleDbConnection) DB.Connection;  
				stmt = SupportClass.TransactionManager.manager.CreateStatement(conn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				l = temp_OleDbCommand.ExecuteNonQuery();
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				////stmt.close()()();
				////SHGNConnectionPool.fsreturnConnection(conn);(conn);
				errorCode = 0;
		/*	}
			catch (System.Data.OleDb.OleDbException e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				errorCode = e.ErrorCode;
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
				return - 1;
			}
			finally
			{
				returnConnection(conn);
			}*/
			return (int) l;
		}
		
		public virtual int fsexecuteUpdate(System.String strQry, int i, System.Data.OleDb.OleDbConnection cn)
		{
			strQry = removeNull(strQry);
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
			long l = 0;
			System.Data.OleDb.OleDbCommand stmt = null;
		//	try
		//	{
				stmt = SupportClass.TransactionManager.manager.CreateStatement(cn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				l = temp_OleDbCommand.ExecuteNonQuery();
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//stmt.close()();
				errorCode = 0;
		/*	}
			catch (System.Data.OleDb.OleDbException e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				errorCode = e.ErrorCode;
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
				return - 1;
			}*/
			return (int) l;
		}
		// To Remove Comments from Query
		internal virtual System.String removeComments(System.String strQry)
		{
			while (strQry.IndexOf("--") != - 1)
				strQry = replace(strQry, "--", "-");
			return strQry;
		}
		
		/// <summary><br> Returns the Prepared Statement. </summary>
		public virtual System.Data.OleDb.OleDbCommand getPreparedStatment(System.String str_Qry)
		{
		//	try
		//	{
				conn = (System.Data.OleDb.OleDbConnection)DB.Connection;
				System.Data.OleDb.OleDbCommand ps_Result = SupportClass.TransactionManager.manager.PrepareStatement(conn, str_Qry);
				//SHGNConnectionPool.fsreturnConnection(conn);(conn);
				return ps_Result;
		/*	}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			finally
			{
				//SHGNConnectionPool.fsreturnConnection(conn);(conn);
			}*/
			
			//return null;
		}
		/// <summary><br> Serve the internal method by executing select queries. </summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsexecuteQueryWithScrolling(System.String strQry)
		{
			System.Data.OleDb.OleDbCommand stmt = null;
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader rset = null;
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
			//System.out.println(strQry);
		//	try
		//	{
				conn = (System.Data.OleDb.OleDbConnection)DB.Connection;  
				//UPGRADE_TODO: Method 'java.sql.Connection.createStatement' was converted to 'System.Data.OleDb.OleDbConnection.CreateStatement()' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073"'
				stmt = conn.CreateCommand();
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				rset = temp_OleDbCommand.ExecuteReader();
				//SHGNConnectionPool.fsreturnConnection(conn);(conn);
		/*	}
			catch (System.Exception e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			finally
			{
				returnConnection(conn);
			}*/
			return rset;
		}
		
		/// <summary><br> Serve the internal method by executing select queries. </summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsexecuteQuery(System.String strQry)
		{
			System.Data.OleDb.OleDbCommand stmt = null;
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader rset = null;
			strQry = removeComments(strQry);
			strQry = fsprocessDateInQuery(strQry);
			System.Console.Out.WriteLine(strQry);
		//	try
		//	{
				conn = (System.Data.OleDb.OleDbConnection)DB.Connection;  
				stmt = SupportClass.TransactionManager.manager.CreateStatement(conn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				rset = temp_OleDbCommand.ExecuteReader();
				//SHGNConnectionPool.fsreturnConnection(conn);(conn);
		/*	}
			catch (System.Exception e)
			{	
				System.Console.Out.WriteLine("\n\n" + strQry);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			finally
			{
				returnConnection(conn);
			}*/
			return rset;
		}
		internal virtual void  returnConnection(System.Data.OleDb.OleDbConnection conn)
		{
			//SHGNConnectionPool.fsreturnConnection(conn);(conn);
		}
		/// <summary><br> Serve the internal method by executing select queries., take Connection as argument. </summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsexecuteQuery(System.String strQry, System.Data.OleDb.OleDbConnection cn)
		{
			//System.out.println("Query Before: "+strQry);
			//System.out.println("Query After: "+fsprcoessQry(strQry));
			//if (true)
			//  return null;
			System.Data.OleDb.OleDbCommand stmt = null;
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader rset = null;
			strQry = fsprocessDateInQuery(strQry);
			System.Console.Out.WriteLine(strQry);
		/*	try
			{
				stmt = SupportClass.TransactionManager.manager.CreateStatement(cn);
				System.Data.OleDb.OleDbCommand temp_OleDbCommand;
				temp_OleDbCommand = stmt;
				temp_OleDbCommand.CommandText = fsprcoessQry(strQry);
				rset = temp_OleDbCommand.ExecuteReader();
			}
			catch (System.Exception e)
			{
				System.Console.Out.WriteLine("\n\n" + strQry);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}*/
			return rset;
		}
		
		/// <summary><br> Change the Database specific function accroding to current database. </summary>
		public virtual System.String fsprcoessQry(System.String str_Qry)
		{
			if (SHGNDataAccess.database == SHGNDataAccess.ORACLE)
				return str_Qry;
			else if (SHGNDataAccess.database == SHGNDataAccess.DB2)
				return processDB2Qry(str_Qry);
			else if (SHGNDataAccess.database == SHGNDataAccess.SQLSERVER)
				return processSQLServerQry(str_Qry);
			return str_Qry;
		}
		public virtual System.String processSQLServerQry(System.String str_Qry)
		{
			str_Qry = replace(str_Qry, "NVL(", "ISNULL(");
			//str_Qry=processDB2Date(str_Qry);
			return str_Qry;
		}
		public virtual System.String processDB2Qry(System.String str_Qry)
		{
			str_Qry = replace(str_Qry, "NVL(", "ISNULL(");
			str_Qry = processDB2Date(str_Qry);
			//System.out.println("Qry is: "+str_Qry);
			return str_Qry;
		}
		public virtual System.String processDB2Date(System.String str_Qry)
		{
			try
			{
				//str_Qry=processDB2Date(str_Qry);
				int got = str_Qry.ToUpper().IndexOf("TO_DATE(");
				if (got == - 1)
					return str_Qry;
				System.String str_Item = "", str_SubItem = "";
				int length = 0;
				System.Text.StringBuilder sb = null;
				while (got != - 1)
				{
					//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
					str_Item = str_Qry.Substring(got, (str_Qry.IndexOf(")", got)) - (got));
					length = str_Item.Length;
					str_SubItem = str_Item.Substring(str_Item.IndexOf("("), (str_Item.IndexOf(",")) - (str_Item.IndexOf("(")));
					sb = new System.Text.StringBuilder(str_Qry);
					sb.Replace(sb.ToString(got, got + length - got), "DATE" + str_SubItem, got, got + length - got);
					str_Qry = sb.ToString();
					//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
					got = str_Qry.ToUpper().IndexOf("TO_DATE(", got + 1);
				}
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
				//SupportClass.WriteStackTrace(e, Console.Error);
			}
			return str_Qry;
		}
		public virtual System.String replace(System.String s, System.String f, System.String r)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(s);
			int got = sb.ToString().ToUpper().IndexOf(f.ToUpper());
			while (got != - 1)
			{
				sb.Replace(sb.ToString(got, got + f.Length - got), r, got, got + f.Length - got);
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				got = sb.ToString().ToUpper().IndexOf(f.ToUpper(), got + 1);
			}
			return sb.ToString();
		}
		
		/// <summary><br> Delete Record from Table by a where Numeric Condition. </summary>
		
		public virtual bool fsdelete(System.String tName, System.String cName, long cValue)
		{
			System.String strQry = "Delete from " + tName + " where " + cName + "=" + cValue;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Inform the user where given Condition values areexists in columns of table or not. </summary>
		
		/*	public boolean fsisExists(String tName, String condColumn[], String condition[])
		{
		boolean result=false;
		
		String strQry="Select * from "+tName+" where ";
		
		for (int i=0;i<condition.length;i++)
		{
		strQry += condColumn[i]+"='"+condition[i]+"' AND ";
		}
		strQry 	=  strQry.substring(0,strQry.length()-4);
		System.out.println("Query is: "+strQry);
		try{
		ResultSet rset = fsexecuteQuery(strQry);
		
		if (rset!=null)
		{
		if (rset.next())
		result = true;
		}
		//rset.getStatement().close();
		rset.close();
		
		}
		catch(Exception e)
		{
		System.out.println(e.Message);
		}
		return result;
		}*/
		/// <summary><br> Inform the user where given (String) value is exists in column of table or not. </summary>
		
		public virtual bool fsisExists(System.String tName, System.String cName, System.String cValue)
		{
			bool result = false;
			
			System.String strQry = "Select * from " + tName + " where " + cName + "='" + cValue + "'";
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Inform the user where given (String) value is exists in column of table or not. </summary>
		
		public virtual bool fsisExists(System.String strQry)
		{
			bool result = false;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Inform the user where given (String) value is exists in column of table or not. </summary>
		
		public virtual bool fsisExists(System.String tName, System.String[] cName, System.String[] cValue)
		{
			bool result = false;
			
			System.String strQry = "Select * from " + tName + " where ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				strQry += (cName[i] + "='" + cValue[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				//System.out.println("Query is: "+strQry);
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Inform the user where given (String) value is exists in column of table or not. </summary>
		
		public virtual bool fsisExists(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cNameNotEqual, System.String[] cValueNotEqual)
		{
			bool result = false;
			
			System.String strQry = "Select * from " + tName + " where ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				strQry += (cName[i] + "='" + cValue[i] + "' AND ");
			}
			for (int i = 0; i < cNameNotEqual.Length; i++)
			{
				strQry += (cNameNotEqual[i] + "<>'" + cValueNotEqual[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				//System.out.println("Query is: "+strQry);
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Inform the user where given (String) value is exists in column of table or not. (Two Values) </summary>
		
		public virtual bool fsisExists(System.String tName, System.String cName1, System.String cValue1, System.String cName2, System.String cValue2)
		{
			bool result = false;
			
			System.String strQry = "Select * from " + tName + " where " + cName1 + "='" + cValue1 + "'";
			strQry += (" AND " + cName2 + "='" + cValue2 + "'");
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Inform the user where given (Numeric) value is exists in column of table or not. </summary>
		
		public virtual bool fsisExists(System.String tName, System.String cName, long cValue)
		{
			bool result = false;
			System.String strQry = "Select * from " + tName + " where " + cName + "=" + cValue;
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = true;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return No. of Records in given Resultset.</summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual int fsrecordCount(System.Data.OleDb.OleDbDataReader rset)
		{
			int result = 0;
			try
			{
				if (rset != null)
				{
					while (rset.Read())
						result++;
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return No. of Columns in given Resultset.</summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual int fscolCount(System.Data.OleDb.OleDbDataReader rset)
		{
			int result = 0;
			try
			{
				result = rset.GetSchemaTable().Rows.Count;
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return the value of required column, where given column has given (String) value.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String cName, System.String cValue)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "='" + cValue + "' order by " + rcName;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		/// <summary><br> Return the value of required column, where given column has given (String) value.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String cName, System.String cValue, System.String cName1, System.String cValue1)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "='" + cValue + "' AND " + cName1 + "=" + cValue1 + " order by " + rcName;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		/// <summary><br> Return the value of required column, where given column has given (String) value.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String cName, System.String cValue, System.String cName1, System.String cValue1, System.Data.OleDb.OleDbConnection cn)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "='" + cValue + "' AND " + cName1 + "=" + cValue1 + " order by " + rcName;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry, cn);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		/// <summary><br> Return the value of required column, against the given query.</summary>
		
		public virtual System.String fsgetColumnAgainstQuery(System.String strQry)
		{
			System.String result = null;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Return the array value of required column, where given columns has given (String) values.</summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName, System.String[] condColumn, System.String[] condition)
		{
			System.String[] result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where ";
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				int ind = 0;
				if (rset != null)
				{
					while (rset.Read())
						result[ind++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		/// <summary><br> Return the Resultset of required columns, where given columns has given (String) values.</summary>
		
		public virtual System.String[][] fsgetCol(System.String tName, System.String[] rcName, System.String[] condColumn, System.String[] condition)
		{
			System.String strQry = "Select ";
			System.String[][] result = null; int counter = 0, colCount = 0;
			for (int i = 0; i < rcName.Length; i++)
			{
				strQry += (rcName[i] + ",");
			}
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			
			strQry += (" from " + tName + " where ");
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
					//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
					//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
					//rset.getStatement().close();
					rset.Close();
				}
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetCols(System.String tName, System.String[] rcName, System.String[] condColumn, System.String[] condition)
		{
			System.String strQry = "Select ";
			
			for (int i = 0; i < rcName.Length; i++)
			{
				strQry += (rcName[i] + ",");
			}
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			
			strQry += (" from " + tName + " where ");
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				return rset;
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return null;
		}
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetCols(System.String tName, System.String[] rcName, System.String[] condColumn, System.String[] condition, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Select ";
			
			for (int i = 0; i < rcName.Length; i++)
			{
				strQry += (rcName[i] + ",");
			}
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			
			strQry += (" from " + tName + " where ");
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry, cn);
				return rset;
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return null;
		}
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String[] condColumn, System.String[] condition, System.Data.OleDb.OleDbConnection cn)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where ";
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry, cn);
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String[] condColumn, System.String[] condition)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where ";
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Return the value of required column, where given column has given (Numeric) value.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String cName, long cValue)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "=" + cValue;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Return the value of required column.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return Result of Query given.</summary>
		
		public virtual System.String[][] fsgetCols(System.String query, int noOfCols)
		{
			System.String[][] result = null; int counter = 0;
			
		//	try
		//	{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(query);
				result = new System.String[fsrecordCount(rset)][];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = new System.String[noOfCols];
				}
				rset.Close();
				rset = fsexecuteQuery(query);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < noOfCols; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
		/*	}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}*/
			return result;
		}
		
		/// <summary><br> Return Result of Query given.</summary>
		
		public virtual System.String[][] fsgetCols(System.String query, int noOfCols, System.Data.OleDb.OleDbConnection cn)
		{
			System.String[][] result = null; int counter = 0;
			
		//	try
		//	{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(query, cn);
				result = new System.String[fsrecordCount(rset)][];
				for (int i = 0; i < fsrecordCount(rset); i++)
				{
					result[i] = new System.String[noOfCols];
				}
				rset = fsexecuteQuery(query, cn);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < noOfCols; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
		//	}
		/*	catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}*/
			return result;
		}
		
		/// <summary><br> Return ResultSet of Query given, with an order </summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetColsSel(System.String query, System.String order)
		{
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader result = null;
			//int counter = 0;
			
		//	try
		//	{
				result = fsexecuteQuery(query + " order by " + order);
		/*	}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}*/
			return result;
		}
		
		/// <summary><br> Return required column, where given column has given (String) value.</summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName, System.String cName, System.String cValue)
		{
			System.String[] result = null; int counter = 0;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "='" + cValue + "'";
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
						result[counter++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Return required column, where given column has given (Numeric) value.</summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName, System.String cName, long cValue)
		{
			System.String[] result = null; int counter = 0;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + "=" + cValue;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
						result[counter++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return required column from the Table. </summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName)
		{
			System.String[] result = null; int counter = 0;
			System.String strQry = "Select " + rcName + " from " + tName + " order by " + rcName;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
						result[counter++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Retrun One or More Column from required table by a where (String) clause. </summary>
		
		public virtual System.String[][] fsgetCols(System.String tName, System.String[] rcName, System.String cName, System.String cValue)
		{
			System.String[][] result = null; int counter = 0, colCount = 0;
			System.String strQry = "Select ";
			for (int i = 0; i < rcName.Length; i++)
				strQry += (rcName[i] + ",");
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			strQry += (" from " + tName + " where " + cName + "='" + cValue + "'");
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
					//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
					//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
					//rset.getStatement().close();
					rset.Close();
				}
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Retrun One or More Column from required table by a where (Numeric) clause.</summary>
		
		public virtual System.String[][] fsgetCols(System.String tName, System.String[] rcName, System.String cName, long cValue)
		{
			System.String[][] result = null; int counter = 0, colCount = 0;
			System.String strQry = "Select ";
			for (int i = 0; i < rcName.Length; i++)
				strQry += (rcName[i] + ",");
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			strQry += (" from " + tName + " where " + cName + "=" + cValue);
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
					//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
					//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
					//rset.getStatement().close();
					rset.Close();
				}
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Insert String & Date values in a table in given fields. While it take Connectin as argument</summary>
		
		public virtual bool fsinsert(System.String tName, System.String[] cName, System.String[] cValue, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + ", ");
				values += (cValue[i] + "', '");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			values = values.Substring(0, (values.Length - 3) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			return fsexecuteUpdate(strQry, cn);
		}
		
		/// <summary><br> Insert String & Date values in a table in given fields.</summary>
		
		public virtual bool fsinsert(System.String tName, System.String[] cName, System.String[] cValue)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + ", ");
				values += (cValue[i] + "', '");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			values = values.Substring(0, (values.Length - 3) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Insert Numeric, String & Date values in a table in given fields.</summary>
		public virtual bool fsinsert(System.String tName, System.String[] cName1, System.String[] cValue1, System.String[] cName2, long[] cValue2)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName1.Length; i++)
			{
				cols += (cName1[i] + ", ");
				values += (cValue1[i] + "', '");
			}
			values = values.Substring(0, (values.Length - 2) - (0));
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + ", ");
				values += (cValue2[i] + ", ");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			if (values.EndsWith(", "))
				values = values.Substring(0, (values.Length - 2) - (0));
			if (values.EndsWith(","))
				values = values.Substring(0, (values.Length - 1) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			
			return fsexecuteUpdate(strQry);
		}
		/// <summary><br> Insert Numeric, String & Date values in a table in given fields.</summary>
		public virtual bool fsinsert(System.String tName, System.String[] cName1, System.String[] cValue1, System.String[] cName2, System.String[] cValue2)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName1.Length; i++)
			{
				cols += (cName1[i] + ", ");
				values += (cValue1[i] + "', '");
			}
			values = values.Substring(0, (values.Length - 2) - (0));
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + ", ");
				values += (cValue2[i] + ", ");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			if (values.EndsWith(", "))
				values = values.Substring(0, (values.Length - 2) - (0));
			if (values.EndsWith(","))
				values = values.Substring(0, (values.Length - 1) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			
			return fsexecuteUpdate(strQry);
		}
		/// <summary><br> Insert Numeric, String & Date values in a table in given fields, with a connection for Transaction Support.</summary>
		public virtual bool fsinsert(System.String tName, System.String[] cName1, System.String[] cValue1, System.String[] cName2, long[] cValue2, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName1.Length; i++)
			{
				cols += (cName1[i] + ", ");
				values += (cValue1[i] + "', '");
			}
			values = values.Substring(0, (values.Length - 2) - (0));
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + ", ");
				values += (cValue2[i] + ", ");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			if (values.EndsWith(", "))
				values = values.Substring(0, (values.Length - 2) - (0));
			if (values.EndsWith(","))
				values = values.Substring(0, (values.Length - 1) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			
			return fsexecuteUpdate(strQry, cn);
		}
		public virtual bool fsinsert(System.String tName, System.String[] cName1, System.String[] cValue1, System.String[] cName2, System.String[] cValue2, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName1.Length; i++)
			{
				cols += (cName1[i] + ", ");
				values += (cValue1[i] + "', '");
			}
			values = values.Substring(0, (values.Length - 2) - (0));
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + ", ");
				values += (cValue2[i] + ", ");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			if (values.EndsWith(", "))
				values = values.Substring(0, (values.Length - 2) - (0));
			if (values.EndsWith(","))
				values = values.Substring(0, (values.Length - 1) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			System.Console.Out.WriteLine("Insert Query is: " + strQry);
			
			return fsexecuteUpdate(strQry, cn);
		}
		
		/// <summary><br> Insert Data in All Column of Table.</summary>
		
		public virtual bool fsinsert(System.String tName, System.String values)
		{
			
			values = "Insert into " + tName + " Values (" + values + ")";
			System.Console.Out.WriteLine("Insert Query is: " + values);
			return fsexecuteUpdate(values);
		}
		
		/// <summary><br> Convert Numeric Month to String.</summary>
		
		public virtual System.String fsconvertMonth(int index)
		{
			System.String[] date = new System.String[]{"jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"};
			return date[index - 1];
		}
		public virtual int fsconvertMonthInNo(System.String str_Month)
		{
			if ((System.Object) str_Month == null)
				return - 1;
			System.String[] date = new System.String[]{"jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"};
			for (int i = 0; i < date.Length; i++)
			{
				if (date[i].Equals(str_Month.ToLower()))
					return i + 1;
			}
			return - 1;
		}
		
		/// <summary><br> Updates String Columns in Table by given where (String) clause.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String condColumn, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + "='" + condition + "'");
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String and Numeric Columns in Table by given where (String) clause.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String condColumn, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + "='" + condition + "'");
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String Columns in Table by given where (Numeric) clause.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String condColumn, long condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + "=" + condition);
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String & Numeric Columns in Table by given where (Numeric) clause.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String condColumn, long condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + "=" + condition);
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String[] condColumn, System.String[] condValue, System.String[] condColumn1, long[] condValue1, int rt)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condColumn.Length; i++)
			{
				cols += (condColumn[i] + "='" + condValue[i] + "' AND ");
			}
			for (int i = 0; i < condColumn1.Length; i++)
			{
				cols += (condColumn1[i] + "=" + condValue1[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry, rt);
		}
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String[] condColumn, System.String[] condValue, System.String[] condColumn1, long[] condValue1, int rt, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condColumn.Length; i++)
			{
				cols += (condColumn[i] + "='" + condValue[i] + "' AND ");
			}
			for (int i = 0; i < condColumn1.Length; i++)
			{
				cols += (condColumn1[i] + "=" + condValue1[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry, rt, cn);
		}
		
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, System.String[] cValue2, System.String[] condColumn, System.String[] condValue, System.String[] condColumn1, System.String[] condValue1, int rt)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condColumn.Length; i++)
			{
				cols += (condColumn[i] + "='" + condValue[i] + "' AND ");
			}
			for (int i = 0; i < condColumn1.Length; i++)
			{
				cols += (condColumn1[i] + "=" + condValue1[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry, rt);
		}
		public virtual int fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, System.String[] cValue2, System.String[] condColumn, System.String[] condValue, System.String[] condColumn1, System.String[] condValue1, int rt, System.Data.OleDb.OleDbConnection cn)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += " Where ";
			for (int i = 0; i < condColumn.Length; i++)
			{
				cols += (condColumn[i] + "='" + condValue[i] + "' AND ");
			}
			for (int i = 0; i < condColumn1.Length; i++)
			{
				cols += (condColumn1[i] + "=" + condValue1[i] + " AND ");
			}
			cols = cols.Substring(0, (cols.Length - 4) - (0));
			strQry += cols;
			return fsexecuteUpdate(strQry, rt, cn);
		}
		
		/// <summary><br> Updates String Columns in Table by given where (String) clause & given conditional operator.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String condColumn, System.String condValue, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + " " + condColumn + condition + " '" + condValue + "'");
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String and Numeric Columns in Table by given where (String) clause & given conditional operator.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String condColumn, System.String condValue, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + " " + condition + " '" + condValue + "'");
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String Columns in Table by given where (Numeric) clause & given conditional operator.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String condColumn, long condValue, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + " " + condition + " " + condValue);
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		
		/// <summary><br> Updates String & Numeric Columns in Table by given where (Numeric) clause & given conditional operator.</summary>
		
		public virtual bool fsupdate(System.String tName, System.String[] cName, System.String[] cValue, System.String[] cName2, long[] cValue2, System.String condColumn, long condValue, System.String condition)
		{
			System.String strQry = "Update " + tName;
			System.String cols = " set ";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + "='" + cValue[i] + "',");
			}
			
			for (int i = 0; i < cName2.Length; i++)
			{
				cols += (cName2[i] + "=" + cValue2[i] + ",");
			}
			
			cols = cols.Substring(0, (cols.Length - 1) - (0));
			cols += (" Where " + condColumn + " " + condition + " " + condValue);
			
			strQry += cols;
			return fsexecuteUpdate(strQry);
		}
		
		/// <summary><br> Return the value of required column, where given column has given (String) value & conditional operator.</summary>
		
		public virtual System.String fsgetCols1(System.String tName, System.String rcName, System.String cName, System.String cValue, System.String condition)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + condition + "'" + cValue + "'";
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Return the value of required column, where given column has given (Numeric) value & conditional operator.</summary>
		
		public virtual System.String fsgetCol(System.String tName, System.String rcName, System.String cName, long cValue, System.String condition)
		{
			System.String result = null;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + condition + cValue;
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					if (rset.Read())
						result = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				//rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		
		/// <summary><br> Return required column, where given column has given (String) value & conditional operator.</summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName, System.String cName, System.String cValue, System.String condition)
		{
			System.String[] result = null; int counter = 0;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + condition + "'" + cValue + "'";
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
						result[counter++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				////rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Return required column, where given column has given (Numeric) value & conditional operator.</summary>
		
		public virtual System.String[] fsgetCols(System.String tName, System.String rcName, System.String cName, long cValue, System.String condition)
		{
			System.String[] result = null; int counter = 0;
			System.String strQry = "Select " + rcName + " from " + tName + " where " + cName + " " + condition + " " + cValue;
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
						result[counter++] = System.Convert.ToString(rset[1 - 1]);
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				////rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Retrun One or More Column from required table by a where (String) clause & conditional operator.</summary>
		
		public virtual System.String[][] fsgetCols(System.String tName, System.String[] rcName, System.String cName, System.String cValue, System.String condition)
		{
			System.String[][] result = null; int counter = 0, colCount = 0;
			System.String strQry = "Select ";
			for (int i = 0; i < rcName.Length; i++)
				strQry += (rcName[i] + ",");
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			strQry += (" from " + tName + " where " + cName + condition + "'" + cValue + "'");
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				////rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Retrun One or More Column from required table by a where (Numeric) clause & conditional operator.</summary>
		
		public virtual System.String[][] fsgetCols(System.String tName, System.String[] rcName, System.String cName, long cValue, System.String condition)
		{
			System.String[][] result = null; int counter = 0, colCount = 0;
			System.String strQry = "Select ";
			for (int i = 0; i < rcName.Length; i++)
				strQry += (rcName[i] + ",");
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			strQry += (" from " + tName + " where " + cName + condition + cValue);
			
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				////rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Retrun ResultSet of One or More Column from required table.</summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetCols(System.String tName, System.String[] rcName)
		{
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader result = null;
			System.String strQry = "Select ";
			
			for (int i = 0; i < rcName.Length; i++)
				strQry += (rcName[i] + ",");
			
			strQry = strQry.Substring(0, (strQry.Length - 1) - (0));
			strQry += (" from " + tName);
			
			try
			{
				result = fsexecuteQuery(strQry);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		/// <summary><br> Retrun ResultSet of all Column from required table.</summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetCols(System.String tName)
		{
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader result = null;
			System.String strQry = "Select * from " + tName;
			try
			{
				result = fsexecuteQuery(strQry);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		/// <summary><br> Retrun ResultSet of all Column from required table & in given order.</summary>
		
		//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		public virtual System.Data.OleDb.OleDbDataReader fsgetColsAll(System.String tName, System.String orderCol)
		{
			//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
			System.Data.OleDb.OleDbDataReader result = null;
			System.String strQry = "Select * from " + tName + " order by " + orderCol;
			try
			{
				result = fsexecuteQuery(strQry);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			SHGNDataAccess da = new SHGNDataAccess();
			//da.fsexecuteQuery("Select NVL(A,''),B,TO_DATE(C,'MM/DD/YYYY') from XYZ",null);
			//da.fsexecuteUpdate("insert into values (null,'__null__','hello','null'");
			//da.database=da.ORACLE;
			//System.out.println(da.fscheckAndResetDate("12/25/2003"));
			//System.out.println(da.removeComments("select concat('--',--') from abc"));
		}
		public virtual System.String fsgetErrorMessage()
		{
			if (errorCode == 1)
			{
				return "Unable to save the record, Duplicate value found";
			}
			else if (errorCode == 2291)
			{
				return "Unable to save the record, Parent Record not found";
			}
			else if (errorCode == 2292)
			{
				return "Unable to delete the record, Child Record found ";
			}
			else if (errorCode == 0)
			{
				return "Can't Insert NULL ";
			}
			else
				return null;
		}
		public virtual System.String fsconvertMonth(System.String str)
		{
			if ((System.Object) str == null || str.Length < 7)
				return null;
			System.String e_date = str;
			System.String sep = e_date.IndexOf("-") != - 1?"-":"/";
			int pos1, pos2;
			pos1 = e_date.IndexOf(sep);
			//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
			pos2 = e_date.IndexOf(sep, pos1 + 1);
			System.String day = e_date.Substring(0, (pos1 + 1) - (0));
			System.String month = fsconvertMonth(System.Int32.Parse(e_date.Substring(pos1 + 1, (pos2) - (pos1 + 1))));
			System.String year = e_date.Substring(pos2);
			str = day + month + year;
			return str;
		}
		/*
		This routine takes string table name , string array columns and string array values and insert
		them into the tName table. Values containing date or time were first concatinated with convert() SQL
		function for MSSQL or with to_date for ORACLE.
		This routine was modified after deciding that all time and date fields will be varchar;*/
		
		public virtual bool fsinsertDate(System.String tName, System.String[] cName, System.String[] cValue)
		{
			System.String strQry = "Insert into " + tName;
			System.String cols = " ("; System.String values = " Values ('";
			
			for (int i = 0; i < cName.Length; i++)
			{
				cols += (cName[i] + ", ");
				values += (cValue[i] + "', '");
			}
			cols = cols.Substring(0, (cols.Length - 2) - (0));
			values = values.Substring(0, (values.Length - 3) - (0));
			cols += ")";
			values += ")";
			
			strQry += (cols + values);
			//System.out.println(strQry);
			return fsexecuteUpdate(strQry);
		}
		
		/// <summary>This routine is used in Security Manager
		/// <br> Return result 2 dimension string, where given columns has given (String) values.
		/// </summary>
		
		public virtual System.String[][] fsgetColsResultSet(System.String tName, System.String rcName, System.String[] condColumn, System.String[] condition)
		{
			
			System.String strQry = "Select " + rcName + " from " + tName + " where ";
			
			System.String[][] result = null; int counter = 0, colCount = 0;
			
			for (int i = 0; i < condition.Length; i++)
			{
				strQry += (condColumn[i] + "='" + condition[i] + "' AND ");
			}
			
			strQry = strQry.Substring(0, (strQry.Length - 4) - (0));
			//System.out.println(strQry);
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				colCount = rset.GetSchemaTable().Rows.Count;
				result = new System.String[fsrecordCount(rset)][];
				for (int i2 = 0; i2 < fsrecordCount(rset); i2++)
				{
					result[i2] = new System.String[colCount];
				}
				rset = fsexecuteQuery(strQry);
				if (rset != null)
				{
					while (rset.Read())
					{
						for (int i = 0; i < colCount; i++)
							result[counter][i] = (System.Object) System.Convert.ToString(rset[i + 1 - 1]) == null?"":System.Convert.ToString(rset[i + 1 - 1]);
						counter++;
					}
					//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
					//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
					////rset.getStatement().close();
					rset.Close();
				}
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		public virtual System.String setDate(System.String e_date)
		{
			//Oracle
			if (database == SHGNDataAccess.ORACLE || database == SHGNDataAccess.SQLSERVER)
			{
				System.String sep = e_date.IndexOf("-") != - 1?"-":"/";
				int pos1, pos2;
				pos1 = e_date.IndexOf(sep);
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				pos2 = e_date.IndexOf(sep, pos1 + 1);
				System.String day = e_date.Substring(0, (pos1 + 1) - (0));
				System.String month = fsconvertMonth(System.Int32.Parse(e_date.Substring(pos1 + 1, (pos2) - (pos1 + 1))));
				System.String year = e_date.Substring(pos2);
				return day + month + year;
			}
			else if (database == SHGNDataAccess.DB2)
			{
				System.String sep = e_date.IndexOf("-") != - 1?"-":"/";
				int pos1, pos2;
				pos1 = e_date.IndexOf(sep);
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				pos2 = e_date.IndexOf(sep, pos1 + 1);
				System.String day = e_date.Substring(0, (pos1 + 1) - (0));
				System.String month = e_date.Substring(pos1 + 1, (pos2) - (pos1 + 1)) + sep;
				System.String year = e_date.Substring(pos2 + 1);
				//System.out.println("Date is: "+month+day+year);
				return month + day + year;
			}
			return e_date;
		}
		public virtual System.String fscheckAndResetDate(System.String str_Date)
		{
			int int_StartIndex = str_Date.IndexOf("/");
			int int_EndIndex = str_Date.LastIndexOf("/");
			if (int_StartIndex == - 1)
			{
				int_StartIndex = str_Date.IndexOf("-");
				int_EndIndex = str_Date.LastIndexOf("-");
			}
			if (int_StartIndex != - 1 && int_EndIndex != - 1 && int_StartIndex != int_EndIndex)
				return resetDate(str_Date);
			return str_Date;
		}
		public virtual System.String resetDate(System.String e_date)
		{
			if (database == SHGNDataAccess.ORACLE || database == SHGNDataAccess.SQLSERVER)
			{
				//Oracle
				System.String sep = e_date.IndexOf("-") != - 1?"-":"/";
				int pos1, pos2;
				pos1 = e_date.IndexOf(sep);
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				pos2 = e_date.IndexOf(sep, pos1 + 1);
				System.String day = e_date.Substring(0, (pos1 + 1) - (0));
				int month = fsconvertMonthInNo(e_date.Substring(pos1 + 1, (pos2) - (pos1 + 1)));
				System.String year = e_date.Substring(pos2);
				System.String str_Month = "0" + month;
				str_Month = str_Month.Substring(str_Month.Length - 2);
				//System.out.println("Date is: "+day+month+year);
				return day + str_Month + year;
			}
			else if (database == SHGNDataAccess.DB2)
			{
				//DB2
				System.String sep = e_date.IndexOf("-") != - 1?"-":"/";
				int pos1, pos2;
				pos1 = e_date.IndexOf(sep);
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				pos2 = e_date.IndexOf(sep, pos1 + 1);
				System.String month = e_date.Substring(0, (pos1 + 1) - (0));
				System.String day = e_date.Substring(pos1 + 1, (pos2) - (pos1 + 1)) + sep;
				System.String year = e_date.Substring(pos2 + 1);
				//System.out.println("Date is: "+day+month+year);
				return day + month + year;
			}
			return e_date;
		}
		public virtual System.String fsgetFormatedDate(System.String str_Date)
		{
			if ((System.Object) str_Date == null || str_Date.Length < 7)
				return "";
			System.String str_FDate = str_Date.Substring(8, (10) - (8)) + "/" + str_Date.Substring(5, (7) - (5)) + "/" + str_Date.Substring(0, (4) - (0));
			return str_FDate;
		}
		public virtual System.String fsgetFormatedDateMon(System.String str_Date)
		{
			if ((System.Object) str_Date == null || str_Date.Length < 7)
				return "";
			System.String str_FDate = str_Date.Substring(8, (10) - (8)) + "/" + fsconvertMonth(System.Int32.Parse(str_Date.Substring(5, (7) - (5)))) + "/" + str_Date.Substring(0, (4) - (0));
			return str_FDate;
		}
		/// <summary><br>Return the value of required column array , against the given query. </summary>
		public virtual System.String[] getColumnsAgainstQuery(System.String strQry)
		{
			System.String[] result = null;
			try
			{
				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
				System.Data.OleDb.OleDbDataReader rset = fsexecuteQuery(strQry);
				result = new System.String[fsrecordCount(rset)];
				rset = fsexecuteQuery(strQry);
				int i = 0;
				if (rset != null)
				{
					while (rset.Read())
					{
						result[i] = System.Convert.ToString(rset[1 - 1]);
						i++;
					}
				}
				//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
				//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
				////rset.getStatement().close();
				rset.Close();
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				System.Console.Out.WriteLine(e.Message);
			}
			return result;
		}
		static SHGNDataAccess()
		{
			{
				try
				{
					//BufferedReader d=new BufferedReader(new InputStreamReader(new FileInputStream("F:\\IGIApplication\\WEB-INF\\classes\\shgn\\SHGNParameter.txt")));
					//BufferedReader d=new BufferedReader(new InputStreamReader(new FileInputStream("E:\\IGIApplicationBU\\WEB-INF\\classes\\shgn\\Parameter.txt")));
					//BufferedReader d=new BufferedReader(new InputStreamReader(new FileInputStream("C:\\SHGNParameter.txt")));
					//UPGRADE_TODO: Expected value of parameters of constructor 'java.io.BufferedReader.BufferedReader' are different in the equivalent in .NET. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092"'
			//		System.IO.StreamReader d = new System.IO.StreamReader(new System.IO.StreamReader(new System.IO.FileStream("C:\\SHGNParameter.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read)).BaseStream, System.Text.Encoding.UTF7);
			//		ip = d.ReadLine();
			//		port = d.ReadLine();
			//		instance = d.ReadLine();
			//		user = d.ReadLine();
			//		password = d.ReadLine();
			//		str_Database = d.ReadLine();
			//		System.Console.Out.WriteLine(ip + ":" + port + ":" + instance + ":" + user + ":" + password + ":" + str_Database);
					setDataBase();
					//oracle
					if (database == ORACLE)
					{
						//SHGNConnectionPool = new SHGNConnectionPool("jdbc:oracle:thin:@" + ip + ":" + port + ":" + instance, user, password, 10);
						//db2
						//SHGNConnectionPool=new SHGNConnectionPool("jdbc:datadirect:db2://"+ip+":"+port+";DatabaseName="+instance+";User="+user+";Password="+password,user,password,1);
					}
					else if (database == DB2)
					{
						//SHGNConnectionPool = new SHGNConnectionPool("jdbc:db2:" + instance, user, password, 10);
					}
					else if (database == SQLSERVER)
					{
						//sqlserver
						//SHGNConnectionPool = new SHGNConnectionPool("jdbc:microsoft:sqlserver://" + ip + ":" + port, instance, user, password, 1);
					}
					System.Console.Out.WriteLine("Data base is: " + str_Database);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
					System.Console.Out.WriteLine("Error during Opening Connection " + e.Message);
				//	//SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
		}
	}
}