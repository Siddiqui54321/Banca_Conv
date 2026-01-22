using System;
using System.Data;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using System.Collections;
using System.Collections.Specialized;
namespace SHMA.Enterprise{
	public abstract class DataGateway{
		
		private string _IdentityColumn;
		protected string IdentityColumn{
			set{_IdentityColumn=value;}
		}
		public DataHolder Holder;
		public abstract String TableName
		{get;}
			 
		public System.Data.DataSet Data
		{
			get{return Holder.Data;}
		}

		private DataGateway()
		{
			Holder = new DataHolder();
		}
		
		public DataGateway(DataHolder holder)
		{
			this.Holder = holder;
		}

		public void LoadAll()							
		{
			String commandString = String.Format("select * from {0}",TableName);
			if (_IdentityColumn==null)
				Holder.FillData(commandString, TableName);
			else
				Holder.FillData(commandString, TableName,_IdentityColumn);
		}

		public void LoadWhere(String whereClause)	
		{
			String commandString = String.Format("select * from {0} where {1}", TableName, whereClause);
			if (_IdentityColumn==null)
				Holder.FillData(commandString, TableName);
			else
				Holder.FillData(commandString, TableName, _IdentityColumn);
		}

		public DataHolder GetBlankTable(){
			String commandString ="select * from " + TableName + " Where 1=0";
			if (_IdentityColumn==null)
				Holder.FillData(commandString, TableName);
			else
				Holder.FillData(commandString, TableName,_IdentityColumn);
			return this.Holder; 		
		}

		public System.Data.DataTable Table			
		{
			get {return Data.Tables[TableName];}
		}

		public static System.Int32 MaxConcatenationInQuery	
		{
			get {return 25;}
		}

		public DataHolder FillHolder(string query){
			IDbCommand myCommand = DB.CreateCommand(query);			
			this.Holder.FillData(myCommand, TableName);  
			return this.Holder; 
		}

		public IDataReader GetSomeRecords_RO(int count) {
			String strQuery= string.Format("SELECT top {0} * FROM {1}", count.ToString(), TableName);
			IDbCommand myCommand = DB.CreateCommand(strQuery);	
			return myCommand.ExecuteReader();
		}
		public IDataReader GetAll_RO() {
			String strQuery= string.Format("SELECT * FROM {0}", TableName);
			IDbCommand myCommand = DB.CreateCommand(strQuery);	
			return myCommand.ExecuteReader();
		}
		
//		public DataTable GetSomeRows_ROT(IDbCommand command, int offset, int numRows){
//			
//			IDataReader reader = GetAll_RO();			
//			int colCount = reader.FieldCount;
//			DataTable tbl= reader.GetSchemaTable();
//			DataTable table = new DataTable();
//			foreach(DataRow row1 in tbl.Rows){
//				table.Columns.Add(row1[0].ToString());
//			}			
//			DataRow row;
//			int rowNum=0;
//			while (reader.Read()){
//				if (rowNum>startIndex){
//					row = table.NewRow();
//					for (int i=0; i<colCount; i++){
//						row[i] = reader.GetValue(i);
//					}
//					table.Rows.Add(row);
//					if (rowNum > startIndex+length)
//						break;
//				}
//				rowNum++;
//			}
//			reader.Close();
//			return table;
//		} 		

		public IDataReader GetSomeFilteredRecords_RO( int count, string colName, string colValue) {
			String strQuery= string.Format("SELECT top {0} * from {1} where {2} like '{3}'",count.ToString(), TableName,  colName, colValue);
			IDbCommand myCommand = DB.CreateCommand(strQuery);							
			return myCommand.ExecuteReader();
		}
		
		public DataTable GetSomeRecords_ROT(int offset, int numRows) {			
			String strQuery= "SELECT * FROM [Orders]";
			IDbCommand myCommand = DB.CreateCommand(strQuery);	
			System.Data.Common.DbDataAdapter da = DB.CreateDataAdapter(myCommand);
			DataSet ds = new DataSet("ds");da.Fill(ds, offset, numRows, "Orders");return ds.Tables[0];
		}
		public DataTable GetSomeRecords_ROT(int offset, int numRows, string query) {			
			IDbCommand myCommand = DB.CreateCommand(query);	
			System.Data.Common.DbDataAdapter da = DB.CreateDataAdapter(myCommand);
			DataSet ds = new DataSet("ds");
			da.Fill(ds, offset, numRows, "Orders");
			return ds.Tables[0];
		}
//		public IDataReader GetSomeRecords_RO(int count) {
//			String strQuery= string.Format("SELECT top {0} from {1}", count.ToString(), TableName)  ;
//			System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand(strQuery, DB.Connection);				
//			return myCommand.ExecuteReader();
//		}
	}
}
