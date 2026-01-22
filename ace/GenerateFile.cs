using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using ace;


namespace ace
{
	public class GenerateFile : shgn.ProcessCommand
	{
		public override String processing()
		{
			try
			{
				NameValueCollection obj_cols = this.getDataRows()[0];
				
				DateTime dateFrom = Convert.ToDateTime(obj_cols.getObject("DATEFROM"));
				DateTime dateTo = Convert.ToDateTime(obj_cols.getObject("DATETO"));
				
				//Call Procedure now
				ProcedureAdapter proc = new ProcedureAdapter("GENERATEFILE");
				proc.Set("DATEFROM", OleDbType.Date, dateFrom);
				proc.Set("DATETO",   OleDbType.Date, dateTo);
				proc.RegisetrOutParameter("P_ERROR", OleDbType.VarChar, 500);
				proc.Execute();

				object error = proc.Get("P_ERROR");
				if (error != null)
				{
					if(error.ToString().Trim().Length > 0)
					{
						throw new ProcessException(error.ToString());
					}
				}
				return "Process executed successfully";
			}
				
			catch (Exception ex) 
			{
				throw new ProcessException(ex.Message);
			}
		}
	
	
	}

}
