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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "publication")]
    public class PublicationController : ApiController
    {
        [HttpGet]
        [Route("get-all-publications")]
        [UnAuthorized]
        public HttpResponseMessage GetAllPublications()
        {
            try
            {
                var response = PublicationService.GetAllPublications().ToList();
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
        [Route("get-active-publications")]
        [UnAuthorized]
        public HttpResponseMessage GetActivePublications()
        {
            try
            {
                var response = PublicationService.GetActivePublications().ToList();
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
        [Route("get-publication")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetPublication(string id)
        {
            try
            {
                var response = PublicationService.GetPublication(Convert.ToInt16(id));
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

        [HttpGet]
        [Route("get-publications-by-category")]
        [UnAuthorized]
        public HttpResponseMessage GetPublicationsByCategoryId(string id)
        {
            try
            {
                var response = PublicationService.GetPublicationsByCategoryId(Convert.ToInt16(id));
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
        [Route("create-publication")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage CreatePublication(CreatePublicationRequest createRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                string username = User.Identity.Name;
                var response = PublicationService.CreatePublication(createRequest, username);
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
        [Route("delete-publication")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage DeletePublication(string id)
        {
            try
            {
                string username = User.Identity.Name;
                var response = PublicationService.Delete(Convert.ToInt16(id), username);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
        

        [HttpPost]
        [Route("mail-publication")]
        [UnAuthorized]
        public HttpResponseMessage MailPublication(MailPublicationRequest mailRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                string username = User.Identity.Name;
                var response = PublicationService.MailPublication(mailRequest, username);
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
