using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogOutUser.Startup))]
namespace LogOutUser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
