using System;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Data
{
	/// <summary>
	/// Summary description for Types.
	/// </summary>
	public sealed class Types
	{
		private Types()
		{
		}
		public static System.Data.DbType  BIT = System.Data.DbType.Boolean;
		public static System.Data.DbType TINYINT =  System.Data.DbType.Decimal;
		public static System.Data.DbType SMALLINT = System.Data.DbType.Decimal;
		public static System.Data.DbType INTEGER =  System.Data.DbType.Decimal;
		public static  System.Data.DbType BIGINT =  System.Data.DbType.Decimal;
		public static  System.Data.DbType FLOAT = System.Data.DbType.Decimal;
		public static  System.Data.DbType DOUBLE = System.Data.DbType.Double;
		public static  System.Data.DbType NUMERIC = System.Data.DbType.VarNumeric;
		public static  System.Data.DbType DECIMAL = System.Data.DbType.Decimal;
		public static  System.Data.DbType CHAR = System.Data.DbType.String;
		public static  System.Data.DbType VARCHAR = System.Data.DbType.String;
		public static  System.Data.DbType LONGVARCHAR = System.Data.DbType.String;
		public static  System.Data.DbType DATE = System.Data.DbType.DateTime;
		public static  System.Data.DbType TIME = System.Data.DbType.Time;
		public static  System.Data.DbType TIMESTAMP = System.Data.DbType.Date;
		public static  System.Data.DbType BINARY = System.Data.DbType.Binary;
		public static  System.Data.DbType VARBINARY = System.Data.DbType.Binary;
		public static  System.Data.DbType LONGVARBINARY = System.Data.DbType.Binary;
		public static  System.Data.DbType NULL = System.Data.DbType.Object;
		public static  System.Data.DbType OTHER = System.Data.DbType.Object;
		public static  System.Data.DbType JAVA_OBJECT = System.Data.DbType.Object;
		public static  System.Data.DbType DISTINCT = System.Data.DbType.Object;
		public static  System.Data.DbType STRUCT = System.Data.DbType.Object;
		public static  System.Data.DbType ARRAY = System.Data.DbType.Object;
		public static  System.Data.DbType BLOB = System.Data.DbType.Object;
		public static  System.Data.DbType CLOB = System.Data.DbType.Object;
		public static  System.Data.DbType REF = System.Data.DbType.Object;
	}
}
