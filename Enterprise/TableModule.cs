using System;
namespace SHMA.Enterprise{
	/// <summary>
	/// Summary description for TableModule.
	/// </summary>
	public class TableModule{
		protected DataHolder dataHolder;
		protected System.Data.DataTable table;
		public TableModule(){}
		
		public TableModule(DataHolder dataHolder, String TableName)	{
			this.dataHolder = dataHolder;
			if (this.dataHolder.Data.Tables.IndexOf(TableName) >= 0){
				table = this.dataHolder.Data.Tables[TableName];
			}
			else{
				//table = this.dataHolder.Data.Tables[TableName];
				// Throw Exception Class object
			}	
		}
		
		public System.Data.DataRow this[int rowIndex]{
			get{
				return this.table.Rows[rowIndex];  
			}
			
		}

		

	}
}
