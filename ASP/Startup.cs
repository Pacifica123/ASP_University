using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP.Startup))]
namespace ASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
