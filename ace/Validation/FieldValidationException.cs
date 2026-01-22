using System;
namespace SHMA.Enterprise.Exceptions
{
	[Serializable]
	public class FieldValidationException:System.SystemException
	{
		public override System.String Message
		{
			get
			{
				return msg;
			}
			
		}
		private System.String msg;
		public FieldValidationException()
		{
			msg = "Validation Error";
		}
		public FieldValidationException(System.String msg)
		{
			this.msg = msg;
		}
	}
}