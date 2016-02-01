using Microsoft.Owin;
using Owin;
using UniversitySite;

[assembly: OwinStartup(typeof (Startup))]

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