using System;
using System.Data;
using System.Data.OleDb;
using SHMA.Enterprise.Data;
using SHMA.Enterprise;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Shared;

namespace ace
{
	/// <summary>
	/// This class is developed only to call Stored Procedure nothing else.
    /// NOTE >>>> This class will use the current Connection and Transaction.
	/// </summary>
	public class ProcedureAdapter
	{
        private OleDbCommand cmd;
		
		public ProcedureAdapter(String storedProcedure)
		{
			cmd = new OleDbCommand();
			cmd.Connection  = (OleDbConnection)DB.Connection;
			//cmd.Transaction = (OleDbTransaction)DB.Transaction;
			OleDbTransaction transaction = (OleDbTransaction)DB.Transaction;
			if(transaction !=null)
				cmd.Transaction = transaction;
            
			cmd.CommandText = storedProcedure;
			cmd.CommandType = CommandType.StoredProcedure;
		}

		public ProcedureAdapter(String storedProcedure, OleDbConnection conn)
        {
            cmd = new OleDbCommand();
            cmd.Connection  = conn;
            cmd.CommandText = storedProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public void Execute()
        {
            try
            {
                cmd.ExecuteScalar();
            }
            catch (NullReferenceException NullE)
            {
                throw NullE;
            }
            catch (OleDbException e)
            {
                throw e;
            }
        }

        #region "Setter/Getter"
        public void Set(String parameterName, Object val)
        {
            cmd.Parameters[parameterName].Value = val;
        }

        public void Set(string parameter, System.Data.OleDb.OleDbType type, Object val)
        {
            cmd.Parameters.Add(parameter, type).Value = val;
        }

        public void Set(string parameter, System.Data.OleDb.OleDbType type, int size, String val)
        {
            cmd.Parameters.Add(parameter, System.Data.OleDb.OleDbType.VarChar, 50).Value = val;
        }

		public void RegisetrOutParameter(string parameter, System.Data.OleDb.OleDbType type, int size)
		{
			cmd.Parameters.Add(parameter, type, size).Direction = ParameterDirection.Output;
		}

		public void RegisetrOutParameter(string parameter, System.Data.OleDb.OleDbType type)
		{
			cmd.Parameters.Add(parameter, type).Direction = ParameterDirection.Output;
		}

        public Object Get(String parameterName)
        {
            return cmd.Parameters[parameterName].Value;
        }

//        public Object GetOld(String parameterName)
//        {
//            return cmd.Parameters[parameterName].Value;
//        }

//        public void RegisetrInParameter(string parameter, System.Data.OleDb.OleDbType type)
//        {
//            cmd.Parameters.Add(parameter, type);
//        }

//        public void RegisetrInParameter(string parameter, System.Data.OleDb.OleDbType type, int size)
//        {
//            cmd.Parameters.Add(parameter, type, size);
//        }

//        public void RegisetrReturnParameter(string parameter, System.Data.OleDb.OleDbType type, int size)
//        {
//            cmd.Parameters.Add(parameter, type, size).Direction = ParameterDirection.ReturnValue;
//        }
        #endregion
    }
}
