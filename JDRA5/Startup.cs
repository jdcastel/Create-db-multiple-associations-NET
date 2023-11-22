using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JDRA5.Startup))]

namespace JDRA5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
