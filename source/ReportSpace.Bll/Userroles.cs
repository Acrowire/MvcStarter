using ReportSpace.Bll.Exceptions;
using System;
namespace ReportSpace.Bll {
    
    
    public partial class Userroles 
    {
        public bool Create(Roles role, Users user)
        {
            bool created = false;

            try
            {
                // Assign local values
                this._userid = user.Id.Value;
                this._roleid = role.Id.Value;
                this._active = true;
                this.Insert();
                created = true;
            }
            catch (Exception x)
            {
                throw new UsersRolesException("Could not Create user-Role Mapping", x);
            }

            return created;
        }

        public Int32 RoleId
        {
            get { return this._roleid.Value; }
        }

        public Int32 UserId
        {
            get { return this._userid.Value; }
        }
    }
}
