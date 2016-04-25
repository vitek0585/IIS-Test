using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogOutUser.Core
{
    public class AuthHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += context_AuthenticateRequest;

        }

        private void context_AuthenticateRequest(object sender, EventArgs e)
        {
            var auth = HttpContext.Current.GetOwinContext().Authentication;
            //if (auth != null && HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    auth.SignOut();
            //}
        }

        public void Dispose()
        {
        }
    }
}