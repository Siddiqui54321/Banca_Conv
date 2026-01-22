using System;

namespace SHMA.Enterprise.Exceptions
{
	public class HandledException : System.ApplicationException{		
		public HandledException() {}
		public HandledException(String s) : base(s) {}
		public HandledException(String s, Exception inner) : base(s, inner) {}
	}
	public class AppSettingNotFoundException : System.ApplicationException{		
		public AppSettingNotFoundException(object key)  : base("Application setting " + key.ToString() + "not found."){}
		public AppSettingNotFoundException(string description) : base(description) {}
	}	
	public class SessionValNotFoundException : System.ApplicationException{		
		public SessionValNotFoundException(): base("Session value not found.") {}
		public SessionValNotFoundException(string message): base(message) {}
	}
	public class UnassignedPrimaryKeyException : System.ApplicationException{		
		public UnassignedPrimaryKeyException(): base() {}
		public UnassignedPrimaryKeyException(string message): base(message) {}
	}
	public class RecordAlreadyFoundException : ApplicationException{
		public RecordAlreadyFoundException(string tableName):  base(string.Format("Record already found in table {0}",tableName)) {}
		public RecordAlreadyFoundException():  base("Record already found.") {}
	}
	public class EntityRKNotDefinedException : ApplicationException{
		public EntityRKNotDefinedException (string entity):  base(string.Format("Relation Keys not defined! Entity :'{0}'", entity)){}		
		public EntityRKNotDefinedException ():  base(string.Format("Relation Keys not defined!")){}		
	}
	public class EntityPKNotDefinedException : ApplicationException{
		public EntityPKNotDefinedException (string entity):  base(string.Format("Primary Keys not defined! Entity :'{0}'", entity)){}		
		public EntityPKNotDefinedException ():  base(string.Format("Primary Keys not defined!")){}		
	}

	public class NumGenManualException : ApplicationException{
		public NumGenManualException (string column):  base( string.Format("Number Generation Method is defined as Manual for the column '{0}', please enter Number!", column)){}		
		public NumGenManualException ():  base("Number Generation Method is defined as Manual, please enter Number!"){}		
	}
	public class InvalidNumGenException : ApplicationException{
		public InvalidNumGenException():  base("Number Generation Method have invalid definition!"){}		
	}
	public class InvalidNumberWidthException : ApplicationException{
		public InvalidNumberWidthException():  base("Auto Number Exceeds from its defined width!"){}
	}
	public class InvalidNumberStopException : ApplicationException{
		public InvalidNumberStopException():  base("Auto Number Exceeds from its defined Stop point!"){}
	}

	public class RecordNotFoundException : ApplicationException{
		public RecordNotFoundException (string tableName):  base(string.Format("Record not found in table {0}",tableName)) {}
		public RecordNotFoundException ():  base("Record not found...") {}
	}
	public class ClassNotFoundException : ApplicationException{
		public ClassNotFoundException (string className):  base(string.Format("Class not found '{0}'", className)) {}
		public ClassNotFoundException ():  base("Class not found.") {}
		public ClassNotFoundException (string className, string message):  base(string.Format( message, className)) {}
	}	
	public class InvalidParameterException : ApplicationException{
		public InvalidParameterException (string paramName):  base(string.Format( "Invalid parameter found. Parameter Name : {0}", paramName)) {}
	}	
}
