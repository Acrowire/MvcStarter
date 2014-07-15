namespace ReportSpace.Dal {
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;
    using System.Xml;
    using ReportSpace.Data;
    
    
    public partial class Userroles : IDisposable {
        
        private DataAccess _dataAccess;
        
        public Userroles() {
            this._dataAccess = new DataAccess();
        }
        
        public virtual System.Data.DataSet Select_UserRoless_By_UserId(System.Nullable<int> UserId) {
            this._dataAccess.CreateProcedureCommand("sp_Select_UserRoless_By_UserId");
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Select_UserRoless_By_RoleId(System.Nullable<int> RoleId) {
            this._dataAccess.CreateProcedureCommand("sp_Select_UserRoless_By_RoleId");
            this._dataAccess.AddParameter("RoleId", RoleId, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet UserRoles_Select_All() {
            this._dataAccess.CreateProcedureCommand("sp_UserRoles_Select_All");
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet UserRoles_Select_One(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_UserRoles_Select_One");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Nullable<int> UserRoles_Insert(System.Nullable<int> RoleId, System.Nullable<int> UserId, System.Nullable<bool> Active) {
            this._dataAccess.CreateProcedureCommand("sp_UserRoles_Insert");
            this._dataAccess.AddParameter("RoleId", RoleId, ParameterDirection.Input);
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
            this._dataAccess.AddParameter("Active", Active, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> UserRoles_Delete(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_UserRoles_Delete");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> UserRoles_Update(System.Nullable<int> Id, System.Nullable<int> RoleId, System.Nullable<int> UserId, System.Nullable<bool> Active) {
            this._dataAccess.CreateProcedureCommand("sp_UserRoles_Update");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            this._dataAccess.AddParameter("RoleId", RoleId, ParameterDirection.Input);
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
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
