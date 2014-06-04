using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Samples_Activity.Startup))]
namespace Samples_Activity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
