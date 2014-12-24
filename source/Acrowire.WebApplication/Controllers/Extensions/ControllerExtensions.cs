using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acrowire.WebApplication.Extensions;

namespace Acrowire.WebApplication.Controllers.Extensions
{
    using Application.Security;
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        public static ApplicationUser AppUser(this Controller controller)
        {
            ApplicationUser user = new ApplicationUser();
            Guid PublicId = Guid.Empty;

            if (HttpContext.Current != null)
            {
                user = HttpContext.Current.User.Identity.User();
            }

            return user;
        }
    }

    
}