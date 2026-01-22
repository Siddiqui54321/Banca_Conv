using System;
using System.Data;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
namespace SHMA.Enterprise.Shared
{
	/// <summary>
	/// Summary description for AuditLog.
	/// </summary>
	public class AuditLog
	{
		static bool applyAudit=(System.Configuration.ConfigurationSettings.AppSettings["Audit"]!=null && System.Configuration.ConfigurationSettings.AppSettings["Audit"]=="Y")?true:false;
		public AuditLog()
		{
			if(System.Configuration.ConfigurationSettings.AppSettings["Audit"]!=null && System.Configuration.ConfigurationSettings.AppSettings["Audit"]=="Y")
				applyAudit=true;
			else
				applyAudit=false;

		}

		public static IDbCommand CheckAuditFields(IDbCommand command, String TableName)
		{
			if(applyAudit)
				command.CommandText = Util.AddInQuery(command.CommandText, getAuditFields());
			return command;
		}

		public static string CheckAuditFields(String command, String TableName)
		{
			if(applyAudit)
				command = Util.AddInQuery(command, getAuditFields());
			return command;
		}

		private static string[] getAuditFields()
		{
			String[] AuditFields={"ADT_CREATEUSER","ADT_CREATEDATE","ADT_MODIFIEDUSER","ADT_MODIFIEDDATE"};
			return AuditFields;
		}

		private static object[] getAuditData()
		{
			object[] data=new object[4];
			EnvHelper env = new EnvHelper();
			data[0] = env.getAttribute("s_SUS_USERCODE")==null?"":env.getAttribute("s_SUS_USERCODE").ToString();
			data[1] = System.DateTime.Now;
			data[2] = env.getAttribute("s_SUS_USERCODE")==null?"":env.getAttribute("s_SUS_USERCODE").ToString();
			data[3] = System.DateTime.Now;
			return data;
		}

		public static NameValueCollection PutAuditFieldsForAdd(NameValueCollection nameValue)
		{
			string[] auditFields = getAuditFields();
			object[] data=getAuditData();

			EnvHelper env=new EnvHelper();
			if(applyAudit)
			{
				for(int i=0;i<auditFields.Length;i++)
				{
					if(nameValue.Contains(auditFields[i]))
					{
						nameValue[auditFields[i]] = data[i];
					}
					else
					{
						nameValue.add(auditFields[i],data[i]);
					}
				}
			}
			return nameValue;
		}

		public static NameValueCollection PutAuditFieldsForUpdate(NameValueCollection nameValue)
		{
			string[] auditFields = getAuditFields();
			object[] data=getAuditData();

			EnvHelper env=new EnvHelper();
			if(applyAudit)
			{
				for(int i=2;i<auditFields.Length;i++)
				{
					if(nameValue.Contains(auditFields[i]))
					{
						nameValue[auditFields[i]] = data[i];
					}
					else
					{
						nameValue.add(auditFields[i],data[i]);
					}
				}
			}
			return nameValue;
		}
	}
}
