using System;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Exceptions;

namespace ace
{
	/// <summary>
	/// Summary description for LNFU_FUNDS.
	/// </summary>
	public class LNFU_FUNDS:shgn.SHGNCommand
	{
		public override void fsoperationBeforeSave()
		{
		}

		public override void fsoperationAfterSave()
		{
		}

		public override void fsoperationBeforeUpdate()
		{
		}

		public override void fsoperationAfterUpdate()
		{
		}

		private void validatePercentage()
		{
		}

		private void SaveUpdate()
		{
			/*
			try
			{
				string query = " UPDATE LNFU_FUNDS SET NFU_RATE = 100 "
					         + " WHERE CFU_FUNDCODE IN (SELECT CSD_TYPE FROM LCSD_SYSTEMDTL "
				             + "                        WHERE CSD_VALUE = (SELECT CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='FUNDS' AND CSD_TYPE =? )  " 
					         + "                        AND   CSH_ID    = 'FUNDS' AND CSD_STATUS='N' )" ;
				
				SHMA.Enterprise.Data.ParameterCollection pc=new SHMA.Enterprise.Data.ParameterCollection();
				pc.puts("@fund", Convert.ToString(this.get("CFU_FUNDCODE")), Types.VARCHAR);
				DB.executeDML(query, pc);
			}
			catch(Exception e)
			{
				throw e;
			}
			*/
		}

		public static void resetRate()
		{
			try
			{
				EnvHelper env = new EnvHelper();
				string proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
				string setNumber= Convert.ToString(env.getAttribute("NP2_SETNO"));
				string product  = Convert.ToString(env.getAttribute("PPR_PRODCD"));
				DB.executeDML("UPDATE LNFU_FUNDS SET NFU_RATE=0  WHERE NP1_PROPOSAL='" + proposal + "' and NP2_SETNO=" + setNumber + " and PPR_PRODCD='" + product + "' ");
			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
		}

		public static void validateInputPercentage()
		{
			try
			{
				EnvHelper env = new EnvHelper();
				string proposal = Convert.ToString(env.getAttribute("NP1_PROPOSAL"));
				string setNumber= Convert.ToString(env.getAttribute("NP2_SETNO"));
				string product  = Convert.ToString(env.getAttribute("PPR_PRODCD"));

				rowset rs  = DB.executeQuery("select SUM(NFU_RATE) RATE_SUM, COUNT(NFU_RATE) RATE_COUNT from LNFU_FUNDS WHERE NP1_PROPOSAL='" + proposal + "' and NP2_SETNO=" + setNumber + " and PPR_PRODCD='" + product + "'  AND NFU_RATE > 0 ");
				if(rs.next())
				{
					double rateSUM = rs.getDouble("RATE_SUM");
					if(rateSUM != 100.0)
					{
						throw new ProcessException("Fund percentage is " + rateSUM.ToString() + "%. It should be 100%.");
					}

					int rateCOUNT = rs.getInt("RATE_COUNT");
					if(rateCOUNT > 10)
					{
						throw new ProcessException(rateCOUNT.ToString() +  " Funds are selected. Maximum 10 Funds are allowed to select.");
					}
				}
				else
				{
					throw new ProcessException("Fund not defined.");
				}
			}
			catch(ProcessException e)
			{
				throw e;
			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
		}
	}
}
