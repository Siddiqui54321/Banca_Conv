using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	public class MandatoryRiderProdEntityClass:shgn.SHGNCommand
	{
		EnvHelper env = new EnvHelper();
		public override void fsoperationBeforeDelete()
		{
			/***************************************************************************
			Called from Master Tab (Product)
			Check either Detail records are present in LPVL_VALIDATION table or not
			Do not allow to delete if records found in LPVL_VALIDATION 
			****************************************************************************/
			string product = Convert.ToString(env.getAttribute("PPR_PRODCD"));
			string vField  = Convert.ToString(env.getAttribute("PVF_CODE"));
			rowset rs = DB.executeQuery("select 'A' from LPVL_VALIDATION WHERE PPR_PRODCD='"+product+"' AND PVL_VALIDATIONFOR='"+vField+"'");
			if(rs.next())
			{
				throw new ProcessException("Delete Rider(s) first.");
			}
		}

		public override void fsoperationAfterSave()
		{
		}

		public override void fsoperationAfterUpdate()
		{
		}
	}
}