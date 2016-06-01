using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mhg_internalnet2.Startup))]
namespace mhg_internalnet2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
