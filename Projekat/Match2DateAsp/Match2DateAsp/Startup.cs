using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Match2DateAsp.Startup))]
namespace Match2DateAsp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
