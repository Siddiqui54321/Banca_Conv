using System;
using System.Web.Mail;
using System.Configuration;

namespace SendMail
{
	/// <summary>
	/// Summary description for SendingMail.
	/// </summary>
	public class SendingMail
	{
		private string str_ToMail="";
		private string str_Subject="";
		private string str_mailBody="";
		private string str_Attachment="";
		private string str_cc="";
		//		private string str_MailServer="";
		//		private string str_ServerPort="";

		public SendingMail()
		{
		}
		public void MailSendingProcess()
		{
			string str_FromMail= ConfigurationSettings.AppSettings["FromMail"];
			string str_SmtpServer= ConfigurationSettings.AppSettings["Smtpserver"];
			string str_ServerPort= ConfigurationSettings.AppSettings["ServerPort"];
			string str_Pass= ConfigurationSettings.AppSettings["AddPassward"];

			MailMessage objEmail = new MailMessage();
			objEmail.To = str_ToMail;
//			if (objEmail.To=="")
//			{
//				System.Web.HttpContext.Current.Session["ErrorMsg"]="Must specify \"To Address\"";
//				return ;
//			}
			objEmail.From = str_FromMail;
			objEmail.Cc = str_cc;
			objEmail.Subject = str_Subject;
			if (str_Attachment != "" )
				{
					MailAttachment mt = new MailAttachment(str_Attachment);
					objEmail.Attachments.Add(mt);
				}

			//Setting value in session
			

			objEmail.Body = str_mailBody;
			//objEmail.BodyFormat =str_mailBody;
			
			objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtsperver", str_SmtpServer);
			objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", str_ServerPort);
			//objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 110);
			objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
			objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", str_FromMail);
			objEmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", str_Pass);

			SmtpMail.SmtpServer= str_SmtpServer;
			SmtpMail.Send(objEmail);
			System.Web.HttpContext.Current.Session["ErrorMsg"]= (string) System.Web.HttpContext.Current.Session["ToUser"];


		}

		public string MailTo
		{
			get
			{
				return str_ToMail;
			}
			set 
			{
				str_ToMail= value;
			}
		}
		public string MailCc
		{
			get
			{
				return str_cc;
			}
			set
			{
				str_cc = value;
			}
		}

		
		public string MailSubject
		{
			get
			{
				return str_Subject;
			}
			set
			{
				str_Subject = value;
			}
		}
		
		public string MailBody
		{
			get
			{
				return str_mailBody;
			}
			set 
			{
				str_mailBody = value;
			}
		}
		public string MailAttach
		{
			get
			{
				return str_Attachment;
			}
			set
			{
				str_Attachment = value;
			}
		}
	}

}
