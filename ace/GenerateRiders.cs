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
	
	public class GenerateRiders : ProcessCommand
	{
		EnvHelper env = new EnvHelper();
        SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();

		String strProposal;
		String strProduct;

		public override String processing()
		{
			try
			{
				NameValueCollection[] nmRows = this.getDataRows();

				for(int a=0; a<nmRows.Length; a++)
				{
					NameValueCollection nvc = nmRows[a];
					if ( nvc.getObject("NP1_PROPOSAL") == null || nvc.getObject("NP1_PROPOSAL" ).ToString().Equals(""))
					{
						throw new ProcessException ( "Please Select A Proposal First" );
					}
					else
					{
						strProposal = nvc.getObject( "NP1_PROPOSAL" ).ToString();

						DB.executeDML("Update lnpr_product set ppr_prodcd='"+nvc.getObject("PPR_PRODCD" ).ToString()+"' where np1_proposal = '" + strProposal + "' and np2_setno = 1 and npr_basicflag='Y' ") ;
						DB.executeDML("delete from lnpr_product where np1_proposal = '" + strProposal + "' and np2_setno = 1 and NVL(npr_basicflag,'N')='N'" );

						String strBasicPlan = " Select ppr_prodcd,nvl(NPR_BENEFITTERM,0) NPR_BENEFITTERM, nvl(NPR_PREMIUMTER,0) NPR_PREMIUMTER, nvl(NPR_SUMASSURED,0) NPR_SUMASSURED from lnpr_product "+
								                " Where np1_proposal = '"+ strProposal +"' and NPR_BASICFLAG = 'Y'";

						rowset rstBasicPlan = DB.executeQuery( strBasicPlan );
						if ( rstBasicPlan.next() )
						{
							strProduct = rstBasicPlan.getString("PPR_PRODCD");
							String strRider = " Select pri.ppr_rider, decode(ppr.PPR_COMMLOADINGENABLED,'Y', GET_SYSPARA.GET_VALUE('DEFLT','DEF_DBOPTION'), '0') DBOPTION " +
												" from lpri_rider pri, lppr_product ppr where ppr.ppr_prodcd=pri.ppr_rider and pri.ppr_prodcd = '" + strProduct + "'";
							rowset rstRiders = DB.executeQuery(strRider);

							while ( rstRiders.next() )
							{
								DB.executeDML("delete from lnpr_product where np1_proposal = '" + strProposal + "' and np2_setno = 1 and ppr_prodcd = '" + rstRiders.getString("PPR_RIDER") +"'" );
								String strInsert = "Insert into lnpr_product (np1_proposal, np2_setno, ppr_prodcd, npr_basicflag,npr_life,ccb_code, NPR_BENEFITTERM, NPR_PREMIUMTER, NPR_SUMASSURED,NPR_SELECTED, NPR_COMMLOADING) values ('"+
										            strProposal + "',1,'" + rstRiders.getString("PPR_RIDER") + "','N',GET_SYSPARA.GET_VALUE('GLOBL','DEF_LIFE'),GET_SYSPARA.GET_VALUE('GLOBL','DEF_CALC_BASIS'),"+
										            " 0,0,0,'Y', decode('"+rstRiders.getString("DBOPTION")+"','0',null,'"+rstRiders.getString("DBOPTION")+"') )";
								DB.executeDML( strInsert );
							}
						}
						else
						{
							throw new ProcessException ( "Please Enter The Basic Plan, Before Generating Riders" );
						}
					}
				}
			}
			catch(Exception e)
			{
				throw new ProcessException (  e.Message );
			}
			return "";
		}	
	}
	//End of Class
}