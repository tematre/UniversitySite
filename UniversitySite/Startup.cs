using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversitySite.Startup))]
namespace UniversitySite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
