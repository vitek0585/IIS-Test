using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LogOutUser.Core
{
    public class CustomActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            throw new Exception();
            base.OnActionExecuting(actionContext);
        }
    }
}