namespace ReportSpace.Dal {
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;
    using System.Xml;
    using ReportSpace.Data;
    
    
    public partial class Users : IDisposable {
        
        private DataAccess _dataAccess;
        
        public Users() {
            this._dataAccess = new DataAccess();
        }
        
        public virtual System.Data.DataSet Users_Select_All() {
            this._dataAccess.CreateProcedureCommand("sp_Users_Select_All");
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Users_Select_One(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Users_Select_One");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Nullable<int> Users_Insert(System.Nullable<System.Guid> PublicId, string UserName, string Email, System.Nullable<bool> Active, string PasswordHash) {
            this._dataAccess.CreateProcedureCommand("sp_Users_Insert");
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("UserName", UserName, ParameterDirection.Input);
            this._dataAccess.AddParameter("Email", Email, ParameterDirection.Input);
            this._dataAccess.AddParameter("Active", Active, ParameterDirection.Input);
            this._dataAccess.AddParameter("PasswordHash", PasswordHash, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> Users_Delete(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Users_Delete");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> Users_Update(System.Nullable<int> Id, System.Nullable<System.Guid> PublicId, string UserName, string Email, System.Nullable<bool> Active, string PasswordHash) {
            this._dataAccess.CreateProcedureCommand("sp_Users_Update");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("UserName", UserName, ParameterDirection.Input);
            this._dataAccess.AddParameter("Email", Email, ParameterDirection.Input);
            this._dataAccess.AddParameter("Active", Active, ParameterDirection.Input);
            this._dataAccess.AddParameter("PasswordHash", PasswordHash, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual void Dispose() {
            if ((this._dataAccess != null)) {
                this._dataAccess.Dispose();
            }
        }
    }
}
