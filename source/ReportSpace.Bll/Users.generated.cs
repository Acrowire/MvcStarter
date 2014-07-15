namespace ReportSpace.Bll {
    
    
    public partial class Users {
        
        private System.Nullable<int> _id;
        
        private System.Nullable<System.Guid> _publicid;
        
        private string _username;
        
        private string _email;
        
        private System.Nullable<bool> _active;
        
        private string _passwordhash;
        
        private UserrolesCollection _userrolesCollection;
        
        public virtual System.Nullable<int> Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        
        public virtual System.Nullable<System.Guid> Publicid {
            get {
                return _publicid;
            }
            set {
                _publicid = value;
            }
        }
        
        public virtual string Username {
            get {
                return _username;
            }
            set {
                _username = value;
            }
        }
        
        public virtual string Email {
            get {
                return _email;
            }
            set {
                _email = value;
            }
        }
        
        public virtual System.Nullable<bool> Active {
            get {
                return _active;
            }
            set {
                _active = value;
            }
        }
        
        public virtual string Passwordhash {
            get {
                return _passwordhash;
            }
            set {
                _passwordhash = value;
            }
        }
        
        public virtual UserrolesCollection UserrolesCollection {
            get {
                if ((this._userrolesCollection == null)) {
                    _userrolesCollection = ReportSpace.Bll.Userroles.Select_UserRoless_By_RoleId(this.Id);
                }
                return this._userrolesCollection;
            }
        }
        
        private void Clean() {
            this.Id = null;
            this.Publicid = null;
            this.Username = string.Empty;
            this.Email = string.Empty;
            this.Active = null;
            this.Passwordhash = string.Empty;
            this._userrolesCollection = null;
        }
        
        private void Fill(System.Data.DataRow dr) {
            this.Clean();
            if ((dr["Id"] != System.DBNull.Value)) {
                this.Id = ((System.Nullable<int>)(dr["Id"]));
            }
            if ((dr["PublicId"] != System.DBNull.Value)) {
                this.Publicid = ((System.Nullable<System.Guid>)(dr["PublicId"]));
            }
            if ((dr["UserName"] != System.DBNull.Value)) {
                this.Username = ((string)(dr["UserName"]));
            }
            if ((dr["Email"] != System.DBNull.Value)) {
                this.Email = ((string)(dr["Email"]));
            }
            if ((dr["Active"] != System.DBNull.Value)) {
                this.Active = ((System.Nullable<bool>)(dr["Active"]));
            }
            if ((dr["PasswordHash"] != System.DBNull.Value)) {
                this.Passwordhash = ((string)(dr["PasswordHash"]));
            }
        }
        
        public static UsersCollection GetAll() {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                System.Data.DataSet ds = dbo.Users_Select_All();
                UsersCollection collection = new UsersCollection();
                if (GlobalTools.IsSafeDataSet(ds)) {
                    for (int i = 0; (i < ds.Tables[0].Rows.Count); i = (i + 1)) {
                        Users obj = new Users();
                        obj.Fill(ds.Tables[0].Rows[i]);
                        if ((obj != null)) {
                            collection.Add(obj);
                        }
                    }
                }
                return collection;
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
        
        public static Users Load(System.Nullable<int> Id) {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                System.Data.DataSet ds = dbo.Users_Select_One(Id);
                Users obj = null;
                if (GlobalTools.IsSafeDataSet(ds)) {
                    if ((ds.Tables[0].Rows.Count > 0)) {
                        obj = new Users();
                        obj.Fill(ds.Tables[0].Rows[0]);
                    }
                }
                return obj;
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
        
        public virtual void Load() {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                System.Data.DataSet ds = dbo.Users_Select_One(this.Id);
                if (GlobalTools.IsSafeDataSet(ds)) {
                    if ((ds.Tables[0].Rows.Count > 0)) {
                        this.Fill(ds.Tables[0].Rows[0]);
                    }
                }
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
        
        public virtual void Insert() {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                dbo.Users_Insert(this.Publicid, this.Username, this.Email, this.Active, this.Passwordhash);
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
        
        public virtual void Delete() {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                dbo.Users_Delete(this.Id);
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
        
        public virtual void Update() {
            ReportSpace.Dal.Users dbo = null;
            try {
                dbo = new ReportSpace.Dal.Users();
                dbo.Users_Update(this.Id, this.Publicid, this.Username, this.Email, this.Active, this.Passwordhash);
            }
            catch (System.Exception ) {
                throw;
            }
            finally {
                if ((dbo != null)) {
                    dbo.Dispose();
                }
            }
        }
    }
}
