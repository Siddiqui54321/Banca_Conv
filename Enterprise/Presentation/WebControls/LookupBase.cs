
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web;

namespace SHMA.Enterprise.Presentation.WebControls
{

	public abstract class LookupBase : System.Web.UI.WebControls.TextBox
	{		//SHMA.Enterprise.Presentation.WebControls.TextBox {
		string errorQuery = "";

		#region Private Variables
		private StringBuilder query;

		//		private string listWidth;
		private string columnMapping;
		protected IDataReader dataSource;
		private string descriptionColumn;
		private string valueField ;
		private string tableName;
		protected string textFields ;
		private string whereColumns ;
		private string whereValues ;
		private string whereOperators ;
		private string queryExtraInfo ;
		private string onChange;
		private bool highLight=false;
		private string validation="Y";
        private bool Encrypt = true;

		#endregion

		#region Accessors

		public string ValueField
		{
			set 
			{
				valueField= value;
			}
		}

		public string TableName
		{
			set 
			{
				tableName= value;
			}
		}

		public string TextFields
		{
			set
			{
				if (value.Length>0)
					textFields = value;
			}
		}

		public string WhereColumns
		{
			set 
			{
				if (value.Length>0)
					whereColumns = value;
			}
		}
		public string WhereValues
		{
			set 
			{
				if (value.Length>0)
					whereValues= value;
			}
		}
		public string WhereOperators
		{
			set 
			{
				if (value.Length>0)
					whereOperators= value;
			}
		}
		public string OnChange
		{
			get
			{
				return onChange;
			}
			set
			{
				onChange = value;
			}
		}

		//		public string ListWidth{
		//			set {
		//				listWidth = value;
		//			}
		//		}

		public string QueryExtraInfo
		{
			set 
			{
				if (value.Trim().Length>0)
					queryExtraInfo = value.Trim();
			}
		}

		public string ColumnMapping
		{
			set
			{
				if (value.Length>0)
					columnMapping=value;
			}
		}
		public string DescriptionColumn
		{
			set 
			{
				if (value!=null)
					descriptionColumn = value;
			}
		}

		public string MappingQuery
		{
			get
			{
				string str_Qry = Query ;
				string str_valueField="";
				string[] str_values = valueField.Split(new char[]{','});

				/*W:
				if (str_Qry.IndexOf(" where ") >= 0)
					str_Qry = str_Qry.Replace(" where " , " where " + valueField + "='" + this.Text + "' and ") ;
				else
					str_Qry = str_Qry.Replace(" " + tableName + " " , " " + tableName + " where " + valueField + "='" + this.Text + "' ") ;
				W:*/

				if (valueField.IndexOf(",")>=0)
					str_valueField=str_values[1] + "='" + this.Text + "'";
				else
					str_valueField=valueField + "='" + this.Text + "'";

				if(str_Qry.IndexOf(" where ") >= 0)
					str_Qry = str_Qry.Replace(" where " , " where " + str_valueField + " and ") ;
				else
					str_Qry = str_Qry.Replace(" " + tableName + " " , " " + tableName + " where " + str_valueField) ;

				return str_Qry;
			}
		}

		public string Query
		{
			get 
			{
				if (query!=null)
					return query.ToString();
				else
				{
					query=new StringBuilder("select  ");
					if (descriptionColumn==null)
						query.Append("1");
					else
						query.Append(descriptionColumn);

					//query.Remove(query.Length-1,1);
					query.Append(" from ");
					query.Append(tableName + " ");
					query.Append(" where  '1'='1' "); // Rehan
					if (whereColumns!=null && whereColumns.Length>0 )	
					{
						string[] _whereColumns =whereColumns.Split(',') ;
						string[] _whereValues =whereValues.Split(',') ;
						string[] _whereOperators =whereOperators.Split(',') ;
						WebControl ctrl = null;
						//query.Append(" where     "); // Rehan
						query.Append("  and  "); // Rehan
						for(int i=0; i<_whereColumns.Length; i++)
						{
							if	(_whereValues[i].IndexOf("_cl")>=0) 
							{
								ctrl = (WebControl) this.Parent.FindControl( _whereValues[i].Replace("_cl","").Replace("'","") );
								if (ctrl!=null)
								{
									query.Append(_whereColumns[i]);
									query.Append(_whereOperators[i]);
									query.Append("'");
									query.Append(Utilities.GetControlValue(ctrl));
									//query.Append(Page.Request[_whereValues[i].Replace("_cl","").Replace("'","")]);
									query.Append("'");
									query.Append(" and ");
								}
							}
							else
							{
								query.Append(_whereColumns[i]);
								query.Append(_whereOperators[i]);
								query.Append(_whereValues[i]);
								query.Append(" and ");
							}
						}
						query.Remove(query.Length-5,4);
					}
					if (queryExtraInfo!=null)
					{
						string queryExtraInfo1 = queryExtraInfo.Trim();
						queryExtraInfo1 = ParseExtraInfo(queryExtraInfo1);
						if ((queryExtraInfo1.Length>10) && (queryExtraInfo1.Substring(0,8).ToLower() == "order by" || queryExtraInfo1.Substring(0,8).ToLower() == "group by"))
						{
							query.Append(" " + queryExtraInfo1);
						}
						else
						{
							if (queryExtraInfo1.Substring(0,5).ToUpper() == "WHERE" )
								query.Append(" and " + queryExtraInfo1.Remove(0,5) );
							else if (queryExtraInfo1.Substring(0,3).ToUpper() != "AND" )
								query.Append(" and " + queryExtraInfo1) ;
							else
								query.Append(" " + queryExtraInfo1) ;
						}
					}
					query.Replace("_Concat", SHMA.Enterprise.Data.PortableSQL.getConcateOperator()).ToString();
					return SHMA.Enterprise.Shared.EnvHelper.Parse(query.ToString()) ;
				}
			}
		}

		public IDataReader DataSource
		{
			set 
			{
				dataSource = value;

			}

		}

		public bool HighLighted
		{
			get
			{
				return highLight;
			}
			set
			{
				highLight = value;
			}
		}

		public virtual string Validation
		{
			get
			{
				return validation;
			}
			set
			{
				validation = value;
			}
		}

		#endregion

		#region Page Methods
		protected override void OnLoad(EventArgs e) 
		{
			base.OnLoad (e);
			
			//Start Rehan 02/10/2010
			//Stop validation if page submited by lister click "lister: or pagerList:" 
			String EventControl = this.Parent.Parent.Page.Request["__EVENTTARGET"];
			bool PostByLister = false;
			if ( EventControl != null) 
			{
			    if (EventControl.IndexOf("lister:",0) != -1 || EventControl.IndexOf("pagerList",0) != -1)
				{
					PostByLister = true;
				}
			}
			// End -- Rehan 02/10/2010
			if( this.Validation=="Y" && PostByLister == false ) 
			{
				if (!ValidatData())
					throw new ApplicationException(@"[Incorrect entry]  Query : " + errorQuery);
			}
		}

		protected override void OnPreRender(EventArgs e) 
		{
			base.OnPreRender (e);
			SHMA.Enterprise.Presentation.Filters.ApplyControlFilter.ApplyFilter(this.ID, this);
			if(!this.Width.IsEmpty)
			{
				Style.Add( "width", this.Width.ToString());
			}
			/*
			this.Attributes.Add( "onkeydown", "comboPress('" + this.ClientID + "');" )  ;
			this.Attributes.Add("title", "'Press F9 or double click to open complete list of values.'" );
			this.Attributes.Add("ondblclick" , "showHide('" + this.ClientID + "');" ) ;

			//this.Attributes.Add("query" , Query );
			//this.Attributes.Add("mappingQuery" , mappingQuery );
			this.Attributes.Add("arrCode" , "" );
			this.Attributes.Add("arrDesc" , "" );
			this.Attributes.Add("arrInfo" , "" );
			this.Attributes.Add("pageNo" , "" );
			this.Attributes.Add("divwidth" , listWidth );
			this.Attributes.Add("textFields",textFields.ToString());

			if(this.CssClass==null || this.CssClass=="")
				this.CssClass = "ComboText";
			*/

            this.Attributes.Add("tableName", Encrypt ? getEncodeValue(tableName.ToString()) : tableName.ToString());
			this.Attributes.Add("valueField",valueField.ToString());

            if (queryExtraInfo != null)
            {
                this.Attributes.Add("queryExtraInfo", queryExtraInfo);
                this.Attributes.Add("QEI", Encrypt ? getEncodeValue(queryExtraInfo) : queryExtraInfo);
            }
			if (whereColumns!=null)
			{
                this.Attributes.Add("whereColumns", Encrypt ? getEncodeValue(whereColumns.ToString()) : whereColumns.ToString());
				this.Attributes.Add("whereValues",whereValues.ToString());
                this.Attributes.Add("whereOperators", Encrypt ? getEncodeValue(whereOperators.ToString()) : whereOperators.ToString());
			}

			this.Attributes.Add("dependent" , "true");

			string changeMethod="";
			if (onChange!=null)
			{
				changeMethod = onChange;
			}
			if (this.Attributes["onchange"]!=null)
			{
				changeMethod += this.Attributes["onchange"];
			}

			string mappingID = "";
			if (columnMapping!=null && descriptionColumn != null)
			{
				Control MapColumn =  this.Parent.FindControl(this.columnMapping);
				if (MapColumn!=null && this.Validation=="Y") 
				{
					mappingID = MapColumn.ClientID ;
					SetMappingDescription(MapColumn);
					this.Attributes.Add("columnMapping" , mappingID);
					this.Attributes.Add("descriptionColumn", descriptionColumn);
					changeMethod += string.Format(";putComboDescription('{0}','{1}');", this.ClientID, mappingID) ;
				}
			}
			else
				changeMethod += string.Format(";putComboDescription('{0}','');", this.ClientID) ;
			//string descMethod="";


			//changeMethod = changeMethod.Replace("parentChanged(", "parentChanged('" + this.ClientID + "'," ) ;

			this.Attributes["onchange"] = changeMethod ;
			changeMethod = changeMethod.Replace( "this", "document.getElementById('" + this.ClientID + "')");
			this.Attributes.Add("JavascriptOnChangeFunction", changeMethod );

			if (this.highLight == true)
			{
				CssClass += " " + "highLightControl";
			}
		}


//		protected override void Render(HtmlTextWriter output) {
//			base.Render(output);
//			string divWidth ="";
//			divWidth = string.Format("STYLE=\"width:{0}\"" , 0);
//
//			string div = string.Format("<DIV class='listHide' id='div{0}' onkeypress=\"hide('{0}') ;\"  onblur=\"hide('{0}');\" onclick=\"hide('{0}');\" {1}></DIV>\n" , this.ClientID, divWidth) ;
//			string frm = "<iframe class='listHide' id='frm" + this.ClientID + "' "+ divWidth +"></iframe>\n"  ;
//
//			output.Write("<script>\n");
//
//			output.Write("</script>");
//			output.Write(div);
//			output.Write(frm);
//
//		}

		#endregion
		
		#region Utility Methods
		bool ValidatData()
		{
			bool found = false;
			if (Page.Request[this.ID]!=null && Page.Request[this.ID].Length>0)
			{
				//string str_Qry = SHMA.Enterprise.Shared.EnvHelper.Parse(query.Replace("_Concat", "+")) ;
				string str_Qry = MappingQuery;
				errorQuery = str_Qry;
				string myValue = Page.Request[this.ID];
				try
				{
					IDbCommand myCommand = SHMA.Enterprise.Data.DB.CreateCommand(str_Qry);
					IDataReader rs_ComboArray = myCommand.ExecuteReader();
					if (rs_ComboArray.Read())
					{
						found=true;
					}
					rs_ComboArray.Close();
				}
				catch (System.Exception e)
				{
					System.Console.WriteLine(e.Message);
					return false;
				}
				finally
				{
					SHMA.Enterprise.Data.DB.DisConnect();
				}
			}
			else
				found=true;
			return found;
		}
		
		void SetMappingDescription(Control mapColumn )
		{
			//Control mapColumn = this.Parent.FindControl(this.columnMapping);
			if(mapColumn!=null)
			{
				//mapColumn = this.Page.FindControl(this.UniqueID.Replace(this.ID , columnMapping));
				if(base.Text.Trim()!="")
				{
					IDbCommand myCommand = SHMA.Enterprise.Data.DB.CreateCommand( SHMA.Enterprise.Shared.EnvHelper.Parse(MappingQuery));
					IDataReader rs_ComboArray = myCommand.ExecuteReader();
					while (rs_ComboArray.Read())
					{
						if(rs_ComboArray[0]!=null)
							((System.Web.UI.WebControls.TextBox)mapColumn).Text = rs_ComboArray[this.descriptionColumn].ToString();
						else
							((System.Web.UI.WebControls.TextBox)mapColumn).Text ="";
					}
					rs_ComboArray.Close();
				}
				else
					((System.Web.UI.WebControls.TextBox)mapColumn).Text = "";
			}
		}
		
		string ParseExtraInfo(string qei)
		{
			int start  =  -1;
			int end  =  -1;
			string fieldName ="";
			WebControl ctrl;
			while (true)
			{
				start = qei.IndexOf("~~") ;
				if (start > -1)
				{
					start += 2;
					end = qei.IndexOf("~~", start) ;
					fieldName = qei.Substring( start , end-start);
					string val = Utilities.GetFieldValue( fieldName, Parent.Controls);
					if (val !=null)
						qei = qei.Replace("~~" + fieldName + "~~", val) ;
					//qei = qei.Remove( 0 , start);
				}
				else
					break;
			}
			return qei;
		}

		#endregion

        private string getEncodeValue(string val)
        {
            string str = Encryption64.Encrypt(val);
            str = HttpContext.Current.Server.UrlEncode(str);
            return str;

        }

	}
}
