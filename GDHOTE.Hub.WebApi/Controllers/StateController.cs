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
using GDHOTE.Hub.WebApi.OwinProvider;
using Newtonsoft.Json;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "state")]
    public class StateController : ApiController
    {
        [HttpGet]
        [Route("get-all-states")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetAllStates()
        {
            try
            {
                var response = StateService.GetAllStates().ToList();
                if (response.Count > 0)
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

        [HttpGet]
        [Route("get-active-states")]
        [UnAuthorized]
        public HttpResponseMessage GetActiveStates()
        {
            try
            {
                var response = StateService.GetActiveStates().ToList();
                if (response.Count > 0)
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

        [HttpGet]
        [Route("get-state")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetState(string id)
        {
            try
            {
                var response = StateService.GetState(Convert.ToInt16(id));
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


        [HttpPost]
        [Route("create-state")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage CreateState(CreateStateRequest createRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                string username = User.Identity.Name;
                var response = StateService.CreateState(createRequest, username);
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


        [HttpPost]
        [Route("delete-state")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage DeleteState(int id)
        {
            try
            {
                string username = User.Identity.Name;
                var response = StateService.Delete(Convert.ToInt16(id), username);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
    }
}
