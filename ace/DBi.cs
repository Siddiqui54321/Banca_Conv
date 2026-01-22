using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;

using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;




namespace Aceins.ace
{
	/// <summary>
	/// Summary description for DBi.
	/// </summary>
	public class DBi
	{
		public DBi()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		private void Page_Load()
		{
			// Only run this code the first time the page is loaded.
			// The code inside the IF statement is skipped when you resubmit the page.
			//Create a connection to the SQL Server; modify the connection string for your environment
			//SqlConnection MyConnection = new SqlConnection("server=(local);database=pubs;Trusted_Connection=yes");
			SqlConnection MyConnection = new SqlConnection("server=(local);database=pubs;UID=myUser;PWD=myPassword;");

			// Create a Command object, and then set the connection.
			// The following SQL statements check whether a GetAuthorsByLastName  
			// stored procedure already exists.
			SqlCommand MyCommand = new SqlCommand("select * from sysobjects where id = object_id(N'GetAuthorsByLastName')" +
				"  and OBJECTPROPERTY(id, N'IsProcedure') = 1", MyConnection);

			// Set the command type that you will run.
			MyCommand.CommandType = CommandType.Text;

			// Open the connection.
			MyCommand.Connection.Open();

			// Run the SQL statement, and then get the returned rows to the DataReader.
			SqlDataReader MyDataReader = MyCommand.ExecuteReader();

			// If any rows are returned, the stored procedure that you are trying 
			// to create already exists. Therefore, try to create the stored procedure
			// only if it does not exist.
			if(!MyDataReader.Read())
			{
				MyCommand.CommandText = "create procedure GetAuthorsByLastName" + 
					" (@au_lname varchar(40), select * from authors where" +
					" au_lname like @au_lname; select @RowCount=@@ROWCOUNT";
				MyDataReader.Close();
				MyCommand.ExecuteNonQuery();
			}
			else
			{
				MyDataReader.Close();
			}

			MyCommand.Dispose();  //Dispose of the Command object.
			MyConnection.Close(); //Close the connection.
		}

		// Add the event handler to the Button_Click event.
		//this.btnGetAuthors.Click += new System.EventHandler(this.btnGetAuthors_Click);


		private void btnGetAuthors_Click()
		{
			//Create a connection to the SQL Server; modify the connection string for your environment.
			//SqlConnection MyConnection = new SqlConnection("server=(local);database=pubs;Trusted_Connection=yes");
			SqlConnection MyConnection = new SqlConnection("server=(local);database=pubs;UID=myUser;PWD=myPassword;");

			//Create a DataAdapter, and then provide the name of the stored procedure.
			SqlDataAdapter MyDataAdapter = new SqlDataAdapter("GetAuthorsByLastName", MyConnection);

			//Set the command type as StoredProcedure.
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

			//Create and add a parameter to Parameters collection for the stored procedure.
			MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@au_lname", SqlDbType.VarChar, 40));

			//Assign the search value to the parameter.
			MyDataAdapter.SelectCommand.Parameters["@au_lname"].Value = "Name";

			//Create and add an output parameter to the Parameters collection. 
			MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@RowCount", SqlDbType.Int, 4));

			//Set the direction for the parameter. This parameter returns the Rows that are returned.
			MyDataAdapter.SelectCommand.Parameters["@RowCount"].Direction = ParameterDirection.Output;

			//Create a new DataSet to hold the records.
			DataSet DS = new DataSet();
		
			//Fill the DataSet with the rows that are returned.
			MyDataAdapter.Fill(DS, "AuthorsByLastName");

			//Get the number of rows returned, and assign it to the Label control.
			//lblRowCount.Text = DS.Tables(0).Rows.Count().ToString() & " Rows Found!"
			String lblRowCount = MyDataAdapter.SelectCommand.Parameters[1].Value + " Rows Found!";

			//Set the data source for the DataGrid as the DataSet that holds the rows.
			//GrdAuthors.DataSource = DS.Tables["AuthorsByLastName"].DefaultView;

			//NOTE: If you do not call this method, the DataGrid is not displayed!
			//GrdAuthors.DataBind();

			MyDataAdapter.Dispose(); //Dispose the DataAdapter.
			MyConnection.Close(); //Close the connection.

		}


		public static string GetTechName(string aID)
		{
			string returnvalue = "";

			//OleDbConnection myConnection = new OleDbConnection();
			
			OracleConnection conn = new OracleConnection("Data Source=ws107;User Id=ace;Password=ace;");



			conn.Open();

			// create the command for the function
			OracleCommand cmd = new OracleCommand();

			cmd.Connection = conn;
			cmd.CommandText = "GET_EMPLOYEE_EMAIL";
			cmd.CommandType = CommandType.StoredProcedure;

			// add the parameters, including the return parameter to retrieve
			// the return value
			cmd.Parameters.Add("p_employee_id", OracleType.Number).Value = 1;
			cmd.Parameters.Add("p_email", OracleType.VarChar, 25).Direction = ParameterDirection.ReturnValue;


			
			//Test to see if the new row was added
			try
			{
				//myConnection.Open();
				cmd.ExecuteNonQuery();
				returnvalue = ""+cmd.Parameters["p_email"].Value;
			}
			catch(NullReferenceException NullE)
			{
				//ignore null references. assign to dummy to avoid warning
				string msg = NullE.Message;
			}
			catch (OleDbException e)
			{    
				//myConnection.Close();
				throw e;
			}

			//Close the active connection to the database.
			//myConnection.Close();
			string TechName="";
			return (TechName);
		}


		public static string GetTechName1(string aID)
		{
			string returnvalue = "";

			//OleDbConnection myConnection = new OleDbConnection();
			
			
			OleDbConnection conn = (OleDbConnection) DB.Connection;
			

			// create the command for the function

			OleDbCommand cmd =  new OleDbCommand("{? = call GET_EMPLOYEE_EMAIL(?)}", conn);

			// add the parameters, including the return parameter to retrieve
			// the return value
			cmd.Parameters.Add("p_employee_id", OleDbType.VarChar).Value = "101";
			cmd.Parameters.Add("p_email", OleDbType.VarChar, 25).Direction = ParameterDirection.ReturnValue;
			//myCMD.Parameters.Add("ID", OleDbType.Numeric, 4).Value = 0;

			//Test to see if the new row was added
			try
			{
				//myConnection.Open();
				cmd.ExecuteNonQuery();
				returnvalue = ""+cmd.Parameters["p_email"].Value;
				//Response.Write(returnvalue);
			}
			catch(NullReferenceException NullE)
			{
				//ignore null references. assign to dummy to avoid warning
				string msg = NullE.Message;
			}
			catch (OleDbException e)
			{    
				//myConnection.Close();
				throw e;
			}

			//Close the active connection to the database.
			//myConnection.Close();
			string TechName="";
			return (returnvalue);
		}


		public static string GetTechName2(string aID)
		{
			string returnvalue = "";

			//OleDbConnection myConnection = new OleDbConnection();
			
			
			OleDbConnection conn = (OleDbConnection) DB.Connection;
			

			// create the command for the function


			OleDbCommand cmd =  new OleDbCommand();

			cmd.Connection = conn;
			cmd.CommandText = "GET_EMPLOYEE_EMAIL";
			cmd.CommandType = CommandType.StoredProcedure;

			// add the parameters, including the return parameter to retrieve
			// the return value
			cmd.Parameters.Add("p_employee_id", OleDbType.Variant).Value = "101";
			cmd.Parameters.Add("p_email", OleDbType.Variant, 25).Direction = ParameterDirection.ReturnValue;

			//Test to see if the new row was added
			try
			{
				//myConnection.Open();
				cmd.ExecuteNonQuery();
				returnvalue = ""+cmd.Parameters["p_email"].Value;
				//Response.Write(returnvalue);
			}
			catch(NullReferenceException NullE)
			{
				//ignore null references. assign to dummy to avoid warning
				string msg = NullE.Message;
			}
			catch (OleDbException e)
			{    
				//myConnection.Close();
				throw e;
			}

			//Close the active connection to the database.
			//myConnection.Close();
			string TechName="";
			return (returnvalue);
		}

		public static string GetTechName3(string aID)
		{
			string returnvalue = "";

			//OleDbConnection myConnection = new OleDbConnection();
			
			OracleConnection conn = new OracleConnection("Data Source=ws107;User Id=ace;Password=ace;");



			conn.Open();

			// create the command for the function

			OracleCommand cmd =  new OracleCommand("{? = call GET_EMPLOYEE_EMAIL(?)}", conn);
			
			// add the parameters, including the return parameter to retrieve
			// the return value
			cmd.Parameters.Add("p_email", OracleType.VarChar, 25).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("p_employee_id", OracleType.VarChar).Value = "101";


			
			//Test to see if the new row was added
			try
			{
				//myConnection.Open();
				cmd.ExecuteNonQuery();
				returnvalue = ""+cmd.Parameters["p_email"].Value;
			}
			catch(NullReferenceException NullE)
			{
				//ignore null references. assign to dummy to avoid warning
				string msg = NullE.Message;
			}
			catch (OleDbException e)
			{    
				//myConnection.Close();
				throw e;
			}

			//Close the active connection to the database.
			//myConnection.Close();
			string TechName="";
			return (TechName);
		}


		/*private static string DemoFunction9i()
		{
			// create the connection
			OracleConnection conn = new OracleConnection("Data Source=oracledb; User Id=UserID;Password=Password;");

			// create the command for the function
			OracleCommand cmd = new OracleCommand();
			cmd.Connection = conn;
			cmd.CommandText = "GET_EMPLOYEE_EMAIL";
			cmd.CommandType = CommandType.StoredProcedure;

			// add the parameters, including the return parameter to retrieve
			// the return value
			cmd.Parameters.Add("p_employee_id", OracleType.Number).Value = 101;
			cmd.Parameters.Add("p_email", OracleType.VarChar, 25).Direction = ParameterDirection.ReturnValue;

			// execute the function
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();

			// output the result
			Console.WriteLine("Email address is: " + cmd.Parameters["p_email"].Value);
		}
		*/


	
	
	
	
	}
}
