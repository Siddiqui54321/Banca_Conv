using System;
	//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace ace
{
	
	public class ILUS_USE_USERMASTER:shgn.SHGNCommand
	{
		
		public override void fsoperationAfterSave()
		{
			//EnvHelper env = new EnvHelper();
			//string user = Convert.ToString(env.getAttribute("USE_USERID"));
			string user = getString("USE_USERID");
			
			//For the first time on User creation password will be same as its User Code
			ace.Change_Password objChangePassword = new ace.Change_Password();
			objChangePassword.updateuserpwd(user,user);

		}
	}

}