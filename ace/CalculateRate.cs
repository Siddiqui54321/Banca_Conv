using System;
//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using SHGNDateUtil = shgn.SHGNDateUtil;
using System.Web.SessionState;
using shgn;

using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Web.UI.HtmlControls;


using System.Data.OleDb;
using System.Data.OracleClient;

namespace ace
{
	public class CalculateRate:ProcessCommand
	{
		//EnvHelper env = new EnvHelper();
		//SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
		string FileName = "";

		public override System.String processing()
		{
			try 
			{
				NameValueCollection obj_cols = this.getDataRows()[0];
				if (obj_cols == null)
					throw new ProcessException("Error in computing Rate Setup!");

				//DB.executeDML("Alter_Rates('M','450','001','100','FE158,FE037','020','FE038','44,A','FV015','26.2')");
				String strFileName = (String)obj_cols.getObject("FILENAME");
				String strLocFrom = (String)obj_cols.getObject("LOCFROM");
				String strLocTo = (String)obj_cols.getObject("LOCTO");
				String strSheet = (String)obj_cols.getObject("SHEET");//"Investa-Life";

				this.FileName = strFileName;
				

				rowset rs;
				rs = DB.executeQuery("SELECT GET_SYSPARA.GET_VALUE('GLOBL','RATESETUPPATH') PATH  FROM DUAL");
				if(rs.next())
				{
					if(rs.getObject("PATH") == null)      throw new ProcessException("RATESETUPPATH is null in System Detail table");
					if(rs.getString("PATH").Trim() == "") throw new ProcessException("RATESETUPPATH is empty in System Detail table");
					strFileName = rs.getString("PATH").Trim() + @"\" + strFileName;
				}
				else
				{
					throw new ProcessException("Rate Setup path not defined in System Detail table.");
				}
				
				//Start Main Process
				StartProcess(strFileName, strSheet, strLocFrom, strLocTo);
			}
			catch (System.Exception e)
			{
				throw new ProcessException(e.Message);
			}
			return "Process completed successfully.";
		}


		private void StartProcess(string strFile, string strSheet, string strCellFrom, string strCellTo)
		{
			object [,] values;

			Excel.Application sXL =null;
			Excel._Workbook oWB = null;
			Excel._Worksheet oSheet = null;
			Excel.Range oRange = null;

			string processStep = "";

			try
			{
				processStep = "Opening Excel Application : ";
				sXL = new Excel.Application();
				sXL.Visible=false;

				processStep = "Opening Excel FIle " + this.FileName + " : ";
				oWB = sXL.Workbooks.Open(strFile,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);

				processStep = "Opening Sheet " + strSheet + " : ";
				oSheet = (Excel._Worksheet)oWB.Sheets[strSheet];

				processStep = "Picking values from cell " + strCellFrom + " to  cell " + strCellTo + " : ";
				oRange = oSheet.get_Range(strCellFrom, strCellTo);

				if(strCellFrom != strCellTo)
				{
					//values = (object[,]) oRange.Value2;
					object [,] multipleValues = (object[,]) oRange.Value2;
					values = new object [multipleValues.Length ,1];
					for (int i=0; i<values.Length; i++)
						values[i,0]= multipleValues[i+1,1];
				}
				else
				{
					object objValue = oRange.Value2;
					values = new object [1,1];
					values[0,0] = objValue;
				}

			}
			catch(Exception e)
			{
				//throw new ProcessException("Error in opening Excel File. " + e.Message);
				throw new ProcessException("Error in " +  processStep + e.Message);
			}
			finally
			{
				oRange = null;
				oSheet = null;
						
				if(oWB != null)	oWB.Close(null,null,null);
				if(sXL != null) sXL.Quit();
							
				oWB = null;
				sXL = null;
				//if(sXL != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sXL);
				//if(oWB != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB);
			}

			//Executing script
			try
			{
				ExecuteScript(values);
			}
			catch(Exception e2)
			{
				throw new ProcessException(e2.Message);
			}
		}

		private void ExecuteScript(object [,] scriptArray)
		{
			string script = "";
			for (int i=0; i<scriptArray.Length; i++)
			{
				if(scriptArray[i,0] != null)
				{
					script = scriptArray[i,0].ToString();
					if(script.IndexOf("Alter_Rates(") > -1 )
					{
						//Put ~ character in the query in order to get Parameter List properly
						script = script.Replace("Alter_Rates(","").Replace(")","").Replace(";","");
						script = script.Replace("','","~");
						script = script.Replace("', '","~");
						script = script.Replace("' ,'","~");
						script = script.Replace("' , '","~");

						//Finally Remove single quote because all the parameters are character type which will send to stored procedure
						script = script.Replace("'","");

						string [] parameter =  script.Split('~');

						try
						{
							ProcedureAdapter objOCA = new ProcedureAdapter("Alter_Rates");
							objOCA.Set("PTAG",            OleDbType.VarChar, 50, parameter[0]);
							objOCA.Set("PPPR_PRODCD",     OleDbType.VarChar, 50, parameter[1]);
							objOCA.Set("PCMP_PROCESSID",  OleDbType.VarChar, 50, parameter[2]);
							objOCA.Set("PCSP_PROCESSID",  OleDbType.VarChar, 50, parameter[3]);
							objOCA.Set("PCFC_FIELDCOMB",  OleDbType.VarChar, 50, parameter[4]);
							objOCA.Set("PCFC_CONDITIONID",OleDbType.VarChar, 50, parameter[5]);
							objOCA.Set("PCCN_COLUMNID1",  OleDbType.VarChar, 50, parameter[6]);
							objOCA.Set("PCVC_VALUECOMB",  OleDbType.VarChar, 50, parameter[7]);
							objOCA.Set("PCCN_COLUMNID",   OleDbType.VarChar, 50, parameter[8]);
							objOCA.Set("PCVC_VALUE",      OleDbType.VarChar, 50, parameter[9]);
							objOCA.Execute();

						}
						catch(Exception e)
						{
							throw new ProcessException("Error in executing Stored Procedure: <<<<" + script  + ">>>>"  + e.Message);
						}
					}
					else
					{
						throw new ProcessException("Stored Procedure 'Alter_Rate' not found in query, may be Excel Cell Range is wrong.");
					}
				}
				else
				{
					throw new ProcessException("Query not found or Null in the Excel sheet, may be Excel Cell Range is wrong.");
				}
			}

		}

	}	//End of Class
}