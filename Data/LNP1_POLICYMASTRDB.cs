using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
using System.Data.OracleClient;
using System.IO;
using SHMA.Enterprise.Presentation;

namespace SHAB.Data
{
    public class LNP1_POLICYMASTRDB : SHMA.CodeVision.Data.DataClassBase
    {
       // string BankCode = System.Web.HttpContext.Current.Session["BankCode"].ToString();
        //<constructor>
        public LNP1_POLICYMASTRDB(DataHolder dh) : base(dh)
        { }
        //</constructor>
        //<property><property-name>TableName</property-name><property-signature>
        public override String TableName
        {
            //</property-signature><property-body>
            get { return "LNP1_POLICYMASTR"; }
            //			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//			//</property-body>
        }
        //</property>
        //<property><property-name>RecordCount</property-name><property-signature>
        public static int RecordCount
        {
            //</property-signature><property-body>
            get
            {
                const string strQuery = "SELECT COUNT(*) FROM LNP1_POLICYMASTR";
                return (int)DB.CreateCommand(strQuery).ExecuteScalar();
            }
            ////////////////////////////////////////</property-body>
        }
        //</property>
        //<method><method-name>FindByPK</method-name><method-signature>
        public DataHolder FindByPK(string NP1_PROPOSAL)
        {
            //</method-signature><method-body>
            String strQuery = "SELECT * FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=? ";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, NP1_PROPOSAL));

            this.Holder.FillData(myCommand, "LNP1_POLICYMASTR"); return this.Holder;
            //</method-body>
        }
        //</method>

        //<method><method-name>getAll_RO</method-name><method-signature>
        public static IDataReader getAll_RO()
        {
            //</method-signature><method-body>
            const String strQuery = "SELECT * FROM LNP1_POLICYMASTR";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_PROPOSAL_lister_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_PROPOSAL_lister_RO(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_PROPOSAL_lister_filter_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_PROPOSAL_lister_filter_RO(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE  ({0} like '{1}')  AND NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        public static IDataReader GetProposalForExcelReport(string CsvProposalNos)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("select NP1.NP1_PROPOSAL TRANS_ID,");
            sb_query.Append("       nvl(nu1.pbb_branchcode ,CCS.ccs_field1) F_BRANCH_CODE,");//Modified new line
            sb_query.Append("       coalesce(NP1.NP1_ACCOUNTNO,NP1.np1_iban) F_ACC_TYPE,");
            sb_query.Append("       '10' F_ACC_NO,");
            sb_query.Append("       'C' F_CLSL,");
            sb_query.Append("       '586' F_ACC_CURR,");
            sb_query.Append("       SUBSTR(CCD.ccd_field1, 1,INSTR(CCD.ccd_field1,',',1,1)-1)");
            sb_query.Append("        T_BRANCH_CODE,");
            sb_query.Append("       substr(CCD.Ccd_Field1,(instr(CCD.Ccd_Field1,',',1,1))+1,length(CCD.Ccd_Field1)) T_ACC_NO,");
            sb_query.Append("       '10' T_ACC_TYPE,");
            sb_query.Append("       '586' T_ACC_CURR,");
            sb_query.Append("       'C' T_CLSL,");
            sb_query.Append("       (SELECT SUM(NVL(NPR_PREMIUM,0))+SUM(NVL(NPR_LOADING,0)) from LNPR_PRODUCT WHERE NP1_PROPOSAL =NP1.NP1_PROPOSAL) AMOUNT,");
            sb_query.Append("       '586' TRANS_CURR,");
            sb_query.Append("       TO_CHAR(SYSDATE, 'MM/DD/YY') VAL_DATE,");
            sb_query.Append("       'State Life Premium deduction against proposal ' || NP1.NP1_PROPOSAL USER_NAR,");
            sb_query.Append("       ' ' INST_NO,");
            sb_query.Append("       'HOCOM01' F_CRC,");
            sb_query.Append("       'HOCOM01' T_CRC,");
            sb_query.Append("       'N' WHT_FLG,");
            sb_query.Append("       '00' TRAN_CODE");
            sb_query.Append("  from lnp1_policymastr  np1,");
            sb_query.Append("       ccs_chanlsubdetl  ccs,");
            sb_query.Append("       ccd_channeldetail ccd,");
            sb_query.Append("       lnpr_product      npr,");
            sb_query.Append("       lnu1_underwriti   nu1");
            sb_query.Append(" WHERE np1.np1_channeldetail = ccd.ccd_code");
            sb_query.Append("   and np1.np1_channel = ccd.cch_code");
            sb_query.Append("   and np1.np1_channeldetail = ccs.ccd_code");
            sb_query.Append("   and np1.np1_channel = ccs.cch_code");
            sb_query.Append("   and np1.np1_channelsdetail = ccs.ccs_code");
            sb_query.Append("   and np1.np1_proposal = npr.np1_proposal");
            sb_query.Append("   and npr.npr_basicflag = 'Y'");
            sb_query.Append("   and nu1.np1_proposal = npr.np1_proposal");//Added new line
            sb_query.Append("   and nu1.nu1_life = 'F'");//Added new line
            sb_query.Append("   and np1.np1_proposal in (" + CsvProposalNos + ")");

            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }


        public static DataTable GetBancaMisUpload(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);

            sb_query.Append(@" SELECT  p1.np1_propdate ,
       nvl(p1.np1_proposalref,'0') || '/' || case when  lr.ppr_prodcd ='003' then 'EDP' when  lr.ppr_prodcd ='005' then 'TPP' when  lr.ppr_prodcd ='074' then 'SP' end,
ph.nph_fullname Client  ,ph.nph_idno CNIC  ,
getcsvbeneficiaries(p1.np1_proposal) NomineeName, GetCSVRelations(p1.np1_proposal)  RelationWithNominee,   
translate(LNAD.NAD_ADDRESS1,'""''',' ') Address, LNAD.NAD_TELNO2 ,    LNAD.NAD_TELNO1 Phone,   LNAD.NAD_MOBILE  Mobile, 
case when  lr.ppr_prodcd ='003' then 'EDP' when  lr.ppr_prodcd ='005' then 'TPP' when  lr.ppr_prodcd ='074' then 'SP' end,
                    (SELECT SUM(lnpr.npr_premium) + p2.np2_totload FROM LNPR_PRODUCT LNPR      
                    WHERE lnpr.np1_proposal = p2.np1_proposal and  lnpr.np2_setno = P2.NP2_SETNO) PremiumAmount, 

case when p1.cmo_mode = 'A' then 'Annually'when p1.cmo_mode = 'H' then 'Half Yearly' when p1.cmo_mode = 'Q' then 'Quarterly' when p1.cmo_mode = 'M' then 'Monthly'  end ModeofPremium, 
pr.npr_sumassured SumAssured, TO_CHAR(ph.nph_birthdate , 'MM/DD/YYYY')  DateofBirth  ,ph.nph_sex  , 0,0,'',p1.np1_policyno PolicyNo,  
              case             when NVL(p1.cst_statuscd, '0') ='0'  then 'ACTIVE (INFORCE)'            
              when nvl(p1.cst_statuscd, '0') = '002' then 'Referred' else 'Pending' end PolicyStatus, 
            '' CommencementDate  ,'' MaturityDate  , '' IssuedDate  ,'' NextDueDate  , 
            '50' commrate,            '' Commission  

              FROM LNP1_POLICYMASTR P1
            INNER JOIN LNU1_UNDERWRITI u1 on p1.np1_proposal = u1.np1_proposal                  
            INNER join LNPH_PHOLDER ph on PH.NPH_CODE = U1.NPH_CODE AND PH.NPH_LIFE = U1.NPH_LIFE AND U1.NU1_LIFE = 'F'                 
            INNER JOIN LNP2_POLICYMASTR P2 ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL
                            AND P2.NP2_SETNO = (SELECT MAX(p2.NP2_SETNO) FROM LNP2_POLICYMASTR NP2
                            WHERE p2.NP1_PROPOSAL = P1.NP1_PROPOSAL --AND NVL(p2.NP2_APPROVED, 'N') = 'Y'
                            )
            INNER join LNPR_PRODUCT PR on pr.np1_proposal = p1.np1_proposal and pr.np2_setno = p2.np2_setno and pr.npr_basicflag = 'Y'
            inner join LPPR_PRODUCT LR ON LR.PPR_PRODCD = PR.PPR_PRODCD
            INNER JOIN LNAD_ADDRESS LNAD ON LNAD.NPH_CODE = PH.NPH_CODE AND LNAD.NAD_ADDRESSTYP ='C'

            WHERE PR.NPR_SUMASSURED > 0 AND p1.np1_policyno IS NULL    AND nvl(p2.np2_substandar, 'N') = 'N'
						");

            if (!IsAdmin)
            {
                sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    sb_query.Append("  and p1.np1_channelsdetail='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                }
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            sb_query.Append("           ORDER BY p1.np1_proposal, p1.np1_propdate");



            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            con.Close();
            DataTable dtTable = ds.Tables["BANCRPT"];

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in dtTable.Rows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }


        public static DataTable GetIlasMisUpload(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);

            sb_query.Append(@" SELECT vw.np1_propdate , nvl(vw.np1_proposalref,'0') || '/' || case when  ppr.ppr_prodcd ='003' then 'EDP' when  ppr.ppr_prodcd ='005' then 'TPP' when  ppr.ppr_prodcd ='074' then 'SP' end,
vw.nph_fullname Client  , vw.nph_idno CNIC  ,
				getcsvbeneficiaries(vw.np1_proposal) NomineeName, GetCSVRelations(vw.np1_proposal)  RelationWithNominee,   
				translate(LNAD.NAD_ADDRESS1,'""''',' ') Address, LNAD.NAD_TELNO2 ,LNAD.NAD_TELNO1 Phone,   LNAD.NAD_MOBILE  Mobile, 
case when  ppr.ppr_prodcd ='003' then 'EDP' when  ppr.ppr_prodcd ='005' then 'TPP' when  ppr.ppr_prodcd ='074' then 'SP' end ,vw.gross_prem,
				case when vw.cmo_mode = 'A' then 'Annually'when vw.cmo_mode = 'H' then 'Half Yearly' when vw.cmo_mode = 'Q' then 'Quarterly' when vw.cmo_mode = 'M' then 'Monthly'  end ModeofPremium, 
				vw.npr_sumassured SumAssured,TO_CHAR(vw.nph_birthdate  , 'MM/DD/YYYY')  DateofBirth  ,vw.nph_sex  , 0,0,'', vw.np1_policyno PolicyNo,  
				(select CST_DESCR from  lcst_ppstatus where cst_statuscd=vw.cst_statuscd) Status ,
TO_CHAR(vw.np2_commendate , 'MM/DD/YYYY')  CommencementDate  , TO_CHAR(vw.npr_maturitydate , 'MM/DD/YYYY')    MaturityDate  ,  
trunc(vw.np1_issuedate ) IssuedDate  ,TO_CHAR(vw.np2_nextduedat  , 'MM/DD/YYYY')   NextDueDate  , 

--vw.np2_commendate , vw.npr_maturitydate ,  vw.np1_issuedate  IssuedDate  ,vw.np2_nextduedat  , 

				'50' commrate,ncm.ncm_value Commission  

            FROM VW_POLICYMASTR VW , lnad_address lnad ,LPPR_PRODUCT PPR , lncm_commission ncm

            WHERE lnad.nph_code=vw.nph_code AND vw.NPR_SUMASSURED > 0 AND LNAD.NAD_ADDRESSTYP ='C' AND PPR.PPR_PRODCD = VW.ppr_prodcd
            AND NCM.NP1_PROPOSAL =vw.np1_proposal and ncm.np2_setno =vw.np2_setno and ncm.cyr_yearcode ='01S'

						");

            if (!IsAdmin)
            {
                sb_query.Append("  and VW.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    BranchCode = "0" + BranchCode;
                    sb_query.Append("  and VW.PBB_BRANCHCODE='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and VW.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                }
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(vw.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(vw.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            sb_query.Append("           ORDER BY vw.np1_proposal, vw.np1_propdate ");



            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            con.Close();
            DataTable dtTable = ds.Tables["BANCRPT"];

            DataRow[] foundRows;

            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            else
            {
                foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }

        public static DataTable GetExcelReportForBancaMis(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);

            sb_query.Append(@" 
SELECT  '' SNo  , p1.np1_propdate ,
            p1.np1_propdate InputDate  ,ph.nph_fullname Client  , TO_CHAR(ph.nph_birthdate , 'MM/DD/YYYY')  DateofBirth  ,
nvl(p1.np1_proposalref,'0') || '/' || case when  lr.ppr_prodcd ='003' then 'EDP' when  lr.ppr_prodcd ='005' then 'TPP' when  lr.ppr_prodcd ='074' then 'SP' end as ApplicationNo  ,
p1.np1_policyno PolicyNo,  
            case when not exists  (select csd_value from SLBANCAPRD.lcsd_systemdtl where csh_id = 'PRDSC' and csd_type = p1.aag_agcode || '-' || p1.ppr_prodcd) then lr.ppr_descr  
            else  (select csd_value from lcsd_systemdtl where csh_id ='PRDSC' and csd_type = p1.aag_agcode  || '-' ||  p1.ppr_prodcd) end as Product,

            p1.np1_accountno AccountNo,  
       (select c.ccs_descr from ccs_chanlsubdetl c where c.cch_code =p1.np1_channel  and  c.ccd_code =p1.np1_channeldetail and c.ccs_code = p1.np1_channelsdetail)  BankBranch,
'' BankSaleOfficer  ,
            case when p1.cmo_mode = 'A' then 'Annually'when p1.cmo_mode = 'H' then 'Half Yearly' when p1.cmo_mode = 'Q' then 'Quarterly' when p1.cmo_mode = 'M' then 'Monthly'  end ModeofPremium, 
            '0' FAP  ,
                      (SELECT SUM(lnpr.npr_premium)+p2.np2_totload FROM LNPR_PRODUCT LNPR
                      WHERE lnpr.np1_proposal = p2.np1_proposal and
                      lnpr.np2_setno  =  P2.NP2_SETNO) ModalPremium  , 
            (DECODE(p1.cmo_mode,'A',1,'M',12,'Q',4,'H',2,1) * pr.npr_premium + nvl(pr.npr_loading,0))  AnnualPremium  ,


            case    
                    when LENGTH(NVL(np1_policyno, '')) > 2 then 'Policy Issued'    
                    when nvl(p2.np2_substandar, 'N') = 'Y' then 'Referred'  
                      when nvl(p1.np1_selected, 'D') = 'R' then 'Compliance OK' 
                        when nvl(p1.np1_selected, 'D') = 'F' then 'Payment Waiting'  
                        when nvl(p1.np1_selected, 'D') = 'Y' then 'Payment Deducted' 
                          else 'Pending'  end Status ,
       (select l.cm_comments from lncm_comments l where l.np1_proposal=p1.np1_proposal
       and l.cm_serial_no = (SELECT NVL(MAX(CM_SERIAL_NO),0) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = p1.np1_proposal)) Requirements,
       (select l.cm_comments from lncm_comments l where l.np1_proposal=p1.np1_proposal
       and l.cm_serial_no = (SELECT NVL(MAX(CM_SERIAL_NO),0) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = p1.np1_proposal))  Reasons,
 '' PolicyYear  ,          
            '-' IssuedDate  , '-' NextDueDate  , '-' StatusDate  ,'-' CommencementDate  ,'-' MaturityDate  ,
            pr.npr_premiumter   Term  ,

            p1.np1_totreceiv TotalPremiumReceived  ,'' AgentCode  ,'' BSCCoordinator  ,

             translate(LNAD.NAD_ADDRESS1,'""''',' ') Address,     LNAD.NAD_TELNO1 Phone,   LNAD.NAD_MOBILE  Mobile, p1.np1_creditbalance CreditBalance  ,

            p1.np1_totcommacc Commission  , p1.pbb_branchcode BranchCode  , '' DispatchDate  ,ph.nph_idno CNIC  ,'' agentname  ,

'' ChequeDate,

            '0' Counter  ,pr.npr_sumassured SumAssured

              FROM LNP1_POLICYMASTR P1

            INNER JOIN LNU1_UNDERWRITI u1 on p1.np1_proposal = u1.np1_proposal                  
            INNER join LNPH_PHOLDER ph on PH.NPH_CODE = U1.NPH_CODE AND PH.NPH_LIFE = U1.NPH_LIFE AND U1.NU1_LIFE = 'F'                 
            INNER JOIN LNP2_POLICYMASTR P2 ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL
                            AND P2.NP2_SETNO = (SELECT MAX(NP2.NP2_SETNO) FROM LNP2_POLICYMASTR NP2
                            WHERE NP2.NP1_PROPOSAL = P1.NP1_PROPOSAL )
            INNER join LNPR_PRODUCT PR on pr.np1_proposal = p1.np1_proposal and pr.np2_setno = p2.np2_setno and pr.npr_basicflag = 'Y'
            inner join LPPR_PRODUCT LR ON LR.PPR_PRODCD = PR.PPR_PRODCD
            INNER JOIN LNAD_ADDRESS LNAD ON LNAD.NPH_CODE = PH.NPH_CODE AND LNAD.NAD_ADDRESSTYP ='C'

            WHERE lnad.nph_code=ph.nph_code AND pr.NPR_SUMASSURED > 0 AND LNAD.NAD_ADDRESSTYP ='C' 
            AND p1.np1_policyno IS NULL    AND nvl(p2.np2_substandar, 'N') = 'N'

						");

            if (!IsAdmin)
            {
                sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    sb_query.Append("  and p1.np1_channelsdetail='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                }
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            sb_query.Append("            ORDER BY SNO, p1.np1_proposal, p1.np1_propdate  ");



            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            con.Close();
            DataTable dtTable = ds.Tables["BANCRPT"];

            DataRow[] foundRows;

            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >= #" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <= #" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
                //foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >='"+Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy")+"' AND ISSUEDDATE <='"+Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy")+"' ");

            }
            else
            {
                foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >= #" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <= #" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
                //foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >='"+Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy")+"' AND NP1_PROPDATE <='"+Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy")+"' ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }

        public static DataTable GetExcelReportForIlasMis(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);

            sb_query.Append(@" SELECT  '' SNo  , vw.np1_propdate ,
						vw.np1_propdate InputDate  ,translate(vw.nph_fullname,'""''',' ') Client  ,TO_CHAR(vw.nph_birthdate , 'MM/DD/YYYY')  DateofBirth  ,
nvl(vw.np1_proposalref,'0') || '/' || case when  ppr.ppr_prodcd ='003' then 'EDP' when  ppr.ppr_prodcd ='005' then 'TPP' when  ppr.ppr_prodcd ='074' then 'SP' end as ApplicationNo  ,
vw.np1_policyno PolicyNo,  
						case when not exists  (select csd_value from lcsd_systemdtl where csh_id = 'PRDSC' and csd_type = vw.aag_agcode || '-' || vw.ppr_prodcd) then PPR.ppr_descr  
						else  (select csd_value from lcsd_systemdtl where csh_id ='PRDSC' and csd_type = vw.aag_agcode  || '-' ||  vw.ppr_prodcd) end as Product,
						  
						vw.np1_accountno AccountNo,  translate(vw.PBB_BRANCHNAME,'""''',' ') BankBranch,  '' BankSaleOfficer  ,
						case when vw.cmo_mode = 'A' then 'Annually'when vw.cmo_mode = 'H' then 'Half Yearly' when vw.cmo_mode = 'Q' then 'Quarterly' when vw.cmo_mode = 'M' then 'Monthly'  end ModeofPremium, 
						'0' FAP  ,vw.gross_prem ModalPremium  , vw.gross_prem AnnualPremium   , 
/*(DECODE(vw.cmo_mode,'A',1,'M',12,'Q',4,'H',2,1) * vw.npr_premium + nvl(vw.npr_loading,0))  AnnualPremium  ,*/

						(select CST_DESCR from  lcst_ppstatus where cst_statuscd=vw.cst_statuscd) Status ,
						'' Requirements  ,
						'' Reasons  ,
						(select MAX(policyear) from view_adjusted_premium  where np1_proposal =vw.np1_proposal) PolicyYear  ,
vw.np1_issuedate   IssuedDate  , vw.np2_nextduedat   NextDueDate  ,vw.np1_statusdate StatusDate  ,
vw.np2_commendate CommencementDate  , vw.npr_maturitydate  MaturityDate  ,
						vw.npr_premiumter   Term  ,
						vw.np1_totreceiv TotalPremiumReceived  ,'' AgentCode  ,'' BSCCoordinator  ,
						translate(LNAD.NAD_ADDRESS1,'""''',' ') Address,     LNAD.NAD_TELNO1 Phone,   LNAD.NAD_MOBILE  Mobile, vw.np1_creditbalance CreditBalance  ,
						vw.np1_totcommacc Commission  , vw.pbb_branchcode BranchCode  , '' DispatchDate  ,vw.nph_idno CNIC  ,'' agentname  ,
       (select max(rrc_chequedate) from lrrc_receipt rrc where rrc_receiptno in (select rrc_receiptno from lrrd_rectdetail rrd 
                  where rrc.rrc_receiptno=rrd.rrc_receiptno and gsc_type = '1010' and gca_account = vw.np1_proposal 
                  and rrd.rrd_canceldat is null and rrc.rrc_chequeno is not null))  ChequeDate,

						'0' Counter  ,vw.npr_sumassured SumAssured

						FROM VW_POLICYMASTR VW , lnad_address lnad ,LPPR_PRODUCT PPR

						WHERE lnad.nph_code=vw.nph_code AND vw.NPR_SUMASSURED > 0 AND LNAD.NAD_ADDRESSTYP ='C' AND PPR.PPR_PRODCD = VW.ppr_prodcd

						");

            if (!IsAdmin)
            {
                sb_query.Append("  and VW.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    BranchCode = "0" + BranchCode;
                    sb_query.Append("  and VW.PBB_BRANCHCODE='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and VW.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                }
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(vw.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(vw.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            sb_query.Append("           ORDER BY SNO, vw.np1_proposal, vw.np1_propdate ");



            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds.Tables["BANCRPT"];

            DataRow[] foundRows;

            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            else
            {
                foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }


        public static DataTable GetExcelReportForDataDump(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);

            sb_query.Append(@" SELECT (select bi.polc_np1_issuedate from ilas_banca_interface bi where bi.polc_np1_proposal = P1.NP1_PROPOSAL) ISSUEDDATE
							,  P1.NP1_PROPDATE ,
							P1.NP1_PROPDATE PolicyIssueDate,  
							add_months(p2.np2_commendate,decode(p2.cmo_mode,'A',12,'H',6,'Q',3,'M',1,0)) RenewalDueDate,
							  
							pr.npr_maturitydate MaturityDate,
							  
							TO_CHAR(p1.np1_propdate, 'DD-Mon-YYYY') ProposalDate,
							TO_CHAR(p1.np1_propdate, 'DD-Mon-YYYY') PropSignDate,
							P1.NP1_PROFREF PaymentRef,
							--substr(p1.np1_comments, instr(p1.np1_comments, ':', 1) + 1, 12) PropSignDate,
							--  substr(p1.np1_comments, instr(p1.np1_comments, ':', 1, 2) + 1) PaymentRef,
							p1.np1_proposalref ApplicationNo,  
							  
							case when LENGTH(NVL(np1_policyno, '')) > 2 then TO_CHAR(p2.np2_commendate, 'DD-Mon-YYYY') end CommencementDate,
							  
							p1.np1_proposal as Proposal_Number,
							  
							TO_CHAR(' ' || p1.np1_policyno) as PolicyNo,
							  
							PH.NPH_CODE,
							ph.nph_fullname CustomerName,
							ph.nph_idno CustomerCNICNumber, 
							ph.NPH_BIRTHDATE DateofBirth ,
							ph.nph_sex Gender,  
							          
									P2.NP2_AGEPREM ClientAge,
							          
								/* case when not exists  (select csd_value from lcsd_systemdtl where csh_id = 'PRDSC'  
									and csd_type = p1.np1_channel || p1.np1_channeldetail || '-' || LR.ppr_prodcd)  
									then LR.ppr_descr   else  (select csd_value from lcsd_systemdtl where csh_id ='PRDSC' 
									and csd_type = p1.np1_channel || p1.np1_channeldetail || '-' ||  LR.ppr_prodcd) end as PlanName,*/
							          
											case when not exists  (select csd_value from lcsd_systemdtl where csh_id = 'PRDSC'  
									and csd_type = 
									p2.aag_agcode || '-' || LR.ppr_prodcd)  
									then LR.ppr_descr   else  (select csd_value from lcsd_systemdtl where csh_id ='PRDSC' 
									and csd_type = p2.aag_agcode  || '-' ||  LR.ppr_prodcd) end as PlanName, 

							           
									case when p2.cmo_mode = 'A' then 'Annually'    
									when p2.cmo_mode = 'H' then 'Half Yearly'    
									when p2.cmo_mode = 'Q' then 'Quarterly'    
									when p2.cmo_mode = 'M' then 'Monthly'  end PayMode, 
										(SELECT SUM(lnpr.npr_premium) + p2.np2_totload FROM LNPR_PRODUCT LNPR      
										WHERE lnpr.np1_proposal = p2.np1_proposal and  lnpr.np2_setno = P2.NP2_SETNO) PremiumAmount, 
										pr.Npr_Sumassured SumAssured, 
										pr.npr_benefitterm Term, 
										/* case    
										when LENGTH(NVL(np1_policyno, '')) > 2 then 'Policy Issued'    
										when nvl(p2.np2_substandar, 'N') = 'Y' then 'Referred'  
											when nvl(p1.np1_selected, 'D') = 'R' then 'Compliance OK' 
												when nvl(p1.np1_selected, 'D') = 'F' then 'Payment Waiting'  
												when nvl(p1.np1_selected, 'D') = 'Y' then 'Payment Deducted' 
													else 'Pending'  end*/ 
													'Posted'  InsuranceRemarks,
							case             when NVL(p1.cst_statuscd, '0') ='0'  then 'ACTIVE (INFORCE)'            
							when nvl(p1.cst_statuscd, '0') = '002' then 'Referred'        
								else 'Pending'           end PolicyStatus, 
											translate(LNAD.NAD_ADDRESS1,'""''',' ') ADDRESS, 
											LNAD.NAD_TELNO1 ResidenceNumber,
											LNAD.NAD_TELNO2  OfficeNumber ,
											LNAD.NAD_MOBILE  CellNumber,  
											getcsvbeneficiaries(p1.np1_proposal) NomineeName, GetCSVRelations(p1.np1_proposal)  RelationWithNominee,   
							'0' FAPAmount  , '0' Counter, '50' BankCommissionRate  ,P1.NP1_TOTCOMMACC BankCommission      
							    
							FROM LNP1_POLICYMASTR P1

						INNER JOIN LNU1_UNDERWRITI u1 on p1.np1_proposal = u1.np1_proposal                  
						INNER join LNPH_PHOLDER ph on PH.NPH_CODE = U1.NPH_CODE AND PH.NPH_LIFE = U1.NPH_LIFE AND U1.NU1_LIFE = 'F'                 
						INNER JOIN LNP2_POLICYMASTR P2 ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL
														AND P2.NP2_SETNO = (SELECT MAX(NP2.NP2_SETNO) FROM LNP2_POLICYMASTR NP2
														WHERE NP2.NP1_PROPOSAL = P1.NP1_PROPOSAL AND NVL(NP2.NP2_APPROVED, 'N') = 'Y')
						INNER join LNPR_PRODUCT PR on pr.np1_proposal = p1.np1_proposal and pr.np2_setno = p2.np2_setno and pr.npr_basicflag = 'Y'
						inner join LPPR_PRODUCT LR ON LR.PPR_PRODCD = PR.PPR_PRODCD
						INNER JOIN LNAD_ADDRESS LNAD ON LNAD.NPH_CODE = PH.NPH_CODE AND LNAD.NAD_ADDRESSTYP ='C'

						WHERE PR.NPR_SUMASSURED > 0
							  
							");


            if (!IsAdmin)
            {
                sb_query.Append("  and P1.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    BranchCode = "0" + BranchCode;
                    sb_query.Append("  and P1.PBB_BRANCHCODE='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and P1.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                }
            }

            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }

            sb_query.Append("           ORDER BY p1.use_userid, p1.np1_proposal, p1.np1_propdate");

            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds.Tables["BANCRPT"];

            DataRow[] foundRows;

            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            else
            {
                foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }

        public static DataTable GetExcelReportForBanca(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);

            string sqlString = "SELECT (select bi.polc_np1_issuedate\n" +
            "          from ilas_banca_interface bi\n" +
            "         where bi.polc_np1_proposal = P1.NP1_PROPOSAL) ISSUEDDATE,\n" +
            "       P1.NP1_PROPDATE,\n" +
            "       P1.USE_USERID,\n" +
            "       to_Char(p1.np1_propdate, 'rrrr') policyyear,\n" +
            "       (SELECT USE_NAME FROM USE_USERMASTER WHERE USE_USERID = P1.USE_USERID) USERNAME,\n" +
            "       ccs.ccs_field1 BranchCode,\n" +
            "       ccs.ccs_descr BranchName,p1.pbb_branchcode acc_branch,\n" +
            "       TO_CHAR(p1.np1_propdate, 'DD-Mon-YYYY') ProposalDate,\n" +
            "       substr(p1.np1_comments, instr(p1.np1_comments, ':', 1) + 1, 12) PropSignDate,\n" +
            "       substr(p1.np1_comments, instr(p1.np1_comments, ':', 1, 2) + 1) PaymentRef,\n" +
            "       p1.np1_proposalref ProposalRef,\n" +
            "       case\n" +
            "         when LENGTH(NVL(np1_policyno, '')) > 2 then\n" +
            "          TO_CHAR(p2.np2_commendate, 'DD-Mon-YYYY')\n" +
            "       end CommencentDate,\n" +
            "       p1.np1_proposal as Proposal_Number,\n" +
            "       TO_CHAR(' ' || p1.np1_policyno) as Policy_Number,\n" +
            "       PH.NPH_CODE,\n" +
            "       SUBSTR(NPH_IDNO, 1, 5) || '-' || SUBSTR(NPH_IDNO, 6, 7) || '-' ||\n" +
            "       SUBSTR(NPH_IDNO, 13) CNIC,\n" +
            "       ph.nph_fullname ClientName,\n" +
            "       ph.NPH_BIRTHDATE ClientDOB,\n" +
            "       P2.NP2_AGEPREM ClientAge,\n" +
            "       (select c.ccd_logo\n" +
            "          from ccd_channeldetail c\n" +
            "         where c.ccd_code = p1.np1_channeldetail\n" +
            "           and c.cch_code = p1.np1_channel) BANKNAME,\n" +
            "       case\n" +
            "         when not exists\n" +
            "          (select csd_value\n" +
            "                 from lcsd_systemdtl\n" +
            "                where csh_id = 'PRDSC'\n" +
            "                  and csd_type = p1.np1_channel || p1.np1_channeldetail || '-' ||\n" +
            "                      LR.ppr_prodcd) then\n" +
            "          LR.ppr_descr\n" +
            "         else\n" +
            "          (select csd_value\n" +
            "             from lcsd_systemdtl\n" +
            "            where csh_id = 'PRDSC'\n" +
            "              and csd_type = p1.np1_channel || p1.np1_channeldetail || '-' ||\n" +
            "                  LR.ppr_prodcd)\n" +
            "       end as PlanDesc,\n" +
            "       case\n" +
            "         when pr.cmo_mode = 'A' then\n" +
            "          'Annually'\n" +
            "         when pr.cmo_mode = 'H' then\n" +
            "          'Half Yearly'\n" +
            "         when pr.cmo_mode = 'Q' then\n" +
            "          'Quarterly'\n" +
            "         when pr.cmo_mode = 'M' then\n" +
            "          'Monthly'\n" +
            "       end PaymentMode,\n" +
            "       getcsvriders(p1.np1_proposal) \"Riders Detail\",\n" +
            "       nvl(pr.npr_premium, 0) + nvl(pr.npr_loading, 0) \"Basic Premium\",\n" +
            "       (Select sum((nvl(rnpr.npr_premium, 0) + nvl(rnpr.npr_loading, 0)))\n" +
            "          From lnpr_product rnpr\n" +
            "         Where rnpr.np1_proposal = pr.np1_proposal\n" +
            "           and rnpr.np2_setno = pr.np2_setno\n" +
            "           and nvl(rnpr.npr_basicflag, 'N') = 'N') \"Rider Premium\",\n" +
            "       (SELECT SUM(lnpr.npr_premium) + p2.np2_totload\n" +
            "          FROM LNPR_PRODUCT LNPR\n" +
            "         WHERE lnpr.np1_proposal = p2.np1_proposal\n" +
            "           and lnpr.np2_setno = P2.NP2_SETNO) \"Modal Premium\",\n" +
            "       pr.Npr_Sumassured SumAssured,\n" +
            "       pr.npr_benefitterm Term,\n" +
            "       case\n" +
            "         when LENGTH(NVL(np1_policyno, '')) > 2 then\n" +
            "          'Policy Issued'\n" +
            "         when nvl(p2.np2_substandar, 'N') = 'Y' then\n" +
            "          'Referred'\n" +
            "         when nvl(p1.np1_selected, 'D') = 'R' then\n" +
            "          'Compliance OK'\n" +
            "         when nvl(p1.np1_selected, 'D') = 'F' then\n" +
            "          'Payment Waiting'\n" +
            "         when nvl(p1.np1_selected, 'D') = 'Y' then\n" +
            "          'Payment Deducted'\n" +
            "         else\n" +
            "          'Pending'\n" +
            "       end policy_status,\n" +
            "       (select replace(replace(l.cm_comments, '\"', ''), '''', '')\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal)) CurrentStatus,\n" +
            "       (select l.cm_commentby\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'CBC%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'CBC%')) CBCUser,\n" +
            "       (select case\n" +
            "                 when l.cm_action = 'H' then\n" +
            "                  'Hold at Approval Screen'\n" +
            "                 when l.cm_action = 'K' then\n" +
            "                  'Hold at Referred Screen'\n" +
            "                 when l.cm_action = 'D' then\n" +
            "                  'Declined by CBC'\n" +
            "                 when l.cm_action = 'C' then\n" +
            "                  'Approved by CBC'\n" +
            "                 when l.cm_action = 'P' then\n" +
            "                  'Returned by CBC'\n" +
            "                 else\n" +
            "                  l.cm_action\n" +
            "               end cm_action\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'CBC%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'CBC%')) CBCDecision,\n" +
            "       (select l.cm_commentdate\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'CBC%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'CBC%')) CBCDate,\n" +
            "       (select case\n" +
            "                 when l.cm_action = 'Ok' then\n" +
            "                  'Approved by RBH'\n" +
            "                 when l.cm_action = 'Not Ok' then\n" +
            "                  'Declined by RBH'\n" +
            "                 else\n" +
            "                  l.cm_action\n" +
            "               end cm_action\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'RBH%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'RBH%')) RBHDecision,\n" +
            "       (select l.cm_commentdate\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'RBH%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'RBH%')) RBHDate,\n" +
            "       (select l.cm_commentdate\n" +
            "          from lncm_comments l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'BSO%'\n" +
            "           and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(CM_SERIAL_NO), 0)\n" +
            "                  FROM LNCM_COMMENTS\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'BSO%')) ProposalPostingDate,\n" +
            "       (select l.cm_commentdate\n" +
            "          from LNCM_COMMENTS l\n" +
            "         where l.np1_proposal = p1.np1_proposal\n" +
            "           and cm_commentby like 'BM%'\n" +
            "          and l.cm_serial_no =\n" +
            "               (SELECT NVL(MAX(cm_serial_no), 0)\n" +
            "                  FROM lncm_comments\n" +
            "                 WHERE NP1_PROPOSAL = p1.np1_proposal\n" +
            "                   and cm_commentby like 'BM%')) BMSupervisionDate,\n" +  //dataReportbynoman14092023
            "       (select a.CSD_VALUE\n" +
            "          from lcsd_systemdtl a\n" +
            "        where a.csh_id = 'AGENT'\n" +
            "           and A.CSD_TYPE = 'COMM_RATE') CommissionPer,\n" +

            "       coalesce(p1.np1_accountno,p1.np1_iban) ACCOUNTNO,\n" +
      //      "       p1.NP1_ACCOUNTNO ACCOUNTNO,\n" +
            "       p1.pbr_reference StaffId,\n" +
            "       p1.np1_purpose StaffId2,\n" +      /*--chg-25082023--*/
            "       sch.staff_name StaffName,\n" +
            "       la.nad_mobile,\n" +
            "       translate(la.nad_address1 || ' ' || la.nad_address2 || ' ' || la.nad_address3,'\"''',' ') address,lb.COLLECTION_AMOUNT,lb.COLLECTION_DATE\n" +
            "  FROM LNP1_POLICYMASTR P1\n" +
            " INNER JOIN LNU1_UNDERWRITI u1\n" +
            "    on p1.np1_proposal = u1.np1_proposal\n" +
            "  left outer join lsch_staffchannelmapping sch\n" +
            "    on sch.staff_id = p1.pbr_reference\n" +
            " INNER join LNPH_PHOLDER ph\n" +
            "    on PH.NPH_CODE = U1.NPH_CODE\n" +
            "   AND PH.NPH_LIFE = U1.NPH_LIFE\n" +
            "   AND U1.NU1_LIFE = 'F'\n" +
            " INNER join LNPR_PRODUCT PR\n" +
            "    on pr.np1_proposal = p1.np1_proposal\n" +
            "   and pr.npr_basicflag = 'Y'\n" +
            " INNER join CCS_CHANLSUBDETL ccs\n" +
            "    on ccs.cch_code || ccs.ccd_code || ccs.ccs_code = p1.pcl_locatcode\n" +
            " inner join LPPR_PRODUCT LR\n" +
            "    ON LR.PPR_PRODCD = PR.PPR_PRODCD\n" +
            " INNER JOIN LNP2_POLICYMASTR P2\n" +
            "    ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL\n" +
            " left outer join lnad_address la\n" +
            "    on la.nph_code=u1.nph_code\n" +
            "	and la.Nad_Addresstyp='C'\n" +
            " left outer join LBCC_Collection lb\n" +
            "    on lb.np1_proposal = p1.np1_proposal\n" +
            " WHERE P1.USE_USERID LIKE (CASE\n" +
            "         WHEN (SELECT USE_TYPE\n" +
            "                 FROM USE_USERMASTER UM\n" +
            "                WHERE upper(UM.USE_USERID) = upper('ADMIN')) IN ('A', 'C') THEN\n" +
            "          '%'\n" +
            "         ELSE\n" +
            "          '{?P_UserId}'\n" +
            "       END)\n" +
            "   AND PR.NPR_SUMASSURED > 0\n";
            if (!IsAdmin)
            {
                sqlString += "  and p1.np1_channeldetail='" + BankCode + "'";
                if (!BYPASSBRANCHFILTER)
                {
                    sqlString += "  and p1.np1_channelsdetail='" + BranchCode + "'";
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sqlString += "  and p1.np1_channeldetail='" + BankCode + "'";
                }
            }
            if (DateType == "IssueDate")
            {
                sqlString += "   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy') ";
            }
            else
            {
                sqlString += "   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')  ";
            }
            sqlString +=
                        " ORDER BY p1.use_userid,\n" +
            "          p1.np1_channel,\n" +
            "          p1.np1_channeldetail,\n" +
            "          p1.np1_channelsdetail,\n" +
            "          p1.np1_proposal,\n" +
            "          p1.np1_propdate";
            //   #region OldQuery
            //   sb_query.Append("SELECT");
            //   sb_query.Append(@"         (select bi.polc_np1_issuedate from ilas_banca_interface bi 
            //	where bi.polc_np1_proposal=P1.NP1_PROPOSAL) ISSUEDDATE, ");
            //   sb_query.Append("           P1.NP1_PROPDATE,P1.USE_USERID,(SELECT USE_NAME FROM USE_USERMASTER WHERE USE_USERID=P1.USE_USERID) USERNAME,");
            //   sb_query.Append("           ccs.ccs_field1 BranchCode,");
            //   sb_query.Append("           ccs.ccs_descr BranchName,");
            //   sb_query.Append("           p1.pbb_branchcode acc_branch,");
            //   sb_query.Append("           to_Char(p1.np1_propdate,'rrrr') policyyear,");
            //   sb_query.Append("           TO_CHAR(p1.np1_propdate ,'DD-Mon-YYYY') ProposalDate,");

            //   sb_query.Append("           substr(p1.np1_comments,instr(p1.np1_comments,':',1)+1,12 ) PropSignDate,");
            //   sb_query.Append("           substr(p1.np1_comments,instr(p1.np1_comments,':',1,2)+1 ) PaymentRef,");
            //   sb_query.Append("           p1.np1_proposalref ProposalRef,");

            //   sb_query.Append("           case when LENGTH(NVL(np1_policyno,''))>2 then TO_CHAR(p2.np2_commendate ,'DD-Mon-YYYY')  end CommencentDate,");
            //   sb_query.Append("           p1.np1_proposal as Proposal_Number,");
            //   sb_query.Append("           TO_CHAR(' '||p1.np1_policyno) as Policy_Number,");
            //   sb_query.Append("           PH.NPH_CODE,");
            //   //----- ADD CNIC-----//
            //   sb_query.Append("			SUBSTR(NPH_IDNO, 1, 5) || '-' || SUBSTR(NPH_IDNO, 6, 7) || '-' || SUBSTR(NPH_IDNO, 13) CNIC ,");
            //   //----- ADD CNIC-----//

            //   sb_query.Append("           ph.nph_fullname ClientName , ph.NPH_BIRTHDATE ClientDOB , P2.NP2_AGEPREM ClientAge,");
            //   sb_query.Append("           (select c.ccd_logo from ccd_channeldetail c where c.ccd_code=p1.np1_channeldetail and c.cch_code=p1.np1_channel) BANKNAME,");

            //   //sb_query.Append("           LR.PPR_DESCR PlanDesc,");
            //   sb_query.Append(@"           case when not exists   
            //	(select csd_value from lcsd_systemdtl where csh_id='PRDSC' and csd_type=p1.np1_channel || p1.np1_channeldetail || '-' || LR.ppr_prodcd)
            //	then LR.ppr_descr 
            //	else
            //	(select csd_value from lcsd_systemdtl where csh_id='PRDSC' and csd_type=p1.np1_channel || p1.np1_channeldetail || '-' || LR.ppr_prodcd)
            //	end as PlanDesc , ");

            //   sb_query.Append("           case");
            //   sb_query.Append("           when pr.cmo_mode = 'A' then 'Annually'");
            //   sb_query.Append("           when pr.cmo_mode = 'H' then 'Half Yearly'");
            //   sb_query.Append("           when pr.cmo_mode = 'Q' then 'Quarterly'");
            //   sb_query.Append("           when pr.cmo_mode = 'M' then 'Monthly'");
            //   sb_query.Append("           end PaymentMode,");

            //   sb_query.Append(@"           getcsvriders(p1.np1_proposal) ""Riders Detail"",
            //       nvl(pr.npr_premium, 0) + nvl(pr.npr_loading, 0) ""Basic Premium"",
            //		(Select sum((nvl(rnpr.npr_premium, 0) + nvl(rnpr.npr_loading, 0))) From lnpr_product rnpr 
            //		Where rnpr.np1_proposal = pr.np1_proposal and rnpr.np2_setno = pr.np2_setno  
            //		and nvl(rnpr.npr_basicflag, 'N') = 'N') ""Rider Premium"",
            //		(SELECT SUM(lnpr.npr_premium)+p2.np2_totload FROM LNPR_PRODUCT LNPR
            //		WHERE lnpr.np1_proposal = p2.np1_proposal and
            //		lnpr.np2_setno  =  P2.NP2_SETNO) ""Modal Premium"", ");
            //   //sb_query.Append("           nvl(pr.npr_premium,0)+ nvl(pr.npr_loading,0) Premium,");

            //   sb_query.Append("           pr.Npr_Sumassured SumAssured,");
            //   sb_query.Append("           pr.npr_benefitterm Term,");
            //   sb_query.Append("           case");
            //   sb_query.Append("           when LENGTH(NVL(np1_policyno,'')) >2 then 'Policy Issued'");
            //   sb_query.Append("           when nvl(p2.np2_substandar,'N')='Y' then 'Referred'");
            //   sb_query.Append("           when nvl(p1.np1_selected,'D')='R' then 'Compliance OK'");
            //   sb_query.Append("           when nvl(p1.np1_selected,'D')='F' then 'Payment Waiting'");
            //   sb_query.Append("           when nvl(p1.np1_selected,'D')='Y' then 'Payment Deducted'");
            //   sb_query.Append("           else 'Pending' end policy_status,");
            //   sb_query.Append("           (select replace(replace(l.cm_comments,'\"',''),'''','') from lncm_comments l where l.np1_proposal=p1.np1_proposal and l.cm_serial_no = (SELECT NVL(MAX(CM_SERIAL_NO),0) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = p1.np1_proposal)) CurrentStatus, ");
            //   sb_query.Append(" 		(select l.cm_commentby");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'CBC%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0)");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'CBC%')) CBCUser,");
            //   sb_query.Append("        (select case when l.cm_action='H' then 'Hold at Approval Screen'");
            //   sb_query.Append("                     when l.cm_action='K' then 'Hold at Referred Screen'");
            //   sb_query.Append("                     when l.cm_action='D' then 'Declined by CBC'");
            //   sb_query.Append("                     when l.cm_action='C' then 'Approved by CBC'");
            //   sb_query.Append("                     when l.cm_action='P' then 'Returned by CBC'");
            //   sb_query.Append("                       else l.cm_action end cm_action");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'CBC%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0)");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'CBC%')) CBCDecision,");
            //   sb_query.Append("        (select to_Char(l.cm_commentdate, 'DD-Mon-YYYY')");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'CBC%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0)");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'CBC%')) CBCDate,");
            //   sb_query.Append("        (select case when l.cm_action='Ok' then 'Approved by RBH'");
            //   sb_query.Append("                     when l.cm_action='Not Ok' then 'Declined by RBH'");
            //   sb_query.Append("                       else l.cm_action end cm_action");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'RBH%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0)");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'RBH%')) RBHDecision,");
            //   sb_query.Append("        (select to_Char(l.cm_commentdate, 'DD-Mon-YYYY')");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'RBH%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'RBH%')) RBHDate,");
            //   sb_query.Append("         (select to_Char(l.cm_commentdate, 'DD-Mon-YYYY')");
            //   sb_query.Append("           from lncm_comments l");
            //   sb_query.Append("          where l.np1_proposal =  p1.np1_proposal");
            //   sb_query.Append("            and cm_commentby like 'BSO%'");
            //   sb_query.Append("            and l.cm_serial_no =");
            //   sb_query.Append("                (SELECT NVL(MAX(CM_SERIAL_NO), 0)");
            //   sb_query.Append("                   FROM LNCM_COMMENTS");
            //   sb_query.Append("                  WHERE NP1_PROPOSAL =  p1.np1_proposal");
            //   sb_query.Append("                  and cm_commentby like 'BSO%')) ProposalPostingDate,");


            //   //			sb_query.Append(@"         CASE WHEN NP1_SELECTED = 'Y' and (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B
            //   //										WHERE B.NP1_PROPOSAL = p1.NP1_PROPOSAL) = 'Y' THEN 'Referred'
            //   //										WHEN NP1_SELECTED = 'Y' THEN 'Posted'
            //   //										WHEN CDC_CODE = '001' THEN 'Approved'
            //   //										WHEN CDC_CODE = '002' THEN 'Referred'
            //   //										WHEN CDC_CODE = '003' THEN 'Referred'
            //   //										WHEN (SELECT NP2_SUBSTANDAR FROM LNP2_POLICYMASTR B
            //   //												WHERE B.NP1_PROPOSAL = p1.NP1_PROPOSAL) = 'Y' THEN 'Referred'
            //   //										ELSE 'Pending' end policy_status, ");

            ////   sb_query.Append("           p1.NP1_ACCOUNTNO ACCOUNTNO,"); original

            //   sb_query.Append("           coalesce(p1.np1_accountno,p1.np1_iban) ACCOUNTNO,");
            //   sb_query.Append("			p1.pbr_reference StaffId,");
            //   sb_query.Append("			p1.np1_purpose StaffId2,");       /*--chg-25082023--*/
            //   sb_query.Append("			sch.staff_name StaffName,");
            //   sb_query.Append("			la.nad_mobile,");
            //   sb_query.Append(@"			translate(la.nad_address1||' '||la.nad_address2||' '||la.nad_address3,'""''',' ') address");
            //   sb_query.Append("           FROM");
            //   sb_query.Append("           LNP1_POLICYMASTR P1");
            //   sb_query.Append("           INNER JOIN");
            //   sb_query.Append("           LNU1_UNDERWRITI u1 on");
            //   sb_query.Append("           p1.np1_proposal=u1.np1_proposal");
            //   sb_query.Append("			left outer join lsch_staffchannelmapping sch");
            //   sb_query.Append("			on sch.staff_id = p1.pbr_reference");
            //   sb_query.Append("           INNER join");
            //   sb_query.Append("           LNPH_PHOLDER ph on");
            //   sb_query.Append("           PH.NPH_CODE=U1.NPH_CODE");
            //   sb_query.Append("           AND PH.NPH_LIFE = U1.NPH_LIFE");
            //   sb_query.Append("           AND U1.NU1_LIFE = 'F'");
            //   sb_query.Append("           INNER join");
            //   sb_query.Append("           LNPR_PRODUCT PR on");
            //   sb_query.Append("           pr.np1_proposal=p1.np1_proposal");

            //   sb_query.Append("           and pr.npr_basicflag='Y'");
            //   //sb_query.Append("           and nvl(pr.npr_basicflag,'N')||nvl(pr.npr_selected,'N') in ('YN','NY')");

            //   //			sb_query.Append("           INNER join luch_userchannel uch on uch.cch_code = p1.np1_channel");
            //   //			sb_query.Append("           and uch.ccd_code = p1.np1_channeldetail");
            //   //			sb_query.Append("           and uch.ccs_code = p1.np1_channelsdetail");

            //   //			if(!ISMULTIBRANCH)
            //   //			{
            //   //				sb_query.Append("       and uch.use_userid = P1.Use_Userid");
            //   //			}
            //   //			else
            //   //			{
            //   //			    sb_query.Append("       and uch.use_userid IN (SELECT UC.USE_USERID FROM LUCH_USERCHANNEL UC");
            //   //				sb_query.Append("       WHERE UC.cch_code  = p1.np1_channel");
            //   //				sb_query.Append("       and uc.ccd_code = p1.np1_channeldetail");
            //   //				sb_query.Append("       and uc.ccs_code =p1.np1_channelsdetail AND UC.USE_USERID LIKE 'BSO%' )");
            //   //			}
            //   sb_query.Append("              INNER join CCS_CHANLSUBDETL ccs on ccs.cch_code || ccs.ccd_code || ccs.ccs_code =p1.pcl_locatcode");
            //   //			sb_query.Append("           INNER join CCS_CHANLSUBDETL ccs on ccs.cch_code = uch.cch_code");
            //   //			sb_query.Append("           and ccs.ccd_code = uch.ccd_code");
            //   //			sb_query.Append("           and ccs.ccs_code = uch.ccs_code");
            //   sb_query.Append("           inner join");
            //   sb_query.Append("           LPPR_PRODUCT LR ON");
            //   sb_query.Append("           LR.PPR_PRODCD=PR.PPR_PRODCD");
            //   sb_query.Append("           INNER JOIN LNP2_POLICYMASTR P2");
            //   sb_query.Append("           ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL");
            //   sb_query.Append("           Inner join lnad_address la");
            //   sb_query.Append("           on la.np1_proposal=p1.np1_proposal");
            //   sb_query.Append("           WHERE");
            //   sb_query.Append("           P1.USE_USERID LIKE");
            //   sb_query.Append("           (");
            //   sb_query.Append("           CASE WHEN (SELECT USE_TYPE FROM USE_USERMASTER UM WHERE upper(UM.USE_USERID)=upper('ADMIN')) IN ('A','C')");
            //   sb_query.Append("           THEN '%'");
            //   sb_query.Append("           ELSE '{?P_UserId}'");
            //   sb_query.Append("           END");
            //   sb_query.Append("           )");
            //   sb_query.Append("           AND PR.NPR_SUMASSURED > 0");

            //   if (!IsAdmin)
            //   {
            //       sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
            //       if (!BYPASSBRANCHFILTER)
            //       {
            //           sb_query.Append("  and p1.np1_channelsdetail='" + BranchCode + "'");
            //       }
            //   }
            //   else
            //   {
            //       //Admin
            //       if (BankCode != "ALL")
            //       {
            //           sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
            //       }
            //   }
            //   if (DateType == "IssueDate")
            //   {
            //       sb_query.Append("   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            //   }
            //   else
            //   {
            //       sb_query.Append("   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            //   }
            //   sb_query.Append("           ORDER BY p1.use_userid, p1.np1_channel, p1.np1_channeldetail, p1.np1_channelsdetail, p1.np1_proposal, p1.np1_propdate");
            //   #endregion OldQuery

            string query = sqlString.ToString();
            query = EnvHelper.Parse(sqlString);

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BANCRPT");
            cmd.Dispose();
            con.Close();

            DataTable dtTable = ds.Tables["BANCRPT"];

            DataRow[] foundRows;

            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["BANCRPT"].Select(" ISSUEDDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            else
            {
                foundRows = ds.Tables["BANCRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;


        }

        public static DataTable GetExcelReportForIlas(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string Status, string DateType)
        {
            string BANKCODEFORILAS = LNP1_POLICYMASTRDB.getBANKFORILAS(BankCode);
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);


            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append(@"select vw.NP1_PROPDATE,   

			 vw.np1_proposal ""Proposal"", vw.np1_policyno ""Policy""
 
								,vw.nph_code ""ClientCode"",translate(vw.nph_fullname,'""''',' ') ""Customer Name"", vw.NPH_BIRTHDATE ""Customer DOB"" , vw.NP2_AGEPREM ""Customer Age"",
 
								SUBSTR(NPH_IDNO, 1, 5) || '-' || SUBSTR(NPH_IDNO, 6, 7) || '-' ||SUBSTR(NPH_IDNO, 13) CNIC
 
									
 
                         ,coalesce(IB.GCA_IBAN,vw.NP1_ACCOUNTNO) ""Account No"",ND.NAD_TELNO1 ""Office No"",ND.NAD_TELNO2 ""Residence No""
 
										,translate(ND.NAD_ADDRESS1 || NVL(ND.NAD_ADDRESS2, '') ||NVL(ND.NAD_ADDRESS3, ''),'""''',' ') ADDRESSC,
 
 
(select ct.cct_descr from lcct_city ct
where ct.ccn_ctrycd=nd.ccn_ctrycd
and ct.cct_citycd=nd.cct_citycd
and nd.nad_addresstyp='C') CITYCODE,
 
(select pr.cpr_descr from LCPR_PROVINCE pr
where pr.ccn_ctrycd = ND.CCN_CTRYCD
AND PR.CPR_PROVCD=nd.cpr_provcd
and nd.nad_addresstyp='C') PROVCODE,
 
  translate(NDP.NAD_ADDRESS1 || NVL(NDP.NAD_ADDRESS2, '') ||NVL(NDP.NAD_ADDRESS3, ''),'""''',' ') ADDRESSP,
  translate(NDB.NAD_ADDRESS1 || NVL(NDB.NAD_ADDRESS2, '') ||NVL(NDB.NAD_ADDRESS3, ''),'""''',' ') ADDRESSB,
 
										ND.NAD_MOBILE ""Mobile No""
									,GetCSVBeneficiaries(vw.np1_proposal) ""Beneficiary"",GetCSVRelations(vw.np1_proposal) ""Relation to Customer""
										,GetCSVIDNumber(vw.np1_proposal) ""CNIC/B Form No of beneficiary"", st.cst_descr ""Status"",");
            //sb_query.Append(@"       ""Status"",");
            sb_query.Append("        to_char(vw.np1_issuedate, 'DD-MON-YYYY') AS \"IssueDate\", TO_CHAR(vw.np2_commendate, 'DD-MON-YYYY') AS \"ComncDate\", TO_CHAR(vw.np2_nextduedat, 'DD-MON-YYYY') AS \"NextDueDate\", vw.np1_proposalref PROPOSALREF,vw.np1_quotationref PAYMENTREF, ");
            //********
            //    **********
            sb_query.Append("        mo.cmo_description AS \"PayFreq\",");

            ////==== BRANCH CODE ADDED
            sb_query.Append(@"br.pbb_branchcode ""BranchCode"",");
            ///===== BRANCH CODE ADDED

            sb_query.Append(@"        translate(br.pbb_branchname,'""''',' ') ""ColctBranch"", vw.pbk_bankcode ""Bank Name"", cm.ncm_value");
            sb_query.Append(@"       ""1stYearCmsn"",");

            //sb_query.Append(@"       ppr.ppr_descr Prod, ");
            sb_query.Append(@"         nvl(get_syspara.get_value('PRDSC',vw.aag_imedsupr||'-'||vw.ppr_prodcd),ppr.ppr_descr) ""Prod"", ");

            sb_query.Append(@"       getcsvriders(vw.np1_proposal) ""Riders Detail"", npr.npr_benefitterm ""BnftTerm"", npr.npr_premiumter ""PayTerm"",");
            sb_query.Append("        npr.npr_sumassured AS \"Prod_SmAsurd\",");


            //sb_query.Append(@"        (nvl(npr.npr_premium,0)+nvl(npr.npr_loading,0)) ProdPrem");
            sb_query.Append(@"        (nvl(npr.npr_premium,0)+nvl(npr.npr_loading,0)) ""Basic Premium"",");

            sb_query.Append(@"        (Select sum((nvl(rnpr.npr_premium,0)+nvl(rnpr.npr_loading,0)))
										From lnpr_product rnpr
										Where rnpr.np1_proposal = npr.np1_proposal
											and rnpr.np2_setno = npr.np2_setno
											and nvl(rnpr.npr_basicflag,'N') = 'N') ""Rider Premium"", ");
            sb_query.Append(@"        vw.gross_prem ""Modal Premium"", ");

            ////============== CREDIT BALANCE ADDED 01-02-2018
            sb_query.Append(@"        VW.np1_creditbalance ""CREDIT BALANCE"", ");
            ////============== CREDIT BALANCE ADDED 01-02-2018
            ///
            ////============== CREDIT BALANCE ADDED 01-02-2018
            ///
            sb_query.Append(@"        VW.aag_agcode ""AGENT CODE"", ");
            sb_query.Append(@"         VW.aag_name ""AGENT NAME"" ");

            sb_query.Append("        from vw_policymastr vw, lrdo_debitorder do, lcmo_mode mo,");
            sb_query.Append("        lcst_ppstatus st, pbb_bankbranch br, lncm_commission cm,");
            sb_query.Append("        lnpr_product npr, lppr_product ppr,LNAD_ADDRESS ND,  LNAD_ADDRESS NDP,  LNAD_ADDRESS NDB,LGCA_MAINACNT IB");
            sb_query.Append("        where vw.np1_proposal = do.np1_proposal");

            sb_query.Append("        and vw.np1_proposal = IB.GCA_ACCOUNT");

            sb_query.Append("        and IB.GSC_TYPE='1010' ");
            sb_query.Append("        and IB.PCM_COMPCODE='01' ");
            sb_query.Append("        and IB.PCL_LOCATCODE='180' ");
            sb_query.Append("        and IB.PPF_PRFCENTCODE='0100' ");

            sb_query.Append("        and vw.cmo_mode = mo.cmo_mode");
            sb_query.Append("        and vw.cst_statuscd=st.cst_statuscd");
            sb_query.Append("        and do.pbk_bankcode = br.pbk_bankcode");
            sb_query.Append("        and do.pbb_branchcode=br.pbb_branchcode");
            sb_query.Append("        and vw.np1_proposal = cm.np1_proposal");
            sb_query.Append("        and vw.np2_setno = cm.np2_setno");
            sb_query.Append("        and cm.cyr_yearcode='01S'");
            sb_query.Append("        and npr.ppr_prodcd = ppr.ppr_prodcd");
            sb_query.Append("        and vw.np1_proposal= npr.np1_proposal");
            sb_query.Append("        and vw.np2_setno   = npr.np2_setno and npr.npr_basicflag='Y' and vw.nph_code = ND.nph_code AND ND.NAD_ADDRESSTYP = 'C' and ND.nph_life = 'D' ");
            sb_query.Append("     and vw.nph_code = NDP.nph_code  AND NDP.NAD_ADDRESSTYP in ('P') and NDP.nph_life = 'D'   ");
            sb_query.Append("        and vw.nph_code = NDB.nph_code AND NDB.NAD_ADDRESSTYP in ('B') and NDB.nph_life = 'D' ");

            if (!IsAdmin)
            {
                sb_query.Append("  and BR.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    BranchCode = "0" + BranchCode;
                    sb_query.Append("  and BR.PBB_BRANCHCODE='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and BR.PBK_BANKCODE='" + BANKCODEFORILAS + "'");
                }
            }

            if (Status == "I")
            {
                sb_query.Append("       and vw.np1_policyno is not null");
            }
            else if (Status == "P")
            {
                sb_query.Append("     and vw.np1_policyno is null");
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(vw.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(vw.NP1_PROPDATE) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");

            }

            sb_query.Append("       order by vw.np1_proposal,npr.npr_basicflag desc, npr.ppr_prodcd");

            string query = sb_query.ToString();
            query = EnvHelper.Parse(query);


            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSNILAS"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ILASRPT");
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds.Tables["ILASRPT"];


            DataRow[] foundRows;
            if (DateType == "IssueDate")
            {

                foundRows = ds.Tables["ILASRPT"].Select(" IssueDate >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND IssueDate <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
              //  foundRows = ds.Tables["ILASRPT"].Select(" np1_issuedate >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND np1_issuedate <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");

            }
            else
            {
                foundRows = ds.Tables["ILASRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }

            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;
        }

        public static DataTable GetExcelReportForUbl(string FromDate, string ToDate, string BankCode, string BranchCode, bool IsAdmin, string DateType)
        {
            //GBA LIVE --S*L*I*L*A*S*P*R*D
            //GBA TEST --S*L*I*L*A*S*U*A*T
            //LOCAL    --S*L*I*L*A*S*P*R*D

            // Define DBUser in web config line no 116   <add key="DBUSER" value="SLILASPRD"/>

            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            bool BYPASSBRANCHFILTER = ace.Ace_General.IsBranchByPass(BankCode);
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(BankCode);

            string DBUSER = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBUSER"]);

            sb_query.Append("SELECT");
            sb_query.Append(@"         (select bi.polc_np1_issuedate from ilas_banca_interface bi 
										where bi.polc_np1_proposal=P1.NP1_PROPOSAL) ISSUEDDATE, ");
            sb_query.Append(@"         P1.NP1_PROPDATE,'' SNO,");

            sb_query.Append("           substr(p1.np1_comments,instr(p1.np1_comments,':',1)+1,12 ) PropSignDate,");
            sb_query.Append("           substr(p1.np1_comments,instr(p1.np1_comments,':',1,2)+1 ) PaymentRef,");
            sb_query.Append("           p1.np1_proposalref ProposalRef,");

            sb_query.Append(@"          P1.NP1_PROPOSAL ""Proposal No"",");
            sb_query.Append(@"          (select " + DBUSER + ".lnp1_policymastr.np1_policyno");
            sb_query.Append(@"          from " + DBUSER + ".lnp1_policymastr");
            sb_query.Append(@"          where " + DBUSER + ".lnp1_policymastr.np1_proposal = P1.NP1_PROPOSAL)");
            sb_query.Append(@"          Policy_Number,"); //
            sb_query.Append(@"          TO_CHAR(p1.np1_propdate , 'DD-Mon-YYYY') Proposal_Entry_Date,");
            sb_query.Append(@"          'STATE LIFE' ""Company Name"",");
            sb_query.Append(@"          (select TO_CHAR(CM_COMMENTDATE,'DD-Mon-YYYY')  from LNCM_COMMENTS WHERE CM_ACTION = 'PR' AND NP1_PROPOSAL = P1.NP1_PROPOSAL AND CM_SERIAL_NO=(SELECT MAX(CM_SERIAL_NO) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL=P1.NP1_PROPOSAL)) ""Premium Deduction Date"",");
            sb_query.Append(@"          (SELECT TO_CHAR(NP.NP1_ISSUEDATE,'DD-Mon-YYYY') FROM " + DBUSER + ".LNP1_POLICYMASTR NP WHERE NP1_PROPOSAL=P1.NP1_PROPOSAL) ");
            sb_query.Append(@"           ""Issuance Date"",");
            sb_query.Append(@"           ph.nph_fullname ""Customer Name"",");

            sb_query.Append("           ph.NPH_BIRTHDATE CustomerDOB , P2.NP2_AGEPREM CustomerAge,");
            sb_query.Append("           (select c.ccd_logo from ccd_channeldetail c where c.ccd_code=p1.np1_channeldetail and c.cch_code=p1.np1_channel) BankName,");

            sb_query.Append("            SUBSTR(NPH_IDNO,1,5)||'-'||SUBSTR(NPH_IDNO,6,7)||'-'||SUBSTR(NPH_IDNO,13) CNIC,");
            sb_query.Append(@"           p1.NP1_ACCOUNTNO ""UBL Account No"",");
            sb_query.Append(@"           ND.NAD_TELNO1 ""Office No"",");
            sb_query.Append(@"           ND.NAD_TELNO2 ""Residence No"",");
            sb_query.Append(@"            translate(ND.NAD_ADDRESS1 || NVL(ND.NAD_ADDRESS2,'') ||NVL(ND.NAD_ADDRESS3,''),'""''',' ') ADDRESS,");
            sb_query.Append(@"           ND.NAD_MOBILE ""Mobile No"",");

            //sb_query.Append("            LR.PPR_DESCR Plan,");
            sb_query.Append(@"           case when not exists   
										(select csd_value from lcsd_systemdtl where csh_id='PRDSC' and csd_type=p1.np1_channel || p1.np1_channeldetail || '-' || LR.ppr_prodcd)
										then LR.ppr_descr 
										else
										(select csd_value from lcsd_systemdtl where csh_id='PRDSC' and csd_type=p1.np1_channel || p1.np1_channeldetail || '-' || LR.ppr_prodcd)
										end as Plan , ");

            //Farrukh as below using function 
            sb_query.Append(@"           getcsvriders(p1.np1_proposal) ""Riders Detail"",");
            sb_query.Append("            case");
            sb_query.Append("            when pr.cmo_mode = 'A' then 'Annually'");
            sb_query.Append("            when pr.cmo_mode = 'H' then 'Half Yearly'");
            sb_query.Append("            when pr.cmo_mode = 'Q' then 'Quarterly'");
            sb_query.Append("            when pr.cmo_mode = 'M' then 'Monthly'");
            sb_query.Append("            end PayMode,");
            sb_query.Append("            pr.npr_benefitterm Term ,");

            //sb_query.Append(@"           nvl ( pr.npr_premium,0)+ nvl(pr.npr_loading , 0 ) ""Modal Premium"",");
            sb_query.Append(@"          nvl(pr.npr_premium, 0) + nvl(pr.npr_loading, 0) ""Modal Premium"",
											(Select sum((nvl(rnpr.npr_premium, 0) + nvl(rnpr.npr_loading, 0))) From lnpr_product rnpr 
											Where rnpr.np1_proposal = pr.np1_proposal and rnpr.np2_setno = pr.np2_setno  
											and nvl(rnpr.npr_basicflag, 'N') = 'N') ""Rider Premium"",
											(SELECT SUM(lnpr.npr_premium)+p2.np2_totload FROM LNPR_PRODUCT LNPR
											WHERE lnpr.np1_proposal = p2.np1_proposal and
											lnpr.np2_setno  =  P2.NP2_SETNO) ""Modal Premium"", ");

            sb_query.Append(@"           pr.Npr_Sumassured ""Sum Assured"",");
            sb_query.Append(@"           ccs.ccs_field1 ""Branch Code"",");
            sb_query.Append(@"            '' ""Cluster"",");
            sb_query.Append(@"            '' ""Region"",");
            sb_query.Append(@"            '' ""District"",");
            sb_query.Append("            ccs.ccs_descr BranchName,");
            sb_query.Append(@"           p1.use_userid ""User ID"",(SELECT USE_NAME FROM USE_USERMASTER WHERE USE_USERID=P1.USE_USERID) USERNAME,");
            sb_query.Append("            case");
            sb_query.Append("            when LENGTH(NVL((select np1_policyno from ilas_banca_interface where polc_np1_proposal=p1.np1_proposal),'')) > 2 then 'Policy Issued'");
            sb_query.Append("            when nvl(p2.np2_substandar,'N')= 'Y' then 'Referred'");
            sb_query.Append("            when nvl(p1.np1_selected,'D')= 'R' then 'Compliance OK'");
            sb_query.Append("            when nvl(p1.np1_selected,'D')= 'F' then 'Payment Waiting'");
            sb_query.Append("            when nvl(p1.np1_selected,'D')= 'Y' then 'Payment Deducted'");
            sb_query.Append(@"           else 'Pending' end ""POS Status"",");
            sb_query.Append(@"           (SELECT cst_descr  FROM  " + DBUSER + ".LNP1_POLICYMASTR NP, " + DBUSER + ".LCST_PPSTATUS ST WHERE NP1_PROPOSAL = P1.NP1_PROPOSAL AND NP.CST_STATUSCD = ST.CST_STATUSCD ) ");
            sb_query.Append(@"           ""Proposal Status (ILAS)"",");
            //03-01-2013 
            //Farrukh Start 
            //Break In Three Differenct Column by functions

            //sb_query.Append(@"            bf.nbf_benname ""Beneficiary"", rl.crl_descr ""Relation to Customer"", bf.nbf_idno ""CNIC/B Form No of beneficiary"", '' Remarks");
            sb_query.Append(@"           GetCSVBeneficiaries(p1.np1_proposal) ""Beneficiary"",");
            sb_query.Append(@"           GetCSVRelations(p1.np1_proposal) ""Relation to Customer"",");
            sb_query.Append(@"           GetCSVIDNumber(p1.np1_proposal) ""CNIC/B Form No of beneficiary"",");

            sb_query.Append("           (select replace(replace(l.cm_comments,'\"',''),'''','') from lncm_comments l where l.np1_proposal=p1.np1_proposal and l.cm_serial_no = (SELECT NVL(MAX(CM_SERIAL_NO),0) FROM LNCM_COMMENTS WHERE NP1_PROPOSAL = p1.np1_proposal)) CurrentStatus, ");

            sb_query.Append(@"       '' ""Remarks""");
            //Farrukh End 
            sb_query.Append("             FROM");
            sb_query.Append("             LNP1_POLICYMASTR P1");
            sb_query.Append("             INNER JOIN");
            sb_query.Append("             LNU1_UNDERWRITI u1 on");
            sb_query.Append("             p1 . np1_proposal=u1.np1_proposal");
            sb_query.Append("             INNER join LNPH_PHOLDER ph on");
            sb_query.Append("             PH . NPH_CODE=U1.NPH_CODE");
            sb_query.Append("             AND PH.NPH_LIFE = U1.NPH_LIFE");
            sb_query.Append("             AND U1.NU1_LIFE = 'F'");
            sb_query.Append("             INNER join");
            sb_query.Append("             LNPR_PRODUCT PR on");
            sb_query.Append("             pr.np1_proposal=p1.np1_proposal ");
            sb_query.Append("             and pr.npr_basicflag='Y' ");//farrukh
                                                                      //			sb_query.Append("             INNER join luch_userchannel uch on uch.cch_code = p1.np1_channel");
                                                                      //			sb_query.Append("             and uch.ccd_code = p1.np1_channeldetail");
                                                                      //
                                                                      //
                                                                      //			if(!ISMULTIBRANCH)
                                                                      //			{
                                                                      //				sb_query.Append("             and uch.use_userid = P1.Use_Userid");
                                                                      //			}
                                                                      //			else
                                                                      //			{
                                                                      //				sb_query.Append("       and uch.use_userid =(SELECT UC.USE_USERID FROM LUCH_USERCHANNEL UC");
                                                                      //				sb_query.Append("       WHERE UC.cch_code  = p1.np1_channel");
                                                                      //				sb_query.Append("       and uc.ccd_code = p1.np1_channeldetail");
                                                                      //				sb_query.Append("       and uc.ccs_code =p1.np1_channelsdetail AND UC.USE_USERID LIKE 'BSO%' )");
                                                                      //			}

            sb_query.Append("              INNER join CCS_CHANLSUBDETL ccs on ccs.cch_code || ccs.ccd_code || ccs.ccs_code =p1.pcl_locatcode");
            //			sb_query.Append("             INNER join CCS_CHANLSUBDETL ccs on ccs.cch_code = uch.cch_code");
            //			sb_query.Append("             and ccs.ccd_code = uch.ccd_code");
            //			sb_query.Append("             and ccs.ccs_code = uch.ccs_code");
            sb_query.Append("             inner join");
            sb_query.Append("             LPPR_PRODUCT LR ON");
            sb_query.Append("             LR.PPR_PRODCD=PR.PPR_PRODCD");
            sb_query.Append("             INNER JOIN LNP2_POLICYMASTR P2");
            sb_query.Append("             ON P2.NP1_PROPOSAL = P1.NP1_PROPOSAL");
            sb_query.Append("             INNER JOIN LNAD_ADDRESS ND");
            sb_query.Append("             ON  P1.NP1_PROPOSAL= ND.NP1_PROPOSAL AND ND.NAD_ADDRESSTYP = 'C' and ND.nph_life='D'");

            sb_query.Append("             WHERE");
            sb_query.Append("             PR.NPR_SUMASSURED > 0");
            sb_query.Append("             and p1.use_userid <> 'admin'");


            if (!IsAdmin)
            {
                sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                if (!BYPASSBRANCHFILTER)
                {
                    sb_query.Append("  and p1.np1_channelsdetail='" + BranchCode + "'");
                }
            }
            else
            {
                //Admin
                if (BankCode != "ALL")
                {
                    sb_query.Append("  and p1.np1_channeldetail='" + BankCode + "'");
                }
            }
            if (DateType == "IssueDate")
            {
                sb_query.Append("   and trunc(P1.np1_issuedate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            else
            {
                sb_query.Append("   and trunc(P1.np1_propdate) between to_date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')");
            }
            sb_query.Append(@"            ORDER BY p1.np1_proposal, p1 . np1_propdate");





            string query = sb_query.ToString();
            query = query.Replace(@"\", " ");
            query = EnvHelper.Parse(query);

            query = EnvHelper.Parse(query);


            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "UBLRPT");
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds.Tables["UBLRPT"];

            DataRow[] foundRows;
            if (DateType == "IssueDate")
            {
                foundRows = ds.Tables["UBLRPT"].Select(" ISSUEDDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND ISSUEDDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            else
            {
                foundRows = ds.Tables["UBLRPT"].Select(" NP1_PROPDATE >=#" + Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy") + "# AND NP1_PROPDATE <=#" + Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy") + "# ");
            }
            DataTable dtFinal = new DataTable();
            foreach (DataColumn colname in dtTable.Columns)
            {
                dtFinal.Columns.Add(colname.ColumnName, typeof(string));
            }

            foreach (DataRow sourcerow in foundRows)
            {
                dtFinal.ImportRow(sourcerow);
            }
            return dtFinal;
        }

        public static DataTable GetExcelReportForStaffChannel(string acch, string bccd)
        {
            string sqlString1 = "SELECT S.STAFF_ID as STAFF_ID,\n" +
                "		S.STAFF_NAME AS STAFF_NAME,\n" +
                //"		Ch.Cch_Descr AS CH_DESCR,\n" +
                "		Cd.Ccd_Descr AS BANK_NAME\n" +
                "	FROM LSCH_STAFFCHANNELMAPPING S\n" +
                "	INNER JOIN cch_channel ch \n" +
                "	on ch.cch_code = s.cch_code\n" +
                "	INNER JOIN CCD_CHANNELDETAIL cd\n" +
                "	on cd.ccd_code = s.ccd_code\n" +
                "	where s.cch_code = ('" + acch + "')\n" +
                "	and s.ccd_code = ('" + bccd + "')";

            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = sqlString1;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            int v = da1.Fill(ds1);
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds1.Tables[0];
            return dtTable;
        }


        public static DataTable GetExcelReportForPolicyIssuance(string FromDate, string ToDate, string users)
        {
            string user_con = string.Empty;

            string[] fd = FromDate.Split('/');
            string[] td = ToDate.Split('/');

            if (int.Parse(fd[0]) < 10)
            {
                fd[0] = "0" + int.Parse(fd[0]).ToString();
            }
            if (int.Parse(fd[1]) < 10)
            {
                fd[1] = "0" + int.Parse(fd[1]).ToString();
            }

            if (int.Parse(td[0]) < 10)
            {
                td[0] = "0" + int.Parse(td[0]).ToString();
            }
            if (int.Parse(td[1]) < 10)
            {
                td[1] = "0" + int.Parse(td[1]).ToString();
            }

            FromDate = fd[0] + "/" + fd[1] + "/" + fd[2];
            ToDate = td[0] + "/" + td[1] + "/" + td[2];

            if (users.ToUpper() != "ALL")
            {
                user_con = " and upper(c.cm_commentby)=upper('" + users + "')\n";
            }
            string sqlString = "Select c.NP1_PROPOSAL as PROPOSAL_NO,\n" +
            "       p.np1_policyno AS POLICY_NO,\n" +
            "       c.cm_commentby as TRANSFERRED_BY,\n" +
            "       p.NP1_PROPOSALREF AS PROPOSAL_REFERENCE_NO,\n" +
            "       to_char(c.cm_commentdate,'dd/MM/yyyy HH:MI:SS PM') as DATE_TRANSFER,\n" +
            "       chd.CCD_LOGO ||'-'||chd.ccd_descr as BANK_NAME,\n" +
            "       csd.ccs_field1||'-'||csd.ccs_descr as BRANCH\n" +
            "  from lncm_comments c\n" +
            " inner join lnp1_policymastr p\n" +
            "    on c.np1_proposal = p.np1_proposal\n" +
            "    and p.np1_policyno is not null\n" +
            " inner join lnu1_underwriti u\n" +
            " on u.np1_proposal=p.np1_proposal\n" +
            " inner join lnph_pholder ph\n" +
            " on ph.nph_code=u.nph_code\n" +
            " inner join CCD_CHANNELDETAIL chd\n" +
            " on chd.cch_code=p.np1_channel\n" +
            " and chd.ccd_code=p.np1_channeldetail\n" +
            " inner join CCS_CHANLSUBDETL csd\n" +
            "  on csd.cch_code=p.np1_channel\n" +
            " and csd.ccd_code=p.np1_channeldetail\n" +
            " and csd.ccs_code=p.np1_channelsdetail\n" +
            " where c.CM_Action = 'T'\n" +
            user_con +
            "and c.cm_commentdate between to_Date('" + FromDate + "','dd/MM/yyyy') and to_Date('" + ToDate + "','dd/MM/yyyy')\n" +
            " and c.cm_serial_no=(Select Max(cm_serial_no) from lncm_comments cm where cm.np1_proposal=c.np1_proposal)\n" +
           " group by  c.NP1_PROPOSAL ,\n" +
            "      p.np1_policyno,\n" +
            "      c.cm_commentby ,\n" +
            "      p.NP1_PROPOSALREF ,\n" +
            "      c.cm_commentdate,\n" +
            "      chd.CCD_LOGO ||'-'||chd.ccd_descr ,\n" +
            "      csd.ccs_field1||'-'||csd.ccs_descr\n" +
            " ORDER BY 5";


            OleDbConnection con = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = sqlString;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.Dispose();
            con.Close();
            DataTable dtTable = ds.Tables[0];
            return dtTable;

        }


        public DataHolder getPendingProposalList(string selected, string userId, string userType, string bankCode, string branchCode, bool IsFromTransferPolicy)
        {
            bool BRBYPASS = ace.Ace_General.IsBranchByPass(bankCode);
            StringBuilder query_ = new StringBuilder();
            //query_.Append("select * From (");		//chg_closing
            query_.Append("select nvl(p1.np1_selected,'D') np1_selected,cm.cm_commentdate,p1.np1_proposal,to_char(p1.np1_propdate, 'DD/MM/YYYY') np1_propdate,pr.ppr_descr plan, TO_CHAR(p2.np2_commendate ,'DD/MM/YYYY') np2_commendate, ph.nph_fullname,ph.nph_idno,ad.nad_mobile,coalesce(p1.np1_accountno, p1.np1_iban) np1_accountno,(SELECT SUM(NVL(NPR_PREMIUM,0))+SUM(NVL(NPR_LOADING,0)) from LNPR_PRODUCT WHERE NP1_PROPOSAL=p1.NP1_PROPOSAL),cm.cm_commentby,cm.cm_comments,p1.NP1_TOTDIFPREM,csd.ccs_field1||'-'||csd.ccs_descr branch,p2.use_datetime from lnp1_policymastr p1 ");
            query_.Append("inner join lnp2_policymastr p2 on ");
            query_.Append("      p1.np1_proposal = p2.np1_proposal and p2.np2_setno = 1 ");
            //query_.Append(selected.Equals("R") ? " nvl(np1_selected,'D') = 'R' " : " nvl(p2.np2_substandar,'D') <> 'D' ");
            if (selected.Equals("R") || selected.Equals("F"))
            {
                query_.Append(" AND nvl(np1_selected,'D') in ('").Append(selected).Append("','H') ");
            }
            else if (selected.Equals("W"))
            {
                query_.Append(" AND nvl(np1_selected,'W') in ('").Append(selected).Append("','H') ");
            }
            else if (selected.Equals("Y") || selected.Equals("y"))
            {
                query_.Append(" AND nvl(np1_selected,'D') = '").Append(selected).Append("' ");
                //Comneted By Izhar Ul Haque due to Show Standard and Sub Standard Cases query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' ");
                query_.Append(" AND nvl(p2.np2_substandar,'D') IN ('N','Y') ");
                query_.Append(" AND nvl(p1.np1_policyno, 'D') = 'D' "); // Policy No should be NULL
            }
            else if (selected.Equals("C") && userType == "O")
            {
                query_.Append(" AND nvl(np1_selected,'D') in ('").Append(selected).Append("','H') ");
            }
            else if (selected.Equals("C"))
            {
          //      query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'C' "); // old code
                query_.Append(" AND nvl(p2.np2_substandar,'D') IN ('Y', 'N') and np1_selected = 'C' "); // read also sub-standard
            }
            // new code BM pick sub-standard case
            else if  (selected.Length<= 0 && userType == "M" && bankCode == "F")
            {
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' "); // BM pick sub-standard case
            }

            // new code pick GM data
            else if (selected.Length <= 0 && userType == "H" && bankCode == "F")
            {
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'G' "); // GM pick sub-standard case
            }

            // BOP RBH
            else if (selected.Length <= 0 && userType == "H" && bankCode == "9")
            {
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' "); // POB RBH pick sub-standard case
            }

            // new code pick WMO after GM approval data

            else if (selected.Length <= 0 && userType == "W" && bankCode == "F")
            {
                // query_.Append(" AND nvl(p2.np2_substandar,'D') = 'Y' and np1_selected = 'P' "); // 
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' ");
                
            }


            else
            {
                //query_.Append(" AND nvl(p2.np2_substandar,'D') <> 'D' ");
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' ");

            }
            query_.Append("inner join lnu1_underwriti u1 on ");
            query_.Append("      u1.np1_proposal = p1.np1_proposal and u1.nph_life = 'D' and u1.nu1_life='F'  ");
            query_.Append(" inner join lnpr_product pp");
            query_.Append(" on pp.np1_proposal=p1.np1_proposal");
            query_.Append(" and pp.npr_basicflag='Y'");
            query_.Append(" inner join lppr_product pr");
            query_.Append(" on pr.ppr_prodcd=pp.ppr_prodcd");
            query_.Append(" inner join ccs_chanlsubdetl csd");
            query_.Append("    on p1.np1_channel = csd.cch_code");
            query_.Append("   and p1.np1_channeldetail = csd.ccd_code");
            query_.Append("   and p1.np1_channelsdetail = csd.ccs_code ");
            query_.Append("inner join lnph_pholder ph on ");
            query_.Append("      ph.nph_code = u1.nph_code and ph.nph_life = u1.nph_life ");
            query_.Append("inner join lnad_address ad on ");
            query_.Append("      ad.nph_code = ph.nph_code and ad.nph_life = ph.nph_life and ad.nad_addresstyp='C' ");
            query_.Append("inner join lncm_comments cm on cm.np1_proposal = p1.np1_proposal ");
            //query_.Append("       and cm.cm_serial_no = 1 ");
            query_.Append("       and cm.cm_serial_no =(select max(cm_serial_no) ");
            query_.Append("                                   from lncm_comments ");
            query_.Append("                                  where np1_proposal = p1.np1_proposal) ");
            //query_.Append("where p2.np2_substandar <> null ");

            query_.Append(" WHERE (1=1) ");

            if (selected.Equals("Y") || selected.Equals("y"))
            {
                query_.Append(" AND (nvl(np1_selected,'D') = 'Y') ");
                //Comneted By Izhar Ul Haque due to Show Standard and Sub Standard Cases query_.Append(" AND (nvl(p2.np2_substandar,'D') = 'N') ");
                query_.Append(" AND (nvl(p2.np2_substandar,'D') IN ('N','Y')) ");

                //New Part For Checking Transfer Flag

                      // BELOW IS ORGINAL QUERY FOR ADMIN
                 query_.Append(" AND p1.np1_proposal not in (select ib.polc_np1_proposal from ilas_banca_interface ib ");

                // TEMPARARY REMOVE NOT FROM BELOW QUERY FOR SHOWING SUB-STANDARD CASE JSB IN ADMIN
              //  query_.Append(" AND p1.np1_proposal  in (select ib.polc_np1_proposal from ilas_banca_interface ib ");

                query_.Append(" WHERE ib.polc_np1_proposal=p1.np1_proposal ");
                query_.Append(" AND nvl(ib.polc_trnsfr_ilas,'N')='Y') ");

                //End New Part For Checking Transfer Flag


            }
            if (userType == "S" || userType == "N")
            {
                query_.Append(" AND p1.use_userid='" + userId + "'  ");
            }
            else if (userType == "M") // BOP old
            {
                query_.Append(" AND p1.np1_channeldetail='" + bankCode + "'  ");
                query_.Append(" AND p1.np1_channelsdetail='" + branchCode + "'  ");
                if (bankCode == "9")
                {
                    query_.Append(" AND p1.np1_checked='N'");
                }
                else if (bankCode == "F") // new for JS bank
                {
                    query_.Append(" AND p1.np1_checked='Y'");
                }
            }

            // new show WMO case

            else if (userType == "W")
            {
                query_.Append(" AND p1.np1_channeldetail='" + bankCode + "'  ");
              //  query_.Append(" AND p1.np1_channelsdetail='" + branchCode + "'  "); //no need Branch code for WMO user
                if (bankCode == "F")
                {
                    query_.Append(" AND p1.np1_checked='N'");
                }
            }


            else if (userType == "H")
            {
                string mapping = string.Empty;
                DataHolder RHChannelsHolder = new DataHolder();
                string query = "Select listagg(ccs_code,',') WITHIN GROUP (order by ccs_code) ChannelMapping  From luch_userchannel where use_userid like '" + userId + "'";
                rowset rs = DB.executeQuery(query);
                if (rs.next())
                {
                    mapping = rs.getObject(1).ToString();
                }
                else
                {
                    mapping = branchCode;
                }
                query_.Append(" AND p1.np1_channeldetail='" + bankCode + "'  ");
                query_.Append(" AND p1.np1_channelsdetail in (" + mapping + ")  ");
                query_.Append(" AND p1.np1_checked='Y'");
            }
            else if (userType == "C" || userType == "L" || userType == "O" || userType == "W") // new code
            {
                query_.Append(" AND p1.np1_channeldetail='" + bankCode + "'");
                if (!BRBYPASS)
                {
                    query_.Append(" AND p1.np1_channelsdetail='" + branchCode + "' ");
                }

            }
            //else
            //{
            //	query_.Append(" AND 1<>1 ");			
            //}



            query_.Append("			order by p1.np1_proposal desc ");

            //chg_closing
            //query_.Append(") a");
            //if (Convert.ToString(SessionObject.Get("ClossingFlag")) == "P")
            //{
            //	query_.Append(" where trunc(a.np1_propdate) <= to_Date('" + Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate"))).ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ");
            //}
            //else
            //{
            //	var today = Convert.ToDateTime(Convert.ToString(SessionObject.Get("ClossingDate"))); ;
            //	var monthStart = new DateTime(today.Year, today.Month, 1);
            //	var lastMonthEnd = monthStart.AddDays(-1);
            //	query_.Append(" where trunc(a.np1_propdate) > to_Date('" + lastMonthEnd.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') ");
            //}
            //chg_closing_end
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            this.Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA");
            myCommand.Dispose();
            return this.Holder;
        }

        public DataHolder getPendingProposalListForAdmin()
        {
            DataHolder Holder = new DataHolder();
            string sqlString = "Select p1.np1_proposal,\n" +
            "         case when to_Char(p1.np1_propdate) is null then to_date('01/01/1947','dd/MM/yyyy') else p1.np1_propdate end as np1_propdate,\n" +
            "       lph.nph_fullname,\n" +
            "       lph.nph_idno,\n" +
            "       p1.cst_statuscd,\n" +
            "       p1.np1_selected,p1.Use_userId\n" +
            " from lnp1_policymastr p1\n" +
            "inner join lnu1_underwriti uw\n" +
            "on p1.np1_proposal=uw.np1_proposal\n" +
            "inner join lnph_pholder lph\n" +
            "on lph.nph_code=uw.nph_code\n" +
            "where ((p1.cst_statuscd is null) or (p1.cst_statuscd=0))\n" +
            "and p1.np1_selected is null \n" +
            " order by  2 desc";

            IDbCommand myCommand = DB.CreateCommand(sqlString, DB.Connection);
            Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA_ADMIN");
            myCommand.Dispose();
            return Holder;
        }
        public DataHolder getSearchedPendingProposalListForAdmin(string column, string text)
        {
            DataHolder Holder = new DataHolder();
            string condition = string.Empty;
            if (column == "1")
            {
                if (text != "")
                {
                    condition = "and p1.np1_proposal like '%" + text + "%'";
                }

            }
            else if (column == "2")
            {
                if (text != "")
                {
                    condition = "and to_Date(p1.np1_propdate,'yyyy-MM-dd')=to_Date('" + text + "','yyyy-MM-dd')";
                }
            }
            else if (column == "3")
            {
                if (text != "")
                {
                    condition = "and lph.nph_fullname like '%" + text + "%'";
                }
            }
            else if (column == "4")
            {
                if (text != "")
                {
                    condition = "and lph.nph_idno like '%" + text + "%'";
                }
            }
            else
            {
                condition = string.Empty;

            }
            string sqlString = "Select p1.np1_proposal,\n" +
            "         case when to_Char(p1.np1_propdate) is null then to_date('01/01/1947','dd/MM/yyyy') else p1.np1_propdate end as np1_propdate,\n" +
            "       lph.nph_fullname,\n" +
            "       lph.nph_idno,\n" +
            "       p1.cst_statuscd,\n" +
            "       p1.np1_selected,p1.Use_userId\n" +
            " from lnp1_policymastr p1\n" +
            "inner join lnu1_underwriti uw\n" +
            "on p1.np1_proposal=uw.np1_proposal\n" +
            "inner join lnph_pholder lph\n" +
            "on lph.nph_code=uw.nph_code\n" +
            "where ((p1.cst_statuscd is null) or (p1.cst_statuscd=0))\n" +
            "and p1.np1_selected is null\n" +
            condition + "\n" +
            "order by  2 desc";

            IDbCommand myCommand = DB.CreateCommand(sqlString, DB.Connection);
            Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA_ADMIN");
            myCommand.Dispose();
            return Holder;
        }
        //=============== New Method for Seacrhing 08012017
        public DataHolder getPendingProposalList_New(string selected, string userId, string userType, string bankCode, string branchCode, bool IsFromTransferPolicy, string Search_Event, bool Searched_Clicked, string ddlSearchValue, string SearchValue)
        {

            /********* Preparing Searching Criteria Query ********/
            string SearchCriteria = "";


            //if(txtSearchEvent.Text == "1" || txtSearchEvent.Text == "2" || txtSearchEvent.Text == "3")
            if (Search_Event == "1" || Search_Event == "2" || Search_Event == "3" || Search_Event == "4")
            {
                //if(SearchClicked == false)
                if (Searched_Clicked == false)
                {
                    //ddlsearch.SelectedIndex = Convert.ToInt16(txtSearchEvent.Text.Trim()=="" ? "0" : txtSearchEvent.Text);
                    ddlSearchValue = Search_Event.ToString().Trim() == "" ? "0" : Search_Event.ToString();
                }

                //if(ddlsearch.SelectedIndex == 1)//Proposal Search
                if (ddlSearchValue == "1")
                {
                    //SearchCriteria = " AND UPPER(NP1_PROPOSAL) LIKE UPPER('"+txtsearch.Text+"')";
                    SearchCriteria = " AND UPPER(p1.NP1_PROPOSAL) LIKE UPPER('%" + SearchValue.ToString() + "%')";
                }
                //else if(ddlsearch.SelectedIndex == 2)//Proposal Date Search
                else if (ddlSearchValue == "2")
                {
                    //SearchCriteria = " AND UPPER(NP1_PROPDATE) LIKE UPPER('"+txtsearch.Text+"')";
                    SearchCriteria = " AND p1.NP1_PROPDATE = INITCAP('" + SearchValue.ToString() + "')";
                }
                //else if(ddlsearch.SelectedIndex == 2)//Name Search
                else if (ddlSearchValue == "3")
                {
                    //SearchCriteria = " AND UPPER(NPH_FULLNAME) LIKE UPPER('"+txtsearch.Text+"') ";
                    SearchCriteria = " AND UPPER(ph.NPH_FULLNAME) LIKE UPPER('%" + SearchValue.ToString() + "%') ";
                }
                //else if(ddlsearch.SelectedIndex == 3)//NIC Search
                else if (ddlSearchValue == "4")
                {
                    //SearchCriteria = "  AND (NPH_IDNO) LIKE '"+txtsearch.Text+"'";
                    SearchCriteria = "  AND (ph.NPH_IDNO) LIKE '%" + SearchValue.ToString() + "%'";
                }

            }

            /********* Preparing Searching Criteria Query ********/


            bool BRBYPASS = ace.Ace_General.IsBranchByPass(bankCode);
            StringBuilder query_ = new StringBuilder();
            query_.Append("select nvl(p1.np1_selected,'D') np1_selected,p1.np1_proposal,p1.np1_propdate, TO_CHAR(p2.np2_commendate ,'DD/MM/YYYY') np2_commendate, ph.nph_fullname,ph.nph_idno,ad.nad_mobile,coalesce(p1.np1_accountno,p1.np1_iban) np1_accountno,(SELECT SUM(NVL(NPR_PREMIUM,0))+SUM(NVL(NPR_LOADING,0)) from LNPR_PRODUCT WHERE NP1_PROPOSAL=p1.NP1_PROPOSAL),cm.cm_commentby,cm.cm_comments from lnp1_policymastr p1 ");
            query_.Append("inner join lnp2_policymastr p2 on ");
            query_.Append("      p1.np1_proposal = p2.np1_proposal and p2.np2_setno = 1 ");
            //query_.Append(selected.Equals("R") ? " nvl(np1_selected,'D') = 'R' " : " nvl(p2.np2_substandar,'D') <> 'D' ");
            if (selected.Equals("R") || selected.Equals("F"))
            {
                query_.Append(" AND nvl(np1_selected,'D') = '").Append(selected).Append("' ");
            }
            else if (selected.Equals("Y") || selected.Equals("y"))
            {
                query_.Append(" AND nvl(np1_selected,'D') = '").Append(selected).Append("' ");
                //Comneted By Izhar Ul Haque due to Show Standard and Sub Standard Cases query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' ");
                query_.Append(" AND nvl(p2.np2_substandar,'D') IN ('N','Y') ");
                query_.Append(" AND nvl(p1.np1_policyno, 'D') = 'D' "); // Policy No should be NULL
            }
            else if (selected.Equals("C"))
            {
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'C' ");
            }
            else
            {
                //query_.Append(" AND nvl(p2.np2_substandar,'D') <> 'D' ");
                query_.Append(" AND nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' ");
            }
            query_.Append("inner join lnu1_underwriti u1 on ");
            query_.Append("      u1.np1_proposal = p1.np1_proposal and u1.nph_life = 'D' and u1.nu1_life='F'  ");
            query_.Append("inner join lnph_pholder ph on ");
            query_.Append("      ph.nph_code = u1.nph_code and ph.nph_life = u1.nph_life ");
            query_.Append("inner join lnad_address ad on ");
            query_.Append("      ad.nph_code = ph.nph_code and ad.nph_life = ph.nph_life and ad.nad_addresstyp='C' ");
            query_.Append("inner join lncm_comments cm on cm.np1_proposal = p1.np1_proposal ");
            //query_.Append("       and cm.cm_serial_no = 1 ");
            query_.Append("       and cm.cm_serial_no =(select max(cm_serial_no) ");
            query_.Append("                                   from lncm_comments ");
            query_.Append("                                  where np1_proposal = p1.np1_proposal) ");
            //query_.Append("where p2.np2_substandar <> null ");

            query_.Append(" WHERE (1=1) ");

            if (selected.Equals("Y") || selected.Equals("y"))
            {
                query_.Append(" AND (nvl(np1_selected,'D') = 'Y') ");
                //Comneted By Izhar Ul Haque due to Show Standard and Sub Standard Cases query_.Append(" AND (nvl(p2.np2_substandar,'D') = 'N') ");
                query_.Append(" AND (nvl(p2.np2_substandar,'D') IN ('N','Y')) ");

                //New Part For Checking Transfer Flag

                query_.Append(" AND p1.np1_proposal not in (select ib.polc_np1_proposal from ilas_banca_interface ib ");
                query_.Append(" WHERE ib.polc_np1_proposal=p1.np1_proposal ");
                query_.Append(" AND nvl(ib.polc_trnsfr_ilas,'N')='Y') ");

                //End New Part For Checking Transfer Flag


            }
            if (userType == "S" || userType == "N")
            {
                query_.Append(" AND p1.use_userid='" + userId + "'  ");
            }
            else if (userType == "M" || userType == "O")
            {
                query_.Append(" AND p1.np1_channeldetail=" + bankCode + "  ");
                query_.Append(" AND p1.np1_channelsdetail='" + branchCode + "'  ");
            }
            else if (userType == "C" || userType == "L")
            {
                query_.Append(" AND p1.np1_channeldetail=" + bankCode + "  ");
                if (!BRBYPASS)
                {
                    query_.Append(" AND p1.np1_channelsdetail='" + branchCode + "'  ");
                }
            }
            //else
            //{
            //	query_.Append(" AND 1<>1 ");			
            //}

            //======= New Addition for Searching 09012018
            query_.Append(SearchCriteria);
            //======= New Addition for Searching 09012018

            query_.Append("			order by p1.np1_proposal desc ");
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);

            this.Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA");
            myCommand.Dispose();
            return this.Holder;
        }

        //=============== New Method for Searching 08012017

        public IDataReader getProposalSatusList(string extraClause, String userId, String userType, String branchCode, string BankCode)
        {

            if (extraClause.Equals(string.Empty))
            {
                extraClause = "1=1";
            }

            StringBuilder query_ = new StringBuilder();
            /*query_.Append("select nvl(p1.np1_selected,'D') np1_selected,p1.np1_proposal,p1.np1_propdate,ph.nph_fullname,ph.nph_idno,ad.nad_mobile,p1.np1_accountno,p2.np2_totpremium,cm.cm_commentby,cm.cm_comments from lnp1_policymastr p1 ");
			query_.Append("inner join lnp2_policymastr p2 on ");
			query_.Append("      p1.np1_proposal = p2.np1_proposal and p2.np2_setno = 1 ");
			
			
			//query_.Append(selected.Equals("R") ? " nvl(np1_selected,'D') = 'R' " : " nvl(p2.np2_substandar,'D') <> 'D' ");
			//			if(selected.Equals("R") || selected.Equals("F"))
			//			{
			//				query_.Append(" nvl(np1_selected,'D') = '").Append(selected).Append("' ");
			//			}			
			//			else
			//			{
			//				//query_.Append(" nvl(p2.np2_substandar,'D') <> 'D' ");
			//				query_.Append(" nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' ");
			//			}
			query_.Append("inner join lnu1_underwriti u1 on ");
			query_.Append("      u1.np1_proposal = p1.np1_proposal and u1.nph_life = 'D' and u1.nu1_life='F'  ");
			query_.Append("inner join lnph_pholder ph on ");
			query_.Append("      ph.nph_code = u1.nph_code and ph.nph_life = u1.nph_life ");
			query_.Append("inner join lnad_address ad on ");
			query_.Append("      ad.nph_code = ph.nph_code and ad.nph_life = ph.nph_life and ad.nad_addresstyp='C' ");
			query_.Append("INNER join lncm_comments cm on cm.np1_proposal = p1.np1_proposal ");
			//query_.Append("       and cm.cm_serial_no = 1 ");
			query_.Append("       and cm.cm_serial_no =(select max(cm_serial_no) ");
			query_.Append("       from lncm_comments ");
			query_.Append("       where np1_proposal = p1.np1_proposal) ");
			query_.Append("		   ");
			*/


            query_.Append(" SELECT ");
            query_.Append(" inq.np1_proposal, inq.np1_selected, inq.policy_status, inq.np1_propdate, ");
            query_.Append(" inq.nph_fullname, inq.nph_idno, inq.nad_mobile, ");
            // Add below line for IBAN # 29Oct2025 by Imran
            query_.Append(" coalesce(inq.np1_accountno,inq.np1_iban) np1_accountno, inq.np2_totpremium, inq.cm_commentby, ");
         
            // query_.Append(" case when inq.np1_accountno is null then inq.np1_iban else inq.np1_accountno end np1_accountno");

            query_.Append(" inq.np2_totpremium, inq.cm_commentby, ");
            query_.Append(" inq.cm_comments ");
            query_.Append(" from vw_CpuInquiry inq ");

            query_.Append("where  1=1 ");

            if (userType == "S" || userType == "N")
            {
                query_.Append(" AND inq.use_userid='" + userId + "'  ");
            }
            else if (userType == "M")
            {
                query_.Append(" AND inq.np1_channelsdetail='" + branchCode + "'  ");
            }
            else if (userType == "V")
            {
                query_.Append(" AND inq.np1_channeldetail='" + BankCode + "'  ");
            }
            else if (userType == "C" || userType == "A" || userType == "B")
            {

            }
            else
            {
                query_.Append(" AND 1<>1 ");
            }
            query_.Append(" AND " + extraClause + " order by inq.np1_proposal desc ");
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            return myCommand.ExecuteReader();

            //this.Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA"); return this.Holder;
        }

        public DataHolder getUnApprovedProposalList(string selected, String userId)
        {
            StringBuilder query_ = new StringBuilder();
            query_.Append("select nvl(p1.np1_selected,'D') np1_selected,p1.np1_proposal,p1.np1_propdate,ph.nph_fullname,ph.nph_idno,ad.nad_mobile,coalesce(p1.np1_accountno, p1.np1_iban) np1_accountno,p2.np2_totpremium,cm.cm_commentby,cm.cm_comments from lnp1_policymastr p1 ");
            query_.Append("inner join lnp2_policymastr p2 on ");
            query_.Append("      p1.np1_proposal = p2.np1_proposal and p2.np2_setno = 1 ");
            //query_.Append(selected.Equals("R") ? " nvl(np1_selected,'D') = 'R' " : " nvl(p2.np2_substandar,'D') <> 'D' ");
            //			if(selected.Equals("R") || selected.Equals("F"))
            //			{
            //				query_.Append(" nvl(np1_selected,'D') = '").Append(selected).Append("' ");
            //			}			
            //			else
            //			{
            //				//query_.Append(" nvl(p2.np2_substandar,'D') <> 'D' ");
            //				query_.Append(" nvl(p2.np2_substandar,'D') = 'N' and np1_selected = 'P' ");
            //			}
            query_.Append("inner join lnu1_underwriti u1 on ");
            query_.Append("      u1.np1_proposal = p1.np1_proposal and u1.nph_life = 'D' and u1.nu1_life='F'  ");
            query_.Append("inner join lnph_pholder ph on ");
            query_.Append("      ph.nph_code = u1.nph_code and ph.nph_life = u1.nph_life ");
            query_.Append("inner join lnad_address ad on ");
            query_.Append("      ad.nph_code = ph.nph_code and ad.nph_life = ph.nph_life and ad.nad_addresstyp='C' ");
            query_.Append("INNER join lncm_comments cm on cm.np1_proposal = p1.np1_proposal ");
            //query_.Append("       and cm.cm_serial_no = 1 ");
            query_.Append("       and cm.cm_serial_no =(select max(cm_serial_no) ");
            query_.Append("       from lncm_comments ");
            query_.Append("       where np1_proposal = p1.np1_proposal) ");
            query_.Append("		   ");
            query_.Append("where  ");
            query_.Append("		p1.use_userid='" + userId + "'  and upper(cm.cm_action)='NOT OK'	order by p1.np1_proposal desc ");
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            this.Holder.FillData(myCommand, "LNP1_POLICYMASTR_DATA");
            myCommand.Dispose();
            return this.Holder;

        }

        public IDataReader getProposalListforDecision(string extraClause, String userId, String userType, String branchCode, string bankCode = "")
        {

            if (extraClause.Equals(string.Empty))
            {
                extraClause = "1=1";
            }

            StringBuilder query_ = new StringBuilder();

            query_.Append(" SELECT ");
            query_.Append(" inq.np1_proposal, inq.np1_selected, inq.policy_status, inq.np1_propdate, ");
            query_.Append(" inq.nph_fullname, inq.nph_idno, inq.nad_mobile, ");
            query_.Append(" coalesce(inq.np1_accountno,inq.np1_iban) np1_accountno, inq.np2_totpremium, inq.cm_action, inq.cm_commentby, ");
            query_.Append(" inq.cm_comments ");
            query_.Append(" from vw_CpuInquiry inq ");

            query_.Append("where  1=1 ");
          //  query_.Append(" AND inq.use_userid NOT LIKE 'BCO%' ");

            
            if (userType != "A")
            {
                query_.Append(" and inq.np1_channeldetail = '" + bankCode + "'"); // not showing JS Bank record
            }
            if (userType == "L")
            {
                query_.Append(" AND (inq.cm_action='Decs' or inq.np1_selected='K') ");
                query_.Append(" order by inq.np1_proposal desc ");
            }
            else
            {
                query_.Append(" AND " + extraClause + " order by inq.np1_proposal desc ");
            }
            IDbCommand myCommand = DB.CreateCommand(query_.ToString(), DB.Connection);
            return myCommand.ExecuteReader();
        }


        //<method><method-name>GetILUS_ET_NM_HOME_lister_filter_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_HOME_lister_filter_RO(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE  ({0} like '{1}')  AND NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_HOME_lister_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_HOME_lister_RO(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>Exists</method-name><method-signature>
        public static Boolean Exists(NameValueCollection pkNameValue)
        {
            //</method-signature><method-body>
            String strQuery = "SELECT count(*) FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=? ";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, pkNameValue["NP1_PROPOSAL"]));
            int noOfRecords = (int)myCommand.ExecuteScalar();
            myCommand.Dispose();
            return (noOfRecords >= 1);
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_PER_PROPOSALDET_lister_filter_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_PER_PROPOSALDET_lister_filter_RO(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE  ({0} like '{1}')  AND NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_WELCOME_lister_filter_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_WELCOME_lister_filter_RO(string columnName, string columnValue)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE  ({0} like '{1}')  AND NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            query = string.Format(query, columnName, columnValue);
            query = string.Format(query, columnName, columnValue);
            query = EnvHelper.Parse(query);
            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_WELCOME_lister_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_WELCOME_lister_RO(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        //<method><method-name>GetILUS_ET_NM_PER_PROPOSALDET_lister_RO</method-name><method-signature>
        public static IDataReader GetILUS_ET_NM_PER_PROPOSALDET_lister_RO(int offset, int numRows)
        {
            //</method-signature><method-body>
            StringBuilder sb_query = new StringBuilder(198);//to do we have to Optimize it too.
            sb_query.Append("SELECT NP1_PROPOSAL FROM LNP1_POLICYMASTR  WHERE NP2_SETNO = 1 AND NP1_PROPOSAL = SV(\"NP1_PROPOSAL\")  ");
            string query = sb_query.ToString(); query = EnvHelper.Parse(query);

            IDbCommand myCommand = DB.CreateCommand(query);
            return myCommand.ExecuteReader();
            //</method-body>
        }
        //</method>

        public static void MarkStatusNull(string proposal)
        {
            DB.executeDML("update lnp1_policymastr set np1_selected=null, cdc_code=null where np1_proposal = '" + proposal + "'");
        }

        public static void UpdateLastFileName(string FileName)
        {
            DB.executeDML("UPDATE LCSD_SYSTEMDTL SET CSD_VALUE='" + FileName + "' WHERE CSH_ID='OPSFL'");
        }

        public static void MarkSubStand(string proposal, string Status)
        {
            DB.executeDML("update lnp2_policymastr set np2_substandar='" + Status + "' where np1_proposal = '" + proposal + "'");
        }

        public static void UpdateCommencmentDate(string proposal, DateTime CommencDate)
        {
            //we don't assign 28-31 days of a month as a commencement date on proposals.

            //if(CommencDate.Day >= 28)
            //{
            //CommencDate = CommencDate; //new DateTime(CommencDate.Year,CommencDate.Month,27);

            //}
            //			if(CommencDate.Day > 27)
            //			{
            //				string ComDate="27/"+CommencDate.Month+"/"+CommencDate.Year;
            //				CommencDate=Convert.ToDateTime(ComDate);
            //			}

            string strQuery = "Update lnp1_policymastr set np2_commendate=? where np1_proposal = '" + proposal + "'";   //chg-04032024 Feb-29 issue comments the line and add below	
                                                                                                                        //string strQuery = "Update lnp1_policymastr set np2_commendate=to_date('29/02/2024','DD/MM/YYYY') where np1_proposal = '" + proposal + "'";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@np2_commendate", DbType.Date, CommencDate));
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            string strQuery2 = "Update lnp2_policymastr set np2_commendate=? where np1_proposal = '" + proposal + "'";  //chg-04032024 Feb-29 issue comments the line and add below	
                                                                                                                        //string strQuery2 = "Update lnp2_policymastr set np2_commendate=to_date('29/02/2024','DD/MM/YYYY') where np1_proposal = '" + proposal + "'";
            IDbCommand myCommand2 = DB.CreateCommand(strQuery2, DB.Connection);
            myCommand2.Parameters.Add(DB.CreateParameter("@np2_commendate", DbType.Date, CommencDate));
            myCommand2.ExecuteNonQuery();
            myCommand2.Dispose();
            //UpdateMaturityDate(proposal);
            UpdateMaturityDate(proposal);
        }

        public static void UpdateBranchDetails(string PROPOSAL, string CCS_FIELD1)
        {
            //Create Proposal For the Branch Which is not assigned to the Agent
            string CCH_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCH_CODE").ToString();
            string CCD_CODE = SHMA.Enterprise.Presentation.SessionObject.Get("s_CCD_CODE").ToString();
            bool ISMULTIBRANCH = ace.Ace_General.MultiBranchCase(CCD_CODE);

            if (SessionObject.Get("s_ISMULTIBRANCH") != null && SessionObject.Get("s_ISMULTIBRANCH").ToString() == "Y")
            {
                ISMULTIBRANCH = true;
            }

            if (ISMULTIBRANCH)
            {
                string PCL_LOCATCODE = "";
                string AAG_AGCODE = "";
                string NP1_PRODUCER = "";
                string NP1_CHANNELSDETAIL = "";

                rowset rs = DB.executeQuery("SELECT CCS_CODE FROM CCS_CHANLSUBDETL WHERE CCH_CODE='" + CCH_CODE + "' AND CCD_CODE='" + CCD_CODE + "' AND CCS_FIELD1='" + CCS_FIELD1 + "'");
                if (rs.next())
                    NP1_CHANNELSDETAIL = rs.getString(1);

                PCL_LOCATCODE = CCH_CODE + CCD_CODE + NP1_CHANNELSDETAIL;

                if (CCS_FIELD1.Length == 5)
                {
                    AAG_AGCODE = "9" + CCD_CODE + CCS_FIELD1;
                }
                else
                {
                    AAG_AGCODE = ace.Ace_General.AGENTPREFIX + CCD_CODE + CCS_FIELD1;
                }
                NP1_PRODUCER = AAG_AGCODE;

                string strQuery = "UPDATE LNP1_POLICYMASTR SET PCL_LOCATCODE='" + PCL_LOCATCODE + "',AAG_AGCODE='" + AAG_AGCODE + "',NP1_PRODUCER='" + NP1_PRODUCER + "',NP1_CHANNELSDETAIL='" + NP1_CHANNELSDETAIL + "' WHERE NP1_PROPOSAL='" + PROPOSAL + "'";
                IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                string strQuery2 = "UPDATE LNP2_POLICYMASTR SET AAG_AGCODE='" + AAG_AGCODE + "' WHERE NP1_PROPOSAL = '" + PROPOSAL + "'";
                IDbCommand myCommand2 = DB.CreateCommand(strQuery2, DB.Connection);
                myCommand2.ExecuteNonQuery();
                myCommand2.Dispose();
            }
        }


        public static void UpdateMaturityDate(string proposal)
        {
            rowset rs = DB.executeQuery("SELECT NPR_BENEFITTERM FROM LNPR_PRODUCT WHERE NPR_BASICFLAG='Y' AND NP1_PROPOSAL='" + proposal + "'");
            if (rs.next())
            {
                //******* Update Basic Product Maturity Date *********//
                int BTERM = Convert.ToInt16(rs.getString("NPR_BENEFITTERM"));
                DateTime CommDate = ace.clsIlasUtility.getCommencementDate(proposal);
                DateTime MatDate = new DateTime(CommDate.Year + BTERM, CommDate.Month, CommDate.Day);   //chg-04032024 Feb-29 issue comments the line
                SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                pc.clear();

                pc.puts("@NPR_BENEFITTERM", BTERM, DbType.String);
                pc.puts("@NPR_MATURITYDATE", MatDate, DbType.Date); //chg-04032024 Feb-29 issue comments the line
                pc.puts("@NP1_PROPOSAL", proposal, DbType.String);

                DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_MATURITYDATE=? WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'", pc);  //chg-04032024 Feb-29 issue comments the line and add below
                                                                                                                                                                //DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_MATURITYDATE=to_date('29/02/2024','DD/MM/YYYY') WHERE NP1_PROPOSAL=? AND NP2_SETNO=1 AND NPR_BASICFLAG='Y'", pc);  


            }

            //******* Update Selected Riders Maturity Date *********//
            UpdateRidersMaturityDate(proposal);
        }

        public static void UpdateRidersMaturityDate(string proposal)
        {
            rowset rs = DB.executeQuery("SELECT NPR_BENEFITTERM,PPR_PRODCD FROM LNPR_PRODUCT WHERE NPR_BASICFLAG='N' AND NPR_SELECTED='Y' AND NP1_PROPOSAL='" + proposal + "'");
            while (rs.next())
            {
                //******* Update Maturity Date *********//
                int BTERM = Convert.ToInt16(rs.getString("NPR_BENEFITTERM"));
                string PPR_PRODCD = rs.getString("PPR_PRODCD");
                DateTime CommDate = ace.clsIlasUtility.getCommencementDate(proposal);
                DateTime MatDate = new DateTime(CommDate.Year + BTERM, CommDate.Month, CommDate.Day);
                SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
                pc.clear();

                pc.puts("@NPR_BENEFITTERM", BTERM, DbType.String);
                pc.puts("@NPR_MATURITYDATE", MatDate, DbType.Date);
                pc.puts("@NP1_PROPOSAL", proposal, DbType.String);
                pc.puts("@PPR_PRODCD", PPR_PRODCD, DbType.String);

                DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_MATURITYDATE=? WHERE NP1_PROPOSAL=? AND NPR_BASICFLAG='N' AND PPR_PRODCD=?", pc); //chg-04032024 Feb-29 issue comments the line and add below
                                                                                                                                                                //DB.executeDML("UPDATE LNPR_PRODUCT SET NPR_BENEFITTERM=?, NPR_MATURITYDATE=to_date('29/02/2024','DD/MM/YYYY') WHERE NP1_PROPOSAL=? AND NPR_BASICFLAG='N' AND PPR_PRODCD=?", pc);

            }

        }


        public static void MarkStandSubStandNull(string proposal)
        {
            DB.executeDML("update lnp2_policymastr set np2_substandar=null where np1_proposal = '" + proposal + "'");
        }

        public static void MarkStatus(string proposal, string status)
        {
            if (status == "D")
            {
                DB.executeDML("update lnp1_policymastr set cst_statuscd='14', np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");
            }
            else
            {
                if (SessionObject.Get("s_use_type").ToString() == "H")
                {
                    if (status == "R")
                    {
                        DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

                    }

                    else if (status == "P") // new update status code for GM
                    {
                        DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

                    }

                    else
                    {
                        DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");

                    }
                }

                // new update status code for GM

                // for referal approval process flow for JS bank release case for BM or WMO
                //else if (SessionObject.Get("s_use_type").ToString() == "L" && System.Web.HttpContext.Current.Session["BankCode"].ToString() == "F")
                //{
                //    if (status == "P")
                //    {
                //        //  DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

                //        DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");
                //        DB.executeDML("update lnp2_policymastr set np2_substandar = 'N' where np1_proposal = '" + proposal + "'");


                //    }

                //}
                else
                {
                    DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");
                }

            }

        }

        //public static void MarkStatus(string proposal, string status, string substandardFlag)
        //{
        //    if (status == "D")
        //    {
        //        DB.executeDML("update lnp1_policymastr set cst_statuscd='14', np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");
        //    }
        //    else
        //    {
        //        if (SessionObject.Get("s_use_type").ToString() == "H")
        //        {
        //            if (status == "R")
        //            {
        //                DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

        //            }

        //            else if (status == "P") // new update status code for GM
        //            {
        //                DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

        //            }

        //            else
        //            {
        //                DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");

        //            }
        //        }

        //        // new update status code for GM

        //        // for referal approval process flow for JS bank release case for BM or WMO
        //       // else if (SessionObject.Get("s_use_type").ToString() == "L" && System.Web.HttpContext.Current.Session["BankCode"].ToString() == "F" && 
        //       //     (cstatus == null || (cstatus+"").Trim().Length <=0) )

        //        //    else if (SessionObject.Get("s_use_type").ToString() == "L" && System.Web.HttpContext.Current.Session["BankCode"].ToString() == "F" &&
        //        //    (substandardFlag == "Y" ))

        //        //        {
        //        //                           //  DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "',np1_checked='N' where np1_proposal = '" + proposal + "'");

        //        //        // DB.executeDML("update lnp1_policymastr set np1_selected = 'P' where np1_proposal = '" + proposal + "'");
        //        //        DB.executeDML("update lnp2_policymastr set np2_substandar = 'N' where np1_proposal = '" + proposal + "'");



        //        //}
        //        else
        //        {
        //            DB.executeDML("update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'");
        //        }

        //    }

        //}

        public static void insertCollection(string proposal, string userid, double collection, string collectiondate)
        {
            try
            {
                DB.executeDML("insert into LBCC_Collection values('" + proposal + "','" + userid + "','" + collection + "',TO_DATE('" + collectiondate + "','YYYY-MM-DD'))");
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////================= MARK PAYMENT UNCOLLECTED - 08-MAY-2018
        public static void MarkStatus_Payment(string proposal, string status)
        {
            /*String strQuery = " UPDATE LNP1_POLICYMASTR SET NP1_SELECTED = ? WHERE NP1_PROPOSAL = ? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12 proposal));
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_SELECTED",DbType.String, 1, status));
			myCommand.ExecuteNonQuery();*/

            String strQuery = "update lnp1_policymastr set np1_selected = '" + status + "' where np1_proposal = '" + proposal + "'";
            DataTable dt = new DataTable();
            OleDbDataAdapter myda = new OleDbDataAdapter(strQuery, DB.Connection.ConnectionString.ToString());
            myda.Fill(dt);

            /*SHMA.Enterprise.Data.ParameterCollection pm = new SHMA.Enterprise.Data.ParameterCollection();
			pm.puts("@NP1_PROPOSAL",proposal);		
			pm.puts("@NP1_SELECTED",status);	
			DB.executeDML("UPDATE LNP1_POLICYMASTR SET NP1_SELECTED = ? WHERE NP1_PROPOSAL = ? ", pm);*/
        }
        ////================= MARK PAYMENT UNCOLLECTED - 08-MAY-2018	

        /// <param name="proposal"></param>
        public static void UnMarkStandSubStand(string proposal)
        {
            //DB.executeDML("update lnp2_policymastr set np2_substandar = NULL where np1_proposal = '"+proposal+"'");
        }

        public static void MarkStandSubStandNullReCalc(string proposal)
        {
            DB.executeDML("update lnp2_policymastr set np2_approved=null, np2_substandar=null where np1_proposal = '" + proposal + "'");
        }

        public static string getStatus(string NP1_PROPOSAL)
        {
            //</method-signature><method-body>
            String strQuery = " SELECT NP1_SELECTED FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=?";
            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
            myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, NP1_PROPOSAL));
            object objPolicy = myCommand.ExecuteScalar();
            myCommand.Dispose();
            if (objPolicy == null)
            {
                return "";
            }
            else
            {
                return System.Convert.ToString(objPolicy);
            }
        }
        // new method for get Proposal status and bank code
        public static string ProposalStatusandBankCode(string NP1_PROPOSAL , String Bank_Code)
            {
            //String strQuery = @"select  '''' || ms.np1_selected || ''',''' || cs.ccd_code || '''' AS combined_value  from lnp1_policymastr ms 
            //                   inner join use_usermaster us on us.use_userid = ms.use_userid
            //                   inner join ccs_chanlsubdetl cs on ccs_field1 = SUBSTR(ms.use_userid, 6)
            //                    where ms.np1_proposal = ? ";

            //String strQuery = @"select  '''' || ms.np1_checked || ''',''' || cs.ccd_code || '''' AS combined_value  from lnp1_policymastr ms 
            //                   inner join use_usermaster us on us.use_userid = ms.use_userid
            //                   inner join ccs_chanlsubdetl cs on ccs_field1 = SUBSTR(ms.use_userid, 6)
            //                    where ms.np1_proposal = ? ";

            
            String strQuery = @"SELECT '''' || ms.np1_Selected || ''',''' || ms.np1_checked || ''',''' || cs.ccd_code || ''',''' ||p2.np2_substandar || '''' AS combined_value  FROM lnp1_policymastr ms
                                  INNER JOIN use_usermaster us
                                  ON us.use_userid = ms.use_userid
                                  INNER JOIN ccs_chanlsubdetl cs
                                  ON cs.ccs_field1 = SUBSTR(ms.use_userid, 6)
                            INNER JOIN lnp2_policymastr p2 ON p2.np1_proposal =ms.np1_proposal
                                  WHERE ms.np1_proposal = ?  and cs.ccd_code='" + Bank_Code + "'";



            IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
                    myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, 12, NP1_PROPOSAL));
                    object objPolicy = myCommand.ExecuteScalar();
                    myCommand.Dispose();
                    if (objPolicy == null)
                    {
                        return "";
                    }
                    else
                    {
                        return System.Convert.ToString(objPolicy);
                    }
        }

        public static string GetLastUpdatedFileName()
		{
			//</method-signature><method-body>
			String strQuery = " SELECT NVL(CSD_VALUE,'') CSD_VALUE FROM LCSD_SYSTEMDTL WHERE CSH_ID='OPSFL'";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			object objFileName = myCommand.ExecuteScalar();

			if(objFileName==null || objFileName.ToString()=="1")
			{
				return "";
			}
			else
			{
				return System.Convert.ToString(objFileName);
			}
		}

		public static string getBANKFORILAS(string CCD_CODE)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT CCD_LOGO FROM CCD_CHANNELDETAIL WHERE CCD_CODE =? AND CCH_CODE=?";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@CCD_CODE",DbType.String, 12, CCD_CODE));
			myCommand.Parameters.Add(DB.CreateParameter("@CCH_CODE",DbType.String, 12, ace.Ace_General.BANCACHANNEL));
			
			object objBANK = myCommand.ExecuteScalar();

			if(objBANK==null)
			{
				return "";
			}
			else
			{
				return System.Convert.ToString(objBANK);
			}
		}

		public static int CalculateAge(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT NPH.NPH_BIRTHDATE FROM LNPH_PHOLDER NPH INNER JOIN LNU1_UNDERWRITI NU1 ON NU1.NPH_CODE=NPH.NPH_CODE WHERE NU1.NP1_PROPOSAL=? AND NU1.NU1_LIFE='F'";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			DateTime objNPH_BIRTHDATE =(DateTime) myCommand.ExecuteScalar();
			int Age=Convert.ToInt16(GetAgeInYears(objNPH_BIRTHDATE));
            myCommand.Dispose();
			return Age;
		}

		public static Double GetAgeInYears(DateTime dOfB)
		{
			TimeSpan span1 = new DateTime(dOfB.Year,dOfB.Month,dOfB.Day)-new DateTime(1970,1,1);
			TimeSpan span2 = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day)-new DateTime(1970,1,1);
		

	       	Double TotalMillisecondsPre = span1.TotalMilliseconds;
			Double TotalMillisecondsNew = span2.TotalMilliseconds;

			
			Double msPerDay = 24 * 60 * 60 * 1000 * 365.25;
			string AgeRoundingCriteria=ace.clsIlasUtility.getAgeRoundingCriteria();
		


			if(AgeRoundingCriteria == null)
				return  Math.Round((TotalMillisecondsNew-TotalMillisecondsPre)/ msPerDay) ;
			else if(AgeRoundingCriteria.ToUpper() == "FLOOR")
				return  Math.Floor((TotalMillisecondsNew-TotalMillisecondsPre)/ msPerDay) ;
			else if(AgeRoundingCriteria.ToUpper() == "CEIL")
				return  Math.Ceiling((TotalMillisecondsNew-TotalMillisecondsPre)/ msPerDay) ;
			else if(AgeRoundingCriteria.ToUpper() == "ACTUAL")
                return  Math.Round((TotalMillisecondsNew-TotalMillisecondsPre)/ msPerDay) ;
			else
				return Math.Round((TotalMillisecondsNew-TotalMillisecondsPre)/ msPerDay) ;
		}

		public static bool CheckAge(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT NP2_AGEPREM FROM LNP2_POLICYMASTR WHERE NP1_PROPOSAL=?";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			object objPreviousAge = myCommand.ExecuteScalar();
            int CurrentAge=CalculateAge(NP1_PROPOSAL);
			int PreviousAge=Convert.ToInt16(objPreviousAge);
            myCommand.Dispose();
			if(CurrentAge==PreviousAge)
			{
				return true;
			}
			else
			{
			    return false;
			}
		
		}
		

		public static Boolean PolicyApproved(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery = " SELECT NP1_SELECTED FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL=?";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			object objPolicy = myCommand.ExecuteScalar();
            myCommand.Dispose();
			if(objPolicy==null)
			{
				return false;
			}
			else
			{
				if(System.Convert.ToString(objPolicy) == "")
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public static Boolean PolicyValidated(string NP1_PROPOSAL)
		{
			//</method-signature><method-body>
			String strQuery="select np2_substandar from lnp2_policymastr np2 "+ 
							" inner join lnp1_policymastr np1 "+
							" on np1.np1_proposal=np2.np1_proposal "+
							" and np1.np1_proposal=? and np1.np1_selected is null";

			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, 12, NP1_PROPOSAL));
			object objPolicy = myCommand.ExecuteScalar();
            myCommand.Dispose();
			if(objPolicy==null)
			{
				return false;
			}
			else
			{
				if(System.Convert.ToString(objPolicy) == "")
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public static Boolean PolicyReferred(string nph_code)
		{
			String strQuery="select * from lnp2_policymastr l where l.np1_proposal in (select u.np1_proposal from lnu1_underwriti u where u.nph_code=?) and l.np2_substandar ='Y'";

			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NPH_CODE",DbType.String, 12, nph_code));
			object objPolicy = myCommand.ExecuteScalar();
            myCommand.Dispose();
			if(objPolicy==null)
			{
				return false;
			}
			else
			{
				if(System.Convert.ToString(objPolicy) == "")
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}


	//========New Method 08-Feb-2017
		public static string UpdateCommDate_New(string CommDate, string Proposal)
		{
//			string strQuery="UPDATE LNP1_POLICYMASTR SET NP2_COMMENDATE = '"+CommDate+"' WHERE NP1_PROPOSAL='"+Proposal+"'";
//			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
//			myCommand.ExecuteNonQuery(); 


			string strQuery="Update lnp1_policymastr set np2_commendate=? where np1_proposal = '"+Proposal+"'";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@np2_commendate",DbType.Date, CommDate));
			myCommand.ExecuteNonQuery(); 

			string strQuery2="Update lnp2_policymastr set np2_commendate=? where np1_proposal = '"+Proposal+"'";
			IDbCommand myCommand2 = DB.CreateCommand(strQuery2, DB.Connection);
			myCommand2.Parameters.Add(DB.CreateParameter("@np2_commendate",DbType.Date, CommDate));
			myCommand2.ExecuteNonQuery();
            myCommand.Dispose();
            myCommand2.Dispose();
			return "True";
		}
	
		public static string GetCommDate(string Proposal)
		{
			string strQuery= "select NP2_COMMENDATE FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL = ? ";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
			myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL",DbType.String, Proposal));			
			Object CommDate = myCommand.ExecuteScalar();
            myCommand.Dispose();
			return CommDate.ToString();


		
		}
        public static string GetProposalStatus(string strQuery)
        {
            try
            {
                IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
                //myCommand.Parameters.Add(DB.CreateParameter("@NP1_PROPOSAL", DbType.String, Proposal));
                Object status = myCommand.ExecuteScalar();
                myCommand.Dispose();
                return status.ToString();

            }
            catch (Exception)
            {
                return "";
            }
   

        }
	
	}
}
