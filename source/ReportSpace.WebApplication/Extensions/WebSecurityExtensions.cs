using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ReportSpace.WebApplication.Extensions
{
    using ReportSpace.Application;
    using ReportSpace.Application.Security;
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

            // Cache 
            if(MvcApplication.UserRolesCache.ContainsKey(identity.GetUserName()) == false)
            {
                var roles = MvcApplication.Security.GetRolesAsync(identity.User()).Result;
                MvcApplication.UserRolesCache.TryAdd(identity.GetUserName(), roles.ToList());
            }
            // End Cache

            has_role = MvcApplication.UserRolesCache[identity.GetUserName()]
                                     .Where(r => r.ToLower() == RoleName.ToLower())
                                     .Any();                                     
       
            return has_role;
        }
    }
}