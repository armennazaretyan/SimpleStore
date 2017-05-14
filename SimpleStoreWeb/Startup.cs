using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleStoreWeb.Startup))]
namespace SimpleStoreWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
