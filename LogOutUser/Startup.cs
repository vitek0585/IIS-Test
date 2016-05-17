using LogOutUser.Controllers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogOutUser.Startup))]
namespace LogOutUser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var resolver = new DummyDependencyResolver();
            //var config = new HubConfiguration();
            //GlobalHost.DependencyResolver = resolver;
            //config.Resolver = resolver;

            ConfigureAuth(app);
            ConfigureSignalR(app);
            app.MapSignalR();


        }
    }
}
