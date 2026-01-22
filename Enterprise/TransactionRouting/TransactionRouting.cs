using System;
using  SHMA.Enterprise;
using SHMA.Enterprise.Exceptions;

namespace SHMA.Enterprise.TransactionRouting {
	
	public class RoutingController {
		
		public RoutingController() 
		{
			bln_supportsExtendedMessage = false;
			str_beforeRouteMessage = "";

		}

		public void setFields(NameValueCollection fields) 
		{
				this.obj_fields = fields;
		}
				
		public virtual bool beforeRoute() {
			return true;
		}
		
		
		public virtual void  afterRoute() {
		}
		
		// addition
		public virtual String afterApproval()
		{
			if (bln_supportsExtendedMessage)
				throw new ProcessException("Invalid State: afterApproval() is NOT overridden by " + this.GetType().Name);
			else 
				throw new ProcessException("Invalid State: call enableMessageSupport() before afterApproval()");

			return "";
		}

		protected void enableMessageSupport()
		{
			bln_supportsExtendedMessage = true;
		}
		
		protected void setBeforeRouteMessage(String str_message)
		{
			if (!bln_supportsExtendedMessage)
				throw new ProcessException("Invalid State: call enableMessageSupport() before setBeforeRouteMessage(String)");
			if (str_message != null)
				str_beforeRouteMessage = str_message;

		}
		public virtual String getBeforeRouteMessage()
		{
			return str_beforeRouteMessage;
		}

		public bool hasMessageSupport()
		{
			return bln_supportsExtendedMessage;
		}

		public Object getObject(String str_key) 
		{
			if (obj_fields == null)
				throw new SystemException("Column Values Not Available");
			return obj_fields.getObject(str_key);
		}
		
		public String getString(String str_key)
		{
			return (String)obj_fields.getObject(str_key);
		}

		private NameValueCollection obj_fields;
		private bool bln_supportsExtendedMessage;
		private String str_beforeRouteMessage;

	}
}