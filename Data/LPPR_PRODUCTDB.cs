using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LPPR_PRODUCTDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LPPR_PRODUCTDB   (DataHolder dh):base(dh)
		
		
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LPPR_PRODUCT";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LPPR_PRODUCT";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetDDL_BASEPRODUCT_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_BASEPRODUCT_PPR_PRODCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(217);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT  WHERE PPR_BASRIDR='B' AND PPR_PRODCD <> '999'");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_BASEPRODUCT_VALIDATION_PPR_PRODCD_S_RO</method-name><method-signature>
	public static IDataReader GetDDL_BASEPRODUCT_VALIDATION_PPR_PRODCD_S_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(228);//to do we have to Optimize it too.
sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("'-'");
sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_PRODCD<>'999' ORDER BY PPR_BASRIDR,PPR_PRODCD");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
	//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_LPVF_VALIDFIELDS_VALIDATION_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_LPVF_VALIDFIELDS_VALIDATION_PPR_PRODCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query = new StringBuilder(217);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("PPR_DESCR DESC_F FROM LPPR_PRODUCT  WHERE PPR_BASRIDR = 'B'");
			string query = sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LPPR_PRODUCT";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(212);//to do we have to Optimize it too.

            //	string csdtype= SV(\"s_CCH_CODE\")  SV(\"s_CCD_CODE\")+ "'"); 

            //		sb_query.Append("SELECT PPR_PRODCD,");
            //		sb_query.Append("case when PPR_PRODCD = '074' then 'Legacy Plan'when PPR_PRODCD = '003' then 'Secured Saving Plan' end as desc_s,");
            //		//sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_BASRIDR='B' AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPCH_CHANNEL WHERE CCH_CODE = SV(\"NP1_CHANNEL\")) AND PPR_PRODCD IN (select distinct LPPU.PPR_CODE from LPPU_PURPOSE LPPU, LNPU_PURPOSE LNPU where LPPU.CPU_CODE=LNPU.CPU_CODE and NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") and nvl(NPU_SELECTED,'N')='Y') ORDER BY PPR_DESCR");
            //		//sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_BASRIDR='B' AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPCH_CHANNEL WHERE CCH_CODE = SV(\"NP1_CHANNEL\") )") ;
            //		sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_BASRIDR='B' AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPCH_CHANNEL WHERE CCH_CODE = SV(\"s_CCH_CODE\") AND CCD_CODE = SV(\"s_CCD_CODE\") AND CCS_CODE = SV(\"s_CCS_CODE\")  )  ") ;
            //		sb_query.Append("AND PPR_SNGJNT IN (SELECT CASE WHEN NP1_JOINT = 'Y' THEN 'J' ELSE 'S' END  FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=SV(\"NP1_PROPOSAL\") ) ") ;

            //		sb_query.Append("SELECT PPR_PRODCD,");
            // Above query already comments before 26-OCT-2025

            // below is original Query for Products 26-OCT-2025 

            //sb_query.Append(@"select PPR_PRODCD,csd_value DESC_F from lcsd_systemdtl , lppr_product where csh_id='PRDSC' and csd_type = (SV(""s_CCH_CODE"") || SV(""s_CCD_CODE"") || '-'  || PPR_PRODCD)
            //				and PPR_BASRIDR='B' 
            //				AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPCH_CHANNEL WHERE CCH_CODE = SV(""s_CCH_CODE"") AND CCD_CODE = SV(""s_CCD_CODE"") AND CCS_CODE = SV(""s_CCS_CODE"")  )  
            //				AND PPR_SNGJNT IN (SELECT CASE WHEN NP1_JOINT = 'Y' THEN 'J' ELSE 'S' END  FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=SV(""NP1_PROPOSAL""))  

            //				union

            //				select PPR_PRODCD, ppr_descr DESC_F from lppr_product where not exists   
            //				(select csd_value from lcsd_systemdtl where csh_id='PRDSC' and csd_type = (SV(""s_CCH_CODE"") || SV(""s_CCD_CODE"") || '-'  || PPR_PRODCD) )
            //				and PPR_BASRIDR='B' AND PPR_PRODCD IN (SELECT PPR_PRODCD FROM LPCH_CHANNEL 
            //				WHERE CCH_CODE = SV(""s_CCH_CODE"") AND CCD_CODE = SV(""s_CCD_CODE"") AND CCS_CODE = SV(""s_CCS_CODE"") )  
            //				AND PPR_SNGJNT IN (SELECT CASE WHEN NP1_JOINT = 'Y' THEN 'J' ELSE 'S' END  FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=SV(""NP1_PROPOSAL""))  
            //				") ;

            //    StringBuilder sb_query = new StringBuilder();

            // below is modify query for exclude plan State Life Child Education & Marriage + FIB for JS Bank 26-OCT-2025 

            sb_query.Append(@"
                        select PPR_PRODCD, csd_value DESC_F 
                        from lcsd_systemdtl , lppr_product 
                        where csh_id='PRDSC' 
                          and csd_type = (SV(""s_CCH_CODE"") || SV(""s_CCD_CODE"") || '-'  || PPR_PRODCD)
                          and PPR_BASRIDR='B' 
                          AND PPR_PRODCD IN (
                              SELECT PPR_PRODCD 
                              FROM LPCH_CHANNEL 
                              WHERE CCH_CODE = SV(""s_CCH_CODE"") 
                                AND CCD_CODE = SV(""s_CCD_CODE"") 
                                AND CCS_CODE = SV(""s_CCS_CODE"")
                          )
                          AND PPR_SNGJNT IN (
                              SELECT CASE WHEN NP1_JOINT = 'Y' THEN 'J' ELSE 'S' END  
                              FROM LNP1_POLICYMASTR 
                              WHERE NP1_PROPOSAL=SV(""NP1_PROPOSAL"")
                          )

union

                                select PPR_PRODCD, ppr_descr DESC_F 
                                from lppr_product 
                                where not exists (
                                    select csd_value 
                                    from lcsd_systemdtl 
                                    where csh_id='PRDSC' 
                                      and csd_type = (SV(""s_CCH_CODE"") || SV(""s_CCD_CODE"") || '-'  || PPR_PRODCD)
                                )
                                and PPR_BASRIDR='B' 
                                AND PPR_PRODCD IN (
                                    SELECT PPR_PRODCD 
                                    FROM LPCH_CHANNEL 
                                    WHERE CCH_CODE = SV(""s_CCH_CODE"") 
                                      AND CCD_CODE = SV(""s_CCD_CODE"") 
                                      AND CCS_CODE = SV(""s_CCS_CODE"")
                                )
                                AND PPR_SNGJNT IN (
                                    SELECT CASE WHEN NP1_JOINT = 'Y' THEN 'J' ELSE 'S' END  
                                    FROM LNP1_POLICYMASTR 
                                    WHERE NP1_PROPOSAL=SV(""NP1_PROPOSAL"")
                                )
                                ");

            // Apply filter only if BankCode = 'F'
            string query = sb_query.ToString();

            if (System.Web.HttpContext.Current.Session["BankCode"] != null &&
                System.Web.HttpContext.Current.Session["BankCode"].ToString().Trim().ToUpper() == "F")
            {
                // Wrap in outer SELECT to filter by DESC_F after UNION
                query = "SELECT * FROM (" + query + @") WHERE DESC_F <> 'State Life Child Education & Marriage + FIB'";
            }

            // Let EnvHelper substitute SV(...) placeholders
            query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();


            // below original
            // string query =sb_query.ToString();
            //query = EnvHelper.Parse(query);
            //IDbCommand myCommand = DB.CreateCommand(query);
            //return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO_FOR_NIB</method-name><method-signature>
        public static IDataReader GetDDL_ILUS_ET_NM_PLANDETAILS_PPR_PRODCD_RO_FOR_NIB()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(212);//to do we have to Optimize it too.
		sb_query.Append("SELECT PPR_PRODCD,");
		sb_query.Append("case when PPR_PRODCD = '074' then 'Legacy Plan'when PPR_PRODCD = '003' then 'Secured Saving Plan' end as desc_s,");
		sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT   WHERE PPR_BASRIDR='B' ") ;

		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ST_BRANCHPRODUCT_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ST_BRANCHPRODUCT_PPR_PRODCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(172);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT  WHERE PPR_BASRIDR='B' AND PPR_PRODCD<>'999'  order by PPR_PRODCD ");
			string query=sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_LPVF_VALIDFIELDS_DECISION_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_LPVF_VALIDFIELDS_DECISION_PPR_PRODCD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query = new StringBuilder(195);//to do we have to Optimize it too.
			sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("'-'");
			sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
			sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT  WHERE PPR_BASRIDR = 'B'  and PPR_PRODCD <> '999' order by PPR_PRODCD ");
			string query = sb_query.ToString();
			query = EnvHelper.Parse(query);
			IDbCommand myCommand = DB.CreateCommand(query);
			return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_BASEPRODUCT_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_BASEPRODUCT_PPR_PRODCD1_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(217);//to do we have to Optimize it too.
		sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("'-'");
		sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("PPR_DESCR DESC_F  FROM LPPR_PRODUCT  WHERE PPR_BASRIDR='B' AND PPR_PRODCD <> '999'");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_LPVF_PPRID_DESCRIPTION_PPR_PRODCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_LPVF_PPRID_DESCRIPTION_PPR_PRODCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(213);//to do we have to Optimize it too.
		sb_query.Append("SELECT PPR_PRODCD,PPR_PRODCD || ' - ' || PPR_DESCR DESC_F  FROM LPPR_PRODUCT  WHERE PPR_PRODCD <> '999' ORDER BY PPR_BASRIDR, PPR_PRODCD");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

}
}
