using System;
using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace BA
{
	/// <summary>
	/// Summary description for BAUtility.
	/// </summary>
	public class BAUtility
	{
		
		public static void RecalculatePemium(string proposal, int maturityAge)
		{
			//updatePlanRiderTerm(proposal, maturityAge);
			//string query = "SELECT * FROM LNPR_PRODUCT where NP1_PROPOSAL='" + proposal + "' AND NVL(NPR_BASICFLAG,'N')='Y'";
			string query = " SELECT lnpr.NP1_PROPOSAL,lnpr.NP2_SETNO,lnpr.PPR_PRODCD,lnpr.PCU_CURRCODE,CCB_CODE,NPR_SUMASSURED,NPR_PAIDUPTOAGE, " 
				+ " NPR_BENEFITTERM,NPR_PREMIUMTER,NPR_PREMIUM,NPR_INCLUDELOADINNIV,NPR_PREMIUMDISCOUNT,NPR_EXCESSPREMIUM, "
				+ " NPR_COMMLOADING,NPR_TOTPREM,NPR_BASICFLAG,NP1_RETIREMENTAGE "
				+ " from LNPR_PRODUCT lnpr, Lnp1_Policymastr lnp1 "
				+ " where lnp1.np1_proposal = lnpr.np1_proposal "
				+ " and lnp1.NP1_PROPOSAL='" + proposal + "' AND NVL(NPR_BASICFLAG,'N')='Y'";
			rowset rs = DB.executeQuery(query);
	
			if(rs.next())
			{
				NameValueCollection columnNameValue=new NameValueCollection();
				
				columnNameValue.Add("PPR_PRODCD",          rs.getString("PPR_PRODCD"));
				columnNameValue.Add("PCU_CURRCODE",        rs.getString("PCU_CURRCODE"));
				columnNameValue.Add("CCB_CODE",            rs.getString("CCB_CODE"));
				columnNameValue.Add("NPR_SUMASSURED",      rs.getDouble("NPR_SUMASSURED"));
				columnNameValue.Add("NPR_PAIDUPTOAGE",     rs.getDouble("NPR_PAIDUPTOAGE"));
				columnNameValue.Add("NPR_BENEFITTERM",     rs.getDouble("NPR_BENEFITTERM"));
				columnNameValue.Add("NPR_PREMIUMTER",      rs.getDouble("NPR_PREMIUMTER"));
				columnNameValue.Add("NPR_PREMIUM",         rs.getDouble("NPR_PREMIUM"));
				columnNameValue.Add("NPR_INCLUDELOADINNIV",rs.getString("NPR_INCLUDELOADINNIV"));
				columnNameValue.Add("NPR_PREMIUMDISCOUNT", rs.getDouble("NPR_PREMIUMDISCOUNT"));
				columnNameValue.Add("NPR_EXCESSPREMIUM",   rs.getDouble("NPR_EXCESSPREMIUM"));
				columnNameValue.Add("NPR_COMMLOADING",     rs.getString("NPR_COMMLOADING"));
				columnNameValue.Add("NPR_TOTPREM",         rs.getDouble("NPR_TOTPREM"));
				columnNameValue.Add("NP1_PROPOSAL",        rs.getString("NP1_PROPOSAL"));
				columnNameValue.Add("NP2_SETNO",           rs.getDouble("NP2_SETNO"));
				columnNameValue.Add("NPR_BASICFLAG",       rs.getString("NPR_BASICFLAG"));
				

				
				string processName = "ace.Calculate_Premium";
				Type type = Type.GetType(processName);
				if (type != null)
				{
					shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
					NameValueCollection[] dataRows = new NameValueCollection[1];
					bool[] SelectedRowIndexes = new bool[1];
					dataRows[0] = columnNameValue;
					SelectedRowIndexes[0] = true;
					proccessCommand.setAllFields(columnNameValue);
					proccessCommand.setEntityID("ILUS_ET_NM_PLANDETAILS");
					proccessCommand.setPrimaryKeys(SHAB.Business.LNPR_PRODUCT.PrimaryKeys);
					proccessCommand.setTableName("LNPR_PRODUCT");
					proccessCommand.setDataRows(dataRows);
					proccessCommand.setSelectedRows(SelectedRowIndexes);
					string result = proccessCommand.processing();

					//DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM="+Benifit_Term+" WHERE NP1_PROPOSAL='"+SessionObject.GetString("NP1_PROPOSAL")+"' AND PPR_PRODCD!='"+rs.getString("PPR_PRODCD")+"'");
					//UpdateRiderBenifitTerm(rs.getString("NP1_PROPOSAL"),rs.getString("PPR_PRODCD"),rs.getInt("NP1_RETIREMENTAGE"));
			
				}

				//UpdateRiderBenifitTerm(rs.getString("NP1_PROPOSAL"),rs.getString("PPR_PRODCD"),rs.getInt("NP1_RETIREMENTAGE"));
				//if (result.Length>0) PrintMessage(result);
			}
		}

		/*public static void UpdateRiderBenifitTerm(string Proposal, string prodCode, int maturityAge)
		{
			if(prodCode=="020")
			{
				int benifitTerm=0;
				if(maturityAge>Convert.ToInt16(60))
				{
					benifitTerm = maturityAge - Convert.ToInt16(60);				
				}
				else
				{				
					benifitTerm = Convert.ToInt16(60)- maturityAge;
				}
				if(benifitTerm<21)
				{
					DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=" + benifitTerm + " WHERE NP1_PROPOSAL='" + Proposal + "' AND PPR_PRODCD<>'" + prodCode + "'");
					SessionObject.Add("RIDER_BENEFITTERM", benifitTerm);
				}	
			}
		}*/

		public static bool Validate_Check()
		{
			try
			{
				if(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"]).ToLower() == "false")
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch(Exception e)
			{
				Console.Write(e.Message);
				return true;
			}
		}

		public static string Validate_Text()
		{
			string validatText = "Proposal has been validated";
			try
			{
				if(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validate_check"]) != null)
				{
					validatText = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["validatetext"]);
				}
				return validatText;
			}
			catch(Exception e)
			{
				Console.Write(e.Message);
				return validatText;
			}
		}

		/*private static void updatePlanRiderTerm(string proposal,int maturityAge)
		{
			System.Data.OleDb.OleDbConnection dbCon=null;
			System.Data.OleDb.OleDbDataAdapter dbAdapter;
			System.Data.OleDb.OleDbCommand dbCom;

			try
			{
				string strSQL = "Update lnp1_policymastr SET Np1_Retirementage="+maturityAge+" WHERE NP1_PROPOSAL='"+proposal+"'";

				string str_connString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];
				dbCon = new System.Data.OleDb.OleDbConnection(str_connString);
				dbCon.Open();
				dbAdapter = new System.Data.OleDb.OleDbDataAdapter();
				dbCom = new System.Data.OleDb.OleDbCommand(strSQL,dbCon);
				int x= dbCom.ExecuteNonQuery();
			}
			catch(Exception e)
			{}
			finally
			{
				dbCon.Close();
			}
		}*/
	}

	


}
