using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using LogOutUser.Models;
using LogOutUser.Models.Entity;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LogOutUser
{
    public partial class Startup
    {
        public static void ConfigureSignalR(IAppBuilder app)
        {
            var kernel = new StandardKernel(new SignalRModule());
            var resolver = new NinjectSignalRDependencyResolver(kernel);
            var config = new HubConfiguration();
            GlobalHost.DependencyResolver = resolver;
            config.Resolver = resolver;
            app.MapSignalR(config);
        }
    }
    public class SignalRModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MyContext>().ToSelf().InRequestScope();
            
        }
    }

    class MyContext
    {
       public CategoryContext CategoryContext;

        public MyContext(CategoryContext categoryContext)
        {
            CategoryContext = categoryContext;
        }

    }

    internal class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }


    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, AspNetUser>(
                        validateInterval: TimeSpan.FromSeconds(5),
                        regenerateIdentity: (manager, user) =>
                        {
                            return user.GenerateUserIdentityAsync(manager);
                        })
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        }
    }
}