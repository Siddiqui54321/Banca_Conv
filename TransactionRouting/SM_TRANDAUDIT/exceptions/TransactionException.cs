using System;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

namespace shsm.exceptions
{
	/// <summary>
	/// Summary description for TransactionException.cs.
	/// </summary>
	public class TransactionException: ProcessException
	{
		public TransactionException():this("[undefined]")
		{
		}
		
		public TransactionException(String str_message): base(str_message)
		{
		}

		public TransactionException(Exception e): base(e.Message)
		{
		}

	}
}
