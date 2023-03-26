using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoanNhom10.Startup))]
namespace DoanNhom10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
