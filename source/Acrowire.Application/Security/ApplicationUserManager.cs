using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Microsoft.AspNet.Identity;


namespace Acrowire.Application.Security
{
    public class ApplicationUserManager : Microsoft.AspNet.Identity.UserManager<ApplicationUser>
    {
        #region [ Fields ] 
        #endregion

        #region [ Constructor ]
        public ApplicationUserManager(ApplicationSecurityComponent comp)
            : base(comp)
        {
            
        }
        #endregion

        #region [ override ]
        
        public override Task<Microsoft.AspNet.Identity.IdentityResult> CreateAsync(ApplicationUser user)
        {
            return base.CreateAsync(user);
        }

        public override Task<Microsoft.AspNet.Identity.IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            Microsoft.AspNet.Identity.IdentityResult result = new Microsoft.AspNet.Identity.IdentityResult();
            var comp = new ApplicationSecurityComponent();
            try
            {
                user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);
                comp.CreateAsync(user);
                result = IdentityResult.Success;
            }
            catch (Exception x)
            {
                result = IdentityResult.Failed(x.Message);
            }

            return Task.FromResult(result);
        }

        public override Task<Microsoft.AspNet.Identity.IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var comp = new ApplicationSecurityComponent();

            return comp.UpdateUserAsync(user);
        }

        public Task<Microsoft.AspNet.Identity.IdentityResult> SetPassword(Guid PublicId, String new_password)
        {
            Microsoft.AspNet.Identity.IdentityResult result = IdentityResult.Failed();
            string new_password_hashed = PasswordHasher.HashPassword(new_password);
            var comp = new ApplicationSecurityComponent();
            var user = FindByIdAsync(PublicId.ToString()).Result;

            comp.SetPasswordHashAsync(user, new_password_hashed);
            result = IdentityResult.Success;

            return Task.FromResult(result);
        }
        #endregion

        #region [ Local Methods ] 
        #endregion
    }
}
