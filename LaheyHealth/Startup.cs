using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaheyHealth.Startup))]
namespace LaheyHealth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
