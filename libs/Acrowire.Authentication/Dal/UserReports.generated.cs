using Acrowire.Data;

namespace Acrowire.Dal {
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Data;
    using System.Xml;
    
    
    public partial class Userreports : IDisposable {
        
        private DataAccess _dataAccess;
        
        public Userreports() {
            this._dataAccess = new DataAccess();
        }
        
        public virtual System.Data.DataSet Select_UserReportss_By_ReportId(System.Nullable<int> ReportId) {
            this._dataAccess.CreateProcedureCommand("sp_Select_UserReportss_By_ReportId");
            this._dataAccess.AddParameter("ReportId", ReportId, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet Select_UserReportss_By_UserId(System.Nullable<int> UserId) {
            this._dataAccess.CreateProcedureCommand("sp_Select_UserReportss_By_UserId");
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet UserReports_Select_All() {
            this._dataAccess.CreateProcedureCommand("sp_UserReports_Select_All");
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Data.DataSet UserReports_Select_One(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_UserReports_Select_One");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            DataSet value = this._dataAccess.ExecuteDataSet();
            return value;
        }
        
        public virtual System.Nullable<int> UserReports_Insert(System.Nullable<int> UserId, System.Nullable<int> ReportId) {
            this._dataAccess.CreateProcedureCommand("sp_UserReports_Insert");
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
            this._dataAccess.AddParameter("ReportId", ReportId, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> UserReports_Delete(System.Nullable<int> Id) {
            this._dataAccess.CreateProcedureCommand("sp_UserReports_Delete");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            int value = this._dataAccess.ExecuteNonQuery();
            return value;
        }
        
        public virtual System.Nullable<int> UserReports_Update(System.Nullable<int> Id, System.Nullable<int> UserId, System.Nullable<int> ReportId) {
            this._dataAccess.CreateProcedureCommand("sp_UserReports_Update");
            this._dataAccess.AddParameter("Id", Id, ParameterDirection.Input);
            this._dataAccess.AddParameter("UserId", UserId, ParameterDirection.Input);
            this._dataAccess.AddParameter("ReportId", ReportId, ParameterDirection.Input);
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
