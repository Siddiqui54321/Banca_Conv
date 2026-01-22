using System;
using System.Web;
namespace SHMA.Enterprise.Presentation.Filters
{
	/// <summary>
	/// Summary description for FieldLengthFilter.
	/// </summary>
	public class ProgrammableLengthFilter:BaseFieldFilter
	{
		
		static System.Data.DataTable filterTable=null;

		public override void applyFilter(string cntrlName, System.Web.UI.Control cntrl)
		{
			string entity = getEntityName();
			if(filterTable ==null)
			{
				SHMA.Enterprise.xml.xmlReader reader=new SHMA.Enterprise.xml.xmlReader();
				reader.xmlFileName = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\" + "MaxLengthFilter.xml";
				filterTable = reader.ReadFilters2Table();
			}

			foreach(System.Data.DataRow filterRow in filterTable.Rows)
			{
				if((entity == filterRow["EntityName"].ToString() || filterRow["EntityName"].ToString()=="EVERYENTITY") && cntrlName.ToUpper()==filterRow["FieldName"].ToString())
				{
					if(filterRow["FilterType"]!=null && cntrl is System.Web.UI.WebControls.TextBox)
					{
						System.Web.UI.WebControls.TextBox txtBox= (System.Web.UI.WebControls.TextBox)cntrl;
						txtBox.MaxLength = Convert.ToInt32(filterRow["FieldLength"]);
					}
				}
			}
		}

		private string getEntityName()
		{
			string entity = HttpContext.Current.Request.CurrentExecutionFilePath;
			entity = SHMA.Enterprise.Utilities.File2EntityID(entity.Substring(entity.LastIndexOf("/")+1));
			return entity;
		}

		
	}
}




