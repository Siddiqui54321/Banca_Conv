using System;
namespace SHMA.Enterprise.Exceptions
{
	[Serializable]
	public class ProcessException:System.SystemException
	{
		public override System.String Message
		{
			get
			{
				return msg;
			}
			
		}
		private System.String msg;
		public ProcessException()
		{
			msg = "Process Un-Successful";
		}
		public ProcessException(System.String msg)
		{
			this.msg = msg;
		}
	}
}