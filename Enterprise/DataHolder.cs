using System;
using System.Data.OleDb;
using System.Data;
using SHMA.Enterprise.Data;
using System.Data.Common;
using SHMA.Enterprise.Shared;

namespace SHMA.Enterprise
{

	public class DataHolder
	{
			
		private DataSet dataChanges;
		public System.Data.DataSet Data = new System.Data.DataSet();
		private System.Collections.Hashtable DataAdapters = new System.Collections.Hashtable();
		private System.Collections.Hashtable CommandBuilders = new System.Collections.Hashtable();
		protected System.Collections.Hashtable IdentityColumns = new System.Collections.Hashtable();

		public DataHolder()
		{//TODO: Before adding data, it checks whether the dataAdapters list contains the table name
			// Instead it should check the dataset to have that table (b/c the readonly tables now don't add
			// Adapters
		}		

		public void FillData(string CommandString, String TableName)
			/*Runs the query given in the parameter CommandString, populates a recordset
			 * and adds it to its collection of recordsets.
			 * TableName would be the key into the hashtable
			 */
		{			
			
			if (DataAdapters.Contains(TableName)) //throw new System.Exception();
				InsertNewRowsInTable(CommandString,TableName);
			else
			{
				CommandString =AuditLog.CheckAuditFields(CommandString,TableName);
				DbDataAdapter da = DB.CreateDataAdapter(CommandString);
				
				//	System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder((OleDbDataAdapter) da);
				//da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Rehan//
				da.Fill(Data, TableName);
				DataAdapters.Add(TableName, da);
				OraclePKPatch(Data.Tables[TableName], TableName);
				//	CommandBuilders.Add(TableName, builder);
				//	builder.GetUpdateCommand(); 	
			}
		}

		
		public void FillData(String CommandString, String TableName, string identity) 
		{
			FillData(CommandString , TableName);
			IdentityColumns.Add(TableName.ToString(),identity.ToString());
			System.Data.OleDb.OleDbDataAdapter da = (OleDbDataAdapter) DataAdapters[TableName];
			OraclePKPatch(Data.Tables[TableName], TableName);
			da.RowUpdated += new OleDbRowUpdatedEventHandler(OnRowUpdated);			

		}

		public void FillData(IDbCommand command, String TableName)
		{
			/*Runs the prepared statement given in the parameter command,
			 * populates a recordset
			 * and adds it to its collection of recordsets.
			 * TableName would be the key into the hashtable
			 */
			if (DataAdapters.Contains(TableName)) // throw new System.Exception();
			{
				InsertNewRowsInTable(command,TableName);
			}
			else
			{
				command = AuditLog.CheckAuditFields(command,TableName);
				System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)command);
				
				//da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Rehan//
			//	System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder(da);
                
                da.Fill(Data, TableName);
			    DataAdapters.Add(TableName, da);

				OraclePKPatch(Data.Tables[TableName], TableName);
			//	CommandBuilders.Add(TableName, builder);
			//	builder.GetUpdateCommand();
			}
		}

		public void FillData(IDbCommand command, String TableName, bool ReadOnly)
		{
			/*Runs the prepared statement given in the parameter command,
			 * populates a recordset
			 * and adds it to its collection of recordsets.
			 * TableName would be the key into the hashtable
			 */
			if (DataAdapters.Contains(TableName)) // throw new System.Exception();
			{
				InsertNewRowsInTable(command,TableName);
			}
			else
			{
				if(!ReadOnly)
				{
					command = AuditLog.CheckAuditFields(command,TableName);
					
				}
				System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)command);
				//da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Rehan//
				
				//	System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder(da);
				da.Fill(Data, TableName);
				if (!ReadOnly)
				{
					DataAdapters.Add(TableName, da);
					OraclePKPatch(Data.Tables[TableName], TableName);
				}
				//	CommandBuilders.Add(TableName, builder);
				//	builder.GetUpdateCommand();
			}
			
		}
		public void FillData(IDbCommand command, String TableName, string identity) 
		{
			FillData(command , TableName);
			IdentityColumns.Add(TableName.ToString(),identity.ToString());
			OleDbDataAdapter da = (OleDbDataAdapter) DataAdapters[TableName];
			OraclePKPatch(Data.Tables[TableName], TableName);
			da.RowUpdated += new OleDbRowUpdatedEventHandler(OnRowUpdated);			
		}

		
		public void FillData_RO(String CommandString, String TableName)
			/*Similar to FillData, except that it would add a readonly recordset, which would not be saved 
			 * back to database*/
		{
			if (DataAdapters.Contains(TableName)) throw new System.Exception();
			DbDataAdapter da = DB.CreateDataAdapter(CommandString);
			da.Fill(Data, TableName);
		}

		public void FillData_RO(IDbCommand command, String TableName)
			/*Similar to FillData, except that it would add a readonly recordset, which would not be saved back to database*/
		{
			if (DataAdapters.Contains(TableName)) throw new System.Exception();
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)command);
			da.Fill(Data, TableName);
		}
						
		public void Update()
		{
			/* Applies the changes made in all the updateable recordsets to the database.
			 *  Tries to resolve the dependencies		 */
			const System.Int32 MaxPasses = 4;
			System.Boolean ErrorOccured = false;
			System.Collections.Hashtable remainingTables=new System.Collections.Hashtable();
			System.Int32 Pass = 1;
			System.Collections.Hashtable tablesToBeUpdated = DataAdapters;
            
			/*			for(int i=0;i<Data.Tables["trans_Detail"].Rows.Count;i++)
						{
							Data.Tables["trans_Detail"].Rows[i][0];
							Data.Tables["trans_Detail"].Rows[i][1];
							Data.Tables["trans_Detail"].Rows[i][2];
							Data.Tables["trans_Detail"].Rows[i][3];
							Data.Tables["trans_Detail"].Rows[i][4];
							Data.Tables["trans_Detail"].Rows[i][5];
							Data.Tables["trans_Detail"].Rows[i][6];
							Data.Tables["trans_Detail"].Rows[i][7];
							Data.Tables["trans_Detail"].Rows[i][8];
						}*/
			do
			{					
				ErrorOccured = false;
				foreach (String table in tablesToBeUpdated.Keys)
				{
					try
					{

						((System.Data.OleDb.OleDbDataAdapter) DataAdapters[table]).Update(Data,table);

					}  
						//					   catch (System.ApplicationException e)
					catch (Exception e)
					{
						if (Pass<MaxPasses)
						{

							ErrorOccured = true;
							remainingTables.Add(table,DataAdapters[table]);
						}
						else
						{
							throw e;
						}

						//						   if (e.Message.IndexOf("REFERENCE") >=0 || e.Message.IndexOf("FOREIGN KEY") >=0)
						//						   {
						//							   ErrorOccured = true;
						//							   remainingTables.Add(table,DataAdapters[table]);
						//						   }
						//						   else
						//						   {
						//								throw e;
						//						   }
					}
				}
				Pass ++;
				if (ErrorOccured)
				{
					tablesToBeUpdated = (System.Collections.Hashtable) remainingTables.Clone();
					remainingTables.Clear();
				}
			}
			while (ErrorOccured && (Pass <= MaxPasses)); 

		}


		public void Update(IDbTransaction transaction) 
			/* Same as Update(), except it updates inside database transaction, so the changes may either
			 * all be applied or rolled back */
		{			
			BuildCommandBuilder();
			//w29:foreach (OleDbCustomCommandBuilder builder in CommandBuilders.Values)
			foreach (System.Data.OleDb.OleDbCommandBuilder builder in CommandBuilders.Values)
			{
				builder.GetDeleteCommand().Transaction = (OleDbTransaction) transaction ;
				builder.GetDeleteCommand().Connection = (OleDbConnection) transaction.Connection;
				builder.GetInsertCommand().Transaction = (OleDbTransaction)  transaction ;
				builder.GetInsertCommand().Connection = (OleDbConnection) transaction.Connection;
				builder.GetUpdateCommand().Transaction = (OleDbTransaction) transaction ;				
				builder.GetUpdateCommand().Connection = (OleDbConnection) transaction.Connection;
			}

			Update();
													   
		}

		private void BuildCommandBuilder(){
			System.Collections.IDictionaryEnumerator enumerator = DataAdapters.GetEnumerator();
			while(enumerator.MoveNext()) {
				if(!CommandBuilders.ContainsKey(enumerator.Key))
				{
					OleDbDataAdapter da = (OleDbDataAdapter)enumerator.Value;
					da.SelectCommand.Connection = (OleDbConnection) DB.Connection;
					if(DB.isInTransaction())
						da.SelectCommand.Transaction = (OleDbTransaction) DB.Transaction;
					//w29:OleDbCustomCommandBuilder builder = new OleDbCustomCommandBuilder(da, Data.Tables[0]);
					System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder(da);
					CommandBuilders.Add(enumerator.Key, builder);
					builder.RefreshSchema();
				}
				else
				{
					OleDbDataAdapter da = (OleDbDataAdapter)enumerator.Value;
					da.SelectCommand.Connection = (OleDbConnection) DB.Connection;
					if(DB.isInTransaction())
						da.SelectCommand.Transaction = (OleDbTransaction) DB.Transaction;
				}
			}
			
			
			
		}
		private void InsertNewRowsInTable(string CommandString, String TableName)
		{
			CommandString = AuditLog.CheckAuditFields(CommandString,TableName);
			IDataAdapter dataAdapter = DB.CreateDataAdapter(CommandString);
			dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			DataSet ds = new DataSet();
			dataAdapter.Fill(ds);
			DataTable dataTable = ds.Tables[0];
			DataTable CompareTable;
			if(!Data.Tables.Contains(TableName))
			{
				CompareTable = AddDataTableInDataSet(dataTable,TableName);
			}
			else
				CompareTable = Data.Tables[TableName];
			if(CompareTable.PrimaryKey.Length<=0 && dataTable.PrimaryKey.Length>0)
				AddPrimaryKey(dataTable,CompareTable);
			InsertRows(dataTable,CompareTable);
		}

		
		private void InsertNewRowsInTable(IDbCommand Command, String TableName)
		{
			Command = AuditLog.CheckAuditFields(Command,TableName);
			IDataAdapter dataAdapter = DB.CreateDataAdapter(Command);
			dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			DataSet ds = new DataSet();
			dataAdapter.Fill(ds);
			DataTable dataTable = ds.Tables[0];
			DataTable CompareTable;
			if(!Data.Tables.Contains(TableName))
				CompareTable = AddDataTableInDataSet(dataTable,TableName);
			else
				CompareTable = Data.Tables[TableName];
			if(CompareTable.PrimaryKey.Length<=0 && dataTable.PrimaryKey.Length>0)
				AddPrimaryKey(dataTable,CompareTable);
			InsertRows(dataTable,CompareTable);

		}

		public void LoadNewRows(DataTable dataTable){
			DataTable CompareTable = Data.Tables[dataTable.TableName];
			if(CompareTable.PrimaryKey.Length<=0 && dataTable.PrimaryKey.Length>0)
				AddPrimaryKey(dataTable,CompareTable);
			else if(dataTable.PrimaryKey.Length<=0 && CompareTable.PrimaryKey.Length>0)
				AddPrimaryKey(CompareTable,dataTable);
			InsertRows(CompareTable,dataTable);
		}

		private DataTable AddDataTableInDataSet(DataTable dataTable,String TableName)
		{
			Data.Tables.Add(TableName);
			DataTable CompareTable = Data.Tables[TableName];
			foreach(System.Data.DataColumn dcolumn in dataTable.Columns)
				CompareTable.Columns.Add(dcolumn.ColumnName, dcolumn.DataType);
			return CompareTable;
		}
		
		private void AddPrimaryKey(DataTable TableWithPK, DataTable TableWithoutPK)
		{
			System.Data.DataColumn[] keys = new System.Data.DataColumn[TableWithPK.PrimaryKey.Length];
			System.Data.DataColumn colm = new System.Data.DataColumn();
			int col=0;
			foreach(DataColumn column in TableWithPK.PrimaryKey)
			{
				colm = TableWithoutPK.Columns[column.ColumnName];
				keys[col] = colm;
				col++;
			}	
			TableWithoutPK.PrimaryKey = keys; //TableWithPK.PrimaryKey;;
		}

		private void InsertRows(DataTable dataTable, DataTable CompareTable)
		{
			bool inserted=false;
			
			DataRow foundRow;
			// Create an array for the key values to find.
			object[] keys = dataTable.PrimaryKey;
			int findValsLength=0;

			if (dataTable.PrimaryKey.Length>0)
				findValsLength = dataTable.PrimaryKey.Length;
			else
				findValsLength = dataTable.Columns.Count;
			
			object[]findTheseVals = new object[findValsLength];
			int col=0;
		
			bool flag = CompareTable.DataSet.EnforceConstraints ;
			CompareTable.DataSet.EnforceConstraints = false;
			foreach(DataRow DR in dataTable.Rows)
			{
				if(dataTable.PrimaryKey.Length>0)
				{
					foreach(DataColumn column in keys)
					{
						findTheseVals[col] = DR[column];
						col++;
					}
				}
				else
				{
					foreach(DataColumn column in dataTable.Columns)
					{
						findTheseVals[col] = DR[column];
						col++;
					}

				}
				col=0;
				foundRow =  CompareTable.Rows.Find(findTheseVals); 
				if(foundRow == null)
				{ 
					inserted=true;
					//CompareTable.Rows.Add(DR.ItemArray);
					
					DataRow newRow = CompareTable.NewRow(); 
					foreach(DataColumn column in dataTable.Columns){
						if (CompareTable.Columns.Contains(column.ColumnName)){
							newRow[column.ColumnName] = DR[column.ColumnName];
						}
					}
					CompareTable.Rows.Add(newRow);
				}
				
			}
			CompareTable.DataSet.EnforceConstraints = flag;
			if(inserted)
				CompareTable.AcceptChanges();

		}
		
		private bool FindInTable(DataTable table, object[] findTheseVals)
		{
			DataView view =  new DataView(table);
			int foundAt = -1;
			
			foundAt = view.Find(findTheseVals);
			
			if (foundAt < 0)
			{
				view.RowStateFilter = DataViewRowState.Deleted;
				foundAt = view.Find(findTheseVals);
			}

			return (foundAt > -1);
		}
		private void OnRowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs args) 
		{
			int newID = 0;			
			if (args.StatementType == StatementType.Insert) 
			{
				IDbCommand cmdID = DB.CreateCommand("SELECT @@IDENTITY");				
				newID = int.Parse(cmdID.ExecuteScalar().ToString());				
				args.Row[IdentityColumns[args.Row.Table.ToString()].ToString()] = newID;
			}
		}
		
		
		public System.Data.DataTable this[String tableName]
		{
			get {return Data.Tables[tableName];}
		}
		public void RejectChanges(){
			this.Data = dataChanges.Copy();			
		}
		public void AcceptChanges(){
			this.Data.AcceptChanges();
			dataChanges = this.Data.Copy();
		}
		public void BeginChanges(bool keepChanges){
			if (keepChanges){
				if (dataChanges==null)
					dataChanges = this.Data.Copy();
			}
			else{
				dataChanges = this.Data.Copy();
			}
		}

		public rowset getAsSHMARowSet(string tableName){
			rowset rowSet =null;
			if (tableName!=null && tableName.Length>0){
				DataTable table = this.Data.Tables[tableName];				
				if (table!=null){
					rowSet = new rowset(table);
				}					
			}			
			return rowSet;
		}

		private void OraclePKPatch(DataTable table, string TableName)
		{
			if (DB.dataBaseType==DB.DataBaseType.Oracle)
			{
				ConstraintManager.AddOraPKIfMissing( Data.Tables[TableName]);//Rehan//
			}
		}
	}
}
