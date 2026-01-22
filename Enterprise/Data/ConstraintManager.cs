using System;
using System.Data.OleDb;
using System.Data;
using SHMA.Enterprise.Data;

namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for ConstraintManager.
	/// </summary>
	public class ConstraintManager
	{
		public ConstraintManager()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void AddOraPKIfMissing(DataTable table)
		{


			if ((table== null) || (table.Columns == null) || (table.Columns.Count<=0))
			{
				throw new ApplicationException("Data Table has no columns -- unable to set primary keys");
			}
			else if ((table.PrimaryKey == null) || (table.PrimaryKey.Length<=0))
			{
				DataColumnCollection cols = table.Columns;
				//string tableName = table.TableName;
				string[] keyColNames = getKeyColumnNames(table.TableName);
				DataColumn[] keys =  new DataColumn[keyColNames.Length];
				for (int i=0; i<keys.Length; i++)
				{
					DataColumn column = cols[keyColNames[i]];
				}

				table.PrimaryKey = keys;
			}
		}
		
		public static string[] getKeyColumnNames(string tableName)
		{
			string[] keyCols = null;
			string query = "SELECT a.owner, a.table_name, b.column_name FROM all_constraints a, all_cons_columns b WHERE a.constraint_type='P' AND a.constraint_name=b.constraint_name AND a.table_name = ?	";	
			ParameterCollection ParaCollection = new ParameterCollection();
			ParaCollection.puts("@table_name", tableName);
	
			rowset rs = DB.executeQuery(query, ParaCollection);
			
			if (!rs.next())
			{
				keyCols = new string[rs.size()];
				for (int i = 0; i< rs.size();i++){
					keyCols[i] = rs.getString("COLUMN_NAME");
				}
			}
			else
			{
				keyCols = new string[0];
			}
			
			return keyCols;
		}

}
}
