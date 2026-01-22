using System;
using System.Data;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using System.Text;
namespace SHMA.Enterprise.Business {
	[CLSCompliant(false)]
	public class ColumnTransform {
		
		public ColumnTransform() {
		}
		bool forUpdate=false;

		public void HandleAdd(string entityID, NameValueCollection baseNameValue, NameValueCollection columnNameValue){
			//TransformColumn(entityID,baseNameValue, columnNameValue);
		}

		public void HandleAdd(string entityID, NameValueCollection columnNameValue)
		{
			//TransformColumn(entityID,columnNameValue, columnNameValue);
		}

		public void HandleUpdate(string entityID, NameValueCollection baseNameValue, NameValueCollection columnNameValue)
		{
			forUpdate=true;
			//TransformColumn(entityID, baseNameValue, columnNameValue);
		}
		public void HandleUpdate(string entityID, NameValueCollection baseNameValue){
			forUpdate=true;
			//TransformColumn(entityID, baseNameValue, baseNameValue);
		}

		//
		// TODO: Generated Number should be copied into the columnNameValue from baseNameValue
		//

		private void TransformColumn(string entityID, NameValueCollection baseNameValue,  NameValueCollection columnNameValue) {
			DataTable table = ColumnTransformDB.GetColumnTranformationData(entityID);
			DataTable colTable =  ColumnTransformDB.GetDistinctColumn(entityID);
			string strValueComb, strFieldComb, columnName="", defaultFormula, definedValueComb, newValue="";
			int defaultVerifyOnUpdate=0, verifyOnUpdate=0;
			bool transformationIsDefined=false;
			NameValueCollection parameter = new NameValueCollection();

			foreach(DataRow DR in colTable.Rows) {
				columnName = DR["PEC_COLFIELDID"].ToString();
				System.Data.DataRow[] colDetail = table.Select("PEC_COLFIELDID = '" + columnName + "'");
				defaultFormula = "";
				String lastNumber, stopNumber, startNumber, ctr_colsrno, pec_colsrno;
				
				for(int detailCounter=0; detailCounter < colDetail.Length;detailCounter++) {

					strFieldComb = colDetail[detailCounter]["CTR_FIELDCOMBINATION"]!=DBNull.Value?colDetail[detailCounter]["CTR_FIELDCOMBINATION"].ToString():"";
					definedValueComb = colDetail[detailCounter]["CTR_VALUECOMBINATION"]!=DBNull.Value?colDetail[detailCounter]["CTR_VALUECOMBINATION"].ToString():"";

					startNumber = colDetail[detailCounter]["CTR_STARTNO"]!=DBNull.Value?colDetail[detailCounter]["CTR_STARTNO"].ToString():"0";;
					lastNumber = colDetail[detailCounter]["CTR_LASTNO"]!=DBNull.Value?colDetail[detailCounter]["CTR_LASTNO"].ToString():"0";
					stopNumber = colDetail[detailCounter]["CTR_STOPNO"]!=DBNull.Value?colDetail[detailCounter]["CTR_STOPNO"].ToString():"0";
					ctr_colsrno = colDetail[detailCounter]["CTR_COLSRNO"].ToString();
					pec_colsrno = colDetail[detailCounter]["PEC_COLSRNO"].ToString();
					parameter.Clear();
					parameter.add("STARTNO",Convert.ToDecimal(startNumber));
					parameter.add("LASTNO",Convert.ToDecimal(lastNumber));
					parameter.add("STOPNO",Convert.ToDecimal(stopNumber));
					parameter.add("SERIALCOLUMN_PEC",pec_colsrno);
					parameter.add("SERIALCOLUMN_CTR",ctr_colsrno);
					parameter.add("ENTITYID",entityID);

					String[] FieldComb = strFieldComb.Split("~".ToCharArray());
					StringBuilder sb=new StringBuilder();
					for(int i=0;i<FieldComb.Length;i++) {
							sb.Append(columnNameValue[FieldComb[i]]);
							sb.Append("~");
					}
					transformationIsDefined = true;
					strValueComb = sb!=null? sb.ToString():"";

					int lastIndexOfTild = strValueComb.LastIndexOf("~");
		
					if (lastIndexOfTild>0){
						strValueComb = strValueComb.Remove(strValueComb.LastIndexOf("~"), 1);
					}

					if(strFieldComb.ToUpper().Equals("DEFAULT")){
						defaultFormula = colDetail[detailCounter]["CTR_FORMULA"].ToString();
						defaultVerifyOnUpdate = colDetail[detailCounter]["CTR_VERIFYONUPDATE"]==DBNull.Value?0:Convert.ToInt32(colDetail[detailCounter]["CTR_VERIFYONUPDATE"]);
					}

					if(strValueComb.Length > 1 && strValueComb.ToUpper().Equals(definedValueComb.ToUpper())){
						newValue = FormulateValue(colDetail[detailCounter]["CTR_FORMULA"].ToString(),baseNameValue,columnNameValue, parameter);
						verifyOnUpdate = colDetail[detailCounter]["CTR_VERIFYONUPDATE"]==DBNull.Value?0:Convert.ToInt32(colDetail[detailCounter]["CTR_VERIFYONUPDATE"]);
						break;
					}
					else if(detailCounter==colDetail.Length-1 && defaultFormula!=""){
						newValue = FormulateValue(defaultFormula,baseNameValue,columnNameValue, parameter);
						verifyOnUpdate = defaultVerifyOnUpdate;
					}

				}
			}
			
			if(forUpdate && verifyOnUpdate==1 && newValue!="") {
				if(!baseNameValue[columnName].ToString().Equals(newValue))
					throw new ApplicationException("Unable to Verify Column Transformation " + columnName);
			}
			else if(forUpdate && verifyOnUpdate==2 && newValue!="")
				baseNameValue[columnName] = newValue;
			else if((!forUpdate) && newValue !="" && columnName != "" && transformationIsDefined)
				baseNameValue[columnName] = newValue;
		}

		private string FormulateValue(string Expression, NameValueCollection baseTableFields, NameValueCollection columnNameValue, NameValueCollection parameter){
			string eval;
			SHMA.Enterprise.Formula.ExpressionEvaluator ev = new SHMA.Enterprise.Formula.ExpressionEvaluator();
			ev.setDataFields(baseTableFields);
			ev.setBaseTableFields(baseTableFields);
			ev.setExpression(Expression);
			ev.setParameter(parameter);
			eval = ev.Evaluate();
			return eval;
		}

	}
}
