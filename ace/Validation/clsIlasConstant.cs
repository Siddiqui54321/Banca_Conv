namespace ace
{
	public class clsIlasConstant
	{
		//************ Contribution Frequency *************
		public const string CONTFREQ_SINGLE = "S";
		public const string CONTFREQ_MONTHLY = "M";
		public const string CONTFREQ_QUARTERLY = "Q";
		public const string CONTFREQ_SEMIANNUAL = "H";
		public const string CONTFREQ_ANNUAL = "A";
   
		//************ Product Frequency ***********************
		public const string PRODUCT_FREQUENCY = "RDRFR";
   
		//************ Validation Fields ***********************
		public const string VALIDATE_BMI            = "BMI";
		public const string VALIDATE_BTERM          = "BTERM";
		public const string VALIDATE_ENTRYAGE       = "ENTRYAGE";
		public const string VALIDATE_FAFACTOR       = "FAFACTOR";
		public const string VALIDATE_INDEXRATE      = "INDEXRATE";
		public const string VALIDATE_MATURITYAGE    = "MATURITYAGE";
		public const string VALIDATE_MORTALITYRATE  = "MORTRATE";
		public const string VALIDATE_PREMIUM        = "PREMIUM";
		public const string VALIDATE_PTERM          = "PTERM";
		public const string VALIDATE_SUMASSURED     = "SUMASSURED";
		public const string VALIDATE_TOTPREM        = "TOTPREM";
		public const string VALIDATE_BENRELATION    = "BENRELATION";

		//************ Different purpose Fields ***********************
		//public const string VALIDATE_MANDATORYRIDER = "MANDRIDER";
		public const string DECISION = "DECISION";
		public const string REPORT   = "REPORT";
		public const string CALLVALIDATION = "CALLVALIDATION";
		public const string MANDATORY_RIDER = "MANDRIDER";
		public const string FORBIDDEN_RIDER = "FORBIDDENRIDER";
		public const string VALIDATEOTHER = "VALIDATEOTHER";

		//******************** Global Product *********************
		public const string GLOBAL_PRODUCT = "999";


		//************ More ***********************
		public const int  AGE_LIMIT = 18;

		//************ Report Type ***********************
		public const string REPORTTYPE_ILLUSTRATION = "ILLUSTRATION";
		public const string REPORTTYPE_ADVICE       = "ADVICE";
		public const string REPORTTYPE_PROFILE      = "PROFILE";
		public const string REPORTTYPE_POLICY       = "POLICY";
		public const string reportType_HISTORY = "HISTORY";		//CHG-HIS
		public const string reportType_STATUS = "STATUS";
        public const string reportType_DDA = "DDAFORM";


    }
}