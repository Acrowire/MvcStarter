using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace.Data
{
    public class DataAccessException : Exception
    {


        #region [ Constructors ] 
        public DataAccessException(String ConnectionString, String CommandText, String ErrorMessage, Exception inner) : base(ErrorMessage, inner)
        {
            this.ConnectionString = ConnectionString;
            this.CommandText = CommandText;
            ///
            /// Build Sql Error Excpetion Handling
            ///
            if (inner is System.Data.SqlClient.SqlException)
            {
                var sql_exception = (System.Data.SqlClient.SqlException)inner;
                var sql_error_detail = "<sql_error>";

                foreach (SqlError sql_error in sql_exception.Errors)
                {
                    sql_error_detail = sql_error_detail + String.Format("<Class>{0}</Class>", sql_error.Class);
                    sql_error_detail = sql_error_detail + String.Format("<LineNumber>{0}</LineNumber>", sql_error.LineNumber);
                    sql_error_detail = sql_error_detail + String.Format("<Message>{0}</Message>", sql_error.Message);
                    sql_error_detail = sql_error_detail + String.Format("<Number>{0}</Number>", sql_error.Number);
                    sql_error_detail = sql_error_detail + String.Format("<Procedure>{0}</Procedure>", sql_error.Procedure);
                    sql_error_detail = sql_error_detail + String.Format("<Server>{0}</Source>", sql_error.Server);
                    sql_error_detail = sql_error_detail + String.Format("<State>{0}</State>", sql_error.State);
                }
                sql_error_detail = sql_error_detail + "</sql_error>";
                this.Data.Add("sql_error_ml", sql_error_detail);
            }

            
        }
        #endregion

        public string ConnectionString { get; set; }

        public string CommandText { get; set; }
    }

    
}
