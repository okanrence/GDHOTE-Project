using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using Newtonsoft.Json;

namespace GDHOTE.Hub.WebApi.OwinProvider
{
    public class UnAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent
                (JsonConvert.SerializeObject(
                    new Response
                    {
                        ErrorCode = "700",
                        ErrorMessage = "Your are not authorised to use this resource"
                        //ErrorMessage = "Authorization has been denied for this request."
                    }, Formatting.Indented))
            };
        }
    }
}