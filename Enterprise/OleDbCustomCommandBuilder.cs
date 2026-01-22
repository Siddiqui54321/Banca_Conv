using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OleDb;
using SHMA.Enterprise.Configuration;

namespace SHMA.Enterprise
{
	public class OleDbCustomCommandBuilder
	{
		DataTable dataTable;
		OleDbConnection connection;
		OleDbCommandBuilder builder;
		OleDbDataAdapter adapter;
		OleDbTransaction transaction ;

		public OleDbCustomCommandBuilder( DataTable dataTable, OleDbConnection connection )
		{
			this.dataTable = dataTable;
			this.connection = connection;
		}

		public OleDbCustomCommandBuilder( OleDbDataAdapter adapter, DataTable dataTable )
		{
			this.adapter = adapter;
			this.dataTable = dataTable;
			this.connection = adapter.SelectCommand.Connection;
			this.transaction = adapter.SelectCommand.Transaction;
		}

		private OleDbCommandBuilder getDefaultBuilder()
		{
			if(builder==null)
			{
				builder = new OleDbCommandBuilder(this.adapter);
			}
			return builder;
		}

		public OleDbCommand SelectAllCommand
		{
			get 
			{
				string commandText = "SELECT " + ColumnsString + " FROM " + TableName;
				return GetTextCommand( commandText );
			}
		}

		public OleDbCommand GetSelectWithFilterCommand( string filter )
		{
			string commandText = "SELECT " + ColumnsString + 
				" FROM " + TableName +
				" WHERE " + filter;
			return GetTextCommand( commandText );
		}

		public OleDbCommand GetSelectWithOrderCommand( string order )
		{
			string commandText = "SELECT " + ColumnsString + 
				" FROM " + TableName +
				" ORDER BY " + order;
			return GetTextCommand( commandText );
		}

		public OleDbCommand DeleteCommand
		{
			get
			{
				DataTable schema = new DataTable();
				adapter.FillSchema(schema, SchemaType.Source);
 
				OleDbCommand command = GetTextCommand( "" );
				StringBuilder whereString = new StringBuilder();
				Hashtable exceptionalColumns = new Hashtable();

				foreach( DataColumn column in schema.Columns )
				{
					if( isExceptionColumn( column ) )
					{
						exceptionalColumns.Add(column.Caption, column);
					}
					else
					{
						if( whereString.Length > 0 )
						{
							whereString.Append( " AND " );
						}
						if(column.AllowDBNull)
						{
							whereString.Append("( ( ").Append( column.ColumnName ).Append( " = ?  )" );
							whereString.Append( " OR ( " ).Append( column.ColumnName ).Append( " IS NULL ) )" );
						}
						else
						{
							whereString.Append( column.ColumnName ).Append( " = ? " );
						}
						command.Parameters.Add( CreateParam( column, false ) );
					}
				}

				string commandText = "DELETE FROM " + TableName
					+ " WHERE " + whereString.ToString();
				command.CommandText = commandText;

				if(exceptionalColumns.Count==0)
				{
					command = this.getDefaultBuilder().GetDeleteCommand();
				}
				this.adapter.DeleteCommand	 = command;
				return command;

//				OleDbCommand command = this.getDefaultBuilder().GetDeleteCommand();
//				this.adapter.DeleteCommand	 = command;
//				return command;
			}
		}

		/// <summary>
		/// Creates Insert command with support for Autoincrement (Identity) tables
		/// </summary>
		public OleDbCommand InsertCommand
		{
			get
			{
				// For the time being using the default command builder generated commands...
//				OleDbCommand command = GetTextCommand( "" );
//				StringBuilder intoString = new StringBuilder();
//				StringBuilder valuesString = new StringBuilder();
//				ArrayList autoincrementColumns = AutoincrementKeyColumns;
//				foreach( DataColumn column in dataTable.Columns )
//				{
//					if( ! autoincrementColumns.Contains( column ) )
//					{
//						// Not a primary key
//						if( intoString.Length > 0 )
//						{
//							intoString.Append( ", " );
//							valuesString.Append( ", " );
//						}
//						intoString.Append( column.ColumnName );
//						valuesString.Append( "@" ).Append( column.ColumnName );
//						command.Parameters.Add( CreateParam(column) );
//					}
//				}
//				string commandText = "INSERT INTO " + TableName + "("
//					+ intoString.ToString() + ") VALUES (" + valuesString.ToString() + "); ";
//				if( autoincrementColumns.Count > 0 ) 
//				{
//					commandText += "SELECT SCOPE_IDENTITY() AS "
//						+ ( (DataColumn) autoincrementColumns[0]) .ColumnName;
//				}
//				command.CommandText = commandText;

				OleDbCommand command = this.getDefaultBuilder().GetInsertCommand();
				this.adapter.InsertCommand	 = command;
				return command;
			}
		}

		/// <summary>
		/// Creates Update command with optimistic concurency support
		/// </summary>
		public OleDbCommand UpdateCommand
		{
			get
			{
				DataTable schema = new DataTable();
				adapter.FillSchema(schema, SchemaType.Source);
				OleDbCommand command = GetTextCommand( "" );
				StringBuilder setString = new StringBuilder();
				StringBuilder whereString = new StringBuilder();
				DataColumn[] primaryKeyColumns = schema.PrimaryKey;
				Hashtable exceptionalColumns = new Hashtable();

//				foreach( DataColumn column in dataTable.Columns )
//				{
//					
//					Console.WriteLine("{0}"+column.MaxLength);
//					
//					if( column.MaxLength <= 4000 ) //System.Array.IndexOf( primaryKeyColumns, column) != -1 )
//					{
//						// A primary key
//						if( whereString.Length > 0 )
//						{
//							whereString.Append( " AND " );
//						}
//						whereString.Append( column.ColumnName )
//							.Append( " = ?" );
//					}
//					else
//					{
//						exceptionalColumns.Add(column.Caption, column);
//					}
//
//					if( System.Array.IndexOf( primaryKeyColumns, column) == -1 )
//					{
//						if( setString.Length > 0 )
//						{
//							setString.Append( ", " );
//						}
//						setString.Append( column.ColumnName )
//							.Append( " = ?" );
//						command.Parameters.Add( CreateParam( column ) );
//					}
//				}
//
//				foreach( DataColumn column in dataTable.Columns )
//				{
//					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
//					{
//						command.Parameters.Add( CreateParam( column ) );
//					} 
//				}

				foreach( DataColumn column in schema.Columns )
				{
					if( System.Array.IndexOf( primaryKeyColumns, column) == -1 )
					{
						if( setString.Length > 0 )
						{
							setString.Append( ", " );
						}
						setString.Append( column.ColumnName ).Append( " = ?" );
						command.Parameters.Add( CreateParam( column, false ) );
					}
				}

				foreach( DataColumn column in schema.Columns )
				{
					Console.WriteLine("{0}"+column.MaxLength);
					//if( column.MaxLength <= 4000 ) //System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					if( isExceptionColumn( column ) )
					{
						exceptionalColumns.Add(column.Caption, column);
					}
					else
					{
						if( whereString.Length > 0 )
						{
							whereString.Append( " AND " );
						}
						if(column.AllowDBNull)
						{
							whereString.Append("( ( ").Append( column.ColumnName ).Append( " = ?  )" );
							whereString.Append( " OR ( " ).Append( column.ColumnName ).Append( " IS NULL ) )" );
						}
						else
						{
							whereString.Append( column.ColumnName ).Append( " = ? " );
						}
						command.Parameters.Add( CreateParam( column, true ) );
					}
				}

				string commandText = "UPDATE "	 + TableName 
									 + " SET "	 + setString.ToString() 
									 + " WHERE " + whereString.ToString();

				command.CommandText = commandText;
				
				if(exceptionalColumns.Count==0)
				{
					command = this.getDefaultBuilder().GetUpdateCommand();
				}
				this.adapter.UpdateCommand = command;

				return command;
			}
		}

		private ArrayList AutoincrementKeyColumns
		{
			get
			{
				ArrayList autoincrementKeys = new ArrayList();
				foreach( DataColumn primaryKeyColumn in dataTable.PrimaryKey )
				{
					if( primaryKeyColumn.AutoIncrement ) 
					{
						autoincrementKeys.Add( primaryKeyColumn );
					}
				}
				return autoincrementKeys;
			}
		}

		private OleDbParameter CreateParam( DataColumn column, bool original )
		{
			OleDbParameter sqlParam = new OleDbParameter();
			string columnName = column.ColumnName;
			sqlParam.ParameterName = "@" + columnName;
			sqlParam.SourceColumn = columnName;
			if(original)
			{
				sqlParam.SourceVersion = DataRowVersion.Original;
			}
			return sqlParam;
		}

		private OleDbCommand GetTextCommand( string text )
		{
			OleDbCommand command = new OleDbCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = text;
			command.Connection = connection;
			command.Transaction = transaction;
			return command;
		}

		private string TableName 
		{
			get { return "[" + dataTable.TableName + "]"; }
		}

		private string ColumnsString
		{
			get 
			{
				StringBuilder columnsString = new StringBuilder();
				foreach( DataColumn column in dataTable.Columns )
				{
					if( columnsString.Length > 0 ) 
					{
						columnsString.Append( ", " );
					}
					columnsString.Append( column.ColumnName );
				}
				return columnsString.ToString();
			}
		}

		public void RefreshSchema()
		{
			this.getDefaultBuilder().RefreshSchema();
		}

		public OleDbCommand GetDeleteCommand()
		{
			return this.DeleteCommand;
		}

		public OleDbCommand GetInsertCommand()
		{
			return this.InsertCommand;
		}

		public OleDbCommand GetUpdateCommand()
		{
			return this.UpdateCommand;
		}

		private bool isExceptionColumn( DataColumn column )
		{
			//Data Type: System.String		== varchar
			//Data Type: System.Int32		== int
			//Data Type: System.DateTime	== datetime
			//Data Type: System.Boolean		== bit
			string Setting = AppSettings.GetSetting("ConcurrencyIgnoreTypes"); //"String(4000)";
			if ((Setting!=null) && (Setting.Trim().Length>0))
			{
				char[] splitter  = {','};
				string[]List = Setting.Split(splitter);
				string datatype, size;
				int start, end;

				for(int i=0; i< List.Length; i++)
				{
					List[i] = List[i].Trim();
					start = List[i].IndexOf("(");
					end = List[i].IndexOf(")");
					datatype = "System." + List[i].Substring(0, start);
					size = List[i].Substring(start+1, end-start-1);
					if( datatype.Equals( column.DataType.ToString() ))
					{
						if(size.Trim().Equals(""))
						{
							return true;
						}
						else
						{
							int isize = Convert.ToInt32(size);
							if(column.MaxLength>isize)
								return true;
						}
					}
				}
			}
			return false;
		}




	}
}
