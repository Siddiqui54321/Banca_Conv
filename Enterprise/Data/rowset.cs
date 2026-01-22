using System;

namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for rowset.
	/// </summary>
	public class rowset
	{
		private System.Data.DataTable _rowset = new System.Data.DataTable();
		private int currentRecord;
		private string prevColName =null;
		private int prevColIndex = -1;
		public rowset(System.Data.DataTable _dataTable)
		{
			_rowset = _dataTable;
			currentRecord = -1;
		}

		//TODO Give the option to traverse the deleted rows

       /* public string[][] getItemArray()
        { 
            string[][] arry = new string[1][];
            return arry; 
            //_rowset.
        }
        */

        public rowset Copy()
        {
            return new rowset(_rowset.Copy());
        }

		public bool next() 
		{
			if(currentRecord +1 < _rowset.Rows.Count) 
			{
				if(_rowset.Rows[currentRecord+1].RowState != System.Data.DataRowState.Deleted)
				{
					currentRecord++;
					return true;
				}
				else
				{
					while(currentRecord +1 < _rowset.Rows.Count && _rowset.Rows[currentRecord+1].RowState == System.Data.DataRowState.Deleted)
					{
						currentRecord++;
					}
					if(currentRecord +1 < _rowset.Rows.Count)
						return true;
				}
				return false;
			}
			else 
			{
				return(false);
			}
		}

		public bool previous()
		{
			if(currentRecord  >= 0 )
			{
				currentRecord--;
				return(true);
			}
			else
			{
				return(false);
			}
		}
		public bool  absolute(int rowIndex)
		{
			if(rowIndex <= _rowset.Rows.Count-1)
			{
				currentRecord = rowIndex-1;
				return(false);
			}
			else
				return(false);
		}

		public int size() 
		{
			return(_rowset.Rows.Count);
		}

		public void beforeFirst(){
			currentRecord = -1;
		}
		public bool isFirst() 
		{
			if(currentRecord==0 && _rowset.Rows.Count>0)
				return(true);
			else
				return(false);
		}

		public bool isLast() 
		{
			if(currentRecord+1 == _rowset.Rows.Count && _rowset.Rows.Count>0)
				return(true);
			else
				return(false);
		}
		
		public int getRow()
		{
			return(currentRecord + 1);
		}

		public void close()
		{
			//_rowset.Dispose();
		}

		public object getObject(int colIndex)
		{
			colIndex--;
			prevColIndex = colIndex;
			prevColName = null;
			if(_rowset.Rows[currentRecord][colIndex] == DBNull.Value)
				return null;
			else
				return _rowset.Rows[currentRecord][colIndex];
		}

		public object getObject(String colName)
		{
			prevColIndex = -1;
			prevColName = colName;
			if(_rowset.Rows[currentRecord][colName]==DBNull.Value)
				return null;
			else
				return _rowset.Rows[currentRecord][colName];
		}

		public string getString(int colIndex)
		{
			colIndex--;
			prevColIndex = colIndex;
			prevColName = null;
			return (_rowset.Rows[currentRecord][colIndex] == System.DBNull.Value ? null:_rowset.Rows[currentRecord][colIndex].ToString());
		}

		public string getString(String colName)
		{
			prevColIndex = -1;
			prevColName = colName;
			return (_rowset.Rows[currentRecord][colName] == System.DBNull.Value? null : _rowset.Rows[currentRecord][colName].ToString());
		}
		public int getInt(int colIndex)
		{
			colIndex--;
			prevColIndex = colIndex;
			prevColName = null;
			if(_rowset.Rows[currentRecord][colIndex]==DBNull.Value)
				return 0;
			else
				return  Convert.ToInt32(_rowset.Rows[currentRecord][colIndex]);
		}

		public int getInt(String colName)
		{
			prevColIndex = -1;
			prevColName = colName;
			if(_rowset.Rows[currentRecord][colName] == DBNull.Value)
				return 0;
			else
				return Convert.ToInt32(_rowset.Rows[currentRecord][colName]);
		}
		public double getDouble(int colIndex)
		{
			colIndex--;
			prevColIndex = colIndex;
			prevColName = null;
			if(_rowset.Rows[currentRecord][colIndex]== DBNull.Value)
				return 0;
			else
				return Convert.ToDouble(_rowset.Rows[currentRecord][colIndex]);
		}

		public double getDouble(String colName)
		{
			prevColIndex = -1;
			prevColName = colName;
			if(_rowset.Rows[currentRecord][colName] == DBNull.Value)
				return 0;
			else
				return Convert.ToDouble(_rowset.Rows[currentRecord][colName]);
		}

		public DateTime  getDate(int colIndex)
		{
			colIndex--;
			prevColIndex = colIndex;
			prevColName = null;
			if(_rowset.Rows[currentRecord][colIndex]==DBNull.Value)
				throw new Exception("Found Null while retrieving date from rowset");
			else
				return Convert.ToDateTime(_rowset.Rows[currentRecord][colIndex]);
		}

		public DateTime getDate(String colName)
		{
			prevColIndex = -1;
			prevColName = colName;
			if(_rowset.Rows[currentRecord][colName] == DBNull.Value)
				throw new Exception("Found Null found while retrieving data from rowset");
			else
				return Convert.ToDateTime(_rowset.Rows[currentRecord][colName]);
		}

		public bool isNull(String colName) {
			Object obj = _rowset.Rows[currentRecord][colName];
			if(obj==DBNull.Value)
				return true;
			else
				return false;
		}

		public bool isNull(int colIndex) {
			colIndex--;
			Object obj = _rowset.Rows[currentRecord][colIndex];
			if(obj==DBNull.Value)
				return true;
			else
				return false;
		}

		public bool wasNull(){
			Object obj=null;
			if (prevColName ==null)
				obj = _rowset.Rows[currentRecord][prevColIndex];
			else if(prevColName !=null)
				obj = _rowset.Rows[currentRecord][prevColName];
			if(obj==DBNull.Value)
				return true;
			else
				return false;

		}

		public void updateDouble(String colName,double _value){
			_rowset.Rows[currentRecord][colName] = _value;
	}


	}

}
