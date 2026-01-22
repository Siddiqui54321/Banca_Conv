///<summary>
///
///
///</summary>

/*=================	Made by Farrukh in java	 =====================
===================	converted by ahmed in c# =====================
===================	modified by Shahid       ====================*/

using System;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Shared;

namespace SHMA.Enterprise.Shared
{
	public class RunTimeLibrary
	{		
		System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
		System.Web.HttpApplicationState application = System.Web.HttpContext.Current.Application;
		//		virtual public System.Web.HttpRequest setRequestValuesInSession{
		//			set{
		//				//System.Collections.IEnumerator en =  SupportClass.GetParameterNames(value.QueryString.GetEnumerator(), value.Form.GetEnumerator());
		//				
		//				System.Collections.IEnumerator en = SupportClass.GetParameterNames(value.QueryString.GetEnumerator(), value.Form.GetEnumerator());
		//				System.String str_RequestObject = "";
		//
		//				while (en.MoveNext()){
		//					str_RequestObject = ((System.String) en.Current);
		//					if (str_RequestObject.StartsWith("r_")){
		//						SessionObject.Add(str_RequestObject.Substring(2), value[str_RequestObject]);
		//					}
		//					if (str_RequestObject.StartsWith("g_")){
		//						SessionObject.Add(str_RequestObject.Substring(2), System.Web.HttpContext.Current.Application.Get(value[str_RequestObject]));
		//					}
		//				}
		//			}			
		//		}
		bool loadGlobalFromSession = false;
		public RunTimeLibrary() 
		{
			if((System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"]!=null)
				&& (System.Configuration.ConfigurationSettings.AppSettings["GlobalVariable"]=="Session"))
				loadGlobalFromSession =true;
		}
		//		public virtual System.String setDate(System.String str_Date, System.Web.HttpRequest request){
		//			if (Session["SHGNDataAccess"] == null)
		//			{
		////				DB = new SHGNDataAccess();
		////				Session.Add("SHGNDataAccess", DB);
		//			}
		//			else
		////				DB = (SHGNDataAccess) Session["SHGNDataAccess"];
		//			return DB.setDate(str_Date);
		//		}
		public virtual string replaceRequestSessionApplicationValues(System.String str_Data)
		{
			str_Data = replaceSessionValues(str_Data);
			str_Data = replaceSessionNumericValues(str_Data);
			str_Data = replaceSessionDateValues(str_Data);
			str_Data = replaceApplicationValues(str_Data);
			str_Data = replaceApplicationNumericValues(str_Data);
			str_Data = replaceApplicationDateValues(str_Data);
			str_Data = replaceRequestValues(str_Data);
			str_Data = replaceRequestNumericValues(str_Data);
			str_Data = replaceRequestDateValues(str_Data);
			//str_Data = replaceSessionValueWithQuotes(str_Data);
			//str_Data = replaceApplicationValueWithQuotes(str_Data);

			return str_Data;
		}
		public virtual string replaceSessionValues(System.String str_Data)
		{
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("SV(\"");
			System.String str_Item = "";
			while (got != - 1)
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got)
				{
					str_Item = sb.ToString(got + 4, indexOfQuote-(got+4));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 6) - got), (System.String) (SessionObject.Get(str_Item) == null?"''":("'" + SessionObject.Get(str_Item) + "'")), got, (got + str_Item.Length + 6) - got);
				got = sb.ToString().IndexOf("SV(\"", got + 1);
			}
			return sb.ToString();
		}
		public virtual System.String replaceSessionNumericValues(System.String str_Data) 
		{
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("SVN(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
                sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (SessionObject.Get(str_Item) == null || SessionObject.Get(str_Item) == "" ? "-1" : (SessionObject.GetString(str_Item))), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("SVN(\"", got + 1);
			}
			return sb.ToString();
		}

		public virtual System.String replaceSessionDateValues(string str_Data)
		{
			/*if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("SVD(\"");
			System.String str_Item = "";
			while (got != - 1){
				str_Item = sb.ToString(got + 5, sb.ToString().IndexOf("\")", got + 1));
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), "'" + DateTime.Parse(SessionObject.Get(str_Item).ToString()));



				got = sb.ToString().IndexOf("SVD(\"", got + 1);
			}
			return sb.ToString();
			*/
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("SVD(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (SessionObject.GetString(str_Item) == null?"''":("'" +  SessionObject.GetString(str_Item) + "'")), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("SVD(\"", got + 1);
			}
			return sb.ToString();
		}

		public virtual System.String replaceApplicationValues(System.String str_Data)
		{
			/* Coment By rehan to aplly cnhages made by Shahid Siddiqi for session values
				if ((System.Object) str_Data == null || str_Data.Equals("null"))
							return "";
						System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
						int got = sb.ToString().IndexOf("AV(\"");
						System.String str_Item = "";
						while (got != - 1)
						{
							str_Item = sb.ToString(got + 4, (sb.ToString().IndexOf("\")", got + 1)) );
							sb.Replace(sb.ToString(got, (got + str_Item.Length + 6) - got), (System.String) (System.Web.HttpContext.Current.Application[str_Item] == null?"''":("'" + System.Web.HttpContext.Current.Application.Get(str_Item) + "'")), got, (got + str_Item.Length + 6) - got);
							got = sb.ToString().IndexOf("AV(\"", got + 1);
						}
						return sb.ToString();
			*/
			//System.out.println("Start Query is: "+str_Data);
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AV(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1101"'
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 4, indexOfQuote-(got+4));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				if(loadGlobalFromSession)
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 6) - got), (System.String) (SessionObject.GetString(str_Item) == null?"''":("'" + SessionObject.GetString(str_Item) + "'")), got, (got + str_Item.Length + 6) - got);
				else
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 6) - got), (System.String) (System.Web.HttpContext.Current.Application[str_Item] == null?"''":("'" + System.Web.HttpContext.Current.Application[str_Item] + "'")), got, (got + str_Item.Length + 6) - got);
				got = sb.ToString().IndexOf("AV(\"", got + 1);
			}
			return sb.ToString();

		}
		public virtual System.String replaceApplicationNumericValues(System.String str_Data)
		{
			/*
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AVN(\"");
			System.String str_Item = "";
			while (got != - 1)
			{
				str_Item = sb.ToString(got + 5, sb.ToString().IndexOf("\")", got + 1));
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (System.Web.HttpContext.Current.Application.Get(str_Item) == null || ((System.String) System.Web.HttpContext.Current.Application.Get(str_Item)).Equals("")?"0":System.Web.HttpContext.Current.Application.Get(str_Item)), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("AVN(\"", got + 1);
			}
			return sb.ToString();

*/
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AVN(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				if(loadGlobalFromSession)
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (SessionObject.GetString(str_Item) == null?"''":( SessionObject.GetString(str_Item) )), got, (got + str_Item.Length + 7) - got);
				else
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (System.Web.HttpContext.Current.Application[str_Item] == null?"''":( System.Web.HttpContext.Current.Application[str_Item] )), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("AVN(\"", got + 1);
			}
			return sb.ToString();
		}
		public virtual System.String replaceApplicationDateValues(System.String str_Data)
		{
			/*
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AVD(\"");
			System.String str_Item = "";
			while (got != - 1){
				str_Item = sb.ToString(got + 5, sb.ToString().IndexOf("\")", got + 1));
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), "'" + DateTime.Parse(application[str_Item].ToString()).ToShortTimeString() + "'", got, (got + str_Item.Length + 7) - got);
 
				got = sb.ToString().IndexOf("AVD(\"", got + 1);
			}
			return sb.ToString();
			*/
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AVD(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				if(loadGlobalFromSession)
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (SessionObject.GetString(str_Item) == null?"''":("'" + SessionObject.GetString(str_Item) + "'")), got, (got + str_Item.Length + 7) - got);
				else
					sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (System.Web.HttpContext.Current.Application[str_Item] == null?"''":("'" + System.Web.HttpContext.Current.Application[str_Item] + "'")), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("AVD(\"", got + 1);
			}
			return sb.ToString();
		}
		public virtual System.String replaceRequestValues(System.String str_Data)
		{
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("RV(\"");
			System.String str_Item = "";
			while (got != - 1) 
			{
				str_Item = sb.ToString(got + 4, sb.ToString().IndexOf("\")", got + 1));
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 6) - got), (System.String) (request[str_Item] == null?"''":("'" + request[str_Item] + "'")), got, (got + str_Item.Length + 6) - got);
				got = sb.ToString().IndexOf("RV(\"", got + 1);
			}
			//System.out.println("After RV is: "+sb.toString());
			return sb.ToString();
		}
		public virtual System.String replaceRequestNumericValues(System.String str_Data)
		{
			//System.out.println("Before RV is: "+str_Data);
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("RVN(\"");
			System.String str_Item = "", str_Value = "";
			while (got != - 1) 
			{
				str_Item = sb.ToString(got + 5, sb.ToString().IndexOf("\")", got + 1));
				str_Value = (request[str_Item] == null || ((System.String) request[str_Item]).Equals("")?"0":request[str_Item].ToString());
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), str_Value, got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("RVN(\"", got + 1);
			}
			return sb.ToString();
		}
		public virtual System.String replaceRequestDateValues(System.String str_Data)
		{
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("RVD(\"");
			System.String str_Item = "", str_Value = "";
			while (got != - 1)
			{
				str_Item = sb.ToString(got + 5, sb.ToString().IndexOf("\")", got + 1));
				str_Value = "'" + DateTime.Parse(request[str_Item]);
					
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), str_Value, got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("RVD(\"", got + 1);
			}
			return sb.ToString();
		}
		//		public virtual System.String getTokens(System.String str_EntityId, System.Web.HttpRequest request){
		//			try{
		//				System.String str_Tokens = "";
		//				if (Session["SHGNDataAccess"] == null) {
		//					//					DB = new SHGNDataAccess();
		//					//					Session.Add("SHGNDataAccess", DB);
		//				}
		//				else{}
		//					//DB = (SHGNDataAccess) Session["SHGNDataAccess"];
		////				System.String str_Qry = " Select NVL(A.PKL_LVLWIDTH,0) from PR_ED_KL_KEYLEVELS A, PR_ED_SE_SYSTEMENTITY B WHERE A.PSE_ENTITYID='" + str_EntityId + "' AND A.PSE_ENTITYID=B.PSE_ENTITYID ";
		//				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
		//				str_Qry += (" AND ((NVL(B.PSE_ORGANIZATIONBASEDLEVEL,'N')='Y' AND A.POR_ORGACODE='" + Session["s_POR_ORGACODE"] + "') OR (NVL(B.PSE_ORGANIZATIONBASEDLEVEL,'N')<>'Y')) order by A.PKL_LVLNO");
		//				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		//				System.Data.OleDb.OleDbDataReader rset = DB.fsexecuteQuery(str_Qry);
		//				if (rset != null)
		//				{
		//					while (rset.Read())
		//					{
		//						str_Tokens += (System.Convert.ToString(rset[1 - 1]) + ",");
		//					}
		//					if (str_Tokens.Length > 1)
		//					{
		//						str_Tokens = str_Tokens.Substring(0, (str_Tokens.Length - 1) - (0));
		//					}
		//				}
		//				return str_Tokens;
		//			}
		//			catch (System.Exception e){				
		//			}
		//			return null;
		//		}
		public static System.String replace(System.String s, System.String f, System.String r) 
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(s);
			int got = sb.ToString().IndexOf(f);
			while (got != - 1) 
			{
				sb.Replace(sb.ToString(got, got + f.Length - got), r, got, got + f.Length - got);
				got = sb.ToString().IndexOf(f, got + 1);
			}
			return sb.ToString();
		}
		public virtual System.String replaceSessionValueWithQuotes(System.String str_Data) 
		{
			//replace session values with quotes if quotes not exist in session value
			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("SVS(\"");
			System.String str_Item = "";
			System.String SessionValue = "";

			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				
				SessionValue = "";
				if (SessionObject.Get(str_Item) != null && SessionObject.Get(str_Item).ToString().Length > 0)
				{
					SessionValue = SessionObject.GetString(str_Item);
					if (SessionValue.IndexOf("'",0) != 0) 
					{
						SessionValue = "'" + SessionValue ;
					}
					if (SessionValue.LastIndexOf("'") != SessionValue.Length-1) 
					{
						SessionValue = SessionValue + "'";
					}
				}
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (SessionObject.Get(str_Item) == null?"''":(  SessionValue  )), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("SVS(\"", got + 1);
			}
			return sb.ToString();
		}

		public virtual System.String replaceApplicationValueWithQuotes(System.String str_Data)
		{

			if ((System.Object) str_Data == null || str_Data.Equals("null"))
				return "";
			System.Text.StringBuilder sb = new System.Text.StringBuilder(str_Data);
			int got = sb.ToString().IndexOf("AVS(\"");
			System.String str_Item = "";
			System.String SessionValue = "";

			while (got != - 1) 
			{
				int indexOfQuote = sb.ToString().IndexOf( "\")", got + 1);
				if (indexOfQuote > got) 
				{
					str_Item = sb.ToString(got + 5, indexOfQuote-(got+5));
				}
				else 
				{
					throw (new ApplicationException("improper Session place holder"));
				}
				if (SessionObject.Get(str_Item) != null && SessionObject.Get(str_Item).ToString().Length > 0)
				{
					SessionValue = SessionObject.GetString(str_Item);
					if (SessionValue.IndexOf("'",0) != 0) 
					{
						SessionValue = "'" + SessionValue ;
					}
					if (SessionValue.LastIndexOf("'") != SessionValue.Length-1) 
					{
						SessionValue = SessionValue + "'";
					}
				}
				sb.Replace(sb.ToString(got, (got + str_Item.Length + 7) - got), (System.String) (SessionObject.Get(str_Item) == null?"''":( SessionValue )), got, (got + str_Item.Length + 7) - got);
				got = sb.ToString().IndexOf("AVS(\"", got + 1);
			}
			return sb.ToString();
		}





		//		public virtual System.String getDBHeading(System.String strQry, System.Web.HttpRequest request)
		//		{
		//			try
		//			{
		//				if (Session["SHGNDataAccess"] == null) {
		//					//					DB = new SHGNDataAccess();
		//					//					Session.Add("SHGNDataAccess", DB);
		//				}
		//				else{}
		////					DB = (SHGNDataAccess) Session["SHGNDataAccess"];
		//				//String strQry = " SELECT "+getDescCol(5)+" FROM "+sh_Properties.rs_Data.getString(4)+getCondition();
		//				//System.out.println(strQry);
		//				//UPGRADE_TODO: Interface 'java.sql.ResultSet' was converted to 'System.Data.OleDb.OleDbDataReader' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javasqlResultSet"'
		//				System.Data.OleDb.OleDbDataReader lrset = DB.fsexecuteQuery(strQry);
		//				System.String result = "";
		//				if (lrset != null)
		//				{
		//					if (lrset.Read())
		//					{
		//						result = System.Convert.ToString(lrset[1 - 1]);
		//					}
		//					lrset.Close();
		//					//UPGRADE_ISSUE: Method 'java.sql.Statement.close' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000_javasqlStatementclose"'
		//					//UPGRADE_ISSUE: Method 'java.sql.ResultSet.getStatement' was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1000"'
		//					//lrset.getStatement().close();
		//				}
		//				return result;
		//			}
		//			catch (System.Exception e)
		//			{
		//				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
		//				//System.Console.Out.WriteLine("Exception in getDBHeading(): " + e.Message);
		//				SupportClass.WriteStackTrace(e, Console.Error);
		//			}
		//			return "";
		//			
		//		}
	}
}


