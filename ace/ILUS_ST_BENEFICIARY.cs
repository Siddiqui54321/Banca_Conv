using System;
using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace ace
{
    public class ILUS_ST_BENEFICIARY : shgn.SHGNCommand
    {
        public override void fsoperationBeforeSave()
        {
            set("NBF_BENEFCD", getBeneficiaryCode(getString("NP1_PROPOSAL")));
        }

        private string getBeneficiaryCode(string proposal)
        {
            rowset rs = DB.executeQuery("SELECT NVL(MAX(NBF_BENEFCD),0)+1 NBF_BENEFCD FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + proposal + "'");
            if (rs.next())
            {
                return rs.getString("NBF_BENEFCD");
            }
            else
            {
                return "1";
            }
        }

        public override void fsoperationAfterSave()
        {
            string proposal = getString("NP1_PROPOSAL");
            string ByPercent = "02";
            rowset rs = DB.executeQuery("SELECT SUM(NBF_PERCNTAGE) NBF_PERCNTAGE FROM LNBF_BENEFICIARY WHERE NP1_PROPOSAL='" + proposal + "' AND NBF_BASIS='" + ByPercent + "'");
            if (rs.next())
            {
                if (rs.getObject("NBF_PERCNTAGE") != null)
                {
                    if (rs.getDouble("NBF_PERCNTAGE") <= 0)
                    {

                        throw new ProcessException("Total Percentage must be greater than 0");
                    }
                    if (rs.getDouble("NBF_PERCNTAGE") > 100)
                    {
                        throw new ProcessException("Total Percentage is exceeding from 100%");
                    }
                }
            }

            if (get("NBF_BENNAMEARABIC") != null)
            {
                string benfCode = getString("NBF_BENEFCD");
                string NameInArabic = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(getString("NBF_BENNAMEARABIC")));

                //Update 
                DB.executeDML("UPDATE LNBF_BENEFICIARY SET NBF_BENNAMEARABIC='" + NameInArabic + "' WHERE NP1_PROPOSAL='" + proposal + "' AND NBF_BENEFCD='" + benfCode + "'");
            }

            //************* Activity Log *************//			
            Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.BENEFICIARY_UPDATED);

        }

        public override void fsoperationAfterUpdate()
        {
            fsoperationAfterSave();
        }
    }
}