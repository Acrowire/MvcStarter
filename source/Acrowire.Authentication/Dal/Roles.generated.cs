namespace Acrowire.Dal {
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;
    using System.Xml;
    using Acrowire.Data;
    
    
    public partial class Roles : IDisposable {
        
        private DataAccess _dataAccess;
        
        public Roles() {
            this._dataAccess = new DataAccess();
        }
        
        public virtual System.Data.DataSet Roles_Select_All() {
            this._dataAccess.CreateProcedureCommand("sp_Roles_Select_All");
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Roles_Select_One(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Roles_Select_One");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Nullable<int> Roles_Insert(System.Nullable<System.Guid> PublicId, string Name, System.Nullable<bool> Active) {
            this._dataAccess.CreateProcedureCommand("sp_Roles_Insert");
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("Name", Name, ParameterDirection.Input);
            this._dataAccess.AddParameter("Active", Active, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> Roles_Delete(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Roles_Delete");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> Roles_Update(System.Nullable<int> Id, System.Nullable<System.Guid> PublicId, string Name, System.Nullable<bool> Active) {
            this._dataAccess.CreateProcedureCommand("sp_Roles_Update");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("Name", Name, ParameterDirection.Input);
            this._dataAccess.AddParameter("Active", Active, ParameterDirection.Input);
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
