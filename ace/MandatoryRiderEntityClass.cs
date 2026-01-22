using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
	public class MandatoryRiderEntityClass:shgn.SHGNCommand
	{
		EnvHelper env = new EnvHelper();

		
		//string product = Convert.ToString(getAttribute("PPR_PRODCD"));
		//string vField  = Convert.ToString(getAttribute("VFS_CODE"));


		public override void fsoperationBeforeDelete()
		{
		}

		public override void fsoperationAfterSave()
		{

			string product = Convert.ToString(env.getAttribute("PPR_PRODCD"));
			string vField  = Convert.ToString(env.getAttribute("VFS_CODE"));
			string riderList = Convert.ToString(env.getAttribute("PVL_VALIDRANGE"));
			//string riderList = getString Convert.ToString(env.getAttribute("PVL_VALIDRANGE"));
			validateInput(product, vField, riderList);
			setLevelCode(product, vField);

		}

		public override void fsoperationAfterUpdate()
		{
			string product = Convert.ToString(env.getAttribute("PPR_PRODCD"));
			string vField  = Convert.ToString(env.getAttribute("VFS_CODE"));
			string riderList = Convert.ToString(env.getAttribute("PVL_VALIDRANGE"));
			//string riderList = getString Convert.ToString(env.getAttribute("PVL_VALIDRANGE"));
			validateInput(product, vField, riderList);
			setLevelCode(product, vField);
		}

		private void validateInput(string product, string vField, string riderList)
		{
			try
			{
				if(vField == "FORBIDDENRIDER" )//MANDRIDER
				{	//Check this information in Forbidden Setup
					string query = "select PVL_VALIDRANGE from lpvl_validation where ppr_prodcd='" + product + "' AND pvl_validationfor='MANDRIDER' AND PVL_VALIDRANGE='" + riderList + "'" ;
					rowset rs = DB.executeQuery(query);
					if(rs.next() == true)
					{
						throw new ProcessException("Rider " + riderList + " already defined as Mandatory ");
					}
					
				}
				if (vField=="MANDRIDER")
				{
					string query = "select PVL_VALIDRANGE from lpvl_validation where ppr_prodcd='" + product + "' AND pvl_validationfor='FORBIDDENRIDER' AND PVL_VALIDRANGE='" + riderList + "'" ;
					rowset rs = DB.executeQuery(query);
					if(rs.next() == true)
					{
						throw new ProcessException("Rider " + riderList + " already defined as Forbidden ");
					}

				}


			}
			catch(Exception e)
			{
				throw new ProcessException(e.Message);
			}
		}

		private void setLevelCode(string product, string vField)
		{
			//**************** Get Level Code *****************
			string query = "select NVL(MAX(PVL_LEVEL),0)+1 PVL_LEVEL  from LPVL_VALIDATION where ppr_prodcd='" + product +"' and pvl_validationfor = '" + vField + "'";
			rowset rs = DB.executeQuery(query);
			//1. Check the existance of records (i.e Record(s) found or not
			//2. Set the position of cursor to 1st record
			if(rs.next() == true)
			{
				int level =  rs.getInt("PVL_LEVEL");
				
				//**************** Update Level in LPVL_VALIDATION >> PVL_LEVEL *******************
				query = "UPDATE LPVL_VALIDATION SET PVL_LEVEL=" + level + " where ppr_prodcd='" + product +"' and pvl_validationfor = '" + vField + "' AND PVL_LEVEL=0";
				DB.executeDML(query);
			}
			else
			{
				throw new ProcessException("Error in getting Code for Mandatory Rider");
			}
		}



	}
}