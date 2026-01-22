using System;

namespace SHAB.Shared.Exceptions
{
	public class RecordAlreadyFoundException : ApplicationException{
		public RecordAlreadyFoundException():  base("Record already found.") {}
	}
	public class DBRecNotFoundException : System.ApplicationException{		
		public DBRecNotFoundException(): base("Record not found in database.") {}
		public DBRecNotFoundException(string message): base(message) {}
	}
	public class SessionValNotFoundException : System.ApplicationException{		
		public SessionValNotFoundException(): base("Session value not found.") {}
		public SessionValNotFoundException(string message): base(message) {}
	}

	public class UnassignedPrimaryKeyException : ApplicationException
	{
		public UnassignedPrimaryKeyException(string message): base(message) {}
	}
//	public class OrgCodeNotFoundException : SessionValNotFoundException{		
//		public OrgCodeNotFoundException(): base("Organization code not found in session.") {}
//	}
	
}
