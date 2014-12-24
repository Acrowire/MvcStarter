using Acrowire.Data;

namespace Acrowire.Dal {
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;
    using System.Xml;
    
    
    public partial class Reports : IDisposable {
        
        private DataAccess _dataAccess;
        
        public Reports() {
            this._dataAccess = new DataAccess();
        }
        
        public virtual System.Data.DataSet Select_Reportss_By_OrganizationId(System.Nullable<int> OrganizationId) {
            this._dataAccess.CreateProcedureCommand("sp_Select_Reportss_By_OrganizationId");
            this._dataAccess.AddParameter("OrganizationId", OrganizationId, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Reports_Select_All() {
            this._dataAccess.CreateProcedureCommand("sp_Reports_Select_All");
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Reports_Select_One(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Reports_Select_One");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual void Reports_Insert(System.Nullable<System.Guid> PublicId, string Name, string Controller, string Action, System.Nullable<int> OrganizationId) {
            this._dataAccess.CreateProcedureCommand("sp_Reports_Insert");
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("Name", Name, ParameterDirection.Input);
            this._dataAccess.AddParameter("Controller", Controller, ParameterDirection.Input);
            this._dataAccess.AddParameter("Action", Action, ParameterDirection.Input);
            this._dataAccess.AddParameter("OrganizationId", OrganizationId, ParameterDirection.Input);
            this._dataAccess.ExecuteNonQuery();
        }
        
        public virtual System.Nullable<int> Reports_Delete(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_Reports_Delete");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> Reports_Update(System.Nullable<int> Id, System.Nullable<System.Guid> PublicId, string Name, string Controller, string Action, System.Nullable<int> OrganizationId) {
            this._dataAccess.CreateProcedureCommand("sp_Reports_Update");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            this._dataAccess.AddParameter("PublicId", PublicId, ParameterDirection.Input);
            this._dataAccess.AddParameter("Name", Name, ParameterDirection.Input);
            this._dataAccess.AddParameter("Controller", Controller, ParameterDirection.Input);
            this._dataAccess.AddParameter("Action", Action, ParameterDirection.Input);
            this._dataAccess.AddParameter("OrganizationId", OrganizationId, ParameterDirection.Input);
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
