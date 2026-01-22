using System;
using System.IO;
using System.Web;
using System.Data;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	/// <summary>
	/// Summary description for CSVFileGeneration.
	/// </summary>
	public class CSVFileGeneration
	{
		public CSVFileGeneration()
		{

		}
		public CSVFileGeneration(string path, string fileName, DataTable dt)
		{		
			this.physicalPath = string.Concat(path,"\\",fileName);
			this.dt = dt;
		}
		public CSVFileGeneration(string path, string fileName)
		{	
			this.physicalPath = string.Concat(path,"\\",fileName);			
		}
		public void generateFile()
		{
			StreamWriter sw = null;
			try
			{				
				sw = new StreamWriter(this.physicalPath);
				WriteToFile(sw);
			}
			catch(Exception ex)
			{
				throw new ProcessException(ex.Message);
			}
			finally
			{
				sw.Close();
				this.dt = null;
			}
		}
		public string[] ReadFromFile()
		{
			StreamReader sr = null;
			string[] rows = null;
			int index = 0;
			try
			{
				sr = new StreamReader(this.physicalPath);				
				while (sr.Peek() >= 0)
				{
					rows[index] = sr.ReadLine().Split(',')[0];
					index++;
				}				
			}
			catch(Exception ex)
			{
				throw new ProcessException(ex.Message);
			}
			return rows;
		}
		private void WriteToFile(StreamWriter sw)
		{
			int iCol = this.dt.Columns.Count-1;
			int iRow = this.dt.Rows.Count;
			for(int j = 0; j < iRow; j++)
			{
				DataRow dRow = dt.Rows[j];
				for (int i = 1; i < iCol; i++)
				{
					if (!Convert.IsDBNull(dRow[i]))
					{
						sw.Write(dRow[i].ToString());
					}
					if ( i < iCol - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
			}
		}		

		private string physicalPath = string.Empty;
		private DataTable dt = null;
	}
}
