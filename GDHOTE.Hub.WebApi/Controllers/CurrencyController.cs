using System;
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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "currency")]
    public class CurrencyController : ApiController
    {
        [HttpGet]
        [Route("get-all-currencies")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetAllCurrencies()
        {
            try
            {
                var response = CurrencyService.GetAllCurrencies().ToList();
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
        [Route("get-active-currencies")]
        [UnAuthorized]
        public HttpResponseMessage GetActiveCurrencies()
        {
            try
            {
                var response = CurrencyService.GetActiveCurrencies().ToList();
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
        [Route("get-currency")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage GetCurrency(string id)
        {
            try
            {
                var response = CurrencyService.GetCurrency(Convert.ToInt16(id));
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
        [Route("create-currency")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage CreateCurrency(CreateCurrencyRequest createRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                string username = User.Identity.Name;
                var response = CurrencyService.CreateCurrency(createRequest, username);
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
        [Route("delete-currency")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage DeleteCurrency(int id)
        {
            try
            {
                string username = User.Identity.Name;
                var response = CurrencyService.Delete(Convert.ToInt16(id), username);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
    }
}
