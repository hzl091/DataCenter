using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DC.Web.Startup))]
namespace DC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
