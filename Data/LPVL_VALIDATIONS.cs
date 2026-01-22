using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;

namespace SHAB.Data
{
	/// <summary>
	/// Summary description for LPVL_VALIDATION.
	/// </summary>
	public class LPVL_VALIDATIONS
	{
		public LPVL_VALIDATIONS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//CHECK VALIDATION

		public string check_validation(string PVL_VALIDATIONFOR,string PPR_PRODCD)
		{
			
				string validation_string="";
				string final_validation_string="";
				// Put user code to initialize the page here
				string dsn = DB.Connection.ConnectionString.ToString();
				OleDbConnection cnn = new OleDbConnection( dsn );	
				string query ="select * from lpvl_validation where PVL_VALIDATIONFOR='"+PVL_VALIDATIONFOR+"' AND  PPR_PRODCD='"+PPR_PRODCD+"'";
				query = EnvHelper.Parse (query);
				OleDbDataAdapter da = new OleDbDataAdapter(query, cnn);
				DataSet ds = new DataSet(); 
				da.Fill(ds,"VALIDATION"); 

				//check Rows for sumassured..
				//Check Rows..
			if(ds.Tables[0].Rows.Count>1)
			{
				//Multiple Rows Exsist..			

				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					
					string sumassured=ds.Tables[0].Rows[i]["pvl_validfrom"].ToString();
					validation_string+=sumassured+",";
					
				}	
				
			}
			else
			{
				//Single Rows Exsist
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					string pvl_validfrom_sumassured=ds.Tables[0].Rows[i]["pvl_validfrom"].ToString();
					string pvl_validto_sumassured=ds.Tables[0].Rows[i]["pvl_validto"].ToString();
					
					validation_string+=pvl_validfrom_sumassured+" "+"to"+" "+pvl_validto_sumassured;
				}		
					


			}
			return validation_string;

			
		}
		
	}
}
