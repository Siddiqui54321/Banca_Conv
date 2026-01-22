using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace SHMA.Enterprise.Batch
{
	public sealed class BatchDML
	{
		
		//private static string strConnectionstring = System.Configuration.ConfigurationSettings.AppSettings["DSN"]==null? "":System.Configuration.ConfigurationSettings.AppSettings["DSN"];
		private static System.String strEncryptedConnStr = System.Configuration.ConfigurationSettings.AppSettings["ECS"]==null? "N":System.Configuration.ConfigurationSettings.AppSettings["ECS"];
		private static string strConnectionstring = strEncryptedConnStr=="Y"? Encryption64.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["DSN"],"1p8dMo5BlQ83okH3"):System.Configuration.ConfigurationSettings.AppSettings["DSN"];		

		
		public static void executeDML(String query, SHMA.Enterprise.Data.ParameterCollection parameters)
		{
			IDbConnection objConn=null;
			try
			{
				objConn= AdhocConnection();
				if(objConn == null) throw new Exception("Connection could not be stablished.");

				IDbCommand cmd = CreateCommand(query,objConn,null);
				cmd = setCommandParameters(cmd,parameters);
				cmd.ExecuteNonQuery();
			}
			catch(System.Exception e)
			{
				throw e;
			}
			finally
			{
                if (objConn != null) objConn.Close();
			}
		}

		public static DataTable executeQuery(String query)
		{

            IDbConnection objConn=null;

			try
			{

                objConn= AdhocConnection();

				if(objConn == null) 
					throw new Exception("Connection could not be stablished.");

				IDbCommand cmd = CreateCommand(query,objConn,null);
				IDataReader dataReader = cmd.ExecuteReader();
				DataTable dataTable = new DataTable();
				SHMA.Enterprise.Utilities.Reader2Table(dataReader,dataTable);
				return dataTable;
			}
			catch(System.Exception e)
			{
                throw e;
			}
			finally
			{
				if (objConn != null) objConn.Close();
			}
		}

        public static DataTable executeQuery(String query, SHMA.Enterprise.Data.ParameterCollection parameters)
        {

            IDbConnection objConn = null;

            try
            {

                objConn = AdhocConnection();

                if (objConn == null)
                    throw new Exception("Connection could not be established.");

                IDbCommand cmd = CreateCommand(query, objConn, null);
                cmd = setCommandParameters(cmd, parameters);
                IDataReader dataReader = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                SHMA.Enterprise.Utilities.Reader2Table(dataReader, dataTable);
                return dataTable;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                if (objConn != null) objConn.Close();
            }
        }

		private static IDbConnection AdhocConnection()
		{
			IDbConnection objConn = null;
			objConn =new OleDbConnection(strConnectionstring);
			objConn.Open();
			return objConn;
		}

		private static IDbCommand CreateCommand(string cmdText, IDbConnection conn, IDbTransaction tran )
		{
			IDbCommand cmd = conn.CreateCommand();
			if(tran != null)
				cmd.Transaction = tran;
			cmd.CommandText = cmdText;
			return cmd;
		}

		private static IDbCommand setCommandParameters(IDbCommand pstatement,SHMA.Enterprise.Data.ParameterCollection Parameter) 
		{
			Object obj = new Object();
			int Counter=0;
			
			for(int counter=0; counter<Parameter.Count;counter++)
			{
				obj =Parameter.gets(Parameter.Keys[counter]);
				System.Data.DbType objType = Parameter.getType(Counter);
				Counter++;
				if(obj == null)
					pstatement.Parameters.Add(CreateParameter(Parameter.Keys[counter], objType, DBNull.Value));
				else
					pstatement.Parameters.Add(CreateParameter(Parameter.Keys[counter], objType, Parameter.gets(Parameter.Keys[counter])));
			}
			return(pstatement);
		}

		private static System.Data.IDataParameter CreateParameter(string ParamName, DbType ParamType, object ParamValue)
		{
			IDataParameter parameter = null;
			switch(ParamType)
			{
				case (DbType.String):
					parameter = new OleDbParameter(ParamName, OleDbType.VarChar);
					break;
				case (DbType.VarNumeric): 
					parameter = new OleDbParameter(ParamName, OleDbType.VarNumeric);
					break;
				case (DbType.Decimal): 
					parameter = new OleDbParameter(ParamName, OleDbType.Decimal);
					break;
				case (DbType.Double): 
					parameter = new OleDbParameter(ParamName, OleDbType.Double);
					break;
				case (DbType.DateTime): 
					parameter = new OleDbParameter(ParamName, OleDbType.Date);
					break;
				case (DbType.Date): 
					parameter = new OleDbParameter(ParamName, OleDbType.DBTimeStamp);
					break;
				default : 
					throw (new Exception("Unsupported Parameter Type Requested in Parameter Factory"));
			}		
			parameter.Value = ParamValue;
			return parameter;
		}

	}
}
