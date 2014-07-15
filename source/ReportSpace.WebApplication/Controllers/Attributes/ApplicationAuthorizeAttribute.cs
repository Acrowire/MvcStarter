using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportSpace.WebApplication.Controllers.Attributes
{
    using ReportSpace.Application.Security;
    using ReportSpace.WebApplication.Extensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        #region [ Consturctors ]
        public ApplicationAuthorizeAttribute(string roles = "")
            : base()
        {
            if (String.IsNullOrEmpty(roles) == false)
            {
                this.Roles = roles;
            }
        }
        #endregion

        /// <summary>
        /// Primary Role Authorization Methods
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool authorized = false;     
            authorized = this.Roles.Split(',')
                            .ToList()
                            .Where(role_name => httpContext.User.Identity.HasRole(role_name))
                            .Any();
                
            return authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //return Json 
                JsonResult result = new JsonResult();
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = result;
            }
            else
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult(urlHelper.Action("Unauthroized", "Home"));
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

    }
}