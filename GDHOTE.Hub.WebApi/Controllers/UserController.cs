using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using Newtonsoft.Json;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("start-reset-password")]
        public HttpResponseMessage RequestResetPassword(PasswordResetRequest resetRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
              
                var response = UserService.RequestResetPassword(resetRequest);
                if (response != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
    }
}
