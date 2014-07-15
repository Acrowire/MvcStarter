using ReportSpace.Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections.Concurrent;

namespace ReportSpace.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ApplicationSecurityComponent Security
        {
            get
            {
                return new ApplicationSecurityComponent();
            }
        }

        public static ConcurrentDictionary<String, List<String>> UserRolesCache = new ConcurrentDictionary<string, List<string>>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
