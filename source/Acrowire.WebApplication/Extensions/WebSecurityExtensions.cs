using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Acrowire.WebApplication.Extensions
{
    using Acrowire.Application;
    using Acrowire.Application.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;


    public static class WebSecurityExtensions
    {
        public static ApplicationUser User(this IIdentity identity)
        {
            ApplicationUser user = new ApplicationUser();
            
            user = MvcApplication.Security.FindByNameAsync(identity.GetUserName()).Result;

            return user;
        }

        public static Boolean HasRole(this IIdentity identity, String RoleName)
        {
            bool has_role = false;
            string user_public_id = identity.User().PublicId.ToString();

            try
            {
                var roles = MvcApplication.Security.GetRolesAsync(identity.User()).Result;
                has_role = roles.Where(r => r.ToLower() == RoleName.ToLower()).Any(); 
            }
            catch (ApplicationSecurityException app_exception)
            {
                // Assume failed Login 
            }

                                                
       
            return has_role;
        }

        #region [ Organizations ]
        
        #endregion
    }
}