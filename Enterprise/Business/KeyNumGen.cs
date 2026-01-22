using System;
using System.Data;
using SHMA.Enterprise; 
using SHMA.Enterprise.Shared.Security; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Exceptions;
using System.Threading;
using System.Globalization;


namespace SHMA.Enterprise.Business {
	[CLSCompliant(false)]
	public class KeyNumGen : SHMA.Enterprise.TableModule {
		public string fieldId;
		private string newGeneratedNumber=null;
		private double frPeriodNo;
		private string frAcntYear;

		public KeyNumGen(DataHolder dh):base(dh, "KeyNumGen"){
		}
		public KeyNumGen(){
			new KeyNumGen(findDataHolder());
		}

		public static bool IsAuto(string PSE_ENTITYID, NameValueCollection columnNameValue) {
			NumGenInfo info = new NumGenInfo(PSE_ENTITYID, columnNameValue);
			return info.IsAuto;
		}
		private DataHolder findDataHolder(){
			return new DataHolder();
		}

        public delegate void HandleKeys_Delegate(string PSE_ENTITYID, NameValueCollection columnNameValue, string[] Keys);

        public void HandleKeys_Adhoc(string PSE_ENTITYID, NameValueCollection columnNameValue, string[] Keys)
        {
            HandleKeys_Delegate delInstance = new HandleKeys_Delegate(HandleKeys_Thread);
            IAsyncResult a =  delInstance.BeginInvoke( PSE_ENTITYID, columnNameValue, Keys, null,null);

            delInstance.EndInvoke(a);
		
        }

        public void HandleKeys_Thread(string PSE_ENTITYID, NameValueCollection columnNameValue, string[] Keys)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            DB.BeginTransaction();
            try
            {
                //HandleKeys(PSE_ENTITYID, columnNameValue, Keys);
                DB.CommitTransaction();
            }
            catch
            {
                DB.RollbackTransaction();
            }
            finally {
                DB.TransactionEnd();
                DB.DisConnect();
            }
        }

	
		public void HandleKeys(string PSE_ENTITYID, NameValueCollection columnNameValue, string[] Keys){
			string logicalEntity="";
			newGeneratedNumber = null;
			System.Collections.Hashtable logicalEntityData = KeyNumGenDB.GetLogicalEntityId(PSE_ENTITYID);
			if(logicalEntityData["LOGICALENTITYID"].ToString() !=""  )
			{
				PSE_ENTITYID = logicalEntityData["LOGICALENTITYID"].ToString();
			}
			else if(logicalEntityData["LOGICALENTITYFIELD"].ToString() !="")
			{
				logicalEntity = logicalEntityData["LOGICALENTITYFIELD"].ToString();
				if(columnNameValue.ContainsKey(logicalEntity))
					PSE_ENTITYID = columnNameValue[logicalEntity].ToString();
			}

			NumGenInfo NumGen = new NumGenInfo(PSE_ENTITYID,columnNameValue);

			if (NumGen.IsAuto){
				CheckFrequency(NumGen,columnNameValue);
				bool searching=true;
				while (searching){
					string newNumber= GetNewNum(NumGen.LastNo, NumGen.NumberWidth);
					if (newNumber.Length>0){
						newNumber=CheckStopNumber(newNumber, NumGen.LastNo, NumGen.StopNumber, NumGen.StartNumber, NumGen.NumberWidth);
						if (updateLastNo(NumGen,newNumber)){
							columnNameValue[NumGen.KeyColumnName] =	newNumber;
							string expression = "";
							if(NumGen.Expression!=""){
								expression = AddSuffixPrefix(NumGen.KeyColumnName, NumGen.Expression, columnNameValue, columnNameValue);
								columnNameValue[NumGen.KeyColumnName] =	expression;
								newGeneratedNumber = expression;
							}
							searching = false;
						}
						//else{
						//	searching = true;
						//}
					}
				}
			}
			if(columnNameValue.ContainsKey(NumGen.KeyColumnName))
				ValidateKeyNumberColumn(NumGen,columnNameValue);
			ValidatePrimaryKeyColumn(Keys,columnNameValue);

			KeyLevels.CheckKeyLevelsByEntityID(PSE_ENTITYID, columnNameValue);
		}

		public void HandleKeys(string PSE_ENTITYID,NameValueCollection baseTableFields, NameValueCollection columnNameValue, string[] Keys){
			string logicalEntity="";
			newGeneratedNumber = null;
            logicalEntity += " ";
            /*
			System.Collections.Hashtable logicalEntityData = KeyNumGenDB.GetLogicalEntityId(PSE_ENTITYID);
			if(logicalEntityData["LOGICALENTITYID"].ToString() !=""  )
			{
				PSE_ENTITYID = logicalEntityData["LOGICALENTITYID"].ToString();
			}
			else if(logicalEntityData["LOGICALENTITYFIELD"].ToString() !="")
			{
				logicalEntity = logicalEntityData["LOGICALENTITYFIELD"].ToString();
				if(columnNameValue.ContainsKey(logicalEntity))
					PSE_ENTITYID = columnNameValue[logicalEntity].ToString();
			}

			NumGenInfo NumGen = new NumGenInfo(PSE_ENTITYID,columnNameValue);
			if (NumGen.IsAuto){
				bool searching=true;
				CheckFrequency(NumGen,columnNameValue);
				while (searching){
					string newNumber= GetNewNum(NumGen.LastNo, NumGen.NumberWidth);
					if (newNumber.Length>0){
						newNumber=CheckStopNumber(newNumber, NumGen.LastNo, NumGen.StopNumber, NumGen.StartNumber, NumGen.NumberWidth);
						//if (KeyNumGenDB.UpdateLastNo(newNumber, lastNo, NumGen.OrganizationCode,	NumGen.EntityID, NumGen.LevelNo, NumGen.SerialNo)){
						if(updateLastNo(NumGen,newNumber)){
							baseTableFields[NumGen.KeyColumnName] =	newNumber;
							columnNameValue[NumGen.KeyColumnName] =	newNumber;
							newGeneratedNumber = newNumber;
							string expression = "";
							if(NumGen.Expression!="" && NumGen.Expression!=null){
								expression = AddSuffixPrefix(NumGen.KeyColumnName, NumGen.Expression, baseTableFields, columnNameValue);
								baseTableFields[NumGen.KeyColumnName] =	expression;
								columnNameValue[NumGen.KeyColumnName] = expression;
								newGeneratedNumber = expression;
							}
							searching = false;
						}
						//else{
						//	lastNo = KeyNumGenDB.GetNumGenInfoAgain( NumGen.OrganizationCode, NumGen.EntityID, NumGen.LevelNo, NumGen.SerialNo);
						//}
					}											
				}
			}				 
			if(baseTableFields.ContainsKey(NumGen.KeyColumnName))
				ValidateKeyNumberColumn(NumGen,baseTableFields);
			ValidatePrimaryKeyColumn(Keys,baseTableFields);

			KeyLevels.CheckKeyLevelsByEntityID(PSE_ENTITYID, baseTableFields);*/
		}

		public void HandleKeys(string PSE_ENTITYID, NameValueCollection columnNameValue, string[] Keys, string fieldCombination, string valueCombination) {
			string logicalEntity="";
			newGeneratedNumber = null;
			System.Collections.Hashtable logicalEntityData = KeyNumGenDB.GetLogicalEntityId(PSE_ENTITYID);
			if(logicalEntityData["LOGICALENTITYID"].ToString() !=""  )
			{
				PSE_ENTITYID = logicalEntityData["LOGICALENTITYID"].ToString();
			}
			else if(logicalEntityData["LOGICALENTITYFIELD"].ToString() !="")
			{
				logicalEntity = logicalEntityData["LOGICALENTITYFIELD"].ToString();
				if(columnNameValue.ContainsKey(logicalEntity))
					PSE_ENTITYID = columnNameValue[logicalEntity].ToString();
			}

			NumGenInfo NumGen = new NumGenInfo(PSE_ENTITYID,columnNameValue);
		
			string lastNo;
			if (NumGen.IsAuto) {							
				bool searching=true;
				while (searching) {
					string newNumber= GetNewNum(NumGen.LastNo, NumGen.NumberWidth);
					lastNo = newNumber;
					if (newNumber.Length>0) {
						newNumber=CheckStopNumber(newNumber, NumGen.LastNo, NumGen.StopNumber, NumGen.StartNumber, NumGen.NumberWidth);
						if (updateLastNo(NumGen,newNumber)) {
							columnNameValue[fieldId] =	newNumber;
							newGeneratedNumber = newNumber;
							searching = false;
						}
						//else {
						//	lastNo = KeyNumGenDB.GetNumGenInfoAgain( NumGen.OrganizationCode, NumGen.EntityID, NumGen.LevelNo, NumGen.SerialNo);
						//}
					}											
				}
			}
			if(columnNameValue.ContainsKey(NumGen.KeyColumnName))
				ValidateKeyNumberColumn(NumGen,columnNameValue);
			ValidatePrimaryKeyColumn(Keys,columnNameValue);
			//KeyLevels.CheckKeyLevels(PSE_ENTITYID, columnNameValue);
		}

		public static string GetNewNum(string strOldNumber, decimal numberWidth) {
			string strNewNumber;
			decimal dblNewNumber;		
			dblNewNumber = decimal.Parse(strOldNumber)+1;
			strNewNumber = dblNewNumber.ToString().PadLeft(int.Parse(numberWidth.ToString()),'0');
			return strNewNumber;
		}

		public static double GetNewSerial(string POR_ORGACODE, string PSE_ENTITYID, string PKN_FIELDID){
			return  KeyNumGenDB.GetMaxSerial(POR_ORGACODE, PSE_ENTITYID, PKN_FIELDID);
		}


		public static void ValidateKeyNumberColumn(NumGenInfo NumGen,NameValueCollection columnNameValue){
			string keyColumnValue = "";
			if(columnNameValue.get(NumGen.KeyColumnName)!=null)
				keyColumnValue = columnNameValue[NumGen.KeyColumnName].ToString();

			if(stringOfZeros(keyColumnValue) || keyColumnValue =="")
				if(!NumGen.DefinitionFound)
					throw new UnassignedPrimaryKeyException("Number Generation Method is not defined, contact with System Administrator or enter Number!");
				else if(NumGen.DefinitionFound && NumGen.IsAuto)
					throw new InvalidNumGenException();
				else if(NumGen.DefinitionFound && (!NumGen.IsAuto))
					throw new NumGenManualException(NumGen.KeyColumnName);
				else{
					throw new ApplicationException("Number Field Cannot be Blank or 0!");
				}
		}
		private  static bool stringOfZeros(string s) {	
			bool flag = true;
			if (s!=null) {
				s = s.Replace('0', ' ');
				s = s.Trim();
				if (!s.Equals("")) {
					flag = false;
				}
			}
			return flag;
		}


		public static void ValidatePrimaryKeyColumn(string[] pKey,NameValueCollection NVC){
			for(int i=0;i<pKey.Length;i++) {
				if(NVC[pKey[i]]==null ||  NVC[pKey[i]].ToString().Trim() == "")
					throw new ApplicationException("Null Primary Key Columns Not Allowed");

			}
		}

		private static string AddSuffixPrefix(string KeyColumn, string Expression, NameValueCollection baseTableFields, NameValueCollection columnNameValue){
			string eval;
			SHMA.Enterprise.Formula.ExpressionEvaluator ev = new SHMA.Enterprise.Formula.ExpressionEvaluator();
			ev.setAutoNumberField(KeyColumn);
			ev.setDataFields(columnNameValue);
			ev.setBaseTableFields(baseTableFields);
			ev.setExpression(Expression);
			eval = ev.Evaluate();
			return eval;
		}

		public object  FormulatedNumber{
			get{
				return newGeneratedNumber;
			}
		}

		private  string CheckStopNumber(string newNumber, string lastNo, decimal stopNumber, decimal startNumber, decimal numberWidth){
			decimal decNewNumber = decimal.Parse(newNumber);
			decimal lastno = decimal.Parse(lastNo);
			if(decNewNumber<startNumber){
				newNumber = GetNewNum((startNumber-1).ToString(), numberWidth);
				if(stopNumber < decimal.Parse(newNumber) && stopNumber!=0)
					throw new InvalidNumberStopException();
			}
			else if(lastno+1 > stopNumber && stopNumber!=0)
				throw new InvalidNumberStopException();

			newGeneratedNumber = newNumber;
			return newNumber;
		}

		private bool updateLastNo(NumGenInfo NumGen, string newNumber){
			bool numberUpdated=false;
			if(NumGen.FrequencyType!="" && NumGen.FrequencyDateField!=""){
				string result = KeyNumGenFrequencyDB.CheckPeriodNumber(NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo, frAcntYear,NumGen.FrequencyType, frPeriodNo);
				if(result.Trim()==""){
					if(KeyNumGenFrequencyDB.InsertLastNumber(newNumber, NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo, frAcntYear, NumGen.FrequencyType, frPeriodNo))
						numberUpdated=true;
					else{
						result = KeyNumGenFrequencyDB.CheckPeriodNumber(NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo, frAcntYear,NumGen.FrequencyType, frPeriodNo);
						NumGen.LastNo = result;
					}
				}
				else{
					if(KeyNumGenFrequencyDB.UpdateLastNumber(newNumber,NumGen.LastNo, NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo,frAcntYear,NumGen.FrequencyType,frPeriodNo))
						numberUpdated=true;
					else{
						NumGen.LastNo = KeyNumGenFrequencyDB.CheckPeriodNumber(NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo,frAcntYear,NumGen.FrequencyType, frPeriodNo);
					}
				}
			}
			else{
				if(KeyNumGenDB.UpdateLastNo(newNumber,NumGen.LastNo,NumGen.OrganizationCode,NumGen.EntityID,NumGen.LevelNo,NumGen.SerialNo))
					numberUpdated=true;
				else
					NumGen.LastNo = KeyNumGenDB.GetNumGenInfoAgain( NumGen.OrganizationCode, NumGen.EntityID, NumGen.LevelNo, NumGen.SerialNo);
			}
			return numberUpdated;
		}

		private void CheckFrequency(NumGenInfo NumGen, NameValueCollection columnNameValue){
			string lastNo;
			if(NumGen.FrequencyType!="" && NumGen.FrequencyDateField!="") {
				DateTime frDate=(DateTime)columnNameValue[NumGen.FrequencyDateField];
				string[] periodNo= PeriodTypeDB.GetPeriodNo(NumGen.OrganizationCode,frDate.Date,NumGen.FrequencyType);
				if(periodNo[0]==null){
					throw new ApplicationException("Period for Number Generation Frequnecy Not Defined");
				}
				frPeriodNo = Convert.ToDouble(periodNo[1]);
				frAcntYear = periodNo[0];
				string result = KeyNumGenFrequencyDB.CheckPeriodNumber(NumGen.OrganizationCode,NumGen.EntityID,(double)NumGen.LevelNo,(double)NumGen.SerialNo, periodNo[0], NumGen.FrequencyType, frPeriodNo);
				if(result.Trim()==""){
					lastNo=Convert.ToString(NumGen.StartNumber);
					NumGen.LastNo ="0";
				}
				else{
					NumGen.LastNo = result.Trim();
				}
			}
			else{
				lastNo = NumGen.LastNo;
			}
		}
	}
}
