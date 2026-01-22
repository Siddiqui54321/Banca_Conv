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

	public class GenerateOffers : shgn.ProcessCommand
	{

		public override String processing () //throws SQLException, ProcessException
		{
			/****** Call Process To Generate NIV Values Upto the Given Period ******/
			//conn = DB.getConnection();

			NameValueCollection[] nmRows = this.getDataRows();

			for(int a=0; a<nmRows.Length; a++)
			{
				try
				{
					NameValueCollection nvc = nmRows[a];
					int illustrationUptoAge=0;

					SessionObject.Set("NP1_RETIREMENTAGE",  ""+nvc.getObject("NP1_RETIREMENTAGE"));  
					SessionObject.Set("NP1_TARGETATTAINAGE", ""+nvc.getObject("NP1_TARGETATTAINAGE"));  
					SessionObject.Set("NP1_TARGETRETURNYEAR", ""+nvc.getObject("NP1_TARGETRETURNYEAR")); 


					if ( nvc.getObject("NP1_PREMPAYCOU") != null && !(nvc.getObject("NP1_PREMPAYCOU").ToString().Equals(""))
						&& Int32.Parse((nvc.getObject("NP1_PREMPAYCOU").ToString())) != 0 )
					{
						illustrationUptoAge = Int32.Parse((nvc.getObject("NP1_PREMPAYCOU").ToString())) ;
					}
					else
					{
						String strBT = "Select NPR_BENEFITTERM from lnpr_product where np1_proposal = '"+  nvc.getObject("NP1_PROPOSAL").ToString() +"' AND "+
							" np2_setno = 1 and NPR_BASICFLAG='Y' ";
						rowset rst = DB.executeQuery( strBT ) ;
						if ( rst.next() )
							illustrationUptoAge = Int32.Parse((rst.getString( "NPR_BENEFITTERM" ).ToString())) ;

						if ( illustrationUptoAge == 0 )
							throw new ProcessException ("Unable to get Illustration period");
					}

					//illustrationUptoAge = 60;

					/**** Change Proposal Status to Inforce Policy (temporarily to run process) ****/
					
				//	DB.executeDML("Update lnp1_policymastr set cst_statuscd=GET_SYSPARA.GET_VALUE('PPSTS','INFORCE') where np1_proposal='"+nvc.getObject("NP1_PROPOSAL").ToString()+"'");
				//	DB.executeDML("Update lnp2_policymastr set np2_approved=null where np1_proposal='"+nvc.getObject("NP1_PROPOSAL").ToString()+"'");

					/****** Call generate offer procedure ******/
				//	ProcedureAdapter call = new ProcedureAdapter("GENERATE_OFFER");
				//	call.Set("P_PROPOSAL", OleDbType.VarChar, 50, Convert.ToString(nvc.getObject("NP1_PROPOSAL")));
				//	call.Set("P_UPTOAGE",  OleDbType.Numeric, illustrationUptoAge);
				//	call.Execute();


					/**** Revoke proposal approval ****/
					DB.executeDML("Update lnp1_policymastr set cst_statuscd=null where np1_proposal='"+nvc.getObject("NP1_PROPOSAL").ToString()+"'");
					DB.executeDML("Update lnp2_policymastr set np2_approved=null where np1_proposal='"+nvc.getObject("NP1_PROPOSAL").ToString()+"'");

					/****** Display Offer Letter As A Report using Report Builder ******/
				}
				catch(Exception e)
				{
					//e.printStackTrace();
					throw new ProcessException (e.Message);
				}
			}
			//return "Illustration Generated Suceessfully";
			return "";
		}
	}
}
 