using System;
using SHMA.Enterprise;
namespace shsm
{
	/// <summary>
	/// Summary description for SHSM_SecurityPermission.
	/// </summary>
	public class SHSM_SecurityPermission
	{
		
		private bool saveAllowed=true;
		private bool updateAllowed=true;
		public SHSM_SecurityPermission()
		{
				
		}

		public SHSM_SecurityPermission(String a, String b,  SHMA.Enterprise.NameValueCollection nameValue , String c)
		{
		//	ace.ValidationClass.UpdateInAnyFormAllowed();
		//	updateAllowed=false;
		//	saveAllowed=false;
		}

		public SHSM_SecurityPermission(String a, String[] b,  SHMA.Enterprise.NameValueCollection nameValue , String c)
		{
		//	ace.ValidationClass.UpdateInAnyFormAllowed();
		//	updateAllowed=false;
		//	saveAllowed=false;
		}

		public bool SaveAllowed 
		{
			get
			{
				return saveAllowed;
			}
		}
		public bool UpdateAllowed
		{
			get
			{ 
				return updateAllowed;
			}
		}
		public bool DeleteAllowed = true;


		public bool ProcessAllowed(String a)
		{
			return true;
		}
	}
}
