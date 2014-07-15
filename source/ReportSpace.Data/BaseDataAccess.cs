using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace ReportSpace.Data
{
    public partial class DataAccess : IDisposable
    {
        #region [ Fields ]
        private SqlConnection Connection;
        private SqlTransaction CurrentTransaction;
        private SqlCommand Command;
        public String DatabaseName { get; set; }
        private static bool is_executing = false;

        private String connectionString
        {
            get
            {
                string connection_stirng = "--";

                if (this.Connection != null)
                {
                    connection_stirng = this.Connection.ConnectionString;
                }

                return connection_stirng;
            }
        }

        private String commandText
        {
            get
            {
                string command_text = "--";
                if (this.Command != null)
                {
                    command_text = this.Command.CommandText;
                }
                return command_text;
            }
        }
        #endregion

        #region [ Constructors ]
        public DataAccess()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            this.CreateConnection(connectionString);
            this.DatabaseName = this.Connection.Database;
        }

        public DataAccess(string connectionString)
        {
            this.CreateConnection(connectionString);
            this.DatabaseName = this.Connection.Database;
        }


        #endregion

        #region [ Connection Management Methods ]
        /// <summary>
        /// Initializes the Sql Connection Object 
        /// </summary>
        /// <param name="ConnectionString">ADO.NET Connection String</param>
        public virtual void CreateConnection(string ConnectionString)
        {
            if ((this.CurrentTransaction == null))
            {
                Connection = new SqlConnection(ConnectionString);
            }

        }
        #endregion

        #region [ SQL Command Methods ]
        public virtual void AddParameter(string name, object value, ParameterDirection direction)
        {
            if ((this.Command == null))
            {
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var parameter = new SqlParameter(name, this.GetValue(value));
            parameter.Direction = direction;
            this.Command.Parameters.Add(parameter);
        }

        public virtual void AddParameter(string name, object value, ParameterDirection direction, String UserDefinedTableTypeName)
        {
            if ((this.Command == null))
            {
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var parameter = new SqlParameter(name, SqlDbType.Structured, 0);
            parameter.TypeName = UserDefinedTableTypeName;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = direction;
            
            this.Command.Parameters.Add(parameter);
        }

        public virtual void AddParameter(System.Data.Common.DbParameter Parameter)
        {
            if ((this.Command == null))
            {
                return;
            }
            this.Command.Parameters.Add((SqlParameter)Parameter);
        }

        public virtual void CreateProcedureCommand(string storedProcedureName)
        {
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }
            this.Command = new SqlCommand(storedProcedureName, this.Connection);
            this.Command.CommandType = CommandType.StoredProcedure;
        }
        #endregion

        #region [ Projection Methods ]

        public virtual DataSet ExecuteDataSet()
        {
            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                if (this.DatabaseName == null)
                {
                    throw new ApplicationException("Call CreateConnection method before using the connection. Database Name is also blank.");
                }
            }


            SqlDataAdapter da = null;
            DataSet ds;
            try
            {
                da = new SqlDataAdapter(Command);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (SqlException sqlException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, sqlException.Message, sqlException);
            }
            finally
            {
                if ((da != null))
                {
                    da.Dispose();
                }
            }
            return ds;
        }

        public virtual SqlDataReader ExecuteDataReader()
        {
            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }
            ConnectionState initialState = ConnectionState.Closed;
            SqlDataReader reader = null;
            try
            {
                this.QuietOpen(out initialState);
                reader = this.Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (ObjectDisposedException objectException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, objectException.Message, objectException);
            }
            catch (InvalidOperationException operationException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, operationException.Message, operationException);
            }
            catch (System.IO.IOException ioException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, ioException.Message, ioException);
            }
            catch (InvalidCastException caseError)
            {
                throw new DataAccessException(this.connectionString, this.commandText, caseError.Message, caseError);
            }
            catch (SqlException sqlException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, sqlException.Message, sqlException);
            }

            return reader;
        }

        public virtual int ExecuteNonQuery()
        {
            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }

            ConnectionState initialState = ConnectionState.Closed;
            int value;

            try
            {
                this.QuietOpen(out initialState);

                value = this.Command.ExecuteNonQuery();
            }
            catch (Exception  x)
            {
                throw new DataAccessException(this.connectionString, this.commandText, x.Message, x);
            }
            finally
            {
                this.QuietClose(initialState);
            }
            return value;
        }

        public virtual object ExecuteScalar()
        {
            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }
            ConnectionState initialState = ConnectionState.Closed;
            object value;

            try
            {
                this.QuietOpen(out initialState);
                value = this.Command.ExecuteScalar();
            }
            catch (System.Exception x)
            {
                throw new DataAccessException(this.connectionString, this.commandText, x.Message, x);
            }
            finally
            {
                this.QuietClose(initialState);
            }

            return value;
        }

        public virtual DataSet ExecuteSql(String Sql)
        {
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }
            this.Command = new SqlCommand(Sql, this.Connection);
            SqlDataAdapter da = null;
            DataSet ds;
            try
            {
                da = new SqlDataAdapter(this.Command);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new DataAccessException(this.connectionString, this.commandText, sqlException.Message, sqlException);
            }
            finally
            {
                if ((da != null))
                {
                    da.Dispose();
                }
            }
            return ds;
        }

        public virtual XmlReader ExecuteXmlReader()
        {
            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }

            ConnectionState initialState = ConnectionState.Closed;
            XmlReader value;

            try
            {
                this.QuietOpen(out initialState);
                value = this.Command.ExecuteXmlReader();
            }
            catch (SqlException x)
            {
                throw new DataAccessException(this.connectionString, this.commandText, x.Message, x);
            }
            finally
            {
                this.QuietClose(initialState);
            }

            return value;
        }

        public virtual List<Hashtable> ExecuteHash(string Sql, params object[] pars)
        {
            List<Hashtable> resultSet = new List<Hashtable>();
            #region [ Connection Verification ] 
            if ((Connection == null))
            {
                if (this.DatabaseName == null)
                {
                    throw new ApplicationException("Call CreateConnection method before using the connection. Database Name is also blank.");
                }
            }
            #endregion

            try
            {
                this.Command = new SqlCommand(String.Format(Sql, pars),  this.Connection);
                this.Connection.Open();
                var reader = this.Command.ExecuteReader();
                int row_index = 0;

                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();

                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }

                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.Connection.Close();
            }
            return resultSet;
        }

        public virtual List<Hashtable> ExecuteHash(string sql)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {
                IDbCommand command = this.Connection.CreateCommand();
                command.CommandText = sql;

                this.Connection.Open();
                var reader = command.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.Connection.Close();
            }
            return resultSet;
        }

        public virtual List<Hashtable> ExecuteHash(string sql, params string[] pars)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {
                IDbCommand command = this.Connection.CreateCommand();
                command.CommandText = String.Format(sql, pars);

                this.Connection.Open();
                var reader = command.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.Connection.Close();
            }
            return resultSet;
        }

        public List<Hashtable> ExecuteHash()
        {
            List<Hashtable> results = new List<Hashtable>();

            if ((this.Command == null))
            {
                throw new ApplicationException("The command object is not defined.");
            }
            if ((Connection == null))
            {
                if (this.DatabaseName == null)
                {
                    throw new ApplicationException("Call CreateConnection method before using the connection. Database Name is also blank.");
                }
            }

            try
            {
                this.Connection.Open();
                var reader = this.Command.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    results.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            return results;
        }
        #endregion

        #region [ Transaction Methods ]
        public virtual void BeginTransaction()
        {
            if ((CurrentTransaction != null))
            {
                throw new ApplicationException("There is already a pending transaction. Can not start another one.");
            }
            if ((Connection == null))
            {
                throw new ApplicationException("Call CreateConnection method before using the connection.");
            }

            ConnectionState initialState;
            this.QuietOpen(out initialState);
            CurrentTransaction = Connection.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            if ((CurrentTransaction != null))
            {
                CurrentTransaction.Rollback();
                this.QuietClose(ConnectionState.Closed);
                CurrentTransaction.Dispose();
            }
            else
            {
                throw new ApplicationException("No transaction to Rollback.");
            }
        }

        public virtual void CommitTransaction()
        {
            if ((CurrentTransaction != null))
            {
                CurrentTransaction.Commit();
                this.QuietClose(ConnectionState.Closed);
                CurrentTransaction.Dispose();
            }
            else
            {
                throw new ApplicationException("No transaction to Commit.");
            }
        }
        #endregion

        #region [ Error Handle ]
        private void HandleError()
        {
        }
        #endregion

        #region [ Async Methods ]
 
        #endregion

        public virtual void Dispose()
        {
            if ((this.Command != null))
            {
                this.Command.Dispose();
            }

            if ((CurrentTransaction != null))
            {
                CurrentTransaction.Rollback();
                CurrentTransaction.Dispose();
            }

            if ((Connection != null))
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                Connection.Dispose();
            }
        }

        #region [ Local Methods ]
        private void QuietOpen(out ConnectionState initialState)
        {
            initialState = ConnectionState.Open;
            if ((this.Connection.State == ConnectionState.Closed))
            {
                initialState = ConnectionState.Closed;
                this.Connection.Open();
            }
        }

        private void QuietClose(ConnectionState initialState)
        {
            if ((initialState == ConnectionState.Closed))
            {
                this.Connection.Close();
            }
        }

        private object GetValue(object value)
        {
            if ((value == null))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(byte))
                        && (((byte)(value)) == byte.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(char))
                        && (((char)(value)) == char.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(decimal))
                        && (((decimal)(value)) == decimal.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(double))
                        && (((double)(value)) == double.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(float))
                        && (((float)(value)) == float.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(int))
                        && (((int)(value)) == int.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(long))
                        && (((long)(value)) == long.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(short))
                        && (((short)(value)) == short.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(string))
                        && (((string)(value)) == string.Empty)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(System.DateTime))
                        && (((System.DateTime)(value)) == System.DateTime.MinValue)))
            {
                return DBNull.Value;
            }
            if (((value.GetType() == typeof(System.Guid))
                        && (((System.Guid)(value)) == System.Guid.Empty)))
            {
                return DBNull.Value;
            }
            return value;
        }
        #endregion

        #region [ Factory Methods ] 
        public static DataAccess Create(string connection_string_name)
        {
            return new DataAccess(ConfigurationManager.ConnectionStrings[connection_string_name].ConnectionString);
        }
        #endregion
    }

    public static class DataAccessExtensions
    {
        public static Collection<DataTable> getTables(this DataSet ds)
        {
            var tables = new Collection<DataTable>();
            foreach (DataTable table in ds.Tables)
            {
                tables.Add(table);
            }

            return tables;
        }

        public static Collection<DataRow> getRows(this DataTable table)
        {
            var rows = new Collection<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return rows;
        }

        public static IEnumerable<Object[]> DataRecord(this IDataReader source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            while (source.Read())
            {
                var row = new Object[source.FieldCount];
                source.GetValues(row);
                yield return row;
            }
        }

        public static IEnumerable<T> DataReaderEnumerator<T, TReader>(this IDataReader source) where TReader : IReaderRow<T>, new()
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            IReaderRow<T> rowReader = new TReader { Reader = source };

            while (source.Read())
            {
                yield return rowReader.GetRowData();
            }
        }
    }

    public interface IReaderRow<T>
    {
        IDataReader Reader { get; set; }
        T GetRowData();
    }

}
