using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CarModificationCenter.Startup))]

namespace CarModificationCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}