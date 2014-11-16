using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(emlakCenter.Startup))]
namespace emlakCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
