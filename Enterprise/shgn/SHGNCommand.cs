/*
 * Created on Oct 18, 2004
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
using System;
using SHMA.Enterprise;
using System.Web;
using SHMA.Enterprise.Data;

namespace shgn{
	public class SHGNCommand {
		string message=null;
		protected HttpRequest request;
		protected string str_TargetEntityId;
		private NameValueCollection nameValueCollection;
		private NameValueCollection allFieldCollection;
		private DataHolder dataHolder;
		public SHGNCommand() {
			allFieldCollection = new NameValueCollection() ;
		}
		protected void init() {

		}
		public void setMessage(string message)
		{
			this.message=message;
		}
		public string getMessage(){return this.message;}

		public void add(string name, object value) {
			nameValueCollection.add(name,value);
		}
 
		public void set(String name, Object Value) {
			nameValueCollection.set(name,Value);
		}

		public void set(String name, String Value) {
			nameValueCollection.set(name,Value);
		}

		public void set(String name, double Value) {
			nameValueCollection.set(name,Value);
		}

		public void set(String name, int Value) {
			nameValueCollection.set(name,Value);
		}

		public void set(String name, float Value) {
			nameValueCollection.set(name, Value);
		}

		public void set(String name, DateTime Value) {
			nameValueCollection.set(name, Value);
		}

 
		public void setNull(string name) {
			object obj = nameValueCollection.get(name);
			obj = null;
			nameValueCollection.add(name,obj);
		}
  
		public bool isNull(string name){
			object obj = nameValueCollection.get(name);
			if(obj==null)
				return true;
			else
				return false;
		}
  
		public void setNameValueCollection(NameValueCollection nameValueCollection) {
			this.nameValueCollection=nameValueCollection;
		}

		public void setAllFieldCollection(NameValueCollection nameValueCollection) 
		{
			this.allFieldCollection=nameValueCollection;
		}

		public void setAttributes(HttpRequest request, string str_TargetEntityId) 
		{
			this.request = request;
			this.str_TargetEntityId = str_TargetEntityId;
			init();
		}

		public void setAttributes(HttpRequest request) {
			this.request = request;
			init();
		}

		public void setDataHolder(DataHolder dataHolder) {
			this.dataHolder=dataHolder;
		}
		protected DataHolder getDataHolder() {
			return this.dataHolder;
		}
		protected rowset getRowSet(string str_tableName){
			rowset _rowset=this.dataHolder.getAsSHMARowSet(str_tableName);
			return _rowset;
		}
		public virtual void fsoperationAfterSave(){
		}
		public virtual void fsoperationAfterDelete()  {
		}
		public virtual void fsoperationAfterUpdate()  {
		}

		public virtual void fsoperationBeforeSave()   {

		}
		public virtual void fsoperationBeforeDelete()  {
		}
		public virtual void fsoperationBeforeUpdate()  {
		}

		public virtual void beforeCommittingFinalSave() {
			//System.out.println("base.beforeCommittingFinalSave()");   
		}

		public virtual void beforeCommittingFinalUpdate() {
			//System.out.println("base.beforeCommittingFinalUpdate()");
		}
		public virtual void fsoperationBeforeComittingUpdate(){
				
		}
		public virtual void fsoperationBeforeComittingSave(){
				
		}

		public virtual void fsoperationAfterComittingUpdate()
		{				
		}
		public virtual void fsoperationAfterComittingSave()
		{				
		}
		
		public virtual void beforeApplyingFinalSave() 
		{
			//System.out.println("base.beforeCommittingFinalSave()");   
		}

		public virtual void beforeApplyingFinalUpdate() {
			//System.out.println("base.beforeCommittingFinalUpdate()");
		}

		//Overrides Fucntion For Entity Class Start
		//AD;25/06/2009 Start
		public virtual string fsoperationAfterSave(System.String str)
		{
			return str;
		}
		public virtual string fsoperationAfterDelete(System.String str)  
		{
			return str;
		}
		public virtual string fsoperationAfterUpdate(System.String str)  
		{
			return str;
		}
		public virtual string fsoperationBeforeSave(System.String str)
		{
			return str;
		}
		public virtual string fsoperationBeforeDelete(System.String str)  
		{
			return str;
		}
		public virtual string fsoperationBeforeUpdate(System.String str)  
		{
			return str;
		}
		public virtual string beforeCommittingFinalSave(System.String str) 
		{
			return str;
			//System.out.println("base.beforeCommittingFinalSave()");   
		}
		public virtual string beforeCommittingFinalUpdate(System.String str) 
		{
			return str;
			//System.out.println("base.beforeCommittingFinalUpdate()");
		}
		public virtual string fsoperationBeforeComittingUpdate(System.String str)
		{
			return str;
		}
		public virtual string fsoperationBeforeComittingSave(System.String str)
		{
			return str;
		}
		public virtual string fsoperationAfterComittingUpdate(System.String str)
		{				
			return str;
		}
		public virtual string fsoperationAfterComittingSave(System.String str)
		{
			return str;	
		}
		public virtual string beforeApplyingFinalSave(System.String str) 
		{
			return str;
			//System.out.println("base.beforeCommittingFinalSave()");   
		}
		public virtual string beforeApplyingFinalUpdate(System.String str) 
		{
			return str;
			//System.out.println("base.beforeCommittingFinalUpdate()");
		}
		//AD;25/06/2009 END
		//Overrides Fucntion For Entity Class End

		public virtual object get(string name) 
		{
			if(nameValueCollection.ContainsKey(name))
				return nameValueCollection.get(name);
			else if(allFieldCollection.ContainsKey(name)) 
				return allFieldCollection.get(name);
			else
				return null;
		}

		public virtual double getDouble(String name) 
		{
			if(nameValueCollection.ContainsKey(name))
				return (Double)nameValueCollection.get(name);
			else
				return (Double)allFieldCollection.get(name);
		}
		public int getInt(String name) {
			if(nameValueCollection.ContainsKey(name))
				return (int)nameValueCollection.get(name);
			else
				return (int)allFieldCollection.get(name);
		}


		public float getFloat(String name) {
			if(nameValueCollection.ContainsKey(name))
				return (float)nameValueCollection.get(name);
			else
				return (float)allFieldCollection.get(name);
		}

		public DateTime getDate(String name) {
			String strValue;
			if(nameValueCollection.ContainsKey(name))
				strValue = (string)nameValueCollection.get(name);
			else
				strValue = (string)allFieldCollection.get(name);
			return DateTime.Parse(strValue);
		}	  

		public String getString(String name) {
			if(nameValueCollection.ContainsKey(name))
				return((String)nameValueCollection.get(name));
			else
				return((String)allFieldCollection.get(name));
		}
	}
}
