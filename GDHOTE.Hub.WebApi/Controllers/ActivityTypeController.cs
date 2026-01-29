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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "activity")]
    public class ActivityTypeController : ApiController
    {
        [HttpGet]
        [Route("get-all-activity-types")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetAllActivityTypes()
        {
            try
            {
                var response = ActivityTypeService.GetAllActivityTypes().ToList();
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
        [Route("get-active-activity-types")]
        [UnAuthorized()]
        public HttpResponseMessage GetActivityTypes()
        {
            try
            {
                var response = ActivityTypeService.GetActivityTypes().ToList();
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
        [Route("get-activity-type")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetActivityType(string id)
        {
            try
            {
                var response = ActivityTypeService.GetActivityType(id);
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
        [Route("create-activity-type")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage CreateActivityType(CreateActivityTypeRequest createRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                string username = User.Identity.Name;
                var response = ActivityTypeService.CreateActivityType(createRequest, username);
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
        [Route("update-activity-type")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage UpdateActivityType(UpdateActivityTypeRequest updateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                string username = User.Identity.Name;
                var response = ActivityTypeService.UpdateActivityType(updateRequest, username);
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
        [Route("delete-activity-type")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage DeleteActivityType(string id)
        {
            try
            {
                string username = User.Identity.Name;
                var response = ActivityTypeService.Delete(id, username);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
    }
}
