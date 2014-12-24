using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Security
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Acrowire.Bll;

    public class ApplicationSecurityComponent 
                : IUserStore<ApplicationUser>,
                  IUserPasswordStore<ApplicationUser>,
                  IRoleStore<ApplicationRole>,
                  IUserRoleStore<ApplicationUser>
    {
        #region [ Fields ] 
        private bool m_disposed;
        #endregion

        #region [ local ]
        private Bll.Users ToUser(ApplicationUser user)
        {
            Bll.Users _user = new Users();

            try
            {
                if (user.PublicId != Guid.Empty)
                {
                    _user = Bll.Users.GetById(user.PublicId);
                }
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Error converting user", x);
            }


            return _user;
        }

        private Bll.Roles ToRole(ApplicationRole role)
        {
            Bll.Roles r = new Roles()
            {
                 Publicid = Guid.Parse(role.Id),
                 Name = role.Name
            };

            return r;
        }
        #endregion

        #region [ Constructors ]
        public ApplicationSecurityComponent()
        {

        }
        #endregion

        #region IDisposable Members

        private void ThrowIfDisposed()
        {
            if (this.m_disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        private void CheckDisposed(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentException("user");
            }
        }

        public void Dispose()
        {
            this.m_disposed = true;
        }

        #endregion

        #region IUserStore<ApplicationUser,string> Members

        public Task CreateAsync(ApplicationUser user)
        {
            CheckDisposed(user);

            try
            {
                Bll.Users _user = new Users()
                {
                    Active = true,
                    Email = user.Email,
                    Publicid = Guid.NewGuid(),
                    Username = user.UserName,
                    Passwordhash = user.PasswordHash
                };

                user.PublicId = _user.Publicid??Guid.Empty;

                if (_user.Exists() == false)
                {
                    _user.Insert();
                }
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Failed creating user", x);
            }

            return Task.FromResult(1);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            CheckDisposed(user);

            try
            {
                var _user = this.ToUser(user);

                if (_user.Exists() == false)
                {
                    throw new ApplicationSecurityException(this.GetObjectContext(), "User with that username/email doest not exist", null);
                }

                Users.GetByEmail(user.Email).Delete();
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not delete user", x);
            }

            return Task.FromResult(1);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            ApplicationUser user = new ApplicationUser();

            try
            {
                // Load user info from the Database
                var _user = Users.GetById(Guid.Parse(userId));

                user = new ApplicationUser(_user.Publicid.Value)
                {
                    Email = _user.Email,
                    UserName = _user.Username,
                    PasswordHash = _user.Passwordhash
                };
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Could not Find user by id", x);
            }

            return Task.FromResult(user);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            ApplicationUser user = new ApplicationUser();

            try
            {
                // Load user info from the Database
                var _user = Users.GetByUserName(userName);

                if (_user.Publicid != null)
                {

                    user = new ApplicationUser(_user.Publicid.Value)
                    {
                        Email = _user.Email,
                        UserName = _user.Username,
                        PasswordHash = _user.Passwordhash
                    };
                }
            }
            catch (Exception x)
            {
                //throw new ApplicationSecurityException(this.GetObjectContext(), "Could not Find user by username", x);
            }

            return Task.FromResult(user);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            Microsoft.AspNet.Identity.IdentityResult result = new IdentityResult(new[] { "none" });
            try
            {
                var _user = this.ToUser(user);

                if (_user.Exists())
                {
                    _user.Update();
                    result = new IdentityResult(new string[0]);

                }
                else
                {
                    result = new IdentityResult(new string[1] { "Could not update user." });
                    //throw new ApplicationSecurityException(this.GetObjectContext(), "Could not update user, user does not exist", new ArgumentException("User does not exist"));
                }
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Could not update user", x);
            }

            return Task.FromResult(result);
        }

        public Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            Microsoft.AspNet.Identity.IdentityResult result = new IdentityResult(new[] { "none" });

            try
            {
                var _user = this.ToUser(user);

                if (_user.Exists())
                {
                    _user.Username = user.UserName;
                    _user.Email = user.Email;
                    _user.Update();

                    result = IdentityResult.Success;
                }
                else
                {
                    result = IdentityResult.Failed("User does not exist");
                }
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Could not update user", x);
            }

            return Task.FromResult(result);
        }

        public Task<IdentityResult> UpdateUserFullAsync(ApplicationUser user)
        {
            Microsoft.AspNet.Identity.IdentityResult result = new IdentityResult(new[] { "none" });

            try
            {
                var _user = this.ToUser(user);

                if (_user.Exists())
                {
                    _user.Username = user.UserName;
                    _user.Email = user.Email;
                    _user.Passwordhash = user.PasswordHash;
                    _user.Update();

                    result = IdentityResult.Success;
                }
                else
                {
                    result = IdentityResult.Failed("User does not exist");
                }
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Could not update user", x);
            }

            return Task.FromResult(result);
        }
        #endregion

        #region IUserPasswordStore<ApplicationUser,string> Members

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            CheckDisposed(user);
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            CheckDisposed(user);
            return Task.FromResult<bool>(user.PasswordHash != null || String.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            CheckDisposed(user);
            user.PasswordHash = passwordHash;

            var _user = this.ToUser(user);
            _user.Passwordhash = passwordHash;
            _user.Update();

            return Task.FromResult(0);
        }

        #endregion

        #region IRoleStore<ApplicationRole,string> Members

        public Task CreateAsync(ApplicationRole role)
        {
            var _role = this.ToRole(role);

            if (_role.Exists())
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not create Role, this role already exists", null);
            }
            
            try
            {
                _role.Insert();
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not create role", x);
            }

            return Task.FromResult(1);
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            var _role = this.ToRole(role);

            if (_role.Exists() == false)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not delete Role, this role does not exist", null);
            }

            try
            {
                _role.Delete();
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not delete role", x);
            }

            return Task.FromResult(1);
        }

        Task<ApplicationRole> IRoleStore<ApplicationRole, string>.FindByIdAsync(string roleId)
        {
            ApplicationRole role = new ApplicationRole();

            try
            {
                var _role = Roles.GetById(Guid.Parse(roleId));

                role = new ApplicationRole(_role.Publicid.Value)
                {
                    Name = _role.Name
                };
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not find by Id role", x);
            }

            return Task.FromResult(role);
        }

        Task<ApplicationRole> IRoleStore<ApplicationRole, string>.FindByNameAsync(string roleName)
        {
            ApplicationRole role = new ApplicationRole();

            try
            {
                var _role = Roles.GetByName(roleName);

                role = new ApplicationRole(_role.Publicid.Value)
                {
                    Name = _role.Name
                };
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not find by Id role", x);
            }

            return Task.FromResult(role);
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            try
            {
                var _role = Roles.GetById(Guid.Parse(role.Id));
                _role.Name = role.Name;
                _role.Update();
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not update role", x);
            }

            return Task.FromResult(1);
        }

        #endregion

        #region IUserRoleStore<ApplicationUser,string> Members

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var _user = ToUser(user);
            if (_user.Exists() == false)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not add user to role, user does not exist", null);
            }

            try
            {
                Userroles role_assignment = new Userroles();
                var _role = Bll.Roles.GetByName(roleName);
                    _user = Bll.Users.GetById(user.PublicId);
                role_assignment.Create(_role, _user);
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not add user to role", x);
            }

            return Task.FromResult(1);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            IList<string> userRoles = new List<string>();
            var _user = Bll.Users.GetByUserName(user.UserName);

            if (_user.Exists() == false)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not get roles for user, user does not exist", null);
            }

            try
            {
                var _userRoles = Bll.Userroles.Select_UserRoless_By_UserId(_user.Id);

                userRoles = _userRoles.Select(ur => ur.RoleName).ToList();
            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not get roles for user", x);
            }

            return Task.FromResult(userRoles);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            bool is_in_role = false;
            var _user = ToUser(user);
            if (_user.Exists() == false)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not determine if user is in role, user does not exist", null);
            }

            try
            {

            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not determine if user is in role", x);
            }

            return Task.FromResult(is_in_role) ;
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var _user = ToUser(user);
            if (_user.Exists() == false)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not remove user from role, user does not exist", null);
            }

            try
            {

            }
            catch (Exception x)
            {
                throw new ApplicationSecurityException(this.GetObjectContext(), "Can not remove user from role", x);
            }

            return Task.FromResult(0);
        }

        #endregion
    }
}
