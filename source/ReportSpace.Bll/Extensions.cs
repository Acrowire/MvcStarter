using ReportSpace.Bll.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace.Bll
{
    public static class Extensions
    {
        #region [ Users ]
        public static bool Exists(this Users user)
        {
            bool exists = false;

            try
            {
                if (user.Id.HasValue)
                {

                    exists =  Bll.Users.GetAll()
                                      .Where(u => u.Active == true)
                                      .Where(u => u.Id.Value == user.Id.Value)
                                      .Any();
                    return exists;
                }

                if (user.Publicid.HasValue)
                {
                    exists = Bll.Users.GetAll()
                                      .Where(u => u.Active == true)
                                      .Where(u => u.Publicid.Value == user.Publicid.Value)
                                      .Any();
                    return exists;
                }

                if (String.IsNullOrEmpty(user.Email) == false)
                {
                    exists = Bll.Users.GetAll()
                                      .Where(u => u.Active == true)
                                      .Where(u => u.Email == u.Email)
                                      .Any();
                    return exists;
                }

                if (String.IsNullOrEmpty(user.Username) == false)
                {
                    exists = Bll.Users.GetAll()
                                      .Where(u => u.Active == true)
                                      .Where(u => u.Username == u.Username)
                                      .Any();
                    return exists;
                }
                
            }
            catch (Exception x)
            {
                throw new UsersException("could not determine if user exists", x, user.Publicid.Value);
            }

            return exists;
        }
        #endregion

        #region [ Roles ] 
        public static bool Exists(this Roles role)
        {
            bool exists = false;

            try
            {
                if (role.Publicid.HasValue)
                {
                    return Bll.Roles.GetAll()
                                      .Where(r => r.Active == true)
                                      .Where(r => r.Publicid.Value == role.Publicid.Value)
                                      .Any();
                }

                if (role.Id.HasValue)
                {
                    return Bll.Roles.GetAll()
                                    .Where(r => r.Active == true)
                                    .Where(r => r.Id.Value == role.Id.Value)
                                    .Any();
                }

                if (String.IsNullOrEmpty(role.Name) == false)
                {
                    return Bll.Roles.GetAll()
                                    .Where(r => r.Active == true)
                                    .Where(r => r.Name.ToLower() == role.Name.ToLower())
                                    .Any();
                }
            }
            catch (Exception x)
            {
                throw new RolesException("Could not verify if Role exists", x);
            }

            return exists;
        }
        #endregion

        #region [ User Roles ]
        #endregion
    }
}
