using System;
using System.Data;
using System.Collections;
using SHMA.Enterprise.Shared;
using System.Web;
using System.Web.UI;
using Thycotic.Web.RemoteScripting;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Presentation;

namespace SHAB.Presentation {
	public partial class GetComboData : Thycotic.Web.RemoteScripting.RSPage{
		[RemoteScriptingMethod]		
		public Array filterChildCombo(string str_Qry){			
			ArrayList comboArray =new ArrayList();
			str_Qry = str_Qry.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator()) ; 
			str_Qry = str_Qry.Replace("_Plus","+");
			str_Qry = EnvHelper.Parse(str_Qry);  
			try{
				IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(str_Qry));
				IDataReader rs_ComboArray = myCommand.ExecuteReader();
				string str_TempValue=null;
				if (rs_ComboArray != null){
					while (rs_ComboArray.Read()){
						str_TempValue="";
						for (int i = 1; i <= rs_ComboArray.GetSchemaTable().Rows.Count; i++)
							str_TempValue += rs_ComboArray[(i - 1)].ToString() + "~";
						str_TempValue = str_TempValue.EndsWith("~")?str_TempValue.Substring(0, (str_TempValue.Length - 1)):str_TempValue;
						comboArray.Add(str_TempValue);
					}
					rs_ComboArray.Close();
				}
				
			}
			catch (System.Exception e){return null;}
			finally{
			   DB.DisConnect();
			}
			return comboArray.ToArray();
		}

		[RemoteScriptingMethod]
		public string SetSessionValues(string fieldValueComb){									
			string[] fieldValueCombArr = fieldValueComb.Split('~');
			string[] fieldComb;
			string[] valueComb;
			if (fieldValueCombArr.Length==2){
				fieldComb = fieldValueCombArr[0].Split(',');
				valueComb = fieldValueCombArr[1].Split(',');		
				for (int i = 0; i<fieldComb.Length; i++){
					SessionObject.Set(fieldComb[i], valueComb[i]);
				}
				return "";
			}
			else{
				return "No value to select.";
			}	
		}


		[RemoteScriptingMethod]
		public string RemoveSessionValues(string fieldValueComb)
		{									
			string[] fieldValueCombArr = fieldValueComb.Split('|');
			string[] fieldComb;
			string[] valueComb;
			string[] overvalue;

			if (fieldValueCombArr.Length==3)
			{
				fieldComb = fieldValueCombArr[0].Split(',');
				valueComb = fieldValueCombArr[1].Split(',');
				overvalue = fieldValueCombArr[2].Split(',');
		
				for (int i = 0; i<fieldComb.Length; i++)
				{
					String itemvalue = ""+SessionObject.Get(fieldComb[i]);
					itemvalue = itemvalue.Replace(","+valueComb[i],overvalue[i]).Replace(valueComb[i]+",",overvalue[i]).Replace(valueComb[i],overvalue[i]);
					SessionObject.Set(fieldComb[i],itemvalue);
				}
				return "";
			}
			else
			{
				return "No value to select.";
			}	
		}


		[RemoteScriptingMethod]
		public string SetFixedSessionValues(string fieldValue)
		{									
			string[] fieldValueArr = fieldValue.Split('&');
			string[] fieldVal;

			for (int i=0;i < fieldValueArr.Length ; i++)
			{
				fieldVal = fieldValueArr[i].Split('=');
				string Key = fieldVal[0];
				string Val = fieldVal[1];
				if ((Val != null) && (Key != null))  
				{
					if (Key != "") 
					{ 
						SessionObject.Set(Key.Replace("r_","").Trim(),Val.Trim()); 
					}
				}
			}
			return "";
		}	

		[RemoteScriptingMethod]
		public Array FetchData(string query){			
			System.Globalization.Calendar Clnd ;  
			ArrayList comboArray =new ArrayList();
			string str_TempValue=null;
			try{
				query = query.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator()) ; 
				query = EnvHelper.Parse(query);  
				IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query));
				IDataReader reader = myCommand.ExecuteReader();
				DataTable schema = reader.GetSchemaTable();				
				if (reader.Read()){
					for (int i = 0; i<schema.Rows.Count; i++){
						str_TempValue = schema.Rows[i][0].ToString() + '~' + reader.GetValue(i).ToString() ;
						comboArray.Add(str_TempValue);
					}				
				}
			}
			catch (System.Exception e){}
			finally{
				DB.DisConnect();
			}
			
			return comboArray.ToArray();
		}

		private const int COMBO_PAGE_SIZE = 20;

		[RemoteScriptingMethod]		
		public Array GetComboArray(string str_Qry)
		{ 
			int remainder=0;
			ArrayList codeArray =new ArrayList();
			ArrayList descArray =new ArrayList();
			ArrayList infoArray =new ArrayList();
			int startPage = Convert.ToInt16(str_Qry.Substring(str_Qry.LastIndexOf("~~")+2));
			str_Qry = str_Qry.Substring(0,str_Qry.LastIndexOf("~~"));
			str_Qry = str_Qry.Replace("_Concat", "+") ;
			str_Qry = EnvHelper.Parse(str_Qry);
			bool next = false;

			if(startPage<=0)startPage=1;
			try 
			{ 
				DataTable table =new DataTable();
				DB.CreateDataAdapter(str_Qry).Fill(table);

				//IDataReader rs_ComboArray = DB.CreateCommand(str_Qry).ExecuteReader() ;
				string comboDesc= "" ;
				int totalPages=0;
				if (table != null)
				{
					
					totalPages = table.Rows.Count / COMBO_PAGE_SIZE ; // COMBO_PAGE_SIZE;
					remainder = table.Rows.Count % COMBO_PAGE_SIZE;
					if (remainder >0){
						totalPages++;
					}

					if (startPage>totalPages && totalPages!=0) {startPage = totalPages;next=false;} 
					
					//Changed by ahmed 23062006 for paging
					//else if(totalPages >0) {next=true;} 
					else if(totalPages >1) {next=true;} 
					
					else if(totalPages==0 && startPage>1){startPage=1; totalPages=1;}

					//int startPosition = ((startPage-1) * COMBO_PAGE_SIZE)==0?0:((startPage-1) * COMBO_PAGE_SIZE)-1;
					//rs_ComboArray.absolute(((startPage-1) * COMBO_PAGE_SIZE));
					int ii = ((startPage-1)* COMBO_PAGE_SIZE) ;
					int pageSize ;
					if (table.Rows.Count < COMBO_PAGE_SIZE) 
						pageSize = table.Rows.Count;
					else
						pageSize = COMBO_PAGE_SIZE;
					
					int jj ;
					
					//Changed by ahmed 23062006 for paging
					//if (totalPages == startPage)					
					if (totalPages == startPage && remainder >0)
					
						jj = ii + remainder;
					else
						jj = ii + pageSize;
					for ( ; ii < jj ; ii++ ) 
					{
						codeArray.Add( table.Rows[ii][0]!=null? table.Rows[ii][0].ToString():""); 
						if (table.Columns.Count>1)
						{
							comboDesc = table.Rows[ii][1] != null?table.Rows[ii][1].ToString().Replace("\"",""):""; 
							comboDesc = comboDesc.Replace("'","");
						}
						else
							comboDesc = "";
						descArray.Add(comboDesc);
					}

					// while(rs_ComboArray.next() && recordCount < COMBO_PAGE_SIZE) 
					// {
					// recordCount++;
					// codeArray.Add(rs_ComboArray.getObject(1)!=null?rs_ComboArray.getObject(1).ToString():"");
					// comboDesc = rs_ComboArray.getObject(2)!=null?rs_ComboArray.getObject(2).ToString().Replace("\"",""):"";
					// comboDesc = comboDesc.Replace("'","");
					// descArray.Add(comboDesc);
					// }
					// rs_ComboArray.close();
				}

				if(next) 
				{
					infoArray.Add(startPage);
					infoArray.Add(startPage+1);
					infoArray.Add("1");
					infoArray.Add(totalPages);
				}
				else 
				{
					infoArray.Add(startPage);
					infoArray.Add(startPage);
					infoArray.Add("0");
					infoArray.Add(totalPages);
				} 
			}
			catch (System.Exception e){return null;}
			finally 
			{
				DB.DisConnect();
			}
			return new Array[]{codeArray.ToArray(), descArray.ToArray(), infoArray.ToArray()} ;
		} 
 	
		
		[RemoteScriptingMethod]		
		public string GetComboDescription(string str_Qry) 
		{			
			str_Qry = EnvHelper.Parse(str_Qry.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator())) ; 
			object comboDesciption = null;
			try 
			{
				IDbCommand myCommand = DB.CreateCommand(str_Qry);
				comboDesciption = myCommand.ExecuteScalar();
			}
			catch (System.Exception e){return null;}
			finally 
			{
				DB.DisConnect();
			}
			return comboDesciption.ToString();
		}	

		[RemoteScriptingMethod]
		public Array FetchDataArray(string query)
		{ 
			//System.Globalization.Calendar Clnd ; 
			ArrayList fetchInner ;
			ArrayList fetchOuter =new ArrayList();
			string str_TempValue=null;
			try
			{
				query = query.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator()) ; 
				query = EnvHelper.Parse(query); 
				IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query));
				IDataReader reader = myCommand.ExecuteReader();
				DataTable schema = reader.GetSchemaTable(); 
				fetchInner = new ArrayList();

				for (int i = 0; i<schema.Rows.Count; i++)
				{
					str_TempValue = schema.Rows[i][0].ToString().ToUpper();
					fetchInner.Add(str_TempValue);
				}

				fetchOuter.Add(fetchInner.ToArray());

				while (reader.Read())
				{
					fetchInner = new ArrayList();
					for (int i = 0; i<schema.Rows.Count; i++)
					{
						str_TempValue = reader.GetValue(i).ToString();
						fetchInner.Add(str_TempValue);
					}
					//Parse_Errors(fetchInner.ToArray());
					fetchOuter.Add(fetchInner.ToArray());
				}
				reader.Close();
			}
			catch (System.Exception e){}
			finally
			{
				DB.DisConnect();
			} 
			
			return fetchOuter.ToArray();
		}

		[RemoteScriptingMethod]
		public Object executeClass(String _param) 
		{
			String[] param=_param.Split(',');
			String methodName = param[1];
			String className = param[0];

			//Checking either Method is allowed or not
			//string methodList = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AllowedMethods"]).ToUpper();
			//if(methodList.IndexOf(methodName.ToUpper()) < 0)
			{
			//	return null;
			}

			Object[] argument=null;
			Type[] parameter=null;
			if(param.Length>2)
			{
				parameter=new Type[param.Length-2];
				argument=new Object[param.Length-2];
				for(int i=2; i<param.Length;i++)
				{
					parameter[i-2]=param[i].GetType();
					argument[i-2] = (object)(param[i]);
				}
			}
			try
			{
				SHMA.Enterprise.ClassLoader cloader = new ClassLoader();
				object loadedClass = cloader.loadType(className);
				if(loadedClass!=null)
				{
					System.Reflection.MemberInfo methodToExecute=null;
					if(parameter==null)
						methodToExecute = loadedClass.GetType().GetMethod(methodName);
					else
						methodToExecute = loadedClass.GetType().GetMethod(methodName,parameter);
					if(methodToExecute!=null)
					{
						object retrnValue = loadedClass.GetType().InvokeMember(methodToExecute.Name, System.Reflection.BindingFlags.Default|System.Reflection.BindingFlags.InvokeMethod, null, loadedClass,argument);
						return retrnValue;
					}
					else
						return null;
				}
			}
			catch(Exception e)
			{
				return e.Message;
			}
			return null;
		}

//		public void Parse_Errors(Array arr)
//		{
//			//String str = @"F:\Letters";
//			String[] str_arr = {@"\","\b"};
//			String temp = null;
//			for(int i=0;i<arr.Length;i++)
//			{
//				for(int j=0;j<str_arr.Length;j++)
//				{
//					int index = arr.GetValue(i).ToString().IndexOf(str_arr[j]);
//
//					if(index!=-1)
//					{
//					    temp = arr.GetValue(i).ToString();
//
//						temp.Replace(str_arr[j],"\\");
//						temp.Replace(@"E:",@"");
//						arr.SetValue(temp,i);
//						
//					
//					}
//				}
//			
//			}
//			
//			
////			for(int i=0;i<arr.Length;i++)
////			{
////					for(int h=0;h<str_arr.Length;h++)
////					{
////						if(arr.GetValue(i).ToString().IndexOf(str_arr[h].ToString())!=-1)
////						{
////							if(arr.GetValue(i).ToString().IndexOf(str_arr[0])!=-1)
////							{
////								arr.SetValue(arr.GetValue(i).ToString().Replace(@"\","\\"),i);
////							}
////							else if(arr.GetValue(i).ToString().IndexOf(str_arr[1])!=-1)
////							{
////						     //arr.GetValue(i).ToString().Replace(str_arr[0],"\\");
////							}
////					
////						}
////					}
////						  
////				
////			}
//			//return arr;
//		
//		}



/*		[RemoteScriptingMethod]
		public string[,] FetchData(string query){			
			string[,] fieldValue = null;
			try{
				IDbCommand myCommand = DB.CreateCommand(EnvHelper.Parse(query));
				IDataReader reader = myCommand.ExecuteReader();
				DataTable schema = reader.GetSchemaTable();				
				fieldValue = new string[2,schema.Rows.Count];
				if (reader.Read()){
					for (int i = 0; i<schema.Rows.Count; i++){
						fieldValue[0,i] = schema.Rows[i][0].ToString();
						fieldValue[1,i] = reader.GetValue(i).ToString();						
					}				
				}
			}
			catch (System.Exception e){}
			finally{
				DB.DisConnect();
			}
			
			return fieldValue;
		}
*/
		//		[RemoteScriptingMethod]
//		public Array filterChildComboWithQEI(string args){			
//			string query =args[0];
//			string extraInfo=args[1].Trim();
//			if (extraInfo.ToUpper().IndexOf("WHERE")==0)
//				extraInfo = extraInfo.Remove(0,5);
//			if (extraInfo.ToUpper().IndexOf("AND")==0)
//				extraInfo = extraInfo.Remove(0,3);
//
//			if (query.ToUpper().IndexOf("WHERE")<0)
//				query+=" WHERE " + extraInfo;
//			else
//				query+=" AND " + extraInfo;
//			ArrayList comboArray =new ArrayList();
//			try{
//				IDbCommand myCommand = DB.CreateCommand(query);
//				IDataReader rs_ComboArray = myCommand.ExecuteReader();
//				string str_TempValue=null;
//				if (rs_ComboArray != null){
//					int int_Index = 0;
//					while (rs_ComboArray.Read()){
//						str_TempValue="";
//						for (int i = 1; i <= rs_ComboArray.GetSchemaTable().Rows.Count; i++)
//							str_TempValue += rs_ComboArray[(i - 1)].ToString() + "~";
//						str_TempValue = str_TempValue.EndsWith("~")?str_TempValue.Substring(0, (str_TempValue.Length - 1)):str_TempValue;
//						comboArray.Add(str_TempValue);
//					}
//					rs_ComboArray.Close();
//				}
//				return comboArray.ToArray();
//			}
//			catch (System.Exception e){return null;}
//		}
	}
}
