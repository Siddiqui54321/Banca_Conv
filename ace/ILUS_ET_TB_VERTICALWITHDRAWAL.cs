using System;
//using ArrayList = java.util.ArrayList;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	/// <summary>
	/// Summary description for ILUS_ET_TB_VERTICALWITHDRAWAL.
	/// </summary>
	public class ILUS_ET_TB_VERTICALWITHDRAWAL:shgn.SHGNCommand
	{
		EnvHelper env = new EnvHelper();

		//public override void fsoperationAfterSave()
		public override void fsoperationBeforeSave()
		{
		}

		public override void fsoperationAfterSave()
		{
			//GenerateIllustration(Convert.ToString(env.getAttribute("NP1_PROPOSAL")));
		}

		public override void fsoperationAfterUpdate()
		{
			//GenerateIllustration(Convert.ToString(env.getAttribute("NP1_PROPOSAL")));
		}

		private void fsdeleteModelPersonalDetInsSecondLife()
		{
		}

		private void fscreateModelLinkPersonalDet()
		{

		}
	}
}
