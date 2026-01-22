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

namespace ace
{
	
	public class PurposeProtection:ProcessCommand
	{
		EnvHelper env = new EnvHelper();
        SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

		public override System.String processing()
		{
			
			NameValueCollection[] nv = this.getDataRows();

			System.Console.Out.WriteLine("Start Calculate Premium Process...");


			try 
			{
				for (int r = 0; r < this.getSelectedRows().Length; r++)
				{

					
					String strCommand = "";
					String CPU_CODE = nv[r].getObject("CPU_CODE").ToString();

					if (this.getSelectedRows()[r])
					{
						System.Console.Out.WriteLine("**WS**this.getSelectedRows().length " + this.getSelectedRows().Length);

						//TODO pick value from session object
						strCommand = "update LNPU_PURPOSE set NPU_SELECTED='Y' where NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"' and CPU_CODE = '" + CPU_CODE + "'";
					}
					else
					{
						strCommand = "update LNPU_PURPOSE set NPU_SELECTED='N' where NP1_PROPOSAL='"+SessionObject.Get("NP1_PROPOSAL")+"' and CPU_CODE = '" + CPU_CODE + "'";
					}
					DB.executeDML(strCommand);
				}

			}
			catch (System.Exception e)
			{
				System.Console.Out.WriteLine(e.Message);
				SupportClass.WriteStackTrace(e, Console.Error);
				throw new ProcessException(e.Message);
			} 

			return "";

		}
	}
	//End of Class
}