namespace Acrowire.Bll {
    
    
    public partial class Roles {
        
        private System.Nullable<int> _id;
        
        private System.Nullable<System.Guid> _publicid;
        
        private string _name;
        
        private System.Nullable<bool> _active;
        
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
        
        public virtual string Name {
            get {
                return _name;
            }
            set {
                _name = value;
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
        
        public virtual UserrolesCollection UserrolesCollection {
            get {
                if ((this._userrolesCollection == null)) {
                    _userrolesCollection = Acrowire.Bll.Userroles.Select_UserRoless_By_UserId(this.Id);
                }
                return this._userrolesCollection;
            }
        }
        
        private void Clean() {
            this.Id = null;
            this.Publicid = null;
            this.Name = string.Empty;
            this.Active = null;
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
            if ((dr["Name"] != System.DBNull.Value)) {
                this.Name = ((string)(dr["Name"]));
            }
            if ((dr["Active"] != System.DBNull.Value)) {
                this.Active = ((System.Nullable<bool>)(dr["Active"]));
            }
        }
        
        public static RolesCollection GetAll() {
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                System.Data.DataSet ds = dbo.Roles_Select_All();
                RolesCollection collection = new RolesCollection();
                if (GlobalTools.IsSafeDataSet(ds)) {
                    for (int i = 0; (i < ds.Tables[0].Rows.Count); i = (i + 1)) {
                        Roles obj = new Roles();
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
        
        public static Roles Load(System.Nullable<int> Id) {
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                System.Data.DataSet ds = dbo.Roles_Select_One(Id);
                Roles obj = null;
                if (GlobalTools.IsSafeDataSet(ds)) {
                    if ((ds.Tables[0].Rows.Count > 0)) {
                        obj = new Roles();
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
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                System.Data.DataSet ds = dbo.Roles_Select_One(this.Id);
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
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                dbo.Roles_Insert(this.Publicid, this.Name, this.Active);
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
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                dbo.Roles_Delete(this.Id);
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
            Acrowire.Dal.Roles dbo = null;
            try {
                dbo = new Acrowire.Dal.Roles();
                dbo.Roles_Update(this.Id, this.Publicid, this.Name, this.Active);
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
