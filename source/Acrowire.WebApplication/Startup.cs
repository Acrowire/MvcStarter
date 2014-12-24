using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Acrowire.WebApplication.Startup))]
namespace Acrowire.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
