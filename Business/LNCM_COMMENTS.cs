using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.CodeVision.Data; 
using SHMA.Enterprise.Business; 
using SHMA.Enterprise.Presentation;
using SHAB.Data; 





namespace SHAB.Business
{
	public class LNCM_COMMENTS : SHMA.CodeVision.Business.BusinessClassBase
	{
		public LNCM_COMMENTS(DataHolder dh) : base(dh, "LNCM_COMMENTS")
		{		
		}
		
		public static string[] PrimaryKeys
		{
			get{ return new string[]{"NP1_PROPOSAL", "CM_SERIAL_NO"}; }}

		public sealed override string[] GetPrimaryKeys()
		{
			return PrimaryKeys;
		}

		protected sealed override DataClassBase dataObject
		{
			get{ return new LNCM_COMMENTSDB(dataHolder); }
		}
		
		public override void Add(NameValueCollection columnNameValue, NameValueCollection allFields, string EntityID) 
		{
			base.Add(columnNameValue, allFields, EntityID);
		}
		
		public override void Add(NameValueCollection columnNameValue, string fieldCombination, string valueCombination)
		{   			         
			base.Add(columnNameValue, fieldCombination, valueCombination);			
		}
		public void AddComments(string proposal,string comments,string status)
		{
			this.Add(getNameValueCollection(proposal,comments,status),"");
		}
		public static void AddComentsInTable1(string proposal,string comments,string status)
		{
				
			LNCM_COMMENTSDB.addComments(getParameterCollection(proposal,comments, status));

		}
		public void AddComentsInTable(string proposal,string comments,string status)
		{
				
			LNCM_COMMENTSDB.addComments(getParameterCollection(proposal,comments, status));

		}
		private NameValueCollection getNameValueCollection(string proposal,string comments,string status)
		{
			NameValueCollection nvc = new NameValueCollection();
			nvc.add("NP1_PROPOSAL",proposal);
			nvc.add("CM_SERIAL_NO",LNCM_COMMENTSDB.getNextSerial(proposal).ToString());
            nvc.add("CM_COMMENTDATE", DateTime.Now);
            //nvc.add("CM_COMMENTDATE", SessionObject.GetString("ClossingDate"));	//chg_closing comments above
			nvc.add("CM_COMMENTBY",SessionObject.GetString("s_USE_USERID"));
			nvc.add("CM_ACTION",status);
			nvc.add("CM_COMMENTS",comments);
			return nvc;
		}
		private static SHMA.Enterprise.Data.ParameterCollection getParameterCollection(string proposal,string comments,string status)
		{
			SHMA.Enterprise.Data.ParameterCollection pc = new SHMA.Enterprise.Data.ParameterCollection();
			pc.puts("@NP1_PROPOSAL",proposal,SHMA.Enterprise.Data.Types.VARCHAR);
			pc.puts("@CM_SERIAL_NO",LNCM_COMMENTSDB.getNextSerial(proposal).ToString(),SHMA.Enterprise.Data.Types.VARCHAR);
            pc.puts("@CM_COMMENTDATE", DateTime.Now, SHMA.Enterprise.Data.Types.TIMESTAMP);
            //pc.puts("@CM_COMMENTDATE", SessionObject.Get("ClossingDate"), SHMA.Enterprise.Data.Types.TIMESTAMP);	//chg_closing comments above
			pc.puts("@CM_COMMENTBY",SessionObject.GetString("s_USE_USERID"),SHMA.Enterprise.Data.Types.VARCHAR);
			pc.puts("@CM_ACTION",status,SHMA.Enterprise.Data.Types.VARCHAR);
			pc.puts("@CM_COMMENTS",comments,SHMA.Enterprise.Data.Types.VARCHAR);
			return pc;
		}

//		public DataHolder GetComments(string Proposal,DataHolder dataHolder)
//		{
//			//IDataReader temp = LNCM_COMMENTSDB.getCommentsOfPorposal_New(Proposal);
//			//DataHolder temp = LNCM_COMMENTSDB(dataHolder).getCommentsOfPorposal_New(Proposal);
//			//return temp["LNCM_COMMENTS_DATA"];
//		}
	}
}
