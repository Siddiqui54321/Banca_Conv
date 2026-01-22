/*
* Created on Nov 5, 2004
* TODO To change the template for this generated file go to
* Window - Preferences - Java - Code Style - Code Templates
*/
using System;
using SHMA.Enterprise;
namespace shgn{
	public abstract class ProcessCommand{
		string entityId;
		string[] arrKeyFields;
		string tableCode;
		NameValueCollection allFields;
		NameValueCollection[] dataRows;
		bool[] selectedRows;

		private string processName ;
        private string sessionId ;
        private string referenceNo ;
        private string user ;


        public string getEntityID(){
			return this.entityId;	
		}

		public void  setEntityID(string eID) {
				this.entityId = eID;
		}

		public string[] getPrimaryKeys() {
			return this.arrKeyFields;
		}
		public void setPrimaryKeys(string[] pKeys) {
			this.arrKeyFields = pKeys;
		}

		public string getTableName(){
			return this.tableCode;
		}
		public void setTableName(string tableName) {
			this.tableCode = tableName;
		}
		public virtual NameValueCollection getAllFields(){
			return allFields;
		}
		
		public virtual void setAllFields(NameValueCollection _allFields){
			allFields = _allFields;
		}

		public void setDataRows(NameValueCollection[] _dataRows){
			dataRows = _dataRows;
		}
		
		public NameValueCollection[] getDataRows(){
			return dataRows;
		}
		
		public  void setSelectedRows(bool[] _selectedRows){
						 selectedRows = _selectedRows;
		}

		public  bool[] getSelectedRows(){
						 return selectedRows;
		}

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        public string ReferenceNo
        {
            get { return referenceNo; }
            set { referenceNo = value; }
        }

		public virtual string  processing()
		{
			return "";
		}
	}
}