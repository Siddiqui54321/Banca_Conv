using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHAB.Data; 


namespace SHAB.Business{
	public class LNP1_POLICYMASTR : SHMA.CodeVision.Business.BusinessClassBase{	
		public LNP1_POLICYMASTR(DataHolder dh):base(dh, "LNP1_POLICYMASTR"){		
		}

		public static string[] PrimaryKeys{
			get{	return new string[]{"NP1_PROPOSAL"};}
		}
		public sealed override string[] GetPrimaryKeys(){
			return PrimaryKeys;
		}
		protected sealed override DataClassBase dataObject{
			get{
				return new LNP1_POLICYMASTRDB(dataHolder);
			}
		}
		
		public override void Add(NameValueCollection columnNameValue, NameValueCollection allFields, string EntityID) 
		{
		   	
		   	
		   	base.Add(columnNameValue,allFields,EntityID);
		   	
		}
		
		public override void Add(NameValueCollection columnNameValue,string fieldCombination,string valueCombination)
		{
		   base.Add(columnNameValue,fieldCombination,valueCombination);
		}
		public static void UpdateCommencmentDate(string proposal,DateTime CommencDate)
		{
		   LNP1_POLICYMASTRDB.UpdateCommencmentDate(proposal,CommencDate);
		}
		//========New Method 08-Feb-2017
		public static void UpdateCommencmentDate_New(string CommencDate, string proposal)
		{
			LNP1_POLICYMASTRDB.UpdateCommDate_New(CommencDate, proposal);
		}

		public static string GetCommencementDate(string proposal)
		{
			return	LNP1_POLICYMASTRDB.GetCommDate(proposal);
		}

        public static string GetProposalStatus(string query)
        {
            return LNP1_POLICYMASTRDB.GetProposalStatus(query);
        }
		public static void insertCollection(string proposal,string userid,double collection,string collectiondate)
		{
			LNP1_POLICYMASTRDB.insertCollection(proposal,userid,collection, collectiondate);
		}
		//========New Method 08-Feb-2017
		public static void markStatus(string proposal,string status)
		{
            // string B_code = System.Convert.ToString(Session["s_CCD_CODE"]).ToString();
            String result = LNP1_POLICYMASTRDB.ProposalStatusandBankCode(proposal, System.Web.HttpContext.Current.Session["BankCode"].ToString());
            string[] parts = result.Replace("'", "").Split(',');

           // string substandardFlag = parts[3].ToString();

            if (status.Equals("Y-FromFile") || status.Equals("Y-FromCallCenter"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal,"Y");							
			}


			else if(status.Equals("Y"))
			{
                

                String forcedStatus = "R"; // Mark WMO
                                           // below is BM case

                //if (parts[0].ToString() == "Y" && parts[1].ToString() == "F")
                //    forcedStatus = "G";
                
                if (parts[0].ToString() == "P" && parts[1].ToString() == "Y" && parts[2].ToString() == "F")

                { forcedStatus = "G"; } // Mark by BM

                else if (parts[0].ToString() == "G" && parts[1].ToString() == "Y" && parts[2].ToString() == "F")

                { forcedStatus = "P"; } // Mark by GM


                else if (parts[0].ToString() == "P" && parts[1].ToString() == "N" && parts[2].ToString() == "F")
                {
                    forcedStatus = "R"; // Mark by WMO
                }

                else if (parts[0].ToString() == "P" && parts[1].ToString() == "Y" && parts[2].ToString() == "9")
                {
                    forcedStatus = "R"; // Mark by BOP-RBH
                }

                else if (parts[0].ToString() == "P" && parts[1].ToString() == "N" && parts[2].ToString() == "9")
                {
                    forcedStatus = "R"; // Mark by BOP-BM
                }

                else if (parts[0].ToString() == "P" && parts[1].ToString() == "" && parts[2].ToString() == "4")
                {
                    forcedStatus = "R"; // Mark by NBP-BM
                }


                else
                {
                    throw new Exception("Error message:" + result.ToString());
                }


                LNP1_POLICYMASTRDB.MarkStatus(proposal, forcedStatus);							
			}
			else if (status.Equals("D"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal, "D");
			}

            //else if (status.Equals("T"))
            //{
            //   // LNP1_POLICYMASTRDB.MarkStatus(proposal, "P"); // wmo disprement
            //    LNP1_POLICYMASTRDB.MarkStatus(proposal, string.Empty);
            //    LNP1_POLICYMASTRDB.UnMarkStandSubStand(proposal);
            //}

            else if (status.Equals("H"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal, "H");
			}
			else if (status.Equals("K"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal, "K");
			}

          //  else if (status.Equals("T")) // for WMO Mark Discrepant
          //  {
          //      LNP1_POLICYMASTRDB.MarkStatus(proposal, "P");
           // }

            


            else if(status.Equals("N"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal,string.Empty);
				LNP1_POLICYMASTRDB.UnMarkStandSubStand(proposal);	
			}
			else if(status.Equals("A"))
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal,"Y");
			}
			else if(status.Equals("null"))
			{
				LNP1_POLICYMASTRDB.MarkStatusNull(proposal);
				LNP1_POLICYMASTRDB.MarkStandSubStandNull(proposal);	
			}
			else if(status.Equals("ReCal"))
		    {
				LNP1_POLICYMASTRDB.MarkStatusNull(proposal);
				LNP1_POLICYMASTRDB.MarkStandSubStandNullReCalc(proposal);	
		    }
			else
			{
				LNP1_POLICYMASTRDB.MarkStatus(proposal,status);
			}

		}




		///////============= UPDATE UNCOLLECTED STATUS - 08-May-2018
		public static void markStatus_Uncollected(string proposal,string status)
		{
			LNP1_POLICYMASTRDB.MarkStatus_Payment(proposal, status);
		}
		//////////============= UPDATE UNCOLLECTED STATUS - 08-May-2018
	}
}

