using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Owin;
using IDependencyResolver = Microsoft.AspNet.SignalR.IDependencyResolver;

namespace LogOutUser.Controllers
{
    //public partial class Startup
    //{
    //    public static void ConfigureSignalR(IAppBuilder app)
    //    {
    //        GlobalHost.DependencyResolver = new DummyDependencyResolver();
    //    }
    //}
    public class DummyDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (serviceType.Equals(typeof(CategoryContext)))
            {
                return new CategoryContext();
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return null;
        }

        public void Register(Type serviceType, Func<object> activator)
        {
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
        }

        public void Dispose()
        {
        }
    }
}