using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
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
	public class Call_Illustration : shgn.ProcessCommand
	{
		EnvHelper env = new EnvHelper();
		private String parseError(String pError) 
		{
			String[] tokens = pError.Split('#');
			String line1 ;

			if (tokens.Length > 1 )
			{
				//line1 = tokens[0]+"\n"+"    "+tokens[1]==null?"":tokens[1];
				line1 = tokens[0]+"    "+(tokens[1]==null?"":tokens[1]);
			}
			else
			{
				line1 = pError;
			}
			//String UserId = tokens[1].ToString().Split('=')[1].ToString().Trim();
			//String Password = tokens[2].ToString().Split('=')[1].ToString().Trim();
			//String DataSource = tokens[3].ToString().Split('=')[1].ToString().Trim();

			return line1;
		}
		public override String processing()
		{
			try
			{
				string proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));

				/*ProcedureAdapter obj =  new ProcedureAdapter("CALL_ILUSTRATION");
				obj.Set("P_PROPOSAL", System.Data.OleDb.OleDbType.VarChar, 50, proposal);
				//obj.RegisetrOutParameter("M_ERROR",OleDbType.VarChar,1000 );
				obj.Execute();
				//string pError =  obj.Get("M_ERROR") == null ? "" : Convert.ToString(obj.Get("M_ERROR"));
				//if (pError.Trim().Length > 0)
				//	return parseError(pError);//throw new ProcessException(parseError(pError));*/
				ProcedureAdapter obj =  new ProcedureAdapter("CALL_ILUSTRATION");
				obj.Set("P_PROPOSAL", System.Data.OleDb.OleDbType.VarChar, 50, proposal);
				obj.RegisetrOutParameter("M_ERROR",OleDbType.VarChar,1000 );
				obj.Execute();
				string pError =  obj.Get("M_ERROR") == null ? "" : Convert.ToString(obj.Get("M_ERROR"));
				if (pError.Trim().Length > 0)
					return parseError(pError);//throw new ProcessException(parseError(pError));
				return "";
			}
			catch (Exception ex)
			{
				String msg = ex.Message;
				msg = msg.Replace("\"", "");
				msg = msg.Replace("'", "");
				msg = msg.Replace("\r", " ");
				return msg;
			}
			finally
			{
				//SessionObject.Set("FLAG_RESET_PREMIUM","");
			}
		}
	}
}