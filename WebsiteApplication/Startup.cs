using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteApplication.Startup))]
namespace WebsiteApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
