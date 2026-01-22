using System;
using System.Web;
using System.Text;
using System.Data;
using SHMA.Enterprise.Presentation;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Collections;
using DatePopUp = SHMA.Enterprise.Presentation.WebControls.DatePopUp;
namespace SHMA.Enterprise{
	public class Utilities{
		public static string GetArrayData(System.Data.IDataReader dr){
			StringBuilder sb = new StringBuilder();
			String str = "";
			int i;
			int j = dr.FieldCount;
			while (dr.Read()){
				sb.Append("'");
				for (i=0; i<j; i++){					
					sb.Append(dr.GetValue(i).ToString());
					sb.Append("~");
				}
				sb = sb.Remove(sb.Length-1,1);
				sb.Append("',");
			}
			if (sb.Length>0){
				sb.Remove(sb.Length-1,1).ToString(); 
				sb.Replace("\v", "");
				sb.Replace("\n", "");
				str= sb.ToString();
			}
			return str;
		}
		public static string GetArrayDataTabular(System.Data.IDataReader dr){
			StringBuilder sb = new StringBuilder();
			string str = "";
			int i;
			int j = dr.FieldCount; 
			while (dr.Read()){
				sb.Append("'");
				for (i=0; i<j; i++) {					
					sb.Append(dr.GetValue(i).ToString());
					sb.Append("~");
				}
				sb = sb.Remove(sb.Length-1,1);
				sb.Replace('~','^',sb.ToString().LastIndexOf("~"),1).ToString();
				sb.Append("',");
			}
			if (sb.Length>0){
				sb.Remove(sb.Length-1,1).ToString(); 
				sb.Replace("\v", "");
				sb.Replace("\n", "");
				str= sb.ToString();
			}

			return str;
		}

		public static NameValueCollection RowToNameValue(DataRow row) {
			NameValueCollection columnNameValue = new NameValueCollection();
			foreach(DataColumn column in row.Table.Columns) {
				columnNameValue.Add(column.ColumnName, row[column.ColumnName]);
			}
			return columnNameValue;
		}
		public static NameValueCollection Row2NameValue(DataRow row) {
			NameValueCollection columnNameValue = new NameValueCollection();
			foreach(DataColumn column in row.Table.Columns) {
				columnNameValue.Add(column.ColumnName, row[column.ColumnName]);
			}
			return columnNameValue;
		}		

		public static void Reader2Table(IDataReader reader, DataTable table) 
		{
			// written by Syed Zulfiqar
			System.Data.DataTable _table = reader.GetSchemaTable();
			System.Data.DataColumn _dc;
			System.Data.DataRow _row;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < _table.Rows.Count; i ++) 
			{
				_dc = new System.Data.DataColumn();
				object col  = _table.Rows[i]["ColumnName"];
				string colName = (col==null||col.ToString()==String.Empty)?i.ToString(): (String)col;
				if (! table.Columns.Contains(colName)) 
				{
					//_dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
					_dc.ColumnName = colName;
					_dc.DataType = Type.GetType(_table.Rows[i]["DataType"].ToString());
					_dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
					_dc.AllowDBNull = true; 
					_dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
					_al.Add(_dc.ColumnName);
					table.Columns.Add(_dc);
				}
			}
			while (reader.Read()) 
			{
					_row = table.NewRow();
					for ( int i = 0; i < _al.Count; i++)
					{
						//_row[((System.String) _al[i])]=reader[(string)_al[i]]
						if(((string)_al[i]).Length == i.ToString().Length && ((string)_al[i]).IndexOf(i.ToString())==0 && int.Parse(((string)_al[i]))==i)
							_row[((System.String) _al[i])] = reader[i];
						else
							_row[((System.String) _al[i])]=reader[(string)_al[i]];
					} 
					table.Rows.Add(_row);
			}
		}

		public static int Reader2Table(IDataReader reader, DataTable table, int pageSize,int page) {
			// written by Shahid Siddiqui
			if (page < 1)			page = 1;
			if (pageSize < 0)		pageSize = 1;			
			System.Data.DataTable _table = reader.GetSchemaTable();
			System.Data.DataColumn _dc;
			System.Data.DataRow _row;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < _table.Rows.Count; i ++) {
				_dc = new System.Data.DataColumn();
				if (! table.Columns.Contains(_table.Rows[i]["ColumnName"].ToString())) {
					_dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
					// Added for milliseconds problem
					_dc.DataType = Type.GetType(_table.Rows[i]["DataType"].ToString());

					_dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
					_dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]); 
					_dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
					_al.Add(_dc.ColumnName);
					table.Columns.Add(_dc);
				}
			}
			int totalRecords = 0;
			int startIndex = ((page-1) * pageSize) + 1;
			int endIndex = startIndex+pageSize;
			while (reader.Read()) {
				totalRecords++;
				if (totalRecords >= startIndex && totalRecords < endIndex){
					_row = table.NewRow();
					for ( int i = 0; i < _al.Count; i++) {
						_row[((System.String) _al[i])] = reader[(string)_al[i]];
					} 
					table.Rows.Add(_row);
				}				
			}
			return totalRecords;
		}

		public static int Reader2Table(IDataReader reader, DataTable table, int pageSize , string[] pKeys, out int foundAt) {
			// written by Shahid Siddiqui
			foundAt = 1;
			if (pageSize < 0)		pageSize = 1;			
			System.Data.DataTable _table = reader.GetSchemaTable();
			System.Data.DataColumn _dc;
			System.Data.DataRow _row;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < _table.Rows.Count; i ++) {
				_dc = new System.Data.DataColumn();
				if (! table.Columns.Contains(_table.Rows[i]["ColumnName"].ToString())) {
					_dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
					// Added for milliseconds problem
					_dc.DataType = Type.GetType(_table.Rows[i]["DataType"].ToString());

					_dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
					_dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]); 
					_dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
					_al.Add(_dc.ColumnName);
					table.Columns.Add(_dc);
				}
			}
			int totalRecords=0;
			int foundRecord = 0;
			bool pageFound=false;
			bool EOF=true;
			int pageRecords = 0;
			if (reader.Read()){
				EOF = false;
				totalRecords ++;
				pageRecords ++ ;
			}
			//table.Rows.Clear();
			while ((!pageFound ) && (!EOF)){				
				pageRecords = 1;
				while ((!EOF) && (pageRecords<=pageSize)) {
					//if (totalRecords >= startIndex && totalRecords < endIndex){
					_row = table.NewRow();
					for ( int i = 0; i < _al.Count; i++) {
						_row[((System.String) _al[i])] = reader[(System.String) _al[i]];
					} 
					table.Rows.Add(_row);					
					if (!pageFound){
						pageFound = true;
						foundRecord = totalRecords;
						foreach (string pKey in pKeys){
							if (_row[pKey].ToString() != SessionObject.GetString(pKey)){
								pageFound=false;
								break;
							}
						}
					}
					EOF = !reader.Read();
					
					if (!EOF){
						totalRecords++;
						pageRecords++;
					}
				}
			}
			while (reader.Read()){
				totalRecords++;
			}
			if (pageFound){
				foundAt = ((int)(foundRecord-1) / pageSize)+1;
				int recordsToBeCleared = foundRecord - (((foundRecord-1) % pageSize)+1);
				for (int i = 0; i<recordsToBeCleared; i++){
					table.Rows[0].Delete();
				}
			}
			else{
				for (int i = pageSize; i<totalRecords; i++){
					table.Rows[pageSize].Delete();
				}
			}
			return totalRecords;
		}

		public static int Reader2Table(IDataReader reader, DataTable table, int pageSize , string[] pKeys) {
			// written by Shahid Siddiqui
			if (pageSize < 0)		pageSize = 1;			
			System.Data.DataTable _table = reader.GetSchemaTable();
			System.Data.DataColumn _dc;
			System.Data.DataRow _row;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < _table.Rows.Count; i ++) {
				_dc = new System.Data.DataColumn();
				if (! table.Columns.Contains(_table.Rows[i]["ColumnName"].ToString())) {
					_dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
					// Added for milliseconds problem
					_dc.DataType = Type.GetType(_table.Rows[i]["DataType"].ToString());

					_dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
					_dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]); 
					_dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
					_al.Add(_dc.ColumnName);
					table.Columns.Add(_dc);
				}
			}
			int totalRecords=0;
			int foundRecord = 0;
			bool pageFound=false;
			bool EOF=true;
			int pageRecords = 0;
			if (reader.Read()){
				EOF = false;
				totalRecords ++;
				pageRecords ++ ;
			}
			//table.Rows.Clear();
			while ((!pageFound ) && (!EOF)){				
				pageRecords = 1;
				while ((!EOF) && (pageRecords<pageSize)) {
					//if (totalRecords >= startIndex && totalRecords < endIndex){
					_row = table.NewRow();
					for ( int i = 0; i < _al.Count; i++) {
						_row[((System.String) _al[i])] = reader[(System.String) _al[i]];
					} 
					table.Rows.Add(_row);					
					if (!pageFound){
						pageFound = true;
						foundRecord = totalRecords;
						foreach (string pKey in pKeys){
							if (_row[pKey].ToString() != SessionObject.GetString(pKey)){
								pageFound=false;
								break;
							}
						}
					}
					EOF = !reader.Read();
					
					if (!EOF){
						totalRecords++;
						pageRecords++;
					}
				}
			}
			while (reader.Read()){
				totalRecords++;
			}
			if (pageFound){
				int recordsToBeCleared = foundRecord - (foundRecord % pageSize);
				for (int i = 0; i<recordsToBeCleared; i++){
					table.Rows[0].Delete();
				}
	

			}
			else{
				for (int i = pageSize; i<totalRecords; i++){
					table.Rows[pageSize].Delete();
				}
			}
			return totalRecords;
		}

		public static int Reader2Table(IDataReader reader, DataTable table, int pageSize,int page,  NameValueCollection totalValues,  NameValueCollection grandTotalValues) 
		{
			SortedList totalColumns = (SortedList)totalValues.Clone() ;
			SortedList grandTotalColumns = (SortedList)grandTotalValues.Clone() ;
			// written by Shahid Siddiqui
			if (page < 1) page = 1;
			if (pageSize < 0) pageSize = 1; 
			System.Data.DataTable _table = reader.GetSchemaTable();
			System.Data.DataColumn _dc;
			System.Data.DataRow _row;
			System.Collections.ArrayList _al = new System.Collections.ArrayList();
			for (int i = 0; i < _table.Rows.Count; i ++) 
			{
				_dc = new System.Data.DataColumn();
				if (! table.Columns.Contains(_table.Rows[i]["ColumnName"].ToString())) 
				{
					_dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
					// Added for milliseconds problem
					_dc.DataType = Type.GetType(_table.Rows[i]["DataType"].ToString());
					_dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
					_dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]); 
					_dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
					_al.Add(_dc.ColumnName);
					table.Columns.Add(_dc);
				}
			}
			int totalRecords = 0;
			int startIndex = ((page-1) * pageSize) + 1;
			int endIndex = startIndex+pageSize;
			while (reader.Read()) 
			{
				totalRecords++;
				if (totalRecords >= startIndex && totalRecords < endIndex)
				{
					_row = table.NewRow();
					for ( int i = 0; i < _al.Count; i++) 
					{
						_row[((System.String) _al[i])] = reader[(string)_al[i]];
					} 
					// <<<< appended for Total 
					foreach (object key in totalColumns.Keys){
						if ( reader[((string)key)] != DBNull.Value )
							//R//totalValues[key] = ((float)totalValues[key]) + float.Parse( reader[((string)key)].ToString() );
						    totalValues[key] = ((Decimal)totalValues[key]) + Convert.ToDecimal(reader[((string)key)].ToString() );
						//grandTotalValues[key] = ((float)grandTotalValues[key]) + float.Parse( reader[((string)key)].ToString() );							
					}
					foreach (object key in grandTotalColumns.Keys){
						if ( reader[((string)key)] != DBNull.Value )
							//R//grandTotalValues[key] = ((float)grandTotalValues[key]) + float.Parse( reader[((string)key)].ToString() );							
						    grandTotalValues[key] = ((Decimal)grandTotalValues[key]) + Convert.ToDecimal( reader[((string)key)].ToString() );							
						//totalValues[key] = ((float)totalValues[key]) + float.Parse( reader[((string)key)].ToString() );
						
					}
					table.Rows.Add(_row);
					//appended for Total >>>>
				} 
					// <<<< appended for Grand Total 
				else
				{
					foreach (object key in grandTotalColumns.Keys )
					{
						if (reader[((string)key)] != DBNull.Value )
							//R//grandTotalValues[key] = ((float)grandTotalValues[key]) + float.Parse( reader[((string)key)].ToString() );
						    grandTotalValues[key] = ((Decimal)grandTotalValues[key]) + Convert.ToDecimal( reader[((string)key)].ToString() );
					} 
				} 
				// appended for Total >>>>>
			}
			return totalRecords;
		}


		public static int PaginateTable(DataTable _table, DataTable table, int pageSize,int page) 
		{ // to be inserted in Enterprise Utilities(Inserted bu sufyan Islam)
			if (page < 1)			page = 1;
			if (pageSize < 0)		pageSize = 1;			
			System.Data.DataColumn _newCol;
			System.Data.DataRow _newRow;
			foreach(DataColumn _dc in _table.Columns) 
			{
				_newCol = new System.Data.DataColumn();
				if (! table.Columns.Contains(_dc.ColumnName.ToString()) )
				{
					_newCol.ColumnName = _dc.ColumnName.ToString();
					_newCol.DataType = Type.GetType(_dc.DataType.ToString());
					_newCol.Unique = _dc.Unique;
					_newCol.AllowDBNull = _dc.AllowDBNull; 
					_newCol.ReadOnly = _dc.ReadOnly;
					table.Columns.Add(_newCol);
				}
			}
			int totalRecords = 0;
			int startIndex = ((page-1) * pageSize) + 1;
			int endIndex = startIndex+pageSize;
			foreach(DataRow row in _table.Rows)
			{
				totalRecords++;
				if (totalRecords >= startIndex && totalRecords < endIndex)
				{
					_newRow = table.NewRow();
					_newRow.ItemArray  = row.ItemArray;
					table.Rows.Add(_newRow);
				}				
			}
			return totalRecords;
		}

		public static string File2EntityID(string name) 
		{
			string id = null;
			//Rehan Code to test Transaction Routing Fail in
			//System.IO.StreamWriter strW = new System.IO.StreamWriter("c:\\UtilityLog.txt",true);
			//strW.WriteLine("Entity Name : " + name); 
			//
			name = name.ToString().Replace("_ASP.","").Replace("ASP.","").Replace("_aspx","").Replace(".aspx","");		
			int n = name.Length;
			id =  "shgn_ts_se_tblscreen_";
			int a = name.IndexOf(id);
			if (a < 0) {
				id  = "shgn_gs_se_stdgridscreen_";
				a = name.IndexOf(id);
			}
			if (a < 0) {
				id  = "shgn_ss_se_stdscreen_";
				a = name.IndexOf(id);
			}
			if (a < 0) {
				id  = "shgn_dh_se_enquiry_";
				a = name.IndexOf(id);
			}
			if (a < 0) 
			{
				id  = "shgn_gp_gp_";
				a = name.IndexOf(id);
			}
			if (a < 0) 
			{
				id  = "shgn_ca_gp_";
				a = name.IndexOf(id);
			}
			if (a < 0) 
			{
				id = "Not a Valid Entity Name";
			}
			else {
				
				a = a + id.Length;
				id = name.Substring(a,(n-a));
			}
			//strW.WriteLine("Entity id : " + id); 
			//strW.Close(); 
			return id.ToUpper();
		}
		public static bool childRecordExist(DataRow row){									
			bool exist = false;
			DataRelationCollection relations = row.Table.ChildRelations;
			foreach (DataRelation relation in relations){
				if (row.GetChildRows(relation).Length>0)
					exist = true;
			}

			return exist;
		}

		public static double DoubleParse(string doubleString){						
			try{
				return double.Parse(doubleString);
			}			
			catch(FormatException e){
				Console.WriteLine(e.Message);
				return double.MinValue;
			}	
		}
		public static DateTime DateParse(string dateString){						
			try{
				return DateTime.Parse(dateString);
			}			
			catch(FormatException e){
				Console.WriteLine(e.Message);
				return DateTime.MinValue;
			}	
		}

		public static object ParseDBNull(object obj){
			object returnObj = obj;
			if (obj == null){
				returnObj = DBNull.Value;
			}
			return returnObj;
		}

		public static string DBVals2String(object DBDate){
			string val = null;
			if (DBDate is DateTime) {
				val = ((DateTime) DBDate).ToShortDateString();
			}
			else if (DBDate.Equals(DBNull.Value)){
				val = "";
			}
			else {
				val = "";
			}

			return val;

		}

		public static bool stringOfZeros(string s) {	
			bool flag = true;
			if (s!=null) {
				s = s.Replace('0', ' ');
				s = s.Trim();
				if (!s.Equals("")) {
					flag = false;
				}
			}
			return flag;
		}
		object GetFieldValue(System.Web.UI.Control control){
			if (control is TextBox)
				return ((TextBox)control).Text;
			else if (control is DropDownList)
				return ((DropDownList)control).SelectedValue;
			else if (control is SHMA.Enterprise.Presentation.WebControls.DatePopUp)
				return ((SHMA.Enterprise.Presentation.WebControls.DatePopUp)control).Text;
			else
				return null;
		}
		public static string Array2String(string[] arrayOfString){
			return "";
		}
		public static WebControl GetFieldByColumnName(string colName, ControlCollection formFields){
			WebControl field = null;
			if (colName!=null && colName.Length>0){				
				foreach(Control control in formFields){
					if (control.ID != null){
						if (control.ID.IndexOf(colName)>=0){
							field = (WebControl)control;
							break;
						}
					}
				}
			}
			return field;
		}
		
		public static string GetFieldValue(string colName, ControlCollection formFields){
			string fieldValue = "";
			foreach(Control control in formFields){
				if (control.ID != null){
					if (control.ID.IndexOf(colName) >= 0)
					{
						if(control.ID.Substring(control.ID.IndexOf(colName)).Equals(colName))
						{
							if (control is TextBox)
							{
								fieldValue = ((TextBox)control).Text;
							}					
							else if (control is DropDownList)
							{
								fieldValue = ((DropDownList)control).SelectedValue ;
							}
							else if (control is DatePopUp)
							{
								fieldValue = ((DatePopUp)control).Text ;
							}
						}
					}
				}
			}
			return fieldValue;
		}
		public static NameValueCollection GetPKNameValue(string[] pKeys, ControlCollection formFields){
			NameValueCollection colNameValue = new NameValueCollection();
			foreach(string pKey in pKeys){
				colNameValue.Add(pKey, GetFieldValue(pKey, formFields));
			}
			return colNameValue;
		}
		void RegisterArrayDeclaration(Page page){
				
		}
		public static void DisableChildControls(Control parent){
			foreach(Control control in parent.Controls){
				if (control is WebControl){
					if (control is TextBox){
						((TextBox)control).ReadOnly = true;
					}
					else if (control is DatePopUp){
						((DatePopUp)control).ReadOnly = true;
					}
					else{
						((WebControl)control).Enabled = false;
					}
				}
			}				
		}
		public static void HideChildControls(Control parent){
			foreach(Control control in parent.Controls){
				if (control is WebControl){
					((WebControl)control).Attributes.Add("visibility", "hidden");
				}
			}				
		}		
		public static void SetEditEventOnGridItems(DataGridItem item){
			foreach(Control control in item.Cells[0].Controls){
				if (control is WebControl){
					((WebControl)control).Attributes.Add("onchange" ,"SetStatus('" + item.ItemIndex.ToString()+"');");
				}
			}							
		}
		public static string GetRowFieldValueByColName(string colName, DataGridItem item){
			if (colName!=null && colName.Length>0){				
				foreach (TableCell cell in item.Cells){
					foreach(Control control in cell.Controls){
						if (control.ID != null && control.ID.IndexOf(colName) >= 0){
							if (control is TextBox){
								return ((TextBox)control).Text;					
							}
							else if (control is DropDownList){
								return ((DropDownList)control).SelectedValue;					
							}
							else {
								throw new ApplicationException(" Error in Utilities.GetRowFieldByColName, unsupported control type!");

							}

							
						}
					}
				}
			}
			return null;
		}
		/*Replaced by Ahemd to add control type check on 21-02-06
		 * *******************************************************

		public static WebControl GetRowFieldByColName(string colName, DataGridItem item){
			if (colName!=null && colName.Length>0){				
				foreach (TableCell cell in item.Cells){
					foreach(Control control in cell.Controls){
						if (control.ID != null && control.ID.IndexOf(colName) >= 0)
							return (WebControl)control;					
					}
				}
			}
			return null;
		}
*/
		public static WebControl GetRowFieldByColName(string colName, DataGridItem item){
			if (colName!=null && colName.Length>0){				
				foreach (TableCell cell in item.Cells){
					foreach(Control control in cell.Controls)
					{
						if (control.ID != null && control.ID.IndexOf(colName) >= 0)
						{
							if (control is TextBox || control is DropDownList || control is DatePopUp )
								return (WebControl)control;					
						}					
					}
				}
			}
			return null;
		}
		
		public static void PutNonPKInSession(string[] PKs, ControlCollection allControls)
		{
			bool notPK = true;
			foreach (Control control in allControls){
				notPK = true;
				if (control is TextBox || control is DropDownList || control is DatePopUp){
					foreach (string pk in PKs){
						if (control.ID.IndexOf(pk)>=0){
							notPK=false;
							break;
						}
					}	
					if (notPK){						
						if (control is TextBox){
							SessionObject.Add( control.UniqueID.Replace("txt","") , ((TextBox)control).Text);
						}					
						else if (control is DropDownList){
							SessionObject.Add( control.UniqueID.Replace("ddl",""), ((DropDownList)control).SelectedValue);
						}							
						else if (control is DatePopUp){
							SessionObject.Add( control.UniqueID.Replace("txt",""), ((DatePopUp)control).Text);
						}		
					}											
				}
			}
		}

		public static void ReplaceItems( NameValueCollection source, NameValueCollection destination ){
			if (source!=null && destination!=null){
				foreach(string pKey in source.Keys){
					if (destination.ContainsKey(pKey)){
						destination[pKey] = source[pKey];
					}				
				}
			}
		}
		public static void AddPKItems( NameValueCollection source, NameValueCollection destination , string[] pKeys){
			if (source!=null && destination!=null){			
				foreach(string pKey in pKeys){
					if (!destination.ContainsKey(pKey)){
						destination.add(pKey, source[pKey]);
					}				
				}
			}
		}
		public static string GetControlValue(WebControl control){
			string fieldValue=null;
			if (control is TextBox){
				fieldValue =((TextBox)control).Text;
			}					
			else if (control is DropDownList){
				fieldValue =((DropDownList)control).Items[((DropDownList)control).SelectedIndex].Value;
				//fieldValue =((DropDownList)control).SelectedValue;
			}							
			else if (control is DatePopUp){
				fieldValue =((DatePopUp)control).Text;
			}	
			return fieldValue;
		}
		
        private static bool GetListerVisibility(Page page)
		{
			return !((page.Request["operation"] == "View") && (page.Request["ShowSingleRecordInView"] == "Y")) ;
		}

        private static double GetPageVersion(Page page)
        {
            PageController thisPage = (PageController)page;
            double pageversion = thisPage.pageVersion;
            return pageversion;
        }

		public static void SetListerVisibility(Page page)
		{

            double pageVersion = GetPageVersion(page);

            if (pageVersion == 2.5)
            {
                HtmlGenericControl ListerDiv = (HtmlGenericControl)page.FindControl("ListerDiv");
                HtmlGenericControl PagerDiv = (HtmlGenericControl)page.FindControl("PagerDiv");
                HtmlInputButton btnHideLister = (HtmlInputButton)page.FindControl("btnHideLister");
                HtmlGenericControl GridDiv = (HtmlGenericControl)page.FindControl("EntryTableDiv");


                if (!GetListerVisibility(page))
                {
                    ListerDiv.FindControl("ListerFieldSet").Controls.Clear();

                    if (PagerDiv != null)
                    {
                        PagerDiv.Style.Add("visibility", "hidden");
                    }

                    if (btnHideLister != null)
                    {
                        ListerDiv.Style.Add("visibility", "hidden");
                        GridDiv.Attributes.Add("class", "GridDiv");
                    }
                }
                else
                {
                    if (PagerDiv != null)
                    {
                        PagerDiv.Style.Add("visibility", "visible");
                    }

                    if (btnHideLister != null)
                    {
                        ListerDiv.Style.Add("visibility", "visible");
                        GridDiv.Attributes.Add("class", "GridDivWithLister");
                    }
                }

            }
            else
            {
                HtmlGenericControl ListerDiv = (HtmlGenericControl)page.FindControl("ListerDiv");
                DropDownList pagerList = (DropDownList)ListerDiv.FindControl("pagerList");
                HtmlInputButton btnHideLister = (HtmlInputButton)page.FindControl("btnHideLister");
                HtmlGenericControl GridDiv = (HtmlGenericControl)page.FindControl("EntryTableDiv");


                if (!GetListerVisibility(page))
                {
                    ListerDiv.FindControl("ListerFieldSet").Controls.Clear();
                    pagerList.Enabled = false;
                    if (btnHideLister != null)
                    {

                        ListerDiv.Style.Add("visibility", "hidden");
                        GridDiv.Attributes.Add("class", "GridDiv");
                        //GridDiv. 
                    }
                }
                else
                {
                    pagerList.Enabled = true;

                    if (btnHideLister != null)
                    {

                        ListerDiv.Style.Add("visibility", "visible");
                        GridDiv.Attributes.Add("class", "GridDivWithLister");
                        //GridDiv. 
                    }
                }

            }
		
		}

	}
}
//if (control==null) {
//			control = cell.FindControl("lbl" + colName);
//			if (control==null){
//				control = cell.FindControl("ddl" + colName);
//				if (control==null){
//					control = cell.FindControl("txtNew" + colName);
//					if (control==null){			
//						control = cell.FindControl("lblNew" + colName);
//						if (control==null){
//							control = cell.FindControl("ddlNew" + colName);
//						}
//					}
//				}
//			}			
