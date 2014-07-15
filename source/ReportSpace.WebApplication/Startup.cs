using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportSpace.WebApplication.Startup))]
namespace ReportSpace.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
