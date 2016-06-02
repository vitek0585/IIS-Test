using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
//using System.Web.Mvc;
//using IExceptionFilter = System.Web.Mvc.IExceptionFilter;

namespace MergeIdentity.EcxeptionHandling
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExceptionFilterAjaxAttribute : ExceptionFilterAttribute//, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var isAjaxRequest = actionExecutedContext.Request.Headers.Contains("My-Header");
            if (isAjaxRequest)
            {

                var res = actionExecutedContext.Exception.Message;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(res)
                };
                actionExecutedContext.Response = response;
            }
        }

        //public void OnException(ExceptionContext filterContext)
        //{

        //    var isAjaxRequest = filterContext.HttpContext.Request.Headers["My-Header"] != null;
        //    if (isAjaxRequest)
        //    {
        //        var res = filterContext.Exception.Message;
        //        filterContext.ExceptionHandled = true;
        //        filterContext.Result = new ContentResult()
        //        {
        //            Content = res
        //        };
        //    }
        //}
    }
}