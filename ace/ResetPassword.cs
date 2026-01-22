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
	public class ResetPassword : shgn.ProcessCommand
	{
		public override String processing()
		{
			try
			{
				EnvHelper env = new EnvHelper();
				NameValueCollection obj_cols = this.getDataRows()[0];
				string user      = Convert.ToString(env.getAttribute("USE_USERID"));
				string password  = Convert.ToString(obj_cols.getObject("USE_PASSWORD")).Trim();
				string password2 = Convert.ToString(obj_cols.getObject("USE_PASSWORD2")).Trim();

				if(password.Length == 0)
				{
					throw new Exception("Password is empty.");
				}

				if(password2.Length == 0)
				{
					throw new Exception("Confirm Password is empty.");
				}

				if(password != password2)
				{
					throw new Exception("Passwords does not match.");
				}
				
				
				//ace.Change_Password objChangePassword = new ace.Change_Password();
				//objChangePassword.updateuserpwd(user,password);
				new ace.Change_Password().updateUserPWD(user,Security.ACTIVITY.NONE);

				return "Process executed successfully";

			}
			catch (Exception ex) 
			{
				throw new ProcessException(ex.Message);
			}
		}
	
	}

}
