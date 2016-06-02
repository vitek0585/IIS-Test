using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MergeIdentity.Controllers
{
    public class ExceptionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            throw new ArgumentNullException("Is Null");
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

    }
}
