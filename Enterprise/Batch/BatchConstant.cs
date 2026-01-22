using System;

namespace SHMA.Enterprise.Batch
{
	public sealed class BatchConstant
	{
		private BatchConstant(){}
		//Status Code
		public static string NEW_JOB ="N";
		public static string EXECUTING_JOB ="P";
		public static string REJECTED_JOB ="R";
		public static string ERROR_JOB ="E";
		public static string COMPLETED_JOB ="D";
		//Status Nature
		public static string STATUS_OPEN ="Y";
		public static string STATUS_CLOSED ="N";
		//Parallel Job flag
		public static string ALLOW_ALLJOBS="any";
		public static string ALLOW_ONEJOB_ONLY="none";
		public static string ALLOW_ONEJOB_PER_CLASS="perUser";
		public static string ALLOW_ONEJOB_PER_USER="perclass";
		public static string ALLOW_ONEJOB_PER_USERCLASS="perUserClass";
		//public static string ALLOW_ONEJOB_PER_CLASSPARAM="perClassParameter";
	}
}
