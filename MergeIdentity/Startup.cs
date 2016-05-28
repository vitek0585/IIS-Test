using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MergeIdentity.Startup))]
namespace MergeIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
