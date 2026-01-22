using System;

namespace ace
{
	/// <summary>
	/// Summary description for FieldRegister.
	/// </summary>
	public class FieldRegister
	{
		private System.Collections.SortedList slVFDesc;
		private System.Collections.SortedList slVFDBName;
		private System.Collections.SortedList slVFDataType;
		private System.Collections.SortedList slVFSource;
		public FieldRegister()
		{
			slVFDesc = new System.Collections.SortedList();
			slVFDBName = new System.Collections.SortedList();
			slVFDataType = new System.Collections.SortedList();
			slVFSource = new System.Collections.SortedList();
		}


		#region "Field Description"
		public void setDescription(string validationField, string p_Description)
		{
			if (this.slVFDesc.ContainsKey(validationField)) 
			{
				this.slVFDesc[validationField] = p_Description;
			}
			else 
			{
				this.slVFDesc.Add(validationField, p_Description);
			}
		}

		public string getDescription(string validationField)
		{
			return Convert.ToString(this.slVFDesc[validationField]);
		}

		#endregion

		#region "Field Name"
		public void setFieldName(string validationField, string p_DBFieldName)
		{
			if (this.slVFDBName.ContainsKey(validationField)) 
			{
				this.slVFDBName[validationField] = p_DBFieldName;
			}
			else 
			{
				this.slVFDBName.Add(validationField, p_DBFieldName);
			}
		}

		public string getFieldName(string validationField)
		{
			return Convert.ToString(this.slVFDBName[validationField]);
		}

		#endregion

		#region "Data Type"
		public void setDataType(string validationField, string p_DataType)
		{
			if (this.slVFDataType.ContainsKey(validationField)) 
			{
				this.slVFDataType[validationField] = p_DataType;
			}
			else 
			{
				this.slVFDataType.Add(validationField, p_DataType);
			}
		}

		public string getDataType(string validationField)
		{
			return Convert.ToString(this.slVFDataType[validationField]);
		}
	
		#endregion

		#region "Source"
		public void setSourceType(string validationField, string p_SourceType)
		{
			if (this.slVFSource.ContainsKey(validationField)) 
			{
				this.slVFSource[validationField] = p_SourceType;
			}
			else 
			{
				this.slVFSource.Add(validationField, p_SourceType);
			}
		}

		public string getSourceType(string validationField)
		{
			return Convert.ToString(this.slVFSource[validationField]);
		}

		#endregion
	}
}
