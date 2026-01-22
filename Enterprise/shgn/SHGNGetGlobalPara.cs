using System;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
namespace shgn
{
	
	public class SHGNGetGlobalPara
	{
		
		private ParameterCollection cGlobalColl = new ParameterCollection();
		
		//UPGRADE_NOTE: There are other database providers or managers under System.Data namespace which can be used optionally to better fit the application requirements. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1208_3"'
		public virtual System.String GetGlobalPara(System.String GlbPara1, System.String GlbPara2)
		{
			System.String mGlobalPara = null;
			DB.openConnection();
			
			System.Text.StringBuilder stringGlobalPara = new System.Text.StringBuilder(" select pyd_spdtvalue from pr_gn_Yd_Systemparadtl ");
			stringGlobalPara.Append(" where pyh_sphdcode = ? and pyd_spdtcode = ? ");
			
			cGlobalColl.clear();
			cGlobalColl.puts("@A1", GlbPara1);
			cGlobalColl.puts("@A2", GlbPara2);
			rowset rGlobalPara = DB.executeQuery(stringGlobalPara.ToString(), cGlobalColl);
			
			if (!rGlobalPara.next())
			{
				throw new System.Exception("no paremeter found of " + GlbPara1 + "," + GlbPara2 + " in Global Setup.");
			}
			
			rGlobalPara.absolute(1);
			mGlobalPara = rGlobalPara.getString("pyd_spdtvalue");
			if (mGlobalPara == null || mGlobalPara.Equals("") )
			{
			   throw new System.Exception("null value found for " + GlbPara1 + "," + GlbPara2 + " in Global Setup.");
			}
			
			
		//	DB.closeConnection();
			return mGlobalPara;
		}
		
		//UPGRADE_NOTE: There are other database providers or managers under System.Data namespace which can be used optionally to better fit the application requirements. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1208_3"'
		public virtual System.String GetGlobalPara2(System.String GlbPara1, System.String GlbPara2, System.Data.OleDb.OleDbConnection databaseConnection)
		{
			
			System.String mGlobalPara = null;
			System.String mGlobalPara2 = null;
			DB.openConnection();
			
			System.Text.StringBuilder stringGlobalPara = new System.Text.StringBuilder(" select value(pyd_spdtvalue,'value') pyd_spdtvalue,value(pyd_spdtstatus,'Y') status from pr_gn_Yd_Systemparadtl ");
			stringGlobalPara.Append(" where pyh_sphdcode = ? and pyd_spdtcode = ? ");
			
			cGlobalColl.clear();
			cGlobalColl.puts("@A1", GlbPara1);
			cGlobalColl.puts("@A2", GlbPara2);
			rowset rGlobalPara = DB.executeQuery(stringGlobalPara.ToString(), cGlobalColl);
			
			if (!rGlobalPara.next())
			{
				throw new System.Exception("no data found in global table against zakat value ");
			}
			
			rGlobalPara.absolute(1);
			mGlobalPara = rGlobalPara.getString("pyd_spdtvalue");
			mGlobalPara2 = rGlobalPara.getString("status");
			
			DB.closeConnection();
			return mGlobalPara + "~" + mGlobalPara2;
		}
	}
}