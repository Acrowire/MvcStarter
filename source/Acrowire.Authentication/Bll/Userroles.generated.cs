namespace Acrowire.Bll {
    
    
    public partial class Userroles {
        
        private System.Nullable<int> _id;
        
        private System.Nullable<int> _roleid;
        
        private System.Nullable<int> _userid;
        
        private System.Nullable<bool> _active;
        
        private Roles _roles;
        
        private Users _users;
        
        public virtual System.Nullable<int> Id {
            get {
                return _id;
            }
            set {
                _id = value;
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
        
        public virtual Roles Roles {
            get {
                if ((this._roles == null)) {
                    this._roles = Acrowire.Bll.Roles.Load(this._roleid);
                }
                return this._roles;
            }
            set {
                _roles = value;
            }
        }
        
        public virtual Users Users {
            get {
                if ((this._users == null)) {
                    this._users = Acrowire.Bll.Users.Load(this._userid);
                }
                return this._users;
            }
            set {
                _users = value;
            }
        }
        
        private void Clean() {
            this.Id = null;
            this._roleid = null;
            this._userid = null;
            this.Active = null;
            this.Roles = null;
            this.Users = null;
        }
        
        private void Fill(System.Data.DataRow dr) {
            this.Clean();
            if ((dr["Id"] != System.DBNull.Value)) {
                this.Id = ((System.Nullable<int>)(dr["Id"]));
            }
            if ((dr["RoleId"] != System.DBNull.Value)) {
                this._roleid = ((System.Nullable<int>)(dr["RoleId"]));
            }
            if ((dr["UserId"] != System.DBNull.Value)) {
                this._userid = ((System.Nullable<int>)(dr["UserId"]));
            }
            if ((dr["Active"] != System.DBNull.Value)) {
                this.Active = ((System.Nullable<bool>)(dr["Active"]));
            }
        }
        
        public static UserrolesCollection Select_UserRoless_By_UserId(System.Nullable<int> UserId) {
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                System.Data.DataSet ds = dbo.Select_UserRoless_By_UserId(UserId);
                UserrolesCollection collection = new UserrolesCollection();
                if (GlobalTools.IsSafeDataSet(ds)) {
                    for (int i = 0; (i < ds.Tables[0].Rows.Count); i = (i + 1)) {
                        Userroles obj = new Userroles();
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
        
        public static UserrolesCollection Select_UserRoless_By_RoleId(System.Nullable<int> RoleId) {
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                System.Data.DataSet ds = dbo.Select_UserRoless_By_RoleId(RoleId);
                UserrolesCollection collection = new UserrolesCollection();
                if (GlobalTools.IsSafeDataSet(ds)) {
                    for (int i = 0; (i < ds.Tables[0].Rows.Count); i = (i + 1)) {
                        Userroles obj = new Userroles();
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
        
        public static UserrolesCollection GetAll() {
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                System.Data.DataSet ds = dbo.UserRoles_Select_All();
                UserrolesCollection collection = new UserrolesCollection();
                if (GlobalTools.IsSafeDataSet(ds)) {
                    for (int i = 0; (i < ds.Tables[0].Rows.Count); i = (i + 1)) {
                        Userroles obj = new Userroles();
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
        
        public static Userroles Load(System.Nullable<int> Id) {
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                System.Data.DataSet ds = dbo.UserRoles_Select_One(Id);
                Userroles obj = null;
                if (GlobalTools.IsSafeDataSet(ds)) {
                    if ((ds.Tables[0].Rows.Count > 0)) {
                        obj = new Userroles();
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
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                System.Data.DataSet ds = dbo.UserRoles_Select_One(this.Id);
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
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                dbo.UserRoles_Insert(this._roleid, this._userid, this.Active);
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
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                dbo.UserRoles_Delete(this.Id);
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
            Acrowire.Dal.Userroles dbo = null;
            try {
                dbo = new Acrowire.Dal.Userroles();
                dbo.UserRoles_Update(this.Id, this._roleid, this._userid, this.Active);
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
