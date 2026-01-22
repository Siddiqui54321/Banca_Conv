using System;
using System.Xml;

namespace SHMA.Enterprise.xml
{
	/// <summary>
	/// Summary description for xmlReader.
	/// </summary>
	public class xmlReader
	{
		
		string xmlFile="";
		private static System.Data.DataTable FilterTable;
		public xmlReader()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string xmlFileName 
		{
			set{xmlFile=value;}
			get{return xmlFile;}
		}
		
		public System.Data.DataTable ReadFilters2Table()
		{
			if(FilterTable!=null && FilterTable.Rows.Count>0)
				return FilterTable;
			System.Data.DataTable table=new System.Data.DataTable("ReadFilters2Table");
			XmlTextReader txtReader = new XmlTextReader(xmlFile);
			table.Columns.Add("EntityName");
			table.Columns.Add("FieldName");
			table.Columns.Add("FilterType");
			table.Columns.Add("FieldLength");
			string entityName = "", fieldName="",filterType="",fieldLength="";
			bool recordCompleted=false;
			try
			{
				while(txtReader.Read())
				{
					XmlNodeType nType = txtReader.NodeType;
					if(nType == XmlNodeType.Element && txtReader.GetAttribute("EntityName")!=null && txtReader.GetAttribute("EntityName")!="")
						entityName = txtReader.GetAttribute("EntityName");
					else if(nType == XmlNodeType.Element && txtReader.GetAttribute("FieldName") !=null && txtReader.GetAttribute("FieldName")!="")
						fieldName = txtReader.GetAttribute("FieldName");
					else if(nType == XmlNodeType.Element && txtReader.GetAttribute("FilterType") !=null && txtReader.GetAttribute("FilterType")!="")
					{
						filterType = txtReader.GetAttribute("FilterType");
					}
					else if(nType == XmlNodeType.Element && txtReader.GetAttribute("FieldLength") !=null && txtReader.GetAttribute("FieldLength")!="")
					{
						fieldLength = txtReader.GetAttribute("FieldLength");
						recordCompleted = true;
					}
				
					if(recordCompleted)
					{
						table.Rows.Add(new object[] {entityName.ToUpper(), fieldName.ToUpper(),filterType.ToUpper(),fieldLength.ToUpper()});
						recordCompleted=false;
					}
				}
			}
			catch(System.Exception e)
			{
				throw new System.Exception("Unrecognized Filter XML File" + e.Message.ToString());
			}
			FilterTable= table;
			return FilterTable;
		}
	}
}
