using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kommunikationsverktyg.Startup))]
namespace Kommunikationsverktyg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
